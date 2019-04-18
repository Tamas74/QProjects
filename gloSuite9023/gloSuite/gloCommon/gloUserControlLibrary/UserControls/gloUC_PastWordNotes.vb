Imports oOffice = Microsoft.Office.Core
Imports Wd = Microsoft.Office.Interop.Word
Public Class gloUC_PastWordNotes

    '  Private WithEvents oCurDoc1 As Wd.Document
    Private WithEvents oCurDoc As Wd.Document
    '  Private WithEvents oTempDoc As Wd.Document
    '  Private WithEvents oWordApp As Wd.Application
    Dim tlsMessages As Object

    '23-Apr-13 Aniket: Resolving Memory Leaks
    ' Private dtExam As DataTable

    Public Property TLSMESSAGESs As Object
        Get
            Return tlsMessages
        End Get
        Set(ByVal value As Object)
            tlsMessages = value
        End Set
    End Property
    Dim _ShowPast As Boolean
    Public Property SHOWPASTs As Boolean
        Get
            Return _ShowPast
        End Get
        Set(ByVal value As Boolean)
            _ShowPast = value
        End Set
    End Property
    Dim clsPatientExams As Object
    Public Property CLSPATIENTEXAMSs As Object
        Get
            Return clsPatientExams
        End Get
        Set(ByVal value As Object)
            clsPatientExams = value
        End Set
    End Property
    Dim m_PatientID As Int64
    Public Property PATIENTIDs As Int64
        Get
            Return m_PatientID
        End Get
        Set(ByVal value As Int64)
            m_PatientID = value
        End Set
    End Property
    Dim m_visitID As Int64
    Public Property M_VISITIDs As Int64
        Get
            Return m_visitID
        End Get
        Set(ByVal value As Int64)
            m_visitID = value
        End Set
    End Property

    Dim m_LetterID As Int64
    Public Property M_LETTERIDs As Int64
        Get
            Return m_LetterID
        End Get
        Set(ByVal value As Int64)
            m_LetterID = value
        End Set
    End Property
    Dim strFormName As Int64
    Public Property STRFORMNAMEs As String
        Get
            Return strFormName
        End Get
        Set(ByVal value As String)
            strFormName = value
        End Set
    End Property
    Public Property OBJWORDs As Object
        Get
            Return ObjWord
        End Get
        Set(ByVal value As Object)
            ObjWord = value
        End Set
    End Property
    Dim objCriteria As Object
    Public Property OBJCRITERIAs As Object
        Get
            Return objCriteria
        End Get
        Set(ByVal value As Object)
            objCriteria = value
        End Set
    End Property
    Dim objclsPatientLetters As Object
    Public Property OBJCLSPATIENTLETTERSs As Object
        Get
            Return objclsPatientLetters
        End Get
        Set(ByVal value As Object)
            objclsPatientLetters = value
        End Set
    End Property
    Dim ExamNewDocumentName As String
    Public Property EXAMNEWDOCUMENTNAMEs As String
        Get
            Return ExamNewDocumentName
        End Get
        Set(ByVal value As String)
            ExamNewDocumentName = value
        End Set
    End Property
    Dim FromForm As String
    Public Property FromForms As String
        Get
            Return FromForm
        End Get
        Set(ByVal value As String)
            FromForm = value
        End Set
    End Property
    Dim objclsPTProtocol As Object
    Public Property OBJCLSPTPROTOCOLs As Object
        Get
            Return objclsPTProtocol
        End Get
        Set(ByVal value As Object)
            objclsPTProtocol = value
        End Set
    End Property
    Dim objclsPatientConsent As Object
    Public Property OBJCLSPATIENTCONSENTs As Object
        Get
            Return objclsPatientConsent
        End Get
        Set(ByVal value As Object)
            objclsPatientConsent = value
        End Set
    End Property
    Dim objclsNotes As Object
    Public Property OBJCLSNOTESs As Object
        Get
            Return objclsNotes
        End Get
        Set(ByVal value As Object)
            objclsNotes = value
        End Set
    End Property
    Dim objclsDisclosure As Object
    Public Property OBJCLSDISCLOSUREs As Object
        Get
            Return objclsDisclosure
        End Get
        Set(ByVal value As Object)
            objclsDisclosure = value
        End Set
    End Property

    Public Sub ShowHide_PastExam()
        Select Case FromForms
            Case "PatientExam"
                If dgExams.CurrentRowIndex <> -1 Then
                    FillPastExamContents(dgExams.Item(dgExams.CurrentRowIndex, 0))
                End If
            Case "PatientLetter"
                lblName.Text = "Past PatientLetter"
                FillPatientLetter()
            Case "PTProtocols"
                lblName.Text = "Past PTProtocols"
                FillPTProtocal()
            Case "PatientConsent"
                lblName.Text = "Past PatientConsent"
                FillPatientConcent()
            Case "NursesNotes"
                lblName.Text = "Past NursesNotes"
                FillNursesNote()
            Case "DisclosureManagement"
                lblName.Text = "Past DisclosureManagement"
                FillDisclosurManagement()
        End Select
    End Sub
