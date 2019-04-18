Imports C1.Win.C1FlexGrid
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports gloEMR.gloEMRWord
Imports Wd = Microsoft.Office.Interop.Word
Imports pdftron.PDF
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports gloEDocumentV3
Imports System.Drawing.Printing
Imports gloEMRGeneralLibrary.gloEMRLab
Imports gloEMRReports
Imports gloUserControlLibrary
Imports System.Text
Imports gloSettings
Imports gloCMSEDI
Imports gloBilling


Public Class frmClinicalChart
    Implements IPatientContext, IHotKey
    Private intFullFilePageCount As Integer = 0
    Private intDMSFilePageCount As Integer = 0
    Private OrderIDList As String = ""
    Private _MessageBoxCaption As String = String.Empty
    Private Const COL_Check As Integer = 0
    Private Const COL_Category As Integer = 1
    Private Const COL_Provider As Integer = 2
    Private Const COL_Date As Integer = 3

    Private Const COL_Four As Integer = 4
    Private Const COL_Five As Integer = 5
    Private Const COL_Six As Integer = 6
    Private Const COL_Seven As Integer = 7

    Private UserID As Int64 = 0
    Private UserName As String = String.Empty

    Public Delegate Sub GetQueuedata(ByVal Backgroundprocessas As gloUIControlLibrary.Classes.ClinicalChartQueue.enumBackgroundProcessType)
    Public Event EvnMsgQueue As GetQueuedata

    Public Delegate Sub GetQueuedataForPrint()
    Public Event EvnMsgQueueforPrint As GetQueuedataForPrint

    ''Sanjog
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings

    Dim _PatientID As Long
    Dim _nQueueID As Long = 0
    Dim _nAssociatedContactID As Long = 0
    Dim isloading As Boolean = True
    Dim isblankDoc As Boolean = True
    Dim dtQueued As DataTable = Nothing
    Dim dtQueuedStartDate As Date = Nothing
    Dim dtQueuedEndDate As Date = Nothing
    Private WithEvents _PatientStrip As gloUC_PatientStrip = Nothing

    Dim AuditLogModuleList As New StringBuilder

    Dim lstClaimMasterIDs As New List(Of ClaimNoteIDs)
    Dim sNoteDescription As String = String.Empty
    Dim dtDistinctClaim As DataTable = Nothing
    Enum ClaimPrintType
        PrintForm
        PrintData
    End Enum


    Private Sub frmClinicalChart_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            If Not IsNothing(Me.ParentForm) Then
                CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.ClinicalChart, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub frmClinicalChart_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            If lstClaimMasterIDs IsNot Nothing Then
                lstClaimMasterIDs.Clear()
                lstClaimMasterIDs = Nothing
            End If

            If IsNothing(_PatientStrip) = False Then
                Me.Panel4.Controls.Remove(_PatientStrip)
                _PatientStrip.Dispose()
                _PatientStrip = Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
        End Try
    End Sub
    Private Shared opsPrinter As New PrinterSettings
  Private Sub frmClinicalChart_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            Me.SuspendLayout()

        Catch ex As System.InvalidOperationException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
            MessageBox.Show("Unable to load required components for clinical chart.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        Catch ex As Exception
            If (ex.ToString.Contains("AxFramerControl") = True) Then
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
                MessageBox.Show("Unable to load required components for clinical chart.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            Else
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
            End If
        End Try
        Try
            isloading = True
            mskStartDate.Text = Today.AddMonths(-1).ToString("MM/dd/yyyy")
            mskEndDate.Text = Today.ToString("MM/dd/yyyy")
            isloading = False
            If _nQueueID > 0 Then
                mskStartDate.Text = dtQueuedStartDate.ToString("MM/dd/yyyy")
                mskEndDate.Text = dtQueuedEndDate.ToString("MM/dd/yyyy")
            End If
            ArrangeGridItems()
            Me.Visible = True

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
            Me.Visible = True
        Finally
            Me.ResumeLayout()
        End Try
        Try
            gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        '''' Check Secure Messaging is enable and User has rights to access it
        If gblnIsSecureMsgEnable = False Or gblnSecureUserrights = False Then
            tlbbtn_SendSS.Visible = False

        End If
        LoadPrinter()
        If rbPrintData.Checked Then
            pnlClaimPrinter.Visible = True
            If _nQueueID <> 0 Then
                Dim sprintername = GetQueuedPrinterName(_nQueueID)
                cmbClaimPrinter.Text = sprintername
            Else
                cmbClaimPrinter.Text = opsPrinter.PrinterName
            End If
        ElseIf rbPrintOnForm.Checked Then
            pnlClaimPrinter.Visible = False
        End If
    End Sub
    'temp function

    Public Sub New(ByVal PatientID As Long)

        ' Th0is call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _PatientID = PatientID
        If appSettings("MessageBOXCaption") IsNot Nothing Then

            If appSettings("MessageBOXCaption") <> "" Then

                _MessageBoxCaption = System.Convert.ToString(appSettings("MessageBOXCaption"))
            Else
                _MessageBoxCaption = "gloEMR"
            End If
        End If

        If appSettings("UserID") IsNot Nothing Then

            If appSettings("UserID") <> "" Then

                UserID = System.Convert.ToInt64(appSettings("UserID"))
            Else
                UserID = 0
            End If
        End If

        If appSettings("UserName") IsNot Nothing Then

            If appSettings("UserName") <> "" Then

                UserName = System.Convert.ToString(appSettings("UserName"))
            Else
                UserName = ""
            End If
        End If

        loadPatientStrip()

    End Sub

    Public Sub New(ByVal PatientID As Long, ByVal nQueueID As Long, ByVal nAssociatedQueueID As Long, ByVal dtStartDate As Date, ByVal dtEndDate As Date)

        ' Th0is call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _PatientID = PatientID
        _nQueueID = nQueueID
        _nAssociatedContactID = nAssociatedQueueID
        dtQueuedStartDate = dtStartDate
        dtQueuedEndDate = dtEndDate
        If appSettings("MessageBOXCaption") IsNot Nothing Then

            If appSettings("MessageBOXCaption") <> "" Then

                _MessageBoxCaption = System.Convert.ToString(appSettings("MessageBOXCaption"))
            Else
                _MessageBoxCaption = "gloEMR"
            End If
        End If

        If appSettings("UserID") IsNot Nothing Then

            If appSettings("UserID") <> "" Then

                UserID = System.Convert.ToInt64(appSettings("UserID"))
            Else
                UserID = 0
            End If
        End If

        If appSettings("UserName") IsNot Nothing Then

            If appSettings("UserName") <> "" Then

                UserName = System.Convert.ToString(appSettings("UserName"))
            Else
                UserName = ""
            End If
        End If

        loadPatientStrip()
        setQueuedFormType(_nQueueID)
        tblbtn_Queue.Visible = False

    End Sub

    Private Sub DateMouseClick(sender As Object, e As MouseEventArgs) Handles mskStartDate.MouseClick, mskEndDate.MouseClick, mskDOS.MouseClick
        DirectCast(sender, MaskedTextBox).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
        If DirectCast(sender, MaskedTextBox).Text.Trim() = "" Then
            DirectCast(sender, MaskedTextBox).SelectionStart = 0
            DirectCast(sender, MaskedTextBox).SelectionLength = 0
        End If
    End Sub

    Private Sub ClaimNodeCheck(ByVal RowIndex As Integer)
        Dim sOriginalNodeType As String = C1ClinicalChart.Rows(RowIndex).UserData
        Dim sNodeType As String = C1ClinicalChart.Rows(RowIndex).UserData
        Dim cbStatus As C1.Win.C1FlexGrid.CheckEnum
        Dim cbStatusChild As C1.Win.C1FlexGrid.CheckEnum
        Dim ParentNodeIndex As Integer = C1ClinicalChart.Rows(RowIndex).Node.Parent.Row.Index + 1
        Dim AllChildSelected As Boolean = True
        Dim NextRowIndex As Integer = 0
        NextRowIndex = RowIndex + 1
        cbStatus = C1ClinicalChart.GetCellCheck(RowIndex, 1)

        Try

            'Individual Claim/Payment Node
            If (sNodeType = "Individual Claim" Or sNodeType = "Individual Payment" Or sNodeType = "Individual History") Then
                C1ClinicalChart.SetCellCheck(RowIndex, 1, cbStatus)
                While (System.Convert.ToString(((C1ClinicalChart.Rows(ParentNodeIndex).UserData) = "Individual Claim" And C1ClinicalChart.Rows(RowIndex)(4).ToString.Trim() <> "0") Or
                        (System.Convert.ToString(C1ClinicalChart.Rows(ParentNodeIndex).UserData) = "Individual Payment") And C1ClinicalChart.Rows(RowIndex)(4).ToString.Trim() <> "0") Or
                        (System.Convert.ToString(C1ClinicalChart.Rows(ParentNodeIndex).UserData) = "Individual History") And C1ClinicalChart.Rows(RowIndex)(4).ToString.Trim() <> "0")
                    cbStatusChild = C1ClinicalChart.GetCellCheck(ParentNodeIndex, 1)
                    If (cbStatusChild = C1.Win.C1FlexGrid.CheckEnum.Unchecked) Then
                        AllChildSelected = False
                        Exit While
                    End If
                    ParentNodeIndex = ParentNodeIndex + 1
                    If (ParentNodeIndex = C1ClinicalChart.Rows.Count) Then
                        Exit While
                    End If
                End While
                If (AllChildSelected) Then
                    C1ClinicalChart.SetCellCheck(C1ClinicalChart.Rows(RowIndex).Node.Parent.Row.Index, 1, C1.Win.C1FlexGrid.CheckEnum.Checked)
                Else
                    C1ClinicalChart.SetCellCheck(C1ClinicalChart.Rows(RowIndex).Node.Parent.Row.Index, 1, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                End If
            End If

            If System.Convert.ToString(C1ClinicalChart.Rows(RowIndex).Node.Parent.Row.UserData) = "Claim Details" Then
                sNodeType = "Claim Details"
                ParentNodeIndex = C1ClinicalChart.Rows(RowIndex).Node.Parent.Row.Index
            ElseIf System.Convert.ToString(C1ClinicalChart.Rows(RowIndex).Node.Parent.Row.UserData) = "Payment Details" Then
                sNodeType = "Payment Details"
                ParentNodeIndex = C1ClinicalChart.Rows(RowIndex).Node.Parent.Row.Index
            ElseIf System.Convert.ToString(C1ClinicalChart.Rows(RowIndex).Node.Parent.Row.UserData) = "Claim History" Then
                sNodeType = "Claim History"
                ParentNodeIndex = C1ClinicalChart.Rows(RowIndex).Node.Parent.Row.Index
            End If



            'Claim Details Node
            If sOriginalNodeType = "Claim Details" Then
                If (sNodeType = "Claim Details") Then
                    While (System.Convert.ToString(C1ClinicalChart.Rows(NextRowIndex).UserData) <> "Claim" And
                                 System.Convert.ToString(C1ClinicalChart.Rows(NextRowIndex).UserData) <> "Payment Details" And
                                 System.Convert.ToString(C1ClinicalChart.Rows(NextRowIndex).UserData) <> "Claim History" And
                                 C1ClinicalChart.Rows(NextRowIndex)(4).ToString.Trim() <> "0")
                        C1ClinicalChart.SetCellCheck(NextRowIndex, 1, cbStatus)
                        NextRowIndex = NextRowIndex + 1
                        If (NextRowIndex = C1ClinicalChart.Rows.Count) Then
                            Exit While
                        End If
                    End While
                End If
            End If
            'Payment Details Node
            If sOriginalNodeType = "Payment Details" Then
                If (sNodeType = "Payment Details") Then
                    While (System.Convert.ToString(C1ClinicalChart.Rows(NextRowIndex).UserData) <> "Claim" And
                           System.Convert.ToString(C1ClinicalChart.Rows(NextRowIndex).UserData) <> "Claim History" And
                           C1ClinicalChart.Rows(NextRowIndex)(4).ToString.Trim() <> "0")
                        C1ClinicalChart.SetCellCheck(NextRowIndex, 1, cbStatus)
                        NextRowIndex = NextRowIndex + 1
                        If (NextRowIndex = C1ClinicalChart.Rows.Count) Then
                            Exit While
                        End If
                    End While
                End If
            End If


            'Claim History Node
            If sOriginalNodeType = "Claim History" Then
                If (sNodeType = "Claim History") Then
                    While (System.Convert.ToString(C1ClinicalChart.Rows(NextRowIndex).UserData) <> "Claim" And
                           C1ClinicalChart.Rows(NextRowIndex)(4).ToString.Trim() <> "0")
                        C1ClinicalChart.SetCellCheck(NextRowIndex, 1, cbStatus)
                        NextRowIndex = NextRowIndex + 1
                        If (NextRowIndex = C1ClinicalChart.Rows.Count) Then
                            Exit While
                        End If
                    End While
                End If
            End If



            If (sNodeType = "Claim Details" Or sNodeType = "Payment Details" Or sNodeType = "Claim History") Then
                If (sOriginalNodeType <> "Claim Details" Or sOriginalNodeType <> "Payment Details" Or sOriginalNodeType <> "Claim History") Then
                    ParentNodeIndex = C1ClinicalChart.Rows(ParentNodeIndex).Node.Parent.Row.Index + 1
                End If

                While System.Convert.ToString(C1ClinicalChart.Rows(ParentNodeIndex).UserData) <> "Claim"
                    If System.Convert.ToString(C1ClinicalChart.Rows(ParentNodeIndex).UserData) = "Claim Details" Or
                       System.Convert.ToString(C1ClinicalChart.Rows(ParentNodeIndex).UserData) = "Payment Details" Or
                       System.Convert.ToString(C1ClinicalChart.Rows(ParentNodeIndex).UserData) = "Claim History" Then
                        cbStatusChild = C1ClinicalChart.GetCellCheck(ParentNodeIndex, 1)
                        If (cbStatusChild = C1.Win.C1FlexGrid.CheckEnum.Unchecked) Then
                            AllChildSelected = False
                            Exit While
                        End If
                    End If
                    ParentNodeIndex = ParentNodeIndex + 1
                    If (ParentNodeIndex = C1ClinicalChart.Rows.Count) Then
                        Exit While
                    End If
                End While

                If (sOriginalNodeType = "Claim Details" Or sOriginalNodeType = "Payment Details" Or sOriginalNodeType = "Claim History") Then
                    If (AllChildSelected) Then
                        C1ClinicalChart.SetCellCheck(C1ClinicalChart.Rows(RowIndex).Node.Parent.Row.Index, 1, C1.Win.C1FlexGrid.CheckEnum.Checked)
                    Else
                        C1ClinicalChart.SetCellCheck(C1ClinicalChart.Rows(RowIndex).Node.Parent.Row.Index, 1, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                    End If
                    If System.Convert.ToString(C1ClinicalChart.Rows(RowIndex).Node.Parent.Row.UserData) = "Claim" Then
                        sNodeType = "Claim"
                        ParentNodeIndex = C1ClinicalChart.Rows(RowIndex).Node.Parent.Row.Index
                    End If
                Else
                    If (AllChildSelected) Then
                        C1ClinicalChart.SetCellCheck(C1ClinicalChart.Rows(RowIndex).Node.Parent.Row.Node.Parent.Row.Index, 1, C1.Win.C1FlexGrid.CheckEnum.Checked)
                    Else
                        C1ClinicalChart.SetCellCheck(C1ClinicalChart.Rows(RowIndex).Node.Parent.Row.Node.Parent.Row.Index, 1, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                    End If
                    If System.Convert.ToString(C1ClinicalChart.Rows(RowIndex).Node.Parent.Row.Node.Parent.Row.UserData) = "Claim" Then
                        sNodeType = "Claim"
                        ParentNodeIndex = C1ClinicalChart.Rows(RowIndex).Node.Parent.Row.Node.Parent.Row.Index
                    End If
                End If
            End If

            'Claim Number Node
            If (sNodeType = "Claim") Then
                If sOriginalNodeType = "Claim" Then
                    While (System.Convert.ToString(C1ClinicalChart.Rows(NextRowIndex).UserData) <> "Claim" And
                           C1ClinicalChart.Rows(NextRowIndex)(4).ToString.Trim() <> "0")
                        C1ClinicalChart.SetCellCheck(NextRowIndex, 1, cbStatus)
                        NextRowIndex = NextRowIndex + 1
                        If (NextRowIndex = C1ClinicalChart.Rows.Count) Then
                            Exit While
                        End If
                    End While
                End If
                If (sOriginalNodeType <> "Claim") Then
                    ParentNodeIndex = C1ClinicalChart.Rows(ParentNodeIndex).Node.Parent.Row.Index + 1
                End If

                While C1ClinicalChart.Rows(ParentNodeIndex)(4).ToString.Trim() <> "0"
                    If (System.Convert.ToString(C1ClinicalChart.Rows(ParentNodeIndex).UserData) = "Claim") Then
                        cbStatusChild = C1ClinicalChart.GetCellCheck(ParentNodeIndex, 1)
                        If (cbStatusChild = C1.Win.C1FlexGrid.CheckEnum.Unchecked) Then
                            AllChildSelected = False
                            Exit While
                        End If
                    End If
                    ParentNodeIndex = ParentNodeIndex + 1
                    If (ParentNodeIndex = C1ClinicalChart.Rows.Count) Then
                        Exit While
                    End If
                End While
                If (sOriginalNodeType = "Claim") Then
                    If (AllChildSelected) Then
                        C1ClinicalChart.SetCellCheck(C1ClinicalChart.Rows(RowIndex).Node.Parent.Row.Index, 1, C1.Win.C1FlexGrid.CheckEnum.Checked)
                    Else
                        C1ClinicalChart.SetCellCheck(C1ClinicalChart.Rows(RowIndex).Node.Parent.Row.Index, 1, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                    End If
                ElseIf (sOriginalNodeType = "Individual Claim" Or sOriginalNodeType = "Individual Payment" Or sOriginalNodeType = "Individual History") Then
                    If (AllChildSelected) Then
                        C1ClinicalChart.SetCellCheck(C1ClinicalChart.Rows(RowIndex).Node.Parent.Row.Node.Parent.Row.Node.Parent.Row.Index, 1, C1.Win.C1FlexGrid.CheckEnum.Checked)
                    Else
                        C1ClinicalChart.SetCellCheck(C1ClinicalChart.Rows(RowIndex).Node.Parent.Row.Node.Parent.Row.Node.Parent.Row.Index, 1, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                    End If
                Else
                    If (AllChildSelected) Then
                        C1ClinicalChart.SetCellCheck(C1ClinicalChart.Rows(RowIndex).Node.Parent.Row.Node.Parent.Row.Index, 1, C1.Win.C1FlexGrid.CheckEnum.Checked)
                    Else
                        C1ClinicalChart.SetCellCheck(C1ClinicalChart.Rows(RowIndex).Node.Parent.Row.Node.Parent.Row.Index, 1, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                    End If

                End If

            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.ClaimDetailsReport, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub


    Private Sub C1ClinicalChart_CellChecked(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1ClinicalChart.CellChecked
        If (isloading = True) Then
            Exit Sub
        End If

        Dim sFlg As String = C1ClinicalChart.Rows(e.Row)(4).ToString.Trim()

        If (C1ClinicalChart.Rows(e.Row)(4).ToString.Trim() = "1") Then
            Exit Sub
        End If

        If System.Convert.ToString(C1ClinicalChart.Rows(e.Row).UserData).Contains("Claim") Or
           System.Convert.ToString(C1ClinicalChart.Rows(e.Row).UserData).Contains("Payment") Or
           System.Convert.ToString(C1ClinicalChart.Rows(e.Row).UserData).Contains("Individual History") Then
            ClaimNodeCheck(e.Row)
            ' ClaimNodeCheck_Recursive(e.Row)
            Exit Sub
        End If

        If (C1ClinicalChart.Rows(e.Row)(4).ToString.Trim() = "0") Or (C1ClinicalChart.Rows(e.Row)(4).ToString.Trim() = "4") Then
            Dim RowIndex As Integer = e.Row + 1
            Dim cbStatus As C1.Win.C1FlexGrid.CheckEnum
            cbStatus = C1ClinicalChart.GetCellCheck(e.Row, 1)

            If (e.Row = C1ClinicalChart.Rows.Count - 1) Then
                Exit Sub
            End If
            While (C1ClinicalChart.Rows(RowIndex)(4).ToString.Trim() <> "0")
                If (C1ClinicalChart.Rows(RowIndex)(4).ToString.Trim() = "4") Or (C1ClinicalChart.Rows(RowIndex)(4).ToString.Trim() = "1") Then

                Else
                    C1ClinicalChart.SetCellCheck(RowIndex, 1, cbStatus)
                End If
                If (RowIndex = C1ClinicalChart.Rows.Count - 1) Then
                    Exit Sub
                End If
                RowIndex = RowIndex + 1
            End While

        ElseIf (sFlg.Contains("R") Or sFlg.Contains("L")) Then
            Dim sNewflg As String
            If sFlg.Contains("L") Then
                sNewflg = String.Concat("T", sFlg.Remove(0, 1))
            Else
                sNewflg = String.Concat("C", sFlg.Remove(0, 1))
            End If

            Dim RowIndex As Integer = e.Row + 1
            Dim cbStatus As C1.Win.C1FlexGrid.CheckEnum
            Dim Pfound As Boolean = False
            Dim rIndex As Integer = e.Row
            ' Dim iRootnode As Integer
            cbStatus = C1ClinicalChart.GetCellCheck(e.Row, 1)
            ''

            ''
            If cbStatus = CheckEnum.Unchecked Then
                If e.Row > 1 Then
                    While (Pfound <> True)
                        If (C1ClinicalChart.Rows(rIndex)(4).ToString.Trim() = "0") Then

                            Pfound = True
                            If System.Convert.ToString(C1ClinicalChart.GetData(rIndex, 1)) = "Orders" Then
                                C1ClinicalChart.SetCellCheck(rIndex, 1, cbStatus)
                                If System.Convert.ToString(C1ClinicalChart.GetData(e.Row, 1)).Contains("ORD") Then
                                    If cbStatus = CheckEnum.Unchecked Then
                                        Exit Sub
                                    End If
                                End If
                            End If
                        End If
                        rIndex = rIndex - 1
                    End While

                End If
            End If


            If (e.Row = C1ClinicalChart.Rows.Count - 1) Then
                Exit Sub
            End If


            While (C1ClinicalChart.Rows(RowIndex)(4).ToString.Trim() <> "0")

                If (C1ClinicalChart.Rows(RowIndex)(4).ToString.Trim() = sNewflg Or C1ClinicalChart.Rows(RowIndex)(4).ToString.Trim() = "L12") Then
                    'Else
                    C1ClinicalChart.SetCellCheck(RowIndex, 1, cbStatus)
                End If

                If (RowIndex = C1ClinicalChart.Rows.Count - 1) Then
                    'Exit Sub
                    Exit While
                End If
                RowIndex = RowIndex + 1

            End While

            While (Pfound <> True)
                If (C1ClinicalChart.Rows(rIndex)(4).ToString.Trim() = "0") Then
                    ' C1ClinicalChart.SetCellCheck(rIndex, 1, cbStatus)
                    Pfound = True
                End If
                rIndex = rIndex - 1
            End While
            rIndex = rIndex + 1
            SetParentCellChecked_Unchecked(rIndex, cbStatus)

        Else
            Dim ParentFound As Boolean = False
            Dim rootfound As Boolean = False
            Dim bStatus As Boolean = False
            Dim RowIndex As Integer = e.Row
            Dim RootIndex, ParentIndx As Integer
            Dim cbStatus As C1.Win.C1FlexGrid.CheckEnum
            cbStatus = C1ClinicalChart.GetCellCheck(e.Row, 1)

            While (ParentFound <> True)
                If (C1ClinicalChart.Rows(RowIndex)(4).ToString.Trim() = "0") Then
                    ParentFound = True
                ElseIf (C1ClinicalChart.Rows(RowIndex)(4).ToString.Contains("R") Or C1ClinicalChart.Rows(RowIndex)(4).ToString.Contains("L")) Then
                    If rootfound = False Then
                        RootIndex = RowIndex
                    End If
                    rootfound = True
                End If
                RowIndex = RowIndex - 1
            End While
            RowIndex = RowIndex + 2
            ParentIndx = RowIndex - 1
            If sFlg.Contains("C") Or sFlg.Contains("T") Then
                If cbStatus = CheckEnum.Unchecked Then
                    If sFlg.Contains("T") Then
                        Exit Sub
                    End If
                    While (C1ClinicalChart.Rows(RowIndex)(4).ToString.Trim() <> "0")
                        If (C1ClinicalChart.Rows(RowIndex)(4).ToString.Trim().Contains("R") Or C1ClinicalChart.Rows(RowIndex)(4).ToString.Trim().Contains("L")) Then
                            RowIndex = RowIndex + 1
                            While (C1ClinicalChart.Rows(RowIndex)(4).ToString.Trim() <> "0") Or (C1ClinicalChart.Rows(RowIndex)(4).ToString.Trim().Contains("R")) Or (C1ClinicalChart.Rows(RowIndex)(4).ToString.Trim().Contains("L"))

                                '  If (C1ClinicalChart.GetCellCheck(RowIndex, 1) = CheckEnum.Checked) Then
                                If (C1ClinicalChart.GetCellCheck(RowIndex, 1) = CheckEnum.Checked) AndAlso (sFlg.Remove(0, 1) = C1ClinicalChart.Rows(RowIndex)(4).ToString.Trim().Remove(0, 1)) AndAlso (C1ClinicalChart.Rows(RowIndex)(4).ToString.Trim().Contains("R") = False AndAlso C1ClinicalChart.Rows(RowIndex)(4).ToString.Trim().Contains("L") = False) Then
                                    C1ClinicalChart.SetCellCheck(RootIndex, 1, cbStatus)
                                    C1ClinicalChart.SetCellCheck(ParentIndx, 1, cbStatus)
                                    'If (sFlg.Remove(0, 1) = C1ClinicalChart.Rows(RowIndex)(4).ToString.Trim().Remove(0, 1)) Then
                                    bStatus = True
                                    'End If
                                    'Exit While
                                End If
                                ''bStatus = False
                                RowIndex = RowIndex + 1
                                If RowIndex >= C1ClinicalChart.Rows.Count Then
                                    Exit While
                                End If
                            End While
                            If bStatus = False Then
                                C1ClinicalChart.SetCellCheck(RootIndex, 1, cbStatus)
                            End If
                            ' bStatus = False
                        End If
                        RowIndex = RowIndex + 1
                        If RowIndex >= C1ClinicalChart.Rows.Count Then
                            Exit While
                        End If
                    End While
                    SetParentCellChecked_Unchecked(ParentIndx, cbStatus)
                Else     'For Checked
                    While (C1ClinicalChart.Rows(RowIndex)(4).ToString.Trim() <> "0")
                        ' bchk_Uncheck = False

                        If (C1ClinicalChart.Rows(RowIndex)(4).ToString.Trim().Contains("R") Or C1ClinicalChart.Rows(RowIndex)(4).ToString.Trim().Contains("L")) Then
                            RowIndex = RowIndex + 1
                            While (C1ClinicalChart.Rows(RowIndex)(4).ToString.Trim() <> "0") Or (C1ClinicalChart.Rows(RowIndex)(4).ToString.Trim().Contains("R")) Or (C1ClinicalChart.Rows(RowIndex)(4).ToString.Trim().Contains("L"))
                                'bStatus = False
                                If (C1ClinicalChart.GetCellCheck(RowIndex, 1) = CheckEnum.Unchecked) AndAlso (sFlg.Remove(0, 1) = C1ClinicalChart.Rows(RowIndex)(4).ToString.Trim().Remove(0, 1)) AndAlso (C1ClinicalChart.Rows(RowIndex)(4).ToString.Trim().Contains("R") = False AndAlso C1ClinicalChart.Rows(RowIndex)(4).ToString.Trim().Contains("L") = False) Then
                                    If sFlg.Contains("T") Then
                                        Exit Sub
                                    End If
                                    C1ClinicalChart.SetCellCheck(RootIndex, 1, CheckEnum.Unchecked)
                                    'If (sFlg.Remove(0, 1) = C1ClinicalChart.Rows(RowIndex)(4).ToString.Trim().Remove(0, 1)) Then
                                    bStatus = True
                                    
                                End If
                                ''bStatus = False
                                RowIndex = RowIndex + 1
                                If RowIndex >= C1ClinicalChart.Rows.Count Then
                                    Exit While
                                End If
                            End While
                            If bStatus = False Then
                                C1ClinicalChart.SetCellCheck(RootIndex, 1, cbStatus)
                            End If
                        End If
                        RowIndex = RowIndex + 1
                        If RowIndex >= C1ClinicalChart.Rows.Count Then
                            Exit While
                        End If
                    End While
                    
                    SetParentCellChecked_Unchecked(ParentIndx, cbStatus)
                End If
            Else
                SetParentCellChecked_Unchecked(ParentIndx, cbStatus)
            End If
        End If
    End Sub

    Private Function SetParentCellChecked_Unchecked(ByVal cRowIndex As Integer, ByVal cbStatus As C1.Win.C1FlexGrid.CheckEnum)
        Dim RowIndex As Integer = cRowIndex
        Dim bCheck As Boolean = False
        RowIndex = RowIndex + 1
        If cbStatus = CheckEnum.Checked Then
            While (C1ClinicalChart.Rows(RowIndex)(4).ToString.Trim() <> "0")
                If C1ClinicalChart.Rows(RowIndex)(4).ToString.Trim() = "1" Then
                    Exit While
                End If
                If (C1ClinicalChart.GetCellCheck(RowIndex, 1) = CheckEnum.Unchecked) AndAlso (C1ClinicalChart.Rows(RowIndex)(4).ToString.Trim() <> "4") Then
                    C1ClinicalChart.SetCellCheck(cRowIndex, 1, CheckEnum.Unchecked)
                    SetParentCellChecked_Unchecked = Nothing
                    Exit Function
                End If
                bCheck = True
                RowIndex = RowIndex + 1
                If RowIndex >= C1ClinicalChart.Rows.Count Then
                    Exit While
                End If
            End While
            If bCheck = True Then
                C1ClinicalChart.SetCellCheck(cRowIndex, 1, cbStatus)
            End If
        Else
            While (C1ClinicalChart.Rows(RowIndex)(4).ToString.Trim() <> "0") AndAlso (C1ClinicalChart.Rows(RowIndex)(4).ToString.Trim() <> "4")
                If C1ClinicalChart.Rows(RowIndex)(4).ToString.Trim() = "1" Then
                    Exit While
                End If
                If (C1ClinicalChart.GetCellCheck(RowIndex, 1) = CheckEnum.Checked) Then
                    C1ClinicalChart.SetCellCheck(cRowIndex, 1, CheckEnum.Unchecked)
                    SetParentCellChecked_Unchecked = Nothing
                    Exit Function
                End If
                bCheck = True
                RowIndex = RowIndex + 1
                If RowIndex >= C1ClinicalChart.Rows.Count Then
                    Exit While
                End If
            End While
            If bCheck = True Then
                C1ClinicalChart.SetCellCheck(cRowIndex, 1, cbStatus)
            End If
        End If
        SetParentCellChecked_Unchecked = Nothing
    End Function

    Public Function Fill_ClinicalChartInfoDS(ByVal nPatientId As Long, ByVal dtFromdate As DateTime, ByVal dtTodate As DateTime, Optional dtDos As String = "") As DataSet
        Dim dt As New DataSet()
        Try
            Dim objCon As New SqlConnection(GetConnectionString())
            Dim cmd As New SqlCommand("BL_ClaimClinicalInfo", objCon)
            Dim da As New SqlDataAdapter(cmd)
            da.SelectCommand = cmd

            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PatientID", nPatientId)
            cmd.Parameters.AddWithValue("@dtStartDate", dtFromdate)
            cmd.Parameters.AddWithValue("@dtEndDate", dtTodate)
            cmd.Parameters.AddWithValue("@sDOS", dtDos)
            cmd.CommandTimeout = 0


            da.Fill(dt)

            objCon.Dispose()
            objCon = Nothing
            da.Dispose()
            da = Nothing

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            Return dt
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return Nothing
        End Try
    End Function

    Public Function Fill_ClinicalChartInfo(ByVal nPatientId As Long, ByVal flag As Int16, ByVal dtFromdate As Date, ByVal dtTodate As Date, Optional dtDos As String = "") As DataTable
        Dim dt As DataTable = Nothing
        Try
            Dim objCon As New SqlConnection
            objCon.ConnectionString = GetConnectionString()

            Dim cmd As New SqlCommand("gsp_GetClinicalChartDetails", objCon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PatientID", nPatientId)
            cmd.Parameters.AddWithValue("@sFromDate", dtFromdate)
            cmd.Parameters.AddWithValue("@sToDate", dtTodate)
            cmd.Parameters.AddWithValue("@sFlag", flag)
            cmd.Parameters.AddWithValue("@sDOS", dtDos)
            If (flag = 5) Then
                cmd.Parameters.AddWithValue("@ServerName", gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsServerName)
                cmd.Parameters.AddWithValue("@DatabaseName", gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsDatabaseName)
            End If
            cmd.CommandTimeout = 0
            Dim da As SqlDataAdapter

            objCon.Open()
            da = New SqlDataAdapter
            da.SelectCommand = cmd
            dt = New DataTable
            da.Fill(dt)
            objCon.Close()
            objCon.Dispose()
            objCon = Nothing
            da.Dispose()
            da = Nothing

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            Return dt
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            If (flag = 5) Then
                MessageBox.Show("Error while establishing connection with the DMS server.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return Nothing
        End Try
        ''dt Contains Following Columns
        ' ''dtMedicationDate, sMedication ,sDosage ,sRoute, sFrequency, sDuration, sAmount, sStatus, dtStartDate, dtEndDate, UserID , UserName 
    End Function
    Private Sub UpdateManualOrderStatus(ByVal intOrderStatus As Int16)
        Dim oDBLayer As gloDatabaseLayer.DBLayer = Nothing
        Dim oDBparams As gloDatabaseLayer.DBParameters = Nothing

        Try
            'Create parameters for SP to update the Manual Order status.
            oDBparams = New gloDatabaseLayer.DBParameters()
            oDBparams.Add("@intOrderStatus", intOrderStatus, ParameterDirection.Input, SqlDbType.Int)
            oDBparams.Add("@OrderList", OrderIDList, ParameterDirection.Input, SqlDbType.VarChar)

            'Execution for the stored procedures
            oDBLayer = New gloDatabaseLayer.DBLayer(GetConnectionString())
            oDBLayer.Connect(False)
            oDBLayer.Execute("gsp_UpdateClinicalChartOrderStatus", oDBparams)

            oDBLayer.Disconnect()
        Catch ex As Exception
            'Log if any exception;

            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ex = Nothing
        Finally
            'Cleanup for internal objects utilized in the method.

            If oDBLayer IsNot Nothing Then
                oDBLayer.Dispose()
                oDBLayer = Nothing
            End If

            If oDBparams IsNot Nothing Then

                oDBparams.Clear()
                oDBparams.Dispose()

                oDBparams = Nothing

            End If
        End Try

    End Sub

    Private Function GetExportDataTable() As DataTable

        Dim dtMain As New DataTable

        With dtMain
            .Columns.Add("nFaxID", Type.GetType("System.Int64"))
            .Columns.Add("FaxCoverPageBinary", Type.GetType("System.String"))
            .Columns.Add("ExportPath", Type.GetType("System.String"))
            .Columns.Add("PrinterSettingsID", Type.GetType("System.Int64"))
            .Columns.Add("PrintData", Type.GetType("System.Boolean"))
            .Columns.Add("IsClaimSelected", Type.GetType("System.Boolean"))
        End With

        Return dtMain

    End Function
    Private Sub ToolStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked
        'Callprint(_PatientID)
        Dim oclsClinicalChartDBLayer As New gloUIControlLibrary.Classes.ClinicalChartQueue.clsClinicalChartDBLayer(GetConnectionString())

        Select Case e.ClickedItem.Tag
            Case "Show"
                ArrangeGridItems()
            Case "Export"
                If Not (CheckSSRSConfiguration()) OrElse Not isValidDates() Then
                    Exit Sub
                End If
                If (IsHaveDocumenttoExport()) Then
                    Dim strFiletoExport As String = ""
                    Dim dtExport As DataTable

                    dtExport = GetExportDataTable()
                    strFiletoExport = ShowExportDialog()
                    dtExport.Rows.Add(dtExport.NewRow)
                    dtExport.Rows(0)("ExportPath") = strFiletoExport
                    If strFiletoExport = "" Then
                        lblStatus.Visible = False
                        pnlProgress.Visible = False
                        Panel2.Height = 60
                        pnlProgress.Dock = DockStyle.None
                    Else
                        Queue(gloUIControlLibrary.Classes.ClinicalChartQueue.enumBackgroundProcessType.Export, dtExport)
                    End If


                Else
                    MessageBox.Show("Elements of the patient’s chart are not selected or selected elements do not have data.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                    '    If (strFiletoExport = "WORDEXCEPTION" OrElse strFiletoExport = "ERROR") Then
                    '        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Reports, gloAuditTrail.ActivityCategory.ClinicalChart, gloAuditTrail.ActivityType.Export, "Exporting using clinical chart is aborted.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    '        oclsClinicalChartDBLayer.InsertQueueNotes(_nQueueID, "Exporting using clinical chart is aborted.", _nAssociatedContactID, UserName, UserID, gloUIControlLibrary.Classes.ClinicalChartQueue.enumPrintStatus.ErrorProcessed, gloUIControlLibrary.Classes.ClinicalChartQueue.enumPrintStatus.ErrorProcessed)
                    '        Exit Sub
                    '    End If

                    '    If strFiletoExport <> "" Then
                    '        oclsClinicalChartDBLayer.InsertQueueNotes(_nQueueID, "Exported using clinical chart.", _nAssociatedContactID, UserName, UserID, gloUIControlLibrary.Classes.ClinicalChartQueue.enumPrintStatus.Processed, gloUIControlLibrary.Classes.ClinicalChartQueue.enumPrintStatus.Processed)
                    '    End If
                    '    'gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Export, "Patient data exported using clinical chart.", gloAuditTrail.ActivityOutCome.Success)
                    '    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Reports, gloAuditTrail.ActivityCategory.ClinicalChart, gloAuditTrail.ActivityType.Export, AuditLogModuleList.ToString() & " exported using clinical chart.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    'Else
                    '    MessageBox.Show("Elements of the patient’s chart are not selected or selected elements do not have data.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '    Exit Sub
                    'End If
            Case "SelectAll"
                SelectAll(True)
            Case "DeSelectAll"
                SelectAll(False)
            Case "Close"
                Me.Close()
            Case "Queue"
                If Not isValidDates() Then
                    Exit Sub
                Else
                    ''Added by Mayuri: 20160809-enum for print,fax
                    Me.Queue(gloUIControlLibrary.Classes.ClinicalChartQueue.enumBackgroundProcessType.Queue)

                End If


            Case "Print"
                'PrintOld()

                If Not (CheckSSRSConfiguration()) OrElse Not isValidDates() Then
                    Exit Sub
                End If
               
                If (IsHaveDocumenttoExport()) Then
                    Dim strFiletoprint As String = ""
                    isClaimSelected = False
                    If CheckPrintRestriction() = True Then
                        Exit Sub
                    End If
                    ''Send documents to local printer if Local (TS) print enabled
                    If gblnEnableLocalPrinter = True Then
                        If gloGlobal.gloTSPrint.isMapped() Then
                            ''Added by Mayuri: 20160809-enum for print,fax
                            Me.Queue(gloUIControlLibrary.Classes.ClinicalChartQueue.enumBackgroundProcessType.TsPrint)
                            Exit Sub
                        Else
                            Dim s As DialogResult = MessageBox.Show("Unable to find mapped drive. Please check whether gloLDSSniffer Service is running. Looks like you have not enabled mapping while connecting to RDP." + Environment.NewLine + Environment.NewLine + "Instead can RDP printer be used now?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            If s = Windows.Forms.DialogResult.No Then
                                Exit Sub
                            End If
                        End If
                    End If

                    Dim dtPrint As DataTable
                    Dim intPrinterSettingsID As Long

                    dtPrint = GetExportDataTable()


                    intPrinterSettingsID = ShowPrinterDialog()

                    ''Resolved issues #99474-(8071)gloEMR : Clinical Chart :  Application should not print data when user click on cancel button.
                    If intPrinterSettingsID <> -1 Then
                        dtPrint.Rows.Add(dtPrint.NewRow)
                        dtPrint.Rows(0)("PrinterSettingsID") = intPrinterSettingsID
                        dtPrint.Rows(0)("PrintData") = rbPrintData.Checked
                        dtPrint.Rows(0)("IsClaimSelected") = isClaimSelected

                        Queue(gloUIControlLibrary.Classes.ClinicalChartQueue.enumBackgroundProcessType.Print, dtPrint)

                        'strFiletoprint = ExportClinicalCharts(1)
                        'If (strFiletoprint = "WORDEXCEPTION" OrElse strFiletoprint = "ERROR") Then
                        '    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Reports, gloAuditTrail.ActivityCategory.ClinicalChart, gloAuditTrail.ActivityType.Export, "Printing using clinical chart is aborted.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                        '    oclsClinicalChartDBLayer.InsertQueueNotes(_nQueueID, "Printing using clinical chart is aborted.", _nAssociatedContactID, UserName, UserID, gloUIControlLibrary.Classes.ClinicalChartQueue.enumPrintStatus.ErrorProcessed, gloUIControlLibrary.Classes.ClinicalChartQueue.enumPrintStatus.ErrorProcessed)
                        '    Exit Sub
                        'End If
                        'If (strFiletoprint.Trim <> "") Then
                        '    'START: Implemented new dialog from 8060 
                        '    Dim bIsPrintinSuccessfull As Boolean = False
                        '    bIsPrintinSuccessfull = PrintPDF(strFiletoprint:=strFiletoprint)

                        '    If bIsPrintinSuccessfull Then

                        '        If OrderIDList <> "" Then
                        '            UpdateManualOrderStatus(1005)
                        '        End If

                        '        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Reports, gloAuditTrail.ActivityCategory.ClinicalChart, gloAuditTrail.ActivityType.Print, AuditLogModuleList.ToString() & " printed using clinical chart.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        '        oclsClinicalChartDBLayer.InsertQueueNotes(_nQueueID, "Printed using clinical chart.", _nAssociatedContactID, UserName, UserID, gloUIControlLibrary.Classes.ClinicalChartQueue.enumPrintStatus.Processed, gloUIControlLibrary.Classes.ClinicalChartQueue.enumPrintStatus.Processed)
                        '        Me.Focus()
                        '    End If

                        '    ''END : Implemented new dialog from 8060

                        '    Panel2.Height = 60
                        '    pnlProgress.Dock = DockStyle.None
                        '    Me.Cursor = Cursors.Default

                        'Else
                        '    MessageBox.Show("Can not create document for printing.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        '    oclsClinicalChartDBLayer.InsertQueueNotes(_nQueueID, "Can not create document for printing.", _nAssociatedContactID, UserName, UserID, gloUIControlLibrary.Classes.ClinicalChartQueue.enumPrintStatus.ErrorProcessed, gloUIControlLibrary.Classes.ClinicalChartQueue.enumPrintStatus.ErrorProcessed)

                    End If

                Else
                    MessageBox.Show("Elements of the patient’s chart are not selected or selected elements do not have data.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

                End If

            Case "FAX"
                Dim dtDetails As DataTable = Nothing
                If Not (CheckSSRSConfiguration()) Then
                    Exit Sub
                End If
                If Not isValidDates() Then
                    Exit Sub
                End If
                Dim strFiletofax As String = String.Empty
                'If strFiletoprint = Nothing Then
                If (IsHaveDocumenttoExport()) Then

                    'strFiletofax = ExportClinicalCharts(2)
                    'If (strFiletofax = "WORDEXCEPTION" OrElse strFiletofax = "ERROR") Then
                    '    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Reports, gloAuditTrail.ActivityCategory.ClinicalChart, gloAuditTrail.ActivityType.Export, "Fax using clinical chart is aborted.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                    '    oclsClinicalChartDBLayer.InsertQueueNotes(_nQueueID, "Fax using clinical chart is aborted.", _nAssociatedContactID, UserName, UserID, gloUIControlLibrary.Classes.ClinicalChartQueue.enumPrintStatus.ErrorProcessed, gloUIControlLibrary.Classes.ClinicalChartQueue.enumPrintStatus.ErrorProcessed)
                    '    Panel2.Height = 60
                    '    pnlProgress.Dock = DockStyle.None
                    '    Me.Cursor = Cursors.Default
                    '    Exit Sub
                    'End If
                    'If strFiletofax.Trim = "" Then
                    '    Me.Cursor = Cursors.Default
                    '    MessageBox.Show("Can not create document for fax.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '    oclsClinicalChartDBLayer.InsertQueueNotes(_nQueueID, "Can not create document for fax.", _nAssociatedContactID, UserName, UserID, gloUIControlLibrary.Classes.ClinicalChartQueue.enumPrintStatus.ErrorProcessed, gloUIControlLibrary.Classes.ClinicalChartQueue.enumPrintStatus.ErrorProcessed)

                    '    Panel2.Height = 60
                    '    pnlProgress.Dock = DockStyle.None

                    '    Exit Sub
                    'End If
                Else
                    Me.Cursor = Cursors.Default
                    MessageBox.Show("Elements of the patient’s chart are not selected or selected elements do not have data.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Panel2.Height = 60
                    pnlProgress.Dock = DockStyle.None

                    Exit Sub
                End If

                'End If
                Try
                    Dim _IsFaxsavenclose As Boolean = False

                    Dim gloEDocFAx As New gloEDocumentV3.gloEDocV3Management
                    gloEDocFAx.dtFaxDetails = GetExportDataTable()
                    gloEDocumentV3.gloEDocV3Admin.gstrFaxOutputDirectory = gstrFAXOutputDirectory
                    gloEDocumentV3.gloEDocV3Admin.blnIsInternetFaxEnabled = gblnInternetFax
                    gloEDocumentV3.gloEDocV3Admin.gblnAddFaxCoverpage = gblnFAXCoverPage
                    gloEDocumentV3.gloEDocV3Admin.gblnAssociatedProvider = gblnAssociatedProviderSignature
                    'Commented BY Rahul Patel on 26-10-2010
                    'gloEDocumentV3.gloEDocV3Admin.Connect(GetConnectionString(), gDMSV3TempPath, gnLoginID, gClinicID, Application.StartupPath)
                    'Added by Rahul Patel on 26-10-2010
                    'For DMS Hybrid Database change
                    gloEDocumentV3.gloEDocV3Admin.Connect(GetConnectionString(), GetDMSConnectionString(), gDMSV3TempPath, gnLoginID, gClinicID, Application.StartupPath)
                    gloEDocumentV3.gloEDocV3Admin.SetUserName(gstrLoginName)
                    gloEDocumentV3.gloEDocV3Admin.GetDefaultPrinterDialog(gblnUseDefaultPrinter)

                    _IsFaxsavenclose = gloEDocFAx.ShowDMSFax(_PatientID, Forms.enmFAXDocType.OtherPDFDoc, strFiletofax)
                    If _IsFaxsavenclose = True Then
                        If gloEDocFAx.DocumentFaxed Then
                            If Me.lstClaimMasterIDs.Any() Then
                                Me.InsertNotesInClaims(lstClaimMasterIDs)
                            End If
                            oclsClinicalChartDBLayer.InsertQueueNotes(_nQueueID, "Faxed using clinical chart.", _nAssociatedContactID, UserName, UserID, gloUIControlLibrary.Classes.ClinicalChartQueue.enumPrintStatus.Processed, gloUIControlLibrary.Classes.ClinicalChartQueue.enumPrintStatus.Processed)

                        End If
                        If OrderIDList <> "" Then
                            UpdateManualOrderStatus(1005)
                        End If

                        dtDetails = gloEDocFAx.dtFaxDetails 'Aniket To Remove This

                        If Not IsNothing(dtDetails) Then
                            If dtDetails.Rows.Count > 0 Then

                                Me.Queue(gloUIControlLibrary.Classes.ClinicalChartQueue.enumBackgroundProcessType.FAX, dtDetails)
                            End If
                        End If
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Reports, gloAuditTrail.ActivityCategory.ClinicalChart, gloAuditTrail.ActivityType.Fax, AuditLogModuleList.ToString() & " Faxed Using Clinical Chart.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                    End If
                    'gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Export, "Patient data faxed using clinical chart.", gloAuditTrail.ActivityOutCome.Success)
                    'SendFax(strFiletoprint)
                    'Dim ofx As New 
                    gloEDocFAx.Dispose()
                    gloEDocFAx = Nothing
                    Panel2.Height = 60
                    pnlProgress.Dock = DockStyle.None
                    Me.Cursor = Cursors.Default
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
                End Try
            Case "SendSS"

                ShowSecureMessage()

                '    MessageBox.Show("File Printed successfully", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Else
                '    MessageBox.Show("File Not Printed successfully", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                'End If
        End Select
    End Sub

    Private Sub ShowSecureMessage()

        Dim oclsClinicalChartDBLayer As New gloUIControlLibrary.Classes.ClinicalChartQueue.clsClinicalChartDBLayer(GetConnectionString())

        If strProviderDirectAddress <> "" OrElse gloSurescriptSecureMessage.SecureMessageProperties.ListUserProviderAssociation IsNot Nothing Then
            If Not isValidDates() Then
                Exit Sub
            End If
            If (IsHaveDocumenttoExport()) Then
                Dim strFiletoprint As String = ""
                strFiletoprint = ExportClinicalCharts(3)
                If (strFiletoprint = "WORDEXCEPTION" OrElse strFiletoprint = "ERROR") Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Reports, gloAuditTrail.ActivityCategory.ClinicalChart, gloAuditTrail.ActivityType.Export, "Printing using clinical chart is aborted.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                    oclsClinicalChartDBLayer.InsertQueueNotes(_nQueueID, "Clinical Chart send secure message aborted.", _nAssociatedContactID, UserName, UserID, gloUIControlLibrary.Classes.ClinicalChartQueue.enumPrintStatus.ErrorProcessed, gloUIControlLibrary.Classes.ClinicalChartQueue.enumPrintStatus.ErrorProcessed)

                    Exit Sub
                End If
                If (strFiletoprint.Trim <> "") Then


                    ''Read Secure Messages settings and call Inbox form
                    If System.IO.File.Exists(strFiletoprint) Then
                        Dim sError As String = gloSurescriptSecureMessage.SecureMessage.ValidateZipCode(_PatientID)
                        If sError <> "" Then
                            MessageBox.Show(sError, gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
                            oclsClinicalChartDBLayer.InsertQueueNotes(_nQueueID, sError, _nAssociatedContactID, UserName, UserID, gloUIControlLibrary.Classes.ClinicalChartQueue.enumPrintStatus.ErrorProcessed, gloUIControlLibrary.Classes.ClinicalChartQueue.enumPrintStatus.ErrorProcessed)

                            Return
                        Else
                            Dim ofrmSendNewMail As New InBox.NewMail(_PatientID, strFiletoprint)
                            AddHandler ofrmSendNewMail.EvntGenerateCDA, AddressOf Raise_GenerateCDAFromClinicalChart

                            If gloSurescriptSecureMessage.SecureMessageProperties.ListUserProviderAssociation IsNot Nothing Then
                                gloSurescriptSecureMessage.SecureMessage.SetPreferredProvider(gloEMR.gnPatientProviderID)
                                ofrmSendNewMail.ListOfProviders = gloSurescriptSecureMessage.SecureMessageProperties.ListUserProviderAssociation
                            End If
                            ofrmSendNewMail.ShowInTaskbar = True
                            ofrmSendNewMail.ShowDialog()
                            'ofrmInbox.Dispose()
                            RemoveHandler ofrmSendNewMail.EvntGenerateCDA, AddressOf Raise_GenerateCDAFromClinicalChart

                            If ofrmSendNewMail.SendButtonClicked AndAlso Me.lstClaimMasterIDs.Any() Then
                                Me.InsertNotesInClaims(lstClaimMasterIDs)
                                oclsClinicalChartDBLayer.InsertQueueNotes(_nQueueID, "Clinical Chart secure message sent.", _nAssociatedContactID, UserName, UserID, gloUIControlLibrary.Classes.ClinicalChartQueue.enumPrintStatus.Processed, gloUIControlLibrary.Classes.ClinicalChartQueue.enumPrintStatus.Processed)

                            End If

                            ofrmSendNewMail.Close()
                            ofrmSendNewMail = Nothing
                        End If

                    Else
                        MessageBox.Show("Error While generating attachment. Please try again", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        oclsClinicalChartDBLayer.InsertQueueNotes(_nQueueID, "Error While generating attachment. Please try again", _nAssociatedContactID, UserName, UserID, gloUIControlLibrary.Classes.ClinicalChartQueue.enumPrintStatus.ErrorProcessed, gloUIControlLibrary.Classes.ClinicalChartQueue.enumPrintStatus.ErrorProcessed)

                    End If
                Else
                    MessageBox.Show("Can not create document for sending.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    oclsClinicalChartDBLayer.InsertQueueNotes(_nQueueID, "Can not create document for sending.", _nAssociatedContactID, UserName, UserID, gloUIControlLibrary.Classes.ClinicalChartQueue.enumPrintStatus.ErrorProcessed, gloUIControlLibrary.Classes.ClinicalChartQueue.enumPrintStatus.ErrorProcessed)

                End If

            Else
                MessageBox.Show("Elements of the patient’s chart are not selected or selected elements do not have data.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If
        Else
            MessageBox.Show(gstrDirectWarningMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            oclsClinicalChartDBLayer.InsertQueueNotes(_nQueueID, "Direct Address Not Set To Login Provider. Please Set Direct Address From gloEMR Admin.", _nAssociatedContactID, UserName, UserID, gloUIControlLibrary.Classes.ClinicalChartQueue.enumPrintStatus.ErrorProcessed, gloUIControlLibrary.Classes.ClinicalChartQueue.enumPrintStatus.ErrorProcessed)

        End If
    End Sub

    Private Function ShowPrinterDialog() As Long

        Dim intPrinterSettingsID As Long

        Using oDialog As gloPrintDialog.gloPrintDialog = New gloPrintDialog.gloPrintDialog()
            oDialog.ConnectionString = GetConnectionString()
            oDialog.ShowPrinterProfileDialog = True
            oDialog.TopMost = True
            oDialog.AllowSomePages = False
            oDialog.ModuleName = "PrintClinicalCharts"
            oDialog.RegistryModuleName = "ClinicalCharts"

            If Not IsNothing(oDialog) Then



                oDialog.PrinterSettings = printDocument1.PrinterSettings
                oDialog.PrinterSettings.Copies = 1



                If (oDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
                    printDocument1.PrinterSettings = oDialog.PrinterSettings

                    intPrinterSettingsID = oDialog.PrinterSettingsDetailsID

                    Dim footer As gloPrintDialog.gloPrintProgressController.FooterInfo = New gloPrintDialog.gloPrintProgressController.FooterInfo
                    Dim footerList As List(Of gloPrintDialog.gloPrintProgressController.FooterInfo) = New List(Of gloPrintDialog.gloPrintProgressController.FooterInfo)
                    Dim dtPatientTable As DataTable = GetPatientInformation(_PatientID, GetConnectionString())
                    Dim StrPatientName As String = ""

                    StrPatientName = dtPatientTable.Rows(0).Item("Patient Name") + ", DOB : " + dtPatientTable.Rows(0).Item("DOB")

                    footer.FromPage = intFullFilePageCount
                    footer.ToPage = intDMSFilePageCount - 1
                    footer.StartingPage = 1
                    footer.TotalPages = intDMSFilePageCount - intFullFilePageCount
                    footer.CenterText = ""
                    footer.RightText = "[{PAGE()}] of [{TOTAL()}]"
                    footer.LeftText = StrPatientName
                    footerList.Add(footer)

                    If rbPrintData.Checked = True AndAlso isClaimSelected = True Then
                        oDialog.CustomPrinterExtendedSettings.CurrentPageSize = gloPrintDialog.gloExtendedPrinterSettings.PageSize.ActualPageSize
                        oDialog.CustomPrinterExtendedSettings.AdjustActualPageHorizontalPageWidthMargin = 50.9
                        oDialog.CustomPrinterExtendedSettings.AdjustActualPageVerticalPageHeightMargin = 50.9
                        oDialog.CustomPrinterExtendedSettings.AdjustFitToPageHorizontalPageWidthMargin = 0
                        oDialog.CustomPrinterExtendedSettings.AdjustFitToPageVerticalPageHeightMargin = 0
                        oDialog.CustomPrinterExtendedSettings.FooterBottom = 0
                        oDialog.CustomPrinterExtendedSettings.FooterColor = Color.Black
                        oDialog.CustomPrinterExtendedSettings.FooterFont = Nothing
                        oDialog.CustomPrinterExtendedSettings.FooterLeft = 0
                        oDialog.CustomPrinterExtendedSettings.FooterRight = 0
                        oDialog.CustomPrinterExtendedSettings.FooterTop = 0
                        oDialog.CustomPrinterExtendedSettings.HorizontalGutter = 0
                        oDialog.CustomPrinterExtendedSettings.HorizontalOverlap = 0
                        oDialog.CustomPrinterExtendedSettings.IsCustomDPI = False
                        oDialog.CustomPrinterExtendedSettings.PrinterMarginsBottom = 0
                        oDialog.CustomPrinterExtendedSettings.PrinterMarginsLeft = 0
                        oDialog.CustomPrinterExtendedSettings.PrinterMarginsRight = 0
                        oDialog.CustomPrinterExtendedSettings.PrinterMarginsTop = 0
                        oDialog.CustomPrinterExtendedSettings.VerticalGutter = 0
                        oDialog.CustomPrinterExtendedSettings.VerticalOverlap = 0
                    End If
                Else

                    intPrinterSettingsID = -1

                End If

            End If
        End Using

        Return intPrinterSettingsID

    End Function
    'Dim doc As PDFDoc = Nothing
    'Dim _pdfdoc As PDFDoc = Nothing

    'Private Function PrintPDF(ByRef strFiletoprint As String) As Boolean
    '    Dim result As Boolean = False

    '    Try
    '        Using oDialog As gloPrintDialog.gloPrintDialog = New gloPrintDialog.gloPrintDialog()
    '            oDialog.ConnectionString = GetConnectionString()
    '            oDialog.ShowPrinterProfileDialog = True
    '            oDialog.TopMost = True
    '            oDialog.AllowSomePages = True
    '            oDialog.ModuleName = "PrintClinicalCharts"
    '            oDialog.RegistryModuleName = "ClinicalCharts"
    '            If Not IsNothing(oDialog) Then
    '                If IsNothing(_pdfdoc) Then
    '                    _pdfdoc = New PDFDoc(strFiletoprint)
    '                End If
    '                doc = _pdfdoc
    '                doc.Lock()

    '                Dim maxpage As Integer = doc.GetPageCount()

    '                oDialog.PrinterSettings = printDocument1.PrinterSettings
    '                oDialog.PrinterSettings.Copies = 1

    '                oDialog.AllowSomePages = True
    '                oDialog.PrinterSettings.ToPage = maxpage
    '                oDialog.PrinterSettings.FromPage = 1
    '                oDialog.PrinterSettings.MaximumPage = maxpage
    '                oDialog.PrinterSettings.MinimumPage = 1

    '                PrintDialog1.AllowSomePages = True
    '                PrintDialog1.PrinterSettings.ToPage = maxpage
    '                PrintDialog1.PrinterSettings.FromPage = 1
    '                PrintDialog1.PrinterSettings.MaximumPage = maxpage
    '                PrintDialog1.PrinterSettings.MinimumPage = 1


    '                If (oDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
    '                    printDocument1.PrinterSettings = oDialog.PrinterSettings

    '                    Dim footer As gloPrintDialog.gloPrintProgressController.FooterInfo = New gloPrintDialog.gloPrintProgressController.FooterInfo
    '                    Dim footerList As List(Of gloPrintDialog.gloPrintProgressController.FooterInfo) = New List(Of gloPrintDialog.gloPrintProgressController.FooterInfo)
    '                    Dim dtPatientTable As DataTable = GetPatientInformation(_PatientID, GetConnectionString())
    '                    Dim StrPatientName As String = ""
    '                    StrPatientName = dtPatientTable.Rows(0).Item("Patient Name") + ", DOB : " + dtPatientTable.Rows(0).Item("DOB")
    '                    footer.FromPage = intFullFilePageCount
    '                    footer.ToPage = intDMSFilePageCount - 1
    '                    footer.StartingPage = 1
    '                    footer.TotalPages = intDMSFilePageCount - intFullFilePageCount
    '                    footer.CenterText = ""
    '                    footer.RightText = "[{PAGE()}] of [{TOTAL()}]"
    '                    footer.LeftText = StrPatientName
    '                    footerList.Add(footer)

    '                    If rbPrintData.Checked = True AndAlso isClaimSelected = True Then
    '                        oDialog.CustomPrinterExtendedSettings.CurrentPageSize = gloPrintDialog.gloExtendedPrinterSettings.PageSize.ActualPageSize
    '                        oDialog.CustomPrinterExtendedSettings.AdjustActualPageHorizontalPageWidthMargin = 50.9
    '                        oDialog.CustomPrinterExtendedSettings.AdjustActualPageVerticalPageHeightMargin = 50.9
    '                        oDialog.CustomPrinterExtendedSettings.AdjustFitToPageHorizontalPageWidthMargin = 0
    '                        oDialog.CustomPrinterExtendedSettings.AdjustFitToPageVerticalPageHeightMargin = 0
    '                        oDialog.CustomPrinterExtendedSettings.FooterBottom = 0
    '                        oDialog.CustomPrinterExtendedSettings.FooterColor = Color.Black
    '                        oDialog.CustomPrinterExtendedSettings.FooterFont = Nothing
    '                        oDialog.CustomPrinterExtendedSettings.FooterLeft = 0
    '                        oDialog.CustomPrinterExtendedSettings.FooterRight = 0
    '                        oDialog.CustomPrinterExtendedSettings.FooterTop = 0
    '                        oDialog.CustomPrinterExtendedSettings.HorizontalGutter = 0
    '                        oDialog.CustomPrinterExtendedSettings.HorizontalOverlap = 0
    '                        oDialog.CustomPrinterExtendedSettings.IsCustomDPI = False
    '                        oDialog.CustomPrinterExtendedSettings.PrinterMarginsBottom = 0
    '                        oDialog.CustomPrinterExtendedSettings.PrinterMarginsLeft = 0
    '                        oDialog.CustomPrinterExtendedSettings.PrinterMarginsRight = 0
    '                        oDialog.CustomPrinterExtendedSettings.PrinterMarginsTop = 0
    '                        oDialog.CustomPrinterExtendedSettings.VerticalGutter = 0
    '                        oDialog.CustomPrinterExtendedSettings.VerticalOverlap = 0
    '                    End If


    '                    Dim ogloPrintProgressController As gloPrintDialog.gloPrintProgressController = New gloPrintDialog.gloPrintProgressController(doc, doc.GetFileName(), oDialog.PrinterSettings, oDialog.CustomPrinterExtendedSettings, Nothing, footerList, blnIsFromClinicalChart:=True)
    '                    ogloPrintProgressController.ShowProgress(Me)

    '                    'If (oDialog.CustomPrinterExtendedSettings.IsBackGroundPrint) Then
    '                    '    If (oDialog.CustomPrinterExtendedSettings.IsShowProgress) Then
    '                    '        ogloPrintProgressController.Show()
    '                    '    Else
    '                    '        ogloPrintProgressController.Show()
    '                    '    End If
    '                    'Else
    '                    '    ogloPrintProgressController.TopMost = True
    '                    '    ogloPrintProgressController.ShowInTaskbar = False

    '                    '    ogloPrintProgressController.ShowDialog(Me)
    '                    '    If Not IsNothing(ogloPrintProgressController) Then
    '                    '        ogloPrintProgressController.Dispose()
    '                    '    End If
    '                    '    ogloPrintProgressController = Nothing

    '                    'End If
    '                    Me.InsertNotesInClaims(lstClaimMasterIDs)
    '                    result = True
    '                Else
    '                    result = False
    '                End If
    '                doc.Unlock()
    '            Else

    '                Dim _errorNessage As String = "Error in showing print dialog"

    '                If (_errorNessage.Trim() <> "") Then
    '                    Dim _messageString As String = " Date Time :" & DateTime.Now.Ticks.ToString() & Environment.NewLine & " ERROR: " + _errorNessage
    '                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_messageString)
    '                    _messageString = String.Empty
    '                    result = False
    '                End If
    '            End If
    '        End Using
    '        'pdfdraw = New pdftron.PDF.PDFDraw()
    '        'If Not IsNothing(rect) Then
    '        '    rect.Dispose()
    '        '    rect = Nothing
    '        'End If

    '    Catch ex As Exception

    '        Dim _errorNessage As String = ex.ToString()

    '        If (_errorNessage.Trim() <> "") Then
    '            Dim _messageString As String = " Date Time :" & DateTime.Now.Ticks.ToString() & Environment.NewLine & " ERROR: " + _errorNessage
    '            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_messageString)
    '            _messageString = String.Empty

    '        End If

    '        MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        ex = Nothing

    '        result = False


    '    Finally

    '        If Not IsNothing(_pdfdoc) Then
    '            _pdfdoc.Dispose()
    '            _pdfdoc = Nothing
    '        End If

    '        PageArray.Clear()

    '        '' result = True

    '    End Try
    '    Return result
    'End Function

    'Dim PageArray As List(Of Integer) = New List(Of Integer)()
    'Private Sub PopulatePrinterPageArray(ByVal maxPage As Integer, printerSettings As PrinterSettings)

    '    PageArray.Clear()

    '    Dim collate As Boolean = printerSettings.Collate
    '    Dim copies As Integer = printerSettings.Copies
    '    Dim from As Integer = printerSettings.FromPage
    '    Dim [to] As Integer = printerSettings.ToPage

    '    If (from < 1) Then from = 1
    '    If ([to] > maxPage) Then [to] = maxPage
    '    If (collate) Then

    '        For ipage As Integer = from To [to] Step 1

    '            For icopies As Integer = 0 To copies Step 1

    '                PageArray.Add(ipage)

    '            Next

    '        Next

    '    Else
    '        For icopies As Integer = 0 To copies Step 1

    '            For ipage As Integer = from To [to] Step 1

    '                PageArray.Add(ipage)

    '            Next

    '        Next

    '    End If

    'End Sub

    Private _printPageIndex = 0

    Public Function CheckSSRSConfiguration() As Boolean
        Dim strReportProtocol As String = String.Empty
        Dim strReportServer As String = String.Empty
        Dim strReportFolder As String = String.Empty
        Dim strVirtualDir As String = String.Empty
        Dim ReturnFlag As Boolean = False

        Dim sSQL As String = Nothing
        Dim dtSettings As DataTable = Nothing

        Dim oDBLayer As gloDatabaseLayer.DBLayer = Nothing

        Try
            oDBLayer = New gloDatabaseLayer.DBLayer(GetConnectionString())

            sSQL = "SELECT sSettingsName, ISNULL(sSettingsValue,'') AS sSettingsValue FROM Settings WITH (NOLOCK) WHERE UPPER(sSettingsName) IN ('REPORTPROTOCOL','REPORTSERVER','REPORTFOLDER','REPORTVIRTUALDIRECTORY') AND nClinicID = 1"

            dtSettings = New DataTable

            oDBLayer.Connect(False)
            oDBLayer.Retrive_Query(sSQL, dtSettings)
            oDBLayer.Disconnect()

            If Not IsNothing(dtSettings) AndAlso dtSettings.Rows.Count > 0 Then
                strReportProtocol = (From s In dtSettings.AsEnumerable() Where String.Compare(s.Field(Of String)("sSettingsName"), "ReportProtocol", True) = 0 Select s.Field(Of String)("sSettingsValue")).FirstOrDefault
                strReportServer = (From s In dtSettings.AsEnumerable() Where String.Compare(s.Field(Of String)("sSettingsName"), "ReportServer", True) = 0 Select s.Field(Of String)("sSettingsValue")).FirstOrDefault
                strReportFolder = (From s In dtSettings.AsEnumerable() Where String.Compare(s.Field(Of String)("sSettingsName"), "ReportFolder", True) = 0 Select s.Field(Of String)("sSettingsValue")).FirstOrDefault
                strVirtualDir = (From s In dtSettings.AsEnumerable() Where String.Compare(s.Field(Of String)("sSettingsName"), "ReportVirtualDirectory", True) = 0 Select s.Field(Of String)("sSettingsValue")).FirstOrDefault
            End If

            If (String.IsNullOrEmpty(strReportProtocol) OrElse String.IsNullOrEmpty(strReportServer) OrElse String.IsNullOrEmpty(strReportFolder) OrElse String.IsNullOrEmpty(strVirtualDir)) Then
                MessageBox.Show("SSRS Settings not set. Set the Report Server settings and then deploy the reports.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                ReturnFlag = False
            Else
                ReturnFlag = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ex = Nothing
        Finally
            strReportProtocol = Nothing
            strReportServer = Nothing
            strReportFolder = Nothing
            strVirtualDir = Nothing
            sSQL = Nothing

            If Not IsNothing(dtSettings) Then
                dtSettings.Dispose()
                dtSettings = Nothing
            End If
        End Try

        Return ReturnFlag

    End Function
    Private Function ShowExportDialog() As String

        Dim dlgSaveFileDialog As New SaveFileDialog
        Dim strExportPath As String = ""


        Try

            dlgSaveFileDialog.Filter = "Adobe Acrobat (*.pdf) |*.pdf"
            dlgSaveFileDialog.FilterIndex = 2
            dlgSaveFileDialog.RestoreDirectory = True

            If dlgSaveFileDialog.ShowDialog(System.Windows.Forms.Form.ActiveForm) = Windows.Forms.DialogResult.OK Then
                strExportPath = dlgSaveFileDialog.FileName
            End If


        Catch ex As Exception
            Throw ex

        Finally

            If Not IsNothing(dlgSaveFileDialog) Then
                dlgSaveFileDialog.Dispose()
                dlgSaveFileDialog = Nothing
            End If

        End Try

        Return strExportPath


    End Function

    'Start'GLO2011-0011148 ''White Space Issue'

    Private Function ExportClinicalCharts(Optional ByVal isPrintFax As Int16 = 0) As String
        '' Changed
        intFullFilePageCount = 0
        intDMSFilePageCount = 0
        OrderIDList = ""
        Dim strmessage As String = ""
        Dim _isPrintOrderMessageshown As Boolean = False
        Dim strProgressBarMessage As String = "Generating file ... "
        Dim LastOrderID As Int64 = 0
        Dim strNDC As String = ""
        Dim in_DocBlank As pdftron.PDF.PDFDoc = Nothing
        Dim oCurDoc1 As Wd.Document = Nothing
        '' Problem 00000028 : When printing from Reports Exams print/fax the reports do not have footers on them

        Try
            C1ClinicalChart.Enabled = False
            Dim sBuilder As New StringBuilder()

            Panel2.SuspendLayout()
            pnlProgress.Dock = DockStyle.Bottom
            Panel2.Height = 96
            Panel2.ResumeLayout()

            If (isPrintFax = 1) Then
                strProgressBarMessage = "Preparing for print..."
                strmessage = " printed out "
            ElseIf (isPrintFax = 2) Then
                strProgressBarMessage = "Preparing for fax..."
                strmessage = " faxed "
            ElseIf (isPrintFax = 3) Then
                strProgressBarMessage = "Preparing for Secure Message..."
            Else
                strProgressBarMessage = "Generating file ... "
            End If
            Dim strDocumentsPDF As String = Nothing
            If (isPrintFax) Then

                strDocumentsPDF = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".pdf", "MMddyyyyHHmmssffff")
            Else
                Dim Sfd1 As New SaveFileDialog
                If (IsNothing(Sfd1) = True) Then
                    Return ""
                End If
                Sfd1.Filter = "Adobe Acrobat (*.pdf) |*.pdf"
                Sfd1.FilterIndex = 2
                Sfd1.RestoreDirectory = True

                If Sfd1.ShowDialog(System.Windows.Forms.Form.ActiveForm) = Windows.Forms.DialogResult.OK Then
                    strDocumentsPDF = Sfd1.FileName
                Else
                    lblStatus.Visible = False
                    pnlProgress.Visible = False
                    Panel2.Height = 60
                    pnlProgress.Dock = DockStyle.None
                    If Not IsNothing(Sfd1) Then
                        Sfd1.Dispose()
                        Sfd1 = Nothing
                    End If
                    Return ""
                End If
                If Not IsNothing(Sfd1) Then
                    Sfd1.Dispose()
                    Sfd1 = Nothing
                End If
            End If

            If (IsNothing(strDocumentsPDF) = True) Then
                Return strDocumentsPDF
            End If
            Try
                in_DocBlank = New pdftron.PDF.PDFDoc()
                in_DocBlank.InitSecurityHandler()
                Dim page As Page = in_DocBlank.PageCreate()
                Dim new_DocPageCount As Integer = in_DocBlank.GetPageCount()
                Dim myNewPageItr As PageIterator = in_DocBlank.GetPageIterator(new_DocPageCount)
                in_DocBlank.PageInsert(myNewPageItr, page)
                in_DocBlank.Save(strDocumentsPDF, 0)
                in_DocBlank.Close()
                myNewPageItr.Dispose()
                page = Nothing
            Catch ex As Exception
                If (IsNothing(in_DocBlank) = False) Then
                    in_DocBlank.Dispose()
                    in_DocBlank = Nothing
                End If
                Return False
            End Try


            ''Changed
            If (IsNothing(in_DocBlank) = False) Then
                in_DocBlank.Dispose()
                in_DocBlank = Nothing
            End If
            prgGeneratefile.Value = 0
            ''Label12.Visible = False
            prgGeneratefile.Visible = True
            lblStatus.Text = strProgressBarMessage
            prgGeneratefile.Value = 0
            lblStatus.Visible = True
            pnlProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            prgGeneratefile.Minimum = 0
            prgGeneratefile.Maximum = C1ClinicalChart.Rows.Count - 1
            isblankDoc = True

            Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()


            For i As Int64 = 0 To C1ClinicalChart.Rows.Count - 1

                Dim strAllExamsDoc As String = ""
                Dim Flag As String
                Dim IsCheck As C1.Win.C1FlexGrid.CheckEnum

                Try
                    Try
                        Flag = C1ClinicalChart.GetData(System.Convert.ToInt16(i), 4).ToString()
                        IsCheck = C1ClinicalChart.GetCellCheck(System.Convert.ToInt16(i), 1)
                    Catch ex As Exception
                        Flag = 0
                        IsCheck = CheckEnum.None
                        'Audit LogFor Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
                    End Try
                    If (Flag = "1111") Then
                        Try
                            If (IsCheck = C1.Win.C1FlexGrid.CheckEnum.Checked) Then
                                If (strNDC.Trim() = "") Then
                                    strNDC = C1ClinicalChart.GetData(System.Convert.ToInt16(i), 0).ToString()
                                Else
                                    strNDC = strNDC & "~" & C1ClinicalChart.GetData(System.Convert.ToInt16(i), 0).ToString()
                                End If
                            End If
                            If (strNDC.Trim() = "") Then
                                prgGeneratefile.Value = prgGeneratefile.Value + 1
                                Application.DoEvents()
                                Continue For
                            End If
                            Dim strParameter As String
                            strParameter = _PatientID.ToString() & "," & mskStartDate.Text & "," & mskEndDate.Text & "," & strNDC
                            Dim strfilepath As String = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".pdf", "MMddyyyyHHmmssffff")

                            Dim blnSqlAuthentication As String = appSettings("WindowAuthentication")
                            If blnSqlAuthentication = True Then
                                blnSqlAuthentication = False
                            Else
                                blnSqlAuthentication = True
                            End If

                            Dim ocls As New gloSSRSApplication.clsSSRSRender(gstrSQLServerName, gstrDatabaseName, blnSqlAuthentication, gstrSQLUserEMR, gstrSQLPasswordEMR)
                            ocls.SSRSGeneratePDF("rptMedicalChart", "PatientID,sFromDate,sToDate,MedicationID", strParameter, strfilepath) ' "398297737939314001,01/01/2010,12/31/2010,4018495897290401~4018495897290402"
                            If (IO.File.Exists(strfilepath)) Then
                                Dim in_Doc1 As pdftron.PDF.PDFDoc = New pdftron.PDF.PDFDoc(strfilepath)
                                in_Doc1.InitSecurityHandler()
                                in_Doc1.PageCreate()
                                in_Doc1.Save(strfilepath, 0)
                                in_Doc1.Close()
                                in_Doc1.Dispose()
                                in_Doc1 = Nothing
                            End If
                            strDocumentsPDF = ImportsEntireDocument(strfilepath, strDocumentsPDF)
                            If (AuditLogModuleList.ToString.Contains("Medications") = False) Then
                                AuditLogModuleList.Append(", " & "Medications")
                            End If

                            ocls = Nothing

                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
                        End Try
                    End If

                    If IsCheck = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                        C1ClinicalChart.SetCellCheck(System.Convert.ToInt16(i), 1, CheckEnum.Unchecked)
                    Else
                        prgGeneratefile.Value = prgGeneratefile.Value + 1
                        Application.DoEvents()
                        Continue For
                    End If

                    If (Flag = "0") Then
                    ElseIf (Flag = "Flag") Then
                    ElseIf (Flag = "111") Then
                        ''sanjogs code
                        If (strNDC.Trim() = "") Then
                            strNDC = C1ClinicalChart.GetData(System.Convert.ToInt16(i), 0).ToString()
                        Else
                            strNDC = strNDC & "~" & C1ClinicalChart.GetData(System.Convert.ToInt16(i), 0).ToString()
                        End If
                    ElseIf (Flag = "1") Then
                        Try
                            Dim strTempExamfilepath As String = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".pdf", "MMddyyyyHHmmssffff")
                            strTempExamfilepath = GenratePatientInfoPDF(_PatientID, strTempExamfilepath)
                            If (strTempExamfilepath.Trim <> "") Then
                                isblankDoc = False
                                strDocumentsPDF = ImportsEntireDocument(strTempExamfilepath, strDocumentsPDF)
                                If (AuditLogModuleList.ToString.Contains("Patient Demographics") = False) Then
                                    AuditLogModuleList.Append("Patient Demographics")
                                End If

                            Else
                                'Audit Log
                            End If
                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
                        End Try

                    ElseIf (Flag = "2") Then
                        Dim ObjWord As clsWordDocument = Nothing
                        Dim objCriteria As DocCriteria = Nothing

                        Try

                            Dim nPastExamID As Long
                            nPastExamID = C1ClinicalChart.GetData(System.Convert.ToInt16(i), 0)
                            Dim strFileName As String

                            ObjWord = New clsWordDocument
                            objCriteria = New DocCriteria

                            If (IsNothing(ObjWord) = True) Then
                                Return False
                            End If

                            'objCriteria = New DocCriteria

                            If (IsNothing(objCriteria) = True) Then
                                Return False
                            End If

                            objCriteria.DocCategory = enumDocCategory.Exam
                            objCriteria.PrimaryID = nPastExamID
                            ObjWord.DocumentCriteria = objCriteria
                            strFileName = ObjWord.RetrieveDocumentFile()
                            strAllExamsDoc = strFileName
                            If (IsNothing(strFileName) = False) Then

                                Dim strTempExamfilepath As String = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".pdf", "MMddyyyyHHmmssffff")

                                Try
                                    oCurDoc1 = myLoadWord.LoadWordApplication(strAllExamsDoc)

                                Catch ex As Exception
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
                                    Return False
                                Finally

                                End Try

                                Dim objPrintFAX As New clsPrintFAX()

                                If System.Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings("USE_BUILDING_BLOCKS_IN_WORD_TEMPLATES")) Then
                                    objPrintFAX.InsertNamePageNo(oCurDoc1, objPrintFAX.GetPatientDetails(_PatientID))
                                Else
                                    objPrintFAX.InsertPageFooterWithoutMSWBuildingBlock(oCurDoc1, objPrintFAX.GetPatientDetails(_PatientID))
                                End If

                                objPrintFAX.Dispose()
                                objPrintFAX = Nothing
                                gloWord.LoadAndCloseWord.CleanupDoc(oCurDoc1)

                                oCurDoc1.Application.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone
                                oCurDoc1.SaveAs(strTempExamfilepath, Wd.WdSaveFormat.wdFormatPDF, False, "", False)

                                myLoadWord.CloseWordOnly(oCurDoc1)
                                strDocumentsPDF = ImportsEntireDocument(strTempExamfilepath, strDocumentsPDF)
                                If (AuditLogModuleList.ToString.Contains("Patient Exams") = False) Then
                                    AuditLogModuleList.Append(", Patient Exams")
                                End If

                                If (IO.File.Exists(strFileName)) Then
                                    IO.File.Delete(strFileName)
                                End If
                            End If
                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
                        Finally
                            ObjWord = Nothing

                            If Not IsNothing(objCriteria) Then
                                objCriteria.Dispose()
                                objCriteria = Nothing
                            End If

                        End Try
                    ElseIf (Flag.Contains("L")) Then '' = "3") Then
                        Dim oLabOrderRequest As New gloEMRLabOrder
                        Dim OrderId As Long = System.Convert.ToInt64(C1ClinicalChart.GetData(System.Convert.ToInt16(i), 0))
                        Dim OrderNumber As String = System.Convert.ToString(C1ClinicalChart.GetData(System.Convert.ToInt16(i), 1))

                        If isPrintFax = 1 Or isPrintFax = 2 Then
                            Dim OrderStatus As Int16 = oLabOrderRequest.GetOrderStatus(OrderId)
                            _isPrintOrderMessageshown = False

                            LastOrderID = OrderId
                            If _isPrintOrderMessageshown = False Then
                                If OrderStatus = 1001 Then
                                    If MessageBox.Show("You" + strmessage + "the Order Report for Order '" + OrderNumber + "'.  Its Status is 'New'." + vbNewLine + "Do you want to set the Order Status to 'Sent'?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                                        If OrderIDList = "" Then
                                            OrderIDList = System.Convert.ToString(OrderId)
                                        Else
                                            OrderIDList = OrderIDList + "," + System.Convert.ToString(OrderId)
                                        End If
                                    End If
                                    _isPrintOrderMessageshown = True
                                End If
                            End If
                        End If

                        Dim bnSqlAuthentication As Boolean = Not System.Convert.ToBoolean(appSettings("WindowAuthentication"))

                        Dim strTempExamfilepath As String = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".pdf", "MMddyyyyHHmmssffff")
                        Dim ocls As New gloSSRSApplication.clsSSRSRender(gstrSQLServerName, gstrDatabaseName, bnSqlAuthentication, gstrSQLUserEMR, gstrSQLPasswordEMR)
                        ocls.SSRSGeneratePDF("LabOrderReport_SSRS", "OrderID,PatientID", System.Convert.ToString(OrderId) + "," & System.Convert.ToString(_PatientID), strTempExamfilepath)
                        If (IO.File.Exists(strTempExamfilepath)) Then
                            Dim in_Doc1 As pdftron.PDF.PDFDoc = New pdftron.PDF.PDFDoc(strTempExamfilepath)
                            in_Doc1.InitSecurityHandler()
                            in_Doc1.PageCreate()
                            in_Doc1.Save(strTempExamfilepath, 0)
                            in_Doc1.Close()
                            in_Doc1.Dispose()
                            in_Doc1 = Nothing
                        End If

                        ocls = Nothing
                        'END here
                        strDocumentsPDF = ImportsEntireDocument(strTempExamfilepath, strDocumentsPDF)
                        If (AuditLogModuleList.ToString.Contains("Orders") = False) Then
                            AuditLogModuleList.Append(", Orders")
                        End If
                        oLabOrderRequest.Dispose()
                        oLabOrderRequest = Nothing
                    ElseIf (Flag = "4") Then
                        Try
                            Dim _dtLabdata As DataTable = GetLabPrintData(_PatientID, mskStartDate.Text, mskEndDate.Text)
                            If (_dtLabdata.Rows.Count = 0) Then
                                prgGeneratefile.Value = prgGeneratefile.Value + 1
                                Application.DoEvents()
                                _dtLabdata.Dispose()
                                _dtLabdata = Nothing
                                Continue For
                            End If
                            Dim dtPatientTable As DataTable = GetPatientInformation(_PatientID, GetConnectionString())
                            Dim cntCol As Integer = 0
                            Dim intrptPages As Integer = 0
                            Dim intLastColPrinted As Integer = 2
                            Dim MyText As CrystalDecisions.CrystalReports.Engine.TextObject = Nothing
                            Dim objfrmViewgloLab As New gloEmdeonInterface.Forms.frmViewgloLab(_PatientID)
                            Dim strTempExamfilepath As String = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".pdf", "MMddyyyyHHmmssffff")
                            Dim isExported As Boolean = objfrmViewgloLab.TestResultPrint(_dtLabdata, dtPatientTable, intrptPages, intLastColPrinted, MyText, cntCol, strTempExamfilepath)
                            If (isExported) Then
                                strDocumentsPDF = ImportsEntireDocument(strTempExamfilepath, strDocumentsPDF)
                                If (AuditLogModuleList.ToString.Contains("Lab Flowsheet(s)") = False) Then
                                    AuditLogModuleList.Append(", Lab Flowsheet(s)")
                                End If
                            End If

                            If Not IsNothing(MyText) Then
                                MyText.Dispose()
                                MyText = Nothing
                            End If

                            _dtLabdata.Dispose()
                            _dtLabdata = Nothing
                            dtPatientTable.Dispose()
                            dtPatientTable = Nothing
                            If (IsNothing(objfrmViewgloLab) = False) Then
                                objfrmViewgloLab.Close()
                            End If

                            If (IsNothing(objfrmViewgloLab) = False) Then
                                objfrmViewgloLab.Dispose()
                                objfrmViewgloLab = Nothing
                            End If


                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
                        End Try
                        ' ElseIf (Flag = "5") Then
                    ElseIf (Flag.Contains("C")) Then
                        Dim _DocID As Long = 0
                        Dim docDMSFile As pdftron.PDF.PDFDoc = Nothing
                        Dim docFullFile As pdftron.PDF.PDFDoc = Nothing
                        Try
                            _DocID = System.Convert.ToInt64(C1ClinicalChart.GetData(System.Convert.ToInt16(i), 0))
                            ''
                            Dim myDMSConnectionString As String = gloEDocumentV3.gloEDocV3Admin.GetDMSConnectionString(GetConnectionString())
                            Dim myDMSObj As gloEDocumentV3.eDocManager.eDocGetList = Nothing
                            Dim mySqlQuery As String = "Select Distinct eContainerID from eDocument_Container_V3 where eDocumentID = " + _DocID.ToString()
                            Dim myDataAdapter As New SqlClient.SqlDataAdapter(mySqlQuery, myDMSConnectionString)
                            Dim myDatatable As New DataTable
                            Dim myContainerID As Long ' = 40518550163683901
                            myDataAdapter.SelectCommand.CommandTimeout = 0
                            myDataAdapter.Fill(myDatatable)
                            myDataAdapter.Dispose()
                            myDataAdapter = Nothing
                            If Not IsNothing(myDatatable) Then
                                If (myDatatable.Rows.Count > 0) Then

                                    Dim strContainerfileName As String = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".pdf", "MMddyyyyHHmmssffff")
                                    For j As Integer = 0 To myDatatable.Rows.Count - 1
                                        myContainerID = myDatatable.Rows(j)(0)
                                        If (IO.File.Exists(strDocumentsPDF) = False) Then
                                            myDMSObj = New gloEDocumentV3.eDocManager.eDocGetList()
                                            myDMSObj.GetContainerStream(_DocID, myContainerID, 1, strDocumentsPDF)
                                        Else
                                            myDMSObj = New gloEDocumentV3.eDocManager.eDocGetList()
                                            myDMSObj.GetContainerStream(_DocID, myContainerID, 1, strContainerfileName)
                                            docDMSFile = New pdftron.PDF.PDFDoc(strContainerfileName)
                                            docFullFile = New pdftron.PDF.PDFDoc(strDocumentsPDF)

                                            If (intFullFilePageCount <= 0) Then
                                                docFullFile.InitSecurityHandler()
                                                intFullFilePageCount = docFullFile.GetPageCount()
                                                docFullFile.Close()
                                            End If

                                            docDMSFile.InitSecurityHandler()
                                            If (intDMSFilePageCount <= 0) Then
                                                intDMSFilePageCount = docDMSFile.GetPageCount() + intFullFilePageCount
                                            Else
                                                intDMSFilePageCount = docDMSFile.GetPageCount() + intDMSFilePageCount
                                            End If
                                            docDMSFile.Close()

                                            strDocumentsPDF = ImportsEntireDocument(strContainerfileName, strDocumentsPDF)
                                        End If
                                        If (AuditLogModuleList.ToString.Contains("Scanned Documents") = False) Then
                                            AuditLogModuleList.Append(", Scanned Documents")
                                        End If
                                        myDMSObj.Dispose()
                                        myDMSObj = Nothing
                                    Next

                                End If
                                myDatatable.Dispose()
                                myDatatable = Nothing
                            End If
                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
                        End Try
                    ElseIf (Flag = "") Then
                    ElseIf (Flag = "16") Then
                        Dim TransactionId As Long = System.Convert.ToInt64(C1ClinicalChart.GetData(System.Convert.ToInt32(i), COL_Seven))
                        Dim MasterTransactionID As Long = System.Convert.ToInt64(C1ClinicalChart.GetData(System.Convert.ToInt16(i), 0).ToString())
                        Dim sClaimType As String = System.Convert.ToString(C1ClinicalChart.GetData(System.Convert.ToInt16(i), COL_Five))
                        Dim bIsInstitutional As Boolean = False
                        Dim sFilePath As String = String.Empty
                        Dim nContactID As Int64 = System.Convert.ToInt64(C1ClinicalChart.GetData(System.Convert.ToInt16(i), COL_Six))
                        Dim sClaimNumber As String = System.Convert.ToString(C1ClinicalChart.GetData(System.Convert.ToInt16(i), COL_Category))

                        If sBuilder Is Nothing Then
                            sBuilder = New StringBuilder()
                        End If

                        sBuilder.Append(" Claim: ")
                        sBuilder.Append(System.Convert.ToString(C1ClinicalChart.GetData(System.Convert.ToInt16(i), COL_Category)) + " ")

                        Dim strTempExamfilepath As String = ExamNewDocumentName.ToString.Replace(".docx", ".pdf")
                        Dim sPrinterName As String = "Default"
                        If _nQueueID = 0 Then
                            If cmbClaimPrinter.Text = "" Then
                                sPrinterName = "Default"
                            Else
                                sPrinterName = cmbClaimPrinter.Text
                            End If
                        Else
                            sPrinterName = GetQueuedPrinterName(_nQueueID)
                        End If

                        If Boolean.TryParse(sClaimType, bIsInstitutional) Then
                            If bIsInstitutional Then
                                Using objPrintUB As New UB04(GetConnectionString())
                                    objPrintUB.FillUB04Form(TransactionId, MasterTransactionID)

                                    If rbPrintOnForm.Checked Then
                                        sFilePath = objPrintUB.Print(True, strTempExamfilepath)
                                    ElseIf rbPrintData.Checked Then
                                        sFilePath = objPrintUB.Print(False, strTempExamfilepath, sPrinterName)
                                    End If

                                End Using
                            Else
                                Using objPrintClaim As New HCFA1500New(GetConnectionString())
                                    objPrintClaim.PatientID = _PatientID
                                    objPrintClaim.FillTransactionOnForm(TransactionId, MasterTransactionID)
                                    If rbPrintOnForm.Checked Then
                                        sFilePath = objPrintClaim.Print(True, strTempExamfilepath)
                                    ElseIf rbPrintData.Checked Then
                                        sFilePath = objPrintClaim.Print(False, strTempExamfilepath, sPrinterName)
                                    End If
                                End Using
                            End If
                        End If

                        If (IO.File.Exists(sFilePath)) Then

                            strDocumentsPDF = ImportsEntireDocument(sFilePath, strDocumentsPDF)

                            Dim claimNoteID As New ClaimNoteIDs()
                            With claimNoteID
                                .MasterTransactionID = MasterTransactionID
                                .ContactID = nContactID
                                .ClaimID = sClaimNumber
                            End With
                            IO.File.Delete(sFilePath)
                            lstClaimMasterIDs.Add(claimNoteID)
                            claimNoteID = Nothing
                        End If
                    ElseIf (Flag = "17") Then
                        Dim bnSqlAuthentication As Boolean = Not System.Convert.ToBoolean(appSettings("WindowAuthentication"))

                        Dim nCreditID As Int64 = System.Convert.ToInt64(C1ClinicalChart.GetData(System.Convert.ToInt32(i), 0))
                        Dim nEOBID As Int64 = System.Convert.ToInt64(C1ClinicalChart.GetData(System.Convert.ToInt32(i), COL_Five))

                        Dim strTempExamfilepath As String = ExamNewDocumentName.ToString.Replace(".docx", ".pdf")

                        Dim ocls As New gloSSRSApplication.clsSSRSRender(gstrSQLServerName, gstrDatabaseName, bnSqlAuthentication, gstrSQLUserEMR, gstrSQLPasswordEMR)
                        ocls.SSRSGeneratePDF("rptClaimRemittance", "nCreditID,nEOBID,nEOBType,UserName", System.Convert.ToString(nCreditID) + "," + System.Convert.ToString(nEOBID) + "," & 4 & "," + gloGlobal.gloPMGlobal.UserName, strTempExamfilepath)
                        ocls = Nothing

                        If (IO.File.Exists(strTempExamfilepath)) Then
                            Dim pdfDoc As pdftron.PDF.PDFDoc = New pdftron.PDF.PDFDoc(strTempExamfilepath)
                            pdfDoc.InitSecurityHandler()
                            pdfDoc.PageCreate()
                            pdfDoc.Save(strTempExamfilepath, 0)
                            pdfDoc.Close()
                            pdfDoc.Dispose()
                            pdfDoc = Nothing
                            strDocumentsPDF = ImportsEntireDocument(strTempExamfilepath, strDocumentsPDF)
                        End If

                        sBuilder.Append(" Insurance Payment: ")
                        sBuilder.Append(System.Convert.ToString(C1ClinicalChart.GetData(System.Convert.ToInt16(i), COL_Category)) + " ")
                    ElseIf (Flag = "18") Then
                        Dim bnSqlAuthentication As Boolean = Not System.Convert.ToBoolean(appSettings("WindowAuthentication"))

                        Dim TransactionId As Long = System.Convert.ToInt64(C1ClinicalChart.GetData(System.Convert.ToInt32(i), COL_Seven))
                        Dim MasterTransactionID As Long = System.Convert.ToInt64(C1ClinicalChart.GetData(System.Convert.ToInt16(i), 0).ToString())
                        Dim strTempExamfilepath As String = ExamNewDocumentName.ToString.Replace(".docx", ".pdf")
                        Dim ocls As New gloSSRSApplication.clsSSRSRender(gstrSQLServerName, gstrDatabaseName, bnSqlAuthentication, gstrSQLUserEMR, gstrSQLPasswordEMR)
                        ocls.SSRSGeneratePDF("rptAgingClaimHistory", "nMasterTransactionID,nTransactionID,nPatientID,suser", System.Convert.ToString(MasterTransactionID) + "," + System.Convert.ToString(TransactionId) + "," & _PatientID & "," + gloGlobal.gloPMGlobal.UserName, strTempExamfilepath)
                        'ocls.SSRSGeneratePDF("rptAgingClaimHistory", "nMasterTransactionID,nTransactionID,nPatientID", System.Convert.ToString(MasterTransactionID) + "," + System.Convert.ToString(TransactionId) + "," & _PatientID, strTempExamfilepath)
                        ocls = Nothing

                        If (IO.File.Exists(strTempExamfilepath)) Then
                            Dim pdfDoc As pdftron.PDF.PDFDoc = New pdftron.PDF.PDFDoc(strTempExamfilepath)
                            pdfDoc.InitSecurityHandler()
                            pdfDoc.PageCreate()
                            pdfDoc.Save(strTempExamfilepath, 0)
                            pdfDoc.Close()
                            pdfDoc.Dispose()
                            pdfDoc = Nothing
                            strDocumentsPDF = ImportsEntireDocument(strTempExamfilepath, strDocumentsPDF)
                        End If

                        sBuilder.Append(" Insurance Payment: ")
                        sBuilder.Append(System.Convert.ToString(C1ClinicalChart.GetData(System.Convert.ToInt16(i), COL_Category)) + " ")
                    ElseIf (Flag = "6") Or (Flag = "7") Or (Flag = "8") Or (Flag = "9") Or (Flag = "10") Or (Flag = "11") Or (Flag = "12") Or (Flag = "13") Or (Flag = "14") Or (Flag = "15") Or (Flag = "10") Or (Flag = "19") Or (Flag.Contains("T")) Or (Flag = "21") Then
                        Dim objWord As clsWordDocument = Nothing
                        Dim oDB As DataBaseLayer = Nothing
                        Dim oParamater As DBParameter = Nothing
                        Dim dt As DataTable = Nothing
                        Try
                            objWord = New clsWordDocument
                            Dim strFileName As String
                            Dim nID As String
                            strFileName = ExamNewDocumentName
                            nID = C1ClinicalChart.GetData(System.Convert.ToInt16(i), 0).ToString()
                            oDB = New DataBaseLayer 'gloStream.gloDataBase.gloDataBase

                            Dim SQL As String = ""
                            Dim Pfound As Boolean = False
                            Dim rIndex As Integer = 0
                            Dim OrderID As Int64 = 0
                            Dim OrderNumber As String = ""
                            Dim OrderStatus As Int16

                            Dim oLabOrderRequest As New gloEMRLabOrder
                            If (C1ClinicalChart.GetData(System.Convert.ToInt16(i), 4).ToString().Contains("T")) Then
                                Flag = 3
                                rIndex = i
                                While (Pfound <> True)
                                    If (C1ClinicalChart.Rows(rIndex)(4).ToString.Trim().Contains("L")) Then

                                        Pfound = True
                                        OrderID = System.Convert.ToInt64(C1ClinicalChart.GetData(rIndex, 0))
                                        OrderNumber = System.Convert.ToString(C1ClinicalChart.GetData(rIndex, 1))
                                        OrderStatus = oLabOrderRequest.GetOrderStatus(OrderID)
                                    End If
                                    rIndex = rIndex - 1
                                End While
                            Else
                                Flag = CType(C1ClinicalChart.GetData(System.Convert.ToInt16(i), 4).ToString(), Integer)
                            End If
                            If IsNothing(oLabOrderRequest) = False Then
                                oLabOrderRequest.Dispose()
                                oLabOrderRequest = Nothing
                            End If
                            If isPrintFax = 1 Or isPrintFax = 2 Then
                                If (LastOrderID <> OrderID) Then
                                    LastOrderID = OrderID
                                    _isPrintOrderMessageshown = False
                                End If
                                If _isPrintOrderMessageshown = False Then
                                    If OrderStatus = 1001 Then
                                        If MessageBox.Show("You" + strmessage + "the Order Template for Order '" + OrderNumber + "'.  Its Status is 'New'." + vbNewLine + "Do you want to set the Order Status to 'Sent'?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                                            If OrderIDList = "" Then
                                                OrderIDList = System.Convert.ToString(OrderID)
                                            Else
                                                OrderIDList = OrderIDList + "," + System.Convert.ToString(OrderID)
                                            End If
                                        End If
                                        _isPrintOrderMessageshown = True
                                    End If
                                End If

                            End If

                            Dim nVisistId As Int64 = 0
                            Dim sDocumentURL As String = ""
                            Dim NeedUpdate As Byte = 0
                            If Flag = "21" Then
                                nVisistId = System.Convert.ToInt64(C1ClinicalChart.GetData(System.Convert.ToInt16(i), COL_Five))
                                sDocumentURL = System.Convert.ToString(C1ClinicalChart.GetData(System.Convert.ToInt16(i), COL_Six))
                                NeedUpdate = System.Convert.ToByte(C1ClinicalChart.GetData(System.Convert.ToInt16(i), COL_Seven))
                                If NeedUpdate = 1 Then
                                    SaveEducationLinkData(System.Convert.ToInt64(nID), nVisistId, sDocumentURL)
                                    C1ClinicalChart(System.Convert.ToInt16(i), COL_Seven) = "0"
                                End If
                            End If

                            
                            oParamater = New DBParameter
                            oParamater.DataType = SqlDbType.BigInt
                            oParamater.Direction = ParameterDirection.Input
                            oParamater.Name = "@ID"
                            oParamater.Value = System.Convert.ToInt64(nID)
                            oDB.DBParametersCol.Add(oParamater)
                            oParamater = Nothing

                            oParamater = New DBParameter
                            oParamater.DataType = SqlDbType.BigInt
                            oParamater.Direction = ParameterDirection.Input
                            oParamater.Name = "@sFlag"
                            oParamater.Value = Flag
                            oDB.DBParametersCol.Add(oParamater)
                            oParamater = Nothing
                            If (Flag = 3) Then
                                oParamater = New DBParameter
                                oParamater.DataType = SqlDbType.BigInt
                                oParamater.Direction = ParameterDirection.Input
                                oParamater.Name = "@OrderID"
                                oParamater.Value = OrderID
                                oDB.DBParametersCol.Add(oParamater)
                                oParamater = Nothing
                            End If


                            dt = oDB.GetDataTable("gsp_GetClinicalChartTemplate")

                            If (dt.Rows.Count >= 1) Then
                                Dim strTempfilepath As String = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".pdf", "MMddyyyyHHmmssffff")
                                strFileName = objWord.GenerateFile(dt.Rows(0)(0), strFileName)

                                oCurDoc1 = myLoadWord.LoadWordApplication(strFileName)

                                '' Problem 00000028 : When printing from Reports Exams print/fax the reports do not have footers on them
                                Dim objPrintFAX As New clsPrintFAX()

                                If System.Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings("USE_BUILDING_BLOCKS_IN_WORD_TEMPLATES")) Then
                                    objPrintFAX.InsertNamePageNo(oCurDoc1, objPrintFAX.GetPatientDetails(_PatientID))
                                Else
                                    objPrintFAX.InsertPageFooterWithoutMSWBuildingBlock(oCurDoc1, objPrintFAX.GetPatientDetails(_PatientID))
                                End If

                                objPrintFAX.Dispose()
                                objPrintFAX = Nothing

                                gloWord.LoadAndCloseWord.CleanupDoc(oCurDoc1)
                                oCurDoc1.Application.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone
                                oCurDoc1.SaveAs(strTempfilepath, Wd.WdSaveFormat.wdFormatPDF, False, "", False)
                                myLoadWord.CloseWordOnly(oCurDoc1)
                                strDocumentsPDF = ImportsEntireDocument(strTempfilepath, strDocumentsPDF)
                            Dim strModuleName As String = ""
                            Select Case Flag
                                Case "6"
                                    strModuleName = "Order Templates"
                                Case "7"
                                    strModuleName = "Referral Letters"
                                Case "8"
                                    strModuleName = "Messages"
                                Case "9"
                                    strModuleName = "Nurse Notes"
                                Case "10"
                                    strModuleName = "Triage"
                                Case "11"
                                    strModuleName = "PT Protocol"
                                Case "12"
                                    strModuleName = "Patient Consent"
                                Case "13"
                                    strModuleName = "Disclosure Management"
                                Case "14"
                                    strModuleName = "Patient Letters"
                                Case "15"
                                    strModuleName = "Form Gallery"
                                Case "19"
                                    strModuleName = "Patient Forms"
                                Case "3"
                                    strModuleName = "Orders"
                                Case "21"
                                    strModuleName = "Patient Education"
                            End Select

                            If (AuditLogModuleList.ToString.Contains(strModuleName.ToString()) = False) Then
                                AuditLogModuleList.Append(", " & strModuleName.ToString())
                            End If

                            If (IO.File.Exists(strFileName)) Then
                                IO.File.Delete(strFileName)
                            End If
                            End If
                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
                        Finally
                            objWord = Nothing
                            oParamater = Nothing

                            If Not IsNothing(oDB) Then
                                oDB.Dispose()
                                oDB = Nothing
                            End If

                            If Not IsNothing(dt) Then
                                dt.Dispose()
                                dt = Nothing
                            End If

                        End Try
                    End If


                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
                End Try
                prgGeneratefile.Value = i
                Application.DoEvents()
            Next

            myLoadWord.CloseApplicationOnly()

            If (AuditLogModuleList.Length > 0) Then
                If (AuditLogModuleList.Chars(0).ToString = ",") Then
                    AuditLogModuleList.Remove(0, 1)
                End If
            End If

            Me.Cursor = Cursors.Default
            Try
                If (IO.File.Exists(strDocumentsPDF)) Then
                    Dim in_DocBlank1 As pdftron.PDF.PDFDoc = New pdftron.PDF.PDFDoc(strDocumentsPDF)
                    in_DocBlank1.InitSecurityHandler()
                    Dim new_DocPageCount1 As Integer = in_DocBlank1.GetPageCount()
                    Dim myNewPageItr1 As PageIterator = in_DocBlank1.GetPageIterator(new_DocPageCount1)
                    in_DocBlank1.PageRemove(myNewPageItr1)
                    in_DocBlank1.Save(strDocumentsPDF, 0)
                    in_DocBlank1.Close()
                    in_DocBlank1.Dispose()
                    in_DocBlank1 = Nothing
                    myNewPageItr1.Dispose()
                    myNewPageItr1 = Nothing
                End If
                lblStatus.Text = "Completed"
                prgGeneratefile.Value = 0

                Select Case isPrintFax
                    Case 1
                        sNoteDescription = " printed."
                    Case 2
                        sNoteDescription = " faxed."
                    Case 3
                        sNoteDescription = " Secure messaged."
                    Case Else
                        sNoteDescription = " exported."
                        Me.InsertNotesInClaims(lstClaimMasterIDs)
                End Select
                If sBuilder IsNot Nothing AndAlso sBuilder.ToString().Any() Then
                    Dim activityType As gloAuditTrail.ActivityType

                    Select Case isPrintFax
                        Case 1
                            activityType = gloAuditTrail.ActivityType.Print
                        Case 2
                            activityType = gloAuditTrail.ActivityType.Fax
                        Case 3
                            activityType = gloAuditTrail.ActivityType.ClinicalExchange
                        Case Else
                            activityType = gloAuditTrail.ActivityType.Export
                    End Select

                    gloAuditTrail.gloAuditTrail.CreateAuditLog(
                                                                gloAuditTrail.ActivityModule.Reports,
                                                                gloAuditTrail.ActivityCategory.ClinicalChart,
                                                                activityType,
                                                                sBuilder.ToString(),
                                                                gloAuditTrail.ActivityOutCome.Success
                                                                )
                End If

                If sBuilder IsNot Nothing Then
                    sBuilder.Clear()
                    sBuilder = Nothing
                End If

                Me.Cursor = Cursors.Default
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
            End Try

            Return strDocumentsPDF
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
            Return ""
        Finally
            C1ClinicalChart.Enabled = True
        End Try
    End Function

    Private Sub SaveEducationLinkData(nEducatioId As Int64, VisitId As Int64, sURL As String)
        'Code to load url to browser then browser to word -> PDF -> Append PDF
        Dim outputFileName As String = gloGlobal.clsFileExtensions.NewDocumentName(gloGlobal.gloTSPrint.TempPath, ".docx", "MMddyyyyHHmmssffff")
        Dim InfoButtonForm As gloEMRGeneralLibrary.frmInfoButtonBrowser = gloEMRGeneralLibrary.frmInfoButtonBrowser.GetInstance
        'Try
        '    RemoveHandler InfoButtonForm.FormClosed, AddressOf On_InfoButton_Closed
        'Catch ex As Exception

        'End Try
        'AddHandler InfoButtonForm.FormClosed, AddressOf On_InfoButton_Closed
        With InfoButtonForm
            .LoginProviderID = gnLoginProviderID
            .PatientId = _PatientID
            .VisitID = VisitId
            .EducationID = nEducatioId
            .isViewed = True
            '.Source = _src
            '.ResourceCategory = _recCat
            '.ResourceType = _resTyp
            .NavigateTo(sURL)
            '.ValidatePortalFeatures()
            .justToSaveLink = True
            .PathToSaveLink = outputFileName
            .ControlBox = False
            .WindowState = FormWindowState.Minimized
            .ShowDialog()
        End With
        InfoButtonForm.Dispose()

        Dim oWord As clsWordDocument = New clsWordDocument
        Dim _speNotes As Object = CType(oWord.ConvertFiletoBinary(outputFileName), Object)
        oWord = Nothing

        Dim oPatientEducation As New clsPatientEducation
        oPatientEducation.SaveExamEducation(nVisitID:=VisitId, nPatientID:=_PatientID, nExamID:=0, oTemplateResult:=_speNotes, sTemplateName:="", Resourcecategory:=2, nModifyEducationId:=nEducatioId)
        oPatientEducation.Dispose()
        oPatientEducation = Nothing
    End Sub

    Public Function ImportsEntireDocument_old(ByVal file1 As String, ByVal file2 As String)

        Dim in_Doc1 As pdftron.PDF.PDFDoc = New pdftron.PDF.PDFDoc(file1)
        Dim in_Doc2 As pdftron.PDF.PDFDoc = New pdftron.PDF.PDFDoc(file2)

        in_Doc1.InitSecurityHandler()
        in_Doc2.InitSecurityHandler()
        Dim myInteger As Integer = in_Doc1.GetPageCount()
        For i As Integer = 1 To myInteger
            Dim src_Page As Page = in_Doc1.GetPage(i)
            Dim new_DocPageCount As Integer = in_Doc2.GetPageCount()

            Dim myNewPageItr As PageIterator = in_Doc2.GetPageIterator(new_DocPageCount)
            in_Doc2.PageInsert(myNewPageItr, src_Page)
            src_Page = Nothing
            myNewPageItr.Dispose()
            myNewPageItr = Nothing
        Next i

        in_Doc2.PageCreate()
        in_Doc2.Save(file2, 0)
        in_Doc2.Close()
        in_Doc1.Close()
        in_Doc1 = Nothing
        in_Doc2 = Nothing
        Try
            If (IO.File.Exists(file1)) Then
                IO.File.Delete(file1)
            End If
        Catch ex As Exception

        End Try
        isblankDoc = False
        Return file2
    End Function
    Public Function ImportsEntireDocument(ByVal file1 As String, ByVal file2 As String)
        Dim in_Doc1 As pdftron.PDF.PDFDoc = Nothing
        Dim in_Doc2 As pdftron.PDF.PDFDoc = Nothing
        Try

            in_Doc1 = New pdftron.PDF.PDFDoc(file1)
            If (IsNothing(in_Doc1) = True) Then

                Return file2
            End If
            in_Doc2 = New pdftron.PDF.PDFDoc(file2)
            If (IsNothing(in_Doc2) = True) Then
                in_Doc1.Dispose()
                in_Doc1 = Nothing
                Return file1
            End If
            Try
                in_Doc1.InitSecurityHandler()
                in_Doc2.InitSecurityHandler()
            Catch ex As Exception
                in_Doc1.Dispose()
                in_Doc1 = Nothing
                in_Doc2.Dispose()
                in_Doc2 = Nothing
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
                Return file2
            End Try

            Dim myInteger As Integer = in_Doc1.GetPageCount()

            If myInteger > 0 Then
                For i As Integer = 1 To myInteger
                    Dim src_Page As Page = in_Doc1.GetPage(i)
                    Dim new_DocPageCount As Integer = in_Doc2.GetPageCount()

                    Dim myNewPageItr As PageIterator = in_Doc2.GetPageIterator(new_DocPageCount)
                    in_Doc2.PageInsert(myNewPageItr, src_Page)
                    myNewPageItr.Dispose()
                    myNewPageItr = Nothing
                    src_Page = Nothing
                Next i
            End If
            Try
                in_Doc2.PageCreate()
                in_Doc2.Save(file2, 0)

            Catch ex As Exception

                gloAuditTrail.gloAuditTrail.ExceptionLog("Cannot able to save :" & ex.ToString, False)
                Return file2
            End Try

            in_Doc2.Close()
            in_Doc1.Close()
            in_Doc1.Dispose()
            in_Doc1 = Nothing
            in_Doc2.Dispose()
            in_Doc2 = Nothing

            Try
                If (IO.File.Exists(file1)) Then
                    IO.File.Delete(file1)
                End If
            Catch ex As Exception

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
                Return file2
            End Try
            isblankDoc = False
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
            Return file2
        End Try
        Return file2
    End Function
    'End'GLO2011-0011148 ''White Space Issue'
    Sub LoadClinicalChartlist(Optional dtDos As String = "")
        Try
            isloading = True
            C1ClinicalChart.Clear()

            gloC1FlexStyle.Style(C1ClinicalChart)
            C1ClinicalChart.Tree.Column = 1
            C1ClinicalChart.Rows.Count = 1
            C1ClinicalChart.Cols.Count = 8
            C1ClinicalChart.Cols(COL_Check).Caption = "ID"
            C1ClinicalChart.Cols(COL_Check).Width = 0
            C1ClinicalChart.Cols(1).Width = 700
            C1ClinicalChart.Cols(2).Width = 400
            C1ClinicalChart.Cols(3).Width = 50
            C1ClinicalChart.Cols(4).Width = 0
            C1ClinicalChart.Cols(COL_Five).Width = 0
            C1ClinicalChart.Cols(COL_Six).Width = 0
            C1ClinicalChart.Cols(COL_Seven).Width = 0

            C1ClinicalChart.Cols(COL_Check).AllowEditing = True
            C1ClinicalChart.Cols(1).AllowEditing = True
            C1ClinicalChart.Cols(2).AllowEditing = False
            C1ClinicalChart.Cols(3).AllowEditing = False
            C1ClinicalChart.Cols(4).AllowEditing = False
            C1ClinicalChart.Cols(COL_Five).AllowEditing = False
            C1ClinicalChart.Cols(COL_Six).AllowEditing = False
            C1ClinicalChart.Cols(COL_Seven).AllowEditing = False

            C1ClinicalChart.Cols(COL_Check).AllowDragging = False
            C1ClinicalChart.Cols(1).AllowDragging = False
            C1ClinicalChart.Cols(2).AllowDragging = False
            C1ClinicalChart.Cols(3).AllowDragging = False
            C1ClinicalChart.Cols(4).AllowDragging = False
            C1ClinicalChart.Cols(COL_Five).AllowDragging = False
            C1ClinicalChart.Cols(COL_Six).AllowDragging = False
            C1ClinicalChart.Cols(COL_Seven).AllowDragging = False

            C1ClinicalChart.Cols(1).Caption = "Category"
            C1ClinicalChart.Cols(2).Caption = "Provider"
            C1ClinicalChart.Cols(3).Caption = "Date"
            C1ClinicalChart.Cols(4).Caption = "Flag"

            ' populate the control with product structure

            Dim cs As C1.Win.C1FlexGrid.CellStyle = C1ClinicalChart.Cols(COL_Date).StyleNew
            cs.DataType = GetType(DateTime)

            Dim row As Integer = 1, level As Integer = 0

            C1ClinicalChart.Rows.InsertNode(row, level)
            C1ClinicalChart(row, COL_Category) = "Patient Information"
            C1ClinicalChart(row, COL_Four) = 1
            C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
            row = row + 1
            level = 1

            level = 0
            C1ClinicalChart.Rows.InsertNode(row, level)
            C1ClinicalChart(row, COL_Category) = "Current Medications "
            C1ClinicalChart(row, COL_Four) = 0
            C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
            row = row + 1
            level = 1

            Dim dtPatientDetails As DataTable = Fill_ClinicalChartInfo(_PatientID, 1, mskStartDate.Text, mskEndDate.Text, dtDos)
            Dim j As Int64 = 0
            For row = row To row + dtPatientDetails.Rows.Count - 1

                C1ClinicalChart.Rows.InsertNode(row, level)
                C1ClinicalChart(row, COL_Check) = dtPatientDetails.Rows(j)("nMedicationID").ToString() ''id
                If (dtPatientDetails.Rows(j)("sDosage").ToString.Trim() = "") Then
                    C1ClinicalChart(row, COL_Category) = dtPatientDetails.Rows(j)("sMedication").ToString()
                Else
                    C1ClinicalChart(row, COL_Category) = dtPatientDetails.Rows(j)("sMedication").ToString() & " " & dtPatientDetails.Rows(j)("sDosage").ToString()
                End If
                C1ClinicalChart(row, COL_Provider) = ""  'provider name
                C1ClinicalChart(row, COL_Date) = System.Convert.ToDateTime(dtPatientDetails.Rows(j)("dtMedicationDate").ToString())
                If j = dtPatientDetails.Rows.Count - 1 Then
                    C1ClinicalChart(row, COL_Four) = 1111
                Else
                    C1ClinicalChart(row, COL_Four) = 111
                End If

                j = j + 1
                C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)

            Next
            'row = row + 1

            level = 0
            C1ClinicalChart.Rows.InsertNode(row, level)
            C1ClinicalChart(row, COL_Category) = "Lab Flowsheet"
            C1ClinicalChart(row, COL_Four) = 4
            C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
            row = row + 1
            level = 1

            level = 0
            C1ClinicalChart.Rows.InsertNode(row, level)
            C1ClinicalChart(row, COL_Category) = "Orders"
            C1ClinicalChart(row, COL_Four) = 0
            C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
            row = row + 1
            level = 1
            If (IsNothing(dtPatientDetails) = False) Then
                dtPatientDetails.Dispose()
                dtPatientDetails = Nothing
            End If
            dtPatientDetails = Fill_ClinicalChartInfo(_PatientID, 3, mskStartDate.Text, mskEndDate.Text, dtDos)
            j = 0
            For row = row To row + dtPatientDetails.Rows.Count - 1

                C1ClinicalChart.Rows.InsertNode(row, level)
                C1ClinicalChart(row, COL_Check) = dtPatientDetails.Rows(j)("labom_OrderID")
                C1ClinicalChart(row, COL_Category) = dtPatientDetails.Rows(j)("labom_OrderNoPrefix").ToString() + " " + dtPatientDetails.Rows(j)("labom_OrderNoID").ToString()

                C1ClinicalChart(row, COL_Provider) = "" 'labom_ProviderID
                C1ClinicalChart(row, COL_Date) = System.Convert.ToDateTime(dtPatientDetails.Rows(j)("labom_TransactionDate").ToString())
                C1ClinicalChart(row, COL_Four) = 3
                j = j + 1
                C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)

            Next

            level = 0
            C1ClinicalChart.Rows.InsertNode(row, level)
            C1ClinicalChart(row, COL_Category) = "Patient Exams"
            C1ClinicalChart(row, COL_Four) = 0
            C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
            row = row + 1
            level = 1
            If (IsNothing(dtPatientDetails) = False) Then
                dtPatientDetails.Dispose()
                dtPatientDetails = Nothing
            End If

            dtPatientDetails = Fill_ClinicalChartInfo(_PatientID, 4, mskStartDate.Text, mskEndDate.Text, dtDos)
            j = 0
            For row = row To row + dtPatientDetails.Rows.Count - 1

                C1ClinicalChart.Rows.InsertNode(row, level)
                C1ClinicalChart(row, COL_Check) = dtPatientDetails.Rows(j)("nExamID")
                C1ClinicalChart(row, COL_Category) = dtPatientDetails.Rows(j)("Exam Name").ToString()

                C1ClinicalChart(row, COL_Provider) = dtPatientDetails.Rows(j)("ProviderName").ToString()
                C1ClinicalChart(row, COL_Date) = System.Convert.ToDateTime(dtPatientDetails.Rows(j)("DOS").ToString())
                C1ClinicalChart(row, COL_Four) = 2
                j = j + 1
                C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)

            Next

            '    #Region "Scanned Document (DMS)"
            level = 0
            C1ClinicalChart.Rows.InsertNode(row, level)
            C1ClinicalChart(row, COL_Category) = "Scanned Documents"
            C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
            C1ClinicalChart(row, COL_Four) = 0
            row = row + 1
            '''' level = 1
            If (IsNothing(dtPatientDetails) = False) Then
                dtPatientDetails.Dispose()
                dtPatientDetails = Nothing
            End If
            dtPatientDetails = Fill_ClinicalChartInfo(_PatientID, 5, mskStartDate.Text, mskEndDate.Text, dtDos)
            j = 0

            Dim rowCount As Integer ''= row + dtPatientDetails.Rows.Count - 1
            Dim CategoryId As Integer = 0
            rowCount = row
            Dim fCnt As Integer = 10
            For row = row To row + dtPatientDetails.Rows.Count - 1
                If CategoryId <> CType(dtPatientDetails(j)("CategoryID"), Int32) Then
                    fCnt = fCnt + 1
                    level = 1
                    C1ClinicalChart.Rows.InsertNode(rowCount, level)
                    C1ClinicalChart(rowCount, COL_Check) = 0
                    C1ClinicalChart(rowCount, COL_Category) = dtPatientDetails.Rows(j)("Category").ToString()
                    C1ClinicalChart(rowCount, COL_Provider) = ""
                    C1ClinicalChart(rowCount, COL_Date) = ""
                    C1ClinicalChart(rowCount, 4) = String.Concat("R", fCnt)
                    C1ClinicalChart.SetCellCheck(rowCount, 1, CheckEnum.Unchecked)

                    CategoryId = CType(dtPatientDetails.Rows(j)("CategoryID"), Int32)
                    rowCount = rowCount + 1
                End If
                level = 2
                ' rowCount = row + 1
                C1ClinicalChart.Rows.InsertNode(rowCount, level)
                C1ClinicalChart(rowCount, COL_Check) = dtPatientDetails.Rows(j)("eDocumentID")
                C1ClinicalChart(rowCount, COL_Category) = dtPatientDetails.Rows(j)("DocumentName").ToString()
                C1ClinicalChart(rowCount, COL_Provider) = ""
                C1ClinicalChart(rowCount, COL_Date) = System.Convert.ToDateTime(dtPatientDetails.Rows(j)("CreatedDateTime").ToString())
                C1ClinicalChart(rowCount, 4) = String.Concat("C", fCnt)
                j = j + 1
                C1ClinicalChart.SetCellCheck(rowCount, 1, CheckEnum.Unchecked)
                rowCount = rowCount + 1

            Next
            row = rowCount

            ' #End Region
            level = 0
            C1ClinicalChart.Rows.InsertNode(row, level)
            C1ClinicalChart(row, COL_Category) = "Order Templates"
            C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
            C1ClinicalChart(row, COL_Four) = 0
            row = row + 1
            level = 1
            If (IsNothing(dtPatientDetails) = False) Then
                dtPatientDetails.Dispose()
                dtPatientDetails = Nothing
            End If
            dtPatientDetails = Fill_ClinicalChartInfo(_PatientID, 6, mskStartDate.Text, mskEndDate.Text, dtDos)
            j = 0
            Dim objpatientexam As New clsPatientExams()

            For row = row To row + dtPatientDetails.Rows.Count - 1
                C1ClinicalChart.Rows.InsertNode(row, level)
                C1ClinicalChart(row, COL_Check) = dtPatientDetails.Rows(j)("lm_Order_ID").ToString()
                C1ClinicalChart(row, COL_Category) = dtPatientDetails.Rows(j)("lm_sCategoryName").ToString() & "~" & dtPatientDetails.Rows(j)("lm_sGroupName").ToString() & "~" & dtPatientDetails.Rows(j)("lm_sTestName").ToString()
                C1ClinicalChart(row, COL_Provider) = objpatientexam.GetProvidernameforExam(CType(dtPatientDetails.Rows(j)("lm_Provider_id"), Int64))    '"Provider Name" 'dtPatientDetails.Rows(j)("ProviderName").ToString()
                C1ClinicalChart(row, COL_Date) = dtPatientDetails.Rows(j)("lm_OrderDate").ToString()

                C1ClinicalChart(row, COL_Four) = "6"
                j = j + 1
                C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
            Next

            level = 0
            C1ClinicalChart.Rows.InsertNode(row, level)
            C1ClinicalChart(row, COL_Category) = "Referral Letters"
            C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
            C1ClinicalChart(row, COL_Four) = 0
            row = row + 1
            level = 1
            If (IsNothing(dtPatientDetails) = False) Then
                dtPatientDetails.Dispose()
                dtPatientDetails = Nothing
            End If
            dtPatientDetails = Fill_ClinicalChartInfo(_PatientID, 7, mskStartDate.Text, mskEndDate.Text, dtDos)
            j = 0
            For row = row To row + dtPatientDetails.Rows.Count - 1
                C1ClinicalChart.Rows.InsertNode(row, level)
                C1ClinicalChart(row, COL_Check) = dtPatientDetails.Rows(j)("nReferralID").ToString()
                C1ClinicalChart(row, COL_Category) = dtPatientDetails.Rows(j)("sTemplatename").ToString()
                C1ClinicalChart(row, COL_Provider) = dtPatientDetails.Rows(j)("ProviderName").ToString() 'objpatientexam.GetProvidernameforExam(CType(dtPatientDetails.Rows(j)("nProviderID"), Int64))    '"Provider Name" 'dtPatientDetails.Rows(j)("ProviderName").ToString()
                C1ClinicalChart(row, COL_Date) = dtPatientDetails.Rows(j)("dtReferralDate").ToString()
                C1ClinicalChart(row, COL_Four) = 7
                j = j + 1
                C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
            Next

            level = 0
            C1ClinicalChart.Rows.InsertNode(row, level)
            C1ClinicalChart(row, COL_Category) = "Messages"
            C1ClinicalChart(row, COL_Four) = 0
            C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
            row = row + 1
            level = 1
            If (IsNothing(dtPatientDetails) = False) Then
                dtPatientDetails.Dispose()
                dtPatientDetails = Nothing
            End If
            dtPatientDetails = Fill_ClinicalChartInfo(_PatientID, 8, mskStartDate.Text, mskEndDate.Text, dtDos)
            j = 0
            For row = row To row + dtPatientDetails.Rows.Count - 1
                C1ClinicalChart.Rows.InsertNode(row, level)
                C1ClinicalChart(row, COL_Check) = dtPatientDetails.Rows(j)("nMessageID").ToString()
                C1ClinicalChart(row, COL_Category) = dtPatientDetails.Rows(j)("sTemplateName").ToString()
                C1ClinicalChart(row, COL_Date) = System.Convert.ToDateTime(dtPatientDetails.Rows(j)("Date").ToString())
                C1ClinicalChart(row, COL_Four) = 8
                j = j + 1
                C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
            Next

            level = 0
            C1ClinicalChart.Rows.InsertNode(row, level)
            C1ClinicalChart(row, COL_Category) = "Nurse Notes"
            C1ClinicalChart(row, COL_Four) = 0
            C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
            row = row + 1
            level = 1
            If (IsNothing(dtPatientDetails) = False) Then
                dtPatientDetails.Dispose()
                dtPatientDetails = Nothing
            End If
            dtPatientDetails = Fill_ClinicalChartInfo(_PatientID, 9, mskStartDate.Text, mskEndDate.Text, dtDos)
            j = 0
            For row = row To row + dtPatientDetails.Rows.Count - 1
                C1ClinicalChart.Rows.InsertNode(row, level)
                C1ClinicalChart(row, COL_Check) = dtPatientDetails.Rows(j)("nNurseNotesID").ToString()
                C1ClinicalChart(row, COL_Category) = dtPatientDetails.Rows(j)("sTemplateName").ToString()
                C1ClinicalChart(row, COL_Date) = dtPatientDetails.Rows(j)("dtNotesDate").ToString()
                C1ClinicalChart(row, COL_Four) = 9
                j = j + 1
                C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
            Next

            level = 0
            C1ClinicalChart.Rows.InsertNode(row, level)
            C1ClinicalChart(row, COL_Category) = "Triage"
            C1ClinicalChart(row, COL_Four) = 0
            C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
            row = row + 1
            level = 1
            If (IsNothing(dtPatientDetails) = False) Then
                dtPatientDetails.Dispose()
                dtPatientDetails = Nothing
            End If
            dtPatientDetails = Fill_ClinicalChartInfo(_PatientID, 10, mskStartDate.Text, mskEndDate.Text, dtDos)
            j = 0
            For row = row To row + dtPatientDetails.Rows.Count - 1
                C1ClinicalChart.Rows.InsertNode(row, level)
                C1ClinicalChart(row, COL_Check) = dtPatientDetails.Rows(j)("nTriageID").ToString()
                C1ClinicalChart(row, COL_Category) = dtPatientDetails.Rows(j)("sTemplateName").ToString()
                C1ClinicalChart(row, COL_Date) = dtPatientDetails.Rows(j)("dtDate").ToString()
                C1ClinicalChart(row, COL_Four) = 10
                j = j + 1
                C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
            Next

            level = 0
            C1ClinicalChart.Rows.InsertNode(row, level)
            C1ClinicalChart(row, COL_Category) = "PT Protocol"
            C1ClinicalChart(row, COL_Four) = 0
            C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
            row = row + 1
            level = 1
            If (IsNothing(dtPatientDetails) = False) Then
                dtPatientDetails.Dispose()
                dtPatientDetails = Nothing
            End If
            dtPatientDetails = Fill_ClinicalChartInfo(_PatientID, 11, mskStartDate.Text, mskEndDate.Text, dtDos)
            j = 0
            For row = row To row + dtPatientDetails.Rows.Count - 1
                C1ClinicalChart.Rows.InsertNode(row, level)
                C1ClinicalChart(row, COL_Check) = dtPatientDetails.Rows(j)("nProtocolID").ToString()
                C1ClinicalChart(row, COL_Category) = dtPatientDetails.Rows(j)("sTemplateName").ToString()
                C1ClinicalChart(row, COL_Date) = dtPatientDetails.Rows(j)("dtProtocoldate").ToString()
                C1ClinicalChart(row, COL_Four) = 11
                j = j + 1
                C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
            Next

            level = 0
            C1ClinicalChart.Rows.InsertNode(row, level)
            C1ClinicalChart(row, COL_Category) = "Patient Consent"
            C1ClinicalChart(row, COL_Four) = 0
            C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
            row = row + 1
            level = 1
            If (IsNothing(dtPatientDetails) = False) Then
                dtPatientDetails.Dispose()
                dtPatientDetails = Nothing
            End If
            dtPatientDetails = Fill_ClinicalChartInfo(_PatientID, 12, mskStartDate.Text, mskEndDate.Text, dtDos)
            j = 0
            For row = row To row + dtPatientDetails.Rows.Count - 1
                C1ClinicalChart.Rows.InsertNode(row, level)
                C1ClinicalChart(row, COL_Check) = dtPatientDetails.Rows(j)("nConsentID").ToString()
                C1ClinicalChart(row, COL_Category) = dtPatientDetails.Rows(j)("sTemplateName").ToString()
                C1ClinicalChart(row, COL_Date) = dtPatientDetails.Rows(j)("dtConsentDate").ToString()
                C1ClinicalChart(row, COL_Four) = 12
                j = j + 1
                C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
            Next

            level = 0
            C1ClinicalChart.Rows.InsertNode(row, level)
            C1ClinicalChart(row, COL_Category) = "Disclosure Management"
            C1ClinicalChart(row, COL_Four) = 0
            C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
            row = row + 1
            level = 1

            If (IsNothing(dtPatientDetails) = False) Then
                dtPatientDetails.Dispose()
                dtPatientDetails = Nothing
            End If
            dtPatientDetails = Fill_ClinicalChartInfo(_PatientID, 13, mskStartDate.Text, mskEndDate.Text, dtDos)
            j = 0
            For row = row To row + dtPatientDetails.Rows.Count - 1
                C1ClinicalChart.Rows.InsertNode(row, level)
                C1ClinicalChart(row, COL_Check) = dtPatientDetails.Rows(j)("nDisclosureID").ToString()
                C1ClinicalChart(row, COL_Category) = dtPatientDetails.Rows(j)("sTemplateName").ToString()
                C1ClinicalChart(row, COL_Date) = dtPatientDetails.Rows(j)("dtDisclosureDate").ToString()
                C1ClinicalChart(row, COL_Four) = 13
                j = j + 1
                C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
            Next

            level = 0
            C1ClinicalChart.Rows.InsertNode(row, level)
            C1ClinicalChart(row, COL_Category) = "Patient Letters"
            C1ClinicalChart(row, COL_Four) = 0
            C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
            row = row + 1
            level = 1
            If (IsNothing(dtPatientDetails) = False) Then
                dtPatientDetails.Dispose()
                dtPatientDetails = Nothing
            End If
            dtPatientDetails = Fill_ClinicalChartInfo(_PatientID, 14, mskStartDate.Text, mskEndDate.Text, dtDos)
            j = 0
            For row = row To row + dtPatientDetails.Rows.Count - 1
                C1ClinicalChart.Rows.InsertNode(row, level)
                C1ClinicalChart(row, COL_Check) = dtPatientDetails.Rows(j)("nLetterID").ToString()
                C1ClinicalChart(row, COL_Category) = dtPatientDetails.Rows(j)("sTemplateName").ToString()
                C1ClinicalChart(row, COL_Date) = dtPatientDetails.Rows(j)("dtLetterDate").ToString()
                C1ClinicalChart(row, COL_Four) = 14
                j = j + 1
                C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
            Next

            level = 0
            C1ClinicalChart.Rows.InsertNode(row, level)
            C1ClinicalChart(row, COL_Category) = "Form Gallery"
            C1ClinicalChart(row, COL_Four) = 0
            C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
            row = row + 1
            level = 1
            If (IsNothing(dtPatientDetails) = False) Then
                dtPatientDetails.Dispose()
                dtPatientDetails = Nothing
            End If
            dtPatientDetails = Fill_ClinicalChartInfo(_PatientID, 15, mskStartDate.Text, mskEndDate.Text, dtDos)
            j = 0
            For row = row To row + dtPatientDetails.Rows.Count - 1
                C1ClinicalChart.Rows.InsertNode(row, level)
                C1ClinicalChart(row, COL_Check) = dtPatientDetails.Rows(j)("nFormID").ToString()
                C1ClinicalChart(row, COL_Category) = dtPatientDetails.Rows(j)("sTemplateName").ToString()
                C1ClinicalChart(row, COL_Date) = dtPatientDetails.Rows(j)("dtVisitDate").ToString()
                C1ClinicalChart(row, COL_Four) = 15
                j = j + 1
                C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
            Next

            level = 0
            C1ClinicalChart.Rows.InsertNode(row, level)
            C1ClinicalChart(row, COL_Category) = "Patient Education"
            C1ClinicalChart(row, COL_Four) = 0
            setCellcheck(row, "Patient Education")
            row = row + 1
            level = 1
            If (IsNothing(dtPatientDetails) = False) Then
                dtPatientDetails.Dispose()
                dtPatientDetails = Nothing
            End If
            dtPatientDetails = Fill_ClinicalChartInfo(_PatientID, 21, mskStartDate.Text, mskEndDate.Text, dtDos)
            j = 0
            For row = row To row + dtPatientDetails.Rows.Count - 1
                C1ClinicalChart.Rows.InsertNode(row, level)
                C1ClinicalChart(row, COL_Check) = dtPatientDetails.Rows(j)("nEducationID").ToString()
                C1ClinicalChart(row, COL_Category) = dtPatientDetails.Rows(j)("sTemplateName").ToString()
                C1ClinicalChart(row, COL_Date) = dtPatientDetails.Rows(j)("dtVisitDate").ToString()
                C1ClinicalChart(row, COL_Four) = 21
                C1ClinicalChart(row, COL_Five) = dtPatientDetails.Rows(j)("nVisitID").ToString()
                C1ClinicalChart(row, COL_Six) = dtPatientDetails.Rows(j)("sDocumentURL").ToString()
                C1ClinicalChart(row, COL_Seven) = dtPatientDetails.Rows(j)("NeedUpdate").ToString()
                setCellcheck(row, dtPatientDetails.Rows(j)("nEducationID").ToString())
                j = j + 1
            Next

            If Not IsNothing(dtPatientDetails) Then
                dtPatientDetails.Dispose()
                dtPatientDetails = Nothing
            End If
            objpatientexam.Dispose()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
        isloading = False
    End Sub


    Private Sub ArrangeGridItems()
        Try
            Dim dtDOS As String = ""
            If Not isValidDates() Then
                Return
            End If
            isloading = True

            If (_nQueueID > 0) Then
                dtQueued = GetQueuedDocuments(_nQueueID)
            End If
            mskDOS.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
            If mskDOS.Text.Trim <> "" Then
                mskDOS.TextMaskFormat = MaskFormat.IncludePromptAndLiterals
                dtDOS = mskDOS.Text
            End If
            Dim sClinicalchartSetting() As String
            Dim strSetting As String = getClinicalChartSetting()
            If String.Compare(strSetting, "") <> 0 Then
                sClinicalchartSetting = strSetting.Split(",")
            Else
                LoadClinicalChartlist(dtDOS)
                Exit Sub
            End If



            C1ClinicalChart.Clear()

            gloC1FlexStyle.Style(C1ClinicalChart)
            C1ClinicalChart.Tree.Column = 1
            C1ClinicalChart.Rows.Count = 1
            C1ClinicalChart.Cols.Count = 8

            C1ClinicalChart.Cols(COL_Check).Caption = "ID"
            C1ClinicalChart.Cols(COL_Check).Width = 0

            C1ClinicalChart.Cols(1).Width = 700
            C1ClinicalChart.Cols(2).Width = 400
            C1ClinicalChart.Cols(3).Width = 50
            C1ClinicalChart.Cols(4).Width = 0
            C1ClinicalChart.Cols(5).Width = 0
            C1ClinicalChart.Cols(6).Width = 0
            C1ClinicalChart.Cols(7).Width = 0

            C1ClinicalChart.Cols(COL_Check).AllowEditing = True
            C1ClinicalChart.Cols(1).AllowEditing = True
            C1ClinicalChart.Cols(2).AllowEditing = False
            C1ClinicalChart.Cols(3).AllowEditing = False
            C1ClinicalChart.Cols(4).AllowEditing = False
            C1ClinicalChart.Cols(COL_Five).AllowEditing = False
            C1ClinicalChart.Cols(COL_Six).AllowEditing = False
            C1ClinicalChart.Cols(COL_Seven).AllowEditing = False

            Dim cs As C1.Win.C1FlexGrid.CellStyle = C1ClinicalChart.Cols(COL_Date).StyleNew
            cs.DataType = GetType(DateTime)

            C1ClinicalChart.Cols(COL_Check).AllowDragging = False
            C1ClinicalChart.Cols(1).AllowDragging = False
            C1ClinicalChart.Cols(2).AllowDragging = False
            C1ClinicalChart.Cols(3).AllowDragging = False
            C1ClinicalChart.Cols(4).AllowDragging = False
            C1ClinicalChart.Cols(COL_Five).AllowDragging = False
            C1ClinicalChart.Cols(COL_Six).AllowDragging = False
            C1ClinicalChart.Cols(COL_Seven).AllowDragging = False

            C1ClinicalChart.Cols(1).Caption = "Category"
            C1ClinicalChart.Cols(2).Caption = "Provider"
            C1ClinicalChart.Cols(3).Caption = "Date"
            C1ClinicalChart.Cols(4).Caption = "Flag"

            ' populate the control with product structure

            Dim row As Integer = 1, level As Integer = 0, i As Integer, j As Int64
            Dim dtPatientDetails As DataTable = Nothing
            Dim _sClinicalchartValue() As String
            For i = 0 To sClinicalchartSetting.Length - 1
                _sClinicalchartValue = sClinicalchartSetting.GetValue(i).ToString().Split(".")

                If _sClinicalchartValue(0).ToString().Trim = "C" Then
                    If (_sClinicalchartValue.Length > 1) Then


                        Select Case Replace(_sClinicalchartValue(1).Trim(), " ", "_")

                            Case enumDefaultClinicalChart.Patient_Information.ToString()
                                level = 0
                                C1ClinicalChart.Rows.InsertNode(row, level)
                                C1ClinicalChart(row, COL_Category) = "Patient Information"
                                C1ClinicalChart(row, COL_Four) = 1
                                setCellcheck(row, "Patient Information")
                                row = row + 1
                                level = 1

                            Case enumDefaultClinicalChart.Current_Medications.ToString()

                                level = 0
                                C1ClinicalChart.Rows.InsertNode(row, level)
                                C1ClinicalChart(row, COL_Category) = "Current Medications "
                                C1ClinicalChart(row, COL_Four) = 0
                                setCellcheck(row, "Current Medications ")
                                row = row + 1
                                level = 1

                                dtPatientDetails = Fill_ClinicalChartInfo(_PatientID, 1, mskStartDate.Text, mskEndDate.Text, dtDOS)

                                j = 0
                                For row = row To row + dtPatientDetails.Rows.Count - 1

                                    C1ClinicalChart.Rows.InsertNode(row, level)
                                    C1ClinicalChart(row, COL_Check) = dtPatientDetails.Rows(j)("nMedicationID").ToString() ''id
                                    If (dtPatientDetails.Rows(j)("sDosage").ToString.Trim() = "") Then
                                        C1ClinicalChart(row, COL_Category) = dtPatientDetails.Rows(j)("sMedication").ToString()
                                    Else
                                        C1ClinicalChart(row, COL_Category) = dtPatientDetails.Rows(j)("sMedication").ToString() & " " & dtPatientDetails.Rows(j)("sDosage").ToString()
                                    End If
                                    C1ClinicalChart(row, COL_Provider) = ""  'provider name
                                    C1ClinicalChart(row, COL_Date) = System.Convert.ToDateTime(dtPatientDetails.Rows(j)("dtMedicationDate").ToString()) '.ToShortDateString() 'dtPatientDetails.Rows(j)("dtMedicationDate").ToString()  'Date.Today() 'dtMedicationDate
                                    If j = dtPatientDetails.Rows.Count - 1 Then
                                        C1ClinicalChart(row, COL_Four) = 1111
                                    Else
                                        C1ClinicalChart(row, COL_Four) = 111
                                    End If
                                    setCellcheck(row, dtPatientDetails.Rows(j)("nMedicationID").ToString())
                                    j = j + 1

                                Next
                                If (IsNothing(dtPatientDetails) = False) Then
                                    dtPatientDetails.Dispose()
                                    dtPatientDetails = Nothing
                                End If
                                'row = row + 1

                            Case enumDefaultClinicalChart.Lab_Flowsheet.ToString()

                                level = 0
                                C1ClinicalChart.Rows.InsertNode(row, level)
                                C1ClinicalChart(row, COL_Category) = "Lab Flowsheet"
                                C1ClinicalChart(row, COL_Four) = 4
                                setCellcheck(row, "Lab Flowsheet")
                                row = row + 1
                                level = 1
                            Case enumDefaultClinicalChart.Labs.ToString()
                                Dim objpatientexam As New clsPatientExams()
                                level = 0
                                C1ClinicalChart.Rows.InsertNode(row, level)
                                C1ClinicalChart(row, COL_Category) = "Orders"
                                C1ClinicalChart(row, COL_Four) = 0
                                setCellcheck(row, "Orders")
                                row = row + 1
                                level = 1

                                dtPatientDetails = Fill_ClinicalChartInfo(_PatientID, 3, mskStartDate.Text, mskEndDate.Text, dtDOS)
                                ''code modified by Mayuri:20140613-To show orders with templates and to print templates and report
                                j = 0
                                Dim rowCount As Integer ''= row + dtPatientDetails.Rows.Count - 1
                                Dim OrderID As Int64 = 0
                                rowCount = row
                                Dim fCnt As Integer = 10
                                For row = row To row + dtPatientDetails.Rows.Count - 1
                                    If OrderID <> CType(dtPatientDetails(j)("labom_OrderID"), Int64) Then
                                        fCnt = fCnt + 1
                                        level = 1

                                        C1ClinicalChart.Rows.InsertNode(rowCount, level)
                                        C1ClinicalChart(rowCount, COL_Check) = dtPatientDetails.Rows(j)("labom_OrderID")
                                        C1ClinicalChart(rowCount, COL_Category) = dtPatientDetails.Rows(j)("labom_OrderNoPrefix").ToString() + " " + dtPatientDetails.Rows(j)("labom_OrderNoID").ToString()

                                        C1ClinicalChart(rowCount, COL_Provider) = objpatientexam.GetProvidernameforExam(CType(dtPatientDetails.Rows(j)("labom_ProviderID"), Int64))
                                        'Added code changes for Bug #79115: 00000849: Orders in clinical chart should show Ordered Date instead of Transaction Date.
                                        C1ClinicalChart(rowCount, COL_Date) = System.Convert.ToDateTime(dtPatientDetails.Rows(j)("labom_OrderDate").ToString()) '.ToShortDateString() 'dtPatientDetails.Rows(j)("dtMedicationDate").ToString()  'Date.Today() 'dtMedicationDate
                                        C1ClinicalChart(rowCount, 4) = String.Concat("L", fCnt)

                                        C1ClinicalChart.SetCellCheck(rowCount, 1, CheckEnum.Unchecked)
                                        OrderID = CType(dtPatientDetails.Rows(j)("labom_OrderID"), Int64)
                                        setCellcheck(rowCount, dtPatientDetails.Rows(j)("labom_OrderID").ToString())
                                        rowCount = rowCount + 1
                                    End If
                                    If dtPatientDetails.Rows(j)("labotd_Template").ToString() <> "" Then
                                        level = 2
                                        ' rowCount = row + 1
                                        C1ClinicalChart.Rows.InsertNode(rowCount, level)
                                        C1ClinicalChart(rowCount, COL_Check) = dtPatientDetails.Rows(j)("labotd_TestID")
                                        C1ClinicalChart(rowCount, COL_Category) = dtPatientDetails.Rows(j)("TemplateName").ToString()
                                        C1ClinicalChart(rowCount, COL_Provider) = objpatientexam.GetProvidernameforExam(CType(dtPatientDetails.Rows(j)("labom_ProviderID"), Int64))
                                        'Added code changes for Bug #79115: 00000849: Orders in clinical chart should show Ordered Date instead of Transaction Date.
                                        C1ClinicalChart(rowCount, COL_Date) = System.Convert.ToDateTime(dtPatientDetails.Rows(j)("labom_OrderDate").ToString()) '.ToShortDateString() 'dtPatientDetails.Rows(j)("dtMedicationDate").ToString()  'Date.Today() 'dtMedicationDate
                                        C1ClinicalChart(rowCount, 4) = String.Concat("T", fCnt)

                                        setCellcheck(rowCount, dtPatientDetails.Rows(j)("labotd_TestID").ToString(), gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.OrdersTestResult.GetHashCode())

                                        rowCount = rowCount + 1
                                    End If
                                    j = j + 1
                                Next
                                objpatientexam.Dispose()
                                objpatientexam = Nothing
                                If (IsNothing(dtPatientDetails) = False) Then
                                    dtPatientDetails.Dispose()
                                    dtPatientDetails = Nothing
                                End If
                                row = rowCount
                            Case enumDefaultClinicalChart.Patient_Exams.ToString()

                                level = 0
                                C1ClinicalChart.Rows.InsertNode(row, level)
                                C1ClinicalChart(row, COL_Category) = "Patient Exams"
                                C1ClinicalChart(row, COL_Four) = 0
                                '  C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
                                setCellcheck(row, "Patient Exams")
                                row = row + 1
                                level = 1


                                dtPatientDetails = Fill_ClinicalChartInfo(_PatientID, 4, mskStartDate.Text, mskEndDate.Text, dtDOS)
                                j = 0
                                For row = row To row + dtPatientDetails.Rows.Count - 1

                                    C1ClinicalChart.Rows.InsertNode(row, level)
                                    C1ClinicalChart(row, COL_Check) = dtPatientDetails.Rows(j)("nExamID")
                                    C1ClinicalChart(row, COL_Category) = dtPatientDetails.Rows(j)("Exam Name").ToString()

                                    C1ClinicalChart(row, COL_Provider) = dtPatientDetails.Rows(j)("ProviderName").ToString()
                                    C1ClinicalChart(row, COL_Date) = System.Convert.ToDateTime(dtPatientDetails.Rows(j)("DOS").ToString()) '.ToShortDateString() 'dtPatientDetails.Rows(j)("dtMedicationDate").ToString()  'Date.Today() 'dtMedicationDate
                                    C1ClinicalChart(row, COL_Four) = 2
                                    setCellcheck(row, dtPatientDetails.Rows(j)("nExamID").ToString())

                                    j = j + 1

                                Next
                                If (IsNothing(dtPatientDetails) = False) Then
                                    dtPatientDetails.Dispose()
                                    dtPatientDetails = Nothing
                                End If
                            Case enumDefaultClinicalChart.Scanned_Documents.ToString()
                                '    #Region "Scanned Document (DMS)"
                                level = 0
                                C1ClinicalChart.Rows.InsertNode(row, level)
                                C1ClinicalChart(row, COL_Category) = "Scanned Documents"
                                setCellcheck(row, "Scanned Documents")
                                C1ClinicalChart(row, COL_Four) = 0
                                row = row + 1
                                '''' level = 1

                                dtPatientDetails = Fill_ClinicalChartInfo(_PatientID, 5, mskStartDate.Text, mskEndDate.Text, dtDOS)

                                j = 0

                                Dim rowCount As Integer ''= row + dtPatientDetails.Rows.Count - 1
                                Dim CategoryId As Integer = 0
                                rowCount = row
                                Dim fCnt As Integer = 10
                                For row = row To row + dtPatientDetails.Rows.Count - 1
                                    If CategoryId <> CType(dtPatientDetails(j)("CategoryID"), Int32) Then
                                        fCnt = fCnt + 1
                                        level = 1
                                        C1ClinicalChart.Rows.InsertNode(rowCount, level)
                                        C1ClinicalChart(rowCount, COL_Check) = 0 '' dtPatientDetails.Rows(j)("eDocumentID")
                                        C1ClinicalChart(rowCount, COL_Category) = dtPatientDetails.Rows(j)("Category").ToString()
                                        C1ClinicalChart.Rows(rowCount).TextAlign = TextAlignEnum.LeftCenter
                                        C1ClinicalChart(rowCount, COL_Provider) = ""
                                        C1ClinicalChart(rowCount, COL_Date) = "" ''System.Convert.ToDateTime(dtPatientDetails.Rows(j)("CreatedDateTime").ToString()).ToShortDateString() 'dtPatientDetails.Rows(j)("dtMedicationDate").ToString()  'Date.Today() 'dtMedicationDate
                                        C1ClinicalChart(rowCount, 4) = String.Concat("R", fCnt)
                                        setCellcheck(rowCount, dtPatientDetails.Rows(j)("Category").ToString())

                                        CategoryId = CType(dtPatientDetails.Rows(j)("CategoryID"), Int32)
                                        'j = j + 1
                                        rowCount = rowCount + 1
                                    End If
                                    level = 2
                                    ' rowCount = row + 1
                                    C1ClinicalChart.Rows.InsertNode(rowCount, level)
                                    C1ClinicalChart(rowCount, COL_Check) = dtPatientDetails.Rows(j)("eDocumentID")
                                    C1ClinicalChart(rowCount, COL_Category) = dtPatientDetails.Rows(j)("DocumentName").ToString()
                                    C1ClinicalChart(rowCount, COL_Provider) = ""
                                    C1ClinicalChart(rowCount, COL_Date) = System.Convert.ToDateTime(dtPatientDetails.Rows(j)("CreatedDateTime").ToString()) '.ToShortDateString() 'dtPatientDetails.Rows(j)("dtMedicationDate").ToString()  'Date.Today() 'dtMedicationDate
                                    C1ClinicalChart(rowCount, 4) = String.Concat("C", fCnt)
                                    setCellcheck(rowCount, dtPatientDetails.Rows(j)("eDocumentID").ToString())

                                    j = j + 1
                                    rowCount = rowCount + 1


                                Next
                                If (IsNothing(dtPatientDetails) = False) Then
                                    dtPatientDetails.Dispose()
                                    dtPatientDetails = Nothing
                                End If
                                row = rowCount
                            Case enumDefaultClinicalChart.Orders.ToString()


                                level = 0
                                C1ClinicalChart.Rows.InsertNode(row, level)
                                C1ClinicalChart(row, COL_Category) = "Order Templates"
                                '  C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
                                setCellcheck(row, "Order Templates")
                                C1ClinicalChart(row, COL_Four) = 0
                                row = row + 1
                                level = 1

                                dtPatientDetails = Fill_ClinicalChartInfo(_PatientID, 6, mskStartDate.Text, mskEndDate.Text, dtDOS)
                                j = 0
                                Dim objpatientexam As New clsPatientExams()


                                For row = row To row + dtPatientDetails.Rows.Count - 1
                                    C1ClinicalChart.Rows.InsertNode(row, level)
                                    C1ClinicalChart(row, COL_Check) = dtPatientDetails.Rows(j)("lm_Order_ID").ToString()
                                    C1ClinicalChart(row, COL_Category) = dtPatientDetails.Rows(j)("lm_sCategoryName").ToString() & "~" & dtPatientDetails.Rows(j)("lm_sGroupName").ToString() & "~" & dtPatientDetails.Rows(j)("lm_sTestName").ToString()
                                    C1ClinicalChart(row, COL_Provider) = objpatientexam.GetProvidernameforExam(CType(dtPatientDetails.Rows(j)("lm_Provider_id"), Int64))    '"Provider Name" 'dtPatientDetails.Rows(j)("ProviderName").ToString()
                                    C1ClinicalChart(row, COL_Date) = dtPatientDetails.Rows(j)("lm_OrderDate").ToString()




                                    C1ClinicalChart(row, COL_Four) = "6"
                                    setCellcheck(row, dtPatientDetails.Rows(j)("lm_Order_ID").ToString())

                                    j = j + 1
                                    '   C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
                                Next
                                If (IsNothing(dtPatientDetails) = False) Then
                                    dtPatientDetails.Dispose()
                                    dtPatientDetails = Nothing
                                End If
                                objpatientexam.Dispose()
                                objpatientexam = Nothing
                            Case enumDefaultClinicalChart.Referral_Letters.ToString()


                                level = 0
                                C1ClinicalChart.Rows.InsertNode(row, level)
                                C1ClinicalChart(row, COL_Category) = "Referral Letters"
                                ' C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
                                setCellcheck(row, "Referral Letters")
                                C1ClinicalChart(row, COL_Four) = 0
                                row = row + 1
                                level = 1

                                dtPatientDetails = Fill_ClinicalChartInfo(_PatientID, 7, mskStartDate.Text, mskEndDate.Text, dtDOS)
                                j = 0
                                For row = row To row + dtPatientDetails.Rows.Count - 1
                                    C1ClinicalChart.Rows.InsertNode(row, level)
                                    C1ClinicalChart(row, COL_Check) = dtPatientDetails.Rows(j)("nReferralID").ToString()
                                    C1ClinicalChart(row, COL_Category) = dtPatientDetails.Rows(j)("sTemplatename").ToString()
                                    C1ClinicalChart(row, COL_Provider) = dtPatientDetails.Rows(j)("ProviderName").ToString() 'objpatientexam.GetProvidernameforExam(CType(dtPatientDetails.Rows(j)("nProviderID"), Int64))    '"Provider Name" 'dtPatientDetails.Rows(j)("ProviderName").ToString()
                                    C1ClinicalChart(row, COL_Date) = dtPatientDetails.Rows(j)("dtReferralDate").ToString()
                                    C1ClinicalChart(row, COL_Four) = 7
                                    setCellcheck(row, dtPatientDetails.Rows(j)("nReferralID").ToString())

                                    j = j + 1
                                    '  C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
                                Next
                                If (IsNothing(dtPatientDetails) = False) Then
                                    dtPatientDetails.Dispose()
                                    dtPatientDetails = Nothing
                                End If
                            Case enumDefaultClinicalChart.Messages.ToString()

                                level = 0
                                C1ClinicalChart.Rows.InsertNode(row, level)
                                C1ClinicalChart(row, COL_Category) = "Messages"
                                C1ClinicalChart(row, COL_Four) = 0
                                '  C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
                                setCellcheck(row, "Messages")
                                row = row + 1
                                level = 1

                                dtPatientDetails = Fill_ClinicalChartInfo(_PatientID, 8, mskStartDate.Text, mskEndDate.Text, dtDOS)
                                j = 0
                                For row = row To row + dtPatientDetails.Rows.Count - 1
                                    C1ClinicalChart.Rows.InsertNode(row, level)
                                    C1ClinicalChart(row, COL_Check) = dtPatientDetails.Rows(j)("nMessageID").ToString()
                                    C1ClinicalChart(row, COL_Category) = dtPatientDetails.Rows(j)("sTemplateName").ToString()
                                    'C1ClinicalChart(row, COL_Provider) = dtPatientDetails.Rows(j)("ProviderName").ToString() 'objpatientexam.GetProvidernameforExam(CType(dtPatientDetails.Rows(j)("nProviderID"), Int64))    '"Provider Name" 'dtPatientDetails.Rows(j)("ProviderName").ToString()
                                    C1ClinicalChart(row, COL_Date) = System.Convert.ToDateTime(dtPatientDetails.Rows(j)("Date").ToString()) '.ToShortDateString()
                                    C1ClinicalChart(row, COL_Four) = 8
                                    setCellcheck(row, dtPatientDetails.Rows(j)("nMessageID").ToString())

                                    j = j + 1
                                    'C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
                                Next
                                If (IsNothing(dtPatientDetails) = False) Then
                                    dtPatientDetails.Dispose()
                                    dtPatientDetails = Nothing
                                End If
                            Case enumDefaultClinicalChart.Nurse_Notes.ToString()

                                level = 0
                                C1ClinicalChart.Rows.InsertNode(row, level)
                                C1ClinicalChart(row, COL_Category) = "Nurse Notes"
                                C1ClinicalChart(row, COL_Four) = 0
                                setCellcheck(row, "Nurse Notes")
                                ' C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
                                row = row + 1
                                level = 1

                                dtPatientDetails = Fill_ClinicalChartInfo(_PatientID, 9, mskStartDate.Text, mskEndDate.Text, dtDOS)
                                j = 0
                                For row = row To row + dtPatientDetails.Rows.Count - 1
                                    C1ClinicalChart.Rows.InsertNode(row, level)
                                    C1ClinicalChart(row, COL_Check) = dtPatientDetails.Rows(j)("nNurseNotesID").ToString()
                                    C1ClinicalChart(row, COL_Category) = dtPatientDetails.Rows(j)("sTemplateName").ToString()
                                    'C1ClinicalChart(row, COL_Provider) = dtPatientDetails.Rows(j)("ProviderName").ToString() 'objpatientexam.GetProvidernameforExam(CType(dtPatientDetails.Rows(j)("nProviderID"), Int64))    '"Provider Name" 'dtPatientDetails.Rows(j)("ProviderName").ToString()
                                    C1ClinicalChart(row, COL_Date) = dtPatientDetails.Rows(j)("dtNotesDate").ToString()
                                    C1ClinicalChart(row, COL_Four) = 9
                                    setCellcheck(row, dtPatientDetails.Rows(j)("nNurseNotesID").ToString())

                                    j = j + 1
                                    '  C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
                                Next
                                If (IsNothing(dtPatientDetails) = False) Then
                                    dtPatientDetails.Dispose()
                                    dtPatientDetails = Nothing
                                End If
                            Case enumDefaultClinicalChart.Triage.ToString()

                                level = 0
                                C1ClinicalChart.Rows.InsertNode(row, level)
                                C1ClinicalChart(row, COL_Category) = "Triage"
                                C1ClinicalChart(row, COL_Four) = 0
                                '  C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
                                setCellcheck(row, "Triage")
                                row = row + 1
                                level = 1
                                dtPatientDetails = Fill_ClinicalChartInfo(_PatientID, 10, mskStartDate.Text, mskEndDate.Text, dtDOS)
                                j = 0
                                For row = row To row + dtPatientDetails.Rows.Count - 1
                                    C1ClinicalChart.Rows.InsertNode(row, level)
                                    C1ClinicalChart(row, COL_Check) = dtPatientDetails.Rows(j)("nTriageID").ToString()
                                    C1ClinicalChart(row, COL_Category) = dtPatientDetails.Rows(j)("sTemplateName").ToString()
                                    'C1ClinicalChart(row, COL_Provider) = dtPatientDetails.Rows(j)("ProviderName").ToString() 'objpatientexam.GetProvidernameforExam(CType(dtPatientDetails.Rows(j)("nProviderID"), Int64))    '"Provider Name" 'dtPatientDetails.Rows(j)("ProviderName").ToString()
                                    C1ClinicalChart(row, COL_Date) = dtPatientDetails.Rows(j)("dtDate").ToString()
                                    C1ClinicalChart(row, COL_Four) = 10
                                    setCellcheck(row, dtPatientDetails.Rows(j)("nTriageID").ToString())

                                    j = j + 1
                                    'C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
                                Next
                                If (IsNothing(dtPatientDetails) = False) Then
                                    dtPatientDetails.Dispose()
                                    dtPatientDetails = Nothing
                                End If
                            Case enumDefaultClinicalChart.PT_Protocol.ToString()
                                level = 0
                                C1ClinicalChart.Rows.InsertNode(row, level)
                                C1ClinicalChart(row, COL_Category) = "PT Protocol"
                                C1ClinicalChart(row, COL_Four) = 0
                                ' C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
                                setCellcheck(row, "PT Protocol")
                                row = row + 1
                                level = 1
                                dtPatientDetails = Fill_ClinicalChartInfo(_PatientID, 11, mskStartDate.Text, mskEndDate.Text, dtDOS)
                                j = 0
                                For row = row To row + dtPatientDetails.Rows.Count - 1
                                    C1ClinicalChart.Rows.InsertNode(row, level)
                                    C1ClinicalChart(row, COL_Check) = dtPatientDetails.Rows(j)("nProtocolID").ToString()
                                    C1ClinicalChart(row, COL_Category) = dtPatientDetails.Rows(j)("sTemplateName").ToString()
                                    'C1ClinicalChart(row, COL_Provider) = dtPatientDetails.Rows(j)("ProviderName").ToString() 'objpatientexam.GetProvidernameforExam(CType(dtPatientDetails.Rows(j)("nProviderID"), Int64))    '"Provider Name" 'dtPatientDetails.Rows(j)("ProviderName").ToString()
                                    C1ClinicalChart(row, COL_Date) = dtPatientDetails.Rows(j)("dtProtocoldate").ToString()
                                    C1ClinicalChart(row, COL_Four) = 11
                                    setCellcheck(row, dtPatientDetails.Rows(j)("nProtocolID").ToString())

                                    j = j + 1
                                    '  C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
                                Next
                                If (IsNothing(dtPatientDetails) = False) Then
                                    dtPatientDetails.Dispose()
                                    dtPatientDetails = Nothing
                                End If
                            Case enumDefaultClinicalChart.Patient_Consent.ToString()

                                level = 0
                                C1ClinicalChart.Rows.InsertNode(row, level)
                                C1ClinicalChart(row, COL_Category) = "Patient Consent"
                                C1ClinicalChart(row, COL_Four) = 0
                                ' C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
                                setCellcheck(row, "Patient Consent")
                                row = row + 1
                                level = 1

                                dtPatientDetails = Fill_ClinicalChartInfo(_PatientID, 12, mskStartDate.Text, mskEndDate.Text, dtDOS)
                                j = 0
                                For row = row To row + dtPatientDetails.Rows.Count - 1
                                    C1ClinicalChart.Rows.InsertNode(row, level)
                                    C1ClinicalChart(row, COL_Check) = dtPatientDetails.Rows(j)("nConsentID").ToString()
                                    C1ClinicalChart(row, COL_Category) = dtPatientDetails.Rows(j)("sTemplateName").ToString()
                                    'C1ClinicalChart(row, COL_Provider) = dtPatientDetails.Rows(j)("ProviderName").ToString() 'objpatientexam.GetProvidernameforExam(CType(dtPatientDetails.Rows(j)("nProviderID"), Int64))    '"Provider Name" 'dtPatientDetails.Rows(j)("ProviderName").ToString()
                                    C1ClinicalChart(row, COL_Date) = dtPatientDetails.Rows(j)("dtConsentDate").ToString()
                                    C1ClinicalChart(row, COL_Four) = 12
                                    setCellcheck(row, dtPatientDetails.Rows(j)("nConsentID").ToString())

                                    j = j + 1
                                    '  C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
                                Next
                                If (IsNothing(dtPatientDetails) = False) Then
                                    dtPatientDetails.Dispose()
                                    dtPatientDetails = Nothing
                                End If
                            Case enumDefaultClinicalChart.Disclosure_Management.ToString()

                                level = 0
                                C1ClinicalChart.Rows.InsertNode(row, level)
                                C1ClinicalChart(row, COL_Category) = "Disclosure Management"
                                C1ClinicalChart(row, COL_Four) = 0
                                ' C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
                                setCellcheck(row, "Disclosure Management")
                                row = row + 1
                                level = 1


                                dtPatientDetails = Fill_ClinicalChartInfo(_PatientID, 13, mskStartDate.Text, mskEndDate.Text, dtDOS)
                                j = 0
                                For row = row To row + dtPatientDetails.Rows.Count - 1
                                    C1ClinicalChart.Rows.InsertNode(row, level)
                                    C1ClinicalChart(row, COL_Check) = dtPatientDetails.Rows(j)("nDisclosureID").ToString()
                                    C1ClinicalChart(row, COL_Category) = dtPatientDetails.Rows(j)("sTemplateName").ToString()
                                    'C1ClinicalChart(row, COL_Provider) = dtPatientDetails.Rows(j)("ProviderName").ToString() 'objpatientexam.GetProvidernameforExam(CType(dtPatientDetails.Rows(j)("nProviderID"), Int64))    '"Provider Name" 'dtPatientDetails.Rows(j)("ProviderName").ToString()
                                    C1ClinicalChart(row, COL_Date) = dtPatientDetails.Rows(j)("dtDisclosureDate").ToString()
                                    C1ClinicalChart(row, COL_Four) = 13
                                    setCellcheck(row, dtPatientDetails.Rows(j)("nDisclosureID").ToString())
                                    j = j + 1
                                    '   C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
                                Next
                                If (IsNothing(dtPatientDetails) = False) Then
                                    dtPatientDetails.Dispose()
                                    dtPatientDetails = Nothing
                                End If
                            Case enumDefaultClinicalChart.Patient_Letters.ToString()

                                level = 0
                                C1ClinicalChart.Rows.InsertNode(row, level)
                                C1ClinicalChart(row, COL_Category) = "Patient Letters"
                                C1ClinicalChart(row, COL_Four) = 0
                                'C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
                                setCellcheck(row, "Patient Letters")
                                row = row + 1
                                level = 1

                                dtPatientDetails = Fill_ClinicalChartInfo(_PatientID, 14, mskStartDate.Text, mskEndDate.Text, dtDOS)
                                j = 0
                                For row = row To row + dtPatientDetails.Rows.Count - 1
                                    C1ClinicalChart.Rows.InsertNode(row, level)
                                    C1ClinicalChart(row, COL_Check) = dtPatientDetails.Rows(j)("nLetterID").ToString()
                                    C1ClinicalChart(row, COL_Category) = dtPatientDetails.Rows(j)("sTemplateName").ToString()
                                    'C1ClinicalChart(row, COL_Provider) = dtPatientDetails.Rows(j)("ProviderName").ToString() 'objpatientexam.GetProvidernameforExam(CType(dtPatientDetails.Rows(j)("nProviderID"), Int64))    '"Provider Name" 'dtPatientDetails.Rows(j)("ProviderName").ToString()
                                    C1ClinicalChart(row, COL_Date) = dtPatientDetails.Rows(j)("dtLetterDate").ToString()
                                    C1ClinicalChart(row, COL_Four) = 14
                                    setCellcheck(row, dtPatientDetails.Rows(j)("nLetterID").ToString())

                                    j = j + 1
                                    '  C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
                                Next
                                If (IsNothing(dtPatientDetails) = False) Then
                                    dtPatientDetails.Dispose()
                                    dtPatientDetails = Nothing
                                End If

                            Case enumDefaultClinicalChart.Form_Gallery.ToString()

                                level = 0
                                C1ClinicalChart.Rows.InsertNode(row, level)
                                C1ClinicalChart(row, COL_Category) = "Form Gallery"
                                C1ClinicalChart(row, COL_Four) = 0
                                '   C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
                                setCellcheck(row, "Form Gallery")
                                row = row + 1
                                level = 1
                                dtPatientDetails = Fill_ClinicalChartInfo(_PatientID, 15, mskStartDate.Text, mskEndDate.Text, dtDOS)
                                j = 0
                                For row = row To row + dtPatientDetails.Rows.Count - 1
                                    C1ClinicalChart.Rows.InsertNode(row, level)
                                    C1ClinicalChart(row, COL_Check) = dtPatientDetails.Rows(j)("nFormID").ToString()
                                    C1ClinicalChart(row, COL_Category) = dtPatientDetails.Rows(j)("sTemplateName").ToString()
                                    'C1ClinicalChart(row, COL_Provider) = dtPatientDetails.Rows(j)("ProviderName").ToString() 'objpatientexam.GetProvidernameforExam(CType(dtPatientDetails.Rows(j)("nProviderID"), Int64))    '"Provider Name" 'dtPatientDetails.Rows(j)("ProviderName").ToString()
                                    C1ClinicalChart(row, COL_Date) = dtPatientDetails.Rows(j)("dtVisitDate").ToString()
                                    C1ClinicalChart(row, COL_Four) = 15
                                    setCellcheck(row, dtPatientDetails.Rows(j)("nFormID").ToString())

                                    j = j + 1
                                    ' C1ClinicalChart.SetCellCheck(row, 1, CheckEnum.Unchecked)
                                Next
                                'C1ClinicalChart.AutoSizeCols()
                                If (IsNothing(dtPatientDetails) = False) Then
                                    dtPatientDetails.Dispose()
                                    dtPatientDetails = Nothing
                                End If

                            Case enumDefaultClinicalChart.Claims.ToString()
                                Dim dsClaimDetails As New DataSet()
                                Dim dtPaymentDetails As DataTable = Nothing
                                Dim dtClaimDetails As DataTable = Nothing
                                Dim dtDistinctClaim As DataTable = Nothing
                                Dim nClaimRow As Int32 = 0
                                Dim nPaymentRow As Int32 = 0
                                Dim nHistoryRow As Int32 = 0
                                level = 0
                                C1ClinicalChart.Rows.InsertNode(row, level)
                                C1ClinicalChart(row, COL_Category) = "Claims"
                                C1ClinicalChart(row, COL_Four) = 0
                                setCellcheck(row, "Claims")

                                row = row + 1
                                level = 1

                                dsClaimDetails = Fill_ClinicalChartInfoDS(_PatientID, mskStartDate.Text, mskEndDate.Text)
                                If dsClaimDetails.Tables.Count = 3 Then
                                    dtDistinctClaim = dsClaimDetails.Tables(0)
                                    dtClaimDetails = dsClaimDetails.Tables(1)
                                    dtPaymentDetails = dsClaimDetails.Tables(2)

                                    Dim rowCount As Integer = row
                                    Dim sDate As String = String.Empty
                                    Dim sContactID As String = String.Empty
                                    Dim ClaimKey As String = String.Empty
                                    Dim TransactionID As String = String.Empty
                                    Dim sDOS As String = String.Empty
                                    Dim nRowCount As Int32 = 0
                                    Dim dtDateTime As DateTime
                                    Dim enumClaimRows As IEnumerable(Of DataRow) = Nothing
                                    Dim enumPaymentRows As IEnumerable(Of DataRow) = Nothing

                                    nRowCount = row
                                    For Each DataRow As DataRow In dtDistinctClaim.Rows
                                        sDate = String.Empty
                                        sContactID = String.Empty
                                        ClaimKey = String.Empty
                                        TransactionID = String.Empty
                                        sDOS = String.Empty
                                        nRowCount = C1ClinicalChart.Rows.Count

                                        Dim str_nTransactionMasterID As String = System.Convert.ToString(DataRow("nTransactionMasterID")).ToLower()
                                        If dtDOS <> "" Then
                                            enumClaimRows = From element As DataRow In dtClaimDetails.AsEnumerable()
                                                              Where (System.Convert.ToString(element("nTransactionMasterID")).ToLower() = str_nTransactionMasterID AndAlso System.Convert.ToString(element("DOS")).ToLower() = dtDOS)
                                                              Select element Order By System.Convert.ToDateTime(element("dtCreateddate")) Descending
                                        Else
                                            enumClaimRows = From element As DataRow In dtClaimDetails.AsEnumerable()
                                                              Where System.Convert.ToString(element("nTransactionMasterID")).ToLower() = str_nTransactionMasterID
                                                              Select element Order By System.Convert.ToDateTime(element("dtCreateddate")) Descending
                                        End If

                                        If Not IsNothing(enumClaimRows) AndAlso enumClaimRows.Count > 0 Then
                                            C1ClinicalChart.Rows.InsertNode(nRowCount, level + 1)
                                            C1ClinicalChart(nRowCount, COL_Check) = str_nTransactionMasterID
                                            C1ClinicalChart(nRowCount, COL_Category) = System.Convert.ToString(DataRow("displayClaimno"))
                                            C1ClinicalChart(nRowCount, COL_Date) = ""
                                            C1ClinicalChart(nRowCount, COL_Four) = ""
                                            C1ClinicalChart(nRowCount, COL_Five) = ""
                                            C1ClinicalChart(nRowCount, COL_Six) = ""
                                            C1ClinicalChart(nRowCount, COL_Seven) = ""
                                            C1ClinicalChart.Rows(nRowCount).UserData = "Claim"
                                            C1ClinicalChart.SetCellCheck(nRowCount, 1, CheckEnum.Unchecked)
                                            setCellcheck(nRowCount, System.Convert.ToString(DataRow("displayClaimno")))

                                            nRowCount = C1ClinicalChart.Rows.Count
                                            C1ClinicalChart.Rows.InsertNode(nRowCount, level + 2)
                                            C1ClinicalChart(nRowCount, COL_Check) = System.Convert.ToString(DataRow("nTransactionMasterID"))
                                            C1ClinicalChart(nRowCount, COL_Category) = "Claim Details"
                                            C1ClinicalChart(nRowCount, COL_Date) = ""
                                            C1ClinicalChart(nRowCount, COL_Four) = ""
                                            C1ClinicalChart(nRowCount, COL_Five) = ""
                                            C1ClinicalChart(nRowCount, COL_Six) = ""
                                            C1ClinicalChart(nRowCount, COL_Seven) = ""
                                            C1ClinicalChart.Rows(nRowCount).UserData = "Claim Details"
                                            setCellcheck(nRowCount, "Claim Details")
                                            nClaimRow = nRowCount + 1
                                            For Each Element As DataRow In enumClaimRows
                                                sDOS = System.Convert.ToString(Element("DOS"))
                                                If DateTime.TryParse(sDOS, dtDateTime) Then
                                                    sDate = dtDateTime.ToString("MM/dd/yyyy")
                                                End If
                                                C1ClinicalChart.Rows.InsertNode(nClaimRow, level + 3)
                                                C1ClinicalChart(nClaimRow, COL_Check) = System.Convert.ToString(Element("nTransactionMasterID"))
                                                C1ClinicalChart(nClaimRow, COL_Category) = System.Convert.ToString(Element("nClaimNo"))
                                                C1ClinicalChart(nClaimRow, COL_Date) = sDate
                                                sDate = String.Empty
                                                C1ClinicalChart(nClaimRow, COL_Four) = 16
                                                C1ClinicalChart(nClaimRow, COL_Five) = System.Convert.ToString(Element("ClaimType"))
                                                C1ClinicalChart(nClaimRow, COL_Six) = System.Convert.ToString(Element("nContactID"))
                                                C1ClinicalChart(nClaimRow, COL_Seven) = System.Convert.ToString(Element("nTransactionID"))
                                                C1ClinicalChart.SetCellCheck(nClaimRow, 1, CheckEnum.Unchecked)
                                                C1ClinicalChart.Rows(nClaimRow).UserData = "Individual Claim"
                                                setCellcheck(nClaimRow, System.Convert.ToString(Element("nTransactionID")), gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.ClaimDetails)
                                                nClaimRow = nClaimRow + 1
                                            Next

                                            enumPaymentRows = From element As DataRow In dtPaymentDetails.AsEnumerable()
                                                                 Where System.Convert.ToString(element("nTransactionMasterID")).ToLower() = str_nTransactionMasterID
                                                                 Select element Order By System.Convert.ToDateTime(element("dtCreatedDateTime")) Descending

                                            If Not IsNothing(enumPaymentRows) AndAlso enumPaymentRows.Count > 0 Then
                                                nRowCount = nClaimRow
                                                C1ClinicalChart.Rows.InsertNode(nRowCount, level + 2)
                                                C1ClinicalChart(nRowCount, COL_Check) = System.Convert.ToString(DataRow("nTransactionMasterID"))
                                                C1ClinicalChart(nRowCount, COL_Category) = "Payment Details"
                                                C1ClinicalChart(nRowCount, COL_Date) = ""
                                                C1ClinicalChart(nRowCount, COL_Four) = ""
                                                C1ClinicalChart(nRowCount, COL_Five) = ""
                                                C1ClinicalChart(nRowCount, COL_Six) = ""
                                                C1ClinicalChart(nRowCount, COL_Seven) = ""
                                                C1ClinicalChart.Rows(nClaimRow).UserData = "Payment Details"
                                                setCellcheck(nRowCount, "Payment Details")
                                                nPaymentRow = nRowCount + 1
                                                For Each Element As DataRow In enumPaymentRows
                                                    C1ClinicalChart.Rows.InsertNode(nPaymentRow, level + 3)
                                                    C1ClinicalChart(nPaymentRow, COL_Check) = System.Convert.ToString(Element("nCreditID"))
                                                    C1ClinicalChart(nPaymentRow, COL_Category) = System.Convert.ToString(Element("sDescription"))
                                                    C1ClinicalChart(nPaymentRow, COL_Four) = 17
                                                    C1ClinicalChart(nPaymentRow, COL_Five) = System.Convert.ToString(Element("nEOBID"))
                                                    C1ClinicalChart(nPaymentRow, COL_Six) = System.Convert.ToString(Element("nContactID"))
                                                    C1ClinicalChart.SetCellCheck(nPaymentRow, 1, CheckEnum.Unchecked)
                                                    C1ClinicalChart.Rows(nPaymentRow).UserData = "Individual Payment"
                                                    setCellcheck(nPaymentRow, System.Convert.ToString(Element("nCreditID")))
                                                    nPaymentRow = nPaymentRow + 1
                                                Next
                                            End If

                                            If Not IsNothing(enumPaymentRows) And enumPaymentRows.Count > 0 Then
                                                nHistoryRow = nPaymentRow + 1
                                            ElseIf (Not IsNothing(enumClaimRows) And enumClaimRows.Count > 0) Then
                                                nHistoryRow = nClaimRow + 1
                                            End If

                                            Dim enumClaimHistoryRows
                                            If dtDOS <> "" Then
                                                enumClaimHistoryRows = (From q In
                                                         (From element As DataRow In dtClaimDetails.AsEnumerable()
                                                          Where (System.Convert.ToString(element("nTransactionMasterID")).ToLower() = str_nTransactionMasterID AndAlso System.Convert.ToString(element("DOS")).ToLower() = dtDOS)
                                                          Select New With {.DistinctClaimKey = element.Item("ClaimKey"),
                                                                           .LatestTransactionId = element.Item("LatestTransactionID")}).AsEnumerable()
                                                          Select q.DistinctClaimKey, q.LatestTransactionId Distinct).ToList
                                            Else
                                                enumClaimHistoryRows = (From q In
                                                        (From element As DataRow In dtClaimDetails.AsEnumerable()
                                                         Where (System.Convert.ToString(element("nTransactionMasterID")).ToLower() = str_nTransactionMasterID)
                                                         Select New With {.DistinctClaimKey = element.Item("ClaimKey"),
                                                                          .LatestTransactionId = element.Item("LatestTransactionID")}).AsEnumerable()
                                                         Select q.DistinctClaimKey, q.LatestTransactionId Distinct).ToList
                                            End If

                                            If Not IsNothing(enumClaimRows) AndAlso enumClaimRows.Count > 0 Then
                                                If Not IsNothing(enumPaymentRows) And enumPaymentRows.Count > 0 Then
                                                    nRowCount = nPaymentRow
                                                ElseIf (Not IsNothing(enumClaimRows) And enumClaimRows.Count > 0) Then
                                                    nRowCount = nClaimRow
                                                End If

                                                C1ClinicalChart.Rows.InsertNode(nRowCount, level + 2)
                                                C1ClinicalChart(nRowCount, COL_Check) = System.Convert.ToString(DataRow("nTransactionMasterID"))
                                                C1ClinicalChart(nRowCount, COL_Category) = "Claim History"
                                                C1ClinicalChart(nRowCount, COL_Date) = ""
                                                C1ClinicalChart(nRowCount, COL_Four) = ""
                                                C1ClinicalChart(nRowCount, COL_Five) = ""
                                                C1ClinicalChart(nRowCount, COL_Six) = ""
                                                C1ClinicalChart(nRowCount, COL_Seven) = ""
                                                C1ClinicalChart.Rows(nRowCount).UserData = "Claim History"
                                                setCellcheck(nRowCount, "Claim History")
                                                nHistoryRow = nRowCount + 1

                                                If Not IsNothing(enumClaimHistoryRows) Then
                                                    For roCount As Integer = 0 To enumClaimHistoryRows.Count - 1 Step 1
                                                        C1ClinicalChart.Rows.InsertNode(nHistoryRow, level + 3)
                                                        C1ClinicalChart(nHistoryRow, COL_Check) = System.Convert.ToString(DataRow("nTransactionMasterID"))
                                                        C1ClinicalChart(nHistoryRow, COL_Category) = "Claim#" + System.Convert.ToString(enumClaimHistoryRows(roCount).DistinctClaimKey)
                                                        C1ClinicalChart(nHistoryRow, COL_Date) = sDate
                                                        sDate = String.Empty
                                                        C1ClinicalChart(nHistoryRow, COL_Four) = 18
                                                        C1ClinicalChart(nHistoryRow, COL_Five) = ""
                                                        C1ClinicalChart(nHistoryRow, COL_Six) = ""
                                                        C1ClinicalChart(nHistoryRow, COL_Seven) = System.Convert.ToString(enumClaimHistoryRows(roCount).LatestTransactionId)
                                                        C1ClinicalChart.SetCellCheck(nHistoryRow, 1, CheckEnum.Unchecked)
                                                        C1ClinicalChart.Rows(nHistoryRow).UserData = "Individual History"
                                                        setCellcheck(nHistoryRow, System.Convert.ToString(enumClaimHistoryRows(roCount).LatestTransactionId), gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.ClaimHistory)
                                                        nHistoryRow = nHistoryRow + 1
                                                    Next

                                                End If




                                                'For Each Element As DataRow In enumClaimHistoryRows
                                                '    sDOS = System.Convert.ToString(Element("DOS"))
                                                '    If DateTime.TryParse(sDOS, dtDateTime) Then
                                                '        sDate = dtDateTime.ToString("MM/dd/yyyy")
                                                '    End If
                                                '    C1ClinicalChart.Rows.InsertNode(nHistoryRow, level + 3)
                                                '    C1ClinicalChart(nHistoryRow, COL_Check) = System.Convert.ToString(Element("nTransactionMasterID"))
                                                '    C1ClinicalChart(nHistoryRow, COL_Category) = "Claim#" + System.Convert.ToString(Element("ClaimKey"))
                                                '    C1ClinicalChart(nHistoryRow, COL_Date) = sDate
                                                '    sDate = String.Empty
                                                '    C1ClinicalChart(nHistoryRow, COL_Four) = 18
                                                '    C1ClinicalChart(nHistoryRow, COL_Five) = System.Convert.ToString(Element("ClaimType"))
                                                '    C1ClinicalChart(nHistoryRow, COL_Six) = System.Convert.ToString(Element("nContactID"))
                                                '    C1ClinicalChart(nHistoryRow, COL_Seven) = System.Convert.ToString(Element("LatestTransactionID"))
                                                '    C1ClinicalChart.SetCellCheck(nHistoryRow, 1, CheckEnum.Unchecked)
                                                '    C1ClinicalChart.Rows(nHistoryRow).UserData = "Individual History"
                                                '    setCellcheck(nHistoryRow, System.Convert.ToString(Element("nTransactionID")), gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.ClaimHistory)
                                                '    nHistoryRow = nHistoryRow + 1
                                                'Next
                                            End If

                                        End If
                                    Next

                                    If dtDistinctClaim.Rows.Count > 0 Then
                                        If (Not IsNothing(enumClaimRows) And enumClaimRows.Count > 0) Then
                                            row = nHistoryRow
                                        Else
                                            row = nRowCount
                                        End If
                                    End If

                                    enumClaimRows = Nothing
                                    enumPaymentRows = Nothing
                                    If dsClaimDetails IsNot Nothing Then
                                        dsClaimDetails.Dispose()
                                        dsClaimDetails = Nothing
                                    End If
                                    ''  dtDistinctClaim = Nothing

                                    dtPaymentDetails = Nothing
                                    dtClaimDetails = Nothing
                                End If


                            Case enumDefaultClinicalChart.Patient_Forms.ToString()

                                level = 0
                                'row = C1ClinicalChart.Rows.Count
                                C1ClinicalChart.Rows.InsertNode(row, level)
                                C1ClinicalChart(row, COL_Category) = "Patient Forms"
                                C1ClinicalChart(row, COL_Four) = 0
                                setCellcheck(row, "Patient Forms")
                                row = row + 1
                                level = 1
                                dtPatientDetails = Fill_ClinicalChartInfo(_PatientID, 19, mskStartDate.Text, mskEndDate.Text, dtDOS)
                                j = 0
                                For row = row To row + dtPatientDetails.Rows.Count - 1
                                    C1ClinicalChart.Rows.InsertNode(row, level)
                                    C1ClinicalChart(row, COL_Check) = dtPatientDetails.Rows(j)("nFormID").ToString()
                                    C1ClinicalChart(row, COL_Category) = dtPatientDetails.Rows(j)("sTemplateName").ToString()
                                    C1ClinicalChart(row, COL_Date) = dtPatientDetails.Rows(j)("dtVisitDate").ToString()
                                    C1ClinicalChart(row, COL_Four) = 19
                                    setCellcheck(row, dtPatientDetails.Rows(j)("nFormID").ToString())
                                    j = j + 1
                                Next

                                If (IsNothing(dtPatientDetails) = False) Then
                                    dtPatientDetails.Dispose()
                                    dtPatientDetails = Nothing
                                End If

                            Case enumDefaultClinicalChart.Patient_Education.ToString()

                                level = 0
                                'row = C1ClinicalChart.Rows.Count
                                C1ClinicalChart.Rows.InsertNode(row, level)
                                C1ClinicalChart(row, COL_Category) = "Patient Education"
                                C1ClinicalChart(row, COL_Four) = 0
                                setCellcheck(row, "Patient Education")
                                row = row + 1
                                level = 1
                                dtPatientDetails = Fill_ClinicalChartInfo(_PatientID, 21, mskStartDate.Text, mskEndDate.Text, dtDOS)
                                j = 0
                                For row = row To row + dtPatientDetails.Rows.Count - 1
                                    C1ClinicalChart.Rows.InsertNode(row, level)
                                    C1ClinicalChart(row, COL_Check) = dtPatientDetails.Rows(j)("nEducationID").ToString()
                                    C1ClinicalChart(row, COL_Category) = dtPatientDetails.Rows(j)("sTemplateName").ToString()
                                    C1ClinicalChart(row, COL_Date) = dtPatientDetails.Rows(j)("dtVisitDate").ToString()
                                    C1ClinicalChart(row, COL_Four) = 21
                                    C1ClinicalChart(row, COL_Five) = dtPatientDetails.Rows(j)("nVisitID").ToString()
                                    C1ClinicalChart(row, COL_Six) = dtPatientDetails.Rows(j)("sDocumentURL").ToString()
                                    C1ClinicalChart(row, COL_Seven) = dtPatientDetails.Rows(j)("NeedUpdate").ToString()
                                    setCellcheck(row, dtPatientDetails.Rows(j)("nEducationID").ToString())
                                    j = j + 1
                                Next

                                If (IsNothing(dtPatientDetails) = False) Then
                                    dtPatientDetails.Dispose()
                                    dtPatientDetails = Nothing
                                End If
                        End Select
                    End If

                End If
            Next

            If Not IsNothing(dtPatientDetails) Then
                dtPatientDetails.Dispose()
                dtPatientDetails = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
        isloading = False


    End Sub

    Private Sub setCellcheck(Row As Integer, sFieldName As String, Optional ByVal ndocType As Integer = 0)
        Try

            Dim result
            If (dtQueued IsNot Nothing) Then
                If (dtQueued.Rows.Count > 0) Then
                    If ndocType = gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.OrdersTestResult.GetHashCode() Then
                        Dim Pfound As Boolean = False
                        Dim nID As String = C1ClinicalChart.GetData(System.Convert.ToInt16(Row), 0).ToString()
                        Dim rIndex As Integer = 0
                        Dim OrderID As String = String.Empty
                        Dim OrderNumber As String = String.Empty
                        rIndex = Row
                        While (Pfound <> True)
                            If (C1ClinicalChart.Rows(rIndex)(4).ToString.Trim().Contains("L")) Then
                                Pfound = True
                                OrderID = System.Convert.ToInt64(C1ClinicalChart.GetData(rIndex, 0))
                                OrderNumber = System.Convert.ToString(C1ClinicalChart.GetData(rIndex, 1))
                            End If
                            rIndex = rIndex - 1
                        End While

                        result = (From m In dtQueued.AsEnumerable()
                                  Where m.Field(Of String)("sTranID_I") = nID And m.Field(Of String)("sTranID_II") = OrderID
                                  ).FirstOrDefault()
                    ElseIf sFieldName = "Claim Details" Or sFieldName = "Payment Details" Or sFieldName = "Claim History" Then
                        Dim nID As String = C1ClinicalChart.GetData(System.Convert.ToInt16(Row), 0).ToString()
                        result = (From m In dtQueued.AsEnumerable()
                                Where m.Field(Of String)("sTranID_I") = sFieldName And m.Field(Of String)("sTranID_II") = nID
                                ).FirstOrDefault()
                    Else
                        If ndocType = gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.ClaimHistory.GetHashCode() Then
                            result = (From m In dtQueued.AsEnumerable() Where m.Field(Of String)("sTranID_I") = sFieldName And m.Field(Of String)("sTranID_IV") = "").FirstOrDefault()
                        ElseIf ndocType = gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.ClaimDetails.GetHashCode() Then
                            result = (From m In dtQueued.AsEnumerable() Where m.Field(Of String)("sTranID_I") = sFieldName And m.Field(Of String)("sTranID_IV") <> "").FirstOrDefault()
                        Else
                            result = (From m In dtQueued.AsEnumerable() Where m.Field(Of String)("sTranID_I") = sFieldName).FirstOrDefault()
                        End If
                    End If
                    If result IsNot Nothing Then
                        C1ClinicalChart.SetCellCheck(Row, 1, CheckEnum.Checked)
                    Else
                        C1ClinicalChart.SetCellCheck(Row, 1, CheckEnum.Unchecked)
                    End If
                Else
                    C1ClinicalChart.SetCellCheck(Row, 1, CheckEnum.Unchecked)
                End If

            Else
                C1ClinicalChart.SetCellCheck(Row, 1, CheckEnum.Unchecked)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Public Function GetQueuedDocuments(nQueuedID As Int64) As DataTable
        Dim dtDocument As DataTable = Nothing
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Try
            oDB.Connect(False)
            Dim _strSqlQuery As String = "SELECT ISNULL(sTranID_I,'')  AS sTranID_I ,ISNULL(sTranID_II,'')  AS sTranID_II ,ISNULL(sTranID_III,'')  AS sTranID_III ,ISNULL(sTranID_IV,'')  AS sTranID_IV  FROM ClinicalChartQueueDetails where nQueueID = " & nQueuedID
            oDB.Retrive_Query(_strSqlQuery, dtDocument)
            If (dtDocument IsNot Nothing) Then
                If (dtDocument.Rows.Count > 0) Then
                    Return dtDocument
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If
        Catch ex As gloDatabaseLayer.DBException
            ex.ERROR_Log(ex.ToString())
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Return Nothing
        Finally
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()
            End If
        End Try
    End Function



    Public Sub setQueuedFormType(nQueuedID As Int64)
        Dim nformType As Integer = 0
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Try
            oDB.Connect(False)
            Dim _strSqlQuery As String = " SELECT ISNULL(dbo.ClinicalChartQueueMst.ClaimPrintType,0)  FROM ClinicalChartQueueMst WHERE dbo.ClinicalChartQueueMst.nQueueID=" & nQueuedID
            ''oDB.Retrive_Query(_strSqlQuery, dtDocument)
            nformType = oDB.ExecuteScalar_Query(_strSqlQuery)
            If nformType = ClaimPrintType.PrintForm.GetHashCode() Then
                rbPrintOnForm.Checked = True
            Else
                rbPrintData.Checked = True
            End If

        Catch ex As gloDatabaseLayer.DBException
            ex.ERROR_Log(ex.ToString())

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)

        Finally
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()
            End If
        End Try
    End Sub
    Public Function getClinicalChartSetting() As String
        Dim result As String = String.Empty
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Try
            oDB.Connect(False)
            Dim _strSqlQuery As String = "SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM Settings where sSettingsName='CLINICALCHARTCUSTOMIZATION'"

            result = oDB.ExecuteScalar_Query(_strSqlQuery)
        Catch ex As gloDatabaseLayer.DBException
            ex.ERROR_Log(ex.ToString())
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()
            End If
        End Try

        Return result

    End Function

    Private Sub dtpicStartDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If (isloading = False) Then
            'LoadClinicalChartlist()
            ArrangeGridItems()
        End If
    End Sub
    Sub SelectAll(ByVal isSelect As Boolean)
        For i As Int64 = 1 To C1ClinicalChart.Rows.Count - 1
            If (isSelect) Then
                C1ClinicalChart.SetCellCheck(i, 1, CheckEnum.Checked)
            Else
                C1ClinicalChart.SetCellCheck(i, 1, CheckEnum.Unchecked)
            End If

        Next
    End Sub
    Function IsHaveDocumenttoExport() As Boolean

        For i As Int64 = 1 To C1ClinicalChart.Rows.Count - 1
            ' Dim Flag1 As Int16 = System.Convert.ToInt16(C1ClinicalChart.GetData(System.Convert.ToInt16(i), 4).ToString())
            Dim Flag1 As String = C1ClinicalChart.GetData(System.Convert.ToInt16(i), 4).ToString()
            Dim isSelected As C1.Win.C1FlexGrid.CheckEnum
            isSelected = C1ClinicalChart.GetCellCheck(System.Convert.ToInt16(i), 1)
            If (Flag1 <> "0") AndAlso (Flag1.Contains("R") = False) AndAlso (isSelected = CheckEnum.Checked) Then

                If (Flag1 = "4") Then
                    Dim _dtLabdata As DataTable = GetLabPrintData(_PatientID, mskStartDate.Text, mskEndDate.Text)
                    If IsNothing(_dtLabdata) Then
                        Continue For
                    End If
                    If (_dtLabdata.Rows.Count = 0) Then
                        _dtLabdata.Dispose()
                        _dtLabdata = Nothing
                        Continue For
                    End If
                    _dtLabdata.Dispose()
                    _dtLabdata = Nothing

                    Return True
                Else
                    Return True
                End If

            End If

            Flag1 = Nothing
            isSelected = Nothing
        Next
        Return False
    End Function
    Private Function PrintPDFDocument(ByVal myString As String) As Boolean
        Dim myPDFdoc As PDFDoc = Nothing
        myPDFdoc = New PDFDoc(myString)
        myPDFdoc.InitSecurityHandler()

        Dim myPrintMode As PrinterMode = New PrinterMode()
        myPrintMode.SetCollation(True)
        myPrintMode.SetCopyCount(1)
        myPrintMode.SetDPI(300)     ' regardless of ordering, an explicit DPI setting overrides the OutputQuality setting
        myPrintMode.SetDuplexing(PrinterMode.DuplexMode.e_Duplex_Auto)
        myPrintMode.SetOutputColor(PrinterMode.OutputColor.e_OutputColor_Grayscale)
        myPrintMode.SetOutputQuality(PrinterMode.OutputQuality.e_OutputQuality_Medium)


        Dim myPagesToPrint As PageSet = New PageSet(1, myPDFdoc.GetPageCount(), PageSet.Filter.e_all)

        Dim PrinterName = ""
        If gblnUseDefaultPrinter Then
        Else

        End If
        '----------
        Dim objPrintDocument As New System.Drawing.Printing.PrintDocument
        Dim _sDefaultPrinter = objPrintDocument.PrinterSettings.PrinterName
        If gblnUseDefaultPrinter = True Then

            PrinterName = _sDefaultPrinter
            ' oTempDoc.Application.ActivePrinter = oTempDoc.Application.Dialogs(Microsoft.Office.Interop.Word.WdWordDialog.wdDialogFilePrint).
        Else
            Dim PrintDialog1 As PrintDialog = New PrintDialog()
            If PrintDialog1.ShowDialog(System.Windows.Forms.Form.ActiveForm) = Windows.Forms.DialogResult.OK Then
                PrinterName = PrintDialog1.PrinterSettings.PrinterName
            Else
                PrintDialog1.Dispose()
                myPDFdoc.Dispose()
                myPagesToPrint.Dispose()
                myPrintMode.Dispose()
                gloGlobal.cEventHelper.RemoveAllEventHandlers(objPrintDocument)
                objPrintDocument.Dispose()
                Return False
            End If
            PrintDialog1.Dispose()
        End If
        '----------

        Try
            Me.SuspendLayout()
            Print.StartPrintJob(myPDFdoc, PrinterName, myPDFdoc.GetFileName(), "", myPagesToPrint, myPrintMode)
            myPDFdoc.Dispose()
            myPagesToPrint.Dispose()
            myPrintMode.Dispose()
            gloGlobal.cEventHelper.RemoveAllEventHandlers(objPrintDocument)
            objPrintDocument.Dispose()
            Threading.Thread.Sleep(1000)
            Me.ResumeLayout()
        Catch ex As Exception
            Me.ResumeLayout()
            Return False
        End Try


        Return True
    End Function

    Private Sub dtpicEndDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If (isloading = False) Then
            'LoadClinicalChartlist()
            ArrangeGridItems()
        End If
    End Sub

    Public Sub ExportLabOrderReport(ByVal OrderID As Long, ByVal arrTests As ArrayList, ByVal strFilePath As String)
        'Create the object for report

        Dim oLabs As Rpt_LabOrder = Nothing
        'Dim oFrmViewLab As gloEmdeonInterface.Forms.frmViewgloLab()
        Try
            If OrderID <> 0 Then
                oLabs = CreateReport(OrderID, arrTests)

                oLabs.ExportToDisk(ExportFormatType.PortableDocFormat, strFilePath)
                oLabs.Dispose()
                oLabs = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub
    Public Function CreateReport(ByVal OrderID As Long, ByVal arrTests As ArrayList) As Rpt_LabOrder
        'Create the object for report
        Dim oLabs As New Rpt_LabOrder()
        'Create the object for dataset i.e dsgloEMRReports.xsd
        Dim dsReports As New dsgloEMRReports()
        ' Dim obj As New gloEmdeonInterface.Classes.clsGeneral()
        'gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
        Dim sConstr As New SqlConnection(GetConnectionString())
        Dim sCmd As New SqlCommand()
        Dim da As SqlDataAdapter
        Dim strQuery As String = String.Empty
        Try
            strQuery = " SELECT sClinicName, sAddress1, sAddress2, sStreet, sCity, sState, sZIP, sPhoneNo,sMobileNo, sFAX, sEmail, sURL, imgClinicLogo FROM  Clinic_MST where nclinicId = 1"
            sCmd.CommandText = strQuery
            sCmd.Connection = sConstr
            da = New SqlDataAdapter(sCmd)
            da.Fill(dsReports, "dt_Clinic_MST")
            da.Dispose()
            da = Nothing

            If OrderID <> 0 Then
                '#Region "Final Inserted Results in Order Report Query"

                Dim oDBLayer As New gloDatabaseLayer.DBLayer(GetConnectionString())
                Dim _strQryResultDateTime As String = String.Empty
                Dim _strResultDateTime As String = String.Empty
                Dim objResult As Object = Nothing
                Try

                    oDBLayer.Connect(False)
                    _strQryResultDateTime = " select top 1 dbo.Lab_Order_Test_Result.labotr_TestResultDateTime from dbo.Lab_Order_Test_Result WHERE Lab_Order_Test_Result.labotr_OrderID =  '" & OrderID & "' " & " order by dbo.Lab_Order_Test_Result.labotr_TestResultDateTime  desc "

                    objResult = oDBLayer.ExecuteScalar_Query(_strQryResultDateTime)
                    If objResult IsNot Nothing AndAlso objResult.ToString() <> "" Then
                        ' _strResultDateTime = Convert.ToString(objResult);
                        _strResultDateTime = System.Convert.ToDateTime(objResult).ToString("yyyy-MM-dd HH:mm:ss.fff")
                    End If
                    oDBLayer.Disconnect()
                Catch ex As Exception

                    gloAuditTrail.gloAuditTrail.ExceptionLog("" & ex.ToString(), False)
                Finally
                    If oDBLayer IsNot Nothing Then
                        oDBLayer.Dispose()
                    End If
                    If objResult IsNot Nothing Then
                        objResult = Nothing
                    End If
                    _strQryResultDateTime = String.Empty
                End Try

                'Start Change :  Qry by Sandip Deshmukh : 201005181554                     
                ' This has been to print final inserted Results in order .
                ''Bug #65404: 00000646: Collection Date missing while printing Lab from clinical chart. 
                ''Description: When printing labs from clinical chart section the collection date fields are not showing.
                ''Resolution: Added Column for dates missing in report.
                strQuery = ""
                strQuery = "SELECT Lab_Order_MST.labom_TransactionDate AS TransDate, " _
                                & "Lab_Order_MST.labom_OrderNoPrefix + '-' + CONVERT(varchar(100), " _
                                & "Lab_Order_MST.labom_OrderNoID) AS OrderNumber, " _
                                & " dbo.Lab_Order_Test_Result.labotr_SpecimenReceivedDateTime  AS SpecimenRecievedDate, " _
                                & " dbo.Lab_Order_Test_Result.labotr_ResultTransferDateTime AS ReportDate , " _
                                & " ISNULL(dbo.Lab_Order_MST.labom_ReceivingFacilityCode,'') AS ReceivingFacilityCode, " _
                                & " ISNULL(dbo.Lab_Order_TestDtl.labotd_Comment,'') As TestComments, " _
                                & " ISNULL(dbo.Lab_Order_MST.labom_LabComment,'') AS LabComment, " _
                                & " Lab_Order_MST.labom_CollectionDate AS LabMSTCollectionDate,  " _
                                & " ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_LOINCID,'') AS LoinicCode, " _
                                & " ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_AlternateResultName,'') AS AlternateResulttName, " _
                                & " (ISNULL(dbo.Lab_Order_MST.labom_ExternalCode,'')  + CASE ISNULL(dbo.Lab_Order_MST.labom_FileOrderIdentifier,'')  WHEN '' THEN '' ELSE ' (Req. # : ' +  dbo.Lab_Order_MST.labom_FileOrderIdentifier + ')' END) AS ExternalCode, " _
                                & "ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_ProducerIdentifier,'') AS ProducerIdentifier, " _
                                & "Lab_Order_TestDtl.labotd_Instruction AS Instruction, " _
                                & "Lab_Order_TestDtl.labotd_Precaution AS Precaution, " _
                                & "Lab_Order_TestDtl.labotd_DateTime AS TestDate, " _
                                & "CONVERT(varchar(100),Lab_Order_MST.labom_OrderID) AS OrderID, " _
                                & " Lab_Order_TestDtl.labotd_TestID AS TestID, " _
                                & " CONVERT(varchar(100), Lab_Order_MST.labom_PatientID) AS PatientID, " _
                                & "User_MST.sLoginName AS SampledBy, " _
                                & "contacts_mst.sName AS ReferredBy, " _
                                & "ISNULL(Provider_MST.sFirstName, '') + ' ' + ISNULL(Provider_MST.sMiddleName, '') + ' ' + ISNULL(Provider_MST.sLastName, '') AS Provider, " _
                                & "ISNULL(dbo.Lab_Order_Test_Result.labotr_TestResultNumber, 0) AS TestResultNumber, " _
                                & "ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_ResultLineNo,0) AS ResultLineNo, " _
                                & "case len(ISNUll(dbo.Lab_Order_Test_Result.labotr_TestResultName,'')) when 0 then '-' when null then '-' else dbo.Lab_Order_Test_Result.labotr_TestResultName END AS TestResultName, " _
                                & "dbo.Lab_Order_Test_Result.labotr_TestResultDateTime AS TestResultDateTime, " _
                                & "ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_ResultName, '') AS ResultName, " _
                                & "ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_ResultValue, '') AS ResultValue, " _
                                & "ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_ResultUnit, '') AS ResultUnit, " _
                                & "ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_ResultRange, '') AS ResultRange, " _
                                & "ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_ResultType, '') AS ResultType, " _
                                & "ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_AbnormalFlag, '') AS AbnormalFlag, " _
                                & "ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_ResultComment, '') AS ResultComment, " _
                                & "dbo.Lab_Order_Test_ResultDtl.labotrd_ResultDateTime AS ResultDateTime, " _
                                & "dbo.Lab_Order_TestDtl.labotd_TestName AS TestName, " _
                                & "dbo.Lab_Order_TestDtl.labotd_SpecimenName AS Speciman, " _
                                & "ISNULL(Lab_Collection_Mst.labcm_Name, '') AS CollectionContainer, " _
                                & "dbo.Lab_Order_TestDtl.labotd_StorageName AS StorageTemperature, " _
                                & "ISNULL(dbo.Lab_GetUTCTimeString(Lab_Order_MST.labom_TransactionDateUTC),'') AS labom_TransactionDateUTC," _
                                & "ISNULL(dbo.Lab_GetUTCTimeString(Lab_Order_TestDtl.labotd_DateTimeUTC),'') AS labotd_DateTimeUTC," _
                                & "ISNULL(Lab_Order_TestDtl.labotd_SpecimenTypeText,'') AS labotd_SpecimenTypeText, " _
                                & "Lab_Order_TestDtl.labotd_SpecimenCollectionStartDateTime AS labotd_SpecimenCollectionStartDateTime, " _
                                & "ISNULL(Lab_Order_TestDtl.labotd_SpecimenRejectReason,'') AS labotd_SpecimenRejectReason, " _
                                & "ISNULL(Lab_Order_TestDtl.labotd_SpecimenCondition,'') AS labotd_SpecimenCondition, " _
                                & "ISNULL(dbo.Lab_GetUTCTimeString(Lab_Order_TestDtl.labotd_SpecimenCollectionStartDateTimeUTC),'') AS labotd_SpecimenCollectionStartDateTimeUTC, " _
                                & "ISNULL(Lab_Order_Test_ResultDtl.labotrd_LabFacilityName,'') AS labotrd_LabFacilityName, " _
                                & "ISNULL(Lab_Order_Test_ResultDtl.labotrd_LabFacilityStreetAddress,'') AS labotrd_LabFacilityStreetAddress, " _
                                & "ISNULL(Lab_Order_Test_ResultDtl.labotrd_LabFacilityCity,'') AS labotrd_LabFacilityCity, " _
                                & "ISNULL(Lab_Order_Test_ResultDtl.labotrd_LabFacilityState,'') AS labotrd_LabFacilityState, " _
                                & "ISNULL(Lab_Order_Test_ResultDtl.labotrd_LabFacilityZipCode,'') AS labotrd_LabFacilityZipCode, " _
                                & "ISNULL(Lab_Order_Test_ResultDtl.labotrd_LabFacilityCountry,'') AS labotrd_LabFacilityCountry, " _
                                & "ISNULL(Lab_Order_Test_ResultDtl.labotrd_LabFacilityCountyOrParishCode ,'') AS labotrd_LabFacilityCountyOrParishCode, " _
                                & "ISNULL(dbo.Lab_GetUTCTimeString(Lab_Order_MST.labom_CollectionDateUTC),'') AS labom_CollectionDateUTC, " _
                                & "ISNULL(dbo.Lab_GetUTCTimeString(dbo.Lab_Order_Test_Result.labotr_ResultTransferDateTimeUTC),'') AS labotr_ResultTransferDateTimeUTC, " _
                                & "ISNULL(dbo.Lab_GetUTCTimeString(dbo.Lab_Order_Test_ResultDtl.labotrd_ResultDateTimeUTC),'') AS ResultDateTimeUTC, " _
                                & "dbo.Lab_Order_MST.labom_OrderDate AS OrderDate, " _
                                & "ISNULL(dbo.Lab_GetUTCTimeString(dbo.Lab_Order_MST.labom_OrderDateUTC),'') AS OrderDateUTC, " _
                                & "ISNULL(dbo.Lab_GetUTCTimeString(dbo.Lab_Order_Test_Result.labotr_SpecimenReceivedDateTimeUTC),'') AS SpecimenReceivedDateTimeUTC, " _
                                & "ISNULL(Lab_Order_Test_ResultDtl.labotrd_ResultParentChildFlag,0) AS ParentChildResultFlag " _
                                & "FROM Lab_Order_MST INNER JOIN " _
                                & "Lab_Order_TestDtl ON Lab_Order_MST.labom_OrderID = Lab_Order_TestDtl.labotd_OrderID LEFT OUTER JOIN " _
                                & "Lab_Order_Test_Result ON Lab_Order_TestDtl.labotd_TestID = Lab_Order_Test_Result.labotr_TestID AND " _
                                & " Lab_Order_TestDtl.labotd_OrderID = Lab_Order_Test_Result.labotr_OrderID LEFT OUTER JOIN " _
                                & "Lab_Order_Test_ResultDtl ON Lab_Order_TestDtl.labotd_OrderID = Lab_Order_Test_ResultDtl.labotrd_OrderID AND " _
                                & "Lab_Order_TestDtl.labotd_TestID = Lab_Order_Test_ResultDtl.labotrd_TestID AND " _
                                & " Lab_Order_Test_Result.labotr_TestResultNumber = Lab_Order_Test_ResultDtl.labotrd_TestResultNumber LEFT OUTER JOIN " _
                                & "User_MST ON Lab_Order_MST.labom_SampledByID = User_MST.nUserID LEFT OUTER JOIN " _
                                & "Provider_MST ON Lab_Order_MST.labom_ProviderID = Provider_MST.nProviderID LEFT OUTER JOIN " _
                                & "contacts_mst ON Lab_Order_MST.labom_ReferredByID = contacts_mst.nContactID LEFT OUTER JOIN " _
                                & "Lab_Specimen_Mst ON Lab_Order_TestDtl.labotd_SpecimenID = Lab_Specimen_Mst.labsm_ID LEFT OUTER JOIN " _
                                & "Lab_Collection_Mst ON Lab_Order_TestDtl.labotd_CollectionID = Lab_Collection_Mst.labcm_ID " _
                                & "WHERE Lab_Order_MST.labom_OrderID = " & OrderID

                If _strResultDateTime.Trim() <> "" Then

                    strQuery += " and Lab_Order_Test_Result.labotr_TestResultDateTime ='" & _strResultDateTime & "' "
                End If
                strQuery += " order by Lab_Order_TestDtl.labotd_LineNo"


                'End Qry by Sandip Deshmukh : 201005181554

                '#End Region
                sCmd.CommandText = strQuery
                da = New SqlDataAdapter(sCmd)
                da.Fill(dsReports, "dt_LabOrderMainReport")
                da.Dispose()
                da = Nothing
                strQuery = ""
                sCmd.CommandText = ""
                strQuery = " SELECT Patient.sPatientCode AS PatientCode,ISNULL(Patient.sFirstName,'') + SPACE(1) + ISNULL(Patient.sMiddleName,'')+ SPACE(1) + ISNULL(Patient.sLastName,'') AS PatientName,ISNULL(Patient.sGender,'') as Gender," _
                           & " ISNULL(Patient.SAddressLine1,'') + SPACE(1)+ ISNULL(Patient.sAddressLine2,'') AS PatientAddress,ISNULL(Patient.sPhone,'') AS PatientPhone," _
                           & " ISNULL(Patient.sCity,'') AS PatientCity,ISNULL(Patient.sState,'') AS PatientState,ISNULL(Patient.sZIP, '') AS PatientZip,ISNULL(Patient.sCounty,'') AS PatientCounty," _
                           & " Patient.dtDOB AS DateOfBirth, Lab_Order_MST.labom_PatientAgeYear AS AgeInYrs, " _
                           & " Lab_Order_MST.labom_PatientAgeMonth AS AgeInMnths, Lab_Order_MST.labom_PatientAgeDay AS AgeInDays, ISNULL(Provider_MST.sFirstName, '') " _
                           & " + ' ' + ISNULL(Provider_MST.sMiddleName, '') + ' ' + ISNULL(Provider_MST.sLastName, '') AS Provider, ISNULL(User_MST.sLoginName, '') " _
                           & " AS SampledBy, ISNULL(Contacts_MST.sFirstName, '') + ' ' + ISNULL(Contacts_MST.sMiddleName, '') + ' ' + ISNULL(Contacts_MST.sLastName, '') " _
                           & " AS ReferredBy, CONVERT(varchar(100), Lab_Order_MST.labom_OrderID) AS OrderID, Lab_Order_MST.labom_TransactionDate AS TransDate, " _
                           & " Lab_Order_MST.labom_OrderNoPrefix + ' ' + CONVERT(varchar(100), Lab_Order_MST.labom_OrderNoID) AS OrderNumber" _
                           & " FROM Lab_Order_MST INNER JOIN " & " Patient ON Lab_Order_MST.labom_PatientID = Patient.nPatientID LEFT OUTER JOIN " _
                           & " User_MST ON Lab_Order_MST.labom_SampledByID = User_MST.nUserID LEFT OUTER JOIN " _
                           & " Lab_ContactInfo ON Lab_Order_MST.labom_PreferredLabID = Lab_ContactInfo.labci_Id LEFT OUTER JOIN " _
                           & " Provider_MST ON Lab_Order_MST.labom_ProviderID = Provider_MST.nProviderID LEFT OUTER JOIN " _
                           & " Contacts_MST ON Lab_Order_MST.labom_ReferredByID = Contacts_MST.nContactID " _
                           & " WHERE Patient.nPatientID = '" & _PatientID & "' And Lab_Order_MST.labom_OrderID=" & OrderID

                sCmd.CommandText = strQuery
                da = New SqlDataAdapter(sCmd)
                da.Fill(dsReports, "dt_PatientInfo")
                da.Dispose()
                da = Nothing
                strQuery = ""
                strQuery = "select isnull(dbo.lab_order_testdtl_diagcpt.labodtl_code, '') + '-' + isnull(dbo.lab_order_testdtl_diagcpt.labodtl_description, '') as description, " _
                           & " convert(varchar(1), dbo.lab_order_testdtl_diagcpt.labodtl_type) + isnull(dbo.lab_order_testdtl_diagcpt.labodtl_code, '') " _
                           & " + isnull(dbo.lab_order_testdtl_diagcpt.labodtl_description, '') as diagcpttype, dbo.lab_order_testdtl_diagcpt.labodtl_type as type," _
                           & " convert(varchar(100), dbo.lab_order_mst.labom_orderid) as orderid,  dbo.lab_order_testdtl.labotd_testid as testid, " _
                           & " dbo.lab_order_mst.labom_patientid as patientid, dbo.lab_order_testdtl_diagcpt.labodtl_testname as testname" _
                           & " from  dbo.lab_order_mst inner join " _
                           & " dbo.lab_order_testdtl on dbo.lab_order_mst.labom_orderid = dbo.lab_order_testdtl.labotd_orderid inner join " _
                           & " dbo.lab_order_testdtl_diagcpt on dbo.lab_order_testdtl.labotd_orderid = dbo.lab_order_testdtl_diagcpt.labodtl_orderid and " _
                           & " dbo.lab_order_testdtl.labotd_testname = dbo.lab_order_testdtl_diagcpt.labodtl_testname " _
                           & " where lab_order_mst.labom_orderid= '" & OrderID & "'"

                sCmd.CommandText = strQuery
                da = New SqlDataAdapter(sCmd)
                da.Fill(dsReports, "dt_LabOrderReportCPTICD9")
                da.Dispose()
                da = Nothing
                'Added by Abhijeet to show insurance details of patient on 20100916
                strQuery = ""
                strQuery = " select  patientInsurance_dtl.nPatientID as Ins_patientid,patientInsurance_dtl.nInsuranceID as nInsuarnceID," _
                           & " patientInsurance_dtl.nInsuranceID as Ins_id," _
                           & " IsNull(patientInsurance_dtl.sSubscriberPolicy#,'') as Ins_subscriberPolicyNo," _
                           & " IsNull(patientInsurance_dtl.sSubscriberID,'') as ins_SubscriberID," _
                           & " IsNull(patientInsurance_dtl.sGroup,'') as Ins_group," _
                           & " IsNull(patientInsurance_dtl.sEmployer,'') as Ins_employer," _
                           & " IsNull(patientInsurance_dtl.dtDOB,'') as Ins_DOB," _
                           & "  dbo.GET_NAME(patientInsurance_dtl.sSubFName,patientInsurance_dtl.sSubMName,patientInsurance_dtl.sSubLName) as INs_Subscribername," _
                           & " Isnull(patientInsurance_dtl.bPrimaryFlag,'') as ins_Primaryflag," _
                           & " Isnull(patientInsurance_dtl.sInsurancePhone,'') as ins_insurancephone," _
                           & " Isnull(patientInsurance_dtl.sInsuranceName,'') as InsuranceName " _
                           & " from patientInsurance_dtl where patientInsurance_dtl.nInsuranceFlag in (1,2,3)" _
                           & " and patientInsurance_dtl.nPatientID=" & _PatientID.ToString()

                sCmd.CommandText = strQuery
                da = New SqlDataAdapter(sCmd)
                da.Fill(dsReports, "dt_PatientInsDtl")
                'End of changes for adding insurance details on 20100916

                da.Dispose()
                da = Nothing
                oLabs.SetDataSource(dsReports)

                'Added by Abhijeet on 20100916 for showing patient insurance information
                'End of changes by Abhijeet on 20100916 for showing patient insurance information
                oLabs.Subreports("Rpt_LabOrderPatientIns.rpt").SetDataSource(dsReports.Tables("dt_PatientInsDtl"))

                ''Bug #65404: 00000646: Collection Date missing while printing Lab from clinical chart.
                ''Description: When printing labs from clinical chart section the collection date fields are not showing.
                ''Resolution: Added margin to page for display the page with proper margin.
                Dim margins As PageMargins = oLabs.PrintOptions.PageMargins
                margins.bottomMargin = 420
                margins.leftMargin = 5
                margins.rightMargin = 5
                margins.topMargin = 420
                oLabs.PrintOptions.ApplyPageMargins(margins)

            End If
        Catch ex As SqlException
            'obj.UpdateLog("Error while generating lab Order report : " + ex.ToString());

            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Catch ex As Exception
            'obj.UpdateLog("Error while generating lab Order report : " + ex.ToString());
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If sCmd IsNot Nothing Then
                sCmd.Parameters.Clear()
                sCmd.Dispose()
                sCmd = Nothing
            End If
            If sConstr IsNot Nothing Then
                sConstr.Close()
                sConstr.Dispose()
                sConstr = Nothing
            End If
        End Try
        Return oLabs
    End Function
    Public Function GetLabPrintData(ByVal nPatientID As Int64, ByVal dtFromDate As DateTime, ByVal dtToDate As DateTime) As DataTable
        Dim _dtLabData As DataTable = Nothing
        Dim _dtFormattedDataTable As DataTable = Nothing
        'Dim _dtOrderIDDataTable As DataTable = Nothing
        Dim _strQuery As String = String.Empty
        Dim oDbLayer As gloDatabaseLayer.DBLayer = Nothing
        Dim arrTests As System.Collections.ArrayList = Nothing

        Try
            '_dtOrderIDDataTable = New DataTable()
            _dtFormattedDataTable = New DataTable()

            oDbLayer = New gloDatabaseLayer.DBLayer(GetConnectionString())
            oDbLayer.Connect(False)

            _strQuery = " SELECT  " & _
                         "	Lab_Order_Test_Result.labotr_TestName As TestName,Lab_Order_Test_ResultDtl.labotrd_ResultLineNo as TestResultLineNumber, " & _
                         "	Lab_Order_Test_Result.labotr_TestResultNumber as TestResultNumber, " & _
                         "	Lab_Order_Test_Result.labotr_TestResultName AS ResultGroupName, " & _
                         "	Lab_Order_Test_ResultDtl.labotrd_ResultValue AS ResultValue, " & _
                         "	Lab_Order_Test_ResultDtl.labotrd_ResultRange AS ResultRange, " & _
                         "	Lab_Order_Test_ResultDtl.labotrd_AbnormalFlag AS ResultAbnormalFlag, " & _
                         "	Lab_Order_Test_ResultDtl.labotrd_ResultUnit AS ResultUnit, " & _
                         "	Lab_Order_Test_ResultDtl.labotrd_ResultName As ResultName, " & _
                         "	Convert(varchar,Lab_Order_Test_ResultDtl.labotrd_ResultDateTime,101) AS ResultDate, " & _
                         "	Lab_Order_Test_ResultDtl.labotrd_OrderID AS OrderID,  " & _
                         "	Lab_Order_Test_ResultDtl.labotrd_TestID AS TestID  " & _
                         " FROM    Lab_Order_MST INNER JOIN Lab_Order_Test_Result  " & _
                         "			ON Lab_Order_MST.labom_OrderID = Lab_Order_Test_Result.labotr_OrderID  " & _
                         "		INNER JOIN " & _
                         "			Lab_Order_Test_ResultDtl  " & _
                         "				ON Lab_Order_Test_Result.labotr_OrderID = Lab_Order_Test_ResultDtl.labotrd_OrderID  " & _
                         "				AND Lab_Order_Test_Result.labotr_TestID = Lab_Order_Test_ResultDtl.labotrd_TestID  " & _
                         "				AND Lab_Order_Test_Result.labotr_TestResultNumber = Lab_Order_Test_ResultDtl.labotrd_TestResultNumber " & _
                         " Where (Lab_Order_Test_ResultDtl.labotrd_ResultType = 'F') AND (Lab_Order_MST.labom_PatientID='" & nPatientID.ToString() & "')  " & _
            "  AND Lab_Order_Test_ResultDtl.labotrd_ResultValue IS NOT NULL AND Ltrim(rtrim(Lab_Order_Test_ResultDtl.labotrd_ResultValue ))!=''" & _
                         "  AND Lab_Order_Test_ResultDtl.labotrd_ResultValue IS NOT NULL  AND Ltrim(rtrim(Lab_Order_Test_ResultDtl.labotrd_ResultValue ))!=''" & _
                         "		AND Convert(DateTime,Convert(varchar,Lab_Order_Test_ResultDtl.labotrd_ResultDateTime,101))>='" & mskStartDate.Text & "'  " & _
                         "	  AND Convert(DateTime,Convert(varchar,Lab_Order_Test_ResultDtl.labotrd_ResultDateTime ,101)) <= '" & mskEndDate.Text & "' " & _
                         "	 AND Lab_Order_Test_ResultDtl.labotrd_TestResultNumber =  " & _
                         "		(  " & _
                         "			SELECT MAX(Lab_Order_Test_ResultDtl1.labotrd_TestResultNumber) " & _
                         "			FROM  Lab_Order_Test_ResultDtl As Lab_Order_Test_ResultDtl1 " & _
                         "			WHERE Lab_Order_Test_ResultDtl1.labotrd_OrderID=Lab_Order_Test_ResultDtl.labotrd_OrderID " & _
                         "				AND Lab_Order_Test_ResultDtl1.labotrd_TestID =Lab_Order_Test_ResultDtl.labotrd_TestID  " & _
                         "		 )  order by Lab_Order_Test_ResultDtl.labotrd_ResultDateTime desc"


            oDbLayer.Retrive_Query(_strQuery, _dtLabData)

            _dtFormattedDataTable.Columns.Add("IsResult")
            '_dtOrderIDDataTable.Columns.Add("IsResult")
            _dtFormattedDataTable.Columns.Add("OrderId")
            '_dtOrderIDDataTable.Columns.Add("OrderId")
            _dtFormattedDataTable.Columns.Add("TestId")
            '_dtOrderIDDataTable.Columns.Add("TestId")
            _dtFormattedDataTable.Columns.Add("TestName")
            '_dtOrderIDDataTable.Columns.Add("TestName")

            'Code for adding the columns...
            Dim _Cnt As Integer = 0

            For Each oDataRow As DataRow In _dtLabData.Rows

                _strQuery = "With LabData As ( " & _
                                " SELECT " & _
                                "	Lab_Order_Test_Result.labotr_TestName As TestName,Lab_Order_Test_ResultDtl.labotrd_TestResultNumber," & _
                                "	Lab_Order_Test_Result.labotr_TestResultNumber as TestResultNumber," & _
                                "	Lab_Order_Test_Result.labotr_TestResultName AS ResultGroupName," & _
                                "	Lab_Order_Test_ResultDtl.labotrd_ResultValue AS ResultValue," & _
                                "	Lab_Order_Test_ResultDtl.labotrd_ResultRange AS ResultRange," & _
                                "	Lab_Order_Test_ResultDtl.labotrd_AbnormalFlag AS ResultAbnormalFlag," & _
                                "	Lab_Order_Test_ResultDtl.labotrd_ResultUnit AS ResultUnit," & _
                                "	Lab_Order_Test_ResultDtl.labotrd_ResultName As ResultName," & _
                                "	Convert(varchar,Lab_Order_Test_ResultDtl.labotrd_ResultDateTime,101) AS ResultDate," & _
                                "	Lab_Order_Test_ResultDtl.labotrd_OrderID AS OrderID, " & _
                                "	Lab_Order_Test_ResultDtl.labotrd_TestID AS TestID " & _
                                "FROM    Lab_Order_MST INNER JOIN Lab_Order_Test_Result " & _
                                "			ON Lab_Order_MST.labom_OrderID = Lab_Order_Test_Result.labotr_OrderID " & _
                                "		INNER JOIN " & _
                                "			Lab_Order_Test_ResultDtl " & _
                                "				ON Lab_Order_Test_Result.labotr_OrderID = Lab_Order_Test_ResultDtl.labotrd_OrderID " & _
                                "				AND Lab_Order_Test_Result.labotr_TestID = Lab_Order_Test_ResultDtl.labotrd_TestID " & _
                                "				AND Lab_Order_Test_Result.labotr_TestResultNumber = Lab_Order_Test_ResultDtl.labotrd_TestResultNumber " & _
                                "Where (Lab_Order_Test_ResultDtl.labotrd_ResultType = 'F') AND (Lab_Order_MST.labom_PatientID='" & nPatientID.ToString() & "') " & _
                "  AND Lab_Order_Test_ResultDtl.labotrd_ResultValue IS NOT NULL AND Ltrim(rtrim(Lab_Order_Test_ResultDtl.labotrd_ResultValue ))!=''" & _
                                "  AND Lab_Order_Test_ResultDtl.labotrd_ResultValue IS NOT NULL  AND Ltrim(rtrim(Lab_Order_Test_ResultDtl.labotrd_ResultValue ))!='' " & _
                                "		AND Convert(DateTime,Convert(varchar,Lab_Order_Test_ResultDtl.labotrd_ResultDateTime,101))>='" & mskStartDate.Text & "' " & _
                                  " AND Convert(DateTime,Convert(varchar,Lab_Order_Test_ResultDtl.labotrd_ResultDateTime ,101)) <= '" & mskEndDate.Text & "' " & _
                                 " AND Lab_Order_Test_ResultDtl.labotrd_TestResultNumber = " & _
                                 "	( " & _
                                 "		SELECT MAX(Lab_Order_Test_ResultDtl1.labotrd_TestResultNumber)" & _
                                 "		FROM  Lab_Order_Test_ResultDtl As Lab_Order_Test_ResultDtl1 " & _
                                 "		WHERE Lab_Order_Test_ResultDtl1.labotrd_OrderID=Lab_Order_Test_ResultDtl.labotrd_OrderID " & _
                                 "			AND Lab_Order_Test_ResultDtl1.labotrd_TestID =Lab_Order_Test_ResultDtl.labotrd_TestID " & _
                                 "	))select count(ResultName) from LabData where ResultDate='" & System.Convert.ToString(oDataRow("ResultDate")) & "' AND TestName='" & System.Convert.ToString(oDataRow("TestName")) & "' group  by ResultName having count(ResultName) >= All(select count(ResultName) from LabData where ResultDate='" & System.Convert.ToString(oDataRow("ResultDate")) & "' AND TestName='" & System.Convert.ToString(oDataRow("TestName")) & "' group  by ResultName) "

                _Cnt = System.Convert.ToInt32(oDbLayer.ExecuteScalar_Query(_strQuery))


                For i As Integer = 0 To _Cnt - 1
                    If i = 0 Then
                        If Not _dtFormattedDataTable.Columns.Contains(oDataRow("ResultDate").ToString()) Then
                            _dtFormattedDataTable.Columns.Add(oDataRow("ResultDate").ToString())
                            '_dtOrderIDDataTable.Columns.Add(oDataRow("ResultDate").ToString())
                        End If
                    Else
                        If Not _dtFormattedDataTable.Columns.Contains(oDataRow("ResultDate").ToString() & "(" & i.ToString() & ")") Then
                            _dtFormattedDataTable.Columns.Add(oDataRow("ResultDate").ToString() & "(" & i.ToString() & ")")
                            '_dtOrderIDDataTable.Columns.Add(oDataRow("ResultDate").ToString() & "(" & i.ToString() & ")")
                        End If
                    End If
                Next
            Next

            'Code for adding the data in table
            arrTests = New System.Collections.ArrayList()
            ' For Each oDataRow As DataRow In dvLabDataFororder.ToTable().Rows
            For Each oDataRow As DataRow In _dtLabData.Rows
                If Not arrTests.Contains(System.Convert.ToString(oDataRow("TestName"))) Then
                    arrTests.Add(System.Convert.ToString(oDataRow("TestName")))
                    Dim dvTestData As New DataView()
                    dvTestData.Table = _dtLabData
                    'dvTestData.RowFilter = "TestName='" & System.Convert.ToString(oDataRow("TestName")) & "' And OrderId = " & strOrderId
                    dvTestData.RowFilter = "TestName='" & System.Convert.ToString(oDataRow("TestName")) & "'"
                    ' Adding test name in seperate row
                    Dim drFormatted As DataRow = _dtFormattedDataTable.NewRow()
                    'Dim drOrderIdrow As DataRow = _dtOrderIDDataTable.NewRow()
                    drFormatted("IsResult") = "False"
                    drFormatted("TestName") = System.Convert.ToString(oDataRow("TestName"))
                    drFormatted("OrderId") = System.Convert.ToString(oDataRow("OrderId"))
                    drFormatted("TestId") = System.Convert.ToString(oDataRow("TestId"))

                    _dtFormattedDataTable.Rows.Add(drFormatted)
                    '_dtOrderIDDataTable.Rows.Add(drOrderIdrow)
                    Dim valToPrint As String
                    'Adding data in formatted datatable
                    Dim arrResults As New System.Collections.ArrayList()
                    Dim _dvTestResultData As New DataView()
                    For rCnt As Integer = 0 To dvTestData.Count - 1
                        If Not arrResults.Contains(System.Convert.ToString(dvTestData(rCnt)("ResultName"))) Then
                            arrResults.Add(System.Convert.ToString(dvTestData(rCnt)("ResultName")))
                            _dvTestResultData.Table = _dtLabData
                            '_dvTestResultData.RowFilter = "TestName='" & System.Convert.ToString(oDataRow("TestName")) & "' AND ResultName= '" & System.Convert.ToString(dvTestData(rCnt)("ResultName")) & "' AND OrderID=" & strOrderId
                            _dvTestResultData.RowFilter = "TestName='" & System.Convert.ToString(oDataRow("TestName")) & "' AND ResultName= '" & System.Convert.ToString(dvTestData(rCnt)("ResultName")) & "'"
                            If _dvTestResultData.Count = 1 Then
                                drFormatted = _dtFormattedDataTable.NewRow()
                                'drOrderIdrow = _dtOrderIDDataTable.NewRow()
                                drFormatted("TestName") = System.Convert.ToString(dvTestData(rCnt)("ResultName"))
                                'drOrderIdrow("TestName") = System.Convert.ToString(dvTestData(rCnt)("ResultName"))
                                drFormatted("IsResult") = "True"
                                drFormatted("OrderId") = System.Convert.ToString(dvTestData(rCnt)("OrderId"))

                                drFormatted("TestId") = System.Convert.ToString(dvTestData(rCnt)("TestId"))
                                valToPrint = String.Empty
                                valToPrint = System.Convert.ToString(dvTestData(rCnt)("ResultValue")) & " " & System.Convert.ToString(dvTestData(rCnt)("ResultUnit"))
                                If System.Convert.ToString(dvTestData(rCnt)("ResultAbnormalFlag")) <> "" AndAlso Not IsNothing(System.Convert.ToString(dvTestData(rCnt)("ResultAbnormalFlag"))) Then
                                    valToPrint &= "(" & System.Convert.ToString(dvTestData(rCnt)("ResultAbnormalFlag")) & ")"
                                End If
                                If System.Convert.ToString(dvTestData(rCnt)("ResultRange")) <> "" AndAlso Not IsNothing(System.Convert.ToString(dvTestData(rCnt)("ResultRange"))) Then
                                    valToPrint &= " [" & System.Convert.ToString(dvTestData(rCnt)("ResultRange")) & "]"
                                End If
                                'drFormatted(System.Convert.ToString(dvTestData(rCnt)("ResultDate"))) = System.Convert.ToString(dvTestData(rCnt)("ResultValue")) & " " & System.Convert.ToString(dvTestData(rCnt)("ResultUnit")) & "(" & System.Convert.ToString(dvTestData(rCnt)("ResultAbnormalFlag")) & ") [" & System.Convert.ToString(dvTestData(rCnt)("ResultRange")) & "]"
                                drFormatted(System.Convert.ToString(dvTestData(rCnt)("ResultDate"))) = valToPrint
                                'drOrderIdrow(System.Convert.ToString(dvTestData(rCnt)("ResultDate"))) = System.Convert.ToString(dvTestData(rCnt)("OrderId"))
                                _dtFormattedDataTable.Rows.Add(drFormatted)
                                '_dtOrderIDDataTable.Rows.Add(drOrderIdrow)
                                '_dtOrderIDDataTable.Rows(_dtOrderIDDataTable.Rows.Count - 1)(System.Convert.ToString(dvTestData(rCnt)("ResultDate"))) = System.Convert.ToString(dvTestData(rCnt)("OrderId"))
                                _dvTestResultData.Dispose()
                                _dvTestResultData = Nothing

                            ElseIf _dvTestResultData.Count > 1 Then

                                Dim arrResultDates As New ArrayList()
                                drFormatted = _dtFormattedDataTable.NewRow()

                                'drOrderIdrow = _dtOrderIDDataTable.NewRow()
                                For oRow As Integer = 0 To _dvTestResultData.Count - 1
                                    If Not arrResultDates.Contains(_dvTestResultData(oRow)("ResultDate")) Then
                                        arrResultDates.Add(_dvTestResultData(oRow)("ResultDate"))
                                        Dim dvResultDatesData As New DataView()
                                        dvResultDatesData.Table = _dvTestResultData.ToTable()
                                        dvResultDatesData.RowFilter = "ResultDate= '" & System.Convert.ToString(_dvTestResultData(oRow)("ResultDate")) & "'"
                                        For introwCnt As Integer = 0 To dvResultDatesData.Count - 1

                                            drFormatted("TestName") = System.Convert.ToString(dvResultDatesData(introwCnt)("ResultName"))
                                            drFormatted("TestId") = System.Convert.ToString(dvResultDatesData(introwCnt)("TestId"))
                                            drFormatted("OrderId") = System.Convert.ToString(dvResultDatesData(introwCnt)("OrderId"))
                                            drFormatted("IsResult") = "True"

                                            valToPrint = String.Empty
                                            If introwCnt = 0 Then
                                                valToPrint = String.Empty
                                                valToPrint = System.Convert.ToString(dvResultDatesData(introwCnt)("ResultValue")) & " " & System.Convert.ToString(dvResultDatesData(introwCnt)("ResultUnit"))
                                                If System.Convert.ToString(dvResultDatesData(introwCnt)("ResultAbnormalFlag")) <> "" AndAlso Not IsNothing(System.Convert.ToString(dvResultDatesData(introwCnt)("ResultAbnormalFlag"))) Then
                                                    valToPrint &= "(" & System.Convert.ToString(dvResultDatesData(introwCnt)("ResultAbnormalFlag")) & ")"
                                                End If
                                                If System.Convert.ToString(dvResultDatesData(introwCnt)("ResultRange")) <> "" AndAlso Not IsNothing(System.Convert.ToString(dvResultDatesData(introwCnt)("ResultRange"))) Then
                                                    valToPrint &= " [" & System.Convert.ToString(dvResultDatesData(introwCnt)("ResultRange")) & "]"
                                                End If
                                                'drFormatted(System.Convert.ToString(dvResultDatesData(introwCnt)("ResultDate"))) = System.Convert.ToString(dvResultDatesData(introwCnt)("ResultValue")) & " " & System.Convert.ToString(dvResultDatesData(introwCnt)("ResultUnit")) & "(" & System.Convert.ToString(dvResultDatesData(introwCnt)("ResultAbnormalFlag")) & ") [" & System.Convert.ToString(dvResultDatesData(introwCnt)("ResultRange")) & " ]"
                                                drFormatted(System.Convert.ToString(dvResultDatesData(introwCnt)("ResultDate"))) = valToPrint
                                                'drOrderIdrow(System.Convert.ToString(dvResultDatesData(introwCnt)("ResultDate"))) = System.Convert.ToString(dvResultDatesData(introwCnt)("OrderId"))
                                            Else
                                                valToPrint = String.Empty
                                                valToPrint = System.Convert.ToString(dvResultDatesData(introwCnt)("ResultValue")) & " " & System.Convert.ToString(dvResultDatesData(introwCnt)("ResultUnit"))
                                                If System.Convert.ToString(dvResultDatesData(introwCnt)("ResultAbnormalFlag")) <> "" AndAlso Not IsNothing(System.Convert.ToString(dvResultDatesData(introwCnt)("ResultAbnormalFlag"))) Then
                                                    valToPrint &= "(" & System.Convert.ToString(dvResultDatesData(introwCnt)("ResultAbnormalFlag")) & ")"
                                                End If
                                                If System.Convert.ToString(dvResultDatesData(introwCnt)("ResultRange")) <> "" AndAlso Not IsNothing(System.Convert.ToString(dvResultDatesData(introwCnt)("ResultRange"))) Then
                                                    valToPrint &= " [" & System.Convert.ToString(dvResultDatesData(introwCnt)("ResultRange")) & "]"
                                                End If
                                                'drFormatted(System.Convert.ToString(dvResultDatesData(introwCnt)("ResultDate")) & "(" & introwCnt.ToString() & ")") = System.Convert.ToString(dvResultDatesData(introwCnt)("ResultValue")) & " " & System.Convert.ToString(dvResultDatesData(introwCnt)("ResultUnit")) & "(" & System.Convert.ToString(dvResultDatesData(introwCnt)("ResultAbnormalFlag")) & ") [" & System.Convert.ToString(dvResultDatesData(introwCnt)("ResultAbnormalFlag")) & "]"
                                                drFormatted(System.Convert.ToString(dvResultDatesData(introwCnt)("ResultDate")) & "(" & introwCnt.ToString() & ")") = valToPrint
                                                'drOrderIdrow(System.Convert.ToString(dvResultDatesData(introwCnt)("ResultDate")) & "(" & introwCnt.ToString() & ")") = System.Convert.ToString(dvResultDatesData(introwCnt)("OrderId"))
                                            End If

                                        Next
                                        dvResultDatesData.Dispose()
                                        dvResultDatesData = Nothing
                                    End If
                                    '**New one 

                                Next
                                _dvTestResultData.Dispose()
                                _dtFormattedDataTable.Rows.Add(drFormatted)
                                '_dtOrderIDDataTable.Rows.Add(drOrderIdrow)
                                arrResultDates = Nothing
                            End If
                        End If

                    Next
                    drFormatted = Nothing
                    'drOrderIdrow = Nothing
                    dvTestData.Dispose()
                    dvTestData = Nothing
                    arrResults = Nothing
                End If
            Next

            _dtFormattedDataTable.Columns(_dtFormattedDataTable.Columns.IndexOf("TestName")).ColumnName = "Test Name"

            Return _dtFormattedDataTable
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ex = Nothing
        Finally

            arrTests = Nothing

            If Not IsNothing(oDbLayer) Then
                oDbLayer.Disconnect()
                oDbLayer.Dispose()
                oDbLayer = Nothing
            End If

            If Not IsNothing(_dtLabData) Then
                _dtLabData.Dispose()
                _dtLabData = Nothing
            End If

            If Not IsNothing(_dtFormattedDataTable) Then
                _dtFormattedDataTable.Dispose()
                _dtFormattedDataTable = Nothing
            End If
        End Try

        Return _dtFormattedDataTable
    End Function
    Public Function GetPatientInformation(ByVal nPatinetId As Int64, ByVal strConnection As String) As DataTable
        Dim oDbLayer As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Try
            oDbLayer.Connect(False)
            Dim oPatientDataTable As DataTable = Nothing
            oDbLayer.Retrive_Query("SELECT dbo.GET_NAME(Patient.sFirstName, Patient.sMiddleName, Patient.sLastName) As 'Patient Name',Convert(Datetime,Patient.dtDOB,101)As DOB, datediff(yy,Patient.dtDOB,dbo.gloGetDate()) AS 'Age',Patient.sGender As Gender,  Clinic_MST.sClinicName 'Practice Name' FROM Clinic_MST INNER JOIN Patient ON Clinic_MST.nClinicID = Patient.nClinicID Where Patient.nPatientId=" + nPatinetId.ToString(), oPatientDataTable)
            Return oPatientDataTable
        Catch ex As Exception
            Return Nothing
        Finally
            If Not IsNothing(oDbLayer) Then
                oDbLayer.Disconnect()
                oDbLayer.Dispose()
            End If
        End Try
    End Function
    Private Sub loadPatientStrip()
        If IsNothing(_PatientStrip) = False Then
            Me.Panel4.Controls.Remove(_PatientStrip)
            _PatientStrip.Dispose()
            _PatientStrip = Nothing
        End If
        _PatientStrip = New gloUC_PatientStrip
        _PatientStrip.ShowDetail(_PatientID, gloUC_PatientStrip.enumFormName.ClinicalChart)
        _PatientStrip.Dock = DockStyle.Top

        '_PatientStrip.SendToBack()
        _PatientStrip.Padding = New Padding(3, 0, 3, 0)

        Panel1.SendToBack()
        _PatientStrip.BringToFront()
        Panel4.Controls.Add(_PatientStrip)
        'If m_IsReadOnly Then
        _PatientStrip.DTPEnabled = False

        'End If
        ' wdMessages.BringToFront()
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

#Region "Call Generate CCDA from Dashboard"
    'Public Delegate Sub GenerateCDAFromClinicalChart(ByVal PatientID As Int64)
    'Public Event EvntGenerateCDAFromClinicalChart(ByVal PatientID As Int64)

    Protected Overridable Sub Raise_GenerateCDAFromClinicalChart(ByVal PatientID As Int64)
        'RaiseEvent EvntGenerateCDAFromClinicalChart(PatientID)

        Try
            mdlGeneral.OpenCDA(PatientID)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try
    End Sub
#End Region

    Public Sub Navigate(ByVal strstring As String) Implements mdlHotkey.IHotKey.Navigate
        For Each frm As Form In Application.OpenForms
            If frm.Name = "frmEDocEvent_Fax" Then
                ' frm.Navigate(strstring)
                DirectCast(frm, gloEDocumentV3.Forms.frmEDocEvent_Fax).Navigate(strstring)
            End If
        Next
    End Sub

    Private Sub InsertNotesInClaims(ByVal ListClaimNoteIDs As List(Of ClaimNoteIDs))

        Dim dtContacts As DataTable = Nothing
        Dim sContactName As String = String.Empty

        Try
            If ListClaimNoteIDs.Any() Then
                Dim nUserID As Long
                Dim elementRow As DataRow = Nothing
                If appSettings("UserID") IsNot Nothing Then nUserID = System.Convert.ToInt64(appSettings("UserID"))

                dtContacts = Me.GetContacts(ListClaimNoteIDs)

                For Each Element As ClaimNoteIDs In ListClaimNoteIDs
                    Dim claimNoteElement As ClaimNoteIDs = Element
                    sContactName = String.Empty
                    elementRow = (From dRow As DataRow In dtContacts.Rows
                                   Where System.Convert.ToInt64(dRow("nContactID")) = claimNoteElement.ContactID
                                    Select dRow).FirstOrDefault()

                    If elementRow IsNot Nothing Then
                        sContactName = elementRow("sName")

                        Dim oNote As New gloBilling.Common.GeneralNote()
                        With oNote
                            .TransactionID = Element.MasterTransactionID
                            .TransactionLineId = 0
                            .TransactionDetailID = 0
                            .NoteType = gloBilling.NoteType.Claim_Note
                            .NoteID = 0
                            .NoteDate = gloDateMaster.gloDate.DateAsNumber(System.Convert.ToString(Date.Today))
                            .UserID = nUserID
                            .NoteDescription = Element.ClaimID + sNoteDescription
                            .ClinicID = gnClinicID
                        End With

                        Dim oNotes As New gloBilling.Common.GeneralNotes()
                        oNotes.Add(oNote)

                        gloCharges.SaveClaimNotes(oNotes)

                        If oNotes IsNot Nothing Then
                            oNotes.Dispose()
                            oNotes = Nothing
                        End If

                        If oNote IsNot Nothing Then
                            oNote.Dispose()
                            oNote = Nothing
                        End If
                    End If
                    elementRow = Nothing
                    claimNoteElement = Nothing
                Next

                Me.lstClaimMasterIDs.Clear()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Public Function GetContacts(ByVal ListClaimIDs As List(Of ClaimNoteIDs)) As DataTable
        Dim dt As DataTable = Nothing
        Dim dtTVP As DataTable = Nothing
        Dim dRow As DataRow = Nothing
        Dim IEnum As IEnumerable(Of ClaimNoteIDs) = Nothing

        Try
            dtTVP = New DataTable()
            dtTVP.Columns.Add(New DataColumn("nContactID", System.Type.GetType("System.Int64")))

            For Each Element As ClaimNoteIDs In ListClaimIDs
                dRow = dtTVP.NewRow()
                dRow("nContactID") = Element.ContactID
                dtTVP.Rows.Add(dRow)
            Next

            Dim objCon As New SqlConnection
            Dim cmd As New SqlCommand("ClinicalChart_GetContacts", objCon)
            Dim da As New SqlDataAdapter
            dt = New DataTable

            objCon.ConnectionString = GetConnectionString()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@TVP", dtTVP)
            cmd.CommandTimeout = 0

            objCon.Open()

            da.SelectCommand = cmd

            da.Fill(dt)
            objCon.Close()
            objCon.Dispose()
            objCon = Nothing
            da.Dispose()
            da = Nothing

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If dtTVP IsNot Nothing Then
                dtTVP.Dispose()
                dtTVP = Nothing
            End If
            Return dt
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return Nothing
        End Try
    End Function

    Private Sub Queue(ByVal BackgroundProcessType As gloUIControlLibrary.Classes.ClinicalChartQueue.enumBackgroundProcessType, Optional ByVal dtDetails As DataTable = Nothing)
        If IsHaveDocumenttoExport() = False Then
            MessageBox.Show("Elements of the patient’s chart are not selected or selected elements do not have data.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim dtTVP As New DataTable()
        Dim sEnum As String = ""
        Dim strmessage As String = ""
        Dim strProgressBarMessage As String = "Generating file ... "
        Dim _isPrintOrderMessageshown As Boolean = False
        Dim LastOrderID As Int64 = 0
        Dim nAssociatedContactID As Int64 = 0
        Dim sNotes As String = ""
        Dim sClaimPrinter As String = ""
        Dim bIsforQueue As Boolean = False
        Dim sDocumentDate As String = ""
        Dim sDocumentName As String = ""
        C1ClinicalChart.Enabled = False
        Dim nUserID As Int64 = 0
        Dim nClaimPrintType As Integer = 0



        With dtTVP
            .Columns.Add("sEnum", Type.GetType("System.String"))
            .Columns.Add("sTranID_I", Type.GetType("System.String"))
            .Columns.Add("sTranID_II", Type.GetType("System.String"))
            .Columns.Add("sTranID_III", Type.GetType("System.String"))
            .Columns.Add("sTranID_IV", Type.GetType("System.String"))
            .Columns.Add("sTranID_V", Type.GetType("System.String"))
            .Columns.Add("bIsforPrinting", Type.GetType("System.Boolean"))
            .Columns.Add("sDocumentName", Type.GetType("System.String"))
        End With

        If BackgroundProcessType = gloUIControlLibrary.Classes.ClinicalChartQueue.enumBackgroundProcessType.FAX Then
            nAssociatedContactID = 0
            sNotes = ""
            sClaimPrinter = ""

        ElseIf BackgroundProcessType = gloUIControlLibrary.Classes.ClinicalChartQueue.enumBackgroundProcessType.Print OrElse BackgroundProcessType = gloUIControlLibrary.Classes.ClinicalChartQueue.enumBackgroundProcessType.TsPrint Then

            If IsNothing(cmbClaimPrinter) = False AndAlso IsNothing(cmbClaimPrinter.Text) = False Then
                sClaimPrinter = cmbClaimPrinter.Text
            End If


        ElseIf BackgroundProcessType = gloUIControlLibrary.Classes.ClinicalChartQueue.enumBackgroundProcessType.Queue Then

            Dim ofrmClinicalChartQueueNotes = Nothing
            If rbPrintData.Checked Then
                ''Bug #96466: gloEMR: Clinical Chart: application gives exception on queue
                ''Added System.Convert.ToString to combobox item
                ofrmClinicalChartQueueNotes = New gloUIControlLibrary.WPFForms.frmClinicalChartQueueNotes(_PatientID, _nQueueID, UserName, UserID, _MessageBoxCaption, GetConnectionString(), False, System.Convert.ToString(cmbClaimPrinter.SelectedItem))
            ElseIf rbPrintOnForm.Checked Then
                ofrmClinicalChartQueueNotes = New gloUIControlLibrary.WPFForms.frmClinicalChartQueueNotes(_PatientID, _nQueueID, UserName, UserID, _MessageBoxCaption, GetConnectionString(), False, "")
            End If

            'Dim ofrmClinicalChartQueueNotes As New gloUIControlLibrary.WPFForms.frmClinicalChartQueueNotes(_PatientID, _nQueueID, UserName, UserID, _MessageBoxCaption, GetConnectionString(), cmbClaimPrinter.SelectedItem.ToString())

            Dim _interophelper As New System.Windows.Interop.WindowInteropHelper(ofrmClinicalChartQueueNotes)
            _interophelper.Owner = Me.Handle

            ofrmClinicalChartQueueNotes.ShowDialog()
            bIsforQueue = ofrmClinicalChartQueueNotes.SaveAndClose

            _interophelper.Owner = Nothing
            _interophelper = Nothing

            If ofrmClinicalChartQueueNotes.SaveAndClose Then
                nAssociatedContactID = ofrmClinicalChartQueueNotes.nAssociatedContactID
                sNotes = ofrmClinicalChartQueueNotes.Notes
                sClaimPrinter = ofrmClinicalChartQueueNotes.SelectedPrinter
            Else
                C1ClinicalChart.Enabled = True
                Exit Sub
            End If
            If ofrmClinicalChartQueueNotes IsNot Nothing Then
                ofrmClinicalChartQueueNotes = Nothing
            End If
        Else
            nAssociatedContactID = 0
            sNotes = ""
            If cmbClaimPrinter.Text = "" Then
                sClaimPrinter = ""
            Else
                sClaimPrinter = cmbClaimPrinter.Text
            End If
        End If



        Try
            Dim strNDC As String = ""
            lstClaimMasterIDs.Clear()
            Panel2.SuspendLayout()
            pnlProgress.Dock = DockStyle.Bottom
            Panel2.Height = 96
            Panel2.ResumeLayout()
            'AuditLogModuleList.Remove(0, AuditLogModuleList.Length) '' 


            'Sfd1.FileName = sFilename

            strProgressBarMessage = "Preparing for Queue..."
            strmessage = "Queued "
            prgGeneratefile.Value = 0
            ''Label12.Visible = False
            prgGeneratefile.Visible = True
            lblStatus.Text = strProgressBarMessage
            prgGeneratefile.Value = 0
            lblStatus.Visible = True
            pnlProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            prgGeneratefile.Minimum = 0
            prgGeneratefile.Maximum = C1ClinicalChart.Rows.Count - 1

            For i As Int64 = 0 To C1ClinicalChart.Rows.Count - 1

                Dim Flag As String
                Dim IsCheck As C1.Win.C1FlexGrid.CheckEnum
                Dim i16 As Int16 = System.Convert.ToInt16(i)
                Try
                    Flag = C1ClinicalChart.GetData(i16, 4).ToString()
                    IsCheck = C1ClinicalChart.GetCellCheck(i16, 1)
                    sEnum = C1ClinicalChart.GetData(i16, 1).ToString()

                    If IsCheck = CheckEnum.Checked Then
                        Dim a As Integer = 0
                    End If
                Catch ex As Exception
                    Flag = 0
                    IsCheck = CheckEnum.None
                    'Audit LogFor Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
                End Try
                If (IsCheck = C1.Win.C1FlexGrid.CheckEnum.Checked) Then
                    If (Flag = "1111") OrElse (Flag = "111") Then
                        If (Flag = "1111") Then
                            'Dim sStartDate As String = mskStartDate.Text
                            'Dim sEndDate As String = mskEndDate.Text

                            'If (IsCheck = C1.Win.C1FlexGrid.CheckEnum.Checked) Then
                            strNDC = C1ClinicalChart.GetData(i16, 0).ToString()
                            'sEnum = C1ClinicalChart.GetData(i16, 1).ToString()
                            sDocumentDate = System.Convert.ToString(C1ClinicalChart.GetData(i16, 3))
                            sDocumentName = gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.CurrentMedication.ToString() & "_" & sEnum & "_" & sDocumentDate
                            dtTVP.Rows.Add(GetDataRow(gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.CurrentMedication.ToString(), strNDC, String.Empty, String.Empty, String.Empty, String.Empty, dtTVP, sDocumentName))
                            'End If

                        ElseIf Flag = "111" Then
                            'If (IsCheck = C1.Win.C1FlexGrid.CheckEnum.Checked) Then
                            'Dim sStartDate As String = mskStartDate.Text
                            'Dim sEndDate As String = mskEndDate.Text
                            strNDC = C1ClinicalChart.GetData(i16, 0).ToString()
                            'sEnum = C1ClinicalChart.GetData(i16, 1).ToString()
                            sDocumentDate = System.Convert.ToString(C1ClinicalChart.GetData(i16, 3))
                            sDocumentName = gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.CurrentMedication.ToString() & "_" & sEnum & "_" & sDocumentDate

                            dtTVP.Rows.Add(GetDataRow(gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.CurrentMedication.ToString(), strNDC, String.Empty, String.Empty, String.Empty, String.Empty, dtTVP, sDocumentName))
                            'End If

                        End If

                    ElseIf (Flag = "2") Then
                        'If (IsCheck = C1.Win.C1FlexGrid.CheckEnum.Checked) Then
                        Dim nPastExamID As Long
                        nPastExamID = C1ClinicalChart.GetData(i16, 0)
                        'sEnum = C1ClinicalChart.GetData(i16, 1).ToString()
                        sDocumentDate = System.Convert.ToString(C1ClinicalChart.GetData(i16, 3))
                        sDocumentName = gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.PatientExam.ToString() & "_" & sEnum & "_" & sDocumentDate

                        dtTVP.Rows.Add(GetDataRow(gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.PatientExam.ToString(), System.Convert.ToString(nPastExamID), String.Empty, String.Empty, String.Empty, String.Empty, dtTVP, sDocumentName))
                        'End If

                    ElseIf (Flag.Contains("L")) Then
                        'If (IsCheck = C1.Win.C1FlexGrid.CheckEnum.Checked) Then
                        'Dim oLabOrderRequest As New gloEMRLabOrder
                        Dim OrderId As Long = System.Convert.ToInt64(C1ClinicalChart.GetData(i16, 0))
                        Dim OrderNumber As String = System.Convert.ToString(C1ClinicalChart.GetData(i16, 1))
                        'sEnum = C1ClinicalChart.GetData(i16, 1).ToString()
                        sDocumentDate = System.Convert.ToString(C1ClinicalChart.GetData(i16, 3))
                        sDocumentName = gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.Orders.ToString() & "_" & sEnum & "_" & sDocumentDate

                        'If IsNothing(oLabOrderRequest) = False Then
                        '    oLabOrderRequest.Dispose()
                        '    oLabOrderRequest = Nothing
                        'End If
                        dtTVP.Rows.Add(GetDataRow(gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.Orders.ToString(), System.Convert.ToString(OrderId), System.Convert.ToString(OrderNumber), String.Empty, String.Empty, String.Empty, dtTVP, sDocumentName))
                        'End If
                    ElseIf (Flag = "4") Then
                        'If (IsCheck = C1.Win.C1FlexGrid.CheckEnum.Checked) Then
                        Dim sStartDate As String = mskStartDate.Text
                        Dim sEndDate As String = mskEndDate.Text
                        'sEnum = C1ClinicalChart.GetData(i16, 1).ToString()
                        sDocumentDate = System.Convert.ToString(C1ClinicalChart.GetData(i16, 3))
                        sDocumentName = Flag & "_" & sEnum & "_" & sDocumentDate

                        dtTVP.Rows.Add(GetDataRow(Flag, sStartDate, sEndDate, String.Empty, String.Empty, String.Empty, dtTVP, sDocumentName))
                        'End If

                    ElseIf (Flag.Contains("C")) Then
                        'If (IsCheck = C1.Win.C1FlexGrid.CheckEnum.Checked) Then
                        Dim _DocID As Long = System.Convert.ToInt64(C1ClinicalChart.GetData(i16, 0))
                        'sEnum = C1ClinicalChart.GetData(i16, 1).ToString()
                        sDocumentDate = System.Convert.ToString(C1ClinicalChart.GetData(i16, 3))
                        sDocumentName = gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.ScannedDocument.ToString() & "_" & sEnum & "_" & sDocumentDate

                        dtTVP.Rows.Add(GetDataRow(gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.ScannedDocument.ToString(), System.Convert.ToString(_DocID), String.Empty, String.Empty, String.Empty, String.Empty, dtTVP, sDocumentName))
                        'End If

                    ElseIf (Flag = "16") Then
                        'If (IsCheck = C1.Win.C1FlexGrid.CheckEnum.Checked) Then
                        Dim TransactionId As Long = System.Convert.ToInt64(C1ClinicalChart.GetData(System.Convert.ToInt32(i), COL_Seven))
                        Dim MasterTransactionID As Long = System.Convert.ToInt64(C1ClinicalChart.GetData(i16, 0).ToString())
                        Dim sClaimType As String = System.Convert.ToString(C1ClinicalChart.GetData(i16, COL_Five))
                        Dim nContactID As Int64 = System.Convert.ToInt64(C1ClinicalChart.GetData(i16, COL_Six))
                        Dim sClaimNumber As String = System.Convert.ToString(C1ClinicalChart.GetData(i16, COL_Category))
                        'sEnum = C1ClinicalChart.GetData(i16, 1).ToString()
                        sDocumentDate = System.Convert.ToString(C1ClinicalChart.GetData(i16, 3))
                        sDocumentName = gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.ClaimDetails.ToString() & "_" & sEnum & "_" & sDocumentDate

                        dtTVP.Rows.Add(GetDataRow(gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.ClaimDetails.ToString(), System.Convert.ToString(TransactionId), System.Convert.ToString(MasterTransactionID), System.Convert.ToString(sClaimType), System.Convert.ToString(nContactID), System.Convert.ToString(sClaimNumber), dtTVP, sDocumentName))
                        'End If

                    ElseIf (Flag = "17") Then
                        'If (IsCheck = C1.Win.C1FlexGrid.CheckEnum.Checked) Then
                        Dim nCreditID As Int64 = System.Convert.ToInt64(C1ClinicalChart.GetData(System.Convert.ToInt32(i), 0))
                        Dim nEOBID As Int64 = System.Convert.ToInt64(C1ClinicalChart.GetData(System.Convert.ToInt32(i), COL_Five))
                        Dim sPaymentDescription As String = System.Convert.ToString(C1ClinicalChart.GetData(i16, COL_Category))
                        Dim nEOBType As Int64 = 4
                        'sEnum = C1ClinicalChart.GetData(i16, 1).ToString()
                        sDocumentDate = System.Convert.ToString(C1ClinicalChart.GetData(i16, 3))
                        sDocumentName = gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.PaymentDetails.ToString() & "_" & sEnum & "_" & sDocumentDate

                        dtTVP.Rows.Add(GetDataRow(gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.PaymentDetails.ToString(), System.Convert.ToString(nCreditID), System.Convert.ToString(nEOBID), System.Convert.ToString(nEOBType), System.Convert.ToString(sPaymentDescription), String.Empty, dtTVP, sDocumentName))

                        'End If
                    ElseIf (Flag = "18") Then
                        'If (IsCheck = C1.Win.C1FlexGrid.CheckEnum.Checked) Then
                        Dim TransactionId As Long = System.Convert.ToInt64(C1ClinicalChart.GetData(System.Convert.ToInt32(i), COL_Seven))
                        Dim MasterTransactionID As Long = System.Convert.ToInt64(C1ClinicalChart.GetData(i16, 0).ToString())
                        Dim sClaimType As String = System.Convert.ToString(C1ClinicalChart.GetData(i16, COL_Five))
                        Dim nContactID As Int64 = 0 ''System.Convert.ToInt64(C1ClinicalChart.GetData(i16, COL_Six))
                        Dim sClaimNumber As String = System.Convert.ToString(C1ClinicalChart.GetData(i16, COL_Category))
                        'sEnum = C1ClinicalChart.GetData(i16, 1).ToString()
                        sDocumentDate = System.Convert.ToString(C1ClinicalChart.GetData(i16, 3))
                        sDocumentName = gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.ClaimHistory.ToString() & "_" & sEnum & "_" & sDocumentDate

                        dtTVP.Rows.Add(GetDataRow(gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.ClaimHistory.ToString(), System.Convert.ToString(TransactionId), System.Convert.ToString(MasterTransactionID), System.Convert.ToString(sClaimType), "", System.Convert.ToString(sClaimNumber), dtTVP, sDocumentName))
                        'End If
                    ElseIf (Flag = "19") Then
                        'If (IsCheck = C1.Win.C1FlexGrid.CheckEnum.Checked) Then
                        Dim nPatientformID As Int64 = System.Convert.ToInt64(C1ClinicalChart.GetData(System.Convert.ToInt32(i), 0))
                        'sEnum = C1ClinicalChart.GetData(i16, 1).ToString()
                        sDocumentDate = System.Convert.ToString(C1ClinicalChart.GetData(i16, 3))
                        sDocumentName = gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.PatientForms.ToString() & "_" & sEnum & "_" & sDocumentDate

                        dtTVP.Rows.Add(GetDataRow(gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.PatientForms.ToString(), System.Convert.ToString(nPatientformID), System.Convert.ToString(_PatientID), String.Empty, String.Empty, String.Empty, dtTVP, sDocumentName))

                        'End If
                    ElseIf (Flag = "21") Then
                        'If (IsCheck = C1.Win.C1FlexGrid.CheckEnum.Checked) Then
                        Dim nEducationID As Int64 = System.Convert.ToInt64(C1ClinicalChart.GetData(System.Convert.ToInt32(i), 0))
                        'sEnum = C1ClinicalChart.GetData(i16, 1).ToString()
                        sDocumentDate = System.Convert.ToString(C1ClinicalChart.GetData(i16, 3))
                        sDocumentName = gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.Patient_Education.ToString() & "_" & sEnum & "_" & sDocumentDate

                        'Update online Education content to table
                        Dim nVisistId As Int64 = System.Convert.ToInt64(C1ClinicalChart.GetData(i16, COL_Five))
                        Dim sDocumentURL As String = System.Convert.ToString(C1ClinicalChart.GetData(i16, COL_Six))
                        Dim NeedUpdate As Byte = System.Convert.ToByte(C1ClinicalChart.GetData(i16, COL_Seven))
                        If NeedUpdate = 1 Then
                            SaveEducationLinkData(nEducationID, nVisistId, sDocumentURL)
                            C1ClinicalChart(i16, COL_Seven) = "0"
                        End If

                        dtTVP.Rows.Add(GetDataRow(gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.Patient_Education.ToString(), System.Convert.ToString(nEducationID), System.Convert.ToString(_PatientID), String.Empty, String.Empty, String.Empty, dtTVP, sDocumentName))

                        'End If
                    ElseIf (Flag = "" Or Flag = "0" Or Flag = "1" Or Flag.Contains("R")) Then
                        'If (IsCheck = C1.Win.C1FlexGrid.CheckEnum.Checked) Then
                        If sEnum = "Claim Details" Then
                            'sDocumentDate = System.Convert.ToString(C1ClinicalChart.GetData(i16, 3))
                            'sDocumentName = sEnum & "#" & sEnum & "#" & sDocumentDate

                            Dim MasterTransactionID As Long = 0
                            MasterTransactionID = System.Convert.ToInt64(C1ClinicalChart.GetData(i16, 0).ToString())
                            dtTVP.Rows.Add(GetDataRow(sEnum, sEnum, System.Convert.ToString(MasterTransactionID), String.Empty, String.Empty, String.Empty, dtTVP, String.Empty, False))
                        ElseIf sEnum = "Claim History" Then
                            Dim MasterTransactionID As Long = 0
                            MasterTransactionID = System.Convert.ToInt64(C1ClinicalChart.GetData(i16, 0).ToString())
                            dtTVP.Rows.Add(GetDataRow(sEnum, sEnum, System.Convert.ToString(MasterTransactionID), String.Empty, String.Empty, String.Empty, dtTVP, String.Empty, False))
                        ElseIf sEnum = "Payment Details" Then
                            Dim nCreditID As Int64 = 0
                            'sDocumentDate = System.Convert.ToString(C1ClinicalChart.GetData(i16, 3))
                            'sDocumentName = sEnum & "#" & sEnum & "#" & sDocumentDate

                            nCreditID = System.Convert.ToInt64(C1ClinicalChart.GetData(System.Convert.ToInt32(i), 0))
                            dtTVP.Rows.Add(GetDataRow(sEnum, sEnum, System.Convert.ToString(nCreditID), String.Empty, String.Empty, String.Empty, dtTVP, String.Empty, False))
                        ElseIf Flag = "1" Then
                            'sDocumentDate = System.Convert.ToString(C1ClinicalChart.GetData(i16, 3))
                            sDocumentName = "PatientInformation"

                            dtTVP.Rows.Add(GetDataRow(sEnum, sEnum, String.Empty, String.Empty, String.Empty, String.Empty, dtTVP, sDocumentName, True))
                        Else
                            'sDocumentDate = System.Convert.ToString(C1ClinicalChart.GetData(i16, 3))
                            'sDocumentName = sEnum & "#" & sEnum & "#" & sDocumentDate

                            dtTVP.Rows.Add(GetDataRow(sEnum, sEnum, String.Empty, String.Empty, String.Empty, String.Empty, dtTVP, String.Empty, False))
                        End If


                        'End If
                    ElseIf (Flag = "6") Or (Flag = "7") Or (Flag = "8") Or (Flag = "9") Or (Flag = "10") Or (Flag = "11") Or (Flag = "12") Or (Flag = "13") Or (Flag = "14") Or (Flag = "15") Or (Flag = "10") Or (Flag.Contains("T")) Then
                        'If (IsCheck = C1.Win.C1FlexGrid.CheckEnum.Checked) Then
                        Dim Pfound As Boolean = False
                        Dim nID As String = C1ClinicalChart.GetData(i16, 0).ToString()
                        Dim rIndex As Integer = 0
                        Dim OrderID As String = String.Empty
                        Dim OrderNumber As String = String.Empty
                        Dim OrderStatus As Int16

                        If (C1ClinicalChart.GetData(i16, 4).ToString().Contains("T")) Then
                            Flag = 3
                            rIndex = i
                            Dim oLabOrderRequest As New gloEMRLabOrder
                            While (Pfound <> True)
                                If (C1ClinicalChart.Rows(rIndex)(4).ToString.Trim().Contains("L")) Then
                                    Pfound = True
                                    OrderID = System.Convert.ToInt64(C1ClinicalChart.GetData(rIndex, 0))
                                    OrderNumber = System.Convert.ToString(C1ClinicalChart.GetData(rIndex, 1))
                                    OrderStatus = oLabOrderRequest.GetOrderStatus(OrderID)
                                End If
                                rIndex = rIndex - 1
                            End While
                            If IsNothing(oLabOrderRequest) = False Then
                                oLabOrderRequest.Dispose()
                                oLabOrderRequest = Nothing
                            End If
                        Else
                            Flag = CType(C1ClinicalChart.GetData(i16, 4).ToString(), Integer)
                        End If
                        ''Flag = CType(C1ClinicalChart.GetData(i16, 4).ToString(), Integer)
                        'sEnum = C1ClinicalChart.GetData(i16, 1).ToString()
                        sDocumentDate = System.Convert.ToString(C1ClinicalChart.GetData(i16, 3))
                        If (Flag = "15") Then

                            sDocumentName = gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.FormGallery.ToString() & "_" & sEnum & "_" & sDocumentDate
                            dtTVP.Rows.Add(GetDataRow(gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.FormGallery.ToString(), System.Convert.ToString(nID), System.Convert.ToString(OrderID), System.Convert.ToString(OrderNumber), String.Empty, String.Empty, dtTVP, sDocumentName))
                        ElseIf (Flag = "11") Then

                            sDocumentName = gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.PTProtocol.ToString() & "_" & sEnum & "_" & sDocumentDate
                            dtTVP.Rows.Add(GetDataRow(gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.PTProtocol.ToString(), System.Convert.ToString(nID), System.Convert.ToString(OrderID), System.Convert.ToString(OrderNumber), String.Empty, String.Empty, dtTVP, sDocumentName))
                        ElseIf (Flag = "6") Then

                            sDocumentName = gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.OrderTemplates.ToString() & "_" & sEnum & "_" & sDocumentDate
                            dtTVP.Rows.Add(GetDataRow(gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.OrderTemplates.ToString(), System.Convert.ToString(nID), System.Convert.ToString(OrderID), System.Convert.ToString(OrderNumber), String.Empty, String.Empty, dtTVP, sDocumentName))
                        ElseIf (Flag = "7") Then

                            sDocumentName = gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.RefferalDocument.ToString() & "_" & sEnum & "_" & sDocumentDate
                            dtTVP.Rows.Add(GetDataRow(gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.RefferalDocument.ToString(), System.Convert.ToString(nID), System.Convert.ToString(OrderID), System.Convert.ToString(OrderNumber), String.Empty, String.Empty, dtTVP, sDocumentName))

                        ElseIf (Flag = "8") Then

                            sDocumentName = gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.Messages.ToString() & "_" & sEnum & "_" & sDocumentDate
                            dtTVP.Rows.Add(GetDataRow(gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.Messages.ToString(), System.Convert.ToString(nID), System.Convert.ToString(OrderID), System.Convert.ToString(OrderNumber), String.Empty, String.Empty, dtTVP, sDocumentName))
                        ElseIf (Flag = "9") Then

                            sDocumentName = gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.NursesNotes.ToString() & "_" & sEnum & "_" & sDocumentDate
                            dtTVP.Rows.Add(GetDataRow(gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.NursesNotes.ToString(), System.Convert.ToString(nID), System.Convert.ToString(OrderID), System.Convert.ToString(OrderNumber), String.Empty, String.Empty, dtTVP, sDocumentName))
                        ElseIf (Flag = "10") Then

                            sDocumentName = gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.Triage.ToString() & "_" & sEnum & "_" & sDocumentDate
                            dtTVP.Rows.Add(GetDataRow(gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.Triage.ToString(), System.Convert.ToString(nID), System.Convert.ToString(OrderID), System.Convert.ToString(OrderNumber), String.Empty, String.Empty, dtTVP, sDocumentName))
                        ElseIf (Flag = "12") Then

                            sDocumentName = gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.PatientConsent.ToString() & "_" & sEnum & "_" & sDocumentDate
                            dtTVP.Rows.Add(GetDataRow(gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.PatientConsent.ToString(), System.Convert.ToString(nID), System.Convert.ToString(OrderID), System.Convert.ToString(OrderNumber), String.Empty, String.Empty, dtTVP, sDocumentName))
                        ElseIf (Flag = "13") Then

                            sDocumentName = gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.DisclosureManagement.ToString() & "_" & sEnum & "_" & sDocumentDate
                            dtTVP.Rows.Add(GetDataRow(gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.DisclosureManagement.ToString(), System.Convert.ToString(nID), System.Convert.ToString(OrderID), System.Convert.ToString(OrderNumber), String.Empty, String.Empty, dtTVP, sDocumentName))
                        ElseIf (Flag = "14") Then

                            sDocumentName = gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.PatientLetters.ToString() & "_" & sEnum & "_" & sDocumentDate
                            dtTVP.Rows.Add(GetDataRow(gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.PatientLetters.ToString(), System.Convert.ToString(nID), System.Convert.ToString(OrderID), System.Convert.ToString(OrderNumber), String.Empty, String.Empty, dtTVP, sDocumentName))
                        ElseIf (Flag = "3") Then

                            sDocumentName = gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.OrdersTestResult.ToString() & "_" & sEnum & "_" & sDocumentDate
                            dtTVP.Rows.Add(GetDataRow(gloUIControlLibrary.Classes.ClinicalChartQueue.enumDocType.OrdersTestResult.ToString(), System.Convert.ToString(nID), System.Convert.ToString(OrderID), System.Convert.ToString(OrderNumber), String.Empty, String.Empty, dtTVP, sDocumentName))

                        End If

                        'End If
                    End If
                End If
                prgGeneratefile.Value = i
                Application.DoEvents()
            Next

            If appSettings("UserID") IsNot Nothing Then nUserID = System.Convert.ToInt64(appSettings("UserID"))
            If rbPrintData.Checked Then
                nClaimPrintType = ClaimPrintType.PrintData
            ElseIf rbPrintOnForm.Checked Then
                nClaimPrintType = ClaimPrintType.PrintForm
            End If

            If dtTVP IsNot Nothing Then
                If dtTVP.Rows.Count > 0 Then
                    ''added condition to check if local (TS) print is disabled
                    '   If isLocalPrint = True Then
                    Me.InsertQueueIntoDatabase(_PatientID, nUserID, Date.Now, nAssociatedContactID, sNotes, dtTVP, nClaimPrintType, sClaimPrinter, BackgroundProcessType, dtDetails)

                    '   Else
                    '   If bIsforQueue Then
                    'Me.InsertQueueIntoDatabase(_PatientID, nUserID, Date.Now, nAssociatedContactID, sNotes, dtTVP, nClaimPrintType, sClaimPrinter)
                    ' End If
                    'End If
                End If
            End If



            For i As Int64 = 0 To C1ClinicalChart.Rows.Count - 1
                Try
                    If C1ClinicalChart.GetCellCheck(System.Convert.ToInt16(i), 1) = CheckEnum.Checked Then
                        C1ClinicalChart.SetCellCheck(i, 1, CheckEnum.Unchecked)
                    End If
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
                Finally
                End Try
            Next
            Panel2.Height = 60
            pnlProgress.Dock = DockStyle.None
            Me.Cursor = Cursors.Default

            If BackgroundProcessType = gloUIControlLibrary.Classes.ClinicalChartQueue.enumBackgroundProcessType.Queue Then
                MessageBox.Show("Queued successfully.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                'ElseIf BackgroundProcessType = gloUIControlLibrary.Classes.ClinicalChartQueue.enumBackgroundProcessType.TsPrint Then
                '    MessageBox.Show("Sent to mapped drive for printing.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            Panel2.Height = 60
            pnlProgress.Dock = DockStyle.None
            Me.Cursor = Cursors.Default
            C1ClinicalChart.Enabled = True
            '  If Not IsNothing(EvnMsgQueue) Then
            If BackgroundProcessType = gloUIControlLibrary.Classes.ClinicalChartQueue.enumBackgroundProcessType.TsPrint Then
                RaiseEvent EvnMsgQueueforPrint()
            Else
                RaiseEvent EvnMsgQueue(BackgroundProcessType)
            End If

            '  End If
        End Try
    End Sub

    Private Function GetDataRow(ByVal sEnum As String,
                               ByVal TranID_I As String,
                               ByVal TranID_II As String,
                               ByVal TranID_III As String,
                               ByVal TranID_IV As String,
                               ByVal TranID_V As String,
                               ByVal DataTable As DataTable,
                               ByVal DocumentName As String,
                               Optional ByVal bIsforPrinting As Boolean = True
                               ) As DataRow



        Dim dRow As DataRow = DataTable.NewRow()
        dRow("sEnum") = sEnum

        dRow("sTranID_I") = System.Convert.ToString(TranID_I)
        dRow("sTranID_II") = System.Convert.ToString(TranID_II)
        dRow("sTranID_III") = System.Convert.ToString(TranID_III)
        dRow("sTranID_IV") = System.Convert.ToString(TranID_IV)
        dRow("sTranID_V") = System.Convert.ToString(TranID_V)
        dRow("bIsforPrinting") = System.Convert.ToString(bIsforPrinting)
        dRow("sDocumentName") = System.Convert.ToString(DocumentName)

        Return dRow
    End Function

    Private Sub InsertQueueIntoDatabase(ByVal PatientID As Int64, ByVal UserID As Int64, ByVal CreatedDate As Date, ByVal AssociatedContactID As Int64, ByVal Notes As String, ByVal DataTable As DataTable, ByVal ClaimPrintType As Integer, ByVal ClaimPrinter As String, Optional ByVal BackgroundProcessType As gloUIControlLibrary.Classes.ClinicalChartQueue.enumBackgroundProcessType = gloUIControlLibrary.Classes.ClinicalChartQueue.enumBackgroundProcessType.Print, Optional dtDetails As DataTable = Nothing)

        Try
            Dim strCmd As String = ""
            Dim ProcessType As String = ""
            'strCmd = "ClinicalChart_InsertQueue_ForPrint"
            'Else
            'strCmd = "ClinicalChart_InsertQueue"
            'End If
            If BackgroundProcessType = gloUIControlLibrary.Classes.ClinicalChartQueue.enumBackgroundProcessType.TsPrint Then
                strCmd = "ClinicalChart_InsertQueue_ForPrint"
            Else

                strCmd = "ClinicalChart_InsertQueue"
            End If

            ProcessType = BackgroundProcessType.GetDescription

            Using Connection As New SqlConnection(GetConnectionString())
                Using Command As New SqlCommand(strCmd, Connection)
                    With Command
                        .CommandType = CommandType.StoredProcedure

                        .Parameters.AddWithValue("@nPatientID", PatientID)
                        .Parameters.AddWithValue("@nUserID", UserID)
                        .Parameters.AddWithValue("@dtCreatedDateTime", CreatedDate)
                        .Parameters.AddWithValue("@nQueueStatus", gloUIControlLibrary.Classes.ClinicalChartQueue.enumPrintStatus.Queued)
                        .Parameters.AddWithValue("@nAssociatedContactID", AssociatedContactID)
                        .Parameters.AddWithValue("@sNotes", Notes)
                        .Parameters.AddWithValue("@dtStartDate", System.Convert.ToDateTime(mskStartDate.Text).ToString("MM/dd/yyyy"))
                        .Parameters.AddWithValue("@dtEndDate", System.Convert.ToDateTime(mskEndDate.Text).ToString("MM/dd/yyyy"))
                        .Parameters.AddWithValue("@TVP", DataTable)
                        .Parameters.AddWithValue("@ClaimPrintType", ClaimPrintType)
                        .Parameters.AddWithValue("@sClaimPrinter", ClaimPrinter)
                        If BackgroundProcessType <> gloUIControlLibrary.Classes.ClinicalChartQueue.enumBackgroundProcessType.TsPrint Then
                            .Parameters.AddWithValue("@TVP_ClinicalChartDetails", dtDetails)
                            .Parameters.AddWithValue("@BackgroundProcessType", ProcessType)
                        End If
                        .Connection.Open()
                        .ExecuteNonQuery()
                        .Connection.Close()
                    End With
                End Using
            End Using
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try


    End Sub

    Private Sub LoadPrinter()
        Try
            Dim dtClaimPrinter As DataTable = GetClaimPrinterList()
            For Each dr As DataRow In dtClaimPrinter.Rows
                cmbClaimPrinter.Items.Add(System.Convert.ToString(dr("Printer Name")))
            Next
        Catch Ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), True)
        End Try
    End Sub

    Public Function GetClaimPrinterList() As DataTable
        Dim ds As DataSet = New System.Data.DataSet()

        Try
            Using cn As New SqlConnection(GetConnectionString())
                Using cmd As New SqlCommand("SELECT bncs.sPrinterName AS [Printer Name] FROM dbo.BL_NewCMS1500PrintSettings bncs UNION SELECT bus.sPrinterName AS [Printer Name] FROM dbo.BL_UB04PrintSettings bus ORDER BY [Printer Name]", cn)
                    cmd.CommandType = System.Data.CommandType.Text
                    cmd.CommandTimeout = 0

                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(ds)
                    End Using
                End Using
            End Using
            ds.Tables(0).Rows.InsertAt(ds.Tables(0).NewRow(), 0)
            Return ds.Tables(0).Copy()
        Catch Ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), True)
            Return Nothing
        Finally
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If
        End Try
    End Function

    Private Sub rbPrintData_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbPrintData.CheckedChanged
        If rbPrintData.Checked Then
            pnlClaimPrinter.Visible = True
            Dim ops As New PrinterSettings
            cmbClaimPrinter.Text = ops.PrinterName
        ElseIf rbPrintOnForm.Checked Then
            pnlClaimPrinter.Visible = False
        End If

    End Sub
    Dim isClaimSelected As Boolean = False
    Private Function CheckPrintRestriction() As Boolean

        Dim sEnum As String = ""
        Dim strmessage As String = ""
        C1ClinicalChart.Enabled = False

        Dim isClaimDetailsSelected As Boolean = False
        Dim isOtherDocumentSelected As Boolean = False
        Me.Cursor = Cursors.WaitCursor
        Dim Flag As String
        Dim IsCheck As C1.Win.C1FlexGrid.CheckEnum

        Dim RestrictPrint As Boolean = False
        Try
            For i As Int64 = 0 To C1ClinicalChart.Rows.Count - 1
                Try
                    Flag = C1ClinicalChart.GetData(System.Convert.ToInt16(i), 4).ToString()
                    IsCheck = C1ClinicalChart.GetCellCheck(System.Convert.ToInt16(i), 1)
                    sEnum = C1ClinicalChart.GetData(System.Convert.ToInt16(i), 1).ToString()
                Catch ex As Exception
                    Flag = 0
                    IsCheck = CheckEnum.None
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
                End Try

                If rbPrintData.Checked Then
                    If IsCheck = CheckEnum.Checked Then
                        If Flag = "16" AndAlso sEnum.Contains("Claim") Then
                            isClaimDetailsSelected = True
                            isClaimSelected = True
                            If isOtherDocumentSelected = True Then
                                Exit For
                            End If
                        ElseIf (Flag <> "") AndAlso sEnum <> "Claims" Then
                            isOtherDocumentSelected = True
                            If isClaimDetailsSelected = True Then
                                Exit For
                            End If
                        End If
                    End If
                End If
            Next

            If isClaimDetailsSelected = True AndAlso isOtherDocumentSelected = True Then
                MessageBox.Show("Only Claim Data"" option can only be used to print claim(s) on pre-printed form.  " + Environment.NewLine + " " +
                                                         "For printing patient other document(s) switch to ""With Claim Form"" option.",
                                                           _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                RestrictPrint = True

            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            Me.Cursor = Cursors.Default
            C1ClinicalChart.Enabled = True
        End Try

        Return RestrictPrint

    End Function

    Private Function isValidDates() As Boolean
        Try
            If Not mskStartDate.MaskCompleted Then
                MessageBox.Show("Please enter the Start Date. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                mskStartDate.Select()
                mskStartDate.Focus()
                Return False
            ElseIf Not ValidateDate(mskStartDate) Then
                Return False
            End If
            If Not mskEndDate.MaskCompleted Then
                MessageBox.Show("Please enter the End Date. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                mskEndDate.Select()
                mskEndDate.Focus()
                Return False
            ElseIf Not ValidateDate(mskEndDate) Then
                Return False
            End If

            If System.Convert.ToDateTime(mskStartDate.Text) > System.Convert.ToDateTime(mskEndDate.Text) Then
                MessageBox.Show("Start Date cannot be greater than End Date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                mskStartDate.Select()
                mskStartDate.Focus()
                Return False
            End If

            mskDOS.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
            If mskDOS.Text.Trim <> "" Then
                mskDOS.TextMaskFormat = MaskFormat.IncludePromptAndLiterals
                If Not mskDOS.MaskFull Then
                    MessageBox.Show("Please enter the DOS Date. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    mskDOS.Select()
                    mskDOS.Focus()
                    Return False
                ElseIf Not ValidateDate(mskDOS) Then
                    Return False
                ElseIf System.Convert.ToDateTime(mskDOS.Text) < System.Convert.ToDateTime(mskStartDate.Text) OrElse System.Convert.ToDateTime(mskDOS.Text) > System.Convert.ToDateTime(mskEndDate.Text) Then
                    MessageBox.Show("Please enter the DOS Date between Start date and End Date. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    mskDOS.Select()
                    mskDOS.Focus()
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
    Private Function ValidateDate(sender As Object)
        Dim mskDate As MaskedTextBox = DirectCast(sender, MaskedTextBox)
        mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
        Dim strDate As String = mskDate.Text.Trim
        mskDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals

        Dim _isValid As Boolean = False

        If mskDate IsNot Nothing Then
            If strDate.Length > 0 Then
                _isValid = IsValid(mskDate.Text)
                If Not _isValid Then
                    MessageBox.Show("Please enter a valid date.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    mskDate.Clear()
                    mskDate.Focus()
                    Return False
                End If
            End If
        End If
        Return True
    End Function

    Private Shared Function IsValid(DateToValidate As String) As Boolean
        Dim Success As Boolean
        Try
            Dim validatedDate As DateTime
            Success = DateTime.TryParseExact(DateToValidate.ToString(), "MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.None, validatedDate)
            If Not IsNothing(validatedDate) AndAlso Success = True Then
                If validatedDate < DateTime.MaxValue AndAlso validatedDate >= System.Convert.ToDateTime("01/01/1900") Then
                    Success = True
                Else
                    Success = False

                End If
            End If
        Catch
            ' If this line is reached, an exception was thrown
            Success = False
        End Try
        Return Success
    End Function

End Class

Public Class ClaimNoteIDs

    Public Property MasterTransactionID As Int64
    Public Property ContactID As Int64
    Public Property ClaimID As String

End Class
