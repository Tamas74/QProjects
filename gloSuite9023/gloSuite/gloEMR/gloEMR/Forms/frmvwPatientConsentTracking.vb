Imports gloUserControlLibrary
Imports gloEMR.gloEMRWord

Public Class frmvwPatientConsentTracking

#Region "Vaiables"

    Dim _PatientID As Long
    Private WithEvents gloUC_PatientStrip1 As gloUC_PatientStrip
    Public Shared blnCancelDelete As Boolean = False
    Dim strSortColumnName As String = String.Empty
    Dim strSortOrder As String = String.Empty
    Dim myDataView As DataView = Nothing
    Dim strSortExprn As String = String.Empty
    Dim PatConID As Long = 0
#End Region

#Region "Construcor"

    Public Sub New(ByVal PatientID As Long)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
        _PatientID = PatientID
    End Sub

#End Region

#Region "Form Events"

    Private Sub frmvwPatientConsentTracking_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If Not IsNothing(myDataView) Then
            myDataView.Dispose()
            myDataView = Nothing
        End If
    End Sub

    Private Sub frmvwPatientConsentTracking_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub

    Private Sub frmvwPatientConsentTracking_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        gloC1FlexStyle.Style(gvData)

        Try
            Set_PatientDetailStrip()
            LoadConsentTrackingList()
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
        Catch ex As Exception
            MessageBox.Show("Error on Consent Tracking." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "Button Events"

    Private Sub tblbtn_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Add.Click
        Try
            myDataView = CType(gvData.DataSource, DataView)
            If (IsNothing(myDataView) = False) Then


                strSortExprn = myDataView.Sort

                Dim arrcolumnsort As String() = Split(strSortExprn, "]")
                If arrcolumnsort.Length > 1 Then
                    strSortColumnName = arrcolumnsort.GetValue(0)

                End If
            End If
            strSortColumnName = strSortColumnName.Replace("[", "")
            Dim frm As New frmPatientConsentTracking(_PatientID)
            frm.ShowInTaskbar = False
            frm.KeyPreview = True
            frm.StartPosition = FormStartPosition.CenterParent
            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            frm.Dispose()
            frm = Nothing
            LoadConsentTrackingList()
            strSortColumnName = String.Empty
            ' strSortOrder = String.Empty
            strSortExprn = String.Empty
        Catch ex As Exception
            MessageBox.Show("Error on Consent Tracking." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tblbtn_Modify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Modify.Click
        Try
            If gvData.Rows.Count > 1 Then
                If Convert.ToInt64(gvData.GetData(gvData.Row, 1)) > 0 Then
                    '   Dim rowIndex As Integer = gvData.RowSel
                    'PatConID = gvData.GetData(gvData.RowSel, 0)
                    myDataView = CType(gvData.DataSource, DataView)
                    If (IsNothing(myDataView) = False) Then


                        strSortExprn = myDataView.Sort

                        Dim arrcolumnsort As String() = Split(strSortExprn, "]")
                        If arrcolumnsort.Length > 1 Then
                            strSortColumnName = arrcolumnsort.GetValue(0)

                        End If
                    End If
                    strSortColumnName = strSortColumnName.Replace("[", "")
                    PatConID = Convert.ToInt64(gvData.GetData(gvData.Row, "nPatientConsentTrackingID")) ''rowsel change to row for bugid 85823
                    Dim frm As New frmPatientConsentTracking(_PatientID, PatConID)
                    frm.Text = "Modify Consent Tracking"
                    frm.KeyPreview = True
                    frm.ShowInTaskbar = False
                    frm.StartPosition = FormStartPosition.CenterParent
                    frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                    frm.Dispose()
                    frm = Nothing
                    LoadConsentTrackingList()
                    'Dim dr As DataRow() = myDataView.Table.Select("nPatientConsentTrackingID= " & PatConID)
                    'rowIndex = myDataView.Table.Rows.IndexOf(dr(0))
                    '  gvData.RowSel = rowIndex
                    strSortColumnName = String.Empty
                    '   strSortOrder = String.Empty
                    strSortExprn = String.Empty
                End If
            End If
            myDataView = CType(gvData.DataSource, DataView)
            Dim rowsel As Integer = -1
            If Not myDataView Is Nothing Then
                If PatConID > 0 Then
                    Dim dtdata As DataTable = myDataView.ToTable()
                    Dim drr As DataRow() = dtdata.Select("nPatientConsentTrackingID=" + PatConID.ToString() + "")
                    If drr.Length > 0 Then
                        rowsel = dtdata.Rows.IndexOf(drr(0))
                    End If
                    dtdata.Dispose()
                    dtdata = Nothing
                    drr = Nothing
                End If
            End If
            If gvData.Rows.Count > rowsel And rowsel <> -1 Then
                gvData.RowSel = rowsel
            Else
                If gvData.Rows.Count > 0 Then
                    gvData.RowSel = 0
                End If
            End If

        Catch ex As Exception
            MessageBox.Show("Error on Consent Tracking." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            PatConID = 0
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        txtSearch.Clear()
    End Sub

    Private Sub tblbtn_ViewHistory_Click(sender As System.Object, e As System.EventArgs) Handles tblbtn_ViewHistory.Click
        Dim TransactionID As Int64 = 0
        If gvData.Row > 0 Then
            'TransactionID = Convert.ToInt64(gvData.GetData(gvData.Row, 1))
        End If

        Dim oForm As New frmConsentTracking_History(_PatientID)
        oForm.WindowState = FormWindowState.Normal
        oForm.StartPosition = FormStartPosition.CenterScreen
        oForm.ShowInTaskbar = False
        oForm.ShowDialog(IIf(IsNothing(oForm.Parent), Me, oForm.Parent))
        Me.Cursor = Cursors.Arrow
        oForm.Dispose()
        oForm = Nothing

    End Sub

    Private Sub tblbtn_Delete_Click(sender As System.Object, e As System.EventArgs) Handles tblbtn_Delete.Click
        Try
            If gvData.Rows.Count > 1 Then
                If Convert.ToInt64(gvData.GetData(gvData.Row, 1)) > 0 Then
                    If MessageBox.Show(Me, "Do you want to delete the selected ‘Patient Consent Tracking’ record?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        '@Opeation = 1 Add , 2 Modify , 3 Delete
                        Dim OperationType As Integer = 3
                        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
                        Dim oParam As gloDatabaseLayer.DBParameters
                        oDB.Connect(False)
                        oParam = New gloDatabaseLayer.DBParameters
                        oParam.Add("@Opeation", OperationType, ParameterDirection.Input, SqlDbType.Int)
                        oParam.Add("@nPatientConsentTrackingID", Convert.ToInt64(gvData.GetData(gvData.Row, "nPatientConsentTrackingID")), ParameterDirection.Input, SqlDbType.BigInt)
                        oParam.Add("@nPatientID", _PatientID, ParameterDirection.Input, SqlDbType.BigInt)
                        Dim intResult As Integer = oDB.Execute("ConsentTrackingOperation", oParam)
                        oParam.Dispose()
                        oParam = Nothing
                        oDB.Dispose()
                        oParam = Nothing
                        LoadConsentTrackingList()
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(Me, ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub

    Private Sub tblbtn_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Close.Click
        Me.Close()
    End Sub

#End Region

#Region "Grid Events"

    Private Sub gvData_AfterSort(sender As Object, e As C1.Win.C1FlexGrid.SortColEventArgs) Handles gvData.AfterSort
        If (e.Order = 1) Then
            strSortOrder = "Asc"
        Else
            strSortOrder = "Desc"
        End If
    End Sub

    Private Sub gvData_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles gvData.KeyDown
        If e.KeyCode = Keys.Enter Then
            If gvData.RowSel > 0 Then
                Call tblbtn_Modify_Click(Nothing, Nothing)
            End If
        End If
    End Sub

    Private Sub gvData_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles gvData.MouseDoubleClick
        If (gvData.Rows.Count > 0) Then
            If (gvData.RowSel > 0) Then
                Dim hittest As C1.Win.C1FlexGrid.HitTestInfo = gvData.HitTest() ''added for bugid 80005
                If (hittest.Row > 0) Then
                    tblbtn_Modify_Click(Nothing, Nothing)
                End If
            End If
        End If

    End Sub

#End Region

#Region "Function & Methods"

    Private Sub LoadConsentTrackingList()
        Dim dtdata As DataTable = Nothing
        Try
            Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
            Dim oParam As gloDatabaseLayer.DBParameters

            oDB.Connect(False)
            oParam = New gloDatabaseLayer.DBParameters
            oParam.Add("@nPatientConsentTrackingID", 0, ParameterDirection.Input, SqlDbType.BigInt)
            oParam.Add("@PatientID", _PatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Retrive("GetConsentTrackingList", oParam, dtdata)
            oDB.Disconnect()
            oParam.Dispose()
            oParam = Nothing
            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing

            If dtdata IsNot Nothing Then
                myDataView = dtdata.DefaultView
                dtdata.Dispose()
                dtdata = Nothing
                gvData.Clear()
                gvData.DataSource = Nothing
                If strSortColumnName <> String.Empty And strSortOrder <> String.Empty Then ''added for bugid 80004

                    myDataView.Sort = "[" & strSortColumnName & "] " & strSortOrder
                End If
                gvData.DataSource = myDataView
                gvData.AutoResize = True
            End If
            If (txtSearch.Text.Trim() <> "") Then ''added for bugid 80001
                FilterRecord()
            End If
            formatGrid()

        Catch ex As Exception
            MessageBox.Show("Error on Consent Tracking." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub formatGrid()
        Try
            If gvData.DataSource IsNot Nothing Then
                gvData.AllowEditing = False
                For i As Integer = 0 To gvData.Cols.Count - 1
                    'gvData.Cols(i).AllowEditing = False
                    If gvData.Cols(i).Caption = "nPatientConsentTrackingID" OrElse gvData.Cols(i).Caption = "nPatientID" OrElse gvData.Cols(i).Caption = "nConsentType" OrElse gvData.Cols(i).Caption = "nConsentStatus" OrElse gvData.Cols(i).Caption = "nObtainedBy" Then
                        gvData.Cols(i).Visible = False
                    End If
                    gvData.Cols(i).AllowSorting = True
                    gvData.Cols(i).Width = 190
                Next
                If (gvData.Cols.Contains("Date of Consent")) Then
                    gvData.Cols("Date of Consent").Format = "MM/dd/yyyy hh:mm:ss tt"
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error on Consent Tracking." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Set_PatientDetailStrip()
        'Add Patient Details Control
        gloUC_PatientStrip1 = New gloUC_PatientStrip

        With gloUC_PatientStrip1
            .Dock = DockStyle.Top
            .Padding = New Padding(3, 0, 3, 0)
            .ShowDetail(_PatientID, gloUC_PatientStrip.enumFormName.PatientConsentTracking)
            .BringToFront()
        End With
        Me.Controls.Add(gloUC_PatientStrip1)
        pnlSearch.BringToFront()
        pnlMain.BringToFront()
        pnlTop.SendToBack()
    End Sub

    Private Sub FilterRecord()
        Try

            myDataView = CType(gvData.DataSource, DataView)

            If IsNothing(myDataView) Then
                Exit Sub
            End If

            gvData.DataSource = myDataView

            Dim strPatientSearchDetails As String
            If Trim(txtSearch.Text) <> "" Then
                strPatientSearchDetails = Replace(txtSearch.Text, "'", "''")
                strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
            Else
                strPatientSearchDetails = ""
            End If

            myDataView.RowFilter = " Comments Like '%" & strPatientSearchDetails & "%' OR " & _
                                        " [Consent Type] Like '%" & strPatientSearchDetails & "%' OR " & _
                                        " [Consent Status] Like '%" & strPatientSearchDetails & "%' OR " & _
                                        " [Obtained By] Like '%" & strPatientSearchDetails & "%' OR " & _
                                        " Consenter Like '%" & strPatientSearchDetails & "%' "


            gvData.AutoResize = True
            formatGrid()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "Textbox Event"

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        FilterRecord()
    End Sub

#End Region

  
    Private Sub gvData_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles gvData.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub
End Class
