Imports gloEMRGeneralLibrary
'Imports gloUserControlLibrary
Imports System.Data.SqlClient

Public Class gloUC_LabOrderDetail


    Enum SendToType
        Lab = 1
        Physician = 2
    End Enum



    Dim _ONPrx As String = ""

    'Developer:Sanjog Dhamke
    'Date: 12/07/2011
    'Bug ID/PRD Name/Sales force Case: SF Case = GLO2011-0015430 - Lab Results Not Displaying Properly
    'Reason: _ONId,_OrderNumberID is converted to int16 format but actual _ONId,_OrderNumberID is bigger than this so it cause the exception and the lab result is not displaying properly. Now we make this as int64 data type.
    Dim _ONId As Int64 = 0

    Private _OrderNumberPrefix As String
    Private _OrderNumberID As Int64



   


    Private _PreferredLab As String
    Private _PreferredLabID As Int64


    Private _ReferredTo As String
    Private _ReferredToID As Int64
    Private _SendTo As Int32 = SendToType.Lab

    Private _ReferredBy As String
    Private _ReferredByID As Int64
    Private _SampledBy As String
    Private _SampledByID As Int64
    Private _Users As New gloEMRActors.LabActor.ItemDetails


    'sarika Labs Denormalization 20090317
    'Private _PreferredLabName As String = ""
    'Private _SampledByName As String = ""
    Private _ReferredByFName As String = ""
    Private _ReferredByMName As String = ""
    Private _ReferredByLName As String = ""
    Private _ClinicID As Int64 = 0
    '----


    '' 20071121 Mahesh
    Private _TaskDescription As String
    Private _TaskDueDate As Date
    ''
    Private _Ht As Int64 = 140
    'sarika 4th july 07
    '  Dim pnl As New System.Windows.Forms.Panel
    Private WithEvents oC1flex As gloUC_CustomSearchInC1Flexgrid

    Public intStatus As Int16
    Dim dtID As DataTable
    ' Private objgloEMRLab As gloEMRGeneralLibrary.gloEMRLab.gloEMRLabContactInfo

    '-----------

    'sarika 13th oct 07
    Public Event Lab_btnUC_ADDclick(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal intStatus As Int16)
    Public Event Lab_btnUC_Modifyclick(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal intStatus As Int16, ByVal nEditID As Int64)

    'sarika 16th oct 07
    '  Public searchdt As String = ""
    Public searchdt As Long = 0
    '--------
    Dim colno As Integer
    '-------------------------

    '' Sudhir 20090210
    Dim ofrmList As frmViewListControl

    Private oListControl As gloListControl.gloListControl
    Private oListUsers As gloListControl.gloListControl
    Private ToList As gloGeneralItem.gloItems
    '' 
    Dim dtUsers As New DataTable()
    ''Sandip Darade 20090730
    Dim _MessageBoxCaption As String = "gloEMR"
    Dim oListPreferredLab As gloListControl.gloListControl
    Dim oListReferredBy As gloListControl.gloListControl
    Dim oListReferredTo As gloListControl.gloListControl
    Dim _IsSampledBy As Boolean = False
    ''dhruv 20091207
    Dim _IsOrderModified As Boolean = False
    Dim _IsLoading As Boolean = False
    'Madan added on 2010517
    Dim _IsPanelClosed As Boolean = False
    Dim _OrderLabType As String = ""
    Dim _isOrderSelected As Boolean = False
    Dim _IsPreferredIDCleared As Boolean = False
    'When ever this control is opened from viewlabform from emdoen interface...
    'The task description should be allways visible by it it should not allow any thing to edit.
    Dim _IsOpenedFromViewLab As Boolean = False
    Dim dtTaskUsers As DataTable = Nothing

#Region "Order Detail Properties"
    ''Modified by madan on 20100902 from readonly to normal.. property.
    Public Property OrderSelected() As Boolean
        Get
            Return _isOrderSelected
        End Get
        Set(ByVal value As Boolean)
            _isOrderSelected = value
        End Set
    End Property
    Public Property OrderModified() As Boolean
        Get
            Return _IsOrderModified
        End Get
        Set(ByVal value As Boolean)
            _IsOrderModified = value
        End Set
    End Property
    Public Property OrderNumberPrefix() As String
        Get
            Return _OrderNumberPrefix
        End Get
        Set(ByVal value As String)
            _OrderNumberPrefix = value
        End Set
    End Property
   

    'Developer:Sanjog Dhamke
    'Date: 12/07/2011
    'Bug ID/PRD Name/Sales force Case: SF Case = GLO2011-0015430 - Lab Results Not Displaying Properly
    'Reason: OrderNoID is converted to int16 format but actual OrderNumberID is bigger than this so it cause the exception and the lab result is not displaying properly. Now we make this as int64 data type.
    Public Property OrderNumberID() As Int64
        Get
            Return _OrderNumberID
        End Get
        Set(ByVal value As Int64)
            _OrderNumberID = value
        End Set
    End Property

    Public Property PreferredLab() As String
        Get
            Return _PreferredLab
        End Get
        Set(ByVal value As String)
            _PreferredLab = value
        End Set
    End Property

    Public Property PreferredLabID() As Int64
        Get
            Return _PreferredLabID
        End Get
        Set(ByVal value As Int64)
            _PreferredLabID = value
        End Set
    End Property
    Public Property OrderLabType() As String
        Get
            Return _OrderLabType
        End Get
        Set(ByVal value As String)
            _OrderLabType = value
        End Set
    End Property

    Public Property IsPreferredIDCleared() As String
        Get
            Return _IsPreferredIDCleared
        End Get
        Set(ByVal value As String)
            _IsPreferredIDCleared = value
        End Set
    End Property

    Public Property SendTo() As Int32
        Get
            Return _SendTo
        End Get
        Set(ByVal value As Int32)
            _SendTo = value
        End Set
    End Property

    Public Property ReferredTo() As String
        Get
            Return _ReferredTo
        End Get
        Set(ByVal value As String)
            _ReferredTo = value
        End Set
    End Property

    Public Property ReferredToID() As Int64
        Get
            Return _ReferredToID
        End Get
        Set(ByVal value As Int64)
            _ReferredToID = value
        End Set
    End Property


    Public Property ReferredBy() As String
        Get
            Return _ReferredBy
        End Get
        Set(ByVal value As String)
            _ReferredBy = value
        End Set
    End Property

    Public Property ReferredByID() As Int64
        Get
            Return _ReferredByID
        End Get
        Set(ByVal value As Int64)
            _ReferredByID = value
        End Set
    End Property

    Public Property SampledBy() As String
        Get
            Return _SampledBy
        End Get
        Set(ByVal value As String)
            _SampledBy = value
        End Set
    End Property

    Public Property SampledByID() As Int64
        Get
            Return _SampledByID
        End Get
        Set(ByVal value As Int64)
            _SampledByID = value
        End Set
    End Property

    Public Property Users() As gloEMRActors.LabActor.ItemDetails
        Get
            Return _Users
        End Get
        Set(ByVal value As gloEMRActors.LabActor.ItemDetails)
            _Users = value
        End Set
    End Property
    '' 20071121 Mahesh
    Public Property TaskDescription() As String
        Get
            Return _TaskDescription
        End Get
        Set(ByVal value As String)
            _TaskDescription = value
        End Set
    End Property

    Public Property TaskDueDate() As Date
        Get
            Return _TaskDueDate
        End Get
        Set(ByVal value As Date)
            _TaskDueDate = value
        End Set
    End Property

    ''

    ''sarika Labs Denormalization 20090317
    'Public Property PreferredLabName() As String
    '    Get
    '        Return _PreferredLabName
    '    End Get
    '    Set(ByVal value As String)
    '        _PreferredLabName = value
    '    End Set
    'End Property

    'Public Property SampledByName() As String
    '    Get
    '        Return _SampledByName
    '    End Get
    '    Set(ByVal value As String)
    '        _SampledByName = value
    '    End Set
    'End Property

    Public Property ReferredByFName() As String
        Get
            Return _ReferredByFName
        End Get
        Set(ByVal value As String)
            _ReferredByFName = value
        End Set
    End Property

    Public Property ReferredByMName() As String
        Get
            Return _ReferredByMName
        End Get
        Set(ByVal value As String)
            _ReferredByMName = value
        End Set
    End Property

    Public Property ReferredByLName() As String
        Get
            Return _ReferredByLName
        End Get
        Set(ByVal value As String)
            _ReferredByLName = value
        End Set
    End Property

    Public Property ClinicID() As Int64
        Get
            Return _ClinicID
        End Get
        Set(ByVal value As Int64)
            _ClinicID = value
        End Set
    End Property
    '--------------
    'Madan added on 20100517
    Public ReadOnly Property IsPanelClosed() As Boolean
        Get
            Return _IsPanelClosed
        End Get
    End Property
    Public Property IsOpenedFromViewLab() As Boolean
        Get
            Return _IsOpenedFromViewLab
        End Get
        Set(ByVal value As Boolean)
            _IsOpenedFromViewLab = value
        End Set
    End Property
    'End Madan
