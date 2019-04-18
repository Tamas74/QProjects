Imports gloCCDLibrary
Imports System.IO
Imports System.Data.SqlClient
Imports gloGlobal
Imports System.Net
Imports System.Collections.Specialized
Imports System.Net.Http
Imports System.Threading.Tasks
Imports System.Threading
Imports System.Web.Services
Imports Microsoft.Win32

'Added by kanchan on 20101008

Public Class frmAttachment
    ''Private mPatient As gloCCDLibrary.Patient
    'Private NewDocumentName As String   'Added by kanchan on 20101020 for CCD
    Private mMDIParent As Form
    Private mEffectiveTime As String = ""
    ''Private sFileType As String = "" 'Added by kanchan on 20101008
    Dim _DashBoardPatientID As Long
    Dim isregisterrequired As Boolean = False

    Private _IsInboxForm As Boolean = False
    Private Shared AddTrustedSitetoRegistry As Boolean = False
    Dim strNonXMLFilePath As String = ""
    Public WithEvents oEDocumentV3 As gloEDocumentV3.gloEDocV3Management
    Public Property IsInboxForm() As Boolean
        Get
            Return _IsInboxForm
        End Get
        Set(ByVal value As Boolean)
            _IsInboxForm = value
        End Set
    End Property

#Region "Contructor and Form Load"

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal PatientID As Long)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _DashBoardPatientID = PatientID
    End Sub

    Public Property MyMDIParent() As Form
        Get
            Return mMDIParent
        End Get
        Set(ByVal value As Form)
            mMDIParent = value
        End Set
    End Property

    Private Sub frmAttachment_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If Not IsNothing(oEDocumentV3) Then
            oEDocumentV3.Dispose()
            oEDocumentV3 = Nothing
        End If

    End Sub

    Private Sub frmAttachment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If rbExistingPatient.Checked Then
            pnlReconciliationType.Visible = True
        Else
            pnlReconciliationType.Visible = False
        End If
        If (AddTrustedSitetoRegistry = False) Then
            Try
                TrustedSiteRegistry.SaveToTrustedSite(gloCCDLibrary.gloLibCCDGeneral.sCDAValidatorUrl)
                AddTrustedSitetoRegistry = True
            Catch ex As Exception

            End Try
        End If
        cmbImportnExtract.Items.Add("Import and Reconcile")
        cmbImportnExtract.Items.Add("Import and Extract Lists")
        cmbImportnExtract.Items.Add("Only Import File")
        cmbImportnExtract.SelectedIndex = 0

        Try
            gloPatient.gloPatient.GetWindowTitle(Me, _DashBoardPatientID, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex, gloAuditTrail.ActivityOutCome.Failure)
        End Try
        ''Added by Mayuri :20151120-Import CCDA Emebedded documents into DMS-Version 8070
        If _IsInboxForm = True Then
            Dim _sFileType As String = ""
            _sFileType = gloReconciliation.GetFileType(txtClinicalPath.Text)

            If _sFileType = "CDA" Or _sFileType = "CCD" Then
                Dim oCDAReader As gloCDAReader = New gloCDAReader()
                Dim mediatype As String = ""

                Try
                    strNonXMLFilePath = oCDAReader.IsExistsCCDANonXMLBody(txtClinicalPath.Text, mediatype)
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
                    Me.Close()
                End Try


                If mediatype = "image/gif" Or mediatype = "image/tiff" Or mediatype = "image/jpeg" Or mediatype = "image/png" Then
                    Dim oDocManager As New gloEDocumentV3.eDocManager.eDocManager()
                    Dim osourcedocuments As New ArrayList
                    osourcedocuments.Add(strNonXMLFilePath)

                    Dim _strfileName As String = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".pdf", "MMddyyyyHHmmssffff")
                    Dim _isResult As Boolean = False
                    _isResult = oDocManager.ImportImages(osourcedocuments, _strfileName, "", True)
                    strNonXMLFilePath = _strfileName
                    osourcedocuments.Clear()
                    osourcedocuments = Nothing
                    oDocManager.Dispose()
                    oDocManager = Nothing
                    oCDAReader.Dispose()
                    oCDAReader = Nothing
                ElseIf mediatype = "application/msword" Or mediatype = "text/plain" Or mediatype = "text/rtf" Or mediatype = "text/html" Then
                    strNonXMLFilePath = gloWord.gloWord.ConvertFileToPDF(strNonXMLFilePath, gloSettings.FolderSettings.AppTempFolderPath)
                End If
            End If
            If strNonXMLFilePath <> "" Then
                cmbImportnExtract.Text = "Only Import File"
                cmbImportnExtract.Enabled = False
                chkSendTask.Visible = False
            End If
        End If

    End Sub

