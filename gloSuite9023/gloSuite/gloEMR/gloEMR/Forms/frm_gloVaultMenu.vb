Imports System.IO
Imports System.Text
Imports System.Drawing.Printing

#Region "Removed as per new implimentation"
'Public Class frm_gloVaultMenu

'    Dim _nPatientId As Int64 = 0
'    Dim _clinicID As Int64 = 0
'    Dim _sConnectionString As String = String.Empty
'    Dim ToPatientList As New gloGeneralItem.gloItems
'    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings

'#Region "List Controls & Variables"

'    Private oListControl As gloListControl.gloListControl
'    Private _CurrentControlType As gloListControl.gloListControlType = gloListControl.gloListControlType.Patient

'    Dim _IsEmailRequest As Boolean = False
'#End Region

'    Enum enmMessagetypes
'        email = 1
'        data = 2
'    End Enum

'    ''Constructor for healthvault
'    Public Sub New(ByVal nPatientId As Int64)

'        ' This call is required by the Windows Form Designer.
'        InitializeComponent()

'        ' Add any initialization after the InitializeComponent() call.

'        _nPatientId = nPatientId

'        ''Clinic id
'        If appSettings("ClinicID") IsNot Nothing Then
'            If appSettings("ClinicID") <> "" Then
'                _clinicID = Convert.ToInt64(appSettings("ClinicID"))
'            Else
'                _clinicID = 0
'            End If
'        Else
'            _clinicID = 0
'        End If

'        ''DataBaseConnectionString
'        If appSettings("DataBaseConnectionString") IsNot Nothing Then
'            If appSettings("DataBaseConnectionString") <> "" Then
'                _sConnectionString = Convert.ToString(appSettings("DataBaseConnectionString"))
'            End If
'        End If
'    End Sub

'    ''Construcot for email request
'    Public Sub New(ByVal nPatientId As Int64, ByVal IsEmailRequest As Boolean)

'        ' This call is required by the Windows Form Designer.
'        InitializeComponent()

'        _IsEmailRequest = IsEmailRequest
'        ' Add any initialization after the InitializeComponent() call.

'        _nPatientId = nPatientId

'        ''Clinic id
'        If appSettings("ClinicID") IsNot Nothing Then
'            If appSettings("ClinicID") <> "" Then
'                _clinicID = Convert.ToInt64(appSettings("ClinicID"))
'            Else
'                _clinicID = 0
'            End If
'        Else
'            _clinicID = 0
'        End If

'        ''DataBaseConnectionString
'        If appSettings("DataBaseConnectionString") IsNot Nothing Then
'            If appSettings("DataBaseConnectionString") <> "" Then
'                _sConnectionString = Convert.ToString(appSettings("DataBaseConnectionString"))
'            End If
'        End If
'    End Sub

'    Private Sub btnApp_Patient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
'        Dim dtPatients As New DataTable
'        Try
'            If oListControl IsNot Nothing Then
'                For i As Integer = Me.Controls.Count - 1 To 0 Step -1
'                    If Me.Controls(i).Name = oListControl.Name Then
'                        Me.Controls.Remove(Me.Controls(i))
'                        Exit For

'                    End If
'                Next
'            End If



'            If _IsEmailRequest Then
'                oListControl = New gloListControl.gloListControl(_sConnectionString, gloListControl.gloListControlType.gloVaultEmail, True, True, 800)
'            Else
'                oListControl = New gloListControl.gloListControl(_sConnectionString, gloListControl.gloListControlType.gloVaultData, True, True, 800)
'            End If


'            'oListControl = New gloListControl.gloListControl(dtPatients, gloListControl.gloListControlType.Patient, True, 800)

'            oListControl.ControlHeader = "Patient"

'            ''Events
'            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelected
'            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosed
'            '' to select already added Patients
'            If Not IsNothing(ToPatientList) And ToPatientList.Count > 0 Then

'                For index As Integer = 0 To ToPatientList.Count - 1
'                    oListControl.SelectedItems.Add(ToPatientList(index))
'                Next

'            End If

'            oListControl.Dock = DockStyle.Fill
'            Me.Controls.Add(oListControl)

'            Me.Width = 800
'            Me.Height = 400
'            oListControl.OpenControl()
'            oListControl.Dock = DockStyle.Fill
'            oListControl.BringToFront()
'        Catch ex As Exception
'            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
'        End Try
'    End Sub

'    Private Sub oListControl_ItemClosed(ByVal sender As Object, ByVal e As System.EventArgs)
'        If _IsEmailRequest Then
'            Me.Width = 382
'            Me.Height = 134
'        Else
'            Me.Width = 382
'            Me.Height = 307
'        End If
'    End Sub

'    Private Sub oListControl_ItemSelected(ByVal sender As Object, ByVal e As System.EventArgs)
'        Dim dtPatients As New DataTable
'        Dim dcId As New DataColumn("ID")
'        Dim dcDesc As New DataColumn("Description")
'        Dim ToItem As gloGeneralItem.gloItem

'        Try
'            dtPatients.Columns.Add(dcDesc)
'            dtPatients.Columns.Add(dcId)

'            If oListControl.SelectedItems.Count > 0 Then
'                ToPatientList = New gloGeneralItem.gloItems

'                For index As Integer = 0 To oListControl.SelectedItems.Count - 1
'                    Dim drTemp As DataRow = dtPatients.NewRow()
'                    drTemp("ID") = oListControl.SelectedItems(index).ID
'                    drTemp("Description") = oListControl.SelectedItems(index).Description
'                    dtPatients.Rows.Add(drTemp)


'                    ToItem = New gloGeneralItem.gloItem()
'                    ToItem.ID = oListControl.SelectedItems(index).ID
'                    ToItem.Description = oListControl.SelectedItems(index).Description
'                    ToPatientList.Add(ToItem)
'                    ToItem = Nothing
'                    drTemp = Nothing

'                Next


'                If Not IsNothing(dtPatients) And dtPatients.Rows.Count > 0 Then
'                    cmbPatients.DataSource = dtPatients
'                    cmbPatients.ValueMember = dtPatients.Columns("ID").ColumnName
'                    cmbPatients.DisplayMember = dtPatients.Columns("Description").ColumnName
'                    cmbPatients.Refresh()
'                End If

'            End If

'            If _IsEmailRequest Then
'                Me.Width = 382
'                Me.Height = 134
'            Else
'                Me.Width = 382
'                Me.Height = 307
'            End If

'        Catch ex As Exception
'            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
'        End Try
'    End Sub

'    Private Sub tlsDM_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsDM_Save.Click
'        Try

'            If ToPatientList.Count <= 0 Then
'                MessageBox.Show("Please select the patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
'                Exit Sub
'            End If

'            If _IsEmailRequest Then
'                ProcessEmailRequest()
'                Me.Close()
'            Else
'                ProcessPatientInformation()
'                Me.Close()
'            End If

'        Catch ex As Exception
'            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
'        End Try

'    End Sub

'    Private Sub frm_gloVaultMenu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
'        Dim dtConfiguration As New DataTable
'        Dim objgloPatient As New gloPatient.gloPatient(_sConnectionString)
'        Dim objPatient As gloPatient.Patient
'        Dim ToItem As gloGeneralItem.gloItem
'        Dim sPatientName As String = String.Empty
'        Dim dtPatients As New DataTable
'        Dim dcId As New DataColumn("ID")
'        Dim dcDesc As New DataColumn("Description")

'        Try


'            If GetGloVaultSettings() = False Then
'                MessageBox.Show("Clinical Exchange settings is turned off, Please contact administrator.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
'                Me.Close()
'                Exit Sub
'            End If


'            dtPatients.Columns.Add(dcDesc)
'            dtPatients.Columns.Add(dcId)

'            'Retrive patient information.
'            objPatient = objgloPatient.GetPatient(_nPatientId)

'            sPatientName = objPatient.DemographicsDetail.PatientFirstName & " " & objPatient.DemographicsDetail.PatientMiddleName & " " & objPatient.DemographicsDetail.PatientLastName

'            ''Check wether this is loaded for email request.
'            If _IsEmailRequest = True Then

'                Me.Icon = Global.gloEMR.My.Resources.Send_Request_Access
'                'pnlConfig.Visible = False

'                If objPatient.DemographicsDetail.PatientEmail.Length <= 0 Then
'                    MessageBox.Show("Selected patient does not have email address.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
'                    Me.Close()
'                End If

'                Me.Width = 382
'                Me.Height = 134

'            Else
'                Me.Icon = Global.gloEMR.My.Resources.Send_Information
'                Dim nid As Int64 = 0

'                nid = GetPatientStatus(_nPatientId)


'                If nid = 1 Then

'                    ''Retrive configuration.
'                    dtConfiguration = GetgloVaultConfigurations()

'                    If IsNothing(dtConfiguration) Then
'                        MessageBox.Show("glovault service configurations not found in the system.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
'                        Me.Close()
'                        Exit Sub
'                    End If

'                    For index As Integer = 0 To dtConfiguration.Rows.Count - 1

'                        If dtConfiguration.Rows(index)("modname").ToString().ToLower() = "demographics" Then
'                            chkLstConfig.Items.Add(dtConfiguration.Rows(index)("modname"), True)
'                        Else
'                            chkLstConfig.Items.Add(dtConfiguration.Rows(index)("modname"))
'                        End If
'                    Next
'                    Me.Width = 382
'                    Me.Height = 307


'                Else

'                    Select Case nid
'                        Case 0
'                            MessageBox.Show("Information: Send information requires patient approval, Please send email request to patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
'                            Me.Close()
'                            Exit Select
'                        Case 11
'                            MessageBox.Show("Information: Send information requires patient approval, Waiting for patient approval.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
'                            Me.Close()
'                            Exit Select

'                        Case 3
'                            MessageBox.Show("Information: Send information requires patient approval. The previous sent email request was expired, Please send request again.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
'                            Me.Close()
'                            Exit Select

'                        Case 2
'                            MessageBox.Show("Information: The HealthVault account access request denied by patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
'                            Me.Close()
'                            Exit Select
'                        Case Else

'                    End Select


