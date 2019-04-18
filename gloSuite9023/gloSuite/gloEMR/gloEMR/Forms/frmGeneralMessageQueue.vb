Imports System.IO
Imports System.Text
Imports gloUserControlLibrary
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class frmGeneralMessageQueue

    WithEvents ogloUCSearchctrl As gloUC_CustomSearchInC1Flexgrid
    Dim dtMsgQueue As DataTable
    Dim _blnFormLoading As Boolean = False

    Private Sub LoadData()
        Dim _sServiceName As String = String.Empty
        Try
            dtMsgQueue = Nothing


            If cmbServiceName.Items.Count > 0 Then
                _sServiceName = cmbServiceName.SelectedValue.ToString()
            End If

            ''Set default service name
            If _sServiceName.Length <= 0 Then
                _sServiceName = "Empty"
            End If

            FillData(_sServiceName)

            If Not IsNothing(dtMsgQueue) Then
                ' If dtHL7MsgQueue.Rows.Count > 0 Then
                ogloUCSearchctrl = New gloUC_CustomSearchInC1Flexgrid(dtMsgQueue, False, False)



                ogloUCSearchctrl.Dock = DockStyle.Fill
                pnlReceivedTriage1.Controls.Add(ogloUCSearchctrl)

                'To make Add_new ,OK ,Modify and Close btns as Hidden ...
                ogloUCSearchctrl.pic_ADDNew.Visible = False
                ogloUCSearchctrl.pic_OK.Visible = False
                ogloUCSearchctrl.pic_Modify.Visible = False
                ogloUCSearchctrl.pnl_Close.Visible = False

                ogloUCSearchctrl._UCflex.ExtendLastCol = True
                ogloUCSearchctrl._UCflex.Refresh()
                Dim _ColWidth = (pnlReceivedTriage1.Width - 10) / ogloUCSearchctrl._UCflex.Cols.Count
                For Each ocol As C1.Win.C1FlexGrid.Column In ogloUCSearchctrl._UCflex.Cols
                    ocol.Width = _ColWidth
                Next

            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try

    End Sub

    Private Sub FillData(ByVal sServiceName As String)

        Dim objDbLayer As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim _strSQL As String = String.Empty
        Try

            _strSQL = "SELECT     dbo.Patient.nPatientID AS PatientId, dbo.Gl_Messagequeue.dtDateTimeStamp AS DateTimeStamp, " & _
                         " CASE dbo.Gl_Messagequeue.sMessageName WHEN 'HEALTHVAULT-DATA' THEN 'Data Exchange' WHEN 'HEALTHVAULT-EMAIL' THEN 'Access Request'  else dbo.Gl_Messagequeue.sMessageName END AS 'MessageName', dbo.Gl_Messagequeue.sMachineName AS MachineName, " & _
                        " ISNULL(dbo.Patient.sPatientCode,'') AS PatientCode, ISNULL(dbo.Patient.sFirstName,'')+' '+ISNULL(dbo.Patient.sMiddleName,'')+' '+ ISNULL(dbo.Patient.sLastName,'') as Patient, " & _
                        " CASE dbo.Gl_Messagequeue.nStatus WHEN 1 THEN 'Queue' WHEN 0 THEN 'Processed'WHEN 2 THEN 'Error' ELSE 'Process Error' END as 'Status',CONVERT(varchar,dbo.Gl_Messagequeue.dtDateTimeStamp,101) AS Date" & _
                        " FROM dbo.Gl_Messagequeue INNER JOIN dbo.Patient ON dbo.Gl_Messagequeue.nPatientID = dbo.Patient.nPatientID Where sServiceName='" & sServiceName & "'"

            objDbLayer.Connect(False)

            objDbLayer.Retrive_Query(_strSQL, dtMsgQueue)

            objDbLayer.Disconnect()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If Not IsNothing(objDbLayer) Then
                objDbLayer.Dispose()
            End If
            _strSQL = String.Empty
        End Try

    End Sub
    Private Sub LoadServiceName()
        Dim dtServiceNames As New DataTable
        Dim objDbLayer As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim _strSQL As String = String.Empty

        Try
            _strSQL = "SELECT Distinct sServiceName FROM Gl_Messagequeue"

            objDbLayer.Connect(False)
            objDbLayer.Retrive_Query(_strSQL, dtServiceNames)


            If Not IsNothing(dtServiceNames) AndAlso dtServiceNames.Rows.Count > 0 Then
                cmbServiceName.DataSource = dtServiceNames
                cmbServiceName.DisplayMember = "sServiceName"
                cmbServiceName.ValueMember = "sServiceName"
                cmbServiceName.SelectedIndex = 0
            End If

            objDbLayer.Disconnect()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If Not IsNothing(objDbLayer) Then
                objDbLayer.Dispose()
            End If
            _strSQL = String.Empty
        End Try
    End Sub

    Private Sub frmGeneralMessageQueue_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        _blnFormLoading = True
        LoadServiceName()
        LoadData()
        _blnFormLoading = False
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

    Private Sub cmbServiceName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbServiceName.SelectedIndexChanged
        If _blnFormLoading = False Then
            If Not IsNothing(ogloUCSearchctrl) Then
                pnlReceivedTriage1.Controls.Remove(ogloUCSearchctrl)
                ogloUCSearchctrl.Dispose()
                ogloUCSearchctrl = Nothing
                LoadData()
            End If
        End If
    End Sub


End Class