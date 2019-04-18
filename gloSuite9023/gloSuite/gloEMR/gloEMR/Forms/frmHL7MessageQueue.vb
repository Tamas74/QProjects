Imports System.IO
Imports System.Text
Imports gloUserControlLibrary
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient


Public Class frmHL7MessageQueue
    WithEvents ogloUCSearchctrl As gloUC_CustomSearchInC1Flexgrid
    Dim dtHL7MsgQueue As DataTable

    Private Sub frmHL7MessageQueue_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If IsNothing(ogloUCSearchctrl) = False Then
            pnlReceivedTriage1.Controls.Remove(ogloUCSearchctrl)
            ogloUCSearchctrl.Dispose()
            ogloUCSearchctrl = Nothing
        End If
        If Not IsNothing(dtHL7MsgQueue) Then
            dtHL7MsgQueue.Dispose()
            dtHL7MsgQueue = Nothing
        End If
    End Sub


    Private Sub frmHL7MessageQueue_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        LoadData()
    End Sub


    Private Sub LoadData()
        Try
            FillData()

            If Not IsNothing(dtHL7MsgQueue) Then
                ' If dtHL7MsgQueue.Rows.Count > 0 Then
                If IsNothing(ogloUCSearchctrl) = False Then
                    pnlReceivedTriage1.Controls.Remove(ogloUCSearchctrl)
                    ogloUCSearchctrl.Dispose()
                    ogloUCSearchctrl = Nothing
                End If
                ogloUCSearchctrl = New gloUC_CustomSearchInC1Flexgrid(dtHL7MsgQueue, False, False)

                

                ogloUCSearchctrl.Dock = DockStyle.Fill
                pnlReceivedTriage1.Controls.Add(ogloUCSearchctrl)

                'To make Add_new ,OK ,Modify and Close btns as Hidden ...
                ogloUCSearchctrl.pic_ADDNew.Visible = False
                ogloUCSearchctrl.pic_OK.Visible = False
                ogloUCSearchctrl.pic_Modify.Visible = False
                ogloUCSearchctrl.pnl_Close.Visible = False


                'Code Start-Added by Sandip Deshmukh on 20100112 for adjusting size of C1 flex grid
                'ogloUCSearchctrl._UCflex.AutoResize = True
                ogloUCSearchctrl._UCflex.ExtendLastCol = True
                ogloUCSearchctrl._UCflex.Refresh()
                Dim _ColWidth = (pnlReceivedTriage1.Width - 10) / ogloUCSearchctrl._UCflex.Cols.Count
                For Each ocol As C1.Win.C1FlexGrid.Column In ogloUCSearchctrl._UCflex.Cols
                    ocol.Width = _ColWidth
                Next

                Try
                    ''Added by Abhijeet on 20101115 for bug no 4195
                    If ogloUCSearchctrl._UCflex.Cols.Count > 0 Then
                        Dim totCol As Int32 = 0
                        For cnt As Int32 = 0 To ogloUCSearchctrl._UCflex.Cols.Count - 1
                            ogloUCSearchctrl._UCflex.Cols(cnt).WidthDisplay = 150
                        Next
                    End If
                    ogloUCSearchctrl._UCflex.ExtendLastCol = True
                    ''end of changes by Abhijeet on 20101115 for bug no 4195
                Catch ex As Exception
                End Try

                'Code End-Added by Sandip Deshmukh on 20100112 for adjusting size of C1 flex grid

                'Else
                ''If Not IsNothing(ogloUCSearchctrl) Then
                ''    pnlReceivedTriage1.Controls.Remove(ogloUCSearchctrl)
                ' ogloUCSearchctrl = New gloUC_CustomSearchInC1Flexgrid(dtHL7MsgQueue, False, False)

                ''End If
                ' End If
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Public Sub FillData()

        Dim conn As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing
        Dim da As New SqlDataAdapter

        Dim _strSQL As String = ""

        Try

            '_strSQL = "SELECT  HL7_MessageQueue.dtDateTimeStamp, HL7_MessageQueue.sMessageName, HL7_MessageQueue.sMachineName AS sMachineName,HL7_MessageQueue.nID, case HL7_MessageQueue.nStatus when 1 then 'Queue'when 2 then 'InProcess' when 3 then 'Processed' end as 'Status',  Patient.nPatientID AS nPatientID,isnull( Patient.sPatientCode,'') as  sPatientCode , isnull(Patient.sFirstName, '') + ' ' + isnull(Patient.sMiddleName,'') + ' ' + isnull(Patient.sLastName,'') AS PatientName  FROM Patient INNER JOIN HL7_MessageQueue ON Patient.nPatientID = HL7_MessageQueue.nPatientID "

            'Commnted by shubhangi 20091013
            '_strSQL = "SELECT  HL7_MessageQueue.dtDateTimeStamp,Case HL7_MessageQueue.sMessageName when 'A08' then 'Patient Modification' when 'A04' then 'Patient registration' when 'o01' then 'Lab Order' when 'p03' then 'Charges' end as sMesageName, HL7_MessageQueue.sMachineName AS sMachineName,HL7_MessageQueue.nID, case HL7_MessageQueue.nStatus when 1 then 'Queue'when 2 then 'InProcess' when 3 then 'Processed' end as 'Status',Patient.nPatientID AS nPatientID,isnull( Patient.sPatientCode,'') as  sPatientCode , isnull(Patient.sFirstName, '') + ' ' + isnull(Patient.sMiddleName,'') + ' ' + isnull(Patient.sLastName,'') AS PatientName FROM Patient INNER JOIN HL7_MessageQueue ON Patient.nPatientID = HL7_MessageQueue.nPatientID"

            'Shubhangi 20091013
            'Add one more column B'coz we wnat general search on date column aloso. So add date column by converting date into varchar & make that column visible false
            '_strSQL = "SELECT Patient.nPatientID as PatientID, HL7_MessageQueue.dtDateTimeStamp as DateTimeStamp ,Case HL7_MessageQueue.sMessageName when 'A08' then 'Patient Modification' when 'A04' then 'Patient registration' when 'o01' then 'Lab Order' when 'p03' then 'Charges' end as 'HL7Message', HL7_MessageQueue.sMachineName AS 'MachineName',isnull( Patient.sPatientCode,'') as  'PatientCode' , isnull(Patient.sFirstName, '') + ' ' + isnull(Patient.sMiddleName,'') + ' ' + isnull(Patient.sLastName,'') AS 'Patient', case HL7_MessageQueue.nStatus when 1 then 'Queue'when 2 then 'InProcess' when 3 then 'Processed' end as 'Status',convert(varchar,HL7_MessageQueue.dtDateTimeStamp,101) as Date FROM Patient INNER JOIN HL7_MessageQueue ON Patient.nPatientID = HL7_MessageQueue.nPatientID "

            'Code Added by kanchan on 20100112 for Appointment outbound message description
            '_strSQL = "SELECT Patient.nPatientID as PatientID, HL7_MessageQueue.dtDateTimeStamp as DateTimeStamp ,Case HL7_MessageQueue.sMessageName when 'A08' then 'Patient Modification' when 'A04' then 'Patient Registration' when 'O01' then 'Lab Order' when 'P03' then 'Charges' when 'S12' then 'Appointment Registration' when 'S13' then 'Appointment Modification' when 'S15' then 'Appointment Deletion' when 'A28' then 'Patient Information' end as 'HL7Message', HL7_MessageQueue.sMachineName AS 'MachineName',isnull( Patient.sPatientCode,'') as  'PatientCode' , isnull(Patient.sFirstName, '') + ' ' + isnull(Patient.sMiddleName,'') + ' ' + isnull(Patient.sLastName,'') AS 'Patient', case HL7_MessageQueue.nStatus when 1 then 'Queue'when 2 then 'InProcess' when 3 then 'Processed' end as 'Status',convert(varchar,HL7_MessageQueue.dtDateTimeStamp,101) as Date FROM Patient INNER JOIN HL7_MessageQueue ON Patient.nPatientID = HL7_MessageQueue.nPatientID "


            'Code commented & added on 20100918
            '_strSQL = "SELECT Patient.nPatientID as PatientID, HL7_MessageQueue.dtDateTimeStamp as DateTimeStamp ,Case HL7_MessageQueue.sMessageName when 'A08' then 'Patient Modification' when 'A04' then 'Patient Registration' when 'O01' then 'Lab Order' when 'P03' then 'Charges' when 'S12' then 'Appointment Registration' when 'S13' then 'Appointment Modification' when 'S15' then 'Appointment Deletion' when 'V04' then 'Immunization Insertion' end as 'HL7Message', HL7_MessageQueue.sMachineName AS 'MachineName',isnull( Patient.sPatientCode,'') as  'PatientCode' , isnull(Patient.sFirstName, '') + ' ' + isnull(Patient.sMiddleName,'') + ' ' + isnull(Patient.sLastName,'') AS 'Patient', case HL7_MessageQueue.nStatus when 1 then 'Queue'when 2 then 'InProcess' when 3 then 'Processed' end as 'Status',convert(varchar,HL7_MessageQueue.dtDateTimeStamp,101) as Date FROM Patient INNER JOIN HL7_MessageQueue ON Patient.nPatientID = HL7_MessageQueue.nPatientID"
            _strSQL = "SELECT Patient.nPatientID as PatientID, HL7_MessageQueue.dtDateTimeStamp as DateTimeStamp ," _
            & " Case HL7_MessageQueue.sMessageName when 'A08' then 'Patient Modification' when 'A04' then 'Patient Registration' " _
            & " when 'O01' then 'Lab Order' when 'P03' then 'Charges' when 'S12' then 'Appointment Registration' " _
            & " when 'S13' then 'Appointment Modification' when 'S15' then 'Appointment Deletion' " _
            & " when 'V04' then 'Immunization Insertion' when 'A28' then 'Patient Information' end as 'HL7Message', HL7_MessageQueue.sMachineName AS 'MachineName'," _
            & " isnull( Patient.sPatientCode,'') as  'PatientCode' , isnull(Patient.sFirstName, '') + ' ' " _
            & " + isnull(Patient.sMiddleName,'') + ' ' + isnull(Patient.sLastName,'') AS 'Patient', " _
            & " case HL7_MessageQueue.nStatus when 1 then 'Queue'when 2 then 'InProcess' when 3 then 'Processed' end as 'Status'," _
            & " convert(varchar,HL7_MessageQueue.dtDateTimeStamp,101) as Date FROM Patient INNER JOIN HL7_MessageQueue " _
            & " ON Patient.nPatientID = HL7_MessageQueue.nPatientID"
            cmd = New SqlCommand(_strSQL, conn)

            da = New SqlDataAdapter(cmd)
            If (IsNothing(dtHL7MsgQueue) = False) Then
                dtHL7MsgQueue.Dispose()
                dtHL7MsgQueue = Nothing
            End If
            dtHL7MsgQueue = New DataTable
            da.Fill(dtHL7MsgQueue)
            da.Dispose()
            da = Nothing
        Catch ex As Exception
            MsgBox(ex.Message)

        Finally

            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try


    End Sub

    Private Sub tlsTriage_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsTriage.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Refresh"
                If Not IsNothing(ogloUCSearchctrl) Then
                    pnlReceivedTriage1.Controls.Remove(ogloUCSearchctrl)
                    ogloUCSearchctrl.Dispose()
                    ogloUCSearchctrl = Nothing
                    LoadData()

                End If
            Case "Close"
                Me.Close()
        End Select
    End Sub

End Class