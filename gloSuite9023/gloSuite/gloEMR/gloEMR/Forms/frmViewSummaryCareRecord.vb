Imports gloCommon
Imports gloEMR.gloEMRWord
Imports C1.Win.C1FlexGrid
Imports System.Windows.Forms
'Imports System.Drawing.Design

Public Class frmViewSummaryCareRecord
    Dim _blnSearch As Boolean = True

    Dim _patientID As Int64 = 0
    Dim dtTransactionDate As DateTime =DateTime.now 

    Dim bRecordUnavailable As Boolean
    Dim bRecordRemark As String = String.Empty
    Dim SummaryOfCareRecordStatusID As Int64 = 0
    Dim COL_TransactionDate As Integer = 0
    Dim COL_RecordUnAvailable As Integer = 1
    Dim COL_RecordRemark As Integer = 2
    Dim Col_SummaryOfCareRecordStatusID As Integer = 3
    Dim COLUMN_COUNT As Int16 = 4
    Public gstrMessageBoxCaption As String = "gloEMR"
    Dim dtsummary As DataTable = Nothing
    Public Shared IsOpen As Boolean = False



    Dim objclsViewSummaryCareRecord As New clsViewSummaryCareRecord
    Dim frmAddSummaryCareRecord As frmAddSummaryCareRecord
    
   
    Public Property patientID As Long
        Get
            Return _patientID
        End Get
        Set(ByVal Value As Long)
            _patientID = Value
        End Set
    End Property

   
    Private Sub frmViewSummaryCareRecord_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If Not IsNothing(dtsummary) Then
            dtsummary.Dispose()
            dtsummary = Nothing

        End If
        If Not IsNothing(objclsViewSummaryCareRecord) Then
            objclsViewSummaryCareRecord.Dispose()
            objclsViewSummaryCareRecord = Nothing

        End If
    End Sub

   


    Private Sub frmViewSummaryCareRecord_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        loadPatientStrip()
        Dim scheme As gloCommon.Cls_TabIndexSettings.TabScheme = gloCommon.Cls_TabIndexSettings.TabScheme.AcrossFirst
        Dim tom As New Cls_TabIndexSettings(Me)
        tom.SetTabOrder(scheme)
        tom = Nothing
       
    End Sub
    

    Private Sub ts_btnClose_Click(sender As Object, e As System.EventArgs) Handles ts_btnClose.Click
        Me.Close()
    End Sub

    Private Sub ts_btnAdd_Click(sender As Object, e As System.EventArgs) Handles ts_btnAdd.Click
        Dim rowsel As Integer = 0
        Dim ID As Long = 0
        
        rowsel = c1SummaryCareRecord.RowSel
        frmAddSummaryCareRecord = New frmAddSummaryCareRecord(patientID)
        frmAddSummaryCareRecord.TransactionDate = DateTime.Now
        frmAddSummaryCareRecord.ShowDialog(Me)
        ID = frmAddSummaryCareRecord.sumofcare_ID
       
        If (Not IsNothing(dtsummary)) Then
            dtsummary.Dispose()
            dtsummary = Nothing
        End If

        dtsummary = objclsViewSummaryCareRecord.Get_SummaryCareRecord(patientID)

        If Not IsNothing(dtsummary) Then
            Dim dr() As DataRow
            dr = dtsummary.Select("SummaryOfCareRecordStatusID =" & ID)
            Dim intindex As Integer = 0
            If (dr.Length > 0) Then
                intindex = dtsummary.Rows.IndexOf(dr(0))
                intindex = intindex + 1
                Array.Clear(dr, 0, dr.Length)
            Else
                intindex = 0
            End If


            FillGrid(dtsummary, intindex)

        End If

        If (IsNothing(frmAddSummaryCareRecord) = False) Then
            frmAddSummaryCareRecord.Close()
            frmAddSummaryCareRecord.Dispose()
            frmAddSummaryCareRecord = Nothing
        End If


    End Sub

    Public Sub FillGrid(ByVal dtsumm As DataTable, Optional ByVal intindex As Integer = 0)
        Try

        

        With c1SummaryCareRecord



            .Redraw = False
            Dim _TotalWidth As Single = .Width - 5
            c1SummaryCareRecord.Cols(Col_SummaryOfCareRecordStatusID).Width = _TotalWidth * 0.3
            c1SummaryCareRecord.Cols(Col_SummaryOfCareRecordStatusID).AllowEditing = False
            c1SummaryCareRecord.Cols(Col_SummaryOfCareRecordStatusID).Visible = False
            c1SummaryCareRecord.SetData(0, Col_SummaryOfCareRecordStatusID, "SummaryCareID")
                c1SummaryCareRecord.Cols(Col_SummaryOfCareRecordStatusID).TextAlign = TextAlignEnum.LeftCenter


                c1SummaryCareRecord.Cols(COL_TransactionDate).Width = _TotalWidth * 0.1
                c1SummaryCareRecord.Cols(COL_TransactionDate).AllowEditing = False
                c1SummaryCareRecord.SetData(0, COL_TransactionDate, "Date")
                c1SummaryCareRecord.Cols(COL_TransactionDate).TextAlign = TextAlignEnum.LeftCenter
                .Cols(COL_TransactionDate).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter



                c1SummaryCareRecord.Cols(COL_RecordUnAvailable).Width = _TotalWidth * 0.2
                c1SummaryCareRecord.Cols(COL_RecordUnAvailable).AllowEditing = False
                c1SummaryCareRecord.SetData(0, COL_RecordUnAvailable, "Record Unavailable")
                c1SummaryCareRecord.Cols(COL_RecordUnAvailable).TextAlign = TextAlignEnum.LeftCenter
                .Cols(COL_RecordUnAvailable).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter


                c1SummaryCareRecord.Cols(COL_RecordRemark).Width = _TotalWidth * 0.2
                c1SummaryCareRecord.Cols(COL_RecordRemark).AllowEditing = False
                c1SummaryCareRecord.SetData(0, COL_RecordRemark, "Remarks")
                c1SummaryCareRecord.Cols(COL_RecordRemark).TextAlign = TextAlignEnum.LeftCenter
                .Cols(COL_RecordRemark).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

            c1SummaryCareRecord.Cols.Count = COLUMN_COUNT
            If (c1SummaryCareRecord.Rows.Count > 1) Then
                c1SummaryCareRecord.Rows.RemoveRange(1, c1SummaryCareRecord.Rows.Count - 1)
            End If

            ''--------------------------------------------------------------------------
         
         

            For Each dr As DataRow In dtsumm.Rows
                .Rows.Add()
                .SetData(.Rows.Count - 1, COL_TransactionDate, dr("dtTransactionDate"))
                .SetData(.Rows.Count - 1, COL_RecordRemark, dr("bRecordRemark"))
                .SetData(.Rows.Count - 1, COL_RecordUnAvailable, dr("bRecordUnavailable"))
                .SetData(.Rows.Count - 1, Col_SummaryOfCareRecordStatusID, dr("SummaryOfCareRecordStatusID"))
            Next
            .Redraw = True
            c1SummaryCareRecord.Select(intindex, 0)
        End With
        Catch ex As Exception

        End Try
    End Sub
    Dim r As Integer
    Private Sub c1SummaryCareRecord_DoubleClick(sender As Object, e As System.EventArgs) Handles c1SummaryCareRecord.DoubleClick


        If r > 0 Then


            If Not IsNothing(c1SummaryCareRecord) Then
                If c1SummaryCareRecord.Rows.Count > 0 Then
                    If c1SummaryCareRecord.RowSel > 0 Then
                        c1SummaryCareRecord.Select(c1SummaryCareRecord.RowSel, 1)
                        ts_btnModify_Click(sender, e)
                        'txtSearch_TextChanged(sender, e)

                    End If
                    End If
                End If
            End If

    End Sub
    Dim dttoday As Date = Nothing

    Private Sub ts_btnModify_Click(sender As Object, e As System.EventArgs) Handles ts_btnModify.Click
        Dim i As Integer
        Dim commdt As String = String.Empty
        Dim Remark As String = String.Empty
        Dim RecAval As Boolean
        Dim sumcareID As Long
        Dim rowsel As Integer = 0
        Try

            dttoday = Nothing
            If c1SummaryCareRecord.Rows.Count > 0 Then
                i = c1SummaryCareRecord.RowSel
                If i <= 0 Then
                    Exit Sub
                End If

                commdt = Convert.ToString(c1SummaryCareRecord.GetData(i, COL_TransactionDate))
                Remark = Convert.ToString(c1SummaryCareRecord.GetData(i, COL_RecordRemark))

                If Convert.ToString(c1SummaryCareRecord.GetData(i, COL_RecordUnAvailable)).Trim() = "Yes" Then
                    RecAval = 1
                Else
                    RecAval = 0
                End If
                sumcareID = Convert.ToInt64(c1SummaryCareRecord.GetData(i, Col_SummaryOfCareRecordStatusID))
            End If
            Dim frm As frmAddSummaryCareRecord
            frm = New frmAddSummaryCareRecord(_patientID)
            frm.patientID = patientID
            frm.SummaryOfCareRecordStatusID = sumcareID
            frm.TransactionDate = commdt
            frm.bRecordUnavailable = RecAval
            frm.strRecordRemark = Remark
            rowsel = c1SummaryCareRecord.RowSel
            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            If (Not IsNothing(dtsummary)) Then
                dtsummary.Dispose()
                dtsummary = Nothing
            End If
            dtsummary = objclsViewSummaryCareRecord.Get_SummaryCareRecord(patientID)
            FillGrid(dtsummary)
            c1SummaryCareRecord.Select(rowsel, 0)
            If (IsNothing(frm) = False) Then
                frm.Dispose()
                frm = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally

        End Try


    End Sub
    Private Sub ts_btnDelete_Click(sender As Object, e As System.EventArgs) Handles ts_btnDelete.Click
        Dim rowNo As Integer = -1
        Dim summCareRecId As Int64
        Dim rowsel As Integer = 0
        If c1SummaryCareRecord.Rows.Count > 0 Then
            rowNo = c1SummaryCareRecord.RowSel
            If rowNo <= 0 Then
                Exit Sub
            End If
            rowsel = c1SummaryCareRecord.RowSel
            If rowNo > 0 Then
                If MessageBox.Show("Are you sure you want to delete the selected record?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    c1SummaryCareRecord.Select(rowNo, Col_SummaryOfCareRecordStatusID)
                    summCareRecId = c1SummaryCareRecord.GetData(c1SummaryCareRecord.RowSel, Col_SummaryOfCareRecordStatusID)
                    If (Not IsNothing(dtsummary)) Then
                        dtsummary.Dispose()
                        dtsummary = Nothing
                    End If
                    dtsummary = objclsViewSummaryCareRecord.DeleteSummaryCareRecord(summCareRecId, patientID)

                    FillGrid(dtsummary)
                    If c1SummaryCareRecord.Rows.Count > 1 Then

                        c1SummaryCareRecord.Select(1, 0)
                        'Else
                        '   
                    End If
                    'c1SummaryCareRecord.Select(-1, -1)

                End If
            End If
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.SummaryCareRecord, gloAuditTrail.ActivityCategory.SummaryCareRecord, gloAuditTrail.ActivityType.Delete, "Summary Care Record Deleted", _patientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

        End If
    End Sub
    Public Function ValidateText(ByVal Text As String, ByVal e As KeyPressEventArgs)
        Try
            If InStr(Trim(Text), ".") <> 0 And (e.KeyChar = ChrW(46)) Or ((e.KeyChar >= ChrW(35) And e.KeyChar <= ChrW(38)) Or (e.KeyChar = ChrW(64)) Or (e.KeyChar = ChrW(33)) Or (e.KeyChar = ChrW(42)) Or (e.KeyChar = ChrW(43)) Or (e.KeyChar = ChrW(45)) Or (e.KeyChar = ChrW(60)) Or (e.KeyChar = ChrW(59)) Or (e.KeyChar = ChrW(61)) Or (e.KeyChar = ChrW(94)) Or (e.KeyChar = ChrW(96)) Or (e.KeyChar = ChrW(124)) Or (e.KeyChar = ChrW(125)) Or (e.KeyChar = ChrW(126))) Then
                e.Handled = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return Nothing
    End Function

    Private Sub txtSearch_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress

    End Sub
    Public Sub ResetSearchText()
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As System.EventArgs) Handles btnClear.Click
        ResetSearchText()
        FillGrid(dtsummary)
    End Sub
    Dim dv As DataView
    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        If _blnSearch = True Then
            Try
                Me.Cursor = Cursors.WaitCursor

                dv = dtsummary.DefaultView
                If IsNothing(dv) Then
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If



                Dim strPatientSearchDetails As String
                If txtSearch.Text.Trim().Length > 0 Then
                    strPatientSearchDetails = Replace(txtSearch.Text, "'", "''")

                    strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                    strPatientSearchDetails = ReplaceSpecialCharacters(strPatientSearchDetails)
                Else
                    strPatientSearchDetails = ""
                End If

                dv.RowFilter = "bRecordRemark Like '%" & strPatientSearchDetails & "%'"
                FillGrid(dv.ToTable())

                Me.Cursor = Cursors.Default

            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If

    End Sub
    Public Function ReplaceSpecialCharacters(ByVal strSpecialChar As String) As String
        Try

            strSpecialChar = Replace(strSpecialChar, "#", "[#]") & ""
            strSpecialChar = Replace(strSpecialChar, "$", "[$]") & ""
            strSpecialChar = Replace(strSpecialChar, "%", "[%]") & ""
            strSpecialChar = Replace(strSpecialChar, "^", "[^]") & ""
            strSpecialChar = Replace(strSpecialChar, "&", "[&]") & ""

            '' Was Commented Before 2090602
            '' Uncommneted By Mahesh to Handle the Special Char in search By Replacing char with '[Char]'
            '' Ref: http://sqlserver2000.databases.aspfaq.com/how-do-i-search-for-special-characters-e-g-in-sql-server.html
            strSpecialChar = Replace(strSpecialChar, "~", "[~]") & ""
            strSpecialChar = Replace(strSpecialChar, "!", "[!]") & ""
            strSpecialChar = Replace(strSpecialChar, "*", "[*]") & ""
            strSpecialChar = Replace(strSpecialChar, ";", "[;]") & ""
            strSpecialChar = Replace(strSpecialChar, "/", "[/]") & ""
            strSpecialChar = Replace(strSpecialChar, "?", "[?]") & ""
            strSpecialChar = Replace(strSpecialChar, ">", "[>]") & ""
            strSpecialChar = Replace(strSpecialChar, "<", "[<]") & ""
            strSpecialChar = Replace(strSpecialChar, "\", "[\]") & ""
            strSpecialChar = Replace(strSpecialChar, "|", "[|]") & ""
            strSpecialChar = Replace(strSpecialChar, "{", "[{]") & ""
            strSpecialChar = Replace(strSpecialChar, "}", "[}]") & ""
            strSpecialChar = Replace(strSpecialChar, "-", "[-]") & ""
            strSpecialChar = Replace(strSpecialChar, "_", "[_]") & ""
            ''END Was Commented Before 2090602
            Return strSpecialChar
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
    Private WithEvents _PatientStrip As gloUserControlLibrary.gloUC_PatientStrip = Nothing
    Private Sub loadPatientStrip()
        If IsNothing(_PatientStrip) = False Then
            Me.Controls.Remove(_PatientStrip)
            _PatientStrip.Dispose() : _PatientStrip = Nothing
        End If
        _PatientStrip = New gloUserControlLibrary.gloUC_PatientStrip
        '_PatientStrip = New gloUserControlLibrary.gloUC_PatientStrip(Me)
        _PatientStrip.ShowDetail(patientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.SummaryCareRecord)
        _PatientStrip.Dock = DockStyle.Top
        _PatientStrip.Padding = New Padding(3, 0, 3, 0)
        _PatientStrip.BringToFront()

        '  _PatientStrip.f()
        _PatientStrip.DTP.CustomFormat = "MM/dd/yyyy"
        Me.Controls.Add(_PatientStrip)
        pnlToolStrip.SendToBack()
        'pnlWebbrowser.BringToFront()
    End Sub


    Private Sub frmViewSummaryCareRecord_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
       
        c1SummaryCareRecord.Cols.Count = COLUMN_COUNT

        gloC1FlexStyle.Style(c1SummaryCareRecord, True)

        If (Not IsNothing(dtsummary)) Then
            dtsummary.Dispose()
            dtsummary = Nothing
        End If
        dtsummary = objclsViewSummaryCareRecord.Get_SummaryCareRecord(patientID)
        FillGrid(dtsummary)
        Try
            gloPatient.gloPatient.GetWindowTitle(Me, _patientID, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            Cursor.Current = Cursors.Default
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub c1SummaryCareRecord_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles c1SummaryCareRecord.MouseDown
        If e.Button = MouseButtons.Left Then
            With c1SummaryCareRecord
                r = .HitTest(e.X, e.Y).Row
                
            End With
End If 
    End Sub
End Class