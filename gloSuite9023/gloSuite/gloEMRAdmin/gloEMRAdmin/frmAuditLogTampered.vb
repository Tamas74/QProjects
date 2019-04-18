Imports C1.Win.C1FlexGrid
Imports gloDatabaseLayer
Imports System.Linq


Public Class frmAuditLogTampered

#Region "Class Attributes"
    Private Shared frmAuditLog As frmAuditLogTampered    

    Private objTamperedDataSet As New DataSet
    Private objTamperedDataTable As New DataTable
    Private objOriginalDataTable As New DataTable
    Private frmAuditLogDetailsView As frmAuditLogTamperedDetails
    Dim objUpdated_With_nAuditTrailID_Tamper As EnumerableRowCollection(Of DataRow)
    Dim objUpdated_Without_nAuditTrailID_Tamper As EnumerableRowCollection(Of DataRow)
    Dim objDeletedDataRows As EnumerableRowCollection(Of DataRow)
    Dim objMatchedOriginalRow As EnumerableRowCollection(Of DataRow)

    Private columnTamperedID As Integer = 0
    Private columnTamperedUserName As Integer = 1
    Private columnActionType As Integer = 2
    Private columnTamperedDateTime As Integer = 3
    Private columnTamperedMachineName As Integer = 4
    Private columnGetMoreInfo As Integer = 5

    Private bIsAuditTrailIDUpdated As Boolean = False

    Private bIsMoreInfoPanelDisplayed As Boolean = False
#End Region
    
#Region "Form Initialization"

    Private Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Shared Function GetInstance() As frmAuditLogTampered
        Try
            If frmAuditLog Is Nothing Then
                frmAuditLog = New frmAuditLogTampered
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
        Return frmAuditLog
    End Function
#End Region
    
