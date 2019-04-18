Imports System.Data.SqlClient
Public Class frmViewPatientConfidential_New
    Implements IPatientContext
#Region "Variable Declaration"
    '''' <summary>
    ''''dhruv 20100106  
    '''' intalizing the _confidential id
    '''' </summary>
    '''' <remarks></remarks>
    'Dim _confidentialid As Int64 = 0

    ''Column declaration in the form of the integer    
    Private Const Col_nConfidentialId As Integer = 0
    Private Const Col_nPatientId As Integer = 1
    Private Const Col_sDescription As Integer = 2
    Private Const Col_nVisitID As Integer = 3
    Private Const Col_nExamId As Integer = 4
    Private Const Col_dtVisitDate As Integer = 5
    Private Const Col_IsActive As Integer = 6

    Private Const Col_COUNT As Integer = 7


    'Dim _nPatientId As Int64 = gnPatientID
    Dim _nPatientId As Int64
    Dim _nconfidentialId As Int64 = 0
    Dim _sDescription As String = ""
    Dim _nVisitId As Int64 = 0
    Dim _nExamId As Int64 = 0
    Dim _dtvisitdate As DateTime = Nothing
    Dim _isActive As String

    Dim _dv As DataView = Nothing
    Private WithEvents gloUC_PatientStrip1 As gloUserControlLibrary.gloUC_PatientStrip

#End Region
#Region "Property"
    ''' <summary>
    ''' Property writen for Patient Context
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return _nPatientId  'Curent patient variable(Local variable) for this module 
        End Get
    End Property
#End Region
#Region "Constructor"
    ''' <summary>
    ''' dhruv 20100106 
    ''' assigning the value to internal variable using the constructor
    ''' </summary>
    ''' <param name="ConfidentialId"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal ConfidentialId As Int64, ByVal PatientID As Long)
        InitializeComponent()
        ' _confidentialid = ConfidentialId
        _nPatientId = PatientID
    End Sub
