Imports System.Data.SqlClient
Imports gloGlobal.SBPQuestionnaire
Imports gloDatabaseLayer

Public Class frmSBPTransaction

    Dim _PatientID As Long
    Dim _SelectedNodeVisitID As Long = 0
    Dim _SelectedNodeVisitDate As DateTime = Nothing
    Dim _SelectedNodeTransactionDate As DateTime = Nothing
    Dim _PatDetailsSelectedRowNo As Integer = 0

    Dim IsCalledFromPatientDetails As Boolean

    Dim sConnectionString As String = String.Empty

    Private WithEvents gloUC_PatientStrip As gloUserControlLibrary.gloUC_PatientStrip

    Dim oDynamicSBP As gloGlobal.SBPQuestionnaire.usrCtrlDynamicSBP = Nothing

    Public Sub New(ByVal PatientID As Long, ByVal ApplicationConnectionString As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        _PatientID = PatientID
        sConnectionString = ApplicationConnectionString
    End Sub

    Public Sub New(ByVal PatientID As Long, ByVal SBPTransactionDate As DateTime, ByVal ApplicationConnectionString As String, ByVal IsOpenedFromPatientDetails As Boolean, ByVal RowNo As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        _PatientID = PatientID
        sConnectionString = ApplicationConnectionString
        _SelectedNodeTransactionDate = SBPTransactionDate
        IsCalledFromPatientDetails = IsOpenedFromPatientDetails ''''if true means form is called from patient details panel from dashboard, hence do not show the transaction date treeview
        _PatDetailsSelectedRowNo = RowNo
    End Sub

    'Public Sub New(ByVal PatientID As Long, ByVal VisitID As Long, ByVal ApplicationConnectionString As String)
    '    ' This call is required by the Windows Form Designer.                
    '    InitializeComponent()

    '    nPatientID = PatientID
    '    nVisitID = VisitID
    '    sConnectionString = ApplicationConnectionString

    '    ' Add any initialization after the InitializeComponent() call.  
    'End Sub


    Private Function GetSBPVisits(PatientId As Long) As DataTable

        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oParameters As New gloDatabaseLayer.DBParameters()
        Dim dt As DataTable = Nothing

        Try
            oDB.Connect(False)
            'Get the Patient Demographic Details for dashboard.
            oParameters.Add("@nPatientid", PatientId, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Retrive("SBP_GetVisits", oParameters, dt)
            Return dt
        Catch dbEx As gloDatabaseLayer.DBException
            dbEx.ERROR_Log(dbEx.ToString())
            Return Nothing
        Catch ex As Exception
            MessageBox.Show("ERROR : " & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return Nothing
        Finally
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(oParameters) Then
                oParameters.Dispose()
                oParameters = Nothing
            End If
            If oDB IsNot Nothing Then
                oDB.Disconnect()
            End If
            If oDB IsNot Nothing Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try

    End Function
    Private Sub frmSBPTransaction_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try

            ''''if true means form is called from patient details panel from dashboard, hence do not show the transaction date treeview
            If IsCalledFromPatientDetails = True Then
                pnl_trv.Visible = False
                lblTransactionDate.Text = "Transaction Date: " & _SelectedNodeTransactionDate.ToString()
                lblTransactionDate.Visible = False ''temperorily kept false
                FillDynamicControl()
            Else
                lblTransactionDate.Visible = False ''show only when opened from patient details panel

                Dim dtSBPVisits As DataTable = GetSBPVisits(_PatientID)
                trvSBPVisits.Nodes.Clear()
                trvSBPVisits.Nodes.Add("Date")
                'trvSBPVisits.Nodes.Item(0).ImageIndex = 3
                'trvSBPVisits.Nodes.Item(0).SelectedImageIndex = 3


                If Not IsNothing(dtSBPVisits) Then
                    If dtSBPVisits.Rows.Count > 0 Then
                        For icnt As Int32 = 0 To dtSBPVisits.Rows.Count - 1
                            'Dim nvisitid As Long = dtSBPVisits.Rows(icnt)("nvisitid")
                            'If trvSBPVisits.Nodes.Count > 0 Then

                            'Else
                            Dim mynode As TreeNode
                            mynode = New TreeNode
                            mynode.Tag = dtSBPVisits.Rows(icnt)("nvisitid") ''visit id
                            mynode.Text = dtSBPVisits.Rows(icnt)("dttransactiondate") ''dtvisitdate
                            'mynode.ImageIndex = 6
                            'mynode.SelectedImageIndex = 6
                            trvSBPVisits.Nodes.Item(0).Nodes.Add(mynode)
                            'End If

                        Next
                    End If

                End If

                If trvSBPVisits.Nodes.Count > 0 Then
                    trvSBPVisits.ExpandAll()
                    _SelectedNodeTransactionDate = trvSBPVisits.Nodes(0).FirstNode.Text
                    FillDynamicControl()
                End If
            End If


           

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FillDynamicControl()
        Try

            If pnlWebbrowserCtrl.Controls.Count > 0 Then
                pnlWebbrowserCtrl.Controls.Clear()
            End If
            If IsCalledFromPatientDetails = True Then
                gloGlobal.SBPQuestionnaire.DynamicFormData.AppConnectionString = sConnectionString
                gloGlobal.SBPQuestionnaire.DynamicFormData.nPatientId = _PatientID
                'gloGlobal.SBPQuestionnaire.DynamicFormData.nVisitId = nVisitID
                gloGlobal.SBPQuestionnaire.DynamicFormData.nPatientProviderId = gnPatientProviderID
                gloGlobal.SBPQuestionnaire.DynamicFormData.nLoginsessionId = gintLoginSessionID
                gloGlobal.SBPQuestionnaire.DynamicFormData.PatientProviderName = gstrPatientProviderName

                Me.Text = "Patient Social Psychological Behavioral observations"


                ''Dim oDynamicSBP As New gloGlobal.SBPQuestionnaire.usrCtrlDynamicSBP(sConnectionString)

                ''if from patient details panel when we select first row that holds latest SBP data 
                ''which is present in SBPTransaction table and not in SBP_Transaction_HST table 
                ''hence if selected row from patient details panel is 1 then take data from SBPTransaction table else fetch data from SBP_Transaction_HST table 
                If _PatDetailsSelectedRowNo = 1 Then
                    oDynamicSBP = New gloGlobal.SBPQuestionnaire.usrCtrlDynamicSBP(_PatientID, _SelectedNodeTransactionDate, False)
                Else
                    oDynamicSBP = New gloGlobal.SBPQuestionnaire.usrCtrlDynamicSBP(_PatientID, _SelectedNodeTransactionDate, True)
                End If



                'Set_PatientDetailStrip()
                oDynamicSBP.Dock = DockStyle.Fill
                pnlWebbrowserCtrl.Controls.Add(oDynamicSBP)
            Else
                If trvSBPVisits.Nodes.Count > 0 Then
                    If IsNothing(_SelectedNodeTransactionDate) Then
                        _SelectedNodeTransactionDate = trvSBPVisits.SelectedNode.Text
                    End If

                    '_SelectedNodeVisitID = trvSBPVisits.SelectedNode.Tag

                    gloGlobal.SBPQuestionnaire.DynamicFormData.AppConnectionString = sConnectionString
                    gloGlobal.SBPQuestionnaire.DynamicFormData.nPatientId = _PatientID
                    'gloGlobal.SBPQuestionnaire.DynamicFormData.nVisitId = nVisitID
                    gloGlobal.SBPQuestionnaire.DynamicFormData.nPatientProviderId = gnPatientProviderID
                    gloGlobal.SBPQuestionnaire.DynamicFormData.nLoginsessionId = gintLoginSessionID
                    gloGlobal.SBPQuestionnaire.DynamicFormData.PatientProviderName = gstrPatientProviderName

                    Me.Text = "Patient Social Psychological Behavioral observations"


                    ''Dim oDynamicSBP As New gloGlobal.SBPQuestionnaire.usrCtrlDynamicSBP(sConnectionString)
                    oDynamicSBP = New gloGlobal.SBPQuestionnaire.usrCtrlDynamicSBP(_PatientID, _SelectedNodeTransactionDate, False)


                    'Set_PatientDetailStrip()
                    oDynamicSBP.Dock = DockStyle.Fill
                    pnlWebbrowserCtrl.Controls.Add(oDynamicSBP)



                End If
            End If
           
        Catch ex As Exception

        End Try
    End Sub

    Private Sub trvSBPVisits_NodeMouseDoubleClick(sender As System.Object, e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvSBPVisits.NodeMouseDoubleClick
        Try
            If trvSBPVisits.Nodes.Count > 0 Then

                _SelectedNodeTransactionDate = trvSBPVisits.SelectedNode.Text
                FillDynamicControl()

            End If



        Catch ex As Exception

        End Try
    End Sub

    Private Sub ts_btnClose_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnClose.Click
        Me.Close()
    End Sub
End Class
