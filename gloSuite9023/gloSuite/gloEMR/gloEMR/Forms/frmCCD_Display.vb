Imports gloCCDLibrary
Imports System.IO
Imports System.Data.SqlClient
Imports System.Xml
Imports gloGlobal

Public Class frmCCD_Display
    Private _PatientID As Int64
    Private mPatient As gloCCDLibrary.Patient
    Private mCCDFilename As String = ""
    Private nTaskType As Int32
    Private nCCDId As Int64
    Private CCDFilePath As String
    Private mEffectiveTime As String
    Private sFileType As String = "" 'Added by kanchan on 20100611 for CCR/CCD
    Dim Source As String
    Dim ogloCCDInterface As gloCCDInterface
    Dim ogloCCRInterface As gloCCR_Interface 'Added by kanchan on 20100611 for CCR/CCD
    Dim nProviderID As Int64 = 0
    'Dim _strfileName As String = ""
    Private WithEvents ouctlClinicalInfo As gloCCDLibrary.uctl_ClinicalInformation = Nothing
    ' Dim NewDocumentName As String = ""
    Dim oPDFView As pdftron.PDF.PDFViewCtrl
    Dim oPDFDoc As pdftron.PDF.PDFDoc
    ' Dim nPageNo As Byte
    Private _IsInboxForm As Boolean = False
    Dim strNonXMLFilePath As String = ""
    Public Property IsInboxForm() As Boolean
        Get
            Return _IsInboxForm
        End Get
        Set(ByVal value As Boolean)
            _IsInboxForm = value
        End Set
    End Property
    Public Property TaskType() As Int32
        Get
            Return nTaskType
        End Get
        Set(ByVal value As Int32)
            nTaskType = value
        End Set
    End Property
    Public Property CCDId() As Int64
        Get
            Return nCCDId
        End Get
        Set(ByVal value As Int64)
            nCCDId = value
        End Set
    End Property
    Private _DMSPatientID As Int64
    Public Property DMSPatientID() As Int64
        Get
            Return _DMSPatientID
        End Get
        Set(ByVal value As Int64)
            _DMSPatientID = value
        End Set
    End Property
    Public Property EffectiveTime() As String
        Get
            Return mEffectiveTime
        End Get
        Set(ByVal value As String)
            mEffectiveTime = value
        End Set
    End Property

    Private Sub frmCCD_Display_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If Not IsNothing(ouctlClinicalInfo) Then
            ouctlClinicalInfo.Dispose()
            ouctlClinicalInfo = Nothing
        End If
        If Not IsNothing(ogloCCDInterface) Then
            ogloCCDInterface.Dispose()
            ogloCCDInterface = Nothing
        End If
        If Not IsNothing(ogloCCRInterface) Then
            ogloCCRInterface.Dispose()
            ogloCCRInterface = Nothing
        End If
        If Not IsNothing(mPatient) Then
            mPatient.Dispose()
            mPatient = Nothing
        End If
    End Sub

    Private Sub frmCCD_Display_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing


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
           
        
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Clinical Information", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmCCD_Display_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim dtFile As DataTable
        CCDFilePath = RetrieveDocumentFile(nCCDId)
        If (IsNothing(CCDFilePath) = False) Then
            If File.Exists(CCDFilePath) = False Then
                MessageBox.Show("Can not Show Clinical Document,File Does not Exist", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        Else
            MessageBox.Show("Can not Show Clinical Document,File Does not Exist", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        dtFile = getFileType(nCCDId)
        If Not IsNothing(dtFile) Then
            If dtFile.Rows.Count > 0 Then
                sFileType = Convert.ToString(dtFile.Rows(0)(0))
                Source = Convert.ToString(dtFile.Rows(0)(1))
            End If
            dtFile.Dispose()
            dtFile = Nothing
        End If

        gloLibCCDGeneral.Connectionstring = GetConnectionString()
        gloLibCCDGeneral.CCDFilePath = CCDFilePath
        gloLibCCDGeneral.gloCCDApplicationPath = System.Windows.Forms.Application.StartupPath
        If (IsNothing(ogloCCDInterface) = False) Then
            ogloCCDInterface.Dispose()
            ogloCCDInterface = Nothing
        End If


        ogloCCDInterface = New gloCCDInterface
        'Code Start- Added by kanchan on 20100611 for CCR/CCD
        If (IsNothing(ogloCCRInterface) = False) Then
            ogloCCRInterface.Dispose()
            ogloCCRInterface = Nothing
        End If
        ogloCCRInterface = New gloCCR_Interface
        ogloCCRInterface.FileType = sFileType
        ogloCCRInterface.UserID = gnLoginID
        Dim ClinicName As String = ""

        If sFileType = "CCR" Then
            'ogloCCRInterface.CCRVersion = "1.0"
            'mPatient = ogloCCRInterface.ExtractClinicalInformation()
            'ogloCCRInterface = New gloCCR_Interface
            'ClinicName = ogloCCRInterface.ReadClinicNameCCR()
            'mEffectiveTime = ogloCCRInterface.ReadCreatedDateCCR()
            Dim oCCRReader As gloCCRReader = New gloCCRReader()
            Dim _oCCDPatient As ReconcileList = Nothing
            _oCCDPatient = oCCRReader.ExtractCCR(gloLibCCDGeneral.CCDFilePath)
            mPatient = _oCCDPatient.mPatient
            ClinicName = Source
            mEffectiveTime = _oCCDPatient.FileHeaderDateTime
            _oCCDPatient.Dispose()
            _oCCDPatient = Nothing
            oCCRReader.Dispose()
            oCCRReader = Nothing
        ElseIf sFileType = "CDA" Then
            Dim oCDAReader As gloCDAReader = New gloCDAReader()
            Dim _oCCDPatient As ReconcileList = Nothing
            _oCCDPatient = oCDAReader.ExtractCDA(gloLibCCDGeneral.CCDFilePath)
            mPatient = _oCCDPatient.mPatient
            ClinicName = Source
            mEffectiveTime = _oCCDPatient.FileHeaderDateTime
            _oCCDPatient.Dispose()
            _oCCDPatient = Nothing
            oCDAReader.Dispose()
            oCDAReader = Nothing
        Else
            'ogloCCRInterface.CCRVersion = "2.5"
            'mPatient = ogloCCRInterface.ExtractClinicalInformation()
            'ogloCCDInterface = New gloCCDInterface
            'ClinicName = ogloCCDInterface.ReadClinicNameCCD()
            'mEffectiveTime = ogloCCDInterface.ReadCreatedDateCCD()
            Dim oCCDReader As gloCCDReader = New gloCCDReader()
            Dim _oCCDPatient As ReconcileList = Nothing
            _oCCDPatient = oCCDReader.ExtractCCD(gloLibCCDGeneral.CCDFilePath)
            mPatient = _oCCDPatient.mPatient
            ClinicName = Source
            mEffectiveTime = _oCCDPatient.FileHeaderDateTime
            _oCCDPatient.Dispose()
            _oCCDPatient = Nothing
            oCCDReader.Dispose()
            oCCDReader = Nothing
        End If
        If _IsInboxForm = True Then


            If sFileType = "CDA" Or sFileType = "CCD" Then
                Dim oCDAReader As gloCDAReader = New gloCDAReader()
                Dim mediatype As String = ""
                strNonXMLFilePath = oCDAReader.IsExistsCCDANonXMLBody(gloLibCCDGeneral.CCDFilePath, mediatype)
                If mediatype = "image/gif" Or mediatype = "image/tiff" Or mediatype = "image/jpeg" Or mediatype = "image/png" Then
                    Dim oDocManager As New gloEDocumentV3.eDocManager.eDocManager()
                    Dim osourcedocuments As New ArrayList
                    osourcedocuments.Add(strNonXMLFilePath)

                    Dim _strfileName As String = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".pdf", "MMddyyyyHHmmssffff") 'gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".pdf", "MM dd yyyy hh mm ss tt")
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
                ShowPDFPreview(strNonXMLFilePath)
            End If

        End If
        ogloCCRInterface.SourceName = ClinicName
        txtSource.Text = ClinicName

        'Code End- Added by kanchan on 20100611 for CCR/CCD
        ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Select, "Imported CCD data.", gloAuditTrail.ActivityOutCome.Success)

        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Select, "Imported CCD data.", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
        ''
        If nTaskType = gloTaskMail.TaskType.CCDUnmatchedPatient Then
            tls_RegisterPatient.Visible = True
            tls_DiscardCCD.Visible = True
            tlsRegisterDemo.Visible = True
            txtSource.Enabled = True
            'tlsSave.Visible = False
            'tlsClose.Visible = False
        Else
            'ShowCCD_PatientClinicalInfo()
            tls_RegisterPatient.Visible = False
            tls_DiscardCCD.Visible = False
            tlsRegisterDemo.Visible = False
            txtSource.Enabled = False
            'tlsSave.Visible = True
            'tlsClose.Visible = True
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex, gloAuditTrail.ActivityOutCome.Failure)
            End Try
        End If
        LoadBrowserData()

    End Sub

    Private Function LoadBrowserData()
        Try
            If CCDFilePath <> "" Then
                ' Added transformation
                Dim myXslTransform As Xsl.XslTransform
                Dim _strfileName As String = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".html", "yyyyMMddhhmmssffff") 'DateTime.Now.ToString("yyyyMMddhhmmssffff") & System.Guid.NewGuid().ToString() & ".html"


                If sFileType = "CCR" Then
                    myXslTransform = New Xml.Xsl.XslTransform()
                    'myXslTransform.Load("http://www.glostream.com/css/XSLT/gloccrCss.xsl")
                    myXslTransform.Load(Application.StartupPath & "/gloccrCss.xsl")
                    myXslTransform.Transform(CCDFilePath, _strfileName) 'System.IO.Path.Combine(gloSettings.FolderSettings.AppTempFolderPath, _strfileName))
                    WebBrowser1.Navigate(_strfileName) 'System.IO.Path.Combine(gloSettings.FolderSettings.AppTempFolderPath, _strfileName))

                ElseIf sFileType = "CCD" Or sFileType = "CDA" Then
                    myXslTransform = New Xml.Xsl.XslTransform()
                    'myXslTransform.Load("http://www.glostream.com/css/XSLT/gloccdCss.xsl")
                    myXslTransform.Load(Application.StartupPath & "/gloCCDAcss_MU2.xsl")
                    myXslTransform.Transform(CCDFilePath, _strfileName) 'System.IO.Path.Combine(gloSettings.FolderSettings.AppTempFolderPath, _strfileName))
                    WebBrowser1.Navigate(_strfileName) 'System.IO.Path.Combine(gloSettings.FolderSettings.AppTempFolderPath, _strfileName))
                Else
                    WebBrowser1.Navigate(CCDFilePath)
                End If

                'WebBrowser1.Navigate(CCDFilePath)

                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.View, "Viewed CCD", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20100916
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewCCD, gloAuditTrail.ActivityType.View, "Viewed CCD", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                ''
            End If
            If mCCDFilename = "" Then
                If Not IsNothing(mPatient) Then
                    If (IsNothing(ouctlClinicalInfo) = False) Then
                        Me.pnlClinicalInfo.Controls.Remove(ouctlClinicalInfo)
                        ouctlClinicalInfo.Dispose()
                        ouctlClinicalInfo = Nothing
                    End If
                    ouctlClinicalInfo = New gloCCDLibrary.uctl_ClinicalInformation(mPatient, EffectiveTime, sFileType)
                    Me.pnlClinicalInfo.Controls.Add(ouctlClinicalInfo)
                    ouctlClinicalInfo.Visible = False
                    ouctlClinicalInfo.Dock = DockStyle.Fill
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.View, "Viewed CCD", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20100916
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewCCD, gloAuditTrail.ActivityType.View, "Viewed CCD", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                    ''
                    'tlsDisclosureSet.Items(0).Visible = True
                End If
            End If
        Catch ex As gloCCDException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.View, ex, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gloCCDGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.View, "Load CCD Failed", gloAuditTrail.ActivityOutCome.Failure)
            ''Added Rahul P on 20100916
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.View, "Load CCD Failed", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Failure)
            ''
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.View, ex, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gloCCDGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.View, "Load CCD Failed", gloAuditTrail.ActivityOutCome.Failure)
            ''Added Rahul P on 20100916
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.View, "Load CCD Failed", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Failure)
            ''
        End Try
        Return Nothing
    End Function

    Public Function RetrieveDocumentFile(ByVal nCCDId As Int64) As String
        Dim oResult As Object = Nothing
        Dim strFileName As String = ""
        Dim sqlParam As SqlParameter
        Dim cmd As SqlCommand = Nothing
        Dim conn As New SqlConnection(GetConnectionString)

        Try
            cmd = New SqlCommand("CCD_RetrieveXMLFile", conn)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = cmd.Parameters.Add("@nCCDID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nCCDId
            conn.Open()
            oResult = cmd.ExecuteScalar()

            If oResult Is Nothing Then
                Return ""
            End If

            If IsDBNull(oResult) = False Then
                strFileName = ExamNewFaxFileName(gloSettings.FolderSettings.AppTempFolderPath, ".xml")
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
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Dispose()
            conn = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing
        End Try

    End Function

    Public Function Retrieve_rtf_File(ByVal nCCDId As Int64) As String
        Dim oResult As Object = Nothing
        Dim strFileName As String = ""
        Dim sqlParam As SqlParameter
        Dim cmd As SqlCommand = Nothing
        Dim conn As New SqlConnection(GetConnectionString)

        Try
            cmd = New SqlCommand("CCD_RetrieveXMLFile", conn)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = cmd.Parameters.Add("@nCCDID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nCCDId
            conn.Open()
            oResult = cmd.ExecuteScalar()

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
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Dispose()
            conn = Nothing
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

    Private Function ShowCCD_PatientClinicalInfo()
        Dim ogloCCDInterface As gloCCDInterface
        ogloCCDInterface = New gloCCDInterface
        Dim oPatientClinicalInfo As New frmPatientClinicalInformation
        oPatientClinicalInfo.EffectiveTime = ogloCCDInterface.EffectiveTime
        ogloCCDInterface.Dispose()
        ogloCCDInterface = Nothing
        oPatientClinicalInfo.CCDPatient = mPatient
        Dim _PatientId As Int64 = getPatientID(nCCDId)
        mPatient.PatientName.ID = _PatientId
        oPatientClinicalInfo.CCDXMLFilePath = CCDFilePath
        oPatientClinicalInfo.WebBrowser1.Navigate(CCDFilePath)
        oPatientClinicalInfo.WindowState = FormWindowState.Normal
        oPatientClinicalInfo.StartPosition = FormStartPosition.CenterScreen
        oPatientClinicalInfo.BringToFront()
        oPatientClinicalInfo.ShowDialog(IIf(IsNothing(oPatientClinicalInfo.Parent), Me, oPatientClinicalInfo.Parent))
        oPatientClinicalInfo.Dispose()
        Return Nothing
    End Function

    'Code Start- Added by kanchan on 20100604 for CCD Queue
    Private Function getPatientID(ByVal _nCCDID As Int64) As Int64
        Dim cmd As New SqlCommand
        Dim conn As SqlConnection = Nothing
        Dim strQuery As String = ""
        Dim _PatientID As Int64 = 0
        Try
            conn = New SqlConnection(gloLibCCDGeneral.Connectionstring)

            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            strQuery = "SELECT nPatientID FROM CCD_Queue WHERE nCCDID=" & _nCCDID
            cmd.CommandText = strQuery
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Dim temp As Object = cmd.ExecuteScalar()
            If Not IsNothing(temp) Then
                _PatientID = CType(temp, Int64)
            End If
            Return _PatientID
        Catch ex As Exception
            Return 0
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Dispose()
            conn = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function

    'Code Start- Added by kanchan on 20100604 for CCD Queue
    Private Function UpdateCCDQueue(ByVal _nCCDID As Int64) As Boolean
        Dim cmd As New SqlCommand
        Dim conn As SqlConnection = Nothing
        Dim strQuery As String = ""
        Dim _PatientID As Int64 = 0
        Try
            conn = New SqlConnection(gloLibCCDGeneral.Connectionstring)

            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            strQuery = "UPDATE CCD_Queue SET sStatus='Processed' WHERE nCCDID=" & _nCCDID
            cmd.CommandText = strQuery
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            cmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Dispose()
            conn = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function

    'Code Start- Added by kanchan on 20100604 for CCD Queue
    Private Function Update_taskStatus(ByVal _nCCDID As Int64) As Boolean
        Dim cmd As New SqlCommand
        Dim conn As SqlConnection = Nothing
        Dim strQuery As String = ""
        Dim _TaskID As Int64 = 0
        Try
            _TaskID = getTaskID(_nCCDID)
            conn = New SqlConnection(gloLibCCDGeneral.Connectionstring)

            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            Dim statusID As Int32 = 0
            statusID = gloTaskMail.frmTask.StatusType.Completed
            strQuery = "UPDATE TM_Task_Progress SET dComplete='100',nStatusID = " & statusID & " where nTaskID=" & _TaskID
            cmd.CommandText = strQuery
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            cmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Dispose()
            conn = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function

    'Code Start- Added by kanchan on 20100604 for CCD Queue
    Private Function getTaskID(ByVal _nCCDID As Int64) As Int64
        Dim cmd As New SqlCommand
        Dim conn As SqlConnection = Nothing
        Dim strQuery As String = ""
        Dim _TaskID As Int64 = 0
        Try
            conn = New SqlConnection(gloLibCCDGeneral.Connectionstring)

            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            strQuery = "SELECT nTaskId FROM CCD_Queue WHERE nCCDID=" & _nCCDID
            cmd.CommandText = strQuery
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Dim temp As Object = cmd.ExecuteScalar()
            If Not IsNothing(temp) Then
                _TaskID = CType(temp, Int64)
            End If
            Return _TaskID
        Catch ex As Exception
            Return 0
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Dispose()
            conn = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function


    Private Sub tls_DiscardCCD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tls_DiscardCCD.Click
        Try
            If UpdateCCDQueue(nCCDId) = True Then
                Update_taskStatus(nCCDId)
            End If
            Me.Close()
            Me.DialogResult = DialogResult.OK
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Select, "Could not Discard CCD file.", gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Could not Discard Clinical Document file - " & sFileType & ":" & ex.ToString, gloCCDGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    'Code Start- Added by kanchan on 20100611 to check Patient Exist or not
    Private Function IsPatientExists(ByVal objPatient As gloCCDLibrary.Patient) As Int64
        Dim cmd As New SqlCommand
        Dim conn As SqlConnection = Nothing
        Dim strQuery As String = ""
        Dim _PatientID As Int64 = 0
        Try
            conn = New SqlConnection(gloLibCCDGeneral.Connectionstring)

            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            strQuery = "SELECT nPatientID FROM Patient where sFirstName='" & mPatient.PatientDemographics.DemographicsDetail.PatientFirstName & "' and sLastName='" & mPatient.PatientDemographics.DemographicsDetail.PatientLastName & "' " _
            & " and dtDOB='" & mPatient.PatientDemographics.DemographicsDetail.PatientDOB & "' and sGender='" & mPatient.PatientDemographics.DemographicsDetail.PatientGender & "'"
            cmd.CommandText = strQuery
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Dim temp As Object = cmd.ExecuteScalar()
            If Not IsNothing(temp) Then
                _PatientID = CType(temp, Int64)
            End If
            Return _PatientID
        Catch ex As Exception
            Return 0
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Dispose()
            conn = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function
    'Code Start- Added by kanchan on 20100607 for Registration of CCD Patient
    Private Function RegisterNew_Patient(ByVal oPatient As gloCCDLibrary.Patient) As Int64
        Dim _PatientID As Int64
        Dim conn As SqlConnection = Nothing
        Dim _CCDID As Int64 = 0
        Dim _Language As String = ""
        Dim cmd As SqlCommand = Nothing
        Dim sqlparam As SqlParameter
        Try


            conn = New SqlConnection(gloLibCCDGeneral.Connectionstring)

            Dim strQuery As String = ""

            cmd = New SqlCommand("gsp_CCDInUpPatient", conn)
            cmd.CommandType = CommandType.StoredProcedure

            sqlparam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            sqlparam.Direction = ParameterDirection.InputOutput
            sqlparam.Value = 0
            sqlparam = cmd.Parameters.Add("@sPatientCode", SqlDbType.VarChar, 50)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(oPatient.PatientName.Code) Then
                sqlparam.Value = oPatient.PatientName.Code
            Else
                sqlparam.Value = ""
            End If
            sqlparam = cmd.Parameters.Add("@sFirstName", SqlDbType.VarChar, 100)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(oPatient.PatientName.FirstName) Then
                sqlparam.Value = oPatient.PatientName.FirstName
            Else
                MessageBox.Show("Patient Could not be register,Missing Patient First Name", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return 0
            End If
            sqlparam = cmd.Parameters.Add("@sMiddleName", SqlDbType.VarChar, 50)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(oPatient.PatientName.MiddleName) Then
                sqlparam.Value = oPatient.PatientName.MiddleName
            Else
                sqlparam.Value = ""
            End If
            sqlparam = cmd.Parameters.Add("@sLastName", SqlDbType.VarChar, 100)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(oPatient.PatientName.LastName) Then
                sqlparam.Value = oPatient.PatientName.LastName
            Else
                MessageBox.Show("Patient Could not be register,Missing Patient Lat Name", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return 0
            End If

            sqlparam = cmd.Parameters.Add("@nSSN", SqlDbType.VarChar, 10)
            sqlparam.Direction = ParameterDirection.Input
            sqlparam.Value = ""
            sqlparam = cmd.Parameters.Add("@dtDOB", SqlDbType.DateTime)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(oPatient.DateofBirth) Then
                sqlparam.Value = oPatient.DateofBirth
            Else
                MessageBox.Show("Patient Could not be register,Missing Patient Date of Birth", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return 0
            End If
            sqlparam = cmd.Parameters.Add("@sGender", SqlDbType.VarChar, 10)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(oPatient.Gender) Then
                sqlparam.Value = oPatient.Gender
            Else
                MessageBox.Show("Patient Could not be register,Missing Patient Gender", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return 0
            End If
            sqlparam = cmd.Parameters.Add("@sMaritalStatus", SqlDbType.VarChar, 10)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(oPatient.MaritalStatus) Then
                sqlparam.Value = oPatient.MaritalStatus
            Else
                sqlparam.Value = ""
            End If
            sqlparam = cmd.Parameters.Add("@sAddressLine1", SqlDbType.VarChar, 255)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(oPatient.PatientName.PersonContactAddress.Street) Then
                sqlparam.Value = oPatient.PatientName.PersonContactAddress.Street
            Else
                sqlparam.Value = ""
            End If
            'Code Start-Added by kanchan on 20100709 for Modular CCD Rendering & save
            sqlparam = cmd.Parameters.Add("@sAddressLine2", SqlDbType.VarChar, 255)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(oPatient.PatientName.PersonContactAddress.AddressLine2) Then
                sqlparam.Value = oPatient.PatientName.PersonContactAddress.AddressLine2
            Else
                sqlparam.Value = ""
            End If
            'Code End-Added by kanchan on 20100709 for Modular CCD Rendering & save
            sqlparam = cmd.Parameters.Add("@sCity", SqlDbType.VarChar, 255)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(oPatient.PatientName.PersonContactAddress.City) Then
                sqlparam.Value = oPatient.PatientName.PersonContactAddress.City
            Else
                sqlparam.Value = ""
            End If
            sqlparam = cmd.Parameters.Add("@sState", SqlDbType.VarChar, 50)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(oPatient.PatientName.PersonContactAddress.State) Then
                sqlparam.Value = oPatient.PatientName.PersonContactAddress.State
            Else
                sqlparam.Value = ""
            End If
            sqlparam = cmd.Parameters.Add("@sZip", SqlDbType.VarChar, 50)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(oPatient.PatientName.PersonContactAddress.Zip) Then
                sqlparam.Value = oPatient.PatientName.PersonContactAddress.Zip
            Else
                sqlparam.Value = ""
            End If
            sqlparam = cmd.Parameters.Add("@sCountry", SqlDbType.VarChar, 50)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(oPatient.PatientName.PersonContactAddress.Country) Then
                sqlparam.Value = oPatient.PatientName.PersonContactAddress.Country
            Else
                sqlparam.Value = ""
            End If
            sqlparam = cmd.Parameters.Add("@sPhone", SqlDbType.VarChar, 50)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(oPatient.PatientName.PersonContactPhone.Phone) Then
                sqlparam.Value = oPatient.PatientName.PersonContactPhone.Phone
            Else
                sqlparam.Value = ""
            End If

            sqlparam = cmd.Parameters.Add("@sMobile", SqlDbType.VarChar, 50)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(oPatient.PatientName.PersonContactPhone.Mobile) Then
                sqlparam.Value = oPatient.PatientName.PersonContactPhone.Mobile
            Else
                sqlparam.Value = ""
            End If
            sqlparam = cmd.Parameters.Add("@sEmail", SqlDbType.VarChar, 50)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(oPatient.PatientName.PersonContactPhone.Email) Then
                sqlparam.Value = oPatient.PatientName.PersonContactPhone.Email
            Else
                sqlparam.Value = ""
            End If
            sqlparam = cmd.Parameters.Add("@sWorkPhone", SqlDbType.VarChar, 50)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(oPatient.PatientName.PersonContactPhone.WorkPhone) Then
                sqlparam.Value = oPatient.PatientName.PersonContactPhone.WorkPhone
            Else
                sqlparam.Value = ""
            End If
            sqlparam = cmd.Parameters.Add("@sEmergencyPhone", SqlDbType.VarChar, 50)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(oPatient.PatientName.PersonContactPhone.VacationPhone) Then
                sqlparam.Value = oPatient.PatientName.PersonContactPhone.VacationPhone
            Else
                sqlparam.Value = ""
            End If
            sqlparam = cmd.Parameters.Add("@sRace", SqlDbType.VarChar, 50)
            sqlparam.Direction = ParameterDirection.Input
            'Code Start-Added by kanchan on 20100709 for Modular CCD Rendering & save
            'If Not IsNothing(oPatient.RaceCode) Then
            '    sqlparam.Value = oPatient.RaceCode
            If Not IsNothing(oPatient.Race) Then
                sqlparam.Value = oPatient.Race
            Else
                sqlparam.Value = ""
            End If
            sqlparam = cmd.Parameters.Add("@sLang", SqlDbType.VarChar, 50)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(oPatient.PatientLanguages) Then
                If oPatient.PatientLanguages.Count > 0 Then
                    If Not IsNothing(oPatient.PatientLanguages.Item(0).Language) Then
                        sqlparam.Value = oPatient.PatientLanguages.Item(0).Language
                        _Language = sqlparam.Value
                    Else
                        sqlparam.Value = ""
                    End If
                Else
                    sqlparam.Value = ""
                End If
            Else
                sqlparam.Value = ""
            End If
            sqlparam = cmd.Parameters.Add("@sEthn", SqlDbType.VarChar, 50)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(oPatient.ethnicGroupCode) Then
                sqlparam.Value = oPatient.ethnicGroupCode
            Else
                sqlparam.Value = ""
            End If

            'Code Start-Added by kanchan on 20100709 for Modular CCD Rendering & save
            sqlparam = cmd.Parameters.Add("@sLocation", SqlDbType.VarChar, 50)
            sqlparam.Direction = ParameterDirection.Input
            Dim _sLocation As String = getDefaultLocation()
            If Not IsNothing(_sLocation) Then
                sqlparam.Value = _sLocation
            Else
                sqlparam.Value = ""
            End If
            'Code End-Added by kanchan on 20100709 for Modular CCD Rendering & save

            sqlparam = cmd.Parameters.Add("@nProviderID", SqlDbType.VarChar, 50)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(nProviderID) Then
                sqlparam.Value = nProviderID
            Else
                sqlparam.Value = 0
            End If

            'Code Start-Added by kanchan on 20100712 for Modular CCD Rendering & save
            sqlparam = cmd.Parameters.Add("@sGuardian_fName", SqlDbType.VarChar, 50)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(oPatient.Guardian_fName) Then
                sqlparam.Value = oPatient.Guardian_fName
            Else
                sqlparam.Value = ""
            End If
            sqlparam = cmd.Parameters.Add("@sGuardian_mName", SqlDbType.VarChar, 50)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(oPatient.Guardian_mName) Then
                sqlparam.Value = oPatient.Guardian_mName
            Else
                sqlparam.Value = ""
            End If
            sqlparam = cmd.Parameters.Add("@sGuardian_lName", SqlDbType.VarChar, 50)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(oPatient.Guardian_lName) Then
                sqlparam.Value = oPatient.Guardian_lName
            Else
                sqlparam.Value = ""
            End If
            sqlparam = cmd.Parameters.Add("@sGuardian_Address1", SqlDbType.VarChar, 255)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(oPatient.Guardian_Address1) Then
                sqlparam.Value = oPatient.Guardian_Address1
            Else
                sqlparam.Value = ""
            End If
            sqlparam = cmd.Parameters.Add("@sGuardian_Address2", SqlDbType.VarChar, 255)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(oPatient.Guardian_Address2) Then
                sqlparam.Value = oPatient.Guardian_Address2
            Else
                sqlparam.Value = ""
            End If
            sqlparam = cmd.Parameters.Add("@sGuardian_City", SqlDbType.VarChar, 255)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(oPatient.Guardian_City) Then
                sqlparam.Value = oPatient.Guardian_City
            Else
                sqlparam.Value = ""
            End If
            sqlparam = cmd.Parameters.Add("@sGuardian_State", SqlDbType.VarChar, 50)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(oPatient.Guardian_State) Then
                sqlparam.Value = oPatient.Guardian_State
            Else
                sqlparam.Value = ""
            End If
            sqlparam = cmd.Parameters.Add("@sGuardian_Country", SqlDbType.VarChar, 50)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(oPatient.Guardian_Country) Then
                sqlparam.Value = oPatient.Guardian_Country
            Else
                sqlparam.Value = ""
            End If
            sqlparam = cmd.Parameters.Add("@sGuardian_ZIP", SqlDbType.VarChar, 50)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(oPatient.Guardian_ZIP) Then
                sqlparam.Value = oPatient.Guardian_ZIP
            Else
                sqlparam.Value = ""
            End If
            sqlparam = cmd.Parameters.Add("@sGuardian_Phone", SqlDbType.VarChar, 50)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(oPatient.Guardian_Phone) Then
                sqlparam.Value = oPatient.Guardian_Phone
            Else
                sqlparam.Value = ""
            End If
            sqlparam = cmd.Parameters.Add("@sGuardian_Mobile", SqlDbType.VarChar, 50)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(oPatient.Guardian_Mobile) Then
                sqlparam.Value = oPatient.Guardian_Mobile
            Else
                sqlparam.Value = ""
            End If
            sqlparam = cmd.Parameters.Add("@sGuardian_Email", SqlDbType.VarChar, 50)
            sqlparam.Direction = ParameterDirection.Input
            If Not IsNothing(oPatient.Guardian_Email) Then
                sqlparam.Value = oPatient.Guardian_Email
            Else
                sqlparam.Value = ""
            End If
            'Code End-Added by kanchan on 20100712 for Modular CCD Rendering & save

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            cmd.ExecuteNonQuery()

            If Not IsNothing(cmd.Parameters("@nPatientID").Value) Then
                _PatientID = CType(cmd.Parameters("@nPatientID").Value, Int64)
            End If

            'Code Start-Added by kanchan on 20100709 for Modular CCD Rendering & save
            'Add Race,Ethnicity & Language in Category_Mst,if not exists
            If mPatient.Race <> "" Then
                UpdateCategoryMaster(mPatient.Race, "Race")
            End If
            If mPatient.ethnicGroupCode <> "" Then
                UpdateCategoryMaster(mPatient.ethnicGroupCode, "Ethnicity")
            End If
            If _Language <> "" Then
                UpdateCategoryMaster(_Language, "Language")
            End If

            'Code End-Added by kanchan on 20100709 for Modular CCD Rendering & save

            Return _PatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Select, "Patient not get registered.", gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Patient not get registered" & ex.ToString, gloCCDGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Dispose()
            conn = Nothing

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlparam = Nothing
        End Try

    End Function

    Private Sub tlsSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsSave.Click
        Try
            Dim _PatientId As Int64 = getPatientID(nCCDId)
            mPatient.PatientName.ID = _PatientId
            ouctlClinicalInfo.SaveCCD(CCDFilePath, gnLoginID, txtSource.Text, mEffectiveTime, strNonXMLFilePath)

            If UpdateCCDQueue(nCCDId) = True Then
                Update_taskStatus(nCCDId)
            End If
            MessageBox.Show("CCD File saved successfully ", gloCCDGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Select, "Could not Save CCD data.", gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Could not save CCD File" & ex.ToString, gloCCDGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tlsClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsClose.Click

        Me.Close()

    End Sub

    'Code Start- Added by kanchan on 20100611 for CCD/CCR
    Private Function getFileType(ByVal _nCCDID As Int64) As DataTable
        Dim cmd As New SqlCommand
        Dim conn As SqlConnection = Nothing
        Dim strQuery As String = ""
        Dim _FileType As String = ""
        Dim _da As SqlDataAdapter = Nothing
        Dim dt As New DataTable
        Try
            conn = New SqlConnection(gloLibCCDGeneral.Connectionstring)

            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            strQuery = "SELECT sFileType,sSource FROM CCD_Queue WHERE nCCDID=" & _nCCDID
            cmd.CommandText = strQuery
            _da = New SqlDataAdapter(cmd)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            _da.Fill(dt)

            Return dt
        Catch ex As Exception
            Return Nothing
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Dispose()
            conn = Nothing
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(_da) Then
                _da.Dispose()
                _da = Nothing
            End If
        End Try
    End Function

    'Code Start- Added by kanchan on 20100611 for CCD/CCR , to get default provider id
    Private Function getDefaultProviderId() As Int64
        Dim cmd As New SqlCommand
        Dim conn As SqlConnection = Nothing
        Dim strQuery As String = ""
        Dim _Provider As Int64 = 0
        Try
            conn = New SqlConnection(gloLibCCDGeneral.Connectionstring)

            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            strQuery = "select sSettingsValue from settings where sSettingsName like 'PatientDefaultProvider'"
            cmd.CommandText = strQuery
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Dim temp As Object = cmd.ExecuteScalar()
            If Not IsNothing(temp) Then
                _Provider = CType(temp, Int64)
                If _Provider = 0 Then
                    strQuery = "SELECT TOP 1 nProviderID FROM Provider_mst WHERE ISNULL(bIsBlocked, 0) = 0"
                    cmd.CommandText = strQuery
                    Dim temp1 As Object = cmd.ExecuteScalar()
                    If Not IsNothing(temp1) Then
                        _Provider = CType(temp1, Int64)
                    End If
                End If
            End If
            Return _Provider
        Catch ex As Exception
            Return 0
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Dispose()
            conn = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function


    'Code Start-Added by kanchan on 20100709 for Modular CCD Rendering & save
    Private Function UpdateCategoryMaster(ByVal _CategoryDesc As String, ByVal _Category As String) As Boolean
        Dim cmd As SqlCommand = Nothing
        Dim conn As SqlConnection = Nothing
        Dim sqlparam As SqlParameter
        Try
            conn = New SqlConnection(gloLibCCDGeneral.Connectionstring)
            cmd = New SqlCommand("gsp_CCDINUP_CategoryMST", conn)
            cmd.CommandType = CommandType.StoredProcedure

            sqlparam = cmd.Parameters.Add("@CategoryID", SqlDbType.BigInt)
            sqlparam.Direction = ParameterDirection.InputOutput
            sqlparam.Value = 0

            sqlparam = cmd.Parameters.Add("@CategoryDescription", SqlDbType.VarChar, 50)
            sqlparam.Direction = ParameterDirection.Input
            sqlparam.Value = _CategoryDesc

            sqlparam = cmd.Parameters.Add("@CategoryType", SqlDbType.VarChar, 50)
            sqlparam.Direction = ParameterDirection.Input
            sqlparam.Value = _Category

            sqlparam = cmd.Parameters.Add("@ClinicID", SqlDbType.BigInt)
            sqlparam.Direction = ParameterDirection.Input
            sqlparam.Value = mdlGeneral.gClinicID

            sqlparam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
            sqlparam.Direction = ParameterDirection.Input
            sqlparam.Value = mdlGeneral.GetPrefixTransactionID()

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            cmd.ExecuteNonQuery()

            Return True
        Catch ex As Exception
            Return False
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Select, "Patient not get registered.", gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Patient not get registered" & ex.ToString, gloCCDGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Dispose()
            conn = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlparam = Nothing
        End Try
    End Function
    'Code End-Added by kanchan on 20100709 for Modular CCD Rendering & save

    'Code Start-Added by kanchan on 20100709 for Modular CCD Rendering & save
    Private Function getDefaultLocation() As String
        Dim cmd As New SqlCommand
        Dim conn As SqlConnection = Nothing
        Dim strQuery As String = ""
        Dim _Location As String = ""
        Try
            conn = New SqlConnection(gloLibCCDGeneral.Connectionstring)

            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            strQuery = "Select isnull(sLocation,'') from AB_Location where bIsDefault='True'"
            cmd.CommandText = strQuery
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Dim temp As Object = cmd.ExecuteScalar()
            If Not IsNothing(temp) Then
                _Location = temp.ToString()
                If _Location = "" Then
                    strQuery = "Select Top 1 sLocation from AB_Location"
                    cmd.CommandText = strQuery
                    Dim temp1 As Object = cmd.ExecuteScalar()
                    If Not IsNothing(temp1) Then
                        _Location = temp1.ToString()
                    End If
                End If
            End If
            Return _Location
        Catch ex As Exception
            Return 0
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Dispose()
            conn = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function
    'Code End-Added by kanchan on 20100709 for Modular CCD Rendering & save

    Public Sub New(ByVal PatientID As Long)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        _PatientID = PatientID

    End Sub

    Private Sub tlsRegisterDemo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsRegisterDemo.Click
        Dim _PatientID As Int64 = 0
        '  Dim dtFile As DataTable

        Try

            ''-----------------------------------------------------------------------
            'dtFile = getFileType(nCCDId)
            'If Not IsNothing(dtFile) Then
            '    If dtFile.Rows.Count > 0 Then
            '        sFileType = dtFile.Rows(0)(0)
            '        Source = dtFile.Rows(0)(1)
            '    End If
            'End If


            Dim ClinicName As String = ""

            If sFileType = "CCR" Then
                Dim oCCRReader As gloCCRReader = New gloCCRReader()
                Dim _oCCRPatient As ReconcileList = Nothing
                _oCCRPatient = oCCRReader.ExtractCCR_DemographicsOnly(gloLibCCDGeneral.CCDFilePath)
                mPatient = _oCCRPatient.mPatient
                ClinicName = txtSource.Text
                mEffectiveTime = _oCCRPatient.FileHeaderDateTime
                _oCCRPatient.Dispose()
                oCCRReader.Dispose()
            ElseIf sFileType = "CDA" Then
                Dim oCDAReader As gloCDAReader = New gloCDAReader()
                Dim _oCCAPatient As ReconcileList = Nothing
                _oCCAPatient = oCDAReader.ExtractCDA_DemographicsOnly(gloLibCCDGeneral.CCDFilePath)
                mPatient = _oCCAPatient.mPatient
                ClinicName = txtSource.Text
                mEffectiveTime = _oCCAPatient.FileHeaderDateTime
                _oCCAPatient.Dispose()
                oCDAReader.Dispose()
            ElseIf sFileType = "CCD" Then
                Dim oCCDReader As gloCCDReader = New gloCCDReader()
                Dim _oCCDPatient As ReconcileList = Nothing
                _oCCDPatient = oCCDReader.ExtractCCD_DemographicsOnly(gloLibCCDGeneral.CCDFilePath)


                mPatient = _oCCDPatient.mPatient
                ClinicName = txtSource.Text
                mEffectiveTime = _oCCDPatient.FileHeaderDateTime
                _oCCDPatient.Dispose()
                oCCDReader.Dispose()
            End If
            txtSource.Text = ClinicName

            ''-----------------------------------------------------------------------

            If Not IsNothing(mPatient) Then

                If ValidatePatientMandatoryField() = False Then
                    UpdateCCDQueue(nCCDId)
                    Me.Close()
                    Me.DialogResult = DialogResult.OK
                    Exit Sub
                End If
                ''Set Patient Code
                Dim ogloPatient As gloPatient.gloPatient = New gloPatient.gloPatient(gloLibCCDGeneral.Connectionstring)
                If Not IsNothing(ogloPatient) Then
                    mPatient.PatientDemographics.DemographicsDetail.PatientCode = ogloPatient.GeneratePatientCode()
                    ogloPatient.Dispose()
                End If
                ''Set Patient default provider
                mPatient.PatientDemographics.DemographicsDetail.PatientProviderID = getDefaultProviderId()

                Dim obj As New gloPatientRegDBLayer()
                _PatientID = obj.Register_PatientDemographics(mPatient)
                obj.SavePatientAccount(_PatientID)
                obj.Dispose()
                'Patient Audit
                If _PatientID <> 0 Then
                    Dim AuditTrailId = gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.SetupPatient, gloAuditTrail.ActivityType.Add, "Add Patient", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
                    Dim oAudit As New gloPatient.clsgloPatientAudit(gloLibCCDGeneral.Connectionstring)
                    oAudit.SavePatientAuditDetails(_PatientID, AuditTrailId, "Register Patient - CCD-CCR")
                    oAudit.Dispose()
                    oAudit = Nothing
                End If
                mPatient.PatientName.ID = _PatientID
            End If

            Dim oClinicalInfo As New gloCCDLibrary.uctl_ClinicalInformation(mPatient, mEffectiveTime, sFileType)
            If Not IsNothing(oClinicalInfo) Then
                oClinicalInfo.SaveCCD(CCDFilePath, gnLoginID, txtSource.Text, mEffectiveTime, strNonXMLFilePath)
                oClinicalInfo.Dispose()
                oClinicalInfo = Nothing
            End If
            If strNonXMLFilePath = "" Then


                ''generate task of clinical document available and marked exisitng document as progressed.
                Dim _PatientName As String
                Dim _Taskid As Int64
                Dim _CCDId As Int64
                Dim _Subject As String = ""
                Dim _Note As String = ""
                Dim _TaskType As Int32
                If UpdateCCDQueue(nCCDId) = True Then
                    Update_taskStatus(nCCDId)
                    MessageBox.Show("Clinical document saved successfully ", gloCCDGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                _CCDId = InsertInCCDQueue(mPatient, CCDFilePath, 0, txtSource.Text)
                _PatientName = mPatient.PatientDemographics.DemographicsDetail.PatientFirstName & " " & mPatient.PatientDemographics.DemographicsDetail.PatientLastName
                _Subject = "Clinical document is available"
                _Note = sFileType & " file available for Patient " & _PatientName & " for " & gsCCDUSerName
                _TaskType = gloTaskMail.TaskType.CCD
                _Taskid = GenerateTasks(_PatientID, _Subject, _Note, _CCDId, _TaskType)

                If _Taskid = 0 Then

                    Exit Sub
                End If
                UpdateTaskID_CCDQueue(_CCDId, _Taskid)
                ''
            End If

            'Code Added for MU2 - Clinical Reconciliation. Open Patient in modify after registered from CCD file
            Dim frmPatientReg As New gloPatient.frmSetupPatient(_PatientID, GetConnectionString())
            If frmPatientReg IsNot Nothing Then
                frmPatientReg.ShowSaveAsCopyButton = False
                frmPatientReg.ShowDialog(IIf(IsNothing(frmPatientReg.Parent), Me, frmPatientReg.Parent))
                frmPatientReg.Dispose()
            End If

            Me.Close()
            Me.DialogResult = DialogResult.OK
            _DMSPatientID = _PatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Select, "Could not save CCD data.", gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gloCCDGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

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
            Return gloGlobal.clsFileExtensions.NewDocumentNameWithoutPath(_path, _extension, "MMddyyyyHHmmssffff") '"MM dd yyyy - hh mm ss tt")
        End Get
    End Property

    Private Function InsertInCCDQueue(ByVal objPatient As gloCCDLibrary.Patient, ByVal FilePath As String, ByVal _PatientId As Int64, ByVal Source As String) As Int64
        Dim conn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim sqlparam As SqlParameter = Nothing
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
                sqlparam.Value = objPatient.PatientDemographics.DemographicsDetail.PatientDOB
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
    Private Function GenerateTasks(ByVal _PatientID As Int64, ByVal _Subject As String, ByVal _Note As String, ByVal _CCDID As Int64, ByVal _TaskType As Int32) As Long
        Dim oTask As gloTaskMail.Task = Nothing
        Dim ogloTask As gloTaskMail.gloTask = Nothing
        Dim oTaskAssign As gloTaskMail.TaskAssign = Nothing
        Try
            Dim _TaskID As Long = 0

            If IsNothing(gnCCDDefaultUserID) = True OrElse gnCCDDefaultUserID = 0 Then
                MessageBox.Show("No CCD user have been associated, please configure using gloEMR Admin", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                GenerateTasks = Nothing
                Exit Function
            Else
                '' Send the Task to The Users

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
                'oTaskAssign.Dispose()

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

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
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

    Private Sub tls_RegisterPatient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tls_RegisterPatient.Click
        Dim _PatientID As Int64 = 0
        Try
            _PatientID = IsPatientExists(mPatient)
            If _PatientID = 0 Then
                If Not IsNothing(mPatient) Then
                    If ValidatePatientMandatoryField() = False Then
                        UpdateCCDQueue(nCCDId)
                        Me.Close()
                        Me.DialogResult = DialogResult.OK
                        Exit Sub
                    End If
                End If
                Dim obj As New gloPatientRegDBLayer()
                _PatientID = obj.RegisterNew_Patient(mPatient, "True")
                obj.SavePatientAccount(_PatientID)
                obj.Dispose()
            End If

            mPatient.PatientName.ID = _PatientID
            ouctlClinicalInfo.SaveCCD(CCDFilePath, ogloCCRInterface.UserID, txtSource.Text, mEffectiveTime, strNonXMLFilePath)
            Me.Close()
            Me.DialogResult = DialogResult.OK
            If UpdateCCDQueue(nCCDId) = True Then
                Update_taskStatus(nCCDId)
                'Code Start-Added by kanchan on 20101102
                'MessageBox.Show("Clinical Document file - " & sFileType & " saved successfully ", gloCCDGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                MessageBox.Show("Clinical Document saved successfully ", gloCCDGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            'Code Added for MU2 - Clinical Reconciliation. Open Patient in modify after registered from CCD file
            Dim frmPatientReg As New gloPatient.frmSetupPatient(_PatientID, GetConnectionString())
            If frmPatientReg IsNot Nothing Then
                frmPatientReg.ShowSaveAsCopyButton = False
                frmPatientReg.ShowDialog(IIf(IsNothing(frmPatientReg.Parent), Me, frmPatientReg.Parent))
                frmPatientReg.Dispose()
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Select, "Could not save CCD data.", gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gloCCDGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Function ValidatePatientMandatoryField() As Boolean
        Dim _result As Boolean = True
        Try
            If IsNothing(mPatient.PatientDemographics.DemographicsDetail.PatientFirstName) OrElse mPatient.PatientDemographics.DemographicsDetail.PatientFirstName.Trim() = "" Then
                MessageBox.Show("Patient could not be register, Missing patient first name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If
            If IsNothing(mPatient.PatientDemographics.DemographicsDetail.PatientLastName) OrElse mPatient.PatientDemographics.DemographicsDetail.PatientLastName.Trim() = "" Then
                MessageBox.Show("Patient could not be register, Missing patient last name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If
            If IsNothing(mPatient.PatientDemographics.DemographicsDetail.PatientDOB) OrElse mPatient.PatientDemographics.DemographicsDetail.PatientDOB.ToString("MM/dd/yyyy") = "01/01/0001" Then
                MessageBox.Show("Patient could not be register, Missing patient date of birth.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If
            If IsNothing(mPatient.PatientDemographics.DemographicsDetail.PatientGender) OrElse mPatient.PatientDemographics.DemographicsDetail.PatientGender.Trim() = "" Then
                MessageBox.Show("Patient could not be register, Missing patient gender details.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If

        Catch ex As Exception

        End Try
        Return _result
    End Function
    Public Sub ShowPDFPreview(ByVal strFilePath As String)

        '  If isCCDnonxml Then
        'pnlBrowser.Visible = True
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
    End Sub
    Private Sub btnFirst_Click(sender As System.Object, e As System.EventArgs) Handles btnFirst.Click
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

    Private Sub btnPrevious_Click(sender As System.Object, e As System.EventArgs) Handles btnPrevious.Click
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

    Private Sub btnNext_Click(sender As System.Object, e As System.EventArgs) Handles btnNext.Click
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

    Private Sub btnLast_Click(sender As System.Object, e As System.EventArgs) Handles btnLast.Click
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
End Class