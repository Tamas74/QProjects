
Public Class clsAuditHistory
    Public Function GetProblemHistory(ByVal nTransactionID As Int64, ByVal sActivityType As String) As DataTable
        Dim _dt As DataTable = Nothing
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
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
    Public Function GetImplantableDevicesHistory(ByVal nTransactionID As Int64, ByVal sActivityType As String) As DataTable
        Dim _dt As DataTable = Nothing
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
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
    
    Public Function GetVitalHistory(ByVal nTransactionID As Int64, ByVal sActivityType As String) As DataTable
        Dim _dt As DataTable = Nothing
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
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
        Dim _dt As DataTable = Nothing
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
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
        Dim _dt As DataTable = Nothing
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
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
    Public Function GetReconcileDetails(ByVal nTransactionID As Int64, ByVal sActivityType As String, ByVal sModuleName As String, ByVal IsUpdatedReconcile As Boolean) As DataTable
        Dim _dt As DataTable = Nothing
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParamater As New gloDatabaseLayer.DBParameters()
        Dim sStartdate As String = ""
        Dim sToDate As String = ""
        Try
            If oDB IsNot Nothing Then
                If oDB.Connect(False) Then

                    oParamater.Add("@nID", nTransactionID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    If IsUpdatedReconcile = True Then
                        oParamater.Add("@nActionType", 1, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                    Else
                        oParamater.Add("@nActionType", 0, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                    End If

                    oParamater.Add("@ModuleName", sModuleName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                    oDB.Retrive("Get_AuditTrialDetails_CCDReconcile", oParamater, _dt)


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
    Public Function GetLabOrderHistory(ByVal nTransactionID As Int64, ByVal sActivityType As String) As DataSet
        Dim _ds As DataSet = Nothing
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
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
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
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
    Public Function GetPrescriptionHistory(ByVal nTransactionID As Int64, ByVal sActivityType As String) As DataTable
        Dim _dt As DataTable = Nothing
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
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

    Public Function GetHistoryofHistory(ByVal nTransactionID As Int64, ByVal sActivityType As String) As DataTable
        Dim _dt As DataTable = Nothing
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
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

    Public Function GetExamHistory(ByVal nTransactionID As Int64, ByVal sActivityType As String) As DataTable
        Dim _dt As DataTable = Nothing
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
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
        Dim _dt As DataTable = Nothing
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
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
        Dim _dt As DataTable = Nothing
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
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
        Dim _dt As DataTable = Nothing
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
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

    Public Function GetPatientAuditHistory(ByVal nPatientID As Int64, ByVal nAuditHistoryID As Int64) As DataTable
        Dim _dt As DataTable = Nothing
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
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
End Class
