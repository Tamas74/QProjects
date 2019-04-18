Imports System.Data.SqlClient
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports gloEmdeonInterface.Classes ' added by manoj jadhav on 20111102 for welchAllyn Device integration
Public Class clsCVElectroCardioGrams

    Public Function SaveElectroCardioGrams(ByVal ECGID As Long, ByVal PatientID As Int64, ByVal ExamID As Int64, ByVal VisitID As Int64, ByVal ClinicID As Int64, ByVal OrderDate As Date, ByVal ReviewDate As Date, ByVal GivenDate As DateTime, ByVal GroupID As Long, Optional ByVal CPTCode As String = "", Optional ByVal TestType As String = "", Optional ByVal ECGPerform As String = "", Optional ByVal PR As String = "", Optional ByVal QT As String = "", Optional ByVal QTc As String = "", Optional ByVal ORSDuration As String = "", Optional ByVal PAxis As String = "", Optional ByVal QRSAxis As String = "", Optional ByVal TAxis As String = "", Optional ByVal ECGInterpretation As String = "", Optional ByVal OrderInPhysician As String = "", Optional ByVal ReviewInPhysician As String = "", Optional ByVal ModEntry As Boolean = False) As Long

        Dim Con As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing
        Dim EleCardGraphId As Long = 0

        'Dim trCatheterization As SqlTransaction
        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If
        'trCatheterization = Con.BeginTransaction()
        ''Dim objCatheterization As Cls_CardioVascular
        Try

            '' '' ''For i As Int16 = 0 To ArrList.Count - 1

            ''objCatheterization = New Cls_CardioVascular
            ''objCatheterization = CType(ArrList(0), Cls_CardioVascular)
            Dim CathParam As SqlParameter

            If ModEntry = True Then



                '''' if exist then delete that Catheterization
                cmd = New SqlCommand("CV_DeleteElectroCardioGram", Con)
                cmd.CommandType = CommandType.StoredProcedure
                'cmd.Transaction = trCatheterization

                CathParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
                CathParam.Direction = ParameterDirection.Input
                CathParam.Value = PatientID


                CathParam = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
                CathParam.Direction = ParameterDirection.Input
                CathParam.Value = VisitID

                CathParam = cmd.Parameters.Add("@dtGivenDate", SqlDbType.DateTime)
                CathParam.Direction = ParameterDirection.Input
                CathParam.Value = Convert.ToDateTime(GivenDate.Date)


                If Con.State = ConnectionState.Closed Then
                    Con.Open()
                End If

                If cmd.ExecuteNonQuery() > 0 Then
                    'Dim objAudit As New clsAudit
                    ''objAudit.CreateLog(clsAudit.enmActivityType.Delete, "'Problem List Deleted for date " & VisitDate & "'", gstrLoginName, gstrClientMachineName, gnPatientID)
                    'objAudit = Nothing
                End If
                If cmd IsNot Nothing Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If

            End If
            ECGID = 0 'added by manoj jadhav on 20111102 for welchAllyn Device integration
            '' Insert Or Update problem List
            cmd = New SqlCommand("CV_InUpElectroCardioGram", Con)
            cmd.CommandType = CommandType.StoredProcedure
            'cmd.Transaction = trCatheterization


            CathParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            CathParam.Direction = ParameterDirection.Input
            CathParam.Value = PatientID
            'oDB.DBParametersCol.Add(CathParam)

            CathParam = cmd.Parameters.Add("@nExamID", SqlDbType.BigInt)
            CathParam.Direction = ParameterDirection.Input
            CathParam.Value = ExamID
            'oDB.DBParametersCol.Add(CathParam)

            CathParam = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
            CathParam.Direction = ParameterDirection.Input
            CathParam.Value = VisitID
            'oDB.DBParametersCol.Add(CathParam)

            CathParam = cmd.Parameters.Add("@nClinicID", SqlDbType.BigInt)
            CathParam.Direction = ParameterDirection.Input
            CathParam.Value = ClinicID

            CathParam = cmd.Parameters.Add("@dtGivenDate", SqlDbType.DateTime)
            CathParam.Direction = ParameterDirection.Input
            CathParam.Value = Convert.ToDateTime(GivenDate)
            ''Now.Date
            ''Convert.ToDateTime(Proceduredt)
            CathParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
            CathParam.Direction = ParameterDirection.Input
            CathParam.Value = GetPrefixTransactionID()

            CathParam = cmd.Parameters.AddWithValue("@nECGID", ECGID)
            CathParam.Direction = ParameterDirection.InputOutput
            CathParam.Value = ECGID


            CathParam = cmd.Parameters.AddWithValue("@nGroupID", ECGID)
            CathParam.Direction = ParameterDirection.Input
            CathParam.Value = GroupID

            ''''''''''''''''''''''''''''''
            CathParam = cmd.Parameters.Add("@sCPTCode", SqlDbType.VarChar, 500)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(CPTCode) Then
                CathParam.Value = CPTCode
            End If

            CathParam = cmd.Parameters.Add("@sTestType", SqlDbType.VarChar, 500)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(TestType) Then
                CathParam.Value = TestType
            End If

            CathParam = cmd.Parameters.Add("@sOrderInPhysician", SqlDbType.VarChar, 2000)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(OrderInPhysician) Then
                CathParam.Value = OrderInPhysician
            End If

            CathParam = cmd.Parameters.Add("@sReviewInPhysician", SqlDbType.VarChar, 2000)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(ReviewInPhysician) Then
                CathParam.Value = ReviewInPhysician
            End If



            CathParam = cmd.Parameters.Add("@sECGPerform", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(ECGPerform) Then
                CathParam.Value = ECGPerform
            End If

            CathParam = cmd.Parameters.Add("@dtOrderDate", SqlDbType.DateTime)
            CathParam.Direction = ParameterDirection.Input
            CathParam.Value = Convert.ToDateTime(OrderDate)

            CathParam = cmd.Parameters.Add("@sPR", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(PR) Then
                CathParam.Value = PR
            End If

            CathParam = cmd.Parameters.Add("@sQT", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(QT) Then
                CathParam.Value = QT
            End If

            CathParam = cmd.Parameters.Add("@sQTc", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(QTc) Then
                CathParam.Value = QTc
            End If

            CathParam = cmd.Parameters.Add("@sORSDuration", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(ORSDuration) Then
                CathParam.Value = ORSDuration
            End If

            CathParam = cmd.Parameters.Add("@sPAxis", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(PAxis) Then
                CathParam.Value = PAxis
            End If

            CathParam = cmd.Parameters.Add("@sQRSAxis", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(QRSAxis) Then
                CathParam.Value = QRSAxis
            End If



            CathParam = cmd.Parameters.Add("@sTAxis", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(TAxis) Then
                CathParam.Value = TAxis
            End If

            CathParam = cmd.Parameters.Add("@sECGInterpretation", SqlDbType.VarChar, 500)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(ECGInterpretation) Then
                CathParam.Value = ECGInterpretation
            End If

            CathParam = cmd.Parameters.Add("@dtReviewDate", SqlDbType.DateTime)
            CathParam.Direction = ParameterDirection.Input
            CathParam.Value = Convert.ToDateTime(ReviewDate)

            'Parameter added by manoj jadhav on 20111102 for welchAllyn Device integration
            CathParam = cmd.Parameters.Add("@sDeviceType", SqlDbType.VarChar)
            CathParam.Direction = ParameterDirection.Input
            CathParam.Value = "Local"


            ''''''''''''''''''''''''''''''

            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If

            If cmd.ExecuteNonQuery() > 0 Then
                EleCardGraphId = cmd.Parameters("@nECGID").Value
                ''nCatheterizationID = CathId
            End If

            ''If nCatheterizationID = 0 Then
            ''    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Add, "Catheterization Added", gloAuditTrail.ActivityOutCome.Success)
            ''Else
            ''    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Modify, "Catheterization could not be Modified", gloAuditTrail.ActivityOutCome.Success)
            ''End If

            ' '' ''Next
            ''trCatheterization.Commit()
            CathParam = Nothing
            Return EleCardGraphId
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ''If CatheterizationID = 0 Then
            ''    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Add, "Catheterization could not be Added", gloAuditTrail.ActivityOutCome.Failure)
            ''Else
            ''    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Catheterization could not be Modified", gloAuditTrail.ActivityOutCome.Failure)
            ''End If
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ''trCatheterization.Rollback()
            Return False
        Catch ex As Exception
            ''If nCatheterizationID = 0 Then
            ''    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, "Catheterization could not be Added", gloAuditTrail.ActivityOutCome.Failure)
            ''Else
            ''    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Catheterization could not be Modified", gloAuditTrail.ActivityOutCome.Failure)
            ''End If
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ''trCatheterization.Rollback()
            Return False
        Finally

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(Con) Then
                If Con.State = ConnectionState.Open Then
                    Con.Close()
                End If
                Con.Dispose()
                Con = Nothing
            End If

        End Try
    End Function

    Public Function DeleteElectroCardioGrams(ByVal mPatientID As Int64, ByVal mVisitId As Int64, ByVal mdtproceduredate As Date)
        Dim Con As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing

        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If

        Dim CathParam As SqlParameter
        Try

            '''' if exist then delete that Catheterization
            cmd = New SqlCommand("CV_DeleteElectroCardioGram", Con)
            cmd.CommandType = CommandType.StoredProcedure


            CathParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            CathParam.Direction = ParameterDirection.Input
            CathParam.Value = mPatientID


            CathParam = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
            CathParam.Direction = ParameterDirection.Input
            CathParam.Value = mVisitId

            CathParam = cmd.Parameters.Add("@dtGivenDate", SqlDbType.DateTime)
            CathParam.Direction = ParameterDirection.Input
            CathParam.Value = Convert.ToDateTime(mdtproceduredate)


            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If

            If cmd.ExecuteNonQuery() > 0 Then

            End If
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Delete, "CV CVCatheterization deleted.", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 201000915
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Delete, "CV CVElectrocardioGrams deleted.", mPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            ''
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Delete, "CV CVElectrocardioGrams could not be deleted.", gloAuditTrail.ActivityOutCome.Failure)
            ''Added Rahul P on 201000915
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Delete, "CV CVElectrocardioGrams could not be deleted.", mPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            ''
        Finally

            CathParam = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(Con) Then
                If Con.State = ConnectionState.Open Then
                    Con.Close()
                End If
                Con.Dispose()
                Con = Nothing
            End If

        End Try

        Return True
    End Function

    ''Added by madan on 20100108 for device orders,
    ''' <summary>
    ''' Method to check the pending device orders, if the orders are pending then user need to cancel the order or retrive existing order status.
    ''' </summary>
    ''' <param name="nPatientId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckPendingDeviceOrders(ByVal nPatientId As Long) As Boolean
        Dim objDbLayer As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim sQuery As String = String.Empty
        Dim objResult As Object = Nothing
        Dim blnResult As Boolean = False
        Try
            sQuery = " select count(*) from CV_ElectroCardioGrams WHERE (sOrderId<>'' OR sOrderId<>null) AND sExternalCode='Pending' AND nPatientID=" & nPatientId
            objDbLayer.Connect(False)
            objResult = objDbLayer.ExecuteScalar_Query(sQuery)
            objDbLayer.Disconnect()

            If Not IsNothing(objResult) AndAlso objResult.ToString() <> "" Then
                If Convert.ToInt16(objResult) > 0 Then
                    blnResult = True
                End If
            End If

        Catch ex As Exception
            blnResult = True
        Finally
            If Not IsNothing(objDbLayer) Then
                objDbLayer.Dispose()
            End If
            objResult = Nothing
        End Try
        Return blnResult
    End Function


    ''Load Forms From Exam:
    ''Added by madan on 20112201-- For deviceorder implimentaion.
    ''' <summary>
    ''' Method is added for opening ecg orders based on 
    ''' </summary>
    ''' <param name="nPatientId"></param>
    ''' <param name="nVisitId"></param>
    ''' <param name="dtVisitDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadElectroCardioGrams(ByVal nPatientId As Int64, ByVal nVisitId As Int64, ByVal dtVisitDate As DateTime) As Boolean

        Dim _dtECGOrders As DataTable = Nothing
        Dim OrderType As String = String.Empty
        Dim sOrderId As String = String.Empty
        Dim sTestID As String = String.Empty
        Dim nECGID As Long = 0
        Dim nClinicId As Long = 0
        Try
            _dtECGOrders = GetLatestECGOrder(nPatientId, nVisitId)
            If _dtECGOrders Is Nothing OrElse _dtECGOrders.Rows.Count <= 0 Then

                LoadElectroCardioGrams = NewTest(nPatientId, nVisitId, dtVisitDate)
            Else
                sOrderId = Convert.ToString(_dtECGOrders.Rows(0)("sOrderId"))
                sTestID = Convert.ToString(_dtECGOrders.Rows(0)("sTestId"))
                Long.TryParse(Convert.ToString(_dtECGOrders.Rows(0)("nECGID")), nECGID)
                Long.TryParse(Convert.ToString(_dtECGOrders.Rows(0)("nClinicID")), nClinicId)
                OrderType = Convert.ToString(_dtECGOrders.Rows(0)("sDeviceType"))

                If (OrderType.Trim().Length <= 0) Then

                    If sTestID.Trim().Length <= 0 AndAlso sOrderId.Trim().Length <= 0 Then
                        OrderType = "Local"
                    ElseIf sTestID.Trim().ToUpper() = sOrderId.Trim().ToUpper() Then
                        OrderType = "WelchAllyn"
                    Else
                        OrderType = "Cardio"
                    End If

                End If

                LoadElectroCardioGrams = ModifyTest(nECGID, nPatientId, dtVisitDate, nVisitId, nClinicId, OrderType, sOrderId, sTestID)

            End If
        Catch ex As Exception
            LoadElectroCardioGrams = False
            ex = Nothing
        Finally
            If Not IsNothing(_dtECGOrders) Then
                _dtECGOrders.Dispose()
            End If
        End Try
        'end of code added by manoj jadhav on 20111102 for welchAllyn Device integration
    End Function

    'start of New function added by manoj jadhav on 20111102 for welchAllyn Device integration
    Private Function NewTest(ByVal nPatientId As Int64, ByVal nVisitId As Int64, ByVal dtVisitDate As DateTime) As Boolean
        Dim oFrmEmdeonOptionScreen As New gloEmdeonCommon.frmECGOrderOptions()
        Try
            oFrmEmdeonOptionScreen.ShowDialog(oFrmEmdeonOptionScreen.Parent)
            If oFrmEmdeonOptionScreen.TestToConduct = gloEmdeonCommon.frmECGOrderOptions.TestType.LocalTest Then

                Dim frmLocalOrder As frmCV_ElectroCardiograms = Nothing
                Try
                    frmLocalOrder = New frmCV_ElectroCardiograms(nPatientId, dtVisitDate.Date, nVisitId)
                    frmLocalOrder.ShowInTaskbar = False
                    frmLocalOrder.blnIsNew = True
                    frmLocalOrder.StartPosition = FormStartPosition.CenterScreen
                    frmLocalOrder.BringToFront()
                    frmLocalOrder.ShowDialog(frmLocalOrder.Parent)
                    NewTest = True
                Catch ex As Exception
                    ex = Nothing
                    NewTest = False
                Finally
                    If Not frmLocalOrder Is Nothing Then
                        frmLocalOrder.Dispose()
                        frmLocalOrder = Nothing
                    End If
                End Try

            ElseIf oFrmEmdeonOptionScreen.TestToConduct = gloEmdeonCommon.frmECGOrderOptions.TestType.DeviceTest Then

                Dim obj As frmCV_VWElectroCardiograms = Nothing
                Try
                    obj = New frmCV_VWElectroCardiograms(nPatientId)

                    If obj.Device_Availabel() Then
                        NewTest = obj.NewDeviceTest()
                    Else
                        MessageBox.Show("No Device Interface found, Please configure interface  in gloEMR Admin>> Settings>> Interface Settings", mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        NewTest = False
                    End If


                Catch ex As Exception
                    ex = Nothing
                    NewTest = False
                Finally
                    If Not obj Is Nothing Then
                        obj.Dispose()
                        obj = Nothing
                    End If
                End Try
            Else
                NewTest = False
            End If
        Catch ex As Exception
            ex = Nothing
            NewTest = Nothing
        Finally
            If Not oFrmEmdeonOptionScreen Is Nothing Then
                oFrmEmdeonOptionScreen.Dispose()
                oFrmEmdeonOptionScreen = Nothing
            End If
        End Try
    End Function
    'end of New function added by manoj jadhav on 20111102 for welchAllyn Device integration

    'start of New function added by manoj jadhav on 20111102 for welchAllyn Device integration
    Private Function ModifyTest(ByVal _nECGID As Long, ByVal nPatientId As Long, ByVal dtVisitDate As DateTime, ByVal nVisitId As Long, ByVal nClinicId As Long, ByVal OrderType As String, ByVal sOrderId As String, ByVal sTestID As String) As Boolean
        Dim ObjfrmCV_VWElectroCardiograms As frmCV_VWElectroCardiograms = Nothing
        Try
            ObjfrmCV_VWElectroCardiograms = New frmCV_VWElectroCardiograms(nPatientId)
            ModifyTest = ObjfrmCV_VWElectroCardiograms.UpdateTest(_nECGID, nPatientId, nVisitId, dtVisitDate, nClinicId, sTestID, sOrderId, OrderType)
        Catch ex As Exception
            ex = Nothing
            ModifyTest = False
        Finally
            If Not ObjfrmCV_VWElectroCardiograms Is Nothing Then
                ObjfrmCV_VWElectroCardiograms.Dispose()
                ObjfrmCV_VWElectroCardiograms = Nothing
            End If
        End Try
    End Function
    'end of New function added by manoj jadhav on 20111102 for welchAllyn Device integration

    ''' <summary>
    ''' Method to get latest order based on visitid.
    ''' </summary>
    ''' <param name="nECGPatientID"></param>
    ''' <param name="nVisitId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetLatestECGOrder(ByVal nECGPatientID As Int64, ByVal nVisitId As Int64) As DataTable
        Dim _dtEcgOrders As DataTable = Nothing
        Dim _objDbLayer As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim _sQuery As String = String.Empty

        Try
            '_sQuery = "SELECT sOrderID,sTestId from CV_ElectroCardioGrams where nPatientID=" & nECGPatientID & " and nVisitID=" & nVisitId & "  order by dtGivenDate desc "   'commented by manoj jadhav on 20111102 for welchAllyn Device integration
            _sQuery = "SELECT TOP 1 nECGID, sOrderID,sTestId,nClinicID,sDeviceType from CV_ElectroCardioGrams where nPatientID=" & nECGPatientID & " and nVisitID=" & nVisitId & "  order by dtGivenDate desc " 'Added by manoj jadhav on 20111102 for welchAllyn Device integration
            _objDbLayer.Connect(False)
            _objDbLayer.Retrive_Query(_sQuery, _dtEcgOrders)

        Catch ex As Exception
            _dtEcgOrders = Nothing
        Finally
            If Not IsNothing(_objDbLayer) Then
                _objDbLayer.Disconnect()
                _objDbLayer.Dispose()
            End If
            _sQuery = String.Empty
        End Try
        Return _dtEcgOrders
    End Function

    ''' <summary>
    ''' Event is raised from ECGorder options form for viewing local ECG order.
    ''' </summary>
    ''' <param name="nPatientId"></param>
    ''' <param name="nVisitId"></param>
    ''' <param name="dtVisitDate"></param>
    ''' <remarks></remarks>
    Private Sub loadLocaECGOrder(ByVal nPatientId As Int64, ByVal nVisitId As Int64, ByVal dtVisitDate As Date)
        Dim frmLocalOrder As New frmCV_ElectroCardiograms(nPatientId, dtVisitDate.Date, nVisitId)
        frmLocalOrder.blnIsNew = True
        frmLocalOrder.StartPosition = FormStartPosition.CenterScreen
        frmLocalOrder.ShowDialog(frmLocalOrder.Parent)
        frmLocalOrder.Dispose()
    End Sub

End Class
