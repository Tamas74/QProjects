
Public Class frmVWTriage

#Region " C1 Constants "
    Private Const col_Received_TriageID = 0
    Private Const col_Received_Date = 1
    Private Const col_Received_Sender = 2
    Private Const col_Received_PatientID = 3
    Private Const col_Received_PatientName = 4
    Private Const col_Received_TriageName = 5
    Private Const col_Received_TemplateID = 6
    Private Const col_Received_Count = 7

    Private Const col_Sent_TriageID = 0
    Private Const col_Sent_Date = 1
    Private Const col_Sent_PatientID = 2
    Private Const col_Sent_PatientName = 3
    Private Const col_Sent_TriageName = 4
    Private Const col_Sent_Finished = 5
    Private Const col_Sent_TemplateID = 6
    Private Const col_Sent_Count = 7
#End Region

    Private _TriageID As Int64 = 0
    Private _IsFinished As Boolean = False
    Private _PatientID As Int64 = 0
    Private Default_PatientID As Int64 = 0 'patient id pass from constructor
    Private _TemplateID As Int64 = 0

    Dim oClsTriage As New gloStream.gloEMR.Triage.clsTriage
    Dim oTriages As gloStream.gloEMR.Triage.Supportings.Triages
    Dim oDashBoard As New clsDoctorsDashBoard
    Public Shared blnmodify As Boolean


    Private Sub frmVWTriage_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        pnlSentTriage.Width = (Me.Width / 2) - 10
        pnlReceivedTriage.Width = (Me.Width / 2) - 10
        FillReceivedTriage()
        FillSentTriage()
    End Sub

    Private Sub FillReceivedTriage()
        Dim oPatient As New gloPatient.gloPatient(GetConnectionString)
        Dim lastRow As Integer
        Try
            DesignReceivedGrid()
            oTriages = Nothing
            'oTriages = oClsTriage.GetUserTriage(gnLoginID)
            oTriages = oClsTriage.GetUserTriage(gnLoginID)
            If Not IsNothing(oTriages) Then
                For i As Integer = 1 To oTriages.Count
                    If oTriages(i).IsFinished = False Then
                        C1ReceivedTriage.Rows.Add()
                        lastRow = C1ReceivedTriage.Rows.Count - 1

                        C1ReceivedTriage.SetData(lastRow, col_Received_TriageID, oTriages(i).TriageID)
                        C1ReceivedTriage.SetData(lastRow, col_Received_Date, oTriages(i).TriageDate)
                        C1ReceivedTriage.SetData(lastRow, col_Received_Sender, oDashBoard.GetUserName(oTriages(i).FromID))
                        C1ReceivedTriage.SetData(lastRow, col_Received_PatientID, oTriages(i).PatientID)
                        C1ReceivedTriage.SetData(lastRow, col_Received_PatientName, oPatient.GetPatientName(oTriages(i).PatientID))
                        C1ReceivedTriage.SetData(lastRow, col_Received_TriageName, oTriages(i).TemplateName)
                        C1ReceivedTriage.SetData(lastRow, col_Received_TemplateID, oTriages(i).TemplateID)
                    End If
                Next
            End If
            'DesignReceivedGrid()
            C1ReceivedTriage.Row = -1
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub FillSentTriage()
        Dim oPatient As New gloPatient.gloPatient(GetConnectionString)
        Dim lastRow As Integer
        Try
            DesignSentGrid()
            oTriages = Nothing
            oTriages = oClsTriage.GetSentTriage(gnLoginID)
            If Not IsNothing(oTriages) Then
                For i As Integer = 1 To oTriages.Count
                    C1SentTriage.Rows.Add()
                    lastRow = C1SentTriage.Rows.Count - 1

                    C1SentTriage.SetData(lastRow, col_Sent_TriageID, oTriages(i).TriageID)
                    C1SentTriage.SetData(lastRow, col_Sent_Date, oTriages(i).TriageDate)
                    C1SentTriage.SetData(lastRow, col_Sent_PatientID, oTriages(i).PatientID)
                    C1SentTriage.SetData(lastRow, col_Sent_PatientName, oPatient.GetPatientName(oTriages(i).PatientID))
                    C1SentTriage.SetData(lastRow, col_Sent_TriageName, oTriages(i).TemplateName)
                    If oTriages(i).IsFinished Then
                        C1SentTriage.SetData(lastRow, col_Sent_Finished, "Yes")
                    Else
                        C1SentTriage.SetData(lastRow, col_Sent_Finished, "No")
                    End If
                    C1SentTriage.SetData(lastRow, col_Sent_TemplateID, oTriages(i).TemplateID)
                Next
            End If
            'DesignSentGrid()
            C1SentTriage.Row = -1
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DesignReceivedGrid()
        Try
            C1ReceivedTriage.AllowEditing = False
            C1ReceivedTriage.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
            C1ReceivedTriage.Cols.Count = col_Received_Count
            C1ReceivedTriage.Rows.Count = 1
            C1ReceivedTriage.Rows.Fixed = 1

            C1ReceivedTriage.SetData(0, col_Received_TriageID, "TriageID")
            C1ReceivedTriage.SetData(0, col_Received_Date, "Date")
            C1ReceivedTriage.SetData(0, col_Received_Sender, "Sender")
            C1ReceivedTriage.SetData(0, col_Received_PatientID, "PatientID")
            C1ReceivedTriage.SetData(0, col_Received_PatientName, "Patient Name")
            C1ReceivedTriage.SetData(0, col_Received_TriageName, "Triage")
            C1ReceivedTriage.SetData(0, col_Received_TemplateID, "Template ID")

            Dim width As Integer = C1ReceivedTriage.Width

            C1ReceivedTriage.Cols(col_Received_TriageID).Width = 0 ''Hidden
            C1ReceivedTriage.Cols(col_Received_Date).Width = 0.2 * width
            C1ReceivedTriage.Cols(col_Received_Sender).Width = 0.2 * width
            C1ReceivedTriage.Cols(col_Received_PatientID).Width = 0 ''Hidden
            C1ReceivedTriage.Cols(col_Received_PatientName).Width = 0.3 * width
            C1ReceivedTriage.Cols(col_Received_TriageName).Width = 0.3 * width
            C1ReceivedTriage.Cols(col_Received_TemplateID).Width = 0
            'C1ReceivedTriage.Cols(col_Received_TriageName).Visible = False

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub DesignSentGrid()
        Try

            C1SentTriage.AllowEditing = False
            C1SentTriage.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
            C1SentTriage.Cols.Count = col_Sent_Count
            C1SentTriage.Rows.Count = 1
            C1SentTriage.Rows.Fixed = 1

            C1SentTriage.SetData(0, col_Sent_TriageID, "TriageID")
            C1SentTriage.SetData(0, col_Sent_Date, "Date")
            C1SentTriage.SetData(0, col_Sent_PatientID, "PatientID")
            C1SentTriage.SetData(0, col_Sent_PatientName, "Patient Name")
            C1SentTriage.SetData(0, col_Sent_TriageName, "Triage")
            C1SentTriage.SetData(0, col_Sent_Finished, "Finished")
            C1SentTriage.SetData(0, col_Sent_TemplateID, "Template ID")

            Dim width As Integer = C1SentTriage.Width

            C1SentTriage.Cols(col_Sent_TriageID).Width = 0 ''Hidden
            C1SentTriage.Cols(col_Sent_Date).Width = 0.3 * width
            C1SentTriage.Cols(col_Sent_PatientID).Width = 0 ''Hidden
            C1SentTriage.Cols(col_Sent_PatientName).Width = 0.3 * width
            C1SentTriage.Cols(col_Sent_TriageName).Width = 0.3 * width
            C1SentTriage.Cols(col_Sent_Finished).Width = 0.1 * width
            C1SentTriage.Cols(col_Sent_TemplateID).Width = 0 ''Hidden
            'C1SentTriage.Cols(col_Sent_TemplateID).Visible = False

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AddNewTriage()
        Try

            '' SUDHIR 20090708 '' CHECK PATIENT STATUS ''
            ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'If CheckPatientStatus(gnPatientID) = False Then
            '    Exit Sub
            'End If
            'If CheckPatientStatus(Default_PatientID) = False Then
            '    Exit Sub
            'End If
            If MainMenu.IsAccess(False, _PatientID) = False Then
                Exit Sub
            End If
            'end modification 

            '' SUDHIR 20090521 '' CHECK PROVIDER ''
            ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'If gblnProviderDisable = True Then
            '    If ShowAssociateProvider(gnPatientID) = True Then
            '        CType(Me.ParentForm, MainMenu).oPatientListControl.FillPatients()
            '    End If
            'End If
            If gblnProviderDisable = True Then
                If ShowAssociateProvider(Default_PatientID, Me) = True Then
                    CType(Me.ParentForm, MainMenu).oPatientListControl.FillPatients()
                End If
            End If
            'end modification

            '' END SUDHIR

            '******Shweta 20090828 *********'
            'To check exeception related to word
            If CheckWordForException() = False Then
                Exit Sub
            End If
            'End Shweta


            Dim oTriage As New gloStream.gloEMR.Triage.clsTriage
            Dim dtTemplate As New DataTable
            dtTemplate = oTriage.FillTemplates()

            If dtTemplate.Rows.Count <= 0 Then
                MessageBox.Show("No Template is associated for Triage. Please associate any template first", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim frm As frmTriage

            ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'frm = frmTriage.GetInstance(0, gnPatientID)
            frm = frmTriage.GetInstance(0, Default_PatientID)
            'AddHandler frm.EvntGenerateCDAFromTriage, AddressOf Raise_EvntGenerateCDAFromViewTriage
            'end modification

            If IsNothing(frm) = True Then
                Exit Sub
            End If

            CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
            CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False
            blnmodify = False
            frm.MdiParent = CType(Me.ParentForm, MainMenu)
            frm.myCaller = Me
            'frm.IsModify = True
            frm.WindowState = FormWindowState.Maximized
            frm.BringToFront()
            frm.Show()

        Catch ex As Exception
            CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub ModifyTriage(ByVal triageID As Int64, ByVal patientID As Int64, ByVal TemplateID As Int64)
        Try

            ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            '' SUDHIR 20090708 '' CHECK PATIENT STATUS ''
            'If CheckPatientStatus(gnPatientID) = False Then
            '    Exit Sub
            'End If
            'If CheckPatientStatus(patientID) = False Then
            '    Exit Sub
            'End If
            If MainMenu.IsAccess(False, _PatientID) = False Then
                Exit Sub
            End If
            'end modification

            '******Shweta 20090828 *********'
            'To check exeception related to word
            If CheckWordForException() = False Then
                Exit Sub
            End If
            'End Shweta


            If triageID = 0 Then
                MessageBox.Show("Please select triage to modify.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            Dim frm As frmTriage
            CType(Me.ParentForm, MainMenu).ShowDefaultPatientDetails(patientID)
            frm = frmTriage.GetInstance(triageID, patientID, 0, False, TemplateID)
            Try
                RemoveHandler frm.FormClosed, AddressOf On_Triage_Closed
            Catch ex As Exception

            End Try

            AddHandler frm.FormClosed, AddressOf On_Triage_Closed
            'AddHandler frm.EvntGenerateCDAFromTriage, AddressOf Raise_EvntGenerateCDAFromViewTriage
            If IsNothing(frm) = True Then
                Exit Sub
            End If

            CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
            CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False
            blnmodify = True
            frm.MdiParent = CType(Me.ParentForm, MainMenu)
            frm.myCallerMain = CType(Me.ParentForm, MainMenu)
            frm.IsModify = True
            frm.WindowState = FormWindowState.Maximized
            frm.BringToFront()
            frm.Show()
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.Triage, gloAuditTrail.ActivityType.View, "Triage viewed", patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        Catch ex As Exception
            CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub DeleteTriage()
        Try
            If _TriageID = 0 Then
                Exit Sub
            End If

            '' SUDHIR 20090708 '' CHECK PATIENT STATUS ''
            ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'If CheckPatientStatus(gnPatientID) = False Then
            '    Exit Sub
            'End If
            'If CheckPatientStatus(_PatientID) = False Then
            '    Exit Sub
            'End If
            If MainMenu.IsAccess(False, _PatientID) = False Then
                Exit Sub
            End If
            'End modification

            If _IsFinished Then
                MessageBox.Show("The status of triage is finished, you can not delete this triage.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If


            If MessageBox.Show("Are you sure you want to delete Triage?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                If oClsTriage.DeleteTriage(_TriageID, _PatientID) = True Then
                    RefreshTriage()
                Else
                    MessageBox.Show("Error while deleting Triage", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub RefreshTriage()
        FillReceivedTriage()
        FillSentTriage()
        _TriageID = 0
        _PatientID = 0
        _TemplateID = 0
    End Sub

    Private Sub tlsTriage_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsTriage.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Add"
                    AddNewTriage()
                Case "Modify"

                    ModifyTriage(_TriageID, _PatientID, _TemplateID)

                Case "Delete"
                    DeleteTriage()
                Case "Refresh"
                    RefreshTriage()
                Case "Close"
                    ' Me.Close()
                    gloWord.WordDialogBoxBackgroundCloser.Close(Me)
                Case Else
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub C1ReceivedTriage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1ReceivedTriage.Click
        Try
            'For iRow As Integer = 1 To C1SentTriage.Rows.Count - 1
            'C1SentTriage.RowSel
            'Next
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub C1SentTriage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1SentTriage.Click
        Try
            'For iRow As Integer = 1 To C1ReceivedTriage.Rows.Count - 1
            '    C1ReceivedTriage.Rows(iRow).Selected = False
            'Next
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub C1ReceivedTriage_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1ReceivedTriage.MouseDoubleClick
        Try
            Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo
            htInfo = C1ReceivedTriage.HitTest(e.X, e.Y)
            If htInfo.Row > 0 Then
                _TriageID = CType(C1ReceivedTriage.GetData(htInfo.Row, col_Received_TriageID), Int64)
                _PatientID = CType(C1ReceivedTriage.GetData(htInfo.Row, col_Received_PatientID), Int64)
                _TemplateID = CType(C1ReceivedTriage.GetData(htInfo.Row, col_Received_TemplateID), Int64)
                ModifyTriage(_TriageID, _PatientID, _TemplateID)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub C1SentTriage_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1SentTriage.MouseDoubleClick
        Try
            Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo
            htInfo = C1SentTriage.HitTest(e.X, e.Y)
            If htInfo.Row > 0 Then
                _TriageID = CType(C1SentTriage.GetData(htInfo.Row, col_Sent_TriageID), Int64)
                _PatientID = CType(C1SentTriage.GetData(htInfo.Row, col_Sent_PatientID), Int64)
                _TemplateID = CType(C1SentTriage.GetData(htInfo.Row, col_Sent_TemplateID), Int64)
                ModifyTriage(_TriageID, _PatientID, _TemplateID)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub C1ReceivedTriage_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1ReceivedTriage.MouseUp
        C1SentTriage.Row = -1
        If C1ReceivedTriage.Row > 0 Then
            _TriageID = CType(C1ReceivedTriage.GetData(C1ReceivedTriage.Row, col_Received_TriageID), Int64)
            _PatientID = CType(C1ReceivedTriage.GetData(C1ReceivedTriage.Row, col_Received_PatientID), Int64)
            _TemplateID = CType(C1ReceivedTriage.GetData(C1ReceivedTriage.Row, col_Received_TemplateID), Int64)
        Else
            _TriageID = 0
            _PatientID = 0
            _TemplateID = 0
        End If
        _IsFinished = False
    End Sub

    Private Sub C1SentTriage_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1SentTriage.MouseUp
        C1ReceivedTriage.Row = -1
        If C1SentTriage.Row > 0 Then
            _TriageID = CType(C1SentTriage.GetData(C1SentTriage.Row, col_Sent_TriageID), Int64)
            _PatientID = CType(C1SentTriage.GetData(C1SentTriage.Row, col_Sent_PatientID), Int64)
            If C1SentTriage.GetData(C1SentTriage.Row, col_Sent_Finished) = "Yes" Then
                _IsFinished = True
            Else
                _IsFinished = False
            End If
            _TemplateID = CType(C1SentTriage.GetData(C1SentTriage.Row, col_Sent_TemplateID), Int64)
        Else
            _TriageID = 0
            _PatientID = 0
            _TemplateID = 0
        End If
    End Sub

    Private Sub On_Triage_Closed(ByVal sender As Object, ByVal e As FormClosedEventArgs)
        Dim frm As frmTriage = Nothing

        Try
            frm = DirectCast(sender, frmTriage)
        Catch ex As Exception

        End Try
        Try
            If (IsNothing(frm) = False) Then
                RemoveHandler frm.FormClosed, AddressOf On_Triage_Closed
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
        If Me.IsAccessible = True Then
            RefreshTriage()
        End If
    End Sub

    Private Sub cmnuAddTriage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuAddTriage.Click
        AddNewTriage()
    End Sub

    Public Sub New(ByVal PatientID As Long)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Default_PatientID = PatientID
        ' Add any initialization after the InitializeComponent() call.

    End Sub

#Region "Call Generate CCDA from Dashboard"
    'Public Delegate Sub GenerateCDAFromViewTriage(ByVal PatientID As Int64)
    'Public Event EvntGenerateCDAFromViewTriage(ByVal PatientID As Int64)

    Protected Overridable Sub Raise_EvntGenerateCDAFromViewTriage(ByVal PatientID As Int64)
        'RaiseEvent EvntGenerateCDAFromViewTriage(PatientID)

        Try
            mdlGeneral.OpenCDA(PatientID)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try
    End Sub
#End Region
End Class