Imports System.Data.SqlClient
Public Class ClsPatientCarePlan
    Implements IDisposable
    Private disposed As Boolean = False
    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        If disposed = False Then
            If disposing Then
                disposed = True
            End If
        End If
    End Sub

    Protected Overrides Sub Finalize()
        Dispose(False)
    End Sub


    Public Function GetPatientCarePlan(ByVal nPatientID As Int64, ByVal nCarePlanID As Int64) As DataTable
        Dim dtTable As DataTable = Nothing

        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim oDBPara As gloDatabaseLayer.DBParameters = Nothing

        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString)
            oDBPara = New gloDatabaseLayer.DBParameters


            oDBPara.Add("@nPatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBPara.Add("@nID", nCarePlanID, ParameterDirection.Input, SqlDbType.BigInt)

            oDB.Connect(False)
            oDB.Retrive("Get_Patient_CarePlan", oDBPara, dtTable)

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

        Return dtTable

    End Function

    Public Function GetLoincUnits() As DataTable
        Dim dtTable As DataTable = Nothing

        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim oDBPara As gloDatabaseLayer.DBParameters = Nothing

        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString)
            oDBPara = New gloDatabaseLayer.DBParameters


            oDB.Connect(False)
            oDB.Retrive("GetLoincsUnits", dtTable)

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

        Return dtTable

    End Function

    Public Function GetUnits() As DataTable
        Dim dtTable As DataTable = Nothing

        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim oDBPara As gloDatabaseLayer.DBParameters = Nothing

        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString)
            oDBPara = New gloDatabaseLayer.DBParameters


            oDB.Connect(False)
            oDB.Retrive("GetUnits", dtTable)

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

        Return dtTable

    End Function

    Public Function SavePatientCarePlan(ByVal nID As Int64, ByVal nPatientID As Int64, ByVal sProblem As String, ByVal sGoal As String, ByVal sNote As String, ByVal sInstruction As String) As Int64


        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim oDBPara As gloDatabaseLayer.DBParameters = Nothing

        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString)
            oDBPara = New gloDatabaseLayer.DBParameters
            oDBPara.Add("@nID", nID, ParameterDirection.InputOutput, SqlDbType.BigInt)
            oDBPara.Add("@nPatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBPara.Add("@sProblem", sProblem, ParameterDirection.Input, SqlDbType.VarChar)
            oDBPara.Add("@sGoal", sGoal, ParameterDirection.Input, SqlDbType.VarChar)
            oDBPara.Add("@sNote", sNote, ParameterDirection.Input, SqlDbType.VarChar)
            oDBPara.Add("@sInstruction", sInstruction, ParameterDirection.Input, SqlDbType.VarChar)
            oDB.Connect(False)
            oDB.Execute("INUP_Patient_CarePlan", oDBPara, nID)
            oDB.Disconnect()
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
        Return nID
    End Function

    Public Function DeActivatePatientCarePlan(ByVal nID As Int64)

        Dim oDB As gloDatabaseLayer.DBLayer = Nothing


        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString)
            oDB.Connect(False)
            oDB.Execute_Query("Update Patient_CarePlan set bIsActive=0 where nId=" & nID)
            oDB.Disconnect()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            If Not IsNothing(oDB) Then
                oDB.Dispose()
            End If
        End Try

        Return Nothing
    End Function

    Public Function ActivatePatientCarePlan(ByVal nID As Int64)

        Dim oDB As gloDatabaseLayer.DBLayer = Nothing


        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString)
            oDB.Connect(False)
            oDB.Execute_Query("Update Patient_CarePlan set bIsActive=1 where nId=" & nID)
            oDB.Disconnect()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            If Not IsNothing(oDB) Then
                oDB.Dispose()
            End If
        End Try

        Return Nothing
    End Function

    Public Function DeletePatientCarePlan(ByVal nID As Int64, ByVal nPatientID As Int64)

        Dim oDB As gloDatabaseLayer.DBLayer = Nothing


        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString)
            oDB.Connect(False)
            oDB.Execute_Query("Delete from Patient_CarePlan where nId=" & nID)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Delete, "Patient Care Plan Deleted", nPatientID, nID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
            End If
        End Try

        Return Nothing
    End Function

End Class

