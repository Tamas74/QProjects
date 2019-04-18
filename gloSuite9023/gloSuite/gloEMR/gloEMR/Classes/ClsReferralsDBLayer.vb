Imports System.IO
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports gloEMR.gloEMRWord

Public Class ClsReferralsDBLayer


    Private Dv As DataView
    ' Private Tb As DataTable 'slr not used

    Public Function FetchData(ByVal PatientID As Long) As Boolean
        Dim oDB As New DataBaseLayer
        Try


            Dim oParamater As DBParameter

            Dim oResultTable As DataTable

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nPatientID"
            oParamater.Value = PatientID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oResultTable = oDB.GetDataTable("gsp_ViewReferrals")
            If (IsNothing(Dv) = False) Then
                Dv.Dispose()
                Dv = Nothing
            End If
            If (IsNothing(oResultTable) = False) Then
                Dv = New DataView(oResultTable.Copy())
                oResultTable.Dispose()
                oResultTable = Nothing
            End If
          
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.View, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            Return False
        Finally
            If Not IsNothing(oDB) Then  ''slr free odb
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function
    Public Function CheckReferral(ByVal VisitId As Long, ByVal ExamId As Long, ByVal intPatientId As Int64) As Boolean

        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter

        Try

            Dim cntRecords As Int32 ''slr new is not needed 

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nPatientID"

            'If intPatientId = 0 Then
            '    oParamater.Value = gnPatientID
            'Else
            oParamater.Value = intPatientId
            'End If
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nvisitId"
            oParamater.Value = VisitId
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            ''Sandip Darade 20090739
            ''temporarily we are saving Exam ID to the field nNumberofvisit

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nExamId"
            oParamater.Value = ExamId
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            cntRecords = oDB.GetDataValue("gsp_CheckReferrals")

            '  cntRecords = oDB.GetDataValue("gsp_CheckReferrals")

            If cntRecords <> 0 Then
                Return False
            Else

                Return True
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            Return False
        Finally
            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Public Function AddData(ByVal ArrList As ArrayList, ByVal VisitID As Long, ByRef ReferralDate As DateTime, ByVal intPatientId As Int64, ByVal ExamId As Long, Optional ByVal SaveStatus As Integer = 1, Optional ByVal ReferralID As Long = 0) As Boolean
        'optional parameter ReferralID added by dipak 20100106 to used for when refferals form open from view->refferals
        'Dim DeleteStatus As Boolean
        Try

            Dim oDB As DataBaseLayer
            Dim oParamater As DBParameter

            If SaveStatus = 2 AndAlso ReferralID = 0 Then
                ' if Referral is opened from Exam the it should delete all Refrrals first & the insert
                '' for Referral which is open from View Referral it should not delete by this method
                oDB = New DataBaseLayer
                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@PatientID"
                oParamater.Value = intPatientId
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@visitId"
                oParamater.Value = VisitID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@nReferralID"
                oParamater.Value = 0
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@nExamId"
                oParamater.Value = ExamId
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oDB.Delete("gsp_DeleteReferrals")
                oDB.Dispose()

            End If

            If SaveStatus = 1 Then
                ReferralDate = Now
            End If


            For i As Int16 = 0 To ArrList.Count - 1
                Dim objmylist As myList
                objmylist = CType(ArrList.Item(i), myList)

                oDB = New DataBaseLayer
                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@nReferralID"
                'line modify and commented by dipak 20100106 to update data of refferalid=ReferralID  in other case referal id passed 0 which is default value
                'oParamater.Value = 0
                oParamater.Value = ReferralID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@nvisitId"
                oParamater.Value = VisitID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@nPatientID"
                oParamater.Value = intPatientId
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.DateTime
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@dtReferralDate"
                oParamater.Value = ReferralDate
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@nReferralToFrom"
                oParamater.Value = objmylist.Index

                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@nTemplateID"
                oParamater.Value = objmylist.ID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.Bit
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@bIsPCP"
                oParamater.Value = objmylist.Type
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing


                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.Image
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@TemplateResult"

                If IsNothing(objmylist.TemplateResult) = False Then
                    'SLR: Assigned directly lst.TemplateResult on 12/2/2014
                     
                    If (IsNothing(objmylist.TemplateResult) = False) Then
                        oParamater.Value = CType(objmylist.TemplateResult, Byte()).Clone() 'objword.ConvertFiletoBinary(objword.GenerateFile(lst.TemplateResult, strFileName))
                    Else
                        oParamater.Value = DBNull.Value
                    End If
                End If
                'Dim strFileName As String
                'strFileName = ExamNewDocumentName
                'Dim objword As New clsWordDocument
                ' '' To convert from Object to Binary Format
                'oParamater.Value = objword.ConvertFiletoBinary(objword.GenerateFile(objmylist.TemplateResult, strFileName))
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing
                ' objword = Nothing


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
                oParamater.Name = "@sFirstName"
                oParamater.Value = objmylist.ContactFirstName
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.VarChar
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@sMiddleName"
                oParamater.Value = objmylist.ContactMiddleName
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing



                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.VarChar
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@sLastName"
                oParamater.Value = objmylist.ContactLastName
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing


                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.VarChar
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@sGender"
                oParamater.Value = objmylist.ContactGender
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing


                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.VarChar
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@sDegree"
                oParamater.Value = objmylist.ContactDegree
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.VarChar
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@sStreet"
                oParamater.Value = objmylist.ContactAddressLine1
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.VarChar
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@sAddressLine2"
                oParamater.Value = objmylist.ContactAddressLine2
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.VarChar
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@sCity"
                oParamater.Value = objmylist.ContactCity
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.VarChar
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@sState"
                oParamater.Value = objmylist.ContactState
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing


                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.VarChar
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@sZIP"
                oParamater.Value = objmylist.ContactZip
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.VarChar
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@sPhone"
                oParamater.Value = objmylist.ContactPhone
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.VarChar
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@sFax"
                oParamater.Value = objmylist.ContactFax
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.VarChar
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@sMobile"
                oParamater.Value = objmylist.ContactMobile
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.VarChar
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@sExternalCode"
                oParamater.Value = objmylist.ContactExternalCode
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.VarChar
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@sTemplateName"
                oParamater.Value = objmylist.ContactTemplateName
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing
                'nNumberofvisit()

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.VarChar
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@nNumberofvisit"
                oParamater.Value = ExamId
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                ''Sandip Darade  20100520

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@nContactID"
                oParamater.Value = objmylist.Index ''contactID 
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@nContactFlag"
                oParamater.Value = objmylist.ContactFlag  ''contactID 
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oDB.Add("gsp_InUpReferrals")
                oDB.Dispose() ''slr dispose oDB
                oDB = Nothing
            Next

            If SaveStatus = 1 Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Add, "Referrals Added", intPatientId, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Referrals Added", gstrLoginName, gstrClientMachineName, gnPatientID)
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Modify, "Referrals Modified", intPatientId, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Referrals Modified", gstrLoginName, gstrClientMachineName, gnPatientID)
            End If

            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Add, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MsgBox(ex.Message)

            Return False
        End Try
    End Function


    Public Sub DeleteData(ByVal ReferralID As Long, ByVal PatientID As Long)
        Dim oDB As New DataBaseLayer
        Try


            Dim oParamater As DBParameter


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nReferralId"
            oParamater.Value = ReferralID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            If oDB.Delete("gsp_DeleteReferrals") Then

                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Delete, "Referral Details Deleted", PatientID, ReferralID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Delete, "Referral Details Deleted", gstrLoginName, gstrClientMachineName, gnPatientID)
            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Delete, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MsgBox(ex.Message)
        Finally
            If Not IsNothing(oDB) Then  ''slr free oDB
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try

    End Sub

    'Public ReadOnly Property DsDataSet() As DataSet
    '    Get
    '        'Dv = Ds.Tables("Category_Mst").DefaultView
    '        Return Ds
    '        'Return Ds
    '    End Get
    'End Property
    Public ReadOnly Property DsDataview() As DataView
        Get

            Return Dv

        End Get

    End Property
    Public Sub SortDataview(ByVal strsort As String, Optional ByVal strSortOrder As String = "")
        'Dv.Sort = strsort
        If (IsNothing(Dv) = False) Then
            Dv.Sort = "[" & strsort & "]" & strSortOrder
        End If

    End Sub

    Public Function FillControls(ByVal FillType As Char, ByVal intPatientId As Long) As DataTable

        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter

        Try


            Dim oResultTable As DataTable = Nothing ''slr new is not needed

            If FillType = "R" Then

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@nPatientID"
                'If intPatientId = 0 Then
                'oParamater.Value = gnPatientID
                'Else
                oParamater.Value = intPatientId
                'End If

                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.Int
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@flag"
                oParamater.Value = 1
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oResultTable = oDB.GetDataTable("gsp_FillPatient_DTL")


            ElseIf FillType = "T" Then
                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.Int
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@flag"
                oParamater.Value = 10
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oResultTable = oDB.GetDataTable("gsp_FillTemplateGallery_MST")
            Else
                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.Char
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@Type"
                oParamater.Value = FillType
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oResultTable = oDB.GetDataTable("gsp_FillContacts_Mst")

            End If


            If Not oResultTable Is Nothing Then
                Return oResultTable
            Else
                Return Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.View, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MsgBox(ex.Message)
            Return Nothing
        Finally
            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Public Function FetchReferralsForUpdate(ByVal VisitId As Long, ByVal intPatientId As Int64, ByVal ExamID As Long) As DataTable
        Dim oDB As New DataBaseLayer
        Try

            Dim oParamater As DBParameter

            Dim oResultTable As DataTable = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nPatientID"
            oParamater.Value = intPatientId
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nVisitId"
            oParamater.Value = VisitId
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            ''Sandip Darade 20090739
            ''temporarily we are saving Exam ID to the field nNumberofvisit

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nExamId"
            oParamater.Value = ExamID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            oResultTable = oDB.GetDataTable("gsp_scanReferrals")

            If Not oResultTable Is Nothing Then

                Return oResultTable
            Else
                Return Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Modify, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MsgBox(ex.Message)
            Return Nothing
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function
    Public Function GetTemplate(ByVal TemplateID) As DataTable
        Dim oDB As New DataBaseLayer
        Try

            Dim oParamater As DBParameter

            Dim oResultTable As DataTable


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nTemplateID"
            oParamater.Value = TemplateID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@flag"
            oParamater.Value = 1
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oResultTable = oDB.GetDataTable("gsp_GetExamContents")

            If Not oResultTable Is Nothing Then

                Return oResultTable
            Else
                Return Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.View, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MsgBox(ex.Message)
            Return Nothing
        Finally
            If Not IsNothing(oDB) Then  ''slr free oDB
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function
   
    Public Sub SetRowFilter(ByVal ColIndex As Integer, ByVal txtSearch As String)
        Dim strexpr As String
        If (IsNothing(Dv) = False) Then
            strexpr = "" & Dv.Table.Columns(ColIndex).ColumnName() & "  Like '%" & txtSearch & "%'"
            Dv.RowFilter = strexpr
        End If

    End Sub
    Public Sub SetRowFilter(ByVal txtSearch As String)
        Dim strexpr As String
        Dim str As String = ""
        'str = Mid(str, 2)
        'str = Mid(str, 1, Len(str) - 1)
        '  str = EndsWith("]")
        If (IsNothing(Dv) = False) Then
            strexpr = "" & Dv.Table.Columns(str).ColumnName() & "  Like '" & txtSearch & "%'"
            Dv.RowFilter = strexpr
            str = Dv.Sort
        End If

    End Sub

    Public Function DefaultReferralLetter(ByVal sProviderName As String) As String
        Dim sDefaultReferralLetter As String = ""
        Dim oDB As New DataBaseLayer
        Try

      
        If Trim(sProviderName) <> "" Then


            Dim oParamater As DBParameter

            '    Dim oResultTable As New DataTable


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ProviderName"
            oParamater.Value = sProviderName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            sDefaultReferralLetter = oDB.GetDataValue("gsp_GetDefaultReferralLetter")

          
            If IsNothing(sDefaultReferralLetter) Then
                sDefaultReferralLetter = ""
            End If
          
        End If
        Return sDefaultReferralLetter

        Catch ex As Exception
            Return sDefaultReferralLetter
        Finally
            If Not IsNothing(oDB) Then  ''''slr free oDB
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Public Function GetReferralLetter(ByVal nTemplateId As Int64) As String
        Dim strReferralLetter As String = ""

        Dim oDB As New DataBaseLayer
        Try

            Dim strSQL As String = "Select sTemplateName from TemplateGallery_MST where nTemplateID=" & nTemplateId
            strReferralLetter = oDB.GetRecord_Query(strSQL)
            If Not IsNothing(strReferralLetter) Then
                Return strReferralLetter
            Else
                Return ""
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.View, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            Return ""
        Finally
            If Not IsNothing(oDB) Then  ''slr 
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Public Function CheckDirectAddress(ByVal nSendPhysicanID As Int64) As String
        Dim strDirectAddress As String = ""

        Dim oDB As New DataBaseLayer
        Try

            Dim strSQL As String = "Select ISNULL(c.sDirectAddress,'') FROM Contacts_MST c WHERE c.nContactID =" & nSendPhysicanID
            strDirectAddress = oDB.GetRecord_Query(strSQL)
            If Not IsNothing(strDirectAddress) Then
                Return strDirectAddress
            Else
                Return ""
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.View, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            Return ""
        Finally
            If Not IsNothing(oDB) Then  ''slr 
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function


    Public Function GetReferralTemplate(ByVal sProviderName As String) As Int64

        Dim nTemplate As Int64
        Dim oDB As New DataBaseLayer
        Dim strSQL As String
        Dim strProvider As String()

        Try

            strProvider = sProviderName.Split(" ")

            'strSQL = "SELECT ProviderSettings.nOthersID FROM  ProviderSettings inner join Provider_MST ON ProviderSettings.nProviderID = Provider_MST.nProviderID WHERE (ProviderSettings.sSettingsType = 'Referralletter') AND ISNULL(sFirstName,'')+Space(2)+ISNULL(sMiddleName,'')+Space(2)+ISNULL(sLastName,'')= '" & sProviderName & "'"
            'solving salesforce case-GLO2010-0007737
            strSQL = " Select ProviderSettings.nOthersID"
            strSQL &= " FROM         ProviderSettings INNER JOIN"
            strSQL &= " Provider_MST ON ProviderSettings.nProviderID = Provider_MST.nProviderID"
            strSQL &= " WHERE     (ProviderSettings.sSettingsType = 'Referralletter')  "
            strSQL &= " AND (RTRIM(LTRIM(ISNULL(Provider_MST.sFirstName, ''))) = '" & Replace(strProvider(0), "'", "''") & "' "

            If strProvider.Length = 3 Then
                strSQL &= "AND   RTRIM(LTRIM(ISNULL(Provider_MST.sMiddleName, ''))) = '" & Replace(Trim(strProvider(1)), "'", "''") & "' "
                strSQL &= " AND  RTRIM(LTRIM(ISNULL(Provider_MST.sLastName, ''))) = '" & Replace(Trim(strProvider(2)), "'", "''") & "')"
            End If

            If strProvider.Length = 2 Then
                strSQL &= " AND  RTRIM(LTRIM(ISNULL(Provider_MST.sLastName, ''))) = '" & Replace(Trim(strProvider(1)), "'", "''") & "')"
            End If


            nTemplate = oDB.GetDataValue(strSQL, False)

            If Not IsDBNull(nTemplate) Then
                Return nTemplate
            Else
                Return 0
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.View, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            Return 0

        Finally
            If Not IsNothing(oDB) Then  ''slr 
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try

    End Function
    'solving salesforce case-GLO2010-0007737
    Public Function GetReferralTemplateBYProviderID(ByVal lngProviderID As Long) As Int64
        Dim nTemplate As Int64
        Dim oDB As New DataBaseLayer
        Dim strSQL As String

        Try

            strSQL = " Select ProviderSettings.nOthersID "
            strSQL &= " FROM         ProviderSettings "
            strSQL &= " WHERE     (UPPER(ProviderSettings.sSettingsType) = 'REFERRALLETTER')  "
            strSQL &= "  AND (ProviderSettings.nProviderID=" & lngProviderID & ")"

            Dim thisObject As Object = oDB.GetDataValue(strSQL, False)
            If Not IsDBNull(thisObject) Then
                nTemplate = Convert.ToInt64(thisObject)
                Return nTemplate
            Else
                Return 0
            End If
            'If Not IsDBNull(nTemplate) Then
            '    Return nTemplate
            'Else
            '    Return 0
            'End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.View, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            Return 0

        Finally
            If Not IsNothing(oDB) Then  ''slr 
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function
    'End

    ''Sandip Darade 20100302
    Public Function GetPatientExamsInformation(ByVal blnGetdetail As Boolean, ByVal PatientID As Long, Optional ByVal ServiceDate As DateTime = Nothing) As DataTable
        Dim dt As DataTable = Nothing  ''slr no new reqd

        Dim oDB As New DataBaseLayer
        Try

            Dim strSQL As String
            If (blnGetdetail = False) Then ''get service dates (dates for exams) for patinet 
                strSQL = "  SELECT DISTINCT dtdos as  ServiceDate FROM PatientExams WHERE nPatientID = '" & PatientID & "'"
            Else ''get exam details  for a particcular service date 
                strSQL = "  SELECT ISNULL(nVisitID,0) as  VisitID,ISNULL(nExamID,0) as  ExamID ,ISNULL(sExamName,'') as ExamName,ISNULL(nProviderID,0) As  ProviderID ," _
                           & "ISNULL(bIsFinished,'false') as IsFinished  , ISNULL(sTemplateName,'') as TemplateName " _
                           & " FROM PatientExams WHERE  dtdos ='" & ServiceDate & "'  AND nPatientID = '" & PatientID & "'"
            End If

            dt = oDB.GetDataTable_Query(strSQL)
            Return dt
        Catch ex As Exception

            Return Nothing
        Finally
            If Not IsNothing(oDB) Then  ''slr 
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function
    Public Function GetReferralName(ByVal nConatctId As Int64) As String
        Dim strReferralName As String = ""

        Dim oDB As New DataBaseLayer
        Try
            ''''''Query modified by Anil on 20071126
            Dim strSQL As String = "Select REPLACE(LTRIM(ISNULL(sFirstName + SPACE(1), '')) + LTRIM(ISNULL(sMiddleName + SPACE(1), '')) + LTRIM(ISNULL(sLastName + SPACE(1), '')) + ISNULL(sDegree + SPACE(1), ' '), ',  ', '') as Name from Contacts_MST where sContactType='Physician' and nContactID=" & nConatctId
            ''''''
            strReferralName = oDB.GetRecord_Query(strSQL)
            If Not IsNothing(strReferralName) Then
                Return strReferralName
            Else
                Return ""
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            Return ""
        Finally
            If Not IsNothing(oDB) Then  ''slr 
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    ''sudhir 20081208  to fetch Referral
    Public Function GetReferral(ByVal ReferralID As Int64, ByVal PatientID As Int64) As DataTable
        Dim dt As DataTable  ''slr no new reqd
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter

        Dim oResultTable As New DataTable
        Try
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ReferralID"
            oParamater.Value = ReferralID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@PatientID"
            oParamater.Value = PatientID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            dt = oDB.GetDataTable("gsp_ScanReferralDoc")
            If Not dt Is Nothing Then
                Return dt
            Else
                Return Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient Education", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(oDB) Then  ''slr 
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Public Sub New()

    End Sub
    Public Sub Dispose()
        If (IsNothing(Dv) = False) Then
            Dv.Dispose()
            Dv = Nothing
        End If
    End Sub
End Class