#End Region
    ''Added to dispose datatable on control disposed
    Private Sub gloUC_LabOrderDetail_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        If Not IsNothing(dtTaskUsers) Then
            dtTaskUsers.Dispose()
            dtTaskUsers = Nothing
        End If
    End Sub

    '// 1.START <<<<<<<<<<<<<----------CONTROL BASIC CODING---------->>>>>>>>>>>>>>>>> //

    Private Sub gloUC_LabOrderDetail_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnUp.Visible = True
        btnUp.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.UP
        btnUp.BackgroundImageLayout = ImageLayout.Center
        btnDown.Visible = False

        'Size
        On Error Resume Next

        Me.Height = _Ht  ' 322

        On Error Resume Next
        Dim _Width As Single = 0
        pnlPD_RefBySamBy.BorderStyle = Windows.Forms.BorderStyle.None
        '' 20071121 Mahesh 
        '' if User are Selected then Only show the Panel of Task Description 
        If cmbAssignedTo.Items.Count > 0 Then
            pnlPD_TaskDescDueDate.Visible = True
            ' pnlPD_SplitterTasks.Visible = True

            pnlPatientDetail.Height = pnlPD_OrderNumber.Height + pnlPD_PrefLabAssTask.Height + pnlPD_TaskDescDueDate.Height + pnlPD_RefBySamBy.Height + 10
            Me.Height = _Ht + pnlPD_TaskDescDueDate.Height + 5
        Else

            If _IsOpenedFromViewLab = True Then

                txtTaskDesc.Enabled = False
                dtTaskDueDate.Enabled = False
                pnlPD_TaskDescDueDate.Visible = False

                pnlPatientDetail.Height = pnlPD_OrderNumber.Height + pnlPD_PrefLabAssTask.Height + pnlPD_TaskDescDueDate.Height + pnlPD_RefBySamBy.Height + 10
                Me.Height = _Ht + pnlPD_TaskDescDueDate.Height + 5
            Else
                pnlPD_TaskDescDueDate.Visible = False
                ' pnlPD_SplitterTasks.Visible = False
                pnlPatientDetail.Height = pnlPD_OrderNumber.Height + pnlPD_PrefLabAssTask.Height + pnlPD_RefBySamBy.Height + 10
                Me.Height = _Ht + 5
            End If
        End If
        ''

        '_Width = Me.Width
        'pnlPD_PrefLabAssTask_PrefLab.Width = _Width / 2
        'pnlPD_PrefLabAssTask_AssTask.Width = _Width / 2

        '_Width = Me.Width
        'pnlPD_RefBySamBy_RefBy.Width = _Width / 2
        'pnlPD_RefBySamBy_SamBy.Width = _Width / 2
    End Sub

    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click
        pnlPatientDetail.Visible = False
        btnUp.Visible = False
        btnDown.Visible = True
        btnDown.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Down
        btnDown.BackgroundImageLayout = ImageLayout.Center
        'Size
        On Error Resume Next
        ' pnlPatientDetail.Height = 0
        Me.Height = 33
        'Madan added on 20100517
        _IsPanelClosed = True
        'End Madan 
    End Sub

    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click
        pnlPatientDetail.Visible = True
        btnUp.Visible = True
        btnUp.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.UP
        btnUp.BackgroundImageLayout = ImageLayout.Center
        btnDown.Visible = False
        'Size
        On Error Resume Next

        '' 20071121 Mahesh 
        '' if User are Selected then Only show the Panel of Task Description 
        If cmbAssignedTo.Items.Count > 0 Then
            pnlPD_TaskDescDueDate.Visible = True
            '            pnlPD_SplitterTasks.Visible = True
            '' 
            pnlPatientDetail.Height = pnlPD_OrderNumber.Height + pnlPD_PrefLabAssTask.Height + pnlPD_TaskDescDueDate.Height + pnlPD_RefBySamBy.Height + 10
            Me.Height = _Ht + pnlPD_TaskDescDueDate.Height + 5
        Else

            If _IsOpenedFromViewLab Then
                pnlPD_TaskDescDueDate.Visible = True
                txtTaskDesc.Enabled = False
                dtTaskDueDate.Enabled = False

                pnlPatientDetail.Height = pnlPD_OrderNumber.Height + pnlPD_PrefLabAssTask.Height + pnlPD_TaskDescDueDate.Height + pnlPD_RefBySamBy.Height + 10
                Me.Height = _Ht + pnlPD_TaskDescDueDate.Height + 5

            Else
                pnlPD_TaskDescDueDate.Visible = False
                pnlPatientDetail.Height = pnlPD_OrderNumber.Height + pnlPD_PrefLabAssTask.Height + pnlPD_RefBySamBy.Height + 10
                Me.Height = _Ht + 5
            End If
        End If
        'Madan added on 20100517
        _IsPanelClosed = False
        'End Madan 
    End Sub



    'Developer:Sanjog Dhamke
    'Date: 12/07/2011
    'Bug ID/PRD Name/Sales force Case: SF Case = GLO2011-0015430 - Lab Results Not Displaying Properly
    'Reason: OrderNoID is converted to int16 format but actual OrderNoID is bigger than this so it cause the exception and the lab result is not displaying properly. Now we make this as int64 data type.
    Public Function SetData(ByVal OrderNumberPrefix As String, ByVal OrderNumberID As Int64, ByVal PreferredLab As String, ByVal ReferredBy As String, ByVal SampledBy As String, ByVal Users As gloEMRActors.LabActor.ItemDetails, ByVal PreferredLabID As Int64, ByVal ReferredByID As Int64, ByVal SampledByID As Int64, ByVal TaskDesc As String, ByVal TaskDueDate As Date, ByVal ReferredToID As Int64, ByVal ReferredTo As String, ByVal SendTo As Int32) As Boolean
        Try
            _IsLoading = True

            _ONPrx = OrderNumberPrefix
            _ONId = OrderNumberID

            _OrderNumberPrefix = OrderNumberPrefix
            _OrderNumberID = OrderNumberID

            _PreferredLab = PreferredLab
            _ReferredBy = ReferredBy
            _SampledBy = SampledBy
            _PreferredLabID = PreferredLabID

            _SendTo = SendTo
            _ReferredToID = ReferredToID
            _ReferredTo = ReferredTo

            _ReferredByID = ReferredByID
            _SampledByID = SampledByID




            If _Users Is Nothing Then
                _Users = New gloEMRActors.LabActor.ItemDetails
            End If

            _Users = Users

            _TaskDescription = TaskDesc
            _TaskDueDate = TaskDueDate

            lblOrderNumber.Text = _OrderNumberPrefix & "-" & _OrderNumberID

            txtPreferredBy.Text = ""
            txtPreferredBy.Tag = ""
            txtReferredBy.Text = ""
            txtReferredBy.Tag = ""
            txtSampledBy.Text = ""
            txtSampledBy.Tag = ""

            If _SendTo = SendToType.Lab Then
                rbSendToLab.Checked = True
            ElseIf _SendTo = SendToType.Physician Then
                rbSendToPhysician.Checked = True
            End If


            If _SendTo = SendToType.Lab Then
                If Not PreferredLab.Trim = "" Then
                    txtPreferredBy.Text = PreferredLab
                    txtPreferredBy.Tag = PreferredLabID
                End If
            ElseIf _SendTo = SendToType.Physician Then
                If Not ReferredTo.Trim = "" Then
                    txtPreferredBy.Text = ReferredTo
                    txtPreferredBy.Tag = ReferredToID
                End If
            End If


            If Not _ReferredBy.Trim = "" Then
                'cmbReferredBy.Text = _ReferredBy
                'For i As Int16 = 0 To cmbReferredBy.Items.Count - 1
                '    cmbReferredBy.SelectedItem = cmbReferredBy.Items(i)
                '    If _ReferredBy = cmbReferredBy.SelectedItem.Row.ItemArray(1) Then
                '        cmbReferredBy.SelectedIndex = i
                '        Exit For
                '    End If
                'Next
                'sarika 29th may 07
                'Else
                '    cmbReferredBy.SelectedIndex = -1
                '------------------

                txtReferredBy.Text = _ReferredBy
                txtReferredBy.Tag = _ReferredByID
            End If

            If Not _SampledBy.Trim = "" Then
                '    cmbSampledBy.Text = _SampledBy
                '    For i As Int16 = 0 To cmbSampledBy.Items.Count - 1
                '        cmbSampledBy.SelectedItem = cmbSampledBy.Items(i)
                '        If _SampledBy = cmbSampledBy.SelectedItem.Row.ItemArray(1) Then
                '            cmbSampledBy.SelectedIndex = i
                '            Exit For
                '        End If
                '    Next
                '    'sarika 29th may 07
                'Else
                '    cmbSampledBy.SelectedIndex = -1
                '    '------------------
                txtSampledBy.Text = _SampledBy
                txtSampledBy.Tag = _SampledByID
            End If



            ''Sudhir

            cmbAssignedTo.DataSource = Nothing
            cmbAssignedTo.Items.Clear()
            Try
                If (IsNothing(ToList) = False) Then
                    ToList.Dispose()
                    ToList = Nothing
                End If
            Catch ex As Exception

            End Try
            ToList = New gloGeneralItem.gloItems

            If Not _Users Is Nothing Then
                Dim ToItem As gloGeneralItem.gloItem
                For i As Int16 = 0 To _Users.Count - 1
                    ToItem = New gloGeneralItem.gloItem()

                    ToItem.ID = _Users.Item(i).ID
                    ToItem.Description = _Users.Item(i).Description
                    ToItem.Code = _Users.Item(i).Code

                    ToList.Add(ToItem)
                    ToItem.Dispose()
                    ToItem = Nothing
                Next
                If _Users.Count > 0 Then
                    ''To Fill ComboBox of Users 
                    FillAssignedToCombo()
                End If
            End If
            ''

            If cmbAssignedTo.Items.Count > 0 Then

                If _IsOpenedFromViewLab = True Then
                    dtTaskDueDate.Enabled = True
                    txtTaskDesc.Enabled = True
                End If

                txtTaskDesc.Text = _TaskDescription.Trim
                If _TaskDueDate <> "#12:00:00 AM#" Then ''Condition By Sudhir - 20090207 - Default Date Value Cannot be Formated.
                    dtTaskDueDate.Value = Format(_TaskDueDate, "MM/dd/yyyy hh:mm tt")
                End If
            Else

                If _IsOpenedFromViewLab = True Then
                    pnlPD_TaskDescDueDate.Visible = True
                    txtTaskDesc.Text = ""
                    txtTaskDesc.Enabled = False
                    dtTaskDueDate.Enabled = False

                    pnlPatientDetail.Height = pnlPD_OrderNumber.Height + pnlPD_PrefLabAssTask.Height + pnlPD_TaskDescDueDate.Height + pnlPD_RefBySamBy.Height + 10
                    Me.Height = _Ht + pnlPD_TaskDescDueDate.Height + 5

                Else
                    pnlPD_TaskDescDueDate.Visible = False
                    Me.Height = _Ht
                End If
            End If
            'Added by madan on 20100521-- for fixing issue in labs
            If IsPanelClosed = True Then
                Me.Height = 33
            End If

            '' 
            _IsLoading = False
            Return True

        Catch ex As Exception
            MessageBox.Show("Error on UserListControl" & ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function



    Public Sub ReadData()
        _OrderNumberPrefix = ""
        _OrderNumberID = 0
        _PreferredLab = ""
        _PreferredLabID = 0
        _ReferredTo = ""
        _ReferredToID = 0
        _ReferredBy = ""
        _ReferredByID = 0
        _SampledBy = ""
        _SampledByID = 0
        _TaskDescription = ""
        If Not _Users Is Nothing Then
            _Users.Clear()
        Else
            _Users = New gloEMRActors.LabActor.ItemDetails
        End If


        _OrderNumberPrefix = _ONPrx
        _OrderNumberID = _ONId


        'code commented by sarika 4th july 07
        'If Not cmbPreferredLab.SelectedItem Is Nothing Then
        '    _PreferredLab = cmbPreferredLab.SelectedItem.Row.ItemArray(1)
        '    _PreferredLabID = cmbPreferredLab.SelectedItem.Row.ItemArray(0)
        'End If
        If (rbSendToLab.Checked = True) Then
            _SendTo = SendToType.Lab
        ElseIf (rbSendToPhysician.Checked = True) Then
            _SendTo = SendToType.Physician
        End If

        If _SendTo = SendToType.Lab Then
            _PreferredLab = txtPreferredBy.Text
            If Not txtPreferredBy.Tag Is Nothing Then
                If Not txtPreferredBy.Tag.ToString.Trim = "" Then
                    _PreferredLabID = txtPreferredBy.Tag
                End If
            End If
        ElseIf (_SendTo = SendToType.Physician) Then
            _ReferredTo = txtPreferredBy.Text
            If Not txtPreferredBy.Tag Is Nothing Then
                If Not txtPreferredBy.Tag.ToString.Trim = "" Then
                    _ReferredToID = txtPreferredBy.Tag
                End If
            End If
        End If

        'If Not cmbReferredBy.SelectedItem Is Nothing Then
        '_ReferredBy = cmbReferredBy.SelectedItem.Row.ItemArray(1)
        '_ReferredByID = cmbReferredBy.SelectedItem.Row.ItemArray(0)
        'End If

        _ReferredBy = txtReferredBy.Text
        If Not txtReferredBy.Tag Is Nothing Then
            If Not txtReferredBy.Tag.ToString.Trim = "" Then
                _ReferredByID = txtReferredBy.Tag
            End If
        End If

        'sarika Labs Denormalization 20090318
        'get the referred by firstname , middlename and lastname using  _ReferredByID
        If _ReferredByID > 0 Then
            SetReferredByName(_ReferredByFName, _ReferredByMName, _ReferredByLName, _ReferredByID)
        Else
            _ReferredByFName = ""
            _ReferredByMName = ""
            _ReferredByLName = ""
        End If

        ' MsgBox(_ReferredByFName)
        ' MsgBox(_ReferredByMName)
        ' MsgBox(_ReferredByLName)
        '---



        'If Not cmbSampledBy.SelectedItem Is Nothing Then
        '    _SampledBy = cmbSampledBy.SelectedItem.Row.ItemArray(1)
        '    _SampledByID = cmbSampledBy.SelectedItem.Row.ItemArray(0)
        'End If

        _SampledBy = txtSampledBy.Text
        If Not txtSampledBy.Tag Is Nothing Then
            If Not txtSampledBy.Tag.ToString.Trim = "" Then
                _SampledByID = txtSampledBy.Tag
            End If
        End If



        If Not _Users Is Nothing Then
            If cmbAssignedTo.Items.Count > 0 Then
                Dim _UserID As Long
                Dim _UserCode As String
                Dim _UserName As String
                Dim _ResultType As String
                For i As Int16 = 0 To cmbAssignedTo.Items.Count - 1
                    ''Sudhir 20090205
                    _UserName = ToList(i).Description
                    _UserID = ToList(i).ID
                    _UserCode = ""
                    _ResultType = ToList(i).Code ''Result Type
                    _Users.Add(_UserID, _ResultType, _UserName)

                    _UserName = ""
                    _UserID = 0
                    _UserCode = ""
                    _ResultType = ""

                Next
                _TaskDescription = txtTaskDesc.Text.Trim
                _TaskDueDate = Format(dtTaskDueDate.Value, "MM/dd/yyyy hh:mm tt")
            End If
        End If

    End Sub


    'sarika Labs Denormalization 20090318
    Public Sub SetReferredByName(ByRef RFName As String, ByRef RMName As String, ByRef RLName As String, ByVal ReferredByID As Int64)
        Dim oDB As New gloEMRDatabase.DataBaseLayer
        Dim dt As DataTable = Nothing
        Dim _strSQL As String = ""

        Try

            '   oNumber = Val(oDB.GetRecord_Query("select isnull(MAX(labom_OrderNoID),0) + 1 from Lab_Order_MST WHERE labom_OrderNoPrefix = '" & oPrefix & "'") & "")
            _strSQL = "select   isnull(sFirstName,'')AS FirstName, isnull(sMiddleName,'') As MiddleName, isnull(sLastName,'') As LastName  FROM Contacts_MST where nContactID = " & ReferredByID  ' (isnull(sFirstName,'') + ' ' + isnull(sMiddleName,'') + ' ' + isnull(sLastName,'')) AS ContactName  from Contacts_MST where sContactType = 'Physician' AND nContactID IS NOT NULL ORDER BY sFirstName, sMiddleName, sLastName"
            dt = oDB.GetDataTable_Query(_strSQL)
            RFName = dt.Rows(0)("FirstName")
            RMName = dt.Rows(0)("MiddleName")
            RLName = dt.Rows(0)("LastName")

        Catch ex As Exception
            Throw ex
        Finally
            If (IsNothing(dt) = False) Then
                dt.Dispose()
                dt = Nothing
            End If

            oDB.Dispose()
            oDB = Nothing

        End Try
    End Sub
    '----



    Public Function SetNewOrderNumber(Optional ByVal Prefix As String = "ORD") As Boolean
        _OrderNumberPrefix = ""
        _OrderNumberID = 0
        lblOrderNumber.Text = ""
        rbSendToLab.Checked = True

        'Developer:Sanjog Dhamke
        'Date: 12/07/2011
        'Bug ID/PRD Name/Sales force Case: SF Case = GLO2011-0015430 - Lab Results Not Displaying Properly
        'Reason: OrderNoID is converted to int16 format but actual OrderNoID is bigger than this so it cause the exception and the lab result is not displaying properly. Now we make this as int64 data type.
        Dim oNumber As Int64

        Dim oPrefix As String = ""
        If oPrefix.Trim = "" Then
            oPrefix = "ORD"
        End If

        Dim oDB As New gloEMRDatabase.DataBaseLayer
        oNumber = Val(oDB.GetRecord_Query("select isnull(MAX(labom_OrderNoID),0) + 1 from Lab_Order_MST WHERE labom_OrderNoPrefix = '" & oPrefix & "'") & "")
        ' oDB = Nothing

        _ONPrx = oPrefix
        _ONId = oNumber

        _OrderNumberPrefix = oPrefix
        _OrderNumberID = oNumber

        lblOrderNumber.Text = _OrderNumberPrefix & "-" & _OrderNumberID
        oDB.Dispose()
        oDB = Nothing
        Return False
    End Function

    'code commented by sarika 4th july 07
    'Private Sub txtSearchUser_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        'search for the user in the chkLstUsers check list box.
    '        For i As Integer = 0 To chkLstUsers.Items.Count - 1
    '            Dim str As String
    '            str = UCase(Trim(chkLstUsers.Items.Item(i).ToString))
    '            If Mid(str, 1, Len(Trim(txtSearchUser.Text))) = UCase(Trim(txtSearchUser.Text)) Then
    '                chkLstUsers.SelectedIndex = i
    '                Exit For
    '            End If
    '        Next

    '        txtSearchUser.Focus()
    '        txtSearchUser.Select()
    '    Catch ex As Exception
    '    End Try
    'End Sub

    Private Function GetOrderContactInformations(ByVal ContactType As gloEMRActors.LabActor.enumContactType) As DataTable
        Dim _gloEMRDatabase As New gloEMRDatabase.DataBaseLayer
        Dim dt As DataTable
        Dim _strSQL As String = ""

        Try
            Select Case ContactType
                Case gloEMRActors.LabActor.enumContactType.PreferredLab
                    _strSQL = "select labci_Id AS ContactID, labci_ContactName AS ContactName from Lab_ContactInfo where labci_Type=" & CInt(ContactType) & " AND labci_Id IS NOT NULL"
                Case gloEMRActors.LabActor.enumContactType.ReferredBy
                    _strSQL = "select nContactID AS ContactID, isnull(sFirstName,'')AS FirstName, isnull(sMiddleName,'') As MiddleName, isnull(sLastName,'') As LastName ,isnull(sStreet,'') As Street, isnull(sCity,'') As City,isnull(sState,'') As State FROM Contacts_MST where sContactType = 'Physician' AND nContactID IS NOT NULL ORDER BY sFirstName, sMiddleName, sLastName" ' (isnull(sFirstName,'') + ' ' + isnull(sMiddleName,'') + ' ' + isnull(sLastName,'')) AS ContactName  from Contacts_MST where sContactType = 'Physician' AND nContactID IS NOT NULL ORDER BY sFirstName, sMiddleName, sLastName"
                    '_strSQL = "select labci_Id AS ContactID, (isnull(labci_FirstName,'') + ' ' + isnull(labci_MiddleName,'') + ' ' + isnull(labci_LastName,'')) AS ContactName  from Lab_ContactInfo where labci_Type=" & CInt(ContactType) & " AND labci_Id IS NOT NULL"
                Case gloEMRActors.LabActor.enumContactType.SampledBy
                    _strSQL = "select nUserID AS ContactID, (isnull(sLoginName,'')) AS UserName,(isnull(sFirstName,'') + ' ' + isnull(sMiddleName,'') + ' ' + isnull(sLastName,'')) AS Name from User_MST where nUserID IS NOT NULL ORDER BY sLoginName"
                    '_strSQL = "select labci_Id AS ContactID, (isnull(labci_FirstName,'') + ' ' + isnull(labci_MiddleName,'') + ' ' + isnull(labci_LastName,'')) AS ContactName from Lab_ContactInfo where labci_Type=" & CInt(ContactType) & " AND labci_Id IS NOT NULL"
            End Select

            dt = _gloEMRDatabase.GetDataTable_Query(_strSQL)
            Return dt
        Catch ex As Exception
            Return Nothing
        Finally
            _gloEMRDatabase.Dispose()
            _gloEMRDatabase = Nothing


        End Try

    End Function

    '// 1.FINISH <<<<<<<<<<<<<----------CONTROL BASIC CODING---------->>>>>>>>>>>>>>>>> //

    '// 2.START <<<<<<<<<<<<<----------BUTTON SEARCH/CLOSE CODING---------->>>>>>>>>>>>>>>>> //

#Region "Preferred Labs"

    Private Sub btnSearchPL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchPL.Click
        'intStatus = 1
        'Call load_c1flexControlData()
        'Me.Height = 322 + 10

        ''Above code commented by Sandip Darade 
        ''Added glolist control 
        Try
            If rbSendToLab.Checked = True Then
                ofrmList = New frmViewListControl
                ofrmList.Text = "Preferred/Performing Labs"
                oListPreferredLab = New gloListControl.gloListControl(gloEMRDatabase.DataBaseLayer.ConnectionString, gloListControl.gloListControlType.PreferredLab, False, ofrmList.Width)
                oListPreferredLab.ControlHeader = "Preferred Labs"


                AddHandler oListPreferredLab.ItemSelectedClick, AddressOf oListPreferredLab_ItemSelectedClick
                AddHandler oListPreferredLab.ItemClosedClick, AddressOf oListPreferredLab_ItemClosedClick

                ofrmList.Controls.Add(oListPreferredLab)
                oListPreferredLab.Dock = DockStyle.Fill
                oListPreferredLab.BringToFront()
                oListPreferredLab.OpenControl()
                oListPreferredLab.ShowHeaderPanel(False)
                ofrmList.ShowDialog(IIf(IsNothing(ofrmList.Parent), Me, ofrmList.Parent))

                If Not IsNothing(oListPreferredLab.SelectedItems) Then
                    If oListPreferredLab.SelectedItems.Count > 0 Then

                        Dim objfrmViewNormalLab As Object

                        objfrmViewNormalLab = Me.ParentForm
                        _isOrderSelected = True

                        LoadTests(objfrmViewNormalLab, oListPreferredLab.SelectedItems(0).ID)

                        'If (objfrmViewNormalLab.Name = "frmViewNormalLab") Then
                        '    'If (_OrderLabType = "") Then
                        '    '    objfrmViewNormalLab.FillTests_NEW(oListPreferredLab.SelectedItems(0).ID)
                        '    'Else
                        '    If (_OrderLabType = "") OrElse (gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.LabTests.ToString() = _OrderLabType) Then
                        '        'objfrmViewNormalLab.FillTests_NEW(oListPreferredLab.SelectedItems(0).ID)
                        '        objfrmViewNormalLab.FillTestsByType(gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.LabTests, oListPreferredLab.SelectedItems(0).ID)

                        '    ElseIf (gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.RadiologyImaging.ToString() = _OrderLabType) Then
                        '        'objfrmViewNormalLab.FillRadiologyImagingTests(oListPreferredLab.SelectedItems(0).ID)
                        '        objfrmViewNormalLab.FillTestsByType(gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.RadiologyImaging, oListPreferredLab.SelectedItems(0).ID)

                        '    ElseIf (gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.Other.ToString() = _OrderLabType) Then
                        '        'objfrmViewNormalLab.FillOthers(oListPreferredLab.SelectedItems(0).ID)
                        '        objfrmViewNormalLab.FillTestsByType(gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.Other, oListPreferredLab.SelectedItems(0).ID)

                        '    ElseIf (gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.Groups.ToString() = _OrderLabType) Then
                        '        objfrmViewNormalLab.FillGroups_NEW(oListPreferredLab.SelectedItems(0).ID)

                        '    ElseIf (gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.Referrals.ToString() = _OrderLabType) Then
                        '        'objfrmViewNormalLab.FillRefTests(oListPreferredLab.SelectedItems(0).ID)
                        '        objfrmViewNormalLab.FillTestsByType(gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.Referrals, oListPreferredLab.SelectedItems(0).ID)

                        '    End If
                        'End If
                        ''End If

                        _PreferredLabID = oListPreferredLab.SelectedItems(0).ID
                    End If
                End If

                If IsNothing(ofrmList) = False Then

                    RemoveHandler oListPreferredLab.ItemSelectedClick, AddressOf oListPreferredLab_ItemSelectedClick
                    RemoveHandler oListPreferredLab.ItemClosedClick, AddressOf oListPreferredLab_ItemClosedClick
                    ofrmList.Controls.Remove(oListPreferredLab)
                    oListPreferredLab.Dispose()
                    oListPreferredLab = Nothing
                    ofrmList.Dispose()
                End If



            ElseIf rbSendToPhysician.Checked = True Then
                ofrmList = New frmViewListControl
                ofrmList.Text = "Referred to"
                oListReferredTo = New gloListControl.gloListControl(gloEMRDatabase.DataBaseLayer.ConnectionString, gloListControl.gloListControlType.Physicians, False, ofrmList.Width)
                oListReferredTo.ControlHeader = "Referred to"


                AddHandler oListReferredTo.ItemSelectedClick, AddressOf oListReferredTo_ItemSelectedClick
                AddHandler oListReferredTo.ItemClosedClick, AddressOf oListReferredTo_ItemClosedClick
                AddHandler oListReferredTo.AddFormHandlerClick, AddressOf oListReferredTo_AddFormHandlerClick
                AddHandler oListReferredTo.ModifyFormHandlerClick, AddressOf oListReferredTo_ModifyFormHandlerClick

                ofrmList.Controls.Add(oListReferredTo)
                oListReferredTo.Dock = DockStyle.Fill
                oListReferredTo.BringToFront()
                oListReferredTo.OpenControl()
                oListReferredTo.ShowHeaderPanel(False)
                ofrmList.ShowDialog(IIf(IsNothing(ofrmList.Parent), Me, ofrmList.Parent))

                If IsNothing(ofrmList) = False Then
                    RemoveHandler oListReferredTo.ItemSelectedClick, AddressOf oListReferredTo_ItemSelectedClick
                    RemoveHandler oListReferredTo.ItemClosedClick, AddressOf oListReferredTo_ItemClosedClick
                    RemoveHandler oListReferredTo.AddFormHandlerClick, AddressOf oListReferredTo_AddFormHandlerClick
                    RemoveHandler oListReferredTo.ModifyFormHandlerClick, AddressOf oListReferredTo_ModifyFormHandlerClick
                    ofrmList.Controls.Remove(oListReferredTo)
                    oListReferredTo.Dispose()
                    oListReferredTo = Nothing
                    ofrmList.Dispose()
                End If

            End If

        Catch ex As Exception
            MessageBox.Show("Error on ListControl" & ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub oListPreferredLab_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)

        Try
            If oListPreferredLab.SelectedItems.Count > 0 Then
                txtPreferredBy.Text = oListPreferredLab.SelectedItems(0).Description
                txtPreferredBy.Tag = oListPreferredLab.SelectedItems(0).ID

            End If
            ofrmList.Close()
        Catch ex As Exception
            MessageBox.Show("Error on UserListControl" & ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub oListPreferredLab_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)
        ofrmList.Close()
    End Sub

    Private Sub btnClearPL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearPL.Click
        'Commented by Mayuri:20091027
        'To Fix Bug:#4310
        'If Trim(txtPreferredBy.Text) <> "" And txtPreferredBy.Tag <> Nothing Then '' And txtPreferredBy.Tag <> 0 Then
        'End Code Commented by Mayuri:20091027
        If Trim(txtPreferredBy.Text) <> "" OrElse txtPreferredBy.Tag <> Nothing Then '' And txtPreferredBy.Tag <> 0 Then
            Dim strMsg As String = String.Empty
            If SendTo = SendToType.Physician Then
                strMsg = "Are you sure you want to clear referred To name?"
            Else
                strMsg = "Are you sure you want to clear preferred lab name?"
            End If

            Dim objfrmViewNormalLab As Object
            objfrmViewNormalLab = Me.ParentForm
            If MessageBox.Show(strMsg, _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                txtPreferredBy.Text = ""
                txtPreferredBy.Tag = 0
                ReferredTo = String.Empty
                ReferredToID = 0

                If (objfrmViewNormalLab.Name = "frmViewNormalLab") Then
                    objfrmViewNormalLab.IsPreferredLabCleared = True
                    _isOrderSelected = False

                    LoadTests(objfrmViewNormalLab)

                    ''If (_OrderLabType = "") Then
                    ''    objfrmViewNormalLab.FillTests_NEW()
                    ''Else
                    'If (_OrderLabType = "") OrElse (gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.LabTests.ToString() = _OrderLabType) Then
                    '    'objfrmViewNormalLab.FillTests_NEW()
                    '    objfrmViewNormalLab.FillTestsByType(gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.LabTests)

                    'ElseIf (gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.RadiologyImaging.ToString() = _OrderLabType) Then
                    '    'objfrmViewNormalLab.FillRadiologyImagingTests()
                    '    objfrmViewNormalLab.FillTestsByType(gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.RadiologyImaging)

                    'ElseIf (gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.Other.ToString() = _OrderLabType) Then
                    '    'objfrmViewNormalLab.FillOthers()
                    '    objfrmViewNormalLab.FillTestsByType(gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.Other)

                    'ElseIf (gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.Groups.ToString() = _OrderLabType) Then
                    '    objfrmViewNormalLab.FillGroups_NEW()

                    'ElseIf (gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.Referrals.ToString() = _OrderLabType) Then
                    '    'objfrmViewNormalLab.FillRefTests()
                    '    objfrmViewNormalLab.FillTestsByType(gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.Referrals)

                    'End If
                    ''End If

                Else
                    Exit Sub
                End If
            End If

        End If
    End Sub

#End Region
#Region "Referred By"
    Private Sub btnSearchRB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchRB.Click
        'intStatus = 2
        'Call load_c1flexControlData()
        'Me.Height = 322 + 10
        ''Above code commented by Sandip Darade 20090630
        ''Added glolist control 
        ''the lab is referred  by physian  so glolist control added to browse Physicians
        Try
            ofrmList = New frmViewListControl
            ofrmList.Text = "Referred by"
            oListReferredBy = New gloListControl.gloListControl(gloEMRDatabase.DataBaseLayer.ConnectionString, gloListControl.gloListControlType.Physicians, False, ofrmList.Width)
            oListReferredBy.ControlHeader = "Referred by"


            AddHandler oListReferredBy.ItemSelectedClick, AddressOf oListReferredBy_ItemSelectedClick
            AddHandler oListReferredBy.ItemClosedClick, AddressOf oListReferredBy_ItemClosedClick
            AddHandler oListReferredBy.AddFormHandlerClick, AddressOf oListReferredBy_AddFormHandlerClick
            AddHandler oListReferredBy.ModifyFormHandlerClick, AddressOf oListReferredBy_ModifyFormHandlerClick

            ofrmList.Controls.Add(oListReferredBy)
            oListReferredBy.Dock = DockStyle.Fill
            oListReferredBy.BringToFront()
            oListReferredBy.OpenControl()
            oListReferredBy.ShowHeaderPanel(False)
            ofrmList.ShowDialog(IIf(IsNothing(ofrmList.Parent), Me, ofrmList.Parent))

            If IsNothing(ofrmList) = False Then
                RemoveHandler oListReferredBy.ItemSelectedClick, AddressOf oListReferredBy_ItemSelectedClick
                RemoveHandler oListReferredBy.ItemClosedClick, AddressOf oListReferredBy_ItemClosedClick
                RemoveHandler oListReferredBy.AddFormHandlerClick, AddressOf oListReferredBy_AddFormHandlerClick
                RemoveHandler oListReferredBy.ModifyFormHandlerClick, AddressOf oListReferredBy_ModifyFormHandlerClick
                ofrmList.Controls.Remove(oListReferredBy)
                oListReferredBy.Dispose()
                oListReferredBy = Nothing
                ofrmList.Dispose()
            End If
        Catch ex As Exception
            MessageBox.Show("Error on ListControl" & ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''Added by Mayuri:-on 20110124-New,modify functionality on physicians-
    Private Sub oListReferredBy_AddFormHandlerClick(ByVal sender As Object, ByVal e As EventArgs)
        If oListReferredBy.ControlHeader = "Referred by" Then
            Dim ofrmAddContact As New gloContacts.frmSetupPhysician(gloEMRDatabase.DataBaseLayer.ConnectionString)
            ofrmAddContact.ShowDialog(IIf(IsNothing(ofrmAddContact.Parent), Me, ofrmAddContact.Parent))
            If ofrmAddContact.DialogResult = DialogResult.OK Then

                oListReferredBy.FillListAsCriteria(ofrmAddContact.ContactID)


            End If
            ofrmAddContact.Dispose()
        End If
    End Sub
    Private Sub oListReferredBy_ModifyFormHandlerClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim _contactid As Int64 = 0
        Dim _sSPIID As String = String.Empty
        If oListReferredBy.ControlHeader = "Referred by" Then
            If Not IsNothing(oListReferredBy.dgListView.CurrentRow) Then
                _contactid = Convert.ToInt64(oListReferredBy.dgListView("nContactID", oListReferredBy.dgListView.CurrentRow.Index).Value)
                _sSPIID = Convert.ToString(oListReferredBy.dgListView("sSPI", oListReferredBy.dgListView.CurrentRow.Index).Value)
            End If
            If oListReferredBy.dgListView.Rows.Count <> 0 Then
                Dim ofrmModifyContact As New gloContacts.frmSetupPhysician(_contactid, gloEMRDatabase.DataBaseLayer.ConnectionString)
                If _sSPIID = "" Then
                    ofrmModifyContact.CallFrom = "Physician"
                Else
                    ofrmModifyContact.CallFrom = "Direct Physician"
                End If
                ofrmModifyContact.ShowDialog(IIf(IsNothing(ofrmModifyContact.Parent), Me, ofrmModifyContact.Parent))
                If ofrmModifyContact.DialogResult = DialogResult.OK Then

                    oListReferredBy.FillListAsCriteria(ofrmModifyContact.ContactID)


                End If
                ofrmModifyContact.Dispose()
            End If
        End If
    End Sub

    Private Sub btnClearRB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearRB.Click

        If Trim(txtReferredBy.Text) <> "" OrElse txtPreferredBy.Tag <> Nothing Then
            If MessageBox.Show("Are you sure you want to clear referred by name?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                txtReferredBy.Text = ""
                txtReferredBy.Tag = 0
            End If
        End If

    End Sub
    Private Sub oListReferredBy_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        Try


            If oListReferredBy.SelectedItems.Count > 0 Then

                txtReferredBy.Text = oListReferredBy.SelectedItems(0).Description
                txtReferredBy.Tag = oListReferredBy.SelectedItems(0).ID
            End If

            ofrmList.Close()

        Catch ex As Exception
            MessageBox.Show("Error on UserListControl" & ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub oListReferredBy_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)
        ofrmList.Close()
    End Sub

    Private Sub oListReferredTo_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        Try
            ReferredTo = ""
            ReferredToID = 0

            If oListReferredTo.SelectedItems.Count > 0 Then

                txtPreferredBy.Text = oListReferredTo.SelectedItems(0).Description
                txtPreferredBy.Tag = oListReferredTo.SelectedItems(0).ID
                ReferredTo = oListReferredTo.SelectedItems(0).Description
                ReferredToID = Convert.ToInt64(oListReferredTo.SelectedItems(0).ID)
            End If

            ofrmList.Close()

        Catch ex As Exception
            MessageBox.Show("Error on UserListControl" & ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub oListReferredTo_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)
        ofrmList.Close()
    End Sub

    Private Sub oListReferredTo_AddFormHandlerClick(ByVal sender As Object, ByVal e As EventArgs)
        If oListReferredTo.ControlHeader = "Referred to" Then
            Dim ofrmAddContact As New gloContacts.frmSetupPhysician(gloEMRDatabase.DataBaseLayer.ConnectionString)
            ofrmAddContact.ShowDialog(IIf(IsNothing(ofrmAddContact.Parent), Me, ofrmAddContact.Parent))
            If ofrmAddContact.DialogResult = DialogResult.OK Then

                oListReferredTo.FillListAsCriteria(ofrmAddContact.ContactID)


            End If
            ofrmAddContact.Dispose()
        End If
    End Sub
    Private Sub oListReferredTo_ModifyFormHandlerClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim _contactid As Int64 = 0
        If oListReferredTo.ControlHeader = "Referred to" Then
            If Not IsNothing(oListReferredTo.dgListView.CurrentRow) Then
                _contactid = Convert.ToInt64(oListReferredTo.dgListView("nContactID", oListReferredTo.dgListView.CurrentRow.Index).Value)
            End If
            If oListReferredTo.dgListView.Rows.Count <> 0 Then
                Dim ofrmModifyContact As New gloContacts.frmSetupPhysician(_contactid, gloEMRDatabase.DataBaseLayer.ConnectionString)
                ofrmModifyContact.ShowDialog(IIf(IsNothing(ofrmModifyContact.Parent), Me, ofrmModifyContact.Parent))
                If ofrmModifyContact.DialogResult = DialogResult.OK Then

                    oListReferredTo.FillListAsCriteria(ofrmModifyContact.ContactID)


                End If
                ofrmModifyContact.Dispose()
            End If
        End If
    End Sub

#End Region
#Region "Sampled by"
    Private Sub btnSearchSB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchSB.Click
        'intStatus = 3
        'Call load_c1flexControlData()
        'Me.Height = 322 + 10
        ''Above code commented by Sandip Darade 20090630
        ''Added glolist control 
        ''the lab is samples by user so glolist control added to browse users
        Try
            _IsSampledBy = True
            ofrmList = New frmViewListControl
            ofrmList.Text = "Sampled by"
            oListUsers = New gloListControl.gloListControl(gloEMRDatabase.DataBaseLayer.ConnectionString, gloListControl.gloListControlType.Users, False, ofrmList.Width)
            oListUsers.ControlHeader = "Sampled by"

            AddHandler oListUsers.ItemSelectedClick, AddressOf oListUsers_ItemSelectedClick
            AddHandler oListUsers.ItemClosedClick, AddressOf oListUsers_ItemClosedClick

            ofrmList.Controls.Add(oListUsers)
            oListUsers.Dock = DockStyle.Fill
            oListUsers.BringToFront()
            oListUsers.OpenControl()
            oListUsers.ShowHeaderPanel(False)
            ofrmList.ShowDialog(IIf(IsNothing(ofrmList.Parent), Me, ofrmList.Parent))

            If IsNothing(ofrmList) = False Then
                RemoveHandler oListUsers.ItemSelectedClick, AddressOf oListUsers_ItemSelectedClick
                RemoveHandler oListUsers.ItemClosedClick, AddressOf oListUsers_ItemClosedClick
                ofrmList.Controls.Remove(oListUsers)
                oListUsers.Dispose()
                oListUsers = Nothing
                ofrmList.Dispose()
            End If
        Catch ex As Exception
            MessageBox.Show("Error on UserListControl" & ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub btnClearSB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSB.Click
        If Trim(txtSampledBy.Text) <> "" AndAlso txtSampledBy.Tag <> Nothing Then ''txtSampledBy.Tag <> 0 Then
            If MessageBox.Show("Are you sure you want to clear sampled by name?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                txtSampledBy.Text = ""
                txtSampledBy.Tag = 0
            End If
        End If
    End Sub
#End Region



    Private Sub btnSearchAssTo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchAssTo.Click
        ''Comment by Sudhir 20090210 - Old UserList Panel.
        'intStatus = 4
        'Call load_c1flexControlData()
        'Me.Height = 322 + 10
        ''Exit Sub

        ''New ListControl on Form.
        Try

            _IsSampledBy = False
            ofrmList = New frmViewListControl
            ofrmList.Text = "Users"
            oListUsers = New gloListControl.gloListControl(gloEMRDatabase.DataBaseLayer.ConnectionString, gloListControl.gloListControlType.Users, True, ofrmList.Width)
            oListUsers.ControlHeader = "Users"

            AddHandler oListUsers.ItemSelectedClick, AddressOf oListUsers_ItemSelectedClick
            AddHandler oListUsers.ItemClosedClick, AddressOf oListUsers_ItemClosedClick

            ''To Select already Added Users.
            'If IsNothing(ToList) = False Then
            '    For i As Integer = 0 To ToList.Count - 1
            '        oListUsers.SelectedItems.Add(ToList(i))
            '    Next
            'End If
            ''

            ofrmList.Controls.Add(oListUsers)
            oListUsers.Dock = DockStyle.Fill
            oListUsers.BringToFront()

            If Not IsNothing(cmbAssignedTo.DataSource) Then
                If cmbAssignedTo.Items.Count > 0 Then
                    dtTaskUsers = cmbAssignedTo.DataSource
                    If IsNothing(dtTaskUsers) = False Then
                        If dtTaskUsers.Rows.Count > 0 Then
                            For i As Integer = 0 To dtTaskUsers.Rows.Count - 1
                                cmbAssignedTo.SelectedIndex = i
                                oListUsers.SelectedItems.Add(Convert.ToInt64(dtTaskUsers.Rows(i)("ID")), dtTaskUsers.Rows(i)("Description"))
                            Next
                        End If

                    End If

                End If
            End If

            oListUsers.OpenControl()
            oListUsers.ShowHeaderPanel(False)
            ofrmList.ShowDialog(IIf(IsNothing(ofrmList.Parent), Me, ofrmList.Parent))

            If IsNothing(ofrmList) = False Then
                RemoveHandler oListUsers.ItemSelectedClick, AddressOf oListUsers_ItemSelectedClick
                RemoveHandler oListUsers.ItemClosedClick, AddressOf oListUsers_ItemClosedClick
                ofrmList.Controls.Remove(oListUsers)
                oListUsers.Dispose()
                oListUsers = Nothing
                ofrmList.Dispose()
            End If
            'If IsNothing(dt) = False Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
        Catch ex As Exception
            MessageBox.Show("Error on UserListControl" & ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub FillLabTaskUsers(ByVal dtTask As DataTable)
        Dim dt As DataTable = Nothing
        Dim dv As DataView = Nothing
        Try
            If Not IsNothing(dtTask) Then
                If IsNothing(ToList) Then
                    ToList = New gloGeneralItem.gloItems
                End If
                Dim ToItem As gloGeneralItem.gloItem
                Dim ToItemOld As gloGeneralItem.gloItem

                dt = cmbAssignedTo.DataSource

                If dtTask.Rows.Count > 0 Then
                    For i As Integer = 0 To dtTask.Rows.Count - 1

                        ToItem = New gloGeneralItem.gloItem()

                        ToItem.ID = dtTask.Rows(i)("nUserID")
                        ToItem.Description = dtTask.Rows(i)("sloginname")
                        If dtTask.Rows(i)("ResultType") = "1" Then
                            ToItem.Code = "Normal"
                            If ToList.Count > 0 Then
                                For j As Int16 = 0 To ToList.Count - 1
                                    ToItemOld = ToList.Item(j)
                                    If ToItemOld.ID = ToItem.ID Then
                                        ToItemOld.Code = "Normal"
                                        Exit For
                                    End If


                                Next
                            End If

                        Else
                            ToItem.Code = "Abnormal"
                            ''
                            If ToList.Count > 0 Then
                                For j As Int16 = 0 To ToList.Count - 1
                                    ToItemOld = ToList.Item(j)
                                    ToItemOld.Code = "Abnormal"
                                Next
                            End If

                            ''
                        End If


                        If IsNothing(dt) = False Then
                            If dt.Rows.Count > 0 Then
                                dv = dt.DefaultView
                                dv.RowFilter = "ID='" & ToItem.ID & "'"
                                'If dv.Count > 0 Then
                                '    Dim dr As DataRow()
                                '    dr = dt.Select("ID= '" & ToItem.ID & "'")
                                '    dr(0).ItemArray(2) = ToItem.Code

                                'End If

                                If dv.Count > 0 Then
                                Else
                                    ToList.Add(ToItem)
                                    ToItem.Dispose()
                                    ToItem = Nothing
                                End If
                            Else
                                ToList.Add(ToItem)
                                ToItem.Dispose()
                                ToItem = Nothing
                            End If

                        Else
                            ToList.Add(ToItem)
                            ToItem.Dispose()
                            ToItem = Nothing
                        End If

                    Next

                    FillAssignedToCombo()
                End If

            End If
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            'If Not IsNothing(ToList) Then
            '    ToList.Clear()
            'End If
            'If Not IsNothing(dv) Then
            '    dv.Dispose()
            '    dv = Nothing
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Public Sub ResetUsersOnProviderchange()
        If Not IsNothing(ToList) Then
            If ToList.Count > 0 Then
                ToList.Clear()
                FillAssignedToCombo()

            End If
        End If



    End Sub
    Private Sub FillAssignedToCombo()

        Dim dcId As New DataColumn("ID")
        Dim dcDescription As New DataColumn("Description")
        Dim dcResultType As New DataColumn("ResultType")

        Try
            ''To Fill Assigned users from Available ToList Object.

            If IsNothing(dtTaskUsers) = False Then
                dtTaskUsers.Dispose()
                dtTaskUsers.Clear()
            End If

            dtTaskUsers = New DataTable

            dtTaskUsers.Columns.Add(dcId)
            dtTaskUsers.Columns.Add(dcDescription)
            dtTaskUsers.Columns.Add(dcResultType)

            If ToList.Count > 0 Then
                For i As Int16 = 0 To ToList.Count - 1
                    Dim drTemp As DataRow = dtTaskUsers.NewRow()
                    drTemp("ID") = ToList.Item(i).ID
                    drTemp("Description") = ToList.Item(i).Description
                    drTemp("ResultType") = ToList.Item(i).Code
                    dtTaskUsers.Rows.Add(drTemp)
                Next
            End If

            cmbAssignedTo.DataSource = Nothing

            cmbAssignedTo.DataSource = dtTaskUsers
            cmbAssignedTo.ValueMember = dtTaskUsers.Columns("ID").ColumnName
            cmbAssignedTo.DisplayMember = dtTaskUsers.Columns("Description").ColumnName


            If ToList.Count > 0 Then
                pnlPD_TaskDescDueDate.Visible = True

                If _IsOpenedFromViewLab Then
                    txtTaskDesc.Enabled = True
                    dtTaskDueDate.Enabled = True
                End If

            Else

                If _IsOpenedFromViewLab Then
                    pnlPD_TaskDescDueDate.Visible = True
                    txtTaskDesc.Enabled = False
                    dtTaskDueDate.Enabled = False
                End If

            End If

            If pnlPD_TaskDescDueDate.Visible = True Then
                pnlPatientDetail.Height = pnlPD_OrderNumber.Height + pnlPD_PrefLabAssTask.Height + pnlPD_TaskDescDueDate.Height + pnlPD_RefBySamBy.Height + 10
                Me.Height = _Ht + pnlPD_TaskDescDueDate.Height + 5
            Else
                pnlPatientDetail.Height = pnlPD_OrderNumber.Height + pnlPD_PrefLabAssTask.Height + pnlPD_RefBySamBy.Height + 10
                Me.Height = _Ht + 5
            End If
            'Added by madan on 20100521 for fixing issue in labs
            If IsPanelClosed = True Then
                Me.Height = 33
            End If

        Catch ex As Exception
            MessageBox.Show("Error on UserListControl" & ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub oListUsers_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim dt As DataTable = Nothing
        Dim dv As DataView = Nothing
        Try
            If (_IsSampledBy = False) Then ''If clicked on  assigned tasks to 
                'Dim dtUsers As New DataTable()
                dtUsers = New DataTable
                Dim dcId As New DataColumn("ID")
                Dim dcDescription As New DataColumn("Description")
                Dim dcResultType As New DataColumn("ResultType")
                dtUsers.Columns.Add(dcId)
                dtUsers.Columns.Add(dcDescription)
                dtUsers.Columns.Add(dcResultType)
                If IsNothing(ToList) Then
                    ToList = New gloGeneralItem.gloItems()
                End If



                Dim ToItem As gloGeneralItem.gloItem


                dt = cmbAssignedTo.DataSource
                If oListUsers.SelectedItems.Count > 0 Then
                    For i As Int16 = 0 To oListUsers.SelectedItems.Count - 1
                        
                        Dim drTemp As DataRow = dtUsers.NewRow()
                        drTemp("ID") = oListUsers.SelectedItems(i).ID
                        drTemp("Description") = oListUsers.SelectedItems(i).Description
                        drTemp("ResultType") = "Normal"
                        ''ToItem.Code = dtTask.Rows(i)("nTaskTypeID")
                        dtUsers.Rows.Add(drTemp)

                        ToItem = New gloGeneralItem.gloItem()

                        ToItem.ID = oListUsers.SelectedItems(i).ID
                        ToItem.Description = oListUsers.SelectedItems(i).Description
                        ToItem.Code = "Normal"
                        If IsNothing(dt) = False Then
                            If dt.Rows.Count > 0 Then
                                dv = dt.Copy().DefaultView
                                dv.RowFilter = "ID='" & ToItem.ID & "'"
                                If dv.Count > 0 Then
                                Else
                                    ToList.Add(ToItem)
                                    ToItem.Dispose()
                                    ToItem = Nothing
                                End If

                            Else
                                ToList.Add(ToItem)
                                ToItem.Dispose()
                                ToItem = Nothing
                            End If

                        Else
                            ToList.Add(ToItem)
                            ToItem.Dispose()
                            ToItem = Nothing
                        End If

                    Next
                Else
                    ToList.Clear()
                End If

                ''Added on 20140326-To remove items from Tolist if user removes items from user control.
                Dim dvuser As DataView = Nothing

                For k As Integer = ToList.Count - 1 To 0 Step -1

                    dvuser = dtUsers.Copy().DefaultView
                    dvuser.RowFilter = "ID ='" & ToList.Item(k).ID & "'"
                    If dvuser.Count > 0 Then

                    Else
                        ToList.Remove(ToList.Item(k))
                    End If

                Next

                cmbAssignedTo.DataSource = dtUsers
                cmbAssignedTo.ValueMember = dtUsers.Columns("ID").ColumnName
                cmbAssignedTo.DisplayMember = dtUsers.Columns("Description").ColumnName

                ofrmList.Close()

                If ToList.Count > 0 Then
                    pnlPD_TaskDescDueDate.Visible = True

                    If _IsOpenedFromViewLab Then
                        txtTaskDesc.Enabled = True
                        dtTaskDueDate.Enabled = True
                    End If

                Else
                    If _IsOpenedFromViewLab Then
                        pnlPD_TaskDescDueDate.Visible = True
                        txtTaskDesc.Text = ""
                        txtTaskDesc.Enabled = False
                        dtTaskDueDate.Enabled = False
                    End If

                End If
                If pnlPD_TaskDescDueDate.Visible = True Then
                    pnlPatientDetail.Height = pnlPD_OrderNumber.Height + pnlPD_PrefLabAssTask.Height + pnlPD_TaskDescDueDate.Height + pnlPD_RefBySamBy.Height + 10
                    Me.Height = _Ht + pnlPD_TaskDescDueDate.Height + 5
                Else
                    pnlPatientDetail.Height = pnlPD_OrderNumber.Height + pnlPD_PrefLabAssTask.Height + pnlPD_RefBySamBy.Height + 10
                    Me.Height = _Ht + 5
                End If
            Else ''If clicked on sampled by 

                If oListUsers.SelectedItems.Count > 0 Then

                    txtSampledBy.Text = oListUsers.SelectedItems(0).Description
                    txtSampledBy.Tag = oListUsers.SelectedItems(0).ID
                End If
                ofrmList.Close()
            End If
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            'If Not IsNothing(dv) Then
            '    dv.Dispose()
            '    dv = Nothing
            'End If

        Catch ex As Exception
            MessageBox.Show("Error on UserListControl" & ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
  
    Private Sub oListUsers_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)
        ofrmList.Close()
    End Sub
   


    Private Sub btnClearAssTo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearAssTo.Click
        Try
            If cmbAssignedTo.SelectedIndex >= 0 Then
                If MessageBox.Show("Are you sure you want to clear the user?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                    ''Comment By Sudhir 20090210
                    'cmbAssignedTo.Items.RemoveAt(cmbAssignedTo.SelectedIndex)
                    'cmbAssignedTo.DataSource = Nothing
                    'ToList = Nothing

                    ''Above code commented by Sandip Darade 20090620
                    ''Added code below to remove the selected user only
                    Try
                        If cmbAssignedTo.SelectedIndex >= 0 Then
                            Dim _userId As Int64 = 0
                            'Remove item from ToList

                            _userId = Convert.ToInt64(cmbAssignedTo.SelectedValue)

                            For i As Integer = 0 To ToList.Count - 1

                                If (ToList(i).ID = _userId) Then

                                    ToList.RemoveAt(i)
                                    FillAssignedToCombo()
                                    ''Refresh rhe datasource of the combobox
                                    ''Remove the row containg  the selected user from the table 
                                    'Dim dr As DataRow
                                    'For Each dr In dtUsers.Rows
                                    '    If (dr.Item(0) = _userId) Then
                                    '        dtUsers.Rows.Remove(dr)
                                    '        Exit For
                                    '    End If
                                    'Next

                                    Exit For
                                End If
                            Next


                        End If
                    Catch ex As Exception

                    End Try

                    If cmbAssignedTo.Items.Count = 0 Then
                        cmbAssignedTo.Text = ""


                        If _IsOpenedFromViewLab Then
                            pnlPD_TaskDescDueDate.Visible = True
                            txtTaskDesc.Text = ""
                            txtTaskDesc.Enabled = False
                            dtTaskDueDate.Enabled = False
                        Else
                            pnlPD_TaskDescDueDate.Visible = False
                            ' pnlPD_SplitterTasks.Visible = False
                            pnlPatientDetail.Height = pnlPD_OrderNumber.Height + pnlPD_PrefLabAssTask.Height + pnlPD_RefBySamBy.Height + 10
                            Me.Height = _Ht + 5
                        End If
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    '// 2.FINISH <<<<<<<<<<<<<----------BUTTON SEARCH/CLOSE CODING---------->>>>>>>>>>>>>>>>> //


    '// 3.START <<<<<<<<<<<<<----------oC1FlexControl Event CODING---------->>>>>>>>>>>>>>>>> //

    Private Sub oC1flex__FlexDoubleClick(ByVal sender As Object, ByVal e As System.EventArgs, ByVal _bSelectFlag As Boolean) Handles oC1flex._FlexDoubleClick
        oC1flex_btnUC_OKclick(sender, e)
    End Sub

    Private Sub oC1flex_btnUC_ADDclick(ByVal sender As Object, ByVal e As System.EventArgs) Handles oC1flex.btnUC_ADDclick

        'code commented by sarika
        'setFlexControlValue()
        'UnloadFlexControl()
        '--------------------------------------

        'code added by sarika 13th oct 07
        Try
            RaiseEvent Lab_btnUC_ADDclick(sender, e, intStatus)

            ' ButtonX14_Click(sender, e)
            ' modify cod eon 20070613 to refresh the c1Grid
            ' Call loadC1flexgrid()
            Call load_c1flexControlData()

            ' default selection of new added row
            Dim searchdt1 = Me.searchdt

            If searchdt1 = 0 Then
                'i.e., if user clicks cancel of the form used for adding new contact then by default the first row should be selected
                'otherwise it will give error if user clicks cancel and then clicks OK button of the control to select the contact
                oC1flex._UCflex.Select(1, 1)
                Exit Sub
            End If

            'Dim c1Uc As New gloUC_CustomSearchInC1Flexgrid
            'c1Uc.SortDataview(searchdt)

            Dim searchrow As Integer = 0
            If oC1flex._bSelectFlag = False Then
                searchrow = oC1flex._UCflex.FindRow(searchdt1, 0, 0, False, True, False)

            Else
                searchrow = oC1flex._UCflex.FindRow(searchdt1, 0, 1, False, True, False)
            End If

            oC1flex._UCflex.Select(searchrow, 1)

            searchdt1 = 0
            'code commented on 20070524 Bipin
            'BindGrid()
            colno = 0

        Catch ex As Exception
            MessageBox.Show("Error in Lab Order Detail, while invoking add contact. " & ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


        '-----------------------------------------------------


    End Sub

    'code added by sarika 13/10/2007
    Private Sub oC1flex_btnUC_Modify_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles oC1flex.btnUC_Modify_click
        Dim ID As Int64
        Try
            If oC1flex._UCflex.Row > 0 Then
                ID = CType(oC1flex._UCflex.GetData(oC1flex._UCflex.Row, "ContactID"), System.Int64)
                'txtPharmacy.Text = CType(dgCustomGrid.CurrentName, System.String)
                'txtPharmacy.Tag = CType(dgCustomGrid.CurrentID, Long)
                btnSearchPL.Focus()
                'Else
                '    oC1flex_btnUC_ADDclick(sender, e)
                '    Exit Sub
            End If

            RaiseEvent Lab_btnUC_Modifyclick(sender, e, intStatus, ID)
            ' ButtonX14_Click(sender, e)
            ' modify cod eon 20070613 to refresh the c1Grid
            ' Call loadC1flexgrid()
            Call load_c1flexControlData()

            ' default selection of new added row
            Dim searchdt1 = ID
            'Dim c1Uc As New gloUC_CustomSearchInC1Flexgrid
            'c1Uc.SortDataview(searchdt)

            Dim searchrow As Integer = 0
            If oC1flex._bSelectFlag = False Then
                searchrow = oC1flex._UCflex.FindRow(searchdt1, 0, 0, False, True, False)

            Else
                searchrow = oC1flex._UCflex.FindRow(searchdt1, 0, 1, False, True, False)
            End If

            oC1flex._UCflex.Select(searchrow, 1)

            searchdt1 = 0
            'code commented on 20070524 Bipin
            'BindGrid()
            colno = 0

        Catch ex As Exception
            MessageBox.Show("Error in Lab Order Detail, while invoking Modify contact. " & ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try



    End Sub
    '-----------------------------------------------------

    Private Sub oC1flex_btnUC_Cancelclick(ByVal sender As Object, ByVal e As System.EventArgs) Handles oC1flex.btnUC_Cancelclick
        Try
            Me.Height = _Ht
            Select Case intStatus
                Case 1
                    btnSearchPL.Focus()
                    '''' Pramod 12212007 Referredby and sample by panel will show it we add this if statment start
                    If pnlPD_TaskDescDueDate.Visible = True Then
                        pnlPatientDetail.Height = pnlPD_OrderNumber.Height + pnlPD_PrefLabAssTask.Height + pnlPD_TaskDescDueDate.Height + pnlPD_RefBySamBy.Height + 10
                        Me.Height = Me.Height + pnlPD_TaskDescDueDate.Height + 5
                    Else
                        pnlPatientDetail.Height = pnlPD_OrderNumber.Height + pnlPD_PrefLabAssTask.Height + pnlPD_RefBySamBy.Height + 10
                        Me.Height = Me.Height + 5
                    End If
                    '''' Pramod 12212007 Referredby and sample by panel will show it we add this if statment end
                Case 2
                    btnSearchRB.Focus()
                    '''' Pramod 12212007 Referredby and sample by panel will show it we add this if statment start
                    If pnlPD_TaskDescDueDate.Visible = True Then
                        pnlPatientDetail.Height = pnlPD_OrderNumber.Height + pnlPD_PrefLabAssTask.Height + pnlPD_TaskDescDueDate.Height + pnlPD_RefBySamBy.Height + 10
                        Me.Height = Me.Height + pnlPD_TaskDescDueDate.Height + 5
                    Else
                        pnlPatientDetail.Height = pnlPD_OrderNumber.Height + pnlPD_PrefLabAssTask.Height + pnlPD_RefBySamBy.Height + 10
                        Me.Height = Me.Height + 5
                    End If
                    '''' Pramod 12212007 Referredby and sample by panel will show it we add this if statment end
                Case 3
                    btnSearchSB.Focus()
                    '''' Pramod 12212007 Referredby and sample by panel will show it we add this if statment start
                    If pnlPD_TaskDescDueDate.Visible = True Then
                        pnlPatientDetail.Height = pnlPD_OrderNumber.Height + pnlPD_PrefLabAssTask.Height + pnlPD_TaskDescDueDate.Height + pnlPD_RefBySamBy.Height + 10
                        Me.Height = Me.Height + pnlPD_TaskDescDueDate.Height + 5
                    Else
                        pnlPatientDetail.Height = pnlPD_OrderNumber.Height + pnlPD_PrefLabAssTask.Height + pnlPD_RefBySamBy.Height + 10
                        Me.Height = Me.Height + 5
                    End If
                    '''' Pramod 12212007 Referredby and sample by panel will show it we add this if statment end
                Case 4
                    btnSearchAssTo.Focus()
                    '' 20071121 - Mahesh 
                    '' if User are Selected then Only show the Panel of Task Description 
                    If cmbAssignedTo.Items.Count > 0 Then
                        pnlPD_TaskDescDueDate.Visible = True
                        ' pnlPD_SplitterTasks.Visible = True
                    Else
                        pnlPD_TaskDescDueDate.Visible = False
                        ' pnlPD_SplitterTasks.Visible = False
                    End If

                    If pnlPD_TaskDescDueDate.Visible = True Then
                        pnlPatientDetail.Height = pnlPD_OrderNumber.Height + pnlPD_PrefLabAssTask.Height + pnlPD_TaskDescDueDate.Height + pnlPD_RefBySamBy.Height + 10
                        Me.Height = Me.Height + pnlPD_TaskDescDueDate.Height + 5
                    Else
                        pnlPatientDetail.Height = pnlPD_OrderNumber.Height + pnlPD_PrefLabAssTask.Height + pnlPD_RefBySamBy.Height + 10
                        Me.Height = Me.Height + 5
                    End If
                    '' 
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub oC1flex_btnUC_OKclick(ByVal sender As Object, ByVal e As System.EventArgs) Handles oC1flex.btnUC_OKclick
        setFlexControlValue()
        UnloadFlexControl()
        '  pnl.Visible = False
    End Sub

    '// 3.FINISH <<<<<<<<<<<<<----------oC1FlexControl Event CODING---------->>>>>>>>>>>>>>>>> //


    '// 4.START <<<<<<<<<<<<<----------CONTROL GRID CODING---------->>>>>>>>>>>>>>>>> //

    Public Sub load_c1flexControlData()
        Try
            Dim _SelectFlag As Boolean = False
            'added by sarika 1 oct 07
            Dim sColumnName As String = ""
            '--------------
            If intStatus = 1 Then
                dtID = GetOrderContactInformations(gloEMRActors.LabActor.enumContactType.PreferredLab)
                'added by sarika 1 oct 07
                sColumnName = dtID.Columns(1).ColumnName

                'added by sarika 13th oct 07
                colno = 1
                '-----

                '--------------

            ElseIf intStatus = 2 Then
                dtID = GetOrderContactInformations(gloEMRActors.LabActor.enumContactType.ReferredBy)
                'added by sarika 1 oct 07
                sColumnName = dtID.Columns(3).ColumnName

                'added by sarika 13th oct 07
                colno = 1
                '-----------------

                '---------------------------------

                'sampled by --user_mst
            ElseIf intStatus = 3 Then
                dtID = GetOrderContactInformations(gloEMRActors.LabActor.enumContactType.SampledBy)
                'added by sarika 1 oct 07
                sColumnName = dtID.Columns("Name").ColumnName
                'added by sarika 13th oct 07
                colno = dtID.Columns.IndexOf("Name")
                '--------------

                '-----------------


                'Assigned to -- user_mst
            ElseIf intStatus = 4 Then
                Dim oDB As New gloEMRDatabase.DataBaseLayer
                dtID = oDB.GetDataTable_Query("SELECT nUserID,sLoginName as UserName,(isnull(sFirstName,'') + ' ' + isnull(sMiddleName,'') + ' ' + isnull(sLastName,'')) AS Name FROM User_MST WHERE sLoginName IS NOT NULL ORDER BY sLoginName")
                oDB.Dispose()
                oDB = Nothing
                _SelectFlag = True
                'added by sarika 1 oct 07
                sColumnName = dtID.Columns("UserName").ColumnName

                'added by sarika 13th oct 07
                colno = dtID.Columns.IndexOf("UserName")
                '-------
                '--------------

            End If

            If intStatus >= 1 And intStatus <= 4 Then
                If (IsNothing(oC1flex) = False) Then
                    Try
                        If (pnlGrid.Controls.Contains(oC1flex)) Then
                            pnlGrid.Controls.Remove(oC1flex)
                        End If
                        oC1flex.Dispose()
                        oC1flex = Nothing
                    Catch ex As Exception

                    End Try
                   
                End If
                oC1flex = New gloUC_CustomSearchInC1Flexgrid(dtID, _SelectFlag)
                If intStatus = 3 Or intStatus = 4 Then
                    oC1flex.AddEditFlag = False
                End If

                'added by sarika 1 oct 07
                oC1flex.SortDataview(sColumnName)
                '------------------------------------------------

                'If intStatus = 4 Then
                '    oC1flex.SortDataview("UserName")
                '    oC1flex.SetRowfilter("")
                'End If
                pnlGrid.Controls.Add(oC1flex)
                oC1flex.Dock = DockStyle.Fill
                oC1flex.BringToFront()
                oC1flex.Show()
            End If

        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub setFlexControlValue()
        If intStatus = 1 Then
            txtPreferredBy.Text = oC1flex._UCflex.GetData(oC1flex._UCflex.Row, 1)
            txtPreferredBy.Tag = oC1flex._UCflex.GetData(oC1flex._UCflex.Row, 0)
        ElseIf intStatus = 2 Then
            txtReferredBy.Text = oC1flex._UCflex.GetData(oC1flex._UCflex.Row, 1) & " " & oC1flex._UCflex.GetData(oC1flex._UCflex.Row, 2) & " " & oC1flex._UCflex.GetData(oC1flex._UCflex.Row, 3)
            txtReferredBy.Tag = oC1flex._UCflex.GetData(oC1flex._UCflex.Row, 0)
        ElseIf intStatus = 3 Then
            txtSampledBy.Text = oC1flex._UCflex.GetData(oC1flex._UCflex.Row, 1) '& " " & oC1flex._UCflex.GetData(oC1flex._UCflex.Row, 2) & " " & oC1flex._UCflex.GetData(oC1flex._UCflex.Row, 3)
            txtSampledBy.Tag = oC1flex._UCflex.GetData(oC1flex._UCflex.Row, 0)
        ElseIf intStatus = 4 Then

            Dim dv As DataView = oC1flex.SetRowfilter

            '                If IsNothing(dv) = False Then
            Dim j As Integer
            Dim strname As String

            ' code modified on 20070605 by bipin
            If IsNothing(dv) = False Then
                'If dv.Count > 0 Then
                cmbAssignedTo.Items.Clear()

                For j = 0 To dv.Count - 1
                    'cmbReferrals.Items.Add(New myList(CType(dv.Item(j)(0), String), strname))
                    '  If intStatus = 4 Then
                    strname = CType(dv.Item(j)(1), System.String) '& " " & CType(dv.Item(j)(2), System.String) & " " & CType(dv.Item(j)(3), System.String)
                    cmbAssignedTo.Items.Add(New gloEMRGeneralLibrary.Glogeneral.myList(dv.Item(j)(0), strname))
                    cmbAssignedTo.Text = strname
                    ' End If
                Next
            End If

            '' 20071121 Mahesh 
            '' if User are Selected then Only show the Panel of Task Description 
            If cmbAssignedTo.Items.Count > 0 Then
                pnlPD_TaskDescDueDate.Visible = True
                'pnlPatientDetail.Height = pnlPD_OrderNumber.Height + pnlPD_PrefLabAssTask.Height + pnlPD_TaskDescDueDate.Height + pnlPD_RefBySamBy.Height + 10
                'Me.Height = Me.Height + pnlPD_TaskDescDueDate.Height + 10
            Else
                pnlPD_TaskDescDueDate.Visible = False
                'pnlPatientDetail.Height = pnlPD_OrderNumber.Height + pnlPD_PrefLabAssTask.Height + pnlPD_RefBySamBy.Height + 10
                'Me.Height = Me.Height + 10
            End If
            '' 
        End If
    End Sub

    Private Sub UnloadFlexControl()
        If (IsNothing(oC1flex) = False) Then
            Try
                If (pnlGrid.Controls.Contains(oC1flex)) Then
                    pnlGrid.Controls.Remove(oC1flex)
                End If
                oC1flex.Dispose()
                oC1flex = Nothing
            Catch ex As Exception

            End Try

        End If
        ' pnlGrid.Controls.Remove(oC1flex)
        If pnlPD_TaskDescDueDate.Visible = True Then
            pnlPatientDetail.Height = pnlPD_OrderNumber.Height + pnlPD_PrefLabAssTask.Height + pnlPD_TaskDescDueDate.Height + pnlPD_RefBySamBy.Height + 10
            Me.Height = _Ht + pnlPD_TaskDescDueDate.Height + 5
        Else
            pnlPatientDetail.Height = pnlPD_OrderNumber.Height + pnlPD_PrefLabAssTask.Height + pnlPD_RefBySamBy.Height + 10
            Me.Height = _Ht + 5
        End If

    End Sub

    '// 4.FINISH <<<<<<<<<<<<<----------CONTROL GRID CODING---------->>>>>>>>>>>>>>>>> //


    ''Function By Sudhir 20090205
    Public Function GetUserID(ByVal strLoginName As String) As Long
        Try
            Dim _gloEMRDatabase As New gloEMRDatabase.DataBaseLayer
            Dim dt As DataTable
            Dim nUserID As Long

            dt = _gloEMRDatabase.GetDataTable_Query("SELECT nUserID,sLoginName as UserName,(isnull(sFirstName,'') + ' ' + isnull(sMiddleName,'') + ' ' + isnull(sLastName,'')) AS Name FROM User_MST WHERE sLoginName ='" + strLoginName + "' ORDER BY sLoginName")

            If IsNothing(dt) = False Then
                If dt.Rows.Count > 0 Then
                    nUserID = dt.Rows(0)("nUserID")
                End If
            End If

            If IsNothing(_gloEMRDatabase) = False Then
                _gloEMRDatabase.Dispose()
                _gloEMRDatabase = Nothing
            End If

            If IsNothing(dt) = False Then
                dt.Dispose()
                dt = Nothing
            End If

            Return nUserID

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        End Try
    End Function

    Private Sub btnUp_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.MouseHover
        btnUp.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.UPHover
        btnUp.BackgroundImageLayout = ImageLayout.Center
    End Sub

    Private Sub btnUp_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.MouseLeave
        btnUp.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.UP
        btnUp.BackgroundImageLayout = ImageLayout.Center
    End Sub

    Private Sub btnDown_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.MouseHover
        btnDown.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.DownHover
        btnDown.BackgroundImageLayout = ImageLayout.Center
    End Sub

    Private Sub btnDown_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.MouseLeave
        btnDown.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Down
        btnDown.BackgroundImageLayout = ImageLayout.Center
    End Sub

    Private Sub btn_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchPL.MouseHover, Button1.MouseHover, btnSearchSB.MouseHover, btnSearchRB.MouseHover, btnSearchAssTo.MouseHover, btnClearSB.MouseHover, btnClearRB.MouseHover, btnClearPL.MouseHover, btnClearAssTo.MouseHover
        CType(sender, Button).BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_LongYellow
        CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btn_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchPL.MouseLeave, Button1.MouseLeave, btnSearchSB.MouseLeave, btnSearchRB.MouseLeave, btnSearchAssTo.MouseLeave, btnClearSB.MouseLeave, btnClearRB.MouseLeave, btnClearPL.MouseLeave, btnClearAssTo.MouseLeave
        CType(sender, Button).BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_LongButton
        CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub txtPreferredBy_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPreferredBy.TextChanged
        If _IsLoading = False Then
            _IsOrderModified = True
        End If
    End Sub

    Private Sub txtTaskDesc_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTaskDesc.TextChanged
        If _IsLoading = False Then
            _IsOrderModified = True
        End If
    End Sub

    Private Sub txtReferredBy_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtReferredBy.TextChanged
        If _IsLoading = False Then
            _IsOrderModified = True
        End If
    End Sub

    Private Sub cmbAssignedTo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAssignedTo.TextChanged
        If _IsLoading = False Then
            _IsOrderModified = True
        End If
    End Sub

    Private Sub dtTaskDueDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtTaskDueDate.ValueChanged
        If _IsLoading = False Then
            _IsOrderModified = True
        End If
    End Sub

    Private Sub txtSampledBy_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSampledBy.TextChanged
        If _IsLoading = False Then
            _IsOrderModified = True
        End If
    End Sub
    'Added by madan on 20100601
    'This method is used in gloLabs.
    Public Sub PreferredLabActivity(ByVal IsEmdeonOrder As Boolean)
        If IsEmdeonOrder Then
            btnSearchPL.Enabled = False
            btnClearPL.Enabled = False
            txtPreferredBy.Enabled = False
            txtPreferredBy.BackColor = Color.FromKnownColor(KnownColor.Control)
        Else
            btnSearchPL.Enabled = True
            btnClearPL.Enabled = True
            txtPreferredBy.BackColor = Color.White
            txtPreferredBy.Enabled = True
        End If

    End Sub

  

    Private Sub rbSendToLab_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbSendToLab.CheckedChanged
        If rbSendToLab.Checked = True Then
            rbSendToLab.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            lblSendTo.Text = "Preferred/Performing Lab : "
        Else
            rbSendToLab.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
            txtPreferredBy.Text = ""
            txtPreferredBy.Tag = 0
            SendTo = SendToType.Physician
            ReferredTo = ""
            ReferredToID = 0

            Dim objfrmViewNormalLab As Object
            objfrmViewNormalLab = Me.ParentForm

            LoadTests(objfrmViewNormalLab)

            'If (objfrmViewNormalLab.Name = "frmViewNormalLab") Then
            '    'If (_OrderLabType = "") Then
            '    'objfrmViewNormalLab.FillTests_NEW()
            '    'Else
            '    If (_OrderLabType = "") OrElse (gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.LabTests.ToString() = _OrderLabType) Then
            '        'objfrmViewNormalLab.FillTests_NEW()
            '        objfrmViewNormalLab.FillTestsByType(gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.LabTests)

            '    ElseIf (gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.RadiologyImaging.ToString() = _OrderLabType) Then
            '        'objfrmViewNormalLab.FillRadiologyImagingTests()
            '        objfrmViewNormalLab.FillTestsByType(gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.RadiologyImaging)

            '    ElseIf (gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.Other.ToString() = _OrderLabType) Then
            '        'objfrmViewNormalLab.FillOthers()
            '        objfrmViewNormalLab.FillTestsByType(gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.Other)

            '    ElseIf (gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.Groups.ToString() = _OrderLabType) Then
            '        objfrmViewNormalLab.FillGroups_NEW()

            '    ElseIf (gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.Referrals.ToString() = _OrderLabType) Then
            '        'objfrmViewNormalLab.FillRefTests()
            '        objfrmViewNormalLab.FillTestsByType(gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.Referrals)

            '    End If
            '    'End If
            'End If


        End If
    End Sub

    Private Sub rbSendToPhysician_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbSendToPhysician.CheckedChanged
        If rbSendToPhysician.Checked = True Then

            rbSendToPhysician.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            lblSendTo.Text = "Referred To : "

            'LoadTests()

            Dim objfrmViewNormalLab As Object
            objfrmViewNormalLab = Me.ParentForm

            If (objfrmViewNormalLab.Name = "frmViewNormalLab") Then
                objfrmViewNormalLab.IsPreferredLabCleared = True
            End If

            LoadTests(objfrmViewNormalLab)

            'If (objfrmViewNormalLab.Name = "frmViewNormalLab") Then
            '    objfrmViewNormalLab.IsPreferredLabCleared = True

            '    'If (_OrderLabType = "") Then
            '    '    'objfrmViewNormalLab.FillTests_NEW()
            '    '    objfrmViewNormalLab.FillTestsByType(gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.LabTests)

            '    'Else
            '    If (_OrderLabType = "") OrElse (gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.LabTests.ToString() = _OrderLabType) Then
            '        'objfrmViewNormalLab.FillTests_NEW()
            '        objfrmViewNormalLab.FillTestsByType(gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.LabTests)

            '    ElseIf (gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.RadiologyImaging.ToString() = _OrderLabType) Then
            '        'objfrmViewNormalLab.FillRadiologyImagingTests()
            '        objfrmViewNormalLab.FillTestsByType(gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.RadiologyImaging)

            '    ElseIf (gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.Other.ToString() = _OrderLabType) Then
            '        'objfrmViewNormalLab.FillOthers()
            '        objfrmViewNormalLab.FillTestsByType(gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.Other)

            '    ElseIf (gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.Groups.ToString() = _OrderLabType) Then
            '        objfrmViewNormalLab.FillGroups_NEW()
            '        'objfrmViewNormalLab.FillTestsByType(gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.Groups)

            '    ElseIf (gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.Referrals.ToString() = _OrderLabType) Then
            '        'objfrmViewNormalLab.FillRefTests()
            '        objfrmViewNormalLab.FillTestsByType(gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.Referrals)

            '    End If
            '    'End If
            'End If

        Else
            rbSendToPhysician.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
            txtPreferredBy.Text = ""
            txtPreferredBy.Tag = 0
            SendTo = SendToType.Lab
            PreferredLab = ""
            PreferredLabID = 0
        End If
    End Sub


    Private Sub LoadTests(objViewNormalLab As Object, Optional PreferredID As Int64 = 0)

        '28-Jul-16 Aniket: Resolving Bug #98908: gloEMR:Lab Order:Application unable to display all lab test .
        If PreferredID = 0 Then
            PreferredLabID = 0
        End If

        objViewNormalLab = Me.ParentForm

        If (objViewNormalLab.Name = "frmViewNormalLab") Then
            objViewNormalLab.IsPreferredLabCleared = True

            If (_OrderLabType = "") OrElse (gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.LabTests.ToString() = _OrderLabType) Then
                'objfrmViewNormalLab.FillTests_NEW()
                objViewNormalLab.FillTestsByType(gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.LabTests, PreferredID)

            ElseIf (gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.RadiologyImaging.ToString() = _OrderLabType) Then
                'objfrmViewNormalLab.FillRadiologyImagingTests()
                objViewNormalLab.FillTestsByType(gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.RadiologyImaging, PreferredID)

            ElseIf (gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.Other.ToString() = _OrderLabType) Then
                'objfrmViewNormalLab.FillOthers()
                objViewNormalLab.FillTestsByType(gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.Other, PreferredID)

            ElseIf (gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.Groups.ToString() = _OrderLabType) Then
                objViewNormalLab.FillGroups_NEW(PreferredID)
                'objfrmViewNormalLab.FillTestsByType(gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.Groups)

            ElseIf (gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.Referrals.ToString() = _OrderLabType) Then
                'objfrmViewNormalLab.FillRefTests()
                objViewNormalLab.FillTestsByType(gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType.Referrals, PreferredID)

            End If
            'End If
        End If

    End Sub

End Class
