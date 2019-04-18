Imports System.Linq
Imports System.Data
Imports System.Collections.Generic
Imports System.Data.Linq


Public Class clsICD10Settings
    Public oDataContext As DataClassICD10DataContext = New DataClassICD10DataContext(mdlGeneral.gstrConnectionString)

    Private disposed As Boolean = False

    Public Sub Dispose()
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposed Then
            If disposing Then
            End If
        End If
        disposed = True
    End Sub

    Protected Overrides Sub Finalize()
        Try
            Dispose(False)
        Finally
            MyBase.Finalize()
        End Try
    End Sub

    Public Function FetchPlanLevelICDDOS(Optional ByVal _OnlyOverride As Boolean = False) As IEnumerable
        Dim _List As IEnumerable = Nothing
        Try
            If _OnlyOverride = True Then
                _List = (From c In oDataContext.Contacts_MSTs
                       Group Join b In oDataContext.BL_ICD10TransitionSettings
                       On c.nContactID Equals b.nContactID Into Group From d In Group.DefaultIfEmpty()
                       Where (c.sContactType Is "Insurance") And d.nContactID <> 0 And d.dtDOSDate IsNot Nothing
                       Order By c.sName
                       Select New With {c.nContactID, c.sName, d.dtDOSDate})
            Else

                _List = (From c In oDataContext.Contacts_MSTs
                                        Group Join b In oDataContext.BL_ICD10TransitionSettings
                                        On c.nContactID Equals b.nContactID Into Group From d In Group.DefaultIfEmpty()
                                        Where (c.sContactType Is "Insurance") And If(d IsNot Nothing, d.nContactID <> 0, True)
                                        Order By c.sName
                                        Select New With {c.nContactID, c.sName, d.dtDOSDate})
            End If

        Catch ex As Exception
            Throw ex
        Finally
        End Try
        Return _List
    End Function

    Public Function FetchICDDOS() As String
        Dim _Result As String = ""
        Try
            Dim _List = (From b In oDataContext.BL_ICD10TransitionSettings
                         Where b.nContactID = 0
                         Select b.dtDOSDate).FirstOrDefault()

            If Not IsNothing(_List) Then
                _Result = _List.ToString()
            End If
            _List = Nothing
        Catch ex As Exception
            Throw ex
        Finally
        End Try
        Return _Result
    End Function

    Public Function SaveData(ByVal oList As BL_ICD10TransitionSetting)
        Try

            Dim _List As BL_ICD10TransitionSetting = (From b In oDataContext.BL_ICD10TransitionSettings
                         Where b.nContactID = 0
                         Select b).FirstOrDefault()
            If _List IsNot Nothing Then
                _List.dtDOSDate = oList.dtDOSDate
            Else
                oDataContext.BL_ICD10TransitionSettings.InsertOnSubmit(oList)
            End If
            oDataContext.SubmitChanges()
            oDataContext.Refresh(Linq.RefreshMode.OverwriteCurrentValues)
        Catch ex As Exception
            Throw ex
        Finally
        End Try
    End Function


    Public Function getChangeDataforAuditing()
        Dim _List As IEnumerable(Of BL_ICD10TransitionSetting) = Nothing
        Try
            _List = oDataContext.GetChangeSet.Inserts.OfType(Of BL_ICD10TransitionSetting)()
            If Not IsNothing(_List) Then
                WriteAuditLog(_List, clsAudit.enmActivityType.ICD10DOSInsert)
            End If
            _List = Nothing
            _List = oDataContext.GetChangeSet.Updates.OfType(Of BL_ICD10TransitionSetting)()
            If Not IsNothing(_List) Then
                WriteAuditLog(_List, clsAudit.enmActivityType.ICD10DOSUpdate)
            End If
        Catch ex As Exception
            Return Nothing
        Finally
            _List = Nothing
        End Try
    End Function

    Private Sub WriteAuditLog(ByVal _List As IEnumerable(Of BL_ICD10TransitionSetting), ByVal Type As clsAudit.enmActivityType)
        Dim objAudit As New clsAudit
        Dim FieldName, OldValue, NewValue, Message, PlanName As String
        Try
            If Type = clsAudit.enmActivityType.ICD10DOSUpdate Then
                If Not _List Is Nothing AndAlso _List.Count > 0 Then
                    For Each obj As BL_ICD10TransitionSetting In _List
                        Dim mi As ModifiedMemberInfo() = oDataContext.BL_ICD10TransitionSettings.GetModifiedMembers(obj)
                        If Not mi Is Nothing AndAlso mi.Count > 0 Then
                            For Each member As ModifiedMemberInfo In mi
                                FieldName = member.Member.Name
                                If Not FieldName Is Nothing AndAlso FieldName = "dtDOSDate" Then
                                    OldValue = If(member.OriginalValue IsNot Nothing, member.OriginalValue, "Blank")
                                    NewValue = If(member.CurrentValue IsNot Nothing, member.CurrentValue, "Blank")
                                    Message = "ICD10 DOS changed from " & OldValue & " to " & NewValue
                                    If obj.nContactID <> 0 Then
                                        PlanName = (From Result In oDataContext.Contacts_MSTs
                                        Where (Result.nContactID = obj.nContactID)
                                        Select Result.sName).FirstOrDefault()
                                        If Not PlanName Is Nothing AndAlso PlanName <> "" Then
                                            Message = Message & " for " & PlanName
                                        End If
                                    End If
                                    objAudit.CreateLog(Type, Message, gstrLoginName, gstrClientMachineName, 0, , clsAudit.enmOutcome.Success)
                                End If
                            Next
                        End If
                        mi = Nothing
                    Next
                End If
            ElseIf Type = clsAudit.enmActivityType.ICD10DOSInsert Then
                If Not _List Is Nothing AndAlso _List.Count > 0 Then
                    For Each obj As BL_ICD10TransitionSetting In _List
                        NewValue = If(obj.dtDOSDate IsNot Nothing, obj.dtDOSDate, "Blank")
                        Message = "ICD10 DOS changed from Blank to " & NewValue
                        If obj.nContactID <> 0 Then
                            PlanName = (From Result In oDataContext.Contacts_MSTs
                            Where (Result.nContactID = obj.nContactID)
                            Select Result.sName).FirstOrDefault()
                            If Not PlanName Is Nothing AndAlso PlanName <> "" Then
                                Message = Message & " for " & PlanName
                            End If
                        End If
                        objAudit.CreateLog(Type, Message, gstrLoginName, gstrClientMachineName, 0, , clsAudit.enmOutcome.Success)
                    Next
                End If
            End If

        Catch ex As Exception
        Finally
            objAudit = Nothing
            FieldName = String.Empty
            OldValue = String.Empty
            NewValue = String.Empty
            Message = String.Empty
            PlanName = String.Empty
        End Try

    End Sub

End Class
