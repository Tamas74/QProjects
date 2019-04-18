#Region "Comment"
'Created By  : Shubhangi Gujar
'Created Date: 20100908
'Purpose     : View Form of MUDashboard.
#End Region

Imports gloEMR.gloEMRWord
Imports gloEMRGeneralLibrary.gloEMRDatabase
Public Class cls_MU_Measures

    Private dv As DataView = Nothing
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

    Public Function FillMUMeasures()
        Dim oDB As New DataBaseLayer
        'Dim oParamater As DBParameter

        '  Dim oResultTable As New DataTable
        Try

            'oParamater = New DBParameter
            'oParamater.DataType = SqlDbType.Int
            'oParamater.Direction = ParameterDirection.Input
            'oParamater.Name = "@flag"
            'oParamater.Value = 19 '' to Fill NurseNotes Templates
            'oDB.DBParametersCol.Add(oParamater)
            'oParamater = Nothing
            Dim dt As DataTable
            dt = oDB.GetDataTable("MU_FillMeasures_MST")

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
            oDB.Dispose()
        End Try

    End Function


    Public Function DeleteMeasures(ByVal nReportID As Long)
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nReportID"
            oParamater.Value = nReportID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oDB.Delete("MU_DeleteMeasures")

            ' gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Delete, "'" & NotesHeader & "' Nurses Notes Deleted on Dated '" & Notesdate & "'", gloAuditTrail.ActivityOutCome.Success)
            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.Delete, "'" & NotesHeader & "' Nurses Notes Deleted on Dated '" & Notesdate & "'", gstrLoginName, gstrClientMachineName, gnPatientID)
            'objAudit = Nothing

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDB.Dispose()
        End Try
        Return Nothing
    End Function

End Class
