Imports gloEMR.gloEMRWord
'Imports gloEMR.gloAuditTrail
Public Class frmVWNurseNotes
    Inherits gloGlobal.Common.TriarqFormWithFocusListner
    Implements IPatientContext


    Dim _PatientID As Long
    Public Shared blnModify As Boolean
    Dim objclsNotes As New clsNurseNotes
    'Dim dt As DataTable
    Dim dv As DataView
    Dim _blnSearch As Boolean = True
    Dim sortOrder As String
    Dim strSearchstring As String
    Dim arrcolumnsort() As String
    Dim strcolumnName As String
    Dim strsortorder As String
    Dim _blnAdd As Boolean
    ' Dim dtWord As DataTable
    ' Dim objWord As clsWordDocument
    Dim _GridWidth As Int32
    Dim Col_NotesID As Integer = 0
    Dim Col_NotesDate As Integer = 1
    Dim Col_TemplateID As Integer = 2
    Dim Col_NotesHeader As Integer = 3
    Dim Col_IsFinished As Integer = 4
    Dim Col_Count As Integer = 5
    Dim Ind As Integer = -1


    Private Sub SetGridStyle(Optional ByVal strcolumnName As String = "", Optional ByVal strSortBy As String = "", Optional ByVal strsearchtxt As String = "")
        ''added to solve  sorting issue Bugid 72083
        Try

            dv = objclsNotes.GetDataView
            If (IsNothing(dv)) Then
                Exit Sub
            End If
            c1NurseNotes.DataSource = dv
            With c1NurseNotes
                .AllowSorting = True


                .Redraw = False

                Dim _TotalWidth As Single = 0
                _TotalWidth = Me.Width - 20

                ' c1Disclosure.Height = Me.Height - 20
                c1NurseNotes.ShowCellLabels = False
                Dim cStyle As C1.Win.C1FlexGrid.CellStyle = Nothing


                .Cols.Count = Col_Count
                .Rows.Fixed = 1


                .Styles.ClearUnused()
                c1NurseNotes.Width = _TotalWidth
                .Dock = DockStyle.Fill
                .AllowResizing = True

                .Cols(Col_NotesID).Width = _TotalWidth * 0
                .Cols(Col_NotesID).AllowEditing = False
                .Cols(Col_NotesID).Visible = False
                .Cols(Col_NotesID).Caption = "NotesID"
                .Cols(Col_NotesID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter


                .Cols(Col_NotesDate).Width = _TotalWidth * 0.33
                .Cols(Col_NotesDate).AllowEditing = False
                .Cols(Col_NotesDate).Visible = True
                .Cols(Col_NotesDate).Caption = "Notes Date"
                .Cols(Col_NotesDate).DataType = GetType(System.DateTime)
                .Cols(Col_NotesDate).Format = "MM/dd/yyyy h:mm tt"
                .Cols(Col_NotesDate).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter


                .Cols(Col_TemplateID).Width = _TotalWidth * 0
                .Cols(Col_TemplateID).AllowEditing = False
                .Cols(Col_TemplateID).Visible = False
                .Cols(Col_TemplateID).Caption = "TemplateID"
                .Cols(Col_TemplateID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

                .Cols(Col_NotesHeader).Width = _TotalWidth * 0.33
                .Cols(Col_NotesHeader).AllowEditing = False
                .Cols(Col_NotesHeader).Visible = True
                .Cols(Col_NotesHeader).Caption = "Notes Header"
                .Cols(Col_NotesHeader).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter


                .Cols(Col_IsFinished).Width = _TotalWidth * 0.33
                .Cols(Col_IsFinished).AllowEditing = False
                .Cols(Col_IsFinished).Visible = True
                .Cols(Col_IsFinished).Caption = "Is Finished"
                .Cols(Col_IsFinished).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter



                .Redraw = True


            End With


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        Finally

        End Try

    End Sub




    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                If c1NurseNotes.Rows.Count > 0 Then
                    c1NurseNotes.RowSel = 0
                End If
            End If
            ''--Added by Anil on 20071213
            mdlGeneral.ValidateText(txtSearch.Text, e)
            ''----
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        If _blnSearch = True Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Dim dvPatient As DataView = CType(c1NurseNotes.DataSource(), DataView)
                If IsNothing(dvPatient) Then
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
                c1NurseNotes.Enabled = False
                c1NurseNotes.DataSource = dvPatient
                c1NurseNotes.Enabled = True
                Dim strPatientSearchDetails As String

                If Trim(txtSearch.Text) <> "" Then
                    strPatientSearchDetails = Replace(txtSearch.Text, "'", "''")
                    ''''Code line below is added on 29/10/2007 by Anil, to resolve the bug, which was giving error for special characters in search.
                    strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                    strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
                Else
                    strPatientSearchDetails = ""
                End If
                Dim strSearch As String = "Notes Header"
                'Select Case strSearch
                'Case "Date"
                '    If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                '        dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                '    Else
                '        dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                '    End If
                ' Case "Notes Header"
                If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                    dvPatient.RowFilter = dvPatient.Table.Columns(3).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                Else
                    'Commented by Shubhangi 20091006
                    '                    dvPatient.RowFilter = dvPatient.Table.Columns(3).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                    'Shubhangi 20091006
                    'Use to Apply Genera & In string search
                    dvPatient.RowFilter = dvPatient.Table.Columns(3).ColumnName & " Like '%" & strPatientSearchDetails & "%' OR " _
                               & dvPatient.Table.Columns(4).ColumnName & " Like '%" & strPatientSearchDetails & "%' "


                End If
                'End Select
                Me.Cursor = Cursors.Default
            Catch objErr As Exception
                Me.Cursor = Cursors.Default
                MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    ''Code Added by Shilpa for adding the new buttons on 14th Nov 2007
    Private Sub AddNotes()
        ''''' 20070427 - Bipin
        'By Shweta 
        'If CheckPatientStatus(_PatientID) = False Then
        '    Exit Sub
        'End If
        If MainMenu.IsAccess(False, _PatientID) = False Then
            Exit Sub
        End If

        '' SUDHIR 20090521 '' CHECK PROVIDER ''
        If gblnProviderDisable = True Then
            'shweta// If ShowAssociateProvider(gnPatientID) = True Then
            If ShowAssociateProvider(_PatientID, Me) = True Then
                CType(Me.ParentForm, MainMenu).oPatientListControl.FillPatients()
            End If
        End If
        '' END SUDHIR

        '******Shweta 20090828 *********'
        'To check exeception related to word
        If CheckWordForException() = False Then
            Exit Sub
        End If
        'End Shweta


        Dim objWord As clsWordDocument = New clsWordDocument
        Dim dtWord As DataTable = objWord.FillTemplates(enumTemplateFlag.NurseNotes)
        If (IsNothing(dtWord)) Then
            objWord = Nothing
            Exit Sub
        End If
        If dtWord.Rows.Count = 0 Then
            ''''If not present then exit from sub
            MessageBox.Show("No Template is associated for Nurses Notes. Please associate any template first", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            objWord = Nothing
            dtWord.Dispose()
            dtWord = Nothing
            Exit Sub
        Else
            objWord = Nothing
            dtWord.Dispose()
            dtWord = Nothing
            _blnAdd = True
            Dim objfrmNotes As New frmNurseNotes(_PatientID)
            Try
                blnModify = False

                With objfrmNotes
                    .Text = "New Nurses Notes"
                    ' .MdiParent = Me.ParentForm
                    .MyCaller = Me
                    .MdiParent = Me.ParentForm
                    CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
                    CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False

                    .Show()
                    .WindowState = FormWindowState.Maximized
                    .BringToFront()
                End With

                '28-Jan-15 Aniket: Resolving Bug #78687: gloEMR: View PT protocol- Grid displays database based columns name
                'If objfrmNotes.CancelClick = False Then
                '    c1NurseNotes.Enabled = False
                '    'Shweta//grdNurseNotes.DataSource = objclsNotes.GetAllNurseNotes(gnPatientID)
                '    c1NurseNotes.DataSource = objclsNotes.GetAllNurseNotes(_PatientID)
                '    c1NurseNotes.Enabled = True
                'End If
            Catch ex As Exception
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
                CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True

            Finally
                objfrmNotes = Nothing
            End Try
        End If

    End Sub




    Private Sub UpdateNurseNotes()
        Dim LetterID As Long
        Dim TemplateID As Long

        Try

            If c1NurseNotes.Rows.Count > 1 And c1NurseNotes.RowSel > 0 Then
                blnModify = True
                _blnAdd = False
                LetterID = c1NurseNotes.Item(c1NurseNotes.RowSel, 0).ToString
                TemplateID = c1NurseNotes.Item(c1NurseNotes.RowSel, 2).ToString

                ' '' <><><> Record Level Locking <><><><> 
                ' '' Mahesh - 20070718 
                Dim blnRecordLock As Boolean = False
                If gblnRecordLocking = True Then
                    Dim mydt As mytable
                    ''GLO2011-0015182 : Nurse Note
                    ''wrong trntype was set to retrive 
                    mydt = Scan_n_Lock_Transaction(TrnType.NurseNotes, LetterID, 0, Now)
                    If mydt.Code <> gstrLoginName OrElse mydt.Description <> gstrClientMachineName Then
                        If MessageBox.Show("This Nurses Notes is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot modify it now. Do you want to open it?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            blnRecordLock = True
                        Else
                            mydt.Dispose()
                            mydt = Nothing
                            Exit Sub
                        End If
                    End If
                    mydt.Dispose()
                    mydt = Nothing
                End If
                '''' <><><> Record Level Locking <><><><> 

                '******Shweta 20090828 *********'
                'To check exeception related to word
                If CheckWordForException() = False Then
                    Exit Sub
                End If
                'End Shweta



                '''''''''''Code is Added by Anil on 20071103
                Dim myView As DataView = CType(c1NurseNotes.DataSource, DataView)
                If (IsNothing(myView)) Then
                    Exit Sub
                End If
                sortOrder = myView.Sort
                strSearchstring = txtSearch.Text.Trim
                arrcolumnsort = Split(sortOrder, "]")
                If arrcolumnsort.Length > 1 Then
                    strcolumnName = arrcolumnsort.GetValue(0)
                    strsortorder = arrcolumnsort.GetValue(1)
                End If
                ''''''''''''''''''''''
                'Dim grdIndex As Integer = c1NurseNotes.RowSel
                Dim objfrmNotes As frmNurseNotes = Nothing
                If c1NurseNotes.Item(c1NurseNotes.RowSel, 4).ToString = "Yes" Then
                    ''if Letter's Sataus is 'Finished' IsFinished=Yes
                    objfrmNotes = New frmNurseNotes(LetterID, TemplateID, True, blnRecordLock, _PatientID)
                Else
                    ''GLO2011-0015182 : Nurse Note 
                    ''Code is commented only because it has been handled Through isFinished Query.
                    'If blnRecordLock Then
                    ''if Letter's Sataus is 'NOT Finished' IsFinished=No and record is lock then open is as finished
                    objfrmNotes = New frmNurseNotes(LetterID, TemplateID, False, blnRecordLock, _PatientID)
                    'Else
                    '    ''if Letter's Sataus is 'NOT Finished' IsFinished=No and record is not lock
                    '    objfrmNotes = New frmNurseNotes(LetterID, TemplateID, True, blnRecordLock, _PatientID)
                    'End If

                End If
                With objfrmNotes
                    .Text = "Modify Nurses Notes"
                    '.MdiParent = Me.ParentForm
                    .MdiParent = Me.ParentForm
                    .IsModify = True
                    CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
                    CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False

                    .MyCaller = Me
                    .Show()
                    .WindowState = FormWindowState.Maximized
                    .BringToFront()
                End With

                If objfrmNotes.CancelClick = False Then
                    'grdPatientConsent.DataSource = objclsPatientConsent.GetAllPatientConsents(_PatientID)
                    c1NurseNotes.Enabled = False
                    'shweta//grdNurseNotes.DataSource = objclsNotes.GetAllNurseNotes(gnPatientID)
                    c1NurseNotes.DataSource = objclsNotes.GetAllNurseNotes(_PatientID)
                    ''Resolved issue #78687
                    SetGridStyle()
                    c1NurseNotes.Enabled = True
                    'If Not IsNothing(objclsPatientLetters.DsDataview) Then
                    '    objclsPatientLetters.SortDataview(objclsPatientLetters.GetDataview.Table.Columns(1).ColumnName)
                    'End If
                    '''' To Remember the Selection of Row 

                    If (IsNothing(c1NurseNotes.DataSource)) Then
                        Exit Sub
                    End If
                    Dim i As Integer

                    For i = 1 To c1NurseNotes.Rows.Count - 1
                        '''' when ID Found select that matching Row
                        If LetterID = c1NurseNotes.Item(i, 0) Then
                            c1NurseNotes.RowSel = i
                            c1NurseNotes.Select(i, 0)
                            Exit For
                        End If
                    Next
                    'Else
                    '    grdPatientConsent.Select(grdIndex)
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
            CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True

        Finally
            '' Resolving Issuse of Icon Dispose Error.
            'objfrmNotes = Nothing
        End Try
    End Sub


    Private Sub UpdateNotes()
        Try

            '''''<><><><><> Check Patient Status <><><><><><>''''
            ''''' 20070125 -Mahesh 
            'Shweta// If CheckPatientStatus(gnPatientID) = False Then
            'If CheckPatientStatus(_PatientID) = False Then
            '    Exit Sub
            'End If
            If MainMenu.IsAccess(False, _PatientID) = False Then
                Exit Sub
            End If
            '''''<><><><><> Check Patient Status <><><><><><>''''
            If c1NurseNotes.Rows.Count > 1 And c1NurseNotes.RowSel > 0 Then
                Call UpdateNurseNotes()
                Dim _TotalWidth As Single = 0
                _TotalWidth = Me.Width - 20
                c1NurseNotes.Width = _TotalWidth
            End If
        Catch ex As Exception
        End Try
    End Sub



    Private Sub DeleteNotes()
        Dim ID As Long
        Dim NotesDate As String
        Dim NotesHeader As String

        Try
            ''''' 20070427 - Bipin 
            'Shweta//If CheckPatientStatus(gnPatientID) = False Then
            'If CheckPatientStatus(_PatientID) = False Then

            '    Exit Sub
            'End If
            If MainMenu.IsAccess(False, _PatientID) = False Then
                Exit Sub
            End If

            If c1NurseNotes.Rows.Count > 1 Then

                If c1NurseNotes.RowSel < 1 Then
                    Exit Sub
                End If

                If c1NurseNotes.Item(c1NurseNotes.RowSel, 4) = "Yes" Then
                    MessageBox.Show("The status of Nurses Notes is finished, you cannot delete this Nurse Notes.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
                'blnModify = True
                ID = c1NurseNotes.Item(c1NurseNotes.RowSel, 0).ToString
                NotesDate = c1NurseNotes.Item(c1NurseNotes.RowSel, 1).ToString

                ' '' <><><> Record Level Locking <><><><> 
                ' '' Mahesh - 20070718 
                'Dim blnRecordLock As Boolean = False
                If gblnRecordLocking = True Then
                    Dim mydt As mytable
                    mydt = Scan_n_Lock_Transaction(TrnType.NurseNotes, ID, 0, NotesDate)
                    If mydt.Code <> gstrLoginName OrElse mydt.Description <> gstrClientMachineName Then
                        MessageBox.Show("This Nurses Notes is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot delete it.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        mydt.Dispose()
                        mydt = Nothing
                        Exit Sub
                    End If
                    mydt.Dispose()
                    mydt = Nothing
                End If
                '''' <><><> Record Level Locking <><><><> 

                If MessageBox.Show("Do you want to delete selected Nurses Notes?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then

                    NotesHeader = c1NurseNotes.Item(c1NurseNotes.RowSel, 3).ToString
                    objclsNotes.DeleteNurseNotes(ID, NotesDate, NotesHeader)

                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "Nurse notes deleted", _PatientID, ID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                    c1NurseNotes.Enabled = False
                    'shweta//grdNurseNotes.DataSource = objclsNotes.GetAllNurseNotes(gnPatientID)
                    c1NurseNotes.DataSource = objclsNotes.GetAllNurseNotes(_PatientID)
                    c1NurseNotes.Enabled = True
                    'If Not IsNothing(objclsPatientLetters.GetDataview) Then
                    '    objclsPatientLetters.SortDataview(objclsPatientLetters.GetDataview.Table.Columns(1).ColumnName)
                    'End If
                    Dim myView As DataView = CType(c1NurseNotes.DataSource, DataView)
                    If (IsNothing(myView)) Then
                        Exit Sub
                    End If
                    '''''''''''Code is Added by Anil on 20071103
                    sortOrder = myView.Sort
                    strSearchstring = txtSearch.Text.Trim
                    arrcolumnsort = Split(sortOrder, "]")
                    If arrcolumnsort.Length > 1 Then
                        strcolumnName = arrcolumnsort.GetValue(0)
                        strsortorder = arrcolumnsort.GetValue(1)
                    End If

                    SetGridStyle(strcolumnName, strsortorder, strSearchstring)
                    ''''''''''''''''''
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Public Sub RefreshNotes(ByVal NotesID As Long)

        c1NurseNotes.Enabled = False
        'Shweta// grdNurseNotes.DataSource = objclsNotes.GetAllNurseNotes(gnPatientID)
        c1NurseNotes.DataSource = objclsNotes.GetAllNurseNotes(_PatientID)
        c1NurseNotes.Enabled = True
        '''''''''''Code is Added by Anil on 20071103
        '''''Flag is checked to find whether we are adding or modifying the record, so according to that the grid will be filled.


        If _blnAdd = False Then

            If (IsNothing(dv)) Then
                dv = CType(c1NurseNotes.DataSource, DataView)
                If (IsNothing(dv)) Then
                    Exit Sub
                End If
            End If
            sortOrder = dv.Sort
            strSearchstring = txtSearch.Text.Trim
            arrcolumnsort = Split(sortOrder, "]")
            If arrcolumnsort.Length > 1 Then
                strcolumnName = arrcolumnsort.GetValue(0)
                strsortorder = arrcolumnsort.GetValue(1)
            End If
            '''''''''''''''''''''''
            SetGridStyle(strcolumnName, strsortorder, strSearchstring)
            '''' To Remember the Selection of Row 
            'Dim i As Integer
            'For i = 0 To CType(grdNurseNotes.DataSource, DataView).Count - 1
            '    '''' when ID Found select that matching Row
            '    If NotesID = grdNurseNotes.Item(i, 0) Then
            '        grdNurseNotes.CurrentRowIndex = i
            '        grdNurseNotes.Select(i)
            '        Exit For
            '    End If
            'Next
        Else
            SetGridStyle()
        End If

        If NotesID <> 0 Then
            c1NurseNotes.RowSel = -1
        End If
        Dim i As Integer
        Dim myView As DataView = CType(c1NurseNotes.DataSource, DataView)
        If (IsNothing(myView)) Then
            Exit Sub
        End If
        For i = 1 To c1NurseNotes.Rows.Count - 1
            '''' when ID Found select that matching Row
            If NotesID = c1NurseNotes.Item(i, 0) Then
                c1NurseNotes.RowSel = i

                Exit For
            End If
        Next
    End Sub
    Public Sub SetGridSelection()
        Try
            SetControlSelection()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub RefreshNotes()
        Try
            c1NurseNotes.Enabled = False
            'Shweta//grdNurseNotes.DataSource = objclsNotes.GetAllNurseNotes(gnPatientID)
            c1NurseNotes.DataSource = objclsNotes.GetAllNurseNotes(_PatientID)
            c1NurseNotes.Enabled = True
            SetGridStyle()
            If c1NurseNotes.Rows.Count > 1 Then
                c1NurseNotes.RowSel = 1

            End If
            txtSearch.Text = ""
            _blnSearch = True
            'Call RefreshConsent()
        Catch ex As Exception
            'MessageBox.Show(ex.ToString, "Nurse Notes", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ts_ViewButtons_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Add"
                Call AddNotes()
            Case "Modify"
                Call UpdateNotes()
            Case "Delete"
                Call DeleteNotes()
            Case "Refresh"
                Call RefreshNotes()
            Case "Close"
                Call Me.Close()
        End Select
    End Sub

    Private Sub frmVWNurseNotes_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try

            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
       
        Try

            Me.WindowState = FormWindowState.Maximized
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmVWNurseNotes_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RemoveGotFocusListener(Me)
    End Sub


    Private Sub frmVWNurseNotes_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            'Line commented by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            '_PatientID = gnPatientID
            'end modification

            c1NurseNotes.Enabled = False
            Dim dv As DataView = objclsNotes.GetAllNurseNotes(_PatientID)
            '' dv.Table.Columns(1).DataType = GetType(System.String)
            c1NurseNotes.DataSource = dv ''objclsNotes.GetAllNurseNotes(_PatientID)
            c1NurseNotes.Enabled = True
            ''nLetterID, dtLetterDate, nTemplateID, sTemplateName

            SetGridStyle()
            'Sanjog - Added on 2011 May 17 for Patient Safety
            Try
                AddGotFocusListener(Me)
                gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
            'Sanjog - Added on 2011 May 17 for Patient Safety
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        'Shubhangi 20091006
        'Use clear button to clear search text box
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub

    Private Sub frmVWNurseNotes_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            _GridWidth = c1NurseNotes.Width
            SetGridStyle()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Public Sub New(ByVal PatientID As Long)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _PatientID = PatientID
    End Sub

    'SHUBHANGI 20110401

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

    Private Sub c1NurseNotes_AfterSort(sender As Object, e As C1.Win.C1FlexGrid.SortColEventArgs) Handles c1NurseNotes.AfterSort  ''added for solving bugid 72082 sorting issue
        Try
            If Ind > -1 Then
                Dim rw As C1.Win.C1FlexGrid.Row
                For Each rw In c1NurseNotes.Rows
                    Dim cm As CurrencyManager = CType(BindingContext(Me.c1NurseNotes.DataSource), CurrencyManager)
                    Dim dr As DataRowView = CType(rw.DataSource, DataRowView)
                    If Not dr Is Nothing Then
                        Dim currIndex As Integer = dr.Row.Table.Rows.IndexOf(dr.Row)
                        If currIndex = Ind Then
                            Dim cr As C1.Win.C1FlexGrid.CellRange = c1NurseNotes.GetCellRange(rw.Index, 1)
                            ' to scroll the selected row in the visible area
                            c1NurseNotes.Select(cr, True)
                            cr = c1NurseNotes.GetCellRange(rw.Index, 0, rw.Index, c1NurseNotes.Cols.Count - 1)
                            c1NurseNotes.RowSel = rw.Index
                            c1NurseNotes.Select(cr, False)
                            Exit For
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(" InterfaceMessageQueue AfterSort " + ex.Message.ToString(), False)
        End Try
        Ind = -1
    End Sub

    Private Sub c1NurseNotes_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles c1NurseNotes.MouseClick

        Try

            If (Not IsNothing(c1NurseNotes.DataSource) AndAlso (c1NurseNotes.Rows.Count > 0)) Then
                Dim cm As CurrencyManager = CType(BindingContext(Me.c1NurseNotes.DataSource), CurrencyManager)
                Dim dr As DataRowView = CType(cm.Current, DataRowView)
                Ind = dr.Row.Table.Rows.IndexOf(dr.Row)

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(" InterfaceMessageQueue MouseClick " + ex.Message.ToString(), False)
        End Try
    End Sub

    Private Sub c1NurseNotes_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles c1NurseNotes.MouseDoubleClick
        Try


            GetControlSelection()

            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = c1NurseNotes.HitTest(ptPoint)
            ''''''''''''Code is Added by Anil on 20071103
            If htInfo.Type = DataGrid.HitTestType.ColumnHeader Then

                If txtSearch.Text = "" Then
                    _blnSearch = True
                Else
                    _blnSearch = False
                    txtSearch.Text = ""
                    _blnSearch = True
                End If
                '''''''''''''''''''''''''''''''''''''
            ElseIf htInfo.Type = DataGrid.HitTestType.Cell Then
                'If CheckPatientStatus(_PatientID) = False Then
                '    Exit Sub
                'End If
                If MainMenu.IsAccess(False, _PatientID) = False Then
                    Exit Sub
                End If
                UpdateNurseNotes()
            Else
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
       



    End Sub
End Class