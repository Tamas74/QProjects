Imports gloEMRGeneralLibrary.gloEMRActors
Imports gloEMRGeneralLibrary.gloEMRPrescription
Imports gloEMRGeneralLibrary.gloEMRDatabase
'Imports DrugInteractionControl
Imports gloUserControlLibrary
Imports gloEMRGeneralLibrary.gloPrintFax
Imports gloEMRGeneralLibrary.gloGeneral
Imports gloEMRGeneralLibrary

Public Class frmVWErrorMessage



    Dim _blnSearch As Boolean = True 'for applying searching on grid
    Dim nTransactionId As Int64 = 0
    Dim strRelatedMsgId As String = ""
    Dim _PrescriberId As String = ""
    Dim intRefillpanelheight As Int32
    Dim blnbtnstatus As Boolean = False
    Private RxTransactionID As String = "0"
    Private mSourceRxRefernceNumber As String = ""
    Private msourceRelatesToMessageID As String = ""

    Dim selectednode As TreeNode
    Dim strSelectednode As String = ""
    Dim strselectednodetag = ""

    'Public Sub New()

    '    ' This call is required by the Windows Form Designer.
    '    InitializeComponent()

    '    ' Add any initialization after the InitializeComponent() call.
    '    intRefillpanelheight = pnl_RefillInfo.Height
    '    pnl_RefillInfo.Height = pnlError.Height
    '    btnupdown.BackgroundImage = Global.gloEMR.My.Resources.ImgDownButton
    '    btnupdown.BackgroundImageLayout = ImageLayout.Center

    'End Sub

    Public Sub New(Optional ByVal PrescriberId As String = "")

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If PrescriberId <> "" Then
            _PrescriberId = PrescriberId
        End If

        intRefillpanelheight = 705 ''pnl_RefillInfo.Height
        pnl_RefillInfo.Height = pnlError.Height
        btnupdown.BackgroundImage = Global.gloEMR.My.Resources.Down
        btnupdown.BackgroundImageLayout = ImageLayout.Center

        gloSureScript.gloSurescriptGeneral.sUserName = gstrSQLUserEMR
        gloSureScript.gloSurescriptGeneral.sPassword = gstrSQLPasswordEMR
    End Sub

    Private Sub frmVWErrorMessage_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            'Application.DoEvents()
            Me.Dispose()
        Catch exdispose As Exception

        End Try
    End Sub

   
    Private Sub frmVWErrorMessage_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        globalSecurity.gstrSQLUserEMR = gstrSQLUserEMR
        globalSecurity.gstrSQLPasswordEMR = gstrSQLPasswordEMR
        globalSecurity.gblnSQLAuthentication = gblnSQLAuthentication

        globalSecurity.gstrDatabaseName = gstrDatabaseName
        globalSecurity.gstrSQLServerName = gstrSQLServerName
        clsgeneral.StartUpPath = System.Windows.Forms.Application.StartupPath
        clsgeneral.gblnIsStagingServer = gblnStagingServer


        'Dim hgth As Integer = System.Windows.SystemParameters.PrimaryScreenHeight
        'Dim wdth As Integer = System.Windows.SystemParameters.PrimaryScreenWidth
        'Dim res As Integer = System.Windows.SystemParameters.FullPrimaryScreenHeight
        'If res < 800 Then
        '    Me.MaximumSize = New System.Drawing.Size(wdth, (hgth - 50))
        '    Me.Height = hgth - 50
        '    Me.AutoScroll = True
        'End If

        Dim oErrorMessage As New gloEMRGeneralLibrary.ErrorMessage
        Dim DBLayer As New PrescriptionBusinessLayer()

        If gblnSQLAuthentication = True Then '''' this is used in gloSurescriptGeneral.GetconnectionString()
            gloSureScript.gloSurescriptGeneral.gblnIsSQLAuthentication = True
        End If

        Try
            Dim dt As DataTable = DBLayer.GetPrescriberList()
            trvPrescribers.Nodes.Clear()
            trvPrescribers.Nodes.Add("Prescribers")
            trvPrescribers.Nodes.Item(0).ImageIndex = 3
            trvPrescribers.Nodes.Item(0).SelectedImageIndex = 3

            'show the lable name with the coloumn name to search
            'lblSearch.Text = "Error Code"
            lblSearch.Text = "Search : "

            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    Dim mynode As TreeNode
                    For icnt As Int32 = 0 To dt.Rows.Count - 1
                        mynode = New TreeNode
                        mynode.Tag = dt.Rows(icnt)(0)
                        mynode.Text = dt.Rows(icnt)(1)
                        mynode.ImageIndex = 4
                        mynode.SelectedImageIndex = 4
                        trvPrescribers.Nodes.Item(0).Nodes.Add(mynode)
                    Next

                    If _PrescriberId <> "" Then 'means that the prescriber id is passed from the popup grid of dashboard

                        'select that prescriber whose tag value is matched with the prescriber id passed from the popup grid of dashboard
                        For i As Int16 = 0 To trvPrescribers.GetNodeCount(True) - 1
                            If _PrescriberId = trvPrescribers.Nodes(0).Nodes(i).Tag Then
                                trvPrescribers.SelectedNode = trvPrescribers.Nodes.Item(0).Nodes.Item(i)

                                'make the treenode as selected and exit from the loop
                                Exit For
                            End If
                        Next

                        'get the errordetails for that prescriber id which is passed from the dashboard popup grid
                        dt = oErrorMessage.GetErrorDetails(_PrescriberId)
                        If Not IsNothing(dt) Then

                            If dt.Rows.Count > 0 Then
                                pnl_RefillInfo.Height = intRefillpanelheight
                                blnbtnstatus = True
                                btnupdown.BackgroundImage = Global.gloEMR.My.Resources.UP
                                btnupdown.BackgroundImageLayout = ImageLayout.Center

                                'take a dataview because we can apply searching and sorting on dataview instead of datatable
                                Dim dv As DataView

                                'convert the datatable to default view
                                dv = dt.DefaultView

                                'binf the dataview instead of datatable to the datagrid
                                dgRefillList.DataSource = dv

                                'for setting the grid style pass the datatable
                                setDataGridStyle(dt)
                                dgRefillList.Select(0)
                                showDetails()

                            End If
                        End If

                    Else ' this means that the form is directly opened by selecting the menu

                        trvPrescribers.SelectedNode = trvPrescribers.Nodes.Item(0).Nodes.Item(0)

                        dt = oErrorMessage.GetErrorDetails(trvPrescribers.SelectedNode.Tag)
                        If Not IsNothing(dt) Then

                            If dt.Rows.Count > 0 Then
                                pnl_RefillInfo.Height = intRefillpanelheight
                                blnbtnstatus = True
                                btnupdown.BackgroundImage = Global.gloEMR.My.Resources.UP
                                btnupdown.BackgroundImageLayout = ImageLayout.Center

                                'take a dataview because we can apply searching and sorting on dataview instead of datatable
                                Dim dv As DataView

                                'convert the datatable to default view
                                dv = dt.DefaultView

                                'binf the dataview instead of datatable to the datagrid
                                dgRefillList.DataSource = dv

                                'for setting the grid style pass the datatable
                                setDataGridStyle(dt)
                                dgRefillList.Select(0)
                                showDetails()

                            End If
                        End If
                    End If

                End If

            End If

            pnlError.Height = 0
            pnlErrorMessage.Height = 0

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If DBLayer IsNot Nothing Then
                DBLayer.Dispose()
                DBLayer = Nothing
            End If
        End Try
    End Sub



    ''' <summary>
    ''' set the header coloumns names and width for the dgRefillList datagrid
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Private Sub setDataGridStyle(ByVal dt As DataTable)
        Dim _grdWidth As Int16 = (dgRefillList.Width - 15) / 3
        Try
            Dim ts As New clsDataGridTableStyle(dt.TableName)

            Dim TransactionId As New DataGridTextBoxColumn
            With TransactionId
                .Width = 0 '_grdWidth * 0.5
                .MappingName = dt.Columns(0).ColumnName
                .HeaderText = "Transaction Id"
            End With

            Dim RelatesToMsgId As New DataGridTextBoxColumn
            With RelatesToMsgId
                .Width = _grdWidth * 0.9
                .MappingName = dt.Columns(1).ColumnName
                .HeaderText = "Relates To Message ID"
            End With

            Dim ErrorCode As New DataGridTextBoxColumn
            With ErrorCode
                .Width = _grdWidth * 0.3
                .MappingName = dt.Columns(2).ColumnName
                .HeaderText = "Error Code"
            End With

            Dim DescriptionCode As New DataGridTextBoxColumn
            With DescriptionCode
                .Width = 0 '_grdWidth * 0.5 'hide
                .MappingName = dt.Columns(3).ColumnName
                .HeaderText = "Description Code"
            End With

            Dim Description As New DataGridTextBoxColumn
            With Description
                .Width = _grdWidth * 2
                .MappingName = dt.Columns(4).ColumnName
                .HeaderText = "Description"
            End With

            Dim MessageID As New DataGridTextBoxColumn
            With MessageID
                .Width = _grdWidth * 1
                .MappingName = dt.Columns(5).ColumnName
                .HeaderText = "Message ID"
            End With

            Dim DateReceived As New DataGridTextBoxColumn
            With DateReceived
                .Width = _grdWidth * 0.5
                .MappingName = dt.Columns(6).ColumnName
                .HeaderText = "Date Sent"
            End With

            ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {TransactionId, DateReceived, ErrorCode, DescriptionCode, Description, RelatesToMsgId, MessageID})
            dgRefillList.TableStyles.Clear()
            dgRefillList.TableStyles.Add(ts)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

   


    Private Sub dgRefillList_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgRefillList.MouseClick
        Try
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As DataGrid.HitTestInfo = dgRefillList.HitTest(ptPoint)
            If htInfo.Type = DataGrid.HitTestType.ColumnHeader Then


                'Select Case htInfo.Column
                '    Case 2 ' 2nd coloum is Error Code
                '        lblSearch.Text = "Error Code"

                '    Case 4 '3rd coloumn is Description
                '        lblSearch.Text = "Description"

                'End Select
            End If
            'If txtSearch.Text = "" Then
            '    _blnSearch = True
            'Else
            '    _blnSearch = False
            '    txtSearch.Text = ""
            '    _blnSearch = True
            'End If

            '*********************************************************************
            ''assign the values of the selected row to the variables so that some of the values that are assigned can be passed to the oRefillRequest.GetDrugDetailsToRefill(sRxReferenceNumber, dtdatereceived, nRxTransactionId) 
            Dim selectedRowNo As Integer = dgRefillList.HitTest(e.X, e.Y).Row

            If selectedRowNo >= 0 Then
                nTransactionId = dgRefillList.Item(selectedRowNo, 0)

                strRelatedMsgId = dgRefillList.Item(selectedRowNo, 5)
                If strRelatedMsgId.Contains("NewRx") Then
                    'pnl_MedicalDispanced.Visible = False
                    'Panel17.Visible = False
                    pnl_MedicalDispanced.Hide()
                    Panel16.Hide()
                Else
                    'pnl_MedicalDispanced.Visible = True
                    'Panel17.Visible = True
                    pnl_MedicalDispanced.Show()
                    Panel16.Show()

                End If
                Dim oErrorMessage As New gloEMRGeneralLibrary.ErrorMessage

                oErrorMessage.GetErrorMessageDetails(nTransactionId, strRelatedMsgId)


                'assign values to Message information labels from oErrorMessage property procedures
                lblErrMsgDtl_Name.Text = oErrorMessage.MessageName
                lblRelatesMsgId.Text = oErrorMessage.RelatesToMsgId
                lblMessageId.Text = oErrorMessage.MessageId
                lblDateSent.Text = oErrorMessage.DateSent

                lbl_Patient.Text = oErrorMessage.PatientName
                lbl_PatDOB.Text = oErrorMessage.PatientDOB
                lbl_PatGender.Text = oErrorMessage.PatientGender
                lbl_PatientAddress.Text = oErrorMessage.PatientAddress
                lbl_PatientAddress2.Text = oErrorMessage.PatientAddress2
                lbl_PatientPhoneNo.Text = oErrorMessage.PatientPhone
                lbl_CityName.Text = oErrorMessage.PatientCity
                lbl_StateName.Text = oErrorMessage.PatientState
                lbl_ZIPCode.Text = If(oErrorMessage.PatientZip.Length < 5, "", oErrorMessage.PatientZip)


                lbl_Pharmacy.Text = oErrorMessage.PharmacyName
                lbl_PharmacyAddress.Text = oErrorMessage.PharmacyAddress
                lbl_PharmacyAddress2.Text = oErrorMessage.PharmacyAddress2
                lbl_PharmacyPhoneNo.Text = oErrorMessage.PharmacyPhone
                lbl_PharamcyCity.Text = oErrorMessage.PharmacyCity
                lbl_PharmacyState.Text = oErrorMessage.PharmacyState
                lbl_PharmacyZip.Text = oErrorMessage.PharmacyZip
                lbl_PharmacyFax.Text = oErrorMessage.PharmacyFax
                lbl_PharmacyNPI.Text = oErrorMessage.PharmacyNPI

                lbl_Provider.Text = oErrorMessage.ProviderName
                lbl_ProviderAddress.Text = oErrorMessage.ProviderAddress
                lbl_ProviderAddress2.Text = oErrorMessage.ProviderAddress2
                lbl_PrPhone.Text = oErrorMessage.ProviderPhone
                lbl_ProviderCity.Text = oErrorMessage.ProviderCity
                lbl_ProviderState.Text = oErrorMessage.ProviderState
                lbl_ProviderZIP.Text = oErrorMessage.ProviderZip
                lbl_ProviderFax.Text = oErrorMessage.ProviderFax
                lbl_ProviderNPI.Text = oErrorMessage.ProviderNPI


                ''MD Drug Info  -------
                lblDrugName_Strength_Dosageform.Text = oErrorMessage.MDDrugName & " " & oErrorMessage.MDDosage & " " & oErrorMessage.MDDrugForm
                lblDrugQuantity.Text = oErrorMessage.MDQuantity & " " & oErrorMessage.MDDosageDescription
                lblDirection.Text = oErrorMessage.MDFrequency
                lblDrugNotes.Text = oErrorMessage.MDNotes
                lblRefillQuantity.Text = oErrorMessage.MDRefillQuantity
                If oErrorMessage.MDdtWrittendate <> "" Then
                    lblWrittenDate.Text = Format(CDate(oErrorMessage.MDdtWrittendate), "MM/dd/yyyy")
                Else
                    lblWrittenDate.Text = ""
                End If

                lblSubstitution.Text = If(oErrorMessage.MDbMaySubstitutions = "True", "Yes", "No")
                If oErrorMessage.MDdtlastdate <> "" Then
                    lbl_MDLastFillDate.Text = Format(CDate(oErrorMessage.MDdtlastdate), "MM/dd/yyyy")
                Else
                    lbl_MDLastFillDate.Text = ""
                End If

                lbl_Ref_Qlfr.Text = If(oErrorMessage.RefillsQualifier = "P", "A", oErrorMessage.RefillsQualifier)
                lbl_MDRef_Qlfr.Text = If(oErrorMessage.MDRefillQualifier = "P", "A", oErrorMessage.MDRefillQualifier)

                lbl_MDDuration.Text = oErrorMessage.MDDuration
                lbl_Duration.Text = oErrorMessage.DrugDuration

                ''Prescribed Drug Info  -------
                lbl_MPDrugName_Strength_Dosageform.Text = oErrorMessage.DrugName & " " & oErrorMessage.Dosage & " " & oErrorMessage.Drugform
                lbl_MPDrugQuantity.Text = oErrorMessage.DrugQuantity & " " & oErrorMessage.DosageDescription
                lbl_MPDirection.Text = oErrorMessage.Directions
                lbl_MPDrugnotes.Text = oErrorMessage.Notes
                lbl_MPRefillQuantity.Text = oErrorMessage.RefillQuantity

                If oErrorMessage.WrittenDate <> "" Then
                    lbl_MPWrittenDate.Text = Format(CDate(oErrorMessage.WrittenDate), "MM/dd/yyyy")
                Else
                    lbl_MPWrittenDate.Text = ""
                End If

                lbl_MPSubstitution.Text = If(oErrorMessage.MaySubstitute = "True", "Yes", "No")

                If oErrorMessage.LastfillDate <> "" Then
                    lbl_LastFillDate.Text = Format(CDate(oErrorMessage.LastfillDate), "MM/dd/yyyy")
                Else
                    lbl_LastFillDate.Text = ""
                End If
                '----xx-----



                pnl_RefillInfo.Height = intRefillpanelheight
                blnbtnstatus = True
                btnupdown.BackgroundImage = Global.gloEMR.My.Resources.UP
                btnupdown.BackgroundImageLayout = ImageLayout.Center

                'Code commneted by supriya
                'If MessageBox.Show("Do you want to update the status of this message to " & """Reviewed""?" & "", "Error Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                '    oErrorMessage.UpdateMessageTransaction(nTransactionId.ToString)

                '    Dim snd As Object
                '    Dim evntArgs As EventArgs
                '    ts_btnRefresh_Click(snd, evntArgs)
                'Else
                '    Exit Sub
                'End If
                'code commented by supriya
            End If
            

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgRefillList_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgRefillList.MouseDoubleClick
        Try
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As DataGrid.HitTestInfo = dgRefillList.HitTest(ptPoint)
            If htInfo.Type = DataGrid.HitTestType.ColumnHeader Then


                'Select Case htInfo.Column
                '    Case 2 ' 2nd coloum is Error Code
                '        lblSearch.Text = "Error Code"

                '    Case 4 '3rd coloumn is Description
                '        lblSearch.Text = "Description"

                'End Select
            End If
            'If txtSearch.Text = "" Then
            '    _blnSearch = True
            'Else
            '    _blnSearch = False
            '    txtSearch.Text = ""
            '    _blnSearch = True
            'End If

            '*********************************************************************
            ''assign the values of the selected row to the variables so that some of the values that are assigned can be passed to the oRefillRequest.GetDrugDetailsToRefill(sRxReferenceNumber, dtdatereceived, nRxTransactionId) 
            Dim selectedRowNo As Integer = dgRefillList.HitTest(e.X, e.Y).Row

            If selectedRowNo >= 0 Then
                nTransactionId = dgRefillList.Item(selectedRowNo, 0)

                strRelatedMsgId = dgRefillList.Item(selectedRowNo, 5)

                If strRelatedMsgId.Contains("NewRx") Then
                    'pnl_MedicalDispanced.Visible = False
                    'Panel17.Visible = False
                    pnl_MedicalDispanced.Hide()
                    Panel16.Hide()
                Else
                    'pnl_MedicalDispanced.Visible = True
                    'Panel17.Visible = True
                    pnl_MedicalDispanced.Show()
                    Panel16.Show()

                End If

                Dim oErrorMessage As New gloEMRGeneralLibrary.ErrorMessage

                oErrorMessage.GetErrorMessageDetails(nTransactionId, strRelatedMsgId)


                'assign values to Message information labels from oErrorMessage property procedures
                lblErrMsgDtl_Name.Text = oErrorMessage.MessageName
                lblRelatesMsgId.Text = oErrorMessage.RelatesToMsgId
                lblMessageId.Text = oErrorMessage.MessageId
                lblDateSent.Text = oErrorMessage.DateSent

                lbl_Patient.Text = oErrorMessage.PatientName
                lbl_PatDOB.Text = oErrorMessage.PatientDOB
                lbl_PatGender.Text = oErrorMessage.PatientGender
                lbl_PatientAddress.Text = oErrorMessage.PatientAddress
                lbl_PatientAddress2.Text = oErrorMessage.PatientAddress2
                lbl_PatientPhoneNo.Text = oErrorMessage.PatientPhone
                lbl_CityName.Text = oErrorMessage.PatientCity
                lbl_StateName.Text = oErrorMessage.PatientState
                lbl_ZIPCode.Text = If(oErrorMessage.PatientZip.Length < 5, "", oErrorMessage.PatientZip)


                lbl_Pharmacy.Text = oErrorMessage.PharmacyName
                lbl_PharmacyAddress.Text = oErrorMessage.PharmacyAddress
                lbl_PharmacyAddress2.Text = oErrorMessage.PharmacyAddress2
                lbl_PharmacyPhoneNo.Text = oErrorMessage.PharmacyPhone
                lbl_PharamcyCity.Text = oErrorMessage.PharmacyCity
                lbl_PharmacyState.Text = oErrorMessage.PharmacyState
                lbl_PharmacyZip.Text = oErrorMessage.PharmacyZip
                lbl_PharmacyFax.Text = oErrorMessage.PharmacyFax
                lbl_PharmacyNPI.Text = oErrorMessage.PharmacyNPI

                lbl_Provider.Text = oErrorMessage.ProviderName
                lbl_ProviderAddress.Text = oErrorMessage.ProviderAddress
                lbl_ProviderAddress2.Text = oErrorMessage.ProviderAddress2
                lbl_PrPhone.Text = oErrorMessage.ProviderPhone
                lbl_ProviderCity.Text = oErrorMessage.ProviderCity
                lbl_ProviderState.Text = oErrorMessage.ProviderState
                lbl_ProviderZIP.Text = oErrorMessage.ProviderZip
                lbl_ProviderFax.Text = oErrorMessage.ProviderFax
                lbl_ProviderNPI.Text = oErrorMessage.ProviderNPI


                ''MD Drug Info  -------
                lblDrugName_Strength_Dosageform.Text = oErrorMessage.MDDrugName & " " & oErrorMessage.MDDosage & " " & oErrorMessage.MDDrugForm
                lblDrugQuantity.Text = oErrorMessage.MDQuantity & " " & oErrorMessage.MDDosageDescription
                lblDirection.Text = oErrorMessage.MDFrequency
                lblDrugNotes.Text = oErrorMessage.MDNotes
                lblRefillQuantity.Text = oErrorMessage.MDRefillQuantity
                If oErrorMessage.MDdtWrittendate <> "" Then
                    lblWrittenDate.Text = Format(CDate(oErrorMessage.MDdtWrittendate), "MM/dd/yyyy")
                Else
                    lblWrittenDate.Text = ""
                End If

                lblSubstitution.Text = If(oErrorMessage.MDbMaySubstitutions = "True", "Yes", "No")
                If oErrorMessage.MDdtlastdate <> "" Then
                    lbl_MDLastFillDate.Text = Format(CDate(oErrorMessage.MDdtlastdate), "MM/dd/yyyy")
                Else
                    lbl_MDLastFillDate.Text = ""
                End If

                lbl_Ref_Qlfr.Text = If(oErrorMessage.RefillsQualifier = "P", "A", oErrorMessage.RefillsQualifier)
                lbl_MDRef_Qlfr.Text = If(oErrorMessage.MDRefillQualifier = "P", "A", oErrorMessage.MDRefillQualifier)

                lbl_MDDuration.Text = oErrorMessage.MDDuration
                lbl_Duration.Text = oErrorMessage.DrugDuration

                ''Prescribed Drug Info  -------
                lbl_MPDrugName_Strength_Dosageform.Text = oErrorMessage.DrugName & " " & oErrorMessage.Dosage & " " & oErrorMessage.Drugform
                lbl_MPDrugQuantity.Text = oErrorMessage.DrugQuantity & " " & oErrorMessage.DosageDescription
                lbl_MPDirection.Text = oErrorMessage.Directions
                lbl_MPDrugnotes.Text = oErrorMessage.Notes
                lbl_MPRefillQuantity.Text = oErrorMessage.RefillQuantity

                If oErrorMessage.WrittenDate <> "" Then
                    lbl_MPWrittenDate.Text = Format(CDate(oErrorMessage.WrittenDate), "MM/dd/yyyy")
                Else
                    lbl_MPWrittenDate.Text = ""
                End If

                lbl_MPSubstitution.Text = If(oErrorMessage.MaySubstitute = "True", "Yes", "No")

                If oErrorMessage.LastfillDate <> "" Then
                    lbl_LastFillDate.Text = Format(CDate(oErrorMessage.LastfillDate), "MM/dd/yyyy")
                Else
                    lbl_LastFillDate.Text = ""
                End If
                '----xx-----



                pnl_RefillInfo.Height = intRefillpanelheight
                blnbtnstatus = True
                btnupdown.BackgroundImage = Global.gloEMR.My.Resources.UP
                btnupdown.BackgroundImageLayout = ImageLayout.Center

                'code commented by supriya
                'If MessageBox.Show("Do you want to update the status of this message to " & """Reviewed""?" & "", "Error Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                '    oErrorMessage.UpdateMessageTransaction(nTransactionId.ToString)

                '    Dim snd As Object
                '    Dim evntArgs As EventArgs
                '    ts_btnRefresh_Click(snd, evntArgs)
                'Else
                '    Exit Sub
                'End If
                'code commented by supriya
            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgRefillList_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgRefillList.MouseDown
        Try
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As DataGrid.HitTestInfo = dgRefillList.HitTest(ptPoint)
            'Code Added by Mayuri:20091112
            'To fix bug:#4598-Application should not display rightclick context menubar if no records present.
            cntListmenuStrip.Items(0).Visible = True
            cntListmenuStrip.Items(1).Visible = True
            If htInfo.Row < 0 Then

                cntListmenuStrip.Items(0).Visible = False
                cntListmenuStrip.Items(1).Visible = False
                Exit Sub
            End If
            'End Code Added by Mayuri:20091112

            dgRefillList.Select(htInfo.Row)

            If htInfo.Type = DataGrid.HitTestType.ColumnHeader Then
                'Select Case htInfo.Column
                '    Case 2 ' 2nd coloum is Error Code
                '        lblSearch.Text = "Error Code"

                '    Case 4 '3rd coloumn is Description
                '        lblSearch.Text = "Description"

                'End Select
            End If
            'If txtSearch.Text = "" Then
            '    _blnSearch = True
            'Else
            '    _blnSearch = False
            '    txtSearch.Text = ""
            '    _blnSearch = True
            'End If

            '*********************************************************************
            ''assign the values of the selected row to the variables so that some of the values that are assigned can be passed to the oRefillRequest.GetDrugDetailsToRefill(sRxReferenceNumber, dtdatereceived, nRxTransactionId) 
            Dim selectedRowNo As Integer = dgRefillList.HitTest(e.X, e.Y).Row

            If selectedRowNo >= 0 Then
                nTransactionId = dgRefillList.Item(selectedRowNo, 0)

                strRelatedMsgId = dgRefillList.Item(selectedRowNo, 1)

                If strRelatedMsgId.Contains("NewRx") Then
                    'pnl_MedicalDispanced.Visible = False
                    'Panel17.Visible = False
                    pnl_MedicalDispanced.Hide()
                    Panel16.Hide()
                Else
                    'pnl_MedicalDispanced.Visible = True
                    'Panel17.Visible = True
                    pnl_MedicalDispanced.Show()
                    Panel16.Show()

                End If

                Dim oErrorMessage As New gloEMRGeneralLibrary.ErrorMessage

                oErrorMessage.GetErrorMessageDetails(nTransactionId, strRelatedMsgId)


                'assign values to Message information labels from oErrorMessage property procedures
                lblErrMsgDtl_Name.Text = oErrorMessage.MessageName
                lblRelatesMsgId.Text = oErrorMessage.RelatesToMsgId
                lblMessageId.Text = oErrorMessage.MessageId
                lblDateSent.Text = oErrorMessage.DateSent

                lbl_Patient.Text = oErrorMessage.PatientName
                lbl_PatDOB.Text = oErrorMessage.PatientDOB
                lbl_PatGender.Text = oErrorMessage.PatientGender
                lbl_PatientAddress.Text = oErrorMessage.PatientAddress
                lbl_PatientAddress2.Text = oErrorMessage.PatientAddress2
                lbl_PatientPhoneNo.Text = oErrorMessage.PatientPhone
                lbl_CityName.Text = oErrorMessage.PatientCity
                lbl_StateName.Text = oErrorMessage.PatientState
                lbl_ZIPCode.Text = If(oErrorMessage.PatientZip.Length < 5, "", oErrorMessage.PatientZip)


                lbl_Pharmacy.Text = oErrorMessage.PharmacyName
                lbl_PharmacyAddress.Text = oErrorMessage.PharmacyAddress
                lbl_PharmacyAddress2.Text = oErrorMessage.PharmacyAddress2
                lbl_PharmacyPhoneNo.Text = oErrorMessage.PharmacyPhone
                lbl_PharamcyCity.Text = oErrorMessage.PharmacyCity
                lbl_PharmacyState.Text = oErrorMessage.PharmacyState
                lbl_PharmacyZip.Text = oErrorMessage.PharmacyZip
                lbl_PharmacyFax.Text = oErrorMessage.PharmacyFax
                lbl_PharmacyNPI.Text = oErrorMessage.PharmacyNPI

                lbl_Provider.Text = oErrorMessage.ProviderName
                lbl_ProviderAddress.Text = oErrorMessage.ProviderAddress
                lbl_ProviderAddress2.Text = oErrorMessage.ProviderAddress2
                lbl_PrPhone.Text = oErrorMessage.ProviderPhone
                lbl_ProviderCity.Text = oErrorMessage.ProviderCity
                lbl_ProviderState.Text = oErrorMessage.ProviderState
                lbl_ProviderZIP.Text = oErrorMessage.ProviderZip
                lbl_ProviderFax.Text = oErrorMessage.ProviderFax
                lbl_ProviderNPI.Text = oErrorMessage.ProviderNPI

                ''MD Drug Info  -------
                lblDrugName_Strength_Dosageform.Text = oErrorMessage.MDDrugName & " " & oErrorMessage.MDDosage & " " & oErrorMessage.MDDrugForm
                lblDrugQuantity.Text = oErrorMessage.MDQuantity & " " & oErrorMessage.MDDosageDescription
                lblDirection.Text = oErrorMessage.MDFrequency
                lblDrugNotes.Text = oErrorMessage.MDNotes
                lblRefillQuantity.Text = oErrorMessage.MDRefillQuantity
                If oErrorMessage.MDdtWrittendate <> "" Then
                    lblWrittenDate.Text = Format(CDate(oErrorMessage.MDdtWrittendate), "MM/dd/yyyy")
                Else
                    lblWrittenDate.Text = ""
                End If

                lblSubstitution.Text = If(oErrorMessage.MDbMaySubstitutions = "True", "Yes", "No")
                If oErrorMessage.MDdtlastdate <> "" Then
                    lbl_MDLastFillDate.Text = Format(CDate(oErrorMessage.MDdtlastdate), "MM/dd/yyyy")
                Else
                    lbl_MDLastFillDate.Text = ""
                End If

                lbl_Ref_Qlfr.Text = If(oErrorMessage.RefillsQualifier = "P", "A", oErrorMessage.RefillsQualifier)
                lbl_MDRef_Qlfr.Text = If(oErrorMessage.MDRefillQualifier = "P", "A", oErrorMessage.MDRefillQualifier)

                lbl_MDDuration.Text = oErrorMessage.MDDuration
                lbl_Duration.Text = oErrorMessage.DrugDuration

                ''Prescribed Drug Info  -------
                lbl_MPDrugName_Strength_Dosageform.Text = oErrorMessage.DrugName & " " & oErrorMessage.Dosage & " " & oErrorMessage.Drugform
                lbl_MPDrugQuantity.Text = oErrorMessage.DrugQuantity & " " & oErrorMessage.DosageDescription
                lbl_MPDirection.Text = oErrorMessage.Directions
                lbl_MPDrugnotes.Text = oErrorMessage.Notes
                lbl_MPRefillQuantity.Text = oErrorMessage.RefillQuantity

                If oErrorMessage.WrittenDate <> "" Then
                    lbl_MPWrittenDate.Text = Format(CDate(oErrorMessage.WrittenDate), "MM/dd/yyyy")
                Else
                    lbl_MPWrittenDate.Text = ""
                End If

                lbl_MPSubstitution.Text = If(oErrorMessage.MaySubstitute = "True", "Yes", "No")

                If oErrorMessage.LastfillDate <> "" Then
                    lbl_LastFillDate.Text = Format(CDate(oErrorMessage.LastfillDate), "MM/dd/yyyy")
                Else
                    lbl_LastFillDate.Text = ""
                End If
                '----xx-----

                pnl_RefillInfo.Height = intRefillpanelheight
                blnbtnstatus = True
                btnupdown.BackgroundImage = Global.gloEMR.My.Resources.UP
                btnupdown.BackgroundImageLayout = ImageLayout.Center



                If e.Button = Windows.Forms.MouseButtons.Right Then
                    RxTransactionID = "0"
                    mSourceRxRefernceNumber = ""
                    If oErrorMessage.SourceMessageName = "RefillResponse" Then
                        Select Case oErrorMessage.MessageStatus

                            Case "Denied", "DeniedWithNewRxToFollow"
                                mnuReloadRx.Visible = False
                            Case "Approved", "ApprovedWithChanges"
                                mnuReloadRx.Visible = True

                        End Select
                    Else
                        mnuReloadRx.Visible = True
                    End If
                    ''code changed to fix bug 20120 -discussed with Drew sir
                    mnuReloadRx.Visible = False
                    RxTransactionID = oErrorMessage.RxTransactionID
                    mSourceRxRefernceNumber = oErrorMessage.SourceRxReferenceNumber
                    msourceRelatesToMessageID = oErrorMessage.SourceRelatesToMessageID
                End If
                If Not IsNothing(oErrorMessage) Then
                    oErrorMessage.Dispose()
                    oErrorMessage = Nothing
                End If
                'Code commented by supriya
                'If MessageBox.Show("Do you want to update the status of this message to " & """Reviewed""?" & "", "Error Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                '    oErrorMessage.UpdateMessageTransaction(nTransactionId.ToString)

                '    Dim snd As Object
                '    Dim evntArgs As EventArgs
                '    ts_btnRefresh_Click(snd, evntArgs)
                'ElseIf htInfo.Type = DataGrid.HitTestType.Cell Then

                '    'If dgRefillList.CurrentRowIndex >= 0 Then
                '    '    dgRefillList.UnSelect(dgRefillList.CurrentRowIndex)
                '    'End If
                '    'dgRefillList.CurrentRowIndex = htInfo.Row
                '    'dgRefillList.Focus()
                '    dgRefillList.Select(htInfo.Row)
                '    Exit Sub
                'End If
                'code commented by supriya
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

   
    Private Sub dgRefillList_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgRefillList.MouseUp

        Try
            Dim point As New Point(e.X, e.Y)
            Dim hti As DataGrid.HitTestInfo = dgRefillList.HitTest(point)
            If hti.Type = DataGrid.HitTestType.Cell Then
                'dgPatient.CurrentCell = New DataGridCell(hti.Row, hti.Column)
                ' ''
                If dgRefillList.CurrentRowIndex >= 0 Then
                    dgRefillList.UnSelect(dgRefillList.CurrentRowIndex)
                End If
                dgRefillList.CurrentRowIndex = hti.Row
                dgRefillList.Select(hti.Row)

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            'select the first row of the grid
            If (e.KeyChar = ChrW(13)) Then
                If dgRefillList.CurrentRowIndex >= 0 Then
                    dgRefillList.Select(0)
                    dgRefillList.CurrentRowIndex = 0
                End If
            End If

            mdlGeneral.ValidateText(txtSearch.Text, e)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        'If _blnSearch = True Then
        Try
            If txtSearch.Text.Trim = "" Then ''bug # 20073 version 6060 if the search text is blank do not search the grid
                Exit Try
            End If
            Me.Cursor = Cursors.WaitCursor
            Dim dvPatient As DataView
            Dim dt As New DataTable

            dvPatient = CType(dgRefillList.DataSource, DataView)

            If IsNothing(dvPatient) Then
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            dgRefillList.DataSource = dvPatient
            Dim strPatientSearchDetails As String
            If Trim(txtSearch.Text) <> "" Then
                strPatientSearchDetails = Replace(txtSearch.Text, "'", "''")
                strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
            Else
                strPatientSearchDetails = ""
            End If

            ''Select Case Trim(lblSearch.Text)
            ''    Case "Error Code"
            ''        '''''Code Modified by Anil on 24/09/2007 at 3:20 p.m.
            ''        '''''This change is made to get In-String search i.e.,for any string which has the character/characters present in the search textbox.
            ''        '''''Previously the commented code was searching for the strings which are having first character same as that in search textbox 
            ''        '''''and also it was searching strings which are having the character in between the words but for that we had to use " % " or " * " sign before that character. But now it is not required to add % or * signs.

            ''        ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
            ''        dvPatient.RowFilter = dvPatient.Table.Columns(2).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
            ''        ''Else
            ''        ''dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '" & strPatientSearchDetails & "%'"
            ''        ''End If
            ''    Case "Description"
            ''        dvPatient.RowFilter = dvPatient.Table.Columns(4).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
            ''End Select

            'dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '" & strPatientSearchDetails & "%' OR " _
            '                                 & dvPatient.Table.Columns(2).ColumnName & " Like '" & strPatientSearchDetails & "%' OR " _
            '                                 & dvPatient.Table.Columns(3).ColumnName & " Like '" & strPatientSearchDetails & "%' OR " _
            '                                 & dvPatient.Table.Columns(4).ColumnName & " Like '" & strPatientSearchDetails & "%' OR " _
            '                                 & dvPatient.Table.Columns(5).ColumnName & " Like '" & strPatientSearchDetails & "%' OR " _
            '                                 & dvPatient.Table.Columns(6).ColumnName & " Like '" & strPatientSearchDetails & "%' OR " _
            '                                 & dvPatient.Table.Columns(8).ColumnName & " Like '" & strPatientSearchDetails & "%'"

            ''Search functionality on Error Code & Description [not on description code] bugs no:5399
            dvPatient.RowFilter = dvPatient.Table.Columns(2).ColumnName & " Like '%" & strPatientSearchDetails & "%'OR " _
                                    & dvPatient.Table.Columns(4).ColumnName & " Like '%" & strPatientSearchDetails & "%'"

            If dvPatient.Count > 0 Then
                dgRefillList.Select(0)
            End If

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
        'End If
    End Sub

    Private Sub ts_btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvPrescribers_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trvPrescribers.DoubleClick
        Dim oErrorMessage As New gloEMRGeneralLibrary.ErrorMessage
        Dim dt As DataTable
        Try
            If trvPrescribers.Nodes.Item(0).Nodes.Count > 0 Then
                ''trvPrescribers.SelectedNode = trvPrescribers.Nodes.Item(0).Nodes.Item(0)
                trvPrescribers.SelectedNode = selectednode
            Else
                trvPrescribers.SelectedNode = trvPrescribers.Nodes.Item(0)
            End If
            ''trvPrescribers.SelectedNode = trvPrescribers.Nodes.Item(0).Nodes.Item(0)
            ''dt = oErrorMessage.GetErrorDetails(trvPrescribers.SelectedNode.Tag)
            dt = oErrorMessage.GetErrorDetails(selectednode.Tag)
            If Not IsNothing(dt) Then

                If dt.Rows.Count > 0 Then
                    pnl_RefillInfo.Height = intRefillpanelheight
                    blnbtnstatus = True
                    btnupdown.BackgroundImage = Global.gloEMR.My.Resources.UP
                    btnupdown.BackgroundImageLayout = ImageLayout.Center

                    'take a dataview because we can apply searching and sorting on dataview instead of datatable
                    Dim dv As DataView

                    'convert the datatable to default view
                    dv = dt.DefaultView

                    'binf the dataview instead of datatable to the datagrid
                    dgRefillList.DataSource = dv

                    'for setting the grid style pass the datatable
                    setDataGridStyle(dt)
                    dgRefillList.Select(0)
                    showDetails()
                Else

                    ClearErrorDetails()
                    pnl_RefillInfo.Height = pnlError.Height
                    btnupdown.BackgroundImage = Global.gloEMR.My.Resources.Down
                    btnupdown.BackgroundImageLayout = ImageLayout.Center
                End If
            End If
        Catch ex As PrescriptionException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oErrorMessage) Then
                oErrorMessage.Dispose()
                oErrorMessage = Nothing
            End If
        End Try
    End Sub

   

    Private Sub trvPrescribers_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvPrescribers.MouseDown
        'Dim oErrorMessage As New gloEMRGeneralLibrary.ErrorMessage
        'Dim dt As DataTable = New DataTable
        'Try

        '    Dim trvNode As myTreeNode = CType(trvPrescribers.GetNodeAt(e.X, e.Y), TreeNode)
        '    If IsNothing(trvNode) = False Then
        '        trvPrescribers.SelectedNode = trvNode
        '        'trvNode.BackColor = Color.CornflowerBlue
        '        If Not IsNothing(trvPrescribers.SelectedNode) Then
        '            If trvPrescribers.Nodes.Item(0) Is trvPrescribers.SelectedNode Then
        '                dgRefillList.DataSource = Nothing
        '            Else

        '                'pass the appropriate provider id that is stored in the tag value to the function
        '                dt = oErrorMessage.GetErrorDetails(trvPrescribers.SelectedNode.Tag)

        '                If Not IsNothing(dt) Then

        '                    dgRefillList.DataSource = dt

        '                Else ' there are no pending refill request populated in the datatable
        '                    dgRefillList.DataSource = Nothing
        '                End If

        '            End If
        '        End If
        '    End If
        'Catch ex As PrescriptionException
        '    MessageBox.Show(ex.ToString, "Refill Request", MessageBoxButtons.OK, MessageBoxIcon.Error)

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    If Not IsNothing(oErrorMessage) Then
        '        oErrorMessage.Dispose()
        '        oErrorMessage = Nothing
        '    End If

        'End Try


        Dim trvNode As TreeNode
        Try
            txtSearch.Text = "" ''bug # 20073 version 6060  clear the search text box after clicking the the prescriber node
            trvNode = CType(trvPrescribers.GetNodeAt(e.X, e.Y), TreeNode)
            If IsNothing(trvNode) = False Then
                trvPrescribers.SelectedNode = trvNode
                selectednode = trvNode
            End If
            GetErrorMessages()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Refill Request", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    
    Private Sub ts_btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnRefresh.Click
        Try
            GetErrorMessages()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.Refresh, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Public Sub GetErrorMessages()

        Dim dtErrorDetails As DataTable
        Dim oErrorMessage As New gloEMRGeneralLibrary.ErrorMessage
        Try
            'refresh the Popup grid of dashboard so that if there are new items then those will be shown in the dgRefillList grid also.
            CType(Me.MdiParent, MainMenu).FillMessage()

            dtErrorDetails = New DataTable

            Dim mynode As TreeNode
            mynode = CType(trvPrescribers.SelectedNode, TreeNode)

            If IsNothing(trvPrescribers.SelectedNode) = False Then
                'validation if the selected node is not rootnode
                If mynode.Text = "Prescribers" Then

                    dgRefillList.DataSource = Nothing
                    pnl_RefillInfo.Height = pnlError.Height
                    btnupdown.BackgroundImage = Global.gloEMR.My.Resources.Down
                    btnupdown.BackgroundImageLayout = ImageLayout.Center
                Else 'root node is not selected

                    'pass the appropriate provider id that is stored in the tag value to the function
                    dtErrorDetails = oErrorMessage.GetErrorDetails(trvPrescribers.SelectedNode.Tag)

                    If Not IsNothing(dtErrorDetails) Then


                        If dtErrorDetails.Rows.Count > 0 Then
                            dgRefillList.DataSource = dtErrorDetails.DefaultView ''bug # 20073 version 6060 set source as the data table default view instead of data table 
                            pnl_RefillInfo.Height = intRefillpanelheight
                            btnupdown.BackgroundImage = Global.gloEMR.My.Resources.UP
                            btnupdown.BackgroundImageLayout = ImageLayout.Center
                            setDataGridStyle(dtErrorDetails)
                            dgRefillList.Select(0)
                            showDetails()
                            spitpanelprocessrefill()
                        Else
                            dgRefillList.DataSource = Nothing
                            ClearErrorDetails()
                            pnl_RefillInfo.Height = pnlError.Height
                            btnupdown.BackgroundImage = Global.gloEMR.My.Resources.Down
                            btnupdown.BackgroundImageLayout = ImageLayout.Center
                        End If

                    Else ' there are no error messages populated in the datatable
                        ClearErrorDetails()
                        dgRefillList.DataSource = Nothing
                        pnl_RefillInfo.Height = pnlError.Height
                        btnupdown.BackgroundImage = Global.gloEMR.My.Resources.Down
                        btnupdown.BackgroundImageLayout = ImageLayout.Center
                    End If
                End If

            Else
                ClearErrorDetails()
                dgRefillList.DataSource = Nothing
                pnl_RefillInfo.Height = pnlError.Height
                btnupdown.BackgroundImage = Global.gloEMR.My.Resources.Down
                btnupdown.BackgroundImageLayout = ImageLayout.Center
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oErrorMessage) Then
                oErrorMessage.Dispose()
                oErrorMessage = Nothing
            End If
        End Try
    End Sub


    Private Sub showDetails()
        Try

            nTransactionId = dgRefillList.Item(0, 0)

            strRelatedMsgId = dgRefillList.Item(0, 5)

            If strRelatedMsgId.Contains("NewRx") Then
                'pnl_MedicalDispanced.Visible = False
                'Panel17.Visible = False
                pnl_MedicalDispanced.Hide()
                Panel16.Hide()
            Else
                'pnl_MedicalDispanced.Visible = True
                'Panel17.Visible = True
                pnl_MedicalDispanced.Show()
                Panel16.Show()

            End If

            Dim oErrorMessage As New gloEMRGeneralLibrary.ErrorMessage

            oErrorMessage.GetErrorMessageDetails(nTransactionId, strRelatedMsgId)


            'assign values to Message information labels from oErrorMessage property procedures
            lblErrMsgDtl_Name.Text = oErrorMessage.MessageName
            lblRelatesMsgId.Text = oErrorMessage.RelatesToMsgId
            lblMessageId.Text = oErrorMessage.MessageId
            lblDateSent.Text = oErrorMessage.DateSent

            lbl_Patient.Text = oErrorMessage.PatientName
            lbl_PatDOB.Text = oErrorMessage.PatientDOB
            lbl_PatGender.Text = oErrorMessage.PatientGender
            lbl_PatientAddress.Text = oErrorMessage.PatientAddress
            lbl_PatientAddress2.Text = oErrorMessage.PatientAddress2
            lbl_PatientPhoneNo.Text = oErrorMessage.PatientPhone
            lbl_CityName.Text = oErrorMessage.PatientCity
            lbl_StateName.Text = oErrorMessage.PatientState
            lbl_ZIPCode.Text = If(oErrorMessage.PatientZip.Length < 5, "", oErrorMessage.PatientZip)


            lbl_Pharmacy.Text = oErrorMessage.PharmacyName
            lbl_PharmacyAddress.Text = oErrorMessage.PharmacyAddress
            lbl_PharmacyAddress2.Text = oErrorMessage.PharmacyAddress2
            lbl_PharmacyPhoneNo.Text = oErrorMessage.PharmacyPhone
            lbl_PharamcyCity.Text = oErrorMessage.PharmacyCity
            lbl_PharmacyState.Text = oErrorMessage.PharmacyState
            lbl_PharmacyZip.Text = oErrorMessage.PharmacyZip
            lbl_PharmacyFax.Text = oErrorMessage.PharmacyFax
            lbl_PharmacyNPI.Text = oErrorMessage.PharmacyNPI

            lbl_Provider.Text = oErrorMessage.ProviderName
            lbl_ProviderAddress.Text = oErrorMessage.ProviderAddress
            lbl_ProviderAddress2.Text = oErrorMessage.ProviderAddress2
            lbl_PrPhone.Text = oErrorMessage.ProviderPhone
            lbl_ProviderCity.Text = oErrorMessage.ProviderCity
            lbl_ProviderState.Text = oErrorMessage.ProviderState
            lbl_ProviderZIP.Text = oErrorMessage.ProviderZip
            lbl_ProviderFax.Text = oErrorMessage.ProviderFax
            lbl_ProviderNPI.Text = oErrorMessage.ProviderNPI

            ''MD Drug Info  -------
            lblDrugName_Strength_Dosageform.Text = oErrorMessage.MDDrugName & " " & oErrorMessage.MDDosage & " " & oErrorMessage.MDDrugForm
            lblDrugQuantity.Text = oErrorMessage.MDQuantity & " " & oErrorMessage.MDDosageDescription
            lblDirection.Text = oErrorMessage.MDFrequency
            lblDrugNotes.Text = oErrorMessage.MDNotes
            lblRefillQuantity.Text = oErrorMessage.MDRefillQuantity
            If oErrorMessage.MDdtWrittendate <> "" Then
                lblWrittenDate.Text = Format(CDate(oErrorMessage.MDdtWrittendate), "MM/dd/yyyy")
            Else
                lblWrittenDate.Text = ""
            End If

            lblSubstitution.Text = If(oErrorMessage.MDbMaySubstitutions = "True", "Yes", "No")
            If oErrorMessage.MDdtlastdate <> "" Then
                lbl_MDLastFillDate.Text = Format(CDate(oErrorMessage.MDdtlastdate), "MM/dd/yyyy")
            Else
                lbl_MDLastFillDate.Text = ""
            End If

            lbl_Ref_Qlfr.Text = If(oErrorMessage.RefillsQualifier = "P", "A", oErrorMessage.RefillsQualifier)
            lbl_MDRef_Qlfr.Text = If(oErrorMessage.MDRefillQualifier = "P", "A", oErrorMessage.MDRefillQualifier)

            lbl_MDDuration.Text = oErrorMessage.MDDuration
            lbl_Duration.Text = oErrorMessage.DrugDuration

            ''Prescribed Drug Info  -------
            lbl_MPDrugName_Strength_Dosageform.Text = oErrorMessage.DrugName & " " & oErrorMessage.Dosage & " " & oErrorMessage.Drugform
            lbl_MPDrugQuantity.Text = oErrorMessage.DrugQuantity & " " & oErrorMessage.DosageDescription
            lbl_MPDirection.Text = oErrorMessage.Directions
            lbl_MPDrugnotes.Text = oErrorMessage.Notes
            lbl_MPRefillQuantity.Text = oErrorMessage.RefillQuantity

            If oErrorMessage.WrittenDate <> "" Then
                lbl_MPWrittenDate.Text = Format(CDate(oErrorMessage.WrittenDate), "MM/dd/yyyy")
            Else
                lbl_MPWrittenDate.Text = ""
            End If

            lbl_MPSubstitution.Text = If(oErrorMessage.MaySubstitute = "True", "Yes", "No")

            If oErrorMessage.LastfillDate <> "" Then
                lbl_LastFillDate.Text = Format(CDate(oErrorMessage.LastfillDate), "MM/dd/yyyy")
            Else
                lbl_LastFillDate.Text = ""
            End If
            '----xx-----


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub ClearErrorDetails()
        Try
            lblErrMsgDtl_Name.Text = ""
            lblRelatesMsgId.Text = ""
            lblMessageId.Text = ""
            lblDateSent.Text = ""

            lbl_Patient.Text = ""
            lbl_PatDOB.Text = ""
            lbl_PatGender.Text = ""
            lbl_PatientAddress.Text = ""
            lbl_PatientAddress2.Text = ""
            lbl_PatientPhoneNo.Text = ""
            lbl_CityName.Text = ""
            lbl_StateName.Text = ""
            lbl_ZIPCode.Text = ""


            lbl_Pharmacy.Text = ""
            lbl_PharmacyAddress.Text = ""
            lbl_PharmacyAddress2.Text = ""
            lbl_PharmacyPhoneNo.Text = ""
            lbl_PharamcyCity.Text = ""
            lbl_PharmacyState.Text = ""
            lbl_PharmacyZip.Text = ""
            lbl_PharmacyFax.Text = ""
            lbl_PharmacyNPI.Text = ""

            lbl_Provider.Text = ""
            lbl_ProviderAddress.Text = ""
            lbl_ProviderAddress2.Text = ""
            lbl_PrPhone.Text = ""
            lbl_ProviderCity.Text = ""
            lbl_ProviderState.Text = ""
            lbl_ProviderZIP.Text = ""
            lbl_ProviderFax.Text = ""
            lbl_ProviderNPI.Text = ""


            ''MD Drug Info  -------
            lblDrugName_Strength_Dosageform.Text = ""
            lblDrugQuantity.Text = ""
            lblDirection.Text = ""
            lblDrugNotes.Text = ""
            lblRefillQuantity.Text = ""
            lblWrittenDate.Text = ""
            lblSubstitution.Text = ""
            lbl_MDLastFillDate.Text = ""

            lbl_Ref_Qlfr.Text = ""
            lbl_MDRef_Qlfr.Text = ""

            ''Prescribed Drug Info  -------
            lbl_MPDrugName_Strength_Dosageform.Text = ""
            lbl_MPDrugQuantity.Text = ""
            lbl_MPDirection.Text = ""
            lbl_MPDrugnotes.Text = ""
            lbl_MPRefillQuantity.Text = ""
            lbl_MPWrittenDate.Text = ""
            lbl_MPSubstitution.Text = ""
            lbl_LastFillDate.Text = ""
            '----xx-----

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Function spitpanelprocessrefill()
        'pnl_Grid.Height = 490
        'pnl_Grid.Dock = DockStyle.Top
        pnl_RefillInfo.Height = 705 '350
        Return Nothing
    End Function
    Private Function Onepanelprocessrefill()

        pnl_Grid.Dock = DockStyle.Fill
        pnl_Grid.Height = 820 '825
        'pnl_RefillInfo.Height = 25
        Return Nothing
    End Function
    Private Sub btnupdown_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnupdown.Click
        Try
            If blnbtnstatus Then
                blnbtnstatus = False
                pnl_RefillInfo.Height = pnlError.Height
                btnupdown.BackgroundImage = Global.gloEMR.My.Resources.Down
                btnupdown.BackgroundImageLayout = ImageLayout.Center
            Else
                blnbtnstatus = True
                pnl_RefillInfo.Height = intRefillpanelheight
                btnupdown.BackgroundImage = Global.gloEMR.My.Resources.UP
                btnupdown.BackgroundImageLayout = ImageLayout.Center
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub mnuReloadRx_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuReloadRx.Click
        If IsNothing(RxTransactionID) Then
            RxTransactionID = 0
        End If

        'if the is no data against the RxTransactionID i.e. the PrescriptionId in the Prescription table then do not show the Prescription form.
        Dim strsql As String = "select nPatientid,sAmount,sNdccode,nProviderid,nPharmacyId from Prescription where nPrescriptionID = " & RxTransactionID

        Dim oDB As New gloStream.gloDataBase.gloDataBase
        oDB.Connect(GetConnectionString)
        Dim dt As DataTable = oDB.ReadQueryDataTable(strsql)
        oDB.Disconnect()

        If IsNothing(dt) Then
            MessageBox.Show("There is no prescription transaction found against this message. Review the message.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        ElseIf dt.Rows.Count <= 0 Then
            MessageBox.Show("There is no prescription transaction found against this message. Review the message.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub

        End If
        If RxTransactionID <> "0" AndAlso RxTransactionID <> "" Then
            'we were passing patient ID as 0 
            'When We get error message we get Prescription ID And from preescription Id we get the related details
            Dim frmRx As frmPrescription
            Dim npatientId As Long = 0
            npatientId = CType(dt.Rows(0)("nPatientid"), Long)
            Dim refRequest As New RefRequest(CType(RxTransactionID, Int64), dt.Rows(0)("sAmount"), lblRelatesMsgId.Text, DateTime.Now, msourceRelatesToMessageID, dt.Rows(0)("sNdccode"), dt.Rows(0)("nProviderid"), dt.Rows(0)("nPharmacyId"), npatientId)
            frmRx = frmPrescription.GetInstance(refRequest)
            If IsNothing(frmRx) = True Then
                Exit Sub
            End If

            'code added to avoid exception fix bug 14363
            If frmRx.blncancel = True Then
                frmRx.ShowInTaskbar = False
                frmRx.MdiParent = Me.MdiParent
                If frmRx.GetCurrentPatientID <> npatientId Then
                    frmRx.Setform = Me
                Else
                    frmRx.BringToFront()
                    frmRx.WindowState = FormWindowState.Maximized
                End If
                frmRx.ShowReconcileMessage()
                frmRx.Show()
            End If
        End If

    End Sub

    Private Sub mnuReview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuReview.Click
        Try
            Dim oErrorMessage As New gloEMRGeneralLibrary.ErrorMessage
            'changed the message in 6060 as discussed with Drew Sir related with bug 20120
            'If MessageBox.Show("Are you sure, you want to update the status of this message to " & """Reviewed""?" & "", "Error Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            If MessageBox.Show("This message will be taken off from the list and will no longer be available. Do you want to continue?", "Error Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                oErrorMessage.UpdateMessageTransaction(nTransactionId.ToString)

                Dim snd As Object = Nothing
                Dim evntArgs As EventArgs = Nothing
                ts_btnRefresh_Click(snd, evntArgs)

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
       

    End Sub
    'C

    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub
End Class