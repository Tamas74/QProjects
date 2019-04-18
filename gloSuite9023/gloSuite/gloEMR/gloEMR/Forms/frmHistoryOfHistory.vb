Imports C1.Win.C1FlexGrid
Public Class frmHistoryOfHistory
    Private Col_ActivityDate As Integer = 0
    Private Col_VisitDate As Integer = 1
    Private Col_HistoryCategory As Integer = 2
    'Private Col_HistoryCategory_Hidden As Integer = 3
    Private Col_HistoryItem As Integer = 3
    Private Col_Comments As Integer = 4
    Private Col_Reaction As Integer = 5
    Private Col_Active As Integer = 6
    Private Col_Activity As Integer = 7
    Private Col_UserName As Integer = 8
    '' chetan added on 15 -nov -2010
    Private Col_FullName As Integer = 9
    '' chetan added on 15 -nov -2010
    Private Col_COUNT As Integer = 10
    Private Col_CQMCategory As Integer = 10
    Private Col_UDI As Integer = 11
    Private Col_ConcernStatus As Integer = 12
    Private Col_ResolvedEndDate As Integer = 13
    Private Col_AllergySeverity As Integer = 14
    Private Col_AllergyIntelorenceCode As Integer = 15
    Dim _PatientID As Long
#Region " Windows Form Designer generated code "

    Public Sub New(ByVal PatientID As Long)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        _PatientID = PatientID
    End Sub


  