#Region "Exam"
    Public Sub FillPastExams()
        dgExams.DataSource = Nothing
        Dim dtExam As DataTable

        'Pass patient id & get all exams for that patient
        dtExam = clsPatientExams.Fill_Exams(m_PatientID)
        ' clsPatientExams = Nothing
        dgExams.DataSource = dtExam.DefaultView
        'Grid Style
        Dim grdTableStyle As New clsDataGridTableStyle(dtExam.TableName)

        Dim grdColStylePatientID As New DataGridTextBoxColumn
        With grdColStylePatientID
            .HeaderText = "Exam ID"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtExam.Columns(0).ColumnName
            .NullText = ""
            .Width = 0
        End With

        Dim grdColStylePatientCode As New DataGridTextBoxColumn
        With grdColStylePatientCode
            .HeaderText = "Visit ID"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtExam.Columns(1).ColumnName
            .NullText = ""
            .Width = 0
        End With

        Dim grdColStylePatientFirstName As New DataGridTextBoxColumn
        With grdColStylePatientFirstName
            .HeaderText = "DOS"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtExam.Columns(2).ColumnName
            .NullText = ""
            .Width = (dgExams.Width / 3)
        End With

        Dim grdColStylePatientLastName As New DataGridTextBoxColumn
        With grdColStylePatientLastName
            .HeaderText = "Exam Name"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtExam.Columns(3).ColumnName
            .NullText = ""
            .Width = dgExams.Width / 3
        End With


        Dim grdColStylePatientSSNNo As New DataGridTextBoxColumn
        With grdColStylePatientSSNNo
            .HeaderText = "Finished"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtExam.Columns(5).ColumnName
            .NullText = ""
            .Width = (dgExams.Width / 3) - 20
        End With


        grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStylePatientID, grdColStylePatientCode, grdColStylePatientFirstName, grdColStylePatientLastName, grdColStylePatientSSNNo})
        dgExams.TableStyles.Clear()

        dgExams.TableStyles.Add(grdTableStyle)
        dgExams.ColumnHeadersVisible = True
        dgExams.RowHeadersVisible = True

    End Sub
    Private Sub FillPastExamContents(ByVal nPastExamId As Long)
        Dim strFileName As String
        objCriteria.DocCategory = enumDocCategory.Exam
        objCriteria.PrimaryID = nPastExamId
        ObjWord.DocumentCriteria = objCriteria
        ''// Get the Docuemnt From DB
        strFileName = ObjWord.RetrieveDocumentFile()
        objCriteria = Nothing
        ObjWord = Nothing
        If (IsNothing(strFileName)) Then
            Exit Sub
        End If
        If strFileName = "" Then
            Exit Sub
        End If
        ObjWord = Nothing
        wdPastMessages.Open(strFileName)
        oCurDoc = wdPastMessages.ActiveDocument
        '   oWordApp = oCurDoc1.Application
        SetWordObjectView()
    End Sub
    Private Sub SetWordObjectView()
        If (IsNothing(oCurDoc)) Then
            Return
        End If
        oCurDoc.ActiveWindow.SetFocus()
        oCurDoc.ActiveWindow.View.WrapToWindow = True
        oCurDoc.Application.ActiveDocument.Protect(Wd.WdProtectionType.wdAllowOnlyComments)
        'oCurDoc2.ActiveWindow.View.Type = 7
        'tmrPastExamProtect.Enabled = False
        ' '''' to Hide Protection Bar when Document is Finished

        ' '' Save Btn is Always INVisible when doc is Finished
        ' '' Initalise Timer
        'tmrPastExamProtect.Enabled = True
        'tmrPastExamProtect.Interval = 10


    End Sub
#End Region

#Region "PatientLetters"
    Public Sub FillPatientLetter()
        dgExams.DataSource = Nothing
        Dim dvExam As DataView
        Dim dtExam As DataTable
        'Pass patient id & get all exams for that patient

        dvExam = objclsPatientLetters.GetAllPatientLetters(m_PatientID)
        dtExam = dvExam.ToTable
        '   clsPatientExams = Nothing
        dgExams.DataSource = dvExam
        'Grid Style
        Dim grdTableStyle As New clsDataGridTableStyle(dtExam.TableName)

        Dim grdColStylePatientID As New DataGridTextBoxColumn
        With grdColStylePatientID
            .HeaderText = "Letter ID"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtExam.Columns(0).ColumnName
            .NullText = ""
            .Width = 0
        End With

        Dim grdColStylePatientFirstName As New DataGridTextBoxColumn
        With grdColStylePatientFirstName
            .HeaderText = "Letter date"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtExam.Columns(1).ColumnName
            .NullText = ""
            .Width = (dgExams.Width / 3)
        End With

        Dim grdColStylePatientCode As New DataGridTextBoxColumn
        With grdColStylePatientCode
            .HeaderText = "Template ID"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtExam.Columns(2).ColumnName
            .NullText = ""
            .Width = 0
        End With



        Dim grdColStylePatientLastName As New DataGridTextBoxColumn
        With grdColStylePatientLastName
            .HeaderText = "Template Name"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtExam.Columns(3).ColumnName
            .NullText = ""
            .Width = dgExams.Width / 3
        End With


        Dim grdColStylePatientSSNNo As New DataGridTextBoxColumn
        With grdColStylePatientSSNNo
            .HeaderText = "Finished"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtExam.Columns(4).ColumnName
            .NullText = ""
            .Width = (dgExams.Width / 3) - 20
        End With


        grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStylePatientID, grdColStylePatientCode, grdColStylePatientFirstName, grdColStylePatientLastName, grdColStylePatientSSNNo})
        dgExams.TableStyles.Clear()

        dgExams.TableStyles.Add(grdTableStyle)
        dgExams.ColumnHeadersVisible = True
        dgExams.RowHeadersVisible = True

    End Sub
    Private Sub Fill_PatientLetter()
        Dim grdIndex As Integer = dgExams.CurrentRowIndex
        If (dgExams.Item(grdIndex, 0).ToString() <> "") AndAlso (Not IsNothing(dgExams.Item(grdIndex, 0).ToString())) Then

            m_LetterID = dgExams.Item(grdIndex, 0).ToString()
            Dim dtLetter As DataTable = objclsPatientLetters.ScanPatientLetter(m_LetterID)

            If Not IsNothing(dtLetter) Then
                If dtLetter.Rows.Count > 0 Then

                    Dim strFileName As String
                    strFileName = gloSettings.FolderSettings.AppTempFolderPath + getUniqueID() + ".docx"
                    strFileName = ObjWord.GenerateFile(dtLetter.Rows(0)(1), strFileName)

                    LoadWordUserControl(strFileName, False)
                    'Set the Start postion of the cursor in documents
                    oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
                    oCurDoc.Saved = True
                    'getFields()

                Else
                    wdPastMessages.Close()
                End If
                dtLetter.Dispose()
                dtLetter = Nothing
            Else
                wdPastMessages.Close()

            End If
        Else
            Return
        End If
    End Sub
#End Region

#Region "PTProtocal"
    Public Sub FillPTProtocal()
        dgExams.DataSource = Nothing
        Dim dvExam As DataView
        Dim dtExam As DataTable
        'Pass patient id & get all exams for that patient

        dvExam = objclsPTProtocol.GetAllPTProtocols(m_PatientID)
        dtExam = dvExam.ToTable
        '   clsPatientExams = Nothing
        dgExams.DataSource = dvExam
        'Grid Style
        Dim grdTableStyle As New clsDataGridTableStyle(dtExam.TableName)

        Dim grdColStylePatientID As New DataGridTextBoxColumn
        With grdColStylePatientID
            .HeaderText = "PTProtocal ID"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtExam.Columns(0).ColumnName
            .NullText = ""
            .Width = 0
        End With

        Dim grdColStylePatientFirstName As New DataGridTextBoxColumn
        With grdColStylePatientFirstName
            .HeaderText = "PTProtocal date"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtExam.Columns(1).ColumnName
            .NullText = ""
            .Width = (dgExams.Width / 3)
        End With

        Dim grdColStylePatientCode As New DataGridTextBoxColumn
        With grdColStylePatientCode
            .HeaderText = "Template ID"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtExam.Columns(2).ColumnName
            .NullText = ""
            .Width = 0
        End With



        Dim grdColStylePatientLastName As New DataGridTextBoxColumn
        With grdColStylePatientLastName
            .HeaderText = "Template Name"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtExam.Columns(3).ColumnName
            .NullText = ""
            .Width = dgExams.Width / 3
        End With


        Dim grdColStylePatientSSNNo As New DataGridTextBoxColumn
        With grdColStylePatientSSNNo
            .HeaderText = "Finished"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtExam.Columns(4).ColumnName
            .NullText = ""
            .Width = (dgExams.Width / 3) - 20
        End With


        grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStylePatientID, grdColStylePatientCode, grdColStylePatientFirstName, grdColStylePatientLastName, grdColStylePatientSSNNo})
        dgExams.TableStyles.Clear()

        dgExams.TableStyles.Add(grdTableStyle)
        dgExams.ColumnHeadersVisible = True
        dgExams.RowHeadersVisible = True

    End Sub
    Private Sub Fill_PTProtocal()
        Dim grdIndex As Integer = dgExams.CurrentRowIndex
        If (dgExams.Item(grdIndex, 0).ToString() <> "") AndAlso (Not IsNothing(dgExams.Item(grdIndex, 0).ToString())) Then
            m_LetterID = dgExams.Item(grdIndex, 0).ToString()
            Dim dtLetter As DataTable = OBJCLSPTPROTOCOLs.ScanPTProtocol(m_LetterID)

            If Not IsNothing(dtLetter) Then
                If dtLetter.Rows.Count > 0 Then

                    Dim strFileName As String
                    strFileName = gloSettings.FolderSettings.AppTempFolderPath + getUniqueID() + ".docx"
                    strFileName = ObjWord.GenerateFile(dtLetter.Rows(0)(1), strFileName)

                    LoadWordUserControl(strFileName, False)
                    'Set the Start postion of the cursor in documents
                    oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
                    oCurDoc.Saved = True
                    'getFields()

                Else
                    wdPastMessages.Close()
                End If
                dtLetter.Dispose()
                dtLetter = Nothing
            Else
                wdPastMessages.Close()
            End If
        Else
            Return
        End If
    End Sub
#End Region

#Region "PatientConcent"
    Public Sub FillPatientConcent()
        dgExams.DataSource = Nothing
        Dim dvExam As DataView
        Dim dtExam As DataTable
        'Pass patient id & get all exams for that patient

        dvExam = objclsPatientConsent.GetAllPatientConsents(m_PatientID)
        dtExam = dvExam.ToTable
        '    clsPatientExams = Nothing
        dgExams.DataSource = dvExam
        'Grid Style
        Dim grdTableStyle As New clsDataGridTableStyle(dtExam.TableName)

        Dim grdColStylePatientID As New DataGridTextBoxColumn
        With grdColStylePatientID
            .HeaderText = "PatientConcentID ID"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtExam.Columns(0).ColumnName
            .NullText = ""
            .Width = 0
        End With

        Dim grdColStylePatientFirstName As New DataGridTextBoxColumn
        With grdColStylePatientFirstName
            .HeaderText = "Patient Consent Date"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtExam.Columns(1).ColumnName
            .NullText = ""
            .Width = (dgExams.Width / 3)
        End With

        Dim grdColStylePatientCode As New DataGridTextBoxColumn
        With grdColStylePatientCode
            .HeaderText = "Template ID"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtExam.Columns(2).ColumnName
            .NullText = ""
            .Width = 0
        End With



        Dim grdColStylePatientLastName As New DataGridTextBoxColumn
        With grdColStylePatientLastName
            .HeaderText = "Template Name"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtExam.Columns(3).ColumnName
            .NullText = ""
            .Width = dgExams.Width / 3
        End With


        Dim grdColStylePatientSSNNo As New DataGridTextBoxColumn
        With grdColStylePatientSSNNo
            .HeaderText = "Finished"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtExam.Columns(4).ColumnName
            .NullText = ""
            .Width = (dgExams.Width / 3) - 20
        End With


        grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStylePatientID, grdColStylePatientCode, grdColStylePatientFirstName, grdColStylePatientLastName, grdColStylePatientSSNNo})
        dgExams.TableStyles.Clear()

        dgExams.TableStyles.Add(grdTableStyle)
        dgExams.ColumnHeadersVisible = True
        dgExams.RowHeadersVisible = True

    End Sub
    Private Sub Fill_PatientConcent()
        Dim grdIndex As Integer = dgExams.CurrentRowIndex

        If (dgExams.Item(grdIndex, 0).ToString() <> "") AndAlso (Not IsNothing(dgExams.Item(grdIndex, 0).ToString())) Then
            m_LetterID = dgExams.Item(grdIndex, 0).ToString()
            Dim dtLetter As DataTable = objclsPatientConsent.ScanPatientConsent(m_LetterID)

            If Not IsNothing(dtLetter) Then
                If dtLetter.Rows.Count > 0 Then

                    Dim strFileName As String
                    strFileName = gloSettings.FolderSettings.AppTempFolderPath + getUniqueID() + ".docx"
                    strFileName = ObjWord.GenerateFile(dtLetter.Rows(0)(1), strFileName)

                    LoadWordUserControl(strFileName, False)
                    'Set the Start postion of the cursor in documents
                    oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
                    oCurDoc.Saved = True
                    'getFields()

                Else
                    wdPastMessages.Close()
                End If
                dtLetter.Dispose()
                dtLetter = Nothing
            Else
                wdPastMessages.Close()
            End If
        Else
            Return
        End If
    End Sub
#End Region

#Region "NursesNote"
    Public Sub FillNursesNote()
        dgExams.DataSource = Nothing
        Dim dvExam As DataView
        Dim dtExam As DataTable
        'Pass patient id & get all exams for that patient

        dvExam = objclsNotes.GetAllNurseNotes(m_PatientID)
        dtExam = dvExam.ToTable
        '   clsPatientExams = Nothing
        dgExams.DataSource = dvExam
        'Grid Style
        Dim grdTableStyle As New clsDataGridTableStyle(dtExam.TableName)

        Dim grdColStylePatientID As New DataGridTextBoxColumn
        With grdColStylePatientID
            .HeaderText = "Nursesnote ID"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtExam.Columns(0).ColumnName
            .NullText = ""
            .Width = 0
        End With

        Dim grdColStylePatientFirstName As New DataGridTextBoxColumn
        With grdColStylePatientFirstName
            .HeaderText = "Nurses Note Date"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtExam.Columns(1).ColumnName
            .NullText = ""
            .Width = (dgExams.Width / 3)
        End With

        Dim grdColStylePatientCode As New DataGridTextBoxColumn
        With grdColStylePatientCode
            .HeaderText = "Template ID"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtExam.Columns(2).ColumnName
            .NullText = ""
            .Width = 0
        End With



        Dim grdColStylePatientLastName As New DataGridTextBoxColumn
        With grdColStylePatientLastName
            .HeaderText = "Template Name"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtExam.Columns(3).ColumnName
            .NullText = ""
            .Width = dgExams.Width / 3
        End With


        Dim grdColStylePatientSSNNo As New DataGridTextBoxColumn
        With grdColStylePatientSSNNo
            .HeaderText = "Finished"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtExam.Columns(4).ColumnName
            .NullText = ""
            .Width = (dgExams.Width / 3) - 20
        End With


        grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStylePatientID, grdColStylePatientCode, grdColStylePatientFirstName, grdColStylePatientLastName, grdColStylePatientSSNNo})
        dgExams.TableStyles.Clear()

        dgExams.TableStyles.Add(grdTableStyle)
        dgExams.ColumnHeadersVisible = True
        dgExams.RowHeadersVisible = True

    End Sub
    Private Sub Fill_NursesNote()
        Dim grdIndex As Integer = dgExams.CurrentRowIndex
        If (dgExams.Item(grdIndex, 0).ToString() <> "") AndAlso (Not IsNothing(dgExams.Item(grdIndex, 0).ToString())) Then
            m_LetterID = dgExams.Item(grdIndex, 0).ToString()
            Dim dtLetter As DataTable = objclsNotes.ScanNurseNotes(m_LetterID)

            If Not IsNothing(dtLetter) Then
                If dtLetter.Rows.Count > 0 Then

                    Dim strFileName As String
                    strFileName = gloSettings.FolderSettings.AppTempFolderPath + getUniqueID() + ".docx"
                    strFileName = ObjWord.GenerateFile(dtLetter.Rows(0)(1), strFileName)

                    LoadWordUserControl(strFileName, False)
                    'Set the Start postion of the cursor in documents
                    oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
                    oCurDoc.Saved = True
                    'getFields()

                Else
                    wdPastMessages.Close()
                End If
                dtLetter.Dispose()
                dtLetter = Nothing
            Else
                wdPastMessages.Close()
            End If
        Else
            Return
        End If
    End Sub
#End Region

#Region "DisclosurManagement"
    Public Sub FillDisclosurManagement()
        dgExams.DataSource = Nothing
        Dim dvExam As DataView
        Dim dtExam As DataTable
        'Pass patient id & get all exams for that patient

        dvExam = objclsDisclosure.GetAllDisclosures(m_PatientID)
        dtExam = dvExam.ToTable
        '   clsPatientExams = Nothing
        dgExams.DataSource = dvExam
        'Grid Style
        Dim grdTableStyle As New clsDataGridTableStyle(dtExam.TableName)

        Dim grdColStylePatientID As New DataGridTextBoxColumn
        With grdColStylePatientID
            .HeaderText = "DisclosurManagement ID"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtExam.Columns(0).ColumnName
            .NullText = ""
            .Width = 0
        End With

        Dim grdColStylePatientFirstName As New DataGridTextBoxColumn
        With grdColStylePatientFirstName
            .HeaderText = "DisclosurManagement date"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtExam.Columns(1).ColumnName
            .NullText = ""
            .Width = (dgExams.Width / 3)
        End With

        Dim grdColStylePatientCode As New DataGridTextBoxColumn
        With grdColStylePatientCode
            .HeaderText = "Template ID"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtExam.Columns(2).ColumnName
            .NullText = ""
            .Width = 0
        End With



        Dim grdColStylePatientLastName As New DataGridTextBoxColumn
        With grdColStylePatientLastName
            .HeaderText = "Template Name"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtExam.Columns(3).ColumnName
            .NullText = ""
            .Width = dgExams.Width / 3
        End With


        Dim grdColStylePatientSSNNo As New DataGridTextBoxColumn
        With grdColStylePatientSSNNo
            .HeaderText = "Finished"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtExam.Columns(4).ColumnName
            .NullText = ""
            .Width = (dgExams.Width / 3) - 20
        End With


        grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStylePatientID, grdColStylePatientCode, grdColStylePatientFirstName, grdColStylePatientLastName, grdColStylePatientSSNNo})
        dgExams.TableStyles.Clear()

        dgExams.TableStyles.Add(grdTableStyle)
        dgExams.ColumnHeadersVisible = True
        dgExams.RowHeadersVisible = True

    End Sub
    Private Sub Fill_DisclosurManagement()
        Dim grdIndex As Integer = dgExams.CurrentRowIndex
        If (dgExams.Item(grdIndex, 0).ToString() <> "") AndAlso (Not IsNothing(dgExams.Item(grdIndex, 0).ToString())) Then
            m_LetterID = dgExams.Item(grdIndex, 0).ToString()
            Dim dtLetter As DataTable = objclsDisclosure.ScanDisclosure(m_LetterID)

            If Not IsNothing(dtLetter) Then
                If dtLetter.Rows.Count > 0 Then

                    Dim strFileName As String
                    strFileName = gloSettings.FolderSettings.AppTempFolderPath + getUniqueID() + ".docx"
                    strFileName = ObjWord.GenerateFile(dtLetter.Rows(0)(1), strFileName)

                    LoadWordUserControl(strFileName, False)
                    'Set the Start postion of the cursor in documents
                    oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
                    oCurDoc.Saved = True
                    'getFields()

                Else
                    wdPastMessages.Close()
                End If
                dtLetter.Dispose()
                dtLetter = Nothing
            Else
                wdPastMessages.Close()
            End If
        Else
            Return
        End If
    End Sub
#End Region

    Private Sub dgExams_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgExams.DoubleClick
        Try
            'Dim nPastExamId As Long

            ''Bug #13532 First nurse notes is displayed when we click on empty space also.
            ''If there is no rows available in the grid it throw the exception
            ''hence checked for the boundary condition "-1"

            Dim grdIndex As Integer = dgExams.CurrentRowIndex
            If grdIndex > -1 Then
                Select Case FromForms
                    Case "PatientExam"
                        If dgExams.CurrentRowIndex <> -1 Then
                            FillPastExamContents(dgExams.Item(dgExams.CurrentRowIndex, 0))
                        End If
                    Case "PatientLetter"
                        Fill_PatientLetter()
                    Case "PTProtocols"
                        Fill_PTProtocal()
                    Case "PatientConsent"
                        Fill_PatientConcent()
                    Case "NursesNotes"
                        Fill_NursesNote()
                    Case "DisclosureManagement"
                        Fill_DisclosurManagement()
                End Select
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK)
        End Try
    End Sub

    Dim ObjWord As Object

    Private Sub LoadWordUserControl(ByVal strFileName As String, Optional ByVal blnGetData As Boolean = False)

        wdPastMessages.Open(strFileName)
        If blnGetData Then
            ''//To retrieve the Form fields for the Word document

            objCriteria.DocCategory = enumDocCategory.Others

            objCriteria.PatientID = m_PatientID

            objCriteria.PrimaryID = 0
            ObjWord.DocumentCriteria = objCriteria
            ObjWord.CurDocument = oCurDoc
            ''Replace Form fields with Concerned data
            ObjWord.GetFormFieldData(enumDocType.None)
            oCurDoc = ObjWord.CurDocument
            oCurDoc.ActiveWindow.View.ShowFieldCodes = False

        Else
            oCurDoc = wdPastMessages.ActiveDocument
            ObjWord.CurDocument = oCurDoc
            ObjWord.HighlightColor()
            oCurDoc = ObjWord.CurDocument
            oCurDoc.ActiveWindow.View.ShowFieldCodes = False

        End If

        SetWordObjectView()
    End Sub
#Region "enums"
    Public Enum enumDocCategory
        Template = 1
        Exam = 2
        Referrals = 3
        Orders = 4
        Message = 5
        Others = 6
        Addendum = 7

    End Enum
    Public Enum enumControls
        None = 0
        FormFieldControl = 1
        ContentControl = 2
        Others = 3
    End Enum
    Public Enum enumDocType
        None = 0
        Diagnosis = 1
        Treatment = 2
        Prescription = 3
        RadiologyOrders = 4
        Vitals = 5
        ROS = 6
        History = 7
        Medication = 8
        PatientEducation = 9
        Flowsheet = 10
        Referrals = 11
        SmartDiagnosis = 12
        ProblemList = 13
        SmartTreatment = 14
        Tasks = 15
        CheifComplaints = 16
        PatientDemographics = 17
        PatientGuideline = 18
        Others = 19
        Contacts = 20
        Narration = 21
        ProviderSign = 22
        LabOrders = 23
        Clinic = 24
        PatientExam = 25
        Fax = 26
        Providers = 27
        DisclosureSet = 28
        Intervention = 29
        PatientExamDos = 30
        PatientExamsDx = 31
        PatientDetails = 32
        Catheterization = 33
        StressTest = 34
        ElectroPhysiology = 35
        CardiologyDevice = 36
        ElectroCardioGrams = 37
        Echocardiogram = 38
    End Enum
#End Region
    'Private Sub dgExams_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgExams.MouseUp
    '    Select Case FromForms
    '        Case "PatientExam"
    '            If dgExams.CurrentRowIndex <> -1 Then
    '                FillPastExamContents(dgExams.Item(dgExams.CurrentRowIndex, 0))
    '            End If
    '        Case "PatientLetter"
    '            GetHitPoints(sender, e)
    '        Case "PTProtocal"
    '            GetHitPoints(sender, e)
    '    End Select

    'End Sub
    Private Sub GetHitPoints(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Try
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As DataGrid.HitTestInfo = dgExams.HitTest(ptPoint)
            If htInfo.Type = DataGrid.HitTestType.Cell Then
                dgExams.Select(htInfo.Row)
            Else
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Function getUniqueID() As String
        'Static firstTime As Boolean = True
        'Static myWatch As New Stopwatch()
        'Static myTime As DateTime
        'If firstTime = True Then
        '    firstTime = False
        '    myTime = Now()
        '    myWatch.Start()
        'End If
        'Dim TmSp As New TimeSpan(myTime.Ticks + myWatch.ElapsedTicks)
        'getUniqueID = TmSp.Ticks.ToString()
        'TmSp = Nothing
        Return gloGlobal.clsFileExtensions.GetUniqueDateString()
    End Function

    '23-Apr-13 Aniket: Resolving Memory Leaks
    Public Sub ControlDispose()

        dgExams.DataSource = Nothing

        'If IsNothing(dtExam) = False Then
        '    dtExam.Dispose()
        '    dtExam = Nothing
        'End If

    End Sub

End Class