#Region "Data Binding"

    Private Sub LoadLINQRows()
        Try
            objUpdated_With_nAuditTrailID_Tamper = From Element As DataRow In objTamperedDataTable
                                                        Where (Element("sActionType") = "UPDATED" And Element("nAuditTrailID_Original") IsNot DBNull.Value)
                                                           Select Element


            objUpdated_Without_nAuditTrailID_Tamper = From ElementRow As DataRow In objTamperedDataTable
                                                      Where (ElementRow("sActionType") = "UPDATED" And ElementRow("nAuditTrailID_Original") Is DBNull.Value)
                                                        Select ElementRow

            objDeletedDataRows = From ElementDeletedRow As DataRow In objTamperedDataTable
                          Where (ElementDeletedRow("sActionType") = "DELETED")
                           Select ElementDeletedRow

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Function GetOriginalMatchedDataRow(ByVal nAuditTrailID As Integer) As DataRow
        objMatchedOriginalRow = From RequiredRow As DataRow In objOriginalDataTable
                                        Where RequiredRow("nAuditTrailID") = nAuditTrailID
                                            Select RequiredRow

        Return objMatchedOriginalRow(0)
    End Function
    Public Sub refreshgrid(stampered As String)
        Dim DatabaseLayer As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim objDBParameters As New gloDatabaseLayer.DBParameters()

        objDBParameters.Add(New gloDatabaseLayer.DBParameter("@GetCountOnly", 1, ParameterDirection.Input, SqlDbType.Int))

        DatabaseLayer.Connect(False)
        DatabaseLayer.Retrive("gsp_RetrieveAuditLogTampering", objDBParameters, objTamperedDataSet)
        DatabaseLayer.Disconnect()

        DatabaseLayer.Disconnect()
        DatabaseLayer = Nothing

        objDBParameters.Dispose()
        objDBParameters = Nothing

        objTamperedDataTable = objTamperedDataSet.Tables(0)
        objOriginalDataTable = objTamperedDataSet.Tables(1)
        flxData.DataSource = Nothing
        flxData.Cols.Count = 0
        bindGridControl()

        Dim rowIndex As Int64
        rowIndex = flxData.FindRow(stampered, 1, 0, False, True, False)
        flxData.Select(rowIndex, 0, True)

        flxData.Rows(rowIndex).Style.ForeColor = System.Drawing.Color.FromArgb(255, 197, 108)


    End Sub
    Private Sub bindGridControl()
        Dim dataColumn As C1.Win.C1FlexGrid.Column = flxData.Cols.Add
        Try
            With dataColumn
                .Name = "nTamperedID"
                .Caption = "TamperedID"
                .Visible = False

            End With

            dataColumn = flxData.Cols.Add

            With dataColumn
                .Name = "sTamperedUserName"
                .Caption = "User Name"
                .Width = frmAuditLog.Width * 24 / 100
            End With

            dataColumn = flxData.Cols.Add
            With dataColumn
                .Name = "sActionType"
                .Caption = "Action Taken"
                .Width = frmAuditLog.Width * 19 / 100
            End With

            dataColumn = flxData.Cols.Add
            With dataColumn
                .Name = "dtTamperedDateTime"
                .Caption = "Date and Time"
                .Width = frmAuditLog.Width * 24 / 100
            End With

            dataColumn = flxData.Cols.Add
            With dataColumn
                .Name = "sTamperedMachineName"
                .Caption = "Machine Name"
                .Width = frmAuditLog.Width * 24 / 100
            End With

            dataColumn = flxData.Cols.Add
            With dataColumn
                .Name = "getMoreInformation"
                .Width = 9
                .ImageAlign = ImageAlignEnum.CenterCenter
            End With

            dataColumn = flxData.Cols.Add
            With dataColumn
                .Name = "IsDetailTamperViewed"
                .Caption = "RecordNotViewed"
                .Width = 2
                .Visible = False
            End With

            With flxData
                .DataSource = objTamperedDataTable
                .DataMember = objTamperedDataTable.Columns("nTamperedID").ToString
                '.AutoSizeCols()
                .Refresh()
            End With
            Dim newstyle As C1.Win.C1FlexGrid.CellStyle
            newstyle = flxData.Styles.Add("newstyle")
            newstyle.ForeColor = Color.Red
            For counter As Integer = 1 To flxData.Rows.Count - 1
                flxData.SetCellImage(counter, Me.columnGetMoreInfo, ImageList1.Images(0))
                If flxData.Rows(counter)("IsDetailTamperViewed") = 0 Then
                    flxData.Rows(counter).Style = newstyle
                End If
            Next
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            dataColumn = Nothing
        End Try
    End Sub

#End Region