'                    'MessageBox.Show("gloVault request access is still pending or not approved.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
'                    'Me.Close()
'                End If
'            End If


'            If sPatientName.Length > 0 Then
'                ToItem = New gloGeneralItem.gloItem()
'                ToItem.ID = _nPatientId
'                ToItem.Description = sPatientName
'                ToPatientList.Add(ToItem)
'            End If

'            If ToPatientList.Count > 0 Then
'                Dim drTemp As DataRow = dtPatients.NewRow()
'                drTemp("ID") = ToPatientList.Item(0).ID
'                drTemp("Description") = ToPatientList.Item(0).Description
'                dtPatients.Rows.Add(drTemp)
'            End If

'            If Not IsNothing(dtPatients) And dtPatients.Rows.Count > 0 Then
'                cmbPatients.DataSource = dtPatients
'                cmbPatients.ValueMember = "ID"
'                cmbPatients.DisplayMember = "Description"
'                cmbPatients.Refresh()
'            End If


'        Catch ex As Exception
'            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
'        Finally

'            If Not IsNothing(dtConfiguration) Then
'                dtConfiguration.Dispose()
'            End If

'            If Not IsNothing(objgloPatient) Then
'                objgloPatient.Dispose()
'            End If

'            If Not IsNothing(objPatient) Then
'                objPatient.Dispose()
'            End If

'            If Not IsNothing(ToItem) Then
'                ToItem.Dispose()
'            End If


'        End Try
'    End Sub

'    Private Function GetgloVaultConfigurations() As DataTable
'        Dim objDbLayer As New gloDatabaseLayer.DBLayer(_sConnectionString)
'        Dim dtConfig As New DataTable()
'        Dim sQuery As String = String.Empty
'        Dim colModname As New DataColumn("modname")
'        Dim colModid As New DataColumn("ModuleId")
'        Try
'            sQuery = "SELECT ISNULL(nModuleId,0) as ModuleId,ISNULL(sMODNAME,'') as modname FROM gl_ModulesMst WHERE bIsActive=1 AND sType='gloVault'"

'            objDbLayer.Connect(False)
'            dtConfig.Columns.Add(colModid)
'            dtConfig.Columns.Add(colModname)


'            dtConfig.Columns("modname").DataType = Type.GetType("System.String")
'            dtConfig.Columns("ModuleId").DataType = Type.GetType("System.Int32")

'            objDbLayer.Retrive_Query(sQuery, dtConfig)

'            If dtConfig.Rows.Count <= 0 Then
'                dtConfig = Nothing
'            End If

'            objDbLayer.Disconnect()

'        Catch ex As Exception
'            dtConfig = Nothing
'            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
'        Finally

'            If Not IsNothing(objDbLayer) Then
'                objDbLayer.Dispose()
'            End If
'            sQuery = String.Empty
'        End Try
'        Return dtConfig
'    End Function

'    Private Sub tlsDM_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsDM_Close.Click
'        Me.Close()
'    End Sub

'    Private Sub chkLstConfig_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLstConfig.SelectedIndexChanged




'    End Sub

'    Private Sub chkLstConfig_ItemCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles chkLstConfig.ItemCheck

'        If chkLstConfig.Items(e.Index).ToString.ToLower() = "demographics" Then
'            e.NewValue = CheckState.Checked
'        End If


'        If chkLstConfig.Items(e.Index).ToString().ToLower() = "ccd" Then
'            If chkLstConfig.GetItemCheckState(1) = CheckState.Unchecked Then
'                Dim _sValue As String = String.Empty
'                _sValue = CheckCCDFilePathAvailability()
'                If _sValue.Length > 0 Then
'                    If Directory.Exists(_sValue) Then
'                        e.NewValue = CheckState.Checked
'                    Else
'                        MessageBox.Show("Please configure valid CCD file generation path in gloEMR Admin settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
'                        e.NewValue = CheckState.Unchecked
'                    End If
'                Else
'                    MessageBox.Show("Please configure CCD file generation path in gloEMR Admin settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
'                    e.NewValue = CheckState.Unchecked
'                End If
'            End If
'        End If

'    End Sub

'    Private Sub ChkAllPatients_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

'    End Sub

'    Private Sub btnApp_ClearPatient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
'        Dim _TempId As Int64 = 0
'        Dim dtPatients As DataTable
'        Try
'            _TempId = Convert.ToInt64(cmbPatients.SelectedValue())

'            For index As Integer = 0 To ToPatientList.Count - 1
'                If ToPatientList.Item(index).ID = _TempId Then
'                    ToPatientList.RemoveAt(index)
'                    Exit For
'                End If
'            Next


'            If Not IsNothing(cmbPatients.DataSource) Then
'                dtPatients = New DataTable()
'                dtPatients = DirectCast(cmbPatients.DataSource, DataTable)
'                dtPatients.Rows.RemoveAt(cmbPatients.SelectedIndex)
'                dtPatients.AcceptChanges()

'                cmbPatients.DataSource = Nothing
'                cmbPatients.DataSource = dtPatients
'                cmbPatients.ValueMember = "ID"
'                cmbPatients.DisplayMember = "Description"
'                cmbPatients.Refresh()
'            End If

'        Catch ex As Exception
'            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
'        Finally
'            'If Not IsNothing(dtPatients) Then
'            '    dtPatients.Dispose()
'            'End If
'        End Try
'    End Sub

'    Private Sub ChkAllPatients_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

'        If ChkAllPatients.CheckState = CheckState.Checked Then
'            cmbPatients.Enabled = False
'            btnApp_ClearPatient.Enabled = False
'            btnApp_Patient.Enabled = False
'        Else
'            cmbPatients.Enabled = True
'            btnApp_ClearPatient.Enabled = True
'            btnApp_Patient.Enabled = True
'        End If

'    End Sub

'    Private Sub InsertMessageQueue(ByVal PatientID As Int64, ByVal sConfigurations As String, ByVal eMessageType As enmMessagetypes, ByVal IsAllPatients As String, Optional ByVal sCCDFilePath As String = "")
'        Dim oDBLayer As New gloDatabaseLayer.DBLayer(GetConnectionString)
'        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
'        Try
'            oDBLayer.Connect(False)

'            oDBParameters.Clear()

'            oDBParameters.Add("@dtDatetimeStamp", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime)

'            oDBParameters.Add("@sMachineID", gnClientMachineID.ToString().Trim(), ParameterDirection.Input, SqlDbType.VarChar)
'            oDBParameters.Add("@sMachinename", gstrClientMachineName, ParameterDirection.Input, SqlDbType.VarChar)


'            If eMessageType = enmMessagetypes.email Then
'                oDBParameters.Add("@MessageName", "HEALTHVAULT-EMAIL", ParameterDirection.Input, SqlDbType.VarChar)
'                oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt)
'                oDBParameters.Add("@nOtherID", gnLoginID, ParameterDirection.Input, SqlDbType.BigInt)

'            ElseIf eMessageType = enmMessagetypes.data Then
'                oDBParameters.Add("@MessageName", "HEALTHVAULT-DATA", ParameterDirection.Input, SqlDbType.VarChar)
'                oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt)
'                oDBParameters.Add("@nOtherID", gnLoginID, ParameterDirection.Input, SqlDbType.BigInt)
'            End If
'            oDBParameters.Add("@sField1", sConfigurations, ParameterDirection.Input, SqlDbType.VarChar)

'            If sCCDFilePath.Length > 0 Then
'                oDBParameters.Add("@sField2", sCCDFilePath, ParameterDirection.Input, SqlDbType.VarChar)
'            End If

'            oDBParameters.Add("@Status", 1, ParameterDirection.Input, SqlDbType.Int)

'            oDBParameters.Add("@sServiceName", "RXSNIFFER", ParameterDirection.Input, SqlDbType.VarChar)
'            oDBLayer.ExecuteScalar("Gl_InsertMessageQueue", oDBParameters)

'            oDBLayer.Disconnect()

'        Catch ex As Exception
'            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
'        Finally

'            If oDBParameters IsNot Nothing Then
'                oDBParameters.Dispose()
'            End If

'            If oDBLayer IsNot Nothing Then
'                oDBLayer.Dispose()
'            End If
'        End Try
'    End Sub

'    Private Function GetgloVaultConfigurationsId(ByVal sConfigurationName As String) As Int64
'        Dim objDbLayer As New gloDatabaseLayer.DBLayer(_sConnectionString)
'        Dim nValue As Int64 = 0
'        Dim sQuery As String = String.Empty
'        Dim objResult As Object
'        Try
'            sQuery = "SELECT ISNULL(nModuleId,0) as ModuleId FROM gl_ModulesMst WHERE bIsActive=1 AND sType='gloVault' AND sMODNAME='" & sConfigurationName & "'"

'            objDbLayer.Connect(False)

'            objResult = objDbLayer.ExecuteScalar_Query(sQuery)

'            If objResult IsNot Nothing AndAlso objResult.ToString() <> "" Then
'                nValue = Convert.ToInt64(objResult.ToString())
'            End If

'            objDbLayer.Disconnect()

'        Catch ex As Exception
'            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
'            nValue = 0
'        Finally

'            If Not IsNothing(objDbLayer) Then
'                objDbLayer.Dispose()
'            End If

'            objResult = Nothing
'            sQuery = String.Empty
'        End Try
'        Return nValue
'    End Function

'    'Private Function GetHealthVaultPatients(ByVal eMessageType As enmMessagetypes) As DataTable
'    '    Dim objDbLayer As New gloDatabaseLayer.DBLayer(_sConnectionString)
'    '    Dim sQuery As String = String.Empty
'    '    Dim dtPatients As New DataTable

'    '    Try

'    '        If eMessageType = enmMessagetypes.email Then
'    '            sQuery = ""

'    '            'ElseIf eMessageType = enmMessagetypes.email Then

'    '            sQuery = " SELECT    dbo.Patient.sPatientCode AS Code, dbo.Patient.sFirstName AS [First Name], dbo.Patient.sMiddleName AS MI, dbo.Patient.sLastName AS [Last Name], " & _
'    '                     " dbo.Patient.nSSN AS SSN, dbo.Patient.dtDOB AS DOB, dbo.Patient.sPhone AS Phone, dbo.Patient.sMobile AS Mobile, dbo.Patient.sEmail, " & _
'    '                     " COALESCE(dbo.Provider_MST.sFirstName,'') +' '+ COALESCE(dbo.Provider_MST.sLastName,'') as Provider " & _
'    '                     " FROM         dbo.Patient INNER JOIN  dbo.Provider_MST ON dbo.Patient.nProviderID = dbo.Provider_MST.nProviderID " & _
'    '                     " Where dbo.Patient.sEmail IS NOT NULL AND  dbo.Patient.sEmail <> ''"
'    '        End If



'    '        objDbLayer.Connect(False)

'    '        objDbLayer.Retrive_Query(sQuery, dtPatients)

'    '        objDbLayer.Disconnect()

'    '    Catch ex As Exception
'    '        dtPatients = Nothing
'    '        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
'    '    Finally
'    '        If Not IsNothing(objDbLayer) Then
'    '            objDbLayer.Dispose()
'    '        End If

'    '        sQuery = String.Empty
'    '    End Try
'    '    Return dtPatients

'    'End Function

'    'Private Function ValidatePatient(ByVal eMessType As enmMessagetypes) As Boolean
'    '    Dim objDbLayer As New gloDatabaseLayer.DBLayer(_sConnectionString)
'    '    Dim sQuery As String = String.Empty
'    '    Dim nValue As Int64 = 0
'    '    Dim objResult As Object

'    '    Try



'    '    Catch ex As Exception

'    '    Finally

'    '        If Not IsNothing(objDbLayer) Then
'    '            objDbLayer.Dispose()
'    '        End If

'    '        objResult = Nothing
'    '        sQuery = String.Empty
'    '    End Try


'    'End Function

'    Private Function GetPatientStatus(ByVal nSelectedPatientId As Int64) As Int64
'        Dim objDbLayer As New gloDatabaseLayer.DBLayer(_sConnectionString)
'        Dim sQuery As String = String.Empty
'        Dim nValue As Int64 = 0
'        Dim objResult As Object
'        Try

'            sQuery = "select ISNULL(nExternalStatus,0) as nStatus from PatientExternalCodes where sModuleName='HEALTHVAULT' AND sExternalType <> 'HV-LASTSENT' AND nPatientId =" & nSelectedPatientId

'            objDbLayer.Connect(False)

'            objResult = objDbLayer.ExecuteScalar_Query(sQuery)

'            If ((objResult IsNot Nothing)) And Not String.IsNullOrEmpty(Convert.ToString(objResult)) Then
'                nValue = Convert.ToInt64(objResult)
'            End If

'            objDbLayer.Disconnect()

'        Catch ex As Exception
'            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
'            nValue = 0
'        Finally

'            If Not IsNothing(objDbLayer) Then
'                objDbLayer.Dispose()
'            End If

'            objResult = Nothing
'            sQuery = String.Empty
'        End Try
'        Return nValue
'    End Function
'    ''' <summary>
'    ''' Method to validate email.
'    ''' </summary>
'    ''' <param name="nSelectedPatientId"></param>
'    ''' <param name="sPatientName"></param>
'    ''' <returns>'3' Email not found in patient Externalcodes,'1' Email not found in patienttable,'2' Email are different,'11' EMails are same. </returns>
'    ''' <remarks></remarks>
'    Private Function ValidateEmail(ByVal nSelectedPatientId As Int64, ByRef sEmail As String) As Int16
'        Dim objDbLayer As New gloDatabaseLayer.DBLayer(_sConnectionString)
'        Dim sQuery As String = String.Empty
'        Dim sExterValue As String = String.Empty
'        Dim sInterValue As String = String.Empty
'        Dim objResult As Object = Nothing
'        Dim nResult As Int16 = 0

'        Try
'            ''Retrive Email address from externalcodes table.
'            sQuery = "select ISNULL(sExternalSubType ,'') as Email from PatientExternalCodes where sExternalType='HVSEND' AND sModuleName='HEALTHVAULT' AND nPatientId =" & nSelectedPatientId

'            objDbLayer.Connect(False)

'            objResult = objDbLayer.ExecuteScalar_Query(sQuery)

'            If ((objResult IsNot Nothing)) And Not String.IsNullOrEmpty(Convert.ToString(objResult)) Then
'                sExterValue = Convert.ToString(objResult).Trim()
'            Else
'                nResult = 3
'            End If

'            'Retrive email address from patient table.
'            objResult = Nothing

'            sQuery = String.Empty

'            sQuery = "SELECT ISNULL(sEmail,'')as Email FROM Patient WHERE nPatientID=" & nSelectedPatientId

'            objResult = objDbLayer.ExecuteScalar_Query(sQuery)

'            If ((objResult IsNot Nothing)) And Not String.IsNullOrEmpty(Convert.ToString(objResult)) Then
'                sInterValue = Convert.ToString(objResult).Trim()
'            Else
'                sEmail = sExterValue
'                nResult = 1
'            End If

'            objDbLayer.Disconnect()


'            If nResult <> 3 And nResult <> 1 Then
'                If String.Compare(sInterValue, sExterValue) = 0 Then
'                    sEmail = sExterValue
'                    nResult = 11
'                Else
'                    sEmail = sExterValue
'                    nResult = 2
'                End If
'            End If
'        Catch ex As Exception
'            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
'        Finally

'            If Not IsNothing(objDbLayer) Then
'                objDbLayer.Dispose()
'            End If
'            objResult = Nothing
'            sQuery = String.Empty
'        End Try
'        Return nResult
'    End Function
'    Private Function ValidateUserExchangeInput(ByVal _type As String, ByVal _currentPatId As Int64) As Boolean
'        Dim strSql As String = String.Empty
'        Dim objDbLayer As New gloDatabaseLayer.DBLayer(_sConnectionString)
'        Dim sExterValue As String = String.Empty
'        Dim sInterValue As String = String.Empty
'        Dim sPatientName As String = String.Empty
'        Dim dtResult As New DataTable
'        Dim blnResult As Boolean = False
'        Dim dgResult As New DialogResult
'        Try

'            strSql = "SELECT     Patient.sEmail AS SourceEmail, PatientExternalCodes.nExternalStatus as nStatus, PatientExternalCodes.sExternalSubType AS ActiveEmail, " & _
'                      " PatientExternalCodes.sExternalType as sStype, PatientExternalCodes.sModuleName, Patient.sFirstName, Patient.sLastName, Patient.sMiddleName,  " & _
'                    " PatientExternalCodes.sExternalSystemCode, PatientExternalCodes.sExternalValue, PatientExternalCodes.dtAccessDate  FROM  Patient INNER JOIN " & _
'                      " PatientExternalCodes ON Patient.nPatientID = PatientExternalCodes.nPatientId " & _
'                        "where PatientExternalCodes.sModuleName='HEALTHVAULT' AND sExternalType <> 'HV-LASTSENT' AND PatientExternalCodes.npatientid = " & _currentPatId & ""

'            objDbLayer.Connect(False)
'            objDbLayer.Retrive_Query(strSql, dtResult)
'            objDbLayer.Disconnect()

'            If dtResult Is Nothing Or dtResult.Rows.Count <= 0 Then
'                Return True
'                Exit Function
'            End If

'            'Get emails.
'            sExterValue = Convert.ToString(dtResult.Rows(0)("ActiveEmail"))
'            sInterValue = Convert.ToString(dtResult.Rows(0)("SourceEmail"))
'            sPatientName = Convert.ToString(dtResult.Rows(0)("sFirstName")) & " " & Convert.ToString(dtResult.Rows(0)("sMiddleName")) & " " & Convert.ToString(dtResult.Rows(0)("sLastName"))

'            If _type = "email" Then

'                ''11-Request sent,0-Allowed to send,1--approved,2--denied,3--expired
'                Dim nStatus As Int16 = 0
'                nStatus = Convert.ToInt16(dtResult.Rows(0)("nStatus"))

'                Select Case nStatus
'                    Case 11 ''Email Request sent, Waiting for approval
'                        If String.Compare(sInterValue, sExterValue) = 0 Then
'                            dgResult = MessageBox.Show("The HealthVault account request access email already sent to patient '" & sPatientName & "'." & vbCrLf & "Waiting for approval. Do you want to send the request again.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
'                            If dgResult = Windows.Forms.DialogResult.Yes Then
'                                Return True
'                            Else
'                                Return False
'                            End If
'                        Else '' Patient selected other email than gloDatabase ' Alrewady request sent to one email1- now sending for another email now
'                            dgResult = MessageBox.Show("The HealthVault account request access email already sent to patient '" & sPatientName & "' on email account " & sExterValue & " and waiting for approval." & vbCrLf & "Do you want to send the request again to changed email account " & sInterValue, gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
'                            If dgResult = Windows.Forms.DialogResult.Yes Then
'                                Return True
'                            Else
'                                Return False
'                            End If
'                        End If
'                        Exit Select
'                    Case 1 '' Approved - trying to send again
'                        If String.Compare(sInterValue, sExterValue) = 0 Then
'                            dgResult = MessageBox.Show("The HealthVault account access approved from patient '" & sPatientName & "'. Do you want to send the request again?" & vbCrLf & " Note: Response will overwrite exitsting rights.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
'                            If dgResult = Windows.Forms.DialogResult.Yes Then
'                                Return True
'                            Else
'                                Return False
'                            End If
'                        Else '' Approved - trying to send again , But found recieved right email is diferent than gloDatabase
'                            dgResult = MessageBox.Show("The HealthVault account access approved from patient '" & sPatientName & "' email account: '" & sExterValue & "'. Do you want to send the request again to changed email account: '" & sInterValue & "'?" & vbCrLf & " Note: Response will overwrite exitsting rights.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
'                            If dgResult = Windows.Forms.DialogResult.Yes Then
'                                Return True
'                            Else
'                                Return False
'                            End If
'                        End If
'                        Exit Select

'                    Case 0
'                        Return True
'                        Exit Select
'                    Case 2 'Accesss Denied from patient
'                        If String.Compare(sInterValue, sExterValue) = 0 Then
'                            dgResult = MessageBox.Show("The HealthVault account access request denied by patient '" & sPatientName & "' from eamil account: '" & sExterValue & "'." & vbCrLf & "Do you want to send the request again? ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
'                            If dgResult = Windows.Forms.DialogResult.Yes Then
'                                Return True
'                            Else
'                                Return False
'                            End If
'                        Else ' Access Denied but the email address denied is different from current email address
'                            dgResult = MessageBox.Show("The HealthVault account access request denied by patient '" & sPatientName & "' from eamil account: '" & sExterValue & "'." & vbCrLf & "Do you want to send the request again to changed email account: '" & sInterValue & "'? ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
'                            If dgResult = Windows.Forms.DialogResult.Yes Then
'                                Return True
'                            Else
'                                Return False
'                            End If
'                        End If
'                        Exit Select
'                    Case 3 'Expired.
'                        If String.Compare(sInterValue, sExterValue) = 0 Then
'                            dgResult = MessageBox.Show("The HealthVault account email request expired for patient '" & sPatientName & "'. Do you want to send the request again?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
'                            If dgResult = Windows.Forms.DialogResult.Yes Then
'                                Return True
'                            Else
'                                Return False
'                            End If
'                        Else ' Previous sent memail request was expired, now tell user that sending tho another email account.
'                            dgResult = MessageBox.Show("The HealthVault account email request expired for patient '" & sPatientName & "' on email account: '" & sExterValue & "'." & vbCrLf & "Do you want to send the request again to changed email account '" & sInterValue & "'?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
'                            If dgResult = Windows.Forms.DialogResult.Yes Then
'                                Return True
'                            Else
'                                Return False
'                            End If
'                        End If
'                        Exit Select
'                    Case Else

'                End Select

'            ElseIf _type = "information" Then
'                If String.Compare(sInterValue, sExterValue) = 0 Then
'                    Return True
'                Else
'                    dgResult = MessageBox.Show("The selected patient '" & sPatientName & "' information  will be processed to approved HealthVault account '" & sExterValue & "' not to changed email account '" & sInterValue & "'. Do you want to proceed?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
'                    If dgResult = Windows.Forms.DialogResult.Yes Then
'                        Return True
'                    Else
'                        Return False
'                    End If
'                End If
'            End If

'        Catch ex As Exception
'            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
'        Finally
'            strSql = String.Empty

'            If Not IsNothing(objDbLayer) Then
'                objDbLayer.Dispose()
'            End If
'            sExterValue = String.Empty
'            sInterValue = String.Empty
'            sPatientName = String.Empty
'        End Try
'        Return blnResult
'    End Function




'    Private Sub ProcessEmailRequest()
'        Try
'            If ToPatientList.Count > 0 Then
'                For index As Integer = 0 To ToPatientList.Count - 1

'                    Dim dgResult As New DialogResult
'                    Dim nSelectedPatientID As Int64 = Convert.ToInt64(ToPatientList.Item(index).ID)

'                    If ValidateUserExchangeInput("email", nSelectedPatientID) Then
'                        InsertMessageQueue(nSelectedPatientID, "", enmMessagetypes.email, "")
'                        Continue For
'                    Else
'                        Continue For
'                    End If

'                    '    Dim nEmailStatus As Int16 = 0
'                    '    Dim sEmailAddress As String = String.Empty

'                    '    nEmailStatus = ValidateEmail(nSelectedPatientID, sEmailAddress)

'                    '    '***********Email Validation**************
'                    '    '3' Email not found in patient Externalcodes,
'                    '    '1' Email not found in patienttable,
'                    '    '2' Email are different,
'                    '    '11' EMails are same. 
'                    '    '*****************************************

'                    '    Select Case nEmailStatus
'                    '        Case 2
'                    '            dgResult = MessageBox.Show("The request access mail for patient '" + ToPatientList.Item(index).Description + "' was already sent for email account ' " & sEmailAddress & " '." & vbCrLf & " Do you want to send the request again.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
'                    '            If dgResult = Windows.Forms.DialogResult.Yes Then
'                    '                InsertMessageQueue(nSelectedPatientID, "", enmMessagetypes.email, "")
'                    '                Continue For
'                    '            Else
'                    '                Continue For
'                    '            End If
'                    '            Exit Select

'                    '    End Select


'                    '    Dim nID As Int64 = 0

'                    '    nID = GetPatientStatus(nSelectedPatientID)



'                    '    ''11-Request sent,0-Allowed to send,1--approved,2--denied,3--expired
'                    '    Select Case nID
'                    '        Case 11
'                    '            dgResult = MessageBox.Show("The request access mail for patient '" + ToPatientList.Item(index).Description + "' was already sent and waiting for approval. Do you want to send the request again.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
'                    '            If dgResult = Windows.Forms.DialogResult.Yes Then
'                    '                InsertMessageQueue(nSelectedPatientID, "", enmMessagetypes.email, "")
'                    '            Else
'                    '                Exit Select
'                    '            End If
'                    '            Exit Select
'                    '        Case 0
'                    '            InsertMessageQueue(nSelectedPatientID, "", enmMessagetypes.email, "")
'                    '            Exit Select
'                    '        Case 1
'                    '            MessageBox.Show("The patient '" + ToPatientList.Item(index).Description + "' was already approved.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
'                    '            Exit Select
'                    '        Case 2
'                    '            dgResult = MessageBox.Show("The request access mail for patient '" + ToPatientList.Item(index).Description + "' was already sent and request has denied by patient. Do you want to send the request again.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
'                    '            If dgResult = Windows.Forms.DialogResult.Yes Then
'                    '                InsertMessageQueue(nSelectedPatientID, "", enmMessagetypes.email, "")
'                    '            Else
'                    '                Exit Select
'                    '            End If
'                    '            Exit Select
'                    '        Case 3
'                    '            dgResult = MessageBox.Show("The request access mail for patient '" + ToPatientList.Item(index).Description + "' was already sent and request was expired. Do you want to send the request again.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
'                    '            If dgResult = Windows.Forms.DialogResult.Yes Then
'                    '                InsertMessageQueue(nSelectedPatientID, "", enmMessagetypes.email, "")
'                    '            Else
'                    '                Exit Select
'                    '            End If
'                    '            Exit Select
'                    '        Case Else

'                    '    End Select
'                Next
'            Else
'                MessageBox.Show("Please select the patients", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
'            End If
'        Catch ex As Exception
'            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
'        End Try
'    End Sub

'    Private Sub ProcessPatientInformation()
'        Dim _sConfigurations As String = String.Empty
'        Dim _sCCDFilePath As String = String.Empty
'        Dim dgResult As New DialogResult

'        Try
'            Application.DoEvents()
'            pnlProcess.Visible = True
'            Application.DoEvents()
'            For index As Integer = 0 To chkLstConfig.CheckedItems.Count - 1
'                If index = 0 Then
'                    _sConfigurations += GetgloVaultConfigurationsId(chkLstConfig.CheckedItems.Item(index).ToString()).ToString()
'                Else
'                    _sConfigurations += "," + GetgloVaultConfigurationsId(chkLstConfig.CheckedItems.Item(index).ToString()).ToString()
'                End If
'            Next

'            ''Genreate CCD if available and send request to messagequeue
'            If ToPatientList.Count > 0 Then
'                For indexi As Integer = 0 To ToPatientList.Count - 1
'                    If ValidateUserExchangeInput("information", Convert.ToInt64(ToPatientList.Item(indexi).ID)) Then
'                        'Dim nEmailValidatedValue As Int16 = 0
'                        'Dim _sEmail As String = String.Empty
'                        'nEmailValidatedValue = ValidateEmail(Convert.ToInt64(ToPatientList.Item(indexi).ID), _sEmail)

'                        ''***********Email Validation**************
'                        ''3' Email not found in patient Externalcodes,
'                        ''1' Email not found in patienttable,
'                        ''2' Email are different,
'                        ''11' EMails are same. 
'                        ''*****************************************

'                        'Select Case nEmailValidatedValue
'                        '    Case 1
'                        '        dgResult = MessageBox.Show("Patient '" & ToPatientList.Item(indexi).Description & "' does not have email address. Do you want to send information for " & vbCrLf & "approved email address ' " & _sEmail & " ' ?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)

'                        '        If dgResult = Windows.Forms.DialogResult.No Then
'                        '            Continue For
'                        '        End If
'                        '        Exit Select
'                        '    Case 2
'                        '        dgResult = MessageBox.Show("Patient '" & ToPatientList.Item(indexi).Description & "' has approved healthvault account access for ' " & _sEmail & " ' which is not matching with " & vbCrLf & "current patient email address, Data will be sent to the approved email account." & vbCrLf & vbCrLf & "Do you want to send information to approved account?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)

'                        '        If dgResult = Windows.Forms.DialogResult.No Then
'                        '            Continue For
'                        '        End If

'                        '        Exit Select
'                        '    Case 3
'                        '        Exit Select
'                        '    Case 11
'                        '        ''Send to message queue.
'                        '        Exit Select
'                        '    Case Else
'                        '        Exit Select
'                        'End Select

'                        ''Generate CCD
'                        For indexL As Integer = 0 To chkLstConfig.CheckedItems.Count - 1
'                            If chkLstConfig.CheckedItems.Item(indexL).ToString().ToLower() = "ccd" Then
'                                Try
'                                    _sCCDFilePath = GenerateCCDFilePath(Convert.ToInt64(ToPatientList.Item(indexi).ID))
'                                Catch ex As Exception
'                                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
'                                End Try
'                            End If
'                        Next
'                        ''Genreate MessageQueue
'                        InsertMessageQueue(Convert.ToInt64(ToPatientList.Item(indexi).ID), _sConfigurations, enmMessagetypes.data, "", _sCCDFilePath)
'                        _sCCDFilePath = String.Empty
'                    End If
'                Next
'            Else
'                MessageBox.Show("Please select the patients", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
'            End If
'        Catch ex As Exception
'            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
'        Finally
'            _sCCDFilePath = String.Empty
'            _sConfigurations = String.Empty
'            pnlProcess.Visible = False
'        End Try
'    End Sub

'    ''' <summary>
'    ''' Method to Retrive and check folder availibility
'    ''' </summary>
'    ''' <returns></returns>
'    ''' <remarks></remarks>
'    Private Function CheckCCDFilePathAvailability() As String
'        Dim oDBLayer As New gloDatabaseLayer.DBLayer(GetConnectionString)
'        Dim sQuery As String = String.Empty
'        Dim objResult As Object = Nothing
'        Dim sValue As String = String.Empty
'        Try
'            sQuery = "select sSettingsValue from settings where sSettingsName='CCD File PATH'"
'            oDBLayer.Connect(False)
'            objResult = oDBLayer.ExecuteScalar_Query(sQuery)

'            If ((objResult IsNot Nothing)) And Not String.IsNullOrEmpty(Convert.ToString(objResult)) Then
'                sValue = Convert.ToString(objResult)
'            End If

'            oDBLayer.Disconnect()
'        Catch ex As Exception
'            sValue = String.Empty
'            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
'        Finally
'            If Not IsNothing(oDBLayer) Then
'                oDBLayer.Dispose()
'            End If
'            objResult = Nothing
'            sQuery = String.Empty
'        End Try
'        Return sValue
'    End Function

'    ''' <summary>
'    ''' Method to genrate ccd and returns filepath.
'    ''' </summary>
'    ''' <param name="ccdSelection"></param>
'    ''' <returns></returns>
'    ''' <remarks></remarks>
'    Private Function GenerateCCDFilePath(ByVal nSelectedPatientId As Int64) As String
'        Dim sCCDPath As String = String.Empty
'        Dim objCCD As New gloCCDLibrary.gloCCDInterface()
'        Try
'            gloCCDLibrary.gloLibCCDGeneral.CCDFileGenerationPath = gstrCCDFilePath
'            gloCCDLibrary.gloLibCCDGeneral.Connectionstring = GetConnectionString()

'            sCCDPath = objCCD.GenerateClinicalInformation(nSelectedPatientId, gnLoginID, "All")

'        Catch ex As Exception
'            sCCDPath = String.Empty
'            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
'        Finally
'            If Not IsNothing(objCCD) Then
'                objCCD.Dispose()
'            End If
'        End Try

'        Return sCCDPath
'    End Function

'    ''' <summary>
'    ''' Retrive gloVault Settings
'    ''' </summary>
'    ''' <returns></returns>
'    ''' <remarks></remarks>
'    Private Function GetGloVaultSettings() As Boolean
'        Dim oDBLayer As New gloDatabaseLayer.DBLayer(GetConnectionString)
'        Dim sQuery As String = String.Empty
'        Dim objResult As Object = Nothing
'        Dim sValue As Boolean = False
'        Try
'            sQuery = "select sSettingsValue from settings where sSettingsName='GLOVAULTVISIBILITY'"
'            oDBLayer.Connect(False)
'            objResult = oDBLayer.ExecuteScalar_Query(sQuery)

'            If ((objResult IsNot Nothing)) And Not String.IsNullOrEmpty(Convert.ToString(objResult)) Then
'                sValue = Convert.ToBoolean(objResult)
'            End If

'            oDBLayer.Disconnect()
'        Catch ex As Exception
'            sValue = String.Empty
'            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
'        Finally
'            If Not IsNothing(oDBLayer) Then
'                oDBLayer.Dispose()
'            End If
'            objResult = Nothing
'            sQuery = String.Empty
'        End Try
'        Return sValue
'    End Function

'End Class
#End Region

Public Class frm_gloVaultMenu

    Dim dtMyQuestions As New DataTable()
    Dim sQuestions As String() = New String(8) {}
    Dim _nPatientId As Int64 = 0
    Dim sPatientName As String = String.Empty
    Dim _clinicID As Int64 = 0
    Dim _sConnectionString As String = String.Empty
    'Dim ToPatientList As New gloGeneralItem.gloItems
    Dim objPrintDocument As PrintDocument
    Dim sPrintString As StringBuilder
    Dim sHealthVaultId As String = String.Empty
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings

#Region "List Controls & Variables"

    Private oListControl As gloListControl.gloListControl
    Private _CurrentControlType As gloListControl.gloListControlType = gloListControl.gloListControlType.Patient

    Dim _IsEmailRequest As Boolean = False
#End Region

    Enum enmMessagetypes
        email = 1
        data = 2
        disconnect = 3
    End Enum
    Enum enmEmailTypes
        PrintAndMail = 1
        OnlyEmail = 2
        OnlyPrint = 3
    End Enum
    'Constructor for healthvault
    Public Sub New(ByVal nPatientId As Int64)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _nPatientId = nPatientId

        ''Clinic id
        If appSettings("ClinicID") IsNot Nothing Then
            If appSettings("ClinicID") <> "" Then
                _clinicID = Convert.ToInt64(appSettings("ClinicID"))
            Else
                _clinicID = 0
            End If
        Else
            _clinicID = 0
        End If

        ''DataBaseConnectionString
        If appSettings("DataBaseConnectionString") IsNot Nothing Then
            If appSettings("DataBaseConnectionString") <> "" Then
                _sConnectionString = Convert.ToString(appSettings("DataBaseConnectionString"))
            End If
        End If
    End Sub

    ''Construcot for email request
    Public Sub New(ByVal nPatientId As Int64, ByVal IsEmailRequest As Boolean)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        _IsEmailRequest = IsEmailRequest
        ' Add any initialization after the InitializeComponent() call.

        _nPatientId = nPatientId

        ''Clinic id
        If appSettings("ClinicID") IsNot Nothing Then
            If appSettings("ClinicID") <> "" Then
                _clinicID = Convert.ToInt64(appSettings("ClinicID"))
            Else
                _clinicID = 0
            End If
        Else
            _clinicID = 0
        End If
        ''DataBaseConnectionString
        If appSettings("DataBaseConnectionString") IsNot Nothing Then
            If appSettings("DataBaseConnectionString") <> "" Then
                _sConnectionString = Convert.ToString(appSettings("DataBaseConnectionString"))
            End If
        End If
    End Sub

    ''' <summary>
    ''' Retrive gloVault Settings
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetGloVaultSettings() As Boolean
        Dim oDBLayer As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim sQuery As String = String.Empty
        Dim objResult As Object = Nothing
        Dim sValue As Boolean = False
        Try
            sQuery = "select sSettingsValue from settings where sSettingsName='GLOVAULTVISIBILITY'"
            oDBLayer.Connect(False)
            objResult = oDBLayer.ExecuteScalar_Query(sQuery)

            If ((objResult IsNot Nothing)) AndAlso Not String.IsNullOrEmpty(Convert.ToString(objResult)) Then
                sValue = Convert.ToBoolean(objResult)
            End If

            oDBLayer.Disconnect()
        Catch ex As Exception
            sValue = String.Empty
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If Not IsNothing(oDBLayer) Then
                oDBLayer.Dispose()
            End If
            objResult = Nothing
            sQuery = String.Empty
        End Try
        Return sValue
    End Function

    Private Sub ProcessPatientInformation()
        Dim _sConfigurations As String = String.Empty
        Dim _sCCDFilePath As String = String.Empty
        Dim dgResult As New DialogResult

        Try
            Application.DoEvents()
            pnlProcess.BringToFront()

            pnlProcess.Visible = True
            Application.DoEvents()
            For index As Integer = 0 To chkLstConfig.CheckedItems.Count - 1
                If index = 0 Then
                    _sConfigurations += GetgloVaultConfigurationsId(chkLstConfig.CheckedItems.Item(index).ToString()).ToString()
                Else
                    _sConfigurations += "," + GetgloVaultConfigurationsId(chkLstConfig.CheckedItems.Item(index).ToString()).ToString()
                End If
            Next

            ''Genreate CCD if available and send request to messagequeue
            For indexL As Integer = 0 To chkLstConfig.CheckedItems.Count - 1
                If chkLstConfig.CheckedItems.Item(indexL).ToString().Contains("CCD") Then
                    Try
                        _sCCDFilePath = GenerateCCDFilePath(_nPatientId)
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
                    End Try
                End If
            Next
            ''Genreate MessageQueue
            InsertMessageQueue(_nPatientId, _sConfigurations, enmMessagetypes.data, "", _sCCDFilePath)
            _sCCDFilePath = String.Empty


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            _sCCDFilePath = String.Empty
            _sConfigurations = String.Empty
            pnlProcess.Visible = False
        End Try
    End Sub

    ''' <summary>
    ''' Method to Retrive and check folder availibility
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckCCDFilePathAvailability() As String
        Dim oDBLayer As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim sQuery As String = String.Empty
        Dim objResult As Object = Nothing
        Dim sValue As String = String.Empty
        Try
            sQuery = "select sSettingsValue from settings where sSettingsName='CCD File PATH'"
            oDBLayer.Connect(False)
            objResult = oDBLayer.ExecuteScalar_Query(sQuery)

            If ((objResult IsNot Nothing)) AndAlso Not String.IsNullOrEmpty(Convert.ToString(objResult)) Then
                sValue = Convert.ToString(objResult)
            End If

            oDBLayer.Disconnect()
        Catch ex As Exception
            sValue = String.Empty
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If Not IsNothing(oDBLayer) Then
                oDBLayer.Dispose()
            End If
            objResult = Nothing
            sQuery = String.Empty
        End Try
        Return sValue
    End Function

    ''' <summary>
    ''' Method to genrate ccd and returns filepath.
    ''' </summary>
    ''' <param name="nSelectedPatientId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateCCDFilePath(ByVal nSelectedPatientId As Int64) As String
        Dim sCCDPath As String = String.Empty
        Dim objCCD As New gloCCDLibrary.gloCCDInterface()
        Try
            gloCCDLibrary.gloLibCCDGeneral.CCDFileGenerationPath = gstrCCDFilePath
            gloCCDLibrary.gloLibCCDGeneral.Connectionstring = GetConnectionString()

            sCCDPath = objCCD.GenerateClinicalInformation(nSelectedPatientId, gnLoginID, "All")

        Catch ex As Exception
            sCCDPath = String.Empty
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If Not IsNothing(objCCD) Then
                objCCD.Dispose()
            End If
        End Try

        Return sCCDPath
    End Function

    Private Function GetgloVaultConfigurations() As DataTable
        Dim objDbLayer As New gloDatabaseLayer.DBLayer(_sConnectionString)
        Dim dtConfig As New DataTable()
        Dim sQuery As String = String.Empty
        Dim colModname As New DataColumn("modname")
        Dim colModid As New DataColumn("ModuleId")
        Try
            sQuery = "SELECT ISNULL(nModuleId,0) as ModuleId,ISNULL(sMODNAME,'') as modname FROM gl_ModulesMst WHERE bIsActive=1 AND sType='gloVault'"

            objDbLayer.Connect(False)
            dtConfig.Columns.Add(colModid)
            dtConfig.Columns.Add(colModname)


            dtConfig.Columns("modname").DataType = Type.GetType("System.String")
            dtConfig.Columns("ModuleId").DataType = Type.GetType("System.Int32")

            objDbLayer.Retrive_Query(sQuery, dtConfig)

            If dtConfig.Rows.Count <= 0 Then
                dtConfig = Nothing
            End If

            objDbLayer.Disconnect()

        Catch ex As Exception
            dtConfig = Nothing
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally

            If Not IsNothing(objDbLayer) Then
                objDbLayer.Dispose()
            End If
            sQuery = String.Empty
        End Try
        Return dtConfig
    End Function

    Private Sub InsertMessageQueue(ByVal PatientID As Int64, ByVal sConfigurations As String, ByVal eMessageType As enmMessagetypes, ByVal IsAllPatients As String, Optional ByVal sCCDFilePath As String = "")
        Dim oDBLayer As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Try
            oDBLayer.Connect(False)

            oDBParameters.Clear()

            oDBParameters.Add("@dtDatetimeStamp", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime)

            oDBParameters.Add("@sMachineID", gnClientMachineID.ToString().Trim(), ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sMachinename", gstrClientMachineName, ParameterDirection.Input, SqlDbType.VarChar)


            If eMessageType = enmMessagetypes.email Then
                oDBParameters.Add("@MessageName", "HEALTHVAULT-EMAIL", ParameterDirection.Input, SqlDbType.VarChar)
                oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt)
                oDBParameters.Add("@nOtherID", gnLoginID, ParameterDirection.Input, SqlDbType.BigInt)

            ElseIf eMessageType = enmMessagetypes.data Then
                oDBParameters.Add("@MessageName", "HEALTHVAULT-DATA", ParameterDirection.Input, SqlDbType.VarChar)
                oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt)
                oDBParameters.Add("@nOtherID", gnLoginID, ParameterDirection.Input, SqlDbType.BigInt)
            ElseIf eMessageType = enmMessagetypes.disconnect Then
                oDBParameters.Add("@MessageName", "HEALTHVAULT-DISCONNECT", ParameterDirection.Input, SqlDbType.VarChar)
                oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt)
                oDBParameters.Add("@nOtherID", gnLoginID, ParameterDirection.Input, SqlDbType.BigInt)
            End If
            oDBParameters.Add("@sField1", sConfigurations, ParameterDirection.Input, SqlDbType.VarChar)

            If sCCDFilePath.Length > 0 Then
                oDBParameters.Add("@sField2", sCCDFilePath, ParameterDirection.Input, SqlDbType.VarChar)
            End If

            oDBParameters.Add("@Status", 1, ParameterDirection.Input, SqlDbType.Int)

            oDBParameters.Add("@sServiceName", "gloVaultService", ParameterDirection.Input, SqlDbType.VarChar)
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

    Private Function GetgloVaultConfigurationsId(ByVal sConfigurationName As String) As Int64
        Dim objDbLayer As New gloDatabaseLayer.DBLayer(_sConnectionString)
        Dim nValue As Int64 = 0
        Dim sQuery As String = String.Empty
        Dim objResult As Object
        Try

            If sConfigurationName.Contains("CCD") Then
                sConfigurationName = "CCD"
            End If

            sQuery = "SELECT ISNULL(nModuleId,0) as ModuleId FROM gl_ModulesMst WHERE bIsActive=1 AND sType='gloVault' AND sMODNAME='" & sConfigurationName & "'"

            objDbLayer.Connect(False)

            objResult = objDbLayer.ExecuteScalar_Query(sQuery)

            If objResult IsNot Nothing AndAlso objResult.ToString() <> "" Then
                nValue = Convert.ToInt64(objResult.ToString())
            End If

            objDbLayer.Disconnect()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            nValue = 0
        Finally

            If Not IsNothing(objDbLayer) Then
                objDbLayer.Dispose()
            End If

            objResult = Nothing
            sQuery = String.Empty
        End Try
        Return nValue
    End Function

    Private Sub lstQuestions_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstQuestions.MouseDoubleClick
        Try
            If lstQuestions.Items.Count > 0 Then
                If lstQuestions.SelectedItem IsNot Nothing Then
                    txtQuestions.Text = lstQuestions.SelectedItem.ToString()
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Private Sub lstQuestions_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstQuestions.Leave
        Try
            If lstQuestions.SelectedItem IsNot Nothing Then
                txtQuestions.Text = lstQuestions.SelectedItem.ToString()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Private Sub lstQuestions_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstQuestions.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If lstQuestions.Items.Count > 0 Then
                    If lstQuestions.SelectedItem IsNot Nothing Then
                        txtQuestions.Text = lstQuestions.SelectedItem.ToString()
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Private Sub txtQuestions_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtQuestions.KeyUp
        If e.KeyCode = Keys.Down OrElse e.KeyCode = Keys.Up Then
            lstQuestions.Focus()
        End If
    End Sub

    Private Sub txtQuestions_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQuestions.TextChanged
        Dim sSearchText As String = String.Empty
        Dim dtResult As New DataTable()
        Dim dv As New DataView()

        Try
            sSearchText = txtQuestions.Text
            If sSearchText.Length <= 0 Then
                lstQuestions.Visible = False
                Return
            End If

            dv = dtMyQuestions.DefaultView

            dv.RowFilter = "questions Like '" & sSearchText & "%'"

            dtResult = New DataTable()
            dtResult = dv.ToTable()

            If dtResult Is Nothing OrElse dtResult.Rows.Count <= 0 Then
                lstQuestions.Visible = False
                Return
            End If
            lstQuestions.Items.Clear()

            For i As Integer = 0 To dtResult.Rows.Count - 1
                If dtResult.Rows(i)("questions").ToString() = txtQuestions.Text Then
                    lstQuestions.Visible = False
                    Return
                Else
                    lstQuestions.Items.Add(dtResult.Rows(i)("questions").ToString())
                End If
            Next
            lstQuestions.Visible = True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Private Sub CreateQuestionTable()
        SaveQuestions()
        dtMyQuestions.Columns.Add("questions")
        For Each sQuest As String In sQuestions
            Dim dr As DataRow = dtMyQuestions.NewRow()
            dr("questions") = sQuest
            dtMyQuestions.Rows.Add(dr)
            dr = Nothing
        Next
    End Sub

    Private Sub SaveQuestions()
        sQuestions(0) = "What was the name of your first pet?"
        sQuestions(1) = "What is the middle name of your youngest child?"
        sQuestions(2) = "What school did you attend for sixth grade?"
        sQuestions(3) = "What is your oldest sibling’s birthday month and year?"
        sQuestions(4) = "What was your dream job as a child?"
        sQuestions(5) = "In what city does your nearest sibling live?"
        sQuestions(6) = "What school did you go to?"
        sQuestions(7) = "In what city or town was your first job?"
        sQuestions(8) = "Where did you vacation last year?"
    End Sub

    Private Sub frm_gloVaultMenu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dtConfiguration As New DataTable
        Dim objgloPatient As New gloPatient.gloPatient(_sConnectionString)
        Dim objPatient As gloPatient.Patient = Nothing
        Dim dcId As New DataColumn("ID")
        Dim dcDesc As New DataColumn("Description")
        Dim dialogResult As DialogResult
        Dim nid As Int64 = 0
        Try

            If GetGloVaultSettings() = False Then
                MessageBox.Show("Clinical Exchange settings is turned off, Please contact administrator.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
                Exit Sub
            End If

            'Retrive patient information.
            objPatient = objgloPatient.GetPatient(_nPatientId)

            If objPatient.DemographicsDetail.PatientEmail.Length <= 0 Then
                MessageBox.Show("Selected patient does not have email address.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Me.Close()
                Exit Sub
            End If

            If objPatient.DemographicsDetail.PatientMiddleName.Length > 0 Then
                sPatientName = objPatient.DemographicsDetail.PatientFirstName & " " & objPatient.DemographicsDetail.PatientMiddleName & " " & objPatient.DemographicsDetail.PatientLastName
            Else
                sPatientName = objPatient.DemographicsDetail.PatientFirstName & " " & objPatient.DemographicsDetail.PatientLastName
            End If

            lblPatientName.Text = sPatientName
            lblPatientCode.Text = objPatient.DemographicsDetail.PatientCode
            lblPatientEmail.Text = objPatient.DemographicsDetail.PatientEmail

            ''Check wether this is loaded for email request.
            If _IsEmailRequest = True Then

                Me.Text = "Request Access to Patient"
                Me.Icon = Global.gloEMR.My.Resources.Send_Request_Access
                pnlEmail.BringToFront()
                pnlEmail.Visible = True
                pnInformation.Visible = False
                gloPatient.gloPatient.GetWindowTitle(Me, _nPatientId, GetConnectionString(), gstrMessageBoxCaption)
                ''close form if allerady in queue
                If GetPatinetQueueStatus() Then
                    MessageBox.Show("The selected patient request access is already in queue.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.Close()
                    Exit Sub
                End If

                CreateQuestionTable()
                GetQuestionsFields()

                nid = GetPatientStatus(_nPatientId)

                Select Case nid
                    Case 11
                        If sHealthVaultId.Length > 0 Then
                            dialogResult = MessageBox.Show("The request access email for patient '" & sPatientName & "' was already sent and waiting for approval. Do you want to send the request email/print again.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                            If dialogResult = Windows.Forms.DialogResult.No Then
                                Me.Close()
                            End If
                        End If
                        Exit Select
                    Case 1
                        dialogResult = MessageBox.Show("The HealthVault account access approved from patient '" & sPatientName & "'.You should remove authorization to send the request again?" & vbCrLf & " Note: Response will overwrite exitsting rights.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                        If dialogResult = Windows.Forms.DialogResult.No Then
                            Me.Close()
                        End If
                        tlbRemoveAuthorization.Visible = True
                        tlsDM_Save.Enabled = False
                    Case 12
                        dialogResult = MessageBox.Show("The HealthVault authorization has been removed. Do you want to send the request email/print again.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                        If dialogResult = Windows.Forms.DialogResult.No Then
                            Me.Close()
                        End If
                    Case Else
                End Select
                Me.Width = 484
                Me.Height = 293
            Else
                Me.Icon = Global.gloEMR.My.Resources.Send_Information
                Me.Text = "Update Patient Record"

                Me.Width = 484
                Me.Height = 350

                pnInformation.BringToFront()
                pnInformation.Visible = True
                pnlProcess.Visible = False
                pnlEmail.Visible = False
                gloPatient.gloPatient.GetWindowTitle(Me, _nPatientId, GetConnectionString(), gstrMessageBoxCaption)
                nid = GetPatientStatus(_nPatientId)

                If nid = 1 Then

                    ''Retrive configuration.
                    dtConfiguration = GetgloVaultConfigurations()

                    If IsNothing(dtConfiguration) Then
                        MessageBox.Show("glovault service configurations not found in the system.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.Close()
                        Exit Sub
                    End If

                    For index As Integer = 0 To dtConfiguration.Rows.Count - 1

                        If dtConfiguration.Rows(index)("modname").ToString().ToLower() = "ccd" Then
                            chkLstConfig.Items.Add("Continuity of Care Document(CCD)")
                            Continue For
                        End If

                        If dtConfiguration.Rows(index)("modname").ToString().ToLower() = "demographics" Then
                            chkLstConfig.Items.Add(dtConfiguration.Rows(index)("modname"), True)
                        Else
                            chkLstConfig.Items.Add(dtConfiguration.Rows(index)("modname"))
                        End If
                    Next

                Else

                    Select Case nid
                        Case 0
                            MessageBox.Show("Information: Send information requires patient approval, Please send email request to patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Me.Close()
                            Exit Select
                        Case 11
                            MessageBox.Show("Information: Send information requires patient approval, Waiting for patient approval.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Me.Close()
                            Exit Select
                        Case 3
                            MessageBox.Show("Information: Send information requires patient approval. The previous sent email request was expired, Please send request again.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Me.Close()
                            Exit Select
                        Case 12
                            MessageBox.Show("Information: Send information requires patient approval, Please send email request to patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Me.Close()
                            Exit Select
                        Case 2
                            MessageBox.Show("Information: The HealthVault account access request denied by patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Me.Close()
                            Exit Select
                        Case Else

                    End Select
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally

            If Not IsNothing(dtConfiguration) Then
                dtConfiguration.Dispose()
            End If

            If Not IsNothing(objgloPatient) Then
                objgloPatient.Dispose()
            End If

            If Not IsNothing(objPatient) Then
                objPatient.Dispose()
            End If
        End Try
    End Sub

    Private Function GetPatientStatus(ByVal nSelectedPatientId As Int64) As Int64
        Dim objDbLayer As New gloDatabaseLayer.DBLayer(_sConnectionString)
        Dim sQuery As String = String.Empty
        Dim nValue As Int64 = 0
        Dim objResult As Object
        Try

            sQuery = "select ISNULL(nExternalStatus,0) as nStatus from PatientExternalCodes where sModuleName='HEALTHVAULT' AND sExternalType <> 'HV-LASTSENT' AND nPatientId =" & nSelectedPatientId

            objDbLayer.Connect(False)

            objResult = objDbLayer.ExecuteScalar_Query(sQuery)

            If ((objResult IsNot Nothing)) AndAlso Not String.IsNullOrEmpty(Convert.ToString(objResult)) Then
                nValue = Convert.ToInt64(objResult)
            End If

            objDbLayer.Disconnect()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            nValue = 0
        Finally

            If Not IsNothing(objDbLayer) Then
                objDbLayer.Dispose()
            End If

            objResult = Nothing
            sQuery = String.Empty
        End Try
        Return nValue
    End Function

    Private Sub chkLstConfig_ItemCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles chkLstConfig.ItemCheck

        If chkLstConfig.Items(e.Index).ToString.ToLower() = "demographics" Then
            e.NewValue = CheckState.Checked
        End If

        If chkLstConfig.Items(e.Index).ToString().Contains("CCD") Then
            If chkLstConfig.GetItemCheckState(1) = CheckState.Unchecked Then
                Dim _sValue As String = String.Empty
                _sValue = CheckCCDFilePathAvailability()
                If _sValue.Length > 0 Then
                    If Directory.Exists(_sValue) Then
                        e.NewValue = CheckState.Checked
                    Else
                        MessageBox.Show("Please configure valid CCD file generation path in gloEMR Admin settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        e.NewValue = CheckState.Unchecked
                    End If
                Else
                    MessageBox.Show("Please configure CCD file generation path in gloEMR Admin settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    e.NewValue = CheckState.Unchecked
                End If
            End If
        End If

    End Sub

    Private Sub tlsDM_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsDM_Close.Click
        Me.Close()
    End Sub

    Private Sub tlsDM_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsDM_Save.Click
        Dim dgResult As DialogResult
        Dim oEnce As New clsencryption()

        If _IsEmailRequest Then
            If txtQuestions.Text.Trim().Length <= 0 Then
                MessageBox.Show("Please enter Security question.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            If txtAnswer.Text.Trim().Length <= 0 Then
                MessageBox.Show("Please enter Security answer.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            If chkEmail.CheckState = CheckState.Unchecked AndAlso chkPrint.CheckState = CheckState.Unchecked Then
                MessageBox.Show("Please select at least one notification type.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            dgResult = MessageBox.Show("Do you want to proceed with following question and answer?" & vbCrLf & "Question: " & txtQuestions.Text.Trim() & vbCrLf & "Answer: " & txtAnswer.Text.Trim(), gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            If dgResult = Windows.Forms.DialogResult.No Then
                Return
            End If

            pnlProcess.BringToFront()
            pnlProcess.Visible = True
            Application.DoEvents()

            'Added for disabling the form components.
            DisableFormComponents()

            Dim sAnswer As String = String.Empty
            sAnswer = oEnce.EncryptToBase64String(txtAnswer.Text.Trim(), constEncryptDecryptKey)


            If chkEmail.CheckState = CheckState.Checked AndAlso chkPrint.CheckState = CheckState.Checked Then
                If ProcessEmailRequest(_nPatientId, txtQuestions.Text.Trim(), sAnswer, enmEmailTypes.PrintAndMail) Then
                    Me.Close()
                End If
            ElseIf chkPrint.CheckState = CheckState.Checked Then
                If sHealthVaultId.Length <= 0 Then
                    If ProcessEmailRequest(_nPatientId, txtQuestions.Text.Trim(), sAnswer, enmEmailTypes.OnlyPrint) Then
                        Me.Close()
                    End If
                Else
                    PrintPage(sHealthVaultId)
                    Me.Close()
                End If
            ElseIf chkEmail.CheckState = CheckState.Checked Then
                SendEmailRequest(_nPatientId, txtQuestions.Text.Trim(), sAnswer, enmEmailTypes.OnlyEmail)
                Me.Close()
            End If
        Else
            ''Send Information.
            ''Validation.
            dgResult = MessageBox.Show("Are you sure you want exchange data from gloEMR to Microsoft Healthvault?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)
            If dgResult = Windows.Forms.DialogResult.Cancel Then
                Return
            ElseIf dgResult = Windows.Forms.DialogResult.No Then
                Me.Close()
            ElseIf dgResult = Windows.Forms.DialogResult.Yes Then
                ProcessPatientInformation()
                Me.Close()
            End If
        End If
        pnlProcess.Visible = False
    End Sub
    ''' <summary>
    ''' Method to process email request. based upon the options selected,
    ''' </summary>
    ''' <param name="PatientId"></param>
    ''' <param name="sString1"></param>
    ''' <param name="sString2"></param>
    ''' <param name="sState"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ProcessEmailRequest(ByVal PatientId As Long, ByVal sString1 As String, ByVal sString2 As String, ByVal sState As enmEmailTypes) As Boolean
        Dim _blnResult As Boolean = False
        Dim dtStartedTime As DateTime = Now
        Dim _nId As Long = 0
        Dim _blnStatus As Boolean = False

        Try
            pnlProcess.Visible = True
            Application.DoEvents()
            _nId = SendEmailRequest(PatientId, sString1, sString2, sState)

            'Skip if the process is completed.
            While _blnStatus = False
                ''Check for 25 seconds
                Dim DtDifference As TimeSpan = Nothing
                DtDifference = DateTime.Now - dtStartedTime
                Application.DoEvents()
                If DtDifference.Seconds < 25 Then
                    _blnStatus = CheckQueueState(_nId)
                Else
                    Exit While
                End If
            End While
            Application.DoEvents()
            ''If the process has completed sending value to health vault. then an id will be stored
            ''in patient external codes.
            If _blnStatus Then
                Dim sProcessValue As String = String.Empty
                sProcessValue = RetriveProcessedHealthVaultID(PatientId)
                ''Print the value
                If sProcessValue.Length > 0 AndAlso Not IsNothing(sProcessValue) Then
                    _blnResult = True
                    PrintPage(sProcessValue)
                End If
            Else
                ''Server not responding.
                MessageBox.Show("Currently unable to process the request to health vault, this request is queued  an email will be sent to patient after the process.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                _blnResult = True
            End If
            Application.DoEvents()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
        pnlProcess.Visible = False
        Return _blnResult
    End Function
    ''' <summary>
    ''' Method to retrive processid form patient externcal codes
    ''' </summary>
    ''' <param name="PatientId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RetriveProcessedHealthVaultID(ByVal PatientId As Long) As String
        Dim objDbLayer As New gloDatabaseLayer.DBLayer(_sConnectionString)
        Dim sQuery As String = String.Empty
        Dim objResult As Object
        Try
            sQuery = "Select ISNULL(sExternalSubType,'') as sType From patientexternalcodes where sExternalType IN ('EMAIL','HVSEND') AND  nPatientId=" & PatientId
            objDbLayer.Connect(False)
            objResult = objDbLayer.ExecuteScalar_Query(sQuery)
            objDbLayer.Disconnect()

        Catch ex As Exception
            objResult = Nothing
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If Not IsNothing(objDbLayer) Then
                objDbLayer.Dispose()
            End If
            sQuery = String.Empty
        End Try
        Return Convert.ToString(objResult)
    End Function

    ''' <summary>
    ''' 'Method to retrive queue status.
    ''' </summary>
    ''' <param name="nTranasctionID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckQueueState(ByVal nTranasctionID As Long) As Boolean
        Dim objDbLayer As New gloDatabaseLayer.DBLayer(_sConnectionString)
        Dim blnResult As Boolean = False
        Dim sQuery As String = String.Empty
        Dim objResult As Object
        Try
            sQuery = "select nStatus from gl_messagequeue Where sServiceName='gloVaultService' AND nMessageid =" & nTranasctionID
            objDbLayer.Connect(False)
            objResult = objDbLayer.ExecuteScalar_Query(sQuery)
            objDbLayer.Disconnect()

            If objResult IsNot Nothing AndAlso objResult.ToString() <> "" Then
                If Convert.ToInt64(objResult.ToString()) = 0 Then
                    blnResult = True
                Else
                    blnResult = False
                End If
            End If
        Catch ex As Exception
            blnResult = True
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If Not IsNothing(objDbLayer) Then
                objDbLayer.Dispose()
            End If
            sQuery = String.Empty
        End Try
        Return blnResult
    End Function
    ''' <summary>
    ''' Method to add the message int the queue.
    ''' </summary>
    ''' <param name="PatientId"></param>
    ''' <param name="sString1"></param>
    ''' <param name="sString2"></param>
    ''' <param name="sState"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SendEmailRequest(ByVal PatientId As Long, ByVal sString1 As String, ByVal sString2 As String, ByVal sState As enmEmailTypes) As Long

        Dim objDbLayer As New gloDatabaseLayer.DBLayer(_sConnectionString)
        Dim nId As Int64 = 0
        Dim sQuery As String = String.Empty
        Dim ssQuestion As String = String.Empty
        Dim ssAnswer As String = String.Empty
        Try
            nId = GetUniqueueId()
            If nId <= 0 Then
                Return 0
            End If

            ssQuestion = sString1.Replace("'", "''")
            ssAnswer = sString2.Replace("'", "''")

            If sState = enmEmailTypes.PrintAndMail Then
                sQuery = "INSERT INTO Gl_Messagequeue ([nMessageID],[dtDateTimeStamp],[sMessageName],[sMachineID],[sMachineName] ,[nPatientID],[nOtherID],[nStatus],[sField1] ,[sField2] ,[sServiceName])" & _
                    " VALUES(" & nId & ",'" & DateTime.Now.ToString() & "','PrintAndEMail','" & gnClientMachineID.ToString().Trim() & "','" & gstrClientMachineName & "'," & PatientId & "," & gnLoginID & ",1,'" & ssQuestion & "','" & ssAnswer & "','gloVaultService')"

            ElseIf sState = enmEmailTypes.OnlyPrint Then
                sQuery = "INSERT INTO Gl_Messagequeue ([nMessageID],[dtDateTimeStamp],[sMessageName],[sMachineID],[sMachineName] ,[nPatientID],[nOtherID],[nStatus],[sField1] ,[sField2] ,[sServiceName])" & _
                    " VALUES(" & nId & ",'" & DateTime.Now.ToString() & "','PRINT','" & gnClientMachineID.ToString().Trim() & "','" & gstrClientMachineName & "'," & PatientId & "," & gnLoginID & ",1,'" & ssQuestion & "','" & ssAnswer & "','gloVaultService')"

            ElseIf sState = enmEmailTypes.OnlyEmail Then
                sQuery = "INSERT INTO Gl_Messagequeue ([nMessageID],[dtDateTimeStamp],[sMessageName],[sMachineID],[sMachineName] ,[nPatientID],[nOtherID],[nStatus],[sField1] ,[sField2] ,[sServiceName])" & _
                    " VALUES(" & nId & ",'" & DateTime.Now.ToString() & "','EMAIL','" & gnClientMachineID.ToString().Trim() & "','" & gstrClientMachineName & "'," & PatientId & "," & gnLoginID & ",1,'" & ssQuestion & "','" & ssAnswer & "','gloVaultService')"
            End If

            objDbLayer.Connect(False)
            objDbLayer.Execute_Query(sQuery)
            objDbLayer.Disconnect()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            nId = 0
        Finally
            If Not IsNothing(objDbLayer) Then
                objDbLayer.Dispose()
            End If
            sQuery = String.Empty
        End Try
        Return nId
    End Function
    ''' <summary>
    ''' Method to getunique id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetUniqueueId() As Long
        Dim oDBLayer As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Dim objResult As Object = Nothing
        Try
            oDBLayer.Connect(False)
            oDBParameters.Clear()
            oDBParameters.Add("@ID", "0", ParameterDirection.Output, SqlDbType.BigInt)
            oDBLayer.Execute("gsp_GetUniqueID", oDBParameters, objResult)
            oDBLayer.Disconnect()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If Not IsNothing(oDBLayer) Then
                oDBLayer.Dispose()
            End If
            If Not IsNothing(oDBParameters) Then
                oDBParameters.Dispose()
            End If
        End Try
        Return Convert.ToInt64(objResult)
    End Function
    ''' <summary>
    ''' Method to retrive Clinic Name
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RetriveClinicName() As String
        Dim objDbLayer As New gloDatabaseLayer.DBLayer(_sConnectionString)
        Dim sQuery As String = String.Empty
        Dim objResult As Object
        Try
            sQuery = "Select Top 1 sClinicName From  clinic_mst"
            objDbLayer.Connect(False)
            objResult = objDbLayer.ExecuteScalar_Query(sQuery)
            objDbLayer.Disconnect()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            objResult = Nothing
        Finally
            If Not IsNothing(objDbLayer) Then
                objDbLayer.Dispose()
            End If
            sQuery = String.Empty
        End Try
        Return Convert.ToString(objResult)
    End Function
    ''' <summary>
    ''' Method to getClinicAddress
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetClinicAddress() As StringBuilder
        Dim objDbLayer As New gloDatabaseLayer.DBLayer(_sConnectionString)
        Dim sBValue As New StringBuilder()
        Dim sQuery As String = String.Empty
        Dim dtData As New DataTable
        Try
            sQuery = "Select Top 1 sClinicName,ISNULL(sAddress1,'') as Address1,ISNULL(sStreet,'') as street,ISNULL(sCity,'') as City, ISNULL(sState,'') as sState,ISNULL(sZIP,'') as sZip  From  clinic_mst"
            objDbLayer.Connect(False)
            objDbLayer.Retrive_Query(sQuery, dtData)
            objDbLayer.Disconnect()

            If Not IsNothing(dtData) AndAlso dtData.Rows.Count > 0 Then

                sBValue.AppendLine(dtData.Rows(0)("sClinicName").ToString()).AppendLine()
                If dtData.Rows(0)("Address1").ToString() <> "" Then
                    sBValue.AppendLine(dtData.Rows(0)("Address1").ToString())
                End If
                If dtData.Rows(0)("street").ToString() <> "" Then
                    sBValue.AppendLine(dtData.Rows(0)("street").ToString())
                End If
                If dtData.Rows(0)("City").ToString() <> "" Then
                    sBValue.AppendLine(dtData.Rows(0)("City").ToString())
                End If
                If dtData.Rows(0)("sZip").ToString() <> "" Then
                    sBValue.AppendLine(dtData.Rows(0)("sZip").ToString())
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            sBValue = Nothing
        Finally
            If Not IsNothing(objDbLayer) Then
                objDbLayer.Dispose()
            End If
            sQuery = String.Empty
        End Try
        Return sBValue
    End Function

    ''' <summary>
    ''' Method to print the healthvault id
    ''' </summary>
    ''' <param name="sMicrosoftId"></param>
    ''' <remarks></remarks>
    Private Sub PrintPage(ByVal sMicrosoftId As String)
        Dim sClinicName As String = String.Empty
        Dim smyBuider As New StringBuilder
        Try
            ''Retrive ClinicName
            sClinicName = RetriveClinicName()
            smyBuider = GetClinicAddress()

            If IsNothing(sClinicName) OrElse sClinicName.Length <= 0 Then
                Return
            End If
            sPrintString = New StringBuilder


            If Not IsNothing(smyBuider) Then
                sPrintString.AppendLine(smyBuider.ToString())
            Else
                sPrintString.AppendLine(sClinicName.ToString())
            End If
            sPrintString.AppendLine("------------------------------------------------------------------------------------------------------------------------------------")
            sPrintString.AppendLine()
            sPrintString.Append("Connect with your Health Care provider using Micorsoft® HealthVault™").AppendLine()
            sPrintString.AppendLine()
            sPrintString.Append("Dear " & sPatientName & ",").AppendLine()
            sPrintString.AppendLine()
            sPrintString.Append(sClinicName + " is pleased to offer you the option to exchange information electronically with your healthcare").AppendLine()
            sPrintString.Append("provider. " + sClinicName + " uses Microsoft® HealthVault™ to connect your healthcare provider to your own personal").AppendLine()
            sPrintString.Append("health record and securely store your health information. ").AppendLine()
            sPrintString.AppendLine()
            sPrintString.AppendLine()
            sPrintString.Append("Your identity code is: " & sMicrosoftId).AppendLine()
            sPrintString.AppendLine()
            sPrintString.Append("You will use this code to complete the connection between Microsoft HealthVault and " & sClinicName).AppendLine()
            sPrintString.AppendLine()
            sPrintString.Append("1. Visit the following link: https://account.healthvault.com/patientconnect.aspx").AppendLine()
            sPrintString.Append("2. Follow the HealthVault's instructions to authorize access. ").AppendLine()
            sPrintString.Append("        a. Log into HealthVault").AppendLine()
            sPrintString.Append("        b. Enter your identity code.").AppendLine()
            sPrintString.Append("        c. Answer your identification question.").AppendLine()
            sPrintString.Append("        d. Select the patient record and permissions you wish to allow this connection").AppendLine()
            sPrintString.Append("        e. Approve access to your HealthVault record").AppendLine()
            sPrintString.AppendLine()
            sPrintString.AppendLine("If you have any questions regarding this connection, please contact: gloServiceSupport@glostream.com").AppendLine()
            sPrintString.AppendLine()
            sPrintString.AppendLine()
            sPrintString.AppendLine("Thank you,").AppendLine()
            sPrintString.Append(sClinicName.ToString())
            sPrintString.AppendLine()
            sPrintString.AppendLine("--------------------------------------------------------------------------------------------------------------------------------------").AppendLine()
            sPrintString.AppendLine("                                                                                                                           Powered by gloStream").AppendLine()
            objPrintDocument = New PrintDocument

            AddHandler objPrintDocument.PrintPage, AddressOf Me.objPrintDocument_Print
            objPrintDocument.Print()
            RemoveHandler objPrintDocument.PrintPage, AddressOf Me.objPrintDocument_Print
            objPrintDocument.Dispose()
            objPrintDocument = Nothing

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub
    ''' <summary>
    ''' Event to print document
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub objPrintDocument_Print(ByVal sender As System.Object, ByVal e As PrintPageEventArgs)
        Dim myFont As Font = New Font("Tahoma", 11)
        Try

            e.Graphics.DrawString(sPrintString.ToString(), myFont, Brushes.Black, 50, 100)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            myFont.Dispose()
            myFont = Nothing
        End Try

    End Sub
    ''' <summary>
    ''' Method to retrive fields.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetQuestionsFields()
        Dim objDbLayer As New gloDatabaseLayer.DBLayer(_sConnectionString)
        Dim sQuery As String = String.Empty
        Dim dtREsult As New DataTable
        Try
            sQuery = "SELECT ISNULL(sExternalSubType,'') as VaultId,ISNULL(sExternalDescription,'') as QuestionAns FROM patientExternalCodes where sModuleName='Healthvault' AND sExternalType IN('HVSEND','EMAIL') AND nPatientId=" & _nPatientId
            objDbLayer.Connect(False)
            objDbLayer.Retrive_Query(sQuery, dtREsult)
            objDbLayer.Disconnect()

            If IsNothing(dtREsult) OrElse dtREsult.Rows.Count <= 0 Then
                Return
            End If
            If Not IsNothing(dtREsult.Rows(0)("VaultId")) AndAlso Convert.ToString(dtREsult.Rows(0)("VaultId")).Length > 0 Then
                sHealthVaultId = Convert.ToString(dtREsult.Rows(0)("VaultId"))
            End If

            If Not IsNothing(dtREsult.Rows(0)("QuestionAns")) AndAlso Convert.ToString(dtREsult.Rows(0)("QuestionAns")).Length > 0 Then
                Dim sValue() As String
                sValue = (Convert.ToString(dtREsult.Rows(0)("QuestionAns"))).Split("—")

                If sValue.Length > 0 Then
                    txtAnswer.Text = sValue(1)
                    'txtAnswer.Enabled = False
                    If sValue.Length > 1 Then
                        txtQuestions.Text = sValue(0)
                    End If

                    'txtQuestions.Enabled = False
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If Not IsNothing(objDbLayer) Then
                objDbLayer.Dispose()
            End If
            sQuery = String.Empty
            If Not IsNothing(dtREsult) Then
                dtREsult.Dispose()
            End If
        End Try
    End Sub

    Private Sub tlbRemoveAuthorization_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbRemoveAuthorization.Click
        Dim dialogResult As DialogResult
        dialogResult = MessageBox.Show("Are you sure you want to remove patient HealthVault authorization? Note: No longer you can exchange information to this patient Healthvault account.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
        If dialogResult = Windows.Forms.DialogResult.No Then
            Return
        End If
        InsertMessageQueue(_nPatientId, "", enmMessagetypes.disconnect, "", "")
        Me.Close()
    End Sub

    ''' <summary>
    ''' Get patient message queue status
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetPatinetQueueStatus() As Boolean
        Dim objDbLayer As New gloDatabaseLayer.DBLayer(_sConnectionString)
        Dim sQuery As String = String.Empty
        Dim objResult As Object
        Dim blnResult As Boolean = False
        Try
            sQuery = "SELECT count(*) as MessageStatus  from Gl_Messagequeue Where sMessageName In('PrintAndMail','PRINT','EMAIL','HEALTHVAULT-DISCONNECT') AND sServiceName='gloVaultService' AND nStatus=1 AND nPatientId=" & _nPatientId
            objDbLayer.Connect(False)
            objResult = objDbLayer.ExecuteScalar_Query(sQuery)
            objDbLayer.Disconnect()

            If ((objResult IsNot Nothing)) AndAlso Not String.IsNullOrEmpty(Convert.ToString(objResult)) Then
                If Convert.ToInt64(objResult) > 0 Then
                    blnResult = True
                Else
                    blnResult = False
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            objResult = Nothing
        Finally
            If Not IsNothing(objDbLayer) Then
                objDbLayer.Dispose()
            End If
            sQuery = String.Empty
        End Try
        Return blnResult
    End Function

    Private Sub DisableFormComponents()
        txtAnswer.Enabled = False
        txtQuestions.Enabled = False
        chkEmail.Enabled = False
        chkPrint.Enabled = False
        tlsDM_Save.Enabled = False
        tlsDM_Close.Enabled = False
    End Sub
    Private Sub frm_gloVaultMenu_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

    End Sub
End Class