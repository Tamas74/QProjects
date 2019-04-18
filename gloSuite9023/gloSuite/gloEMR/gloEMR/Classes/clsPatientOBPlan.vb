Imports System.Data.SqlClient


Public Class clsPatientOBPlan
  
    Private dv As DataView = Nothing
    Private _TempOBPlanID As Long = 0

    Private sConnectionString As String = Nothing
    Private oDBLayer As gloDatabaseLayer.DBLayer = Nothing
    Private oDBParameters As gloDatabaseLayer.DBParameters = Nothing

    Public Sub Dispose()

        If Not IsNothing(dv) Then
            dv.Dispose()
            dv = Nothing
        End If

        If Not IsNothing(oDBParameters) Then
            oDBParameters.Clear()
            oDBParameters.Dispose()
            oDBParameters = Nothing
        End If

        If Not IsNothing(oDBLayer) Then
            oDBLayer.Dispose()
            oDBLayer = Nothing
        End If

        sConnectionString = Nothing

    End Sub

    Public Sub New()
        Try
            sConnectionString = GetConnectionString()

        Catch ex As Exception   ' Catch the error.
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    Public ReadOnly Property GetDataview() As DataView
        Get
            'Dv = Ds.Tables("Category_Mst").DefaultView
            Return dv

        End Get
    End Property

    Public Function Search(ByVal dv As DataView, ByVal colIndex As Integer, ByVal txtSearch As String) As DataView

        If (IsNothing(dv) = False) Then
            dv.RowFilter = "" & dv.Table.Columns(colIndex).ColumnName & " Like '" & txtSearch & "%'"
        End If

        Return Nothing
    End Function

    Public Sub SetRowFilter(ByVal txtSearch As String)
        Dim strexpr As String
        Dim str As String
        If (IsNothing(dv) = False) Then
            str = dv.Sort
            str = Mid(str, 2)
            str = Mid(str, 1, Len(str) - 1)
            strexpr = "" & dv.Table.Columns(str).ColumnName() & "  Like '" & txtSearch & "%'"
            dv.RowFilter = strexpr
        End If

    End Sub

    Public Sub SortDataview(ByVal strsort As String)
        If (IsNothing(dv) = False) Then
            dv.Sort = "[" & strsort & "]"
        End If

    End Sub

    Public Function getFamilyMember() As DataTable
        Dim dt As DataTable = Nothing

        Try
            dt = New DataTable

            oDBLayer = New gloDatabaseLayer.DBLayer(sConnectionString)
            oDBParameters = New gloDatabaseLayer.DBParameters
            oDBParameters.Clear()

            oDBParameters.Add("@nMemberID", 0, ParameterDirection.Input, SqlDbType.BigInt)

            oDBLayer.Connect(False)
            oDBLayer.Retrive("gsp_viewFamilyMember_MST", oDBParameters, dt)
            oDBLayer.Disconnect()

        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
            Return Nothing
        Finally

            If Not IsNothing(oDBParameters) Then
                oDBParameters.Clear()
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If

            If Not IsNothing(oDBLayer) Then
                oDBLayer.Dispose()
                oDBLayer = Nothing
            End If
        End Try

        Return dt
    End Function

    Public Function GetAllCategory(ByVal CategoryType As String) As DataTable
        Dim dt As DataTable = Nothing

        Try
            dt = New DataTable

            oDBLayer = New gloDatabaseLayer.DBLayer(sConnectionString)
            oDBParameters = New gloDatabaseLayer.DBParameters
            oDBParameters.Clear()

            oDBParameters.Add("@CategoryType", CategoryType, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@flag", 1, ParameterDirection.Input, SqlDbType.Int)

            oDBLayer.Connect(False)
            oDBLayer.Retrive("gsp_FillCategory_Mst", oDBParameters, dt)
            oDBLayer.Disconnect()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(oDBParameters) Then
                oDBParameters.Clear()
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If

            If Not IsNothing(oDBLayer) Then
                oDBLayer.Dispose()
                oDBLayer = Nothing
            End If
        End Try

        Return dt
    End Function

    Public Function GetOBPlan(ByVal PatientID As Long, ByVal intflag As Int16, ByVal VisitDate As Date, Optional ByVal intvisitId As Long = 0) As DataTable
        Dim dt As DataTable = Nothing

        Try
            'intflag = 0 --check if current history exists  
            'intflag = 1 --check if history record before current date in history table        
            'intflag = 2 --if history record before current date in history table fetch details of that transaction     

            dt = New DataTable

            oDBLayer = New gloDatabaseLayer.DBLayer(sConnectionString)
            oDBParameters = New gloDatabaseLayer.DBParameters
            oDBParameters.Clear()

            oDBParameters.Add("@dtVisitdate", VisitDate, ParameterDirection.Input, SqlDbType.DateTime)
            oDBParameters.Add("@PatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@flag", intflag, ParameterDirection.Input, SqlDbType.Int)
            If intflag = 2 Then
                oDBParameters.Add("@VisitId", intvisitId, ParameterDirection.Input, SqlDbType.BigInt)
            End If

            oDBLayer.Connect(False)
            oDBLayer.Retrive("gsp_GetOBPlan", oDBParameters, dt)
            oDBLayer.Disconnect()

        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
            Return Nothing
        Finally

            If Not IsNothing(oDBParameters) Then
                oDBParameters.Clear()
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If

            If Not IsNothing(oDBLayer) Then
                oDBLayer.Dispose()
                oDBLayer = Nothing
            End If
        End Try

        Return dt
    End Function

    Public Function GetPatName(ByVal PatientID As Long) As DataTable
        Dim dt As DataTable = Nothing

        Try
            dt = New DataTable

            oDBLayer = New gloDatabaseLayer.DBLayer(sConnectionString)
            oDBParameters = New gloDatabaseLayer.DBParameters
            oDBParameters.Clear()

            oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt)

            oDBLayer.Connect(False)
            oDBLayer.Retrive("GetPatNameWithoutExtraSpace", oDBParameters, dt)
            oDBLayer.Disconnect()

        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
            Return Nothing
        Finally

            If Not IsNothing(oDBParameters) Then
                oDBParameters.Clear()
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If

            If Not IsNothing(oDBLayer) Then
                oDBLayer.Dispose()
                oDBLayer = Nothing
            End If
        End Try

        Return dt
    End Function

    Public Function GetPatientOBPlan_Optimize(ByVal PatientID As Long, ByVal intflag As Int16, ByVal VisitDate As Date, ByVal CategoryType As String, ByVal Flag As Int16, Optional ByVal intvisitId As Long = 0) As DataSet
        Dim dsOBPlan As DataSet = Nothing

        Try
            'intflag = 0 --check if current history exists  
            'intflag = 1 --check if history record before current date in history table        
            'intflag = 2 --if history record before current date in history table fetch details of that transaction        

            dsOBPlan = New DataSet()

            oDBLayer = New gloDatabaseLayer.DBLayer(sConnectionString)
            oDBParameters = New gloDatabaseLayer.DBParameters
            oDBParameters.Clear()

            oDBParameters.Add("@dtVisitdate", VisitDate, ParameterDirection.Input, SqlDbType.DateTime)
            oDBParameters.Add("@PatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@flag1", intflag, ParameterDirection.Input, SqlDbType.Int)
            oDBParameters.Add("@CategoryType", CategoryType, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@flag", 1, ParameterDirection.Input, SqlDbType.Int)
            If intflag = 2 Or Flag = 1 Then
                oDBParameters.Add("@VisitId", intvisitId, ParameterDirection.Input, SqlDbType.BigInt)
            End If

            oDBLayer.Connect(False)
            oDBLayer.Retrive("gsp_GetOBPlanData", oDBParameters, dsOBPlan)
            oDBLayer.Disconnect()

            dsOBPlan.Tables(0).TableName = "Category"
            dsOBPlan.Tables(1).TableName = "OBPlan"
            dsOBPlan.Tables(2).TableName = "Patient"

        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
            Return Nothing
        Finally

            If Not IsNothing(oDBParameters) Then
                oDBParameters.Clear()
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If

            If Not IsNothing(oDBLayer) Then
                oDBLayer.Dispose()
                oDBLayer = Nothing
            End If
        End Try

        Return dsOBPlan
    End Function

    Public Function GetAllOBPlan(ByVal strGroup As String) As DataTable
        Dim dt As DataTable = Nothing
        Try
            dt = New DataTable

            oDBLayer = New gloDatabaseLayer.DBLayer(sConnectionString)
            oDBParameters = New gloDatabaseLayer.DBParameters
            oDBParameters.Clear()

            oDBParameters.Add("@strGroup", strGroup, ParameterDirection.Input, SqlDbType.VarChar)

            oDBLayer.Connect(False)
            oDBLayer.Retrive("gsp_FillOBPlan", oDBParameters, dt)
            oDBLayer.Disconnect()

        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
            Return Nothing
        Finally

            If Not IsNothing(oDBParameters) Then
                oDBParameters.Clear()
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If

            If Not IsNothing(oDBLayer) Then
                oDBLayer.Dispose()
                oDBLayer = Nothing
            End If
        End Try

        Return dt
    End Function

    Public Function AddNewOBPlanDataset(ByVal PatientID As Int64, ByVal IsModify As Boolean, ByVal dsOBPlan As DataSet, ByVal ParameterName As String) As Boolean
        
        Try
            oDBLayer = New gloDatabaseLayer.DBLayer(sConnectionString)
            oDBParameters = New gloDatabaseLayer.DBParameters
            oDBParameters.Clear()

            oDBParameters.Add(ParameterName, dsOBPlan.Tables("OBPlan"), ParameterDirection.Input, SqlDbType.Structured)

            oDBLayer.Connect(False)
            oDBLayer.Execute("gsp_InUpOBPlan", oDBParameters)
            oDBLayer.Disconnect()

            If IsModify = True Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, "Patient OB Plan Modified", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, True)
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "Patient OB Plan Added", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, True)
            End If

            Return True
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
            Return Nothing
        Finally

            If Not IsNothing(oDBParameters) Then
                oDBParameters.Clear()
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If

            If Not IsNothing(oDBLayer) Then
                oDBLayer.Dispose()
                oDBLayer = Nothing
            End If
        End Try

    End Function

    Public Sub DeleteOBPlan(ByVal VisitID As Long, ByVal PatientID As Long)
        
        Try
            oDBLayer = New gloDatabaseLayer.DBLayer(sConnectionString)
            oDBParameters = New gloDatabaseLayer.DBParameters
            oDBParameters.Clear()

            oDBParameters.Add("@VisitID", VisitID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@PatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt)

            oDBLayer.Connect(False)
            oDBLayer.Execute("gsp_DeleteOBPlan", oDBParameters)
            oDBLayer.Disconnect()

        Catch ex As SqlException

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        Finally

            If Not IsNothing(oDBParameters) Then
                oDBParameters.Clear()
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If

            If Not IsNothing(oDBLayer) Then
                oDBLayer.Dispose()
                oDBLayer = Nothing
            End If
        End Try

    End Sub

    Public Function FillDetailsFromMaster(ByVal ConceptID As String, ByVal Description As String) As DataTable
        Dim dt As DataTable = Nothing

        Try
            dt = New DataTable

            oDBLayer = New gloDatabaseLayer.DBLayer(sConnectionString)
            oDBParameters = New gloDatabaseLayer.DBParameters
            oDBParameters.Clear()

            oDBParameters.Add("@ConceptID", ConceptID, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@Description", Description, ParameterDirection.Input, SqlDbType.VarChar)

            oDBLayer.Connect(False)
            oDBLayer.Retrive("History_FillDetailsFromMst", oDBParameters, dt)
            oDBLayer.Disconnect()

        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
            Return Nothing
        Finally

            If Not IsNothing(oDBParameters) Then
                oDBParameters.Clear()
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If

            If Not IsNothing(oDBLayer) Then
                oDBLayer.Dispose()
                oDBLayer = Nothing
            End If
        End Try

        Return dt
    End Function

    Public Function GetReviewHistory(ByVal m_PatientID As Int64, ByVal visitid As Int64) As DataTable
        Dim sSQL As String = Nothing
        Dim dtReview As DataTable = Nothing

        Try
            dtReview = New DataTable

            sSQL = "Select sComments,dtReviewDate from ReviewHistory where nPatientID = " & m_PatientID & " AND nVisitID = " & visitid & " Order By dtReviewDate DESC"

            oDBLayer = New gloDatabaseLayer.DBLayer(sConnectionString)

            oDBLayer.Connect(False)
            oDBLayer.Retrive_Query(sSQL, dtReview)
            oDBLayer.Disconnect()

            Return dtReview
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
            Return Nothing
        Finally

            sSQL = Nothing

            If Not IsNothing(oDBLayer) Then
                oDBLayer.Dispose()
                oDBLayer = Nothing
            End If
        End Try
    End Function

    Public Function Fill_LockPatientOBPlan(ByVal MachinName As String, ByVal TransactionType As Integer) As DataTable
        Dim dt As DataTable = Nothing

        Try
            dt = New DataTable

            oDBLayer = New gloDatabaseLayer.DBLayer(sConnectionString)
            oDBParameters = New gloDatabaseLayer.DBParameters
            oDBParameters.Clear()

            oDBParameters.Add("@sMachinName", MachinName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@nTrnType", TransactionType, ParameterDirection.Input, SqlDbType.Int)
            oDBParameters.Add("@nMachinID", 0, ParameterDirection.Input, SqlDbType.BigInt)

            oDBLayer.Connect(False)
            oDBLayer.Retrive("gsp_Select_UnLock_Record", oDBParameters, dt)
            oDBLayer.Disconnect()

            Return dt
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
            Return Nothing
        Finally

            If Not IsNothing(oDBParameters) Then
                oDBParameters.Clear()
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If

            If Not IsNothing(oDBLayer) Then
                oDBLayer.Dispose()
                oDBLayer = Nothing
            End If
        End Try
    End Function

    Public Function Fill_ICD9Code(ByVal ICD9 As String, ByVal CPT As String)
        Dim ds As DataSet = Nothing
        
        Try
            ds = New DataSet

            oDBLayer = New gloDatabaseLayer.DBLayer(sConnectionString)
            oDBParameters = New gloDatabaseLayer.DBParameters
            oDBParameters.Clear()

            oDBParameters.Add("@ICD9", ICD9, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@CPT", CPT, ParameterDirection.Input, SqlDbType.VarChar)

            oDBLayer.Connect(False)
            oDBLayer.Retrive("History_FillICD9Code", oDBParameters, ds)
            oDBLayer.Disconnect()

            ds.Tables(0).TableName = "ICD9"
            ds.Tables(1).TableName = "CPT"

            Return ds
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
            Return Nothing
        Finally

            If Not IsNothing(oDBParameters) Then
                oDBParameters.Clear()
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If

            If Not IsNothing(oDBLayer) Then
                oDBLayer.Dispose()
                oDBLayer = Nothing
            End If
        End Try
        Return ds
    End Function

End Class


