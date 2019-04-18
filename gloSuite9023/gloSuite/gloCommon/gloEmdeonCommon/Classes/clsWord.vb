Imports Microsoft.Office.Core
Imports Wd = Microsoft.Office.Interop.Word
Imports System.IO
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Windows.Media.Imaging
Imports System.Collections
Imports gloSettings
Imports System.Linq
Imports System.Data

Namespace gloEMRWord
 


    Interface IgloEMRWord
        Function GetData(ByVal DocumentType As enumDocType) As DataTable        
        Function InsertDocument(ByVal strFileName As String) As Byte()
        Function RetrieveDocumentFile() As String
    End Interface


    Public Class clsWordDocument
        Implements IgloEMRWord, IDisposable


        Public Property IncludeCaption As Boolean = False

        Public AddDictionaryParent As Form = Nothing
        Private _oCurDoc As Wd.Document
        Private _DocumentCriteria As DocCriteria
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


        Private strPatientCode As String = ""
        Private strPatientFirstName As String = ""
        Private strPatientLastName As String = ""
        Private strPatientDOB As String = ""
        Private strPatientAge As String = ""
        Private strPatientGender As String = ""
        Private strPatientMaritalStatus As String = ""

        'Flag is Settled For Applying Double Click event On Word Document -Yatin 20111222(Bug No. 17073 )
        Public LiquidFlag As Boolean = False
        ''Used in Generate file
        Dim _myPhoto As Boolean = False

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
        Friend WithEvents wdNewExam As AxDSOFramer.AxFramerControl
        Private WithEvents oWordApp As Wd.Application
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

        Public Function getData_FromDB(ByVal strFields As String, Optional ByVal strHelpError As String = "", Optional ByRef dtTable As DataTable = Nothing) As String
            Dim oDB As DataBaseLayer
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

                oDB = New DataBaseLayer
                '' SUDHIR 20091212 '' FETCH CLOSEST POSSIBLE APPOINTMENT ID ''
                If strFields.EndsWith("CurrentAppointment") Or strFields.StartsWith("AS_Appointment_DTL") Then
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


                oResultTable = New DataTable
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
                        If strSplitlocal = "01" Or strSplitlocal = "02" Or strSplitlocal = "03" Or strSplitlocal = "04" Or strSplitlocal = "05" Or strSplitlocal = "10" Then
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

                ElseIf strFields.StartsWith("pa_accounts.") Or strFields.StartsWith("PA_Accounts_Patients.") Or strFields.StartsWith("pa_accounts_Billing.") Or strFields.StartsWith("pa_accounts_PatientLastClaimDiag.") Then
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
                Dim VisitDate As Date = GetVisitdate(_VisitID)

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
                If (DaysToAdd < 0) Or (MonthsToAdd < 0) Then
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

                    If strFields.StartsWith("FlowSheet") And strFields.EndsWith("SingleRow") Then
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
                    oDB.Dispose()
                    oDB = Nothing
                    If oResultTable Is Nothing Then
                        Return ""
                    End If

                    If oResultTable.Rows.Count > 0 Then

                        ''// Check if Form field is of Following types
                        ''Bug : 00000875: Some Liquid Links not working from Orders and Results
                        If InStr(strFields, "Narration") Or InStr(strFields, "LM_LabResult") Or InStr(strFields, "imgSignature") Or InStr(strFields, "imgClinicLogo") Or InStr(strFields, "iPhoto") Or InStr(strFields, "iCard") Or InStr(strFields, "iDASImage") Then

                            For i As Int32 = 0 To oResultTable.Rows.Count - 1
                                _myPhoto = False
                                '   For j As Int32 = 0 To oResultTable.Columns.Count - 1
                                If IsDBNull(oResultTable.Rows(i).Item(0)) = False Then
                                    Dim strFileName As String
                                    If oResultTable.Rows(i).Item(1) = "2" Then
                                        filecnt = filecnt + 1
                                        If InStr(strFields, "Narration") Then
                                            strFileName = gloSettings.FolderSettings.AppTempFolderPath & gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddhhmmssmmm") & "Narration.txt"
                                        Else
                                            strFileName = gloSettings.FolderSettings.AppTempFolderPath & gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddhhmmssmmm") & "Flowsheet" & filecnt & ".txt"
                                        End If
                                    Else
                                        If strFields = "Provider.imgSignature" Then
                                            strFileName = gloSettings.FolderSettings.AppTempFolderPath & gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddhhmmssmmm") & "imgSignature.bmp"
                                        ElseIf strFields = "User_MST.imgSignature" Then
                                            strFileName = gloSettings.FolderSettings.AppTempFolderPath & gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddhhmmssmmm") & "imgCoSignature.bmp"
                                        ElseIf strFields = "Patient_Cards.iCard|Driving" Then
                                            strFileName = gloSettings.FolderSettings.AppTempFolderPath & gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddhhmmssmmm") & "imgDrivingLicense.bmp"
                                        ElseIf strFields = "Patient_Cards.iCard|Insurance" Then
                                            strFileName = gloSettings.FolderSettings.AppTempFolderPath & gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddhhmmssmmm") & "imgInsuranceCard.bmp"
                                            ''Bug : 00000875: Some Liquid Links not working from Orders and Results
                                        ElseIf strFields = "DAS.iDASImage" Then
                                            strFileName = gloSettings.FolderSettings.AppTempFolderPath & gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddhhmmssmmm") & "DasCalculater.bmp"
                                        Else
                                            _myPhoto = InStr(strFields, "iPhoto") '' Used in Generate File
                                            strFileName = gloSettings.FolderSettings.AppTempFolderPath & gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddhhmmssmmm") & "imgPhoto.bmp"
                                        End If
                                    End If

                                    strData = strFileName
                                    ''Save contents in file
                                    If File.Exists(strFileName) Then
                                        File.Delete(strFileName)
                                    End If
                                    strFileName = GenerateFile(oResultTable.Rows(i).Item(0), strFileName)
                                    If InStr(strFields, "SingleRow") Then
                                        GetLastLine(strFileName)
                                    End If
                                    '''''
                                    ResultDataType = oResultTable.Rows(i).Item(1)
                                End If
                                'Next
                            Next
                        ElseIf InStr(strFields, "FlowSheet") Then
                            ''Sudhir 20090225 ''
                            Dim oDBTemp As New gloDatabaseLayer.DBLayer(GetConnectionString)
                            Dim Query As String = ""
                            If strFields.StartsWith("FlowSheet") And strFields.EndsWith("SingleRow") Then
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
                            dtTable = New DataTable
                            For j As Integer = 0 To nColumnCount - 1
                                dtTable.Columns.Add(oResultTable.Rows(j)("sFieldName").ToString)
                            Next

                            ''Fill All Data to dtFlowSheet
                            ''Read each value from database and store as a datarow.
                            Dim nRow As Int32 = 0
                            If strFields.StartsWith("FlowSheet") And strFields.EndsWith("SingleRow") Then
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
                                dtTable = _dv.ToTable()
                                strData = "OBVitals Table"
                                ResultDataType = "6"
                            End If
                            _dv.Dispose()
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

                        ElseIf InStr(strFields, "Lab_Test_Mst") Then
                            Dim _dv As DataView = Nothing
                            If InStr(strFields, "Lab_Test_Mst.OrderedLabTests") Then
                                _dv = oResultTable.DefaultView
                                dtTable = _dv.ToTable()
                                strData = "Ordered Lab Tests"
                                ResultDataType = "6"
                            Else
                                _dv = oResultTable.DefaultView
                                _dv.Sort = "Test"
                                dtTable = _dv.ToTable()
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
                            _dv.Dispose()
                            _dv = Nothing
                            ''Bug : 00000875: Some Liquid Links not working from Orders and Results
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
                            dtTable = oResultTable
                            strData = "ImplantDevice"
                            ResultDataType = "6"
                        ElseIf strFields.Contains("Im_Trn_Dtl.Vaccines") = True Then
                            dtTable = oResultTable
                            strData = "Vaccines"
                            ResultDataType = "6"

                        ElseIf (strFields.Contains("DAS.DAS28Form") = True) And (oResultTable.Columns.Contains("FlagOthers") = False) Then
                            ''Bug : 00000875: Some Liquid Links not working from Orders and Results
                            If Not IsNothing(dtTable) Then  ''slr free previous memory
                                dtTable.Dispose()
                            End If
                            dtTable = Nothing
                            dtTable = Nothing  ''slr free previous memory
                            dtTable = oResultTable.Copy()
                            strData = "DAS"
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
                        ElseIf InStr(strFields, "Vitals") And strFields.Contains("|") Then
                            Dim oDictionary As New clsDataDictionary
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
                        If strData.EndsWith(".00") Then
                            strData = strData.Replace(".00", "")
                        End If
                        If strData.EndsWith(".000") Then
                            strData = strData.Replace(".000", "")
                        End If
                        If InStr(strData, ".00") Then
                            strData = strData.Replace(".00", "")
                        End If
                        'strData = Split(strData, ".00")(0)
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

                '22-Mar-13 Aniket: Cannot dispose dtTable as it is a ByRef variable
            End Try

        End Function

        Public Function getImagePath(ByVal strFields As String, ByVal strHelpError As String) As String
            Dim iPath As String = getData_FromDB(strFields, strHelpError)
            Return iPath

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
            'oDB.Dispose()
            oDB = Nothing
            dtDate = System.DateTime.Now
            strID = System.Math.Abs(DateDiff(DateInterval.Day, CDate("1/1/1900"), dtDate.Date)) & System.Math.Abs(DateDiff(DateInterval.Second, dtDate.Date, dtDate)) & System.Math.Abs(DateDiff(DateInterval.Day, CDate("1/1/1900"), PatientDOB.Date))
            Return CLng(strID)

            ' Requires setting a reference to System.Management.dll
            ''
        End Function

        Public Function GenerateFile(ByVal cntFromDB As Object, ByVal strFileName As String) As String

            If Not cntFromDB Is Nothing Then
                If IsDBNull(cntFromDB) = False Then
                    If _myPhoto = True Then

                        Dim content As Byte() = CType(cntFromDB, Byte())
                        'SLR: Added from static function
                        'Dim MyPictureBoxControl As New gloPictureBox.gloPictureBox
                        'MyPictureBoxControl.byteImage = content
                        'Dim PatientPhoto As Image = MyPictureBoxControl.copyFrame(True)
                        'PatientPhoto.Save(strFileName, System.Drawing.Imaging.ImageFormat.Bmp)
                        'PatientPhoto.Dispose()
                        'PatientPhoto = Nothing


                        ' ''22-Mar-13 Aniket: Resolving Memory Leak Issues
                        ''content.Clear(content, 1, content.Length - 1)

                        ''09-May-13 Aniket: Do not call the content.clear method as it damages the word file Bug #50357 
                        'Array.Resize(content, 0)
                        'content = Nothing

                        'MyPictureBoxControl.Dispose()
                        'MyPictureBoxControl = Nothing

                        Dim PatientPhoto As Image = gloPictureBox.gloImage.GetImage(content, True)
                        PatientPhoto.Save(strFileName, System.Drawing.Imaging.ImageFormat.Bmp)
                        PatientPhoto.Dispose()
                        PatientPhoto = Nothing
                        Return strFileName

                    Else
                        Dim content() As Byte = CType(cntFromDB, Byte())
                        'Dim stream As MemoryStream = New MemoryStream(content)
                        'If stream Is Nothing Then
                        '    Return Nothing
                        'End If
                        If (IsNothing(content) = False) Then
                            Dim oFile As New System.IO.FileStream(strFileName, System.IO.FileMode.Create)
                            If oFile Is Nothing Then
                                'If Not IsNothing(Stream) Then
                                '    Stream.Dispose()
                                '    Stream = Nothing
                                'End If
                                Return Nothing
                            End If
                            oFile.Write(content, 0, content.Length)
                            ' Stream.WriteTo(oFile)
                            oFile.Flush()
                            oFile.Close()
                            ''Changed
                            If Not IsNothing(oFile) Then
                                oFile.Dispose()
                                oFile = Nothing
                            End If
                        End If
                       


                        ''22-Mar-13 Aniket: Resolving Memory Leak Issues
                        'content.Clear(content, 1, content.Length - 1)

                        ''09-May-13 Aniket: Do not call the content.clear method as it damages the word file Bug #50357 
                        'Array.Resize(content, 0)
                        'content = Nothing 'Change made to solve memory Leak and word crash issue



                        'If Not IsNothing(stream) Then
                        '    stream.Dispose()
                        '    stream = Nothing
                        'End If
                        Return strFileName
                    End If
                Else
                    Return Nothing
                End If

            Else
                Return Nothing

            End If
        End Function

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
                        Dim Tempcheck = Split(Trim(strNewString.Item(nLoop)), vbTab)
                        If Trim(Tempcheck(1)) <> "" Or Trim(Tempcheck(2)) <> "" Then
                            Exit For
                        End If
                    End If
                Next
                oWrite.WriteLine(strNewString.Item(nLoop))
            End If

            oWrite.Close()
            oWrite.Dispose()
            oWrite = Nothing 'Change made to solve memory Leak and word crash issue

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

        Private Sub InsertWordTable(ByVal dtTable As DataTable, ByVal oField As Wd.FormField)
            Dim objDefaultBehaviorWord8 As Object = Wd.WdDefaultTableBehavior.wdWord8TableBehavior
            Dim objAutoFitFixed As Object = Wd.WdAutoFitBehavior.wdAutoFitFixed
            Try
                If Not IsNothing(dtTable) Then
                    If dtTable.Rows.Count > 0 Then
                        With _oCurDoc.Application.Selection
                            Dim cntcontrol As Wd.ContentControl = _oCurDoc.Application.Selection.Range.ParentContentControl
                            If Not IsNothing(cntcontrol) Then
                                _oCurDoc.Application.Selection.Move(Unit:=Wd.WdUnits.wdSection, Count:=1)
                            End If

                            ''''Create Basic Table
                            Dim nrRows As Integer = 1
                            Dim nrCols As Integer = dtTable.Columns.Count

                            _oCurDoc.Application.Selection.Select()
                            '_oCurDoc.Application.Selection.Move(Unit:=Wd.WdUnits.wdWord, Count:=1)
                            Dim wdRng As Wd.Range = _oCurDoc.Application.Selection.Range

                            While wdRng.Tables.Count > 0
                                _oCurDoc.Application.Selection.MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=1)
                                wdRng = _oCurDoc.Application.Selection.Range
                            End While

                            'Developer:Yatin N. Bhagat
                            'Date:12/13/2011
                            'Bug ID/PRD Name/Salesforce Case:Liquid Link For Ob Vital Table
                            'Reason: To Remove Extra Columns From Table for Ob Vitals table Liquid Link
                            Dim tb1 As Wd.Table
                            If (oField.StatusText.Trim.Contains("OBVitals") = False) Then
                                tb1 = wdRng.Tables.Add(wdRng, nrRows, nrCols, objDefaultBehaviorWord8, objAutoFitFixed)
                            Else
                                tb1 = wdRng.Tables.Add(wdRng, nrRows, nrCols - 4, objDefaultBehaviorWord8, objAutoFitFixed)
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
                                If PopulateAndExtendFlowSheetTable(tb1, dtTable) Then
                                    'wdRng = CreateSpaceAfterTable(tb1)
                                    Dim style As Wd.Style = CreateWordTableStyleForLabTables()
                                    FormatWordTables(style, tb1)
                                    style = Nothing
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
                _oCurDoc.Application.Selection.InsertParagraph()
                _oCurDoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
            End Try
        End Sub

        Private Sub InsertWordTableForLabResult(ByVal dtTable As DataTable, ByVal oField As Wd.FormField)
            Dim objDefaultBehaviorWord8 As Object = Wd.WdDefaultTableBehavior.wdWord8TableBehavior
            Dim objAutoFitFixed As Object = Wd.WdAutoFitBehavior.wdAutoFitFixed

            Try
                If Not IsNothing(dtTable) Then
                    If dtTable.Rows.Count > 0 Then
                        With _oCurDoc.Application.Selection
                            Dim cntcontrol As Wd.ContentControl = _oCurDoc.Application.Selection.Range.ParentContentControl
                            If Not IsNothing(cntcontrol) Then
                                _oCurDoc.Application.Selection.Move(Unit:=Wd.WdUnits.wdSection, Count:=1)
                            End If

                            ''''Create Basic Table
                            Dim nrRows As Integer = 1
                            Dim nrCols As Integer = dtTable.Columns.Count - 2
                            _oCurDoc.Application.Selection.Select()
                            '_oCurDoc.Application.Selection.Move(Unit:=Wd.WdUnits.wdWord, Count:=1)
                            Dim wdRng As Wd.Range = _oCurDoc.Application.Selection.Range

                            While wdRng.Tables.Count > 0
                                _oCurDoc.Application.Selection.MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=1)
                                wdRng = _oCurDoc.Application.Selection.Range
                            End While

                            Dim tb1 As Wd.Table = wdRng.Tables.Add(wdRng, nrRows, nrCols, objDefaultBehaviorWord8, objAutoFitFixed)
                            'Dim tb1 As Wd.Table = oField.Range.Tables.Add(oField.Range, nrRows, nrCols, objDefaultBehaviorWord8, objAutoFitFixed)
                            If PopulateAndExtendLabResultTable(tb1, dtTable) Then
                                'wdRng = CreateSpaceAfterTable(tb1)
                                Dim style As Wd.Style = CreateWordTableStyleForLabTables()
                                FormatWordTables(style, tb1)
                                style = Nothing
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
                _oCurDoc.Application.Selection.InsertParagraph()
                _oCurDoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
            End Try
        End Sub

        Private Function PopulateAndExtendLabResultTable(ByVal tb1 As Wd.Table, ByVal dtFlowSheet As DataTable) As Boolean

            Dim objMissing As Object
            Try
                objMissing = System.Reflection.Missing.Value

                Dim nrRows As Integer = 1
                Dim nrCols As Integer = dtFlowSheet.Columns.Count - 3
                Dim minCols As Integer = Math.Min(dtFlowSheet.Columns.Count - 2, tb1.Columns.Count)
                ''Set Column Names
                If (tb1.Rows.Count >= 1) Then


                    For i As Integer = 0 To minCols - 1
                        If (dtFlowSheet.Columns(i).Caption.ToString.Trim = "Flag") Then
                            tb1.Cell(1, i + 1).Range.Text = dtFlowSheet.Columns(i).Caption & "*"
                        Else
                            tb1.Cell(1, i + 1).Range.Text = dtFlowSheet.Columns(i).Caption
                        End If
                    Next
                End If

                '''''Move Cursor to the Table 
                '_oCurDoc.Application.Selection.GoToNext(Microsoft.Office.Interop.Word.WdGoToItem.wdGoToTable)

                ''''''Move Cursor down in the Table
                Dim strCellText As String
                Dim RowIndex As Int16 = 0
                Dim isCommentSkiped As Boolean = False
                Dim NoofSkippedRows As Int64 = 0
                Dim iRowTemp As Int64 = 0
                Dim iColTemp As Int64 = 0
                Dim strFlag As String = "* Flag Legend :"
                'For iRow As Integer = 0 To dtFlowSheet.Rows.Count - 1 '* 2 - 2 '+ dtFlowSheet.Rows.Count - 1
                For iRow As Integer = 0 To dtFlowSheet.Rows.Count * 2 - 2 '+ dtFlowSheet.Rows.Count - 1
                    Try


                        tb1.Rows.Add(objMissing)  '''' new Row
                        _oCurDoc.Application.Selection.Move(Wd.WdUnits.wdRow)
                        If (iRow > 1) And isCommentSkiped = False Then
                            If (tb1.Rows.Count >= (iRow + 2)) And (tb1.Columns.Count >= 2) Then


                                tb1.Rows(iRow + 2).Cells(2).Split(1, 5)
                            End If

                        End If
                        isCommentSkiped = False
                        'If (iCol = 5) Then
                        If (strFlag.Contains(dtFlowSheet.Rows(RowIndex)("Flag1").ToString) = False) Then
                            strFlag = strFlag & Chr(11) & dtFlowSheet.Rows(RowIndex)("Flag1").ToString
                        End If
                        'End If
                        For iCol As Integer = 0 To minCols - 1
                            strCellText = dtFlowSheet.Rows(RowIndex)(iCol).ToString
                            If (tb1.Rows.Count >= (iRow + 2)) And (tb1.Columns.Count >= (iCol + 1)) Then
                                If iCol = 0 Then
                                    _oCurDoc.Application.Selection.Move(Wd.WdUnits.wdRow) '''' Move Cursor to Newly Added Row in table
                                    _oCurDoc.Application.Selection.MoveRight()

                                    '''' Add Catergory in New Row and category Column
                                    'tb1.Cell(iRow + 2, iCol + 1).Range.Text = strCellText commented by manoj on 20121127

                                    'start added by manoj on 20121127 for dispalying hyperlinks in result value
                                    If String.Compare(dtFlowSheet.Columns(iCol).ColumnName, "value", False) AndAlso gloGlobal.gloLabGenral.IsResultisHyperLink(strCellText) Then
                                        tb1.Cell(iRow + 2, iCol + 1).Range.Hyperlinks.Add(tb1.Cell(iRow + 2, iCol + 1).Range, If(strCellText.StartsWith("www."), "http://" & strCellText, strCellText), Nothing, Nothing, strCellText, Nothing)
                                    Else
                                        tb1.Cell(iRow + 2, iCol + 1).Range.Text = strCellText
                                    End If
                                    'end of added by manoj on 20121127 for dispalying hyperlinks in result value

                                    '''' Add Item for Selected category 
                                    '  Dim oNameField As Wd.FormField
                                    tb1.Cell(iRow + 2, iCol + 1).Application.Selection.Select()

                                Else '''' If the category is already add then add Item in the category
                                    _oCurDoc.Application.Selection.MoveRight()
                                    'tb1.Cell(iRow + 2, iCol + 1).Range.Text = strCellText
                                    'start added by manoj on 20121127 for dispalying hyperlinks in result value
                                    If String.Compare(dtFlowSheet.Columns(iCol).ColumnName, "value", False) AndAlso gloGlobal.gloLabGenral.IsResultisHyperLink(strCellText) Then
                                        tb1.Cell(iRow + 2, iCol + 1).Range.Hyperlinks.Add(tb1.Cell(iRow + 2, iCol + 1).Range, If(strCellText.StartsWith("www."), "http://" & strCellText, strCellText), Nothing, Nothing, strCellText, Nothing)
                                    Else
                                        tb1.Cell(iRow + 2, iCol + 1).Range.Text = strCellText
                                    End If
                                    'end of added by manoj on 20121127 for dispalying hyperlinks in result value
                                    tb1.Cell(iRow + 2, iCol + 1).Application.Selection.Select()
                                End If
                            End If

                            'strCellText = "Interpretation"
                            'If iCol = 0 Then
                            '    _oCurDoc.Application.Selection.Move(Wd.WdUnits.wdRow) '''' Move Cursor to Newly Added Row in table
                            '    _oCurDoc.Application.Selection.MoveRight()
                            '    '''' Add Catergory in New Row and category Column
                            '    tb1.Cell(iRow + 1 + 2, iCol + 1).Range.Text = strCellText
                            '    '''' Add Item for Selected category 
                            '    Dim oNameField As Wd.FormField
                            '    tb1.Cell(iRow + 1 + 2, iCol + 1).Application.Selection.Select()
                            'Else '''' If the category is already add then add Item in the category
                            '    _oCurDoc.Application.Selection.MoveRight()
                            '    tb1.Cell(iRow + 1 + 2, iCol + 1).Range.Text = strCellText
                            '    tb1.Cell(iRow + 1 + 2, iCol + 1).Application.Selection.Select()
                            'End If
                        Next

                        '------------
                        If (dtFlowSheet.Rows(RowIndex)("labotrd_ResultComment").Trim() <> "") Then
                            iRow = iRow + 1
                            tb1.Rows.Add(objMissing)  '''' new Row
                            _oCurDoc.Application.Selection.Move(Wd.WdUnits.wdRow)
                            For iCol As Integer = 0 To 1
                                If iCol = 0 Then
                                    strCellText = ""
                                ElseIf iCol = 1 Then
                                    strCellText = dtFlowSheet.Rows(RowIndex)("labotrd_ResultComment").ToString '"Interpretation results are usually a long comment with no other fields filled in.  Also there are times when these lines come one line per result.  Can we display these in any reasonable way, perhaps by merging cells as shown here?"
                                Else
                                    strCellText = ""
                                End If
                                'strCellText = "Data for Col =" & iCol 'dtFlowSheet.Rows(RowIndex)(iCol).ToString
                                If (tb1.Rows.Count >= (iRow + 2)) And (tb1.Columns.Count >= (iCol + 1)) Then
                                    If iCol = 0 Then
                                        _oCurDoc.Application.Selection.Move(Wd.WdUnits.wdRow) '''' Move Cursor to Newly Added Row in table
                                        _oCurDoc.Application.Selection.MoveRight()
                                        'Dim CL As Microsoft.Office.Interop.Word.Cell = tb1.Cell(iRow + 2, iCol + 1)
                                        'tb1.Cell(iRow + 2, iCol + 1).Merge(CL)
                                        'oTable.Rows[1].Cells[1].Merge(oTable.Rows[1].Cells[2]);
                                        '''' Add Catergory in New Row and category Column
                                        tb1.Cell(iRow + 2, iCol + 1).Range.Text = strCellText
                                        '''' Add Item for Selected category 
                                        '   Dim oNameField As Wd.FormField
                                        tb1.Cell(iRow + 2, iCol + 1).Application.Selection.Select()

                                    Else '''' If the category is already add then add Item in the category
                                        _oCurDoc.Application.Selection.MoveRight()
                                        tb1.Rows(iRow + 2).Cells(2).Merge(tb1.Rows(iRow + 2).Cells(6))
                                        'tb1.Cell(iRow + 2, iCol + 1).Range.Text = strCellText 'commented by manoj jadhav on 20121228  
                                        LabResultComments(tb1.Cell(iRow + 2, iCol + 1).Range, strCellText) 'Added by manoj jadhav on 20121228 
                                        tb1.Cell(iRow + 2, iCol + 1).Application.Selection.Select()

                                    End If
                                End If

                            Next
                            '------------
                        Else
                            isCommentSkiped = True
                            NoofSkippedRows = NoofSkippedRows + 1
                        End If
                        If (iRow > ((dtFlowSheet.Rows.Count * 2 - 2) - NoofSkippedRows)) Then
                            Exit For
                        End If
                        RowIndex = RowIndex + 1


                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    End Try
                    'iRowTemp = iRow
                    'iColTemp = iCol
                Next
                If (strFlag.Trim <> "* Flag Legend :") Then
                    tb1.Rows.Add(objMissing)  '''' new Row
                    iRowTemp = tb1.Rows.Count ' - 1
                    'tb1.Cell(iRowTemp, iColTemp + 1).Range.Text = "Demo text1"

                    _oCurDoc.Application.Selection.Move(Wd.WdUnits.wdRow)
                    _oCurDoc.Application.Selection.MoveRight()
                    ''tb1.ro
                    If (tb1.Rows.Count >= (iRowTemp)) And (tb1.Columns.Count >= (iColTemp + 1)) Then
                        tb1.Rows(iRowTemp).Cells(1).Merge(tb1.Rows(iRowTemp).Cells(tb1.Rows(iRowTemp).Cells.Count))
                        tb1.Cell(iRowTemp, iColTemp + 1).Range.Text = strFlag

                        tb1.Cell(iRowTemp, iColTemp + 1).Application.Selection.Select()
                    End If

                    'tb1.Application.Selection.Select()
                    Try
                        _oCurDoc.Applicbation.Selection.MoveEnd()
                        _oCurDoc.Application.Selection.Move(Wd.WdUnits.wdRow)
                        _oCurDoc.Application.Selection.MoveDown()
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    End Try

                    ''''Move Cursor down in the Table
                End If

                _oCurDoc.Application.Selection.MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=2)
                _oCurDoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
                '_oCurDoc.Application.Selection.InsertParagraph()
                _oCurDoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
                _oCurDoc.Application.Selection.GoToNext(Microsoft.Office.Interop.Word.WdGoToItem.wdGoToLine)
                Return True
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                If _oCurDoc.Application.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText Then
                    _oCurDoc.Application.Selection.Range.ParentContentControl.Delete(True)
                End If
                Return False

            Finally

                '08-May-13 Aniket: Resolving Memory Leaks
                objMissing = Nothing

            End Try

        End Function

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
                    If (IOfWww > -1) AndAlso ((IOfHttp > -1 AndAlso IOfHttp >= IOfWww) Or (IOfHttps > -1 AndAlso IOfHttps >= IOfWww) Or _
                                (IOfHttp1 > -1 AndAlso IOfHttp1 >= IOfWww) Or (IOfHttps1 > -1 AndAlso IOfHttps1 >= IOfWww)) Then
                        iIndex = IOfWww
                    ElseIf (IOfHttp > -1) AndAlso ((IOfWww > -1 AndAlso IOfWww >= IOfHttp) Or (IOfHttps > -1 AndAlso IOfHttps >= IOfHttp) Or _
                           (IOfHttp1 > -1 AndAlso IOfHttp1 >= IOfHttp) Or (IOfHttps1 > -1 AndAlso IOfHttps1 >= IOfHttp)) Then
                        iIndex = IOfHttp
                    ElseIf (IOfHttps > -1) AndAlso ((IOfWww > -1 AndAlso IOfWww >= IOfHttps) Or (IOfHttp > -1 AndAlso IOfHttp >= IOfHttps) Or _
                       (IOfHttp1 > -1 AndAlso IOfHttp1 >= IOfHttps) Or (IOfHttps1 > -1 AndAlso IOfHttps1 >= IOfHttps)) Then
                        iIndex = IOfHttps
                    ElseIf (IOfHttp1 > -1) AndAlso ((IOfWww > -1 AndAlso IOfWww >= IOfHttp1) Or (IOfHttp > -1 AndAlso IOfHttp >= IOfHttp1) Or _
                          (IOfHttps > -1 AndAlso IOfHttps >= IOfHttp1) Or (IOfHttps1 > -1 AndAlso IOfHttps1 >= IOfHttp1)) Then
                        iIndex = IOfHttp1
                    ElseIf (IOfHttps1 > -1) AndAlso ((IOfWww > -1 AndAlso IOfWww >= IOfHttps1) Or (IOfHttp > -1 AndAlso IOfHttp >= IOfHttps1) Or _
                          (IOfHttps > -1 AndAlso IOfHttps >= IOfHttps1) Or (IOfHttp1 > -1 AndAlso IOfHttp1 >= IOfHttps1)) Then
                        iIndex = IOfHttp1
                    End If
                    If iIndex <= -1 Then
                        MovePositionTotableCell(tRange, bflag)
                        tRange.InsertAfter(ResultComment)
                        Exit While
                    End If
                    For k As Integer = iIndex To Len(ResultComment) - 1
                        If ResultComment.Substring(k, 1) = " " Then
                            EIndex = (k - iIndex) + 1
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

        Private Function PopulateAndExtendFlowSheetTable(ByVal tb1 As Wd.Table, ByVal dtFlowSheet As DataTable) As Boolean
            Try
                Dim objMissing As Object = System.Reflection.Missing.Value
                Dim minCol As Integer = Math.Min(dtFlowSheet.Columns.Count, tb1.Columns.Count)
                ''Set Column Names
                If (tb1.Rows.Count >= 1) Then


                    For i As Integer = 0 To minCol - 1
                        tb1.Cell(1, i + 1).Range.Text = dtFlowSheet.Columns(i).Caption
                        If (0) Then
                            tb1.Columns(i + 1).SetWidth(200, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone)
                        End If
                    Next
                End If

                '''''Move Cursor to the Table 
                _oCurDoc.Application.Selection.GoToNext(Microsoft.Office.Interop.Word.WdGoToItem.wdGoToTable)

                ''''''Move Cursor down in the Table
                Dim strCellText As String

                For iRow As Integer = 0 To dtFlowSheet.Rows.Count - 1
                    tb1.Rows.Add(objMissing)  '''' new Row
                    For iCol As Integer = 0 To minCol - 1
                        strCellText = dtFlowSheet.Rows(iRow)(iCol).ToString
                        If (tb1.Rows.Count >= (iRow + 2)) And (tb1.Columns.Count >= (iCol + 1)) Then
                            If iCol = 0 Then
                                _oCurDoc.Application.Selection.Move(Wd.WdUnits.wdRow) '''' Move Cursor to Newly Added Row in table
                                _oCurDoc.Application.Selection.MoveRight()
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
                                tb1.Cell(iRow + 2, iCol + 1).Application.Selection.Select()

                            Else '''' If the category is already add then add Item in the category
                                _oCurDoc.Application.Selection.MoveRight()
                                ' tb1.Cell(iRow + 2, iCol + 1).Range.Text = strCellText  'commneted by mnaoj on 20121127

                                'start added by manoj on 20121127 for dispalying hyperlinks in result value
                                If String.Compare(dtFlowSheet.Columns(iCol).ColumnName, "value", False) AndAlso gloGlobal.gloLabGenral.IsResultisHyperLink(strCellText) Then
                                    tb1.Cell(iRow + 2, iCol + 1).Range.Hyperlinks.Add(tb1.Cell(iRow + 2, iCol + 1).Range, If(strCellText.StartsWith("www."), "http://" & strCellText, strCellText), Nothing, Nothing, strCellText, Nothing)
                                Else
                                    tb1.Cell(iRow + 2, iCol + 1).Range.Text = strCellText
                                End If
                                'end of added by manoj on 20121127 for dispalying hyperlinks in result value

                                tb1.Cell(iRow + 2, iCol + 1).Application.Selection.Select()
                            End If
                        End If

                    Next
                Next

                ''''Move Cursor down in the Table
                _oCurDoc.Application.Selection.MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=2)
                _oCurDoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
                '_oCurDoc.Application.Selection.InsertParagraph()
                _oCurDoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)

                Return True
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                If _oCurDoc.Application.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText Then
                    _oCurDoc.Application.Selection.Range.ParentContentControl.Delete(True)
                End If
                Return False
            End Try
        End Function

        Private Function PopulateAndExtendDASTable(ByVal tb1 As Wd.Table, ByVal dtFlowSheet As DataTable) As Boolean
            Try
                If (IsNothing(dtFlowSheet)) Then
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
                _oCurDoc.Application.Selection.GoToNext(Microsoft.Office.Interop.Word.WdGoToItem.wdGoToTable)

                ''''''Move Cursor down in the Table
                Dim strCellText As String
                Dim minimumRow As Integer = dtFlowSheet.Rows.Count

                For iRow As Integer = 1 To minimumRow - 1
                    tb1.Rows.Add(objMissing)  '''' new Row
                    For iCol As Integer = 0 To minimumColumn - 1
                        strCellText = dtFlowSheet.Rows(iRow)(iCol).ToString
                        If (String.IsNullOrEmpty(strCellText)) Then
                            strCellText = " "
                        End If
                        ''Bug : 00000875: Some Liquid Links not working from Orders and Results
                        If (tb1.Rows.Count >= (iRow + 1)) And (tb1.Columns.Count >= (iCol + 1)) Then
                            If iCol = 0 Then
                                _oCurDoc.Application.Selection.Move(Wd.WdUnits.wdRow) '''' Move Cursor to Newly Added Row in table
                                _oCurDoc.Application.Selection.MoveRight()
                                '''' Add Catergory in New Row and category Column
                                Try
                                    tb1.Cell(iRow + 1, iCol + 1).Range.Text = strCellText
                                    '''' Add Item for Selected category 
                                    '   Dim oNameField As Wd.FormField
                                    tb1.Cell(iRow + 1, iCol + 1).Application.Selection.Select()
                                Catch ex2 As Exception
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Select, ex2.ToString(), gloAuditTrail.ActivityOutCome.Failure)

                                End Try

                            Else '''' If the category is already add then add Item in the category
                                _oCurDoc.Application.Selection.MoveRight()
                                Try
                                    tb1.Cell(iRow + 1, iCol + 1).Range.Text = strCellText
                                    '''' Add Item for Selected category 
                                    '   Dim oNameField As Wd.FormField
                                    tb1.Cell(iRow + 1, iCol + 1).Application.Selection.Select()
                                Catch ex2 As Exception
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Select, ex2.ToString(), gloAuditTrail.ActivityOutCome.Failure)

                                End Try
                            End If
                        End If

                    Next
                Next

                ''''Move Cursor down in the Table
                '28052012 Bug no.27574 *Move DownCount Incremented by 1*
                _oCurDoc.Application.Selection.MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=2)
                _oCurDoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
                '_oCurDoc.Application.Selection.InsertParagraph()
                '_oCurDoc.Application.Selection.TypeParagraph()
                _oCurDoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)

                Return True
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                If _oCurDoc.Application.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText Then
                    _oCurDoc.Application.Selection.Range.ParentContentControl.Delete(True)
                End If
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
                        If (tb1.Rows.Count >= 1) And (tb1.Columns.Count >= 1) Then
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


                    ElseIf dtFlowSheet.Columns(i).Caption <> "PrePregnancyWeight" And dtFlowSheet.Columns(i).Caption <> "WeightChange" And dtFlowSheet.Columns(i).Caption <> "nextAppt" And dtFlowSheet.Columns(i).Caption <> "Comments" Then
                        If (tb1.Rows.Count > 1) And (j < tb1.Columns.Count) Then
                            tb1.Cell(2, j + 1).Range.Text = dtFlowSheet.Columns(i).Caption
                            If dtFlowSheet.Columns(i).Caption = "Date" Then
                                tb1.Cell(2, j + 1).Range.Cells.Width = tb1.Cell(2, j + 1).Range.Cells.Width + 21
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
                            If dtFlowSheet.Columns(iCol).Caption <> "PrePregnancyWeight" And dtFlowSheet.Columns(iCol).Caption <> "WeightChange" And dtFlowSheet.Columns(iCol).Caption <> "nextAppt" And dtFlowSheet.Columns(iCol).Caption <> "Comments" Then
                                If (((tempRCount + 3) <= tb1.Rows.Count) And ((c + 1) <= tb1.Columns.Count)) Then
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
                            tb1.Cell(ir, 0).Range.Bold = 1
                        ElseIf tb1.Cell(ir, 0).Range.Text.Contains("Next Appointment: ") Then
                            tb1.Cell(ir, 0).Merge(MergeTo:=tb1.Cell(ir, 13))
                            tb1.Cell(ir, 0).Range.Bold = 1
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
                Dim objtStyl As Object = CType(tstyle, Object)
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
                'oDB.Dispose()
                'oDB = Nothing
                Return Nothing
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
            Dim objField As Wd.FormField 'Form field Variable

            If _oCurDoc.FormFields Is Nothing Then
                Exit Sub
            End If

            For Each objField In _oCurDoc.FormFields

                If objField.Type = Wd.WdFieldType.wdFieldFormTextInput Then
                    Try
                        objField.Range.HighlightColorIndex = Wd.WdColorIndex.wdNoHighlight
                    Catch ex As Exception

                    End Try

                    Dim myHelptextString As String = "|" & objField.HelpText.ToString() & "|"
                    Dim myResultTextString As String = objField.Result
                    '' If objField.HelpText = objField.Result Then ''Old
                    If myHelptextString = myResultTextString Then '' New 
                        objField.Result = ""
                        objField.Delete()
                    End If

                End If

            Next

            ''//To replace the special tags
            Dim col_Tags As New Collection
            col_Tags.Add("[]")
            col_Tags.Add("[HPI]")
            col_Tags.Add("[Xray]")
            col_Tags.Add("[MRI]")
            col_Tags.Add("[PLAN]")

            For i As Int16 = 1 To col_Tags.Count

                'Try
                '    _oCurDoc.Application.Selection.Find.Execute(FindText:=CStr(col_Tags(i)).Trim, ReplaceWith:=" ", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceAll)

                'Catch ex As Exception
                Try

                    gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=_oCurDoc.Application, FindText:=CStr(col_Tags(i)).Trim, ReplaceWith:=" ", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceAll, MatchWildCards:=False, MatchWholeWord:=False)
                Catch ex2 As Exception

                End Try
                'End Try
            Next

            col_Tags.Clear()
            col_Tags = Nothing

            'For Each cntCtrl As Wd.ContentControl In _oCurDoc.ContentControls
            '    If cntCtrl.Type = Wd.WdContentControlType.wdContentControlDropdownList Then
            '        cntCtrl.Delete(False)
            '    End If
            'Next
            For iCtrl As Integer = _oCurDoc.ContentControls.Count To 1 Step -1
                Try
                    Dim cntCtrl As Wd.ContentControl = _oCurDoc.ContentControls(iCtrl)

                    If cntCtrl.Type = Wd.WdContentControlType.wdContentControlDropdownList Then
                        cntCtrl.Delete(False)
                    End If
                Catch ex As Exception

                End Try
            Next
        End Sub
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

        Function getUniqueID() As String
            'Static firstTime As Boolean = True
            'Static myWatch As New Stopwatch()
            'Static myTime As DateTime
            'If firstTime = True Then
            '    firstTime = False
            '    myTime = Now()
            '    myWatch.Start()
            'End If
            'Dim TmSp As New TimeSpan(myTime.Ticks + myWatch.ElapsedTicks)
            'getUniqueID = TmSp.Ticks.ToString()
            'TmSp = Nothing
            Return gloGlobal.clsFileExtensions.GetUniqueDateString()
        End Function

        Public Function GetandSetMyFirstFlag(ByVal GetOrSet As Boolean, ByVal Assign As Boolean) As Boolean
            Static myFlag As Boolean = True
            If (GetOrSet) Then
                myFlag = Assign
                Return myFlag
            Else
                GetandSetMyFirstFlag = myFlag
            End If
        End Function

        Private Function FreeBigImageResources(ByRef big As BitmapImage) As Boolean
            If (IsNothing(big) = False) Then



                Try
                    big.StreamSource.Dispose()
                Catch

                End Try

                big.UriSource = Nothing

                Try
                    big.StreamSource.Dispose()
                Catch

                End Try


                Try
                    big.BeginInit()
                    big.UriSource = Nothing
                    big.EndInit()
                Catch
                End Try
                Try
                    big.StreamSource.Dispose()
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

        ''' <summary>
        ''' To Insert Image into the selected Template using Clipboard method
        ''' </summary>
        ''' <param name="ImagePath"></param>
        ''' <remarks> 20090604 </remarks>
        ''' 

        Public Sub InsertImage(ByVal ImagePath As String)
            Try
                If File.Exists(ImagePath) = True Then
                    Dim big As New BitmapImage()
                    Try


                        big.BeginInit()

                        big.CreateOptions = BitmapCreateOptions.IgnoreImageCache
                        big.CacheOption = BitmapCacheOption.OnLoad

                        big.UriSource = New Uri(ImagePath, UriKind.RelativeOrAbsolute)
                        big.EndInit()


                        Dim myWidth As Integer = big.PixelWidth
                        Dim myHeight As Integer = big.PixelHeight

                        'Try
                        '    '08-May-13 Aniket: Resolving Memory Leaks
                        '    big.StreamSource.Dispose()
                        'Catch ex As Exception

                        'End Try

                        'big.UriSource = Nothing
                        'Try

                        '    big.BeginInit()
                        '    big.UriSource = Nothing
                        '    big.EndInit()
                        '    '08-May-13 Aniket: Resolving Memory Leaks
                        '    big.StreamSource.Dispose()

                        'Catch
                        'End Try

                        ''08-May-13 Aniket: Resolving Memory Leaks
                        ''big = New BitmapImage()
                        ''Try
                        ''    big.StreamSource.Dispose()
                        ''Catch ex As Exception

                        ''End Try

                        ''big.UriSource = Nothing

                        '' Dim myScale As Double = System.Math.Max(myWidth, myHeight)
                        'big = Nothing
                        FreeBigImageResources(big)

                        Dim mySucessDPI As Integer = 0
                        Dim SuccessCopied As Boolean = False
                        Do While (SuccessCopied = False)
                            If ((mySucessDPI = 1) And (GetandSetMyFirstFlag(False, False) = True)) Then
                                If (MessageBox.Show("Image resolution will be reduced during insertion due to insufficient memory. Do you want to proceed?", gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) = DialogResult.Cancel) Then
                                    Return
                                Else
                                    GetandSetMyFirstFlag(True, False)
                                End If
                            End If
                            'End If
                            Dim myFileName As String = ""

                            If (mySucessDPI <> 0) Then
                                If (mySucessDPI = myWidth) Then
                                    MessageBox.Show("Unable to Insert even after reducing resolution due to insufficient memory", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                    Return
                                Else
                                    ConvertImage(ImagePath, myWidth, myHeight, CDbl(mySucessDPI), myFileName)
                                End If

                            Else
                                myFileName = ImagePath
                            End If

                            ''ImagePath = ""
                            If (myFileName <> "") Then
                                CurDocument.ActiveWindow.SetFocus()
                                Dim yesCopied As Boolean = True
                                Dim myStart As Integer = 0
                                Dim myEnd As Integer = 0
                                Try

                                    If (mySucessDPI = 0) Then
                                        Global.gloWord.gloWord.GetClipboardData()
                                    End If

                                    Dim oImage As Image = Nothing
                                    Try
                                        oImage = Image.FromFile(myFileName)
                                    Catch ex As Exception
                                        yesCopied = False
                                    End Try
                                    If (Not IsNothing(oImage)) Then

                                        Try
                                            ' Clipboard.Clear()
                                            Try
                                                Clipboard.SetImage(oImage)
                                            Catch ex As Exception
                                                yesCopied = False
                                            End Try
                                            If (yesCopied) Then
                                                myStart = CurDocument.Application.Selection.Range.Start
                                                Try
                                                    CurDocument.Application.Selection.Paste()
                                                Catch ex As Exception
                                                    yesCopied = False
                                                End Try
                                            End If


                                            ' Clipboard.Clear()
                                        Catch ex As Exception
                                            yesCopied = False
                                        End Try
                                        oImage.Dispose()
                                        oImage = Nothing
                                    Else
                                        yesCopied = False
                                    End If
                                    'Developer                          :Dipak(Patil)
                                    'Date                               :20120105
                                    'Bug ID/PRD Name/Sales force Case   :GLO2011-0015651 
                                    'Reason                             :To fix above case (only If condition added-If (IsFromScanImage) Then)              
                                    If (yesCopied) Then
                                        ' If (IsFromScanImage) Then
                                        Try
                                            myEnd = CurDocument.Application.Selection.Range.End
                                            Dim ShapeCounter As Long = 0
                                            CurDocument.Application.Selection.SetRange(myStart, myEnd)

                                            Try
                                                ShapeCounter = CurDocument.Application.Selection.InlineShapes.Count
                                            Catch ex As Exception
                                                ShapeCounter = 0
                                            End Try



                                            If (ShapeCounter > 0) Then
                                                With CurDocument.Application.Selection
                                                    Try
                                                        .Cut()
                                                        Try
                                                            .PasteSpecial(DataType:=15)
                                                        Catch ex As System.Runtime.InteropServices.COMException
                                                            Try
                                                                .Paste()
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

                                        ' End If
                                        Global.gloWord.gloWord.SetClipboardData()
                                        SuccessCopied = True

                                    End If

                                    If myFileName <> ImagePath Then
                                        Try
                                            If (File.Exists(myFileName)) Then
                                                File.Delete(myFileName)
                                            End If
                                        Catch
                                        End Try
                                    End If
                                    myFileName = ""

                                Catch e As Exception

                                End Try


                            End If
                            mySucessDPI += 1

                        Loop
                    Catch ex As Exception
                        FreeBigImageResources(big)
                    End Try

                End If
            Catch ex As Exception
                Throw ex
            Finally

            End Try
        End Sub

        Function ConvertImage(ByVal ImagePath As String, ByVal myWidth As Integer, ByVal myHeight As Integer, ByVal curPixel As Double, ByRef myFileName As String) As Boolean

            Dim big As New BitmapImage()
            big.BeginInit()

            big.CreateOptions = BitmapCreateOptions.IgnoreImageCache
            big.CacheOption = BitmapCacheOption.OnLoad

            big.UriSource = New Uri(ImagePath, UriKind.RelativeOrAbsolute)
            Dim myArea As Double
            Dim myScale As Double

            myArea = CDbl(myWidth) * CDbl(myHeight)
            If (myArea > (1280 * 1024)) Then
                myScale = System.Math.Sqrt((1280 * 1024) / myArea)
                big.DecodePixelWidth = CInt(CDbl(myWidth) * myScale / curPixel)
            Else
                big.DecodePixelWidth = CInt(CDbl(myWidth) / curPixel)
            End If
            big.EndInit()

            Try
                Dim oDFileInfo As New FileInfo(ImagePath)
                Dim myDir As String = oDFileInfo.DirectoryName()
                Dim oFileName As String = myDir + "\" + getUniqueID() + ".bmp"
                oDFileInfo = Nothing

                Dim Encoder As New BmpBitmapEncoder

                Encoder.Frames.Add(BitmapFrame.Create(big))
                Dim fs As New FileStream(oFileName, FileMode.Create)
                Encoder.Save(fs)
                Try
                    fs.Close()
                    fs.Dispose()
                    fs = Nothing
                Catch ex As Exception

                End Try

                Encoder = Nothing
                FreeBigImageResources(big)
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
                Dim ofileInfo As New FileInfo(oFileName)

                myFileName = myDir + "\" + getUniqueID() + ".TIF"
                ofileInfo.CopyTo(myFileName, True)
                ofileInfo = Nothing
                Try

                    If (File.Exists(oFileName)) Then
                        File.Delete(oFileName)
                    End If
                Catch ex As Exception

                End Try


            Catch ex As Exception
                FreeBigImageResources(big)
                'small = Nothing
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
                myFileName = ""
                ConvertImage = False

            Finally
                FreeBigImageResources(big)
                ConvertImage = True
            End Try

        End Function

#Region "Function for functionality Liquid data "

        Public Sub GetdataFromOtherForms(ByVal _DocType As gloEMRWord.enumDocType)
            'CType(myCallingForm, IWord).GetdataFromOtherForms(_DocType)
        End Sub

        ''' <summary>
        '''           Function called TurnOffMicrophone function of myCallingForm 
        ''' <para >
        '''           This function calling is equavalant to following code statement.
        ''' </para>
        ''' <example >
        '''            myCallingForm.TurnOffMicrophone()
        ''' </example>
        ''' <para >
        '''           But compile time we did't know which form is calling form thats why myCallingForm is typecasted to Interface  IWord
        ''' </para>
        ''' </summary>
        ''' <remarks>added by dipak 20090919</remarks>
        ''' 
        Private Sub TurnOffMicrophone()
            'CType(myCallingForm, IWord).TurnOffMicrophone()
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
            'CType(myCallingForm, IWord).ShowMicrophone()
        End Sub

        ''' <summary>
        ''' Check for Condiotion for is document is procted or controls selected is not  FormFieldControl
        ''' </summary>
        ''' <param name="eCtrlType"></param>
        ''' <returns>boolean value (true-Feasible and false=Not Feasible)</returns>
        ''' <remarks>added by dipak 20090919</remarks>
        ''' 
        Private Function IsFeasible(ByVal eCtrlType As enumControls) As Boolean
            Try
                If _oCurDoc.Application.ActiveDocument.ProtectionType <> Wd.WdProtectionType.wdNoProtection Then
                    MessageBox.Show("Current operation is invalid as document is under protection mode.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return False
                End If
                Dim oRng As Wd.Range
                If Not _oCurDoc.Application.Selection.Range Is Nothing Then
                    oRng = _oCurDoc.Application.Selection.Range
                End If
                If _oCurDoc.Application.Selection.ShapeRange.Count > 0 Then
                    If eCtrlType = enumControls.FormFieldControl Then
                        MessageBox.Show("Checkboxes and drop down lists cannot be used inside text boxes or other shapes.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return False
                    End If
                End If
                If Not _oCurDoc.Application.Selection.HeaderFooter Is Nothing Then
                    'If _oCurDoc.Application.Selection.HeaderFooter.IsHeader Then
                    If eCtrlType = enumControls.FormFieldControl Or eCtrlType = enumControls.ContentControl Then
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
        ''' To open the form under same thread as we are opening the form using com object click events
        ''' </summary>
        ''' <remarks>added by dipak 20090919
        ''' </remarks>
        Private Sub AccessControl()
            If Not IsNothing(myCallingForm) Then
                If myCallingForm.InvokeRequired Then
                    myCallingForm.Invoke(New MethodInvoker(AddressOf AccessControl))
                Else
                    'OpenLink(FieldValue)
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
                    'OpenDrawingPad()
                End If
            End If
        End Sub


      
        Private Function GetCurrentVitalID(ByVal mgnVisitID As Long, ByVal dtDOS As Date, ByVal m_PatientID As Long) As Long
            Dim Con As SqlConnection
            Dim cmd As SqlCommand
            Dim VitalID As Int64 = 0
            Dim objParam As SqlParameter = Nothing
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
                ' Dim VisitID As Long
                VitalID = cmd.ExecuteScalar
                'Change made to solve memory Leak and word crash issue
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
                If Not IsNothing(objParam) Then
                    objParam = Nothing
                End If

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
                        _oCurDoc.Application.Selection.Delete()
                        _oCurDoc.Application.Selection.Paste()
                        'Clipboard.Clear()
                    End If
                Catch ex As Exception

                End Try

            End If
        End Sub


   


        ''' <summary>
        ''' Load the  Word Document in the Dso control
        ''' </summary>
        ''' <param name="strFileName"></param>
        ''' <param name="blnGetData"></param>
        ''' <remarks></remarks>
        Private Sub LoadWordUserControl(ByVal strFileName As String, Optional ByVal blnGetData As Boolean = False)
            Dim objExams As New clsPatientExams
            Dim ObjWord As clsWordDocument
            Dim objCriteria As DocCriteria
            ExamProviderId = objExams.GetProviderIdforExam(_DocumentCriteria.PrimaryID)
            objExams.Dispose()
            objExams = Nothing

            Try
                wdNewExam.Open(strFileName)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.Open, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show("Template cannot be open because there are problems with the contents.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                wdNewExam.CreateNew("Word.Document")
            End Try

            If blnGetData Then
                ObjWord = New clsWordDocument
                objCriteria = New DocCriteria
                ''//Mapping values for retrieving data from DB
                objCriteria.DocCategory = enumDocCategory.Exam
                objCriteria.PatientID = _DocumentCriteria.PatientID
                objCriteria.VisitID = mgnVisitID
                objCriteria.PrimaryID = _DocumentCriteria.PrimaryID
                ObjWord.DocumentCriteria = objCriteria
                ObjWord.CurDocument = _oCurDoc
                ''//Replace the Form Fields with data in the Word Document
                ObjWord.GetFormFieldData(enumDocType.None)
                _oCurDoc = ObjWord.CurDocument
                _oCurDoc.ActiveWindow.View.ShowFieldCodes = False

                objCriteria = Nothing
                ObjWord = Nothing
            Else
                ObjWord = New clsWordDocument
                ObjWord.CurDocument = _oCurDoc
                ObjWord.HighlightColor()
                _oCurDoc = ObjWord.CurDocument
                _oCurDoc.ActiveWindow.View.ShowFieldCodes = False
                ObjWord = Nothing
            End If

            _oCurDoc.ActiveWindow.SetFocus()
            _oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)

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
                    con.Dispose()
                    con = Nothing
                End If
                If Not IsNothing(dtICD9) Then
                    'dtICD9.Dispose()
                    dtICD9 = Nothing
                End If
            End Try
        End Function



      
        ''Add this method to check selection for Add Datadictionay button on forms
        Public Sub ValidateSelection(ByRef _oCurDoc As Wd.Document)

            Dim rTemp As Wd.Range = _oCurDoc.Application.Selection.Range
            If Not rTemp.ParentContentControl Is Nothing Then
                If rTemp.ParentContentControl.Type = Wd.WdContentControlType.wdContentControlDropdownList Then
                    Dim cntctrl As Wd.ContentControl = rTemp.ParentContentControl
                    'If cntctrl.Type = Wd.WdContentControlType.wdContentControlDropdownList Then
                    _oCurDoc.Application.Selection.EndKey(Unit:=Wd.WdUnits.wdLine)
                    _oCurDoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=2)
                    'Selection.MoveRight(Unit:=wdCharacter, Count:=3)
                    'End If
                End If
            End If

        End Sub
        ''' <summary>
        ''' Procedure for Add Data field
        ''' </summary>
        ''' <remarks>Added by dipak 20091001</remarks>
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
                    Application.UseWaitCursor = True
                    AddDynamicField(frm.GetArrlistDataDictionary, frm.IncludeCaption)
                    Application.UseWaitCursor = False
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
            Try
                If IsNothing(Arrlist) = False AndAlso _oCurDoc.FormFields IsNot Nothing Then
                    If Arrlist.Count > 0 Then


                        _oCurDoc.ActiveWindow.SetFocus()
                        Dim GurantorID As Int64 = 0
                        For i As Integer = 0 To Arrlist.Count - 1
                            'If _oCurDoc.Application.ActiveDocument.ProtectionType = Wd.WdProtectionType.wdNoProtection Then
                            If (GurantorID = 0) And (CType(Arrlist.Item(i), myList).Code.ToString.Contains("pa_accounts.") Or CType(Arrlist.Item(i), myList).Code.ToString.Contains("PA_Accounts_Patients.") Or CType(Arrlist.Item(i), myList).Code.ToString.Contains("pa_accounts_Billing.") Or CType(Arrlist.Item(i), myList).Code.ToString.Contains("pa_accounts_PatientLastClaimDiag.")) Then
                                ''Set GurantorID here.
                                ''Set GurantorID here.
                                'Dim ofrmSelectPatientGuarantor As New frmSelectPatientGuarantor(_DocumentCriteria.PatientID, gnClinicID)
                                'Dim oClsSelectPatientGuarantor As New clsSelectPatientGuarantor(_DocumentCriteria.PatientID, gnClinicID)
                                'Dim dtAccounts As DataTable
                                'dtAccounts = oClsSelectPatientGuarantor.GetPatientAccounts(_DocumentCriteria.PatientID, gnClinicID)
                                'If (dtAccounts.Rows.Count = 1) Then
                                '    GurantorID = dtAccounts.Rows(0)("nPAccountID").ToString()
                                'ElseIf (dtAccounts.Rows.Count > 1) Then
                                '    ofrmSelectPatientGuarantor.ShowDialog()
                                '    'aField.Result = ofrmSelectPatientGuarantor.SelectedGuarantor
                                '    If (ofrmSelectPatientGuarantor.DialogResult = DialogResult.OK) Then
                                '        GurantorID = ofrmSelectPatientGuarantor.SelectedAccount
                                '    Else
                                '        GurantorID = -1
                                '    End If
                                'Else
                                '    GurantorID = 0
                                'End If
                                'If IsNothing(ofrmSelectPatientGuarantor) = False Then
                                '    ofrmSelectPatientGuarantor.Dispose()
                                '    ofrmSelectPatientGuarantor = Nothing
                                'End If
                                'If IsNothing(oClsSelectPatientGuarantor) = False Then
                                '    oClsSelectPatientGuarantor.Dispose()
                                '    oClsSelectPatientGuarantor = Nothing
                                'End If
                            End If
                            '' Using MyList class for mapping value temporary
                            ''-- Description is mapped to sCaption
                            ''-- Code is mapped to sFiledName 
                            'Added New line After Liquid Link
                            If i >= 1 Then
                                _oCurDoc.Application.Selection.TypeText(vbNewLine)
                            End If
                            Dim oNameField As Wd.FormField

                            'Yatin- Liquid link Lable is get merged in to table
                            Dim wdRng As Wd.Range = _oCurDoc.Application.Selection.Range
                            While wdRng.Tables.Count > 0
                                _oCurDoc.Application.Selection.MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=1)
                                wdRng = _oCurDoc.Application.Selection.Range
                            End While

                            'Added Lable For The Liquid Link
                            If IncludeCaption Then
                                '_oCurDoc.Application.Selection.Range.HighlightColorIndex = Wd.WdColorIndex.wdNoHighlight
                                _oCurDoc.Application.Selection.TypeText(CType(Arrlist.Item(i), myList).Description.ToString & ": ")
                            End If
                            ''Add the form field of type text

                            If _oCurDoc.Application.Selection.Range.Tables.Count = 0 Then
                                _oCurDoc.Application.Selection.TypeText(" ") '' IF TABLE NOT PRESENT THEN SEPERATE FIELDS BY SPACE ''
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
                                'If Not IsNothing(_oCurDoc.ActiveWindow.Selection.Range.Cells.Count) Then '' IF TABLE PRESENT THEN MOVE BY CELL ''
                                '    _oCurDoc.ActiveWindow.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)

                                'End If
                            End If

                            oNameField = _oCurDoc.FormFields.Add(_oCurDoc.Application.Selection.Range, Wd.WdFieldType.wdFieldFormTextInput)
                            oNameField.Result = CType(Arrlist.Item(i), myList).Description 'Result To show caption 
                            oNameField.StatusText = CType(Arrlist.Item(i), myList).Code 'Status text to hold Table & field names 
                            oNameField.HelpText = CType(Arrlist.Item(i), myList).Description 'Help text to hold group
                            '' replace the form field with data 
                            'oCurDoc1.Application.Selection.TypeText(vbTab)
                            _oCurDoc.Application.Selection.InsertParagraph()
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
            End Try


        End Sub

        Private Sub RemoveWordHandler()

            Try
                ' RemoveHandler oWordApp.WindowBeforeDoubleClick, AddressOf OnFormClicked
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
                'If isHandlerRemoved Then
                '    oWordApp = _oCurDoc.Application
                '    If Not oWordApp Is Nothing Then
                '        Try
                '            'RemoveHandler oWordApp.WindowBeforeDoubleClick, AddressOf OnFormClicked
                '            RemoveWordHandler()


                '            'Developer:Yatin N. Bhagat
                '            'Date:12/13/2011
                '            'Bug ID/PRD Name/Salesforce Case:Bug No 17073
                '            'Reason: Condition is Not applied for the Double Click Event on Word Document
                '            If LiquidFlag Then
                '                AddHandler oWordApp.WindowBeforeDoubleClick, AddressOf OnFormClicked
                '            End If

                '        Catch ex As Exception
                '            UpdateVoiceLog(ex.ToString)
                '        Finally
                '            'commented as not used
                '            'blnIsHandlers = False
                '            isHandlerRemoved = False
                '        End Try

                '    End If
                'End If
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
                    ''Please uncomment the following line of code to read the file, even the file is in use by same or another process
                    'oFile = New FileStream(strFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 8, FileOptions.Asynchronous)

                    ''To read the file only when it is not in use by any process
                    oFile = New FileStream(strFileName, FileMode.Open, FileAccess.Read)

                    oReader = New BinaryReader(oFile)
                    Dim bytesRead As Byte() = oReader.ReadBytes(oFile.Length)
                    Return bytesRead

                Catch ex As IOException
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    MsgBox("Error while conversion  - " & ex.ToString)
                    Return Nothing
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    MsgBox("Error while conversion  - " & ex.ToString)
                    Return Nothing

                Finally

                    ''22-Mar-13 Aniket: Resolving Memory Leak Issues
                    oReader.Close()
                    oReader.Dispose()
                    oReader = Nothing

                    oFile.Close()
                    oFile.Dispose()
                    oFile = Nothing

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



            oParamater = New DBParameter
            If (IsNothing(oParamater) = True) Then
                Return ("")
            End If

            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input

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

            oParamater.Name = _ParaName
            oParamater.Value = _DocumentCriteria.PrimaryID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
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
                If _oCurDoc.FormFields Is Nothing Then
                    Exit Sub
                End If

                If IsNothing(_oCurDoc) = True Then
                    Exit Sub 'Return Nothing
                End If
                If _oCurDoc.Application.ActiveDocument.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
                    _oCurDoc.Application.ActiveDocument.Unprotect()
                    blnUnprotect = True
                End If
                Dim aField As Wd.FormField 'Form field Variable
                For Each aField In _oCurDoc.FormFields
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
                Next
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Finally
                If blnUnprotect = True Then
                    _oCurDoc.Application.ActiveDocument.Protect(Wd.WdProtectionType.wdAllowOnlyComments)
                End If
            End Try
        End Sub
        Public Sub GetFormFieldData(ByVal DocumentType As enumDocType, Optional ByVal sStatusText As String = "", Optional ByVal GurantorID As Int64 = 0) 'Implements IgloEMRWord.GetFormFieldData

            _oCurDoc.Application.ActiveDocument.FormFields.Shaded = False 'Make shading false

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
                Try
                    Select Case aField.Type
                        Case Wd.WdFieldType.wdFieldFormTextInput
                            '' Compare table name with FormField status text
                            Dim strTableName = Split(aField.StatusText, ".")
                            If strTableName(0) <> "" Then
                                If (strTableName(0) = _HelpText Or _HelpText = "None") And (sStatusText = "" Or sStatusText = aField.StatusText) Then '' 2nd STATEMENT BY SUDHIR 20090706 '' TO FETCH ONLY SINGLE FIELD ''

                                    Select Case aField.StatusText
                                        Case Is <> ""
                                            If strTableName(1) <> "iPhoto" And strTableName(1) <> "imgSignature" And strTableName(1) <> "iCard|Driving" And strTableName(1) <> "iCard|Insurance" Then
                                                If _oCurDoc.Application.ActiveDocument.ProtectionType = Wd.WdProtectionType.wdNoProtection Then
                                                    If gblnWordColorHighlight Then
                                                        aField.Range.HighlightColorIndex = gblnWordBackColor
                                                    Else
                                                        aField.Range.HighlightColorIndex = Wd.WdColorIndex.wdNoHighlight
                                                    End If
                                                End If
                                            End If
                                            '' SUDHIR 20090711 '' TO FETCH USER NAME / LOGIN NAME ''
                                            'aField.
                                            If aField.StatusText.StartsWith("PA_Accounts_Patients") Or aField.StatusText.StartsWith("pa_accounts") Or aField.StatusText.StartsWith("Patient_OtherContacts.") Or aField.StatusText.StartsWith("pa_accounts_Billing.") Or aField.StatusText.StartsWith("pa_accounts_PatientLastClaimDiag.") Then
                                                If (GurantorID = 0) Then
                                                    If (isGuarantorAlreadySelected = False) Then
                                                        ''Bug : 00000875: Some Liquid Links not working from Orders and Results
                                                        Dim ofrmSelectPatientGuarantor As New gloWord.frmSelectPatientGuarantor(_DocumentCriteria.PatientID, gnClinicID)
                                                        Dim oClsSelectPatientGuarantor As New gloWord.clsSelectPatientGuarantor(_DocumentCriteria.PatientID, gnClinicID)
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
                                                    'Bug #76377: 00000806: LOGIN NAME LIQUID LINK IN ORDERS AND RESULTS
                                                    aField.Result = gloEMRGeneralLibrary.gloGeneral.globalSecurity.gstrLoginName 'gstrLoginName
                                                ElseIf aField.StatusText.StartsWith("User_MST.sFirstName") Then
                                                    aField.Result = GetDoctorName(gloEMRGeneralLibrary.gloGeneral.globalSecurity.gstrLoginName)
                                                End If
                                            Else
                                                '' SUDHIR 20091217 '' TO PRINT SEPERATOR BETWEEN HISTORY & COMMENT ''
                                                '' ONLY FOR HISTORY ''
                                                Dim _StatusText As String = aField.StatusText

                                                ''GLO2011-0010684 : ROS
                                                ''ROS item and ROS Comment seperated by colon (:).
                                                If (_StatusText.StartsWith("History") And _StatusText.EndsWith("Allergy") = False) Or _StatusText.Contains("ROS.sROSItem+ROS.sComments") Then
                                                    _StatusText = _StatusText.Replace("+", "+':'+")
                                                End If


                                                '' strData = getData_FromDB(Replace(_StatusText, "+", "+space(2)+"), aField.HelpText, dtTable)''Before we where providing 2 spaces in between the words now no
                                                strData = getData_FromDB(Replace(_StatusText, "+", "+space(1)+"), aField.HelpText, dtTable)   ''Now providing only 1 space between the words

                                                '' chetan added for maintaining past history on Oct 18 2010
                                                If strData.Trim.Length > 2 Then
                                                    CheckDataForPast_ExamDB(_StatusText, aField.HelpText, dtTable)
                                                End If
                                                '' chetan added for maintaining past history on Oct 18 2010

                                                If _StatusText.StartsWith("History") And _StatusText.EndsWith("Allergy") = False Then
                                                    ''Added by Mayuri on 20100309-To fix case:#GLO2010-0004643-On exams, after allergies there is an extra : symbol 
                                                    strData = strData.Replace(":  ", "").Replace(": ", "").Replace(" :  : ", "").Replace(" :  ", "").Replace(": |", "|").Replace(":  |", "|").Replace(" :  :  |", "|").Replace(" :  |", "|")



                                                End If
                                                '' ONLY FOR HISTORY ''
                                                '' END SUDHIR ''
                                                'Lines Added by dipak 20100114 to fix bug no 5284 appointment>Notes>print> Text after special character "|" is NOT displayed in Notes section on printout of appointment template (Test1|Test2)
                                                '29052012 Case no 00000104:Applied Trim()
                                                ''commented for Problem:00000186
                                                ''Start
                                                'strDataCols(0) = strData.Substring(0, strData.LastIndexOf("|")).Trim()
                                                ''End
                                                'strDataCols(0) = strData.Substring(0, strData.LastIndexOf("|"))
                                                'strDataCols(1) = strData.Substring(strData.LastIndexOf("|") + 1, strData.Length - strData.LastIndexOf("|") - 1)
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
                                                                        '_oCurDoc.Application.Selection.GoTo(What:=Wd.WdGoToItem.wdGoToBookmark, Name:=aField.Name)
                                                                        _oCurDoc.Application.Selection.Collapse()
                                                                        _oCurDoc.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                                                                        If File.Exists(strDataCols(0)) Then
                                                                            _oCurDoc.Application.Selection.InsertFile(strDataCols(0))
                                                                        End If
                                                                        Exit Select
                                                                    Case "3"
                                                                        aField.Result = "  "
                                                                        aField.Select()
                                                                        '_oCurDoc.Application.Selection.GoTo(What:=Wd.WdGoToItem.wdGoToObject, Which:=aField, Name:=aField.Name)
                                                                        _oCurDoc.Application.Selection.Collapse()
                                                                        _oCurDoc.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)


                                                                        If File.Exists(strDataCols(0)) Then
                                                                            ''Bug : 00000875: Some Liquid Links not working from Orders and Results
                                                                            If strDataCols(0).Contains("DasCalculater") Then

                                                                                aField.Result = "DAS"
                                                                                _oCurDoc.Application.Selection.Delete()
                                                                                _oCurDoc.Application.Selection.InlineShapes.AddPicture(strDataCols(0), False, True, _oCurDoc.ActiveWindow.Selection.Range)
                                                                                _oCurDoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
                                                                                _oCurDoc.Application.Selection.TypeText("   ")
                                                                                _oCurDoc.Application.Selection.MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=1)
                                                                                _oCurDoc.Application.Selection.ParagraphFormat.SpaceAfter = 5
                                                                            Else
                                                                                Global.gloWord.gloWord.InsertImageIntoSelectionField(_oCurDoc, strDataCols(0), "", "GloEmdeonGetFormFieldData1", False)
                                                                                
                                                                                'Dim oImage As Image = Image.FromFile(strDataCols(0))
                                                                                ''Dipak :20100610 :Code Added for Case GLO2010-0005258 to backup clipboard data
                                                                                ''end code added by dipak

                                                                                'If (IsNothing(oImage) = False) Then
                                                                                '    Global.gloWord.gloWord.GetClipboardData()
                                                                                '    'end code added by dipak
                                                                                '    ' Clipboard.Clear()


                                                                                '    Try
                                                                                '        Clipboard.SetImage(oImage)
                                                                                '    Catch ex As Exception

                                                                                '    End Try



                                                                                '    'If _StatusText.Contains("nDAS28") Then
                                                                                '    '    _oCurDoc.Application.Selection.Bookmarks.Add("DasImage")
                                                                                '    'End If
                                                                                '    Try
                                                                                '        _oCurDoc.Application.Selection.Paste()
                                                                                '    Catch ex As Exception

                                                                                '    End Try

                                                                                '    '   Clipboard.Clear()
                                                                                '    oImage.Dispose()
                                                                                '    oImage = Nothing


                                                                                '    'Dipak :20100610 :Code Added for Case GLO2010-0005258 to backup clipboard data
                                                                                '    Global.gloWord.gloWord.SetClipboardData()
                                                                                'End If

                                                                                'End Code added by dipak
                                                                                '_oCurDoc.Application.Selection.InlineShapes.AddPicture(FileName:=strDataCols(0), LinkToFile:=False, SaveWithDocument:=True).AlternativeText = "gloImageDataField"

                                                                                _oCurDoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
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
                                                                        If dtTable.Rows.Count > 0 Then
                                                                            aField.Result = "  "
                                                                            '_oCurDoc.Application.Selection.GoTo(What:=Wd.WdGoToItem.wdGoToBookmark, Name:=aField.Name)
                                                                            aField.Select()
                                                                            _oCurDoc.Application.Selection.Collapse()
                                                                            _oCurDoc.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                                                                            '_oCurDoc.Application.Selection.Expand()
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
                                                                        End If
                                                                    Case Else
                                                                        aField.Result = strDataCols(0)
                                                                End Select
                                                            Case Is > 255
                                                                Select Case strDataCols(1)
                                                                    Case "2"
                                                                        aField.Result = "  "
                                                                        aField.Select()
                                                                        '_oCurDoc.Application.Selection.GoTo(What:=Wd.WdGoToItem.wdGoToBookmark, Name:=aField.Name)
                                                                        _oCurDoc.Application.Selection.Collapse()
                                                                        _oCurDoc.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                                                                        If File.Exists(strDataCols(0)) Then
                                                                            _oCurDoc.Application.Selection.InsertFile(strDataCols(0))
                                                                        End If
                                                                        Exit Select
                                                                    Case "3"
                                                                        aField.Result = "  "
                                                                        aField.Select()
                                                                        '_oCurDoc.Application.Selection.GoTo(What:=Wd.WdGoToItem.wdGoToBookmark, Name:=aField.Name)
                                                                        _oCurDoc.Application.Selection.Collapse()
                                                                        _oCurDoc.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                                                                        If File.Exists(strDataCols(0)) Then
                                                                            ''Bug : 00000875: Some Liquid Links not working from Orders and Results
                                                                            If strDataCols(0).Contains("DasCalculater") Then
                                                                                aField.Result = "DAS"
                                                                                _oCurDoc.ActiveWindow.Selection.Delete()
                                                                                _oCurDoc.ActiveWindow.Selection.InlineShapes.AddPicture(strDataCols(0), False, True, _oCurDoc.ActiveWindow.Selection.Range)
                                                                                _oCurDoc.ActiveWindow.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
                                                                                _oCurDoc.ActiveWindow.Selection.TypeText("   ")
                                                                                _oCurDoc.ActiveWindow.Selection.MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=1)
                                                                                _oCurDoc.ActiveWindow.Selection.ParagraphFormat.SpaceAfter = 5
                                                                            Else
                                                                                Global.gloWord.gloWord.InsertImageIntoSelectionField(_oCurDoc, strDataCols(0), "", "GloEmdeonGetFormFieldData2", False)

                                                                                '    Dim oImage As Image = Image.FromFile(strDataCols(0))
                                                                                '    'Dipak :20100610 :Code Added for Case GLO2010-0005258 to backup clipboard data
                                                                                '    If (IsNothing(oImage) = False) Then
                                                                                '        Global.gloWord.gloWord.GetClipboardData()
                                                                                '        ''end code added by dipak
                                                                                '        ' Clipboard.Clear()


                                                                                '        Try
                                                                                '            Clipboard.SetImage(oImage)
                                                                                '        Catch ex As Exception

                                                                                '        End Try
                                                                                '        Try
                                                                                '            _oCurDoc.Application.Selection.Paste()
                                                                                '        Catch ex As Exception

                                                                                '        End Try

                                                                                '        ' Clipboard.Clear()
                                                                                '        oImage.Dispose()
                                                                                '        oImage = Nothing


                                                                                '        'Dipak :20100610 :Code Added for Case GLO2010-0005258 to backup clipboard data
                                                                                '        Global.gloWord.gloWord.SetClipboardData()
                                                                                '    End If
                                                                                '    '    'end code added buy dipak

                                                                                '    '_oCurDoc.Application.Selection.InlineShapes.AddPicture(FileName:=strDataCols(0), LinkToFile:=False, SaveWithDocument:=True).AlternativeText = "gloImageDataField"
                                                                            End If
                                                                        End If
                                                                        Exit Select
                                                                    Case "4"
                                                                        '' For Decimal Datatype
                                                                        aField.Result = Convert.ToDecimal(strDataCols(0))
                                                                    Case "5"
                                                                        aField.Result = GetAge(CType(strDataCols(0), Date))
                                                                    Case "6" '' To Generate Data in Tabular Form '' Now for Flowsheet or Vital
                                                                        If dtTable.Rows.Count > 0 Then
                                                                            aField.Result = "  "
                                                                            aField.Select()
                                                                            '_oCurDoc.Application.Selection.GoTo(What:=Wd.WdGoToItem.wdGoToBookmark, Name:=aField.Name)
                                                                            _oCurDoc.Application.Selection.Collapse()
                                                                            _oCurDoc.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                                                                            InsertWordTable(dtTable, aField)
                                                                        Else
                                                                            aField.Result = "|" & Replace(aField.HelpText, "|", "") & "|"
                                                                        End If
                                                                    Case Else
                                                                        aField.Result = "  "
                                                                        aField.Select()
                                                                        '_oCurDoc.Application.Selection.GoTo(What:=Wd.WdGoToItem.wdGoToBookmark, Name:=aField.Name)
                                                                        _oCurDoc.Application.Selection.Collapse()
                                                                        _oCurDoc.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                                                                        _oCurDoc.Application.Selection.TypeText(strDataCols(0))
                                                                        _oCurDoc.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                                                                End Select
                                                        End Select
                                                    Case Else
                                                        'aField.Result = aField.HelpText
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
                If strFields.EndsWith("CurrentAppointment") Or strFields.StartsWith("AS_Appointment_DTL") Then
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
                '  Dim filecnt As Int16
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

                    If strFields.StartsWith("FlowSheet") And strFields.EndsWith("SingleRow") Then
                        oParamater.Value = Mid(strFields, 1, InStrRev(strFields, "|") - 1)
                    Else
                        oParamater.Value = strFields
                    End If
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    If strFields.StartsWith("FAX") Then
                        strDataCol = strData & "|" & ResultDataType.ToString
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
            'Dim cls As New clsPatientExams

            ''22-Mar-13 Aniket: Resolving Memory Leak Issues
            'Dim dt As DataTable

            ''Line commented and modifid by dipak 20100826 for case UC5070.003 to limit use of gnpatientID
            ''dt = cls.getHistoryVisitsAfterDate(dtLetterDOS, gnPatientID)
            'dt = cls.getHistoryVisitsAfterDate(dtLetterDOS, nPatientID)
            'Try
            '    'end modification
            '    cls = Nothing
            '    If dt.Rows.Count > 0 Then
            '        If Format(dt.Rows(0)("VisitDate"), "MM/dd/yyyy") <> Format(dtLetterDOS, "MM/dd/yyyy") Then
            '            '' History Not Exists on dtDOS
            '            '' If History of the DOS is not exists then ask user about Pull History 
            '            If MessageBox.Show("History of the date '" & dtLetterDOS & "' is not entered." & vbCrLf & "Do you want to pull the History of the date '" & Format(dt.Rows(0)("VisitDate"), "MM/dd/yyyy") & "'?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            '                Return RetriveAndSaveHistory(dt.Rows(0)("VisitID"), CurrentVisitID)
            '            End If
            '        End If
            '    Else
            '        Return True
            '    End If
            'Catch ex As Exception
            '    If Not IsNothing(dt) Then
            '        dt.Dispose()
            '        dt = Nothing
            '    End If
            '    If Not IsNothing(cls) Then
            '        cls = Nothing
            '    End If
            'End Try
            Return Nothing
        End Function

      

#End Region


        '' SUDHIR 20091012 ''
        '' IF NO DATA AVAILABLE IN ROW THEN REMOVE THAT ROW ''
        Private Function RemoveBlankRows(ByVal dtTable As DataTable, ByVal enmDataDictionaryType As clsDataDictionary.enumDictionaryType) As DataTable
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
        Public Function ExportData(ByVal oCurDoc1 As Wd.Document, ByVal sFilename As String, Optional ByVal sDocType As String = "Document") As Boolean
            ' ''' '''' Export Function for Word Docs added by Ujwala Atre as on 18032010
            'ExportData = False
            'If oCurDoc1 Is Nothing Then
            '    Exit Function
            'End If

            'Dim oResult As DialogResult = MessageBox.Show("Do you want to encrypt the document?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            'Dim blnSecureDoc As Boolean = False
            'Dim blnSecureContinue As Boolean = False
            'Dim sEncryotKey As String = ""
            'Dim bEncryptedExe As Boolean = True

            'If oResult = DialogResult.Yes Then

            '    Dim ofrmExportEncryption As New frmExportEncryption
            '    ofrmExportEncryption.ShowInTaskbar = False
            '    ofrmExportEncryption.ShowDialog()

            '    If ofrmExportEncryption.DialogResult = DialogResult.OK Then
            '        sEncryotKey = ofrmExportEncryption.sEncryptKey
            '        bEncryptedExe = ofrmExportEncryption.bEncryptedExe
            '        blnSecureDoc = True
            '        blnSecureContinue = True
            '    ElseIf ofrmExportEncryption.DialogResult = DialogResult.Cancel Then
            '        blnSecureDoc = False
            '        blnSecureContinue = False

            '    End If

            '    ofrmExportEncryption.Dispose()
            '    ofrmExportEncryption = Nothing

            'End If

            ''If blnSecureContinue = False Then Exit Function

            ' '''''''''''''
            'Dim Sfd1 As New SaveFileDialog
            'Dim SaveFrmt As Wd.WdSaveFormat
            ' ''Word Macro-Enabled Document(*.docm) |*.docm |
            ' ''Word Macro-Enabled Template(*.dotm) |*.dotm |
            ' ''
            'Sfd1.Filter = "Word 97-2003 Document(*.doc) |*.doc |Word Document(*.docx) |*.docx |Word Template(*.dotx) |*.dotx |Word 97-2003 Template(*.dot) |*.dot |RichText Format (*.rtf) |*.rtf |Adobe Acrobat (*.pdf) |*.pdf |XPS Document(*.xps) |*.xps |Web Page(*.htm;*.html) |*.htm;*.html |Plain Text(*.txt) |*.txt |Word XML Document(*.xml) |*.xml "
            'Sfd1.FilterIndex = 2
            'Sfd1.RestoreDirectory = True
            ''Sfd1.CreatePrompt = True
            'Sfd1.FileName = sFilename
            'If Sfd1.ShowDialog = Windows.Forms.DialogResult.OK Then
            '    Select Case Sfd1.FilterIndex
            '        Case 1
            '            SaveFrmt = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocument97
            '        Case 2
            '            SaveFrmt = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocument
            '        Case 3
            '            SaveFrmt = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatTemplate
            '        Case 4
            '            SaveFrmt = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatTemplate97
            '        Case 5
            '            SaveFrmt = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatRTF
            '        Case 6
            '            SaveFrmt = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF
            '        Case 7
            '            SaveFrmt = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatXPS
            '        Case 8
            '            SaveFrmt = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatHTML
            '        Case 9
            '            SaveFrmt = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatText
            '        Case 10
            '            SaveFrmt = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatXMLDocument
            '    End Select


            '    If SaveFrmt = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocument Then
            '        ''                    
            '        If IsNothing(oCurDoc1) = False Then
            '            If oCurDoc1.Saved = False Then
            '                'Saveit = MessageBox.Show("Do you want to save the changes to " & sDocType & "?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            '                'If Saveit = MsgBoxResult.Yes Then
            '                oCurDoc1.SaveAs(oCurDoc1.FullName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
            '                'End If
            '            End If
            '        End If
            '        FileSystem.FileCopy(oCurDoc1.FullName, Sfd1.FileName)
            '    Else
            '        oCurDoc1.SaveAs(Sfd1.FileName, SaveFrmt, False, "", False)
            '    End If

            '    FileSystem.FileClose()

            '    If blnSecureDoc = True And blnSecureContinue = True Then

            '        Dim _retVal As String
            '        _retVal = ""
            '        _retVal = gloSecurity.gloEncryption.PerformFileEncryption(Sfd1.FileName, sEncryotKey, bEncryptedExe)

            '        If Not IsNothing(_retVal) And _retVal.Trim() <> "" Then
            '            Dim oFile As System.IO.File
            '            oFile.Delete(Sfd1.FileName)
            '        End If

            '        Select Case sDocType
            '            Case "Form Gallery"
            '                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Encryption, gloAuditTrail.ActivityType.Export, "Form Gallery Exported with Encryption", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            '            Case "Disclosure Management"
            '                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.Encryption, gloAuditTrail.ActivityType.Export, "Disclosure Management with Encryption", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            '            Case "Patient Message"
            '                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.Encryption, gloAuditTrail.ActivityType.Export, "Patient Message Exported with Encryption", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            '            Case "Nurses Note"
            '                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.Encryption, gloAuditTrail.ActivityType.Export, "Nurses Note Exported with Encryption", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            '            Case "Patient Consent"
            '                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.Encryption, gloAuditTrail.ActivityType.Export, "Patient Consent Exported with Encryption", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            '            Case "Patient Exam"
            '                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Encryption, gloAuditTrail.ActivityType.Export, "Patient Exam Exported with Encryption", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            '            Case "PT Protocol"
            '                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.Encryption, gloAuditTrail.ActivityType.Export, "PT Protocol Exported with Encryption", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            '            Case "Referral letter"
            '                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.Encryption, gloAuditTrail.ActivityType.Export, "Referral letter Exported with Encryption", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            '            Case "Triage"
            '                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.Encryption, gloAuditTrail.ActivityType.Export, "Triage Exported with Encryption", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            '            Case "Patient letter"
            '                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.Encryption, gloAuditTrail.ActivityType.Export, "Patient letter Exported with Encryption", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            '            Case Else
            '                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.Encryption, gloAuditTrail.ActivityType.Export, sDocType & " Exported with Encryption", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            '        End Select
            '    Else
            '        Select Case sDocType
            '            Case "Form Gallery"
            '                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Export, "Form Gallery Exported", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            '            Case "Disclosure Management"
            '                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Export, "Disclosure Management", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            '            Case "Patient Message"
            '                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Export, "Patient Message Exported", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            '            Case "Nurses Note"
            '                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Export, "Nurses Note Exported", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            '            Case "Patient Consent"
            '                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Export, "Patient Consent Exported", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            '            Case "Patient Exam"
            '                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Export, "Patient Exam Exported", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            '            Case "PT Protocol"
            '                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Export, "PT Protocol Exported", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            '            Case "Referral letter"
            '                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Export, "Referral letter Exported", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            '            Case "Triage"
            '                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Export, "Triage Exported", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            '            Case "Patient letter"
            '                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Export, "Patient letter Exported", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            '            Case Else
            '                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.Encryption, gloAuditTrail.ActivityType.Export, sDocType & " Exported.", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            '        End Select
            '    End If

            '    ExportData = True

            '    Sfd1.Dispose()
            '    Sfd1 = Nothing

            'End If
            '
            Return Nothing
        End Function

        Public Function GetLoginUserName(ByVal UserID As Int64) As String

            Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
            Dim _strLoginUserName As String = String.Empty
            Try
                oDB.Connect(False)
                'ProID = Trim(oDB.ExecuteScaler)
                _strLoginUserName = Convert.ToString(oDB.ExecuteScalar_Query("Select sLoginName from dbo.User_MST where nUserID =" & Convert.ToString(UserID) & ""))
                oDB.Disconnect()
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)

                _strLoginUserName = ""
            Finally
                If oDB IsNot Nothing Then
                    oDB.Dispose()
                End If
            End Try
            Return _strLoginUserName
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


                gnLoginID = DocumentCriteria.PrimaryID
                gnLoginProviderID = DocumentCriteria.ProviderID
                gstrLoginName = GetLoginUserName(gnLoginID)

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
                                If IsNothing(oclsProvider) = False Then
                                    oclsProvider.Dispose()
                                    oclsProvider = Nothing
                                End If

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
                                If dt.Rows.Count = 0 Then
                                    If _ProviderID <> gnLoginProviderID Then
                                        MessageBox.Show("You are not designated as a Signature Delegate for any providers.  No provider signatures are available for you to use. Signature delegates may be added in Provider setup in gloEMR Admin.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Return ProviderSign
                                    End If
                                Else
                                    If dt.Rows.Count > 0 Then
                                        For i = 0 To dt.Rows.Count - 1
                                            If _ProviderID = dt.Rows(i)("nProviderId").ToString().Trim() Or _ProviderID = gnLoginProviderID Then
                                                _IsSignRight = True
                                            End If
                                        Next
                                        If _IsSignRight = False Then
                                            Dim strName As String = oclsProvider.GetPatientProviderNameWithPrefix(m_PatientID)
                                            MessageBox.Show("User '" & gstrLoginName & "' is not designated as a Signature Delegate for '" & strName & "'. Signature Delegates may be assigned via the ‘Provider’ option in gloEMR Admin.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                            If IsNothing(oclsProvider) = False Then
                                                oclsProvider.Dispose()
                                                oclsProvider = Nothing
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
                        If dt.Rows.Count = 0 Then
                            If _ProviderID <> gnLoginProviderID Then
                                MessageBox.Show("You are not designated as a Signature Delegate for any providers.  No provider signatures are available for you to use. Signature delegates may be added in Provider setup in gloEMR Admin.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                If IsNothing(oclsProvider) = False Then
                                    oclsProvider.Dispose()
                                    oclsProvider = Nothing
                                End If

                                Return ProviderSign
                                'Exit Function
                                ''Return
                            End If
                        Else
                            If dt.Rows.Count > 0 Then
                                For i = 0 To dt.Rows.Count - 1
                                    If _ProviderID = dt.Rows(i)("nProviderId").ToString().Trim() Or _ProviderID = gnLoginProviderID Then
                                        _IsSignRight = True
                                    End If
                                Next
                                If _IsSignRight = False Then
                                    Dim strName As String = oclsProvider.GetPatientProviderNameWithPrefix(m_PatientID)

                                    MessageBox.Show("User '" & gstrLoginName & "' is not designated as a Signature Delegate for '" & strName & "'. Signature Delegates may be assigned via the ‘Provider’ option in gloEMR Admin.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    If IsNothing(oclsProvider) = False Then
                                        oclsProvider.Dispose()
                                        oclsProvider = Nothing
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
                objCriteria.ProviderID = ProviderId
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


                Dim oDBLayer As New gloDatabaseLayer.DBLayer(GetConnectionString())
                Dim _strSQL As String = String.Empty
                Dim _objIFax As New Object
                Dim gstrSignatureText As String = ""

                Try
                    oDBLayer.Connect(False)
                    _strSQL = "select sSettingsValue from Settings where sSettingsName like 'SIGNATURETEXT'"
                    _objIFax = oDBLayer.ExecuteScalar_Query(_strSQL)

                    If Not IsNothing(_objIFax) Then
                        gstrSignatureText = _objIFax
                    Else
                        gstrSignatureText = ""
                    End If
                    oDBLayer.Disconnect()

                Catch ex As Exception
                    If Not IsNothing(oDBLayer) Then
                        oDBLayer.Dispose()
                    End If
                    _objIFax = Nothing
                End Try


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
            End Try
        End Function

        Private Function RetriveAndSaveHistory(ByVal p1 As Object, ByVal CurrentVisitID As Long) As Boolean
            Throw New NotImplementedException
        End Function


#Region "Check Word For Exception"
        Public Function CheckWordForException() As Boolean
            Dim ofrmTest As New gloEMRWord.frmDSOTest
            Try
                If ofrmTest.ShowDialog(ofrmTest.Parent) = DialogResult.Yes Then
                    Return True
                Else
                    MessageBox.Show(ofrmTest.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return False
                End If
            Catch ex As Exception
                Return True
            Finally
                If (IsNothing(ofrmTest) = False) Then
                    ofrmTest.Dispose()
                    ofrmTest = Nothing
                End If
            End Try
        End Function
#End Region

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    'If Me.LiquidLinkOutputList IsNot Nothing Then
                    '    If Me.LiquidLinkOutputList.Any() Then
                    '        For Each element As LiquidLinkOutput In Me.LiquidLinkOutputList
                    '            element.Dispose()
                    '            element = Nothing
                    '        Next

                    '        Me.LiquidLinkOutputList.Clear()
                    '    End If

                    '    Me.LiquidLinkOutputList = Nothing
                    'End If
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

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
    Public Class DocCriteria
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

        Private _ProviderID As Int64 = 0
        Public Property ProviderID() As Int64
            Get
                Return _ProviderID
            End Get
            Set(ByVal value As Int64)
                _ProviderID = value
            End Set
        End Property

        Public Sub New()
            MyBase.new()
            '            _TableName = New TableName
            gstrMessageBoxCaption = GetMessageBoxCaption()
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
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

End Namespace

