Imports System.Windows.Forms
Imports C1.Win.C1FlexGrid
Imports System.IO
Imports System.Data.SqlClient

Module mdlProcedureFunctions

    Public gTroubleShooting_RootPath As String = Application.StartupPath & "\" & "TroubleShooting"
    Public gTroubleShooting_Publisher As String = "Publisher.txt"
    Public gTroubleShooting_Subscriber As String = "Subscriber.txt"
    Public gTroubleShooting_NotInPublisher As String = "NotInPublisher.txt"
    Public gTroubleShooting_NotInSubscriber As String = "NotInSubscriber.txt"
    Public gTroubleShooting_ModNotInPublisher As String = "ModNotInPublisher.txt"
    Public gTroubleShooting_ModNotInSubscriber As String = "ModNotInPublisher.txt"
    Public gTroubleShooting_ConflictPublisherToSubscriber As String = "ConflictPublisherToSubscriber.txt"
    Public gTroubleShooting_ConflictSubscriberToPublisher As String = "ConflictSubscriberToPublisher.txt"

    Public _LogFileName As String = ""
    Public _LogMessage As String = ""


    Public Const COL_SET_SUB_ISSYNCH As Int16 = 0
    Public Const COL_SET_SUB_NAME As Int16 = 1
    Public Const COL_SET_SUB_SERVER As Int16 = 2
    Public Const COL_SET_SUB_DATABASE As Int16 = 3
    Public Const COL_SET_SUB_DMSPATH As Int16 = 4
    Public Const COL_SET_SUB_SQLAUTHONICATE As Int16 = 5
    Public Const COL_SET_SUB_SQLUSER As Int16 = 6
    Public Const COL_SET_SUB_SQLPWD As Int16 = 7
    Public Const COL_SET_SUB_COUNT As Int16 = 8

    Public _IsSynchInProcess As Boolean = False


    Public Sub Fill_Publishers(ByVal cmbPublisher As ComboBox)
        cmbPublisher.Items.Clear()
        Try
            'Fill Publisher
            Dim clSubscribers As New gloStream.gloDMS.gloSync.Subscriber.PublisherSubscriberDetails
            Dim objSubscribers As gloStream.gloDMS.gloSync.Subscriber.Subscriber
            objSubscribers = New gloStream.gloDMS.gloSync.Subscriber.Subscriber
            clSubscribers = objSubscribers.Publishers()
            objSubscribers = Nothing
            Dim nCount As Int16
            cmbPublisher.BeginUpdate()
            cmbPublisher.Items.Clear()
            For nCount = 1 To clSubscribers.Count
                If clSubscribers.Item(nCount).IsSubscriber = False Then
                    cmbPublisher.Items.Add(clSubscribers.Item(nCount).Name)
                    If clSubscribers.Item(nCount).IsSelect Then
                        cmbPublisher.SelectedIndex = nCount - 1
                    End If
                End If
            Next
            cmbPublisher.EndUpdate()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub Fill_Subscribers(ByVal lstSubscribers As ListBox)
        Dim clSubscribers As New gloStream.gloDMS.gloSync.Subscriber.PublisherSubscriberDetails
        Dim objSubscribers As gloStream.gloDMS.gloSync.Subscriber.Subscriber
        Dim nCount As Int16
        objSubscribers = New gloStream.gloDMS.gloSync.Subscriber.Subscriber
        clSubscribers = objSubscribers.Subscribers()
        objSubscribers = Nothing
        lstSubscribers.BeginUpdate()
        lstSubscribers.Items.Clear()
        For nCount = 1 To clSubscribers.Count
            lstSubscribers.Items.Add(clSubscribers.Item(nCount).Name)
            If clSubscribers.Item(nCount).IsSelect Then
                'lstSubscribers.SetItemChecked(nCount - 1, True)
            End If
        Next
        lstSubscribers.EndUpdate()
    End Sub

    Public Sub Fill_Subscribers(ByVal c1Subscribers As C1FlexGrid)
        Dim objSubscribers As New gloStream.gloDMS.gloSync.Subscriber.PublisherSubscriberDetails
        Dim objSubscriber As gloStream.gloDMS.gloSync.Subscriber.Subscriber
        Dim nCount As Int16
        objSubscriber = New gloStream.gloDMS.gloSync.Subscriber.Subscriber
        objSubscribers = objSubscriber.Subscribers()
        objSubscriber = Nothing

        c1Subscribers.Clear(ClearFlags.All)
        c1Subscribers.Rows.Count = 1
        c1Subscribers.Rows.Fixed = 1
        c1Subscribers.Cols.Count = COL_SET_SUB_COUNT
        c1Subscribers.Cols.Fixed = 0

        ''  c1Subscribers.SetData(0, 0, "")

        c1Subscribers.Cols(COL_SET_SUB_ISSYNCH).DataType = GetType(Boolean)
        c1Subscribers.Cols(COL_SET_SUB_NAME).DataType = GetType(String)
        c1Subscribers.Cols(COL_SET_SUB_SERVER).DataType = GetType(String)
        c1Subscribers.Cols(COL_SET_SUB_DATABASE).DataType = GetType(String)
        c1Subscribers.Cols(COL_SET_SUB_DMSPATH).DataType = GetType(String)
        c1Subscribers.Cols(COL_SET_SUB_SQLAUTHONICATE).DataType = GetType(String)
        c1Subscribers.Cols(COL_SET_SUB_SQLUSER).DataType = GetType(String)
        c1Subscribers.Cols(COL_SET_SUB_SQLPWD).DataType = GetType(String)

        c1Subscribers.SetData(0, 0, "")

        c1Subscribers.SetData(0, COL_SET_SUB_ISSYNCH, "..")
        c1Subscribers.SetData(0, COL_SET_SUB_NAME, "Name")
        c1Subscribers.SetData(0, COL_SET_SUB_SERVER, "Server")
        c1Subscribers.SetData(0, COL_SET_SUB_DATABASE, "Database")
        c1Subscribers.SetData(0, COL_SET_SUB_DMSPATH, "DMS Path")
        c1Subscribers.SetData(0, COL_SET_SUB_SQLAUTHONICATE, "Authontication")
        c1Subscribers.SetData(0, COL_SET_SUB_SQLUSER, "User")
        c1Subscribers.SetData(0, COL_SET_SUB_SQLPWD, "Password")


        c1Subscribers.Cols(COL_SET_SUB_ISSYNCH).Visible = True
        c1Subscribers.Cols(COL_SET_SUB_NAME).Visible = True
        c1Subscribers.Cols(COL_SET_SUB_SERVER).Visible = True
        c1Subscribers.Cols(COL_SET_SUB_DATABASE).Visible = True
        c1Subscribers.Cols(COL_SET_SUB_DMSPATH).Visible = True
        c1Subscribers.Cols(COL_SET_SUB_SQLAUTHONICATE).Visible = True
        c1Subscribers.Cols(COL_SET_SUB_SQLUSER).Visible = False
        c1Subscribers.Cols(COL_SET_SUB_SQLPWD).Visible = False

        c1Subscribers.Cols(COL_SET_SUB_ISSYNCH).Width = 30
        c1Subscribers.Cols(COL_SET_SUB_NAME).Width = 120
        c1Subscribers.Cols(COL_SET_SUB_SERVER).Width = 150
        c1Subscribers.Cols(COL_SET_SUB_DATABASE).Width = 150
        c1Subscribers.Cols(COL_SET_SUB_DMSPATH).Width = 150
        c1Subscribers.Cols(COL_SET_SUB_SQLAUTHONICATE).Width = 70
        c1Subscribers.Cols(COL_SET_SUB_SQLUSER).Width = 0
        c1Subscribers.Cols(COL_SET_SUB_SQLPWD).Width = 0

        c1Subscribers.Cols(COL_SET_SUB_ISSYNCH).AllowEditing = True
        c1Subscribers.Cols(COL_SET_SUB_NAME).AllowEditing = False
        c1Subscribers.Cols(COL_SET_SUB_SERVER).AllowEditing = False
        c1Subscribers.Cols(COL_SET_SUB_DATABASE).AllowEditing = False
        c1Subscribers.Cols(COL_SET_SUB_DMSPATH).AllowEditing = False
        c1Subscribers.Cols(COL_SET_SUB_SQLAUTHONICATE).AllowEditing = False
        c1Subscribers.Cols(COL_SET_SUB_SQLUSER).AllowEditing = False
        c1Subscribers.Cols(COL_SET_SUB_SQLPWD).AllowEditing = False

        For nCount = 1 To objSubscribers.Count
            c1Subscribers.Rows.Add()
            c1Subscribers.SetData(c1Subscribers.Rows.Count - 1, COL_SET_SUB_ISSYNCH, objSubscribers.Item(nCount).IsSelect)
            c1Subscribers.SetData(c1Subscribers.Rows.Count - 1, COL_SET_SUB_NAME, objSubscribers.Item(nCount).Name)
            c1Subscribers.SetData(c1Subscribers.Rows.Count - 1, COL_SET_SUB_SERVER, objSubscribers.Item(nCount).ServerName)
            c1Subscribers.SetData(c1Subscribers.Rows.Count - 1, COL_SET_SUB_DATABASE, objSubscribers.Item(nCount).DatabaseName)
            c1Subscribers.SetData(c1Subscribers.Rows.Count - 1, COL_SET_SUB_DMSPATH, objSubscribers.Item(nCount).DMSPath)
            c1Subscribers.SetData(c1Subscribers.Rows.Count - 1, COL_SET_SUB_SQLAUTHONICATE, objSubscribers.Item(nCount).ConnectionAuthontication)
            c1Subscribers.SetData(c1Subscribers.Rows.Count - 1, COL_SET_SUB_SQLUSER, objSubscribers.Item(nCount).ConnectionLoginName)
            c1Subscribers.SetData(c1Subscribers.Rows.Count - 1, COL_SET_SUB_SQLPWD, objSubscribers.Item(nCount).ConnectionPassword)
            c1Subscribers.Rows(c1Subscribers.Rows.Count - 1).Height = 23
        Next

    End Sub

    Public Function EstablishPublisher(ByVal ServerName As String, ByVal DatabaseName As String, ByVal ConnectionMode As gloStream.gloDMS.gloSync.Subscriber.enmConnectionAuthentication, ByVal DMSPath As String, Optional ByVal LoginName As String = "", Optional ByVal PassWord As String = "") As Boolean
        'Database Connection
        gPublisherConnectionString = gloEMRAdmin.mdlgloDMS.GetConnectionString(ServerName, DatabaseName, ConnectionMode, LoginName, PassWord)
        'DMS Path
        DMSPublisherRootPath = DMSPath
        'Check Valid Connection
        If IsConnect(gPublisherConnectionString) = False Then
            Return False
        End If
        'Check DMS Valid Path
        Dim oDMS As New gloStream.gloDMS.Supporting.Supporting
        If oDMS.IsDMSSystem(DMSPath) = False Then
            Return False
        End If
        Return True
    End Function

    Public Function EstablishSubscriber(ByVal ServerName As String, ByVal DatabaseName As String, ByVal ConnectionMode As gloStream.gloDMS.gloSync.Subscriber.enmConnectionAuthentication, ByVal DMSPath As String, Optional ByVal LoginName As String = "", Optional ByVal PassWord As String = "") As Boolean
        'Database Connection
        gSubscriberConnectionString = gloEMRAdmin.mdlgloDMS.GetConnectionString(ServerName, DatabaseName, ConnectionMode, LoginName, PassWord)
        'DMS Path
        DMSSubscriberRootPath = DMSPath
        'Check Valid Connection
        If IsConnect(gSubscriberConnectionString) = False Then
            Return False
        End If
        'Check DMS Valid Path
        Dim oDMS As New gloStream.gloDMS.Supporting.Supporting
        If oDMS.IsDMSSystem(DMSPath) = False Then
            Return False
        End If
        Return True
    End Function

    Public Sub Fill_Categories(ByVal lstCategories As CheckedListBox, ByVal SynchDataType As gloStream.gloDMS.gloSync.Subscriber.SynchrnoizeDataType)
        Dim oDiffCategory As New gloStream.gloDMS.gloSync.Supporting.Supporting
        Dim oCatCollection As New Collection

        oCatCollection = oDiffCategory.Categories(SynchDataType)
        If Not oCatCollection Is Nothing Then
            lstCategories.BeginUpdate()
            lstCategories.Items.Clear()
            For i As Int16 = 1 To oCatCollection.Count
                lstCategories.Items.Add(oCatCollection(i), False)
            Next
            lstCategories.EndUpdate()
        End If

        oCatCollection = Nothing
        oDiffCategory = Nothing
    End Sub

    Public Function AddCategories(ByVal lstCategories As CheckedListBox, ByVal SynchDataType As gloStream.gloDMS.gloSync.Subscriber.SynchrnoizeDataType) As Boolean
        Dim i As Int16
        Dim _Category As String
        Dim oDocumentCategory As gloStream.DocumentCategory
        '//Generate Categories in Publishers//
        For i = 0 To lstCategories.Items.Count - 1
            _Category = lstCategories.Items.Item(i)
            If SynchDataType = gloStream.gloDMS.gloSync.Subscriber.SynchrnoizeDataType.NotInPublisher Then
                oDocumentCategory = New gloStream.DocumentCategory(True)
            ElseIf SynchDataType = gloStream.gloDMS.gloSync.Subscriber.SynchrnoizeDataType.NotInSubscriber Then
                oDocumentCategory = New gloStream.DocumentCategory(False)
            End If
            If oDocumentCategory.Add(_Category) = True Then
                lstCategories.SetItemChecked(i, True)
            End If
            oDocumentCategory = Nothing
        Next
    End Function

    Public Function Fill_Documents(ByVal C1NotInPublisher As C1FlexGrid, ByVal C1NotInSubscriber As C1FlexGrid, ByVal C1ModNotInPublisher As C1FlexGrid, ByVal C1ModNotInSubscriber As C1FlexGrid, ByVal C1ConflictInPublihser As C1FlexGrid, ByVal C1ConflictInSubscriber As C1FlexGrid, ByVal C1Publisher As C1FlexGrid, ByVal C1Subscriber As C1FlexGrid, ByVal PublisherIsWinner As Boolean, ByVal SubscriberServerName As String, ByVal SubscriberDataBaseName As String) As Boolean
        Dim _Result As Boolean = False
        Dim oDataTable As DataTable
        Dim _SQLQuery As String
        Dim oDB As gloStream.gloDataBase.gloDataBase
        Dim _temp As Boolean = True

        'If _temp = True Then
        If CopyTable(SubscriberServerName, SubscriberDataBaseName) = True Then
            _LogMessage = "AFTER GETTING DOCUMENTS START TO FIND SYNCHRONIZATION(DIFFERENT) DOCUMENTS "
            LogInformation.EnterLog(_LogMessage, _LogFileName)

            ''SQL Query
            '_SQLQuery = "SELECT DocumentID,DocumentName,Extension,SourceMachine,SystemFolder,Container,Category,PatientID,[Year], " _
            '& " [Month],DocumentFormat,SourceBin,Pages,ArchiveStatus,ArchiveDescription,UsedStatus,UsedMachine, " _
            '& " UsedType,DocumentType,DocumentFileName,MachineID,Modified,Synchronized, " _
            '& " (SystemFolder+'\'+Container+'\'+Category+'\'+convert(varchar(60),PatientID)+'\'+[Year]+'\'+[Month]+'\'+convert(varchar(22),DocumentFileName)+'.'+Extension) As DocPath,VersionNo,ModifyDateTime " _
            '& " FROM DMS_MST Where DocumentType=2 AND DocumentFileName IS NOT NULL ORDER BY CATEGORY"

            '//---Publisher All Records---//
            '_SQLQuery = "SELECT DocumentID,DocumentName,Extension,SourceMachine,SystemFolder,Container,Category,PatientID,[Year], " _
            '& " [Month],DocumentFormat,SourceBin,Pages,ArchiveStatus,ArchiveDescription,UsedStatus,UsedMachine, " _
            '& " UsedType,DocumentType,DocumentFileName,MachineID,Modified,Synchronized, " _
            '& " (SystemFolder+'\'+Container+'\'+Category+'\'+convert(varchar(60),PatientID)+'\'+[Year]+'\'+[Month]+'\'+convert(varchar(22),DocumentFileName)+'.'+Extension) As DocPath,VersionNo,ModifyDateTime " _
            '& " FROM DMS_MST Where DocumentType=2 AND DocumentFileName IS NOT NULL ORDER BY CATEGORY"
            'oDB = New gloStream.gloDataBase.gloDataBase
            'oDataTable = New DataTable
            'oDB.Connect(GetConnectionString(gloStream.Supporting.ConnectionType.Publisher))
            'oDataTable = oDB.ReadQueryData(_SQLQuery)
            'oDataTable.Columns.Add(New DataColumn("Select", GetType(Boolean)))
            'oDB.Disconnect()
            'C1Publisher.DataSource = oDataTable
            'oDB = Nothing
            'oDataTable = Nothing

            '//---Subscriber All Records---//
            '_SQLQuery = "SELECT DocumentID,DocumentName,Extension,SourceMachine,SystemFolder,Container,Category,PatientID,[Year], " _
            ' & " [Month],DocumentFormat,SourceBin,Pages,ArchiveStatus,ArchiveDescription,UsedStatus,UsedMachine, " _
            ' & " UsedType,DocumentType,DocumentFileName,MachineID,Modified,Synchronized, " _
            ' & " (SystemFolder+'\'+Container+'\'+Category+'\'+convert(varchar(60),PatientID)+'\'+[Year]+'\'+[Month]+'\'+convert(varchar(22),DocumentFileName)+'.'+Extension) As DocPath,VersionNo,ModifyDateTime " _
            ' & " FROM DMS_MST_SUB Where DocumentType=2 AND DocumentFileName IS NOT NULL ORDER BY CATEGORY"
            'oDB = New gloStream.gloDataBase.gloDataBase
            'oDataTable = New DataTable
            'oDB.Connect(GetConnectionString(gloStream.Supporting.ConnectionType.Publisher))
            'oDataTable = oDB.ReadQueryData(_SQLQuery)
            'oDataTable.Columns.Add(New DataColumn("Select", GetType(Boolean)))
            'oDB.Disconnect()
            'C1Subscriber.DataSource = oDataTable
            'oDB = Nothing
            'oDataTable = Nothing

            '//***Design Grids***//
            _LogMessage = "Design All Grids - Start"
            LogInformation.EnterLog(_LogMessage, _LogFileName)

            DesignGrid(C1Publisher) : DesignGrid(C1Subscriber)
            'C1NotInPublisher.Rows.Count = 1 : C1NotInSubscriber.Rows.Count = 1 : C1ModNotInPublisher.Rows.Count = 1 : C1ModNotInSubscriber.Rows.Count = 1 : C1ConflictInPublihser.Rows.Count = 1 : C1ConflictInSubscriber.Rows.Count = 1
            DesignGrid(C1NotInPublisher) : DesignGrid(C1NotInSubscriber) : DesignGrid(C1ModNotInPublisher) : DesignGrid(C1ModNotInSubscriber) : DesignGrid(C1ConflictInPublihser) : DesignGrid(C1ConflictInSubscriber)

            _LogMessage = "Design All Grids - Finish"
            LogInformation.EnterLog(_LogMessage, _LogFileName)

            Application.DoEvents()

            _LogMessage = "Star Not in Publisher"
            LogInformation.EnterLog(_LogMessage, _LogFileName)

            '//---Not in Publisher---// - @QueryOption = 1
            '_SQLQuery = "SELECT DocumentID,DocumentName,Extension,SourceMachine,SystemFolder,Container,Category,PatientID,[Year], " _
            '& " [Month],DocumentFormat,SourceBin,Pages,ArchiveStatus,ArchiveDescription,UsedStatus,UsedMachine, " _
            '& " UsedType,DocumentType,DocumentFileName,MachineID,Modified,Synchronized, " _
            '& " (SystemFolder+'\'+Container+'\'+Category+'\'+convert(varchar(60),PatientID)+'\'+[Year]+'\'+[Month]+'\'+convert(varchar(22),DocumentFileName)+'.'+Extension) As DocPath,VersionNo,ModifyDateTime " _
            '& " FROM DMS_MST_SUB  " _
            '& " Where (DocumentType=2) AND (DocumentFileName IS NOT NULL) " _
            '& " AND (DocumentFileName NOT IN (SELECT DocumentFileName FROM DMS_MST WHERE DocumentFileName IS NOT NULL)) " _
            '& " ORDER BY CATEGORY"
            _LogMessage = "Establish Connection - Start"
            LogInformation.EnterLog(_LogMessage, _LogFileName)

            oDB = New gloStream.gloDataBase.gloDataBase
            oDataTable = New DataTable
            oDB.Connect(gloEMRAdmin.mdlgloDMS.GetConnectionString_DMS(gloStream.Supporting.ConnectionType.Publisher))

            _LogMessage = "Establish Connection - Finish"
            LogInformation.EnterLog(_LogMessage, _LogFileName)

            oDB.DBParameters.Clear()

            _LogMessage = "Assign Stored Procedured - Start"
            LogInformation.EnterLog(_LogMessage, _LogFileName)

            oDB.DBParameters.Add("@QueryOption", 1, ParameterDirection.Input, SqlDbType.Int)

            _LogMessage = "Assign Stored Procedured - Finish"
            LogInformation.EnterLog(_LogMessage, _LogFileName)

            _LogMessage = "Excecute Stored Procedure - Start"
            LogInformation.EnterLog(_LogMessage, _LogFileName)

            oDataTable = oDB.ReadData("gsp_DMS_SynchDocuments")

            _LogMessage = "Excecute Stored Procedure - Finish"
            LogInformation.EnterLog(_LogMessage, _LogFileName)

            oDB.Disconnect()

            Application.DoEvents()

            _LogMessage = "Bind Database - Start"
            LogInformation.EnterLog(_LogMessage, _LogFileName)

            C1NotInPublisher.DataSource = oDataTable

            _LogMessage = "Bind Database - Finish"
            LogInformation.EnterLog(_LogMessage, _LogFileName)

            If oDB.ErrorMessage <> "" Then
                _LogMessage = oDB.ErrorMessage
                LogInformation.EnterLog(_LogMessage, _LogFileName)
            End If

            oDB = Nothing
            oDataTable = Nothing

            Application.DoEvents()

            '//---Not in Subscriber---// - @QueryOption = 2
            _LogMessage = "Star Not in Subscriber"
            LogInformation.EnterLog(_LogMessage, _LogFileName)

            '_SQLQuery = "SELECT DocumentID,DocumentName,Extension,SourceMachine,SystemFolder,Container,Category,PatientID,[Year], " _
            '& " [Month],DocumentFormat,SourceBin,Pages,ArchiveStatus,ArchiveDescription,UsedStatus,UsedMachine, " _
            '& " UsedType,DocumentType,DocumentFileName,MachineID,Modified,Synchronized, " _
            '& " (SystemFolder+'\'+Container+'\'+Category+'\'+convert(varchar(60),PatientID)+'\'+[Year]+'\'+[Month]+'\'+convert(varchar(22),DocumentFileName)+'.'+Extension) As DocPath,VersionNo,ModifyDateTime " _
            '& " FROM DMS_MST " _
            '& " Where (DocumentType=2) AND (DocumentFileName IS NOT NULL) " _
            '& " AND (DocumentFileName NOT IN (SELECT DocumentFileName FROM DMS_MST_SUB  WHERE DocumentFileName IS NOT NULL)) " _
            '& " ORDER BY CATEGORY"
            oDB = New gloStream.gloDataBase.gloDataBase
            oDataTable = New DataTable
            oDB.Connect(gloEMRAdmin.mdlgloDMS.GetConnectionString_DMS(gloStream.Supporting.ConnectionType.Publisher))
            oDB.DBParameters.Clear()
            oDB.DBParameters.Add("@QueryOption", 2, ParameterDirection.Input, SqlDbType.Int)
            oDataTable = oDB.ReadData("gsp_DMS_SynchDocuments")
            oDB.Disconnect()
            C1NotInSubscriber.DataSource = oDataTable

            If oDB.ErrorMessage <> "" Then
                _LogMessage = oDB.ErrorMessage
                LogInformation.EnterLog(_LogMessage, _LogFileName)
            End If

            oDB = Nothing
            oDataTable = Nothing

            Application.DoEvents()

            '//---Modified Not in Publisher---// @QueryOption = 3
            _LogMessage = "Star Modified Not in Publisher"
            LogInformation.EnterLog(_LogMessage, _LogFileName)

            '_SQLQuery = "SELECT DocumentID,DocumentName,Extension,SourceMachine,SystemFolder,Container,Category,PatientID,[Year], " _
            '& " [Month],DocumentFormat,SourceBin,Pages,ArchiveStatus,ArchiveDescription,UsedStatus,UsedMachine, " _
            '& " UsedType,DocumentType,DocumentFileName,MachineID,Modified,Synchronized, " _
            '& " (SystemFolder+'\'+Container+'\'+Category+'\'+convert(varchar(60),PatientID)+'\'+[Year]+'\'+[Month]+'\'+convert(varchar(22),DocumentFileName)+'.'+Extension) As DocPath,VersionNo,ModifyDateTime " _
            '& " FROM DMS_MST_SUB  " _
            '& " Where (DocumentType=2) AND (DocumentFileName IS NOT NULL) " _
            '& " AND (DocumentFileName IN (SELECT DocumentFileName FROM DMS_MST WHERE VersionNo < DMS_MST_SUB.VersionNo  AND DocumentFileName IS NOT NULL)) " _
            '& " ORDER BY CATEGORY"
            oDB = New gloStream.gloDataBase.gloDataBase
            oDataTable = New DataTable
            oDB.Connect(gloEMRAdmin.mdlgloDMS.GetConnectionString_DMS(gloStream.gloDMS.gloSync.Subscriber.ConnectionType.Publisher))
            oDB.DBParameters.Clear()
            oDB.DBParameters.Add("@QueryOption", 3, ParameterDirection.Input, SqlDbType.Int)
            oDataTable = oDB.ReadData("gsp_DMS_SynchDocuments")
            oDB.Disconnect()
            C1ModNotInPublisher.DataSource = oDataTable

            If oDB.ErrorMessage <> "" Then
                _LogMessage = oDB.ErrorMessage
                LogInformation.EnterLog(_LogMessage, _LogFileName)
            End If


            oDB = Nothing
            oDataTable = Nothing

            Application.DoEvents()

            '//---Modified Not in Subscriber---// - @QueryOption = 4
            _LogMessage = "Star Modified Not in Subscriber"
            LogInformation.EnterLog(_LogMessage, _LogFileName)

            '_SQLQuery = "SELECT DocumentID,DocumentName,Extension,SourceMachine,SystemFolder,Container,Category,PatientID,[Year], " _
            '& " [Month],DocumentFormat,SourceBin,Pages,ArchiveStatus,ArchiveDescription,UsedStatus,UsedMachine, " _
            '& " UsedType,DocumentType,DocumentFileName,MachineID,Modified,Synchronized, " _
            '& " (SystemFolder+'\'+Container+'\'+Category+'\'+convert(varchar(60),PatientID)+'\'+[Year]+'\'+[Month]+'\'+convert(varchar(22),DocumentFileName)+'.'+Extension) As DocPath,VersionNo,ModifyDateTime " _
            '& " FROM DMS_MST " _
            '& " Where (DocumentType=2) AND (DocumentFileName IS NOT NULL) " _
            '& " AND (DocumentFileName IN (SELECT DocumentFileName FROM DMS_MST_SUB WHERE VersionNo < DMS_MST.VersionNo  AND DocumentFileName IS NOT NULL)) " _
            '& " ORDER BY CATEGORY"
            oDB = New gloStream.gloDataBase.gloDataBase
            oDataTable = New DataTable
            oDB.Connect(gloEMRAdmin.mdlgloDMS.GetConnectionString_DMS(gloStream.Supporting.ConnectionType.Publisher))
            oDB.DBParameters.Clear()
            oDB.DBParameters.Add("@QueryOption", 4, ParameterDirection.Input, SqlDbType.Int)
            oDataTable = oDB.ReadData("gsp_DMS_SynchDocuments")
            oDB.Disconnect()
            C1ModNotInSubscriber.DataSource = oDataTable

            If oDB.ErrorMessage <> "" Then
                _LogMessage = oDB.ErrorMessage
                LogInformation.EnterLog(_LogMessage, _LogFileName)
            End If


            oDB = Nothing
            oDataTable = Nothing

            Application.DoEvents()

            '//---Conflict Not in Publisher---// - @QueryOption = 5
            _LogMessage = "Star Conflict Not in Publisher"
            LogInformation.EnterLog(_LogMessage, _LogFileName)

            '_SQLQuery = "SELECT DocumentID,DocumentName,Extension,SourceMachine,SystemFolder,Container,Category,PatientID,[Year], " _
            '& " [Month],DocumentFormat,SourceBin,Pages,ArchiveStatus,ArchiveDescription,UsedStatus,UsedMachine, " _
            '& " UsedType,DocumentType,DocumentFileName,MachineID,Modified,Synchronized, " _
            '& " (SystemFolder+'\'+Container+'\'+Category+'\'+convert(varchar(60),PatientID)+'\'+[Year]+'\'+[Month]+'\'+convert(varchar(22),DocumentFileName)+'.'+Extension) As DocPath,VersionNo,ModifyDateTime " _
            '& " FROM DMS_MST_SUB " _
            '& " Where (DocumentType=2) AND (DocumentFileName IS NOT NULL) " _
            '& " AND (DocumentFileName IN (SELECT DocumentFileName FROM DMS_MST WHERE VersionNo = DMS_MST_SUB.VersionNo AND ModifyDateTime < DMS_MST_SUB.ModifyDateTime  AND DocumentFileName IS NOT NULL)) " _
            '& " ORDER BY CATEGORY"
            oDB = New gloStream.gloDataBase.gloDataBase
            oDataTable = New DataTable
            oDB.Connect(gloEMRAdmin.mdlgloDMS.GetConnectionString_DMS(gloStream.Supporting.ConnectionType.Publisher))
            oDB.DBParameters.Clear()
            oDB.DBParameters.Add("@QueryOption", 5, ParameterDirection.Input, SqlDbType.Int)
            oDataTable = oDB.ReadData("gsp_DMS_SynchDocuments")
            oDB.Disconnect()
            C1ConflictInPublihser.DataSource = oDataTable

            If oDB.ErrorMessage <> "" Then
                _LogMessage = oDB.ErrorMessage
                LogInformation.EnterLog(_LogMessage, _LogFileName)
            End If


            oDB = Nothing
            oDataTable = Nothing

            '//---Conflict Not in Subscriber---// - @QueryOption = 6
            _LogMessage = "Star Conflict Not in Subscriber"
            LogInformation.EnterLog(_LogMessage, _LogFileName)

            Application.DoEvents()

            '_SQLQuery = "SELECT DocumentID,DocumentName,Extension,SourceMachine,SystemFolder,Container,Category,PatientID,[Year], " _
            '& " [Month],DocumentFormat,SourceBin,Pages,ArchiveStatus,ArchiveDescription,UsedStatus,UsedMachine, " _
            '& " UsedType,DocumentType,DocumentFileName,MachineID,Modified,Synchronized, " _
            '& " (SystemFolder+'\'+Container+'\'+Category+'\'+convert(varchar(60),PatientID)+'\'+[Year]+'\'+[Month]+'\'+convert(varchar(22),DocumentFileName)+'.'+Extension) As DocPath,VersionNo,ModifyDateTime " _
            '& " FROM DMS_MST " _
            '& " Where (DocumentType=2) AND (DocumentFileName IS NOT NULL) " _
            '& " AND (DocumentFileName IN (SELECT DocumentFileName FROM DMS_MST_SUB WHERE VersionNo = DMS_MST.VersionNo AND ModifyDateTime < DMS_MST.ModifyDateTime  AND DocumentFileName IS NOT NULL)) " _
            '& " ORDER BY CATEGORY"
            oDB = New gloStream.gloDataBase.gloDataBase
            oDataTable = New DataTable
            oDB.Connect(gloEMRAdmin.mdlgloDMS.GetConnectionString_DMS(gloStream.Supporting.ConnectionType.Publisher))
            oDB.DBParameters.Clear()
            oDB.DBParameters.Add("@QueryOption", 6, ParameterDirection.Input, SqlDbType.Int)
            oDataTable = oDB.ReadData("gsp_DMS_SynchDocuments")
            oDB.Disconnect()
            C1ConflictInSubscriber.DataSource = oDataTable

            If oDB.ErrorMessage <> "" Then
                _LogMessage = oDB.ErrorMessage
                LogInformation.EnterLog(_LogMessage, _LogFileName)
            End If


            oDB = Nothing
            oDataTable = Nothing

            Application.DoEvents()

            _Result = True

            _LogMessage = "AFTER GETTING DOCUMENTS FINISH TO FIND SYNCHRONIZATION(DIFFERENT) DOCUMENTS "
            LogInformation.EnterLog(_LogMessage, _LogFileName)

        Else
            MessageBox.Show("Problem in remote server database connectivity", gstrMessageBoxCaption, MessageBoxButtons.OK)
        End If

        DesignGrid(C1NotInPublisher)
        DesignGrid(C1NotInSubscriber)
        DesignGrid(C1ModNotInPublisher)
        DesignGrid(C1ModNotInSubscriber)
        DesignGrid(C1ConflictInPublihser)
        DesignGrid(C1ConflictInSubscriber)

        Return _Result
    End Function

    Public Function CopyTable(ByVal _SubscriberServerName As String, ByVal _SubscriberDataBaseName As String) As Boolean
        Dim oDB As gloStream.gloDataBase.gloDataBase
        Dim _strSQL As String
        Dim _Result As Boolean = False

        ''Drop Subscriber Linked Server
        'oDBPub = New gloStream.gloDataBase.gloDataBase
        'oDBPub.Connect(PubConnString)
        'oDBPub.DBParameters.Add("@server", txtSubscriber_Server.Text.Trim, ParameterDirection.Input, SqlDbType.VarChar)
        'oDBPub.ExecuteNonQuery("gsp_dropserver")
        'oDBPub.Disconnect()
        'oDBPub = Nothing

        ''Add Subscriber Linked Server
        'oDBPub = New gloStream.gloDataBase.gloDataBase
        'oDBPub.Connect(PubConnString)
        'oDBPub.DBParameters.Add("@server", txtSubscriber_Server.Text.Trim, ParameterDirection.Input, SqlDbType.VarChar)
        'oDBPub.DBParameters.Add("@provider", "SQLOLEDB", ParameterDirection.Input, SqlDbType.VarChar)
        'oDBPub.DBParameters.Add("@srvproduct", "", ParameterDirection.Input, SqlDbType.VarChar)
        'oDBPub.DBParameters.Add("@provstr", "DRIVER={SQL Server};SERVER=" & txtPublisher_Server.Text & ";UID=" & txtPublisherUserName.Text & ";PWD=" & txtPublisherPassword.Text & ";", ParameterDirection.Input, SqlDbType.VarChar)
        'oDBPub.ExecuteNonQuery("gsp_addlinkedserver")
        'oDBPub.Disconnect()
        'oDBPub = Nothing

        'Drop Subscriber Table
        'oDB = New gloStream.gloDataBase.gloDataBase
        'oDB.Connect(GetConnectionString(gloStream.Supporting.ConnectionType.Publisher))
        '_strSQL = "IF EXISTS (SELECT * FROM dbo.sysobjects where id = object_id(N'[dbo].[DMS_MST_SUB]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [dbo].[DMS_MST_SUB]"
        'oDB.ExecuteQueryNonQuery(_strSQL)
        '_strSQL = "SELECT * INTO DMS_MST_SUB FROM " & _SubscriberServerName & "." & _SubscriberDataBaseName & ".dbo.DMS_MST"
        'oDB.ExecuteQueryNonQuery(_strSQL)
        'oDB.Disconnect()
        'oDB = Nothing

        _LogMessage = "1. Imp Synchronizaion Table Staus Checking"
        LogInformation.EnterLog(_LogMessage, _LogFileName)

        oDB = New gloStream.gloDataBase.gloDataBase
        oDB.Connect(gloEMRAdmin.mdlgloDMS.GetConnectionString_DMS(gloStream.Supporting.ConnectionType.Publisher))
        oDB.DBParameters.Add("@SubscriberServerName", _SubscriberServerName, ParameterDirection.Input, SqlDbType.VarChar, (255))
        oDB.DBParameters.Add("@SubscriberDataBaseName", _SubscriberDataBaseName, ParameterDirection.Input, SqlDbType.VarChar, (255))
        oDB.ExecuteNonQuery("gsp_DMS_ImportSynchTable")
        If oDB.ErrorMessage <> "" Then
            MessageBox.Show(oDB.ErrorMessage)
        End If
        If oDB.ErrorMessage <> "" Then
            _LogMessage = "2. " & oDB.ErrorMessage
            LogInformation.EnterLog(_LogMessage, _LogFileName)
        Else
            _LogMessage = "2. Imp Synchronizaion Table Staus OK"
            LogInformation.EnterLog(_LogMessage, _LogFileName)
        End If
        oDB.Disconnect()
        oDB = Nothing


        oDB = New gloStream.gloDataBase.gloDataBase
        oDB.Connect(gloEMRAdmin.mdlgloDMS.GetConnectionString_DMS(gloStream.Supporting.ConnectionType.Subscriber))
        _strSQL = "SELECT COUNT(*) FROM DMS_MST"
        Dim _PublisherCount As Long = CLng(oDB.ExecuteQueryScaler(_strSQL))
        oDB.Disconnect()
        oDB = Nothing
        _LogMessage = "3. Publisher - Find Synch Document in : " & _PublisherCount
        LogInformation.EnterLog(_LogMessage, _LogFileName)


        oDB = New gloStream.gloDataBase.gloDataBase
        oDB.Connect(gloEMRAdmin.mdlgloDMS.GetConnectionString_DMS(gloStream.Supporting.ConnectionType.Publisher))
        _strSQL = "SELECT COUNT(*) FROM DMS_MST_SUB"
        Dim _SubscriberCount As Long = CLng(oDB.ExecuteQueryScaler(_strSQL))
        If oDB.ErrorMessage <> "" Then
            _LogMessage = "4.0 Subscriber Document not found " & oDB.ErrorMessage
            LogInformation.EnterLog(_LogMessage, _LogFileName)
        End If
        oDB.Disconnect()
        oDB = Nothing

        _LogMessage = "4. Subscriber - Find Synch Document in : " & _SubscriberCount
        LogInformation.EnterLog(_LogMessage, _LogFileName)

        If _PublisherCount = _SubscriberCount Then
            _Result = True
        Else
            _Result = False
        End If

        _LogMessage = "5. Copy of Table result send to program is : " & _Result
        LogInformation.EnterLog(_LogMessage, _LogFileName)

        Return _Result
    End Function

    Public Function DropTable(ByVal _PublisherSubscriberFlag As gloStream.Supporting.ConnectionType) As Boolean
        Dim oDB As gloStream.gloDataBase.gloDataBase
        Dim _strSQL As String
        Dim _Result As Boolean = False

        _LogMessage = "---Start Removing Temporary Data if found---"
        LogInformation.EnterLog(_LogMessage, _LogFileName)

        'Drop Subscriber Table
        oDB = New gloStream.gloDataBase.gloDataBase
        oDB.Connect(gloEMRAdmin.mdlgloDMS.GetConnectionString_DMS(_PublisherSubscriberFlag))
        _strSQL = "IF EXISTS (SELECT * FROM dbo.sysobjects where id = object_id(N'[dbo].[DMS_MST_SUB]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [dbo].[DMS_MST_SUB]"
        _Result = oDB.ExecuteQueryNonQuery(_strSQL)

        If oDB.ErrorMessage <> "" Then
            _LogMessage = "---Finish Removing Temporary Data if found---"
            LogInformation.EnterLog(_LogMessage, _LogFileName)
        End If


        oDB.Disconnect()
        oDB = Nothing


        _LogMessage = "---Finish Removing Temporary Data if found---"
        LogInformation.EnterLog(_LogMessage, _LogFileName)

        Return _Result
    End Function

    Public Sub Fill_Documents_WithForLoop(ByVal C1NotInPublisher As C1FlexGrid, ByVal C1NotInSubscriber As C1FlexGrid, ByVal C1ModNotInPublisher As C1FlexGrid, ByVal C1ModNotInSubscriber As C1FlexGrid, ByVal C1ConflictInPublihser As C1FlexGrid, ByVal C1ConflictInSubscriber As C1FlexGrid, ByVal C1Publisher As C1FlexGrid, ByVal C1Subscriber As C1FlexGrid, ByVal PublisherIsWinner As Boolean)
        Dim dtPublisher As New DataTable, dtSubscriber As New DataTable
        Dim oDB As gloStream.gloDataBase.gloDataBase
        Dim _SQLQuery As String
        Dim nRow As Integer, nCol As Integer, nFoundRow As Integer

        Dim _LogFileName As String, _LogMessage As String
        _LogFileName = LogInformation.NewLog

        _LogMessage = "Start Log"
        LogInformation.EnterLog(_LogMessage, _LogFileName)

        'SQL Query
        _SQLQuery = "SELECT DocumentID,DocumentName,Extension,SourceMachine,SystemFolder,Container,Category,PatientID,[Year], " _
        & " [Month],DocumentFormat,SourceBin,Pages,ArchiveStatus,ArchiveDescription,UsedStatus,UsedMachine, " _
        & " UsedType,DocumentType,DocumentFileName,MachineID,Modified,Synchronized, " _
        & " (SystemFolder+'\'+Container+'\'+Category+'\'+convert(varchar(60),PatientID)+'\'+[Year]+'\'+[Month]+'\'+convert(varchar(22),DocumentFileName)+'.'+Extension) As DocPath,VersionNo,ModifyDateTime " _
        & " FROM DMS_MST Where DocumentType=2 AND DocumentFileName IS NOT NULL ORDER BY CATEGORY"

        _LogMessage = "Create Publisher and Subscriber common query to pick documents"
        LogInformation.EnterLog(_LogMessage, _LogFileName)

        'Publisher All Records
        _LogMessage = "Pick publisher record from datatbase - Start"
        LogInformation.EnterLog(_LogMessage, _LogFileName)

        oDB = New gloStream.gloDataBase.gloDataBase
        oDB.Connect(gloEMRAdmin.mdlgloDMS.GetConnectionString_DMS(gloStream.Supporting.ConnectionType.Publisher))
        dtPublisher = oDB.ReadQueryData(_SQLQuery)
        oDB.Disconnect()
        oDB = Nothing

        _LogMessage = "Pick publisher record from datatbase - Finish"
        LogInformation.EnterLog(_LogMessage, _LogFileName)

        'Subscriber All Records
        _LogMessage = "Pick subscriber record from datatbase - Start"
        LogInformation.EnterLog(_LogMessage, _LogFileName)

        oDB = New gloStream.gloDataBase.gloDataBase
        oDB.Connect(gloEMRAdmin.mdlgloDMS.GetConnectionString_DMS(gloStream.Supporting.ConnectionType.Subscriber))
        dtSubscriber = oDB.ReadQueryData(_SQLQuery)
        oDB.Disconnect()
        oDB = Nothing

        _LogMessage = "Pick subscriber record from datatbase - Finish"
        LogInformation.EnterLog(_LogMessage, _LogFileName)

        'Publisher & Subscriber Data Table
        dtPublisher.Columns.Add(New DataColumn("Select", GetType(Boolean)))
        dtSubscriber.Columns.Add(New DataColumn("Select", GetType(Boolean)))
        'Publisher & Subscriber C1FlexGrid

        _LogMessage = "Bound publisher record to data table - Start"
        LogInformation.EnterLog(_LogMessage, _LogFileName)
        C1Publisher.DataSource = dtPublisher
        _LogMessage = "Bound publisher record to data table - Finish"
        LogInformation.EnterLog(_LogMessage, _LogFileName)

        _LogMessage = "Bound subscriber record to data table - Start"
        LogInformation.EnterLog(_LogMessage, _LogFileName)
        C1Subscriber.DataSource = dtSubscriber
        _LogMessage = "Bound subscriber record to data table - Finish"
        LogInformation.EnterLog(_LogMessage, _LogFileName)

        DesignGrid(C1Publisher) : DesignGrid(C1Subscriber)

        C1NotInPublisher.Rows.Count = 1 : C1NotInSubscriber.Rows.Count = 1 : C1ModNotInPublisher.Rows.Count = 1 : C1ModNotInSubscriber.Rows.Count = 1 : C1ConflictInPublihser.Rows.Count = 1 : C1ConflictInSubscriber.Rows.Count = 1
        DesignGrid(C1NotInPublisher) : DesignGrid(C1NotInSubscriber) : DesignGrid(C1ModNotInPublisher) : DesignGrid(C1ModNotInSubscriber) : DesignGrid(C1ConflictInPublihser) : DesignGrid(C1ConflictInSubscriber)

        Application.DoEvents()

        '---Find Documents From Subscriber to Publisher---
        _LogMessage = "Fill document from subscriber to publisher - Start"
        LogInformation.EnterLog(_LogMessage, _LogFileName)

        For nRow = C1Subscriber.Rows.Count - 1 To 1 Step -1
            Application.DoEvents()
            nFoundRow = 0

            _LogMessage = "Find Record in publisher from subscriber list " & nRow & " - Start"
            LogInformation.EnterLog(_LogMessage, _LogFileName)

            nFoundRow = C1Publisher.FindRow(C1Subscriber.GetData(nRow, COL_DOCUMENTFILENAME), 1, COL_DOCUMENTFILENAME, False, True, False)

            If nFoundRow > 0 Then
                _LogMessage = "Record Found in publisher from subscriber list " & nRow
                LogInformation.EnterLog(_LogMessage, _LogFileName)

                'Found in Publisher
                If C1Subscriber.Rows(nRow).Item(COL_VERSIONNO) > C1Publisher.Rows(nFoundRow).Item(COL_VERSIONNO) Then
                    _LogMessage = "Subscriber Version is greater than publisher version " & nRow
                    LogInformation.EnterLog(_LogMessage, _LogFileName)

                    C1ModNotInPublisher.Rows.Add()
                    For nCol = 0 To C1Subscriber.Cols.Count - 1
                        C1ModNotInPublisher.SetData(C1ModNotInPublisher.Rows.Count - 1, nCol, C1Subscriber.GetData(nRow, nCol) & "")
                    Next
                    _LogMessage = "Subscriber modified document add in list of Modified Not In Publisher" & nRow
                    LogInformation.EnterLog(_LogMessage, _LogFileName)

                    'C1Subscriber.Rows.Remove(nRow)
                ElseIf C1Subscriber.Rows(nRow).Item(COL_VERSIONNO) = C1Publisher.Rows(nFoundRow).Item(COL_VERSIONNO) Then
                    _LogMessage = "Subscriber Version is same with publisher version so compare document date " & nRow
                    LogInformation.EnterLog(_LogMessage, _LogFileName)

                    If Date.Compare(Format(C1Subscriber.Rows(nRow).Item(COL_MODYDATETIME), "MM/dd/yyyy hh:mm:ss tt"), Format(C1Publisher.Rows(nFoundRow).Item(COL_MODYDATETIME), "MM/dd/yyyy hh:mm:ss tt")) <> 0 Then
                        'If C1Subscriber.Rows(nRow).Item(COL_MODYDATETIME) > C1Publisher.Rows(nFoundRow).Item(COL_MODYDATETIME) Then
                        If PublisherIsWinner = False Then
                            C1ConflictInPublihser.Rows.Add()
                            For nCol = 0 To C1Subscriber.Cols.Count - 1
                                C1ConflictInPublihser.SetData(C1ConflictInPublihser.Rows.Count - 1, nCol, C1Subscriber.GetData(nRow, nCol) & "")
                            Next

                            _LogMessage = "Subscriber date not match with publisher date so its add in Conflict in publisher list " & nRow
                            LogInformation.EnterLog(_LogMessage, _LogFileName)

                            'C1Subscriber.Rows.Remove(nRow)
                        End If
                    End If
                End If
            Else
                _LogMessage = "Record Not Found in publisher from subscriber list " & nRow
                LogInformation.EnterLog(_LogMessage, _LogFileName)

                'Not in Publisher
                C1NotInPublisher.Rows.Add()
                For nCol = 0 To C1Subscriber.Cols.Count - 1
                    C1NotInPublisher.SetData(C1NotInPublisher.Rows.Count - 1, nCol, C1Subscriber.GetData(nRow, nCol) & "")
                Next

                _LogMessage = "Subscriber new document add in list of Not In Publisher list " & nRow
                LogInformation.EnterLog(_LogMessage, _LogFileName)
                'C1Subscriber.Rows.Remove(nRow)
            End If

            _LogMessage = "Find Record in publisher from subscriber list " & nRow & " - Finish"
            LogInformation.EnterLog(_LogMessage, _LogFileName)

        Next

        _LogMessage = "Fill document from subscriber to publisher - Finish"
        LogInformation.EnterLog(_LogMessage, _LogFileName)

        Application.DoEvents()

        '---Find Documents From Publisher to Subscriber---
        For nRow = C1Publisher.Rows.Count - 1 To 1 Step -1
            nFoundRow = 0
            nFoundRow = C1Subscriber.FindRow(C1Publisher.GetData(nRow, COL_DOCUMENTFILENAME), 1, COL_DOCUMENTFILENAME, False, True, False)
            If nFoundRow > 0 Then
                'Found in Publisher
                If C1Publisher.Rows(nRow).Item(COL_VERSIONNO) > C1Subscriber.Rows(nFoundRow).Item(COL_VERSIONNO) Then
                    C1ModNotInSubscriber.Rows.Add()
                    For nCol = 0 To C1Publisher.Cols.Count - 1
                        C1ModNotInSubscriber.SetData(C1ModNotInSubscriber.Rows.Count - 1, nCol, C1Publisher.GetData(nRow, nCol) & "")
                    Next
                    'C1Publisher.Rows.Remove(nRow)
                ElseIf C1Publisher.Rows(nRow).Item(COL_VERSIONNO) = C1Subscriber.Rows(nFoundRow).Item(COL_VERSIONNO) Then
                    If Date.Compare(Format(C1Publisher.Rows(nRow).Item(COL_MODYDATETIME), "MM/dd/yyyy hh:mm:ss tt"), Format(C1Subscriber.Rows(nFoundRow).Item(COL_MODYDATETIME), "MM/dd/yyyy hh:mm:ss tt")) <> 0 Then
                        'If C1Publisher.Rows(nRow).Item(COL_MODYDATETIME) > C1Subscriber.Rows(nFoundRow).Item(COL_MODYDATETIME) Then
                        If PublisherIsWinner = True Then
                            C1ConflictInSubscriber.Rows.Add()
                            For nCol = 0 To C1Publisher.Cols.Count - 1
                                C1ConflictInSubscriber.SetData(C1ConflictInSubscriber.Rows.Count - 1, nCol, C1Publisher.GetData(nRow, nCol) & "")
                            Next
                            'C1Publisher.Rows.Remove(nRow)
                        End If
                    End If
                End If
            Else
                'Not in Publisher
                C1NotInSubscriber.Rows.Add()
                For nCol = 0 To C1Publisher.Cols.Count - 1
                    C1NotInSubscriber.SetData(C1NotInSubscriber.Rows.Count - 1, nCol, C1Publisher.GetData(nRow, nCol) & "")
                Next
                'C1Publisher.Rows.Remove(nRow)
            End If
        Next
        'button 2 click
        'Dim dtPublisherNew As New DataTable
        'Dim dtSubscriberNew As New DataTable

        'Dim dtPublisherMod As New DataTable
        'Dim dtSubscriberMod As New DataTable

        'Dim dtPublisherConflict As New DataTable
        'Dim dtSubscriberConflict As New DataTable

        'dtPublisherNew = dtPublisher.Clone : dtPublisherMod = dtPublisher.Clone : dtPublisherConflict = dtPublisher.Clone
        'dtSubscriberNew = dtPublisher.Clone : dtSubscriberMod = dtPublisher.Clone : dtSubscriberConflict = dtPublisher.Clone

        'Dim nCount As Integer

        'Dim dr As DataRow
        'Dim nCount1 As Int16
        'For nCount = 0 To dtPublisher.Rows.Count - 1
        '    If dtSubscriber.Select("DocumentFileName=" & dtPublisher.Rows(nCount).Item("DocumentFileName")).GetUpperBound(0) = -1 Then
        '        dr = dtPublisherNew.NewRow
        '        For nCount1 = 0 To dtPublisherNew.Columns.Count - 1
        '            dr(nCount1) = dtPublisher.Rows(nCount).Item(nCount1)
        '        Next
        '        dtPublisherNew.Rows.Add(dr)
        '    Else
        '        'Dim nSubscriberRow As DataRow = dtSubscriber.Select(dtPublisher.Rows(nCount).Item("DocumentFileName"))
        '        Dim nSubscriberRowID As Integer
        '        'Dim nSubscriberRow As DataRow = dtSubscriber.Select(dtPublisher.Rows(nCount).Item("DocumentFileName"))
        '        'Dim nSubscriberRow As DataRow
        '        'nSubscriberRow = dtSubscriber.Rows.Item(1)
        '        'Dim i As IList
        '        'nSubscriberRowID = CType(dtSubscriber.Rows, IList).IndexOf(nSubscriberRow)

        '        If dtPublisher.Rows(nCount).Item("VersionNo") <> dtSubscriber.Rows(nCount).Item("VersionNo") Then
        '            If dtPublisher.Rows(nCount).Item("VersionNo") > dtSubscriber.Rows(nCount).Item("VersionNo") Then
        '                dr = dtPublisherMod.NewRow
        '                For nCount1 = 0 To dtPublisherMod.Columns.Count - 1
        '                    dr(nCount1) = dtPublisher.Rows(nCount).Item(nCount1)
        '                Next
        '                dtPublisherMod.Rows.Add(dr)
        '            End If
        '        Else
        '            If dtPublisher.Rows(nCount).Item("ModifyDateTime") > dtSubscriber.Rows(nCount).Item("ModifyDateTime") Then
        '                dr = dtPublisherConflict.NewRow
        '                For nCount1 = 0 To dtPublisherMod.Columns.Count - 1
        '                    dr(nCount1) = dtPublisher.Rows(nCount).Item(nCount1)
        '                Next
        '                dtPublisherConflict.Rows.Add(dr)
        '            End If
        '        End If
        '    End If
        'Next

        'C1NotInSubscriber.DataSource = dtPublisherNew
        'C1ModNotInSubscriber.DataSource = dtPublisherMod
        'C1ConflictInSubscriber.DataSource = dtPublisherConflict

        ''----------------


        'For nCount = 0 To dtPublisher.Rows.Count - 1
        '    If dtPublisher.Select("DocumentFileName=" & dtSubscriber.Rows(nCount).Item("DocumentFileName")).GetUpperBound(0) = -1 Then
        '        dr = dtSubscriberNew.NewRow
        '        For nCount1 = 0 To dtSubscriberNew.Columns.Count - 1
        '            dr(nCount1) = dtSubscriber.Rows(nCount).Item(nCount1)
        '        Next
        '        dtSubscriberNew.Rows.Add(dr)
        '    Else
        '        If dtSubscriber.Rows(nCount).Item("VersionNo") <> dtPublisher.Rows(nCount).Item("VersionNo") Then
        '            If dtSubscriber.Rows(nCount).Item("VersionNo") > dtPublisher.Rows(nCount).Item("VersionNo") Then
        '                dr = dtSubscriberMod.NewRow
        '                For nCount1 = 0 To dtSubscriberMod.Columns.Count - 1
        '                    dr(nCount1) = dtSubscriber.Rows(nCount).Item(nCount1)
        '                Next
        '                dtSubscriberMod.Rows.Add(dr)
        '            End If
        '        Else
        '            If dtSubscriber.Rows(nCount).Item("ModifyDateTime") > dtPublisher.Rows(nCount).Item("ModifyDateTime") Then
        '                dr = dtSubscriberConflict.NewRow
        '                For nCount1 = 0 To dtSubscriberMod.Columns.Count - 1
        '                    dr(nCount1) = dtSubscriber.Rows(nCount).Item(nCount1)
        '                Next
        '                dtSubscriberConflict.Rows.Add(dr)
        '            End If
        '        End If
        '    End If
        'Next

        'C1NotInPublisher.DataSource = dtSubscriberNew
        'C1ModNotInPublisher.DataSource = dtSubscriberMod
        'C1ConflictInPublihser.DataSource = dtSubscriberConflict

        'DesignGrid(C1NotInPublisher)
        'DesignGrid(C1NotInSubscriber)
        'DesignGrid(C1ModNotInPublisher)
        'DesignGrid(C1ModNotInSubscriber)
        'DesignGrid(C1ConflictInPublihser)
        'DesignGrid(C1ConflictInSubscriber)
    End Sub

    Public Sub Fill_Documents_WithVersion(ByVal C1NotInPublisher As C1FlexGrid, ByVal C1NotInSubscriber As C1FlexGrid, ByVal C1ModNotInPublisher As C1FlexGrid, ByVal C1ModNotInSubscriber As C1FlexGrid, ByVal C1ConflictInPublihser As C1FlexGrid, ByVal C1ConflictInSubscriber As C1FlexGrid)
        Dim dtPublisher As New DataTable
        Dim dtSubscriber As New DataTable
        Dim oDB As gloStream.gloDataBase.gloDataBase
        Dim _SQLQuery As String

        _SQLQuery = "SELECT DocumentID,DocumentName,Extension,SourceMachine,SystemFolder,Container,Category,PatientID,[Year], " _
        & " [Month],DocumentFormat,SourceBin,Pages,ArchiveStatus,ArchiveDescription,UsedStatus,UsedMachine, " _
        & " UsedType,DocumentType,DocumentFileName,MachineID,Modified,Synchronized, " _
        & " (SystemFolder+'\'+Container+'\'+Category+'\'+convert(varchar(60),PatientID)+'\'+[Year]+'\'+[Month]+'\'+convert(varchar(22),DocumentFileName)+'.'+Extension) As DocPath,VersionNo,ModifyDateTime " _
        & " FROM DMS_MST Where DocumentType=2 AND DocumentFileName IS NOT NULL ORDER BY CATEGORY"

        oDB = New gloStream.gloDataBase.gloDataBase
        oDB.Connect(gloEMRAdmin.mdlgloDMS.GetConnectionString_DMS(gloStream.Supporting.ConnectionType.Publisher))
        dtPublisher = oDB.ReadQueryData(_SQLQuery)
        oDB.Disconnect()
        oDB = Nothing

        oDB = New gloStream.gloDataBase.gloDataBase
        oDB.Connect(gloEMRAdmin.mdlgloDMS.GetConnectionString_DMS(gloStream.Supporting.ConnectionType.Subscriber))
        dtSubscriber = oDB.ReadQueryData(_SQLQuery)
        oDB.Disconnect()
        oDB = Nothing

        dtPublisher.Columns.Add(New DataColumn("Select", GetType(Boolean)))
        dtSubscriber.Columns.Add(New DataColumn("Select", GetType(Boolean)))

        'button 2 click
        Dim dtPublisherNew As New DataTable
        Dim dtSubscriberNew As New DataTable

        Dim dtPublisherMod As New DataTable
        Dim dtSubscriberMod As New DataTable

        Dim dtPublisherConflict As New DataTable
        Dim dtSubscriberConflict As New DataTable

        dtPublisherNew = dtPublisher.Clone : dtPublisherMod = dtPublisher.Clone : dtPublisherConflict = dtPublisher.Clone
        dtSubscriberNew = dtPublisher.Clone : dtSubscriberMod = dtPublisher.Clone : dtSubscriberConflict = dtPublisher.Clone

        Dim nCount As Integer

        Dim dr As DataRow
        Dim nCount1 As Int16
        For nCount = 0 To dtPublisher.Rows.Count - 1
            If dtSubscriber.Select("DocumentFileName=" & dtPublisher.Rows(nCount).Item("DocumentFileName")).GetUpperBound(0) = -1 Then
                dr = dtPublisherNew.NewRow
                For nCount1 = 0 To dtPublisherNew.Columns.Count - 1
                    dr(nCount1) = dtPublisher.Rows(nCount).Item(nCount1)
                Next
                dtPublisherNew.Rows.Add(dr)
            Else
                'Dim nSubscriberRow As DataRow = dtSubscriber.Select(dtPublisher.Rows(nCount).Item("DocumentFileName"))
                Dim nSubscriberRowID As Integer
                'Dim nSubscriberRow As DataRow = dtSubscriber.Select(dtPublisher.Rows(nCount).Item("DocumentFileName"))
                'Dim nSubscriberRow As DataRow
                'nSubscriberRow = dtSubscriber.Rows.Item(1)
                'Dim i As IList
                'nSubscriberRowID = CType(dtSubscriber.Rows, IList).IndexOf(nSubscriberRow)

                If dtPublisher.Rows(nCount).Item("VersionNo") <> dtSubscriber.Rows(nCount).Item("VersionNo") Then
                    If dtPublisher.Rows(nCount).Item("VersionNo") > dtSubscriber.Rows(nCount).Item("VersionNo") Then
                        dr = dtPublisherMod.NewRow
                        For nCount1 = 0 To dtPublisherMod.Columns.Count - 1
                            dr(nCount1) = dtPublisher.Rows(nCount).Item(nCount1)
                        Next
                        dtPublisherMod.Rows.Add(dr)
                    End If
                Else
                    If dtPublisher.Rows(nCount).Item("ModifyDateTime") > dtSubscriber.Rows(nCount).Item("ModifyDateTime") Then
                        dr = dtPublisherConflict.NewRow
                        For nCount1 = 0 To dtPublisherMod.Columns.Count - 1
                            dr(nCount1) = dtPublisher.Rows(nCount).Item(nCount1)
                        Next
                        dtPublisherConflict.Rows.Add(dr)
                    End If
                End If
            End If
        Next

        C1NotInSubscriber.DataSource = dtPublisherNew
        C1ModNotInSubscriber.DataSource = dtPublisherMod
        C1ConflictInSubscriber.DataSource = dtPublisherConflict

        '----------------


        For nCount = 0 To dtPublisher.Rows.Count - 1
            If dtPublisher.Select("DocumentFileName=" & dtSubscriber.Rows(nCount).Item("DocumentFileName")).GetUpperBound(0) = -1 Then
                dr = dtSubscriberNew.NewRow
                For nCount1 = 0 To dtSubscriberNew.Columns.Count - 1
                    dr(nCount1) = dtSubscriber.Rows(nCount).Item(nCount1)
                Next
                dtSubscriberNew.Rows.Add(dr)
            Else
                If dtSubscriber.Rows(nCount).Item("VersionNo") <> dtPublisher.Rows(nCount).Item("VersionNo") Then
                    If dtSubscriber.Rows(nCount).Item("VersionNo") > dtPublisher.Rows(nCount).Item("VersionNo") Then
                        dr = dtSubscriberMod.NewRow
                        For nCount1 = 0 To dtSubscriberMod.Columns.Count - 1
                            dr(nCount1) = dtSubscriber.Rows(nCount).Item(nCount1)
                        Next
                        dtSubscriberMod.Rows.Add(dr)
                    End If
                Else
                    If dtSubscriber.Rows(nCount).Item("ModifyDateTime") > dtPublisher.Rows(nCount).Item("ModifyDateTime") Then
                        dr = dtSubscriberConflict.NewRow
                        For nCount1 = 0 To dtSubscriberMod.Columns.Count - 1
                            dr(nCount1) = dtSubscriber.Rows(nCount).Item(nCount1)
                        Next
                        dtSubscriberConflict.Rows.Add(dr)
                    End If
                End If
            End If
        Next

        C1NotInPublisher.DataSource = dtSubscriberNew
        C1ModNotInPublisher.DataSource = dtSubscriberMod
        C1ConflictInPublihser.DataSource = dtSubscriberConflict

        DesignGrid(C1NotInPublisher)
        DesignGrid(C1NotInSubscriber)
        DesignGrid(C1ModNotInPublisher)
        DesignGrid(C1ModNotInSubscriber)
        DesignGrid(C1ConflictInPublihser)
        DesignGrid(C1ConflictInSubscriber)
    End Sub

    Public Sub Fill_Documents_WithourVersion(ByVal C1NotInPublisher As C1FlexGrid, ByVal C1NotInSubscriber As C1FlexGrid, ByVal C1ModNotInPublisher As C1FlexGrid, ByVal C1ModNotInSubscriber As C1FlexGrid, ByVal C1Conflict As C1FlexGrid)
        Dim dtPublisher As New DataTable
        Dim dtSubscriber As New DataTable
        Dim objDB As New gloStream.gloDataBase.gloDataBase
        Dim _strSQL As String

        _strSQL = "SELECT DocumentID,DocumentName,Extension,SourceMachine,SystemFolder,Container,Category,PatientID,[Year], " _
        & " [Month],DocumentFormat,SourceBin,Pages,ArchiveStatus,ArchiveDescription,UsedStatus,UsedMachine, " _
        & " UsedType,DocumentType,DocumentFileName,MachineID,Modified,Synchronized, " _
        & " (SystemFolder+'\'+Container+'\'+Category+'\'+convert(varchar(60),PatientID)+'\'+[Year]+'\'+[Month]+'\'+convert(varchar(22),DocumentFileName)+'.'+Extension) As DocPath " _
        & " FROM DMS_MST Where DocumentType=2 AND Modified=1 AND DocumentFileName IS NOT NULL ORDER BY CATEGORY"

        'Publisher

        objDB.Connect(gloEMRAdmin.mdlgloDMS.GetConnectionString_DMS(gloStream.Supporting.ConnectionType.Publisher))

        dtPublisher = objDB.ReadQueryData(_strSQL)
        objDB.Disconnect()

        'Subscriber
        objDB.Connect(gloEMRAdmin.mdlgloDMS.GetConnectionString_DMS(gloStream.Supporting.ConnectionType.Subscriber))
        dtSubscriber = objDB.ReadQueryData(_strSQL)
        objDB.Disconnect()

        dtSubscriber.Columns.Add(New DataColumn("Select", GetType(Boolean)))
        dtPublisher.Columns.Add(New DataColumn("Select", GetType(Boolean)))

        Dim dtPublisherNew As New DataTable
        Dim dtSubscriberNew As New DataTable
        dtPublisherNew = objDB.FindDifferenceTable(dtSubscriber, dtPublisher, "DocumentFileName", True)
        dtSubscriberNew = objDB.FindDifferenceTable(dtPublisher, dtSubscriber, "DocumentFileName", True)



        C1NotInPublisher.DataSource = dtPublisherNew
        C1NotInSubscriber.DataSource = dtSubscriberNew
        '---
        'Dim dtPublisherModified As New DataTable
        'Dim dtSubscriberModified As New DataTable
        'dtPublisherModified = objDB.FindDifferenceTable(dtSubscriber, dtPublisherNew, "DocumentFileName", True, "Category", False)
        'dtSubscriberModified = objDB.FindDifferenceTable(dtPublisher, dtSubscriberNew, "DocumentFileName", True, "Category", False)

        'C1ModNotInPublisher.DataSource = dtPublisherModified
        'C1ModNotInSubscriber.DataSource = dtSubscriberModified
        '---

        Dim dtPublisherRemaining As New DataTable
        Dim dtSubscriberRemaining As New DataTable
        dtPublisherRemaining = objDB.FindDifferenceTable(dtSubscriber, dtPublisherNew, "DocumentFileName", True)
        dtSubscriberRemaining = objDB.FindDifferenceTable(dtPublisher, dtSubscriberNew, "DocumentFileName", True)

        Dim dtCommon As New DataTable
        Dim dtPublisherRemainingRemaining As New DataTable
        Dim dtSubscriberRemainingRemaining As New DataTable
        dtCommon = objDB.CommonRecordsInDMS(dtPublisherRemaining, dtSubscriberRemaining, "Category", False)
        dtPublisherRemainingRemaining = objDB.FindDifferenceTable(dtPublisherRemaining, dtCommon, "DocumentFileName", True)
        dtSubscriberRemainingRemaining = objDB.FindDifferenceTable(dtSubscriberRemaining, dtCommon, "DocumentFileName", True)

        C1Conflict.DataSource = dtCommon
        C1ModNotInPublisher.DataSource = dtPublisherRemainingRemaining
        C1ModNotInSubscriber.DataSource = dtSubscriberRemainingRemaining




        DesignGrid(C1NotInPublisher)
        DesignGrid(C1NotInSubscriber)
        DesignGrid(C1ModNotInPublisher)
        DesignGrid(C1ModNotInSubscriber)
        DesignGrid(C1Conflict)
        '---
    End Sub

    ''Sudhir 20090107
    Public Function Get_gloEMR_ProviderList() As DataTable
        Dim oDataTable As DataTable
        Dim _SQLQuery As String
        Dim oDB As gloStream.gloDataBase.gloDataBase
        Try
            '_SQLQuery = "SELECT nProviderID, sLastName +'  '+sFirstName AS ProviderName, sExternalCode FROM Provider_MST"
            _SQLQuery = "SELECT nProviderID, (ISNULL(sLastName,'') + SPACE(2) + ISNULL(sFirstName,'')) AS ProviderName, sExternalCode FROM Provider_MST"
            oDB = New gloStream.gloDataBase.gloDataBase
            oDB.Connect(gloEMRAdmin.mdlGeneral.GetConnectionString)
            oDataTable = oDB.ReadQueryData(_SQLQuery)
            Return oDataTable
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function Get_gloPM_ProviderList(ByVal PMConnectionString As String) As String
        Dim oDataTable As DataTable
        Dim _SQLQuery As String
        Dim _strPMProvider As String = " "
        Dim oDB As gloStream.gloDataBase.gloDataBase

        Try
            '_SQLQuery = "SELECT nProviderID, sLastName +'  '+ sFirstName AS ProviderName FROM Provider_MST"
            _SQLQuery = "SELECT nProviderID, (ISNULL(sLastName,'') + SPACE(2) + ISNULL(sFirstName,'')) AS ProviderName FROM Provider_MST"
            oDB = New gloStream.gloDataBase.gloDataBase
            oDB.Connect(PMConnectionString)
            oDataTable = oDB.ReadQueryData(_SQLQuery)

            If Not oDataTable Is Nothing Then
                If oDataTable.Rows.Count > 0 Then
                    For i As Integer = 0 To oDataTable.Rows.Count - 1
                        'If i = 0 Then
                        '_strPMProvider = oDataTable.Rows(i)("ProviderName").ToString()
                        'Else
                        _strPMProvider = _strPMProvider & "|" & oDataTable.Rows(i)("ProviderName").ToString()
                        'End If

                    Next
                End If
            End If
            Return _strPMProvider
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function Get_gloPM_Provider(ByVal PMConnectionString As String, ByVal ExternalID As String) As String
        Dim oDataTable As DataTable
        Dim _SQLQuery As String
        Dim _strPMProvider As String = ""
        Dim oDB As gloStream.gloDataBase.gloDataBase

        Try
            '_SQLQuery = "SELECT nProviderID, sLastName +'  '+ sFirstName AS ProviderName FROM Provider_MST WHERE sExternalCode = '" & ExternalID & "'"
            _SQLQuery = "SELECT nProviderID, (ISNULL(sLastName,'') + SPACE(2) + ISNULL(sFirstName,'')) AS ProviderName FROM Provider_MST WHERE sExternalCode = '" & ExternalID & "'"
            oDB = New gloStream.gloDataBase.gloDataBase
            oDB.Connect(PMConnectionString)
            oDataTable = oDB.ReadQueryData(_SQLQuery)

            If IsNothing(oDataTable) = False Then
                If oDataTable.Rows.Count > 0 Then
                    _strPMProvider = oDataTable.Rows(0)("ProviderName").ToString
                End If
            End If

            Return _strPMProvider
        Catch ex As Exception
            Return _strPMProvider
        End Try
    End Function

    Public Function IsExternalIDPresent(ByVal RowDetail As myList, ByVal PMConnectionString As String) As Boolean
        Dim _result As Boolean = False
        Dim _cnt As Object
        Dim _SQLQuery As String
        Dim oDB As gloStream.gloDataBase.gloDataBase

        Try
            _SQLQuery = "SELECT COUNT(*) FROM Provider_MST WHERE sExternalCode= '" & RowDetail.ExternalID & "' AND sLastName+'  '+sFirstName <> '" & RowDetail.gloEMR_Provider & "'"
            oDB = New gloStream.gloDataBase.gloDataBase
            oDB.Connect(gloEMRAdmin.mdlGeneral.GetConnectionString)
            _cnt = oDB.ExecuteQueryScaler(_SQLQuery)
            oDB.Disconnect()
            If Convert.ToInt16(_cnt) > 0 Then
                _result = True
                Return _result
            End If

            _SQLQuery = "SELECT COUNT(*) FROM Provider_MST WHERE sExternalCode= '" & RowDetail.ExternalID & "' AND sLastName+'  '+sFirstName <> '" & RowDetail.gloPM_Provider & "'"
            oDB.Connect(PMConnectionString)
            _cnt = oDB.ExecuteQueryScaler(_SQLQuery)
            If Convert.ToInt16(_cnt) > 0 Then
                _result = True
                Return _result
            End If

            Return _result
        Catch ex As Exception

        End Try
    End Function

    ''end Sudhir

    Public Sub DesignGrid(ByVal C1DesignGrid As C1FlexGrid)
        With C1DesignGrid
            .ScrollBars = ScrollBars.Both
            '.Rows.Count = 2
            If .Rows.Count >= 1 Then .Rows.Fixed = 1
            .Cols.Fixed = 0
            .Cols.Count = COL_COUNT
            .AllowEditing = False

            .Cols(COL_ID).Visible = False
            .Cols(COL_NAME).Visible = True
            .Cols(COL_EXTENSION).Visible = False
            .Cols(COL_SOURCEMACHINE).Visible = False
            .Cols(COL_SYSTEMFOLDER).Visible = False
            .Cols(COL_CONTAINER).Visible = False
            .Cols(COL_CATEGORY).Visible = True
            .Cols(COL_PATIENTID).Visible = False
            .Cols(COL_YEAR).Visible = False
            .Cols(COL_MONTH).Visible = False
            .Cols(COL_DOCUMENTFORMAT).Visible = False
            .Cols(COL_SOURCEBIN).Visible = False
            .Cols(COL_PAGES).Visible = False
            .Cols(COL_ARCHSTATUS).Visible = False
            .Cols(COL_ARCHDESC).Visible = False
            .Cols(COL_USEDSTATUS).Visible = False
            .Cols(COL_USEDMACHINE).Visible = False
            .Cols(COL_USEDTYPE).Visible = False
            .Cols(COL_DOCUMENTTYPE).Visible = False
            .Cols(COL_DOCUMENTFILENAME).Visible = False
            .Cols(COL_MACHINEID).Visible = False
            .Cols(COL_MODIFIED).Visible = False
            .Cols(COL_SYNCHRONIZED).Visible = False
            .Cols(COL_PATH).Visible = False
            .Cols(COL_VERSIONNO).Visible = False
            .Cols(COL_MODYDATETIME).Visible = False
            .Cols(COL_ISREVIWED).Visible = False
            .Cols(COL_SYNCHSTATUS).Visible = True


            .Cols(COL_SYNCHSTATUS).DataType = GetType(Boolean)
            '.Cols(COL_SYNCHSTATUS).AllowEditing = True

            If .Rows.Count > 0 Then
                .SetData(0, COL_NAME, "Document Name")
                .SetData(0, COL_CATEGORY, "Category")
                .SetData(0, COL_SYNCHSTATUS, "Status")
            End If

            Dim _Width As Single = (.Width - 20) / 6
            .Cols(COL_PATH).Width = 0
            .Cols(COL_NAME).Width = _Width * 3
            .Cols(COL_CATEGORY).Width = _Width * 2
            .Cols(COL_SYNCHSTATUS).Width = _Width * 1


        End With
    End Sub

    Private Function GetTroubleshootingFileName(ByVal TroubleShootingType As gloStream.Supporting.TroubleShootingType) As String
        Select Case TroubleShootingType
            Case gloStream.Supporting.TroubleShootingType.None
                Return ""
            Case gloStream.Supporting.TroubleShootingType.Publisher
                Return gTroubleShooting_Publisher
            Case gloStream.Supporting.TroubleShootingType.Subscriber
                Return gTroubleShooting_Subscriber
            Case gloStream.Supporting.TroubleShootingType.NotInPublisher
                Return gTroubleShooting_NotInPublisher
            Case gloStream.Supporting.TroubleShootingType.NotInSubscriber
                Return gTroubleShooting_NotInSubscriber
            Case gloStream.Supporting.TroubleShootingType.ModNotInPublisher
                Return gTroubleShooting_ModNotInPublisher
            Case gloStream.Supporting.TroubleShootingType.ModNotInSubscriber
                Return gTroubleShooting_ModNotInSubscriber
            Case gloStream.Supporting.TroubleShootingType.ConflictPublisherToSubscriber
                Return gTroubleShooting_ConflictPublisherToSubscriber
            Case gloStream.Supporting.TroubleShootingType.ConflictSubscriberToPublisher
                Return gTroubleShooting_ConflictSubscriberToPublisher
        End Select
    End Function

    Public Function Troublshooting(ByVal TroublshootingPath As String, ByVal TroubleShootingType As gloStream.Supporting.TroubleShootingType, ByVal Message As String, Optional ByVal PublisherSubscriberName As String = "")
        If TroubleShootingType <> gloStream.Supporting.TroubleShootingType.None Then
            Dim oLogFile As String = TroublshootingPath & "\" & GetTroubleshootingFileName(TroubleShootingType)
            Select Case TroubleShootingType
                Case gloStream.Supporting.TroubleShootingType.Publisher
                    If File.Exists(oLogFile) = False Then
                        Dim oPublisher As New gloStream.gloDMS.gloSync.Subscriber.Subscriber
                        Dim oPublisherDetail As New gloStream.gloDMS.gloSync.Subscriber.PublisherSubscriberDetail

                        oPublisherDetail = oPublisher.GetPublisher(PublisherSubscriberName)
                        If Not oPublisherDetail Is Nothing Then
                            Message = ""
                            Message = Message & "Publisher Name : " & oPublisherDetail.Name & vbCrLf
                            Message = Message & "Server Name : " & oPublisherDetail.ServerName & vbCrLf
                            Message = Message & "Database Name : " & oPublisherDetail.DatabaseName & vbCrLf
                            Message = Message & "DMS Path : " & oPublisherDetail.DMSPath & vbCrLf
                            Message = Message & "Authontication : " & oPublisherDetail.ConnectionAuthontication.ToString & vbCrLf
                            Message = Message & "SQL Connection Login Name : " & oPublisherDetail.ConnectionLoginName & vbCrLf
                            Message = Message & "SQL Connection Login Password : " & oPublisherDetail.ConnectionPassword & vbCrLf
                            Message = Message & "Is Defualt : " & oPublisherDetail.IsSelect.ToString
                        End If

                        oPublisherDetail = Nothing
                        oPublisher = Nothing

                        Dim oFilePublisher As New System.IO.StreamWriter(oLogFile)
                        oFilePublisher.WriteLine(Message)
                        oFilePublisher.Close()
                    End If
                Case gloStream.Supporting.TroubleShootingType.Subscriber
                    If File.Exists(oLogFile) = False Then
                        Dim oSubscriber As New gloStream.gloDMS.gloSync.Subscriber.Subscriber
                        Dim oSubscriberDetail As New gloStream.gloDMS.gloSync.Subscriber.PublisherSubscriberDetail

                        oSubscriberDetail = oSubscriber.GetSubscriber(PublisherSubscriberName)
                        If Not oSubscriberDetail Is Nothing Then
                            Message = ""
                            Message = Message & "Subscriber Name : " & oSubscriberDetail.Name & vbCrLf
                            Message = Message & "Server Name : " & oSubscriberDetail.ServerName & vbCrLf
                            Message = Message & "Database Name : " & oSubscriberDetail.DatabaseName & vbCrLf
                            Message = Message & "DMS Path : " & oSubscriberDetail.DMSPath & vbCrLf
                            Message = Message & "Authontication : " & oSubscriberDetail.ConnectionAuthontication.ToString & vbCrLf
                            Message = Message & "SQL Connection Login Name : " & oSubscriberDetail.ConnectionLoginName & vbCrLf
                            Message = Message & "SQL Connection Login Password : " & oSubscriberDetail.ConnectionPassword & vbCrLf
                            Message = Message & "Is Defualt : " & oSubscriberDetail.IsSelect.ToString
                        End If

                        oSubscriberDetail = Nothing
                        oSubscriber = Nothing

                        Dim oFileSubscriber As New System.IO.StreamWriter(oLogFile)
                        oFileSubscriber.WriteLine(Message)
                        oFileSubscriber.Close()
                    End If
                Case Else
                    Dim oFile As New System.IO.StreamWriter(oLogFile, True)
                    oFile.WriteLine(Message)
                    oFile.Close()
            End Select
        End If
    End Function

    Public Function NewTroublshooting(ByVal PublisherName As String, ByVal SubscriberName As String) As String
        Dim _Date As String = Format(Date.Now, "MM/dd/yyyy")
        Dim _Time As String = Format(Date.Now, "hh:mm:ss tt")
        Dim _NewTroubleshootingName As String = Replace(_Date, "/", " ") & " - " & Replace(_Time, ":", " ")
        Dim i As Int16 = 1
        Dim _RootPath As String = gTroubleShooting_RootPath & "\" & PublisherName & "-" & SubscriberName & "-" & _NewTroubleshootingName
        While Directory.Exists(_RootPath)
            _RootPath = gTroubleShooting_RootPath & "\" & PublisherName & "-" & SubscriberName & "-" & _NewTroubleshootingName & "-" & i
        End While

        MkDir(_RootPath)
        If Directory.Exists(_RootPath) = True Then
            Return _RootPath
        End If
    End Function

    Public Function DeleteTroublshooting(ByVal TroublshootingID As String) As String

    End Function

    Private Function DifferenceTable(ByVal findTable As DataTable, ByVal containertable As DataTable) As DataTable
        Dim nCount As Integer
        Dim dtDifferenece As New DataTable
        dtDifferenece = findTable.Clone
        Dim dr As DataRow
        Dim nCount1 As Int16
        For nCount = 0 To findTable.Rows.Count - 1
            If containertable.Select("DocumentFileName=" & findTable.Rows(nCount).Item("DocumentFileName")).GetUpperBound(0) = -1 Then
                dr = dtDifferenece.NewRow
                For nCount1 = 0 To dtDifferenece.Columns.Count - 1
                    dr(nCount1) = findTable.Rows(nCount).Item(nCount1)
                Next
                dtDifferenece.Rows.Add(dr)
            End If
        Next
        Return dtDifferenece
    End Function

    Private Function CommonRecords(ByVal findTable As DataTable, ByVal containertable As DataTable, Optional ByVal strCategoryName As String = "") As DataTable
        Dim nCount As Integer
        Dim dtDifferenece As New DataTable
        dtDifferenece = findTable.Clone
        Dim dr As DataRow
        Dim nCount1 As Int16
        Dim strCondition As String
        For nCount = 0 To findTable.Rows.Count - 1
            strCondition = "DocumentFileName=" & findTable.Rows(nCount).Item("DocumentFileName")
            If strCategoryName <> "" Then
                strCondition = strCondition & " AND Category='" & findTable.Rows(nCount).Item("Category") & "'"
            End If
            If containertable.Select(strCondition).GetUpperBound(0) >= 0 Then
                dr = dtDifferenece.NewRow
                For nCount1 = 0 To dtDifferenece.Columns.Count - 1
                    dr(nCount1) = findTable.Rows(nCount).Item(nCount1)
                Next
                dtDifferenece.Rows.Add(dr)
            End If
        Next
        Return dtDifferenece
    End Function

