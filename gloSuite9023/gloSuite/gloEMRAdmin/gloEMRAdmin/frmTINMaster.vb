Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms


Public Class frmTINMaster
    Inherits Form
#Region " Variables "

    Private appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    Private _MessageBoxCaption As String = String.Empty
    Private _databaseconnectionstring As String = "", sGuarantorCode As String = ""

    Private _nTINMasterID As Int64
    Private _nLoginSessionID As Int64
    Private _bIsModify As Boolean
    Private _bIsUsed As Boolean
    Private _bIsActive As Boolean = False

    Private dtProviderAssication As New DataTable()

    Private oListControl As gloListControl.gloListControl = Nothing
    Private _selectedItemsColl As gloGeneralItem.gloItems

    Private oTINMaster As TINMaster
    Private oTINProviderAssociation As TINProviderAssociation
    Private lstProviderList As List(Of TINProviderAssociation)

    Private oToolTip1 As New ToolTip()
#End Region

#Region " C1 Constants "

    'Added By Mahesh Satlapalli (Apollo)
    Private Const COL_AssociationID As Integer = 0
    Private Const COL_TINMasterID As Integer = 1
    Private Const COL_ProviderID As Integer = 2
    Private Const COL_FirstName As Integer = 3
    Private Const COL_MiddleName As Integer = 4
    Private Const COL_LastName As Integer = 5
    Private Const COL_Gender As Integer = 6
    Private Const COL_Status As Integer = 7

#End Region

    Public Sub New()
        InitializeComponent()
        'InitializeComponent
    End Sub

    Public Sub New(databaseconnectionstring As String)
        InitializeComponent()
        _databaseconnectionstring = databaseconnectionstring
    End Sub

    Public Sub New(databaseconnectionstring As String, nTINMasterID As Int64, nLoginSessionID As Int64, Optional IsModify As Boolean = False, Optional IsUsed As Boolean = False)
        InitializeComponent()
        _databaseconnectionstring = databaseconnectionstring
        _nTINMasterID = nTINMasterID
        _nLoginSessionID = nLoginSessionID
        _bIsModify = IsModify
        _bIsUsed = IsUsed
        _MessageBoxCaption = gloGlobal.gloPMGlobal.MessageBoxCaption
    End Sub

    Private Sub btnAddProvider_Click(sender As Object, e As EventArgs) Handles btnAddProvider.Click
        Try
            oListControl = New gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Providers, True, Me.Width, "Tax ID Providers")
            oListControl.ControlHeader = "Tax ID Providers"

            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_PatientSelectedClick
            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
            pnlControl.Controls.Add(oListControl)
            pnlControl.BringToFront()
            pnlTop.Visible = False

            'To allow the user to add multiple guarantors at one time 
            For iPatRow As Int32 = 1 To gvProvider.Rows.Count - 1
                'Added by SaiKrishna:2011-01-06(yyyy-mm-dd) For Existing patient as guarantor(based on patienid)
                If Convert.ToInt64(gvProvider.Rows(iPatRow)(COL_ProviderID).ToString()) > 0 Then
                    oListControl.SelectedItems.Add(Convert.ToInt64(gvProvider.Rows(iPatRow)(COL_ProviderID).ToString()), gvProvider.Rows(iPatRow)(COL_FirstName).ToString())
                End If
            Next

            oListControl.OpenControl()
            If oListControl.IsDisposed = False Then
                oListControl.Dock = DockStyle.Fill
                oListControl.BringToFront()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

