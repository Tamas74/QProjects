Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports gloEMRGeneralLibrary.gloEMRDatabase
'Imports System.Data.SqlClient
Public Class ClsAmendments

#Region "Amendments Enum"
    Public Enum RequestorType
        None = 0
        Patient = 1
        Provider = 2
        Other = 3
    End Enum

    Public Enum AmendmentStatus
        Accepted = 1
        Denied = 2
        Pending = 3
    End Enum

    Public Enum DeniedReasons
        DeniedReasonOne = 1 ''''The protected health information or record was not created by this organization
        DeniedReasonTwo = 2 ''''The protected health information is not part of the patient's "designated record set"
        DeniedReasonThree = 3  'The protected health information or record is not available to the patient for inspection as required by federal law (e.g., psychotherapy notes.)
        DeniedReasonfour = 4  ''The protected health information or record is accurate and complete
    End Enum
#End Region

#Region "Private Variables"
    Dim PatientID As Int64
    Dim AmendmentId As Int64

#End Region

#Region "Constructor "
    Public Sub New(ByVal _PatientId As Int64, ByVal _AmendmentId As Int64)
        Try
            PatientID = _PatientId
            AmendmentId = _AmendmentId
        Catch ex As SqlException
            '' gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Initialize, "clsCPT -- New -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsCPT -- New -- " & ex.ToString)
        Catch ex As Exception
            '' gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Initialize, "clsCPT -- New -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsCPT -- New -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try

    End Sub
#End Region

#Region " Property Procedures "

    Property AmendmentsRequestorType As Integer = 0
    Property RequestDate As DateTime = Date.MinValue
    Property RequestorOtherSpecified As String = ""
    Property RequestorProviderID As Int64 = 0
    Property RequestorPhone As String = ""
    Property AmendmentsReason As String = ""
    Property AmendmentsDetails As String = ""
    Property AmendmentStatusId As Integer = AmendmentStatus.Pending
    Property AcceptedOrDeniedDate As DateTime = Date.MinValue
    Property AcceptedOrDeniedUserID As Int64 = 0
    Property AmendmentsDeniedReasonOne As Integer = 0
    Property AmendmentsDeniedReasonTwo As Integer = 0
    Property AmendmentsDeniedReasonThree As Integer = 0
    Property AmendmentsDeniedReasonFour As Integer = 0
    Property AmendmentsDeniedNotes As String = ""
    Property AmendmentsDocumentID As Int64 = 0
#End Region