Public Class ClsCarePlanMST
    Implements IDisposable
    Private disposed As Boolean = False
    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        If disposed = False Then
            If disposing Then
                disposed = True
            End If
        End If
    End Sub

    Protected Overrides Sub Finalize()
        Dispose(False)
    End Sub

    
    Public Function GetCarePlanMST(ByVal nCarePlanID As Int64) As DataTable
        Dim dtTable As DataTable = Nothing

        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim oDBPara As gloDatabaseLayer.DBParameters = Nothing

        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString)
            oDBPara = New gloDatabaseLayer.DBParameters


            oDBPara.Add("@nID", nCarePlanID, ParameterDirection.Input, SqlDbType.BigInt)

            oDB.Connect(False)
            oDB.Retrive("Get_CarePlanMST", oDBPara, dtTable)

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

        Return dtTable

    End Function

    Public Function SaveCarePlanMST(ByVal nID As Int64, ByVal sProblem As String, ByVal sGoal As String, ByVal sNote As String, ByVal sInstruction As String) As Int64

        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim oDBPara As gloDatabaseLayer.DBParameters = Nothing

        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString)
            oDBPara = New gloDatabaseLayer.DBParameters
            oDBPara.Add("@nId", nID, ParameterDirection.InputOutput, SqlDbType.BigInt)
            oDBPara.Add("@sProblem", sProblem, ParameterDirection.Input, SqlDbType.VarChar)
            oDBPara.Add("@sGoal", sGoal, ParameterDirection.Input, SqlDbType.VarChar)
            oDBPara.Add("@sInternalNote", sNote, ParameterDirection.Input, SqlDbType.VarChar)
            oDBPara.Add("@sPatientInstruction", sInstruction, ParameterDirection.Input, SqlDbType.VarChar)
            oDB.Connect(False)
            oDB.Execute("INUP_CarePlan_MST", oDBPara, nID)
            oDB.Disconnect()
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
        Return nID
    End Function

    Public Function DeletePatientCarePlan(ByVal nID As Int64)

        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim oDBPara As gloDatabaseLayer.DBParameters = Nothing

        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString)
            oDBPara = New gloDatabaseLayer.DBParameters
            oDBPara.Add("@nId", nID, ParameterDirection.InputOutput, SqlDbType.BigInt)
            oDBPara.Add("@IsDelete", "true", ParameterDirection.Input, SqlDbType.Bit)
            oDB.Connect(False)
            oDB.Execute("INUP_CarePlan_MST", oDBPara, nID)
            oDB.Disconnect()
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
        Return nID

    End Function

End Class

