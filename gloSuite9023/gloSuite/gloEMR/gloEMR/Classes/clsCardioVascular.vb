Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports gloEMRGeneralLibrary.gloEMRDatabase

Namespace gloStream

    Namespace CardioVascular

        Public Class CardioVascular

            Public Sub New()
                MyBase.new()
            End Sub

            Protected Overrides Sub Finalize()
                MyBase.Finalize()
            End Sub

            Private Function GetFtInch(ByVal strHeight As String) As Array
                'Dim arrHeight() As String
                strHeight = Mid(strHeight, 1, Len(strHeight) - 1)
                'arrHeight = 
                Return Split(strHeight, "'", , CompareMethod.Text)

                'Return arrHeight
            End Function

            Private Function FtToMtr(ByVal Ft As Decimal, ByVal Inch As Decimal) As Decimal
                Return (Ft * 30.48 + Inch * 2.54) / 100
                ''   1 ft = 30.48 cm
                ''   1 inch = 2.54 cm
            End Function

            Private Function FtToMtr(ByVal FtInchString As String) As Decimal
                Dim str() As String
                Dim Ft As Decimal
                Dim Inch As Decimal

                str = GetFtInch(FtInchString)
                If Not IsNothing(str) Then
                    If Not IsNothing(str(0)) AndAlso str(0) <> "" Then
                        Ft = CType(str(0), Decimal)
                    End If
                    If Not IsNothing(str(1)) AndAlso str(1) <> "" Then
                        Inch = CType(str(1), Decimal)
                    End If
                    Return FtToMtr(Ft, Inch)
                Else
                    Return 0.0
                End If
            End Function

            Private Function IsOtherDetailSame(ByVal PatientDetail As Supporting.OtherDetail, ByVal CriteriaDetail As Supporting.OtherDetail) As Boolean
                If PatientDetail.DetailType <> CriteriaDetail.DetailType Then
                    Return False
                End If
                If PatientDetail.ItemName.ToLower.Trim <> CriteriaDetail.ItemName.ToLower.Trim Then
                    Return False
                End If
                If PatientDetail.CategoryName.ToLower.Trim <> CriteriaDetail.CategoryName.ToLower.Trim Then
                    Return False
                End If
                If PatientDetail.OperatorName = CriteriaDetail.OperatorName AndAlso CriteriaDetail.DetailType = Supporting.enumDetailType.Order Then '' ONLY FOR ORDER ''
                    Return True
                End If
                If CriteriaDetail.OperatorName <> "" Then
                    If CriteriaDetail.OperatorName = "Greater Than" Then
                        If PatientDetail.Result1 > CriteriaDetail.Result1 Then
                            Return True
                        End If
                    ElseIf CriteriaDetail.OperatorName = "Less Than" Then
                        If PatientDetail.Result1 < CriteriaDetail.Result1 Then
                            Return True
                        End If
                    ElseIf CriteriaDetail.OperatorName = "Between" Then
                        If PatientDetail.Result1 > CriteriaDetail.Result1 AndAlso PatientDetail.Result2 < CriteriaDetail.Result2 Then
                            Return True
                        End If
                    End If
                End If
                Return True
            End Function

            Public Function AddCVCriteria(ByVal oCriteria As Supporting.Criteria) As Boolean
                Dim conn As New SqlConnection(GetConnectionString)

                'declare a transaction object
                Dim myTrans As SqlTransaction = Nothing
                Dim cmdCriteria As SqlCommand = Nothing

                Dim Criteria_ID As Long
                Dim MachineID As Long

                Dim objparam As SqlParameter = Nothing
                Try
                    conn.Open()
                    myTrans = conn.BeginTransaction
                    cmdCriteria = conn.CreateCommand
                    cmdCriteria.Transaction = myTrans


                    cmdCriteria.Connection = conn
                    cmdCriteria.CommandType = CommandType.StoredProcedure
                    cmdCriteria.CommandText = "CV_InUpCriteriaMST"

                    ''INSERT INTO CV_CRITARIA_MST
                    objparam = New SqlParameter("@cv_mst_Id", SqlDbType.BigInt)
                    With cmdCriteria.Parameters
                        objparam.Direction = ParameterDirection.InputOutput
                        .Add(objparam)
                        .Add("@MachineID", SqlDbType.BigInt)
                        .Add("@cv_mst_CriteriaName", SqlDbType.VarChar)
                        .Add("@cv_mst_AgeMin", SqlDbType.Decimal)
                        .Add("@cv_mst_AgeMax", SqlDbType.Decimal)
                        .Add("@cv_mst_Gender", SqlDbType.VarChar)
                        .Add("@cv_mst_HeightMin", SqlDbType.VarChar)
                        .Add("@cv_mst_HeightMax", SqlDbType.VarChar)
                        .Add("@cv_mst_WeightMin", SqlDbType.Decimal)
                        .Add("@cv_mst_WeightMax", SqlDbType.Decimal)
                        .Add("@cv_mst_BMIMin", SqlDbType.Decimal)
                        .Add("@cv_mst_BMIMax", SqlDbType.Decimal)
                        .Add("@cv_mst_TemperatureMin", SqlDbType.Decimal)
                        .Add("@cv_mst_TemperatureMax", SqlDbType.Decimal)
                        .Add("@cv_mst_PulseMin", SqlDbType.Decimal)
                        .Add("@cv_mst_PulseMax", SqlDbType.Decimal)
                        .Add("@cv_mst_PulseOxMin", SqlDbType.Decimal)
                        .Add("@cv_mst_PulseOxMax", SqlDbType.Decimal)
                        .Add("@cv_mst_BPSittingMin", SqlDbType.Decimal)
                        .Add("@cv_mst_BPSittingMax", SqlDbType.Decimal)
                        .Add("@cv_mst_BPStandingMin", SqlDbType.Decimal)
                        .Add("@cv_mst_BPStandingMax", SqlDbType.Decimal)
                        .Add("@cv_mst_DisplayMessage", SqlDbType.VarChar)
                    End With

                    With cmdCriteria
                        MachineID = GetPrefixTransactionID()

                        objparam.Value = Criteria_ID
                        .Parameters("@MachineID").Value = MachineID
                        .Parameters("@cv_mst_CriteriaName").Value = oCriteria.Name
                        .Parameters("@cv_mst_AgeMin").Value = oCriteria.AgeMinimum
                        .Parameters("@cv_mst_AgeMax").Value = oCriteria.AgeMaximum
                        .Parameters("@cv_mst_Gender").Value = oCriteria.Gender
                        .Parameters("@cv_mst_HeightMin").Value = oCriteria.HeightMinimum
                        .Parameters("@cv_mst_HeightMax").Value = oCriteria.HeightMaximum

                        If oCriteria.WeightMinimum = 0.0 Then
                            .Parameters("@cv_mst_WeightMin").Value = System.DBNull.Value
                        Else
                            .Parameters("@cv_mst_WeightMin").Value = oCriteria.WeightMinimum
                        End If

                        If oCriteria.WeightMaximum = 0.0 Then
                            .Parameters("@cv_mst_WeightMax").Value = System.DBNull.Value
                        Else
                            .Parameters("@cv_mst_WeightMax").Value = oCriteria.WeightMaximum
                        End If

                        If oCriteria.BMIMinimum = 0.0 Then
                            .Parameters("@cv_mst_BMIMin").Value = System.DBNull.Value
                        Else
                            .Parameters("@cv_mst_BMIMin").Value = oCriteria.BMIMinimum
                        End If

                        If oCriteria.BMIMaximum = 0.0 Then
                            .Parameters("@cv_mst_BMIMax").Value = System.DBNull.Value
                        Else
                            .Parameters("@cv_mst_BMIMax").Value = oCriteria.BMIMaximum
                        End If

                        If oCriteria.PulseMinimum = 0.0 Then
                            .Parameters("@cv_mst_PulseMin").Value = System.DBNull.Value
                        Else
                            .Parameters("@cv_mst_PulseMin").Value = oCriteria.PulseMinimum
                        End If

                        If oCriteria.PulseMaximum = 0.0 Then
                            .Parameters("@cv_mst_PulseMax").Value = System.DBNull.Value
                        Else
                            .Parameters("@cv_mst_PulseMax").Value = oCriteria.PulseMaximum
                        End If

                        If oCriteria.BPSittingMinimum = 0.0 Then
                            .Parameters("@cv_mst_BPSittingMin").Value = System.DBNull.Value
                        Else
                            .Parameters("@cv_mst_BPSittingMin").Value = oCriteria.BPSittingMinimum
                        End If

                        If oCriteria.BPSittingMaximum = 0.0 Then
                            .Parameters("@cv_mst_BPSittingMax").Value = System.DBNull.Value
                        Else
                            .Parameters("@cv_mst_BPSittingMax").Value = oCriteria.BPSittingMaximum
                        End If

                        If oCriteria.BPStandingMinimum = 0.0 Then
                            .Parameters("@cv_mst_BPStandingMin").Value = System.DBNull.Value
                        Else
                            .Parameters("@cv_mst_BPStandingMin").Value = oCriteria.BPStandingMinimum
                        End If

                        If oCriteria.BPStandingMaximum = 0.0 Then
                            .Parameters("@cv_mst_BPStandingMax").Value = System.DBNull.Value
                        Else
                            .Parameters("@cv_mst_BPStandingMax").Value = oCriteria.BPStandingMaximum
                        End If

                        If oCriteria.PulseOXMinimum = 0.0 Then
                            .Parameters("@cv_mst_PulseOxMin").Value = System.DBNull.Value
                        Else
                            .Parameters("@cv_mst_PulseOxMin").Value = oCriteria.PulseOXMinimum
                        End If

                        If oCriteria.PulseOXMaximum = 0.0 Then
                            .Parameters("@cv_mst_PulseOxMax").Value = System.DBNull.Value
                        Else
                            .Parameters("@cv_mst_PulseOxMax").Value = oCriteria.PulseOXMaximum
                        End If

                        If oCriteria.TempratureMinumum = 0.0 Then
                            .Parameters("@cv_mst_TemperatureMin").Value = System.DBNull.Value
                        Else
                            .Parameters("@cv_mst_TemperatureMin").Value = oCriteria.TempratureMinumum
                        End If

                        If oCriteria.TempratureMaximum = 0.0 Then
                            .Parameters("@cv_mst_TemperatureMax").Value = System.DBNull.Value
                        Else
                            .Parameters("@cv_mst_TemperatureMax").Value = oCriteria.TempratureMaximum
                        End If

                        .Parameters("@cv_mst_DisplayMessage").Value = oCriteria.DisplayMessage

                    End With

                    cmdCriteria.ExecuteNonQuery()


                    If Not IsNothing(objparam) Then
                        Criteria_ID = objparam.Value
                    End If

                    If Criteria_ID > 0 Then

                        Dim i As Int64 = 0

                        cmdCriteria.Parameters.Clear()
                        cmdCriteria.CommandText = "CV_InUpCriteriaDTL"

                        ''Insert All OtherDetails.
                        For i = 1 To oCriteria.OtherDetails.Count
                            cmdCriteria.Parameters.Add("@cv_mst_Id", SqlDbType.BigInt)
                            cmdCriteria.Parameters.Add("@cv_dtl_Id", SqlDbType.BigInt)
                            cmdCriteria.Parameters.Add("@MachineID", SqlDbType.BigInt)
                            cmdCriteria.Parameters.Add("@cv_dtl_CategoryName", SqlDbType.VarChar)
                            cmdCriteria.Parameters.Add("@cv_dtl_ItemName", SqlDbType.VarChar)
                            cmdCriteria.Parameters.Add("@cv_dtl_Operator", SqlDbType.VarChar)
                            cmdCriteria.Parameters.Add("@cv_dtl_ResultValue1", SqlDbType.VarChar)
                            cmdCriteria.Parameters.Add("@cv_dtl_ResultValue2", SqlDbType.VarChar)
                            cmdCriteria.Parameters.Add("@cv_dtl_Type", SqlDbType.Int)

                            MachineID = GetPrefixTransactionID()
                            cmdCriteria.Parameters("@cv_mst_Id").Value = Criteria_ID
                            cmdCriteria.Parameters("@cv_dtl_Id").Value = 0
                            cmdCriteria.Parameters("@MachineID").Value = MachineID
                            cmdCriteria.Parameters("@cv_dtl_CategoryName").Value = oCriteria.OtherDetails(i).CategoryName
                            cmdCriteria.Parameters("@cv_dtl_ItemName").Value = oCriteria.OtherDetails(i).ItemName
                            cmdCriteria.Parameters("@cv_dtl_Operator").Value = oCriteria.OtherDetails(i).OperatorName
                            cmdCriteria.Parameters("@cv_dtl_ResultValue1").Value = oCriteria.OtherDetails(i).Result1
                            cmdCriteria.Parameters("@cv_dtl_ResultValue2").Value = oCriteria.OtherDetails(i).Result2
                            cmdCriteria.Parameters("@cv_dtl_Type").Value = oCriteria.OtherDetails(i).DetailType.GetHashCode

                            cmdCriteria.ExecuteNonQuery()
                            cmdCriteria.Parameters.Clear()
                        Next

                        myTrans.Commit()

                        ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "'" & oCriteria.Name & "' Cardio Vascular Criteria Added", gloAuditTrail.ActivityOutCome.Success)
                        ''Added Rahul P on 20101008
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "'" & oCriteria.Name & "' Cardio Vascular Criteria Added", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        ''
                        'Dim objAudit As New clsAudit
                        'objAudit.CreateLog(clsAudit.enmActivityType.Add, "'" & oCriteria.Name & "' Cardio Vascular Criteria Added", gstrLoginName, 0)
                        'objAudit = Nothing

                        Return True
                    Else
                        myTrans.Rollback()
                        Return False
                    End If

                Catch ex As Exception
                    Try
                        myTrans.Rollback()
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Catch ex1 As SqlException
                        If Not myTrans.Connection Is Nothing Then
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "clsCardioVascular -- Add -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                            'UpdateLog("clsCardioVascular -- Add -- " & ex.ToString)
                        End If
                    End Try
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "clsCardioVascular -- Add -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    'UpdateLog("clsCardioVascular -- Add -- " & ex.ToString)
                    Return False
                Finally
                    If objparam IsNot Nothing Then
                        objparam = Nothing
                    End If

                    cmdCriteria.Parameters.Clear()
                    cmdCriteria.Dispose()
                    cmdCriteria = Nothing
                    conn.Close()
                    conn.Dispose()
                    conn = Nothing
                    If Not IsNothing(myTrans) Then
                        myTrans.Dispose()
                        myTrans = Nothing
                    End If
                End Try

            End Function

            Public Function ModifyCVCriteria(ByVal CriteriaID As Int64, ByVal oCriteria As Supporting.Criteria) As Boolean
                Dim conn As New SqlConnection(GetConnectionString)

                'declare a transaction object
                Dim myTrans As SqlTransaction = Nothing
                Dim cmdCriteria As SqlCommand = Nothing
                Dim MachineID As Long

                Dim objparam As SqlParameter = Nothing
                Try
                    conn.Open()
                    myTrans = conn.BeginTransaction
                    cmdCriteria = conn.CreateCommand
                    cmdCriteria.Transaction = myTrans


                    cmdCriteria.Connection = conn
                    cmdCriteria.CommandType = CommandType.StoredProcedure
                    cmdCriteria.CommandText = "CV_InUpCriteriaMST"

                    ''INSERT INTO CV_CRITARIA_MST
                    objparam = New SqlParameter("@cv_mst_Id", SqlDbType.BigInt)
                    With cmdCriteria.Parameters
                        objparam.Direction = ParameterDirection.InputOutput
                        .Add(objparam)
                        .Add("@MachineID", SqlDbType.BigInt)
                        .Add("@cv_mst_CriteriaName", SqlDbType.VarChar)
                        .Add("@cv_mst_AgeMin", SqlDbType.Decimal)
                        .Add("@cv_mst_AgeMax", SqlDbType.Decimal)
                        .Add("@cv_mst_Gender", SqlDbType.VarChar)
                        .Add("@cv_mst_HeightMin", SqlDbType.VarChar)
                        .Add("@cv_mst_HeightMax", SqlDbType.VarChar)
                        .Add("@cv_mst_WeightMin", SqlDbType.Decimal)
                        .Add("@cv_mst_WeightMax", SqlDbType.Decimal)
                        .Add("@cv_mst_BMIMin", SqlDbType.Decimal)
                        .Add("@cv_mst_BMIMax", SqlDbType.Decimal)
                        .Add("@cv_mst_TemperatureMin", SqlDbType.Decimal)
                        .Add("@cv_mst_TemperatureMax", SqlDbType.Decimal)
                        .Add("@cv_mst_PulseMin", SqlDbType.Decimal)
                        .Add("@cv_mst_PulseMax", SqlDbType.Decimal)
                        .Add("@cv_mst_PulseOxMin", SqlDbType.Decimal)
                        .Add("@cv_mst_PulseOxMax", SqlDbType.Decimal)
                        .Add("@cv_mst_BPSittingMin", SqlDbType.Decimal)
                        .Add("@cv_mst_BPSittingMax", SqlDbType.Decimal)
                        .Add("@cv_mst_BPStandingMin", SqlDbType.Decimal)
                        .Add("@cv_mst_BPStandingMax", SqlDbType.Decimal)
                        .Add("@cv_mst_DisplayMessage", SqlDbType.VarChar)
                    End With

                    With cmdCriteria
                        MachineID = GetPrefixTransactionID()

                        objparam.Value = CriteriaID
                        .Parameters("@MachineID").Value = MachineID
                        .Parameters("@cv_mst_CriteriaName").Value = oCriteria.Name
                        .Parameters("@cv_mst_AgeMin").Value = oCriteria.AgeMinimum
                        .Parameters("@cv_mst_AgeMax").Value = oCriteria.AgeMaximum
                        .Parameters("@cv_mst_Gender").Value = oCriteria.Gender
                        .Parameters("@cv_mst_HeightMin").Value = oCriteria.HeightMinimum
                        .Parameters("@cv_mst_HeightMax").Value = oCriteria.HeightMaximum

                        If oCriteria.WeightMinimum = 0.0 Then
                            .Parameters("@cv_mst_WeightMin").Value = System.DBNull.Value
                        Else
                            .Parameters("@cv_mst_WeightMin").Value = oCriteria.WeightMinimum
                        End If

                        If oCriteria.WeightMaximum = 0.0 Then
                            .Parameters("@cv_mst_WeightMax").Value = System.DBNull.Value
                        Else
                            .Parameters("@cv_mst_WeightMax").Value = oCriteria.WeightMaximum
                        End If

                        If oCriteria.BMIMinimum = 0.0 Then
                            .Parameters("@cv_mst_BMIMin").Value = System.DBNull.Value
                        Else
                            .Parameters("@cv_mst_BMIMin").Value = oCriteria.BMIMinimum
                        End If

                        If oCriteria.BMIMaximum = 0.0 Then
                            .Parameters("@cv_mst_BMIMax").Value = System.DBNull.Value
                        Else
                            .Parameters("@cv_mst_BMIMax").Value = oCriteria.BMIMaximum
                        End If

                        If oCriteria.PulseMinimum = 0.0 Then
                            .Parameters("@cv_mst_PulseMin").Value = System.DBNull.Value
                        Else
                            .Parameters("@cv_mst_PulseMin").Value = oCriteria.PulseMinimum
                        End If

                        If oCriteria.PulseMaximum = 0.0 Then
                            .Parameters("@cv_mst_PulseMax").Value = System.DBNull.Value
                        Else
                            .Parameters("@cv_mst_PulseMax").Value = oCriteria.PulseMaximum
                        End If

                        If oCriteria.BPSittingMinimum = 0.0 Then
                            .Parameters("@cv_mst_BPSittingMin").Value = System.DBNull.Value
                        Else
                            .Parameters("@cv_mst_BPSittingMin").Value = oCriteria.BPSittingMinimum
                        End If

                        If oCriteria.BPSittingMaximum = 0.0 Then
                            .Parameters("@cv_mst_BPSittingMax").Value = System.DBNull.Value
                        Else
                            .Parameters("@cv_mst_BPSittingMax").Value = oCriteria.BPSittingMaximum
                        End If

                        If oCriteria.BPStandingMinimum = 0.0 Then
                            .Parameters("@cv_mst_BPStandingMin").Value = System.DBNull.Value
                        Else
                            .Parameters("@cv_mst_BPStandingMin").Value = oCriteria.BPStandingMinimum
                        End If

                        If oCriteria.BPStandingMaximum = 0.0 Then
                            .Parameters("@cv_mst_BPStandingMax").Value = System.DBNull.Value
                        Else
                            .Parameters("@cv_mst_BPStandingMax").Value = oCriteria.BPStandingMaximum
                        End If

                        If oCriteria.PulseOXMinimum = 0.0 Then
                            .Parameters("@cv_mst_PulseOxMin").Value = System.DBNull.Value
                        Else
                            .Parameters("@cv_mst_PulseOxMin").Value = oCriteria.PulseOXMinimum
                        End If

                        If oCriteria.PulseOXMaximum = 0.0 Then
                            .Parameters("@cv_mst_PulseOxMax").Value = System.DBNull.Value
                        Else
                            .Parameters("@cv_mst_PulseOxMax").Value = oCriteria.PulseOXMaximum
                        End If

                        If oCriteria.TempratureMinumum = 0.0 Then
                            .Parameters("@cv_mst_TemperatureMin").Value = System.DBNull.Value
                        Else
                            .Parameters("@cv_mst_TemperatureMin").Value = oCriteria.TempratureMinumum
                        End If

                        If oCriteria.TempratureMaximum = 0.0 Then
                            .Parameters("@cv_mst_TemperatureMax").Value = System.DBNull.Value
                        Else
                            .Parameters("@cv_mst_TemperatureMax").Value = oCriteria.TempratureMaximum
                        End If

                        .Parameters("@cv_mst_DisplayMessage").Value = oCriteria.DisplayMessage

                    End With

                    cmdCriteria.ExecuteNonQuery()
                    If Not IsNothing(objparam) Then
                        CriteriaID = objparam.Value
                    End If

                    ''Delete All Records from OtherDetail Table & Insert Updated Data
                    cmdCriteria.Connection = conn
                    cmdCriteria.CommandType = CommandType.Text
                    cmdCriteria.CommandText = "DELETE FROM CV_Criteria_DTL WHERE (cv_mst_Id = " & CriteriaID & ")"
                    cmdCriteria.ExecuteNonQuery()

                    Dim i As Int64 = 0

                    cmdCriteria.Parameters.Clear()
                    cmdCriteria.CommandType = CommandType.StoredProcedure
                    cmdCriteria.CommandText = "CV_InUpCriteriaDTL"

                    ''Insert All OtherDetails.
                    For i = 1 To oCriteria.OtherDetails.Count
                        cmdCriteria.Parameters.Add("@cv_mst_Id", SqlDbType.BigInt)
                        cmdCriteria.Parameters.Add("@cv_dtl_Id", SqlDbType.BigInt)
                        cmdCriteria.Parameters.Add("@MachineID", SqlDbType.BigInt)
                        cmdCriteria.Parameters.Add("@cv_dtl_CategoryName", SqlDbType.VarChar)
                        cmdCriteria.Parameters.Add("@cv_dtl_ItemName", SqlDbType.VarChar)
                        cmdCriteria.Parameters.Add("@cv_dtl_Operator", SqlDbType.VarChar)
                        cmdCriteria.Parameters.Add("@cv_dtl_ResultValue1", SqlDbType.VarChar)
                        cmdCriteria.Parameters.Add("@cv_dtl_ResultValue2", SqlDbType.VarChar)
                        cmdCriteria.Parameters.Add("@cv_dtl_Type", SqlDbType.Int)

                        MachineID = GetPrefixTransactionID()
                        cmdCriteria.Parameters("@cv_mst_Id").Value = CriteriaID
                        cmdCriteria.Parameters("@cv_dtl_Id").Value = 0
                        cmdCriteria.Parameters("@MachineID").Value = MachineID
                        cmdCriteria.Parameters("@cv_dtl_CategoryName").Value = oCriteria.OtherDetails(i).CategoryName
                        cmdCriteria.Parameters("@cv_dtl_ItemName").Value = oCriteria.OtherDetails(i).ItemName
                        cmdCriteria.Parameters("@cv_dtl_Operator").Value = oCriteria.OtherDetails(i).OperatorName
                        cmdCriteria.Parameters("@cv_dtl_ResultValue1").Value = oCriteria.OtherDetails(i).Result1
                        cmdCriteria.Parameters("@cv_dtl_ResultValue2").Value = oCriteria.OtherDetails(i).Result2
                        cmdCriteria.Parameters("@cv_dtl_Type").Value = oCriteria.OtherDetails(i).DetailType.GetHashCode

                        cmdCriteria.ExecuteNonQuery()
                        cmdCriteria.Parameters.Clear()
                    Next

                    myTrans.Commit()

                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, "'" & oCriteria.Name & "' Cardio Vascular Criteria Modified", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20101008
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, "'" & oCriteria.Name & "' Cardio Vascular Criteria Modified", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ''
                    'Dim objAudit As New clsAudit
                    'objAudit.CreateLog(clsAudit.enmActivityType.Add, "'" & oCriteria.Name & "' Cardio Vascular Criteria Added", gstrLoginName, 0)
                    'objAudit = Nothing

                    Return True
                Catch ex As Exception
                    Try
                        myTrans.Rollback()
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Catch ex1 As SqlException
                        If Not myTrans.Connection Is Nothing Then
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, "clsCardioVascular -- Modify -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                            'UpdateLog("clsCardioVascular -- Add -- " & ex.ToString)
                        End If
                    End Try
                    Return False
                Finally
                    If objparam IsNot Nothing Then
                        objparam = Nothing
                    End If

                    cmdCriteria.Parameters.Clear()
                    cmdCriteria.Dispose()
                    cmdCriteria = Nothing
                    conn.Close()
                    conn.Dispose()
                    conn = Nothing
                    If Not IsNothing(myTrans) Then
                        myTrans.Dispose()
                        myTrans = Nothing
                    End If
                End Try
            End Function

            Public Function GetCriteria(ByVal CriteriaID As Long) As Supporting.Criteria
                Dim _strSQL As String = ""
                Dim oCriteria As New Supporting.Criteria
                Dim oOtherDetails As New Supporting.OtherDetails
                Dim oOtherDetail As Supporting.OtherDetail
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim oDataReader As SqlClient.SqlDataReader
                Dim _FillDetail As Boolean = False
                Try
                    If CriteriaID > 0 Then
                        'Criteria Master Record
                        _strSQL = "SELECT cv_mst_CriteriaName, cv_mst_AgeMin, cv_mst_AgeMax, cv_mst_Gender, " _
                                 & " cv_mst_HeightMin, cv_mst_HeightMax, cv_mst_WeightMin, cv_mst_WeightMax, cv_mst_BMIMin, " _
                                 & " cv_mst_BMIMax, cv_mst_TemperatureMin, cv_mst_TemperatureMax, cv_mst_PulseMin, cv_mst_PulseMax," _
                                 & " cv_mst_PulseOxMin, cv_mst_PulseOxMax, cv_mst_BPSittingMin, cv_mst_BPSittingMax, cv_mst_BPStandingMin," _
                                 & " cv_mst_BPStandingMax, cv_mst_DisplayMessage FROM CV_Criteria_MST WHERE (cv_mst_Id = " & CriteriaID & ")"
                        oDB.Connect(GetConnectionString)
                        oDataReader = oDB.ReadQueryRecords(_strSQL)
                        If Not oDataReader Is Nothing Then
                            If oDataReader.HasRows = True Then
                                _FillDetail = True
                                While oDataReader.Read
                                    oCriteria.Name = oDataReader.Item("cv_mst_CriteriaName") & ""
                                    If Not IsDBNull(oDataReader.Item("cv_mst_AgeMin")) Then
                                        oCriteria.AgeMinimum = oDataReader.Item("cv_mst_AgeMin") & ""
                                    End If
                                    If Not IsDBNull(oDataReader.Item("cv_mst_AgeMax")) Then
                                        oCriteria.AgeMaximum = oDataReader.Item("cv_mst_AgeMax") & ""
                                    End If
                                    If Not IsDBNull(oDataReader.Item("cv_mst_Gender")) Then
                                        oCriteria.Gender = oDataReader.Item("cv_mst_Gender") & ""
                                    End If
                                    If Not IsDBNull(oDataReader.Item("cv_mst_HeightMin")) Then
                                        oCriteria.HeightMinimum = oDataReader.Item("cv_mst_HeightMin") & ""
                                    End If
                                    If Not IsDBNull(oDataReader.Item("cv_mst_HeightMax")) Then
                                        oCriteria.HeightMaximum = oDataReader.Item("cv_mst_HeightMax") & ""
                                    End If
                                    If Not IsDBNull(oDataReader.Item("cv_mst_WeightMin")) Then
                                        oCriteria.WeightMinimum = oDataReader.Item("cv_mst_WeightMin") & ""
                                    End If
                                    If Not IsDBNull(oDataReader.Item("cv_mst_WeightMax")) Then
                                        oCriteria.WeightMaximum = oDataReader.Item("cv_mst_WeightMax") & ""
                                    End If
                                    If Not IsDBNull(oDataReader.Item("cv_mst_BMIMin")) Then
                                        oCriteria.BMIMinimum = oDataReader.Item("cv_mst_BMIMin") & ""
                                    End If
                                    If Not IsDBNull(oDataReader.Item("cv_mst_BMIMax")) Then
                                        oCriteria.BMIMaximum = oDataReader.Item("cv_mst_BMIMax") & ""
                                    End If
                                    If Not IsDBNull(oDataReader.Item("cv_mst_TemperatureMin")) Then
                                        oCriteria.TempratureMinumum = oDataReader.Item("cv_mst_TemperatureMin") & ""
                                    End If
                                    If Not IsDBNull(oDataReader.Item("cv_mst_TemperatureMax")) Then
                                        oCriteria.TempratureMaximum = oDataReader.Item("cv_mst_TemperatureMax") & ""
                                    End If
                                    If Not IsDBNull(oDataReader.Item("cv_mst_PulseMin")) Then
                                        oCriteria.PulseMinimum = oDataReader.Item("cv_mst_PulseMin") & ""
                                    End If
                                    If Not IsDBNull(oDataReader.Item("cv_mst_PulseMax")) Then
                                        oCriteria.PulseMaximum = oDataReader.Item("cv_mst_PulseMax") & ""
                                    End If
                                    If Not IsDBNull(oDataReader.Item("cv_mst_PulseOxMin")) Then
                                        oCriteria.PulseOXMinimum = oDataReader.Item("cv_mst_PulseOxMin") & ""
                                    End If
                                    If Not IsDBNull(oDataReader.Item("cv_mst_PulseOxMax")) Then
                                        oCriteria.PulseOXMaximum = oDataReader.Item("cv_mst_PulseOxMax") & ""
                                    End If
                                    If Not IsDBNull(oDataReader.Item("cv_mst_BPSittingMin")) Then
                                        oCriteria.BPSittingMinimum = oDataReader.Item("cv_mst_BPSittingMin") & ""
                                    End If
                                    If Not IsDBNull(oDataReader.Item("cv_mst_BPSittingMax")) Then
                                        oCriteria.BPSittingMaximum = oDataReader.Item("cv_mst_BPSittingMax") & ""
                                    End If
                                    If Not IsDBNull(oDataReader.Item("cv_mst_BPStandingMin")) Then
                                        oCriteria.BPStandingMinimum = oDataReader.Item("cv_mst_BPStandingMin") & ""
                                    End If
                                    If Not IsDBNull(oDataReader.Item("cv_mst_BPStandingMax")) Then
                                        oCriteria.BPStandingMaximum = oDataReader.Item("cv_mst_BPStandingMax") & ""
                                    End If
                                    If Not IsDBNull(oDataReader.Item("cv_mst_DisplayMessage")) Then
                                        oCriteria.DisplayMessage = oDataReader.Item("cv_mst_DisplayMessage") & ""
                                    End If
                                End While
                            End If
                            oDataReader.Close()
                        End If

                        ''Other Details
                        _strSQL = "SELECT cv_dtl_CategoryName, cv_dtl_ItemName, cv_dtl_Operator, cv_dtl_ResultValue1, " &
                            "cv_dtl_ResultValue2, cv_dtl_Type FROM CV_Criteria_DTL WHERE (cv_mst_Id = " & CriteriaID & ")"
                        oDataReader = oDB.ReadQueryRecords(_strSQL)
                        If Not oDataReader Is Nothing Then
                            If oDataReader.HasRows = True Then
                                While oDataReader.Read
                                    oOtherDetail = New Supporting.OtherDetail
                                    If Not IsDBNull(oDataReader.Item("cv_dtl_CategoryName")) Then
                                        oOtherDetail.CategoryName = oDataReader.Item("cv_dtl_CategoryName")
                                    End If
                                    If Not IsDBNull(oDataReader.Item("cv_dtl_ItemName")) Then
                                        oOtherDetail.ItemName = oDataReader.Item("cv_dtl_ItemName")
                                    End If
                                    If Not IsDBNull(oDataReader.Item("cv_dtl_Operator")) Then
                                        oOtherDetail.OperatorName = oDataReader.Item("cv_dtl_Operator")
                                    End If
                                    If Not IsDBNull(oDataReader.Item("cv_dtl_ResultValue1")) Then
                                        oOtherDetail.Result1 = oDataReader.Item("cv_dtl_ResultValue1")
                                    End If
                                    If Not IsDBNull(oDataReader.Item("cv_dtl_ResultValue2")) Then
                                        oOtherDetail.Result2 = oDataReader.Item("cv_dtl_ResultValue2")
                                    End If
                                    If Not IsDBNull(oDataReader.Item("cv_dtl_Type")) Then
                                        oOtherDetail.DetailType = CType(oDataReader.Item("cv_dtl_Type"), Supporting.enumDetailType)
                                    End If
                                    oOtherDetails.Add(oOtherDetail)
                                    oOtherDetail = Nothing
                                End While
                            End If
                            oDataReader.Close()
                        End If
                        oDataReader = Nothing
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                        oCriteria.OtherDetails = oOtherDetails
                        oOtherDetail = Nothing
                        oOtherDetails = Nothing
                        Return oCriteria
                    End If
                    Return Nothing
                Catch ex As Exception
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                End Try
            End Function

            Public Function GetOrders() As Collection
                Dim oDB As New gloStream.gloDataBase.gloDataBase

                Dim oLabs As New Collection
                Dim oLab As myList
                Dim oGroup As myList
                Dim oTest As myList

                '  Dim strSQL As String

                Dim dtCategory As DataTable = Nothing
                Dim dtGroups As DataTable = Nothing
                Dim dtTests As DataTable = Nothing

                Try
                    oDB.Connect(GetConnectionString)
                    'fill the oDTLab_Category table by the data(id,description) from the LM_Category table  
                    dtCategory = oDB.ReadData("DM_SelectLMCategory")

                    'With oLabs
                    'for each category in the oDTLab_Category(LM_Category) table
                    If (IsNothing(dtCategory) = False) Then


                        For i As Integer = 0 To dtCategory.Rows.Count - 1
                            oLab = New myList
                            'With oLab

                            oLab.ID = Convert.ToInt64(dtCategory.Rows(i)("lm_category_ID"))
                            Dim id As Int64 = oLab.ID
                            oLab.Description = dtCategory.Rows(i)("lm_category_Description")

                            'fill the oDTLab_Groups table by the data(id,name) from the LM_Test table  
                            'where  'LM_Test(TestGroupFlag = 'G' and CateogryID = LM_Category(i))
                            oDB.DBParameters.Clear()
                            oDB.DBParameters.Add("@id", id, ParameterDirection.Input, SqlDbType.BigInt)
                            dtGroups = oDB.ReadData("DM_SelectCategoryWiseLabGroup")
                            If (IsNothing(dtGroups) = False) Then


                                For j As Integer = 0 To dtGroups.Rows.Count - 1
                                    oGroup = New myList
                                    oGroup.ID = Convert.ToInt64(dtGroups.Rows(j)("lm_test_ID"))
                                    Dim groupid As Int64 = oGroup.ID
                                    oGroup.Description = dtGroups.Rows(j)("lm_test_Name")
                                    'fill the oDTLab_Tests table by data (id,description) from the LM_test table 
                                    'where LM_Test(TestGroupFlag = 'T' and CategoryID = LM_Category(i) and groupNo = LM_Test(j).id)
                                    oDB.DBParameters.Clear()
                                    oDB.DBParameters.Add("@id", id, ParameterDirection.Input, SqlDbType.BigInt)
                                    oDB.DBParameters.Add("@Groupid", groupid, ParameterDirection.Input, SqlDbType.BigInt)
                                    dtTests = oDB.ReadData("DM_SelectGroupWiseLabTests")
                                    If (IsNothing(dtTests) = False) Then
                                        For k As Integer = 0 To dtTests.Rows.Count - 1
                                            oTest = New myList
                                            oTest.ID = Convert.ToInt64(dtTests.Rows(k)("lm_test_ID"))
                                            oTest.Description = dtTests.Rows(k)("lm_test_Name")
                                            oGroup.MyCollection.Add(oTest)
                                        Next
                                    End If
                                    oLab.MyCollection.Add(oGroup)
                                    oGroup = Nothing
                                Next
                            End If
                            'End With
                            oLabs.Add(oLab)
                            oLab = Nothing
                        Next
                    End If
                    'End With

                    Return oLabs

                Catch ex As Exception
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    oDB.Disconnect()
                    oDB.Dispose()
                    oDB = Nothing

                    If Not IsNothing(dtTests) Then
                        dtTests.Dispose()
                        dtTests = Nothing
                    End If

                    If Not IsNothing(dtGroups) Then
                        dtGroups.Dispose()
                        dtGroups = Nothing
                    End If

                    If Not IsNothing(dtCategory) Then
                        dtCategory.Dispose()
                        dtCategory = Nothing
                    End If
                End Try


            End Function

            Public Function GetDrugs(Optional ByVal SearchText As String = "") As gloStream.CardioVascular.Supporting.OtherDetails

                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim oDataReader As SqlClient.SqlDataReader
                Dim oDrugs As New gloStream.CardioVascular.Supporting.OtherDetails

                Try
                    oDB.DBParameters.Add("@drugletter", SearchText, ParameterDirection.Input, SqlDbType.VarChar)
                    oDB.DBParameters.Add("@flag", 1, ParameterDirection.Input, SqlDbType.Int)
                    oDB.DBParameters.Add("@PatientID", 0, ParameterDirection.Input, SqlDbType.BigInt)

                    oDB.Connect(GetConnectionString)
                    oDataReader = oDB.ReadRecords("gsp_FillDrugs_Mst")

                    If Not oDataReader Is Nothing Then
                        If oDataReader.HasRows = True Then
                            While oDataReader.Read
                                Dim oDrug As New gloStream.CardioVascular.Supporting.OtherDetail
                                If Not IsDBNull(oDataReader.Item(0)) Then  '' DrugID
                                    oDrug.ItemID = oDataReader.Item(0)
                                End If
                                If Not IsDBNull(oDataReader.Item(1)) Then '' Drug Name
                                    oDrug.CategoryName = oDataReader.Item(1)
                                End If
                                If Not IsDBNull(oDataReader.Item(2)) Then '' Dosage
                                    oDrug.ItemName = oDataReader.Item(2)
                                End If
                                oDrugs.Add(oDrug)
                                oDrug = Nothing
                            End While
                        End If
                        oDataReader.Close()
                    End If

                    oDB.Disconnect()
                    Return oDrugs
                Catch ex As SqlException
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, "clsCardioVascular - GetDrugs" & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    'UpdateLog("clsDiseaseManagement -- Drugs -- " & ex.ToString)
                    Return Nothing
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, "clsCardioVascular - GetDrugs" & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    'UpdateLog("clsDiseaseManagement -- Drugs -- " & ex.ToString)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    oDB.Dispose()
                    oDB = Nothing
                    oDataReader = Nothing
                End Try
            End Function

            Public Function GetAge() As Collection
                'Declare a collection object for age values
                Dim _Age As New Collection
                Try
                    'fill the collection object
                    With _Age
                        For i As Int16 = 0 To 125
                            .Add(i)
                        Next
                    End With

                    'return the age collection
                    Return _Age
                Catch ex As Exception
                    Return Nothing
                Finally
                    '_Age = Nothing
                End Try
            End Function

            Public Function GetGender() As Collection
                'Male,Female,Other,All ' ref: gloEMR - patient registration form code file

                'declare the collection object
                Dim _Gender As New Collection

                Try
                    'fill the collection object
                    With _Gender
                        .Add("Male")
                        .Add("Female")
                        .Add("Other")
                        .Add("All")
                    End With

                    Return _Gender

                Catch ex As Exception
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    _Gender = Nothing
                End Try
            End Function

            Public Function GetHistoryCategory() As DataTable
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim dt As DataTable
                Dim Query As String = ""
                '     Dim HistoryCategory As Collection
                Try
                    oDB.Connect(GetConnectionString)
                    Query = "SELECT nCategoryID, sDescription FROM Category_MST WHERE (sCategoryType = 'History') ORDER BY sDescription"
                    dt = oDB.ReadQueryDataTable(Query)
                    If Not IsNothing(dt) Then
                        Return dt
                    End If
                    Return Nothing
                Catch ex As Exception
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    oDB.Disconnect()
                    oDB.Dispose()
                    oDB = Nothing
                    'dt.Dispose()
                    dt = Nothing
                End Try
            End Function

            Public Function GetHistoryItems(ByVal CategoryID As Int64, Optional ByVal CategoryName As String = "") As Supporting.OtherDetails
                Dim dt As DataTable = Nothing
                Dim oOtherDetails As New Supporting.OtherDetails
                Dim oOtherDetail As Supporting.OtherDetail
                Dim Query As String = ""
                '   Dim HistoryCategory As Collection
                Try
                    ''For MedicalCondition
                
                        Dim oDB As New gloStream.gloDataBase.gloDataBase

                        oDB.Connect(GetConnectionString)
                        Query = "SELECT History_MST.nHistoryID as HistoryID, History_MST.sDescription as HistoryItem, Category_MST.sDescription AS HistoryCategory, Category_MST.nCategoryID AS CategoryID FROM History_MST INNER JOIN Category_MST ON History_MST.nCategoryID = Category_MST.nCategoryID WHERE (Category_MST.nCategoryID = " & CategoryID & ") ORDER BY HistoryItem"
                        dt = oDB.ReadQueryDataTable(Query)
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing

                    If Not IsNothing(dt) Then
                        For i As Integer = 0 To dt.Rows.Count - 1
                            oOtherDetail = New Supporting.OtherDetail
                            oOtherDetail.ItemID = CType(dt.Rows(i)("HistoryID"), Int64) ''HistoryID
                            oOtherDetail.CategoryID = CType(dt.Rows(i)("CategoryID"), Int64) ''HistoryCategoryID
                            oOtherDetail.CategoryName = dt.Rows(i)("HistoryCategory").ToString   ''HistoryCategory
                            oOtherDetail.ItemName = dt.Rows(i)("HistoryItem").ToString  ''HistoryItem
                            oOtherDetails.Add(oOtherDetail)
                            oOtherDetail = Nothing
                        Next
                        dt.Dispose()
                        dt = Nothing

                    End If
                    'oOtherDetails = Nothing
                    'oOtherDetail = Nothing
                    '  HistoryCategory = Nothing
                    Return oOtherDetails
                Catch ex As Exception
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                End Try
            End Function
            Public Function GetHistoryItemsTable(ByVal CategoryID As Int64, Optional ByVal CategoryName As String = "") As DataTable

                Dim dt As DataTable = Nothing
                'Dim oOtherDetails As New Supporting.OtherDetails
                'Dim oOtherDetail As Supporting.OtherDetail
                Dim Query As String = ""
                'Dim HistoryCategory As Collection

                Try

                        Dim oDB As New gloStream.gloDataBase.gloDataBase
                        oDB.Connect(GetConnectionString)
                        Query = "SELECT History_MST.nHistoryID as HistoryID, History_MST.sDescription as HistoryItem, Category_MST.sDescription AS HistoryCategory, Category_MST.nCategoryID AS CategoryID FROM History_MST INNER JOIN Category_MST ON History_MST.nCategoryID = Category_MST.nCategoryID WHERE (Category_MST.nCategoryID = " & CategoryID & ") ORDER BY HistoryItem"
                        dt = oDB.ReadQueryDataTable(Query)
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                   

                    If Not IsNothing(dt) Then
                        Return dt
                    Else
                        Return Nothing
                    End If

                Catch ex As Exception
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                End Try

            End Function


            Public Function GetOrderGroups() As DataTable
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim dt As DataTable = Nothing
                Dim Query As String = ""
                '   Dim HistoryCategory As Collection
                Try
                    oDB.Connect(GetConnectionString)
                    Query = "SELECT lm_test_ID As GroupID, lm_test_Name as GroupName FROM LM_Test WHERE (lm_test_TestGroupFlag = 'G')"
                    dt = oDB.ReadQueryDataTable(Query)
                    
                   
                    If Not IsNothing(dt) Then
                        Return dt
                    Else
                        Return Nothing
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    oDB.Disconnect()
                    oDB.Dispose()
                    oDB = Nothing
                End Try

            End Function

            Public Function GetLabs() As Supporting.OtherDetails

                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim oOtherDetails As New Supporting.OtherDetails
                Dim oOtherDetail As Supporting.OtherDetail
                Dim dtTest As DataTable
                Dim dtResult As DataTable

                oDB.Connect(GetConnectionString)
                Try
                    Dim strSelectQryTest As String = "SELECT DISTINCT labtm_Name,labtm_id FROM Lab_Test_Mst"
                    dtTest = oDB.ReadQueryDataTable(strSelectQryTest)
                    If (IsNothing(dtTest) = False) Then


                        For i As Integer = 0 To dtTest.Rows.Count - 1

                            Dim strSelectQryResult As String = "Select DISTINCT Lab_Order_Test_ResultDtl.labotrd_ResultName,Lab_Order_Test_ResultDtl.labotrd_ResultNameID FROM Lab_Order_Test_ResultDtl INNER JOIN Lab_Order_Test_Result ON Lab_Order_Test_ResultDtl.labotrd_TestID = " & dtTest.Rows(i)("labtm_id") & " "
                            dtResult = oDB.ReadQueryDataTable(strSelectQryResult)
                            If (IsNothing(dtResult) = False) Then


                                For j As Integer = 0 To dtResult.Rows.Count - 1

                                    oOtherDetail = New Supporting.OtherDetail

                                    If IsDBNull(dtTest.Rows(i)("labtm_id")) = False Then
                                        oOtherDetail.CategoryID = dtTest.Rows(i)("labtm_id") '' TestID
                                    End If

                                    If IsDBNull(dtResult.Rows(j)("labotrd_ResultNameID")) = False Then
                                        oOtherDetail.ItemID = dtResult.Rows(j)("labotrd_ResultNameID") '' ResultID
                                    End If

                                    If IsDBNull(dtTest.Rows(i)("labtm_Name")) = False Then
                                        oOtherDetail.CategoryName = dtTest.Rows(i)("labtm_Name")  ''TestName
                                    End If

                                    If IsDBNull(dtResult.Rows(j)("labotrd_ResultName")) = False Then
                                        oOtherDetail.ItemName = dtResult.Rows(j)("labotrd_ResultName") ''ResultName
                                    End If

                                    oOtherDetails.Add(oOtherDetail)

                                    oOtherDetail = Nothing
                                Next
                                dtResult.Dispose()
                                dtResult = Nothing
                            End If
                        Next

                        dtTest.Dispose()
                        dtTest = Nothing
                    End If
                    Return oOtherDetails

                Catch ex As Exception
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    oDB.Disconnect()
                    oDB.Dispose()
                    oDB = Nothing
                End Try

            End Function

            Public Function GetLabCatagories() As DataTable

                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim dtTest As DataTable

                oDB.Connect(GetConnectionString)
                Try
                    Dim strSelectQryTest As String = "SELECT DISTINCT labtm_Name,labtm_id FROM Lab_Test_Mst"
                    dtTest = oDB.ReadQueryDataTable(strSelectQryTest)
                    Return dtTest

                Catch ex As Exception
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    oDB.Disconnect()
                    oDB.Dispose()
                    oDB = Nothing
                    '    dtTest.Dispose()
                    dtTest = Nothing
                End Try

            End Function

            Public Function IsExists(ByVal oCriteriaName As String) As Boolean
                'criteria master name exists
                Dim _strSQL As String = ""
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim oDataReader As SqlClient.SqlDataReader
                Dim _Result As Boolean = False


                Try

                    'connect to the database
                    oDB.Connect(GetConnectionString)
                    'set the query string
                    '_strSQL = "SELECT dm_mst_Id, dm_mst_CriteriaName FROM DM_Criteria_MST where dm_mst_CriteriaName = '" & oCriteriaName & "'"
                    _strSQL = "SELECT cv_mst_Id, cv_dtl_CategoryName FROM CV_Criteria_DTL WHERE cv_dtl_CategoryName = '" & oCriteriaName & "'"
                    'execute the query and return a datareader
                    oDataReader = oDB.ReadQueryRecords(_strSQL)

                    'check if there is any data in the datareader
                    If Not oDataReader Is Nothing Then
                        If oDataReader.HasRows = True Then
                            While oDataReader.Read
                                If Not IsDBNull(oDataReader.Item("cv_dtl_CategoryName")) Then
                                    'if the criteria name matches a name in the table then return true
                                    If oDataReader.Item("cv_dtl_CategoryName") = oCriteriaName Then
                                        _Result = True
                                        Exit While
                                    End If
                                End If
                            End While
                        End If
                        oDataReader.Close()
                    End If

                    Return _Result
                Catch ex As SqlException
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    'UpdateLog("clsCardioVascular -- IsExists -- " & ex.ToString)
                    Return Nothing
                Catch ex As Exception
                    'UpdateLog("clsCardioVascular -- IsExists -- " & ex.ToString)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    oDB.Disconnect()
                    oDB.Dispose()
                    oDB = Nothing
                    oDataReader = Nothing
                End Try
            End Function

            Public Function IsDelete(ByVal oCriteriaName As String) As Boolean
                '// REMARK

                Dim _strSQL As String = ""
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                ' Dim oDataReader As SqlClient.SqlDataReader
                Dim CriteriaID As Long = 0            '''''Previously it was declared as integer changed by Anil on 29/10/2007 toresolve bug no-436
                Dim _Result As Boolean = True
                Dim _Count As Long = 0

                Dim Conn As SqlConnection = Nothing
                Dim cmd As SqlCommand = Nothing

                Try

                    'connect to the database
                    Conn = New SqlConnection(GetConnectionString())
                    Conn.Open()
                    oDB.Connect(GetConnectionString)

                    'extract the criteria id from the table for the given criteria name
                    _strSQL = "SELECT dm_mst_Id FROM DM_Criteria_MST where dm_mst_CriteriaName = '" & oCriteriaName & "'"
                    cmd = New SqlCommand(_strSQL, Conn)

                    CriteriaID = cmd.ExecuteScalar()  'Val(oDB.ExecuteQueryScaler(_strSQL))
                    cmd.Dispose()
                    cmd = Nothing
                    Conn.Close()
                    Conn.Dispose()
                    Conn = Nothing
                    'set the query string
                    _strSQL = "SELECT COUNT(DM_TransId) FROM DM_Patient where DM_nCriteriaID =" & CriteriaID
                    'execute the query and return a datareader
                    _Count = oDB.ExecuteQueryScaler(_strSQL)

                    'check if there is any data in the datareader
                    If _Count > 0 Then
                        _Result = False
                    End If

                    Return _Result
                Catch ex As SqlException
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    'UpdateLog("clsCardioVascular -- IsDelete -- " & ex.ToString)
                    Return Nothing
                Catch ex As Exception
                    'UpdateLog("clsCardioVascular -- IsDelete -- " & ex.ToString)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                    ' Return _Result
                Finally
                    oDB.Disconnect()
                    oDB.Dispose()
                    oDB = Nothing
                End Try
                'Return True
            End Function

            Public Function GetPatientAgeinYrs(ByVal PatientDOB As Date) As Int32
                Dim nMonths, nPatAge As Int32
                nMonths = DateDiff(DateInterval.Month, CType(PatientDOB, Date), Date.Now.Date)
                nPatAge = nMonths \ 12
                Return nPatAge
            End Function

            Public Function GetPatientDetails(ByVal PatientID As Int64) As Supporting.PatientDetail

                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim dt As DataTable = Nothing
                Dim oPatientDetails As New Supporting.PatientDetail
                Dim oOtherDetail As Supporting.OtherDetail
                Dim strQuery As String = ""
                Dim i As Int16
                oDB.Connect(GetConnectionString)
                Try

                    '' Get Patient's Demographic Details
                    strQuery = "Select  sPatientCode, ISNULL(sFirstName,'') + SPACE(1) + ISNULL(sMiddleName,'') + SPACE(1) + ISNULL(sLastName,'') AS PatientName ,dtDOB, sGender FROM Patient WHERE nPatientID = " & PatientID & ""
                    dt = oDB.ReadQueryDataTable(strQuery)
                    If IsNothing(dt) = False Then
                        If dt.Rows.Count > 0 Then
                            With dt.Rows(0)
                                oPatientDetails.PatientCode = .Item("sPatientCode")
                                oPatientDetails.PatientName = .Item("PatientName")
                                oPatientDetails.Age = GetPatientAgeinYrs(.Item("dtDOB"))
                                oPatientDetails.Gender = .Item("sGender")
                            End With
                            '           dt = Nothing
                        End If
                        dt.Dispose()
                        dt = Nothing
                    End If

                    '' Get Patient's Latest Vital Details
                    strQuery = "Select TOP 1 nVitalID, nVisitID, dtVitalDate, ISNULL(sHeight,'') AS sHeight, ISNULL(dWeightinlbs,0) AS dWeightinlbs, ISNULL(dBMI,0) AS dBMI, ISNULL(dWeightinKg,0) AS dWeightinKg, ISNULL(dTemperature,0) AS dTemperature, ISNULL(dRespiratoryRate,0) AS dRespiratoryRate, ISNULL(dPulsePerMinute,0) AS dPulsePerMinute, ISNULL(dPulseOx,0) AS dPulseOx, ISNULL(dBloodPressureSittingMin,0) AS dBloodPressureSittingMin, ISNULL(dBloodPressureSittingMax,0) AS dBloodPressureSittingMax, ISNULL(dBloodPressureStandingMin,0) AS dBloodPressureStandingMin, ISNULL(dBloodPressureStandingMax,0) AS dBloodPressureStandingMax  FROM Vitals WHERE nPatientID = " & PatientID & " Order by  dtVitalDate DESC "
                    dt = oDB.ReadQueryDataTable(strQuery)

                    If IsNothing(dt) = False Then
                        If dt.Rows.Count > 0 Then

                            With dt.Rows(0)
                                oPatientDetails.VitalDate = .Item("dtVitalDate")
                                oPatientDetails.Height = .Item("sHeight")
                                oPatientDetails.WeightInlbs = .Item("dWeightinlbs")
                                oPatientDetails.BMI = .Item("dBMI")
                                oPatientDetails.TempratureInF = .Item("dTemperature")
                                oPatientDetails.Pulse = .Item("dPulsePerMinute")
                                oPatientDetails.PulseOX = .Item("dPulseOx")
                                oPatientDetails.BPSittingMinimum = .Item("dBloodPressureSittingMin")
                                oPatientDetails.BPSittingMaximum = .Item("dBloodPressureSittingMax")
                                oPatientDetails.BPStandingMinimum = .Item("dBloodPressureStandingMin")
                                oPatientDetails.BPStandingMaximum = .Item("dBloodPressureStandingMax")
                            End With
                            '             dt = Nothing
                        End If
                        dt.Dispose()
                        dt = Nothing
                    End If

                    '' Get Patient's Latest History Details 
                    Dim strVisitID As String = ""
                    Dim nVisitID As Int64 = 0
                    '' Get Latest VisitID against which the History Is Entered
                    strQuery = "SELECT History.nVisitID AS nVisitID  " _
                                        & " FROM	History INNER JOIN	Visits ON History.nVisitID = Visits.nVisitID WHERE History.nPatientID= " & PatientID & " ORDER BY dtVisitDate DESC "
                    strVisitID = oDB.ExecuteQueryScaler(strQuery)
                    If (strVisitID <> "") Then
                        nVisitID = Convert.ToInt64(strVisitID)
                    Else
                        nVisitID = 0
                    End If

                    If nVisitID <> 0 Then
                        '' Get History For the latest Visit
                        strQuery = "SELECT  ISNULL(sHistoryCategory,'') AS sHistoryCategory,ISNULL(sHistoryItem,'') AS sHistoryItem, ISNULL(sComments,'') AS sComments, dtVisitDate, History.nVisitID AS nVisitID  " _
                                  & " FROM	History INNER JOIN	Visits ON History.nVisitID = Visits.nVisitID WHERE History.nVisitID= " & nVisitID & " ORDER BY dtVisitDate DESC "
                        dt = oDB.ReadQueryDataTable(strQuery)

                        If IsNothing(dt) = False Then
                            For i = 0 To dt.Rows.Count - 1
                                oOtherDetail = New Supporting.OtherDetail
                                With dt.Rows(i)
                                    oOtherDetail.CategoryName = .Item("sHistoryCategory")
                                    oOtherDetail.ItemName = .Item("sHistoryItem")
                                    oOtherDetail.ItemDate = .Item("dtVisitDate")
                                    oOtherDetail.ItemID = .Item("nVisitID")
                                    oOtherDetail.DetailType = Supporting.enumDetailType.History

                                    oPatientDetails.OtherDetails.Add(oOtherDetail)
                                End With
                            Next
                            dt.Dispose()
                            dt = Nothing
                        End If
                    End If



                    ''Medication
                    '' Get Patient's Latest Medication Details 
                    strVisitID = ""
                    nVisitID = 0
                    '' Get Latest VisitID against which the Medication Is Entered
                    strQuery = "SELECT ISNULL(Medication.nVisitID,0) AS nVisitID  " _
                                        & " FROM	Medication INNER JOIN	Visits ON Medication.nVisitID = Visits.nVisitID WHERE Medication.nPatientID= " & PatientID & " ORDER BY dtVisitDate DESC "
                    strVisitID = oDB.ExecuteQueryScaler(strQuery)
                    If (strVisitID <> "") Then
                        nVisitID = Convert.ToInt64(strVisitID)
                    Else
                        nVisitID = 0
                    End If

                    If nVisitID <> 0 Then
                        'fill the Patient's Drug information using the Medication table
                        strQuery = "SELECT distinct LTRIM(RTRIM(ISNULL(Medication.sMedication,''))) AS sMedication ,LTRIM(RTRIM(ISNULL(Medication.sDosage,''))) AS sDosage, LTRIM(RTRIM(ISNULL(Medication.sRoute,''))) AS sRoute , dtMedicationDate FROM Medication WHERE Medication.nVisitID = " & nVisitID & ""

                        dt = oDB.ReadQueryDataTable(strQuery)

                        If IsNothing(dt) = False Then
                            For i = 0 To dt.Rows.Count - 1
                                oOtherDetail = New Supporting.OtherDetail
                                With dt.Rows(i)
                                    oOtherDetail.ItemID = 0
                                    oOtherDetail.ItemDate = .Item("dtMedicationDate")
                                    oOtherDetail.CategoryName = .Item("sMedication")
                                    oOtherDetail.ItemName = .Item("sDosage")
                                    oOtherDetail.DetailType = Supporting.enumDetailType.Medication
                                    oPatientDetails.OtherDetails.Add(oOtherDetail)
                                End With
                            Next
                            dt.Dispose()
                            dt = Nothing
                        End If
                    End If
                    ''

                    '' Get Patient's Latest Labs Details 
                    Dim strLabID As String = ""
                    Dim nLabID As Int64 = 0
                    '' Get Latest  Labs Date against which the Labs Is Entered
                    strQuery = "SELECT ISNULL(labom_OrderID,0) AS labom_OrderID FROM Lab_Order_MST WHERE Lab_Order_MST.labom_PatientID= " & PatientID & " ORDER BY labom_TransactionDate DESC "
                    strLabID = oDB.ExecuteQueryScaler(strQuery)
                    If (strLabID <> "") Then
                        nLabID = Convert.ToInt64(strLabID)
                    Else
                        nLabID = 0
                    End If

                    If nLabID <> 0 Then
                        '' Get Labs For the latest Labs Date
                        strQuery = "SELECT DISTINCT  Lab_Order_MST.labom_OrderNoPrefix, Lab_Order_MST.labom_OrderNoID, ISNULL(Lab_Test_Mst.labtm_Name,'') AS labtm_Name, Lab_Order_TestDtl.labotd_TestID, " _
                            & " Lab_Test_Mst.labtm_Code, Lab_Order_MST.labom_TransactionDate, Lab_Order_MST.labom_VisitID, Lab_Order_MST.labom_ProviderID, " _
                            & " ISNULL(Lab_Order_Test_ResultDtl.labotrd_ResultName,'') AS labotrd_ResultName, ISNULL(Lab_Order_Test_ResultDtl.labotrd_ResultValue,'') AS labotrd_ResultValue, Lab_Order_Test_ResultDtl.labotrd_ResultUnit " _
                            & " FROM         Lab_Order_TestDtl INNER JOIN " _
                            & " Lab_Order_MST ON Lab_Order_TestDtl.labotd_OrderID = Lab_Order_MST.labom_OrderID INNER JOIN " _
                            & " Lab_Test_Mst ON Lab_Order_TestDtl.labotd_TestID = Lab_Test_Mst.labtm_ID LEFT OUTER JOIN " _
                            & " Lab_Order_Test_ResultDtl ON Lab_Order_TestDtl.labotd_OrderID = Lab_Order_Test_ResultDtl.labotrd_OrderID AND  " _
                            & " Lab_Order_TestDtl.labotd_TestID = Lab_Order_Test_ResultDtl.labotrd_TestID " _
                            & " WHERE Lab_Order_MST.labom_OrderID = " & nLabID & " "
                        dt = oDB.ReadQueryDataTable(strQuery)

                        If IsNothing(dt) = False Then
                            For i = 0 To dt.Rows.Count - 1
                                oOtherDetail = New Supporting.OtherDetail
                                With dt.Rows(i)
                                    oOtherDetail.ItemID = nLabID
                                    oOtherDetail.ItemDate = .Item("labom_TransactionDate")
                                    oOtherDetail.CategoryName = .Item("labtm_Name")
                                    oOtherDetail.ItemName = .Item("labotrd_ResultName")
                                    oOtherDetail.Result1 = .Item("labotrd_ResultValue")
                                    oOtherDetail.DetailType = Supporting.enumDetailType.Lab
                                    oPatientDetails.OtherDetails.Add(oOtherDetail)
                                End With
                            Next
                            dt.Dispose()
                            dt = Nothing
                        End If
                    End If


                    '' Get Patient's Latest Order Details 

                    '' Get Latest Orders Date against which the Labs Is Entered

                    strQuery = "SELECT TOP 1 LM_Orders.lm_Visit_ID, LM_Orders.lm_OrderDate FROM   LM_Orders WHERE lm_Patient_ID = " & PatientID & " ORDER BY lm_OrderDate DESC "
                    Dim dtOrders As DataTable = oDB.ReadQueryDataTable(strQuery)
                    Dim VisitID As Int64
                    Dim OrderDate As Date
                    If IsNothing(dtOrders) = False Then
                        If dtOrders.Rows.Count > 0 Then
                            VisitID = Convert.ToInt64(dtOrders.Rows(0)("lm_Visit_ID"))
                            OrderDate = Convert.ToDateTime(dtOrders.Rows(0)("lm_OrderDate"))

                            '' Get Orders For the latest Order DateTime
                            '' COMMENT BY SUDHIR 20090623 ''
                            'strQuery = "SELECT     ISNULL(LM_Category.lm_category_Description,'') AS lm_category_Description, ISNULL(LM_Test.lm_test_Name,lm_test_Name ) AS lm_test_Name, LM_Orders.lm_Visit_ID, LM_Orders.lm_Patient_ID, LM_Orders.lm_OrderDate " _
                            '        & " FROM         LM_Orders INNER JOIN " _
                            '        & " LM_Test ON LM_Orders.lm_test_ID = LM_Test.lm_test_ID INNER JOIN " _
                            '        & " LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID " _
                            '        & " WHERE lm_Visit_ID = " & VisitID & " AND lm_OrderDate = '" & OrderDate & "'"

                            strQuery = " SELECT ISNULL(lm_sCategoryName,'') AS lm_sCategoryName, ISNULL(lm_sTestName,'') AS lm_sTestName, lm_OrderDate " _
                                    & " FROM LM_Orders WHERE lm_Visit_ID = " & VisitID & " AND lm_OrderDate = '" & OrderDate & "'"


                            dt = oDB.ReadQueryDataTable(strQuery)

                            If IsNothing(dt) = False Then
                                For i = 0 To dt.Rows.Count - 1
                                    oOtherDetail = New Supporting.OtherDetail
                                    With dt.Rows(i)
                                        oOtherDetail.ItemID = VisitID
                                        oOtherDetail.ItemDate = .Item("lm_OrderDate")
                                        oOtherDetail.CategoryName = .Item("lm_sCategoryName")
                                        oOtherDetail.ItemName = .Item("lm_sTestName")
                                        oOtherDetail.DetailType = Supporting.enumDetailType.Order
                                        oPatientDetails.OtherDetails.Add(oOtherDetail)
                                    End With
                                Next
                                dt.Dispose()
                                dt = Nothing
                            End If
                        End If
                        dtOrders.Dispose()
                        dtOrders = Nothing
                    End If

                Catch ex As Exception
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    oOtherDetail = Nothing
                    If Not IsNothing(dt) Then
                        dt.Dispose()
                        dt = Nothing
                    End If
                    If Not IsNothing(oDB) Then
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                    End If
                End Try

                Return oPatientDetails

            End Function

            Public Function GetAllRisk(ByVal PatientDetail As Supporting.PatientDetail, ByVal Criterias As Supporting.Criterias) As Supporting.PatientDetails
                Dim oRisks As New Supporting.PatientDetails
                Dim oRisk As Supporting.PatientDetail

                Try
                    If Not IsNothing(Criterias) Then
                        For i As Integer = 1 To Criterias.Count
                            'oRisk = New Supporting.PatientDetail
                            oRisk = GetRisk(PatientDetail, Criterias(i))
                            oRisks.Add(oRisk)
                            oRisk = Nothing
                        Next
                    End If
                    Return oRisks
                Catch ex As Exception
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                End Try
            End Function

            Public Function GetRisk(ByVal PatientDetail As Supporting.PatientDetail, ByVal Criteria As Supporting.Criteria) As Supporting.PatientDetail
                Dim oRisk As New Supporting.PatientDetail
                Dim oOtherDetails As New Supporting.OtherDetails
                '  Dim oOtherDetail As Supporting.OtherDetail

                Try
                    ''Set Criteria ID & Name to oRist
                    oRisk.CriteriaID = Criteria.ID
                    oRisk.CriteriaName = Criteria.Name

                    ''AGE
                    If PatientDetail.Age > Criteria.AgeMinimum AndAlso PatientDetail.Age < Criteria.AgeMaximum Then
                        oRisk.Age = PatientDetail.Age
                    End If
                    ''GENDER
                    If PatientDetail.Gender = Criteria.Gender Or Criteria.Gender = "All" Then
                        oRisk.Gender = PatientDetail.Gender
                    End If
                    ''HEIGHT
                    If PatientDetail.Height <> "" Then
                        Dim PatientHeight As Decimal = FtToMtr(PatientDetail.Height)
                        Dim CriteriaHeightMax As Decimal = FtToMtr(Criteria.HeightMaximum)
                        Dim CriteriaHeightMin As Decimal = FtToMtr(Criteria.HeightMinimum)

                        If PatientHeight > CriteriaHeightMin AndAlso PatientHeight < CriteriaHeightMax Then
                            oRisk.Height = PatientDetail.Height
                        End If
                    End If
                    ''WEIGHT
                    If PatientDetail.WeightInlbs > Criteria.WeightMinimum AndAlso PatientDetail.WeightInlbs < Criteria.WeightMaximum Then
                        oRisk.WeightInlbs = PatientDetail.WeightInlbs
                    End If
                    ''PULSE
                    If PatientDetail.Pulse > Criteria.PulseMinimum AndAlso PatientDetail.Pulse < Criteria.PulseMaximum Then
                        oRisk.Pulse = PatientDetail.Pulse
                    End If
                    ''PULSE_OX
                    If PatientDetail.PulseOX > Criteria.PulseOXMinimum AndAlso PatientDetail.PulseOX < Criteria.PulseOXMaximum Then
                        oRisk.PulseOX = PatientDetail.PulseOX
                    End If
                    ''BP SITTING MAX
                    If PatientDetail.BPSittingMaximum = Criteria.BPSittingMaximum Then
                        oRisk.BPSittingMaximum = PatientDetail.BPSittingMaximum
                    End If
                    ''BP SITTING MIN
                    If PatientDetail.BPSittingMinimum = Criteria.BPSittingMinimum Then
                        oRisk.BPSittingMinimum = PatientDetail.BPSittingMinimum
                    End If
                    ''BP STANDIN MAX
                    If PatientDetail.BPStandingMaximum = Criteria.BPStandingMaximum Then
                        oRisk.BPStandingMaximum = PatientDetail.BPStandingMaximum
                    End If
                    ''BP STANDIN MIN
                    If PatientDetail.BPStandingMinimum = Criteria.BPStandingMinimum Then
                        oRisk.BPStandingMinimum = PatientDetail.BPStandingMinimum
                    End If
                    ''BMI
                    If PatientDetail.BMI > Criteria.BMIMinimum AndAlso PatientDetail.BMI < Criteria.BMIMaximum Then
                        oRisk.BMI = PatientDetail.BMI
                    End If
                    If PatientDetail.TempratureInF > Criteria.TempratureMinumum AndAlso PatientDetail.TempratureInF < Criteria.TempratureMaximum Then
                        oRisk.TempratureInF = PatientDetail.TempratureInF
                    End If

                    ''OTHER DETAILS
                    For intPatDetail As Integer = 1 To PatientDetail.OtherDetails.Count
                        For intCriteria As Integer = 1 To Criteria.OtherDetails.Count
                            If IsOtherDetailSame(PatientDetail.OtherDetails(intPatDetail), Criteria.OtherDetails(intCriteria)) Then
                                oOtherDetails.Add(PatientDetail.OtherDetails(intPatDetail))
                                Exit For
                            End If
                        Next
                    Next

                    ''SET OTHERDETAILS TO ORISK
                    oRisk.OtherDetails = oOtherDetails

                    Return oRisk
                Catch ex As Exception
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                End Try
            End Function

            Public Function GetLatestPatientHistory(ByVal PatientID As Int64) As Supporting.PatientDetail
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim dt As DataTable = Nothing
                Dim oPatientDetails As New Supporting.PatientDetail
                Dim oOtherDetail As Supporting.OtherDetail
                Dim strQuery As String = ""
                Dim i As Int16
                oDB.Connect(GetConnectionString)
                Try
                    '' Get Patient's Latest History Details 
                    Dim strVisitID As String = ""
                    Dim nVisitID As Int64 = 0
                    '' Get Latest VisitID against which the History Is Entered
                    strQuery = "SELECT History.nVisitID AS nVisitID  " _
                                        & " FROM	History INNER JOIN	Visits ON History.nVisitID = Visits.nVisitID WHERE History.nPatientID= " & PatientID & " ORDER BY dtVisitDate DESC "
                    strVisitID = oDB.ExecuteQueryScaler(strQuery)
                    If (strVisitID <> "") Then
                        nVisitID = Convert.ToInt64(strVisitID)
                    Else
                        nVisitID = 0
                    End If

                    If nVisitID <> 0 Then
                        '' Get History For the latest Visit
                        strQuery = "SELECT  ISNULL(sHistoryCategory,'') AS sHistoryCategory,ISNULL(sHistoryItem,'') AS sHistoryItem, ISNULL(sComments,'') AS sComments, dtVisitDate, History.nVisitID AS nVisitID  " _
                                  & " FROM	History INNER JOIN	Visits ON History.nVisitID = Visits.nVisitID WHERE History.nVisitID= " & nVisitID & " ORDER BY dtVisitDate DESC "
                        dt = oDB.ReadQueryDataTable(strQuery)

                        If IsNothing(dt) = False Then
                            For i = 0 To dt.Rows.Count - 1
                                oOtherDetail = New Supporting.OtherDetail
                                With dt.Rows(i)
                                    oOtherDetail.CategoryName = .Item("sHistoryCategory")
                                    oOtherDetail.ItemName = .Item("sHistoryItem")
                                    oOtherDetail.ItemDate = .Item("dtVisitDate")
                                    oOtherDetail.Result1 = .Item("sComments")
                                    oOtherDetail.ItemID = .Item("nVisitID")
                                    oOtherDetail.DetailType = Supporting.enumDetailType.History

                                    oPatientDetails.OtherDetails.Add(oOtherDetail)
                                End With
                            Next
                        End If
                    End If

                    Return oPatientDetails
                Catch ex As Exception
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    If (IsNothing(dt) = False) Then
                        dt.Dispose()
                        dt = Nothing
                    End If

                    oDB.Disconnect()

                    oDB.Dispose()
                    oDB = Nothing
                End Try

            End Function

        End Class

        Namespace Supporting

            Public Enum enumDetailType
                None
                History
                Medication
                Lab
                Order
            End Enum

            Public Class OtherDetail
                Private mItemID As Long = 0
                Private mCategoryID As Long = 0
                Private mCategoryName As String = ""
                Private mItemName As String = ""
                Private mOperator As String = ""
                Private mResult1 As String = ""
                Private mResult2 As String = ""
                Private mItemDate As Date
                Private mDetailType As enumDetailType

                Public Property ItemID() As Long
                    Get
                        Return mItemID
                    End Get
                    Set(ByVal Value As Long)
                        mItemID = Value
                    End Set
                End Property

                Public Property CategoryID() As Long
                    Get
                        Return mCategoryID
                    End Get
                    Set(ByVal Value As Long)
                        mCategoryID = Value
                    End Set
                End Property

                Public Property CategoryName() As String
                    Get
                        Return mCategoryName
                    End Get
                    Set(ByVal Value As String)
                        mCategoryName = Value
                    End Set
                End Property

                Public Property ItemName() As String
                    Get
                        Return mItemName
                    End Get
                    Set(ByVal Value As String)
                        mItemName = Value
                    End Set
                End Property

                Public Property OperatorName() As String
                    Get
                        Return mOperator
                    End Get
                    Set(ByVal Value As String)
                        mOperator = Value
                    End Set
                End Property

                Public Property Result1() As String
                    Get
                        Return mResult1
                    End Get
                    Set(ByVal Value As String)
                        mResult1 = Value
                    End Set
                End Property

                Public Property Result2() As String
                    Get
                        Return mResult2
                    End Get
                    Set(ByVal Value As String)
                        mResult2 = Value
                    End Set
                End Property

                Public Property ItemDate() As Date
                    Get
                        Return mItemDate
                    End Get
                    Set(ByVal Value As Date)
                        mItemDate = Value
                    End Set
                End Property

                Public Property DetailType() As enumDetailType
                    Get
                        Return mDetailType
                    End Get
                    Set(ByVal Value As enumDetailType)
                        mDetailType = Value
                    End Set
                End Property

                Public Sub New()
                    MyBase.new()
                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub

            End Class

            Public Class OtherDetails
                Implements System.Collections.IEnumerable
                Private mCol As Collection
                Public Sub Dispose()
                    If (IsNothing(mCol) = False) Then
                        mCol.Clear()
                        mCol = Nothing
                    End If

                End Sub
                Public Function Add(ByRef oOtherDetail As gloStream.CardioVascular.Supporting.OtherDetail) As gloStream.CardioVascular.Supporting.OtherDetail
                    mCol.Add(oOtherDetail)
                    Return Nothing
                End Function

                Public Function Add(ByVal ItemID As Int64, ByVal CategoryID As Int64, ByVal CategoryName As String, ByVal ItemName As String, ByVal OperatorName As String, ByVal Result1 As String, ByVal Result2 As String) As gloStream.CardioVascular.Supporting.OtherDetail
                    'create a new object
                    Dim oOtherDetail As gloStream.CardioVascular.Supporting.OtherDetail
                    Try
                        oOtherDetail = New gloStream.CardioVascular.Supporting.OtherDetail
                        oOtherDetail.ItemID = ItemID
                        oOtherDetail.CategoryID = CategoryID
                        oOtherDetail.CategoryName = CategoryName
                        oOtherDetail.ItemName = ItemName
                        oOtherDetail.OperatorName = OperatorName
                        oOtherDetail.Result1 = Result1
                        oOtherDetail.Result2 = Result2
                        mCol.Add(oOtherDetail)
                        Add = oOtherDetail
                        oOtherDetail = Nothing
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "clsCardioVascular -- ItemDetails -- Add -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                        'UpdateLog("clsCardioVascular -- ItemDetails -- Add -- " & ex.ToString)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Add = Nothing
                    End Try
                End Function

                Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As gloStream.CardioVascular.Supporting.OtherDetail
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

            '//<<<<<<<<<<<<<< CRITERIA PARAMETERS >>>>>>>>>>>>>>>>>>//
            Public Class Criteria
                Private _ID As Long = 0
                Private _Name As String = ""
                Private _Age_Minimum As Double = 0
                Private _Age_Maximum As Double = 0
                Private _Gender As String = ""
                Private _Race As String = ""
                Private _MaritalStatus As String = ""
                Private _City As String = ""
                Private _State As String = ""
                Private _Zip As String = ""
                Private _EmployementStatus As String = ""
                Private _Height_Minimum As String = ""
                Private _Height_Maximum As String = ""
                Private _Weight_Minimum As Double = 0
                Private _Weight_Maximum As Double = 0
                Private _BMI_Minimum As Double = 0
                Private _BMI_Maximum As Double = 0
                Private _Temprature_Minimum As Double = 0
                Private _Temprature_Maximum As Double = 0
                Private _Pulse_Minimum As Double = 0
                Private _Pulse_Maximum As Double = 0
                Private _PulseOX_Minimum As Double = 0
                Private _PulseOX_Maximum As Double = 0
                Private _BPSitting_Minimum As Double = 0
                Private _BPSitting_Maximum As Double = 0
                Private _BPStanding_Minimum As Double = 0
                Private _BPStanding_Maximum As Double = 0
                Private _DisplayMessage As String = ""
                Private mOtherDetails As New OtherDetails
                Private bAssigned As Boolean = True

                Public Sub Dispose()
                    If (bAssigned) Then
                        mOtherDetails.Dispose()
                        bAssigned = False
                    End If
                End Sub
                Public Property ID() As Long
                    Get
                        Return _ID
                    End Get
                    Set(ByVal Value As Long)
                        _ID = Value
                    End Set
                End Property

                Public Property Name() As String
                    Get
                        Return _Name
                    End Get
                    Set(ByVal Value As String)
                        _Name = Value
                    End Set
                End Property

                Public Property AgeMinimum() As Double
                    Get
                        Return _Age_Minimum
                    End Get
                    Set(ByVal Value As Double)
                        _Age_Minimum = Value
                    End Set
                End Property

                Public Property AgeMaximum() As Double
                    Get
                        Return _Age_Maximum
                    End Get
                    Set(ByVal Value As Double)
                        _Age_Maximum = Value
                    End Set
                End Property

                Public Property Gender() As String
                    Get
                        Return _Gender
                    End Get
                    Set(ByVal Value As String)
                        _Gender = Value
                    End Set
                End Property

                Public Property Race() As String
                    Get
                        Return _Race
                    End Get
                    Set(ByVal Value As String)
                        _Race = Value
                    End Set
                End Property

                Public Property MaritalStatus() As String
                    Get
                        Return _MaritalStatus
                    End Get
                    Set(ByVal Value As String)
                        _MaritalStatus = Value
                    End Set
                End Property

                Public Property City() As String
                    Get
                        Return _City
                    End Get
                    Set(ByVal Value As String)
                        _City = Value
                    End Set
                End Property

                Public Property State() As String
                    Get
                        Return _State
                    End Get
                    Set(ByVal Value As String)
                        _State = Value
                    End Set
                End Property

                Public Property Zip() As String
                    Get
                        Return _Zip
                    End Get
                    Set(ByVal Value As String)
                        _Zip = Value
                    End Set
                End Property

                Public Property EmployeeStatus() As String
                    Get
                        Return _EmployementStatus
                    End Get
                    Set(ByVal Value As String)
                        _EmployementStatus = Value
                    End Set
                End Property

                Public Property HeightMinimum() As String
                    Get
                        Return _Height_Minimum
                    End Get
                    Set(ByVal Value As String)
                        _Height_Minimum = Value
                    End Set
                End Property

                Public Property HeightMaximum() As String
                    Get
                        Return _Height_Maximum
                    End Get
                    Set(ByVal Value As String)
                        _Height_Maximum = Value
                    End Set
                End Property

                Public Property WeightMinimum() As Double
                    Get
                        Return _Weight_Minimum
                    End Get
                    Set(ByVal Value As Double)
                        _Weight_Minimum = Value
                    End Set
                End Property

                Public Property WeightMaximum() As Double
                    Get
                        Return _Weight_Maximum
                    End Get
                    Set(ByVal Value As Double)
                        _Weight_Maximum = Value
                    End Set
                End Property

                Public Property BMIMinimum() As Double
                    Get
                        Return _BMI_Minimum
                    End Get
                    Set(ByVal Value As Double)
                        _BMI_Minimum = Value
                    End Set
                End Property

                Public Property BMIMaximum() As Double
                    Get
                        Return _BMI_Maximum
                    End Get
                    Set(ByVal Value As Double)
                        _BMI_Maximum = Value
                    End Set
                End Property

                Public Property TempratureMinumum() As Double
                    Get
                        Return _Temprature_Minimum
                    End Get
                    Set(ByVal Value As Double)
                        _Temprature_Minimum = Value
                    End Set
                End Property

                Public Property TempratureMaximum() As Double
                    Get
                        Return _Temprature_Maximum
                    End Get
                    Set(ByVal Value As Double)
                        _Temprature_Maximum = Value
                    End Set
                End Property

                Public Property PulseMinimum() As Double
                    Get
                        Return _Pulse_Minimum
                    End Get
                    Set(ByVal Value As Double)
                        _Pulse_Minimum = Value
                    End Set
                End Property

                Public Property PulseMaximum() As Double
                    Get
                        Return _Pulse_Maximum
                    End Get
                    Set(ByVal Value As Double)
                        _Pulse_Maximum = Value
                    End Set
                End Property

                Public Property PulseOXMinimum() As Double
                    Get
                        Return _PulseOX_Minimum
                    End Get
                    Set(ByVal Value As Double)
                        _PulseOX_Minimum = Value
                    End Set
                End Property

                Public Property PulseOXMaximum() As Double
                    Get
                        Return _PulseOX_Maximum
                    End Get
                    Set(ByVal Value As Double)
                        _PulseOX_Maximum = Value
                    End Set
                End Property

                Public Property BPSittingMinimum() As Double
                    Get
                        Return _BPSitting_Minimum
                    End Get
                    Set(ByVal Value As Double)
                        _BPSitting_Minimum = Value
                    End Set
                End Property

                Public Property BPSittingMaximum() As Double
                    Get
                        Return _BPSitting_Maximum
                    End Get
                    Set(ByVal Value As Double)
                        _BPSitting_Maximum = Value
                    End Set
                End Property

                Public Property BPStandingMinimum() As Double
                    Get
                        Return _BPStanding_Minimum
                    End Get
                    Set(ByVal Value As Double)
                        _BPStanding_Minimum = Value
                    End Set
                End Property

                Public Property BPStandingMaximum() As Double
                    Get
                        Return _BPStanding_Maximum
                    End Get
                    Set(ByVal Value As Double)
                        _BPStanding_Maximum = Value
                    End Set
                End Property

                Public Property DisplayMessage() As String
                    Get
                        Return _DisplayMessage
                    End Get
                    Set(ByVal Value As String)
                        _DisplayMessage = Value
                    End Set
                End Property

                Public Property OtherDetails() As OtherDetails
                    Get
                        Return mOtherDetails
                    End Get
                    Set(ByVal Value As OtherDetails)
                        If (bAssigned) Then
                            mOtherDetails.Dispose()
                            bAssigned = False
                        End If
                        mOtherDetails = Value
                    End Set
                End Property

                Public Sub New()
                    MyBase.new()
                    '    mOtherDetails = New gloStream.CardioVascular.Supporting.OtherDetails

                End Sub

                Protected Overrides Sub Finalize()
                    '_Histories = Nothing
                    '_Drugs = Nothing
                    '_ICD9s = Nothing
                    '_CPTs = Nothing
                    '_Labs = Nothing
                    '_Guidelines = Nothing
                    '_LabModuleTests = Nothing
                    '_Guidelines = Nothing
                    '_LabOrders = Nothing
                    '_RadiologyOrders = Nothing
                    '_RxDrugs = Nothing
                    '_Referrals = Nothing
                    MyBase.Finalize()
                End Sub
            End Class

            Public Class Criterias
                Implements System.Collections.IEnumerable
                Private mCol As Collection
                Public Sub Dispose()
                    If (IsNothing(mCol) = False) Then
                        mCol.Clear()
                        mCol = Nothing
                    End If
                End Sub
                Public Function Add(ByRef oCriteria As gloStream.CardioVascular.Supporting.Criteria) As gloStream.CardioVascular.Supporting.Criteria
                    mCol.Add(oCriteria)
                    Return Nothing
                End Function

                Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As gloStream.CardioVascular.Supporting.Criteria
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


            Public Class PatientDetail

                Private _CriteriaID As Int64 = 0
                Private _CriteriaName As String = ""
                Private _PatientID As Long = 0
                Private _PatientCode As String = ""
                Private _PatientName As String = ""
                Private _Age As Double = 0
                Private _Gender As String = ""
                Private _Race As String = ""
                Private _MaritalStatus As String = ""
                Private _City As String = ""
                Private _State As String = ""
                Private _Zip As String = ""
                Private _EmployementStatus As String = ""
                Private _Height As String = ""
                Private _Weight As Double = 0
                Private _BMI As Double = 0
                Private _Temprature As Double = 0
                Private _Pulse As Double = 0
                Private _PulseOX As Double = 0
                Private _BPSitting_Minimum As Double = 0
                Private _BPSitting_Maximum As Double = 0
                Private _BPStanding_Minimum As Double = 0
                Private _BPStanding_Maximum As Double = 0
                Private _VitalDate As Date

                Private mOtherDetails As New OtherDetails
                Private bAssigned As Boolean = True
                Public Sub Dispose()
                    If (bAssigned) Then
                        mOtherDetails.Dispose()
                        bAssigned = False
                    End If
                End Sub
                Public Sub New()
                    MyBase.New()
                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub

                Public Property CriteriaID() As Long
                    Get
                        Return _CriteriaID
                    End Get
                    Set(ByVal Value As Long)
                        _CriteriaID = Value
                    End Set
                End Property

                Public Property CriteriaName() As String
                    Get
                        Return _CriteriaName
                    End Get
                    Set(ByVal Value As String)
                        _CriteriaName = Value
                    End Set
                End Property

                Public Property PatientID() As Long
                    Get
                        Return _PatientID
                    End Get
                    Set(ByVal Value As Long)
                        _PatientID = Value
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

                Public Property PatientName() As String
                    Get
                        Return _PatientName
                    End Get
                    Set(ByVal Value As String)
                        _PatientName = Value
                    End Set
                End Property

                Public Property Age() As Double
                    Get
                        Return _Age
                    End Get
                    Set(ByVal Value As Double)
                        _Age = Value
                    End Set
                End Property

                Public Property Gender() As String
                    Get
                        Return _Gender
                    End Get
                    Set(ByVal Value As String)
                        _Gender = Value
                    End Set
                End Property

                Public Property Race() As String
                    Get
                        Return _Race
                    End Get
                    Set(ByVal Value As String)
                        _Race = Value
                    End Set
                End Property

                Public Property MaritalStatus() As String
                    Get
                        Return _MaritalStatus
                    End Get
                    Set(ByVal Value As String)
                        _MaritalStatus = Value
                    End Set
                End Property

                Public Property City() As String
                    Get
                        Return _City
                    End Get
                    Set(ByVal Value As String)
                        _City = Value
                    End Set
                End Property

                Public Property State() As String
                    Get
                        Return _State
                    End Get
                    Set(ByVal Value As String)
                        _State = Value
                    End Set
                End Property

                Public Property Zip() As String
                    Get
                        Return _Zip
                    End Get
                    Set(ByVal Value As String)
                        _Zip = Value
                    End Set
                End Property

                Public Property EmployeeStatus() As String
                    Get
                        Return _EmployementStatus
                    End Get
                    Set(ByVal Value As String)
                        _EmployementStatus = Value
                    End Set
                End Property

                Public Property Height() As String
                    Get
                        Return _Height
                    End Get
                    Set(ByVal Value As String)
                        _Height = Value
                    End Set
                End Property

                Public Property WeightInlbs() As Double
                    Get
                        Return _Weight
                    End Get
                    Set(ByVal Value As Double)
                        _Weight = Value
                    End Set
                End Property

                Public Property BMI() As Double
                    Get
                        Return _BMI
                    End Get
                    Set(ByVal Value As Double)
                        _BMI = Value
                    End Set
                End Property

                Public Property TempratureInF() As Double
                    Get
                        Return _Temprature
                    End Get
                    Set(ByVal Value As Double)
                        _Temprature = Value
                    End Set
                End Property


                Public Property Pulse() As Double
                    Get
                        Return _Pulse
                    End Get
                    Set(ByVal Value As Double)
                        _Pulse = Value
                    End Set
                End Property

                Public Property PulseOX() As Double
                    Get
                        Return _PulseOX
                    End Get
                    Set(ByVal Value As Double)
                        _PulseOX = Value
                    End Set
                End Property

                Public Property BPSittingMinimum() As Double
                    Get
                        Return _BPSitting_Minimum
                    End Get
                    Set(ByVal Value As Double)
                        _BPSitting_Minimum = Value
                    End Set
                End Property

                Public Property BPSittingMaximum() As Double
                    Get
                        Return _BPSitting_Maximum
                    End Get
                    Set(ByVal Value As Double)
                        _BPSitting_Maximum = Value
                    End Set
                End Property

                Public Property BPStandingMinimum() As Double
                    Get
                        Return _BPStanding_Minimum
                    End Get
                    Set(ByVal Value As Double)
                        _BPStanding_Minimum = Value
                    End Set
                End Property

                Public Property BPStandingMaximum() As Double
                    Get
                        Return _BPStanding_Maximum
                    End Get
                    Set(ByVal Value As Double)
                        _BPStanding_Maximum = Value
                    End Set
                End Property

                Public Property VitalDate() As Date
                    Get
                        Return _VitalDate
                    End Get
                    Set(ByVal Value As Date)
                        _VitalDate = Value
                    End Set
                End Property

                Public Property OtherDetails() As OtherDetails
                    Get
                        Return mOtherDetails
                    End Get
                    Set(ByVal Value As OtherDetails)
                        If (bAssigned) Then
                            mOtherDetails.Dispose()
                            bAssigned = False
                        End If
                        mOtherDetails = Value
                    End Set
                End Property

            End Class

            Public Class PatientDetails
                Implements System.Collections.IEnumerable
                Private mCol As Collection
                Public Sub Dispose()
                    If (IsNothing(mCol) = False) Then
                        mCol.Clear()
                        mCol = Nothing
                    End If
                End Sub
                Public Function Add(ByRef oPatientDetail As gloStream.CardioVascular.Supporting.PatientDetail) As gloStream.CardioVascular.Supporting.PatientDetail
                    mCol.Add(oPatientDetail)
                    Return Nothing
                End Function

                Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As gloStream.CardioVascular.Supporting.PatientDetail
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

        End Namespace

    End Namespace
End Namespace