#Region "Public Event"
    Public Function SaveAmendmets() As Boolean

        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParamater As New gloDatabaseLayer.DBParameters()
        Try
            If oDB IsNot Nothing Then
                If oDB.Connect(False) Then
                    oParamater.Add("@nAmendmentId", AmendmentId, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oParamater.Add("@dtRequestDateTime", RequestDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.DateTime)
                    oParamater.Add("@nRequestorType", AmendmentsRequestorType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.TinyInt)
                    oParamater.Add("@sRequestorName", RequestorOtherSpecified, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                    oParamater.Add("@sRequestorPhone", RequestorPhone, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                    oParamater.Add("@nAmendmentToPatientId", PatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oParamater.Add("@sAmendmentReason", AmendmentsReason, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                    oParamater.Add("@sAmendmentDetails", AmendmentsDetails, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                    oParamater.Add("@nAmendmentStatus", AmendmentStatusId, System.Data.ParameterDirection.Input, System.Data.SqlDbType.TinyInt)

                    If AcceptedOrDeniedDate <> Date.MinValue Then
                        oParamater.Add("@dtAcceptedDeniedDateTime", AcceptedOrDeniedDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.DateTime)
                    End If

                    oParamater.Add("@nAcceptedDeniedUserId", AcceptedOrDeniedUserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oParamater.Add("@sAcceptedDeniedNotes", AmendmentsDeniedNotes, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                    oParamater.Add("@nDeniedReasonOne", AmendmentsDeniedReasonOne, System.Data.ParameterDirection.Input, System.Data.SqlDbType.TinyInt)
                    oParamater.Add("@nDeniedReasonTwo", AmendmentsDeniedReasonTwo, System.Data.ParameterDirection.Input, System.Data.SqlDbType.TinyInt)
                    oParamater.Add("@nDeniedReasonThree", AmendmentsDeniedReasonThree, System.Data.ParameterDirection.Input, System.Data.SqlDbType.TinyInt)
                    oParamater.Add("@nDeniedReasonFour", AmendmentsDeniedReasonFour, System.Data.ParameterDirection.Input, System.Data.SqlDbType.TinyInt)
                    oParamater.Add("@nCreatedUserId", gnLoginID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oParamater.Add("@sCreatedUser", gstrLoginName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                    oParamater.Add("@nProviderId", RequestorProviderID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oParamater.Add("@AmendmentsDocumentID", AmendmentsDocumentID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oDB.Execute("gsp_InUpAmendments", oParamater)
                    If AmendmentId = 0 Then
                        Select Case AmendmentStatusId

                            Case AmendmentStatus.Accepted

                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Amedments, gloAuditTrail.ActivityCategory.AddAmedments, gloAuditTrail.ActivityType.Add, "Amedment added and accepted", PatientID, AmendmentId, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                            Case AmendmentStatus.Denied

                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Amedments, gloAuditTrail.ActivityCategory.AddAmedments, gloAuditTrail.ActivityType.Add, "Amedment added and denied", PatientID, AmendmentId, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                            Case AmendmentStatus.Pending

                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Amedments, gloAuditTrail.ActivityCategory.AddAmedments, gloAuditTrail.ActivityType.Add, "Amedment added with pendning status", PatientID, AmendmentId, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                        End Select
                    Else


                        Select Case AmendmentStatusId

                            Case AmendmentStatus.Accepted

                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Amedments, gloAuditTrail.ActivityCategory.ModifyAmedments, gloAuditTrail.ActivityType.Modify, "Amedment accepted", PatientID, AmendmentId, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                            Case AmendmentStatus.Denied

                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Amedments, gloAuditTrail.ActivityCategory.ModifyAmedments, gloAuditTrail.ActivityType.Modify, "Amedment denied", PatientID, AmendmentId, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                            Case AmendmentStatus.Pending

                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Amedments, gloAuditTrail.ActivityCategory.ModifyAmedments, gloAuditTrail.ActivityType.Modify, "Amedment mark as pendning", PatientID, AmendmentId, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                        End Select

                    End If
                    

                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If

        Catch ex As gloDatabaseLayer.DBException
            ex.ERROR_Log(ex.ToString())
            If AmendmentId = 0 Then
                Select Case AmendmentStatusId

                    Case AmendmentStatus.Accepted

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Amedments, gloAuditTrail.ActivityCategory.AddAmedments, gloAuditTrail.ActivityType.Add, "Amedment added and accepted", PatientID, AmendmentId, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)

                    Case AmendmentStatus.Denied

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Amedments, gloAuditTrail.ActivityCategory.AddAmedments, gloAuditTrail.ActivityType.Add, "Amedment added and denied", PatientID, AmendmentId, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)

                    Case AmendmentStatus.Pending

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Amedments, gloAuditTrail.ActivityCategory.AddAmedments, gloAuditTrail.ActivityType.Add, "Amedment added with pedning status", PatientID, AmendmentId, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)

                End Select
            Else


                Select Case AmendmentStatusId

                    Case AmendmentStatus.Accepted

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Amedments, gloAuditTrail.ActivityCategory.ModifyAmedments, gloAuditTrail.ActivityType.Modify, "Amedment accepted", PatientID, AmendmentId, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)

                    Case AmendmentStatus.Denied

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Amedments, gloAuditTrail.ActivityCategory.ModifyAmedments, gloAuditTrail.ActivityType.Modify, "Amedment denied", PatientID, AmendmentId, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)

                    Case AmendmentStatus.Pending

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Amedments, gloAuditTrail.ActivityCategory.ModifyAmedments, gloAuditTrail.ActivityType.Modify, "Amedment mark as pedning", PatientID, AmendmentId, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)

                End Select

            End If
            Return False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            If AmendmentId = 0 Then
                Select Case AmendmentStatusId

                    Case AmendmentStatus.Accepted

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Amedments, gloAuditTrail.ActivityCategory.AddAmedments, gloAuditTrail.ActivityType.Add, "Amedment added and accepted", PatientID, AmendmentId, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)

                    Case AmendmentStatus.Denied

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Amedments, gloAuditTrail.ActivityCategory.AddAmedments, gloAuditTrail.ActivityType.Add, "Amedment added and denied", PatientID, AmendmentId, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)

                    Case AmendmentStatus.Pending

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Amedments, gloAuditTrail.ActivityCategory.AddAmedments, gloAuditTrail.ActivityType.Add, "Amedment added with pedning status", PatientID, AmendmentId, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)

                End Select
            Else


                Select Case AmendmentStatusId

                    Case AmendmentStatus.Accepted

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Amedments, gloAuditTrail.ActivityCategory.ModifyAmedments, gloAuditTrail.ActivityType.Modify, "Amedment accepted", PatientID, AmendmentId, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)

                    Case AmendmentStatus.Denied

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Amedments, gloAuditTrail.ActivityCategory.ModifyAmedments, gloAuditTrail.ActivityType.Modify, "Amedment denied", PatientID, AmendmentId, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)

                    Case AmendmentStatus.Pending

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Amedments, gloAuditTrail.ActivityCategory.ModifyAmedments, gloAuditTrail.ActivityType.Modify, "Amedment mark as pedning", PatientID, AmendmentId, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)

                End Select

            End If
            Return False
        Finally
            If oParamater IsNot Nothing Then
                oParamater.Dispose()
                oParamater = Nothing
            End If
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try

       
    End Function

    Public Function GetPatientAmedments()

        Dim _dt As DataTable = Nothing
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParamater As New gloDatabaseLayer.DBParameters()
        Dim sStartdate As String = ""
        Dim sToDate As String = ""
        Try
            If oDB IsNot Nothing Then
                If oDB.Connect(False) Then

                    oParamater.Add("@nPatientID", PatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oParamater.Add("@nAmendmentsID", AmendmentId, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oDB.Retrive("gsp_Amendment", oParamater, _dt)
                    If _dt IsNot Nothing AndAlso _dt.Rows.Count > 0 Then
                        AmendmentsRequestorType = _dt.Rows(0)("RequestorType")
                        RequestDate = _dt.Rows(0)("RequestDateTime")
                        RequestorOtherSpecified = _dt.Rows(0)("RequestorName")
                        RequestorPhone = _dt.Rows(0)("RequestorPhone")
                        AmendmentsReason = _dt.Rows(0)("AmendmentReason")
                        AmendmentsDetails = _dt.Rows(0)("AmendmentDetails")
                        AmendmentStatusId = _dt.Rows(0)("AmendmentStatus")
                        If Not IsDBNull(_dt.Rows(0)("AcceptedDeniedDateTime")) Then
                            AcceptedOrDeniedDate = _dt.Rows(0)("AcceptedDeniedDateTime")
                        End If
                        AcceptedOrDeniedUserID = _dt.Rows(0)("AcceptedDeniedUserId")
                        AmendmentsDeniedReasonOne = _dt.Rows(0)("DeniedReasonOne")
                        AmendmentsDeniedReasonTwo = _dt.Rows(0)("DeniedReasonTwo")
                        AmendmentsDeniedReasonThree = _dt.Rows(0)("DeniedReasonThree")
                        AmendmentsDeniedReasonFour = _dt.Rows(0)("DeniedReasonFour")
                        AmendmentsDeniedNotes = _dt.Rows(0)("AcceptedDeniedNotes")
                        RequestorProviderID = _dt.Rows(0)("ProviderId")
                        AmendmentsDocumentID = _dt.Rows(0)("DocumentID")

                    End If

                End If
            End If
        Catch ex As gloDatabaseLayer.DBException
            ex.ERROR_Log(ex.ToString())

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)

        Finally
            If oParamater IsNot Nothing Then
                oParamater.Dispose()
                oParamater = Nothing
            End If
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If

            If _dt IsNot Nothing Then
                _dt.Dispose()
                _dt = Nothing
            End If
        End Try

        Return Nothing
    End Function

    Public Function GetPatientAmedments(ByVal _nPatientID As Int64) As DataTable

        Dim _dt As DataTable = Nothing
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParamater As New gloDatabaseLayer.DBParameters()
        Dim sStartdate As String = ""
        Dim sToDate As String = ""
        Try
            If oDB IsNot Nothing Then
                If oDB.Connect(False) Then

                    oParamater.Add("@nPatientID", _nPatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oDB.Retrive("gsp_UPAmendments", oParamater, _dt)


                End If
            End If
        Catch ex As gloDatabaseLayer.DBException
            ex.ERROR_Log(ex.ToString())

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)

        Finally
            If oParamater IsNot Nothing Then
                oParamater.Dispose()
                oParamater = Nothing
            End If
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try

        Return _dt

    End Function

    Public Function DeleteAmedments() As Boolean
        Dim conn As New SqlConnection(GetConnectionString)
        Dim myTrans As SqlTransaction = Nothing
        Dim cmdAmedment As SqlCommand = Nothing

        Try
            conn.Open()

            myTrans = conn.BeginTransaction
            cmdAmedment = conn.CreateCommand
            cmdAmedment.Transaction = myTrans

            
            With cmdAmedment
                .Connection = conn
                .CommandType = CommandType.Text
                .CommandText = "DELETE FROM Patient_Amendments WHERE nAmendmentId=" & AmendmentId & " AND nAmendmentToPatientId=" & PatientID
                cmdAmedment.ExecuteNonQuery()
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Amedments, gloAuditTrail.ActivityCategory.DeleteAmedments, gloAuditTrail.ActivityType.Delete, "Amedment deleted", PatientID, AmendmentId, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End With

            myTrans.Commit()
           
            Return True

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Amedments, gloAuditTrail.ActivityCategory.DeleteAmedments, gloAuditTrail.ActivityType.Delete, "Amedment deleted", PatientID, AmendmentId, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Try
                myTrans.Rollback()
            Catch ex1 As SqlException
                If Not myTrans.Connection Is Nothing Then
                   
                End If
            End Try
            Return False
        Finally
            If myTrans IsNot Nothing Then
                myTrans.Dispose()
                myTrans = Nothing
            End If

            If conn IsNot Nothing Then
                conn.Close()
                conn.Dispose()
                conn = Nothing
            End If


            cmdAmedment.Dispose()
            cmdAmedment = Nothing
        End Try
    End Function

    Public Function GetActiveProvider() As DataTable
        Dim oDB As New DataBaseLayer
        Dim strSelect As String
        Dim dt As DataTable = Nothing
        Try
            strSelect = "select nProviderID,isnull(sFirstName,'') + ' ' + CASE ISNULL(sMiddleName,'') WHEN  '' THEN '' When sMiddleName then  " _
                                & "sMiddleName +  ' ' END +  isnull(sLastName,'') as Name from Provider_MST Order by Name"

            dt = oDB.GetDataTable_Query(strSelect)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            strSelect = Nothing
            oDB.Dispose()
            oDB = Nothing
        End Try

        Return dt


    End Function

    Public Function GetProviderPhonenumber(ByVal _nProviderID As Int64) As String
        Dim oDB As New DataBaseLayer
        Dim strSelect As String
        'Dim dt As DataTable = Nothing
        Dim _strPhone As String = ""

        Try
            strSelect = "select Top 1 ISNULL(sPhoneNo,'') as PhoneNo  from Provider_MST where nProviderID=" & _nProviderID
            _strPhone = oDB.GetRecord_Query(strSelect)
            'dt = oDB.GetDataTable_Query(strSelect)
            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            '    Return dt.Rows(0)("PhoneNo").ToString()
            'Else
            '    Return ""
            'End If
            Return _strPhone
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Return ""
        Finally
            strSelect = Nothing
            'If dt IsNot Nothing Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            oDB.Dispose()
            oDB = Nothing
        End Try
        'If dt IsNot Nothing And dt.Rows.Count > 0 Then

        '    Return dt.Rows(0)("PhoneNo").ToString()
        'Else
        '    Return ""
        'End If



    End Function
    Public Function GetPatientPhonenumber(ByVal _nPatientID As Int64) As String
        Dim oDB As New DataBaseLayer
        Dim strSelect As String
        'Dim dt As DataTable = Nothing
        Dim _strPhone As String = ""
        Try
            strSelect = "SELECT Top 1  ISNULL(sPhone,'') as PhoneNo FROM dbo.Patient WHERE nPatientID=" & _nPatientID

            _strPhone = oDB.GetRecord_Query(strSelect)
            Return _strPhone
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Return ""
        Finally
            strSelect = Nothing
            
            oDB.Dispose()
            oDB = Nothing
        End Try

        


    End Function

    Public Function GetActiveUser() As DataTable
        Dim oDB As New DataBaseLayer
        Dim strSelect As String
        Dim dt As DataTable = Nothing
        Try
            strSelect = "SELECT sLoginName AS LoginName,nUserID AS USERID FROM dbo.User_MST WHERE nBlockStatus=0 ORDER BY sLoginName"

            dt = oDB.GetDataTable_Query(strSelect)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            strSelect = Nothing
            oDB.Dispose()
            oDB = Nothing
        End Try

        Return dt


    End Function
#End Region

  

End Class
