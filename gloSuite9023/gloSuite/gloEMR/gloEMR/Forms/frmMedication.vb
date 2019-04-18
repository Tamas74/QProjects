Imports gloEMRGeneralLibrary.gloEMRActors
Imports gloUserControlLibrary
Imports gloEMRGeneralLibrary.gloPrintFax
Imports gloEMRGeneralLibrary.gloGeneral
Imports gloEMRGeneralLibrary.gloEMRMedication
Imports gloEMRGeneralLibrary.gloEMRDatabase
'For NDC Interaction
Imports gloDIControl
'For NDC Interaction
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class frmMedication

    'to get the events of the user controls when they are added at runtime
    Private WithEvents _MedBusinessLayer As MedicationBusinessLayer
    Private WithEvents _MedListUserCtrl As gloMedListUserCtrl
    Private WithEvents _MedHistoryUserCtrl As gloMedHistoryUserCtrl
    Private WithEvents _MedicationC1FlexGrdUserCtrl As gloMedicationC1FlexGrdUserCtrl
    Private WithEvents _MedRefillC1FlexGridUserCtrl As gloMedRefillC1FlexGridUserCtrl
    'Private WithEvents _MedSearchC1FlexGridUserCtrl As gloSearchC1FlexgridUserCtrl

    Private WithEvents _MedPatientStrip As gloUC_PatientStrip
    Private WithEvents _MedToolBarUserCtrl As gloMedicationToolBarUserCtrl

    Private WithEvents objCustomMedication As CustomMedication
    'now instead of customDataGrid we have to take bipins control
    'Private WithEvents ObjcustomGrid As CustomDataGrid
    Private WithEvents objSigControl As gloUC_CustomSearchInC1Flexgrid

    Public Event DrugDuplication(ByVal ValidationMessage As String)
    Public blncancel As Boolean
    Private arrdrugs As ArrayList

    'sarika 15th oct 07
    Private sigid As Long = 0
    '--------------------

    'Private gblnmedalert As Boolean = False 'True
    Private rowindex As Integer
    Private WithEvents cmbMedStatus As ComboBox
    Private m_visitdate As DateTime
    Private blnIsRefill As Boolean
    Private GridStatus As Int16
    Dim dt As New DataTable
    ' Dim voicecol As DNSTools.DgnStrings
    Private intMode As Int16
    'Private objMedicationDBLayer As clsMedicationDBLayer
  
    ' Private PreviousData As New ArrayList
    '  Dim objPreviousData As ClsPrescription
    Private treeindex As Int16
    Dim visitid As Long
    Dim tempVisitId As Long
    Dim tempdate As DateTime
    Dim trvSearchNode As TreeNode
    Dim PastVisitID As Long
    Public myCaller As frmPatientExam
    Public myLetter As frmPatientLetter
    Public myCallerSynopsis As frmPatientSynopsis
    'Boolean variable to check that, form is open from Main form or from Patient Exam
    'This variable is used for voice purpose
    Public blnOpenFromExam As Boolean = False
    Private ReferralCount As Int64
   
    ' Private MedicationVoiceCol As DNSTools.DgnStrings
    Dim cntPreviousData As Int16 = 0
    Private blnDIResult As Boolean
    
    Private arrcol As Collection
    Private ArrMedicationCol As Collection
    Dim btntype As Int16
    Private strnodetype As String = ""
    Private m_status As String
    Private pnlmedstatus As Panel
   
    Private pnlMedicationreport As Panel
    Private ResultCol As DrugInteractionCollection.gloInteractionCollection
    Dim strUncodedDrugs As New System.Text.StringBuilder
    'DI User Control Declaration
    Private WithEvents objDrugInteraction As DIToolbar
    Private WithEvents objDIScreenResults As DIScreeningResults
    Private WithEvents objMonoDIScreenResults As DIScreeningResults
    Private WithEvents objADEScreenResults As ADEScreening
    Enum enmPreviousMedications
        Current
        Yesterday
        LastWeek
        LastMonth
        Older
    End Enum

    Public Sub New()
        MyBase.New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        'a business layer object is created in the constructor so that it is used by all user controls at runtime
        _MedBusinessLayer = New MedicationBusinessLayer
        _MedBusinessLayer.PastVisitDate = Now
        _MedBusinessLayer.CurrentVisitDate = Now

        ' Add any initialization after the InitializeComponent() call.

        'initialising all the user controls at runtime
        InitialiseControls()

    End Sub

    Public Sub New(ByVal m_visitid As Long)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
        _MedBusinessLayer = New MedicationBusinessLayer
        _MedBusinessLayer.CurrentVisitID = m_visitid
        _MedBusinessLayer.PastVisitID = m_visitid
        _MedBusinessLayer.TransactionMode = MedicationBusinessLayer._TransactionMode.Add
        If _MedBusinessLayer.PastVisitID <> 0 Then
            _MedBusinessLayer.FetchPastVisitDate()
        Else
            _MedBusinessLayer.PastVisitDate = Now
            _MedBusinessLayer.CurrentVisitDate = Now
        End If
        InitialiseControls()

    End Sub

    Public Sub New(ByVal m_visitid As Long, ByVal m_patientid As Long)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
        _MedBusinessLayer = New MedicationBusinessLayer
        _MedBusinessLayer.CurrentVisitID = m_visitid
        _MedBusinessLayer.PastVisitID = m_visitid
        _MedBusinessLayer.TransactionMode = MedicationBusinessLayer._TransactionMode.Add
        If _MedBusinessLayer.PastVisitID <> 0 Then
            _MedBusinessLayer.FetchPastVisitDate()
        Else
            _MedBusinessLayer.PastVisitDate = Now
            _MedBusinessLayer.CurrentVisitDate = Now
        End If
        InitialiseControls()
    End Sub

    Public Sub New(ByVal Arrlist As ArrayList, Optional ByVal m_visitid As Long = 0)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        arrdrugs = Arrlist
        _MedBusinessLayer = New MedicationBusinessLayer
        _MedBusinessLayer.CurrentVisitID = m_visitid
        _MedBusinessLayer.PastVisitID = m_visitid
        _MedBusinessLayer.TransactionMode = MedicationBusinessLayer._TransactionMode.Add
        If _MedBusinessLayer.PastVisitID <> 0 Then
            _MedBusinessLayer.FetchPastVisitDate()
        Else
            _MedBusinessLayer.PastVisitDate = Now
            _MedBusinessLayer.CurrentVisitDate = Now
        End If
        InitialiseControls()
    End Sub



    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub InitialiseControls()
        'all the user controls gets initialised  when the form gets loaded 
        Try

            clsgeneral.ConnectionString = GetConnectionString()
            globalSecurity.gstrLoginName = gstrLoginName
            clsgeneral.gnThresholdSetting = gnThresholdSetting
            'initialising of ListUser control
            globalPatient.gnPatientID = gnPatientID
            clsgeneral.gblnRecordLocking = gblnRecordLocking
            globalSecurity.gstrClientMachineName = gstrClientMachineName


            _MedListUserCtrl = New gloMedListUserCtrl(_MedBusinessLayer, gnMedDrugButton)
            _MedHistoryUserCtrl = New gloMedHistoryUserCtrl(_MedBusinessLayer)

            'call the refreshMedicationHistory function of medication business layer
            'code added by sagar on 5 june 2007 as recomended by supriya madam
            _MedHistoryUserCtrl.RefreshMedicationHistory()


            _MedicationC1FlexGrdUserCtrl = New gloMedicationC1FlexGrdUserCtrl(_MedBusinessLayer)
            _MedicationC1FlexGrdUserCtrl.Threshold = gnThresholdSetting
            _MedToolBarUserCtrl = New gloMedicationToolBarUserCtrl
            _MedToolBarUserCtrl.Dock = DockStyle.Fill
            _MedToolBarUserCtrl.tStrpERx.Visible = False
            pnlmainToolBar.Controls.Add(_MedToolBarUserCtrl)

            _MedToolBarUserCtrl.Visible = True
            pnlmainToolBar.BringToFront()
            _MedPatientStrip = New gloUC_PatientStrip
            _MedPatientStrip.ShowDetail(globalPatient.gnPatientID, gloUC_PatientStrip.enumFormName.Prescription)
            _MedPatientStrip.HideButton = False

            pnlRefill.Visible = False

            _MedicationC1FlexGrdUserCtrl.AutoSize = True
            pnlFlexGrid.AutoScroll = True
            pnlFlexGrid.VerticalScroll.Enabled = True
            pnlFlexGrid.HorizontalScroll.Enabled = True

            pnlcenter.VerticalScroll.Enabled = True
            pnlcenter.HorizontalScroll.Enabled = True

            pnlDIScreenResult.AutoScroll = True
            pnlDIScreenResult.HorizontalScroll.Enabled = True
            pnlDIScreenResult.VerticalScroll.Enabled = True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub
 

    'Function invoked at runtime to add Medication user controls to Medication form
    Private Sub AddMedControls()
        Try
            'pnlmainToolBar.Controls.Add(_MedToolBarUserCtrl)
            '_MedToolBarUserCtrl.Dock = DockStyle.Fill
            _MedListUserCtrl.Dock = DockStyle.Fill
            pnlleft.Controls.Add(_MedListUserCtrl)

            _MedHistoryUserCtrl.Dock = DockStyle.Fill
            pnlRight.Controls.Add(_MedHistoryUserCtrl)
            pnlRight.Visible = False
            splRight.Visible = False

            _MedPatientStrip.Dock = DockStyle.Top
            _MedPatientStrip.Padding = New Padding(0, 3, 3, 0)
            _MedPatientStrip.BringToFront()
            pnlcenter.Controls.Add(_MedPatientStrip)


            pnlFlexGrid.Controls.Add(_MedicationC1FlexGrdUserCtrl)

            _MedicationC1FlexGrdUserCtrl.Dock = DockStyle.Fill
            '_MedicationC1FlexGrdUserCtrl.Visible = True
            _MedicationC1FlexGrdUserCtrl.BringToFront()


            '_MedRefillC1FlexGridUserCtrl.Dock = DockStyle.Fill
            'pnlRefill.Controls.Add(_MedRefillC1FlexGridUserCtrl)
            'pnlRefill.BringToFront()

            '_MedRefillC1FlexGridUserCtrl.Visible = True
            pnlRefill.Visible = False
            'pnlDI.BackgroundImageLayout = ImageLayout.Stretch
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub

    'Function invoked to add Medication details User control to Medication Form
    Private Sub AddControl()
        If Not IsNothing(objCustomMedication) Then
            RemoveControl()
        End If
        splRight.Visible = False
        pnlRight.Visible = False
        _MedToolBarUserCtrl.tStrpShowHide.Text = "Show"
        _MedToolBarUserCtrl.tStrpShowHide.ToolTipText = "Show Medication History"
        objCustomMedication = New CustomMedication(_MedBusinessLayer, _MedicationC1FlexGrdUserCtrl.RowIndex)
        objCustomMedication.Threshold = gnThresholdSetting
        Me.pnlRefill.Controls.Add(objCustomMedication)

        objCustomMedication.Dock = DockStyle.Fill
        objCustomMedication.Visible = True
        objCustomMedication.BringToFront()
        pnlFlexGrid.BringToFront()
        pnlRefill.Visible = True
        'objSigControl = New gloUC_CustomSearchInC1Flexgrid(dt, False)

    End Sub

    Private Sub _MedToolBarUserCtrl_SendFaxImmediatelyToolStripMenuItemClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _MedToolBarUserCtrl.SendFaxImmediatelyToolStripMenuItemClick
        'Try
        '    'Check FAX Printer settings are set or not
        '    If isPrinterSettingsSet(True) = False Then
        '        Exit Sub
        '    End If
        '    CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.SendImmediately
        '    Callprint(False)

        'Catch ex As Exception

        'End Try

        Try
            Cursor.Current = Cursors.WaitCursor
            If gblnClinicDIAlert Then
                If gblnDIAlert Then
                    RefreshDIScreen()
                    If _MedBusinessLayer.MedicationCol.Count > 0 Then
                        'check screening result ,if true then show the count
                        Dim strmessage As String = ""
                        Dim strmessage1 As String = ""
                        If PerformAutoScreening(False, strmessage, strmessage1) Then
                            DisplayAlertForUncodedDrugs()
                            Dim result As Int16
                            'to Supress Drug alert Message
                            MessageBox.Show(strmessage, "Medication", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            If gblnDrugAlertMsg = True Then
                                result = MessageBox.Show("Do you want to override the alert and continue to fax and save medication?", "Medication", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                If result = vbYes Then
                                    _MedBusinessLayer.SaveMedication()
                                    'Check FAX Printer settings are set or not
                                    If gblnInternetFax = False Then
                                        If isPrinterSettingsSet(True) = False Then
                                            Exit Sub
                                        End If
                                    End If

                                    UpdateLog("Current Sending FAX Priority is set to IMMEDIATELY.......................")
                                    CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.SendImmediately

                                    UpdateLog("Function Callprint(False) called.......................")
                                    Callprint(False)
                                Else
                                    'Me.Close()
                                    Exit Sub
                                End If
                            Else
                                _MedBusinessLayer.SaveMedication()
                                'Check FAX Printer settings are set or not
                                If gblnInternetFax = False Then
                                    If isPrinterSettingsSet(True) = False Then
                                        Exit Sub
                                    End If
                                End If

                                UpdateLog("Current Sending FAX Priority is set to IMMEDIATELY.......................")
                                CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.SendImmediately

                                UpdateLog("Function Callprint(False) called.......................")
                                Callprint(False)
                            End If
                            'to Supress Drug alert Message

                            '--------original code
                            ''Notes need to be added
                            'result = MessageBox.Show(strmessage & "Do you want to Override the alert and continue prescribing drugs?", "Medication", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            'If result = vbYes Then
                            '    _MedBusinessLayer.SaveMedication()
                            '    'Check FAX Printer settings are set or not
                            '    If gblnInternetFax = False Then
                            '        If isPrinterSettingsSet(True) = False Then
                            '            Exit Sub
                            '        End If
                            '    End If

                            '    UpdateLog("Current Sending FAX Priority is set to IMMEDIATELY.......................")
                            '    CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.SendImmediately

                            '    UpdateLog("Function Callprint(False) called.......................")
                            '    Callprint(False)
                            'Else
                            '    'Me.Close()
                            '    Exit Sub
                            'End If
                            ''Drug alert for some

                            ''No Positive Drug Screening
                            '--------original code
                        Else
                            DisplayAlertForUncodedDrugs()
                            _MedBusinessLayer.SaveMedication()
                            'Check FAX Printer settings are set or not
                            If gblnInternetFax = False Then
                                If isPrinterSettingsSet(True) = False Then
                                    Exit Sub
                                End If
                            End If

                            CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.SendImmediately
                            Callprint(False)
                        End If
                        'Do not perform screening as no items in medication
                    End If
                    'Machine Level DI alert turned off
                Else
                    _MedBusinessLayer.SaveMedication()
                    'Check FAX Printer settings are set or not
                    If gblnInternetFax = False Then
                        If isPrinterSettingsSet(True) = False Then
                            Exit Sub
                        End If
                    End If
                    CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.SendImmediately
                    Callprint(False)
                    End If
                    'Clinic level DI Alert turned off
            Else
                    _MedBusinessLayer.SaveMedication()
                'Check FAX Printer settings are set or not
                If gblnInternetFax = False Then
                    If isPrinterSettingsSet(True) = False Then
                        Exit Sub
                    End If
                End If
                CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.SendImmediately
                Callprint(False)
                End If

                Cursor.Current = Cursors.Default
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Fax, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub _MedToolBarUserCtrl_SendFaxNormalPriorityToolStripMenuItemClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _MedToolBarUserCtrl.SendFaxNormalPriorityToolStripMenuItemClick
        'Try
        '    'Check FAX Printer settings are set or not
        '    If isPrinterSettingsSet(True) = False Then
        '        Exit Sub
        '    End If
        '    CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.NormalPriority
        '    Callprint(False)
        'Catch ex As Exception

        'End Try
        Try
            Cursor.Current = Cursors.WaitCursor
            If gblnClinicDIAlert Then
                If gblnDIAlert Then
                    RefreshDIScreen()
                    If _MedBusinessLayer.MedicationCol.Count > 0 Then
                        'check screening result ,if true then show the count
                        Dim strmessage As String = ""
                        Dim strmessage1 As String = ""
                        If PerformAutoScreening(False, strmessage, strmessage1) Then
                            DisplayAlertForUncodedDrugs()
                            Dim result As Int16
                            'to Supress Drug alert Message
                            MessageBox.Show(strmessage, "Medication", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            If gblnDrugAlertMsg = True Then
                                result = MessageBox.Show("Do you want to override the alert and continue to fax and save medication?", "Medication", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                If result = vbYes Then
                                    _MedBusinessLayer.SaveMedication()
                                    'Check FAX Printer settings are set or not
                                    If gblnInternetFax = False Then
                                        If isPrinterSettingsSet(True) = False Then
                                            Exit Sub
                                        End If
                                    End If

                                    UpdateLog("Current Sending FAX Priority is set to Normally.......................")
                                    CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.NormalPriority

                                    UpdateLog("Function Callprint(False) called.......................")
                                    Callprint(False)
                                Else
                                    'Me.Close()
                                    Exit Sub
                                End If
                            Else
                                _MedBusinessLayer.SaveMedication()
                                'Check FAX Printer settings are set or not
                                If gblnInternetFax = False Then
                                    If isPrinterSettingsSet(True) = False Then
                                        Exit Sub
                                    End If
                                End If

                                UpdateLog("Current Sending FAX Priority is set to Normally.......................")
                                CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.NormalPriority

                                UpdateLog("Function Callprint(False) called.......................")
                                Callprint(False)
                            End If
                            'to Supress Drug alert Message

                            '--------original code
                            ''Notes need to be added
                            'result = MessageBox.Show(strmessage & "Do you want to Override the alert and continue prescribing drugs?", "Medication", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            'If result = vbYes Then
                            '    _MedBusinessLayer.SaveMedication()
                            '    'Check FAX Printer settings are set or not
                            '    If gblnInternetFax = False Then
                            '        If isPrinterSettingsSet(True) = False Then
                            '            Exit Sub
                            '        End If
                            '    End If

                            '    UpdateLog("Current Sending FAX Priority is set to Normally.......................")
                            '    CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.NormalPriority

                            '    UpdateLog("Function Callprint(False) called.......................")
                            '    Callprint(False)
                            'Else
                            '    'Me.Close()
                            '    Exit Sub
                            'End If
                            ''Drug alert for some

                            ''No Positive Drug Screening
                            '--------original code
                        Else
                            DisplayAlertForUncodedDrugs()
                            _MedBusinessLayer.SaveMedication()
                            'Check FAX Printer settings are set or not
                            If gblnInternetFax = False Then
                                If isPrinterSettingsSet(True) = False Then
                                    Exit Sub
                                End If
                            End If

                            CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.NormalPriority
                            Callprint(False)
                        End If
                        'Do not perform screening as no items in medication
                    End If
                    'Machine Level DI alert turned off
                Else
                    _MedBusinessLayer.SaveMedication()
                    'Check FAX Printer settings are set or not

                    If gblnInternetFax = False Then
                        If isPrinterSettingsSet(True) = False Then
                            Exit Sub
                        End If
                    End If

                    CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.NormalPriority
                    Callprint(False)
                End If
                'Clinic level DI Alert turned off
            Else
                _MedBusinessLayer.SaveMedication()
                'Check FAX Printer settings are set or not

                If gblnInternetFax = False Then
                    If isPrinterSettingsSet(True) = False Then
                        Exit Sub
                    End If
                End If

                CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.NormalPriority
                Callprint(False)
            End If

            Cursor.Current = Cursors.Default
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Fax, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Cursor.Current = Cursors.Default
        End Try
    End Sub


    Private Sub _MedToolBarUserCtrl_tStrpCloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _MedToolBarUserCtrl.tStrpCloseClick
        Try
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub _MedToolBarUserCtrl_tStrpFaxButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _MedToolBarUserCtrl.tStrpFaxButtonClick
        'code commented because we need to first check the drug interaction and then print/fax
        'Try
        '    Callprint(False)

        'Catch ex As Exception

        'End Try
        'code commented because we need to first check the drug interaction and then print/fax

        Try
            Cursor.Current = Cursors.WaitCursor
            If gblnClinicDIAlert Then
                If gblnDIAlert Then
                    RefreshDIScreen()
                    If _MedBusinessLayer.MedicationCol.Count > 0 Then
                        'check screening result ,if true then show the count
                        Dim strmessage As String = ""
                        Dim strmessage1 As String = ""
                        If PerformAutoScreening(False, strmessage, strmessage1) Then
                            DisplayAlertForUncodedDrugs()
                            Dim result As Int16
                            'to Supress Drug alert Message
                            MessageBox.Show(strmessage, "Medication", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            If gblnDrugAlertMsg = True Then
                                result = MessageBox.Show("Do you want to override the alert and continue to fax and save medication?", "Medication", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                If result = vbYes Then
                                    _MedBusinessLayer.SaveMedication()

                                    UpdateLog("Function Callprint(False) called with no Fax Priorities i.e IMMEDIATELY or NORMAL.......................")
                                    Callprint(False)
                                Else
                                    'Callprint(False)
                                    'Me.Close()
                                    Exit Sub
                                End If
                            Else
                                _MedBusinessLayer.SaveMedication()

                                UpdateLog("Function Callprint(False) called with no Fax Priorities i.e IMMEDIATELY or NORMAL.......................")
                                Callprint(False)
                            End If
                            'to Supress Drug alert Message

                            '--------original code
                            ''Notes need to be added
                            'result = MessageBox.Show(strmessage & "Do you want to Override the alert and continue saving medication?", "Medication", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            'If result = vbYes Then
                            '    _MedBusinessLayer.SaveMedication()

                            '    UpdateLog("Function Callprint(False) called with no Fax Priorities i.e IMMEDIATELY or NORMAL.......................")
                            '    Callprint(False)
                            'Else
                            '    'Callprint(False)
                            '    'Me.Close()
                            '    Exit Sub
                            'End If
                            ''Drug alert for some

                            ''No Positive Drug Screening
                            '--------original code
                        Else
                            DisplayAlertForUncodedDrugs()
                            _MedBusinessLayer.SaveMedication()
                            Callprint(False)
                        End If
                        'Do not perform screening as no items in medication
                    End If
                    'Machine Level DI alert turned off
                Else
                    _MedBusinessLayer.SaveMedication()
                    Callprint(False)
                End If
                'Clinic level DI Alert turned off
            Else
                _MedBusinessLayer.SaveMedication()
                Callprint(False)
            End If

            Cursor.Current = Cursors.Default
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Fax, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Cursor.Current = Cursors.Default
        End Try
    End Sub


    'Private Sub CallFax()

    '    Dim oRpt As ReportDocument
    '    Try
    '        oRpt = New ReportDocument
    '        'oRpt.Load(Application.StartupPath & "\Reports\RptPatientCurrentMedication.rpt")
    '        oRpt.Load("D:\gloEMR Working Folder\gloEMR\gloEMR\bin\Reports\rptPatientCurrentMedication.rpt")
    '        Dim crtableLogoninfos As New TableLogOnInfos
    '        Dim crtableLogoninfo As New TableLogOnInfo
    '        Dim crConnectionInfo As New ConnectionInfo
    '        Dim CrTables As Tables
    '        Dim CrTable As Table
    '        Dim TableCounter

    '        With crConnectionInfo
    '            .ServerName = gstrSQLServerName

    '            'If you are connecting to Oracle there is no 
    '            'DatabaseName. Use an empty string. 
    '            'For example, .DatabaseName = "" 

    '            .DatabaseName = gstrDatabaseName

    '            '.UserID = "Your User ID"
    '            '.Password = "Your Password"
    '            .IntegratedSecurity = True
    '        End With

    '        'This code works for both user tables and stored 
    '        'procedures. Set the CrTables to the Tables collection 
    '        'of the report 

    '        CrTables = oRpt.Database.Tables

    '        'Loop through each table in the report and apply the 
    '        'LogonInfo information 

    '        For Each CrTable In CrTables
    '            crtableLogoninfo = CrTable.LogOnInfo
    '            crtableLogoninfo.ConnectionInfo = crConnectionInfo
    '            CrTable.ApplyLogOnInfo(crtableLogoninfo)

    '            'If your DatabaseName is changing at runtime, specify 
    '            'the table location. 
    '            'For example, when you are reporting off of a 
    '            'Northwind database on SQL server you 
    '            'should have the following line of code: 

    '            CrTable.Location = gstrDatabaseName & ".dbo." & CrTable.Name

    '        Next

    '        oRpt.SetParameterValue("PatientID", gnPatientID.ToString)

    '        'If trMedicationDetails.Nodes.Item(0).GetNodeCount(False) > 0 Then ''''' by sagar start here
    '        'If C1FlexGrdMedicationDetails.Rows.Count > 0 Then
    '        '    oRpt.SetParameterValue("VisitID", returnvisitid.ToString)
    '        'Else
    '        '    oRpt.SetParameterValue("VisitID", tempVisitId.ToString)

    '        'End If ''''' by sagar end here

    '        If _MedBusinessLayer.MedicationCol.Count > 0 Then
    '            Call SetFAXPrinterDefaultSettings()
    '            'Retrieve the FAX Cover Page details
    '            'Find FAX Parameters
    '            'Get Pharmacy FAX No
    '            Dim strFAXTo As String
    '            Dim strFAXNo As String
    '            Dim objmytable As mytable
    '            Dim objFAX As New clsFAX
    '            objmytable = objFAX.GetPharmacyFAXNo(gnPatientID)

    '            If Not IsNothing(objmytable) Then
    '                gstrFAXContactPersonFAXNo = objmytable.Description
    '                gstrFAXContactPerson = objmytable.Code
    '            End If

    '            If Trim(gstrFAXContactPerson) = "" Then
    '                gstrFAXContactPerson = InputBox("Please enter the Pharmacy Name", gstrMessageBoxCaption)
    '            End If

    '            If gblnFAXCoverPage Then
    '                If RetrieveFAXDetails(mdlFAX.enmFAXType.Medication, gnPatientID, gstrFAXContactPerson, gstrFAXContactPersonFAXNo, "Medication", 0, visitid, 0) = False Then
    '                    Exit Sub
    '                End If
    '            Else
    '                If Trim(gstrFAXContactPersonFAXNo) = "" Then
    '                    gstrFAXContactPersonFAXNo = InputBox("Please enter the Pharmacy FAX No", gstrMessageBoxCaption)
    '                End If
    '            End If

    '            If Trim(gstrFAXContactPersonFAXNo) = "" Then
    '                MessageBox.Show("You have not entered the FAX No. So FAX will not be send", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            End If

    '            'Retrieve FAX Document Name
    '            Dim strFAXDocumentName As String
    '            strFAXDocumentName = RetrieveFAXDocumentName()
    '            If SetFAXPrinterDocumentSettings(strFAXDocumentName) = False Then Exit Sub
    '            objFAX.AddPendingFAX(gnPatientID, gstrFAXContactPerson, "Medication", gstrFAXContactPersonFAXNo, gstrLoginName, strFAXDocumentName, System.DateTime.Now, CurrentSendingFAXPriority)
    '            objFAX = Nothing
    '            oRpt.PrintToPrinter(1, False, 0, 0)
    '        Else

    '            oRpt.PrintToPrinter(1, False, 0, 0)

    '        End If

    '        Dim objAudit As New clsAudit
    '        objAudit.CreateLog(clsAudit.enmActivityType.PHIExport, "Fax is sent.", gstrLoginName, gstrClientMachineName, 0, True)
    '        objAudit = Nothing

    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '        Dim objAudit As New clsAudit
    '        objAudit.CreateLog(clsAudit.enmActivityType.PHIExport, "Unable to send fax.", gstrLoginName, clsAudit.enmOutCome.Failure, gstrClientMachineName, 0, True)
    '        objAudit = Nothing
    '    End Try
    'End Sub

    Private Function RetrieveFAXDocumentName() As String
        'Set FAX Settings
        Dim strTIFFFileName As String = ""
        Dim _dtCurrentDateTime As DateTime = System.DateTime.Now
        strTIFFFileName = gnClientMachineID & "-" & Format(_dtCurrentDateTime, "yyyyMMddhhmmss") & _dtCurrentDateTime.Millisecond
        Return strTIFFFileName
    End Function


    'Private Sub _MedToolBarUserCtrl_tStrpNewClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _MedToolBarUserCtrl.tStrpNewClick
    '    Try
    '        _MedBusinessLayer.MedicationCol.Clear()
    '        'Clear flexgrid
    '        _MedBusinessLayer.CurrentVisitID = _MedBusinessLayer.PastVisitID
    '        _MedBusinessLayer.CurrentVisitDate = _MedBusinessLayer.PastVisitDate
    '        _MedBusinessLayer.TransactionMode = MedicationBusinessLayer._TransactionMode.Add
    '        _MedicationC1FlexGrdUserCtrl.ClearRows()
    '        RemoveControl()
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub _MedToolBarUserCtrl_tStrpPrintClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _MedToolBarUserCtrl.tStrpPrintClick
        'code commented because we need to first check the drug interaction and then print/fax
        'Try
        '    Callprint(True)
        'Catch ex As Exception

        'End Try
        'code commented because we need to first check the drug interaction and then print/fax


        Try
            Cursor.Current = Cursors.WaitCursor
            If gblnClinicDIAlert Then
                If gblnDIAlert Then
                    RefreshDIScreen()
                    If _MedBusinessLayer.MedicationCol.Count > 0 Then
                        'check screening result ,if true then show the count
                        Dim strmessage As String = ""
                        Dim strmessage1 As String = ""
                        If PerformAutoScreening(False, strmessage, strmessage1) Then
                            DisplayAlertForUncodedDrugs()
                            Dim result As Int16
                            'to Supress Drug alert Message
                            MessageBox.Show(strmessage, "Medication", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            If gblnDrugAlertMsg = True Then
                                result = MessageBox.Show("Do you want to override the alert and continue to print and save medication?", "Medication", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                If result = vbYes Then
                                    _MedBusinessLayer.SaveMedication()

                                    UpdateLog("Printing Medication Started.....................................")
                                    Callprint(True)
                                    UpdateLog("Printing Medication Completed.....................................")
                                Else
                                    'Callprint(True)
                                    'Me.Close()
                                    Exit Sub
                                End If
                            Else
                                _MedBusinessLayer.SaveMedication()

                                UpdateLog("Printing Medication Started.....................................")
                                Callprint(True)
                                UpdateLog("Printing Medication Completed.....................................")
                            End If
                            'to Supress Drug alert Message

                            '--------original code
                            ''Notes need to be added
                            'result = MessageBox.Show(strmessage & "Do you want to Override the alert and continue saving medication?", "Medication", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            'If result = vbYes Then
                            '    _MedBusinessLayer.SaveMedication()

                            '    UpdateLog("Printing Medication Started.....................................")
                            '    Callprint(True)
                            '    UpdateLog("Printing Medication Completed.....................................")
                            'Else
                            '    'Callprint(True)
                            '    'Me.Close()
                            '    Exit Sub
                            'End If
                            ''Drug alert for some

                            ''No Positive Drug Screening
                            '--------original code
                        Else
                            DisplayAlertForUncodedDrugs()
                            _MedBusinessLayer.SaveMedication()
                            Callprint(True)
                        End If
                        'Do not perform screening as no items in medication
                    End If
                    'Machine Level DI alert turned off
                Else
                    _MedBusinessLayer.SaveMedication()
                    Callprint(True)
                End If
                'Clinic level DI Alert turned off
            Else
                _MedBusinessLayer.SaveMedication()
                Callprint(True)
            End If

            Cursor.Current = Cursors.Default
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Cursor.Current = Cursors.Default
        End Try


    End Sub


    'Function to print the report
    Private Sub Callprint(ByVal blnPrint As Boolean)

       
        'Only if there are items in flexgrid then check for duplication.
        Dim returnvisitid As Int64

        Try

            'If _MedBusinessLayer.MedicationCol.Count > 0 Then

            '    If Not _MedBusinessLayer.SaveMedication() Then
            '        Exit Sub
            '    End If
            '    'commented by sagar because no functions available
            '    'If objMedicationDBLayer.AddData(intMode, visitid, returnvisitid) Then--------
            '    '    RefreshMedicationHistory()
            '    '    RefreshUpdatedMedicationList(returnvisitid)
            '    'End If-----------
            'End If


            Dim oRpt As ReportDocument
            oRpt = New ReportDocument
            'code commented temporarily by sarika 13th june 07

            'oRpt.Load(System.Windows.Forms.Application.StartupPath & "\Reports\RptPatientCurrentMedication.rpt")
            'added the startuppath code as asked by madam on MSN on 19 june 2007
            oRpt.Load(gstrgloEMRStartupPath & "\Reports\RptPatientCurrentMedication.rpt")

            '-----------------------
            'sarika 
            'oRpt.Load("D:\gloEMR Working Folder\gloEMR\gloEMR\bin\Reports\rptPatientCurrentMedication.rpt")

            '------------------------
            ' oRpt.Load(Application.StartupPath & "E:\Current Work 2005\MedicationProject\bin\Reports\rptPatientCurrentMedication.rpt")
            'oRpt.Load("E:\Current Work 2005\MedicationProject\bin\Reports\rptPatientCurrentMedication.rpt")
            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table
            Dim TableCounter

            With crConnectionInfo
                .ServerName = gstrSQLServerName

                'If you are connecting to Oracle there is no 
                'DatabaseName. Use an empty string. 
                'For example, .DatabaseName = "" 

                .DatabaseName = gstrDatabaseName

                '.UserID = "Your User ID"
                '.Password = "Your Password"

                'sarika 13th june 2007
                .IntegratedSecurity = True
            End With

            'This code works for both user tables and stored 
            'procedures. Set the CrTables to the Tables collection 
            'of the report 

            CrTables = oRpt.Database.Tables

            'Loop through each table in the report and apply the 
            'LogonInfo information 

            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)

                'If your DatabaseName is changing at runtime, specify 
                'the table location. 
                'For example, when you are reporting off of a 
                'Northwind database on SQL server you 
                'should have the following line of code: 

                CrTable.Location = gstrDatabaseName & ".dbo." & CrTable.Name
            Next

            oRpt.SetParameterValue("PatientID", gnPatientID.ToString())


            If _MedBusinessLayer.MedicationCol.Count > 0 Then
                oRpt.SetParameterValue("VisitID", _MedBusinessLayer.CurrentVisitID)
            Else
                oRpt.SetParameterValue("VisitID", tempVisitId.ToString)

            End If

            'oRpt.PrintToPrinter(1, False, 0, 0)
            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.PHIExport, "Fax is sent.", gstrLoginName, gstrClientMachineName, 0, True)
            'objAudit = Nothing
            mdlFAX.gstrFAXContactPerson = ""
            mdlFAX.gstrFAXContactPersonFAXNo = ""
            mdlFAX.multipleRecipients = False
            mdlFAX.gstrFAXContacts = Nothing

            '---------------------------------
            If blnPrint = False Then
                'sarika internet fax
                If gblnInternetFax = False Then

                    'Check FAX Printer settings are set or not
                    If isPrinterSettingsSet(True) = False Then
                        Exit Sub
                    End If


                    'sarika internet fax
                    'Call SetFAXPrinterDefaultSettings()
                    Try
                        Call MainMenu.SetFAXPrinterDefaultSettings1()
                    Catch ex As Exception
                        MessageBox.Show("Error in medication : " & ex.ToString, "Medication", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try


                    'Retrieve the FAX Cover Page details
                    'Find FAX Parameters
                    'Get Pharmacy FAX No
                    Dim strFAXTo As String
                    Dim strFAXNo As String
                    Dim objmytable As mytable
                    Dim objFAX As New clsFAX


                    'sarika 26th nov 07
                    objmytable = objFAX.GetPharmacyFAXNo(gnPatientID)

                    If Not IsNothing(objmytable) Then
                        gstrFAXContactPersonFAXNo = objmytable.Description
                        gstrFAXContactPerson = objmytable.Code
                    End If

                    If Trim(gstrFAXContactPerson) = "" Then
                        gstrFAXContactPerson = InputBox("Please enter the Pharmacy Name", gstrMessageBoxCaption)
                    End If

                    If Trim(gstrFAXContactPersonFAXNo) = "" Then
                        gstrFAXContactPersonFAXNo = InputBox("Please enter the Pharmacy FAX No", gstrMessageBoxCaption)
                    End If

                    If Trim(gstrFAXContactPersonFAXNo) = "" Then
                        MessageBox.Show("You have not entered the FAX No. So FAX will not be send", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        'code added by sagar because it use to sent fax even after clicked the Ok button of the warning
                        Exit Sub
                    End If

                    'Retrieve FAX Document Name
                    Dim strFAXDocumentName As String
                    strFAXDocumentName = RetrieveFAXDocumentName()

                    'If SetFAXPrinterDocumentSettings(strFAXDocumentName) = False Then Exit Sub
                    If MainMenu.SetFAXPrinterDocumentSettings1(strFAXDocumentName) = False Then Exit Sub

                    objFAX.AddPendingFAX(gnPatientID, gstrFAXContactPerson, "Medication", gstrFAXContactPersonFAXNo, gstrLoginName, strFAXDocumentName, System.DateTime.Now, CurrentSendingFAXPriority)
                    objFAX = Nothing


                    oRpt.PrintOptions.PrinterName = gstrFAXPrinterName
                    oRpt.PrintToPrinter(1, False, 0, 0)
                    ' -----------------------------------


                    'code commented by sarika on 26th nov 07
                    'objmytable = objFAX.GetPharmacyFAXNo(gnPatientID)

                    'If Not IsNothing(objmytable) Then
                    '    gstrFAXContactPersonFAXNo = objmytable.Description
                    '    gstrFAXContactPerson = objmytable.Code
                    'End If

                    'If Trim(gstrFAXContactPerson) = "" Then
                    '    gstrFAXContactPerson = InputBox("Please enter the Pharmacy Name", gstrMessageBoxCaption)
                    'End If

                    'If gblnFAXCoverPage Then
                    '    If RetrieveFAXDetails(mdlFAX.enmFAXType.Medication, gnPatientID, gstrFAXContactPerson, gstrFAXContactPersonFAXNo, "Medication", 0, visitid, 0) = False Then
                    '        Exit Sub
                    '    End If
                    'Else
                    '    If Trim(gstrFAXContactPersonFAXNo) = "" Then
                    '        gstrFAXContactPersonFAXNo = InputBox("Please enter the Pharmacy FAX No", gstrMessageBoxCaption)
                    '    End If
                    'End If

                    'If Trim(gstrFAXContactPersonFAXNo) = "" Then
                    '    MessageBox.Show("You have not entered the FAX No. So FAX will not be send", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    'code added by sagar because it use to sent fax even after clicked the Ok button of the warning
                    '    Exit Sub
                    'End If

                    ''Retrieve FAX Document Name
                    'Dim strFAXDocumentName As String
                    'strFAXDocumentName = RetrieveFAXDocumentName()

                    ''If SetFAXPrinterDocumentSettings(strFAXDocumentName) = False Then Exit Sub
                    'If MainMenu.SetFAXPrinterDocumentSettings1(strFAXDocumentName) = False Then Exit Sub

                    'objFAX.AddPendingFAX(gnPatientID, gstrFAXContactPerson, "Medication", gstrFAXContactPersonFAXNo, gstrLoginName, strFAXDocumentName, System.DateTime.Now, CurrentSendingFAXPriority)
                    'objFAX = Nothing
                    'oRpt.PrintOptions.PrinterName = gstrFAXPrinterName
                    'oRpt.PrintToPrinter(1, False, 0, 0)
                    '-------------------------------------------------


                    ''    'code added by sarika for 1 fax to multiple recipients on 26th nov 07

                    ''    If RetrieveFAXDetails(mdlFAX.enmFAXType.Medication, gnPatientID, gstrFAXContactPerson, gstrFAXContactPersonFAXNo, "Medication", 0, visitid, 0) = False Then
                    ''        Exit Sub
                    ''    End If

                    ''    If multipleRecipients = False Then


                    ''        If Trim(gstrFAXContactPersonFAXNo) = "" Then
                    ''            MessageBox.Show("You have not entered the FAX No. So FAX will not be send", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ''            'sarika 3rd oct 07
                    ''            'the fax is send even then the fax no. is not entered.
                    ''            Exit Sub
                    ''            '----------------------------------
                    ''        End If

                    ''        'Retrieve FAX Document Name
                    ''        Dim strFAXDocumentName As String
                    ''        strFAXDocumentName = RetrieveFAXDocumentName()
                    ''        If MainMenu.SetFAXPrinterDocumentSettings1(strFAXDocumentName) = False Then Exit Sub
                    ''        objFAX.AddPendingFAX(gnPatientID, gstrFAXContactPerson, "Medication", gstrFAXContactPersonFAXNo, gstrLoginName, strFAXDocumentName, System.DateTime.Now, CurrentSendingFAXPriority)
                    ''        oRpt.PrintOptions.PrinterName = gstrFAXPrinterName
                    ''        oRpt.PrintToPrinter(1, False, 0, 0)

                    ''    Else

                    ''        If gstrFAXContacts.Count = 0 Then
                    ''            MessageBox.Show("You have not entered the FAX No. So FAX will not be send", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ''            'sarika 3rd oct 07
                    ''            'the fax is send even then the fax no. is not entered.
                    ''            Exit Sub
                    ''            '----------------------------------
                    ''        End If

                    ''        'Retrieve FAX Document Name
                    ''        Dim strFAXDocumentName As String
                    ''        strFAXDocumentName = RetrieveFAXDocumentName()

                    ''        Dim strFAXDocumentName1 As String = ""
                    ''        strFAXDocumentName1 = strFAXDocumentName

                    ''        For i As Integer = 0 To gstrFAXContacts.Count - 1
                    ''            strFAXDocumentName = strFAXDocumentName1 & i.ToString
                    ''            If MainMenu.SetFAXPrinterDocumentSettings1(strFAXDocumentName) = False Then Exit Sub

                    ''            Dim mynode As myTreeNode

                    ''            mynode = New myTreeNode

                    ''            mynode = CType(gstrFAXContacts.Item(i + 1), myTreeNode)

                    ''            objFAX.AddPendingFAX(gnPatientID, mynode.Text, "Medication", mynode.Tag, gstrLoginName, strFAXDocumentName, System.DateTime.Now, CurrentSendingFAXPriority)
                    ''            oRpt.PrintOptions.PrinterName = gstrFAXPrinterName
                    ''            oRpt.PrintToPrinter(1, False, 0, 0)
                    ''            mynode = Nothing
                    ''        Next



                    ''    End If
                    ''    objFAX = Nothing

                    'sarika internet fax
                Else
                    'Retrieve the FAX Cover Page details
                    'Find FAX Parameters
                    'Get Pharmacy FAX No
                    Dim strFAXTo As String
                    Dim strFAXNo As String
                    Dim objmytable As mytable
                    Dim objFAX As New clsFAX



                    'sarika 26th nov 07
                    objmytable = objFAX.GetPharmacyFAXNo(gnPatientID)

                    If Not IsNothing(objmytable) Then
                        gstrFAXContactPersonFAXNo = objmytable.Description
                        gstrFAXContactPerson = objmytable.Code
                    End If

                    If Trim(gstrFAXContactPerson) = "" Then
                        gstrFAXContactPerson = InputBox("Please enter the Pharmacy Name", gstrMessageBoxCaption)
                    End If

                    If Trim(gstrFAXContactPersonFAXNo) = "" Then
                        gstrFAXContactPersonFAXNo = InputBox("Please enter the Pharmacy FAX No", gstrMessageBoxCaption)
                    End If

                    If Trim(gstrFAXContactPersonFAXNo) = "" Then
                        MessageBox.Show("You have not entered the FAX No. So FAX will not be send", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        'code added by sagar because it use to sent fax even after clicked the Ok button of the warning
                        Exit Sub
                    End If

                    Dim objFaxReport As New clsPrintFaxReport
                    objFaxReport.FaxReport(oRpt)
                    'sarika internet fax
                End If
            Else
                'print
                'sarika Show Print Dialog 20090309
                '                        oRpt.PrintToPrinter(1, False, 0, 0)


                If gblnUseDefaultPrinter = False Then


                    ' PrintDialog1.UseEXDialog = True
                    PrintDialog1 = New PrintDialog()
                    '            PrintDialog1.ShowDialog()
                    'If PrintDialog1.Then Then
                   

                    If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then

                        oRpt.PrintOptions.PrinterName = PrintDialog1.PrinterSettings.PrinterName
                        oRpt.Load(Application.StartupPath & "\Reports\RptPatientCurrentMedication.rpt")

                        oRpt.PrintToPrinter(1, False, 0, 0)

                        If Not IsNothing(oRpt) Then
                            oRpt.Close()
                        End If


                    End If
                Else
                    oRpt.PrintToPrinter(1, False, 0, 0)
                    '------
                End If
            End If

            Dim objAudit As New clsAudit
            objAudit.CreateLog(clsAudit.enmActivityType.PHIExport, "Fax is sent.", gstrLoginName, gstrClientMachineName, 0, True)
            objAudit = Nothing

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub _MedToolBarUserCtrl_tStrpPrvRxClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _MedToolBarUserCtrl.tStrpPrvRxClick
        Try

            If Not IsNothing(objCustomMedication) Then
                RemoveControl()
            End If
            AddRefillControl()
            _MedToolBarUserCtrl.tStrpShowHide.Text = "Show"
            _MedToolBarUserCtrl.tStrpShowHide.ToolTipText = "Show Prescription History"
            'If Me.pnlRefill.Controls.Contains(objCustomMedication) = True Then
            '    RemoveControl()
            'End If

            'splRight.Visible = False
            'pnlRight.Visible = False

            'Me.pnlRefill.Controls.Add(_MedRefillC1FlexGridUserCtrl)
            '_MedRefillC1FlexGridUserCtrl.Visible = True
            '_MedRefillC1FlexGridUserCtrl.Dock = DockStyle.Fill
            '_MedRefillC1FlexGridUserCtrl.BringToFront()
            'pnlRefill.BringToFront()
            'pnlRefill.Visible = True

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub


    '**************
    Private Sub _MedRefillC1FlexGridUserCtrl_btnCloseRefillClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _MedRefillC1FlexGridUserCtrl.btnCloseRefillClick
        Try
            pnlRefill.Visible = False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.Finish, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub
    '**************

    Private Sub _MedRefillC1FlexGridUserCtrl_cntListmenuStripClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _MedRefillC1FlexGridUserCtrl.cntListmenuStripClick
        Try
            _MedicationC1FlexGrdUserCtrl.AddandSetFlexgridData(_MedBusinessLayer.MedicationCol.Item(_MedBusinessLayer.MedicationCol.Count - 1))
        Catch ex As Exception

        End Try
        'Try
        '    Cursor.Current = Cursors.WaitCursor
        '    If gblnClinicDIAlert Then
        '        If gblnDIAlert Then
        '            RefreshDIScreen()
        '            If _MedBusinessLayer.MedicationCol.Count > 0 Then
        '                'check screening result ,if true then show the count
        '                Dim strmessage As String = ""
        '                Dim strmessage1 As String = ""
        '                If PerformAutoScreening(False, strmessage, strmessage1) Then
        '                    DisplayAlertForUncodedDrugs()
        '                    Dim result As Int16
        '                    'Notes need to be added
        '                    result = MessageBox.Show(strmessage & "Do you want to Override the alert and continue prescribing drugs?", "Medication", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        '                    If result = vbYes Then
        '                        _MedBusinessLayer.SaveMedication()
        '                        _MedicationC1FlexGrdUserCtrl.AddandSetFlexgridData(_MedBusinessLayer.MedicationCol.Item(_MedBusinessLayer.MedicationCol.Count - 1))
        '                    Else
        '                        'Me.Close()
        '                        _MedicationC1FlexGrdUserCtrl.AddandSetFlexgridData(_MedBusinessLayer.MedicationCol.Item(_MedBusinessLayer.MedicationCol.Count - 1))
        '                        Exit Sub
        '                    End If
        '                    'Drug alert for some

        '                    'No Positive Drug Screening
        '                Else
        '                    DisplayAlertForUncodedDrugs()
        '                    _MedBusinessLayer.SaveMedication()
        '                    _MedicationC1FlexGrdUserCtrl.AddandSetFlexgridData(_MedBusinessLayer.MedicationCol.Item(_MedBusinessLayer.MedicationCol.Count - 1))
        '                End If
        '                'Do not perform screening as no items in medication
        '            End If
        '            'Machine Level DI alert turned off
        '        Else
        '            _MedBusinessLayer.SaveMedication()
        '            _MedicationC1FlexGrdUserCtrl.AddandSetFlexgridData(_MedBusinessLayer.MedicationCol.Item(_MedBusinessLayer.MedicationCol.Count - 1))
        '        End If
        '        'Clinic level DI Alert turned off
        '    Else
        '        _MedBusinessLayer.SaveMedication()
        '        _MedicationC1FlexGrdUserCtrl.AddandSetFlexgridData(_MedBusinessLayer.MedicationCol.Item(_MedBusinessLayer.MedicationCol.Count - 1))
        '    End If

        '    Cursor.Current = Cursors.Default
        'Catch ex As Exception
        '    Cursor.Current = Cursors.Default
        'End Try

       
    End Sub

    Private Sub _MedListUserCtrl_CheckDrugInteraction(ByVal DrugID As Long) Handles _MedListUserCtrl.CheckDrugInteraction
        'Try
        '    If gblnClinicDIAlert Then
        '        If gblnDIAlert Then
        '            If gblnMedAlert Then
        '                If PerformAutoScreening(DrugID) Then
        '                    'If PerformAutoScreening(_MedBusinessLayer.MedicationCol.Item(_MedBusinessLayer.MedicationCol.Count - 1).DDID) Then
        '                    Dim objsender As Object
        '                    Dim obje As System.EventArgs
        '                    'objDrugInteraction_DIScreen_Click1(objsender, obje, btntype, _MedBusinessLayer.MedicationCol.Item(_MedBusinessLayer.MedicationCol.Count - 1).DDID)
        '                    objDrugInteraction_DIScreen_Click1(objsender, obje, btntype, DrugID)
        '                End If
        '                DisplayAlertForUncodedDrugs()

        '            End If
        '        End If
        '    End If

        'Catch ex As Exception

        'End Try
        Try
            If gblnClinicDIAlert Then
                If gblnDIAlert Then
                    If gblnMedAlert Then
                        If PerformAutoScreening(DrugID) Then
                            'If PerformAutoScreening(_MedBusinessLayer.MedicationCol.Item(_MedBusinessLayer.MedicationCol.Count - 1).DDID) Then
                            Dim objsender As Object
                            Dim obje As System.EventArgs
                            'objDrugInteraction_DIScreen_Click1(objsender, obje, btntype, _MedBusinessLayer.MedicationCol.Item(_MedBusinessLayer.MedicationCol.Count - 1).DDID)
                            objDrugInteraction_DIScreen_Click1(objsender, obje, btntype, DrugID)
                        End If
                        DisplayAlertForUncodedDrugs()

                    End If
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub _MedListUserCtrl_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles _MedListUserCtrl.KeyPress
        Try
            'Code to add drug to collection has been handled here 
            'if drug is valid drug then add it to the flexgrid
            If _MedListUserCtrl.ValidDrug Then
                _MedicationC1FlexGrdUserCtrl.AddandSetFlexgridData(_MedBusinessLayer.MedicationCol.Item(_MedBusinessLayer.MedicationCol.Count - 1))

                If Not IsNothing(objCustomMedication) Then
                    RemoveControl()
                End If
            End If
        Catch ex As gloUserControlExceptions
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub

    Private Sub _MedListUserCtrl_cntListmenuStripClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _MedListUserCtrl.cntListmenuStripClick
        'Show Drug Master
        Try
            If sender.Text = "Add Provider Specific Drugs" Then
                Dim objfrmDrugProviderAssociation As New frmDrugProviderAssociation
                objfrmDrugProviderAssociation.Text = "Add Provider Specific Drugs"
                objfrmDrugProviderAssociation.ShowDialog()
                _MedListUserCtrl.RefreshDrugList()
            Else
                Dim objfrmMSTDrugs As New frmMSTDrugs
                objfrmMSTDrugs.Text = "Add New Drugs"
                objfrmMSTDrugs.ShowDialog()
                'code for RefreshDrugList() need to be added in the user control
                _MedListUserCtrl.RefreshDrugList()
                'Catch ex As gloEMRDatabase.gloDBException
            End If


        Catch ex As MedicationBusinessLayerException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        Catch ex As gloUserControlExceptions
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub

    Private Sub _MedListUserCtrl_trvDoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _MedListUserCtrl.trvDoubleClick
        Try
            'Code to add drug to collection has been handled here 
            'if drug is valid drug then add it to the flexgrid
            If _MedListUserCtrl.ValidDrug Then
                'If gblnmedalert Then
                '    If PerformAutoScreening(_MedBusinessLayer.MedicationCol.Item(_MedBusinessLayer.MedicationCol.Count - 1).DDID) Then
                '        'If PerformAutoScreening(_MedBusinessLayer.MedicationCol.Item(_MedBusinessLayer.MedicationCol.Count - 1).DDID) Then
                '        Dim objsender As Object
                '        Dim obje As System.EventArgs
                '        'objDrugInteraction_DIScreen_Click1(objsender, obje, btntype, _MedBusinessLayer.MedicationCol.Item(_MedBusinessLayer.MedicationCol.Count - 1).DDID)
                '        objDrugInteraction_DIScreen_Click1(objsender, obje, btntype, _MedBusinessLayer.MedicationCol.Item(_MedBusinessLayer.MedicationCol.Count - 1).DDID)
                '    End If
                '    DisplayAlertForUncodedDrugs()

                'End If
                _MedicationC1FlexGrdUserCtrl.AddandSetFlexgridData(_MedBusinessLayer.MedicationCol.Item(_MedBusinessLayer.MedicationCol.Count - 1))

                If Not IsNothing(objCustomMedication) Then
                    RemoveControl()
                End If
            End If

        Catch ex As gloUserControlExceptions
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub
    Public Function ShowMedication() As Int16
        Try
            Dim retval As Int16
            retval = _MedBusinessLayer.PopulateMedicationHistory(arrdrugs, blncancel)
            If retval = 3 Then
                '' No
                If blnOpenFromExam = True Then
                    If Not IsNothing(myCaller) Then
                        myCaller.GetdataFromOtherForms(gloEMRWord.enumDocType.Medication)

                    End If
                    If Not IsNothing(myLetter) Then
                        myLetter.GetdataFromOtherForms(gloEMRWord.enumDocType.Medication)
                    End If
                End If
                blnOpenFromExam = False
            End If
            Return retval  '' 1 = Cancel, 2= Yes , 3 No
        Catch ex As gloDBException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            blncancel = True
        Catch ex As MedicationDatabaseLayerException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            blncancel = True
        Catch ex As MedicationBusinessLayerException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            blncancel = True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            blncancel = True
        End Try
    End Function

    Private Sub frmMedication_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            If blnOpenFromExam = True Then
                If Not IsNothing(myCaller) Then
                    myCaller.GetdataFromOtherForms(gloEMRWord.enumDocType.Medication)

                End If
                If Not IsNothing(myLetter) Then
                    myLetter.GetdataFromOtherForms(gloEMRWord.enumDocType.Medication)
                End If
            End If
            blnOpenFromExam = False

            '' SUDHIR 20090720 ''
            If myCallerSynopsis IsNot Nothing Then
                myCallerSynopsis.ofrmMeds_FormClosed(Nothing, Nothing)
            End If
            '' END SUDHIR ''

            '' <><><> Unlock the Record <><><>
            '' Mahesh - 20070726
            If clsgeneral._blnRecordLock_Med = False Then
                '' if the Locked by by the Current User & on Current Machine only
                UnLock_Transaction(TrnType.Medication, gnPatientID, _MedBusinessLayer.CurrentVisitID, Now)
            End If
            '' <><><> Unlock the Record <><><>

            'gloAuditTrail.gloAuditTrail.CreateLog(clsAudit.enmActivityType.Other, "Patient Medication Closed", gstrLoginName, gstrClientMachineName, gnPatientID)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.Close, "Patient Medication Closed", gloAuditTrail.ActivityOutCome.Success)
            Me.Dispose()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmMedication_New_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            'dont show the RefillRequest/Approve/Deny buttons, when u are in medication form
            _MedToolBarUserCtrl.tStrpRefReq.Visible = False
            _MedToolBarUserCtrl.tStrpSendRx.Visible = False
            '_MedToolBarUserCtrl.tStrpDeny.Visible = False


            AddMedControls()

            _MedBusinessLayer.GetLatestMedicalConditions()

            If _MedBusinessLayer.MedicationCol.Count > 0 Then
                If Not IsNothing(_MedBusinessLayer.CurrentVisitDate) Then
                    If _MedBusinessLayer.CurrentVisitDate <> "12:00:00 AM" Then
                        'Following is code to Set the datetime picker visitdate to visitdate that has been retrieved
                        _MedPatientStrip.DTPValue = _MedBusinessLayer.CurrentVisitDate
                        _MedPatientStrip.DTPEnabled = False
                        _MedPatientStrip.ShowDetail(globalPatient.gnPatientID, gloUC_PatientStrip.enumFormName.Prescription)
                        _MedPatientStrip.Padding = New Padding(3, 0, 3, 0)
                    End If

                End If
                _MedicationC1FlexGrdUserCtrl.BindFlexgrid()
                'Code to load the DI control
                'PerformAutoScreening(False, "", "")
                _MedicationC1FlexGrdUserCtrl.RowIndex = 1
            End If
            'Check if Clinic Level DI Alert is Set
            If gblnClinicDIAlert Then
                'Check if Machine Level DI Alert is Set
                If gblnDIAlert Then
                    Dim ptdat As Date = CType(gstrPatientDOB, Date)
                    ClsDIGeneral.PatientAgeInDays = DateDiff(DateInterval.Day, ptdat, Now.Date)

                    objDrugInteraction = New DIToolbar
                    AddDrugInteractionControl()
                End If
            End If

            pnlAllergiesAlerts.Visible = False
            Call GetAllergies()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub

    'Function invoked to remove the Medication Details User Control from the form
    Private Sub RemoveControl()
        'Not using custom grid so commented this section of the code
        'If Not IsNothing(objCustomGrid) Then
        '    Me.Controls.Remove(objCustomGrid)
        '    objCustomGrid.Visible = False
        '    objCustomGrid = Nothing
        '    rowindex = 0
        'End If
        'Not using custom grid so commented this section of the code
        If Not IsNothing(objCustomMedication) Then
            Me.Controls.Remove(objCustomMedication)
            objCustomMedication.Visible = False
            objCustomMedication = Nothing
            pnlRefill.Visible = False
        End If
        RemoveRefillControl()
    End Sub
    Private Sub _MedHistoryUserCtrl_cntListmenuStripClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _MedHistoryUserCtrl.cntListmenuStripClick
        Try
            _MedicationC1FlexGrdUserCtrl.AddandSetFlexgridData(_MedBusinessLayer.MedicationCol.Item(_MedBusinessLayer.MedicationCol.Count - 1))
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub _MedToolBarUserCtrl_tStrpSaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _MedToolBarUserCtrl.tStrpSaveClick
        Try
            UpdateLog("********************************Saving MEDICATION****************************")
            Cursor.Current = Cursors.WaitCursor
            If gblnClinicDIAlert Then
                UpdateLog("Clinic Drug Interaction alert setting is  True.......................")
                If gblnDIAlert Then
                    UpdateLog("Drug Interaction alert setting is  True.......................")
                    RefreshDIScreen()
                    If _MedBusinessLayer.MedicationCol.Count > 0 Then
                        'check screening result ,if true then show the count
                        Dim strmessage As String = ""
                        Dim strmessage1 As String = ""
                        If PerformAutoScreening(False, strmessage, strmessage1) Then
                            UpdateLog("Performing auto Screening is True.......................")
                            DisplayAlertForUncodedDrugs()
                            Dim result As Int16
                            'to Supress Drug alert Message
                            MessageBox.Show(strmessage, "Medication", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            If gblnDrugAlertMsg = True Then
                                result = MessageBox.Show("Do you want to override the alert and continue saving medication?", "Medication", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                If result = vbYes Then
                                    UpdateLog("Medication save Started.......................")
                                    If _MedBusinessLayer.SaveMedication() Then
                                        UpdateLog("Medication save Completed.......................")
                                        Me.Close()
                                    End If
                                Else
                                    Me.Close()
                                    Exit Sub
                                End If
                            Else
                                UpdateLog("Medication save Started.......................")

                                If _MedBusinessLayer.SaveMedication() Then
                                    UpdateLog("Medication save Completed.......................")
                                    Me.Close()
                                End If


                            End If
                            'to Supress Drug alert Message

                            '--------original code
                            ''Notes need to be added
                            'result = MessageBox.Show(strmessage & "Do you want to Override the alert and continue saving medication?", "Medication", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            'If result = vbYes Then
                            '    UpdateLog("Medication save Started.......................")
                            '    _MedBusinessLayer.SaveMedication()
                            '    UpdateLog("Medication save Completed.......................")
                            '    Me.Close()
                            'Else
                            '    Me.Close()
                            '    Exit Sub
                            'End If
                            ''Drug alert for some

                            ''No Positive Drug Screening
                            '--------original code
                        Else
                            UpdateLog("Performing auto Screening is False.......................")
                            DisplayAlertForUncodedDrugs()
                            UpdateLog("Medication save started.......................")

                            If _MedBusinessLayer.SaveMedication() Then
                                UpdateLog("Medication save Completed.......................")
                                Me.Close()
                            End If


                        End If
                        'Do not perform screening as no items in medication
                    Else
                        Me.Close()
                    End If
                    'Machine Level DI alert turned off

                Else
                    UpdateLog("Drug Interaction alert setting is  False.......................")
                    UpdateLog("Medication save Started.......................")
                    If _MedBusinessLayer.MedicationCol.Count > 0 Then
                        If _MedBusinessLayer.SaveMedication() Then
                            UpdateLog("Medication save Completed.......................")
                            ' issue reported by shailesh that we shall not close the form after the user is promted for duplication of medications
                            Me.Close()

                        End If
                    Else
                        Me.Close()
                    End If




                End If
                'Clinic level DI Alert turned off
            Else
                UpdateLog("Clinic Drug Interaction alert setting is  False.......................")
                UpdateLog("Medication save Started.......................")
                If _MedBusinessLayer.MedicationCol.Count > 0 Then
                    If _MedBusinessLayer.SaveMedication() Then
                        UpdateLog("Medication save Completed.......................")
                        Me.Close()
                    End If
                Else
                    Me.Close()
                End If


            End If

            Cursor.Current = Cursors.Default
        Catch ex As MedicationBusinessLayerException
            Cursor.Current = Cursors.Default
            UpdateLog(ex.Message & ":" & ex.Source)

        Catch ex As Exception
            UpdateLog(ex.Message & ":" & ex.Source)
            Cursor.Current = Cursors.Default
        End Try

    End Sub
    Private Sub _MedToolBarUserCtrl_tStrpShowHideClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _MedToolBarUserCtrl.tStrpShowHideClick
        Try
            If pnlRight.Visible = False Then
                _MedToolBarUserCtrl.tStrpShowHide.Text = "Hide"
                _MedToolBarUserCtrl.tStrpShowHide.ToolTipText = "Hide Prescription History"
                pnlRight.Visible = True
                splRight.Visible = True
            Else
                _MedToolBarUserCtrl.tStrpShowHide.Text = "Show"
                _MedToolBarUserCtrl.tStrpShowHide.ToolTipText = "Show Prescription History"
                pnlRight.Visible = False
                splRight.Visible = False
            End If
            ' _MedHistoryUserCtrl.RefreshMedicationHistory()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub _MedPatientStrip_ControlSizeChanged() Handles _MedPatientStrip.ControlSizeChanged
        Try
            'pnlcentertop.Height = _MedPatientStrip.Height
        Catch ex As Exception

        End Try

    End Sub

    Private Sub objCustomMedication_CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles objCustomMedication.CloseClick

        Try
            RemoveControl()
            'Add Code to select the first row by default in the FlexGrid
            '***************************************************************

            'Code has been Commented temporarily as we are not using objcustomgrid
            'If Not IsNothing(objCustomGrid) Then
            '    objCustomGrid = Nothing
            'End If
            'Code has been Commented temporarily as we are not using objcustomgrid
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub

    'Save the data in Medication Details User Control to Collection
    Private Sub objCustomMedication_OKClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles objCustomMedication.OKClick
        Try
            'Save data in customMedication object
            'Also save data in flexgrid
            _MedicationC1FlexGrdUserCtrl.SetFlexGridData()
            _MedBusinessLayer.CheckDuplicateDrug()
            RemoveControl()
        Catch ex As gloUserControlLibrary.gloUserControlExceptions
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub _MedicationC1FlexGrdUserCtrl__FlexClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _MedicationC1FlexGrdUserCtrl._FlexClick

        'Try
        '    If _MedBusinessLayer.MedicationCol.Count > 0 Then

        '        'If _MedRefillC1FlexGridUserCtrl.Visible = True Then
        '        '    Me.Controls.Remove(_MedRefillC1FlexGridUserCtrl)
        '        '    '_MedRefillC1FlexGridUserCtrl.Visible = False
        '        '    _MedRefillC1FlexGridUserCtrl = Nothing
        '        'End If
        '        If pnlRefill.Controls.Contains(_MedRefillC1FlexGridUserCtrl) = True Then
        '            pnlRefill.Controls.Remove(_MedRefillC1FlexGridUserCtrl)
        '            pnlRefill.Visible = False
        '        End If

        '        'If _MedRefillC1FlexGridUserCtrl.Visible = True Then
        '        '    Me.Controls.Remove(_MedRefillC1FlexGridUserCtrl)
        '        '    _MedRefillC1FlexGridUserCtrl.Visible = False

        '        'End If
        '        AddControl()
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub objCustomMedication_SigClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles objCustomMedication.SigClick
        Try
            'If Not IsNothing(ObjcustomGrid) Then
            '    Me.Controls.Remove(ObjcustomGrid)
            '    ObjcustomGrid.Visible = False
            '    ObjcustomGrid = Nothing
            '    rowindex = 0
            'End If
            'ObjcustomGrid = New CustomDataGrid
            'Me.Controls.Add(ObjcustomGrid)
            ''objCustomMedication.Controls.Add(ObjcustomGrid)
            ''trMedicationDetails.Controls.Add(objCustomGrid)
            'ObjcustomGrid.Visible = True
            'ObjcustomGrid.Dock = DockStyle.Bottom

            ''objCustomGrid.Anchor = AnchorStyles.Right
            ''objCustomGrid.Height = objCustomMedication.Height
            ''objCustomGrid.Top = objCustomMedication.Top
            ''ObjcustomGrid.Width = trMedicationDetails.Width / 1.58
            ''GridStatus = 1-------------
            ''BindGrid()------------
            '--------------------------

            If Not IsNothing(objSigControl) Then
                Me.Controls.Remove(objSigControl)
                objSigControl.Visible = False
                objSigControl = Nothing
                rowindex = 0
            End If
            'objSigControl = New CustomDataGrid
            GridStatus = 1

            BindSigGrid()
            objSigControl = New gloUC_CustomSearchInC1Flexgrid(dt, False)
            'SetSigGridStyle()

            '-----------------

            'With objSigControl._flexObj
            'objSigControl._flexObj.Cols.Fixed = 0
            'objSigControl._flexObj.Font = New Font("Verdana", 9, FontStyle.Regular)
            'objSigControl._flexObj.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
            'objSigControl._flexObj.BackColor = System.Drawing.Color.White

            'objSigControl._flexObj.Styles.Alternate.BackColor = Color.FromArgb(181, 216, 241)
            'objSigControl._flexObj.Styles.Alternate.Border.Color = Color.Black
            'objSigControl._flexObj.Styles.Alternate.ForeColor = SystemColors.WindowText


            'objSigControl._flexObj.Styles.Editor.BackColor = Color.Beige
            'objSigControl._flexObj.Styles.Editor.Border.Color = Color.Black
            'objSigControl._flexObj.Styles.Editor.ForeColor = SystemColors.WindowText


            '.Styles.EmptyArea.BackColor = Color.FromArgb(242, 242, 242)
            '.Styles.EmptyArea.Border.Color = Drawing.SystemColors.ControlDarkDark
            '.Styles.EmptyArea.ForeColor = SystemColors.WindowText


            '.Styles.Fixed.BackColor = Color.FromArgb(4, 96, 162)
            '.Styles.Fixed.Border.Color = Drawing.SystemColors.ControlDark
            '.Styles.Fixed.ForeColor = Color.White


            '.Styles.Focus.BackColor = Color.FromArgb(255, 197, 108)
            '.Styles.Focus.Border.Color = Color.Black
            '.Styles.Focus.ForeColor = SystemColors.WindowText


            '.Styles.Frozen.BackColor = Color.Beige
            '.Styles.Frozen.Border.Color = Color.Black
            '.Styles.Frozen.ForeColor = SystemColors.WindowText


            '.Styles.Highlight.BackColor = Color.FromArgb(255, 197, 108)
            '.Styles.Highlight.Border.Color = Color.Black
            '.Styles.Highlight.ForeColor = SystemColors.HighlightText


            '.Styles.NewRow.BackColor = SystemColors.Window
            '.Styles.NewRow.Border.Color = Color.Black
            '.Styles.NewRow.ForeColor = SystemColors.WindowText


            '.Styles.Normal.BackColor = SystemColors.Window
            '.Styles.Normal.Border.Color = Color.Black
            '.Styles.Normal.ForeColor = SystemColors.WindowText


            '.Styles.Search.BackColor = Color.FromArgb(255, 197, 108)
            '.Styles.Search.Border.Color = Color.Black
            '.Styles.Search.ForeColor = SystemColors.HighlightText


            ' End With
            '------------------
            Me.Controls.Add(objSigControl)

            'objSigControl.Visible = True
            'objSigControl.Dock = DockStyle.Bottom

            'Me.pnlcenter.Controls.Add(objSigControl)
            Me.pnlRefill.Controls.Add(objSigControl)



            objSigControl.Dock = DockStyle.Fill
            objSigControl.Visible = True
            objSigControl.BringToFront()

            Dim searchrow As Integer

            If sigid <> 0 Then
                searchrow = objSigControl._UCflex.FindRow(sigid, 0, 0, False, True, False)
                objSigControl._UCflex.Select(searchrow, 0, True)
                sigid = 0
            Else
                objSigControl._UCflex.Select(1, 0, True)
            End If

            'objSigControl.Anchor = AnchorStyles.Right
            'objSigControl.Height = objCustomMedication.Height + 65
            ''objSigControl.Top = objCustomMedication.Top
            'objSigControl.Width = objCustomMedication.Width ' trMedicationDetails.Width / 1.58



        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Medication", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '********************comented by sagar as we r not using the custom data grid & instead of that v use Bipins control
    'Private Sub ObjcustomGrid_AddClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ObjcustomGrid.AddClick

    '    'Dim objfrmMSTSIG As New frmMSTSIG
    '    'Try
    '    '    objfrmMSTSIG.Text = "Add New SIG"
    '    '    objfrmMSTSIG.ShowDialog()
    '    '    BindGrid()
    '    '    If objMedicationDBLayer.DsDataview.Count > 0 Then
    '    '        ObjcustomGrid.GetSelect(rowindex)
    '    '    End If
    '    'Catch ex As Exception
    '    '    MessageBox.Show(ex.ToString, "SIG", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    'Finally
    '    '    objfrmMSTSIG = Nothing
    '    'End Try
    'End Sub

    '********************comented by sagar as we r not using the custom data grid & instead of that v use Bipins control
    'Private Sub ObjcustomGrid_CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ObjcustomGrid.CloseClick
    '    If Not IsNothing(ObjcustomGrid) Then
    '        Me.Controls.Remove(ObjcustomGrid)
    '        ObjcustomGrid.Visible = False
    '        ObjcustomGrid = Nothing
    '        rowindex = 0
    '    End If
    'End Sub

    '********************comented by sagar as we r not using the custom data grid & instead of that v use Bipins control
    'Private Sub ObjcustomGrid_Dblclick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ObjcustomGrid.Dblclick
    '    Try
    '        'SetSigDetails()-------
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Medication", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    '********************comented by sagar as we r not using the custom data grid & instead of that v use Bipins control
    'Private Sub ObjcustomGrid_GridKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ObjcustomGrid.GridKeyPress
    '    Try
    '        'SetSigDetails()--------
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Medication", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub
    '********************comented by sagar as we r not using the custom data grid & instead of that v use Bipins control
    'Private Sub ObjcustomGrid_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ObjcustomGrid.MouseUp
    '    If ObjcustomGrid.GetCurrentrowIndex > 0 Then
    '        ObjcustomGrid.GetSelect(ObjcustomGrid.GetCurrentrowIndex)
    '        rowindex = ObjcustomGrid.GetCurrentrowIndex
    '    End If
    'End Sub
    '********************comented by sagar as we r not using the custom data grid & instead of that v use Bipins control
    'Private Sub ObjcustomGrid_OKClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ObjcustomGrid.OKClick
    '    Try
    '        'SetSigDetails()------------
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Medication", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub _MedHistoryUserCtrl_MedicationLoaded() Handles _MedHistoryUserCtrl.MedicationLoaded
        Try
            If gblnClinicDIAlert Then
                If gblnDIAlert Then
                    UnloadDrugInteractionControl()
                    If Not IsNothing(objDrugInteraction) Then
                        objDrugInteraction.RefreshToolBar()
                    End If
                End If
            End If
            _MedicationC1FlexGrdUserCtrl.BindFlexgrid()
            _MedPatientStrip.DTPValue = _MedBusinessLayer.CurrentVisitDate
            _MedPatientStrip.DTPEnabled = False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub _MedBusinessLayer_DisplayMessage(ByVal strmessage As String, ByRef intresponse As Short) Handles _MedBusinessLayer.DisplayMessage
        Dim strresult As DialogResult
        Try
            strresult = MessageBox.Show(strmessage, gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If strresult = Windows.Forms.DialogResult.Cancel Then
                intresponse = 1
            ElseIf strresult = Windows.Forms.DialogResult.Yes Then
                intresponse = 2
            ElseIf strresult = Windows.Forms.DialogResult.No Then
                intresponse = 3
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub
    Private Sub _MedBusinessLayer_DrugDuplication(ByVal ValidationMessage As String) Handles _MedBusinessLayer.DrugDuplication
        MessageBox.Show(ValidationMessage, "Medication", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    Private Sub _MedBusinessLayer_MedicationDeleted() Handles _MedBusinessLayer.MedicationDeleted
        Try

            'Enables disables medication icons for history node
            _MedHistoryUserCtrl.RefreshMedicationHistory()

            If gblnClinicDIAlert Then
                If gblnDIAlert Then
                    UnloadDrugInteractionControl()
                    If Not IsNothing(objDrugInteraction) Then
                        objDrugInteraction.RefreshToolBar()
                    End If
                End If
            End If
            RemoveControl()
            _MedicationC1FlexGrdUserCtrl.ClearRows()
            strnodetype = ""
            _MedBusinessLayer.FilterType = "Active"

            'cmbMedStatus.Text = "Active"
            'MessageBox.Show("Patient Medication is deleted sucessfully", "Medication", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub _MedBusinessLayer_MedicationSaveStatus(ByVal blnsaved As Boolean) Handles _MedBusinessLayer.MedicationSaveStatus
        Try
            '_MedBusinessLaye.MedicationSaveStatus()
            _MedHistoryUserCtrl.RefreshMedicationHistory()
            If blnsaved Then
                '_RxPatientStrip.DTPValue = _RxBusinessLayer.Visitdate-----------
                _MedPatientStrip.DTPValue = _MedBusinessLayer.CurrentVisitDate 'no visitdate so used currentvisitdate
                _MedPatientStrip.DTPEnabled = False
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub FillDrugsforDI(Optional ByVal drugid As Int64 = 0)
        strUncodedDrugs = New System.Text.StringBuilder
        Dim i As Int32
        'For NDC Interaction
        Dim ddid As Int64
        Dim sNDCCode As String
        'For NDC Interaction

        'Add Rx items to the objdruginteraction collection which will be used for screening drugs
        'Dim mynode As TreeNode
        'Check if Rx items present
        'If objPrescriptionDBLayer.PrescriptionColCount > 0 Then

        'If intMode = 1 Or (intMode = 2 And objMedicationDBLayer.FilterType = "Active") Then-----------------
        ' If intMode = 1 Or (intMode = 2 And _MedBusinessLayer.FilterType = "Active") Then
        If _MedBusinessLayer.TransactionMode = MedicationBusinessLayer._TransactionMode.Add Or (_MedBusinessLayer.TransactionMode = MedicationBusinessLayer._TransactionMode.Edit And _MedBusinessLayer.FilterType = "Active") Then

            'If C1FlexGrdMedicationDetails.Rows.Count > 0 Or drugid <> 0 Then-----------------
            If _MedBusinessLayer.MedicationCol.Count > 0 Or drugid <> 0 Then
                'ddid = 0
                sNDCCode = ""
                Dim strstatus As String
                'For i = 1 To C1FlexGrdMedicationDetails.Rows.Count - 1-----------------
                For i = 0 To _MedBusinessLayer.MedicationCol.Count - 1
                    'ddid = RetrieveDDID(CType(mynode.Nodes.Item(i), myTreeNode).Key)
                    ' strstatus = objMedicationDBLayer.GetStatus(i - 1)-------------
                    strstatus = _MedBusinessLayer.MedicationCol.Item(i).Status
                    If strstatus = "Active" Or strstatus = "" Then
                        'Dim drgId As Long = C1FlexGrdMedicationDetails.GetData(i, COL_DRUGID)
                        'ddid = C1FlexGrdMedicationDetails.GetData(i, COL_DDID) ' code added by sagar
                        'ddid = _MedBusinessLayer.MedicationCol.Item(i).DDID
                        sNDCCode = _MedBusinessLayer.MedicationCol.Item(i).NDCCode
                        'If ddid > 0 Then
                        '    objDrugInteraction.AddDrugtocol(ddid)
                        'Else
                        '    'strUncodedDrugs.Append(mynode.Nodes.Item(i).Text)'''''code commented by sagar to add the code written on next line for C1FlexGrid
                        '    'strUncodedDrugs.Append(C1FlexGrdMedicationDetails.GetData(i, COL_MEDICATION)) '''''code added by sagar 
                        '    strUncodedDrugs.Append(_MedBusinessLayer.MedicationCol.Item(i).Medication)
                        '    strUncodedDrugs.Append(vbCr)
                        'End If
                        If sNDCCode <> "" Then
                            objDrugInteraction.AddDrugtocol(sNDCCode)
                        Else
                            'strUncodedDrugs.Append(mynode.Nodes.Item(i).Text)'''''code commented by sagar to add the code written on next line for C1FlexGrid
                            'strUncodedDrugs.Append(C1FlexGrdMedicationDetails.GetData(i, COL_MEDICATION)) '''''code added by sagar 
                            strUncodedDrugs.Append(_MedBusinessLayer.MedicationCol.Item(i).Medication & _MedBusinessLayer.MedicationCol.Item(i).Dosage & " is an uncoded drug and will not be screened against drug interaction database")
                            strUncodedDrugs.Append(vbCr)
                        End If
                    End If
                    strstatus = ""
                    'ddid = 0
                    sNDCCode = ""
                Next

                '  If intMode = 1 Then
                If _MedBusinessLayer.TransactionMode = MedicationBusinessLayer._TransactionMode.Add Then
                    Dim arrlist As New ArrayList
                    'get list of active medications for given visit from the database
                    'arrlist = objMedicationDBLayer.GetActiveMedications(m_visitdate, 0)
                    arrlist = _MedBusinessLayer.GetActiveMedications()

                    'ddid = 0
                    sNDCCode = ""

                    If arrlist.Count > 0 Then
                        For i = 0 To arrlist.Count - 1
                            'ddid = CType(arrlist.Item(i), mytable).Unit*************function available in class myTable
                            'This Id is declared in clsgloEMRActors which is the instead of myTable we use generalList class that has Id & description as property procedures

                            'ddid = CType(arrlist.Item(i), generalList).ID
                            sNDCCode = CType(arrlist.Item(i), generalList).ID

                            'if the ddid is not zero add it for screening purpose
                            'If ddid > 0 Then
                            '    objDrugInteraction.AddDrugtocol(ddid)
                            '    'if drug id is not zero then add it to string builder variable
                            '    'to alert the user that drug screening will not be performed
                            '    'for the same.
                            'Else
                            '    'strUncodedDrugs.Append(CType(arrlist.Item(i), mytable).Code)************include the mytable class                                strUncodedDrugs.Append(vbCr)
                            '    strUncodedDrugs.Append(CType(arrlist.Item(i), generalList).Description)
                            'End If
                            'ddid = 0

                            If sNDCCode <> "" Then
                                objDrugInteraction.AddDrugtocol(sNDCCode)
                                'if drug id is not zero then add it to string builder variable
                                'to alert the user that drug screening will not be performed
                                'for the same.
                            Else
                                'strUncodedDrugs.Append(CType(arrlist.Item(i), mytable).Code)************include the mytable class                                strUncodedDrugs.Append(vbCr)
                                strUncodedDrugs.Append(CType(arrlist.Item(i), generalList).Description & " is an uncoded drug and will not be screened against drug interaction database")
                            End If
                            sNDCCode = ""

                        Next
                    End If
                End If
                If drugid <> 0 Then
                    'ddid = RetrieveDDID(drugid)
                    'ddid = _MedBusinessLayer.RetrieveDDID(drugid)
                    sNDCCode = _MedBusinessLayer.RetrieveNDCCode(drugid)

                    'If ddid > 0 Then
                    '    objDrugInteraction.AddDrugtocol(ddid)
                    'Else
                    '    '*************************
                    '    'confirm madam about what to write in this code because no event of trvlist is shown here
                    '    'If Not IsNothing(trDrugs.SelectedNode) Then
                    '    '    strUncodedDrugs.Append(trDrugs.SelectedNode.Text)
                    '    '    strUncodedDrugs.Append(vbCr)
                    '    'End If
                    '    '**************************
                    'End If
                    If sNDCCode <> "" Then
                        objDrugInteraction.AddDrugtocol(sNDCCode)
                    Else
                        '*************************
                        'confirm madam about what to write in this code because no event of trvlist is shown here
                        'If Not IsNothing(trDrugs.SelectedNode) Then
                        '    strUncodedDrugs.Append(trDrugs.SelectedNode.Text)
                        '    strUncodedDrugs.Append(vbCr)
                        'End If
                        '**************************
                    End If
                End If

                'For NDC Interaction
                'Check if allergies exist for that patient,if so then
                'populate the objdruginteraction collection with the allergies
                'associated with the patient.
                'If Not IsNothing(_MedBusinessLayer.HistoriesCol) Then
                '    'Check the count of medication items
                '    ddid = 0
                '    If _MedBusinessLayer.HistoriesCol.Count > 0 Then
                '        'Add the medication drugs to the objdruginteraction screening control
                '        For i = 0 To _MedBusinessLayer.HistoriesCol.Count - 1
                '            ddid = _MedBusinessLayer.RetrieveDDID(CType(_MedBusinessLayer.HistoriesCol.Item(i), History).DrugID)
                '            If ddid > 0 Then
                '                objDrugInteraction.AddDrugtocol1(ddid)
                '            End If
                '            ddid = 0
                '        Next
                '    End If
                'End If
                If Not IsNothing(_MedBusinessLayer.HistoriesCol) Then
                    'Check the count of medication items
                    'ddid = 0
                    sNDCCode = ""
                    If _MedBusinessLayer.HistoriesCol.Count > 0 Then
                        'Add the medication drugs to the objdruginteraction screening control
                        For i = 0 To _MedBusinessLayer.HistoriesCol.Count - 1
                            'ddid = _MedBusinessLayer.RetrieveDDID(CType(_MedBusinessLayer.HistoriesCol.Item(i), History).DrugID)
                            'sNDCCode = _MedBusinessLayer.RetrieveNDCCode(CType(_MedBusinessLayer.HistoriesCol.Item(i), History).DrugID)
                            sNDCCode = CType(_MedBusinessLayer.HistoriesCol.Item(i), History).NDCCode
                            If sNDCCode <> "" Then
                                objDrugInteraction.AddDrugtocol1(sNDCCode)
                            End If
                            'ddid = 0
                            sNDCCode = ""
                        Next
                    End If
                End If

                'For NDC Interaction

                '--------------------------------------------------------
                If Not IsNothing(_MedBusinessLayer.MedicalCondtionCol) Then
                    If _MedBusinessLayer.MedicalCondtionCol.Count > 0 Then
                        'Add the medication drugs to the objdruginteraction screening control
                        'Check the count of medicalconditions items
                        Dim MedConditionId As Int64
                        If _MedBusinessLayer.MedicalCondtionCol.Count > 0 Then
                            'Add the medication drugs to the objdruginteraction screening control
                            For i = 1 To _MedBusinessLayer.MedicalCondtionCol.Count
                                MedConditionId = _MedBusinessLayer.MedicalCondtionCol.Item(i)
                                objDrugInteraction.AddDrugtocol2(MedConditionId)
                            Next
                        End If
                    End If
                End If
                '--------------------------------------------------------

            End If
            ' End If
            ' ElseIf intMode = 2 And objMedicationDBLayer.FilterType <> "Active" Then----------------
            ' ElseIf intMode = 2 And _MedBusinessLayer.FilterType <> "Active" Then
        ElseIf _MedBusinessLayer.TransactionMode = MedicationBusinessLayer._TransactionMode.Edit And _MedBusinessLayer.FilterType <> "Active" Then
            If drugid <> 0 Then
                'ddid = _MedBusinessLayer.RetrieveDDID(drugid)
                sNDCCode = _MedBusinessLayer.RetrieveNDCCode(drugid)
                'If ddid > 0 Then
                '    objDrugInteraction.AddDrugtocol(ddid)
                'Else
                '    '*************************
                '    'confirm madam about what to write in this code because no event of trvlist is shown here
                '    'If Not IsNothing(trDrugs.SelectedNode) Then
                '    '    strUncodedDrugs.Append(trDrugs.SelectedNode.Text)
                '    '    strUncodedDrugs.Append(vbCr)
                '    'End If
                '    '**************************
                'End If
                If sNDCCode <> "" Then
                    objDrugInteraction.AddDrugtocol(sNDCCode)
                Else
                    '*************************
                    'confirm madam about what to write in this code because no event of trvlist is shown here
                    'If Not IsNothing(trDrugs.SelectedNode) Then
                    '    strUncodedDrugs.Append(trDrugs.SelectedNode.Text)
                    '    strUncodedDrugs.Append(vbCr)
                    'End If
                    '**************************
                End If
            End If

            'If objMedicationDBLayer.FilterType <> "All" Then-------------------
            If _MedBusinessLayer.FilterType <> "All" Then
                Dim arrlist As New ArrayList
                'get list of active medications for given visit from the database
                ' arrlist = objMedicationDBLayer.GetActiveMedications(tempdate, tempVisitId)
                arrlist = _MedBusinessLayer.GetActiveMedications()

                'ddid = 0
                sNDCCode = ""

                If arrlist.Count > 0 Then
                    For i = 0 To arrlist.Count - 1
                        'ddid = CType(arrlist.Item(i), mytable).Unit*********************need to add the myTable class

                        'ddid = CType(arrlist.Item(i), generalList).ID
                        sNDCCode = CType(arrlist.Item(i), generalList).ID

                        'if the ddid is not zero add it for screening purpose
                        'If ddid > 0 Then
                        '    objDrugInteraction.AddDrugtocol(ddid)
                        '    'if drug id is not zero then add it to string builder variable
                        '    'to alert the user that drug screening will not be performed
                        '    'for the same.
                        'Else
                        '    'strUncodedDrugs.Append(CType(arrlist.Item(i), mytable).Code)********************need to access the mytable class
                        '    strUncodedDrugs.Append(CType(arrlist.Item(i), generalList).Description)
                        '    strUncodedDrugs.Append(vbCr)
                        'End If
                        'ddid = 0
                        If sNDCCode <> "" Then
                            objDrugInteraction.AddDrugtocol(sNDCCode)
                            'if drug id is not zero then add it to string builder variable
                            'to alert the user that drug screening will not be performed
                            'for the same.
                        Else
                            'strUncodedDrugs.Append(CType(arrlist.Item(i), mytable).Code)********************need to access the mytable class
                            strUncodedDrugs.Append(CType(arrlist.Item(i), generalList).Description & " is an uncoded drug and will not be screened against drug interaction database")
                            strUncodedDrugs.Append(vbCr)
                        End If
                        sNDCCode = ""
                    Next
                End If
            End If


            '    mynode = trMedicationDetails.Nodes.Item(0)
            'ddid = 0
            sNDCCode = ""
            Dim strstatus As String = ""
            '    For i = 0 To mynode.GetNodeCount(False) - 1
            'For i = 1 To C1FlexGrdMedicationDetails.Rows.Count - 1
            For i = 0 To _MedBusinessLayer.MedicationCol.Count - 1
                'ddid = RetrieveDDID(CType(mynode.Nodes.Item(i), myTreeNode).Key)
                'strstatus = objMedicationDBLayer.GetStatus(i - 1)
                strstatus = _MedBusinessLayer.MedicationCol.Item(i).Status
                If strstatus = "Active" Or strstatus = "" Then
                    'ddid = CType(mynode.Nodes.Item(i), myTreeNode).DDID'''''code commented by sagar to add the code written on next line for C1FlexGrid
                    'Dim drgId As Long = C1FlexGrdMedicationDetails.GetData(i, COL_DRUGID)
                    'ddid = C1FlexGrdMedicationDetails.GetData(i, COL_DDID) ' code added by sagar

                    'ddid = _MedBusinessLayer.MedicationCol.Item(i).DDID
                    sNDCCode = _MedBusinessLayer.MedicationCol.Item(i).NDCCode

                    'If ddid > 0 Then
                    '    objDrugInteraction.AddDrugtocol(ddid)
                    'Else
                    '    'strUncodedDrugs.Append(mynode.Nodes.Item(i).Text)'''''code commented by sagar to add the code written on next line for C1FlexGrid
                    '    'strUncodedDrugs.Append(C1FlexGrdMedicationDetails.GetData(i, COL_MEDICATION)) '''''code added by sagar 
                    '    strUncodedDrugs.Append(_MedBusinessLayer.MedicationCol.Item(i).Medication)
                    '    strUncodedDrugs.Append(vbCr)
                    'End If
                    'ddid = 0
                    If sNDCCode <> "" Then
                        objDrugInteraction.AddDrugtocol(sNDCCode)
                    Else
                        'strUncodedDrugs.Append(mynode.Nodes.Item(i).Text)'''''code commented by sagar to add the code written on next line for C1FlexGrid
                        'strUncodedDrugs.Append(C1FlexGrdMedicationDetails.GetData(i, COL_MEDICATION)) '''''code added by sagar 
                        strUncodedDrugs.Append(_MedBusinessLayer.MedicationCol.Item(i).Medication & _MedBusinessLayer.MedicationCol.Item(i).Dosage & " is an uncoded drug and will not be screened against drug interaction database")
                        strUncodedDrugs.Append(vbCr)
                    End If
                    sNDCCode = ""
                End If
                strstatus = ""
            Next

            'For NDC Interaction
            'Check if allergies exist for that patient,if so then
            'populate the objdruginteraction collection with the allergies
            'associated with the patient.
            'If Not IsNothing(_MedBusinessLayer.HistoriesCol) Then
            '    'Check the count of medication items
            '    ddid = 0
            '    If _MedBusinessLayer.HistoriesCol.Count > 0 Then
            '        'Add the medication drugs to the objdruginteraction screening control
            '        For i = 0 To _MedBusinessLayer.HistoriesCol.Count - 1
            '            ddid = _MedBusinessLayer.RetrieveDDID(CType(_MedBusinessLayer.HistoriesCol.Item(i), History).DrugID)
            '            If ddid > 0 Then
            '                objDrugInteraction.AddDrugtocol1(ddid)
            '            End If
            '            ddid = 0
            '        Next
            '    End If
            'End If
            If Not IsNothing(_MedBusinessLayer.HistoriesCol) Then
                'Check the count of medication items
                'ddid = 0
                sNDCCode = ""
                If _MedBusinessLayer.HistoriesCol.Count > 0 Then
                    'Add the medication drugs to the objdruginteraction screening control
                    For i = 0 To _MedBusinessLayer.HistoriesCol.Count - 1
                        'ddid = _MedBusinessLayer.RetrieveDDID(CType(_MedBusinessLayer.HistoriesCol.Item(i), History).DrugID)
                        sNDCCode = _MedBusinessLayer.RetrieveNDCCode(CType(_MedBusinessLayer.HistoriesCol.Item(i), History).DrugID)
                        If sNDCCode <> "" Then
                            objDrugInteraction.AddDrugtocol1(sNDCCode)
                        End If
                        'ddid = 0
                        sNDCCode = ""
                    Next
                End If
            End If
            'For NDC Interaction

            '--------------------------------------------------------
            If Not IsNothing(_MedBusinessLayer.MedicalCondtionCol) Then
                If _MedBusinessLayer.MedicalCondtionCol.Count > 0 Then
                    'Add the medication drugs to the objdruginteraction screening control
                    'Check the count of medicalconditions items
                    Dim MedConditionId As Int64
                    If _MedBusinessLayer.MedicalCondtionCol.Count > 0 Then
                        'Add the medication drugs to the objdruginteraction screening control
                        For i = 1 To _MedBusinessLayer.MedicalCondtionCol.Count
                            MedConditionId = _MedBusinessLayer.MedicalCondtionCol.Item(i)
                            objDrugInteraction.AddDrugtocol2(MedConditionId)
                        Next
                    End If
                End If
            End If
            '--------------------------------------------------------


        End If
    End Sub

    'Private Sub DrugInteraction_Click(ByVal sender As Object, ByVal e As System.EventArgs, ByVal inttype As Int16, ByVal drugid As Int64) Handles objDrugInteraction.DIScreen_Click1
    '    If inttype = 7 Then
    '        Try
    '            'Unload any previous screen results before loading new screen results
    '            RefreshDIScreen()
    '            RemoveControl()
    '            'check if Rx items added 

    '            'If Not IsNothing(trMedicationDetails.SelectedNode) Then '''''by sagar start here
    '            'If C1FlexGrdMedicationDetails.Rows.Count > 0 Then
    '            If _MedicationC1FlexGrdUserCtrl.RowIndex > 0 Then ''''' by sagar start here

    '                'If trPrescriptionDetails.SelectedNode.Text <> "Prescription" Then
    '                objDIScreenResults = New PEScreeningResult

    '                'get dispensable drug id
    '                Dim id As Int64 = _MedBusinessLayer.MedicationCol.Item(_MedicationC1FlexGrdUserCtrl.RowIndex - 1).DDID

    '                'Load Patient Education
    '                If CType(objDIScreenResults, PEScreeningResult).LoadPatientEducation(id) Then
    '                    objDIScreenResults.Dock = DockStyle.Fill
    '                    pnlDIScreenResult.Controls.Add(objDIScreenResults)
    '                    pnlDIScreenResult.Visible = True
    '                    pnlDIScreenResult.BringToFront()
    '                Else
    '                    MsgBox("Medication Instructions not available for selected drug", MsgBoxStyle.ApplicationModal + MsgBoxStyle.OkOnly + MsgBoxStyle.Information, gstrMessageBoxCaption)
    '                End If
    '                'Else
    '                'MsgBox("Please Select a Medication to view its Medication Instructions", MsgBoxStyle.ApplicationModal + MsgBoxStyle.OkOnly + MsgBoxStyle.Information, gstrMessageBoxCaption)
    '                'End If
    '            End If '''''by sagar end here

    '        Catch ex As DrugInteraction.glostream.DrugInteraction.gloScreeningException
    '            MsgBox(ex.Message, MsgBoxStyle.ApplicationModal + MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, clsgeneral.gstrMessageBoxCaption)
    '        End Try
    '        'Check if ADEScreen has been selected or not
    '    ElseIf inttype = 1 Then

    '        Try
    '            'Unload any previous screen results before loading new screen results
    '            RefreshDIScreen()
    '            RemoveControl()
    '            'check if Rx items added 
    '            'objADEScreenResults = New ADEScreening(GetConnectionString())
    '            'objADEScreenResults = New ADEScreening(clsgeneral.ConnectionString)
    '            'Populate Drugs in ADEScreenResults treeview
    '            objADEScreenResults.FillDrugsforADEScreening()

    '            objADEScreenResults.Dock = DockStyle.Fill
    '            pnlDIScreenResult.Controls.Add(objADEScreenResults)

    '            pnlDIScreenResult.Visible = True
    '            pnlDIScreenResult.BringToFront()
    '        Catch ex As DrugInteraction.glostream.DrugInteraction.gloScreeningException
    '            MsgBox(ex.Message, MsgBoxStyle.ApplicationModal + MsgBoxStyle.OkOnly, clsgeneral.gstrMessageBoxCaption)
    '        End Try
    '    Else
    '        'Close the screenresult panel
    '        If inttype = 6 Then
    '            If Not IsNothing(objADEScreenResults) Then
    '                pnlDIScreenResult.Controls.Remove(objADEScreenResults)
    '                objADEScreenResults.Dispose()
    '                objADEScreenResults = Nothing
    '            End If
    '            If Not IsNothing(objDIScreenResults) Then
    '                pnlDIScreenResult.Controls.Remove(objDIScreenResults)
    '                objDIScreenResults.Dispose()
    '                objDIScreenResults = Nothing
    '            End If
    '            If Not IsNothing(objMonoDIScreenResults) Then
    '                pnlDIScreenResult.Controls.Remove(objMonoDIScreenResults)
    '                objMonoDIScreenResults.Dispose()
    '                objMonoDIScreenResults = Nothing
    '            End If
    '            pnlmonograph.SendToBack()

    '            pnlDIScreenResult.SendToBack()
    '        Else
    '            'Unload any previous screen results before loading new screen results
    '            RefreshDIScreen()
    '            'check if Rx items added 
    '            'If trMedicationDetails.Nodes.Item(0).GetNodeCount(False) > 0 Or drugid <> 0 Then ''''' by sagar start here
    '            'If C1FlexGrdMedicationDetails.Rows.Count > 0 Or drugid <> 0 Then
    '            If _MedBusinessLayer.MedicationCol.Count > 0 Or drugid <> 0 Then
    '                'Fill Drugs collection to be passed on to user control
    '                'to load patient profile
    '                'FillDrugsforDI(drugid)
    '                FillDrugsforDI()

    '                'auto medication alert is switched off
    '                If Not gblnmedalert Then
    '                    DisplayAlertForUncodedDrugs()
    '                End If
    '                Dim strresult As String

    '                'Add Monograph information to collection
    '                'Add DIScreenResults objects to the panel
    '                'Each DIScreenResult represents a distinct drug-drug interaction
    '                Select Case inttype
    '                    Case 1
    '                        objDrugInteraction.SetSeverityLevels(gstrADESeverityLevel)
    '                    Case 3
    '                        objDrugInteraction.SetSeverityLevels(gstrDISeverityLevel, gstrDIDocLevel)
    '                    Case 5
    '                        objDrugInteraction.SetSeverityLevels(gstrDFASeverityLevel, gstrDFADocLevel)
    '                End Select
    '                strresult = objDrugInteraction.PerformScreening(inttype)
    '                Dim j As Int32
    '                If objDrugInteraction.DrugInteractionResultSet.Count > 0 Then

    '                    For j = 1 To objDrugInteraction.DrugInteractionResultSet.Count
    '                        objDIScreenResults = CType(objDrugInteraction.DrugInteractionResultSet.Item(j), DIScreeningResults)
    '                        'objDIScreenResults.Dock = DockStyle.Fill
    '                        pnlDIScreenResult.Controls.Add(objDIScreenResults)
    '                        'objDIScreenResults.description = strresult
    '                        'objDIScreenResults.ScreenType = inttype
    '                        If j = 1 Then
    '                            RemoveControl()
    '                            pnlmedstatus.Visible = False
    '                            pnlDIScreenResult.Visible = True
    '                            pnlDIScreenResult.BringToFront()
    '                        End If
    '                    Next

    '                Else
    '                    Select Case inttype
    '                        Case 2
    '                            MessageBox.Show("Patient not found Allergic to given drugs", "Drug Interaction", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                        Case 3
    '                            MessageBox.Show("No Drug Interaction found for given drugs", "Drug Interaction", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                        Case 4
    '                            MessageBox.Show("No Duplication found for given drugs", "Drug Interaction", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                        Case 5
    '                            MessageBox.Show("No Drug Food Interaction found for given drugs", "Drug Interaction", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                    End Select
    '                End If
    '            End If ''''' by sagar End here
    '        End If
    '    End If
    'End Sub

    Private Sub objDIScreenResults_OkClk(ByVal sender As Object, ByVal e As System.EventArgs) Handles objDIScreenResults.OkClk
        Try
            pnlDIScreenResult.Controls.Remove(objDIScreenResults)
            objDIScreenResults = Nothing
            pnlDIScreenResult.Visible = False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub
    Private Function ValidateDrugInteractionScreen(ByRef objautoscreening As ClsAutoScreening, ByRef strmessage1 As String) As String
        Dim strDrugInteractionresult As New System.Text.StringBuilder
        Dim strDrugInteractionresult1 As New System.Text.StringBuilder

        Dim objMedication As Medication
        Dim j As Int16
        For j = 0 To _MedBusinessLayer.MedicationCol.Count - 1
            Dim i As Int16

            'For NDC Interaction
            Dim ddid As Int64
            Dim sNDCCode As String
            'For NDC Interaction
            'ddid = _MedBusinessLayer.MedicationCol.Item(j).DDID
            'For i = 0 To ResultCol.Count - 1
            '    If ResultCol.Item(i).ID = ddid Then
            '        strDrugInteractionresult.Append(ResultCol.Item(i).Name & " was found for Drug :" & _MedBusinessLayer.MedicationCol.Item(j).Medication & "-" & _MedBusinessLayer.MedicationCol.Item(j).Dosage)
            '        strDrugInteractionresult.Append(vbCr)
            '        Exit For
            '    End If
            'Next
            'ddid = 0
            sNDCCode = _MedBusinessLayer.MedicationCol.Item(j).NDCCode
            For i = 0 To ResultCol.Count - 1
                If ResultCol.Item(i).ID = sNDCCode Then
                    strDrugInteractionresult.Append(ResultCol.Item(i).Name & " was found for Drug :" & _MedBusinessLayer.MedicationCol.Item(j).Medication & "-" & _MedBusinessLayer.MedicationCol.Item(j).Dosage)
                    strDrugInteractionresult.Append(vbCr)
                    Exit For
                End If
            Next
            ddid = 0
            'For NDC Interaction
        Next
        'Drugs for which notes have been added
        strmessage1 = strDrugInteractionresult1.ToString
        'Drugs for which notes need to be added
        Return strDrugInteractionresult.ToString
    End Function
    Private Sub objADEScreenResults_OkClk(ByVal sender As Object, ByVal e As System.EventArgs) Handles objADEScreenResults.OkClk
        Try
            pnlDIScreenResult.Controls.Remove(objADEScreenResults)
            objADEScreenResults = Nothing
            pnlDIScreenResult.Visible = False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub objDIScreenResultsOkClk(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            pnlDIScreenResult.Controls.Clear()
            objADEScreenResults = Nothing
            pnlDIScreenResult.Visible = False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub objDIScreenResultsMonoClk(ByVal sender As Object, ByVal e As System.EventArgs, ByVal monograph As String)
        Try
            Dim strresult As String = objDrugInteraction.DisplayMonograph(monograph)
            If strresult <> "" Then
                objMonoDIScreenResults = New DIScreeningResults
                objMonoDIScreenResults.description = strresult
                objMonoDIScreenResults.Dock = DockStyle.Fill
                objMonoDIScreenResults.Header = "More Info"
                objMonoDIScreenResults.MakeReadOnly = True
                objMonoDIScreenResults.SetMonoVisibility = False
                pnlmonograph.Controls.Add(objMonoDIScreenResults)
                pnlmonograph.Visible = True
                pnlmonograph.BringToFront()
            End If
        Catch ex As gloScreeningException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MsgBox(ex.Message, MsgBoxStyle.ApplicationModal + MsgBoxStyle.OkOnly, clsgeneral.gstrMessageBoxCaption)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MsgBox("Error Showing monograph", MsgBoxStyle.ApplicationModal + MsgBoxStyle.OkOnly, clsgeneral.gstrMessageBoxCaption)

        End Try

    End Sub
    Private Sub objDrugInteraction_AddEventHandler(ByVal objDISCreenResultTemp As DIScreeningResults) Handles objDrugInteraction.AddEventHandler
        Try
            AddHandler objDISCreenResultTemp.OkClk, AddressOf objDIScreenResultsOkClk
            AddHandler objDISCreenResultTemp.MonoClk, AddressOf objDIScreenResultsMonoClk
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub
    Private Sub objMonoDIScreenResults_OkClk(ByVal sender As Object, ByVal e As System.EventArgs) Handles objMonoDIScreenResults.OkClk

        Try
            pnlmonograph.Controls.Remove(objMonoDIScreenResults)
            objMonoDIScreenResults = Nothing
            pnlmonograph.Visible = False
            pnlDIScreenResult.BringToFront()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub
    'Invoked fron AD button to do manual drug screening
    'blnsave is false

    Private Function PerformAutoScreening(ByVal blnSave As Boolean, Optional ByRef strmessage As String = "", Optional ByRef strmessage1 As String = "") As Boolean
        Dim objautoscreening As ClsAutoScreening
        Try
            If Not IsNothing(objautoscreening) Then
                objautoscreening.mydispose()
                objautoscreening = Nothing
            End If
            objautoscreening = New ClsAutoScreening
            objautoscreening.DFAScreenStatus = gblnDFAAlert

            FillDrugsforDI(objautoscreening, 0)
            Dim objhash As Hashtable
            objautoscreening.SetScreeningAlert(gstrADESeverityLevel, gstrDISeverityLevel, gstrDFASeverityLevel, gstrDIDocLevel, gstrDFADocLevel)
            objhash = objautoscreening.PerformScreening
            ResultCol = objautoscreening.ResultCol
            If Not IsNothing(objhash) Then
                If objhash.Count > 0 Then
                    'Display the drug alert when invoked from AD button
                    'If invoked from 'AD' button,just suppress the drug alert and display the result

                    If blnSave Then
                        Dim result As Int16
                        result = MessageBox.Show("Drug Interaction found for newly Prescribed Drug", "Drug Interaction", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
                        If result = vbOK Then
                            DisplayScreeningResult(objhash)
                            Return True
                        Else
                            Return False
                        End If
                        'Display the drug alert when invoked from Save button
                    Else
                        DisplayScreeningResult(objhash)
                        strmessage = ""
                        strmessage = ValidateDrugInteractionScreen(objautoscreening, strmessage1)
                        Return True
                    End If
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return False
        Finally
            objautoscreening = Nothing
        End Try

    End Function

    Private Function PerformAutoScreening(ByVal drugid As Int64) As Boolean
        Try
            Dim objautoscreening As ClsAutoScreening
            If Not IsNothing(objautoscreening) Then
                objautoscreening.mydispose()
                objautoscreening = Nothing
            End If
            objautoscreening = New ClsAutoScreening
            objautoscreening.DFAScreenStatus = gblnDFAAlert

            ''For NDC Interaction
            Dim DDID As Int64 = 0
            Dim sNDCCode As String = ""

            If Not IsNothing(_MedBusinessLayer.MedicationCol.Item(_MedBusinessLayer.MedicationCol.Count - 1)) Then
                sNDCCode = _MedBusinessLayer.MedicationCol.Item(_MedBusinessLayer.MedicationCol.Count - 1).NDCCode
            End If
            FillDrugsforDI(objautoscreening, drugid, sNDCCode)


            If sNDCCode <> "" Then
                Dim objhash As Hashtable
                Dim m_DrugAlert As String = ""
                objautoscreening.SetScreeningAlert(gstrADESeverityLevel, gstrDISeverityLevel, gstrDFASeverityLevel, gstrDIDocLevel, gstrDFADocLevel)
                objhash = objautoscreening.PerformScreening(m_DrugAlert, sNDCCode)
                'Got atleast one screening result for the new drug which is to be prescribed
                If m_DrugAlert <> "" Then
                    Dim result As Int16
                    result = MessageBox.Show("Drug Interaction found for given Medication", "Drug Interaction", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
                    If result = vbOK Then
                        If Not IsNothing(objhash) Then
                            objautoscreening = Nothing
                            If objhash.Count > 0 Then
                                DisplayScreeningResult(objhash, m_DrugAlert)
                                Return True
                            Else
                                Return False
                            End If
                        Else
                            Return False
                        End If
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return False
        End Try
    End Function
    Private Function DisplayScreeningResult(ByVal objhash As Hashtable, Optional ByVal m_Drugalert As String = "")
        UnloadDrugInteractionControl()
        'Don't create new drug interaction toolbar instance
        'objDrugInteraction = New DrugInteractionControl.glostream.DIToolbar(objhash, m_Drugalert)
        objDrugInteraction.RefreshToolBar(objhash, m_Drugalert)
        'Set the Screentype i.e PAR/DI/DT/DFA
        btntype = objDrugInteraction.Screentype
        'AddDrugInteractionControl()
    End Function
    Private Sub AddDrugInteractionControl()
        objDrugInteraction.Dock = DockStyle.Left
        pnlDI.Controls.Clear()
        pnlDI.Controls.Add(objDrugInteraction)
        pnlDI.BringToFront()
    End Sub

    Private Sub FillDrugsforDI(ByVal objautoscreening As ClsAutoScreening, ByVal drugid As Int64, Optional ByRef DrugNDCCode As String = "")

        strUncodedDrugs = New System.Text.StringBuilder

        Dim i As Int32
        Dim ddid As Int64
        Dim sNDCCode As String

        'Dim mynode As TreeNode
        Dim objGeneralList As New generalList
        If _MedBusinessLayer.TransactionMode = MedicationBusinessLayer._TransactionMode.Add Then

            sNDCCode = ""

            Dim strstatus As String = ""

            For j As Integer = 0 To _MedBusinessLayer.MedicationCol.Count - 1 '''''code added by sagar for c1flexgrid

                strstatus = _MedBusinessLayer.MedicationCol.Item(j).Status
                strstatus = _MedBusinessLayer.FilterType ' _MedBusinessLayer.MedicationCol.Item(j).Status
                If strstatus = "Active" Or strstatus = "" Then

                    sNDCCode = _MedBusinessLayer.MedicationCol.Item(j).NDCCode

                    If sNDCCode <> "" Then

                        objautoscreening.AddDrugtocol(sNDCCode)

                    Else
                        'strUncodedDrugs.Append(mynode.Nodes.Item(i).Text)'''''by sagar
                        'strUncodedDrugs.Append(C1FlexGrdMedicationDetails.GetData(j, COL_MEDICATION))
                        strUncodedDrugs.Append(_MedBusinessLayer.MedicationCol.Item(j).Medication & _MedBusinessLayer.MedicationCol.Item(j).Dosage & " is an uncoded drug and will not be screened against drug interaction database")
                        strUncodedDrugs.Append(vbCr)
                    End If

                    strstatus = ""
                    'ddid = 0
                    sNDCCode = ""
                End If
            Next


            Dim arrlist As New ArrayList
            'get list of active medications for given visit from the database
            arrlist = _MedBusinessLayer.GetActiveMedications()
            'ddid = 0
            sNDCCode = ""
            If arrlist.Count > 0 Then
                For i = 0 To arrlist.Count - 1
                    sNDCCode = CType(arrlist.Item(i), generalList).ID
                    If sNDCCode <> "" Then
                        objautoscreening.AddDrugtocol(sNDCCode)
                        'if drug id is not zero then add it to string builder variable
                        'to alert the user that drug screening will not be performed
                        'for the same.
                    Else
                        'strUncodedDrugs.Append(CType(arrlist.Item(i), mytable).Code)*******************class file myTable needed
                        strUncodedDrugs.Append(CType(arrlist.Item(i), generalList).Description & " is an uncoded drug and will not be screened against drug interaction database")
                        strUncodedDrugs.Append(vbCr)
                    End If
                    sNDCCode = ""
                Next
            End If
            'm_visitdate
            'ElseIf intMode = 2 Then
        ElseIf _MedBusinessLayer.TransactionMode = MedicationBusinessLayer._TransactionMode.Edit Then
            'if the list contains active medications

            'If objMedicationDBLayer.FilterType = "Active" Then
            If _MedBusinessLayer.FilterType = "Active" Then
                'mynode = trMedicationDetails.Nodes.Item(0) '''''by sagar 

                'ddid = 0
                sNDCCode = ""

                Dim strstatus As String = ""




                'For j As Integer = 1 To C1FlexGrdMedicationDetails.Rows.Count - 1 '''''code added by sagar for c1flexgrid
                For j As Integer = 0 To _MedBusinessLayer.MedicationCol.Count - 1
                    'strstatus = objMedicationDBLayer.GetStatus(j - 1)
                    strstatus = _MedBusinessLayer.MedicationCol.Item(j).Status
                    If strstatus = "Active" Or strstatus = "" Then
                        sNDCCode = _MedBusinessLayer.MedicationCol.Item(j).NDCCode

                        If sNDCCode <> "" Then
                            objautoscreening.AddDrugtocol(sNDCCode)

                        Else
                            'strUncodedDrugs.Append(mynode.Nodes.Item(i).Text)'''''by sagar
                            'strUncodedDrugs.Append(C1FlexGrdMedicationDetails.GetData(j, COL_MEDICATION))
                            strUncodedDrugs.Append(_MedBusinessLayer.MedicationCol.Item(j).Medication & _MedBusinessLayer.MedicationCol.Item(j).Dosage & " is an uncoded drug and will not be screened against drug interaction database")
                            strUncodedDrugs.Append(vbCr)
                        End If

                        strstatus = ""
                        sNDCCode = ""
                    End If
                Next
                'if list does not contain active medications
                'then query the database and get list of only active medications
                'for given visit
            Else

                Dim arrlist As New ArrayList
                'If objMedicationDBLayer.FilterType <> "All" Then
                If _MedBusinessLayer.FilterType <> "All" Then
                    'get list of active medications for given visit from the database
                    'arrlist = objMedicationDBLayer.GetActiveMedications(tempdate, tempVisitId)
                    arrlist = _MedBusinessLayer.GetActiveMedications()
                    'ddid = 0
                    sNDCCode = ""
                    If arrlist.Count > 0 Then
                        For i = 0 To arrlist.Count - 1

                            sNDCCode = CType(arrlist.Item(i), generalList).ID


                            If sNDCCode <> "" Then
                                objautoscreening.AddDrugtocol(sNDCCode)
                                'if drug id is not zero then add it to string builder variable
                                'to alert the user that drug screening will not be performed
                                'for the same.
                            Else
                                'strUncodedDrugs.Append(CType(arrlist.Item(i), mytable).Code)'****************myTable class file needed
                                strUncodedDrugs.Append(CType(arrlist.Item(i), generalList).Description & " is an uncoded drug and will not be screened against drug interaction database")
                                strUncodedDrugs.Append(vbCr)
                            End If
                            sNDCCode = ""
                        Next
                    End If
                End If

                'mynode = trMedicationDetails.Nodes.Item(0) ''''' by sagar
                'ddid = 0
                sNDCCode = ""

                Dim strstatus As String = ""
                'For i = 0 To mynode.GetNodeCount(False) - 1
                For i = 0 To _MedBusinessLayer.MedicationCol.Count - 1
                    'ddid = RetrieveDDID(CType(mynode.Nodes.Item(i), myTreeNode).Key)
                    'strstatus = objMedicationDBLayer.GetStatus(i)
                    strstatus = _MedBusinessLayer.MedicationCol.Item(i).Status
                    If strstatus = "Active" Or strstatus = "" Then
                        'ddid = CType(mynode.Nodes.Item(i), myTreeNode).DDID

                        'ddid = _MedBusinessLayer.MedicationCol.Item(i).DDID
                        sNDCCode = _MedBusinessLayer.MedicationCol.Item(i).NDCCode

                        If sNDCCode <> "" Then

                            objautoscreening.AddDrugtocol(sNDCCode)

                        Else
                            'strUncodedDrugs.Append(mynode.Nodes.Item(i).Text)
                            strUncodedDrugs.Append(objGeneralList.Description & " is an uncoded drug and will not be screened against drug interaction database")
                            strUncodedDrugs.Append(vbCr)
                        End If
                        sNDCCode = ""
                    End If
                    strstatus = ""
                Next
            End If
        End If

        ' History Collection
        If Not IsNothing(_MedBusinessLayer.HistoriesCol) Then
            'Check the count of medication items
            'ddid = 0
            sNDCCode = ""
            If _MedBusinessLayer.HistoriesCol.Count > 0 Then
                'Add the medication drugs to the objdruginteraction screening control
                For i = 0 To _MedBusinessLayer.HistoriesCol.Count - 1
                    'ddid = _MedBusinessLayer.RetrieveDDID(CType(_MedBusinessLayer.HistoriesCol.Item(i), History).DrugID)
                    'sNDCCode = _MedBusinessLayer.RetrieveNDCCode(CType(_MedBusinessLayer.HistoriesCol.Item(i), History).DrugID)
                    sNDCCode = CType(_MedBusinessLayer.HistoriesCol.Item(i), History).NDCCode '_MedBusinessLayer.RetrieveNDCCode()
                    If sNDCCode <> "" Then
                        objautoscreening.AddDrugtocol1(sNDCCode)
                    End If
                    'ddid = 0
                    sNDCCode = ""
                Next
            End If
        End If
        'For NDC Interaction

        'Medical Condition Collection
        '--------------------------------------------------------
        If Not IsNothing(_MedBusinessLayer.MedicalCondtionCol) Then
            If _MedBusinessLayer.MedicalCondtionCol.Count > 0 Then
                'Add the medication drugs to the objdruginteraction screening control
                'Check the count of medicalconditions items
                Dim MedConditionId As Int64
                If _MedBusinessLayer.MedicalCondtionCol.Count > 0 Then
                    'Add the medication drugs to the objdruginteraction screening control
                    For i = 1 To _MedBusinessLayer.MedicalCondtionCol.Count
                        MedConditionId = _MedBusinessLayer.MedicalCondtionCol.Item(i)
                        objautoscreening.AddDrugtocol2(MedConditionId)
                    Next
                End If
            End If
        End If
        '--------------------------------------------------------

    End Sub

    Private Sub DisplayAlertForUncodedDrugs()
        If Not IsNothing(strUncodedDrugs) Then
            If strUncodedDrugs.ToString <> "" Then
                'strUncodedDrugs.Append("Above Drugs have been found to be Uncoded Drugs")
                'strUncodedDrugs.Append(vbCr)
                'strUncodedDrugs.Append("Drug Screening will not be performed for the same")
                MessageBox.Show(strUncodedDrugs.ToString, "Medication", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                strUncodedDrugs.Remove(0, strUncodedDrugs.Length)
                strUncodedDrugs = Nothing
            End If
        End If
    End Sub

    'Private Sub DisplayAlertForUncodedDrugs()
    '    If Not IsNothing(strUncodedDrugs) Then
    '        If strUncodedDrugs.ToString <> "" Then
    '            strUncodedDrugs.Append("Above Drugs have been found to be Uncoded Drugs")
    '            strUncodedDrugs.Append(vbCr)
    '            strUncodedDrugs.Append("Drug Screening will not be performed for the same")
    '            MessageBox.Show(strUncodedDrugs.ToString, "Medication", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '            strUncodedDrugs.Remove(0, strUncodedDrugs.Length)
    '            strUncodedDrugs = Nothing
    '        End If
    '    End If
    'End Sub
    Private Sub RefreshDIScreen()
        'Unload ADE Screen
        If Not IsNothing(objADEScreenResults) Then
            pnlDIScreenResult.Controls.Remove(objADEScreenResults)
            objADEScreenResults.Dispose()
            objADEScreenResults = Nothing
            pnlDIScreenResult.Visible = False
        End If
        'Unload Monograph screening
        If Not IsNothing(objMonoDIScreenResults) Then
            pnlmonograph.Controls.Remove(objMonoDIScreenResults)
            objMonoDIScreenResults.Dispose()
            objMonoDIScreenResults = Nothing
            pnlmonograph.Visible = False
            pnlDIScreenResult.BringToFront()
        End If

        'Unload DI screenresult
        If pnlDIScreenResult.Controls.Count > 0 Then
            Dim objcontrol As System.Windows.Forms.Control
            For Each objcontrol In pnlDIScreenResult.Controls
                If TypeOf (objcontrol) Is DIScreeningResults Then
                    objcontrol.Dispose()
                    objcontrol = Nothing
                End If
            Next
        End If
        pnlDIScreenResult.Controls.Clear()
        pnlDIScreenResult.SendToBack()
        pnlcenter.BringToFront()
    End Sub
    Private Sub UnloadDrugInteractionControl()
        If Not IsNothing(objADEScreenResults) Then
            pnlDIScreenResult.Controls.Remove(objADEScreenResults)
            objADEScreenResults.Dispose()
            objADEScreenResults = Nothing
        End If
        If Not IsNothing(objMonoDIScreenResults) Then
            pnlDIScreenResult.Controls.Remove(objMonoDIScreenResults)
            objMonoDIScreenResults.Dispose()
            objMonoDIScreenResults = Nothing
        End If
        If pnlDIScreenResult.Controls.Count > 0 Then
            Dim objcontrol As System.Windows.Forms.Control
            For Each objcontrol In pnlDIScreenResult.Controls
                If TypeOf (objcontrol) Is DIScreeningResults Then
                    objcontrol.Dispose()
                    objcontrol = Nothing
                End If
            Next
        End If
        pnlDIScreenResult.Controls.Clear()

        pnlDIScreenResult.SendToBack()
        pnlcenter.BringToFront()
        'objDrugInteraction.MyDispose()
    End Sub




    Private Sub objDrugInteraction_DIScreen_Click1(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs, ByVal inttype As Short, ByVal drugid As Long) Handles objDrugInteraction.DIScreen_Click1
        If inttype = 7 Then
            Try
                'Unload any previous screen results before loading new screen results
                RefreshDIScreen()
                RemoveControl()
                'check if Rx items added 

                'If Not IsNothing(trMedicationDetails.SelectedNode) Then '''''by sagar start here
                'If C1FlexGrdMedicationDetails.Rows.Count > 0 Then
                If _MedBusinessLayer.MedicationCol.Count > 0 Then ''''' by sagar start here

                    'If trPrescriptionDetails.SelectedNode.Text <> "Prescription" Then
                    objDIScreenResults = New PEScreeningResult

                    'get dispensable drug id
                    Dim id As Int64 = _MedBusinessLayer.MedicationCol.Item(_MedicationC1FlexGrdUserCtrl.RowIndex - 1).DDID
                    Dim img As Image = _MedBusinessLayer.getClinicLogo
                    CType(objDIScreenResults, PEScreeningResult).ClinicLogo = img

                    'Load Patient Education
                    If CType(objDIScreenResults, PEScreeningResult).LoadPatientEducation(id) Then
                        objDIScreenResults.Dock = DockStyle.Fill
                        pnlDIScreenResult.Controls.Add(objDIScreenResults)
                        pnlDIScreenResult.Visible = True
                        pnlDIScreenResult.BringToFront()
                    Else
                        MsgBox("Medication Instructions not available for selected drug", MsgBoxStyle.ApplicationModal + MsgBoxStyle.OkOnly + MsgBoxStyle.Information, clsgeneral.gstrMessageBoxCaption)
                    End If
                    'Else
                    'MsgBox("Please Select a Medication to view its Medication Instructions", MsgBoxStyle.ApplicationModal + MsgBoxStyle.OkOnly + MsgBoxStyle.Information, gstrMessageBoxCaption)
                    'End If
                End If '''''by sagar end here

            Catch ex As gloScreeningException
                MsgBox(ex.Message, MsgBoxStyle.ApplicationModal + MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, clsgeneral.gstrMessageBoxCaption)
            End Try
            'Check if ADEScreen has been selected or not
            'ElseIf inttype = 1 Then

            '    Try
            '        'Unload any previous screen results before loading new screen results
            '        RefreshDIScreen()
            '        RemoveControl()
            '        'check if Rx items added 
            '        objADEScreenResults = New ADEScreening(GetConnectionString(), gstrADESeverityLevel)
            '        'objADEScreenResults = New ADEScreening(clsgeneral.ConnectionString)
            '        'Populate Drugs in ADEScreenResults treeview
            '        objADEScreenResults.FillDrugsforADEScreening()

            '        objADEScreenResults.Dock = DockStyle.Fill
            '        pnlDIScreenResult.Controls.Add(objADEScreenResults)

            '        pnlDIScreenResult.Visible = True
            '        pnlDIScreenResult.BringToFront()
            '    Catch ex As gloScreeningException
            '        MsgBox(ex.Message, MsgBoxStyle.ApplicationModal + MsgBoxStyle.OkOnly, clsgeneral.gstrMessageBoxCaption)
            '    End Try
        Else
            'Close the screenresult panel
            If inttype = 6 Then
                If Not IsNothing(objADEScreenResults) Then
                    pnlDIScreenResult.Controls.Remove(objADEScreenResults)
                    objADEScreenResults.Dispose()
                    objADEScreenResults = Nothing
                End If
                If Not IsNothing(objDIScreenResults) Then
                    pnlDIScreenResult.Controls.Remove(objDIScreenResults)
                    objDIScreenResults.Dispose()
                    objDIScreenResults = Nothing
                End If
                If Not IsNothing(objMonoDIScreenResults) Then
                    pnlDIScreenResult.Controls.Remove(objMonoDIScreenResults)
                    objMonoDIScreenResults.Dispose()
                    objMonoDIScreenResults = Nothing
                End If
                pnlmonograph.SendToBack()

                pnlDIScreenResult.SendToBack()
            Else
                'Unload any previous screen results before loading new screen results
                RefreshDIScreen()
                'check if Rx items added 
                'If trMedicationDetails.Nodes.Item(0).GetNodeCount(False) > 0 Or drugid <> 0 Then ''''' by sagar start here
                'If C1FlexGrdMedicationDetails.Rows.Count > 0 Or drugid <> 0 Then
                If _MedBusinessLayer.MedicationCol.Count > 0 Or drugid <> 0 Then
                    'Fill Drugs collection to be passed on to user control
                    'to load patient profile
                    'FillDrugsforDI(drugid)
                    FillDrugsforDI()

                    'auto medication alert is switched off
                    If Not gblnMedAlert Then
                        DisplayAlertForUncodedDrugs()
                    End If
                    Dim strresult As String

                    'Add Monograph information to collection
                    'Add DIScreenResults objects to the panel
                    'Each DIScreenResult represents a distinct drug-drug interaction
                    Select Case inttype
                        Case 1
                            objDrugInteraction.SetSeverityLevels(gstrADESeverityLevel)
                        Case 3
                            objDrugInteraction.SetSeverityLevels(gstrDISeverityLevel, gstrDIDocLevel)
                        Case 5
                            objDrugInteraction.SetSeverityLevels(gstrDFASeverityLevel, gstrDFADocLevel)
                    End Select
                    strresult = objDrugInteraction.PerformScreening(inttype)
                    Dim j As Int32
                    If objDrugInteraction.DrugInteractionResultSet.Count > 0 Then

                        For j = 1 To objDrugInteraction.DrugInteractionResultSet.Count
                            objDIScreenResults = CType(objDrugInteraction.DrugInteractionResultSet.Item(j), DIScreeningResults)
                            'objDIScreenResults.Dock = DockStyle.Fill
                            pnlDIScreenResult.Controls.Add(objDIScreenResults)
                            'objDIScreenResults.description = strresult
                            'objDIScreenResults.ScreenType = inttype
                            If j = 1 Then
                                RemoveControl()
                                pnlDIScreenResult.Visible = True
                                pnlDIScreenResult.BringToFront()
                            End If
                        Next

                    Else
                        Select Case inttype
                            '-------------------------------------
                            Case 1
                                MessageBox.Show("No Adverse Drugs Effect found for given drugs", "Drug Interaction", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                '-------------------------------------
                            Case 2
                                MessageBox.Show("Patient not found Allergic to given drugs", "Drug Interaction", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Case 3
                                MessageBox.Show("No Drug Interaction found for given drugs", "Drug Interaction", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Case 4
                                MessageBox.Show("No Duplication found for given drugs", "Drug Interaction", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Case 5
                                MessageBox.Show("No Drug Food Interaction found for given drugs", "Drug Interaction", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Case 8
                                MessageBox.Show("No Drug to Disease Interaction for given drugs", "Drug Interaction", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End Select
                    End If
                End If ''''' by sagar End here
            End If
        End If

    End Sub



    '______________________________________________
    Private Function BindSigGrid()

        'Dim dt As New DataTable
        'ReferralCount = 0
        'If GridStatus = 1 Then
        '    dt = objMedicationDBLayer.FillControls(0)
        '    ObjcustomGrid.SetDatasource(objMedicationDBLayer.DsDataview())
        '    objMedicationDBLayer.SortDataview(objMedicationDBLayer.DsDataview.Table.Columns(0).ColumnName)
        '    HideColumns(objMedicationDBLayer.DsDataview)
        '    ReferralCount = objMedicationDBLayer.DsDataview.Count - 1
        '    ObjcustomGrid.BringToFront()
        '    ObjcustomGrid.Selectsearch(ObjcustomGrid.enmcontrol.Search)
        'ElseIf GridStatus = 2 Then
        '    dt = objMedicationDBLayer.FetchMedicationforView(0)
        '    dgMakeCurrent.DataSource = dt
        '    objMedicationDBLayer.SortDataview(objMedicationDBLayer.DsDataview.Table.Columns(7).ColumnName)
        '    HideColumns(objMedicationDBLayer.DsDataview)
        '    'referral count holds the current row count
        '    ReferralCount = objMedicationDBLayer.DsDataview.Count - 1
        'End If



        ReferralCount = 0
        If GridStatus = 1 Then
            dt = _MedBusinessLayer.FillSigControls(0)
            'objMedicationDBLayer.DsDataview.Count - 1
            'objSigControl.BringToFront()
            Return dt
            'objSigControl.SetDatasource(objMedicationDBLayer.DsDataview())
            'objMedicationDBLayer.SortDataview(objMedicationDBLayer.DsDataview.Table.Columns(0).ColumnName)
            'HideColumns(objMedicationDBLayer.DsDataview)


            'ask madam for this logic
            'ObjcustomGrid.Selectsearch(ObjcustomGrid.enmcontrol.Search)
        ElseIf GridStatus = 2 Then
            dt = _MedBusinessLayer.FetchMedicationforView(0)
            'ReferralCount = objSigControl._flexObj.Rows.Count - 1 ' objMedicationDBLayer.DsDataview.Count - 1
            Return dt
            'dgMakeCurrent.DataSource = dt
            'objMedicationDBLayer.SortDataview(objMedicationDBLayer.DsDataview.Table.Columns(7).ColumnName)
            'HideColumns(objMedicationDBLayer.DsDataview)
            'referral count holds the current row count

        End If
        ReferralCount = objSigControl._flexObj.Rows.Count - 1
    End Function

    Private Sub objSigControl__FlexDoubleClick(ByVal sender As Object, ByVal e As System.EventArgs, ByVal _bSelectFlag As Boolean) Handles objSigControl._FlexDoubleClick
        Try
            SetSigDetails()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub



    Private Sub objSigControl_btnUC_ADDclick(ByVal sender As Object, ByVal e As System.EventArgs) Handles objSigControl.btnUC_ADDclick
        'Dim objfrmMSTSIG As New frmMSTSIG
        'Try
        '    objfrmMSTSIG.Text = "Add New SIG"
        '    objfrmMSTSIG.ShowDialog()
        '    BindGrid()
        '    If objMedicationDBLayer.DsDataview.Count > 0 Then
        '        ObjcustomGrid.GetSelect(rowindex)
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.ToString, "SIG", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    objfrmMSTSIG = Nothing
        'End Try

        Dim objfrmMSTSIG As New frmMSTSIG
        Try
            objfrmMSTSIG.Text = "Add New SIG"
            objfrmMSTSIG.ShowDialog()
            'setCustomGridData(objfrmMSTSIG.txtDosage.Text, objfrmMSTSIG.txtRoute.Text, objfrmMSTSIG.txtFrequency.Text, objfrmMSTSIG.txtDuration.Text)
            sigid = objfrmMSTSIG.SigId
            objCustomMedication_SigClick(sender, e)
            'BindSigGrid()
            'If dt.Rows.Count > 0 Then
            '    objSigControl.GetSelect(rowindex)
            'End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "SIG", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            objfrmMSTSIG = Nothing
        End Try
    End Sub

    'Private Function setCustomGridData(ByVal strDosage As String, ByVal strRoute As String, ByVal strFrequency As String, ByVal strDuration As String)
    '    objCustomMedication.Dosage = strDosage
    '    objCustomMedication.route = strRoute
    '    objCustomMedication.Frequency = strFrequency
    '    objCustomMedication.Duration = strDuration

    'End Function

    Private Sub objSigControl_btnUC_Cancelclick(ByVal sender As Object, ByVal e As System.EventArgs) Handles objSigControl.btnUC_Cancelclick
        Try
            'If Not IsNothing(ObjcustomGrid) Then
            '    Me.Controls.Remove(ObjcustomGrid)
            '    ObjcustomGrid.Visible = False
            '    ObjcustomGrid = Nothing
            '    rowindex = 0
            'End If
            If Not IsNothing(objSigControl) Then
                Me.Controls.Remove(objSigControl)
                objSigControl.Visible = False
                objSigControl = Nothing
                rowindex = 0
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    'sarika 15th oct 07
    Private Sub objSigControl_btnUC_Modify_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles objSigControl.btnUC_Modify_click
        Dim nSigId As Long


        Try
            If GridStatus = 1 Then
                If dt.Rows.Count > 0 Then
                    If rowindex <= ReferralCount Then
                        If Not IsDBNull(objSigControl) Then
                            nSigId = objSigControl._UCflex.GetData(objSigControl._UCflex.Row, "nSigId")
                        End If
                    End If
                End If
            End If

            Dim objfrmMSTSIG As New frmMSTSIG(nSigId)

            objfrmMSTSIG.Text = "Modify SIG"
            objfrmMSTSIG.ShowDialog()
            sigid = nSigId

            'setCustomGridData(objfrmMSTSIG.txtDosage.Text, objfrmMSTSIG.txtRoute.Text, objfrmMSTSIG.txtFrequency.Text, objfrmMSTSIG.txtDuration.Text)
            objCustomMedication_SigClick(sender, e)
            '           BindSigGrid()
            'If dt.Rows.Count > 0 Then
            '    objSigControl.GetSelect(rowindex)
            'End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Medication", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    '------------------------------------------------

    Private Sub SetSigDetails()

        '---------------------------------------------------
        Dim strCmbDuration As String = ""
        Dim strDuration As String = ""
        If GridStatus = 1 Then
            If dt.Rows.Count > 0 Then
                If rowindex <= ReferralCount Then
                    If Not IsDBNull(objSigControl) Then
                        objCustomMedication.Dosage = objSigControl._UCflex.GetData(objSigControl._UCflex.Row, "Dosage")
                        objCustomMedication.route = objSigControl._UCflex.GetData(objSigControl._UCflex.Row, "Route")
                        objCustomMedication.Frequency = objSigControl._UCflex.GetData(objSigControl._UCflex.Row, "Frequency")

                        'get the value from the selected sig and assign the duration and Days/Weeks/Months value as pee the splited text.
                        'CCHIT 08
                        strDuration = objSigControl._UCflex.GetData(objSigControl._UCflex.Row, "sDuration_SplitChr")

                        If strDuration <> "" Then
                            Dim retval As String() = SplitDrug(strDuration)

                            If Not IsNothing(retval) Then
                                If retval.Length > 1 Then
                                    objCustomMedication.Duration = retval(0) 'this will assign the duration value to the TxtDuration text box of the CustomMedication control
                                    strCmbDuration = retval(retval.Length - 1) ' retrieve the values of Days/Weeks/Months accordingly set in the Sig Control
                                Else
                                    objCustomMedication.Duration = retval(0)
                                End If

                            Else
                                objCustomMedication.Duration = retval(0)
                            End If
                        Else
                            strDuration = ""
                        End If

                        'this will assign the Days/Weeks/Months value to the cmbDuration combobox of the CustomMedication control
                        If strCmbDuration <> "" Then

                            If strCmbDuration.ToUpper() = "DAYS" Then
                                objCustomMedication.cmbDurationDyWkMnth = "Days" '0th item is Days
                            ElseIf strCmbDuration.ToUpper() = "WEEKS" Then
                                objCustomMedication.cmbDurationDyWkMnth = "Weeks" '1st item is Weeks
                            Else
                                objCustomMedication.cmbDurationDyWkMnth = "Months" '2nd item is Months
                            End If
                        End If
                        'CCHIT 08

                    End If
                End If
            End If
        End If

        If Not IsNothing(objSigControl) Then
            Me.Controls.Remove(objSigControl)
            objSigControl.Visible = False
            objSigControl = Nothing
            rowindex = 0
        End If

        'Call ResetSearch()
    End Sub

    Private Function SplitDrug(ByVal _strDuration As String) As Array
        Try
            Dim _result As String()
            _result = _strDuration.Split("|")
            Return _result
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        End Try

    End Function

#Region " Allergy Alert "
    Private blnMoving As Boolean = False
    Private MouseDownX As Integer
    Private MouseDownY As Integer

    Private Sub GetAllergies()
        Try
            Dim strallergies As String
            'Retrieve the patient specific allergies and maintain it in HistoriesCol collection
            'of RXBusiness Layer
            strallergies = _MedBusinessLayer.GetHistory_CategoryWise()

            lblAlert1.Text = strallergies

            If _MedBusinessLayer.HistoriesCol.Count > 0 Then
                lblAlert1.Height = _MedBusinessLayer.HistoriesCol.Count * 15
            End If

            pnlAllergiesAlerts.Height = 104
            pnlAllergiesAlerts.BringToFront()
            If pnlAllergiesAlerts.Height < lblAlert1.Height + 35 Then
                pnlAllergiesAlerts.Height = lblAlert1.Height + 35
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
#Region "Allergies Panel"
    'Code commented as allergies not to be shown
    'Private Sub pnlAllergiesAlerts_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlAllergiesAlerts.MouseDown

    '    Try
    '        Me.Cursor = Cursors.SizeAll
    '        If e.Button = MouseButtons.Left Then
    '            blnMoving = True
    '            MouseDownX = e.X
    '            MouseDownY = e.Y
    '        End If
    '    Catch ex As Exception

    '    End Try

    'End Sub

    'Private Sub pnlAllergiesAlerts_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlAllergiesAlerts.MouseUp
    '    Try
    '        If e.Button = MouseButtons.Left Then
    '            blnMoving = False
    '        End If
    '        Me.Cursor = Cursors.Default
    '    Catch ex As Exception

    '    End Try

    'End Sub

    'Private Sub pnlAllergiesAlerts_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlAllergiesAlerts.MouseMove
    '    Try
    '        If blnMoving Then
    '            With pnlAllergiesAlerts
    '                Dim temp As Point = New Point
    '                temp.X = .Location.X + (e.X - MouseDownX)
    '                temp.Y = .Location.Y + (e.Y - MouseDownY)
    '                .Location = temp
    '                .BringToFront()
    '            End With
    '        End If
    '    Catch ex As Exception

    '    End Try

    'End Sub

    'Private Sub lblAlert_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lblAlert1.MouseDown
    '    Try
    '        pnlAllergiesAlerts_MouseDown(sender, e)
    '    Catch ex As Exception

    '    End Try

    'End Sub
    'Private Sub lblAlert_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lblAlert1.MouseUp
    '    Try
    '        pnlAllergiesAlerts_MouseUp(sender, e)
    '    Catch ex As Exception

    '    End Try

    'End Sub
    'Private Sub lblAlert_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lblAlert1.MouseMove
    '    Try
    '        pnlAllergiesAlerts_MouseMove(sender, e)
    '    Catch ex As Exception

    '    End Try

    'End Sub


    'Private Sub picTop_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picTop.MouseDown
    '    Try
    '        pnlAllergiesAlerts_MouseDown(sender, e)
    '    Catch ex As Exception

    '    End Try

    'End Sub
    'Private Sub picTop_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picTop.MouseUp
    '    Try
    '        pnlAllergiesAlerts_MouseUp(sender, e)
    '    Catch ex As Exception

    '    End Try

    'End Sub
    'Private Sub picTop_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picTop.MouseMove
    '    Try
    '        pnlAllergiesAlerts_MouseMove(sender, e)
    '    Catch ex As Exception

    '    End Try

    'End Sub

    'Private Sub picMiddle_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picMiddle.MouseDown
    '    Try
    '        pnlAllergiesAlerts_MouseDown(sender, e)
    '    Catch ex As Exception

    '    End Try

    'End Sub
    'Private Sub picMiddle_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picMiddle.MouseUp
    '    Try
    '        pnlAllergiesAlerts_MouseUp(sender, e)
    '    Catch ex As Exception

    '    End Try

    'End Sub
    'Private Sub picMiddle_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picMiddle.MouseMove
    '    Try
    '        pnlAllergiesAlerts_MouseMove(sender, e)
    '    Catch ex As Exception

    '    End Try

    'End Sub


    'Private Sub picBottom_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picBottom.MouseDown
    '    Try
    '        pnlAllergiesAlerts_MouseDown(sender, e)
    '    Catch ex As Exception

    '    End Try

    'End Sub
    'Private Sub picBottom_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picBottom.MouseUp
    '    Try
    '        pnlAllergiesAlerts_MouseUp(sender, e)
    '    Catch ex As Exception

    '    End Try

    'End Sub
    'Private Sub picBottom_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picBottom.MouseMove
    '    Try
    '        pnlAllergiesAlerts_MouseMove(sender, e)
    '    Catch ex As Exception

    '    End Try

    'End Sub

    'Private Sub picAlertClose1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picAlertClose1.Click
    '    Try
    '        pnlAllergiesAlerts.Visible = False
    '    Catch ex As Exception
    '    End Try
    'End Sub
    'Code commented as allergies not to be shown
#End Region
    Private Sub frmPrescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.F2 Then
                With pnlAllergiesAlerts
                    If .Visible = False Then
                        .Visible = True
                        '''' 20060801 '' Alergies Alerts
                        GetAllergies()
                        ''''
                        .BringToFront()
                        .BackColor = Color.LightYellow
                    ElseIf .Visible = True Then
                        .Visible = False
                    End If
                End With
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub

    Private Sub picInfo_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picInfo.MouseHover
        Try
            Dim ToolTip1 = New System.Windows.Forms.ToolTip
            ToolTip1.SetToolTip(Me.picInfo, "Allergies Alerts")
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub
    Private Sub AddRefillControl()
        If Not IsNothing(_MedRefillC1FlexGridUserCtrl) Then
            RemoveRefillControl()
        End If
        splRight.Visible = False
        pnlRight.Visible = False

        _MedRefillC1FlexGridUserCtrl = New gloMedRefillC1FlexGridUserCtrl(_MedBusinessLayer)
        '_MedRefillC1FlexGridUserCtrl.BindGrid()
        Me.pnlRefill.Controls.Add(_MedRefillC1FlexGridUserCtrl)

        _MedRefillC1FlexGridUserCtrl.Dock = DockStyle.Fill
        _MedRefillC1FlexGridUserCtrl.Visible = True
        _MedRefillC1FlexGridUserCtrl.BringToFront()
        pnlRefill.Visible = True
    End Sub
    Private Sub RemoveRefillControl()
        If Not IsNothing(_MedRefillC1FlexGridUserCtrl) Then
            Me.pnlRefill.Controls.Remove(_MedRefillC1FlexGridUserCtrl)
            _MedRefillC1FlexGridUserCtrl.Visible = False
            _MedRefillC1FlexGridUserCtrl = Nothing
            pnlRefill.Visible = False
        End If
    End Sub
#End Region

    Private Sub _MedicationC1FlexGrdUserCtrl__FlexMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _MedicationC1FlexGrdUserCtrl._FlexMouseUp
        Try

            If _MedBusinessLayer.MedicationCol.Count > 0 Then
                Dim r As Int32
                r = CType(sender, C1.Win.C1FlexGrid.C1FlexGrid).HitTest(e.X, e.Y).Row
                If r > 0 Then

                    If pnlRefill.Controls.Contains(_MedRefillC1FlexGridUserCtrl) = True Then
                        pnlRefill.Controls.Remove(_MedRefillC1FlexGridUserCtrl)
                        pnlRefill.Visible = False
                    End If
                    If e.Button = Windows.Forms.MouseButtons.Right Then
                        RemoveControl()
                        Exit Sub
                    End If

                    AddControl()
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub


    'Private Sub _MedicationC1FlexGrdUserCtrl_SaveMedicationforOldStatus() Handles _MedicationC1FlexGrdUserCtrl.SaveMedicationforOldStatus
    '    Try
    '        Cursor.Current = Cursors.WaitCursor
    '        RefreshDIScreen()
    '        ' If _MedBusinessLayer.MedicationCol.Count > 0 Then
    '        'No need to perform screening while changing the combo status
    '        ''check screening result ,if true then show the count
    '        'Dim strmessage As String = ""
    '        'Dim strmessage1 As String = ""
    '        'If PerformAutoScreening(False, strmessage, strmessage1) Then
    '        '    DisplayAlertForUncodedDrugs()
    '        '    Dim result As Int16
    '        '    'Notes need to be added
    '        '    result = MessageBox.Show(strmessage & "Do you want to Override the alert and continue prescribing drugs?", "Medication", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    '        '    If result = vbYes Then
    '        '        _MedBusinessLayer.SaveMedication()

    '        '    Else

    '        '        Exit Sub
    '        '    End If
    '        '    'Drug alert for some

    '        '    'No Positive Drug Screening
    '        'Else
    '        '    DisplayAlertForUncodedDrugs()
    '        '    _MedBusinessLayer.SaveMedication()

    '        'End If
    '        ''Do not perform screening as no items in medication
    '        'No need to perform screening while changing the combo status
    '        ' End If
    '        Cursor.Current = Cursors.Default
    '    Catch ex As Exception
    '        Cursor.Current = Cursors.Default
    '    End Try
    'End Sub


    Private Sub _MedicationC1FlexGrdUserCtrl_cmbChangeRemoveControl() Handles _MedicationC1FlexGrdUserCtrl.cmbChangeRemoveControl
        'this event is called to remove the custom control if any when we change the status of the combo box
        Try
            RemoveControl()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub _MedicationC1FlexGrdUserCtrl_MedicationItemDeleted() Handles _MedicationC1FlexGrdUserCtrl.MedicationItemDeleted
        Try
            RemoveControl()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub


    Private Sub _MedicationC1FlexGrdUserCtrl_PrnFaxToggle(ByVal flag As Boolean) Handles _MedicationC1FlexGrdUserCtrl.PrnFaxToggle
        'this  event is called when the user changes the combo box status 
        'therefore if the status of the combo box is "Active" then the flag is False i.e don't make the buttons as Invisible
        'but if the status of the combo box is anything other than that of "Active" then the flag is True i.e. make the button as Invisible
        Try
            If flag = True Then
                _MedToolBarUserCtrl.tStrpPrint.Visible = False
                _MedToolBarUserCtrl.tStrpFax.Visible = False
            Else
                _MedToolBarUserCtrl.tStrpPrint.Visible = True
                _MedToolBarUserCtrl.tStrpFax.Visible = True
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub _MedBusinessLayer_Recordlock(ByVal blnRecordLock As Boolean) Handles _MedBusinessLayer.Recordlock

        If clsgeneral._blnRecordLock_Med = True Then
            '' If Medication Is Opened By other User the Open it in Locked Mode 
            _MedToolBarUserCtrl.tStrpSave.Enabled = False
            _MedToolBarUserCtrl.tStrpPrint.Enabled = False
            _MedToolBarUserCtrl.tStrpFax.Enabled = False
        Else
            _MedToolBarUserCtrl.tStrpSave.Enabled = True
            _MedToolBarUserCtrl.tStrpPrint.Enabled = True
            _MedToolBarUserCtrl.tStrpFax.Enabled = True
        End If

    End Sub

    'this  event is called to refresh the medicationflexgrid.
    Private Sub _MedBusinessLayer_RollRowsCount() Handles _MedBusinessLayer.RollRowsCount
        Try
            '_MedicationC1FlexGrdUserCtrl.ClearRows()
            ''_Flex.Rows.Count = 1
            'If _MedBusinessLayer.MedicationCol.Count > 0 Then

            _MedicationC1FlexGrdUserCtrl.BindFlexgrid()
            'rowindex = 1
            'End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub _MedHistoryUserCtrl_Recordlock(ByVal blnRecordLock As Boolean) Handles _MedHistoryUserCtrl.Recordlock

        If clsgeneral._blnRecordLock_Med = True Then
            '' If Medication Is Opened By other User the Open it in Locked Mode 
            _MedToolBarUserCtrl.tStrpSave.Enabled = False
            _MedToolBarUserCtrl.tStrpPrint.Enabled = False
            _MedToolBarUserCtrl.tStrpFax.Enabled = False
        Else
            _MedToolBarUserCtrl.tStrpSave.Enabled = True
            _MedToolBarUserCtrl.tStrpPrint.Enabled = True
            _MedToolBarUserCtrl.tStrpFax.Enabled = True
        End If
    End Sub
#Region "OLD Drug Interaction functions"
    Private Function PerformAutoScreeningOld(ByVal blnSave As Boolean, Optional ByRef strmessage As String = "", Optional ByRef strmessage1 As String = "") As Boolean
        Dim objautoscreening As ClsAutoScreening
        Try
            If Not IsNothing(objautoscreening) Then
                objautoscreening.mydispose()
                objautoscreening = Nothing
            End If
            objautoscreening = New ClsAutoScreening
            objautoscreening.DFAScreenStatus = gblnDFAAlert

            FillDrugsforDI(objautoscreening, 0)
            Dim objhash As Hashtable
            objautoscreening.SetScreeningAlert(gstrADESeverityLevel, gstrDISeverityLevel, gstrDFASeverityLevel, gstrDIDocLevel, gstrDFADocLevel)
            objhash = objautoscreening.PerformScreening
            ResultCol = objautoscreening.ResultCol
            If Not IsNothing(objhash) Then
                If objhash.Count > 0 Then
                    'Display the drug alert when invoked from AD button
                    'If invoked from 'AD' button,just suppress the drug alert and display the result

                    If blnSave Then
                        Dim result As Int16
                        result = MessageBox.Show("Drug Interaction found for newly Prescribed Drug", "Drug Interaction", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
                        If result = vbOK Then
                            DisplayScreeningResult(objhash)
                            Return True
                        Else
                            Return False
                        End If
                        'Display the drug alert when invoked from Save button
                    Else
                        DisplayScreeningResult(objhash)
                        strmessage = ""
                        strmessage = ValidateDrugInteractionScreen(objautoscreening, strmessage1)
                        Return True
                    End If
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        Finally
            objautoscreening = Nothing
        End Try

    End Function
    Private Function PerformAutoScreeningOld(ByVal drugid As Int64) As Boolean
        Try
            Dim objautoscreening As ClsAutoScreening
            If Not IsNothing(objautoscreening) Then
                objautoscreening.mydispose()
                objautoscreening = Nothing
            End If
            objautoscreening = New ClsAutoScreening
            objautoscreening.DFAScreenStatus = gblnDFAAlert

            ''For NDC Interaction
            Dim DDID As Int64 = 0
            Dim sNDCCode As String = ""

            'Send the DDID to be populated for the selected drug
            'FillDrugsforDI(objautoscreening, drugid, DDID)
            FillDrugsforDI(objautoscreening, drugid, sNDCCode)

            ''DDID >0  only then perform screening
            'If DDID > 0 Then
            '    Dim objhash As Hashtable
            '    Dim m_DrugAlert As String = ""
            '    objautoscreening.SetScreeningAlert(gstrADESeverityLevel, gstrDISeverityLevel, gstrDFASeverityLevel, gstrDIDocLevel, gstrDFADocLevel)
            '    objhash = objautoscreening.PerformScreening(m_DrugAlert, DDID)
            '    'Got atleast one screening result for the new drug which is to be prescribed
            '    If m_DrugAlert <> "" Then
            '        Dim result As Int16
            '        result = MessageBox.Show("Drug Interaction found for given Medication", "Drug Interaction", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
            '        If result = vbOK Then
            '            If Not IsNothing(objhash) Then
            '                objautoscreening = Nothing
            '                If objhash.Count > 0 Then
            '                    DisplayScreeningResult(objhash, m_DrugAlert)
            '                    Return True
            '                Else
            '                    Return False
            '                End If
            '            Else
            '                Return False
            '            End If
            '        Else
            '            Return False
            '        End If
            '    Else
            '        Return False
            '    End If
            'Else
            '    Return False
            'End If


            If sNDCCode <> "" Then
                Dim objhash As Hashtable
                Dim m_DrugAlert As String = ""
                objautoscreening.SetScreeningAlert(gstrADESeverityLevel, gstrDISeverityLevel, gstrDFASeverityLevel, gstrDIDocLevel, gstrDFADocLevel)
                objhash = objautoscreening.PerformScreening(m_DrugAlert, sNDCCode)
                'Got atleast one screening result for the new drug which is to be prescribed
                If m_DrugAlert <> "" Then
                    Dim result As Int16
                    result = MessageBox.Show("Drug Interaction found for given Medication", "Drug Interaction", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
                    If result = vbOK Then
                        If Not IsNothing(objhash) Then
                            objautoscreening = Nothing
                            If objhash.Count > 0 Then
                                DisplayScreeningResult(objhash, m_DrugAlert)
                                Return True
                            Else
                                Return False
                            End If
                        Else
                            Return False
                        End If
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub FillDrugsforDIOld(Optional ByVal drugid As Int64 = 0)
        strUncodedDrugs = New System.Text.StringBuilder
        Dim i As Int32
        'For NDC Interaction
        Dim ddid As Int64
        Dim sNDCCode As String
        'For NDC Interaction

        'Add Rx items to the objdruginteraction collection which will be used for screening drugs
        'Dim mynode As TreeNode
        'Check if Rx items present
        'If objPrescriptionDBLayer.PrescriptionColCount > 0 Then

        'If intMode = 1 Or (intMode = 2 And objMedicationDBLayer.FilterType = "Active") Then-----------------
        ' If intMode = 1 Or (intMode = 2 And _MedBusinessLayer.FilterType = "Active") Then
        If _MedBusinessLayer.TransactionMode = MedicationBusinessLayer._TransactionMode.Add Or (_MedBusinessLayer.TransactionMode = MedicationBusinessLayer._TransactionMode.Edit And _MedBusinessLayer.FilterType = "Active") Then

            'If C1FlexGrdMedicationDetails.Rows.Count > 0 Or drugid <> 0 Then-----------------
            If _MedBusinessLayer.MedicationCol.Count > 0 Or drugid <> 0 Then
                'ddid = 0
                sNDCCode = ""
                Dim strstatus As String
                'For i = 1 To C1FlexGrdMedicationDetails.Rows.Count - 1-----------------
                For i = 0 To _MedBusinessLayer.MedicationCol.Count - 1
                    'ddid = RetrieveDDID(CType(mynode.Nodes.Item(i), myTreeNode).Key)
                    ' strstatus = objMedicationDBLayer.GetStatus(i - 1)-------------
                    strstatus = _MedBusinessLayer.MedicationCol.Item(i).Status
                    If strstatus = "Active" Or strstatus = "" Then
                        'Dim drgId As Long = C1FlexGrdMedicationDetails.GetData(i, COL_DRUGID)
                        'ddid = C1FlexGrdMedicationDetails.GetData(i, COL_DDID) ' code added by sagar
                        'ddid = _MedBusinessLayer.MedicationCol.Item(i).DDID
                        sNDCCode = _MedBusinessLayer.MedicationCol.Item(i).NDCCode
                        'If ddid > 0 Then
                        '    objDrugInteraction.AddDrugtocol(ddid)
                        'Else
                        '    'strUncodedDrugs.Append(mynode.Nodes.Item(i).Text)'''''code commented by sagar to add the code written on next line for C1FlexGrid
                        '    'strUncodedDrugs.Append(C1FlexGrdMedicationDetails.GetData(i, COL_MEDICATION)) '''''code added by sagar 
                        '    strUncodedDrugs.Append(_MedBusinessLayer.MedicationCol.Item(i).Medication)
                        '    strUncodedDrugs.Append(vbCr)
                        'End If
                        If sNDCCode <> "" Then
                            objDrugInteraction.AddDrugtocol(sNDCCode)
                        Else
                            'strUncodedDrugs.Append(mynode.Nodes.Item(i).Text)'''''code commented by sagar to add the code written on next line for C1FlexGrid
                            'strUncodedDrugs.Append(C1FlexGrdMedicationDetails.GetData(i, COL_MEDICATION)) '''''code added by sagar 
                            strUncodedDrugs.Append(_MedBusinessLayer.MedicationCol.Item(i).Medication)
                            strUncodedDrugs.Append(vbCr)
                        End If
                    End If
                    strstatus = ""
                    'ddid = 0
                    sNDCCode = ""
                Next

                '  If intMode = 1 Then
                If _MedBusinessLayer.TransactionMode = MedicationBusinessLayer._TransactionMode.Add Then
                    Dim arrlist As New ArrayList
                    'get list of active medications for given visit from the database
                    'arrlist = objMedicationDBLayer.GetActiveMedications(m_visitdate, 0)
                    arrlist = _MedBusinessLayer.GetActiveMedications()

                    'ddid = 0
                    sNDCCode = ""

                    If arrlist.Count > 0 Then
                        For i = 0 To arrlist.Count - 1
                            'ddid = CType(arrlist.Item(i), mytable).Unit*************function available in class myTable
                            'This Id is declared in clsgloEMRActors which is the instead of myTable we use generalList class that has Id & description as property procedures

                            'ddid = CType(arrlist.Item(i), generalList).ID
                            sNDCCode = CType(arrlist.Item(i), generalList).ID

                            'if the ddid is not zero add it for screening purpose
                            'If ddid > 0 Then
                            '    objDrugInteraction.AddDrugtocol(ddid)
                            '    'if drug id is not zero then add it to string builder variable
                            '    'to alert the user that drug screening will not be performed
                            '    'for the same.
                            'Else
                            '    'strUncodedDrugs.Append(CType(arrlist.Item(i), mytable).Code)************include the mytable class                                strUncodedDrugs.Append(vbCr)
                            '    strUncodedDrugs.Append(CType(arrlist.Item(i), generalList).Description)
                            'End If
                            'ddid = 0

                            If sNDCCode <> "" Then
                                objDrugInteraction.AddDrugtocol(sNDCCode)
                                'if drug id is not zero then add it to string builder variable
                                'to alert the user that drug screening will not be performed
                                'for the same.
                            Else
                                'strUncodedDrugs.Append(CType(arrlist.Item(i), mytable).Code)************include the mytable class                                strUncodedDrugs.Append(vbCr)
                                strUncodedDrugs.Append(CType(arrlist.Item(i), generalList).Description)
                            End If
                            sNDCCode = ""

                        Next
                    End If
                End If
                If drugid <> 0 Then
                    'ddid = RetrieveDDID(drugid)
                    'ddid = _MedBusinessLayer.RetrieveDDID(drugid)
                    sNDCCode = _MedBusinessLayer.RetrieveNDCCode(drugid)

                    'If ddid > 0 Then
                    '    objDrugInteraction.AddDrugtocol(ddid)
                    'Else
                    '    '*************************
                    '    'confirm madam about what to write in this code because no event of trvlist is shown here
                    '    'If Not IsNothing(trDrugs.SelectedNode) Then
                    '    '    strUncodedDrugs.Append(trDrugs.SelectedNode.Text)
                    '    '    strUncodedDrugs.Append(vbCr)
                    '    'End If
                    '    '**************************
                    'End If
                    If sNDCCode <> "" Then
                        objDrugInteraction.AddDrugtocol(sNDCCode)
                    Else
                        '*************************
                        'confirm madam about what to write in this code because no event of trvlist is shown here
                        'If Not IsNothing(trDrugs.SelectedNode) Then
                        '    strUncodedDrugs.Append(trDrugs.SelectedNode.Text)
                        '    strUncodedDrugs.Append(vbCr)
                        'End If
                        '**************************
                    End If
                End If

                'For NDC Interaction
                'Check if allergies exist for that patient,if so then
                'populate the objdruginteraction collection with the allergies
                'associated with the patient.
                'If Not IsNothing(_MedBusinessLayer.HistoriesCol) Then
                '    'Check the count of medication items
                '    ddid = 0
                '    If _MedBusinessLayer.HistoriesCol.Count > 0 Then
                '        'Add the medication drugs to the objdruginteraction screening control
                '        For i = 0 To _MedBusinessLayer.HistoriesCol.Count - 1
                '            ddid = _MedBusinessLayer.RetrieveDDID(CType(_MedBusinessLayer.HistoriesCol.Item(i), History).DrugID)
                '            If ddid > 0 Then
                '                objDrugInteraction.AddDrugtocol1(ddid)
                '            End If
                '            ddid = 0
                '        Next
                '    End If
                'End If
                If Not IsNothing(_MedBusinessLayer.HistoriesCol) Then
                    'Check the count of medication items
                    'ddid = 0
                    sNDCCode = ""
                    If _MedBusinessLayer.HistoriesCol.Count > 0 Then
                        'Add the medication drugs to the objdruginteraction screening control
                        For i = 0 To _MedBusinessLayer.HistoriesCol.Count - 1
                            'ddid = _MedBusinessLayer.RetrieveDDID(CType(_MedBusinessLayer.HistoriesCol.Item(i), History).DrugID)
                            sNDCCode = _MedBusinessLayer.RetrieveNDCCode(CType(_MedBusinessLayer.HistoriesCol.Item(i), History).DrugID)
                            If sNDCCode <> "" Then
                                objDrugInteraction.AddDrugtocol1(sNDCCode)
                            End If
                            'ddid = 0
                            sNDCCode = ""
                        Next
                    End If
                End If

                'For NDC Interaction

                '--------------------------------------------------------
                If Not IsNothing(_MedBusinessLayer.MedicalCondtionCol) Then
                    If _MedBusinessLayer.MedicalCondtionCol.Count > 0 Then
                        'Add the medication drugs to the objdruginteraction screening control
                        'Check the count of medicalconditions items
                        Dim MedConditionId As Int64
                        If _MedBusinessLayer.MedicalCondtionCol.Count > 0 Then
                            'Add the medication drugs to the objdruginteraction screening control
                            For i = 1 To _MedBusinessLayer.MedicalCondtionCol.Count
                                MedConditionId = _MedBusinessLayer.MedicalCondtionCol.Item(i)
                                objDrugInteraction.AddDrugtocol2(MedConditionId)
                            Next
                        End If
                    End If
                End If
                '--------------------------------------------------------

            End If
            ' End If
            ' ElseIf intMode = 2 And objMedicationDBLayer.FilterType <> "Active" Then----------------
            ' ElseIf intMode = 2 And _MedBusinessLayer.FilterType <> "Active" Then
        ElseIf _MedBusinessLayer.TransactionMode = MedicationBusinessLayer._TransactionMode.Edit And _MedBusinessLayer.FilterType <> "Active" Then
            If drugid <> 0 Then
                'ddid = _MedBusinessLayer.RetrieveDDID(drugid)
                sNDCCode = _MedBusinessLayer.RetrieveNDCCode(drugid)
                'If ddid > 0 Then
                '    objDrugInteraction.AddDrugtocol(ddid)
                'Else
                '    '*************************
                '    'confirm madam about what to write in this code because no event of trvlist is shown here
                '    'If Not IsNothing(trDrugs.SelectedNode) Then
                '    '    strUncodedDrugs.Append(trDrugs.SelectedNode.Text)
                '    '    strUncodedDrugs.Append(vbCr)
                '    'End If
                '    '**************************
                'End If
                If sNDCCode <> "" Then
                    objDrugInteraction.AddDrugtocol(sNDCCode)
                Else
                    '*************************
                    'confirm madam about what to write in this code because no event of trvlist is shown here
                    'If Not IsNothing(trDrugs.SelectedNode) Then
                    '    strUncodedDrugs.Append(trDrugs.SelectedNode.Text)
                    '    strUncodedDrugs.Append(vbCr)
                    'End If
                    '**************************
                End If
            End If

            'If objMedicationDBLayer.FilterType <> "All" Then-------------------
            If _MedBusinessLayer.FilterType <> "All" Then
                Dim arrlist As New ArrayList
                'get list of active medications for given visit from the database
                ' arrlist = objMedicationDBLayer.GetActiveMedications(tempdate, tempVisitId)
                arrlist = _MedBusinessLayer.GetActiveMedications()

                'ddid = 0
                sNDCCode = ""

                If arrlist.Count > 0 Then
                    For i = 0 To arrlist.Count - 1
                        'ddid = CType(arrlist.Item(i), mytable).Unit*********************need to add the myTable class

                        'ddid = CType(arrlist.Item(i), generalList).ID
                        sNDCCode = CType(arrlist.Item(i), generalList).ID

                        'if the ddid is not zero add it for screening purpose
                        'If ddid > 0 Then
                        '    objDrugInteraction.AddDrugtocol(ddid)
                        '    'if drug id is not zero then add it to string builder variable
                        '    'to alert the user that drug screening will not be performed
                        '    'for the same.
                        'Else
                        '    'strUncodedDrugs.Append(CType(arrlist.Item(i), mytable).Code)********************need to access the mytable class
                        '    strUncodedDrugs.Append(CType(arrlist.Item(i), generalList).Description)
                        '    strUncodedDrugs.Append(vbCr)
                        'End If
                        'ddid = 0
                        If sNDCCode <> "" Then
                            objDrugInteraction.AddDrugtocol(sNDCCode)
                            'if drug id is not zero then add it to string builder variable
                            'to alert the user that drug screening will not be performed
                            'for the same.
                        Else
                            'strUncodedDrugs.Append(CType(arrlist.Item(i), mytable).Code)********************need to access the mytable class
                            strUncodedDrugs.Append(CType(arrlist.Item(i), generalList).Description)
                            strUncodedDrugs.Append(vbCr)
                        End If
                        sNDCCode = ""
                    Next
                End If
            End If


            '    mynode = trMedicationDetails.Nodes.Item(0)
            'ddid = 0
            sNDCCode = ""
            Dim strstatus As String = ""
            '    For i = 0 To mynode.GetNodeCount(False) - 1
            'For i = 1 To C1FlexGrdMedicationDetails.Rows.Count - 1
            For i = 0 To _MedBusinessLayer.MedicationCol.Count - 1
                'ddid = RetrieveDDID(CType(mynode.Nodes.Item(i), myTreeNode).Key)
                'strstatus = objMedicationDBLayer.GetStatus(i - 1)
                strstatus = _MedBusinessLayer.MedicationCol.Item(i).Status
                If strstatus = "Active" Or strstatus = "" Then
                    'ddid = CType(mynode.Nodes.Item(i), myTreeNode).DDID'''''code commented by sagar to add the code written on next line for C1FlexGrid
                    'Dim drgId As Long = C1FlexGrdMedicationDetails.GetData(i, COL_DRUGID)
                    'ddid = C1FlexGrdMedicationDetails.GetData(i, COL_DDID) ' code added by sagar

                    'ddid = _MedBusinessLayer.MedicationCol.Item(i).DDID
                    sNDCCode = _MedBusinessLayer.MedicationCol.Item(i).NDCCode

                    'If ddid > 0 Then
                    '    objDrugInteraction.AddDrugtocol(ddid)
                    'Else
                    '    'strUncodedDrugs.Append(mynode.Nodes.Item(i).Text)'''''code commented by sagar to add the code written on next line for C1FlexGrid
                    '    'strUncodedDrugs.Append(C1FlexGrdMedicationDetails.GetData(i, COL_MEDICATION)) '''''code added by sagar 
                    '    strUncodedDrugs.Append(_MedBusinessLayer.MedicationCol.Item(i).Medication)
                    '    strUncodedDrugs.Append(vbCr)
                    'End If
                    'ddid = 0
                    If sNDCCode <> "" Then
                        objDrugInteraction.AddDrugtocol(sNDCCode)
                    Else
                        'strUncodedDrugs.Append(mynode.Nodes.Item(i).Text)'''''code commented by sagar to add the code written on next line for C1FlexGrid
                        'strUncodedDrugs.Append(C1FlexGrdMedicationDetails.GetData(i, COL_MEDICATION)) '''''code added by sagar 
                        strUncodedDrugs.Append(_MedBusinessLayer.MedicationCol.Item(i).Medication)
                        strUncodedDrugs.Append(vbCr)
                    End If
                    sNDCCode = ""
                End If
                strstatus = ""
            Next

            'For NDC Interaction
            'Check if allergies exist for that patient,if so then
            'populate the objdruginteraction collection with the allergies
            'associated with the patient.
            'If Not IsNothing(_MedBusinessLayer.HistoriesCol) Then
            '    'Check the count of medication items
            '    ddid = 0
            '    If _MedBusinessLayer.HistoriesCol.Count > 0 Then
            '        'Add the medication drugs to the objdruginteraction screening control
            '        For i = 0 To _MedBusinessLayer.HistoriesCol.Count - 1
            '            ddid = _MedBusinessLayer.RetrieveDDID(CType(_MedBusinessLayer.HistoriesCol.Item(i), History).DrugID)
            '            If ddid > 0 Then
            '                objDrugInteraction.AddDrugtocol1(ddid)
            '            End If
            '            ddid = 0
            '        Next
            '    End If
            'End If
            If Not IsNothing(_MedBusinessLayer.HistoriesCol) Then
                'Check the count of medication items
                'ddid = 0
                sNDCCode = ""
                If _MedBusinessLayer.HistoriesCol.Count > 0 Then
                    'Add the medication drugs to the objdruginteraction screening control
                    For i = 0 To _MedBusinessLayer.HistoriesCol.Count - 1
                        'ddid = _MedBusinessLayer.RetrieveDDID(CType(_MedBusinessLayer.HistoriesCol.Item(i), History).DrugID)
                        sNDCCode = _MedBusinessLayer.RetrieveNDCCode(CType(_MedBusinessLayer.HistoriesCol.Item(i), History).DrugID)
                        If sNDCCode <> "" Then
                            objDrugInteraction.AddDrugtocol1(sNDCCode)
                        End If
                        'ddid = 0
                        sNDCCode = ""
                    Next
                End If
            End If
            'For NDC Interaction

            '--------------------------------------------------------
            If Not IsNothing(_MedBusinessLayer.MedicalCondtionCol) Then
                If _MedBusinessLayer.MedicalCondtionCol.Count > 0 Then
                    'Add the medication drugs to the objdruginteraction screening control
                    'Check the count of medicalconditions items
                    Dim MedConditionId As Int64
                    If _MedBusinessLayer.MedicalCondtionCol.Count > 0 Then
                        'Add the medication drugs to the objdruginteraction screening control
                        For i = 1 To _MedBusinessLayer.MedicalCondtionCol.Count
                            MedConditionId = _MedBusinessLayer.MedicalCondtionCol.Item(i)
                            objDrugInteraction.AddDrugtocol2(MedConditionId)
                        Next
                    End If
                End If
            End If
            '--------------------------------------------------------


        End If
    End Sub

#End Region
   
End Class


