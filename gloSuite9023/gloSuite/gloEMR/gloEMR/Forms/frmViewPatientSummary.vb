Imports gloEMR.gloEMRWord

Public Class frmViewPatientSummary
    Implements IPatientContext


    Private Col_CategoryName As Integer = 0
    ''Private Col_OrderNo As Integer = 1
    Private Col_HiddenOrderNo As Integer = 1
    Private Col_Date As Integer = 2
    Private Col_Name As Integer = 3
    Private Col_Provider As Integer = 4
    Private Col_AssociateID As Integer = 5
    Private Col_AssociateType As Integer = 6
    Private Col_VisitID As Integer = 7
    Private Col_IsFinish As Integer = 8
    Private Col_HiddenDate As Integer = 9
    Private col_TemplateName As Integer = 10
    Dim Col_COUNT As Integer = 11


    Private m_PatientID As Int64
    Dim oclsPatientSummry As clsPatientSummery
    Dim dt As DataTable
    Dim dtExam As DataTable
    Dim dtRadiology As DataTable
    Dim dtLabs As DataTable
    Dim dtScanDoc As DataTable
    Dim arrLabs As New ArrayList
    Dim lst As myList
    Private WithEvents gloUC_PatientStrip1 As gloUserControlLibrary.gloUC_PatientStrip
    Dim WithEvents oShowDocument As gloEDocumentV3.gloEDocV3Management

    Private strPatientCode As String
    Private strPatientFirstName As String
    Private strPatientMiddleName As String
    Private strPatientLastName As String
    Private strPatientDOB As String
    Private strPatientAge As String
    Private strPatientGender As String
    Private strPatientMaritalStatus As String
    ''' <summary>
    ''' this function is used to design the flex grid
    ''' </summary>
    ''' <remarks></remarks>
    ''' 

    Public Sub setGridStyle()
        With PatientSummFlexGrid
            'Dim i As Int16
            Dim _TotalWidth As Single = .Width - 5
            ' Dim cStyle As C1.Win.C1FlexGrid.CellStyle
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Count = Col_COUNT

            .Cols.Fixed = 0

            .AllowEditing = True


            .Cols(Col_CategoryName).Width = _TotalWidth * 0.3
            .Cols(Col_CategoryName).AllowEditing = False
            .SetData(0, Col_CategoryName, "Category")
            '.Cols(Col_CategoryName).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(Col_CategoryName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

            '.Cols(Col_OrderNo).Width = _TotalWidth * 0.2
            '.Cols(Col_OrderNo).AllowEditing = False
            '.SetData(0, Col_OrderNo, "Order No.")
            '.Cols(Col_OrderNo).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .Cols(Col_HiddenOrderNo).Width = _TotalWidth * 0
            .Cols(Col_HiddenOrderNo).AllowEditing = False
            .SetData(0, Col_HiddenOrderNo, "Hidden Order No.")
            .Cols(Col_HiddenOrderNo).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .Cols(Col_Date).Width = _TotalWidth * 0.3
            .Cols(Col_Date).AllowEditing = False
            .SetData(0, Col_Date, "Date")
            .Cols(Col_Date).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .Cols(Col_Name).Width = _TotalWidth * 0.5
            .Cols(Col_Name).AllowEditing = False
            .SetData(0, Col_Name, "Name")
            '.Cols(Col_Name).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(Col_Name).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

            .Cols(col_TemplateName).Width = 0
            .Cols(col_TemplateName).AllowEditing = False
            .SetData(0, col_TemplateName, "Name")
            '.Cols(Col_Name).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(col_TemplateName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

            .Cols(Col_Provider).Width = _TotalWidth * 0.5
            .Cols(Col_Provider).AllowEditing = False
            .SetData(0, Col_Provider, "Provider")
            '.Cols(Col_Provider).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(Col_Provider).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter


            .Cols(Col_AssociateID).Width = _TotalWidth * 0
            .Cols(Col_AssociateID).AllowEditing = False
            .SetData(0, Col_AssociateID, "Associate ID")
            .Cols(Col_AssociateID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .Cols(Col_AssociateType).Width = _TotalWidth * 0
            .Cols(Col_AssociateType).AllowEditing = False
            .SetData(0, Col_AssociateType, "Associate Type")
            .Cols(Col_AssociateType).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .Cols(Col_VisitID).Width = _TotalWidth * 0
            .Cols(Col_VisitID).AllowEditing = False
            .SetData(0, Col_VisitID, "Visit ID")
            .Cols(Col_VisitID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .Cols(Col_IsFinish).Width = _TotalWidth * 0.15
            .Cols(Col_IsFinish).AllowEditing = False
            .SetData(0, Col_IsFinish, "Finished")
            '.Cols(Col_IsFinish).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(Col_IsFinish).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

            .Cols(Col_HiddenDate).Width = _TotalWidth * 0
            .Cols(Col_HiddenDate).AllowEditing = False
            .SetData(0, Col_HiddenDate, "Hidden Date")
            .Cols(Col_HiddenDate).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            'Col_IsFinish
        End With
    End Sub
    Private Function Get_PatientDetails()
        Dim dtPatient As DataTable = Nothing

        Try
            dtPatient = New DataTable
            dtPatient = GetPatientInfo(m_PatientID)
            If IsNothing(dtPatient) = False Then
                If dtPatient.Rows.Count > 0 Then
                    strPatientCode = Convert.ToString(dtPatient.Rows(0)("sPatientCode"))
                    strPatientFirstName = Convert.ToString(dtPatient.Rows(0)("sFirstName"))
                    strPatientLastName = Convert.ToString(dtPatient.Rows(0)("sLastName"))
                    strPatientDOB = Convert.ToString(dtPatient.Rows(0)("dtDOB"))
                    strPatientAge = GetAge(Convert.ToDateTime(dtPatient.Rows(0)("dtDOB")))
                    strPatientGender = Convert.ToString(dtPatient.Rows(0)("sGender"))
                    strPatientMaritalStatus = Convert.ToString(dtPatient.Rows(0)("sMaritalStatus"))

                End If
            End If
        Catch ex As Exception

        Finally
            If IsNothing(dtPatient) = False Then
                dtPatient.Dispose()
                dtPatient = Nothing
            End If


        End Try
        Return Nothing
    End Function
    Private Sub frmViewPatientSummary_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientConsent, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

        Try
            'Me.WindowState = FormWindowState.Maximized
            'CType(Me.MdiParent, gloEMR.MainMenu).ShowHideMainMenu(False, False)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub frmViewPatientSummary_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        gloC1FlexStyle.Style(PatientSummFlexGrid)

        Try
            ''Fixed issue-#13435-: Patient Summary >> If we Double click on any item, it asks for select patient.
            Get_PatientDetails()
            setGridStyle()
            'Line commented by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'm_PatientID = gnPatientID
            'end line commented by dipak.
            FillPatientAssociation()
            Set_PatientDetailStrip()
            'Sanjog - Added on 2011 May 17 for Patient Safety
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, m_PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
            'Sanjog - Added on 2011 May 17 for Patient Safety
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Set_PatientDetailStrip()
        ' '' Add Patient Details Control
        gloUC_PatientStrip1 = New gloUserControlLibrary.gloUC_PatientStrip

        With gloUC_PatientStrip1
            .Dock = DockStyle.Top
            .Padding = New Padding(3, 0, 3, 0)
            '' Pass Paarameters Type of Form
            'Line commented by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            '.ShowDetail(gnPatientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.PatientSummary)
            .ShowDetail(m_PatientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.PatientSummary)
            'end lines modified by  dipak
            .BringToFront()
            pnltls.SendToBack()
            Panel1.BringToFront()
            ''.DTPValue = Format(m_VisitDate, "MM/dd/yyyy")
            .DTPEnabled = False
        End With
        Me.Controls.Add(gloUC_PatientStrip1)

    End Sub

    Public Sub FillPatientAssociation()
        Dim oCategories As gloEDocumentV3.Common.Categories
        Dim oList As New gloEDocumentV3.eDocManager.eDocGetList()
        Dim oDocuments As gloEDocumentV3.Document.BaseDocuments

        oclsPatientSummry = New clsPatientSummery
        '''' Get Patient related Information of Exam,Radiology,labs,Exam
        'dt = New DataTable
        dt = oclsPatientSummry.GetPatientAssociation(m_PatientID)
        '''' Get Patient Exam
        'dtExam = New DataTable
        dtExam = oclsPatientSummry.GetExamforPatient(m_PatientID)
        '''' Get Patient Radiology
        'dtRadiology = New DataTable
        dtRadiology = oclsPatientSummry.GetRadiologyOrderforPatient(m_PatientID)
        '''' Get patient Labs
        'dtLabs = New DataTable
        dtLabs = oclsPatientSummry.GetLabOrderforPatient(m_PatientID)
        '''' Get Patient Scan Document
        'dtScanDoc = New DataTable
        dtScanDoc = oclsPatientSummry.GetScanDocumentforPatient(m_PatientID)
        Dim strFinished As String = ""
        Dim strProviderName As String = ""
        'Dim NewNode As myTreeNode
        If Not IsNothing(dt) Then
            For i As Integer = 0 To dt.Rows.Count - 1
                If dt.Rows(i)("nAssociateType") = PatientAssociatType.Exam Then
                    For j As Integer = 0 To dtExam.Rows.Count - 1
                        If dt.Rows(i)("nAssociateID") = dtExam.Rows(j)("nExamID") Then
                            With PatientSummFlexGrid
                                If .Rows.Count = 1 Then
                                    .Rows.Add()
                                    .SetData(.Rows.Count - 1, Col_CategoryName, "Exams")
                                    .Rows.Add()
                                    .SetData(.Rows.Count - 1, Col_Date, dtExam.Rows(j)("dtDOS"))
                                    .SetData(.Rows.Count - 1, Col_Name, dtExam.Rows(j)("sExamName"))
                                    .SetData(.Rows.Count - 1, Col_AssociateID, dtExam.Rows(j)("nExamID"))
                                    .SetData(.Rows.Count - 1, Col_AssociateType, dt.Rows(i)("nAssociateType"))
                                    .SetData(.Rows.Count - 1, col_TemplateName, dtExam.Rows(j)("sTemplateName"))
                                    .SetData(.Rows.Count - 1, Col_VisitID, dtExam.Rows(j)("nVisitID"))
                                    If dtExam.Rows(j)("bIsFinished") = True Then
                                        strFinished = "Yes"
                                    Else
                                        strFinished = "No"
                                    End If
                                    .SetData(.Rows.Count - 1, Col_IsFinish, strFinished)
                                    If (dtExam.Rows(j)("MiddleName") <> "") Then
                                        strProviderName = dtExam.Rows(j)("FirstName") & " " & dtExam.Rows(j)("MiddleName") & " " & dtExam.Rows(j)("LastName")
                                    Else
                                        strProviderName = dtExam.Rows(j)("FirstName") & " " & dtExam.Rows(j)("LastName")
                                    End If
                                    .SetData(.Rows.Count - 1, Col_Provider, strProviderName)
                                    .SetData(.Rows.Count - 1, Col_HiddenDate, dtExam.Rows(j)("dtDOS"))


                                Else
                                    .Rows.Add()
                                    .SetData(.Rows.Count - 1, Col_Date, dtExam.Rows(j)("dtDOS"))
                                    .SetData(.Rows.Count - 1, Col_Name, dtExam.Rows(j)("sExamName"))
                                    .SetData(.Rows.Count - 1, Col_AssociateID, dtExam.Rows(j)("nExamID"))
                                    .SetData(.Rows.Count - 1, Col_AssociateType, dt.Rows(i)("nAssociateType"))
                                    .SetData(.Rows.Count - 1, Col_VisitID, dtExam.Rows(j)("nVisitID"))
                                    .SetData(.Rows.Count - 1, col_TemplateName, dtExam.Rows(j)("sTemplateName"))
                                    If dtExam.Rows(j)("bIsFinished") = True Then
                                        strFinished = "Yes"
                                    Else
                                        strFinished = "No"
                                    End If
                                    .SetData(.Rows.Count - 1, Col_IsFinish, strFinished)
                                    If (dtExam.Rows(j)("MiddleName") <> "") Then
                                        strProviderName = dtExam.Rows(j)("FirstName") & " " & dtExam.Rows(j)("MiddleName") & " " & dtExam.Rows(j)("LastName")
                                    Else
                                        strProviderName = dtExam.Rows(j)("FirstName") & " " & dtExam.Rows(j)("LastName")
                                    End If
                                    .SetData(.Rows.Count - 1, Col_Provider, strProviderName)
                                    .SetData(.Rows.Count - 1, Col_HiddenDate, dtExam.Rows(j)("dtDOS"))
                                End If

                            End With
                            'For Each mynode As myTreeNode In trvPatientAssoication.Nodes.Item(0).Nodes
                            '    If mynode.Text = "Exam" Then
                            '        NewNode = New myTreeNode
                            '        NewNode.Text = dtExam.Rows(j)("sExamName")
                            '        NewNode.Tag = dtExam.Rows(j)("nExamID")
                            '        mynode.Nodes.Add(NewNode)
                            '    End If
                            'Next
                        End If
                    Next
                ElseIf dt.Rows(i)("nAssociateType") = PatientAssociatType.Radiology Then
                    For j As Integer = 0 To dtRadiology.Rows.Count - 1
                        If dt.Rows(i)("nAssociateID") = dtRadiology.Rows(j)("lm_Order_ID") Then

                            With PatientSummFlexGrid
                                If .Rows.Count <> 1 Then
                                    If .GetData(.Rows.Count - 1, Col_AssociateType) = PatientAssociatType.Exam Or .Rows.Count = 1 Then
                                        .Rows.Add()
                                        .SetData(.Rows.Count - 1, Col_CategoryName, "Order Templates")
                                        .Rows.Add()
                                        .SetData(.Rows.Count - 1, Col_Date, Format(dtRadiology.Rows(j)("lm_OrderDate"), "MM/dd/yyyy"))
                                        .SetData(.Rows.Count - 1, Col_Name, dtRadiology.Rows(j)("lm_test_Name"))
                                        .SetData(.Rows.Count - 1, Col_AssociateID, dtRadiology.Rows(j)("lm_Order_ID"))
                                        .SetData(.Rows.Count - 1, Col_AssociateType, dt.Rows(i)("nAssociateType"))
                                        .SetData(.Rows.Count - 1, Col_VisitID, dtRadiology.Rows(j)("lm_Visit_ID"))

                                        If dtRadiology.Rows(j)("lm_IsFinished") = True Then
                                            strFinished = "Yes"
                                        Else
                                            strFinished = "No"
                                        End If
                                        .SetData(.Rows.Count - 1, Col_IsFinish, strFinished)
                                        If (dtRadiology.Rows(j)("MiddleName") <> "") Then
                                            strProviderName = dtRadiology.Rows(j)("FirstName") & " " & dtRadiology.Rows(j)("MiddleName") & " " & dtRadiology.Rows(j)("LastName")
                                        Else
                                            strProviderName = dtRadiology.Rows(j)("FirstName") & " " & dtRadiology.Rows(j)("LastName")
                                        End If
                                        .SetData(.Rows.Count - 1, Col_Provider, strProviderName)
                                        .SetData(.Rows.Count - 1, Col_HiddenDate, dtRadiology.Rows(j)("lm_OrderDate"))
                                    Else
                                        .Rows.Add()
                                        .SetData(.Rows.Count - 1, Col_Date, Format(dtRadiology.Rows(j)("lm_OrderDate"), "MM/dd/yyyy"))
                                        .SetData(.Rows.Count - 1, Col_Name, dtRadiology.Rows(j)("lm_test_Name"))
                                        .SetData(.Rows.Count - 1, Col_AssociateID, dtRadiology.Rows(j)("lm_Order_ID"))
                                        .SetData(.Rows.Count - 1, Col_AssociateType, dt.Rows(i)("nAssociateType"))
                                        .SetData(.Rows.Count - 1, Col_VisitID, dtRadiology.Rows(j)("lm_Visit_ID"))
                                        If dtRadiology.Rows(j)("lm_IsFinished") = True Then
                                            strFinished = "Yes"
                                        Else
                                            strFinished = "No"
                                        End If
                                        .SetData(.Rows.Count - 1, Col_IsFinish, strFinished)
                                        If (dtRadiology.Rows(j)("MiddleName") <> "") Then
                                            strProviderName = dtRadiology.Rows(j)("FirstName") & " " & dtRadiology.Rows(j)("MiddleName") & " " & dtRadiology.Rows(j)("LastName")
                                        Else
                                            strProviderName = dtRadiology.Rows(j)("FirstName") & " " & dtRadiology.Rows(j)("LastName")
                                        End If
                                        .SetData(.Rows.Count - 1, Col_Provider, strProviderName)
                                        .SetData(.Rows.Count - 1, Col_HiddenDate, dtRadiology.Rows(j)("lm_OrderDate"))
                                    End If
                                Else
                                    .Rows.Add()
                                    .SetData(.Rows.Count - 1, Col_CategoryName, "Order Templates")
                                    .Rows.Add()
                                    .SetData(.Rows.Count - 1, Col_Date, Format(dtRadiology.Rows(j)("lm_OrderDate"), "MM/dd/yyyy"))
                                    .SetData(.Rows.Count - 1, Col_Name, dtRadiology.Rows(j)("lm_test_Name"))
                                    .SetData(.Rows.Count - 1, Col_AssociateID, dtRadiology.Rows(j)("lm_Order_ID"))
                                    .SetData(.Rows.Count - 1, Col_AssociateType, dt.Rows(i)("nAssociateType"))
                                    .SetData(.Rows.Count - 1, Col_VisitID, dtRadiology.Rows(j)("lm_Visit_ID"))

                                    If dtRadiology.Rows(j)("lm_IsFinished") = True Then
                                        strFinished = "Yes"
                                    Else
                                        strFinished = "No"
                                    End If
                                    .SetData(.Rows.Count - 1, Col_IsFinish, strFinished)
                                    If (dtRadiology.Rows(j)("MiddleName") <> "") Then
                                        strProviderName = dtRadiology.Rows(j)("FirstName") & " " & dtRadiology.Rows(j)("MiddleName") & " " & dtRadiology.Rows(j)("LastName")
                                    Else
                                        strProviderName = dtRadiology.Rows(j)("FirstName") & " " & dtRadiology.Rows(j)("LastName")
                                    End If
                                    .SetData(.Rows.Count - 1, Col_Provider, strProviderName)
                                    .SetData(.Rows.Count - 1, Col_HiddenDate, dtRadiology.Rows(j)("lm_OrderDate"))
                                End If
                            End With
                            'For Each mynode As myTreeNode In trvPatientAssoication.Nodes.Item(0).Nodes
                            '    If mynode.Text = "Radiology" Then
                            '        NewNode = New myTreeNode
                            '        NewNode.Text = dtRadiology.Rows(j)("lm_test_Name")
                            '        NewNode.Tag = dtRadiology.Rows(j)("lm_Order_ID")
                            '        mynode.Nodes.Add(NewNode)
                            '    End If
                            'Next
                        End If
                    Next
                ElseIf dt.Rows(i)("nAssociateType") = PatientAssociatType.Labs Then
                    For j As Integer = 0 To dtLabs.Rows.Count - 1
                        If dt.Rows(i)("nAssociateID") = dtLabs.Rows(j)("labom_OrderID") Then
                            With PatientSummFlexGrid
                                If .Rows.Count <> 1 Then
                                    If .GetData(.Rows.Count - 1, Col_AssociateType) = PatientAssociatType.Exam Or .GetData(.Rows.Count - 1, Col_AssociateType) = PatientAssociatType.Radiology Or .Rows.Count = 1 Then
                                        .Rows.Add()
                                        .SetData(.Rows.Count - 1, Col_CategoryName, "Orders")
                                        '.Rows.Add()
                                        '.SetData(.Rows.Count - 1, Col_OrderNo, dtLabs.Rows(j)("labom_OrderNoPrefix") & "-" & dtLabs.Rows(j)("labom_OrderNoID"))
                                        '.SetData(.Rows.Count - 1, Col_AssociateID, dtLabs.Rows(j)("labom_OrderID"))
                                        '.SetData(.Rows.Count - 1, Col_VisitID, dtLabs.Rows(j)("labom_VisitID"))
                                        '.SetData(.Rows.Count - 1, Col_AssociateType, dt.Rows(i)("nAssociateType"))
                                        '.SetData(.Rows.Count - 1, Col_HiddenOrderNo, dtLabs.Rows(j)("labom_OrderNoPrefix") & "-" & dtLabs.Rows(j)("labom_OrderNoID"))
                                        .Rows.Add()
                                        .SetData(.Rows.Count - 1, Col_HiddenOrderNo, dtLabs.Rows(j)("labom_OrderNoPrefix") & "-" & dtLabs.Rows(j)("labom_OrderNoID"))
                                        .SetData(.Rows.Count - 1, Col_Date, Format(dtLabs.Rows(j)("labom_TransactionDate"), "MM/dd/yyyy"))
                                        .SetData(.Rows.Count - 1, Col_Name, dtLabs.Rows(j)("labom_OrderNoPrefix") & "-" & dtLabs.Rows(j)("labom_OrderNoID") & " - " & dtLabs.Rows(j)("labtm_Name"))
                                        .SetData(.Rows.Count - 1, Col_AssociateID, dtLabs.Rows(j)("labom_OrderID"))
                                        .SetData(.Rows.Count - 1, Col_AssociateType, dt.Rows(i)("nAssociateType"))
                                        .SetData(.Rows.Count - 1, Col_VisitID, dtLabs.Rows(j)("labom_VisitID"))
                                        '
                                        If (dtLabs.Rows(j)("MiddleName") <> "") Then
                                            strProviderName = dtLabs.Rows(j)("FirstName") & " " & dtLabs.Rows(j)("MiddleName") & " " & dtLabs.Rows(j)("LastName")
                                        Else
                                            strProviderName = dtLabs.Rows(j)("FirstName") & " " & dtLabs.Rows(j)("LastName")
                                        End If
                                        .SetData(.Rows.Count - 1, Col_Provider, strProviderName)
                                        .SetData(.Rows.Count - 1, Col_HiddenDate, dtLabs.Rows(j)("labom_TransactionDate"))
                                    Else
                                        If .GetData((.Rows.Count - 1), Col_HiddenOrderNo) = dtLabs.Rows(j)("labom_OrderNoPrefix") & "-" & dtLabs.Rows(j)("labom_OrderNoID") Then
                                            '.Rows.Add()
                                            '.SetData(.Rows.Count - 1, Col_OrderNo, dtLabs.Rows(j)("labom_OrderNoPrefix") & "-" & dtLabs.Rows(j)("labom_OrderNoID"))
                                            .Rows.Add()
                                            .SetData(.Rows.Count - 1, Col_HiddenOrderNo, dtLabs.Rows(j)("labom_OrderNoPrefix") & "-" & dtLabs.Rows(j)("labom_OrderNoID"))
                                            .SetData(.Rows.Count - 1, Col_Date, Format(dtLabs.Rows(j)("labom_TransactionDate"), "MM/dd/yyyy"))
                                            .SetData(.Rows.Count - 1, Col_Name, dtLabs.Rows(j)("labom_OrderNoPrefix") & "-" & dtLabs.Rows(j)("labom_OrderNoID") & " - " & dtLabs.Rows(j)("labtm_Name"))
                                            .SetData(.Rows.Count - 1, Col_AssociateID, dtLabs.Rows(j)("labom_OrderID"))
                                            .SetData(.Rows.Count - 1, Col_AssociateType, dt.Rows(i)("nAssociateType"))
                                            .SetData(.Rows.Count - 1, Col_VisitID, dtLabs.Rows(j)("labom_VisitID"))
                                            If (dtLabs.Rows(j)("MiddleName") <> "") Then
                                                strProviderName = dtLabs.Rows(j)("FirstName") & " " & dtLabs.Rows(j)("MiddleName") & " " & dtLabs.Rows(j)("LastName")
                                            Else
                                                strProviderName = dtLabs.Rows(j)("FirstName") & " " & dtLabs.Rows(j)("LastName")
                                            End If
                                            .SetData(.Rows.Count - 1, Col_Provider, strProviderName)
                                            .SetData(.Rows.Count - 1, Col_HiddenDate, dtLabs.Rows(j)("labom_TransactionDate"))
                                        Else
                                            '.Rows.Add()
                                            '.SetData(.Rows.Count - 1, Col_OrderNo, dtLabs.Rows(j)("labom_OrderNoPrefix") & "-" & dtLabs.Rows(j)("labom_OrderNoID"))
                                            '.SetData(.Rows.Count - 1, Col_AssociateID, dtLabs.Rows(j)("labom_OrderID"))
                                            '.SetData(.Rows.Count - 1, Col_VisitID, dtLabs.Rows(j)("labom_VisitID"))
                                            '.SetData(.Rows.Count - 1, Col_AssociateType, dt.Rows(i)("nAssociateType"))
                                            '.SetData(.Rows.Count - 1, Col_HiddenOrderNo, dtLabs.Rows(j)("labom_OrderNoPrefix") & "-" & dtLabs.Rows(j)("labom_OrderNoID"))
                                            .Rows.Add()
                                            .SetData(.Rows.Count - 1, Col_HiddenOrderNo, dtLabs.Rows(j)("labom_OrderNoPrefix") & "-" & dtLabs.Rows(j)("labom_OrderNoID"))
                                            .SetData(.Rows.Count - 1, Col_Date, Format(dtLabs.Rows(j)("labom_TransactionDate"), "MM/dd/yyyy"))
                                            .SetData(.Rows.Count - 1, Col_Name, dtLabs.Rows(j)("labom_OrderNoPrefix") & "-" & dtLabs.Rows(j)("labom_OrderNoID") & " - " & dtLabs.Rows(j)("labtm_Name"))
                                            .SetData(.Rows.Count - 1, Col_AssociateID, dtLabs.Rows(j)("labom_OrderID"))
                                            .SetData(.Rows.Count - 1, Col_AssociateType, dt.Rows(i)("nAssociateType"))
                                            .SetData(.Rows.Count - 1, Col_VisitID, dtLabs.Rows(j)("labom_VisitID"))
                                            If (dtLabs.Rows(j)("MiddleName") <> "") Then
                                                strProviderName = dtLabs.Rows(j)("FirstName") & " " & dtLabs.Rows(j)("MiddleName") & " " & dtLabs.Rows(j)("LastName")
                                            Else
                                                strProviderName = dtLabs.Rows(j)("FirstName") & " " & dtLabs.Rows(j)("LastName")
                                            End If
                                            .SetData(.Rows.Count - 1, Col_Provider, strProviderName)
                                            .SetData(.Rows.Count - 1, Col_HiddenDate, dtLabs.Rows(j)("labom_TransactionDate"))
                                        End If

                                    End If
                                Else
                                    .Rows.Add()
                                    .SetData(.Rows.Count - 1, Col_CategoryName, "Orders")
                                    '.Rows.Add()
                                    '.SetData(.Rows.Count - 1, Col_OrderNo, dtLabs.Rows(j)("labom_OrderNoPrefix") & "-" & dtLabs.Rows(j)("labom_OrderNoID"))
                                    '.SetData(.Rows.Count - 1, Col_AssociateID, dtLabs.Rows(j)("labom_OrderID"))
                                    '.SetData(.Rows.Count - 1, Col_VisitID, dtLabs.Rows(j)("labom_VisitID"))
                                    '.SetData(.Rows.Count - 1, Col_AssociateType, dt.Rows(i)("nAssociateType"))
                                    '.SetData(.Rows.Count - 1, Col_HiddenOrderNo, dtLabs.Rows(j)("labom_OrderNoPrefix") & "-" & dtLabs.Rows(j)("labom_OrderNoID"))
                                    .Rows.Add()
                                    .SetData(.Rows.Count - 1, Col_HiddenOrderNo, dtLabs.Rows(j)("labom_OrderNoPrefix") & "-" & dtLabs.Rows(j)("labom_OrderNoID"))
                                    .SetData(.Rows.Count - 1, Col_Date, Format(dtLabs.Rows(j)("labom_TransactionDate"), "MM/dd/yyyy"))
                                    .SetData(.Rows.Count - 1, Col_Name, dtLabs.Rows(j)("labom_OrderNoPrefix") & "-" & dtLabs.Rows(j)("labom_OrderNoID") & " - " & dtLabs.Rows(j)("labtm_Name"))
                                    .SetData(.Rows.Count - 1, Col_AssociateID, dtLabs.Rows(j)("labom_OrderID"))
                                    .SetData(.Rows.Count - 1, Col_AssociateType, dt.Rows(i)("nAssociateType"))
                                    .SetData(.Rows.Count - 1, Col_VisitID, dtLabs.Rows(j)("labom_VisitID"))
                                    '
                                    If (dtLabs.Rows(j)("MiddleName") <> "") Then
                                        strProviderName = dtLabs.Rows(j)("FirstName") & " " & dtLabs.Rows(j)("MiddleName") & " " & dtLabs.Rows(j)("LastName")
                                    Else

                                        strProviderName = dtLabs.Rows(j)("FirstName") & " " & dtLabs.Rows(j)("LastName")
                                    End If
                                    .SetData(.Rows.Count - 1, Col_Provider, strProviderName)
                                    .SetData(.Rows.Count - 1, Col_HiddenDate, dtLabs.Rows(j)("labom_TransactionDate"))
                                End If
                            End With
                            'For Each mynode As myTreeNode In trvPatientAssoication.Nodes.Item(0).Nodes
                            '    If mynode.Text = "Orders" Then
                            '        NewNode = New myTreeNode
                            '        NewNode.Text = dtLabs.Rows(j)("labtm_Name")
                            '        NewNode.Tag = dtLabs.Rows(j)("labom_OrderID")
                            '        mynode.Nodes.Add(NewNode)
                            '    End If
                            'Next
                        End If
                    Next
                ElseIf dt.Rows(i)("nAssociateType") = PatientAssociatType.ScanDocument Then

                    oCategories = oList.GetCategories(gClinicID)
                    If Not oCategories Is Nothing Then

                        For j As Int16 = 0 To oCategories.Count - 1
                            '                   oDocuments = New gloEDocumentV3.Document.BaseDocuments()
                            'Line commented by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                            'oDocuments = oList.GetBaseDocuments(gnPatientID, oCategories(j).CategoryName, gClinicID)
                            oDocuments = oList.GetBaseDocuments(m_PatientID, oCategories(j).CategoryName, gClinicID)
                            'end line modified by dipak
                            If Not oDocuments Is Nothing Then
                                For k As Int16 = 0 To oDocuments.Count - 1
                                    If dt.Rows(i)("nAssociateID") = oDocuments(k).EDocumentID Then ''  dtScanDoc.Rows(k)("DocumentID") Then
                                        With PatientSummFlexGrid
                                            If .Rows.Count <> 1 Then
                                                If .GetData(.Rows.Count - 1, Col_AssociateType) = PatientAssociatType.Exam Or .GetData(.Rows.Count - 1, Col_AssociateType) = PatientAssociatType.Radiology Or .GetData(.Rows.Count - 1, Col_AssociateType) = PatientAssociatType.Labs Or .Rows.Count = 1 Then
                                                    .Rows.Add()
                                                    .SetData(.Rows.Count - 1, Col_CategoryName, "Scanned Documents")
                                                    .Rows.Add()
                                                    .SetData(.Rows.Count - 1, Col_Date, Format(oDocuments(k).CreatedDateTime, "MM/dd/yyyy")) ''Format(dtScanDoc.Rows(j)("ModifyDateTime"), "MM/dd/yyyy"))
                                                    .SetData(.Rows.Count - 1, Col_Name, oDocuments(k).DocumentName)  ''dtScanDoc.Rows(j)("DocumentName"))
                                                    .SetData(.Rows.Count - 1, Col_AssociateID, oDocuments(k).EDocumentID) ''dtScanDoc.Rows(j)("DocumentID"))
                                                    .SetData(.Rows.Count - 1, Col_AssociateType, dt.Rows(i)("nAssociateType"))
                                                    .SetData(.Rows.Count - 1, Col_VisitID, oDocuments(k).EContainers(0).EContainerID) '' dtScanDoc.Rows(j)("DocumentFileName"))
                                                    .SetData(.Rows.Count - 1, Col_HiddenDate, oDocuments(k).CreatedDateTime) '' dtScanDoc.Rows(j)("ModifyDateTime"))
                                                    .SetData(.Rows.Count - 1, Col_IsFinish, oDocuments(k).Year) '''' IsFinish Column is used to store Year of the Docuemnt
                                                    ''DocumentFileName
                                                Else
                                                    .Rows.Add()
                                                    .SetData(.Rows.Count - 1, Col_Date, Format(oDocuments(k).CreatedDateTime, "MM/dd/yyyy")) '' Format(dtScanDoc.Rows(j)("ModifyDateTime"), "MM/dd/yyyy"))
                                                    .SetData(.Rows.Count - 1, Col_Name, oDocuments(k).DocumentName) ''dtScanDoc.Rows(j)("DocumentName"))
                                                    .SetData(.Rows.Count - 1, Col_AssociateID, oDocuments(k).EDocumentID) ''dtScanDoc.Rows(j)("DocumentID"))
                                                    .SetData(.Rows.Count - 1, Col_AssociateType, dt.Rows(i)("nAssociateType"))
                                                    .SetData(.Rows.Count - 1, Col_VisitID, oDocuments(k).EContainers(0).EContainerID) ''dtScanDoc.Rows(j)("DocumentFileName"))
                                                    .SetData(.Rows.Count - 1, Col_HiddenDate, oDocuments(k).CreatedDateTime) '' dtScanDoc.Rows(j)("ModifyDateTime"))
                                                    .SetData(.Rows.Count - 1, Col_IsFinish, oDocuments(k).Year) '''' IsFinish Column is used to store Year of the Docuemnt
                                                End If
                                            Else
                                                .Rows.Add()
                                                .SetData(.Rows.Count - 1, Col_CategoryName, "Scanned Documents")
                                                .Rows.Add()
                                                .SetData(.Rows.Count - 1, Col_Date, Format(oDocuments(k).CreatedDateTime, "MM/dd/yyyy")) '' Format(dtScanDoc.Rows(j)("ModifyDateTime"), "MM/dd/yyyy"))
                                                .SetData(.Rows.Count - 1, Col_Name, oDocuments(k).DocumentName) '' dtScanDoc.Rows(j)("DocumentName"))
                                                .SetData(.Rows.Count - 1, Col_AssociateID, oDocuments(k).EDocumentID) ''dtScanDoc.Rows(j)("DocumentID"))
                                                .SetData(.Rows.Count - 1, Col_AssociateType, dt.Rows(i)("nAssociateType"))
                                                .SetData(.Rows.Count - 1, Col_VisitID, oDocuments(k).EContainers(0).EContainerID) '' dtScanDoc.Rows(j)("DocumentFileName"))
                                                .SetData(.Rows.Count - 1, Col_HiddenDate, oDocuments(k).CreatedDateTime) '' dtScanDoc.Rows(j)("ModifyDateTime"))
                                                .SetData(.Rows.Count - 1, Col_IsFinish, oDocuments(k).Year) '''' IsFinish Column is used to store Year of the Docuemnt
                                            End If
                                        End With
                                        'For Each mynode As myTreeNode In trvPatientAssoication.Nodes.Item(0).Nodes
                                        '    If mynode.Text = "Scan Document" Then
                                        '        NewNode = New myTreeNode
                                        '        NewNode.Text = dtScanDoc.Rows(j)("DocumentName")
                                        '        NewNode.Tag = dtScanDoc.Rows(j)("DocumentID")
                                        '        mynode.Nodes.Add(NewNode)
                                        '    End If
                                        'Next
                                    End If
                                Next
                            End If
                            oDocuments.Dispose()
                        Next
                        oCategories.Dispose()
                    End If

                End If

            Next
        End If
        oList.Dispose()
        oList = Nothing
    End Sub

    Private Sub PatientSummFlexGrid_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles PatientSummFlexGrid.DoubleClick
        Try

            '******Shweta 20090828 *********'
            'To check exeception related to word
            If CheckWordForException() = False Then
                Exit Sub
            End If
            'End Shweta

            With PatientSummFlexGrid
                If .Rows.Count = 1 Then
                    Exit Sub
                End If
                Dim int As Integer = .Row
                If .GetData(.Row, Col_AssociateType) = PatientAssociatType.Exam Then
                    Dim nPastExamID As Long
                    Dim nVisitID As Long
                    Dim dtDOS As DateTime
                    Dim strExamName As String
                    Dim strTemplateName As String
                    ''Dim em As System.Windows.Forms.MouseEventArgs
                    nPastExamID = .GetData(.Row, Col_AssociateID)
                    nVisitID = .GetData(.Row, Col_VisitID)
                    dtDOS = .GetData(.Row, Col_HiddenDate)
                    strExamName = .GetData(.Row, Col_Name)
                    strTemplateName = .GetData(.Row, col_TemplateName)
                    Dim blnFinished As Boolean
                    If .GetData(.Row, Col_IsFinish) = "Yes" Then
                        blnFinished = True
                    Else
                        blnFinished = False
                    End If
                    ShowPastExam(nPastExamID, m_PatientID, nVisitID, dtDOS, strExamName, blnFinished, strTemplateName, strPatientCode)
                ElseIf .GetData(.Row, Col_AssociateType) = PatientAssociatType.Radiology Then
                    Dim nVisitId As Long
                    Dim VisitDate As String

                    nVisitId = .GetData(.Row, Col_VisitID)
                    VisitDate = .GetData(.Row, Col_HiddenDate)


                    ' '' <><><> Record Level Locking <><><><> 
                    ' '' Mahesh - 20070724 
                    Dim blnRecordLock As Boolean = False
                    If gblnRecordLocking = True Then
                        Dim mydt As mytable
                        'Line commented by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                        'mydt = Scan_n_Lock_Transaction(TrnType.Radiology, gnPatientID, nVisitId, VisitDate)
                        mydt = Scan_n_Lock_Transaction(TrnType.Radiology, m_PatientID, nVisitId, VisitDate)
                        If (IsNothing(mydt) = False) Then
                            'end modification by dipak.
                            If mydt.Code <> gstrLoginName OrElse mydt.Description <> gstrClientMachineName Then
                                If MessageBox.Show("This radiology order is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot modify it now. Do you want to open it?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                                    blnRecordLock = True
                                Else
                                    If (IsNothing(mydt) = False) Then
                                        mydt.Dispose()
                                        mydt = Nothing
                                    End If
                                    Exit Sub
                                End If
                            End If
                            If (IsNothing(mydt) = False) Then
                                mydt.Dispose()
                                mydt = Nothing
                            End If
                        End If

                    End If

                    Dim frm As frm_LM_Orders
                    frm = frm_LM_Orders.GetInstance(nVisitId, VisitDate, m_PatientID, 1, blnRecordLock)

                    If IsNothing(frm) = True Then
                        Exit Sub
                    End If

                    With frm
                        'Me.pnlLeft.Visible = False
                        ''Me.pnlRights.Visible = False
                        'Me.Splitter1.Visible = False
                        ''''' Hide Tool Bar Mahesh 20070613
                        'Me.pnlMenu.Visible = False
                        CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
                        .MdiParent = CType(Me.MdiParent, MainMenu)
                        '.dtOrderTime.Enabled = False
                        .WindowState = FormWindowState.Maximized
                        .Show()
                    End With

                ElseIf .GetData(.Row, Col_AssociateType) = PatientAssociatType.Labs Then
                    'lst = New myList
                    'lst.Value = .GetData(.Row, Col_Name)
                    'lst.ID = .GetData(.Row, Col_AssociateID)
                    'lst.IsFinished = False
                    'lst.Index = 0 ''.GetData(.Row, Col_COUNT)
                    'arrLabs.Add(lst)
                    Dim arrOrder() As String
                    arrOrder = Split(.GetData(.Row, Col_HiddenOrderNo), "-")

                    'Madan added on 20100406-- added for viewing gloLab
                    'Added by madan in 20100619
                    'If gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_hsilabel <> "" Then
                    Dim _OrderID = PatientSummFlexGrid.GetData(PatientSummFlexGrid.Row, Col_AssociateID)
                    Dim frm_viewgloLab As New gloEmdeonInterface.Forms.frmViewgloLab(_OrderID, m_PatientID)
                    With frm_viewgloLab.LabOrderParameter
                        .OrderID = _OrderID
                        .OrderNumberID = arrOrder.GetValue(1)
                        .OrderNumberPrefix = arrOrder.GetValue(0) ''"ORD"
                        .PatientID = m_PatientID
                        .VisitID = PatientSummFlexGrid.GetData(PatientSummFlexGrid.Row, Col_VisitID) 'gnVisitID
                        .TransactionDate = Now
                        .CloseAfterSave = True
                    End With
                    With frm_viewgloLab
                        .StartPosition = FormStartPosition.CenterScreen
                        .WindowState = FormWindowState.Maximized
                        .BringToFront()
                        .ShowInTaskbar = False
                        .MdiParent = Me.MdiParent
                        .Show()
                    End With

                    ' 'Removed as per new lab changes in 5050-- AS we have implemented old labs.
                    'Else
                    '    Dim frm As New frmLab_RequestOrder
                    '    With frm.LabOrderParameter
                    '        .IsEditMode = False
                    '        .OrderID = PatientSummFlexGrid.GetData(PatientSummFlexGrid.Row, Col_AssociateID)
                    '        .OrderNumberID = arrOrder.GetValue(1)
                    '        .OrderNumberPrefix = arrOrder.GetValue(0) ''"ORD"
                    '        .PatientID = m_PatientID
                    '        .VisitID = PatientSummFlexGrid.GetData(PatientSummFlexGrid.Row, Col_VisitID) 'gnVisitID
                    '        .TransactionDate = Now
                    '        .CloseAfterSave = True
                    '    End With
                    '    With frm
                    '        ''pass the collection of labs to be given
                    '        '._arrLabs = arrLabs
                    '        .bIsOpenfrmOutstanding = True
                    '        .OrderID = PatientSummFlexGrid.GetData(PatientSummFlexGrid.Row, Col_AssociateID)
                    '        .VisitID = PatientSummFlexGrid.GetData(PatientSummFlexGrid.Row, Col_VisitID)
                    '        .StartPosition = FormStartPosition.CenterScreen
                    '        .WindowState = FormWindowState.Maximized
                    '        .BringToFront()
                    '        .ShowInTaskbar = False
                    '        .ShowDialog(Me.Owner)
                    '    End With
                    'End If
                ElseIf .GetData(.Row, Col_AssociateType) = PatientAssociatType.ScanDocument Then
                    Dim _DocID As Long = 0
                    Dim _Year As String = ""
                    Try
                        _DocID = Convert.ToInt64(.GetData(.Row, Col_AssociateID).ToString())
                        _Year = .GetData(.Row, Col_IsFinish).ToString()
                    Catch ex As Exception

                    End Try

                    If _DocID > 0 And _Year <> "" Then
                        If IsNothing(oShowDocument) Then
                            oShowDocument = New gloEDocumentV3.gloEDocV3Management
                        End If

                        'Line commented by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                        'oShowDocument.ShowEDocument(gnPatientID, gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ViewDocumentForExternalModule, CType(Me.ParentForm, MainMenu), gloEDocumentV3.Enumeration.enum_OpenExternalSource.ViewPatientSummary, _DocID)


                        oShowDocument.oPatientExam = New clsPatientExams
                        oShowDocument.oPatientMessages = New clsMessage
                        oShowDocument.oPatientLetters = New clsPatientLetters
                        oShowDocument.oNurseNotes = New clsNurseNotes
                        oShowDocument.oHistory = New clsPatientHistory
                        oShowDocument.oLabs = New clsLabs
                        oShowDocument.oDMS = New gloEDocumentV3.eDocManager.eDocGetList()
                        oShowDocument.oRxmed = New clsPatientDetails
                        oShowDocument.oOrders = New clsPatientDetails
                        oShowDocument.oProblemList = New clsPatientProblemList
                        oShowDocument.oCriteria = New DocCriteria
                        oShowDocument.oWord = New clsWordDocument


                        Dim isItDialog As Boolean = oShowDocument.ShowEDocument(m_PatientID, gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ViewDocumentForExternalModule, CType(Me.ParentForm, MainMenu), gloEDocumentV3.Enumeration.enum_OpenExternalSource.ViewPatientSummary, _DocID)
                        'end modification by dipak.
                        If (isItDialog = True) Then
                            If (isItDialog = True) Then


                                If (IsNothing(oShowDocument.oPatientExam) = False) Then
                                    DirectCast(oShowDocument.oPatientExam, clsPatientExams).Dispose()
                                    oShowDocument.oPatientExam = Nothing
                                End If
                                If (IsNothing(oShowDocument.oPatientMessages) = False) Then
                                    DirectCast(oShowDocument.oPatientMessages, clsMessage).Dispose()
                                    oShowDocument.oPatientMessages = Nothing
                                End If
                                If (IsNothing(oShowDocument.oPatientLetters) = False) Then
                                    DirectCast(oShowDocument.oPatientLetters, clsPatientLetters).Dispose()
                                    oShowDocument.oPatientLetters = Nothing
                                End If
                                If (IsNothing(oShowDocument.oNurseNotes) = False) Then
                                    DirectCast(oShowDocument.oNurseNotes, clsNurseNotes).Dispose()
                                    oShowDocument.oNurseNotes = Nothing
                                End If
                                If (IsNothing(oShowDocument.oHistory) = False) Then
                                    DirectCast(oShowDocument.oHistory, clsPatientHistory).Dispose()
                                    oShowDocument.oHistory = Nothing
                                End If
                                If (IsNothing(oShowDocument.oLabs) = False) Then
                                    DirectCast(oShowDocument.oLabs, clsLabs).Dispose()
                                    oShowDocument.oLabs = Nothing
                                End If
                                If (IsNothing(oShowDocument.oDMS) = False) Then
                                    DirectCast(oShowDocument.oDMS, gloEDocumentV3.eDocManager.eDocGetList).Dispose()
                                    oShowDocument.oDMS = Nothing
                                End If
                                If (IsNothing(oShowDocument.oRxmed) = False) Then
                                    DirectCast(oShowDocument.oRxmed, clsPatientDetails).Dispose()
                                    oShowDocument.oRxmed = Nothing
                                End If
                                If (IsNothing(oShowDocument.oOrders) = False) Then
                                    DirectCast(oShowDocument.oOrders, clsPatientDetails).Dispose()
                                    oShowDocument.oOrders = Nothing
                                End If
                                If (IsNothing(oShowDocument.oProblemList) = False) Then
                                    DirectCast(oShowDocument.oProblemList, clsPatientProblemList).Dispose()
                                    oShowDocument.oProblemList = Nothing
                                End If

                                If (IsNothing(oShowDocument.oCriteria) = False) Then
                                    DirectCast(oShowDocument.oCriteria, DocCriteria).Dispose()
                                    oShowDocument.oCriteria = Nothing
                                End If

                                oShowDocument.Dispose()
                            End If
                            oShowDocument = Nothing
                        End If
                        

                    End If


                    'Dim _DocumentFileName As String = .GetData(.Row, Col_VisitID)   ''c1CategorisedDocuments.GetData(c1CategorisedDocuments.RowSel, COL_CAT_FILENAME)
                    'Dim oViewDocument As New frmDMS_ViewDocument
                    'oViewDocument.pnlDocument.Visible = False
                    'oViewDocument._DMSPatientID = m_PatientID
                    'oViewDocument._DMSDocumentFileName = _DocumentFileName
                    ''pnlLeft.Visible = False
                    'oViewDocument.MdiParent = CType(Me.MdiParent, MainMenu)
                    'oViewDocument.WindowState = FormWindowState.Maximized
                    'oViewDocument.Show()
                    'oViewDocument = Nothing
                End If
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ShowPastExam(ByVal ExamID As Long, ByVal PatientId As Int64, ByVal VisitID As Long, ByVal DOS As String, ByVal ExamName As String, ByVal blnFinished As Boolean, ByVal strTemplateName As String, Optional ByVal PatientCode As String = "")
        Try

            If Trim(strPatientFirstName) <> "" Then

                '''''<><><><><> Check Patient Status <><><><><><>''''
                ''''' 20070125 -Mahesh
                'Line commented by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                'If CheckPatientStatus(gnPatientID) = False Then
                'If CheckPatientStatus(PatientId) = False Then
                '    Exit Sub
                'End If
                If MainMenu.IsAccess(False, PatientId) = False Then
                    Exit Sub
                End If
                'end modification by dipak
                '''''<><><><><> Check Patient Status <><><><><><>''''

                If Not blnFinished Then
                    Dim objExam As New clsPatientExams
                    objExam.SetProviderExam(ExamID)
                    objExam.Dispose()
                    objExam = Nothing
                End If

                Me.Cursor = Cursors.WaitCursor
                'Line commented by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                'Dim frm As New frmPatientExam(gnPatientID, VisitID)
                Dim frm As New frmPatientExam(PatientId, VisitID)
                'end modification by dipak 
                AddHandler frm.FormClosed, AddressOf On_ExamClosed
                With frm
                    .Hide()
                    .blnModify = True
                    .Text = "Past Exams"
                    Dim sender As Object = Nothing
                    Dim e As System.EventArgs = Nothing
                    .cmdPastExam_Click(sender, e)
                    'Line commented by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                    '.PatientID = gnPatientID
                    .PatientID = PatientId
                    'end modification by dipak

                    .pnlPastExam.Visible = True
                    ' .chkShowPreview.Visible = True
                    .pnlPastExam.Visible = True
                    .TemplateName = strTemplateName
                    '' IF CONDITION BY SUDHIR 20090722 ''
                    If .OpenPastExam(ExamID, VisitID, Convert.ToDateTime(DOS), ExamName.Trim, blnFinished) Then


                        CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
                        CType(Me.MdiParent, MainMenu).pnlMainToolBar.Visible = False
                        .MdiParent = CType(Me.MdiParent, MainMenu)
                        .IsPastExam = True
                        .Show()
                        If .ExamViewMode Then
                            .ViewExam(ExamID)
                        Else
                            .OpenPastExamContents(ExamID, blnFinished)
                        End If

                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, "Exam Opened", gloAuditTrail.ActivityOutCome.Success)
                    Else

                        RemoveHandler frm.FormClosed, AddressOf On_ExamClosed
                        
                        frm.Dispose()
                        frm = Nothing

                    End If
                End With
                Me.Cursor = Cursors.Default
            Else
                Me.Cursor = Cursors.Default
                MessageBox.Show("Select the Patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub On_ExamClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs)
        Dim frm As frmPatientExam = Nothing

        Try
            frm = DirectCast(sender, frmPatientExam)
        Catch ex As Exception

        End Try
        Try
            If (IsNothing(frm) = False) Then
                RemoveHandler frm.FormClosed, AddressOf On_ExamClosed
            End If
            If (IsNothing(frm) = False) Then
                frm.Close()
            End If
            If (IsNothing(frm) = False) Then
                frm.Dispose()
                frm = Nothing
            End If

        Catch ex As Exception

        End Try
        Try
            PatientSummFlexGrid.Rows.Count = 1
            FillPatientAssociation()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tlsSummary_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsSummary.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Close"
                    'Me.Close()
                    gloWord.WordDialogBoxBackgroundCloser.Close(Me)
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PatientSummFlexGrid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PatientSummFlexGrid.Click

    End Sub

    Private Sub oShowDocument_EvnRefreshDocuments() Handles oShowDocument.EvnRefreshDocuments

    End Sub

    Private Sub PatientSummFlexGrid_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PatientSummFlexGrid.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Public Sub New(ByVal PatientID As Long)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_PatientID = PatientID
    End Sub
    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return m_PatientID  'Curent patient variable(Local variable) for this module 
        End Get
    End Property

End Class