
Imports gloUserControlLibrary
Imports gloListControl
Imports System.Data.SqlClient
Imports gloEMR.gloEMRWord

Public Class frmPatientSynopsis
    Implements IPatientContext
    Private blnFlagIsGlobalPatientCheckedIn As Boolean = False

    'Declaration OF variables
    ' Dim dt As New DataTable
    Dim oPage As TabPage = Nothing

    Private WithEvents otab As New TabControl
    Dim WithEvents oEDocumentV3 As gloEDocumentV3.gloEDocV3Management
    'object frmobj frmobj commented as not in use
    'Dim frmobj As New gloUCPatientSynopsis

    '''''''''''''''instead of medication form show the Rx-Med Form
    'Dim WithEvents ofrmMeds As frmMedication
    Dim WithEvents frmRxMeds As frmPrescription
    '''''''''''''''instead of medication form show the Rx-Med Form

    Dim WithEvents ofrmProblems As frmProblemList
    Dim WithEvents ofrmDiagnosis As frm_Diagnosis
    Dim WithEvents ofrmOrders As frm_LM_Orders
    'Dim WithEvents ofrmlabReqOrder As frmLab_RequestOrder
    Dim WithEvents ofrmHistroy As frmHistory
    'Madan added for viewing gloLab... on 20100406
    Dim WithEvents ofrmViewGloLab As gloEmdeonInterface.Forms.frmViewgloLab
    'End

    ' Dim c2 As ClsPatientSynopsis
    Private strPatientCode As String
    'PRD: PRD EMR 6010_0210 Patient Safety Patient Identification changes
    Private strPatientFullName As String
    Private strPatientFirstName As String
    Private strPatientMiddleName As String
    Private strPatientLastName As String
    Private strPatientDOB As String
    Private strPatientAge As String
    Private strPatientGender As String
    Private strPatientMaritalStatus As String

    'Dim SelectedPatientId As Int64 = gnPatientID
    Dim SelectedPatientId As Int64 = _PatientID

    Dim _PatientID As Long 'Patient pass by constructor.

    Private WithEvents _PatientSynopsisUC As gloUCPatientSynopsis
    Private WithEvents octlPatientList As gloListControl.gloListControl
    Private WithEvents oPatientListControl As gloPatient.PatientListControl

    Private Const COL_D_CAT_ID = 0 ' ID
    Private Const COL_D_CAT_NAME = 1 ' Name
    Private Const COL_D_CAT_NOTEFLAG = 2 ' Note Flag
    Private Const COL_D_CAT_EXTRAFLAG = 3 ' Extra Col
    Private Const COL_D_CAT_SOURCEMACHINE = 4 ' Source Machine
    Private Const COL_D_CAT_SYSTEMFOLDER = 5 ' System Folder
    Private Const COL_D_CAT_CONTAINER = 6 ' Container
    Private Const COL_D_CAT_CATEGORY = 7 ' Category
    Private Const COL_D_CAT_PATIENTID = 8 ' Patient ID
    Private Const COL_D_CAT_YEAR = 9 ' Year
    Private Const COL_D_CAT_MONTH = 10 ' Month
    Private Const COL_D_CAT_SOURCEBIN = 11 ' Source Bin
    Private Const COL_D_CAT_INUSED = 12 ' In Used
    Private Const COL_D_CAT_USEDMACHINE = 13 ' Used Machine
    Private Const COL_D_CAT_USEDTYPE = 14 ' Used Type
    Private Const COL_D_CAT_PATH = 15 ' Path
    Private Const COL_D_CAT_COLTYPE = 16
    Private Const COL_D_CAT_FILENAME = 17 ' File Name
    Private Const COL_D_CAT_MACHINEID = 18 ' File Name
    Private Const COL_D_CAT_VERSIONNO = 19 ' Version No
    Private Const COL_D_CAT_ISREVIWED = 20 ' Reviwed
    Private Const COL_D_CAT_REVIWEDFLAG = 21 ' Reviwed
    'Private Const COL_D_CAT_COUNT = 22
    Private Const COL_View_CategoryHidden = 22
    Private Const COL_View_Category = 23
    Private Const COL_View_Month = 24
    Private Const COL_View_DocumentName = 25
    Private Const COL_View_NOTEFLAG = 26
    Private Const COL_View_REVIWEDFLAG = 27
    Private Const Col_view_Count = 28
    Private mgnVisitID As Int64 = 0
    Private RecordCount As Int32 = 0
    Dim currCount As Int32 = 0
    Dim lastCount As Int32 = 1
    Private WithEvents GloUC_TransactionHistory1 As gloUserControlLibrary.gloUC_TransactionHistory = Nothing

    'Code Added for opening Vitals form -20110525
    Public Sub subOpenVitalsForm(ByVal sender As Object, ByVal e As EventArgs, ByVal longVitalId As Long)
        Dim frmPatientVitals As New frmPatientVitals(longVitalId, GetCurrentPatientID)
        With frmPatientVitals
            '.MdiParent = Me
            '.WindowState = FormWindowState.Maximized
            .ShowInTaskbar = False
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog(IIf(IsNothing(frmPatientVitals.Parent), Me, frmPatientVitals.Parent))
        End With
        If Not IsNothing(frmPatientVitals) Then   'Disposed By Mitesh
            frmPatientVitals.Dispose()
            frmPatientVitals = Nothing
        End If
    End Sub
    Public Sub OpenVitalsForm(ByVal longVitalId As Long)
        Dim frmPatientVitals As New frmPatientVitals(longVitalId, GetCurrentPatientID)
        With frmPatientVitals
            '.MdiParent = Me
            '.WindowState = FormWindowState.Maximized
            .ShowInTaskbar = False
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog(IIf(IsNothing(frmPatientVitals.Parent), Me, frmPatientVitals.Parent))
        End With
        If Not IsNothing(frmPatientVitals) Then   'Disposed By Mitesh
            frmPatientVitals.Dispose()
            frmPatientVitals = Nothing
        End If
    End Sub

    Private Sub tlsbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsbtnClose.Click
        ' Me.Close()
        gloWord.WordDialogBoxBackgroundCloser.Close(Me)
    End Sub

    Private Sub frmPatientSynopsis_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub frmPatientSynopsis_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If (IsNothing(GloUC_TransactionHistory1) = False) Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(GloUC_TransactionHistory1)
                GloUC_TransactionHistory1.Dispose()
                GloUC_TransactionHistory1 = Nothing
            Catch ex As Exception

            End Try
        End If
        If (IsNothing(_PatientSynopsisUC) = False) Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(_PatientSynopsisUC)
                _PatientSynopsisUC.oMDI = Nothing
                _PatientSynopsisUC.Dispose()
                _PatientSynopsisUC = Nothing
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub frmPatientSynopsis_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not IsNothing(frmRxMeds) Then
            frmRxMeds.Dispose()
            frmRxMeds = Nothing
        End If
        If Not IsNothing(ofrmHistroy) Then
            ofrmHistroy.Dispose()
            ofrmHistroy = Nothing
        End If
        If Not IsNothing(ofrmProblems) Then
            ofrmProblems.Dispose()
            ofrmProblems = Nothing
        End If
        If Not IsNothing(ofrmDiagnosis) Then
            ofrmDiagnosis.Dispose()
            ofrmDiagnosis = Nothing
        End If
        If Not IsNothing(ofrmOrders) Then
            ofrmOrders.Dispose()
            ofrmOrders = Nothing
        End If
        'Lines commented by dipak 20100906 for cASE UC5070.003 as ofrmlabReqOrder not in use
        'If Not IsNothing(ofrmlabReqOrder) Then
        '    ofrmlabReqOrder.Dispose()
        '    ofrmlabReqOrder = Nothing
        'End If
        'end line commented by dipak.
        'Madan added on 20100406--For viewing gloLab
        If Not IsNothing(ofrmViewGloLab) Then
            'RemoveHandler ofrmViewGloLab.EvntGenerateCDAHandler, AddressOf CType(Me.ParentForm, MainMenu).openCDA
            'RemoveHandler ofrmViewGloLab.EventCDA, AddressOf mdlGeneral.OpenCDA
            ' Dim myCloseEventHandler As New System.Windows.Forms.FormClosedEventHandler(AddressOf CType(Me.ParentForm, MainMenu).ofrmViewgloLab_FormClosed) 'ShowTasks)
            ' Dim myOpenEventHandler As New gloEmdeonInterface.Forms.frmViewgloLab.OpenClinicalChart(AddressOf CType(Me.ParentForm, MainMenu).OpenClinicalChart)
            '  Dim myCCDEventHandler As New gloEmdeonInterface.Forms.frmViewgloLab.GenerateCCDHandler(AddressOf CType(Me.ParentForm, MainMenu).openCCD)
            Try
                RemoveHandler ofrmViewGloLab.EvntOpenClinicalChart, AddressOf CType(Me.ParentForm, MainMenu).OpenClinicalChart 'myOpenEventHandler
                RemoveHandler ofrmViewGloLab.EntOpenMessage, AddressOf CType(Me.ParentForm, MainMenu).OpenMessage
                RemoveHandler ofrmViewGloLab.EvntOpenPatientLetter, AddressOf CType(Me.ParentForm, MainMenu).OpenPatientLetter
                RemoveHandler ofrmViewGloLab.EvntOpenReferralLetter, AddressOf CType(Me.ParentForm, MainMenu).OpenReferralLetters
                RemoveHandler ofrmViewGloLab.EvntGenerateCCDHandler, AddressOf CType(Me.ParentForm, MainMenu).openCCD 'myCCDEventHandler
                RemoveHandler ofrmViewGloLab.EvntGenerateCDAHandler, AddressOf CType(Me.ParentForm, MainMenu).openCDA
                RemoveHandler ofrmViewGloLab.FormClosed, AddressOf CType(Me.ParentForm, MainMenu).ofrmViewgloLab_FormClosed 'myCloseEventHandler
                RemoveHandler ofrmViewGloLab.Activated, AddressOf CType(Me.ParentForm, MainMenu).frmViewgloLab_Activated
                ' RemoveHandler ofrmViewGloLab.EvntOpenClinicalChart, myOpenEventHandler
                ''Bug No 57350::Patient InfoButton - Applicatipn not able to open Patient spacific & Provider Spacific Document
                RemoveHandler ofrmViewGloLab.EntOpenEducation, AddressOf CType(Me.ParentForm, MainMenu).OpenEducation
                RemoveHandler ofrmViewGloLab.EventCDA, AddressOf mdlGeneral.OpenCDA
            Catch ex As Exception

            End Try
            If (IsNothing(ofrmViewGloLab) = False) Then
                ofrmViewGloLab.Close()
            End If

            If (IsNothing(ofrmViewGloLab) = False) Then
                ofrmViewGloLab.Dispose()
                ofrmViewGloLab = Nothing
            End If

            'myCloseEventHandler = Nothing
            'myOpenEventHandler = Nothing
            'myCCDEventHandler = Nothing
        End If

        'End Madan
        DisposeGlobal()
    End Sub
    Private Sub Get_PatientDetails()
        Dim dtPatient As DataTable = Nothing

        Try
            'dtPatient = New DataTable
            'dtPatient = GetPatientInfo(_PatientID)
            'PRD: PRD EMR 6010_0210 Patient Safety Patient Identification changes
            dtPatient = gloPatient.gloPatient.GetPatientInfo(_PatientID, GetConnectionString(), gstrMessageBoxCaption)

            If IsNothing(dtPatient) = False Then
                If dtPatient.Rows.Count > 0 Then
                    strPatientCode = Convert.ToString(dtPatient.Rows(0)("sPatientCode"))
                    'strPatientFirstName = Convert.ToString(dtPatient.Rows(0)("sFirstName"))
                    ''ADDED BY SHUBHANGI 20110516
                    'strPatientMiddleName = Convert.ToString(dtPatient.Rows(0)("sMiddleName"))
                    'strPatientLastName = Convert.ToString(dtPatient.Rows(0)("sLastName"))
                    strPatientFullName = dtPatient.Rows(0)("PatientName")             'PRD: PRD EMR 6010_0210 Patient Safety Patient Identification changes
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


        End Try
    End Sub

    Private Sub frmPatientSynopsis_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''Sanjog-Jan 31 2011 to show the tools button item
        Me.SuspendLayout()
        ''Sanjog-Jan 31 2011 to show the tools button item


        Try
            Me.DoubleBuffered = True
            InitializeToolStrip()
            Me.WindowState = FormWindowState.Maximized
            pnlPatientListView.Visible = False

            'Dim dt As DataTable = getAllPatientsWithTodaysAppointmets()



            ''Add TabControl 
            pnlPatientTab.Controls.Add(otab)
            otab.Dock = DockStyle.Fill
            Call Get_PatientDetails()
            oPage = New TabPage
            'COMMENTED BY SHUBHANGI 20110516
            'oPage.Text = strPatientFirstName & " " & strPatientLastName
            'ADDED BY SHUBHANGI 20110516
            'PRD: PRD EMR 6010_0210 Patient Safety Patient Identification changes

            oPage.Text = strPatientFullName

            ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'oPage.Tag = gnPatientID
            oPage.Tag = _PatientID
            'end modification
            otab.TabPages.Add(oPage)

            pnlPatientTab.Visible = True

            _PatientSynopsisUC = New gloUCPatientSynopsis(oPage.Tag)
            _PatientSynopsisUC.oMDI = CType(Me.MdiParent, MainMenu)
            'Set the PatientID from first tab to user control patientid
            oPage.Controls.Add(_PatientSynopsisUC)
            _PatientSynopsisUC.Dock = DockStyle.Fill
            '' otab.ContextMenuStrip = cntPatient
            ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'SelectedPatientId = gnPatientID
            SelectedPatientId = _PatientID
            'end modification

            mgnVisitID = GetVisitID()
            ''Commenetd becoz if we double click on synopsis button double instances were opening
            '' Application.DoEvents()

            '*************************************************************************
            '1. get patients having todays appointments
            '2. check the criteria for the patient appointments 
            ' Get Patients having Today's Appointments 
            '_strSQL = "SELECT DISTINCT isnull(PatientTracking.nPatientID,0) as nPatientID, ISNULL(PatientTracking.nMSTAppointmentID, 0) AS nMSTAppointmentID, ISNULL(PatientTracking.nDTLAppointmentID, 0) " _
            '    & " AS nDTLAppointmentID, AS_Appointment_DTL.dtStartTime" _
            '    & " FROM PatientTracking LEFT OUTER JOIN AS_Appointment_DTL ON PatientTracking.nMSTAppointmentID = AS_Appointment_DTL.nMSTAppointmentID" _
            '    & " WHERE (CONVERT(varchar, PatientTracking.dtDate, 101) = '" & DateTime.Now.ToString("MM/dd/yyyy") & "') AND (PatientTracking.nClinicID = " & gnClinicID & ")" _
            '    & "ORDER BY AS_Appointment_DTL.dtStartTime"
            tlsbtnNext.Enabled = False
            tlsbtnPrevious.Enabled = False
            'GetCheckedInPatients()
            ''Sandip Darade 24 Feb 2009

            'for first patient in the list,retrive patient id,add lab control
            GloUC_TransactionHistory1 = New gloUserControlLibrary.gloUC_TransactionHistory()
            GloUC_TransactionHistory1.Dock = DockStyle.Fill
            GloUC_TransactionHistory1.Visible = True
            GloUC_TransactionHistory1.ShowReceivedate = True
            pnlPatientTab.Controls.Add(GloUC_TransactionHistory1)
            ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'GloUC_TransactionHistory1.LoadPreviousLabs(gnPatientID, DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss"))
            GloUC_TransactionHistory1.LoadPreviousLabs(_PatientID, DateTime.Now) '.ToString("MM/dd/yyyy hh:mm:ss"))
            'end modification
            GloUC_TransactionHistory1.DesignTestGrid()
            GloUC_TransactionHistory1.SetDataByDate(DateTime.Now.Date, DateTime.Now.Date)
            GloUC_TransactionHistory1.cmbCriteria.Text = "Date"
            'GloUC_TransactionHistory1.BringToFront()
            GloUC_TransactionHistory1.Enabled = False

            '*****************************************************************************888
            Try

                'gloPatient.gloPatient.GetWindowTitle(Me, strPatientLastName, strPatientFirstName, strPatientMiddleName, strPatientCode)
                gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try





        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        ''Sanjog-Jan 31 2011 to show the tools button item
        Me.ResumeLayout()
        ''Sanjog-Jan 31 2011 to show the tools button item
    End Sub



    Private Sub ShowCheckedInPatients()
        Try
            GetCheckedInPatients()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ShowCheckinPatient, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub


    Private Function DeleteGlobalPatientIfNotCheckedIn(ByVal dtPatients As DataTable) As Boolean
        Try
            For cnt As Int16 = 0 To dtPatients.Rows.Count - 1
                If dtPatients.Rows(cnt)("npatientid") = _PatientID Then '' Replace gnPatientID
                    blnFlagIsGlobalPatientCheckedIn = True
                End If
            Next
            Return blnFlagIsGlobalPatientCheckedIn
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ShowCheckinPatient, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        End Try
    End Function

    ' GLO2010-0004251 : Synopsis Check--in button doesn't work
    ' Updated the Function : Shifted the query to SP for showing Checked-In Patients as per Provider. 
    Private Function GetCheckedInPatients() As Boolean
        Dim dtPatients As New DataTable
        Dim dtPatientName As DataTable = Nothing
        Dim oPatient As New gloPatient.gloPatient(GetConnectionString)
        Dim cmd As SqlCommand = Nothing
        Dim con As SqlConnection = Nothing
        Dim da As New SqlDataAdapter
        Dim objParam As SqlParameter
        Try
            con = New SqlConnection(GetConnectionString)
            cmd = New SqlCommand("gsp_GetCheckedInPatientByProvider", con)
            cmd.CommandType = CommandType.StoredProcedure

            objParam = cmd.Parameters.Add("@nProviderID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = gnLoginProviderID

            objParam = cmd.Parameters.Add("@nRowNumber", SqlDbType.Int)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = currCount

            da.SelectCommand = cmd
            da.Fill(dtPatients)

            If dtPatients IsNot Nothing Then
                If dtPatients.Rows.Count > 0 Then
                    'check if the global patient is check in. if not then remove the tab page
                    If DeleteGlobalPatientIfNotCheckedIn(dtPatients) = False Then
                        otab.TabPages.Clear()
                    Else
                        otab.TabPages.Clear()
                    End If

                    RecordCount = dtPatients.Rows.Count
                    If RecordCount > gnPatientSynopsisTabCount Then
                        tlsbtnNext.Enabled = True
                    Else
                        tlsbtnNext.Enabled = False
                    End If

                    If gnPatientSynopsisTabCount >= CType(dtPatients.Rows(0)("RowNumber"), Integer) Then
                        tlsbtnPrevious.Enabled = False
                    Else
                        tlsbtnPrevious.Enabled = True
                    End If

                    For i As Integer = 0 To dtPatients.Rows.Count - 1
                        If otab.TabPages.Count < gnPatientSynopsisTabCount Then
                            Dim oPage As TabPage = New TabPage
                            dtPatientName = gloPatient.gloPatient.GetPatientInfo(dtPatients.Rows(i)("nPatientID"), GetConnectionString(), gstrMessageBoxCaption)
                            Dim PatientName As String = dtPatientName.Rows(0)("PatientName")
                            oPage.Text = PatientName
                            oPage.Tag = dtPatients.Rows(i)("nPatientID") 'gnPatientID
                            otab.TabPages.Add(oPage)

                            pnlPatientTab.Visible = True

                            SelectedPatientId = dtPatients.Rows(i)("nPatientID") 'gnPatientID
                            mgnVisitID = GetVisitID()
                        End If
                    Next
                    Try
                        Me.Text = "Patient Synopsis"
                        gloPatient.gloPatient.GetWindowTitle(Me, SelectedPatientId, GetConnectionString(), gstrMessageBoxCaption)
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    End Try
                Else
                    MessageBox.Show("There are no checked in patients available.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    GetCheckedInPatients = Nothing
                    Exit Function
                End If
            Else
                MessageBox.Show("There are no checked in patients available.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                GetCheckedInPatients = Nothing
                Exit Function
            End If

            If otab.TabPages.Count > 0 Then
                otab.SelectedTab = otab.TabPages(0)
                Dim _patid As Int64
                _patid = CType(otab.SelectedTab.Tag, Int64)
                SelectedPatientId = _patid
                If Not IsNothing(_PatientSynopsisUC) Then
                    If (IsNothing(oPage) = False) Then
                        If (oPage.Controls.Contains(_PatientSynopsisUC)) Then
                            oPage.Controls.Remove(_PatientSynopsisUC)
                        End If
                    End If

                    RemoveHandler _PatientSynopsisUC.ViewMedications, AddressOf _PatientSynopsisUC_ViewMedications
                    RemoveHandler _PatientSynopsisUC.ViewDMS, AddressOf _PatientSynopsisUC_ViewDMS
                    RemoveHandler _PatientSynopsisUC.ViewPatientHistory, AddressOf _PatientSynopsisUC_ViewPatientHistory
                    RemoveHandler _PatientSynopsisUC.ViewProblemList, AddressOf _PatientSynopsisUC_ViewProblemList
                    RemoveHandler _PatientSynopsisUC.ViewProcedures, AddressOf _PatientSynopsisUC_ViewProcedures
                    RemoveHandler _PatientSynopsisUC.ViewRadiology, AddressOf _PatientSynopsisUC_ViewRadiology
                    RemoveHandler _PatientSynopsisUC.OnLabsFlexDoubleClick, AddressOf _PatientSynopsisUC_OnLabsFlexDoubleClick
                    RemoveHandler _PatientSynopsisUC.ShowMedicationForm, AddressOf _PatientSynopsisUC_ShowMedicationForm
                    RemoveHandler _PatientSynopsisUC.ShowProblemListForm, AddressOf _PatientSynopsisUC_ShowProblemListForm
                    RemoveHandler _PatientSynopsisUC.ShowRadiologyForm, AddressOf _PatientSynopsisUC_ShowRadiologyForm
                    RemoveHandler _PatientSynopsisUC.ShowHistoryForm, AddressOf _PatientSynopsisUC_ShowHistoryForm
                    _PatientSynopsisUC.oMDI = Nothing
                    _PatientSynopsisUC.Dispose()
                    _PatientSynopsisUC = Nothing
                End If

                oPage = otab.SelectedTab

                CType(Me.ParentForm, MainMenu).ShowDefaultPatientDetails(SelectedPatientId)


                _PatientSynopsisUC = New gloUCPatientSynopsis(otab.SelectedTab.Tag)
                _PatientSynopsisUC.PatientID = CType(otab.SelectedTab.Tag, Int64)
                _PatientSynopsisUC.oMDI = CType(Me.MdiParent, MainMenu)

                AddHandler _PatientSynopsisUC.ViewMedications, AddressOf _PatientSynopsisUC_ViewMedications
                AddHandler _PatientSynopsisUC.ViewDMS, AddressOf _PatientSynopsisUC_ViewDMS
                AddHandler _PatientSynopsisUC.ViewPatientHistory, AddressOf _PatientSynopsisUC_ViewPatientHistory
                AddHandler _PatientSynopsisUC.ViewProblemList, AddressOf _PatientSynopsisUC_ViewProblemList
                AddHandler _PatientSynopsisUC.ViewProcedures, AddressOf _PatientSynopsisUC_ViewProcedures
                AddHandler _PatientSynopsisUC.ViewRadiology, AddressOf _PatientSynopsisUC_ViewRadiology
                AddHandler _PatientSynopsisUC.OnLabsFlexDoubleClick, AddressOf _PatientSynopsisUC_OnLabsFlexDoubleClick
                AddHandler _PatientSynopsisUC.ShowMedicationForm, AddressOf _PatientSynopsisUC_ShowMedicationForm
                AddHandler _PatientSynopsisUC.ShowProblemListForm, AddressOf _PatientSynopsisUC_ShowProblemListForm
                AddHandler _PatientSynopsisUC.ShowRadiologyForm, AddressOf _PatientSynopsisUC_ShowRadiologyForm
                AddHandler _PatientSynopsisUC.ShowHistoryForm, AddressOf _PatientSynopsisUC_ShowHistoryForm

                oPage.Controls.Add(_PatientSynopsisUC)
                _PatientSynopsisUC.Dock = DockStyle.Fill
                'Try
                '    If (IsNothing(otab.ContextMenuStrip) = False) Then
                '        otab.ContextMenuStrip.Dispose()
                '        otab.ContextMenuStrip = Nothing
                '    End If
                'Catch ex As Exception

                'End Try
                otab.ContextMenuStrip = Nothing
                Try
                    'ADDED BY SHUBHANGI TO RESOLVED ISSUE 14250
                    Me.Text = "Patient Synopsis"
                    gloPatient.gloPatient.GetWindowTitle(Me, SelectedPatientId, GetConnectionString(), gstrMessageBoxCaption)
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                End Try
            End If
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ShowCheckinPatient, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return False
        Finally
            If Not IsNothing(dtPatients) Then  'Disposed By Mitesh
                dtPatients.Dispose()
                dtPatients = Nothing
            End If
            If Not IsNothing(dtPatientName) Then
                dtPatientName.Dispose()
                dtPatientName = Nothing
            End If
            If Not IsNothing(oPatient) Then
                oPatient.Dispose()
                oPatient = Nothing
            End If
            If Not IsNothing(con) Then
                con.Dispose()
                con = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            objParam = Nothing
        End Try
    End Function

    Private Sub PrevButton()
        Try
            'Remove existing tabs
            RecordCount = RecordCount - gnPatientSynopsisTabCount
            Dim tabcount As Int32 = otab.TabPages.Count - 1

            For i As Int32 = tabcount - 1 To 0 Step -1
                otab.TabPages(i).Controls.Clear()
                otab.TabPages.RemoveAt(i)
            Next


            If (currCount - gnPatientSynopsisTabCount) >= 0 Then
                currCount = currCount - gnPatientSynopsisTabCount

                'If (currCount - gnPatientSynopsisTabCount + 1) >= 0 Then
                'currCount = (currCount - gnPatientSynopsisTabCount) + 1


                'tlsbtnPrevious.Enabled = True
            Else
                currCount = 0
                'tlsbtnPrevious.Enabled = False
            End If
            'If lastCount < currCount Then
            '    tlsbtnNext.Enabled = False
            'Else
            '    tlsbtnNext.Enabled = True
            'End If


            'If blnFlagIsGlobalPatientCheckedIn = False And currCount > 0 Then
            '    currCount = currCount - 1
            'End If
            GetCheckedInPatients()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ShowCheckinPatient, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub NextButton()

        Try

            lastCount = RecordCount
            'Remove existing tabs

            Dim tabcount As Int32 = otab.TabPages.Count - 1

            For i As Int32 = otab.TabPages.Count - 1 To 0 Step -1
                otab.TabPages(i).Controls.Clear()
                otab.TabPages.RemoveAt(i)
            Next

            currCount = currCount + gnPatientSynopsisTabCount


            GetCheckedInPatients()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ShowCheckinPatient, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    'Incident #55315: 00016572 : Carry forward issue
    Private Sub _PatientSynopsisUC_GetPrescription() Handles _PatientSynopsisUC.GetPrescription
        Dim frmRxMeds As frmPrescription
        frmRxMeds = frmPrescription.GetInstance(SelectedPatientId)
        If IsNothing(frmRxMeds) = True Then
            Exit Sub
        End If
        If frmPrescription.IsOpen = False Then
            frmRxMeds.ShowMedication()
        End If

        If frmRxMeds.blncancel = True Then
            With frmRxMeds
                .WindowState = FormWindowState.Maximized
                .MdiParent = Me.ParentForm
                .blnOpenFromExam = True
                .Show()
            End With
        End If
    End Sub

    Private Sub otab_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles otab.MouseDown, Me.MouseDown, _PatientSynopsisUC.MouseDown
        Try



            If e.Button = Windows.Forms.MouseButtons.Right Then
                'If e.X = otab.TabPages(0).Height - otab.TabPages(0).ClientSize.Height Then
                '    MessageBox.Show("Tab Clicked")
                'End If
                'Try
                '    If (IsNothing(otab.ContextMenuStrip) = False) Then
                '        otab.ContextMenuStrip.Dispose()
                '        otab.ContextMenuStrip = Nothing
                '    End If
                'Catch ex As Exception

                'End Try
                otab.ContextMenuStrip = Nothing
                If otab.TabPages.Count = 1 Then
                    mnuDeleteTab.Visible = False
                    'Try
                    '    If (IsNothing(otab.ContextMenuStrip) = False) Then
                    '        otab.ContextMenuStrip.Dispose()
                    '        otab.ContextMenuStrip = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    otab.ContextMenuStrip = Nothing

                ElseIf otab.TabPages.Count > 1 Then
                    'Try
                    '    If (IsNothing(otab.ContextMenuStrip) = False) Then
                    '        otab.ContextMenuStrip.Dispose()
                    '        otab.ContextMenuStrip = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    'If e.X >= otab.SelectedTab.Text And e.X <= otab.SelectedTab.Width Then
                    otab.ContextMenuStrip = cntPatient
                    mnuDeleteTab.Visible = True
                    'End If
                End If
            Else
                'Try
                '    If (IsNothing(otab.ContextMenuStrip) = False) Then
                '        otab.ContextMenuStrip.Dispose()
                '        otab.ContextMenuStrip = Nothing
                '    End If
                'Catch ex As Exception

                'End Try
                otab.ContextMenuStrip = Nothing
                mnuDeleteTab.Visible = False
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub



    Private Sub otab_Selected(ByVal sender As Object, ByVal e As System.Windows.Forms.TabControlEventArgs) Handles otab.Selected
        Try
            If otab.TabPages.Count > 0 Then


                Dim _patid As Int64
                _patid = CType(e.TabPage.Tag, Int64)
                SelectedPatientId = _patid
                ''gnPatientID = _patid '' Coed Commented Replace gnPatientID
                Dim oPage As TabPage = otab.SelectedTab
                'If CType(Me.MdiParent, MainMenu).ShowDefaultPatientDetails() = False Then
                CType(Me.ParentForm, MainMenu).ShowDefaultPatientDetails(SelectedPatientId) ''Replace gnPatientID
                CType(Me.ParentForm, MainMenu).oPatientListControl.SelectPatient(SelectedPatientId) ''Replace gnPatientID

                'End If

                'If Not IsNothing(_PatientSynopsisUC) Then
                '    For i As Integer = pnlPatientTab.Controls.Count - 1 To 0 Step -1

                '        If _PatientSynopsisUC.Name = pnlPatientTab.Controls(i).Name Then
                '            pnlPatientTab.Controls.RemoveAt(i)
                '            _PatientSynopsisUC.Dispose()
                '        End If
                '    Next
                'End If

                If Not IsNothing(_PatientSynopsisUC) Then
                    If (oPage.Controls.Contains(_PatientSynopsisUC)) Then
                        oPage.Controls.Remove(_PatientSynopsisUC)
                    End If

                    RemoveHandler _PatientSynopsisUC.ViewMedications, AddressOf _PatientSynopsisUC_ViewMedications
                    RemoveHandler _PatientSynopsisUC.ViewDMS, AddressOf _PatientSynopsisUC_ViewDMS
                    RemoveHandler _PatientSynopsisUC.ViewPatientHistory, AddressOf _PatientSynopsisUC_ViewPatientHistory
                    RemoveHandler _PatientSynopsisUC.ViewProblemList, AddressOf _PatientSynopsisUC_ViewProblemList
                    RemoveHandler _PatientSynopsisUC.ViewProcedures, AddressOf _PatientSynopsisUC_ViewProcedures
                    RemoveHandler _PatientSynopsisUC.ViewRadiology, AddressOf _PatientSynopsisUC_ViewRadiology
                    RemoveHandler _PatientSynopsisUC.OnLabsFlexDoubleClick, AddressOf _PatientSynopsisUC_OnLabsFlexDoubleClick
                    ''Commented on 20100812 by sanjog for History Form should open only one time
                    ''RemoveHandler _PatientSynopsisUC.ShowHistoryForm, AddressOf _PatientSynopsisUC_ShowHistoryForm
                    ''Commented on 20100812 by sanjog for History Form should open only one time
                    RemoveHandler _PatientSynopsisUC.ShowMedicationForm, AddressOf _PatientSynopsisUC_ShowMedicationForm
                    RemoveHandler _PatientSynopsisUC.ShowProblemListForm, AddressOf _PatientSynopsisUC_ShowProblemListForm
                    RemoveHandler _PatientSynopsisUC.ShowRadiologyForm, AddressOf _PatientSynopsisUC_ShowRadiologyForm
                    RemoveHandler _PatientSynopsisUC.ShowHistoryForm, AddressOf _PatientSynopsisUC_ShowHistoryForm
                    _PatientSynopsisUC.oMDI = Nothing
                    _PatientSynopsisUC.Dispose()
                    _PatientSynopsisUC = Nothing
                End If

                _PatientSynopsisUC = New gloUCPatientSynopsis(e.TabPage.Tag)
                _PatientSynopsisUC.PatientID = CType(e.TabPage.Tag, Int64)


                AddHandler _PatientSynopsisUC.ViewMedications, AddressOf _PatientSynopsisUC_ViewMedications
                AddHandler _PatientSynopsisUC.ViewDMS, AddressOf _PatientSynopsisUC_ViewDMS
                AddHandler _PatientSynopsisUC.ViewPatientHistory, AddressOf _PatientSynopsisUC_ViewPatientHistory
                AddHandler _PatientSynopsisUC.ViewProblemList, AddressOf _PatientSynopsisUC_ViewProblemList
                AddHandler _PatientSynopsisUC.ViewProcedures, AddressOf _PatientSynopsisUC_ViewProcedures
                AddHandler _PatientSynopsisUC.ViewRadiology, AddressOf _PatientSynopsisUC_ViewRadiology
                AddHandler _PatientSynopsisUC.OnLabsFlexDoubleClick, AddressOf _PatientSynopsisUC_OnLabsFlexDoubleClick
                ''Commented on 20100812 by sanjog for History Form should open only one time
                ''AddHandler _PatientSynopsisUC.ShowHistoryForm, AddressOf _PatientSynopsisUC_ShowHistoryForm
                ''Commented on 20100812 by sanjog for History Form should open only one time
                AddHandler _PatientSynopsisUC.ShowMedicationForm, AddressOf _PatientSynopsisUC_ShowMedicationForm
                AddHandler _PatientSynopsisUC.ShowProblemListForm, AddressOf _PatientSynopsisUC_ShowProblemListForm
                AddHandler _PatientSynopsisUC.ShowRadiologyForm, AddressOf _PatientSynopsisUC_ShowRadiologyForm
                AddHandler _PatientSynopsisUC.ShowHistoryForm, AddressOf _PatientSynopsisUC_ShowHistoryForm

                oPage.Controls.Add(_PatientSynopsisUC)
                _PatientSynopsisUC.Dock = DockStyle.Fill
                'Try
                '    If (IsNothing(otab.ContextMenuStrip) = False) Then
                '        otab.ContextMenuStrip.Dispose()
                '        otab.ContextMenuStrip = Nothing
                '    End If
                'Catch ex As Exception

                'End Try
                otab.ContextMenuStrip = Nothing
                Try
                    Me.Text = "Patient Synopsis"
                    gloPatient.gloPatient.GetWindowTitle(Me, SelectedPatientId, GetConnectionString(), gstrMessageBoxCaption)
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                End Try
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            pnlPatientListView.Visible = True

            '' If Patient List Control alredy Exits Then Remove it
            If IsNothing(octlPatientList) = False Then
                If pnlPatientListView.Contains(octlPatientList) = True Then
                    pnlPatientListView.Controls.Remove(octlPatientList)
                End If
                octlPatientList.Dispose()
                octlPatientList = Nothing
            End If

            'Declare Variable for connection string
            octlPatientList = New gloListControl.gloListControl(GetConnectionString, gloListControlType.AllPatient, False, Me.Width)
            octlPatientList.Dock = DockStyle.Fill
            octlPatientList.ClinicID = 1
            octlPatientList.ShowHeaderPanel(False)
            octlPatientList.OpenControl()

            Dim intControlHeight As Integer
            intControlHeight = Me.Height - pnl_tlsp_Top.Height
            pnlPatientListView.Height = intControlHeight / 2
            'Add octlPatientList into pnlPatientListView
            pnlPatientListView.Controls.Add(octlPatientList)
            pnlPatientListView.Dock = DockStyle.Top

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub


    Private Sub _PatientSynopsisUC_OnLabsFlexDoubleClick(ByVal PatientID As Long, ByVal OrderID As Long, ByVal VisitID As Long, ByVal TransactionDate As Date) Handles _PatientSynopsisUC.OnLabsFlexDoubleClick
        Try

            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.Load, "frmPatientSynopsis ------- _PatientSynopsisUC_OnLabsFlexDoubleClick fired", gloAuditTrail.ActivityOutCome.Success)
            If (PatientID > 0 And OrderID > 0 And OrderID > 0) Then
                'Madan added for viewing gloLab on 20100406
                ' If gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_hsilabel <> "" Then
                If Not IsNothing(ofrmViewGloLab) Then
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.Load, "Found new instance of Labs........", gloAuditTrail.ActivityOutCome.Success)

                    If Not IsNothing(ofrmViewGloLab) Then
                        'RemoveHandler ofrmViewGloLab.EvntGenerateCDAHandler, AddressOf CType(Me.ParentForm, MainMenu).openCDA
                        'RemoveHandler ofrmViewGloLab.EventCDA, AddressOf mdlGeneral.OpenCDA
                        ' Dim myCloseEventHandler As New System.Windows.Forms.FormClosedEventHandler(AddressOf CType(Me.ParentForm, MainMenu).ofrmViewgloLab_FormClosed) 'ShowTasks)
                        ' Dim myOpenEventHandler As New gloEmdeonInterface.Forms.frmViewgloLab.OpenClinicalChart(AddressOf CType(Me.ParentForm, MainMenu).OpenClinicalChart)
                        ' Dim myCCDEventHandler As New gloEmdeonInterface.Forms.frmViewgloLab.GenerateCCDHandler(AddressOf CType(Me.ParentForm, MainMenu).openCCD)

                        Try
                            RemoveHandler ofrmViewGloLab.EvntOpenClinicalChart, AddressOf CType(Me.ParentForm, MainMenu).OpenClinicalChart 'myOpenEventHandler
                            RemoveHandler ofrmViewGloLab.EntOpenMessage, AddressOf CType(Me.ParentForm, MainMenu).OpenMessage
                            RemoveHandler ofrmViewGloLab.EvntOpenPatientLetter, AddressOf CType(Me.ParentForm, MainMenu).OpenPatientLetter
                            RemoveHandler ofrmViewGloLab.EvntOpenReferralLetter, AddressOf CType(Me.ParentForm, MainMenu).OpenReferralLetters
                            RemoveHandler ofrmViewGloLab.EvntGenerateCCDHandler, AddressOf CType(Me.ParentForm, MainMenu).openCCD 'myCCDEventHandler
                            RemoveHandler ofrmViewGloLab.EvntGenerateCDAHandler, AddressOf CType(Me.ParentForm, MainMenu).openCDA
                            RemoveHandler ofrmViewGloLab.FormClosed, AddressOf CType(Me.ParentForm, MainMenu).ofrmViewgloLab_FormClosed
                            RemoveHandler ofrmViewGloLab.Activated, AddressOf CType(Me.ParentForm, MainMenu).frmViewgloLab_Activated
                            '  RemoveHandler ofrmViewGloLab.EvntOpenClinicalChart, AddressOf CType(Me.ParentForm, MainMenu).OpenClinicalChart
                            ''Bug No 57350::Patient InfoButton - Applicatipn not able to open Patient spacific & Provider Spacific Document
                            RemoveHandler ofrmViewGloLab.EntOpenEducation, AddressOf CType(Me.ParentForm, MainMenu).OpenEducation
                            RemoveHandler ofrmViewGloLab.EventCDA, AddressOf mdlGeneral.OpenCDA
                        Catch ex As Exception

                        End Try
                        If (IsNothing(ofrmViewGloLab) = False) Then
                            ofrmViewGloLab.Close()
                        End If

                        If (IsNothing(ofrmViewGloLab) = False) Then
                            ofrmViewGloLab.Dispose()
                            ofrmViewGloLab = Nothing
                        End If

                        'myCloseEventHandler = Nothing
                        'myOpenEventHandler = Nothing
                        'myCCDEventHandler = Nothing
                    End If
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.Load, "Disposed the instance of labs form........", gloAuditTrail.ActivityOutCome.Success)
                End If
                ofrmViewGloLab = New gloEmdeonInterface.Forms.frmViewgloLab(OrderID, PatientID)

                'Code added for Already lab order Is open
                If (ofrmViewGloLab.CheckInstance() = True) Then
                    MessageBox.Show("A Lab Order screen is already open. Please close that to continue…", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If (IsNothing(ofrmViewGloLab) = False) Then
                        ofrmViewGloLab.Close()
                    End If

                    If (IsNothing(ofrmViewGloLab) = False) Then
                        ofrmViewGloLab.Dispose()
                        ofrmViewGloLab = Nothing
                    End If
                    Exit Sub
                End If
                'RemoveHandler ofrmViewGloLab.EvntGenerateCDAHandler, AddressOf CType(Me.ParentForm, MainMenu).openCDA
                'RemoveHandler ofrmViewGloLab.EventCDA, AddressOf mdlGeneral.OpenCDA
                'RemoveHandler ofrmViewGloLab.EvntOpenClinicalChart, AddressOf CType(Me.ParentForm, MainMenu).OpenClinicalChart
                AddHandler ofrmViewGloLab.EntOpenMessage, AddressOf CType(Me.ParentForm, MainMenu).OpenMessage
                AddHandler ofrmViewGloLab.EvntOpenPatientLetter, AddressOf CType(Me.ParentForm, MainMenu).OpenPatientLetter
                AddHandler ofrmViewGloLab.EvntOpenReferralLetter, AddressOf CType(Me.ParentForm, MainMenu).OpenReferralLetters
                AddHandler ofrmViewGloLab.EvntGenerateCCDHandler, AddressOf CType(Me.ParentForm, MainMenu).openCCD
                AddHandler ofrmViewGloLab.EvntGenerateCDAHandler, AddressOf CType(Me.ParentForm, MainMenu).openCDA
                AddHandler ofrmViewGloLab.EvntOpenPlanOfTreatment, AddressOf CType(Me.ParentForm, MainMenu).OpenPlanofTreatment
                AddHandler ofrmViewGloLab.FormClosed, AddressOf CType(Me.ParentForm, MainMenu).ofrmViewgloLab_FormClosed 'ShowTasks
                AddHandler ofrmViewGloLab.Activated, AddressOf CType(Me.ParentForm, MainMenu).frmViewgloLab_Activated
                AddHandler ofrmViewGloLab.EvntOpenClinicalChart, AddressOf CType(Me.ParentForm, MainMenu).OpenClinicalChart
                ''Bug No 57350::Patient InfoButton - Applicatipn not able to open Patient spacific & Provider Spacific Document
                AddHandler ofrmViewGloLab.EntOpenEducation, AddressOf CType(Me.ParentForm, MainMenu).OpenEducation
                AddHandler ofrmViewGloLab.EventCDA, AddressOf mdlGeneral.OpenCDA
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.Load, "Created new instance of Labs........", gloAuditTrail.ActivityOutCome.Success)
                'Developer:Sanjog(Dhamke)
                'Date: 2 Feb 2012
                'PRD Name: Lab Usability Changes
                'Reason: If order is open for modification that time its need to direct on orders tab not on result set tab
                ofrmViewGloLab.SelectOrderTab = True
                'End -Sanjog

                ofrmViewGloLab.LoginProviderID = gnLoginProviderID

                ofrmViewGloLab.LabOrderParameter.OrderID = OrderID
                ofrmViewGloLab.LabOrderParameter.OrderNumberID = 0
                ofrmViewGloLab.LabOrderParameter.OrderNumberPrefix = "ORD"
                ofrmViewGloLab.LabOrderParameter.PatientID = PatientID
                ofrmViewGloLab.LabOrderParameter.VisitID = VisitID
                ofrmViewGloLab.LabOrderParameter.TransactionDate = TransactionDate
                ''added for split control functionality order & result screen
                Dim objclsSplit_Laborder As New gloEMRGeneralLibrary.clsSplitScreen()
                objclsSplit_Laborder.clsPatientExams = New clsPatientExams()
                objclsSplit_Laborder.clsPatientLetters = New clsPatientLetters()
                objclsSplit_Laborder.clsPatientMessages = New clsMessage()
                objclsSplit_Laborder.clsNurseNotes = New clsNurseNotes()
                objclsSplit_Laborder.clsHistory = New clsPatientHistory()
                objclsSplit_Laborder.clsLabs = New clsDoctorsDashBoard()
                objclsSplit_Laborder.clsRxmed = New clsPatientDetails()
                objclsSplit_Laborder.clsOrders = New clsPatientDetails()
                objclsSplit_Laborder.clsProblemList = New clsPatientProblemList()
                objclsSplit_Laborder.blnShowSmokingStatusCol = gblnShowSmokingColumn

                ofrmViewGloLab.objCriteria = New DocCriteria
                ofrmViewGloLab.objWord = New clsWordDocument
                ofrmViewGloLab.VisitID = VisitID
                ofrmViewGloLab.clsSplit_Laborder = objclsSplit_Laborder

                CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
                CType(Me.MdiParent, MainMenu).pnlMainToolBar.Visible = False

                AddHandler ofrmViewGloLab.Activated, AddressOf frmViewgloLab_Activated
                'ADD TO RESOLVE THE ISSUE : IF WE ADD NEW LAB OR RESULT THEN IT SHOULD BE REFRESH
                AddHandler ofrmViewGloLab.FormClosed, AddressOf frmViewgloLab_Closed
                ofrmViewGloLab.MdiParent = Me.MdiParent
                ofrmViewGloLab.WindowState = FormWindowState.Maximized
                ofrmViewGloLab.Show()
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.Load, "Shown the Labs form from patient synopsis", gloAuditTrail.ActivityOutCome.Success)    ''  Me.Close()


            End If
            ' gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SysnopsisScreen, gloAuditTrail.ActivityType.Load, "", gloAuditTrail.ActivityOutCome.Success)    ''  Me.Close()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub openCCD(ByVal PatientID As Int64)
        'Code Start - Added by sanjog on 20100707 for CCD
        Try
            Dim objfrm As New frmCCDGenerateList(_PatientID)
            objfrm.ChkResults.Checked = True

            With objfrm
                .WindowState = FormWindowState.Normal
                .BringToFront()
                .StartPosition = FormStartPosition.CenterParent
                .ShowDialog(IIf(IsNothing(objfrm.Parent), Me, objfrm.Parent))
                .Dispose()
            End With
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub frmViewgloLab_Activated(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            CType(Me.MdiParent, MainMenu).SetGnPatientID = sender.GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    'ADD TO RESOLVE THE ISSUE : IF WE ADD NEW LAB OR RESULT THEN IT SHOULD BE REFRESH
    Public Sub frmViewgloLab_Closed(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ofrmViewgloLab As gloEmdeonInterface.Forms.frmViewgloLab = Nothing
        Try
            ofrmViewgloLab = DirectCast(sender, gloEmdeonInterface.Forms.frmViewgloLab)
        Catch ex As Exception

        End Try
        Try
            If (IsNothing(ofrmViewgloLab) = False) Then
                RemoveHandler ofrmViewgloLab.Activated, AddressOf frmViewgloLab_Activated
                'ADD TO RESOLVE THE ISSUE : IF WE ADD NEW LAB OR RESULT THEN IT SHOULD BE REFRESH
                RemoveHandler ofrmViewgloLab.FormClosed, AddressOf frmViewgloLab_Closed
            End If
        Catch ex As Exception
        End Try

        Try
            If (IsNothing(ofrmViewgloLab) = False) Then
                ofrmViewgloLab.Close()
            End If

        Catch ex As Exception

        End Try
        Try
            If (IsNothing(ofrmViewgloLab) = False) Then
                ofrmViewgloLab.Dispose()
                ofrmViewgloLab = Nothing
            End If

        Catch ex As Exception

        End Try

        Try
            If IsNothing(_PatientSynopsisUC) = False Then
                Dim dsdata As DataSet
                Dim c2 As ClsPatientSynopsis = New ClsPatientSynopsis
                dsdata = c2.PopulateSynopsisData(_PatientID, "Labs")
                _PatientSynopsisUC.PopulateLabs(dsdata.Tables("Labs"))
                If Not IsNothing(dsdata) Then
                    dsdata.Dispose()
                    dsdata = Nothing
                End If
                If Not IsNothing(c2) Then
                    c2.Dispose()
                    c2 = Nothing
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub _PatientSynopsisUC_ShowHistoryForm(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _PatientSynopsisUC.ShowHistoryForm
        Try
            If _PatientSynopsisUC.C1dgPatientDetails.Rows.Count > 1 Then
                Dim _VisitID As Int64 = CType(_PatientSynopsisUC.C1dgPatientDetails.GetData(_PatientSynopsisUC.C1dgPatientDetails.Row, 0), Int64)
                Dim _VisitDate As Date = CType(_PatientSynopsisUC.C1dgPatientDetails.GetData(_PatientSynopsisUC.C1dgPatientDetails.Row, 2), Date)

                Dim blnRecordLock As Boolean = False
                'If gblnRecordLocking = True Then
                '    Dim mydt As New mytable
                '    mydt = Scan_n_Lock_Transaction(TrnType.PatientHistory, SelectedPatientId, _VisitID, _VisitDate)
                '    'If mydt.Code <> gstrLoginName Or mydt.Description <> gstrClientMachineName Then
                '    If mydt.Description <> gstrClientMachineName Then
                '        If MessageBox.Show("This Patient History is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot modify it now.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                '            '' Open in Lock Mode
                '            blnRecordLock = True
                '        Else
                '            '' Dont Open '' DO Nothing
                '            Exit Sub
                '        End If
                '    End If
                'End If
                '''' <><><> Record Level Locking <><><><> 

                'If Not IsNothing(ofrmHistroy) Then
                '    ofrmHistroy.Dispose()
                '    ofrmHistroy = Nothing
                'End If
                Dim ofrmHistroy As frmHistory
                'Dim frm As frmHistory(CLng(.Item(.RowSel, 0)), CDate(.Item(.RowSel, 2)))
                '   ofrmHistroy = New frmHistory(_VisitID, _VisitDate, SelectedPatientId)
                ofrmHistroy = frmHistory.GetInstance(_VisitID, _VisitDate, SelectedPatientId, blnRecordLock)
                If IsNothing(ofrmHistroy) = True Then
                    Exit Sub
                End If

                'ShowHideMainMenu(False, False)
                'pnlMainToolBar.Visible = False
                If (frmHistory.IsOpen = False) Then
                    ofrmHistroy.PopulatePatientHistory_Final()
                End If
                If ofrmHistroy.blncancel Then
                    ofrmHistroy.MdiParent = Me.MdiParent
                    ofrmHistroy.WindowState = FormWindowState.Maximized
                    ofrmHistroy.Show()
                Else
                    ofrmHistroy.Dispose()
                    ofrmHistroy = Nothing
                End If

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub _PatientSynopsisUC_ShowMedicationForm(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _PatientSynopsisUC.ShowMedicationForm

        Dim ptPoint As Point = New Point(e.X, e.Y)
        Try
            Dim dtdatereceived As DateTime
            Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = _PatientSynopsisUC.C1MedicationDetails.HitTest(ptPoint)
            Dim selectedRowNo As Integer = _PatientSynopsisUC.C1MedicationDetails.HitTest(e.X, e.Y).Row

            If selectedRowNo > 0 Then 'AS WE ALLOW RESIZING ON THE COLUMN OF MEDICATION C1, selectedRowNo >= 0 CHANGES TO selectedRowNo > 0 
                dtdatereceived = CType(_PatientSynopsisUC.C1MedicationDetails.GetData(selectedRowNo, 0), DateTime)
                '' Medication Date
                Dim nVisitID As Long = GenerateVisitID(dtdatereceived, SelectedPatientId)
                Dim frmRxMeds As frmPrescription
                frmRxMeds = frmPrescription.GetInstance(nVisitID, SelectedPatientId)

                If IsNothing(frmRxMeds) Then
                    Exit Sub
                End If

                If frmPrescription.IsOpen = False Then
                    frmRxMeds.ShowMedication()
                End If
                If frmRxMeds.blncancel = True Then
                    With frmRxMeds
                        .WindowState = FormWindowState.Maximized
                        .MdiParent = Me.MdiParent
                        .Show()
                    End With
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(ptPoint) Then
                ptPoint = Nothing
            End If

        End Try
    End Sub

    Private Sub _PatientSynopsisUC_ShowProblemListForm(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _PatientSynopsisUC.ShowProblemListForm
        Try

            '' <><><> Record Level Locking <><><><> 
            Dim blnRecordLock As Boolean = False
            If gblnRecordLocking = True Then
                Dim mydt As mytable
                mydt = Scan_n_Lock_Transaction(TrnType.ProblemList, SelectedPatientId, 0, Now)
                If mydt.Code <> gstrLoginName OrElse mydt.Description <> gstrClientMachineName Then
                    If MessageBox.Show("This patients problem lists are being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot Modify it now. Do you want to open it?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        blnRecordLock = True
                    Else
                        mydt.Dispose()
                        mydt = Nothing
                        Exit Sub
                    End If
                End If
                mydt.Dispose()
                mydt = Nothing 'Disposed By Mitesh
            End If

            If _PatientSynopsisUC.c1ProblemList.Rows.Count > 1 Then
                Dim _problemID As Int64 = CType(_PatientSynopsisUC.c1ProblemList.GetData(_PatientSynopsisUC.c1ProblemList.Row, 0), Int64)    'ProblemID  

                Dim _visitID As Int64

                '04-Dec-13 Aniket: Resolving Bug #60883: 
                If _PatientSynopsisUC.c1ProblemList.GetData(_PatientSynopsisUC.c1ProblemList.Row, 5) <> "" Then
                    _visitID = CType(_PatientSynopsisUC.c1ProblemList.GetData(_PatientSynopsisUC.c1ProblemList.Row, 5), Int64)      'VisitID  
                End If



                ofrmProblems = frmProblemList.GetInstance(_problemID, _visitID, SelectedPatientId, blnRecordLock) '(_visitID, _VisitDate, blnRecordLock, _RecordLock)
                If IsNothing(ofrmProblems) = True Then
                    Exit Sub
                End If
                Try
                    RemoveHandler ofrmProblems.FormClosed, AddressOf ProblemListClosed
                Catch ex As Exception

                End Try
                AddHandler ofrmProblems.FormClosed, AddressOf ProblemListClosed
                '' chetan added on 28 oct 2010

                '   ofrmProblems = New frmProblemList(_problemID, _visitID, SelectedPatientId, blnRecordLock) chetan added on 28 oct 2010
                ofrmProblems.MdiParent = Me.MdiParent
                ''Sanjog Added on 2011 feb 28 to visible the Exam button in Problem list form
                frmProblemList.blnRxMedFromExam = False
                frmProblemList.blnOpenFromExam = False
                ''Sanjog Added on 2011 feb 28 to visible the Exam button in Problem list form
                ofrmProblems.WindowState = FormWindowState.Maximized
                ofrmProblems.ShowMessageForPendingReconciliation()
                ofrmProblems.Show()

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub _PatientSynopsisUC_ShowRadiologyForm(ByVal visitId As Long, ByVal orderdate As String) Handles _PatientSynopsisUC.ShowRadiologyForm
        Try

            '''''<><><><><> Check Patient Status <><><><><><>''''                        
            'If CheckPatientStatus(SelectedPatientId) = False Then
            '    Exit Sub
            'End If
            If MainMenu.IsAccess(False, SelectedPatientId) = False Then
                Exit Sub
            End If
            '''''<><><><><> Check Patient Status <><><><><><>''''

            '' SUDHIR 20090521 '' CHECK PROVIDER ''
            If gblnProviderDisable = True Then
                If ShowAssociateProvider(SelectedPatientId, Me) = True Then ''Replace gnPatientID
                    CType(Me.ParentForm, MainMenu).oPatientListControl.FillPatients()
                End If
            End If
            '' END SUDHIR

            Dim nVisitId As Long
            Dim VisitDate As String

            'If IsNumeric(_PatientSynopsisUC.C1OrderDetails.GetData(_PatientSynopsisUC.C1OrderDetails.Row, 0)) = False Then
            '    Exit Sub
            'End If

            nVisitId = visitId 'CType(_PatientSynopsisUC.C1OrderDetails.GetData(_PatientSynopsisUC.C1OrderDetails.Row, 0), Int64)
            VisitDate = orderdate 'CType(_PatientSynopsisUC.C1OrderDetails.GetData(_PatientSynopsisUC.C1OrderDetails.Row, 1), String)

            ' '' <><><> Record Level Locking <><><><>                         
            Dim blnRecordLock As Boolean = False
            If gblnRecordLocking = True Then
                Dim mydt As mytable
                mydt = Scan_n_Lock_Transaction(TrnType.Radiology, SelectedPatientId, nVisitId, VisitDate)
                If mydt.Code <> gstrLoginName OrElse mydt.Description <> gstrClientMachineName Then
                    If MessageBox.Show("This Radiology Order is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot modify it now. Do you want to open it?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        blnRecordLock = True
                    Else
                        mydt.Dispose()
                        mydt = Nothing
                        Exit Sub
                    End If
                End If
                mydt.Dispose()
                mydt = Nothing
            End If
            ' '' <><><> Record Level Locking <><><><> 

            'Create instance of Class to get message User                         
            'ofrmOrders = New frm_LM_Orders(nVisitId, VisitDate, SelectedPatientId)
            Dim ofrmOrders As frm_LM_Orders
            ofrmOrders = frm_LM_Orders.GetInstance(nVisitId, VisitDate, SelectedPatientId, 1, blnRecordLock)
            If IsNothing(ofrmOrders) = True Then
                Exit Sub
            End If

            'ShowHideMainMenu(False, False)
            'pnlMainToolBar.Visible = False

            ofrmOrders.MdiParent = Me.MdiParent
            ofrmOrders.WindowState = FormWindowState.Maximized
            ofrmOrders.Show()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CreateOrders, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub



    Private Sub _PatientSynopsisUC_ViewDMS() Handles _PatientSynopsisUC.ViewDMS

        Try
            If (_PatientSynopsisUC.C1PatientDMS.RowSel) > 0 Then
                If _PatientSynopsisUC.C1PatientDMS.GetData(_PatientSynopsisUC.C1PatientDMS.RowSel, COL_CAT_COLTYPE) = enumColType.Document Then

                    '' SUDHIR 20090521 '' CHECK PROVIDER ''
                    If gblnProviderDisable = True Then
                        If ShowAssociateProvider(SelectedPatientId, Me) = False Then ''Replace gnPatientID
                            Exit Sub
                        Else
                            CType(Me.ParentForm, MainMenu).oPatientListControl.FillPatients()
                        End If
                    End If
                    '' END SUDHIR

                    Dim _ContID As Long = 0
                    Dim _DocID As Long = 0
                    Dim _Year As String = ""
                    Try
                        _DocID = Convert.ToInt64(_PatientSynopsisUC.C1PatientDMS.GetData(_PatientSynopsisUC.C1PatientDMS.RowSel, COL_D_CAT_FILENAME).ToString())
                        _ContID = Convert.ToInt64(_PatientSynopsisUC.C1PatientDMS.GetData(_PatientSynopsisUC.C1PatientDMS.RowSel, COL_D_CAT_CONTAINER).ToString())
                        _Year = _PatientSynopsisUC.C1PatientDMS.GetData(_PatientSynopsisUC.C1PatientDMS.RowSel, COL_D_CAT_YEAR).ToString()
                    Catch ex As Exception

                    End Try

                    If _DocID > 0 And _Year <> "" Then
                        'Dim oShowDocument As New gloEDocument.gloEDocumentManagement
                        'oShowDocument.ShowEDocument(gnPatientID, True, _Year, _DocID, True, Me)
                        'oShowDocument.Dispose()
                        'ShowPatientDetails()
                        If oEDocumentV3 Is Nothing Then
                            oEDocumentV3 = New gloEDocumentV3.gloEDocV3Management()
                        End If

                        ''Replace gnPatientID
                        ''Added by Mayuri:20121205-Split Screen functionality
                        oEDocumentV3.oPatientExam = New clsPatientExams

                        oEDocumentV3.oPatientMessages = New clsMessage
                        oEDocumentV3.oPatientLetters = New clsPatientLetters
                        oEDocumentV3.oNurseNotes = New clsNurseNotes
                        oEDocumentV3.oHistory = New clsPatientHistory
                        oEDocumentV3.oLabs = New clsLabs
                        oEDocumentV3.oDMS = New gloEDocumentV3.eDocManager.eDocGetList()
                        oEDocumentV3.oRxmed = New clsPatientDetails
                        oEDocumentV3.oOrders = New clsPatientDetails
                        oEDocumentV3.oProblemList = New clsPatientProblemList

                        oEDocumentV3.oCriteria = New DocCriteria
                        oEDocumentV3.oWord = New clsWordDocument
                        ''End 20121205
                        Dim isItDialog As Boolean = oEDocumentV3.ShowEDocument(SelectedPatientId, gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ViewDocumentForExternalModule, Me.MdiParent, gloEDocumentV3.Enumeration.enum_OpenExternalSource.DashBoard, _DocID)
                        If (isItDialog = True) Then
                            If Not oEDocumentV3 Is Nothing Then
                                If (IsNothing(oEDocumentV3.oPatientExam) = False) Then
                                    DirectCast(oEDocumentV3.oPatientExam, clsPatientExams).Dispose()
                                    oEDocumentV3.oPatientExam = Nothing
                                End If
                                If (IsNothing(oEDocumentV3.oPatientMessages) = False) Then
                                    DirectCast(oEDocumentV3.oPatientMessages, clsMessage).Dispose()
                                    oEDocumentV3.oPatientMessages = Nothing
                                End If
                                If (IsNothing(oEDocumentV3.oPatientLetters) = False) Then
                                    DirectCast(oEDocumentV3.oPatientLetters, clsPatientLetters).Dispose()
                                    oEDocumentV3.oPatientLetters = Nothing
                                End If
                                If (IsNothing(oEDocumentV3.oNurseNotes) = False) Then
                                    DirectCast(oEDocumentV3.oNurseNotes, clsNurseNotes).Dispose()
                                    oEDocumentV3.oNurseNotes = Nothing
                                End If
                                If (IsNothing(oEDocumentV3.oHistory) = False) Then
                                    DirectCast(oEDocumentV3.oHistory, clsPatientHistory).Dispose()
                                    oEDocumentV3.oHistory = Nothing
                                End If
                                If (IsNothing(oEDocumentV3.oLabs) = False) Then
                                    DirectCast(oEDocumentV3.oLabs, clsLabs).Dispose()
                                    oEDocumentV3.oLabs = Nothing
                                End If
                                If (IsNothing(oEDocumentV3.oDMS) = False) Then
                                    DirectCast(oEDocumentV3.oDMS, gloEDocumentV3.eDocManager.eDocGetList).Dispose()
                                    oEDocumentV3.oDMS = Nothing
                                End If
                                If (IsNothing(oEDocumentV3.oRxmed) = False) Then
                                    DirectCast(oEDocumentV3.oRxmed, clsPatientDetails).Dispose()
                                    oEDocumentV3.oRxmed = Nothing
                                End If
                                If (IsNothing(oEDocumentV3.oOrders) = False) Then
                                    DirectCast(oEDocumentV3.oOrders, clsPatientDetails).Dispose()
                                    oEDocumentV3.oOrders = Nothing
                                End If
                                If (IsNothing(oEDocumentV3.oProblemList) = False) Then
                                    DirectCast(oEDocumentV3.oProblemList, clsPatientProblemList).Dispose()
                                    oEDocumentV3.oProblemList = Nothing
                                End If

                                If (IsNothing(oEDocumentV3.oCriteria) = False) Then
                                    DirectCast(oEDocumentV3.oCriteria, DocCriteria).Dispose()
                                    oEDocumentV3.oCriteria = Nothing
                                End If
                                oEDocumentV3.Dispose()
                                oEDocumentV3 = Nothing
                            End If
                        End If

                        'oEDocumentV3.Dispose()
                        'ShowPatientDetails()
                    End If
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub oEDocumentV3_EvnRefreshDocuments() Handles oEDocumentV3.EvnRefreshDocuments
        'if form is disposed then dont call refresh method
        'bugID :2668
        If Me.Visible = True Then
            _PatientSynopsisUC.Fill_PatientSacnedDocuments(SelectedPatientId, False)  ''Replace gnPatientID
        End If
    End Sub

    Private Sub _PatientSynopsisUC_ViewMedications(ByVal PatientId As Long, ByVal VisitID As Long) Handles _PatientSynopsisUC.ViewMedications
        Try

            'Dim frm As New frmMedication(VisitID)
            'frm.ShowMedication()
            'If frm.blncancel = True Then
            '    frm.MdiParent = Me.MdiParent
            '    frm.WindowState = FormWindowState.Maximized
            '    frm.Show()
            'End If

            If Not IsNothing(frmRxMeds) Then
                Try
                    RemoveHandler frmRxMeds.FormClosed, AddressOf frmPrecription_Closed
                Catch ex As Exception

                End Try
                If (IsNothing(frmRxMeds) = False) Then
                    frmRxMeds.Close()
                End If
                If (IsNothing(frmRxMeds) = False) Then
                    frmRxMeds.Dispose()
                    frmRxMeds = Nothing
                End If

            End If
            frmRxMeds = frmPrescription.GetInstance(VisitID, PatientId)
            If IsNothing(frmRxMeds) Then
                Exit Sub
            End If
            Try
                RemoveHandler frmRxMeds.FormClosed, AddressOf frmPrecription_Closed
            Catch ex As Exception

            End Try
            AddHandler frmRxMeds.FormClosed, AddressOf frmPrecription_Closed

            If frmPrescription.IsOpen = False Then
                frmRxMeds.ShowMedication()
            End If
            If frmRxMeds.blncancel = True Then
                With frmRxMeds
                    .WindowState = FormWindowState.Maximized
                    .MdiParent = Me.MdiParent
                    .Show()
                End With
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub _PatientSynopsisUC_ViewPatientHistory(ByVal VisitId As Long, ByVal Visitdate As Date) Handles _PatientSynopsisUC.ViewPatientHistory
        Try

            Dim blnRecordLock As Boolean = False
            'If gblnRecordLocking = True Then
            '    Dim mydt As New mytable
            '    mydt = Scan_n_Lock_Transaction(TrnType.PatientHistory, SelectedPatientId, VisitId, Visitdate)
            '    'If mydt.Code <> gstrLoginName Or mydt.Description <> gstrClientMachineName Then
            '    If mydt.Description <> gstrClientMachineName Then
            '        If MessageBox.Show("This Patient History is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot modify it now.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            '            '' Open in Lock Mode
            '            blnRecordLock = True
            '        Else
            '            '' Dont Open '' DO Nothing
            '            Exit Sub
            '        End If
            '    End If
            'End If
            '''' <><><> Record Level Locking <><><><> 

            'Dim frm As frmHistory(CLng(.Item(.RowSel, 0)), CDate(.Item(.RowSel, 2)))
            Dim frm As frmHistory
            ''frm = frmHistory.GetInstance(VisitId, Visitdate, _PatientID, blnRecordLock)
            frm = frmHistory.GetInstance(VisitId, Visitdate, SelectedPatientId, blnRecordLock)
            If IsNothing(frm) = True Then
                Exit Sub
            End If
            frm.myCallerSynopsis = Me
            Try
                RemoveHandler frm.FormClosed, AddressOf ofrmHistroy_FormClosed
            Catch ex As Exception

            End Try
            AddHandler frm.FormClosed, AddressOf ofrmHistroy_FormClosed


            'ShowHideMainMenu(False, False)
            'pnlMainToolBar.Visible = False
            If (frmHistory.IsOpen = False) Then
                frm.PopulatePatientHistory_Final()
            End If

            If frm.blncancel Then
                frm.MdiParent = Me.MdiParent
                frm.WindowState = FormWindowState.Maximized
                frm.Show()
            Else
                frm.Dispose()
                frm = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub
    Private Sub _PatientSynopsisUC_ViewProblemList(ByVal PatientID As Long, ByVal ProblemId As Long, ByVal VisitID As Long) Handles _PatientSynopsisUC.ViewProblemList
        Try

            '' <><><> Record Level Locking <><><><> 
            Dim blnRecordLock As Boolean = False
            If gblnRecordLocking = True Then
                Dim mydt As mytable
                mydt = Scan_n_Lock_Transaction(TrnType.ProblemList, SelectedPatientId, 0, Now)
                If mydt.Code <> gstrLoginName OrElse mydt.Description <> gstrClientMachineName Then
                    If MessageBox.Show("This patients problem lists are being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot Modify it now. Do you want to open it?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        blnRecordLock = True
                    Else
                        mydt.Dispose()
                        mydt = Nothing

                        Exit Sub
                    End If
                End If
                mydt.Dispose()
                mydt = Nothing
            End If


            ofrmProblems = frmProblemList.GetInstance(ProblemId, VisitID, SelectedPatientId, blnRecordLock) '(_visitID, _VisitDate, blnRecordLock, _RecordLock)
            If IsNothing(ofrmProblems) = True Then
                Exit Sub
            End If
            '' chetan added on 19-nov-2010


            '   ofrmProblems = New frmProblemList(_problemID, _visitID, SelectedPatientId, blnRecordLock) chetan added on 28 oct 2010
            ofrmProblems.MdiParent = Me.MdiParent
            ''Sanjog Added on 2011 feb 28 to visible the Exam button in Problem list form
            frmProblemList.blnRxMedFromExam = False
            frmProblemList.blnOpenFromExam = False
            ''Sanjog Added on 2011 feb 28 to visible the Exam button in Problem list form
            ofrmProblems.WindowState = FormWindowState.Maximized
            ofrmProblems.ShowMessageForPendingReconciliation()
            ofrmProblems.Show()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub
    'CHANGE THE PARAMETER FOR THE FUCNTION FOR CHECKING THE gblnICD9Driven SETTING OF ADMIN
    Private Sub _PatientSynopsisUC_ViewProcedures(ByVal ExamId As Long, ByVal VisitId As Long, ByVal dtDOS As DateTime, ByVal ExamName As String, ByVal PatientId As Int64) Handles _PatientSynopsisUC.ViewProcedures
        Try
            If Not IsNothing(ofrmDiagnosis) Then
                Try
                    ofrmDiagnosis.Dispose()
                    ofrmDiagnosis = Nothing
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ofrmDiagnosis = Nothing
                End Try

            End If
            'CHECK THE gblnICD9Driven SETTING OF ADMIN
            If (gblnICD9Driven) Then
                Dim PS_ActiveWindow As IntPtr = gloWord.WordDialogBoxBackgroundCloser.GetForegroundWindow()
                ofrmDiagnosis = New frm_Diagnosis(VisitId, ExamId, SelectedPatientId, , , PS_ActiveWindow)

                ofrmDiagnosis.BringToFront()
                ' ofrmDiagnosis.MdiParent = Me.MdiParent
                'ofrmDiagnosis.StartPosition = FormWindowState.Maximized '
                ofrmDiagnosis.StartPosition = FormStartPosition.CenterParent
                ofrmDiagnosis.ShowDialog(IIf(IsNothing(ofrmDiagnosis.Parent), Me, ofrmDiagnosis.Parent))
                ofrmDiagnosis.Dispose()
                ofrmDiagnosis = Nothing
                'ofrmDiagnosis.Show()
            Else
                Dim oTreatment As New frm_Treatment(ExamId, VisitId, dtDOS, ExamName, SelectedPatientId)
                'Call TurnOffMicrophone()
                oTreatment.ShowDialog(IIf(IsNothing(oTreatment.Parent), Me, oTreatment.Parent))
                oTreatment.Dispose()
                oTreatment = Nothing

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub
    Private Sub _PatientSynopsisUC_ViewRadiology(ByVal VisitId As Long, ByVal Visitdate As Date) Handles _PatientSynopsisUC.ViewRadiology
        Try

            '''''<><><><><> Check Patient Status <><><><><><>''''                        
            'If CheckPatientStatus(SelectedPatientId) = False Then
            '    Exit Sub
            'End If

            If MainMenu.IsAccess(False, SelectedPatientId) = False Then
                Exit Sub
            End If
            '''''<><><><><> Check Patient Status <><><><><><>''''

            '' SUDHIR 20090521 '' CHECK PROVIDER ''
            If gblnProviderDisable = True Then
                If ShowAssociateProvider(SelectedPatientId, Me) = True Then  ''Replace gnPatientID
                    CType(Me.ParentForm, MainMenu).oPatientListControl.FillPatients()
                End If
            End If
            '' END SUDHIR

            ' '' <><><> Record Level Locking <><><><>                         
            Dim blnRecordLock As Boolean = False
            If gblnRecordLocking = True Then
                Dim mydt As mytable
                mydt = Scan_n_Lock_Transaction(TrnType.Radiology, SelectedPatientId, VisitId, Visitdate)
                If mydt.Code <> gstrLoginName OrElse mydt.Description <> gstrClientMachineName Then
                    If MessageBox.Show("This Radiology Order is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot modify it now. Do you want to open it?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        blnRecordLock = True
                    Else
                        mydt.Dispose()
                        mydt = Nothing
                        Exit Sub
                    End If
                End If
                mydt.Dispose()
                mydt = Nothing
            End If
            ' '' <><><> Record Level Locking <><><><> 

            'Create instance of Class to get message User                         
            Dim frm As frm_LM_Orders
            frm = frm_LM_Orders.GetInstance(VisitId, Visitdate, SelectedPatientId, 1, blnRecordLock)
            If IsNothing(frm) = True Then
                Exit Sub
            End If

            'ShowHideMainMenu(False, False)
            'pnlMainToolBar.Visible = False

            frm.MdiParent = Me.MdiParent
            frm.WindowState = FormWindowState.Maximized
            frm.Show()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub _PatientSynopsisUC_ViewStressTest(ByVal PatientId As Long, ByVal VisitID As Long, ByVal DateOfStudy As Date, ByVal ExamID As Long, ByVal ClinicID As Long) Handles _PatientSynopsisUC.ViewStressTest
        Try
            '''''<><><><><> Check Patient Status <><><><><><>''''                        
            'If CheckPatientStatus(SelectedPatientId) = False Then
            '    Exit Sub
            'End If
            If MainMenu.IsAccess(False, SelectedPatientId) = False Then
                Exit Sub
            End If
            '''''<><><><><> Check Patient Status <><><><><><>''''

            '' SUDHIR 20090521 '' CHECK PROVIDER ''
            If gblnProviderDisable = True Then
                If ShowAssociateProvider(SelectedPatientId, Me) = True Then
                    CType(Me.ParentForm, MainMenu).oPatientListControl.FillPatients()
                End If
            End If
            '' END SUDHIR

            'Create instance of Class to get message User                         
            Dim frm As New frmCV_StressTests(PatientId, VisitID, DateOfStudy, ExamID, ClinicID)

            AddHandler frm.FormClosed, AddressOf OnCV_StressTest_Closed

            If IsNothing(frm) = True Then
                Exit Sub
            End If

            'ShowHideMainMenu(False, False)
            'pnlMainToolBar.Visible = False

            'frm.MdiParent = Me.MdiParent
            'frm.WindowState = FormWindowState.Maximized
            frm.StartPosition = FormStartPosition.CenterScreen
            frm.ShowInTaskbar = False
            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))

            If Not IsNothing(frm) Then   'Disposed by mitesh
                Try
                    RemoveHandler frm.FormClosed, AddressOf OnCV_StressTest_Closed
                Catch ex As Exception

                End Try
                frm.Close()
                If (IsNothing(frm) = False) Then
                    frm.Dispose()
                    frm = Nothing
                End If

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub OnCV_StressTest_Closed(ByVal sender As Object, ByVal e As FormClosedEventArgs)
        Dim frm As frmCV_StressTests = Nothing

        Try
            frm = DirectCast(sender, frmCV_StressTests)
        Catch ex As Exception

        End Try
        Try
            If (IsNothing(frm) = False) Then
                RemoveHandler frm.FormClosed, AddressOf OnCV_StressTest_Closed
            End If
            If (IsNothing(frm) = False) Then
                frm.Close()
            End If
            If (IsNothing(frm) = False) Then
                frm.Dispose()
                frm = Nothing
            End If

        Catch ex As Exception

        End Try
        If IsNothing(_PatientSynopsisUC) = False Then

            If _PatientSynopsisUC.IsDisposed = False Then
                Dim dsdata As DataSet
                Dim c2 As ClsPatientSynopsis = New ClsPatientSynopsis
                dsdata = c2.PopulateSynopsisData(_PatientID, "Imaging")
                _PatientSynopsisUC.PopulateImaging(dsdata.Tables("Imaging"))
                If Not IsNothing(dsdata) Then
                    dsdata.Dispose()
                    dsdata = Nothing
                End If
                If Not IsNothing(c2) Then
                    c2.Dispose()
                    c2 = Nothing
                End If
            End If

        End If
    End Sub
    Private Sub OnCV_Echocardiogram_Closed(ByVal sender As Object, ByVal e As FormClosedEventArgs)
        Dim frm As frmCV_VWEChocardiogram = Nothing

        Try
            frm = DirectCast(sender, frmCV_VWEChocardiogram)
        Catch ex As Exception

        End Try
        Try
            If (IsNothing(frm) = False) Then
                RemoveHandler frm.FormClosed, AddressOf OnCV_Echocardiogram_Closed
            End If
            If (IsNothing(frm) = False) Then
                frm.Close()
            End If
            If (IsNothing(frm) = False) Then
                frm.Dispose()
                frm = Nothing
            End If

        Catch ex As Exception

        End Try
        Try
            If IsNothing(_PatientSynopsisUC) = False Then


                If _PatientSynopsisUC.IsDisposed = False Then
                    Dim dsdata As DataSet
                    Dim c2 As ClsPatientSynopsis = New ClsPatientSynopsis
                    dsdata = c2.PopulateSynopsisData(_PatientID, "Imaging")
                    _PatientSynopsisUC.PopulateImaging(dsdata.Tables("Imaging"))
                    If Not IsNothing(dsdata) Then
                        dsdata.Dispose()
                        dsdata = Nothing
                    End If
                    If Not IsNothing(c2) Then
                        c2.Dispose()
                        c2 = Nothing
                    End If
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub _PatientSynopsisUC_ViewCardiologyDevice(ByVal PatientId As Long, ByVal ExamID As Long, ByVal VisitID As Long) Handles _PatientSynopsisUC.ViewCardiologyDevice
        Try
            '''''<><><><><> Check Patient Status <><><><><><>''''                        
            'If CheckPatientStatus(SelectedPatientId) = False Then
            '    Exit Sub
            'End If


            If MainMenu.IsAccess(False, SelectedPatientId) = False Then
                Exit Sub
            End If
            '''''<><><><><> Check Patient Status <><><><><><>''''

            '' SUDHIR 20090521 '' CHECK PROVIDER ''
            If gblnProviderDisable = True Then
                If ShowAssociateProvider(SelectedPatientId, Me) = True Then   ''Replace gnPatientID
                    CType(Me.ParentForm, MainMenu).oPatientListControl.FillPatients()
                End If
            End If
            '' END SUDHIR

            'Create instance of Class to get message User      

            '''''''' commented as on 20101020 - need to discuss for new implant device form logic - Ujwala Atre
            '' '' '' ''Dim frm As New frmCardiologyDevice(PatientId, ExamID, VisitID)
            '' '' '' ''AddHandler frm.FormClosed, AddressOf On_CardiologyDevice_Closed

            '' '' '' ''If IsNothing(frm) = True Then
            '' '' '' ''    Exit Sub
            '' '' '' ''End If

            'ShowHideMainMenu(False, False)
            'pnlMainToolBar.Visible = False

            '' '' ''frm.MdiParent = Me.MdiParent
            '' '' ''frm.WindowState = FormWindowState.Maximized
            '' '' ''frm.Show()
            '''''''' commented as on 20101020 - need to discuss for new implant device form logic - Ujwala Atre

        Catch ex As Exception

        End Try
    End Sub

    Private Sub On_CardiologyDevice_Closed(ByVal sender As Object, ByVal e As FormClosedEventArgs)
        Dim frm As frmCV_VWImplantDevice = Nothing

        Try
            frm = DirectCast(sender, frmCV_VWImplantDevice)
        Catch ex As Exception

        End Try
        Try
            If (IsNothing(frm) = False) Then
                RemoveHandler frm.FormClosed, AddressOf On_CardiologyDevice_Closed
            End If
            If (IsNothing(frm) = False) Then
                frm.Close()
            End If
            If (IsNothing(frm) = False) Then
                frm.Dispose()
                frm = Nothing
            End If

        Catch ex As Exception

        End Try
        Try

            '05-Jan-15 Aniket: Resolving Bug #78236: gloEMR: Implant Device exception- Application gives exception 
            If IsNothing(_PatientSynopsisUC) = False Then
                If _PatientSynopsisUC.IsDisposed = False Then
                    Dim dsdata As DataSet
                    Dim c2 As ClsPatientSynopsis = New ClsPatientSynopsis
                    dsdata = c2.PopulateSynopsisData(_PatientID, "Implant")
                    _PatientSynopsisUC.PopulateImplant(dsdata.Tables("Implant"))
                    If Not IsNothing(dsdata) Then
                        dsdata.Dispose()
                        dsdata = Nothing
                    End If
                    If Not IsNothing(c2) Then
                        c2.Dispose()
                        c2 = Nothing
                    End If
                    _PatientSynopsisUC.FillCardiologyDevice(SelectedPatientId)  ''Replace gnPatientID
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub



    Private Sub mnuDeleteTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDeleteTab.Click
        Try
            If otab.TabPages.Count = 1 Then
                Exit Sub
            End If
            If Not IsNothing(otab.SelectedTab) Then
                otab.TabPages.Remove(otab.SelectedTab)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tstrip_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tstrip.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Select Patient"

                    ''pnlglots_PatientListView.Visible = True

                    pnlPatientListView.Visible = True
                    ''oPatientListControl.pnlToolstrip.Visible = True

                    '' If Patient List Control alredy Exits Then Remove it
                    ''If IsNothing(octlPatientList) = False Then
                    ''    If pnlPatientListView.Contains(octlPatientList) = True Then
                    ''        pnlPatientListView.Controls.Remove(octlPatientList)
                    ''    End If
                    ''End If

                    ' ''Declare Variable for connection string
                    ''octlPatientList = New gloListControl.gloListControl(GetConnectionString, gloListControlType.AllPatient, False, Me.Width)
                    ''octlPatientList.Dock = DockStyle.Fill
                    ''octlPatientList.ClinicID = 1
                    ''octlPatientList.ShowHeaderPanel(False)
                    ''octlPatientList.OpenControl()
                    ''
                    ''Added by Mayuri:20100212-Added PAtientListControl instead gloListControl
                    ''Case No:GLO2008-0001606-Allow user customization of fields displayed under patient search on dashboard
                    If IsNothing(oPatientListControl) = False Then
                        If pnlPatientListView.Contains(oPatientListControl) = True Then
                            pnlPatientListView.Controls.Remove(oPatientListControl)
                        End If
                        Try
                            RemoveHandler oPatientListControl.Grid_MouseDown, AddressOf oPatientListControl_Grid_MouseDown

                        Catch ex As Exception

                        End Try
                        oPatientListControl.Dispose()
                        oPatientListControl = Nothing
                    End If

                    'Declare Variable for connection string
                    oPatientListControl = New gloPatient.PatientListControl()
                    oPatientListControl.Dock = DockStyle.Fill

                    pnlPatientListView.Controls.Add(oPatientListControl)
                    pnlPatientListView.Dock = DockStyle.Top
                    AddHandler oPatientListControl.Grid_MouseDown, AddressOf oPatientListControl_Grid_MouseDown

                    ' oPatientListControl.ClinicID = 1
                    oPatientListControl.DatabaseConnection = GetConnectionString()
                    oPatientListControl.SelectedPatientID = _PatientID  ''Replace gnPatientID
                    oPatientListControl.FillPatients()
                    ''End code Added by Mayuri:20100212

                    Dim intControlHeight As Integer
                    intControlHeight = Me.Height - pnl_tlsp_Top.Height
                    pnlPatientListView.Height = intControlHeight / 2
                    'Add octlPatientList into pnlPatientListView
                    oPatientListControl.ShowOKCancel(True)

                Case "Stress Test"
                    'Show stress test
                    Dim ofrm As New frmCV_VwStressTest(SelectedPatientId)
                    AddHandler ofrm.FormClosed, AddressOf OnCV_StressTest_Closed
                    ofrm.MdiParent = Me.MdiParent
                    ofrm.Show()

                Case "Electro Physiology"
                    'show electro physiology
                    'Dim ofrm As New frmElectroPhsiology(SelectedPatientId, 0, mgnVisitID)
                    Dim ofrm As New frmCV_VWElectroPhysiology(SelectedPatientId)
                    ofrm.MdiParent = Me.MdiParent
                    ofrm.Show()
                Case "Implant Device"
                    'show cardio device
                    Dim ofrmCardio As New frmCV_VWImplantDevice(SelectedPatientId)
                    AddHandler ofrmCardio.FormClosed, AddressOf On_CardiologyDevice_Closed
                    ofrmCardio.MdiParent = Me.MdiParent
                    ofrmCardio.Show()

                Case "Ejection Fraction"

                    'show ejection fraction
                    Dim ofrm As New EjectionFraction(SelectedPatientId, 0, mgnVisitID)
                    AddHandler ofrm.FormClosed, AddressOf On_EjectionFraction_Closed

                    ofrm.MdiParent = Me.MdiParent
                    ofrm.Show()

                Case "Intervention"
                    'show Interventions
                    Dim ofrm As New frmCV_VWInterventions(SelectedPatientId)
                    ofrm.MdiParent = Me.MdiParent
                    ofrm.Show()
                Case "Risk"
                    'show Cardio Vascular Patient Risk form
                    Dim ofrm As New frmCV_PatientRisk(SelectedPatientId)
                    If ofrm.RiskFound Then
                        ofrm.MdiParent = Me.MdiParent
                        ofrm.Show()
                    Else
                        MessageBox.Show("No risk found for patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

                Case "Check In Patient"
                    ShowCheckedInPatients()

                Case "Previous"
                    PrevButton()
                Case "Next"
                    NextButton()
                Case "EchoCardio"
                    Dim ofrm As New frmCV_VWEChocardiogram(SelectedPatientId)
                    AddHandler ofrm.FormClosed, AddressOf OnCV_Echocardiogram_Closed
                    ofrm.MdiParent = Me.MdiParent
                    ofrm.Show()
                Case "Catheterization"
                    Dim ofrm As New frmCV_VWCatheterization(SelectedPatientId)
                    ofrm.MdiParent = Me.MdiParent
                    ofrm.Show()
                Case "ECG"
                    Dim ofrm As New frmCV_VWElectroCardiograms(SelectedPatientId)
                    ofrm.MdiParent = Me.MdiParent
                    ofrm.Show()
                Case "Spirometry"
                    'commented code by manoj jadhav on 20110817
                    'If isSpirometryDeviceActivated() Then 'code added to check if Spiro Device is Activated by RK on 20110527
                    '    Dim strgloDeviceConnection As String = String.Empty
                    '    strgloDeviceConnection = gloEmdeonInterface.Classes.clsSpiroGeneralModule.RetriveDeviceCon(gnClinicID, GetConnectionString())
                    '    If strgloDeviceConnection.Length > 0 Then
                    '        Dim ofrmSpiro As New gloEmdeonInterface.Forms.frmViewSpirometryTests(SelectedPatientId)
                    '        'ofrmSpiro.Vitalseventhandler+= += New ofrmSpiro.Vitalseventhandler(subOpenVitalsForm)
                    '        AddHandler ofrmSpiro.EvntVitalFormHandler, AddressOf OpenVitalsForm

                    '        ' code added by manoj 20110610
                    '        ' ofrmSpiro.ShowDialog(Me)
                    '        'ofrmSpiro.Dispose()
                    '        'ofrmSpiro = Nothing
                    '        'strgloDeviceConnection = String.Empty
                    '        ofrmSpiro.MdiParent = Me.MdiParent
                    '        ofrmSpiro.Show()
                    '        ' code Ended by manoj 20110610

                    '    Else
                    '        'MessageBox.Show("Configure device database.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '        MessageBox.Show("Unable to connect device database." & vbCrLf & "Configure at gloEMR Admin-->Settings-->Server Settings-->Device Database Settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '        strgloDeviceConnection = String.Empty
                    '    End If
                    'Else
                    '    MessageBox.Show("Spirometry device interface authentication failed, please check activation key.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'End If

                    'start code by manoj jadhav on 20110817 to fix event handler dispose bug
                    ' check if device key activation
                    If isSpirometryDeviceActivated() Then
                        'get gloEMR connectionstring
                        Dim gloEMRconnectionString As String = GetConnectionString()
                        ' get device connection string
                        Dim strgloDeviceConnection As String = gloEmdeonInterface.Classes.clsSpiroGeneralModule.RetriveDeviceCon(gnClinicID, gloEMRconnectionString)
                        ' check if device connection string is present
                        If strgloDeviceConnection.Length > 0 Then
                            Dim Odblayer As New gloDatabaseLayer.DBLayer(strgloDeviceConnection)
                            Try
                                ' if connection string is valid
                                If (Odblayer.CheckConnection()) Then
                                    'set race 
                                    gloEmdeonInterface.Classes.clsSpiroGeneralModule.bMultipleRace = mdlGeneral.globlnEnableMultipleRaceFeatures
                                    'create object of view spirometry form 
                                    Dim ofrmSpiro As New gloEmdeonInterface.Forms.frmViewSpirometryTests(SelectedPatientId, strgloDeviceConnection, gloEMRconnectionString)
                                    ' add handler to form for opening vital device screen 
                                    AddHandler ofrmSpiro.EvntVitalFormHandler, AddressOf OpenVitalsForm
                                    ' load form
                                    ofrmSpiro.MdiParent = Me.MdiParent
                                    ofrmSpiro.Show()
                                Else
                                    MessageBox.Show("Unable to connect device database now, Invalid server credentials.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            Catch ex As Exception
                                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Finally
                                If (Not Odblayer Is Nothing) Then
                                    Odblayer.Dispose()
                                    Odblayer = Nothing
                                End If
                            End Try
                        Else
                            MessageBox.Show("Device database is not configured." & vbCrLf & "Check Configuration at gloEMR Admin-->Settings-->Server Settings-->Device Database Settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                        strgloDeviceConnection = String.Empty
                        gloEMRconnectionString = String.Empty
                    Else
                        MessageBox.Show("Spirometry device interface authentication failed, please check activation key.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                    'end code by manoj jadhav on 20110817 to fix event handler dispose bug
            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' code added by RK to check if Spirometry Device is activated on 20110527
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function isSpirometryDeviceActivated() As Boolean
        Dim blnIsDeviceActivated As Boolean = False
        Dim _strSQL As String = String.Empty
        Dim sAUSName As String = String.Empty
        Dim sSpirometryDeviceKey As String = String.Empty
        'Dim sConnection As SqlConnection()
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Try

            oDB.Connect(False)
            _strSQL = "SELECT ISNULL(sExternalcode,'') FROM Clinic_MST"
            sAUSName = Convert.ToString(oDB.ExecuteScalar_Query(_strSQL))
            _strSQL = String.Empty
            _strSQL = "select ISNULL(sSettingsValue,'') from Settings WHERE sSettingsName='SPIROMETRYDEVICEKEY'"
            sSpirometryDeviceKey = Convert.ToString(oDB.ExecuteScalar_Query(_strSQL))
            oDB.Disconnect()

            Dim objEncr As New clsencryption()
            If Not objEncr.EncryptToBase64String((objEncr.EncryptToBase64String(String.Concat(sAUSName.ToLower(), "gL0@PPs2k9!8228"), "87654321")), mdlGeneral.constEncryptDecryptKey) = sSpirometryDeviceKey Then
                blnIsDeviceActivated = False
            Else
                blnIsDeviceActivated = True
            End If
            objEncr = Nothing
        Catch ex As Exception
            blnIsDeviceActivated = False
        Finally
            If Not IsNothing(oDB) Then 'Disposed by Mitesh
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
        Return blnIsDeviceActivated
    End Function
    'End of code added by RK

    Private Function GetVisitID() As Int64
        Try
            Dim visitid As Int64 = 0
            visitid = GenerateVisitID(Now.Date, SelectedPatientId)
            Return visitid
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Throw ex
        End Try
    End Function


    ''Sandip Darade 24 Feb 2009

    Private Sub GloUC_TransactionHistory_btnShowGraphClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GloUC_TransactionHistory1.btnShowGraphClick
        Dim dv As DataView = Nothing
        Dim dt_selectedResult As DataTable = Nothing
        Try


            '''' Get selected Result From Grid
            dt_selectedResult = GloUC_TransactionHistory1.SelectResult()

            '''' If DataTable is empty then exit from Procedure.
            'If dt_selectedResult Is Nothing Then
            If dt_selectedResult.Rows.Count = 0 Then
                dt_selectedResult.Dispose()
                dt_selectedResult = Nothing
                Exit Sub
            End If
            ' End If

            If String.IsNullOrEmpty(dt_selectedResult.Rows(dt_selectedResult.Rows.Count - 1)(0)) = True Then
                MessageBox.Show("Graph cannot be generated as no Collected Date is present.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            ''''Get Min and Max Value from DataTable


            dv = New DataView(dt_selectedResult)
            dv.Sort = "Value"

            Dim max As Integer = dv.Item(dv.Count - 1)("Value").ToString()
            Dim min As Integer = dv.Item(0)("Value").ToString() '' dv.Table.Rows.Count - 1)("Value")



            Dim dtSelectedResultToDate As DateTime = CType(dt_selectedResult.Rows(dt_selectedResult.Rows.Count - 1)(0), DateTime)


            ' lines for get the first results data and show it into the label as From-date
            Dim dtStartdate As DateTime
            dtStartdate = dt_selectedResult.Rows.Item(0)(0) ' Take from date for Display 
            dtStartdate = Format(dtStartdate, "MM/dd/yyyy")

            ' view the graphs for the provided values as a parameters provided
            Dim oGraphResult As New frmLab_GraphsResult(dtStartdate, dtSelectedResultToDate, 0, 0, SelectedPatientId, dt_selectedResult.Rows(0)(1), dt_selectedResult.Rows(0)(2), dt_selectedResult, , False, , min, max)
            ''Replace gnPatientID

            oGraphResult.MdiParent = Me.MdiParent
            oGraphResult.WindowState = FormWindowState.Maximized
            oGraphResult.ShowInTaskbar = False
            oGraphResult.Show()

            'Me.Close()
            Exit Sub
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ShowGraph, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If Not IsNothing(dt_selectedResult) Then   'Disposed by Mitesh
                dt_selectedResult.Dispose()
                dt_selectedResult = Nothing
            End If
            If Not IsNothing(dv) Then
                dv.Dispose()
                dv = Nothing
            End If
        End Try
    End Sub

    Public Sub frmRxMeds_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles frmRxMeds.FormClosed
        Try
            If Not IsNothing(_PatientSynopsisUC) Then
                'If _PatientSynopsisUC.tbSummary.SelectedTab.Text <> "Synopsis" Then
                _PatientSynopsisUC.dtPatientDetails = Nothing
                _PatientSynopsisUC.Fill_Medications()
                'Else
                Dim dsdata As DataSet
                Dim c2 As ClsPatientSynopsis = New ClsPatientSynopsis
                dsdata = c2.PopulateSynopsisData(_PatientID, "Medications")
                _PatientSynopsisUC.PopulateLatestMedications(dsdata.Tables("Medications"))
                If Not IsNothing(dsdata) Then
                    dsdata.Dispose()
                    dsdata = Nothing
                End If
                If Not IsNothing(c2) Then
                    c2.Dispose()
                    c2 = Nothing
                End If
                'End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub

    'for Problems form
    Private Sub ofrmProblems_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles ofrmProblems.FormClosed
        Try
            If Not IsNothing(_PatientSynopsisUC) Then
                If _PatientSynopsisUC.tbSummary.SelectedTab.Text = "Synopsis" Then
                    _PatientSynopsisUC.Fill_PastExams()
                End If
                _PatientSynopsisUC.dtProblemList = Nothing
                _PatientSynopsisUC.Fill_ProblemList()
                'Else
                Dim dsdata As DataSet
                Dim c2 As ClsPatientSynopsis = New ClsPatientSynopsis
                dsdata = c2.PopulateSynopsisData(_PatientID, "Problems")
                _PatientSynopsisUC.PopulateProblemList1(dsdata.Tables("Problems"))
                If Not IsNothing(dsdata) Then
                    dsdata.Dispose()
                    dsdata = Nothing
                End If
                If Not IsNothing(c2) Then
                    c2.Dispose()
                    c2 = Nothing
                End If
                'End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub ofrmDiagnosis_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles ofrmDiagnosis.FormClosed
        Try
            If Not IsNothing(_PatientSynopsisUC) Then
                'If _PatientSynopsisUC.tbSummary.SelectedTab.Text <> "Synopsis" Then
                _PatientSynopsisUC.FillICDCPTMOD(SelectedPatientId)
                'Else
                Dim dsdata As DataSet
                Dim c2 As ClsPatientSynopsis = New ClsPatientSynopsis
                dsdata = c2.PopulateSynopsisData(_PatientID, "Procedures")
                _PatientSynopsisUC.PopulateProcedures1(dsdata.Tables("Procedures"))
                If Not IsNothing(dsdata) Then
                    dsdata.Dispose()
                    dsdata = Nothing
                End If
                If Not IsNothing(c2) Then
                    c2.Dispose()
                    c2 = Nothing
                End If
                'End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub ofrmOrders_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles ofrmOrders.FormClosed
        Try
            If _PatientSynopsisUC.tbSummary.SelectedTab.Text <> "Synopsis" Then
                If Not IsNothing(_PatientSynopsisUC) Then
                    '_PatientSynopsisUC.FillCategoryTestGroups() '' COMMENT BY SUDHIR 20090601 '' 
                    _PatientSynopsisUC.dtOrder = Nothing
                    _PatientSynopsisUC.FillPatientOrders()
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub


    Private Sub ofrmHistroy_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles ofrmHistroy.FormClosed
        Dim frm As frmHistory = Nothing

        Try
            frm = DirectCast(sender, frmHistory)
        Catch ex As Exception

        End Try
        Try
            If (IsNothing(frm) = False) Then
                RemoveHandler frm.FormClosed, AddressOf ofrmHistroy_FormClosed
            End If
            If (IsNothing(frm) = False) Then
                frm.Close()
            End If
            If (IsNothing(frm) = False) Then
                frm.Dispose()
                frm = Nothing
            End If

        Catch ex As Exception

        End Try


        Try

            '05-May-15 Aniket: Resolving Bug #82983: EMR: Synopsis(PT history)- Search functionlity doe not work properly
            If Not IsNothing(_PatientSynopsisUC) Then

                ' _PatientSynopsisUC.dtHistory = Nothing
                ' _PatientSynopsisUC.Fill_PatientHistory()
                Dim dsdata As DataSet
                Dim c2 As ClsPatientSynopsis = New ClsPatientSynopsis
                dsdata = c2.PopulateSynopsisData(_PatientID, "History")
                If Not IsNothing(_PatientSynopsisUC.oSearchAllergiesCtl) Then
                    If _PatientSynopsisUC.oSearchAllergiesCtl.txtSearch.Text = "" Then
                        _PatientSynopsisUC.PopulateLatestHistory(dsdata.Tables("History"))
                    Else
                        _PatientSynopsisUC.oSearchAllergiesCtl.txtSearch_TextChanged(Nothing, Nothing)
                    End If
                Else
                    _PatientSynopsisUC.PopulateLatestHistory(dsdata.Tables("History"))
                End If
                If Not IsNothing(c2) Then
                    c2.Dispose()
                    c2 = Nothing
                End If
                If Not IsNothing(dsdata) Then
                    dsdata.Dispose()
                    dsdata = Nothing
                End If


            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub On_EjectionFraction_Closed(ByVal sender As Object, ByVal e As FormClosedEventArgs)
        Dim frm As EjectionFraction = Nothing

        Try
            frm = DirectCast(sender, EjectionFraction)
        Catch ex As Exception

        End Try
        Try
            If (IsNothing(frm) = False) Then
                RemoveHandler frm.FormClosed, AddressOf On_EjectionFraction_Closed
            End If
            If (IsNothing(frm) = False) Then
                frm.Close()
            End If
            If (IsNothing(frm) = False) Then
                frm.Dispose()
                frm = Nothing
            End If

        Catch ex As Exception

        End Try
        Try
            If IsNothing(_PatientSynopsisUC) = False Then

                If _PatientSynopsisUC.IsDisposed = False Then

                    _PatientSynopsisUC.FillEjectionFraction(SelectedPatientId)  ''Replace gnPatientID
                    _PatientSynopsisUC.dt_EF = Nothing
                    Dim dsdata As DataSet
                    Dim c2 As ClsPatientSynopsis = New ClsPatientSynopsis
                    dsdata = c2.PopulateSynopsisData(_PatientID, "Imaging")
                    _PatientSynopsisUC.PopulateImaging(dsdata.Tables("Imaging"))
                    If Not IsNothing(dsdata) Then
                        dsdata.Dispose()
                        dsdata = Nothing
                    End If
                    If Not IsNothing(c2) Then
                        c2.Dispose()
                        c2 = Nothing
                    End If
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub tstrip_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tstrip.MouseDown

    End Sub
    Private Sub InitializeToolStrip()
        tstrip.ConnectionString = GetConnectionString()
        tstrip.ModuleName = Me.Name
        tstrip.UserID = gnLoginID
        'tblHistory.ButtonsToHide.Add(tblNew.Name)
        'tblHistory.ButtonsToHide.Add(tsbtn_Export.Name)
        'tblHistory.ButtonsToHide.Add(tsbtn_Delete.Name)
    End Sub

    'Private Function oPatientListControl_Check_IsAccess(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs, ByVal _nPID As gloPatient.PatientListControl.PatientArgs) Handles oPatientListControl.Check_IsAccess
    '    '''''''''Added by Ujwala Atre for 'Lock Chart' Patients - as on 11122010
    '    If MainMenu.IsAccess(False, oPatientListControl.SelectedPatientID) = False Then
    '        Return False
    '    End If
    '    Return True
    '    '''''''''Added by Ujwala Atre for 'Lock Chart' Patients - as on 11122010
    'End Function
    ''Added by Mayuri:20100212-Added PAtientListControl instead gloListControl
    ''Case No:GLO2008-0001606-Allow user customization of fields displayed under patient search on dashboard
    Private Sub oPatientListControl_Grid_DoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles oPatientListControl.Grid_DoubleClick
        Dim dt As DataTable
        Try
            'If MainMenu.IsAccess(False, oPatientListControl.PatientID) = False Then
            '    Exit Sub
            'End If
            pnlPatientListView.Visible = False
            '' Check if The PAtient Tab Already Exist
            Dim IsExist As Boolean = False

            If oPatientListControl.SelectedPatientID <> 0 Then
                For i As Int16 = 0 To otab.TabPages.Count - 1
                    If (otab.TabPages.Item(i).Tag = oPatientListControl.SelectedPatientID) Then
                        IsExist = True
                        otab.SelectedTab = otab.TabPages.Item(i)
                        Exit For
                    End If
                Next

                If IsExist = False Then
                    If oPatientListControl.SelectedPatientID <> _PatientID Then ''Replace gnPatientID
                        'Add Tabpages to the tabcontrol
                        If (IsNothing(oPage) = False) Then
                            If (IsNothing(_PatientSynopsisUC) = False) Then
                                If (oPage.Controls.Contains(_PatientSynopsisUC)) Then
                                    oPage.Controls.Remove(_PatientSynopsisUC)

                                End If

                            End If
                        End If
                        If (IsNothing(_PatientSynopsisUC) = False) Then
                            Try
                                RemoveHandler _PatientSynopsisUC.ViewMedications, AddressOf _PatientSynopsisUC_ViewMedications
                                RemoveHandler _PatientSynopsisUC.ViewDMS, AddressOf _PatientSynopsisUC_ViewDMS
                                RemoveHandler _PatientSynopsisUC.ViewPatientHistory, AddressOf _PatientSynopsisUC_ViewPatientHistory
                                RemoveHandler _PatientSynopsisUC.ViewProblemList, AddressOf _PatientSynopsisUC_ViewProblemList
                                RemoveHandler _PatientSynopsisUC.ViewProcedures, AddressOf _PatientSynopsisUC_ViewProcedures
                                RemoveHandler _PatientSynopsisUC.ViewRadiology, AddressOf _PatientSynopsisUC_ViewRadiology
                                RemoveHandler _PatientSynopsisUC.OnLabsFlexDoubleClick, AddressOf _PatientSynopsisUC_OnLabsFlexDoubleClick
                                ''Commented on 20100812 by sanjog for History Form should open only one time
                                ''RemoveHandler _PatientSynopsisUC.ShowHistoryForm, AddressOf _PatientSynopsisUC_ShowHistoryForm
                                ''Commented on 20100812 by sanjog for History Form should open only one time
                                RemoveHandler _PatientSynopsisUC.ShowMedicationForm, AddressOf _PatientSynopsisUC_ShowMedicationForm
                                RemoveHandler _PatientSynopsisUC.ShowProblemListForm, AddressOf _PatientSynopsisUC_ShowProblemListForm
                                RemoveHandler _PatientSynopsisUC.ShowRadiologyForm, AddressOf _PatientSynopsisUC_ShowRadiologyForm
                                RemoveHandler _PatientSynopsisUC.ShowHistoryForm, AddressOf _PatientSynopsisUC_ShowHistoryForm

                            Catch ex As Exception

                            End Try
                            _PatientSynopsisUC.oMDI = Nothing
                            _PatientSynopsisUC.Dispose()
                            _PatientSynopsisUC = Nothing
                        End If
                        oPage = New TabPage
                        'oPage.Text = oPatientListControl.FirstName & " " & oPatientListControl.LastName
                        'ADDED BY SHUBHANGI 20110516
                        'PRD: PRD EMR 6010_0210 Patient Safety Patient Identification changes
                        oPage.Tag = oPatientListControl.SelectedPatientID
                        SelectedPatientId = oPatientListControl.SelectedPatientID
                        dt = gloPatient.gloPatient.GetPatientInfo(SelectedPatientId, GetConnectionString(), gstrMessageBoxCaption)
                        Dim PatientName As String = dt.Rows(0)("PatientName")
                        oPage.Text = PatientName
                        otab.TabPages.Add(oPage)
                        otab.SelectedTab = oPage

                        '' SUDHIR 20090529 '' DISPOSING ''
                        If (IsNothing(_PatientSynopsisUC) = False) Then
                            _PatientSynopsisUC.oMDI = Nothing
                            _PatientSynopsisUC.Dispose()
                            _PatientSynopsisUC = Nothing
                        End If

                        '' END SUDHIR ''
                        _PatientSynopsisUC = New gloUCPatientSynopsis(oPage.Tag)

                        'Set the PatientID from first tab to user control patientid
                        oPage.Controls.Add(_PatientSynopsisUC)
                        _PatientSynopsisUC.Dock = DockStyle.Fill

                        AddHandler _PatientSynopsisUC.ViewMedications, AddressOf _PatientSynopsisUC_ViewMedications
                        AddHandler _PatientSynopsisUC.ViewDMS, AddressOf _PatientSynopsisUC_ViewDMS
                        AddHandler _PatientSynopsisUC.ViewPatientHistory, AddressOf _PatientSynopsisUC_ViewPatientHistory
                        AddHandler _PatientSynopsisUC.ViewProblemList, AddressOf _PatientSynopsisUC_ViewProblemList
                        AddHandler _PatientSynopsisUC.ViewProcedures, AddressOf _PatientSynopsisUC_ViewProcedures
                        AddHandler _PatientSynopsisUC.ViewRadiology, AddressOf _PatientSynopsisUC_ViewRadiology
                        AddHandler _PatientSynopsisUC.OnLabsFlexDoubleClick, AddressOf _PatientSynopsisUC_OnLabsFlexDoubleClick
                        ''Commented on 20100812 by sanjog for History Form should open only one time
                        ''AddHandler _PatientSynopsisUC.ShowHistoryForm, AddressOf _PatientSynopsisUC_ShowHistoryForm
                        ''Commented on 20100812 by sanjog for History Form should open only one time
                        AddHandler _PatientSynopsisUC.ShowMedicationForm, AddressOf _PatientSynopsisUC_ShowMedicationForm
                        AddHandler _PatientSynopsisUC.ShowProblemListForm, AddressOf _PatientSynopsisUC_ShowProblemListForm
                        AddHandler _PatientSynopsisUC.ShowRadiologyForm, AddressOf _PatientSynopsisUC_ShowRadiologyForm
                        AddHandler _PatientSynopsisUC.ShowHistoryForm, AddressOf _PatientSynopsisUC_ShowHistoryForm

                        mgnVisitID = GetVisitID()
                        Try
                            Me.Text = "Patient Synopsis"
                            gloPatient.gloPatient.GetWindowTitle(Me, SelectedPatientId, GetConnectionString(), gstrMessageBoxCaption)
                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        End Try
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'Finally
            '    If IsNothing(dt) = False Then
            '        dt.Dispose()
            '        dt = Nothing
            '    End If

        End Try
    End Sub

    Private Sub oPatientListControl_Grid_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) ''Handles oPatientListControl.Grid_MouseDown

    End Sub

    Private Sub oPatientListControl_GridRowSelect_Click(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles oPatientListControl.GridRowSelect_Click
        ''For Fixed BugID 6680
        'If MainMenu.IsAccess(False, oPatientListControl.PatientID) = False Then
        '    Exit Sub
        'End If
        Dim oSecurity As New gloSecurity.gloSecurity(GetConnectionString())
        Try

            If oSecurity.isPatientLock(oPatientListControl.PatientID, True) = False Then
                If IsNothing(oSecurity) = False Then
                    oSecurity.Dispose()
                    oSecurity = Nothing
                End If
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If IsNothing(oSecurity) = False Then
                oSecurity.Dispose()
                oSecurity = Nothing
            End If
        End Try

        ''End
    End Sub


    Private Sub ts_btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    'Private Sub ts_btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    'pnlPatientListView.Visible = False
    '    'pnlglots_PatientListView.Visible = False
    'End Sub

    Private Sub oPatientListControl_ItemClosedClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles oPatientListControl.ItemClosedClick
        pnlPatientListView.Visible = False
        'pnlglots_PatientListView.Visible = False
    End Sub

    Public Sub New(ByVal PatientID As Long)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _PatientID = PatientID
    End Sub

    Private Sub otab_Selecting(ByVal sender As Object, ByVal e As System.Windows.Forms.TabControlCancelEventArgs) Handles otab.Selecting

    End Sub

    Private Sub oPatientListControl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles oPatientListControl.Load
        '''''''''''' Added by Ujwala Atre for 'Lock Chart' Patients - as on 11122010
        oPatientListControl.SelectedPatientID = 0
        oPatientListControl.ChkIsAccess = True
        '''''''''''' Added by Ujwala Atre for 'Lock Chart' Patients - as on 11122010
    End Sub



    Private Sub DisposeGlobal()
        If IsNothing(oPatientListControl) = False Then
            If pnlPatientListView.Contains(oPatientListControl) = True Then
                pnlPatientListView.Controls.Remove(oPatientListControl)
            End If
            Try
                RemoveHandler oPatientListControl.Grid_MouseDown, AddressOf oPatientListControl_Grid_MouseDown

            Catch ex As Exception

            End Try
            oPatientListControl.Dispose()
            oPatientListControl = Nothing
        End If
        If IsNothing(octlPatientList) = False Then
            If pnlPatientListView.Contains(octlPatientList) = True Then
                pnlPatientListView.Controls.Remove(octlPatientList)
            End If
            octlPatientList.Dispose()
            octlPatientList = Nothing
        End If
        If Not IsNothing(_PatientSynopsisUC) Then
            If (IsNothing(oPage) = False) Then
                If (oPage.Controls.Contains(_PatientSynopsisUC)) Then
                    oPage.Controls.Remove(_PatientSynopsisUC)
                End If
            End If

            RemoveHandler _PatientSynopsisUC.ViewMedications, AddressOf _PatientSynopsisUC_ViewMedications
            RemoveHandler _PatientSynopsisUC.ViewDMS, AddressOf _PatientSynopsisUC_ViewDMS
            RemoveHandler _PatientSynopsisUC.ViewPatientHistory, AddressOf _PatientSynopsisUC_ViewPatientHistory
            RemoveHandler _PatientSynopsisUC.ViewProblemList, AddressOf _PatientSynopsisUC_ViewProblemList
            RemoveHandler _PatientSynopsisUC.ViewProcedures, AddressOf _PatientSynopsisUC_ViewProcedures
            RemoveHandler _PatientSynopsisUC.ViewRadiology, AddressOf _PatientSynopsisUC_ViewRadiology
            RemoveHandler _PatientSynopsisUC.OnLabsFlexDoubleClick, AddressOf _PatientSynopsisUC_OnLabsFlexDoubleClick
            RemoveHandler _PatientSynopsisUC.ShowMedicationForm, AddressOf _PatientSynopsisUC_ShowMedicationForm
            RemoveHandler _PatientSynopsisUC.ShowProblemListForm, AddressOf _PatientSynopsisUC_ShowProblemListForm
            RemoveHandler _PatientSynopsisUC.ShowRadiologyForm, AddressOf _PatientSynopsisUC_ShowRadiologyForm
            RemoveHandler _PatientSynopsisUC.ShowHistoryForm, AddressOf _PatientSynopsisUC_ShowHistoryForm

            '_PatientSynopsisUC.Dispose()
            '_PatientSynopsisUC = Nothing
        End If

        If Not IsNothing(GloUC_TransactionHistory1) Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(GloUC_TransactionHistory1)
            Catch ex As Exception

            End Try

            GloUC_TransactionHistory1.Dispose()
            GloUC_TransactionHistory1 = Nothing
        End If
        'If Not IsNothing(c2) Then
        '    c2.Dispose()
        '    c2 = Nothing
        'End If
        If Not IsNothing(ofrmViewGloLab) Then
            'RemoveHandler ofrmViewGloLab.EvntGenerateCDAHandler, AddressOf CType(Me.ParentForm, MainMenu).openCDA
            'RemoveHandler ofrmViewGloLab.EventCDA, AddressOf mdlGeneral.OpenCDA

            ' Dim myCloseEventHandler As New System.Windows.Forms.FormClosedEventHandler(AddressOf CType(Me.ParentForm, MainMenu).ofrmViewgloLab_FormClosed) '.ShowTasks)
            ' Dim myOpenEventHandler As New gloEmdeonInterface.Forms.frmViewgloLab.OpenClinicalChart(AddressOf CType(Me.ParentForm, MainMenu).OpenClinicalChart)
            ' Dim myCCDEventHandler As New gloEmdeonInterface.Forms.frmViewgloLab.GenerateCCDHandler(AddressOf CType(Me.ParentForm, MainMenu).openCCD)

            Try
                RemoveHandler ofrmViewGloLab.EvntOpenClinicalChart, AddressOf CType(Me.ParentForm, MainMenu).OpenClinicalChart 'myOpenEventHandler
                RemoveHandler ofrmViewGloLab.EntOpenMessage, AddressOf CType(Me.ParentForm, MainMenu).OpenMessage
                RemoveHandler ofrmViewGloLab.EvntOpenPatientLetter, AddressOf CType(Me.ParentForm, MainMenu).OpenPatientLetter
                RemoveHandler ofrmViewGloLab.EvntOpenReferralLetter, AddressOf CType(Me.ParentForm, MainMenu).OpenReferralLetters
                RemoveHandler ofrmViewGloLab.EvntGenerateCCDHandler, AddressOf CType(Me.ParentForm, MainMenu).openCCD 'myCCDEventHandler
                RemoveHandler ofrmViewGloLab.EvntGenerateCDAHandler, AddressOf CType(Me.ParentForm, MainMenu).openCDA
                RemoveHandler ofrmViewGloLab.FormClosed, AddressOf CType(Me.ParentForm, MainMenu).ofrmViewgloLab_FormClosed 'myCloseEventHandler
                RemoveHandler ofrmViewGloLab.Activated, AddressOf CType(Me.ParentForm, MainMenu).frmViewgloLab_Activated
                'RemoveHandler ofrmViewGloLab.EvntOpenClinicalChart, AddressOf CType(Me.ParentForm, MainMenu).OpenClinicalChart
                ''Bug No 57350::Patient InfoButton - Applicatipn not able to open Patient spacific & Provider Spacific Document
                RemoveHandler ofrmViewGloLab.EntOpenEducation, AddressOf CType(Me.ParentForm, MainMenu).OpenEducation
                RemoveHandler ofrmViewGloLab.EventCDA, AddressOf mdlGeneral.OpenCDA
            Catch ex As Exception

            End Try
            If (IsNothing(ofrmViewGloLab) = False) Then
                ofrmViewGloLab.Close()
            End If
            If (IsNothing(ofrmViewGloLab) = False) Then
                ofrmViewGloLab.Dispose()
                ofrmViewGloLab = Nothing
            End If

            'myCloseEventHandler = Nothing
            'myOpenEventHandler = Nothing
            'myCCDEventHandler = Nothing

        End If

        If Not IsNothing(ofrmDiagnosis) Then
            ofrmDiagnosis.Dispose()
            ofrmDiagnosis = Nothing
        End If

        If Not IsNothing(oEDocumentV3) Then
            oEDocumentV3.Dispose()
            oEDocumentV3 = Nothing
        End If
        If Not IsNothing(oPage) Then
            oPage.Dispose()
            oPage = Nothing
        End If

        If Not IsNothing(otab) Then
            Dim tabcount As Int32 = otab.TabPages.Count - 1

            For i As Int32 = tabcount - 1 To 0 Step -1
                otab.TabPages(i).Controls.Clear()
                otab.TabPages.RemoveAt(i)
            Next
            otab.Dispose()
            otab = Nothing
        End If


    End Sub



    ''' <summary>
    ''' Property writen for Patient Context
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return SelectedPatientId  'Curent patient variable(Local variable) for this module 
        End Get
    End Property

    Public Sub frmPrecription_Closed(ByVal sender As Object, ByVal e As FormClosedEventArgs)
        Dim frm As frmPrescription = Nothing

        Try
            frm = DirectCast(sender, frmPrescription)
        Catch ex As Exception

        End Try
        Try
            If (IsNothing(frm) = False) Then
                RemoveHandler frm.FormClosed, AddressOf frmPrecription_Closed
            End If
            If (IsNothing(frm) = False) Then
                frm.Close()
            End If
            If (IsNothing(frm) = False) Then
                frm.Dispose()
                frm = Nothing
            End If

        Catch ex As Exception

        End Try
        If IsNothing(_PatientSynopsisUC) = False Then
            Dim dsdata As DataSet
            Dim c2 As ClsPatientSynopsis = New ClsPatientSynopsis
            dsdata = c2.PopulateSynopsisData(_PatientID, "Medications")
            _PatientSynopsisUC.PopulateLatestMedications(dsdata.Tables("Medications"))
            If Not IsNothing(dsdata) Then
                dsdata.Dispose()
                dsdata = Nothing
            End If
            If Not IsNothing(c2) Then
                c2.Dispose()
                c2 = Nothing
            End If
        End If

    End Sub
    Public Sub ProblemListClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs)
        Dim ofrmPm As frmProblemList = Nothing
        Try
            ofrmPm = DirectCast(sender, frmProblemList)
        Catch ex As Exception

        End Try
        Try
            If (IsNothing(ofrmPm) = False) Then
                RemoveHandler ofrmProblems.FormClosed, AddressOf ProblemListClosed
            End If
        Catch ex As Exception
        End Try

        Try
            If (IsNothing(ofrmPm) = False) Then
                ofrmPm.Close()
            End If

        Catch ex As Exception

        End Try
        Try
            If (IsNothing(ofrmPm) = False) Then
                ofrmPm.Dispose()
                ofrmPm = Nothing
            End If

        Catch ex As Exception

        End Try

        Try
            _PatientSynopsisUC.Fill_PastExams()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub
End Class
