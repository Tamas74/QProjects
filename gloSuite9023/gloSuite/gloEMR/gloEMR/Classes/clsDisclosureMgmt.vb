Imports gloEMR.gloEMRWord
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports System.Data
Imports System.Data.SqlClient

Public Class clsDisclosureMgmt



    ' Private ds As New System.Data.DataSet
    Private dv As DataView = Nothing
    ' Private dt As DataTable = Nothing
    Public Sub Dispose()

        ''slr free dv
        If Not IsNothing(dv) Then
            dv.Dispose()
            dv = Nothing
        End If
        'If Not IsNothing(ds) Then
        '    ds.Dispose()
        '    ds = Nothing
        'End If

        'slr free Con
        'If Not IsNothing(dt) Then
        '    dt.Dispose()
        '    dt = Nothing
        'End If

    End Sub
    '  Private DCatview As DataView
    'Public ReadOnly Property GetDataTable() As DataTable
    '    Get
    '        Return dt
    '    End Get
    'End Property
    Private _PatientDisclosureMgmt As String = "PatientDisclosureMgmt"

    Public ReadOnly Property PatientDisclosureMgmt() As String
        Get
            Return _PatientDisclosureMgmt
        End Get
    End Property

    Public ReadOnly Property GetDataView() As DataView
        Get
            Return dv
        End Get
    End Property

    '''' To get All Disclosure(s) for Selected Patient
    Public Function GetAllDisclosures(ByVal PatientID As Long) As DataView


        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Dim dt As DataTable = Nothing
        'Dim oResultTable As New DataTable
        Try
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@PatientID"
            oParamater.Value = PatientID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            'If (IsNothing(dt) = False) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If (IsNothing(dv) = False) Then
                dv.Dispose()
                dv = Nothing
            End If
            dt = oDB.GetDataTable("ViewDisclosure")
            If Not dt Is Nothing Then
  
                dv = New DataView(dt)
                Return dv
            Else
                Return Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Dispose()
            If (IsNothing(dt) = False) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Function

  
    Public Function ScanDisclosure(ByVal DisclosureID As Long) As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter

        Dim dt As DataTable = Nothing
        Try
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DisclosureID"
            oParamater.Value = DisclosureID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
         
            dt = oDB.GetDataTable("ScanDisclosure")
            If Not dt Is Nothing Then
                Return dt
            Else
                Return Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Dispose()
        End Try
    End Function

    ' To Fill ComboBox Template
    Public Function FillTemplates() As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter

        Dim dt As DataTable = Nothing
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@flag"
            oParamater.Value = 18 '' to Fill Disclosure Management Templates
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
         

            dt = oDB.GetDataTable("gsp_FillTemplateGallery_MST")

            If Not dt Is Nothing Then
                Return dt
            Else
                Return Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Dispose()
        End Try
    End Function
    '' Save Disclosure Management
    Public Function SaveDisclosure(ByVal DisclosureID As Long, ByVal PatientID As Long, ByVal TemplateID As Long, ByVal DisclosureDate As Date, ByVal strTempFilePath As String, ByVal strTemplateName As String, ByVal IsFinished As Boolean) As Long

        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateID"
            oParamater.Value = TemplateID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@PatientID"
            oParamater.Value = PatientID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.DateTime
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DisclosureDate"
            oParamater.Value = DisclosureDate
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Bit
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@IsFinished"

            If IsFinished = True Then
                oParamater.Value = 1
            Else
                oParamater.Value = 0
            End If

            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Image
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DisclosureMgmt"

            '' To convert from Object to Binary Format
            If strTempFilePath <> "" Then
                Dim objword As New clsWordDocument
                oParamater.Value = objword.ConvertFiletoBinary(strTempFilePath)
                objword = Nothing
            Else
                oParamater.Value = DBNull.Value
            End If


            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@MachineID"
            oParamater.Value = GetPrefixTransactionID()
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            ''Added by Mayuri:20100525-#6808
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateName"
            oParamater.Value = strTemplateName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            ''

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.InputOutput
            oParamater.Name = "@DisclosureID"
            oParamater.Value = DisclosureID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            'If DisclosureID = 0 Then
            '    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "Disclosure Management Added", gloAuditTrail.ActivityOutCome.Success)
            '    ''Added Rahul P on 20101008
            '    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "Disclosure Management Added", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            '    ''
            '    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Disclosure Management Added", gstrLoginName, gstrClientMachineName, gnPatientID)
            'Else
            '    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, "Disclosure Management Modified", gloAuditTrail.ActivityOutCome.Success)
            '    ''Added Rahul P on 20101008
            '    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, "Disclosure Management Modified", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            '    ''
            '    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Disclosure Management Modified", gstrLoginName, gstrClientMachineName, gnPatientID)
            'End If

            DisclosureID = oDB.Add("InUpDisclosure")

            Return DisclosureID


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        Finally
            oDB.Dispose()
        End Try
    End Function
    Public Function SaveDisclosureBytes(ByVal DisclosureID As Long, ByVal PatientID As Long, ByVal TemplateID As Long, ByVal DisclosureDate As Date, ByVal bBytes As Object, ByVal strTemplateName As String, ByVal IsFinished As Boolean) As Long

        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateID"
            oParamater.Value = TemplateID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@PatientID"
            oParamater.Value = PatientID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.DateTime
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DisclosureDate"
            oParamater.Value = DisclosureDate
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Bit
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@IsFinished"

            If IsFinished = True Then
                oParamater.Value = 1
            Else
                oParamater.Value = 0
            End If

            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Image
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DisclosureMgmt"

            '' To convert from Object to Binary Format
            If IsNothing(bBytes) = False Then

                oParamater.Value = bBytes

            Else
                oParamater.Value = DBNull.Value
            End If


            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@MachineID"
            oParamater.Value = GetPrefixTransactionID()
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            ''Added by Mayuri:20100525-#6808
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateName"
            oParamater.Value = strTemplateName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            ''

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.InputOutput
            oParamater.Name = "@DisclosureID"
            oParamater.Value = DisclosureID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            'If DisclosureID = 0 Then
            '    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "Disclosure Management Added", gloAuditTrail.ActivityOutCome.Success)
            '    ''Added Rahul P on 20101008
            '    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "Disclosure Management Added", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            '    ''
            '    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Disclosure Management Added", gstrLoginName, gstrClientMachineName, gnPatientID)
            'Else
            '    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, "Disclosure Management Modified", gloAuditTrail.ActivityOutCome.Success)
            '    ''Added Rahul P on 20101008
            '    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, "Disclosure Management Modified", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            '    ''
            '    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Disclosure Management Modified", gstrLoginName, gstrClientMachineName, gnPatientID)
            'End If

            DisclosureID = oDB.Add("InUpDisclosure")

            Return DisclosureID


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        Finally
            oDB.Dispose()
        End Try
    End Function

    Public Sub DeleteDisclosureMgmt(ByVal _DisclosureID As Int64, ByVal _PatientID As Int64) ', ByVal Disclosuredate As String, ByVal DisclosureHeader As String)
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DisclosureID"
            oParamater.Value = _DisclosureID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oDB.Delete("DeleteDisclosure")

            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "Disclosure Management Deleted", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101008
            'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "Disclosure Management Deleted", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Delete, "Disclosure Management Deleted", gstrLoginName, gstrClientMachineName, _PatientID)
            DeleteDisclosureSetDetails(_DisclosureID, _PatientID)            
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDB.Dispose()
        End Try
    End Sub
    Public Function Fill_LockPatientDisclosure(ByVal MachinName As String, ByVal TransactionType As Integer) As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter

        Dim oResultTable As DataTable = Nothing
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sMachinName"
            oParamater.Value = MachinName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nTrnType"
            oParamater.Value = TransactionType
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nMachinID"
            oParamater.Value = 0
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oResultTable = oDB.GetDataTable("gsp_Select_UnLock_Record")

            If Not oResultTable Is Nothing Then
                Return oResultTable
            End If
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Dispose()
        End Try
    End Function

    Public Function IsDuplicateDisclosureSet(ByVal strCategory As String, Optional ByVal m_SetId As Int64 = 0) As String
        Dim oDB As New DataBaseLayer
        Dim Record As String
        Dim strQry As String
        If m_SetId = 0 Then
            strQry = "Select Count(*) from DisclosureSet_MST Where sDisclosureSetName = '" & strCategory.Replace("'", "''") & "'"
        Else
            strQry = "Select Count(*) from DisclosureSet_MST Where sDisclosureSetName = '" & strCategory.Replace("'", "''") & "' and nDisclosureSetID <> " & m_SetId
        End If

        Record = oDB.GetRecord_Query(strQry)
        oDB.Dispose()
        Return Record
    End Function
    Public Function GetDisclosureSet() As DataTable
        Dim ODB As New DataBaseLayer
        Dim dt As DataTable = Nothing
        Dim strQRY As String = "select distinct * from DisclosureSet_MST order by sDisclosureSetName"
        Try
            dt = ODB.GetDataTable_Query(strQRY)
            ODB.Dispose()
            If (IsNothing(dv) = False) Then
                dv.Dispose()
                dv = Nothing
            End If
            If Not dt Is Nothing Then

                dv = New DataView(dt)

                Return dt
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        Finally
            'If IsNothing(dt) = False Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
        End Try

    End Function
    

    Public Function GetDisclosureAssociation(ByVal DisclosureSetID As Int64) As String
        Dim ODB As New DataBaseLayer
        Dim DiscolsureSet As String
        Dim strQRY As String = "select sAssociation from DisclosureSet_MST Where nDisclosureSetID = " & DisclosureSetID & ""
        DiscolsureSet = ODB.GetRecord_Query(strQRY)
        ODB.Dispose()
        Return DiscolsureSet
        'If Not dt Is Nothing Then

        '    dv = New DataView(dt)

        '    Return DiscolsureSet
        'Else
        '    Return Nothing
        'End If
    End Function
    'Public ReadOnly Property DsDataview() As DataView
    '    Get
    '        'DCatview = CatDataset.Tables("Category_Mst").DefaultView
    '        Return DCatview
    '        'Return CatDataset
    '    End Get

    'End Property

    'Public Sub SortDataview(ByVal strsort As String, Optional ByVal strSortOrder As String = "")
    '    'DCatview.Sort = strsort
    '    DCatview.Sort = "[" & strsort & "]" & strSortOrder
    'End Sub
    Public Function SaveDisclosureSet(ByVal DisclosureSetID As Int64, ByVal strCategoryName As String, ByVal strAssociations As String) As Int64
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DisclosureSetName"
            oParamater.Value = strCategoryName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@AssociationType"
            oParamater.Value = strAssociations
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@MachineID"
            oParamater.Value = GetPrefixTransactionID()
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.InputOutput
            oParamater.Name = "@DisclosureSetID"
            oParamater.Value = DisclosureSetID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing



            If DisclosureSetID = 0 Then
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "Disclosure Set Added", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101008
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "Disclosure Set Added", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                '                gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "DisclosureSet Added", gstrLoginName, gstrClientMachineName, gnPatientID)
            Else
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, "Disclosure Set Modified", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101008
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, "Disclosure Set Modified", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Disclosure Set Modified", gstrLoginName, gstrClientMachineName, gnPatientID)
            End If

            DisclosureSetID = oDB.Add("InUpDisclosureSet")

            Return DisclosureSetID


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        Finally
            oDB.Dispose()
        End Try


    End Function
    Public Sub DeleteDisclosure(ByVal DisclosureSetID As Long)
        Dim oDB As New DataBaseLayer
        Try

            Dim StrSql As String
            StrSql = "select  count(*) from DisclosureSet where  nDisclosureSet=" & DisclosureSetID
            Dim StrResult As String = oDB.GetRecord_Query(StrSql)
            If StrResult <> "" Then
                If StrResult = "0" Then
                    StrSql = "Delete from DisclosureSet_MST where nDisclosureSetID= " & DisclosureSetID
                    oDB.Delete_Query(StrSql)
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "Disclosure Set Deleted", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20101008
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "Disclosure Set Deleted", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ''
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Delete, "Disclosure Set Deleted", gstrLoginName, gstrClientMachineName, gnPatientID)
                Else
                    MessageBox.Show("Disclosure Set that are associated against patient cannot be deleted", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDB.Dispose()
        End Try
    End Sub
    Public Sub DeleteDisclosureSetDetails(ByVal _DisclsoureID As Int64, ByVal _PatientID As Int64)
        Dim oDB As New DataBaseLayer
        Try

            Dim StrSql As String
            StrSql = "Delete from DisclosureSet where nPatientID = " & _PatientID & " and  nDisclosureId = " & _DisclsoureID '& " and nDisclosureSet = " & _DisclosureSetId & " and sDisclosureSetName = '" & _DisclosureSetName & "'"
            oDB.Delete_Query(StrSql)
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "Disclosure Set Details Deleted", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101008
            'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "Disclosure Set Details Deleted", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Delete, "Disclosure Set Details Deleted", gstrLoginName, gstrClientMachineName, gnPatientID)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDB.Dispose()
        End Try
    End Sub
    Public Sub SaveDislosureSetDetails(ByVal _DisclsoureID As Int64, ByVal _PatientID As Int64, ByVal _DisclosureSetId As Int64, ByVal _DisclosureSetName As String, ByVal _arrAssociation As ArrayList)
        If Not _arrAssociation Is Nothing Then
            Dim oDB As DataBaseLayer = Nothing
            Dim oParamater As DBParameter = Nothing
            Dim lst As myList = Nothing
            'Dim StrSql As String
            'StrSql = "Delete from DisclosureSet where nPatientID = " & _PatientID & " and  nDisclosureId = " & _DisclsoureID '& " and nDisclosureSet = " & _DisclosureSetId & " and sDisclosureSetName = '" & _DisclosureSetName & "'"
            'oDB = New DataBaseLayer
            'oDB.Delete_Query(StrSql)
            DeleteDisclosureSetDetails(_DisclsoureID, _PatientID)
            For _cnt As Int32 = 0 To _arrAssociation.Count - 1
                Try
                    'lst = New myList
                    lst = CType(_arrAssociation(_cnt), myList)
                    oDB = New DataBaseLayer

                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.BigInt
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@nDisclosureId"
                    oParamater.Value = _DisclsoureID
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

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
                    oParamater.Name = "@nDisclosureSet"
                    oParamater.Value = _DisclosureSetId
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing


                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@sDisclosureSetName"
                    oParamater.Value = _DisclosureSetName
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.BigInt
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@nAssociateID"
                    oParamater.Value = lst.DisclosureAssociationID
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@sAssociateType"
                    oParamater.Value = lst.DisclosureType
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing


                    oDB.Add("InUpDisclosureSetDetails")


                Catch ex As Exception
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

                Finally
                    oDB.Dispose()
                End Try
            Next

        End If
      
        
    End Sub
    Public Function GetAssociation(ByVal _DisclsoureID As Int64, ByVal _PatientID As Int64, ByVal _DisclosureSetId As Int64, ByVal _DisclosureSetName As String) As DataTable
        Dim oDB As New DataBaseLayer
        Try

            Dim StrSql As String
            Dim dtResult As DataTable = Nothing
            StrSql = "Select nPatientID, nDisclosureId, nDisclosureSet, sDisclosureSetName, nAssociateID, sAssociateType from DisclosureSet where nPatientID = " & _PatientID & " and  nDisclosureId = " & _DisclsoureID & " and nDisclosureSet = " & _DisclosureSetId & " and sDisclosureSetName = '" & _DisclosureSetName & "' "
            dtResult = oDB.GetDataTable_Query(StrSql)
            Return dtResult
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Dispose()
        End Try
    End Function
    Public Function GetAssociatedSet(ByVal _DisclsoureID As Int64, ByVal _PatientID As Int64) As Int64
        Dim oDB As New DataBaseLayer
        Try

            Dim StrSql As String
            Dim strResult As String
            StrSql = "Select nDisclosureSet from DisclosureSet where nPatientID = " & _PatientID & " and  nDisclosureId = " & _DisclsoureID '& " and nDisclosureSet = " & _DisclosureSetId & " and sDisclosureSetName = '" & _DisclosureSetName & "' "
            strResult = oDB.GetRecord_Query(StrSql)
            If strResult <> "" Then
                Return CType(strResult, Int64)
            Else
                Return 0
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        Finally
            oDB.Dispose()
        End Try
    End Function
    Public Function GetHistory(ByVal PatientId As Int64) As DataTable
        Dim ODB As New DataBaseLayer
        Dim dt As DataTable = Nothing
        Try
            Dim strSql As String
            strSql = "select nHistoryID, nVisitID, nPatientID, sHistoryCategory, sHistoryItem, sComments, sReaction, nDrugID, sUserName, sMachineName, nmedicalconditionid From History Where nPatientID = " & PatientId & " order by sHistoryCategory"
            dt = ODB.GetDataTable_Query(strSql)
            Return dt
        Catch ex As Exception
            Return Nothing
        Finally
            ODB.Dispose()
        End Try
    End Function
    Public Function GetLabOrderforPatient(ByVal PatientID As Int64, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable
        Dim ODB As New DataBaseLayer
        Try

            'dt = New DataTable
            'Dim _strSql As String = "SELECT DISTINCT Lab_Order_MST.labom_OrderID,Lab_Order_MST.labom_OrderNoPrefix,Lab_Order_MST.labom_OrderNoID,Lab_Test_Mst.labtm_Name, Lab_Order_TestDtl.labotd_TestID, Lab_Test_Mst.labtm_Code, Lab_Order_MST.labom_PatientID,Lab_Order_MST.labom_TransactionDate,Lab_Order_MST.labom_VisitID" _
            '     & " FROM Lab_Order_TestDtl INNER JOIN " _
            '     & " Lab_Order_MST ON Lab_Order_TestDtl.labotd_OrderID = Lab_Order_MST.labom_OrderID INNER JOIN " _
            '     & " Lab_Test_Mst ON Lab_Order_TestDtl.labotd_TestID = Lab_Test_Mst.labtm_ID " _
            '     & " WHERE Lab_Order_MST.labom_PatientID = " & PatientID & ""

            Dim _strSql As String = "SELECT DISTINCT Lab_Order_MST.labom_OrderID, Lab_Order_MST.labom_OrderNoPrefix, Lab_Order_MST.labom_OrderNoID, Lab_Test_Mst.labtm_Name, " _
                        & "Lab_Order_TestDtl.labotd_TestID, Lab_Test_Mst.labtm_Code, Lab_Order_MST.labom_PatientID, Lab_Order_MST.labom_TransactionDate, " _
                        & "Lab_Order_MST.labom_VisitID, Lab_Order_MST.labom_ProviderID, isnull(Provider_MST.sFirstName,'') as FirstName,isnull(Provider_MST.sMiddleName,'') as MiddleName, " _
                        & "isnull(Provider_MST.sLastName,'') as LastName FROM Lab_Order_TestDtl INNER JOIN " _
                        & "Lab_Order_MST ON Lab_Order_TestDtl.labotd_OrderID = Lab_Order_MST.labom_OrderID INNER JOIN " _
                        & "Lab_Test_Mst ON Lab_Order_TestDtl.labotd_TestID = Lab_Test_Mst.labtm_ID INNER JOIN " _
                        & "Provider_MST ON Lab_Order_MST.labom_ProviderID = Provider_MST.nProviderID " _
                        & " WHERE Lab_Order_MST.labom_PatientID = " & PatientID & "" _
                        & " AND convert(varchar,Lab_Order_MST.labom_TransactionDate,101) >= '" & Format(FromDate, "MM/dd/yyyy") & "' AND convert(varchar,Lab_Order_MST.labom_TransactionDate,101) <= '" & Format(ToDate, "MM/dd/yyyy") & "'"
           

            Dim dt As DataTable = ODB.GetDataTable_Query(_strSql)
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            ODB.Dispose()

        End Try
    End Function
    Public Function GetRadiologyOrderforPatient(ByVal PatientID As Int64, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable
        Dim ODB As New DataBaseLayer
        Try


            ''Dim dt As New DataTable
            Dim _strSQL As String
            ''Dim _Categories As New Collection
            ''Dim _Groups As New Collection
            ''Dim _Tests As New Collection

            ''Dim _SubTests As New Collection

            ''Dim oFindNode As C1.Win.C1FlexGrid.Node
            ''Dim oTempNode As C1.Win.C1FlexGrid.Node
            ''Dim _tmpRow As Integer
            ''Dim cStyle As C1.Win.C1FlexGrid.CellStyle


            ''_strSQL = " SELECT  DISTINCT LM_Test.lm_test_CategoryID, LM_Category.lm_category_Description " _
            ''        & " FROM  LM_Test LEFT OUTER JOIN LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID LEFT OUTER JOIN " _
            ''        & " LM_Orders ON LM_Test.lm_test_ID = LM_Orders.lm_test_ID " _
            ''        & " WHERE  (LM_Test.lm_test_Name IS NOT NULL) AND (LM_Orders.lm_Patient_ID = " & PatientID & ") " _
            ''        & " AND convert(varchar,LM_Orders.lm_OrderDate,101) >= '" & Format(FromDate, "MM/dd/yyyy") & "' AND convert(varchar,LM_Orders.lm_OrderDate,101) <= '" & Format(ToDate, "MM/dd/yyyy") & "'" _
            ''        & " ORDER BY lm_category_Description "
            '' ''AND (LM_Orders.lm_OrderDate = '" & _VisitDate & "') " _   lm_OrderDate
            ''dt = ODB.GetDataTable_Query(_strSQL)



            ''If IsNothing(dt) = False Then
            ''    For i As Integer = 0 To dt.Rows.Count - 1
            ''        If IsDBNull(dt.Rows(i)("lm_category_Description")) = False Then
            ''            _Categories.Add(dt.Rows(i)("lm_category_Description"))
            ''        End If
            ''    Next
            ''End If


            ' ''Fill Groups
            ''For i As Int16 = 1 To _Categories.Count
            ''    _strSQL = " SELECT DISTINCT  LM_Test.lm_test_GroupNo, LM_Test_1.lm_test_Name AS GroupName, LM_Test.lm_test_CategoryID, LM_Test_1.lm_test_TestGroupFlag, LM_Test.lm_test_Template_ID " _
            ''            & " FROM LM_Test LEFT OUTER JOIN " _
            ''            & " LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID LEFT OUTER JOIN " _
            ''            & " LM_Test LM_Test_1 ON LM_Test.lm_test_GroupNo = LM_Test_1.lm_test_ID LEFT OUTER JOIN " _
            ''            & " LM_Orders ON LM_Test.lm_test_ID = LM_Orders.lm_test_ID " _
            ''            & " WHERE (LM_Orders.lm_Patient_ID =" & PatientID & ") AND  " _
            ''            & " (LM_Category.lm_category_Description = '" & 1 & "') AND (LM_Test.lm_test_Name IS NOT NULL) " _
            ''            & " ORDER BY LM_Test_1.lm_test_Name, LM_Test.lm_test_GroupNo "
            ''    ''AND (LM_Orders.lm_OrderDate = '" & _VisitDate & "') 

            ''    dt = New DataTable
            ''    dt = ODB.GetDataTable_Query(_strSQL)



            ''    If IsNothing(dt) = False Then
            ''        For j As Integer = 0 To dt.Rows.Count - 1


            ''            _Groups.Add(dt.Rows(j)("lm_test_GroupNo"))


            ''            '''''' Add Test 
            ''            Dim dsTest As New DataTable
            ''            '_strSQL = " SELECT LM_Test.lm_test_Name, LM_Orders.lm_Order_ID, LM_Orders.lm_Visit_ID, LM_Orders.lm_Patient_ID, LM_Orders.lm_Provider_ID, " _
            ''            '            & " LM_Orders.lm_test_ID, LM_Orders.lm_OrderDate, LM_Orders.lm_NumericResult, LM_Orders.lm_Result, LM_Orders.lm_IsFinished, LM_Orders.lm_Status, " _
            ''            '            & " LM_Test.lm_test_Template_ID , LM_Test.lm_test_TestGroupFlag, LM_Test.lm_test_Dimension, LM_Orders.lm_sICD9Code, LM_Orders.lm_sICD9Description " _
            ''            '        & " FROM  LM_Test INNER JOIN " _
            ''            '        & " LM_Orders ON LM_Test.lm_test_ID = LM_Orders.lm_test_ID INNER JOIN " _
            ''            '        & " LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID " _
            ''            '        & " WHERE     (LM_Test.lm_test_Name IS NOT NULL) AND (LM_Orders.lm_Patient_ID =" & PatientID & ")  AND  " _
            ''            '        & " (LM_Category.lm_category_Description = '" & _Categories(i) & "') AND (LM_Test.lm_test_GroupNo =" & dt.Rows(j)("lm_test_GroupNo") & ") " _
            ''            '        & " ORDER BY LM_Test.lm_test_GroupNo, LM_Test.lm_test_LevelNo, LM_Test.lm_test_Name, LM_Test.lm_test_TestGroupFlag "

            ''            _strSQL = "SELECT LM_Test.lm_test_Name, LM_Orders.lm_Order_ID, LM_Orders.lm_Visit_ID, LM_Orders.lm_Patient_ID, LM_Orders.lm_Provider_ID, " _
            ''                     & " LM_Orders.lm_test_ID, LM_Orders.lm_OrderDate, LM_Orders.lm_NumericResult, LM_Orders.lm_Result, LM_Orders.lm_IsFinished, " _
            ''                     & "LM_Orders.lm_Status, LM_Test.lm_test_Template_ID, LM_Test.lm_test_TestGroupFlag, LM_Test.lm_test_Dimension, LM_Orders.lm_sICD9Code, " _
            ''                     & "LM_Orders.lm_sICD9Description, isnull(Provider_MST.sFirstName,'') as FirstName, isnull(Provider_MST.sMiddleName,'') as MiddleName, isnull(Provider_MST.sLastName,'') as LastName " _
            ''                     & "FROM LM_Test INNER JOIN " _
            ''                     & " LM_Orders ON LM_Test.lm_test_ID = LM_Orders.lm_test_ID INNER JOIN " _
            ''                     & "LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID INNER JOIN " _
            ''                     & "Provider_MST ON LM_Orders.lm_Provider_ID = Provider_MST.nProviderID " _
            ''                     & " WHERE     (LM_Test.lm_test_Name IS NOT NULL) AND (LM_Orders.lm_Patient_ID =" & PatientID & ")  AND  " _
            ''                     & " (LM_Category.lm_category_Description = '" & 0 & "') AND (LM_Test.lm_test_GroupNo =" & 1 & ") " _
            ''                     & " ORDER BY LM_Test.lm_test_GroupNo, LM_Test.lm_test_LevelNo, LM_Test.lm_test_Name, LM_Test.lm_test_TestGroupFlag "
            ''            ''AND (LM_Orders.lm_OrderDate = '" & _VisitDate & "')

            ''            _strSQL = "SELECT  DISTINCT   LM_Orders.lm_Order_ID, LM_Orders.lm_Visit_ID, LM_Orders.lm_Patient_ID, LM_Orders.lm_Provider_ID, LM_Orders.lm_test_ID, " _
            ''          & "LM_Orders.lm_OrderDate, LM_Orders.lm_NumericResult, LM_Orders.lm_IsFinished, LM_Orders.lm_Status," _
            ''          & "LM_Orders.lm_sICD9Code, LM_Orders.lm_sICD9Description, ISNULL(Provider_MST.sFirstName, '') AS FirstName, " _
            ''          & "ISNULL(Provider_MST.sMiddleName, '') AS MiddleName, ISNULL(Provider_MST.sLastName, '') AS LastName, ISNULL(LM_Orders.lm_sTestName, '') AS lm_test_Name FROM Provider_MST INNER JOIN LM_Orders ON Provider_MST.nProviderID = LM_Orders.lm_Provider_ID " _
            ''        & "WHERE(LM_Orders.lm_Patient_ID = '" & PatientID & "'" _
            ''        & " AND convert(varchar,LM_Orders.lm_OrderDate,101) >= '" & Format(FromDate, "MM/dd/yyyy") & "' AND convert(varchar,LM_Orders.lm_OrderDate,101) <= '" & Format(ToDate, "MM/dd/yyyy") & "'" _
            ''        & " ORDER BY lm_test_Name "
            ''            dsTest = ODB.GetDataTable_Query(_strSQL)
            ''            Return dsTest

            ''            'If IsNothing(dsTest) = False Then
            ''            '    For l As Integer = 0 To dsTest.Rows.Count - 1
            ''            '        _Tests.Add(dsTest.Rows(l)("lm_test_Name"))
            ''            '        ' End If
            ''            '    Next
            ''            'End If


            ''            ' End If
            ''        Next

            ''    End If
            ''Next

            _strSQL = "SELECT  DISTINCT   LM_Orders.lm_Order_ID, LM_Orders.lm_Visit_ID, LM_Orders.lm_Patient_ID, LM_Orders.lm_Provider_ID, LM_Orders.lm_test_ID, " _
                   & "LM_Orders.lm_OrderDate, LM_Orders.lm_NumericResult, LM_Orders.lm_IsFinished, LM_Orders.lm_Status," _
                   & "LM_Orders.lm_sICD9Code, LM_Orders.lm_sICD9Description, ISNULL(Provider_MST.sFirstName, '') AS FirstName, " _
                   & "ISNULL(Provider_MST.sMiddleName, '') AS MiddleName, ISNULL(Provider_MST.sLastName, '') AS LastName, ISNULL(LM_Orders.lm_sTestName, '') AS lm_test_Name FROM Provider_MST INNER JOIN LM_Orders ON Provider_MST.nProviderID = LM_Orders.lm_Provider_ID " _
                 & "WHERE LM_Orders.lm_Patient_ID = '" & PatientID & "'" _
                 & " AND convert(varchar,LM_Orders.lm_OrderDate,101) >= '" & Format(FromDate, "MM/dd/yyyy") & "' AND convert(varchar,LM_Orders.lm_OrderDate,101) <= '" & Format(ToDate, "MM/dd/yyyy") & "'" _
                 & "ORDER BY lm_test_Name "

            Dim dsTest1 As DataTable = Nothing
            dsTest1 = ODB.GetDataTable_Query(_strSQL)
            Return dsTest1
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            ODB.Dispose()
        End Try
    End Function

    Public Function GetScanDocumentforPatient(ByVal PatientID As Int64, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable
        Try
            'Dim ODB As New DataBaseLayer
            'Dim strQry As String = "Select DocumentID, DocumentName, Extension, SourceMachine, SystemFolder, Container, Category, PatientID, Year, Month, DocumentFormat, SourceBin, Pages, ArchiveStatus, ArchiveDescription, UsedStatus, UsedMachine, UsedType, DocumentType, DocumentFileName, MachineID, Modified, Synchronized, VersionNo, ModifyDateTime, IsReviewed from DMS_MST WHERE PatientID = '" & PatientID & "' AND ModifyDateTime >= '" & Format(FromDate, "MM/dd/yyyy") & "' AND ModifyDateTime <= '" & Format(ToDate, "MM/dd/yyyy") & "'"

            Dim strQry As String = "select eDocumentID,DocumentName,ModifiedDateTime FROM eDocument_Details_V3 WHERE PatientID = '" & PatientID & "' AND convert(varchar,ModifiedDateTime,101) >= '" & Format(FromDate, "MM/dd/yyyy") & "' AND convert(varchar,ModifiedDateTime,101) <= '" & Format(ToDate, "MM/dd/yyyy") & "'"


            'dt = New DataTable
            'dt = ODB.GetDataTable_Query(strQry)
            'Return dt

            Dim oSQLCommand As New SqlCommand                       ' SQL Command
            Dim _sqlConnection As New gloEMRDataConnection(GetDMSConnectionString())
            Dim dsData As New DataSet
            Try
                'Check Connection
                'Work with database
                oSQLCommand.CommandType = CommandType.Text   ' SQL Command Type , is Store Procedure
                oSQLCommand.CommandText = strQry
                oSQLCommand.Connection = _sqlConnection.gloSqlConnection

                Dim objDA As New SqlDataAdapter(oSQLCommand)

                objDA.Fill(dsData)
                objDA.Dispose()
                Return dsData.Tables(0).Copy()
            Catch ex As Exception
                Dim objex As New gloDBException
                objex.ErrMessage = "Error Retrieving Data"
                GetScanDocumentforPatient = Nothing
                Throw objex
            Finally
                'oSQLCommand = Nothing

                _sqlConnection.Dispose()
                If (IsNothing(dsData) = False) Then
                    dsData.Dispose()
                    dsData = Nothing
                End If
                If oSQLCommand IsNot Nothing Then
                    oSQLCommand.Parameters.Clear()
                    oSQLCommand.Dispose()
                    oSQLCommand = Nothing
                End If
            End Try


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            GetScanDocumentforPatient = Nothing
        End Try
    End Function

    Public Function GetMedicationforPatient(ByVal PatientId As Int64, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable
        Dim ODB As New DataBaseLayer
        Try

            'Dim strQry As String = "select nMedicationID,sMedication  + ' ' +  isnull(sDosage,'')  + ' ' +  isnull(sRoute,'')  + ' ' +  isnull(sFrequency,'')  + ' ' +  isnull(sDuration,'')  + ' ' +  isnull(sAmount,'')  + ' ' +  isnull(sStatus,'')  from Medication WHERE nPatientID = '" & PatientId & "'"
            Dim strQry As String = "select nMedicationID, sMedication  + ' ' +  isnull(sDosage,'')  + ' ' +  isnull(sRoute,'')  + ' ' +  isnull(sFrequency,'')  + ' ' +  isnull(sDuration,'')  + ' ' +  isnull(sAmount,'')  + ' ' +  isnull(sStatus,'') AS sMedication, ISNULL(dtMedicationDate,'') AS dtMedicationDate from Medication WHERE nPatientID = '" & PatientId & "' AND dbo.CONVERT_DateAsNumber(dtMedicationDate) >= " & gloDateMaster.gloDate.DateAsNumber(FromDate.Date.ToShortDateString()) & " AND dbo.CONVERT_DateAsNumber(dtMedicationDate) <= " & gloDateMaster.gloDate.DateAsNumber(ToDate.Date.ToShortDateString()) & ""
        
            ' dt = New DataTable
            Dim dt As DataTable = ODB.GetDataTable_Query(strQry)
            Return dt
        Catch ex As Exception
            Return Nothing
        Finally
            ODB.Dispose()
        End Try
    End Function
    Public Function GetNotesforPatient(ByVal PatientID As Int64, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable
        Dim ODB As New DataBaseLayer

        Try
            ' dt = New DataTable
            ''Dim strSelect As String = "Select nExamID, nVisitID, nPatientID, sExamName, sPatientNotes, bIsFinished, dtDOS, bIsOpen, sUserName, sMachineName, nProviderID FROM PatientExams WHERE nPatientID = '" & PatientID & "'"

            Dim strSelect As String = "SELECT PatientExams.nExamID,PatientExams.nVisitID,PatientExams.nPatientID, " _
                                      & "PatientExams.sExamName,PatientExams.sPatientNotes,PatientExams.bIsFinished, PatientExams.dtDOS, " _
                                      & "PatientExams.bIsOpen,PatientExams.sUserName,PatientExams.sMachineName,PatientExams.nProviderID, " _
                                      & "isnull(Provider_MST.sFirstName,'') as FirstName ,isnull(Provider_MST.sMiddleName,'') as MiddleName ,isnull(Provider_MST.sLastName,'') as LastName " _
                                      & "FROM PatientExams INNER JOIN Provider_MST ON PatientExams.nProviderID = Provider_MST.nProviderID WHERE nPatientID = '" & PatientID & "'" _
                                      & " AND PatientExams.dtDOS >= '" & Format(FromDate, "MM/dd/yyyy") & "' AND PatientExams.dtDOS <= '" & Format(ToDate, "MM/dd/yyyy") & "'"
            Dim dt As DataTable = ODB.GetDataTable_Query(strSelect)
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            ODB.Dispose()
        End Try
    End Function

    ''SUDHIR 20090219
    Public Function GetConcentforPatient(ByVal PatientID As Int64, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable
        Dim ODB As New DataBaseLayer

        Try
            Dim strQry As String = "SELECT PatientConsent.nConsentId, TemplateGallery_MST.sTemplateName, PatientConsent.dtConsentdate " _
                        & "FROM PatientConsent INNER JOIN TemplateGallery_MST ON PatientConsent.nTemplateID = TemplateGallery_MST.nTemplateID " _
                        & "WHERE PatientConsent.nPatientId = " & PatientID & " " _
                        & "AND convert(varchar,PatientConsent.dtConsentdate,101) >= '" & Format(FromDate, "MM/dd/yyyy") & "' AND convert(varchar,PatientConsent.dtConsentdate,101) <= '" & Format(ToDate, "MM/dd/yyyy") & "'"
            '            dt = New DataTable
           

            Dim dt As DataTable = ODB.GetDataTable_Query(strQry)
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            ODB.Dispose()
        End Try
    End Function

    Public Function GetFlowSheetforPatient(ByVal PatientID As Int64) As DataTable
        Dim ODB As New DataBaseLayer

        Try
            Dim strQry As String = "SELECT DISTINCT nFlowSheetRecordID, sFlowSheetName FROM FlowSheet1 WHERE nPatientID = " & PatientID & ""
            '            dt = New DataTable
           
            Dim dt As DataTable = ODB.GetDataTable_Query(strQry)
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            ODB.Dispose()
        End Try
    End Function

    Public Function GetImmunizationforPatient(ByVal PatientID As Int64, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable
        Dim ODB As New DataBaseLayer
        Try
            Dim strQry As String = "SELECT IM_Trn_Dtl.im_trn_mst_Id, IM_Trn_Dtl.im_item_name, IM_Trn_Dtl.im_trn_Date " _
                            & " FROM IM_Trn_Mst INNER JOIN IM_Trn_Dtl ON IM_Trn_Mst.im_trn_mst_Id = IM_Trn_Dtl.im_trn_mst_Id " _
                            & "WHERE IM_Trn_Mst.im_trn_mst_nPatientID = " & PatientID & " " _
                            & "AND IM_Trn_Dtl.im_trn_Date >= '" & Format(FromDate, "MM/dd/yyyy") & "' AND IM_Trn_Dtl.im_trn_Date <= '" & Format(ToDate, "MM/dd/yyyy") & "'"
            '            dt = New DataTable
            
            Dim dt As DataTable = ODB.GetDataTable_Query(strQry)
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            ODB.Dispose()
        End Try
    End Function

    Public Function GetMessagesforPatient(ByVal PatientID As Int64, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable
        Dim ODB As New DataBaseLayer
        Try
            Dim strQry As String = "SELECT nMessageID, sTemplateName, dtMsgDate FROM Message WHERE nPatientID = " & PatientID & " " _
                            & "AND convert(varchar,dtMsgDate,101) >= '" & Format(FromDate, "MM/dd/yyyy") & "' AND convert(varchar,dtMsgDate,101) <= '" & Format(ToDate, "MM/dd/yyyy") & "'"
            '            dt = New DataTable
            
            Dim dt As DataTable = ODB.GetDataTable_Query(strQry)
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            ODB.Dispose()
        End Try
    End Function

    Public Function GetVitalsforPatient(ByVal PatientID As Int64, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable
        Dim ODB As New DataBaseLayer
        Try
            Dim strQry As String = "SELECT nVitalID, dtVitalDate, ISNULL(sHeight,'') AS sHeight, ISNULL(dWeightinlbs,0) AS dWeightinlbs, " _
                    & "ISNULL(dBMI,0) AS dBMI, ISNULL(dTemperature,0) AS dTemperature, ISNULL(dRespiratoryRate,0) AS dRespiratoryRate, " _
                    & "ISNULL(dPulsePerMinute,0) AS dPulsePerMinute, ISNULL(dPulseOx,0) AS dPulseOx, " _
                    & "ISNULL(dBloodPressureSittingMin,0) AS dBloodPressureSittingMin, ISNULL(dBloodPressureSittingMax,0) AS dBloodPressureSittingMax, " _
                    & "ISNULL(dBloodPressureStandingMin,0) AS dBloodPressureStandingMin, ISNULL(dBloodPressureStandingMax,0) AS dBloodPressureStandingMax, " _
                    & "ISNULL(dHeadCircumferance,0) AS dHeadCircumferance, ISNULL(dHeightinCm,0) AS dHeightinCm " _
                    & "FROM Vitals WHERE nPatientID = " & PatientID & " " _
                    & "AND convert(varchar,dtVitalDate,101) >= '" & Format(FromDate, "MM/dd/yyyy") & "' AND convert(varchar,dtVitalDate,101) <= '" & Format(ToDate, "MM/dd/yyyy") & "'" _
                    & "ORDER BY dtVitalDate DESC"
            '            dt = New DataTable
            
            Dim dt As DataTable = ODB.GetDataTable_Query(strQry)
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            ODB.Dispose()
        End Try
    End Function

    ''END SUDHIR

    Public Sub fill_widthofExam(ByRef pnlGloUC_TemplateTreeControl As Panel)
        Dim oDB As DataBaseLayer = Nothing
        Dim oParameter As DBParameter = Nothing
        Dim sDrugForm As String = ""
        Try


            oDB = New DataBaseLayer
            oParameter = New DBParameter
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Direction = ParameterDirection.Input
            oParameter.Name = "@nUserID"
            oParameter.Value = gnLoginID
            oDB.DBParametersCol.Add(oParameter)
            oParameter = Nothing

            oParameter = New DBParameter
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Direction = ParameterDirection.Input
            oParameter.Name = "@SettingsName"
            oParameter.Value = PatientDisclosureMgmt
            oDB.DBParametersCol.Add(oParameter)
            oParameter = Nothing

            oParameter = New DBParameter
            oParameter.DataType = SqlDbType.Int
            oParameter.Direction = ParameterDirection.Input
            oParameter.Name = "@Flag"
            oParameter.Value = 1
            oDB.DBParametersCol.Add(oParameter)
            oParameter = Nothing

            sDrugForm = oDB.GetDataValue("gsp_TemplatePanelWidth", True)

            If IsNumeric(sDrugForm) Then
                pnlGloUC_TemplateTreeControl.Width = sDrugForm
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If Not IsNothing(oParameter) Then
                oParameter = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try

    End Sub

    Public Sub SaveWidthInDatabase(ByVal nUserId As String, ByVal value As Integer)


        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Try
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@Flag"
            oParamater.Value = 0
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nUserID"
            oParamater.Value = nUserId
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@SettingsName"
            oParamater.Value = PatientDisclosureMgmt
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@SettingsValue"
            oParamater.Value = value
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@MachinName"
            oParamater.Value = ""
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            oDB.Add("gsp_TemplatePanelWidth")

        Catch ex As Exception

        Finally
            If oDB IsNot Nothing Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Sub
End Class