#Region " ListControl related Events "

    'Get the exist patients from patient screen
    Private Sub oListControl_PatientSelectedClick(sender As Object, e As EventArgs)
        'Check for Patient exist or not
        Dim exist As Boolean
        Dim dtSelectedPatient As DataTable = Nothing
        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseconnectionstring)
        oDB.Connect(False)

        Try
            '    _selectedItemsColl = new gloGeneralItem.gloItems();

            _selectedItemsColl = oListControl.SelectedItems

            For Each gl As gloGeneralItem.gloItem In _selectedItemsColl
                exist = False
                Dim dRow As DataRow() = dtProviderAssication.[Select]("nProviderID = " + gl.ID.ToString())
                If dRow IsNot Nothing Then
                    If dRow.Length > 0 Then
                        Continue For
                    End If
                End If
                ' If is not required just for safe side
                If exist = False Then
                    'Added by mahesh S (Apollo) , Purpose: To Get Patient data based selected patients from gloListControl
                    Dim _sqlQuery As String = " SELECT 0 AS nAssociationID, PM.nProviderID AS nProviderID, PM.sFirstName AS sFirstName,PM.sMiddleName AS sMiddleName,PM.sLastName AS sLastName,PM.sGender AS sGender  FROM dbo.Provider_MST AS PM WHERE PM.nProviderID= " + gl.ID.ToString()
                    oDB.Retrive_Query(_sqlQuery, dtSelectedPatient)

                    If dtSelectedPatient IsNot Nothing AndAlso dtSelectedPatient.Rows.Count > 0 Then
                        'dtAccountPatient contains the account exist patient List
                        Dim newDr As DataRow = dtProviderAssication.NewRow()
                        newDr("nAssociationID") = dtSelectedPatient.Rows(0)("nAssociationID")
                        newDr("nTINMasterID") = _nTINMasterID
                        newDr("nProviderID") = dtSelectedPatient.Rows(0)("nProviderID")
                        newDr("sFirstName") = dtSelectedPatient.Rows(0)("sFirstName")
                        newDr("sMiddleName") = dtSelectedPatient.Rows(0)("sMiddleName")
                        newDr("sLastName") = dtSelectedPatient.Rows(0)("sLastName")
                        newDr("sGender") = dtSelectedPatient.Rows(0)("sGender")
                        'newDr["LoginSessionID"] = dtSelectedPatient.Rows[0]["bIsActiveAssociation"];
                        newDr("bIsActiveAssociation") = "Active"
                        'newDr["CreatedDate"] = dtSelectedPatient.Rows[0]["CreatedDate"];
                        dtProviderAssication.Rows.Add(newDr)
                        newDr = Nothing
                    End If
                    If dtSelectedPatient IsNot Nothing Then
                        dtSelectedPatient.Dispose()
                    End If
                    dtSelectedPatient = Nothing
                End If
            Next
            FillProviderAssiciation(dtProviderAssication)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            oDB.Disconnect()
            oDB.Dispose()

            If dtSelectedPatient IsNot Nothing Then
                dtSelectedPatient.Dispose()
            End If
            pnlControl.SendToBack()
            pnlTop.Visible = True
        End Try
    End Sub


    Private Sub oListControl_ItemClosedClick(sender As Object, e As EventArgs)
        If oListControl IsNot Nothing Then
            For iControlCnt As Int32 = Me.Controls.Count - 1 To 0 Step -1
                If Me.Controls(iControlCnt).Name = oListControl.Name Then
                    Me.Controls.Remove(Me.Controls(iControlCnt))
                    Exit For
                End If
            Next
            Try

                RemoveHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
            Catch

            End Try
        End If
        pnlControl.SendToBack()
        pnlTop.Visible = True
    End Sub
#End Region
    Private Sub btnDeactivateProvider_Click(sender As Object, e As EventArgs) Handles btnDeactivateProvider.Click
        If gvProvider.RowSel <> -1 Then
            If btnDeactivateProvider.Text = "Deactivate Provider" Then
                'Confirmation message when account have only one active patient.
                If ConfirmDeactivateStatus() Then
                    gvProvider.Rows(gvProvider.RowSel)(COL_Status) = "Deactive"
                    btnDeactivateProvider.Text = "Activate Provider"
                    btnDeactivateProvider.Tag = "Activate Provider"
                End If
            Else
                gvProvider.Rows(gvProvider.RowSel)(COL_Status) = "Active"
                btnDeactivateProvider.Text = "Deactivate Provider"
                btnDeactivateProvider.Tag = "Deactivate Provider"
            End If
        End If
    End Sub

    Private Sub gvProvider_Click(sender As Object, e As EventArgs) Handles gvProvider.Click
        If gvProvider.RowSel <> -1 Then
            If gvProvider.Rows(gvProvider.RowSel)(COL_Status).ToString().ToLower() = "deactive" Then
                btnDeactivateProvider.Text = "Activate Provider"
                btnDeactivateProvider.Tag = "Activate Provider"
            Else
                btnDeactivateProvider.Text = "Deactivate Provider"
                btnDeactivateProvider.Tag = "Deactivate Provider"
            End If
        End If

    End Sub

