Imports gloEMR.gloEMRWord
Imports gloEMRGeneralLibrary.gloEMRDatabase
Public Class clsNurseNotes
    ' Private ds As New System.Data.DataSet
    Private dv As DataView = Nothing
    ' Private dt As DataTable


    'Public ReadOnly Property GetDataTable() As DataTable
    '    Get
    '        Return dt
    '    End Get
    'End Property
    Public Sub Dispose()
        If (IsNothing(dv) = False) Then
            dv.Dispose()
            dv = Nothing
        End If
    End Sub

    Public ReadOnly Property GetDataView() As DataView
        Get
            Return dv
        End Get
    End Property
    Private _NurseNotes As String = "PatientNurseNotes"
    Public ReadOnly Property NurseNotes() As String
        Get
            Return _NurseNotes
        End Get
    End Property

    '''' To get All Consent(s) for Selected Patient
    Public Function GetAllNurseNotes(ByVal PatientID As Long) As DataView


        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter

        'Memory Leak
        'Dim oResultTable As New DataTable
        Try
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@PatientID"
            oParamater.Value = PatientID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            Dim dt As DataTable = oDB.GetDataTable("gsp_ViewNurseNotes")
            If (IsNothing(dv) = False) Then
                dv.Dispose()
                dv = Nothing
            End If
            If Not dt Is Nothing Then

                dv = New DataView(dt.Copy())
                dt.Dispose()
                dt = Nothing
                Return dv
            Else
                Return Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            ''Added by Mayuri:20120719-Word Crash Issue
            If IsNothing(oDB) = False Then
                oDB.Dispose()

                oDB = Nothing
            End If
        End Try
    End Function

   

    '' To Select Patient Letter 
    Public Function ScanNurseNotes(ByVal NotesID As Long) As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter

        'Memory Leak
        'Dim oResultTable As New DataTable
        Try
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@NotesID"
            oParamater.Value = NotesID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            Dim dt As DataTable = oDB.GetDataTable("gsp_ScanNurseNotes")
            If Not dt Is Nothing Then
                Return dt
            Else
                Return Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            ''Added by Mayuri:20120719-Word Crash Issue
            If IsNothing(oDB) = False Then
                oDB.Dispose()

                oDB = Nothing
            End If
        End Try
    End Function

    ' To Fill ComboBox Template
    Public Function FillTemplates() As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter

        'Memory Leak
        'Dim oResultTable As New DataTable
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@flag"
            oParamater.Value = 19 '' to Fill NurseNotes Templates
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            Dim dt As DataTable = oDB.GetDataTable("gsp_FillTemplateGallery_MST")
            'Memory Leak
            If Not dt Is Nothing Then
                Return dt
            Else
                Return Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            ''Added by Mayuri:20120719-Word Crash Issue
            If IsNothing(oDB) = False Then
                oDB.Dispose()

                oDB = Nothing
            End If
        End Try
    End Function
    ''20100517-''Added by Mayuri:20100517-To fix issue:#6641-Delete nurse note template = lost names of created nurse notes associated with template
    ' To Fill ComboBox Template
    Public Function FillTemplate(ByVal TemplateFlag As enumTemplateFlag, ByVal NotesID As Long, ByVal TemplateID As Long) As DataTable

        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        'Memory Leak
        'Dim oResultTable As New DataTable
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateFlag"
            oParamater.Value = TemplateFlag  '' to Fill NurseNotes Templates
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            'oParamater = New DBParameter
            'oParamater.DataType = SqlDbType.VarChar
            'oParamater.Direction = ParameterDirection.Input
            'oParamater.Name = "@TableName"
            'oParamater.Value = "NurseNotes" '' to Fill NurseNotes Templates
            'oDB.DBParametersCol.Add(oParamater)
            'oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ID"
            oParamater.Value = NotesID '' to Fill NurseNotes Templates
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateID"
            oParamater.Value = TemplateID '' to Fill NurseNotes Templates
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            Dim dt As DataTable = oDB.GetDataTable("gsp_FillTemplate")
            'Memory Leak
            If Not dt Is Nothing Then
                Return dt
            Else
                Return Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            ''Added by Mayuri:20120719-Word Crash Issue
            If IsNothing(oDB) = False Then
                oDB.Dispose()

                oDB = Nothing
            End If
        End Try
    End Function
    ''end 20100517

    Public Function SaveNurseNotes(ByVal NotesId As Long, ByVal PatientID As Long, ByVal TemplateID As Long, ByVal LetterDate As Date, ByVal strTempFilePath As String, ByVal strTemplateName As String, ByVal IsFinished As Boolean, ByVal TemplateName As String) As Long
        '' Save Nurse Notes for  
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Dim nCurrentNoteID As Int64 = 0
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
            oParamater.Name = "@NotesDate"
            oParamater.Value = LetterDate
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
            oParamater.Name = "@NurseNotes"
            Dim objword As New clsWordDocument
            '' To convert from Object to Binary Format
            oParamater.Value = objword.ConvertFiletoBinary(strTempFilePath)

            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            'Memory leaks
            objword = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@MachineID"
            oParamater.Value = GetPrefixTransactionID()
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            'oParamater = New DBParameter
            'oParamater.DataType = SqlDbType.BigInt
            'oParamater.Direction = ParameterDirection.InputOutput
            'oParamater.Name = "@NotesID"
            'oParamater.Value = NotesId
            'oDB.DBParametersCol.Add(oParamater)
            'oParamater = Nothing

            ''Sandip Darade 20100422
            ''Case GLO2010-0004880
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateName"
            oParamater.Value = TemplateName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.InputOutput
            oParamater.Name = "@NotesID"
            oParamater.Value = NotesId
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            nCurrentNoteID = NotesId
            
            NotesId = oDB.Add("gsp_InUpNurseNotes")

            'Dim objAudit As New clsAudit
            If nCurrentNoteID = 0 Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Add, "'" & strTemplateName & "' Nurses Notes Added", PatientID, NotesId, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Modify, "'" & strTemplateName & "' Nurses Notes Modified", PatientID, nCurrentNoteID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
            'objAudit = Nothing
            Return NotesId


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        Finally
            ''Added by Mayuri:20120719-Word Crash Issue
            If IsNothing(oDB) = False Then
                oDB.Dispose()

                oDB = Nothing
            End If

        End Try
    End Function
    Public Function SaveNurseNotesBytes(ByVal NotesId As Long, ByVal PatientID As Long, ByVal TemplateID As Long, ByVal LetterDate As Date, ByVal bBytes As Object, ByVal strTemplateName As String, ByVal IsFinished As Boolean, ByVal TemplateName As String) As Long
        '' Save Nurse Notes for  
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Dim nCurrentNoteID As Int64 = 0
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
            oParamater.Name = "@NotesDate"
            oParamater.Value = LetterDate
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
            oParamater.Name = "@NurseNotes"
            '  Dim objword As New clsWordDocument
            If (IsNothing(bBytes) = False) Then
                oParamater.Value = bBytes
            Else
                oParamater.Value = DBNull.Value
            End If
            
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            'Memory leaks
            ' objword = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@MachineID"
            oParamater.Value = GetPrefixTransactionID()
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            'oParamater = New DBParameter
            'oParamater.DataType = SqlDbType.BigInt
            'oParamater.Direction = ParameterDirection.InputOutput
            'oParamater.Name = "@NotesID"
            'oParamater.Value = NotesId
            'oDB.DBParametersCol.Add(oParamater)
            'oParamater = Nothing

            ''Sandip Darade 20100422
            ''Case GLO2010-0004880
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateName"
            oParamater.Value = TemplateName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.InputOutput
            oParamater.Name = "@NotesID"
            oParamater.Value = NotesId
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            nCurrentNoteID = NotesId

            NotesId = oDB.Add("gsp_InUpNurseNotes")

            'Dim objAudit As New clsAudit
            If nCurrentNoteID = 0 Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Add, "'" & strTemplateName & "' Nurses Notes Added", PatientID, NotesId, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Modify, "'" & strTemplateName & "' Nurses Notes Modified", PatientID, nCurrentNoteID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
            'objAudit = Nothing
            Return NotesId


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        Finally
            ''Added by Mayuri:20120719-Word Crash Issue
            If IsNothing(oDB) = False Then
                oDB.Dispose()

                oDB = Nothing
            End If

        End Try
    End Function

    '' to Delete NurseNotes of ID LetterID
    Public Sub DeleteNurseNotes(ByVal NotesID As Long, ByVal Notesdate As String, ByVal NotesHeader As String)
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@NotesID"
            oParamater.Value = NotesID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oDB.Delete("gsp_DeleteNurseNotes")

            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Delete, "'" & NotesHeader & "' Nurses Notes Deleted on Dated '" & Notesdate & "'", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101009
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Delete, "'" & NotesHeader & "' Nurses Notes Deleted on Dated '" & Notesdate & "'", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.Delete, "'" & NotesHeader & "' Nurses Notes Deleted on Dated '" & Notesdate & "'", gstrLoginName, gstrClientMachineName, gnPatientID)
            'objAudit = Nothing

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ''Added by Mayuri:20120719-Word Crash Issue
            If IsNothing(oDB) = False Then
                oDB.Dispose()

                oDB = Nothing
            End If
        End Try
    End Sub
    Public Function Fill_LockNurseNotes(ByVal MachinName As String, ByVal TransactionType As Integer) As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter

        'Memory leaks
        Dim oResultTable As DataTable
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
            Else
                Return Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            ''Added by Mayuri:20120719-Word Crash Issue
            If IsNothing(oDB) = False Then
                oDB.Dispose()

                oDB = Nothing
            End If
        End Try
    End Function

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
            oParameter.Value = NurseNotes
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
            oParamater.Value = NurseNotes
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
