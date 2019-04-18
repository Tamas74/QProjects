
Imports gloUserControlLibrary
Imports System.Data.SqlClient
Imports C1.Win.C1FlexGrid
Imports gloEMRGeneralLibrary.gloEMRDatabase



Public Class EjectionFraction
    Implements IPatientContext
    Dim _EjectionFractionID As Long = 0
    Dim _PatientID As Long = 0
    Dim _ExamID As Long = 0
    Dim _VisitID As Long = 0
    Dim _ClinicID As Long = 0

    ''''''''''''by Ujwala as on 11232010
    Dim IsDataChanged As Boolean = False
    ''''''''''''by Ujwala as on 11232010
    Dim cStyleModality As C1.Win.C1FlexGrid.CellStyle

    'Dim cl As clsEjectionFraction
    'Dim dt As New DataTable

    '''' Column No
    Dim COL_EJECTION_FRACTIONID As Integer = 0
    Dim COL_PATIENTID As Integer = 1
    Dim COL_EXAMID As Integer = 2
    Dim COL_VISITID As Integer = 3
    Dim COL_CLINICID As Integer = 4
    Dim COL_TEST_DATE As Integer = 5
    Dim COL_MODALITY_TEST As Integer = 6
    Dim COL_QUANTITY_PERCENT As Integer = 7
    Dim COL_QUANTITY As Integer = 8
    Dim COLUMN_COUNT As Int16 = 9
    Private WithEvents objEjectionFractionDBLayer As New ClsEjectionFractionDBLayer
    Private WithEvents gloUC_PatientStrip1 As gloUserControlLibrary.gloUC_PatientStrip = Nothing
    Private WithEvents ogloUC_generalsearch As gloUserControlLibrary.gloUCGeneralSearch

    'Public Sub New()
    '    MyBase.New()

    '    'This call is required by the Windows Form Designer.
    '    InitializeComponent()

    '    'Add any initialization after the InitializeComponent() call

    'End Sub

    Public Sub New(ByVal PatientID As Long, ByVal ExamID As Long, ByVal VisitID As Long)

        MyBase.New()
        ''EjectionFractionID is Zero when EjectionFraction List is Open from Ejection Fraction

        _ClinicID = 1
        ''PatientID  is Zero when Patient list  is Open from Patient list
        _PatientID = PatientID


        ''ExamID is Zero when Exam list  is Open from Exam list
        _ExamID = ExamID

        '' VisitID is Zero when Problem List is Open from Problem List
        _VisitID = VisitID ' 391117437030546801


        '' ClinicID is Zero when Clinic List is Open from Clinic List


        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    Private Sub EjectionFraction_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub EjectionFraction_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            If (IsNothing(gloUC_PatientStrip1) = False) Then
                gloUC_PatientStrip1.Dispose()
                gloUC_PatientStrip1 = Nothing
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Add gloUC_PatientStrip1 into the panel
        gloC1FlexStyle.Style(cfgEjectionFraction)
        Try
            If IsNothing(gloUC_PatientStrip1) = False Then
                Me.Controls.Remove(gloUC_PatientStrip1)
                gloUC_PatientStrip1.Dispose()
                gloUC_PatientStrip1 = Nothing
            End If
            gloUC_PatientStrip1 = New gloUserControlLibrary.gloUC_PatientStrip

            gloUC_PatientStrip1.ShowDetail(_PatientID, gloUC_PatientStrip.enumFormName.None)
            Me.Controls.Add(gloUC_PatientStrip1)
            gloUC_PatientStrip1.Dock = DockStyle.Top
            gloUC_PatientStrip1.BringToFront()
            pnlMain.BringToFront()
            gloUC_PatientStrip1.Padding = New Padding(3, 0, 3, 0)

            'dt = PopulateEjectionFractionList()
            'SetGridStyle(dt)

            'search with UC
            If IsNothing(ogloUC_generalsearch) = False Then
                Panel3.Controls.Remove(ogloUC_generalsearch)
                ogloUC_generalsearch.Dispose()
                ogloUC_generalsearch = Nothing
            End If
            ogloUC_generalsearch = New gloUCGeneralSearch()
            Panel3.Controls.Add(ogloUC_generalsearch)
            ogloUC_generalsearch.Dock = DockStyle.Left
            ogloUC_generalsearch.BringToFront()


            Dim dt As DataTable = PopulateEjectionFractionList()
            SetGridStyle(dt)

            ogloUC_generalsearch.IntialiseDatatable(dt)



        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        Try
            gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    'function for Populating Ejection fraction list
    Private Function PopulateEjectionFractionList() As DataTable

        'Declaration of variables for making connection
        Dim dt As New DataTable
        Dim cmd As SqlCommand
        Dim sqladpt As SqlDataAdapter

        'Connection string
        Dim conn As SqlConnection = New SqlConnection(GetConnectionString)
        '        cmd = New SqlCommand("Select * from CV_EjectionFraction where nPatientID=" & _PatientID & " and nExamID=" & _ExamID & " and nVisitID=" & _VisitID, conn)
        Dim strquery As String = "Select isnull(nEjectionFractionID,0)as EjectionFractionID,isnull(nPatientID,0)as PatientID,isnull(nExamID,0)as ExamID,isnull(nVisitID,0)as VisitID,isnull(nClinicID,0)as ClinicID,isnull(dtDateofTest,0)as TestDate,isnull(sModalityTest,'')as ModalityTest,isnull(sQuantityPercent,0)as QuantityPercent,isnull(sQuantityDesc,0)as QuantityDescription from CV_EjectionFraction  where nPatientID=" & _PatientID & " "
        ''and nVisitID=" & _VisitID
        cmd = New SqlCommand(strquery, conn)
        sqladpt = New SqlDataAdapter(cmd)

        'Fill data adapter
        sqladpt.Fill(dt)
        sqladpt.Dispose()
        sqladpt = Nothing
        conn.Close()
        conn.Dispose()
        conn = Nothing
        If cmd IsNot Nothing Then
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
        End If
        'Return Data table
        Return dt
    End Function

    'Function for insert 

    Private Sub SetGridStyle(ByVal dt As DataTable)
        With cfgEjectionFraction

            .AutoResize = False
            ''Sanjog-Added on 20101208 to set the size of UC
            .ExtendLastCol = False
            .AllowDragging = False
            ''Sanjog-Added on 20101208 to dnt allow the dragging
            Dim i As Int16
            .Dock = DockStyle.Fill
            Dim _TotalWidth As Single = 0
            _TotalWidth = (996 - 20) / 4
            'Dim cStyle As C1.Win.C1FlexGrid.CellStyle

            .Cols.Count = COLUMN_COUNT
            .Rows.Count = 1
            .AllowEditing = True
            .AllowAddNew = True

            .Styles.ClearUnused()

            .Cols(COL_EJECTION_FRACTIONID).Width = .Width * 0
            .Cols(COL_EJECTION_FRACTIONID).AllowEditing = False
            .SetData(0, COL_EJECTION_FRACTIONID, "EjectionFractionID")
            .Cols(COL_EJECTION_FRACTIONID).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(COL_PATIENTID).Width = .Width * 0
            .Cols(COL_PATIENTID).AllowEditing = False
            .SetData(0, COL_PATIENTID, "PatientID")
            .Cols(COL_PATIENTID).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(COL_EXAMID).Width = .Width * 0
            .Cols(COL_EXAMID).AllowEditing = False
            .SetData(0, COL_EXAMID, "ExamID")
            .Cols(COL_EXAMID).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(COL_VISITID).Width = .Width * 0
            .Cols(COL_VISITID).AllowEditing = False
            .SetData(0, COL_VISITID, "VisitID")
            .Cols(COL_VISITID).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(COL_CLINICID).Width = .Width * 0
            .Cols(COL_CLINICID).AllowEditing = False
            .SetData(0, COL_CLINICID, "ClinicID")
            .Cols(COL_CLINICID).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(COL_TEST_DATE).Width = _TotalWidth * 1
            .SetData(0, COL_TEST_DATE, "Date")
            .Cols(COL_TEST_DATE).DataType = GetType(Date)
            .Cols(COL_TEST_DATE).AllowEditing = True

            ''Sanjog-Added on 20101208 to set the size of UC
            .Cols(COL_MODALITY_TEST).Width = _TotalWidth * 1
            ''Sanjog-Added on 20101208 to set the size of UC
            .SetData(0, COL_MODALITY_TEST, "Modality Test")
            .Cols(COL_MODALITY_TEST).AllowEditing = True

            Dim dtResults As DataTable = GetTests()

            Dim strComboString As String = " "
            For icnt As Int32 = 0 To dtResults.Rows.Count - 1
                strComboString = strComboString & "|" & dtResults.Rows(icnt)(0).ToString
            Next

            'Dim rgOperator As C1.Win.C1FlexGrid.CellRange = C1Cardiology.GetCellRange(r.Index, COL_TypeofDevice, r.Index, COL_TypeofDevice)
            '  cStyleModality = .Styles.Add("ModalityTest")
            Try
                If (.Styles.Contains("ModalityTest")) Then
                    cStyleModality = .Styles("ModalityTest")
                Else
                    cStyleModality = .Styles.Add("ModalityTest")

                End If
            Catch ex As Exception
                cStyleModality = .Styles.Add("ModalityTest")

            End Try
            cStyleModality.ComboList = strComboString
            strComboString = ""

            .Cols(COL_QUANTITY_PERCENT).Width = _TotalWidth * 1
            .SetData(0, COL_QUANTITY_PERCENT, "Quantitative value as percent")
            '.Cols(COL_QUANTITY_PERCENT).DataType = GetType(Decimal)
            .Cols(COL_QUANTITY_PERCENT).AllowEditing = True
            .Cols(COL_QUANTITY_PERCENT).TextAlign = TextAlignEnum.LeftCenter

            ''Sanjog-Added on 20101208 to set the size of UC
            .Cols(COL_QUANTITY).Width = _TotalWidth * 1
            ''Sanjog-Added on 20101208 to set the size of UC
            .SetData(0, COL_QUANTITY, "Qualitative value (narrative)")
            .Cols(COL_QUANTITY).AllowEditing = True
            .Cols(COL_QUANTITY).TextAlign = TextAlignEnum.LeftCenter

            '' Table dt Contains following Columns
            '' ProblemID, VisitID , dtDOS, Diagnosis, Complaint ,Status
            For i = 0 To dt.Rows.Count - 1
                .Rows.Add()

                ''''Set Column Style 
                '''' Assinge the Cell for ComboBox
                'Dim rgDia As CellRange = .GetCellRange(.Rows.Count - 1, COL_DIAGNOSIS)
                'rgDia.Style = csDia  '' .Styles.Add("Dia")

                '''' Assinge the Cell for ComboBox
                'Dim rgStatus As CellRange = .GetCellRange(.Rows.Count - 1, COL_STATUS)
                'rgStatus.Style = csStatus ''''  .Styles.Add("Status")

                '' Fill the Retrived information to relative controls
                'dtpDOS.Value = Format(dt.Rows(i)("dtDOS"), "MM/dd/yyyy")

                .SetData(i + 1, COL_EJECTION_FRACTIONID, dt.Rows(i)("EjectionFractionID"))
                .SetData(i + 1, COL_PATIENTID, dt.Rows(i)("PatientID"))
                .SetData(i + 1, COL_EXAMID, dt.Rows(i)("ExamID"))
                .SetData(i + 1, COL_VISITID, dt.Rows(i)("VisitID"))
                .SetData(i + 1, COL_CLINICID, dt.Rows(i)("ClinicID"))

                .SetData(i + 1, COL_TEST_DATE, Format(dt.Rows(i)("TestDate"), "MM/dd/yyyy"))
                .SetCellStyle(i + 1, COL_MODALITY_TEST, cStyleModality)
                .SetData(i + 1, COL_MODALITY_TEST, dt.Rows(i)("ModalityTest"))
                .SetData(i + 1, COL_QUANTITY_PERCENT, dt.Rows(i)("QuantityPercent"))
                .SetData(i + 1, COL_QUANTITY, dt.Rows(i)("QuantityDescription"))

            Next
            If IsNothing(dtResults) = False Then
                dtResults.Dispose()
            End If
            dtResults = Nothing
            .AllowResizing = AllowResizingEnum.None
            .Row = 1
        End With
    End Sub
    Private Function GetTests() As DataTable
        Dim Connection As New SqlConnection(GetConnectionString)
        
        Try
            Connection.Open()
            Dim CommandString As String = "select Distinct sDescription from Category_MST where sCategoryType = 'Cardio Modality Test'"

            Dim adp As New SqlDataAdapter(CommandString, Connection)
            Dim ds As New DataSet
            adp.Fill(ds)
            Dim dt As DataTable = ds.Tables(0).Copy()
            adp.Dispose()
            adp = Nothing
            Connection.Close()
            Connection.Dispose()
            Connection = Nothing

            ds.Dispose()
            ds = Nothing

            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If (IsNothing(Connection) = False) Then
                If Connection.State = ConnectionState.Open Then
                    Connection.Close()
                End If
                Connection.Dispose()
                Connection = Nothing
            End If
         
        End Try
    End Function
    Private Function SaveEjectionFractionList()
        Try
            Dim objEjection As clsEjectionFraction
            Dim Arrlist As New ArrayList
            If (IsNothing(objEjectionFractionDBLayer) = False) Then
                objEjectionFractionDBLayer = Nothing
            End If
            objEjectionFractionDBLayer = New ClsEjectionFractionDBLayer
            With cfgEjectionFraction
                .Select(0, 0, False)
                For i As Int16 = 1 To .Rows.Count - 1
                    If IsNothing(.GetData(i, COL_EJECTION_FRACTIONID)) Then
                        If Not IsNothing(.GetData(i, COL_TEST_DATE)) Then


                            objEjection = New clsEjectionFraction
                            objEjection.ClinicID = 1
                            objEjection.EjectionFractionID = 0
                            objEjection.PatientID = _PatientID
                            objEjection.VisitID = _VisitID
                            objEjection.ExamID = _ExamID

                            .SetData(i, COL_PATIENTID, _PatientID)
                            .SetData(i, COL_VISITID, _VisitID)
                            .SetData(i, COL_EXAMID, _ExamID)
                            .SetData(i, COL_CLINICID, 1)

                            objEjection.TestDate = .GetData(i, COL_TEST_DATE)
                            objEjection.ModalityTest = .GetData(i, COL_MODALITY_TEST) & ""
                            objEjection.QuantityPercent = .GetData(i, COL_QUANTITY_PERCENT)
                            objEjection.QuantityDescription = .GetData(i, COL_QUANTITY) & ""
                            objEjection.RowIndex = i
                            Arrlist.Add(objEjection)
                        End If
                    Else
                        objEjection = New clsEjectionFraction
                        objEjection.ClinicID = 1
                        objEjection.EjectionFractionID = .GetData(i, COL_EJECTION_FRACTIONID)
                        objEjection.PatientID = .GetData(i, COL_PATIENTID)
                        objEjection.VisitID = .GetData(i, COL_VISITID)
                        objEjection.ExamID = .GetData(i, COL_EXAMID)
                        objEjection.TestDate = .GetData(i, COL_TEST_DATE)
                        objEjection.ModalityTest = .GetData(i, COL_MODALITY_TEST) & ""
                        objEjection.QuantityPercent = .GetData(i, COL_QUANTITY_PERCENT)
                        objEjection.QuantityDescription = .GetData(i, COL_QUANTITY) & ""
                        objEjection.RowIndex = i
                        Arrlist.Add(objEjection)
                    End If

                Next

                If Not IsNothing(cfgEjectionFraction) Then
                    If .Rows.Count > 0 Then
                        .RowSel = 0
                    End If
                End If

                objEjectionFractionDBLayer.SaveEjectionFraction(Arrlist)
                objEjectionFractionDBLayer = Nothing
                Arrlist.Clear()
                Arrlist = Nothing

            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return True

    End Function

    Private Sub tsEjectionFraction_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tsEjectionFraction.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Refresh"
                    ''''''''''''by Ujwala as on 11232010                    
                    If IsDataChanged = True Then
                        If MessageBox.Show("This action will lose your Current Changes." & vbCrLf & "Do you want to Proceed anyway?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
                            cfgEjectionFraction.Clear()
                            Dim dt As DataTable = PopulateEjectionFractionList()
                            SetGridStyle(dt)
                            If IsNothing(dt) = False Then
                                dt.Dispose()
                            End If
                            dt = Nothing
                            IsDataChanged = False
                        End If
                    End If
                    ''''''''''''by Ujwala as on 11232010                    
                Case "Delete"
                        DeleteEjectionFraction()
                Case "Save"
                        SaveEjectionFractionList()
                        Me.Close()
                Case "Graph"
                        Dim objfrm As New frmCardioVasGrph(_PatientID, _ExamID, _VisitID)
                        With objfrm

                            .MdiParent = Me.MdiParent

                            ' Me.pnlMainToolBar.Visible = False
                            .WindowState = FormWindowState.Maximized
                            '[ Me.ShowHideMainMenu(False, False)
                            .Show()

                        End With



                Case "Close"
                        Me.Close()
            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub DeleteEjectionFraction()
        Dim cnn As New SqlConnection(GetConnectionString)
        cnn.Open()

        Dim cmd As SqlCommand = Nothing
        ' Dim cnt As Int32
        Dim _strSQL As String = ""

        Try
            Dim intSelectRow1 As Integer = cfgEjectionFraction.Row
            If cfgEjectionFraction.Rows.Count > 0 Then
                Dim ID As Long = CType(cfgEjectionFraction.GetData(intSelectRow1, COL_EJECTION_FRACTIONID), Long)
                If ID = 0 Then
                    cfgEjectionFraction.Rows.Remove(intSelectRow1)
                    ''solving TFS id issue-1861
                    MessageBox.Show("No Ejection Fraction record available for deletion.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ''end
                Else
                    If (MessageBox.Show("Are you sure you want delete the record?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
                        cmd = New SqlCommand
                        cmd.Connection = cnn
                        cmd.CommandType = CommandType.Text
                        _strSQL = "Delete from CV_EjectionFraction where nEjectionFractionID=" & ID & ""
                        cmd.CommandText = _strSQL
                        If ((cmd.ExecuteNonQuery()) > 0) Then
                            cfgEjectionFraction.Rows.Remove(intSelectRow1)

                        End If
                    End If
                End If ''Messagebox If
            End If



        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
            End If
            cnn = Nothing
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

        End Try
    End Sub
    Private Sub objEjectionFractionDBLayer_EjectionFractionAdded(ByVal ID As Long, ByVal RowIndex As Integer) Handles objEjectionFractionDBLayer.EjectionFractionAdded
        Try
            cfgEjectionFraction.SetData(RowIndex, COL_EJECTION_FRACTIONID, ID)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Throw ex
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        'Try
        '    Dim strSearch As String
        '    With txtSearch
        '        If Trim(.Text) <> "" Then
        '            strSearch = Replace(.Text, "'", "''")
        '        Else
        '            strSearch = ""
        '        End If
        '    End With
        '    Dim strNode As String = ""
        '    With cfgEjectionFraction
        '        For i As Int16 = 1 To .Cols.Count - 1
        '            If cfgEjectionFraction.Cols.Item(i).Width > 0 Then
        '                strNode = ""
        '                strNode = UCase(.GetData(i, .Cols.Item(i).Name).ToString.Trim)
        '                If InStr(strNode, UCase(strSearch.Trim), CompareMethod.Text) > 0 Then
        '                    .Row = i
        '                    Exit Sub
        '                End If
        '            End If

        '        Next
        '    End With
        'Catch ex As Exception
        '    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        'End Try
    End Sub
    Private Sub ogloUC_generalsearch_AfterTextSearch(ByVal dv As System.Data.DataView, ByVal sScarchString As String) Handles ogloUC_generalsearch.AfterTextSearch

        Dim iCount As Integer
        Dim iFindRowSel As Integer

        Try

            If sScarchString <> "" Then

                If sScarchString <> "" Then

                    For iCount = COL_TEST_DATE To COL_QUANTITY

                        iFindRowSel = cfgEjectionFraction.FindRow(sScarchString, 1, iCount, False, False, True)

                        If Not iFindRowSel < 0 Then
                            Exit For
                        End If

                    Next

                    If iFindRowSel < 0 Then
                        cfgEjectionFraction.Row = 1
                    Else
                        cfgEjectionFraction.Row = iFindRowSel
                    End If
                End If
            Else
                cfgEjectionFraction.Row = 1
            End If

            ' ''If Not IsNothing(dv) Then
            ' ''    SetGridStyle(dv.ToTable)
            ' ''Else
            ' ''    'SetGridStyle(dt)
            ' ''    Dim strSearch As String
            ' ''    With ogloUC_generalsearch.SearchString
            ' ''        If Trim(ogloUC_generalsearch.SearchString) <> "" Then
            ' ''            strSearch = Replace(ogloUC_generalsearch.SearchString, "'", "''")
            ' ''        Else
            ' ''            strSearch = ""
            ' ''        End If
            ' ''    End With
            ' ''    Dim strNode As String = ""
            ' ''    With cfgEjectionFraction
            ' ''        For i As Int16 = 1 To .Cols.Count - 1
            ' ''            If cfgEjectionFraction.Cols.Item(i).Width > 0 Then
            ' ''                strNode = ""
            ' ''                strNode = UCase(.GetData(i, .Cols.Item(i).Name).ToString.Trim)
            ' ''                If InStr(strNode, UCase(strSearch.Trim), CompareMethod.Text) > 0 Then
            ' ''                    .Row = i
            ' ''                    Exit Sub
            ' ''                End If
            ' ''            End If

            ' ''        Next
            ' ''    End With
            ' ''End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cfgEjectionFraction_AfterAddRow(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles cfgEjectionFraction.AfterAddRow
        Try
            cfgEjectionFraction.SetCellStyle(e.Row, COL_MODALITY_TEST, cStyleModality)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            '  MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cfgEjectionFraction_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cfgEjectionFraction.MouseMove
        Try
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
        Catch ex As Exception
            ex = Nothing
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        'SHUBHANGI 20091006
        'Use to clear search text box
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub

    Private Sub Label9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label9.Click

    End Sub

    Private Sub cfgEjectionFraction_AfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles cfgEjectionFraction.AfterEdit
        ''''''''''''by Ujwala as on 11232010
        IsDataChanged = True
        ''''''''''''by Ujwala as on 11232010
    End Sub
    ''' <summary>
    ''' Property writen for Patient Context
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return _PatientID  'Curent patient variable(Local variable) for this module 
        End Get
    End Property
End Class
'Private Sub SetGridStyle(ByVal dt As DataTable)
'          '        

'       dth = _TotalWidth * 1.0
'        .SetData(0, COL_EXAMNAMEBUTTON, "Open Exam")
'        '''' 
'        If blnOpenFromExam Then
'            .Cols(COL_EXAMNAMEBUTTON).Width = Width * 0
'        End If
'        Dim strStatus As String

