Public Class frmDxSnomed
    Private VisitId As Long
    Private ExamID As Long
    Private _PatientID As Long
    Private Const Col_Diagnosis As Integer = 0
    Private Const Col_Snomed As Integer = 1
    Private Const Col_DxCode As Integer = 2
    Private Const Col_DxDesc As Integer = 3
    Private Const Col_SnomedCode As Integer = 4
    Private Const Col_SnomedDesc As Integer = 5
    Private Const Col_CPTCode As Integer = 6
    Private Const Col_CPTDesc As Integer = 7
    Private Const Col_Unit As Integer = 8
    Private Const Col_ModCode As Integer = 9
    Private Const Col_ModDesc As Integer = 10
    Private Const Col_LineNo As Integer = 11
    Private Const Col_nICDRevision As Integer = 12
    Dim _blnICDTransition As Boolean = False
    Public Property blnICDTransition() As Boolean
        Get
            Return _blnICDTransition
        End Get
        Set(ByVal Value As Boolean)
            _blnICDTransition = Value
        End Set
    End Property
    Public Sub New(ByVal m_VisitID, ByVal m_ExamID, ByVal PatientID, Optional ByVal viewdiagnosis = False)
        MyBase.New()
        VisitId = m_VisitID
        ExamID = m_ExamID
        
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
        _PatientID = PatientID

    End Sub
    Private Sub btnBrowseSnomed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseSnomed.Click
        Dim frm As gloSnoMed.FrmSelectProblem
        gstrSMDBConnstr = GetHybridConnectionString(gstrSMDBServerName, gstrSMDBDatabaseName, gblnSMDBAuthen, gstrSMDBUserID, gstrSMDBPassWord)
        frm = New gloSnoMed.FrmSelectProblem("Dx Snomed", gstrSMDBConnstr, GetConnectionString())
        '  frm.StartPosition = FormStartPosition.CenterScreen
        ' frm.ShowInTaskbar = False
        frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
        Dim _rowcnt As Integer = 0
        Dim ICD9() As String
        If frm._DialogResult Then

            If frm.strSelectedConceptID.Trim() <> "" Then
                _rowcnt = c1DxSnomedList.Rows.Count

                Dim _isItemAdded As Boolean = False
                Dim Ispresent As Boolean = False
                Try

                    If Not IsNothing(c1DxSnomedList) Then
                        If frm.strICD10 <> "" Then
                            For i As Integer = 1 To c1DxSnomedList.Rows.Count - 1
                                ''resolved  #98605-gloEMR : New Exam ( Dx Snomed ) : As user click on save&cls button of Dx snomed window ,application  shows message of ICD type mismatch
                                If c1DxSnomedList.GetData(i, Col_nICDRevision) = 9 Then
                                    If Convert.ToString(c1DxSnomedList.GetData(2, Col_DxCode)) <> "" Then
                                        MessageBox.Show("ICD Type Mismatch. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Exit Sub
                                    End If

                                End If
                                If Convert.ToString(c1DxSnomedList.GetData(i, Col_Diagnosis)) <> "" Then
                                    If Convert.ToString(c1DxSnomedList.GetData(i, Col_Diagnosis)) = frm.strICD10 Then
                                        Ispresent = True
                                        MessageBox.Show("Duplicate ICD10 is not allowed.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation) '' SUDHIR 20091014 '' BUG 3743 ''
                                        Exit Sub
                                    End If


                                End If

                            Next
                        ElseIf frm.strICD9 <> "" Then
                            For i As Integer = 1 To c1DxSnomedList.Rows.Count - 1
                                If c1DxSnomedList.GetData(i, Col_nICDRevision) = 10 Then
                                    MessageBox.Show("ICD Type Mismatch. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Exit Sub
                                End If
                                If Convert.ToString(c1DxSnomedList.GetData(i, Col_Diagnosis)) <> "" Then

                                    If Convert.ToString(c1DxSnomedList.GetData(i, Col_Diagnosis)) = frm.strICD9 Then
                                        Ispresent = True
                                        MessageBox.Show("Duplicate ICD9 is not allowed.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation) '' SUDHIR 20091014 '' BUG 3743 ''
                                        Exit Sub
                                    End If
                                End If

                            Next
                        End If


                        If Ispresent = False Then
                            ''

                            Dim ICD9Count As Integer = 0
                            Dim _Row As Integer
                            _Row = c1DxSnomedList.Rows.Count - 1
                            If _Row > 0 Then
                                If Not IsNothing(c1DxSnomedList.GetData(c1DxSnomedList.Rows.Count - 1, Col_LineNo)) Then
                                    ICD9Count = c1DxSnomedList.GetData(c1DxSnomedList.Rows.Count - 1, Col_LineNo)
                                Else
                                    ICD9Count = 0
                                End If
                            End If
                            ''
                            c1DxSnomedList.Rows.Add()

                            If frm.strICD10 <> "" Then
                                c1DxSnomedList.SetData(_rowcnt, Col_nICDRevision, gloGlobal.gloICD.CodeRevision.ICD10)
                                c1DxSnomedList.SetData(_rowcnt, Col_Diagnosis, frm.strICD10)
                                ICD9 = frm.strICD10.Trim.Split(":")
                                If ICD9.Length > 1 Then
                                    c1DxSnomedList.SetData(_rowcnt, Col_DxCode, ICD9.GetValue(0))
                                    c1DxSnomedList.SetData(_rowcnt, Col_DxDesc, ICD9.GetValue(1))
                                End If
                            Else
                                c1DxSnomedList.SetData(_rowcnt, Col_nICDRevision, gloGlobal.gloICD.CodeRevision.ICD9)
                                c1DxSnomedList.SetData(_rowcnt, Col_Diagnosis, frm.strICD9)
                                ICD9 = frm.strICD9.Trim.Split(":")
                                If ICD9.Length > 1 Then
                                    c1DxSnomedList.SetData(_rowcnt, Col_DxCode, ICD9.GetValue(0))
                                    c1DxSnomedList.SetData(_rowcnt, Col_DxDesc, ICD9.GetValue(1))
                                End If
                            End If



                            c1DxSnomedList.SetData(_rowcnt, Col_Snomed, frm.strSelectedConceptID + " : " + frm.strSelectedDescription)

                            c1DxSnomedList.SetData(_rowcnt, Col_SnomedCode, frm.strSelectedConceptID)
                            c1DxSnomedList.SetData(_rowcnt, Col_SnomedDesc, frm.strSelectedDescription)
                            c1DxSnomedList.SetData(_rowcnt, Col_LineNo, ICD9Count + 1)

                            _isItemAdded = True
                        End If

                    End If

                Catch ex As Exception
                    _isItemAdded = False
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.DxSnomed, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally

                End Try


            End If

        End If
        frm.Dispose()
        frm = Nothing
    End Sub

    Private Sub tlbbtn_Save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tlbbtn_Save.Click
        If saveDiagnosis() = True Then
            frmPatientExam.blnChangesMade = True
            Me.Close()
            ' gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.DxSnomed, gloAuditTrail.ActivityType.Save, "DxSnomed Saved. ", gloAuditTrail.ActivityOutCome.Success)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.DxSnomed, gloAuditTrail.ActivityType.Save, "DxSnomed Saved. ", _PatientID, ExamID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

        End If
    End Sub
    Public Function saveDiagnosis() As Boolean
        '  Dim _result As DialogResult
        Dim _isDiagpresent As Boolean = True
        Try
            With c1DxSnomedList

                Dim i As Integer
                Dim lst As myList

                Dim arrList As New ArrayList


                'Dim strICD9Code As String = ""
                'Dim strICD9Desc As String = ""
                'Dim strCPTCode As String = ""
                'Dim strCPTDesc As String = ""
                'Dim strMODCode As String = ""
                'Dim strMODDesc As String = ""
                'Dim nICD9Count As Integer = 0
                'Dim nCPTCount As Integer = 0
                'Dim nModCount As Integer = 0
                'Dim intUnits As System.Decimal

                Dim timed_pt, untimed_pt As String

                For i = 1 To .Rows.Count - 1
                   
                    'If .GetData(i, Col_nICDRevision) = 10 Then
                    '    MessageBox.Show("ICD 9 codes cannot be mixed with ICD 10 codes." & vbNewLine & "Please remove ICD 10 codes before saving. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '    Return False
                    'End If

                    timed_pt = Nothing
                    untimed_pt = Nothing

                    lst = New myList

                    lst.Code = .GetData(i, Col_DxCode)
                    lst.Description = .GetData(i, Col_DxDesc)
                    lst.SnowMadeID = .GetData(i, Col_SnomedCode)
                    lst.SnoDescription = .GetData(i, Col_SnomedDesc)
                    lst.HistoryCategory = .GetData(i, Col_CPTCode)
                    lst.HistoryItem = .GetData(i, Col_CPTDesc)
                    lst.Value = .GetData(i, Col_ModCode)
                    lst.ParameterName = .GetData(i, Col_ModDesc)
                    lst.TemplateResult = .GetData(i, Col_Unit)
                    lst.ICD9Count = .GetData(i, Col_LineNo)
                    lst.nICDRevision = .GetData(i, Col_nICDRevision)

                    Dim obj As New ClsTreatmentDBLayer
                    Try
                        Using dt_pt_billing As DataTable = obj.FetchPTBillingForCPT(ExamID, lst.HistoryCategory, lst.Code)
                            If dt_pt_billing IsNot Nothing Then
                                If dt_pt_billing.Rows.Count > 0 Then
                                    timed_pt = Convert.ToString(dt_pt_billing.Rows(0)("nTimedTherapy"))
                                    untimed_pt = Convert.ToString(dt_pt_billing.Rows(0)("nUnTimedTherapy"))
                                End If
                            End If
                        End Using
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.DxSnomed, gloAuditTrail.ActivityType.None, ex, gloAuditTrail.ActivityOutCome.Failure)
                    Finally
                        If Not IsNothing(obj) Then
                            obj.Dispose()
                            obj = Nothing
                        End If
                    End Try

                    lst.TimedTherapy = timed_pt
                    lst.UnTimedTherapy = untimed_pt

                    arrList.Add(lst)
                    If lst.Code <> "" Then
                        ' _isDiagpresent = True
                    Else
                        _isDiagpresent = False
                    End If


                    lst = Nothing 'Change made to solve memory Leak and word crash issue
                Next
                If _isDiagpresent = False Then
                    MessageBox.Show("Snomed CT will not be added in Problem List as Diagnosis is not present. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'If _Result = Windows.Forms.DialogResult.Yes Then
                    '    frmPatientExam.blnChangesMade = True
                    'ElseIf _Result = Windows.Forms.DialogResult.No Then
                    '    frmPatientExam.blnChangesMade = False
                    '    Exit Sub

                    'End If

                End If
                Dim oclsDiagnosis As New ClsDiagnosisDBLayer
                'save data in ExamICDCPT Table

                oclsDiagnosis.SaveDiagTreatmentAssociation(ExamID, _PatientID, VisitId, arrList, Me, False, True)
                frmPatientExam.Arrlist = arrList
                oclsDiagnosis = Nothing 'Change made to solve memory Leak and word crash issue
            End With
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.DxSnomed, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Private Sub tlbbtn_Close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tlbbtn_Close.Click
        Me.Close()
    End Sub

    Private Sub frmDxSnomed_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objDiagnosisDBLayer As ClsDiagnosisDBLayer = Nothing
        Dim dt As DataTable = Nothing
        Dim dvICD9 As DataView = Nothing
        Try


            c1DxSnomedList.AllowEditing = False

            '06-Mar-15 Aniket: Resolving Bug #80105: gloEMR: Dx-Snomed- application gives exception
            c1DxSnomedList.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None

            objDiagnosisDBLayer = New ClsDiagnosisDBLayer()
            ' flag = 0 - ICD9   flag = 1 - CPT flag = 2 -MOD
            dt = objDiagnosisDBLayer.FetchICD9CPTMod(ExamID, VisitId, "", "", "", 4)
            If Not IsNothing(dt) Then
                dvICD9 = New DataView(dt)

                Dim strICD9(dt.Columns.Count - 1) As String

                For i As Integer = 0 To dt.Columns.Count - 1
                    strICD9.SetValue(dt.Columns(i).ColumnName, i)
                Next
                dt = dvICD9.ToTable(True, strICD9)

                For j As Integer = 0 To dt.Rows.Count - 1
                    c1DxSnomedList.Rows.Add()
                    c1DxSnomedList.SetData(j + 1, Col_DxCode, dt.Rows(j)("sICD9Code"))
                    c1DxSnomedList.SetData(j + 1, Col_DxDesc, dt.Rows(j)("sICD9Description"))
                    If dt.Rows(j)("sICD9Code") <> "" And dt.Rows(j)("sICD9Description") <> "" Then
                        c1DxSnomedList.SetData(j + 1, Col_Diagnosis, Convert.ToString(dt.Rows(j)("sICD9Code")).Trim + " : " + Convert.ToString(dt.Rows(j)("sICD9Description")).Trim)
                    End If

                    c1DxSnomedList.SetData(j + 1, Col_CPTCode, dt.Rows(j)("sCPTCode"))
                    c1DxSnomedList.SetData(j + 1, Col_CPTDesc, dt.Rows(j)("sCPTDescription"))
                    c1DxSnomedList.SetData(j + 1, Col_ModCode, dt.Rows(j)("sModCode"))
                    c1DxSnomedList.SetData(j + 1, Col_ModDesc, dt.Rows(j)("sModDescription"))
                    c1DxSnomedList.SetData(j + 1, Col_Unit, dt.Rows(j)("nUnit"))

                    c1DxSnomedList.SetData(j + 1, Col_SnomedCode, dt.Rows(j)("sSnomedCode"))
                    c1DxSnomedList.SetData(j + 1, Col_SnomedDesc, dt.Rows(j)("sSnomedDesc"))
                    If dt.Rows(j)("sSnomedCode") <> "" And dt.Rows(j)("sSnomedDesc") <> "" Then
                        If Convert.ToString(dt.Rows(j)("sSnomedDesc")).Contains(" - ") Then
                            c1DxSnomedList.SetData(j + 1, Col_Snomed, Convert.ToString(dt.Rows(j)("sSnomedDesc")).Replace("-", ":"))
                        Else
                            c1DxSnomedList.SetData(j + 1, Col_Snomed, dt.Rows(j)("sSnomedCode") + " : " + dt.Rows(j)("sSnomedDesc"))
                        End If

                    End If
                    c1DxSnomedList.SetData(j + 1, Col_LineNo, dt.Rows(j)("nLineNo"))
                    c1DxSnomedList.SetData(j + 1, Col_nICDRevision, dt.Rows(j)("nICDRevision"))
                Next
            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.DxSnomed, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(objDiagnosisDBLayer) Then
                objDiagnosisDBLayer = Nothing
            End If
           
        End Try
    End Sub
End Class