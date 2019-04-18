'Imports oOffice = Microsoft.Office.Core
Imports Wdoc = Microsoft.Office.Interop.Word
Imports System.Data.SqlClient
Imports System.Data
Imports gloEMRGeneralLibrary
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Drawing
Imports C1.Win.C1FlexGrid
Imports gloWord





Public Class gloUC_PastWordNotes_SplitControl
    Private WithEvents oCurDoc1_SplitControl As Wdoc.Document
    Private WithEvents oTempDoc_SplitControl As Wdoc.Document
    Private WithEvents oWordApp_SplitControl As Wdoc.Application



    Dim CurrFileFullName As String = ""
    Dim PrevFileFullName As String = ""
    Dim StrFileInitialPath As String = gloSettings.FolderSettings.AppTempFolderPath + "SplitScreenTemp\"
    Dim _PatientID As Int64
    Public Property PatientID As Int64
        Get
            Return _PatientID
        End Get
        Set(ByVal value As Int64)
            _PatientID = value
        End Set
    End Property

    Dim _visitID As Int64
    Public Property visitID As Int64
        Get
            Return _visitID
        End Get
        Set(ByVal value As Int64)
            _visitID = value
        End Set
    End Property

    Dim _clinicID As Int64
    Public Property clinicID As Int64
        Get
            Return _clinicID
        End Get
        Set(ByVal value As Int64)
            _clinicID = value
        End Set
    End Property

    Dim _blnShowSmokingCol As Boolean

    Dim _ObjWord As Object
    Public Property ObjWord As Object
        Get
            Return _ObjWord
        End Get
        Set(ByVal value As Object)
            _ObjWord = value
        End Set
    End Property

    Dim _objCriteria As Object
    Public Property objCriteria As Object
        Get
            Return _objCriteria
        End Get
        Set(ByVal value As Object)
            _objCriteria = value
        End Set
    End Property



    Dim _clsPatientExams As Object
    Public Property clsPatientExams As Object
        Get
            Return _clsPatientExams
        End Get
        Set(ByVal value As Object)
            _clsPatientExams = value
        End Set
    End Property

    Dim _clsPatientLetters As Object
    Public Property clsPatientLetters As Object
        Get
            Return _clsPatientLetters
        End Get
        Set(ByVal value As Object)
            _clsPatientLetters = value
        End Set
    End Property

    Dim _clsPatientMessages As Object
    Public Property clsPatientMessages As Object
        Get
            Return _clsPatientMessages
        End Get
        Set(ByVal value As Object)
            _clsPatientMessages = value
        End Set
    End Property

    Dim _clsNurseNotes As Object
    Public Property clsNurseNotes As Object
        Get
            Return _clsNurseNotes
        End Get
        Set(ByVal value As Object)
            _clsNurseNotes = value
        End Set
    End Property

    Dim _clsReferalsLetters As Object
    Public Property clsReferalsLetters As Object
        Get
            Return _clsReferalsLetters
        End Get
        Set(ByVal value As Object)
            _clsReferalsLetters = value
        End Set
    End Property

    Dim _clsRxmed As Object
    Public Property clsRxmed As Object
        Get
            Return _clsRxmed
        End Get
        Set(ByVal value As Object)
            _clsRxmed = value
        End Set
    End Property

    Dim _clsHistory As Object
    Public Property clsHistory As Object
        Get
            Return _clsHistory
        End Get
        Set(ByVal value As Object)
            _clsHistory = value
        End Set
    End Property

    Dim _clsLabs As Object
    Public Property clsLabs As Object
        Get
            Return _clsLabs
        End Get
        Set(ByVal value As Object)
            _clsLabs = value
        End Set
    End Property

    Dim _clsDMS As Object
    Public Property clsDMS As Object
        Get
            Return _clsDMS
        End Get
        Set(ByVal value As Object)
            _clsDMS = value
        End Set
    End Property

    Dim _clsOrders As Object
    Public Property clsOrders As Object
        Get
            Return _clsOrders
        End Get
        Set(ByVal value As Object)
            _clsOrders = value
        End Set
    End Property

    Dim _clsProblemList As Object
    Public Property clsProblemList As Object
        Get
            Return _clsProblemList
        End Get
        Set(ByVal value As Object)
            _clsProblemList = value
        End Set
    End Property


    Dim _clsUCLabControl As Object
    Public Property clsUCLabControl As Object
        Get
            Return _clsUCLabControl
        End Get
        Set(ByVal value As Object)
            _clsUCLabControl = value
        End Set
    End Property



    Dim _LetterID As Int64
    Public Property LetterID As Int64
        Get
            Return _LetterID
        End Get
        Set(ByVal value As Int64)
            _LetterID = value
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

    Dim _ShowPast As Boolean
    Public Property ShowPast As Boolean
        Get
            Return _ShowPast
        End Get
        Set(ByVal value As Boolean)
            _ShowPast = value
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

    Dim _strFormName As Int64
    Public Property strFormName As String
        Get
            Return _strFormName
        End Get
        Set(ByVal value As String)
            _strFormName = value
        End Set
    End Property


    Public Sub New(ByVal PatientId As Long, ByVal VisitId As Long, ByVal ScreenType As String, ByVal objCriteria As Object, ByVal ObjWord As Object, ByVal ObjClass As Object, ByVal clinicId As Long, ByVal blnShowSmokingCol As Boolean)
        InitializeComponent()
        _PatientID = PatientId
        _visitID = VisitId
        _clinicID = clinicId
        _objCriteria = objCriteria
        _ObjWord = ObjWord
        FromForms = ScreenType
        _blnShowSmokingCol = blnShowSmokingCol

        dgList.DataSource = Nothing
        'gloC1FlexStyle.Style(dgList)
        dgList.AllowEditing = False
        dgList.Rows.Count = 1


        Select Case FromForms
            Case "PatientExam"
                _clsPatientExams = ObjClass
                pnlList.Visible = True
                pnlData.Visible = True
                pnlList.Dock = DockStyle.Top
                Splitter1.Dock = DockStyle.Top
                pnlData.Dock = DockStyle.Fill
                FillPatientExams()
            Case "PatientLetter"
                _clsPatientLetters = ObjClass
                pnlList.Visible = True
                pnlData.Visible = True
                pnlList.Dock = DockStyle.Top
                Splitter1.Dock = DockStyle.Top
                pnlData.Dock = DockStyle.Fill
                FillPatientLetter()
            Case "PatientMessages"
                _clsPatientMessages = ObjClass
                pnlList.Visible = True
                pnlData.Visible = True
                pnlList.Dock = DockStyle.Top
                Splitter1.Dock = DockStyle.Top
                pnlData.Dock = DockStyle.Fill
                FillPatientMessages()
            Case "NurseNotes"
                _clsNurseNotes = ObjClass
                pnlList.Visible = True
                pnlData.Visible = True
                pnlList.Dock = DockStyle.Top
                Splitter1.Dock = DockStyle.Top
                pnlData.Dock = DockStyle.Fill
                FillNursesNote()
            Case "RxMed"
                _clsRxmed = ObjClass
                pnlList.Visible = True
                pnlData.Visible = False
                pnlList.Dock = DockStyle.Fill
                Splitter1.Dock = DockStyle.Bottom
                FillRxMed()
            Case "History"
                _clsHistory = ObjClass
                pnlList.Visible = True
                pnlData.Visible = False
                pnlList.Dock = DockStyle.Fill
                Splitter1.Dock = DockStyle.Bottom
                FillHistory()
            Case "Labs"
                _clsLabs = ObjClass
                pnlList.Visible = True
                pnlData.Visible = False
                pnlList.Dock = DockStyle.Fill
                Splitter1.Dock = DockStyle.Bottom
                FillLabs()
            Case "DMS"
                _clsDMS = ObjClass
                pnlList.Visible = True
                pnlData.Visible = True
                pnlList.Dock = DockStyle.Top
                Splitter1.Dock = DockStyle.Top
                pnlData.Dock = DockStyle.Fill
                FillDMS()
            Case "Orders"
                _clsOrders = ObjClass
                pnlList.Visible = True
                pnlData.Visible = True
                pnlList.Dock = DockStyle.Top
                Splitter1.Dock = DockStyle.Top
                pnlData.Dock = DockStyle.Fill
                FillOrders()
            Case "ProblemList"
                _clsProblemList = ObjClass
                pnlList.Visible = True
                pnlData.Visible = False
                pnlList.Dock = DockStyle.Fill
                Splitter1.Dock = DockStyle.Bottom
                FillProblemList()
        End Select

        Try
            wdSplitControl.ActivationPolicy = DSOFramer.dsoActivationPolicy.dsoKeepUIActiveOnAppDeactive
        Catch ex As Exception

        End Try

    End Sub

    Public Sub ShowHide_PastExam()

        dgList.DataSource = Nothing
        'gloC1FlexStyle.Style(dgList)
        dgList.AllowEditing = False
        dgList.Rows.Count = 1

        Select Case FromForms
            Case "PatientExam"
                pnlList.Visible = True
                pnlData.Visible = True
                pnlList.Dock = DockStyle.Top
                Splitter1.Dock = DockStyle.Top
                pnlData.Dock = DockStyle.Fill
                FillPatientExams()
            Case "PatientLetter"
                pnlList.Visible = True
                pnlData.Visible = True
                pnlList.Dock = DockStyle.Top
                Splitter1.Dock = DockStyle.Top
                pnlData.Dock = DockStyle.Fill
                FillPatientLetter()
            Case "PatientMessages"
                pnlList.Visible = True
                pnlData.Visible = True
                pnlList.Dock = DockStyle.Top
                Splitter1.Dock = DockStyle.Top
                pnlData.Dock = DockStyle.Fill
                FillPatientMessages()
            Case "NurseNotes"
                pnlList.Visible = True
                pnlData.Visible = True
                pnlList.Dock = DockStyle.Top
                Splitter1.Dock = DockStyle.Top
                pnlData.Dock = DockStyle.Fill
                FillNursesNote()
            Case "RxMed"
                pnlList.Visible = True
                pnlData.Visible = False
                pnlList.Dock = DockStyle.Fill
                Splitter1.Dock = DockStyle.Bottom
                FillRxMed()
            Case "History"
                pnlList.Visible = True
                pnlData.Visible = False
                pnlList.Dock = DockStyle.Fill
                Splitter1.Dock = DockStyle.Bottom
                FillHistory()
            Case "Labs"
                pnlList.Visible = True
                pnlData.Visible = False
                pnlList.Dock = DockStyle.Fill
                Splitter1.Dock = DockStyle.Bottom
                FillLabs()
            Case "DMS"
                pnlList.Visible = True
                pnlData.Visible = True
                pnlList.Dock = DockStyle.Top
                Splitter1.Dock = DockStyle.Top
                pnlData.Dock = DockStyle.Fill
                FillDMS()
            Case "Orders"
                pnlList.Visible = True
                pnlData.Visible = True
                pnlList.Dock = DockStyle.Top
                Splitter1.Dock = DockStyle.Top
                pnlData.Dock = DockStyle.Fill
                FillOrders()
            Case "ProblemList"
                pnlList.Visible = True
                pnlData.Visible = False
                pnlList.Dock = DockStyle.Fill
                Splitter1.Dock = DockStyle.Bottom
                FillProblemList()
        End Select
    End Sub