End Module

Public Class LogInformation

    Public Shared Function NewLog() As String
        Dim _FilePath As String
        Dim _FileName As String = Replace(Replace(Replace(Format(Date.Now, "dd/MM/yyyy hh:mm:tt"), "/", " "), ":", " "), "-", " ")
        Dim i As Int16 = 1
        _FilePath = Application.StartupPath & "\" & _FileName

        While File.Exists(_FilePath)
            i = i + 1
            _FilePath = Application.StartupPath & "\" & _FileName & " " & i
        End While

        Return _FilePath & ".ini"
    End Function

    Public Shared Function EnterLog(ByVal LogMessage As String, ByVal LogPath As String) As Boolean
        Try
            Dim _Message As String = Format(Date.Now, "dd/MM/yyyy hh:mm:ss:ms:tt") & " " & LogMessage & vbCrLf

            If File.Exists(LogPath) = True Then
                Dim stmReader As StreamReader = New StreamReader(LogPath)
                Dim _OldMessage As String = stmReader.ReadToEnd
                stmReader.Close()

                Dim stmNewWrite As StreamWriter = New StreamWriter(LogPath)
                stmNewWrite.Write(_OldMessage & vbCrLf & _Message)
                stmNewWrite.Close()
            Else
                Dim stmAppendWrite As StreamWriter = File.CreateText(LogPath)
                stmAppendWrite.WriteLine(_Message)
                stmAppendWrite.Close()
            End If
        Catch oError As Exception

        End Try
    End Function

    Public Shared Function DeleteLog(ByVal LogPath As String) As Boolean
        Dim _Result As Boolean = False
        Try
            File.Delete(LogPath)
            _Result = True
            Return _Result
        Catch oError As Exception

        End Try

    End Function

    Public Sub New()
        MyBase.new()
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class