#End Region

    Private Sub Designgrid()
        Try
            Dim dt As DataTable

            dt = getPatientconfidentialInfo()

            '07-Oct-14 Aniket: Resolving Bug #72996:
            If dt.Rows.Count > 0 Then
                ts_btnAdd.Visible = False
                ts_btnModify.Visible = True
            Else
                ts_btnAdd.Visible = True
                ts_btnModify.Visible = False
            End If

            If dt IsNot Nothing Then
                _dv = dt.DefaultView
            End If

            If _dv IsNot Nothing Then
                C1_ChiefComplaints.Visible = True
                'C1_ChiefComplaints.Clear()
                C1_ChiefComplaints.DataSource = Nothing
                C1_ChiefComplaints.DataSource = _dv
                C1_ChiefComplaints.Rows.Fixed = 1

                ''Assigning the name to the column.
                C1_ChiefComplaints.Cols(Col_nConfidentialId).Caption = "Confidential ID"
                C1_ChiefComplaints.Cols(Col_nConfidentialId).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

                C1_ChiefComplaints.Cols(Col_nPatientId).Caption = "Patient ID"
                C1_ChiefComplaints.Cols(Col_nPatientId).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

                C1_ChiefComplaints.Cols(Col_sDescription).Caption = "Description"
                C1_ChiefComplaints.Cols(Col_sDescription).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

                C1_ChiefComplaints.Cols(Col_nVisitID).Caption = "Visit ID"
                C1_ChiefComplaints.Cols(Col_nVisitID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

                C1_ChiefComplaints.Cols(Col_nExamId).Caption = "Exam ID"
                C1_ChiefComplaints.Cols(Col_nExamId).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

                C1_ChiefComplaints.Cols(Col_dtVisitDate).Caption = "Visit Date"
                C1_ChiefComplaints.Cols(Col_dtVisitDate).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

                C1_ChiefComplaints.Cols(Col_IsActive).Caption = "Active"
                C1_ChiefComplaints.Cols(Col_IsActive).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter


                ''Which field has to been shown.
                ''Visibility
                C1_ChiefComplaints.Cols(Col_nConfidentialId).Visible = False
                C1_ChiefComplaints.Cols(Col_nPatientId).Visible = False
                C1_ChiefComplaints.Cols(Col_sDescription).Visible = True
                C1_ChiefComplaints.Cols(Col_nVisitID).Visible = False
                C1_ChiefComplaints.Cols(Col_nExamId).Visible = False
                C1_ChiefComplaints.Cols(Col_dtVisitDate).Visible = True
                C1_ChiefComplaints.Cols(Col_IsActive).Visible = True


                ''providing the width to the c1flexgrid
                Dim nWidth As Integer = Pnl_grid.Width - 10

                'C1_ChiefComplaints.Cols.Move(Col_Visitdate, Col_ChiefComplaint)

                C1_ChiefComplaints.Cols(Col_nConfidentialId).Width = 0
                C1_ChiefComplaints.Cols(Col_nPatientId).Width = 0
                C1_ChiefComplaints.Cols(Col_sDescription).Width = nWidth / 3
                C1_ChiefComplaints.Cols(Col_nVisitID).Width = 0
                C1_ChiefComplaints.Cols(Col_nExamId).Width = 0
                C1_ChiefComplaints.Cols(Col_dtVisitDate).Width = nWidth / 3
                C1_ChiefComplaints.Cols(Col_IsActive).Width = nWidth / 3.5

                ''Setting up the property of the C1 flex grid
                C1_ChiefComplaints.ShowCellLabels = False
                C1_ChiefComplaints.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Rows
                C1_ChiefComplaints.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn
                C1_ChiefComplaints.AllowEditing = False


                ''text alignment
                C1_ChiefComplaints.Cols(Col_sDescription).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                C1_ChiefComplaints.Cols(Col_dtVisitDate).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ts_ViewButtons_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        Try

            Select Case e.ClickedItem.Tag
                Case "Add"
                    Try
                        'If CheckPatientStatus(gnPatientID) = False Then
                        '    Exit Sub
                        'End If
                        'If CheckPatientStatus(_nPatientId) = False Then
                        '    Exit Sub
                        'End If
                        If MainMenu.IsAccess(False, _nPatientId) = False Then
                            Exit Sub
                        End If
                        ''If gnVisitID = 0 Then '' condition commented by Sandip Darade for the the flow to not to use incorrect visit id 
                        'code commented and modified by dipak to remove referance of gnVisitID
                        'gnVisitID = GenerateVisitID(_nPatientId)
                        ' ''End If
                        '_nVisitId = gnVisitID
                        _nVisitId = GenerateVisitID(_nPatientId)
                        'end modification by dipak 
                        Dim oFrmPatientconfidentialInfo As New frmPatientConfidentialInfo(0, _nVisitId, _nPatientId)
                        oFrmPatientconfidentialInfo.WindowState = FormWindowState.Normal
                        oFrmPatientconfidentialInfo.StartPosition = FormStartPosition.CenterScreen
                        oFrmPatientconfidentialInfo.ShowDialog(IIf(IsNothing(oFrmPatientconfidentialInfo.Parent), Me, oFrmPatientconfidentialInfo.Parent))
                        Designgrid()
                        oFrmPatientconfidentialInfo.Dispose()
                        oFrmPatientconfidentialInfo = Nothing
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End Try


                Case "Modify"
                    ModifyConfidentialInfo()
                Case "Delete"

                    'If CheckPatientStatus(gnPatientID) = False Then
                    '    Exit Sub
                    'End If
                    'If CheckPatientStatus(_nPatientId) = False Then
                    '    Exit Sub
                    'End If
                    If MainMenu.IsAccess(False, _nPatientId) = False Then
                        Exit Sub
                    End If

                    If (C1_ChiefComplaints.Rows.Count > 1) Then
                        If MessageBox.Show("Are you sure to delete this record?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            _nconfidentialId = Convert.ToInt64(C1_ChiefComplaints.GetData(C1_ChiefComplaints.RowSel, Col_nConfidentialId))
                            deletePatientConfidentialInfo(_nconfidentialId)
                            Designgrid()
                        End If
                    End If
                Case "Refresh"
                    Designgrid()
                Case "Close"
                    Me.Close()
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

        End Try
    End Sub


#Region "Function and the subroutine"
    Private Sub ModifyConfidentialInfo()
        Try
            If (C1_ChiefComplaints.Rows.Count > 1) Then
                'If CheckPatientStatus(_nPatientId) = False Then
                '    Exit Sub
                'End If
                If MainMenu.IsAccess(False, _nPatientId) = False Then
                    Exit Sub
                End If
                ''Dhruv 20100107
                _nPatientId = Convert.ToInt64(C1_ChiefComplaints.GetData(C1_ChiefComplaints.RowSel, Col_nPatientId))
                _nconfidentialId = Convert.ToInt64(C1_ChiefComplaints.GetData(C1_ChiefComplaints.RowSel, Col_nConfidentialId))
                _sDescription = Convert.ToString(C1_ChiefComplaints.GetData(C1_ChiefComplaints.RowSel, Col_sDescription))
                _nVisitId = Convert.ToInt64(C1_ChiefComplaints.GetData(C1_ChiefComplaints.RowSel, Col_nVisitID))
                _nExamId = Convert.ToInt64((C1_ChiefComplaints.GetData(C1_ChiefComplaints.RowSel, Col_nExamId)))
                _isActive = Convert.ToString(C1_ChiefComplaints.GetData(C1_ChiefComplaints.RowSel, Col_IsActive))
                If IsDBNull(C1_ChiefComplaints.GetData(C1_ChiefComplaints.RowSel, Col_dtVisitDate)) Then
                    _dtvisitdate = Now
                Else
                    _dtvisitdate = Convert.ToString(C1_ChiefComplaints.GetData(C1_ChiefComplaints.RowSel, Col_dtVisitDate))
                End If

                _nVisitId = GenerateVisitID(_dtvisitdate, _nPatientId)

                Dim oFrmPatientConfidentialInfo As New frmPatientConfidentialInfo(_nconfidentialId, _nVisitId, _nPatientId)
                oFrmPatientConfidentialInfo.ShowDialog(IIf(IsNothing(oFrmPatientConfidentialInfo.Parent), Me, oFrmPatientConfidentialInfo.Parent))
                Designgrid()
                oFrmPatientConfidentialInfo.Dispose()
                oFrmPatientConfidentialInfo = Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub frmViewPatientConfidential_New_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.ConfidentialInformation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub frmViewPatientConfidential_New_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            CType(Me.ParentForm, MainMenu).GetPatientConfidentialInfo()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.ConfidentialInformation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    ''' <summary>
    ''' Load function
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmViewChiefComplaints_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        gloC1FlexStyle.Style(C1_ChiefComplaints)

        Set_PatientDetailStrip()
        Designgrid()
        Try
            gloPatient.gloPatient.GetWindowTitle(Me, _nPatientId, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub

    ''' <summary>
    ''' Dhruv 20100107 
    ''' To get the patient Confidential Information
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getPatientconfidentialInfo() As DataTable
        Dim oConn As SqlConnection = Nothing
        Dim oCmd As SqlCommand = Nothing

        Dim oDt As DataTable = Nothing
        Dim _QueryString As String
        Try
            '_QueryString = "Select ISNULL(nConfidentialId,0) as nConfidentialId, ISNULL(nPatientId,0) as nPatientId ,ISNULL(sDescription,'') as sDescription , ISNULL(nVisitId,0) as nVisitId ,ISNULL(nExamId,0) as nExamId ,dtVisitDate, If ISNULL(bIsActive,0) =1 then 'Active'  " _
            '                        & " FROM PatientConfidentialInfo"

            _QueryString = " Select ISNULL(nConfidentialId,0) as nConfidentialId, ISNULL(nPatientId,0) as nPatientId ,ISNULL(sDescription,'') as sDescription , ISNULL(nVisitId,0) as nVisitId ,ISNULL(nExamId,0) as nExamId ,dtVisitDate, " _
                            & " (CASE ISNULL(bIsActive,0) When 1 Then 'Active' WHEN 0 Then 'InActive' END) AS bIsActive  " _
                            & " FROM PatientConfidentialInfo Where nPatientID  = " & _nPatientId

            ''creating the object of the connection string
            '' and binding the connection string to the SqlConnection object.
            oConn = New SqlConnection()
            If Not IsNothing(oConn) Then
                oConn.ConnectionString = GetConnectionString()

                ''creating the object of the SqlCommand
                ''binding the text to the 
                oCmd = New SqlCommand()
                oCmd.Connection = oConn
                oCmd.CommandType = CommandType.Text
                oCmd.CommandText = _QueryString

                ''binding the data to the datatable and dataadapter
                oConn.Open()
                Dim oDa As SqlDataAdapter =  New SqlDataAdapter()
                oDa.SelectCommand = oCmd
                oDt = New DataTable
                oDa.Fill(oDt)
                oConn.Close()
                oDa.Dispose()
                oDa = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            If Not IsNothing(oConn) Then
                oConn.Dispose()
                oConn = Nothing
            End If

            If oCmd IsNot Nothing Then
                oCmd.Parameters.Clear()
                oCmd.Dispose()
                oCmd = Nothing
            End If
        End Try
        Return oDt
    End Function
    ''' <summary>
    ''' To Delete the patient confidential information
    ''' </summary>
    ''' <param name="patientConfidentialId"></param>
    ''' <remarks></remarks>

    Private Sub deletePatientConfidentialInfo(ByVal patientConfidentialId As Int64)
        Dim oConn As SqlConnection = Nothing
        Dim oCom As SqlCommand = Nothing
        Dim _QueryString As String
        Try
            _QueryString = "DELETE FROM PatientConfidentialInfo WHERE nConfidentialId=" & patientConfidentialId & ""

            oConn = New SqlConnection()
            If Not IsNothing(oConn) Then
                oConn.ConnectionString = GetConnectionString()

                oCom = New SqlCommand()
                oCom.Connection = oConn
                oCom.CommandType = CommandType.Text
                oCom.CommandText = _QueryString

                oConn.Open()
                oCom.ExecuteNonQuery()
                oConn.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            If Not IsNothing(oConn) Then
                oConn.Dispose()
                oConn = Nothing

            End If

            If oCom IsNot Nothing Then
                oCom.Parameters.Clear()
                oCom.Dispose()
                oCom = Nothing
            End If
        End Try

    End Sub
    ''' <summary>
    ''' Dhruv 20100107
    ''' To search the text box
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged

        '_dv = New DataView
        _dv = DirectCast(C1_ChiefComplaints.DataSource, DataView)
        If (IsNothing(_dv) = False) Then


            C1_ChiefComplaints.DataSource = _dv

            Try
                Dim strSearch As String = txtSearch.Text.Trim()

                strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "").Replace("*", "")

                If strSearch.StartsWith("%") = True Or strSearch.StartsWith("*") = True Then
                    _dv.RowFilter = (_dv.Table.Columns("sDescription").ColumnName & " Like '%") + strSearch & "%'"
                Else
                    _dv.RowFilter = (_dv.Table.Columns("sDescription").ColumnName & " Like '%") + strSearch & "%'"
                End If

                Pnl_grid_Resize(Nothing, Nothing)

            Catch Ex As Exception
            Finally

            End Try
        End If
    End Sub
    ''' <summary>
    ''' Mouse double Click on the c1 flex grid rows 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub C1_ChiefComplaints_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1_ChiefComplaints.MouseDoubleClick
        Try
            ''return if double click not on grid row
            Dim hti As C1.Win.C1FlexGrid.HitTestInfo = C1_ChiefComplaints.HitTest(e.X, e.Y)
            If (hti.Column = 1 And hti.Row = -1 Or hti.Row = 0) Then
                C1_ChiefComplaints.Row = -1
            End If

            If (C1_ChiefComplaints.Row = -1) Then
                Return
            End If
            ''counting the row as greater then 1 becz 
            ''1st row is fixed 
            If (C1_ChiefComplaints.Rows.Count > 1) Then
                ModifyConfidentialInfo()
                '_nPatientId = Convert.ToInt64(C1_ChiefComplaints.GetData(C1_ChiefComplaints.RowSel, Col_nPatientId))
                '_nconfidentialId = Convert.ToInt64(C1_ChiefComplaints.GetData(C1_ChiefComplaints.RowSel, Col_nConfidentialId))
                '_sDescription = Convert.ToString(C1_ChiefComplaints.GetData(C1_ChiefComplaints.RowSel, Col_sDescription))
                '_nVisitId = Convert.ToInt64(C1_ChiefComplaints.GetData(C1_ChiefComplaints.RowSel, Col_nVisitID))
                '_nExamId = Convert.ToInt64((C1_ChiefComplaints.GetData(C1_ChiefComplaints.RowSel, Col_nExamId)))
                '_isActive = Convert.ToString(C1_ChiefComplaints.GetData(C1_ChiefComplaints.RowSel, Col_IsActive))
                'If IsDBNull(C1_ChiefComplaints.GetData(C1_ChiefComplaints.RowSel, Col_dtVisitDate)) Then
                '    _dtvisitdate = Now
                'Else
                '    _dtvisitdate = Convert.ToDateTime(C1_ChiefComplaints.GetData(C1_ChiefComplaints.RowSel, Col_dtVisitDate))
                'End If
                'Dim oFrmPatientConfidentialInfo As New frmPatientConfidentialInfo(_nVisitId, _nconfidentialId)
                'oFrmPatientConfidentialInfo.ShowDialog(Me)
                'Designgrid()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    ''' <summary>
    ''' dhruv 20100107 
    ''' setting up the pannel width 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Pnl_grid_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pnl_grid.Resize
        Dim nWidth As Integer = Pnl_grid.Width
        C1_ChiefComplaints.Cols(Col_sDescription).Width = nWidth / 2
        C1_ChiefComplaints.Cols(Col_dtVisitDate).Width = nWidth / 2
    End Sub
    ''' <summary>
    ''' to setup the patient detail's over the form when it loads and get active
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Set_PatientDetailStrip()
        ' '' Add Patient Details Control
        gloUC_PatientStrip1 = New gloUserControlLibrary.gloUC_PatientStrip


        ''setting the property of the control over the pannel
        gloUC_PatientStrip1.Dock = DockStyle.Top
        gloUC_PatientStrip1.Padding = New Padding(3, 0, 3, 0)

        '' Pass Paarameters Type of Form
        ''Added On 20100628 by sanjog for Patinet Control Display
        gloUC_PatientStrip1.ShowDetail(_nPatientId, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.PatientConfidentialInformation)
        Me.Controls.Add(gloUC_PatientStrip1)
        ''Added On 20100628 by sanjog for Patinet Control Display
        gloUC_PatientStrip1.BringToFront()
        gloUC_PatientStrip1.DTPValue = Format(Now, "MM/dd/yyyy")
        gloUC_PatientStrip1.DTPEnabled = False

        ''setting up the pannel 
        Pnl_main.BringToFront()
        C1_ChiefComplaints.BringToFront()
    End Sub
    ''' <summary>
    ''' using the clear button 
    ''' Clearing the value of the searchtext box
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub
    ''' <summary>
    ''' on mouse move we can check the tooltip of the rows value.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub C1_ChiefComplaints_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1_ChiefComplaints.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub
    Private Sub frmViewPatientConfidential_New_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Designgrid()
    End Sub
#End Region


End Class