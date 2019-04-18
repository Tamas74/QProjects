Public Class frmOBVital

   

    Public _ArrList As New ArrayList
    Public _HashTable As New Hashtable

    'Age 
    Public _AgeInDays As Integer = 0
    Public _AgeInMonths As Double = 0
    Public Years As Int16 = 0
    Public Months As Int16 = 0
    Public Days As Int16 = 0


    Private Const strGAMessage As String = "Do you want to recalculate the Gestational Age?"

    Private Const dblConceptionCalculationDays As Double = 266
    Private Const dblLMPCalculationDays As Double = 280

    '28-Mar-13: Aniket: Make GA Editable
    Private blnLoad As Boolean
    Private blnReCalculateGA As Boolean
    Private blnShowGAPrompt As Boolean = True
    Private blnGAManuallyEdited As Boolean
    Private _IsEditMode As Boolean = False


    Private nGAPreviousWeeks As Integer
    Private nGAPreviousDays As Integer
    Private dtPreviousVitals As DataTable
    Private IsNewPregCase As Boolean = False

    Private minTempValue As Double = 0
    Private maxTempValue As Double = 0
    Private isEDDvaluechanged As Boolean = False
    Private isDeleteClicked As Boolean = False
    Private _isDroppedDown As Boolean = False
    Private _Validate As Boolean = True ''This will stop validating event of all textbox controls when false.
    Private _IsSaveClicked As Boolean = False

    Private THRMin As Double
    Private THRMax As Double
    Private dLastperiod As DateTime
    Private dStatusofLMPeriod As Boolean = False
    Private _IsTextBoxPresent As Boolean = False

    Private objclsOBVitals As New ClsOBVitals
    Private SelectedVitals() As String
    Private Vitals() As String
    Private patID As Long
    Public ObTaskID As Int64 = 0 'added for task created from OB Vitals
    'Added for OB Vitals Comments
    Private oListControl As gloListControl.gloListControl
    Private strComments As String
    Private dtMasterData As New DataTable

    Private strPatientCode As String
    Private strPatientFirstName As String
    Private strPatientMiddleName As String
    Private strPatientLastName As String
    Private strPatientDOB As String
    Private strPatientAge As String
    Private strPatientGender As String
    Private strPatientMaritalStatus As String
    Private _FocusControl As Control ''It will save textbox control which is to be focused after warning message.

    Public Sub FormatAge(ByVal BirthDate As DateTime, ByVal VitalDate As DateTime)
        ' Compute the difference between BirthDate 'CODE FROM gloPM
        _AgeInDays = DateDiff(DateInterval.Day, BirthDate, VitalDate)
        _AgeInMonths = _AgeInDays / 30.4167

        'year and end year. 
        Years = VitalDate.Year - BirthDate.Year
        ' Check if the last year was a full year. 
        If VitalDate < BirthDate.AddYears(Years) AndAlso Years <> 0 Then
            Years -= 1
        End If
        BirthDate = BirthDate.AddYears(Years)
        ' Now we know BirthDate <= end and the diff between them 
        ' is < 1 year. 
        If BirthDate.Year = VitalDate.Year Then
            Months = VitalDate.Month - BirthDate.Month
        Else
            Months = (12 - BirthDate.Month) + VitalDate.Month
        End If
        ' Check if the last month was a full month. 
        If VitalDate < BirthDate.AddMonths(Months) AndAlso Months <> 0 Then
            Months -= 1
        End If
        BirthDate = BirthDate.AddMonths(Months)
        ' Now we know that BirthDate < end and is within 1 month 
        ' of each other. 
        Days = (VitalDate - BirthDate).Days
        'Return years & " years " & months & " months " & days & " days"
    End Sub

    Private Sub frmOBVital_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        RemoveHandler chkConception.CheckedChanged, AddressOf chkConception_CheckedChanged
        RemoveHandler chkLMP.CheckedChanged, AddressOf chkLMP_CheckedChanged
        RemoveHandler chkUltraSound.CheckedChanged, AddressOf chkUltraSound_CheckedChanged
        
    End Sub

    Private Sub frmOBVital_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        RemoveHandler chkConception.CheckedChanged, AddressOf chkConception_CheckedChanged
        RemoveHandler chkLMP.CheckedChanged, AddressOf chkLMP_CheckedChanged
        RemoveHandler chkUltraSound.CheckedChanged, AddressOf chkUltraSound_CheckedChanged

        Dim objUserRights As New clsLoginUserRights
        Dim ogloUserRights As New gloUserRights.ClsgloUserRights(GetConnectionString)
        Try
            objUserRights.RetrieveUserRights(gstrLoginName)
            ogloUserRights.CheckForUserRights(gstrLoginName)
            tls_OBVitals.Visible = objUserRights.ViewOBVitals
        Catch ex As Exception
        Finally
            If (IsNothing(ogloUserRights) = False) Then
                ogloUserRights.Dispose()
                ogloUserRights = Nothing
            End If
            objUserRights = Nothing
        End Try
        

        '28-Mar-13: Aniket: Make GA Editable
        blnLoad = False

        dtpConEstDate.Format = DateTimePickerFormat.Custom
        dtpConEstDate.CustomFormat = " "
        dtpConDueDate.Format = DateTimePickerFormat.Custom
        dtpConDueDate.CustomFormat = " "
        dtpLMPEstDate.Format = DateTimePickerFormat.Custom
        dtpLMPEstDate.CustomFormat = " "
        dtpLMPDueDate.Format = DateTimePickerFormat.Custom
        dtpLMPDueDate.CustomFormat = " "
        dtpUltraDueDate.Format = DateTimePickerFormat.Custom
        dtpUltraDueDate.CustomFormat = " "
        dtpEstDueDate.Format = DateTimePickerFormat.Custom
        dtpEstDueDate.CustomFormat = " "
        'LoadOBVitalControls()

        lblbpsidia.Text = ""
        lblbpsisys.Text = ""
        '16-Apr-13 Aniket: Resolving Bug Bug #48927
        If _IsEditMode = False Then
            frmPatientVitals.OBVitalsEditCount += 1
        Else
            frmPatientVitals.OBVitalsEditCount = 2
        End If
        ''16-Apr-13 Aniket: Resolving Bug Bug #48927
        If frmPatientVitals.OBVitalsEditCount > 0 Then
            dtVitals.Value = Convert.ToDateTime(frmPatientVitals.visDate)
        End If

        Try
            patID = frmPatientVitals.patientID
        Catch ex As Exception
            patID = 0
        End Try

        If (patID > 0) Then
            Get_PatientDetails()
            FormatAge(CType(strPatientDOB, DateTime), CType(dtVitals.Value, DateTime))
        End If

        Dim dt As DateTime = Convert.ToDateTime(frmPatientVitals.visDate)
        ' Dim dtPrevious As DataTable

        'IF MAKE CURRENT EQUALS TRUE
        If frmPatientVitals.makeCurrent = True Then
            _HashTable.Clear()
            _HashTable = GetValuesFromDatabase(frmPatientVitals.prevVitID)
            _IsEditMode = True

        Else
            'ELSE NORMAL BEHAVOUR
            If frmPatientVitals.vitID > 0 And _HashTable.Count = 0 Then
                _HashTable.Clear()
                _HashTable = GetValuesFromDatabase(frmPatientVitals.vitID)
                _IsEditMode = True
            End If

            If (_IsEditMode = False) Then
                dtPreviousVitals = GetPreviousValuesFromHashTable(patID)
            End If

        End If 'END MAKE CURRENT



        If frmPatientVitals.blnMedCatatRisk = True And frmPatientVitals.vitID = 0 Then
            chkobrsk.Checked = True
        End If
        If (_HashTable.Count > 0) Then
            GetValuesFromHashTable(_HashTable)
            ' _HashTable.Clear()
        Else
            AddDefaultDates()
        End If

        Try
            txtWeight.Text = frmPatientVitals.strtxtwghtlbs
        Catch ex As Exception
            txtWeight.Text = "0"
        End Try

        Try
            txtBPSittingMax.Text = frmPatientVitals.strtxtBPSittingMax
        Catch ex As Exception
            txtBPSittingMax.Text = ""
        End Try

        Try
            txtBPSittingMin.Text = frmPatientVitals.strtxtBPSittingMin
        Catch ex As Exception
            txtBPSittingMin.Text = ""
        End Try

        Try
            txtComment.Text = frmPatientVitals.strComments
        Catch ex As Exception
            txtComment.Text = ""
        End Try

        Try
            If (frmPatientVitals.strchkPain = "1") Then
                ' If (Not frmPatientVitals.strPainLevel Is Nothing) OrElse (frmPatientVitals.strPainLevel <> "") Then
                chkpain.Checked = True
                trbPainLevel.Enabled = True
                trbPainLevel.Value = Convert.ToInt16(frmPatientVitals.strPainLevel)
            Else
                chkpain.Checked = False
                trbPainLevel.Enabled = False
                trbPainLevel.Value = 0
            End If
        Catch ex As Exception
            chkpain.Checked = False
            trbPainLevel.Enabled = False
            trbPainLevel.Value = 0
        End Try

       
        If _HashTable.Count = 0 Then
            If (_IsEditMode = False) Then
                If Not IsNewOBPregCase() Then
                    Dateformattoblank()
                End If
            End If
        End If

        '16-Apr-13 Aniket: Resolving Bug Bug #48927
        If frmPatientVitals.OBVitalsEditCount > 1 Then
            dtVitals.Value = Convert.ToDateTime(frmPatientVitals.visDate)
            If (frmPatientVitals.strchkLMP = "1") Then
                If dtpLMPEstDate.Value.Date <> Convert.ToDateTime(frmPatientVitals.lmpDate).Date Then
                    If MessageBox.Show("The OB Vitals LMP date does not match with the Vitals LMP date." + Environment.NewLine + " Would you like to update the OB Vitals LMP date to Vitals LMP date?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
                        dtpLMPEstDate.Value = Convert.ToDateTime(frmPatientVitals.lmpDate)
                        dtpLMPDueDate.Value = Convert.ToDateTime(dtpLMPEstDate.Value).AddDays(dblLMPCalculationDays)
                        Dim _Week As Integer
                        Dim _Days As Integer

                        '16-Apr-13 Aniket: Code Refactoring
                        CalculateAge(dtpLMPEstDate.Value, dtVitals.Value, _Week, _Days)

                        '18-Apr-13 Aniket: Resolving Bug #49436 
                        If _Week = 0 And _Days = 0 Then
                            cbGAWeeks.Text = _Week
                            cbGADays.Text = _Days
                        End If
                        If chkLMP.Checked Then
                            dtpEstDueDate.Value = dtpLMPDueDate.Value
                            If isEDDvaluechanged Then
                                CalulateGAOnEDD()
                                isEDDvaluechanged = False
                            End If
                        End If


                        txtPreferredGesAge.Text = _Week.ToString() + " Weeks and " + _Days.ToString() + " Days"
                    End If
                End If
            End If
        Else
            'Aniket Fixing Bug 49429
            If (frmPatientVitals.strchkLMP = "1") Then
                dtpLMPEstDate.Value = Convert.ToDateTime(frmPatientVitals.lmpDate)
                dtpLMPDueDate.Value = Convert.ToDateTime(dtpLMPEstDate.Value.ToShortDateString()).AddDays(dblLMPCalculationDays)
                If chkLMP.Checked Then
                    dtpEstDueDate.Value = dtpLMPDueDate.Value
                    If isEDDvaluechanged Then
                        CalulateGAOnEDD()
                        isEDDvaluechanged = False
                    End If
                End If
            End If
        End If
        'Added For OB Comments
       


        
        '16-Aug-13 Aniket: Change done because of Phill Mail 'OB Vitals' dated 08/Aug/13
        dtpConEstDate.Focus()
        dtpConEstDate.Select()
        'End If

      



        blnLoad = True

        '26-May-15 Aniket: Resolving Bug #83612: EMR: Vitals- patient name and id is not present on title bar
        Try
            gloPatient.gloPatient.GetWindowTitle(Me, patID, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

        AddHandler chkConception.CheckedChanged, AddressOf chkConception_CheckedChanged
        AddHandler chkLMP.CheckedChanged, AddressOf chkLMP_CheckedChanged
        AddHandler chkUltraSound.CheckedChanged, AddressOf chkUltraSound_CheckedChanged

    End Sub

    Private Sub CalculateAge(ByVal LMPDate As Date, ByVal VitalDate As Date, ByRef Weeks As Integer, ByRef Days As Integer)

        Try

            '08-Aug-13 Aniket: Resolving age calculation issue
            Dim _ageInDays As Integer = DateDiff(DateInterval.Day, Convert.ToDateTime(LMPDate.Date), Convert.ToDateTime(VitalDate.Date))

            Weeks = 0

            Dim _fltWeek As Double = 0.0
            Dim _strArray() As String

            If _ageInDays < 7 Then
                Weeks = 0
            Else
                _fltWeek = Convert.ToDouble(_ageInDays) / 7.0
                _strArray = _fltWeek.ToString().Split(".")
                Weeks = Convert.ToInt16(_strArray(0))
            End If


            Days = _ageInDays Mod 7

            If (Days < 0) Then
                Days = 0
            End If

            If (Weeks < 0) Then
                Weeks = 0
            End If

        Catch ex As Exception

            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

        End Try

    End Sub

    Public Function SaveOBVitalsInHashTable() As Hashtable
        Try

            _HashTable.Clear()

            'Vital Date
            _HashTable.Add("dtVital", Format(dtVitals.Value, "MM/dd/yyyy") & " " & Format(dtVitals.Value, "hh:mm:ss tt"))

            ' All Radio buttons
            If chkConception.Checked = True Then ' rdConception.Checked = True Then
                _HashTable.Add("rdConception", 1)

                If dtpConEstDate.CustomFormat = " " Then
                    _HashTable.Add("dtpConEstDate", Nothing)
                Else
                    _HashTable.Add("dtpConEstDate", Format(dtpConEstDate.Value, "MM/dd/yyyy"))
                End If

                If dtpConDueDate.CustomFormat = " " Then
                    _HashTable.Add("dtpConDueDate", Nothing)
                Else
                    _HashTable.Add("dtpConDueDate", Format(dtpConDueDate.Value, "MM/dd/yyyy"))
                End If

                _HashTable.Add("rdLMP", 0)

                If dtpLMPEstDate.CustomFormat = " " Then
                    _HashTable.Add("dtpLMPEstDate", Nothing)
                Else
                    _HashTable.Add("dtpLMPEstDate", Format(dtpLMPEstDate.Value, "MM/dd/yyyy"))
                End If

                If dtpLMPDueDate.CustomFormat = " " Then
                    _HashTable.Add("dtpLMPDueDate", Nothing)
                Else
                    _HashTable.Add("dtpLMPDueDate", Format(dtpLMPDueDate.Value, "MM/dd/yyyy"))
                End If

                _HashTable.Add("txtLMPGesAge", txtPreferredGesAge.Text.Trim())

                _HashTable.Add("rdUltra", 0)

                If dtpUltraDueDate.CustomFormat = " " Then
                    _HashTable.Add("dtpUltraDueDate", Nothing)
                Else
                    _HashTable.Add("dtpUltraDueDate", Format(dtpUltraDueDate.Value, "MM/dd/yyyy"))
                End If

            ElseIf chkLMP.Checked = True Then ' rdLMP.Checked = True Then
                _HashTable.Add("rdConception", 0)
                If dtpConEstDate.CustomFormat = " " Then
                    _HashTable.Add("dtpConEstDate", Nothing)
                Else
                    _HashTable.Add("dtpConEstDate", Format(dtpConEstDate.Value, "MM/dd/yyyy"))
                End If

                If dtpConDueDate.CustomFormat = " " Then
                    _HashTable.Add("dtpConDueDate", Nothing)
                Else
                    _HashTable.Add("dtpConDueDate", Format(dtpConDueDate.Value, "MM/dd/yyyy"))
                End If


                _HashTable.Add("rdLMP", 1)
                If dtpLMPEstDate.CustomFormat = " " Then
                    _HashTable.Add("dtpLMPEstDate", Nothing)
                Else
                    _HashTable.Add("dtpLMPEstDate", Format(dtpLMPEstDate.Value, "MM/dd/yyyy"))
                End If

                If dtpLMPDueDate.CustomFormat = " " Then
                    _HashTable.Add("dtpLMPDueDate", Nothing)
                Else
                    _HashTable.Add("dtpLMPDueDate", Format(dtpLMPDueDate.Value, "MM/dd/yyyy"))
                End If

                _HashTable.Add("txtLMPGesAge", txtPreferredGesAge.Text.Trim())

                _HashTable.Add("rdUltra", 0)

                If dtpUltraDueDate.CustomFormat = " " Then
                    _HashTable.Add("dtpUltraDueDate", Nothing)
                Else
                    _HashTable.Add("dtpUltraDueDate", Format(dtpUltraDueDate.Value, "MM/dd/yyyy"))
                End If

            ElseIf chkUltraSound.Checked = True Then ' rdUltraSound.Checked = True Then
                _HashTable.Add("rdConception", 0)
                If dtpConEstDate.CustomFormat = " " Then
                    _HashTable.Add("dtpConEstDate", Nothing)
                Else
                    _HashTable.Add("dtpConEstDate", Format(dtpConEstDate.Value, "MM/dd/yyyy"))
                End If

                If dtpConDueDate.CustomFormat = " " Then
                    _HashTable.Add("dtpConDueDate", Nothing)
                Else
                    _HashTable.Add("dtpConDueDate", Format(dtpConDueDate.Value, "MM/dd/yyyy"))
                End If

                _HashTable.Add("rdLMP", 0)

                If dtpLMPEstDate.CustomFormat = " " Then
                    _HashTable.Add("dtpLMPEstDate", Nothing)
                Else
                    _HashTable.Add("dtpLMPEstDate", Format(dtpLMPEstDate.Value, "MM/dd/yyyy"))
                End If

                If dtpLMPDueDate.CustomFormat = " " Then
                    _HashTable.Add("dtpLMPDueDate", Nothing)
                Else
                    _HashTable.Add("dtpLMPDueDate", Format(dtpLMPDueDate.Value, "MM/dd/yyyy"))
                End If

                _HashTable.Add("txtLMPGesAge", txtPreferredGesAge.Text.Trim())

                _HashTable.Add("rdUltra", 1)

                If dtpUltraDueDate.CustomFormat = " " Then
                    _HashTable.Add("dtpUltraDueDate", Nothing)
                Else
                    _HashTable.Add("dtpUltraDueDate", Format(dtpUltraDueDate.Value, "MM/dd/yyyy"))
                End If

            Else
                _HashTable.Add("rdConception", 0)
                If dtpConEstDate.CustomFormat = " " Then
                    _HashTable.Add("dtpConEstDate", Nothing)
                Else
                    _HashTable.Add("dtpConEstDate", Format(dtpConEstDate.Value, "MM/dd/yyyy"))
                End If

                If dtpConDueDate.CustomFormat = " " Then
                    _HashTable.Add("dtpConDueDate", Nothing)
                Else
                    _HashTable.Add("dtpConDueDate", Format(dtpConDueDate.Value, "MM/dd/yyyy"))
                End If

                _HashTable.Add("rdLMP", 0)

                If dtpLMPEstDate.CustomFormat = " " Then
                    _HashTable.Add("dtpLMPEstDate", Nothing)
                Else
                    _HashTable.Add("dtpLMPEstDate", Format(dtpLMPEstDate.Value, "MM/dd/yyyy"))
                End If

                If dtpLMPDueDate.CustomFormat = " " Then
                    _HashTable.Add("dtpLMPDueDate", Nothing)
                Else
                    _HashTable.Add("dtpLMPDueDate", Format(dtpLMPDueDate.Value, "MM/dd/yyyy"))
                End If

                _HashTable.Add("txtLMPGesAge", txtPreferredGesAge.Text.Trim())

                _HashTable.Add("rdUltra", 0)

                If dtpUltraDueDate.CustomFormat = " " Then
                    _HashTable.Add("dtpUltraDueDate", Nothing)
                Else
                    _HashTable.Add("dtpUltraDueDate", Format(dtpUltraDueDate.Value, "MM/dd/yyyy"))
                End If


            End If

            If dtpEstDueDate.CustomFormat = " " Then
                _HashTable.Add("dtpEstDueDate", Nothing)
            Else
                _HashTable.Add("dtpEstDueDate", Format(dtpEstDueDate.Value, "MM/dd/yyyy"))
            End If


            'Preferred Gestational Age 
            _HashTable.Add("txtPreferredGesAge", txtPreferredGesAge.Text)

            

            'Urine
            _HashTable.Add("txtUrineAlbumin", txtUrineAlbumin.Text)
            _HashTable.Add("txtUrineGlucose", txtUrineGlucose.Text)

            'Cervical Exam
            _HashTable.Add("txtDilation", txtDilation.Text)
            _HashTable.Add("txtEffacement", txtEffacement.Text)
            _HashTable.Add("txtStation", txtStation.Text)

            'Pain Level
            If chkpain.Checked = True Then
                _HashTable.Add("chkPain", 1)
                _HashTable.Add("trbPainLevel", trbPainLevel.Value)
            Else
                '01-Jun-16 Aniket: Resolving Bug #96617: gloEMR : New exam (OB vitals liquid link) : OB Vitals pain level liquid link showing data as user uncheck it
                _HashTable.Add("chkPain", 0)
            End If

            'BP
            _HashTable.Add("txtBPSittingMax", txtBPSittingMax.Text)
            _HashTable.Add("txtBPSittingMin", txtBPSittingMin.Text)

            '_HashTable.Add("txtBPStandingMax", txtBPStandingMax.Text)
            '_HashTable.Add("txtBPStandingMin", txtBPStandingMin.Text)

            'Comments
            _HashTable.Add("txtComments", txtComment.Text)

            'Height Weight
            _HashTable.Add("txtFundalHeight", txtFundalHeight.Text)
            _HashTable.Add("txtPrePregWeight", txtPrePregWeight.Text)
            _HashTable.Add("txtWeight", txtWeight.Text)
            _HashTable.Add("txtWeightChange", txtWeightChange.Text)

            'Other
            _HashTable.Add("txtFetalheartrate", txtFetalheartrate.Text)
            _HashTable.Add("txtEdema", txtEdema.Text)
            If cbFetalMovement.SelectedIndex >= 0 Then
                _HashTable.Add("cbFetalMovement", cbFetalMovement.Text)
            Else
                _HashTable.Add("cbFetalMovement", "")
            End If

            If cbPreTermLabor.SelectedIndex >= 0 Then
                _HashTable.Add("cbPreTermLabor", cbPreTermLabor.Text)
            Else
                _HashTable.Add("cbPreTermLabor", "")
            End If

            _HashTable.Add("txtPresentation", txtPresentation.Text)

            _HashTable.Add("txtNextAppointment", txtNextAppointment.Text)

            _HashTable.Add("cbGAWeeks", cbGAWeeks.Text)
            _HashTable.Add("cbGADays", cbGADays.Text)

            If chkobrsk.Checked = True Then
                _HashTable.Add("chkOBrsk", 1)
            Else
                _HashTable.Add("chkOBrsk", 0)
            End If
            _HashTable.Add("txtOBComment", txtobcomm.Text)
            Return _HashTable
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return Nothing
        Finally

        End Try
        ''End
    End Function

    Private Sub tblStrip_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tblStrip.ItemClicked
        tblStrip.Select()
        Select Case e.ClickedItem.Tag
            Case "Close"

                '18-Apr-13 Aniket: Resolving Bug #48927 
                '_HashTable.Clear()
                Me.Close()
            Case "Ok" 'Save + Close
                '_IsSavenClose = True
                _Validate = ValidateDates()
                If (_Validate = True) Then
                    _HashTable = SaveOBVitalsInHashTable()
                    Me.Close()
                End If
            Case "History"
                GetOBHistoryDialogue()
                'Added Code Changes for View OBVitals
            Case "OBVitals"
                ShowPatientOBVital()
            Case "OB Plan"
                GetPatientOBPlan()
        End Select
    End Sub

    Private Sub GetPatientOBPlan()
        Try
            If Trim(strPatientFirstName) <> "" Then

                Dim m_visitID As Int64 = GenerateVisitID(Date.Now(), patID)

                Dim blnRecordLock As Boolean = False

                Dim frmPatOBPlan As New frmPatientOBPlan(m_visitID, Date.Now(), patID, blnRecordLock)
                With frmPatOBPlan
                    .WindowState = FormWindowState.Maximized
                    .StartPosition = FormStartPosition.CenterParent
                    .blnOpenFromExam = True
                    .MdiParent = Me.ParentForm
                    .PopulatePatientOBPlan_Final()
                    If frmPatOBPlan.blncancel Then
                        '7-May-15 Aniket: Setting the owner
                        .ShowDialog(IIf(IsNothing(frmPatOBPlan.Parent), Me, frmPatOBPlan.Parent))
                        '.BringToFront()
                        '.WindowState = FormWindowState.Maximized
                        frmPatOBPlan.Close() ''added for memory issues 
                        frmPatOBPlan.Dispose()
                        frmPatOBPlan = Nothing
                    Else
                        frmPatOBPlan.Close() 'Change made to solve memory Leak and word crash issue
                        frmPatOBPlan.Dispose()
                        frmPatOBPlan = Nothing
                    End If
                End With
            Else
                MessageBox.Show("Please select the Patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Vitals, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing

        End Try

    End Sub
    'Added Code Changes for View OBVitals
    Private Sub ShowPatientOBVital()
        Try
            If Trim(strPatientFirstName) <> "" Then

                Dim ofrm As New frmVWPatientOBVitals(patID)
                With ofrm
                    .WindowState = FormWindowState.Maximized
                    .StartPosition = FormStartPosition.CenterParent
                    .blnOpenFromExam = True
                    .MdiParent = Me.ParentForm

                    .ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                    ofrm.Dispose()
                    ofrm = Nothing

                End With
            Else
                MessageBox.Show("Please select the Patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Vitals, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing

        End Try

    End Sub

    Private Sub GetOBHistoryDialogue()
        Dim blnRecordLock As Boolean = False


        Dim ofrmHistory As New frmHistory(frmPatientVitals.nVisitID, frmPatientVitals.visDate, frmPatientVitals.patientID, blnRecordLock, , True)
        Try
            If Trim(strPatientFirstName) <> "" Then
                If frmPatientVitals.nVisitID = 0 Then
                    Try
                        frmPatientVitals.nVisitID = GenerateVisitID(frmPatientVitals.visDate, frmPatientVitals.patientID)
                    Catch ex As Exception

                    End Try

                End If
                ofrmHistory.StartPosition = FormStartPosition.CenterParent
                If (frmHistory.IsOpen = False) Then
                    ofrmHistory.blnShowMessageBox = False
                    ofrmHistory.PopulatePatientHistory_Final()
                End If
                If ofrmHistory.blncancel Then
                    '  ofrmHistory.tblHistory.Enabled = False
                    ofrmHistory.WindowState = FormWindowState.Maximized
                    ofrmHistory.uiPanSplitScreen_History.Visible = False
                    ofrmHistory.uiPanSplitScreen_History.Enabled = False
                    ofrmHistory.ShowDialog(Me)


                    ofrmHistory.Close() ''added for memory issues
                    ofrmHistory.Dispose()    '24-Apr-15 Aniket: Dispose added for ShowDialog
                    ofrmHistory = Nothing
                Else
                    ofrmHistory.Close() 'Change made to solve memory Leak and word crash issue
                    ofrmHistory.Dispose()
                    ofrmHistory = Nothing
                    Me.BringToFront()
                End If
            Else
                MessageBox.Show("Please select the Patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Vitals, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If (IsNothing(ofrmHistory) = False) Then
                ofrmHistory.Dispose()
                ofrmHistory = Nothing
            End If
        End Try
    End Sub

    Private Sub GetValuesFromHashTable(ByVal HashTable As Hashtable)
        Dim _whichRadioChecked As String = String.Empty
        For Each element As DictionaryEntry In _HashTable
            Select Case element.Key
                'dtOBVital Date
                Case "dtVital"
                    dtVitals.Value = Convert.ToDateTime(element.Value)

                Case "rdConception"  'Radio Conception
                    If Convert.ToInt16(element.Value) = 1 Then
                        'rdConception.Checked = True
                        'rdLMP.Checked = False
                        'rdUltraSound.Checked = False
                        chkConception.Checked = True
                        chkLMP.Checked = False
                        chkUltraSound.Checked = False
                        _whichRadioChecked = "rdConception"
                    Else
                        'rdConception.Checked = False
                        chkUltraSound.Checked = False
                    End If

                Case "dtpConEstDate"
                    If element.Value <> String.Empty Then
                        dtpConEstDate.Value = Convert.ToDateTime(element.Value)
                        '' Reason behind resetting this, If same value is set then event not firing & due to which format won't change.
                        If dtpConEstDate.Format <> DateTimePickerFormat.Short Then
                            dtpConEstDate.Format = DateTimePickerFormat.Short
                        End If
                        dtpConEstDate.CustomFormat = "MM/dd/yyyy"
                    End If

                Case "dtpConDueDate"
                    If element.Value <> String.Empty Then
                        dtpConDueDate.Value = Convert.ToDateTime(element.Value)
                        If dtpConDueDate.Format <> DateTimePickerFormat.Short Then
                            dtpConDueDate.Format = DateTimePickerFormat.Short
                        End If
                        dtpConDueDate.CustomFormat = "MM/dd/yyyy"
                    End If

                    'Case "txtConGesAge"
                    '    If element.Value <> String.Empty Then
                    '        txtConGesAge.Text = Convert.ToString(element.Value)
                    '    End If

                Case "rdLMP"  'Radio LMP
                    If Convert.ToInt16(element.Value) = 1 Then
                        'rdLMP.Checked = True
                        'rdConception.Checked = False
                        'rdUltraSound.Checked = False

                        chkConception.Checked = False
                        chkLMP.Checked = True
                        chkUltraSound.Checked = False


                        _whichRadioChecked = "rdLMP"
                    Else
                        'rdLMP.Checked = False
                        chkLMP.Checked = False
                    End If

                Case "dtpLMPEstDate"
                    If element.Value <> String.Empty Then
                        dtpLMPEstDate.Value = Convert.ToDateTime(element.Value)
                        If dtpLMPEstDate.Format <> DateTimePickerFormat.Short Then
                            dtpLMPEstDate.Format = DateTimePickerFormat.Short
                        End If
                        dtpLMPEstDate.CustomFormat = "MM/dd/yyyy"
                    End If
                Case "dtpEstDueDate"
                    If element.Value <> String.Empty Then
                        dtpEstDueDate.Value = Convert.ToDateTime(element.Value)
                    End If
                Case "dtpLMPDueDate"
                    If element.Value <> String.Empty Then
                        dtpLMPDueDate.Value = Convert.ToDateTime(element.Value)
                        If dtpLMPDueDate.Format <> DateTimePickerFormat.Short Then
                            dtpLMPDueDate.Format = DateTimePickerFormat.Short
                        End If
                        dtpLMPDueDate.CustomFormat = "MM/dd/yyyy"
                    End If

                Case "txtLMPGesAge"
                    If element.Value <> String.Empty Then
                        txtPreferredGesAge.Text = Convert.ToString(element.Value)
                    End If

                Case "rdUltra"  'Radio ULTRA
                    If Convert.ToInt16(element.Value) = 1 Then
                        'rdUltraSound.Checked = True
                        'rdLMP.Checked = False
                        'rdConception.Checked = False

                        chkConception.Checked = False
                        chkLMP.Checked = False
                        chkUltraSound.Checked = True
                        _whichRadioChecked = "rdUltra"

                    Else
                        'rdUltraSound.Checked = False
                        chkUltraSound.Checked = False
                    End If

                    'Case "dtpUltraEstDate"
                    '    If element.Value <> String.Empty Then
                    '        dtpUltraEstDate.Value = Convert.ToDateTime(element.Value)
                    '    End If

                Case "dtpUltraDueDate"
                    If element.Value <> String.Empty Then
                        dtpUltraDueDate.Value = Convert.ToDateTime(element.Value)
                        If dtpUltraDueDate.Format <> DateTimePickerFormat.Short Then
                            dtpUltraDueDate.Format = DateTimePickerFormat.Short
                        End If
                        dtpUltraDueDate.CustomFormat = "MM/dd/yyyy"
                    End If

                    'Case "txtUltraGesAge"
                    '    If element.Value <> String.Empty Then
                    '        txtUltraGesAge.Text = Convert.ToString(element.Value)
                    '    End If

                Case "txtWeightChange" 'Weight & Height Related
                    txtWeightChange.Text = Convert.ToString(element.Value)

                Case "txtPrePregWeight"
                    txtPrePregWeight.Text = Convert.ToString(element.Value)

                Case "txtFundalHeight"
                    txtFundalHeight.Text = Convert.ToString(element.Value)

                   

                Case "txtUrineAlbumin" 'Urine
                    txtUrineAlbumin.Text = Convert.ToString(element.Value)

                Case "txtUrineGlucose"
                    txtUrineGlucose.Text = Convert.ToString(element.Value)

                Case "txtDilation" 'Cervix
                    txtDilation.Text = Convert.ToString(element.Value)

                Case "txtEffacement"
                    txtEffacement.Text = Convert.ToString(element.Value)

                Case "txtStation"
                    txtStation.Text = Convert.ToString(element.Value)

                Case "txtFetalheartrate" 'Other
                    txtFetalheartrate.Text = Convert.ToString(element.Value)

                Case "txtEdema"
                    txtEdema.Text = Convert.ToString(element.Value)

                Case "cbFetalMovement"
                    cbFetalMovement.Text = Convert.ToString(element.Value)

                Case "cbPreTermLabor"
                    cbPreTermLabor.Text = Convert.ToString(element.Value)

                Case "txtPresentation"
                    txtPresentation.Text = Convert.ToString(element.Value)

                Case "txtNextAppointment" 'Next Appointment
                    txtNextAppointment.Text = Convert.ToString(element.Value)

                Case "cbGAWeeks" 'cbGAWeeks
                    cbGAWeeks.Text = element.Value

                Case "cbGADays" 'cbGADays
                    cbGADays.Text = element.Value
                Case "chkOBrsk"
                    chkobrsk.Checked = Convert.ToBoolean(element.Value)
                Case "txtOBComment"
                    txtobcomm.Text = Convert.ToString(element.Value)
            End Select
        Next



    End Sub

    Private Function GetValuesFromDatabase(ByVal vitID As Long) As Hashtable
        Dim _hash As New Hashtable()
        Dim _dt As New DataTable()
        '   Dim _whichRadioChecked As String
        _dt = objclsOBVitals.GetOBVitals(vitID)
        If Not IsNothing(_dt) Then
            If _dt.Rows.Count > 0 Then

                'Vital Date

               
                'OB Vital Date
                If Not IsDBNull(_dt.Rows(0)("dtOBVitalDate")) Then
                    If _dt.Rows(0)("dtOBVitalDate").ToString() <> "" Then
                        _hash.Add("dtVital", Format(_dt.Rows(0)("dtOBVitalDate"), "MM/dd/yyyy hh:mm:ss tt"))
                    Else
                        _hash.Add("dtVital", "")
                    End If
                Else
                    _hash.Add("dtVital", "")
                End If

                'Radio Conception
                If Not IsDBNull(_dt.Rows(0)("bConception")) Then
                    If _dt.Rows(0)("bConception").ToString() <> "" Then
                        _hash.Add("rdConception", Convert.ToInt16(_dt.Rows(0)("bConception")))
                    Else
                        _hash.Add("rdConception", 0)
                    End If
                Else
                    _hash.Add("rdConception", 0)
                End If


                If Not IsDBNull(_dt.Rows(0)("dtConceptionEst")) Then
                    If _dt.Rows(0)("dtConceptionEst").ToString() <> "" Then
                        _hash.Add("dtpConEstDate", Format(_dt.Rows(0)("dtConceptionEst"), "MM/dd/yyyy"))
                    Else
                        _hash.Add("dtpConEstDate", Format(System.DateTime.Now, "MM/dd/yyyy"))
                    End If
                Else
                    _hash.Add("dtpConEstDate", "")
                    '_hash.Add("dtpConEstDate", Format(System.DateTime.Now, "MM/dd/yyyy"))
                End If


                If Not IsDBNull(_dt.Rows(0)("dtConceptionDue")) Then
                    If _dt.Rows(0)("dtConceptionDue").ToString() <> "" Then
                        _hash.Add("dtpConDueDate", Format(_dt.Rows(0)("dtConceptionDue"), "MM/dd/yyyy"))
                    Else
                        _hash.Add("dtpConDueDate", Format(System.DateTime.Now, "MM/dd/yyyy"))
                    End If
                Else
                    _hash.Add("dtpConDueDate", "")
                    '_hash.Add("dtpConDueDate", Format(System.DateTime.Now, "MM/dd/yyyy"))
                End If


                If Not IsDBNull(_dt.Rows(0)("dtEstimatedDueDate")) Then
                    If _dt.Rows(0)("dtEstimatedDueDate").ToString() <> "" Then
                        _hash.Add("dtpEstDueDate", Format(_dt.Rows(0)("dtEstimatedDueDate"), "MM/dd/yyyy"))
                    Else
                        _hash.Add("dtpEstDueDate", Format(System.DateTime.Now, "MM/dd/yyyy"))
                    End If
                Else
                    _hash.Add("dtpEstDueDate", "")
                    '_hash.Add("dtpConDueDate", Format(System.DateTime.Now, "MM/dd/yyyy"))
                End If

                

                'Radio LMP
                If Not IsDBNull(_dt.Rows(0)("bLastMenstrualPeriod")) Then
                    If _dt.Rows(0)("bLastMenstrualPeriod").ToString() <> "" Then
                        If Convert.ToBoolean(_dt.Rows(0)("bLastMenstrualPeriod").ToString()) = True Then
                            _hash.Add("rdLMP", Convert.ToInt16(_dt.Rows(0)("bLastMenstrualPeriod")))
                        Else
                            _hash.Add("rdLMP", 0)
                        End If

                    Else
                        _hash.Add("rdLMP", 0)
                    End If
                Else
                    _hash.Add("rdLMP", 0)
                End If


                If Not IsDBNull(_dt.Rows(0)("dtLMPEst")) Then
                    If _dt.Rows(0)("dtLMPEst").ToString() <> "" Then
                        _hash.Add("dtpLMPEstDate", Format(_dt.Rows(0)("dtLMPEst"), "MM/dd/yyyy"))
                    Else
                        _hash.Add("dtpLMPEstDate", Format(System.DateTime.Now, "MM/dd/yyyy"))
                    End If
                Else
                    _hash.Add("dtpLMPEstDate", "")
                    '_hash.Add("dtpLMPEstDate", Format(System.DateTime.Now, "MM/dd/yyyy"))
                End If


                If Not IsDBNull(_dt.Rows(0)("dtLMPDue")) Then
                    If _dt.Rows(0)("dtLMPDue").ToString() <> "" Then
                        _hash.Add("dtpLMPDueDate", Format(_dt.Rows(0)("dtLMPDue"), "MM/dd/yyyy"))
                    Else
                        _hash.Add("dtpLMPDueDate", Format(System.DateTime.Now, "MM/dd/yyyy"))
                    End If
                Else
                    _hash.Add("dtpLMPDueDate", "")
                    '_hash.Add("dtpLMPDueDate", Format(System.DateTime.Now, "MM/dd/yyyy"))
                End If


                If Not IsDBNull(_dt.Rows(0)("sLMPGeAge")) Then
                    If _dt.Rows(0)("sLMPGeAge").ToString() <> "" Then
                        _hash.Add("txtLMPGesAge", Convert.ToString(_dt.Rows(0)("sLMPGeAge")))
                    Else
                        _hash.Add("txtLMPGesAge", "")
                    End If
                Else
                    _hash.Add("txtLMPGesAge", "")
                End If

                'Radio UltraSound
                If Not IsDBNull(_dt.Rows(0)("bUltraSound")) Then
                    If _dt.Rows(0)("bUltraSound").ToString() <> "" Then
                        If Convert.ToBoolean(_dt.Rows(0)("bUltraSound").ToString()) = True Then
                            _hash.Add("rdUltra", Convert.ToInt16(_dt.Rows(0)("bUltraSound")))
                        Else
                            _hash.Add("rdUltra", 0)
                        End If

                    Else
                        _hash.Add("rdUltra", 0)
                    End If
                Else
                    _hash.Add("rdUltra", 0)
                End If


              


                If Not IsDBNull(_dt.Rows(0)("dtUltraSoundDue")) Then
                    If _dt.Rows(0)("dtUltraSoundDue").ToString() <> "" Then
                        _hash.Add("dtpUltraDueDate", Format(_dt.Rows(0)("dtUltraSoundDue"), "MM/dd/yyyy"))
                    Else
                        _hash.Add("dtpUltraDueDate", Format(System.DateTime.Now, "MM/dd/yyyy"))
                    End If
                Else
                    _hash.Add("dtpUltraDueDate", "")
                    '_hash.Add("dtpUltraDueDate", Format(System.DateTime.Now, "MM/dd/yyyy"))
                End If


                

                'Obstetric History
                If Not IsDBNull(_dt.Rows(0)("nTotalPregnancies")) Then
                    If _dt.Rows(0)("nTotalPregnancies").ToString() <> "" Then
                        _hash.Add("txtTotalPregenancies", Convert.ToString(_dt.Rows(0)("nTotalPregnancies")))
                    Else
                        _hash.Add("txtTotalPregenancies", "")
                    End If
                Else
                    _hash.Add("txtTotalPregenancies", "")
                End If


                If Not IsDBNull(_dt.Rows(0)("nFullTermDeliveries")) Then
                    If _dt.Rows(0)("nFullTermDeliveries").ToString() <> "" Then
                        _hash.Add("txtFullTerm", Convert.ToString(_dt.Rows(0)("nFullTermDeliveries")))
                    Else
                        _hash.Add("txtFullTerm", "")
                    End If
                Else
                    _hash.Add("txtFullTerm", "")
                End If

                'Preferred Gestational Age 
                ' _HashTable.Add("txtPreferredGesAge", txtPreferredGesAge.Text)

                If Not IsDBNull(_dt.Rows(0)("nPremature")) Then
                    If _dt.Rows(0)("nPremature").ToString() <> "" Then
                        _hash.Add("txtPremature", Convert.ToString(_dt.Rows(0)("nPremature")))
                    Else
                        _hash.Add("txtPremature", "")
                    End If
                Else
                    _hash.Add("txtPremature", "")
                End If

                If Not IsDBNull(_dt.Rows(0)("nAbortionsInduced")) Then
                    If _dt.Rows(0)("nAbortionsInduced").ToString() <> "" Then
                        _hash.Add("txtAbortedInduced", Convert.ToString(_dt.Rows(0)("nAbortionsInduced")))
                    Else
                        _hash.Add("txtAbortedInduced", "")
                    End If
                Else
                    _hash.Add("txtAbortedInduced", "")
                End If

                If Not IsDBNull(_dt.Rows(0)("nAbortionsSpontaneous")) Then
                    If _dt.Rows(0)("nAbortionsSpontaneous").ToString() <> "" Then
                        _hash.Add("txtAbortedSpontanoues", Convert.ToString(_dt.Rows(0)("nAbortionsSpontaneous")))
                    Else
                        _hash.Add("txtAbortedSpontanoues", "")
                    End If
                Else
                    _hash.Add("txtAbortedSpontanoues", "")
                End If

                If Not IsDBNull(_dt.Rows(0)("nEctopics")) Then
                    If _dt.Rows(0)("nEctopics").ToString() <> "" Then
                        _hash.Add("txtEctopic", Convert.ToString(_dt.Rows(0)("nEctopics")))
                    Else
                        _hash.Add("txtEctopic", "")
                    End If
                Else
                    _hash.Add("txtEctopic", "")
                End If

                If Not IsDBNull(_dt.Rows(0)("nMultipleBirths")) Then
                    If _dt.Rows(0)("nMultipleBirths").ToString() <> "" Then
                        _hash.Add("txtMultipleBirth", Convert.ToString(_dt.Rows(0)("nMultipleBirths")))
                    Else
                        _hash.Add("txtMultipleBirth", "")
                    End If
                Else
                    _hash.Add("txtMultipleBirth", "")
                End If

                If Not IsDBNull(_dt.Rows(0)("nLivingChildren")) Then
                    If _dt.Rows(0)("nLivingChildren").ToString() <> "" Then
                        _hash.Add("txtLiving", Convert.ToString(_dt.Rows(0)("nLivingChildren")))
                    Else
                        _hash.Add("txtLiving", "")
                    End If
                Else
                    _hash.Add("txtLiving", "")
                End If

                'Urine
                If Not IsDBNull(_dt.Rows(0)("sUrineAlbumin")) Then
                    If _dt.Rows(0)("sUrineAlbumin").ToString() <> "" Then
                        _hash.Add("txtUrineAlbumin", Convert.ToString(_dt.Rows(0)("sUrineAlbumin")))
                    Else
                        _hash.Add("txtUrineAlbumin", "")
                    End If
                Else
                    _hash.Add("txtUrineAlbumin", "")
                End If

                If Not IsDBNull(_dt.Rows(0)("sUrineGlucose")) Then
                    If _dt.Rows(0)("sUrineGlucose").ToString() <> "" Then
                        _hash.Add("txtUrineGlucose", Convert.ToString(_dt.Rows(0)("sUrineGlucose")))
                    Else
                        _hash.Add("txtUrineGlucose", "")
                    End If
                Else
                    _hash.Add("txtUrineGlucose", "")
                End If

                'Cervical Exam
                If Not IsDBNull(_dt.Rows(0)("sCervixExamDilation")) Then
                    If _dt.Rows(0)("sCervixExamDilation").ToString() <> "" Then
                        _hash.Add("txtDilation", Convert.ToString(_dt.Rows(0)("sCervixExamDilation")))
                    Else
                        _hash.Add("txtDilation", "")
                    End If
                Else
                    _hash.Add("txtDilation", "")
                End If

                If Not IsDBNull(_dt.Rows(0)("sCervixExamEffacement")) Then
                    If _dt.Rows(0)("sCervixExamEffacement").ToString() <> "" Then
                        _hash.Add("txtEffacement", Convert.ToString(_dt.Rows(0)("sCervixExamEffacement")))
                    Else
                        _hash.Add("txtEffacement", "")
                    End If
                Else
                    _hash.Add("txtEffacement", "")
                End If

                If Not IsDBNull(_dt.Rows(0)("sCervixExamStation")) Then
                    If _dt.Rows(0)("sCervixExamStation").ToString() <> "" Then
                        _hash.Add("txtStation", Convert.ToString(_dt.Rows(0)("sCervixExamStation")))
                    Else
                        _hash.Add("txtStation", "")
                    End If
                Else
                    _hash.Add("txtStation", "")
                End If

                'Height Weight

                If Not IsDBNull(_dt.Rows(0)("dFundalHeight")) Then
                    If _dt.Rows(0)("dFundalHeight").ToString() <> "" Then
                        _hash.Add("txtFundalHeight", Convert.ToString(_dt.Rows(0)("dFundalHeight")))
                    Else
                        _hash.Add("txtFundalHeight", "")
                    End If
                Else
                    _hash.Add("txtFundalHeight", "")
                End If

                If Not IsDBNull(_dt.Rows(0)("nWeightChange")) Then
                    If _dt.Rows(0)("nWeightChange").ToString() <> "" Then
                        _hash.Add("txtWeightChange", Convert.ToString(_dt.Rows(0)("nWeightChange")))
                    Else
                        _hash.Add("txtWeightChange", "")
                    End If
                Else
                    _hash.Add("txtWeightChange", "")
                End If

                If Not IsDBNull(_dt.Rows(0)("nPrePregencyWeight")) Then
                    If _dt.Rows(0)("nPrePregencyWeight").ToString() <> "" Then
                        _hash.Add("txtPrePregWeight", Convert.ToString(_dt.Rows(0)("nPrePregencyWeight")))
                    Else
                        _hash.Add("txtPrePregWeight", "")
                    End If
                Else
                    _hash.Add("txtPrePregWeight", "")
                End If


                'Other

                If Not IsDBNull(_dt.Rows(0)("sFetalHeartRate")) Then
                    If _dt.Rows(0)("sFetalHeartRate").ToString() <> "" Then
                        _hash.Add("txtFetalheartrate", Convert.ToString(_dt.Rows(0)("sFetalHeartRate")))
                    Else
                        _hash.Add("txtFetalheartrate", "")
                    End If
                Else
                    _hash.Add("txtFetalheartrate", "")
                End If

                If Not IsDBNull(_dt.Rows(0)("sEdema")) Then
                    If _dt.Rows(0)("sEdema").ToString() <> "" Then
                        _hash.Add("txtEdema", Convert.ToString(_dt.Rows(0)("sEdema")))
                    Else
                        _hash.Add("txtEdema", "")
                    End If
                Else
                    _hash.Add("txtEdema", "")
                End If

                If Not IsDBNull(_dt.Rows(0)("sFetalMovement")) Then
                    If _dt.Rows(0)("sFetalMovement").ToString() <> "" Then
                        _hash.Add("cbFetalMovement", Convert.ToString(_dt.Rows(0)("sFetalMovement")))
                    Else
                        _hash.Add("cbFetalMovement", "")
                    End If
                Else
                    _hash.Add("cbFetalMovement", "")
                End If

                If Not IsDBNull(_dt.Rows(0)("sPreTermLaborSigns")) Then
                    If _dt.Rows(0)("sPreTermLaborSigns").ToString() <> "" Then
                        _hash.Add("cbPreTermLabor", Convert.ToString(_dt.Rows(0)("sPreTermLaborSigns")))
                    Else
                        _hash.Add("cbPreTermLabor", "")
                    End If
                Else
                    _hash.Add("cbPreTermLabor", "")
                End If

                If Not IsDBNull(_dt.Rows(0)("sPresentation")) Then
                    If _dt.Rows(0)("sPresentation").ToString() <> "" Then
                        _hash.Add("txtPresentation", Convert.ToString(_dt.Rows(0)("sPresentation")))
                    Else
                        _hash.Add("txtPresentation", "")
                    End If
                Else
                    _hash.Add("txtPresentation", "")
                End If

                'Next Appointment
                If Not IsDBNull(_dt.Rows(0)("sNextAppointment")) Then
                    If _dt.Rows(0)("sNextAppointment").ToString() <> "" Then
                        _hash.Add("txtNextAppointment", Convert.ToString(_dt.Rows(0)("sNextAppointment")))
                    Else
                        _hash.Add("txtNextAppointment", "")
                    End If
                Else
                    _hash.Add("txtNextAppointment", "")
                End If

                'GA Weeks
                If Not IsDBNull(_dt.Rows(0)("nGAWeeks")) Then
                    If _dt.Rows(0)("nGAWeeks").ToString() <> "" Then
                        _hash.Add("cbGAWeeks", Convert.ToString(_dt.Rows(0)("nGAWeeks")))
                    Else
                        _hash.Add("cbGAWeeks", "")
                    End If
                Else
                    _hash.Add("cbGAWeeks", "")
                End If

                'GA Days
                If Not IsDBNull(_dt.Rows(0)("nGADays")) Then
                    If _dt.Rows(0)("nGADays").ToString() <> "" Then
                        _hash.Add("cbGADays", Convert.ToString(_dt.Rows(0)("nGADays")))
                    Else
                        _hash.Add("cbGADays", "")
                    End If
                Else
                    _hash.Add("cbGADays", "")
                End If

                If Not IsDBNull(_dt.Rows(0)("bISPatientAtRisk")) Then
                    If _dt.Rows(0)("bISPatientAtRisk").ToString() <> "" Then
                        _hash.Add("chkOBrsk", Convert.ToString(_dt.Rows(0)("bISPatientAtRisk")))
                    Else
                        _hash.Add("chkOBrsk", "False")
                    End If
                Else
                    _hash.Add("chkOBrsk", "False")
                End If

                If Not IsDBNull(_dt.Rows(0)("sRiskComments")) Then
                    If _dt.Rows(0)("sRiskComments").ToString() <> "" Then
                        _hash.Add("txtOBComment", Convert.ToString(_dt.Rows(0)("sRiskComments")))
                    Else
                        _hash.Add("txtOBComment", "")
                    End If
                Else
                    _hash.Add("txtOBComment", "")
                End If

            End If
        End If
        Return _hash
    End Function

    Private Sub CalculateGA(ByVal Week As Integer, ByVal Days As Integer, Optional ByVal flagMethod As Int16 = 0)

        Dim strMessage As String = Nothing

        If flagMethod = 0 Then
            strMessage = "Calculating Gestational Age from LMP" & vbCrLf & vbCrLf & "Accept new Gestational Age of " & If(Week > 42, "42", Week.ToString()) + " Weeks and " + If(Days > 6, "6", Days.ToString()) + " Days" & " ?"
        ElseIf flagMethod = 1 Then
            strMessage = "Calculating Gestational Age from EDD" & vbCrLf & vbCrLf & "Accept new Gestational Age of " & If(Week > 42, "42", Week.ToString()) + " Weeks and " + If(Days > 6, "6", Days.ToString()) + " Days" & " ?"
        End If


        If cbGADays.Text = "" And cbGAWeeks.Text = "" Then

            If Week > 42 Then

                'MsgBox("The LMP Date entered is too far in the past and hence Gestational Age cannot be calculated from it.", MsgBoxStyle.Information)

                cbGAWeeks.Text = ""
                cbGADays.Text = ""
            Else
                cbGAWeeks.Text = Week
                cbGADays.Text = Days
            End If

          


            txtPreferredGesAge.Text = Week.ToString() + " Weeks and " + Days.ToString() + " Days"
        Else

            If Week > 42 Then

                'MsgBox("The LMP Date entered is too far in the past and hence Gestational Age cannot be calculated from it.", MsgBoxStyle.Information)

                cbGAWeeks.Text = ""
                cbGADays.Text = ""
                'Aniket Fixing Bug 49429
                Exit Sub
            Else

                '20-Aug-13 Aniket: Do not show the message if the user does not change the date
                '30-Oct-13 Aniket: Fixing Bug #58977
                If CInt(Val(cbGAWeeks.Text)) = Week And CInt(Val(cbGADays.Text)) = Days Then
                    Exit Sub
                End If

                '08-Apr-13 Aniket: OB Vital GA Field Editable change
                If blnShowGAPrompt = True Then

                    '20-Aug-13 Aniket: Resolving Bug #56047: Double message issue
                    blnShowGAPrompt = False

                    If (MsgBox(strMessage, MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton1) = MsgBoxResult.Yes) Then

                        blnReCalculateGA = True

                    Else
                        blnReCalculateGA = False

                    End If

                End If

            End If

            If blnReCalculateGA = True Then

                cbGAWeeks.Text = Week
                cbGADays.Text = Days

                txtPreferredGesAge.Text = Week.ToString() + " Weeks and " + Days.ToString() + " Days"

            End If

            blnShowGAPrompt = False


        End If

    End Sub

    Private Sub AllowDecimal(ByVal Text As String, ByVal e As KeyPressEventArgs)
        Try
            'Allow only numeric and decimal point keys
            If InStr(Trim(Text), ".") <> 0 AndAlso (e.KeyChar = ChrW(46)) Then
                e.Handled = True
            Else
                If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(46)) OrElse (e.KeyChar = ChrW(8))) Then
                    e.Handled = True
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub txtBPSittingMin_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBPSittingMin.Leave
        ChangeColorofInvalidValues(sender)
    End Sub

    Private Sub txtBPSittingMax_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBPSittingMax.Leave
        ChangeColorofInvalidValues(sender)
    End Sub

    Private Sub txtBPSittingMin_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBPSittingMin.KeyPress
        'Allow only numeric and Not decimal point keys
        If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If
        Exit Sub
    End Sub

    Private Sub txtBPSittingMax_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBPSittingMax.KeyPress
        'Allow only numeric and Not decimal point keys
        If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If
        Exit Sub
    End Sub

    Private Sub txtTotalPregenancies_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'Allow only numeric and Not decimal point keys
        If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtFullTerm_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'Allow only numeric and Not decimal point keys
        If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtPremature_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'Allow only numeric and Not decimal point keys
        If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtAbortedSpontanoues_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'Allow only numeric and Not decimal point keys
        If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtAbortedInduced_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'Allow only numeric and Not decimal point keys
        If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtEctopic_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'Allow only numeric and Not decimal point keys
        If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtMultipleBirth_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'Allow only numeric and Not decimal point keys
        If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtLiving_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'Allow only numeric and Not decimal point keys
        If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtFundalHeight_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFundalHeight.KeyPress
        'Allow only numeric and decimal point keys
        AllowDecimal(txtFundalHeight.Text, e)
    End Sub

    Private Sub txtDilation_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDilation.KeyPress
        AllowDecimal(txtDilation.Text, e)
    End Sub

    Private Sub txtEffacement_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEffacement.KeyPress
        AllowDecimal(txtEffacement.Text, e)
    End Sub

    Private Sub txtPrePregWeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrePregWeight.KeyPress
        AllowDecimal(txtPrePregWeight.Text, e)
    End Sub

    Private Sub txtPrePregWeight_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrePregWeight.LostFocus
        CheckWeightChange()
    End Sub

    Private Sub txtWeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWeight.KeyPress
        AllowDecimal(txtWeight.Text, e)
    End Sub

    Private Sub txtWeight_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWeight.LostFocus
        CheckWeightChange()
    End Sub

    Private Sub CheckWeightChange()
        If txtPrePregWeight.Text.Trim().Length > 0 And txtWeight.Text.Trim().Length > 0 Then
            Dim dweight As Decimal = Convert.ToDecimal(txtWeight.Text.Trim()) - Convert.ToDecimal(txtPrePregWeight.Text.Trim())
            'txtWeightChange.Text = Convert.ToString(dweight)
            txtWeightChange.Text = Format(dweight, "#0.00")
        End If
    End Sub

    '16-Aug-13 Aniket: Change done because of Phill Mail 'OB Vitals' dated 08/Aug/13
    Private Sub dtpLMPEstDate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpLMPEstDate.LostFocus

        'Debug.WriteLine("dtpLMPEstDate_LostFocus")

        ''28-Mar-13: Aniket: Make GA Editable
        'If blnLoad = True Then

        '    '08-Jun-16 Aniket: Resolving Bug #96806: gloEMR : OB Vitals : Application takes default date and calculate estimated due date
        '    If dtpLMPEstDate.Text <> " " Then
        '        dtpLMPDueDate.Value = Convert.ToDateTime(dtpLMPEstDate.Value.ToShortDateString()).AddDays(dblLMPCalculationDays)

        '        Dim _Week As Integer
        '        Dim _Days As Integer

        '        '16-Apr-13 Aniket: Code Refactoring
        '        CalculateAge(dtpLMPEstDate.Value, dtVitals.Value, _Week, _Days)


        '        CalculateGA(_Week, _Days)

        '        If chkLMP.Checked Then
        '            blnLoad = False
        '            dtpEstDueDate.Value = dtpLMPDueDate.Value
        '            blnLoad = True
        '        Else
        '            If chkUltraSound.Checked = False And chkConception.Checked = False Then
        '                cbGAWeeks.Text = ""
        '                cbGADays.Text = ""
        '            End If
        '        End If
        '    End If
        'End If

    End Sub

    Private Sub dtpConEstDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpConEstDate.ValueChanged
        If dtpConEstDate.Format <> DateTimePickerFormat.Short Then
            dtpConEstDate.Format = DateTimePickerFormat.Short
        End If
        dtpConEstDate.CustomFormat = "MM/dd/yyyy"
        dtpConDueDate.Value = Convert.ToDateTime(dtpConEstDate.Value).AddDays(dblConceptionCalculationDays)
        If chkConception.Checked Then
            dtpEstDueDate.Value = dtpConDueDate.Value
        End If
    End Sub

    Private Sub AddDefaultDates()

        If Not IsNothing(dtPreviousVitals) Then

            If dtPreviousVitals.Rows.Count > 0 Then
                If dtpLMPEstDate.CustomFormat <> " " Then
                    dtpLMPDueDate.Value = Convert.ToDateTime(dtpLMPEstDate.Value).AddDays(dblLMPCalculationDays)
                End If

                If dtpConEstDate.CustomFormat <> " " Then
                    dtpConDueDate.Value = Convert.ToDateTime(dtpConEstDate.Value).AddDays(dblConceptionCalculationDays)
                End If
               

                If (frmPatientVitals.strchkLMP = "1") Then
                    dtpLMPEstDate.Value = Convert.ToDateTime(frmPatientVitals.lmpDate)
                End If

                If dtpLMPEstDate.CustomFormat <> " " Then
                    dtpLMPDueDate.Value = Convert.ToDateTime(dtpLMPEstDate.Value.ToShortDateString()).AddDays(dblLMPCalculationDays)
                End If


                Dim _Week As Integer
                Dim _Days As Integer

                '16-Apr-13 Aniket: Code Refactoring
                CalculateAge(dtPreviousVitals.Rows(0)("dtOBVitalDate"), dtVitals.Value, _Week, _Days)

              


                'Recalculate the GA for new mode
                If (_Days + nGAPreviousDays) > 6 Then

                    cbGAWeeks.Text = _Week + nGAPreviousWeeks + 1

                    '10-Oct-2013 Aniket: Fixing bug where days were not calculated properly when a new OB vital record was created
                    cbGADays.Text = (_Days + nGAPreviousDays) - 7 '0

                Else

                    '05-Apr-13 Aniket: Resolved Bug  #48924: 
                    If (_Week + nGAPreviousWeeks) > 42 Then

                        cbGAWeeks.Text = ""
                        cbGADays.Text = ""

                    ElseIf (_Days + nGAPreviousDays + _Week + nGAPreviousWeeks) > 0 Then

                        cbGAWeeks.Text = _Week + nGAPreviousWeeks
                        cbGADays.Text = _Days + nGAPreviousDays

                    End If

                End If


                txtPreferredGesAge.Text = _Week.ToString() + " Weeks and " + _Days.ToString() + " Days"

            Else
                'Bug #92174: 00001056: OB Vitals 
                chkLMP.Checked = True
            End If

        End If

        'rdLMP.Checked = True
        'chkLMP.Checked = True
    End Sub

    Private Sub chkpain_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkpain.Click
        If chkpain.Checked = True Then
            trbPainLevel.Enabled = True
        Else
            trbPainLevel.Enabled = False
        End If
    End Sub

    Private Function ValidateDates() As Boolean
        


        If (Trim(cbGADays.Text) = "" Or Trim(cbGADays.Text) = "") Then
            MessageBox.Show("Enter the Gestational Age.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If

        If (txtBPSittingMax.Text <> "" And txtBPSittingMin.Text = "") Or (txtBPSittingMax.Text = "" And txtBPSittingMin.Text <> "") Then
            MessageBox.Show("Enter all details of BPSitting.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If


        If (frmPatientVitals.IsVitalEnabled) Then
            ''Function to Generate Warning for out of range Entries 
            Dim _strWarning As String = GetWarningString()
            If _strWarning <> "" Then
                If MessageBox.Show("Values entered are not within normal range," & Chr(13) & "Do you want to save the records?" & Chr(13) & Chr(13) & Chr(13) & _strWarning & Chr(13) & "YES - To Save current values " & Space(70) & Chr(13) & Chr(13) & " NO - To Modify values", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No Then
                    _FocusControl.Enabled = True
                    _FocusControl.Select()
                    Return False
                End If
            End If
        End If
        If dtpEstDueDate.Text = " " Then
            If MessageBox.Show("Estimated due date is not entered. Do you want to continue?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No Then
                dtpEstDueDate.Focus()
                Return False
            End If
        End If
        Return True

    End Function

    Private Function GetWarningString() As String
        ''To generate string of number of warnings for out of range entries.
        Try

            Dim StrWarning As String = ""
            Dim CountWarning As Int16 = 0
            Dim _FirstFocus As Boolean = False ''To focus TextBox of first Warning only
            Dim minValue As Double = 0
            Dim maxValue As Double = 0

            '' BP SYSTOLIC SITTING ''
            If txtBPSittingMax.Text <> "" Then
                objclsOBVitals.GetMinMaxValues(_AgeInMonths, strPatientGender, gloStream.Vitals.Supportings.VitalTypes.BPSystolic, minValue, maxValue)
                If minValue <> 0 And maxValue <> 0 Then
                    If minValue > Convert.ToDouble(txtBPSittingMax.Text) Or maxValue < Convert.ToDouble(txtBPSittingMax.Text) Then
                        CountWarning += 1
                        StrWarning = StrWarning & CountWarning & ") Systolic Sitting Blood Pressure must be in range (" & minValue & " - " & maxValue & ")" & Chr(13) & Chr(13)
                        If _FirstFocus = False Then
                            _FocusControl = txtBPSittingMax
                            _FirstFocus = True
                        End If
                    End If
                End If
            End If

            '' BP DIASTOLIC SITTING ''
            If txtBPSittingMin.Text <> "" Then
                objclsOBVitals.GetMinMaxValues(_AgeInMonths, strPatientGender, gloStream.Vitals.Supportings.VitalTypes.BPDiastolic, minValue, maxValue)
                If minValue <> 0 And maxValue <> 0 Then
                    If minValue > Convert.ToDouble(txtBPSittingMin.Text) Or maxValue < Convert.ToDouble(txtBPSittingMin.Text) Then
                        CountWarning += 1
                        StrWarning = StrWarning & CountWarning & ") Diastolic Sitting Blood Pressure must be in range (" & minValue & " - " & maxValue & ")" & Chr(13) & Chr(13)
                        If _FirstFocus = False Then
                            _FocusControl = txtBPSittingMin
                            _FirstFocus = True
                        End If
                    End If
                End If
            End If





            Return StrWarning
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""
        End Try
    End Function

    Private Function GetPreviousValuesFromHashTable(ByVal patID As Long) As DataTable

        Dim _dt As New DataTable()
        '  Dim _whichRadioChecked As String
        ' Dim _hash As Hashtable
        _dt = objclsOBVitals.GetPreviousOBVitals(patID)
        If Not IsNothing(_dt) Then
            If _dt.Rows.Count > 0 Then

                If Not IsDBNull(_dt.Rows(0)("dtConceptionEst")) Then
                    If _dt.Rows(0)("dtConceptionEst").ToString() <> "" Then
                        dtpConEstDate.Value = Format(_dt.Rows(0)("dtConceptionEst"), "MM/dd/yyyy")
                    Else
                        dtpConEstDate.Value = Format(System.DateTime.Now, "MM/dd/yyyy")
                    End If
                    If dtpConEstDate.Format <> DateTimePickerFormat.Short Then
                        dtpConEstDate.Format = DateTimePickerFormat.Short
                    End If
                    dtpConEstDate.CustomFormat = "MM/dd/yyyy"
                Else
                    'dtpConEstDate.Value = Format(System.DateTime.Now, "MM/dd/yyyy")
                    dtpConEstDate.Format = DateTimePickerFormat.Custom
                    dtpConEstDate.CustomFormat = " "
                End If


                If Not IsDBNull(_dt.Rows(0)("dtConceptionDue")) Then
                    If _dt.Rows(0)("dtConceptionDue").ToString() <> "" Then
                        dtpConDueDate.Value = Format(_dt.Rows(0)("dtConceptionDue"), "MM/dd/yyyy")
                    Else
                        dtpConDueDate.Value = Format(System.DateTime.Now, "MM/dd/yyyy")
                    End If
                    If dtpConDueDate.Format <> DateTimePickerFormat.Short Then
                        dtpConDueDate.Format = DateTimePickerFormat.Short
                    End If
                    dtpConDueDate.CustomFormat = "MM/dd/yyyy"
                Else
                    'dtpConDueDate.Value = Format(System.DateTime.Now, "MM/dd/yyyy")
                    dtpConDueDate.Format = DateTimePickerFormat.Custom
                    dtpConDueDate.CustomFormat = " "
                End If


                If Not IsDBNull(_dt.Rows(0)("dtLMPEst")) Then
                    If _dt.Rows(0)("dtLMPEst").ToString() <> "" Then
                        dtpLMPEstDate.Value = Format(_dt.Rows(0)("dtLMPEst"), "MM/dd/yyyy")
                    Else
                        dtpLMPEstDate.Value = Format(System.DateTime.Now, "MM/dd/yyyy")
                    End If
                    If dtpLMPEstDate.Format <> DateTimePickerFormat.Short Then
                        dtpLMPEstDate.Format = DateTimePickerFormat.Short
                    End If
                    dtpLMPEstDate.CustomFormat = "MM/dd/yyyy"
                Else
                    'dtpLMPEstDate.Value = Format(System.DateTime.Now, "MM/dd/yyyy")
                    dtpLMPEstDate.Format = DateTimePickerFormat.Custom
                    dtpLMPEstDate.CustomFormat = " "
                End If


                If Not IsDBNull(_dt.Rows(0)("dtLMPDue")) Then
                    If _dt.Rows(0)("dtLMPDue").ToString() <> "" Then
                        dtpLMPDueDate.Value = Format(_dt.Rows(0)("dtLMPDue"), "MM/dd/yyyy")
                    Else
                        dtpLMPDueDate.Value = Format(System.DateTime.Now, "MM/dd/yyyy")
                    End If
                    If dtpLMPDueDate.Format <> DateTimePickerFormat.Short Then
                        dtpLMPDueDate.Format = DateTimePickerFormat.Short
                    End If
                    dtpLMPDueDate.CustomFormat = "MM/dd/yyyy"
                Else
                    'dtpLMPDueDate.Value = Format(System.DateTime.Now, "MM/dd/yyyy")
                    dtpLMPDueDate.Format = DateTimePickerFormat.Custom
                    dtpLMPDueDate.CustomFormat = " "
                End If


                If Not IsDBNull(_dt.Rows(0)("dtUltraSoundDue")) Then
                    If _dt.Rows(0)("dtUltraSoundDue").ToString() <> "" Then
                        dtpUltraDueDate.Value = Format(_dt.Rows(0)("dtUltraSoundDue"), "MM/dd/yyyy")
                    Else
                        dtpUltraDueDate.Value = Format(System.DateTime.Now, "MM/dd/yyyy")
                    End If
                    If dtpUltraDueDate.Format <> DateTimePickerFormat.Short Then
                        dtpUltraDueDate.Format = DateTimePickerFormat.Short
                    End If
                    dtpUltraDueDate.CustomFormat = "MM/dd/yyyy"
                Else
                    'dtpUltraDueDate.Value = Format(System.DateTime.Now, "MM/dd/yyyy")
                    dtpUltraDueDate.Format = DateTimePickerFormat.Custom
                    dtpUltraDueDate.CustomFormat = " "
                End If

                '' Let value assigned & then update format for OBPregnancy check
                If Not IsDBNull(_dt.Rows(0)("dtEstimatedDueDate")) Then
                    If _dt.Rows(0)("dtEstimatedDueDate").ToString() <> "" Then
                        dtpEstDueDate.Value = Format(_dt.Rows(0)("dtEstimatedDueDate"), "MM/dd/yyyy")
                    Else
                        dtpEstDueDate.Value = Format(System.DateTime.Now, "MM/dd/yyyy")
                    End If
                    isEDDvaluechanged = False
                Else
                    dtpEstDueDate.Format = DateTimePickerFormat.Custom
                    dtpEstDueDate.CustomFormat = " "
                End If

                If Not IsDBNull(_dt.Rows(0)("nPrePregencyWeight")) Then
                    If _dt.Rows(0)("nPrePregencyWeight").ToString() <> "" Then
                        txtPrePregWeight.Text = Convert.ToString(_dt.Rows(0)("nPrePregencyWeight"))
                    Else
                        txtPrePregWeight.Text = ""
                    End If
                Else
                    txtPrePregWeight.Text = ""
                End If


                If Not IsDBNull(_dt.Rows(0)("nGAWeeks")) Then
                    nGAPreviousWeeks = _dt.Rows(0)("nGAWeeks")
                End If

                If Not IsDBNull(_dt.Rows(0)("nGADays")) Then
                    nGAPreviousDays = _dt.Rows(0)("nGADays")
                End If

                'Bug #92174: 00001056: OB Vitals 
                If Not IsDBNull(_dt.Rows(0)("bLastMenstrualPeriod")) Then
                    If _dt.Rows(0)("bLastMenstrualPeriod") = True Then
                        chkLMP.Checked = True
                    End If

                End If
                If Not IsDBNull(_dt.Rows(0)("bConception")) Then
                    If _dt.Rows(0)("bConception") = True Then
                        chkConception.Checked = True
                    End If

                End If
                If Not IsDBNull(_dt.Rows(0)("bUltraSound")) Then
                    If _dt.Rows(0)("bUltraSound") = True Then
                        chkUltraSound.Checked = True
                    End If

                End If
            End If
        End If


        Return _dt

    End Function

    Private Function Get_PatientDetails()
        Dim dtPatient As DataTable = Nothing
        Dim oPatient As New gloPatient.gloPatient(GetConnectionString)
        Try
            dtPatient = New DataTable
            dtPatient = GetPatientInfo(patID)
            If IsNothing(dtPatient) = False Then
                If dtPatient.Rows.Count > 0 Then
                    strPatientCode = Convert.ToString(dtPatient.Rows(0)("sPatientCode"))
                    strPatientFirstName = Convert.ToString(dtPatient.Rows(0)("sFirstName"))
                    strPatientLastName = Convert.ToString(dtPatient.Rows(0)("sLastName"))
                    strPatientDOB = Convert.ToString(dtPatient.Rows(0)("dtDOB"))
                    strPatientAge = GetAge(Convert.ToDateTime(dtPatient.Rows(0)("dtDOB")))
                    strPatientGender = Convert.ToString(dtPatient.Rows(0)("sGender"))
                    strPatientMaritalStatus = Convert.ToString(dtPatient.Rows(0)("sMaritalStatus"))

                End If
            End If
        Catch ex As Exception

        Finally
            If IsNothing(dtPatient) = False Then
                dtPatient.Dispose()
                dtPatient = Nothing
            End If
            If IsNothing(oPatient) = False Then
                oPatient.Dispose()
                oPatient = Nothing
            End If

        End Try
        Return Nothing
    End Function

    Public Sub ChangeColorofInvalidValues(ByVal sender As Object)
        Try

            If (frmPatientVitals.IsVitalEnabled) Then
                Dim txt As TextBox = TryCast(sender, TextBox)
                Dim textString As String = txt.Name
                Select Case txt.Name

                    Case "txtBPSittingMin"
                        If txtBPSittingMin.Text.Trim() <> "" Then
                            lblbpsidia.Text = ValidateVitalsAge(textString)
                        Else

                            lblbpsidia.Text = ""
                            txtBPSittingMin.BackColor = Color.White
                            txtBPSittingMin.ForeColor = Color.Black
                            txtBPSittingMin.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular)))
                            'New Font(Me.Font, FontStyle.Regular)
                        End If

                    Case "txtBPSittingMax"
                        If txtBPSittingMax.Text.Trim() <> "" Then
                            lblbpsisys.Text = ValidateVitalsAge(textString)
                        Else
                            txtBPSittingMax.ForeColor = Color.Black
                            txtBPSittingMax.BackColor = Color.White
                            lblbpsisys.Text = ""
                            txtBPSittingMax.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular)

                        End If

                End Select
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function ValidateVitalsAge(ByVal txtName As String) As String

        Dim myRespristoryRate As String = ""
        minTempValue = 0
        maxTempValue = 0
        Select Case txtName



            Case "txtBPSittingMin"
                If Not IsNothing(objclsOBVitals) Then
                    objclsOBVitals.GetMinMaxValues(_AgeInMonths, strPatientGender, gloStream.Vitals.Supportings.VitalTypes.BPDiastolic, minTempValue, maxTempValue)
                    If (Convert.ToInt32(txtBPSittingMin.Text.Trim()) < minTempValue) Or (Convert.ToInt32(txtBPSittingMin.Text.Trim()) > maxTempValue) Then
                        txtBPSittingMin.ForeColor = Color.Red
                        txtBPSittingMin.BackColor = Color.Red
                        txtBPSittingMin.ForeColor = Color.White
                        myRespristoryRate = "(" & minTempValue & "-" & maxTempValue & ")"
                        lblbpsidia.ForeColor = Color.Red
                        txtBPSittingMin.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold) 'IIf(Font.Height = 8.25F, gFont_SMALL_BOLD, IIf(Font.Height = 9.0F, gFont_BOLD, New Font(Font, FontStyle.Bold))) 'New Font(Me.Font, FontStyle.Bold)
                    Else
                        lblbpsidia.Text = ""
                        txtBPSittingMin.BackColor = Color.White
                        txtBPSittingMin.ForeColor = Color.Black
                        txtBPSittingMin.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular)
                    End If
                End If

            Case "txtBPSittingMax"
                If Not IsNothing(objclsOBVitals) Then
                    objclsOBVitals.GetMinMaxValues(_AgeInMonths, strPatientGender, gloStream.Vitals.Supportings.VitalTypes.BPSystolic, minTempValue, maxTempValue)
                    If (Convert.ToInt32(txtBPSittingMax.Text.Trim()) < minTempValue) Or (Convert.ToInt32(txtBPSittingMax.Text.Trim()) > maxTempValue) Then
                        txtBPSittingMax.ForeColor = Color.Red
                        myRespristoryRate = "(" & minTempValue & "-" & maxTempValue & ")"
                        lblbpsisys.ForeColor = Color.Red
                        txtBPSittingMax.BackColor = Color.Red
                        txtBPSittingMax.ForeColor = Color.White
                        txtBPSittingMax.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold) 'IIf(Font.Height = 8.25F, gFont_SMALL_BOLD, IIf(Font.Height = 9.0F, gFont_BOLD, New Font(Font, FontStyle.Bold))) 'New Font(Me.Font, FontStyle.Bold) 'New Font(Me.Font, FontStyle.Bold)

                    Else
                        txtBPSittingMax.ForeColor = Color.Black
                        txtBPSittingMax.BackColor = Color.White
                        lblbpsisys.Text = ""
                        txtBPSittingMax.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular)

                    End If
                End If



        End Select
        Return myRespristoryRate
    End Function

    Private Function AssignVitalTask() As Int64
        Dim ofrmTask As gloTaskMail.frmTask
        Try
            ofrmTask = New gloTaskMail.frmTask(GetConnectionString, 0)
            ofrmTask._TaskType = gloTaskMail.TaskType.Vitals
            ofrmTask.Text = "Tasks"
            ofrmTask.PatientID = patID
            ofrmTask.ShowDialog(IIf(IsNothing(ofrmTask.Parent), Me, ofrmTask.Parent))
            ObTaskID = ofrmTask.TaskGroupID
            ofrmTask.Dispose()
            ofrmTask = Nothing
        Catch ex As Exception

        End Try
        Return Nothing
    End Function

    Private Sub tblbtn_Assign_Task_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Assign_Task.Click
        AssignVitalTask()
    End Sub

    Private Sub txtPreferredGesAge_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPreferredGesAge.KeyPress
        blnGAManuallyEdited = True
    End Sub

    '16-Aug-13 Aniket: Change done because of Phill Mail 'OB Vitals' dated 08/Aug/13
    Private Sub dtVitals_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtVitals.LostFocus
        '28-Mar-13: Aniket: Make GA Editable
        If blnLoad = True Then
            If chkLMP.Checked = True Then
                Dim _Week As Integer
                Dim _Days As Integer
                '16-Apr-13 Aniket: Code Refactoring
                CalculateAge(dtpLMPEstDate.Value, dtVitals.Value, _Week, _Days)
                CalculateGA(_Week, _Days)
            End If
        End If
    End Sub

    Private Sub dtpEstDueDate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpEstDueDate.LostFocus
        If blnLoad = True Then
            CalulateGAOnEDD()
        End If
    End Sub

    Private Sub dtpConDueDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpConDueDate.ValueChanged
        If dtpConDueDate.Format <> DateTimePickerFormat.Short Then
            dtpConDueDate.Format = DateTimePickerFormat.Short
        End If
        dtpConDueDate.CustomFormat = "MM/dd/yyyy"
    End Sub

    Private Sub dtpLMPEstDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpLMPEstDate.ValueChanged

        'Debug.WriteLine("dtpLMPEstDate_ValueChanged")

        If dtpLMPEstDate.Format <> DateTimePickerFormat.Short Then
            dtpLMPEstDate.Format = DateTimePickerFormat.Short
        End If
        dtpLMPEstDate.CustomFormat = "MM/dd/yyyy"

        '28-Mar-13: Aniket: Make GA Editable
        'If blnLoad = True Then

        '08-Jun-16 Aniket: Resolving Bug #96806: gloEMR : OB Vitals : Application takes default date and calculate estimated due date
        'If dtpLMPEstDate.Text <> " " Then
        dtpLMPDueDate.Value = Convert.ToDateTime(dtpLMPEstDate.Value.ToShortDateString()).AddDays(dblLMPCalculationDays)

        'Dim _Week As Integer
        'Dim _Days As Integer

        '16-Apr-13 Aniket: Code Refactoring
        'CalculateAge(dtpLMPEstDate.Value, dtVitals.Value, _Week, _Days)


        'CalculateGA(_Week, _Days)

        If chkLMP.Checked Then
            'blnLoad = False
            dtpEstDueDate.Value = dtpLMPDueDate.Value
            'blnLoad = True
            'Else
            'If chkUltraSound.Checked = False And chkConception.Checked = False Then
            'cbGAWeeks.Text = ""
            'cbGADays.Text = ""
            'End If
            'End If
            'End If
        End If

    End Sub

    Private Sub dtpLMPDueDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpLMPDueDate.ValueChanged
        If dtpLMPDueDate.Format <> DateTimePickerFormat.Short Then
            dtpLMPDueDate.Format = DateTimePickerFormat.Short
        End If
        dtpLMPDueDate.CustomFormat = "MM/dd/yyyy"
    End Sub

    Private Sub dtpUltraDueDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpUltraDueDate.ValueChanged
        If dtpUltraDueDate.Format <> DateTimePickerFormat.Short Then
            dtpUltraDueDate.Format = DateTimePickerFormat.Short
        End If
        dtpUltraDueDate.CustomFormat = "MM/dd/yyyy"

        If chkUltraSound.Checked Then
            dtpEstDueDate.Value = dtpUltraDueDate.Value
        End If
        CheckSpecialRule()


    End Sub

    Private Sub dtpEstDueDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpEstDueDate.ValueChanged

        If dtpEstDueDate.Format <> DateTimePickerFormat.Short Then
            dtpEstDueDate.Format = DateTimePickerFormat.Short
        End If
        dtpEstDueDate.CustomFormat = "MM/dd/yyyy"

        If blnLoad = True Then
            CalulateGAOnEDD()
        End If
        isEDDvaluechanged = True
    End Sub

    Private Sub dtVitals_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtVitals.ValueChanged
        Try
            If blnLoad = True Then
                If _IsEditMode = False Then
                    IsNewPregCase = IsNewOBPregCase()
                    If Not IsNewPregCase Then
                        Dateformattoblank()
                        cbGAWeeks.Text = ""
                        cbGADays.Text = ""
                    Else
                        If (_HashTable.Count > 0) Then
                            GetValuesFromHashTable(_HashTable)
                        Else
                            ' if hashtable has no value then Get previous Value 
                            GetPreviousValuesFromHashTableOBCase(patID)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error in OB Vital." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dtpConEstDate_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtpConEstDate.KeyPress
        If dtpConEstDate.Format <> DateTimePickerFormat.Short Then
            dtpConEstDate.Format = DateTimePickerFormat.Short
        End If
        dtpConEstDate.CustomFormat = "MM/dd/yyyy"
    End Sub

    Private Sub dtpLMPEstDate_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtpLMPEstDate.KeyPress

        'Debug.WriteLine("dtpLMPEstDate_KeyPress")

        If dtpLMPEstDate.Format <> DateTimePickerFormat.Short Then
            dtpLMPEstDate.Format = DateTimePickerFormat.Short
        End If
        dtpLMPEstDate.CustomFormat = "MM/dd/yyyy"
    End Sub

    Private Sub dtpUltraDueDate_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtpUltraDueDate.KeyPress
        If dtpUltraDueDate.Format <> DateTimePickerFormat.Short Then
            dtpUltraDueDate.Format = DateTimePickerFormat.Short
        End If
        dtpUltraDueDate.CustomFormat = "MM/dd/yyyy"
    End Sub

    Private Sub CheckSpecialRule()
        If chkUltraSound.Checked Then
            If dtpLMPDueDate.Text <> " " Then
                Dim ts As TimeSpan = dtpUltraDueDate.Value.Date - dtpLMPDueDate.Value.Date
                'Day Diff Consider as 7 day as per OB Cases 20141030 PRD
                If dtpUltraDueDate.Value.CompareTo(dtpLMPDueDate.Value) < 0 Then
                    If ts.Days >= -7 Then
                        chkLMP.Checked = True
                    End If
                ElseIf dtpUltraDueDate.Value.CompareTo(dtpLMPDueDate.Value) = 0 Then
                    chkLMP.Checked = True
                ElseIf dtpUltraDueDate.Value.CompareTo(dtpLMPDueDate.Value) > 0 Then
                    If ts.Days <= 7 Then
                        chkLMP.Checked = True
                    End If
                End If
                If chkLMP.Checked Then
                    MessageBox.Show("If the ultrasounds suggested due date is within 7 days (before or after) of LMP due date, then LMP due date will be considered. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                ts = Nothing
            End If
        End If
    End Sub

    Public Function CalulateGAOnEDD()
        Dim begingingDate As DateTime
        If dtpEstDueDate.Text <> " " Then
            begingingDate = Convert.ToDateTime(dtpEstDueDate.Value.ToShortDateString()).AddDays(dblLMPCalculationDays * -1)
            Dim _Week As Integer
            Dim _Days As Integer
            CalculateAge(begingingDate, dtVitals.Value, _Week, _Days)
            CalculateGA(_Week, _Days, 1)
        Else
            cbGAWeeks.Text = ""
            cbGADays.Text = ""
        End If
        begingingDate = Nothing
        Return Nothing
    End Function

    Public Function IsNewOBPregCase() As Boolean
        Dim IsNewPreg As Boolean = False
        Dim dsData As New DataSet
        Dim dtObPregCase As DataTable = Nothing
        Dim dtRecentOb As DataTable = Nothing

        Try
            If Not IsNothing(objclsOBVitals) Then
                
                dtObPregCase = objclsOBVitals.GetNewPregnancyCase(patID, dtVitals.Value)
                If Not IsNothing(dtObPregCase) Then
                    If dtObPregCase.Rows.Count > 0 Then
                        Return Convert.ToBoolean(dtObPregCase.Rows(0)(0))
                    End If
                End If

            End If
            Return IsNewPreg
        Catch ex As Exception
            MessageBox.Show("Error in OB Vital." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return IsNewPreg
        Finally
            If IsNothing(dsData) = False Then
                dsData.Dispose()
                dsData = Nothing
            End If
            
        End Try
    End Function

    Private Sub Dateformattoblank()
        Try
            If dtpConEstDate.CustomFormat = "MM/dd/yyyy" Then
                dtpConEstDate.Format = DateTimePickerFormat.Custom
                dtpConEstDate.CustomFormat = " "
            End If

            If dtpConDueDate.CustomFormat = "MM/dd/yyyy" Then
                dtpConDueDate.Format = DateTimePickerFormat.Custom
                dtpConDueDate.CustomFormat = " "
            End If

            If dtpLMPEstDate.CustomFormat = "MM/dd/yyyy" Then
                dtpLMPEstDate.Format = DateTimePickerFormat.Custom
                dtpLMPEstDate.CustomFormat = " "
            End If

            If dtpLMPDueDate.CustomFormat = "MM/dd/yyyy" Then
                dtpLMPDueDate.Format = DateTimePickerFormat.Custom
                dtpLMPDueDate.CustomFormat = " "
            End If

            If dtpUltraDueDate.CustomFormat = "MM/dd/yyyy" Then
                dtpUltraDueDate.Format = DateTimePickerFormat.Custom
                dtpUltraDueDate.CustomFormat = " "
            End If


            If dtpEstDueDate.CustomFormat = "MM/dd/yyyy" Then
                dtpEstDueDate.Format = DateTimePickerFormat.Custom
                dtpEstDueDate.CustomFormat = " "
            End If

            '
            cbGAWeeks.Text = ""
            cbGADays.Text = ""

        Catch ex As Exception
            MessageBox.Show("Error in OB Vital." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Kept Seperate as in future might more condition needed
    Private Sub GetPreviousValuesFromHashTableOBCase(ByVal patID As Long)
        Dim _dt As New DataTable()
        Try
            _dt = objclsOBVitals.GetPreviousOBVitals(patID)
            If Not IsNothing(_dt) Then
                If _dt.Rows.Count > 0 Then
                    If Not IsDBNull(_dt.Rows(0)("dtConceptionEst")) Then
                        If _dt.Rows(0)("dtConceptionEst").ToString() <> "" Then
                            dtpConEstDate.Value = Format(_dt.Rows(0)("dtConceptionEst"), "MM/dd/yyyy")
                        Else
                            dtpConEstDate.Value = Format(System.DateTime.Now, "MM/dd/yyyy")
                        End If
                        If dtpConEstDate.Format <> DateTimePickerFormat.Short Then
                            dtpConEstDate.Format = DateTimePickerFormat.Short
                        End If
                        dtpConEstDate.CustomFormat = "MM/dd/yyyy"
                    Else
                        dtpConEstDate.Format = DateTimePickerFormat.Custom
                        dtpConEstDate.CustomFormat = " "
                    End If
                    If Not IsDBNull(_dt.Rows(0)("dtConceptionDue")) Then
                        If _dt.Rows(0)("dtConceptionDue").ToString() <> "" Then
                            dtpConDueDate.Value = Format(_dt.Rows(0)("dtConceptionDue"), "MM/dd/yyyy")
                        Else
                            dtpConDueDate.Value = Format(System.DateTime.Now, "MM/dd/yyyy")
                        End If
                        If dtpConDueDate.Format <> DateTimePickerFormat.Short Then
                            dtpConDueDate.Format = DateTimePickerFormat.Short
                        End If
                        dtpConDueDate.CustomFormat = "MM/dd/yyyy"
                    Else
                        dtpConDueDate.Format = DateTimePickerFormat.Custom
                        dtpConDueDate.CustomFormat = " "
                    End If
                    If Not IsDBNull(_dt.Rows(0)("dtLMPEst")) Then
                        If _dt.Rows(0)("dtLMPEst").ToString() <> "" Then
                            dtpLMPEstDate.Value = Format(_dt.Rows(0)("dtLMPEst"), "MM/dd/yyyy")
                        Else
                            dtpLMPEstDate.Value = Format(System.DateTime.Now, "MM/dd/yyyy")
                        End If
                        If dtpLMPEstDate.Format <> DateTimePickerFormat.Short Then
                            dtpLMPEstDate.Format = DateTimePickerFormat.Short
                        End If
                        dtpLMPEstDate.CustomFormat = "MM/dd/yyyy"
                    Else
                        dtpLMPEstDate.Format = DateTimePickerFormat.Custom
                        dtpLMPEstDate.CustomFormat = " "
                    End If


                    If Not IsDBNull(_dt.Rows(0)("dtLMPDue")) Then
                        If _dt.Rows(0)("dtLMPDue").ToString() <> "" Then
                            dtpLMPDueDate.Value = Format(_dt.Rows(0)("dtLMPDue"), "MM/dd/yyyy")
                        Else
                            dtpLMPDueDate.Value = Format(System.DateTime.Now, "MM/dd/yyyy")
                        End If
                        If dtpLMPDueDate.Format <> DateTimePickerFormat.Short Then
                            dtpLMPDueDate.Format = DateTimePickerFormat.Short
                        End If
                        dtpLMPDueDate.CustomFormat = "MM/dd/yyyy"
                    Else
                        dtpLMPDueDate.Format = DateTimePickerFormat.Custom
                        dtpLMPDueDate.CustomFormat = " "
                    End If

                    If Not IsDBNull(_dt.Rows(0)("dtUltraSoundDue")) Then
                        If _dt.Rows(0)("dtUltraSoundDue").ToString() <> "" Then
                            dtpUltraDueDate.Value = Format(_dt.Rows(0)("dtUltraSoundDue"), "MM/dd/yyyy")
                        Else
                            dtpUltraDueDate.Value = Format(System.DateTime.Now, "MM/dd/yyyy")
                        End If
                        If dtpUltraDueDate.Format <> DateTimePickerFormat.Short Then
                            dtpUltraDueDate.Format = DateTimePickerFormat.Short
                        End If
                        dtpUltraDueDate.CustomFormat = "MM/dd/yyyy"
                    Else
                        dtpUltraDueDate.Format = DateTimePickerFormat.Custom
                        dtpUltraDueDate.CustomFormat = " "
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error in OB Vital." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If IsNothing(_dt) = False Then
                _dt.Dispose()
                _dt = Nothing
            End If
        End Try
    End Sub

    Private Function isRecentOBDataAvilable(ByVal _dt As DataTable) As Boolean

        Try
            If Not IsNothing(_dt) Then
                If _dt.Rows.Count > 0 Then
                    If Not IsDBNull(_dt.Rows(0)("dtConceptionEst")) Then
                        If _dt.Rows(0)("dtConceptionEst").ToString() <> "" Then
                            dtpConEstDate.Value = Format(_dt.Rows(0)("dtConceptionEst"), "MM/dd/yyyy")
                        Else
                            dtpConEstDate.Value = Format(System.DateTime.Now, "MM/dd/yyyy")
                        End If
                        If dtpConEstDate.Format <> DateTimePickerFormat.Short Then
                            dtpConEstDate.Format = DateTimePickerFormat.Short
                        End If
                        dtpConEstDate.CustomFormat = "MM/dd/yyyy"
                    Else
                        dtpConEstDate.Format = DateTimePickerFormat.Custom
                        dtpConEstDate.CustomFormat = " "
                    End If
                    If Not IsDBNull(_dt.Rows(0)("dtConceptionDue")) Then
                        If _dt.Rows(0)("dtConceptionDue").ToString() <> "" Then
                            dtpConDueDate.Value = Format(_dt.Rows(0)("dtConceptionDue"), "MM/dd/yyyy")
                        Else
                            dtpConDueDate.Value = Format(System.DateTime.Now, "MM/dd/yyyy")
                        End If
                        If dtpConDueDate.Format <> DateTimePickerFormat.Short Then
                            dtpConDueDate.Format = DateTimePickerFormat.Short
                        End If
                        dtpConDueDate.CustomFormat = "MM/dd/yyyy"
                    Else
                        dtpConDueDate.Format = DateTimePickerFormat.Custom
                        dtpConDueDate.CustomFormat = " "
                    End If
                    If Not IsDBNull(_dt.Rows(0)("dtLMPEst")) Then
                        If _dt.Rows(0)("dtLMPEst").ToString() <> "" Then
                            dtpLMPEstDate.Value = Format(_dt.Rows(0)("dtLMPEst"), "MM/dd/yyyy")
                        Else
                            dtpLMPEstDate.Value = Format(System.DateTime.Now, "MM/dd/yyyy")
                        End If
                        If dtpLMPEstDate.Format <> DateTimePickerFormat.Short Then
                            dtpLMPEstDate.Format = DateTimePickerFormat.Short
                        End If
                        dtpLMPEstDate.CustomFormat = "MM/dd/yyyy"
                    Else
                        dtpLMPEstDate.Format = DateTimePickerFormat.Custom
                        dtpLMPEstDate.CustomFormat = " "
                    End If


                    If Not IsDBNull(_dt.Rows(0)("dtLMPDue")) Then
                        If _dt.Rows(0)("dtLMPDue").ToString() <> "" Then
                            dtpLMPDueDate.Value = Format(_dt.Rows(0)("dtLMPDue"), "MM/dd/yyyy")
                        Else
                            dtpLMPDueDate.Value = Format(System.DateTime.Now, "MM/dd/yyyy")
                        End If
                        If dtpLMPDueDate.Format <> DateTimePickerFormat.Short Then
                            dtpLMPDueDate.Format = DateTimePickerFormat.Short
                        End If
                        dtpLMPDueDate.CustomFormat = "MM/dd/yyyy"
                    Else
                        dtpLMPDueDate.Format = DateTimePickerFormat.Custom
                        dtpLMPDueDate.CustomFormat = " "
                    End If

                    If Not IsDBNull(_dt.Rows(0)("dtUltraSoundDue")) Then
                        If _dt.Rows(0)("dtUltraSoundDue").ToString() <> "" Then
                            dtpUltraDueDate.Value = Format(_dt.Rows(0)("dtUltraSoundDue"), "MM/dd/yyyy")
                        Else
                            dtpUltraDueDate.Value = Format(System.DateTime.Now, "MM/dd/yyyy")
                        End If
                        If dtpUltraDueDate.Format <> DateTimePickerFormat.Short Then
                            dtpUltraDueDate.Format = DateTimePickerFormat.Short
                        End If
                        dtpUltraDueDate.CustomFormat = "MM/dd/yyyy"
                    Else
                        dtpUltraDueDate.Format = DateTimePickerFormat.Custom
                        dtpUltraDueDate.CustomFormat = " "
                    End If
                    Return False
                Else
                    Return True
                End If
            Else
                Return True
            End If
        Catch ex As Exception
            MessageBox.Show("Error in OB Vital." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If IsNothing(_dt) = False Then
                _dt.Dispose()
                _dt = Nothing
            End If
        End Try
    End Function

    Private Sub chkConception_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkConception.CheckedChanged

        If chkConception.Checked Then
            chkLMP.Checked = False
            chkUltraSound.Checked = False
            If dtpConDueDate.Text <> " " Then

                If dtpEstDueDate.Format <> DateTimePickerFormat.Short Then
                    dtpEstDueDate.Format = DateTimePickerFormat.Short
                End If

                dtpEstDueDate.CustomFormat = "MM/dd/yyyy"
                dtpEstDueDate.Value = dtpConDueDate.Value

                If dtpEstDueDate.Format <> DateTimePickerFormat.Short Then
                    dtpEstDueDate.Format = DateTimePickerFormat.Short
                End If

                dtpEstDueDate.CustomFormat = "MM/dd/yyyy"

                If blnLoad = True Then
                    CalulateGAOnEDD()
                End If

                isEDDvaluechanged = False
            Else
                dtpEstDueDate.Format = DateTimePickerFormat.Custom
                dtpEstDueDate.CustomFormat = " "
                cbGAWeeks.Text = ""
                cbGADays.Text = ""
            End If
        End If

    End Sub

    Private Sub chkLMP_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkLMP.CheckedChanged

        If chkLMP.Checked Then
            chkConception.Checked = False
            chkUltraSound.Checked = False

            If dtpLMPDueDate.Text <> " " Then

                If dtpEstDueDate.Format <> DateTimePickerFormat.Short Then
                    dtpEstDueDate.Format = DateTimePickerFormat.Short
                End If

                dtpEstDueDate.CustomFormat = "MM/dd/yyyy"
                dtpEstDueDate.Value = dtpLMPDueDate.Value

                If dtpEstDueDate.Format <> DateTimePickerFormat.Short Then
                    dtpEstDueDate.Format = DateTimePickerFormat.Short
                End If

                dtpEstDueDate.CustomFormat = "MM/dd/yyyy"

                If blnLoad = True Then
                    CalulateGAOnEDD()
                End If

                isEDDvaluechanged = False

            Else
                dtpEstDueDate.Format = DateTimePickerFormat.Custom
                dtpEstDueDate.CustomFormat = " "
                cbGAWeeks.Text = ""
                cbGADays.Text = ""
            End If

        End If

    End Sub

    Private Sub chkUltraSound_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkUltraSound.CheckedChanged

        If chkUltraSound.Checked Then
            chkConception.Checked = False
            chkLMP.Checked = False

            If dtpUltraDueDate.Text <> " " Then
                If dtpEstDueDate.Format <> DateTimePickerFormat.Short Then
                    dtpEstDueDate.Format = DateTimePickerFormat.Short
                End If
                dtpEstDueDate.CustomFormat = "MM/dd/yyyy"
                dtpEstDueDate.Value = dtpUltraDueDate.Value

                If dtpEstDueDate.Format <> DateTimePickerFormat.Short Then
                    dtpEstDueDate.Format = DateTimePickerFormat.Short
                End If
                dtpEstDueDate.CustomFormat = "MM/dd/yyyy"
                If blnLoad = True Then
                    CalulateGAOnEDD()
                End If
                isEDDvaluechanged = False

                CheckSpecialRule()
            Else
                dtpEstDueDate.Format = DateTimePickerFormat.Custom
                dtpEstDueDate.CustomFormat = " "

                '14-Jun-16 Aniket: Resolving Bug #96928: gloEMR : OB Vitals : Application showing gestational age which is not acceptable.
                If blnReCalculateGA = True Then
                    cbGAWeeks.Text = ""
                    cbGADays.Text = ""
                End If

            End If
        End If
    End Sub

    Private Sub btnDelConception_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelConception.Click
        If dtpConDueDate.Text <> "" Then
            dtpConEstDate.Format = DateTimePickerFormat.Custom
            dtpConEstDate.CustomFormat = " "

            dtpConDueDate.Format = DateTimePickerFormat.Custom
            dtpConDueDate.CustomFormat = " "

            If chkConception.Checked Then
                dtpEstDueDate.Format = DateTimePickerFormat.Custom
                dtpEstDueDate.CustomFormat = " "
                cbGADays.Text = ""
                cbGAWeeks.Text = ""
            End If
            isDeleteClicked = True
        End If
    End Sub

    Private Sub btnDelLMPEstDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelLMPEstDate.Click
        If dtpLMPDueDate.Text <> " " Then
            dtpLMPDueDate.Format = DateTimePickerFormat.Custom
            dtpLMPDueDate.CustomFormat = " "

            dtpLMPEstDate.Format = DateTimePickerFormat.Custom
            dtpLMPEstDate.CustomFormat = " "

            If chkLMP.Checked Then
                dtpEstDueDate.Format = DateTimePickerFormat.Custom
                dtpEstDueDate.CustomFormat = " "
                cbGADays.Text = ""
                cbGAWeeks.Text = ""
            End If
            isDeleteClicked = True
        End If
    End Sub

    Private Sub btnDelUltraDueDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelUltraDueDate.Click
        If dtpUltraDueDate.Text <> " " Then
            dtpUltraDueDate.Format = DateTimePickerFormat.Custom
            dtpUltraDueDate.CustomFormat = " "

            If chkUltraSound.Checked Then
                dtpEstDueDate.Format = DateTimePickerFormat.Custom
                dtpEstDueDate.CustomFormat = " "
                cbGADays.Text = ""
                cbGAWeeks.Text = ""
            End If
            isDeleteClicked = True
        End If
    End Sub

    Private Sub btnDelEstDueDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelEstDueDate.Click
        If dtpEstDueDate.Text <> " " Then

            dtpEstDueDate.Format = DateTimePickerFormat.Custom
            dtpEstDueDate.CustomFormat = " "

            cbGADays.Text = ""
            cbGAWeeks.Text = ""
            isDeleteClicked = True
        End If

    End Sub

    Public Property IsDroppedDown() As Boolean
        Get
            IsDroppedDown = _isDroppedDown
        End Get
        Set(ByVal value As Boolean)
            _isDroppedDown = value
        End Set
    End Property

    Private Sub dtpConEstDate_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpConEstDate.CloseUp
        If isDeleteClicked And dtpConEstDate.Format = DateTimePickerFormat.Custom Then

            If dtpConEstDate.Format <> DateTimePickerFormat.Short Then
                dtpConEstDate.Format = DateTimePickerFormat.Short
            End If
            dtpConEstDate.CustomFormat = "MM/dd/yyyy"

            If dtpConDueDate.Format <> DateTimePickerFormat.Short Then
                dtpConDueDate.Format = DateTimePickerFormat.Short
            End If
            dtpConDueDate.CustomFormat = "MM/dd/yyyy"

            If dtpEstDueDate.Format <> DateTimePickerFormat.Short Then
                dtpEstDueDate.Format = DateTimePickerFormat.Short
            End If
            dtpEstDueDate.CustomFormat = "MM/dd/yyyy"


            If chkConception.Checked Then
                dtpEstDueDate.Value = dtpConDueDate.Value
            End If
            isDeleteClicked = False
        End If
    End Sub

    Private Sub dtpLMPEstDate_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpLMPEstDate.CloseUp

        'Debug.WriteLine("dtpLMPEstDate_CloseUp")

        If isDeleteClicked And dtpLMPEstDate.Format = DateTimePickerFormat.Custom Then

            If dtpLMPEstDate.Format <> DateTimePickerFormat.Short Then
                dtpLMPEstDate.Format = DateTimePickerFormat.Short
            End If
            dtpLMPEstDate.CustomFormat = "MM/dd/yyyy"

            If dtpLMPDueDate.Format <> DateTimePickerFormat.Short Then
                dtpLMPDueDate.Format = DateTimePickerFormat.Short
            End If
            dtpLMPDueDate.CustomFormat = "MM/dd/yyyy"

            If dtpEstDueDate.Format <> DateTimePickerFormat.Short Then
                dtpEstDueDate.Format = DateTimePickerFormat.Short
            End If
            dtpEstDueDate.CustomFormat = "MM/dd/yyyy"


            If chkLMP.Checked Then
                dtpEstDueDate.Value = dtpLMPDueDate.Value
            End If
            isDeleteClicked = False
        End If
    End Sub

    Private Sub dtpUltraDueDate_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpUltraDueDate.CloseUp
        If isDeleteClicked And dtpUltraDueDate.Format = DateTimePickerFormat.Custom Then
            If dtpUltraDueDate.Format <> DateTimePickerFormat.Short Then
                dtpUltraDueDate.Format = DateTimePickerFormat.Short
            End If
            dtpUltraDueDate.CustomFormat = "MM/dd/yyyy"
            If chkUltraSound.Checked Then
                CheckSpecialRule()
                dtpEstDueDate.Value = dtpUltraDueDate.Value
            End If
            isDeleteClicked = False
        End If
    End Sub

    Private Sub dtpEstDueDate_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpEstDueDate.CloseUp
        If isDeleteClicked And dtpEstDueDate.Format = DateTimePickerFormat.Custom Then
            If dtpEstDueDate.Format <> DateTimePickerFormat.Short Then
                dtpEstDueDate.Format = DateTimePickerFormat.Short
            End If
            dtpEstDueDate.CustomFormat = "MM/dd/yyyy"
            If blnLoad = True Then
                CalulateGAOnEDD()
            End If
            isDeleteClicked = False
        End If
    End Sub
    'Added for OB Vital Comments
    Private Sub btnInternalBr_Click(sender As System.Object, e As System.EventArgs) Handles btnInternalBr.Click
        strComments = "OB Vital Comments"
        BrowseComments()
    End Sub

    Private Sub BrowseComments()
        Try
            Me.Cursor = Cursors.WaitCursor
            If oListControl IsNot Nothing Then
                Dim i As Integer = Me.Controls.Count - 1
                While i >= 0
                    If Me.Controls(i).Name = oListControl.Name Then
                        Me.Controls.Remove(Me.Controls(i))
                        Exit While
                    End If
                    i += -1
                End While
                Try
                    RemoveHandler oListControl.ItemSelectedClick, AddressOf oListControl_SelectedClick
                    RemoveHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
                Catch
                End Try
                oListControl.Dispose()
                oListControl = Nothing
            End If


            oListControl = New gloListControl.gloListControl(GetConnectionString, gloListControl.gloListControlType.OBVitalComments, True, Me.Width, strComments)
            oListControl.ControlHeader = "OB Vital Comments"
            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_SelectedClick
            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick

            If dtMasterData IsNot Nothing AndAlso dtMasterData.Rows.Count > 0 Then
                For i As Integer = 0 To dtMasterData.Rows.Count - 1
                    oListControl.SelectedItems.Add(Convert.ToInt64(dtMasterData.Rows(i)("ID")), Convert.ToString(dtMasterData.Rows(i)("Comments")), "")
                Next
            End If
           
            Me.Controls.Add(oListControl)
            oListControl.OpenControl()
            pnlMain.Visible = False
            If oListControl.IsDisposed = False Then
                oListControl.Dock = DockStyle.Fill
                oListControl.BringToFront()
            End If
            Me.Cursor = Cursors.[Default]
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub

    Private Sub oListControl_ItemClosedClick(sender As Object, e As EventArgs)
        Try
            If oListControl IsNot Nothing Then
                Dim i As Integer = Me.Controls.Count - 1
                While i >= 0
                    If Me.Controls(i).Name = oListControl.Name Then
                        Me.Controls.Remove(Me.Controls(i))
                        Exit While
                    End If
                    i += -1
                End While
                Try
                    RemoveHandler oListControl.ItemSelectedClick, AddressOf oListControl_SelectedClick
                    RemoveHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
                Catch
                End Try
            End If
            pnlMain.Visible = True
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub

    Private Sub oListControl_SelectedClick(sender As Object, e As EventArgs)

        Try
            Dim dtMasterData As New DataTable
            Dim dcId As New DataColumn
            Dim dcNotes As New DataColumn
            dcId = New DataColumn("ID")
            dcNotes = New DataColumn("Comments")
            dtMasterData.Columns.Add(dcId)
            dtMasterData.Columns.Add(dcNotes)
            dtMasterData.PrimaryKey = New DataColumn() {dtMasterData.Columns("ID")}

            Dim dtAddData As New DataTable()
            Dim dcId1 As New DataColumn("ID")
            Dim dcNotes1 As New DataColumn("Comments")
            dtAddData.Columns.Add(dcId1)
            dtAddData.Columns.Add(dcNotes1)

            If oListControl.SelectedItems.Count > 0 Then

                For i As Int16 = 0 To oListControl.SelectedItems.Count - 1
                    Dim key As Object = oListControl.SelectedItems(i).ID
                    
                    If dtMasterData.Rows.Find(key) Is Nothing Then
                        Dim drTemp As DataRow = dtMasterData.NewRow()
                        drTemp("ID") = oListControl.SelectedItems(i).ID
                        drTemp("Comments") = oListControl.SelectedItems(i).Code

                        dtMasterData.Rows.Add(drTemp)

                        Dim drNew As DataRow = dtAddData.NewRow()
                        drNew("ID") = oListControl.SelectedItems(i).ID
                        drNew("Comments") = oListControl.SelectedItems(i).Code
                        dtAddData.Rows.Add(drNew)

                    End If
                Next
            End If
            Dim k As Integer = 0
            If dtAddData IsNot Nothing AndAlso dtAddData.Rows.Count > 0 Then
                For k = 0 To dtAddData.Rows.Count - 1
                    If strComments = "OB Vital Comments" Then
                        Dim OBComment As String = dtAddData.Rows(k)("Comments").ToString()
                        Dim PreviousOBComment As String = txtComment.Text
                        If (String.Compare(OBComment, PreviousOBComment)) = 0 Then
                            Exit Sub
                        End If
                        If Not txtComment.Text.Equals(OBComment) Then
                            If Not String.IsNullOrEmpty(txtComment.Text.Trim()) Then
                                If (txtComment.Text.Length < txtComment.MaxLength) Then
                                    If ((OBComment.Trim.Length + Environment.NewLine.Length) <= (txtComment.MaxLength - txtComment.Text.Trim.Length)) Then
                                        txtComment.Text = txtComment.Text + Environment.NewLine + dtAddData.Rows(k)("Comments").ToString()
                                    End If
                                End If
                            Else
                                If (txtComment.Text.Length < txtComment.MaxLength) Then
                                    txtComment.Text = dtAddData.Rows(k)("Comments").ToString() + " "
                                End If
                            End If
                        End If
                    End If

                Next


            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
        Finally
            pnlMain.Visible = True
        End Try
    End Sub

End Class

