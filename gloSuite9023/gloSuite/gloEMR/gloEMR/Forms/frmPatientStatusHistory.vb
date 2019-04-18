''Sandip Darade to show patient status history for appointment
Public Class frmPatientStatusHistory
    Dim _nAppointMentId As Int64
    Public Sub New(ByVal nAppointMentId)
        MyBase.New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        _nAppointMentId = nAppointMentId
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmPatientStatusHistory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.AutoScroll = True
        Me.pnlGrid.AutoScroll = True
        Me.C1StatusHistory.ScrollBars = ScrollBars.Both
        Try
            gloC1FlexStyle.Style(C1StatusHistory, True)
            DesignGrid()
        Catch ex As Exception
        End Try
    End Sub

    Private Function GetstatusHistory() As DataTable

        Try

            Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
            Dim dtStatusHistory As New DataTable
            Dim _strSQL As String = ""

            ''get the status location history for given appointment 
            _strSQL = " Select  nID,time,Location,Status,stimeout,ntrackingstatus from  ( " _
      & " ( SELECT ISNULL(PatientTracking.nID,0) AS nID , ISNULL(PatientTracking.sTimeIn,'') as Time,  ISNULL(PatientTracking.sLocation ,'') as Location , ISNULL(PatientTracking.sStatus,'') AS Status, " _
      & " PatientTracking.sTimeIn as sTimeOut,  PatientTracking.nTrackingStatus,  PatientTracking.dtDate  FROM PatientTracking  WHERE   " _
      & "PatientTracking.nDTLAppointmentID = " & _nAppointMentId & " AND   PatientTracking.nTrackingStatus <> 4  ) " _
      & "UNION  ( SELECT ISNULL(PatientTracking.nID,0) AS nID , ISNULL(PatientTracking.sTimeOut,0) AS   Time, ISNULL(PatientTracking.sLocation,'') AS Location, ISNULL(PatientTracking.sStatus,'') AS Status,  " _
      & " PatientTracking.sTimeIn as sTimeOut ,  PatientTracking.nTrackingStatus,  PatientTracking.dtDate FROM PatientTracking  WHERE  " _
      & " PatientTracking.nDTLAppointmentID = " & _nAppointMentId & " AND    PatientTracking.nTrackingStatus = 4  ) " _
      & " ) dtTable ORDER BY dtDate, Time"
            oDB.Connect(False)
            oDB.Retrive_Query(_strSQL, dtStatusHistory)
            ''get all IDs for whole  history   
            Dim _PatientTrackingIds As String = ""
            For I As Int16 = 0 To dtStatusHistory.Rows.Count - 1
                If (I = 0) Then
                    _PatientTrackingIds = Convert.ToString(dtStatusHistory.Rows(I)("nID"))
                Else
                    _PatientTrackingIds += "," & Convert.ToString(dtStatusHistory.Rows(I)("nID"))
                End If
            Next
            Dim dtUsers As New DataTable
            ''get users who were assigned a task 
            _strSQL = " SELECT    ISNULL(PatientTracking_DTL.nUserID,0) AS nUserID, ISNULL(User_MST.sLoginName,'' ) AS sLoginName,ISNULL(PatientTracking_DTL.nID,0) AS nID  " _
                   & " FROM  PatientTracking_DTL INNER JOIN User_MST ON PatientTracking_DTL.nUserID = User_MST.nUserID  WHERE PatientTracking_DTL.nID IN ( " & _PatientTrackingIds & ")    "
            oDB.Connect(False)
            oDB.Retrive_Query(_strSQL, dtUsers)
            Dim col_user As New DataColumn
            col_user.Caption = "User"
            col_user.ColumnName = "User"
            col_user.DataType = GetType(System.String)
            dtStatusHistory.Columns.Add(col_user)

            For i As Int16 = 0 To dtStatusHistory.Rows.Count - 1
                Dim _User As String = ""
                For j As Int16 = 0 To dtUsers.Rows.Count - 1

                    If (Convert.ToInt64(dtStatusHistory.Rows(i)("nID")) = Convert.ToInt64(dtUsers.Rows(j)("nID"))) Then
                        If (_User = "") Then
                            _User = Convert.ToString(dtUsers.Rows(j)("sLoginName"))
                        Else
                            ''Dhruv -> Kept a Space(", ") only for the multiline tooltip and Check for the mousemove Event
                            _User += ", " & Convert.ToString(dtUsers.Rows(j)("sLoginName"))
                        End If


                    End If

                Next

                dtStatusHistory.Rows(i)("User") = _User

                If Convert.ToInt64(dtStatusHistory.Rows(i)("nTrackingStatus")) = 3 Then '' appointment has status as  Check In

                    dtStatusHistory.Rows(i)("Status") = "Check In"

                ElseIf Convert.ToInt64(dtStatusHistory.Rows(i)("nTrackingStatus")) = 4 Then  ''appointment has status as  Check Out

                    dtStatusHistory.Rows(i)("Status") = "Check Out"

                ElseIf Convert.ToInt64(dtStatusHistory.Rows(i)("nTrackingStatus")) = 1 Then   ''appointment has status as  registered

                    dtStatusHistory.Rows(i)("Status") = "Registered"


                End If
            Next

            Return dtStatusHistory

        Catch ex As Exception
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, "Patinet status history viewed ", gloAuditTrail.ActivityOutCome.Failure)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, "Patinet status history viewed ", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            Return Nothing

        End Try
    End Function

    Private Sub DesignGrid()
        Try
            Dim dt As DataTable
            dt = GetstatusHistory()
            Dim _dv
            _dv = dt.DefaultView
            If _dv IsNot Nothing Then
                'C1StatusHistory.Clear()
                C1StatusHistory.DataSource = Nothing
                C1StatusHistory.DataSource = _dv

                C1StatusHistory.Cols(0).Visible = False
                C1StatusHistory.Cols(1).Visible = True
                C1StatusHistory.Cols(2).Visible = True
                C1StatusHistory.Cols(3).Visible = True
                C1StatusHistory.Cols(4).Visible = False
                C1StatusHistory.Cols(5).Visible = False
                C1StatusHistory.Cols(6).Visible = True
                '' Dim width As Integer = C1StatusHistory.Width - 0.098
                ''Dim width As Integer = pnlGrid.Width - 0.075
                Dim width As Integer = C1StatusHistory.Width
                C1StatusHistory.Cols(0).Width = 0
                C1StatusHistory.Cols(1).Width = width * 0.15
                C1StatusHistory.Cols(2).Width = width * 0.15
                C1StatusHistory.Cols(3).Width = width * 0.3
                C1StatusHistory.Cols(4).Width = 0
                C1StatusHistory.Cols(5).Width = 0
                C1StatusHistory.Cols(6).Width = width * 0.4
                C1StatusHistory.ScrollBars = ScrollBars.Horizontal
                C1StatusHistory.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns
                C1StatusHistory.Cols(0).Visible = False
                C1StatusHistory.Cols(1).AllowResizing = True
                C1StatusHistory.Cols(2).AllowResizing = True
                C1StatusHistory.Cols(3).AllowResizing = True
                C1StatusHistory.Cols(4).Visible = False
                C1StatusHistory.Cols(5).Visible = False
                C1StatusHistory.Cols(6).AllowResizing = True
                C1StatusHistory.ShowCellLabels = False
                C1StatusHistory.ScrollBars = ScrollBars.Vertical
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Me.Close()
    End Sub

    Private Sub C1StatusHistory_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1StatusHistory.MouseMove

        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location) ''Dhruv -> Used for the Multiline tooltip
    End Sub
End Class