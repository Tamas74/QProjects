Imports ECGMgmtCom
Imports QuintonECG
Imports AxQuintonECG
Imports gloPatient
Imports System.Windows.Forms
Imports System.Runtime.InteropServices

Public Class frmHealthCentrixLoad

    Dim _nPatientId As Int64 = 0
    Dim _sValue As String = String.Empty
    Dim _sType As ProcessTypes
    Dim _sPresentStatus As String = String.Empty
    Dim _sConnectionString As String = String.Empty
    Dim _sCurrentProcess As String = String.Empty
    Dim _nVisitId As Long = 0
    Dim _nMachineID As Long = 0
    Dim _nProcessid As Long = 0
    Dim sErrorString As String = String.Empty
    Dim _sOrderID As String = String.Empty
    Dim _sTestId As String = String.Empty

    Private _nECGID As Long
    Public Property ECGID() As Long
        Get
            Return _nECGID
        End Get
        Set(ByVal value As Long)
            _nECGID = value
        End Set
    End Property


    Public Enum ProcessTypes
        GetOrderStatus
        GetSelectedOrderInfo
        PlaceOrder
        CancelAndPlaceNewOrder
        Login
    End Enum

    Private Enum OrderStatus
        Pending
        Confirmed
    End Enum
    Public Property ErrorString() As String
        Get
            Return sErrorString
        End Get
        Set(ByVal value As String)
            sErrorString = value
        End Set
    End Property
    Public Sub New(ByVal nPatientId As Int64, ByVal enmType As ProcessTypes, ByVal sValue As String, ByVal nVisitId As Long, ByVal MachineId As Long)
        ' This call is required by the Windows Form Designer.
        _nPatientId = nPatientId
        _sConnectionString = mdlEcgProcessLayer.sConnectionString
        _sType = enmType
        _sValue = sValue
        _nVisitId = nVisitId
        _nMachineID = MachineId
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Public Sub New(ByVal EcgControl As AxQuintonECG.AxECGIntegrationCtrl, ByVal nPatientId As Int64, ByVal enmType As ProcessTypes, ByVal sOrderId As String, ByVal sTestId As String, ByVal nVisitId As Long, ByVal MachineId As Long)
        ' This call is required by the Windows Form Designer.
        _nPatientId = nPatientId
        _sConnectionString = mdlEcgProcessLayer.sConnectionString
        _sType = enmType
        _sOrderID = sOrderId
        _sTestId = sTestId
        _nVisitId = nVisitId
        _nMachineID = MachineId
        InitializeComponent()
        EI = EcgControl

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Sub New(ByVal EcgControl As AxQuintonECG.AxECGIntegrationCtrl, ByVal enmProcessTypes As ProcessTypes)

        _sType = enmProcessTypes
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        EI = EcgControl
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub bgWorker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgWorker.DoWork

        Dim sOrderID As String = String.Empty
        Application.DoEvents()
        Try
            ''Check The type of Process
            Select Case _sType
                Case ProcessTypes.GetOrderStatus
                    ''get pending order status.
                    _sCurrentProcess = "Connecting with Server."
                    bgWorker.ReportProgress(10)

                    If Login(sErrorString) Then
                        bgWorker.ReportProgress(35)
                        If GetOrderStatus() = False Then
                            sErrorString = "Test result not available, Please try again later."
                        End If
                        bgWorker.ReportProgress(100)
                    Else
                        sErrorString = "Login failed :" & sErrorString
                    End If

                Case ProcessTypes.PlaceOrder

                    ''Place order in sphinix.
                    bgWorker.ReportProgress(10)
                    If Login(sErrorString) Then
                        bgWorker.ReportProgress(35)
                        sOrderID = AddNewOrder(sErrorString)
                        If sOrderID.Length > 0 Then
                            ECGID = SaveOrder(sOrderID)
                            bgWorker.ReportProgress(100)
                        End If
                    Else
                        sErrorString = "Login failed :" & sErrorString
                    End If

                Case ProcessTypes.CancelAndPlaceNewOrder
                    'Cancel exixting order And place new order.
                    bgWorker.ReportProgress(5)
                    If Login(sErrorString) Then
                        If ProcessCancelOrder(sErrorString) Then
                            bgWorker.ReportProgress(35)
                            sOrderID = AddNewOrder(sErrorString)
                            If sOrderID.Length > 0 Then
                                ECGID = SaveOrder(sOrderID)
                                bgWorker.ReportProgress(100)
                            End If
                        End If
                    Else
                        sErrorString = "Login failed :" & sErrorString
                    End If
                Case ProcessTypes.Login
                    bgWorker.ReportProgress(10)

                    If Login(sErrorString) Then
                        bgWorker.ReportProgress(100)
                        _nProcessid = 1
                    Else
                        sErrorString = "Login failed :" & sErrorString
                    End If
                    Exit Select
                Case ProcessTypes.GetSelectedOrderInfo
                    _sCurrentProcess = "Connceting to server."
                    bgWorker.ReportProgress(10)
                    GetLatestTestData(Convert.ToInt16(_sTestId), _sOrderID, sErrorString)
                    bgWorker.ReportProgress(100)
                    _nProcessid = 1
                    Exit Select
                Case Else

            End Select
            _nProcessid = 1
        Catch ex As Exception
            sErrorString = String.Empty
            sOrderID = String.Empty
            gloAuditTrail.gloAuditTrail.ExceptionLog(sErrorString, False)
        Finally

        End Try
    End Sub

    Private Sub bgWorker_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bgWorker.ProgressChanged
        Application.DoEvents()
        pbBar.Increment(e.ProgressPercentage)
        Application.DoEvents()
        lblInformation.Text = _sCurrentProcess
        Application.DoEvents()
    End Sub

    Private Sub bgWorker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgWorker.RunWorkerCompleted
        If _nProcessid = 1 Then

            Me.Close()

            If _sType <> ProcessTypes.Login Then
                EI.Logout()
            End If
        End If
    End Sub

    Private Sub frmHealthCentrixLoad_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        lblInformation.Text = "Processing.."
        Application.DoEvents()
        'pbBar.MarqueeAnimationSpeed = 3
        'pbBar.Style = Windows.Forms.ProgressBarStyle.Marquee

        bgWorker.RunWorkerAsync()
        Application.DoEvents()
    End Sub

    ''' <summary>
    ''' Method to Login to HealthCentrix.
    ''' </summary> 
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Login(ByRef sErrorString As String) As Boolean
        Dim _blnLogin As Boolean = False
        Try
            bgWorker.ReportProgress(15)
            _sCurrentProcess = "Connecting with Server..."
            bgWorker.ReportProgress(20)

            EI.UserProviderID = mdlEcgProcessLayer.sECGProviderId
            EI.WebURL = mdlEcgProcessLayer.sECGUrl
            EI.LoginByUser(mdlEcgProcessLayer.sECGUserName, mdlEcgProcessLayer.sECGPassword)
            bgWorker.ReportProgress(25)
            _blnLogin = True
            bgWorker.ReportProgress(27)
            _sCurrentProcess = "Connected with Server..."

            bgWorker.ReportProgress(30)

        Catch ex As COMException
            _blnLogin = False
            sErrorString = mdlEcgProcessLayer.GetErrorString(ex.ErrorCode)
            gloAuditTrail.gloAuditTrail.ExceptionLog(sErrorString, False)
        Catch ex As Exception
            _blnLogin = False
            gloAuditTrail.gloAuditTrail.ExceptionLog(sErrorString, False)
        End Try
        Return _blnLogin
    End Function
    ''' <summary>
    ''' Process to cancel order.
    ''' </summary>
    ''' <param name="sErrorString"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ProcessCancelOrder(ByRef sErrorString As String) As Boolean
        Dim sOrderid As String = String.Empty
        Dim blnResult As Boolean = False
        Try
            sOrderid = GetPendingOrder()

            If sOrderid.Length > 0 Then
                _sCurrentProcess = "Canceling order"
                bgWorker.ReportProgress(30)
                EI.CancelOrder(sOrderid)
                _sCurrentProcess = "Order cancelled"
                bgWorker.ReportProgress(35)
                blnResult = True
            End If
            If blnResult Then
                ''Delete the order.
                DeletePendingOrder(sOrderid)
            End If
        Catch ex As COMException
            sErrorString = mdlEcgProcessLayer.GetErrorString(ex.ErrorCode)
            gloAuditTrail.gloAuditTrail.ExceptionLog(sErrorString, False)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            sOrderid = String.Empty
        End Try
        Return blnResult
    End Function
    ''' <summary>
    ''' Method to retrive and save latest test data.
    ''' </summary>
    ''' <param name="nTestId"></param>
    ''' <param name="sOrderID"></param>
    ''' <param name="sError"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetLatestTestData(ByVal nTestId As Int32, ByVal sOrderID As String, ByRef sError As String) As Boolean
        Dim objTestData As clsTestData
        Dim blnResult As Boolean = False
        Try
            objTestData = New clsTestData()
            'Retrive test data from sphinix.
            _sCurrentProcess = "Retriveing changed information."
            bgWorker.ReportProgress(35)

            objTestData = GetTestData(nTestId, sOrderID, sErrorString)
            bgWorker.ReportProgress(55)
            If IsNothing(objTestData) Then
                Return False
            Else
                _sCurrentProcess = "Retrived test information."
                bgWorker.ReportProgress(75)
                SaveTestData(objTestData)
                bgWorker.ReportProgress(80)
                blnResult = True
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            objTestData = Nothing
        End Try

        Return blnResult
    End Function

    ''' <summary>
    ''' Method to get the status.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetOrderStatus() As Boolean
        Dim dtPendingOrder As New DataTable
        Dim objTestData As clsTestData
        Dim blnResult As Boolean = False
        Try
            dtPendingOrder = GetPendingOrders()

            If IsNothing(dtPendingOrder) Then
                Return False
            End If

            bgWorker.ReportProgress(40)
            ''Get Status for each orders.
            For i As Integer = 0 To dtPendingOrder.Rows.Count - 1

                Dim sOrderId As String = String.Empty
                Dim nTestId As Integer = 0

                Long.TryParse(dtPendingOrder.Rows(i)("nECGID").ToString(), ECGID)

                sOrderId = dtPendingOrder.Rows(i)("orderId").ToString()

                If sOrderId <> Nothing AndAlso sOrderId.Length > 0 AndAlso sOrderId <> String.Empty Then

                    bgWorker.ReportProgress(45)

                    ''Retrive testid from test id from sphinix interface.
                    _sCurrentProcess = "Retriveing test information from device"

                    nTestId = RetriveTestIdFromSphinix(sOrderId)
                    bgWorker.ReportProgress(70)

                    If nTestId > 0 Then
                        objTestData = New clsTestData()
                        'Retrive test data from sphinix.
                        objTestData = GetTestData(nTestId, sOrderId)
                        If IsNothing(objTestData) Then
                            blnResult = True
                            Continue For
                        Else
                            SaveTestData(objTestData)
                            blnResult = True
                        End If
                    End If

                    bgWorker.ReportProgress(90)
                End If
                sOrderId = String.Empty
                nTestId = 0
            Next

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If Not IsNothing(dtPendingOrder) Then
                dtPendingOrder.Dispose()
            End If
        End Try
        Return blnResult
    End Function
    ''' <summary>
    ''' Method to get pending orders.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetPendingOrders() As DataTable
        Dim dtTable As New DataTable
        Dim objDblayer As New gloDatabaseLayer.DBLayer(_sConnectionString)
        Dim sQuery As String = String.Empty

        Try
            sQuery = "select ISNULL(nECGID,0) as nECGID,  ISNULL(sOrderId,'') as orderId from CV_ElectroCardioGrams Where sExternalCode='pending' AND nPatientId=" & _nPatientId

            objDblayer.Connect(False)
            objDblayer.Retrive_Query(sQuery, dtTable)

        Catch ex As Exception
            dtTable = Nothing
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If Not IsNothing(objDblayer) Then
                objDblayer.Disconnect()
                objDblayer.Dispose()
            End If
            sQuery = String.Empty
        End Try
        Return dtTable
    End Function

    ''' <summary>
    ''' Method to place new order in sphinix interface.
    ''' </summary>
    ''' <param name="sErrorString"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AddNewOrder(ByRef sErrorString As String) As String
        Dim _sOrderId As String = String.Empty
        Dim oOrder As New ComOrder
        Dim objgloPatient As New gloPatient.gloPatient(mdlEcgProcessLayer.sConnectionString)
        Dim objPatient As New gloPatient.Patient()
        Dim sRace() As String
        Try
            _sCurrentProcess = "Creating order"
            bgWorker.ReportProgress(40)

            objPatient = objgloPatient.GetPatient(_nPatientId)

            oOrder.PatientMRN = objPatient.DemographicsDetail.PatientCode
            oOrder.PatientFirstName = objPatient.DemographicsDetail.PatientFirstName
            oOrder.PatientLastName = objPatient.DemographicsDetail.PatientLastName
            oOrder.DOB = objPatient.DemographicsDetail.PatientDOB
            oOrder.Sex = objPatient.DemographicsDetail.PatientGender
            sRace = Convert.ToString(objPatient.DemographicsDetail.PatientRace).Split("|")
            If Not sRace Is Nothing AndAlso sRace.Length > 0 AndAlso Not String.IsNullOrEmpty(sRace(0)) Then
                oOrder.Race = sRace(0)
            Else
                oOrder.Race = ""
            End If
            oOrder.TestPlannedDateTime = DateTime.Now()
            oOrder.TestType = ComECGTestType.ECG
            oOrder.Priority = ComECGTestPriority.Asap
            oOrder.AttendingPhysicianName = ""
            oOrder.InstitutionID = mdlEcgProcessLayer.sECGInstitutionId

            _sCurrentProcess = "Placing order"
            bgWorker.ReportProgress(45)
            EI.SaveOrder(oOrder)

            _sOrderId = oOrder.ID
            _sCurrentProcess = "Order placed successful"
            bgWorker.ReportProgress(50)

        Catch ex As COMException
            _sOrderId = String.Empty
            sErrorString = mdlEcgProcessLayer.GetErrorString(ex.ErrorCode)
        Catch ex As Exception
            _sOrderId = String.Empty
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally

            If Not IsNothing(objgloPatient) Then
                objgloPatient.Dispose()
            End If
            If Not IsNothing(objPatient) Then
                objPatient.Dispose()
            End If
            oOrder = Nothing
            sRace = Nothing
        End Try

        Return _sOrderId
    End Function
    ''' <summary>
    ''' Method to save order
    ''' </summary>
    ''' <param name="sOrderID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SaveOrder(ByVal sOrderID As String) As Long
        Dim nEcgID As Long = 0
        Dim oDBLayer As New gloDatabaseLayer.DBLayer(mdlEcgProcessLayer.sConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Dim _Result As Object = Nothing
        Try
            oDBLayer.Connect(False)
            oDBParameters.Clear()
            oDBParameters.Add("@nECGID", nEcgID, ParameterDirection.InputOutput, SqlDbType.BigInt)
            oDBParameters.Add("@nPatientID", _nPatientId, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@nExamID", 0, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@nVisitID", _nVisitId, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@nClinicID", 1, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@dtGivenDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss tt"), ParameterDirection.Input, SqlDbType.DateTime)
            oDBParameters.Add("@nGroupID", 0, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@sTestType", "ECG", ParameterDirection.Input, SqlDbType.VarChar, 500)
            oDBParameters.Add("@dtOrderDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss tt"), ParameterDirection.Input, SqlDbType.DateTime)
            oDBParameters.Add("@dtReviewDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss tt"), ParameterDirection.Input, SqlDbType.DateTime)
            oDBParameters.Add("@AddDt", 1, ParameterDirection.Input, SqlDbType.Bit)
            oDBParameters.Add("@MachineID", _nMachineID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@sOrderId", sOrderID, ParameterDirection.Input, SqlDbType.VarChar, 500)
            oDBParameters.Add("@sExternalCode", OrderStatus.Pending.ToString(), ParameterDirection.Input, SqlDbType.VarChar, 500)
            oDBParameters.Add("@sDeviceType", "Cardio", ParameterDirection.Input, SqlDbType.VarChar, 500)
            oDBLayer.Execute("CV_InUpElectroCardioGram", oDBParameters, _Result)
            Long.TryParse(Convert.ToString(_Result), nEcgID)
        Catch ex As Exception
            nEcgID = 0
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If Not IsNothing(oDBLayer) Then
                oDBLayer.Disconnect()
                oDBLayer.Dispose()
            End If
            If Not IsNothing(oDBParameters) Then
                oDBParameters.Dispose()
            End If
        End Try
        SaveOrder = nEcgID
    End Function
    ''' <summary>
    ''' Method to retrive testid from sphinix interface.
    ''' </summary>
    ''' <param name="sOrderID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RetriveTestIdFromSphinix(ByVal sOrderID As String) As Integer

        Dim nTestId As Integer = 0
        Dim objResult As Object = Nothing
        Try
            nTestId = EI.GetTestForOrder(sOrderID, objResult)

        Catch ex As COMException
            gloAuditTrail.gloAuditTrail.ExceptionLog(mdlEcgProcessLayer.GetErrorString(ex.ErrorCode), False)
        Catch ex As Exception
            nTestId = 0
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
        Return nTestId
    End Function

    ''' <summary>
    ''' Mehod to get test data.
    ''' </summary>
    ''' <param name="nTestID"></param>
    ''' <param name="sOrderId"></param>
    ''' <param name="sErrorString"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetTestData(ByVal nTestID As Integer, ByVal sOrderId As String, Optional ByVal sErrorString As String = "") As clsTestData
        Dim objComtestData As ComTestData = Nothing
        Dim objTestData As New clsTestData()

        ''Get Test data from Sphinix Interface
        Try
            objComtestData = EI.GetTestData(nTestID)
        Catch ex As COMException
            sErrorString = mdlEcgProcessLayer.GetErrorString(ex.ErrorCode)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return Nothing
        End Try

        Try
            ''Fetch data from sphinix interface
            If IsNothing(objComtestData) Then
                Return Nothing
            End If

            objTestData.nPatientId = _nPatientId
            objTestData.sOrderId = sOrderId
            objTestData.sTestId = nTestID.ToString()
            objTestData.sPAxis = objComtestData.GetData("ecg_p_frontaxis")
            objTestData.sPr = objComtestData.GetData("ecg_pr_interval")
            objTestData.sQt = objComtestData.GetData("ecg_qt_interval")
            objTestData.sQTc = objComtestData.GetData("ecg_correct_qt_interval")
            objTestData.sTAxis = objComtestData.GetData("ecg_t_frontaxis")
            objTestData.sQRSAxis = objComtestData.GetData("ecg_qrs_frontaxis")
            objTestData.sECGInterpretation = objComtestData.GetData("interp_interpretation").Replace("'", "''")
            objTestData.sTestStatus = objComtestData.GetData("Status")
            'Added for orsDuration 'ecg_qrs_duration
            objTestData.sQRSDuration = objComtestData.GetData("ecg_qrs_duration").Replace("'", "''")

        Catch ex As Exception
            Return Nothing
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            objComtestData = Nothing
        End Try
        Return objTestData
    End Function
    ''' <summary>
    ''' Method to save test information to database.
    ''' </summary>
    ''' <param name="objComTestData"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SaveTestData(ByVal objComTestData As clsTestData) As Boolean
        Dim oDBLayer As New gloDatabaseLayer.DBLayer(_sConnectionString)
        Dim sQuery As String = String.Empty
        Try

            objComTestData.sECGInterpretation = objComTestData.sECGInterpretation.Replace("\n", "")
            ''objComTestData.sECGInterpretation = objComTestData.sECGInterpretation.Replace("'", "''")

            Dim sInterpretinon As String = String.Empty
            If objComTestData.sECGInterpretation.Length > 3990 Then
                sInterpretinon = objComTestData.sECGInterpretation.Substring(0, 3990).ToString()
            Else
                sInterpretinon = objComTestData.sECGInterpretation
            End If

            sQuery = "UPDATE [CV_ElectroCardioGrams] " & _
                    " SET 		sTestId= '" & objComTestData.sTestId & "'" & _
                    " ,[sPR] = '" & objComTestData.sPr & "' " & _
                    " ,[sQT] = '" & objComTestData.sQt & "'" & _
                    " ,[sQTc] = '" & objComTestData.sQTc & "'" & _
                    " ,[sPAxis] = '" & objComTestData.sPAxis & "'" & _
                    " ,[sQRSAxis] = '" & objComTestData.sQRSDuration & "'" & _
                    " ,[sTAxis] = '" & objComTestData.sTAxis & "'" & _
                    " ,[sECGInterpretation] = '" & sInterpretinon & "'" & _
                    ",[sORSDuration]= '" & objComTestData.sQRSDuration & "'" & _
                    " ,[sExternalCode] = '" & objComTestData.sTestStatus & "'" & _
                    ",[dtReviewDate]='" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss tt") & "'" & _
                    "  WHERE sOrderId ='" & objComTestData.sOrderId & "' And nPatientID = " & objComTestData.nPatientId
            oDBLayer.Connect(False)
            oDBLayer.Execute_Query(sQuery)
            sInterpretinon = String.Empty

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If Not IsNothing(oDBLayer) Then
                oDBLayer.Disconnect()
                oDBLayer.Dispose()
            End If
            sQuery = String.Empty
        End Try
        Return Nothing
    End Function
    ''' <summary>
    ''' Method to retrive peding order.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetPendingOrder() As String
        Dim objDBlayer As New gloDatabaseLayer.DBLayer(_sConnectionString)
        Dim sQuery As String = String.Empty
        Dim objResult As Object = Nothing
        Dim sVaule As String = String.Empty

        Try
            sQuery = " select isnull(sOrderId,'') as orderid from CV_ElectroCardioGrams WHERE sExternalCode='Pending' AND nPatientID=" & _nPatientId
            objDBlayer.Connect(False)
            objResult = objDBlayer.ExecuteScalar_Query(sQuery)
            objDBlayer.Disconnect()

            If Not IsNothing(objResult) AndAlso objResult.ToString() <> "" Then
                sVaule = Convert.ToString(objResult)
            End If

        Catch ex As Exception
            sVaule = String.Empty
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally

            If Not IsNothing(objDBlayer) Then
                objDBlayer.Dispose()
            End If
            objResult = Nothing
            sQuery = String.Empty
        End Try
        Return sVaule
    End Function
    ''' <summary>
    ''' Method to delete pending order.
    ''' </summary>
    ''' <param name="sOrderId"></param>
    ''' <remarks></remarks>
    Private Sub DeletePendingOrder(ByVal sOrderId As String)
        Dim objDBlayer As New gloDatabaseLayer.DBLayer(_sConnectionString)
        Dim sQuery As String = String.Empty
      
        Try
            sQuery = " DELETE FROM  CV_ElectroCardioGrams WHERE sExternalCode='Pending' AND sOrderId='" & sOrderId & "' AND nPatientID=" & _nPatientId
            objDBlayer.Connect(False)
            objDBlayer.Execute_Query(sQuery)
            objDBlayer.Disconnect()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally

            If Not IsNothing(objDBlayer) Then
                objDBlayer.Dispose()
            End If
            sQuery = String.Empty
        End Try
    End Sub
