Imports gloDBInterface
Imports gloHl7Interface
Imports System.Data.SqlClient
Imports gloDatabaseLayer  'Added by kanchan on 20100313
'Generalised class to send charges info.
Public Class clsGeneralInterface
    Implements IDisposable
    Private oVisit As gloDBInterface.Visit
    Private dtDOS As DateTime
    Private intTotalRecords As Int32
    Public Event InvalidFilePath()
    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                oVisit = Nothing
                ' TODO: free managed resources when explicitly called
            End If

            ' TODO: free shared unmanaged resources
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
    Public Property TotalRecords() As Int32
        Get
            Return intTotalRecords
        End Get
        Set(ByVal value As Int32)
            intTotalRecords = value
        End Set
    End Property
    Public Property DOS() As DateTime
        Get
            Return dtDOS
        End Get
        Set(ByVal value As DateTime)
            dtDOS = value
        End Set
    End Property
    Public Sub SendCharges(ByVal examid As Int64, ByVal PatientID As Int64)
        'check setting if true ,generate HL7 file
        '  If gblnHL7SENDOUTBOUNDGLOEMR = True AndAlso gblnSaveandClose = True Then
        'Code Commented by supriya
        'Dim oglohl7 As New gloHl7Interface.HL7Library.gloHL7
        'Try
        '    oglohl7.DatabaseName = gstrDatabaseName
        '    oglohl7.ServerName = gstrSQLServerName
        '    oglohl7.ClientMachineID = gnClientMachineID
        '    oglohl7.Connect(System.Windows.Forms.Application.StartupPath, gloHl7Interface.HL7Library.enumHL7ConnectionType.Create, "2.3")
        '    If oglohl7._HL7FilePath = "" Then
        '        RaiseEvent InvalidFilePath()
        '        Exit Sub
        '    ElseIf System.IO.Directory.Exists(oglohl7._HL7FilePath) = False Then
        '        RaiseEvent InvalidFilePath()
        '        Exit Sub
        '    End If
        '    'call gloHL7 function to create HL7 message for charges P03
        '    oglohl7.CreateMessage(examid, PatientID, intTotalRecords)


        'Catch ex As gloHl7Interface.HL7DatabaseLibrary.HL7DatabaseException
        '    'MessageBox.Show("Error generating HL7 Charges file ,please check log for more details", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)

        '    UpdateLog("Cannot Create HL7 Charges File for Exam ID " & CType(examid, String) & " and Patient ID " & CType(PatientID, String) & " because " & ex.ToString())

        'Catch ex As gloHl7Interface.HL7Library.HL7Exception
        '    'MessageBox.Show("Error generating HL7 Charges file,please check log for more details", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)

        '    UpdateLog("Cannot Create HL7 Charges File for Exam ID " & CType(examid, String) & " and Patient ID " & CType(PatientID, String) & " because " & ex.ToString())
        'Catch ex As Exception
        '    'MessageBox.Show("Error generating HL7 Charges file,please check log for more details", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)

        '    UpdateLog("Cannot Create HL7 Charges File for Exam ID " & CType(examid, String) & " and Patient ID " & CType(PatientID, String) & " because " & ex.ToString())
        'End Try
        'code commented by supriya
        'if setting is false generate genius charges information and post to genius object

        InsertInMessageQueue("P03", PatientID, examid, intTotalRecords)
        'Code commented & start added by kanchan for Hl7 & genius work together for case 3176
        'Else
        '  End If
        'If gblnHL7SENDOUTBOUNDGLOEMR = True AndAlso gblnSaveandFinish = True Then
        '    InsertInMessageQueue("P03", PatientID, examid, intTotalRecords)
        'End If

        '  If gbInGENIUSSENDOUTBOUNDGLOEMR = True AndAlso gbInGeniusSaveClose = True Then

        'InsertInGeniusMessageQueue(PatientID, examid)

        'Below code is commented by madan as per new gloGenius Messagequeue implimentation.
        ''Code End- by kanchan for Hl7 & genius work together for case 3176
        'Dim objDBLayer As New DBImplementationLayer
        'ClsGeniusGeneral.GeniusCode = gstrGeniusCode
        'ClsGeniusGeneral.GeniusPath = gstrGeniusPath

        ''added Genius usersettings...
        'ClsGeniusGeneral.GeniusUserName = gstrGeniusUsername
        'ClsGeniusGeneral.GeniusPassword = gstrGeniusPassword
        'ClsGeniusGeneral.GeniusExam = gnGeniusICD9Driven

        'Try
        '    'populate visit object with charges data for given exam
        '    FillCPT(examid, PatientID)
        '    objDBLayer.Connectionstring = GetConnectionString()
        '    'Send charges information
        '    If Not IsNothing(oVisit) Then
        '        If oVisit.CPTCol.Count > 0 Then

        '            'Code Start - added by roopali for genius path validation
        '            If (ClsGeniusGeneral.GeniusCode = "") Then
        '                MessageBox.Show("No genius Code provided.", "GeniusCode")
        '                Return
        '            End If
        '            If (ClsGeniusGeneral.GeniusPath = "") Then
        '                MessageBox.Show("No genius path provided.", "GeniusPath")
        '                Return
        '            End If
        '            If (ClsGeniusGeneral.GeniusUserName = "") Then
        '                MessageBox.Show("No genius user name provided.", "GeniusUserName")
        '                Return
        '            End If
        '            If (ClsGeniusGeneral.GeniusPassword = "") Then
        '                MessageBox.Show("No genius password provided.", "GeniusPassword")
        '                Return
        '            End If
        '            'Code End - added by roopali for genius path validation

        '            objDBLayer.Sendcharges(oVisit, dtDOS)
        '            intTotalRecords = intTotalRecords + 1
        '            oVisit.CPTCol.Clear()
        '        Else
        '            UpdateLog("Cannot Create Genius Charges File for Exam ID " & CType(examid, String) & " and Patient ID " & CType(PatientID, String) & " as no CPT available ")
        '        End If
        '    End If


        'Catch ex As gloDBInterface.DBInterfaceException
        '    'MessageBox.Show("Error posting charges to Genius ,please check log for more details", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)

        '    UpdateLog("Cannot Create Genius Charges File for Exam ID " & CType(examid, String) & " and Patient ID " & CType(PatientID, String) & ex.ToString())
        'Catch ex As gloDBInterface.DBImplementationException
        '    'MessageBox.Show("Error posting charges to Genius ,please check log for more details", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)

        '    UpdateLog("Cannot Create Genius Charges File for Exam ID " & CType(examid, String) & " and Patient ID " & CType(PatientID, String) & ex.ToString())
        'Catch ex As Exception
        '    'MessageBox.Show("Error posting charges to Genius ,please check log for more details", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)

        '    UpdateLog("Cannot Create Genius Charges File for Exam ID " & CType(examid, String) & " and Patient ID " & CType(PatientID, String) & ex.ToString())
        'End Try
        '  End If

    End Sub

    Private Function FillCPT(ByVal examid As Int64, ByVal patientid As Int64) As Boolean
        Dim _gloDBInterface As gloDBInterface.ClsDBLayer = Nothing
        Dim oCPTs As gloDBInterface.CPTs
        'Dim oCPT As gloDBInterface.CPT
        'Dim oICD9s As gloDBInterface.ICD9s
        'Dim oICD9 As gloDBInterface.ICD9
        'Dim oMods As gloDBInterface.Modifiers
        'Dim oMod As gloDBInterface.Modifier

        Try
            gloEMRGeneralLibrary.gloGeneral.clsgeneral.ConnectionString = GetConnectionString()
            _gloDBInterface = New gloDBInterface.ClsDBLayer(GetConnectionString())
            ' oVisit = New gloDBInterface.Visit
            'oCPTs = New gloDBInterface.CPTs

            oVisit = _gloDBInterface.FillICDCPTMOD(dtDOS, patientid, examid)
            oCPTs = oVisit.CPTCol

            'Dim myNode As TreeNode
            'trvCPTDiagMod.Nodes.Clear()
            'For i As Integer = 0 To oCPTs.Count - 1
            '    oCPT = oCPTs.Item(i)

            '    myNode = New TreeNode
            '    myNode.Tag = oCPT.Code
            '    myNode.Text = oCPT.Code & " - " & oCPT.Description & "-" & oCPT.Unit
            '    trvCPTDiagMod.Nodes.Add(myNode)

            '    oICD9s = oCPT.ICD9Col

            '    Dim childNode As New TreeNode
            '    If oICD9s.Count > 0 Then
            '        childNode.Text = "Diagnosis"
            '        myNode.Nodes.Add(childNode)
            '    End If

            '    Dim tempNode As TreeNode

            '    For j As Integer = 0 To oICD9s.Count - 1
            '        oICD9 = New gloDBInterface.ICD9

            '        oICD9 = oICD9s.Item(j)

            '        tempNode = New TreeNode
            '        tempNode.Tag = oICD9.Code
            '        tempNode.Text = oICD9.Code & " - " & oICD9.Description
            '        childNode.Nodes.Add(tempNode)
            '    Next

            '    oMods = oCPT.ModfierCol

            '    Dim childNode1 As New TreeNode
            '    If oMods.Count > 0 Then
            '        childNode1.Text = "Modifiers"
            '        myNode.Nodes.Add(childNode1)
            '    End If

            '    For j As Integer = 0 To oMods.Count - 1
            '        oMod = New gloDBInterface.Modifier

            '        oMod = oMods.Item(j)

            '        tempNode = New TreeNode
            '        tempNode.Tag = oMod.Code
            '        tempNode.Text = oMod.Code & " - " & oMod.Description
            '        childNode1.Nodes.Add(tempNode)

            '    Next
            'Next
            'trvCPTDiagMod.ExpandAll()
        Catch ex As Exception
            FillCPT = Nothing
            Throw ex
        Finally
            If (IsNothing(_gloDBInterface) = False) Then
                _gloDBInterface.Dispose()
            End If


        End Try
        Return Nothing
    End Function


    Public Sub SendHL7PatientDetails(ByVal PatientId As Int64, ByVal blnIsUpdate As Boolean)
        'old logic to Generate A04/A08
        'Dim oglohl7 As New gloHl7Interface.HL7Library.gloHL7
        'Try
        '    oglohl7.DatabaseName = gstrDatabaseName
        '    oglohl7.ServerName = gstrSQLServerName
        '    oglohl7.ClientMachineID = gnClientMachineID
        '    oglohl7.Connect(System.Windows.Forms.Application.StartupPath, gloHl7Interface.HL7Library.enumHL7ConnectionType.Create, "2.3")
        '    If oglohl7._HL7FilePath = "" Then
        '        RaiseEvent InvalidFilePath()
        '        Exit Sub
        '    End If
        '    If blnIsUpdate Then
        '        oglohl7.CreateA08Message(PatientId)
        '    Else
        '        oglohl7.CreateA04Message(PatientId)
        '    End If

        'Catch ex As gloHl7Interface.HL7DatabaseLibrary.HL7DatabaseException
        '    UpdateLog(ex.ToString)
        '    MessageBox.Show("Patient Registration Details not posted successfully, Please check gloEMR log for more details", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        'Catch ex As gloHl7Interface.HL7Library.HL7Exception
        '    UpdateLog(ex.ToString)
        '    MessageBox.Show("Patient Registration Details not posted successfully, Please check gloEMR log for more details", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        'Catch ex As Exception
        '    UpdateLog(ex.ToString)
        '    MessageBox.Show("Patient Registration Details not posted successfully, Please check gloEMR log for more details", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        'End Try
        'old logic to Generate A04/A08
        Try
            'Code Start -Added by kanchan on 20100313 for setting emdeon patient registration flag

            'If blnIsUpdate Then
            '    InsertInMessageQueue("A08", PatientId, PatientId, 0)
            'Else
            '    InsertInMessageQueue("A04", PatientId, PatientId, 0)
            'End If

            If blnIsUpdate Then
                InsertInMessageQueueforgloLab("A08", PatientId, PatientId, 0)
            Else
                InsertInMessageQueueforgloLab("A04", PatientId, PatientId, 0)
            End If

            'Code End - Added by kanchan on 20100313 for setting emdeon patient registration flag

        Catch ex As Exception
            UpdateLog(ex.ToString)
            MessageBox.Show("Patient Registration Details not posted successfully, Please check gloEMR log for more details", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub
    Public Sub New()

    End Sub
    'Public Sub SendLabs(ByVal OrderId As Int64, ByVal PatientID As Int64, ByVal ArrTestName As ArrayList)
    '    Dim ogloHL7 As New gloHl7Interface.HL7Library.gloHL7
    '    Try


    '        ogloHL7.DatabaseName = gstrDatabaseName
    '        ogloHL7.ServerName = gstrSQLServerName
    '        ogloHL7.ClientMachineID = gnClientMachineID
    '        ogloHL7.Connect(System.Windows.Forms.Application.StartupPath, gloHl7Interface.HL7Library.enumHL7ConnectionType.Create, "2.3")
    '        If ogloHL7._HL7FilePath = "" Then
    '            RaiseEvent InvalidFilePath()
    '            Exit Sub
    '        End If
    '        ogloHL7.CreateMessageforLabs(OrderId, PatientID, ArrTestName)
    '        ogloHL7.Disconnect(gstrgloEMRStartupPath)
    '        MessageBox.Show("HL7 Lab order generated successfully", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

    '    Catch ex As gloHl7Interface.HL7DatabaseLibrary.HL7DatabaseException
    '        MsgBox(ex.ErrMessage)
    '    Catch ex As gloHl7Interface.HL7Library.HL7Exception
    '        MsgBox(ex.ErrMessage)
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    Finally
    '        If Not IsNothing(ogloHL7) Then
    '            ogloHL7.Dispose()
    '            ogloHL7 = Nothing
    '        End If

    '    End Try

    'End Sub
    Public Sub SendLabs(ByVal OrderId As Int64, ByVal PatientID As Int64, ByVal ArrTestName As ArrayList)
        Try
            InsertInMessageQueue("O01", PatientID, OrderId, 0, ArrTestName)
        Catch ex As Exception
            UpdateLog(ex.ToString)
            MessageBox.Show("Lab Order Details not posted successfully, Please check gloEMR log for more details", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub
    'Code Start-Added by kanchan on 20101029 for HL7 setting for immunization
    Public Sub SendImmunization(ByVal eventType As String, ByVal ImmunizationId As Int64, ByVal PatientID As Int64, Optional ByVal IM_TranctionID As Int64 = 0, Optional ByVal _InternalImmunizationStatus As String = "")
        Try
            Dim ArrListImmunizationStatusV04IR As ArrayList = Nothing
            If Not String.IsNullOrEmpty(_InternalImmunizationStatus) Then
                ArrListImmunizationStatusV04IR = New ArrayList
                ArrListImmunizationStatusV04IR.Add(_InternalImmunizationStatus)
            End If
            InsertInMessageQueue(eventType, PatientID, ImmunizationId, 0, ArrListImmunizationStatusV04IR, IM_TranctionID)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Generate, "Generated an Immunization message to send outbound HL7 ", PatientID, ImmunizationId, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            If eventType = "Q11" Then
                MessageBox.Show("Immunization History and Forecast request sent successfully." + vbNewLine + "Once History and Forecast is available, will be notified by task.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            UpdateLog(ex.ToString)
            MessageBox.Show("Immunization Details not posted successfully , Please check gloEMR log for more details", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub
    'Code End-Added by kanchan on 20101029 for HL7 setting for immunization
    Public Sub SendA28(ByVal PatientID As Int64, ByVal _dx As String)
        Try
            InsertInMessageQueue("A28", PatientID, 0, 0, _dx)
        Catch ex As Exception
            UpdateLog(ex.ToString)
            MessageBox.Show("Patient Information not posted successfully, Please check gloEMR log for more details", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Public Sub SendSyndromicSurveillance(ByVal strMessageName As String, ByVal PatientID As Int64, ByVal nVisitID As Int64)
        Try
            InsertInMessageQueue(strMessageName, PatientID, nVisitID, 0, String.Empty)
            If strMessageName = "A08" Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Reports, gloAuditTrail.ActivityCategory.CreateA08, gloAuditTrail.ActivityType.Send, "Generated syndromic message to send outbound HL7 ", PatientID, nVisitID, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Reports, gloAuditTrail.ActivityCategory.CreateA04, gloAuditTrail.ActivityType.Send, "Generated syndromic message to send outbound HL7 ", PatientID, nVisitID, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
        Catch ex As Exception
            UpdateLog(ex.ToString)
            MessageBox.Show("Patient Information not posted successfully, Please check gloEMR log for more details", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Public Sub InsertInMessageQueue(ByVal strMessageName As String, ByVal PatientID As Int64, ByVal OtherID As Int64, ByRef intTotalRecords As Int32, Optional ByVal arrTestName As ArrayList = Nothing, Optional ByVal IM_TranctionID As Int64 = 0)
        Dim conn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim objParam As SqlParameter
        Try
            conn = New SqlConnection(GetConnectionString)
            cmd = New SqlCommand("HL7_InsertMessageQueue", conn)
            cmd.CommandType = CommandType.StoredProcedure

            conn.Open()

            cmd.Parameters.Clear()
            objParam = cmd.Parameters.Add("@dtDatetimeStamp", SqlDbType.DateTime)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = Now

            objParam = Nothing

            objParam = cmd.Parameters.Add("@MessageName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = strMessageName



            objParam = cmd.Parameters.Add("@sMachineID", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = gnClientMachineID.ToString

            objParam = cmd.Parameters.Add("@sMachinename", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = gstrClientMachineName


            objParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = PatientID


            objParam = cmd.Parameters.Add("@nID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = OtherID


            objParam = cmd.Parameters.Add("@Status", SqlDbType.Int)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = 1


            objParam = cmd.Parameters.Add("@sField1", SqlDbType.VarChar, 5000)
            objParam.Direction = ParameterDirection.Input
            Dim strTestName As String = ""
            If Not IsNothing(arrTestName) Then
                For icnt As Int32 = 0 To arrTestName.Count - 1
                    If strTestName = "" Then
                        strTestName = arrTestName.Item(icnt).ToString
                    Else
                        strTestName = strTestName & "|" & arrTestName.Item(icnt).ToString
                    End If
                Next
            End If
            objParam.Value = strTestName

            objParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = GetPrefixTransactionID(PatientID)

            objParam = cmd.Parameters.Add("@nASBaseID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = IM_TranctionID

            cmd.Connection = conn
            cmd.ExecuteNonQuery()
            intTotalRecords = intTotalRecords + 1

        Catch ex As Exception
            UpdateLog("Error generating message " & strMessageName & " " & ex.ToString)
        Finally
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            objParam = Nothing
        End Try
    End Sub

    'Code Start -Added by kanchan on 20100403 for emdeon patient registration Message queue changes
    Private Sub InsertInMessageQueueforgloLab(ByVal strMessageName As String, ByVal PatientID As Int64, ByVal OtherID As Int64, ByRef intTotalRecords As Int32, Optional ByVal arrTestName As ArrayList = Nothing)
        Dim conn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim objParam As SqlParameter
        Try
            conn = New SqlConnection(GetConnectionString)
            cmd = New SqlCommand("HL7_InsertMessageQueueGloLab", conn)
            cmd.CommandType = CommandType.StoredProcedure

            conn.Open()

            cmd.Parameters.Clear()
            objParam = cmd.Parameters.Add("@dtDatetimeStamp", SqlDbType.DateTime)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = Now


            objParam = Nothing

            objParam = cmd.Parameters.Add("@MessageName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = strMessageName

            objParam = cmd.Parameters.Add("@sMachineID", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = gnClientMachineID.ToString

            objParam = cmd.Parameters.Add("@sMachinename", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = gstrClientMachineName


            objParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = PatientID


            objParam = cmd.Parameters.Add("@nID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = OtherID


            objParam = cmd.Parameters.Add("@Status", SqlDbType.Int)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = 1


            objParam = cmd.Parameters.Add("@sField1", SqlDbType.VarChar, 5000)
            objParam.Direction = ParameterDirection.Input
            Dim strTestName As String = ""
            If Not IsNothing(arrTestName) Then
                For icnt As Int32 = 0 To arrTestName.Count - 1
                    If strTestName = "" Then
                        strTestName = arrTestName.Item(icnt).ToString
                    Else
                        strTestName = strTestName & "|" & arrTestName.Item(icnt).ToString
                    End If
                Next
            End If
            objParam.Value = strTestName

            objParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = GetPrefixTransactionID(PatientID)


            objParam = cmd.Parameters.Add("@sField2", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            If strMessageName = "A04" Or strMessageName = "A08" Then
                If gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_hsilabel.Length > 0 Then
                    objParam.Value = "Queue"
                Else
                    objParam.Value = "LabInActive"
                End If
            Else
                objParam.Value = ""
            End If

            cmd.Connection = conn
            cmd.ExecuteNonQuery()
            intTotalRecords = intTotalRecords + 1

        Catch ex As Exception
            UpdateLog("Error generating message " & strMessageName & " " & ex.ToString)
        Finally
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            objParam = Nothing
        End Try

    End Sub
    'Code End -Added by kanchan on 20100403 for emdeon patient registration Message queue changes

    'code added by nilesh on 20101027
    'code to insert record in message queue
    Private Sub InsertInMessageQueue(ByVal strMessageName As String, ByVal PatientID As Int64, ByVal OtherID As Int64, ByRef intTotalRecords As Int32, ByVal _Dx As String, Optional ByVal sField2 As String = Nothing)
        Dim conn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim objParam As SqlParameter
        Try
            conn = New SqlConnection(GetConnectionString)
            cmd = New SqlCommand("HL7_InsertMessageQueue", conn)
            cmd.CommandType = CommandType.StoredProcedure

            conn.Open()

            cmd.Parameters.Clear()
            objParam = cmd.Parameters.Add("@dtDatetimeStamp", SqlDbType.DateTime)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = Now

            objParam = Nothing

            objParam = cmd.Parameters.Add("@MessageName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = strMessageName



            objParam = cmd.Parameters.Add("@sMachineID", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = gnClientMachineID.ToString

            objParam = cmd.Parameters.Add("@sMachinename", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = gstrClientMachineName


            objParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = PatientID


            objParam = cmd.Parameters.Add("@nID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = OtherID


            objParam = cmd.Parameters.Add("@Status", SqlDbType.Int)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = 1


            objParam = cmd.Parameters.Add("@sField1", SqlDbType.VarChar, 5000)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = _Dx

            objParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = GetPrefixTransactionID(PatientID)

            objParam = cmd.Parameters.Add("@sField2", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = sField2

            cmd.Connection = conn
            cmd.ExecuteNonQuery()
            intTotalRecords = intTotalRecords + 1

        Catch ex As Exception
            UpdateLog("Error generating message " & strMessageName & " " & ex.ToString)
        Finally
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            objParam = Nothing
        End Try

    End Sub
    'code end by nilesh on 20101027

    'Added by Madan... 20100902...
    ''This method is used to generate genius message queue for processing charges.
    Private Sub InsertInGeniusMessageQueue(ByVal PatientID As Int64, ByVal OtherID As Int64)
        Dim oDBLayer As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Try
            oDBLayer.Connect(False)

            oDBParameters.Clear()
            oDBParameters.Add("@dtDatetimeStamp", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime)
            oDBParameters.Add("@MessageName", "CHARGES", ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sMachineID", gnClientMachineID.ToString().Trim(), ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sMachinename", gstrClientMachineName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@nOtherID", OtherID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@Status", 1, ParameterDirection.Input, SqlDbType.Int)
            oDBParameters.Add("@sField1", " ", ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sServiceName", "GENIUS", ParameterDirection.Input, SqlDbType.VarChar)

            oDBLayer.ExecuteScalar("Gl_InsertMessageQueue", oDBParameters)

            oDBLayer.Disconnect()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally

            If oDBParameters IsNot Nothing Then
                oDBParameters.Dispose()
            End If

            If oDBLayer IsNot Nothing Then
                oDBLayer.Dispose()
            End If
        End Try
    End Sub

    Public Sub InsertInGeniusMessageQueue(ByVal PatientID As Int64, ByVal ExamID As Int64, ByVal Productname As String)

        Dim oDiagnosis As ClsDiagnosisDBLayer = Nothing
        Try
            oDiagnosis = New ClsDiagnosisDBLayer

            If oDiagnosis.IsICD9CPT_Present(ExamID, PatientID) Then
                InsertInGeniusMessageQueue(PatientID, ExamID)
            End If
        Catch

        Finally

            If Not IsNothing(oDiagnosis) Then
                oDiagnosis.Dispose()
                oDiagnosis = Nothing
            End If

        End Try
    End Sub

    Public Sub InsertInMessageQueue(ByVal PatientID As Int64, ByVal OtherID As Int64)
        Dim oDBLayer As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Try
            oDBLayer.Connect(False)

            oDBParameters.Clear()
            oDBParameters.Add("@dtDatetimeStamp", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime)
            oDBParameters.Add("@sMachineID", gnClientMachineID.ToString().Trim(), ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sMachinename", gstrClientMachineName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@nOtherID", OtherID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@Status", 1, ParameterDirection.Input, SqlDbType.Int)
            oDBParameters.Add("@sField1", " ", ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sServiceName", "GENERAL", ParameterDirection.Input, SqlDbType.VarChar)
            'Patient Portal
            oDBParameters.Add("@PatientPortalSendActivationEmail", mdlGeneral.gblnPatientPortalSendActivationEmail, ParameterDirection.Input, SqlDbType.Bit)
            oDBParameters.Add("@PatientPortalResentEmail", mdlGeneral.gblnPatientPortalActivationEmailAlreadySent, ParameterDirection.Input, SqlDbType.Bit)
            'Patient Portal

            oDBLayer.ExecuteScalar("Gl_InsertMessageQueue", oDBParameters)

            oDBLayer.Disconnect()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally

            If oDBParameters IsNot Nothing Then
                oDBParameters.Dispose()
            End If

            If oDBLayer IsNot Nothing Then
                oDBLayer.Dispose()
            End If
        End Try
    End Sub
    'End madan
End Class

