Imports System.Data.SqlClient
Imports gloEMRGeneralLibrary.gloEMRActors
Imports gloEMRGeneralLibrary.gloEMRPrescription
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports gloUserControlLibrary
Imports gloEMRGeneralLibrary.gloPrintFax
Imports gloEMRGeneralLibrary.gloGeneral
Imports gloEMRGeneralLibrary

Public Class frmRxRequest

    Dim _isInternetAvailable As Boolean = False 'use this variable throughtout the RxForm to add validation to confirm whether there is internet connection available 
    Private WithEvents objDrugControl As gloUC_CustomSearchInC1Flexgrid
    Private WithEvents oRxBusinessLayer As RxBusinesslayer

    Private Enum RefillStatus
        eApprove
        eDeny
        eCancel
    End Enum
    Dim C1_DataTable As New DataTable
    Dim _PrescriberId As String = ""
    Dim nPatientId_RefillReq As Long = 0
    Dim strMedicationName As String = ""
    Dim strPatientName As String = ""
    Dim strPharmacyName As String = ""
    'case 2297
    Dim strPatientLastName As String = ""
    Dim strPatientMiddleName As String = ""
    'case 2297
    Dim strPatientGender As String = ""
    Dim strPatientDOB As String = ""
    Dim nRxTransactionId As String = ""
    Dim strQuantity As String = ""
    Dim sRxReferenceNumber As String = ""
    Dim sPharmacyID As String = ""  'This is the NCPDPID for Pharmacies sending the RefillRequest
    Dim sMessageID As String = "" 'This is the MessageID used by Pharmacies to send a RefillRequest
    Dim dtdatereceived As DateTime
    Dim intRefillpanelheight As Int32
    Dim _blnSearch As Boolean = True 'for applying searching on grid
    Dim blnbtnstatus As Boolean = False
    Dim eRefillStatus As RefillStatus
    Dim DrugId As Int64 = 0
    Dim NDCCode As String = ""
    Dim wbBrowser As WebBrowser = Nothing
    Dim strPatientAddress1 As String = ""
    Dim strPatientAddress2 As String = ""
    Dim strPatientCity As String = ""
    Dim strPatientState As String = ""
    Dim strPatientZip As String = ""
    Dim strPatientPhone As String = ""
    Dim strPatientFax As String = ""
    '---------------C1 Flex Grid Columns name
    Private COL_COUNT As Integer = 63

    Private COL_Approve As Integer = 0
    Private COL_Deny As Integer = 1
    Private COL_Cancel As Integer = 2
    Private COL_RxReferenceNumber As Integer = 3
    Private COL_RxTransactionId As Integer = 4
    Private COL_PatientName As Integer = 5
    Private COL_PatientGender As Integer = 6
    Private COL_PatientDOB As Integer = 7
    Private COL_Medication As Integer = 8
    Private COL_Quantity As Integer = 9
    Private COL_PrescriptionDate As Integer = 10
    Private COL_DateReceived As Integer = 11
    Private COL_RefillQuantity As Integer = 12
    Private COL_PatientId As Integer = 13
    Private COL_LastFillDate As Integer = 14
    Private COL_RefillQualifier As Integer = 15
    Private COL_MessageID As Integer = 16
    Private COL_PharmacyID As Integer = 17
    Private COL_PatientLastName As Integer = 18
    Private COL_RefillReqNDCCode As Integer = 19
    Private COL_SenderSoftwareVersion As Integer = 20
    Private COL_SenderSoftwareDeveloper As Integer = 21
    Private COL_SenderSoftwareProduct As Integer = 22

    Private COL_PrescriberNPI As Integer = 23
    Private COL_CodeListQualifier As Integer = 24
    Private COL_UnitSourceCode As Integer = 25
    Private COL_PotencyUnitCode As Integer = 26

    Private COL_MDDrugName As Integer = 27
    Private COL_MDDrugQuantity As Integer = 28
    Private COL_MDDrugQualifier As Integer = 29
    Private COL_MDRefillQuantity As Integer = 30
    Private COL_MDRefillsQualifier As Integer = 31
    Private COL_MDdtWrittenDate As Integer = 32

    Private COL_MDDrugDuration As Integer = 33
    Private COL_MDDrugDirections As Integer = 34
    Private COL_MDNotes As Integer = 35
    Private COL_MDdtLastFillDate As Integer = 36

    Private COL_MDProductCode As Integer = 37
    Private COL_MDProductCodeQualifier As Integer = 38
    Private COL_MDDosageForm As Integer = 39
    Private COL_MDDrugStrength As Integer = 40
    Private COL_MDDrugStrengthUnits As Integer = 41

    Private COL_MDCodeListQualifier As Integer = 42
    Private COL_MDUnitSourceCode As Integer = 43
    Private COL_MDPotencyUnitCode As Integer = 43
    Private COL_MDDrugDBCode As Integer = 44
    Private COL_MDDrugDBCodeQualifier As Integer = 45
    Private COL_MDSubstitution As Integer = 46
    Private COL_DrugCoverageStatusCode As Integer = 47
    Private COL_MDDrugCoverageStatusCode As Integer = 48
    Private COL_PharmacySpecialty As Integer = 49
    Private COL_PatientRelationship As Integer = 50
    Private COL_DrugDirection As Integer = 51
    Private COL_dtWrittenDate As Integer = 52
    Private COL_Notes As Integer = 53
    Private COL_Substitution As Integer = 54
    Private COL_Drugstreangth As Integer = 55
    Private COL_PatAddr1 As Integer = 56
    Private COL_PatAddr2 As Integer = 57
    Private COL_PatCity As Integer = 58
    Private COL_Patstate As Integer = 59
    Private COL_PatZip As Integer = 60
    Private COL_PatPhone As Integer = 61
    Private COL_PatFax As Integer = 62

    Dim Dv_Search As DataView = Nothing


    Dim dvNext As DataView = Nothing
    ''''''To get the Patient Count in the DataView after Searching 
    Private _VisibleCount As Int16 = 0
    Dim _PatientID

    Dim _setDefaultPharmacy As Boolean = False

    '---------------C1 Flex Grid Columns name


    'Public Sub New()

    '    ' This call is required by the Windows Form Designer.
    '    InitializeComponent()

    '    ' Add any initialization after the InitializeComponent() call.
    '    intRefillpanelheight = pnl_RefillInfo.Height
    '    pnlProcessRefill.Height = intRefillpanelheight

    '    pnl_RefillInfo.Height = 0
    '    btnupdown.BackgroundImage = Global.gloEMR.My.Resources.ImgDownButton
    '    btnupdown.BackgroundImageLayout = ImageLayout.Center
    '    pnlProcessRefill.Visible = False
    '    pnlProcessRefill.SendToBack()
    'End Sub


    Public Sub New(ByVal PatientID As Long, Optional ByVal PrescriberId As String = "")

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        If PrescriberId <> "" Then
            _PrescriberId = PrescriberId
        End If

        intRefillpanelheight = pnl_RefillInfo.Height
        pnlProcessRefill.Height = intRefillpanelheight

        pnl_RefillInfo.Height = 0
        btnupdown.BackgroundImage = Global.gloEMR.My.Resources.Down
        btnupdown.BackgroundImageLayout = ImageLayout.Center
        pnlProcessRefill.Visible = False
        pnlProcessRefill.SendToBack()

        gloSureScript.gloSurescriptGeneral.sUserName = gstrSQLUserEMR
        gloSureScript.gloSurescriptGeneral.sPassword = gstrSQLPasswordEMR
        _PatientID = PatientID
    End Sub




    ''' <summary>
    ''' set the header coloumns names and width for the dgRefillList datagrid
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Private Sub setDataGridStyle(ByVal dt As DataTable)
        Dim _grdWidth As Int16 = (dgRefillList.Width - 15) / 10
        Try
            Dim ts As New clsDataGridTableStyle(dt.TableName)

            Dim RxRefNo As New DataGridTextBoxColumn
            With RxRefNo
                .Width = 0 'hide
                .MappingName = dt.Columns(0).ColumnName
                .NullText = ""
                .HeaderText = "Rx Reference Number"
            End With

            Dim RxTranId As New DataGridTextBoxColumn
            With RxTranId
                .Width = 0 'hide
                .MappingName = dt.Columns(1).ColumnName
                .NullText = "0"
                .HeaderText = "Rx TransactionId"
            End With

            Dim PatientName As New DataGridTextBoxColumn
            With PatientName
                .Width = _grdWidth * 1
                .NullText = ""
                .MappingName = dt.Columns(2).ColumnName
                .HeaderText = "Patient Name"
            End With

            Dim PatientGender As New DataGridTextBoxColumn
            With PatientGender
                .Width = _grdWidth * 1
                .NullText = ""
                .MappingName = dt.Columns(3).ColumnName
                .HeaderText = "Patient Gender"
            End With

            Dim PatientDOB As New DataGridTextBoxColumn
            With PatientDOB
                .Width = _grdWidth * 0.9
                .NullText = ""
                .MappingName = dt.Columns(4).ColumnName
                .HeaderText = "Patient DOB"
            End With


            Dim Medication As New DataGridTextBoxColumn
            With Medication
                .Width = _grdWidth * 1.5
                .NullText = ""
                .MappingName = dt.Columns(5).ColumnName
                .HeaderText = "Medication"
            End With


            Dim Quantity As New DataGridTextBoxColumn
            With Quantity
                .Width = _grdWidth * 0.7
                .NullText = ""
                .MappingName = dt.Columns(6).ColumnName
                .HeaderText = "Quantity"
            End With

            Dim RxDate As New DataGridTextBoxColumn
            With RxDate
                .Width = _grdWidth * 1.3
                .NullText = ""
                .MappingName = dt.Columns(7).ColumnName
                .HeaderText = "Prescription Date"
            End With

            Dim DateRecieved As New DataGridTextBoxColumn
            With DateRecieved
                .Width = _grdWidth * 1.3
                .NullText = ""
                .MappingName = dt.Columns(8).ColumnName
                .HeaderText = "Date Recieved"
            End With

            Dim RefillQuantity As New DataGridTextBoxColumn
            With RefillQuantity
                .Width = _grdWidth * 1
                .NullText = ""
                .MappingName = dt.Columns(9).ColumnName
                .HeaderText = "Refill Quantity"
            End With

            Dim nPatientId_RefillReq As New DataGridTextBoxColumn
            With nPatientId_RefillReq
                .Width = 0 'hide
                .NullText = ""
                .MappingName = dt.Columns(10).ColumnName
                .HeaderText = "Patient Id"
            End With

            Dim LastFillDate As New DataGridTextBoxColumn
            With LastFillDate
                .Width = _grdWidth * 1.4
                .NullText = ""
                .MappingName = dt.Columns(11).ColumnName
                .HeaderText = "Last FillDate"
            End With

            Dim RefillQualifier As New DataGridTextBoxColumn
            With RefillQualifier
                .Width = _grdWidth * 1.15
                .NullText = ""
                .MappingName = dt.Columns(12).ColumnName
                .HeaderText = "Refill Qualifier"
            End With


            Dim RxMessageID As New DataGridTextBoxColumn
            With RxMessageID
                .Width = 0 'hide
                .MappingName = dt.Columns(13).ColumnName
                .NullText = ""
                .HeaderText = "Message ID"
            End With

            Dim RxPharmacyID As New DataGridTextBoxColumn
            With RxPharmacyID
                .Width = 0 'hide
                .MappingName = dt.Columns(14).ColumnName
                .NullText = ""
                .HeaderText = "Pharmacy ID"
            End With

            'Case 2297
            Dim PatientLastName As New DataGridTextBoxColumn
            With PatientLastName
                .Width = 0 'hide
                .NullText = ""
                .MappingName = dt.Columns(15).ColumnName
                .HeaderText = "Patient LastName"
            End With

            ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {RxRefNo, RxTranId, PatientName, PatientGender, PatientDOB, Medication, Quantity, RxDate, DateRecieved, RefillQuantity, nPatientId_RefillReq, LastFillDate, RefillQualifier, RxMessageID, RxPharmacyID, PatientLastName})
            'Case 2297
            dgRefillList.TableStyles.Clear()
            dgRefillList.TableStyles.Add(ts)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub dgRefillList_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgRefillList.MouseClick
    '    Dim oRefillRequest As RefillRequest
    '    Try
    '        '*********************************************************************
    '        ''assign the values of the selected row to the variables so that some of the values that are assigned can be passed to the oRefillRequest.GetDrugDetailsToRefill(sRxReferenceNumber, dtdatereceived, nRxTransactionId) 
    '        Dim selectedRowNo As Integer = dgRefillList.HitTest(e.X, e.Y).Row


    '        strMedicationName = dgRefillList.Item(selectedRowNo, 5)

    '        strPatientName = dgRefillList.Item(selectedRowNo, 2)

    '        strPatientGender = dgRefillList.Item(selectedRowNo, 3)

    '        strPatientDOB = dgRefillList.Item(selectedRowNo, 4)

    '        '1st col is Rxtransactionid
    '        nRxTransactionId = dgRefillList.Item(selectedRowNo, 1)

    '        '5th col is Quantity
    '        strQuantity = dgRefillList.Item(selectedRowNo, 6)

    '        '0th col is Rx Reference number
    '        sRxReferenceNumber = dgRefillList.Item(selectedRowNo, 0)

    '        '7th col is Daterecieved
    '        dtdatereceived = dgRefillList.Item(selectedRowNo, 8)

    '        '9th coloumn is patient id 
    '        nPatientId_RefillReq = dgRefillList.Item(selectedRowNo, 10)
    '        '*********************************************************************

    '        oRefillRequest = New RefillRequest
    '        'Get Pharmcy and Prescriber and Patient info for selected refillrequest
    '        oRefillRequest.GetDrugDetailsToRefill(sRxReferenceNumber, dtdatereceived, nRxTransactionId)

    '        'Get data from ogloPrescription and set it to property procedures
    '        oRefillRequest.SetRefillRequestData()


    '        'assign values to Patient information labels from orefillrequest property procedures
    '        lbl_Patient.Text = oRefillRequest.PatientName
    '        lbl_PatDOB.Text = oRefillRequest.PatientDOB
    '        lbl_PatGender.Text = oRefillRequest.PatientGender
    '        lbl_PatientAddress.Text = oRefillRequest.PatientAddress
    '        lbl_PatientPhoneNo.Text = oRefillRequest.PatientPhone
    '        lbl_CityName.Text = oRefillRequest.PatientCity
    '        lbl_StateName.Text = oRefillRequest.PatientState
    '        lbl_ZIPCode.Text = oRefillRequest.PatientZip

    '        'assign values to Pharmacy information labels from orefillrequest property procedures
    '        lbl_Pharmacy.Text = oRefillRequest.PharmacyName
    '        lbl_PharmacyAddress.Text = oRefillRequest.PharmacyAddress
    '        lbl_PharmacyPhoneNo.Text = oRefillRequest.PharmacyPhone
    '        lbl_PharamcyCity.Text = oRefillRequest.PharmacyCity
    '        lbl_PharmacyState.Text = oRefillRequest.PharmacyState
    '        lbl_PharmacyZip.Text = oRefillRequest.PharmacyZip

    '        'assign values to Provider (means Prescriber) information labels from orefillrequest property procedures
    '        lbl_Provider.Text = oRefillRequest.ProviderName
    '        lbl_ProviderAddress.Text = oRefillRequest.ProviderAddress
    '        lbl_PrPhone.Text = oRefillRequest.ProviderPhone
    '        lbl_ProviderCity.Text = oRefillRequest.ProviderCity
    '        lbl_ProviderState.Text = oRefillRequest.ProviderState
    '        lbl_ProviderZIP.Text = oRefillRequest.ProviderZip
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub
    Private Function ConvertIfNothingToEmptyString(ByVal myObj As Object) As Object
        If (IsNothing(myObj)) Then
            Return ""
        End If
        Return myObj
    End Function
    ''NOT in USED
    ''' <summary>
    ''' call the context menus
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgRefillList_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgRefillList.MouseDown

        Dim strPatFirstName As String = ""
        Dim strPatLastName As String = ""
        Dim strPatMiddleName As String = ""
        Try

            RemoveDrugGridControl()
            If e.Button = Windows.Forms.MouseButtons.Right Then
                'if the mouse is clicked outside the boundry of the datagrid then dont show the context menu
                Dim selectedRowNo As Integer = dgRefillList.HitTest(e.X, e.Y).Row
                pnlProcessRefill.Visible = False
                pnlProcessRefill.SendToBack()
                'the mouse is clicked outside the datagrid
                If selectedRowNo < 0 Then
                    dgRefillList.ContextMenuStrip = Nothing
                    Exit Sub

                Else ' there is data in the grid
                    '3rd col is medication name
                    strMedicationName = dgRefillList.Item(selectedRowNo, 5)

                    'Case 2297
                    Dim retval As String() = SplitPatientName(dgRefillList.Item(selectedRowNo, 15))
                    If retval.Length > 1 Then
                        strPatFirstName = retval(0)
                        strPatLastName = retval(1)
                    Else
                        strPatFirstName = retval(0)
                        If strPatFirstName.Contains("|") Then
                            retval = SplitPatientName_WithPipe(strPatFirstName)
                            If retval.Length > 1 Then
                                strPatFirstName = retval(0)
                                strPatLastName = retval(1)
                            Else
                                strPatFirstName = retval(0)
                                strPatLastName = ""
                            End If
                        Else
                            strPatLastName = ""
                        End If

                    End If


                    strPatientName = strPatFirstName 'dgRefillList.Item(selectedRowNo, 2)

                    strPatientLastName = strPatLastName 'dgRefillList.Item(selectedRowNo, 15)
                    'Case 2297
                    'strPharmacyName = lbl_Pharmacy.Text
                    strPatientGender = dgRefillList.Item(selectedRowNo, 3)

                    strPatientDOB = dgRefillList.Item(selectedRowNo, 4)

                    '1st col is Rxtransactionid
                    If Not IsNothing(dgRefillList.Item(selectedRowNo, 1)) Then
                        nRxTransactionId = dgRefillList.Item(selectedRowNo, 1)

                    Else
                        nRxTransactionId = ""
                    End If



                    '5th col is Quantity
                    strQuantity = dgRefillList.Item(selectedRowNo, 6)

                    '0th col is Rx Reference number
                    sRxReferenceNumber = dgRefillList.Item(selectedRowNo, 0)

                    '7th col is Daterecieved

                    dtdatereceived = dgRefillList.Item(selectedRowNo, 8)

                    '9th coloumn is patient id 
                    If Not IsNothing(dgRefillList.Item(selectedRowNo, 10)) Then
                        If IsNumeric(dgRefillList.Item(selectedRowNo, 10)) Then
                            nPatientId_RefillReq = dgRefillList.Item(selectedRowNo, 10)

                        Else
                            nPatientId_RefillReq = 0
                        End If
                    Else
                        nPatientId_RefillReq = 0
                    End If


                    If Not IsNothing(dgRefillList.Item(selectedRowNo, 13)) Then
                        'If IsNumeric(dgRefillList.Item(selectedRowNo, 13)) Then
                        sMessageID = dgRefillList.Item(selectedRowNo, 13)

                        'Else
                        '    sMessageID = ""
                        'End If
                    Else
                        sMessageID = ""
                    End If

                    If Not IsNothing(dgRefillList.Item(selectedRowNo, 14)) Then
                        'If IsNumeric(dgRefillList.Item(selectedRowNo, 14)) Then
                        sPharmacyID = dgRefillList.Item(selectedRowNo, 14)

                        'Else
                        '    sPharmacyID = ""
                        'End If
                    Else
                        sPharmacyID = ""
                    End If

                    dgRefillList.Select(selectedRowNo)
                    'cntListmenuStrip.Items.Clear()

                    '---------------------------------------------------------------

                    Dim oRefillRequest As gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest
                    oRefillRequest = New gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest
                    'Get Pharmcy and Prescriber and Patient info for selected refillrequest
                    oRefillRequest.GetDrugDetailsToRefill(sMessageID, sRxReferenceNumber, nRxTransactionId)

                    'Get data from ogloPrescription and set it to property procedures
                    oRefillRequest.SetRefillRequestData()
                    ''MD Drug Info  -------
                    lblDrugName_Strength_Dosageform.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(1, 27)) & " " & ConvertIfNothingToEmptyString(C1RefillList.GetData(1, 40)) & " " & ConvertIfNothingToEmptyString(C1RefillList.GetData(1, 39))

                    lblDrugQuantity.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(1, 43))
                    lblDirection.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(1, 34))
                    lblDrugNotes.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(1, 35))
                    lblRefillQuantity.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(1, 30))
                    lblWrittenDate.Text = If(IsNothing(C1RefillList.GetData(1, 32)), "", Format(C1RefillList.GetData(1, 32), "MM/dd/yyyy"))
                    lblSubstitution.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(1, 46))


                    ''------xx------------------
                    lbl_Ref_Qlfr.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(1, COL_RefillQualifier))
                    lbl_MDRef_Qlfr.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(1, COL_MDRefillsQualifier))


                    ''Prescribed Drug Info  -------
                    lbl_MPDrugName_Strength_Dosageform.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(1, COL_Medication)) & " " & ConvertIfNothingToEmptyString(C1RefillList.GetData(1, COL_Drugstreangth))
                    lbl_MPDrugQuantity.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(1, COL_Quantity))
                    lbl_MPDirection.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(1, COL_DrugDirection))
                    lbl_MPDrugnotes.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(1, COL_Notes))
                    lbl_MPRefillQuantity.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(1, COL_RefillQuantity))
                    lbl_MPWrittenDate.Text = If(IsNothing(C1RefillList.GetData(1, COL_dtWrittenDate)), "", Format(C1RefillList.GetData(1, COL_dtWrittenDate), "MM/dd/yyyy"))
                    lbl_MPSubstitution.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(1, COL_Substitution))
                    '----xx-----
                    'drug Info
                    lbl_LastFillDate.Text = oRefillRequest.DTlastdate
                    lbl_MDLastFillDate.Text = oRefillRequest.MDDTlastdate
                    lbl_Duration.Text = If(oRefillRequest.Duration = "", "", oRefillRequest.Duration)
                    lbl_MDDuration.Text = If(oRefillRequest.MDDuration = "", "", oRefillRequest.MDDuration)

                    'assign values to Patient information labels from orefillrequest property procedures
                    lbl_Patient.Text = oRefillRequest.PatientName
                    lbl_PatDOB.Text = oRefillRequest.PatientDOB
                    ' lbl_PatGender.Text = oRefillRequest.PatientGender
                    Select Case oRefillRequest.PatientGender
                        Case "M"
                            lbl_PatGender.Text = "Male"
                        Case "Male"
                            lbl_PatGender.Text = "Male"
                        Case "F"
                            lbl_PatGender.Text = "Female"
                        Case "Female"
                            lbl_PatGender.Text = "Female"
                        Case Else
                            lbl_PatGender.Text = "Other"
                    End Select
                    lbl_PatientAddress.Text = oRefillRequest.PatientAddress
                    strPatientAddress1 = oRefillRequest.PatientAddress
                    lblPatientAddress2.Text = oRefillRequest.PatientAddress2 ''--
                    strPatientAddress2 = oRefillRequest.PatientAddress2
                    lbl_PatientPhoneNo.Text = oRefillRequest.PatientPhone
                    strPatientPhone = oRefillRequest.PatientPhone
                    lbl_CityName.Text = oRefillRequest.PatientCity
                    strPatientCity = oRefillRequest.PatientCity
                    lbl_StateName.Text = oRefillRequest.PatientState
                    strPatientState = oRefillRequest.PatientState
                    lbl_ZIPCode.Text = oRefillRequest.PatientZip
                    strPatientZip = oRefillRequest.PatientZip

                    'assign values to Pharmacy information labels from orefillrequest property procedures
                    lbl_Pharmacy.Text = oRefillRequest.PharmacyName
                    lbl_PharmacyAddress.Text = oRefillRequest.PharmacyAddress
                    lblPharmacyAddress2.Text = oRefillRequest.PharmacyAddress2
                    lbl_PharmacyPhoneNo.Text = oRefillRequest.PharmacyPhone
                    lbl_PharamcyCity.Text = oRefillRequest.PharmacyCity
                    lbl_PharmacyState.Text = oRefillRequest.PharmacyState
                    lbl_PharmacyZip.Text = oRefillRequest.PharmacyZip

                    lbl_PharmacyFax.Text = oRefillRequest.PharmacyFax
                    lbl_PharmacyNPI.Text = oRefillRequest.PharmacyNPI

                    'assign values to Provider (means Prescriber) information labels from orefillrequest property procedures
                    lbl_Provider.Text = oRefillRequest.ProviderName
                    lbl_ProviderAddress.Text = oRefillRequest.ProviderAddress
                    lblProviderAddress2.Text = oRefillRequest.ProviderAddress2
                    lbl_PrPhone.Text = oRefillRequest.ProviderPhone
                    lbl_ProviderCity.Text = oRefillRequest.ProviderCity
                    lbl_ProviderState.Text = oRefillRequest.ProviderState
                    lbl_ProviderZIP.Text = oRefillRequest.ProviderZip


                    lbl_ProviderFax.Text = oRefillRequest.ProviderFax
                    lbl_PrescriberNPI.Text = oRefillRequest.PrescriberNPI
                    pnl_RefillInfo.Visible = True

                    '---------------------------------------------------------------

                    ''call the AddMenu function only when there is rows(data present) in the dgRefillList datagrid
                    'AddMenu()
                    dgRefillList.ContextMenuStrip = cntListmenuStrip
                    oRefillRequest.Dispose()
                    oRefillRequest = Nothing
                End If
            Else
                pnlProcessRefill.Visible = False
                pnlProcessRefill.SendToBack()
                pnl_RefillInfo.Visible = True
            End If




        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''NOT in USED
    Private Sub dgRefillList_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgRefillList.MouseUp
        Dim strPatFirstName As String = ""
        Dim strPatLastName As String = ""
        Dim strPatMiddleName As String = ""
        Try
            Dim point As New Point(e.X, e.Y)
            Dim hti As DataGrid.HitTestInfo = dgRefillList.HitTest(point)
            RemoveDrugGridControl()
            If hti.Type = DataGrid.HitTestType.Cell Then
                'dgPatient.CurrentCell = New DataGridCell(hti.Row, hti.Column)
                ' ''
                If dgRefillList.CurrentRowIndex >= 0 Then
                    dgRefillList.UnSelect(dgRefillList.CurrentRowIndex)
                End If
                dgRefillList.CurrentRowIndex = hti.Row
                dgRefillList.Select(hti.Row)

                'case 2297
                If hti.Row > 0 Then
                    '3rd col is medication name
                    strMedicationName = dgRefillList.Item(hti.Row, 5)



                    'Case 2297
                    Dim retval As String() = SplitPatientName(dgRefillList.Item(hti.Row, 15))
                    If retval.Length > 1 Then
                        strPatFirstName = retval(0)
                        strPatLastName = retval(1)
                    Else
                        strPatFirstName = retval(0)
                        If strPatFirstName.Contains("|") Then
                            retval = SplitPatientName_WithPipe(strPatFirstName)
                            If retval.Length > 1 Then
                                strPatFirstName = retval(0)
                                strPatLastName = retval(1)
                            Else
                                strPatFirstName = retval(0)
                                strPatLastName = ""
                            End If
                        Else
                            strPatLastName = ""
                        End If

                    End If

                    strPatientName = strPatFirstName 'dgRefillList.Item(selectedRowNo, 2)

                    strPatientLastName = strPatLastName 'dgRefillList.Item(selectedRowNo, 15)
                    'Case 2297

                    strPatientGender = dgRefillList.Item(hti.Row, 3)

                    strPatientDOB = dgRefillList.Item(hti.Row, 4)

                    '1st col is Rxtransactionid
                    If Not IsNothing(dgRefillList.Item(hti.Row, 1)) Then
                        nRxTransactionId = dgRefillList.Item(hti.Row, 1)

                    Else
                        nRxTransactionId = ""
                    End If



                    '5th col is Quantity
                    strQuantity = dgRefillList.Item(hti.Row, 6)

                    '0th col is Rx Reference number
                    sRxReferenceNumber = dgRefillList.Item(hti.Row, 0)

                    '7th col is Daterecieved

                    dtdatereceived = dgRefillList.Item(hti.Row, 8)

                    '9th coloumn is patient id 
                    If Not IsNothing(dgRefillList.Item(hti.Row, 10)) Then
                        If IsNumeric(dgRefillList.Item(hti.Row, 10)) Then
                            nPatientId_RefillReq = dgRefillList.Item(hti.Row, 10)

                        Else
                            nPatientId_RefillReq = 0
                        End If
                    Else
                        nPatientId_RefillReq = 0
                    End If


                    If Not IsNothing(dgRefillList.Item(hti.Row, 13)) Then
                        'If IsNumeric(dgRefillList.Item(selectedRowNo, 13)) Then
                        sMessageID = dgRefillList.Item(hti.Row, 13)

                        'Else
                        '    sMessageID = ""
                        'End If
                    Else
                        sMessageID = ""
                    End If

                    If Not IsNothing(dgRefillList.Item(hti.Row, 14)) Then
                        'If IsNumeric(dgRefillList.Item(selectedRowNo, 14)) Then
                        sPharmacyID = dgRefillList.Item(hti.Row, 14)

                        'Else
                        '    sPharmacyID = ""
                        'End If
                    Else
                        sPharmacyID = ""
                    End If

                    dgRefillList.Select(hti.Row)
                    'cntListmenuStrip.Items.Clear()

                    '---------------------------------------------------------------
                    Dim oRefillRequest As gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest


                    oRefillRequest = New gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest
                    'Get Pharmcy and Prescriber and Patient info for selected refillrequest
                    oRefillRequest.GetDrugDetailsToRefill(sMessageID, sRxReferenceNumber, nRxTransactionId)

                    'Get data from ogloPrescription and set it to property procedures
                    oRefillRequest.SetRefillRequestData()

                    'drug Info
                    lbl_LastFillDate.Text = oRefillRequest.DTlastdate
                    lbl_MDLastFillDate.Text = oRefillRequest.MDDTlastdate
                    lbl_Duration.Text = If(oRefillRequest.Duration = "", "", oRefillRequest.Duration)
                    lbl_MDDuration.Text = If(oRefillRequest.MDDuration = "", "", oRefillRequest.MDDuration)

                    'assign values to Patient information labels from orefillrequest property procedures
                    lbl_Patient.Text = oRefillRequest.PatientName
                    lbl_PatDOB.Text = oRefillRequest.PatientDOB
                    ' lbl_PatGender.Text = oRefillRequest.PatientGender
                    Select Case oRefillRequest.PatientGender
                        Case "M"
                            lbl_PatGender.Text = "Male"
                        Case "Male"
                            lbl_PatGender.Text = "Male"
                        Case "F"
                            lbl_PatGender.Text = "Female"
                        Case "Female"
                            lbl_PatGender.Text = "Female"
                        Case Else
                            lbl_PatGender.Text = "Other"
                    End Select
                    lbl_PatientAddress.Text = oRefillRequest.PatientAddress
                    lblPatientAddress2.Text = oRefillRequest.PatientAddress2 ''--
                    lbl_PatientPhoneNo.Text = oRefillRequest.PatientPhone
                    lbl_CityName.Text = oRefillRequest.PatientCity
                    lbl_StateName.Text = oRefillRequest.PatientState
                    lbl_ZIPCode.Text = oRefillRequest.PatientZip

                    'assign values to Pharmacy information labels from orefillrequest property procedures
                    lbl_Pharmacy.Text = oRefillRequest.PharmacyName
                    lbl_PharmacyAddress.Text = oRefillRequest.PharmacyAddress
                    lblPharmacyAddress2.Text = oRefillRequest.PharmacyAddress2
                    lbl_PharmacyPhoneNo.Text = oRefillRequest.PharmacyPhone
                    lbl_PharamcyCity.Text = oRefillRequest.PharmacyCity
                    lbl_PharmacyState.Text = oRefillRequest.PharmacyState
                    lbl_PharmacyZip.Text = oRefillRequest.PharmacyZip

                    lbl_PharmacyFax.Text = oRefillRequest.PharmacyFax
                    lbl_PharmacyNPI.Text = oRefillRequest.PharmacyNPI

                    'assign values to Provider (means Prescriber) information labels from orefillrequest property procedures
                    lbl_Provider.Text = oRefillRequest.ProviderName
                    lbl_ProviderAddress.Text = oRefillRequest.ProviderAddress
                    lblProviderAddress2.Text = oRefillRequest.ProviderAddress2
                    lbl_PrPhone.Text = oRefillRequest.ProviderPhone
                    lbl_ProviderCity.Text = oRefillRequest.ProviderCity
                    lbl_ProviderState.Text = oRefillRequest.ProviderState
                    lbl_ProviderZIP.Text = oRefillRequest.ProviderZip


                    lbl_ProviderFax.Text = oRefillRequest.ProviderFax
                    lbl_PrescriberNPI.Text = oRefillRequest.PrescriberNPI
                    pnl_RefillInfo.Visible = True

                    '---------------------------------------------------------------

                    ''call the AddMenu function only when there is rows(data present) in the dgRefillList datagrid
                    'AddMenu()
                    dgRefillList.ContextMenuStrip = cntListmenuStrip
                    oRefillRequest.Dispose()
                    oRefillRequest = Nothing

                End If
                'case 2297

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''NOT in USED
    ''' <summary>
    ''' show the header coloumname for the search to be applied on
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgRefillList_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgRefillList.MouseDoubleClick
        Dim strPatFirstName As String = ""
        Dim strPatLastName As String = ""
        Dim strPatMiddleName As String = ""
        Try
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As DataGrid.HitTestInfo = dgRefillList.HitTest(ptPoint)
            RemoveDrugGridControl()


            If htInfo.Type = DataGrid.HitTestType.ColumnHeader Then


                Select Case htInfo.Column
                    Case 2 ' 2nd coloum is patient name
                        lblSearch.Text = "Patient Name"

                    Case 3 '3rd coloumn is medication
                        lblSearch.Text = "Medication"

                End Select
            End If
            If txtSearch.Text = "" Then
                _blnSearch = True
            Else
                _blnSearch = False
                txtSearch.Text = ""
                _blnSearch = True
            End If
            pnlProcessRefill.Visible = False
            pnlProcessRefill.SendToBack()

            '*********************************************************************
            ''assign the values of the selected row to the variables so that some of the values that are assigned can be passed to the oRefillRequest.GetDrugDetailsToRefill(sRxReferenceNumber, dtdatereceived, nRxTransactionId) 
            Dim selectedRowNo As Integer = dgRefillList.HitTest(e.X, e.Y).Row

            If selectedRowNo >= 0 Then
                strMedicationName = dgRefillList.Item(selectedRowNo, 5)

                'Case 2297
                Dim retval As String() = SplitPatientName(dgRefillList.Item(selectedRowNo, 15))

                If retval.Length > 1 Then
                    strPatFirstName = retval(0)
                    strPatLastName = retval(1)
                Else
                    strPatFirstName = retval(0)
                    If strPatFirstName.Contains("|") Then
                        retval = SplitPatientName_WithPipe(strPatFirstName)
                        If retval.Length > 1 Then
                            strPatFirstName = retval(0)
                            strPatLastName = retval(1)
                        Else
                            strPatFirstName = retval(0)
                            strPatLastName = ""
                        End If
                    Else
                        strPatLastName = ""
                    End If

                End If


                strPatFirstName = retval(0)
                strPatLastName = retval(1)

                strPatientName = strPatFirstName 'dgRefillList.Item(selectedRowNo, 2)

                strPatientLastName = strPatLastName 'dgRefillList.Item(selectedRowNo, 15)
                'Case 2297

                strPatientGender = dgRefillList.Item(selectedRowNo, 3)

                strPatientDOB = dgRefillList.Item(selectedRowNo, 4)

                '1st col is Rxtransactionid
                nRxTransactionId = dgRefillList.Item(selectedRowNo, 1)

                '5th col is Quantity
                strQuantity = dgRefillList.Item(selectedRowNo, 6)

                '0th col is Rx Reference number
                sRxReferenceNumber = dgRefillList.Item(selectedRowNo, 0)

                '7th col is Daterecieved
                dtdatereceived = dgRefillList.Item(selectedRowNo, 8)

                '9th coloumn is patient id 
                If IsDBNull(dgRefillList.Item(selectedRowNo, 10)) Then
                    nPatientId_RefillReq = 0
                Else
                    nPatientId_RefillReq = dgRefillList.Item(selectedRowNo, 10)
                End If

                If Not IsNothing(dgRefillList.Item(selectedRowNo, 13)) Then
                    'If IsNumeric(dgRefillList.Item(selectedRowNo, 13)) Then
                    sMessageID = dgRefillList.Item(selectedRowNo, 13)

                    'Else
                    '    sMessageID = ""
                    'End If
                Else
                    sMessageID = ""
                End If

                If Not IsNothing(dgRefillList.Item(selectedRowNo, 14)) Then
                    'If IsNumeric(dgRefillList.Item(selectedRowNo, 14)) Then
                    sPharmacyID = dgRefillList.Item(selectedRowNo, 14)
                    'Else
                    '    sPharmacyID = ""
                    'End If
                Else
                    sPharmacyID = ""
                End If
                '*********************************************************************
                Dim oRefillRequest As gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest = Nothing

                oRefillRequest = New gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest
                'Get Pharmcy and Prescriber and Patient info for selected refillrequest
                oRefillRequest.GetDrugDetailsToRefill(sMessageID, sRxReferenceNumber, nRxTransactionId)

                'Get data from ogloPrescription and set it to property procedures
                oRefillRequest.SetRefillRequestData()

                'drug Info
                lbl_LastFillDate.Text = oRefillRequest.DTlastdate
                lbl_MDLastFillDate.Text = oRefillRequest.MDDTlastdate
                lbl_Duration.Text = If(oRefillRequest.Duration = "", "", oRefillRequest.Duration)
                lbl_MDDuration.Text = If(oRefillRequest.MDDuration = "", "", oRefillRequest.MDDuration)

                'assign values to Patient information labels from orefillrequest property procedures
                lbl_Patient.Text = oRefillRequest.PatientName
                lbl_PatDOB.Text = oRefillRequest.PatientDOB
                'lbl_PatGender.Text = oRefillRequest.PatientGender
                Select Case oRefillRequest.PatientGender
                    Case "M"
                        lbl_PatGender.Text = "Male"
                    Case "Male"
                        lbl_PatGender.Text = "Male"
                    Case "F"
                        lbl_PatGender.Text = "Female"
                    Case "Female"
                        lbl_PatGender.Text = "Female"
                    Case Else
                        lbl_PatGender.Text = "Other"
                End Select
                lbl_PatientAddress.Text = oRefillRequest.PatientAddress
                lblPatientAddress2.Text = oRefillRequest.PatientAddress2 ''--
                lbl_PatientPhoneNo.Text = oRefillRequest.PatientPhone
                lbl_CityName.Text = oRefillRequest.PatientCity
                lbl_StateName.Text = oRefillRequest.PatientState
                lbl_ZIPCode.Text = oRefillRequest.PatientZip

                'assign values to Pharmacy information labels from orefillrequest property procedures
                lbl_Pharmacy.Text = oRefillRequest.PharmacyName
                lbl_PharmacyAddress.Text = oRefillRequest.PharmacyAddress
                lblPharmacyAddress2.Text = oRefillRequest.PharmacyAddress2
                lbl_PharmacyPhoneNo.Text = oRefillRequest.PharmacyPhone
                lbl_PharamcyCity.Text = oRefillRequest.PharmacyCity
                lbl_PharmacyState.Text = oRefillRequest.PharmacyState
                lbl_PharmacyZip.Text = oRefillRequest.PharmacyZip

                lbl_PharmacyFax.Text = oRefillRequest.PharmacyFax
                lbl_PharmacyNPI.Text = oRefillRequest.PharmacyNPI

                'assign values to Provider (means Prescriber) information labels from orefillrequest property procedures
                lbl_Provider.Text = oRefillRequest.ProviderName
                lbl_ProviderAddress.Text = oRefillRequest.ProviderAddress
                lblProviderAddress2.Text = oRefillRequest.ProviderAddress2
                lbl_PrPhone.Text = oRefillRequest.ProviderPhone
                lbl_ProviderCity.Text = oRefillRequest.ProviderCity
                lbl_ProviderState.Text = oRefillRequest.ProviderState
                lbl_ProviderZIP.Text = oRefillRequest.ProviderZip


                lbl_ProviderFax.Text = oRefillRequest.ProviderFax
                lbl_PrescriberNPI.Text = oRefillRequest.PrescriberNPI
                pnl_RefillInfo.Visible = True
                oRefillRequest.Dispose()
                oRefillRequest = Nothing

            End If



        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
    End Sub
    Private Sub RefreshRefillDetails()
        'assign values to Patient information labels from orefillrequest property procedures
        lbl_Patient.Text = ""
        lbl_PatDOB.Text = ""
        lbl_PatGender.Text = ""
        lbl_PatientAddress.Text = ""
        lblPatientAddress2.Text = ""
        lbl_PatientPhoneNo.Text = ""
        lbl_CityName.Text = ""
        lbl_StateName.Text = ""
        lbl_ZIPCode.Text = ""

        'assign values to Pharmacy information labels from orefillrequest property procedures
        lbl_Pharmacy.Text = ""
        lbl_PharmacyAddress.Text = ""
        lblPharmacyAddress2.Text = ""
        lbl_PharmacyPhoneNo.Text = ""
        lbl_PharamcyCity.Text = ""
        lbl_PharmacyState.Text = ""
        lbl_PharmacyZip.Text = ""
        lbl_PharmacyFax.Text = ""
        lbl_PharmacyNPI.Text = ""

        'assign values to Provider (means Prescriber) information labels from orefillrequest property procedures
        lbl_Provider.Text = ""
        lbl_ProviderAddress.Text = ""
        lblProviderAddress2.Text = ""
        lbl_PrPhone.Text = ""
        lbl_ProviderCity.Text = ""
        lbl_ProviderState.Text = ""
        lbl_ProviderZIP.Text = ""
        lbl_ProviderFax.Text = ""
        lbl_PrescriberNPI.Text = ""

        'drug Info
        lbl_LastFillDate.Text = ""
        lbl_MDLastFillDate.Text = ""
        lbl_Duration.Text = ""
        lbl_MDDuration.Text = ""

        ''MD Drug Info  -------
        lblDrugName_Strength_Dosageform.Text = ""

        lblDrugQuantity.Text = ""
        lblDirection.Text = ""
        lblDrugNotes.Text = ""
        lblRefillQuantity.Text = ""
        lblWrittenDate.Text = ""
        lblSubstitution.Text = ""

        lbl_Ref_Qlfr.Text = ""
        lbl_MDRef_Qlfr.Text = ""

        lbl_MPDrugName_Strength_Dosageform.Text = ""
        lbl_MPDrugQuantity.Text = ""
        lbl_MPDirection.Text = ""
        lbl_MPDrugnotes.Text = ""
        lbl_MPRefillQuantity.Text = ""
        lbl_MPWrittenDate.Text = ""
        lbl_MPSubstitution.Text = ""
        If Not IsNothing(wbBrowser) Then
            wbBrowser.Dispose()
            wbBrowser = Nothing
        End If
        pnlwbBrowser.Visible = False


    End Sub

    ''NOT in USED
    Private Function showDetails()
        Try

            strMedicationName = dgRefillList.Item(0, 5)

            strPatientName = dgRefillList.Item(0, 2)

            'Case 2297
            ' strPatientLastName = dgRefillList.Item(0, 15)
            Dim retval As String() = SplitPatientName(dgRefillList.Item(0, 15))
            'Dim strPatFirstName As String = retval(0)
            'Dim strPatLastName As String = retval(1)
            If retval.Length > 1 Then

                strPatientName = retval(0)
                strPatientLastName = retval(1)

            End If
            'Case 2297

            strPatientGender = dgRefillList.Item(0, 3)

            strPatientDOB = dgRefillList.Item(0, 4)

            '1st col is Rxtransactionid
            nRxTransactionId = dgRefillList.Item(0, 1)

            '5th col is Quantity
            strQuantity = dgRefillList.Item(0, 6)

            '0th col is Rx Reference number
            sRxReferenceNumber = dgRefillList.Item(0, 0)

            '7th col is Daterecieved
            dtdatereceived = dgRefillList.Item(0, 8)

            '9th coloumn is patient id 
            If IsDBNull(dgRefillList.Item(0, 10)) Then
                nPatientId_RefillReq = 0
            Else
                nPatientId_RefillReq = dgRefillList.Item(0, 10)
            End If
            '*********************************************************************

            If Not IsNothing(dgRefillList.Item(0, 13)) Then
                'If IsNumeric(dgRefillList.Item(0, 13)) Then
                sMessageID = dgRefillList.Item(0, 13)

                'Else
                '    sMessageID = ""
                'End If
            Else
                sMessageID = ""
            End If

            If Not IsNothing(dgRefillList.Item(0, 14)) Then
                ' IsNumeric(dgRefillList.Item(0, 14)) Then
                sPharmacyID = dgRefillList.Item(0, 14)

                'Else
                '    sPharmacyID = ""
                'End If
            Else
                sPharmacyID = ""
            End If
            Dim oRefillRequest As gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest

            oRefillRequest = New gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest
            'Get Pharmcy and Prescriber and Patient info for selected refillrequest
            oRefillRequest.GetDrugDetailsToRefill(sMessageID, sRxReferenceNumber, nRxTransactionId)

            'Get data from ogloPrescription and set it to property procedures
            oRefillRequest.SetRefillRequestData()


            'assign values to Patient information labels from orefillrequest property procedures
            lbl_Patient.Text = oRefillRequest.PatientName
            lbl_PatDOB.Text = oRefillRequest.PatientDOB
            'lbl_PatGender.Text = oRefillRequest.PatientGender
            Select Case oRefillRequest.PatientGender
                Case "M"
                    lbl_PatGender.Text = "Male"
                Case "Male"
                    lbl_PatGender.Text = "Male"
                Case "F"
                    lbl_PatGender.Text = "Female"
                Case "Female"
                    lbl_PatGender.Text = "Female"
                Case Else
                    lbl_PatGender.Text = "Other"
            End Select
            lbl_PatientAddress.Text = oRefillRequest.PatientAddress
            lblPatientAddress2.Text = oRefillRequest.PatientAddress2
            lbl_PatientPhoneNo.Text = oRefillRequest.PatientPhone
            lbl_CityName.Text = oRefillRequest.PatientCity
            lbl_StateName.Text = oRefillRequest.PatientState
            lbl_ZIPCode.Text = oRefillRequest.PatientZip

            'assign values to Pharmacy information labels from orefillrequest property procedures
            lbl_Pharmacy.Text = oRefillRequest.PharmacyName
            lbl_PharmacyAddress.Text = oRefillRequest.PharmacyAddress
            lblPharmacyAddress2.Text = oRefillRequest.PharmacyAddress2
            lbl_PharmacyPhoneNo.Text = oRefillRequest.PharmacyPhone
            lbl_PharamcyCity.Text = oRefillRequest.PharmacyCity
            lbl_PharmacyState.Text = oRefillRequest.PharmacyState
            lbl_PharmacyZip.Text = oRefillRequest.PharmacyZip

            lbl_PharmacyFax.Text = oRefillRequest.PharmacyFax
            lbl_PharmacyNPI.Text = oRefillRequest.PharmacyNPI

            'assign values to Provider (means Prescriber) information labels from orefillrequest property procedures
            lbl_Provider.Text = oRefillRequest.ProviderName
            lbl_ProviderAddress.Text = oRefillRequest.ProviderAddress
            lblProviderAddress2.Text = oRefillRequest.ProviderAddress2
            lbl_PrPhone.Text = oRefillRequest.ProviderPhone
            lbl_ProviderCity.Text = oRefillRequest.ProviderCity
            lbl_ProviderState.Text = oRefillRequest.ProviderState
            lbl_ProviderZIP.Text = oRefillRequest.ProviderZip

            lbl_ProviderFax.Text = oRefillRequest.ProviderFax
            lbl_PrescriberNPI.Text = oRefillRequest.PrescriberNPI
            oRefillRequest.Dispose()
            oRefillRequest = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return Nothing
    End Function

    ''' <summary>
    ''' show the context menu only when there is data in the grid
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddMenu()
        Try
            cntListmenuStrip.ImageList = ImageList1
            Dim tlstripitem As ToolStripMenuItem
            tlstripitem = New ToolStripMenuItem

            tlstripitem.Text = "Approve"
            tlstripitem.Tag = 1
            tlstripitem.ImageIndex = 0
            cntListmenuStrip.Items.Add(tlstripitem)
            AddHandler tlstripitem.Click, AddressOf StripItem_Click
            tlstripitem = Nothing

            tlstripitem = New ToolStripMenuItem
            tlstripitem.Text = "Deny"
            tlstripitem.Tag = 2
            tlstripitem.ImageIndex = 1

            cntListmenuStrip.Items.Add(tlstripitem)
            AddHandler tlstripitem.Click, AddressOf StripItem_Click
            tlstripitem = Nothing

            'tlstripitem = New ToolStripMenuItem
            'tlstripitem.Text = "Deny W/New Rx"
            'tlstripitem.Tag = 3
            'tlstripitem.ImageIndex = 2
            'cntListmenuStrip.Items.Add(tlstripitem)
            'AddHandler tlstripitem.Click, AddressOf StripItem_Click
            'tlstripitem = Nothing

            ''Show Cancel only if this setting is OFF from admin setting->surescript setting
            If gblnAllowRefillCancel = False Then
                '''''Add Cancel menu for updating the status flag / Status Remark
                tlstripitem = New ToolStripMenuItem
                tlstripitem.Text = "Cancel"
                tlstripitem.Tag = 3
                tlstripitem.ImageIndex = 4

                cntListmenuStrip.Items.Add(tlstripitem)
                AddHandler tlstripitem.Click, AddressOf StripItem_Click
                tlstripitem = Nothing
            End If



        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    ''' <summary>
    ''' 1) on the "Approved" click show the Rx form 2) check the selected patientid with gnPatientid 3)pass the parameters to the Rx constructor.
    ''' 2) On the "Denied" click show the "frmDenyRefillRequest" form.
    ''' 3) On the "Denied with New Rx to follow" show the "frmDenyRefillRequest" form and on the OK click check selected patientid of refillRequest form with the gnpatientid and then Open the Rx form.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub StripItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim oRefillRequest As gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest = Nothing
        Try


            Select Case CType(sender, ToolStripMenuItem).Text

                'open the Prescription form directly and to the constructor of that Rx form pass the RxTransactionId and RequestedrefillQuantity 
                Case "Approve"

                    oRefillRequest = New gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest
                    If nRxTransactionId <> "" AndAlso nRxTransactionId <> "0" Then
                        If Not IsNumeric(nRxTransactionId) Then
                            nRxTransactionId = "0"
                        End If
                    End If
                    
                    eRefillStatus = RefillStatus.eApprove
                    lblDenialReasoncode.Visible = True
                    cmbDenialReasonCode.Visible = True

                    oRefillRequest.GetPrescriberPharmacyID(sRxReferenceNumber, dtdatereceived, sMessageID)

                    If nPatientId_RefillReq <> 0 Then
                        Dim dtPatient As DataTable = Nothing
                        Dim blnDemographicsChange As Boolean = False
                        dtPatient = GetPatientInfo(nPatientId_RefillReq)
                        If Not IsNothing(dtPatient) Then
                            If dtPatient.Rows.Count > 0 Then
                                Dim sPatFirstName As String = ""
                                Dim sPatLastName As String = ""
                                Dim sPatDoB As String = ""
                                Dim sPatGender As String = ""
                                Dim sPatMiddleName As String = ""
                                sPatFirstName = dtPatient.Rows(0)("sFirstName").trim()
                                sPatLastName = dtPatient.Rows(0)("sLastName").trim()
                                sPatDoB = dtPatient.Rows(0)("dtDOB").trim()
                                sPatGender = dtPatient.Rows(0)("sGender").trim()
                                sPatMiddleName = dtPatient.Rows(0)("sMiddleName").trim()
                                If strPatientName.Trim <> "" Then
                                    If strPatientName.Trim <> sPatFirstName Then
                                        blnDemographicsChange = True
                                    End If
                                End If
                                If blnDemographicsChange = False Then
                                    If strPatientLastName.Trim <> "" Then
                                        If strPatientLastName.Trim <> sPatLastName Then
                                            blnDemographicsChange = True
                                        End If
                                    End If
                                End If
                                If blnDemographicsChange = False Then
                                    If strPatientGender.Trim <> "" Then
                                        Select Case strPatientGender
                                            Case "M"
                                                strPatientGender = "Male"
                                            Case "Male"
                                                strPatientGender = "Male"
                                            Case "F"
                                                strPatientGender = "Female"
                                            Case "Female"
                                                strPatientGender = "Female"
                                            Case Else
                                                strPatientGender = "Other"
                                        End Select
                                        If strPatientGender.Trim <> sPatGender Then
                                            blnDemographicsChange = True
                                        End If
                                    End If
                                End If
                                If blnDemographicsChange = False Then
                                    If strPatientDOB.Trim <> "" Then
                                        sPatDoB = CDate(sPatDoB).ToString("yyyy-MM-dd")
                                        If strPatientDOB.Trim <> sPatDoB Then
                                            blnDemographicsChange = True
                                        End If
                                    End If
                                End If

                                If blnDemographicsChange = False Then
                                    If sPatMiddleName <> "" And strPatientMiddleName <> "" Then
                                        blnDemographicsChange = CompareMiddleName(sPatMiddleName, strPatientMiddleName)
                                    End If
                                End If

                                If blnDemographicsChange = True Then
                                    nPatientId_RefillReq = 0
                                End If
                            End If
                            dtPatient.Dispose()
                            dtPatient = Nothing
                        End If
                    End If


                    If nPatientId_RefillReq = 0 Then
                        Dim strPatFirstName As String = strPatientName
                        Dim strPatLastName As String = strPatientLastName
                        Dim strpatmiddleName As String = strPatientMiddleName
                        
                        Dim strPataddr As String = ""
                        If Not IsNothing(oRefillRequest.PatientAddress) Then
                            strPataddr = oRefillRequest.PatientAddress
                        End If

                        Dim strPhone As String = ""
                        If Not IsNothing(oRefillRequest.PatientPhone) Then
                            strPhone = oRefillRequest.PatientPhone
                        End If

                        Dim strCity As String = ""
                        If Not IsNothing(oRefillRequest.PatientCity) Then
                            strCity = oRefillRequest.PatientCity
                        End If

                        Dim strState As String = ""
                        If Not IsNothing(oRefillRequest.PatientState) Then
                            strState = oRefillRequest.PatientState
                        End If

                        Dim strZip As String = ""
                        If Not IsNothing(oRefillRequest.PatientZip) Then
                            strZip = oRefillRequest.PatientZip
                        End If

                        Dim strPharmacyname As String = ""
                        If Not IsNothing(oRefillRequest.PharmacyName) Then
                            strPharmacyname = oRefillRequest.PharmacyName
                        End If

                        Dim strPharmacyid As String = ""
                        If Not IsNothing(oRefillRequest.PharmacyID) Then
                            strPharmacyid = oRefillRequest.PharmacyID ''''''this is the ContactID
                        End If

                        Dim strPharmacyNCPDPID As String = ""
                        If Not IsNothing(oRefillRequest.PharmacyNCPDPID) Then
                            strPharmacyNCPDPID = oRefillRequest.PharmacyNCPDPID ''''''this is the NCPDPID
                        End If

                        Dim strproviderName As String = ""
                        If Not IsNothing(oRefillRequest.ProviderName) Then
                            strproviderName = oRefillRequest.ProviderName
                        End If

                        Dim strProviderId As String = ""
                        If Not IsNothing(oRefillRequest.ProviderID) Then
                            strProviderId = oRefillRequest.ProviderID
                            If strProviderId = "" Then
                                strProviderId = "0"
                            End If
                        Else
                            strProviderId = "0"
                        End If
                        ''-- Get EPCS Enabled or not
                        Set_EPCSProviderEnabled(Convert.ToInt64(strProviderId))

                        Select Case strPatientGender

                            Case "M"
                                strPatientGender = "Male"
                            Case "Male"
                                strPatientGender = "Male"
                            Case "F"
                                strPatientGender = "Female"
                            Case "Female"
                                strPatientGender = "Female"
                            Case Else
                                strPatientGender = "Other"
                        End Select


                        'Case 2297
                        'get the patient information with matching First name, Last name, DOB and Gender as parameters
                        Dim _getPatIdSQL As String = "select nPatientID,nProviderID from patient where substring(sFirstName,1,35) = " & "'" & strPatFirstName.Replace("'", "''") & "'" & " and substring(sLastName,1,35) = " & "'" & strPatLastName.Replace("'", "''") & "'" & " and convert(datetime,convert (varchar(50),datepart(mm,dtDOB)) + '/'+ convert(varchar(50),datepart(dd,dtDOB))+'/'+ convert(varchar(50),datepart(yy,dtDOB))) = " & "'" & strPatientDOB & "'" & " and sGender = " & "'" & strPatientGender & "'" & "  "
                        If strpatmiddleName <> "" Then
                            Dim strSurescriptMiddleName As String = ""
                            Dim i As Integer
                            i = strpatmiddleName.Length.ToString
                            Select Case i
                                Case 1
                                    strSurescriptMiddleName = " and substring(sMiddleName,1,1)= " & "'" & strpatmiddleName.Replace("'", "''") & "'"
                                Case 2
                                    strSurescriptMiddleName = " and substring(sMiddleName,1,2)= " & "'" & strpatmiddleName.Replace("'", "''") & "'"
                                Case Else
                                    strSurescriptMiddleName = " and sMiddleName= " & "'" & strpatmiddleName.Replace("'", "''") & "'"
                            End Select
                            _getPatIdSQL = _getPatIdSQL & strSurescriptMiddleName
                        End If


                        Dim _PatID As String = 0
                        Dim _gloEMRDBID As New DataBaseLayer

                        Try
                            Using _dtPatient As DataTable = _gloEMRDBID.GetDataTable_Query(_getPatIdSQL)
                                If (_dtPatient IsNot Nothing) Then
                                    If (_dtPatient.Rows.Count > 0) Then
                                        Dim _drPatient As DataRow = _dtPatient.Select("nProviderID=" & strProviderId).FirstOrDefault()

                                        If (_drPatient IsNot Nothing) Then
                                            _PatID = _drPatient("nPatientId")
                                        Else
                                            _PatID = _dtPatient(0)("nPatientId")
                                        End If
                                    End If
                                End If
                            End Using

                            If _PatID = 0 Then

                                '''''' Attempt PatientID
                                Dim _registeredPatID As Long = AttemptPatientID(strPatFirstName, strPatLastName, strPharmacyNCPDPID, strProviderId, strproviderName)

                                If _registeredPatID > 0 Then
                                    nPatientId_RefillReq = _registeredPatID
                                    If C1RefillList.RowSel > 0 Then
                                        C1RefillList.SetData(C1RefillList.RowSel, COL_PatientId, _registeredPatID)
                                    End If
                                Else
                                    Exit Sub
                                End If
                            Else
                                nPatientId_RefillReq = CType(_PatID, Int64)
                            End If
                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        Finally
                            If Not IsNothing(_gloEMRDBID) Then
                                _gloEMRDBID.Dispose()
                                _gloEMRDBID = Nothing
                            End If
                        End Try
                    End If

                    If MainMenu.IsAccess(False, nPatientId_RefillReq, False, True) = False Then
                        If Not IsNothing(oRefillRequest) Then
                            oRefillRequest.Dispose()
                            oRefillRequest = Nothing
                        End If
                        Exit Sub
                    End If

                    ''''''''pass the selected approve row NDC code to fetch the correct drug info
                    Dim _refreqNDCCode As String = ""
                    Dim _refreqMedPrescribedNDCCode As String = ""
                    Dim _strMedDespenceDrugName As String = ""
                    If Not IsNothing(C1RefillList.GetData(C1RefillList.RowSel, COL_MDProductCode)) Then
                        _refreqNDCCode = C1RefillList.GetData(C1RefillList.RowSel, COL_MDProductCode)
                    End If
                    If Not IsNothing(C1RefillList.GetData(C1RefillList.RowSel, COL_RefillReqNDCCode)) Then
                        _refreqMedPrescribedNDCCode = C1RefillList.GetData(C1RefillList.RowSel, COL_RefillReqNDCCode)
                    End If
                    _strMedDespenceDrugName = ConvertIfNothingToEmptyString(C1RefillList.GetData(C1RefillList.RowSel, 27)) & " " & ConvertIfNothingToEmptyString(C1RefillList.GetData(C1RefillList.RowSel, 40)) & " " & ConvertIfNothingToEmptyString(C1RefillList.GetData(C1RefillList.RowSel, 39))

                    Dim _SupplyFlag As Boolean = False
                    If _refreqNDCCode = "" Then
                        If Not IsNothing(C1RefillList.GetData(C1RefillList.RowSel, COL_DrugCoverageStatusCode)) Then
                            If C1RefillList.GetData(C1RefillList.RowSel, COL_DrugCoverageStatusCode) <> "" Then
                                If C1RefillList.GetData(C1RefillList.RowSel, COL_DrugCoverageStatusCode) = "SP" Then
                                    _SupplyFlag = True
                                End If
                            End If
                        End If
                        If _SupplyFlag = False Then
                            MessageBox.Show("The refill request will not be approved/denied since the drug is sent without an NDCCode, the refill request needs to be cancelled.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            If Not IsNothing(oRefillRequest) Then
                                oRefillRequest.Dispose()
                                oRefillRequest = Nothing
                            End If
                            Exit Sub
                        End If
                    End If
                   
                    Dim DEAClass As Int16? = Me.GetDEASchedule(_refreqNDCCode)

                    If DEAClass.HasValue Then
                        If DEAClass.Value >= 2 Then
                            If gbIsProviderEPCSEnable Then
                                oRefillRequest = New gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest
                                oRefillRequest.GetDrugDetailsToRefill(sMessageID, sRxReferenceNumber, nRxTransactionId)
                                oRefillRequest.SetRefillRequestData()
                                If IsNothing(oRefillRequest.PharmacyNCPDPID) Then
                                    oRefillRequest.GetPrescriberPharmacyID(sRxReferenceNumber, dtdatereceived, sMessageID)
                                End If
                                If Not IsNothing(oRefillRequest.ogloPrescription.DrugsCol) Then
                                    If ValidateEPCSData(oRefillRequest) = False Then
                                        Exit Sub
                                    End If
                                    If oRefillRequest.ogloPrescription.DrugsCol.Item(0).MDRefillQualifier.Trim().ToUpper() = "PRN" Then
                                        If MessageBox.Show("This refill request is for controlled substance with PRN refill qualifier. Refill for controlled substance cannot be approved with PRN refill qualifier. " & vbCrLf & vbCrLf & "Do you want to approve this refill request without PRN?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                                            Exit Sub
                                        End If
                                    End If
                                End If
                            Else
                                Dim strMsgBld As New System.Text.StringBuilder
                                strMsgBld.Append("You have chosen to approve a refill request for a controlled substance. A new printed prescription will be generated and provided to the pharmacy. " & vbCrLf & "")
                                strMsgBld.Append("Would you like to proceed with printing the prescription?")

                                If MessageBox.Show(strMsgBld.ToString, "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.No Then
                                    If oRefillRequest IsNot Nothing Then
                                        oRefillRequest.Dispose()
                                        oRefillRequest = Nothing
                                    End If
                                    Exit Sub
                                End If

                                If strMsgBld IsNot Nothing Then
                                    strMsgBld.Clear()
                                    strMsgBld = Nothing
                                End If
                            End If
                        End If
                    Else
                        Dim _sDrugName As String = ""
                        If _refreqNDCCode <> _refreqMedPrescribedNDCCode Then
                            _sDrugName = _strMedDespenceDrugName
                        Else
                            _sDrugName = strMedicationName
                        End If

                        If MessageBox.Show("Exact NDC for ‘" & _sDrugName & "' is not found in the drug database. Do you want to select the drug with another NDC?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            If _SupplyFlag = False Then
                                showSearchUserControl()
                            Else
                                Dim strdrugName As String = ""
                                strdrugName = C1RefillList.GetData(C1RefillList.RowSel, COL_Medication)
                                showSearchUserControl(strdrugName)
                            End If
                        End If

                        If Not IsNothing(oRefillRequest) Then
                            oRefillRequest.Dispose()
                            oRefillRequest = Nothing
                        End If
                        Exit Sub
                    End If

                    If nPatientId_RefillReq <> _PatientID Then
                        CType(Me.MdiParent, MainMenu).ShowDefaultPatientDetails(nPatientId_RefillReq)
                    End If

                    If strPharmacyName <> "" Then
                        CheckAndSetPharmacy(oRefillRequest)
                    End If

                    Dim intRxTransactionID As Int64 = 0
                    Dim frmRx As frmPrescription = Nothing

                    Int64.TryParse(nRxTransactionId, intRxTransactionID)

                    Dim refRequest As New RefRequest(intRxTransactionID, strQuantity, sRxReferenceNumber, dtdatereceived, sMessageID, _refreqNDCCode, oRefillRequest.ProviderID, oRefillRequest.PharmacyID, nPatientId_RefillReq)
                    frmRx = frmPrescription.GetInstance(refRequest)

                    If IsNothing(frmRx) = True Then
                        If Not IsNothing(oRefillRequest) Then
                            oRefillRequest.Dispose()
                            oRefillRequest = Nothing
                        End If

                        Exit Sub
                    End If

                    If frmRx.blncancel = True Then                        
                        frmRx.ShowInTaskbar = False
                        frmRx.MdiParent = Me.MdiParent
                        'new condition added to fix bug 14363
                        If frmRx.GetCurrentPatientID <> nPatientId_RefillReq Then
                            frmRx.Setform = Me
                        Else
                            frmRx.IsDefaultPharmacyChanged = _setDefaultPharmacy
                            frmRx.RefillRequest(nPatientId_RefillReq, 0, strQuantity, sRxReferenceNumber, dtdatereceived, sMessageID, _refreqNDCCode, oRefillRequest.ProviderID, oRefillRequest.PharmacyID)
                            frmRx.BringToFront()
                            frmRx.WindowState = FormWindowState.Maximized
                        End If

                        frmRx.Show()

                    End If
                   
                Case "Deny"

                    Dim _refreqNDCCode As String = ""
                    If Not IsNothing(C1RefillList.GetData(C1RefillList.RowSel, COL_MDProductCode)) Then
                        _refreqNDCCode = C1RefillList.GetData(C1RefillList.RowSel, COL_MDProductCode)
                    End If
                    If _refreqNDCCode = "" Then
                        MessageBox.Show("The refill request will not be approved/denied since the drug is sent without an NDCCode, the refill request needs to be cancelled.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If

                    eRefillStatus = RefillStatus.eDeny

                    blnbtnstatus = True
                    spitpanelprocessrefill()
                    btnupdown.BackgroundImage = Global.gloEMR.My.Resources.Down    ''ImgUpButton
                    btnupdown.BackgroundImageLayout = ImageLayout.Center

                    pnlProcessRefill.Visible = True
                    pnl_RefillInfo.Visible = False
                    pnlwbBrowser.Visible = False
                    pnlProcessRefill.BringToFront()
                    lblDenialReasoncode.Visible = True
                    cmbDenialReasonCode.Visible = True
                    lblMedicationItemName.Text = strMedicationName
                    txtNotes.Text = ""

                Case "Cancel"


                    If MessageBox.Show("Are you sure you want to cancel this refill request? It will be removed from the list and will no longer be accessed.", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then


                        eRefillStatus = RefillStatus.eCancel
                        oRefillRequest = New gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest
                        oRefillRequest.UpdateStatusCancel(sRxReferenceNumber, sMessageID)

                        ''GetPendingRefillRequest()
                        GetPendingRefillRequestFORC1()
                    End If

                    'pnlProcessRefill.Top = pnl_RefillInfo.Top
                    'lblDenialReasoncode.Visible = True
                    'pnlProcessRefill.Height = intRefillpanelheight
                    'pnl_RefillInfo.Visible = False
                    'cmbDenialReasonCode.Visible = True
                    'pnlProcessRefill.Visible = True
                    'pnlProcessRefill.BringToFront()
                    'lblMedicationItemName.Text = strMedicationName
                    'txtNotes.Text = ""
                    'cmbDenialReasonCode.Text = ""

            End Select
        Catch ex As PrescriptionException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oRefillRequest) Then
                oRefillRequest.Dispose()
                oRefillRequest = Nothing
            End If
        End Try
    End Sub

    Private Function GetDEASchedule(ByVal NDCCode As String) As Int16?
        Dim returnedDEASchedule As Int16? = Nothing
        Dim DrugRow As DataRow = Nothing

        Try
            Using dbLayer As New gloEMRGeneralLibrary.PrescriptionBusinessLayer()
                DrugRow = dbLayer.GetDrugDetailsByID(0, NDCCode)
            End Using

            If DrugRow IsNot Nothing Then
                returnedDEASchedule = Convert.ToInt16(DrugRow("IsNarcotics"))
            Else
                Dim nMPID As Int32 = 0
                Using oDIBHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                    nMPID = oDIBHelper.GetMarketedProductId(NDCCode)
                End Using

                Using dbLayer As New gloEMRGeneralLibrary.PrescriptionBusinessLayer()
                    DrugRow = dbLayer.GetDrugDetailsByID(nMPID, NDCCode)
                End Using

                If DrugRow IsNot Nothing Then
                    returnedDEASchedule = Convert.ToInt16(DrugRow("IsNarcotics"))
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription,
                                                     gloAuditTrail.ActivityCategory.RefillRequest,
                                                     gloAuditTrail.ActivityType.General,
                                                     ex.ToString(),
                                                     gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            DrugRow = Nothing
        End Try

        Return returnedDEASchedule
    End Function

    Private Sub Set_EPCSProviderEnabled(ByVal nProviderId As Long)
        Dim dtProvider As DataTable = Nothing
        Try
            oRxBusinessLayer = New RxBusinesslayer(0)
            dtProvider = oRxBusinessLayer.GetPatientProviderDetails(nProviderId)
            If IsNothing(dtProvider) = False Then
                If dtProvider.Rows.Count > 0 Then
                    Dim strServiceLevel As String = ""
                    strServiceLevel = Convert.ToString(dtProvider.Rows(0)("sServiceLevel"))

                    If gblnEpcsEnabled = True Then  ''Is EPCS Enabled for clinic
                        ''To check Provider is EPCS enabled
                        If strServiceLevel <> "" Then
                            If Mid(strServiceLevel, 5, 1) = 1 Then
                                gbIsProviderEPCSEnable = True
                            Else
                                gbIsProviderEPCSEnable = False
                            End If
                        Else
                            gbIsProviderEPCSEnable = False
                        End If
                    Else
                        gbIsProviderEPCSEnable = False
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If IsNothing(dtProvider) = False Then
                dtProvider.Dispose()
                dtProvider = Nothing
            End If
        End Try
    End Sub
    Private Function ValidateEPCSData(ByRef oRefRequest As gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest) As Boolean
        Dim blnIsValid As Boolean = True
        Dim PrescriberMessageEPCSeRx As String = ""
        Dim PatientMessageEPCSeRx As String = ""
        Dim sMessage As String = ""
        Try
            If Not IsNothing(oRefRequest) Then

                '''''''''''''''''Prescriber Validation------------------------------------------------------------------
                If Not IsNothing(oRefRequest.ProviderLastName) Then
                    If oRefRequest.ProviderLastName.Trim.Length = 0 Then
                        PrescriberMessageEPCSeRx = "LastName,"
                    End If
                End If

                If Not IsNothing(oRefRequest.ProviderFirstName) Then
                    If oRefRequest.ProviderFirstName.Trim.Length = 0 Then
                        PrescriberMessageEPCSeRx = PrescriberMessageEPCSeRx & "FirstName,"
                    End If
                End If

                'If Not IsNothing(oRefRequest.pro) Then
                '    If oRefRequest.RxPrescriber.PrescriberDEA.Trim.Length = 0 Then
                '        PrescriberMessageEPCSeRx = PrescriberMessageEPCSeRx & "PrescriberDEA"
                '    End If
                'End If

                If PrescriberMessageEPCSeRx.Length > 0 Then
                    PrescriberMessageEPCSeRx = "Provider’s " & PrescriberMessageEPCSeRx & ""
                End If

                '''''''''Patient Validation------------------------------------------------------------------------------------------------- 
                'If Not IsNothing(oRefRequest.PatientName) Then
                '    If oRefRequest.RxPatient.PatientName.LastName.Trim.Length = 0 Then
                '        PatientMessageEPCSeRx = "LastName,"
                '    End If
                'End If

                'If Not IsNothing(oRefRequest.RxPatient.PatientName.FirstName) Then
                '    If oRefRequest.RxPatient.PatientName.FirstName.Trim.Length = 0 Then
                '        PatientMessageEPCSeRx = PatientMessageEPCSeRx & "FirstName,"
                '    End If
                'End If

                If Not IsNothing(oRefRequest.PatientAddress) Then
                    If oRefRequest.PatientAddress.Trim.Length = 0 Then
                        PatientMessageEPCSeRx = PatientMessageEPCSeRx & "Address Line1,"
                    End If
                End If

                If Not IsNothing(oRefRequest.PatientCity) Then
                    If oRefRequest.PatientCity.Trim.Length = 0 Then
                        PatientMessageEPCSeRx = PatientMessageEPCSeRx & "City,"
                    End If
                End If
                If Not IsNothing(oRefRequest.PatientState) Then
                    If oRefRequest.PatientState.Trim.Length = 0 Then
                        PatientMessageEPCSeRx = PatientMessageEPCSeRx & "State,"
                    End If
                End If

                If Not IsNothing(oRefRequest.PatientZip) Then
                    If oRefRequest.PatientZip.Trim.Length = 0 Then
                        PatientMessageEPCSeRx = PatientMessageEPCSeRx & "Zip,"
                    End If
                End If

                If PatientMessageEPCSeRx.Length > 0 Then
                    PatientMessageEPCSeRx = "Patient's " & PatientMessageEPCSeRx
                End If
                sMessage = PrescriberMessageEPCSeRx & PatientMessageEPCSeRx
                If sMessage.Trim.Length > 0 Then
                    sMessage = sMessage.Substring(0, sMessage.Length - 1)
                    System.Windows.Forms.MessageBox.Show("This Prescription refill cannot be approved because the following data is missing.  " & vbCrLf & sMessage.ToString & "." & vbCrLf & "Please deny Refill Request.", "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                    blnIsValid = False
                End If

            End If

            Return blnIsValid
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        End Try
    End Function
    Private Function AttemptPatientIDBkp(ByVal strPatFirstName As String, ByVal strPatLastName As String, ByVal strPharmacyNCPDPID As String, ByVal strProviderId As Long, ByVal strproviderName As String) As Long

        'Dim _result As DialogResult

        ''_result = MessageBox.Show(
        ''    "The patient specified in the refill request is not listed in the patient database." & vbCrLf & vbCrLf & _
        ''    "Click Yes to register a new patient " & vbCrLf & _
        ''    "Click No to Match with an existing paitent list", "gloEMR", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)

        '_result = Windows.Forms.DialogResult.No
        'Dim _registeredPatID As String = 0

        'If _result = Windows.Forms.DialogResult.Yes Then

        '    Dim _ConnectStr As String = GetConnectionString()
        '    Using patReg As New gloPatient.frmSetupQuickPatient(strPatFirstName, strPatLastName, CType(strPatientDOB, DateTime), strPatientGender, strPatientAddress1, strPatientAddress2, strPatientPhone, strPatientCity, strPatientState, strPatientZip, strPharmacyNCPDPID, _ConnectStr, strProviderId, strPatientFax, strPatientMiddleName, True)
        '        patReg.ShowDialog(IIf(IsNothing(patReg.Parent), Me, patReg.Parent))
        '        _registeredPatID = patReg.ReturnPatientID
        '    End Using

        'ElseIf _result = Windows.Forms.DialogResult.No Then
        '    Using oFrmMapUnMatchPatients As New gloPatient.frmMapUnMatchPatients(strPatFirstName, strPatLastName, CType(strPatientDOB, DateTime), strPatientGender, strPatientAddress1, strPatientAddress2, strPatientPhone, strPatientCity, strPatientState, strPatientZip, strPharmacyNCPDPID, strProviderId, strPatientFax, strPatientMiddleName, strproviderName)
        '        oFrmMapUnMatchPatients.ShowDialog(IIf(IsNothing(oFrmMapUnMatchPatients.Parent), Me, oFrmMapUnMatchPatients.Parent))
        '        If oFrmMapUnMatchPatients.CurrentAction = gloPatient.frmMapUnMatchPatients.FormAction.MatchPatient Then
        '            _registeredPatID = oFrmMapUnMatchPatients.SelectedPatientId
        '        ElseIf oFrmMapUnMatchPatients.CurrentAction = gloPatient.frmMapUnMatchPatients.FormAction.NewPatient Then
        '            Dim _ConnectStr As String = GetConnectionString()
        '            Using patReg As New gloPatient.frmSetupQuickPatient(strPatFirstName, strPatLastName, CType(strPatientDOB, DateTime), strPatientGender, strPatientAddress1, strPatientAddress2, strPatientPhone, strPatientCity, strPatientState, strPatientZip, strPharmacyNCPDPID, _ConnectStr, strProviderId, strPatientFax, strPatientMiddleName, True)
        '                patReg.ShowDialog(IIf(IsNothing(patReg.Parent), Me, patReg.Parent))
        '                _registeredPatID = patReg.ReturnPatientID
        '            End Using
        '        ElseIf oFrmMapUnMatchPatients.CurrentAction = gloPatient.frmMapUnMatchPatients.FormAction.DenyRequest Then
        '            Call DenySelectedRefillRequest()
        '        ElseIf oFrmMapUnMatchPatients.CurrentAction = gloPatient.frmMapUnMatchPatients.FormAction.Cancel Then
        '            _registeredPatID = 0
        '        End If
        '        'If oFrmMapUnMatchPatients.DialogResult = Windows.Forms.DialogResult.OK Then
        '        '    _registeredPatID = oFrmMapUnMatchPatients.SelectedPatientId
        '        'Else
        '        '    _registeredPatID = 0
        '        'End If
        '    End Using
        'Else
        '    _registeredPatID = 0
        'End If

        'Return _registeredPatID
        Return Nothing

    End Function

    Private Function AttemptPatientID(ByVal strPatFirstName As String, ByVal strPatLastName As String, ByVal strPharmacyNCPDPID As String, ByVal strProviderId As Long, ByVal strproviderName As String) As Long

        Dim _registeredPatID As String = 0
        Dim nIndex As Integer = C1RefillList.RowSel
        Dim strDrugQty As String = ConvertIfNothingToEmptyString(C1RefillList.GetData(nIndex, COL_Quantity))
        Dim dtRecived As DateTime = If(IsNothing(C1RefillList.GetData(nIndex, COL_dtWrittenDate)), Nothing, C1RefillList.GetData(nIndex, COL_dtWrittenDate))
        Dim strRefillQty As String = ConvertIfNothingToEmptyString(C1RefillList.GetData(nIndex, COL_RefillQuantity))
        Using oFrmMapUnMatchPatients As New gloPatient.frmMapUnMatchPatients(strPatFirstName, strPatLastName, CType(strPatientDOB, DateTime), strPatientGender, strPatientAddress1, strPatientAddress2, strPatientPhone, strPatientCity, strPatientState, strPatientZip, strPharmacyNCPDPID, strProviderId, strPatientFax, strPatientMiddleName, strproviderName, strMedicationName, strDrugQty, dtRecived, strRefillQty)
            '' 1. Show MatchPatient Screen
            oFrmMapUnMatchPatients.ShowDialog(IIf(IsNothing(oFrmMapUnMatchPatients.Parent), Me, oFrmMapUnMatchPatients.Parent))
            If oFrmMapUnMatchPatients.CurrentAction = gloPatient.frmMapUnMatchPatients.FormAction.MatchPatient Then
                _registeredPatID = oFrmMapUnMatchPatients.SelectedPatientId
            ElseIf oFrmMapUnMatchPatients.CurrentAction = gloPatient.frmMapUnMatchPatients.FormAction.NewPatient Then
                Dim _ConnectStr As String = GetConnectionString()
                Using patReg As New gloPatient.frmSetupQuickPatient(strPatFirstName, strPatLastName, CType(strPatientDOB, DateTime), strPatientGender, strPatientAddress1, strPatientAddress2, strPatientPhone, strPatientCity, strPatientState, strPatientZip, strPharmacyNCPDPID, _ConnectStr, strProviderId, strPatientFax, strPatientMiddleName, True)
                    patReg.ShowDialog(IIf(IsNothing(patReg.Parent), Me, patReg.Parent))
                    _registeredPatID = patReg.ReturnPatientID
                End Using
            ElseIf oFrmMapUnMatchPatients.CurrentAction = gloPatient.frmMapUnMatchPatients.FormAction.DenyRequest Then
                Call DenySelectedRefillRequest()
            ElseIf oFrmMapUnMatchPatients.CurrentAction = gloPatient.frmMapUnMatchPatients.FormAction.Cancel Then
                _registeredPatID = 0
            End If
        End Using

        Return _registeredPatID

    End Function

    Private Function CompareMiddleName(ByVal SureScriptMiddleName As String, ByVal DatabaseMiddleName As String) As Boolean
        Dim DemographicChange As Boolean = False
        Try

            Select Case DatabaseMiddleName.Length
                Case 2 ''has exactly two characters
                    Select Case SureScriptMiddleName.Length
                        Case 2
                            If SureScriptMiddleName <> DatabaseMiddleName Then
                                DemographicChange = True
                            End If
                        Case 1
                            If SureScriptMiddleName <> DatabaseMiddleName.Substring(0, SureScriptMiddleName.Length) Then
                                DemographicChange = True
                            End If
                        Case Else
                            If SureScriptMiddleName.Substring(0, DatabaseMiddleName.Length) <> DatabaseMiddleName Then
                                DemographicChange = True
                            End If
                    End Select

                Case 1 ''has less than two characters

                    Select Case SureScriptMiddleName.Length
                        Case 2
                            If SureScriptMiddleName.Substring(0, DatabaseMiddleName.Length) <> DatabaseMiddleName Then
                                DemographicChange = True
                            End If
                        Case 1
                            If SureScriptMiddleName <> DatabaseMiddleName Then
                                DemographicChange = True
                            End If
                        Case Else
                            If SureScriptMiddleName.Substring(0, DatabaseMiddleName.Length) <> DatabaseMiddleName Then
                                DemographicChange = True
                            End If
                    End Select

                Case Else ''has maximum characters

                    Select Case SureScriptMiddleName.Length
                        Case 2
                            If SureScriptMiddleName <> DatabaseMiddleName.Substring(0, 2) Then
                                DemographicChange = True
                            End If
                        Case 1
                            If SureScriptMiddleName <> DatabaseMiddleName.Substring(0, SureScriptMiddleName.Length) Then
                                DemographicChange = True
                            End If
                        Case Else
                            If SureScriptMiddleName <> DatabaseMiddleName Then
                                DemographicChange = True
                            End If
                    End Select

            End Select

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return DemographicChange
    End Function

    Private Sub showSearchUserControl(Optional ByVal sDrugName As String = "")
        Dim dt As DataTable
        Dim _gloEMRDatabase As New DataBaseLayer
        Try

            If Not IsNothing(objDrugControl) Then
                If Panel1.Controls.Contains(objDrugControl) Then
                    Panel1.Controls.Remove(objDrugControl)
                End If
                objDrugControl.Dispose()
                objDrugControl = Nothing
            End If

            Dim strGetDrugs As String

            If sDrugName = "" Then
                strGetDrugs = "select d.nDrugsID as DrugID,isnull(d.sDrugName,'') as DrugName,isnull(d.sDosage,'') as Dosage," _
                                            & " isnull(d.sDrugForm,'')as DosageForm, isnull(d.sNDCCode,'') as NDCCode from drugs_mst d"
            Else
                strGetDrugs = "select d.nDrugsID as DrugID,isnull(d.sDrugName,'') as DrugName,isnull(d.sDosage,'') as Dosage," _
                                            & " isnull(d.sDrugForm,'')as DosageForm, isnull(d.sNDCCode,'') as NDCCode from drugs_mst d where d.sDrugName like '" & "%" & sDrugName & "%'"
            End If


            dt = _gloEMRDatabase.GetDataTable_Query(strGetDrugs)
            If IsNothing(dt) = False Then
                If dt.Rows.Count = 0 Then

                    If Not IsNothing(dt) Then
                        dt.Dispose()
                        dt = Nothing
                    End If

                    strGetDrugs = "select d.nDrugsID as DrugID,isnull(d.sDrugName,'') as DrugName,isnull(d.sDosage,'') as Dosage," _
                                         & " isnull(d.sDrugForm,'')as DosageForm, isnull(d.sNDCCode,'') as NDCCode from drugs_mst d"

                    dt = _gloEMRDatabase.GetDataTable_Query(strGetDrugs)

                    If sDrugName <> "" Then
                        lblSupplyText.Visible = True
                        lblSupplyText.Text = "No Alternatives were found on plain text comparison as there was no NDC code sent with refill request.Please select from List"
                    End If

                    If IsNothing(dt) = False Then
                        objDrugControl = New gloUC_CustomSearchInC1Flexgrid(dt, False)
                    End If
                Else

                    If sDrugName <> "" Then
                        lblSupplyText.Visible = True
                        lblSupplyText.Text = "Alternatives are based on plain text comparison as there was no NDC code sent with refill request"
                    End If

                    objDrugControl = New gloUC_CustomSearchInC1Flexgrid(dt, False)
                End If
            End If



            If Not IsNothing(objDrugControl) Then
                'objDrugControl = Nothing
                'For i As Int16 = pnl_RefillInfo.Controls.Count - 1 To i = 0
                '    Me.pnl_RefillInfo.Controls.RemoveAt(i)
                'Next

                Panel1.Controls.Add(objDrugControl)
                objDrugControl.Dock = DockStyle.Fill
                objDrugControl.BringToFront()
                objDrugControl.Visible = True

                pnl_Grid.Height = 0
                pnl_RefillInfo.Height = 0

            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If (IsNothing(_gloEMRDatabase) = False) Then
                _gloEMRDatabase.Dispose()
                _gloEMRDatabase = Nothing
            End If
        End Try
    End Sub

    Public Function getPatientId(ByVal StrSQl As String) As Int64
        Dim _ID As Int64 = 0

        Dim _gloEMRDBID As New DataBaseLayer
        _ID = _gloEMRDBID.GetRecord_Query(StrSQl)
        _gloEMRDBID.Dispose()
        _gloEMRDBID = Nothing
        Return _ID


    End Function



    Private Function SplitPatientName(ByVal PatientName As String) As Array
        Try
            Dim _result As String()
            _result = PatientName.Split(" ")
            Return _result
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try

    End Function

    Private Function SplitPatientName_WithPipe(ByVal PatientName As String) As Array
        Try
            Dim _result As String()
            _result = PatientName.Split("|")
            Return _result
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try

    End Function

    Private Sub trvPrescribers_AfterSelect(sender As Object, e As System.Windows.Forms.TreeViewEventArgs) Handles trvPrescribers.AfterSelect
        Try
            nPatientId_RefillReq = 0
            strMedicationName = ""
            nRxTransactionId = ""
            strQuantity = ""
            sRxReferenceNumber = ""
            sMessageID = ""
            sPharmacyID = ""
            txtSearch.Text = ""
            '\\GetPendingRefillRequest()
            GetPendingRefillRequestFORC1()
            RemoveDrugGridControl()

            ShowCntrlAsPerResolution()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvPrescribers_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles trvPrescribers.Click
        'nPatientId_RefillReq = 0
        'strMedicationName = ""
        'nRxTransactionId = 0
        'strQuantity = ""
        'sRxReferenceNumber = ""
        'Dim dtPendingdetails As DataTable
        'Dim oRefillRequest As RefillRequest
        'Try

        '    dtPendingdetails = New DataTable
        '    oRefillRequest = New RefillRequest
        '    Dim mynode As TreeNode
        '    mynode = CType(trvPrescribers.SelectedNode, TreeNode)

        '    If IsNothing(trvPrescribers.SelectedNode) = False Then
        '        'validation if the selected node is not rootnode
        '        If mynode.Text = "Prescribers" Then
        '            dgRefillList.DataSource = Nothing

        '        Else 'root node is not selected

        '            'pass the appropriate provider id that is stored in the tag value to the function
        '            dtPendingdetails = oRefillRequest.GetAllPendingRefills(trvPrescribers.SelectedNode.Tag)

        '            If Not IsNothing(dtPendingdetails) Then

        '                dgRefillList.DataSource = dtPendingdetails

        '            Else ' there are no pending refill request populated in the datatable
        '                dgRefillList.DataSource = Nothing
        '            End If
        '        End If

        '    Else
        '        dgRefillList.DataSource = Nothing
        '    End If

        Try
            'pnl_RefillInfo.BringToFront()
            nPatientId_RefillReq = 0
            strMedicationName = ""
            nRxTransactionId = ""
            strQuantity = ""
            sRxReferenceNumber = ""
            sMessageID = ""
            sPharmacyID = ""
            txtSearch.Text = ""
            '\\GetPendingRefillRequest()
            GetPendingRefillRequestFORC1()
            RemoveDrugGridControl()

            ShowCntrlAsPerResolution()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    ''' <summary>
    ''' fill the datagrid with pending refill request against that provider
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub trvPrescribers_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trvPrescribers.DoubleClick


        'Try
        '    nPatientId_RefillReq = 0
        '    strMedicationName = ""
        '    nRxTransactionId = ""
        '    strQuantity = ""
        '    sRxReferenceNumber = ""
        '    sMessageID = ""
        '    sPharmacyID = ""
        '    txtSearch.Text = ""

        '    '\\GetPendingRefillRequest()
        '    GetPendingRefillRequestFORC1()
        'Catch ex As Exception
        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        '    MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)

        'End Try

    End Sub
    ''dgrid ''NOT in USED
    Public Function GetPendingRefillRequest()
        nPatientId_RefillReq = 0
        strMedicationName = ""
        nRxTransactionId = ""
        strQuantity = ""
        sRxReferenceNumber = ""
        sMessageID = ""
        sPharmacyID = ""
        Dim dtPendingDetails As DataTable
        Dim oRefillRequest As New gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest
        Try
            RemoveDrugGridControl()
            'refresh the Popup grid of dashboard so that if there are new items then those will be shown in the dgRefillList grid also.
            CType(Me.MdiParent, MainMenu).FillMessage()


            'dtPendingDetails = New DataTable

            Dim mynode As TreeNode
            mynode = CType(trvPrescribers.SelectedNode, TreeNode)

            If IsNothing(trvPrescribers.SelectedNode) = False Then
                'validation if the selected node is not rootnode
                If mynode.Text = "Prescribers" Then

                    dgRefillList.DataSource = Nothing
                    pnl_RefillInfo.Height = 0
                    btnupdown.BackgroundImage = Global.gloEMR.My.Resources.Down
                    btnupdown.BackgroundImageLayout = ImageLayout.Center
                Else 'root node is not selected

                    'pass the appropriate provider id that is stored in the tag value to the function
                    dtPendingDetails = oRefillRequest.GetAllPendingRefills(trvPrescribers.SelectedNode.Tag)

                    If Not IsNothing(dtPendingDetails) Then

                        If dtPendingDetails.Rows.Count > 0 Then
                            dgRefillList.DataSource = dtPendingDetails
                            pnl_RefillInfo.Height = intRefillpanelheight
                            blnbtnstatus = True
                            btnupdown.BackgroundImage = Global.gloEMR.My.Resources.UP
                            btnupdown.BackgroundImageLayout = ImageLayout.Center
                            setDataGridStyle(dtPendingDetails)
                            dgRefillList.Select(0)
                            showDetails()
                        Else
                            RefreshRefillDetails()
                            pnl_RefillInfo.Height = 0
                            btnupdown.BackgroundImage = Global.gloEMR.My.Resources.Down
                            btnupdown.BackgroundImageLayout = ImageLayout.Center
                        End If

                    Else ' there are no pending refill request populated in the datatable
                        dgRefillList.DataSource = Nothing
                        RefreshRefillDetails()
                        pnl_RefillInfo.Height = 0
                        btnupdown.BackgroundImage = Global.gloEMR.My.Resources.Down
                        btnupdown.BackgroundImageLayout = ImageLayout.Center
                    End If
                End If

            Else
                dgRefillList.DataSource = Nothing
                pnl_RefillInfo.Height = 0
                btnupdown.BackgroundImage = Global.gloEMR.My.Resources.Down
                btnupdown.BackgroundImageLayout = ImageLayout.Center
            End If
            If pnlProcessRefill.Visible = True Then
                pnlProcessRefill.Visible = False
                pnlProcessRefill.SendToBack()
            End If
        Catch ex As PrescriptionException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Throw ex
        Finally
            If Not IsNothing(oRefillRequest) Then
                oRefillRequest.Dispose()
                oRefillRequest = Nothing
            End If
        End Try
        Return Nothing
    End Function


    Private Sub trvPrescribers_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvPrescribers.MouseDown

        'Dim trvNode As TreeNode
        'Try
        '    nPatientId_RefillReq = 0
        '    strMedicationName = ""
        '    nRxTransactionId = ""
        '    strQuantity = ""
        '    sRxReferenceNumber = ""
        '    sMessageID = ""
        '    sPharmacyID = ""
        '    txtSearch.Text = ""

        '    trvNode = CType(trvPrescribers.GetNodeAt(e.X, e.Y), TreeNode)
        '    If IsNothing(trvNode) = False Then
        '        trvPrescribers.SelectedNode = trvNode
        '    End If
        '    '\\GetPendingRefillRequest()
        '    GetPendingRefillRequestFORC1()
        'Catch ex As Exception
        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        '    MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub frmRxRequest_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If Not IsNothing(objDrugControl) Then
            If Panel1.Controls.Contains(objDrugControl) Then
                Panel1.Controls.Remove(objDrugControl)
            End If
            objDrugControl.Dispose()
            objDrugControl = Nothing
        End If
        If (IsNothing(Dv_Search) = False) Then
            Dv_Search.Dispose()
            Dv_Search = Nothing
        End If

        If (IsNothing(dvNext) = False) Then
            dvNext.Dispose()
            dvNext = Nothing
        End If
        If (IsNothing(C1_DataTable) = False) Then
            C1_DataTable.Dispose()
            C1_DataTable = Nothing
        End If


        Try
            'Application.DoEvents()
            Me.Dispose()
        Catch exdispose As Exception

        End Try
    End Sub

    ''' <summary>
    ''' search on patient and medication
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmRxRequest_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        gloC1FlexStyle.Style(C1RefillList)

        Dim oRefillRequest As gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest = Nothing
        Dim DBLayer As New PrescriptionBusinessLayer()

        Try
            globalSecurity.gstrDatabaseName = gstrDatabaseName
            globalSecurity.gstrSQLServerName = gstrSQLServerName
            globalSecurity.gstrSQLUserEMR = gstrSQLUserEMR
            globalSecurity.gstrSQLPasswordEMR = gstrSQLPasswordEMR
            globalSecurity.gblnSQLAuthentication = gblnSQLAuthentication

            clsgeneral.gblnIsStagingServer = gblnStagingServer
            clsgeneral.StartUpPath = System.Windows.Forms.Application.StartupPath
            cntListmenuStrip.Items.Clear()

            pnl_MedicalDispanced.Visible = False
            Panel12.Visible = False
            pnl_MedicalPrescribe.Visible = False
            Panel14.Visible = False


            'call the AddMenu function only when there is rows(data present) in the dgRefillList datagrid
            AddMenu()

            If gblnSQLAuthentication = True Then '''' this is used in gloSurescriptGeneral.GetconnectionString()
                gloSureScript.gloSurescriptGeneral.gblnIsSQLAuthentication = True
            End If

            oRefillRequest = New gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest
            Dim dt As DataTable = DBLayer.GetPrescriberList
            trvPrescribers.Nodes.Clear()
            trvPrescribers.Nodes.Add("Prescribers")
            trvPrescribers.Nodes.Item(0).ImageIndex = 3
            trvPrescribers.Nodes.Item(0).SelectedImageIndex = 3

            'show the lable name with the coloumn name to search
            lblSearch.Text = "Search :"

            SetClgrid()

            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    Dim mynode As TreeNode

                    For icnt As Int32 = 0 To dt.Rows.Count - 1
                        mynode = New TreeNode
                        mynode.Tag = dt.Rows(icnt)(0)
                        mynode.Text = dt.Rows(icnt)(1)
                        mynode.ImageIndex = 6
                        mynode.SelectedImageIndex = 6
                        trvPrescribers.Nodes.Item(0).Nodes.Add(mynode)
                    Next

                    If _PrescriberId <> "" Then

                        For i As Int16 = 0 To trvPrescribers.Nodes(0).GetNodeCount(True) - 1
                            If _PrescriberId = trvPrescribers.Nodes(0).Nodes(i).Tag Then
                                trvPrescribers.SelectedNode = trvPrescribers.Nodes.Item(0).Nodes.Item(i)
                                Exit For
                            End If
                        Next
                        If IsNothing(trvPrescribers.SelectedNode) Then
                            trvPrescribers.SelectedNode = trvPrescribers.Nodes.Item(0).Nodes.Item(0)
                            _PrescriberId = trvPrescribers.SelectedNode.Tag
                            trvPrescribers.ExpandAll()
                        End If
                        dt.Dispose()
                        dt = Nothing
                        dt = oRefillRequest.GetAllPendingRefills(_PrescriberId)
                        If Not IsNothing(dt) Then

                            If dt.Rows.Count > 0 Then
                                ''pnl_RefillInfo.Height = intRefillpanelheight
                                spitpanelprocessrefill()
                                blnbtnstatus = True
                                btnupdown.BackgroundImage = Global.gloEMR.My.Resources.Down   '' ImgUpButton
                                btnupdown.BackgroundImageLayout = ImageLayout.Center
                                'take a dataview because we can apply searching and sorting on dataview instead of datatable
                                Dim dv As DataView

                                'convert the datatable to default view
                                dv = dt.DefaultView

                                'binf the dataview instead of datatable to the datagrid
                                '\dgRefillList.DataSource = dv

                                'for setting the grid style pass the datatable
                                '\setDataGridStyle(dt)
                                '\dgRefillList.Select(0)
                                '\showDetails()

                                SetClgrid()
                                setdatatoC1(dt)
                                showDetailsC1()

                                'Dim csComboList As C1.Win.C1FlexGrid.CellStyle = C1RefillList.Styles.Add("CS_ComboList")

                                'With csComboList
                                '    .Font = New Font(Font, FontStyle.Regular)
                                '    .ForeColor = Color.Black
                                '    .BackColor = Color.GhostWhite
                                '    .DataType = GetType(String)
                                '    .ComboList = "..."
                                'End With
                                'C1RefillList.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always

                                'For i As Integer = 0 To dt.Rows.Count - 1
                                '    With C1RefillList
                                '        .Rows.Add()
                                '        .SetCellStyle(i + 1, COL_Approve, .Styles("CS_ComboList"))
                                '        .SetCellStyle(i + 1, COL_Deny, .Styles("CS_ComboList"))
                                '        .SetCellStyle(i + 1, COL_Cancel, .Styles("CS_ComboList"))

                                '        .SetData(i + 1, COL_Approve, "")
                                '        .SetData(i + 1, COL_Deny, "")
                                '        .SetData(i + 1, COL_Cancel, "")

                                '        .SetData(i + 1, COL_RxReferenceNumber, dt.Rows(i)(0))
                                '        .SetData(i + 1, COL_RxTransactionId, dt.Rows(i)(1))
                                '        .SetData(i + 1, COL_PatientName, dt.Rows(i)(2))
                                '        .SetData(i + 1, COL_PatientGender, dt.Rows(i)(3)) ''this is called as Medication but in image it is DrugName
                                '        .SetData(i + 1, COL_PatientDOB, dt.Rows(i)(4))
                                '        .SetData(i + 1, COL_Medication, dt.Rows(i)(5))
                                '        .SetData(i + 1, COL_Quantity, dt.Rows(i)(6))
                                '        .SetData(i + 1, COL_PrescriptionDate, dt.Rows(i)(7))
                                '        .SetData(i + 1, COL_DateReceived, dt.Rows(i)(8))
                                '        .SetData(i + 1, COL_RefillQuantity, dt.Rows(i)(9))
                                '        .SetData(i + 1, COL_PatientId, dt.Rows(i)(10)) ''this is called as Amount but in image it is Dispense 
                                '        .SetData(i + 1, COL_LastFillDate, dt.Rows(i)(11))
                                '        .SetData(i + 1, COL_RefillQualifier, dt.Rows(i)(12))
                                '        .SetData(i + 1, COL_MessageID, dt.Rows(i)(13))
                                '        .SetData(i + 1, COL_PharmacyID, dt.Rows(i)(14))
                                '        .SetData(i + 1, COL_PatientLastName, dt.Rows(i)(15))
                                '        '.SetData(i + 1, COL_Alt_Select, "...")
                                '    End With

                                'Next
                            Else
                                Onepanelprocessrefill()
                                blnbtnstatus = False
                                btnupdown.BackgroundImage = Global.gloEMR.My.Resources.Down '' ImgUpButton
                                btnupdown.BackgroundImageLayout = ImageLayout.Center
                                Dim dv As DataView

                                'convert the datatable to default view
                                dv = dt.DefaultView

                                SetClgrid()
                                setdatatoC1(dt)
                                showDetailsC1()

                            End If
                        End If

                    Else

                        trvPrescribers.SelectedNode = trvPrescribers.Nodes.Item(0).Nodes.Item(0)
                        If (IsNothing(dt) = False) Then
                            dt.Dispose()
                            dt = Nothing

                        End If
                        dt = oRefillRequest.GetAllPendingRefills(trvPrescribers.SelectedNode.Tag)
                        If Not IsNothing(dt) Then

                            If dt.Rows.Count > 0 Then
                                ''pnl_RefillInfo.Height = intRefillpanelheight
                                ''blnbtnstatus = True
                                Onepanelprocessrefill()
                                blnbtnstatus = False


                                btnupdown.BackgroundImage = Global.gloEMR.My.Resources.Down '' ImgUpButton
                                btnupdown.BackgroundImageLayout = ImageLayout.Center
                                'take a dataview because we can apply searching and sorting on dataview instead of datatable
                                Dim dv As DataView

                                'convert the datatable to default view
                                dv = dt.DefaultView

                                'bind the dataview instead of datatable to the datagrid
                                '\dgRefillList.DataSource = dv

                                'for setting the grid style pass the datatable
                                '\setDataGridStyle(dt)
                                '\dgRefillList.Select(0)

                                SetClgrid()
                                setdatatoC1(dt)
                                showDetailsC1()
                                '\showDetails()
                            End If
                        End If

                    End If

                End If

            End If

            FillDenials()
            ShowCntrlAsPerResolution(True)
        Catch ex As PrescriptionException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oRefillRequest) Then
                oRefillRequest.Dispose()
                oRefillRequest = Nothing
            End If

            If DBLayer IsNot Nothing Then
                DBLayer.Dispose()
                DBLayer = Nothing
            End If

        End Try
    End Sub

    Private Sub ts_btnDenied_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ts_btnDenied.Click
        Try
            If sMessageID <> "" Then
                'ofrmDenyRefillRequest = New frmDenyRefillRequest(strMedicationName, sRxReferenceNumber, dtdatereceived, nRxTransactionId.ToString)


                ''Since by default the _blnDenyWithNewRx flag is false means dont show the Rx form
                'With ofrmDenyRefillRequest
                '    .IsDenied = True
                '    .Owner = Me
                '    .ShowDialog()
                '    GetPendingRefillRequest()
                'End With

                blnbtnstatus = True
                spitpanelprocessrefill()
                btnupdown.BackgroundImage = Global.gloEMR.My.Resources.Down    ''ImgUpButton
                btnupdown.BackgroundImageLayout = ImageLayout.Center

                'pnlProcessRefill.Top = pnl_RefillInfo.Top
                pnl_Grid.Dock = DockStyle.Fill
                pnl_Grid.BringToFront()
                pnlProcessRefill.Visible = True
                pnlProcessRefill.Dock = DockStyle.Bottom
                pnl_RefillInfo.Visible = False
                'pnlProcessRefill.BringToFront()
                lblDenialReasoncode.Visible = True
                cmbDenialReasonCode.Visible = True
                lblMedicationItemName.Text = strMedicationName
            Else
                MessageBox.Show("Please select a Refill Request", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As PrescriptionException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub
    Private Sub ts_btnAproved_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ts_btnAproved.Click
        'Dim frmRx As frmPrescription
        'Dim oRefillRequest As RefillRequest
        Try
            If sMessageID <> "" Then
                '        If nPatientId_RefillReq <> gnPatientID Then
                '            'call DoctorsDashboard function to set the global parameters to the PatientId_RefillReq
                '            CType(Me.MdiParent, MainMenu).ShowDefaultPatientDetails(nPatientId_RefillReq)
                '        End If
                '        oRefillRequest = New RefillRequest
                '        oRefillRequest.GetPrescriberPharmacyID(sRxReferenceNumber, dtdatereceived)
                '        frmRx = New frmPrescription(nRxTransactionId, strQuantity, sRxReferenceNumber, dtdatereceived, oRefillRequest.ProviderID, oRefillRequest.PharmacyID)
                '        frmRx.ShowInTaskbar = False
                '        frmRx.MdiParent = Me.MdiParent
                '        frmRx.Show()
                Dim tlsender As ToolStripMenuItem = cntListmenuStrip.Items(0)
                StripItem_Click(tlsender, e)
            Else
                MessageBox.Show("Please select a Refill Request", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As PrescriptionException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'If Not IsNothing(oRefillRequest) Then
            '    oRefillRequest.Dispose()
            '    oRefillRequest = Nothing
            'End If
            'If Not IsNothing(frmRx) Then
            '    frmRx.Dispose()
            '    frmRx = Nothing
            'End If
        End Try
    End Sub

    Private Sub ts_btnDeniedWithNewRxtoFollow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ts_btnDeniedWithNewRxtoFollow.Click
        'Dim ofrmDenyRefillRequest As New frmDenyRefillRequest(strMedicationName, sRxReferenceNumber, dtdatereceived, nRxTransactionId.ToString)

        Try
            If sMessageID <> "" Then

                'With ofrmDenyRefillRequest
                '    .IsDenied = False 'this means that open the Rx form when clicked on the OK button
                '    .Owner = Me
                '    .ShowDialog()
                'End With
                pnlProcessRefill.Top = pnl_RefillInfo.Top
                pnlProcessRefill.Visible = True
                pnl_RefillInfo.Visible = False
                pnlProcessRefill.BringToFront()
                lblDenialReasoncode.Visible = False
                cmbDenialReasonCode.Visible = False
                lblMedicationItemName.Text = strMedicationName
            Else
                MessageBox.Show("Select a refill request", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As PrescriptionException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'If Not IsNothing(ofrmDenyRefillRequest) Then
            '    ofrmDenyRefillRequest.Dispose()
            '    ofrmDenyRefillRequest = Nothing
            'End If
        End Try
    End Sub
    Private Sub ts_btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ts_btnRefresh.Click
        Try
            '\\GetPendingRefillRequest()
            GetPendingRefillRequestFORC1()
            '' RefreshRefillDetails()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.Refresh, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            ' ''select the first row of the grid
            ''If (e.KeyChar = ChrW(13)) Then
            ''    If dgRefillList.CurrentRowIndex >= 0 Then
            ''        dgRefillList.Select(0)
            ''        dgRefillList.CurrentRowIndex = 0
            ''    End If
            ''End If
            ''mdlGeneral.ValidateText(txtSearch.Text, e)

            ''----------------------------
            'select the first row of the grid
            If (e.KeyChar = ChrW(13)) Then
                If C1RefillList.RowSel >= 0 Then
                    C1RefillList.Select(1, 1, 1, 1)
                    C1RefillList.RowSel = 0
                End If
            End If

            mdlGeneral.ValidateText(txtSearch.Text, e)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtSearch_SearchFired() Handles txtSearch.SearchFired

        If _blnSearch = True Then
            Try
                Me.Cursor = Cursors.WaitCursor

                ' ------


                Dim oCol As DataColumn

                If IsNothing(Dv_Search) And txtSearch.Text.Trim <> "" Then

                    'C1_DataTable = New DataTable
                    If C1RefillList.Cols.Count > 0 Then
                        oCol = New DataColumn
                        For i As Integer = 3 To C1RefillList.Cols.Count - 1
                            oCol.Caption = C1RefillList.GetData(0, i)
                            oCol.ColumnName = C1RefillList.GetData(0, i)
                            If (C1_DataTable.Columns.Contains(Convert.ToString(C1RefillList.GetData(0, i))) = False) Then
                                C1_DataTable.Columns.Add(C1RefillList.GetData(0, i))
                            End If
                        Next
                    End If

                    Dim oRow As DataRow
                    If C1RefillList.Rows.Count > 1 Then

                        For iRow As Integer = 1 To C1RefillList.Rows.Count - 1

                            oRow = C1_DataTable.NewRow

                            For iCol As Integer = 3 To C1RefillList.Cols.Count - 1
                                oRow(iCol - 3) = C1RefillList.GetData(iRow, iCol)    'first 3 are button column so remove that 
                            Next
                            C1_DataTable.Rows.Add(oRow)

                        Next
                        Dv_Search = C1_DataTable.DefaultView
                    End If
                End If
                ' ------
                ' Dim dvPatient As DataView
                Dim dt As DataTable

                '\\dvPatient = CType(dgRefillList.DataSource, DataView)

                'Dv_Search = CType(C1RefillList.DataSource, DataView)

                If IsNothing(Dv_Search) Then
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
                '\dgRefillList.DataSource = dvPatient
                InstringSearch()
                '  C1RefillList.Refresh()
                'C1RefillList.Clear()
                C1RefillList.DataSource = Nothing
                SetClgrid()
                dt = Dv_Search.ToTable
                setdatatoC1(dt)

                ''Dim strPatientSearchDetails As String
                ''If Trim(txtSearch.Text) <> "" Then
                ''    strPatientSearchDetails = Replace(txtSearch.Text, "'", "''")
                ''    strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                ''    strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
                ''Else
                ''    strPatientSearchDetails = ""
                ''End If

                ''Select Case Trim(lblSearch.Text)
                ''    Case "Patient Name"
                ''        '''''Code Modified by Anil on 24/09/2007 at 3:20 p.m.
                ''        '''''This change is made to get In-String search i.e.,for any string which has the character/characters present in the search textbox.
                ''        '''''Previously the commented code was searching for the strings which are having first character same as that in search textbox 
                ''        '''''and also it was searching strings which are having the character in between the words but for that we had to use " % " or " * " sign before that character. But now it is not required to add % or * signs.

                ''        ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                ''        Dv_Search.RowFilter = Dv_Search.Table.Columns(5).ColumnName & " Like '%" & strPatientSearchDetails & "%'" '2
                ''        ''Else
                ''        ''dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                ''        ''End If
                ''    Case "Medication"
                ''        Dv_Search.RowFilter = Dv_Search.Table.Columns(6).ColumnName & " Like '%" & strPatientSearchDetails & "%'" '3

                ''End Select
                Me.Cursor = Cursors.Default
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)

            End Try
        End If

    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged

    End Sub

    Private Sub ts_btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ts_btnClose.Click
        Try
            ' Me.Close()
            gloWord.WordDialogBoxBackgroundCloser.Close(Me)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Private Sub btnupdown_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnupdown.Click
        Try
            If C1RefillList.Rows.Count > 1 Then
                If blnbtnstatus Then
                    blnbtnstatus = False
                    '' pnl_RefillInfo.Height = pnlRefillinfotitle.Height

                    Onepanelprocessrefill()
                    pnl_RefillInfo.Height = 25
                    btnupdown.BackgroundImage = Global.gloEMR.My.Resources.UP  ''ImgDownButton
                    btnupdown.BackgroundImageLayout = ImageLayout.Center
                Else
                    blnbtnstatus = True
                    ''pnl_RefillInfo.Height = intRefillpanelheight
                    spitpanelprocessrefill()
                    btnupdown.BackgroundImage = Global.gloEMR.My.Resources.Down    ''ImgUpButton
                    btnupdown.BackgroundImageLayout = ImageLayout.Center
                End If

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    'Protected Overrides Sub Finalize()
    '    MyBase.Finalize()
    'End Sub

    '' Deneied Save&Close Click
    Private Sub tlStrpMain_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlStrpMain.ItemClicked
        Dim oRefillRequest As gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest = Nothing
        Try
            Select Case e.ClickedItem.Tag

                Case "Cancel"
                    pnlProcessRefill.Visible = False
                    pnlProcessRefill.SendToBack()
                    pnl_RefillInfo.Visible = True
                    pnlwbBrowser.Visible = True
                    'found the internal issue..
                    'when we click on cancel window will popup for comments..after closing window
                    'refill grid gets invisible.
                    spitpanelprocessrefill()
                Case "OK"

                    oRefillRequest = New gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest
                    oRefillRequest.GetPrescriberPharmacyID(sRxReferenceNumber, dtdatereceived, sMessageID)
                    oRefillRequest.GetDrugDetailsToRefill(sMessageID, sRxReferenceNumber, nRxTransactionId)
                    Set_EPCSProviderEnabled(Convert.ToInt64(oRefillRequest.ProviderID))
                    Select Case eRefillStatus

                        Case RefillStatus.eDeny

                            Dim RefReqNDCCode As String = ""
                            Dim blnRequest10dot6 As Boolean = False
                            'first chek if internet is available and then only send the eRx, if not then exit from the function
                            ' http://www.developerfusion.com/code/3903/is-an-internet-connection-available/
                            _isInternetAvailable = IsInternetConnectionAvailable()
                            If _isInternetAvailable = False Then

                                MessageBox.Show("Internet connection does not exist.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                If Not IsNothing(oRefillRequest) Then
                                    oRefillRequest.Dispose()
                                    oRefillRequest = Nothing
                                End If

                                Exit Sub
                            End If
                            Dim drugtype As Int16 = oRefillRequest.ValidateDrug
                            'If drugtype = -1 Then
                            '    MessageBox.Show(strMedicationName & " is not listed in the drug database.Add the Drug to the Database", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'End If
                            ''--------- Check Patient ID for deneied request 

                            If nPatientId_RefillReq = 0 Then

                                Dim strPatFirstName As String = strPatientName 'retval(0)
                                Dim strPatLastName As String = strPatientLastName 'retval(1)
                                Select Case strPatientGender
                                    Case "M"
                                        strPatientGender = "Male"
                                    Case "Male"
                                        strPatientGender = "Male"
                                    Case "F"
                                        strPatientGender = "Female"
                                    Case "Female"
                                        strPatientGender = "Female"
                                    Case Else
                                        strPatientGender = "Other"
                                End Select
                                'get the patient information with matching First name, Last name, DOB and Gender as parameters
                                Dim _getPatIdSQL As String = "select nPatientID from patient where sFirstName = " & "'" & strPatFirstName & "'" & " and sLastName = " & "'" & strPatLastName & "'" & " and convert(datetime,convert (varchar(50),datepart(mm,dtDOB)) + '/'+ convert(varchar(50),datepart(dd,dtDOB))+'/'+ convert(varchar(50),datepart(yy,dtDOB))) = " & "'" & strPatientDOB & "'" & " and sGender = " & "'" & strPatientGender & "'" & "  "

                                Dim _PatID As String = 0
                                Dim _gloEMRDBID As New DataBaseLayer
                                Try
                                    _PatID = _gloEMRDBID.GetRecord_Query(_getPatIdSQL)

                                    If _PatID = "" Then
                                        nPatientId_RefillReq = 0 ''Make it 0
                                        'If nPatientId_RefillReq = 0 Then
                                        '    MessageBox.Show("The patient specified in the refill request is not listed in the patient database.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        'End If
                                    Else
                                        nPatientId_RefillReq = _PatID
                                    End If
                                Catch ex As Exception
                                Finally
                                    If Not IsNothing(_gloEMRDBID) Then
                                        _gloEMRDBID.Dispose()
                                        _gloEMRDBID = Nothing
                                    End If
                                End Try
                            End If
                            ''------------
                            If C1RefillList.Rows.Count > 1 Then
                                If C1RefillList.GetData(C1RefillList.RowSel, COL_SenderSoftwareVersion) <> "" Then
                                    blnRequest10dot6 = True
                                End If
                                If blnRequest10dot6 = True Then
                                    RefReqNDCCode = C1RefillList.GetData(C1RefillList.RowSel, COL_MDProductCode)
                                Else
                                    RefReqNDCCode = C1RefillList.GetData(C1RefillList.RowSel, COL_RefillReqNDCCode)
                                End If
                            End If
                            If oRefillRequest.GenerateDeniedRefillResponse(cmbDenialReasonCode.Text, txtNotes.Text.Trim, nPatientId_RefillReq, RefReqNDCCode, gbIsProviderEPCSEnable) Then
                                pnlProcessRefill.Visible = False
                                pnlProcessRefill.SendToBack()
                                '\GetPendingRefillRequest()
                                GetPendingRefillRequestFORC1()
                            Else
                                MessageBox.Show("The refill response was not posted successfully", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If

                    End Select
                    pnlProcessRefill.Visible = False
                    pnlProcessRefill.SendToBack()
                    pnl_RefillInfo.Visible = True
            End Select
        Catch ex As PrescriptionException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            If Not IsNothing(oRefillRequest) Then
                oRefillRequest.Dispose()
                oRefillRequest = Nothing
            End If
        End Try

    End Sub
    Private Sub FillDenials()
        Try
            cmbDenialReasonCode.Items.Add("Patient Unknown to the Prescriber")
            cmbDenialReasonCode.Items.Add("Patient never under Prescriber care")
            cmbDenialReasonCode.Items.Add("Patient no longer under Prescriber care")
            cmbDenialReasonCode.Items.Add("Patient has requested refill too soon")
            cmbDenialReasonCode.Items.Add("Medication never prescribed for the patient")
            cmbDenialReasonCode.Items.Add("Patient should contact Prescriber first")
            cmbDenialReasonCode.Items.Add("Refill not appropriate")
            cmbDenialReasonCode.Items.Add("Patient has picked up prescription")
            cmbDenialReasonCode.Items.Add("Patient has picked up partial fill of prescription")
            cmbDenialReasonCode.Items.Add("Patient has not picked up prescription, drug returned to stock")
            cmbDenialReasonCode.Items.Add("Patient needs appointment")
            cmbDenialReasonCode.Items.Add("Prescriber not associated with this practice or location.")
            cmbDenialReasonCode.Items.Add("No attempt will be made to obtain Prior Authorization.")
            cmbDenialReasonCode.Items.Add("Request already responded to by other means (e.g. phone or fax)")
            cmbDenialReasonCode.SelectedIndex = 0
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Deny Refill Request", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' '' Commented at the time for Instalog Implemantation
    Private Sub objDrugControl__FlexDoubleClick(ByVal sender As Object, ByVal e As System.EventArgs, ByVal _bSelectFlag As Boolean) Handles objDrugControl._FlexDoubleClick
        Dim oRefillRequest As gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest
        Try

            DrugId = 0
            NDCCode = ""
            If objDrugControl._UCflex.Row > 0 Then
                'DrugId = objDrugControl._UCflex.GetData(objDrugControl._UCflex.Row, "DrugID") ''''send NDCCode instead of DrugId
                NDCCode = objDrugControl._UCflex.GetData(objDrugControl._UCflex.Row, "NDCCode") ''''send NDCCode instead of DrugId

                oRefillRequest = New gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest
                'Try
                'code start by nilesh on date 20110121 for case#GLO2010-0007555
                'if nRxTransactionId contains string value then assign it 0 value
                If nRxTransactionId <> "" AndAlso nRxTransactionId <> "0" Then
                    If Not IsNumeric(nRxTransactionId) Then
                        nRxTransactionId = "0"
                    End If
                End If
                'code end by nilesh on date 20110121 for case#GLO2010-0007555

                'If nRxTransactionId <> "" AndAlso nRxTransactionId <> "0" Then
                '    If Not IsNumeric(nRxTransactionId) Then
                '        MessageBox.Show("The prescriber order number " & nRxTransactionId & " for the refill request is invalid.  The request should be denied.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                '        Exit Sub
                '    End If
                '    If Not oRefillRequest.GetOrderID(nRxTransactionId) Then
                '        MessageBox.Show("The prescriber order number " & nRxTransactionId & " for the refill request is invalid.  The request should be denied.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                '        Exit Sub
                '    End If
                'End If
                '        Catch ex As gloDBException
                '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                '    MessageBox.Show("The prescriber order number " & nRxTransactionId & " for the refill request is invalid.  The request should be denied.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                '    Exit Sub
                'Catch ex As PrescriptionException
                '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                '    MessageBox.Show("The prescriber order number " & nRxTransactionId & " for the refill request is invalid.  The request should be denied.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                '    Exit Sub
                'Catch ex As Exception
                '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                '    MessageBox.Show("The prescriber order number " & nRxTransactionId & " for the refill request is invalid.  The request should be denied.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                '    Exit Sub
                'End Try


                '\eRefillStatus = RefillStatus.eApprove
                lblDenialReasoncode.Visible = True
                cmbDenialReasonCode.Visible = True

                oRefillRequest.GetPrescriberPharmacyID(sRxReferenceNumber, dtdatereceived, sMessageID)

                ''Commented on 20091222 for remove frmPatientRegMST <><><><><><><>
                ' ''first check for the condition if the nPatientId_RefillReq  = 0
                ''If nPatientId_RefillReq = 0 Then

                ''    Dim retval As String() = SplitPatientName(strPatientName)
                ''    Dim strPatFirstName As String = retval(0)
                ''    Dim strPatLastName As String = retval(1)
                ''    Select Case strPatientGender

                ''        Case "M"
                ''            strPatientGender = "Male"
                ''        Case "F"
                ''            strPatientGender = "Female"
                ''        Case Else
                ''            strPatientGender = "Other"
                ''    End Select
                ''    'get the patient information with matching First name, Last name, DOB and Gender as parameters
                ''    Dim _getPatIdSQL As String = "select nPatientID from patient where sFirstName = " & "'" & strPatFirstName & "'" & " and sLastName = " & "'" & strPatLastName & "'" & " and convert(datetime,convert (varchar(50),datepart(mm,dtDOB)) + '/'+ convert(varchar(50),datepart(dd,dtDOB))+'/'+ convert(varchar(50),datepart(yy,dtDOB))) = " & "'" & strPatientDOB & "'" & " and sGender = " & "'" & strPatientGender & "'" & "  "

                ''    Dim _PatID As String = 0
                ''    Dim _gloEMRDBID As New DataBaseLayer
                ''    Try
                ''        _PatID = _gloEMRDBID.GetRecord_Query(_getPatIdSQL)

                ''        If _PatID = "" Then
                ''            MessageBox.Show("The patient specified in the refill request is not listed in the patient database.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ''            'invoke the patient registration form and pass the sFirstName, sLastName, strPatientDOB and strPatientGender parameters to the constructor of the patient registration form
                ''            'If strPatientDOB(1) = "/" Then
                ''            'ElseIf strPatientDOB(2) = "/" Then
                ''            'End If
                ''            Dim ofrmPatientRegMst As New frmPatientRegMst(strPatFirstName, strPatLastName, strPatientDOB, strPatientGender)
                ''            ofrmPatientRegMst.ShowDialog()
                ''            Exit Sub
                ''        Else
                ''            nPatientId_RefillReq = CType(_PatID, Int64)
                ''        End If
                ''    Catch ex As Exception
                ''    Finally
                ''        If Not IsNothing(_gloEMRDBID) Then
                ''            _gloEMRDBID.Dispose()
                ''            _gloEMRDBID = Nothing
                ''        End If
                ''    End Try
                ''End If

                '''''''''''Code changed added for using gloPatient DLL use patient registration - 20091222
                'first check for the condition if the nPatientId_RefillReq  = 0
                strPharmacyName = lbl_Pharmacy.Text
                If nPatientId_RefillReq = 0 Then

                    'Case 2297
                    ' Dim retval As String() = SplitPatientName(strPatientLastName)
                    Dim strPatFirstName As String = strPatientName 'retval(0)
                    Dim strPatLastName As String = strPatientLastName 'retval(1)
                    'Case 2297

                    'Commented on 20090828
                    'Dim strPataddr As String = lbl_PatientAddress.Text
                    'Dim strPhone As String = lbl_PatientPhoneNo.Text
                    'Dim strCity As String = lbl_CityName.Text
                    'Dim strState As String = lbl_StateName.Text
                    'Dim strZip As String = lbl_ZIPCode.Text
                    'Dim strPharmacyname As String = lbl_Pharmacy.Text
                    'Dim strPharmacyid As String = oRefillRequest.PharmacyID
                    'Dim strproviderName As String = lbl_Provider.Text
                    'Dim strProviderId As String = oRefillRequest.ProviderID

                    'Addded on 20090828
                    Dim strPataddr As String = ""
                    If Not IsNothing(oRefillRequest.PatientAddress) Then
                        strPataddr = oRefillRequest.PatientAddress
                    End If

                    Dim strPhone As String = ""
                    If Not IsNothing(oRefillRequest.PatientPhone) Then
                        strPhone = oRefillRequest.PatientPhone
                    End If

                    Dim strCity As String = ""
                    If Not IsNothing(oRefillRequest.PatientCity) Then
                        strCity = oRefillRequest.PatientCity
                    End If

                    Dim strState As String = ""
                    If Not IsNothing(oRefillRequest.PatientState) Then
                        strState = oRefillRequest.PatientState
                    End If

                    Dim strZip As String = ""
                    If Not IsNothing(oRefillRequest.PatientZip) Then
                        strZip = oRefillRequest.PatientZip
                    End If

                    Dim strPharmacyname As String = ""
                    If Not IsNothing(oRefillRequest.PharmacyName) Then
                        strPharmacyname = oRefillRequest.PharmacyName

                    End If
                    Dim strPharmacyid As String = ""
                    If Not IsNothing(oRefillRequest.PharmacyID) Then
                        strPharmacyid = oRefillRequest.PharmacyID
                    End If

                    Dim strPharmacyNCPDPID As String = ""
                    If Not IsNothing(oRefillRequest.PharmacyNCPDPID) Then
                        strPharmacyNCPDPID = oRefillRequest.PharmacyNCPDPID ''''''this is the NCPDPID
                    End If

                    Dim strproviderName As String = ""
                    If Not IsNothing(oRefillRequest.ProviderName) Then
                        strproviderName = oRefillRequest.ProviderName
                    End If

                    Dim strProviderId As String = ""
                    If Not IsNothing(oRefillRequest.ProviderID) Then
                        strProviderId = oRefillRequest.ProviderID
                        If strProviderId = "" Then
                            strProviderId = "0"
                        End If
                    Else
                        strProviderId = "0"
                    End If


                    'Case 2297
                    Select Case strPatientGender

                        Case "M"
                            strPatientGender = "Male"
                        Case "Male"
                            strPatientGender = "Male"
                        Case "F"
                            strPatientGender = "Female"
                        Case "Female"
                            strPatientGender = "Female"
                        Case Else
                            strPatientGender = "Other"
                    End Select
                    'Case 2297
                    'get the patient information with matching First name, Last name, DOB and Gender as parameters
                    Dim _getPatIdSQL As String = "select nPatientID from patient where sFirstName = " & "'" & strPatFirstName & "'" & " and sLastName = " & "'" & strPatLastName & "'" & " and convert(datetime,convert (varchar(50),datepart(mm,dtDOB)) + '/'+ convert(varchar(50),datepart(dd,dtDOB))+'/'+ convert(varchar(50),datepart(yy,dtDOB))) = " & "'" & strPatientDOB & "'" & " and sGender = " & "'" & strPatientGender & "'" & "  "


                    Dim _PatID As String = 0
                    Dim _gloEMRDBID As New DataBaseLayer
                    Try

                        _PatID = _gloEMRDBID.GetRecord_Query(_getPatIdSQL)


                        If _PatID = "" Then
                            MessageBox.Show("The patient specified in the refill request is not listed in the patient database.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)

                            Dim _ConnectStr As String = GetConnectionString()
                            Dim patReg As New gloPatient.frmSetupQuickPatient(strPatFirstName, strPatLastName, CType(strPatientDOB, DateTime), strPatientGender, strPataddr, "", strPhone, strCity, strState, strZip, strPharmacyNCPDPID, _ConnectStr, strProviderId, strPatientFax, strPatientMiddleName)
                            patReg.ShowDialog(IIf(IsNothing(patReg.Parent), Me, patReg.Parent))
                            patReg.Dispose()
                            patReg = Nothing
                            '''''''''''''''''Update C1RefillList with the registered patient id for futher use
                            'get the patient information with matching First name, Last name, DOB and Gender as parameters
                            Dim _registeredPatID As String = 0
                            _registeredPatID = _gloEMRDBID.GetRecord_Query(_getPatIdSQL)
                            If _registeredPatID <> "0" Then
                                ''''''''''''''update patient id to C1RefillList
                                If C1RefillList.RowSel > 0 Then
                                    C1RefillList.SetData(C1RefillList.RowSel, COL_PatientId, _registeredPatID)
                                End If
                            End If

                            Exit Sub
                        Else
                            nPatientId_RefillReq = CType(_PatID, Int64)
                        End If
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    Finally
                        If Not IsNothing(_gloEMRDBID) Then
                            _gloEMRDBID.Dispose()
                            _gloEMRDBID = Nothing
                        End If
                    End Try

                End If

                ''''''upto this line code added<><><>
                Dim DrugType As Int16
                If NDCCode <> "" Then
                    DrugType = oRefillRequest.GetDrugType_NDCCode(NDCCode)
                Else
                    DrugType = oRefillRequest.GetDrugType(nRxTransactionId, DrugId)
                End If

                If DrugType = -1 Then

                    MessageBox.Show(strMedicationName & " Drug type could not be found", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                Else
                    If gbIsProviderEPCSEnable = False Then   '' Check If EPCS Enabled or not
                        If DrugType >= 2 Then
                            MessageBox.Show(strMedicationName & " is a schedule " & DrugType & " drug and therefore can not be approved.  Please deny the request", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If
                    End If
                End If
                If nPatientId_RefillReq <> _PatientID Then
                    'call DoctorsDashboard function to set the global parameters to the PatientId_RefillReq
                    CType(Me.MdiParent, MainMenu).ShowDefaultPatientDetails(nPatientId_RefillReq)
                End If

                CheckAndSetPharmacy(oRefillRequest)

                Dim frmRx As frmPrescription
                If nRxTransactionId.Length > 0 Then
                    '''''instead of DrigId send NDCCode
                    Dim refRequest As New RefRequest(nRxTransactionId, strQuantity, sRxReferenceNumber, dtdatereceived, sMessageID, NDCCode, oRefillRequest.ProviderID, oRefillRequest.PharmacyID, nPatientId_RefillReq)
                    frmRx = frmPrescription.GetInstance(refRequest)
                    If IsNothing(frmRx) = True Then
                        Exit Sub
                    End If
                    'frmRx = New frmPrescription(nRxTransactionId, strQuantity, sRxReferenceNumber, dtdatereceived, sMessageID, NDCCode, oRefillRequest.ProviderID, oRefillRequest.PharmacyID, nPatientId_RefillReq)
                Else
                    '''''instead of DrigId send NDCCode
                    Dim refRequest As New RefRequest(0, strQuantity, sRxReferenceNumber, dtdatereceived, sMessageID, NDCCode, oRefillRequest.ProviderID, oRefillRequest.PharmacyID, nPatientId_RefillReq)
                    frmRx = frmPrescription.GetInstance(refRequest)
                    If IsNothing(frmRx) = True Then
                        Exit Sub
                    End If
                    'frmRx = New frmPrescription(0, strQuantity, sRxReferenceNumber, dtdatereceived, sMessageID, NDCCode, oRefillRequest.ProviderID, oRefillRequest.PharmacyID, nPatientId_RefillReq)
                End If

                'code commented to fix issue 14021
                'Code Change for Resolving case no :GLO2011-0011553 i.e 6020 bug - possible to whipe out patient's medication history from refill request screen
                'If frmRx.blncancel = True Then
                '    frmRx.ShowInTaskbar = False
                '    frmRx.MdiParent = Me.MdiParent
                '    frmRx.Setform = Me
                '    frmRx.Show()
                '    RemoveDrugGridControl()
                'End If
                'End of Code Change for Resolving case no :GLO2011-0011553 i.e 6020 bug - possible to whipe out patient's medication history from refill request screen

                'code added to fix issue 14021
                If frmRx.blncancel = True Then
                    frmRx.ShowInTaskbar = False
                    frmRx.MdiParent = Me.MdiParent
                    'new condition added to fix bug 14363
                    If frmRx.GetCurrentPatientID <> nPatientId_RefillReq Then
                        frmRx.Setform = Me
                    Else
                        frmRx.RefillRequest(nPatientId_RefillReq, 0, strQuantity, sRxReferenceNumber, dtdatereceived, sMessageID, NDCCode, oRefillRequest.ProviderID, oRefillRequest.PharmacyID)
                        frmRx.BringToFront()
                        frmRx.WindowState = FormWindowState.Maximized
                    End If

                    frmRx.Show()
                End If
                RemoveDrugGridControl()

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    'code added to do default pharmacy change in 6030
    Private Sub CheckAndSetPharmacy(ByVal oRefillRequest As Object)
        Dim oDataTable As DataTable = Nothing
        Dim dv As DataView = Nothing
        Dim oDB As New DataBaseLayer
        Dim _strSQL As String
        Dim strMessage As String

        Try
            _strSQL = "Select nContactID,sNCPDPID from Patient_DTL where npatientID=" & nPatientId_RefillReq & " and nContactFlag=1 "
            oDataTable = New DataTable
            oDataTable = oDB.GetDataTable_Query(_strSQL)
            If Not IsNothing(oDataTable) Then
                If oDataTable.Rows.Count > 0 Then
                    dv = oDataTable.DefaultView
                    dv.Sort = "sNCPDPID"
                    Dim dRows As DataRowView() = dv.FindRows(oRefillRequest.PharmacyNCPDPID)
                    If dRows.Count = 0 Then
                        strMessage = "Would you like to make " & strPharmacyName & " the default pharmacy for " & strPatientName & " " & strPatientLastName & "?"
                        Dim dialogResult As DialogResult
                        dialogResult = System.Windows.Forms.MessageBox.Show(strMessage, gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        If dialogResult = Windows.Forms.DialogResult.Yes Then
                            Set_DefaultPhamacy(nPatientId_RefillReq, oRefillRequest.PharmacyID, 0, gnClinicID)
                            _setDefaultPharmacy = True
                        Else
                            Set_DefaultPhamacy(nPatientId_RefillReq, oRefillRequest.PharmacyID, 1, gnClinicID)
                        End If
                    End If
                Else
                    Set_DefaultPhamacy(nPatientId_RefillReq, oRefillRequest.PharmacyID, 0, gnClinicID)
                End If
            End If

        Catch ex As Exception

        Finally
            If IsNothing(oDataTable) = False Then
                oDataTable.Dispose()
                oDataTable = Nothing
            End If
            If IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try

    End Sub
    'code added to do default pharmacy change in 6030
    Private Function Set_DefaultPhamacy(ByVal npatientid As Int64, ByVal nContactID As Int64, ByVal nsetDefault As Integer, ByVal nClinicId As Int64)
        Dim objCon As New SqlConnection
        Try
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_UpdateDefaultPharmacy"
            Dim objPara_PatientID As New SqlParameter
            With objPara_PatientID
                .ParameterName = "@npatientid"
                .Value = npatientid
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objPara_PatientID)
            Dim objPara_ContactID As New SqlParameter
            With objPara_ContactID
                .ParameterName = "@nContactID"
                .Value = nContactID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objPara_ContactID)
            Dim objPara_PharmacyID As New SqlParameter
            With objPara_PharmacyID
                .ParameterName = "@SetDefaultPharmacy"
                .Value = nsetDefault
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objPara_PharmacyID)
            Dim objPara_ClinicID As New SqlParameter
            With objPara_ClinicID
                .ParameterName = "@nClinicID"
                .Value = nClinicId
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objPara_ClinicID)
            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()

            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If

            objPara_PatientID = Nothing
            objPara_ContactID = Nothing
            objPara_PharmacyID = Nothing
            objPara_ClinicID = Nothing

            Return True
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            objCon.Close()
        End Try
    End Function

    Private Sub RemoveDrugGridControl()
        Try
            lblSupplyText.Visible = False
            If Not IsNothing(objDrugControl) Then
                If Panel1.Controls.Contains(objDrugControl) Then
                    Panel1.Controls.Remove(objDrugControl)
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.Remove, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub


    Private Sub objDrugControl_btnUC_Cancelclick(ByVal sender As Object, ByVal e As System.EventArgs) Handles objDrugControl.btnUC_Cancelclick
        RemoveDrugGridControl()
        Onepanelprocessrefill()

    End Sub
    'C

#Region "C1Refilllist function"

    Private Function SetClgrid()
        ''C1RefillList
        'Dim cStyle As C1.Win.C1FlexGrid.CellStyle

        Try

            With C1RefillList

                .Font = gloGlobal.clsgloFont.gFontArial_Regular 'New System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Regular)
                .SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell
                .BackColor = System.Drawing.Color.White
                .AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
                .Col = 0
                .Rows.Count = 1
                .Rows.Fixed = 1
                .Cols.Fixed = 0
                .Cols.Count = COL_COUNT
                '.Rows(.Rows.Count - 1).Height = 21
                .ExtendLastCol = True
                Dim _Width As Single = .Width / 10
                'set column value



                .Cols(COL_Approve).Width = _Width * 0.6
                .Cols(COL_Deny).Width = _Width * 0.5
                .Cols(COL_Cancel).Width = _Width * 0.5

                .Cols(COL_RxReferenceNumber).Width = 0 ' _Width * 1 '''''''''''''''''''''''''''''''''''''''''''''''
                .Cols(COL_RxTransactionId).Width = 0 '_Width * 1 '''''''''''''''''''''''''''''''''''''''''''''''
                .Cols(COL_PatientName).Width = _Width * 1.5
                .Cols(COL_PatientGender).Width = _Width * 0.5
                .Cols(COL_PatientDOB).Width = _Width * 1
                .Cols(COL_Medication).Width = _Width * 2
                .Cols(COL_Quantity).Width = _Width * 1.0
                .Cols(COL_PrescriptionDate).Width = _Width * 1.4
                .Cols(COL_DateReceived).Width = _Width * 1.2
                .Cols(COL_RefillQuantity).Width = _Width * 0.7
                .Cols(COL_PatientId).Width = 0 '_Width * 1 '''''''''''''''''''''''''''''''''''''''''''''''
                .Cols(COL_LastFillDate).Width = _Width * 1.2
                .Cols(COL_RefillQualifier).Width = _Width * 1

                .Cols(COL_SenderSoftwareVersion).Width = _Width * 1
                .Cols(COL_SenderSoftwareDeveloper).Width = _Width * 1
                .Cols(COL_SenderSoftwareProduct).Width = _Width * 1

                .Cols(COL_PrescriberNPI).Width = _Width * 1
                .Cols(COL_CodeListQualifier).Width = _Width * 1
                .Cols(COL_UnitSourceCode).Width = _Width * 1
                .Cols(COL_PotencyUnitCode).Width = _Width * 1

                .Cols(COL_MDDrugName).Width = _Width * 1
                .Cols(COL_MDDrugQuantity).Width = _Width * 1
                .Cols(COL_MDDrugQualifier).Width = _Width * 1
                .Cols(COL_MDRefillQuantity).Width = _Width * 1
                .Cols(COL_MDRefillsQualifier).Width = _Width * 1
                .Cols(COL_MDdtWrittenDate).Width = _Width * 1

                .Cols(COL_MDDrugDuration).Width = _Width * 1
                .Cols(COL_MDDrugDirections).Width = _Width * 1
                .Cols(COL_MDNotes).Width = _Width * 1
                .Cols(COL_MDdtLastFillDate).Width = _Width * 1

                .Cols(COL_MDProductCode).Width = _Width * 1
                .Cols(COL_MDProductCodeQualifier).Width = _Width * 1
                .Cols(COL_MDDosageForm).Width = _Width * 1
                .Cols(COL_MDDrugStrength).Width = _Width * 1
                .Cols(COL_MDDrugStrengthUnits).Width = _Width * 1

                .Cols(COL_MDCodeListQualifier).Width = _Width * 1
                .Cols(COL_MDUnitSourceCode).Width = _Width * 1
                .Cols(COL_MDPotencyUnitCode).Width = _Width * 1
                .Cols(COL_MDDrugDBCode).Width = _Width * 1
                .Cols(COL_MDDrugDBCodeQualifier).Width = _Width * 1
                .Cols(COL_Substitution).Width = _Width * 1

                .Cols(COL_MessageID).Width = 0 '_Width * 1 '''''''''''''''''''''''''''''''''''''''''''''''
                .Cols(COL_PharmacyID).Width = 0 '_Width * 1 '''''''''''''''''''''''''''''''''''''''''''''''
                .Cols(COL_PatientLastName).Width = 0 '_Width * 1 '''''''''''''''''''''''''''''''''''''''''''''''
                .Cols(COL_RefillReqNDCCode).Width = 0 '_Width * 0.5 '''''''''''''''''''''''''''''''''''''''''''''''

                .Cols(COL_DrugDirection).Width = 0
                .Cols(COL_dtWrittenDate).Width = 0
                .Cols(COL_Notes).Width = 0
                .Cols(COL_Substitution).Width = 0
                .Cols(COL_Drugstreangth).Width = 0

                'cStyle = C1RefillList.Styles.Add("BubbleValues")
                'cStyle.ComboList = "..."
                ''.CellButtonImage = imgTreeView.Images 
                'Dim rgBubbleValues As C1.Win.C1FlexGrid.CellRange = C1RefillList.GetCellRange(1, COL_Approve, 1, COL_Approve) ', _RowNo, COLUM_BUTTON)
                'rgBubbleValues.Style = cStyle

                'set column header
                .SetData(0, COL_Approve, "Approve")
                .SetData(0, COL_Deny, "Deny")
                .SetData(0, COL_Cancel, "Cancel")

                .SetData(0, COL_RxReferenceNumber, "RxReferenceNumber")
                .SetData(0, COL_RxTransactionId, "RxTransactionId")
                .SetData(0, COL_PatientName, "Name")
                .SetData(0, COL_PatientGender, "Gender") ''this is called as Medication but in image it is DrugName
                .SetData(0, COL_PatientDOB, "DOB")
                .SetData(0, COL_Medication, "Medication Prescribed")
                .SetData(0, COL_Quantity, "Quantity")
                .SetData(0, COL_PrescriptionDate, "Rx Date")
                .SetData(0, COL_DateReceived, "Date Rec.")
                .SetData(0, COL_RefillQuantity, "Refill Qty")
                .SetData(0, COL_PatientId, "PatientId") ''this is called as Amount but in image it is Dispense 
                .SetData(0, COL_LastFillDate, "Last Fill")
                .SetData(0, COL_RefillQualifier, "Ref. Qualifier")
                .SetData(0, COL_MessageID, "MessageID")
                .SetData(0, COL_PharmacyID, "PharmacyID")
                .SetData(0, COL_PatientLastName, "Last Name")
                .SetData(0, COL_RefillReqNDCCode, "RefillReqNDCCode")

                .SetData(0, COL_SenderSoftwareVersion, "SenderSoftwareVersion")
                .SetData(0, COL_SenderSoftwareDeveloper, "SenderSoftwareDeveloper")
                .SetData(0, COL_SenderSoftwareProduct, "SenderSoftwareProduct")

                .SetData(0, COL_PrescriberNPI, "PrescriberNPI")
                .SetData(0, COL_CodeListQualifier, "CodeListQualifier")
                .SetData(0, COL_UnitSourceCode, "UnitSourceCode")
                .SetData(0, COL_PotencyUnitCode, "PotencyUnitCode")

                .SetData(0, COL_MDDrugName, "MDDrugName")
                .SetData(0, COL_MDDrugQuantity, "MDDrugQuantity")
                .SetData(0, COL_MDDrugQualifier, "MDDrugQualifier")
                .SetData(0, COL_MDRefillQuantity, "MDRefillQuantity")
                .SetData(0, COL_MDRefillsQualifier, "MDRefillsQualifier")
                .SetData(0, COL_MDdtWrittenDate, "MDWrittenDate")

                .SetData(0, COL_MDDrugDuration, "MDDrugDuration")
                .SetData(0, COL_MDDrugDirections, "MDDrugDirections")
                .SetData(0, COL_MDNotes, "MDNotes")
                .SetData(0, COL_MDdtLastFillDate, "MDLastFillDate")

                .SetData(0, COL_MDProductCode, "MDProductCode")
                .SetData(0, COL_MDProductCodeQualifier, "MDProductCodeQualifier")
                .SetData(0, COL_MDDosageForm, "MDDosageForm")
                .SetData(0, COL_MDDrugStrength, "MDDrugStrength")
                .SetData(0, COL_MDDrugStrengthUnits, "MDDrugStrengthUnits")

                .SetData(0, COL_MDCodeListQualifier, "MDCodeListQualifier")
                .SetData(0, COL_MDUnitSourceCode, "MDUnitSourceCode")
                .SetData(0, COL_MDPotencyUnitCode, "MDPotencyUnitCode")
                .SetData(0, COL_MDDrugDBCode, "MDDrugDBCode")
                .SetData(0, COL_MDDrugDBCodeQualifier, "MDDrugDBCodeQualifier")
                .SetData(0, COL_MDSubstitution, "MDSubstitution")
                .SetData(0, COL_DrugCoverageStatusCode, "DrugCoverageStatusCode")
                .SetData(0, COL_MDDrugCoverageStatusCode, "MDDrugCoverageStatusCode")

                .SetData(0, COL_PharmacySpecialty, "PharmacySpecialty")
                .SetData(0, COL_PatientRelationship, "PatientRelationship")
                .SetData(0, COL_DrugDirection, "DrugDirection")
                .SetData(0, COL_dtWrittenDate, "WrittenDate")
                .SetData(0, COL_Notes, "DrugNotes")
                .SetData(0, COL_Substitution, "Substitution")
                .SetData(0, COL_Drugstreangth, "Strength")

                .SetData(0, COL_PatAddr1, "PatientAddress1")
                .SetData(0, COL_PatAddr2, "PatientAddress2")
                .SetData(0, COL_PatCity, "PatientCity")
                .SetData(0, COL_Patstate, "PatientState")
                .SetData(0, COL_PatZip, "PatientZipcode")
                .SetData(0, COL_PatPhone, "PatPhone")
                .SetData(0, COL_PatFax, "PatientFax")

                'set visiblity for column 
                .Cols(COL_Approve).Visible = True
                .Cols(COL_Deny).Visible = True

                ''Show Cancel only if this setting is OFF from admin setting->surescript setting
                If gblnAllowRefillCancel = False Then
                    .Cols(COL_Cancel).Visible = True
                Else
                    .Cols(COL_Cancel).Visible = False
                End If

                .Cols(COL_RxReferenceNumber).Visible = False ' True '''''''''''''''''''''''''''''''''''''''''''''''
                .Cols(COL_RxTransactionId).Visible = False 'True '''''''''''''''''''''''''''''''''''''''''''''''
                .Cols(COL_PatientName).Visible = True
                .Cols(COL_PatientGender).Visible = True
                .Cols(COL_PatientDOB).Visible = True
                .Cols(COL_Medication).Visible = True
                .Cols(COL_Quantity).Visible = True
                .Cols(COL_PrescriptionDate).Visible = True ''also called Dispense
                .Cols(COL_DateReceived).Visible = True
                .Cols(COL_RefillQuantity).Visible = True
                .Cols(COL_PatientId).Visible = False ' True '''''''''''''''''''''''''''''''''''''''''''''''
                .Cols(COL_LastFillDate).Visible = True
                .Cols(COL_RefillQualifier).Visible = True
                .Cols(COL_MessageID).Visible = False 'True '''''''''''''''''''''''''''''''''''''''''''''''
                .Cols(COL_PharmacyID).Visible = False 'True '''''''''''''''''''''''''''''''''''''''''''''''
                .Cols(COL_PatientLastName).Visible = False ' True '''''''''''''''''''''''''''''''''''''''''''''''
                .Cols(COL_RefillReqNDCCode).Visible = False 'True '''''''''''''''''''''''''''''''''''''''''''''''

                .Cols(COL_SenderSoftwareVersion).Visible = False
                .Cols(COL_SenderSoftwareDeveloper).Visible = False
                .Cols(COL_SenderSoftwareProduct).Visible = False

                .Cols(COL_PrescriberNPI).Visible = False
                .Cols(COL_CodeListQualifier).Visible = False
                .Cols(COL_UnitSourceCode).Visible = False
                .Cols(COL_PotencyUnitCode).Visible = False

                .Cols(COL_MDDrugName).Visible = False
                .Cols(COL_MDDrugQuantity).Visible = False
                .Cols(COL_MDDrugQualifier).Visible = False
                .Cols(COL_MDRefillQuantity).Visible = False
                .Cols(COL_MDRefillsQualifier).Visible = False
                .Cols(COL_MDdtWrittenDate).Visible = False

                .Cols(COL_MDDrugDuration).Visible = False
                .Cols(COL_MDDrugDirections).Visible = False
                .Cols(COL_MDNotes).Visible = False
                .Cols(COL_MDdtLastFillDate).Visible = False
                .Cols(COL_MDProductCode).Visible = False

                .Cols(COL_MDProductCodeQualifier).Visible = False
                .Cols(COL_MDDosageForm).Visible = False
                .Cols(COL_MDDrugStrength).Visible = False
                .Cols(COL_MDDrugStrengthUnits).Visible = False
                .Cols(COL_MDCodeListQualifier).Visible = False

                .Cols(COL_MDUnitSourceCode).Visible = False
                .Cols(COL_MDPotencyUnitCode).Visible = False
                .Cols(COL_MDDrugDBCode).Visible = False
                .Cols(COL_MDDrugDBCodeQualifier).Visible = False
                .Cols(COL_MDSubstitution).Visible = False

                .Cols(COL_DrugCoverageStatusCode).Visible = False
                .Cols(COL_MDDrugCoverageStatusCode).Visible = False

                .Cols(COL_PharmacySpecialty).Visible = False
                .Cols(COL_PatientRelationship).Visible = False

                .Cols(COL_DrugDirection).Visible = False
                .Cols(COL_dtWrittenDate).Visible = False
                .Cols(COL_Notes).Visible = False
                .Cols(COL_Substitution).Visible = False
                .Cols(COL_Drugstreangth).Visible = False

                ' set column editing properties.
                '.Cols(COL_Approve).AllowEditing = True
                '.Cols(COL_Deny).AllowEditing = True
                '.Cols(COL_Cancel).AllowEditing = True

                .Cols(COL_RxReferenceNumber).AllowEditing = False
                .Cols(COL_RxTransactionId).AllowEditing = False
                .Cols(COL_PatientName).AllowEditing = False
                .Cols(COL_PatientGender).AllowEditing = False
                .Cols(COL_PatientDOB).AllowEditing = False
                .Cols(COL_Medication).AllowEditing = False
                .Cols(COL_Quantity).AllowEditing = False
                .Cols(COL_PrescriptionDate).AllowEditing = False ''also called Dispense
                .Cols(COL_DateReceived).AllowEditing = False
                .Cols(COL_RefillQuantity).AllowEditing = False
                .Cols(COL_PatientId).AllowEditing = False
                .Cols(COL_LastFillDate).AllowEditing = False
                .Cols(COL_RefillQualifier).AllowEditing = False
                .Cols(COL_MessageID).AllowEditing = False
                .Cols(COL_PharmacyID).AllowEditing = False
                .Cols(COL_PatientLastName).AllowEditing = False
                .Cols(COL_RefillReqNDCCode).AllowEditing = False

                .Cols(COL_SenderSoftwareVersion).AllowEditing = False
                .Cols(COL_SenderSoftwareDeveloper).AllowEditing = False
                .Cols(COL_SenderSoftwareProduct).AllowEditing = False

                .Cols(COL_PrescriberNPI).AllowEditing = False
                .Cols(COL_CodeListQualifier).AllowEditing = False
                .Cols(COL_UnitSourceCode).AllowEditing = False
                .Cols(COL_PotencyUnitCode).AllowEditing = False

                .Cols(COL_MDDrugName).AllowEditing = False
                .Cols(COL_MDDrugQuantity).AllowEditing = False
                .Cols(COL_MDDrugQualifier).AllowEditing = False
                .Cols(COL_MDRefillQuantity).AllowEditing = False
                .Cols(COL_MDRefillsQualifier).AllowEditing = False
                .Cols(COL_MDdtWrittenDate).AllowEditing = False

                .Cols(COL_MDDrugDuration).AllowEditing = False
                .Cols(COL_MDDrugDirections).AllowEditing = False
                .Cols(COL_MDNotes).AllowEditing = False
                .Cols(COL_MDdtLastFillDate).AllowEditing = False
                .Cols(COL_MDProductCode).AllowEditing = False

                .Cols(COL_MDProductCodeQualifier).AllowEditing = False
                .Cols(COL_MDDosageForm).AllowEditing = False
                .Cols(COL_MDDrugStrength).AllowEditing = False
                .Cols(COL_MDDrugStrengthUnits).AllowEditing = False
                .Cols(COL_MDCodeListQualifier).AllowEditing = False

                .Cols(COL_MDUnitSourceCode).AllowEditing = False
                .Cols(COL_MDPotencyUnitCode).AllowEditing = False
                .Cols(COL_MDDrugDBCode).AllowEditing = False
                .Cols(COL_MDDrugDBCodeQualifier).AllowEditing = False
                .Cols(COL_Substitution).AllowEditing = False

                .Cols(COL_DrugCoverageStatusCode).AllowEditing = False
                .Cols(COL_MDDrugCoverageStatusCode).AllowEditing = False

                .Cols(COL_PharmacySpecialty).AllowEditing = False
                .Cols(COL_PatientRelationship).AllowEditing = False

                .ForeColor = Color.Black
            End With

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return Nothing
    End Function

    Private Sub SetGridStyle(ByVal oFlex As C1.Win.C1FlexGrid.C1FlexGrid)
        With oFlex
            .ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always

            Dim csHeader As C1.Win.C1FlexGrid.CellStyle '= .Styles.Add("CS_Header")
            Try
                If (.Styles.Contains("CS_Header")) Then
                    csHeader = .Styles("CS_Header")
                Else
                    csHeader = .Styles.Add("CS_Header")
                    With csHeader
                        .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold) 'IIf(Font.Height = 8.25F, gFont_SMALL_BOLD, IIf(Font.Height = 9.0F, gFont_BOLD, New Font(Font, FontStyle.Bold))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold) 'New Font(Font, FontStyle.Bold)
                        .ForeColor = Color.MediumBlue
                        .BackColor = Color.FromArgb(192, 203, 233)
                        '.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                        .DataType = GetType(String)
                    End With
                End If
            Catch ex As Exception
                csHeader = .Styles.Add("CS_Header")
                With csHeader
                    .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold) 'IIf(Font.Height = 8.25F, gFont_SMALL_BOLD, IIf(Font.Height = 9.0F, gFont_BOLD, New Font(Font, FontStyle.Bold))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold)' New Font(Font, FontStyle.Bold)
                    .ForeColor = Color.MediumBlue
                    .BackColor = Color.FromArgb(192, 203, 233)
                    '.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                    .DataType = GetType(String)
                End With
            End Try



            'Dim csRecord As C1.Win.C1FlexGrid.CellStyle = .Styles.Add("CS_Record")

            'With csRecord
            '    .Font = New Font(Font, FontStyle.Regular)
            '    .ForeColor = Color.Black
            '    .BackColor = Color.GhostWhite
            '    '.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
            '    .DataType = GetType(String)
            'End With

            Dim csComboList As C1.Win.C1FlexGrid.CellStyle '= .Styles.Add("CS_ComboList")
            Try
                If (.Styles.Contains("CS_ComboList")) Then
                    csComboList = .Styles("CS_ComboList")
                Else
                    csComboList = .Styles.Add("CS_ComboList")
                    With csComboList
                        .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Font, FontStyle.Regular)
                        .ForeColor = Color.Black
                        .BackColor = Color.GhostWhite
                        '.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                        .DataType = GetType(String)
                        .ComboList = "..."
                    End With
                End If
            Catch ex As Exception
                csComboList = .Styles.Add("CS_ComboList")
                With csComboList
                    .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Font, FontStyle.Regular)
                    .ForeColor = Color.Black
                    .BackColor = Color.GhostWhite
                    '.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                    .DataType = GetType(String)
                    .ComboList = "..."
                End With
            End Try


            'Dim csCheckBox As C1.Win.C1FlexGrid.CellStyle = .Styles.Add("CS_CheckBox")

            'With csCheckBox
            '    .Font = New Font(Font, FontStyle.Regular)
            '    .ForeColor = Color.Black
            '    .BackColor = Color.GhostWhite
            '    '.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
            '    .DataType = GetType(Boolean)
            'End With

            Dim csNotNormal As C1.Win.C1FlexGrid.CellStyle '= .Styles.Add("CS_NotNormal")
            Try
                If (.Styles.Contains("CS_NotNormal")) Then
                    csNotNormal = .Styles("CS_NotNormal")
                Else
                    csNotNormal = .Styles.Add("CS_NotNormal")
                    With csNotNormal
                        .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Font, FontStyle.Regular)
                        .ForeColor = Color.Red
                        .BackColor = Color.GhostWhite
                        '.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                        '.DataType = Type.GetType("System.Boolean")
                    End With
                End If
            Catch ex As Exception
                csNotNormal = .Styles.Add("CS_NotNormal")
                With csNotNormal
                    .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Font, FontStyle.Regular)
                    .ForeColor = Color.Red
                    .BackColor = Color.GhostWhite
                    '.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                    '.DataType = Type.GetType("System.Boolean")
                End With
            End Try


        End With
    End Sub

    Private Function setdatatoC1(ByVal dt As DataTable)
        Dim _sPatFirstName As String = ""
        Dim _sPatLastName As String = ""
        Dim _sPatDOB As String = ""
        Dim _sPatGender As String = ""
        Try
            Dim csComboList As C1.Win.C1FlexGrid.CellStyle '= C1RefillList.Styles.Add("CS_ComboList")
            Try
                If (C1RefillList.Styles.Contains("CS_ComboList")) Then
                    csComboList = C1RefillList.Styles("CS_ComboList")
                Else
                    csComboList = C1RefillList.Styles.Add("CS_ComboList")
                    With csComboList
                        .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Font, FontStyle.Regular)
                        .ForeColor = Color.Black
                        .BackColor = Color.GhostWhite
                        .DataType = GetType(String)
                        .ComboList = "..."
                    End With
                End If
            Catch ex As Exception
                csComboList = C1RefillList.Styles.Add("CS_ComboList")
                With csComboList
                    .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Font, FontStyle.Regular)
                    .ForeColor = Color.Black
                    .BackColor = Color.GhostWhite
                    .DataType = GetType(String)
                    .ComboList = "..."
                End With
            End Try

            C1RefillList.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always

            ''testing for column style
            'With csComboList
            '    .Font = New Font(Font, FontStyle.Regular)
            '    .ForeColor = Color.Black
            '    .BackColor = Color.GhostWhite
            '    .DataType = GetType(String)
            '    .ComboList = "..."
            'End With
            'C1RefillList.DataSource = dt
            'Dim oRange As C1.Win.C1FlexGrid.CellRange
            'oRange = C1RefillList.GetCellRange(1, COL_Approve, C1RefillList.Rows.Count - 1, COL_Approve)
            'oRange.Style = csComboList
            'C1RefillList.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
            'Exit Function


            For i As Integer = 0 To dt.Rows.Count - 1
                With C1RefillList
                    .Rows.Add()
                    .SetCellStyle(i + 1, COL_Approve, .Styles("CS_ComboList"))
                    .SetCellStyle(i + 1, COL_Deny, .Styles("CS_ComboList"))
                    .SetCellStyle(i + 1, COL_Cancel, .Styles("CS_ComboList"))

                    .SetData(i + 1, COL_Approve, "")
                    .SetData(i + 1, COL_Deny, "")
                    .SetData(i + 1, COL_Cancel, "")

                    .SetData(i + 1, COL_RxReferenceNumber, dt.Rows(i)(0))
                    .SetData(i + 1, COL_RxTransactionId, dt.Rows(i)(1))
                    .SetData(i + 1, COL_PatientName, Convert.ToString(dt.Rows(i)(2)).Trim)
                    .SetData(i + 1, COL_PatientGender, dt.Rows(i)(3)) ''this is called as Medication but in image it is DrugName
                    .SetData(i + 1, COL_PatientDOB, dt.Rows(i)(4))
                    .SetData(i + 1, COL_Medication, dt.Rows(i)(5))
                    Try
                        If dt.Rows(i)("DosageDescription") <> "" Then
                            .SetData(i + 1, COL_Quantity, dt.Rows(i)("DosageDescription"))
                        Else
                            .SetData(i + 1, COL_Quantity, dt.Rows(i)("Quantity"))
                        End If
                    Catch ex As Exception
                        .SetData(i + 1, COL_Quantity, dt.Rows(i)("Quantity"))
                    End Try

                    .SetData(i + 1, COL_PrescriptionDate, dt.Rows(i)(7))
                    .SetData(i + 1, COL_DateReceived, dt.Rows(i)(8))
                    .SetData(i + 1, COL_RefillQuantity, dt.Rows(i)(9))
                    If dt.Rows(i)(10).ToString <> "" Then ''dt.Rows(i)("PatientID")
                        .SetData(i + 1, COL_PatientId, dt.Rows(i)(10).ToString)
                    Else
                        'Case 2297
                        Dim retval As String() = SplitPatientName(dt.Rows(i)(15)) ''dt.Rows(i)("PatientLastName"))

                        If retval.Length > 1 Then
                            Select Case retval.Length
                                Case 3
                                    _sPatFirstName = retval(0)
                                    strPatientLastName = retval(2)
                                Case 2
                                    _sPatFirstName = retval(0)
                                    _sPatLastName = retval(1)
                            End Select

                        Else
                            If retval.Length = 0 Then
                                _sPatFirstName = dt.Rows(i)(15)
                            Else
                                _sPatFirstName = retval(0)
                            End If
                            If _sPatFirstName.Contains("|") Then
                                retval = SplitPatientName_WithPipe(_sPatFirstName)
                                If retval.Length > 1 Then
                                    Select Case retval.Length
                                        Case 3
                                            _sPatFirstName = retval(0)
                                            _sPatLastName = retval(2)
                                        Case 2
                                            _sPatFirstName = retval(0)
                                            _sPatLastName = retval(1)
                                    End Select
                                Else
                                    _sPatFirstName = retval(0)
                                    _sPatLastName = ""
                                End If
                            Else
                                _sPatLastName = ""
                            End If

                        End If

                        ''''''get the patient id and add to the C1 flex grid patient id column
                        _sPatGender = dt.Rows(i)(3)
                        Select Case _sPatGender

                            Case "M"
                                _sPatGender = "Male"
                            Case "Male"
                                _sPatGender = "Male"
                            Case "F"
                                _sPatGender = "Female"
                            Case "Female"
                                _sPatGender = "Female"
                            Case Else
                                _sPatGender = "Other"
                        End Select

                        _sPatDOB = dt.Rows(i)(4)

                        Dim _PatID As String = 0

                        Dim _gloEMRDBID As New DataBaseLayer

                        'get the patient information with matching First name, Last name, DOB and Gender as parameters
                        Dim _getPatIdSQL As String = "select nPatientID from patient where sFirstName = " & "'" & _sPatFirstName & "'" & " and sLastName = " & "'" & _sPatLastName & "'" & " and convert(datetime,convert (varchar(50),datepart(mm,dtDOB)) + '/'+ convert(varchar(50),datepart(dd,dtDOB))+'/'+ convert(varchar(50),datepart(yy,dtDOB))) = " & "'" & _sPatDOB & "'" & " and sGender = " & "'" & _sPatGender & "'" & "  "

                        _PatID = _gloEMRDBID.GetRecord_Query(_getPatIdSQL)
                        .SetData(i + 1, COL_PatientId, _PatID.ToString)
                    End If
                    .SetData(i + 1, COL_LastFillDate, dt.Rows(i)(11))
                    .SetData(i + 1, COL_RefillQualifier, dt.Rows(i)(12))
                    .SetData(i + 1, COL_MessageID, dt.Rows(i)(13))
                    .SetData(i + 1, COL_PharmacyID, dt.Rows(i)(14))
                    .SetData(i + 1, COL_PatientLastName, dt.Rows(i)(15))
                    .SetData(i + 1, COL_RefillReqNDCCode, dt.Rows(i)(16)) ''dt.Rows(i)("sProductCode")) ''Refill request NDCCode
                    ''New Col Added
                    .SetData(i + 1, COL_SenderSoftwareVersion, dt.Rows(i)("SenderSoftwareVersion"))
                    .SetData(i + 1, COL_SenderSoftwareDeveloper, dt.Rows(i)("SenderSoftwareDeveloper"))
                    .SetData(i + 1, COL_SenderSoftwareProduct, dt.Rows(i)("SenderSoftwareProduct"))

                    .SetData(i + 1, COL_PrescriberNPI, dt.Rows(i)("PrescriberNPI"))
                    .SetData(i + 1, COL_CodeListQualifier, dt.Rows(i)("CodeListQualifier"))
                    .SetData(i + 1, COL_UnitSourceCode, dt.Rows(i)("UnitSourceCode"))
                    .SetData(i + 1, COL_PotencyUnitCode, dt.Rows(i)("PotencyUnitCode"))

                    .SetData(i + 1, COL_MDDrugName, dt.Rows(i)("MDDrugName"))

                    .SetData(i + 1, COL_MDDrugQuantity, dt.Rows(i)("MDDrugQuantity"))
                    .SetData(i + 1, COL_MDDrugQualifier, dt.Rows(i)("MDDrugQualifier"))
                    .SetData(i + 1, COL_MDRefillQuantity, dt.Rows(i)("MDRefillQuantity"))
                    .SetData(i + 1, COL_MDRefillsQualifier, dt.Rows(i)("MDRefillsQualifier"))
                    .SetData(i + 1, COL_MDdtWrittenDate, dt.Rows(i)("MDWrittenDate"))


                    .SetData(i + 1, COL_MDDrugDuration, dt.Rows(i)("MDDrugDuration"))
                    .SetData(i + 1, COL_MDDrugDirections, dt.Rows(i)("MDDrugDirections"))
                    .SetData(i + 1, COL_MDNotes, dt.Rows(i)("MDNotes"))
                    .SetData(i + 1, COL_MDdtLastFillDate, dt.Rows(i)("MDLastFillDate"))
                    .SetData(i + 1, COL_MDProductCode, dt.Rows(i)("MDProductCode"))

                    .SetData(i + 1, COL_MDProductCodeQualifier, dt.Rows(i)("MDProductCodeQualifier"))
                    .SetData(i + 1, COL_MDDosageForm, dt.Rows(i)("MDDosageForm"))
                    .SetData(i + 1, COL_MDDrugStrength, dt.Rows(i)("MDDrugStrength"))
                    .SetData(i + 1, COL_MDDrugStrengthUnits, dt.Rows(i)("MDDrugStrengthUnits"))
                    .SetData(i + 1, COL_MDCodeListQualifier, dt.Rows(i)("MDCodeListQualifier"))
                    Try
                        .SetData(i + 1, COL_MDUnitSourceCode, dt.Rows(i)("MDUnitSourceCode"))
                    Catch ex As Exception
                        '.SetData(i + 1, COL_MDUnitSourceCode, dt.Rows(i)("UnitSourceCode"))
                    End Try

                    .SetData(i + 1, COL_MDPotencyUnitCode, dt.Rows(i)("MDPotencyUnitCode"))
                    .SetData(i + 1, COL_MDDrugDBCode, dt.Rows(i)("MDDrugDBCode"))
                    .SetData(i + 1, COL_MDDrugDBCodeQualifier, dt.Rows(i)("MDDrugDBCodeQualifier"))
                    Try
                        .SetData(i + 1, COL_MDSubstitution, If(dt.Rows(i)("MDSubstitution") = 0, "Yes", "No"))
                    Catch ex As Exception
                        .SetData(i + 1, COL_MDSubstitution, If(dt.Rows(i)("MDSubstitution") = "Yes", "Yes", "No"))
                    End Try


                    .SetData(i + 1, COL_DrugCoverageStatusCode, dt.Rows(i)("DrugCoverageStatusCode"))
                    .SetData(i + 1, COL_MDDrugCoverageStatusCode, dt.Rows(i)("MDDrugCoverageStatusCode"))

                    .SetData(i + 1, COL_PharmacySpecialty, dt.Rows(i)("PharmacySpecialty"))
                    .SetData(i + 1, COL_PatientRelationship, dt.Rows(i)("PatientRelationship"))

                    .SetData(i + 1, COL_DrugDirection, dt.Rows(i)("DrugDirection"))
                    .SetData(i + 1, COL_dtWrittenDate, dt.Rows(i)("WrittenDate"))
                    .SetData(i + 1, COL_Notes, dt.Rows(i)("DrugNotes"))
                    Try
                        .SetData(i + 1, COL_Substitution, If(dt.Rows(i)("Substitution") = 0, "Yes", "No"))
                    Catch ex As Exception
                        .SetData(i + 1, COL_Substitution, If(dt.Rows(i)("Substitution") = "Yes", "Yes", "No"))
                    End Try

                    .SetData(i + 1, COL_Drugstreangth, dt.Rows(i)("Strength"))
                    .SetData(i + 1, COL_PatAddr1, dt.Rows(i)("PatientAddress1"))
                    .SetData(i + 1, COL_PatAddr2, dt.Rows(i)("PatientAddress2"))
                    .SetData(i + 1, COL_PatCity, dt.Rows(i)("PatientCity"))
                    .SetData(i + 1, COL_Patstate, dt.Rows(i)("PatientState"))
                    .SetData(i + 1, COL_PatZip, dt.Rows(i)("PatientZipcode"))
                    .SetData(i + 1, COL_PatPhone, dt.Rows(i)("PatPhone"))
                    .SetData(i + 1, COL_PatFax, dt.Rows(i)("PatientFax"))
                End With
            Next
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.Initialize, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return Nothing
    End Function



    Private Sub C1RefillList_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1RefillList.MouseDoubleClick

        Dim oRefillRequest As gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest = Nothing


        Try
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = C1RefillList.HitTest(ptPoint)
            RemoveDrugGridControl()
            If htInfo.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.ColumnHeader Then


                Select Case htInfo.Column
                    Case 2 ' 2nd coloum is patient name
                        lblSearch.Text = "Patient Name"

                    Case 3 '3rd coloumn is medication
                        lblSearch.Text = "Medication"

                End Select
            End If
            If txtSearch.Text = "" Then
                _blnSearch = True
            Else
                _blnSearch = False
                txtSearch.Text = ""
                _blnSearch = True
            End If
            pnlProcessRefill.Visible = False
            pnlProcessRefill.SendToBack()

            '' ''to show pharamcy, patient & other info
            ''blnbtnstatus = True
            '' ''pnl_RefillInfo.Height = intRefillpanelheight
            ''spitpanelprocessrefill()
            ''btnupdown.BackgroundImage = Global.gloEMR.My.Resources.ImgDownButton    ''ImgUpButton
            ''btnupdown.BackgroundImageLayout = ImageLayout.Center
            '*********************************************************************
            ''assign the values of the selected row to the variables so that some of the values that are assigned can be passed to the oRefillRequest.GetDrugDetailsToRefill(sRxReferenceNumber, dtdatereceived, nRxTransactionId) 
            Dim selectedRowNo As Integer = C1RefillList.HitTest(e.X, e.Y).Row

            If selectedRowNo > 0 Then
                ''to show pharamcy, patient & other info
                blnbtnstatus = True
                ''pnl_RefillInfo.Height = intRefillpanelheight
                spitpanelprocessrefill()
                btnupdown.BackgroundImage = Global.gloEMR.My.Resources.Down    ''ImgUpButton
                btnupdown.BackgroundImageLayout = ImageLayout.Center

                strPatientName = ""
                strPatientMiddleName = ""
                strPatientLastName = ""


                strMedicationName = C1RefillList.Item(selectedRowNo, 8) ' 5)
                strPatientName = C1RefillList.Item(selectedRowNo, 5)
                'Case 2297
                Dim retval As String() = SplitPatientName(C1RefillList.Item(selectedRowNo, 5)) '15


                strPatientGender = C1RefillList.Item(selectedRowNo, 6) '3)

                strPatientDOB = C1RefillList.Item(selectedRowNo, 7) '4

                '1st col is Rxtransactionid
                nRxTransactionId = C1RefillList.Item(selectedRowNo, 4) '1)

                '5th col is Quantity
                strQuantity = C1RefillList.Item(selectedRowNo, 9) '6)

                '0th col is Rx Reference number
                sRxReferenceNumber = C1RefillList.Item(selectedRowNo, 3) '0)

                '7th col is Daterecieved
                dtdatereceived = C1RefillList.Item(selectedRowNo, 11) '8)

                '9th coloumn is patient id 
                If IsDBNull(C1RefillList.Item(selectedRowNo, 13)) Then '10
                    nPatientId_RefillReq = 0
                Else
                    nPatientId_RefillReq = C1RefillList.Item(selectedRowNo, 13) '10
                End If

                If Not IsNothing(C1RefillList.Item(selectedRowNo, 16)) Then '13
                    'If IsNumeric(dgRefillList.Item(selectedRowNo, 13)) Then
                    sMessageID = C1RefillList.Item(selectedRowNo, 16) '13

                    'Else
                    '    sMessageID = ""
                    'End If
                Else
                    sMessageID = ""
                End If

                If Not IsNothing(C1RefillList.Item(selectedRowNo, 17)) Then '14
                    'If IsNumeric(dgRefillList.Item(selectedRowNo, 14)) Then
                    sPharmacyID = C1RefillList.Item(selectedRowNo, 17) '14
                    'Else
                    '    sPharmacyID = ""
                    'End If
                Else
                    sPharmacyID = ""
                End If
                '*********************************************************************
                ''MD Drug Info  -------
                lblDrugName_Strength_Dosageform.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(selectedRowNo, 27)) & " " & ConvertIfNothingToEmptyString(C1RefillList.GetData(selectedRowNo, 40)) & " " & ConvertIfNothingToEmptyString(C1RefillList.GetData(selectedRowNo, 39))
                lblDrugQuantity.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(selectedRowNo, 43))
                lblDirection.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(selectedRowNo, 34))
                lblDrugNotes.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(selectedRowNo, 35))
                lblRefillQuantity.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(selectedRowNo, 30))
                lblWrittenDate.Text = If(IsNothing(C1RefillList.GetData(selectedRowNo, 32)), "", Format(C1RefillList.GetData(selectedRowNo, 32), "MM/dd/yyyy"))
                lblSubstitution.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(selectedRowNo, 46))
                '----xx-----
                lbl_Ref_Qlfr.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(selectedRowNo, COL_RefillQualifier))
                lbl_MDRef_Qlfr.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(selectedRowNo, COL_MDRefillsQualifier))


                ''Prescribed Drug Info  -------
                lbl_MPDrugName_Strength_Dosageform.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(selectedRowNo, COL_Medication)) & " " & ConvertIfNothingToEmptyString(C1RefillList.GetData(selectedRowNo, COL_Drugstreangth))
                lbl_MPDrugQuantity.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(selectedRowNo, COL_Quantity))
                lbl_MPDirection.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(selectedRowNo, COL_DrugDirection))
                lbl_MPDrugnotes.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(selectedRowNo, COL_Notes))
                lbl_MPRefillQuantity.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(selectedRowNo, COL_RefillQuantity))
                lbl_MPWrittenDate.Text = If(IsNothing(C1RefillList.GetData(selectedRowNo, COL_dtWrittenDate)), "", Format(C1RefillList.GetData(selectedRowNo, COL_dtWrittenDate), "MM/dd/yyyy"))
                lbl_MPSubstitution.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(selectedRowNo, COL_Substitution))
                '----xx-----


                oRefillRequest = New gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest
                'Get Pharmcy and Prescriber and Patient info for selected refillrequest
                oRefillRequest.GetDrugDetailsToRefill(sMessageID, sRxReferenceNumber, nRxTransactionId)

                'Get data from ogloPrescription and set it to property procedures
                oRefillRequest.SetRefillRequestData()

                'drug Info
                lbl_LastFillDate.Text = oRefillRequest.DTlastdate
                lbl_MDLastFillDate.Text = oRefillRequest.MDDTlastdate
                lbl_Duration.Text = If(oRefillRequest.Duration = "", "", oRefillRequest.Duration)
                lbl_MDDuration.Text = If(oRefillRequest.MDDuration = "", "", oRefillRequest.MDDuration)

                'assign values to Patient information labels from orefillrequest property procedures
                lbl_Patient.Text = oRefillRequest.PatientName
                lbl_PatDOB.Text = oRefillRequest.PatientDOB
                'lbl_PatGender.Text = oRefillRequest.PatientGender
                Select Case oRefillRequest.PatientGender
                    Case "M"
                        lbl_PatGender.Text = "Male"
                    Case "Male"
                        lbl_PatGender.Text = "Male"
                    Case "F"
                        lbl_PatGender.Text = "Female"
                    Case "Female"
                        lbl_PatGender.Text = "Female"
                    Case Else
                        lbl_PatGender.Text = "Other"
                End Select
                lbl_PatientAddress.Text = oRefillRequest.PatientAddress
                strPatientAddress1 = oRefillRequest.PatientAddress
                lblPatientAddress2.Text = oRefillRequest.PatientAddress2 ''--
                strPatientAddress2 = oRefillRequest.PatientAddress2
                lbl_PatientPhoneNo.Text = oRefillRequest.PatientPhone
                strPatientPhone = oRefillRequest.PatientPhone
                lbl_CityName.Text = oRefillRequest.PatientCity
                strPatientCity = oRefillRequest.PatientCity
                lbl_StateName.Text = oRefillRequest.PatientState
                strPatientState = oRefillRequest.PatientState
                lbl_ZIPCode.Text = oRefillRequest.PatientZip
                strPatientZip = oRefillRequest.PatientZip

                'assign values to Pharmacy information labels from orefillrequest property procedures
                lbl_Pharmacy.Text = oRefillRequest.PharmacyName
                lbl_PharmacyAddress.Text = oRefillRequest.PharmacyAddress
                lblPharmacyAddress2.Text = oRefillRequest.PharmacyAddress2
                lbl_PharmacyPhoneNo.Text = oRefillRequest.PharmacyPhone
                lbl_PharamcyCity.Text = oRefillRequest.PharmacyCity
                lbl_PharmacyState.Text = oRefillRequest.PharmacyState
                lbl_PharmacyZip.Text = oRefillRequest.PharmacyZip

                lbl_PharmacyFax.Text = oRefillRequest.PharmacyFax
                lbl_PharmacyNPI.Text = oRefillRequest.PharmacyNPI

                'assign values to Provider (means Prescriber) information labels from orefillrequest property procedures
                lbl_Provider.Text = oRefillRequest.ProviderName
                lbl_ProviderAddress.Text = oRefillRequest.ProviderAddress
                lblProviderAddress2.Text = oRefillRequest.ProviderAddress2
                lbl_PrPhone.Text = oRefillRequest.ProviderPhone
                lbl_ProviderCity.Text = oRefillRequest.ProviderCity
                lbl_ProviderState.Text = oRefillRequest.ProviderState
                lbl_ProviderZIP.Text = oRefillRequest.ProviderZip

                lbl_ProviderFax.Text = oRefillRequest.ProviderFax
                lbl_PrescriberNPI.Text = oRefillRequest.PrescriberNPI
                pnl_RefillInfo.Visible = True


            End If



        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oRefillRequest) Then
                oRefillRequest.Dispose()
                oRefillRequest = Nothing
            End If
        End Try

    End Sub

    Private Sub C1RefillList_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1RefillList.MouseDown
        Dim oRefillRequest As gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest
        Dim ogloInterface As gloSureScript.gloSureScriptInterface = Nothing

        Dim colno As Integer = 0
        Try
            Dim selectedRowNo As Integer = C1RefillList.HitTest(e.X, e.Y).Row
            'RemoveDrugGridControl()
            If e.Button = Windows.Forms.MouseButtons.Right Then
                'if the mouse is clicked outside the boundry of the C1 then dont show the context menu

                colno = C1RefillList.HitTest(e.X, e.Y).Column
                pnlProcessRefill.Visible = False
                pnlProcessRefill.SendToBack()
                'the mouse is clicked outside the C1
                If selectedRowNo < 1 Then
                    C1RefillList.ContextMenuStrip = Nothing
                    Exit Sub

                Else ' there is data in the grid

                    strPatientName = ""
                    strPatientMiddleName = ""
                    strPatientLastName = ""

                    '3rd col is medication name
                    strMedicationName = C1RefillList.Item(selectedRowNo, 8) '5)
                    strPatientName = C1RefillList.Item(selectedRowNo, 5)

                    'Case 2297
                    Dim retval As String() = SplitPatientName(C1RefillList.Item(selectedRowNo, 5))  ' 15))
                    If retval.Length > 1 Then
                        Select Case retval.Length
                            Case 3
                                strPatientName = retval(0)
                                strPatientMiddleName = retval(1)
                                strPatientLastName = retval(2)
                            Case 2
                                strPatientName = retval(0)
                                strPatientLastName = retval(1)
                        End Select
                    Else
                        If retval.Length = 0 Then
                            strPatientName = C1RefillList.Item(selectedRowNo, 5)
                        Else
                            strPatientName = retval(0)
                        End If
                        If strPatientName.Contains("|") Then
                            retval = SplitPatientName_WithPipe(strPatientName)
                            If retval.Length > 1 Then
                                Select Case retval.Length
                                    Case 3
                                        strPatientName = retval(0)
                                        strPatientMiddleName = retval(1)
                                        strPatientLastName = retval(2)
                                    Case 2
                                        strPatientName = retval(0)
                                        strPatientLastName = retval(1)
                                End Select
                            Else
                                strPatientName = retval(0)
                                strPatientLastName = ""
                            End If
                        Else
                            strPatientLastName = ""
                        End If

                    End If


                    strPatientGender = C1RefillList.Item(selectedRowNo, 6)  ' 3)

                    strPatientDOB = C1RefillList.Item(selectedRowNo, 7) '4)

                    '1st col is Rxtransactionid
                    If Not IsNothing(C1RefillList.Item(selectedRowNo, 4)) Then   '1
                        nRxTransactionId = C1RefillList.Item(selectedRowNo, 4) '1)

                    Else
                        nRxTransactionId = ""
                    End If



                    '5th col is Quantity
                    strQuantity = C1RefillList.Item(selectedRowNo, 9) '6)

                    '0th col is Rx Reference number
                    sRxReferenceNumber = C1RefillList.Item(selectedRowNo, 3) '0)

                    '7th col is Daterecieved

                    dtdatereceived = C1RefillList.Item(selectedRowNo, 11) '8)

                    '9th coloumn is patient id 
                    If Not IsNothing(C1RefillList.Item(selectedRowNo, 13)) Then '10
                        If IsNumeric(C1RefillList.Item(selectedRowNo, 13)) Then '10
                            nPatientId_RefillReq = C1RefillList.Item(selectedRowNo, 13) '10

                        Else
                            nPatientId_RefillReq = 0
                        End If
                    Else
                        nPatientId_RefillReq = 0
                    End If


                    If Not IsNothing(C1RefillList.Item(selectedRowNo, 16)) Then '13
                        'If IsNumeric(dgRefillList.Item(selectedRowNo, 13)) Then
                        sMessageID = C1RefillList.Item(selectedRowNo, 16) '13

                        'Else
                        '    sMessageID = ""
                        'End If
                    Else
                        sMessageID = ""
                    End If

                    If Not IsNothing(C1RefillList.Item(selectedRowNo, 17)) Then '14
                        'If IsNumeric(dgRefillList.Item(selectedRowNo, 14)) Then
                        sPharmacyID = C1RefillList.Item(selectedRowNo, 17) '14

                        'Else
                        '    sPharmacyID = ""
                        'End If
                    Else
                        sPharmacyID = ""
                    End If

                    C1RefillList.Select(selectedRowNo, colno)

                    'cntListmenuStrip.Items.Clear()

                    '---------------------------------------------------------------
                    ''MD Drug Info  -------
                    lblDrugName_Strength_Dosageform.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(selectedRowNo, 27)) & " " & ConvertIfNothingToEmptyString(C1RefillList.GetData(selectedRowNo, 40)) & " " & ConvertIfNothingToEmptyString(C1RefillList.GetData(selectedRowNo, 39))
                    lblDrugQuantity.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(selectedRowNo, 43))
                    lblDirection.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(selectedRowNo, 34))
                    lblDrugNotes.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(selectedRowNo, 35))
                    lblRefillQuantity.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(selectedRowNo, 30))
                    lblWrittenDate.Text = If(IsNothing(C1RefillList.GetData(selectedRowNo, 32)), "", Format(C1RefillList.GetData(selectedRowNo, 32), "MM/dd/yyyy"))
                    lblSubstitution.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(selectedRowNo, 46))
                    '----xx-----
                    lbl_Ref_Qlfr.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(selectedRowNo, COL_RefillQualifier))
                    lbl_MDRef_Qlfr.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(selectedRowNo, COL_MDRefillsQualifier))


                    ''Prescribed Drug Info  -------
                    lbl_MPDrugName_Strength_Dosageform.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(selectedRowNo, COL_Medication)) & " " & ConvertIfNothingToEmptyString(C1RefillList.GetData(selectedRowNo, COL_Drugstreangth))
                    lbl_MPDrugQuantity.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(selectedRowNo, COL_Quantity))
                    lbl_MPDirection.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(selectedRowNo, COL_DrugDirection))
                    lbl_MPDrugnotes.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(selectedRowNo, COL_Notes))
                    lbl_MPRefillQuantity.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(selectedRowNo, COL_RefillQuantity))
                    lbl_MPWrittenDate.Text = If(IsNothing(C1RefillList.GetData(selectedRowNo, COL_dtWrittenDate)), "", Format(C1RefillList.GetData(selectedRowNo, COL_dtWrittenDate), "MM/dd/yyyy"))
                    lbl_MPSubstitution.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(selectedRowNo, COL_Substitution))
                    '----xx-----


                    oRefillRequest = New gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest
                    'Get Pharmcy and Prescriber and Patient info for selected refillrequest
                    oRefillRequest.GetDrugDetailsToRefill(sMessageID, sRxReferenceNumber, nRxTransactionId)

                    'Get data from ogloPrescription and set it to property procedures
                    oRefillRequest.SetRefillRequestData()


                    '' ------------" Load XML"  ------

                    If Not IsNothing(oRefillRequest.FileData) Then
                        Dim dsViewDataset As New DataSet()
                        Dim dtNewRow As DataRow = Nothing

                        ogloInterface = New gloSureScript.gloSureScriptInterface
                        ogloInterface.LoadtoDataset(dsViewDataset, oRefillRequest.FileData)
                        Dim XMLFile As String = ogloInterface.ViewXML(dsViewDataset)
                        If XMLFile <> "" Then
                            If Not IsNothing(wbBrowser) Then
                                If (pnlwbBrowser.Controls.Contains(wbBrowser)) Then
                                    pnlwbBrowser.Controls.Remove(wbBrowser)
                                End If
                                wbBrowser.Dispose()
                                wbBrowser = Nothing
                            End If
                            wbBrowser = New WebBrowser
                            wbBrowser.Navigate(XMLFile)
                            If pnlwbBrowser.Visible = False Then
                                pnlwbBrowser.Visible = True
                            End If
                            ' pnlwbBrowser.Height = 690
                            pnlwbBrowser.Height = pnlwbBrowser.Parent.Height - 100
                            If (pnlwbBrowser.Height > 690) Then
                                pnlwbBrowser.Height = 690
                            End If
                            pnlwbBrowser.Controls.Add(wbBrowser)
                            wbBrowser.Dock = DockStyle.Fill
                            'pnlwbBrowser.Dock = DockStyle.Fill
                            'pnlwbBrowser.BringToFront()

                            ShowCntrlAsPerResolution()
                        End If
                        If Not IsNothing(dsViewDataset) Then
                            dsViewDataset.Dispose()
                            dsViewDataset = Nothing
                        End If
                        ''-------- End ----------------------
                        If Not IsNothing(ogloInterface) Then
                            ogloInterface.Dispose()
                            ogloInterface = Nothing
                        End If
                    End If



                    'drug Info
                    lbl_LastFillDate.Text = oRefillRequest.DTlastdate
                    lbl_MDLastFillDate.Text = oRefillRequest.MDDTlastdate
                    lbl_Duration.Text = If(oRefillRequest.Duration = "", "", oRefillRequest.Duration)
                    lbl_MDDuration.Text = If(oRefillRequest.MDDuration = "", "", oRefillRequest.MDDuration)

                    'assign values to Patient information labels from orefillrequest property procedures
                    lbl_Patient.Text = oRefillRequest.PatientName
                    lbl_PatDOB.Text = oRefillRequest.PatientDOB
                    '  lbl_PatGender.Text = oRefillRequest.PatientGender
                    Select Case oRefillRequest.PatientGender
                        Case "M"
                            lbl_PatGender.Text = "Male"
                        Case "Male"
                            lbl_PatGender.Text = "Male"
                        Case "F"
                            lbl_PatGender.Text = "Female"
                        Case "Female"
                            lbl_PatGender.Text = "Female"
                        Case Else
                            lbl_PatGender.Text = "Other"
                    End Select
                    lbl_PatientAddress.Text = oRefillRequest.PatientAddress
                    strPatientAddress1 = oRefillRequest.PatientAddress
                    lblPatientAddress2.Text = oRefillRequest.PatientAddress2 ''--
                    strPatientAddress2 = oRefillRequest.PatientAddress2
                    lbl_PatientPhoneNo.Text = oRefillRequest.PatientPhone
                    strPatientPhone = oRefillRequest.PatientPhone
                    lbl_CityName.Text = oRefillRequest.PatientCity
                    strPatientCity = oRefillRequest.PatientCity
                    lbl_StateName.Text = oRefillRequest.PatientState
                    strPatientState = oRefillRequest.PatientState
                    lbl_ZIPCode.Text = oRefillRequest.PatientZip
                    strPatientZip = oRefillRequest.PatientZip

                    'assign values to Pharmacy information labels from orefillrequest property procedures
                    lbl_Pharmacy.Text = oRefillRequest.PharmacyName
                    lbl_PharmacyAddress.Text = oRefillRequest.PharmacyAddress
                    lblPharmacyAddress2.Text = oRefillRequest.PharmacyAddress2
                    lbl_PharmacyPhoneNo.Text = oRefillRequest.PharmacyPhone
                    lbl_PharamcyCity.Text = oRefillRequest.PharmacyCity
                    lbl_PharmacyState.Text = oRefillRequest.PharmacyState
                    lbl_PharmacyZip.Text = oRefillRequest.PharmacyZip

                    lbl_PharmacyFax.Text = oRefillRequest.PharmacyFax
                    lbl_PharmacyNPI.Text = oRefillRequest.PharmacyNPI

                    'assign values to Provider (means Prescriber) information labels from orefillrequest property procedures
                    lbl_Provider.Text = oRefillRequest.ProviderName
                    lbl_ProviderAddress.Text = oRefillRequest.ProviderAddress
                    lblProviderAddress2.Text = oRefillRequest.ProviderAddress2
                    lbl_PrPhone.Text = oRefillRequest.ProviderPhone
                    lbl_ProviderCity.Text = oRefillRequest.ProviderCity
                    lbl_ProviderState.Text = oRefillRequest.ProviderState
                    lbl_ProviderZIP.Text = oRefillRequest.ProviderZip

                    lbl_ProviderFax.Text = oRefillRequest.ProviderFax
                    lbl_PrescriberNPI.Text = oRefillRequest.PrescriberNPI
                    pnl_RefillInfo.Visible = True

                    '---------------------------------------------------------------

                    ''call the AddMenu function only when there is rows(data present) in the dgRefillList datagrid
                    'AddMenu()
                    C1RefillList.ContextMenuStrip = cntListmenuStrip
                End If
                'Else
                '    pnlProcessRefill.Visible = False
                '    pnlProcessRefill.SendToBack()
                '    pnl_RefillInfo.Visible = True
            Else
                If selectedRowNo > 0 Then
                    If Not IsNothing(C1RefillList.Item(selectedRowNo, COL_PatientId)) Then '10
                        If Convert.ToString(C1RefillList.Item(selectedRowNo, COL_PatientId)) <> "" Then '10
                            nPatientId_RefillReq = C1RefillList.Item(selectedRowNo, COL_PatientId) '10

                        Else ''''''patient not registered in database
                            nPatientId_RefillReq = 0

                            MessageBox.Show("Selected patient is not registered.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)

                            '''''-----------Code to show into information panel all info /////added on 20100120
                            '3rd col is medication name
                            strMedicationName = C1RefillList.Item(selectedRowNo, 8) '5)

                            'Case 2297
                            Dim retval As String() = SplitPatientName(C1RefillList.Item(selectedRowNo, 5))  ' 15))
                            If retval.Length > 1 Then
                                Select Case retval.Length
                                    Case 3
                                        strPatientName = retval(0)
                                        strPatientMiddleName = retval(1)
                                        strPatientLastName = retval(2)
                                    Case 2
                                        strPatientName = retval(0)
                                        strPatientLastName = retval(1)
                                End Select
                            Else
                                If retval.Length = 0 Then
                                    strPatientName = C1RefillList.Item(selectedRowNo, 5)
                                Else
                                    strPatientName = retval(0)
                                End If
                                If strPatientName.Contains("|") Then
                                    retval = SplitPatientName_WithPipe(strPatientName)
                                    If retval.Length > 1 Then
                                        Select Case retval.Length
                                            Case 3
                                                strPatientName = retval(0)
                                                strPatientMiddleName = retval(1)
                                                strPatientLastName = retval(2)
                                            Case 2
                                                strPatientName = retval(0)
                                                strPatientLastName = retval(1)
                                        End Select
                                    Else
                                        strPatientName = retval(0)
                                        strPatientLastName = ""
                                    End If
                                Else
                                    strPatientLastName = ""
                                End If

                            End If


                            'Case 2297
                            strPatientGender = C1RefillList.Item(selectedRowNo, 6)  ' 3)
                            strPatientDOB = C1RefillList.Item(selectedRowNo, 7) '4)

                            '1st col is Rxtransactionid
                            If Not IsNothing(C1RefillList.Item(selectedRowNo, 4)) Then   '1
                                nRxTransactionId = C1RefillList.Item(selectedRowNo, 4) '1)

                            Else
                                nRxTransactionId = ""
                            End If
                            '5th col is Quantity
                            strQuantity = C1RefillList.Item(selectedRowNo, 9) '6)

                            '0th col is Rx Reference number
                            sRxReferenceNumber = C1RefillList.Item(selectedRowNo, 3) '0)

                            '7th col is Daterecieved

                            dtdatereceived = C1RefillList.Item(selectedRowNo, 11) '8)

                            '9th coloumn is patient id 
                            If Not IsNothing(C1RefillList.Item(selectedRowNo, 13)) Then '10
                                If IsNumeric(C1RefillList.Item(selectedRowNo, 13)) Then '10
                                    nPatientId_RefillReq = C1RefillList.Item(selectedRowNo, 13) '10

                                Else
                                    nPatientId_RefillReq = 0
                                End If
                            Else
                                nPatientId_RefillReq = 0
                            End If


                            If Not IsNothing(C1RefillList.Item(selectedRowNo, 16)) Then '13
                                'If IsNumeric(dgRefillList.Item(selectedRowNo, 13)) Then
                                sMessageID = C1RefillList.Item(selectedRowNo, 16) '13
                            Else
                                sMessageID = ""
                            End If

                            If Not IsNothing(C1RefillList.Item(selectedRowNo, 17)) Then '14
                                'If IsNumeric(dgRefillList.Item(selectedRowNo, 14)) Then
                                sPharmacyID = C1RefillList.Item(selectedRowNo, 17) '14
                            Else
                                sPharmacyID = ""
                            End If

                            C1RefillList.Select(selectedRowNo, colno)
                            'cntListmenuStrip.Items.Clear()
                            '---------------------------------------------------------------
                            oRefillRequest = New gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest
                            'Get Pharmcy and Prescriber and Patient info for selected refillrequest
                            oRefillRequest.GetDrugDetailsToRefill(sMessageID, sRxReferenceNumber, nRxTransactionId)

                            'Get data from ogloPrescription and set it to property procedures
                            oRefillRequest.SetRefillRequestData()

                            'drug Info
                            lbl_LastFillDate.Text = oRefillRequest.DTlastdate
                            lbl_MDLastFillDate.Text = oRefillRequest.MDDTlastdate
                            lbl_Duration.Text = If(oRefillRequest.Duration = "", "", oRefillRequest.Duration)
                            lbl_MDDuration.Text = If(oRefillRequest.MDDuration = "", "", oRefillRequest.MDDuration)

                            'assign values to Patient information labels from orefillrequest property procedures
                            lbl_Patient.Text = oRefillRequest.PatientName
                            lbl_PatDOB.Text = oRefillRequest.PatientDOB
                            'lbl_PatGender.Text = oRefillRequest.PatientGender
                            Select Case oRefillRequest.PatientGender
                                Case "M"
                                    lbl_PatGender.Text = "Male"
                                Case "Male"
                                    lbl_PatGender.Text = "Male"
                                Case "F"
                                    lbl_PatGender.Text = "Female"
                                Case "Female"
                                    lbl_PatGender.Text = "Female"
                                Case Else
                                    lbl_PatGender.Text = "Other"
                            End Select
                            lbl_PatientAddress.Text = oRefillRequest.PatientAddress
                            lblPatientAddress2.Text = oRefillRequest.PatientAddress2
                            lbl_PatientPhoneNo.Text = oRefillRequest.PatientPhone
                            lbl_CityName.Text = oRefillRequest.PatientCity
                            lbl_StateName.Text = oRefillRequest.PatientState
                            lbl_ZIPCode.Text = oRefillRequest.PatientZip

                            'assign values to Pharmacy information labels from orefillrequest property procedures
                            lbl_Pharmacy.Text = oRefillRequest.PharmacyName
                            lbl_PharmacyAddress.Text = oRefillRequest.PharmacyAddress
                            lblPharmacyAddress2.Text = oRefillRequest.PharmacyAddress2
                            lbl_PharmacyPhoneNo.Text = oRefillRequest.PharmacyPhone
                            lbl_PharamcyCity.Text = oRefillRequest.PharmacyCity
                            lbl_PharmacyState.Text = oRefillRequest.PharmacyState
                            lbl_PharmacyZip.Text = oRefillRequest.PharmacyZip

                            lbl_PharmacyFax.Text = oRefillRequest.PharmacyFax
                            lbl_PharmacyNPI.Text = oRefillRequest.PharmacyNPI

                            'assign values to Provider (means Prescriber) information labels from orefillrequest property procedures
                            lbl_Provider.Text = oRefillRequest.ProviderName
                            lbl_ProviderAddress.Text = oRefillRequest.ProviderAddress
                            lblProviderAddress2.Text = oRefillRequest.ProviderAddress2
                            lbl_PrPhone.Text = oRefillRequest.ProviderPhone
                            lbl_ProviderCity.Text = oRefillRequest.ProviderCity
                            lbl_ProviderState.Text = oRefillRequest.ProviderState
                            lbl_ProviderZIP.Text = oRefillRequest.ProviderZip


                            lbl_ProviderFax.Text = oRefillRequest.ProviderFax
                            lbl_PrescriberNPI.Text = oRefillRequest.PrescriberNPI
                            'pnl_RefillInfo.Visible = True
                            '---------------------------------------------------------------
                            ''''------
                        End If
                    Else ''''''patient not registered in database
                        nPatientId_RefillReq = 0

                        MessageBox.Show("Selected patient is not registered.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        '''''-----------Code to show into information panel all info /////added on 20100120
                        '3rd col is medication name
                        strMedicationName = C1RefillList.Item(selectedRowNo, 8) '5)
                        'Case 2297
                        Dim retval As String() = SplitPatientName(C1RefillList.Item(selectedRowNo, 5))  ' 15))
                        If retval.Length > 1 Then
                            Select Case retval.Length
                                Case 3
                                    strPatientName = retval(0)
                                    strPatientMiddleName = retval(1)
                                    strPatientLastName = retval(2)
                                Case 2
                                    strPatientName = retval(0)
                                    strPatientLastName = retval(1)
                            End Select
                        Else
                            If retval.Length = 0 Then
                                strPatientName = C1RefillList.Item(selectedRowNo, 5)
                            Else
                                strPatientName = retval(0)
                            End If
                            If strPatientName.Contains("|") Then
                                retval = SplitPatientName_WithPipe(strPatientName)
                                If retval.Length > 1 Then
                                    Select Case retval.Length
                                        Case 3
                                            strPatientName = retval(0)
                                            strPatientMiddleName = retval(1)
                                            strPatientLastName = retval(2)
                                        Case 2
                                            strPatientName = retval(0)
                                            strPatientLastName = retval(1)
                                    End Select
                                Else
                                    strPatientName = retval(0)
                                    strPatientLastName = ""
                                End If
                            Else
                                strPatientLastName = ""
                            End If

                        End If


                        'Case 2297
                        strPatientGender = C1RefillList.Item(selectedRowNo, 6)  ' 3)
                        strPatientDOB = C1RefillList.Item(selectedRowNo, 7) '4)

                        strPatientAddress1 = C1RefillList.Item(selectedRowNo, COL_PatAddr1)
                        strPatientAddress2 = C1RefillList.Item(selectedRowNo, COL_PatAddr2)
                        strPatientCity = C1RefillList.Item(selectedRowNo, COL_PatCity)
                        strPatientState = C1RefillList.Item(selectedRowNo, COL_Patstate)
                        strPatientZip = C1RefillList.Item(selectedRowNo, COL_PatZip)
                        strPatientPhone = C1RefillList.Item(selectedRowNo, COL_PatPhone)

                        '1st col is Rxtransactionid
                        If Not IsNothing(C1RefillList.Item(selectedRowNo, 4)) Then   '1
                            nRxTransactionId = C1RefillList.Item(selectedRowNo, 4) '1)
                        Else
                            nRxTransactionId = ""
                        End If
                        '5th col is Quantity
                        strQuantity = C1RefillList.Item(selectedRowNo, 9) '6)

                        '0th col is Rx Reference number
                        sRxReferenceNumber = C1RefillList.Item(selectedRowNo, 3) '0)

                        '7th col is Daterecieved
                        dtdatereceived = C1RefillList.Item(selectedRowNo, 11) '8)

                        '9th coloumn is patient id 
                        If Not IsNothing(C1RefillList.Item(selectedRowNo, 13)) Then '10
                            If IsNumeric(C1RefillList.Item(selectedRowNo, 13)) Then '10
                                nPatientId_RefillReq = C1RefillList.Item(selectedRowNo, 13) '10

                            Else
                                nPatientId_RefillReq = 0
                            End If
                        Else
                            nPatientId_RefillReq = 0
                        End If

                        If Not IsNothing(C1RefillList.Item(selectedRowNo, 16)) Then '13
                            'If IsNumeric(dgRefillList.Item(selectedRowNo, 13)) Then
                            sMessageID = C1RefillList.Item(selectedRowNo, 16) '13
                        Else
                            sMessageID = ""
                        End If

                        If Not IsNothing(C1RefillList.Item(selectedRowNo, 17)) Then '14
                            'If IsNumeric(dgRefillList.Item(selectedRowNo, 14)) Then
                            sPharmacyID = C1RefillList.Item(selectedRowNo, 17) '14
                        Else
                            sPharmacyID = ""
                        End If

                        C1RefillList.Select(selectedRowNo, colno)
                        'cntListmenuStrip.Items.Clear()
                        '---------------------------------------------------------------
                        oRefillRequest = New gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest
                        'Get Pharmcy and Prescriber and Patient info for selected refillrequest
                        oRefillRequest.GetDrugDetailsToRefill(sMessageID, sRxReferenceNumber, nRxTransactionId)

                        'Get data from ogloPrescription and set it to property procedures
                        oRefillRequest.SetRefillRequestData()

                        'drug Info
                        lbl_LastFillDate.Text = oRefillRequest.DTlastdate
                        lbl_MDLastFillDate.Text = oRefillRequest.MDDTlastdate
                        lbl_Duration.Text = If(oRefillRequest.Duration = "", "", oRefillRequest.Duration)
                        lbl_MDDuration.Text = If(oRefillRequest.MDDuration = "", "", oRefillRequest.MDDuration)

                        'assign values to Patient information labels from orefillrequest property procedures
                        lbl_Patient.Text = oRefillRequest.PatientName
                        lbl_PatDOB.Text = oRefillRequest.PatientDOB
                        'lbl_PatGender.Text = oRefillRequest.PatientGender
                        Select Case oRefillRequest.PatientGender
                            Case "M"
                                lbl_PatGender.Text = "Male"
                            Case "Male"
                                lbl_PatGender.Text = "Male"
                            Case "F"
                                lbl_PatGender.Text = "Female"
                            Case "Female"
                                lbl_PatGender.Text = "Female"
                            Case Else
                                lbl_PatGender.Text = "Other"
                        End Select
                        lbl_PatientAddress.Text = oRefillRequest.PatientAddress
                        lblPatientAddress2.Text = oRefillRequest.PatientAddress2
                        lbl_PatientPhoneNo.Text = oRefillRequest.PatientPhone
                        lbl_CityName.Text = oRefillRequest.PatientCity
                        lbl_StateName.Text = oRefillRequest.PatientState
                        lbl_ZIPCode.Text = oRefillRequest.PatientZip

                        'assign values to Pharmacy information labels from orefillrequest property procedures
                        lbl_Pharmacy.Text = oRefillRequest.PharmacyName
                        lbl_PharmacyAddress.Text = oRefillRequest.PharmacyAddress
                        lblPharmacyAddress2.Text = oRefillRequest.PharmacyAddress2
                        lbl_PharmacyPhoneNo.Text = oRefillRequest.PharmacyPhone
                        lbl_PharamcyCity.Text = oRefillRequest.PharmacyCity
                        lbl_PharmacyState.Text = oRefillRequest.PharmacyState
                        lbl_PharmacyZip.Text = oRefillRequest.PharmacyZip

                        lbl_PharmacyFax.Text = oRefillRequest.PharmacyFax
                        lbl_PharmacyNPI.Text = oRefillRequest.PharmacyNPI


                        'assign values to Provider (means Prescriber) information labels from orefillrequest property procedures
                        lbl_Provider.Text = oRefillRequest.ProviderName
                        lbl_ProviderAddress.Text = oRefillRequest.ProviderAddress
                        lblProviderAddress2.Text = oRefillRequest.ProviderAddress2
                        lbl_PrPhone.Text = oRefillRequest.ProviderPhone
                        lbl_ProviderCity.Text = oRefillRequest.ProviderCity
                        lbl_ProviderState.Text = oRefillRequest.ProviderState
                        lbl_ProviderZIP.Text = oRefillRequest.ProviderZip

                        lbl_ProviderFax.Text = oRefillRequest.ProviderFax
                        lbl_PrescriberNPI.Text = oRefillRequest.PrescriberNPI
                        'pnl_RefillInfo.Visible = True
                        '---------------------------------------------------------------
                        ''''------
                    End If
                End If

            End If




        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(ogloInterface) Then
                ogloInterface.Dispose()
                ogloInterface = Nothing
            End If
        End Try
    End Sub


#End Region


    Private Sub C1RefillList_CellButtonClick(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1RefillList.CellButtonClick

        Dim oRefillRequest As gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest
        Try
            initializedata(e.Row, e.Col)

            Select Case e.Col
                Case COL_Approve
                    'MessageBox.Show("Approve " & e.Row.ToString)
                    If sMessageID <> "" Then
                        '        If nPatientId_RefillReq <> gnPatientID Then
                        '            'call DoctorsDashboard function to set the global parameters to the PatientId_RefillReq
                        '            CType(Me.MdiParent, MainMenu).ShowDefaultPatientDetails(nPatientId_RefillReq)
                        '        End If
                        '        oRefillRequest = New RefillRequest
                        '        oRefillRequest.GetPrescriberPharmacyID(sRxReferenceNumber, dtdatereceived)
                        '        frmRx = New frmPrescription(nRxTransactionId, strQuantity, sRxReferenceNumber, dtdatereceived, oRefillRequest.ProviderID, oRefillRequest.PharmacyID)
                        '        frmRx.ShowInTaskbar = False
                        '        frmRx.MdiParent = Me.MdiParent
                        '        frmRx.Show()
                        Dim tlsender As ToolStripMenuItem = cntListmenuStrip.Items(0)
                        StripItem_Click(tlsender, e)
                    Else
                        MessageBox.Show("Select a refill request", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

                    ' ''when deny btn is clicked and without doing any operations directly Approve btn is clicked then we call the code written on the close event of the Deny Control
                    'pnlProcessRefill.Visible = False
                    'pnlProcessRefill.SendToBack()
                    'pnl_RefillInfo.Visible = True

                Case COL_Deny
                    'MessageBox.Show("Deny " & e.Row.ToString)

                    DenySelectedRefillRequest()


                Case COL_Cancel
                    'pnlProcessRefill.Top = pnl_RefillInfo.Top
                    pnl_Grid.Dock = DockStyle.Fill
                    pnl_Grid.BringToFront()
                    pnlProcessRefill.Visible = False
                    pnl_RefillInfo.Visible = False
                    'pnlProcessRefill.BringToFront()
                    lblDenialReasoncode.Visible = True
                    cmbDenialReasonCode.Visible = True
                    lblMedicationItemName.Text = strMedicationName

                    If MessageBox.Show("Are you sure you want to cancel this refill request? It will be removed from the list and will no longer be accessed.", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then

                        eRefillStatus = RefillStatus.eCancel
                        oRefillRequest = New gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest
                        oRefillRequest.UpdateStatusCancel(sRxReferenceNumber, sMessageID)

                        '\\GetPendingRefillRequest()
                        GetPendingRefillRequestFORC1()
                    End If

            End Select

        Catch ex As PrescriptionException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
    End Sub

    Public Sub DenySelectedRefillRequest()
        If sMessageID <> "" Then

            Dim _refreqNDCCode As String = ""
            If Not IsNothing(C1RefillList.GetData(C1RefillList.RowSel, COL_MDProductCode)) Then
                _refreqNDCCode = C1RefillList.GetData(C1RefillList.RowSel, COL_MDProductCode)
            End If

            If _refreqNDCCode = "" Then
                MessageBox.Show("The refill request will not be approved/denied since the drug is sent without an NDCCode, the refill request needs to be cancelled.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            eRefillStatus = RefillStatus.eDeny

            blnbtnstatus = True
            spitpanelprocessrefill()
            btnupdown.BackgroundImage = Global.gloEMR.My.Resources.Down    ''ImgUpButton
            btnupdown.BackgroundImageLayout = ImageLayout.Center

            pnl_Grid.Dock = DockStyle.Fill
            pnl_Grid.BringToFront()
            pnlProcessRefill.Visible = True
            pnlProcessRefill.Dock = DockStyle.Bottom
            pnl_RefillInfo.Visible = False
            pnlwbBrowser.Visible = False
            txtNotes.Text = ""
            lblDenialReasoncode.Visible = True
            cmbDenialReasonCode.Visible = True
            lblMedicationItemName.Text = strMedicationName
        Else
            MessageBox.Show("Select a refill request", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Public Function GetPendingRefillRequestFORC1()
        nPatientId_RefillReq = 0
        strMedicationName = ""
        nRxTransactionId = ""
        strQuantity = ""
        sRxReferenceNumber = ""
        sMessageID = ""
        sPharmacyID = ""
        Dim dtPendingDetails As DataTable
        Dim oRefillRequest As New gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest
        Try
            RemoveDrugGridControl()
            'refresh the Popup grid of dashboard so that if there are new items then those will be shown in the dgRefillList grid also.
            Dim myMenu As MainMenu = CType(Me.MdiParent, MainMenu)
            If (IsNothing(myMenu) = False) Then
                myMenu.FillMessage()
            End If

            dtPendingDetails = New DataTable

            Dim mynode As TreeNode = Nothing
            If (IsNothing(trvPrescribers) = False) Then
                mynode = CType(trvPrescribers.SelectedNode, TreeNode)
            End If

            If IsNothing(mynode) = False Then
                'validation if the selected node is not rootnode
                If mynode.Text = "Prescribers" Then
                    'C1RefillList.Clear()
                    C1RefillList.DataSource = Nothing
                    pnl_RefillInfo.Height = 0
                    btnupdown.BackgroundImage = Global.gloEMR.My.Resources.Down
                    btnupdown.BackgroundImageLayout = ImageLayout.Center
                Else 'root node is not selected

                    'pass the appropriate provider id that is stored in the tag value to the function
                    dtPendingDetails = oRefillRequest.GetAllPendingRefills(trvPrescribers.SelectedNode.Tag)

                    If Not IsNothing(dtPendingDetails) Then

                        If dtPendingDetails.Rows.Count > 0 Then
                            '\\dgRefillList.DataSource = dtPendingDetails

                            SetClgrid()
                            setdatatoC1(dtPendingDetails)
                            ''pnl_RefillInfo.Height = intRefillpanelheight
                            blnbtnstatus = True
                            btnupdown.BackgroundImage = Global.gloEMR.My.Resources.UP
                            btnupdown.BackgroundImageLayout = ImageLayout.Center
                            'setDataGridStyle(dtPendingDetails)
                            'dgRefillList.Select(0)
                            'showDetails()
                            showDetailsC1()
                            spitpanelprocessrefill()
                            pnl_RefillInfo.Show()
                        Else
                            RefreshRefillDetails()
                            'pnl_RefillInfo.Height = 0
                            'btnupdown.BackgroundImage = Global.gloEMR.My.Resources.ImgDownButton
                            'btnupdown.BackgroundImageLayout = ImageLayout.Center
                            Onepanelprocessrefill()
                            blnbtnstatus = False
                            btnupdown.BackgroundImage = Global.gloEMR.My.Resources.Down '' ImgUpButton
                            btnupdown.BackgroundImageLayout = ImageLayout.Center
                        End If

                    Else ' there are no pending refill request populated in the datatable
                        'C1RefillList.Clear()
                        C1RefillList.DataSource = Nothing
                        C1RefillList.Refresh()
                        SetClgrid()
                        RefreshRefillDetails()
                        pnl_Grid.Dock = DockStyle.Fill
                        pnlProcessRefill.Visible = False
                        pnl_RefillInfo.Visible = False
                        'btnupdown.BackgroundImage = Global.gloEMR.My.Resources.ImgDownButton
                        'btnupdown.BackgroundImageLayout = ImageLayout.Center
                        Onepanelprocessrefill()
                        blnbtnstatus = False
                        btnupdown.BackgroundImage = Global.gloEMR.My.Resources.Down '' ImgUpButton
                        btnupdown.BackgroundImageLayout = ImageLayout.Center
                    End If
                End If

            Else

                dgRefillList.DataSource = Nothing
                pnl_RefillInfo.Height = 0
                btnupdown.BackgroundImage = Global.gloEMR.My.Resources.Down
                btnupdown.BackgroundImageLayout = ImageLayout.Center
            End If

            '''''''code commented for solving bug 5525
            'If pnlProcessRefill.Visible = True Then
            '    pnlProcessRefill.Visible = False
            '    pnlProcessRefill.SendToBack()
            'End If
        Catch ex As PrescriptionException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Throw ex
        Finally
            If Not IsNothing(oRefillRequest) Then
                oRefillRequest.Dispose()
                oRefillRequest = Nothing
            End If
        End Try
        Return Nothing
    End Function

    Private Function spitpanelprocessrefill()
         pnl_RefillInfo.Height = 0
        Return Nothing
    End Function

    Private Function Onepanelprocessrefill()

        pnl_Grid.Dock = DockStyle.Fill
        pnl_Grid.Height = 820 '825
        'pnl_RefillInfo.Height = 25
        Return Nothing
    End Function
    Private Function myWindowTopHeight(myControl As Control)
        Dim myParentControl As Control = myControl.Parent
        If (myParentControl Is Nothing) Then
            Return myControl.Height
        Else
            Return myWindowTopHeight(myParentControl)
        End If
    End Function
    Private Function ShowCntrlAsPerResolution(Optional ByVal isfromLoad As Boolean = False)
        Try

            C1RefillList.AutoResize = False

            Dim temp As Integer = 0
            Dim myCount As Integer = C1RefillList.Rows.Count()
            If (myCount > 5) Then
                myCount = 5
            End If
            Dim myHeight As Integer = C1RefillList.Rows(0).HeightDisplay
            If isfromLoad = True Then
                temp = myCount * myHeight  ''if from load event set to 180, rest all places set to 120
            Else
                temp = myCount * myHeight
            End If
            '  C1RefillList.Height = temp
            ' pnl_Grid.Height = temp
            Dim myTopWindowHeight As Integer = myWindowTopHeight(pnlwbBrowser)
            If (myTopWindowHeight > 400) Then
                pnlwbBrowser.Height = myTopWindowHeight - 350
            Else
                pnlwbBrowser.Height = 50
            End If

            'If (temp > C1RefillList.Height) And (C1RefillList.Height > 0) Then
            '    temp = C1RefillList.Height
            'End If



            'pnl_Grid.Height = C1RefillList.Height
            Dim thisCount As Integer = 0
            Do
                If (pnl_Grid.Height < temp) Then
                    Dim revisedheight As Integer = temp - pnl_Grid.Height
                    If (revisedheight > (pnlwbBrowser.Height - 50)) Then
                        pnlwbBrowser.Height = 50
                        Exit Do
                    Else
                        pnlwbBrowser.Height = pnlwbBrowser.Height - (temp - pnl_Grid.Height)
                    End If

                End If
                If (pnl_Grid.Height >= temp) Then
                    Exit Do
                Else
                    thisCount = thisCount + 1
                    If (thisCount = 5) Then
                        Exit Do
                    End If
                End If
            Loop

            pnlwbBrowser.Location = New System.Drawing.Point(0, pnl_Grid.Height)
            pnl_Grid.Location = New System.Drawing.Point(0, 0)
            'Dim strMsg = New System.Text.StringBuilder
            'Do While True
            '    pnlwbBrowser.Location = New System.Drawing.Point(0, pnl_Grid.Height)
            '    pnl_Grid.Location = New System.Drawing.Point(0, 0)
            '    Exit Do
            '    'strMsg = New System.Text.StringBuilder

            '    'strMsg.Append("Panel grid Temp " & temp.ToString & vbCrLf)
            '    'strMsg.Append("Panel grid visible " & pnl_Grid.Visible & vbCrLf)
            '    'strMsg.Append("Panel grid size " & pnl_Grid.Size.ToString() & vbCrLf)
            '    'strMsg.Append("Panel grid location " & pnl_Grid.Location.ToString & vbCrLf)
            '    'strMsg.Append("Panel grid AutoSize " & pnl_Grid.AutoSize.ToString & vbCrLf)
            '    'strMsg.Append("Panel grid AutoSizeMode " & pnl_Grid.AutoSizeMode.ToString & vbCrLf)
            '    'strMsg.Append("Panel grid PreferredSize " & pnl_Grid.PreferredSize.ToString & vbCrLf)
            '    'strMsg.Append("Panel grid GetPreferredSize " & pnl_Grid.GetPreferredSize(pnl_Grid.Size).ToString & vbCrLf)

            '    'strMsg.Append("Panel webbrowser visible " & pnlwbBrowser.Visible & vbCrLf)
            '    'strMsg.Append("Panel webbrowser size " & pnlwbBrowser.Size.ToString() & vbCrLf)
            '    'strMsg.Append("Panel webbrowser location " & pnlwbBrowser.Location.ToString & vbCrLf)
            '    'strMsg.Append("Panel webbrowser AutoSize " & pnlwbBrowser.AutoSize.ToString & vbCrLf)
            '    'strMsg.Append("Panel webbrowser AutoSizeMode " & pnlwbBrowser.AutoSizeMode.ToString & vbCrLf)
            '    'strMsg.Append("Panel webbrowser PreferredSize " & pnlwbBrowser.PreferredSize.ToString & vbCrLf)
            '    'strMsg.Append("Panel webbrowser GetPreferredSize " & pnlwbBrowser.GetPreferredSize(pnl_Grid.Size).ToString & vbCrLf)

            '    'MessageBox.Show("showDetailsC1() " & strMsg.ToString)
            '    Dim tempBrowserHeight As Integer = pnlwbBrowser.Height
            '    If pnl_Grid.Height < temp Then
            '        pnlwbBrowser.Height = pnlwbBrowser.Height - (temp - pnl_Grid.Height)
            '    Else
            '        Exit Do
            '    End If
            '    pnlwbBrowser.Location = New System.Drawing.Point(0, pnl_Grid.Height)
            '    pnl_Grid.Location = New System.Drawing.Point(0, 0)
            '    If (pnlwbBrowser.Height = tempBrowserHeight) Then
            '        Exit Do
            '    End If
            '    'strMsg = New System.Text.StringBuilder

            '    'strMsg.Append("Panel grid Temp " & temp.ToString & vbCrLf)
            '    'strMsg.Append("Panel grid visible " & pnl_Grid.Visible & vbCrLf)
            '    'strMsg.Append("Panel grid size " & pnl_Grid.Size.ToString() & vbCrLf)
            '    'strMsg.Append("Panel grid location " & pnl_Grid.Location.ToString & vbCrLf)
            '    'strMsg.Append("Panel grid AutoSize " & pnl_Grid.AutoSize.ToString & vbCrLf)
            '    'strMsg.Append("Panel grid AutoSizeMode " & pnl_Grid.AutoSizeMode.ToString & vbCrLf)
            '    'strMsg.Append("Panel grid PreferredSize " & pnl_Grid.PreferredSize.ToString & vbCrLf)
            '    'strMsg.Append("Panel grid GetPreferredSize " & pnl_Grid.GetPreferredSize(pnl_Grid.Size).ToString & vbCrLf)

            '    'strMsg.Append("Panel webbrowser visible " & pnlwbBrowser.Visible & vbCrLf)
            '    'strMsg.Append("Panel webbrowser size " & pnlwbBrowser.Size.ToString() & vbCrLf)
            '    'strMsg.Append("Panel webbrowser location " & pnlwbBrowser.Location.ToString & vbCrLf)
            '    'strMsg.Append("Panel webbrowser AutoSize " & pnlwbBrowser.AutoSize.ToString & vbCrLf)
            '    'strMsg.Append("Panel webbrowser AutoSizeMode " & pnlwbBrowser.AutoSizeMode.ToString & vbCrLf)
            '    'strMsg.Append("Panel webbrowser PreferredSize " & pnlwbBrowser.PreferredSize.ToString & vbCrLf)
            '    'strMsg.Append("Panel webbrowser GetPreferredSize " & pnlwbBrowser.GetPreferredSize(pnl_Grid.Size).ToString & vbCrLf)

            '    'MessageBox.Show("showDetailsC1() " & strMsg.ToString)
            'Loop


            'If pnl_Grid.Height < temp Then
            '    pnlwbBrowser.Height = pnlwbBrowser.Height - (temp - pnl_Grid.Height)

            '    pnl_Grid.Height = 70
            '    pnlwbBrowser.Location = New System.Drawing.Point(0, pnl_Grid.Height)
            '    pnl_Grid.Location = New System.Drawing.Point(0, 0)
            'End If

            'strMsg = New System.Text.StringBuilder
            'strMsg.Append("Panel grid visible " & pnl_Grid.Visible & vbCrLf)
            'strMsg.Append("Panel grid size " & pnl_Grid.Size.ToString() & vbCrLf)
            'strMsg.Append("Panel grid location " & pnl_Grid.Location.ToString & vbCrLf)
            'strMsg.Append("Panel grid AutoSize " & pnl_Grid.AutoSize.ToString & vbCrLf)
            'strMsg.Append("Panel grid AutoSizeMode " & pnl_Grid.AutoSizeMode.ToString & vbCrLf)
            'strMsg.Append("Panel grid PreferredSize " & pnl_Grid.PreferredSize.ToString & vbCrLf)
            'strMsg.Append("Panel grid GetPreferredSize " & pnl_Grid.GetPreferredSize(pnl_Grid.Size).ToString & vbCrLf)

            'strMsg.Append("Panel webbrowser visible " & pnlwbBrowser.Visible & vbCrLf)
            'strMsg.Append("Panel webbrowser size " & pnlwbBrowser.Size.ToString() & vbCrLf)
            'strMsg.Append("Panel webbrowser location " & pnlwbBrowser.Location.ToString & vbCrLf)
            'strMsg.Append("Panel webbrowser AutoSize " & pnlwbBrowser.AutoSize.ToString & vbCrLf)
            'strMsg.Append("Panel webbrowser AutoSizeMode " & pnlwbBrowser.AutoSizeMode.ToString & vbCrLf)
            'strMsg.Append("Panel webbrowser PreferredSize " & pnlwbBrowser.PreferredSize.ToString & vbCrLf)
            'strMsg.Append("Panel webbrowser GetPreferredSize " & pnlwbBrowser.GetPreferredSize(pnl_Grid.Size).ToString & vbCrLf)

            'MessageBox.Show("showDetailsC1() " & strMsg.ToString)
        Catch ex As Exception

        End Try
        Return Nothing
    End Function
    Private Function showDetailsC1()
        Dim oRefillRequest As gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest
        Dim ogloInterface As gloSureScript.gloSureScriptInterface = Nothing
        Try

            strMedicationName = C1RefillList.Item(1, 8) '5

            strPatientName = C1RefillList.Item(1, 5) '2)

            'Case 2297
            ' strPatientLastName = dgRefillList.Item(0, 15)
            Dim retval As String() = SplitPatientName(C1RefillList.Item(1, 5)) '15))
            'Dim strPatFirstName As String = retval(0)
            'Dim strPatLastName As String = retval(1)
            Select Case retval.Length
                Case 3
                    strPatientName = retval(0)
                    strPatientMiddleName = retval(1)
                    strPatientLastName = retval(2)
                Case 2
                    strPatientName = retval(0)
                    strPatientLastName = retval(1)
            End Select

            'Case 2297

            strPatientGender = C1RefillList.Item(1, 6) '3)

            strPatientDOB = C1RefillList.Item(1, 7) '4)

            '1st col is Rxtransactionid
            nRxTransactionId = C1RefillList.Item(1, 4) '1)

            '5th col is Quantity
            strQuantity = C1RefillList.Item(1, 9) '6)

            '0th col is Rx Reference number
            sRxReferenceNumber = C1RefillList.Item(1, 3) ' 0)

            '7th col is Daterecieved
            dtdatereceived = C1RefillList.Item(1, 11) '8)

            '9th coloumn is patient id 
            If IsDBNull(C1RefillList.Item(1, 13)) Then '10
                nPatientId_RefillReq = 0
            ElseIf C1RefillList.Item(1, 13).ToString <> "" Then
                nPatientId_RefillReq = C1RefillList.Item(1, 13) '10
            Else
                nPatientId_RefillReq = 0
            End If


            '*********************************************************************

            If Not IsNothing(C1RefillList.Item(1, 16)) Then '13
                'If IsNumeric(dgRefillList.Item(0, 13)) Then
                sMessageID = C1RefillList.Item(1, 16) '13

                'Else
                '    sMessageID = ""
                'End If
            Else
                sMessageID = ""
            End If

            If Not IsNothing(C1RefillList.Item(1, 17)) Then '14
                ' IsNumeric(dgRefillList.Item(0, 14)) Then
                sPharmacyID = C1RefillList.Item(1, 17) '14

                'Else
                '    sPharmacyID = ""
                'End If
            Else
                sPharmacyID = ""
            End If

            ''MD Drug Info  -------
            lblDrugName_Strength_Dosageform.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(1, 27)) & " " & ConvertIfNothingToEmptyString(C1RefillList.GetData(1, 40)) & " " & ConvertIfNothingToEmptyString(C1RefillList.GetData(1, 39))

            lblDrugQuantity.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(1, 43))
            lblDirection.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(1, 34))
            lblDrugNotes.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(1, 35))
            lblRefillQuantity.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(1, 30))
            lblWrittenDate.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(1, 32))
            lblSubstitution.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(1, 46))

            ''------xx------------------
            lbl_Ref_Qlfr.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(1, COL_RefillQualifier))
            lbl_MDRef_Qlfr.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(1, COL_MDRefillsQualifier))


            ''Prescribed Drug Info  -------
            lbl_MPDrugName_Strength_Dosageform.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(1, COL_Medication)) & " " & ConvertIfNothingToEmptyString(C1RefillList.GetData(1, COL_Drugstreangth))
            lbl_MPDrugQuantity.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(1, COL_Quantity))
            lbl_MPDirection.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(1, COL_DrugDirection))
            lbl_MPDrugnotes.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(1, COL_Notes))
            lbl_MPRefillQuantity.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(1, COL_RefillQuantity))
            lbl_MPWrittenDate.Text = If(IsNothing(C1RefillList.GetData(1, COL_dtWrittenDate)), "", Format(C1RefillList.GetData(1, COL_dtWrittenDate), "MM/dd/yyyy"))
            lbl_MPSubstitution.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(1, COL_Substitution))
            '----xx-----

            oRefillRequest = New gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest
            'Get Pharmcy and Prescriber and Patient info for selected refillrequest
            oRefillRequest.GetDrugDetailsToRefill(sMessageID, sRxReferenceNumber, nRxTransactionId)

            'Get data from ogloPrescription and set it to property procedures
            oRefillRequest.SetRefillRequestData()


            '' ------------" Load XML"  ------
            If Not IsNothing(oRefillRequest.FileData) Then
                Dim dsViewDataset As New DataSet()
                Dim dtNewRow As DataRow = Nothing

                ogloInterface = New gloSureScript.gloSureScriptInterface
                ogloInterface.LoadtoDataset(dsViewDataset, oRefillRequest.FileData)
                Dim XMLFile As String = ogloInterface.ViewXML(dsViewDataset)
                If XMLFile <> "" Then
                    If Not IsNothing(wbBrowser) Then
                        wbBrowser.Dispose()
                        wbBrowser = Nothing
                    End If
                    wbBrowser = New WebBrowser
                    wbBrowser.Navigate(XMLFile)
                    If pnlwbBrowser.Visible = False Then
                        pnlwbBrowser.Visible = True
                    End If



                    ' pnlwbBrowser.Height = 690
                    pnlwbBrowser.Height = pnlwbBrowser.Parent.Height - 100
                    If (pnlwbBrowser.Height > 690) Then
                        pnlwbBrowser.Height = 690
                    End If
                    pnlwbBrowser.Controls.Add(wbBrowser)
                    wbBrowser.Dock = DockStyle.Fill
                    'pnlwbBrowser.Dock = DockStyle.Fill
                    'pnlwbBrowser.BringToFront()

                    ShowCntrlAsPerResolution()

                End If
                If Not IsNothing(dsViewDataset) Then
                    dsViewDataset.Dispose()
                    dsViewDataset = Nothing
                End If
                ''-------- End ----------------------



                If Not IsNothing(ogloInterface) Then
                    ogloInterface.Dispose()
                    ogloInterface = Nothing
                End If
            End If
            'drug Info
            lbl_LastFillDate.Text = oRefillRequest.DTlastdate
            lbl_MDLastFillDate.Text = oRefillRequest.MDDTlastdate
            lbl_Duration.Text = If(oRefillRequest.Duration = "", "", oRefillRequest.Duration) '& " " & "Days"
            lbl_MDDuration.Text = If(oRefillRequest.MDDuration = "", "", oRefillRequest.MDDuration)

            'assign values to Patient information labels from orefillrequest property procedures
            lbl_Patient.Text = oRefillRequest.PatientName
            lbl_PatDOB.Text = oRefillRequest.PatientDOB
            ' lbl_PatGender.Text = oRefillRequest.PatientGender
            Select Case oRefillRequest.PatientGender
                Case "M"
                    lbl_PatGender.Text = "Male"
                Case "Male"
                    lbl_PatGender.Text = "Male"
                Case "F"
                    lbl_PatGender.Text = "Female"
                Case "Female"
                    lbl_PatGender.Text = "Female"
                Case Else
                    lbl_PatGender.Text = "Other"
            End Select
            lbl_PatientAddress.Text = oRefillRequest.PatientAddress
            strPatientAddress1 = oRefillRequest.PatientAddress
            lblPatientAddress2.Text = oRefillRequest.PatientAddress2 ''--
            strPatientAddress2 = oRefillRequest.PatientAddress2
            lbl_PatientPhoneNo.Text = oRefillRequest.PatientPhone
            strPatientPhone = oRefillRequest.PatientPhone
            lbl_CityName.Text = oRefillRequest.PatientCity
            strPatientCity = oRefillRequest.PatientCity
            lbl_StateName.Text = oRefillRequest.PatientState
            strPatientState = oRefillRequest.PatientState
            lbl_ZIPCode.Text = oRefillRequest.PatientZip
            strPatientZip = oRefillRequest.PatientZip

            'assign values to Pharmacy information labels from orefillrequest property procedures
            lbl_Pharmacy.Text = oRefillRequest.PharmacyName
            lbl_PharmacyAddress.Text = oRefillRequest.PharmacyAddress
            lblPharmacyAddress2.Text = oRefillRequest.PharmacyAddress2
            lbl_PharmacyPhoneNo.Text = oRefillRequest.PharmacyPhone
            lbl_PharamcyCity.Text = oRefillRequest.PharmacyCity
            lbl_PharmacyState.Text = oRefillRequest.PharmacyState
            lbl_PharmacyZip.Text = oRefillRequest.PharmacyZip

            lbl_PharmacyFax.Text = oRefillRequest.PharmacyFax
            lbl_PharmacyNPI.Text = oRefillRequest.PharmacyNPI

            'assign values to Provider (means Prescriber) information labels from orefillrequest property procedures
            lbl_Provider.Text = oRefillRequest.ProviderName
            lbl_ProviderAddress.Text = oRefillRequest.ProviderAddress
            lblProviderAddress2.Text = oRefillRequest.ProviderAddress2
            lbl_PrPhone.Text = oRefillRequest.ProviderPhone
            lbl_ProviderCity.Text = oRefillRequest.ProviderCity
            lbl_ProviderState.Text = oRefillRequest.ProviderState
            lbl_ProviderZIP.Text = oRefillRequest.ProviderZip

            lbl_ProviderFax.Text = oRefillRequest.ProviderFax
            lbl_PrescriberNPI.Text = oRefillRequest.PrescriberNPI

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            If Not IsNothing(ogloInterface) Then
                ogloInterface.Dispose()
                ogloInterface = Nothing
            End If
        End Try
        Return Nothing
    End Function

    Private Sub C1RefillList_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1RefillList.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub C1RefillList_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1RefillList.MouseUp
        Dim oRefillRequest As gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest
        Dim ogloInterface As gloSureScript.gloSureScriptInterface = Nothing

        Try
            Dim point As New Point(e.X, e.Y)
            Dim hti As C1.Win.C1FlexGrid.HitTestInfo = C1RefillList.HitTest(point)
            RemoveDrugGridControl()
            If hti.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.Cell Then
                'dgPatient.CurrentCell = New DataGridCell(hti.Row, hti.Column)
                ' ''
                'If C1RefillList.CurrentRowIndex >= 0 Then
                '    C1RefillList.UnSelect(C1RefillList.CurrentRowIndex)
                'End If
                'C1RefillList.CurrentRowIndex = hti.Row
                'C1RefillList.Select(hti.Row)


                C1RefillList.RowSel = hti.Row
                C1RefillList.Select(hti.Row, hti.Column)

                'case 2297
                If hti.Row > 0 Then

                    strPatientName = ""
                    strPatientMiddleName = ""
                    strPatientLastName = ""

                    '3rd col is medication name
                    strMedicationName = C1RefillList.Item(hti.Row, 8) '5)



                    strPatientName = C1RefillList.Item(hti.Row, 5)

                    'Case 2297
                    Dim retval As String() = SplitPatientName(C1RefillList.Item(hti.Row, 5)) '15))
                    If retval.Length > 1 Then
                        Select Case retval.Length
                            Case 3
                                strPatientName = retval(0)
                                strPatientMiddleName = retval(1)
                                strPatientLastName = retval(2)
                            Case 2
                                strPatientName = retval(0)
                                strPatientLastName = retval(1)
                        End Select
                    Else
                        If retval.Length = 0 Then
                            strPatientName = C1RefillList.Item(hti.Row, 5)
                        Else
                            strPatientName = retval(0)
                        End If
                        If strPatientName.Contains("|") Then
                            retval = SplitPatientName_WithPipe(strPatientName)
                            If retval.Length > 1 Then
                                Select Case retval.Length
                                    Case 3
                                        strPatientName = retval(0)
                                        strPatientMiddleName = retval(1)
                                        strPatientLastName = retval(2)
                                    Case 2
                                        strPatientName = retval(0)
                                        strPatientLastName = retval(1)
                                End Select
                            Else
                                strPatientName = retval(0)
                                strPatientLastName = ""
                            End If
                        Else
                            strPatientLastName = ""
                        End If

                    End If


                    strPatientGender = C1RefillList.Item(hti.Row, 6) '3)

                    strPatientDOB = C1RefillList.Item(hti.Row, 7) '4)

                    '1st col is Rxtransactionid
                    If Not IsNothing(C1RefillList.Item(hti.Row, 4)) Then '1
                        nRxTransactionId = C1RefillList.Item(hti.Row, 4)  '1

                    Else
                        nRxTransactionId = ""
                    End If



                    '5th col is Quantity
                    strQuantity = C1RefillList.Item(hti.Row, 9) '6)

                    '0th col is Rx Reference number
                    sRxReferenceNumber = C1RefillList.Item(hti.Row, 3) '0)

                    '7th col is Daterecieved

                    dtdatereceived = C1RefillList.Item(hti.Row, 11) '8)

                    '9th coloumn is patient id 
                    If Not IsNothing(C1RefillList.Item(hti.Row, 13)) Then '10
                        If IsNumeric(C1RefillList.Item(hti.Row, 13)) Then  '10
                            nPatientId_RefillReq = C1RefillList.Item(hti.Row, 13) '10

                        Else
                            nPatientId_RefillReq = 0
                        End If
                    Else
                        nPatientId_RefillReq = 0
                    End If


                    If Not IsNothing(C1RefillList.Item(hti.Row, 16)) Then '13
                        'If IsNumeric(dgRefillList.Item(selectedRowNo, 13)) Then
                        sMessageID = C1RefillList.Item(hti.Row, 16) '13)

                        'Else
                        '    sMessageID = ""
                        'End If
                    Else
                        sMessageID = ""
                    End If

                    If Not IsNothing(C1RefillList.Item(hti.Row, 17)) Then '14
                        'If IsNumeric(dgRefillList.Item(selectedRowNo, 14)) Then
                        sPharmacyID = C1RefillList.Item(hti.Row, 17) '14

                        'Else
                        '    sPharmacyID = ""
                        'End If
                    Else
                        sPharmacyID = ""
                    End If

                    C1RefillList.Select(hti.Row, hti.Column)
                    'cntListmenuStrip.Items.Clear()

                    '---------------------------------------------------------------
                    ''MD Drug Info  -------
                    lblDrugName_Strength_Dosageform.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(hti.Row, 27)) & " " & ConvertIfNothingToEmptyString(C1RefillList.GetData(hti.Row, 40)) & " " & ConvertIfNothingToEmptyString(C1RefillList.GetData(hti.Row, 39))
                    lblDrugQuantity.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(hti.Row, 43))
                    lblDirection.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(hti.Row, 34))
                    lblDrugNotes.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(hti.Row, 35))
                    lblRefillQuantity.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(hti.Row, 30))
                    lblWrittenDate.Text = If(IsNothing(C1RefillList.GetData(hti.Row, 32)), "", Format(C1RefillList.GetData(hti.Row, 32), "MM/dd/yyyy"))
                    lblSubstitution.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(hti.Row, 46))

                    ''------xx------------------
                    lbl_Ref_Qlfr.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(hti.Row, COL_RefillQualifier))
                    lbl_MDRef_Qlfr.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(hti.Row, COL_MDRefillsQualifier))


                    ''Prescribed Drug Info  -------
                    lbl_MPDrugName_Strength_Dosageform.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(hti.Row, COL_Medication)) & " " & ConvertIfNothingToEmptyString(C1RefillList.GetData(hti.Row, COL_Drugstreangth))
                    lbl_MPDrugQuantity.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(hti.Row, COL_Quantity))
                    lbl_MPDirection.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(hti.Row, COL_DrugDirection))
                    lbl_MPDrugnotes.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(hti.Row, COL_Notes))
                    lbl_MPRefillQuantity.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(hti.Row, COL_RefillQuantity))
                    lbl_MPWrittenDate.Text = If(IsNothing(C1RefillList.GetData(hti.Row, COL_dtWrittenDate)), "", Format(C1RefillList.GetData(hti.Row, COL_dtWrittenDate), "MM/dd/yyyy"))
                    lbl_MPSubstitution.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(hti.Row, COL_Substitution))
                    '----xx-----


                    oRefillRequest = New gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest
                    'Get Pharmcy and Prescriber and Patient info for selected refillrequest
                    oRefillRequest.GetDrugDetailsToRefill(sMessageID, sRxReferenceNumber, nRxTransactionId)

                    'Get data from ogloPrescription and set it to property procedures
                    oRefillRequest.SetRefillRequestData()


                    If Not IsNothing(oRefillRequest.FileData) Then
                        Dim dsViewDataset As New DataSet()
                        Dim dtNewRow As DataRow = Nothing
                        ogloInterface = New gloSureScript.gloSureScriptInterface
                        ogloInterface.LoadtoDataset(dsViewDataset, oRefillRequest.FileData)
                        Dim XMLFile As String = ogloInterface.ViewXML(dsViewDataset)
                        If XMLFile <> "" Then
                            pnl_RefillInfo.Visible = False
                            If Not IsNothing(wbBrowser) Then
                                wbBrowser.Dispose()
                                wbBrowser = Nothing
                            End If
                            wbBrowser = New WebBrowser
                            wbBrowser.Navigate(XMLFile)
                            'pnlwbBrowser.Height = 690
                            pnlwbBrowser.Height = pnlwbBrowser.Parent.Height - 100
                            If (pnlwbBrowser.Height > 690) Then
                                pnlwbBrowser.Height = 690
                            End If
                            pnlwbBrowser.Controls.Add(wbBrowser)
                            wbBrowser.Dock = DockStyle.Fill
                            'pnlwbBrowser.Dock = DockStyle.Fill
                            'pnlwbBrowser.BringToFront()

                            ShowCntrlAsPerResolution()
                        End If
                        If Not IsNothing(dsViewDataset) Then
                            dsViewDataset.Dispose()
                            dsViewDataset = Nothing
                        End If
                        ''-------- End ----------------------
                    End If




                        'drug Info
                        lbl_LastFillDate.Text = oRefillRequest.DTlastdate
                        lbl_MDLastFillDate.Text = oRefillRequest.MDDTlastdate
                    lbl_Duration.Text = If(oRefillRequest.Duration = "", "", oRefillRequest.Duration)
                    lbl_MDDuration.Text = If(oRefillRequest.MDDuration = "", "", oRefillRequest.MDDuration)

                        'assign values to Patient information labels from orefillrequest property procedures
                        lbl_Patient.Text = oRefillRequest.PatientName
                        lbl_PatDOB.Text = oRefillRequest.PatientDOB

                        Select Case oRefillRequest.PatientGender
                            Case "M"
                                lbl_PatGender.Text = "Male"
                            Case "Male"
                                lbl_PatGender.Text = "Male"
                            Case "F"
                                lbl_PatGender.Text = "Female"
                            Case "Female"
                                lbl_PatGender.Text = "Female"
                            Case Else
                                lbl_PatGender.Text = "Other"
                        End Select

                        'lbl_PatGender.Text = oRefillRequest.PatientGender
                        lbl_PatientAddress.Text = oRefillRequest.PatientAddress
                        strPatientAddress1 = oRefillRequest.PatientAddress
                        lblPatientAddress2.Text = oRefillRequest.PatientAddress2 ''--
                        strPatientAddress2 = oRefillRequest.PatientAddress2
                        lbl_PatientPhoneNo.Text = oRefillRequest.PatientPhone
                        strPatientPhone = oRefillRequest.PatientPhone
                        lbl_CityName.Text = oRefillRequest.PatientCity
                        strPatientCity = oRefillRequest.PatientCity
                        lbl_StateName.Text = oRefillRequest.PatientState
                        strPatientState = oRefillRequest.PatientState
                        lbl_ZIPCode.Text = oRefillRequest.PatientZip
                        strPatientZip = oRefillRequest.PatientZip

                        'assign values to Pharmacy information labels from orefillrequest property procedures
                        lbl_Pharmacy.Text = oRefillRequest.PharmacyName
                        lbl_PharmacyAddress.Text = oRefillRequest.PharmacyAddress
                        lblPharmacyAddress2.Text = oRefillRequest.PharmacyAddress2
                        lbl_PharmacyPhoneNo.Text = oRefillRequest.PharmacyPhone
                        lbl_PharamcyCity.Text = oRefillRequest.PharmacyCity
                        lbl_PharmacyState.Text = oRefillRequest.PharmacyState
                        lbl_PharmacyZip.Text = oRefillRequest.PharmacyZip

                        lbl_PharmacyFax.Text = oRefillRequest.PharmacyFax
                        lbl_PharmacyNPI.Text = oRefillRequest.PharmacyNPI
                        'assign values to Provider (means Prescriber) information labels from orefillrequest property procedures
                        lbl_Provider.Text = oRefillRequest.ProviderName
                        lbl_ProviderAddress.Text = oRefillRequest.ProviderAddress
                        lblProviderAddress2.Text = oRefillRequest.ProviderAddress2
                        lbl_PrPhone.Text = oRefillRequest.ProviderPhone
                        lbl_ProviderCity.Text = oRefillRequest.ProviderCity
                        lbl_ProviderState.Text = oRefillRequest.ProviderState
                        lbl_ProviderZIP.Text = oRefillRequest.ProviderZip

                        lbl_ProviderFax.Text = oRefillRequest.ProviderFax
                        lbl_PrescriberNPI.Text = oRefillRequest.PrescriberNPI
                        'pnlProcessRefill.Visible = True
                        pnl_RefillInfo.Visible = True
                        'pnl_RefillInfo.BringToFront()
                        'pnl_Grid.SendToBack()
                        'pnl_Grid.Visible = False
                        'pnl_Grid.Dock = DockStyle.Bottom

                        '---------------------------------------------------------------

                        ''call the AddMenu function only when there is rows(data present) in the dgRefillList datagrid
                        'AddMenu()
                        C1RefillList.ContextMenuStrip = cntListmenuStrip
                    End If
                    'case 2297

                End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(ogloInterface) Then
                ogloInterface.Dispose()
                ogloInterface = Nothing
            End If
        End Try
    End Sub

    Private Function initializedata(ByVal row As Int32, ByVal col As Int32)
        Dim oRefillRequest As gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest

        Try
            If row > 0 Then
                strPatientName = ""
                strPatientMiddleName = ""
                strPatientLastName = ""


                '3rd col is medication name
                strMedicationName = C1RefillList.Item(row, 8) '5)

                'Case 2297

                Dim retval As String() = SplitPatientName(C1RefillList.Item(row, 5)) '15))
                If retval.Length > 1 Then
                    Select Case retval.Length
                        Case 3
                            strPatientName = retval(0)
                            strPatientMiddleName = retval(1)
                            strPatientLastName = retval(2)
                        Case 2
                            strPatientName = retval(0)
                            strPatientLastName = retval(1)
                    End Select
                Else
                    If retval.Length = 0 Then
                        strPatientName = C1RefillList.Item(row, 5)
                    Else
                        strPatientName = retval(0)
                    End If
                    If strPatientName.Contains("|") Then
                        retval = SplitPatientName_WithPipe(strPatientName)
                        If retval.Length > 1 Then
                            Select Case retval.Length
                                Case 3
                                    strPatientName = retval(0)
                                    strPatientMiddleName = retval(1)
                                    strPatientLastName = retval(2)
                                Case 2
                                    strPatientName = retval(0)
                                    strPatientLastName = retval(1)
                            End Select
                        Else
                            strPatientName = retval(0)
                            strPatientLastName = ""
                        End If
                    Else
                        strPatientLastName = ""
                    End If

                End If

                strPatientGender = C1RefillList.Item(row, 6) '3)

                strPatientDOB = C1RefillList.Item(row, 7) '4)

                '1st col is Rxtransactionid
                If Not IsNothing(C1RefillList.Item(row, 4)) Then '1
                    nRxTransactionId = C1RefillList.Item(row, 4)  '1

                Else
                    nRxTransactionId = ""
                End If



                '5th col is Quantity
                strQuantity = C1RefillList.Item(row, 9) '6)

                '0th col is Rx Reference number
                sRxReferenceNumber = C1RefillList.Item(row, 3) '0)

                '7th col is Daterecieved

                dtdatereceived = C1RefillList.Item(row, 11) '8)

                '9th coloumn is patient id 
                If Not IsNothing(C1RefillList.Item(row, 13)) Then '10
                    If IsNumeric(C1RefillList.Item(row, 13)) Then  '10
                        nPatientId_RefillReq = C1RefillList.Item(row, 13) '10

                    Else
                        nPatientId_RefillReq = 0
                    End If
                Else
                    nPatientId_RefillReq = 0
                End If


                If Not IsNothing(C1RefillList.Item(row, 16)) Then '13
                    'If IsNumeric(dgRefillList.Item(selectedRowNo, 13)) Then
                    sMessageID = C1RefillList.Item(row, 16) '13)

                    'Else
                    '    sMessageID = ""
                    'End If
                Else
                    sMessageID = ""
                End If

                If Not IsNothing(C1RefillList.Item(row, 17)) Then '14
                    'If IsNumeric(dgRefillList.Item(selectedRowNo, 14)) Then
                    sPharmacyID = C1RefillList.Item(row, 17) '14

                    'Else
                    '    sPharmacyID = ""
                    'End If
                Else
                    sPharmacyID = ""
                End If

                C1RefillList.Select(row, col)

                '---------------------------------------------------------------

                ''MD Drug Info  -------
                lblDrugName_Strength_Dosageform.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(row, 27)) & " " & ConvertIfNothingToEmptyString(C1RefillList.GetData(row, 40)) & " " & ConvertIfNothingToEmptyString(C1RefillList.GetData(row, 39))
                lblDrugQuantity.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(row, 43))
                lblDirection.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(row, 34))
                lblDrugNotes.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(row, 35))
                lblRefillQuantity.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(row, 30))
                lblWrittenDate.Text = If(IsNothing(C1RefillList.GetData(row, 32)), "", Format(C1RefillList.GetData(row, 32), "MM/dd/yyyy"))
                lblSubstitution.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(row, 46))

                ''------xx------------------
                lbl_Ref_Qlfr.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(row, COL_RefillQualifier))
                lbl_MDRef_Qlfr.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(row, COL_MDRefillsQualifier))


                ''Prescribed Drug Info  -------
                lbl_MPDrugName_Strength_Dosageform.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(row, COL_Medication)) & " " & ConvertIfNothingToEmptyString(C1RefillList.GetData(row, COL_Drugstreangth))
                lbl_MPDrugQuantity.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(row, COL_Quantity))
                lbl_MPDirection.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(row, COL_DrugDirection))
                lbl_MPDrugnotes.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(row, COL_Notes))
                lbl_MPRefillQuantity.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(row, COL_RefillQuantity))
                lbl_MPWrittenDate.Text = If(IsNothing(C1RefillList.GetData(row, COL_dtWrittenDate)), "", Format(C1RefillList.GetData(row, COL_dtWrittenDate), "MM/dd/yyyy"))
                lbl_MPSubstitution.Text = ConvertIfNothingToEmptyString(C1RefillList.GetData(row, COL_Substitution))
                '----xx-----



                oRefillRequest = New gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest
                'Get Pharmcy and Prescriber and Patient info for selected refillrequest
                oRefillRequest.GetDrugDetailsToRefill(sMessageID, sRxReferenceNumber, nRxTransactionId)

                'Get data from ogloPrescription and set it to property procedures
                oRefillRequest.SetRefillRequestData()

                'drug Info
                lbl_LastFillDate.Text = oRefillRequest.DTlastdate
                lbl_MDLastFillDate.Text = oRefillRequest.MDDTlastdate
                lbl_Duration.Text = If(oRefillRequest.Duration = "", "", oRefillRequest.Duration)
                lbl_MDDuration.Text = If(oRefillRequest.MDDuration = "", "", oRefillRequest.MDDuration)

                'assign values to Patient information labels from orefillrequest property procedures
                lbl_Patient.Text = oRefillRequest.PatientName
                lbl_PatDOB.Text = oRefillRequest.PatientDOB
                ' lbl_PatGender.Text = oRefillRequest.PatientGender
                Select Case oRefillRequest.PatientGender
                    Case "M"
                        lbl_PatGender.Text = "Male"
                    Case "Male"
                        lbl_PatGender.Text = "Male"
                    Case "F"
                        lbl_PatGender.Text = "Female"
                    Case "Female"
                        lbl_PatGender.Text = "Female"
                    Case Else
                        lbl_PatGender.Text = "Other"
                End Select
                lbl_PatientAddress.Text = oRefillRequest.PatientAddress
                strPatientAddress1 = oRefillRequest.PatientAddress
                lblPatientAddress2.Text = oRefillRequest.PatientAddress2 ''--
                strPatientAddress2 = oRefillRequest.PatientAddress2
                lbl_PatientPhoneNo.Text = oRefillRequest.PatientPhone
                strPatientPhone = oRefillRequest.PatientPhone
                lbl_CityName.Text = oRefillRequest.PatientCity
                strPatientCity = oRefillRequest.PatientCity
                lbl_StateName.Text = oRefillRequest.PatientState
                strPatientState = oRefillRequest.PatientState
                lbl_ZIPCode.Text = oRefillRequest.PatientZip
                strPatientZip = oRefillRequest.PatientZip
                strPatientFax = C1RefillList.Item(row, COL_PatFax)

                'assign values to Pharmacy information labels from orefillrequest property procedures
                lbl_Pharmacy.Text = oRefillRequest.PharmacyName
                strPharmacyName = oRefillRequest.PharmacyName
                lbl_PharmacyAddress.Text = oRefillRequest.PharmacyAddress
                lblPharmacyAddress2.Text = oRefillRequest.PharmacyAddress2
                lbl_PharmacyPhoneNo.Text = oRefillRequest.PharmacyPhone
                lbl_PharamcyCity.Text = oRefillRequest.PharmacyCity
                lbl_PharmacyState.Text = oRefillRequest.PharmacyState
                lbl_PharmacyZip.Text = oRefillRequest.PharmacyZip

                lbl_PharmacyFax.Text = oRefillRequest.PharmacyFax
                lbl_PharmacyNPI.Text = oRefillRequest.PharmacyNPI

                'assign values to Provider (means Prescriber) information labels from orefillrequest property procedures
                lbl_Provider.Text = oRefillRequest.ProviderName
                lbl_ProviderAddress.Text = oRefillRequest.ProviderAddress
                lblProviderAddress2.Text = oRefillRequest.ProviderAddress2
                lbl_PrPhone.Text = oRefillRequest.ProviderPhone
                lbl_ProviderCity.Text = oRefillRequest.ProviderCity
                lbl_ProviderState.Text = oRefillRequest.ProviderState
                lbl_ProviderZIP.Text = oRefillRequest.ProviderZip


                lbl_ProviderFax.Text = oRefillRequest.ProviderFax
                lbl_PrescriberNPI.Text = oRefillRequest.PrescriberNPI

                pnl_RefillInfo.Visible = True

                '---------------------------------------------------------------

                ''call the AddMenu function only when there is rows(data present) in the dgRefillList datagrid
                'AddMenu()
                C1RefillList.ContextMenuStrip = cntListmenuStrip
            End If
        Catch ex As Exception

        End Try
        Return Nothing
    End Function


    Private Sub InstringSearch()
        Try
            If Dv_Search Is Nothing Then
                Me.Cursor = Cursors.[Default]
                Exit Sub
            End If

            Dim COL_RxReferenceNumber As Byte = 3
            Dim COL_RxTransactionId As Byte = 4
            Dim COL_PatientName As Byte = 5
            Dim COL_PatientGender As Byte = 6
            Dim COL_PatientDOB As Byte = 7
            Dim COL_Medication As Byte = 8
            Dim COL_Quantity As Byte = 9
            Dim COL_PrescriptionDate As Byte = 10
            Dim COL_DateReceived As Byte = 11
            Dim COL_RefillQuantity As Byte = 12
            Dim COL_PatientId As Byte = 13
            Dim COL_LastFillDate As Byte = 14
            Dim COL_RefillQualifier As Byte = 15
            Dim COL_MessageID As Byte = 16
            Dim COL_PharmacyID As Byte = 17
            Dim COL_PatientLastName As Byte = 18

            Dim str As String = ""
            ' Dim rowid As Byte
            Dim strSearchArray As String()
            Dim strexpr As String = ""
            str = txtSearch.Text

            str = str.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "").Replace("*", "")

            If str.Trim() <> "" Then
                strSearchArray = str.Split(","c)
                Dim strSearch As String = ""
                If strSearchArray.Length = 1 Then
                    strSearch = strSearchArray(0)
                    'Dv_Search.RowFilter = Dv_Search.Table.Columns(COL_RxTransactionId).ColumnName & " Like '%" & strSearch & "%' OR " & Dv_Search.Table.Columns(COL_PatientName).ColumnName & " Like '%" & strSearch & "%' OR " & Dv_Search.Table.Columns(COL_PatientGender).ColumnName & " Like '%" & strSearch & "%' OR " & Dv_Search.Table.Columns(COL_PatientDOB).ColumnName & " Like '%" & strSearch & "%' OR " & Dv_Search.Table.Columns(COL_Medication).ColumnName & " Like '%" & strSearch & "%' OR " & Dv_Search.Table.Columns(COL_PrescriptionDate).ColumnName & " Like '%" & strSearch & "%' OR " & Dv_Search.Table.Columns(COL_Quantity).ColumnName & " Like '%" & strSearch & "%' OR " & Dv_Search.Table.Columns(COL_DateReceived).ColumnName & " Like '%" & strSearch & "%' OR " & Dv_Search.Table.Columns(COL_RefillQuantity).ColumnName & " Like '%" & strSearch & "%' OR " & Dv_Search.Table.Columns(COL_LastFillDate).ColumnName & " Like '%" & strSearch & "%' OR " & Dv_Search.Table.Columns(COL_RefillQualifier).ColumnName & " Like '%" & strSearch & "%'"
                    'Developer:Pradeep
                    'Date:12/26/2011
                    'Bug ID: 17365
                    'Reason:changed column name from qty to Quantity
                    Dv_Search.RowFilter = "Name  Like '%" & strSearch & "%' OR DOB Like '%" & strSearch & "%' OR [Medication Prescribed] Like '%" & strSearch & "%' OR Gender Like '%" & strSearch & "%' OR Quantity Like '%" & strSearch & "%' OR [Rx Date] Like '%" & strSearch & "%' OR [Date Rec.] Like '%" & strSearch & "%' OR [Refill Qty] Like '%" & strSearch & "%' OR [Last Fill] Like '%" & strSearch & "%' OR [Ref. Qualifier]  Like '%" & strSearch & "%'"


                Else
                    Dim dtTemp As DataTable = Nothing
                    For i As Byte = 1 To strSearchArray.Length - 1
                        strSearch = strSearchArray(i)
                        If strSearch.Trim() <> "" Then
                            If i = 1 Then
                                dtTemp = Dv_Search.ToTable()
                                dvNext = dtTemp.Copy().DefaultView
                                dtTemp.Dispose()
                                dtTemp = Nothing
                            Else
                                dtTemp = dvNext.ToTable()
                                dvNext = dtTemp.Copy().DefaultView
                                dtTemp.Dispose()
                                dtTemp = Nothing
                            End If


                            dvNext.RowFilter = dvNext.Table.Columns(COL_RxTransactionId).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_PatientName).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_PatientGender).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_PatientDOB).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_Medication).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_PrescriptionDate).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_Quantity).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_DateReceived).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_RefillQuantity).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_LastFillDate).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_RefillQualifier).ColumnName & " Like '%" & strSearch & "%'"
                        End If
                    Next
                    If Not IsNothing(dtTemp) Then ''disposed as per glo Code optimizer tool in 8000 version
                        dtTemp.Dispose()
                        dtTemp = Nothing
                    End If
                End If

                If strSearch <> "" AndAlso strSearch.Trim() <> "" Then
                    If strSearchArray.Length = 1 Then
                        C1RefillList.DataSource = Dv_Search
                        _VisibleCount = Dv_Search.Count
                    Else
                        C1RefillList.DataSource = dvNext
                        _VisibleCount = dvNext.Count
                    End If
                End If
            Else
                Dv_Search.RowFilter = ""
                C1RefillList.DataSource = Dv_Search
                _VisibleCount = Dv_Search.Count
            End If

            ' C1RefillList.Refresh()

            If txtSearch.Text.Trim() = "" Then
                C1RefillList.DataSource = C1_DataTable
            End If

        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.Search, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub C1RefillList_SelChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1RefillList.SelChange
        If pnlProcessRefill.Visible = True Then
            cmbDenialReasonCode.SelectedIndex = 0
            txtNotes.Text = ""
            pnlProcessRefill.Visible = False
            pnlProcessRefill.SendToBack()
        End If
    End Sub



    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub
End Class
