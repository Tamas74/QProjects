Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class frmLab_Graphs

    Private Conn As SqlConnection
    ' Private Dv As DataView
    ' Private Cmd As System.Data.SqlClient.SqlCommand = Nothing
    Dim _flg As Boolean
    Private _PatientID As Long

    Dim _bFlg
    Dim _fromDt
    Dim _ToDt
    Dim _strTest
    Dim _strResults

    'CONSTRUCTOR CHANGED. ADD PatientID 20100925
    Public Sub New(ByVal PatientID As Long, Optional ByVal bFlg As Boolean = False, Optional ByVal fromDt As DateTime = Nothing, Optional ByVal ToDt As DateTime = Nothing, Optional ByVal strTest As String = "", Optional ByVal strResults As String = "")

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Dim sqlconn As String
        sqlconn = GetConnectionString()
        Conn = New System.Data.SqlClient.SqlConnection(sqlconn)
        'Type = ContactType
        _flg = bFlg
        _fromDt = fromDt
        _ToDt = ToDt
        _strTest = strTest
        _strResults = strResults
        _PatientID = PatientID

    End Sub

    Public Function FillControlsTest() As DataTable
        Dim Cmd As System.Data.SqlClient.SqlCommand = Nothing
        Try
            Dim adpt As New SqlDataAdapter
            Dim dt As New DataTable

            Cmd = New SqlCommand("gsp_Lab_Tests", Conn)
            Cmd.CommandType = CommandType.StoredProcedure

            Dim objParamPatientId As SqlParameter
            objParamPatientId = Cmd.Parameters.Add("@nPatientId", SqlDbType.BigInt)
            objParamPatientId.Direction = ParameterDirection.Input
            'objParamPatientId.Value = gnPatientID
            'REMOVE gnPatientID 20100925
            objParamPatientId.Value = _PatientID

            adpt.SelectCommand = Cmd
            adpt.Fill(dt)
            adpt.Dispose()
            adpt = Nothing
            objParamPatientId = Nothing
            Return dt
        Catch ex As SqlClient.SqlException
            Conn.Close()
            '     gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            '    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
            Return Nothing
        Finally
            Conn.Close()
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
    End Function

    Public Function FillControlsResult() As DataTable
      
        If cmbTests.SelectedItem.Row.ItemArray(1) > 0 Then
            Dim adpt As New SqlDataAdapter
            Dim dt As New DataTable
            Dim Cmd As System.Data.SqlClient.SqlCommand = Nothing
            Cmd = New SqlCommand("gsp_Lab_Results", Conn)
            Cmd.CommandType = CommandType.StoredProcedure

            Dim objParamPatientId As SqlParameter
            objParamPatientId = Cmd.Parameters.Add("@nPatientId", SqlDbType.BigInt)
            objParamPatientId.Direction = ParameterDirection.Input
            'objParamPatientId.Value = gnPatientID
            'REMOVE gnPatientID 20100925
            objParamPatientId.Value = _PatientID

            objParamPatientId = Cmd.Parameters.Add("@nTest", SqlDbType.BigInt)
            objParamPatientId.Direction = ParameterDirection.Input
            objParamPatientId.Value = cmbTests.SelectedItem.Row.ItemArray(1) '.ValueMember

            adpt.SelectCommand = Cmd
            adpt.Fill(dt)
            objParamPatientId = Nothing

            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            adpt.Dispose()
            adpt = Nothing
            Conn.Close()
            Return dt
        End If

        
        Return Nothing
    End Function

    Private Sub cmbTests_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTests.SelectedIndexChanged
        Try

   
            If (cmbTests.SelectedItem.Row.ItemArray(1)) > 0 Then

                Dim dt As DataTable
                Dim i As Integer = 0
                dt = FillControlsResult()

                If IsNothing(dt) = False Then
                    cmbResults.DataSource = dt
                    cmbResults.DisplayMember = dt.Columns(0).ColumnName
                    cmbResults.ValueMember = dt.Columns(1).ColumnName
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub

    Private Sub btnShowGraphs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnSave.Click
        Try

            If IsDate(dtFrom.Text) Then
                If IsDate(dtTo.Text) Then
                    If Date.Compare(dtFrom.Value, dtTo.Value) > 0 Then
                        MessageBox.Show("From-date should be less than To-Date ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        dtFrom.Focus()
                        Exit Sub
                    End If

                    If Not IsNothing(cmbTests.SelectedValue) Then
                        If Not IsNothing(cmbResults.SelectedValue) Then

                            Dim dtFromdt As DateTime
                            Dim dtTodt As DateTime

                            dtFromdt = CType(dtFrom.Text & " 12:01:00.00 AM", DateTime)
                            dtTodt = CType(dtTo.Text & " 12:01:00.00 AM", DateTime)
                            Dim dt_AllPlotValues As DataTable = fillData(dtFromdt, dtTodt, , , )

                            Dim dt_OnlyMinMax As DataTable

                            '' new data table req. for the data fill for the ranges
                            dt_OnlyMinMax = New DataTable

                            Dim clmnMin As New DataColumn
                            With clmnMin
                                .ColumnName = "Min"
                                '.DataType = System.Type.GetType("System.integer")
                            End With
                            dt_OnlyMinMax.Columns.Add(clmnMin)

                            Dim clmnMax As New DataColumn
                            With clmnMax
                                .ColumnName = "Max"
                                '.DataType = System.Type.GetType("System.Integer")
                                '.DefaultValue = strVal(1)
                            End With
                            dt_OnlyMinMax.Columns.Add(clmnMax)
                            ''''''''

                            Dim j As Integer = 0
                            Dim nActualValue = 0

                            For j = 0 To dt_AllPlotValues.Rows.Count - 1
                                'dt_MINMAX = getMinMAxRanges(dt.Rows(j)(0))  'Split(dt.Rows(j)("labotrd_ResultRange"), "-")
                                'nActualValue = getMinMAxRanges()
                                Dim strVal() As String = Nothing
                                If IsDBNull(dt_AllPlotValues.Rows(j)(0)) = False Then
                                    strVal = Split(dt_AllPlotValues.Rows(j)(0), "-")
                                End If

                                If strVal.Length <= 1 Then
                                    MessageBox.Show("No data available against selected Test ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Exit Sub
                                End If

                                Dim r As DataRow
                                r = dt_OnlyMinMax.NewRow()
                                If IsNothing(strVal) = False Then
                                    If strVal(0) = "" Then
                                        strVal(1) = "-" & strVal(1)
                                        r.Item(0) = CType(strVal(1), Integer)
                                        r.Item(1) = CType(strVal(2), Integer)
                                    Else
                                        r.Item(0) = CType(strVal(0), Integer)
                                        r.Item(1) = CType(strVal(1), Integer)
                                    End If
                                    'r.Item(0) = CType(strVal(0), Integer)
                                    'r.Item(1) = CType(strVal(1), Integer)
                                    dt_OnlyMinMax.Rows.Add(r)
                                End If
                            Next

                            Dim dt_MINMAX As DataTable = dt_OnlyMinMax

                            If dt_MINMAX.Rows.Count <= 0 Then
                                MessageBox.Show("No data available against selected Test ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                cmbTests.Focus()
                                dt_AllPlotValues.Dispose()
                                dt_OnlyMinMax.Dispose()

                                Exit Sub
                            Else
                                'Dim oGraphResult As New frmLab_GraphsResult(dtFrom.Text, dtTo.Text, cmbTests.SelectedValue, cmbResults.SelectedValue, gnPatientID, cmbTests.Text, cmbResults.Text, dt_AllPlotValues, dt_OnlyMinMax)
                                'REMOVE gnPatientID 20100925
                                Dim oGraphResult As New frmLab_GraphsResult(dtFrom.Text, dtTo.Text, cmbTests.SelectedValue, cmbResults.SelectedValue, _PatientID, cmbTests.Text, cmbResults.Text, dt_AllPlotValues, dt_OnlyMinMax)
                                With oGraphResult
                                    .MdiParent = Me.Owner
                                    .WindowState = FormWindowState.Maximized
                                    .ShowInTaskbar = False
                                    .BringToFront()
                                    .Show()
                                End With
                                Me.Close()
                            End If
                        Else
                            MessageBox.Show("Please Select Results ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            cmbResults.Focus()
                            Exit Sub
                        End If
                    Else
                        MessageBox.Show("Please Select Tests ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        cmbTests.Focus()
                        Exit Sub
                    End If
                Else
                    MessageBox.Show("Please Select Valid To Date ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    dtTo.Focus()
                    Exit Sub
                End If
            Else
                MessageBox.Show("Please Select Valid From Date ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                dtFrom.Focus()
                Exit Sub
            End If
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)


            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End Try
    End Sub

    Private Sub frmLab_Graphs_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Conn.Dispose()
        Conn = Nothing

    End Sub

    Private Sub frmLab_Graphs_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim dt As DataTable
            Dim i As Integer = 0

            dt = FillControlsTest()

            dtFrom.Value = System.DateTime.Now
            dtTo.Value = System.DateTime.Now

            If Not IsNothing(dt) Then
                cmbTests.DataSource = dt
                cmbTests.DisplayMember = dt.Columns("TestName").ColumnName
                cmbTests.ValueMember = dt.Columns("TestID").ColumnName
                If dt.Rows.Count > 0 Then
                    cmbTests.SelectedIndex = 0
                End If
            End If

            If _flg = True Then
                If Not IsNothing(_fromDt) Then
                    dtFrom.Value = _fromDt
                End If

                If Not IsNothing(_ToDt) Then
                    dtTo.Value = _ToDt
                End If

                If Not IsNothing(_strTest) Then
                    'Dim testTemp As Integer = 0

                    'For testTemp = 0 To cmbTests.Items.Count - 1
                    '    If cmbTests.Items.Item(testTemp) Then
                    cmbTests.Text = _strTest
                    'Next

                End If

                If Not IsNothing(_strResults) Then
                    cmbResults.Text = _strResults
                End If
            End If
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmbResults_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbResults.SelectionChangeCommitted
        ' MessageBox.Show("1." & cmbResults.SelectedValue & "-" & cmbResults.Text & " 2. " & cmbTests.SelectedValue & "-" & cmbTests.Text)
    End Sub

    ' bLab_Flag = false - means call from the same form
    ' bLab_Flag = true -  means call from Lab Order Form

    Public Function fillData(ByVal FrmDate As DateTime, ByVal Todate As DateTime, Optional ByVal bLab_Flag As Boolean = False, Optional ByVal LabTestId As Int64 = 0, Optional ByVal LabResultID As Int64 = 0) As DataTable
        Try
            Dim cmd = New SqlCommand

            Dim oadpt As New SqlDataAdapter
            Dim dt As New DataTable

            cmd = New SqlCommand("gsp_Lab_Graphs", Conn)
            cmd.CommandType = CommandType.StoredProcedure

            Dim objParamPatientId As SqlParameter
            objParamPatientId = cmd.Parameters.Add("@nPatientId", SqlDbType.BigInt)
            objParamPatientId.Direction = ParameterDirection.Input
            'objParamPatientId.Value = gnPatientID 'gnPatientID '391234534627492001 
            'REMOVE gnPatientID 20100925
            objParamPatientId.Value = _PatientID

            Dim objParamTestId As SqlParameter
            objParamTestId = cmd.Parameters.Add("@nTestId", SqlDbType.BigInt)
            objParamTestId.Direction = ParameterDirection.Input
            If bLab_Flag = False Then
                objParamTestId.Value = cmbTests.SelectedValue   '39221650183922105    '39221650133922105 '39221650133922103 
            Else
                objParamTestId.Value = LabTestId
            End If

            Dim objParamnResultId As SqlParameter
            objParamnResultId = cmd.Parameters.Add("@nResultId", SqlDbType.BigInt)
            objParamnResultId.Direction = ParameterDirection.Input
            If bLab_Flag = False Then
                objParamnResultId.Value = cmbResults.SelectedValue  '1
            Else
                objParamnResultId.Value = LabResultID '1
            End If

            ' false = call from same form
            ' true = call from Lab Order form

            Dim objParamFromDate As SqlParameter
            objParamFromDate = cmd.Parameters.Add("@dtFromDate", SqlDbType.DateTime)
            objParamFromDate.Direction = ParameterDirection.Input
            If bLab_Flag = False Then
                objParamFromDate.Value = CType(dtFrom.Text & " 12:01:00.00 AM", DateTime)  '"05/21/2007"
            Else
                objParamFromDate.Value = CType(System.DateTime.Today & " 12:01:00.00 AM", DateTime)  '"05/21/2007"
            End If


            Dim objParamToDate As SqlParameter
            objParamToDate = cmd.Parameters.Add("@dtTodate", SqlDbType.DateTime)
            objParamToDate.Direction = ParameterDirection.Input
            If bLab_Flag = False Then
                objParamToDate.Value = CType(dtTo.Text & " 23:59:00.00 PM", DateTime) '"07/29/2007"
            Else
                Todate = Format(Todate, "MM/dd/yyyy")
                objParamToDate.Value = CType(Todate & " 23:59:00.00 PM", DateTime) 'CType(System.DateTime.Today & " 23:59:00.00 PM", DateTime) '"07/29/2007"
            End If


            Dim objLabFlag As SqlParameter
            objLabFlag = cmd.Parameters.Add("@bLabFlg", SqlDbType.Bit)
            objLabFlag.Direction = ParameterDirection.Input
            objLabFlag.Value = bLab_Flag

            oadpt.SelectCommand = cmd
            oadpt.Fill(dt)

            objParamPatientId = Nothing
            objParamTestId = Nothing
            objParamnResultId = Nothing
            objParamFromDate = Nothing
            objParamToDate = Nothing
            objLabFlag = Nothing

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            oadpt.Dispose()
            oadpt = Nothing
            Return dt
        Catch ex As Exception
            'MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
            Return Nothing
        End Try
    End Function
   
    Private Sub cmbResults_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbResults.SelectedIndexChanged

    End Sub
End Class