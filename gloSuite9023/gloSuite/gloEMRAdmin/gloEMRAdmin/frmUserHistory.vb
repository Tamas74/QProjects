Imports System.Data.SqlClient
Public Class frmUserHistory
    Dim _nUserID As Int64
    Public Sub New(ByVal nUserID As Int64)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        _nUserID = nUserID

    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        If Not IsNothing(c1userRights) Then
            c1userRights.Dispose()
        End If
        If Not IsNothing(C1userRightsHistory) Then
            C1userRightsHistory.Dispose()
        End If
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub frmUserHistory_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        gloC1FlexStyle.Style(C1userRightsHistory)
        Try
            Dim username As String = GetUserName()
            If username <> "" Then
                Me.Text = "[View History - " & username & "]"
            End If
            Fill_UserRightsHistory()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Fill_UserRightsHistory()

        Dim dt As DataTable = Nothing
        Dim _dtUserRights As DataTable = Nothing
        Dim _dtUserRightsHistory As DataTable = Nothing

        Try
            c1userRights.BeginUpdate()
            dt = ShowUserRightsAudit()
            If dt.Rows.Count > 0 Then

                Dim dvUserRights As DataView
                'Dim dvUserRightsHistory As DataView

                dvUserRights = New DataView(dt, "Activity= 'Insert' ", "Activity", DataViewRowState.CurrentRows)

                If dvUserRights.Count > 0 Then
                    c1userRights.DataSource = dvUserRights
                End If

                c1userRights.EndUpdate()
                C1userRightsHistory.BeginUpdate()
                C1userRightsHistory.DataSource = dt
                C1userRightsHistory.EndUpdate()

                If C1userRightsHistory.Rows.Count > 0 Then
                    DesignGrid()
                End If

                If c1userRights.Rows.Count > 0 Then

                    c1userRights.Cols("Activity Date").Visible = False
                    c1userRights.Cols("Activity").Visible = False
                    c1userRights.Cols("Rights Removed").Visible = False
                    c1userRights.Cols("Audit Rights Removed").Visible = False

                    DesignUserRightsGrid()
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(dt) Then
                dt = Nothing
            End If
            If Not IsNothing(_dtUserRights) Then
                _dtUserRights = Nothing
            End If
            If Not IsNothing(_dtUserRightsHistory) Then
                _dtUserRightsHistory = Nothing
            End If
        End Try
    End Sub

    Private Sub DesignGrid()
        Try
            C1userRightsHistory.Cols("Activity Date").DataType = GetType(System.String)
            C1userRightsHistory.Cols("Activity Date").Width = c1userRights.Width * 10 / 100
            C1userRightsHistory.AllowSorting = True
            With C1userRightsHistory

                For i As Int16 = 0 To .Cols.Count - 1
                    .Cols(i).AllowEditing = False
                Next

                .AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn

            End With
            C1userRightsHistory.Cols(0).Format = "MM/dd/yyyy hh:mm tt"
            C1userRightsHistory.Cols(0).Width = 148
            C1userRightsHistory.Cols(1).Width = 115
            C1userRightsHistory.Cols(2).Width = 70
            C1userRightsHistory.Cols(3).Width = 80
            C1userRightsHistory.Cols(7).Width = 120
            C1userRightsHistory.Cols(9).Width = 150


            With C1userRightsHistory
                'For _rowIndex As Int16 = 1 To C1userRightsHistory.Rows.Count - 1
                '    .Rows(_rowIndex).Height = 40
                'Next
                If C1userRightsHistory.Rows.Count > 1 Then

                    Dim cStyle As C1.Win.C1FlexGrid.CellStyle
                    '  cStyle = .Styles.Add("WordWrap")
                    Try
                        If (.Styles.Contains("WordWrap")) Then
                            cStyle = .Styles("WordWrap")
                        Else
                            cStyle = .Styles.Add("WordWrap")

                        End If
                    Catch ex As Exception
                        cStyle = .Styles.Add("WordWrap")

                    End Try
                    cStyle.WordWrap = True
                    cStyle.Trimming = StringTrimming.EllipsisCharacter
                End If
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub DesignUserRightsGrid()
        Try
            c1userRights.Cols("Activity Date").DataType = GetType(System.String)
            c1userRights.Cols("Activity Date").Width = c1userRights.Width * 10 / 100
            c1userRights.Cols(1).Width = 115
            c1userRights.Cols(7).Width = 120
            c1userRights.Cols(2).Width = 70
            c1userRights.Cols(3).Width = 80
            c1userRights.Cols(9).Width = 250
            c1userRights.ShowCellLabels = False
            c1userRights.AllowSorting = True
            c1userRights.AllowEditing = False
            With c1userRights

                For i As Int16 = 0 To .Cols.Count - 1
                    .Cols(i).AllowEditing = False
                Next

                .AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None

            End With

            c1userRights.Cols(0).Format = "MM/dd/yyyy hh:mm tt"
            c1userRights.Cols(0).Width = 148
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Function ShowUserRightsAudit() As DataTable

        Dim oDB As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim oDBParameters As gloDatabaseLayer.DBParameters = Nothing
        Dim dt As DataTable = Nothing

        Try

            oDB.Connect(False)
            oDBParameters = New gloDatabaseLayer.DBParameters
            oDBParameters.Add("@nUserID", _nUserID, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Retrive("gsp_getAuditofuserRights", oDBParameters, dt)

        Catch ex As Exception


            MessageBox.Show("Error on Immunization." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If oDBParameters IsNot Nothing Then
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If

            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB = Nothing
            End If
        End Try
        Return dt

    End Function

    Private Sub c1userRights_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles c1userRights.MouseMove
        Try
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, CType(sender, C1.Win.C1FlexGrid.C1FlexGrid), e.Location)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub C1userRightsHistory_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles C1userRightsHistory.MouseMove
        Try
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, CType(sender, C1.Win.C1FlexGrid.C1FlexGrid), e.Location)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Function GetUserName() As String
        Dim conn As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString)
        Dim cmd As SqlCommand
        Dim _strSQL As String = ""
        Dim sloginname As String
        Try
            conn.Open()
            _strSQL = "select sloginname from User_MST where nuserID = " & _nUserID & ""

            cmd = New SqlCommand(_strSQL, conn)
            sloginname = cmd.ExecuteScalar
            conn.Close()

            Return sloginname
        Catch ex As Exception
            ' MsgBox(ex.Message)
        End Try
    End Function
End Class