#Region "Exam"

    Public Sub FillPatientExams()

        dgList.DataSource = Nothing
        Dim dtExam As DataTable = Nothing
        Dim strRoles As String = String.Empty
        Dim nExamID As Long = 0
        Try
            'dtExam = New DataTable
            If IsNothing(_clsPatientExams) Then
                Exit Sub
            End If

            dtExam = _clsPatientExams.Fill_Exams(_PatientID)

            clsPatientExams = Nothing
            dtExam.Columns.Add("RoleOfProvider", GetType(String))
            dtExam.Columns("nExamID").SetOrdinal(0)
            dtExam.Columns("nVisitID").SetOrdinal(1)
            dtExam.Columns("DOS").SetOrdinal(2)
            dtExam.Columns("Exam Name").SetOrdinal(3)
            dtExam.Columns("Template Name").SetOrdinal(4)
            dtExam.Columns("Specialty").SetOrdinal(5)
            dtExam.Columns("Finished").SetOrdinal(6)
            dtExam.Columns("ProviderName").SetOrdinal(7)
            dtExam.Columns("ReviewedBy").SetOrdinal(8)
            dtExam.Columns("RoleOfProvider").SetOrdinal(9)

            dgList.DataSource = dtExam
            dgList.Cols("nExamID").Visible = False
            dgList.Cols("nVisitID").Visible = False


            'Width Setting

            dgList.Cols("nExamID").Width = 0
            dgList.Cols("nVisitID").Width = 0
            dgList.Cols("DOS").Width = dgList.Width / 3
            dgList.Cols("Exam Name").Width = dgList.Width / 3
            dgList.Cols("Template Name").Width = dgList.Width / 3
            dgList.Cols("ReviewedBy").Width = dgList.Width / 3
            dgList.Cols("ReviewedBy").Caption = "Reviewed By"
            dgList.Cols("ProviderName").Width = dgList.Width / 3
            dgList.Cols("ProviderName").Caption = "Provider Name"
            dgList.Cols("Finished").Width = dgList.Width / 3
            dgList.Cols("Specialty").Width = dgList.Width / 3
            dgList.Cols("RoleOfProvider").Width = dgList.Width / 3
            dgList.Cols("RoleOfProvider").Caption = "Role of Provider"

            dgList.AllowEditing = True
            dgList.Cols(0).AllowEditing = False
            dgList.Cols(1).AllowEditing = False
            dgList.Cols(2).AllowEditing = False
            dgList.Cols(3).AllowEditing = False
            dgList.Cols(4).AllowEditing = False
            dgList.Cols(5).AllowEditing = False
            dgList.Cols(6).AllowEditing = False
            dgList.Cols(7).AllowEditing = False
            dgList.Cols(8).AllowEditing = False
            dgList.Cols(9).AllowEditing = False


            'Set combo list for each row in column 'Role of Provider' 
            For k As Integer = 0 To dtExam.Rows.Count - 1
                nExamID = dtExam.Rows(k)("nExamID")
                strRoles = GetRoles(nExamID)

                Dim cStyle As C1.Win.C1FlexGrid.CellStyle
                ' cStyle = dgList.Styles.Add("CS_Roles" & k) ''style new for every row
                Try
                    If (dgList.Styles.Contains("CS_Roles" & k)) Then
                        cStyle = dgList.Styles("CS_Roles" & k)
                    Else
                        cStyle = dgList.Styles.Add("CS_Roles" & k)

                    End If
                Catch ex As Exception
                    cStyle = dgList.Styles.Add("CS_Roles" & k)

                End Try
                Dim rgRole As C1.Win.C1FlexGrid.CellRange = dgList.GetCellRange(k + 1, 9)

                dgList.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
                cStyle.ComboList = strRoles
                rgRole.Style = cStyle

                dgList.SetCellStyle(k + 1, 9, cStyle)

                cStyle = Nothing

                ''Select First value of combo list  for role of Provider 
                If strRoles <> "" Then

                    Dim _arrSpliter As String()
                    _arrSpliter = strRoles.Split("|") ''Split the string 

                    If _arrSpliter(0).Length > 0 Then
                        Dim strFirstRole As String = _arrSpliter(0) ''First value
                        dgList.SetData(k + 1, "RoleOfProvider", strFirstRole)
                        strFirstRole = Nothing
                    End If

                    _arrSpliter = Nothing

                End If
            Next

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

        End Try
    End Sub

    Private Function GetRoles(ByVal nExamID As Long) As String
        Dim objCon As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dtRoles As DataTable = Nothing
        Dim strRoles As String = String.Empty

        Try
            objCon = New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)

            'strSelect = "SELECT  roleOFprovider  = CASE WHEN ISNULL(sProviderName,'') = '' THEN ( ISNULL(sLoginName,'')  + '-' +  ISNULL
            '(sCategory,'') ) ELSE (ISNULL(sProviderName,'') + '-' +  ISNULL(sCategory,'') ) END   FROM PatientExam_DTL  WHERE nExamID = " & nExamID & " 
            'ORDER BY  sLoginName , sProviderName "

            cmd = New SqlCommand("gsp_GetRoleOfProviderPastExamSplitScreen", objCon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@nExamID", nExamID)
            objCon.Open()
            da = New SqlDataAdapter
            da.SelectCommand = cmd
            dtRoles = New DataTable
            da.Fill(dtRoles)

            If dtRoles IsNot Nothing AndAlso dtRoles.Rows.Count > 0 Then
                For k As Integer = 0 To dtRoles.Rows.Count - 1
                    If k = 0 Then
                        strRoles = Convert.ToString(dtRoles.Rows(k)("roleOFprovider"))
                    Else
                        strRoles += "|" & Convert.ToString(dtRoles.Rows(k)("roleOFprovider"))
                    End If
                Next
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(dtRoles) Then
                dtRoles.Dispose()
                dtRoles = Nothing
            End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try

        Return strRoles
    End Function

    Public Function Fill_Exams(ByVal nPatientID As Long) As DataTable
        Dim objCon As SqlConnection = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Dim cmd As SqlCommand = Nothing
        Try
            objCon = New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)
            cmd = New SqlCommand("gsp_GetPastExams", objCon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PatientID", nPatientID)
            objCon.Open()
            da = New SqlDataAdapter
            da.SelectCommand = cmd
            dt = New DataTable
            da.Fill(dt)
            objCon.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If

        End Try

        Return dt
    End Function

    Private Sub FillPastExamContents(ByVal nPastExamId As Long)
        Try
            Dim strFileName As String = String.Empty
            objCriteria.DocCategory = enumDocCategory.Exam
            objCriteria.PrimaryID = nPastExamId
            ObjWord.DocumentCriteria = objCriteria
            strFileName = ObjWord.RetrieveDocumentFile()
            PrevFileFullName = CurrFileFullName
            CurrFileFullName = strFileName
            If IsNothing(strFileName) Then
                Exit Sub
            End If
            If strFileName = "" Then
                Exit Sub
            End If

            '  wdSplitControl.Open(strFileName)
            '  oCurDoc1_SplitControl = wdSplitControl.ActiveDocument
            '   oWordApp_SplitControl = oCurDoc1_SplitControl.Application
            'SLR: Added Message Filter to tell DSO to wait for until word resonses
            Dim strError As String = gloWord.LoadAndCloseWord.OpenDSO(wdSplitControl, strFileName, oCurDoc1_SplitControl, oWordApp_SplitControl, True)
            If (strError <> String.Empty) Then
                MessageBox.Show(strError, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            '   SetWordObjectView()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub SetWordObjectView()
        Try
            oCurDoc1_SplitControl.ActiveWindow.SetFocus()
            oCurDoc1_SplitControl.ActiveWindow.View.WrapToWindow = True
            oCurDoc1_SplitControl.Application.ActiveDocument.Protect(Wdoc.WdProtectionType.wdAllowOnlyComments)
        Catch ex As Exception

        End Try
       
    End Sub

#End Region

#Region "PatientLetters"

    Public Sub FillPatientLetter()

        dgList.DataSource = Nothing
        Dim dvExam As DataView = Nothing
        Dim dtExam As DataTable = Nothing
        Try
            'Pass patient id & get all exams for that patient
            '        dvExam = New DataView
            '       dtExam = New DataTable

            If Not IsNothing(clsPatientLetters) Then
                dvExam = clsPatientLetters.GetAllPatientLetters(_PatientID) 'GetAllPatientLetters(_PatientID) '
                dtExam = dvExam.ToTable
                clsPatientExams = Nothing
                dgList.DataSource = dtExam

                dgList.Cols(0).Visible = False
                dgList.Cols(2).Visible = False
                dgList.Cols(0).Caption = "Letter ID"
                dgList.Cols(1).Caption = "Letter Date"
                dgList.Cols(2).Caption = "Template ID"
                dgList.Cols(3).Caption = "Letter Header"
                dgList.Cols(4).Caption = "Finished"

                dgList.Cols(0).Width = 0
                dgList.Cols(1).Width = dgList.Width / 3
                dgList.Cols(2).Width = 0
                dgList.Cols(3).Width = dgList.Width / 3
                dgList.Cols(4).Width = dgList.Width / 3
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            If Not IsNothing(dvExam) Then
                dvExam.Dispose()
                dvExam = Nothing
            End If
        End Try

    End Sub

    Private Sub Fill_PatientLetter()
        Dim grdIndex As Integer = dgList.Row
        Dim dtLetter As DataTable = Nothing
        Try
            'dtLetter = New DataTable
            If (dgList.Item(grdIndex, 0).ToString() <> "") AndAlso (Not IsNothing(dgList.Item(grdIndex, 0).ToString())) Then
                _LetterID = dgList.Item(grdIndex, 0).ToString()
                dtLetter = clsPatientLetters.ScanPatientLetter(_LetterID)
                If Not IsNothing(dtLetter) Then
                    If dtLetter.Rows.Count > 0 Then
                        Dim strFileName As String
                        If Not Directory.Exists(StrFileInitialPath) Then
                            Directory.CreateDirectory(StrFileInitialPath)
                        End If
                        strFileName = StrFileInitialPath + getUniqueID() + ".docx"
                        strFileName = ObjWord.GenerateFile(dtLetter.Rows(0)(1), strFileName)
                        PrevFileFullName = CurrFileFullName
                        CurrFileFullName = strFileName
                        LoadWordUserControl(strFileName, False)
                        oCurDoc1_SplitControl.Application.Selection.HomeKey(Wdoc.WdUnits.wdStory)
                        oCurDoc1_SplitControl.Saved = True
                    Else
                        wdSplitControl.Close()
                    End If
                    dtLetter.Dispose()
                    dtLetter = Nothing
                Else
                    wdSplitControl.Close()
                End If
            Else
                Return
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try

    End Sub

#End Region

#Region "PatientMessages"

    Public Sub FillPatientMessages()

        dgList.DataSource = Nothing
        Dim dtExam As DataTable = Nothing
        Try
            ' dtExam = New DataTable
            If Not IsNothing(clsPatientMessages) Then
                dtExam = clsPatientMessages.Fill_PatientMessges(_PatientID)
                clsPatientExams = Nothing
                dgList.DataSource = dtExam
                dgList.Cols("nMessageID").Visible = False
                dgList.Cols("sTemplateName").Caption = "Template Name"
                dgList.Cols("Date").Width = dgList.Width / 3
                dgList.Cols("Date").Caption = "Created Date"
                dgList.Cols("sTemplateName").Width = _dgList.Width / 3
                dgList.Cols("Finished").Width = dgList.Width / 3
                dgList.Cols("Priority").Width = dgList.Width / 3
                dgList.Cols("Priority").Caption = "Priority"
                dgList.Cols("LastReplyDate").Caption = "Last Replied Date"
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try

    End Sub

    Private Sub Fill_PatientMessages()
        Dim grdIndex As Integer = dgList.Row ' dgList.CurrentRowIndex
        Dim MsgId As Long
        Dim MsgDate As Date
        Dim dtMessage As DataTable = Nothing
        Try
            'dtMessage = New DataTable
            If (dgList.Item(grdIndex, 0).ToString() <> "") AndAlso (Not IsNothing(dgList.Item(grdIndex, 0).ToString())) Then
                MsgId = dgList.Item(grdIndex, 0).ToString()
                MsgDate = dgList.Item(grdIndex, 1).ToString()
                dtMessage = _clsPatientMessages.SelectMessage(MsgId, 0, MsgDate)
                If Not IsNothing(dtMessage) Then
                    If dtMessage.Rows.Count > 0 Then
                        Dim strFileName As String
                        If Not Directory.Exists(StrFileInitialPath) Then
                            Directory.CreateDirectory(StrFileInitialPath)
                        End If
                        strFileName = StrFileInitialPath + getUniqueID() + ".docx"
                        strFileName = ObjWord.GenerateFile(dtMessage.Rows(0)(7), strFileName)
                        PrevFileFullName = CurrFileFullName
                        CurrFileFullName = strFileName
                        LoadWordUserControl(strFileName, False)
                        oCurDoc1_SplitControl.Application.Selection.HomeKey(Wdoc.WdUnits.wdStory)
                        oCurDoc1_SplitControl.Saved = True
                    Else
                        wdSplitControl.Close()
                    End If
                    dtMessage.Dispose()
                    dtMessage = Nothing
                Else
                    wdSplitControl.Close()
                End If
            Else
                Return
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
    End Sub

#End Region

#Region "NursesNote"

    Public Sub FillNursesNote()
        Dim dvExam As DataView = Nothing
        Dim dtExam As DataTable = Nothing
        Try

            dgList.DataSource = Nothing
            'dvExam = New DataView
            'dtExam = New DataTable
            If Not IsNothing(_clsNurseNotes) Then
                dvExam = _clsNurseNotes.GetAllNurseNotes(_PatientID)
                dtExam = dvExam.ToTable
                clsPatientExams = Nothing
                dgList.DataSource = dtExam
                dgList.Cols(0).Visible = False
                dgList.Cols(2).Visible = False
                dgList.Cols(0).Caption = "NotesID"
                dgList.Cols(1).Caption = "Notes Date"
                dgList.Cols(2).Caption = "TemplateID"
                dgList.Cols(3).Caption = "Notes Header"
                dgList.Cols(4).Caption = "Finished"
                dgList.Cols(0).Width = 0
                dgList.Cols(1).Width = dgList.Width / 3
                dgList.Cols(2).Width = 0
                dgList.Cols(3).Width = dgList.Width / 3
                dgList.Cols(4).Width = dgList.Width / 3
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            If Not IsNothing(dvExam) Then
                dvExam.Dispose()
                dvExam = Nothing
            End If

        End Try


    End Sub

    Private Sub Fill_NursesNote()
        Dim grdIndex As Integer = dgList.Row 'dgList.CurrentRowIndex
        Dim dtLetter As DataTable = Nothing
        Try
            'dtLetter = New DataTable
            If (dgList.Item(grdIndex, 0).ToString() <> "") AndAlso (Not IsNothing(dgList.Item(grdIndex, 0).ToString())) Then
                _LetterID = dgList.Item(grdIndex, 0).ToString()
                dtLetter = _clsNurseNotes.ScanNurseNotes(_LetterID)
                If Not IsNothing(dtLetter) Then
                    If dtLetter.Rows.Count > 0 Then
                        Dim strFileName As String
                        If Not Directory.Exists(StrFileInitialPath) Then
                            Directory.CreateDirectory(StrFileInitialPath)
                        End If
                        strFileName = StrFileInitialPath + getUniqueID() + ".docx"
                        strFileName = ObjWord.GenerateFile(dtLetter.Rows(0)(1), strFileName)
                        PrevFileFullName = CurrFileFullName
                        CurrFileFullName = strFileName
                        LoadWordUserControl(strFileName, False)
                        oCurDoc1_SplitControl.Application.Selection.HomeKey(Wdoc.WdUnits.wdStory)
                        oCurDoc1_SplitControl.Saved = True
                    Else
                        wdSplitControl.Close()
                    End If
                    dtLetter.Dispose()
                    dtLetter = Nothing
                Else
                    wdSplitControl.Close()
                End If
            Else
                Return
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try

    End Sub

#End Region

#Region "Orders"

    Public Sub FillOrders()

        dgList.DataSource = Nothing
        Dim dtExam As DataTable = Nothing
        Try
            '        dtExam = New DataTable
            If Not IsNothing(_clsOrders) Then
                dtExam = _clsOrders.Fill_PatientOrders_SplitControl(_PatientID)
                dgList.DataSource = dtExam
                dgList.Cols("lm_Order_id").Visible = False
                dgList.Cols("lm_Visit_ID").Visible = False
                dgList.Cols("ICD9").Visible = False
                dgList.Cols("lm_NumericResult").Visible = False
                dgList.Cols("lm_OrderDate").Caption = "Order Date"
                dgList.Cols("lm_sCategoryName").Caption = "Category"
                dgList.Cols("lm_sTestName").Caption = "Test Name"
                dgList.Cols("lm_NumericResult").Caption = "Numeric Result"
                dgList.Cols("Template").Visible = False

                dgList.Cols("lm_OrderDate").Width = dgList.Width / 3
                dgList.Cols("lm_sCategoryName").Width = dgList.Width / 3
                dgList.Cols("lm_sTestName").Width = _dgList.Width / 3
                dgList.Cols("lm_NumericResult").Width = _dgList.Width / 3

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try

    End Sub

    Private Sub Fill_Orders()
        Try

            Dim grdIndex As Integer = dgList.Row
            If (dgList.Item(grdIndex, "Template").ToString() <> "") AndAlso (Not IsNothing(dgList.Item(grdIndex, "Template").ToString())) Then
                Dim strFileName As String
                If Not Directory.Exists(StrFileInitialPath) Then
                    Directory.CreateDirectory(StrFileInitialPath)
                End If
                strFileName = StrFileInitialPath + getUniqueID() + ".docx"
                strFileName = ObjWord.GenerateFile(dgList.Item(grdIndex, "Template"), strFileName)
                PrevFileFullName = CurrFileFullName
                CurrFileFullName = strFileName
                LoadWordUserControl(strFileName, False)
                oCurDoc1_SplitControl.Application.Selection.HomeKey(Wdoc.WdUnits.wdStory)
                oCurDoc1_SplitControl.Saved = True
            Else
                wdSplitControl.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "RxMed"

    Public Sub FillRxMed()
        Dim dvExam As DataView = Nothing
        Dim dtExam As DataTable = Nothing
        Try

            dgList.DataSource = Nothing
            '        dvExam = New DataView
            '       dtExam = New DataTable
            Dim strfilter As String = String.Empty

            If Not IsNothing(clsRxmed) Then
                dtExam = clsRxmed.Fill_Medication(_PatientID) 'clsRxmed.Fill_Prescription(_PatientID)
                clsPatientExams = Nothing
                dvExam = dtExam.Copy().DefaultView

                Dim _date As String = String.Empty
                _date = Search(dtExam)

                strfilter = "sStatus = " & "'Active'" & " and " & " dtmedicationdate >=  '" & _date & " 12:00:00 am" & "' and dtmedicationdate <= '" & _date & " 11:59:59 pm" & "' "
                dvExam.RowFilter = strfilter
                dtExam = dvExam.ToTable
                dgList.DataSource = dvExam

                dgList.AllowSorting = True
                dgList.Visible = True
                dgList.BringToFront()
                dgList.Cols.Count = 14

                dgList.Cols(0).Width = dgList.Width / 3
                dgList.Cols(1).Width = dgList.Width / 3
                dgList.Cols(2).Width = dgList.Width / 3
                dgList.Cols(3).Width = dgList.Width / 3
                dgList.Cols(4).Width = dgList.Width / 3
                dgList.Cols(5).Width = dgList.Width / 3
                dgList.Cols(6).Width = dgList.Width / 3
                dgList.Cols(7).Width = dgList.Width / 3
                dgList.Cols(8).Width = dgList.Width / 3
                dgList.Cols(9).Width = dgList.Width / 3
                dgList.Cols(10).Width = dgList.Width / 3
                dgList.Cols(11).Width = dgList.Width / 3
                dgList.Cols(12).Width = dgList.Width / 3
                dgList.Cols(13).Width = dgList.Width / 3


                dgList.Cols("dtmedicationdate").Caption = "Updated"
                dgList.Cols("username").Caption = "Reviewed By"
                dgList.Cols("UpdatedBy").Caption = "Updated By"
                dgList.Cols("smedication").Caption = "Drug"
                dgList.Cols("Prescriber").Caption = "Prescriber"
                dgList.Cols("dtstartdate").Caption = "Start Date"
                dgList.Cols("dtenddate").Caption = "End Date"
                dgList.Cols("sstatus").Caption = "Status"
                dgList.Cols("sfrequency").Caption = "Patient Directions"
                dgList.Cols("sduration").Caption = "Duration"
                dgList.Cols("samount").Caption = "Quantity" '"amount"
                dgList.Cols("sRefills").Caption = "Refills"
                dgList.Cols("sMethod").Caption = "Issue Method"
                dgList.Cols("Pharmacy").Caption = "Pharmacy"


            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(dtExam) Then
                dtExam.Dispose()
                dtExam = Nothing
            End If

        End Try
    End Sub

    Function Search(ByVal dt As DataTable) As DateTime
        Dim dr As DataRow()
        Dim _strExpr As DateTime
        Try
            Dim _strSort As String = "dtMedicationDate desc"
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    If Not IsNothing(dt) Then
                        dr = dt.Select("", _strSort)
                        If dr.Length > 0 Then
                            _strExpr = dr(0)(0)


                        End If
                    End If
                Else
                    _strExpr = DateTime.Now

                End If
            End If
        Catch ex As Exception
        Finally
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
        End Try

        Return _strExpr.Date
    End Function


#End Region

#Region "ProblemList"

    ''' <summary>
    ''' Fills the problem list.
    ''' </summary>
    Public Sub FillProblemList()

        Dim dtExam As DataTable = Nothing

        Try

            dgList.DataSource = Nothing
            'dtExam = New DataTable
            If Not IsNothing(clsProblemList) Then
                dtExam = clsProblemList.Fill_ProblemLists(_PatientID)

                With dgList
                    .AllowSorting = True
                    .Cols.Count = 20 '8
                    .Rows.Count = 1

                    ' .Rows(0).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter


                    .Cols(0).Width = 0
                    .Cols(1).Width = dgList.Width / 3
                    .Cols(2).Width = dgList.Width / 3
                    .Cols(3).Width = dgList.Width / 3
                    .Cols(4).Width = dgList.Width / 3
                    .Cols(5).Width = 0
                    .Cols(6).Width = dgList.Width / 3
                    .Cols(7).Width = dgList.Width / 3
                    .Cols(8).Width = dgList.Width / 3
                    .Cols(9).Width = 0
                    .Cols(10).Width = dgList.Width / 3
                    .Cols(11).Width = dgList.Width / 3
                    .Cols(12).Width = dgList.Width / 3
                    .Cols(13).Width = dgList.Width / 3
                    .Cols(14).Width = 0
                    .Cols(15).Width = dgList.Width / 3
                    .Cols(16).Width = dgList.Width / 3
                    .Cols(17).Width = 0
                    .Cols(18).Width = 0
                    .Cols(19).Width = 0

                    .SetData(0, 0, "ProblemID")
                    .SetData(0, 1, "DOS")
                    '.SetData(0, 2, "Chief Complaint")
                    .SetData(0, 2, "Description")
                    .SetData(0, 3, "Diagnosis")
                    .SetData(0, 4, "Prescription")
                    .SetData(0, 5, "VisitID")
                    .SetData(0, 6, "Status")
                    .SetData(0, 7, "User")
                    .SetData(0, 8, "Resolved Date")
                    .SetData(0, 9, "UserID")
                    .SetData(0, 10, "Immediacy")
                    .SetData(0, 11, "Provider")
                    .SetData(0, 12, "Location")
                    .SetData(0, 13, "Last Update")
                    .SetData(0, 14, "ExamID")
                    .SetData(0, 15, "Problem Type")
                    .SetData(0, 16, "SnoMed CT ID")
                    .SetData(0, 17, "SnoMed ID")
                    .SetData(0, 18, "Description ID")
                    .SetData(0, 19, "Definition")




                    .Cols(1).TextAlignFixed = TextAlignEnum.LeftCenter
                    .Cols(2).TextAlignFixed = TextAlignEnum.LeftCenter
                    .Cols(3).TextAlignFixed = TextAlignEnum.LeftCenter
                    .Cols(4).TextAlignFixed = TextAlignEnum.LeftCenter
                    .Cols(5).TextAlignFixed = TextAlignEnum.LeftCenter
                    .Cols(6).TextAlignFixed = TextAlignEnum.LeftCenter
                    .Cols(7).TextAlignFixed = TextAlignEnum.LeftCenter
                    .Cols(8).TextAlignFixed = TextAlignEnum.LeftCenter
                    .Cols(9).TextAlignFixed = TextAlignEnum.LeftCenter
                    .Cols(10).TextAlignFixed = TextAlignEnum.LeftCenter
                    .Cols(11).TextAlignFixed = TextAlignEnum.LeftCenter
                    .Cols(12).TextAlignFixed = TextAlignEnum.LeftCenter
                    .Cols(13).TextAlignFixed = TextAlignEnum.LeftCenter
                    .Cols(14).TextAlignFixed = TextAlignEnum.LeftCenter
                    .Cols(15).TextAlignFixed = TextAlignEnum.LeftCenter
                    .Cols(16).TextAlignFixed = TextAlignEnum.LeftCenter

                    .Cols(1).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                    .Cols(2).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                    .Cols(3).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                    .Cols(4).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                    .Cols(5).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                    .Cols(6).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                    .Cols(7).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                    .Cols(8).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                    .Cols(9).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                    .Cols(10).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                    .Cols(11).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                    .Cols(12).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                    .Cols(13).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                    .Cols(14).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                    .Cols(15).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                    .Cols(16).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter






                    If IsNothing(dtExam) = False Then
                        Dim forecolor As Color
                        Dim status As String = ""
                        For i As Int16 = 0 To dtExam.Rows.Count - 1
                            If dtExam.Rows(i)("Status") = 2 Then '2"Active"
                                forecolor = Color.Red
                                status = "Active"
                            ElseIf dtExam.Rows(i)("Status") = 1 Then '1"Resolved"
                                forecolor = Color.Green
                                status = "Resolved"
                            ElseIf dtExam.Rows(i)("Status") = 3 Then '3"Inactive"
                                forecolor = Color.Blue
                                status = "Inactive"
                            ElseIf dtExam.Rows(i)("Status") = 4 Then '4"Chronic"
                                forecolor = Color.Black
                                status = "Chronic"
                            End If

                            Dim r As C1.Win.C1FlexGrid.Row
                            r = .Rows.Add()
                            r.StyleNew.ForeColor = forecolor
                            r.Height = 20

                            .SetData(r.Index, 0, dtExam.Rows(i)("nProblemID"))
                            .SetData(r.Index, 1, dtExam.Rows(i)("dtDOS"))
                            .SetData(r.Index, 3, dtExam.Rows(i)("Diagnosis"))
                            .SetData(r.Index, 4, dtExam.Rows(i)("Prescription"))
                            .SetData(r.Index, 5, dtExam.Rows(i)("VisitID"))
                            .SetData(r.Index, 6, status)
                            .SetData(r.Index, 7, dtExam.Rows(i)("UserName"))
                            .SetData(r.Index, 8, dtExam.Rows(i)("ResolvedDt"))
                            .SetData(r.Index, 9, dtExam.Rows(i)("nUserID"))
                            .SetData(r.Index, 10, dtExam.Rows(i)("Immediacy"))
                            .SetData(r.Index, 11, dtExam.Rows(i)("Provider"))
                            .SetData(r.Index, 12, dtExam.Rows(i)("Location"))

                            If Not IsNothing(dtExam.Rows(i)("ModifiedDate")) Then
                                If Not dtExam.Rows(i)("ModifiedDate").ToString().Contains("4/12/1900") Then
                                    .SetData(r.Index, 13, dtExam.Rows(i)("ModifiedDate"))
                                End If
                            End If

                            .SetData(r.Index, 14, dtExam.Rows(i)("ExamID"))
                            .SetData(r.Index, 15, dtExam.Rows(i)("sTransactionID1"))
                            .SetData(r.Index, 16, dtExam.Rows(i)("sConceptID"))
                            .SetData(r.Index, 17, dtExam.Rows(i)("sSnoMedID"))
                            .SetData(r.Index, 18, dtExam.Rows(i)("sDescriptionID"))
                            .SetData(r.Index, 19, dtExam.Rows(i)("sDescription"))

                            'added for showing chief complaint
                            'added for showing chief complaint
                            Dim _strComplaints As String = String.Empty
                            Dim _Comments() As String
                            Dim _nCommentCount As Integer = 0

                            If Not IsNothing(dtExam.Rows(i)("Comments")) Then
                                If dtExam.Rows(i)("Comments") <> "" Then
                                    _strComplaints = dtExam.Rows(i)("Complaint") & vbNewLine & dtExam.Rows(i)("Comments")
                                    _Comments = Split(dtExam.Rows(i)("Comments"), vbNewLine)
                                    _nCommentCount = _Comments.Length + 1
                                Else
                                    _strComplaints = dtExam.Rows(i)("Complaint")
                                End If
                            Else
                                _strComplaints = dtExam.Rows(i)("Complaint")
                            End If

                            .Rows(.Rows.Count - 1).AllowResizing = AllowDraggingEnum.Both
                            .Rows(.Rows.Count - 1).AllowDragging = DrawModeEnum.OwnerDraw
                            If _nCommentCount <> 0 Then
                                .Rows(.Rows.Count - 1).Height = .Rows.DefaultSize * _nCommentCount
                            End If

                            .SetData(r.Index, 2, _strComplaints)

                            _strComplaints = Nothing
                            _Comments = Nothing
                            _nCommentCount = Nothing

                            r = Nothing
                            forecolor = Nothing
                            status = Nothing

                        Next
                    End If
                End With



            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(dtExam) Then
                dtExam.Dispose()
                dtExam = Nothing
            End If
        End Try
    End Sub

#End Region

#Region "History"

    Public Sub FillHistory()
        Dim dtExam As DataTable = Nothing
        Dim dsHistory As DataSet = Nothing
        Dim _categorytype As String = String.Empty
        Dim stronsetActiveStatus As String = String.Empty
        Dim _arrOnsetActive() As String
        Dim IsActive As Boolean = False
        Dim IsOnsetDate As Boolean = False
        Try

            'dtExam = New DataTable
            'dsHistory = New DataSet

            If Not IsNothing(_clsHistory) Then
                dsHistory = _clsHistory.Fill_StandardHistoryTypes()
                _visitID = GetVisitID(Now, PatientID)
                If _visitID > 0 Then
                    'To Check if Current History Exists
                    dtExam = _clsHistory.Fill_History(PatientID, _visitID, 0)
                Else
                    dtExam = New DataTable
                End If
                If dtExam.Rows.Count > 0 Then
                    'History Exists for Current Date
                Else
                    'If History is Not Exist For Current Date then Check for the Previous Date
                    dtExam.Dispose()
                    dtExam = Nothing
                    dtExam = _clsHistory.Fill_History(PatientID, _visitID, 1)
                    If (IsNothing(dtExam) = False) Then


                        If dtExam.Rows.Count > 0 Then
                            'If there Exist a Visit of History for Previous Date Then 
                            'Get the History for that Date
                            dtExam = _clsHistory.Fill_History(PatientID, dtExam.Rows(0)(0), 2)
                        Else
                        End If
                    End If

                End If
                clsPatientExams = Nothing


                With dgList
                    .Visible = True
                    .BringToFront()
                    .Cols.Count = 13
                    .Rows.Fixed = 1
                    .Rows.Count = 1
                    .SetData(0, 0, "VisitID")
                    .SetData(0, 1, "Category_Hidden")
                    .SetData(0, 2, "Visit Date_Hidden")
                    .SetData(0, 3, "Visit Date")
                    .SetData(0, 4, "Category")
                    .SetData(0, 5, "History")
                    .SetData(0, 6, "Comments")
                    .SetData(0, 7, "")
                    .SetData(0, 8, "Smoking Status")
                    .SetData(0, 9, "Active")
                    .SetData(0, 10, "DrugID")
                    .Cols(11).AllowEditing = False
                    .SetData(0, 11, "Occur Date") '25-Jun-13 Aniket: Change caption to Occur Date
                    '.Cols(11).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                    .Cols(12).AllowEditing = False
                    .SetData(0, 12, "Date Entered")
                    '.Cols(12).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
                    .Cols(0).Width = 0
                    .Cols(1).Width = 0
                    .Cols(2).Width = 0
                    .Cols(3).Width = dgList.Width / 3
                    .Cols(4).Width = dgList.Width / 3
                    .Cols(5).Width = dgList.Width / 3
                    .Cols(6).Width = dgList.Width / 2
                    .Cols(7).Width = dgList.Width / 4




                    .Cols(1).TextAlignFixed = TextAlignEnum.LeftCenter
                    .Cols(2).TextAlignFixed = TextAlignEnum.LeftCenter
                    .Cols(3).TextAlignFixed = TextAlignEnum.LeftCenter
                    .Cols(4).TextAlignFixed = TextAlignEnum.LeftCenter
                    .Cols(5).TextAlignFixed = TextAlignEnum.LeftCenter
                    .Cols(6).TextAlignFixed = TextAlignEnum.LeftCenter
                    .Cols(7).TextAlignFixed = TextAlignEnum.LeftCenter
                    .Cols(8).TextAlignFixed = TextAlignEnum.LeftCenter
                    .Cols(9).TextAlignFixed = TextAlignEnum.LeftCenter
                    .Cols(10).TextAlignFixed = TextAlignEnum.LeftCenter
                    .Cols(11).TextAlignFixed = TextAlignEnum.LeftCenter
                    .Cols(12).TextAlignFixed = TextAlignEnum.LeftCenter


                    .Cols(1).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                    .Cols(2).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                    .Cols(3).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                    .Cols(4).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                    .Cols(5).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                    .Cols(6).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                    .Cols(7).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                    .Cols(8).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                    .Cols(9).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                    .Cols(10).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                    .Cols(11).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                    .Cols(12).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter







                    'Smoking Column
                    If _blnShowSmokingCol = True Then
                        .Cols(8).Width = dgList.Width / 3
                    Else
                        .Cols(8).Width = 0
                    End If

                    .Cols(9).Width = dgList.Width / 6
                    .Cols(10).Width = 0
                    .Cols(11).Width = dgList.Width / 3
                    .Cols(12).Width = dgList.Width / 3
                    '''' 
                    .BeginInit()
                    If IsNothing(dtExam) = False Then
                        For i As Int16 = 0 To dtExam.Rows.Count - 1
                            Dim _Row As Integer = 0

                            For j As Int16 = 1 To .Rows.Count - 1
                                If .GetData(j, 1) = dtExam.Rows(i)("sHistoryCategory") Then
                                    Try
                                        If .GetData(j, 1) <> .GetData(j + 1, 1) Then
                                            .Rows.Insert(j + 1)
                                            _Row = j + 1
                                            .SetData(_Row, 0, dtExam.Rows(i)("nVisitID"))
                                            Exit For
                                        End If
                                    Catch ex As Exception
                                        .Rows.Insert(j + 1)
                                        _Row = j + 1
                                        .SetData(_Row, 0, dtExam.Rows(i)("nVisitID"))
                                        Exit For
                                    End Try
                                End If
                            Next

                            If Convert.ToString(dtExam.Rows(i)("sHistoryType")).Trim = "" Then
                                _categorytype = Convert.ToString(dtExam.Rows(i)(0)).Trim
                                _categorytype = _clsHistory.getHistoryTypefromcategorymaster_Other(_categorytype, dsHistory)
                            Else
                                _categorytype = Convert.ToString(dtExam.Rows(i)("sHistoryType")).Trim
                            End If

                            If _Row = 0 Then ''  Category Is Not exists
                                .Rows.Add()
                                _Row = .Rows.Count - 1
                                .SetData(_Row, 0, dtExam.Rows(i)("nVisitID"))
                                .SetData(_Row, 1, dtExam.Rows(i)("sHistoryCategory"))
                                .SetData(_Row, 2, dtExam.Rows(i)("dtVisitDate"))
                                If _Row = 1 Then
                                    .SetData(_Row, 3, dtExam.Rows(i)("dtVisitDate"))
                                End If
                                .SetData(_Row, 4, dtExam.Rows(i)("sHistoryCategory"))


                                Dim asgTask As C1.Win.C1FlexGrid.CellStyle '= dgList.Styles.Add("asgTask")
                                Try
                                    If (dgList.Styles.Contains("asgTask")) Then
                                        asgTask = dgList.Styles("asgTask")
                                    Else
                                        asgTask = dgList.Styles.Add("asgTask")
                                        asgTask.Font = gloGlobal.clsgloFont.getFontFromExistingSource(.Font, FontStyle.Bold) 'New System.Drawing.Font(.Font.FontFamily.Name, 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

                                    End If
                                Catch ex As Exception
                                    asgTask = dgList.Styles.Add("asgTask")
                                    asgTask.Font = gloGlobal.clsgloFont.getFontFromExistingSource(.Font, FontStyle.Bold) ' New System.Drawing.Font(.Font.FontFamily.Name, 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

                                End Try
                                dgList.SetCellStyle(_Row, 7, asgTask)

                                'If _categorytype = "All" Then
                                '    .SetData(_Row, 7, "Reaction")
                                'ElseIf _categorytype = "Fam" Then
                                '    .SetData(_Row, 7, "Family member")
                                'Else
                                '    .SetData(_Row, 7, "")
                                'End If

                                If Convert.ToString(dtExam.Rows(i)("sHistoryCategory")) = "Allergies" Then
                                    .SetData(_Row, 7, "Reaction")
                                ElseIf Convert.ToString(dtExam.Rows(i)("sHistoryCategory")) = "Family History" Then
                                    .SetData(_Row, 7, "Family member")
                                Else
                                    .SetData(_Row, 7, "")
                                End If


                                .Rows.Insert(_Row + 1)
                                _Row = _Row + 1
                            End If
                            .SetData(_Row, 0, dtExam.Rows(i)("nVisitID"))
                            .SetData(_Row, 1, dtExam.Rows(i)("sHistoryCategory"))
                            .SetData(_Row, 2, dtExam.Rows(i)("dtVisitDate"))
                            .SetData(_Row, 5, dtExam.Rows(i)("sHistoryItem"))
                            .SetData(_Row, 6, dtExam.Rows(i)("sComments"))
                            .SetData(_Row, 7, "")
                            .SetData(_Row, 8, "")
                            .SetData(_Row, 12, dtExam.Rows(i)("DOE_Allergy"))
                            Dim cStyle As C1.Win.C1FlexGrid.CellStyle
                            Dim rgReaction As C1.Win.C1FlexGrid.CellRange = .GetCellRange(_Row, 7, _Row, 7)
                            Dim rgActive As C1.Win.C1FlexGrid.CellRange = .GetCellRange(_Row, 9, _Row, 9)
                            ''
                            IsActive = False
                            IsOnsetDate = False


                            If _categorytype <> "" Then
                                If _categorytype.Length > 2 Then


                                    stronsetActiveStatus = _clsHistory.CheckHistoryTypeinStandardTable_other(_categorytype, dsHistory)
                                    _arrOnsetActive = stronsetActiveStatus.Split(",")
                                    If IsNothing(_arrOnsetActive) = False Then
                                        If _arrOnsetActive.Length >= 1 Then
                                            IsOnsetDate = _arrOnsetActive.GetValue(0)
                                            IsActive = _arrOnsetActive.GetValue(1)
                                        End If
                                    End If
                                End If
                            End If
                            If _categorytype = "Sur" Or _categorytype = "Pro" Then
                                .SetData(_Row, 11, dtExam.Rows(i)("OnsetDate"))
                            End If
                            ''
                            If IsActive And _categorytype = "All" Then
                                Dim strReaction As String = ""
                                Dim strActive As String = ""
                                If dtExam.Rows(i)("sReaction") <> "" Then
                                    Dim arr() As String 'Srting Array
                                    arr = Split(dtExam.Rows(i)("sReaction"), "|")
                                    strReaction = arr.GetValue(0)
                                    If (arr.Length > 1) Then
                                        strActive = arr.GetValue(1)
                                    End If
                                End If
                                Dim strReactions As String = " "
                                Dim dtReaction As DataTable
                                dtReaction = GetAllCategory("Reaction")
                                If IsNothing(dtReaction) = False Then
                                    For k As Int16 = 0 To dtReaction.Rows.Count - 1
                                        strReactions = strReactions & "|" & dtReaction.Rows(k)(1)
                                    Next
                                    dtReaction.Dispose()
                                    dtReaction = Nothing
                                End If
                                ' cStyle = .Styles.Add("Reaction")
                                Try
                                    If (.Styles.Contains("Reaction")) Then
                                        cStyle = .Styles("Reaction")
                                    Else
                                        cStyle = .Styles.Add("Reaction")

                                    End If
                                Catch ex As Exception
                                    cStyle = .Styles.Add("Reaction")

                                End Try
                                cStyle.ComboList = strReactions
                                rgReaction.Style = cStyle
                                rgActive.StyleNew.DataType = GetType(Boolean)
                                rgActive.StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                                rgActive.StyleNew.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.CenterCenter
                                ''Added Rahul for display each Reaction on new line on 20101007
                                Dim arrReaction As String()
                                arrReaction = strReaction.Split(vbNewLine)
                                dgList.Rows(_Row).Height = dgList.Rows.DefaultSize * arrReaction.Length - 1
                                dgList.SetData(_Row, 7, strReaction)
                                '.SetData(_Row, 7, strReaction)
                                ''
                                If strActive = "Active" Then
                                    .SetCellCheck(_Row, 9, C1.Win.C1FlexGrid.CheckEnum.Checked)
                                End If
                                arrReaction = Nothing

                                strActive = Nothing

                                strReaction = Nothing

                                'dtReaction.Dispose()
                                'dtReaction = Nothing

                                strReactions = Nothing
                            ElseIf _categorytype = "Fam" Then

                                Dim strFamily As String = ""
                                Dim strFamilyActive As String = ""
                                If dtExam.Rows(i)("sReaction") <> "" Then
                                    Dim arr() As String 'Srting Array
                                    Dim arr1() As String 'Srting Array
                                    arr = Split(dtExam.Rows(i)("sReaction"), "|")
                                    arr1 = arr(0).Split(":")

                                    If arr1.Length > 0 Then
                                        strFamily = arr1(0)
                                    End If

                                    If (arr.Length > 1) Then
                                        strFamilyActive = arr.GetValue(1)
                                    End If
                                End If

                                ' cStyle = .Styles.Add("Family History")
                                Try
                                    If (.Styles.Contains("Family History")) Then
                                        cStyle = .Styles("Family History")
                                    Else
                                        cStyle = .Styles.Add("Family History")

                                    End If
                                Catch ex As Exception
                                    cStyle = .Styles.Add("Family History")

                                End Try
                                cStyle.ComboList = strFamily
                                rgReaction.Style = cStyle

                                If IsActive Then
                                    rgActive.StyleNew.DataType = GetType(Boolean)
                                    rgActive.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                                    rgActive.StyleNew.ImageAlign = ImageAlignEnum.CenterCenter
                                    If strFamilyActive = "Active" Then
                                        .SetCellCheck(_Row, 9, CheckEnum.Checked)
                                    End If
                                End If

                                Dim arrReaction As String()
                                arrReaction = strFamily.Split(vbNewLine)
                                dgList.Rows(_Row).Height = dgList.Rows.DefaultSize * arrReaction.Length - 1
                                dgList.SetData(_Row, 7, strFamily)

                            ElseIf InStr(dtExam.Rows(i)("sHistoryCategory"), "Smoking", CompareMethod.Text) = 1 Then
                                Dim strSmoking As String = ""
                                Dim strSmokeActive As String = ""
                                If dtExam.Rows(i)("sReaction") <> "" Then
                                    Dim arr() As String 'Srting Array
                                    arr = Split(dtExam.Rows(i)("sReaction"), "|")
                                    strSmoking = arr.GetValue(0)
                                    If (arr.Length > 1) Then
                                        strSmokeActive = arr.GetValue(1)
                                    End If
                                End If
                                Dim strSmokings As String = " "
                                Dim dtSmoking As DataTable
                                dtSmoking = GetAllCategory("Smoking Status Type")
                                If IsNothing(dtSmoking) = False Then
                                    For k As Int16 = 0 To dtSmoking.Rows.Count - 1
                                        strSmokings = strSmokings & "|" & dtSmoking.Rows(k)(1)
                                    Next
                                    dtSmoking.Dispose()
                                    dtSmoking = Nothing

                                End If
                                '  cStyle = .Styles.Add("Smoking Status")
                                Try
                                    If (.Styles.Contains("Smoking Status")) Then
                                        cStyle = .Styles("Smoking Status")
                                    Else
                                        cStyle = .Styles.Add("Smoking Status")

                                    End If
                                Catch ex As Exception
                                    cStyle = .Styles.Add("Smoking Status")

                                End Try
                                cStyle.ComboList = strSmokings
                                rgReaction.Style = cStyle

                                Dim arrSmoking As String()
                                arrSmoking = strSmoking.Split(vbNewLine)

                                dgList.Rows(_Row).Height = dgList.Rows.DefaultSize * arrSmoking.Length - 1
                                dgList.SetData(_Row, 8, strSmoking)
                                strSmokings = Nothing
                                arrSmoking = Nothing

                            ElseIf IsActive Then
                                Dim strReaction As String = ""
                                Dim strActive As String = ""
                                If dtExam.Rows(i)("sReaction") <> "" Then
                                    Dim arr() As String 'Srting Array
                                    arr = Split(dtExam.Rows(i)("sReaction"), "|")
                                    strReaction = arr.GetValue(0)
                                    If (arr.Length > 1) Then
                                        strActive = arr.GetValue(1)
                                    End If

                                    arr = Nothing

                                End If


                                rgActive.StyleNew.DataType = GetType(Boolean)
                                rgActive.StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                                rgActive.StyleNew.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.CenterCenter



                                If strActive = "Active" Then
                                    dgList.SetCellCheck(_Row, 9, C1.Win.C1FlexGrid.CheckEnum.Checked)
                                End If


                                strActive = Nothing

                            End If
                            cStyle = Nothing
                            rgReaction = Nothing
                            rgActive = Nothing
                            dgList.SetData(_Row, 10, dtExam.Rows(i)("nDrugID"))
                            dgList.Row = _Row
                        Next
                    End If
                    FillHistroyCriteria()
                    dgList.EndInit()
                End With
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(dsHistory) Then
                dsHistory.Dispose()
                dsHistory = Nothing
            End If
            If Not IsNothing(dtExam) Then
                dtExam.Dispose()
                dtExam = Nothing
            End If
        End Try

    End Sub


    Public Sub FillHistroyCriteria()

        Dim myCount As Integer = 0
        Dim myArrLst As New ArrayList()
        myArrLst.Clear()
        Try
            If dgList.Rows.Count <= 1 Then
                Exit Sub
            End If


            For iRow As Integer = 1 To dgList.Rows.Count - 1
                ' If C1dgPatientDetails.GetData(iRow, 1).ToString() = "Allergies" Then
                If dgList.GetCellCheck(iRow, 9) = CheckEnum.Unchecked Then
                    dgList.Rows(iRow).Visible = False
                Else
                    dgList.Rows(iRow).Visible = True
                    myArrLst.Add(iRow)
                    myCount = iRow
                End If
                '  End If
            Next


            If myCount = 1 Then
                ''If All Allergies are InActive then don't show the Allergy(Text) Category.
                dgList.Rows(myArrLst.Item(0)).Visible = False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try

    End Sub


    Public Function GetVisitID(ByVal VisitDate As Date, Optional ByVal PatientID As Long = 0) As Long
        Dim con As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim objParam As SqlParameter = Nothing
        Try
            'Call InitialzeCon()
            con = New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)
            cmd = New SqlCommand("gsp_GetVisitID", con)

            objParam = cmd.Parameters.Add("@VisitDate", SqlDbType.DateTime)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = VisitDate

            objParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt, 18)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = PatientID
            cmd.CommandType = CommandType.StoredProcedure
            If con.State <> ConnectionState.Open Then
                con.Open()
            End If
            Dim VisitID As Long
            VisitID = cmd.ExecuteScalar
            con.Close()

            If IsDBNull(VisitID) = True Then
                VisitID = 0
            End If
            Return VisitID


        Catch ex As SqlException
            Return 0
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception

            Return 0
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(objParam) Then
                objParam = Nothing
            End If

            If Not IsNothing(con) Then
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Dispose()
                con = Nothing
            End If


        End Try
    End Function

    Public Function GetAllCategory(ByVal CategoryType As String) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Dim con As SqlConnection = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Try
            con = New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)
            cmd = New SqlCommand("gsp_FillCategory_Mst", con)
            cmd.CommandType = CommandType.StoredProcedure

            con.Open()
            sqlParam = New SqlParameter
            sqlParam = cmd.Parameters.Add("@CategoryType", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = CategoryType
            sqlParam = cmd.Parameters.Add("@flag", SqlDbType.Int)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = 1


            da = New SqlDataAdapter
            da.SelectCommand = cmd
            dt = New DataTable
            da.Fill(dt)
            con.Close()
            Return dt

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient History", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return dt
        Finally
            'If IsNothing(dt) = False Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If IsNothing(da) = False Then
                da.Dispose()
                da = Nothing
            End If
            If IsNothing(con) = False Then
                con.Dispose()
                con = Nothing
            End If
            If IsNothing(sqlParam) = False Then
                sqlParam = Nothing
            End If
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function

#End Region

#Region "Labs"

    Public Sub FillLabs()
        Try
            Dim _FromDate As DateTime = Now
            Dim _ToDate As DateTime = Now
            Dim _Criteria As String = ""
            dgList.Visible = False
            If Not IsNothing(_clsUCLabControl) Then
                pnlList.Controls.Add(_clsUCLabControl)
                _clsUCLabControl.Visible = True
                _clsUCLabControl.Dock = DockStyle.Fill
                _clsUCLabControl.LoadPreviousLabs(_PatientID, DateTime.Now, True) '.ToString("MM/dd/yyyy hh:mm:ss"), True)
                ''Added on 20100119-To set values according to selection
                If _Criteria <> "" Then
                    _clsUCLabControl.cmbCriteria.Text = _Criteria
                    _clsUCLabControl.dtpFrom.Value = _FromDate
                    _clsUCLabControl.dtpToDate.Value = _ToDate
                Else
                    _clsUCLabControl.cmbCriteria.Text = "Date"
                    _clsUCLabControl.dtpFrom.Value = _FromDate
                    _clsUCLabControl.dtpToDate.Value = _ToDate
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "DMS"

    Public Function GetDMSConnectionString(ByVal strSQLServerName As String, ByVal strDatabase As String, ByVal isSQLAuthentication As Boolean, ByVal sUserName As String, ByVal sPassword As String) As String
        Dim strConnectionString As String
        If isSQLAuthentication = False Then
            strConnectionString = "SERVER=" & strSQLServerName & ";DATABASE=" & strDatabase & ";Integrated Security=SSPI"
        Else
            strConnectionString = "SERVER=" & strSQLServerName & ";DATABASE=" & strDatabase & ";User ID=" & sUserName & ";Password=" & sPassword & ""
        End If
        Return strConnectionString
    End Function

    Public Function GetDMSConnectionString() As String
        Return GetDMSConnectionString(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsServerName, gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsDatabaseName, gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsIsSqlAuthentication, gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsUserId, gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsPassword)
    End Function

    Public Sub FillDMS()
        Dim con As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dtExam As DataTable = Nothing
        Try

            Dim strSQL As String = "SELECT eDocument_Details_V3.eDocumentID,eDocument_Details_V3.DocumentName AS DocumentName, " +
                     "eDocument_Details_V3.Category AS Category, eDocument_Details_V3.IsAcknowledge AS IsAcknowledge, " +
                     "eDocument_Details_V3.HasNote AS HasNote, eDocument_Details_V3.ClinicID, CreatedDateTime, ModifiedDateTime, " +
                     "eDocument_Container_V3.eContainerID " +
                     "FROM eDocument_Details_V3 WITH(NOLOCK) inner join eDocument_Container_V3 on " +
                     "eDocument_Details_V3.eDocumentID=eDocument_Container_V3.eDocumentID " +
                     "WHERE eDocument_Details_V3.PatientID = " & _PatientID & " " +
                     "AND eDocument_Details_V3.ClinicID = " & _clinicID & " AND eDocument_Details_V3.DocumentName IS NOT NULL " +
                     "ORDER BY eDocument_Details_V3.Category, CreatedDateTime desc"

            con = New SqlConnection(GetDMSConnectionString())
            cmd = New SqlCommand(strSQL, con)
            con.Open()
            da = New SqlDataAdapter(cmd)
            dtExam = New DataTable
            da.Fill(dtExam)
            con.Close()

            dgList.DataSource = Nothing

            dgList.Cols.Count = 6
            dgList.Cols(0).Width = 0
            dgList.Cols(1).Width = dgList.Width / 3
            dgList.Cols(1).Caption = "Category"
            dgList.Cols(2).Caption = "Document Name"
            dgList.Cols(2).Width = dgList.Width / 3
            dgList.Cols(3).Width = dgList.Width / 5
            dgList.Cols(4).Width = dgList.Width / 5
            dgList.Cols(5).Width = 0

            dgList.Cols(1).TextAlignFixed = TextAlignEnum.LeftCenter
            dgList.Cols(2).TextAlignFixed = TextAlignEnum.LeftCenter
            dgList.Cols(3).TextAlignFixed = TextAlignEnum.LeftCenter
            dgList.Cols(4).TextAlignFixed = TextAlignEnum.LeftCenter
            dgList.Cols(5).TextAlignFixed = TextAlignEnum.LeftCenter

            dgList.Cols(1).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            dgList.Cols(2).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            dgList.Cols(3).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            dgList.Cols(4).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            dgList.Cols(5).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter



            For k As Integer = 0 To dtExam.Rows.Count - 1
                dgList.Rows.Add()
                dgList.SetData(dgList.Rows.Count - 1, 0, dtExam.Rows(k)("eDocumentID"))
                dgList.SetData(dgList.Rows.Count - 1, 1, dtExam.Rows(k)("Category"))
                dgList.SetData(dgList.Rows.Count - 1, 2, dtExam.Rows(k)("DocumentName"))

                If Convert.ToBoolean(dtExam.Rows(k)("HasNote")) Then
                    dgList.SetCellImage(dgList.Rows.Count - 1, 4, ImageList1.Images(1))
                    dgList.Cols(4).ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.CenterCenter
                End If
                If Convert.ToBoolean(dtExam.Rows(k)("IsAcknowledge")) Then
                    dgList.SetCellImage(dgList.Rows.Count - 1, 3, ImageList1.Images(0))
                    dgList.Cols(3).ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.CenterCenter
                Else
                    dgList.SetCellImage(dgList.Rows.Count - 1, 3, Nothing)
                    dgList.Cols(3).ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.CenterCenter
                End If

                dgList.SetData(dgList.Rows.Count - 1, 5, dtExam.Rows(k)("eContainerID"))


            Next

            'If Convert.ToBoolean(dtExam("HasNote")) Then
            '    .SetCellImage(.Rows.Count - 1, COL_View_NOTEFLAG, oImage_D.Img_Note.Image)  'Image.FromFile("D:\Vinayak M3XP\gloEMR\gloEMR\bin\Images\DMS\Flag1.JPG"))
            'End If
            'If oDocuments(k).IsAcknowledge = True Then
            '    .SetData(.Rows.Count - 1, COL_D_CAT_ISREVIWED, 1)
            '    .SetCellImage(.Rows.Count - 1, COL_View_REVIWEDFLAG, oImage_D.Img_Reviwed.Image)  'Image.FromFile("D:\Vinayak M3XP\gloEMR\gloEMR\bin\Images\DMS\Flag1.JPG"))
            '    .Cols(COL_View_REVIWEDFLAG).ImageAlign = ImageAlignEnum.CenterCenter
            'Else
            '    .SetData(.Rows.Count - 1, COL_D_CAT_ISREVIWED, 0)
            '    .SetCellImage(.Rows.Count - 1, COL_View_REVIWEDFLAG, oImage_D.Img_Blanck.Image)  'Image.FromFile("D:\Vinayak M3XP\gloEMR\gloEMR\bin\Images\DMS\Flag1.JPG"))
            '    .Cols(COL_View_REVIWEDFLAG).ImageAlign = ImageAlignEnum.CenterCenter
            'End If         

            '30-Jan-15 Aniket: Resolving Bug #77774: Field: splits screen issue
        Catch ex As SqlException
            MessageBox.Show("Cannot access DMS database. Please check DMS database settings in gloEMR Admin.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(dtExam) Then
                dtExam.Dispose()
                dtExam = Nothing
            End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(con) Then
                con.Dispose()
                con = Nothing
            End If
        End Try
    End Sub
    Dim oPDFDoc As pdftron.PDF.PDFDoc
    Dim oPDFView As pdftron.PDF.PDFViewCtrl
    Dim oProcessLabel As Label = Nothing
    Public Sub Fill_DMS()
        Try
            pnlData.ForeColor = Color.White


            Dim _DocumentLoadedFromDatabase As Boolean = False
            Dim grdIndex As Integer = dgList.Row
            Dim oldDoc As pdftron.PDF.PDFDoc = Nothing
            If Not IsNothing(oPDFView) Then
                If pnlData.Controls.Contains(oPDFView) Then
                    pnlData.Controls.Remove(oPDFView)
                End If
                oldDoc = oPDFView.GetDoc()

                oPDFView.Dispose()
                oPDFView = Nothing
                If Not IsNothing(oldDoc) Then
                    oldDoc.Dispose()
                    oldDoc = Nothing
                End If

            End If
            If (IsNothing(oProcessLabel) = False) Then
                If pnlData.Controls.Contains(oProcessLabel) Then
                    pnlData.Controls.Remove(oProcessLabel)

                End If
                oProcessLabel.Dispose()
                oProcessLabel = Nothing
            End If
            oProcessLabel = New Label()
            pnlData.Controls.Add(oProcessLabel)
            oProcessLabel.Dock = DockStyle.Fill
            oProcessLabel.Location = New Point(0, 0)
            ' oProcessLabel.ForeColor = Color.Blue
            oProcessLabel.Font = New System.Drawing.Font("Verdana", 30.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (Convert.ToByte(0)))
            oProcessLabel.TextAlign = ContentAlignment.MiddleCenter
            oProcessLabel.Text = "Please wait !!!"
            oProcessLabel.Name = "lblProcess"
            oProcessLabel.Visible = True
            oProcessLabel.BringToFront()



            If Not Directory.Exists(StrFileInitialPath) Then
                Directory.CreateDirectory(StrFileInitialPath)
            End If

            Dim FilePath As String = StrFileInitialPath + getUniqueID() + ".pdf"
            PrevFileFullName = CurrFileFullName
            CurrFileFullName = FilePath
            _clsDMS.GetContainerStream(Convert.ToInt64(dgList.Item(grdIndex, 0)), Convert.ToInt64(dgList.Item(grdIndex, 5)), clinicID, FilePath)
            _DocumentLoadedFromDatabase = System.IO.File.Exists(FilePath)

            If IsNothing(oPDFView) Then
                oPDFView = New pdftron.PDF.PDFViewCtrl()
            End If
            If Not IsNothing(oPDFDoc) Then
                oPDFDoc.Close()
                oPDFDoc.Dispose()
                oPDFDoc = Nothing
            End If

            'Dim oldDoc As pdftron.PDF.PDFDoc = oPDFView.GetDoc()
            oPDFDoc = New pdftron.PDF.PDFDoc(FilePath)

            If IsNothing(oPDFDoc) = False Then 'SLR: 7/16/2014 Please check I had changed it to reverse
                If (oPDFDoc.IsModified()) Then
                    oPDFDoc.Save(FilePath, 0)
                End If
            End If

            'If IsNothing(oPDFView) Then
            '    oPDFView = New pdftron.PDF.PDFViewCtrl()
            'End If
            If Not IsNothing(oPDFDoc) Then
                If Not IsNothing(oPDFView) Then
                    oPDFView.Show()
                    oPDFView.SetDoc(oPDFDoc)
                End If
            End If


            'If pnlData.Controls.Contains(oPDFView) Then
            '    pnlData.Controls.Remove(oPDFView)
            'End If

            pnlData.Controls.Add(oPDFView)
            oPDFView.Location = New Point(0, 0)
            'Bug Id:40835
            oPDFView.EnableInteractiveForms(False)
            oPDFView.ShowNavToolbar(True)
            oPDFView.Dock = DockStyle.Fill
            oPDFView.BringToFront()
            oPDFView.SetPagePresentationMode(pdftron.PDF.PDFViewCtrl.PagePresentationMode.e_single_page)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If File.Exists(PrevFileFullName) Then
                File.Delete(PrevFileFullName)
            End If
        End Try

    End Sub

#End Region


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
    'Public Enum enumControls
    '    None = 0
    '    FormFieldControl = 1
    '    ContentControl = 2
    '    Others = 3
    'End Enum
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

    Private Sub LoadWordUserControl(ByVal strFileName As String, Optional ByVal blnGetData As Boolean = False)

        '  wdSplitControl.Open(strFileName)
        gloWord.LoadAndCloseWord.OpenDSO(wdSplitControl, strFileName, oCurDoc1_SplitControl, oWordApp_SplitControl)
        If blnGetData Then
            ''//To retrieve the Form fields for the Word document
            objCriteria.DocCategory = enumDocCategory.Others
            objCriteria.PatientID = _PatientID
            objCriteria.PrimaryID = 0
            ObjWord.DocumentCriteria = objCriteria
            ObjWord.CurDocument = oCurDoc1_SplitControl
            ''Replace Form fields with Concerned data
            ObjWord.GetFormFieldData(enumDocType.None)
            oCurDoc1_SplitControl = ObjWord.CurDocument
            oCurDoc1_SplitControl.ActiveWindow.View.ShowFieldCodes = False

        Else
            oCurDoc1_SplitControl = wdSplitControl.ActiveDocument
            ObjWord.CurDocument = oCurDoc1_SplitControl
            ObjWord.HighlightColor()
            oCurDoc1_SplitControl = ObjWord.CurDocument
            oCurDoc1_SplitControl.ActiveWindow.View.ShowFieldCodes = False

        End If

        SetWordObjectView()
    End Sub

    Private Sub dgList_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgList.DoubleClick
        Try
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            dgList.Enabled = False
            RemoveHandler dgList.DoubleClick, AddressOf dgList_DoubleClick
            Dim grdIndex As Integer = dgList.Row ' 0 ' dgList.CurrentRowIndex
            If grdIndex > -1 Then
                Select Case FromForms
                    Case "PatientExam"
                        If dgList.Row <> -1 Then
                            FillPastExamContents(dgList.Item(dgList.Row, 0))
                        End If
                    Case "PatientLetter"
                        Fill_PatientLetter()
                    Case "PatientMessages"
                        Fill_PatientMessages()
                    Case "NurseNotes"
                        Fill_NursesNote()
                    Case "DMS"
                        Fill_DMS()
                    Case "Orders"
                        Fill_Orders()
                End Select
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK)
        Finally
            AddHandler dgList.DoubleClick, AddressOf dgList_DoubleClick
            dgList.Enabled = True
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        End Try
    End Sub


    Private Sub wdPastMessages_OnDocumentClosed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles wdSplitControl.OnDocumentClosed
        Dim docPath As String = PrevFileFullName 'wdPastMessages.DocumentFullName
        wdSplitControl.Close()
        If File.Exists(docPath) Then
            File.Delete(docPath)
        End If
        Try
            If Not oCurDoc1_SplitControl Is Nothing Then
                Marshal.ReleaseComObject(oCurDoc1_SplitControl)

                oCurDoc1_SplitControl = Nothing
            End If
            If Not oWordApp_SplitControl Is Nothing Then

                ' Ujwala - as on 17112014 - Commented below line and added GC to resolve issue - Bug: 75788 - gloEMR: Exam - Data dictionary are not working once user switch from past exam or split screen
                'Marshal.FinalReleaseComObject(oWordApp_SplitControl)
                oWordApp_SplitControl = Nothing
                'GC.Collect()
                'GC.WaitForPendingFinalizers()
                ' Ujwala - as on 17112014 - Commented below line and added GC to resolve issue - Bug: 75788 - gloEMR: Exam - Data dictionary are not working once user switch from past exam or split screen
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub wdPastMessages_BeforeDocumentClosed(ByVal sender As System.Object, ByVal e As AxDSOFramer._DFramerCtlEvents_BeforeDocumentClosedEvent) Handles wdSplitControl.BeforeDocumentClosed
        Try
            If (IsNothing(oWordApp_SplitControl)) Then
                Try
                    oWordApp_SplitControl = oCurDoc1_SplitControl.Application
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK)
                End Try
            End If

            If Not oWordApp_SplitControl Is Nothing Then
                For Each oFile As Wdoc.RecentFile In oWordApp_SplitControl.RecentFiles
                    If (IsNothing(oFile) = False) Then
                        Try
                            If oFile.Path = gloSettings.FolderSettings.AppTempFolderPath Then
                                Try
                                    oFile.Delete()
                                Catch ex As Exception
                                    MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK)
                                    ex = Nothing
                                End Try
                            End If
                        Catch ex As Exception
                            
                        End Try
                    End If
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub dgList_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgList.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip2, sender, e.Location)
    End Sub


    Private Sub gloUC_PastWordNotes_SplitControl_Disposed(sender As Object, e As System.EventArgs) Handles Me.Disposed
        oCurDoc1_SplitControl = Nothing
        oTempDoc_SplitControl = Nothing
        oWordApp_SplitControl = Nothing

        CurrFileFullName = Nothing
        PrevFileFullName = Nothing
        StrFileInitialPath = Nothing
        _PatientID = Nothing

        _visitID = Nothing
        _clinicID = Nothing
        _blnShowSmokingCol = Nothing
        _ObjWord = Nothing
        _objCriteria = Nothing

        If IsNothing(_clsPatientExams) = False Then
            _clsPatientExams.Dispose()
            _clsPatientExams = Nothing
        End If

        If IsNothing(_clsPatientLetters) = False Then
            '_clsPatientLetters.Dispose()
            _clsPatientLetters = Nothing
        End If

        If IsNothing(_clsPatientMessages) = False Then
            _clsPatientMessages.Dispose()
            _clsPatientMessages = Nothing
        End If

        If IsNothing(_clsNurseNotes) = False Then
            '_clsNurseNotes.Dispose()
            _clsNurseNotes = Nothing
        End If

        If IsNothing(_clsRxmed) = False Then
            _clsRxmed.Dispose()
            _clsRxmed = Nothing
        End If

        If IsNothing(_clsHistory) = False Then
            '_clsHistory.Dispose()
            _clsHistory = Nothing
        End If

        If IsNothing(_clsLabs) = False Then
            '_clsLabs.Dispose()
            _clsLabs = Nothing
        End If

        If IsNothing(_clsDMS) = False Then
            _clsDMS.Dispose()
            _clsDMS = Nothing
        End If

        If IsNothing(_clsOrders) = False Then
            _clsOrders.Dispose()
            _clsOrders = Nothing
        End If


        If IsNothing(_clsProblemList) = False Then
            _clsProblemList.Dispose()
            _clsProblemList = Nothing
        End If


        If IsNothing(_clsUCLabControl) = False Then
            _clsUCLabControl.Dispose()
            _clsUCLabControl = Nothing
        End If


        _LetterID = Nothing
        ExamNewDocumentName = Nothing
        _ShowPast = Nothing
        FromForm = Nothing
        _strFormName = Nothing

    End Sub
End Class


