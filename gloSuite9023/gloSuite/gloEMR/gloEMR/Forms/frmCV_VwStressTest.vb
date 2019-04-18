Imports C1.Win.C1FlexGrid

Public Class frmCV_VwStressTest
    Implements IPatientContext
    Dim mPatientID As Long = 0
    Dim mStressID As Long = 0
    Dim mVisitID As Long = 0
    Dim mExamID As Long = 0
    Dim mClinicID As Long = 0
    Dim mDateofStudy As Date

    Dim COL_StressID As Integer = 0
    Dim COL_PatientID As Integer = 1
    Dim COL_ExamID As Integer = 2
    Dim COL_VisitID As Integer = 3
    Dim COL_ClinicID As Integer = 4
    Dim COL_DateofStudy As Integer = 5
    Dim COL_CPTCODE As Integer = 6
    Dim COL_TestType = 7
    Dim COL_PHYSICIAN As Integer = 8
    Dim COL_RestingHeartRate As Integer = 9
    Dim COL_PeakHeartRate As Integer = 10
    Dim COL_NarrativeSummary As Integer = 11
    Dim COL_Result As Integer = 12
    Dim COL_TotalExerciseTime As Integer = 13
    Dim COL_EjectionFraction As Integer = 14
    Dim COL_DateofStudyInvisible As Integer = 15
    Dim COL_PARENT As Integer = 16
    Dim COL_IDENTITY As Integer = 17

    Dim COLUMN_COUNT As Integer = 18



    Public Sub New(ByVal PatientID As Long)
        MyBase.New()

        '' ProblemID is Zero when Problem List is Open from Patient Exam

        mPatientID = PatientID

        mClinicID = 1

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    Private Sub SetGridStyle()
        ''Declare a variable
        ''Dim cStyle As C1.Win.C1FlexGrid.CellStyle

        'Dim struser As String
        With C1CV_StressTest
            ''.Clear(ClearFlags.All)
            'Dim i As Int16
            .Dock = DockStyle.Fill

            ''Dim _TotalWidth As Single = 0
            ''_TotalWidth = (.Width - 20) / 3

            .Cols.Count = COLUMN_COUNT '' COLUMN_COUNT
            .Rows.Fixed = 1
            .Rows.Count = 1
            .AllowEditing = False
            .AllowAddNew = False

            .Styles.ClearUnused()

            .Cols(COL_StressID).Width = .Width * 0
            .Cols(COL_StressID).AllowEditing = False
            .SetData(0, COL_StressID, "Stress ID")
            .Cols(COL_StressID).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_PatientID).Width = .Width * 0
            .Cols(COL_PatientID).AllowEditing = False
            .SetData(0, COL_PatientID, "Patient ID")
            .Cols(COL_PatientID).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_ExamID).Width = .Width * 0
            .Cols(COL_ExamID).AllowEditing = False
            .SetData(0, COL_ExamID, "Exam ID")
            .Cols(COL_ExamID).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_VisitID).Width = .Width * 0
            .Cols(COL_VisitID).AllowEditing = False
            .SetData(0, COL_VisitID, "Visit ID")
            .Cols(COL_VisitID).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_ClinicID).Width = .Width * 0
            .Cols(COL_ClinicID).AllowEditing = False
            .SetData(0, COL_ClinicID, "Clinic ID")
            .Cols(COL_ClinicID).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_DateofStudy).Width = .Width * 1.3
            .Cols(COL_DateofStudy).AllowEditing = False
            .SetData(0, COL_DateofStudy, "Date Of Study")
            .Cols(COL_DateofStudy).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_CPTCODE).Width = .Width * 0
            .Cols(COL_CPTCODE).AllowEditing = False
            .SetData(0, COL_CPTCODE, "CPT Code")
            .Cols(COL_CPTCODE).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_TestType).Width = .Width * 0
            .Cols(COL_TestType).AllowEditing = False
            .SetData(0, COL_TestType, "Test Type")
            .Cols(COL_TestType).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_PHYSICIAN).Width = .Width * 0
            .Cols(COL_PHYSICIAN).AllowEditing = False
            .SetData(0, COL_PHYSICIAN, "Physician")
            .Cols(COL_PHYSICIAN).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_RestingHeartRate).Width = .Width * 0
            .Cols(COL_RestingHeartRate).AllowEditing = False
            .SetData(0, COL_RestingHeartRate, "Resting Heart Rate")
            .Cols(COL_RestingHeartRate).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_PeakHeartRate).Width = .Width * 0
            .Cols(COL_PeakHeartRate).AllowEditing = False
            .SetData(0, COL_PeakHeartRate, "Peak Heart Rate")
            .Cols(COL_PeakHeartRate).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_NarrativeSummary).Width = .Width * 0
            .Cols(COL_NarrativeSummary).AllowEditing = False
            .SetData(0, COL_NarrativeSummary, "Narrative Summary")
            .Cols(COL_NarrativeSummary).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_Result).Width = .Width * 0
            .Cols(COL_Result).AllowEditing = False
            .SetData(0, COL_Result, "Result")
            .Cols(COL_Result).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_TotalExerciseTime).Width = .Width * 0
            .Cols(COL_TotalExerciseTime).AllowEditing = False
            .SetData(0, COL_TotalExerciseTime, "Total Exercise Time")
            .Cols(COL_TotalExerciseTime).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_EjectionFraction).Width = .Width * 0
            .Cols(COL_EjectionFraction).AllowEditing = False
            .SetData(0, COL_EjectionFraction, "Ejection Fraction")
            .Cols(COL_EjectionFraction).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_DateofStudyInvisible).Width = 0
            .SetData(0, COL_DateofStudyInvisible, "Date of Study Invisible")
            .Cols(COL_DateofStudyInvisible).DataType = GetType(String)
            .Cols(COL_DateofStudyInvisible).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_PARENT).Width = .Width * 0
            .Cols(COL_PARENT).AllowEditing = False
            .SetData(0, COL_PARENT, "PARENT")
            .Cols(COL_PARENT).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_IDENTITY).Width = .Width * 0
            .Cols(COL_IDENTITY).AllowEditing = False
            .SetData(0, COL_IDENTITY, "IDENTITY")
            .Cols(COL_IDENTITY).TextAlignFixed = TextAlignEnum.LeftCenter

        End With
    End Sub

    Private Sub FillStressTest()
        Try

            Dim _Row As Integer

            ''set properties of treeview in flexgrid
            With C1CV_StressTest
                .Tree.Column = COL_DateofStudy
                .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
                .Tree.LineStyle = Drawing2D.DashStyle.Solid
                .Tree.Indent = 15
                .Cols(COL_DateofStudy).TextAlign = TextAlignEnum.LeftCenter
            End With

            Dim dtStudyDate As DataTable
            Dim dtCPT As DataTable
            Dim dtTestType As DataTable
            Dim dtUsers As DataTable
            Dim dtRestingHeartRate As DataTable
            Dim dtPeakHeartRate As DataTable
            Dim dtNarrativeSummary As DataTable
            'Dim dtResult As DataTable
            Dim dtTotalExerciseTime As DataTable
            Dim dtEjectionFraction As DataTable

            Dim mDOS As Int16
            Dim mCPT As Int16
            Dim mTestType As Int16
            Dim mPhysician As Int16
            Dim mRestingHeartRate As Int16
            Dim mPeakHeartRate As Int16
            Dim mNarrativeSummary As Int16
            'Dim mResult As Int16
            Dim mTotalExerciseTime As Int16
            Dim mEjectionFraction As Int16

            Dim strdtQry As String
            Dim strCptQry As String
            Dim strCPTTestTypeQry As String
            Dim strPhysicianQry As String
            Dim strRestingHeartRateQry As String
            Dim strPeakHeartRateQry As String
            Dim strNarrativeSummaryQry As String
            ' '' '' ''Dim strResult As String
            Dim strTotalExerciseTimeQry As String
            Dim strEjectionFractionQry As String
            Dim strconcatCPT1 As String = ""
            Dim nextRow As Integer
            Dim strCombine As String = ""


            'Dim nCPTCode As myTreeNode
            'Dim nCPTTestType As myTreeNode
            'Dim nPhysician As myTreeNode
            'Dim nRestingHeartRate As myTreeNode
            'Dim nPeakHeartRate As myTreeNode
            'Dim nNarrativeSummary As myTreeNode
            'Dim nResult As myTreeNode
            'Dim nTotalExerciseTime As myTreeNode
            'Dim nEjectionFraction As myTreeNode


            strdtQry = "SELECT Distinct isnull(nStressID,0) as nStressID,isnull(nPatientID,0) as nPatientID,isnull(nExamID,0) as nExamID,isnull(nVisitID,0) as nVisitID,isnull(nClinicID,0) as nClinicID,dtDateOfStudy as DateofStudy FROM CV_StressTest WHERE nGroupID=0 AND nPatientID='" & mPatientID & "' order by DateofStudy"

            Dim oDB As New gloStream.gloDataBase.gloDataBase
            oDB.Connect(GetConnectionString)
            dtStudyDate = oDB.ReadQueryDataTable(strdtQry)
            oDB.Disconnect()

            With dtStudyDate
                If IsNothing(dtStudyDate) = False Then
                    For mDOS = 0 To dtStudyDate.Rows.Count - 1
                        Dim StressID As Int64 = 0
                        Dim PatientID As Int64 = 0
                        Dim VisitID As Int64 = 0
                        Dim ExamID As Int64 = 0
                        Dim ClinicID As Int64 = 0
                        Dim DateofStudy As Date
                        Dim TestType As String = ""

                        Dim Result As String = ""

                        Dim count As Integer = mDOS + 1
                        If CStr(dtStudyDate.Rows(mDOS)("DateofStudy")).Trim <> "" Then

                            C1CV_StressTest.Rows.Add()
                            _Row = C1CV_StressTest.Rows.Count - 1
                            ''set the properties for newly added row
                            With C1CV_StressTest.Rows(_Row)
                                .AllowEditing = False
                                .ImageAndText = True
                                .Height = 24
                                .IsNode = True
                                .Node.Level = 0
                                .Node.Data = Convert.ToDateTime(dtStudyDate.Rows(mDOS)("DateofStudy")).ToShortDateString
                                .Node.Image = ImageList1.Images(0)
                            End With
                            nextRow = _Row
                            With C1CV_StressTest
                                ''.SetData(_Row, COL_DateofStudy, _Row)
                                .SetData(_Row, COL_PatientID, dtStudyDate.Rows(mDOS)("nPatientID"))
                                .SetData(_Row, COL_ExamID, dtStudyDate.Rows(mDOS)("nExamID"))
                                .SetData(_Row, COL_VisitID, dtStudyDate.Rows(mDOS)("nVisitID"))
                                .SetData(_Row, COL_ClinicID, dtStudyDate.Rows(mDOS)("nClinicID"))
                                .SetData(_Row, COL_StressID, dtStudyDate.Rows(mDOS)("nStressID"))
                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDate.Rows(mDOS)("DateofStudy")).ToShortDateString)
                                .SetData(_Row, COL_TestType, TestType)
                                .SetData(_Row, COL_Result, Result)

                                PatientID = dtStudyDate.Rows(mDOS)("nPatientID")
                                VisitID = dtStudyDate.Rows(mDOS)("nVisitID")
                                ExamID = dtStudyDate.Rows(mDOS)("nExamID")
                                ClinicID = dtStudyDate.Rows(mDOS)("nClinicID")
                                StressID = dtStudyDate.Rows(mDOS)("nStressID")
                                DateofStudy = Convert.ToDateTime(dtStudyDate.Rows(mDOS)("DateofStudy")).ToShortDateString
                            End With


                            Dim dtDateofStudy As Date = Convert.ToDateTime(dtStudyDate.Rows(mDOS)("DateofStudy")).ToShortDateString()


                            '' Query for selecting CPT for current exam ''
                            strCptQry = "SELECT DISTINCT isnull(sCPT,'') as sCPT from CV_StressTest where nPatientID=" & mPatientID & " AND dtDateofStudy='" & dtDateofStudy & "' AND nGroupID='" & StressID & "' AND sCPT<>''"
                            oDB.Connect(GetConnectionString)
                            dtCPT = oDB.ReadQueryDataTable(strCptQry)
                            oDB.Disconnect()

                            With dtCPT
                                If IsNothing(dtCPT) = False Then
                                    If dtCPT.Rows.Count >= 0 Then
                                        C1CV_StressTest.Rows.Add()
                                        _Row = C1CV_StressTest.Rows.Count - 1
                                        With C1CV_StressTest.Rows(_Row)
                                            .AllowEditing = False
                                            .ImageAndText = True
                                            .Height = 24
                                            .IsNode = True
                                            .Node.Level = 1
                                            .Node.Data = "CPT"
                                            .Node.Image = ImageList1.Images(1)
                                        End With
                                        With C1CV_StressTest
                                            .SetData(_Row, COL_PatientID, dtStudyDate.Rows(mDOS)("nPatientID"))
                                            .SetData(_Row, COL_VisitID, dtStudyDate.Rows(mDOS)("nVisitID"))
                                            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDate.Rows(mDOS)("DateofStudy")).ToShortDateString)
                                        End With
                                    End If
                                    For mCPT = 0 To dtCPT.Rows.Count - 1
                                        Dim strCurrentCPT As String = dtCPT.Rows(mCPT)("sCPT")
                                        If strCurrentCPT.Trim <> "" Then
                                            C1CV_StressTest.Rows.Add()
                                            _Row = C1CV_StressTest.Rows.Count - 1
                                            ''set the properties for newly added row
                                            With C1CV_StressTest.Rows(_Row)
                                                .AllowEditing = True
                                                .ImageAndText = True
                                                .Height = 24
                                                .IsNode = True
                                                .Node.Level = 2
                                                .Node.Data = dtCPT.Rows(mCPT)("sCPT")
                                                .Node.Image = ImageList1.Images(3)
                                            End With
                                            With C1CV_StressTest
                                                .SetData(_Row, COL_PatientID, dtStudyDate.Rows(mDOS)("nPatientID"))
                                                .SetData(_Row, COL_VisitID, dtStudyDate.Rows(mDOS)("nVisitID"))
                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDate.Rows(mDOS)("DateofStudy")).ToShortDateString)
                                            End With


                                            '' Query for selecting CPTCoded Test Type ''
                                            strCPTTestTypeQry = "SELECT DISTINCT isnull(sTestType,'') as sTestType, isnull(sResult,'') as sResult  from CV_StressTest where nPatientID=" & mPatientID & " AND dtDateofStudy='" & dtDateofStudy & "' AND nGroupID='" & StressID & "' AND sCPT='" & strCurrentCPT & "'  and sTestType<>''"
                                            oDB.Connect(GetConnectionString)
                                            dtTestType = oDB.ReadQueryDataTable(strCPTTestTypeQry)
                                            oDB.Disconnect()

                                            If dtTestType.Rows.Count > 0 Then
                                                C1CV_StressTest.Rows.Add()
                                                _Row = C1CV_StressTest.Rows.Count - 1
                                                With C1CV_StressTest.Rows(_Row)
                                                    .AllowEditing = False
                                                    .ImageAndText = True
                                                    .Height = 24
                                                    .IsNode = True
                                                    .Node.Level = 3
                                                    .Node.Data = "CPT Coded Test Type"
                                                    .Node.Image = ImageList1.Images(4)
                                                End With
                                                With C1CV_StressTest
                                                    .SetData(_Row, COL_PatientID, dtStudyDate.Rows(mDOS)("nPatientID"))
                                                    .SetData(_Row, COL_VisitID, dtStudyDate.Rows(mDOS)("nVisitID"))
                                                    .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDate.Rows(mDOS)("DateofStudy")).ToShortDateString)
                                                End With
                                                '' To Identify for which CPT we are addding the Test Type
                                                With dtTestType
                                                    If IsNothing(dtTestType) = False Then
                                                        For mTestType = 0 To dtTestType.Rows.Count - 1
                                                            Dim strTest As String = dtTestType.Rows(mTestType)("sTestType")
                                                            Dim StrResult As String = dtTestType.Rows(mTestType)("sResult")
                                                            If StrResult.Trim <> "" Then
                                                                StrResult = " - " & StrResult
                                                            End If
                                                            C1CV_StressTest.Rows.Add()
                                                            _Row = C1CV_StressTest.Rows.Count - 1
                                                            ''set the properties for newly added row
                                                            With C1CV_StressTest.Rows(_Row)
                                                                .AllowEditing = True
                                                                .ImageAndText = True
                                                                .Height = 24
                                                                .IsNode = True
                                                                .Node.Level = 4
                                                                ''''''''
                                                                .Node.Data = strTest & StrResult
                                                                ''''''''
                                                                .Node.Image = ImageList1.Images(3)
                                                            End With
                                                            With C1CV_StressTest
                                                                .SetData(_Row, COL_PatientID, dtStudyDate.Rows(mDOS)("nPatientID"))
                                                                .SetData(_Row, COL_VisitID, dtStudyDate.Rows(mDOS)("nVisitID"))
                                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDate.Rows(mDOS)("DateofStudy")).ToShortDateString)
                                                            End With
                                                        Next
                                                    End If   ''IsNothing(dtTestType) = False Then
                                                End With
                                            End If   ''dtTestType.Rows.Count > 0 Then
                                        End If
                                    Next   ''mCPT = 0 To dtCPT.Rows.Count - 1
                                End If
                            End With  '' With dtCPT




                            '' Query for selecting Physician for current exam  ''
                            strPhysicianQry = "SELECT Distinct isnull(sUserName,'') as UserName from CV_StressTest WHERE  nPatientID = " & mPatientID & " and dtDateOfStudy = '" & dtDateofStudy & "' AND nStressID='" & StressID & "'"
                            oDB.Connect(GetConnectionString)
                            dtUsers = oDB.ReadQueryDataTable(strPhysicianQry)
                            oDB.Disconnect()

                            With dtUsers
                                If IsNothing(dtUsers) = False Then
                                    If dtUsers.Rows.Count > 0 Then
                                        C1CV_StressTest.Rows.Add()
                                        _Row = C1CV_StressTest.Rows.Count - 1
                                        With C1CV_StressTest.Rows(_Row)
                                            .AllowEditing = False
                                            .ImageAndText = True
                                            .Height = 24
                                            .IsNode = True
                                            .Node.Level = 1
                                            .Node.Data = "Physician's"
                                            .Node.Image = ImageList1.Images(7)
                                        End With
                                        With C1CV_StressTest
                                            .SetData(_Row, COL_PatientID, dtStudyDate.Rows(mDOS)("nPatientID"))
                                            .SetData(_Row, COL_VisitID, dtStudyDate.Rows(mDOS)("nVisitID"))
                                            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDate.Rows(mDOS)("DateofStudy")).ToShortDateString)
                                        End With
                                        For mPhysician = 0 To dtUsers.Rows.Count - 1
                                            Dim strUsers As String = dtUsers.Rows(mPhysician)("UserName")
                                            If strUsers <> "" Then
                                                C1CV_StressTest.Rows.Add()
                                                _Row = C1CV_StressTest.Rows.Count - 1
                                                ''set the properties for newly added row
                                                With C1CV_StressTest.Rows(_Row)
                                                    .AllowEditing = False
                                                    .ImageAndText = True
                                                    .Height = 24
                                                    .IsNode = True
                                                    .Node.Level = 2
                                                    .Node.Data = dtUsers.Rows(mPhysician)("UserName")
                                                    .Node.Image = ImageList1.Images(3)
                                                End With
                                                With C1CV_StressTest
                                                    .SetData(_Row, COL_PatientID, dtStudyDate.Rows(mDOS)("nPatientID"))
                                                    .SetData(_Row, COL_VisitID, dtStudyDate.Rows(mDOS)("nVisitID"))
                                                    .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDate.Rows(mDOS)("DateofStudy")).ToShortDateString)
                                                End With
                                            End If
                                        Next  ''mPhysician = 0 To dtUsers.Rows.Count - 1
                                    End If
                                End If
                            End With  '' With dtUsers



                            ' '' '' '' '' Query for selecting Result ''
                            ' '' '' ''strResult = "SELECT DISTINCT isnull(sResult,'') as Result from CV_StressTest where nPatientID=" & mPatientID & " AND dtDateofStudy='" & dtDateofStudy & "' AND nStressID='" & StressID & "'"
                            ' '' '' ''oDB.Connect(GetConnectionString)
                            ' '' '' ''dtResult = oDB.ReadQueryDataTable(strResult)
                            ' '' '' ''oDB.Disconnect()

                            ' '' '' ''With dtResult
                            ' '' '' ''    If IsNothing(dtResult) = False Then
                            ' '' '' ''        If dtResult.Rows.Count > 0 Then
                            ' '' '' ''            C1CV_StressTest.Rows.Add()
                            ' '' '' ''            _Row = C1CV_StressTest.Rows.Count - 1
                            ' '' '' ''            With C1CV_StressTest.Rows(_Row)
                            ' '' '' ''                .AllowEditing = False
                            ' '' '' ''                .ImageAndText = True
                            ' '' '' ''                .Height = 24
                            ' '' '' ''                .IsNode = True
                            ' '' '' ''                .Node.Level = 1
                            ' '' '' ''                .Node.Data = "Result"
                            ' '' '' ''                .Node.Image = ImageList1.Images(9)
                            ' '' '' ''            End With
                            ' '' '' ''            With C1CV_StressTest
                            ' '' '' ''                .SetData(_Row, COL_PatientID, dtStudyDate.Rows(mDOS)("nPatientID"))
                            ' '' '' ''                .SetData(_Row, COL_VisitID, dtStudyDate.Rows(mDOS)("nVisitID"))
                            ' '' '' ''                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDate.Rows(mDOS)("DateofStudy")).ToShortDateString)
                            ' '' '' ''            End With
                            ' '' '' ''        End If
                            ' '' '' ''        For mResult = 0 To dtResult.Rows.Count - 1
                            ' '' '' ''            Dim strResults As String = dtResult.Rows(mResult)("Result")
                            ' '' '' ''            If strResults.Trim <> "" Then
                            ' '' '' ''                C1CV_StressTest.Rows.Add()
                            ' '' '' ''                _Row = C1CV_StressTest.Rows.Count - 1
                            ' '' '' ''                With C1CV_StressTest.Rows(_Row)
                            ' '' '' ''                    .AllowEditing = True
                            ' '' '' ''                    .ImageAndText = True
                            ' '' '' ''                    .Height = 24
                            ' '' '' ''                    .IsNode = True
                            ' '' '' ''                    .Node.Level = 2
                            ' '' '' ''                    .TextAlign = TextAlignEnum.LeftCenter
                            ' '' '' ''                    .Node.Data = dtResult.Rows(mResult)("Result")
                            ' '' '' ''                    .Node.Image = ImageList1.Images(3)
                            ' '' '' ''                End With
                            ' '' '' ''                With C1CV_StressTest
                            ' '' '' ''                    .SetData(_Row, COL_PatientID, dtStudyDate.Rows(mDOS)("nPatientID"))
                            ' '' '' ''                    .SetData(_Row, COL_VisitID, dtStudyDate.Rows(mDOS)("nVisitID"))
                            ' '' '' ''                    .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDate.Rows(mDOS)("DateofStudy")).ToShortDateString)
                            ' '' '' ''                End With
                            ' '' '' ''            End If
                            ' '' '' ''        Next
                            ' '' '' ''    End If
                            ' '' '' ''End With


                            '' Query for selecting Total Exercise Time ''
                            strTotalExerciseTimeQry = "SELECT DISTINCT isnull(sTotExerciseTime,'') as TotalExerciseTime from CV_StressTest where nPatientID=" & mPatientID & " AND dtDateofStudy='" & dtDateofStudy & "' AND nStressID='" & StressID & "'"
                            oDB.Connect(GetConnectionString)
                            dtTotalExerciseTime = oDB.ReadQueryDataTable(strTotalExerciseTimeQry)
                            oDB.Disconnect()

                            With dtTotalExerciseTime
                                If IsNothing(dtTotalExerciseTime) = False Then
                                    If dtTotalExerciseTime.Rows.Count > 0 Then
                                        C1CV_StressTest.Rows.Add()
                                        _Row = C1CV_StressTest.Rows.Count - 1
                                        With C1CV_StressTest.Rows(_Row)
                                            .AllowEditing = False
                                            .ImageAndText = True
                                            .Height = 24
                                            .IsNode = True
                                            .Node.Level = 1
                                            .Node.Data = "Total Exercise Time"
                                            .Node.Image = ImageList1.Images(12)
                                        End With
                                        With C1CV_StressTest
                                            .SetData(_Row, COL_PatientID, dtStudyDate.Rows(mDOS)("nPatientID"))
                                            .SetData(_Row, COL_VisitID, dtStudyDate.Rows(mDOS)("nVisitID"))
                                            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDate.Rows(mDOS)("DateofStudy")).ToShortDateString)
                                        End With
                                    End If
                                    For mTotalExerciseTime = 0 To dtTotalExerciseTime.Rows.Count - 1
                                        Dim strTotExerciseTime As String = dtTotalExerciseTime.Rows(mTotalExerciseTime)("TotalExerciseTime")
                                        If strTotExerciseTime.Trim <> "" Then
                                            C1CV_StressTest.Rows.Add()
                                            _Row = C1CV_StressTest.Rows.Count - 1
                                            With C1CV_StressTest.Rows(_Row)
                                                .AllowEditing = True
                                                .ImageAndText = True
                                                .Height = 24
                                                .IsNode = True
                                                .Node.Level = 2
                                                .TextAlign = TextAlignEnum.LeftCenter
                                                ''.DataType = Type.GetType("System.String")
                                                .Node.Data = dtTotalExerciseTime.Rows(mTotalExerciseTime)("TotalExerciseTime") + " " + "min."
                                                .Node.Image = ImageList1.Images(3)
                                            End With
                                            With C1CV_StressTest
                                                .SetData(_Row, COL_PatientID, dtStudyDate.Rows(mDOS)("nPatientID"))
                                                .SetData(_Row, COL_VisitID, dtStudyDate.Rows(mDOS)("nVisitID"))
                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDate.Rows(mDOS)("DateofStudy")).ToShortDateString)
                                            End With
                                        End If
                                    Next
                                End If
                            End With



                            '' Query for selecting Ejection Fraction ''
                            strEjectionFractionQry = "SELECT DISTINCT isnull(sEjectionFraction,'') as EjectionFraction from CV_StressTest where nPatientID=" & mPatientID & " AND dtDateofStudy='" & dtDateofStudy & "' AND nStressID='" & StressID & "'"
                            oDB.Connect(GetConnectionString)
                            dtEjectionFraction = oDB.ReadQueryDataTable(strEjectionFractionQry)
                            oDB.Disconnect()

                            With dtEjectionFraction
                                If IsNothing(dtEjectionFraction) = False Then
                                    If dtEjectionFraction.Rows.Count > 0 Then
                                        C1CV_StressTest.Rows.Add()
                                        _Row = C1CV_StressTest.Rows.Count - 1
                                        With C1CV_StressTest.Rows(_Row)
                                            .AllowEditing = False
                                            .ImageAndText = True
                                            .Height = 24
                                            .IsNode = True
                                            .Node.Level = 1
                                            .Node.Data = "Ejection Fraction"
                                            .Node.Image = ImageList1.Images(8)
                                        End With
                                        With C1CV_StressTest
                                            .SetData(_Row, COL_PatientID, dtStudyDate.Rows(mDOS)("nPatientID"))
                                            .SetData(_Row, COL_VisitID, dtStudyDate.Rows(mDOS)("nVisitID"))
                                            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDate.Rows(mDOS)("DateofStudy")).ToShortDateString)
                                        End With
                                    End If
                                    For mEjectionFraction = 0 To dtEjectionFraction.Rows.Count - 1
                                        Dim strTotExerciseTime As String = dtEjectionFraction.Rows(mEjectionFraction)("EjectionFraction")
                                        If strTotExerciseTime.Trim <> "" Then
                                            C1CV_StressTest.Rows.Add()
                                            _Row = C1CV_StressTest.Rows.Count - 1
                                            With C1CV_StressTest.Rows(_Row)
                                                .AllowEditing = True
                                                .ImageAndText = True
                                                .Height = 24
                                                .IsNode = True
                                                .Node.Level = 2
                                                .TextAlign = TextAlignEnum.LeftCenter
                                                .Node.Data = dtEjectionFraction.Rows(mEjectionFraction)("EjectionFraction") + " " + "%"
                                                .Node.Image = ImageList1.Images(3)
                                            End With
                                            With C1CV_StressTest
                                                .SetData(_Row, COL_PatientID, dtStudyDate.Rows(mDOS)("nPatientID"))
                                                .SetData(_Row, COL_VisitID, dtStudyDate.Rows(mDOS)("nVisitID"))
                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDate.Rows(mDOS)("DateofStudy")).ToShortDateString)
                                            End With
                                        End If
                                    Next
                                End If
                            End With


                            '' Query for selecting RestingHeartRate for current exam  ''
                            strRestingHeartRateQry = "SELECT Distinct isnull(nRestingHeartRate,0) as nRestingHeartRate,isnull(nRestingBPMin,0) as nRestingBPMin,isnull(nRestingBPMax,0) as nRestingBPMax from CV_StressTest WHERE  nPatientID = " & mPatientID & " and dtDateOfStudy = '" & dtDateofStudy & "' AND nStressID='" & StressID & "'"
                            oDB.Connect(GetConnectionString)
                            dtRestingHeartRate = oDB.ReadQueryDataTable(strRestingHeartRateQry)
                            oDB.Disconnect()

                            With dtRestingHeartRate
                                If IsNothing(dtRestingHeartRate) = False Then
                                    If dtRestingHeartRate.Rows.Count > 0 Then
                                        C1CV_StressTest.Rows.Add()
                                        _Row = C1CV_StressTest.Rows.Count - 1
                                        With C1CV_StressTest.Rows(_Row)
                                            .AllowEditing = False
                                            .ImageAndText = True
                                            .Height = 24
                                            .IsNode = True
                                            .Node.Level = 1
                                            .Node.Data = "Resting Heart Rate"
                                            .Node.Image = ImageList1.Images(10)
                                        End With
                                        With C1CV_StressTest
                                            .SetData(_Row, COL_PatientID, dtStudyDate.Rows(mDOS)("nPatientID"))
                                            .SetData(_Row, COL_VisitID, dtStudyDate.Rows(mDOS)("nVisitID"))
                                            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDate.Rows(mDOS)("DateofStudy")).ToShortDateString)
                                        End With
                                        For mRestingHeartRate = 0 To dtRestingHeartRate.Rows.Count - 1
                                            strCombine = ""
                                            If Not IsNothing(dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingHeartRate").ToString()) Then
                                                If (dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingHeartRate").ToString() <> "") Then
                                                    If dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingHeartRate").ToString() <> "0" Then
                                                        If (strCombine = "") Then
                                                            strCombine = "Heart Rate" + " " + ":" + " " + Convert.ToString(dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingHeartRate"))
                                                        End If
                                                    End If
                                                End If
                                            End If
                                            If Not IsNothing(dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingBPMax").ToString()) Then
                                                If (dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingBPMax").ToString() <> "") Then
                                                    If dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingBPMax").ToString() <> "0" Then
                                                        If (strCombine = "") Then
                                                            strCombine = "BP Max" + " " + ":" + " " + dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingBPMax").ToString()
                                                        Else
                                                            strCombine = strCombine + "    " + "BP Max" + " " + ":" + " " + Convert.ToString(dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingBPMax"))
                                                        End If
                                                    End If
                                                End If
                                            End If
                                            If Not IsNothing(dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingBPMin").ToString()) Then
                                                If (dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingBPMin").ToString() <> "") Then
                                                    If dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingBPMin").ToString() <> "0" Then
                                                        If (strCombine = "") Then
                                                            strCombine = "BP Min" + " " + ":" + " " + dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingBPMin").ToString()
                                                        Else
                                                            strCombine = strCombine + "    " + "BP Min" + " " + ":" + " " + Convert.ToString(dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingBPMin"))
                                                        End If
                                                    End If
                                                End If
                                            End If

                                            If strCombine.Trim <> "" Then
                                                C1CV_StressTest.Rows.Add()
                                                _Row = C1CV_StressTest.Rows.Count - 1
                                                ''set the properties for newly added row
                                                With C1CV_StressTest.Rows(_Row)
                                                    .AllowEditing = False
                                                    .ImageAndText = True
                                                    .Height = 24
                                                    .IsNode = True
                                                    .Node.Level = 2
                                                    .Node.Data = strCombine
                                                    .Node.Image = ImageList1.Images(3)
                                                End With
                                                With C1CV_StressTest
                                                    .SetData(_Row, COL_PatientID, dtStudyDate.Rows(mDOS)("nPatientID"))
                                                    .SetData(_Row, COL_VisitID, dtStudyDate.Rows(mDOS)("nVisitID"))
                                                    .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDate.Rows(mDOS)("DateofStudy")).ToShortDateString)
                                                End With
                                            End If
                                        Next
                                    End If
                                End If
                            End With



                            '' Query for selecting PeakHeartRate for current exam '' 
                            strPeakHeartRateQry = "SELECT Distinct isnull(nPeakHeartRate,0) as nPeakHeartRate,isnull(nPeakBPMin,0) as nPeakBPMin,isnull(nPeakBPMax,0) as nPeakBPMax from CV_StressTest WHERE  nPatientID = " & mPatientID & " and dtDateOfStudy = '" & dtDateofStudy & "' AND nStressID='" & StressID & "'"
                            oDB.Connect(GetConnectionString)
                            dtPeakHeartRate = oDB.ReadQueryDataTable(strPeakHeartRateQry)
                            oDB.Disconnect()

                            With dtPeakHeartRate
                                If IsNothing(dtPeakHeartRate) = False Then
                                    If dtPeakHeartRate.Rows.Count > 0 Then
                                        C1CV_StressTest.Rows.Add()
                                        _Row = C1CV_StressTest.Rows.Count - 1
                                        With C1CV_StressTest.Rows(_Row)
                                            .AllowEditing = False
                                            .ImageAndText = True
                                            .Height = 24
                                            .IsNode = True
                                            .Node.Level = 1
                                            .Node.Data = "Peak Heart Rate"
                                            .Node.Image = ImageList1.Images(11)
                                        End With
                                        With C1CV_StressTest
                                            .SetData(_Row, COL_PatientID, dtStudyDate.Rows(mDOS)("nPatientID"))
                                            .SetData(_Row, COL_VisitID, dtStudyDate.Rows(mDOS)("nVisitID"))
                                            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDate.Rows(mDOS)("DateofStudy")).ToShortDateString)
                                        End With
                                        For mPeakHeartRate = 0 To dtPeakHeartRate.Rows.Count - 1
                                            strCombine = ""
                                            If Not IsNothing(dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakHeartRate").ToString()) Then
                                                If (dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakHeartRate").ToString() <> "") Then
                                                    If dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakHeartRate").ToString() <> "0" Then
                                                        If (strCombine = "") Then
                                                            strCombine = "Heart Rate" + " " + ":" + " " + dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakHeartRate").ToString()
                                                        End If
                                                    End If
                                                End If
                                            End If
                                            If Not IsNothing(dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakBPMax").ToString()) Then
                                                If (dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakBPMax").ToString() <> "") Then
                                                    If dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakBPMax").ToString() <> "0" Then
                                                        If (strCombine = "") Then
                                                            strCombine = "BP Max" + " " + ":" + " " + dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakBPMax").ToString()
                                                        Else
                                                            strCombine = strCombine + "    " + "BP Max" + " " + ":" + " " + dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakBPMax").ToString()
                                                        End If
                                                    End If
                                                End If
                                            End If
                                            If Not IsNothing(dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakBPMin").ToString()) Then
                                                If (dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakBPMin").ToString() <> "") Then
                                                    If dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakBPMin").ToString() <> "0" Then
                                                        If (strCombine = "") Then
                                                            strCombine = "BP Min" + " " + ":" + " " + dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakBPMin").ToString()
                                                        Else
                                                            strCombine = strCombine + "    " + "BP Min" + " " + ":" + " " + dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakBPMin").ToString()
                                                        End If
                                                    End If
                                                End If
                                            End If

                                            If strCombine.Trim <> "" Then
                                                C1CV_StressTest.Rows.Add()
                                                _Row = C1CV_StressTest.Rows.Count - 1
                                                ''set the properties for newly added row
                                                With C1CV_StressTest.Rows(_Row)
                                                    .AllowEditing = False
                                                    .ImageAndText = True
                                                    .Height = 24
                                                    .IsNode = True
                                                    .Node.Level = 2
                                                    .Node.Data = strCombine
                                                    .Node.Image = ImageList1.Images(3)
                                                End With
                                                With C1CV_StressTest
                                                    .SetData(_Row, COL_PatientID, dtStudyDate.Rows(mDOS)("nPatientID"))
                                                    .SetData(_Row, COL_VisitID, dtStudyDate.Rows(mDOS)("nVisitID"))
                                                    .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDate.Rows(mDOS)("DateofStudy")).ToShortDateString)
                                                End With
                                            End If

                                        Next
                                    End If
                                End If
                            End With


                            '' Query for selecting Narrative Summary ''
                            strNarrativeSummaryQry = "SELECT DISTINCT isnull(sNarrativeSummary,'') as NarrativeSummary from CV_StressTest where nPatientID=" & mPatientID & " AND dtDateofStudy='" & dtDateofStudy & "' AND nStressID='" & StressID & "'"
                            oDB.Connect(GetConnectionString)
                            dtNarrativeSummary = oDB.ReadQueryDataTable(strNarrativeSummaryQry)
                            oDB.Disconnect()

                            With dtNarrativeSummary
                                If IsNothing(dtNarrativeSummary) = False Then
                                    If dtNarrativeSummary.Rows.Count > 0 Then
                                        C1CV_StressTest.Rows.Add()
                                        _Row = C1CV_StressTest.Rows.Count - 1
                                        With C1CV_StressTest.Rows(_Row)
                                            .AllowEditing = False
                                            .ImageAndText = True
                                            .Height = 24
                                            .IsNode = True
                                            .Node.Level = 1
                                            .Node.Data = "Narrative Summary"
                                            .Node.Image = ImageList1.Images(6)
                                        End With
                                        With C1CV_StressTest
                                            .SetData(_Row, COL_PatientID, dtStudyDate.Rows(mDOS)("nPatientID"))
                                            .SetData(_Row, COL_VisitID, dtStudyDate.Rows(mDOS)("nVisitID"))
                                            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDate.Rows(mDOS)("DateofStudy")).ToShortDateString)
                                        End With
                                    End If
                                    For mNarrativeSummary = 0 To dtNarrativeSummary.Rows.Count - 1
                                        Dim strNarrativesummary As String = dtNarrativeSummary.Rows(mNarrativeSummary)("NarrativeSummary").ToString()
                                        If strNarrativesummary.Trim <> "" Then
                                            C1CV_StressTest.Rows.Add()
                                            _Row = C1CV_StressTest.Rows.Count - 1
                                            With C1CV_StressTest.Rows(_Row)
                                                .AllowEditing = True
                                                .ImageAndText = True
                                                .Height = 24
                                                .IsNode = True
                                                .Node.Level = 2
                                                ''.DataType = Type.GetType("System.String")
                                                .TextAlign = TextAlignEnum.LeftCenter
                                                .Node.Data = strNarrativesummary
                                                .Node.Image = ImageList1.Images(3)
                                            End With
                                            With C1CV_StressTest
                                                .SetData(_Row, COL_PatientID, dtStudyDate.Rows(mDOS)("nPatientID"))
                                                .SetData(_Row, COL_VisitID, dtStudyDate.Rows(mDOS)("nVisitID"))
                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDate.Rows(mDOS)("DateofStudy")).ToShortDateString)
                                            End With
                                        End If
                                    Next  'For mNarrativesummary  = 0 To .Rows.Count - 1
                                End If  'dtNarrativeSummary
                            End With

                        End If     '' CStr(dtStudyDate.Rows(mDOS)("DateofStudy")).Trim <> "" Then
                    Next   ''For mDOS = 0 To dtStudyDate.Rows.Count - 1
                End If   ''If IsNothing(dtStudyDate) = False Then
            End With   '' With dtStudyDate

            dtStudyDate = Nothing
            dtCPT = Nothing
            dtUsers = Nothing

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmCV_VwStressTest_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub frmCV_VwStressTest_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try

            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.Close, "Close Stress Test", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20100916
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.Close, "Close Stress Test", 0, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            ''
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub frmCV_VwStressTest_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'gloC1FlexStyle.Style(C1CV_StressTest)
        SetGridStyle()
        FillStressTest()
        Try
            gloPatient.gloPatient.GetWindowTitle(Me, mPatientID, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        'Try

        '    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.View, "View CV Stress Test", gloAuditTrail.ActivityOutCome.Success)
        'Catch ex As Exception
        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        'End Try

    End Sub

    Private Sub ts_ViewButtons_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        Try
            Select Case e.ClickedItem.Tag

                Case "Add"
                    '' StartPosition :: Stress test 
                    '' ''Dim _isCurrentDate As Boolean = False
                    '' ''If C1CV_StressTest.Row > 0 Then
                    '' ''    Dim myDate As Date = C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_DateofStudyInvisible)
                    '' ''    If myDate = Date.Now.Date Then '' If there is data against the todays date then open for the modify
                    '' ''        If C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_PatientID) > 0 And C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_VisitID) > 0 Then

                    '' ''            mPatientID = C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_PatientID)
                    '' ''            mStressID = C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_StressID)
                    '' ''            mExamID = C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_ExamID)
                    '' ''            mVisitID = C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_VisitID)
                    '' ''            mClinicID = C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_ClinicID)
                    '' ''            mDateofStudy = C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_DateofStudyInvisible)
                    '' ''        End If

                    '' ''    Else
                    '' ''        mPatientID = mPatientID
                    '' ''        mStressID = 0
                    '' ''        mExamID = 0
                    '' ''        'mVisitID = GenerateVisitID(Now.Date, mPatientID)
                    '' ''        'Check for visit aganist current date,if visit not available pass 0
                    '' ''        mVisitID = GetVisitID(Now.Date, mPatientID)
                    '' ''        mClinicID = gnClinicID
                    '' ''        mDateofStudy = Now.Date

                    '' ''        _isCurrentDate = True
                    '' ''    End If

                    '' ''Else
                    '' ''    mPatientID = mPatientID
                    '' ''    mStressID = 0
                    '' ''    mExamID = 0
                    '' ''    'mVisitID = GenerateVisitID(Now.Date, mPatientID)
                    '' ''    'Check for visit aganist current date,if visit not available pass 0
                    '' ''    mVisitID = GetVisitID(Now.Date, mPatientID)
                    '' ''    mClinicID = gnClinicID
                    '' ''    mDateofStudy = Now.Date
                    '' ''    _isCurrentDate = True
                    '' ''End If
                    '' End :: Stress TEst

                    mPatientID = mPatientID
                    mExamID = 0
                    'mVisitID = GenerateVisitID(Now.Date, mPatientID)

                    'Check for visit aganist current date,if visit not available pass 0
                    mVisitID = GetVisitID(Now.Date, mPatientID)
                    mDateofStudy = Now.Date
                    '' ''Dim _isCurrentDate As Boolean = False
                    Dim ofrm As New frmCV_StressTests(mPatientID, mVisitID, mDateofStudy)
                    If Not IsNothing(ofrm) Then
                        '' ''If _isCurrentDate = True Then
                        '' ''    ofrm.blnIsNew = True
                        '' ''    _isCurrentDate = False
                        '' ''End If

                        ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                        SetGridStyle()
                        FillStressTest()
                        ofrm.Dispose()
                        ofrm = Nothing
                    End If
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.View, "Record viewed for Cardio Vascular stress test", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20100916
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.View, "Record viewed for Cardio Vascular stress test", mPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)



                    ''
                    ''gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.RecordViewed, "Record viewed for Cardio Vascular stress test", gstrLoginName, gstrClientMachineName, mPatientID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
                Case "Modify"
                    If C1CV_StressTest.Row > 0 Then

                        If C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_PatientID) > 0 And C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_VisitID) > 0 Then
                            mPatientID = C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_PatientID)
                            mStressID = C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_StressID)
                            mExamID = C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_ExamID)
                            mVisitID = C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_VisitID)
                            mClinicID = C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_ClinicID)

                            mDateofStudy = C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_DateofStudyInvisible)
                            SetGridStyle()
                            FillStressTest()
                            Dim ofrm As New frmCV_StressTests(mPatientID, mVisitID, mDateofStudy)
                            ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                            SetGridStyle()
                            FillStressTest()
                            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.Modify, "Modified Records in Cardio Vascular stress test", gloAuditTrail.ActivityOutCome.Success)
                            ''Added Rahul P on 20100916
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.Modify, "Modified Records in Cardio Vascular stress test", mPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                            ''
                            ofrm.Dispose()
                            ofrm = Nothing
                        End If
                    End If

                    ''gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Modified Records in Cardio Vascular stress test", gstrLoginName, gstrClientMachineName, mPatientID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")

                Case "Delete"
                    If C1CV_StressTest.Row > 0 Then
                        mPatientID = C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_PatientID)
                        mStressID = C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_StressID)
                        mExamID = C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_ExamID)
                        mVisitID = C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_VisitID)
                        mClinicID = C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_ClinicID)

                        mDateofStudy = C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_DateofStudyInvisible)
                        If MessageBox.Show("Are you sure you want to delete the Stress test?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            Dim objEjectionDBLayer As New ClsEjectionFractionDBLayer
                            objEjectionDBLayer.DeleteStressTest(mPatientID, mVisitID, mDateofStudy)
                            txtsearch.Text = ""
                            SetGridStyle()
                            FillStressTest()
                        End If
                    End If

                Case "Refresh"
                    SetGridStyle()
                    FillStressTest()
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.Refresh, "Refresh records in Cardio Vascular stress test", gloAuditTrail.ActivityOutCome.Success)

                Case "Close"
                    '  Me.Close()
                    gloWord.WordDialogBoxBackgroundCloser.Close(Me)
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.Close, "Closed Cardio Vascular stress test", gloAuditTrail.ActivityOutCome.Success)

            End Select

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub txtsearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsearch.TextChanged
        Try

            Dim strSearch As String
            With txtsearch
                If Trim(.Text) <> "" Then
                    strSearch = Replace(.Text, "'", "''")
                Else
                    strSearch = ""
                End If
            End With

            With C1CV_StressTest
                'by Ujwala Atre to Expand All Nodes for Search - as on 20101029
                If strSearch.Trim <> "" And strSearch.Trim.Length = 1 Then
                    ''''''''''''
                    Dim objComm As New Cls_CardioVasculars
                    objComm.ExpandAll(C1CV_StressTest)
                    objComm = Nothing
                    ''''''''''''
                End If
                'by Ujwala Atre to Expand All Nodes for Search - as on 20101029
                .Row = .FindRow(strSearch, 1, COL_DateofStudy, False, False, True)
                If .Row > 0 Then
                    Exit Sub
                End If

                ''InString Search 
                Dim strNode As String = ""
                For i As Int16 = 1 To .Rows.Count - 1
                    strNode = ""
                    strNode = UCase(.GetData(i, COL_DateofStudy).ToString.Trim)
                    If InStr(strNode, UCase(strSearch.Trim), CompareMethod.Text) > 0 Then
                        .Row = i
                        Exit Sub
                    End If
                Next
            End With


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.Search, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try



    End Sub



    Private Sub C1CV_StressTest_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        'Use to clear search text box
        txtsearch.ResetText()
        txtsearch.Focus()
    End Sub

    Private Sub C1CV_StressTest_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1CV_StressTest.MouseDoubleClick

        Try
            If C1CV_StressTest.Row > 0 Then
                If C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_PatientID) > 0 And C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_VisitID) > 0 Then
                    mPatientID = C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_PatientID)
                    mVisitID = C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_VisitID)
                    mDateofStudy = C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_DateofStudyInvisible)

                    Dim ofrm As New frmCV_StressTests(mPatientID, mVisitID, mDateofStudy)
                    ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                    SetGridStyle()
                    FillStressTest()
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.Modify, "Modified Records in Cardio Vascular stress test", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20100916
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.Modify, "Modified Records in Cardio Vascular stress test", mPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                    ''
                    ofrm.Dispose()
                    ofrm = Nothing
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub


    ''' <summary>
    ''' Property writen for Patient Context
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return mPatientID  'Curent patient variable(Local variable) for this module 
        End Get
    End Property

End Class