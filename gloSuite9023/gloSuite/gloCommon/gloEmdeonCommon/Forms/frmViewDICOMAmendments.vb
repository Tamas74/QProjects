Imports gloDatabaseLayer
Imports System.Windows.Forms

Public Class frmViewDICOMAmendments

#Region " Private Variables "

    Private _DicomID As Int64
    Private _MessageBoxCaption As String = "gloDICOM"

#End Region

#Region " Constructors "

    Public Sub New(ByVal DicomID As Int64)
        _DicomID = DicomID
        InitializeComponent()
    End Sub

#End Region

#Region " C1 Constants "
    Private Const Col_Amend_User = 0
    Private Const Col_Amend_DateTime = 1
    Private Const Col_Amend_Description = 2
    Private Const Col_Amend_Count = 3
#End Region

    Enum AmendType
        Acknowledge = 1
        Review = 2
        Notes = 3
    End Enum

#Region " Private Functions "

    Private Function GetAmendmentDetail(ByVal aType As AmendType) As DataTable
        Dim dt As New DataTable
        Dim oDB As New DBLayer(GetConnectionString)
        Dim Query As String = ""

        Try
            ''Sandip Darade 20090914
            ''  to show acknowledgements and reviews in one grid 
            If (aType = AmendType.Acknowledge) Then
                Query = "SELECT sUserName As UserName, dtARNDateTime as AmendDate, Description FROM DICOM_AckRvwNotes WHERE (DICOMID = " & _DicomID & ") AND (nType = " & AmendType.Acknowledge & " OR  nType = " & AmendType.Review & ") order by  AmendDate"

            Else
                Query = "SELECT sUserName As UserName, dtARNDateTime as AmendDate, Description FROM DICOM_AckRvwNotes WHERE (DICOMID = " & _DicomID & " AND nType = " & aType.GetHashCode & ")"

            End If
            oDB.Connect(False)
            oDB.Retrive_Query(Query, dt)
            oDB.Disconnect()
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
            End If
        End Try

        Return dt
    End Function

    Private Sub ShowAmendments(ByVal aType As AmendType)
        Dim dt As New DataTable

        ''Design FlexGrid
        c1Amendments.Rows.Count = 1
        c1Amendments.Rows.Fixed = 1
        c1Amendments.Cols.Count = Col_Amend_Count
        c1Amendments.Cols.Fixed = 0
        c1Amendments.AllowEditing = False

        Dim nWidth = c1Amendments.Width
        c1Amendments.Cols(Col_Amend_User).Width = nWidth * 0.2
        c1Amendments.Cols(Col_Amend_Description).Width = nWidth * 0.5
        c1Amendments.Cols(Col_Amend_DateTime).Width = nWidth * 0.3

        c1Amendments.Cols(Col_Amend_User).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        c1Amendments.Cols(Col_Amend_Description).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        c1Amendments.Cols(Col_Amend_DateTime).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

        c1Amendments.SetData(0, Col_Amend_User, "User")
        c1Amendments.SetData(0, Col_Amend_DateTime, "Date / Time")
        c1Amendments.SetData(0, Col_Amend_Description, "Description")

        If aType = AmendType.Acknowledge Then
            lblDescription.Text = "  Acknowledge Description"
        ElseIf aType = AmendType.Notes Then
            lblDescription.Text = "  Note Description"
        ElseIf aType = AmendType.Review Then
            lblDescription.Text = "  Review Description"
        Else
            lblDescription.Text = "  Description"
        End If

        'Dim c1Style As C1.Win.C1FlexGrid.CellStyle
        'c1Style = c1Amendments.Styles.Add("c1Style")
        'c1Style.WordWrap = True

        Try

            dt = GetAmendmentDetail(aType)

            c1Amendments.Rows.Count = 1

            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        c1Amendments.Rows.Add()
                        c1Amendments.SetData(i + 1, Col_Amend_User, dt.Rows(i)("UserName"))
                        c1Amendments.SetData(i + 1, Col_Amend_Description, dt.Rows(i)("Description"))
                        c1Amendments.SetData(i + 1, Col_Amend_DateTime, dt.Rows(i)("AmendDate"))
                    Next
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        End Try
    End Sub

#End Region

    Private Sub frmViewDICOMAmendments_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        gloC1FlexStyle.Style(c1Amendments)
        Try
            ''Sandip Darade 2009014
            ''acknowledgements  and  reviews will be shown  together now 
            ShowAmendments(AmendType.Acknowledge)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DICOM, gloAuditTrail.ActivityCategory.Amendments, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub

#Region " ToolStrip Event "

    Private Sub tls_Amendments_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tls_Amendments.ItemClicked
        Try

      
            Select Case e.ClickedItem.Tag
                Case "Notes"
                    ShowAmendments(AmendType.Notes)
                    ''Sandip Darade 20090914
                    ''Acknowledgements and reviews  will be shown in one grid 
                Case "Acknowledge"
                    ShowAmendments(AmendType.Acknowledge)
                Case "Review"
                    ShowAmendments(AmendType.Review)
                Case "Close"
                    Me.Close()
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

    Private Sub c1Amendments_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles c1Amendments.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub
End Class