'        '  Dim strPresc As String = " "

'        strStatus = "Resolved" & "|" & "Active" & "|" & "Inactive" & "|" & "Chronic"
'        Dim csStatus As CellStyle = .Styles.Add("Status")
'        '' Fill Values In ComboBox
'        csStatus.ComboList = strStatus
'        .Cols(COL_STATUS).Style = csStatus

'        Fill_Diagnosis(_VisitID)
'        Dim csDia As CellStyle = .Styles.Add("Dia")
'        '' Fill Values In ComboBox
'        csDia.ComboList = strDia
'        .Cols(COL_DIAGNOSIS).Style = csDia

'        Dim csTempRx As CellStyle = .Styles.Add("TempRx")
'        csTempRx.ComboList = ""
'        .Cols(COL_PRESCRIPTION).Style = csTempRx


'        cStyle = .Styles.Add("BubbleValues")
'        cStyle.ComboList = "..."
'        .Cols(COL_DIAGNOSISBUTTON).Style = cStyle

'        cStyle = .Styles.Add("BubbleValues")
'        cStyle.ComboList = "..."
'        .Cols(COL_RxBUTTON).Style = cStyle

'        .Cols(COL_COMPLAINTS).Style = Nothing

'        cStyle = .Styles.Add("BubbleValues")
'        cStyle.ComboList = "..."
'        .Cols(COL_EXAMNAMEBUTTON).Style = cStyle
'        ''
'        'dtpDOS.Value = Format(GetVisitdate(_VisitID), "MM/dd/yyyy")

