Imports gloEMR.gloEMRWord
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports System.Data.SqlClient

Public Class clsPatientLetters
    Implements IDisposable
    '23-Apr-13 Aniket: Resolving Memory Leaks

    ' Private ds As New System.Data.DataSet

    Private dt As DataTable
    Public ReadOnly Property GetDataTable() As DataTable
        Get
            Return dt
        End Get
    End Property
    Private _PATIENTLATTERS As String = "PATIENTLATTERS"

    Public ReadOnly Property PATIENTLATTERS() As String
        Get
            Return _PATIENTLATTERS
        End Get
    End Property

    'Public ReadOnly Property DsDataSet() As DataSet
    '    Get
    '        Return ds
    '    End Get
    'End Property

    'Public ReadOnly Property DsDataview() As DataView
    '    Get
    '        Return dv
    '    End Get
    'End Property

    '' To get All Letter(s) for Selected Patient
    Public Function GetAllPatientLetters(ByVal PatientID As Long) As DataView
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter = Nothing
        '  Dim oResultTable As New DataTable  ''slr not needed 

        Try
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@PatientID"
            oParamater.Value = PatientID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            If Not IsNothing(dt) Then ''slr free previous memory
                dt.Dispose()
                dt = Nothing
            End If
            dt = oDB.GetDataTable("gsp_ViewPatientLetters")
            If Not dt Is Nothing Then
                Dim dv As DataView
                dv = New DataView(dt)
                Return dv
            Else
                Return Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            ''slr not needed 
            '  If Not IsNothing(oResultTable) Then
            'oResultTable.Dispose()
            ' oResultTable = Nothing
            ' End If

            If Not IsNothing(oParamater) Then
                oParamater = Nothing
            End If

            If Not IsNothing(oDB) Then
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
            oParameter.Value = PATIENTLATTERS
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
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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



    ' To Fill ComboBox Template
    Public Function FillTemplates() As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter = Nothing
        Dim oResultTable As DataTable  ''slr new not needed 

        Try
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@flag"
            oParamater.Value = 9 '' to Fill Patient Letter Templates
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oResultTable = oDB.GetDataTable("gsp_FillTemplateGallery_MST")

            If Not oResultTable Is Nothing Then
                Return oResultTable
            Else
                Return Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            'If Not IsNothing(oResultTable) Then  slr dont dispose since returned 
            '    oResultTable.Dispose()
            '    oResultTable = Nothing
            'End If

            If Not IsNothing(oParamater) Then
                oParamater = Nothing
            End If

            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try

    End Function

    Public Function SavePatientLetter(ByVal LetterID As Long, ByVal PatientID As Long, ByVal TemplateID As Long, ByVal LetterDate As Date, ByVal strTempFilePath As String, ByVal strTemplateName As String, ByVal IsFinished As Boolean, Optional ByVal bIsUnScheduleRemainder As Boolean = 0, Optional ByVal CommunicationPref As Int64 = 0, Optional ByVal type As Integer = 0) As Long
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter = Nothing
        Dim objword As New clsWordDocument

        Try
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@PatientID"
            oParamater.Value = PatientID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateID"
            oParamater.Value = TemplateID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.DateTime
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@LetterDate"
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
            oParamater.DataType = SqlDbType.Bit
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@bisUnscheduledCare"
            oParamater.Value = bIsUnScheduleRemainder
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nCommunicationType"
            oParamater.Value = CommunicationPref
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Image
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@PatientLetter"

            '' To convert from Object to Binary Format
            oParamater.Value = objword.ConvertFiletoBinary(strTempFilePath)

            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            objword = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@MachineID"
            oParamater.Value = GetPrefixTransactionID()
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateName"
            oParamater.Value = strTemplateName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nType"
            oParamater.Value = type
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            'ADDED THIS PARAMETER AT THE END COZ IT IS AN INPUT OUTPUT PARAMETER
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.InputOutput
            oParamater.Name = "@LetterID"
            oParamater.Value = LetterID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            If IsFinished = True Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ReminderLetter, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.Finish, "Patient Letter finished", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            Else
                If LetterID = 0 Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ReminderLetter, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.Add, "Patient Letter added", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                Else
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ReminderLetter, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.Modify, "Patient Letter modified", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
            End If

            LetterID = oDB.Add("gsp_InUpPatientLetter")

            Return LetterID

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ReminderLetter, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        Finally

            If Not IsNothing(objword) Then
                objword = Nothing
            End If

            If Not IsNothing(oParamater) Then
                oParamater = Nothing
            End If

            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try
    End Function

    Public Sub SaveWidthInDatabase(ByVal nUserId As String, ByVal value As Integer)
        Dim con As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Try
            con = New SqlConnection(GetConnectionString())
            con.Open()
            cmd = New SqlCommand("gsp_TemplatePanelWidth", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add(New SqlParameter("@Flag", 0))
            cmd.Parameters.Add(New SqlParameter("@nUserID", nUserId))
            cmd.Parameters.Add(New SqlParameter("@SettingsName", PATIENTLATTERS))
            cmd.Parameters.Add(New SqlParameter("@SettingsValue", value))
            cmd.Parameters.Add(New SqlParameter("@MachinName", ""))
            cmd.ExecuteNonQuery()
        Catch ex As Exception

        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If con IsNot Nothing Then
                con.Dispose()
                con = Nothing
            End If
        End Try
    End Sub

    Public Function SavePatientLetterBytes(ByVal LetterID As Long, ByVal PatientID As Long, ByVal TemplateID As Long, ByVal LetterDate As Date, ByVal bBytes As Object, ByVal strTemplateName As String, ByVal IsFinished As Boolean, Optional ByVal bIsUnScheduleRemainder As Boolean = 0, Optional ByVal CommunicationPref As Int64 = 0, Optional ByVal type As Integer = 0) As Long
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter = Nothing
        'Dim objword As New clsWordDocument

        Try
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@PatientID"
            oParamater.Value = PatientID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateID"
            oParamater.Value = TemplateID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.DateTime
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@LetterDate"
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
            oParamater.DataType = SqlDbType.Bit
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@bisUnscheduledCare"
            oParamater.Value = bIsUnScheduleRemainder
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nCommunicationType"
            oParamater.Value = CommunicationPref
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Image
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@PatientLetter"
            If (IsNothing(bBytes) = False) Then
                oParamater.Value = bBytes
            Else
                oParamater.Value = DBNull.Value
            End If

            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            'objword = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@MachineID"
            oParamater.Value = GetPrefixTransactionID()
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateName"
            oParamater.Value = strTemplateName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nType"
            oParamater.Value = type
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            'ADDED THIS PARAMETER AT THE END COZ IT IS AN INPUT OUTPUT PARAMETER
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.InputOutput
            oParamater.Name = "@LetterID"
            oParamater.Value = LetterID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            If IsFinished = True Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ReminderLetter, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.Finish, "Patient Letter finished", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            Else
                If LetterID = 0 Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ReminderLetter, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.Add, "Patient Letter added", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                Else
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ReminderLetter, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.Modify, "Patient Letter modified", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
            End If

            LetterID = oDB.Add("gsp_InUpPatientLetter")

            Return LetterID

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ReminderLetter, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        Finally



            If Not IsNothing(oParamater) Then
                oParamater = Nothing
            End If

            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try
    End Function
    '' To Select Patient Letter 
    Public Function ScanPatientLetter(ByVal LetterID As Long) As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter = Nothing

        ' Dim oResultTable As New DataTable slr not used 
        Try
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@LetterID"
            oParamater.Value = LetterID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            ''slr free previous memory
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            dt = oDB.GetDataTable("gsp_ScanPatientLetter")
            If Not dt Is Nothing Then
                Return dt
            Else
                Return Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(oParamater) Then
                oParamater = Nothing
            End If

            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    '' to Delete Patient Letter of ID LetterID
    Public Sub DeletePatientLetter(ByVal LetterID As Long, ByVal Letterdate As String, ByVal LetterHeader As String, ByVal PatientID As Long)
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter = Nothing
        '  Dim oResultTable As New DataTable  slr not used 

        Try
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@LetterID"
            oParamater.Value = LetterID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oDB.Delete("gsp_DeletePatientLetter")
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.Delete, "Patient Letter deleted", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ''slr not needed 
            'If Not IsNothing(oResultTable) Then
            '    oResultTable.Dispose()
            '    oResultTable = Nothing
            'End If

            If Not IsNothing(oParamater) Then
                oParamater = Nothing
            End If

            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Sub

    Public Function Fill_LockPatientLetter(ByVal MachinName As String, ByVal TransactionType As Integer) As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter = Nothing
        Dim oResultTable As DataTable ''slr new not needed 

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
            ''slr dont dispose since returned 
            'If Not IsNothing(oResultTable) Then
            '    oResultTable.Dispose()
            '    oResultTable = Nothing
            'End If

            If Not IsNothing(oParamater) Then
                oParamater = Nothing
            End If

            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try

    End Function

    Public Function FillReminderforUnscheduledCare(Optional ByVal _nTemplateID As Int64 = 0) As DataSet
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter = Nothing

        Try


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sType"
            oParamater.Value = "PatientLetters"
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nTemplateID"
            oParamater.Value = _nTemplateID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            Dim _ds As DataSet

            _ds = oDB.GetDataSet("gsp_FillReminderforUnscheduledCare", True)




            If Not _ds Is Nothing Then
                Return _ds
            Else
                Return Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            If Not IsNothing(oParamater) Then
                oParamater = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    '23-Apr-13 Aniket: Resolving Memory Leaks
#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).


                If IsNothing(dt) = False Then
                    dt.Dispose()
                    dt = Nothing
                End If


            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
