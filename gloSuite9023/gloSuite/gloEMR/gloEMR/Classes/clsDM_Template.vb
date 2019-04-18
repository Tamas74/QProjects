Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports gloEMR.gloEMRWord
'Imports gloEMR.gloAuditTrail.gloAuditTrail
Imports System.Data.SqlClient
Imports gloEMR.gloStream.DiseaseManagement.DiseaseManagement

Public Class clsDM_Template

   
    ' Private Ds As System.Data.DataSet
    ' Private Dv As DataView
    'Private Tb As DataTable



    'Public ReadOnly Property DsDataSet() As DataSet
    '    Get
    '        Return Ds
    '    End Get
    'End Property
    'Public ReadOnly Property DsDataview() As DataView
    '    Get
    '        Return Dv
    '    End Get
    'End Property


    Public Function FillTemplates(ByVal CriteriaID As Long) As DataTable
        Dim oDB As New DataBaseLayer
        '  Dim oParamater As DBParameter
      
        Dim oResultTable As DataTable = Nothing
        Try
            Dim _strSQL As String

            _strSQL = "SELECT TemplateGallery_MST.nTemplateID, TemplateGallery_MST.sTemplateName FROM DM_Templates_DTL INNER JOIN TemplateGallery_MST ON DM_Templates_DTL.dm_Templatedtl_TemplateID = TemplateGallery_MST.nTemplateID WHERE(DM_Templates_DTL.dm_Templatedtl_Id = " & CriteriaID & ")"

            oResultTable = oDB.GetDataTable_Query(_strSQL)
            If Not oResultTable Is Nothing Then
                Return oResultTable
            End If
            Return Nothing
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Load, "clsDM_Template -- FillTemplates -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsDM_Template -- FillTemplates -- " & ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Load, "clsDM_Template -- FillTemplates -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsDM_Template -- FillTemplates -- " & ex.ToString)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
            End If
        End Try
    End Function

    Public Function GetPatientName(ByVal lPatientID As Long) As String
        Dim oDB As New DataBaseLayer
        Dim _Result As String = ""
        Dim _strSQL As String
        Try
            _strSQL = "SELECT sFirstName + ' ' + sLastName FROM Patient WHERE nPatientID = " & lPatientID

            _Result = Trim(oDB.GetRecord_Query(_strSQL))
            Return _Result
            '            oDB.Dispose()
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Load, "clsDM_Template -- GetPatientName -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsDM_Template -- GetPatientName -- " & ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Load, "clsDM_Template -- GetPatientName -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsDM_Template -- GetPatientName -- " & ex.ToString)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
            End If
        End Try
    End Function
    'Public Function GetTemplate(ByVal TemplateID As Long) As DataTable
    '    Try
    '        Dim adpt As New SqlDataAdapter
    '        Dim dt As New DataTable

    '        Cmd = New SqlCommand("gsp_GetExamContents", Con)
    '        Cmd.CommandType = CommandType.StoredProcedure

    '        Dim objParam As SqlParameter
    '        objParam = Cmd.Parameters.Add("@nTemplateID", SqlDbType.BigInt)
    '        objParam.Direction = ParameterDirection.Input
    '        objParam.Value = TemplateID

    '        adpt.SelectCommand = Cmd
    '        adpt.Fill(dt)
    '        Con.Close()
    '        Return dt
    '    Catch ex As Exception
    '        If Con.State = ConnectionState.Open Then
    '            Con.Close()
    '        End If
    '    End Try
    'End Function

    Public Sub Save_Template(ByVal Trn_Id As Long, ByVal Patient_ID As Long, ByVal CriteriaID As Long, ByVal TemplateID As Long, ByVal strTempFilePath As String, ByVal Type As frmDM_Template.Type)
        Dim oDB As DataBaseLayer = Nothing
        Dim oParamater As DBParameter = Nothing
        Dim oPrintParamater As DBParameter = Nothing
        Dim oFaxParamater As DBParameter = Nothing
        Try
            oDB = New DataBaseLayer
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@Trn_Id"
            oParamater.Value = Trn_Id
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.DateTime
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@Trn_Date"
            oParamater.Value = DateTime.Now.Date
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@PatientID"
            oParamater.Value = Patient_ID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@CriteriaID"
            oParamater.Value = CriteriaID
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
            oParamater.DataType = SqlDbType.Image
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@Result"
            Dim objword As New clsWordDocument
            '' To convert from Object to Binary Format
            oParamater.Value = objword.ConvertFiletoBinary(strTempFilePath)
            objword = Nothing

            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@MachineID"
            oParamater.Value = GetPrefixTransactionID()
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oPrintParamater = New DBParameter
            oPrintParamater.DataType = SqlDbType.Int
            oPrintParamater.Direction = ParameterDirection.Input
            oPrintParamater.Name = "@Print"

            oFaxParamater = New DBParameter
            oFaxParamater.DataType = SqlDbType.Int
            oFaxParamater.Direction = ParameterDirection.Input
            oFaxParamater.Name = "@FAX"

            '''' Fill the Status
            If Type = frmDM_Template.Type.Print Then
                '' IF Template is Printing
                oPrintParamater.Value = Type.Print
                oFaxParamater.Value = 0

            ElseIf Type = frmDM_Template.Type.Fax Then
                '' IF Template is send as FAX
                oPrintParamater.Value = 0
                oFaxParamater.Value = Type.Fax

            ElseIf Type = frmDM_Template.Type.Save Then
                '' IF Template is Save as Image
                oPrintParamater.Value = 0
                oFaxParamater.Value = 0

            End If

            oDB.DBParametersCol.Add(oPrintParamater)
            oPrintParamater = Nothing
            oDB.DBParametersCol.Add(oFaxParamater)
            oFaxParamater = Nothing

            Dim _strSQL As String
            _strSQL = "Delete from DM_Tran where dm_Trn_nPatientID=" & Patient_ID & " and dm_Trn_nCriteriaID= " & CriteriaID & " and dm_Trn_nTempGuideLineID= " & TemplateID
            oDB.Delete_Query(_strSQL)

           

            oDB.Add("DM_InsertTran")

            If Type = frmDM_Template.Type.Fax Then
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Fax, "Patient Guideline Faxed", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101008
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Fax, "Patient Guideline Faxed", Patient_ID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PHIExport, " Patient Guideline Faxed", gstrLoginName, gstrClientMachineName, Patient_ID)
            ElseIf Type = frmDM_Template.Type.Print Then
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Print, "Patient Guideline Printed", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101008
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Print, "Patient Guideline Printed", Patient_ID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PHIExport, " Patient Guideline Printed", gstrLoginName, gstrClientMachineName, Patient_ID)
            ElseIf Type = frmDM_Template.Type.Save Then
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Add, "Patient Guideline Saved", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101008
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Add, "Patient Guideline Saved", Patient_ID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, " Patient Guideline Saved", gstrLoginName, gstrClientMachineName, Patient_ID)
            End If

            ' objAudit = Nothing
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Add, "clsDM_Template -- Save_Template -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsDM_Template -- Save_Template -- " & ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Add, "clsDM_Template -- Save_Template -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsDM_Template -- Save_Template -- " & ex.ToString)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
            End If
        End Try
    End Sub
    Public Sub Save_TemplateBytes(ByVal Trn_Id As Long, ByVal Patient_ID As Long, ByVal CriteriaID As Long, ByVal TemplateID As Long, ByVal bBytes As Object, ByVal Type As frmDM_Template.Type)
        Dim oDB As DataBaseLayer = Nothing
        Dim oParamater As DBParameter = Nothing
        Dim oPrintParamater As DBParameter = Nothing
        Dim oFaxParamater As DBParameter = Nothing
        Try
            oDB = New DataBaseLayer
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@Trn_Id"
            oParamater.Value = Trn_Id
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.DateTime
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@Trn_Date"
            oParamater.Value = DateTime.Now.Date
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@PatientID"
            oParamater.Value = Patient_ID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@CriteriaID"
            oParamater.Value = CriteriaID
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
            oParamater.DataType = SqlDbType.Image
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@Result"
            'Dim objword As New clsWordDocument
            '' To convert from Object to Binary Format
            If (IsNothing(bBytes) = False) Then
                oParamater.Value = bBytes
            Else
                oParamater.Value = DBNull.Value
            End If

            'objword = Nothing

            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@MachineID"
            oParamater.Value = GetPrefixTransactionID()
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oPrintParamater = New DBParameter
            oPrintParamater.DataType = SqlDbType.Int
            oPrintParamater.Direction = ParameterDirection.Input
            oPrintParamater.Name = "@Print"

            oFaxParamater = New DBParameter
            oFaxParamater.DataType = SqlDbType.Int
            oFaxParamater.Direction = ParameterDirection.Input
            oFaxParamater.Name = "@FAX"

            '''' Fill the Status
            If Type = frmDM_Template.Type.Print Then
                '' IF Template is Printing
                oPrintParamater.Value = Type.Print
                oFaxParamater.Value = 0

            ElseIf Type = frmDM_Template.Type.Fax Then
                '' IF Template is send as FAX
                oPrintParamater.Value = 0
                oFaxParamater.Value = Type.Fax

            ElseIf Type = frmDM_Template.Type.Save Then
                '' IF Template is Save as Image
                oPrintParamater.Value = 0
                oFaxParamater.Value = 0

            End If

            oDB.DBParametersCol.Add(oPrintParamater)
            oPrintParamater = Nothing
            oDB.DBParametersCol.Add(oFaxParamater)
            oFaxParamater = Nothing

            Dim _strSQL As String
            _strSQL = "Delete from DM_Tran where dm_Trn_nPatientID=" & Patient_ID & " and dm_Trn_nCriteriaID= " & CriteriaID & " and dm_Trn_nTempGuideLineID= " & TemplateID
            oDB.Delete_Query(_strSQL)



            oDB.Add("DM_InsertTran")

            If Type = frmDM_Template.Type.Fax Then
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Fax, "Patient Guideline Faxed", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101008
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Fax, "Patient Guideline Faxed", Patient_ID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PHIExport, " Patient Guideline Faxed", gstrLoginName, gstrClientMachineName, Patient_ID)
            ElseIf Type = frmDM_Template.Type.Print Then
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Print, "Patient Guideline Printed", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101008
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Print, "Patient Guideline Printed", Patient_ID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PHIExport, " Patient Guideline Printed", gstrLoginName, gstrClientMachineName, Patient_ID)
            ElseIf Type = frmDM_Template.Type.Save Then
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Add, "Patient Guideline Saved", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101008
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Add, "Patient Guideline Saved", Patient_ID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, " Patient Guideline Saved", gstrLoginName, gstrClientMachineName, Patient_ID)
            End If

            ' objAudit = Nothing
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Add, "clsDM_Template -- Save_Template -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsDM_Template -- Save_Template -- " & ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Add, "clsDM_Template -- Save_Template -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsDM_Template -- Save_Template -- " & ex.ToString)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
            End If
        End Try
    End Sub
 

    Public Sub Save_TemplateDetail(ByVal Trn_Id As Int64, ByVal strtDate As DateTime, ByVal endDate As DateTime, ByVal sDurationType As String, ByVal nDuration As Int32)
        Dim oDB As DataBaseLayer = Nothing
        Dim oParamater As DBParameter = Nothing
        'Dim oPrintParamater As DBParameter
        'Dim oFaxParamater As DBParameter
        Try
            oDB = New DataBaseLayer
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_TransId" ' "@Trn_Id"
            oParamater.Value = Trn_Id
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.DateTime
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_dtStartDate"
            oParamater.Value = strtDate
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            ''''Pramod
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.DateTime
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_dtEndDate"
            oParamater.Value = endDate
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            ''''Pramod
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_nDurationType"
            oParamater.Value = sDurationType
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_nDurationPeriod" '"@IsOverride"
            oParamater.Value = nDuration
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oDB.Add("DM_InsertPatientDTL")

        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex As Exception

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
            End If
        End Try

    End Sub
    Public Function DeleteTemplate(ByVal _CriteriaId As Int64, ByVal _TriggerId As Int64, ByVal _PatientId As Int64)
        Dim oDB As New DataBaseLayer
        Dim oResult As DataTable
        Dim _strSQL As String

        Try
            _strSQL = "Select DM_TransId from DM_Patient WHERE DM_nCriteriaID = " & _CriteriaId & " and DM_nTriggerID =" & _TriggerId & " and DM_nPatientID= " & _PatientId

            oResult = oDB.GetDataTable_Query(_strSQL)
            If Not oResult Is Nothing Then
                For _index As Int32 = 0 To oResult.Rows.Count - 1
                    _strSQL = "delete from DM_Patient WHERE DM_TransId = " & oResult.Rows(_index)("DM_TransID")
                    oDB.Delete_Query(_strSQL)
                    _strSQL = "delete from DM_Patient_DTL WHERE DM_TransId = " & oResult.Rows(_index)("DM_TransID")
                    oDB.Delete_Query(_strSQL)
                Next
                oResult.Dispose()
                oResult = Nothing
            End If
            

        Catch ex As SqlException
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex As Exception

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
            End If
        End Try
        Return Nothing
    End Function

    '' COMMENT BY SUDHIR 20090309 - OLD SAVING PATIENT CRITERIA - NOT USING.
    'Public Function Save_PatientCriteria(ByVal TemplateList As ArrayList, ByVal CriteriaID As Int64, ByVal PatientID As Int64, ByVal CriteriaName As String, ByVal CriteriaMessage As String) As Boolean
    '    Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
    '    Dim oParameters As gloDatabaseLayer.DBParameters
    '    Dim oParameter As gloDatabaseLayer.DBParameter
    '    Dim Query As String = ""
    '    Try
    '        oDB.Connect(False)

    '        Query = "DELETE FROM DM_Patient_Criteria_MST WHERE DM_MST_ID = " & CriteriaID & " AND DM_PatientID = " & PatientID & ""
    '        oDB.Execute_Query(Query)

    '        For i As Integer = 0 To TemplateList.Count - 1
    '            oParameters = New gloDatabaseLayer.DBParameters

    '            '' CRITERIA ID
    '            oParameter = New gloDatabaseLayer.DBParameter
    '            oParameter.ParameterName = "@Criteria_ID"
    '            oParameter.DataType = SqlDbType.BigInt
    '            oParameter.Value = CriteriaID
    '            oParameters.Add(oParameter)
    '            oParameter = Nothing

    '            '' PATIENT ID
    '            oParameter = New gloDatabaseLayer.DBParameter
    '            oParameter.ParameterName = "@PatientID"
    '            oParameter.DataType = SqlDbType.BigInt
    '            oParameter.Value = PatientID
    '            oParameters.Add(oParameter)
    '            oParameter = Nothing

    '            '' CRITERIA NAME
    '            oParameter = New gloDatabaseLayer.DBParameter
    '            oParameter.ParameterName = "@CriteriaName"
    '            oParameter.DataType = SqlDbType.VarChar
    '            oParameter.Value = CriteriaName
    '            oParameters.Add(oParameter)
    '            oParameter = Nothing

    '            '' CRITERIA MESSAGE
    '            oParameter = New gloDatabaseLayer.DBParameter
    '            oParameter.ParameterName = "@DisplayMessage"
    '            oParameter.DataType = SqlDbType.VarChar
    '            oParameter.Value = CriteriaMessage
    '            oParameters.Add(oParameter)
    '            oParameter = Nothing

    '            '' ORDER ID
    '            oParameter = New gloDatabaseLayer.DBParameter
    '            oParameter.ParameterName = "@OrderID"
    '            oParameter.DataType = SqlDbType.BigInt
    '            oParameter.Value = CType(TemplateList(i), myList).ID
    '            oParameters.Add(oParameter)
    '            oParameter = Nothing

    '            '' ORDER TYPE
    '            oParameter = New gloDatabaseLayer.DBParameter
    '            oParameter.ParameterName = "@OrderType"
    '            oParameter.DataType = SqlDbType.BigInt
    '            oParameter.Value = CType(TemplateList(i), myList).Index
    '            oParameters.Add(oParameter)
    '            oParameter = Nothing

    '            oDB.Execute("DM_IN_Patient_Criteria_MST", oParameters)
    '            oParameters = Nothing
    '        Next

    '        oDB.Disconnect()
    '        oDB.Dispose()
    '        oDB = Nothing
    '        Return True
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Return False
    '    End Try
    'End Function

    'Public Function getData(ByVal strFields As String) As String
    '    Try
    '        Dim strData As String
    '        Dim objCmd As New SqlCommand
    '        Dim objSQLDataReader As SqlDataReader
    '        Dim sqlParam As SqlParameter
    '        objCmd.CommandType = CommandType.StoredProcedure
    '        objCmd.CommandText = "gsp_GetFieldsdata"
    '        objCmd.Parameters.Clear()
    '        'cn.ConnectionString = GetConnectionString()
    '        Con.Open()
    '        sqlParam = objCmd.Parameters.Add("@sFields", SqlDbType.VarChar)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = strFields

    '        sqlParam = objCmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = gnPatientID

    '        sqlParam = objCmd.Parameters.Add("@nExamID", SqlDbType.BigInt)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = 0

    '        objCmd.Connection = Con

    '        objSQLDataReader = objCmd.ExecuteReader
    '        If objSQLDataReader.HasRows = True Then
    '            objSQLDataReader.Read()
    '            If IsDBNull(objSQLDataReader.Item(0)) = False Then
    '                strData = objSQLDataReader.Item(0)
    '            End If
    '        End If
    '        objSQLDataReader.Close()
    '        Con.Close()
    '        Return strData
    '    Catch ex As Exception
    '        If Con.State = ConnectionState.Open Then
    '            Con.Close()
    '        End If
    '    End Try
    'End Function


    'Public Sub GenerateVisitID()
    '    Try
    '        Dim cmdVisits As SqlCommand
    '        Dim objParam As SqlParameter
    '        cmdVisits = New SqlCommand("gsp_InsertVisits", Con)
    '        cmdVisits.CommandType = CommandType.StoredProcedure
    '        Con.ConnectionString = GetConnectionString()

    '        objParam = cmdVisits.Parameters.Add("@nPatientID", SqlDbType.BigInt)
    '        objParam.Direction = ParameterDirection.Input
    '        'objParam.Value = objPrescription.PatientID
    '        objParam.Value = gnPatientID

    '        objParam = cmdVisits.Parameters.Add("@dtVisitdate", SqlDbType.DateTime)
    '        objParam.Direction = ParameterDirection.Input
    '        objParam.Value = Now

    '        objParam = cmdVisits.Parameters.Add("@VisitID", SqlDbType.BigInt)
    '        objParam.Direction = ParameterDirection.Output
    '        'objParam.Value = 0

    '        If Con.State = ConnectionState.Closed Then
    '            Con.Open()
    '        End If

    '        cmdVisits.ExecuteNonQuery()
    '        gnVisitID = objParam.Value


    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, "Patient Education", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        Con.Close()
    '    End Try
    'End Sub

