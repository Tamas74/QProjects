Imports gloEMRGeneralLibrary.gloEMRActors
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports gloEMRGeneralLibrary.gloEMRMedication

Public Class CustomMedication
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "
    Public Threshold As Double
    Friend WithEvents cmbPharmacy As System.Windows.Forms.ComboBox
    Dim _MxGridNDCCode As String = ""
    Friend WithEvents pnlCustomTask As System.Windows.Forms.Panel
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label ''''''class level variable to set the drug data against the drug selected from medication grid and not from collection
    Private _nRxPatientid As Long
    Friend WithEvents chkCPOEOrder As System.Windows.Forms.CheckBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents ChkMedicationAdministered As System.Windows.Forms.CheckBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtRefusalResonCode As System.Windows.Forms.TextBox
    Friend WithEvents buttonSetRefusalCode As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents buttonClearRefusalCode As System.Windows.Forms.Button
    Friend WithEvents buttonClearCqmCategories As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtCqmCategories As System.Windows.Forms.TextBox
    Friend WithEvents buttonSetCqmCategories As System.Windows.Forms.Button
    Friend WithEvents lblMedicationNDCCode As System.Windows.Forms.Label
    Friend WithEvents panel6 As System.Windows.Forms.Panel
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents cmbRoute As System.Windows.Forms.ComboBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblRouteAstr As System.Windows.Forms.Label
    Private _gblnIncludeFrequencyAbbrevationInRxMeds As Boolean = False

    Public Sub New(ByVal objMedBusinessLayer As MedicationBusinessLayer, ByVal _RxRowItemnumber As Int32, Optional ByVal MxGridNDCCode As String = "", Optional ByVal _npatientid As Long = 0)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        _MxGridNDCCode = MxGridNDCCode

        'Add any initialization after the InitializeComponent() call
        _MedBusinessLayer = objMedBusinessLayer
        _RowIndex = _RxRowItemnumber ''RowIndex
        ProviderID = objMedBusinessLayer.MedicationCol.Item(_RxRowItemnumber).Rx_ProviderId
        'Duration
        cmbDuration.Items.Add("Days")
        cmbDuration.Items.Add("Weeks")
        cmbDuration.Items.Add("Months")
        cmbDuration.Text = cmbDuration.Items(0)

        'Added on 20090901 : Add method to combo
        CmbMethod.Items.Add("Fax")
        CmbMethod.Items.Add("Print")
        CmbMethod.Items.Add("Phone")
        CmbMethod.Items.Add("Sample")
        CmbMethod.Items.Add("HandWritten")
        CmbMethod.Items.Add("eRx")
        CmbMethod.Items.Add("OTC")
        _nRxPatientid = _npatientid
        SetData(_RxRowItemnumber) ''_MxGridNDCCode
        If Trim(txtDosage.Text) = "" Then
            txtDosage.Enabled = True
        Else
            txtDosage.Enabled = False
        End If
        If Trim(txtRoute.Text) = "" Then
            txtRoute.Enabled = True
        Else
            txtRoute.Enabled = False
        End If
    End Sub

    Private Sub SetData(ByVal _RxRowItemnumber As Integer) ''MxNDCCode 
        Dim strCmbDuration As String = ""

        Dim sNDCMxGrid As String = ""
        Dim sMedicationName As String = ""
        Dim sMedDosage As String = ""
        Dim sMedRoute As String = ""
        Dim sMedFreq As String = ""
        Dim sMedDuration As String = ""
        Dim SelectedItemInCol As Integer

        Try

            'Medication Status
            cmbStatus.Items.Add("Active")
            cmbStatus.Items.Add("Inactive")
            cmbStatus.Items.Add("Erroneous")
            cmbStatus.Items.Add("Completed")
            cmbStatus.Items.Add("Discontinued")
            cmbStatus.Items.Add("Unknown")
            cmbStatus.Items.Add("Aborted")
            cmbStatus.Items.Add("Held")
            cmbStatus.Items.Add("Normal")
            cmbStatus.Items.Add("New")
            cmbStatus.Items.Add("Nullified")
            cmbStatus.Items.Add("Obsolete")
            cmbStatus.Items.Add("Suspended")
            Dim _Medication As Medication = Nothing



            If _MedBusinessLayer.MedicationCol.Count > 0 Then '''''bug 6485

                For i As Integer = 0 To _MedBusinessLayer.MedicationCol.Count - 1
                    If _MedBusinessLayer.MedicationCol.Item(i).ItemNumber = _RxRowItemnumber Then
                        _Medication = _MedBusinessLayer.MedicationCol.Item(i)
                        SelectedItemInCol = i
                        Exit For
                    End If
                Next

            End If


            Me.Tag = _Medication.MedicationID
            Me.NDCCode = _Medication.NDCCode

            'Dim datarow As DataRow = GetRxnormInfo(_Medication.mpid)

            If Not String.IsNullOrEmpty(_Medication.RxNormCode) Then

                Dim datarow As DataRow = GetRxnormInfo(_Medication.RxNormCode, "RXCUI")
                If datarow IsNot Nothing AndAlso datarow("RxCUI") IsNot Nothing AndAlso Not datarow.IsNull("RxCUI") Then
                    panel6.Visible = True
                    Me.lblMedicationNDCCode.Text = " RxNorm : " + datarow("RxNormName") + " (" + datarow("RxCUI") + ")"
                End If
            Else

            End If

            Me.Caption = _Medication.Medication

            'Me.lblMedNDCCode.Text = _Medication.NDCCode


            Me.StartDate = _Medication.Startdate
            Me.Dosage = _Medication.Dosage
            If Not String.IsNullOrEmpty(Convert.ToString(_Medication.ReasonConceptID)) Then
                Me.strRefusalCode = Convert.ToString(_Medication.ReasonConceptID)
                Me.strRefusalDescription = Convert.ToString(_Medication.ReasonConceptDesc)
                Me.txtRefusalResonCode.Text = Convert.ToString(_Medication.ReasonConceptID) + " - " + Convert.ToString(_Medication.ReasonConceptDesc)
            End If

            If Not String.IsNullOrEmpty(Convert.ToString(_Medication.CQMCategories)) Then
                Me.strCQMCode = Convert.ToString(_Medication.CQMCategories)
                Me.strCQMDescription = Convert.ToString(_Medication.CQMDesc)
                Me.txtCqmCategories.Text = Convert.ToString(_Medication.CQMDesc)
            End If

            Me.Frequency = _Medication.Frequency
            blnCheckDb = True
            '''''if  there is value in Dosageform then make the combo box simple and assign the Dosage form value to combo box and make the combo box disabled
            If Not IsNothing(_Medication.DosageForm) AndAlso _Medication.DosageForm <> "" Then
                cmbDrugsForm.DropDownStyle = ComboBoxStyle.Simple
                cmbDrugsForm.Text = _Medication.DosageForm
                cmbDrugsForm.Enabled = False
            Else
                cmbDrugsForm.DropDownStyle = ComboBoxStyle.DropDownList
            End If

            'CCHIT 08
            'split the duration part from the combo box
            Dim strDuration As String = ""
            If Not IsNothing(_Medication.Duration) Then
                Dim retval As String() = SplitDrug(_Medication.Duration)

                If Not IsNothing(retval) Then
                    If retval.Length > 1 Then
                        'Against salesForce Case No:GLO2009 0002946 For calculating end date while refill Rx
                        'Commented By Shweta 20091127
                        'strDuration = retval(0)
                        'End Commenting
                        'Added By Shweta 20091127
                        'If the duration is alpha numeric or has more than one word then to get whole string 
                        For i As Integer = 0 To retval.Length - 2
                            strDuration = strDuration + " " + retval(i)
                        Next
                        'strDuration = retval(retval.Length)
                        strDuration = strDuration.Remove(0, 1)
                        'End Shweta
                        strCmbDuration = retval(retval.Length - 1)
                    Else
                        strDuration = _Medication.Duration.Trim
                    End If

                Else
                    strDuration = _Medication.Duration
                End If
            Else
                strDuration = ""
            End If

            If strCmbDuration <> "" Then

                If strCmbDuration.ToUpper() = "DAYS" Then
                    cmbDuration.SelectedIndex = 0 '0th item is Days
                ElseIf strCmbDuration.ToUpper() = "WEEKS" Then
                    cmbDuration.SelectedIndex = 1 '1st item is Weeks
                Else
                    cmbDuration.SelectedIndex = 2 '2nd item is Months
                End If
            End If
            Me.Duration = strDuration '_Medication.Duration
            'CCHIT 08

            ' Me.route = _Medication.Route

            'Using helper As New gloEMRGeneralLibrary.PrescriptionBusinessLayer()
            '    If Me.routes Is Nothing Then
            '        Me.routes = helper.GetDrugRoutes(_Medication.mpid)
            '    End If
            'End Using

            If _Medication.routes IsNot Nothing Then
                If (_Medication.routes.Count > 2) Then

                    cmbRoute.DataSource = _Medication.routes
                    If _Medication.Route <> "" Then
                        cmbRoute.SelectedIndex = cmbRoute.FindString(_Medication.Route)
                    End If
                    cmbRoute.Visible = True
                    txtRoute.Visible = False
                    lblRouteAstr.Visible = True
                Else
                    Me.route = _Medication.Route
                    cmbRoute.Visible = False
                    txtRoute.Visible = True
                    lblRouteAstr.Visible = False
                End If
            Else
                Me.route = _Medication.Route
                cmbRoute.Visible = False
                txtRoute.Visible = True
                lblRouteAstr.Visible = False
            End If


            If _Medication.Status = "" Then
                Me.Status = "Active"
            Else
                Me.Status = _Medication.Status
            End If

            Me.CheckEndDate = _Medication.CheckEndDate
            'cmbStatus.Text = _Medication.Status

            Me.Reason = _Medication.Reason

            ''GLO2011-0014767 : Quantity not being written out on prescriptions
            '' text value set to control property instead of textbox

            If _Medication.Amount <> "" Then 'fixed bug 5453
                Dim strDispense As String() = Split(_Medication.Amount, " ")
                If strDispense.Length > 1 Then
                    Me.Amount = strDispense(0)
                    ''txtDoseUnit.Text = strDispense(1)
                    Me.DoseUnit = strDispense(1)
                Else
                    Me.Amount = _Medication.Amount
                    '' txtDoseUnit.Text = ""
                    Me.DoseUnit = ""
                End If
            Else
                Me.Amount = _Medication.Amount
                ''txtDoseUnit.Text = ""
                Me.DoseUnit = ""
            End If
            ''end

            Me.mpid = _Medication.mpid



            'made the change for CCHIT. later code commented on 04 Dec 08 as per client requirement.
            'If _MedBusinessLayer.TransactionMode = MedicationBusinessLayer._TransactionMode.Edit Then
            '    'is this medication already in list
            '    If _Medication.MedicationID <> 0 Then
            '        'is there a value in db for enddate (its not null)
            '        dtpEndDate.Enabled = False
            '    Else
            '        dtpEndDate.Enabled = True
            '    End If
            'Else
            '    dtpEndDate.Enabled = True
            'End If
            If _Medication.CheckEndDate = True Then
                If Not IsNothing(Me.EndDate) Then
                    'assign db value to dtpenddate
                    Me.EndDate = _Medication.Enddate
                End If
            End If

            'added on 20090901 Rx Fields----------------
            Me.PharmacyNotes = _Medication.Rx_Notes 'this is for Pharmacy notes
            Me.PrescriberNotes = _Medication.Rx_PrescriberNotes 'this is for Prescriber notes
            Me.Refills = _Medication.Rx_Refills
            Me.Method = _Medication.Rx_Method
            'Me.txtPharmacy.Text = _Medication.Rx_PhName
            Dim dtPharma As DataTable
            Dim cnt As Int64 = 0
            dtPharma = GetPharmacyList(_nRxPatientid)
            If (IsNothing(dtPharma) = False) Then
                For i As Int64 = 0 To dtPharma.Rows.Count - 1
                    If (_Medication.Rx_PhName <> "") Then
                        If (dtPharma.Rows(i)("sName").ToString() = _Medication.Rx_PhName) Then

                            cnt = cnt + 1
                        End If
                    Else
                        cnt = -1
                    End If
                Next
                If (cnt = 0) Then
                    Dim dtRow As DataRow
                    dtRow = dtPharma.NewRow()
                    dtRow.Item(0) = _Medication.Rx_ContactID
                    dtRow.Item(1) = _Medication.Rx_PhName
                    dtPharma.Rows.Add(dtRow)
                End If
            End If


            Me.cmbPharmacy.DataSource = dtPharma
            If (IsNothing(dtPharma) = False) Then
                cmbPharmacy.ValueMember = dtPharma.Columns("nContactID").ColumnName
                cmbPharmacy.DisplayMember = dtPharma.Columns("sName").ColumnName
            End If

            'cmbPharmacy.Items.Add(_Medication.Rx_PhName)
            If (_Medication.Rx_PhName = "") Then
                Dim dtdefaultPharma As DataTable
                dtdefaultPharma = mdlGeneral.GetDefaultPatPhDetails(_nRxPatientid)
                If Not IsNothing(dtdefaultPharma) AndAlso dtdefaultPharma.Rows.Count > 0 Then
                    Me.cmbPharmacy.Text = dtdefaultPharma.Rows(0)("PharmacyName")
                    Me.m_PharmacyID = dtdefaultPharma.Rows(0)("PharmacyID")
                    Me.NCPDPID = dtdefaultPharma.Rows(0)("NCPDPID")
                End If
                If Not IsNothing(dtdefaultPharma) Then
                    dtdefaultPharma.Dispose()
                    dtdefaultPharma = Nothing
                End If
            Else
                cmbPharmacy.Text = _Medication.Rx_PhName
                Me.m_PharmacyID = cmbPharmacy.SelectedValue
                Me.NCPDPID = _Medication.Rx_NCPDPID
            End If



            If IsNothing(_MedBusinessLayer.MedicationCol.Item(SelectedItemInCol).MedicationID) Then ''_RowIndex - 1
                If _Medication.Rx_Method = "" Then
                    Me.Method = CmbMethod.Items(1)

                Else
                    Me.Method = _Medication.Rx_Method
                    CmbMethod.SelectedItem = _Medication.Rx_Method
                End If

            ElseIf _MedBusinessLayer.MedicationCol.Item(SelectedItemInCol).MedicationID = 0 Then
                If _Medication.Rx_Method = "" Then
                    'Me.Method = ""
                    'Me.Method = CmbMethod.Items(1)
                Else
                    Me.Method = _Medication.Rx_Method
                    CmbMethod.SelectedItem = _Medication.Rx_Method
                End If
            Else
                Me.Method = _Medication.Rx_Method
                CmbMethod.SelectedItem = _Medication.Rx_Method
            End If
            '---------------------------------------
            If _Medication._PrescriptionId <> 0 Then
                If _Medication.Rx_CPOEOrder = True Then
                    Me.chkCPOEOrder.Checked = True
                End If
            Else
                Me.chkCPOEOrder.Visible = False
            End If

            ChkMedicationAdministered.Checked = _Medication.Rx_MedicationAdministered

            '''''EpcsChange
            If _Medication.IsNarcotics = 2 Then
                txtRefills.Enabled = False
            End If

            'EpcsChange....
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Dim objex As New gloUserControlLibrary.gloUserControlExceptions
            objex.ErrorMessage = "Error setting Medication details"
            Throw objex
        End Try
    End Sub

    'SHUBHANGI 20110104
    Private Function GetPharmacyList(ByVal PatientID As Int64) As DataTable
        Dim _dtPharmacy As DataTable
        Dim _gloEMRDatabase As New DataBaseLayer
        Dim _strSQL As String = ""
        Try
            _strSQL = "SELECT nContactID,ISNULL(sName,'') as sName FROM Patient_DTL where nPatientID = " & PatientID & " and sName <> '' AND nContactFlag = 1 "
            _dtPharmacy = _gloEMRDatabase.GetDataTable_Query(_strSQL)
            Return _dtPharmacy
        Catch ex As Exception
            Return Nothing
        Finally
            If (IsNothing(_gloEMRDatabase) = False) Then
                _gloEMRDatabase.Dispose()
                _gloEMRDatabase = Nothing
            End If

        End Try
    End Function
    ''METHOD TO ADD NEWLY ADDED PHARMACY INTO COMBO BOX 20110118
    Public Sub SetPharmacyData(ByVal dt As DataTable, ByVal PatientId As Int64)
        Dim cnt As Int64 = 0
        Dim dtcmbPharma As DataTable = Nothing
        Try

            dtcmbPharma = GetPharmacyList(PatientId)
            If Not IsNothing(dtcmbPharma) Then
                For i As Int64 = 0 To dtcmbPharma.Rows.Count - 1
                    If (dt.Rows(0)("nContactID") <> "") Then
                        If (dtcmbPharma.Rows(i)("nContactID").ToString() = dt.Rows(0)("nContactID").ToString()) Then
                            cnt = cnt + 1
                        End If
                    Else
                        cnt = -1
                    End If
                Next
                If (cnt = 0) Then
                    Dim dtRow As DataRow
                    dtRow = dtcmbPharma.NewRow()
                    dtRow.Item(0) = dt.Rows(0)("nContactID").ToString()
                    dtRow.Item(1) = dt.Rows(0)("sName").ToString()
                    dtcmbPharma.Rows.Add(dtRow)
                End If

                Me.cmbPharmacy.DataSource = Nothing
                Me.cmbPharmacy.Items.Clear()
                Me.cmbPharmacy.DataSource = dtcmbPharma
                Me.cmbPharmacy.ValueMember = dtcmbPharma.Columns("nContactID").ColumnName
                Me.cmbPharmacy.DisplayMember = dtcmbPharma.Columns("sName").ColumnName
                Me.cmbPharmacy.Text = dt.Rows(0)("sName").ToString()
            Else

                Me.cmbPharmacy.DataSource = Nothing
                Me.cmbPharmacy.Items.Clear()
            End If




        Catch ex As Exception
            If Not IsNothing(dtcmbPharma) Then
                dtcmbPharma.Dispose()
                dtcmbPharma = Nothing
            End If
        Finally

        End Try
    End Sub
    'END
    'CCHIT 08
    Private Function SplitDrug(ByVal _strDuration As String) As Array
        Try
            Dim _result As String()
            _result = _strDuration.Split(" ")
            Return _result
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        End Try

    End Function
    'CCHIT 08

    Private Function SplitMedInfo(ByVal MedInfo As String) As Array
        Try
            Dim _result As String()
            _result = MedInfo.Split("~")
            Return _result
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        End Try

    End Function


    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then


            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)

        Dim dtpControls As System.Windows.Forms.DateTimePicker() = {dtpEndDate, dtpStartDate}
        Dim cntControls As System.Windows.Forms.DateTimePicker() = {dtpEndDate, dtpStartDate}

        Try
            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
            gloGlobal.cEventHelper.DisposeAllControls(cntControls)
        Catch

        End Try

    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents dtpEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnSig As System.Windows.Forms.Button
    Friend WithEvents txtDuration As System.Windows.Forms.TextBox
    Friend WithEvents txtFrequency As System.Windows.Forms.TextBox
    Friend WithEvents txtRoute As System.Windows.Forms.TextBox
    Friend WithEvents txtDosage As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents txtReason As System.Windows.Forms.TextBox
    Private WithEvents pnl_Base As System.Windows.Forms.Panel
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Private WithEvents pnl_tlspTOP As System.Windows.Forms.Panel
    Private WithEvents tls As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnOk As System.Windows.Forms.ToolStripButton
    Private WithEvents ts_btnCancel As System.Windows.Forms.ToolStripButton
    Private WithEvents pnl_Caption1 As System.Windows.Forms.Panel
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents lblCaption As System.Windows.Forms.Label
    Friend WithEvents cmbDuration As System.Windows.Forms.ComboBox
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents pnl_Caption As System.Windows.Forms.Panel
    Friend WithEvents GRPbox_PriscriberNotes As System.Windows.Forms.GroupBox
    Friend WithEvents txtPrescriberNotes As System.Windows.Forms.TextBox
    Friend WithEvents GRPbox_PharmacyNotes As System.Windows.Forms.GroupBox
    Friend WithEvents txtNotes As System.Windows.Forms.TextBox
    Friend WithEvents CmbMethod As System.Windows.Forms.ComboBox
    Friend WithEvents lblmethod As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtRefills As System.Windows.Forms.TextBox
    Private WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents lblPharmacy As System.Windows.Forms.Label
    Friend WithEvents txtPharmacy As System.Windows.Forms.TextBox
    Friend WithEvents btnSelectPharmacy As System.Windows.Forms.Button
    Friend WithEvents txtDoseUnit As System.Windows.Forms.TextBox
    Friend WithEvents cmbDrugsForm As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CustomMedication))
        Me.txtReason = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.dtpEndDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpStartDate = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnSig = New System.Windows.Forms.Button()
        Me.txtDuration = New System.Windows.Forms.TextBox()
        Me.txtFrequency = New System.Windows.Forms.TextBox()
        Me.txtRoute = New System.Windows.Forms.TextBox()
        Me.txtDosage = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnl_Base = New System.Windows.Forms.Panel()
        Me.cmbRoute = New System.Windows.Forms.ComboBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.buttonClearCqmCategories = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtCqmCategories = New System.Windows.Forms.TextBox()
        Me.buttonSetCqmCategories = New System.Windows.Forms.Button()
        Me.buttonClearRefusalCode = New System.Windows.Forms.Button()
        Me.pnlCustomTask = New System.Windows.Forms.Panel()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtRefusalResonCode = New System.Windows.Forms.TextBox()
        Me.buttonSetRefusalCode = New System.Windows.Forms.Button()
        Me.ChkMedicationAdministered = New System.Windows.Forms.CheckBox()
        Me.chkCPOEOrder = New System.Windows.Forms.CheckBox()
        Me.cmbPharmacy = New System.Windows.Forms.ComboBox()
        Me.cmbDrugsForm = New System.Windows.Forms.ComboBox()
        Me.txtDoseUnit = New System.Windows.Forms.TextBox()
        Me.btnSelectPharmacy = New System.Windows.Forms.Button()
        Me.txtPharmacy = New System.Windows.Forms.TextBox()
        Me.lblPharmacy = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtRefills = New System.Windows.Forms.TextBox()
        Me.CmbMethod = New System.Windows.Forms.ComboBox()
        Me.lblmethod = New System.Windows.Forms.Label()
        Me.GRPbox_PriscriberNotes = New System.Windows.Forms.GroupBox()
        Me.txtPrescriberNotes = New System.Windows.Forms.TextBox()
        Me.GRPbox_PharmacyNotes = New System.Windows.Forms.GroupBox()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.cmbDuration = New System.Windows.Forms.ComboBox()
        Me.lbl_pnlBottom = New System.Windows.Forms.Label()
        Me.lbl_pnlLeft = New System.Windows.Forms.Label()
        Me.lbl_pnlTop = New System.Windows.Forms.Label()
        Me.lblRouteAstr = New System.Windows.Forms.Label()
        Me.pnl_tlspTOP = New System.Windows.Forms.Panel()
        Me.tls = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnOk = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnCancel = New System.Windows.Forms.ToolStripButton()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.pnl_Caption1 = New System.Windows.Forms.Panel()
        Me.lblCaption = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblMedicationNDCCode = New System.Windows.Forms.Label()
        Me.pnl_Caption = New System.Windows.Forms.Panel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.panel6 = New System.Windows.Forms.Panel()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.pnl_Base.SuspendLayout()
        Me.pnlCustomTask.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GRPbox_PriscriberNotes.SuspendLayout()
        Me.GRPbox_PharmacyNotes.SuspendLayout()
        Me.pnl_tlspTOP.SuspendLayout()
        Me.tls.SuspendLayout()
        Me.pnl_Caption1.SuspendLayout()
        Me.pnl_Caption.SuspendLayout()
        Me.panel6.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtReason
        '
        Me.txtReason.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtReason.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReason.ForeColor = System.Drawing.Color.Black
        Me.txtReason.Location = New System.Drawing.Point(3, 18)
        Me.txtReason.MaxLength = 255
        Me.txtReason.Multiline = True
        Me.txtReason.Name = "txtReason"
        Me.txtReason.Size = New System.Drawing.Size(200, 49)
        Me.txtReason.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(421, 94)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(50, 14)
        Me.Label4.TabIndex = 62
        Me.Label4.Text = "Status :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbStatus
        '
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbStatus.ForeColor = System.Drawing.Color.Black
        Me.cmbStatus.Location = New System.Drawing.Point(474, 90)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(163, 22)
        Me.cmbStatus.TabIndex = 10
        '
        'dtpEndDate
        '
        Me.dtpEndDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpEndDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpEndDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpEndDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpEndDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpEndDate.Checked = False
        Me.dtpEndDate.CustomFormat = "MM/dd/yyyy"
        Me.dtpEndDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEndDate.Location = New System.Drawing.Point(139, 169)
        Me.dtpEndDate.Name = "dtpEndDate"
        Me.dtpEndDate.ShowCheckBox = True
        Me.dtpEndDate.Size = New System.Drawing.Size(223, 22)
        Me.dtpEndDate.TabIndex = 16
        '
        'dtpStartDate
        '
        Me.dtpStartDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpStartDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpStartDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpStartDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpStartDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpStartDate.CustomFormat = "MM/dd/yyyy"
        Me.dtpStartDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpStartDate.Location = New System.Drawing.Point(139, 142)
        Me.dtpStartDate.Name = "dtpStartDate"
        Me.dtpStartDate.Size = New System.Drawing.Size(222, 22)
        Me.dtpStartDate.TabIndex = 15
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(69, 173)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(66, 14)
        Me.Label8.TabIndex = 60
        Me.Label8.Text = "End Date :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(63, 146)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 14)
        Me.Label7.TabIndex = 57
        Me.Label7.Text = "Start Date :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtAmount
        '
        Me.txtAmount.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount.ForeColor = System.Drawing.Color.Black
        Me.txtAmount.Location = New System.Drawing.Point(138, 115)
        Me.txtAmount.MaxLength = 255
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.ShortcutsEnabled = False
        Me.txtAmount.Size = New System.Drawing.Size(70, 22)
        Me.txtAmount.TabIndex = 11
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(73, 119)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(62, 14)
        Me.Label9.TabIndex = 56
        Me.Label9.Text = "Quantity :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnSig
        '
        Me.btnSig.BackColor = System.Drawing.Color.White
        Me.btnSig.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_LongButton
        Me.btnSig.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSig.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSig.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSig.Image = CType(resources.GetObject("btnSig.Image"), System.Drawing.Image)
        Me.btnSig.Location = New System.Drawing.Point(365, 34)
        Me.btnSig.Name = "btnSig"
        Me.btnSig.Size = New System.Drawing.Size(22, 22)
        Me.btnSig.TabIndex = 3
        Me.btnSig.UseVisualStyleBackColor = False
        '
        'txtDuration
        '
        Me.txtDuration.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDuration.ForeColor = System.Drawing.Color.Black
        Me.txtDuration.Location = New System.Drawing.Point(138, 88)
        Me.txtDuration.MaxLength = 3
        Me.txtDuration.Name = "txtDuration"
        Me.txtDuration.ShortcutsEnabled = False
        Me.txtDuration.Size = New System.Drawing.Size(134, 22)
        Me.txtDuration.TabIndex = 8
        '
        'txtFrequency
        '
        Me.txtFrequency.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFrequency.ForeColor = System.Drawing.Color.Black
        Me.txtFrequency.Location = New System.Drawing.Point(138, 61)
        Me.txtFrequency.MaxLength = 255
        Me.txtFrequency.Name = "txtFrequency"
        Me.txtFrequency.Size = New System.Drawing.Size(224, 22)
        Me.txtFrequency.TabIndex = 5
        '
        'txtRoute
        '
        Me.txtRoute.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRoute.ForeColor = System.Drawing.Color.Black
        Me.txtRoute.Location = New System.Drawing.Point(138, 34)
        Me.txtRoute.MaxLength = 255
        Me.txtRoute.Name = "txtRoute"
        Me.txtRoute.Size = New System.Drawing.Size(224, 22)
        Me.txtRoute.TabIndex = 2
        '
        'txtDosage
        '
        Me.txtDosage.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDosage.ForeColor = System.Drawing.Color.Black
        Me.txtDosage.Location = New System.Drawing.Point(138, 8)
        Me.txtDosage.MaxLength = 255
        Me.txtDosage.Name = "txtDosage"
        Me.txtDosage.Size = New System.Drawing.Size(224, 22)
        Me.txtDosage.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(74, 92)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(61, 14)
        Me.Label5.TabIndex = 53
        Me.Label5.Text = "Duration :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(20, 65)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(115, 14)
        Me.Label3.TabIndex = 51
        Me.Label3.Text = "Patient Directions  :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(87, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 14)
        Me.Label2.TabIndex = 49
        Me.Label2.Text = "Route :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(80, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 14)
        Me.Label1.TabIndex = 47
        Me.Label1.Text = "Dosage :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnl_Base
        '
        Me.pnl_Base.AutoScroll = True
        Me.pnl_Base.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_Base.Controls.Add(Me.cmbRoute)
        Me.pnl_Base.Controls.Add(Me.Panel1)
        Me.pnl_Base.Controls.Add(Me.buttonClearCqmCategories)
        Me.pnl_Base.Controls.Add(Me.Label6)
        Me.pnl_Base.Controls.Add(Me.txtCqmCategories)
        Me.pnl_Base.Controls.Add(Me.buttonSetCqmCategories)
        Me.pnl_Base.Controls.Add(Me.buttonClearRefusalCode)
        Me.pnl_Base.Controls.Add(Me.pnlCustomTask)
        Me.pnl_Base.Controls.Add(Me.GroupBox1)
        Me.pnl_Base.Controls.Add(Me.Label22)
        Me.pnl_Base.Controls.Add(Me.txtRefusalResonCode)
        Me.pnl_Base.Controls.Add(Me.buttonSetRefusalCode)
        Me.pnl_Base.Controls.Add(Me.ChkMedicationAdministered)
        Me.pnl_Base.Controls.Add(Me.chkCPOEOrder)
        Me.pnl_Base.Controls.Add(Me.cmbPharmacy)
        Me.pnl_Base.Controls.Add(Me.cmbDrugsForm)
        Me.pnl_Base.Controls.Add(Me.txtDoseUnit)
        Me.pnl_Base.Controls.Add(Me.btnSelectPharmacy)
        Me.pnl_Base.Controls.Add(Me.txtPharmacy)
        Me.pnl_Base.Controls.Add(Me.lblPharmacy)
        Me.pnl_Base.Controls.Add(Me.Label18)
        Me.pnl_Base.Controls.Add(Me.Label17)
        Me.pnl_Base.Controls.Add(Me.txtRefills)
        Me.pnl_Base.Controls.Add(Me.CmbMethod)
        Me.pnl_Base.Controls.Add(Me.lblmethod)
        Me.pnl_Base.Controls.Add(Me.GRPbox_PriscriberNotes)
        Me.pnl_Base.Controls.Add(Me.GRPbox_PharmacyNotes)
        Me.pnl_Base.Controls.Add(Me.cmbDuration)
        Me.pnl_Base.Controls.Add(Me.lbl_pnlBottom)
        Me.pnl_Base.Controls.Add(Me.Label4)
        Me.pnl_Base.Controls.Add(Me.lbl_pnlLeft)
        Me.pnl_Base.Controls.Add(Me.cmbStatus)
        Me.pnl_Base.Controls.Add(Me.lbl_pnlTop)
        Me.pnl_Base.Controls.Add(Me.Label9)
        Me.pnl_Base.Controls.Add(Me.dtpEndDate)
        Me.pnl_Base.Controls.Add(Me.Label1)
        Me.pnl_Base.Controls.Add(Me.dtpStartDate)
        Me.pnl_Base.Controls.Add(Me.Label2)
        Me.pnl_Base.Controls.Add(Me.Label8)
        Me.pnl_Base.Controls.Add(Me.Label3)
        Me.pnl_Base.Controls.Add(Me.Label7)
        Me.pnl_Base.Controls.Add(Me.Label5)
        Me.pnl_Base.Controls.Add(Me.txtAmount)
        Me.pnl_Base.Controls.Add(Me.txtDosage)
        Me.pnl_Base.Controls.Add(Me.txtRoute)
        Me.pnl_Base.Controls.Add(Me.btnSig)
        Me.pnl_Base.Controls.Add(Me.txtFrequency)
        Me.pnl_Base.Controls.Add(Me.txtDuration)
        Me.pnl_Base.Controls.Add(Me.lblRouteAstr)
        Me.pnl_Base.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_Base.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_Base.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_Base.Location = New System.Drawing.Point(0, 111)
        Me.pnl_Base.Name = "pnl_Base"
        Me.pnl_Base.Size = New System.Drawing.Size(1047, 255)
        Me.pnl_Base.TabIndex = 0
        '
        'cmbRoute
        '
        Me.cmbRoute.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(6, Byte), Integer), CType(CType(6, Byte), Integer))
        Me.cmbRoute.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbRoute.ForeColor = System.Drawing.Color.White
        Me.cmbRoute.ItemHeight = 14
        Me.cmbRoute.Location = New System.Drawing.Point(138, 34)
        Me.cmbRoute.Margin = New System.Windows.Forms.Padding(8, 3, 3, 3)
        Me.cmbRoute.Name = "cmbRoute"
        Me.cmbRoute.Size = New System.Drawing.Size(224, 22)
        Me.cmbRoute.TabIndex = 311
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Location = New System.Drawing.Point(405, 169)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(224, 22)
        Me.Panel1.TabIndex = 312
        '
        'buttonClearCqmCategories
        '
        Me.buttonClearCqmCategories.BackColor = System.Drawing.Color.Transparent
        Me.buttonClearCqmCategories.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Button
        Me.buttonClearCqmCategories.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.buttonClearCqmCategories.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttonClearCqmCategories.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.buttonClearCqmCategories.Image = CType(resources.GetObject("buttonClearCqmCategories.Image"), System.Drawing.Image)
        Me.buttonClearCqmCategories.Location = New System.Drawing.Point(606, 224)
        Me.buttonClearCqmCategories.Name = "buttonClearCqmCategories"
        Me.buttonClearCqmCategories.Size = New System.Drawing.Size(22, 22)
        Me.buttonClearCqmCategories.TabIndex = 310
        Me.buttonClearCqmCategories.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(34, 228)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(101, 14)
        Me.Label6.TabIndex = 309
        Me.Label6.Text = "CQM Categories :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCqmCategories
        '
        Me.txtCqmCategories.Enabled = False
        Me.txtCqmCategories.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCqmCategories.ForeColor = System.Drawing.Color.Black
        Me.txtCqmCategories.Location = New System.Drawing.Point(138, 224)
        Me.txtCqmCategories.MaxLength = 255
        Me.txtCqmCategories.Name = "txtCqmCategories"
        Me.txtCqmCategories.Size = New System.Drawing.Size(438, 22)
        Me.txtCqmCategories.TabIndex = 308
        '
        'buttonSetCqmCategories
        '
        Me.buttonSetCqmCategories.BackColor = System.Drawing.Color.Transparent
        Me.buttonSetCqmCategories.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Button
        Me.buttonSetCqmCategories.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.buttonSetCqmCategories.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttonSetCqmCategories.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.buttonSetCqmCategories.Image = CType(resources.GetObject("buttonSetCqmCategories.Image"), System.Drawing.Image)
        Me.buttonSetCqmCategories.Location = New System.Drawing.Point(580, 224)
        Me.buttonSetCqmCategories.Name = "buttonSetCqmCategories"
        Me.buttonSetCqmCategories.Size = New System.Drawing.Size(22, 22)
        Me.buttonSetCqmCategories.TabIndex = 307
        Me.buttonSetCqmCategories.UseVisualStyleBackColor = False
        '
        'buttonClearRefusalCode
        '
        Me.buttonClearRefusalCode.BackColor = System.Drawing.Color.Transparent
        Me.buttonClearRefusalCode.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Button
        Me.buttonClearRefusalCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.buttonClearRefusalCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttonClearRefusalCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.buttonClearRefusalCode.Image = CType(resources.GetObject("buttonClearRefusalCode.Image"), System.Drawing.Image)
        Me.buttonClearRefusalCode.Location = New System.Drawing.Point(606, 196)
        Me.buttonClearRefusalCode.Name = "buttonClearRefusalCode"
        Me.buttonClearRefusalCode.Size = New System.Drawing.Size(22, 22)
        Me.buttonClearRefusalCode.TabIndex = 306
        Me.buttonClearRefusalCode.UseVisualStyleBackColor = False
        '
        'pnlCustomTask
        '
        Me.pnlCustomTask.Controls.Add(Me.Label16)
        Me.pnlCustomTask.Controls.Add(Me.Label19)
        Me.pnlCustomTask.Controls.Add(Me.Label20)
        Me.pnlCustomTask.Controls.Add(Me.Label21)
        Me.pnlCustomTask.Location = New System.Drawing.Point(366, 61)
        Me.pnlCustomTask.Name = "pnlCustomTask"
        Me.pnlCustomTask.Size = New System.Drawing.Size(196, 74)
        Me.pnlCustomTask.TabIndex = 302
        Me.pnlCustomTask.TabStop = True
        Me.pnlCustomTask.Visible = False
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(1, 73)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(194, 1)
        Me.Label16.TabIndex = 301
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(1, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(194, 1)
        Me.Label19.TabIndex = 300
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(195, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(1, 74)
        Me.Label20.TabIndex = 299
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(0, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(1, 74)
        Me.Label21.TabIndex = 298
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtReason)
        Me.GroupBox1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(669, 152)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(206, 70)
        Me.GroupBox1.TabIndex = 22
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Reason"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Location = New System.Drawing.Point(6, 200)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(129, 14)
        Me.Label22.TabIndex = 305
        Me.Label22.Text = "Refusal/Reason Code :"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRefusalResonCode
        '
        Me.txtRefusalResonCode.Enabled = False
        Me.txtRefusalResonCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefusalResonCode.ForeColor = System.Drawing.Color.Black
        Me.txtRefusalResonCode.Location = New System.Drawing.Point(138, 196)
        Me.txtRefusalResonCode.MaxLength = 255
        Me.txtRefusalResonCode.Name = "txtRefusalResonCode"
        Me.txtRefusalResonCode.Size = New System.Drawing.Size(438, 22)
        Me.txtRefusalResonCode.TabIndex = 19
        '
        'buttonSetRefusalCode
        '
        Me.buttonSetRefusalCode.BackColor = System.Drawing.Color.Transparent
        Me.buttonSetRefusalCode.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Button
        Me.buttonSetRefusalCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.buttonSetRefusalCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.buttonSetRefusalCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.buttonSetRefusalCode.Image = CType(resources.GetObject("buttonSetRefusalCode.Image"), System.Drawing.Image)
        Me.buttonSetRefusalCode.Location = New System.Drawing.Point(580, 196)
        Me.buttonSetRefusalCode.Name = "buttonSetRefusalCode"
        Me.buttonSetRefusalCode.Size = New System.Drawing.Size(22, 22)
        Me.buttonSetRefusalCode.TabIndex = 18
        Me.buttonSetRefusalCode.UseVisualStyleBackColor = False
        '
        'ChkMedicationAdministered
        '
        Me.ChkMedicationAdministered.AutoSize = True
        Me.ChkMedicationAdministered.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkMedicationAdministered.Location = New System.Drawing.Point(474, 118)
        Me.ChkMedicationAdministered.Name = "ChkMedicationAdministered"
        Me.ChkMedicationAdministered.Size = New System.Drawing.Size(97, 18)
        Me.ChkMedicationAdministered.TabIndex = 14
        Me.ChkMedicationAdministered.Text = "Administered"
        Me.ChkMedicationAdministered.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkCPOEOrder
        '
        Me.chkCPOEOrder.AutoSize = True
        Me.chkCPOEOrder.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCPOEOrder.Location = New System.Drawing.Point(474, 142)
        Me.chkCPOEOrder.Name = "chkCPOEOrder"
        Me.chkCPOEOrder.Size = New System.Drawing.Size(115, 18)
        Me.chkCPOEOrder.TabIndex = 17
        Me.chkCPOEOrder.Text = "Order Not CPOE"
        Me.chkCPOEOrder.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.chkCPOEOrder, "MU Reporting: Recording of a previous order from this practice. ")
        '
        'cmbPharmacy
        '
        Me.cmbPharmacy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPharmacy.FormattingEnabled = True
        Me.cmbPharmacy.Location = New System.Drawing.Point(474, 63)
        Me.cmbPharmacy.Name = "cmbPharmacy"
        Me.cmbPharmacy.Size = New System.Drawing.Size(164, 21)
        Me.cmbPharmacy.TabIndex = 7
        '
        'cmbDrugsForm
        '
        Me.cmbDrugsForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDrugsForm.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.cmbDrugsForm.FormattingEnabled = True
        Me.cmbDrugsForm.Location = New System.Drawing.Point(275, 115)
        Me.cmbDrugsForm.Name = "cmbDrugsForm"
        Me.cmbDrugsForm.Size = New System.Drawing.Size(87, 22)
        Me.cmbDrugsForm.TabIndex = 13
        '
        'txtDoseUnit
        '
        Me.txtDoseUnit.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDoseUnit.ForeColor = System.Drawing.Color.Black
        Me.txtDoseUnit.Location = New System.Drawing.Point(214, 115)
        Me.txtDoseUnit.MaxLength = 30
        Me.txtDoseUnit.Name = "txtDoseUnit"
        Me.txtDoseUnit.ShortcutsEnabled = False
        Me.txtDoseUnit.Size = New System.Drawing.Size(58, 22)
        Me.txtDoseUnit.TabIndex = 12
        '
        'btnSelectPharmacy
        '
        Me.btnSelectPharmacy.BackColor = System.Drawing.Color.Transparent
        Me.btnSelectPharmacy.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Button
        Me.btnSelectPharmacy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSelectPharmacy.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelectPharmacy.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelectPharmacy.Image = CType(resources.GetObject("btnSelectPharmacy.Image"), System.Drawing.Image)
        Me.btnSelectPharmacy.Location = New System.Drawing.Point(641, 62)
        Me.btnSelectPharmacy.Name = "btnSelectPharmacy"
        Me.btnSelectPharmacy.Size = New System.Drawing.Size(22, 22)
        Me.btnSelectPharmacy.TabIndex = 6
        Me.btnSelectPharmacy.UseVisualStyleBackColor = False
        '
        'txtPharmacy
        '
        Me.txtPharmacy.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPharmacy.ForeColor = System.Drawing.Color.Black
        Me.txtPharmacy.Location = New System.Drawing.Point(931, 245)
        Me.txtPharmacy.MaxLength = 70
        Me.txtPharmacy.Name = "txtPharmacy"
        Me.txtPharmacy.Size = New System.Drawing.Size(63, 22)
        Me.txtPharmacy.TabIndex = 13
        Me.txtPharmacy.Visible = False
        '
        'lblPharmacy
        '
        Me.lblPharmacy.AutoSize = True
        Me.lblPharmacy.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPharmacy.Location = New System.Drawing.Point(404, 68)
        Me.lblPharmacy.Name = "lblPharmacy"
        Me.lblPharmacy.Size = New System.Drawing.Size(67, 14)
        Me.lblPharmacy.TabIndex = 74
        Me.lblPharmacy.Text = "Pharmacy :"
        Me.lblPharmacy.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label18.Location = New System.Drawing.Point(1029, 1)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 266)
        Me.Label18.TabIndex = 72
        Me.Label18.Text = "label3"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(427, 40)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(44, 14)
        Me.Label17.TabIndex = 71
        Me.Label17.Text = "Refills :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRefills
        '
        Me.txtRefills.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefills.ForeColor = System.Drawing.Color.Black
        Me.txtRefills.Location = New System.Drawing.Point(474, 35)
        Me.txtRefills.MaxLength = 3
        Me.txtRefills.Name = "txtRefills"
        Me.txtRefills.Size = New System.Drawing.Size(164, 22)
        Me.txtRefills.TabIndex = 4
        '
        'CmbMethod
        '
        Me.CmbMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbMethod.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbMethod.ForeColor = System.Drawing.Color.Black
        Me.CmbMethod.Location = New System.Drawing.Point(474, 7)
        Me.CmbMethod.Name = "CmbMethod"
        Me.CmbMethod.Size = New System.Drawing.Size(164, 22)
        Me.CmbMethod.TabIndex = 1
        '
        'lblmethod
        '
        Me.lblmethod.AutoSize = True
        Me.lblmethod.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblmethod.Location = New System.Drawing.Point(382, 12)
        Me.lblmethod.Name = "lblmethod"
        Me.lblmethod.Size = New System.Drawing.Size(89, 14)
        Me.lblmethod.TabIndex = 69
        Me.lblmethod.Text = "Issue Method :"
        Me.lblmethod.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GRPbox_PriscriberNotes
        '
        Me.GRPbox_PriscriberNotes.Controls.Add(Me.txtPrescriberNotes)
        Me.GRPbox_PriscriberNotes.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRPbox_PriscriberNotes.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GRPbox_PriscriberNotes.Location = New System.Drawing.Point(669, 78)
        Me.GRPbox_PriscriberNotes.Name = "GRPbox_PriscriberNotes"
        Me.GRPbox_PriscriberNotes.Size = New System.Drawing.Size(206, 70)
        Me.GRPbox_PriscriberNotes.TabIndex = 21
        Me.GRPbox_PriscriberNotes.TabStop = False
        Me.GRPbox_PriscriberNotes.Text = "Prescriber Notes"
        '
        'txtPrescriberNotes
        '
        Me.txtPrescriberNotes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtPrescriberNotes.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrescriberNotes.ForeColor = System.Drawing.Color.Black
        Me.txtPrescriberNotes.Location = New System.Drawing.Point(3, 18)
        Me.txtPrescriberNotes.MaxLength = 210
        Me.txtPrescriberNotes.Multiline = True
        Me.txtPrescriberNotes.Name = "txtPrescriberNotes"
        Me.txtPrescriberNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPrescriberNotes.Size = New System.Drawing.Size(200, 49)
        Me.txtPrescriberNotes.TabIndex = 0
        '
        'GRPbox_PharmacyNotes
        '
        Me.GRPbox_PharmacyNotes.Controls.Add(Me.txtNotes)
        Me.GRPbox_PharmacyNotes.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GRPbox_PharmacyNotes.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GRPbox_PharmacyNotes.Location = New System.Drawing.Point(669, 4)
        Me.GRPbox_PharmacyNotes.Name = "GRPbox_PharmacyNotes"
        Me.GRPbox_PharmacyNotes.Size = New System.Drawing.Size(206, 70)
        Me.GRPbox_PharmacyNotes.TabIndex = 20
        Me.GRPbox_PharmacyNotes.TabStop = False
        Me.GRPbox_PharmacyNotes.Text = "Pharmacy Notes"
        '
        'txtNotes
        '
        Me.txtNotes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtNotes.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNotes.ForeColor = System.Drawing.Color.Black
        Me.txtNotes.Location = New System.Drawing.Point(3, 18)
        Me.txtNotes.MaxLength = 210
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNotes.Size = New System.Drawing.Size(200, 49)
        Me.txtNotes.TabIndex = 0
        '
        'cmbDuration
        '
        Me.cmbDuration.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDuration.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDuration.Location = New System.Drawing.Point(275, 88)
        Me.cmbDuration.Name = "cmbDuration"
        Me.cmbDuration.Size = New System.Drawing.Size(87, 22)
        Me.cmbDuration.TabIndex = 9
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(1, 267)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(1029, 1)
        Me.lbl_pnlBottom.TabIndex = 4
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(0, 1)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 267)
        Me.lbl_pnlLeft.TabIndex = 3
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(1030, 1)
        Me.lbl_pnlTop.TabIndex = 0
        Me.lbl_pnlTop.Text = "label1"
        '
        'lblRouteAstr
        '
        Me.lblRouteAstr.AutoSize = True
        Me.lblRouteAstr.BackColor = System.Drawing.Color.Transparent
        Me.lblRouteAstr.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRouteAstr.ForeColor = System.Drawing.Color.Red
        Me.lblRouteAstr.Location = New System.Drawing.Point(76, 38)
        Me.lblRouteAstr.Name = "lblRouteAstr"
        Me.lblRouteAstr.Size = New System.Drawing.Size(14, 14)
        Me.lblRouteAstr.TabIndex = 313
        Me.lblRouteAstr.Text = "*"
        Me.lblRouteAstr.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnl_tlspTOP
        '
        Me.pnl_tlspTOP.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.pnl_tlspTOP.Controls.Add(Me.tls)
        Me.pnl_tlspTOP.Controls.Add(Me.Label11)
        Me.pnl_tlspTOP.Controls.Add(Me.Label12)
        Me.pnl_tlspTOP.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlspTOP.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnl_tlspTOP.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlspTOP.Name = "pnl_tlspTOP"
        Me.pnl_tlspTOP.Size = New System.Drawing.Size(1047, 56)
        Me.pnl_tlspTOP.TabIndex = 1
        '
        'tls
        '
        Me.tls.BackColor = System.Drawing.Color.Transparent
        Me.tls.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Toolstrip
        Me.tls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnOk, Me.ts_btnCancel})
        Me.tls.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tls.Location = New System.Drawing.Point(1, 1)
        Me.tls.Name = "tls"
        Me.tls.Size = New System.Drawing.Size(1046, 53)
        Me.tls.TabIndex = 0
        Me.tls.TabStop = True
        Me.tls.Text = "toolStrip1"
        '
        'ts_btnOk
        '
        Me.ts_btnOk.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnOk.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnOk.Image = CType(resources.GetObject("ts_btnOk.Image"), System.Drawing.Image)
        Me.ts_btnOk.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnOk.Name = "ts_btnOk"
        Me.ts_btnOk.Size = New System.Drawing.Size(66, 50)
        Me.ts_btnOk.Tag = "OK"
        Me.ts_btnOk.Text = "&Save&&Cls"
        Me.ts_btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnOk.ToolTipText = "Save and Close"
        '
        'ts_btnCancel
        '
        Me.ts_btnCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnCancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnCancel.Image = CType(resources.GetObject("ts_btnCancel.Image"), System.Drawing.Image)
        Me.ts_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnCancel.Name = "ts_btnCancel"
        Me.ts_btnCancel.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnCancel.Tag = "Cancel"
        Me.ts_btnCancel.Text = "&Close"
        Me.ts_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label11.Location = New System.Drawing.Point(0, 1)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 55)
        Me.Label11.TabIndex = 4
        Me.Label11.Text = "label4"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Location = New System.Drawing.Point(0, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1047, 1)
        Me.Label12.TabIndex = 5
        Me.Label12.Text = "label1"
        '
        'pnl_Caption1
        '
        Me.pnl_Caption1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_Caption1.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Button
        Me.pnl_Caption1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnl_Caption1.Controls.Add(Me.lblCaption)
        Me.pnl_Caption1.Controls.Add(Me.Label10)
        Me.pnl_Caption1.Controls.Add(Me.Label13)
        Me.pnl_Caption1.Controls.Add(Me.Label14)
        Me.pnl_Caption1.Controls.Add(Me.Label15)
        Me.pnl_Caption1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_Caption1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_Caption1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_Caption1.Location = New System.Drawing.Point(0, 3)
        Me.pnl_Caption1.Name = "pnl_Caption1"
        Me.pnl_Caption1.Size = New System.Drawing.Size(1047, 23)
        Me.pnl_Caption1.TabIndex = 5
        '
        'lblCaption
        '
        Me.lblCaption.AutoSize = True
        Me.lblCaption.BackColor = System.Drawing.Color.Transparent
        Me.lblCaption.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblCaption.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCaption.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblCaption.Location = New System.Drawing.Point(1, 1)
        Me.lblCaption.Name = "lblCaption"
        Me.lblCaption.Padding = New System.Windows.Forms.Padding(3, 3, 2, 2)
        Me.lblCaption.Size = New System.Drawing.Size(5, 19)
        Me.lblCaption.TabIndex = 47
        Me.lblCaption.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label10.Location = New System.Drawing.Point(1, 22)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1045, 1)
        Me.Label10.TabIndex = 4
        Me.Label10.Text = "label2"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label13.Location = New System.Drawing.Point(0, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 22)
        Me.Label13.TabIndex = 3
        Me.Label13.Text = "label4"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label14.Location = New System.Drawing.Point(1046, 1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 22)
        Me.Label14.TabIndex = 2
        Me.Label14.Text = "label3"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label15.Location = New System.Drawing.Point(0, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1047, 1)
        Me.Label15.TabIndex = 0
        Me.Label15.Text = "label1"
        '
        'lblMedicationNDCCode
        '
        Me.lblMedicationNDCCode.AutoSize = True
        Me.lblMedicationNDCCode.BackColor = System.Drawing.Color.Transparent
        Me.lblMedicationNDCCode.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblMedicationNDCCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMedicationNDCCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.lblMedicationNDCCode.Location = New System.Drawing.Point(1, 1)
        Me.lblMedicationNDCCode.Name = "lblMedicationNDCCode"
        Me.lblMedicationNDCCode.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.lblMedicationNDCCode.Size = New System.Drawing.Size(148, 20)
        Me.lblMedicationNDCCode.TabIndex = 50
        Me.lblMedicationNDCCode.Text = "lblMedicationNDCCode"
        Me.lblMedicationNDCCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnl_Caption
        '
        Me.pnl_Caption.Controls.Add(Me.pnl_Caption1)
        Me.pnl_Caption.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_Caption.ForeColor = System.Drawing.Color.Black
        Me.pnl_Caption.Location = New System.Drawing.Point(0, 82)
        Me.pnl_Caption.Name = "pnl_Caption"
        Me.pnl_Caption.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.pnl_Caption.Size = New System.Drawing.Size(1047, 29)
        Me.pnl_Caption.TabIndex = 10
        '
        'panel6
        '
        Me.panel6.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.panel6.Controls.Add(Me.lblMedicationNDCCode)
        Me.panel6.Controls.Add(Me.Label23)
        Me.panel6.Controls.Add(Me.Label24)
        Me.panel6.Controls.Add(Me.Label25)
        Me.panel6.Controls.Add(Me.Label26)
        Me.panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.panel6.Location = New System.Drawing.Point(0, 56)
        Me.panel6.Name = "panel6"
        Me.panel6.Size = New System.Drawing.Size(1047, 26)
        Me.panel6.TabIndex = 311
        Me.panel6.Visible = False
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label23.Enabled = False
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Location = New System.Drawing.Point(1, 25)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(1045, 1)
        Me.Label23.TabIndex = 27
        Me.Label23.Text = "From"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label24.Enabled = False
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Location = New System.Drawing.Point(1, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1045, 1)
        Me.Label24.TabIndex = 28
        Me.Label24.Text = "From"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label25.Enabled = False
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Location = New System.Drawing.Point(0, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(1, 26)
        Me.Label25.TabIndex = 30
        Me.Label25.Text = "From"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label26.Enabled = False
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Location = New System.Drawing.Point(1046, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(1, 26)
        Me.Label26.TabIndex = 29
        Me.Label26.Text = "From"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CustomMedication
        '
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Controls.Add(Me.pnl_Base)
        Me.Controls.Add(Me.pnl_Caption)
        Me.Controls.Add(Me.panel6)
        Me.Controls.Add(Me.pnl_tlspTOP)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "CustomMedication"
        Me.Size = New System.Drawing.Size(1047, 366)
        Me.pnl_Base.ResumeLayout(False)
        Me.pnl_Base.PerformLayout()
        Me.pnlCustomTask.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GRPbox_PriscriberNotes.ResumeLayout(False)
        Me.GRPbox_PriscriberNotes.PerformLayout()
        Me.GRPbox_PharmacyNotes.ResumeLayout(False)
        Me.GRPbox_PharmacyNotes.PerformLayout()
        Me.pnl_tlspTOP.ResumeLayout(False)
        Me.pnl_tlspTOP.PerformLayout()
        Me.tls.ResumeLayout(False)
        Me.tls.PerformLayout()
        Me.pnl_Caption1.ResumeLayout(False)
        Me.pnl_Caption1.PerformLayout()
        Me.pnl_Caption.ResumeLayout(False)
        Me.panel6.ResumeLayout(False)
        Me.panel6.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    'Added on 20080901 RxHub
    Public Event PharmacyClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Private m_PharmacyID As Int64
    Private m_ProviderID As Int64
    Private m_PhNCPDPID As String
    Private WithEvents dgCustomGrid As CustomTask
    Public Event OKClick(ByVal sender As Object, ByVal e As System.EventArgs, ByVal SelectedMedColItem As Integer)
    Public Event CloseClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event CheckedClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event SigClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Private m_mpid As Int32
    Private _MedBusinessLayer As MedicationBusinessLayer
    Private _RowIndex As Int32
    Private _Medication As Medication
    Dim Prescriptiondate As DateTime
    Dim _ServerDatetime As DateTime
    Private m_DosageForm As String
    Private ofrmRefusalList As frmViewListControl
    Private oRefusalListControl As gloListControl.gloListControl
    Private strRefusalCode As String = ""
    Private strRefusalDescription As String = ""


    '--------------------
    Private ofrmCQMlList As frmViewListControl
    Private oCQMListControl As gloListControl.gloListControl
    Private strCQMCode As String = ""
    Private strCQMDescription As String = ""
    '--------------------
    ''Problem No: 00000337 
    ''Reason: changes done to disable the pop-up.
    ''Description: remove default set value as true for blnCheckDb.
    Dim blnCheckDb As Boolean

    Dim blnGetFreqvalonTab As Boolean = False
    Private sNDCCode As String = ""

    Public ReadOnly Property MedicationObject() As Medication
        Get
            Return _Medication
        End Get
    End Property
    Enum enmcontrolname
        Dosage
        Route
        Frequency
        Duration
        Amount
        StartDate
        EndDate
        OK
        Close
        Sig
        Status
        Reason
    End Enum
    Public Property Dosage() As String
        Get
            Return txtDosage.Text
        End Get
        Set(ByVal Value As String)
            txtDosage.Text = Value
        End Set
    End Property

    Public Property DosageForm() As String
        Get
            Return m_DosageForm
        End Get
        Set(ByVal Value As String)
            m_DosageForm = Value
        End Set
    End Property

    Public Property Caption() As String
        Get
            Return lblCaption.Text
        End Get
        Set(ByVal Value As String)
            lblCaption.Text = Value
        End Set
    End Property

    Public Property mpid() As Int32
        Get
            Return m_mpid

        End Get
        Set(ByVal Value As Int32)
            m_mpid = Value
        End Set
    End Property
    Public Property Amount() As String
        Get
            Return txtAmount.Text
        End Get
        Set(ByVal Value As String)
            txtAmount.Text = Value
        End Set
    End Property
    '' GLO2011-0014767 : Quantity not being written out on prescriptions
    Public Property DoseUnit() As String
        Get
            Return txtDoseUnit.Text
        End Get
        Set(ByVal Value As String)
            txtDoseUnit.Text = Value
        End Set
    End Property
    '' end
    Public Property route() As String
        Get
            Return txtRoute.Text
        End Get
        Set(ByVal Value As String)
            txtRoute.Text = Value
        End Set
    End Property

    'Dim _routes As List(Of String)
    'Public Property routes() As List(Of String)
    '    Get
    '        Return _routes
    '    End Get
    '    Set(ByVal Value As List(Of String))
    '        _routes = Value
    '    End Set
    'End Property

    Public Property Frequency() As String
        Get
            Return txtFrequency.Text
        End Get
        Set(ByVal Value As String)
            txtFrequency.Text = Value
        End Set
    End Property

    Public Property Duration() As String
        Get
            Return txtDuration.Text
        End Get
        Set(ByVal Value As String)
            txtDuration.Text = Value
        End Set
    End Property

    Public Property NDCCode() As String
        Get
            Return sNDCCode
        End Get
        Set(ByVal Value As String)
            sNDCCode = Value
        End Set
    End Property

    'CCHIT 08 to get the values from SIG Control
    Public Property cmbDurationDyWkMnth() As String
        Get

            Return cmbDuration.Text
        End Get
        Set(ByVal Value As String)
            cmbDuration.Text = Value
        End Set
    End Property
    'CCHIT 08

    Public Property StartDate() As DateTime
        Get
            Return dtpStartDate.Value

        End Get
        Set(ByVal Value As DateTime)
            dtpStartDate.Value = Value
        End Set
    End Property
    Public Property CheckEndDate() As Boolean
        Get
            Return dtpEndDate.Checked

        End Get
        Set(ByVal Value As Boolean)
            dtpEndDate.Checked = Value
        End Set
    End Property
    Public Property EndDate() As DateTime
        Get
            Return dtpEndDate.Value
        End Get
        Set(ByVal Value As DateTime)
            dtpEndDate.Value = Value
        End Set
    End Property
    Public WriteOnly Property DisableEndDate() As Boolean
        Set(ByVal Value As Boolean)
            dtpEndDate.Enabled = Value
        End Set
    End Property
    ''''Code added by Ravikiran on 23/01/2007 for CCHIT Requirements
    Public Property Status() As String
        Get
            Return cmbStatus.Text
        End Get
        Set(ByVal Value As String)
            cmbStatus.Text = Value
        End Set
    End Property
    Public Property Reason() As String
        Get
            Return txtReason.Text
        End Get
        Set(ByVal Value As String)
            txtReason.Text = Value
        End Set
    End Property

    'added on 20090801 Rx fields
    Public Property PharmacyName() As String
        Get
            'Return txtPharmacy.Text
            Return cmbPharmacy.Text
        End Get
        Set(ByVal Value As String)
            'txtPharmacy.Text = Value
            cmbPharmacy.Text = Value
        End Set
    End Property
    Public Property PharmacyNotes() As String
        Get
            Return txtNotes.Text
        End Get
        Set(ByVal Value As String)
            txtNotes.Text = Value
        End Set
    End Property
    Public Property PrescriberNotes() As String
        Get
            Return txtPrescriberNotes.Text
        End Get
        Set(ByVal Value As String)
            txtPrescriberNotes.Text = Value
        End Set
    End Property
    Public Property Method() As String
        Get
            Return CmbMethod.Text
        End Get
        Set(ByVal Value As String)
            CmbMethod.Text = Value
        End Set
    End Property
    Public Property Refills() As String
        Get
            Return txtRefills.Text
        End Get
        Set(ByVal Value As String)
            txtRefills.Text = Value
        End Set
    End Property

    Public Property PharmacyID() As Int64
        Get
            Return m_PharmacyID
        End Get
        Set(ByVal Value As Int64)
            m_PharmacyID = Value
        End Set
    End Property

    Public Property ProviderID() As Int64
        Get
            Return m_ProviderID
        End Get
        Set(ByVal Value As Int64)
            m_ProviderID = Value
        End Set
    End Property

    Public Property NCPDPID() As String
        Get
            Return m_PhNCPDPID
        End Get
        Set(ByVal Value As String)
            m_PhNCPDPID = Value
        End Set
    End Property
    Public Property ShowFrequencyAbbrevationInRxMeds() As Boolean
        Get
            Return _gblnIncludeFrequencyAbbrevationInRxMeds

        End Get
        Set(ByVal Value As Boolean)
            _gblnIncludeFrequencyAbbrevationInRxMeds = Value
        End Set
    End Property

    Private _isCQMCypressTesting As Boolean = False
    Public Property IsCQMCypressTesting() As Boolean
        Get
            Return _isCQMCypressTesting
        End Get
        Set(ByVal Value As Boolean)
            _isCQMCypressTesting = Value
        End Set
    End Property

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnCancel.Click
        Try
            RaiseEvent CloseClick(sender, e)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnOk.Click
        'Dim _Medication As Medication
        Dim nSelectedMedColItem As Integer ''''this will help to set data against correct med item on flex grid, and not make tht item as duplicate
        Try
            If cmbRoute.Visible And cmbRoute.SelectedItem = "" Then
                MessageBox.Show("Route cannot be blank.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                cmbRoute.Focus()
                Exit Sub
            End If

            If Me.StartDate.Date > Me.EndDate.Date And dtpEndDate.Checked Then ''bug 75491
                ''''''''''fixed bug 14149
                MessageBox.Show("Start date cannot be greater than end date", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            'Added Code for Incident #00042830 to handle condition of quantity not equal to zero and 0.0
            Dim qresult As Double = 0.0
            If txtAmount.Text.Trim <> "" Then
                If Val(txtAmount.Text.Trim) = qresult Then
                    MessageBox.Show("Quantity cannot be zero.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtAmount.Focus()
                    Exit Sub
                End If
            End If


            If _MedBusinessLayer.MedicationCol.Count > 0 Then
                For i As Integer = 0 To _MedBusinessLayer.MedicationCol.Count - 1
                    ''If _MedBusinessLayer.MedicationCol.Item(i).NDCCode = Me.lblMedNDCCode.Text Then ''''And _MedBusinessLayer.MedicationCol.Item(i).Medication = Me.Caption And _MedBusinessLayer.MedicationCol.Item(i).Dosage = Me.Dosage And _MedBusinessLayer.MedicationCol.Item(i).Route = Me.route And _MedBusinessLayer.MedicationCol.Item(i).Frequency = Me.Frequency And _MedBusinessLayer.MedicationCol.Item(i).Duration = Me.Duration And _MedBusinessLayer.MedicationCol.Item(i).Startdate = Me.StartDate And _MedBusinessLayer.MedicationCol.Item(i).Enddate = Me.EndDate
                    If _MedBusinessLayer.MedicationCol.Item(i).ItemNumber = _RowIndex Then
                        nSelectedMedColItem = i
                        '_Medication.VisitID = _MedBusinessLayer.CurrentVisitID   

                        If cmbRoute.Visible And cmbRoute.SelectedItem <> "" Then
                            Me.route = cmbRoute.SelectedItem
                        End If

                        _MedBusinessLayer.MedicationCol.Item(i).Frequency = Me.Frequency
                        'CCHIT 08
                        If Me.Duration <> "" Then
                            _MedBusinessLayer.MedicationCol.Item(i).Duration = Me.Duration & " " & cmbDuration.SelectedItem.ToString()
                        Else
                            _MedBusinessLayer.MedicationCol.Item(i).Duration = Me.Duration
                        End If
                        'CCHIT 08

                        If String.IsNullOrEmpty(_MedBusinessLayer.MedicationCol.Item(i).Dosage) AndAlso txtDosage.Enabled Then
                            _MedBusinessLayer.MedicationCol.Item(i).Medication = Me.Caption & " " & Me.Dosage.Trim
                        Else
                            _MedBusinessLayer.MedicationCol.Item(i).Medication = Me.Caption
                        End If
                        _MedBusinessLayer.MedicationCol.Item(i).Dosage = Me.Dosage

                        '_MedBusinessLayer.MedicationCol.Item(i).NDCCode = Me.lblMedicationNDCCode.Text
                        _MedBusinessLayer.MedicationCol.Item(i).NDCCode = Me.NDCCode

                        _MedBusinessLayer.MedicationCol.Item(i).ReasonConceptID = Me.strRefusalCode
                        _MedBusinessLayer.MedicationCol.Item(i).ReasonConceptDesc = Me.strRefusalDescription


                        _MedBusinessLayer.MedicationCol.Item(i).CQMCategories = Me.strCQMCode
                        _MedBusinessLayer.MedicationCol.Item(i).CQMDesc = Me.strCQMDescription

                        _MedBusinessLayer.MedicationCol.Item(i).Route = Me.route
                        _MedBusinessLayer.MedicationCol.Item(i).Startdate = Me.StartDate
                        _MedBusinessLayer.MedicationCol.Item(i).Reason = Me.Reason
                        _MedBusinessLayer.MedicationCol.Item(i).Status = Me.Status
                        _MedBusinessLayer.MedicationCol.Item(i).Amount = Me.Amount & " " & txtDoseUnit.Text
                        _MedBusinessLayer.MedicationCol.Item(i).mpid = Me.mpid
                        _MedBusinessLayer.MedicationCol.Item(i).MedicationID = Me.Tag
                        '_MedBusinessLayer.MedicationCol.Item(_RowIndex - 1).Enddate = Me.EndDate''''GLO2010-0006442
                        '_Medication.Enddate = Me.EndDate

                        ''''Assign the cmbDrugsForm combo box value to the prescription object
                        If cmbDrugsForm.Text <> "" Then '''''''bug fix 5895 case no GLO2009-0004131
                            Me.DosageForm = cmbDrugsForm.Text
                            _MedBusinessLayer.MedicationCol.Item(i).DosageForm = Me.DosageForm
                        End If

                        If Me.CheckEndDate Then
                            _MedBusinessLayer.MedicationCol.Item(i).CheckEndDate = True
                            _MedBusinessLayer.MedicationCol.Item(i).Enddate = Me.EndDate
                        Else
                            _MedBusinessLayer.MedicationCol.Item(i).CheckEndDate = False
                            _MedBusinessLayer.MedicationCol.Item(i).Enddate = Nothing '''''GLO2010-0006442
                        End If

                        _MedBusinessLayer.MedicationCol.Item(i).UserName = _MedBusinessLayer.GetCurrentUserName

                        _MedBusinessLayer.MedicationCol.Item(i).UpdatedByUserName = _MedBusinessLayer.GetCurrentUserName '' New Updated by column

                        'Added on 20090901 Rx Fields--------
                        _MedBusinessLayer.MedicationCol.Item(i).Rx_Method = Me.Method
                        _MedBusinessLayer.MedicationCol.Item(i).Rx_Refills = Me.Refills
                        _MedBusinessLayer.MedicationCol.Item(i).Rx_Notes = Me.PharmacyNotes
                        _MedBusinessLayer.MedicationCol.Item(i).Rx_PrescriberNotes = Me.PrescriberNotes
                        'if user has not assigned any pharmacy in the Medication control then assign the pharmacy name that is assigned at the time of registration
                        'COMMENTED BY SHUBHANGI 20110104
                        'If Me.txtPharmacy.Text.Trim = "" Then
                        '    _MedBusinessLayer.MedicationCol.Item(i).Rx_PhName = _MedBusinessLayer.MedicationCol.Item(i).Rx_PhName.ToString
                        'Else
                        '    _MedBusinessLayer.MedicationCol.Item(i).Rx_PhName = Me.txtPharmacy.Text
                        'End If
                        '-----------------------------------

                        _MedBusinessLayer.MedicationCol.Item(i).Rx_PharmacyId = Me.m_PharmacyID
                        _MedBusinessLayer.MedicationCol.Item(i).Rx_ProviderId = Me.ProviderID

                        _MedBusinessLayer.MedicationCol.Item(i).Rx_ContactID = Me.m_PharmacyID

                        ''''''get the appropriate NCPDPID against teh contact id and save in prescription collection
                        If _MedBusinessLayer.MedicationCol.Item(i).Rx_PharmacyId <> 0 Then
                            Dim dtPharmacyDetails As DataTable = GetPharmacyNCPDPID(_MedBusinessLayer.MedicationCol.Item(i).Rx_PharmacyId)
                            If Not IsNothing(dtPharmacyDetails) Then
                                If dtPharmacyDetails.Rows.Count > 0 Then
                                    _MedBusinessLayer.MedicationCol.Item(i).Rx_NCPDPID = dtPharmacyDetails.Rows(0)("NCPDPID")
                                    _MedBusinessLayer.MedicationCol.Item(i).Rx_ServiceLevel = dtPharmacyDetails.Rows(0)("sServiceLevel")
                                End If
                                dtPharmacyDetails.Dispose()
                                dtPharmacyDetails = Nothing
                            End If
                            '_MedBusinessLayer.MedicationCol.Item(i).Rx_NCPDPID = GetPharmacyNCPDPID(_MedBusinessLayer.MedicationCol.Item(i).Rx_PharmacyId)
                        Else
                            _MedBusinessLayer.MedicationCol.Item(i).Rx_NCPDPID = ""
                        End If
                        If Me.cmbPharmacy.Text.Trim = "" Then
                            _MedBusinessLayer.MedicationCol.Item(i).Rx_PhName = _MedBusinessLayer.MedicationCol.Item(i).Rx_PhName.ToString
                        Else
                            _MedBusinessLayer.MedicationCol.Item(i).Rx_PhName = Me.cmbPharmacy.Text
                        End If
                        'added new property to save rowstate used in TVP(6040)
                        If _MedBusinessLayer.MedicationCol.Item(i).State <> "A" Then
                            _MedBusinessLayer.MedicationCol.Item(i).State = "M"
                        End If

                        _MedBusinessLayer.MedicationCol.Item(i).Rx_CPOEOrder = chkCPOEOrder.Checked

                        _MedBusinessLayer.MedicationCol.Item(i).Rx_MedicationAdministered = ChkMedicationAdministered.Checked

                        gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnIsRxMedsFormEdited = True ''''''''means Rx-Meds form is edited and we should prompt the user if he directly Clicks the Close button

                        Exit For '''''for optimization once the values from the control are assigned then exit from the loop
                    End If
                Next

            End If

            '_MedBusinessLayer.SetMedicationCol(_Medication, _RowIndex - 1)
            RaiseEvent OKClick(sender, e, nSelectedMedColItem)
            gloEMRGeneralLibrary.gloGeneral.clsgeneral.gblnCustomMediEdited = True ''''for CCHIT11 audit log
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub

    Private Function GetRxnormInfo(ByVal id As String, ByVal idQualifier As String) As DataRow
        Dim _gloEMRDatabase As New DataBaseLayer
        Dim oParamater As gloEMRGeneralLibrary.gloEMRDatabase.DBParameter

        Dim _dtRxnormInfo As DataTable = Nothing
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@id"
            oParamater.Value = id
            _gloEMRDatabase.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@idQualifier"
            oParamater.Value = idQualifier
            _gloEMRDatabase.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            _dtRxnormInfo = _gloEMRDatabase.GetDataTable("gsp_GetRxnormInfo")

            If _dtRxnormInfo IsNot Nothing AndAlso _dtRxnormInfo.Rows.Count > 0 Then
                Return _dtRxnormInfo.Rows(0)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        Finally
            If Not IsNothing(_dtRxnormInfo) Then
                _dtRxnormInfo.Dispose()
                _dtRxnormInfo = Nothing
            End If
            _gloEMRDatabase.Dispose()
            _gloEMRDatabase = Nothing
        End Try

    End Function

    Private Function GetPharmacyNCPDPID(ByVal PhContactId As Long) As DataTable
        Dim _gloEMRDatabase As New DataBaseLayer
        Dim _strSQL As String = ""

        Dim _dtPharmacyDetails As DataTable = Nothing
        Try
            _strSQL = "SELECT isnull(sNCPDPID,'') as NCPDPID,ISNULL(sServiceLevel,'') as sServiceLevel FROM Contacts_Mst where nContactId = " & PhContactId & " AND sContactType = 'Pharmacy' "
            _dtPharmacyDetails = _gloEMRDatabase.GetDataTable_Query(_strSQL)
            Return _dtPharmacyDetails
        Catch ex As Exception
            Return _dtPharmacyDetails
        Finally
            _gloEMRDatabase.Dispose()
            _gloEMRDatabase = Nothing
        End Try
    End Function

    Private Sub btnSig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSig.Click
        Try

            RaiseEvent SigClick(sender, e)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Public Sub SetControlFocus(ByVal enm As enmcontrolname)
        Dim objSender As New Object
        Dim obje As New EventArgs
        Select Case enm
            Case enmcontrolname.Dosage
                txtDosage.Focus()
            Case enmcontrolname.Route
                txtRoute.Focus()
            Case enmcontrolname.Frequency
                txtFrequency.Focus()
            Case enmcontrolname.Duration
                txtDuration.Focus()
            Case enmcontrolname.Amount
                txtAmount.Focus()
            Case enmcontrolname.StartDate
                dtpStartDate.Focus()
            Case enmcontrolname.EndDate
                dtpEndDate.Focus()
            Case enmcontrolname.OK
                btnOK_Click(objSender, obje)
            Case enmcontrolname.Close
                btnClose_Click(objSender, obje)
            Case enmcontrolname.Sig
                btnSig_Click(objSender, obje)
            Case enmcontrolname.Status
                cmbStatus.Focus()
            Case enmcontrolname.Reason
                txtReason.Focus()
        End Select
        obje = Nothing
        objSender = Nothing
    End Sub

    Private Sub CustomMedication_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            '  cmbStatus.Text = cmbStatus.Items(0)
            If _MedBusinessLayer.MedicationCol.Count > _RowIndex - 1 Then
                If _RowIndex = 0 Then ''since it gives error when the rowindex value is 0, check if the value is 0 and add 1 only when the rowIndex value is 0
                    Dim TempRowIndex As Int16 = 0
                    TempRowIndex = _RowIndex + 1

                    If Not IsDBNull(_MedBusinessLayer.MedicationCol.Item(TempRowIndex - 1)._PrescriptionId) AndAlso Not IsNothing(_MedBusinessLayer.MedicationCol.Item(TempRowIndex - 1)._PrescriptionId) AndAlso (_MedBusinessLayer.MedicationCol.Item(TempRowIndex - 1)._PrescriptionId) <> 0 Then

                        Dim dt As System.Data.DataTable = _MedBusinessLayer.getServerTime(_MedBusinessLayer.MedicationCol.Item(TempRowIndex - 1)._PrescriptionId)
                        If (IsNothing(dt) = False) Then


                            If dt.Rows.Count > 0 Then

                                If Not IsDBNull(dt.Rows(0)(1)) Then
                                    Prescriptiondate = CType(dt.Rows(0)(1), DateTime)
                                End If
                                If Not IsDBNull(dt.Rows(0)(0)) Then
                                    _ServerDatetime = CType(dt.Rows(0)(0), DateTime)
                                End If
                                If Not IsNothing(Prescriptiondate) AndAlso Not IsNothing(_ServerDatetime) Then
                                    If DateDiff(DateInterval.Minute, Prescriptiondate, _ServerDatetime) <= Threshold Then
                                        txtAmount.ReadOnly = True
                                        txtAmount.BackColor = Color.White
                                        txtDosage.ReadOnly = True
                                        txtDosage.BackColor = Color.White
                                        txtDuration.ReadOnly = True
                                        txtDuration.BackColor = Color.White
                                        txtFrequency.ReadOnly = True
                                        txtFrequency.BackColor = Color.White
                                        txtReason.ReadOnly = True
                                        txtReason.BackColor = Color.White
                                        txtRoute.ReadOnly = True
                                        txtRoute.BackColor = Color.White
                                        dtpStartDate.Enabled = False
                                        dtpEndDate.Enabled = True
                                        'btnSig.Enabled = False
                                    End If
                                End If
                            End If
                            dt.Dispose()
                            dt = Nothing
                        End If

                    End If
                Else
                    If Not IsDBNull(_MedBusinessLayer.MedicationCol.Item(_RowIndex)._PrescriptionId) AndAlso Not IsNothing(_MedBusinessLayer.MedicationCol.Item(_RowIndex)._PrescriptionId) AndAlso (_MedBusinessLayer.MedicationCol.Item(_RowIndex)._PrescriptionId) <> 0 Then

                        Dim dt As System.Data.DataTable = Nothing
                        If _MedBusinessLayer.MedicationCol.Item(_RowIndex)._PrescriptionId <> 0 Then
                            dt = _MedBusinessLayer.getServerTime(_MedBusinessLayer.MedicationCol.Item(_RowIndex)._PrescriptionId)
                            If (IsNothing(dt) = False) Then


                                If dt.Rows.Count > 0 Then

                                    If Not IsDBNull(dt.Rows(0)(1)) Then
                                        Prescriptiondate = CType(dt.Rows(0)(1), DateTime)
                                    End If
                                    If Not IsDBNull(dt.Rows(0)(0)) Then
                                        _ServerDatetime = CType(dt.Rows(0)(0), DateTime)
                                    End If
                                    If Not IsNothing(Prescriptiondate) AndAlso Not IsNothing(_ServerDatetime) Then
                                        If DateDiff(DateInterval.Minute, Prescriptiondate, _ServerDatetime) <= Threshold Then
                                            txtAmount.ReadOnly = True
                                            txtAmount.BackColor = Color.White
                                            txtDosage.ReadOnly = True
                                            txtDosage.BackColor = Color.White
                                            txtDuration.ReadOnly = True
                                            txtDuration.BackColor = Color.White
                                            txtFrequency.ReadOnly = True
                                            txtFrequency.BackColor = Color.White
                                            txtReason.ReadOnly = True
                                            txtReason.BackColor = Color.White
                                            txtRoute.ReadOnly = True
                                            txtRoute.BackColor = Color.White
                                            dtpStartDate.Enabled = False
                                            dtpEndDate.Enabled = True
                                            'btnSig.Enabled = False
                                        End If
                                    End If
                                End If
                                dt.Dispose()
                                dt = Nothing
                            End If
                        End If
                    End If
                End If
            End If

            ''Load the Drugs Form Combo box
            'Rxhub
            ''in the setdata() i.e called in constructor we if we set the text of drugform then we disable the cmbDrugsForm combo box, 
            ''therefore if the combo box is disabled do not fetch the drugs form values for cmbdrugsform.
            If cmbDrugsForm.Enabled = True Then
                Dim _listDrugsForm As List(Of String)

                Using oDIBGSHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                    _listDrugsForm = oDIBGSHelper.GetDrugFormList()
                End Using
                'clear the combo box
                cmbDrugsForm.Items.Clear()

                If Not IsNothing(_listDrugsForm) And _listDrugsForm.Count > 0 Then

                    For _dtRowCount As Integer = 0 To _listDrugsForm.Count - 1
                        'add the items to the combo box
                        cmbDrugsForm.Items.Add(_listDrugsForm.Item(_dtRowCount))
                        'cmbDrugsForm.
                    Next
                End If
            End If

            If IsCQMCypressTesting Then
                dtpStartDate.CustomFormat = "MM/dd/yyyy hh:mm:ss tt"
                dtpEndDate.CustomFormat = "MM/dd/yyyy hh:mm:ss tt"
            Else
                dtpStartDate.CustomFormat = "MM/dd/yyyy"
                dtpEndDate.CustomFormat = "MM/dd/yyyy"
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try


    End Sub
    'C
    'Added on 20080901

    'Added on 20090901
    Private Sub btnSelectPharmacy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectPharmacy.Click
        Try
            RaiseEvent PharmacyClick(sender, e)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtRefills_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRefills.KeyPress

        Try

            Dim chkNumeric As String = txtRefills.Text.Trim()
            If e.KeyChar = vbBack Or e.KeyChar = vbCr Then
                e.Handled = False
            Else

                If Char.IsDigit(e.KeyChar) Then

                Else
                    If e.KeyChar = "." And chkNumeric.Contains(".") = False Then

                    Else
                        MessageBox.Show("Enter valid numeric or decimal value", "gloEMR", MessageBoxButtons.OK)
                        e.Handled = True

                        Exit Sub
                    End If
                End If


            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Dim objex As New gloUserControlLibrary.gloUserControlExceptions
            objex.ErrorMessage = "Error setting Medication details"
            Throw objex
        End Try

    End Sub

    'Added BY Shweta 20091127
    'Against salesForce Case NO: GLO2009 0002946
    Private Sub CalculateEndDate()
        'Added By Shweta 20091127
        'To calculate the end date  
        Dim sDuration As String
        sDuration = txtDuration.Text
        Dim aDuration As Array
        aDuration = sDuration.Split(" ")
        'If the duration mention is alphanumeric then pass the only numeric part to calculate end date
        Try
            If txtDuration.Text.Trim.Length > 0 Then
                'Commented By Shweta 20091127
                'If IsNumeric(txtDuration.Text.Trim) Then
                'End Commenting
                If IsNumeric(aDuration(0)) Then
                    Select Case cmbDuration.Text
                        Case "Months"
                            'Changed By Shweta 20091127
                            dtpEndDate.Value = DateAdd(DateInterval.Month, CType(aDuration(0), Double), dtpStartDate.Value.Date).AddDays(If(Convert.ToInt64(txtDuration.Text) = 0, 0, -1))
                            'End Shweta 
                            'dtpEndDate.Value = DateAdd(DateInterval.Month, CType(txtDuration.Text.Trim, Double), dtpStartDate.Value.Date)
                        Case "Days"
                            'Changed By Shweta 20091127
                            dtpEndDate.Value = DateAdd(DateInterval.Day, CType(aDuration(0), Double), dtpStartDate.Value.Date).AddDays(If(Convert.ToInt64(txtDuration.Text) = 0, 0, -1))
                            'End Shweta 
                            'dtpEndDate.Value = DateAdd(DateInterval.Day, CType(txtDuration.Text.Trim, Double), dtpStartDate.Value.Date)
                        Case "Weeks"
                            'Changed By Shweta 20091127
                            dtpEndDate.Value = DateAdd(DateInterval.WeekOfYear, CType(aDuration(0), Double), dtpStartDate.Value.Date).AddDays(If(Convert.ToInt64(txtDuration.Text) = 0, 0, -1))
                            'End Shweta 
                            'dtpEndDate.Value = DateAdd(DateInterval.WeekOfYear, CType(txtDuration.Text.Trim, Double), dtpStartDate.Value.Date)
                    End Select
                Else
                    'Added By Shweta 20091127
                    dtpEndDate.ResetText()
                End If
            Else
                'Added By Shweta 20091127
                dtpEndDate.ResetText()
            End If
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(txtDuration.Text & " is invalid duration value to calculate end date. Please enter valid duration.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtDuration.Text = "" ''''''''fixed bug 4742
        End Try
    End Sub
    'End Code Add 20091127

    Private Sub txtDuration_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDuration.TextChanged
        CalculateEndDate()
    End Sub

    '' issue: 5493 
    ''20091205 Added as per pravin sir discussion: Duration should be numeric value
    Private Sub txtDuration_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDuration.KeyPress
        Try

            Dim chkNumeric As String = txtDuration.Text.Trim()
            If e.KeyChar.ToString() = " " Then
                e.Handled = True
                Exit Sub
            End If

            If e.KeyChar = vbBack Or e.KeyChar = vbCr Then
                e.Handled = False
            Else

                If Char.IsDigit(e.KeyChar) Then

                Else
                    If e.KeyChar = "." And chkNumeric.Contains(".") = False Then

                        MessageBox.Show("Enter valid numeric value", "gloEMR", MessageBoxButtons.OK)
                        e.Handled = True
                        Exit Sub
                    Else
                        MessageBox.Show("Enter valid numeric value", "gloEMR", MessageBoxButtons.OK)
                        e.Handled = True
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    ''bugs no:5494 

    Private Sub txtAmount_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAmount.KeyPress
        Try

            Dim chkNumeric As String = txtAmount.Text.Trim()
            If e.KeyChar.ToString() = " " Then
                e.Handled = True
                Exit Sub
            End If

            If e.KeyChar = vbBack Or e.KeyChar = vbCr Then
                e.Handled = False
            Else

                If Char.IsDigit(e.KeyChar) Then

                Else
                    If e.KeyChar = "." And chkNumeric.Contains(".") = False Then

                    Else
                        MessageBox.Show("Enter valid numeric or decimal value", "gloEMR", MessageBoxButtons.OK)
                        e.Handled = True
                        Exit Sub
                    End If
                End If

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Dim objex As New gloUserControlLibrary.gloUserControlExceptions
            objex.ErrorMessage = "Error setting Medication Drug details"
            Throw objex
        End Try
    End Sub

    Private Sub cmbDrugsForm_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDrugsForm.SelectedIndexChanged
        Try
            If cmbDrugsForm.Items.Count > 0 Then
                Me.DosageForm = cmbDrugsForm.Text
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub cmbDuration_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDuration.SelectedIndexChanged
        CalculateEndDate()
    End Sub

    Private Sub cmbPharmacy_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPharmacy.SelectionChangeCommitted
        Try
            Me.m_PharmacyID = cmbPharmacy.SelectedValue
        Catch ex As Exception

        End Try

    End Sub

    Private Sub txtFrequency_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtFrequency.KeyPress
        Dim key As Integer = Asc(e.KeyChar)
        blnGetFreqvalonTab = True
        If key = 13 And pnlCustomTask.Visible = True Then
            Try
                'blnCheckDb = False

                Dim abbrv As String = ""
                If dgCustomGrid.C1Task.Rows.Count > 1 Then
                    abbrv = dgCustomGrid.C1Task.Cols(0)(dgCustomGrid.C1Task.RowSel).ToString() ''''the 0th col contains abbrevation value
                End If

                If abbrv <> "" Then
                    If ShowFrequencyAbbrevationInRxMeds = True Then
                        txtFrequency.Text = abbrv & " - " & dgCustomGrid.C1Task.Cols(1)(dgCustomGrid.C1Task.RowSel).ToString()
                    Else
                        txtFrequency.Text = dgCustomGrid.C1Task.Cols(1)(dgCustomGrid.C1Task.RowSel).ToString()
                    End If
                Else
                    txtFrequency.Text = dgCustomGrid.C1Task.Cols(1)(dgCustomGrid.C1Task.RowSel).ToString()
                End If
                txtFrequency.Tag = dgCustomGrid.C1Task.Cols(2)(dgCustomGrid.C1Task.RowSel).ToString()

                'blnCheckDb = True
                'pnlCustomTask.Visible = False
            Catch ex As Exception

            End Try
        End If
        pnlCustomTask.Visible = False
    End Sub

    Private Sub txtFrequency_LostFocus(sender As Object, e As System.EventArgs) Handles txtFrequency.LostFocus
        ''00000284 : Rx Meds Bug #77746 
        If Not IsNothing(dgCustomGrid) Then
            If Not (IsNothing(dgCustomGrid.C1Task) OrElse IsNothing(txtFrequency)) Then
                If Not (txtFrequency.Focused OrElse dgCustomGrid.C1Task.Focused) Then
                    If Not IsNothing(pnlCustomTask) Then
                        pnlCustomTask.Visible = False
                    End If
                End If
            End If
        End If
        If blnGetFreqvalonTab = True Then
            If pnlCustomTask.Visible = True Then
                pnlCustomTask.Visible = False
            End If
            blnGetFreqvalonTab = False
        End If

    End Sub

    Private Sub txtFrequency_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFrequency.TextChanged
        ''Problem No: 00000337 
        ''Reason: changes done to disable the pop-up.
        ''Description: move pnlCustomTask.Visible = True inside IF and set blnCheckDb=true
        If blnCheckDb = True Then
            pnlCustomTask.Visible = True
            LoadUserGrid()
        End If
        blnCheckDb = True
    End Sub
    Private Sub LoadUserGrid()
        Try
            AddControl()
            If Not IsNothing(dgCustomGrid) Then
                dgCustomGrid.Panel1.Visible = False
                dgCustomGrid.Visible = True
                'dgCustomGrid.Width = pnlWordObj.Width
                pnlCustomTask.Width = 350
                dgCustomGrid.Width = pnlCustomTask.Width
                'reduced the height for case GLO2011-0013418
                pnlCustomTask.Height = 130
                'pnlcustomTask.Width = dgCustomGrid.Width
                dgCustomGrid.Height = pnlCustomTask.Height
                dgCustomGrid.txtsearch.Visible = False
                dgCustomGrid.Label1.Visible = False

                dgCustomGrid.BringToFront()
                dgCustomGrid.SetVisible = False

                BindUserGrid()
                dgCustomGrid.Panel2.Visible = False
                dgCustomGrid.tsbtn_SelectAll.Visible = False
                'dgCustomGrid.tsbtn_OK.Visible = False
                dgCustomGrid.Selectsearch(CustomDataGrid.enmcontrol.Search)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Cardiac Catheterization", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub AddControl()

        If Not IsNothing(dgCustomGrid) Then
            RemoveControl()
        End If
        dgCustomGrid = New CustomTask
        pnlCustomTask.Controls.Add(dgCustomGrid)
        pnlCustomTask.BringToFront()

        ''''''''''''''''''''''
        ''dgCustomGrid.lblCaption.Visible = True
        ''''''''''''''''''''''

        Dim y As Int64
        Dim x As Int64
        y = 220
        x = 200
        'If strLst = "cpt" Then
        '    y = 220
        '    x = 200
        '    'dgCustomGrid.lblCaption.Text = " CPT List "
        'ElseIf strLst = "testtype" Then
        '    y = 220
        '    x = 520
        '    'dgCustomGrid.lblCaption.Text = " Test Type List "
        'ElseIf strLst = "intervention" Then
        '    y = 220
        '    x = 550
        '    'dgCustomGrid.lblCaption.Text = " Intervention Type List "
        'ElseIf strLst = "physician" Then
        '    y = 220
        '    x = 560
        '    'dgCustomGrid.lblCaption.Text = " Physician List "
        'End If

        'pnlCustomTask.Location = New Point(txtFrequency.Margin.Right + txtFrequency.Width, txtFrequency.Margin.Top)
        pnlCustomTask.Visible = True
        dgCustomGrid.Visible = True
        pnlCustomTask.BringToFront()
        dgCustomGrid.BringToFront()

    End Sub
    Private Sub RemoveControl()
        If Not IsNothing(dgCustomGrid) Then
            'pnlWordObj.Controls.Remove(dgCustomGrid)
            pnlCustomTask.Controls.Remove(dgCustomGrid)
            dgCustomGrid.Visible = False
            dgCustomGrid.Dispose()
            dgCustomGrid = Nothing
        End If
    End Sub
    Public Sub CustomDrugsGridStyle()

        Dim _TotalWidth As Single = dgCustomGrid.C1Task.Width - 5

        ' '' Show Drugs Info
        With dgCustomGrid.C1Task
            .Cols.Fixed = 0
            .Rows.Fixed = 1
            .Cols.Count = 3

            Dim Col_Abbre As Integer = 0
            Dim Col_Mean As Integer = 1
            Dim Col_Val As Integer = 2

            .AllowEditing = True

            .SetData(0, Col_Abbre, "Abb")
            '.Cols(Col_Check).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            .Cols(Col_Abbre).Width = _TotalWidth * 0.35
            .Cols(Col_Abbre).AllowEditing = False


            .SetData(0, Col_Mean, "Meaning")
            .Cols(Col_Mean).Width = _TotalWidth * 0.35
            .Cols(Col_Mean).AllowEditing = False


            .SetData(0, Col_Val, "Value")
            .Cols(Col_Val).Width = _TotalWidth * 0
            .Cols(Col_Val).AllowEditing = False



            ' .Cols(Col_DrugName).AllowEditing = False

        End With

    End Sub
    Private Sub BindUserGrid()
        Try
            Dim dt As DataTable
            dt = FillFreqAbbreviation()
            CustomDrugsGridStyle()
            'Dim col As New DataColumn
            'col.ColumnName = "Select"
            'col.DataType = System.Type.GetType("System.Boolean")

            'col.DefaultValue = CBool("False")
            'dt.Columns.Add(col)

            If Not IsNothing(dt) Then
                ''dt.Columns("sICD9Display").Caption = "Diagnosis Name"
                dgCustomGrid.datasource(dt.DefaultView)
            End If
            ''Reset the grid
            Dim _TotalWidth As Single = dgCustomGrid.C1Task.Width - 5
            '  dgCustomGrid.C1Task.Cols.Move(dgCustomGrid.C1Task.Cols.Count - 1, 0)
            dgCustomGrid.C1Task.AllowEditing = False
            dgCustomGrid.C1Task.Cols(0).AllowEditing = False
            dgCustomGrid.C1Task.Cols(0).Width = _TotalWidth * 0.35
            dgCustomGrid.C1Task.Cols(1).AllowEditing = False
            dgCustomGrid.C1Task.Cols(1).Width = _TotalWidth * 0.35
            dgCustomGrid.C1Task.Cols(2).AllowEditing = False
            dgCustomGrid.C1Task.Cols(2).Width = _TotalWidth * 0

            dgCustomGrid.C1Task.ScrollBars = ScrollBars.Both

        Catch ex As SqlClient.SqlException
            ' MessageBox.Show(ex.Message, "Catheterization", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            ' MessageBox.Show(ex.Message, "Catheterization", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Function FillFreqAbbreviation() As DataTable
        Dim oDB As New DataBaseLayer
        Dim _strSQL As String = ""
        Dim dtabbr As DataTable = Nothing
        Try
            Dim strfreq As String = txtFrequency.Text.Trim.Replace("'", "") ''''bug fix 6891
            _strSQL = "Select ISNULL(Abbrevation,'')as Abb,ISNULL(Meaning,'') as Meaning,ISNULL(Value,'1') as Value From Frequency_Abbreviation Where  Abbrevation Like '%" + strfreq + "%'  OR Meaning Like '%" + strfreq + "%' order by Abbrevation asc"
            dtabbr = oDB.GetDataTable_Query(_strSQL)
            If Not dtabbr Is Nothing Then
                Return dtabbr
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        Finally
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function

    Private Sub dgCustomGrid_GotFocus(sender As Object, e As System.EventArgs) Handles dgCustomGrid.GotFocus
        blnGetFreqvalonTab = True
    End Sub

    ''on grid enter add the frequency selected item to the txt frequency text box
    Private Sub dgCustomGrid_Gridkeypress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles dgCustomGrid.Gridkeypress
        Dim key As Integer = Asc(e.KeyChar)
        blnGetFreqvalonTab = True
        If key = 13 And pnlCustomTask.Visible = True Then
            Try
                If dgCustomGrid.C1Task.RowSel > 0 Then
                    txtFrequency.Tag = dgCustomGrid.C1Task.Cols(2)(dgCustomGrid.C1Task.RowSel).ToString()

                    '''''we want the frequency as abbrevation seperation as per Drew comment...
                    Dim abbrv As String = dgCustomGrid.C1Task.Cols(0)(dgCustomGrid.C1Task.RowSel).ToString() ''''the 0th col contains abbrevation value
                    If abbrv <> "" Then
                        If ShowFrequencyAbbrevationInRxMeds = True Then
                            txtFrequency.Text = abbrv & " - " & dgCustomGrid.C1Task.Cols(1)(dgCustomGrid.C1Task.RowSel).ToString()
                        Else
                            txtFrequency.Text = dgCustomGrid.C1Task.Cols(1)(dgCustomGrid.C1Task.RowSel).ToString()
                        End If
                    Else
                        txtFrequency.Text = dgCustomGrid.C1Task.Cols(1)(dgCustomGrid.C1Task.RowSel).ToString()
                    End If

                    pnlCustomTask.Visible = False
                End If

            Catch ex As Exception
            End Try
        End If
        pnlCustomTask.Visible = False
    End Sub

    Private Sub dgCustomGrid_Leave(sender As Object, e As System.EventArgs) Handles dgCustomGrid.Leave
        blnGetFreqvalonTab = True
        pnlCustomTask.Visible = False
    End Sub

    Private Sub dgCustomGrid_LostFocus(sender As Object, e As System.EventArgs) Handles dgCustomGrid.LostFocus
        blnGetFreqvalonTab = True
        pnlCustomTask.Visible = False
    End Sub

    Private Sub dgCustomGrid_MouseDubClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles dgCustomGrid.MouseDubClick
        Try
            blnCheckDb = False
            '''''we want the frequency as abbrevation seperation as per Drew comment...
            Dim abbrv As String = dgCustomGrid.C1Task.Cols(0)(dgCustomGrid.C1Task.RowSel).ToString() ''''the 0th col contains abbrevation value
            If abbrv <> "" Then
                If ShowFrequencyAbbrevationInRxMeds = True Then
                    txtFrequency.Text = abbrv & " - " & dgCustomGrid.C1Task.Cols(1)(dgCustomGrid.C1Task.RowSel).ToString()
                Else
                    txtFrequency.Text = dgCustomGrid.C1Task.Cols(1)(dgCustomGrid.C1Task.RowSel).ToString()
                End If
            Else
                txtFrequency.Text = dgCustomGrid.C1Task.Cols(1)(dgCustomGrid.C1Task.RowSel).ToString()
            End If
            txtFrequency.Tag = dgCustomGrid.C1Task.Cols(2)(dgCustomGrid.C1Task.RowSel).ToString()

            blnCheckDb = True
            pnlCustomTask.Visible = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgCustomGrid_MouseMoveClick(sender As Object, e As MouseEventArgs) Handles dgCustomGrid.MouseMoveClick
        blnGetFreqvalonTab = False
    End Sub

    Private Sub dgCustomGrid_OKClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.OKClick
        If dgCustomGrid.C1Task.Rows.Count > 1 Then
            txtFrequency.Tag = dgCustomGrid.C1Task.Cols(2)(dgCustomGrid.C1Task.RowSel).ToString()
            blnCheckDb = False

            '''''we want the frequency as abbrevation seperation as per Drew comment...
            Dim abbrv As String = dgCustomGrid.C1Task.Cols(0)(dgCustomGrid.C1Task.RowSel).ToString() ''''the 0th col contains abbrevation value
            If abbrv <> "" Then
                If ShowFrequencyAbbrevationInRxMeds = True Then
                    txtFrequency.Text = abbrv & " - " & dgCustomGrid.C1Task.Cols(1)(dgCustomGrid.C1Task.RowSel).ToString()
                Else
                    txtFrequency.Text = dgCustomGrid.C1Task.Cols(1)(dgCustomGrid.C1Task.RowSel).ToString()
                End If
            Else
                txtFrequency.Text = dgCustomGrid.C1Task.Cols(1)(dgCustomGrid.C1Task.RowSel).ToString()
            End If

            blnCheckDb = True
            pnlCustomTask.Visible = False
        End If
    End Sub

    Public Sub RemoveAbbrevationCntrl()
        If Not IsNothing(dgCustomGrid) Then ''fixed bug Bug #50662 in 7030
            If dgCustomGrid.C1Task.Rows.Count <= 1 Then
                pnlCustomTask.Visible = False
            End If
        End If

    End Sub

#Region "Refusal Code Changes"

    Private Sub buttonSetRefusalCode_Click(sender As Object, e As System.EventArgs) Handles buttonSetRefusalCode.Click
        Try
            ofrmRefusalList = New frmViewListControl
            oRefusalListControl = New gloListControl.gloListControl(DataBaseLayer.ConnectionString, gloListControl.gloListControlType.MUCQMRefusedCode, False, Me.Width)
            oRefusalListControl.ControlHeader = "Refusal Reason Code"
            'set the property true for refused code you want 
            oRefusalListControl.bShowNotTakenCodes = True
            oRefusalListControl.bShowAttributeCodes = True
            AddHandler oRefusalListControl.ItemSelectedClick, AddressOf oRefusalListControl_ItemSelectedClick
            AddHandler oRefusalListControl.ItemClosedClick, AddressOf oRefusalListControl_ItemClosedClick
            ofrmRefusalList.Controls.Add(oRefusalListControl)
            oRefusalListControl.Dock = DockStyle.Fill
            oRefusalListControl.BringToFront()

            oRefusalListControl.ShowHeaderPanel(False)
            ofrmRefusalList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmRefusalList.Text = "Refusal Reason Code"
            ofrmRefusalList.ShowDialog(IIf(IsNothing(CType(ofrmRefusalList, Control).Parent), Me, CType(ofrmRefusalList, Control).Parent))
            If IsNothing(ofrmRefusalList) = False Then
                ofrmRefusalList.Dispose()
                ofrmRefusalList = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "glEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub oRefusalListControl_ItemClosedClick(sender As Object, e As EventArgs)
        ofrmRefusalList.Close()
    End Sub

    Private Sub oRefusalListControl_ItemSelectedClick(sender As Object, e As EventArgs)
        Try
            If oRefusalListControl.SelectedItems.Count > 0 Then
                For i As Int16 = 0 To oRefusalListControl.SelectedItems.Count - 1
                    txtRefusalResonCode.Text = oRefusalListControl.SelectedItems(i).Code + " - " + oRefusalListControl.SelectedItems(i).Description
                    strRefusalCode = Convert.ToString(oRefusalListControl.SelectedItems(i).Code)
                    strRefusalDescription = Convert.ToString(oRefusalListControl.SelectedItems(i).Description)
                Next
                ofrmRefusalList.Close()
            Else
                txtRefusalResonCode.Clear()
                ofrmRefusalList.Close()
            End If

        Catch ex As Exception
            MessageBox.Show("Error on User List Control." & ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub oCQMListControl_ItemClosedClick(sender As Object, e As EventArgs)
        ofrmCQMlList.Close()
    End Sub

    Private Sub oCQMListControl_ItemSelectedClick(sender As Object, e As EventArgs)
        Try
            If oCQMListControl.SelectedItems.Count > 0 Then
                For i As Int16 = 0 To oCQMListControl.SelectedItems.Count - 1
                    txtCqmCategories.Text = oCQMListControl.SelectedItems(i).Description
                    strCQMCode = Convert.ToString(oCQMListControl.SelectedItems(i).Code)
                    strCQMDescription = Convert.ToString(oCQMListControl.SelectedItems(i).Description)
                Next
                ofrmCQMlList.Close()
            Else
                txtCqmCategories.Clear()
                ofrmCQMlList.Close()
            End If

        Catch ex As Exception
            MessageBox.Show("Error on User List Control." & ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub buttonClearRefusalCode_Click(sender As Object, e As System.EventArgs) Handles buttonClearRefusalCode.Click
        txtRefusalResonCode.Clear()
        strRefusalCode = String.Empty
        strRefusalDescription = String.Empty
    End Sub

#End Region

   
    Private Sub buttonSetCqmCategories_Click(sender As System.Object, e As System.EventArgs) Handles buttonSetCqmCategories.Click
        Try

            ofrmCQMlList = New frmViewListControl
            oCQMListControl = New gloListControl.gloListControl(DataBaseLayer.ConnectionString, gloListControl.gloListControlType.CQMCategoriesValueSet, False, Me.Width)
            oCQMListControl.ControlHeader = "CQM Categories"
            'set the property true for refused code you want 
            oCQMListControl.bShowNotTakenCodes = True
            oCQMListControl.bShowAttributeCodes = True
            AddHandler oCQMListControl.ItemSelectedClick, AddressOf oCQMListControl_ItemSelectedClick
            AddHandler oCQMListControl.ItemClosedClick, AddressOf oCQMListControl_ItemClosedClick
            ofrmCQMlList.Controls.Add(oCQMListControl)
            oCQMListControl.Dock = DockStyle.Fill
            oCQMListControl.BringToFront()

            oCQMListControl.ShowHeaderPanel(False)
            ofrmCQMlList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmCQMlList.Text = "CQM Categories"
            ofrmCQMlList.ShowDialog(IIf(IsNothing(CType(ofrmCQMlList, Control).Parent), Me, CType(ofrmCQMlList, Control).Parent))
            If IsNothing(ofrmCQMlList) = False Then
                ofrmCQMlList.Dispose()
                ofrmCQMlList = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "glEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub buttonClearCqmCategories_Click(sender As System.Object, e As System.EventArgs) Handles buttonClearCqmCategories.Click
        txtCqmCategories.Clear()
        strCQMCode = String.Empty
        strCQMDescription = String.Empty
    End Sub
End Class