'        '' Table dt Contains following Columns
'        '' ProblemID, VisitID , dtDOS, Diagnosis, Complaint ,Status
'        For i = 0 To dt.Rows.Count - 1
'            .Rows.Add()

'            ''''Set Column Style 
'            '''' Assinge the Cell for ComboBox
'            'Dim rgDia As CellRange = .GetCellRange(.Rows.Count - 1, COL_DIAGNOSIS)
'            'rgDia.Style = csDia  '' .Styles.Add("Dia")

'            '''' Assinge the Cell for ComboBox
'            'Dim rgStatus As CellRange = .GetCellRange(.Rows.Count - 1, COL_STATUS)
'            'rgStatus.Style = csStatus ''''  .Styles.Add("Status")

'            '' Fill the Retrived information to relative controls
'            'dtpDOS.Value = Format(dt.Rows(i)("dtDOS"), "MM/dd/yyyy")

'            .SetData(i + 1, COL_VISITID, dt.Rows(i)("VisitID"))
'            .SetData(i + 1, COL_DOS, Format(dt.Rows(i)("dtDOS"), "MM/dd/yyyy"))
'            .SetData(i + 1, COL_PROBLEMID, dt.Rows(i)("ProblemID"))
'            .SetData(i + 1, COL_COMPLAINTS, dt.Rows(i)("Complaint"))
'            .SetData(i + 1, COL_DIAGNOSIS, dt.Rows(i)("Diagnosis"))
'            '' To set the values in Rx Combolist
'            Dim csPresc As CellStyle = .Styles.Add("Rx" & i + 1)
'            '' Fill Values In ComboBox
'            If Not IsDBNull(dt.Rows(i)("Prescription")) Then
'                strRx = dt.Rows(i)("Prescription").ToString
'                strRx = strRx.Replace(",", "|")
'            End If
'            csPresc.ComboList = strRx
'            .Cols(COL_PRESCRIPTION).Style = csPresc

