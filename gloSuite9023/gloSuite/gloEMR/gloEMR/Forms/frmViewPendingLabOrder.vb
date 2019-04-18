Imports gloUserControlLibrary
Imports gloListControl

Public Class frmViewPendingLabOrder

    Private WithEvents octlPatientList As gloListControl.gloListControl
    Private WithEvents GloUC_TransactionHistory1 As gloUserControlLibrary.gloUC_TransactionHistory

    ' Dim dt As New DataTable
    Dim PatientID As Int64 = 0
    Dim _isControlLoading As Boolean = False

    Private Sub frmViewPendingLabOrder_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If (IsNothing(octlPatientList) = False) Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(octlPatientList)
                octlPatientList.Dispose()
                octlPatientList = Nothing
            Catch ex As Exception

            End Try
        End If
        If (IsNothing(GloUC_TransactionHistory1) = False) Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(GloUC_TransactionHistory1)
                GloUC_TransactionHistory1.Dispose()
                GloUC_TransactionHistory1 = Nothing
            Catch ex As Exception

            End Try
        End If
    End Sub
   

    Private Sub frmViewPendingLabOrder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            _isControlLoading = True

            'Declare Variable for connection string
            octlPatientList = New gloListControl.gloListControl(GetConnectionString, gloListControlType.PendingLabPatients, False, Me.Width)
            octlPatientList.Dock = DockStyle.Fill
            octlPatientList.ClinicID = 1
            octlPatientList.CloseOnDoubleClick = False
            octlPatientList.ShowHeaderPanel(False)
            octlPatientList.HideToolStrip = True
            octlPatientList.OpenControl()

            
            'Add octlPatientList into pnlPatientListView
            pnlPatientList.Controls.Add(octlPatientList)
            pnlPatientList.Dock = DockStyle.Top
            PatientID = octlPatientList.DefaultPatientID

            'for first patient in the list,retrive patient id,add lab control
            GloUC_TransactionHistory1 = New gloUserControlLibrary.gloUC_TransactionHistory()
            GloUC_TransactionHistory1.Dock = DockStyle.Fill
            GloUC_TransactionHistory1.Visible = True
            GloUC_TransactionHistory1.ShowReceivedate = True
            pnlPatientLab.Controls.Add(GloUC_TransactionHistory1)

            GloUC_TransactionHistory1.LoadPreviousLabs(PatientID, DateTime.Now) '.ToString("MM/dd/yyyy hh:mm:ss"))
            GloUC_TransactionHistory1.DesignTestGrid()
            GloUC_TransactionHistory1.SetDataByDate(DateTime.Now.Date, DateTime.Now.Date)
            GloUC_TransactionHistory1.cmbCriteria.Text = "Date"


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        Finally
            _isControlLoading = False
        End Try


    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsb_Close.Click
        Me.Close()
    End Sub

    Private Sub octlPatientList_ItemSelectedClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles octlPatientList.ItemSelectedClick

        Try

            Dim oSelectedList As gloGeneralItem.gloItems = Nothing

            If octlPatientList.SelectedItems.Count > 0 Then
                oSelectedList = octlPatientList.SelectedItems
            Else
                Exit Sub
            End If

            If oSelectedList.Count > 0 Then
                PatientID = oSelectedList.Item(0).ID



                'First remove any controls from the panel for labs
                If IsNothing(GloUC_TransactionHistory1) = False Then
                    If pnlPatientLab.Contains(GloUC_TransactionHistory1) = True Then
                        pnlPatientLab.Controls.Remove(GloUC_TransactionHistory1)
                    End If
                    GloUC_TransactionHistory1.Dispose()
                    GloUC_TransactionHistory1 = Nothing
                End If

                'invoke the functions to populate the control

                GloUC_TransactionHistory1 = New gloUserControlLibrary.gloUC_TransactionHistory()
                GloUC_TransactionHistory1.Dock = DockStyle.Fill
                GloUC_TransactionHistory1.Visible = True
                GloUC_TransactionHistory1.ShowReceivedate = True 'receivedate
                pnlPatientLab.Controls.Add(GloUC_TransactionHistory1)

                GloUC_TransactionHistory1.LoadPreviousLabs(PatientID, DateTime.Now) '.ToString("MM/dd/yyyy hh:mm:ss"))
                GloUC_TransactionHistory1.DesignTestGrid()
                GloUC_TransactionHistory1.SetDataByDate(DateTime.Now.Date, DateTime.Now.Date)
                GloUC_TransactionHistory1.cmbCriteria.Text = "Date"
                'pnlPatientList.Visible = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub

    Private Sub octlPatientList_ItemSelection_Change(ByVal sender As Object, ByVal e As System.EventArgs) Handles octlPatientList.ItemSelection_Change

        'If Not _isControlLoading Then
        '    octlPatientList_ItemSelectedClick(Nothing, Nothing)
        'End If
    End Sub


    ''Sandip Darade 24 Feb 2009
    Private Sub GloUC_TransactionHistory_btnShowGraphClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GloUC_TransactionHistory1.btnShowGraphClick
        Try

            Dim dt_selectedResult As New DataTable
            '''' Get selected Result From Grid
            dt_selectedResult = GloUC_TransactionHistory1.SelectResult()

            '''' If DataTable is empty then exit from Procedure.
            'If dt_selectedResult Is Nothing Then
            If dt_selectedResult.Rows.Count = 0 Then
                Exit Sub
            End If
            ' End If

            If String.IsNullOrEmpty(dt_selectedResult.Rows(dt_selectedResult.Rows.Count - 1)(0)) = True Then
                MessageBox.Show("Graph cannot be generated as no Collected Date is present.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            ''''Get Min and Max Value from DataTable

            Dim dv As DataView
            dv = New DataView(dt_selectedResult)
            dv.Sort = "Value"

            Dim max As Integer = dv.Item(dv.Count - 1)("Value").ToString()
            Dim min As Integer = dv.Item(0)("Value").ToString() '' dv.Table.Rows.Count - 1)("Value")



            Dim dtSelectedResultToDate As DateTime = CType(dt_selectedResult.Rows(dt_selectedResult.Rows.Count - 1)(0), DateTime)


            ' lines for get the first results data and show it into the label as From-date
            Dim dtStartdate As DateTime
            dtStartdate = dt_selectedResult.Rows.Item(0)(0) ' Take from date for Display 
            dtStartdate = Format(dtStartdate, "MM/dd/yyyy")

            ' view the graphs for the provided values as a parameters provided
            Dim oGraphResult As New frmLab_GraphsResult(dtStartdate, dtSelectedResultToDate, 0, 0, PatientID, dt_selectedResult.Rows(0)(1), dt_selectedResult.Rows(0)(2), dt_selectedResult, , False, , min, max)
            With oGraphResult
                .MdiParent = Me.MdiParent
                .WindowState = FormWindowState.Maximized
                .ShowInTaskbar = False
                .Show()
            End With
            'Me.Close()
            Exit Sub
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub

   
End Class