End Class

Public Class clsTestData
#Region "Constructor & destructor"

    Private disposed As Boolean = False
    Public Sub Dispose()
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposed Then
            If disposing Then
            End If
        End If
        disposed = True
    End Sub

    Protected Overrides Sub Finalize()
        Try
            Dispose(False)
        Finally
            MyBase.Finalize()
        End Try
    End Sub
#End Region

#Region "Private Variables"

    Private _sOrderId As String = String.Empty
    Private _sTestId As String = String.Empty
    Private _sPr As String = String.Empty
    Private _sQt As String = String.Empty
    Private _sQTc As String = String.Empty
    Private _sQRSDuration As String = String.Empty
    Private _sQRSAxis As String = String.Empty
    Private _sPAxis As String = String.Empty
    Private _sQAxis As String = String.Empty
    Private _sTAxis As String = String.Empty
    Private _sECGInterpretation As String = String.Empty
    Private _nPatientId As Long = 0
    Private _sTestStatus As String = String.Empty
#End Region

#Region "Properties"
   
    Public Property sTestStatus() As String
        Get
            Return _sTestStatus
        End Get
        Set(ByVal value As String)
            _sTestStatus = value
        End Set
    End Property

    Public Property sQRSAxis() As String
        Get
            Return _sQRSAxis
        End Get
        Set(ByVal value As String)
            _sQRSAxis = value
        End Set
    End Property

    Public Property nPatientId() As Long
        Get
            Return _nPatientId
        End Get
        Set(ByVal value As Long)
            _nPatientId = value
        End Set
    End Property

    Public Property sTestId() As String
        Get
            Return _sTestId
        End Get
        Set(ByVal value As String)
            _sTestId = value
        End Set
    End Property

    Public Property sOrderId() As String
        Get
            Return _sOrderId
        End Get
        Set(ByVal value As String)
            _sOrderId = value
        End Set
    End Property
    Public Property sECGInterpretation() As String
        Get
            Return _sECGInterpretation
        End Get
        Set(ByVal value As String)
            _sECGInterpretation = value
        End Set
    End Property

    Public Property sTAxis() As String
        Get
            Return _sTAxis
        End Get
        Set(ByVal value As String)
            _sTAxis = value
        End Set
    End Property

    Public Property sQAxis() As String
        Get
            Return _sQAxis
        End Get
        Set(ByVal value As String)
            _sQAxis = value
        End Set
    End Property

    Public Property sPAxis() As String
        Get
            Return _sPAxis
        End Get
        Set(ByVal value As String)
            _sPAxis = value
        End Set
    End Property

    Public Property sQRSDuration() As String
        Get
            Return _sQRSDuration
        End Get
        Set(ByVal value As String)
            _sQRSDuration = value
        End Set
    End Property

    Public Property sQTc() As String
        Get
            Return _sQTc
        End Get
        Set(ByVal value As String)
            _sQTc = value
        End Set
    End Property

    Public Property sQt() As String
        Get
            Return _sQt
        End Get
        Set(ByVal value As String)
            _sQt = value
        End Set
    End Property

    Public Property sPr() As String
        Get
            Return _sPr
        End Get
        Set(ByVal value As String)
            _sPr = value
        End Set
    End Property
#End Region
End Class