#Region " Private Methods "
    Private Function ConfirmDeactivateStatus() As Boolean
        If Not ValidStatus() Then
            Dim res As DialogResult = MessageBox.Show("Provider will be deactivated from this TIN Association." + Environment.NewLine + "This Association has no other active provider.", _MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
            If res = DialogResult.Cancel Then
                Return False
            End If
        End If
        Return True
    End Function

    Private Function ValidStatus() As Boolean
        Dim activeStatusCnt As Int32 = 0

        For Each oRow As C1.Win.C1FlexGrid.Row In gvProvider.Rows
            If oRow(COL_Status).ToString().ToUpper() = "ACTIVE" Then
                activeStatusCnt = activeStatusCnt + 1
            End If
            ' last patient record with active status
            If activeStatusCnt > 1 Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub SetData()

        FillData()
        FillGrid()
    End Sub

    ' fill the form controls.
    Private Sub FillData()
        Dim dtTINInfo As New DataTable()
        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseconnectionstring)
        Dim oParameters As New gloDatabaseLayer.DBParameters()

        oDB.Connect(False)

        Try
            oParameters.Add("@nTINMasterID", _nTINMasterID, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Retrive("gsp_GetTINInfo", oParameters, dtTINInfo)

            If dtTINInfo IsNot Nothing AndAlso dtTINInfo.Rows.Count > 0 Then
                txtTINNo.Text = Convert.ToString(dtTINInfo.Rows(0)("sTIN"))
                txtTINTitle.Text = Convert.ToString(dtTINInfo.Rows(0)("sTINTitle"))
                _bIsActive = Convert.ToBoolean(dtTINInfo.Rows(0)("bIsActive"))
            Else
                txtTINNo.Clear()
                txtTINTitle.Clear()
                _bIsActive = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            oDB.Disconnect()
            oDB.Dispose()
            oParameters.Dispose()

            If dtTINInfo IsNot Nothing Then
                dtTINInfo.Dispose()
            End If
        End Try

    End Sub

    ' display patients on Grid.
    Private Sub FillGrid()
        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseconnectionstring)
        Dim oParameters As New gloDatabaseLayer.DBParameters()

        Try
            oDB.Connect(False)
            oParameters.Add("@nTINMasterID", _nTINMasterID, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Retrive("gsp_GetProvider_TIN_Association", oParameters, dtProviderAssication)
            FillProviderAssiciation(dtProviderAssication)

            'loading time change the "btnDeactivateProvider" Text.
            ProviderActivateDeActivate()

            oDB.Disconnect()
        Catch ex As gloDatabaseLayer.DBException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Catch gex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gex.ToString(), True)
        Finally
            oDB.Dispose()
            oParameters.Dispose()
        End Try
    End Sub

    'Design Grid 
    Private Sub FillProviderAssiciation(dtProvider As DataTable)
        gvProvider.DataSource = dtProvider

        gvProvider.Cols(COL_AssociationID).Caption = "nAssociationID"
        gvProvider.Cols(COL_TINMasterID).Caption = "nTINMasterID"
        gvProvider.Cols(COL_ProviderID).Caption = "nProviderID"
        gvProvider.Cols(COL_FirstName).Caption = "First Name"
        gvProvider.Cols(COL_MiddleName).Caption = "MI"
        gvProvider.Cols(COL_LastName).Caption = "Last Name"
        gvProvider.Cols(COL_Gender).Caption = "Gender"
        gvProvider.Cols(COL_Status).Caption = "Status"

        gvProvider.Cols(COL_AssociationID).Visible = False
        gvProvider.Cols(COL_TINMasterID).Visible = False
        gvProvider.Cols(COL_ProviderID).Visible = False
        gvProvider.Cols(COL_FirstName).Width = 150
        gvProvider.Cols(COL_MiddleName).Width = 40
        gvProvider.Cols(COL_LastName).Width = 150
        gvProvider.Cols(COL_Gender).Width = 90
        gvProvider.Cols(COL_Status).Width = 80

        gvProvider.ScrollBars = ScrollBars.Both
    End Sub

    'btnDeactivateProvider button text change at form load.
    Private Sub ProviderActivateDeActivate()
        If gvProvider.Rows.Count > 1 Then
            If gvProvider.Rows(1)(COL_Status).ToString().ToLower() = "deactive" Then
                btnDeactivateProvider.Text = "Activate Provider"
                btnDeactivateProvider.Tag = "Activate Provider"
            Else
                btnDeactivateProvider.Text = "Deactivate Provider"
                btnDeactivateProvider.Tag = "DeActivate Provider"
            End If
        End If
    End Sub

