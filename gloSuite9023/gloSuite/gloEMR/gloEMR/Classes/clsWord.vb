Imports Wd = Microsoft.Office.Interop.Word
Imports System.IO
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports System.Data.SqlClient
Imports System.Windows.Media.Imaging
Imports System.Runtime.InteropServices
Imports gloWord
Namespace gloEMRWord
    'added for tfs test 
    Interface IgloEMRWord
        Function GetData(ByVal DocumentType As enumDocType) As DataTable
        Sub GetFormFieldData(ByVal DocumentType As enumDocType, Optional ByVal sStatusText As String = "", Optional ByVal GurantorID As Int64 = 0, Optional ByRef oColManagement_option As CollLiquidData = Nothing, Optional ByRef oColLabs As CollLiquidData = Nothing, Optional ByRef oColoColX_Ray_Radiology As CollLiquidData = Nothing, Optional ByRef oColOther_Diagonsis_Tests As CollLiquidData = Nothing)
        Function InsertDocument(ByVal strFileName As String) As Byte()
        Function RetrieveDocumentFile() As String
    End Interface
    '''<summary>
    ''' Class containing the IOleMessageFilter
    ''' thread error-handling functions.
    '''</summary>
    Friend Class clsMessageFilter
        Implements IOleMessageFilter

        '''<summary>Start the filter.</summary>
        Public Shared Sub Register()
            Dim staThread As Boolean = False
            Try
                System.Threading.Thread.CurrentThread.SetApartmentState(System.Threading.ApartmentState.STA)
                staThread = True
            Catch ex As Exception
                staThread = False
            End Try
            If (staThread) Then
                Try
                    Dim newFilter As IOleMessageFilter = New clsMessageFilter()
                    Dim oldFilter As IOleMessageFilter = Nothing
                    CoRegisterMessageFilter(newFilter, oldFilter)
                Catch ex As Exception

                End Try

            End If


        End Sub

        '''<summary>Done with the filter, close it.</summary>
        Public Shared Sub Revoke()
            Dim staThread As Boolean = False
            Try
                System.Threading.Thread.CurrentThread.SetApartmentState(System.Threading.ApartmentState.STA)
                staThread = True
            Catch ex As Exception
                staThread = False
            End Try
            If (staThread) Then
                Try
                    Dim oldFilter As IOleMessageFilter = Nothing
                    CoRegisterMessageFilter(Nothing, oldFilter)
                Catch ex As Exception

                End Try
            End If
        End Sub


        ' IOleMessageFilter functions.

        '''<summary>Handle incoming thread requests.</summary>
        Private Function HandleInComingCall(ByVal dwCallType As Integer, ByVal hTaskCaller As System.IntPtr, ByVal dwTickCount As Integer, ByVal lpInterfaceInfo As System.IntPtr) As Integer Implements IOleMessageFilter.HandleInComingCall
            'Return the flag SERVERCALL_ISHANDLED.
            Return 0
        End Function

        '''<summary>Thread call was rejected, so try again.</summary>
        Private Function RetryRejectedCall(ByVal hTaskCallee As System.IntPtr, ByVal dwTickCount As Integer, ByVal dwRejectType As Integer) As Integer Implements IOleMessageFilter.RetryRejectedCall
            If dwRejectType = 2 Then
                ' flag = SERVERCALL_RETRYLATER.
                ' Retry the thread call immediately if return >=0 & 
                ' <100.
                Return 99
            End If
            ' Too busy; cancel call.
            Return -1
        End Function

        Private Function MessagePending(ByVal hTaskCallee As System.IntPtr, ByVal dwTickCount As Integer, ByVal dwPendingType As Integer) As Integer Implements IOleMessageFilter.MessagePending
            'Return the flag PENDINGMSG_WAITDEFPROCESS.
            Return 2
        End Function

        '''<summary>Implement the IOleMessageFilter interface.</summary>
        <DllImport("Ole32.dll")> _
        Private Shared Function CoRegisterMessageFilter(ByVal newFilter As IOleMessageFilter, ByRef oldFilter As IOleMessageFilter) As Integer
        End Function
    End Class


    <ComImport(), Guid("00000016-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)> _
    Interface IOleMessageFilter
        <PreserveSig()> _
        Function HandleInComingCall(ByVal dwCallType As Integer, ByVal hTaskCaller As IntPtr, ByVal dwTickCount As Integer, ByVal lpInterfaceInfo As IntPtr) As Integer

        <PreserveSig()> _
        Function RetryRejectedCall(ByVal hTaskCallee As IntPtr, ByVal dwTickCount As Integer, ByVal dwRejectType As Integer) As Integer

        <PreserveSig()> _
        Function MessagePending(ByVal hTaskCallee As IntPtr, ByVal dwTickCount As Integer, ByVal dwPendingType As Integer) As Integer
    End Interface

    '    Public Class WordRefresh
    '        Implements IDisposable

    '        Private panel As Panel = Nothing
    '        Private waitControl As CustomControlWait = Nothing

    '        Public Sub New()
    '            'panel = New Panel()
    '            'waitControl = New CustomControlWait()
    '            'panel.Controls.Add(waitControl)

    '            'waitControl.Dock = DockStyle.Fill
    '            'panel.Dock = DockStyle.Fill
    '        End Sub

    '        Public Sub OptimizePerformance(ByVal StopWordDisp As Boolean, ByRef Wd1 As Wd.Document, WDocViewType As Wd.WdViewType)
    '            'If (IsNothing(Wd1) = False) Then
    '            '    If StopWordDisp = False Then
    '            '        With Wd1
    '            '            .Application.ScreenUpdating = StopWordDisp
    '            '            '.Application.Options.Pagination = StopWordDisp
    '            '            '.ActiveWindow.View.Type = Wd.WdViewType.wdNormalView
    '            '        End With

    '            '    Else
    '            '        With Wd1
    '            '            '.ActiveWindow.View.Type = WDocViewType
    '            '            '.Application.Options.Pagination = StopWordDisp
    '            '            .Application.ScreenUpdating = StopWordDisp
    '            '        End With
    '            '    End If
    '            'End If

    '        End Sub

    '        Public Sub ShowPanel(PnlActivePanel As Control)

    '            'panel.Left = (PnlActivePanel.Width - waitControl.Width) / 2
    '            'panel.Top = (PnlActivePanel.Height - waitControl.Height) / 2

    '            'waitControl.Height = PnlActivePanel.Height
    '            'waitControl.Width = PnlActivePanel.Width

    '            'PnlActivePanel.Controls.Add(panel)
    '            'panel.Show()
    '            'panel.BringToFront()
    '        End Sub

    '        Public Sub HidePanel(PnlActivePanel As Control)
    '            'If panel.Controls.Contains(waitControl) Then
    '            '    panel.Controls.Remove(waitControl)
    '            'End If

    '            'If PnlActivePanel.Controls.Contains(panel) Then
    '            '    PnlActivePanel.Controls.Remove(panel)
    '            'End If
    '        End Sub
    '#Region "IDisposable Support"
    '        Private disposedValue As Boolean ' To detect redundant calls

    '        ' IDisposable
    '        Protected Overridable Sub Dispose(disposing As Boolean)
    '            If Not Me.disposedValue Then
    '                If disposing Then
    '                    'If Me.waitControl IsNot Nothing Then
    '                    '    Me.waitControl.Dispose()
    '                    '    Me.waitControl = Nothing
    '                    'End If

    '                    'If Me.panel IsNot Nothing Then
    '                    '    Me.panel.Dispose()
    '                    '    Me.panel = Nothing
    '                    'End If
    '                End If

    '                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
    '                ' TODO: set large fields to null.
    '            End If
    '            Me.disposedValue = True
    '        End Sub

    '        ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    '        'Protected Overrides Sub Finalize()
    '        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '        '    Dispose(False)
    '        '    MyBase.Finalize()
    '        'End Sub

    '        ' This code added by Visual Basic to correctly implement the disposable pattern.
    '        Public Sub Dispose() Implements IDisposable.Dispose
    '            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '            Dispose(True)
    '            GC.SuppressFinalize(Me)
    '        End Sub
    '#End Region

    '    End Class


    Public Class clsWordDocument
        Implements IgloEMRWord


        Public Property IncludeCaption As Boolean = False        
        Public AddDictionaryParent As Form = Nothing
        Private _oCurDoc As Wd.Document
        Private _DocumentCriteria As DocCriteria
        Private _IsImageCopied As Boolean = False
        Private _DocumentType As enumDocType

        Private Const _History As String = "History"
        Private Const _OBGeneticHistory As String = "OB Genetic History"
        Private Const _OBInfectionHistory As String = "OB Infection History"
        Private Const _OBInitialPhysicalExamination As String = "OB Initial Physical Examination"
        Private Const _OBMedicalHistory As String = "OB Medical History"
        Private Const _OBPlan As String = "OB Plan"

        Private Const _Medication As String = "Medication"
        Private Const _Referrals As String = "Referrals"
        Private Const _Prescription As String = "Prescription"
        Private Const _Exam As String = "Exam"
        Private Const _RadiologyOrder As String = "LM_Test"
        Private Const _LabOrder As String = "Lab_Test_Mst"
        Private Const _Message As String = "Message"

        Private Const _Diagnosis As String = "ExamICD9CPT"

        '28-Dec-15 Resolving Bug #92200: gloEMR: (Liquid link) CPT with charges: CPT with charges liquid link is not working properly
        Private Const _ExamICD9CPT_PM As String = "ExamICD9CPT_PM"

        Private Const _Treatment As String = "ExamICD9CPT"
        Private Const _VitalsDAS As String = "DAS"
        Private Const _Vitals As String = "Vitals"
        Private Const _OBVitals As String = "OBVitals"
        Private Const _ROS As String = "ROS"
        Private Const _PatientEducation As String = "ExamEducation"
        Private Const _Flowsheet As String = "FlowSheet1"
        Private Const _SmartDiagnosis As String = "SmartDiagnosis"
        Private Const _ProblemList As String = "ProblemList"
        Private Const _SmartTreatment As String = "SmartTreatment"
        Private Const _Tasks As String = "TM_Task_Progress"
        Private Const _CheifComplaints As String = "PatientChiefComplaint"
        Private Const _PatientDemographics As String = "Patient"
        Private Const _Contacts_Detail As String = "Contacts_Detail" ''added  for incident 00065814  PER s hospital affiliation
        '21-May-15 Aniket: Open Modify Patient on Patient Insurance Liquid Link
        Private Const _PatientInsurance_DTL As String = "PatientInsurance_DTL"

        Private Const _PatientGuideline As String = "PatientMaterial"
        Private Const _Patient_DTL As String = "Patient_DTL"
        Private Const _Others As String = "Others"
        Private Const _None As String = "None"
        Private Const _Contacts As String = "Contacts_MST"
        Private Const _Narration As String = "Narration"
        Private Const _ProviderSign As String = "Provider_MST"
        Private Const _Clinic As String = "Clinic_MST"
        Private Const _Fax As String = "FAX"
        Private Const _PatientExam As String = "PatientExams"
        Private Const _DisclosureSet As String = "DisclosureSet"
        Private Const _PateintExamDos As String = "PatientExamsDos"
        Private Const _PatientExamsDx As String = "PatientExamsDx"
        Private nPatientID As Long
        Private Const _stresstest = "cv_stresstest"
        Private Const _ElectroPhysiology = "CV_ElectroPhysiology"
        Private Const _CardiologyDevice = "CV_CardiologyDevice"
        Private Const _ElectroCardioGrams = "CV_ElectroCardioGrams"
        Private Const _Echocardiogram = "CV_Echocardiogram"
        Private Const _Catheterization = "CV_Catheterization"
        Private Const _DAS = "DAS"
        Private Const _PastPregnancies As String = "PastPregnancies"

        '5-Oct-15 Aniket: Patient Alerts Liquid Link
        Private Const _PatientAlerts As String = "PatientAlerts"

        Private strPatientCode As String = ""
        Private strPatientFirstName As String = ""
        Private strPatientLastName As String = ""
        Private strPatientDOB As String = ""
        Private strPatientAge As String = ""
        Private strPatientGender As String = ""
        Private strPatientMaritalStatus As String = ""
        Public dtHoldDataForCPTModifierLL As DataTable
        'Flag is Settled For Applying Double Click event On Word Document -Yatin 20111222(Bug No. 17073 )
        Public LiquidFlag As Boolean = False

        'Added by Ashish on 4th November 2014
        'to show wait image on execution of 
        'Add Fields functionality
        Public Property WaitControlPanel As Control = Nothing
        Public Property DisableWordRefresh As Boolean = False
        Public Property IsLiquidFormField As Boolean = False
        Public Property historycont As Integer = 0
        'Public Property oColManagement_option = New CollLiquidData
        'Public Property oColLabs = New CollLiquidData
        'Public Property oColX_Ray_Radiology = New CollLiquidData
        'Public Property oColOther_Diagonsis_Tests = New CollLiquidData


#Region "Declaration of variables for liquid data"
        'constant for compairing _ChiefComplaints field value to know chief complaint liquid field is clicked
        'Private Const _ChiefComplaints As String = "sChiefComplaint"
        Private Const _ChiefComplaints As String = "PatientChiefComplaint"

        'variable for date of service
        Public dtDOS As DateTime = DateTime.Now.Date 'Date of service
        Dim FieldValue As String ''// To get the field value from the Window Double click event of Word User Control 
        'to track root calling form of clsworddocument
        Public myCallingForm As System.Windows.Forms.Form
        Dim _blnHasAdvDirective As Boolean = False
        Dim mgnVisitID As Long
        Dim ExamProviderId As Long
        Dim strDiagnosis As String
        'check referances and logical use before check in        
        '  Friend WithEvents wdNewExam As AxDSOFramer.AxFramerControl = Nothing
        'Private WithEvents oWordApp As Wd.Application
#End Region
        Public Property PatientId() As Long
            Get
                Return nPatientID
            End Get
            Set(ByVal value As Long)
                nPatientID = value
            End Set
        End Property
        Public Property CurDocument() As Wd.Document
            Get
                Return _oCurDoc
            End Get
            Set(ByVal value As Wd.Document)
                _oCurDoc = value
            End Set
        End Property


        Public Property DocumentCriteria() As DocCriteria
            Get
                Return _DocumentCriteria
            End Get
            Set(ByVal value As DocCriteria)
                _DocumentCriteria = value
            End Set
        End Property
        Public Property IsImageCopied() As Boolean
            Get
                Return _IsImageCopied
            End Get
            Set(value As Boolean)
                _IsImageCopied = value
            End Set
        End Property

        ''Used in Generate file
        Dim _myPhoto As Boolean = False

        '//Get Data from DB and replace the data with form field


        Public Function getData_FromDB(ByVal strFields As String, Optional ByVal strHelpError As String = "", Optional ByRef dtTable As DataTable = Nothing) As String
            '    Dim oDB As DataBaseLayer
            Dim oDB As DataBaseLayer = New DataBaseLayer
            Dim oParamater As DBParameter
            Dim oResultTable As DataTable = Nothing
            Dim dvICD9CPT As DataView = Nothing
            Dim dtICD9CPT As DataTable = Nothing
            Try
                'Parameter Varibles - Start
                Dim _PatientID As Int64 = _DocumentCriteria.PatientID
                Dim _DocCategory As Int32 = _DocumentCriteria.DocCategory
                Dim _PrimaryID As Int64 = _DocumentCriteria.PrimaryID
                Dim _VisitID As Int64 = _DocumentCriteria.VisitID
                Dim _AppointmetID As Int64 = _DocumentCriteria.AppointmentID
                ''Added On 20101006 by sanjog for Signaure
                Dim _ProviderID As Int64 = _DocumentCriteria.ProviderID
                ''Added On 20101006 by sanjog for Signaure
                Dim DaysToAdd As Int16 = 0
                Dim MonthsToAdd As Int16 = 0

                'Parameter Varibles - Finish


                '' SUDHIR 20091212 '' FETCH CLOSEST POSSIBLE APPOINTMENT ID ''
                If strFields.EndsWith("CurrentAppointment") OrElse strFields.StartsWith("AS_Appointment_DTL") Then
                    'Dim _Query As String = " SELECT TOP 1 nMSTAppointmentID, dtStartDate FROM AS_Appointment_MST " & " WHERE dtStartDate <= " & gloDateMaster.gloDate.DateAsNumber(Now.Date.ToShortDateString()) & " AND nPatientID = " & _PatientID & " ORDER BY dtStartDate DESC"
                    Dim _Query As String
                    Dim _QueryDos As String
                    If (_DocCategory = 2) Then
                        Dim oResultDos As Object
                        'Need to initialize as it will capture string/decimal/Datetime value,if not initialized it  will raise Conversion Exception
                        Dim DOS As Date
                        _QueryDos = "SELECT dtDOS from PatientExamS where nExamID=" & _PrimaryID
                        Dim _oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
                        _oDB.Connect(False)
                        oResultDos = _oDB.ExecuteScalar_Query(_QueryDos)
                        If oResultDos IsNot Nothing AndAlso oResultDos.ToString() <> "" Then
                            DOS = Convert.ToDateTime(oResultDos)
                        End If

                        'Aniket: 22-Mar-13 Resolving Memory Leak Issues. Dispose not implemented for the class as yet
                        oResultDos = Nothing

                        _oDB.Disconnect()
                        _oDB.Dispose()
                        _oDB = Nothing  'Change made to solve memory Leak and word crash issue

                        ' Problem 27 : Appointment date liquid link is no longer pulling 
                        ' if the patient has no deleted appointments on the same day
                        _Query = "SELECT TOP 1 nDTLAppointmentID, AS_Appointment_MST.dtStartDate FROM AS_Appointment_MST INNER JOIN Patient ON AS_Appointment_MST.nPatientID = Patient.nPatientID INNER JOIN AS_Appointment_DTL ON  AS_Appointment_MST.nMSTAppointmentID =AS_Appointment_DTL.nMSTAppointmentID  WHERE AS_Appointment_DTL.dtStartDate = " & gloDateMaster.gloDate.DateAsNumber(DOS.ToShortDateString()) & " AND nRefFlag = 0 AND AS_Appointment_DTL.nUsedStatus not in (6,7) AND AS_Appointment_MST.nPatientID = " & _PatientID & " ORDER BY AS_Appointment_DTL.dtStartDate ,AS_Appointment_DTL.dtStartTime"


                    Else
                        ' Problem 27 : Appointment date liquid link is no longer pulling 
                        ' if the patient has no deleted appointments on the same day
                        _Query = "SELECT TOP 1 nDTLAppointmentID, AS_Appointment_MST.dtStartDate FROM AS_Appointment_MST INNER JOIN Patient ON AS_Appointment_MST.nPatientID = Patient.nPatientID INNER JOIN AS_Appointment_DTL ON  AS_Appointment_MST.nMSTAppointmentID =AS_Appointment_DTL.nMSTAppointmentID  WHERE AS_Appointment_DTL.dtStartDate = " & gloDateMaster.gloDate.DateAsNumber(Now.Date.ToShortDateString()) & "AND nRefFlag = 0 AND AS_Appointment_DTL.nUsedStatus not in (6,7)  AND AS_Appointment_MST.nPatientID = " & _PatientID & " ORDER BY AS_Appointment_DTL.dtStartDate ,AS_Appointment_DTL.dtStartTime"
                    End If

                    Dim oResult As Object
                    Dim _DB As New gloDatabaseLayer.DBLayer(GetConnectionString)
                    _DB.Connect(False)
                    oResult = _DB.ExecuteScalar_Query(_Query)
                    If oResult IsNot Nothing AndAlso oResult.ToString() <> "" Then
                        _PrimaryID = Convert.ToInt64(oResult)
                    End If
                    _DB.Disconnect()
                    _DB.Dispose()
                    _DB = Nothing 'Change made to solve memory Leak and word crash issue

                    'do not dispose oResult as it holds integer/decimal values and integer/decimal does not have dispose method. It will throw exception.
                    oResult = Nothing
                End If


                ' oResultTable = New DataTable  'SLR: new is not neeeded
                Dim strData As String
                Dim ResultDataType As Integer
                Dim filecnt As Int16
                Dim strDataCol As String

                ''//Mapping values for PatienID, ExamID, VisitID
                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@nPatientID"
                oParamater.Value = _PatientID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@nPrimaryID"
                oParamater.Value = _PrimaryID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@nVisitID"
                oParamater.Value = _VisitID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.Int
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@DocumentCategory"
                oParamater.Value = _DocCategory
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@nFieldId1"
                oParamater.Value = 0
                If strFields.StartsWith("Vitals.") Then
                    Dim strSplit() As String
                    Dim strSplitlocal As String

                    strSplit = Split(strFields, "|")
                    If strSplit.Length > 1 Then
                        strSplitlocal = strSplit(1).Trim.ToString()
                        If strSplitlocal = "01" OrElse strSplitlocal = "02" OrElse strSplitlocal = "03" OrElse strSplitlocal = "04" OrElse strSplitlocal = "05" OrElse strSplitlocal = "10" Then
                            oParamater.Value = Convert.ToInt16(strSplitlocal)
                        ElseIf (strSplitlocal = "LastThreeDays") Then
                            DaysToAdd = -3
                        ElseIf (strSplitlocal = "LastWeek") Then
                            DaysToAdd = -7
                        ElseIf (strSplitlocal = "LastMonth") Then
                            MonthsToAdd = -1
                        ElseIf (strSplitlocal = "LastThreeMonths") Then
                            MonthsToAdd = -3
                        ElseIf (strSplitlocal = "LastSixMonths") Then
                            MonthsToAdd = -6
                        ElseIf (strSplitlocal = "LastYear") Then
                            MonthsToAdd = -12
                        End If
                    End If
                    strSplit = Nothing
                    strSplitlocal = Nothing

                ElseIf strFields.StartsWith("pa_accounts.") OrElse strFields.StartsWith("PA_Accounts_Patients.") OrElse strFields.StartsWith("pa_accounts_Billing.") OrElse strFields.StartsWith("pa_accounts_PatientLastClaimDiag.") Then
                    oParamater.Value = DocumentCriteria.FieldID1
                End If

                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@nFieldId2"
                oParamater.Value = 0
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@nFieldId3"
                oParamater.Value = 0
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                '' NEW PARAMETERS FOR gloPM ''
                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@nAppintmentID"
                oParamater.Value = _AppointmetID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@nProviderID"
                ''Added On 20101006 by sanjog for Signaure
                oParamater.Value = _ProviderID
                ''Added On 20101006 by sanjog for Signaure
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@nClinicID"
                oParamater.Value = gnClinicID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@nFromDate"

                Dim VisitDate As Date
                If _VisitID = PreviousVisitID AndAlso PreviousVisitID <> -1 Then
                    VisitDate = PreviousVisitDate
                Else
                    VisitDate = GetVisitdate(_VisitID)
                    PreviousVisitID = _VisitID
                    PreviousVisitDate = VisitDate
                End If

                If strFields = "Lab_Test_Mst.LabResults|LastWeek" Then
                    VisitDate = VisitDate.AddDays(-7)
                ElseIf strFields = "Lab_Test_Mst.LabResults|LastMonth" Then
                    VisitDate = VisitDate.AddMonths(-1)
                ElseIf strFields = "Lab_Test_Mst.LabResults|LastSixMonths" Then
                    VisitDate = VisitDate.AddMonths(-6)
                End If

                If (_VisitID > 0) Then
                    If (DaysToAdd < 0) Then
                        oParamater.Value = gloDateMaster.gloDate.DateAsNumber(VisitDate.Date.AddDays(DaysToAdd).ToShortDateString())
                    ElseIf (MonthsToAdd < 0) Then
                        oParamater.Value = gloDateMaster.gloDate.DateAsNumber(VisitDate.Date.AddMonths(MonthsToAdd).ToShortDateString())
                    Else
                        oParamater.Value = gloDateMaster.gloDate.DateAsNumber(VisitDate.Date.ToShortDateString())
                        'oParamater.Value = gloDateMaster.gloDate.DateAsNumber(Now.Date.ToShortDateString())
                    End If
                Else
                    oParamater.Value = gloDateMaster.gloDate.DateAsNumber(Now.Date.ToShortDateString())
                End If


                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@nToDate"
                If (DaysToAdd < 0) OrElse (MonthsToAdd < 0) Then
                    oParamater.Value = gloDateMaster.gloDate.DateAsNumber(VisitDate.Date.ToShortDateString())
                Else
                    oParamater.Value = gloDateMaster.gloDate.DateAsNumber(Now.Date.ToShortDateString())
                End If

                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                '' gloPM PARAMETERS END ''
                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.VarChar
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@ServerName"
                oParamater.Value = gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsServerName
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing


                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.VarChar
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@DatabaseName"
                oParamater.Value = gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsDatabaseName
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                ''Bug #61724: 00000605 : When using the Liquid Link for "Time" it pulls in Eastern Time instead of the practice's local time
                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.DateTime
                oParamater.Name = "@TodaysDate"
                oParamater.Direction = ParameterDirection.Input
                oParamater.Value = Now
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing
                ''----End -- Bug #61724: 00000605

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.VarChar
                oParamater.Name = "@sFields"
                oParamater.Direction = ParameterDirection.Input


                strData = ""
                If strFields <> "" Then

                    If strFields.StartsWith("FlowSheet") AndAlso strFields.EndsWith("SingleRow") Then
                        oParamater.Value = Mid(strFields, 1, InStrRev(strFields, "|") - 1)
                    Else
                        oParamater.Value = strFields
                    End If
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing
                    'If InStr(strFields, "SingleRow") And InStr(strFields, "FlowSheet") Then
                    '    oParamater.Value = Mid(strFields, 1, InStrRev(strFields, "|") - 1)
                    'Else
                    '    oParamater.Value = strFields
                    'End If
                    'oDB.DBParametersCol.Add(oParamater)
                    'oParamater = Nothing

                    If strFields.StartsWith("FAX") Then
                        strDataCol = strData & "|" & ResultDataType.ToString
                        Return strDataCol
                    End If

                    oResultTable = oDB.GetDataTable("GetFormFieldsData")
                    If oResultTable Is Nothing Then
                        Return ""
                    End If

                    If oResultTable.Rows.Count > 0 Then

                        ''// Check if Form field is of Following types
                        If InStr(strFields, "Narration") OrElse InStr(strFields, "LM_LabResult") OrElse InStr(strFields, "imgSignature") OrElse InStr(strFields, "imgClinicLogo") OrElse InStr(strFields, "iPhoto") OrElse InStr(strFields, "iDASImage") Then

                            For i As Int32 = 0 To oResultTable.Rows.Count - 1
                                _myPhoto = False
                                '   For j As Int32 = 0 To oResultTable.Columns.Count - 1
                                If IsDBNull(oResultTable.Rows(i).Item(0)) = False Then
                                    Dim strFileName As String
                                    If oResultTable.Rows(i).Item(1) = "2" Then
                                        filecnt = filecnt + 1
                                        If InStr(strFields, "Narration") Then
                                            strFileName = gloSettings.FolderSettings.AppTempFolderPath & getUniqueID() & "Narration.txt"
                                        Else
                                            strFileName = gloSettings.FolderSettings.AppTempFolderPath & getUniqueID() & "Flowsheet" & filecnt & ".txt"
                                        End If
                                    Else
                                        If strFields = "Provider.imgSignature" Then
                                            strFileName = gloSettings.FolderSettings.AppTempFolderPath & getUniqueID() & "imgSignature.bmp"
                                        ElseIf strFields = "User_MST.imgSignature" Then
                                            strFileName = gloSettings.FolderSettings.AppTempFolderPath & getUniqueID() & "imgCoSignature.bmp"
                                        ElseIf strFields = "Patient_Cards.iCard|Driving" Then
                                            strFileName = gloSettings.FolderSettings.AppTempFolderPath & getUniqueID() & "imgDrivingLicense.bmp"
                                        ElseIf strFields = "Patient_Cards.iCard|Insurance" Then
                                            strFileName = gloSettings.FolderSettings.AppTempFolderPath & getUniqueID() & "imgInsuranceCard.bmp"
                                        ElseIf strFields = "DAS.iDASImage" Then
                                            strFileName = gloSettings.FolderSettings.AppTempFolderPath & getUniqueID() & "DasCalculater.bmp"
                                        Else
                                            _myPhoto = InStr(strFields, "iPhoto") '' Used in Generate File
                                            strFileName = gloSettings.FolderSettings.AppTempFolderPath & getUniqueID() & "imgPhoto.bmp"
                                        End If
                                    End If

                                    strData = strFileName
                                    ''Save contents in file
                                    If File.Exists(strFileName) Then
                                        File.Delete(strFileName)
                                    End If
                                    'SLR: Modified on 3/28/2014
                                    strFileName = GenerateFile(oResultTable.Rows(i).Item(0), strFileName)
                                    If InStr(strFields, "SingleRow") Then
                                        GetLastLine(strFileName)
                                    End If
                                    '''''
                                    ResultDataType = oResultTable.Rows(i).Item(1)
                                End If
                                'Next
                            Next

                        ElseIf InStr(strFields, "iCard") Then
                            Dim strFileName As String = "DoNotExist"
                            For i As Int32 = 0 To oResultTable.Rows.Count - 1
                                _myPhoto = False
                                '   For j As Int32 = 0 To oResultTable.Columns.Count - 1
                                If IsDBNull(oResultTable.Rows(i).Item(0)) = False Then

                                    If strFields = "Patient_Cards.iCard |Driving" Then
                                        strFileName = gloSettings.FolderSettings.AppTempFolderPath & getUniqueID() & "imgDrivingLicense.bmp"
                                    ElseIf strFields = "Patient_Cards.iCard |Insurance" Then
                                        strFileName = gloSettings.FolderSettings.AppTempFolderPath & getUniqueID() & "imgInsuranceCard.bmp"
                                    End If
                                    strData = strFileName

                                    If File.Exists(strFileName) Then
                                        File.Delete(strFileName)
                                    End If
                                    'SLR: Modified on 3/28/2014
                                    strFileName = GenerateFile(oResultTable.Rows(i).Item(0), strFileName)
                                    If InStr(strFields, "SingleRow") Then
                                        GetLastLine(strFileName)
                                    End If
                                    '''''
                                End If

                                If IsDBNull(oResultTable.Rows(i).Item(1)) = False Then
                                    If strFields = "Patient_Cards.iCard |Driving" Then
                                        strFileName = gloSettings.FolderSettings.AppTempFolderPath & getUniqueID() & "imgDrivingLicense_Back.bmp"
                                    ElseIf strFields = "Patient_Cards.iCard |Insurance" Then
                                        strFileName = gloSettings.FolderSettings.AppTempFolderPath & getUniqueID() & "imgInsuranceCard_Back.bmp"
                                    End If
                                    strData = strData + "~" + strFileName
                                    If File.Exists(strFileName) Then
                                        File.Delete(strFileName)
                                    End If
                                    'SLR: Modified on 3/28/2014
                                    strFileName = GenerateFile(oResultTable.Rows(i).Item(1), strFileName)
                                    If InStr(strFields, "SingleRow") Then
                                        GetLastLine(strFileName)
                                    End If
                                    '''''
                                End If

                                ResultDataType = oResultTable.Rows(i).Item(2)
                            Next

                        ElseIf InStr(strFields, "FlowSheet") Then
                            ''Sudhir 20090225 ''
                            Dim oDBTemp As New gloDatabaseLayer.DBLayer(GetConnectionString)
                            Dim Query As String = ""
                            If strFields.StartsWith("FlowSheet") AndAlso strFields.EndsWith("SingleRow") Then
                                Query = " SELECT nTotalCols FROM FlowSheet1 WHERE sFlowSheetName = '" & Mid(strFields, InStr(strFields, "|") + 1, strFields.Length).Replace("|SingleRow", "").Trim.Replace("'", "''") & "' AND nPatientID = " & _PatientID & ""
                            Else
                                Query = " SELECT nTotalCols FROM FlowSheet1 WHERE sFlowSheetName = '" & Mid(strFields, InStr(strFields, "|") + 1, strFields.Length).Trim.Replace("'", "''") & "' AND nPatientID = " & _PatientID & ""
                            End If

                            oDBTemp.Connect(False)
                            Dim objColums As Object = oDBTemp.ExecuteScalar_Query(Query)
                            oDBTemp.Disconnect()
                            oDBTemp.Dispose()
                            oDBTemp = Nothing
                            Dim nColumnCount As Integer = CType(objColums, Integer)

                            ''Fill DataColumns

                            If Not IsNothing(dtTable) Then  ''slr free previous memory
                                dtTable.Dispose()
                            End If
                            dtTable = Nothing
                            dtTable = New DataTable
                            For j As Integer = 0 To nColumnCount - 1
                                dtTable.Columns.Add(oResultTable.Rows(j)("sFieldName").ToString)
                            Next

                            ''Fill All Data to dtFlowSheet
                            ''Read each value from database and store as a datarow.
                            Dim nRow As Int32 = 0
                            If strFields.StartsWith("FlowSheet") AndAlso strFields.EndsWith("SingleRow") Then
                                ''IF SINGLEROW THEN  WE ARE FETCHING LAST ROW OF FLOWSHEET. nRow will be set as it will read only last row from table.
                                nRow = oResultTable.Rows.Count - nColumnCount
                            Else
                                nRow = 0
                            End If

                            Dim newRow As DataRow
                            While nRow < oResultTable.Rows.Count
                                newRow = dtTable.NewRow
                                For i As Int32 = 0 To dtTable.Columns.Count - 1
                                    newRow.Item(i) = oResultTable.Rows.Item(nRow)("sValue")
                                    nRow += 1
                                Next
                                dtTable.Rows.Add(newRow)
                            End While
                            strData = "FlowSheet"
                            ResultDataType = "6" ''For FlowSheet To Create Table


                        ElseIf InStr(strFields, "OBVitalTable") Then
                            Dim _dv As DataView = Nothing
                            If InStr(strFields, "OBVitalTable") Then
                                _dv = oResultTable.DefaultView
                                If Not IsNothing(dtTable) Then  ''slr free previous memory
                                    dtTable.Dispose()
                                End If
                                dtTable = Nothing
                                dtTable = _dv.ToTable()
                                strData = "OBVitals Table"
                                ResultDataType = "6"
                            End If
                            If Not IsNothing(_dv) Then
                                _dv.Dispose()
                            End If

                            _dv = Nothing


                        ElseIf InStr(strFields, "PastPregnancies") Then
                            Dim _dv As DataView = Nothing
                            If InStr(strFields, "PastPregnancies") Then
                                _dv = oResultTable.DefaultView
                                If Not IsNothing(dtTable) Then  ''slr free previous memory
                                    dtTable.Dispose()
                                End If
                                dtTable = Nothing
                                dtTable = _dv.ToTable()
                                strData = "PastPregnancies"
                                ResultDataType = "6"
                            End If
                            If Not IsNothing(_dv) Then
                                _dv.Dispose()
                            End If

                            _dv = Nothing

                            '07-Oct-15 Aniket: Todays Orders Liquid Links Implementation
                        ElseIf InStr(strFields, "Lab_Test_Mst") AndAlso strFields.Contains("Todays") = False Then
                            Dim _dv As DataView = Nothing
                            If InStr(strFields, "Lab_Test_Mst.OrderedLabTests") Then
                                _dv = oResultTable.DefaultView
                                If Not IsNothing(dtTable) Then  ''slr free previous memory
                                    dtTable.Dispose()
                                End If
                                dtTable = Nothing
                                dtTable = _dv.ToTable()
                                strData = "Ordered Lab Tests"
                                ResultDataType = "6"
                            Else
                                '_dv = oResultTable.DefaultView
                                ''_dv.Sort = "Test"
                                If Not IsNothing(dtTable) Then  ''slr free previous memory
                                    dtTable.Dispose()
                                End If
                                dtTable = Nothing
                                dtTable = oResultTable.Copy()
                                Dim _temp As String = ""

                                For iRow As Integer = 0 To dtTable.Rows.Count - 1
                                    If _temp = dtTable.Rows(iRow)("Test") Then
                                        dtTable.Rows(iRow)("Test") = ""
                                    Else
                                        _temp = dtTable.Rows(iRow)("Test")
                                    End If
                                Next
                                strData = "Lab"
                                ResultDataType = "6"
                            End If

                            If Not IsNothing(_dv) Then
                                _dv.Dispose()
                            End If

                            _dv = Nothing
                        ElseIf InStr(strFields, "Patient_CarePlan.sGoal") Then
                            Dim _dv As DataView = Nothing
                            If Not IsNothing(dtTable) Then  ''slr free previous memory
                                dtTable.Dispose()
                            End If
                            dtTable = Nothing
                            _dv = oResultTable.DefaultView
                            dtTable = _dv.ToTable()
                            Dim _temp As String = ""
                            strData = "Care Plan"
                            ResultDataType = "6"
                        ElseIf InStr(strFields, "PatientExamsDx.nExamID|Encounter Diagnosis") Then
                            Dim _dv As DataView = Nothing
                            _dv = oResultTable.DefaultView
                            If Not IsNothing(dtTable) Then  ''slr free previous memory
                                dtTable.Dispose()
                            End If
                            dtTable = Nothing
                            dtTable = _dv.ToTable()
                            Dim _temp As String = ""
                            strData = "Diagnosis Snomed"
                            ResultDataType = "6"
                        ElseIf strFields = "CV_CardiologyDevice.sDeviceType+space(1)+CV_CardiologyDevice.sProductName+space(1)+CV_CardiologyDevice.sDevicemanufacturer" Then
                            If Not IsNothing(dtTable) Then  ''slr free previous memory
                                dtTable.Dispose()
                            End If
                            dtTable = Nothing  ''slr free previous memory
                            dtTable = oResultTable.Copy()
                            strData = "ImplantDevice"
                            ResultDataType = "6"
                        ElseIf strFields.Contains("Im_Trn_Dtl.Vaccines") = True Then
                            If Not IsNothing(dtTable) Then  ''slr free previous memory
                                dtTable.Dispose()
                            End If
                            dtTable = Nothing
                            dtTable = Nothing   ''slr free previous memory
                            dtTable = oResultTable.Copy()
                            strData = "Vaccines"
                            ResultDataType = "6"

                        ElseIf (strFields.Contains("DAS.DAS28Form") = True) AndAlso (oResultTable.Columns.Contains("FlagOthers") = False) Then
                            If Not IsNothing(dtTable) Then  ''slr free previous memory
                                dtTable.Dispose()
                            End If
                            dtTable = Nothing
                            dtTable = Nothing  ''slr free previous memory
                            dtTable = oResultTable.Copy()
                            strData = "DAS"
                            ResultDataType = "6"
                        ElseIf (strFields.Contains("|Modifier") = True) Then
                            If Not IsNothing(dtTable) Then  ''slr free previous memory
                                dtTable.Dispose()
                            End If
                            dtTable = Nothing
                            dtTable = Nothing  ''slr free previous memory
                            dtTable = oResultTable.Copy()
                            strData = "Treatment"
                            ResultDataType = "6"
                        ElseIf InStr(strFields, "ExamICD9CPT_PM") Then
                            For i As Int32 = 0 To oResultTable.Rows.Count - 1
                                ' For j As Int32 = 0 To oResultTable.Columns.Count - 1
                                If IsDBNull(oResultTable.Rows(i).Item(0)) = False Then
                                    If strData = Nothing Then
                                        strData = oResultTable.Rows(i).Item(0)
                                        ResultDataType = oResultTable.Rows(i).Item(1)
                                    Else
                                        strData = strData & Chr(11) & oResultTable.Rows(i).Item(0)
                                    End If
                                End If
                                'Next
                            Next
                        ElseIf InStr(strFields, "ExamICD9CPT") Then

                            dvICD9CPT = New DataView(oResultTable)
                            'dtICD9CPT = New DataTable

                            Dim strICD9CPT(oResultTable.Columns.Count - 1) As String

                            For i As Integer = 0 To oResultTable.Columns.Count - 1
                                strICD9CPT.SetValue(oResultTable.Columns(i).ColumnName, i)
                            Next
                            dtICD9CPT = dvICD9CPT.ToTable(True, strICD9CPT)
                            dtICD9CPT.Columns.Remove("nLineNo")
                            dvICD9CPT.Dispose()
                            dvICD9CPT = Nothing

                            '08-May-13 Aniket: Resolving Memory Leaks
                            'dvICD9CPT = New DataView
                            dvICD9CPT = dtICD9CPT.DefaultView
                            'dvICD9CPT = New DataView(dtICD9CPT)
                            strICD9CPT = Nothing
                            Dim strICD9CPT1(dtICD9CPT.Columns.Count - 1) As String
                            For i As Integer = 0 To dtICD9CPT.Columns.Count - 1
                                strICD9CPT1.SetValue(dtICD9CPT.Columns(i).ColumnName, i)
                            Next
                            If Not IsNothing(dtICD9CPT) Then  ''slr free previous memory
                                dtICD9CPT.Dispose()
                            End If
                            dtICD9CPT = Nothing
                            dtICD9CPT = dvICD9CPT.ToTable(True, strICD9CPT1)

                            For i As Int32 = 0 To dtICD9CPT.Rows.Count - 1
                                ' For j As Int32 = 0 To dtICD9CPT.Columns.Count - 1
                                If IsDBNull(dtICD9CPT.Rows(i).Item(0)) = False AndAlso dtICD9CPT.Rows(i).Item(0).ToString().Trim() <> "" Then
                                    If strData = Nothing Then
                                        strData = dtICD9CPT.Rows(i).Item(0)
                                        ResultDataType = dtICD9CPT.Rows(i).Item(1)
                                    Else
                                        strData = strData & Chr(11) & dtICD9CPT.Rows(i).Item(0)
                                    End If
                                End If
                                'Next
                            Next

                        ElseIf InStr(strFields, "Vitals") AndAlso strFields.Contains("|") Then
                            Dim oDictionary As New clsDataDictionary
                            If Not IsNothing(dtTable) Then  ''slr free dttable
                                dtTable.Dispose()
                            End If
                            dtTable = Nothing
                            dtTable = oDictionary.ReplaceColumnNames(oResultTable, clsDataDictionary.enumDictionaryType.Vitals)
                            dtTable = RemoveBlankRows(dtTable, clsDataDictionary.enumDictionaryType.Vitals)

                            oDictionary = Nothing
                            strData = "FlowSheet"
                            ResultDataType = "6" ''For Vital To Create Table
                        Else
                            For i As Int32 = 0 To oResultTable.Rows.Count - 1
                                ' For j As Int32 = 0 To oResultTable.Columns.Count - 1
                                If IsDBNull(oResultTable.Rows(i).Item(0)) = False Then
                                    If strData = Nothing Then
                                        ''commented for Problem:00000186
                                        ''Start
                                        'strData = oResultTable.Rows(i).Item(0).ToString().Trim()
                                        ''End
                                        strData = oResultTable.Rows(i).Item(0)
                                        ResultDataType = oResultTable.Rows(i).Item(1)
                                    Else
                                        ''commented for Problem:00000186
                                        ''Start
                                        'strData = strData & Chr(11) & oResultTable.Rows(i).Item(0).Trim()
                                        ''End
                                        strData = strData & Chr(11) & oResultTable.Rows(i).Item(0)
                                    End If
                                Else
                                    '' Problem : 00000052
                                    '' Description : When there is no weight change, it appears that there is no value liquid linked into the office note.
                                    '' Reason for change : To display weight change as 0.00 if no weight change.

                                    If InStr(strFields, "dWeightChange") Then
                                        If strData = Nothing Then
                                            strData = "0.00"
                                            ResultDataType = oResultTable.Rows(i).Item(1)
                                        Else
                                            strData = strData & Chr(11) & "0.00"
                                        End If
                                    End If
                                End If
                                'Next
                            Next
                        End If
                    End If
                    ''// For Vitals if Field is of BloodPressure (Sitting or Standing(MIN/MAX))
                    ''// then take only Integer part of its Value 
                    'If InStr(strFields, "Vitals") Then
                    '    strData = Split(strData, ".00")(0)
                    'End If


                    If InStr(strFields, "Vitals") Then
                        'Bug #61727: 00000581 : WEIGHT IN KG LIQUID LINK
                        If strData.Contains(".") Then
                            strData = strData.TrimEnd("0").TrimEnd(".")
                        End If
                    End If

                    strDataCol = strData & "|" & ResultDataType.ToString
                    Return strDataCol
                End If
                Return ""
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Select, "Error while retrieving data for the DataField - '" & strHelpError & "' in the Template" & vbLf & ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                '' COMMENT BY SUDHIR 20090708 ''
                ''MessageBox.Show("Error while retrieving data for the DataField - '" & strHelpError & "' in the Template", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return ""

            Finally
                If Not IsNothing(dtICD9CPT) Then
                    dtICD9CPT.Dispose()
                    dtICD9CPT = Nothing
                End If

                If Not IsNothing(dvICD9CPT) Then
                    dvICD9CPT.Dispose()
                    dvICD9CPT = Nothing
                End If

                If Not IsNothing(oResultTable) Then
                    oResultTable.Dispose()
                    oResultTable = Nothing
                End If
                If Not IsNothing(oDB) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If
                '22-Mar-13 Aniket: Cannot dispose dtTable as it is a ByRef variable
            End Try

        End Function


        Public Sub Get_PatientDetails(ByVal _PatientID As Long)

            Dim dtPatient As DataTable = Nothing

            Try

                ''22-Mar-13 Aniket: Resolving Memory Leak Issues
                'dtPatient = New DataTable
                dtPatient = GetPatientInfo(_PatientID)
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            Finally
                If IsNothing(dtPatient) = False Then
                    dtPatient.Dispose()
                    dtPatient = Nothing
                End If
            End Try

        End Sub
        Public Function GetPrefixTransactionID(ByVal PatientID As Long) As Long
            Dim PatientDOB As DateTime
            Dim strID As String
            Dim dtDate As DateTime

            'Get Patient Date Of Birth
            Dim oDB As New gloStream.gloDataBase.gloDataBase
            oDB.Connect(GetConnectionString)
            PatientDOB = oDB.ExecuteQueryScaler("SELECT dtDOB FROM Patient WHERE nPatientID = " & PatientID & "")
            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing
            dtDate = System.DateTime.Now
            strID = System.Math.Abs(DateDiff(DateInterval.Day, CDate("1/1/1900"), dtDate.Date)) & System.Math.Abs(DateDiff(DateInterval.Second, dtDate.Date, dtDate)) & System.Math.Abs(DateDiff(DateInterval.Day, CDate("1/1/1900"), PatientDOB.Date))
            Return CLng(strID)

            ' Requires setting a reference to System.Management.dll
            ''
        End Function
        'SLR: Created on 3/28/2014
        Public Function ConvertToNewDocx(ByVal strFileName As String, ByVal mysFileName As String) As String

            Dim mywordApplication As New Microsoft.Office.Interop.Word.Application()
            If mywordApplication IsNot Nothing Then
                Dim myoFileFormat As Object = CType(Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatXMLDocument, Object)
                Dim mysaveoptions As Object = CType(Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges, Object)
                Try
                    Dim myDoc As Microsoft.Office.Interop.Word.Document = mywordApplication.Documents.Add(strFileName)
                    If myDoc IsNot Nothing Then
                        Dim bClosed As Boolean = False
                        Dim bSaved As Boolean = False
                        Try
                            myDoc.SaveAs(mysFileName, myoFileFormat)
                            bSaved = True
                            Try
                                myDoc.Close(mysaveoptions)
                                bClosed = True
                            Catch ex As Exception

                            End Try

                            Try
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(myDoc)
                            Catch ex As Exception

                            End Try
                        Catch ex As Exception
                            If (bSaved) Then
                                If (bClosed = False) Then
                                    Try
                                        System.Runtime.InteropServices.Marshal.ReleaseComObject(myDoc)
                                    Catch ex2 As Exception

                                    End Try
                                End If
                            Else

                                Try
                                    myDoc.Close(mysaveoptions)
                                    bClosed = True
                                Catch ex2 As Exception

                                End Try
                                Try
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(myDoc)
                                Catch ex2 As Exception

                                End Try
                                File.Copy(strFileName, mysFileName)
                            End If

                        End Try
                    Else
                        File.Copy(strFileName, mysFileName)
                    End If
                Catch ex As Exception

                End Try
                mywordApplication.Application.Quit(mysaveoptions)
                Try
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(mywordApplication)
                Catch ex As Exception

                End Try
            Else
                File.Copy(strFileName, mysFileName)
            End If
            Return mysFileName
        End Function
        ''Start' Enew Code
        'SLR: Modified on 3/28/2014
        Public Function GenerateFile(ByVal cntFromDB As Object, ByVal strFileName As String, Optional ByVal convertToNewFormat As Boolean = True) As String

            If Not cntFromDB Is Nothing Then
                If IsDBNull(cntFromDB) = False Then
                    If _myPhoto = True Then

                        Dim content() As Byte = CType(cntFromDB, Byte())
                        'Dim MyPictureBoxControl As New gloPictureBox.gloPictureBox
                        'MyPictureBoxControl.byteImage = content
                        'MyPictureBoxControl.copyFrame(True)

                        Dim PatientPhoto As Image = gloPictureBox.gloImage.GetImage(content, True)

                        PatientPhoto.Save(strFileName, System.Drawing.Imaging.ImageFormat.Bmp)
                        PatientPhoto.Dispose()
                        PatientPhoto = Nothing


                        ''22-Mar-13 Aniket: Resolving Memory Leak Issues
                        'content.Clear(content, 1, content.Length - 1)

                        '09-May-13 Aniket: Do not call the content.clear method as it damages the word file Bug #50357 
                        'Array.Resize(content, 0)
                        'content = Nothing

                        'MyPictureBoxControl.Dispose()
                        'MyPictureBoxControl = Nothing

                        Return strFileName

                    Else
                        Dim content() As Byte = CType(cntFromDB, Byte())
                        Dim contentLength As Integer = content.Length
                        'Dim stream As MemoryStream = New MemoryStream(content)
                        'If stream Is Nothing Then
                        '    Return Nothing
                        'End If
                        'SLR: Added the following code on 3/28/2014
                        Dim header As String = ""
                        Dim oldFileName As String = strFileName
                        Dim tobeConverted As Boolean = False
                        If (convertToNewFormat) Then
                            If (contentLength > 5) Then
                                header = Conversion.Hex(content(0)) & Conversion.Hex(content(1)) & Conversion.Hex(content(2)) & Conversion.Hex(content(3)) & Conversion.Hex(content(4)) & Conversion.Hex(content(5))
                                If (header.ToLower() = "7b5c72746631") Then
                                    oldFileName = strFileName & ".rtf"
                                    tobeConverted = True
                                Else
                                    If (contentLength > 7) Then
                                        header = header & Conversion.Hex(content(6)) & Conversion.Hex(content(7))
                                        If (header.ToLower() = "d0cf11e0a1b11ae1") Then
                                            oldFileName = strFileName & ".doc"
                                            tobeConverted = True
                                        End If
                                    End If
                                End If
                            End If
                            'If (contentLength > 7) Then
                            '    header = Conversion.Hex(content(0)) & Conversion.Hex(content(1)) & Conversion.Hex(content(2)) & Conversion.Hex(content(3)) & Conversion.Hex(content(4)) & Conversion.Hex(content(5)) & Conversion.Hex(content(6)) & Conversion.Hex(content(7))
                            'End If
                            'If (header.ToLower() = "d0cf11e0a1b11ae1") Then
                            '    oldFileName = strFileName + ".doc"
                            '    tobeConverted = True
                            'End If
                        End If
                        'SLR: End the above code on 3/28/2014
                        Dim oFile As New System.IO.FileStream(oldFileName, System.IO.FileMode.Create)
                        If oFile Is Nothing Then
                            'If Not IsNothing(stream) Then
                            '    stream.Dispose()
                            '    stream = Nothing
                            'End If
                            Return Nothing
                        End If
                        'SLR: Stream is not needed 12/22
                        'stream.WriteTo(oFile)
                        oFile.Write(content, 0, contentLength)


                        ''22-Mar-13 Aniket: Resolving Memory Leak Issues
                        'content.Clear(content, 1, content.Length - 1)

                        '09-May-13 Aniket: Do not call the content.clear method as it damages the word file Bug #50357 
                        'Array.Resize(content, 0)
                        'content = Nothing 'Change made to solve memory Leak and word crash issue


                        oFile.Flush()
                        oFile.Close()
                        ''Changed
                        If Not IsNothing(oFile) Then
                            oFile.Dispose()
                            oFile = Nothing
                        End If
                        'If Not IsNothing(stream) Then
                        '    stream.Dispose()
                        '    stream = Nothing
                        'End If
                        'SLR : Added the following code.
                        If (convertToNewFormat AndAlso tobeConverted) Then

                            ConvertToNewDocx(oldFileName, strFileName)

                        End If
                        Return strFileName
                    End If
                Else
                    Return Nothing
                End If

            Else
                Return Nothing

            End If
        End Function
        ''End' Old Code

        Private Sub GetLastLine(ByVal strFileName As String)

            Dim oRead As StreamReader
            Dim LineIn As String
            Dim strNewString As New ArrayList
            'Dim j As Integer

            oRead = File.OpenText(strFileName)

            While oRead.Peek <> -1
                LineIn = oRead.ReadLine()
                strNewString.Add(LineIn)
            End While

            ''22-Mar-13 Aniket: Resolving Memory Leak Issues
            oRead.Close()
            oRead.Dispose()
            oRead = Nothing 'Change made to solve memory Leak and word crash issue

            Dim oWrite As StreamWriter
            oWrite = File.CreateText(strFileName)
            oWrite.WriteLine(strNewString.Item(0))

            If strNewString.Count > 1 Then
                Dim nLoop As Int16
                For nLoop = strNewString.Count - 1 To 0 Step -1
                    If Trim(strNewString.Item(nLoop)) <> "" Then
                        Dim Tempcheck As String() = Split(Trim(strNewString.Item(nLoop)), vbTab)
                        If (Tempcheck.Length > 2) Then
                            If Trim(Tempcheck(1)) <> "" OrElse Trim(Tempcheck(2)) <> "" Then
                                Exit For
                            End If
                        End If

                    End If
                Next
                oWrite.WriteLine(strNewString.Item(nLoop))
            End If

            oWrite.Close()
            oWrite.Dispose()
            oWrite = Nothing 'Change made to solve memory Leak and word crash issue
            strNewString.Clear()  ''slr free strnewstring
            strNewString = Nothing
        End Sub

        '''' <summary>
        '''' To get the Table name for filtering the form fields
        '''' </summary>
        '''' <param name="enumDocumentType"></param>
        '''' <returns></returns>
        '''' <remarks></remarks>
        Private Function GetHelpText(ByVal enumDocumentType As enumDocType) As String
            Select Case enumDocumentType
                Case enumDocType.CheifComplaints
                    Return _CheifComplaints
                Case enumDocType.Diagnosis
                    Return _Diagnosis
                    '28-Dec-15 Resolving Bug #92200: gloEMR: (Liquid link) CPT with charges: CPT with charges liquid link is not working properly
                Case enumDocType.ExamICD9CPT_PM
                    Return _ExamICD9CPT_PM
                Case enumDocType.Flowsheet
                    Return _Flowsheet
                Case enumDocType.History
                    Return _History
                Case enumDocType.Medication
                    Return _Medication
                Case enumDocType.None
                    Return _None
                Case enumDocType.RadiologyOrders
                    Return _RadiologyOrder
                Case enumDocType.Others
                    Return _Others
                Case enumDocType.PatientDemographics
                    Return _PatientDemographics
                    '21-May-15 Aniket: Open Modify Patient on Patient Insurance Liquid Link
                Case enumDocType.PatientInsurance
                    Return _PatientInsurance_DTL
                Case enumDocType.PatientAlerts
                    Return _PatientAlerts
                Case enumDocType.PatientEducation
                    Return _PatientEducation
                Case enumDocType.PatientGuideline
                    Return _PatientGuideline
                Case gloEMRWord.enumDocType.Prescription
                    Return _Prescription
                Case enumDocType.ProblemList
                    Return _ProblemList
                Case enumDocType.Referrals
                    Return _Referrals
                Case enumDocType.ROS
                    Return _ROS
                Case enumDocType.SmartDiagnosis
                    Return _SmartDiagnosis
                Case enumDocType.SmartTreatment
                    Return _SmartTreatment
                Case enumDocType.Tasks
                    Return _Tasks
                Case enumDocType.Treatment
                    Return _Treatment
                Case enumDocType.Vitals
                    Return _Vitals
                Case enumDocType.Contacts
                    Return _Contacts
                Case enumDocType.Narration
                    Return _Narration
                Case enumDocType.LabOrders
                    Return _LabOrder
                Case enumDocType.Clinic
                    Return _Clinic
                Case enumDocType.Fax
                    Return _Fax
                Case enumDocType.PatientExam
                    Return _PatientExam
                Case enumDocType.Providers
                    Return _ProviderSign
                Case enumDocType.DisclosureSet
                    Return _DisclosureSet
                Case enumDocType.PatientExamDos
                    Return _PateintExamDos
                Case enumDocType.PatientExamsDx
                    Return _PatientExamsDx
                    'case added by dipak 20091125 to add patient details as enumDocType.PatientDetails enum value added.
                Case enumDocType.PatientDetails
                    Return _Patient_DTL
                Case enumDocType.PatientDetails
                    Return _Patient_DTL
                Case enumDocType.Catheterization
                    Return _Catheterization
                Case enumDocType.StressTest
                    Return _stresstest
                Case enumDocType.ElectroPhysiology
                    Return _ElectroPhysiology
                Case enumDocType.CardiologyDevice
                    Return _CardiologyDevice
                Case enumDocType.ElectroCardioGrams
                    Return _ElectroCardioGrams
                Case enumDocType.Echocardiogram
                    Return _Echocardiogram
                Case enumDocType.DAS
                    Return _DAS

                    '05-May-15 Aniket: Implementation of Past Pregnancies Liquid Link
                Case enumDocType.PastPregancies
                    Return _PastPregnancies

                Case enumDocType.OBGeneticHistory
                    Return _OBGeneticHistory

                Case enumDocType.OBInfectionHistory
                    Return _OBInfectionHistory

                Case enumDocType.OBInitialPhysicalExamination
                    Return _OBInitialPhysicalExamination

                Case enumDocType.OBMedicalHistory
                    Return _OBMedicalHistory

                Case enumDocType.OBPlan
                    Return _OBPlan

                    'Resolve Bug #91611: 00001034: Patient Exam DOS liquid link 
                Case enumDocType.OBVitals
                    Return _OBVitals

                Case enumDocType.Contacts_Detail ''added  for incident 00065814  PER s hospital affiliation
                    Return _Contacts_Detail
                Case Else
                    Return _None
            End Select

            Return Nothing

        End Function

        Private Function GetSPNames(ByVal enumDocumentType As enumDocType) As String

            Select Case enumDocumentType

                Case enumDocType.CheifComplaints
                    Return _CheifComplaints
                Case enumDocType.Diagnosis
                    Return _Diagnosis
                    '28-Dec-15 Resolving Bug #92200: gloEMR: (Liquid link) CPT with charges: CPT with charges liquid link is not working properly
                Case enumDocType.ExamICD9CPT_PM
                    Return _ExamICD9CPT_PM
                Case enumDocType.Flowsheet
                    Return _Flowsheet
                Case enumDocType.History
                    Return _History
                Case enumDocType.Medication
                    Return _Medication
                Case enumDocType.None
                    Return _None
                Case enumDocType.RadiologyOrders
                    Return _RadiologyOrder
                Case enumDocType.Others
                    Return _Others
                Case enumDocType.PatientDemographics
                    Return _PatientDemographics
                Case enumDocType.PatientEducation
                    Return _PatientEducation
                Case enumDocType.PatientGuideline
                    Return _PatientGuideline
                Case gloEMRWord.enumDocType.Prescription
                    Return _Prescription
                Case enumDocType.ProblemList
                    Return _ProblemList
                Case enumDocType.Referrals
                    Return "gsp_ViewReferrals"
                Case enumDocType.ROS
                    Return _ROS
                Case enumDocType.SmartDiagnosis
                    Return _SmartDiagnosis
                Case enumDocType.SmartTreatment
                    Return _SmartTreatment
                Case enumDocType.Tasks
                    Return _Tasks
                Case enumDocType.Treatment
                    Return _Treatment
                Case enumDocType.Vitals
                    Return _Vitals
                Case enumDocType.Contacts
                    Return _Contacts
                Case enumDocType.Narration
                    Return _Narration
            End Select

            Return Nothing

        End Function

        ''Sudhir 20090225 '' TO GENERATE TABLE IN WORD CONTROL AT FORMFIELD
        Private Sub InsertWordTable(ByVal dtTable As DataTable, ByVal oField As Wd.FormField)
            Dim objDefaultBehaviorWord8 As Object = Wd.WdDefaultTableBehavior.wdWord8TableBehavior
            Dim objAutoFitFixed As Object = Wd.WdAutoFitBehavior.wdAutoFitFixed
            Try
                If Not IsNothing(dtTable) Then
                    If dtTable.Rows.Count > 0 Then
                        With _oCurDoc.ActiveWindow.Selection
                            Dim cntcontrol As Wd.ContentControl = .Range.ParentContentControl
                            If Not IsNothing(cntcontrol) Then
                                .Move(Unit:=Wd.WdUnits.wdSection, Count:=1)
                            End If

                            ''''Create Basic Table
                            Dim nrRows As Integer = 1
                            Dim nrCols As Integer = dtTable.Columns.Count

                            .Select()
                            '_oCurDoc.ActiveWindow.Selection.Move(Unit:=Wd.WdUnits.wdWord, Count:=1)
                            Dim wdRng As Wd.Range = .Range

                            If (IsNothing(wdRng) = False) Then
                                Dim myStart As Integer = wdRng.Start
                                Dim thisStart As Integer = myStart
                                While wdRng.Tables.Count > 0
                                    .MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=1)
                                    wdRng = .Range
                                    If (IsNothing(wdRng) = False) Then
                                        thisStart = wdRng.Start
                                        If (thisStart = myStart) Then
                                            Exit While
                                        End If
                                        myStart = thisStart
                                    Else
                                        Exit While

                                    End If

                                End While
                            End If
                            If (IsNothing(wdRng) = False) Then
                                .Select()
                                wdRng = .Range
                            End If

                            'Developer:Yatin N. Bhagat
                            'Date:12/13/2011
                            'Bug ID/PRD Name/Salesforce Case:Liquid Link For Ob Vital Table
                            'Reason: To Remove Extra Columns From Table for Ob Vitals table Liquid Link
                            Dim tb1 As Wd.Table = Nothing
                            ''Bug #63475: 00000435 : To improve performance of Lab result liquid links
                            If (oField.StatusText.Trim.Contains("Lab_Test_Mst") = False) Then
                                If (oField.StatusText.Trim.Contains("OBVitals") = False) Then
                                    tb1 = wdRng.Tables.Add(wdRng, nrRows, nrCols, objDefaultBehaviorWord8, objAutoFitFixed)
                                Else
                                    tb1 = wdRng.Tables.Add(wdRng, nrRows, nrCols - 4, objDefaultBehaviorWord8, objAutoFitFixed)
                                End If
                            End If

                            'Dim tb1 As Wd.Table = oField.Range.Tables.Add(oField.Range, nrRows, nrCols, objDefaultBehaviorWord8, objAutoFitFixed)
                            If (oField.StatusText.Trim() = "DAS.DAS28Form") Then
                                If (IsNothing(tb1)) Then
                                    tb1 = wdRng.Tables.Add(wdRng, nrRows, nrCols, objDefaultBehaviorWord8, objAutoFitFixed)
                                End If
                                If PopulateAndExtendDASTable(tb1, dtTable) Then
                                    'wdRng = CreateSpaceAfterTable(tb1)
                                    Dim style As Wd.Style = CreateWordTableStyle()
                                    FormatWordTables(style, tb1)
                                    style = Nothing
                                End If
                            ElseIf (oField.StatusText.Trim.Contains("Lab_Test_Mst") = True) Then
                                ''Bug #63475: 00000435 : To improve performance of Lab result liquid links
                                If PopulateAndExtendFlowSheetTable(wdRng, dtTable) Then
                                    ''wdRng = CreateSpaceAfterTable(tb1)
                                    'Dim style As Wd.Style = CreateWordTableStyleForLabTables()
                                    'FormatWordTables(style, tb1)
                                    'style = Nothing
                                End If

                                'Developer:Yatin N. Bhagat
                                'Date:12/13/2011
                                'Bug ID/PRD Name/Salesforce Case:Liquid Link For Ob Vital Table
                                'Reason: To Add Table for Ob Vital Liquid Link
                            ElseIf (oField.StatusText.Trim.Contains("OBVitals") = True) Then
                                If PopulateAndExtendObVitalTable(tb1, dtTable) Then
                                    'wdRng = CreateSpaceAfterTable(tb1)
                                    Dim style As Wd.Style = CreateWordTableStyleForLabTables()
                                    FormatWordTables(style, tb1)
                                    style = Nothing
                                End If
                            ElseIf (oField.StatusText.Trim.Contains("|Modifier") = True) Then
                                If PopulateAndExtendTreatmentTable(tb1, dtTable, oField.StatusText) Then
                                    Dim style As Wd.Style = CreateWordTableStyle()
                                    FormatWordTables(style, tb1)
                                    style = Nothing
                                End If
                            ElseIf (oField.StatusText.Trim.Contains("PastPregnancies") = True) Then
                                If PopulateAndExtendPastPregnanciesTable(tb1, dtTable) Then
                                    Dim style As Wd.Style = CreateWordTableStyleOB()
                                    FormatWordTables(style, tb1)
                                    style = Nothing
                                End If
                            Else
                                If PopulateAndExtendFlowSheetTable(tb1, dtTable) Then
                                    'wdRng = CreateSpaceAfterTable(tb1)
                                    Dim style As Wd.Style = CreateWordTableStyle()
                                    FormatWordTables(style, tb1)
                                    style = Nothing
                                End If
                            End If

                            ''8-Apr-13 :: Yatin :: Resolving Memory Leak Issues
                            cntcontrol = Nothing
                            wdRng = Nothing
                            tb1 = Nothing

                        End With

                        _oCurDoc.ActiveWindow.SetFocus()

                    End If
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                _oCurDoc.ActiveWindow.Selection.InsertParagraph()
                _oCurDoc.ActiveWindow.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
            End Try
        End Sub
        Private Sub InsertWordTableForLabResult(ByVal dtTable As DataTable, ByVal oField As Wd.FormField)
            Dim objDefaultBehaviorWord8 As Object = Wd.WdDefaultTableBehavior.wdWord8TableBehavior
            Dim objAutoFitFixed As Object = Wd.WdAutoFitBehavior.wdAutoFitFixed

            Try
                If Not IsNothing(dtTable) Then
                    If dtTable.Rows.Count > 0 Then
                        With _oCurDoc.ActiveWindow.Selection
                            Dim cntcontrol As Wd.ContentControl = .Range.ParentContentControl
                            If Not IsNothing(cntcontrol) Then
                                .Move(Unit:=Wd.WdUnits.wdSection, Count:=1)
                            End If

                            ''''Create Basic Table
                            Dim nrRows As Integer = 1
                            Dim nrCols As Integer = dtTable.Columns.Count - 2
                            .Select()
                            Dim wdRng As Wd.Range = .Range

                            If (IsNothing(wdRng) = False) Then
                                Dim myStart As Integer = wdRng.Start
                                Dim thisStart As Integer = myStart
                                While wdRng.Tables.Count > 0
                                    .MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=1)
                                    wdRng = .Range
                                    If (IsNothing(wdRng) = False) Then
                                        thisStart = wdRng.Start
                                        If (thisStart = myStart) Then
                                            Exit While
                                        End If
                                        myStart = thisStart
                                    Else
                                        Exit While

                                    End If

                                End While
                            End If
                            If (IsNothing(wdRng) = False) Then
                                .Select()
                                wdRng = .Range
                            End If

                            If oField.StatusText = "Lab_Test_Mst.LabResults|AllLabResultsSinceLastAppointmentDate" Or
                              oField.StatusText = "Lab_Test_Mst.LabResults|AllLabResultsSinceLastNurseNoteDate" Or
                              oField.StatusText = "Lab_Test_Mst.LabResults|AllLabResultsSinceLastExamDate" Then
                                PopulateAndExtendOrderResultTable(wdRng, dtTable)
                            Else
                                ''Bug #63475: 00000435 : To improve performance of Lab result liquid links
                                'Dim tb1 As Wd.Table = wdRng.Tables.Add(wdRng, nrRows, nrCols, objDefaultBehaviorWord8, objAutoFitFixed)
                                'Dim tb1 As Wd.Table = oField.Range.Tables.Add(oField.Range, nrRows, nrCols, objDefaultBehaviorWord8, objAutoFitFixed)
                                ''Bug #63475: 00000435 : To improve performance of Lab result liquid links
                                If PopulateAndExtendLabResultTable(wdRng, dtTable) Then
                                    ''wdRng = CreateSpaceAfterTable(tb1)
                                    'Dim style As Wd.Style = CreateWordTableStyleForLabTables()
                                    'FormatWordTables(style, tb1)
                                    'style = Nothing
                                End If
                            End If

                            ' ''Bug #63475: 00000435 : To improve performance of Lab result liquid links
                            ''Dim tb1 As Wd.Table = wdRng.Tables.Add(wdRng, nrRows, nrCols, objDefaultBehaviorWord8, objAutoFitFixed)
                            ''Dim tb1 As Wd.Table = oField.Range.Tables.Add(oField.Range, nrRows, nrCols, objDefaultBehaviorWord8, objAutoFitFixed)
                            ' ''Bug #63475: 00000435 : To improve performance of Lab result liquid links
                            'If PopulateAndExtendLabResultTable(wdRng, dtTable) Then
                            '    ''wdRng = CreateSpaceAfterTable(tb1)
                            '    'Dim style As Wd.Style = CreateWordTableStyleForLabTables()
                            '    'FormatWordTables(style, tb1)
                            '    'style = Nothing
                            'End If

                            ''8-Apr-13 :: Yatin :: Resolving Memory Leak Issues
                            cntcontrol = Nothing
                            wdRng = Nothing
                            ''Bug #63475: 00000435 : To improve performance of Lab result liquid links
                            'tb1 = Nothing

                        End With
                        _oCurDoc.ActiveWindow.SetFocus()
                    End If
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                _oCurDoc.ActiveWindow.Selection.InsertParagraph()
                _oCurDoc.ActiveWindow.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
            End Try
        End Sub

        ''Bug #63475: 00000435 : To improve performance of Lab result liquid links
        ''Added new function to populate Lab result liquid link
        Function BuildDataStringForLabResult(ByVal aData As DataTable) As String
            If (IsNothing(aData)) Then
                Return ""
            End If
            Dim dataString As String = ""
            Dim nrRow As Int32, nrCol As Int32

            For nrCol = 0 To aData.Columns.Count - 3
                If (aData.Columns(nrCol).Caption.ToString.Trim = "Flag") Then
                    dataString = dataString & aData.Columns(nrCol).Caption & "*"
                Else
                    dataString = dataString & aData.Columns(nrCol).Caption
                End If
                If nrCol < aData.Columns.Count - 3 Then
                    dataString = dataString & vbTab
                Else
                    dataString = dataString & vbCr
                End If
            Next

            For nrRow = 0 To aData.Rows.Count - 1
                For nrCol = 0 To aData.Columns.Count - 3
                    dataString = dataString & aData.Rows(nrRow)(nrCol).ToString().Replace(vbTab, " ").Replace(vbCr, " ").Replace(vbLf, " ").Trim
                    If nrCol < aData.Columns.Count - 3 _
                        Then dataString = dataString & vbTab
                Next nrCol
                dataString = dataString & vbCr

                If (aData.Rows(nrRow)("labotrd_ResultComment").Trim() <> "") Then
                    dataString = dataString & "" & vbTab
                    dataString = dataString & aData.Rows(nrRow)("labotrd_ResultComment").ToString.Trim.Replace(vbTab, " ").Replace(vbCr, " ").Replace(vbLf, " ").Trim & vbTab
                    For nrCol = 2 To aData.Columns.Count - 3
                        dataString = dataString & ""
                        If nrCol < aData.Columns.Count - 3 _
                       Then dataString = dataString & vbTab
                    Next
                    dataString = dataString & vbCr
                End If
            Next nrRow
            BuildDataStringForLabResult = dataString
        End Function

        Function BuildDataStringForOrderResult(ByVal aData As DataTable) As String
            If (IsNothing(aData)) Then
                Return ""
            End If

            Dim dataString As String = ""
            Dim nrRow As Int32, nrCol As Int32

            For nrCol = 0 To aData.Columns.Count - 1
                If (aData.Columns(nrCol).Caption.ToString.Trim = "Flag") Then
                    dataString = dataString & aData.Columns(nrCol).Caption & "*"
                Else
                    dataString = dataString & aData.Columns(nrCol).Caption
                End If
                If nrCol < aData.Columns.Count - 1 Then
                    dataString = dataString & vbTab
                Else
                    dataString = dataString & vbCr
                End If
            Next

            For nrRow = 0 To aData.Rows.Count - 1
                For nrCol = 0 To aData.Columns.Count - 1
                    dataString = dataString & aData.Rows(nrRow)(nrCol).ToString().Replace(vbTab, " ").Replace(vbCr, " ").Replace(vbLf, " ").Trim
                    If nrCol < aData.Columns.Count - 3 _
                        Then dataString = dataString & vbTab
                Next nrCol
                dataString = dataString & vbCr

                'If (aData.Rows(nrRow)("labotrd_ResultComment").Trim() <> "") Then
                '    dataString = dataString & "" & vbTab
                '    dataString = dataString & aData.Rows(nrRow)("labotrd_ResultComment").ToString.Trim.Replace(vbTab, " ").Replace(vbCr, " ").Replace(vbLf, " ").Trim & vbTab
                '    For nrCol = 2 To aData.Columns.Count - 3
                '        dataString = dataString & ""
                '        If nrCol < aData.Columns.Count - 3 _
                '       Then dataString = dataString & vbTab
                '    Next
                '    dataString = dataString & vbCr
                'End If
            Next nrRow
            BuildDataStringForOrderResult = dataString
        End Function

        ''Bug #63475: 00000435 : To improve performance of Lab result liquid links
        ''Added new function to populate Lab result liquid link
        Private Function PopulateAndExtendLabResultTable(ByVal rng As Wd.Range, ByVal dtFlowSheet As DataTable) As Boolean
            If (IsNothing(dtFlowSheet)) Then
                Return False
            End If

            Dim objMissing As Object
            Dim tbl As Wd.Table
            Try
                objMissing = System.Reflection.Missing.Value

                Dim dataString As String
                Dim rowCnt As Int32
                Dim strFlag As String = "* Flag Legend :"

                rng.Text = BuildDataStringForLabResult(dtFlowSheet)

                tbl = rng.ConvertToTable(vbTab, AutoFitBehavior:=Wd.WdAutoFitBehavior.wdAutoFitFixed, DefaultTableBehavior:=Wd.WdDefaultTableBehavior.wdWord8TableBehavior)
                rowCnt = 1
                Dim minimumRow As Integer = dtFlowSheet.Rows.Count
                Dim minimumCol As Integer = Math.Min(tbl.Columns.Count, dtFlowSheet.Columns.Count - 2)
                For s As Int32 = 0 To minimumRow - 1
                    rowCnt = rowCnt + 1
                    If (rowCnt <= tbl.Rows.Count) Then


                        For j As Int32 = 0 To minimumCol - 1
                            dataString = dtFlowSheet.Rows(s)(j).ToString().Trim
                            If String.Compare(dtFlowSheet.Columns(j).ColumnName, "value", False) AndAlso gloGlobal.gloLabGenral.IsResultisHyperLink(dataString) Then
                                tbl.Cell(rowCnt, j + 1).Range.Text = ""
                                tbl.Cell(rowCnt, j + 1).Range.Hyperlinks.Add(tbl.Cell(rowCnt, j + 1).Range, If(dataString.StartsWith("www."), "http://" & dataString, dataString), Nothing, Nothing, dataString, Nothing)
                            End If
                        Next
                        If (dtFlowSheet.Rows(s)("labotrd_ResultComment").Trim() <> "") AndAlso (minimumCol >= 2) Then
                            rowCnt = rowCnt + 1
                            tbl.Rows(rowCnt).Cells(2).Merge(tbl.Rows(rowCnt).Cells(tbl.Rows(rowCnt).Cells.Count))
                            If (tbl.Columns.Count >= 2) Then


                                tbl.Cell(rowCnt, 2).Range.Text = ""
                                LabResultComments(tbl.Cell(rowCnt, 2).Range, dtFlowSheet.Rows(s)("labotrd_ResultComment"))
                            End If

                        End If
                    End If

                    If (strFlag.Contains(dtFlowSheet.Rows(s)("Flag1").ToString) = False) Then
                        strFlag = strFlag & Chr(11) & dtFlowSheet.Rows(s)("Flag1").ToString
                    End If
                Next
                If (strFlag.Trim <> "* Flag Legend :") Then
                    tbl.Rows.Add(objMissing)  '''' new Row
                    rowCnt = tbl.Rows.Count
                    If (minimumCol > 0) Then
                        tbl.Rows(rowCnt).Cells(1).Merge(tbl.Rows(rowCnt).Cells(tbl.Rows(rowCnt).Cells.Count))
                        tbl.Cell(rowCnt, 1).Range.Text = strFlag
                    End If

                End If

                Dim style As Wd.Style = CreateWordTableStyleForLabTables()
                FormatWordTables(style, tbl)
                style = Nothing

                _oCurDoc.ActiveWindow.Selection.EndOf(Unit:=Wd.WdUnits.wdTable)
                _oCurDoc.ActiveWindow.Selection.MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=2)
                _oCurDoc.ActiveWindow.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
                '_oCurDoc.ActiveWindow.Selection.InsertParagraph()
                _oCurDoc.ActiveWindow.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
                _oCurDoc.ActiveWindow.Selection.GoToNext(Microsoft.Office.Interop.Word.WdGoToItem.wdGoToLine)
                Return True
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                If _oCurDoc.ActiveWindow.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText Then
                    _oCurDoc.ActiveWindow.Selection.Range.ParentContentControl.Delete(True)
                End If
                Return False

            Finally
                '08-May-13 Aniket: Resolving Memory Leaks
                objMissing = Nothing
                tbl = Nothing
            End Try

        End Function
        Private Function PopulateAndExtendOrderResultTable(ByVal rng As Wd.Range, ByVal dtFlowSheet As DataTable) As Boolean
            If (IsNothing(dtFlowSheet)) Then
                Return False
            End If
            Dim objMissing As Object
            Dim tbl As Wd.Table
            Try
                objMissing = System.Reflection.Missing.Value

                Dim dataString As String
                Dim rowCnt As Int32
                Dim strFlag As String = "* Flag Legend :"

                rng.Text = BuildDataStringForOrderResult(dtFlowSheet)

                tbl = rng.ConvertToTable(vbTab, AutoFitBehavior:=Wd.WdAutoFitBehavior.wdAutoFitFixed, DefaultTableBehavior:=Wd.WdDefaultTableBehavior.wdWord8TableBehavior)
                rowCnt = 1
                Dim minimumRow As Integer = Math.Min(tbl.Rows.Count, dtFlowSheet.Rows.Count)
                Dim minimumCol As Integer = Math.Min(tbl.Columns.Count, dtFlowSheet.Columns.Count - 2)
                For s As Int32 = 0 To minimumRow - 1
                    rowCnt = rowCnt + 1
                    If (rowCnt <= tbl.Rows.Count) Then
                        For j As Int32 = 0 To minimumCol - 1
                            dataString = dtFlowSheet.Rows(s)(j).ToString().Trim
                            If String.Compare(dtFlowSheet.Columns(j).ColumnName, "value", False) AndAlso gloGlobal.gloLabGenral.IsResultisHyperLink(dataString) Then
                                tbl.Cell(rowCnt, j + 1).Range.Text = ""
                                tbl.Cell(rowCnt, j + 1).Range.Hyperlinks.Add(tbl.Cell(rowCnt, j + 1).Range, If(dataString.StartsWith("www."), "http://" & dataString, dataString), Nothing, Nothing, dataString, Nothing)
                            End If
                        Next
                    End If
                Next
                Dim style As Wd.Style = CreateWordTableStyleForLabTables()
                FormatWordTables(style, tbl)
                style = Nothing

                _oCurDoc.ActiveWindow.Selection.EndOf(Unit:=Wd.WdUnits.wdTable)
                _oCurDoc.ActiveWindow.Selection.MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=2)
                _oCurDoc.ActiveWindow.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
                _oCurDoc.ActiveWindow.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
                _oCurDoc.ActiveWindow.Selection.GoToNext(Microsoft.Office.Interop.Word.WdGoToItem.wdGoToLine)
                Return True
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                If _oCurDoc.ActiveWindow.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText Then
                    _oCurDoc.ActiveWindow.Selection.Range.ParentContentControl.Delete(True)
                End If
                Return False

            Finally

                '08-May-13 Aniket: Resolving Memory Leaks
                objMissing = Nothing
                tbl = Nothing
            End Try

        End Function

        'Funcation Added by manoj jadhav on 20121228 for showing hyperlinks in Lab Result comments
        Private Function LabResultComments(ByRef tRange As Microsoft.Office.Interop.Word.Range, ByVal ResultComment As String) As Boolean
            Dim IOfWww As Integer = 0
            Dim IOfHttp As Integer = 0
            Dim IOfHttps As Integer = 0
            Dim IOfHttp1 As Integer = 0
            Dim IOfHttps1 As Integer = 0
            Dim iIndex As Integer = 0
            Dim EIndex As Integer = 0
            Dim bflag As Boolean = False
            Try
                If String.IsNullOrEmpty(ResultComment) Then
                    Exit Try
                End If

                While (Len(ResultComment) > 0)
                    iIndex = -1
                    EIndex = -1
                    IOfWww = ResultComment.ToLower().IndexOf("www.")
                    IOfHttp = ResultComment.ToLower().IndexOf("http://")
                    IOfHttps = ResultComment.ToLower().IndexOf("https://")
                    IOfHttp1 = ResultComment.ToLower().IndexOf("http:\\")
                    IOfHttps1 = ResultComment.ToLower().IndexOf("https:\\")
                    If IOfWww > -1 Then
                        iIndex = IOfWww
                    ElseIf IOfHttp > -1 Then
                        iIndex = IOfHttp
                    ElseIf IOfHttps > -1 Then
                        iIndex = IOfHttps
                    ElseIf IOfHttp1 > -1 Then
                        iIndex = IOfHttp1
                    ElseIf IOfHttps1 > -1 Then
                        iIndex = IOfHttps1
                    End If
                    If (IOfWww > -1) AndAlso ((IOfHttp > -1 AndAlso IOfHttp >= IOfWww) OrElse (IOfHttps > -1 AndAlso IOfHttps >= IOfWww) OrElse _
                                (IOfHttp1 > -1 AndAlso IOfHttp1 >= IOfWww) OrElse (IOfHttps1 > -1 AndAlso IOfHttps1 >= IOfWww)) Then
                        iIndex = IOfWww
                    ElseIf (IOfHttp > -1) AndAlso ((IOfWww > -1 AndAlso IOfWww >= IOfHttp) OrElse (IOfHttps > -1 AndAlso IOfHttps >= IOfHttp) OrElse _
                           (IOfHttp1 > -1 AndAlso IOfHttp1 >= IOfHttp) OrElse (IOfHttps1 > -1 AndAlso IOfHttps1 >= IOfHttp)) Then
                        iIndex = IOfHttp
                    ElseIf (IOfHttps > -1) AndAlso ((IOfWww > -1 AndAlso IOfWww >= IOfHttps) OrElse (IOfHttp > -1 AndAlso IOfHttp >= IOfHttps) OrElse _
                       (IOfHttp1 > -1 AndAlso IOfHttp1 >= IOfHttps) OrElse (IOfHttps1 > -1 AndAlso IOfHttps1 >= IOfHttps)) Then
                        iIndex = IOfHttps
                    ElseIf (IOfHttp1 > -1) AndAlso ((IOfWww > -1 AndAlso IOfWww >= IOfHttp1) OrElse (IOfHttp > -1 AndAlso IOfHttp >= IOfHttp1) OrElse _
                          (IOfHttps > -1 AndAlso IOfHttps >= IOfHttp1) OrElse (IOfHttps1 > -1 AndAlso IOfHttps1 >= IOfHttp1)) Then
                        iIndex = IOfHttp1
                    ElseIf (IOfHttps1 > -1) AndAlso ((IOfWww > -1 AndAlso IOfWww >= IOfHttps1) OrElse (IOfHttp > -1 AndAlso IOfHttp >= IOfHttps1) OrElse _
                          (IOfHttps > -1 AndAlso IOfHttps >= IOfHttps1) OrElse (IOfHttp1 > -1 AndAlso IOfHttp1 >= IOfHttps1)) Then
                        iIndex = IOfHttp1
                    End If
                    If iIndex <= -1 Then
                        MovePositionTotableCell(tRange, bflag)
                        tRange.InsertAfter(ResultComment)
                        Exit While
                    End If
                    For k As Integer = iIndex To Len(ResultComment) - 1
                        If ResultComment.Substring(k, 1) = " " OrElse ResultComment.Substring(k, 1) = vbCr OrElse ResultComment.Substring(k, 1) = vbCrLf Then
                            EIndex = (k - iIndex)
                            Exit For
                        End If
                        If k = Len(ResultComment) - 1 Then
                            EIndex = (k - iIndex) + 1
                        End If
                    Next
                    If EIndex <= -1 Then
                        EIndex = Len(ResultComment) - 1
                    End If
                    If EIndex <= -1 Then
                        MovePositionTotableCell(tRange, bflag)
                        tRange.InsertAfter(ResultComment)
                        Exit While
                    End If
                    MovePositionTotableCell(tRange, bflag)
                    tRange.InsertAfter(ResultComment.Substring(0, iIndex))
                    MovePositionTotableCell(tRange, bflag)
                    tRange = tRange.Hyperlinks.Add(tRange, If(ResultComment.Substring(iIndex, EIndex).StartsWith("www."), "http://" & ResultComment.Substring(iIndex, EIndex), ResultComment.Substring(iIndex, EIndex)), Nothing, Nothing, ResultComment.Substring(iIndex, EIndex), Nothing).Range
                    If (iIndex + EIndex) < Len(ResultComment) Then
                        ResultComment = ResultComment.Substring((iIndex + EIndex), (Len(ResultComment) - (iIndex + EIndex)))
                    Else
                        ResultComment = String.Empty
                    End If
                End While
            Catch ex As Exception
                ex = Nothing
            Finally
                IOfWww = 0
                IOfHttp = 0
                IOfHttps = 0
                IOfHttp1 = 0
                IOfHttps1 = 0
                iIndex = 0
                EIndex = 0
                bflag = False
            End Try
            Return Nothing
        End Function
        'Funcation Added by manoj jadhav on 20121228 for showing hyperlinks in Lab Result comments
        Private Sub MovePositionTotableCell(ByRef tRange As Microsoft.Office.Interop.Word.Range, ByRef bflag As Boolean)
            Try
                If bflag Then
                    tRange.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd)
                Else
                    tRange.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseStart)
                    bflag = True
                End If
            Catch ex As Exception
                ex = Nothing
            End Try
        End Sub

        ''Bug #63475: 00000435 : To improve performance of Lab result liquid links
        ''Added new function to populate Lab Test liquid link
        Function BuildDataString(ByVal aData As DataTable) As String
            If (IsNothing(aData)) Then
                Return ""
            End If

            Dim dataString As String = ""
            Dim nrRow As Int32, nrCol As Int32

            For nrCol = 0 To aData.Columns.Count - 1
                dataString = dataString & aData.Columns(nrCol).Caption
                If nrCol < aData.Columns.Count - 1 Then
                    dataString = dataString & vbTab
                Else
                    dataString = dataString & vbCr
                End If
            Next

            For nrRow = 0 To aData.Rows.Count - 1
                For nrCol = 0 To aData.Columns.Count - 1
                    dataString = dataString & aData.Rows(nrRow)(nrCol).ToString().Replace(vbTab, " ").Replace(vbCr, " ").Replace(vbLf, " ").Trim
                    If nrCol < aData.Columns.Count - 1 _
                        Then dataString = dataString & vbTab
                Next nrCol
                If nrRow < aData.Rows.Count - 1 _
                    Then dataString = dataString & vbCr
            Next nrRow
            BuildDataString = dataString
        End Function

        ''Bug #63475: 00000435 : To improve performance of Lab result liquid links
        ''Added new function to populate Lab Test liquid link
        Private Function PopulateAndExtendFlowSheetTable(ByVal rng As Wd.Range, ByVal dtFlowSheet As DataTable) As Boolean
            If (IsNothing(dtFlowSheet)) Then
                Return False
            End If
            Dim objMissing As Object = System.Reflection.Missing.Value
            Dim tbl As Wd.Table
            Try

                Dim dataString As String

                rng.Text = BuildDataString(dtFlowSheet)

                tbl = rng.ConvertToTable(vbTab, AutoFitBehavior:=Wd.WdAutoFitBehavior.wdAutoFitFixed, DefaultTableBehavior:=Wd.WdDefaultTableBehavior.wdWord8TableBehavior)
                Dim minimumRow As Integer = Math.Min(tbl.Rows.Count, dtFlowSheet.Rows.Count)
                Dim minimumCol As Integer = Math.Min(tbl.Columns.Count, dtFlowSheet.Columns.Count)
                'SLR: to handle s+2 and j+1
                For s As Int32 = 0 To minimumRow - 1
                    For j As Int32 = 0 To minimumCol - 1
                        dataString = dtFlowSheet.Rows(s)(j).ToString()
                        If String.Compare(dtFlowSheet.Columns(j).ColumnName, "value", False) AndAlso gloGlobal.gloLabGenral.IsResultisHyperLink(dataString) Then
                            If (tbl.Rows.Count >= (s + 2)) AndAlso (tbl.Columns.Count >= (j + 1)) Then


                                tbl.Cell(s + 2, j + 1).Range.Text = ""
                                tbl.Cell(s + 2, j + 1).Range.Hyperlinks.Add(tbl.Cell(s + 2, j + 1).Range, If(dataString.StartsWith("www."), "http://" & dataString, dataString), Nothing, Nothing, dataString, Nothing)
                            End If
                        End If
                    Next
                Next


                Dim style As Wd.Style = CreateWordTableStyleForLabTables()
                FormatWordTables(style, tbl)
                style = Nothing

                ''''Move Cursor down in the Table
                _oCurDoc.ActiveWindow.Selection.EndOf(Unit:=Wd.WdUnits.wdTable)
                _oCurDoc.ActiveWindow.Selection.MoveDown(Wd.WdUnits.wdLine, Count:=2)
                _oCurDoc.ActiveWindow.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
                '_oCurDoc.ActiveWindow.Selection.InsertParagraph()
                _oCurDoc.ActiveWindow.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)

                Return True
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                If _oCurDoc.ActiveWindow.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText Then
                    _oCurDoc.ActiveWindow.Selection.Range.ParentContentControl.Delete(True)
                End If
                Return False
            Finally
                objMissing = Nothing
                tbl = Nothing
            End Try
        End Function

        Private Function PopulateAndExtendPastPregnanciesTable(ByVal tb1 As Wd.Table, ByVal dtFlowSheet As DataTable) As Boolean
            Try
                If (IsNothing(dtFlowSheet)) Then
                    Return False
                End If

                Dim objMissing As Object = System.Reflection.Missing.Value
                Dim minimumRow As Integer = Math.Min(tb1.Rows.Count - 1, dtFlowSheet.Rows.Count)
                Dim minimumCol As Integer = Math.Min(tb1.Columns.Count, dtFlowSheet.Columns.Count)

                tb1.Rows.Add(objMissing)

                ''Set Column Names
                If (tb1.Rows.Count >= 1) Then
                    For i As Integer = 0 To minimumCol - 1
                        'tb1.Cell(1, i + 1).Range.Text = dtFlowSheet.Columns(i).Caption

                        If dtFlowSheet.Columns(i).Caption = "Past Pregnancies" Then

                            tb1.Cell(1, i + 1).Range.Text = dtFlowSheet.Columns(i).Caption
                        Else
                            tb1.Cell(2, i).Range.Text = dtFlowSheet.Columns(i).Caption
                        End If

                        If dtFlowSheet.Columns(i).Caption = "Past Pregnancies" Then
                            If (tb1.Rows.Count >= 1) AndAlso (tb1.Columns.Count >= 1) Then

                                tb1.Rows(1).Cells(i + 1).Merge(tb1.Rows(1).Cells(11))
                                tb1.Rows(1).Cells(i + 1).Range.ParagraphFormat.Alignment = Wd.WdParagraphAlignment.wdAlignParagraphCenter
                            End If
                        ElseIf dtFlowSheet.Columns(i).Caption = "Date" Then

                            If (tb1.Rows.Count >= 1) AndAlso (tb1.Columns.Count >= 1) Then
                                tb1.Rows(2).Cells(i + 1).Merge(tb1.Rows(2).Cells(1))
                            End If
                            tb1.Cell(2, i).Range.Cells.Width = tb1.Cell(2, i).Range.Cells.Width - 25
                        ElseIf dtFlowSheet.Columns(i).Caption = "GA Wks." Then
                            'tb1.Cell(2, i).Range.Cells.Width = tb1.Cell(2, i).Range.Cells.Width + 50
                            'tb1.Cell(2, i + 1).Range.Cells.Width = 50
                        ElseIf dtFlowSheet.Columns(i).Caption = "Comments" Then
                            tb1.Cell(2, i).Range.Cells.Width = tb1.Cell(2, i).Range.Cells.Width + 25
                            If (tb1.Rows.Count >= 1) AndAlso (tb1.Columns.Count >= 1) Then
                                'tb1.Rows(2).Cells(i + 1).Merge(tb1.Rows(2).Cells(1))
                            End If
                        End If

                        'If dtFlowSheet.Columns(i).Caption = "Date" Then
                        '    tb1.Cell(1, 1).Range.Cells.Width = 55

                        'ElseIf dtFlowSheet.Columns(i).Caption = "GA Wks." Then
                        '    tb1.Cell(1, 1).Range.Cells.Width = 20

                        'End If

                        If (0) Then
                            tb1.Columns(i + 1).SetWidth(200, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone)
                        End If
                    Next
                End If

                '''''Move Cursor to the Table 
                _oCurDoc.ActiveWindow.Selection.GoToNext(Microsoft.Office.Interop.Word.WdGoToItem.wdGoToTable)

                ''''''Move Cursor down in the Table
                Dim strCellText As String
                minimumRow = dtFlowSheet.Rows.Count
                For iRow As Integer = 0 To minimumRow - 1
                    tb1.Rows.Add(objMissing)  '''' new Row
                    For iCol As Integer = 0 To minimumCol - 1
                        strCellText = dtFlowSheet.Rows(iRow)(iCol).ToString
                        If (tb1.Rows.Count >= (iRow + 3)) AndAlso (tb1.Columns.Count >= (iCol)) Then
                            If iCol = 0 Then
                                _oCurDoc.ActiveWindow.Selection.Move(Wd.WdUnits.wdRow) '''' Move Cursor to Newly Added Row in table
                                _oCurDoc.ActiveWindow.Selection.MoveRight()

                                tb1.Cell(iRow + 3, iCol).Range.Text = strCellText
                                tb1.Cell(iRow + 3, iCol).Select()

                            Else '''' If the category is already add then add Item in the category
                                _oCurDoc.ActiveWindow.Selection.MoveRight()
                                tb1.Cell(iRow + 3, iCol).Range.Text = strCellText

                                tb1.Cell(iRow + 3, iCol).Select()
                            End If
                        End If

                    Next
                Next

                ''''Move Cursor down in the Table
                _oCurDoc.ActiveWindow.Selection.MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=2)
                _oCurDoc.ActiveWindow.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
                '_oCurDoc.ActiveWindow.Selection.InsertParagraph()
                _oCurDoc.ActiveWindow.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)

                Return True
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                If _oCurDoc.ActiveWindow.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText Then
                    _oCurDoc.ActiveWindow.Selection.Range.ParentContentControl.Delete(True)
                End If
                Return False
            End Try
        End Function

        Private Function PopulateAndExtendFlowSheetTable(ByVal tb1 As Wd.Table, ByVal dtFlowSheet As DataTable) As Boolean
            Try
                If (IsNothing(dtFlowSheet)) Then
                    Return False
                End If

                Dim objMissing As Object = System.Reflection.Missing.Value
                Dim minimumRow As Integer = Math.Min(tb1.Rows.Count - 1, dtFlowSheet.Rows.Count)
                Dim minimumCol As Integer = Math.Min(tb1.Columns.Count, dtFlowSheet.Columns.Count)
                ''Set Column Names
                If (tb1.Rows.Count >= 1) Then
                    For i As Integer = 0 To minimumCol - 1
                        tb1.Cell(1, i + 1).Range.Text = dtFlowSheet.Columns(i).Caption
                        If (0) Then
                            tb1.Columns(i + 1).SetWidth(200, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone)
                        End If
                    Next
                End If

                '''''Move Cursor to the Table 
                _oCurDoc.ActiveWindow.Selection.GoToNext(Microsoft.Office.Interop.Word.WdGoToItem.wdGoToTable)

                ''''''Move Cursor down in the Table
                Dim strCellText As String
                minimumRow = dtFlowSheet.Rows.Count
                For iRow As Integer = 0 To minimumRow - 1
                    tb1.Rows.Add(objMissing)  '''' new Row
                    For iCol As Integer = 0 To minimumCol - 1
                        strCellText = dtFlowSheet.Rows(iRow)(iCol).ToString
                        If (tb1.Rows.Count >= (iRow + 2)) AndAlso (tb1.Columns.Count >= (iCol + 1)) Then
                            If iCol = 0 Then
                                _oCurDoc.ActiveWindow.Selection.Move(Wd.WdUnits.wdRow) '''' Move Cursor to Newly Added Row in table
                                _oCurDoc.ActiveWindow.Selection.MoveRight()
                                '''' Add Catergory in New Row and category Column
                                'tb1.Cell(iRow + 2, iCol + 1).Range.Text = strCellText 'commneted by mnaoj on 20121127

                                'start added by manoj on 20121127 for dispalying hyperlinks in result value
                                If String.Compare(dtFlowSheet.Columns(iCol).ColumnName, "value", False) AndAlso gloGlobal.gloLabGenral.IsResultisHyperLink(strCellText) Then
                                    tb1.Cell(iRow + 2, iCol + 1).Range.Hyperlinks.Add(tb1.Cell(iRow + 2, iCol + 1).Range, If(strCellText.StartsWith("www."), "http://" & strCellText, strCellText), Nothing, Nothing, strCellText, Nothing)
                                Else
                                    tb1.Cell(iRow + 2, iCol + 1).Range.Text = strCellText
                                End If
                                'end of added by manoj on 20121127 for dispalying hyperlinks in result value

                                '''' Add Item for Selected category 
                                '  Dim oNameField As Wd.FormField
                                tb1.Cell(iRow + 2, iCol + 1).Select()

                            Else '''' If the category is already add then add Item in the category
                                _oCurDoc.ActiveWindow.Selection.MoveRight()
                                ' tb1.Cell(iRow + 2, iCol + 1).Range.Text = strCellText  'commneted by mnaoj on 20121127
                                'start added by manoj on 20121127 for dispalying hyperlinks in result value
                                If String.Compare(dtFlowSheet.Columns(iCol).ColumnName, "value", False) AndAlso gloGlobal.gloLabGenral.IsResultisHyperLink(strCellText) Then
                                    tb1.Cell(iRow + 2, iCol + 1).Range.Hyperlinks.Add(tb1.Cell(iRow + 2, iCol + 1).Range, If(strCellText.StartsWith("www."), "http://" & strCellText, strCellText), Nothing, Nothing, strCellText, Nothing)
                                Else
                                    tb1.Cell(iRow + 2, iCol + 1).Range.Text = strCellText
                                End If
                                'end of added by manoj on 20121127 for dispalying hyperlinks in result value
                                tb1.Cell(iRow + 2, iCol + 1).Select()
                            End If
                        End If

                    Next
                Next

                ''''Move Cursor down in the Table
                _oCurDoc.ActiveWindow.Selection.MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=2)
                _oCurDoc.ActiveWindow.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
                '_oCurDoc.ActiveWindow.Selection.InsertParagraph()
                _oCurDoc.ActiveWindow.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)

                Return True
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                If _oCurDoc.ActiveWindow.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText Then
                    _oCurDoc.ActiveWindow.Selection.Range.ParentContentControl.Delete(True)
                End If
                Return False
            End Try
        End Function


        Private Function PopulateAndExtendTreatmentTable(ByVal tb1 As Wd.Table, ByVal dtFlowSheet As DataTable, ByVal FieldName As String) As Boolean
            Try
                If (IsNothing(dtFlowSheet)) Then
                    Return False
                End If

                Dim objMissing As Object = System.Reflection.Missing.Value
                Dim minimumRow As Integer = Math.Min(tb1.Rows.Count - 1, dtFlowSheet.Rows.Count)
                Dim minimumCol As Integer = Math.Min(tb1.Columns.Count, dtFlowSheet.Columns.Count)

                If (tb1.Rows.Count >= 1) Then
                    For i As Integer = 0 To minimumCol - 1
                        tb1.Cell(1, i + 1).Range.Text = dtFlowSheet.Columns(i).Caption
                        If (0) Then
                            tb1.Columns(i + 1).SetWidth(200, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone)
                        End If


                    Next
                    If FieldName.Contains("|Modifier1") Then
                        tb1.Columns(1).SetWidth(100, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone)
                        tb1.Columns(2).SetWidth(100, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone)
                        tb1.Columns(3).SetWidth(100, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone)
                    ElseIf FieldName.Contains("|Modifier2") Then
                        tb1.Columns(1).SetWidth(230, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone)
                        tb1.Columns(2).SetWidth(230, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone)
                        tb1.Columns(3).SetWidth(40, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone)
                    ElseIf FieldName.Contains("|Modifier3") Then
                        tb1.Columns(1).SetWidth(60, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone)
                        tb1.Columns(2).SetWidth(60, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone)
                        tb1.Columns(3).SetWidth(170, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone)
                        tb1.Columns(4).SetWidth(170, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone)
                        tb1.Columns(5).SetWidth(40, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone)
                    End If
                End If

                _oCurDoc.ActiveWindow.Selection.GoToNext(Microsoft.Office.Interop.Word.WdGoToItem.wdGoToTable)

                Dim strCellText As String
                minimumRow = dtFlowSheet.Rows.Count
                For iRow As Integer = 0 To minimumRow - 1
                    tb1.Rows.Add(objMissing)
                    For iCol As Integer = 0 To minimumCol - 1
                        strCellText = dtFlowSheet.Rows(iRow)(iCol).ToString
                        If (tb1.Rows.Count >= (iRow + 2)) AndAlso (tb1.Columns.Count >= (iCol + 1)) Then
                            If iCol = 0 Then
                                _oCurDoc.ActiveWindow.Selection.Move(Wd.WdUnits.wdRow)
                                _oCurDoc.ActiveWindow.Selection.MoveRight()
                                If String.Compare(dtFlowSheet.Columns(iCol).ColumnName, "value", False) AndAlso gloGlobal.gloLabGenral.IsResultisHyperLink(strCellText) Then
                                    tb1.Cell(iRow + 2, iCol + 1).Range.Hyperlinks.Add(tb1.Cell(iRow + 2, iCol + 1).Range, If(strCellText.StartsWith("www."), "http://" & strCellText, strCellText), Nothing, Nothing, strCellText, Nothing)
                                Else
                                    If Not IsNothing(dtHoldDataForCPTModifierLL) Then
                                        If iRow < dtHoldDataForCPTModifierLL.Rows.Count AndAlso iCol < 2 Then
                                            If Convert.ToString(dtHoldDataForCPTModifierLL.Rows(iRow)(2)).Contains(Convert.ToString(dtFlowSheet.Rows(iRow)(2))) Then
                                                strCellText = Convert.ToString(dtHoldDataForCPTModifierLL.Rows(iRow)(iCol))
                                            End If
                                        End If
                                    End If
                                    tb1.Cell(iRow + 2, iCol + 1).Range.Text = strCellText
                                End If
                                tb1.Cell(iRow + 2, iCol + 1).Select()
                            Else
                                _oCurDoc.ActiveWindow.Selection.MoveRight()
                                If String.Compare(dtFlowSheet.Columns(iCol).ColumnName, "value", False) AndAlso gloGlobal.gloLabGenral.IsResultisHyperLink(strCellText) Then
                                    tb1.Cell(iRow + 2, iCol + 1).Range.Hyperlinks.Add(tb1.Cell(iRow + 2, iCol + 1).Range, If(strCellText.StartsWith("www."), "http://" & strCellText, strCellText), Nothing, Nothing, strCellText, Nothing)
                                Else
                                    If Not IsNothing(dtHoldDataForCPTModifierLL) Then
                                        If iRow < dtHoldDataForCPTModifierLL.Rows.Count AndAlso iCol < 2 Then
                                            If Convert.ToString(dtHoldDataForCPTModifierLL.Rows(iRow)(2)).Contains(Convert.ToString(dtFlowSheet.Rows(iRow)(2))) Then
                                                strCellText = Convert.ToString(dtHoldDataForCPTModifierLL.Rows(iRow)(iCol))
                                            End If
                                        End If
                                    End If
                                    tb1.Cell(iRow + 2, iCol + 1).Range.Text = strCellText
                                End If
                                tb1.Cell(iRow + 2, iCol + 1).Select()
                            End If
                        End If

                    Next
                Next

                _oCurDoc.ActiveWindow.Selection.MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=2)
                _oCurDoc.ActiveWindow.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
                _oCurDoc.ActiveWindow.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
                Return True
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                If _oCurDoc.ActiveWindow.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText Then
                    _oCurDoc.ActiveWindow.Selection.Range.ParentContentControl.Delete(True)
                End If
                Return False
            End Try
        End Function


        Private Function PopulateAndExtendDASTable(ByVal tb1 As Wd.Table, ByVal dtFlowSheet As DataTable) As Boolean
            Try
                If (IsNothing(dtFlowSheet)) Then
                    Return False
                End If
                If (IsNothing(tb1)) Then
                    Return False
                End If
                Dim objMissing As Object = System.Reflection.Missing.Value
                Dim minimumColumn As Integer = Math.Min(tb1.Columns.Count, dtFlowSheet.Columns.Count)
                'SLR: To avoid i+1 > tbl.Columns.count
                ''Set Column Names
                If (tb1.Rows.Count >= 1) Then


                    For i As Integer = 0 To minimumColumn - 1
                        If (dtFlowSheet.Rows.Count >= 1) Then

                            If (dtFlowSheet.Rows(0)(i).ToString().ToUpper() = "DATE") Then
                                tb1.Cell(1, i + 1).Range.Text = "Measurements"
                            Else
                                tb1.Cell(1, i + 1).Range.Text = dtFlowSheet.Rows(0)(i).ToString()
                            End If

                        End If

                    Next
                End If

                '''''Move Cursor to the Table 
                _oCurDoc.ActiveWindow.Selection.GoToNext(Microsoft.Office.Interop.Word.WdGoToItem.wdGoToTable)

                ''''''Move Cursor down in the Table
                Dim strCellText As String
                Dim minimumRow As Integer = dtFlowSheet.Rows.Count
                'SLR: To avoid irow+2 > tbl.rows.count
                For iRow As Integer = 1 To minimumRow - 1
                    tb1.Rows.Add(objMissing)  '''' new Row
                    For iCol As Integer = 0 To minimumColumn - 1
                        strCellText = dtFlowSheet.Rows(iRow)(iCol).ToString
                        If (tb1.Rows.Count >= (iRow + 1)) AndAlso (tb1.Columns.Count >= (iCol + 1)) Then
                            If iCol = 0 Then
                                _oCurDoc.ActiveWindow.Selection.Move(Wd.WdUnits.wdRow) '''' Move Cursor to Newly Added Row in table
                                _oCurDoc.ActiveWindow.Selection.MoveRight()
                                '''' Add Catergory in New Row and category Column
                                tb1.Cell(iRow + 1, iCol + 1).Range.Text = strCellText
                                '''' Add Item for Selected category 
                                '  Dim oNameField As Wd.FormField
                                tb1.Cell(iRow + 1, iCol + 1).Select()

                            Else '''' If the category is already add then add Item in the category
                                _oCurDoc.ActiveWindow.Selection.MoveRight()
                                tb1.Cell(iRow + 1, iCol + 1).Range.Text = strCellText
                                tb1.Cell(iRow + 1, iCol + 1).Select()
                            End If
                        End If

                    Next
                Next

                ''''Move Cursor down in the Table
                '28052012 Bug no.27574 *Move DownCount Incremented by 1*
                _oCurDoc.ActiveWindow.Selection.MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=2)
                _oCurDoc.ActiveWindow.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
                '_oCurDoc.ActiveWindow.Selection.InsertParagraph()
                '_oCurDoc.ActiveWindow.Selection.TypeParagraph()
                _oCurDoc.ActiveWindow.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)

                Return True
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                'MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Try
                    If _oCurDoc.ActiveWindow.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText Then
                        _oCurDoc.ActiveWindow.Selection.Range.ParentContentControl.Delete(True)
                    End If
                Catch ex1 As Exception

                End Try

                Return False
            End Try
        End Function

        'Developer:Yatin N. Bhagat
        'Date:12/13/2011
        'Bug ID/PRD Name/Salesforce Case:Liquid Link For Ob Vital Table
        'Reason: To Add Table for Ob Vital Table Liquid Link

        Private Function PopulateAndExtendObVitalTable(ByVal tb1 As Wd.Table, ByVal dtFlowSheet As DataTable) As Boolean
            Try
                If (IsNothing(dtFlowSheet)) Then
                    Return False
                End If

                Dim objMissing As Object = System.Reflection.Missing.Value
                Dim nrCols As Integer = dtFlowSheet.Columns.Count
                'Resolved bug No 79985::Liquid link not refreshing when OB vitals data added after template load
                Dim minCols As Integer = dtFlowSheet.Columns.Count - 1 ' Math.Min(dtFlowSheet.Columns.Count, tb1.Columns.Count)

                'Added New Row  ANd Merged as Required
                tb1.Rows.Add(objMissing)  '
                Dim iReset As String = "0"
                Dim j As Integer = 0

                'Set Column Names
                For i As Integer = 0 To minCols - 1
                    If dtFlowSheet.Columns(i).Caption = "PrePregnancyWeight" Then
                        If (tb1.Rows.Count >= 1) AndAlso (tb1.Columns.Count >= 1) Then
                            tb1.Cell(1, 1).Range.Text = "Pre Pregnancy Weight"
                            tb1.Cell(1, 1).Range.Text = "Pre Pregnancy Weight: " + Convert.ToString(dtFlowSheet.Rows(dtFlowSheet.Rows.Count - 1)("PrePregnancyWeight"))
                            tb1.Rows(1).Cells(i + 1).Merge(tb1.Rows(1).Cells(8))
                            'tb1.Cell(1, 1).Range.Cells.Width = 306
                        End If

                    ElseIf dtFlowSheet.Columns(i).Caption = "WeightChange" Then
                        If (nrCols > 1) Then
                            If (tb1.Rows.Count >= 1) Then
                                tb1.Cell(1, 2).Range.Text = "Weight Change"
                                tb1.Cell(1, 2).Range.Text = "Weight Change: " + Convert.ToString(dtFlowSheet.Rows(dtFlowSheet.Rows.Count - 1)("WeightChange"))
                            End If

                        End If
                        If (tb1.Rows.Count >= 1) Then
                            tb1.Rows(1).Cells(i + 1).Merge(tb1.Rows(1).Cells(6))
                        End If


                    ElseIf dtFlowSheet.Columns(i).Caption <> "PrePregnancyWeight" AndAlso dtFlowSheet.Columns(i).Caption <> "WeightChange" AndAlso dtFlowSheet.Columns(i).Caption <> "nextAppt" AndAlso dtFlowSheet.Columns(i).Caption <> "Comments" Then
                        If (tb1.Rows.Count > 1) AndAlso (j < tb1.Columns.Count) Then
                            tb1.Cell(2, j + 1).Range.Text = dtFlowSheet.Columns(i).Caption
                            If dtFlowSheet.Columns(i).Caption = "Date" Then
                                tb1.Cell(2, j + 1).Range.Cells.Width = tb1.Cell(2, j + 1).Range.Cells.Width + 21
                                tb1.Cell(2, j + 1).Range.Bold = 1
                            ElseIf dtFlowSheet.Columns(i).Caption = "WeekGest" Then
                                tb1.Cell(2, j + 1).Range.Text = "Week Gest. (Best Est.)"
                                tb1.Cell(2, j + 1).Range.Cells.Width = tb1.Cell(2, j + 1).Range.Cells.Width + 4
                            ElseIf dtFlowSheet.Columns(i).Caption = "FundalHeight" Then
                                tb1.Cell(2, j + 1).Range.Cells.Width = tb1.Cell(2, j + 1).Range.Cells.Width - 2
                                tb1.Cell(2, j + 1).Range.Text = "Fundal Height(cm)"
                            ElseIf dtFlowSheet.Columns(i).Caption = "FHR" Then
                                tb1.Cell(2, j + 1).Range.Cells.Width = tb1.Cell(2, j + 1).Range.Cells.Width - 5
                            ElseIf dtFlowSheet.Columns(i).Caption = "FetalMovement" Then
                                tb1.Cell(2, j + 1).Range.Text = "Fetal Movement P=Present A=Absent R=Reduced"
                                tb1.Cell(2, j + 1).Range.Cells.Width = tb1.Cell(2, j + 1).Range.Cells.Width - 6
                            ElseIf dtFlowSheet.Columns(i).Caption = "PreTermLaborSigns" Then
                                tb1.Cell(2, j + 1).Range.Text = "Preterm Labor Signs P=Present A=Absent"
                                tb1.Cell(2, j + 1).Range.Cells.Width = tb1.Cell(2, j + 1).Range.Cells.Width - 3
                            ElseIf dtFlowSheet.Columns(i).Caption = "CarvixExam" Then
                                tb1.Cell(2, j + 1).Range.Cells.Width = tb1.Cell(2, j + 1).Range.Cells.Width + 5
                                tb1.Cell(2, j + 1).Range.Text = "Cervical Exam (DIL/EFF.STA)"
                            ElseIf dtFlowSheet.Columns(i).Caption = "BloodPressure" Then
                                tb1.Cell(2, j + 1).Range.Text = "Blood Pressure"
                                tb1.Cell(2, j + 1).Range.Cells.Width = tb1.Cell(2, j + 1).Range.Cells.Width + 9
                            ElseIf dtFlowSheet.Columns(i).Caption = "Weight" Then
                                tb1.Cell(2, j + 1).Range.Cells.Width = tb1.Cell(2, j + 1).Range.Cells.Width - 5
                            ElseIf dtFlowSheet.Columns(i).Caption = "Edema" Then
                                tb1.Cell(2, j + 1).Range.Cells.Width = tb1.Cell(2, j + 1).Range.Cells.Width - 4
                            ElseIf dtFlowSheet.Columns(i).Caption = "Urine" Then
                                tb1.Cell(2, j + 1).Range.Text = "Urine(Albumin/Glucose)"
                                'tb1.Cell(2, j + 1).Range.Cells.Width = 46
                            ElseIf dtFlowSheet.Columns(i).Caption = "PainLevel" Then
                                tb1.Cell(2, j + 1).Range.Text = "Pain Scale(0-10)"
                                tb1.Cell(2, j + 1).Range.Cells.Width = tb1.Cell(2, j + 1).Range.Cells.Width - 14
                            End If

                            tb1.Cell(2, j + 1).Range.Orientation = Microsoft.Office.Interop.Word.WdTextOrientation.wdTextOrientationUpward
                            tb1.Cell(2, j + 1).Range.Cells.Height = 130

                        End If

                        j = j + 1
                    End If

                Next

                '''''Move Cursor to the Table 
                ''_oCurDoc.ActiveWindow.Selection.GoToNext(Microsoft.Office.Interop.Word.WdGoToItem.wdGoToTable)
                ''''''Move Cursor down in the Table

                Dim strCellText As String
                Dim FlagSetValue As String = "0"
                Dim rCount As Integer = dtFlowSheet.Rows.Count - 1
                Dim d As Integer = 0
                Dim tempRCount As Integer = 0

                'Added Values in the row of Table

                Try
                    For iRow As Integer = 0 To dtFlowSheet.Rows.Count - 1
                        tb1.Rows.Add(objMissing)  '''' new Row
                        Dim c As Integer = 0
                        'If iRow = 0 Then
                        '    FlagSetValue = "0"
                        'End If
                        For iCol As Integer = 0 To nrCols - 1
                            strCellText = dtFlowSheet.Rows(iRow)(iCol).ToString
                            If dtFlowSheet.Columns(iCol).Caption <> "PrePregnancyWeight" AndAlso dtFlowSheet.Columns(iCol).Caption <> "WeightChange" AndAlso dtFlowSheet.Columns(iCol).Caption <> "nextAppt" AndAlso dtFlowSheet.Columns(iCol).Caption <> "Comments" Then
                                If (((tempRCount + 3) <= tb1.Rows.Count) AndAlso ((c + 1) <= tb1.Columns.Count)) Then
                                    If iCol = 0 Then
                                        _oCurDoc.ActiveWindow.Selection.Move(Wd.WdUnits.wdRow) '''' Move Cursor to Newly Added Row in table
                                        _oCurDoc.ActiveWindow.Selection.MoveRight()
                                        tb1.Cell(tempRCount + 3, c + 1).Range.Orientation = Microsoft.Office.Interop.Word.WdTextOrientation.wdTextOrientationHorizontal
                                        tb1.Cell(tempRCount + 3, c + 1).Range.Text = strCellText
                                        tb1.Cell(tempRCount + 3, c + 1).Range.Cells.Height = 40
                                        tb1.Cell(tempRCount + 3, c + 1).Select()
                                    Else
                                        _oCurDoc.ActiveWindow.Selection.MoveRight()
                                        tb1.Cell(tempRCount + 3, c + 1).Range.Orientation = Microsoft.Office.Interop.Word.WdTextOrientation.wdTextOrientationHorizontal
                                        'Dim oNameField As Wd.FormField

                                        If dtFlowSheet.Columns(iCol).Caption = "PreTermLaborSigns" Then
                                            If strCellText = "Absent" Then
                                                tb1.Cell(tempRCount + 3, c + 1).Range.Text = "A"
                                            ElseIf strCellText = "Present" Then
                                                tb1.Cell(tempRCount + 3, c + 1).Range.Text = "P"
                                            End If
                                        ElseIf dtFlowSheet.Columns(iCol).Caption = "FetalMovement" Then
                                            If strCellText = "Absent" Then
                                                tb1.Cell(tempRCount + 3, c + 1).Range.Text = "A"
                                            ElseIf strCellText = "Present" Then
                                                tb1.Cell(tempRCount + 3, c + 1).Range.Text = "P"
                                            ElseIf strCellText = "Reduced" Then
                                                tb1.Cell(tempRCount + 3, c + 1).Range.Text = "R"
                                            End If
                                        ElseIf dtFlowSheet.Columns(iCol).Caption = "Weight" Then
                                            If strCellText <> "" Then
                                                Dim weight As Double = Convert.ToDouble(strCellText)
                                                weight = Math.Round(weight, 1)
                                                tb1.Cell(tempRCount + 3, c + 1).Range.Text = weight

                                            End If
                                        ElseIf dtFlowSheet.Columns(iCol).Caption = "CarvixExam" Then
                                            Try
                                                If strCellText <> "" Then
                                                    Dim a() As String
                                                    a = strCellText.Split("/")
                                                    tb1.Cell(tempRCount + 3, c + 1).Range.Text = a(0) + " cm"
                                                    If (a.Length > 1) Then
                                                        tb1.Cell(tempRCount + 3, c + 1).Range.Text = tb1.Cell(tempRCount + 3, c + 1).Range.Text.Trim() + a(1).Trim() + " %"
                                                    End If
                                                    If (a.Length > 2) Then
                                                        tb1.Cell(tempRCount + 3, c + 1).Range.Text = tb1.Cell(tempRCount + 3, c + 1).Range.Text.Trim() + a(2).Trim()
                                                    End If
                                                    tb1.Cell(tempRCount + 3, c + 1).Range.Paragraphs.SpaceAfter = 0 'To Remove Line Spacing
                                                End If
                                            Catch ex As Exception
                                            End Try


                                        Else
                                            tb1.Cell(tempRCount + 3, c + 1).Range.Text = strCellText

                                        End If
                                        tb1.Cell(tempRCount + 3, c + 1).Range.Cells.Height = 40
                                        tb1.Cell(tempRCount + 3, c + 1).Select()
                                    End If
                                End If

                                c = c + 1
                            Else
                                Dim cellText As String = ""
                                If iRow = 0 Then
                                    If dtFlowSheet.Columns(iCol).Caption = "PrePregnancyWeight" Then
                                        _oCurDoc.ActiveWindow.Selection.MoveRight()
                                        'Dim oNameField As Wd.FormField
                                        'tb1.Cell(iRow, 1).Range.Text = "Pre Pregnancy Weight : " + strCellText ' Convert.ToString(dtFlowSheet.Rows(0)("PrePregnancyWeight"))
                                        'tb1.Cell(iRow, 1).ActiveWindow.Selection.Select()
                                    ElseIf dtFlowSheet.Columns(iCol).Caption = "WeightChange" Then
                                        _oCurDoc.ActiveWindow.Selection.MoveRight()
                                        ' Dim oNameField As Wd.FormField
                                        ' tb1.Cell(iRow, 2).Range.Text = "Weight Change : " + strCellText 'Convert.ToString(dtFlowSheet.Rows(0)("WeightChange"))
                                        ' tb1.Cell(iRow, 2).ActiveWindow.Selection.Select()
                                        FlagSetValue = "Weight Change : " + strCellText
                                    End If
                                End If
                                If dtFlowSheet.Columns(iCol).Caption = "Comments" Then
                                    tb1.Rows.Add(objMissing)  '''' new Row
                                    _oCurDoc.ActiveWindow.Selection.Move(Wd.WdUnits.wdRow)
                                    _oCurDoc.ActiveWindow.Selection.MoveRight()
                                    'SLR is '0' allowed : 9/1/2014?
                                    If ((tempRCount + 4) <= tb1.Rows.Count) Then
                                        tb1.Cell(tempRCount + 4, 0).Range.Text = "Comments: " + strCellText
                                        tb1.Cell(tempRCount + 4, 0).Range.Orientation = Microsoft.Office.Interop.Word.WdTextOrientation.wdTextOrientationHorizontal
                                        tb1.Cell(tempRCount + 4, 0).Range.Cells.Height = 20
                                    End If

                                    tempRCount = tempRCount + 1
                                    rCount = rCount + 2
                                ElseIf dtFlowSheet.Columns(iCol).Caption = "nextAppt" Then
                                    tb1.Rows.Add(objMissing)  '''' new Row
                                    _oCurDoc.ActiveWindow.Selection.Move(Wd.WdUnits.wdRow)
                                    _oCurDoc.ActiveWindow.Selection.MoveRight()

                                    'SLR is '0' allowed : 9/1/2014?

                                    '28-Apr-15 Aniket: Commented the below condition as 'Next Appointment' column was not visible
                                    'If ((tempRCount + 5) <= tb1.Rows.Count) Then
                                    tb1.Cell(tempRCount + 5, 0).Range.Text = "Next Appointment: " + strCellText
                                    tb1.Cell(tempRCount + 5, 0).Range.Orientation = Microsoft.Office.Interop.Word.WdTextOrientation.wdTextOrientationHorizontal
                                    tb1.Cell(tempRCount + 5, 0).Range.Cells.Height = 20
                                    'End If

                                    tempRCount = tempRCount + 1
                                    rCount = rCount + 2
                                End If
                            End If
                        Next
                        d = d + 1
                        tempRCount = tempRCount + 1
                    Next

                    For ir As Integer = 2 To tb1.Rows.Count
                        'SLR is '0' allowed : 9/1/2014?
                        If tb1.Cell(ir, 0).Range.Text.Contains("Comments: ") Then
                            tb1.Cell(ir, 0).Merge(MergeTo:=tb1.Cell(ir, 13))
                            tb1.Cell(ir, 0).Range.Bold = 0
                        ElseIf tb1.Cell(ir, 0).Range.Text.Contains("Next Appointment: ") Then
                            tb1.Cell(ir, 0).Merge(MergeTo:=tb1.Cell(ir, 13))
                            tb1.Cell(ir, 0).Range.Bold = 0
                        End If
                    Next
                Catch ex As Exception
                End Try

                ''''Move Cursor down in the Table

                _oCurDoc.ActiveWindow.Selection.MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=tb1.Rows.Count + 2)
                _oCurDoc.ActiveWindow.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
                _oCurDoc.ActiveWindow.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)

                Return True
            Catch ex As Exception
                If _oCurDoc.ActiveWindow.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText Then
                    _oCurDoc.ActiveWindow.Selection.Range.ParentContentControl.Delete(True)
                End If
                Return False
            End Try
        End Function


        Private Function CreateWordTableStyle() As Wd.Style
            Dim styleTypeTable As Object = Wd.WdStyleType.wdStyleTypeTable
            Dim TextureNone As Wd.WdTextureIndex = Microsoft.Office.Interop.Word.WdTextureIndex.wdTextureNone
            Dim ColorGray10 As Wd.WdColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray10
            Dim LineStyleSingle As Wd.WdLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle
            Dim StyleName As String = "New Table Style_" & Now & Rnd().ToString.Replace(".", "")
            Dim styl As Wd.Style = _oCurDoc.Styles.Add(StyleName, styleTypeTable)
            styl.Font.Name = "Arial"
            styl.Font.Size = 10
            Dim stylTbl As Wd.TableStyle = styl.Table
            stylTbl.Borders.Enable = 1

            Dim evenrowbinding As Wd.ConditionalStyle = stylTbl.Condition(Microsoft.Office.Interop.Word.WdConditionCode.wdEvenRowBanding)
            Dim FirstRow As Wd.ConditionalStyle = stylTbl.Condition(Microsoft.Office.Interop.Word.WdConditionCode.wdFirstRow)
            Try
                evenrowbinding.Shading.Texture = TextureNone
                evenrowbinding.Shading.BackgroundPatternColor = ColorGray10
                evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).LineStyle = LineStyleSingle
                evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = LineStyleSingle
                evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical).LineStyle = LineStyleSingle
                evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = LineStyleSingle

                FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).LineStyle = LineStyleSingle
                FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop).LineStyle = LineStyleSingle
                FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical).LineStyle = LineStyleSingle
                FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = LineStyleSingle
                FirstRow.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray25
                FirstRow.Font.Size = 10
                FirstRow.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdAuto
                FirstRow.Font.Bold = 1

                'stylTbl.RowStripe = 1
                Return styl
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                Return Nothing
            Finally
                TextureNone = Nothing
                ColorGray10 = Nothing
                LineStyleSingle = Nothing
                styl = Nothing
                stylTbl = Nothing
                evenrowbinding = Nothing
                FirstRow = Nothing
            End Try

        End Function

        Private Function CreateWordTableStyleOB() As Wd.Style
            Dim styleTypeTable As Object = Wd.WdStyleType.wdStyleTypeTable
            Dim TextureNone As Wd.WdTextureIndex = Microsoft.Office.Interop.Word.WdTextureIndex.wdTextureNone
            Dim ColorGray10 As Wd.WdColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray10
            Dim LineStyleSingle As Wd.WdLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle
            Dim StyleName As String = "New Table Style_" & Now & Rnd().ToString.Replace(".", "")
            Dim styl As Wd.Style = _oCurDoc.Styles.Add(StyleName, styleTypeTable)
            styl.Font.Name = "Calibri"
            styl.Font.Size = 9
            Dim stylTbl As Wd.TableStyle = styl.Table
            stylTbl.Borders.Enable = 1

            Dim evenrowbinding As Wd.ConditionalStyle = stylTbl.Condition(Microsoft.Office.Interop.Word.WdConditionCode.wdEvenRowBanding)
            Dim FirstRow As Wd.ConditionalStyle = stylTbl.Condition(Microsoft.Office.Interop.Word.WdConditionCode.wdFirstRow)
            Try
                evenrowbinding.Shading.Texture = TextureNone
                evenrowbinding.Shading.BackgroundPatternColor = ColorGray10
                evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).LineStyle = LineStyleSingle
                evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = LineStyleSingle
                evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical).LineStyle = LineStyleSingle
                evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = LineStyleSingle

                FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).LineStyle = LineStyleSingle
                FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop).LineStyle = LineStyleSingle
                FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical).LineStyle = LineStyleSingle
                FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = LineStyleSingle
                FirstRow.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray25
                FirstRow.Font.Size = 9
                FirstRow.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdAuto
                FirstRow.Font.Bold = 1

                'stylTbl.RowStripe = 1
                Return styl
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                Return Nothing
            Finally
                TextureNone = Nothing
                ColorGray10 = Nothing
                LineStyleSingle = Nothing
                styl = Nothing
                stylTbl = Nothing
                evenrowbinding = Nothing
                FirstRow = Nothing
            End Try

        End Function

        Private Function CreateWordTableStyleForLabTables() As Wd.Style
            Dim styleTypeTable As Object = Wd.WdStyleType.wdStyleTypeTable
            Dim TextureNone As Wd.WdTextureIndex = Microsoft.Office.Interop.Word.WdTextureIndex.wdTextureNone
            Dim ColorGray10 As Wd.WdColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray10
            Dim LineStyleSingle As Wd.WdLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle
            Dim StyleName As String = "New Table Style_" & Now & Rnd().ToString.Replace(".", "")
            Dim styl As Wd.Style = _oCurDoc.Styles.Add(StyleName, styleTypeTable)
            styl.Font.Name = "Arial"
            styl.Font.Size = 9
            Dim stylTbl As Wd.TableStyle = styl.Table
            stylTbl.Borders.Enable = 1

            Dim evenrowbinding As Wd.ConditionalStyle = stylTbl.Condition(Microsoft.Office.Interop.Word.WdConditionCode.wdEvenRowBanding)
            Dim FirstRow As Wd.ConditionalStyle = stylTbl.Condition(Microsoft.Office.Interop.Word.WdConditionCode.wdFirstRow)

            Try
                evenrowbinding.Shading.Texture = TextureNone
                evenrowbinding.Shading.BackgroundPatternColor = ColorGray10
                evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).LineStyle = LineStyleSingle
                evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = LineStyleSingle
                evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical).LineStyle = LineStyleSingle
                evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = LineStyleSingle


                FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).LineStyle = LineStyleSingle
                FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop).LineStyle = LineStyleSingle
                FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical).LineStyle = LineStyleSingle
                FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = LineStyleSingle
                FirstRow.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray25
                FirstRow.Font.Size = 9
                FirstRow.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdAuto
                FirstRow.Font.Bold = 1

                'stylTbl.RowStripe = 1
                Return styl
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                Return Nothing
            Finally
                TextureNone = Nothing
                ColorGray10 = Nothing
                LineStyleSingle = Nothing
                styl = Nothing
                stylTbl = Nothing
                evenrowbinding = Nothing
                FirstRow = Nothing
            End Try

        End Function
        Private Sub FormatWordTables(ByVal tstyle As Wd.Style, ByVal tb1 As Wd.Table)
            Try
                tb1.Range.Style = tstyle
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try

        End Sub

        '''' <summary>
        '''' To get the template flag w.rto gloEMr Tempalte Id
        '''' </summary>
        '''' <param name="DocType"></param>
        '''' <returns></returns>
        '''' <remarks></remarks>
        Private Function GetTemplateFlag(ByVal DocType As enumDocType) As Int16
            Select Case DocType
                Case enumDocType.CheifComplaints
                    Return 0
                Case enumDocType.Diagnosis
                    Return 0
                Case enumDocType.Flowsheet
                    Return 0
                Case enumDocType.History
                    Return 0
                Case enumDocType.Medication
                    Return 0
                Case enumDocType.None
                    Return 0
                Case enumDocType.RadiologyOrders
                    Return 0
                Case enumDocType.Others
                    Return 0
                Case enumDocType.PatientDemographics
                    Return 0
                Case enumDocType.PatientEducation
                    Return 0
                Case enumDocType.PatientGuideline
                    Return 0
                Case gloEMRWord.enumDocType.Prescription
                    Return 0
                Case enumDocType.ProblemList
                    Return 0
                Case enumDocType.Referrals
                    Return 0
                Case enumDocType.ROS
                    Return 0
                Case enumDocType.SmartDiagnosis
                    Return 0
                Case enumDocType.SmartTreatment
                    Return 0
                Case enumDocType.Tasks
                    Return 0
                Case enumDocType.Treatment
                    Return 0
                Case enumDocType.Vitals
                    Return 0
                Case enumDocType.Contacts
                    Return 0
                Case enumDocType.Narration
                    Return 0
            End Select

            Return 0
        End Function

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal PatientId As Long)
            MyBase.New()
            nPatientID = PatientId
            Call Get_PatientDetails(PatientId)
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            CurDocument = Nothing
        End Sub

        '''' <summary>
        '''' To Fill the respective templates based upon the flag set
        '''' </summary>
        '''' <param name="DocType"></param>
        '''' <returns></returns>
        '''' <remarks></remarks>
        Public Function FillTemplates(ByVal TemplateFlag As enumTemplateFlag) As DataTable
            Dim oDB As New DataBaseLayer
            Dim oParamater As DBParameter

            ''22-Mar-13 Aniket: Resolving Memory Leak Issues
            Dim oResultTable As DataTable
            'Dim oResultTable As New DataTable
            Try

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.Int
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@flag"
                oParamater.Value = TemplateFlag '' To get respective templates
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oResultTable = oDB.GetDataTable("gsp_FillTemplateGallery_MST")

                If Not oResultTable Is Nothing Then
                    Return oResultTable
                End If
                Return Nothing
                'oDB.Dispose()
                'oDB = Nothing

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Nothing
            Finally

                ''22-Mar-13 Aniket: Resolving Memory Leak Issues
                If Not IsNothing(oDB) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If

                '08-May-13 Aniket: Resolving Memory Leaks
                'If Not IsNothing(oResultTable) Then
                '    oResultTable.Dispose()
                '    oResultTable = Nothing
                'End If
            End Try
        End Function

        '''' <summary>
        '''' To Clean up the Document for removing FormFields and Tags that does n't contan data
        '''' </summary>
        '''' <remarks></remarks>
        Public Sub CleanupDoc()
            
            Dim objField As Wd.FormField
            Dim myHelptextString As String
            Dim myResultTextString As String
            Dim strtags As String()
            Dim cntCtrl As Wd.ContentControl

            For iobjField As Integer = _oCurDoc.FormFields.Count To 1 Step -1
                Try
                    objField = _oCurDoc.FormFields(iobjField)

                    If (Not IsNothing(objField)) Then

                        If objField.Type = Wd.WdFieldType.wdFieldFormTextInput Then

                            Try
                                objField.Range.HighlightColorIndex = Wd.WdColorIndex.wdNoHighlight
                            Catch ex As Exception

                            End Try

                            Try
                                myHelptextString = "|" & objField.HelpText.ToString() & "|"
                                myResultTextString = objField.Result

                                If myHelptextString = myResultTextString Then '' New 
                                    objField.Result = ""
                                    objField.Delete()
                                End If
                            Catch ex As Exception

                            End Try

                        End If
                    End If

                Catch ex As Exception

                End Try
            Next

            ''//To replace the special tags
            ReDim Preserve strtags(4)
            ''//To replace the special tags
            strtags(0) = "[]"
            strtags(1) = "[HPI]"
            strtags(2) = "[Xray]"
            strtags(3) = "[MRI]"
            strtags(4) = "[PLAN]"
            Dim dtcat As DataTable = FillTagsCategory()
            If (Not IsNothing(dtcat)) Then

                For Each dr As DataRow In dtcat.Rows
                    If (strtags.Contains(dr(0), StringComparer.OrdinalIgnoreCase) = False) Then

                        ReDim Preserve strtags(strtags.Length)
                        strtags(strtags.Length - 1) = dr(0).ToString()
                    End If
                Next

            End If

            For i As Int16 = 0 To strtags.Length

                Try

                    gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=_oCurDoc.Application, FindText:=CStr(strtags(i)).Trim, ReplaceWith:=" ", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceAll, MatchWildCards:=False, MatchWholeWord:=False)
                Catch ex2 As Exception

                End Try

            Next

            Array.Clear(strtags, 0, strtags.Length)
            strtags = Nothing
            If Not IsNothing(dtcat) Then
                dtcat.Rows.Clear()
                dtcat.Dispose()
                dtcat = Nothing
            End If

        
            For iCtrl As Integer = _oCurDoc.ContentControls.Count To 1 Step -1
                Try
                    cntCtrl = _oCurDoc.ContentControls(iCtrl)

                    If cntCtrl.Type = Wd.WdContentControlType.wdContentControlDropdownList Then
                        cntCtrl.Delete(False)
                    End If
                Catch ex As Exception

                End Try
            Next

        End Sub
        '' added for case CAS-07812-N2K3R9 
        Public Function FillTagsCategory() As System.Data.DataTable

            Dim oDB As New DataBaseLayer


            ''22-Mar-13 Aniket: Resolving Memory Leak Issues
            Dim oResultTable As DataTable = Nothing
            Try





                oResultTable = oDB.GetDataTable("gsp_getTagsCategory")
                If Not oResultTable Is Nothing Then
                    Return oResultTable
                End If
                Return Nothing
            Catch ex As Exception
                Return Nothing
            Finally

                If Not IsNothing(oDB) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If
            End Try
        End Function
        ''MatchCase parameter default set to false so that no need to pass it from all the screens
        Public Shared Function FindAndReplace(ByVal MyApp As Microsoft.Office.Interop.Word.Application, ByVal FindText As String, ByVal ReplaceWith As String, Optional ByVal Forward As Boolean = False, Optional ByVal Wrap As Integer = 1, Optional ByVal Replace As Integer = 2, Optional ByVal MatchWildCards As Boolean = False, Optional ByVal MatchWholeWord As Boolean = False, Optional ByVal Format As Boolean = False, Optional ByVal MatchCase As Object = False) As Boolean
            '  Refer below link, a bug from Microsoft..
            '  http://support2.microsoft.com/default.aspx?scid=kb;en-us;313104
            '  http://www.experts-exchange.com/Programming/Languages/C_Sharp/Q_26924442.html

            'Dim MatchCase As Object = True
            ' Dim MatchWholeWord As Object = True
            '   Dim MatchWildCards As Object = False
            Dim MatchSoundsLike As Object = False
            Dim nMatchAllWordForms As Object = False
            '   Dim Forward As Object = True
            '    Dim Format As Object = False
            Dim MatchKashilda As Object = False
            Dim MatchDiacritics As Object = False
            Dim MatchAlefHamza As Object = False
            Dim MatchControl As Object = False
            Dim [ReadOnly] As Object = False
            Dim Visible As Object = True
            '  Dim Replace As Object = 2
            ' Dim Wrap As Object = 1
            Dim Parameters As Object() = New Object() {FindText, MatchCase, MatchWholeWord, MatchWildCards, MatchSoundsLike, nMatchAllWordForms, _
             Forward, Wrap, Format, ReplaceWith, Replace, MatchKashilda, _
             MatchDiacritics, MatchAlefHamza, MatchControl}
            Return MyApp.Selection.Find.[GetType]().InvokeMember("Execute", System.Reflection.BindingFlags.InvokeMethod, Nothing, MyApp.Selection.Find, Parameters)

        End Function
        'Function getUniqueID() As String
        '    Static firstTime As Boolean = True
        '    Static myWatch As New Stopwatch()
        '    Static myTime As DateTime
        '    If firstTime = True Then
        '        firstTime = False
        '        myTime = Now()
        '        myWatch.Start()
        '    End If
        '    Dim TmSp As New TimeSpan(myTime.Ticks + myWatch.ElapsedTicks)
        '    getUniqueID = TmSp.Ticks.ToString()
        '    TmSp = Nothing

        'End Function
        Private Function FreeBigImageResources(ByRef big As BitmapImage) As Boolean
            If (IsNothing(big) = False) Then



                Try
                    If IsNothing(big.StreamSource) = False Then
                        big.StreamSource.Dispose()
                    End If

                Catch

                End Try

                big.UriSource = Nothing

                Try
                    If IsNothing(big.StreamSource) = False Then
                        big.StreamSource.Dispose()
                    End If

                Catch

                End Try


                Try
                    big.BeginInit()
                    big.UriSource = Nothing
                    big.EndInit()
                Catch
                End Try
                Try
                    If IsNothing(big.StreamSource) = False Then
                        big.StreamSource.Dispose()
                    End If

                Catch

                End Try

                '08-May-13 Aniket: Resolving Memory Leaks
                'big = New BitmapImage()
                'big.UriSource = Nothing

                big = Nothing
                Return True
            Else

                Return False
            End If

        End Function
        'Function ConvertImage(ByVal ImagePath As String, ByVal myWidth As Integer, ByVal myHeight As Integer, ByVal pgWidth As Integer, ByVal pgHeight As Integer, ByVal curPixel As Double, ByRef myFileName As String) As Double



        '    Dim big As New BitmapImage()
        '    Dim errorInLoading As Boolean = False
        '    Dim myScale As Double = 1.0

        '    Try
        '        big.BeginInit()

        '        big.CreateOptions = BitmapCreateOptions.IgnoreImageCache
        '        big.CacheOption = BitmapCacheOption.OnLoad

        '        big.UriSource = New Uri(ImagePath, UriKind.RelativeOrAbsolute)
        '        Dim myArea As Double

        '        Dim pgArea As Double

        '        myArea = CDbl(myWidth) * CDbl(myHeight)
        '        pgArea = CDbl(pgWidth) * CDbl(pgHeight)

        '        Dim horzScale As Double = CDbl(pgWidth) / CDbl(myWidth)
        '        Dim verScale As Double = CDbl(pgHeight) / CDbl(myHeight)
        '        If (myArea > pgArea) Then
        '            myScale = System.Math.Sqrt(pgArea / myArea)
        '        End If

        '        If (myScale > horzScale) Then
        '            myScale = horzScale
        '        End If
        '        If (myScale > verScale) Then
        '            myScale = verScale
        '        End If


        '        big.DecodePixelWidth = CInt(CDbl(myWidth) * myScale / curPixel)
        '        big.DecodePixelHeight = CInt(CDbl(myHeight) * myScale / curPixel)

        '        big.EndInit()

        '    Catch ex As Exception
        '        errorInLoading = True
        '    Finally

        '    End Try

        '    If (errorInLoading = False) Then

        '        Try
        '            Dim oDFileInfo As New FileInfo(ImagePath)
        '            Dim myDir As String = oDFileInfo.DirectoryName()
        '            Dim oFileName As String = gloGlobal.clsFileExtensions.NewDocumentName(myDir, ".bmp", "yyyyMMddHHmmssffff") 'myDir + "\" + getUniqueID() + ".bmp"
        '            Dim fs As New FileStream(oFileName, FileMode.Create)
        '            oDFileInfo = Nothing

        '            Dim Encoder As New BmpBitmapEncoder

        '            Encoder.Frames.Add(BitmapFrame.Create(big))

        '            Encoder.Save(fs)
        '            Try
        '                fs.Close()
        '                fs.Dispose()
        '                fs = Nothing
        '            Catch ex As Exception

        '            End Try

        '            Encoder = Nothing

        '            'Try
        '            '    big.StreamSource.Dispose()
        '            'Catch

        '            'End Try

        '            'big.UriSource = Nothing

        '            'Try
        '            '    big.StreamSource.Dispose()
        '            'Catch

        '            'End Try


        '            'Try
        '            '    big.BeginInit()
        '            '    big.UriSource = Nothing
        '            '    big.EndInit()
        '            'Catch
        '            'End Try
        '            'Try
        '            '    big.StreamSource.Dispose()
        '            'Catch

        '            'End Try

        '            ''08-May-13 Aniket: Resolving Memory Leaks
        '            ''big = New BitmapImage()
        '            ''big.UriSource = Nothing
        '            'big = Nothing
        '            myFileName = oFileName
        '            'Dim ofileInfo As New FileInfo(oFileName)

        '            'myFileName = myDir + "\" + getUniqueID() + ".TIF"
        '            'ofileInfo.CopyTo(myFileName, True)
        '            'ofileInfo = Nothing
        '            'Try

        '            '    If (File.Exists(oFileName)) Then
        '            '        File.Delete(oFileName)
        '            '    End If
        '            'Catch ex As Exception

        '            'End Try


        '        Catch ex As Exception
        '            ''small = Nothing
        '            'big.UriSource = Nothing

        '            'Try
        '            '    big.BeginInit()
        '            '    big.EndInit()
        '            'Catch
        '            'End Try

        '            'Try
        '            '    big.StreamSource.Dispose()
        '            'Catch

        '            'End Try

        '            ''08-May-13 Aniket: Resolving Memory Leaks
        '            ''big = New BitmapImage()
        '            ''big.UriSource = Nothing
        '            'big = Nothing
        '            myFileName = ""
        '            FreeBigImageResources(big)
        '            ConvertImage = myScale

        '        Finally
        '            FreeBigImageResources(big)
        '            ConvertImage = myScale
        '        End Try
        '    Else
        '        myFileName = ""
        '        FreeBigImageResources(big)
        '        ConvertImage = myScale
        '    End If

        'End Function
        Function ConvertImageToBitmap(ByVal ImagePath As String, ByVal myWidth As Integer, ByVal myHeight As Integer, ByVal pgWidth As Integer, ByVal pgHeight As Integer, ByVal curPixel As Double, ByRef dScale As Double) As MemoryStream


            ConvertImageToBitmap = Nothing
            Dim big As New BitmapImage()
            Dim errorInLoading As Boolean = False
            Dim myScale As Double = 1.0

            Try
                big.BeginInit()

                big.CreateOptions = BitmapCreateOptions.IgnoreImageCache
                big.CacheOption = BitmapCacheOption.OnLoad

                big.UriSource = New Uri(ImagePath, UriKind.RelativeOrAbsolute)
                Dim myArea As Double

                Dim pgArea As Double

                myArea = CDbl(myWidth) * CDbl(myHeight)
                pgArea = CDbl(pgWidth) * CDbl(pgHeight)

                Dim horzScale As Double = CDbl(pgWidth) / CDbl(myWidth)
                Dim verScale As Double = CDbl(pgHeight) / CDbl(myHeight)
                If (myArea > pgArea) Then
                    myScale = System.Math.Sqrt(pgArea / myArea)
                End If

                If (myScale > horzScale) Then
                    myScale = horzScale
                End If
                If (myScale > verScale) Then
                    myScale = verScale
                End If


                big.DecodePixelWidth = CInt(CDbl(myWidth) * myScale / curPixel)
                big.DecodePixelHeight = CInt(CDbl(myHeight) * myScale / curPixel)

                big.EndInit()

            Catch ex As Exception
                errorInLoading = True
            Finally

            End Try

            If (errorInLoading = False) Then

                Try
                    'Dim oDFileInfo As New FileInfo(ImagePath)
                    'Dim myDir As String = oDFileInfo.DirectoryName()
                    'Dim oFileName As String = gloGlobal.clsFileExtensions.NewDocumentName(myDir, ".bmp", "yyyyMMddHHmmssffff") 'myDir + "\" + getUniqueID() + ".bmp"
                    'oDFileInfo = Nothing

                    'Dim Encoder As New BmpBitmapEncoder

                    'Encoder.Frames.Add(BitmapFrame.Create(big))
                    'Dim fs As New FileStream(oFileName, FileMode.Create)
                    'Encoder.Save(fs)
                    'Try
                    '    fs.Close()
                    '    fs.Dispose()
                    '    fs = Nothing
                    'Catch ex As Exception

                    'End Try

                    'Encoder = Nothing

                    'Try
                    '    big.StreamSource.Dispose()
                    'Catch

                    'End Try

                    'big.UriSource = Nothing

                    'Try
                    '    big.StreamSource.Dispose()
                    'Catch

                    'End Try


                    'Try
                    '    big.BeginInit()
                    '    big.UriSource = Nothing
                    '    big.EndInit()
                    'Catch
                    'End Try
                    'Try
                    '    big.StreamSource.Dispose()
                    'Catch

                    'End Try

                    ''08-May-13 Aniket: Resolving Memory Leaks
                    ''big = New BitmapImage()
                    ''big.UriSource = Nothing
                    'big = Nothing
                    Dim outStream As MemoryStream = New MemoryStream()
                    Dim enc As BitmapEncoder = New BmpBitmapEncoder()
                    enc.Frames.Add(BitmapFrame.Create(big))
                    enc.Save(outStream)
                    outStream.Flush()
                    ConvertImageToBitmap = outStream
                    enc = Nothing


                    'myFileName = oFileName
                    'Dim ofileInfo As New FileInfo(oFileName)

                    'myFileName = myDir + "\" + getUniqueID() + ".TIF"
                    'ofileInfo.CopyTo(myFileName, True)
                    'ofileInfo = Nothing
                    'Try

                    '    If (File.Exists(oFileName)) Then
                    '        File.Delete(oFileName)
                    '    End If
                    'Catch ex As Exception

                    'End Try


                Catch ex As Exception
                    ''small = Nothing
                    'big.UriSource = Nothing

                    'Try
                    '    big.BeginInit()
                    '    big.EndInit()
                    'Catch
                    'End Try

                    'Try
                    '    big.StreamSource.Dispose()
                    'Catch

                    'End Try

                    ''08-May-13 Aniket: Resolving Memory Leaks
                    ''big = New BitmapImage()
                    ''big.UriSource = Nothing
                    'big = Nothing
                    'myFileName = ""
                    FreeBigImageResources(big)
                    dScale = myScale

                Finally
                    FreeBigImageResources(big)
                    dScale = myScale
                End Try
            Else
                'myFileName = ""
                FreeBigImageResources(big)
                dScale = myScale
            End If

        End Function

        ''' 
        Public Function GetandSetMyFirstFlag(ByVal GetOrSet As Boolean, ByVal Assign As Boolean) As Boolean
            Static myFlag As Boolean = True
            If (GetOrSet) Then
                myFlag = Assign
                Return myFlag
            Else
                GetandSetMyFirstFlag = myFlag
            End If
        End Function
        ''' <summary>
        ''' To Insert Image into the selected Template using Clipboard method
        ''' </summary>
        ''' <param name="ImagePath"></param>
        ''' <remarks> 20090604 </remarks>
        ''' 
        Public Sub InsertImage(ByVal ImagePath As String)

            Dim ClipBoardWatcher As gloGlobal.gloClipboardWatcher = Nothing

            Try

                'Aniket: This method is used for Local Signature Pad
                If ImagePath.Contains("SignatureCreated.txt") = True AndAlso gblnLocalSignaturePad = True Then

                    Dim strException As String = ""
                    Dim dataObject As DataObject = Nothing

                    Try
                        dataObject = Global.gloWord.gloWord.GetClipBoardDataWithRetry(5, strException)

                        If (IsNothing(dataObject) AndAlso (String.IsNullOrEmpty(strException) = False)) Then
                            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, strException, gloAuditTrail.ActivityOutCome.Failure)
                        End If

                    Catch ex1 As Exception

                    End Try

                    _oCurDoc.Application.Selection.Paste()

                ElseIf File.Exists(ImagePath) = True Then
                    Dim big As New BitmapImage()
                    Try
                        big.BeginInit()

                        big.CreateOptions = BitmapCreateOptions.IgnoreImageCache
                        big.CacheOption = BitmapCacheOption.OnLoad

                        big.UriSource = New Uri(ImagePath, UriKind.RelativeOrAbsolute)
                        big.EndInit()


                        Dim myWidth As Integer = big.PixelWidth
                        Dim myHeight As Integer = big.PixelHeight

                        FreeBigImageResources(big)

                        'Dim wordPageWidth As Integer = CurDocument.PageSetup.PageWidth
                        'Dim wordPageHeight As Integer = CurDocument.PageSetup.PageHeight
                        'SLR: It is slowing down, and hence changed
                        'Dim aCopyofFile As String = LoadAndCloseWord.GetACopy(CurDocument, CurDocument.Application, gloSettings.FolderSettings.AppTempFolderPath)
                        'Dim PictureCount As Integer = gloGlobal.BitmapConverter.GetImagesCount(aCopyofFile)

                        Dim wordPageWidth As Integer = 0

                        Try
                            wordPageWidth = CurDocument.PageSetup.PageWidth
                        Catch ex As Exception
                        End Try

                        Dim wordPageHeight As Integer = 0

                        Try
                            wordPageHeight = CurDocument.PageSetup.PageHeight
                        Catch ex As Exception
                        End Try


                        If (wordPageWidth <= 0) Then
                            wordPageWidth = 8 * 1440
                        End If
                        If (wordPageHeight <= 0) Then
                            wordPageHeight = 11 * 1440
                        End If
                        Dim mySucessDPI As Integer = 0
                        Dim SuccessCopied As Boolean = False
                        'ClipBoardWatcher = New gloGlobal.gloClipboardWatcher()
                        ClipBoardWatcher = gloGlobal.gloProgressAndClipboard.getClipboardWatcher()
                        AddHandler ClipBoardWatcher.OnClipboardContentChanged, AddressOf ClipBoardChanged
                        ClipBoardWatcher.Start()

                        Do While (SuccessCopied = False)
                            If ((mySucessDPI = 1) AndAlso (GetandSetMyFirstFlag(False, False) = True)) Then
                                If (MessageBox.Show("Image resolution will be reduced during insertion due to insufficient memory. Do you want to proceed?", gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) = DialogResult.Cancel) Then
                                    Return
                                Else
                                    GetandSetMyFirstFlag(True, False)
                                End If
                            End If
                            'End If
                            Dim myFileName As String = ""
                            Dim myScale As Double = 1.0
                            Dim thisImage As Bitmap = Nothing
                            Dim thisImageStream As MemoryStream = Nothing
                            Dim firstTimeTogetClip As Boolean = True
                            Dim gotClip As Boolean = False

                            If (mySucessDPI <> 0) Then
                                If (mySucessDPI = myWidth) Then
                                    MessageBox.Show("Unable to Insert even after reducing resolution due to insufficient memory", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                    Return
                                Else
                                    thisImageStream = ConvertImageToBitmap(ImagePath, myWidth, myHeight, wordPageWidth, wordPageHeight, CDbl(mySucessDPI), myScale)
                                End If

                            Else
                                'SLR: Changed the code to copy for clipboard as bitmap instead of original image: otherwise word is crashing in 2013
                                thisImageStream = ConvertImageToBitmap(ImagePath, myWidth, myHeight, wordPageWidth, wordPageHeight, CDbl(1), myScale)
                            End If
                            If (IsNothing(thisImageStream) = False) Then
                                thisImage = New Bitmap(thisImageStream)
                                If (IsNothing(thisImage) = False) Then
                                    CurDocument.ActiveWindow.SetFocus()
                                    Dim yesCopied As Boolean = True
                                    Dim myStart As Integer = 0
                                    Dim myEnd As Integer = 0
                                    Try

                                        If (mySucessDPI = 0) Then
                                            If (firstTimeTogetClip) Then
                                                Try
                                                    Dim strEx As String = ""
                                                    gotClip = Global.gloWord.gloWord.GetClipBoardWithRetry(5, strEx)

                                                Catch ex As Exception
                                                Finally
                                                    firstTimeTogetClip = False
                                                End Try
                                            End If

                                        End If
                                        Try
                                            System.Threading.Thread.Sleep(100)
                                            Try
                                                Clipboard.SetImage(thisImage)
                                                bClipChanged = False
                                            Catch ex As Exception
                                                yesCopied = False
                                            End Try
                                            If (yesCopied) Then
                                                myStart = CurDocument.ActiveWindow.Selection.Range.Start
                                                With CurDocument.ActiveWindow.Selection
                                                    If (bClipChanged = True) Then
                                                        System.Threading.Thread.Sleep(100)
                                                        Try
                                                            Clipboard.SetImage(thisImage)
                                                            bClipChanged = False
                                                        Catch ex As Exception
                                                            yesCopied = False
                                                        End Try
                                                    End If
                                                    If (bClipChanged = False) Then
                                                        Try
                                                            .Paste()
                                                        Catch ex As Exception
                                                            yesCopied = False
                                                            _IsImageCopied = True
                                                        End Try
                                                    Else
                                                        yesCopied = False
                                                        _IsImageCopied = True
                                                    End If

                                                End With
                                            End If


                                        Catch ex As Exception
                                            yesCopied = False
                                            _IsImageCopied = True
                                        End Try
                                        If (yesCopied) Then

                                            Try

                                                myEnd = CurDocument.ActiveWindow.Selection.Range.End
                                                Dim ShapeCounter As Long = 0

                                                CurDocument.ActiveWindow.Selection.SetRange(myStart, myEnd)

                                                Try
                                                    ShapeCounter = CurDocument.ActiveWindow.Selection.InlineShapes.Count

                                                Catch ex As Exception
                                                    ShapeCounter = 0
                                                End Try


                                                If (ShapeCounter > 0) Then
                                                    With CurDocument.ActiveWindow.Selection

                                                        Try
                                                            .Cut()
                                                            bClipChanged = False
                                                            Try
                                                                If (bClipChanged = False) Then
                                                                    .PasteSpecial(DataType:=15)
                                                                Else
                                                                    yesCopied = False
                                                                    _IsImageCopied = True
                                                                End If

                                                            Catch ex As System.Runtime.InteropServices.COMException
                                                                Try
                                                                    If (bClipChanged = False) Then
                                                                        .Paste()
                                                                        '07-Jun-16 Aniket: Resolving Incident #00062239
                                                                        'LoadAndCloseWord.RefreshWord(CurDocument, CurDocument.Application)
                                                                    Else
                                                                        yesCopied = False
                                                                        _IsImageCopied = True
                                                                    End If

                                                                Catch
                                                                End Try
                                                            End Try

                                                            If (.Tables.Count = 0) Then

                                                                .Move(1)
                                                            Else

                                                                .EndKey()
                                                            End If

                                                        Catch ex As Exception

                                                        End Try

                                                    End With
                                                End If

                                            Catch ex As Exception
                                                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                            End Try
                                            If (gotClip) Then
                                                Try
                                                    Global.gloWord.gloWord.SetClipboardData()
                                                Catch ex As Exception

                                                End Try

                                            End If
                                            SuccessCopied = True

                                        End If


                                    Catch e As Exception
                                    End Try

                                    If (IsNothing(thisImage) = False) Then
                                        thisImage.Dispose()
                                        thisImage = Nothing
                                    End If

                                End If
                                If (IsNothing(thisImageStream) = False) Then
                                    thisImageStream.Dispose()
                                    thisImageStream = Nothing
                                End If
                            End If
                            mySucessDPI += 1

                        Loop
                    Catch ex As Exception
                        FreeBigImageResources(big)
                    Finally
                        FreeBigImageResources(big)
                    End Try

                End If
            Catch ex As Exception
                Throw ex
            Finally
                If (IsNothing(ClipBoardWatcher) = False) Then
                    Try
                        RemoveHandler ClipBoardWatcher.OnClipboardContentChanged, AddressOf ClipBoardChanged
                    Catch ex As Exception

                    End Try
                    'ClipBoardWatcher.Stop()
                    'ClipBoardWatcher.Dispose()
                    'ClipBoardWatcher = Nothing
                End If

            End Try
        End Sub

        Private Shared bClipChanged As Boolean = False
        Public Sub ClipBoardChanged(Sender As Object, e As EventArgs)
            bClipChanged = True
        End Sub


#Region "Function for functionality Liquid data "


        Public Sub GetdataFromOtherForms(ByVal _DocType As gloEMRWord.enumDocType)
            CType(myCallingForm, IWord).GetdataFromOtherForms(_DocType)
        End Sub

        Private Sub TurnOffMicrophone()
            CType(myCallingForm, IWord).TurnOffMicrophone()
        End Sub

        ''' <summary>
        '''           Function called ShowMicrophone function of myCallingForm 
        ''' <para >
        '''           This function calling is equavalant to following code statement.
        ''' </para>
        ''' <example >
        '''            myCallingForm.ShowMicrophone()
        ''' </example>
        ''' <para >
        '''           But compile time we did't know which form is calling form thats why myCallingForm is typecasted to Interface  IWord
        ''' </para>
        ''' </summary>
        ''' <remarks>added by dipak 20090919</remarks>
        Private Sub ShowMicrophone()
            CType(myCallingForm, IWord).ShowMicrophone()
        End Sub

        ''' <summary>
        ''' Check for Condiotion for is document is procted or controls selected is not  FormFieldControl
        ''' </summary>
        ''' <param name="eCtrlType"></param>
        ''' <returns>boolean value (true-Feasible and false=Not Feasible)</returns>
        ''' <remarks>added by dipak 20090919</remarks>
        Private Function IsFeasible(ByVal eCtrlType As enumControls) As Boolean
            Try
                If _oCurDoc.ProtectionType <> Wd.WdProtectionType.wdNoProtection Then
                    MessageBox.Show("Current operation is invalid as document is under protection mode.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return False
                End If
                Dim oRng As Wd.Range
                If Not _oCurDoc.ActiveWindow.Selection.Range Is Nothing Then
                    oRng = _oCurDoc.ActiveWindow.Selection.Range
                End If
                If _oCurDoc.ActiveWindow.Selection.ShapeRange.Count > 0 Then
                    If eCtrlType = enumControls.FormFieldControl Then
                        MessageBox.Show("Checkboxes and drop down lists cannot be used inside text boxes or other shapes.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return False
                    End If
                End If
                If Not _oCurDoc.ActiveWindow.Selection.HeaderFooter Is Nothing Then
                    'If _oCurDoc.ActiveWindow.Selection.HeaderFooter.IsHeader Then
                    If eCtrlType = enumControls.FormFieldControl OrElse eCtrlType = enumControls.ContentControl Then
                        MessageBox.Show("Checkboxes and drop down lists cannot be used inside text boxes or other shapes.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return False
                    End If
                    'End If
                End If
                Return True
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Procedure is a event procedure(handles double click event of word document) for implement liquid link for forms where word document used
        ''' </summary>
        ''' <param name="Sel">passes value of selected field/fields in word document </param>
        ''' <param name="Cancel"></param>
        ''' <remarks> Ref: Patien Exam </remarks>
        Public Sub OnFormClicked(ByVal Sel As Wd.Selection, ByRef Cancel As Boolean)
            Try
                '' SUDHIR 20091202 '' TO HANDLE EMR-PM WORD INSTANCE ''            
                'If gstrOpenDocument <> GetActiveDocumentName() Then
                '    Exit Sub
                'End If
                '' END SUDHIR '' 
                'CODE ADDED BY DIPAK 20091224 ''' TO HANDLE EMR-PM WORD INSTANCE ''
                Dim activeDocumentName As String = GetActiveDocumentName()
                If (String.IsNullOrEmpty(activeDocumentName)) Then
                    Exit Sub
                End If
                If Not garrOpenDocument.Contains(activeDocumentName) Then
                    Exit Sub
                End If
                'END CODE ADDED BY DIPAK 
                If frmPatientEducation.Formopen_Info = True Then
                    Exit Sub
                End If

                'If tmrDocProtect.Enabled = False Then '' Protected conditon 
                ' _oCurDoc = 
                Cancel = False
                Dim r As Wd.Range = Nothing
                Try
                    r = Sel.Range
                Catch ex As Exception

                End Try
                If (IsNothing(r)) Then
                    Exit Sub
                End If
                Try
                    r.SetRange(Sel.Start, Sel.End + 1)
                Catch ex As Exception

                End Try
                If (IsNothing(r)) Then
                    Exit Sub
                End If
                If r.FormFields IsNot Nothing AndAlso r.FormFields.Count >= 1 Then
                    Dim om As Object = System.Reflection.Missing.Value
                    Dim f As Wd.FormField
                    f = r.FormFields.Item(1)
                    If (IsNothing(f) = False) Then
                        If f.Type = Wd.WdFieldType.wdFieldFormTextInput Then
                            FieldValue = f.StatusText
                            ''To implement liquid in same thread context
                            Dim thrdtooltip As Threading.Thread = New Threading.Thread(AddressOf AccessControl)
                            thrdtooltip.IsBackground = True
                            thrdtooltip.Start()
                            '    AccessControl()
                            Cancel = True
                        Else
                            If f.Type = Wd.WdFieldType.wdFieldFormDropDown Then
                                Cancel = True
                            Else
                                If f.Type = Wd.WdFieldType.wdFieldFormCheckBox Then
                                    Cancel = True
                                End If
                            End If
                        End If
                    End If
                End If
                'if clicked on InlineShape i.e.any drawing object
                If Sel.Type = Wd.WdSelectionType.wdSelectionInlineShape Then
                    Dim r1 As Wd.Range = Nothing
                    Try
                        r1 = Sel.Range
                    Catch ex As Exception

                    End Try
                    If (IsNothing(r1)) Then
                        Exit Sub
                    End If
                    Try

                        If (Sel.Start > 3) Then
                            r1.SetRange(Sel.Start - 3, Sel.End + 1)
                        Else
                            r1.SetRange(1, Sel.End + 1)
                        End If
                    Catch ex As Exception

                    End Try
                    If (IsNothing(r1)) Then
                        Exit Sub
                    End If

                    'Dim r1 As Wd.Range = Sel.Range
                    'r1.SetRange(Sel.Start - 3, Sel.End + 1)
                    ''Changes to Resolve Incident#00037454 
                    Dim isDas As Boolean = False
                    For Each fd As Wd.FormField In r1.FormFields
                        If fd.Result = "DAS" Then
                            isDas = True
                        End If
                    Next
                    If r1.FormFields IsNot Nothing AndAlso r1.FormFields.Count >= 1 Then
                        If isDas Then
                            Sel.Select()
                            Sel.Delete()
                            Sel.Collapse()
                            FieldValue = "DAS.iDASImage"
                            AccessControl()
                            Sel.MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=1)
                            Sel.MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=1)
                        Else
                            If IsFeasible(enumControls.None) = False Then
                                Exit Sub
                            End If
                            Sel.Select()
                            Sel.Copy()
                            AccessDrawingPad()
                            Cancel = True
                        End If
                    Else
                        If IsFeasible(enumControls.None) = False Then
                            Exit Sub
                        End If
                        Sel.Select()
                        Sel.Copy()
                        AccessDrawingPad()
                        Cancel = True
                    End If
                    r1 = Nothing
                End If

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
        End Sub

        ''' <summary>
        ''' To open the form under same thread as we are opening the form using com object click events
        ''' </summary>
        ''' <remarks>added by dipak 20090919
        ''' </remarks>
        Private Sub AccessControl()
            If Not IsNothing(myCallingForm) Then
                If myCallingForm.InvokeRequired Then
                    myCallingForm.BeginInvoke(New MethodInvoker(AddressOf AccessControl))
                Else
                    OpenLink(FieldValue)
                End If
            End If
        End Sub

        ''' <summary>
        ''' To open the Drawing pad under same thread as we are opening the form using com object click events
        ''' </summary>
        ''' <remarks>added by dipak 20090919</remarks>
        Private Sub AccessDrawingPad()
            If Not IsNothing(myCallingForm) Then
                If myCallingForm.InvokeRequired Then
                    myCallingForm.Invoke(New MethodInvoker(AddressOf AccessDrawingPad))
                Else
                    OpenDrawingPad()
                End If
            End If
        End Sub

        ''' <summary>
        ''' procedure show the Drawing pad for editing Image(i.e. InlineShape)
        ''' </summary>
        ''' <param name="blnNew"></param>
        ''' <remarks>Added by dipak 20090919 </remarks>
        Public Sub OpenDrawingPad(Optional ByVal blnNew As Boolean = False)
            Dim frm As New frmDrawingPad()
            Dim Img As Image = Nothing
            Try
                TurnOffMicrophone()
                If blnNew = False Then
                    Try
                        If Clipboard.ContainsImage Then
                            Img = Clipboard.GetImage()
                        End If

                    Catch ex As Exception
                        MessageBox.Show("Opening drawingpad unable to get image from Clipboard due to locked by " & gloWord.gloWord.GetOpenClipboardWindowText(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                    End Try

                    If Not Img Is Nothing Then
                        'Clipboard.Clear()
                        'variable for storing temp file name as DrawingImage+MMddyyyyhhmmssmmmtt_.jpg format
                        Dim strfilename As String = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".jpg", "MMddyyyyHHmmssffff") ' "DrawingImage" & Format(DateTime.Now, "MMddyyyyHHmmssffff") & ".jpg"
                        Img.Save(strfilename, System.Drawing.Imaging.ImageFormat.Jpeg)
                        frm.DrawingImagePath = strfilename '' strfilename
                    Else
                        If Not IsNothing(frm) Then
                            frm.Close() 'Change made to solve memory Leak and word crash issue
                            frm.Dispose()
                            frm = Nothing
                        End If
                        Exit Sub
                    End If
                End If
                frm.ShowDialog(frm.Parent)
                If frm.blnInsertStatus Then
                    GetDrawingImage()
                End If

                If Not IsNothing(frm) Then
                    frm.Close() 'Change made to solve memory Leak and word crash issue
                    frm.Dispose()
                    frm = Nothing
                End If
                If Not IsNothing(Img) Then
                    Img.Dispose()
                    Img = Nothing
                End If

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Finally
                If Not IsNothing(frm) Then
                    frm.Dispose()
                    frm = Nothing
                End If
            End Try
        End Sub

        ''' <summary>
        ''' To open the concerned form on Formfield value clicked
        ''' </summary>
        ''' <param name="strFormFieldResult"></param>
        ''' <remarks></remarks>
        Public Sub OpenLink(ByVal strFormFieldResult As String)
            Dim strTable = Split(strFormFieldResult, ".")
            'code commented by dipak 20100205 as case for chief complaint  table implemented
            'If strTable(1) = _ChiefComplaints Then
            '    GetCheifComplaints()
            'Else
            If (strFormFieldResult = "MedicalCategory_mst.sMedicalCategory") Then
                strTable(0) = _PatientDemographics
            End If
            Select Case strTable(0)
                'case for chief complaint implemented by dipak 20100205
                Case _ChiefComplaints
                    GetCheifComplaints()
                    'end dipak 20100205

                    'case commented and modify by dipak 20091107 to fix bug of pharmacy liquid link not working.
                    'Case _PatientDemographics, _Contacts
                    '21-May-15 Aniket: Open Modify Patient on Patient Insurance Liquid Link
                Case _PatientDemographics, _Contacts, _Patient_DTL, _PatientInsurance_DTL
                    'opens form for modify patient demographis information i.e. Patient registration form
                    GetDemographics()
                Case _Prescription
                    'opens prescription form for modify prescription and reflect changes on word document
                    GetPrescription()
                Case _Medication
                    GetMedication()

                    '04-May-15 Aniket: Bug #80986 ( Modified): gloEMR: OB vitals liquid link- Application does not allow user quadruple click on OB vitals liquid link
                    '14-May-15 Aniket: Bug #83286: EMR: Antepartum Template- In anterpartum template OB viatls table 

                Case _OBVitals
                    GetVitalsForOB()
                Case _Vitals
                    GetVitals()
                Case _VitalsDAS
                    GetVitalsForDAS()
                Case _History, _Narration, _OBGeneticHistory, _OBInfectionHistory, _OBInitialPhysicalExamination, _OBMedicalHistory
                    GetHistory()

                Case _OBPlan
                    GetOBPlan()

                Case _ProblemList
                    GetProblemList()
                Case _RadiologyOrder
                    GetRadiologyOrders()
                Case _LabOrder
                    GetLabOrders()
                Case _ROS
                    GetROS()
                Case _Flowsheet
                    InsertFlowSheet(strFormFieldResult)
                Case _Tasks
                    GetTasks()
                    '05-May-15 Aniket: Implementation of Past Pregnancies Liquid Link
                Case _PastPregnancies
                    ShowPastPregnancies()
                Case "patient_clinicalInstruction"
                    Dim frm As New frmPatientClinicalInstruction(nPatientID, Date.Now)
                    frm.ShowDialog(frm.Parent)
                    If Not IsNothing(frm) Then
                        frm.Dispose()
                        frm = Nothing
                    End If
                    GetdataFromOtherForms(enumDocType.None)
                Case "Patient_CarePlan"
                    Dim frm As New frmPatientCarePlan(nPatientID, 0)
                    frm.ShowDialog(frm.Parent)
                    If Not IsNothing(frm) Then
                        frm.Dispose()
                        frm = Nothing
                    End If
                    GetdataFromOtherForms(enumDocType.None)
                Case "PatientConsentTracking"
                    Dim frm As New frmPatientConsentTracking(nPatientID, 0, True)
                    frm.Text = "Modify Consent Tracking"
                    frm.KeyPreview = True
                    frm.ShowInTaskbar = False
                    frm.StartPosition = FormStartPosition.CenterParent
                    frm.ShowDialog(frm.Parent)
                    frm.Dispose()
                    frm = Nothing
                    GetdataFromOtherForms(enumDocType.None)
                Case Else

            End Select
            'End If

        End Sub

        '05-May-15 Aniket: Implementation of Past Pregnancies Liquid Link
        Private Sub ShowPastPregnancies()
            Try
                Using ofrmPastPregnancies As New frmPastPregnancies(nPatientID)
                    ofrmPastPregnancies.myCaller1 = Me.myCallingForm
                    ofrmPastPregnancies.StartPosition = FormStartPosition.CenterParent
                    ofrmPastPregnancies.ShowDialog(Me.myCallingForm)
                    ofrmPastPregnancies.Close()
                    ofrmPastPregnancies.Dispose()
                End Using

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                ex = Nothing
            End Try
        End Sub

        ''' <summary>
        ''' Get Task : open task form for addeing a new task
        ''' </summary>
        ''' <remarks>added by dipak 20090924</remarks>
        Private Sub GetTasks()
            Dim ofrmTask As gloTaskMail.frmTask
            'line commented used in Patient exam.
            'ofrmTask = New gloTaskMail.frmTask(GetConnectionString,0, examid, 0)
            'as we don't pass exam id pass GetConnectionString and task id 0
            ofrmTask = New gloTaskMail.frmTask(GetConnectionString, 0)
            ofrmTask.IsEMREnable = True
            ofrmTask.PatientID = nPatientID
            ofrmTask.ShowDialog(ofrmTask.Parent)
            ofrmTask.Close()    'Change made to solve memory Leak and word crash issue
            ofrmTask.Dispose()
            ofrmTask = Nothing
            GetdataFromOtherForms(enumDocType.Tasks)
        End Sub
        ''' <summary>
        ''' InsertFlowSheet:Opens frmPatientFlowSheet form to make entries in FlowSheet
        ''' </summary>
        ''' <param name="flowSheetName"></param>
        ''' <remarks>Added by dipak 20090924</remarks>
        Public Sub InsertFlowSheet(Optional ByVal flowSheetName As String = "")
            Try
                '' SUDHIR 20090522 '' TO OPEN FLOWSHEET ''
                If flowSheetName <> "" Then
                    Dim strFlowSheet() As String = Split(flowSheetName, "|")
                    If strFlowSheet.Length > 1 Then
                        flowSheetName = strFlowSheet(1)
                    Else
                        flowSheetName = ""
                    End If
                End If

                ''Bug : 00000828: Record locking
                Dim f As New frmPatientFlowSheet(_DocumentCriteria.PatientID, flowSheetName)
                If f.FormLevelLock() Then
                    Call TurnOffMicrophone()
                    f.ShowDialog(f.Parent)
                    f.Close() 'Change made to solve memory Leak and word crash issue
                    f.Dispose()
                    f = Nothing
                    GetdataFromOtherForms(enumDocType.Flowsheet)
                    Call ShowMicrophone()
                Else
                    f.Dispose()
                    f = Nothing
                End If

            Catch objErr As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.Add, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' Opens vital form modification
        ''' </summary>
        ''' <remarks>added by dipak 20090919</remarks>
        Public Sub GetVitals()
            If Trim(strPatientFirstName) <> "" Then
                If mgnVisitID = 0 Then
                    mgnVisitID = GenerateVisitID(nPatientID)
                End If
                Dim frmPatientVitals As New frmPatientVitals(mgnVisitID, dtDOS, nPatientID)
                Call ShowMicrophone()
                With frmPatientVitals
                    '.blnOpenFromExam = True
                    .myCaller1 = Me.myCallingForm
                    .StartPosition = FormStartPosition.CenterParent
                    .ShowDialog(Me.myCallingForm)
                End With
                frmPatientVitals.Close()    'Change made to solve memory Leak and word crash issue 
                frmPatientVitals.Dispose()
                frmPatientVitals = Nothing
                Call ShowMicrophone()
            Else
                MessageBox.Show("Please select the Patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End Sub

        '14-May-15 Aniket: Bug #83286: EMR: Antepartum Template- In anterpartum template OB viatls table 
        Private Function GetCurrentOBVitalID(ByVal mgnVisitID As Long, ByVal dtDOS As Date, ByVal m_PatientID As Long) As Long

            Dim Con As SqlConnection = Nothing
            Dim cmd As SqlCommand = Nothing
            Dim VitalID As Int64 = 0
            Dim objParam As SqlParameter

            Try
                Con = New SqlConnection(GetConnectionString())
                cmd = New SqlCommand("gsp_GetCurrentOBVitalID", Con)

                objParam = cmd.Parameters.Add("@VisitID", SqlDbType.BigInt, 18)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = mgnVisitID

                objParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt, 18)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = m_PatientID

                cmd.CommandType = CommandType.StoredProcedure

                If Con.State <> ConnectionState.Open Then
                    Con.Open()
                End If


                VitalID = cmd.ExecuteScalar
                Con.Close()

                If IsDBNull(VitalID) = True Then
                    VitalID = 0
                End If

                Return VitalID
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
                Return Nothing
            Finally
                If IsNothing(Con) = False Then
                    Con.Dispose() : Con = Nothing
                End If
                If IsNothing(cmd) = False Then
                    cmd.Parameters.Clear()
                    cmd.Dispose() : cmd = Nothing
                End If
                objParam = Nothing
            End Try
        End Function

        Public Sub GetVitalsForOB()

            Dim frmPatientVitalsForm As frmPatientVitals
            Dim nVitalID As Int64

            Try


                'Sanjog - Added on 2011 Sept 17 to show DAS form 
                If Trim(strPatientFirstName) <> "" Then

                    If mgnVisitID = 0 Then
                        mgnVisitID = GenerateVisitID(nPatientID)
                    End If

                    nVitalID = GetCurrentOBVitalID(mgnVisitID, dtDOS, nPatientID)

                    If nVitalID > 0 Then
                        frmPatientVitalsForm = New frmPatientVitals(nVitalID, nPatientID, False, False)
                    Else
                        frmPatientVitalsForm = New frmPatientVitals(mgnVisitID, dtDOS, nPatientID)
                    End If

                    Call TurnOffMicrophone()

                    With frmPatientVitalsForm
                        .blnOpenFromExam = True

                        .myCaller1 = Me.myCallingForm

                        .StartPosition = FormStartPosition.CenterParent
                        .ShowDialog(Me.myCallingForm)
                        .Dispose()
                    End With

                    frmPatientVitalsForm = Nothing

                    Call ShowMicrophone()
                Else
                    MessageBox.Show("Please select the Patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                ex = Nothing
            End Try

        End Sub

        Public Sub GetVitalsForDAS()
            'Change made to solve memory Leak and word crash issue
            If Trim(strPatientFirstName) <> "" Then
                If mgnVisitID = 0 Then
                    mgnVisitID = GenerateVisitID(nPatientID)
                End If
                Dim nVitalID As Int64
                nVitalID = GetCurrentVitalID(mgnVisitID, dtDOS, nPatientID)
                Call TurnOffMicrophone()
                If nVitalID > 0 Then
                    Dim frmPatientVitalsForm As New frmPatientVitals(nVitalID, nVitalID, False, False)
                    With frmPatientVitalsForm
                        .blnOpenFromExam = False
                        frmPatientVitals.blnOpenFromExamForDAS = True
                        .myCaller1 = Me.myCallingForm
                        .StartPosition = FormStartPosition.CenterParent
                        .ShowDialog(Me.myCallingForm)
                        'Change made to solve memory Leak and word crash issue
                        .Close()
                        frmPatientVitals.blnOpenFromExamForDAS = False
                        .Dispose()

                    End With
                    frmPatientVitalsForm = Nothing 'Change made to solve memory Leak and word crash issue                    
                Else
                    Dim frmPatientVitalsForm As New frmPatientVitals(mgnVisitID, dtDOS, nPatientID)
                    With frmPatientVitalsForm
                        .blnOpenFromExam = False
                        frmPatientVitals.blnOpenFromExamForDAS = True
                        .myCaller1 = Me.myCallingForm
                        .StartPosition = FormStartPosition.CenterParent
                        .ShowDialog(Me.myCallingForm)
                        'Change made to solve memory Leak and word crash issue
                        .Close()
                        frmPatientVitals.blnOpenFromExamForDAS = False
                        .Dispose()

                    End With
                    frmPatientVitalsForm = Nothing 'Change made to solve memory Leak and word crash issue
                End If

                Call ShowMicrophone()
            Else
                MessageBox.Show("Please select the Patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End Sub
        Private Function GetCurrentVitalID(ByVal mgnVisitID As Long, ByVal dtDOS As Date, ByVal m_PatientID As Long) As Long
            Dim Con As SqlConnection
            Dim cmd As SqlCommand
            Dim VitalID As Int64 = 0
            Dim objParam As SqlParameter
            Try
                Con = New SqlConnection(GetConnectionString())
                cmd = New SqlCommand("gsp_GetCurrentVitalID", Con)

                objParam = cmd.Parameters.Add("@VisitID", SqlDbType.BigInt, 18)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = mgnVisitID

                objParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt, 18)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = m_PatientID

                cmd.CommandType = CommandType.StoredProcedure

                If Con.State <> ConnectionState.Open Then
                    Con.Open()
                End If
                'Dim VisitID As Long
                VitalID = cmd.ExecuteScalar
                'Change made to solve memory Leak and word crash issue
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
                Con.Close()
                Con.Dispose()
                Con = Nothing
                If IsDBNull(VitalID) = True Then
                    '' If VisitId is Not Found then Return 0
                    VitalID = 0
                End If

                Return VitalID
            Catch ex As Exception
                Return Nothing
            Finally
                objParam = Nothing
            End Try
        End Function
        ''' <summary>
        ''' delete selected image/any object which can open in drawing pad as image from word document and past edited image in drawing pad 
        ''' </summary>
        ''' <remarks>added by dipak 20090919</remarks>
        Private Sub GetDrawingImage()
            If Not _oCurDoc Is Nothing Then
                Try
                    If Clipboard.ContainsImage Then
                        _oCurDoc.ActiveWindow.Selection.Delete()
                        _oCurDoc.ActiveWindow.Selection.Paste()
                        'Clipboard.Clear()
                    End If
                Catch ex As Exception
                    MessageBox.Show("Unable to get drawing image from Clipboard due to locked by " & gloWord.gloWord.GetOpenClipboardWindowText(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                End Try

            End If
        End Sub

        ''' <summary>
        ''' GetCheifComplaints :opens patient CheifComplaints form i.e.frmPatientInjuryDate
        ''' </summary>
        ''' <remarks></remarks>
        ''' 
        Private Sub GetCheifComplaints()
            Try
                Dim frm As New frmPatientInjuryDate(nPatientID)
                Call TurnOffMicrophone()
                'frm.Text = "Chief Complaints/Injury/Surgery Date for " & strPatientFirstName & " " & strPatientLastName
                frm.Text = "Chief Complaints/Injury/Surgery Date for " '& strPatientFirstName & " " & strPatientLastName
                frm.ShowDialog(frm.Parent)
                frm.Close() 'Change made to solve memory Leak and word crash issue
                frm.Dispose()
                frm = Nothing
                Call ShowMicrophone()
                GetdataFromOtherForms(enumDocType.CheifComplaints)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
        End Sub

        ''' <summary>
        ''' GetDemographics :Function open Patient for modify patient demographics information for modify.
        ''' </summary>
        ''' <remarks>added by dipak 20090919 :</remarks>
        ''' 
        Private Sub GetDemographics()
            ''Dim blnRecordLock As Boolean = False
            Try
                ''Bug #62022: 00000606 : EMR PATIENT RELATED LIQUID LINKS CREATE FALSE WARNING WHEN TRIPLE-CLICKED 
                ''added form level locking for patient registration.
                'code for Record Level Locking 
                'If gblnRecordLocking = True Then
                '    Dim mydt As mytable = Nothing ''Slr new not needed 
                '    mydt = Scan_n_Lock_Transaction(TrnType.PatientRegistration, nPatientID, 0, Now)
                '    If mydt.Description <> gstrClientMachineName Then
                '        If MessageBox.Show("This Patient is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot modify this patient now. Do you want to open it?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                '            blnRecordLock = True
                '        Else
                '            If Not IsNothing(mydt) Then  ''slr free mydt
                '                mydt.Dispose()
                '                mydt = Nothing
                '            End If
                '            Exit Sub
                '        End If
                '    End If
                '    If Not IsNothing(mydt) Then  ''slr free mydt
                '        mydt.Dispose()
                '    End If
                '    mydt = Nothing
                'End If
                'end code Record Level Locking

                Dim oPatient As gloPatient.gloPatient = New gloPatient.gloPatient(GetConnectionString)
                Dim _CurrentPatientID As Int64
                _CurrentPatientID = nPatientID

                oPatient.ShowPatientRegistration(_CurrentPatientID, _CurrentPatientID)


                oPatient.Dispose()
                oPatient = Nothing
                If _CurrentPatientID > 0 Then
                    nPatientID = _CurrentPatientID
                End If

                'for Patient Advance Directive
                Dim _result As Boolean = False

                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim _HasDirective As Int16 = 0
                oDB.Connect(GetConnectionString)
                'get value for  PatientDirective
                _HasDirective = Val(oDB.ExecuteQueryScaler("SELECT ISnull(nPatientDirective,0) As nPatientDirective FROM patient WHERE npatientid = " & nPatientID & " "))
                oDB.Disconnect()
                oDB.Dispose()   'Change made to solve memory Leak and word crash issue
                oDB = Nothing
                _blnHasAdvDirective = _HasDirective

                If _blnHasAdvDirective = True Then
                    If gloEDocumentV3.eDocManager.eDocValidator.IsCategoryExists(0, gDMSCategory_PatientDirective, gClinicID) = False Then
                        MessageBox.Show("DMS Category for Advance Directive has not been set, Please set the category", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        _blnHasAdvDirective = False
                    End If
                End If

                If _blnHasAdvDirective = True Then
                    If _CurrentPatientID = 0 Then
                        _result = ShowEScanner(_CurrentPatientID)

                    Else
                        If _blnHasAdvDirective = True Then
                            If MessageBox.Show("Do you want to add more documents against patient directive?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                _result = ShowEScanner(_CurrentPatientID)
                            End If
                        Else
                            _result = ShowEScanner(_CurrentPatientID)
                        End If
                    End If

                Else

                    _result = False
                    '-----------
                End If
                Dim PatientRegistration As New ClsPatientRegistrationDBLayer
                'code for Patient Reg Advance Directive
                If (_CurrentPatientID = 0) Then
                    If (_result = False) Then
                        PatientRegistration.UpdateAdvDirective(_CurrentPatientID)
                    End If
                Else
                    If _blnHasAdvDirective = True Then
                    Else
                        If (_result = False) Then
                            PatientRegistration.UpdateAdvDirective(_CurrentPatientID)
                        End If
                    End If
                End If
                PatientRegistration.Dispose()
                PatientRegistration = Nothing
                'following line equavalant to statement "Call myCallingForm.ShowMicrophone()"
                'type casting used beacause compile time we did not know myCallingForm refer which forms object
                CType(myCallingForm, IWord).ShowMicrophone()


                GetdataFromOtherForms(enumDocType.PatientDemographics)
                GetdataFromOtherForms(enumDocType.Contacts)
                GetdataFromOtherForms(enumDocType.PatientDetails)
                GetdataFromOtherForms(enumDocType.Others)
                '21-May-15 Aniket: Open Modify Patient on Patient Insurance Liquid Link
                GetdataFromOtherForms(enumDocType.PatientInsurance)

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Finally

            End Try
        End Sub

        'for Patient Advance Directive
        Private Function ShowEScanner(ByVal _lPatientDirectiveID As Long) As Boolean

            Dim _result As Boolean = False
            Dim oEDocument As New gloEDocumentV3.gloEDocV3Management
            Try

                'Commented BY Rahul Patel on 26-10-2010
                'gloEDocumentV3.gloEDocV3Admin.Connect(GetConnectionString(), gDMSV3TempPath, gnLoginID, gClinicID, Application.StartupPath)
                'Added by Rahul Patel on 26-10-2010
                'For changing the DMS Hybrid database change.
                gloEDocumentV3.gloEDocV3Admin.Connect(GetConnectionString(), GetDMSConnectionString(), gDMSV3TempPath, gnLoginID, gClinicID, Application.StartupPath)
                'End of code added by Rahul Patel on 26-10-2010

                Dim _OutContID As Long = 0
                Dim _OutDocID As Long = 0
                oEDocument.ShowEScanner(_lPatientDirectiveID, gDMSCategory_PatientDirective, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), gClinicID, gloEDocumentV3.Enumeration.enum_DocumentEventType.ScanDocument, _OutContID, _OutDocID)
                If _OutContID > 0 AndAlso _OutDocID > 0 Then
                    _result = True
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                _result = False

            Finally
                oEDocument.Dispose()
                oEDocument = Nothing
            End Try


            Return _result
        End Function
        '--------------------
        ''' <summary>
        '''GetProblemList: Opens Poblem list form frmProblemList for modify 
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub GetProblemList()
            ' '' <><><> Record Level Locking <><><><> 
            Dim blnRecordLock As Boolean = False
            If gblnRecordLocking = True Then
                Dim mydt As mytable = Nothing  ''Slr no new needed 
                mydt = Scan_n_Lock_Transaction(TrnType.ProblemList, nPatientID, 0, Now)
                If mydt.Code <> gstrLoginName OrElse mydt.Description <> gstrClientMachineName Then
                    If MessageBox.Show("This patients problem lists are being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot Modify it now. Do you want to open it?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        blnRecordLock = True
                    Else
                        If Not IsNothing(mydt) Then  ''slr free mydt
                            mydt.Dispose()
                        End If
                        mydt = Nothing
                        Exit Sub
                    End If
                End If
                If Not IsNothing(mydt) Then ''slr free mydt
                    mydt.Dispose()
                End If
                mydt = Nothing
                'Change made to solve memory Leak and word crash issue
            End If

            '''' <><><> Record Level Locking <><><><>

            ''''' To Modify Selected Problem List
            '' VisitID is Zero when Problem List is Open from Patient Problem List
            Dim frm As New frmProblemList(0, mgnVisitID, PatientId)
            With frm
                frmProblemList.blnOpenFromExam = True
                .ShowMessageForPendingReconciliation()
                .ShowDialog(frm.Parent)
                frmProblemList.blnOpenFromExam = False
                '' Refresh Grid
                'Call Fill_ProblemList()
            End With
            frm.Close() 'Change made to solve memory Leak and word crash issue
            frm.Dispose()
            frm = Nothing
            GetdataFromOtherForms(enumDocType.ProblemList)
            GetdataFromOtherForms(enumDocType.LabOrders)
            GetdataFromOtherForms(enumDocType.RadiologyOrders)
            GetdataFromOtherForms(enumDocType.Prescription)
        End Sub

        ''' <summary>
        ''' GetRadiologyOrders :Opens RadiologyOrders for modify and get reflected changes on word form
        ''' </summary>
        ''' <remarks>added by dipak 20090919</remarks>
        Public Sub GetRadiologyOrders()
            'If mgnVisitID <> 0 Then
            If Trim(strPatientFirstName) <> "" Then
                If mgnVisitID = 0 Then
                    mgnVisitID = GenerateVisitID(nPatientID)
                End If

                Dim frm As frm_LM_Orders
                '' Returns the Existance of Form if any    
                frm = frm_LM_Orders.GetInstance(mgnVisitID, dtDOS, nPatientID, 0, False)
                '' 
                If IsNothing(frm) = True Then
                    Exit Sub
                End If

                'Dim frmOrders As New frmVWOrders(mgnVisitID, dtDOS)
                Call TurnOffMicrophone()
                With frm
                    .MdiParent = Me.myCallingForm.ParentForm
                    .WindowState = FormWindowState.Maximized
                    .myCaller1 = Me.myCallingForm
                    .blnOpenFromExam = False
                    .BringToFront()
                    .Show()
                    'GetdataFromOtherForms("Orders")
                End With
                Call ShowMicrophone()
            Else
                MessageBox.Show("Please select the Patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
            'Else
            'MessageBox.Show("Please select the Visit", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            'End If
        End Sub

        ''' <summary>
        ''' 
        '''GetLabOrders:Opens frmLab_RequestOrder from  for modify and reflect modified changes to word document
        ''' </summary>
        ''' <remarks>added by dipak 20090919 </remarks>
        ''' 

        Public Sub GetLabOrders()
            'If mgnVisitID <> 0 Then
            If Trim(strPatientFirstName) <> "" Then
                If mgnVisitID = 0 Then
                    'PArameter pass to GenerateVisitID() as we replace gnPatientID and gnvisitId by local variables 
                    mgnVisitID = GenerateVisitID(nPatientID)
                End If
                Dim ofrmViewgloLab As New gloEmdeonInterface.Forms.frmViewgloLab(nPatientID)

                If IsNothing(ofrmViewgloLab) = True Then
                    Exit Sub
                End If



                AddHandler ofrmViewgloLab.EvntGenerateCDAHandler, AddressOf mdlGeneral.openCDAFromword
                AddHandler ofrmViewgloLab.EvntOpenClinicalChart, AddressOf mdlGeneral.OpenClinicalChart
                AddHandler ofrmViewgloLab.EvntGenerateCCDHandler, AddressOf mdlGeneral.openCCD

                With ofrmViewgloLab.LabOrderParameter
                    .IsEditMode = False
                    .OrderID = 0
                    .OrderNumberID = 0
                    .OrderNumberPrefix = "ORD"
                    .PatientID = nPatientID
                    .VisitID = mgnVisitID
                    .TransactionDate = Now
                    .CloseAfterSave = True
                End With

                ofrmViewgloLab.WindowState = FormWindowState.Maximized
                ''ofrmViewgloLab.MdiParent = Me
                ofrmViewgloLab.BringToFront()
                ofrmViewgloLab.ShowInTaskbar = False
                ''Pass 'myCallingForm' parameter to ShowDialog for fixed Bug Id 42239 on 20121221
                ofrmViewgloLab.ShowDialog(myCallingForm)
                ''End
                RemoveHandler ofrmViewgloLab.EvntGenerateCDAHandler, AddressOf mdlGeneral.openCDAFromword
                RemoveHandler ofrmViewgloLab.EvntOpenClinicalChart, AddressOf mdlGeneral.OpenClinicalChart
                RemoveHandler ofrmViewgloLab.EvntGenerateCCDHandler, AddressOf mdlGeneral.openCCD
                If (IsNothing(ofrmViewgloLab) = False) Then
                    ofrmViewgloLab.Close()  'Change made to solve memory Leak and word crash issue
                End If

                If (IsNothing(ofrmViewgloLab) = False) Then
                    ofrmViewgloLab.Dispose()
                    ofrmViewgloLab = Nothing
                End If


                Call ShowMicrophone()
            Else
                MessageBox.Show("Please select the Patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

        End Sub


        'Private Sub LoadWordUserControl(ByVal strFileName As String, Optional ByVal blnGetData As Boolean = False)
        '    Dim objExams As New clsPatientExams
        '    Dim ObjWord As clsWordDocument
        '    Dim objCriteria As DocCriteria
        '    ExamProviderId = objExams.GetProviderIdforExam(_DocumentCriteria.PrimaryID)
        '    objExams.Dispose()  ''slr free objExams
        '    objExams = Nothing

        '    Try
        '        wdNewExam.Open(strFileName)
        '    Catch ex As Exception
        '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.Open, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
        '        MessageBox.Show("Template cannot be open because there are problems with the contents.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        wdNewExam.CreateNew("Word.Document")
        '        System.Threading.Thread.Sleep(500)
        '    End Try

        '    If blnGetData Then
        '        ObjWord = New clsWordDocument
        '        objCriteria = New DocCriteria
        '        ''//Mapping values for retrieving data from DB
        '        objCriteria.DocCategory = enumDocCategory.Exam
        '        objCriteria.PatientID = _DocumentCriteria.PatientID
        '        objCriteria.VisitID = mgnVisitID
        '        objCriteria.PrimaryID = _DocumentCriteria.PrimaryID
        '        ObjWord.DocumentCriteria = objCriteria
        '        ObjWord.CurDocument = _oCurDoc
        '        ''//Replace the Form Fields with data in the Word Document
        '        ObjWord.GetFormFieldData(enumDocType.None)
        '        _oCurDoc = ObjWord.CurDocument
        '        _oCurDoc.ActiveWindow.View.ShowFieldCodes = False
        '        objCriteria = Nothing
        '        ObjWord = Nothing
        '    Else
        '        ObjWord = New clsWordDocument
        '        ObjWord.CurDocument = _oCurDoc
        '        ObjWord.HighlightColor()
        '        _oCurDoc = ObjWord.CurDocument
        '        _oCurDoc.ActiveWindow.View.ShowFieldCodes = False
        '        ObjWord = Nothing
        '    End If

        '    _oCurDoc.ActiveWindow.SetFocus()
        '    _oCurDoc.ActiveWindow.Selection.HomeKey(Wd.WdUnits.wdStory)

        'End Sub

        ''' <summary>
        ''' Procedure fill Diagnosis
        ''' </summary>
        ''' <remarks>added by dipak 20090919</remarks>
        Private Sub FillDiagnosis()

            Dim dt As DataTable = Nothing
            Dim dtDiagnosisDistinct As DataTable = Nothing
            Dim dvDiagnosis As DataView = Nothing

            If gblnICD9Driven Then
                Dim objDiagnosisDBLayer As New ClsDiagnosisDBLayer
                dt = objDiagnosisDBLayer.FetchICD9forUpdate(_DocumentCriteria.PrimaryID, mgnVisitID)
                objDiagnosisDBLayer = Nothing
            Else
                dt = FetchICD9forUpdate(_DocumentCriteria.PrimaryID, mgnVisitID)
            End If

            Dim i As Int16

            If Not IsNothing(dt) Then
                dvDiagnosis = New DataView(dt)
                dtDiagnosisDistinct = New DataTable

                Dim strICD9(dt.Columns.Count - 1) As String

                For i = 0 To dt.Columns.Count - 1
                    strICD9.SetValue(dt.Columns(i).ColumnName, i)
                Next
                dtDiagnosisDistinct = dvDiagnosis.ToTable(True, strICD9)
            End If

            If IsNothing(frmPatientExam.Arrlist) = False Then
                If frmPatientExam.Arrlist.Count > 0 Then
                    frmPatientExam.Arrlist.Clear()
                End If
                frmPatientExam.Arrlist = Nothing
            End If
            frmPatientExam.Arrlist = New ArrayList
            If IsNothing(dtDiagnosisDistinct) = False Then
                For i = 0 To dtDiagnosisDistinct.Rows.Count - 1
                    'trDiagnosis.Nodes.Item(0).Nodes.Add(New myTreeNode(dt.Rows(i)(0) & "-" & dt.Rows(i)(1), -1, CType(dt.Rows(i)(0), String)))
                    frmPatientExam.Arrlist.Add(New mytable(CStr(dtDiagnosisDistinct.Rows(i)(1)), CStr(dtDiagnosisDistinct.Rows(i)(0))))
                Next
            End If

            If Not IsNothing(dvDiagnosis) Then
                dvDiagnosis.Dispose()
                dvDiagnosis = Nothing
            End If
            If Not IsNothing(dtDiagnosisDistinct) Then
                dtDiagnosisDistinct.Dispose()
                dtDiagnosisDistinct = Nothing
            End If
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            'If Arrlist.Count = 0 Then
            '    cmbExamName.Enabled = False
            'Else
            '    cmbExamName.Enabled = True
            'End If

        End Sub

        ''' <summary>
        ''' functio fetch ICD9 For Update
        ''' </summary>
        ''' <param name="nExamID"></param>
        ''' <param name="nVisitID"></param>
        ''' <returns> Data tabel</returns>
        ''' <remarks>added by dipak 20090919 and used in FillDiagnosis() </remarks>
        Private Function FetchICD9forUpdate(ByVal nExamID As Int64, ByVal nVisitID As Int64) As DataTable
            Dim dtICD9 As New DataTable
            Dim con As SqlConnection = Nothing
            Dim adp As SqlDataAdapter = Nothing
            Try
                Dim Query As String = " SELECT DISTINCT ISNULL(sICD9Code,'') AS sICD9Code, ISNULL(sICD9Description,'') AS sICD9Description " _
                    & " FROM ExamICD9CPT WHERE nExamID = " & nExamID & " AND nVisitID = " & nVisitID & " AND sICD9Code <> '' "
                con = New SqlConnection(GetConnectionString)
                adp = New SqlDataAdapter(Query, con)
                adp.Fill(dtICD9)
                If dtICD9 IsNot Nothing Then
                    If dtICD9.Rows.Count > 0 Then
                        Return dtICD9
                    End If
                End If
                Return Nothing
            Catch ex As Exception
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Nothing
            Finally
                If Not IsNothing(adp) Then
                    adp.Dispose()
                    adp = Nothing
                End If
                If Not IsNothing(con) Then
                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Dispose()
                    con = Nothing
                End If
                'If Not IsNothing(dtICD9) Then   ''slr dont free it
                '    'dtICD9.Dispose()
                '    dtICD9 = Nothing
                'End If
            End Try
        End Function



        Private Sub GetDiagnosisfromVisitSummary()
            If frmPatientExam.blnChangesMade = True Then
                FillDiagnosis()
                GetdataFromOtherForms(enumDocType.Diagnosis)
                '28-Dec-15 Resolving Bug #92200: gloEMR: (Liquid link) CPT with charges: CPT with charges liquid link is not working properly
                GetdataFromOtherForms(enumDocType.ExamICD9CPT_PM)
            End If
        End Sub


        'Public Sub FillProblemList()
        '    Dim _strCode As String = ""
        '    Dim _strDescription As String = ""
        '    Dim _strSnomedCode As String = ""
        '    Dim _strSnomedDesc As String = ""

        '    Dim oclsDiagnosis As New ClsDiagnosisDBLayer
        '    If IsNothing(frmPatientExam.Arrlist) = False Then
        '        If frmPatientExam.Arrlist.Count > 0 Then
        '            For i As Integer = 0 To frmPatientExam.Arrlist.Count - 1
        '                strDiagnosis = CType(frmPatientExam.Arrlist.Item(i), mytable).Code.ToString & " " & CType(frmPatientExam.Arrlist.Item(i), mytable).Description.ToString
        '                _strCode = CType(frmPatientExam.Arrlist.Item(i), mytable).Code.ToString
        '                _strDescription = CType(frmPatientExam.Arrlist.Item(i), mytable).Description.ToString
        '                _strSnomedCode = CType(frmPatientExam.Arrlist.Item(i), mytable).SnoCode.ToString
        '                _strSnomedDesc = CType(frmPatientExam.Arrlist.Item(i), mytable).snomeddescription.ToString
        '                oclsDiagnosis.FillProblemList(_DocumentCriteria.PatientID, _DocumentCriteria.VisitID, dtDOS, _strCode, _strDescription, _strSnomedCode, _strSnomedDesc)
        '            Next
        '        End If
        '    End If
        '    oclsDiagnosis = Nothing
        'End Sub
        ''' <summary>
        ''' GetROS: procedure opens ROS form for modify ROS and reflect changes on word document
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub GetROS()
            If Trim(strPatientFirstName) <> "" Then
                If mgnVisitID = 0 Then
                    '
                    mgnVisitID = GenerateVisitID(nPatientID)
                End If

                Dim frmPatROS As New frmPatientROS(mgnVisitID, dtDOS, nPatientID)
                With frmPatROS
                    '.MdiParent = Me
                    .WindowState = FormWindowState.Maximized
                    .myCaller1 = Me.myCallingForm
                    '.blnOpenFromExam = True
                    .MdiParent = Me.myCallingForm.ParentForm
                    .Show()
                    'GetdataFromOtherForms()
                End With
            Else
                MessageBox.Show("Please select the Patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

        End Sub

        ''' <summary>
        '''GetHistory: procedure opens History form for modify history and reflect changes on word document
        ''' </summary>
        ''' <remarks>added by dipak 20090919</remarks>
        Public Sub GetHistory()
            Dim ofrmHistory As frmHistory
            'If mgnVisitID <> 0 Then
            If Trim(strPatientFirstName) <> "" Then
                mgnVisitID = GenerateVisitID(dtDOS, nPatientID)
                ' '' <><><> Record Level Locking <><><><> 
                Dim blnRecordLock As Boolean = False
                ofrmHistory = New frmHistory(mgnVisitID, dtDOS, nPatientID, blnRecordLock)

                '.MdiParent = Me
                ofrmHistory.WindowState = FormWindowState.Maximized
                ofrmHistory.blnOpenFromExam = True
                ofrmHistory.myCaller1 = Me.myCallingForm
                ofrmHistory.MdiParent = myCallingForm.ParentForm

                ofrmHistory.PopulatePatientHistory_Final()
                If ofrmHistory.blncancel Then
                    'intflag = 1
                    ofrmHistory.Show()
                    ofrmHistory.BringToFront()
                    ofrmHistory.WindowState = FormWindowState.Maximized
                Else
                    ofrmHistory.Close() 'Change made to solve memory Leak and word crash issue
                    ofrmHistory.Dispose()
                    ofrmHistory = Nothing
                End If
            Else
                MessageBox.Show("Please select the Patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End Sub

        Public Sub GetOBPlan()

            Dim ofrmPatientOBPlan As frmPatientOBPlan

            If Trim(strPatientFirstName) <> "" Then
                mgnVisitID = GenerateVisitID(dtDOS, nPatientID)

                Dim blnRecordLock As Boolean = False
                ofrmPatientOBPlan = New frmPatientOBPlan(mgnVisitID, dtDOS, nPatientID, blnRecordLock)


                ofrmPatientOBPlan.WindowState = FormWindowState.Maximized
                ofrmPatientOBPlan.blnOpenFromExam = True
                ofrmPatientOBPlan.myCaller1 = Me.myCallingForm
                ofrmPatientOBPlan.MdiParent = myCallingForm.ParentForm
                ofrmPatientOBPlan.PopulatePatientOBPlan_Final()

                If ofrmPatientOBPlan.blncancel Then

                    ofrmPatientOBPlan.Show()
                    ofrmPatientOBPlan.BringToFront()
                    ofrmPatientOBPlan.WindowState = FormWindowState.Maximized
                Else
                    ofrmPatientOBPlan.Close()
                    ofrmPatientOBPlan.Dispose()
                    ofrmPatientOBPlan = Nothing
                End If
            Else
                MessageBox.Show("Please select the Patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

        End Sub

        ''' <summary>
        '''GetMedication :procedure opens medication form for modify Medication and reflect changes on word document
        ''' </summary>
        ''' <remarks>added by dipak 20090919</remarks>
        Public Sub GetMedication()
            Dim frmRxMeds As frmPrescription = Nothing
            If Trim(strPatientFirstName) <> "" Then
                If Not IsNothing(myCallingForm) Then                    
                    mgnVisitID = _DocumentCriteria.VisitID
                    frmRxMeds = frmPrescription.GetInstance(mgnVisitID, nPatientID)
                End If

                If IsNothing(frmRxMeds) = True Then
                    Exit Sub
                End If

                frmRxMeds.WindowState = FormWindowState.Maximized
                frmRxMeds.myCaller1 = Me.myCallingForm                
                frmRxMeds.MdiParent = Me.myCallingForm.MdiParent
                If frmPrescription.IsOpen = False Then
                    frmRxMeds.ShowMedication()
                End If
                If frmRxMeds.blncancel Then
                    frmRxMeds.Show()
                End If
            Else
                MessageBox.Show("Please select the Patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End Sub

        Public Sub GetPrescription()
            Dim frmRxMeds As frmPrescription
            If Not IsNothing(myCallingForm) Then
                If Trim(strPatientFirstName) <> "" Then
                    mgnVisitID = _DocumentCriteria.VisitID
                    frmRxMeds = frmPrescription.GetInstance(mgnVisitID, nPatientID)
                    If IsNothing(frmRxMeds) = True Then
                        Exit Sub
                    End If

                    With frmRxMeds
                        .WindowState = FormWindowState.Maximized                        
                        .myCaller1 = Me.myCallingForm
                        .MdiParent = Me.myCallingForm.ParentForm
                        .ShowReconcileMessage()
                        .Show()
                    End With
                Else
                    MessageBox.Show("Please select the Patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End If
        End Sub
        ''Add this method to check selection for Add Datadictionay button on forms
        Public Sub ValidateSelection(ByRef _oCurDoc As Wd.Document)

            Dim rTemp As Wd.Range = _oCurDoc.ActiveWindow.Selection.Range
            If Not rTemp.ParentContentControl Is Nothing Then
                If rTemp.ParentContentControl.Type = Wd.WdContentControlType.wdContentControlDropdownList Then
                    Dim cntctrl As Wd.ContentControl = rTemp.ParentContentControl
                    'If cntctrl.Type = Wd.WdContentControlType.wdContentControlDropdownList Then
                    _oCurDoc.ActiveWindow.Selection.EndKey(Unit:=Wd.WdUnits.wdLine)
                    _oCurDoc.ActiveWindow.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=2)
                    'Selection.MoveRight(Unit:=wdCharacter, Count:=3)
                    'End If
                End If
            End If

        End Sub
        ''' <summary>
        ''' Procedure for Add Data field
        ''' </summary>
        ''' <remarks>Added by dipak 20091001</remarks>
        Public Sub AddDataDictionaryFields(ByVal oParent As Form)
            If IsNothing(_oCurDoc) = True Then
                Exit Sub
            End If
            If IsFeasible(enumControls.FormFieldControl) = False Then
                Exit Sub
            End If

            Dim frm As New frmAddDictionary()
            If (IsNothing(AddDictionaryParent) = False) Then
                Try
                    frm.Parent = AddDictionaryParent
                Catch ex As Exception

                End Try

            End If

            Try
                With frm
                    .ShowInTaskbar = False
                    .StartPosition = FormStartPosition.CenterParent
                    If .ShowDialog(oParent) = Windows.Forms.DialogResult.Yes Then
                        Try
                            Application.UseWaitCursor = True

                            AddDynamicField(frm.GetArrlistDataDictionary, frm.IncludeCaption)
                        Catch ex As Exception

                        Finally
                            Application.UseWaitCursor = False


                            ' Ujwala - added function on 01112014-OptimizePerformance to optimize word object while displaying word modules            

                            ' Ujwala - added function on 01112014- OptimizePerformance to optimize word object while displaying word modules
                        End Try

                    End If
                End With
                frm.Close() 'Change made to solve memory Leak and word crash issue
                frm.Dispose()
                frm = Nothing
            Catch ex As Exception


            End Try



        End Sub

        Public Sub AddDataDictionaryFields()
            If IsNothing(_oCurDoc) = True Then
                Exit Sub
            End If
            If IsFeasible(enumControls.FormFieldControl) = False Then
                Exit Sub
            End If

            Dim frm As New frmAddDictionary()
            If (IsNothing(AddDictionaryParent) = False) Then
                Try
                    frm.Parent = AddDictionaryParent
                Catch ex As Exception

                End Try

            End If



            With frm
                .ShowInTaskbar = False
                .StartPosition = FormStartPosition.CenterParent
                If .ShowDialog(frm.Parent) = Windows.Forms.DialogResult.Yes Then
                    Try
                        Application.UseWaitCursor = True
                        AddDynamicField(frm.GetArrlistDataDictionary, frm.IncludeCaption)
                    Catch ex As Exception
                    Finally
                        Application.UseWaitCursor = False


                    End Try
                End If
            End With
            frm.Close() 'Change made to solve memory Leak and word crash issue
            frm.Dispose()
            frm = Nothing

        End Sub



        ''' <summary>
        ''' Procedure for add a liquid link fields to a documents
        ''' </summary>
        ''' <param name="Arrlist">Is array list contain list of field added to document</param>
        ''' <param name="IncludeCaption">Boolean variable indicate IncludeCaption or not </param>
        ''' <remarks>Added by dipak 20091001</remarks>
        Public Sub AddDynamicField(ByVal Arrlist As ArrayList, ByVal IncludeCaption As Boolean)
            'SLR: Added to resolve Bug #83545: Concurrency Testing : Application gives an exception when user insert liquid link on the template
            'SLR: Looks like went it to readonly mode: Hence changed to edit mode. Need to test again
            Dim myType As Wd.WdViewType = Nothing
            Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(_oCurDoc, myType)
            Try


                If IsNothing(Arrlist) = False Then
                    If Arrlist.Count > 0 Then
                        '_oCurDoc.ActiveWindow.SetFocus()
                        Dim GurantorID As Int64 = 0
                        For i As Integer = 0 To Arrlist.Count - 1
                            'If _oCurDoc.Application.ActiveDocument.ProtectionType = Wd.WdProtectionType.wdNoProtection Then
                            If (GurantorID = 0) AndAlso (CType(Arrlist.Item(i), myList).Code.ToString.Contains("pa_accounts.") OrElse CType(Arrlist.Item(i), myList).Code.ToString.Contains("PA_Accounts_Patients.") OrElse CType(Arrlist.Item(i), myList).Code.ToString.Contains("pa_accounts_Billing.") OrElse CType(Arrlist.Item(i), myList).Code.ToString.Contains("pa_accounts_PatientLastClaimDiag.")) Then
                                'Set GurantorID here.
                                'Set GurantorID here.
                                Dim ofrmSelectPatientGuarantor As New frmSelectPatientGuarantor(_DocumentCriteria.PatientID, gnClinicID)
                                Dim oClsSelectPatientGuarantor As New clsSelectPatientGuarantor(_DocumentCriteria.PatientID, gnClinicID)
                                Dim dtAccounts As DataTable
                                dtAccounts = oClsSelectPatientGuarantor.GetPatientAccounts(_DocumentCriteria.PatientID, gnClinicID)
                                If (IsNothing(dtAccounts) = False) Then


                                    If (dtAccounts.Rows.Count = 1) Then
                                        GurantorID = dtAccounts.Rows(0)("nPAccountID").ToString()
                                    ElseIf (dtAccounts.Rows.Count > 1) Then
                                        ofrmSelectPatientGuarantor.ShowDialog(ofrmSelectPatientGuarantor.Parent)
                                        'aField.Result = ofrmSelectPatientGuarantor.SelectedGuarantor
                                        If (ofrmSelectPatientGuarantor.DialogResult = DialogResult.OK) Then
                                            GurantorID = ofrmSelectPatientGuarantor.SelectedAccount
                                        Else
                                            GurantorID = -1
                                        End If
                                    Else
                                        GurantorID = 0
                                    End If
                                Else
                                    GurantorID = 0
                                End If
                                If IsNothing(ofrmSelectPatientGuarantor) = False Then
                                    ofrmSelectPatientGuarantor.Dispose()
                                    ofrmSelectPatientGuarantor = Nothing
                                End If
                                If IsNothing(oClsSelectPatientGuarantor) = False Then
                                    oClsSelectPatientGuarantor.Dispose()
                                    oClsSelectPatientGuarantor = Nothing
                                End If
                                If Not IsNothing(dtAccounts) Then  ''slr free dtAccounts
                                    dtAccounts.Dispose()
                                End If
                                dtAccounts = Nothing
                            End If

                            If i >= 1 Then
                                If Convert.ToString(Arrlist.Item(i - 1)) = "PT - CPT/Mod/Units with description" Then
                                    _oCurDoc.ActiveWindow.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
                                End If
                                _oCurDoc.ActiveWindow.Selection.TypeText(vbNewLine)
                            End If
                            Dim oNameField As Wd.FormField

                            'Yatin- Liquid link Lable is get merged in to table
                            Dim wdRng As Wd.Range = _oCurDoc.ActiveWindow.Selection.Range
                            If (IsNothing(wdRng) = False) Then
                                Dim myStart As Integer = wdRng.Start
                                Dim thisStart As Integer = myStart
                                While wdRng.Tables.Count > 0
                                    _oCurDoc.ActiveWindow.Selection.MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=1)
                                    wdRng = _oCurDoc.ActiveWindow.Selection.Range
                                    If (IsNothing(wdRng) = False) Then
                                        thisStart = wdRng.Start
                                        If (thisStart = myStart) Then
                                            Exit While
                                        End If
                                        myStart = thisStart
                                    Else
                                        Exit While

                                    End If

                                End While
                            End If
                            If (IsNothing(wdRng) = False) Then
                                _oCurDoc.ActiveWindow.Selection.Select()
                                wdRng = _oCurDoc.ActiveWindow.Selection.Range
                            End If

                            'Added Lable For The Liquid Link
                            If IncludeCaption Then
                                '_oCurDoc.ActiveWindow.Selection.Range.HighlightColorIndex = Wd.WdColorIndex.wdNoHighlight
                                _oCurDoc.ActiveWindow.Selection.TypeText(CType(Arrlist.Item(i), myList).Description.ToString & ": ")
                            End If
                            ''Add the form field of type text

                            If _oCurDoc.ActiveWindow.Selection.Range.Tables.Count = 0 Then
                                _oCurDoc.ActiveWindow.Selection.TypeText(" ") '' IF TABLE NOT PRESENT THEN SEPERATE FIELDS BY SPACE ''
                                ''Added condition- for FOA template if table present and cells not present then 
                            Else
                                'SLR: Change to check for Cells
                                Dim myCells As Wd.Cells = Nothing
                                Try
                                    myCells = _oCurDoc.Application.Selection.Range.Cells
                                Catch exCells As Exception

                                End Try
                                If IsNothing(myCells) = False Then
                                    If myCells.Count = 0 Then '' IF TABLE PRESENT, But No Cells Exist THEN MOVE BY CELL ''
                                        _oCurDoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
                                    Else
                                    End If
                                Else
                                End If
                                'SLR: Previous Code
                                'If Not IsNothing(_oCurDoc.ActiveWindow.Selection.Range.Tables.Count) Then '' IF TABLE PRESENT THEN MOVE BY CELL ''
                                '    _oCurDoc.ActiveWindow.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)

                                'End If
                            End If

                            oNameField = _oCurDoc.FormFields.Add(_oCurDoc.ActiveWindow.Selection.Range, Wd.WdFieldType.wdFieldFormTextInput)
                            oNameField.Result = CType(Arrlist.Item(i), myList).Description 'Result To show caption 
                            oNameField.StatusText = CType(Arrlist.Item(i), myList).Code 'Status text to hold Table & field names 
                            oNameField.HelpText = CType(Arrlist.Item(i), myList).Description 'Help text to hold group


                            '' replace the form field with data 
                            'oCurDoc1.ActiveWindow.Selection.TypeText(vbTab)
                            _oCurDoc.ActiveWindow.Selection.InsertParagraph()
                            ''ReplaceFormData(oNameField.StatusText)
                            GetDataForField(oNameField.StatusText, GurantorID) '' to fetch data for only this field ''
                            oNameField = Nothing
                            'Else
                            '    MessageBox.Show("Current operation is invalid as document is under protection mode.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            'End If

                        Next

                    End If
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                Arrlist = Nothing
                gloWord.LoadAndCloseWord.RestoreFromEditView(_oCurDoc, myType, myLayout)
            End Try


        End Sub

        Private Sub RemoveWordHandler(ByVal oWordApp As Wd.Application)

            Try
                RemoveHandler oWordApp.WindowBeforeDoubleClick, AddressOf OnFormClicked
            Catch ex As Exception

            End Try
        End Sub



        ''' <summary>
        '''Procedure for fetch only data for this formfiled ''
        ''' </summary>
        ''' <param name="sStatusText"></param>
        ''' <remarks>Added by dipak 20091001</remarks>
        Public Sub GetDataForField(ByVal sStatusText As String, Optional ByVal GurantorID As Int64 = 0)
            If Not _oCurDoc Is Nothing Then
                _oCurDoc.ActiveWindow.SetFocus()
                GetFormFieldData(enumDocType.None, sStatusText, GurantorID)
                ''Added beacuse of handlers are removed when other word related forms are opened
                ' If _DocType = enumDocType.PatientEducation Or _DocType = enumDocType.RadiologyOrders Or _DocType = enumDocType.ProblemList Then
                If isHandlerRemoved Then
                    ' oWordApp = _oCurDoc.Application
                    If Not _oCurDoc.Application Is Nothing Then
                        Try
                            'RemoveHandler oWordApp.WindowBeforeDoubleClick, AddressOf OnFormClicked
                            RemoveWordHandler(_oCurDoc.Application)


                            'Developer:Yatin N. Bhagat
                            'Date:12/13/2011
                            'Bug ID/PRD Name/Salesforce Case:Bug No 17073
                            'Reason: Condition is Not applied for the Double Click Event on Word Document
                            If LiquidFlag Then
                                AddHandler _oCurDoc.Application.WindowBeforeDoubleClick, AddressOf OnFormClicked
                            End If

                        Catch ex As Exception
                            UpdateVoiceLog(ex.ToString)
                        Finally
                            'commented as not used
                            'blnIsHandlers = False
                            isHandlerRemoved = False
                        End Try

                    End If
                End If
            End If
        End Sub
        '' END SUDHIR ''
#End Region
#Region "//--Interface Functions Implementation--//"

        ''//Procedure to cleanup the Formfields which has no data and special tags like Navigation, HPI used for print & Fax


        ''//To insert Word Document as Binary
        Public Function ConvertFiletoBinary(ByVal strFileName As String) As Byte() Implements IgloEMRWord.InsertDocument
            If File.Exists(strFileName) Then
                Dim oFile As FileStream = Nothing
                Dim oReader As BinaryReader = Nothing
                Try

                    ''To read the file only when it is not in use by any process
                    Try
                        oFile = New FileStream(strFileName, FileMode.Open, FileAccess.Read)
                    Catch ex As Exception
                        Try
                            ''Please uncomment the following line of code to read the file, even the file is in use by same or another process

                            oFile = New FileStream(strFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 8, FileOptions.Asynchronous)

                        Catch ex2 As IOException
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex2.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                            MsgBox("Error while conversion  - " & ex2.ToString)
                            Return Nothing
                        Catch ex2 As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex2.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                            MsgBox("Error while conversion  - " & ex2.ToString)
                            Return Nothing
                        End Try

                    End Try

                    oReader = New BinaryReader(oFile)
                    Dim bytesRead As Byte() = oReader.ReadBytes(oFile.Length)
                    Return bytesRead

                Catch ex3 As IOException
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex3.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    MsgBox("Error while conversion  - " & ex3.ToString)
                    Return Nothing
                Catch ex3 As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex3.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    MsgBox("Error while conversion  - " & ex3.ToString)
                    Return Nothing

                Finally

                    ''22-Mar-13 Aniket: Resolving Memory Leak Issues
                    If (IsNothing(oReader) = False) Then
                        oReader.Close()
                        oReader.Dispose()
                        oReader = Nothing
                    End If

                    If (IsNothing(oFile) = False) Then
                        oFile.Close()
                        oFile.Dispose()
                        oFile = Nothing
                    End If


                End Try

            Else
                Return Nothing
            End If
        End Function

        ''// Retreive Document from Database and
        Public Function RetrieveDocumentFile() As String Implements IgloEMRWord.RetrieveDocumentFile

            Dim strFileName As String
            Dim oDB As New DataBaseLayer
            Dim oParamater As DBParameter
            Dim _SPName As String
            Dim _ParaName As String

            If (IsNothing(oDB) = True) Then
                Return ("")
            End If

            Select Case _DocumentCriteria.DocCategory
                Case enumDocCategory.Template
                    _ParaName = "@nTemplateID"
                    _SPName = "gsp_GetExamContents"
                Case enumDocCategory.Exam
                    _ParaName = "@ExamID"
                    _SPName = "gsp_GetPastExamContents"
                Case Else
                    _ParaName = ""
                    _SPName = ""
            End Select

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = _ParaName
            oParamater.Value = _DocumentCriteria.PrimaryID

            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            '26-Apr-17 Aniket: Tag Snomed association
            If _DocumentCriteria.DocCategory = enumDocCategory.Template Then
                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@nExamID"
                oParamater.Value = _DocumentCriteria.FieldID1

                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@nVisitID"
                oParamater.Value = _DocumentCriteria.FieldID2

                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing
            End If

            Dim oResult As Object = oDB.GetDataValue(_SPName)

            If oResult Is Nothing Then
                If (IsNothing(oDB) = False) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If
                Return ""
            End If

            If IsDBNull(oResult) = False Then
                strFileName = ExamNewDocumentName
                '' generate Physical file
                'SLR: Modified on 3/28/2014
                strFileName = GenerateFile(oResult, strFileName)
                If (IsNothing(oDB) = False) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If
                Return strFileName
            Else
                If (IsNothing(oDB) = False) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If
                Return ""
            End If
        End Function

        ''//Retreive data 
        Public Function GetData(ByVal DocumentType As enumDocType) As System.Data.DataTable Implements IgloEMRWord.GetData
            Dim _SPName As String
            Dim oDB As New DataBaseLayer
            Dim oParamater As DBParameter

            ''22-Mar-13 Aniket: Resolving Memory Leak Issues
            Dim oResultTable As DataTable = Nothing
            Try

                _SPName = GetSPNames(DocumentType)
                Dim _PatientID As Int64 = _DocumentCriteria.PatientID
                Dim _DocCategory As String = _DocumentCriteria.DocCategory
                Dim _PrimaryID As Int64 = _DocumentCriteria.PrimaryID
                Dim _VisitID As Int64 = _DocumentCriteria.VisitID
                'Parameter Varibles - Finish


                '22-Mar-13 Aniket: Resolving Memory Leak Issues
                'oResultTable = New DataTable

                ''//Mapping values for PatienID, ExamID, VisitID
                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@nPatientID"
                oParamater.Value = _PatientID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@nPrimaryID"
                oParamater.Value = _PrimaryID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@nVisitID"
                oParamater.Value = _VisitID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oResultTable = oDB.GetDataTable(_SPName)
                If Not oResultTable Is Nothing Then
                    Return oResultTable
                End If
                Return Nothing
            Catch ex As Exception
                Return Nothing
            Finally
                If Not IsNothing(oResultTable) Then
                    'oResultTable.Dispose()
                    oResultTable = Nothing
                End If
                If Not IsNothing(oDB) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If
            End Try
        End Function

        '''' <summary>
        '''' Public  function that is accessible to all forms for replacing hte form fields with data
        '''' </summary>
        '''' <param name="DocumentType"></param>
        '''' <remarks></remarks>
        Public Sub HighlightColor()
            ' _oCurDoc.Application.ActiveDocument.FormFields.Shaded = False 'Make shading false
            Dim blnUnprotect As Boolean = False
            Try


                If IsNothing(_oCurDoc) = True Then
                    Exit Sub 'Return Nothing
                End If
                If _oCurDoc.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
                    _oCurDoc.Unprotect()

                    blnUnprotect = True
                End If
                Dim aField As Wd.FormField 'Form field Variable
                For Each aField In _oCurDoc.FormFields
                    If (Not IsNothing(aField)) Then
                        If aField.Type = Wd.WdFieldType.wdFieldFormTextInput Then
                            Try
                                If gblnWordColorHighlight Then
                                    aField.Range.HighlightColorIndex = gblnWordBackColor
                                Else
                                    aField.Range.HighlightColorIndex = Wd.WdColorIndex.wdNoHighlight
                                End If
                            Catch ex As Exception

                            End Try

                        End If
                    End If

                Next
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Finally
                If blnUnprotect = True Then
                    _oCurDoc.Protect(Wd.WdProtectionType.wdAllowOnlyComments)
                End If
            End Try
        End Sub

        'Private Sub SaveFreeTextForTreatment()
        '    Try
        '        Dim r As Wd.Range = _oCurDoc.ActiveWindow.Selection.Range
        '        r.SetRange(_oCurDoc.ActiveWindow.Selection.Range.Start, _oCurDoc.ActiveWindow.Selection.Range.End + 1)
        '        Dim tbCount As Integer = r.Tables.Count
        '        If tbCount > 0 Then

        '            If IsNothing(dtHoldDataForCPTModifierLL) Then
        '                Dim column1 As DataColumn = New DataColumn("Column1")
        '                Dim column2 As DataColumn = New DataColumn("Column2")
        '                Dim column3 As DataColumn = New DataColumn("Column3")
        '                Dim column4 As DataColumn = New DataColumn("Column4")
        '                Dim column5 As DataColumn = New DataColumn("Column5")
        '                dtHoldDataForCPTModifierLL = New DataTable()
        '                dtHoldDataForCPTModifierLL.Columns.Add(column1)
        '                dtHoldDataForCPTModifierLL.Columns.Add(column2)
        '                dtHoldDataForCPTModifierLL.Columns.Add(column3)
        '                dtHoldDataForCPTModifierLL.Columns.Add(column4)
        '                dtHoldDataForCPTModifierLL.Columns.Add(column5)
        '            End If

        '            Dim tb1 As Wd.Table = Nothing
        '            Dim strsellData As String = ""
        '            Dim i As Integer
        '            Dim j As Integer
        '            Dim dRow As DataRow
        '            If r.Tables.Count > 0 Then
        '                For Each tb As Wd.Table In r.Tables
        '                    For i = 2 To tb.Rows.Count Step 1
        '                        dRow = dtHoldDataForCPTModifierLL.NewRow()
        '                        For j = 1 To tb.Columns.Count Step 1
        '                            strsellData = Convert.ToString(tb.Cell(i, j).Range.Text.TrimEnd)
        '                            strsellData = Replace(strsellData, vbCr, "")
        '                            dRow.Item(j - 1) = strsellData
        '                        Next
        '                        dtHoldDataForCPTModifierLL.Rows.Add(dRow)
        '                    Next
        '                Next
        '            End If
        '        End If
        '    Catch ex As Exception
        '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        '    End Try

        'End Sub


        Public Sub FillLiquidFormFieldscollection(ByVal aField As Wd.FormField, Optional ByRef oColManagement_option As CollLiquidData = Nothing, Optional ByRef oColLabs As CollLiquidData = Nothing, Optional ByRef oColX_Ray_Radiology As CollLiquidData = Nothing, Optional ByRef oColOther_Diagonsis_Tests As CollLiquidData = Nothing)

            Dim EMresult As String
            '  Dim oColLiquidData = New CollLiquidData()



            Dim arrText As New ArrayList
            Dim myDataList As New myList
            Dim oLiquidData As clsLiquidData
            Dim m_elementId As Long
            If InStr(aField.StatusText, "History") Then
                Dim result() As String = Nothing
                result = aField.Result.Split(aField.Result, Chr(11))
                historycont = historycont + result.Length - 1
            End If

            If InStr(aField.StatusText, "TimeSpent") Then
                myDataList = New myList
                oLiquidData = New clsLiquidData()
                oLiquidData.PatientID = PatientId
                oLiquidData.mgnVisitID = mgnVisitID
                oLiquidData.examid =
                oLiquidData.m_elementId = m_elementId
                oLiquidData.HelpText = 0
                oLiquidData.m_datatype = ""
                oLiquidData.Category = CategoryType.Management_option

                EMresult = aField.Result

                arrText = New ArrayList
                myDataList.HistoryItem = EMresult
                myDataList.HistoryCategory = CategoryType.Management_option
                myDataList.AssociatedProperty = "confWPatientFamilyMinutes"
                arrText.Add(myDataList)
                oLiquidData.ArrText_Field = arrText

                myDataList = Nothing
                arrText = Nothing
                oColManagement_option.Add(oLiquidData)
                'Change made to solve memory Leak and word crash issue
                If Not oLiquidData Is Nothing Then
                    oLiquidData.Dispose()
                    oLiquidData = Nothing
                End If
            End If
            If InStr(aField.StatusText, "OtherOrders") Then
                myDataList = New myList
                oLiquidData = New clsLiquidData()
                oLiquidData.PatientID = _DocumentCriteria.PatientID
                oLiquidData.mgnVisitID = _DocumentCriteria.VisitID
                oLiquidData.examid = _DocumentCriteria.PrimaryID
                oLiquidData.m_elementId = m_elementId
                oLiquidData.HelpText = 0
                oLiquidData.m_datatype = ""
                oLiquidData.Category = CategoryType.X_Ray_Radiology
                Dim result As String ' = Nothing
                result = aField.Result

                arrText = New ArrayList
                myDataList.HistoryItem = result
                myDataList.HistoryCategory = CategoryType.X_Ray_Radiology
                myDataList.AssociatedProperty = "OtherXRaysCount"
                arrText.Add(myDataList)
                oLiquidData.ArrText_Field = arrText
                myDataList = Nothing
                arrText = Nothing
                oColX_Ray_Radiology.Add(oLiquidData)
                'Change made to solve memory Leak and word crash issue
                If Not oLiquidData Is Nothing Then
                    oLiquidData.Dispose()
                    oLiquidData = Nothing
                End If
            End If
            If InStr(aField.StatusText, "Otherlabs") Then
                myDataList = New myList
                oLiquidData = New clsLiquidData()
                oLiquidData.PatientID = _DocumentCriteria.PatientID
                oLiquidData.mgnVisitID = _DocumentCriteria.VisitID
                oLiquidData.examid = _DocumentCriteria.PrimaryID
                oLiquidData.m_elementId = m_elementId
                oLiquidData.HelpText = 0
                oLiquidData.m_datatype = ""
                oLiquidData.Category = CategoryType.Labs

                EMresult = aField.Result
                arrText = New ArrayList
                myDataList.HistoryItem = EMresult
                myDataList.HistoryCategory = CategoryType.Labs
                myDataList.AssociatedProperty = "OtherLabsCount"
                arrText.Add(myDataList) '' objTemp.GetFieldName(Convert.ToInt64(oField.HelpText)))
                oLiquidData.ArrText_Field = arrText
                myDataList = Nothing
                arrText = Nothing
                oColLabs.Add(oLiquidData)
                'Change made to solve memory Leak and word crash issue
                If Not oLiquidData Is Nothing Then
                    oLiquidData.Dispose()
                    oLiquidData = Nothing
                End If
            End If
            If InStr(aField.StatusText, "OtherdiagnosticStudies") Then
                myDataList = New myList
                oLiquidData = New clsLiquidData()
                oLiquidData.PatientID = _DocumentCriteria.PatientID
                oLiquidData.mgnVisitID = _DocumentCriteria.VisitID
                oLiquidData.examid = _DocumentCriteria.PrimaryID
                oLiquidData.m_elementId = m_elementId
                oLiquidData.HelpText = 0
                oLiquidData.m_datatype = ""
                oLiquidData.Category = CategoryType.Other_Diagonsis_Tests

                EMresult = aField.Result
                arrText = New ArrayList
                myDataList.HistoryItem = EMresult
                myDataList.HistoryCategory = CategoryType.Other_Diagonsis_Tests
                myDataList.AssociatedProperty = "speOtherDiagnosticStudiesCount"
                arrText.Add(myDataList)
                oLiquidData.ArrText_Field = arrText
                myDataList = Nothing
                arrText = Nothing
                oColOther_Diagonsis_Tests.Add(oLiquidData)
                'Change made to solve memory Leak and word crash issue
                If Not oLiquidData Is Nothing Then
                    oLiquidData.Dispose()
                    oLiquidData = Nothing
                End If
            End If


        End Sub

        Public Sub GetFormFieldData(ByVal DocumentType As enumDocType, Optional ByVal sStatusText As String = "", Optional ByVal GurantorID As Int64 = 0, Optional ByRef oColManagement_option As CollLiquidData = Nothing, Optional ByRef oColLabs As CollLiquidData = Nothing, Optional ByRef oColoColX_Ray_Radiology As CollLiquidData = Nothing, Optional ByRef oColOther_Diagonsis_Tests As CollLiquidData = Nothing) Implements IgloEMRWord.GetFormFieldData

            If IsNothing(_oCurDoc) = True Then
                Exit Sub 'Return Nothing
            End If
            ''added for bugid 89836 finished exam on emr alert change from banner giving exception
            If _oCurDoc.ProtectionType = Microsoft.Office.Interop.Word.WdProtectionType.wdAllowOnlyComments Then
                Exit Sub
            End If
            _oCurDoc.FormFields.Shaded = False 'Make shading false

            _DocumentType = DocumentType
            Dim _HelpText As String
            _HelpText = GetHelpText(DocumentType)

            Dim strfieldsvalcol As String 'Collection of values
            Dim strDataCols(1) As String 'To split collection values with value & Flag
            Dim strData As String 'Split data will be stored in this variable 
            Dim dtTable As DataTable = Nothing

            If IsNothing(_oCurDoc) = True Then
                Exit Sub 'Return Nothing
            End If


            Dim aField As Wd.FormField 'Form field Variable
            Dim isGuarantorAlreadySelected As Boolean = False

            For Each aField In _oCurDoc.FormFields
                If (Not IsNothing(aField)) Then
                    Try
                        Select Case aField.Type
                            Case Wd.WdFieldType.wdFieldFormTextInput
                                '' Compare table name with FormField status text
                                Dim strTableName As String() = Split(aField.StatusText, ".")
                                If strTableName(0) <> "" Then
                                    If (strTableName(0) = _HelpText OrElse _HelpText = "None") AndAlso (sStatusText = "" OrElse sStatusText = aField.StatusText) Then '' 2nd STATEMENT BY SUDHIR 20090706 '' TO FETCH ONLY SINGLE FIELD ''

                                        Select Case aField.StatusText
                                            Case Is <> ""
                                                If (strTableName.Length > 1) Then


                                                    If strTableName(1) <> "iPhoto" AndAlso strTableName(1) <> "imgSignature" AndAlso strTableName(1) <> "iCard|Driving" AndAlso strTableName(1) <> "iCard|Insurance" Then
                                                        If _oCurDoc.ProtectionType = Wd.WdProtectionType.wdNoProtection Then
                                                            Try
                                                                If gblnWordColorHighlight Then
                                                                    aField.Range.HighlightColorIndex = gblnWordBackColor
                                                                Else
                                                                    aField.Range.HighlightColorIndex = Wd.WdColorIndex.wdNoHighlight
                                                                End If
                                                            Catch ex As Exception

                                                            End Try

                                                        End If
                                                    End If
                                                End If
                                                If aField.StatusText.Contains("|Modifier3") Then
                                                    aField.Select()
                                                    'SaveFreeTextForTreatment()
                                                End If
                                                '' SUDHIR 20090711 '' TO FETCH USER NAME / LOGIN NAME ''
                                                'aField.
                                                If aField.StatusText.StartsWith("PA_Accounts_Patients") OrElse aField.StatusText.StartsWith("pa_accounts") OrElse aField.StatusText.StartsWith("Patient_OtherContacts.") OrElse aField.StatusText.StartsWith("pa_accounts_Billing.") OrElse aField.StatusText.StartsWith("pa_accounts_PatientLastClaimDiag.") Then
                                                    If (GurantorID = 0) Then
                                                        If (isGuarantorAlreadySelected = False) Then
                                                            Dim ofrmSelectPatientGuarantor As New frmSelectPatientGuarantor(_DocumentCriteria.PatientID, gnClinicID)
                                                            Dim oClsSelectPatientGuarantor As New clsSelectPatientGuarantor(_DocumentCriteria.PatientID, gnClinicID)
                                                            Dim dtAccounts As DataTable
                                                            dtAccounts = oClsSelectPatientGuarantor.GetPatientAccounts(_DocumentCriteria.PatientID, gnClinicID)
                                                            If (IsNothing(dtAccounts) = False) Then


                                                                If (dtAccounts.Rows.Count = 1) Then
                                                                    _DocumentCriteria.FieldID1 = dtAccounts.Rows(0)("nPAccountID").ToString()
                                                                    isGuarantorAlreadySelected = True
                                                                ElseIf (dtAccounts.Rows.Count > 1) Then
                                                                    ofrmSelectPatientGuarantor.ShowDialog(ofrmSelectPatientGuarantor.Parent)
                                                                    'aField.Result = ofrmSelectPatientGuarantor.SelectedGuarantor
                                                                    If (ofrmSelectPatientGuarantor.DialogResult = DialogResult.OK) Then
                                                                        _DocumentCriteria.FieldID1 = ofrmSelectPatientGuarantor.SelectedAccount
                                                                    Else
                                                                        _DocumentCriteria.FieldID1 = -1
                                                                    End If
                                                                    isGuarantorAlreadySelected = True
                                                                Else
                                                                    _DocumentCriteria.FieldID1 = 0
                                                                End If
                                                            Else
                                                                _DocumentCriteria.FieldID1 = 0

                                                            End If
                                                            If IsNothing(ofrmSelectPatientGuarantor) = False Then
                                                                ofrmSelectPatientGuarantor.Dispose()
                                                                ofrmSelectPatientGuarantor = Nothing
                                                            End If
                                                            If IsNothing(oClsSelectPatientGuarantor) = False Then
                                                                oClsSelectPatientGuarantor.Dispose()
                                                                oClsSelectPatientGuarantor = Nothing
                                                            End If

                                                            dtAccounts.Dispose()
                                                            dtAccounts = Nothing

                                                        End If
                                                    Else
                                                        _DocumentCriteria.FieldID1 = GurantorID
                                                    End If
                                                End If
                                                If aField.StatusText.StartsWith("User_MST") Then
                                                    If aField.StatusText = "User_MST.sLoginName" Then
                                                        aField.Result = gstrLoginName
                                                    ElseIf aField.StatusText.StartsWith("User_MST.sFirstName") Then
                                                        aField.Result = GetDoctorName()
                                                    End If
                                                Else
                                                    '' SUDHIR 20091217 '' TO PRINT SEPERATOR BETWEEN HISTORY & COMMENT ''
                                                    '' ONLY FOR HISTORY ''
                                                    Dim _StatusText As String = aField.StatusText

                                                    ''GLO2011-0010684 : ROS
                                                    ''ROS item and ROS Comment seperated by colon (:).
                                                    If (_StatusText.StartsWith("History") AndAlso _StatusText.EndsWith("Allergy") = False) Then ' Or _StatusText.Contains("ROS.sROSItem+ROS.sComments") Then
                                                        _StatusText = _StatusText.Replace("+", "+':'+")
                                                    End If


                                                    '' strData = getData_FromDB(Replace(_StatusText, "+", "+space(2)+"), aField.HelpText, dtTable)''Before we where providing 2 spaces in between the words now no
                                                    strData = getData_FromDB(Replace(_StatusText, "+", "+space(1)+"), aField.HelpText, dtTable)   ''Now providing only 1 space between the words

                                                    '' chetan added for maintaining past history on Oct 18 2010
                                                    If _DocumentCriteria.PrimaryID <> 0 Then
                                                        If strData.Trim.Length > 2 Then
                                                            CheckDataForPast_ExamDB(_StatusText, aField.HelpText, dtTable)
                                                        End If
                                                    End If

                                                    '' chetan added for maintaining past history on Oct 18 2010

                                                    If _StatusText.StartsWith("History") AndAlso _StatusText.EndsWith("Allergy") = False Then
                                                        ''Added by Mayuri on 20100309-To fix case:#GLO2010-0004643-On exams, after allergies there is an extra : symbol 
                                                        strData = strData.Replace(":  ", "").Replace(": ", "").Replace(" :  : ", "").Replace(" :  ", "").Replace(": |", "|").Replace(":  |", "|").Replace(" :  :  |", "|").Replace(" :  |", "|")



                                                    End If
                                                    If _DocumentCriteria.DocCategory = enumDocCategory.Exam Then
                                                        If gblnSAVELIQUIDDATA Then
                                                            If InStr(aField.StatusText, "History") OrElse InStr(aField.StatusText, "TimeSpent") OrElse InStr(aField.StatusText, "OtherOrders") OrElse InStr(aField.StatusText, "Otherlabs") OrElse InStr(aField.StatusText, "OtherdiagnosticStudies") Then
                                                                FillLiquidFormFieldscollection(aField, oColManagement_option, oColLabs, oColoColX_Ray_Radiology, oColOther_Diagonsis_Tests)
                                                                IsLiquidFormField = True
                                                            End If
                                                        End If
                                                    End If

                                                    '' ONLY FOR HISTORY ''
                                                    '' END SUDHIR ''
                                                    'Lines Added by dipak 20100114 to fix bug no 5284 appointment>Notes>print> Text after special character "|" is NOT displayed in Notes section on printout of appointment template (Test1|Test2)
                                                    '29052012 Case no 00000104:Applied Trim()
                                                    ''commented for Problem:00000186
                                                    ''Start
                                                    'strDataCols(0) = strData.Substring(0, strData.LastIndexOf("|")).Trim()
                                                    ''End
                                                    Dim thisIndex As Integer = strData.LastIndexOf("|")
                                                    If (thisIndex >= 0) Then
                                                        strDataCols(0) = strData.Substring(0, thisIndex)
                                                        If ((strData.Length - thisIndex - 1) > 0) Then
                                                            strDataCols(1) = strData.Substring(thisIndex + 1, strData.Length - thisIndex - 1)
                                                        Else
                                                            strDataCols(1) = ""
                                                        End If
                                                    Else
                                                        strDataCols(0) = strData
                                                        strDataCols(1) = ""
                                                    End If


                                                    Select Case Trim(strDataCols(0))
                                                        Case Is <> ""
                                                            Select Case Len(strDataCols(0))
                                                                Case Is <= 255
                                                                    Select Case strDataCols(1)
                                                                        Case "2"
                                                                            aField.Result = "  "
                                                                            aField.Select()
                                                                            '_oCurDoc.ActiveWindow.Selection.GoTo(What:=Wd.WdGoToItem.wdGoToBookmark, Name:=aField.Name)
                                                                            _oCurDoc.ActiveWindow.Selection.Collapse()
                                                                            _oCurDoc.ActiveWindow.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                                                                            If File.Exists(strDataCols(0)) Then
                                                                                _oCurDoc.ActiveWindow.Selection.InsertFile(strDataCols(0))
                                                                            End If
                                                                            Exit Select
                                                                        Case "3"

                                                                            aField.Result = "  "
                                                                            aField.Select()

                                                                            '_oCurDoc.ActiveWindow.Selection.GoTo(What:=Wd.WdGoToItem.wdGoToObject, Which:=aField, Name:=aField.Name)
                                                                            _oCurDoc.ActiveWindow.Selection.Collapse()
                                                                            _oCurDoc.ActiveWindow.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)

                                                                            If _StatusText.Contains("Patient_Cards.iCard") Then
                                                                                Dim strDatas() As String = strDataCols(0).Split("~")
                                                                                For Each Str As String In strDatas
                                                                                    If File.Exists(Str) Then
                                                                                        Global.gloWord.gloWord.InsertImageIntoSelectionField(_oCurDoc, Str, gstrMessageBoxCaption, "GetFormFieldData1", True)
                                                                                        _oCurDoc.ActiveWindow.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
                                                                                    End If
                                                                                Next
                                                                            Else
                                                                                If File.Exists(strDataCols(0)) Then
                                                                                    If strDataCols(0).Contains("DasCalculater") Then

                                                                                        aField.Result = "DAS"
                                                                                        _oCurDoc.ActiveWindow.Selection.Delete()
                                                                                        _oCurDoc.ActiveWindow.Selection.InlineShapes.AddPicture(strDataCols(0), False, True, _oCurDoc.ActiveWindow.Selection.Range)
                                                                                        _oCurDoc.ActiveWindow.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
                                                                                        _oCurDoc.ActiveWindow.Selection.TypeText("   ")
                                                                                        _oCurDoc.ActiveWindow.Selection.MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=1)
                                                                                        _oCurDoc.ActiveWindow.Selection.ParagraphFormat.SpaceAfter = 5
                                                                                    Else
                                                                                        Global.gloWord.gloWord.InsertImageIntoSelectionField(_oCurDoc, strDataCols(0), gstrMessageBoxCaption, "GetFormFieldData1", True)
                                                                                        'Try
                                                                                        '    Dim oImage As Image = Image.FromFile(strDataCols(0))
                                                                                        '    'Dipak :20100610 :Code Added for Case GLO2010-0005258 to backup clipboard data
                                                                                        '    'end code added by dipak
                                                                                        '    If (IsNothing(oImage) = False) Then


                                                                                        '        Try
                                                                                        '            Global.gloWord.gloWord.GetClipboardData()
                                                                                        '            'end code added by dipak
                                                                                        '            ' Clipboard.Clear()
                                                                                        '            Clipboard.SetImage(oImage)
                                                                                        '        Catch ex As Exception
                                                                                        '            MessageBox.Show("Unable to set image to Clipboard during getting form field data", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                                                        '        End Try

                                                                                        '        'If _StatusText.Contains("nDAS28") Then
                                                                                        '        '    _oCurDoc.ActiveWindow.Selection.Bookmarks.Add("DasImage")
                                                                                        '        'End If
                                                                                        '        Try
                                                                                        '            _oCurDoc.ActiveWindow.Selection.Paste()
                                                                                        '            ' Clipboard.Clear()
                                                                                        '            Global.gloWord.gloWord.SetClipboardData()
                                                                                        '        Catch ex As Exception
                                                                                        '            MessageBox.Show("While getting form field data unable to get image from Clipboard due to locked by " & gloWord.gloWord.GetOpenClipboardWindowText(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                                                        '        End Try

                                                                                        '        oImage.Dispose()
                                                                                        '        oImage = Nothing
                                                                                        '    End If

                                                                                        '    'Dipak :20100610 :Code Added for Case GLO2010-0005258 to backup clipboard data

                                                                                        'Catch ex As Exception
                                                                                        '    MessageBox.Show("While getting form field data unable to get image from Clipboard due to locked by " & gloWord.gloWord.GetOpenClipboardWindowText(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                                                                                        'End Try
                                                                                    End If

                                                                                    'End Code added by dipak
                                                                                    '_oCurDoc.ActiveWindow.Selection.InlineShapes.AddPicture(FileName:=strDataCols(0), LinkToFile:=False, SaveWithDocument:=True).AlternativeText = "gloImageDataField"
                                                                                    _oCurDoc.ActiveWindow.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
                                                                                End If
                                                                            End If

                                                                            Exit Select

                                                                        Case "4"
                                                                            '' For Decimal Datatype
                                                                            aField.Result = Convert.ToDecimal(strDataCols(0))
                                                                        Case "5"
                                                                            '' GetAge In String
                                                                            aField.Result = GetAge(CType(strDataCols(0), Date))
                                                                        Case "6" '' To Generate Data in Tabular Form '' Now for Flowsheet or Vital
                                                                            If (IsNothing(dtTable) = False) Then


                                                                                If dtTable.Rows.Count > 0 Then
                                                                                    aField.Result = "  "
                                                                                    '_oCurDoc.ActiveWindow.Selection.GoTo(What:=Wd.WdGoToItem.wdGoToBookmark, Name:=aField.Name)
                                                                                    aField.Select()
                                                                                    _oCurDoc.ActiveWindow.Selection.Collapse()
                                                                                    _oCurDoc.ActiveWindow.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                                                                                    '_oCurDoc.ActiveWindow.Selection.Expand()
                                                                                    'InsertWordTable(dtTable, aField)
                                                                                    If (aField.HelpText.Contains("Lab Results") = True) Then
                                                                                        InsertWordTableForLabResult(dtTable, aField)
                                                                                    Else
                                                                                        InsertWordTable(dtTable, aField)
                                                                                    End If
                                                                                    If IsNothing(dtTable) = False Then
                                                                                        dtTable.Dispose()
                                                                                        dtTable = Nothing
                                                                                    End If

                                                                                Else
                                                                                    aField.Result = "|" & Replace(aField.HelpText, "|", "") & "|"
                                                                                    If aField.HelpText.Contains("DAS") Then
                                                                                        _oCurDoc.ActiveWindow.Selection.Delete()
                                                                                    End If
                                                                                End If
                                                                            End If
                                                                        Case Else
                                                                            If (strDataCols(0) = "Pos." OrElse strDataCols(0) = "Yes") Then
                                                                                aField.Range.Bold = 1
                                                                            End If
                                                                            aField.Result = strDataCols(0)
                                                                    End Select
                                                                Case Is > 255
                                                                    Select Case strDataCols(1)
                                                                        Case "2"
                                                                            aField.Result = "  "
                                                                            aField.Select()
                                                                            '_oCurDoc.ActiveWindow.Selection.GoTo(What:=Wd.WdGoToItem.wdGoToBookmark, Name:=aField.Name)
                                                                            _oCurDoc.ActiveWindow.Selection.Collapse()
                                                                            _oCurDoc.ActiveWindow.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                                                                            If File.Exists(strDataCols(0)) Then
                                                                                _oCurDoc.ActiveWindow.Selection.InsertFile(strDataCols(0))
                                                                            End If
                                                                            Exit Select
                                                                        Case "3"
                                                                            aField.Result = "  "
                                                                            aField.Select()
                                                                            '_oCurDoc.ActiveWindow.Selection.GoTo(What:=Wd.WdGoToItem.wdGoToBookmark, Name:=aField.Name)
                                                                            _oCurDoc.ActiveWindow.Selection.Collapse()
                                                                            _oCurDoc.ActiveWindow.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)

                                                                            If _StatusText.Contains("Patient_Cards.iCard") Then
                                                                                Dim strDatas() As String = strDataCols(0).Split("~")
                                                                                For Each Str As String In strDatas
                                                                                    If File.Exists(Str) Then
                                                                                        Global.gloWord.gloWord.InsertImageIntoSelectionField(_oCurDoc, Str, gstrMessageBoxCaption, "GetFormFiedlData2", True)
                                                                                    End If
                                                                                Next
                                                                            Else
                                                                                If File.Exists(strDataCols(0)) Then
                                                                                    If strDataCols(0).Contains("DasCalculater") Then
                                                                                        aField.Result = "DAS"
                                                                                        _oCurDoc.ActiveWindow.Selection.Delete()
                                                                                        _oCurDoc.ActiveWindow.Selection.InlineShapes.AddPicture(strDataCols(0), False, True, _oCurDoc.ActiveWindow.Selection.Range)
                                                                                        _oCurDoc.ActiveWindow.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
                                                                                        _oCurDoc.ActiveWindow.Selection.TypeText("   ")
                                                                                        _oCurDoc.ActiveWindow.Selection.MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=1)
                                                                                        _oCurDoc.ActiveWindow.Selection.ParagraphFormat.SpaceAfter = 5
                                                                                    Else

                                                                                        Global.gloWord.gloWord.InsertImageIntoSelectionField(_oCurDoc, strDataCols(0), gstrMessageBoxCaption, "GetFormFiedlData2", True)
                                                                                        'Try
                                                                                        '    Dim oImage As Image = Image.FromFile(strDataCols(0))
                                                                                        '    'Dipak :20100610 :Code Added for Case GLO2010-0005258 to backup clipboard data
                                                                                        '    If (IsNothing(oImage) = False) Then


                                                                                        '        Try
                                                                                        '            Global.gloWord.gloWord.GetClipboardData()
                                                                                        '            ''end code added by dipak
                                                                                        '            'Clipboard.Clear()
                                                                                        '            Clipboard.SetImage(oImage)
                                                                                        '        Catch ex As Exception
                                                                                        '            MessageBox.Show("Unable to set image to Clipboard from getting form field data", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                                                        '        End Try
                                                                                        '        Try
                                                                                        '            _oCurDoc.ActiveWindow.Selection.Paste()
                                                                                        '            'Clipboard.Clear()
                                                                                        '            Global.gloWord.gloWord.SetClipboardData()
                                                                                        '        Catch ex As Exception
                                                                                        '            MessageBox.Show("Unable to get image from Clipboard from getting form field data due to locked by " & gloWord.gloWord.GetOpenClipboardWindowText(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                                                        '        End Try

                                                                                        '        oImage.Dispose()
                                                                                        '        oImage = Nothing
                                                                                        '    End If

                                                                                        '    'Dipak :20100610 :Code Added for Case GLO2010-0005258 to backup clipboard data

                                                                                        'Catch ex As Exception
                                                                                        '    MessageBox.Show("Unable to get image from Clipboard from getting form field data due to locked by " & gloWord.gloWord.GetOpenClipboardWindowText(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                                                                                        'End Try

                                                                                        '    'end code added buy dipak
                                                                                    End If
                                                                                    '_oCurDoc.ActiveWindow.Selection.InlineShapes.AddPicture(FileName:=strDataCols(0), LinkToFile:=False, SaveWithDocument:=True).AlternativeText = "gloImageDataField"

                                                                                End If
                                                                            End If
                                                                            Exit Select
                                                                        Case "4"
                                                                            '' For Decimal Datatype
                                                                            aField.Result = Convert.ToDecimal(strDataCols(0))
                                                                        Case "5"
                                                                            aField.Result = GetAge(CType(strDataCols(0), Date))
                                                                        Case "6" '' To Generate Data in Tabular Form '' Now for Flowsheet or Vital
                                                                            If (IsNothing(dtTable) = False) Then


                                                                                If dtTable.Rows.Count > 0 Then
                                                                                    aField.Result = "  "
                                                                                    aField.Select()
                                                                                    '_oCurDoc.ActiveWindow.Selection.GoTo(What:=Wd.WdGoToItem.wdGoToBookmark, Name:=aField.Name)
                                                                                    _oCurDoc.ActiveWindow.Selection.Collapse()
                                                                                    _oCurDoc.ActiveWindow.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                                                                                    InsertWordTable(dtTable, aField)
                                                                                Else
                                                                                    aField.Result = "|" & Replace(aField.HelpText, "|", "") & "|"
                                                                                End If
                                                                            End If
                                                                        Case Else
                                                                            aField.Result = "  "
                                                                            aField.Select()
                                                                            '_oCurDoc.ActiveWindow.Selection.GoTo(What:=Wd.WdGoToItem.wdGoToBookmark, Name:=aField.Name)
                                                                            _oCurDoc.ActiveWindow.Selection.Collapse()
                                                                            _oCurDoc.ActiveWindow.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                                                                            _oCurDoc.ActiveWindow.Selection.TypeText(strDataCols(0))
                                                                            _oCurDoc.ActiveWindow.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                                                                    End Select
                                                            End Select
                                                        Case Else
                                                            aField.Result = "|" & Replace(aField.HelpText, "|", "") & "|"

                                                    End Select

                                                End If '' USER_MST END IF ''

                                        End Select
                                    End If
                                End If
                        End Select
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, "aField.StatusText - " & aField.StatusText & " - " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    End Try
                End If

            Next aField

            '08-May-13 Aniket: Resolving Memory Leaks
            If IsNothing(dtTable) = False Then
                dtTable.Dispose()
                dtTable = Nothing
            End If

            aField = Nothing ' set nothing
            strfieldsvalcol = Nothing 'Set nothing
        End Sub



        Public Sub CheckDataForPast_ExamDB(ByVal strFields As String, Optional ByVal strHelpError As String = "", Optional ByRef dtTable As DataTable = Nothing)
            Try
                'Parameter Varibles - Start
                Dim _PatientID As Int64 = _DocumentCriteria.PatientID
                Dim _DocCategory As Int32 = _DocumentCriteria.DocCategory
                Dim _PrimaryID As Int64 = _DocumentCriteria.PrimaryID
                Dim _VisitID As Int64 = _DocumentCriteria.VisitID
                Dim _AppointmetID As Int64 = _DocumentCriteria.AppointmentID

                'Parameter Varibles - Finish

                Dim oDB As New DataBaseLayer
                Dim oParamater As DBParameter


                '' SUDHIR 20091212 '' FETCH CLOSEST POSSIBLE APPOINTMENT ID ''
                If strFields.EndsWith("CurrentAppointment") OrElse strFields.StartsWith("AS_Appointment_DTL") Then
                    Dim _Query As String = " SELECT TOP 1 nMSTAppointmentID, dtStartDate FROM AS_Appointment_MST " & " WHERE dtStartDate <= " & gloDateMaster.gloDate.DateAsNumber(Now.Date.ToShortDateString()) & " AND nPatientID = " & _PatientID & " ORDER BY dtStartDate DESC"
                    Dim oResult As Object
                    Dim _DB As New gloDatabaseLayer.DBLayer(GetConnectionString)
                    _DB.Connect(False)
                    oResult = _DB.ExecuteScalar_Query(_Query)
                    If oResult IsNot Nothing AndAlso oResult.ToString() <> "" Then
                        _PrimaryID = Convert.ToInt64(oResult)
                    End If
                    oResult = Nothing
                    _DB.Disconnect()
                    _DB.Dispose()
                    _DB = Nothing
                End If


                'Dim oResultTable As New DataTable
                'Dim dvICD9CPT As New DataView
                'Dim dtICD9CPT As New DataTable

                Dim strData As String
                Dim ResultDataType As Integer
                ' Dim filecnt As Int16
                Dim strDataCol As String

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@nPrimaryID"
                oParamater.Value = _PrimaryID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@nVisitID"
                oParamater.Value = _VisitID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing


                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.VarChar
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@UserName"
                oParamater.Value = gstrLoginName
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing



                '' gloPM PARAMETERS END ''

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.VarChar
                oParamater.Name = "@sFields"
                oParamater.Direction = ParameterDirection.Input

                strData = ""
                If strFields <> "" Then

                    If strFields.StartsWith("FlowSheet") AndAlso strFields.EndsWith("SingleRow") Then
                        oParamater.Value = Mid(strFields, 1, InStrRev(strFields, "|") - 1)
                    Else
                        oParamater.Value = strFields
                    End If
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    If strFields.StartsWith("FAX") Then
                        strDataCol = strData & "|" & ResultDataType.ToString
                        If Not IsNothing(oDB) Then  ''slr free oDB
                            oDB.Dispose()
                        End If
                        oDB = Nothing
                        Exit Sub
                        ' Return strDataCol
                    End If
                    ''''''''''' Modified the SP to add new fields to Word Doc by Ujwala 

                    oDB.ExecuteNon_Query("gsp_InsertPatientExamHistory")
                    ''''''''''' Modified the SP to add new fields to Word Doc by Ujwala 
                    '    If oResultTable Is Nothing Then

                    ''22-Mar-13 Aniket: Resolving Memory Leak Issues
                    oDB.Dispose()
                    oDB = Nothing

                End If

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Select, "Error while retrieving data for the DataField - '" & strHelpError & "' in the Template" & vbLf & ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try

        End Sub


        '''' <summary>
        ''''  To get the DataDictionary_MST
        '''' </summary>
        '''' <returns></returns>
        '''' <remarks></remarks>
        Public Function GetDatadictionary() As DataTable
            Dim oDB As New DataBaseLayer
            ' Dim oParamater As DBParameter
            Dim oResultTable As New DataTable
            Dim strSQL As String
            strSQL = "Select sFieldName,sCaption from DataDictionary_MST"

            Try
                oResultTable = oDB.GetDataTable_Query(strSQL)
                If Not oResultTable Is Nothing Then
                    Return oResultTable
                End If
                Return Nothing
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                Return Nothing
            Finally
                If Not IsNothing(oResultTable) Then
                    'oResultTable.Dispose()
                    oResultTable = Nothing
                End If

                '22-Mar-13 Aniket: Resolving Memory Leak Issues
                If Not IsNothing(oDB) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If

            End Try

        End Function

        Public Function getHistoryAfterDate(ByVal dtLetterDOS As Date, ByVal CurrentVisitID As Int64) As Boolean
            Dim cls As New clsPatientExams

            '22-Mar-13 Aniket: Resolving Memory Leak Issues
            Dim dt As DataTable

            'Line commented and modifid by dipak 20100826 for case UC5070.003 to limit use of gnpatientID
            'dt = cls.getHistoryVisitsAfterDate(dtLetterDOS, gnPatientID)
            dt = cls.getHistoryVisitsAfterDate(dtLetterDOS, nPatientID)
            Try
                'end modification
                If Not IsNothing(cls) Then  ''slr free cls
                    cls.Dispose()
                End If

                cls = Nothing
                If (IsNothing(dt) = False) Then


                    If dt.Rows.Count > 0 Then
                        If Format(dt.Rows(0)("VisitDate"), "MM/dd/yyyy") <> Format(dtLetterDOS, "MM/dd/yyyy") Then
                            '' History Not Exists on dtDOS
                            '' If History of the DOS is not exists then ask user about Pull History 
                            If MessageBox.Show("History of the date '" & dtLetterDOS & "' is not entered." & vbCrLf & "Do you want to pull the History of the date '" & Format(dt.Rows(0)("VisitDate"), "MM/dd/yyyy") & "'?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                                Return RetriveAndSaveHistory(dt.Rows(0)("VisitID"), CurrentVisitID)
                            Else
                                Return False
                            End If
                        Else
                            Return False
                        End If
                    Else
                        Return True
                    End If
                Else
                    Return True
                End If
            Catch ex As Exception
                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If
                If Not IsNothing(cls) Then
                    cls = Nothing
                End If
                Return False
            Finally
                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If
            End Try
        End Function

        Private Function RetriveAndSaveHistory(ByVal VisitID As Long, ByVal CurrentVisitID As Int64) As Boolean
            Dim cls As New clsPatientHistory

            '22-Mar-13 Aniket: Resolving Memory Leak Issues
            Dim dtHistory As DataTable
            Dim dtNarration As DataTable

            Dim ArrLst As New ArrayList

            '' Get the History of Patient of the next date(next VisitID) 
            'Line commented and modifid by dipak 20100826 for case UC5070.003 to limit use of gnPatientID
            'dtHistory = cls.SelectPatientHistory(VisitID, gnPatientID)
            dtHistory = cls.SelectPatientHistory(VisitID, nPatientID)

            '' Get the History-Narration of Patient of the next date(next VisitID) 
            'dtNarration = cls.SelectNarration(VisitID, gnPatientID)
            dtNarration = cls.SelectNarration(VisitID, nPatientID)
            'end modification by dipak

            '' Put Histroy in ArrayList to save 
            Dim lst As myList
            If (IsNothing(dtHistory) = False) Then


                With dtHistory
                    For i As Integer = 0 To dtHistory.Rows.Count - 1
                        lst = New myList
                        ''sHistoryCategory,sHistoryItem,sComments
                        lst.HistoryCategory = .Rows(i)(0)
                        lst.HistoryItem = .Rows(i)(1)
                        lst.Description = .Rows(i)(2)
                        lst.Reaction = .Rows(i)(3) ''Reaction 
                        lst.ID = .Rows(i)("nDrugID")  ''  DrugID
                        lst.MedicalConditionID = .Rows(i)("MedicalCondition_Id")  ''  MedicalCondition ID 
                        lst.HxDrugName = .Rows(i)("sHistoryItem")
                        ' lst.HxDrugDosage = .Rows(i)("sDosage")

                        lst.HxNDCCode = .Rows(i)("sNDCCode")
                        ' ''Added Rahul on 2010104
                        lst.DOEOAllergy = .Rows(i)("DOEAllergy")
                        lst.ConceptId = .Rows(i)("sConceptID")
                        lst.DescId = .Rows(i)("sDescriptionID")
                        lst.SnowMadeID = .Rows(i)("sSnoMedID")
                        lst.SnoDescription = .Rows(i)("sDescription")
                        lst.ICD9 = .Rows(i)("sICD9")
                        lst.RxNormID = .Rows(i)("sTranID1")
                        ''




                        ''Added Rahul for Insert Smokingstatus on 20101004
                        'If StrCategory = "Smoking Status" Then
                        '    lst.Reaction = strSmokingstatus & "|" & strActive
                        'End If
                        ArrLst.Add(lst)
                    Next
                End With
            End If
            '' Save History on DOS 
            'Line commented and modifid by dipak 20100826 for case UC5070.003 to limit use of gnPatientID
            'cls.AddNewHistory_New(0, CurrentVisitID, gnPatientID, ArrLst, False)
            cls.AddNewHistory_New(0, CurrentVisitID, nPatientID, ArrLst, False)
            'end modification

            If (IsNothing(dtNarration) = False) Then


                With dtNarration
                    If .Rows.Count > 0 Then
                        'Dim mstream As ADODB.Stream
                        'Dim strFileName As String
                        'mstream = New ADODB.Stream
                        'mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
                        'mstream.Open()
                        ' '' Write Contents to the Stream
                        'mstream.Write(.Rows(0)(0))
                        'strFileName = gloSettings.FolderSettings.AppTempFolderPath & "Temp5.txt"
                        ' '' SUDHIR 20100102 '' READONLY FILE WAS GIVING WRITE ERROR ''
                        'If File.Exists(strFileName) Then
                        '    Dim oFileInfo As New FileInfo(strFileName)
                        '    oFileInfo.IsReadOnly = False
                        '    oFileInfo = Nothing
                        'End If
                        '' END SUDHIR ''
                        '' Write contents of ADODB Stream to Temporary Text File
                        'mstream.SaveToFile(strFileName, ADODB.SaveOptionsEnum.adSaveCreateOverWrite)
                        'mstream.Close()

                        ''22-Mar-13 Aniket: Resolving Memory Leak Issues
                        'mstream = Nothing


                        '' Save Narration as on Date-DOS
                        'Line commented and modifid by dipak 20100826 for case UC5070.003 to limit use of gnPatientID
                        'cls.SaveNarration(CurrentVisitID, gnPatientID, strFileName)
                        Dim bBytes() As Byte = .Rows(0)(0)
                        If (IsNothing(bBytes) = False) Then
                            cls.SaveNarrationBytes(CurrentVisitID, nPatientID, bBytes)
                        End If

                        'end modification

                    End If
                End With
            End If
            If (IsNothing(cls) = False) Then
                cls.Dispose()
            End If
            cls = Nothing
            If (IsNothing(dtHistory) = False) Then
                dtHistory.Dispose()
                dtHistory = Nothing

            End If
            If (IsNothing(dtNarration) = False) Then
                dtNarration.Dispose()
                dtNarration = Nothing

            End If

            '22-Mar-13 Aniket: Resolving Memory Leak Issues
            ArrLst.Clear()
            ArrLst = Nothing
            Return Nothing
        End Function

#End Region


        '' SUDHIR 20091012 ''
        '' IF NO DATA AVAILABLE IN ROW THEN REMOVE THAT ROW ''
        Private Function RemoveBlankRows(ByVal dtTable As DataTable, ByVal enmDataDictionaryType As clsDataDictionary.enumDictionaryType) As DataTable
            If (IsNothing(dtTable)) Then
                Return dtTable
            End If
            Try
                Select Case enmDataDictionaryType
                    Case clsDataDictionary.enumDictionaryType.Vitals
                        Dim _BlankRow As Boolean
                        For iRow As Integer = dtTable.Rows.Count - 1 To 0 Step -1

                            _BlankRow = True
                            For iCol As Integer = 1 To dtTable.Columns.Count - 1
                                If Convert.ToString(dtTable.Rows(iRow)(iCol)) <> "" Then
                                    _BlankRow = False
                                    Exit For
                                End If
                            Next

                            If _BlankRow Then
                                dtTable.Rows.RemoveAt(iRow)
                            End If
                        Next
                End Select
                Return dtTable
            Catch ex As Exception
                Return dtTable
            End Try
        End Function
        '''''''''Code shifted from MU Certification branch to 6000 by Sagar Ghodke
        ''''''''Code need to be verified
        ''' '''' Export Function for Word Docs added by Ujwala Atre as on 18032010        
        Public Function ExportData(
                                  ByVal oCurDoc1 As Wd.Document,
                                  ByVal sFilename As String,
                                  ByVal CleanupDocument As Boolean,
                                  Optional ByVal sDocType As String = "Document",
                                  Optional ByVal oParent As Form = Nothing) As Boolean

            ExportData = False
            If oCurDoc1 Is Nothing Then
                Exit Function
            End If
            'Added code for Bug #66970: 00000152 : Liquid Links
            Dim objWord As New clsWordDocument
            objWord.CurDocument = oCurDoc1

            ' Added by Ashish on 22 May 2014
            ' to prevent Exception if Document is already cleaned.
            If CleanupDocument Then objWord.CleanupDoc()

            oCurDoc1 = objWord.CurDocument
            objWord = Nothing

            Dim oResult As DialogResult = MessageBox.Show("Do you want to encrypt the document?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            Dim blnSecureDoc As Boolean = False
            Dim blnSecureContinue As Boolean = False
            Dim sEncryotKey As String = ""
            Dim bEncryptedExe As Boolean = True

            If oResult = DialogResult.Yes Then

                Dim ofrmExportEncryption As New frmExportEncryption
                ofrmExportEncryption.ShowInTaskbar = False
                ofrmExportEncryption.ShowDialog(oParent)

                If ofrmExportEncryption.DialogResult = DialogResult.OK Then
                    sEncryotKey = ofrmExportEncryption.sEncryptKey
                    bEncryptedExe = ofrmExportEncryption.bEncryptedExe
                    blnSecureDoc = True
                    blnSecureContinue = True
                ElseIf ofrmExportEncryption.DialogResult = DialogResult.Cancel Then
                    blnSecureDoc = False
                    blnSecureContinue = False

                End If

                ofrmExportEncryption.Dispose()
                ofrmExportEncryption = Nothing

            End If

            'If blnSecureContinue = False Then Exit Function

            '''''''''''''
            Dim Sfd1 As New SaveFileDialog
            Dim SaveFrmt As Wd.WdSaveFormat
            ''Word Macro-Enabled Document(*.docm) |*.docm |
            ''Word Macro-Enabled Template(*.dotm) |*.dotm |
            ''
            Sfd1.Filter = "Word 97-2003 Document(*.doc) |*.doc |Word Document(*.docx) |*.docx |Word Template(*.dotx) |*.dotx |Word 97-2003 Template(*.dot) |*.dot |RichText Format (*.rtf) |*.rtf |Adobe Acrobat (*.pdf) |*.pdf |XPS Document(*.xps) |*.xps |Web Page(*.htm;*.html) |*.htm;*.html |Plain Text(*.txt) |*.txt |Word XML Document(*.xml) |*.xml "
            Sfd1.FilterIndex = 2
            Sfd1.RestoreDirectory = True
            'Sfd1.CreatePrompt = True
            Sfd1.FileName = sFilename
            If Sfd1.ShowDialog(System.Windows.Forms.Form.ActiveForm) = Windows.Forms.DialogResult.OK Then
                Select Case Sfd1.FilterIndex
                    Case 1
                        SaveFrmt = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocument97
                    Case 2
                        SaveFrmt = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocument
                    Case 3
                        SaveFrmt = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatTemplate
                    Case 4
                        SaveFrmt = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatTemplate97
                    Case 5
                        SaveFrmt = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatRTF
                    Case 6
                        SaveFrmt = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF
                    Case 7
                        SaveFrmt = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatXPS
                    Case 8
                        SaveFrmt = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatHTML
                    Case 9
                        SaveFrmt = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatText
                    Case 10
                        SaveFrmt = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatXMLDocument
                End Select


                If SaveFrmt = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocument Then
                    ''                    
                    If IsNothing(oCurDoc1) = False Then
                        If oCurDoc1.Saved = False Then
                            'Saveit = MessageBox.Show("Do you want to save the changes to " & sDocType & "?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                            'If Saveit = MsgBoxResult.Yes Then
                            oCurDoc1.SaveAs(oCurDoc1.FullName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                            'End If
                        End If
                    End If
                    FileSystem.FileCopy(oCurDoc1.FullName, Sfd1.FileName)
                Else
                    oCurDoc1.SaveAs(Sfd1.FileName, SaveFrmt, False, "", False)
                End If

                FileSystem.FileClose()

                If blnSecureDoc = True AndAlso blnSecureContinue = True Then

                    Dim _retVal As String
                    _retVal = ""
                    _retVal = gloSecurity.gloEncryption.PerformFileEncryption(Sfd1.FileName, sEncryotKey, bEncryptedExe)

                    If Not IsNothing(_retVal) AndAlso _retVal.Trim() <> "" Then
                        ' Dim oFile As System.IO.File
                        File.Delete(Sfd1.FileName)
                    End If

                    Select Case sDocType
                        Case "Form Gallery"
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Encryption, gloAuditTrail.ActivityType.Export, "Form Gallery Exported with Encryption", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                        Case "Disclosure Management"
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.Encryption, gloAuditTrail.ActivityType.Export, "Disclosure Management with Encryption", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                        Case "Patient Message"
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.Encryption, gloAuditTrail.ActivityType.Export, "Patient Message Exported with Encryption", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                        Case "Nurses Note"
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.Encryption, gloAuditTrail.ActivityType.Export, "Nurses Note Exported with Encryption", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                        Case "Patient Consent"
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.Encryption, gloAuditTrail.ActivityType.Export, "Patient Consent Exported with Encryption", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                        Case "Patient Exam"
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Encryption, gloAuditTrail.ActivityType.Export, "Patient Exam Exported with Encryption", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                        Case "PT Protocol"
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.Encryption, gloAuditTrail.ActivityType.Export, "PT Protocol Exported with Encryption", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                        Case "Referral letter"
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.Encryption, gloAuditTrail.ActivityType.Export, "Referral letter Exported with Encryption", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                        Case "Triage"
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.Encryption, gloAuditTrail.ActivityType.Export, "Triage Exported with Encryption", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                        Case "Patient letter"
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.Encryption, gloAuditTrail.ActivityType.Export, "Patient letter Exported with Encryption", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                        Case Else
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.Encryption, gloAuditTrail.ActivityType.Export, sDocType & " Exported with Encryption", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                    End Select
                Else
                    Select Case sDocType
                        Case "Form Gallery"
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Export, "Form Gallery Exported without Encryption", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                        Case "Disclosure Management"
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Export, "Disclosure Management without Encryption", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                        Case "Patient Message"
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Export, "Patient Message Exported without Encryption", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                        Case "Nurses Note"
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Export, "Nurses Note Exported without Encryption", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                        Case "Patient Consent"
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Export, "Patient Consent Exported without Encryption", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                        Case "Patient Exam"
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Export, "Patient Exam Exported without Encryption", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                        Case "PT Protocol"
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Export, "PT Protocol Exported without Encryption", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                        Case "Referral letter"
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Export, "Referral letter Exported without Encryption", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                        Case "Triage"
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Export, "Triage Exported without Encryption", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                        Case "Patient letter"
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Export, "Patient letter Exported without Encryption", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                        Case Else
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.Encryption, gloAuditTrail.ActivityType.Export, sDocType & " Exported without Encryption.", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                    End Select
                End If

                ExportData = True

                Sfd1.Dispose()
                Sfd1 = Nothing
                objWord = Nothing
            End If

        End Function


        'Developer:Yatin N. Bhagat
        'Date:01/31/2012
        'Bug ID/PRD Name/Salesforce Case:Provider Signature Format Case
        'Reason: Comman Fucntionality is added 

        Public Function GetProviderSignature(ByVal ProviderId As Long, ByVal m_PatientID As Long, ByVal VisitId As Long, ByVal blnSignClick As Boolean) As String()
            'Bug #45704: 00000394 : EMR Settings
            'Changes integrated from 7020
            Try
                Dim ProviderSign() As String = {"", "", "0"}
                Dim rslt As Boolean
                Dim oclsProvider As New clsProvider
                rslt = oclsProvider.CheckSignDelegateStatus()
                Dim _ProviderID As Int64

                'Resolved : Exam - provider signature - Signature Delegates feature is not working
                'Bug:42445
                If ProviderId <> 0 Then
                    Dim blnResult As Boolean
                    Dim Pat_Provider As String
                    Dim SelectedProvider As String
                    Dim dResult As DialogResult
                    blnResult = oclsProvider.CheckpatientProviderStatus(m_PatientID, ProviderId)
                    If blnSignClick = False Then
                        If blnResult Then
                            ''Selected Provider Is Exam Provider
                        Else
                            Pat_Provider = oclsProvider.GetPatientProviderName(m_PatientID)
                            SelectedProvider = oclsProvider.GetProviderName(ProviderId)
                            dResult = MessageBox.Show("Patient provider '" & Pat_Provider & "' does not match provider selected for signature '" & SelectedProvider & "'.  Would you like to continue?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            If dResult = Windows.Forms.DialogResult.Yes Then
                                ''Insert The Selectedd Provider Sign
                            Else
                                ''Insert PAtient Provider Sign
                                If Not IsNothing(oclsProvider) Then  ''slr free oclsProvider
                                    oclsProvider.Dispose()
                                End If
                                oclsProvider = Nothing

                                Return ProviderSign
                            End If
                        End If
                        If dResult <> Windows.Forms.DialogResult.Yes Then
                            If rslt Then
                                _ProviderID = oclsProvider.GetPatientProvider(m_PatientID)
                                Dim dt As DataTable
                                Dim _IsSignRight As Boolean = False
                                Dim i As Int16

                                '08-May-13 Aniket: Resolving Memory Leaks
                                'dt = New DataTable
                                dt = oclsProvider.GetAllAssignProviders(gnLoginID)
                                If (IsNothing(dt)) Then
                                    If _ProviderID <> gnLoginProviderID Then
                                        MessageBox.Show("You are not designated as a Signature Delegate for any providers.  No provider signatures are available for you to use. Signature delegates may be added in Provider setup in gloEMR Admin.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        If Not IsNothing(oclsProvider) Then  ''slr free oclsProvider
                                            oclsProvider.Dispose()
                                        End If
                                        oclsProvider = Nothing
                                    End If
                                    Return ProviderSign
                                End If
                                If dt.Rows.Count = 0 Then
                                    If _ProviderID <> gnLoginProviderID Then
                                        MessageBox.Show("You are not designated as a Signature Delegate for any providers.  No provider signatures are available for you to use. Signature delegates may be added in Provider setup in gloEMR Admin.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        If Not IsNothing(oclsProvider) Then  ''slr free oclsProvider
                                            oclsProvider.Dispose()
                                        End If
                                        oclsProvider = Nothing
                                        If Not IsNothing(dt) Then
                                            dt.Dispose()
                                            dt = Nothing
                                        End If
                                        Return ProviderSign
                                    End If
                                Else
                                    If dt.Rows.Count > 0 Then
                                        For i = 0 To dt.Rows.Count - 1
                                            If _ProviderID = dt.Rows(i)("nProviderId").ToString().Trim() OrElse _ProviderID = gnLoginProviderID Then
                                                _IsSignRight = True
                                            End If
                                        Next
                                        If _IsSignRight = False Then
                                            Dim strName As String = oclsProvider.GetPatientProviderNameWithPrefix(m_PatientID)
                                            MessageBox.Show("User '" & gstrLoginName & "' is not designated as a Signature Delegate for '" & strName & "'. Signature Delegates may be assigned via the ‘Provider’ option in gloEMR Admin.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                            If Not IsNothing(oclsProvider) Then  ''slr free oclsProvider
                                                oclsProvider.Dispose()
                                            End If
                                            oclsProvider = Nothing
                                            If Not IsNothing(dt) Then
                                                dt.Dispose()
                                                dt = Nothing
                                            End If
                                            Return ProviderSign
                                        End If
                                    End If
                                End If
                                'Dispose object by mitesh
                                If Not IsNothing(dt) Then
                                    dt.Dispose()
                                    dt = Nothing
                                End If
                            End If
                        End If
                    End If
                Else
                    If rslt Then
                        _ProviderID = oclsProvider.GetPatientProvider(m_PatientID)
                        Dim dt As DataTable
                        Dim _IsSignRight As Boolean = False
                        Dim i As Int16

                        '08-May-13 Aniket: Resolving Memory Leaks
                        'dt = New DataTable
                        dt = oclsProvider.GetAllAssignProviders(gnLoginID)
                        If (IsNothing(dt)) Then
                            If _ProviderID <> gnLoginProviderID Then
                                MessageBox.Show("You are not designated as a Signature Delegate for any providers.  No provider signatures are available for you to use. Signature delegates may be added in Provider setup in gloEMR Admin.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                If Not IsNothing(oclsProvider) Then  ''slr free oclsProvider
                                    oclsProvider.Dispose()
                                End If
                                oclsProvider = Nothing


                                'Exit Function
                                ''Return
                            End If
                            Return ProviderSign
                        End If
                        If dt.Rows.Count = 0 Then
                            If _ProviderID <> gnLoginProviderID Then
                                MessageBox.Show("You are not designated as a Signature Delegate for any providers.  No provider signatures are available for you to use. Signature delegates may be added in Provider setup in gloEMR Admin.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                If Not IsNothing(oclsProvider) Then  ''slr free oclsProvider
                                    oclsProvider.Dispose()
                                End If
                                oclsProvider = Nothing
                                If Not IsNothing(dt) Then
                                    dt.Dispose()
                                    dt = Nothing
                                End If
                                Return ProviderSign
                                'Exit Function
                                ''Return
                            End If
                        Else
                            If dt.Rows.Count > 0 Then
                                For i = 0 To dt.Rows.Count - 1
                                    If _ProviderID = dt.Rows(i)("nProviderId").ToString().Trim() OrElse _ProviderID = gnLoginProviderID Then
                                        _IsSignRight = True
                                    End If
                                Next
                                If _IsSignRight = False Then
                                    Dim strName As String = oclsProvider.GetPatientProviderNameWithPrefix(m_PatientID)
                                    MessageBox.Show("User '" & gstrLoginName & "' is not designated as a Signature Delegate for '" & strName & "'. Signature Delegates may be assigned via the ‘Provider’ option in gloEMR Admin.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    If Not IsNothing(oclsProvider) Then  ''slr free oclsProvider
                                        oclsProvider.Dispose()
                                    End If
                                    oclsProvider = Nothing
                                    If Not IsNothing(dt) Then
                                        dt.Dispose()
                                        dt = Nothing
                                    End If
                                    Return ProviderSign
                                    'Exit Function
                                    ''Return
                                End If
                            End If
                        End If
                        'Dispose object by mitesh
                        If Not IsNothing(dt) Then
                            dt.Dispose()
                            dt = Nothing
                        End If
                    End If
                End If
                Dim objWord As New clsWordDocument
                Dim objCriteria As DocCriteria
                objCriteria = New DocCriteria
                objCriteria.ProviderID = ProviderId
                objCriteria.DocCategory = enumDocCategory.Others
                objCriteria.PatientID = m_PatientID
                objCriteria.VisitID = 0
                ''Added on 20101007 by snajog for signature
                objCriteria.ProviderID = ProviderId
                ''Added on 20101007 by snajog for signature
                objCriteria.PrimaryID = 0
                '' SUDHIR 20091229 '' IF LOGIN USER IS A PROVIDER THEN FETCH PROVIDERS SIGNATURE ''
                If gnLoginProviderID > 0 Then
                    '' Prolem : 00000071
                    '' Description : Provider Sign' button pulls in the patient's provider's name into the signature block text when the signature delegates feature is disabled in gloEMR Admin
                    '' Reason for change : If login Provider is equal to ProviderID (supplied) then Insert LoginProvider Signature, Else Patient provider Signature should be inserted.
                    If gnLoginProviderID = ProviderId Then
                        objCriteria.DocCategory = enumDocCategory.Addendum
                        objCriteria.VisitID = 0
                        objCriteria.PrimaryID = gnLoginProviderID '' For inserting Login Provider Signature ''
                    Else
                        objCriteria.DocCategory = enumDocCategory.Others
                        objCriteria.PatientID = m_PatientID
                        objCriteria.VisitID = VisitId
                        objCriteria.PrimaryID = 0
                    End If
                Else  '' ELSE FETCH PATIENT PROVIDERS SIGNATURE ''
                    objCriteria.DocCategory = enumDocCategory.Others
                    objCriteria.PatientID = m_PatientID
                    objCriteria.VisitID = VisitId
                    objCriteria.PrimaryID = 0
                End If
                '' END SUDHIR ''
                Dim clsExam As New clsPatientExams
                Dim PatientProviderId As Long = clsExam.GetProviderIdforPatient(m_PatientID)

                Dim strProviderName As String
                If ProviderId <> 0 Then
                    strProviderName = clsExam.GetProvidernameforExam(ProviderId)
                Else
                    If _ProviderID = 0 Then
                        _ProviderID = PatientProviderId
                    End If
                    strProviderName = clsExam.GetProvidernameforExam(_ProviderID)
                End If

                objWord.DocumentCriteria = objCriteria
                Dim ImagePath As String = ""
                ImagePath = objWord.getData_FromDB("Provider_MST.imgSignature", "Provider Signature")
                objCriteria.Dispose()
                objCriteria = Nothing
                objWord = Nothing
                ImagePath = Mid(ImagePath, 1, Len(ImagePath) - 2)
                If ImagePath = "" Then
                    If blnSignClick = True Then
                        MessageBox.Show("Current user has no signature on file.  Electronic signature cannot be added.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show("Selected Provider has no signature on file.  Electronic signature cannot be added.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                    If IsNothing(clsExam) = False Then
                        clsExam.Dispose()
                        clsExam = Nothing
                    End If

                    If IsNothing(oclsProvider) = False Then
                        oclsProvider.Dispose()
                        oclsProvider = Nothing
                    End If
                    Return ProviderSign
                End If

                Dim pSign As String = ""

                'Developer: Yatin N.Bhagat
                'Date:01/20/2012
                'Bug ID/PRD Name/Salesforce Case:Salesforce Case No.GLO2010-0009688 
                'Reason: If Condition is added to check the Setting to add login user name in the Sign
                Dim pName() As String = strProviderName.Split(" ")
                Dim ProviderName As String = ""
                For i As Integer = 1 To pName.Length - 1
                    Try
                        ProviderName = ProviderName + " " + pName(i)
                    Catch ex As Exception

                    End Try
                Next

                Dim signFormat As Integer = oclsProvider.AddUserNameInProviderSignature()

                If signFormat = 1 Then
                    pSign = gstrSignatureText & " '" & strProviderName & "'. " & Format(Now, "MM/dd/yyyy") & " " & System.DateTime.Now.ToString("hh:mm:ss tt") & " (" & gstrLoginName & ")"
                ElseIf signFormat = 2 Then
                    pSign = gstrSignatureText & " '" & ProviderName & "'. " & Format(Now, "MM/dd/yyyy") & " " & System.DateTime.Now.ToString("hh:mm:ss tt") & " (" & gstrLoginName & ")"
                ElseIf signFormat = 3 Then
                    pSign = gstrSignatureText & " '" & strProviderName & "'. " & Format(Now, "MM/dd/yyyy") & " " & System.DateTime.Now.ToString("hh:mm:ss tt") '& " (" & gstrLoginName & ")"
                ElseIf signFormat = 4 Then
                    pSign = gstrSignatureText & " '" & ProviderName & "'. " & Format(Now, "MM/dd/yyyy") & " " & System.DateTime.Now.ToString("hh:mm:ss tt")  '& " (" & gstrLoginName & ")"
                End If

                ProviderSign(0) = ImagePath
                ProviderSign(1) = pSign
                ProviderSign(2) = "1"

                '08-May-13 Aniket: Resolving Memory Leaks
                If IsNothing(clsExam) = False Then
                    clsExam.Dispose()
                    clsExam = Nothing
                End If

                If IsNothing(oclsProvider) = False Then
                    oclsProvider.Dispose()
                    oclsProvider = Nothing
                End If


                Return ProviderSign
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                Return Nothing
            Finally

            End Try
        End Function


    End Class

    '''' <summary>
    '''' To Fill the Respective Template Selection combobox
    '''' </summary>
    '''' <remarks></remarks>
    Public Enum enumTemplateFlag
        Labs = 1
        Radiology = 2
        Tags = 4
        Restriction = 5
        Messages = 8
        PatientLetters = 9
        ReferralLetter = 10
        Soap = 11
        PTProtocol = 12
        WellGuidelines = 13
        DiseaseManagement = 14
        PatientConsent = 16
        Fax = 17
        PatientEducation = 99
        DisclosureMangement = 18
        NurseNotes = 19
        Triage = 20
    End Enum

    '// Document type for retreive Specific data to avoid repetition
    Public Enum enumDocType
        None = 0
        Diagnosis = 1
        Treatment = 2
        Prescription = 3
        RadiologyOrders = 4
        Vitals = 5
        ROS = 6
        History = 7
        Medication = 8
        PatientEducation = 9
        Flowsheet = 10
        Referrals = 11
        SmartDiagnosis = 12
        ProblemList = 13
        SmartTreatment = 14
        Tasks = 15
        CheifComplaints = 16
        PatientDemographics = 17
        PatientGuideline = 18
        Others = 19
        Contacts = 20
        Narration = 21
        ProviderSign = 22
        LabOrders = 23
        Clinic = 24
        PatientExam = 25
        Fax = 26
        Providers = 27
        DisclosureSet = 28
        Intervention = 29
        PatientExamDos = 30
        PatientExamsDx = 31
        PatientDetails = 32
        Catheterization = 33
        StressTest = 34
        ElectroPhysiology = 35
        CardiologyDevice = 36
        ElectroCardioGrams = 37
        Echocardiogram = 38
        DAS = 39
        OBVitals = 40
        PastPregancies = 41
        OBGeneticHistory = 42
        OBInfectionHistory = 43
        OBInitialPhysicalExamination = 44
        OBMedicalHistory = 45
        OBPlan = 46
        '21-May-15 Aniket: Open Modify Patient on Patient Insurance Liquid Link
        PatientInsurance = 47

        '5-Oct-15 Aniket: Patient Alerts Liquid Link. Do not change the number from 48
        PatientAlerts = 48
        '28-Dec-15 Resolving Bug #92200: gloEMR: (Liquid link) CPT with charges: CPT with charges liquid link is not working properly
        ExamICD9CPT_PM = 49
        Contacts_Detail = 50  ''added  for incident 00065814  PER s hospital affiliation
    End Enum

    '//Document Category from which form the document is loaded/opened
    Public Enum enumDocCategory
        Template = 1
        Exam = 2
        Referrals = 3
        Orders = 4
        Message = 5
        Others = 6
        Addendum = 7

    End Enum
    Public Enum enumControls
        None = 0
        FormFieldControl = 1
        ContentControl = 2
        Others = 3
    End Enum

    ''//Class which can used to populate data accordingly for retriveing data based on selecton criteria
    Public Class DocCriteria : Implements IDisposable   ''Idisposable added against problem 00000591
        Private _PatientID As Int64
        Private _PrimaryID As Int64 '' // ExamId  or ReferralsID or TestID w.r.to gloEMR
        Private _VisitID As Int64
        Private _FieldId1 As Int64 '' // Provision For others
        Private _FieldId2 As Int64
        Private _FieldId3 As Int64

        '' gloPM Parameters ''
        Private _AppointmentID As Int64
        '' ''
        '       Private _TableName As TableName
        Private _DocCategory As enumDocCategory

        ''//These are used while fetching data for Category_mst, Contatc_MSt , etc
        Private _Type As String
        Private _Criteria As Char

        Public Property DocCategory() As enumDocCategory
            Get
                Return _DocCategory
            End Get
            Set(ByVal Value As enumDocCategory)
                _DocCategory = Value

            End Set
        End Property

        Public Property PrimaryID() As Int64
            Get
                Return _PrimaryID
            End Get
            Set(ByVal Value As Int64)
                _PrimaryID = Value
            End Set
        End Property
        Public Property VisitID() As Int64
            Get
                Return _VisitID
            End Get
            Set(ByVal Value As Int64)
                _VisitID = Value
            End Set
        End Property
        Public Property FieldID1() As Int64
            Get
                Return _FieldId1
            End Get
            Set(ByVal Value As Int64)
                _FieldId1 = Value
            End Set
        End Property
        Public Property FieldID2() As Int64
            Get
                Return _FieldId2
            End Get
            Set(ByVal Value As Int64)
                _FieldId2 = Value
            End Set
        End Property
        Public Property FieldID3() As Int64
            Get
                Return _FieldId3
            End Get
            Set(ByVal Value As Int64)
                _FieldId3 = Value
            End Set
        End Property
        Public Property PatientID() As Int64
            Get
                Return _PatientID
            End Get
            Set(ByVal Value As Int64)
                _PatientID = Value
            End Set
        End Property
        Public Property AppointmentID() As Int64
            Get
                Return _AppointmentID
            End Get
            Set(ByVal Value As Int64)
                _AppointmentID = Value
            End Set
        End Property
        Public Property Type() As String
            Get
                Return _Type
            End Get
            Set(ByVal value As String)
                value = _Type
            End Set
        End Property
        Public Property Criteria() As Char
            Get
                Return _Criteria
            End Get
            Set(ByVal value As Char)
                value = _Criteria
            End Set
        End Property
        ''Added On 20101006 by sanjog for Signaure
        Private _ProviderID As Int64 = 0
        Public Property ProviderID() As Int64
            Get
                Return _ProviderID
            End Get
            Set(ByVal value As Int64)
                _ProviderID = value
            End Set
        End Property
        ''Added On 20101006 by sanjog for Signaure


        'Public Property TableName() As TableName
        '    Get
        '        Return _TableName
        '    End Get
        '    Set(ByVal Value As TableName)
        '        _TableName = Value
        '    End Set
        'End Property

        Public Sub New()
            MyBase.new()
            '            _TableName = New TableName
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub



        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).


                    _DocCategory = Nothing

                    ''//These are used while fetching data for Category_mst, Contatc_MSt , etc
                    _Type = Nothing
                    _Criteria = Nothing

                End If


                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
    End Class


#Region " For Upgrade purpose"
    Public Class TableName

        Private _Tablename As String
        Private _TableFields As TableFields
        Public Property Tablename() As String
            Get
                Return _Tablename
            End Get
            Set(ByVal value As String)
                _Tablename = value
            End Set
        End Property
        Public Property TableFields() As TableFields
            Get
                Return _TableFields

            End Get
            Set(ByVal value As TableFields)
                _TableFields = value
            End Set
        End Property


    End Class
    Public Class TableField
        Private _FieldName As String
        Private _FieldValue As String
        Public Property FieldName() As String
            Get
                Return _FieldName
            End Get
            Set(ByVal value As String)
                _FieldName = value
            End Set
        End Property
        Public Property FieldValue() As String
            Get
                Return _FieldValue
            End Get
            Set(ByVal value As String)
                _FieldValue = value
            End Set
        End Property


    End Class

    Public Class TableFields
        Inherits CollectionBase

    End Class
    Public Class FormFields


    End Class
#End Region

    'Added By Shweta 20090827
    Public Class frmDSOTest
        Inherits System.Windows.Forms.Form

        Private components As System.ComponentModel.IContainer
        ''''Private wdTest As New AxDSOFramer.AxFramerControl
        Public _ErrorMessage As String = ""

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub
        Private Sub InitializeComponent()
            Me.SuspendLayout()
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(115, 0)
            Me.Name = ""
            Me.Opacity = 0
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.Text = ""
            Me.ResumeLayout(False)
        End Sub

        Public Sub New()
            InitializeComponent()
            Me.Hide()
        End Sub

        Public ReadOnly Property ErrorMessage() As String
            Get
                Return _ErrorMessage
            End Get
        End Property
        Private Function frmDSOTEst_SubLoad() As String
            Dim wdTest As New AxDSOFramer.AxFramerControl
            If (IsNothing(wdTest) = False) Then

                Me.Controls.Add(wdTest)
                Try
                    wdTest.CreateNew("Word.Document")
                    System.Threading.Thread.Sleep(500)
                    'wdTest.Open("C:\Documents and Settings\Administrator\Desktop\New WinRAR archive.docx")

                    Return ""
                Catch oEx As System.Reflection.TargetInvocationException
                    Return "Please close the dialog box (if open) from other word application outside gloEMR"

                Catch oExCom As System.Runtime.InteropServices.COMException
                    Return "Please close the dialog box (if open) from other word application outside gloEMR"

                Catch ex As Exception
                    Return ex.ToString


                Finally
                    Try
                        Me.Controls.Remove(wdTest)
                    Catch ex As Exception

                    End Try

                    Try
                        wdTest.Close()
                    Catch ex As Exception

                    End Try
                    Try
                        wdTest.Dispose()
                    Catch ex As Exception

                    End Try

                    wdTest = Nothing
                    'Me.Close()
                End Try
            Else
                Return "Unable to Create Word control"
                'Me.Close()
            End If
        End Function

        Private Sub frmDSOTest_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            _ErrorMessage = frmDSOTEst_SubLoad()
            If (_ErrorMessage <> "") Then
                gloWord.WordDialogBoxBackgroundCloser.CloseAnyWordDialogs()
                _ErrorMessage = frmDSOTEst_SubLoad()
            End If
            If (_ErrorMessage <> "") Then
                Me.DialogResult = Windows.Forms.DialogResult.No
            Else
                Me.DialogResult = Windows.Forms.DialogResult.Yes
            End If
            Me.Close()

            'Dim wdTest As New AxDSOFramer.AxFramerControl
            'If (IsNothing(wdTest) = False) Then

            '    Me.Controls.Add(wdTest)
            '    Try
            '        wdTest.CreateNew("Word.Document")
            '        System.Threading.Thread.Sleep(500)
            '        'wdTest.Open("C:\Documents and Settings\Administrator\Desktop\New WinRAR archive.docx")

            '        Me.DialogResult = Windows.Forms.DialogResult.Yes
            '    Catch oEx As System.Reflection.TargetInvocationException
            '        _ErrorMessage = "Please close the dialog box (if open) from other word application outside gloEMR"
            '        Me.DialogResult = Windows.Forms.DialogResult.No
            '    Catch oExCom As System.Runtime.InteropServices.COMException
            '        _ErrorMessage = "Please close the dialog box (if open) from other word application outside gloEMR"
            '        Me.DialogResult = Windows.Forms.DialogResult.No
            '    Catch ex As Exception
            '        _ErrorMessage = ex.ToString
            '        Me.DialogResult = Windows.Forms.DialogResult.No

            '    Finally
            '        Try
            '            Me.Controls.Remove(wdTest)
            '        Catch ex As Exception

            '        End Try

            '        Try
            '            wdTest.Close()
            '        Catch ex As Exception

            '        End Try
            '        Try
            '            wdTest.Dispose()
            '        Catch ex As Exception

            '        End Try

            '        wdTest = Nothing
            '        Me.Close()
            '    End Try
            'Else
            '    Me.DialogResult = Windows.Forms.DialogResult.No
            '    _ErrorMessage = "Unable to Create Word control"
            '    Me.Close()
            'End If
            ''''End Try
            ''''Me.Close()
        End Sub
    End Class

    



    'End Shweta
End Namespace