#End Region

    'Import CCD file
    Private Sub btnAttachFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblOK.Click

        Dim _sResult As String = ""
        _sResult = ImportFile()
        If IsInboxForm = False Then
            txtClinicalPath.Text = ""
        End If


    End Sub

    'Import CCD file
    Private Function ImportFile() As String

        Dim _sResult As String = ""
        Dim _sFileType As String = ""
        Dim _sFilePath As String = ""
        Dim oReconcileList As ReconcileList = Nothing
        If rbNewPatient.Checked = True Then
            If IsInboxForm = False Then
                isregisterrequired = True
            End If


        End If


        Try

            If txtClinicalPath.Text = "" Then
                MessageBox.Show("Select Clinical Document", gloCCDGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                _sResult = ""
                ImportFile = _sResult
                Exit Function
            End If

            If File.Exists(txtClinicalPath.Text) = False Then
                MessageBox.Show(txtClinicalPath.Text & " is not a valid path.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                _sResult = ""
                ImportFile = _sResult
                Exit Function
            End If

            If (IsNothing(gnCCDDefaultUserID) = True OrElse gnCCDDefaultUserID = 0) AndAlso chkSendTask.Checked Then
                Dim DialRes As DialogResult = MessageBox.Show("CCDA default user is not configured in gloEMR Admin and hence task will not be generated." & vbCrLf & vbCrLf & "Please configure the user for task generation.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                'If DialRes = Windows.Forms.DialogResult.Cancel Then
                '    ImportFile = _sResult
                'End If
                '03-Mar-16 Resolving: Bug #93906: gloEMR->Tools->CDA Files->New Patient->Import->shows blank user name in messagebox
                ImportFile = ""
                Exit Function
            End If

            _sFilePath = txtClinicalPath.Text
            _sFileType = gloReconciliation.GetFileType(_sFilePath)


            If _sFileType.Trim() = "" Then
                MessageBox.Show("Import failed : The file your trying to import is either invalid or not a recognized cda format. ", gloCCDGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                _sResult = ""
                ImportFile = _sResult
                Exit Function
            End If
            Label24.BringToFront()
            pnlErrorMessage.Visible = True
            pnlErrorMessage.BringToFront()
            If _sFileType = "CCR" Then
                Dim oCCRReader As gloCCRReader = New gloCCRReader()
                If Not IsNothing(oCCRReader) Then
                    oReconcileList = oCCRReader.ExtractCCR_DemographicsOnly(_sFilePath)
                    oCCRReader.Dispose()
                End If
            ElseIf _sFileType = "CCD" Then
                Dim oCCDReader As gloCCDReader = New gloCCDReader()
                If Not IsNothing(oCCDReader) Then
                    oReconcileList = oCCDReader.ExtractCCD_DemographicsOnly(_sFilePath)
                    oCCDReader.Dispose()
                End If
            ElseIf _sFileType = "CDA" Then
                Dim oCDAReader As gloCDAReader = New gloCDAReader()
                If Not IsNothing(oCDAReader) Then
                    '' check document confidentiality code
                    If oCDAReader.getDocConfidentiality(_sFilePath) = "R" Then
                        '' check Import Restricted CCDA Access for user
                        If gloCCDLibrary.gloLibCCDGeneral.bImportRestrictedCCD = False Then
                            MessageBox.Show("Import Restricted: You do not have sufficient privileges to import the selected CCDA document." & vbCrLf & "Please contact system administrator to grant the required access.", gloCCDGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.Extract, "Import Restricted: User do not have sufficient privileges to import the selected CCDA document." & _sFileType, _DashBoardPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                            pnlErrorMessage.Visible = False
                            _sResult = ""
                            ImportFile = _sResult
                            Exit Function
                        End If
                    End If
                    oCDAReader.gstrCCDAImportCategory = gstrCCDAImportCategory
                    oReconcileList = oCDAReader.ExtractCDA_DemographicsOnly(_sFilePath, False)
                    oCDAReader.Dispose()
                End If
            End If
            pnlErrorMessage.Visible = False
            If Not IsNothing(oReconcileList) Then

                oReconcileList.UserID = gnLoginID
                oReconcileList.UserName = gstrLoginName
                oReconcileList.FilePath = txtClinicalPath.Text
                oReconcileList.FileType = _sFileType
                oReconcileList.PatientID = _DashBoardPatientID
                oReconcileList.CCDID = 0
                oReconcileList.ListName = ""
                oReconcileList.SourceName = oReconcileList.FileHeaderSource
                gloLibCCDGeneral.ClinicalDocFileType = _sFileType
                'If IsNothing(oReconcileList) Then
                '    _sResult = ""
                '    Exit Function
                'End If
                Dim strErrMessage As String = ""
                Dim CCDPatinet As String = ""
                gsCCDUSerName = GetLoginName(gnCCDDefaultUserID)

                ''Match Patient and Set ReconcileList Object 

                If rbExistingPatient.Checked = True Then

                    'isregisterrequired = False
                    If Not IsNothing(oReconcileList) AndAlso Not IsNothing(oReconcileList.mPatient) Then
                        Dim objCCDReconcilation As New gloCCDReconcilation()
                        Dim _SamePatientResult As String = String.Empty
                        If IsInboxForm = True Then
                            CCDPatinet = oReconcileList.mPatient.PatientDemographics.DemographicsDetail.PatientLastName.ToUpper() & ", " & oReconcileList.mPatient.PatientDemographics.DemographicsDetail.PatientFirstName & " " & oReconcileList.mPatient.PatientDemographics.DemographicsDetail.PatientDOB.ToString("MM/dd/yyyy") & " " & oReconcileList.mPatient.PatientDemographics.DemographicsDetail.PatientGender
                            Dim _sureScriptPatientID As Long = objCCDReconcilation.GetPatientIDFromFile(oReconcileList.mPatient)
                            If _sureScriptPatientID = 0 Then

                                'isregisterrequired = True
                                Dim oProvider As gloCCDLibrary.PatientProvider = Nothing
                                If Not IsNothing(oReconcileList.mPatient.PatientProviders) AndAlso oReconcileList.mPatient.PatientProviders.Count > 0 Then
                                    oProvider = oReconcileList.mPatient.PatientProviders.Item(0)
                                    strErrMessage = "Patient Name: " & oReconcileList.mPatient.PatientDemographics.DemographicsDetail.PatientLastName.ToUpper() & ", " & oReconcileList.mPatient.PatientDemographics.DemographicsDetail.PatientFirstName & Environment.NewLine & "Date of Birth: " & oReconcileList.mPatient.PatientDemographics.DemographicsDetail.PatientDOB.ToString("MM/dd/yyyy") & Environment.NewLine & "Gender: " & oReconcileList.mPatient.PatientDemographics.DemographicsDetail.PatientGender & Environment.NewLine & "Provider Name: " & oProvider.FirstName & " " & oProvider.LastName
                                Else
                                    strErrMessage = "Patient Name: " & oReconcileList.mPatient.PatientDemographics.DemographicsDetail.PatientLastName.ToUpper() & ", " & oReconcileList.mPatient.PatientDemographics.DemographicsDetail.PatientFirstName & Environment.NewLine & "Date of Birth: " & oReconcileList.mPatient.PatientDemographics.DemographicsDetail.PatientDOB.ToString("MM/dd/yyyy") & Environment.NewLine & "Gender: " & oReconcileList.mPatient.PatientDemographics.DemographicsDetail.PatientGender
                                End If
                                If Not IsNothing(oReconcileList.mPatient.PatientProviders) Then
                                    oReconcileList.mPatient.PatientProviders.Dispose()
                                    oReconcileList.mPatient.PatientProviders = Nothing
                                End If
                                If Not IsNothing(oProvider) Then
                                    oProvider.Dispose()
                                    oProvider = Nothing
                                End If
                                If Not IsNothing(oReconcileList.mPatient) Then


                                    oReconcileList.mPatient.Dispose()
                                    oReconcileList.mPatient = Nothing
                                End If

                                MessageBox.Show(Environment.NewLine & strErrMessage & Environment.NewLine & Environment.NewLine & "is not found." & Environment.NewLine & Environment.NewLine & "Please select New Patient.", gloCCDGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                _SamePatientResult = ""
                                ImportFile = _sResult
                                Exit Function
                            Else
                                _DashBoardPatientID = _sureScriptPatientID
                                _SamePatientResult = "True"
                            End If
                        Else
                            _SamePatientResult = objCCDReconcilation.IsSamePatientAsDashboard(oReconcileList.mPatient, _DashBoardPatientID)
                        End If

                        If (_SamePatientResult = "True") Then
                            oReconcileList.mPatient.PatientName.ID = _DashBoardPatientID
                        Else '' Patient Missmatch

                            If oReconcileList.mPatient.PatientDemographics.DemographicsDetail.PatientDOB.ToString("MM/dd/yyyy") <> "01/01/0001" Then
                                CCDPatinet = oReconcileList.mPatient.PatientDemographics.DemographicsDetail.PatientLastName.ToUpper() & ", " & oReconcileList.mPatient.PatientDemographics.DemographicsDetail.PatientFirstName & " " & oReconcileList.mPatient.PatientDemographics.DemographicsDetail.PatientDOB.ToString("MM/dd/yyyy") & " " & oReconcileList.mPatient.PatientDemographics.DemographicsDetail.PatientGender
                            Else
                                CCDPatinet = oReconcileList.mPatient.PatientDemographics.DemographicsDetail.PatientLastName.ToUpper() & ", " & oReconcileList.mPatient.PatientDemographics.DemographicsDetail.PatientFirstName & " " & oReconcileList.mPatient.PatientDemographics.DemographicsDetail.PatientGender
                            End If

                            ''Show unmatch dialog
                            Dim frmUnMatchDialog As New frmUnMachedPatientDialog(CCDPatinet, _SamePatientResult)
                            Dim DigResult As DialogResult
                            DigResult = frmUnMatchDialog.ShowDialog(IIf(IsNothing(frmUnMatchDialog.Parent), Me, frmUnMatchDialog.Parent))
                            If Not IsNothing(frmUnMatchDialog) Then
                                frmUnMatchDialog.Dispose()
                            End If

                            If DigResult = Windows.Forms.DialogResult.No Then
                                '' Patient Mismatch Found Stop
                                _sResult = ""
                                If Not IsNothing(objCCDReconcilation) Then
                                    objCCDReconcilation.Dispose()
                                    objCCDReconcilation = Nothing
                                End If
                                ImportFile = _sResult
                                Exit Function
                            ElseIf DigResult = Windows.Forms.DialogResult.Cancel Then
                                '' Patient Mismatch Found so Select Different Patient
                                Me.Close()
                                _sResult = ""
                                If Not IsNothing(objCCDReconcilation) Then
                                    objCCDReconcilation.Dispose()
                                    objCCDReconcilation = Nothing
                                End If
                                ImportFile = _sResult
                                Exit Function
                            ElseIf DigResult = Windows.Forms.DialogResult.Yes Then
                                '' Patient Mismatch Found still continue with selected patient
                                oReconcileList.mPatient.PatientDemographics = objCCDReconcilation.GetDashBoardPatient(_DashBoardPatientID)
                                oReconcileList.mPatient.PatientName.ID = _DashBoardPatientID
                                oReconcileList.mPatient.PatientName.Code = oReconcileList.mPatient.PatientDemographics.DemographicsDetail.PatientCode
                                oReconcileList.mPatient.PatientName.FirstName = oReconcileList.mPatient.PatientDemographics.DemographicsDetail.PatientFirstName
                                oReconcileList.mPatient.PatientName.LastName = oReconcileList.mPatient.PatientDemographics.DemographicsDetail.PatientLastName
                                oReconcileList.mPatient.DateofBirth = oReconcileList.mPatient.PatientDemographics.DemographicsDetail.PatientDOB.ToString("MM/dd/yyyy")
                                oReconcileList.mPatient.Gender = oReconcileList.mPatient.PatientDemographics.DemographicsDetail.PatientGender

                            End If
                        End If
                        If Not IsNothing(objCCDReconcilation) Then
                            objCCDReconcilation.Dispose()
                            objCCDReconcilation = Nothing
                        End If
                    End If



                End If


                ''----------------------------------View CCD File------------------------------------
                Dim myXslTransform As Xml.Xsl.XslTransform = Nothing
                Dim oPatientClinicalInfo As New frmPatientClinicalInformation(isregisterrequired)
                Dim oDialogResultClinicalInfo As DialogResult

                Try

                    oPatientClinicalInfo.EffectiveTime = oReconcileList.FileHeaderDateTime
                    oPatientClinicalInfo.FileType = _sFileType
                    oPatientClinicalInfo.SourceName = oReconcileList.FileHeaderSource
                    If (rbExistingPatient.Checked = True) Then
                        oPatientClinicalInfo.CCDPatient = oReconcileList.mPatient
                    End If
                    oPatientClinicalInfo.strNonXML = strNonXMLFilePath
                    'Convert XML file to readatble format by using XSLT'StartupPath was changed to AppTempFolderPath by SLR
                    'Dim myXslTransform As Xml.Xsl.XslTransform
                    Dim _strfileName As String = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".html", "yyyyMMddhhmmssffff") 'DateTime.Now.ToString("yyyyMMddhhmmssffff") & System.Guid.NewGuid().ToString() & ".html"
                    Select Case _sFileType
                        Case "CCD"
                            oPatientClinicalInfo.CCDXMLFilePath = _sFilePath
                            myXslTransform = New Xml.Xsl.XslTransform()
                            'myXslTransform.Load("http://www.glostream.com/css/XSLT/gloCCDAcss_MU2.xsl")
                            myXslTransform.Load(Application.StartupPath & "/gloCCDAcss_MU2.xsl")
                            myXslTransform.Transform(txtClinicalPath.Text, _strfileName) 'System.IO.Path.Combine(Application.StartupPath, _strfileName))
                            oPatientClinicalInfo.WebBrowser1.Navigate(_strfileName) 'System.IO.Path.Combine(Application.StartupPath, _strfileName))
                            ''Added by Mayuri :20151120-Import CCDA Emebedded documents into DMS-Version 8070
                            If _IsInboxForm Then
                                If strNonXMLFilePath <> "" Then
                                    oPatientClinicalInfo.ShowPanel()
                                    oPatientClinicalInfo.ShowPDFPreview(strNonXMLFilePath)
                                End If
                            Else
                                Dim oCDAReader As New gloCDAReader
                                Dim _IsInvalid As Boolean
                                _IsInvalid = oCDAReader.IsInValidCCDA(txtClinicalPath.Text)
                                If _IsInvalid Then
                                    MessageBox.Show("There is no discrete clinical data available for this patient. hence the file can not be imported. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Return ""
                                End If
                                oCDAReader.Dispose()
                                oCDAReader = Nothing
                            End If


                        Case "CDA"
                            ''Dim oCDAReader1 As gloCDAReader = New gloCDAReader()
                            ''If Not IsNothing(oCDAReader1) Then
                            ''    '' check document confidentiality code
                            ''    If oCDAReader1.getDocConfidentiality(_sFilePath) = "R" Then
                            ''        '' check Import Restricted CCDA Access for user
                            ''        If gloCCDLibrary.gloLibCCDGeneral.bImportRestrictedCCD = False Then
                            ''            MessageBox.Show("Import Restricted: You do not have sufficient privileges to import the selected CCDA document." & vbCrLf & "Please contact system administrator to grant the required access.", gloCCDGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            ''            _sResult = ""
                            ''            ImportFile = _sResult
                            ''            Exit Function
                            ''        End If
                            ''    End If
                            ''End If
                            ''oCDAReader1.Dispose()
                            ''oCDAReader1 = Nothing

                            If Not IsNothing(oReconcileList) Then
                                oPatientClinicalInfo.CCDXMLFilePath = _sFilePath ''set for bugid 108656
                                If oReconcileList.cdaviewerhtmlFile = "" Then
                                    myXslTransform = New Xml.Xsl.XslTransform()
                                    myXslTransform.Load(Application.StartupPath & "/gloCCDAcss_MU2.xsl")
                                    myXslTransform.Transform(txtClinicalPath.Text, _strfileName)
                                    oPatientClinicalInfo.WebBrowser1.Navigate(_strfileName)
                                Else
                                    showDataInPreviewControl(oPatientClinicalInfo.WebBrowser1, oReconcileList.cdaviewerhtmlFile)
                                End If
                            End If


                            ''Added by Mayuri :20151120-Import CCDA Emebedded documents into DMS-Version 8070
                            If strNonXMLFilePath <> "" Then
                                oPatientClinicalInfo.ShowPanel()
                                oPatientClinicalInfo.ShowPDFPreview(strNonXMLFilePath)
                            Else
                                Dim oCDAReader As New gloCDAReader
                                Dim _IsInvalid As Boolean
                                _IsInvalid = oCDAReader.IsInValidCCDA(txtClinicalPath.Text)
                                If _IsInvalid Then
                                    MessageBox.Show("There is no discrete clinical data available for this patient. hence the file can not be imported. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Return ""
                                End If
                                
                                '' add condition for Confidentiality
                                If oCDAReader.getDocConfidentiality(_sFilePath) = "R" Then
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.Extract, "CCDA file imported with confidentiality: Restricted.", _DashBoardPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
                                ElseIf oCDAReader.getDocConfidentiality(_sFilePath) = "N" Then
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.Extract, "CCDA file imported with confidentiality: Normal.", _DashBoardPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
                                End If
                                '' add condition for Confidentiality

                                oCDAReader.Dispose()
                                oCDAReader = Nothing
                            End If

                        Case "CCR"
                            oPatientClinicalInfo.CCDXMLFilePath = _sFilePath
                            myXslTransform = New Xml.Xsl.XslTransform()
                            'myXslTransform.Load("http://www.glostream.com/css/XSLT/gloccrCss.xsl")
                            myXslTransform.Load(Application.StartupPath & "/gloccrCss.xsl")
                            myXslTransform.Transform(txtClinicalPath.Text, _strfileName) 'System.IO.Path.Combine(Application.StartupPath, _strfileName))
                            oPatientClinicalInfo.WebBrowser1.Navigate(_strfileName) 'System.IO.Path.Combine(Application.StartupPath, _strfileName))
                    End Select

                    If IsNothing(_sFileType) = False Then
                        oPatientClinicalInfo.IsShowSaveButton = True
                    End If
                    If strNonXMLFilePath <> "" Then
                        oPatientClinicalInfo.WindowState = FormWindowState.Maximized
                    Else
                        oPatientClinicalInfo.WindowState = FormWindowState.Normal
                    End If

                    oPatientClinicalInfo.StartPosition = FormStartPosition.CenterScreen
                    oPatientClinicalInfo.BringToFront()

                    oDialogResultClinicalInfo = oPatientClinicalInfo.ShowDialog(IIf(IsNothing(oPatientClinicalInfo.Parent), Me, oPatientClinicalInfo.Parent))
                    oReconcileList.SourceName = oPatientClinicalInfo.SourceName
                Catch ex As Exception



                    If ex.ToString.Contains("Invalid character in the given encoding") = True Then
                        MessageBox.Show("The file selected is an invalid CCDA file. This file cannot be viewed or imported.", gloCCDGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Else
                        MessageBox.Show("Error while viewing the file. " + Environment.NewLine + Environment.NewLine + ex.ToString(), gloCCDGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                    If Not IsNothing(oPatientClinicalInfo) Then
                        oPatientClinicalInfo.Dispose()
                        oPatientClinicalInfo = Nothing
                    End If

                    If Not IsNothing(myXslTransform) Then
                        myXslTransform = Nothing
                    End If


                End Try

                If Not IsNothing(myXslTransform) Then
                    myXslTransform = Nothing
                End If
                ''----------------------------------View CCD File------------------------------------


                'Create task Start--------------------------------------------
                If oDialogResultClinicalInfo = DialogResult.OK Then
                    If rbExistingPatient.Checked = True Then
                        If chkSendTask.Visible Then


                            If chkSendTask.Checked Then
                                Dim _PatientName As String
                                Dim _Taskid As Int64
                                Dim _CCDId As Int64
                                Dim _Subject As String = ""
                                Dim _Note As String = ""
                                Dim _TaskType As Int32

                                _CCDId = InsertInCCDQueue(oReconcileList.mPatient, oReconcileList.FilePath, _DashBoardPatientID, oReconcileList.SourceName)
                                _PatientName = oReconcileList.mPatient.PatientDemographics.DemographicsDetail.PatientFirstName & " " & oReconcileList.mPatient.PatientDemographics.DemographicsDetail.PatientLastName
                                _Subject = "Clinical document is available"
                                _Note = oReconcileList.FileType & " file available for Patient " & _PatientName & " for " & gsCCDUSerName
                                _TaskType = gloTaskMail.TaskType.CCD
                                _Taskid = GenerateTasks(_DashBoardPatientID, _Subject, _Note, _CCDId, _TaskType)

                                If _Taskid = 0 Then
                                    _sResult = ""
                                    'Exit Function
                                End If
                                UpdateTaskID_CCDQueue(_CCDId, _Taskid)
                            End If
                        ElseIf _IsInboxForm = True Then
                            openSendToDMSscreen(_DashBoardPatientID)


                        End If
                    ElseIf (rbNewPatient.Checked = True) Then
                        Dim _PatientName As String
                        Dim _Taskid As Int64
                        Dim _CCDId As Int64
                        Dim _Subject As String = ""
                        Dim _Note As String = ""
                        Dim _TaskType As Int32
                        _CCDId = InsertInCCDQueue(oReconcileList.mPatient, oReconcileList.FilePath, 0, oReconcileList.SourceName)
                        If IsInboxForm Then
                            Dim npatientid As Int64 = 0
                            _TaskType = gloTaskMail.TaskType.CCDUnmatchedPatient
                            If _CCDId > 0 Then
                                Dim objfrmCCD_Display As frmCCD_Display = New frmCCD_Display(npatientid)
                                objfrmCCD_Display.TaskType = _TaskType
                                objfrmCCD_Display.CCDId = _CCDId
                                objfrmCCD_Display.IsInboxForm = _IsInboxForm
                                objfrmCCD_Display.WindowState = FormWindowState.Normal
                                objfrmCCD_Display.StartPosition = FormStartPosition.CenterScreen
                                ''Resolved issue #62659-Mayuri 01/29/2014-Tasks - Closing Show CCD-CCR window also closes Task window
                                If objfrmCCD_Display.ShowDialog() = Windows.Forms.DialogResult.OK Then

                                    If strNonXMLFilePath <> "" Then
                                        openSendToDMSscreen(objfrmCCD_Display.DMSPatientID)
                                    End If

                                    objfrmCCD_Display.Dispose()
                                    objfrmCCD_Display = Nothing
                                End If

                            End If
                        Else
                            ' Import CCD file for new patient
                            _PatientName = oReconcileList.mPatient.PatientDemographics.DemographicsDetail.PatientFirstName & " " & oReconcileList.mPatient.PatientDemographics.DemographicsDetail.PatientLastName
                            _Subject = "New patient : " & _PatientName & " found in " & oReconcileList.FileType & " file"
                            _Note = "New Patient: " & _PatientName & " found in " & oReconcileList.FileType & " file for " & gsCCDUSerName
                            _TaskType = gloTaskMail.TaskType.CCDUnmatchedPatient
                            _Taskid = GenerateTasks(0, _Subject, _Note, _CCDId, _TaskType)
                            If _Taskid = 0 Then
                                _sResult = ""
                                'Exit Function
                            End If
                            UpdateTaskID_CCDQueue(_CCDId, _Taskid)
                        End If

                        'If Not IsNothing(oReconcileList.mPatient) Then
                        '    oReconcileList.mPatient.Dispose()
                        '    oReconcileList.mPatient = Nothing
                        'End If
                    End If
                End If
                'Create task End--------------------------------------------

                If oDialogResultClinicalInfo = DialogResult.OK Then

                    ''File Import Successful
                    If cmbImportnExtract.Text.Trim = "Import and Extract Lists" AndAlso (rbExistingPatient.Checked = True) Then
                        ' Dim _ListStatus As Integer

                        Dim ofrm As New frmCCD_ExtractReconcillation(oPatientClinicalInfo.CCDID, _DashBoardPatientID, oPatientClinicalInfo.SourceName)
                        If IsNothing(ofrm) = False Then
                            ofrm.ShowInTaskbar = False
                            ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))

                            ofrm.Close()
                            ofrm.Dispose()
                            ofrm = Nothing
                        End If
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Reconciliation, gloAuditTrail.ActivityType.Extract, "Imported and Extracted " & _sFileType & " data.", _DashBoardPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                    ElseIf cmbImportnExtract.Text.Trim = "Import and Reconcile" AndAlso (rbExistingPatient.Checked = True) Then
                        ' Dim _ListStatus As Integer

                        Dim ofrm As New frmCCD_ExtractReconcillation(oPatientClinicalInfo.CCDID, _DashBoardPatientID, oPatientClinicalInfo.SourceName)
                        If IsNothing(ofrm) = False Then
                            ofrm.ShowInTaskbar = False
                            If ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent)) = DialogResult.OK Then
                                Dim frmReconcilation As New frmReconcileList(_DashBoardPatientID, "")
                                frmReconcilation.LoginUser = gstrLoginName
                                frmReconcilation.LoginID = gnLoginID
                                frmReconcilation.ShowDialog(IIf(IsNothing(frmReconcilation.Parent), Me, frmReconcilation.Parent))
                                frmReconcilation.Dispose()
                                frmReconcilation = Nothing
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Reconciliation, gloAuditTrail.ActivityType.Extract, "Imported and Reconciled " & _sFileType & " data.", _DashBoardPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                            End If
                            ofrm.Close()
                            ofrm.Dispose()
                            ofrm = Nothing
                        End If

                    Else

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Select, "Imported " & _sFileType & " file.", _DashBoardPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
                    End If


                End If
                If Not IsNothing(oPatientClinicalInfo) Then
                    oPatientClinicalInfo.Dispose()
                    oPatientClinicalInfo = Nothing
                End If

            End If   ''If Not IsNothing(oReconcileList) Then    



        Catch ex As Net.WebException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Select, "Could not Import CCD data.", gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Application is not able to connect to remote server", gloCCDGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Select, "Could not Import CCD data.", gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gloCCDGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oReconcileList) Then
                oReconcileList.Dispose()
            End If
        End Try

        ImportFile = _sResult
    End Function



    Private Sub openSendToDMSscreen(ByVal DMSPatientID As Int64)
        Try


            gloEDocumentV3.gloEDocV3Admin.Connect(GetConnectionString(), GetDMSConnectionString(), gDMSV2TempPath, Convert.ToInt64(gnLoginID), gClinicID, System.Windows.Forms.Application.StartupPath)

            If IsNothing(oEDocumentV3) Then
                oEDocumentV3 = New gloEDocumentV3.gloEDocV3Management
            End If

            oEDocumentV3.ShowEDMSFromDirect(strNonXMLFilePath, gloEDocumentV3.Enumeration.enum_OpenExternalSource.DirectMessage, DMSPatientID)
            oEDocumentV3.Dispose()
            oEDocumentV3 = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub btnOpenFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenFile.Click
        Try
            'If Not IsNothing(dlgOpenFile) Then
            '    dlgOpenFile.Dispose()
            '    dlgOpenFile = Nothing
            'End If

            'dlgOpenFile = New OpenFileDialog()
            dlgOpenFile.Title = "Select Clinical Document"
            '            dlgOpenFile.Filter = "Images Files(*.bmp,*.jpg,*.jpeg,*.gif)|*.bmp;*.jpg;*.jpeg;*.gif"
            dlgOpenFile.Filter = "XML Files(*.xml)|*.xml"
            dlgOpenFile.CheckFileExists = True
            dlgOpenFile.Multiselect = False
            dlgOpenFile.ShowHelp = False
            dlgOpenFile.ShowReadOnly = False
            Dim bresult As DialogResult = dlgOpenFile.ShowDialog(System.Windows.Forms.Form.ActiveForm)
            If bresult = Windows.Forms.DialogResult.OK Then
                gloLibCCDGeneral.CCDFilePath = dlgOpenFile.FileName
                txtClinicalPath.Text = gloLibCCDGeneral.CCDFilePath
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gloCCDGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'If Not IsNothing(dlgOpenFile) Then
            '    dlgOpenFile.Dispose()
            '    dlgOpenFile = Nothing
            'End If
        End Try
    End Sub

    'close window
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblClose.Click
        Me.Close()
    End Sub


#Region "Private Methods"

    'Code Start-added by kanchan on 20101008 for CCD Queue
    Private Function GenerateTasks(ByVal _PatientID As Int64, ByVal _Subject As String, ByVal _Note As String, ByVal _CCDID As Int64, ByVal _TaskType As Int32) As Long
        Dim oTask As gloTaskMail.Task = Nothing
        Dim ogloTask As gloTaskMail.gloTask = Nothing
        Dim oTaskAssign As gloTaskMail.TaskAssign = Nothing
        Try
            Dim _TaskID As Long = 0

            If IsNothing(gnCCDDefaultUserID) = True OrElse gnCCDDefaultUserID = 0 Then
                'MessageBox.Show("No CCD user have been associated, please configure using gloEMR Admin", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                GenerateTasks = Nothing
                Exit Function
            Else
                '    '' Send the Task to The Users

                Dim dtDueDate As DateTime = Now
                Dim dtTaskDate As DateTime = Now
                Dim sPriority As String = "High"
                dtDueDate = Format(dtDueDate, "MM/dd/yyyy") & " " & Format(dtDueDate, "Short Time")
                dtTaskDate = Format(dtTaskDate, "MM/dd/yyyy") & " " & Format(dtTaskDate, "Short Time")

                oTask = New gloTaskMail.Task
                ogloTask = New gloTaskMail.gloTask(gloLibCCDGeneral.Connectionstring)
                oTaskAssign = New gloTaskMail.TaskAssign(gloLibCCDGeneral.Connectionstring)

                oTask.TaskID = 0
                oTask.UserID = gnLoginID
                oTask.DateCreated = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dtTaskDate))
                oTask.StartDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dtTaskDate))
                oTask.DueDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dtDueDate))
                oTask.Subject = _Subject
                oTask.PriorityID = 3 ''High
                oTask.Notes = _Note
                oTask.PatientID = _PatientID
                oTask.ReferenceID1 = _CCDID
                oTask.ClinicID = gnClinicID
                oTask.OwnerID = gnLoginID
                oTask.TaskType = _TaskType
                'If _TaskType = 9 Then
                '    oTask.TaskType = gloTaskMail.TaskType.CCD
                'ElseIf _TaskType = 10 Then
                '    oTask.TaskType = gloTaskMail.TaskType.CCDUnmatchedPatient
                'End If

                oTaskAssign.AssignFromID = gnLoginID
                oTaskAssign.AssignFromName = gstrLoginName
                oTaskAssign.AssignToID = gnCCDDefaultUserID
                If oTaskAssign.AssignFromID = oTaskAssign.AssignToID Then
                    oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Self
                    oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Accept
                Else
                    oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Assigned
                    oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Hold
                End If
                oTaskAssign.AssignToName = gsCCDUSerName
                oTask.Assignment.Add(oTaskAssign)
                '          oTaskAssign.Dispose()

                ''Task Assign Properties
                ''Task Progress Values
                oTask.Progress.TaskID = 0
                oTask.Progress.Complete = 0
                oTask.Progress.Description = _Note
                oTask.Progress.StatusID = 1 ''Not Started
                oTask.Progress.DateTime = Now.Date
                oTask.Progress.ClinicID = gnClinicID
                _TaskID = ogloTask.Add(oTask)
                Return _TaskID
            End If
        Catch ex As Exception
            Return 0
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oTask) Then
                oTask.Dispose()
                oTask = Nothing
            End If
            If Not IsNothing(ogloTask) Then
                ogloTask.Dispose()
                ogloTask = Nothing
            End If
            If Not IsNothing(oTaskAssign) Then
                oTaskAssign.Dispose()
                oTaskAssign = Nothing
            End If
        End Try

    End Function

    'Code Start-added by kanchan on 20101008 for CCD Queue
    Private Function GetLoginName(ByVal LoginID As Int64) As String
        Dim cmd As New SqlCommand
        Dim conn As SqlConnection = Nothing
        Dim strQuery As String = ""
        Dim _LoginName As String = ""
        Try
            conn = New SqlConnection(gloLibCCDGeneral.Connectionstring)

            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            strQuery = "SELECT sLoginName from User_Mst where nUserID=" & LoginID
            cmd.CommandText = strQuery
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Dim temp As Object = cmd.ExecuteScalar()
            If Not IsNothing(temp) Then
                _LoginName = temp.ToString()
            End If

            temp = Nothing
            Return _LoginName
        Catch ex As Exception
            Return ""
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            If Not IsNothing(conn) Then
                conn.Dispose()
                conn = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            strQuery = ""
        End Try
    End Function


    'Code Start-added by kanchan on 20101008
    Private Function UpdateTaskID_CCDQueue(ByVal _nCCDID As Int64, ByVal Taskid As Int64)
        Dim cmd As New SqlCommand
        Dim conn As SqlConnection = Nothing
        Dim strQuery As String = ""

        Try
            conn = New SqlConnection(gloLibCCDGeneral.Connectionstring)

            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            strQuery = "UPDATE CCD_Queue SET nTaskId=" & Taskid & " WHERE nCCDID=" & _nCCDID
            cmd.CommandText = strQuery
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            cmd.ExecuteNonQuery()
        Catch ex As Exception

        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            If Not IsNothing(conn) Then
                conn.Dispose()
                conn = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            strQuery = ""
        End Try
        Return Nothing
    End Function

    'Code Start- Added by kanchan on 20101020 for CCD Queue
    Private Function InsertInCCDQueue(ByVal objPatient As gloCCDLibrary.Patient, ByVal FilePath As String, ByVal _PatientId As Int64, ByVal Source As String) As Int64
        Dim conn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim sqlparam As SqlParameter
        Dim _CCDID As Int64 = 0
        Try
            Dim strFilename As String = CCDFile(gloSettings.FolderSettings.AppTempFolderPath, ".XML")
            Dim arrByte As Byte() = ConvertFiletoBinary(FilePath)

            conn = New SqlConnection(gloLibCCDGeneral.Connectionstring)

            Dim strQuery As String = ""

            cmd = New SqlCommand("CCD_InsertQueue", conn)
            cmd.CommandType = CommandType.StoredProcedure

            sqlparam = cmd.Parameters.Add("@nCCDID", SqlDbType.BigInt)
            sqlparam.Direction = ParameterDirection.InputOutput
            sqlparam.Value = _CCDID
            sqlparam = cmd.Parameters.Add("@sPatientCode", SqlDbType.VarChar, 50)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(objPatient.PatientDemographics.DemographicsDetail.PatientCode) Then
                sqlparam.Value = objPatient.PatientDemographics.DemographicsDetail.PatientCode
            Else
                sqlparam.Value = ""
            End If
            sqlparam = cmd.Parameters.Add("@sFirstName", SqlDbType.VarChar, 100)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(objPatient.PatientDemographics.DemographicsDetail.PatientFirstName) Then
                sqlparam.Value = objPatient.PatientDemographics.DemographicsDetail.PatientFirstName
            Else
                sqlparam.Value = ""
            End If
            'sqlparam = cmd.Parameters.Add("@sMiddleName", SqlDbType.VarChar, 50)
            'sqlparam.Direction = ParameterDirection.Input
            'sqlparam.Value = objPatient.PatientName.MiddleName
            sqlparam = cmd.Parameters.Add("@sLastName", SqlDbType.VarChar, 100)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(objPatient.PatientDemographics.DemographicsDetail.PatientLastName) Then
                sqlparam.Value = objPatient.PatientDemographics.DemographicsDetail.PatientLastName
            Else
                sqlparam.Value = ""
            End If
            sqlparam = cmd.Parameters.Add("@dtDOB", SqlDbType.DateTime)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(objPatient.PatientDemographics.DemographicsDetail.PatientDOB) Then
                Dim objdate As Object = objPatient.PatientDemographics.DemographicsDetail.PatientDOB.ToString("MM/dd/yyyy")
                If gloDateMaster.gloDate.IsValidDateV2(objdate) Then
                    sqlparam.Value = objPatient.PatientDemographics.DemographicsDetail.PatientDOB
                Else
                    sqlparam.Value = Now
                End If
            Else
                sqlparam.Value = Now
            End If
            sqlparam = cmd.Parameters.Add("@sGender", SqlDbType.VarChar, 10)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(objPatient.PatientDemographics.DemographicsDetail.PatientGender) Then
                sqlparam.Value = objPatient.PatientDemographics.DemographicsDetail.PatientGender
            Else
                sqlparam.Value = "Other"
            End If
            sqlparam = cmd.Parameters.Add("@sFileName", SqlDbType.VarChar, 50)
            sqlparam.Direction = ParameterDirection.Input
            ' NewDocumentName = GetNewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".XML")

            sqlparam.Value = strFilename 'NewDocumentName
            sqlparam = cmd.Parameters.Add("@iXMLData", SqlDbType.Image)
            sqlparam.Direction = ParameterDirection.Input
            sqlparam.Value = arrByte
            sqlparam = cmd.Parameters.Add("@sStatus", SqlDbType.VarChar, 50)
            sqlparam.Direction = ParameterDirection.Input
            sqlparam.Value = "Queue"
            sqlparam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            sqlparam.Direction = ParameterDirection.Input
            sqlparam.Value = _PatientId
            sqlparam = cmd.Parameters.Add("@nTaskId", SqlDbType.BigInt)
            sqlparam.Direction = ParameterDirection.Input
            sqlparam.Value = 0
            sqlparam = cmd.Parameters.Add("@sFileType", SqlDbType.VarChar, 10)
            sqlparam.Direction = ParameterDirection.Input
            sqlparam.Value = gloLibCCDGeneral.ClinicalDocFileType

            sqlparam = cmd.Parameters.Add("@sSource", SqlDbType.VarChar, 150)
            sqlparam.Direction = ParameterDirection.Input
            sqlparam.Value = Source

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            cmd.ExecuteNonQuery()

            If Not IsNothing(cmd.Parameters("@nCCDID").Value) Then
                _CCDID = CType(cmd.Parameters("@nCCDID").Value, Int64)
            End If

            arrByte = Nothing

            Return _CCDID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Select, "Could not Import CCD data.", gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gloCCDGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            If Not IsNothing(conn) Then
                conn.Dispose()
                conn = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlparam = Nothing

        End Try
    End Function
    'Code Start- Added by kanchan on 20101020 for CCD Queue

    Private Function ConvertFiletoBinary(ByVal strFileName As String) As Byte()
        If File.Exists(strFileName) Then
            Dim oFile As FileStream = Nothing
            Dim oReader As BinaryReader = Nothing
            Try
                ''To read the file only when it is not in use by any process
                oFile = New FileStream(strFileName, FileMode.Open, FileAccess.Read)

                oReader = New BinaryReader(oFile)
                Dim bytesRead As Byte() = oReader.ReadBytes(oFile.Length)
                Return bytesRead

            Catch ex As IOException
                Throw New Exception
            Catch ex As Exception
                Throw New Exception
            Finally
                If Not IsNothing(oReader) Then
                    oReader.Close()
                    oReader.Dispose()
                    oReader = Nothing
                End If
                If Not IsNothing(oFile) Then
                    oFile.Close()
                    oFile.Dispose()
                    oFile = Nothing
                End If

            End Try
        Else
            Return Nothing
        End If
    End Function

    'Code Start- Added by kanchan on 20101020 for CCD Queue
    Private ReadOnly Property CCDFile(ByVal _path As String, ByVal _extension As String) As String
        Get
            'NewDocumentName = ""
            '' Dim _Path As String = gstrgloEMRStartupPath & "\Temp"
            'Dim _NewDocumentName As String = ""
            '' Dim _Extension As String = _extension
            'Dim _dtCurrentDateTime As DateTime = System.DateTime.Now

            'Dim i As Integer = 0
            '_NewDocumentName = Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss tt") & _extension
            ''While File.Exists(_path & "\" & _NewDocumentName) = True
            'While File.Exists(_path & _NewDocumentName) = True And i < Int16.MaxValue
            '    i = i + 1
            '    _NewDocumentName = Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss tt") & "-" & i & _extension
            'End While
            '' Return _path & "\" & _NewDocumentName
            'NewDocumentName = _NewDocumentName
            'Return _path & _NewDocumentName
            Dim strFileName As String = ""
            strFileName = gloGlobal.clsFileExtensions.NewDocumentNameWithoutPath(_path, _extension, "MMddyyyyHHmmssffff")
            ' NewDocumentName = ""
            'NewDocumentName = strFileName.Replace(_path, "").Trim
            Return strFileName
        End Get
    End Property


    'Code Start- Added by kanchan on 20101020 for CCD Queue
    Private Function IsPatientExists(ByVal objPatient As gloCCDLibrary.Patient) As Int64
        'Dim cmd As New SqlCommand
        'Dim conn As SqlConnection
        Dim strQuery As String = ""
        Dim _PatientID As Int64 = 0
        Try
            'conn = New SqlConnection(gloLibCCDGeneral.Connectionstring)

            'cmd.Connection = conn
            'cmd.CommandType = CommandType.Text
            'strQuery = "SELECT nPatientID FROM Patient where sFirstName='" & mPatient.PatientDemographics.DemographicsDetail.PatientFirstName & "' and sLastName='" & mPatient.PatientDemographics.DemographicsDetail.PatientLastName & "' " _
            '& " and dtDOB='" & mPatient.PatientDemographics.DemographicsDetail.PatientDOB & "' and sGender='" & mPatient.PatientDemographics.DemographicsDetail.PatientGender & "'"
            'cmd.CommandText = strQuery
            'If conn.State = ConnectionState.Closed Then
            '    conn.Open()
            'End If
            'Dim temp As Object = cmd.ExecuteScalar()
            'If Not IsNothing(temp) Then
            '    _PatientID = CType(temp, Int64)
            'End If
            Return _PatientID
        Catch ex As Exception
            Return 0
        Finally
            'If conn.State = ConnectionState.Open Then
            '    conn.Close()
            'End If
        End Try
    End Function
    'Code End- Added by kanchan on 20101020 for CCD Queue


    Private Function GetDataTable(ByVal Sqlquery As String) As DataTable
        Dim dt As New DataTable
        Dim sqldata As SqlDataAdapter = Nothing
        Try
            sqldata = New SqlDataAdapter(Sqlquery, gloLibCCDGeneral.Connectionstring)
            sqldata.Fill(dt)

        Catch ex As Exception
            Return Nothing
        Finally
            If Not IsNothing(sqldata) Then
                sqldata.Dispose()
                sqldata = Nothing
            End If
        End Try
        Return dt
    End Function

#End Region

    Private Sub btnOpenFile_MouseLeave(sender As System.Object, e As System.EventArgs) Handles btnOpenFile.MouseLeave
        btnOpenFile.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btnOpenFile.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub rbExistingPatient_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbExistingPatient.CheckedChanged
        pnlReconciliationType.Visible = True
        Try
            gloPatient.gloPatient.GetWindowTitle(Me, _DashBoardPatientID, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex, gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub rbNewPatient_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbNewPatient.CheckedChanged
        pnlReconciliationType.Visible = False
        Me.Text = "Import CCD-CCR-CDA Files"
    End Sub


#Region "Commented Code"

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblCCDMapping.Click
        'Try
        '    gloLibCCDGeneral.Connectionstring = GetConnectionString()
        '    gloLibCCDGeneral.gloCCDApplicationPath = System.Windows.Forms.Application.StartupPath
        '    Dim ofrm As New frmDataMigMappingForCCD(gstrLoginName, gnLoginID, gnClinicID)
        '    ofrm.WindowState = FormWindowState.Maximized
        '    ofrm.StartPosition = FormStartPosition.CenterScreen
        '    ofrm.BringToFront()
        '    ofrm.ShowDialog(Me)
        'Catch

        'End Try
    End Sub


    Private Sub tblMapping_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblMapping.Click
        'Try
        '    gloLibCCDGeneral.Connectionstring = GetConnectionString()
        '    gloLibCCDGeneral.gloCCDApplicationPath = System.Windows.Forms.Application.StartupPath
        '    Dim ofrm As New frmDataMigMapping(gstrLoginName, gnLoginID, gnClinicID)
        '    ofrm.WindowState = FormWindowState.Maximized
        '    ofrm.StartPosition = FormStartPosition.CenterScreen
        '    ofrm.BringToFront()
        '    ofrm.ShowDialog(Me)
        'Catch

        'End Try
    End Sub

#End Region

    Private Sub tblMedication_ItemClicked(sender As System.Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tblMedication.ItemClicked

    End Sub

    Private Sub tblPreview_Click(sender As System.Object, e As System.EventArgs) Handles tblPreview.Click
        Try

            ' Check Internet Explorer version if it is less than 10 then do not preview CDA file (Bug : 108418 (CCDA Viewer: It is showing Error Message for IE version 8))
            Dim browserVersion As String = Registry.LocalMachine.OpenSubKey("Software\Microsoft\Internet Explorer").GetValue("svcVersion")
            If browserVersion <> "" Then
                Dim strVersion() As String = browserVersion.Split(".")
                If strVersion.Length > 0 Then
                    If Convert.ToInt16(strVersion(0)) < 10 Then
                        MessageBox.Show("The version of internet explorer you are using is older. Please upgrade to version 10 or later.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End If
            End If

            Dim strFilepath As String = txtClinicalPath.Text.Trim()
            Dim _sFileType As String = String.Empty
            If strFilepath = "" Then
                MessageBox.Show("Please select CDA file.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            _sFileType = gloReconciliation.GetFileType(strFilepath)


            If _sFileType.Trim() = "" Then
                MessageBox.Show("Import failed : The file your trying to import is either invalid file or not a recognized cda format. ", gloCCDGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            ''


            Dim oReconcileList As ReconcileList = Nothing

            Label24.BringToFront()
            pnlErrorMessage.Visible = True
            pnlErrorMessage.BringToFront()
            If _sFileType = "CCR" Then
                Dim oCCRReader As gloCCRReader = New gloCCRReader()
                If Not IsNothing(oCCRReader) Then
                    oReconcileList = oCCRReader.ExtractCCR_DemographicsOnly(strFilepath)
                    oCCRReader.Dispose()
                End If
            ElseIf _sFileType = "CCD" Then
                Dim oCCDReader As gloCCDReader = New gloCCDReader()
                If Not IsNothing(oCCDReader) Then
                    oReconcileList = oCCDReader.ExtractCCD_DemographicsOnly(strFilepath)
                    oCCDReader.Dispose()
                End If
            ElseIf _sFileType = "CDA" Then

                Dim oCDAReader As gloCDAReader = New gloCDAReader()
                If Not IsNothing(oCDAReader) Then
                    '' check document confidentiality code
                    If oCDAReader.getDocConfidentiality(strFilepath) = "R" Then
                        '' check Import Restricted CCDA Access for user
                        If gloCCDLibrary.gloLibCCDGeneral.bImportRestrictedCCD = False Then
                            MessageBox.Show("Preview Restricted: You do not have sufficient privileges to view the selected CCDA document." & vbCrLf & "Please contact system administrator to grant the required access.", gloCCDGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.Extract, "Preview Restricted: User do not have sufficient privileges to view the selected CCDA document." & _sFileType, _DashBoardPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                            pnlErrorMessage.Visible = False
                            txtClinicalPath.Text = ""
                            Exit Sub
                        End If
                    End If

                    oCDAReader.gstrCCDAImportCategory = gstrCCDAImportCategory
                    oReconcileList = oCDAReader.ExtractCDA_DemographicsOnly(strFilepath, True)
                    If oCDAReader.ErrorInFile Then
                        txtClinicalPath.Text = ""
                    End If
                    oCDAReader.Dispose()
                End If
            End If
            pnlErrorMessage.Visible = False

            ''

            'Dim objfrm As New frmCCDForm
            'Dim ofile1 As FileInfo = New FileInfo(strFilepath)
            'Dim myXslTransform As New Xml.Xsl.XslTransform()
            'Dim _strfileName As String = DateTime.Now.ToString("yyyyMMddhhmmssffff") & ".html"
            'objfrm.Text = "Preview CDA"
            'myXslTransform.Load(Application.StartupPath & "/gloCCDAcss_MU2.xsl")
            'myXslTransform.Transform(strFilepath, System.IO.Path.Combine(gloSettings.FolderSettings.AppTempFolderPath, _strfileName))
            'objfrm.WebBrowser1.Navigate(System.IO.Path.Combine(gloSettings.FolderSettings.AppTempFolderPath, _strfileName))


            'Me.Focus()
            'objfrm.ShowInTaskbar = False
            'objfrm.ShowDialog(Me)
            'objfrm.Close()
            'objfrm.Dispose()
            'objfrm = Nothing

            ''----------------------------------View CCD File------------------------------------
            Dim myXslTransform As Xml.Xsl.XslTransform = Nothing
            If Not IsNothing(oReconcileList) Then
                Dim oPatientClinicalInfo As New frmPatientClinicalInformation
                Dim oDialogResultClinicalInfo As DialogResult

                Try

                    ''set IsShowSaveButton =false for bugid 108449
                    oPatientClinicalInfo.isPreviewed = True
                    oPatientClinicalInfo.IsShowSaveButton = False
                    oPatientClinicalInfo.EffectiveTime = oReconcileList.FileHeaderDateTime
                    oPatientClinicalInfo.FileType = _sFileType
                    oPatientClinicalInfo.SourceName = oReconcileList.FileHeaderSource
                    If (rbExistingPatient.Checked = True) Then
                        oPatientClinicalInfo.CCDPatient = oReconcileList.mPatient
                    End If


                    'Convert XML file to readatble format by using XSLT' StartupPath changed to tempfilderpath by SLR.
                    'Dim myXslTransform As Xml.Xsl.XslTransform
                    Dim _strfileName As String = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".html", "yyyyMMddhhmmssffff") 'DateTime.Now.ToString("yyyyMMddhhmmssffff") & System.Guid.NewGuid().ToString() & ".html"

                    Select Case _sFileType
                        Case "CCD"
                            oPatientClinicalInfo.CCDXMLFilePath = strFilepath
                            myXslTransform = New Xml.Xsl.XslTransform()
                            myXslTransform.Load(Application.StartupPath & "/gloCCDAcss_MU2.xsl")
                            myXslTransform.Transform(txtClinicalPath.Text, _strfileName) ' System.IO.Path.Combine(Application.StartupPath, _strfileName))
                            oPatientClinicalInfo.WebBrowser1.Navigate(_strfileName) 'System.IO.Path.Combine(Application.StartupPath, _strfileName))
                            If strNonXMLFilePath <> "" Then
                                oPatientClinicalInfo.ShowPanel()
                                oPatientClinicalInfo.ShowPDFPreview(strNonXMLFilePath)
                            End If

                        Case "CDA"
                            ''Dim oCDAReader As gloCDAReader = New gloCDAReader()
                            ''If Not IsNothing(oCDAReader) Then
                            ''    '' check document confidentiality code
                            ''    If oCDAReader.getDocConfidentiality(strFilepath) = "R" Then
                            ''        '' check Import Restricted CCDA Access for user
                            ''        If gloCCDLibrary.gloLibCCDGeneral.bImportRestrictedCCD = False Then
                            ''            MessageBox.Show("Preview Restricted: You do not have sufficient privileges to view the selected CCDA document." & vbCrLf & "Please contact system administrator to grant the required access.", gloCCDGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            ''            Exit Sub
                            ''        End If
                            ''    End If
                            ''End If
                            ''oCDAReader = Nothing
                            oPatientClinicalInfo.CCDXMLFilePath = strFilepath
                            myXslTransform = New Xml.Xsl.XslTransform()
                            myXslTransform.Load(Application.StartupPath & "/gloCCDAcss_MU2.xsl")
                            myXslTransform.Transform(txtClinicalPath.Text, _strfileName)
                            'if CDA validatioin service is not up then file will get open in the old browser
                            If oReconcileList.cdaviewerhtmlFile = "" Then
                                oPatientClinicalInfo.WebBrowser1.Navigate(_strfileName)
                            Else
                                showDataInPreviewControl(oPatientClinicalInfo.WebBrowser1, oReconcileList.cdaviewerhtmlFile)
                            End If

                            'showDataInPreviewControl(oPatientClinicalInfo.WebBrowser1, oReconcileList.cdaviewerhtmlFile)

                            '' File.WriteAllText(_strhtmlfileName, strbuild.Replace(vbCrLf, ""))
                        Case "CCR"
                            oPatientClinicalInfo.CCDXMLFilePath = strFilepath
                            myXslTransform = New Xml.Xsl.XslTransform()
                            'myXslTransform.Load("http://www.glostream.com/css/XSLT/gloccrCss.xsl")
                            myXslTransform.Load(Application.StartupPath & "/gloccrCss.xsl")
                            myXslTransform.Transform(txtClinicalPath.Text, _strfileName) ' System.IO.Path.Combine(Application.StartupPath, _strfileName))
                            oPatientClinicalInfo.WebBrowser1.Navigate(_strfileName) 'System.IO.Path.Combine(Application.StartupPath, _strfileName))
                    End Select
                    ''commented for bugid 108449
                    'If IsNothing(_sFileType) = False AndAlso Not Me.IsInboxForm Then
                    '    oPatientClinicalInfo.IsShowSaveButton = True
                    'ElseIf Me.IsInboxForm Then
                    '    oPatientClinicalInfo.IsShowSaveButton = False
                    'End If

                    If (_sFileType = "CDA") Then
                        oPatientClinicalInfo.WindowState = FormWindowState.Normal
                    Else
                        oPatientClinicalInfo.WindowState = FormWindowState.Normal
                        oPatientClinicalInfo.StartPosition = FormStartPosition.CenterScreen
                    End If


                    oPatientClinicalInfo.BringToFront()

                    oDialogResultClinicalInfo = oPatientClinicalInfo.ShowDialog(IIf(IsNothing(oPatientClinicalInfo.Parent), Me, oPatientClinicalInfo.Parent))
                    oReconcileList.SourceName = oPatientClinicalInfo.SourceName

                Catch ex As Exception


                    If ex.ToString.Contains("Invalid character in the given encoding") = True Then
                        MessageBox.Show("The file selected is an invalid CCDA file. This file cannot be viewed or imported.", gloCCDGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Else
                        MessageBox.Show("Error while viewing the file. " + Environment.NewLine + Environment.NewLine + ex.ToString(), gloCCDGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                    If Not IsNothing(myXslTransform) Then
                        myXslTransform = Nothing
                    End If

                Finally
                    If Not IsNothing(oPatientClinicalInfo) Then
                        oPatientClinicalInfo.Dispose()
                        oPatientClinicalInfo = Nothing
                    End If
                End Try
            End If
            If Not IsNothing(myXslTransform) Then
                myXslTransform = Nothing
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub showDataInPreviewControl(wb As WebBrowser, ByVal xmlfilename As String)
        SendFileToServer(wb, xmlfilename)
    End Sub


    Private Shared Sub SendFileToServer(wb As WebBrowser, ByVal htmlfilecontent As String)
        Try
            Cursor.Current = Cursors.WaitCursor

            Dim _strhtmlfileName As String = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".html", "yyyyMMddhhmmssffff")

            Dim strcontent As String = htmlfilecontent.ToString().Replace(vbNewLine, "").Replace("\r", "").Replace("\n", "").Replace(vbLf, "").Replace(vbCr, "").Replace(vbCrLf, "")

            File.WriteAllText(_strhtmlfileName, strcontent)

            Try




                wb.Navigate(_strhtmlfileName)


            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)

            Finally


                Cursor.Current = Cursors.Default
            End Try
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)

        End Try
    End Sub




End Class