'            ' .SetData(i + 1, COL_PRESCRIPTION, dt.Rows(i)("Prescription"))
'            Dim cR As C1.Win.C1FlexGrid.CellRange = .GetCellRange(i + 1, COL_PRESCRIPTION)
'            cR.Style = csPresc

'            If dt.Rows(i)("Status") = Status.Inactive Then
'                .SetData(i + 1, COL_STATUS, "Inactive")
'                'ElseIf dt.Rows(i)("Status") = Status.Pending Or dt.Rows(i)("Status") = Status.Active Then
'            ElseIf dt.Rows(i)("Status") = Status.Active Then
'                .SetData(i + 1, COL_STATUS, "Active")
'            ElseIf dt.Rows(i)("Status") = Status.Resolved Then
'                .SetData(i + 1, COL_STATUS, "Resolved")
'            ElseIf dt.Rows(i)("Status") = Status.Chronic Then
'                .SetData(i + 1, COL_STATUS, "Chronic")
'            End If

'            '''' By Mahesh, 20070317
'            .SetData(i + 1, COL_USER, dt.Rows(i)("nUserID"))

'            '' By Mahesh, 20070326 
'            ''  SET Diagnosis Comment Button
'            .SetData(i + 1, COL_DIAGNOSISBUTTON, "")
'            Dim rgDig As C1.Win.C1FlexGrid.CellRange = .GetCellRange(i + 1, COL_DIAGNOSISBUTTON, i + 1, COL_DIAGNOSISBUTTON)
'            rgDig.Style = cStyle

'            .SetData(i + 1, COL_RxBUTTON, "")
'            Dim rgRx As C1.Win.C1FlexGrid.CellRange = .GetCellRange(i + 1, COL_RxBUTTON, i + 1, COL_RxBUTTON)
'            rgRx.Style = cStyle

'        Next
'        '.Cols(COL_DIAGNOSIS).AllowEditing = False
'        '.Cols(COL_STATUS).AllowEditing = False
'    End With
'End Sub