Imports C1.Win.C1FlexGrid
Public Class frmDMS_ViewReview


    Private Col_ReviwedBy As Integer = 0
    Private Col_UserName As Integer = 1
    Private Col_ReviwedDateTime As Integer = 2
    Private Col_Comments As Integer = 3
    Private Col_FullName As Integer = 4
    Private Col_Count As Integer = 5
    Private _DocFileName As Long
    Private _IsopenFrmHistory As Boolean = False
    Private _m_PatientID As Int64 = 0


#Region " Windows Form Designer generated code "

    Public Sub New(ByVal DocFileName As Long)
        MyBase.New()
        _DocFileName = DocFileName

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub
    Public Sub New(ByVal IsopenFrmHistory As Boolean, ByVal m_PatientID As Int64)
        MyBase.New()
        _IsopenFrmHistory = IsopenFrmHistory
        _m_PatientID = m_PatientID

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

#End Region

    Private Sub frmViewReview_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        gloC1FlexStyle.Style(C1viewReview)
        Try
            Dim strSelectAll As String
            Dim oDB As New gloStream.gloDataBase.gloDataBase
            Dim oDataReader As SqlClient.SqlDataReader
            ' set style for flexgrid
            Call setGridStyle()

            oDB.Connect(GetConnectionString)
            oDataReader = oDB.ReadQueryRecords("SELECT nUserID,sLoginName FROM User_MST WHERE sLoginName IS NOT NULL ORDER BY sLoginName")
            cmbUsers.Items.Add("All Users")
            If oDataReader.HasRows = True Then
                While oDataReader.Read
                    cmbUsers.Items.Add(oDataReader.Item("sLoginName"))
                End While
            End If
            oDB.Disconnect()

            If _IsopenFrmHistory = False Then
                'Fill grid with all user by default
                strSelectAll = " SELECT DMS_ReviwedDetail.ReviwedBy, DMS_ReviwedDetail.ReviwedDateTime, User_MST.sLoginName, DMS_ReviwedDetail.Comments ," _
               & " (Case Len(LTRIM(RTRIM(ISNULL(sFirstName,'')))) when 0 then '' else sFirstName +' ' end + Case Len(LTRIM(RTRIM(ISNULL(sMiddleName,'')))) when 0 then '' else sMiddleName +' ' end + Case Len(LTRIM(RTRIM(ISNULL(sLastName,'')))) when 0 then '' else sLastName +' ' end) as FullName " _
                & " FROM DMS_MST INNER JOIN " _
                                & " DMS_ReviwedDetail ON DMS_MST.DocumentID = DMS_ReviwedDetail.DocumentID INNER JOIN " _
                                & " User_MST ON DMS_ReviwedDetail.ReviwedBy = User_MST.nUserID " _
                                & " WHERE  DMS_MST.DocumentFileName = " & _DocFileName & "  ORDER BY DMS_ReviwedDetail.ReviwedDateTime DESC "

            Else
                strSelectAll = "SELECT ReviewHistory.nID, ReviewHistory.nVisitID, ReviewHistory.nUserID, ReviewHistory.dtReviewDate, ReviewHistory.sComments, " _
                               & " User_MST.sLoginName , " _
                    & " (Case Len(LTRIM(RTRIM(ISNULL(sFirstName,'')))) when 0 then '' else sFirstName +' ' end + Case Len(LTRIM(RTRIM(ISNULL(sMiddleName,'')))) when 0 then '' else sMiddleName +' ' end + Case Len(LTRIM(RTRIM(ISNULL(sLastName,'')))) when 0 then '' else sLastName +' ' end) as FullName " _
                     & " FROM ReviewHistory INNER JOIN " _
                               & "User_MST ON ReviewHistory.nUserID = User_MST.nUserID where nPatientID = " & _m_PatientID & " ORDER BY ReviewHistory.dtReviewDate DESC"

            End If
            Call FillGrid(strSelectAll)
            If (cmbUsers.Items.Count > 0) Then
                cmbUsers.SelectedIndex = 0
            End If


        Catch oError As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.View, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(WriteExceptionLog(oError, mdlGeneral.gloEMRExceptionActorType.DMS), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub setGridStyle()
        With C1viewReview
            Dim _TotalWidth As Single = .Width - 5
            'Dim cStyle As New CellStyle
            Dim StrReation As String = ""
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Count = Col_Count

            .Cols.Fixed = 0
            .AllowEditing = False




            .Cols(Col_ReviwedBy).Width = _TotalWidth * 0
            .Cols(Col_ReviwedBy).AllowEditing = False
            .SetData(0, Col_ReviwedBy, "UserID")
            .Cols(Col_ReviwedBy).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(Col_ReviwedDateTime).Width = _TotalWidth * 0.25
            .Cols(Col_ReviwedDateTime).AllowEditing = False
            .SetData(0, Col_ReviwedDateTime, "Review Date and Time")
            .Cols(Col_ReviwedDateTime).TextAlignFixed = TextAlignEnum.CenterCenter


            .Cols(Col_UserName).Width = _TotalWidth * 0.12

            .Cols(Col_UserName).AllowEditing = False
            .SetData(0, Col_UserName, "User Name")
            .Cols(Col_UserName).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(Col_Comments).Width = _TotalWidth * 0.42 '44
            .Cols(Col_Comments).AllowEditing = False
            .SetData(0, Col_Comments, "Comments")
            .Cols(Col_Comments).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(Col_FullName).Width = _TotalWidth * 0.3 '44
            .Cols(Col_FullName).AllowEditing = False
            .SetData(0, Col_FullName, "Name")
            .Cols(Col_FullName).TextAlignFixed = TextAlignEnum.CenterCenter



        End With

    End Sub

    Private Sub btnok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub cmbUsers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUsers.SelectedIndexChanged
        Try
            C1viewReview.Rows.Count = 1

            Dim StrSelect As String
            Dim strselcteduser As String = cmbUsers.SelectedItem
            strselcteduser = ReplaceSpecialCharacters(strselcteduser)
            If _IsopenFrmHistory = False Then
                If cmbUsers.SelectedIndex = 0 Then
                    ' fill grid when select all user
                    StrSelect = " SELECT     DMS_ReviwedDetail.ReviwedBy, DMS_ReviwedDetail.ReviwedDateTime, User_MST.sLoginName, DMS_ReviwedDetail.Comments , " _
                                  & " (Case Len(LTRIM(RTRIM(ISNULL(sFirstName,'')))) when 0 then '' else sFirstName +' ' end + Case Len(LTRIM(RTRIM(ISNULL(sMiddleName,'')))) when 0 then '' else sMiddleName +' ' end + Case Len(LTRIM(RTRIM(ISNULL(sLastName,'')))) when 0 then '' else sLastName +' ' end) as FullName " _
                                  & " FROM DMS_MST INNER JOIN " _
                                    & " DMS_ReviwedDetail ON DMS_MST.DocumentID = DMS_ReviwedDetail.DocumentID INNER JOIN " _
                                    & " User_MST ON DMS_ReviwedDetail.ReviwedBy = User_MST.nUserID " _
                                    & " WHERE  DMS_MST.DocumentFileName = " & _DocFileName & "  ORDER BY DMS_ReviwedDetail.ReviwedDateTime DESC "

                Else
                    'fill grid when select single user
                    StrSelect = " SELECT     DMS_ReviwedDetail.ReviwedBy, DMS_ReviwedDetail.ReviwedDateTime, User_MST.sLoginName, DMS_ReviwedDetail.Comments , " _
                 & " (Case Len(LTRIM(RTRIM(ISNULL(sFirstName,'')))) when 0 then '' else sFirstName +' ' end + Case Len(LTRIM(RTRIM(ISNULL(sMiddleName,'')))) when 0 then '' else sMiddleName +' ' end + Case Len(LTRIM(RTRIM(ISNULL(sLastName,'')))) when 0 then '' else sLastName +' ' end) as FullName " _
                  & " FROM DMS_MST INNER JOIN " _
                        & " DMS_ReviwedDetail ON DMS_MST.DocumentID = DMS_ReviwedDetail.DocumentID INNER JOIN " _
                        & " User_MST ON DMS_ReviwedDetail.ReviwedBy = User_MST.nUserID " _
                        & " WHERE  DMS_MST.DocumentFileName = " & _DocFileName & " AND User_MST.sLoginName = '" & (strselcteduser).Replace("'", "") & "'  ORDER BY DMS_ReviwedDetail.ReviwedDateTime DESC "
                    'Call FillGrid(StrSelect)
                End If
            Else
                If cmbUsers.SelectedIndex = 0 Then
                    StrSelect = "SELECT ReviewHistory.nID, ReviewHistory.nVisitID, ReviewHistory.nUserID, ReviewHistory.dtReviewDate, ReviewHistory.sComments, " _
                    & " User_MST.sLoginName , " _
                        & " (Case Len(LTRIM(RTRIM(ISNULL(sFirstName,'')))) when 0 then '' else sFirstName +' ' end + Case Len(LTRIM(RTRIM(ISNULL(sMiddleName,'')))) when 0 then '' else sMiddleName +' ' end + Case Len(LTRIM(RTRIM(ISNULL(sLastName,'')))) when 0 then '' else sLastName +' ' end) as FullName " _
                        & " FROM ReviewHistory INNER JOIN " _
                                                                  & "User_MST ON ReviewHistory.nUserID = User_MST.nUserID where nPatientID = " & _m_PatientID & " ORDER BY ReviewHistory.dtReviewDate DESC"
                Else
                    StrSelect = "SELECT ReviewHistory.nID, ReviewHistory.nVisitID, ReviewHistory.nUserID, ReviewHistory.dtReviewDate, ReviewHistory.sComments, " _
                                                                                      & " User_MST.sLoginName, " _
                  & " (Case Len(LTRIM(RTRIM(ISNULL(sFirstName,'')))) when 0 then '' else sFirstName +' ' end + Case Len(LTRIM(RTRIM(ISNULL(sMiddleName,'')))) when 0 then '' else sMiddleName +' ' end + Case Len(LTRIM(RTRIM(ISNULL(sLastName,'')))) when 0 then '' else sLastName +' ' end) as FullName " _
                   & " FROM ReviewHistory INNER JOIN " _
                                                                                      & "User_MST ON ReviewHistory.nUserID = User_MST.nUserID where nPatientID = " & _m_PatientID & " AND User_MST.sLoginName = '" & (strselcteduser).Replace("'", "") & "' ORDER BY ReviewHistory.dtReviewDate DESC"
                End If
                
            End If
            Call FillGrid(StrSelect)

        Catch oError As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.General, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(WriteExceptionLog(oError, mdlGeneral.gloEMRExceptionActorType.DMS), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Public Sub FillGrid(ByVal strQry As String)
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        'Dim StrSelect As String
        Dim dt As New DataTable
        oDB.Connect(GetConnectionString)
        dt = oDB.ReadQueryDataTable(strQry)
        oDB.Disconnect()
        Dim i As Integer
        If dt.Rows.Count > 0 Then
            For i = 0 To dt.Rows.Count - 1
                ' bind data to flexgrid
                If _IsopenFrmHistory = False Then
                    With C1viewReview
                        .Rows.Add()
                        .SetData(.Rows.Count - 1, Col_ReviwedBy, dt.Rows(i)("ReviwedBy"))
                        .SetData(.Rows.Count - 1, Col_ReviwedDateTime, dt.Rows(i)("ReviwedDateTime"))
                        .SetData(.Rows.Count - 1, Col_UserName, dt.Rows(i)("sLoginName"))
                        .SetData(.Rows.Count - 1, Col_Comments, dt.Rows(i)("Comments"))
                        .SetData(.Rows.Count - 1, Col_FullName, dt.Rows(i)("FullName"))
                    End With
                Else
                    With C1viewReview
                        .Rows.Add()
                        '.SetData(.Rows.Count - 1, Col_ReviwedBy, dt.Rows(i)("ReviwedBy"))
                        .SetData(.Rows.Count - 1, Col_ReviwedDateTime, dt.Rows(i)("dtReviewDate"))
                        .SetData(.Rows.Count - 1, Col_UserName, dt.Rows(i)("sLoginName"))
                        .SetData(.Rows.Count - 1, Col_Comments, dt.Rows(i)("sComments"))
                        .SetData(.Rows.Count - 1, Col_FullName, dt.Rows(i)("FullName"))
                    End With
                End If

            Next
        Else
            'cmbUsers.SelectedIndex = -1
        End If
    End Sub

    
    Private Sub tls_ViewReview_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tls_ViewReview.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Close"
                '  Me.Close()
                gloWord.WordDialogBoxBackgroundCloser.Close(Me)
        End Select
    End Sub

    Private Sub C1viewReview_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1viewReview.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub
End Class