#End Region

    Private Sub frmTINMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            SetData()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Sub ts_Commands_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ts_Commands.ItemClicked
        Try
            Select Case e.ClickedItem.Tag.ToString()
                Case "Save"
                    If GetData() = True Then
                        SaveData()
                        Me.Close()
                    End If
                    Exit Select
                Case "Cancel"
                    Dim res As DialogResult = MessageBox.Show("Do you want to save changes to this record? ", _MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)
                    If res = DialogResult.Yes Then
                        If GetData() = True Then
                            SaveData()
                            Me.Close()
                        End If
                    ElseIf res = DialogResult.No Then
                        Me.Close()
                    ElseIf res = DialogResult.Cancel Then
                        Return
                    End If
                    Exit Select

            End Select
        Catch ex As gloDatabaseLayer.DBException
            ex.ERROR_Log(ex.ToString())
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Sub SaveData()
        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseconnectionstring)
        Dim oDBParameter As New gloDatabaseLayer.DBParameters()

        Try
            oDB.Connect(False)
            If oTINMaster IsNot Nothing Then
                If Not _bIsModify Then
                    Dim dtTINUsed As DataTable = Nothing
                    oDBParameter.Clear()
                    oDBParameter.Add("@sTIN", oTINMaster.TIN, ParameterDirection.Input, SqlDbType.VarChar)
                    oDB.Retrive("gsp_Verify_TINInPresent", oDBParameter, dtTINUsed)
                    If dtTINUsed IsNot Nothing AndAlso dtTINUsed.Rows.Count > 0 Then
                        MessageBox.Show("TIN is already present in system. Please enter other TIN.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return
                    End If
                End If
            End If

            oDBParameter.Clear()
            Dim tinId As Object
            oDBParameter.Add("@nTINMasterID", oTINMaster.TINMasterID, ParameterDirection.InputOutput, SqlDbType.BigInt)
            oDBParameter.Add("@sTIN", oTINMaster.TIN, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameter.Add("@sTINTitle", oTINMaster.TINTitle, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameter.Add("@bIsActive", oTINMaster.IsActive, ParameterDirection.Input, SqlDbType.Bit)
            oDBParameter.Add("@bIsDeleted", oTINMaster.IsDeleted, ParameterDirection.Input, SqlDbType.Bit)
            oDBParameter.Add("@LoginSessionID", oTINMaster.LoginSessionID, ParameterDirection.Input, SqlDbType.BigInt)

            oDB.Execute("gsp_INUP_TINInformation", oDBParameter, tinId)

            For Each item As TINProviderAssociation In lstProviderList
                oDBParameter.Clear()
                oDBParameter.Add("@nAssociationID", item.AssociationID, ParameterDirection.Input, SqlDbType.BigInt)
                oDBParameter.Add("@nTINMasterID", If(item.TINMasterID = 0, tinId, item.TINMasterID), ParameterDirection.Input, SqlDbType.BigInt)
                oDBParameter.Add("@nProviderID", item.ProviderID, ParameterDirection.Input, SqlDbType.BigInt)
                oDBParameter.Add("@LoginSessionID", item.LoginSessionID, ParameterDirection.Input, SqlDbType.BigInt)
                oDBParameter.Add("@bIsActiveAssociation", item.IsActiveAssociation, ParameterDirection.Input, SqlDbType.Bit)

                oDB.ExecuteWithTransaction("gsp_INUP_TINProviderAssociation", oDBParameter)
            Next
        Catch ex As gloDatabaseLayer.DBException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Catch gex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gex.ToString(), True)
        Finally

            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If
            If oDBParameter IsNot Nothing Then
                oDBParameter.Dispose()
                oDBParameter = Nothing
            End If
        End Try
    End Sub

    Private Function GetData() As Boolean
        If ValidateData() Then
            PrepareTINInfoAndProviderList()
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub PrepareTINInfoAndProviderList()
        lstProviderList = New List(Of TINProviderAssociation)()
        oTINMaster = New TINMaster()
        oTINMaster.TINMasterID = _nTINMasterID
        oTINMaster.TIN = txtTINNo.Text.Trim()
        oTINMaster.TINTitle = txtTINTitle.Text.Trim()
        oTINMaster.IsActive = _bIsActive
        oTINMaster.IsDeleted = False
        oTINMaster.LoginSessionID = _nLoginSessionID

        If dtProviderAssication.Rows.Count > 0 Then
            For Each dr As DataRow In dtProviderAssication.Rows
                oTINProviderAssociation = New TINProviderAssociation()
                oTINProviderAssociation.AssociationID = IIf(IsNothing(dr("nAssociationID")) = True, Convert.ToInt64("0"), Convert.ToInt64(dr("nAssociationID").ToString()))
                oTINProviderAssociation.TINMasterID = IIf(IsNothing(dr("nTINMasterID")) = True, Convert.ToInt64("0"), Convert.ToInt64(dr("nTINMasterID").ToString()))
                oTINProviderAssociation.ProviderID = IIf(IsNothing(dr("nProviderID")) = True, Convert.ToInt64("0"), Convert.ToInt64(dr("nProviderID").ToString()))
                oTINProviderAssociation.LoginSessionID = _nLoginSessionID
                If dr("bIsActiveAssociation").ToString() = "Active" Then
                    oTINProviderAssociation.IsActiveAssociation = True
                Else
                    oTINProviderAssociation.IsActiveAssociation = False
                End If
                lstProviderList.Add(oTINProviderAssociation)
                oTINProviderAssociation = Nothing
            Next
        End If
    End Sub

    Private Function ValidateData() As Boolean
        If txtTINNo.Text.Trim().Length = 0 Then
            MessageBox.Show("Enter TIN#.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtTINNo.Focus()
            Return False
        End If
        If txtTINTitle.Text.Trim().Length = 0 Then
            MessageBox.Show("Enter TIN Title.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtTINTitle.Focus()
            Return False
        End If
        If gvProvider.Rows.Count <= 1 Then
            MessageBox.Show("Select provider to associate with TaxID.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            btnAddProvider.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub txtTINNo_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtTINNo.KeyPress
        If _bIsUsed Then
            MessageBox.Show("TaxID is associated with provider(s) transaction, you can not edit. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            e.Handled = True
        End If
    End Sub
End Class

Public Class TINProviderAssociation
    Public Property AssociationID() As Int64
        Get
            Return m_AssociationID
        End Get
        Set(value As Int64)
            m_AssociationID = Value
        End Set
    End Property
    Private m_AssociationID As Int64

    Public Property TINMasterID() As Int64
        Get
            Return m_TINMasterID
        End Get
        Set(value As Int64)
            m_TINMasterID = Value
        End Set
    End Property
    Private m_TINMasterID As Int64

    Public Property ProviderID() As Int64
        Get
            Return m_ProviderID
        End Get
        Set(value As Int64)
            m_ProviderID = Value
        End Set
    End Property
    Private m_ProviderID As Int64

    Public Property LoginSessionID() As Int64
        Get
            Return m_LoginSessionID
        End Get
        Set(value As Int64)
            m_LoginSessionID = Value
        End Set
    End Property
    Private m_LoginSessionID As Int64

    Public Property IsActiveAssociation() As Boolean
        Get
            Return m_IsActiveAssociation
        End Get
        Set(value As Boolean)
            m_IsActiveAssociation = Value
        End Set
    End Property
    Private m_IsActiveAssociation As Boolean

    Public Property CreatedDate() As DateTime
        Get
            Return m_CreatedDate
        End Get
        Set(value As DateTime)
            m_CreatedDate = Value
        End Set
    End Property
    Private m_CreatedDate As DateTime
End Class

Public Class TINMaster
    Public Property TINMasterID() As Int64
        Get
            Return m_TINMasterID
        End Get
        Set(value As Int64)
            m_TINMasterID = Value
        End Set
    End Property
    Private m_TINMasterID As Int64

    Public Property TIN() As String
        Get
            Return m_TIN
        End Get
        Set(value As String)
            m_TIN = Value
        End Set
    End Property
    Private m_TIN As String

    Public Property TINTitle() As String
        Get
            Return m_TINTitle
        End Get
        Set(value As String)
            m_TINTitle = Value
        End Set
    End Property
    Private m_TINTitle As String

    Public Property IsActive() As Boolean
        Get
            Return m_IsActive
        End Get
        Set(value As Boolean)
            m_IsActive = Value
        End Set
    End Property
    Private m_IsActive As Boolean

    Public Property IsDeleted() As Boolean
        Get
            Return m_IsDeleted
        End Get
        Set(value As Boolean)
            m_IsDeleted = Value
        End Set
    End Property
    Private m_IsDeleted As Boolean

    Public Property LoginSessionID() As Int64
        Get
            Return m_LoginSessionID
        End Get
        Set(value As Int64)
            m_LoginSessionID = Value
        End Set
    End Property
    Private m_LoginSessionID As Int64

    Public Property CreatedDate() As DateTime
        Get
            Return m_CreatedDate
        End Get
        Set(value As DateTime)
            m_CreatedDate = Value
        End Set
    End Property
    Private m_CreatedDate As DateTime

    Public Property ModifiedDate() As DateTime
        Get
            Return m_ModifiedDate
        End Get
        Set(value As DateTime)
            m_ModifiedDate = Value
        End Set
    End Property
    Private m_ModifiedDate As DateTime
End Class
