Public Class frmToDoList

#Region " To Do Lists Grid Column Constants "
    'Dim COLA_ENV1 As Int16 = 0
    'Dim COLA_ENV2 As Int16 = 1
    'Dim COLA_ENV3 As Int16 = 2
    'Dim COLA_ENV4 As Int16 = 3
    'Dim COLA_ENV5 As Int16 = 4
    'Dim COLA_ENV6 As Int16 = 5

    Const COLT_TODOID As Int16 = 0
    Const COLT_PatientID As Int16 = 1
    Const COLT_LocationDateTime As Int16 = 2
    Const COLT_LocationTime As Int16 = 3
    Const COLT_PatientCode As Int16 = 4
    Const COLT_PatientName As Int16 = 5
    Const COLT_Location As Int16 = 6
    Const COLT_Status As Int16 = 7

    Const COLT_COUNT As Int16 = 8
#End Region

    Private _frm As MainMenu

    Private _dtAppontmentDate As Date
    Private _Flag As Integer

    Public Sub New(ByVal dtDate As Date, ByVal Flag As Integer)
        _dtAppontmentDate = dtDate
        _Flag = Flag

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmToDoList_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            'Application.DoEvents()
            Me.Dispose()
        Catch exdispose As Exception

        End Try
    End Sub

    Private Sub frmToDoList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        gloC1FlexStyle.Style(C1ToDoList)

        Try
            Call Fill_ToDoList(_dtAppontmentDate)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Public Function Fill_ToDoList(ByVal dtAppontmentDate As Date) As Boolean
        Dim i As Integer
        Dim IsToDoListExists As Boolean
        With C1ToDoList
            C1ToDoList.Rows.Fixed = 1
            C1ToDoList.Rows.Count = 1
            C1ToDoList.Cols.Count = COLT_COUNT
            '.SetData(0, COLA_AppTime, "Time")
            '.SetData(0, COLA_PatientCode, "Code")
            '.SetData(0, COLA_PatientName, "Name")


            .SetData(0, COLT_LocationTime, "Time")
            .SetData(0, COLT_PatientName, "Patient")
            .SetData(0, COLT_Location, "Location")
            .SetData(0, COLT_Status, "Status")

            .Cols(COLT_TODOID).Width = 0
            .Cols(COLT_PatientID).Width = 0
            .Cols(COLT_LocationDateTime).Width = 0
            .Cols(COLT_LocationTime).Width = .Width * 0.2
            .Cols(COLT_PatientCode).Width = .Width * 0
            .Cols(COLT_PatientName).Width = .Width * 0.25
            .Cols(COLT_Location).Width = .Width * 0.2
            .Cols(COLT_Status).Width = .Width * 0.3


            '' Fill_TreeView_Structure(trvappointments)

            Dim obj As New clsDoctorsDashBoard
            Dim objClsTasks As New ClsTasksDBLayer
            Dim dt As New DataTable

            dt = obj.GetLocationStatus(objClsTasks.GetLoginId, dtAppontmentDate, _Flag)
            '' nID, nPatientID, sTimeIn, sLocation, sStatus, sPatientCode, PatientName, dtDate
            objClsTasks.Dispose()
            objClsTasks = Nothing
            'lblAppointments.Text = "Appointments For : " & Format(_dtAppontmentDate, "MM/dd/yyyy")

            If IsNothing(dt) = False Then
                For i = 0 To dt.Rows.Count - 1
                    .Rows.Add()
                    .SetData(.Rows.Count - 1, COLT_TODOID, dt.Rows(i)("nID"))
                    .SetData(.Rows.Count - 1, COLT_PatientID, dt.Rows(i)("nPatientID"))
                    .SetData(.Rows.Count - 1, COLT_LocationDateTime, dt.Rows(i)("dtDate"))
                    .SetData(.Rows.Count - 1, COLT_LocationTime, FormatDateTime(dt.Rows(i)("sTimeIn"), DateFormat.LongTime))
                    .SetData(.Rows.Count - 1, COLT_PatientCode, dt.Rows(i)("sPatientCode"))
                    .SetData(.Rows.Count - 1, COLT_PatientName, dt.Rows(i)("PatientName"))
                    .SetData(.Rows.Count - 1, COLT_Location, dt.Rows(i)("sLocation"))
                    .SetData(.Rows.Count - 1, COLT_Status, dt.Rows(i)("sStatus"))
                    IsToDoListExists = True
                Next
                If dt.Rows.Count <= 0 Then
                    IsToDoListExists = False
                End If
            Else
                IsToDoListExists = False
            End If
        End With

        Return IsToDoListExists
    End Function


    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_tls_Close.Click
        Try
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub C1ToDoList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1ToDoList.Click
        Try
            With C1ToDoList
                If .HitTest.Row >= 1 Then
                    If CType(Me.Owner, MainMenu).MdiChildren.Length = 0 Then
                        ''DB CType(Me.Owner, MainMenu).Get_ToDoList(.GetData(.Row, COLT_PatientID), .GetData(.Row, COLT_PatientCode))
                    End If

                End If
            End With
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub C1ToDoList_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1ToDoList.MouseUp

    End Sub
End Class