#Region "Form Events"

    Private Sub frmAuditLogTampered_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            'If tamperedlogstatus = True Then
            UpdateTamperedLogViewedStatus()
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub UpdateTamperedLogViewedStatus()
        Try
            Dim DatabaseLayer As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
            Dim objDBParameters As New gloDatabaseLayer.DBParameters()
            objDBParameters.Add(New gloDatabaseLayer.DBParameter("@logtampered", 0, ParameterDirection.Input, SqlDbType.Int))
            objDBParameters.Add(New gloDatabaseLayer.DBParameter("@ntamperedid", 0, ParameterDirection.Input, SqlDbType.BigInt))
            DatabaseLayer.Connect(False)
            DatabaseLayer.Execute("gsp_UpdateTamperedViewedStatus", objDBParameters)
            DatabaseLayer.Disconnect()
            DatabaseLayer.Disconnect()
            DatabaseLayer = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub frmAuditLogTampered_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            btnBack.BackgroundImage = ImageList1.Images(1)
            pnlRecordDeleted.Visible = False
            pnlAfterAlterationLabels.Visible = True
            pnlGridControl.Visible = True

            gloAddress.gloC1FlexStyle.Style(flxData, False)


            'ResetControls()

            Dim DatabaseLayer As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
            Dim objDBParameters As New gloDatabaseLayer.DBParameters()

            objDBParameters.Add(New gloDatabaseLayer.DBParameter("@GetCountOnly", 1, ParameterDirection.Input, SqlDbType.Int))

            DatabaseLayer.Connect(False)
            DatabaseLayer.Retrive("gsp_RetrieveAuditLogTampering", objDBParameters, objTamperedDataSet)
            DatabaseLayer.Disconnect()

            DatabaseLayer.Disconnect()
            DatabaseLayer = Nothing

            objDBParameters.Dispose()
            objDBParameters = Nothing

            objTamperedDataTable = objTamperedDataSet.Tables(0)
            objOriginalDataTable = objTamperedDataSet.Tables(1)

            bindGridControl()
            LoadLINQRows()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Sub evntFormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            If frmAuditLog IsNot Nothing Then
                objTamperedDataSet.Clear()
                objTamperedDataSet.Dispose()

                If frmAuditLogDetailsView IsNot Nothing Then
                    frmAuditLogDetailsView = Nothing
                End If

                objTamperedDataTable.Clear()
                objTamperedDataTable.Dispose()

                objOriginalDataTable.Clear()
                objOriginalDataTable.Dispose()

                objUpdated_With_nAuditTrailID_Tamper = Nothing
                objUpdated_Without_nAuditTrailID_Tamper = Nothing
                objDeletedDataRows = Nothing

                columnTamperedID = Nothing
                columnTamperedUserName = Nothing
                columnActionType = Nothing
                columnTamperedDateTime = Nothing
                columnTamperedMachineName = Nothing
                columnGetMoreInfo = Nothing
                bIsAuditTrailIDUpdated = Nothing
                bIsMoreInfoPanelDisplayed = Nothing

                frmAuditLog = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub
#End Region

