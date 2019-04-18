Imports System.Data.SqlClient

Public Class clsAuditHistory
    Public Function GetProblemHistory(ByVal nTransactionID As Int64, ByVal sActivityType As String) As DataTable
        Dim _dt As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim oParamater As New gloDatabaseLayer.DBParameters()
        Dim sStartdate As String = ""
        Dim sToDate As String = ""
        Try
            If oDB IsNot Nothing Then
                If oDB.Connect(False) Then

                    oParamater.Add("@nProblemID", nTransactionID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oParamater.Add("@nActionType", sActivityType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                    oDB.Retrive("Get_AuditTrialDetails_ProblemList", oParamater, _dt)


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

    Public Function GetVitalHistory(ByVal nTransactionID As Int64, ByVal sActivityType As String) As DataTable
        Dim _dt As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim oParamater As New gloDatabaseLayer.DBParameters()
        Dim sStartdate As String = ""
        Dim sToDate As String = ""
        Try
            If oDB IsNot Nothing Then
                If oDB.Connect(False) Then

                    oParamater.Add("@VitalID", nTransactionID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oParamater.Add("@nActionType", sActivityType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                    oDB.Retrive("Get_AuditTrailDetails_Vitals", oParamater, _dt)


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

    Public Function GetCarePlanHistory(ByVal nTransactionID As Int64, ByVal sActivityType As String) As DataTable
        Dim _dt As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim oParamater As New gloDatabaseLayer.DBParameters()
        Dim sStartdate As String = ""
        Dim sToDate As String = ""
        Try
            If oDB IsNot Nothing Then
                If oDB.Connect(False) Then

                    oParamater.Add("@CarePlanID", nTransactionID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oParamater.Add("@nActionType", sActivityType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                    oDB.Retrive("Get_AuditTrailDetails_CarePlan", oParamater, _dt)


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

    Public Function GetMedicationHistory(ByVal nTransactionID As Int64, ByVal sActivityType As String) As DataTable
        Dim _dt As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim oParamater As New gloDatabaseLayer.DBParameters()
        Dim sStartdate As String = ""
        Dim sToDate As String = ""
        Try
            If oDB IsNot Nothing Then
                If oDB.Connect(False) Then

                    oParamater.Add("@nMedicationID", nTransactionID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oParamater.Add("@nActionType", sActivityType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                    oDB.Retrive("Get_AuditTrialDetails_Medication", oParamater, _dt)


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

    Public Function GetPrescriptionHistory(ByVal nTransactionID As Int64, ByVal sActivityType As String) As DataTable
        Dim _dt As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim oParamater As New gloDatabaseLayer.DBParameters()
        Dim sStartdate As String = ""
        Dim sToDate As String = ""
        Try
            If oDB IsNot Nothing Then
                If oDB.Connect(False) Then

                    oParamater.Add("@nPrescriptionID", nTransactionID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oParamater.Add("@nActionType", sActivityType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                    oDB.Retrive("Get_AuditTrialDetails_Prescription", oParamater, _dt)


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
    Public Function GetImplantableDevicesHistory(ByVal nTransactionID As Int64, ByVal sActivityType As String) As DataTable
        Dim _dt As DataTable = Nothing
        Dim oDB As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim oParamater As New gloDatabaseLayer.DBParameters()
        Dim sStartdate As String = ""
        Dim sToDate As String = ""
        Try
            If oDB IsNot Nothing Then
                If oDB.Connect(False) Then

                    oParamater.Add("@nDevicelist_Id", nTransactionID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oParamater.Add("@sActionType", sActivityType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                    oDB.Retrive("gsp_getImplantableDevicesAudit", oParamater, _dt)
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
    Public Function GetSPBHistory(ByVal nAuditId As Int64) As DataTable
        Dim _dt As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim oParamater As New gloDatabaseLayer.DBParameters()
        Dim sStartdate As String = ""
        Dim sToDate As String = ""
        Try
            If oDB IsNot Nothing Then
                If oDB.Connect(False) Then

                    oParamater.Add("@nAuditId", nAuditId, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)                    
                    oDB.Retrive("SBP_PatientData_Audit", oParamater, _dt)


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

    Public Function GetHistoryofHistory(ByVal nTransactionID As Int64, ByVal sActivityType As String) As DataTable
        Dim _dt As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim oParamater As New gloDatabaseLayer.DBParameters()
        Dim sStartdate As String = ""
        Dim sToDate As String = ""
        Try
            If oDB IsNot Nothing Then
                If oDB.Connect(False) Then

                    oParamater.Add("@nHistoryID", nTransactionID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oParamater.Add("@nActionType", sActivityType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                    oDB.Retrive("Get_AuditTrailDetails_PatientHistory", oParamater, _dt)


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

    Public Function GetPatientAuditHistory(ByVal nPatientID As Int64, ByVal nAuditHistoryID As Int64) As DataTable
        Dim _dt As DataTable = Nothing
        Dim oDB As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim oParamater As New gloDatabaseLayer.DBParameters()
        Dim sStartdate As String = ""
        Dim sToDate As String = ""
        Try
            If oDB IsNot Nothing Then
                If oDB.Connect(False) Then

                    oParamater.Add("@nPatientID", nPatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oParamater.Add("@nAuditTrailID", nAuditHistoryID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                    oDB.Retrive("Get_AuditTrialDetails_PatientAudit", oParamater, _dt)


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

    Public Function GetExamHistory(ByVal nTransactionID As Int64, ByVal sActivityType As String) As DataTable
        Dim _dt As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim oParamater As New gloDatabaseLayer.DBParameters()
        Dim sStartdate As String = ""
        Dim sToDate As String = ""
        Try
            If oDB IsNot Nothing Then
                If oDB.Connect(False) Then
                    oParamater.Add("@ExamID", nTransactionID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oParamater.Add("@ActionType", sActivityType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                    oDB.Retrive("Get_AuditTrailDetails_PatientExams_AuditLog", oParamater, _dt)
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

    Public Function GetExamHistory(ByVal nTransactionID As Int64, ByVal nPatientExamsAuditID As Int64, ByVal sActivityType As String) As DataTable
        Dim _dt As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim oParamater As New gloDatabaseLayer.DBParameters()
        Dim sStartdate As String = ""
        Dim sToDate As String = ""
        Try
            If oDB IsNot Nothing Then
                If oDB.Connect(False) Then

                    oParamater.Add("@ExamID", nTransactionID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oParamater.Add("@PatientExamsAuditID", nPatientExamsAuditID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oParamater.Add("@nActionType", sActivityType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                    oDB.Retrive("Get_AuditTrailDetails_PatientExams", oParamater, _dt)


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

    Public Function GetImmunizationHistory(ByVal nTransactionID As Int64, ByVal sActivityType As String) As DataTable
        Dim _dt As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim oParamater As New gloDatabaseLayer.DBParameters()
        Dim sStartdate As String = ""
        Dim sToDate As String = ""
        Try
            If oDB IsNot Nothing Then
                If oDB.Connect(False) Then

                    oParamater.Add("@ImmunizationID", nTransactionID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oParamater.Add("@nActionType", sActivityType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                    oDB.Retrive("Get_AuditTrailDetails_Immunization", oParamater, _dt)


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

    Public Function GetPatientHistory(ByVal nTransactionID As Int64, ByVal sActivityType As String) As DataTable
        Dim _dt As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim oParamater As New gloDatabaseLayer.DBParameters()
        Dim sStartdate As String = ""
        Dim sToDate As String = ""
        Try
            If oDB IsNot Nothing Then
                If oDB.Connect(False) Then

                    oParamater.Add("@nPatientID", nTransactionID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oParamater.Add("@nActionType", sActivityType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                    oDB.Retrive("Get_AuditTrialDetails_Patient", oParamater, _dt)


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

    Public Function ShowUserRightsAudit(ByVal _modifiedUserID As Int64, ByVal ActionType As String) As DataTable

        Dim oDB As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim oDBParameters As gloDatabaseLayer.DBParameters = Nothing
        Dim dt As DataTable = Nothing

        Try

            oDB.Connect(False)
            oDBParameters = New gloDatabaseLayer.DBParameters
            oDBParameters.Add("@nmodifiedUserID", _modifiedUserID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@ActionType", ActionType, ParameterDirection.Input, SqlDbType.VarChar)
            oDB.Retrive("Get_AuditTrailDetails_UserRights", oDBParameters, dt)

        Catch ex As Exception


            MessageBox.Show("Error on UserRights." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If oDBParameters IsNot Nothing Then
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If

            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB = Nothing
            End If
        End Try
        Return dt

    End Function
    Public Function GetLabOrderHistory(ByVal nTransactionID As Int64, ByVal sActivityType As String) As DataSet
        Dim _ds As DataSet = Nothing
        Dim oDB As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim oParamater As New gloDatabaseLayer.DBParameters()

        Try
            If oDB IsNot Nothing Then
                If oDB.Connect(False) Then

                    oParamater.Add("@OrderID", nTransactionID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oDB.Retrive("Get_AuditTrailDetails_Labs", oParamater, _ds)

                End If
            End If
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

        Return _ds
    End Function
    Public Function GetPatientEducationHistory(ByVal nTransactionID As Int64, ByVal sActivityType As String) As DataTable
        Dim _dt As DataTable = Nothing
        Dim oDB As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim oParamater As New gloDatabaseLayer.DBParameters()
        Dim sStartdate As String = ""
        Dim sToDate As String = ""
        Try
            If oDB IsNot Nothing Then
                If oDB.Connect(False) Then

                    oParamater.Add("@nEducationid", nTransactionID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oParamater.Add("@sActionType", sActivityType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                    oDB.Retrive("gsp_getPatientEducationAudit", oParamater, _dt)
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

    Public Function GetCarePlanHistory_V2(ByVal nID As Int64, ByVal sModule As String) As DataSet
        Dim ds As DataSet = Nothing

        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim oDBPara As gloDatabaseLayer.DBParameters = Nothing

        Try
            oDB = New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
            oDBPara = New gloDatabaseLayer.DBParameters


            oDBPara.Add("@nID ", nID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBPara.Add("@ModuleFlag", sModule, ParameterDirection.Input, SqlDbType.VarChar)

            oDB.Connect(False)
            oDB.Retrive("CP_CarePlan_Hst", oDBPara, ds)

            If Not ds Is Nothing Then
                ds.Tables(0).TableName = "CarePlanHisory"
                ds.Tables(1).TableName = "AssociationHistory"
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oDBPara) Then
                oDBPara.Dispose()
                oDBPara = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
            End If
        End Try

        Return ds

    End Function
End Class
