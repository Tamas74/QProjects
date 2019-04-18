Imports gloCCDLibrary
Imports System.IO
Imports gloUserControlLibrary
Imports C1.Win.C1FlexGrid

Public Class frmVW_CCDDiagnosisOneFiles
    Inherits System.Windows.Forms.Form
    Implements IPatientContext
    Const COL_CDSFILEId As Integer = 0
    Dim nPatientID As Int64
    Dim dtCCDResponse As DataTable
    Private WithEvents ogloUC_generalsearch As gloUserControlLibrary.gloUCGeneralSearch

    Private Sub frmVW_CCDDiagnosisOneFiles_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub frmVW_CCDDiagnosisOneFiles_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If IsNothing(dtCCDResponse) = False Then
            dtCCDResponse.Dispose()
            dtCCDResponse = Nothing
        End If
    End Sub
    Private Sub frmVW_CCDDiagnosisOneFiles_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            gloPatient.gloPatient.GetWindowTitle(Me, nPatientID, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

        InitiliseSearchControl(dtCCDResponse)
        FillCDSFiles()

    End Sub

    Public Sub New(ByVal PatientID As Int64)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        nPatientID = PatientID
    End Sub

    Private Sub tlbbtn_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbbtn_Close.Click
        Me.Close()
    End Sub

    Private Sub tlbbtn_Refresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbbtn_Refresh.Click
        FillCDSFiles()
    End Sub
    Public Function FillCDSFiles()
        Dim oDiagnosisOneDB As New clsDiagnosisone_DBLayer
        dtCCDResponse = oDiagnosisOneDB.GetCCDDiagnosisOneFiles(nPatientID)
        C1DiagnosisOneCCDResponse.DataSource = dtCCDResponse
        C1DiagnosisOneCCDResponse.Cols(0).Visible = False
        C1DiagnosisOneCCDResponse.Cols(1).Visible = False
        oDiagnosisOneDB = Nothing
        DesignC1DiagnosisOneCCDResponseGrid()
        ogloUC_generalsearch.IntialiseDatatable(dtCCDResponse)
        C1DiagnosisOneCCDResponse.RowSel = 0
        Return Nothing
    End Function

    Private Sub C1DiagnosisOneCCDResponse_DoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1DiagnosisOneCCDResponse.DoubleClick
        Try
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = C1DiagnosisOneCCDResponse.HitTest(ptPoint)
            Dim strFileName As String
            ' Dim HitRow As Integer
            If htInfo.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.Cell Then
                Me.Cursor = Cursors.WaitCursor
                Dim oResult As New Object
                Dim _CDSfiles As Int64 = C1DiagnosisOneCCDResponse.GetData(C1DiagnosisOneCCDResponse.Row, COL_CDSFILEId)
                Dim oDiagnosisOneDB As New clsDiagnosisone_DBLayer
                Dim oDiagnosisOneRequest As New clsDiagnosisone_BusuinessLayer
                dtCCDResponse = oDiagnosisOneDB.GetCCDDiagnosisOneFiles(nPatientID, _CDSfiles)
                oResult = CType(dtCCDResponse.Rows(0)("ResponseFile"), Object)
                If oResult Is Nothing Then
                    Exit Sub
                Else

                End If
                If IsDBNull(oResult) = False Then
                    strFileName = ExamNewFaxFileName(gloSettings.FolderSettings.AppTempFolderPath, ".xml")
                    '' generate Physical file
                    strFileName = oDiagnosisOneRequest.GenerateFile(oResult, strFileName)

                    Dim objfrm As New frmCCDForm
                    ''Added On 20100729 by sanjog for validate the File Path
                    Dim ofile1 As FileInfo = New FileInfo(strFileName)
                    objfrm.WebBrowser1.Navigate(ofile1.FullName)
                    ''Added On 20100729 by sanjog for validate the File Path
                    Me.Focus()
                    objfrm.isCDS = True
                    objfrm.Text = "Preview of Diagnosis One Response"
                    objfrm.ShowInTaskbar = False
                    'objfrm.Parent = Me
                    objfrm.StartPosition = FormStartPosition.CenterScreen
                    objfrm.ShowDialog(IIf(IsNothing(objfrm.Parent), Me, objfrm.Parent))
                    objfrm.Dispose()
                    objfrm = Nothing
                    oResult = Nothing
                    oDiagnosisOneDB = Nothing
                    oDiagnosisOneRequest = Nothing
                    If (IO.File.Exists(strFileName) = True) Then
                        IO.File.Delete(strFileName)
                    End If
                    Me.Cursor = Cursors.Default
                Else
                    Dim SErrorText As String = CType(dtCCDResponse.Rows(0)("sErrorString"), String)
                    Dim ofrmMessageBox As New FrmErrorDetailsMessageBox("The following error occurred while receiving CDS file.", SErrorText)
                    ofrmMessageBox.ShowDialog(IIf(IsNothing(ofrmMessageBox.Parent), Me, ofrmMessageBox.Parent))
                    ofrmMessageBox.Dispose()
                    ofrmMessageBox = Nothing
                End If
                Me.Cursor = Cursors.Default
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CDS, gloAuditTrail.ActivityCategory.ViewCDS, gloAuditTrail.ActivityType.View, ex.Message & " at " & ex.StackTrace, gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub DesignC1DiagnosisOneCCDResponseGrid()
        C1DiagnosisOneCCDResponse.AllowEditing = False
        C1DiagnosisOneCCDResponse.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        C1DiagnosisOneCCDResponse.Cols(2).DataType = GetType(System.String)
        C1DiagnosisOneCCDResponse.SetData(0, 2, "Date Time Stamp")
        C1DiagnosisOneCCDResponse.Cols(2).Width = 200
        C1DiagnosisOneCCDResponse.Cols(3).Width = 400
        C1DiagnosisOneCCDResponse.Cols(4).Width = 400
        C1DiagnosisOneCCDResponse.Cols(3).TextAlignFixed = TextAlignEnum.CenterCenter
        C1DiagnosisOneCCDResponse.Cols(4).TextAlignFixed = TextAlignEnum.CenterCenter
    End Sub
    Public Sub InitiliseSearchControl(ByRef dt As DataTable)
        ogloUC_generalsearch = New gloUCGeneralSearch()
        Panel3.Controls.Add(ogloUC_generalsearch)
        ogloUC_generalsearch.Dock = DockStyle.Left
        ogloUC_generalsearch.BringToFront()

    End Sub

    Private Sub C1DiagnosisOneCCDResponse_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1DiagnosisOneCCDResponse.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub
    ''' <summary>
    ''' Property writen for Patient Context
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return nPatientID  'Curent patient variable(Local variable) for this module 
        End Get
    End Property
End Class