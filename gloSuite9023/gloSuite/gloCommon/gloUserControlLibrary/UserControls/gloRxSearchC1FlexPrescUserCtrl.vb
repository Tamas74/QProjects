Imports gloEMRGeneralLibrary.gloEMRActors
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports gloEMRGeneralLibrary.gloEMRPrescription
Imports gloEMRGeneralLibrary.gloGeneral

Public Class gloRxSearchC1FlexPrescUserCtrl

    Public Event cntListmenuStripClick(ByVal sender As Object, ByVal e As System.EventArgs)

    Private ReferralCount As Long
    Private GridStatus As Int16
    Private VisitID As Long
    Private tempVisitId As Long
    Private tempdate As DateTime
    Public Dv As DataView
    'used for instring search
    'Public dtTemp As DataTable
    Public dvNext As DataView
    ''''''To get the Patient Count in the DataView after Searching 
    Private _VisibleCount As Int16 = 0
    'used for instring search
    Private _IsValidDrug As Boolean
    Private _RefillDisable As Boolean
    Private _RxBuisnessLayer As RxBusinesslayer
    Private _PatientID As Long
    Public Sub New(ByRef objRxBusinessLayer As RxBusinesslayer, ByVal PatientID As Long)
        MyBase.new()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        _PatientID = PatientID
        _RxBuisnessLayer = objRxBusinessLayer
        Dv = New DataView
        AddMenu()
        SetFlexgridColumns()

        ' Add any initialization after the InitializeComponent() call.
        'Dim vId As Long
        'vId = _Flex.GetData(_Flex.Row, COL_VISITID)
        '_RxBuisnessLayer.FetchPrescriptionforView(vId)

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Property RefillDisable() As Boolean
        Get
            Return _RefillDisable
        End Get
        Set(ByVal value As Boolean)
            _RefillDisable = value
        End Set
    End Property

    Public Property ValidDrug() As Boolean
        Get
            Return _IsValidDrug
        End Get
        Set(ByVal value As Boolean)
            _IsValidDrug = value
        End Set
    End Property
    Public Property RxBuisnessLayerObject() As RxBusinesslayer
        Get
            Return _RxBuisnessLayer
        End Get
        Set(ByVal value As RxBusinesslayer)
            _RxBuisnessLayer = value
        End Set
    End Property

    'nPrescriptionId,nVisitId,nPatientID,dtPrescriptiondate,'Drug','Sig','Amount','Refills', bmaysubstitute
    ', sMedication,'Dosage','Route','Frequency'
    'Duration',dtstartdate,dtEnddate,'Notes','Method',nDrugID


    'Private COL_PRESCRIPTIONID As Integer = 0
    'Private COL_VISITID As Integer = 1
    'Private COL_PATIENTID As Integer = 2
    'Private COL_PRESCRIPTIONDATE As Integer = 3
    'Private COL_DRUG As Integer = 4
    'Private COL_SIG As Integer = 5
    'Private COL_AMOUNT As Integer = 6
    'Private COL_REFILLS As Integer = 7
    'Private COL_MAYSUBSTITUTE As Integer = 8
    'Private COL_MEDICATION As Integer = 9
    'Private COL_DOSAGE As Integer = 10
    'Private COL_ROUTE As Integer = 11
    'Private COL_FREQUENCY As Integer = 12
    'Private COL_DURATION As Integer = 13
    'Private COL_STARTDATE As Integer = 14
    'Private COL_ENDDATE As Integer = 15
    'Private COL_NOTES As Integer = 16
    'Private COL_METHOD As Integer = 17
    'Private COL_DRUGID As Integer = 18
    'Private COL_PROVIDERID As Integer = 19
    'Private COL_CHIEFCOMPLAINTS As Integer = 20
    ''For De-Normalization
    'Private COL_NDCCode As Integer = 21
    'Private COL_NARCOTIC As Integer = 22
    'Private COL_DDid As Integer = 23
    ''For De-Normalization

    Private COL_PRESCRIPTIONID As Integer = 0
    Private COL_VISITID As Integer = 1
    Private COL_PATIENTID As Integer = 2
    Private COL_PRESCRIPTIONDATE As Integer = 3
    Private COL_DRUG_NAME As Integer = 4
    Private COL_DOSAGE As Integer = 5
    Private COL_ROUTE As Integer = 6
    Private COL_FREQUENCY As Integer = 7
    Private COL_DURATION As Integer = 8
    Private COL_SIG As Integer = 9
    Private COL_AMOUNT As Integer = 10
    Private COL_REFILLS As Integer = 11
    Private COL_MAYSUBSTITUTE As Integer = 12
    Private COL_MEDICATION As Integer = 13
    Private COL_STARTDATE As Integer = 14
    Private COL_ENDDATE As Integer = 15
    Private COL_NOTES As Integer = 16
    Private COL_METHOD As Integer = 17
    Private COL_DRUGID As Integer = 18
    Private COL_PROVIDERID As Integer = 19
    Private COL_CHIEFCOMPLAINTS As Integer = 20
    'For De-Normalization
    Private COL_NDCCode As Integer = 21
    Private COL_NARCOTIC As Integer = 22
    Private COL_mpid As Integer = 23
    Private COL_DRUGFORM As Integer = 24
    Private COL_STRENGTHUNIT As Integer = 25

    Private COL_Rx_nPharmacyId As Integer = 26
    Private COL_Rx_sNCPDPID As Integer = 27
    Private COL_Rx_nContactID As Integer = 28
    Private COL_Rx_sName As Integer = 29
    Private COL_Rx_sAddressline1 As Integer = 30
    Private COL_Rx_sAddressline2 As Integer = 31
    Private COL_Rx_sCity As Integer = 32
    Private COL_Rx_sState As Integer = 33
    Private COL_Rx_sZip As Integer = 34
    Private COL_Rx_sEmail As Integer = 35
    Private COL_Rx_sFax As Integer = 36
    Private COL_Rx_sPhone As Integer = 37
    Private COL_Rx_sServiceLevel As Integer = 38
    Private COL_Rx_sPrescriberNotes As Integer = 39
    Private COL_Rx_eRxStatus As Integer = 40
    Private COL_DRUG As Integer = 41


    Private COL_COUNT As Integer = 42

  
    Private Sub SetFlexgridColumns()
        Try


            With _Flex
                .AllowDrop = True
                .AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None

                'C1FlexPrescription.SetCellCheck(1, 1, CheckEnum.Unchecked)
                .Cols(0).Width = 30

                '.Cols.Count = 2
                .ExtendLastCol = True
                '.Tree.Column = 1

                'set properties of c1 grid
                '.Rows.Count = 1
                .Rows.Fixed = 1
                .Cols.Count = COL_COUNT
                .Cols.Fixed = 0
                .Rows(.Rows.Count - 1).Height = 21

                Dim _Width As Single = .Width / 5 'Rx Feild Name change in 6030
                'set column value

                .Cols(COL_PRESCRIPTIONID).Width = 0
                .Cols(COL_VISITID).Width = 0
                .Cols(COL_PATIENTID).Width = 0
                .Cols(COL_PRESCRIPTIONDATE).Width = _Width * 1.1
                .Cols(COL_DRUG_NAME).Width = _Width * 3.5 'Rx Feild Name change in 6030
                .Cols(COL_DRUG).Width = 0 'Rx Feild Name change in 6030
                .Cols(COL_DOSAGE).Width = 0 '_Width * 0.75 '0 'Rx Feild Name change in 6030
                .Cols(COL_ROUTE).Width = 0 '_Width * 0.75 ' 0 'Rx Feild Name change in 6030
                .Cols(COL_FREQUENCY).Width = _Width * 1.2 ' 0
                .Cols(COL_DURATION).Width = _Width * 0.75 ' 0
                .Cols(COL_SIG).Width = 0
                .Cols(COL_AMOUNT).Width = _Width * 1
                .Cols(COL_REFILLS).Width = _Width * 0.75
                .Cols(COL_MAYSUBSTITUTE).Width = _Width
                .Cols(COL_MEDICATION).Width = 0
                .Cols(COL_STARTDATE).Width = 0
                .Cols(COL_ENDDATE).Width = 0
                .Cols(COL_NOTES).Width = 0
                .Cols(COL_METHOD).Width = 0
                .Cols(COL_DRUGID).Width = 0
                .Cols(COL_PROVIDERID).Width = 0
                .Cols(COL_CHIEFCOMPLAINTS).Width = 0

                'For De-Normalization
                .Cols(COL_NDCCode).Width = 0
                .Cols(COL_NARCOTIC).Width = 0
                .Cols(COL_mpid).Width = 0
                'For De-Normalization
                .Cols(COL_DRUGFORM).Width = 0
                .Cols(COL_STRENGTHUNIT).Width = 0

                .Cols(COL_Rx_nPharmacyId).Width = 0
                .Cols(COL_Rx_sNCPDPID).Width = 0
                .Cols(COL_Rx_nContactID).Width = 0
                .Cols(COL_Rx_sName).Width = 0
                .Cols(COL_Rx_sAddressline1).Width = 0
                .Cols(COL_Rx_sAddressline2).Width = 0
                .Cols(COL_Rx_sCity).Width = 0
                .Cols(COL_Rx_sState).Width = 0
                .Cols(COL_Rx_sZip).Width = 0
                .Cols(COL_Rx_sEmail).Width = 0
                .Cols(COL_Rx_sFax).Width = 0
                .Cols(COL_Rx_sPhone).Width = 0
                .Cols(COL_Rx_sServiceLevel).Width = 0
                .Cols(COL_Rx_sPrescriberNotes).Width = 0
                .Cols(COL_Rx_eRxStatus).Width = 0


                'set column header

                .SetData(0, COL_PRESCRIPTIONID, "Prescription Id")
                .SetData(0, COL_VISITID, "Visit Id") ''this is called as Medication but in image it is DrugName
                .SetData(0, COL_PATIENTID, "Patient Id")
                .SetData(0, COL_PRESCRIPTIONDATE, "Prescription Date") 'this is called as Route

                .SetData(0, COL_DRUG_NAME, "Drug")
                .SetData(0, COL_DOSAGE, "Dosage")
                .SetData(0, COL_ROUTE, "Route")
                .SetData(0, COL_FREQUENCY, "Patient Directions") 'Rx Feild Name change in 6030
                .SetData(0, COL_DURATION, "Duration")
                .SetData(0, COL_SIG, "Sig")
                .SetData(0, COL_AMOUNT, "Quantity") ''Rx Feild Name change in 6030
                .SetData(0, COL_REFILLS, "Refills")
                .SetData(0, COL_MAYSUBSTITUTE, "Allow Substitution")
                .SetData(0, COL_MEDICATION, "Medication")
                .SetData(0, COL_STARTDATE, "Start Date")
                .SetData(0, COL_ENDDATE, "End Date")
                .SetData(0, COL_NOTES, "Notes")
                .SetData(0, COL_METHOD, "Method")
                .SetData(0, COL_DRUGID, "Drug ID")
                .SetData(0, COL_PROVIDERID, "Provider ID")
                .SetData(0, COL_CHIEFCOMPLAINTS, "Chief Complaints")

                'For De-Normalization
                .SetData(0, COL_NDCCode, "NDC Code")
                .SetData(0, COL_NARCOTIC, "Narcotic")
                .SetData(0, COL_mpid, "mpid")
                'For De-Normalization
                .SetData(0, COL_DRUGFORM, "Drug Form")
                .SetData(0, COL_STRENGTHUNIT, "StrengthUnit")

                .SetData(0, COL_Rx_nPharmacyId, "PharmacyID")
                .SetData(0, COL_Rx_sNCPDPID, "PH NCPDPID")
                .SetData(0, COL_Rx_nContactID, "PH CONTACT ID")
                .SetData(0, COL_Rx_sName, "PharmacyName")
                .SetData(0, COL_Rx_sAddressline1, "PH ADDR1")
                .SetData(0, COL_Rx_sAddressline2, "PH ADDR2")
                .SetData(0, COL_Rx_sCity, "PH CITY")
                .SetData(0, COL_Rx_sState, "PH STATE")
                .SetData(0, COL_Rx_sZip, "PH ZIP")
                .SetData(0, COL_Rx_sEmail, "PH EMAIL")
                .SetData(0, COL_Rx_sFax, "PH FAX")
                .SetData(0, COL_Rx_sPhone, "PH PHONE")
                .SetData(0, COL_Rx_sServiceLevel, "PHSERVICELEVEL")
                .SetData(0, COL_Rx_sPrescriberNotes, "Prescribernotes")
                .SetData(0, COL_Rx_eRxStatus, "eRxStatus")
                .SetData(0, COL_DRUG, "Drug Name")

                'set visiblity for column 

                .Cols(COL_PRESCRIPTIONID).Visible = False
                .Cols(COL_VISITID).Visible = False ''drug name
                .Cols(COL_PATIENTID).Visible = False '
                .Cols(COL_PRESCRIPTIONDATE).Visible = True
                .Cols(COL_DRUG).Visible = False ''the dispense coloum in image
                .Cols(COL_DRUG_NAME).Visible = True
                .Cols(COL_DOSAGE).Visible = True
                .Cols(COL_ROUTE).Visible = True
                .Cols(COL_ROUTE).Visible = True
                .Cols(COL_FREQUENCY).Visible = True
                .Cols(COL_DURATION).Visible = True
                .Cols(COL_SIG).Visible = False
                .Cols(COL_AMOUNT).Visible = True
                .Cols(COL_REFILLS).Visible = True
                .Cols(COL_MAYSUBSTITUTE).Visible = True
                .Cols(COL_MEDICATION).Visible = False
                .Cols(COL_STARTDATE).Visible = False
                .Cols(COL_ENDDATE).Visible = False
                .Cols(COL_NOTES).Visible = False
                .Cols(COL_METHOD).Visible = False
                .Cols(COL_DRUGID).Visible = False
                .Cols(COL_PROVIDERID).Visible = False
                .Cols(COL_CHIEFCOMPLAINTS).Visible = False

                'For De-Normalization
                .Cols(COL_NDCCode).Visible = False
                .Cols(COL_NARCOTIC).Visible = False
                .Cols(COL_mpid).Visible = False
                'For De-Normalization
                .Cols(COL_DRUGFORM).Visible = False
                .Cols(COL_STRENGTHUNIT).Visible = False

                .Cols(COL_Rx_nPharmacyId).Visible = False
                .Cols(COL_Rx_sNCPDPID).Visible = False
                .Cols(COL_Rx_nContactID).Visible = False
                .Cols(COL_Rx_sName).Visible = False
                .Cols(COL_Rx_sAddressline1).Visible = False
                .Cols(COL_Rx_sAddressline2).Visible = False
                .Cols(COL_Rx_sCity).Visible = False
                .Cols(COL_Rx_sState).Visible = False
                .Cols(COL_Rx_sZip).Visible = False
                .Cols(COL_Rx_sEmail).Visible = False
                .Cols(COL_Rx_sFax).Visible = False
                .Cols(COL_Rx_sPhone).Visible = False
                .Cols(COL_Rx_sServiceLevel).Visible = False
                .Cols(COL_Rx_sPrescriberNotes).Visible = False
                .Cols(COL_Rx_eRxStatus).Visible = False


                ' set column editing properties.

                .Cols(COL_PRESCRIPTIONID).AllowEditing = False
                .Cols(COL_VISITID).AllowEditing = False ''drug name
                .Cols(COL_PATIENTID).AllowEditing = False
                .Cols(COL_PRESCRIPTIONDATE).AllowEditing = False '
                .Cols(COL_DRUG).AllowEditing = False ''the dispense coloum in image
                .Cols(COL_DRUG_NAME).AllowEditing = False
                .Cols(COL_DOSAGE).AllowEditing = False
                .Cols(COL_ROUTE).AllowEditing = False
                .Cols(COL_FREQUENCY).AllowEditing = False
                .Cols(COL_DURATION).AllowEditing = False
                .Cols(COL_SIG).AllowEditing = False
                .Cols(COL_AMOUNT).AllowEditing = False
                .Cols(COL_REFILLS).AllowEditing = False
                .Cols(COL_MAYSUBSTITUTE).AllowEditing = False
                .Cols(COL_MEDICATION).AllowEditing = False
                .Cols(COL_STARTDATE).AllowEditing = False
                .Cols(COL_ENDDATE).AllowEditing = False
                .Cols(COL_NOTES).AllowEditing = False
                .Cols(COL_METHOD).AllowEditing = False
                .Cols(COL_DRUGID).AllowEditing = False
                .Cols(COL_PROVIDERID).AllowEditing = False
                .Cols(COL_CHIEFCOMPLAINTS).AllowEditing = False

                'For De-Normalization
                .Cols(COL_NDCCode).AllowEditing = False
                .Cols(COL_NARCOTIC).AllowEditing = False
                .Cols(COL_mpid).AllowEditing = False
                'For De-Normalization
                .Cols(COL_DRUGFORM).AllowEditing = False
                .Cols(COL_STRENGTHUNIT).AllowEditing = False

                .Cols(COL_Rx_nPharmacyId).AllowEditing = False
                .Cols(COL_Rx_sNCPDPID).AllowEditing = False
                .Cols(COL_Rx_nContactID).AllowEditing = False
                .Cols(COL_Rx_sName).AllowEditing = False
                .Cols(COL_Rx_sAddressline1).AllowEditing = False
                .Cols(COL_Rx_sAddressline2).AllowEditing = False
                .Cols(COL_Rx_sCity).AllowEditing = False
                .Cols(COL_Rx_sState).AllowEditing = False
                .Cols(COL_Rx_sZip).AllowEditing = False
                .Cols(COL_Rx_sEmail).AllowEditing = False
                .Cols(COL_Rx_sFax).AllowEditing = False
                .Cols(COL_Rx_sPhone).AllowEditing = False
                .Cols(COL_Rx_sServiceLevel).AllowEditing = False
                .Cols(COL_Rx_sPrescriberNotes).AllowEditing = False
                .Cols(COL_Rx_eRxStatus).AllowEditing = False


                .Cols(COL_MAYSUBSTITUTE).DataType = System.Type.GetType("String")


            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Public Sub AddMenu()
        Try
            Dim tlstripitem As ToolStripMenuItem
            tlstripitem = New ToolStripMenuItem
            tlstripitem.Text = "Refill"
            tlstripitem.Tag = 1
            tlstripitem.Image = ImagSearchFlex.Images(0)
            tlstripitem.ForeColor = Color.FromArgb(31, 73, 125)
            cntListmenuStrip.Items.Add(tlstripitem)
            'AddHandler tlstripitem.Click, AddressOf cntListmenuStrip_Click
            'tlstripitem.Dispose()
            tlstripitem = Nothing
            cntListmenuStrip.SendToBack()
            Panel2.BringToFront()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub




    Public Sub BindGrid()
        Try

            Dim dt As DataTable
            ReferralCount = 0

            dt = _RxBuisnessLayer.PrvRxInstringSearch(VisitID, _PatientID) ''globalPatient.gnPatientID)
            '''' In dt we Get The Following Columns
            'nPrescriptionId,nVisitId,nPatientID,dtPrescriptiondate,'Drug','Sig','Amount','Refills', bmaysubstitute
            ', sMedication,'Dosage','Route','Frequency'
            'Duration',dtstartdate,dtEnddate,'Notes','Method',nDrugID


            Dv = dt.DefaultView
            'Dim i As Integer


            _Flex.DataSource = dt

            '----------code commented because records were to be sorted by Prescription date & here it use to take the 0th coloumns that was prescription id
            'the logic is handled in sp_ScanPrescription where visitid <> 0 and @flag=2 and we use the orderby Desc clause on prescription date in the end of the query
            'i = _Flex.Col
            'Dim strsort As String
            'strsort = _Flex.GetData(0, i)
            'Dv.Sort = "[" & strsort & "]"
            '------------------------------
            'HideColumns(objPrescriptionDBLayer.DsDataview)
            'referral count holds the current row count
            ReferralCount = Dv.Count - 1

            Call SetFlexgridColumns()
            'End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub gloRxSearchC1FlexPrescUserCtrl_txtSearchFlexGridTextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'objPrescriptionDBLayer.SetRowFilter(Trim(txtRefill.Text))
        Try

            Dim str As String = ""
            Dim rowid As Integer
            'For i = 1 To C1FlexGrdPrescriptionDetails.Rows.Count - 1
            'If Len(Trim(txtsearchDrug.Text)) >= 1 Then

            str = txtSearchFlexGrid.Text
            With _Flex

                rowid = .FindRow(str, 1, 4, False, False, True)
                .Row = rowid
            End With



        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub InstringSearch()
        Try
            If Dv Is Nothing Then
                Me.Cursor = Cursors.[Default]
                Exit Sub
            End If


            Dim nCOL_PRESCRIPTIONDATE As Byte = 3
            Dim nCOL_DRUG As Byte = 41
            Dim nCOL_SIG As Byte = 5
            Dim nCOL_AMOUNT As Byte = 6
            Dim nCOL_REFILLS As Byte = 7
            Dim nCOL_MAYSUBSTITUTE As Byte = 8



            Dim str As String = ""
            'Dim rowid As Byte
            Dim strSearchArray As String()
            str = txtSearchFlexGrid.Text
            'str = str.Replace("[", "") + ""; 
            'str = str.Replace("'", "") + ""; 
            str = str.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "").Replace("*", "")

            If str.Trim() <> "" Then
                strSearchArray = str.Split(","c)
                Dim strSearch As String = ""
                If strSearchArray.Length = 1 Then
                    strSearch = strSearchArray(0)
                    Dv.RowFilter = Dv.Table.Columns(nCOL_PRESCRIPTIONDATE).ColumnName & " Like '%" & strSearch & "%' OR " & Dv.Table.Columns(nCOL_DRUG).ColumnName & " Like '%" & strSearch & "%' OR " & Dv.Table.Columns(nCOL_SIG).ColumnName & " Like '%" & strSearch & "%' OR " & Dv.Table.Columns(nCOL_AMOUNT).ColumnName & " Like '%" & strSearch & "%' OR " & Dv.Table.Columns(nCOL_REFILLS).ColumnName & " Like '%" & strSearch & "%' OR " & Dv.Table.Columns(nCOL_MAYSUBSTITUTE).ColumnName & " Like '%" & strSearch & "%'"
                Else
                    Dim dtTemp As DataTable = Nothing
                    For i As Byte = 1 To strSearchArray.Length - 1
                        strSearch = strSearchArray(i)
                        If strSearch.Trim() <> "" Then
                            'If i = 1 Then
                            '    dtTemp = Dv.ToTable()
                            '    dvNext = dtTemp.DefaultView
                            'Else
                            '    dtTemp = dvNext.ToTable()
                            '    dvNext = dtTemp.DefaultView
                            'End If
                            If i = 1 Then
                                dtTemp = Dv.ToTable()
                                dvNext = dtTemp.Copy().DefaultView
                                dtTemp.Dispose()
                                dtTemp = Nothing
                            Else
                                dtTemp = dvNext.ToTable()
                                dvNext = dtTemp.Copy().DefaultView
                                dtTemp.Dispose()
                                dtTemp = Nothing
                            End If

                            dvNext.RowFilter = dvNext.Table.Columns(nCOL_PRESCRIPTIONDATE).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(nCOL_DRUG).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(nCOL_SIG).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(nCOL_AMOUNT).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(nCOL_REFILLS).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(nCOL_MAYSUBSTITUTE).ColumnName & " Like '%" & strSearch & "%'"
                        End If
                    Next
                    If Not IsNothing(dtTemp) Then ''disposed as per glo Code optimizer tool in 8000 version
                        dtTemp.Dispose()
                        dtTemp = Nothing
                    End If
                End If

                If strSearch <> "" AndAlso strSearch.Trim() <> "" Then
                    If strSearchArray.Length = 1 Then
                        _Flex.DataSource = Dv
                        _VisibleCount = Dv.Count
                    Else
                        _Flex.DataSource = dvNext
                        _VisibleCount = dvNext.Count
                    End If
                End If
            Else
                Dv.RowFilter = ""
                _Flex.DataSource = Dv
                _VisibleCount = Dv.Count
            End If

            '_Flex.Selection.Clear(C1.Win.C1FlexGrid.ClearFlags.All)
            _Flex.Refresh()

            If txtSearchFlexGrid.Text.Trim() = "" Then
                'To select the the Row according to PatientID 
                For i As Byte = 0 To _Flex.Rows.Count - 1
                    'compare the returned value with the binding value and show selected.. 

                    'If _patientid = System.Convert.ToInt64(dgPatientView.Rows(i).Cells(0).Value) AndAlso txtSearch.Text <> "" Then

                    '    dgPatientView.Rows(i).Selected = True
                    '    '_patientid = _selectedpatientid; 
                    '    _selectedpatientid = _patientid
                    '    dgPatientView.Rows(i).Visible = True
                    '    dgPatientView.FirstDisplayedScrollingRowIndex = i
                    '    Exit For
                    'ElseIf _selectedpatientid = System.Convert.ToInt64(dgPatientView.Rows(i).Cells(0).Value) AndAlso txtSearch.Text = "" Then

                    '    dgPatientView.Rows(i).Selected = True
                    '    '_patientid = _selectedpatientid; 
                    '    _patientid = _selectedpatientid
                    '    dgPatientView.Rows(i).Visible = True
                    '    dgPatientView.FirstDisplayedScrollingRowIndex = i
                    '    Exit For

                    'End If
                    'for (int i = 0; i <= dgpatientview.rowcount - 1; i++) 
                Next
            End If
            SetFlexgridColumns()
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(objErr.ToString(), True)
        End Try
    End Sub

    Private Sub gloRxSearchC1FlexPrescUserCtrl__FlexClick(ByVal sender As Object, ByVal e As System.EventArgs)
        'Try
        '    Dim i As Integer
        '    If _Flex.Rows.Count > 1 Then
        '        txtSearchFlexGrid.Text = ""
        '        i = _Flex.Col
        '        lblColName.Text = CType((_Flex.GetData(0, i)), String)
        '        If lblColName.Text = "Prescription Id" Then
        '            lblColName.Text = "Prescription Date"
        '        End If
        '        lblSearchString.Text = "Enter " & CType((_Flex.GetData(0, i)), String) & ""
        '        If lblSearchString.Text = "Enter Prescription Id" Then
        '            lblSearchString.Text = "Enter Prescription Date"
        '        End If
        '    End If

        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub gloRxSearchC1FlexPrescUserCtrl__FlexClick1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me._FlexClick
        'Try
        '    Dim i As Integer
        '    If _Flex.Rows.Count > 1 Then
        '        txtSearchFlexGrid.Text = ""
        '        i = _Flex.Col
        '        lblColName.Text = CType((_Flex.GetData(0, i)), String)
        '        If lblColName.Text = "Prescription Id" Then
        '            lblColName.Text = "Prescription Date"
        '        End If
        '        lblSearchString.Text = "Enter " & CType((_Flex.GetData(0, i)), String) & ""
        '        If lblSearchString.Text = "Enter Prescription Id" Then
        '            lblSearchString.Text = "Enter Prescription Date"
        '        End If
        '    End If

        'Catch ex As Exception
        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        'End Try
    End Sub
    Private Sub gloRxSearchC1FlexPrescUserCtrl_btnCloseRefillClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.btnCloseRefillClick
        Try
            ReferralCount = 0
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Public Function FetchPreviousDrugDetail(ByVal ndc As String, ByVal mpid As Int32, ByVal visitID As Int64, ByVal providerID As Int64, ByVal pharmacyID As Int64) As Boolean
        Dim objRxDBLayer As New RxBusinesslayer(_PatientID)
        Try
            ''''''this function is called in case the Rx form is opened from Disease Management
            Dim objprescription As Prescription = objRxDBLayer.FetchRefillDrugDetailsRevised(pharmacyID, mpid, ndc)

            objprescription.VisitID = visitID
            objprescription.ProviderID = providerID

            'objRxDBLayer.FillDrugDetailsRevised(objprescription)

            If Not objprescription.Medication = "" Then
                objprescription.PrescriptionID = 0
                If _RxBuisnessLayer.TransactionMode = RxBusinesslayer._TransactionMode.Edit Then
                    objprescription.Prescriptiondate = _RxBuisnessLayer.PrescriptionDate    'Set date as Old Prescription Date
                End If
                If String.IsNullOrEmpty(objprescription.Dosage) Then
                    objprescription.Medication = CType(_Flex.Item(_Flex.Row, COL_MEDICATION), System.String)
                End If

                objprescription.PatientID = _PatientID 'globalPatient.gnPatientID
                objprescription.Dosage = CType(_Flex.Item(_Flex.Row, COL_DOSAGE), System.String)
                objprescription.Route = CType(_Flex.Item(_Flex.Row, COL_ROUTE), System.String)
                objprescription.Frequency = CType(_Flex.Item(_Flex.Row, COL_FREQUENCY), System.String)

                'Changes done against Surescript Compliance case #00441992  
                'whenever a drug is refill from medication to prescription since we do not have may substitute User interface in Medication custom control we will by default send refill qualifier as "R"
                objprescription.RefillQualifier = "R" ''GLO2011-0012936/ GLO2011-0015685 DCG - RX Err
                'Dim strRefills As String
                If Not IsDBNull(_Flex.Item(_Flex.Row, COL_REFILLS)) Then
                    If CType(_Flex.Item(_Flex.Row, COL_REFILLS), System.String) <> "" AndAlso objprescription.IsNarcotics <> 2 Then
                        objprescription.Refills = CType(_Flex.Item(_Flex.Row, COL_REFILLS), System.String)
                    Else
                        objprescription.Refills = 0
                    End If
                Else
                    objprescription.Refills = 0
                End If

                objprescription.Duration = CType(_Flex.Item(_Flex.Row, COL_DURATION), System.String)
                objprescription.Startdate = Now.Date

                Dim amt As String = CType(_Flex.Item(_Flex.Row, COL_AMOUNT), System.String)
                If Not String.IsNullOrWhiteSpace(amt) Then
                    If Not IsNumeric(amt) Then
                        Dim qty As String() = amt.Split(" ")
                        If (qty.Length >= 1) Then
                            amt = qty(0)
                        End If
                    End If
                    objprescription.Amount = amt & " " & objprescription.PotencyUnit
                End If

                If Not IsNothing(objprescription.Amount) Then
                    If objprescription.Amount <> "" Then
                        If objprescription.Amount.Contains("Unspecified") Then
                            objprescription.Amount = objprescription.Amount.Trim.Replace("Unspecified", "") + objprescription.DosageForm
                        End If
                    End If
                End If

                objprescription.UserName = globalSecurity.gstrLoginName

                'If objprescription.Duration <> "" Then
                '    objprescription.Enddate = CalulateEndDate(objprescription.Startdate, objprescription.Duration)
                '    If objprescription.Enddate <> "12:00:00 AM" Then
                '        objprescription.CheckEndDate = True
                '    End If
                'End If

                objprescription.Notes = CType(_Flex.Item(_Flex.Row, COL_NOTES), System.String)
                objprescription.PrescriberNotes = CType(_Flex.Item(_Flex.Row, COL_Rx_sPrescriberNotes), System.String)

                If clsgeneral.gblnDisableAllowSubstitution.HasValue Then
                    objprescription.Maysubstitute = clsgeneral.gblnDisableAllowSubstitution.Value
                End If

                objprescription.Renewed = "Renewed" & " " & Now & " " & globalSecurity.gstrLoginName
                objprescription.UpdatedByUserName = globalSecurity.gstrLoginName

                If Not IsDBNull(_Flex.Item(_Flex.Row, COL_METHOD)) Then
                    If _Flex.Item(_Flex.Row, COL_METHOD) <> "" Then
                        If _Flex.Item(_Flex.Row, COL_METHOD) <> "eRx" Then
                            objprescription.Method = _Flex.Item(_Flex.Row, COL_METHOD)
                        Else
                            If objprescription.IsNarcotics > 0 Then
                                objprescription.Method = "Print"
                            Else
                                If objprescription.PhNCPDPID <> "" Then
                                    objprescription.Method = "eRx"
                                Else
                                    objprescription.Method = "Print"
                                End If
                            End If
                        End If
                    Else
                        If objprescription.IsNarcotics > 0 Then
                            objprescription.Method = "Print"
                        Else
                            If objprescription.PhNCPDPID <> "" Then
                                objprescription.Method = "eRx"
                            Else
                                objprescription.Method = "Print"
                            End If
                        End If
                    End If
                End If

                objprescription.OldDosage = CType(_Flex.Item(_Flex.Row, COL_DOSAGE), System.String)
                objprescription.DosageForm = CType(_Flex.Item(_Flex.Row, COL_DRUGFORM), System.String)
                objprescription.State = "A"

                If Not _Flex.Item(_Flex.Row, COL_DRUGID) Is Nothing Then
                    If Not _Flex.Item(_Flex.Row, COL_DRUGID).ToString() = "" Then
                        objprescription.DrugID = Convert.ToInt64(_Flex.Item(_Flex.Row, COL_DRUGID))
                    Else
                        ''''''check if Drugid is = 0, if yes remove the drugID using NDCcode, if still the NDCcode is blank then remove the Drug id using SIG parameters and save the DrugID
                        Dim nDrugID As Int64 = 0
                        If Not IsDBNull(_Flex.Item(_Flex.Row, COL_DRUGID)) Then
                            If Convert.ToInt64(_Flex.Item(_Flex.Row, COL_DRUGID)) = 0 Then
                                If objprescription.NDCCode <> "" Then
                                    nDrugID = _RxBuisnessLayer.GetDrugIDByNDC(objprescription.NDCCode)
                                Else
                                    If objprescription.mpid <> 0 Then
                                        '''''get drugID using mpid parameter
                                        nDrugID = _RxBuisnessLayer.GetDrugIdBympid(objprescription.mpid)
                                    Else
                                        Dim sDrugName As String = objprescription.Medication
                                        Dim sDosage As String = objprescription.Dosage
                                        Dim sRoute As String = objprescription.Route
                                        Dim sFrequency As String = objprescription.Frequency
                                        Dim sDuration As String = objprescription.Duration
                                        nDrugID = _RxBuisnessLayer.GetDrugIDFromSig(sDrugName, sDosage, sRoute, sFrequency, sDuration)
                                    End If
                                End If
                            End If
                            objprescription.DrugID = nDrugID
                        Else
                            objprescription.DrugID = nDrugID
                        End If

                    End If
                Else
                    ''''''check if Drugid is = 0, if yes remove the drugID using NDCcode, if still the NDCcode is blank then remove the Drug id using SIG parameters and save the DrugID
                    Dim nDrugID As Int64 = 0
                    If Convert.ToInt64(_Flex.Item(_Flex.Row, COL_DRUGID)) = 0 Then
                        If objprescription.NDCCode <> "" Then
                            nDrugID = _RxBuisnessLayer.GetDrugIDByNDC(objprescription.NDCCode)
                        Else
                            If objprescription.mpid <> 0 Then
                                '''''get drugID using mpid parameter
                                nDrugID = _RxBuisnessLayer.GetDrugIdBympid(objprescription.mpid)
                            Else
                                Dim sDrugName As String = objprescription.Medication
                                Dim sDosage As String = objprescription.Dosage
                                Dim sRoute As String = objprescription.Route
                                Dim sFrequency As String = objprescription.Frequency
                                Dim sDuration As String = objprescription.Duration
                                nDrugID = _RxBuisnessLayer.GetDrugIDFromSig(sDrugName, sDosage, sRoute, sFrequency, sDuration)
                            End If
                        End If
                    End If
                    objprescription.DrugID = nDrugID
                End If


                If objprescription.DosageForm = "" Then
                    objprescription.DosageForm = _RxBuisnessLayer.GetDosageformCode(objprescription.NDCCode)
                End If

                If Not IsNothing(objprescription.Amount) Then
                    If objprescription.Amount <> "" Then
                        If objprescription.Amount.Contains("Unspecified") Then
                            objprescription.Amount = objprescription.Amount.Trim.Replace("Unspecified", "") + objprescription.DosageForm
                        End If
                    End If
                End If
                _RxBuisnessLayer.PrescriptionCol.Add(objprescription)
                _IsValidDrug = True
                gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnIsRxMedsFormEdited = True
            Else
                MessageBox.Show("Selected drug NDC (" & ndc & ") is no longer available in drug master. Please search All Drugs for alternate therapy.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
                Exit Function
            End If
            Return True
        Catch ex As Exception
            Dim objex As New PrescriptionException
            objex.ErrMessage = "Error fetching Drug Details."
            Throw objex
            Return False
        Finally
            objRxDBLayer.Dispose()
            objRxDBLayer = Nothing
        End Try
    End Function

    Private Sub gloRxSearchC1FlexPrescUserCtrl_cntClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles Me.cntClick
        Dim sNDCCode As String = ""
        Dim mpid As Int32 = 0
        Try
            If Not IsDBNull(_Flex.Item(_Flex.Row, COL_NDCCode)) Then
                sNDCCode = CType(_Flex.GetData(_Flex.Row, COL_NDCCode), String)
                mpid = Convert.ToInt32(_Flex.GetData(_Flex.Row, COL_mpid))

                sNDCCode = _RxBuisnessLayer.RetrieveRepresentative_NDC(sNDCCode, mpid)

                'Dim _ServerTime As DateTime
                '_ServerTime = _RxBuisnessLayer.getServerTime()

                If sNDCCode = "" Then
                    sNDCCode = CType(_Flex.GetData(_Flex.Row, COL_NDCCode), String)
                    Using oDIBHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                        mpid = oDIBHelper.GetMarketedProductId(sNDCCode, mpid)
                    End Using
                End If

                Call FetchPreviousDrugDetail(sNDCCode, mpid, _RxBuisnessLayer.CurrentVisitID, _RxBuisnessLayer.ProviderID, _RxBuisnessLayer.PharmacyId)

                ''''''''means Rx-Meds form is edited and we should prompt the user if he directly Clicks the Close button

            End If
            RaiseEvent cntListmenuStripClick(sender, e)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    
    Private Sub gloRxSearchC1FlexPrescUserCtrl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If _RefillDisable = True Then
                cntListmenuStrip.Items(0).Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gloRxSearchC1FlexPrescUserCtrl_txtSearchFlexGridTextChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.txtSearchFlexGridTextChanged
        Try
            InstringSearch()
            'Dim str As String = ""
            'Dim rowid As Integer
            'Select Case lblColName.Text
            '    Case "Prescription Date"

            '        str = txtSearchFlexGrid.Text
            '        With _Flex

            '            rowid = .FindRow(str, 1, .Col, False, False, True)
            '            .Row = rowid
            '        End With

            '    Case "Drug"
            '        str = txtSearchFlexGrid.Text
            '        With _Flex

            '            rowid = .FindRow(str, 1, .Col, False, False, True)
            '            .Row = rowid
            '        End With

            '    Case "Route"
            '        str = txtSearchFlexGrid.Text
            '        With _Flex

            '            rowid = .FindRow(str, 1, .Col, False, False, True)
            '            .Row = rowid
            '        End With

            '    Case "Sig"
            '        str = txtSearchFlexGrid.Text
            '        With _Flex

            '            rowid = .FindRow(str, 1, .Col, False, False, True)
            '            .Row = rowid
            '        End With

            '    Case ("Amount")
            '        str = txtSearchFlexGrid.Text
            '        With _Flex

            '            rowid = .FindRow(str, 1, .Col, False, False, True)
            '            .Row = rowid
            '        End With
            '    Case ("Refills")
            '        str = txtSearchFlexGrid.Text
            '        With _Flex

            '            rowid = .FindRow(str, 1, .Col, False, False, True)
            '            .Row = rowid
            '        End With

            '    Case ("May Substitute")
            '        str = txtSearchFlexGrid.Text
            '        With _Flex

            '            rowid = .FindRow(str, 1, .Col, False, False, True)
            '            .Row = rowid
            '        End With

            '    Case Else 'this is the case in which it will be invoked by default i.e on drug name
            '        str = txtSearchFlexGrid.Text
            '        With _Flex

            '            rowid = .FindRow(str, 1, COL_DRUG, False, False, True)
            '            .Row = rowid
            '        End With

            'End Select


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub _Flex_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _Flex.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    'Added by Shweta 20090910
    'Against salesForce Case No:GLO2009 0002946 For calculating end date while refill Rx
    'To calculate the end date only when duration has mention and it has to be numeric
    Public Function CalulateEndDate(ByVal dStartdate As Date, ByVal dDuration As String) As Date
        Dim dEndDate As Date
        Dim aDuration As Array
        Try
            'To spilt the stringv for getting combo box value
            aDuration = dDuration.Split(" ")
            If IsNumeric(aDuration(0)) Then     'Duration must be numeric
                Select Case aDuration(aDuration.Length - 1)
                    Case "Days"
                        dEndDate = DateAdd(DateInterval.Day, CType(aDuration(0), Double), dStartdate)
                    Case "Months"
                        dEndDate = DateAdd(DateInterval.Month, aDuration(0), dStartdate)
                    Case "Weeks"
                        dEndDate = DateAdd(DateInterval.WeekOfYear, aDuration(0), dStartdate)
                    Case ""
                        CalulateEndDate = Nothing
                        Exit Function
                End Select
            End If
        Catch ex As Exception
            Dim objex As New gloUserControlLibrary.gloUserControlExceptions
            objex.ErrorMessage = "Error in calculating end date"
            CalulateEndDate = Nothing
            Throw objex
        End Try
        Return dEndDate
    End Function
    'End Shweta
End Class