#Region " DM Guideline Duration "

    Public Function Save_GuidelineDuration(ByVal GuideLines As Collection) As Boolean
        Dim oDB As DataBaseLayer = Nothing
        Dim oParamater As DBParameter = Nothing
        Try
            oDB = New DataBaseLayer
            oDB.Delete_Query("DELETE FROM DM_Duration_MST")
            oDB.Dispose()
            oDB = Nothing

            Dim i As Integer
            For i = 1 To GuideLines.Count
                Dim lst As myList
                lst = CType(GuideLines(i), myList)

                oDB = New DataBaseLayer

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@TemplateID"

                oParamater.Value = lst.ID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.VarChar
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@sDuration"
                oParamater.Value = lst.Description
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oDB.Add("DM_InsertDuration_MST")
                oDB.Dispose()
                oDB = Nothing
            Next
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            ' code modified on 20070619 by bipin
            If Not IsNothing(oDB) Then
                oDB.Dispose()
            End If
            ''''
        End Try
    End Function

#End Region

#Region " DM Guideline Association "

    Public Function Save_GuidelineAssociation(ByVal Association As Collection) As Boolean
        Dim oDB As DataBaseLayer = Nothing
        Dim oParamater As DBParameter = Nothing
        Try
            oDB = New DataBaseLayer
            oDB.Delete_Query("DELETE FROM DM_Association")
            oDB.Dispose()
            oDB = Nothing

            Dim i As Integer
            For i = 1 To Association.Count
                Dim lst As myList
                lst = CType(Association(i), myList)

                oDB = New DataBaseLayer

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@PatientID"
                oParamater.Value = lst.ID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.DateTime
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@dtOnSetDate"
                oParamater.Value = lst.VisitDate
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@nTemplateID"
                oParamater.Value = lst.Index
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.Int
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@Flag"
                oParamater.Value = lst.Value
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oDB.Add("DM_InsertAssociation")
                oDB.Dispose()
                oDB = Nothing
            Next
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            ' code modified on 20070619
            If Not IsNothing(oDB) Then
                oDB.Dispose()
            End If
            ''''
        End Try
    End Function

    Public Function CheckForDueGuideline(ByVal PatientID As Long, ByVal VisitDate_mmddyyyy As Date, Optional ByVal Flag As Integer = 0) As GuidelineResults
        Dim strSQL As String = ""
        Dim _OnSetDate As Date
        Dim _TemplateID As Long
        Dim _TemplateName As String = ""
        Dim _Duration As String
        Dim _DurationDate As Date
        Dim _IsValid As Boolean = False
        Dim _Result As New GuidelineResults
        Dim oDB As New DataBaseLayer
        Try


            strSQL = " Select DM_Association.dm_nPatientID, DM_Association.dm_dtOnSetDate, DM_Association.dm_nTemplateID, DM_Duration_MST.dm_sDuration, TemplateGallery_MST.sTemplateName " _
                    & " FROM DM_Association INNER JOIN DM_Duration_MST ON DM_Association.dm_nTemplateID = DM_Duration_MST.dm_nTemplateID INNER JOIN TemplateGallery_MST ON DM_Duration_MST.dm_nTemplateID = TemplateGallery_MST.nTemplateID " _
                    & " WHERE (DM_Association.dm_nPatientID = " & PatientID & ") AND (DM_Association.dm_nFlag = " & Flag & ")"


            Dim oResultTable As DataTable
            oResultTable = oDB.GetDataTable_Query(strSQL)

            If Not oResultTable Is Nothing Then
                If oResultTable.Rows.Count > 0 Then
                    For i As Int16 = 0 To oResultTable.Rows.Count - 1
                        If IsDBNull(oResultTable.Rows(i).Item("dm_dtOnSetDate")) = False AndAlso IsDBNull(oResultTable.Rows(i).Item("dm_nTemplateID")) = False AndAlso IsDBNull(oResultTable.Rows(i).Item("dm_sDuration")) = False AndAlso IsDBNull(oResultTable.Rows(i).Item("sTemplateName")) = False Then
                            If IsDate(oResultTable.Rows(i).Item("dm_dtOnSetDate")) Then
                                _OnSetDate = oResultTable.Rows(i).Item("dm_dtOnSetDate")
                                _Duration = oResultTable.Rows(i).Item("dm_sDuration") & ""
                                _TemplateID = oResultTable.Rows(i).Item("dm_nTemplateID") & ""
                                _DurationDate = FindHealthPlanDate(_Duration, _OnSetDate)
                                _TemplateName = oResultTable.Rows(i).Item("sTemplateName") & ""
                                _IsValid = True
                            End If
                        End If
                    Next
                End If
                oResultTable.Dispose()
                oResultTable = Nothing
            End If

            If _IsValid = True Then
                'For Eg
                'On Set Date - 3/20/2007 - Duration - 1 Week - means - Duration Date is - 3/28/2007
                '1. Visit Date - 3/15/2007 - None (because <= On Set Date)
                '2. Visit Date - 3/22/2007 - (>= On Set Date) & (<= Duration Date) - Due
                '3. Visit Date - 3/29/2007 - (>= Duration Date) Overdue

                With _Result
                    .TemplateID = _TemplateID
                    .TemplateName = _TemplateName
                    .GuidelineDueOverDueDate = _DurationDate
                    .OnSetDate = _OnSetDate

                    If VisitDate_mmddyyyy < _OnSetDate Then
                        .GuidelineIs = EnumGuidelineResult.None
                    ElseIf VisitDate_mmddyyyy >= _OnSetDate AndAlso VisitDate_mmddyyyy <= _DurationDate Then
                        .GuidelineIs = EnumGuidelineResult.Due
                    ElseIf VisitDate_mmddyyyy >= _DurationDate Then
                        .GuidelineIs = EnumGuidelineResult.OverDue
                    End If
                End With
            End If

            Return _Result
        Catch ex As Exception
            Return Nothing
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
            End If
        End Try
    End Function

    Public Function CheckForDueGuideline(ByVal oDT As DataTable, ByVal ReportDate_mmddyyyy As Date, ByVal Flag As EnumGuidelineResult) As Collection
        ' Dim strSQL As String = ""
        'Dim _OnSetDate As Date
        'Dim _TemplateID As Long
        'Dim _TemplateName As String = ""
        Dim _Duration As String
        'Dim _DurationDate As Date
        'Dim _IsValid As Boolean = False
        Dim oGuidelineResults As New GuidelineResults
        Dim oResult As New Collection


        'dm_nPatientID,
        'dm_dtOnSetDate,
        'dm_nTemplateID,
        'dm_nFlag,
        'sPatientCode()
        'PatientName, 
        'sTemplateName()

        If Not oDT Is Nothing Then
            For i As Integer = 0 To oDT.Rows.Count - 1
                oGuidelineResults = New GuidelineResults
                With oGuidelineResults

                    .OnSetDate = oDT.Rows(i)("dm_dtOnSetDate")
                    .PatientCode = oDT.Rows(i)("sPatientCode")
                    .PatientName = oDT.Rows(i)("PatientName")
                    _Duration = oDT.Rows(i)("dm_sDuration") & ""
                    .TemplateID = oDT.Rows(i)("dm_nTemplateID") & ""
                    .GuidelineDueOverDueDate = FindHealthPlanDate(_Duration, .OnSetDate)
                    .TemplateName = oDT.Rows(i)("sTemplateName") & ""
                    '_IsValid = True
                    If Flag = EnumGuidelineResult.Due AndAlso ReportDate_mmddyyyy >= .OnSetDate AndAlso ReportDate_mmddyyyy <= .GuidelineDueOverDueDate Then
                        oResult.Add(oGuidelineResults)
                    ElseIf Flag = EnumGuidelineResult.OverDue AndAlso ReportDate_mmddyyyy >= .GuidelineDueOverDueDate Then
                        oResult.Add(oGuidelineResults)
                    Else
                        oGuidelineResults = Nothing
                    End If
                End With
                oGuidelineResults = Nothing
            Next
        End If


        'If _IsValid = True Then
        '    'For Eg
        '    'On Set Date - 3/20/2007 - Duration - 1 Week - means - Duration Date is - 3/28/2007
        '    '1. Visit Date - 3/15/2007 - None (because <= On Set Date)
        '    '2. Visit Date - 3/22/2007 - (>= On Set Date) & (<= Duration Date) - Due
        '    '3. Visit Date - 3/29/2007 - (>= Duration Date) Overdue

        '    With _Result
        '        .TemplateID = _TemplateID
        '        .TemplateName = _TemplateName
        '        .GuidelineDueOverDueDate = _DurationDate
        '        .OnSetDate = _OnSetDate

        '        If VisitDate_mmddyyyy < _OnSetDate Then
        '            .GuidelineIs = EnumGuidelineResult.None
        '        ElseIf VisitDate_mmddyyyy >= _OnSetDate And VisitDate_mmddyyyy <= _DurationDate Then
        '            .GuidelineIs = EnumGuidelineResult.Due
        '        ElseIf VisitDate_mmddyyyy >= _DurationDate Then
        '            .GuidelineIs = EnumGuidelineResult.OverDue
        '        End If
        '    End With
        'End If

        Return oResult

    End Function

    Public Function CheckDueForLabs(ByVal oDT As DataTable, ByVal ReportDate_mmddyyyy As Date, ByVal Flag As EnumGuidelineResult, ByVal nType As EnumTriggerType) As DueOverDueResults
        Dim _PatientName As String = ""
        Dim _PatientCode As String = ""
        Dim _Orders As String = ""
        Dim _DueOn As String = ""
        ' Dim _DueDate As Date
        Dim _Reasone As String = ""
        Dim _Notes As String = ""
        Dim _Result As New DueOverDueResults
        
        If (IsNothing(oDT) = False) Then


            For i As Integer = 0 To oDT.Rows.Count - 1
                Dim _DateOfBirth As DateTime = oDT.Rows(i)("dtDOB")
                'Calculate Age of Patient Start
                Dim nMonths As Long
                '  Dim _strAge As String
                nMonths = DateDiff(DateInterval.Month, CType(_DateOfBirth, Date), Date.Now.Date)
                Dim Age As Integer = nMonths \ 12
                'Calculate Age of Patient End
                Dim _DueOverDueResult As New DueOverDueResult
                _DueOverDueResult = New DueOverDueResult

                ''''Due,OverDue,Given Type is Date
                If oDT.Rows(i)("DM_DueType") = "Date" Then
                    If Flag = EnumGuidelineResult.Due Then
                        If ReportDate_mmddyyyy <= oDT.Rows(i)("DM_DueValue") AndAlso oDT.Rows(i)("DM_bIsGiven") = False Then ''Due
                            With _DueOverDueResult
                                .PatientCode = oDT.Rows(i)("sPatientCode")
                                .PatientName = oDT.Rows(i)("PatientName")
                                If nType = EnumTriggerType.Labs Then
                                    .TriggerName = oDT.Rows(i)("labtm_Name")
                                ElseIf nType = EnumTriggerType.Radiology Then
                                    .TriggerName = oDT.Rows(i)("lm_test_Name")
                                ElseIf nType = EnumTriggerType.Drugs Then
                                    .TriggerName = oDT.Rows(i)("sDrugName")
                                ElseIf nType = EnumTriggerType.Guidelines Then
                                    .TriggerName = oDT.Rows(i)("sTemplateName")
                                End If

                                .CriteriaName = oDT.Rows(i)("dm_mst_CriteriaName")
                                .DueOn = oDT.Rows(i)("DM_DueType")
                                .DueDate = oDT.Rows(i)("DM_DueValue")
                                .Reason = oDT.Rows(i)("DM_sReason")
                                .Notes = oDT.Rows(i)("DM_sNotes")
                            End With
                        End If
                    ElseIf Flag = EnumGuidelineResult.OverDue Then
                        If ReportDate_mmddyyyy > oDT.Rows(i)("DM_DueValue") AndAlso oDT.Rows(i)("DM_bIsGiven") = False Then ''OverDue
                            With _DueOverDueResult
                                .PatientCode = oDT.Rows(i)("sPatientCode")
                                .PatientName = oDT.Rows(i)("PatientName")
                                If nType = EnumTriggerType.Labs Then
                                    .TriggerName = oDT.Rows(i)("labtm_Name")
                                ElseIf nType = EnumTriggerType.Radiology Then
                                    .TriggerName = oDT.Rows(i)("lm_test_Name")
                                ElseIf nType = EnumTriggerType.Drugs Then
                                    .TriggerName = oDT.Rows(i)("sDrugName")
                                ElseIf nType = EnumTriggerType.Guidelines Then
                                    .TriggerName = oDT.Rows(i)("sTemplateName")
                                End If
                                .CriteriaName = oDT.Rows(i)("dm_mst_CriteriaName")
                                .DueOn = oDT.Rows(i)("DM_DueType")
                                .DueDate = oDT.Rows(i)("DM_DueValue")
                                .Reason = oDT.Rows(i)("DM_sReason")
                                .Notes = oDT.Rows(i)("DM_sNotes")
                            End With
                        End If
                    ElseIf Flag = EnumGuidelineResult.Given Then
                        If oDT.Rows(i)("DM_bIsGiven") = True Then
                            With _DueOverDueResult
                                .PatientCode = oDT.Rows(i)("sPatientCode")
                                .PatientName = oDT.Rows(i)("PatientName")
                                If nType = EnumTriggerType.Labs Then '''' Given Type is Labs
                                    .TriggerName = oDT.Rows(i)("labtm_Name")
                                ElseIf nType = EnumTriggerType.Radiology Then '''' Given Type is Radiology
                                    .TriggerName = oDT.Rows(i)("lm_test_Name")
                                ElseIf nType = EnumTriggerType.Drugs Then '''' Given Type is Drugs
                                    .TriggerName = oDT.Rows(i)("sDrugName")
                                ElseIf nType = EnumTriggerType.Guidelines Then '''' Given Type is Guidelines.
                                    .TriggerName = oDT.Rows(i)("sTemplateName")
                                End If
                                .CriteriaName = oDT.Rows(i)("dm_mst_CriteriaName")
                                .DueOn = oDT.Rows(i)("DM_DueType")
                                .DueDate = oDT.Rows(i)("DM_DueValue")
                                .Reason = oDT.Rows(i)("DM_sReason")
                                .Notes = oDT.Rows(i)("DM_sNotes")
                            End With
                        End If
                    End If

                ElseIf oDT.Rows(i)("DM_DueType") = "Age" Then '''' Due,OverDue,Given Type is Age
                    If Flag = EnumGuidelineResult.Due Then
                        Dim _Age As Int64 = GetTagElement(oDT.Rows(i)("DM_DueValue"), 2)
                        If Age <= _Age AndAlso oDT.Rows(i)("DM_bIsGiven") = False Then
                            With _DueOverDueResult
                                .PatientCode = oDT.Rows(i)("sPatientCode")
                                .PatientName = oDT.Rows(i)("PatientName")
                                If nType = EnumTriggerType.Labs Then ''''Due Type is Labs
                                    .TriggerName = oDT.Rows(i)("labtm_Name")
                                ElseIf nType = EnumTriggerType.Radiology Then '''' Due Type is Radiology
                                    .TriggerName = oDT.Rows(i)("lm_test_Name")
                                ElseIf nType = EnumTriggerType.Drugs Then '''' Due Type is Drugs
                                    .TriggerName = oDT.Rows(i)("sDrugName")
                                ElseIf nType = EnumTriggerType.Guidelines Then '''' Due Type is Guidelines
                                    .TriggerName = oDT.Rows(i)("sTemplateName")
                                End If
                                .CriteriaName = oDT.Rows(i)("dm_mst_CriteriaName")
                                .DueOn = oDT.Rows(i)("DM_DueType")
                                .DueDate = _Age
                                .Reason = oDT.Rows(i)("DM_sReason")
                                .Notes = oDT.Rows(i)("DM_sNotes")
                            End With
                        End If

                    ElseIf Flag = EnumGuidelineResult.OverDue Then
                        Dim _Age As Int64 = GetTagElement(oDT.Rows(i)("DM_DueValue"), 2)
                        If Age > _Age AndAlso oDT.Rows(i)("DM_bIsGiven") = False Then
                            With _DueOverDueResult
                                .PatientCode = oDT.Rows(i)("sPatientCode")
                                .PatientName = oDT.Rows(i)("PatientName")
                                If nType = EnumTriggerType.Labs Then
                                    .TriggerName = oDT.Rows(i)("labtm_Name")
                                ElseIf nType = EnumTriggerType.Radiology Then
                                    .TriggerName = oDT.Rows(i)("lm_test_Name")
                                ElseIf nType = EnumTriggerType.Drugs Then
                                    .TriggerName = oDT.Rows(i)("sDrugName")
                                ElseIf nType = EnumTriggerType.Guidelines Then
                                    .TriggerName = oDT.Rows(i)("sTemplateName")
                                End If
                                .CriteriaName = oDT.Rows(i)("dm_mst_CriteriaName")
                                .DueOn = oDT.Rows(i)("DM_DueType")
                                .DueDate = _Age
                                .Reason = oDT.Rows(i)("DM_sReason")
                                .Notes = oDT.Rows(i)("DM_sNotes")
                            End With
                        End If
                    ElseIf Flag = EnumGuidelineResult.Given Then
                        If oDT.Rows(i)("DM_bIsGiven") = True Then
                            With _DueOverDueResult
                                .PatientCode = oDT.Rows(i)("sPatientCode")
                                .PatientName = oDT.Rows(i)("PatientName")
                                If nType = EnumTriggerType.Labs Then
                                    .TriggerName = oDT.Rows(i)("labtm_Name")
                                ElseIf nType = EnumTriggerType.Radiology Then
                                    .TriggerName = oDT.Rows(i)("lm_test_Name")
                                ElseIf nType = EnumTriggerType.Drugs Then
                                    .TriggerName = oDT.Rows(i)("sDrugName")
                                ElseIf nType = EnumTriggerType.Guidelines Then
                                    .TriggerName = oDT.Rows(i)("sTemplateName")
                                End If
                                .CriteriaName = oDT.Rows(i)("dm_mst_CriteriaName")
                                .DueOn = oDT.Rows(i)("DM_DueType")
                                .DueDate = oDT.Rows(i)("DM_DueValue")
                                .Reason = oDT.Rows(i)("DM_sReason")
                                .Notes = oDT.Rows(i)("DM_sNotes")
                            End With
                        End If
                    End If
                Else
                    If oDT.Rows(i)("DM_bIsGiven") = True AndAlso Flag = EnumGuidelineResult.Given Then
                        With _DueOverDueResult
                            .PatientCode = oDT.Rows(i)("sPatientCode")
                            .PatientName = oDT.Rows(i)("PatientName")
                            If nType = EnumTriggerType.Labs Then
                                .TriggerName = oDT.Rows(i)("labtm_Name")
                            ElseIf nType = EnumTriggerType.Radiology Then
                                .TriggerName = oDT.Rows(i)("lm_test_Name")
                            ElseIf nType = EnumTriggerType.Drugs Then
                                .TriggerName = oDT.Rows(i)("sDrugName")
                            ElseIf nType = EnumTriggerType.Guidelines Then
                                .TriggerName = oDT.Rows(i)("sTemplateName")
                            End If
                            .CriteriaName = oDT.Rows(i)("dm_mst_CriteriaName")
                            .DueOn = oDT.Rows(i)("DM_DueType")
                            .DueDate = oDT.Rows(i)("DM_DueValue")
                            .Reason = oDT.Rows(i)("DM_sReason")
                            .Notes = oDT.Rows(i)("DM_sNotes")
                        End With
                    End If
                End If
                If _DueOverDueResult.PatientName <> "" Then
                    _Result.Add(_DueOverDueResult)
                    'Else
                    '    _DueOverDueResult = Nothing
                End If
                _DueOverDueResult = Nothing
            Next
        End If
        Return _Result
    End Function


    Private Function GetTagElement(ByVal TagContent As String, ByVal Position As Int64) As Int64
        'Return the At element of the String.
        Dim _temp As String = String.Empty
        ''Problem No: 00000344 : Error Message - EMR
        ''Reason: throws "Input string not in correct format" exception.
        ''Description: if TagContent=">=70" Split function return "=70" (Split consider only first character) 
        ''& for Convert.toInt64() "=70" is not valid int64 so throws exception. function return integer value so we replace <,>,= sign by "" and return integer
        'Dim temp As String()
        'If TagContent.Contains(">") Then
        '    temp = TagContent.Split(">")
        '    Return Convert.ToInt64(temp(Position - 1))
        'ElseIf TagContent.Contains("<") Then
        '    temp = TagContent.Split("<")
        '    Return Convert.ToInt64(temp(Position - 1))
        'ElseIf TagContent.Contains("=") Then
        '    temp = TagContent.Split("=")
        '    Return Convert.ToInt64(temp(Position - 1))
        'End If

        If TagContent.Contains(">") Or TagContent.Contains("<") Or TagContent.Contains("=") Then
            _temp = TagContent.Replace(">", "").Replace("<", "").Replace("=", "")
        End If
        Return Convert.ToInt64(_temp)

    End Function

    Public Function Update_GuidelineAssociation(ByVal Association As Collection, ByVal strFileName As String) As Boolean
        Dim oDB As DataBaseLayer = Nothing
        Dim oParamater As DBParameter = Nothing
        Dim lst As myList = Nothing
        Try

            Dim i As Integer
            For i = 1 To Association.Count

                oDB = New DataBaseLayer
                'lst = New myList
                lst = CType(Association(i), myList)

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@PatientID"
                oParamater.Value = lst.ID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing


                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@nTemplateID"
                oParamater.Value = lst.Index
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.Int
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@Flag"
                oParamater.Value = lst.Value
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oDB.Add("DM_UpdateAssociation")
                oDB.Dispose()
                oDB = Nothing
                lst = Nothing

                ''''<><><> To Insert Due Guidelines of Patient <><><>
                oDB = New DataBaseLayer
                'lst = New myList
                lst = CType(Association(i), myList)

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@PatientID"
                oParamater.Value = lst.ID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@CriteriaID"
                oParamater.Value = 0
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.DateTime
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@dtGuideLineDate"
                oParamater.Value = Format(Now, "MM/dd/yyyy hh:mm:ss tt")
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@TemplateID"
                oParamater.Value = lst.Index
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.Image
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@Result"
                Dim objword As New clsWordDocument
                '' To convert from Object to Binary Format
                oParamater.Value = objword.ConvertFiletoBinary(strFileName)

                objword = Nothing

                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@MachineID"
                oParamater.Value = GetPrefixTransactionID()
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oDB.Add("DM_InsertDueGuideline")
                oDB.Dispose()
                objword = Nothing
                oDB = Nothing
                lst = Nothing
            Next
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
            End If
        End Try
    End Function

#End Region


#Region "DM Denormalization"
    'sarika DM Denormalization 20090403
    'Public Function Save_Trigger(ByVal Trn_Id As Long, ByVal Patient_ID As Long, ByVal CriteriaID As Long, ByVal TriggerID As Long, ByVal TrnDate As Date, ByVal strTempFilePath As String, ByVal Type As frmDM_Template.Type, ByVal DM_Type As TemplateCategoryID, Optional ByVal DueType As String = "", Optional ByVal DueValue As String = "", Optional ByVal IsOverride As Boolean = False, Optional ByVal sReason As String = "", Optional ByVal sNotes As String = "", Optional ByVal bIsGiven As Boolean = False, Optional ByVal bIsRecurring As Boolean = False) As Int64
    '    '     
    '    '' Save DM Trans Dtl
    '    Dim oDB As DataBaseLayer
    '    Dim oParamater As DBParameter
    '    Dim oPrintParamater As DBParameter
    '    Dim oFaxParamater As DBParameter
    '    Try
    '        oDB = New DataBaseLayer


    '        oParamater = New DBParameter
    '        oParamater.DataType = SqlDbType.DateTime
    '        oParamater.Direction = ParameterDirection.Input
    '        oParamater.Name = "@DM_dtTransDate" '"@Trn_Date"
    '        oParamater.Value = TrnDate
    '        oDB.DBParametersCol.Add(oParamater)
    '        oParamater = Nothing

    '        oParamater = New DBParameter
    '        oParamater.DataType = SqlDbType.BigInt
    '        oParamater.Direction = ParameterDirection.Input
    '        oParamater.Name = "@DM_nPatientID" '"@PatientID"
    '        oParamater.Value = Patient_ID
    '        oDB.DBParametersCol.Add(oParamater)
    '        oParamater = Nothing

    '        oParamater = New DBParameter
    '        oParamater.DataType = SqlDbType.BigInt
    '        oParamater.Direction = ParameterDirection.Input
    '        oParamater.Name = "@DM_nCriteriaID" '"@CriteriaID"
    '        oParamater.Value = CriteriaID
    '        oDB.DBParametersCol.Add(oParamater)
    '        oParamater = Nothing


    '        ''''Pramod
    '        oParamater = New DBParameter
    '        oParamater.DataType = SqlDbType.BigInt
    '        oParamater.Direction = ParameterDirection.Input
    '        oParamater.Name = "DM_nTriggerID"
    '        oParamater.Value = TriggerID
    '        oDB.DBParametersCol.Add(oParamater)
    '        oParamater = Nothing


    '        oParamater = New DBParameter
    '        oParamater.DataType = SqlDbType.Image
    '        oParamater.Direction = ParameterDirection.Input
    '        oParamater.Name = "@DM_sResult" '"@Result"
    '        Dim objword As New clsWordDocument
    '        '' To convert from Object to Binary Format
    '        If strTempFilePath = "" Then
    '            oParamater.Value = DBNull.Value
    '        Else
    '            oParamater.Value = objword.ConvertFiletoBinary(strTempFilePath)
    '        End If

    '        oDB.DBParametersCol.Add(oParamater)
    '        oParamater = Nothing


    '        oPrintParamater = New DBParameter
    '        oPrintParamater.DataType = SqlDbType.Int
    '        oPrintParamater.Direction = ParameterDirection.Input
    '        oPrintParamater.Name = "@DM_nPrint" '"@Print"

    '        oFaxParamater = New DBParameter
    '        oFaxParamater.DataType = SqlDbType.Int
    '        oFaxParamater.Direction = ParameterDirection.Input
    '        oFaxParamater.Name = "@DM_nFax" '"@FAX"

    '        '''' Fill the Status
    '        If Type = frmDM_Template.Type.Print Then
    '            '' IF Template is Printing
    '            oPrintParamater.Value = Type.Print
    '            oFaxParamater.Value = 0

    '        ElseIf Type = frmDM_Template.Type.Fax Then
    '            '' IF Template is send as FAX
    '            oPrintParamater.Value = 0
    '            oFaxParamater.Value = Type.Fax

    '        ElseIf Type = frmDM_Template.Type.Save Then
    '            '' IF Template is Save as Image
    '            oPrintParamater.Value = 0
    '            oFaxParamater.Value = 0

    '        End If

    '        oDB.DBParametersCol.Add(oPrintParamater)
    '        oPrintParamater = Nothing
    '        oDB.DBParametersCol.Add(oFaxParamater)
    '        oFaxParamater = Nothing

    '        ''''Pramod
    '        oParamater = New DBParameter
    '        oParamater.DataType = SqlDbType.SmallInt
    '        oParamater.Direction = ParameterDirection.Input
    '        oParamater.Name = "@DM_nType"
    '        oParamater.Value = DM_Type
    '        oDB.DBParametersCol.Add(oParamater)
    '        oParamater = Nothing

    '        ''''Pramod
    '        oParamater = New DBParameter
    '        oParamater.DataType = SqlDbType.VarChar
    '        oParamater.Direction = ParameterDirection.Input
    '        oParamater.Name = "@DM_DueType"
    '        oParamater.Value = DueType
    '        oDB.DBParametersCol.Add(oParamater)
    '        oParamater = Nothing

    '        ''''Pramod
    '        oParamater = New DBParameter
    '        oParamater.DataType = SqlDbType.VarChar
    '        oParamater.Direction = ParameterDirection.Input
    '        oParamater.Name = "@DM_DueValue"
    '        oParamater.Value = DueValue
    '        oDB.DBParametersCol.Add(oParamater)
    '        oParamater = Nothing


    '        oParamater = New DBParameter
    '        oParamater.DataType = SqlDbType.Bit
    '        oParamater.Direction = ParameterDirection.Input
    '        oParamater.Name = "@DM_bIsOverride" '"@IsOverride"
    '        oParamater.Value = IsOverride
    '        oDB.DBParametersCol.Add(oParamater)
    '        oParamater = Nothing

    '        oParamater = New DBParameter
    '        oParamater.DataType = SqlDbType.VarChar
    '        oParamater.Direction = ParameterDirection.Input
    '        oParamater.Name = "@DM_sReason" '"@Reason"
    '        oParamater.Value = sReason
    '        oDB.DBParametersCol.Add(oParamater)
    '        oParamater = Nothing


    '        ''''Pramod
    '        oParamater = New DBParameter
    '        oParamater.DataType = SqlDbType.VarChar
    '        oParamater.Direction = ParameterDirection.Input
    '        oParamater.Name = "@DM_sNotes"
    '        oParamater.Value = sNotes
    '        oDB.DBParametersCol.Add(oParamater)
    '        oParamater = Nothing

    '        ''''Pramod
    '        oParamater = New DBParameter
    '        oParamater.DataType = SqlDbType.Bit
    '        oParamater.Direction = ParameterDirection.Input
    '        oParamater.Name = "@DM_bIsGiven"
    '        oParamater.Value = bIsGiven
    '        oDB.DBParametersCol.Add(oParamater)
    '        oParamater = Nothing

    '        ''''Pramod
    '        oParamater = New DBParameter
    '        oParamater.DataType = SqlDbType.Bit
    '        oParamater.Direction = ParameterDirection.Input
    '        oParamater.Name = "@DM_bIsRecurring"
    '        oParamater.Value = bIsRecurring
    '        oDB.DBParametersCol.Add(oParamater)
    '        oParamater = Nothing

    '        oParamater = New DBParameter
    '        oParamater.DataType = SqlDbType.BigInt
    '        oParamater.Direction = ParameterDirection.Input
    '        oParamater.Name = "@MachineID"
    '        oParamater.Value = GetPrefixTransactionID()
    '        oDB.DBParametersCol.Add(oParamater)
    '        oParamater = Nothing

    '        oParamater = New DBParameter
    '        oParamater.DataType = SqlDbType.BigInt
    '        oParamater.Direction = ParameterDirection.InputOutput
    '        oParamater.Name = "@DM_TransId" ' "@Trn_Id"
    '        oParamater.Value = Trn_Id
    '        oDB.DBParametersCol.Add(oParamater)
    '        oParamater = Nothing

    '        Trn_Id = oDB.Add("DM_InsertPatientTemplate")
    '        'oDB.Add("DM_InsertTran")

    '        If Type = frmDM_Template.Type.Fax Then
    '            gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PHIExport, " Patient Guideline Faxed", gstrLoginName, gstrClientMachineName, Patient_ID)
    '        ElseIf Type = frmDM_Template.Type.Print Then
    '            gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PHIExport, " Patient Guideline Printed", gstrLoginName, gstrClientMachineName, Patient_ID)
    '        ElseIf Type = frmDM_Template.Type.Save Then
    '            gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, " Patient Guideline Saved", gstrLoginName, gstrClientMachineName, Patient_ID)
    '        End If
    '        Return Trn_Id
    '        ' objAudit = Nothing
    '    Catch ex As SqlException
    '        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        UpdateLog("clsDM_Template -- Save_Template -- " & ex.ToString)
    '        Return 0
    '    Catch ex As Exception
    '        UpdateLog("clsDM_Template -- Save_Template -- " & ex.ToString)
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Return 0
    '    Finally
    '        If Not IsNothing(oDB) Then
    '            oDB.Dispose()
    '        End If
    '    End Try
    'End Function
    Public Function Save_Trigger_old(ByVal Trn_Id As Long, ByVal Patient_ID As Long, ByVal CriteriaID As Long, ByVal TriggerID As Long, ByVal TriggerName As String, ByVal CriteriaName As String, ByVal TriggerResult As Object, ByVal TrnDate As Date, ByVal strTempFilePath As String, ByVal Type As frmDM_Template.Type, ByVal DM_Type As TemplateCategoryID, Optional ByVal DueType As String = "", Optional ByVal DueValue As String = "", Optional ByVal IsOverride As Boolean = False, Optional ByVal sReason As String = "", Optional ByVal sNotes As String = "", Optional ByVal bIsGiven As Boolean = False, Optional ByVal bIsRecurring As Boolean = False, Optional ByVal DMTemplateDtlInfo As String = "") As Int64
        '     
        '' Save DM Trans Dtl
        Dim oDB As DataBaseLayer = Nothing
        Dim oParamater As DBParameter = Nothing
        Dim oPrintParamater As DBParameter = Nothing
        Dim oFaxParamater As DBParameter = Nothing
        Try
            oDB = New DataBaseLayer


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.DateTime
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_dtTransDate" '"@Trn_Date"
            oParamater.Value = TrnDate
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_nPatientID" '"@PatientID"
            oParamater.Value = Patient_ID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_nCriteriaID" '"@CriteriaID"
            oParamater.Value = CriteriaID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            ''''Pramod
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "DM_nTriggerID"
            oParamater.Value = TriggerID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Image
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_sResult" '"@Result"
            'SLR: Directly assigning on 12/2/2014
            If (IsNothing(TriggerResult) = False) Then
                oParamater.Value = CType(TriggerResult, Byte()).Clone()
            Else
                oParamater.Value = DBNull.Value
            End If
            'Dim img As Byte() = Nothing

            'img = TriggerResult
            '' Dim objword As New clsWordDocument
            '' '' To convert from Object to Binary Format
            ''If strTempFilePath = "" Then
            ''    oParamater.Value = DBNull.Value
            ''Else
            ''    oParamater.Value = objword.ConvertFiletoBinary(strTempFilePath)
            ''End If

            ''If TriggerResult Is Nothing Then
            ''    oParamater.Value = DBNull.Value
            ''Else
            'If Not IsNothing(img) Then
            '    oParamater.Value = img
            'Else
            '    oParamater.Value = DBNull.Value
            'End If

            'img = Nothing
            'End If

            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oPrintParamater = New DBParameter
            oPrintParamater.DataType = SqlDbType.Int
            oPrintParamater.Direction = ParameterDirection.Input
            oPrintParamater.Name = "@DM_nPrint" '"@Print"

            oFaxParamater = New DBParameter
            oFaxParamater.DataType = SqlDbType.Int
            oFaxParamater.Direction = ParameterDirection.Input
            oFaxParamater.Name = "@DM_nFax" '"@FAX"

            '''' Fill the Status
            If Type = frmDM_Template.Type.Print Then
                '' IF Template is Printing
                oPrintParamater.Value = Type.Print
                oFaxParamater.Value = 0

            ElseIf Type = frmDM_Template.Type.Fax Then
                '' IF Template is send as FAX
                oPrintParamater.Value = 0
                oFaxParamater.Value = Type.Fax

            ElseIf Type = frmDM_Template.Type.Save Then
                '' IF Template is Save as Image
                oPrintParamater.Value = 0
                oFaxParamater.Value = 0

            End If

            oDB.DBParametersCol.Add(oPrintParamater)
            oPrintParamater = Nothing
            oDB.DBParametersCol.Add(oFaxParamater)
            oFaxParamater = Nothing

            ''''Pramod
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.SmallInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_nType"
            oParamater.Value = DM_Type
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            ''''Pramod
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_DueType"
            oParamater.Value = DueType
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            ''''Pramod
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_DueValue"
            oParamater.Value = DueValue
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Bit
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_bIsOverride" '"@IsOverride"
            oParamater.Value = IsOverride
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_sReason" '"@Reason"
            oParamater.Value = sReason
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            ''''Pramod
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_sNotes"
            oParamater.Value = sNotes
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            ''''Pramod
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Bit
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_bIsGiven"
            oParamater.Value = bIsGiven
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            ''''Pramod
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Bit
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_bIsRecurring"
            oParamater.Value = bIsRecurring
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
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_TriggerName"
            oParamater.Value = TriggerName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_CriteriaName"
            oParamater.Value = CriteriaName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@dm_triggerdtlinfo" ' "@Trn_Id"
            oParamater.Value = DMTemplateDtlInfo
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.InputOutput
            oParamater.Name = "@DM_TransId" ' "@Trn_Id"
            oParamater.Value = Trn_Id
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            Trn_Id = oDB.Add("DM_InsertPatientTemplate")
            'oDB.Add("DM_InsertTran")

            If Type = frmDM_Template.Type.Fax Then
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Fax, "Patient Guideline Faxed", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101008
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Fax, "Patient Guideline Faxed", Patient_ID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PHIExport, " Patient Guideline Faxed", gstrLoginName, gstrClientMachineName, Patient_ID)
            ElseIf Type = frmDM_Template.Type.Print Then
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Print, "Patient Guideline Printed", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101009
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Print, "Patient Guideline Printed", Patient_ID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PHIExport, " Patient Guideline Printed", gstrLoginName, gstrClientMachineName, Patient_ID)
            ElseIf Type = frmDM_Template.Type.Save Then
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Add, "Patient Guideline Saved", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101009
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Add, "Patient Guideline Saved", Patient_ID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, " Patient Guideline Saved", gstrLoginName, gstrClientMachineName, Patient_ID)
            End If
            Return Trn_Id
            ' objAudit = Nothing
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Add, "clsDM_Template -- Save_Template -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsDM_Template -- Save_Template -- " & ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Add, "clsDM_Template -- Save_Template -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsDM_Template -- Save_Template -- " & ex.ToString)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
            End If
        End Try
    End Function


    'Changed by Shweta 20100117
    'Against the bug id:5350 
    'To passed the Parameter to stored procedure  
    Public Function Save_Trigger(ByVal Trn_Id As Long, ByVal Patient_ID As Long, ByVal CriteriaID As Long, ByVal TriggerID As Long, ByVal TriggerName As String, ByVal CriteriaName As String, ByVal TriggerResult As Object, ByVal TrnDate As Date, ByVal strTempFilePath As String, ByVal Type As frmDM_Template.Type, ByVal DM_Type As TemplateCategoryID, ByVal mynode As myTreeNode, Optional ByVal DueType As String = "", Optional ByVal DueValue As String = "", Optional ByVal IsOverride As Boolean = False, Optional ByVal sReason As String = "", Optional ByVal sNotes As String = "", Optional ByVal bIsGiven As Boolean = False, Optional ByVal bIsRecurring As Boolean = False, Optional ByVal DMTemplateDtlInfo As String = "") As Int64
        '     
        '' Save DM Trans Dtl
        Dim oDB As DataBaseLayer = Nothing
        Dim oParamater As DBParameter = Nothing
        Dim oPrintParamater As DBParameter = Nothing
        Dim oFaxParamater As DBParameter = Nothing
        Try
            oDB = New DataBaseLayer


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.DateTime
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_dtTransDate" '"@Trn_Date"
            oParamater.Value = TrnDate
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_nPatientID" '"@PatientID"
            oParamater.Value = Patient_ID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_nCriteriaID" '"@CriteriaID"
            oParamater.Value = CriteriaID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            ''''Pramod
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "DM_nTriggerID"
            oParamater.Value = TriggerID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Image
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_sResult" '"@Result"
            'SLR: Directly assigning on 12/2/2014
            If (IsNothing(TriggerResult) = False) Then
                oParamater.Value = CType(TriggerResult, Byte()).Clone()
            Else
                oParamater.Value = DBNull.Value
            End If
            'Dim img As Byte() = Nothing

            '            img = TriggerResult
            ' Dim objword As New clsWordDocument
            ' '' To convert from Object to Binary Format
            'If strTempFilePath = "" Then
            '    oParamater.Value = DBNull.Value
            'Else
            '    oParamater.Value = objword.ConvertFiletoBinary(strTempFilePath)
            'End If

            'If TriggerResult Is Nothing Then
            '    oParamater.Value = DBNull.Value
            'Else
            'If Not IsNothing(img) Then
            '    oParamater.Value = img
            'Else
            '    oParamater.Value = DBNull.Value
            'End If

            'img = Nothing
            'End If

            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oPrintParamater = New DBParameter
            oPrintParamater.DataType = SqlDbType.Int
            oPrintParamater.Direction = ParameterDirection.Input
            oPrintParamater.Name = "@DM_nPrint" '"@Print"

            oFaxParamater = New DBParameter
            oFaxParamater.DataType = SqlDbType.Int
            oFaxParamater.Direction = ParameterDirection.Input
            oFaxParamater.Name = "@DM_nFax" '"@FAX"

            '''' Fill the Status
            If Type = frmDM_Template.Type.Print Then
                '' IF Template is Printing
                oPrintParamater.Value = Type.Print
                oFaxParamater.Value = 0

            ElseIf Type = frmDM_Template.Type.Fax Then
                '' IF Template is send as FAX
                oPrintParamater.Value = 0
                oFaxParamater.Value = Type.Fax

            ElseIf Type = frmDM_Template.Type.Save Then
                '' IF Template is Save as Image
                oPrintParamater.Value = 0
                oFaxParamater.Value = 0

            End If

            oDB.DBParametersCol.Add(oPrintParamater)
            oPrintParamater = Nothing
            oDB.DBParametersCol.Add(oFaxParamater)
            oFaxParamater = Nothing

            ''''Pramod
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.SmallInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_nType"
            oParamater.Value = DM_Type
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            ''''Pramod
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_DueType"
            oParamater.Value = DueType
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            ''''Pramod
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_DueValue"
            oParamater.Value = DueValue
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Bit
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_bIsOverride" '"@IsOverride"
            oParamater.Value = IsOverride
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_sReason" '"@Reason"
            oParamater.Value = sReason
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            ''''Pramod
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_sNotes"
            oParamater.Value = sNotes
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            ''''Pramod
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Bit
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_bIsGiven"
            oParamater.Value = bIsGiven
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            ''''Pramod
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Bit
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_bIsRecurring"
            oParamater.Value = bIsRecurring
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
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_TriggerName"
            oParamater.Value = TriggerName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_CriteriaName"
            oParamater.Value = CriteriaName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@dm_triggerdtlinfo" ' "@Trn_Id"
            oParamater.Value = DMTemplateDtlInfo
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sDrugForm" '
            oParamater.Value = mynode.DrugForm
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sRoute "
            oParamater.Value = mynode.Route
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sFrequency"
            oParamater.Value = mynode.Frequency
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sNDCCode"
            oParamater.Value = mynode.NDCCode
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nIsNarcotics"
            oParamater.Value = mynode.IsNarcotics
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sDuration"
            oParamater.Value = mynode.Duration
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing




            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sDrugQtyQualifier"
            oParamater.Value = mynode.DrugQtyQualifier
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing



            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.InputOutput
            oParamater.Name = "@DM_TransId" ' "@Trn_Id"
            oParamater.Value = Trn_Id
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            Trn_Id = oDB.Add("DM_InsertPatientTemplate")
            'oDB.Add("DM_InsertTran")

            If Type = frmDM_Template.Type.Fax Then
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Fax, "Patient Guideline Faxed", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101009
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Fax, "Patient Guideline Faxed", Patient_ID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PHIExport, " Patient Guideline Faxed", gstrLoginName, gstrClientMachineName, Patient_ID)
            ElseIf Type = frmDM_Template.Type.Print Then
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Print, "Patient Guideline Printed", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101009
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Print, "Patient Guideline Printed", Patient_ID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PHIExport, " Patient Guideline Printed", gstrLoginName, gstrClientMachineName, Patient_ID)
            ElseIf Type = frmDM_Template.Type.Save Then
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Add, "Patient Guideline Saved", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101009
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Add, "Patient Guideline Saved", Patient_ID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, " Patient Guideline Saved", gstrLoginName, gstrClientMachineName, Patient_ID)
            End If
            Return Trn_Id
            ' objAudit = Nothing
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Add, "clsDM_Template -- Save_Template -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsDM_Template -- Save_Template -- " & ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Add, "clsDM_Template -- Save_Template -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsDM_Template -- Save_Template -- " & ex.ToString)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
            End If
        End Try
    End Function
    'End Shweta 20100117
    '--


    'To passed the Parameter to stored procedure  
    Public Function Save_Trigger_NewRule(ByVal Trn_Id As Long, ByVal Patient_ID As Long, ByVal CriteriaID As Long, ByVal TriggerID As Long, ByVal TriggerName As String, ByVal CriteriaName As String, ByVal TriggerResult As Object, ByVal TrnDate As Date, ByVal strTempFilePath As String, ByVal Type As frmDM_Template.Type, ByVal DM_Type As TemplateCategoryID, ByVal mynode As myList, Optional ByVal DueType As String = "", Optional ByVal DueValue As String = "", Optional ByVal IsOverride As Boolean = False, Optional ByVal sReason As String = "", Optional ByVal sNotes As String = "", Optional ByVal bIsGiven As Boolean = False, Optional ByVal bIsRecurring As Boolean = False, Optional ByVal DMTemplateDtlInfo As String = "") As Int64
        '     
        '' Save DM Trans Dtl
        Dim oDB As DataBaseLayer = Nothing
        Dim oParamater As DBParameter = Nothing
        Dim oPrintParamater As DBParameter = Nothing
        Dim oFaxParamater As DBParameter = Nothing
        Try
            oDB = New DataBaseLayer


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.DateTime
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_dtTransDate" '"@Trn_Date"
            oParamater.Value = TrnDate
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_nPatientID" '"@PatientID"
            oParamater.Value = Patient_ID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_nCriteriaID" '"@CriteriaID"
            oParamater.Value = CriteriaID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            ''''Pramod
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "DM_nTriggerID"
            oParamater.Value = TriggerID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Image
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_sResult" '"@Result"
            'SLR: Directly assigning on 12/2/2014
            If (IsNothing(TriggerResult) = False) Then
                oParamater.Value = CType(TriggerResult, Byte()).Clone()
            Else
                oParamater.Value = DBNull.Value
            End If
            'Dim img As Byte() = Nothing

            'img = TriggerResult
            '' Dim objword As New clsWordDocument
            '' '' To convert from Object to Binary Format
            ''If strTempFilePath = "" Then
            ''    oParamater.Value = DBNull.Value
            ''Else
            ''    oParamater.Value = objword.ConvertFiletoBinary(strTempFilePath)
            ''End If

            ''If TriggerResult Is Nothing Then
            ''    oParamater.Value = DBNull.Value
            ''Else
            'If Not IsNothing(img) Then
            '    oParamater.Value = img
            'Else
            '    oParamater.Value = DBNull.Value
            'End If

            'img = Nothing
            ''End If

            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oPrintParamater = New DBParameter
            oPrintParamater.DataType = SqlDbType.Int
            oPrintParamater.Direction = ParameterDirection.Input
            oPrintParamater.Name = "@DM_nPrint" '"@Print"

            oFaxParamater = New DBParameter
            oFaxParamater.DataType = SqlDbType.Int
            oFaxParamater.Direction = ParameterDirection.Input
            oFaxParamater.Name = "@DM_nFax" '"@FAX"

            '''' Fill the Status
            If Type = frmDM_Template.Type.Print Then
                '' IF Template is Printing
                oPrintParamater.Value = Type.Print
                oFaxParamater.Value = 0

            ElseIf Type = frmDM_Template.Type.Fax Then
                '' IF Template is send as FAX
                oPrintParamater.Value = 0
                oFaxParamater.Value = Type.Fax

            ElseIf Type = frmDM_Template.Type.Save Then
                '' IF Template is Save as Image
                oPrintParamater.Value = 0
                oFaxParamater.Value = 0

            End If

            oDB.DBParametersCol.Add(oPrintParamater)
            oPrintParamater = Nothing
            oDB.DBParametersCol.Add(oFaxParamater)
            oFaxParamater = Nothing

            ''''Pramod
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.SmallInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_nType"
            oParamater.Value = DM_Type
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            ''''Pramod
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_DueType"
            oParamater.Value = DueType
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            ''''Pramod
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_DueValue"
            oParamater.Value = DueValue
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Bit
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_bIsOverride" '"@IsOverride"
            oParamater.Value = IsOverride
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_sReason" '"@Reason"
            oParamater.Value = sReason
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            ''''Pramod
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_sNotes"
            oParamater.Value = sNotes
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            ''''Pramod
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Bit
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_bIsGiven"
            oParamater.Value = bIsGiven
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            ''''Pramod
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Bit
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_bIsRecurring"
            oParamater.Value = bIsRecurring
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
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_TriggerName"
            oParamater.Value = TriggerName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DM_CriteriaName"
            oParamater.Value = CriteriaName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@dm_triggerdtlinfo" ' "@Trn_Id"
            oParamater.Value = DMTemplateDtlInfo
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sDrugForm" '
            oParamater.Value = mynode.DrugForm
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sRoute "
            oParamater.Value = mynode.Route
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sFrequency"
            oParamater.Value = mynode.Frequency
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sNDCCode"
            oParamater.Value = mynode.NDCCode
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nIsNarcotics"
            oParamater.Value = mynode.IsNarcotic
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sDuration"
            oParamater.Value = mynode.Duration
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sDrugQtyQualifier"
            oParamater.Value = mynode.DrugQtyQualifier
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing



            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.InputOutput
            oParamater.Name = "@DM_TransId" ' "@Trn_Id"
            oParamater.Value = Trn_Id
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            Trn_Id = oDB.Add("DM_InsertPatientTemplate")
            'oDB.Add("DM_InsertTran")

            If Type = frmDM_Template.Type.Fax Then
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Fax, "Patient Guideline Faxed", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101009
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Fax, "Patient Guideline Faxed", Patient_ID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PHIExport, " Patient Guideline Faxed", gstrLoginName, gstrClientMachineName, Patient_ID)
            ElseIf Type = frmDM_Template.Type.Print Then
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Print, "Patient Guideline Printed", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101009
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Print, "Patient Guideline Printed", Patient_ID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PHIExport, " Patient Guideline Printed", gstrLoginName, gstrClientMachineName, Patient_ID)
            ElseIf Type = frmDM_Template.Type.Save Then
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Add, "Patient Guideline Saved", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101009
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Add, "Patient Guideline Saved", Patient_ID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, " Patient Guideline Saved", gstrLoginName, gstrClientMachineName, Patient_ID)
            End If
            Return Trn_Id
            ' objAudit = Nothing
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Add, "clsDM_Template -- Save_Template -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsDM_Template -- Save_Template -- " & ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Add, "clsDM_Template -- Save_Template -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsDM_Template -- Save_Template -- " & ex.ToString)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
            End If
        End Try
    End Function
#End Region

End Class

Public Enum EnumGuidelineResult
    None = 0
    Due = 1
    OverDue = 2
    Given = 3
End Enum
Public Enum EnumTriggerType
    None = 0
    Labs = 1
    Radiology = 2
    Drugs = 3
    Guidelines = 4
End Enum
'Public Enum EnumOrderType
'    none = 0
'    Lab = 1
'    order = 2
'    Referrals = 3
'    Rx = 4
'    Guidlines = 5
'End Enum
Public Class GuidelineResults
    Private _TemplateID As Long
    Private _TemplateName As String
    Private _PatientName As String
    Private _PatientCode As String
    Private _GuidelineIs As EnumGuidelineResult
    Private _GuidelineDueOverDueDate As Date
    Private _OnSetDate As Date

    Public Property TemplateID() As Long
        Get
            Return _TemplateID
        End Get
        Set(ByVal Value As Long)
            _TemplateID = Value
        End Set
    End Property

    Public Property TemplateName() As String
        Get
            Return _TemplateName
        End Get
        Set(ByVal Value As String)
            _TemplateName = Value
        End Set
    End Property

    Public Property PatientName() As String
        Get
            Return _PatientName
        End Get
        Set(ByVal Value As String)
            _PatientName = Value
        End Set
    End Property



    Public Property PatientCode() As String
        Get
            Return _PatientCode
        End Get
        Set(ByVal Value As String)
            _PatientCode = Value
        End Set
    End Property

    Public Property GuidelineIs() As EnumGuidelineResult
        Get
            Return _GuidelineIs
        End Get
        Set(ByVal Value As EnumGuidelineResult)
            _GuidelineIs = Value
        End Set
    End Property

    Public Property GuidelineDueOverDueDate() As Date
        Get
            Return _GuidelineDueOverDueDate
        End Get
        Set(ByVal Value As Date)
            _GuidelineDueOverDueDate = Value
        End Set
    End Property

    Public Property OnSetDate() As Date
        Get
            Return _OnSetDate
        End Get
        Set(ByVal Value As Date)
            _OnSetDate = Value
        End Set
    End Property

    Public Sub New()
        MyBase.new()
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class

Public Class DueOverDueResult
    'Inherits System.Collections.CollectionBase
    Private _PatientName As String
    Private _PatientCode As String
    Private _TirggerName As String
    Private _CriteriaName As String
    Private _DueOn As String
    Private _DueDate As String
    Private _Reason As String
    Private _Notes As String


    Public Property PatientName() As String
        Get
            Return _PatientName
        End Get
        Set(ByVal Value As String)
            _PatientName = Value
        End Set
    End Property

    Public Property PatientCode() As String
        Get
            Return _PatientCode
        End Get
        Set(ByVal Value As String)
            _PatientCode = Value
        End Set
    End Property

    Public Property TriggerName() As String
        Get
            Return _TirggerName
        End Get
        Set(ByVal value As String)
            _TirggerName = value
        End Set
    End Property
    Public Property CriteriaName() As String
        Get
            Return _CriteriaName
        End Get
        Set(ByVal value As String)
            _CriteriaName = value
        End Set
    End Property
    Public Property DueOn() As String
        Get
            Return _DueOn
        End Get
        Set(ByVal value As String)
            _DueOn = value
        End Set
    End Property
    Public Property DueDate() As String
        Get
            Return _DueDate
        End Get
        Set(ByVal value As String)
            _DueDate = value
        End Set
    End Property
    Public Property Reason() As String
        Get
            Return _Reason
        End Get
        Set(ByVal value As String)
            _Reason = value
        End Set
    End Property
    Public Property Notes() As String
        Get
            Return _Notes
        End Get
        Set(ByVal value As String)
            _Notes = value
        End Set
    End Property

    Public Sub New()
        MyBase.new()
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class

Public Class DueOverDueResults
    Implements System.Collections.IEnumerable
    Private mCol As Collection

    Public Sub Dispose()
        If (IsNothing(mCol) = False) Then
            mCol.Clear()
            mCol = Nothing
        End If
    End Sub
    Public Function Add(ByRef oDueOverDueResult As DueOverDueResult) As DueOverDueResult
        'create a new object
        Dim objNewMember As DueOverDueResult
        objNewMember = New DueOverDueResult

        'set the properties passed into the method

        objNewMember.PatientName = oDueOverDueResult.PatientName
        objNewMember.PatientCode = oDueOverDueResult.PatientCode
        objNewMember.TriggerName = oDueOverDueResult.TriggerName
        objNewMember.CriteriaName = oDueOverDueResult.CriteriaName
        objNewMember.DueOn = oDueOverDueResult.DueOn
        objNewMember.DueDate = oDueOverDueResult.DueDate
        objNewMember.Reason = oDueOverDueResult.Reason
        objNewMember.Notes = oDueOverDueResult.Notes


        'If Len(sKey) = 0 Then
        mCol.Add(objNewMember)
        'Else
        '    mCol.Add objNewMember, sKey
        'End If


        'return the object created
        Add = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1029"'
        objNewMember = Nothing
    End Function

    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As DueOverDueResult
        Get
            Item = mCol.Item(vntIndexKey)
        End Get
    End Property

    Public ReadOnly Property Count() As Integer
        Get
            Count = mCol.Count()
        End Get
    End Property

    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1055"'
        'GetEnumerator = mCol.GetEnumerator
        Return Nothing
    End Function

    Public Sub Remove(ByRef vntIndexKey As Object)
        mCol.Remove(vntIndexKey)
    End Sub

    Public Sub New()
        MyBase.New()
        mCol = New Collection
    End Sub

    Protected Overrides Sub Finalize()
        Clear()
        mCol = Nothing
        MyBase.Finalize()
    End Sub

    Public Sub Clear()
        If mCol Is Nothing Then Exit Sub ' Shouldn't happen, but just in case.

        Dim i As Short
        For i = mCol.Count() To 1 Step -1
            mCol.Remove(i)
        Next i
    End Sub
End Class