#Region "Button and Datagrid Control Events"
    Private Sub btnClose_Click(sender As Object, e As System.EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    'Private Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
    '    Try
    '        If pnlLabels.Visible Then
    '            pnlLabels.Visible = False
    '            pnlGridControl.Visible = True
    '            pnlToolstrip.Visible = True

    '            With frmAuditLog
    '                .Width = 760
    '                .Height = 750
    '            End With
    '        End If
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
    '    End Try       
    'End Sub

    Private Sub flxData_Click(sender As Object, e As System.EventArgs) Handles flxData.Click
        Dim CurrentColumnClicked As HitTestInfo = flxData.HitTest        
        'Dim frmAuditLogDetailsView As frmAuditLogTamperedDetails = frmAuditLogDetailsView.

        Try
            If CurrentColumnClicked.Column = Me.columnGetMoreInfo And CurrentColumnClicked.Row <> -1 And CurrentColumnClicked.Row <> 0 Then


                If frmAuditLogDetailsView Is Nothing Then
                    frmAuditLogDetailsView = frmAuditLogDetailsView.GetInstance
                End If

                Dim sTamperedID As String = flxData.GetData(CurrentColumnClicked.Row, columnTamperedID).ToString
                Dim bFoundRecord As Boolean = False

                If objDeletedDataRows.Count > 0 Then
                    For Each ElementRow As DataRow In objDeletedDataRows
                        If ElementRow("nTamperedID").ToString = sTamperedID Then
                            bFoundRecord = True

                            frmAuditLogDetailsView.DisplayCurrentLogLabels(ElementRow)
                            frmAuditLogDetailsView.DisplayDeletedView()
                            frmAuditLogDetailsView.DisplayTamperingUserDetails(ElementRow)
                            Exit For
                        End If
                    Next
                End If

                If Not bFoundRecord Then
                    For Each ElementRow As DataRow In objUpdated_With_nAuditTrailID_Tamper
                        If ElementRow("nTamperedID").ToString = sTamperedID Then
                            bFoundRecord = True

                            If RecordWasDeletedLater(ElementRow("nAuditTrailID")) Then
                                frmAuditLogDetailsView.DisplayCurrentLogLabels(ElementRow)
                                frmAuditLogDetailsView.DisplayDeletedView()
                            Else
                                frmAuditLogDetailsView.DisplayCurrentLogLabels(ElementRow)
                                frmAuditLogDetailsView.DisplayAuditTrailIDUpdatedView()                                
                                frmAuditLogDetailsView.DisplayTamperingUserDetails(ElementRow)
                            End If                            
                            Exit For
                        End If
                    Next
                End If

                If Not bFoundRecord Then
                    For Each ElementRow As DataRow In objUpdated_Without_nAuditTrailID_Tamper
                        If ElementRow("nTamperedID").ToString = sTamperedID Then
                            bFoundRecord = True

                            If RecordWasDeletedLater(ElementRow("nAuditTrailID")) Then
                                frmAuditLogDetailsView.DisplayCurrentLogLabels(ElementRow)
                                frmAuditLogDetailsView.DisplayDeletedView()
                            Else
                                frmAuditLogDetailsView.DisplayCurrentLogLabels(ElementRow)
                                frmAuditLogDetailsView.DisplayNormalUpdatedView()                                
                                frmAuditLogDetailsView.DisplayTamperingUserDetails(ElementRow)

                                For Each DataRow As DataRow In objOriginalDataTable.Rows
                                    If DataRow("nAuditTrailID").ToString = ElementRow("nAuditTrailID") Then
                                        frmAuditLogDetailsView.DisplayAfterAlterationLabels(DataRow)
                                        frmAuditLogDetailsView.ChangeModifiedRecordColors()
                                        Exit For
                                    End If
                                Next
                            End If

                            Exit For
                        End If
                    Next
                End If

                If bFoundRecord Then
                    frmAuditLogDetailsView.stamperedid = sTamperedID
                    frmAuditLogDetailsView.ShowDialog()
                End If

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            CurrentColumnClicked = Nothing
        End Try
    End Sub

    Private Function RecordWasDeletedLater(ByVal sAuditTrailID As String) As Boolean
        Dim bFoundRecord As Boolean = False

        For Each ElementRow As DataRow In objDeletedDataRows
            If ElementRow("nAuditTrailID").ToString = sAuditTrailID Then
                bFoundRecord = True
                Exit For
            End If
        Next
        Return bFoundRecord
    End Function

#End Region

#Region "Panel and Label Displays"

    'Private Sub ResetControls()
    '    Try
    '        lblActivityDateTime.Text = String.Empty
    '        lblAfterActivity.Text = String.Empty

    '        lblCategory.Text = String.Empty
    '        lblAfterCategory.Text = String.Empty

    '        lblDescp.Text = String.Empty
    '        lblAfterDesc.Text = String.Empty

    '        lblMachineName.Text = String.Empty
    '        lblAfterMachineName.Text = String.Empty

    '        lblSoftwareComponent.Text = String.Empty
    '        lblAfterSoftwareComponent.Text = String.Empty

    '        lblOutcome.Text = String.Empty
    '        lblAfterOutcome.Text = String.Empty

    '        lblModule.Text = String.Empty
    '        lblAfterModule.Text = String.Empty

    '        lblType.Text = String.Empty
    '        lblAfterType.Text = String.Empty

    '        lblTamperingDateTime.Text = String.Empty
    '        lblTamperingMachineName.Text = String.Empty
    '        lblTamperingUserName.Text = String.Empty
    '        lblActionUnderTaken.Text = String.Empty
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
    '    End Try
    'End Sub

    'Private Sub DisplayAlteredLabels(ByVal Row As DataRow)
    '    Try
    '        lblActivityDateTime.Text = Row("dtActivityDateTime").ToString
    '        lblCategory.Text = Row("sActivityCategory").ToString
    '        lblDescp.Text = Row("sDescription").ToString
    '        lblMachineName.Text = Row("sMachineName").ToString
    '        lblSoftwareComponent.Text = Row("sSoftwareComponent").ToString
    '        lblOutcome.Text = Row("sOutcome").ToString
    '        lblModule.Text = Row("sActivityModule").ToString
    '        lblType.Text = Row("sActivityType").ToString

    '        With lblTamperingUserName
    '            .Text = Row("sTamperedUserName").ToString
    '            .ForeColor = Color.Red
    '        End With

    '        With lblActionUnderTaken
    '            .Text = Row("sActionType").ToString
    '            .ForeColor = Color.Red
    '        End With


    '        With lblTamperingDateTime
    '            .Text = Row("dtTamperedDateTime").ToString
    '            .ForeColor = Color.Red
    '        End With

    '        With lblTamperingMachineName
    '            .Text = Row("sTamperedMachineName").ToString
    '            .ForeColor = Color.Red
    '        End With
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
    '    End Try
    'End Sub

    'Private Sub ChangeTextColor(ByVal OriginalValue As Label, ByVal AlteredValue As Label)
    '    Try
    '        If OriginalValue.Text.ToString.Trim <> AlteredValue.Text.ToString.Trim Then
    '            AlteredValue.ForeColor = Color.Red
    '        Else
    '            Dim color As New Color
    '            color.FromArgb(31, 73, 125)
    '            AlteredValue.ForeColor = color
    '            color = Nothing

    '        End If
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
    '    End Try
    'End Sub

    'Private Sub DisplayCurrentLogLabels(ByVal Row As DataRow)
    '    Try
    '        lblActivityDateTime.Text = Row("dtActivityDateTime").ToString
    '        lblCategory.Text = Row("sActivityCategory").ToString
    '        lblDescp.Text = Row("sDescription").ToString
    '        lblMachineName.Text = Row("sMachineName").ToString
    '        lblSoftwareComponent.Text = Row("sSoftwareComponent").ToString
    '        lblOutcome.Text = Row("sOutcome").ToString
    '        lblModule.Text = Row("sActivityModule").ToString
    '        lblType.Text = Row("sActivityType").ToString
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
    '    End Try        
    'End Sub

    'Private Sub DisplayModifiedLogLabels(ByVal Row As DataRow)
    '    Try
    '        lblAfterActivity.Text = Row("dtActivityDateTime").ToString
    '        ChangeTextColor(lblActivityDateTime, lblAfterActivity)

    '        lblAfterCategory.Text = Row("sActivityCategory").ToString
    '        ChangeTextColor(lblCategory, lblAfterCategory)

    '        lblAfterDesc.Text = Row("sDescription").ToString
    '        ChangeTextColor(lblDescp, lblAfterDesc)

    '        lblAfterMachineName.Text = Row("sMachineName").ToString
    '        ChangeTextColor(lblMachineName, lblAfterMachineName)

    '        lblAfterSoftwareComponent.Text = Row("sSoftwareComponent").ToString
    '        ChangeTextColor(lblSoftwareComponent, lblAfterSoftwareComponent)

    '        lblAfterOutcome.Text = Row("sOutcome").ToString
    '        ChangeTextColor(lblOutcome, lblAfterOutcome)

    '        lblAfterModule.Text = Row("sActivityModule").ToString
    '        ChangeTextColor(lblModule, lblAfterModule)

    '        lblAfterType.Text = Row("sActivityType").ToString
    '        ChangeTextColor(lblType, lblAfterType)
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
    '    End Try       
    'End Sub

    'Private Sub DisplayDeletedView()
    '    Try
    '        pnlRecordDeleted.Visible = True
    '        pnlAfterAlterationLabels.Visible = False

    '        lblRowDeleted.Text = "This log entry was deleted from the database."

    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
    '    End Try
    'End Sub

    'Private Sub DisplayAuditTrailIDUpdatedView()
    '    Try
    '        pnlRecordDeleted.Visible = True
    '        pnlAfterAlterationLabels.Visible = False

    '        lblRowDeleted.Text = "The internal identification tag of this entry was modified."

    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
    '    End Try
    'End Sub

    'Private Sub DisplayNormalUpdatedView()
    '    Try
    '        pnlRecordDeleted.Visible = False
    '        pnlAfterAlterationLabels.Visible = True
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
    '    End Try
    'End Sub
#End Region

    
End Class