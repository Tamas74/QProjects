Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System
Public Class gloUC_AddRefreshDic

    Dim oCurDoc As Object = Nothing
    Public Property OCURDOCs() As Object
        Get
            Return oCurDoc
        End Get
        Set(ByVal value As Object)
            oCurDoc = value
        End Set
    End Property
    Dim objWord As Object = Nothing
    Public Property OBJWORDs() As Object
        Get
            Return objWord
        End Get
        Set(ByVal value As Object)
            objWord = value
        End Set
    End Property
    Dim oWordApp As Object = Nothing
    Public Property OWORDAPPs() As Object
        Get
            Return oWordApp
        End Get
        Set(ByVal value As Object)
            oWordApp = value
        End Set
    End Property
    Dim objCriteria As Object = Nothing
    Public Property OBJCRITERIAs() As Object
        Get
            Return objCriteria
        End Get
        Set(ByVal value As Object)
            objCriteria = value
        End Set
    End Property
    Dim m_PatientID As Int64 = Nothing
    Public Property M_PATIENTIDs() As Int64
        Get
            Return m_PatientID
        End Get
        Set(ByVal value As Int64)
            m_PatientID = value
        End Set
    End Property
    ''Added on 20150929-To show ordering provider in ordertemplates for provider signature
    Dim m_ProviderID As Int64 = 0
    Public Property M_ProviderIDs() As Int64
        Get
            Return m_ProviderID
        End Get
        Set(ByVal value As Int64)
            m_ProviderID = value
        End Set
    End Property


    Dim AxFramerControl2 As AxDSOFramer.AxFramerControl
    Public Property wdPatientWordDocs() As AxDSOFramer.AxFramerControl
        Get
            Return AxFramerControl2
        End Get
        Set(ByVal value As AxDSOFramer.AxFramerControl)
            AxFramerControl2 = value
        End Set
    End Property
    Dim dtLetterdate As System.Windows.Forms.DateTimePicker = Nothing
    Dim dtAllocated As Boolean = False
    Public Property dtLetterAllocated() As Boolean
        Get
            Return dtAllocated
        End Get
        Set(ByVal value As Boolean)
            dtAllocated = value
        End Set
    End Property
    Public Property DTLETTERDATEs() As System.Windows.Forms.DateTimePicker
        Get
            Return dtLetterdate
        End Get
        Set(ByVal value As System.Windows.Forms.DateTimePicker)
            'SLR: there is an assignment directly referenced from gloUC_PatientStrip1.DTP in frmLMOrders.Vb without allocation and hence can not be disposed. But this is a dangerous implementation eating all memories.
            If (dtAllocated) Then
                Try
                    If (IsNothing(dtLetterdate) = False) Then
                        Try
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtLetterdate)
                        Catch ex As Exception

                        End Try
                        dtLetterdate.Dispose()
                        dtLetterdate = Nothing
                    End If
                Catch
                End Try
                dtAllocated = False
            End If
            dtLetterdate = value
        End Set
    End Property
    Dim objfrm As Object = Nothing
    Public Property ObjFrom() As Object
        Get
            Return objfrm
        End Get
        Set(ByVal value As Object)
            objfrm = value
        End Set
    End Property
    
    Dim ConnectionString As String = Nothing
    Public Property CONNECTIONSTRINGs() As String
        Get
            Return ConnectionString
        End Get
        Set(ByVal value As String)
            ConnectionString = value
        End Set
    End Property

    Public Sub ShowRefreshButton(ByVal _showRefresh As Boolean)
        If _showRefresh Then
            btnRefresh.Visible = True
        Else
            btnRefresh.Visible = False
        End If
    End Sub



    Private Sub btnAddFields_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddFields.Click
        ActivateOcDoc()
        Addformfields()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        ActivateOcDoc()
        GetFormFieldData()
    End Sub
    Public Sub ActivateOcDoc()
        If Not IsNothing(AxFramerControl2) Then



            If AxFramerControl2.DocumentName <> "" Then
                oCurDoc = AxFramerControl2.ActiveDocument
            End If
        End If



    End Sub
    Public Sub Addformfields()

        Try
            If ObjFrom.Name = "frmSummaryofVisit" Then
                MessageBox.Show("Addition of Exam specific liquid links is not supported in Referral Letter." & vbCrLf & "Please add it to the Exam Note instead. ", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
            InitialiseWordObject()
            If Not oCurDoc Is Nothing Then
                objWord.ValidateSelection(oCurDoc)
                objWord.AddDictionaryParent = objfrm

                'Developer:Yatin N. Bhagat
                'Date:12/13/2011
                'Bug ID/PRD Name/Salesforce Case:Bug No 17073:Patient Exam >> Application is opening modify Patient Form Two times through Liquid 
                'Reason: Flag is Settled for Applying double click event on Word Document
                If ObjFrom.Name = "frmMessages" Or ObjFrom.Name = "frmPatientExam" Or ObjFrom.Name = "frmTriage" Or ObjFrom.Name = "frmPatientLetter" Then
                    objWord.LiquidFlag = True
                Else
                    objWord.LiquidFlag = False
                End If


                Call objWord.AddDataDictionaryFields()
                oCurDoc = objWord.CurDocument
                oCurDoc.ActiveWindow.SetFocus()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Visit, gloAuditTrail.ActivityCategory.UC_AddRefreshDic, gloAuditTrail.ActivityType.Add, ex.ToString, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
        End Try

    End Sub
    Public Sub GetFormFieldData()
        Me.Cursor = Cursors.WaitCursor
        GetdataFromOtherForms(enumDocType.None)
        Me.Cursor = Cursors.Default
    End Sub

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
    End Enum

    Public Sub GetdataFromOtherForms(ByVal _DocType As enumDocType)
        Try
            oCurDoc.ActiveWindow.SetFocus()
            ' If IsNothing(objWord) Then
            InitialiseWordObject()
            ' End If
            If objWord.CurDocument Is Nothing Then
                objWord.CurDocument = oCurDoc
            End If
            objWord.GetFormFieldData(_DocType)
            oCurDoc = objWord.CurDocument

            oWordApp = oCurDoc.Application
            If Not oWordApp Is Nothing Then
                Try

                Catch ex As Exception
                    '' UpdateVoiceLog(ex.ToString)
                Finally

                End Try
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Visit, gloAuditTrail.ActivityCategory.UC_AddRefreshDic, gloAuditTrail.ActivityType.Add, ex.ToString, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
        Finally

        End Try
    End Sub
    Private Sub InitialiseWordObject()
        If Not IsNothing(AxFramerControl2) Then
            If AxFramerControl2.DocumentName <> "" Then
                objWord.myCallingForm = ObjFrom
                Try
                    objWord.CurDocument = AxFramerControl2.ActiveDocument
                Catch ex As Exception

                End Try
                objCriteria.PatientID = m_PatientID
                objCriteria.ProviderID = m_ProviderID
                If (IsNothing(dtLetterdate) = False) Then
                    objCriteria.VisitID = GenerateVisitID(dtLetterdate.Value, m_PatientID)
                End If
                objWord.DocumentCriteria = objCriteria
            End If
        End If
    End Sub

    Public Function GenerateVisitID(ByVal VisitDate As Date, ByVal PatientID As Int64) As Long
        Dim con As SqlConnection = Nothing
        Dim cmdVisits As SqlCommand
        Dim objParam As SqlParameter = Nothing
        Dim objFlagParam As SqlParameter
        Dim _returnValue As Long = 0

        Try
            'Call InitialzeCon()
            con = New SqlConnection(ConnectionString)
            cmdVisits = New SqlCommand("gsp_InsertVisits", con)
            cmdVisits.CommandType = CommandType.StoredProcedure

            objParam = cmdVisits.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = PatientID

            objParam = cmdVisits.Parameters.Add("@dtVisitdate", SqlDbType.DateTime)

            objParam.Direction = ParameterDirection.Input
            objParam.Value = VisitDate

            Dim nAppointmentID As Long
            nAppointmentID = 0

            objParam = cmdVisits.Parameters.Add("@AppointmentID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = nAppointmentID

            objFlagParam = cmdVisits.Parameters.Add("@flag", SqlDbType.Int)
            objFlagParam.Direction = ParameterDirection.Output

            objParam = cmdVisits.Parameters.Add("@MachineID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = OBJWORDs.GetPrefixTransactionID(m_PatientID)

            objParam = cmdVisits.Parameters.Add("@VisitID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Output
            objParam.Value = 0

            con.Open()
            cmdVisits.ExecuteNonQuery()

            _returnValue = objParam.Value

            If objFlagParam.Value = 0 Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.UC_AddRefreshDic, gloAuditTrail.ActivityType.General, CType(Now, String), PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If

            objFlagParam = Nothing
            cmdVisits.Parameters.Clear()
            cmdVisits.Dispose()
            cmdVisits = Nothing
            con.Close()
            con.Dispose()
            con = Nothing

            Return _returnValue
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.UC_AddRefreshDic, gloAuditTrail.ActivityType.General, CType(Now, String), PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
            Return Nothing
        Finally
            If Not IsNothing(con) Then
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Dispose()
                con = Nothing
            End If
            If Not IsNothing(objParam) Then
                objParam = Nothing
            End If
        End Try
    End Function

End Class
