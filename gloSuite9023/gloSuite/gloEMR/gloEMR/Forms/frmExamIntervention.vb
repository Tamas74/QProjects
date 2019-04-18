Public Class frmExamIntervention

    Private mExamID As Int64 = 0
    Private mVisitID As Int64 = 0
    Private mPatientID As Int64 = 0

#Region " C1 Constants "
    Private Const Col_Interv_Select = 0
    Private Const Col_Interv_InterventionID = 1
    Private Const Col_Interv_Intervention = 2
    Private Const Col_Interv_Count = 3
#End Region

#Region " Constructor "

    Public Sub New(ByVal ExamID As Int64, ByVal VisitID As Int64, ByVal PatientID As Int64)
        mExamID = ExamID
        mVisitID = VisitID
        mPatientID = PatientID
        InitializeComponent()
    End Sub

#End Region

#Region " Private Functions "

    Private Sub DesignGrid()
        Try
            c1Intervention.Rows.Count = 1
            c1Intervention.Rows.Fixed = 1
            c1Intervention.Cols.Count = Col_Interv_Count
            c1Intervention.Cols.Fixed = 0

            Dim nWidth = c1Intervention.Width
            c1Intervention.Cols(Col_Interv_Select).Width = 50
            c1Intervention.Cols(Col_Interv_InterventionID).Width = 0
            c1Intervention.Cols(Col_Interv_Intervention).Width = nWidth - 50

            c1Intervention.Cols(Col_Interv_Select).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            c1Intervention.Cols(Col_Interv_InterventionID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            c1Intervention.Cols(Col_Interv_Intervention).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

            c1Intervention.SetData(0, Col_Interv_Select, "Select")
            c1Intervention.SetData(0, Col_Interv_InterventionID, "ID")
            c1Intervention.SetData(0, Col_Interv_Intervention, "Description")

            c1Intervention.Cols(Col_Interv_Select).DataType = GetType(Boolean)
            c1Intervention.Cols(Col_Interv_InterventionID).Visible = False
            c1Intervention.Cols(Col_Interv_Intervention).AllowEditing = False

            '29-Apr-14 Aniket: Resolving Bug #81246: gloEMR: Exam Intervention- Application gives exception
            c1Intervention.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Function GetIntervention() As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim dt As New DataTable
        Dim Query As String = ""
        Try
            oDB.Connect(False)
            If (mExamID = 0) Then
                Query = "SELECT sDescription FROM Category_MST WHERE (sCategoryType = 'Intervention') UNION SELECT sInterventionDescription As sDescription FROM ExamIntervention WHERE nPatientID = " & mPatientID & " AND nVisitID = " & mVisitID & ""
            Else
                Query = "SELECT sDescription FROM Category_MST WHERE (sCategoryType = 'Intervention') UNION SELECT sInterventionDescription As sDescription FROM ExamIntervention WHERE nPatientID = " & mPatientID & " AND nExamID = " & mExamID & " AND nVisitID = " & mVisitID & ""
            End If

            oDB.Retrive_Query(Query, dt)
            oDB.Disconnect()
            Return dt

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function

    Private Sub FillGrid()
        Dim dt As DataTable = GetIntervention()
        DesignGrid()

        If Not IsNothing(dt) Then
            For i As Integer = 0 To dt.Rows.Count - 1
                c1Intervention.Rows.Add()
                c1Intervention.SetCellCheck(i + 1, Col_Interv_Select, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                c1Intervention.SetData(i + 1, Col_Interv_Intervention, dt.Rows(i)("sDescription").ToString)
            Next
            dt.Dispose()
            dt = Nothing
        End If
     
    End Sub

    Private Sub CheckListItems()
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim Query As String = ""
        Dim dt As New DataTable
        ' Dim oListItem As gloGeneralItem.gloItem

        Try
            oDB.Connect(False)
            If (mExamID = 0) Then
                Query = "SELECT sInterventionDescription FROM ExamIntervention WHERE nPatientID = " & mPatientID & " AND nVisitID = " & mVisitID & ""
            Else
                Query = "SELECT sInterventionDescription FROM ExamIntervention WHERE nPatientID = " & mPatientID & " AND nExamID = " & mExamID & " AND nVisitID = " & mVisitID & ""
            End If

            oDB.Retrive_Query(Query, dt)
            oDB.Disconnect()
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If

            If Not IsNothing(dt) Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 1 To c1Intervention.Rows.Count - 1
                        If dt.Rows(i)("sInterventionDescription").ToString = c1Intervention.GetData(j, Col_Interv_Intervention) Then
                            c1Intervention.SetCellCheck(j, Col_Interv_Select, C1.Win.C1FlexGrid.CheckEnum.Checked)
                            Exit For
                        End If
                    Next
                Next
                dt.Dispose()
                dt = Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SaveListItems()
        Dim oExam As New clsPatientExams
        Dim oListItems As gloGeneralItem.gloItems
        oListItems = New gloGeneralItem.gloItems
        Dim oListItem As gloGeneralItem.gloItem = Nothing
        Try


            ''Save Selected Items in oListItems
            For i As Integer = 1 To c1Intervention.Rows.Count - 1
                If c1Intervention.GetCellCheck(i, Col_Interv_Select) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.ID = CType(c1Intervention.GetData(i, Col_Interv_InterventionID), Int64)
                    oListItem.Description = c1Intervention.GetData(i, Col_Interv_Intervention)
                    oListItems.Add(oListItem)
                    If IsNothing(oListItem) = False Then
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If

                End If
            Next
            ''

            ''Save Interventions in DataBase against ExamID
            oExam.SaveIntervention(mExamID, mPatientID, mVisitID, oListItems)
            If IsNothing(oExam) = False Then
                oExam.Dispose()
                oExam = Nothing
            End If
            If IsNothing(oListItems) = False Then
                oListItems.Dispose()
                oListItems = Nothing
            End If
           
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

    Private Sub frmExamIntervention_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        gloC1FlexStyle.Style(c1Intervention)
        FillGrid()
        CheckListItems()
        Try
            gloPatient.gloPatient.GetWindowTitle(Me, mPatientID, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

#Region " ToolStrip Event "

    Private Sub tls_Intervention_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tls_Intervention.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Save"
                SaveListItems()
                Me.Close()
            Case "Close"
                Me.Close()
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Close, "Closed Exam Intervention", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101011
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Close, "Closed Exam Intervention", mPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Other, "Closed Exam Intervention.  ", gstrLoginName, gstrClientMachineName, mPatientID, True, gloAuditTrail.enmOutCome.Success, gstrMessageBoxCaption)
        End Select
    End Sub
#End Region

    Private Sub c1Intervention_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles c1Intervention.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub
End Class