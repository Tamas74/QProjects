
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports gloEMRGeneralLibrary.gloEMRActors
Imports gloEMRGeneralLibrary.gloEMRMedication
Imports gloEMRGeneralLibrary.Glogeneral
Public Class gloMedRefillC1FlexGridUserCtrl

    Public Event cntListmenuStripClick(ByVal sender As Object, ByVal e As System.EventArgs)

    Private _MedBuisnessLayer As MedicationBusinessLayer
    Public Property MedBuisnessLayerObject() As MedicationBusinessLayer
        Get
            Return _MedBuisnessLayer
        End Get
        Set(ByVal value As MedicationBusinessLayer)
            _MedBuisnessLayer = value
        End Set
    End Property

    Private ReferralCount As Long
    Private VisitID As Long
    Public Dv As DataView
    'used for instring search

    Public dvNext As DataView
    ''''''To get the Patient Count in the DataView after Searching 
    Private _VisibleCount As Int16 = 0
    Private _MakeCurrentDisable As Boolean
    'used for instring search

    'Private COL_MEDICATIONID As Integer = 0
    'Private COL_VISITID As Integer = 1
    'Private COL_PATIENTID As Integer = 2
    'Private COL_VISITDATE As Integer = 3 'medication date
    'Private COL_DRUG As Integer = 4 ' means Drug
    'Private COL_SIG As Integer = 5 'means sig
    'Private COL_AMOUNT As Integer = 6
    'Private COL_MEDICATION As Integer = 7 ' means Drug
    'Private COL_DOSAGE As Integer = 8
    'Private COL_ROUTE As Integer = 9
    'Private COL_FREQUENCY As Integer = 10
    'Private COL_STARTDATE As Integer = 11
    'Private COL_ENDDATE As Integer = 12
    'Private COL_DURATION As Integer = 13
    'Private COL_STATUS As Integer = 14
    'Private COL_REASON As Integer = 15

    'Private COL_USERNAME As Integer = 16
    'Private COL_PRESCRIPTIONID As Integer = 17
    'Private COL_RENEWED As Integer = 18
    'Private COL_REFILLS As Integer = 19

    ''For De-Normalization
    'Private COL_NDCCode As Integer = 20
    'Private COL_NARCOTIC As Integer = 21
    'Private COL_DRUGFORM As Integer = 22
    'Private COL_DRUGQTYQUALIFIER As Integer = 23
    ''For De-Normalization


    Private COL_MEDICATIONID As Integer = 0
    Private COL_VISITID As Integer = 1
    Private COL_PATIENTID As Integer = 2
    Private COL_VISITDATE As Integer = 3 'medication date
    Private COL_DRUG_NAME As Integer = 4 'new column to show drug name,dosage drugform and route together in 6030
    Private COL_DOSAGE As Integer = 5
    Private COL_ROUTE As Integer = 6
    Private COL_FREQUENCY As Integer = 7
    Private COL_DURATION As Integer = 8
    Private COL_SIG As Integer = 9 'means sig
    Private COL_AMOUNT As Integer = 10
    Private COL_MEDICATION As Integer = 11 ' means Drug
    Private COL_STARTDATE As Integer = 12
    Private COL_ENDDATE As Integer = 13

    Private COL_STATUS As Integer = 14
    Private COL_REASON As Integer = 15

    Private COL_USERNAME As Integer = 16
    Private COL_PRESCRIPTIONID As Integer = 17
    Private COL_RENEWED As Integer = 18
    Private COL_REFILLS As Integer = 19

    'For De-Normalization
    Private COL_NDCCode As Integer = 20
    Private COL_NARCOTIC As Integer = 21
    Private COL_DRUGFORM As Integer = 22
    Private COL_DRUGQTYQUALIFIER As Integer = 23
    'For De-Normalization

    Private COL_PBMSourceName As Integer = 24
    Private COL_Rx_sRefills As Integer = 25
    Private COL_Rx_sNotes As Integer = 26
    Private COL_Rx_sMethod As Integer = 27
    Private COL_Rx_MaySubstitute As Integer = 28 ''''''''set the may substitute flag as it is saved from prescription or from medications
    Private COL_Rx_nproviderId As Integer = 29
    Private COL_Rx_nPharmacyId As Integer = 30
    Private COL_Rx_sNCPDPID As Integer = 31
    Private COL_Rx_nContactId As Integer = 32
    Private COL_Rx_sName As Integer = 33
    Private COL_Rx_sPrescriberNotes As Integer = 34
    Private COL_Rx_eRxStatus As Integer = 35
    Private COL_DRUG As Integer = 36
    Private COL_Order As Integer = 37
    Private COL_IsAdministrator = 38
    Private COL_RxMPID As Integer = 39
    Private COL_UpdatedBy As Integer = 40
    Private COL_ReasonConceptID As Integer = 41
    Private COL_ReasonConceptDesc As Integer = 42

    Private COL_COUNT As Integer = 43

    Public Sub New(ByRef objMedBusinessLayer As MedicationBusinessLayer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        _MedBuisnessLayer = objMedBusinessLayer
        Dv = New DataView
        'SetFlexgridColumns()
        AddMenu()
        ' Add any initialization after the InitializeComponent() call.
    End Sub


    Public Property MakeCurrentDisable() As Boolean
        Get
            Return _MakeCurrentDisable
        End Get
        Set(ByVal value As Boolean)
            _MakeCurrentDisable = value
        End Set
    End Property
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub


    Private Sub SetFlexgridColumns()
        Try
            With _Flex
                .Redraw = False
                .AllowDrop = True

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

                Dim _Width As Single = .Width / 6
                'set column value
                .Cols(COL_MEDICATIONID).Width = 0
                .Cols(COL_VISITID).Width = 0
                .Cols(COL_PATIENTID).Width = 0
                .Cols(COL_VISITDATE).Width = _Width * 2
                .Cols(COL_DRUG).Width = 0
                .Cols(COL_DRUG_NAME).Width = _Width * 2.8
                .Cols(COL_DOSAGE).Width = 0 ' _Width * 1'Rx Feild Name change in 6030
                .Cols(COL_ROUTE).Width = 0 '_Width * 1'Rx Feild Name change in 6030
                .Cols(COL_FREQUENCY).Width = _Width * 1.2
                .Cols(COL_DURATION).Width = _Width * 0.8
                .Cols(COL_SIG).Width = 0
                .Cols(COL_AMOUNT).Width = _Width * 1 ''also called Dispense
                .Cols(COL_MEDICATION).Width = 0  ''also called drug name
                .Cols(COL_STARTDATE).Width = 0
                .Cols(COL_ENDDATE).Width = 0

                .Cols(COL_STATUS).Width = _Width * 1
                .Cols(COL_REASON).Width = _Width * 1

                .Cols(COL_USERNAME).Width = 0
                .Cols(COL_PRESCRIPTIONID).Width = 0
                .Cols(COL_RENEWED).Width = 0
                .Cols(COL_REFILLS).Width = 0

                'For De-Normalization
                .Cols(COL_NDCCode).Width = 0
                .Cols(COL_NARCOTIC).Width = 0
                .Cols(COL_DRUGFORM).Width = 0
                .Cols(COL_DRUGQTYQUALIFIER).Width = 0
                'For De-Normalization

                .Cols(COL_PBMSourceName).Width = 0
                .Cols(COL_Rx_sRefills).Width = 0
                .Cols(COL_Rx_sNotes).Width = 0
                .Cols(COL_Rx_sMethod).Width = 0
                .Cols(COL_Rx_MaySubstitute).Width = 0
                .Cols(COL_Rx_nproviderId).Width = 0
                .Cols(COL_Rx_nPharmacyId).Width = 0
                .Cols(COL_Rx_sNCPDPID).Width = 0
                .Cols(COL_Rx_nContactId).Width = 0
                .Cols(COL_Rx_sName).Width = 0
                .Cols(COL_Rx_sPrescriberNotes).Width = 0
                .Cols(COL_Rx_eRxStatus).Width = 0
                .Cols(COL_Order).Width = 0
                .Cols(COL_IsAdministrator).Width = 0
                .Cols(COL_RxMPID).Width = 0
                .Cols(COL_UpdatedBy).Width = 0
                .Cols(COL_ReasonConceptID).Width = 0
                .Cols(COL_ReasonConceptDesc).Width = 0

                'set column header
                .SetData(0, COL_MEDICATIONID, "Medication ID")
                .SetData(0, COL_VISITID, "Visit ID")
                .SetData(0, COL_PATIENTID, "Patient ID")

                ''GLO2011-0013152 : Need to change the column name in the medication history screen from VISITDATE to "Verified Date"
                ''VISITDATE Changed to Verified Date
                .SetData(0, COL_VISITDATE, "Verified Date")
                .SetData(0, COL_DRUG_NAME, "Drug") ''this is called as Medication but in image it is DrugName
                .SetData(0, COL_DOSAGE, "Dosage")
                .SetData(0, COL_ROUTE, "Route")
                .SetData(0, COL_FREQUENCY, "Patient Directions") 'Rx Feild Name change in 6030
                .SetData(0, COL_DURATION, "Duration")
                .SetData(0, COL_SIG, "Sig") 'this is called as Route
                .SetData(0, COL_AMOUNT, "Quantity") 'Rx Feild Name change in 6030
                .SetData(0, COL_MEDICATION, "Medication")
                .SetData(0, COL_STARTDATE, "Start Date")
                .SetData(0, COL_DURATION, "Duration")
                .SetData(0, COL_STATUS, "Status")
                .SetData(0, COL_REASON, "Reason")

                .SetData(0, COL_USERNAME, "User Name")
                .SetData(0, COL_PRESCRIPTIONID, "RX ID")
                .SetData(0, COL_RENEWED, "Renewed")
                .SetData(0, COL_REFILLS, "Refills")

                'For De-Normalization
                .SetData(0, COL_NDCCode, "NDCCode")
                .SetData(0, COL_NARCOTIC, "IsNarcotic")
                .SetData(0, COL_DRUGFORM, "DrugForm")
                .SetData(0, COL_DRUGQTYQUALIFIER, "DrugQtyQualifier")
                'For De-Normalization

                .SetData(0, COL_PBMSourceName, "PBMSourceName")
                .SetData(0, COL_Rx_sRefills, "Rx_sRefills")
                .SetData(0, COL_Rx_sNotes, "Rx_sNotes")
                .SetData(0, COL_Rx_sMethod, "Rx_sMethod")
                .SetData(0, COL_Rx_MaySubstitute, "Rx_MaySubstitute")
                .SetData(0, COL_Rx_nproviderId, "Rx_nproviderId")
                .SetData(0, COL_Rx_nPharmacyId, "Rx_nPharmacyId")
                .SetData(0, COL_Rx_sNCPDPID, "Rx_sNCPDPID")
                .SetData(0, COL_Rx_nContactId, "Rx_nContactId")
                .SetData(0, COL_Rx_sName, "Rx_sName")
                .SetData(0, COL_Rx_sPrescriberNotes, "Rx_sPrescriberNotes")
                .SetData(0, COL_Rx_eRxStatus, "Rx_eRxStatus")
                .SetData(0, COL_DRUG, "Drug Name")
                .SetData(0, COL_RxMPID, "MPID")
                'set visiblity for column 
                .Cols(COL_MEDICATIONID).Visible = False
                .Cols(COL_VISITID).Visible = False
                .Cols(COL_PATIENTID).Visible = False
                .Cols(COL_VISITDATE).Visible = True
                .Cols(COL_DRUG).Visible = False
                .Cols(COL_DRUG_NAME).Visible = True
                .Cols(COL_DOSAGE).Visible = True
                .Cols(COL_ROUTE).Visible = True
                .Cols(COL_FREQUENCY).Visible = True
                .Cols(COL_DURATION).Visible = True
                .Cols(COL_SIG).Visible = False
                .Cols(COL_AMOUNT).Visible = True
                .Cols(COL_MEDICATION).Visible = False ''also called drug name
                .Cols(COL_STARTDATE).Visible = False
                .Cols(COL_ENDDATE).Visible = False
                .Cols(COL_STATUS).Visible = True
                .Cols(COL_REASON).Visible = True

                .Cols(COL_USERNAME).Visible = False
                .Cols(COL_PRESCRIPTIONID).Visible = False
                .Cols(COL_RENEWED).Visible = False
                .Cols(COL_REFILLS).Visible = False

                'For De-Normalization
                .Cols(COL_NDCCode).Visible = False
                .Cols(COL_NARCOTIC).Visible = False
                .Cols(COL_DRUGFORM).Visible = False
                .Cols(COL_DRUGQTYQUALIFIER).Visible = False
                'For De-Normalization

                .Cols(COL_PBMSourceName).Visible = False
                .Cols(COL_Rx_sRefills).Visible = False
                .Cols(COL_Rx_sNotes).Visible = False
                .Cols(COL_Rx_sMethod).Visible = False
                .Cols(COL_Rx_MaySubstitute).Visible = False
                .Cols(COL_Rx_nproviderId).Visible = False
                .Cols(COL_Rx_nPharmacyId).Visible = False
                .Cols(COL_Rx_sNCPDPID).Visible = False
                .Cols(COL_Rx_nContactId).Visible = False
                .Cols(COL_Rx_sName).Visible = False
                .Cols(COL_Rx_sPrescriberNotes).Visible = False
                .Cols(COL_Rx_eRxStatus).Visible = False
                .Cols(COL_RxMPID).Visible = False
                .Cols(COL_UpdatedBy).Visible = False
                .Cols(COL_ReasonConceptID).Visible = False
                .Cols(COL_ReasonConceptDesc).Visible = False
                .Cols(COL_Order).Visible = False
                .Cols(COL_IsAdministrator).Visible = False
                ' set column editing properties.

                .Cols(COL_MEDICATIONID).AllowEditing = False
                .Cols(COL_VISITID).AllowEditing = False
                .Cols(COL_PATIENTID).AllowEditing = False
                .Cols(COL_VISITDATE).AllowEditing = False
                .Cols(COL_DRUG).AllowEditing = False
                .Cols(COL_DRUG_NAME).AllowEditing = False
                .Cols(COL_DOSAGE).AllowEditing = False
                .Cols(COL_ROUTE).AllowEditing = False 'sig
                .Cols(COL_FREQUENCY).AllowEditing = False
                .Cols(COL_DURATION).AllowEditing = False
                .Cols(COL_SIG).AllowEditing = False
                .Cols(COL_AMOUNT).AllowEditing = False
                .Cols(COL_MEDICATION).AllowEditing = False ''also called drug name
                .Cols(COL_DOSAGE).AllowEditing = False
                .Cols(COL_ROUTE).AllowEditing = False 'sig
                .Cols(COL_FREQUENCY).AllowEditing = False
                .Cols(COL_STARTDATE).AllowEditing = False
                .Cols(COL_ENDDATE).AllowEditing = False
                .Cols(COL_DURATION).AllowEditing = False
                .Cols(COL_STATUS).AllowEditing = False
                .Cols(COL_REASON).AllowEditing = False
                .Cols(COL_USERNAME).AllowEditing = False
                .Cols(COL_PRESCRIPTIONID).AllowEditing = False
                .Cols(COL_RENEWED).AllowEditing = False
                .Cols(COL_REFILLS).AllowEditing = False

                'For De-Normalization
                .Cols(COL_NDCCode).AllowEditing = False
                .Cols(COL_NARCOTIC).AllowEditing = False
                .Cols(COL_DRUGFORM).AllowEditing = False
                .Cols(COL_DRUGQTYQUALIFIER).AllowEditing = False
                'For De-Normalization
                .Cols(COL_Rx_MaySubstitute).AllowEditing = False
                .Cols(COL_Rx_MaySubstitute).DataType = System.Type.GetType("String")
                .Redraw = True
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Public Sub AddMenu()
        Try


            Dim tlstripitem As ToolStripMenuItem
            tlstripitem = New ToolStripMenuItem
            tlstripitem.Text = "Make Current"
            tlstripitem.Tag = 1
            tlstripitem.Image = ImagSearchFlex.Images(0)
            tlstripitem.ForeColor = Color.FromArgb(31, 73, 125)
            cntListmenuStrip.Items.Add(tlstripitem)

            'tlstripitem.Dispose()
            tlstripitem = Nothing
            cntListmenuStrip.SendToBack()
            Panel2.BringToFront()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Public Sub BindGrid()
        Try

            Dim dt As DataTable
            ReferralCount = 0

            dt = _MedBuisnessLayer.FetchMedicationforView(VisitID)
            ' In dt we Get The Following Columns
            'nPrescriptionId,nVisitId,nPatientID,dtPrescriptiondate,'Drug','Sig','Amount','Refills', bmaysubstitute
            ', sMedication,'Dosage','Route','Frequency'
            'Duration',dtstartdate,dtEnddate,'Notes','Method',nDrugID

            'nMedicationId,nVisitId,.nPatientID,dtvisitdate,'Drug','Sig','Amount','sMedication,'Dosage','Route',
            'Frequency',dtstartdate,dtEnddate,'Duration','Status','Reason'
            If (IsNothing(Dv) = False) Then
                Dv.Dispose()
                Dv = Nothing
            End If
            Dv = dt.DefaultView
            'Dim i As Integer


            _Flex.DataSource = dt

            'i = _Flex.Col
            'Dim strsort As String
            'strsort = _Flex.GetData(0, i)
            'Dv.Sort = "[" & strsort & "]"
            'HideColumns(objPrescriptionDBLayer.DsDataview)
            'referral count holds the current row count
            ReferralCount = Dv.Count - 1

            Call SetFlexgridColumns()
            'End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub gloMedRefillC1FlexGridUserCtrl__FlexClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me._FlexClick
        'Try
        '    Dim i As Integer
        '    If _Flex.Rows.Count > 1 Then
        '        txtSearchFlexGrid.Text = ""
        '        i = _Flex.Col
        '        lblColName.Text = CType((_Flex.GetData(0, i)), String)
        '        If lblColName.Text = "Medication ID" Then
        '            lblColName.Text = "Visit Date"
        '        End If
        '        lblSearchString.Text = "Enter " & CType((_Flex.GetData(0, i)), String) & ""
        '        If lblSearchString.Text = "Enter Medication ID" Then
        '            lblSearchString.Text = "Enter Visit Date"
        '        End If
        '    End If

        'Catch ex As Exception
        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        'End Try

    End Sub

    Private Sub gloMedRefillC1FlexGridUserCtrl_cntClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles Me.cntClick
        'Dim tempvisitdate As Date
        Try

            'If intMode = 1 Then----------
            '    tempvisitdate = m_visitdate
            'ElseIf intMode = 2 Then
            '    tempvisitdate = tempdate
            'End If--------------
            'If CType(dgMakeCurrent.Item(dgMakeCurrent.CurrentRowIndex, 3), System.DateTime).Date < tempvisitdate.Date Then-------------
            'If CType(_Flex.Item(_Flex.Row, COL_VISITDATE), System.DateTime).Date < tempvisitdate Then 
            If CType(_Flex.Item(_Flex.Row, COL_VISITDATE), System.DateTime).Date < Now Then 'time being take now it should be tempvisitdate
                'Dim mynode As myTreeNode
                'If trMedicationDetails.Nodes.Item(0).GetNodeCount(False) > 0 Then '''''by sagar start here
                'If C1FlexGrdMedicationDetails.Rows.Count > 0 Then-----------------
                If _Flex.Rows.Count > 0 Then
                    'Verify that there is'nt a duplicate drug
                    '    For Each mynode In trMedicationDetails.Nodes.Item(0).Nodes
                    'For i As Int16 = 1 To C1FlexGrdMedicationDetails.Rows.Count - 1------------
                    ' For i As Int16 = 1 To _Flex.Rows.Count - 1
                    If _MedBuisnessLayer.MedicationCol.Count > 0 Then
                        'SLR: 1/20/2015: Something wrong here: Medication has to be assigned from Col.count instead of new?
                        Dim _Medication As New Medication
                        For j As Integer = 0 To _MedBuisnessLayer.MedicationCol.Count - 1
                            If _Medication.Medication = CType(_Flex.Item(_Flex.Row, COL_MEDICATION), String) And _Medication.Dosage = CType(_Flex.Item(_Flex.Row, COL_DOSAGE), String) And _Medication.Route = CType(_Flex.Item(_Flex.Row, COL_ROUTE), String) And _Medication.Frequency = CType(_Flex.Item(_Flex.Row, COL_FREQUENCY), String) And _Medication.Duration = CType(_Flex.Item(_Flex.Row, COL_DURATION), String) Then                                'MessageBox.Show("This """ & CType(dgMakeCurrent.Item(dgMakeCurrent.CurrentRowIndex, 7), System.String) & """ is already on your Medication list, I will not allow duplicates", "gloEMR")
                                MessageBox.Show("This """ & CType(_Flex.Item(_Flex.Row, COL_MEDICATION), System.String) & """ is already on your Medication list, I will not allow duplicates", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            End If
                            ' And medication.Status = CType(.Item(rowcount, 14), String) And medication.Reason = CType(.Item(rowcount, 15), String) Then
                        Next
                        _Medication.Dispose()
                        _Medication = Nothing
                    End If
                    'Dim medication As New ClsPrescription
                    ''        objMedicationDBLayer.GetInsuranceCol(medication, mynode.Index)
                    'objMedicationDBLayer.GetInsuranceCol(medication, i - 1)
                    'Dim rowcount As Int32
                    'rowcount = dgMakeCurrent.CurrentRowIndex
                    'With dgMakeCurrent
                    '    If medication.Medication = CType(.Item(rowcount, 7), String) And medication.Dosage = CType(.Item(rowcount, 8), String) And medication.Route = CType(.Item(rowcount, 9), String) And medication.frequency = CType(.Item(rowcount, 10), String) And medication.Duration = CType(.Item(rowcount, 13), String) Then ' And medication.Status = CType(.Item(rowcount, 14), String) And medication.Reason = CType(.Item(rowcount, 15), String) Then
                    '        MessageBox.Show("This """ & CType(dgMakeCurrent.Item(dgMakeCurrent.CurrentRowIndex, 7), System.String) & """ is already on your Medication list, I will not allow duplicates", "gloEMR")
                    '        Exit Sub
                    '    End If
                    'End With
                    'medication = Nothing
                    ' Next
                    'mynode = Nothing '''''by sagar
                    'Else-------
                End If
                'Change the form mode to New Mode
                'If Not blnIsRefill Then------------
                '    NewMedication()
                '    blnIsRefill = True
                'End If----------------

                '''''by sagar End here
                'Assign the selected drug parameters to the Prescription class object and
                'Add it to a collection
                ' Dim objMedication As New ClsPrescription----------
                Dim objMedication As New Medication

                'objMedication.PrescriptionID = 0-----------

                'objMedication.VisitId = 0
                'If intMode = 1 Then-------------
                '    objMedication.VisitId = VisitID 'Set VisitId as passed from Visit form
                'ElseIf intMode = 2 Then
                '    objMedication.VisitId = tempVisitId 'Set VisitId to the one fetched for that prescription
                '    objMedication.PrescriptionDate = tempdate 'Set date as Old Prescription Date
                'End If----------------
                'objMedication.PatientID = CType(dgMakeCurrent.Item(dgMakeCurrent.CurrentRowIndex, 2), Long)---------
                objMedication.VisitID = _MedBuisnessLayer.CurrentVisitID
                objMedication.PatientID = CType(_Flex.Item(_Flex.Row, COL_PATIENTID), Long)
                'objMedication.Medication = CType(dgMakeCurrent.Item(dgMakeCurrent.CurrentRowIndex, 7), System.String)
                objMedication.Medication = CType(_Flex.Item(_Flex.Row, COL_MEDICATION), System.String)
                'objMedication.Dosage = CType(dgMakeCurrent.Item(dgMakeCurrent.CurrentRowIndex, 8), System.String)
                objMedication.Dosage = CType(_Flex.Item(_Flex.Row, COL_DOSAGE), System.String)
                'objMedication.Route = CType(dgMakeCurrent.Item(dgMakeCurrent.CurrentRowIndex, 9), System.String)
                objMedication.Route = CType(_Flex.Item(_Flex.Row, COL_ROUTE), System.String)
                'objMedication.frequency = CType(dgMakeCurrent.Item(dgMakeCurrent.CurrentRowIndex, 10), System.String)
                objMedication.Frequency = CType(_Flex.Item(_Flex.Row, COL_FREQUENCY), System.String)
                'objMedication.Duration = CType(dgMakeCurrent.Item(dgMakeCurrent.CurrentRowIndex, 13), System.String)
                objMedication.Duration = CType(_Flex.Item(_Flex.Row, COL_DURATION), System.String)
                'objMedication.StartDate = CType(dgMakeCurrent.Item(dgMakeCurrent.CurrentRowIndex, 14), System.DateTime)
                objMedication.Startdate = Now.Date
                objMedication.ReasonConceptID = CType(_Flex.Item(_Flex.Row, COL_ReasonConceptID), System.String)
                objMedication.ReasonConceptDesc = CType(_Flex.Item(_Flex.Row, COL_ReasonConceptDesc), System.String)

                objMedication.mpid = CType(_Flex.Item(_Flex.Row, COL_RxMPID), Int32)
                ''Added by Shweta 20091127
                'Against salesForce Case No:GLO2009 0002946 For calculating end date while refill Rx
                If objMedication.Duration <> "" Then
                    objMedication.Enddate = CalulateEndDate(objMedication.Startdate, objMedication.Duration)
                    objMedication.CheckEndDate = True
                End If
                ''End Shweta

                '------------date logic need to be checked by madam like in prescription form-----------------
                'If Not IsDBNull(dgMakeCurrent.Item(RowIndex, 12)) Then
                '    objMedication.EndDate = CType(dgMakeCurrent.Item(dgMakeCurrent.CurrentRowIndex, 12), System.DateTime)
                '    objMedication.CheckEndDate = True
                'Else
                '    objMedication.CheckEndDate = False
                'End If
                '---------------------------------------------------------------------------------------------
                objMedication.Amount = CType(_Flex.Item(_Flex.Row, COL_AMOUNT), System.String)
                ' objMedication.Status = CType(dgMakeCurrent.Item(dgMakeCurrent.CurrentRowIndex, 15), System.String)
                'objMedication.Reason = CType(dgMakeCurrent.Item(dgMakeCurrent.CurrentRowIndex, 16), System.String)
                'objMedicationDBLayer.PopulateArraylist(objMedication)-----------------
                objMedication.UserName = globalSecurity.gstrLoginName

                'CCHIT 08
                objMedication.NDCCode = CType(_Flex.Item(_Flex.Row, COL_NDCCode), System.String)
                objMedication.IsNarcotics = CType(_Flex.Item(_Flex.Row, COL_NARCOTIC), System.Int16)
                objMedication.DosageForm = CType(_Flex.Item(_Flex.Row, COL_DRUGFORM), System.String)
                objMedication.StrengthUnit = CType(_Flex.Item(_Flex.Row, COL_DRUGQTYQUALIFIER), System.String)
                'CCHIT 08

                objMedication.Rx_MaySubstitute = CType(_Flex.Item(_Flex.Row, COL_Rx_MaySubstitute), System.String)

                objMedication.Rx_PhName = CType(_Flex.Item(_Flex.Row, COL_Rx_sName), System.String)
                objMedication.Rx_Notes = CType(_Flex.Item(_Flex.Row, COL_Rx_sNotes), System.String)
                objMedication.Rx_PrescriberNotes = CType(_Flex.Item(_Flex.Row, COL_Rx_sPrescriberNotes), System.String)
                objMedication.ItemNumber = _MedBuisnessLayer.MedicationCol.Count
                objMedication.State = "A"
                _MedBuisnessLayer.MedicationCol.Add(objMedication)

                'Add the selected drug to Prescription Details treeview
                'mynode = New myTreeNode--------------------------
                'Dim strname As String
                'strname = ConcatentateDrugDetails(objMedication)
                'mynode.Key = objMedication.DrugID
                'mynode.Text = strname
                ''mynode.ForeColor = trMedicationDetails.Nodes.Item(0).ForeColor '''''by sagar
                'mynode.Checked = True-----------------------------
                'trMedicationDetails.Nodes.Item(0).Nodes.Add(mynode)'''''by sagar
                'trMedicationDetails.ExpandAll()'''''by sagar

                '''''code added by sagar
                'Add the selected drug from Datagrid to C1FlexGridMedicationDetails 
                ''''''''''''''''''''''''''''''''''''Code to add the drug in the C1FlexGrid after right clicking the drug in the DataGrid by  sagar() ''''''''''''''''
                'With C1FlexGrdMedicationDetails-----------------------------------------------
                '    .Rows.Add()
                '    .SetData(.Rows.Count - 1, COL_PRESCRIPTIONID, objMedication.PrescriptionID)
                '    .SetData(.Rows.Count - 1, COL_VISITID, objMedication.VisitId)
                '    .SetData(.Rows.Count - 1, COL_PATIENTID, objMedication.PatientID)
                '    .SetData(.Rows.Count - 1, COL_MEDICATION, objMedication.Medication)
                '    .SetData(.Rows.Count - 1, COL_DOSAGE, objMedication.Dosage)
                '    .SetData(.Rows.Count - 1, COL_ROUTE, objMedication.Route)
                '    .SetData(.Rows.Count - 1, COL_FREQUENCY, objMedication.frequency)
                '    .SetData(.Rows.Count - 1, COL_DURATION, objMedication.Duration)
                '    .SetData(.Rows.Count - 1, COL_AMOUNT, objMedication.Amount) 'dispense
                '    .SetData(.Rows.Count - 1, COL_REFILLS, objMedication.Refills)
                '    .SetData(.Rows.Count - 1, COL_STARTDATE, objMedication.StartDate)
                '    .SetData(.Rows.Count - 1, COL_ENDDATE, objMedication.EndDate)
                '    .SetData(.Rows.Count - 1, COL_NOTES, objMedication.Notes)
                '    .SetData(.Rows.Count - 1, COL_METHOD, objMedication.Method)
                '    .SetData(.Rows.Count - 1, COL_MAYSUBSTITUTE, objMedication.Maysubstitute)
                '    .SetData(.Rows.Count - 1, COL_PRESCRIPTIONDATE, objMedication.PrescriptionDate)
                '    .SetData(.Rows.Count - 1, COL_DRUGID, objMedication.DrugID)
                '    .SetData(.Rows.Count - 1, COL_DDID, objMedication.DDID)
                '    .SetData(.Rows.Count - 1, COL_INFLAG, 0) 'lnflag
                '    .SetData(.Rows.Count - 1, COL_LOTNUMBER, objMedication.LotNo)
                '    .SetData(.Rows.Count - 1, COL_EXPDATE, objMedication.ExpiryDate)
                '    .Row = .Rows.Count - 1
                'End With--------------------------------------------------------------------------

                gloEMRGeneralLibrary.Glogeneral.clsgeneral.blnIsRxMedsFormEdited = True ''''''''means Rx-Meds form is edited and we should prompt the user if he directly Clicks the Close button

                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                RaiseEvent cntListmenuStripClick(sender, e)
                '''''''''''''''''''
            Else
                MessageBox.Show("You have already entered the Medication Item """ & CType(_Flex.Item(_Flex.Row, COL_MEDICATION), System.String) & """ for this patient today", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub

    Private Sub gloMedRefillC1FlexGridUserCtrl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If _MakeCurrentDisable = True Then
                cntListmenuStrip.Items(0).Visible = False
            End If
            BindGrid()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub gloMedRefillC1FlexGridUserCtrl_txtSearchFlexGridTextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.txtSearchFlexGridTextChanged
        Try
            InstringSearch()
            'Dim str As String = ""
            'Dim rowid As Integer
            'Select Case lblColName.Text
            '    Case "Visit Date"

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
            '    Case ("Status")
            '        str = txtSearchFlexGrid.Text
            '        With _Flex

            '            rowid = .FindRow(str, 1, .Col, False, False, True)
            '            .Row = rowid
            '        End With

            '    Case ("Reason")
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    'C
    Private Sub InstringSearch()
        Try
            If Dv Is Nothing Then
                Me.Cursor = Cursors.[Default]
                Exit Sub
            End If

            Dim nCOL_VISITDATE As Byte = 3
            Dim nCOL_DRUG As Byte = 4
            Dim nCOL_DOSAGE As Byte = 5
            Dim nCOL_ROUTE As Byte = 6
            Dim nCOL_FREQUENCY As Byte = 7
            Dim nCOL_DURATION As Byte = 8
            Dim nCOL_STATUS As Byte = 14
            Dim nCOL_REASON As Byte = 15



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
                    Dv.RowFilter = Dv.Table.Columns(nCOL_VISITDATE).ColumnName & " Like '%" & strSearch & "%' OR " & Dv.Table.Columns(nCOL_DRUG).ColumnName & " Like '%" & strSearch & "%' OR " & Dv.Table.Columns(nCOL_DOSAGE).ColumnName & " Like '%" & strSearch & "%' OR " & Dv.Table.Columns(nCOL_ROUTE).ColumnName & " Like '%" & strSearch & "%' OR " & Dv.Table.Columns(nCOL_FREQUENCY).ColumnName & " Like '%" & strSearch & "%' OR " & Dv.Table.Columns(nCOL_DURATION).ColumnName & " Like '%" & strSearch & "%' OR " & Dv.Table.Columns(nCOL_STATUS).ColumnName & " Like '%" & strSearch & "%' OR " & Dv.Table.Columns(nCOL_REASON).ColumnName & " Like '%" & strSearch & "%'"
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

                            dvNext.RowFilter = dvNext.Table.Columns(nCOL_VISITDATE).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(nCOL_DRUG).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(nCOL_DOSAGE).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(nCOL_ROUTE).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(nCOL_FREQUENCY).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(nCOL_DURATION).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(nCOL_STATUS).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(nCOL_REASON).ColumnName & " Like '%" & strSearch & "%'"
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
    Private Sub _Flex_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _Flex.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub
    'Added by Shweta 20091127
    'Against salesForce Case No:GLO2009 0002946 
    'To calculate the end date only when duration has mention and it has to be numeric
    Public Function CalulateEndDate(ByVal dStartdate As Date, ByVal dDuration As String) As Date
        Dim dEndDate As Date
        Dim aDuration As Array
        aDuration = dDuration.Split(" ")
        Try
            '  If dDuration <> "" Then
            If aDuration.Length > 1 Then
                If IsNumeric(aDuration(0)) Then
                    Select Case aDuration(1)
                        Case "Days"
                            dEndDate = DateAdd(DateInterval.Day, CType(aDuration(0), Double), dStartdate)
                        Case "Months"
                            dEndDate = DateAdd(DateInterval.Month, aDuration(0), dStartdate)
                        Case "Weeks"
                            dEndDate = DateAdd(DateInterval.WeekOfYear, aDuration(0), dStartdate)
                    End Select
                End If
            Else
                dEndDate = DateAdd(DateInterval.Day, CType(aDuration(0), Double), dStartdate)
            End If

            '  End If
        Catch ex As Exception
            Dim objex As New gloUserControlLibrary.gloUserControlExceptions
            objex.ErrorMessage = "Error setting prescription details"
            Throw objex
        End Try
        Return dEndDate
    End Function
    'End 20091127
End Class