Public Class ClsCarePlan_V2
    Implements IDisposable
    Private disposed As Boolean = False
    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        If disposed = False Then
            If disposing Then
                disposed = True
            End If
        End If
    End Sub

    Protected Overrides Sub Finalize()
        Dispose(False)
    End Sub

    Public Function GetPatientCarePlan_V2(ByVal nPatientID As Int64) As DataTable
        Dim dtTable As DataTable = Nothing

        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim oDBPara As gloDatabaseLayer.DBParameters = Nothing

        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString)
            oDBPara = New gloDatabaseLayer.DBParameters


            oDBPara.Add("@nPatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt)

            oDB.Connect(False)
            oDB.Retrive("CP_Get_Patient_CarePlan_V2", oDBPara, dtTable)

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oDBPara) Then
                oDBPara.Dispose()
                oDBPara = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Disconnect()
                oDB.Dispose()
            End If
        End Try

        Return dtTable

    End Function

    Public Function GetCarePlanDetails_V2(ByVal nCarePlanID As Int64) As DataSet
        Dim ds As DataSet = Nothing

        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim oDBPara As gloDatabaseLayer.DBParameters = Nothing

        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString)
            oDBPara = New gloDatabaseLayer.DBParameters


            oDBPara.Add("@nCarePlanId", nCarePlanID, ParameterDirection.Input, SqlDbType.BigInt)

            oDB.Connect(False)
            oDB.Retrive("CP_Get_Patient_CarePlanDetails_V2", oDBPara, ds)

            If Not ds Is Nothing Then
                ds.Tables(0).TableName = "PatientCarePlan"
                ds.Tables(1).TableName = "PatientHealthConcern"
                ds.Tables(2).TableName = "PatientGoals"
                ds.Tables(3).TableName = "PatientIntervention"
                ds.Tables(4).TableName = "PatientOutcome"
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oDBPara) Then
                oDBPara.Dispose()
                oDBPara = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Disconnect()
                oDB.Dispose()
            End If
        End Try

        Return ds

    End Function

    Public Function SaveCarePlan(ByVal nCarePlanId As Int64, ByVal nVisitId As Int64, ByVal nPatientId As Int64, ByVal sCarePlanName As String, ByVal sCarePlanReason As String, ByVal dtHealthConcernTVP As DataTable, ByVal dtHealthConcernAssociationTVP As DataTable, ByVal dtGoalTVP As DataTable, ByVal dtGoalAssociationTVP As DataTable, ByVal dtInterventionTVP As DataTable, ByVal dtInterventionAssociationTVP As DataTable, ByVal dtOutcomeTVP As DataTable, ByVal dtOutcomeAssociationTVP As DataTable) As Int64

        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim oDBPara As gloDatabaseLayer.DBParameters = Nothing
        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString)
            oDBPara = New gloDatabaseLayer.DBParameters
            oDBPara.Add("@nCarePlanId", nCarePlanId, ParameterDirection.InputOutput, SqlDbType.BigInt)
            oDBPara.Add("@nVisitId", nVisitId, ParameterDirection.Input, SqlDbType.BigInt)
            oDBPara.Add("@nPatientId", nPatientId, ParameterDirection.Input, SqlDbType.BigInt)
            oDBPara.Add("@sCarePlanName", sCarePlanName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBPara.Add("@sCarePlanReason", sCarePlanReason, ParameterDirection.Input, SqlDbType.VarChar)
            oDBPara.Add("@nProviderId", gnPatientProviderID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBPara.Add("@nUserId", gnLoginID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBPara.Add("@sUsername", gstrLoginName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBPara.Add("@HealthConcerns", dtHealthConcernTVP, ParameterDirection.Input, SqlDbType.Structured)
            oDBPara.Add("@HealthConcernAssociation", dtHealthConcernAssociationTVP, ParameterDirection.Input, SqlDbType.Structured)
            oDBPara.Add("@Goals", dtGoalTVP, ParameterDirection.Input, SqlDbType.Structured)
            oDBPara.Add("@GoalAssociation", dtGoalAssociationTVP, ParameterDirection.Input, SqlDbType.Structured)
            oDBPara.Add("@Interventions", dtInterventionTVP, ParameterDirection.Input, SqlDbType.Structured)
            oDBPara.Add("@InterventionAssociation", dtInterventionAssociationTVP, ParameterDirection.Input, SqlDbType.Structured)
            oDBPara.Add("@Outcomes", dtOutcomeTVP, ParameterDirection.Input, SqlDbType.Structured)
            oDBPara.Add("@OutcomeAssociation", dtOutcomeAssociationTVP, ParameterDirection.Input, SqlDbType.Structured)
            oDB.Connect(False)
            oDB.Execute("CP_InUpCarePlan", oDBPara, nCarePlanId)
            oDB.Disconnect()
        Catch ex As Exception
            'MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        Finally
            If Not IsNothing(oDBPara) Then
                oDBPara.Dispose()
                oDBPara = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
            End If

        End Try
        Return nCarePlanId
    End Function

    Public Function GetCarePlanHistory_V2(ByVal nID As Int64, ByVal sModule As String) As DataSet
        Dim ds As DataSet = Nothing

        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim oDBPara As gloDatabaseLayer.DBParameters = Nothing

        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString)
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
                oDB.Disconnect()
                oDB.Dispose()
            End If
        End Try

        Return ds

    End Function

    Public Shared Function GetUniqueIDs(IdCount As Integer) As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)
        Dim oParameters As gloDatabaseLayer.DBParameters = Nothing
        Dim _dtLineIds As DataTable = Nothing
        Try

            oParameters = New gloDatabaseLayer.DBParameters()
            oParameters.Add("@IDCount", IdCount, ParameterDirection.Input, SqlDbType.Int)
            oParameters.Add("@SingleRow", 1, ParameterDirection.Input, SqlDbType.Bit)
            oDB.Connect(False)
            oDB.Retrive("gsp_GetUniqueIDs", oParameters, _dtLineIds)
            oDB.Disconnect()

            'If _dtLineIds IsNot Nothing AndAlso _dtLineIds.Rows.Count > 0 Then
            '    _dtLineIds.Columns.Add("UniqueId")
            '    _dtLineIds.AcceptChanges()
            'End If
        Catch dbEx As gloDatabaseLayer.DBException
            dbEx.ERROR_Log(dbEx.ToString())
            Throw dbEx
        Finally
            If oParameters IsNot Nothing Then
                oParameters.Dispose()
            End If
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
        End Try

        Return _dtLineIds
    End Function

    Public Function canDelete(ByVal nID As Int64, ByVal sModule As String) As Boolean
        Dim bCanDelete As Boolean = False
        Dim dt As DataTable = Nothing

        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim oDBPara As gloDatabaseLayer.DBParameters = Nothing

        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString)
            oDBPara = New gloDatabaseLayer.DBParameters


            oDBPara.Add("@nID ", nID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBPara.Add("@ModuleFlag", sModule, ParameterDirection.Input, SqlDbType.VarChar)

            oDB.Connect(False)
            oDB.Retrive("CP_Validate", oDBPara, dt)

            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Dim sAssociationList As String = "You cannot delete current record because it is associated in following" + Environment.NewLine
                    For i As Int32 = 0 To dt.Rows.Count - 1
                        sAssociationList += Environment.NewLine + (i + 1).ToString + ". " + dt.Rows(i)("sType").ToString() + " - " + dt.Rows(i)("sName").ToString()
                    Next
                    MessageBox.Show(sAssociationList, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    bCanDelete = True
                End If
            Else
                bCanDelete = True
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oDBPara) Then
                oDBPara.Dispose()
                oDBPara = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Disconnect()
                oDB.Dispose()
            End If
        End Try

        Return bCanDelete

    End Function
End Class

Public Class ClsHealthConcern
    Implements IDisposable
    Private disposed As Boolean = False
    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        If disposed = False Then
            If disposing Then
                disposed = True
            End If
        End If
    End Sub

    Protected Overrides Sub Finalize()
        Dispose(False)
    End Sub

    Public nHealthConcernID As Int64
    Public nVisitId As Int64
    Public nPatientId As Int64
    Public sHealthConcernName As String
    Public sHealthConcernSnomedCode As String
    Public sHealthConcernSnomedDescription As String
    Public sHealthStatusDescription As String
    Public sHealthConcernAuthor As String
    Public sHealthConcernStatus As String
    Public dtHealthConcernStartDate As Date
    Public dtHealthConcernEndDate As Date
    Public sHealthConcernNotes As String
    Public dtHealthConcernDate As Date
    Public bIsStartDate As Boolean
    Public bIsEndDate As Boolean


    Public Function SaveHealthConcern(ByVal dtHealthConcernAssociationTVP As DataTable, ByVal RowState As String) As Int64

        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim oDBPara As gloDatabaseLayer.DBParameters = Nothing
        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString)
            oDBPara = New gloDatabaseLayer.DBParameters
            oDBPara.Add("@nHealthConcernID", nHealthConcernID, ParameterDirection.InputOutput, SqlDbType.BigInt)
            oDBPara.Add("@nVisitId", nVisitId, ParameterDirection.Input, SqlDbType.BigInt)
            oDBPara.Add("@nPatientId", nPatientId, ParameterDirection.Input, SqlDbType.BigInt)
            oDBPara.Add("@nProviderId", gnPatientProviderID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBPara.Add("@nUserId", gnLoginID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBPara.Add("@sUsername", gstrLoginName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBPara.Add("@sHealthConcernName", sHealthConcernName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBPara.Add("@sHealthConcernSnomedCode", sHealthConcernSnomedCode, ParameterDirection.Input, SqlDbType.VarChar)
            oDBPara.Add("@sHealthConcernSnomedDescription", sHealthConcernSnomedDescription, ParameterDirection.Input, SqlDbType.VarChar)
            oDBPara.Add("@sHealthStatusDescription", sHealthStatusDescription, ParameterDirection.Input, SqlDbType.VarChar)
            oDBPara.Add("@sHealthConcernAuthor", sHealthConcernAuthor, ParameterDirection.Input, SqlDbType.VarChar)
            oDBPara.Add("@sHealthConcernStatus", sHealthConcernStatus, ParameterDirection.Input, SqlDbType.VarChar)
            If bIsStartDate Then
                oDBPara.Add("@dtHealthConcernStartDate", dtHealthConcernStartDate, ParameterDirection.Input, SqlDbType.DateTime)
            End If
            If bIsEndDate Then
                oDBPara.Add("@dtHealthConcernEndDate", dtHealthConcernEndDate, ParameterDirection.Input, SqlDbType.DateTime)
            End If
            oDBPara.Add("@sHealthConcernNotes", sHealthConcernNotes, ParameterDirection.Input, SqlDbType.VarChar)
            oDBPara.Add("@dtHealthConcernDate", dtHealthConcernDate, ParameterDirection.Input, SqlDbType.Date)
            oDBPara.Add("@HealthConcernAssociation", dtHealthConcernAssociationTVP, ParameterDirection.Input, SqlDbType.Structured)
            oDBPara.Add("@RowState", RowState, ParameterDirection.Input, SqlDbType.VarChar)
            oDB.Connect(False)
            oDB.Execute("CP_InUpHealthConcern", oDBPara, nHealthConcernID)
            oDB.Disconnect()
        Catch ex As Exception
            'MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        Finally
            If Not IsNothing(oDBPara) Then
                oDBPara.Dispose()
                oDBPara = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
            End If

        End Try
        Return nHealthConcernID
    End Function

    Public Function GetPatientHealthConcern(ByVal nPatientID As Int64, Optional ByVal nHealthConcernID As Int64 = 0) As DataSet
        Dim dtSet As DataSet = Nothing

        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim oDBPara As gloDatabaseLayer.DBParameters = Nothing

        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString)
            oDBPara = New gloDatabaseLayer.DBParameters


            oDBPara.Add("@nPatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBPara.Add("@nHealthConcernID", nHealthConcernID, ParameterDirection.Input, SqlDbType.BigInt)

            oDB.Connect(False)
            oDB.Retrive("CP_Get_Patient_HCDetails", oDBPara, dtSet)

            If nHealthConcernID = 0 Then
                dtSet.Tables(0).TableName = "HealthConcernList"
            Else
                dtSet.Tables(0).TableName = "HealthConcernDetail"
                dtSet.Tables(1).TableName = "HealthConcernAssociation"
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oDBPara) Then
                oDBPara.Dispose()
                oDBPara = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Disconnect()
                oDB.Dispose()
            End If
        End Try

        Return dtSet

    End Function

    Public Function DeletePatientHealthConcern(ByVal nID As Int64, ByVal nPatientID As Int64)

        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim oDBPara As gloDatabaseLayer.DBParameters = Nothing

        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString)
            oDBPara = New gloDatabaseLayer.DBParameters
            oDBPara.Add("@nHealthConcernID", nID, ParameterDirection.InputOutput, SqlDbType.BigInt)
            oDBPara.Add("@HealthConcernAssociation", Nothing, ParameterDirection.Input, SqlDbType.Structured)
            oDBPara.Add("@RowState", "Deleted", ParameterDirection.Input, SqlDbType.VarChar)
            oDB.Connect(False)
            oDB.Execute("CP_InUpHealthConcern", oDBPara, nID)
            oDB.Disconnect()

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientHealthConcern, gloAuditTrail.ActivityType.Delete, "Patient Health Concern Deleted", nPatientID, nID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
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
        Return nID

    End Function
End Class


Public Class ClsGoal
    Implements IDisposable
    Private disposed As Boolean = False
    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        If disposed = False Then
            If disposing Then
                disposed = True
            End If
        End If
    End Sub

    Protected Overrides Sub Finalize()
        Dispose(False)
    End Sub

    Public nGoalID As Int64
    Public nVisitId As Int64
    Public nPatientId As Int64
    Public sGoalName As String
    Public sGoalLoincCode As String
    Public sGoalLoincDescription As String
    Public sGoalValue As String
    Public sGoalUnit As String
    Public sGoalAuthor As String
    Public sGoalNotes As String
    Public dtGoalDate As Date
    Public dtTargetDate As Date
    Public bIsGoalDate As Boolean
    Public bIsTargetDate As Boolean


    Public Function SaveGoal(ByVal dtGoalAssociationTVP As DataTable, ByVal RowState As String) As Int64

        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim oDBPara As gloDatabaseLayer.DBParameters = Nothing
        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString)
            oDBPara = New gloDatabaseLayer.DBParameters
            oDBPara.Add("@nGoalID", nGoalID, ParameterDirection.InputOutput, SqlDbType.BigInt)
            oDBPara.Add("@nVisitId", nVisitId, ParameterDirection.Input, SqlDbType.BigInt)
            oDBPara.Add("@nPatientId", nPatientId, ParameterDirection.Input, SqlDbType.BigInt)
            oDBPara.Add("@nProviderId", gnPatientProviderID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBPara.Add("@nUserId", gnLoginID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBPara.Add("@sUsername", gstrLoginName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBPara.Add("@sGoalName", sGoalName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBPara.Add("@sGoalLoincCode", sGoalLoincCode, ParameterDirection.Input, SqlDbType.VarChar)
            oDBPara.Add("@sGoalLoincDescription", sGoalLoincDescription, ParameterDirection.Input, SqlDbType.VarChar)
            oDBPara.Add("@sGoalValue", sGoalValue, ParameterDirection.Input, SqlDbType.VarChar)
            oDBPara.Add("@sGoalUnit", sGoalUnit, ParameterDirection.Input, SqlDbType.VarChar)
            oDBPara.Add("@sGoalAuthor", sGoalAuthor, ParameterDirection.Input, SqlDbType.VarChar)
            oDBPara.Add("@sGoalNotes", sGoalNotes, ParameterDirection.Input, SqlDbType.VarChar)
            If bIsGoalDate Then
                oDBPara.Add("@dtGoalDate", dtGoalDate, ParameterDirection.Input, SqlDbType.DateTime)
            End If
            If bIsTargetDate Then
                oDBPara.Add("@dtGoalTargetDate", dtTargetDate, ParameterDirection.Input, SqlDbType.DateTime)
            End If
            oDBPara.Add("@GoalAssociation", dtGoalAssociationTVP, ParameterDirection.Input, SqlDbType.Structured)
            oDBPara.Add("@RowState", RowState, ParameterDirection.Input, SqlDbType.VarChar)
            oDB.Connect(False)
            oDB.Execute("CP_InUpGoal", oDBPara, nGoalID)
            oDB.Disconnect()
        Catch ex As Exception
            'MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        Finally
            If Not IsNothing(oDBPara) Then
                oDBPara.Dispose()
                oDBPara = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
            End If

        End Try
        Return nGoalID
    End Function

    Public Function GetPatientGoal(ByVal nPatientID As Int64, Optional ByVal nGoalID As Int64 = 0) As DataSet
        Dim dtSet As DataSet = Nothing

        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim oDBPara As gloDatabaseLayer.DBParameters = Nothing

        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString)
            oDBPara = New gloDatabaseLayer.DBParameters


            oDBPara.Add("@nPatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBPara.Add("@nGoalID", nGoalID, ParameterDirection.Input, SqlDbType.BigInt)

            oDB.Connect(False)
            oDB.Retrive("CP_Get_Patient_GoalDetails", oDBPara, dtSet)

            If nGoalID = 0 Then
                dtSet.Tables(0).TableName = "GoalList"
            Else
                dtSet.Tables(0).TableName = "GoalDetail"
                dtSet.Tables(1).TableName = "GoalAssociation"
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oDBPara) Then
                oDBPara.Dispose()
                oDBPara = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Disconnect()
                oDB.Dispose()
            End If
        End Try

        Return dtSet

    End Function

    Public Function DeletePatientGoal(ByVal nID As Int64, ByVal nPatientId As Int64)

        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim oDBPara As gloDatabaseLayer.DBParameters = Nothing

        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString)
            oDBPara = New gloDatabaseLayer.DBParameters
            oDBPara.Add("@nGoalID", nID, ParameterDirection.InputOutput, SqlDbType.BigInt)
            oDBPara.Add("@GoalAssociation", Nothing, ParameterDirection.Input, SqlDbType.Structured)
            oDBPara.Add("@RowState", "Deleted", ParameterDirection.Input, SqlDbType.VarChar)
            oDB.Connect(False)
            oDB.Execute("CP_InUpGoal", oDBPara, nID)
            oDB.Disconnect()

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientGoal, gloAuditTrail.ActivityType.Delete, "Patient Goal Deleted", nPatientId, nID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
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
        Return nID

    End Function
End Class

Public Class ClsIntervention
    Implements IDisposable
    Private disposed As Boolean = False
    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        If disposed = False Then
            If disposing Then
                disposed = True
            End If
        End If
    End Sub

    Protected Overrides Sub Finalize()
        Dispose(False)
    End Sub

    Public nInterventionID As Int64
    Public nVisitId As Int64
    Public nPatientId As Int64
    Public sInterventionName As String
    Public sInterventionType As String
    Public nPOTID As Int64
    Public sInterventionNotes As String
    Public dtInterventionDate As Date
    Public bIsDate As Boolean

    Public Function SaveIntervention(ByVal dtInterventionAssociationTVP As DataTable, ByVal RowState As String) As Int64

        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim oDBPara As gloDatabaseLayer.DBParameters = Nothing
        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString)
            oDBPara = New gloDatabaseLayer.DBParameters
            oDBPara.Add("@nInterventionID", nInterventionID, ParameterDirection.InputOutput, SqlDbType.BigInt)
            oDBPara.Add("@nVisitId", nVisitId, ParameterDirection.Input, SqlDbType.BigInt)
            oDBPara.Add("@nPatientId", nPatientId, ParameterDirection.Input, SqlDbType.BigInt)
            oDBPara.Add("@nProviderId", gnPatientProviderID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBPara.Add("@nUserId", gnLoginID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBPara.Add("@sUsername", gstrLoginName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBPara.Add("@sInterventionName", sInterventionName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBPara.Add("@sInterventionNotes", sInterventionNotes, ParameterDirection.Input, SqlDbType.VarChar)
            oDBPara.Add("@sInterventionType", sInterventionType, ParameterDirection.Input, SqlDbType.VarChar)
            oDBPara.Add("@nPlanOfTreatmentID", nPOTID, ParameterDirection.Input, SqlDbType.BigInt)
            If bIsDate Then
                oDBPara.Add("@dtInterventionDate", dtInterventionDate, ParameterDirection.Input, SqlDbType.DateTime)
            End If
            oDBPara.Add("@InterventionAssociation", dtInterventionAssociationTVP, ParameterDirection.Input, SqlDbType.Structured)
            oDBPara.Add("@RowState", RowState, ParameterDirection.Input, SqlDbType.VarChar)
            oDB.Connect(False)
            oDB.Execute("CP_InUpIntervention", oDBPara, nInterventionID)
            oDB.Disconnect()
        Catch ex As Exception
            'MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        Finally
            If Not IsNothing(oDBPara) Then
                oDBPara.Dispose()
                oDBPara = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
            End If

        End Try
        Return nInterventionID
    End Function

    Public Function GetPatientIntervention(ByVal nPatientID As Int64, Optional ByVal nInterventionID As Int64 = 0) As DataSet
        Dim dtSet As DataSet = Nothing

        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim oDBPara As gloDatabaseLayer.DBParameters = Nothing

        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString)
            oDBPara = New gloDatabaseLayer.DBParameters


            oDBPara.Add("@nPatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBPara.Add("@nInterventionID", nInterventionID, ParameterDirection.Input, SqlDbType.BigInt)

            oDB.Connect(False)
            oDB.Retrive("CP_Get_Patient_InterventionDetails", oDBPara, dtSet)

            If nInterventionID = 0 Then
                dtSet.Tables(0).TableName = "InterventionList"
            Else
                dtSet.Tables(0).TableName = "InterventionDetail"
                dtSet.Tables(1).TableName = "InterventionAssociation"
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oDBPara) Then
                oDBPara.Dispose()
                oDBPara = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Disconnect()
                oDB.Dispose()
            End If
        End Try

        Return dtSet

    End Function

    Public Function DeletePatientIntervention(ByVal nID As Int64, ByVal nPatientID As Int64)

        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim oDBPara As gloDatabaseLayer.DBParameters = Nothing

        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString)
            oDBPara = New gloDatabaseLayer.DBParameters
            oDBPara.Add("@nInterventionID", nID, ParameterDirection.InputOutput, SqlDbType.BigInt)
            oDBPara.Add("@InterventionAssociation", Nothing, ParameterDirection.Input, SqlDbType.Structured)
            oDBPara.Add("@RowState", "Deleted", ParameterDirection.Input, SqlDbType.VarChar)
            oDB.Connect(False)
            oDB.Execute("CP_InUpIntervention", oDBPara, nID)
            oDB.Disconnect()

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientIntervention, gloAuditTrail.ActivityType.Delete, "Patient Intervention Deleted", nPatientId, nID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
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
        Return nID

    End Function
End Class

Public Class ClsOutcome
    Implements IDisposable
    Private disposed As Boolean = False
    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        If disposed = False Then
            If disposing Then
                disposed = True
            End If
        End If
    End Sub

    Protected Overrides Sub Finalize()
        Dispose(False)
    End Sub

    Public nOutcomeID As Int64
    Public nVisitId As Int64
    Public nPatientId As Int64
    Public sOutcomeName As String
    Public sOutcomeStatus As String
    Public sOutcomeNotes As String
    Public dtOutcomeDate As Date
    Public bIsDate As Boolean
    Public sOutcomeValue As String
    Public sOutcomeUnit As String

    Public Function SaveOutcome(ByVal dtOutcomeAssociationTVP As DataTable, ByVal RowState As String) As Int64

        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim oDBPara As gloDatabaseLayer.DBParameters = Nothing
        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString)
            oDBPara = New gloDatabaseLayer.DBParameters
            oDBPara.Add("@nOutcomeID", nOutcomeID, ParameterDirection.InputOutput, SqlDbType.BigInt)
            oDBPara.Add("@nVisitId", nVisitId, ParameterDirection.Input, SqlDbType.BigInt)
            oDBPara.Add("@nPatientId", nPatientId, ParameterDirection.Input, SqlDbType.BigInt)
            oDBPara.Add("@nProviderId", gnPatientProviderID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBPara.Add("@nUserId", gnLoginID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBPara.Add("@sUsername", gstrLoginName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBPara.Add("@sOutcomeName", sOutcomeName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBPara.Add("@sOutcomeStatus", sOutcomeStatus, ParameterDirection.Input, SqlDbType.VarChar)
            oDBPara.Add("@sOutcomeNotes", sOutcomeNotes, ParameterDirection.Input, SqlDbType.VarChar)
            If bIsDate Then
                oDBPara.Add("@dtOutcomeDate", dtOutcomeDate, ParameterDirection.Input, SqlDbType.DateTime)
            End If
            oDBPara.Add("@sValue", sOutcomeValue, ParameterDirection.Input, SqlDbType.VarChar)
            oDBPara.Add("@sUnit", sOutcomeUnit, ParameterDirection.Input, SqlDbType.VarChar)
            oDBPara.Add("@OutcomeAssociation", dtOutcomeAssociationTVP, ParameterDirection.Input, SqlDbType.Structured)
            oDBPara.Add("@RowState", RowState, ParameterDirection.Input, SqlDbType.VarChar)
            oDB.Connect(False)
            oDB.Execute("CP_InUpOutcome", oDBPara, nOutcomeID)
            oDB.Disconnect()
        Catch ex As Exception
            'MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        Finally
            If Not IsNothing(oDBPara) Then
                oDBPara.Dispose()
                oDBPara = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
            End If

        End Try
        Return nOutcomeID
    End Function

    Public Function GetPatientOutcome(ByVal nPatientID As Int64, Optional ByVal nOutcomeID As Int64 = 0) As DataSet
        Dim dtSet As DataSet = Nothing

        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim oDBPara As gloDatabaseLayer.DBParameters = Nothing

        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString)
            oDBPara = New gloDatabaseLayer.DBParameters


            oDBPara.Add("@nPatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBPara.Add("@nOutcomeID", nOutcomeID, ParameterDirection.Input, SqlDbType.BigInt)

            oDB.Connect(False)
            oDB.Retrive("CP_Get_Patient_OutcomeDetails", oDBPara, dtSet)

            If nOutcomeID = 0 Then
                dtSet.Tables(0).TableName = "OutcomeList"
            Else
                dtSet.Tables(0).TableName = "OutcomeDetail"
                dtSet.Tables(1).TableName = "OutcomeAssociation"
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oDBPara) Then
                oDBPara.Dispose()
                oDBPara = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Disconnect()
                oDB.Dispose()
            End If
        End Try

        Return dtSet

    End Function

    Public Function DeletePatientOutcome(ByVal nID As Int64, ByVal nPatientID As Int64)

        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim oDBPara As gloDatabaseLayer.DBParameters = Nothing

        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString)
            oDBPara = New gloDatabaseLayer.DBParameters
            oDBPara.Add("nOutcomeID", nID, ParameterDirection.InputOutput, SqlDbType.BigInt)
            oDBPara.Add("@OutcomeAssociation", Nothing, ParameterDirection.Input, SqlDbType.Structured)
            oDBPara.Add("@RowState", "Deleted", ParameterDirection.Input, SqlDbType.VarChar)
            oDB.Connect(False)
            oDB.Execute("CP_InUpOutcome", oDBPara, nID)
            oDB.Disconnect()

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientOutcome, gloAuditTrail.ActivityType.Delete, "Patient Outcome Deleted", nPAtientID, nID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
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
        Return nID

    End Function
End Class