#End Region

    Private Sub frmHistoryOfHistory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            gloC1FlexStyle.Style(C1HistoryOfHistory)
            'Aniket 21-Nov-13 Fixing Bug #59951:
            C1HistoryOfHistory.AllowSorting = AllowSortingEnum.SingleColumn
            Call setData()
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.HistoryofHistory, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Enum HistoryEnum
        sHistoryCategory
        sHistoryItem
        sComments
        sReaction
        sActivity
        dtActivityDate
        dtVisitdate
        LoginName
        FullName
        CQMCategory
        UDI
        ConcernStatus
        ResolvedEndDate
        AllergySeverity
        AllergyIntelorenceCode
    End Enum
   
 

    Public Sub setData()
        Dim dt As New DataTable
        Try
            '' Get Data form dataBase on given Patient
            
            Dim objPatientHistory As New clsPatientHistory
            dt = objPatientHistory.GetHistoyOFHistory(_PatientID)
            If IsNothing(objPatientHistory) = False Then
                objPatientHistory.Dispose()
                objPatientHistory = Nothing
            End If
          
            Dim strActivityDate As String
            Dim strVisitDate As String
            Dim strCategory As String
            Dim strhistory As String
            Dim strReaction As String = ""
            Dim strActive As String = ""
            Dim strComment As String
            Dim strRection_Status As String
            Dim strActivity As String
            Dim strUserName As String
            Dim strFullName As String
            Dim strCQMCategory As String
            Dim strUDI As String
            Dim strConcernStatus As String
            Dim strResolvedEndDate As String
            Dim strAllergySeverity As String
            Dim strAllergyIntelorenceCode As String

            For Each drHistory As DataRow In dt.Rows
                strActivityDate = drHistory(HistoryEnum.dtActivityDate)
                strVisitDate = drHistory(HistoryEnum.dtVisitdate)
                strCategory = drHistory(HistoryEnum.sHistoryCategory)
                strhistory = drHistory(HistoryEnum.sHistoryItem)
                strComment = drHistory(HistoryEnum.sComments)
                strRection_Status = drHistory(HistoryEnum.sReaction)
                strActivity = drHistory(HistoryEnum.sActivity)
                strUserName = drHistory(HistoryEnum.LoginName)
                strFullName = drHistory(HistoryEnum.FullName)
                strCQMCategory = drHistory(HistoryEnum.CQMCategory)
                strUDI = drHistory(HistoryEnum.UDI)
                strConcernStatus = drHistory(HistoryEnum.ConcernStatus)
                strResolvedEndDate = drHistory(HistoryEnum.ResolvedEndDate)
                If strResolvedEndDate = "1/1/1900" Then
                    strResolvedEndDate = ""
                End If
                strAllergySeverity = drHistory(HistoryEnum.AllergySeverity)
                strAllergyIntelorenceCode = drHistory(HistoryEnum.AllergyIntelorenceCode)
                FillGrid(strActivityDate, strVisitDate, strCategory, strhistory, strComment, strActivity, strUserName, strFullName, strRection_Status, strActive, strReaction, strCQMCategory, strUDI, strConcernStatus, strResolvedEndDate, strAllergySeverity, strAllergyIntelorenceCode)
            Next

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.HistoryofHistory, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally

        End Try
    End Sub
    Private Sub FillGrid(ByVal strActivityDate As String, ByVal strVisitDate As String, ByVal strCategory As String, ByVal strhistory As String, ByVal strComment As String, ByVal strActivity As String, ByVal strUserName As String, ByVal strFullName As String, ByVal strRection_Status As String, ByVal strActive As String, ByVal strReaction As String, ByVal strCQMCategory As String, ByVal strUDI As String, ByVal strConcernStatus As String, ByVal strResolvedEndDate As String, ByVal strAllergySeverity As String, ByVal strAllergyIntelorenceCode As String)
        Try
            Dim i As Integer
            Dim k As Integer
            With C1HistoryOfHistory
                If i = 0 Then ''  Category Is Not exists
                    .Rows.Add()
                    i = .Rows.Count - 1

                    .SetData(i, Col_ActivityDate, strActivityDate)
                    .SetData(i, Col_VisitDate, strVisitDate)
                    .SetData(i, Col_HistoryCategory, strCategory)
                    .SetData(i, Col_HistoryItem, strhistory)
                    .SetData(i, Col_Comments, strComment)
                    .SetData(i, Col_Reaction, "")
                    .SetData(i, Col_Activity, strActivity)
                    .SetData(i, Col_UserName, strUserName)
                    .SetData(i, Col_FullName, strFullName)
                    .SetData(i, Col_CQMCategory, strCQMCategory)
                    .SetData(i, Col_UDI, strUDI)
                    .SetData(i, Col_ConcernStatus, strConcernStatus)
                    .SetData(i, Col_ResolvedEndDate, strResolvedEndDate)
                    .SetData(i, Col_AllergySeverity, strAllergySeverity)
                    .SetData(i, Col_AllergyIntelorenceCode, strAllergyIntelorenceCode)

                    .Cols(Col_CQMCategory).Width = 200
                    .Cols(Col_UDI).Width = 200
                    .Cols(Col_ConcernStatus).Width = 100
                    .Cols(Col_ResolvedEndDate).Width = 150
                    .Cols(Col_AllergySeverity).Width = 130
                    .Cols(Col_AllergyIntelorenceCode).Width = 200

                    Dim cStyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                    Dim rgReaction As C1.Win.C1FlexGrid.CellRange = .GetCellRange(i, Col_Reaction, i, Col_Reaction)
                    Dim rgActive As C1.Win.C1FlexGrid.CellRange = .GetCellRange(i, Col_Active, i, Col_Active)
                    ''If the category is allergy then insert combox and checkbox in flexgrid 
                    If InStr(strCategory, "Allerg", CompareMethod.Text) = 1 Then
                        Dim arr() As String 'Srting Array
                        arr = Split(strRection_Status, "|")
                        strReaction = arr.GetValue(0)
                        If (arr.Length > 1) Then
                            strActive = arr.GetValue(1)
                        End If


                        Dim strReactions As String = " "
                        Dim objclsPatientHistory As New clsPatientHistory

                        Dim dtReaction As DataTable
                        dtReaction = objclsPatientHistory.GetAllCategory("Reaction")

                        If IsNothing(dtReaction) = False Then
                            For k = 0 To dtReaction.Rows.Count - 1
                                strReactions = strReactions & "|" & dtReaction.Rows(k)(1)
                            Next
                        End If
                        rgReaction.Style = cStyle
                        rgActive.StyleNew.DataType = GetType(Boolean)
                        rgActive.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                        rgActive.StyleNew.ImageAlign = ImageAlignEnum.CenterCenter

                        ''Added Rahul on 20101102
                        Dim arrReaction As String()
                        arrReaction = strReaction.Split(vbNewLine)

                        .Rows(i).Height = .Rows.DefaultSize * arrReaction.Length - 1
                        .SetData(i, Col_Reaction, strReaction)
                        ''

                        If strActive = "Active" Then
                            .SetCellCheck(i, Col_Active, CheckEnum.Checked)
                        End If
                        objclsPatientHistory.Dispose()
                        objclsPatientHistory = Nothing
                    Else
                        rgReaction.Style = Nothing
                        rgActive.Style = Nothing
                    End If
                    '.SetData(i + 1, Col_DrugID, intDrugID) '' CType(trvSource.SelectedNode, myTreeNode).Key())  '' DrugID
                    .Row = i
                    .Cols(Col_Reaction).AllowEditing = False
                    .Cols(Col_Active).AllowEditing = False
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub
    Private Sub tlsHistoryOFHistory_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsHistoryOFHistory.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Close"
                    Me.Close()
            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.HistoryofHistory, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
  
    Private Sub C1HistoryOfHistory_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1HistoryOfHistory.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub
End Class