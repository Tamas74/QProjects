Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports gloEMRGeneralLibrary.gloEMRDatabase

Namespace gloStream
    Namespace DiseaseManagement

        Public Class DiseaseManagement
            Implements IDisposable
            Public Overloads Sub Dispose() Implements IDisposable.Dispose
                Dispose(True)
                GC.SuppressFinalize(Me)
            End Sub

            Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
                If disposing Then
                    ' Free other state (managed objects).
                End If
                ' Free your own state (unmanaged objects).
                ' Set large fields to null.
            End Sub

            Protected Overrides Sub Finalize()
                ' Simply call Dispose(False).
                Dispose(False)
            End Sub

            Private _ErrorMessage As String
            Dim nCPTTypeID As Int16 = 1
            Dim nICDTypeID As Int16 = 2
            Private strPatientCode As String = ""
            Private strPatientFirstName As String = ""
            Private strPatientMiddleName As String = ""
            Private strPatientLastName As String = ""
            Private strPatientDOB As String = ""
            Private strPatientAge As String = ""
            Private strPatientGender As String = ""
            Private strPatientMaritalStatus As String = ""
            Private _CriteriaInProcess As Boolean
            Public Event StartCriteria(ByVal status As Boolean)
            Public Event FinishCriteria(ByVal status As Boolean, ByVal oPatients As Collection)
            Public Event ProcessCriteria(ByVal inProcess As String)
           



            Public Enum DurationType
                Day = 0
                Week = 1
                Month = 2
                Year = 3
            End Enum
            Public Enum TemplateCategoryID
                Guidelines = 0
                Labs = 1
                Radiology = 2
                Referrals = 3
                Rx = 4
                IM = 5

            End Enum
            Public Enum RecommendationStatus
                nNew = 1
                InProcess = 2
                Satisfied = 3
                Reopened = 4
                CancelAsNotAppilcable = 5
                AutoCancel = 6
                None = 7
            End Enum

            Public Enum RecommendationFlag
                Current = 1
                Past = 2
            End Enum

            Public Enum TriggerActivity
                Given = 1
                OverrideNow = 2
                OverrideLater = 3
                OverrideForever = 4
                OverridewithRecur = 5
            End Enum
            Public Enum ObjectType
                Trigger = 1
                Exception = 2
            End Enum

            Public Property CriteriaInProcess() As Boolean
                Get
                    Return _CriteriaInProcess
                End Get
                Set(ByVal Value As Boolean)
                    _CriteriaInProcess = Value
                End Set
            End Property

            Public Property ErrorMessage() As String
                Get
                    Return _ErrorMessage
                End Get
                Set(ByVal Value As String)
                    _ErrorMessage = Value
                End Set
            End Property

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
                End If
                Return 0.0
            End Function

            Public Function SaveCriteria(ByVal criteriaID As Int64, ByVal oCriteria As gloStream.DiseaseManagement.Supporting.Criteria, ByVal LoadFirst As Boolean, Optional ByVal IsCopyRule As Boolean = False) As Int64
                'Dim ODB As New gloStream.gloDataBase.gloDataBase
                Dim conn As New SqlConnection(GetConnectionString)
                Dim _nCriteriaIDforAuditTrail As Int64 = criteriaID
                'declare a transaction object
                Dim myTrans As SqlTransaction = Nothing
                Dim cmdCriteria As SqlCommand = Nothing
                Dim objparam As SqlParameter = Nothing
                Dim MachineID As Long

                Try
                    'do the validations
                    If oCriteria.Name = "" Then
                        _ErrorMessage = "Please enter the Criteria name. "
                    End If

                    conn.Open()

                    myTrans = conn.BeginTransaction

                    cmdCriteria = conn.CreateCommand
                    cmdCriteria.Transaction = myTrans


                    With cmdCriteria
                        .Connection = conn
                        .CommandType = CommandType.StoredProcedure
                        .CommandText = "DM_InsUpdCriteria"
                    End With

                    objparam = New SqlParameter("@dm_mst_Id", SqlDbType.BigInt)
                    With cmdCriteria.Parameters
                        objparam.Direction = ParameterDirection.InputOutput
                        .Add(objparam)
                        .Add("@MachineID", SqlDbType.BigInt)
                        .Add("@dm_mst_CriteriaName", SqlDbType.VarChar)
                        .Add("@dm_mst_AgeMin", SqlDbType.Decimal)
                        .Add("@dm_mst_AgeMax", SqlDbType.Decimal)
                        .Add("@dm_mst_Gender", SqlDbType.VarChar)
                        .Add("@dm_mst_Race", SqlDbType.VarChar)
                        .Add("@dm_mst_MaritalStatus", SqlDbType.VarChar)
                        .Add("@dm_mst_City", SqlDbType.VarChar)
                        .Add("@dm_mst_Status", SqlDbType.VarChar)
                        .Add("@dm_mst_Zip", SqlDbType.VarChar)
                        .Add("@dm_mst_EmplyementStatus", SqlDbType.VarChar)
                        .Add("@dm_mst_HeightMin", SqlDbType.VarChar)
                        .Add("@dm_mst_HeightMax", SqlDbType.VarChar)
                        .Add("@dm_mst_WeightMin", SqlDbType.Decimal)
                        .Add("@dm_mst_WeightMax", SqlDbType.Decimal)
                        .Add("@dm_mst_BMIMin", SqlDbType.Decimal)
                        .Add("@dm_mst_BMIMax", SqlDbType.Decimal)
                        .Add("@dm_mst_TemperatureMin", SqlDbType.Decimal)
                        .Add("@dm_mst_TemperatureMax", SqlDbType.Decimal)
                        .Add("@dm_mst_PulseMin", SqlDbType.Decimal)
                        .Add("@dm_mst_PulseMax", SqlDbType.Decimal)
                        .Add("@dm_mst_PulseOxMin", SqlDbType.Decimal)
                        .Add("@dm_mst_PulseOxMax", SqlDbType.Decimal)
                        .Add("@dm_mst_BPSittingMin", SqlDbType.Decimal)
                        .Add("@dm_mst_BPSittingMax", SqlDbType.Decimal)
                        .Add("@dm_mst_BPStandingMin", SqlDbType.Decimal)
                        .Add("@dm_mst_BPStandingMax", SqlDbType.Decimal)
                        .Add("@dm_mst_DisplayMessage", SqlDbType.VarChar)
                        .Add("@dm_mst_PatientID", SqlDbType.BigInt)
                        .Add("@dm_mst_OriginalID", SqlDbType.BigInt)
                        .Add("@nUserID", SqlDbType.BigInt)
                        .Add("@sUserName", SqlDbType.VarChar)
                        .Add("@bIsActive", SqlDbType.Bit)
                        .Add("@bIsRecurringRule", SqlDbType.Bit)
                        .Add("@dtRecurrenceStartDate", SqlDbType.Date)
                        .Add("@dtRecurrenceEndDate", SqlDbType.Date)
                        .Add("@nDurationType", SqlDbType.TinyInt)
                        .Add("@nDurationPeriod", SqlDbType.Int)
                        .Add("@dm_mst_BPSittingToMin", SqlDbType.Decimal)
                        .Add("@dm_mst_BPSittingToMax", SqlDbType.Decimal)
                        .Add("@dm_mst_BPStandingToMin", SqlDbType.Decimal)
                        .Add("@dm_mst_BPStandingToMax", SqlDbType.Decimal)

                        .Add("@dm_mst_sBibliographicCitatation", SqlDbType.VarChar)
                        .Add("@dm_mst_sInterventionDeveloper", SqlDbType.VarChar)
                        .Add("@dm_mst_sFundingSource", SqlDbType.VarChar)
                        .Add("@dm_mst_sRelease", SqlDbType.VarChar)
                        .Add("@dm_mst_sRevisionDates", SqlDbType.VarChar)

                        'Added by Sameer On 19 Sept 2013 for Special Alert CheckBox in DM Rule Setup 
                        .Add("@bIsSpecialAlert", SqlDbType.Bit)

                    End With

                    With cmdCriteria
                        MachineID = GetPrefixTransactionID()

                        objparam.Value = criteriaID
                        '.Parameters("@dm_mst_Id").Value = Criteria_ID
                        .Parameters("@MachineID").Value = MachineID
                        .Parameters("@dm_mst_CriteriaName").Value = oCriteria.Name
                        '' SUDHIR 20090309 - NEW FIELDS
                        .Parameters("@dm_mst_PatientID").Value = 0
                        .Parameters("@dm_mst_OriginalID").Value = 0
                        '' ''
                        .Parameters("@dm_mst_AgeMin").Value = oCriteria.AgeMinimum
                        .Parameters("@dm_mst_AgeMax").Value = oCriteria.AgeMaximum
                        .Parameters("@dm_mst_Gender").Value = oCriteria.Gender
                        .Parameters("@dm_mst_Race").Value = oCriteria.Race
                        .Parameters("@dm_mst_MaritalStatus").Value = oCriteria.MaritalStatus
                        .Parameters("@dm_mst_City").Value = oCriteria.City
                        .Parameters("@dm_mst_Status").Value = oCriteria.State
                        .Parameters("@dm_mst_Zip").Value = oCriteria.Zip
                        .Parameters("@dm_mst_EmplyementStatus").Value = oCriteria.EmployeeStatus
                        .Parameters("@dm_mst_HeightMin").Value = oCriteria.HeightMinimum
                        .Parameters("@dm_mst_HeightMax").Value = oCriteria.HeightMaximum
                        If oCriteria.WeightMinimum = 0.0 Then
                            .Parameters("@dm_mst_WeightMin").Value = System.DBNull.Value
                        Else
                            .Parameters("@dm_mst_WeightMin").Value = oCriteria.WeightMinimum
                        End If

                        If oCriteria.WeightMaximum = 0.0 Then
                            .Parameters("@dm_mst_WeightMax").Value = System.DBNull.Value
                        Else
                            .Parameters("@dm_mst_WeightMax").Value = oCriteria.WeightMaximum
                        End If

                        If oCriteria.BMIMinimum = 0.0 Then
                            .Parameters("@dm_mst_BMIMin").Value = System.DBNull.Value
                        Else
                            .Parameters("@dm_mst_BMIMin").Value = oCriteria.BMIMinimum
                        End If

                        If oCriteria.BMIMaximum = 0.0 Then
                            .Parameters("@dm_mst_BMIMax").Value = System.DBNull.Value
                        Else
                            .Parameters("@dm_mst_BMIMax").Value = oCriteria.BMIMaximum
                        End If

                        If oCriteria.PulseMinimum = 0.0 Then
                            .Parameters("@dm_mst_PulseMin").Value = System.DBNull.Value
                        Else
                            .Parameters("@dm_mst_PulseMin").Value = oCriteria.PulseMinimum
                        End If

                        If oCriteria.PulseMaximum = 0.0 Then
                            .Parameters("@dm_mst_PulseMax").Value = System.DBNull.Value
                        Else
                            .Parameters("@dm_mst_PulseMax").Value = oCriteria.PulseMaximum
                        End If

                        If oCriteria.BPSittingMinimum = 0.0 Then
                            .Parameters("@dm_mst_BPSittingMin").Value = System.DBNull.Value
                        Else
                            .Parameters("@dm_mst_BPSittingMin").Value = oCriteria.BPSittingMinimum
                        End If

                        If oCriteria.BPSittingMaximum = 0.0 Then
                            .Parameters("@dm_mst_BPSittingMax").Value = System.DBNull.Value
                        Else
                            .Parameters("@dm_mst_BPSittingMax").Value = oCriteria.BPSittingMaximum
                        End If

                        If oCriteria.BPStandingMinimum = 0.0 Then
                            .Parameters("@dm_mst_BPStandingMin").Value = System.DBNull.Value
                        Else
                            .Parameters("@dm_mst_BPStandingMin").Value = oCriteria.BPStandingMinimum
                        End If

                        If oCriteria.BPStandingMaximum = 0.0 Then
                            .Parameters("@dm_mst_BPStandingMax").Value = System.DBNull.Value
                        Else
                            .Parameters("@dm_mst_BPStandingMax").Value = oCriteria.BPStandingMaximum
                        End If

                        .Parameters("@dm_mst_DisplayMessage").Value = oCriteria.DisplayMessage
                        If oCriteria.PulseOXMinimum = 0.0 Then
                            .Parameters("@dm_mst_PulseOxMin").Value = System.DBNull.Value
                        Else
                            .Parameters("@dm_mst_PulseOxMin").Value = oCriteria.PulseOXMinimum
                        End If

                        If oCriteria.PulseOXMaximum = 0.0 Then
                            .Parameters("@dm_mst_PulseOxMax").Value = System.DBNull.Value
                        Else
                            .Parameters("@dm_mst_PulseOxMax").Value = oCriteria.PulseOXMaximum
                        End If

                        If oCriteria.TempratureMinumum = 0.0 Then
                            .Parameters("@dm_mst_TemperatureMin").Value = System.DBNull.Value
                        Else
                            .Parameters("@dm_mst_TemperatureMin").Value = oCriteria.TempratureMinumum
                        End If

                        If oCriteria.TempratureMaximum = 0.0 Then
                            .Parameters("@dm_mst_TemperatureMax").Value = System.DBNull.Value
                        Else
                            .Parameters("@dm_mst_TemperatureMax").Value = oCriteria.TempratureMaximum
                        End If

                        .Parameters("@nUserID").Value = mdlGeneral.gnLoginID
                        .Parameters("@sUserName").Value = mdlGeneral.gstrLoginName
                        .Parameters("@bIsActive").Value = oCriteria.IsActive
                        .Parameters("@bIsRecurringRule").Value = oCriteria.bIsRecuringRule

                        If oCriteria.dtRecurrenceStartDate <> Date.MinValue Then
                            .Parameters("@dtRecurrenceStartDate").Value = oCriteria.dtRecurrenceStartDate
                        Else
                            .Parameters("@dtRecurrenceStartDate").Value = System.DBNull.Value
                        End If

                        If oCriteria.dtRecurrenceEndDate <> Date.MinValue Then
                            .Parameters("@dtRecurrenceEndDate").Value = oCriteria.dtRecurrenceEndDate
                        Else
                            .Parameters("@dtRecurrenceEndDate").Value = System.DBNull.Value
                        End If
                        If oCriteria.nDuratiotype <> -1 Then
                            .Parameters("@nDurationType").Value = oCriteria.nDuratiotype
                        Else
                            .Parameters("@nDurationType").Value = System.DBNull.Value
                        End If


                        .Parameters("@nDurationPeriod").Value = oCriteria.nDuratioPeriod

                        If oCriteria.BPSittingToMinimum = 0.0 Then
                            .Parameters("@dm_mst_BPSittingToMin").Value = System.DBNull.Value
                        Else
                            .Parameters("@dm_mst_BPSittingToMin").Value = oCriteria.BPSittingToMinimum
                        End If

                        If oCriteria.BPSittingToMaximum = 0.0 Then
                            .Parameters("@dm_mst_BPSittingToMax").Value = System.DBNull.Value
                        Else
                            .Parameters("@dm_mst_BPSittingToMax").Value = oCriteria.BPSittingToMaximum
                        End If

                        If oCriteria.BPStandingToMinimum = 0.0 Then
                            .Parameters("@dm_mst_BPStandingToMin").Value = System.DBNull.Value
                        Else
                            .Parameters("@dm_mst_BPStandingToMin").Value = oCriteria.BPStandingToMinimum
                        End If

                        If oCriteria.BPStandingToMaximum = 0.0 Then
                            .Parameters("@dm_mst_BPStandingToMax").Value = System.DBNull.Value
                        Else
                            .Parameters("@dm_mst_BPStandingToMax").Value = oCriteria.BPStandingToMaximum
                        End If

                        .Parameters("@dm_mst_sBibliographicCitatation").Value = oCriteria.sBibliographicCitatation
                        .Parameters("@dm_mst_sInterventionDeveloper").Value = oCriteria.sInterventionDeveloper
                        .Parameters("@dm_mst_sFundingSource").Value = oCriteria.sFundingSource
                        .Parameters("@dm_mst_sRelease").Value = oCriteria.sRelease
                        .Parameters("@dm_mst_sRevisionDates").Value = oCriteria.sRevisionDates

                        'Added by Sameer On 19 Sept 2013 for Special Alert CheckBox in DM Rule Setup 
                        .Parameters("@bIsSpecialAlert").Value = oCriteria.bIsSpecialAlert
                    End With

                    cmdCriteria.ExecuteNonQuery()


                    If Not IsNothing(objparam) Then
                        criteriaID = objparam.Value
                        '//MsgBox(Criteria_ID)
                    End If

                    If criteriaID > 0 Then


                        ''Delete All Records from OtherDetail Table & Insert Updated Data

                        cmdCriteria.Connection = conn

                        
                        cmdCriteria.Parameters.Clear()
                        cmdCriteria.CommandType = CommandType.StoredProcedure
                        cmdCriteria.CommandText = "DM_DeleteCriteriaTemplates"
                        cmdCriteria.Parameters.Add("@dm_mst_Id", SqlDbType.BigInt)
                        cmdCriteria.Parameters("@dm_mst_Id").Value = criteriaID
                        cmdCriteria.ExecuteNonQuery()
                        ''  End If

                        ''END OF DELETING RECORDS ''

                        cmdCriteria.Parameters.Clear()
                        cmdCriteria.CommandType = CommandType.StoredProcedure
                        cmdCriteria.CommandText = "DM_InCriteriaDTL"

                        ''Insert All OtherDetails.
                        For i As Integer = 1 To oCriteria.OtherDetails.Count
                            cmdCriteria.Parameters.Add("@dm_mst_Id", SqlDbType.BigInt)
                            cmdCriteria.Parameters.Add("@dm_dtl_Id", SqlDbType.BigInt)
                            cmdCriteria.Parameters.Add("@MachineID", SqlDbType.BigInt)
                            cmdCriteria.Parameters.Add("@dm_dtl_CategoryName", SqlDbType.VarChar)
                            cmdCriteria.Parameters.Add("@dm_dtl_ItemName", SqlDbType.VarChar)
                            cmdCriteria.Parameters.Add("@dm_dtl_Operator", SqlDbType.VarChar)
                            cmdCriteria.Parameters.Add("@dm_dtl_ResultValue1", SqlDbType.VarChar)
                            cmdCriteria.Parameters.Add("@dm_dtl_ResultValue2", SqlDbType.VarChar)
                            cmdCriteria.Parameters.Add("@dm_dtl_Type", SqlDbType.Int)
                            cmdCriteria.Parameters.Add("@dm_dtl_LionicCode", SqlDbType.VarChar)

                            MachineID = GetPrefixTransactionID()
                            cmdCriteria.Parameters("@dm_mst_Id").Value = criteriaID
                            cmdCriteria.Parameters("@dm_dtl_Id").Value = 0
                            cmdCriteria.Parameters("@MachineID").Value = MachineID
                            cmdCriteria.Parameters("@dm_dtl_CategoryName").Value = oCriteria.OtherDetails(i).CategoryName
                            cmdCriteria.Parameters("@dm_dtl_ItemName").Value = oCriteria.OtherDetails(i).ItemName
                            If Not IsNothing(oCriteria.OtherDetails(i).OperatorName) Then
                                cmdCriteria.Parameters("@dm_dtl_Operator").Value = oCriteria.OtherDetails(i).OperatorName
                            Else
                                cmdCriteria.Parameters("@dm_dtl_Operator").Value = ""
                            End If
                            'cmdCriteria.Parameters("@dm_dtl_Operator").Value = oCriteria.OtherDetails(i).OperatorName
                            cmdCriteria.Parameters("@dm_dtl_ResultValue1").Value = oCriteria.OtherDetails(i).Result1
                            cmdCriteria.Parameters("@dm_dtl_ResultValue2").Value = oCriteria.OtherDetails(i).Result2
                            cmdCriteria.Parameters("@dm_dtl_Type").Value = oCriteria.OtherDetails(i).DetailType.GetHashCode
                            cmdCriteria.Parameters("@dm_dtl_LionicCode").Value = oCriteria.OtherDetails(i).LionicCode

                            cmdCriteria.ExecuteNonQuery()
                            cmdCriteria.Parameters.Clear()
                        Next

                        ''END OF OTHER DETAILS


                        ''insert Orders into Template_MST table
                        cmdCriteria.Parameters.Clear()
                        cmdCriteria.CommandText = "DM_InsTemplates_DTL"
                        For i As Integer = 1 To oCriteria.LabOrders.Count

                            cmdCriteria.Parameters.Add("@Criteria_ID", SqlDbType.BigInt)
                            cmdCriteria.Parameters("@Criteria_ID").Value = criteriaID

                            cmdCriteria.Parameters.Add("@OrderID", SqlDbType.BigInt)
                            cmdCriteria.Parameters("@OrderID").Value = CType(oCriteria.LabOrders.Item(i), myList).ID   'Item(i).TestID

                            cmdCriteria.Parameters.Add("@OrderType", SqlDbType.BigInt)
                            cmdCriteria.Parameters("@OrderType").Value = TemplateCategoryID.Labs

                            'sarika DM Denormalization 20090331
                            cmdCriteria.Parameters.Add("@TemplateName", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@TemplateName").Value = CType(oCriteria.LabOrders.Item(i), myList).Value

                            cmdCriteria.Parameters.Add("@Template", SqlDbType.Image)
                            cmdCriteria.Parameters("@Template").Value = Nothing

                            cmdCriteria.Parameters.Add("@dm_Templatedtl_TemplateDtlInfo", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@dm_Templatedtl_TemplateDtlInfo").Value = ""

                            '--


                            'sarika DM Denormalization for Rx on 20090410

                            cmdCriteria.Parameters.Add("@sDrugForm", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@sDrugForm").Value = "" 'CType(oCriteria.LabOrders.Item(i), myList).DrugForm

                            cmdCriteria.Parameters.Add("@sRoute", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@sRoute").Value = ""

                            cmdCriteria.Parameters.Add("@sFrequency", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@sFrequency").Value = ""

                            cmdCriteria.Parameters.Add("@sNDCCode", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@sNDCCode").Value = ""

                            cmdCriteria.Parameters.Add("@nIsNarcotics", SqlDbType.Int)
                            cmdCriteria.Parameters("@nIsNarcotics").Value = 0

                            cmdCriteria.Parameters.Add("@mpid", SqlDbType.Int)
                            cmdCriteria.Parameters("@mpid").Value = 0

                            cmdCriteria.Parameters.Add("@sDuration", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@sDuration").Value = ""


                            cmdCriteria.Parameters.Add("@sDrugQtyQualifier", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@sDrugQtyQualifier").Value = ""
                            '----

                            cmdCriteria.ExecuteNonQuery()
                            cmdCriteria.Parameters.Clear()
                        Next

                        cmdCriteria.Parameters.Clear()
                        cmdCriteria.CommandText = "DM_InsTemplates_DTL"
                        For i As Integer = 1 To oCriteria.RadiologyOrders.Count

                            cmdCriteria.Parameters.Add("@Criteria_ID", SqlDbType.BigInt)
                            cmdCriteria.Parameters("@Criteria_ID").Value = criteriaID

                            cmdCriteria.Parameters.Add("@OrderID", SqlDbType.BigInt)
                            cmdCriteria.Parameters("@OrderID").Value = CType(oCriteria.RadiologyOrders.Item(i), myList).ID 'Item(i).TestID

                            cmdCriteria.Parameters.Add("@OrderType", SqlDbType.BigInt)
                            cmdCriteria.Parameters("@OrderType").Value = TemplateCategoryID.Radiology

                            'sarika DM Denormalization 20090331
                            cmdCriteria.Parameters.Add("@TemplateName", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@TemplateName").Value = CType(oCriteria.RadiologyOrders.Item(i), myList).Value

                            cmdCriteria.Parameters.Add("@Template", SqlDbType.Image)
                            cmdCriteria.Parameters("@Template").Value = Nothing

                            cmdCriteria.Parameters.Add("@dm_Templatedtl_TemplateDtlInfo", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@dm_Templatedtl_TemplateDtlInfo").Value = ""
                            '--


                            'sarika DM Denormalization for Rx on 20090410

                            cmdCriteria.Parameters.Add("@sDrugForm", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@sDrugForm").Value = ""

                            cmdCriteria.Parameters.Add("@sRoute", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@sRoute").Value = ""

                            cmdCriteria.Parameters.Add("@sFrequency", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@sFrequency").Value = ""

                            cmdCriteria.Parameters.Add("@sNDCCode", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@sNDCCode").Value = ""

                            cmdCriteria.Parameters.Add("@nIsNarcotics", SqlDbType.Int)
                            cmdCriteria.Parameters("@nIsNarcotics").Value = 0

                            cmdCriteria.Parameters.Add("@mpid", SqlDbType.Int)
                            cmdCriteria.Parameters("@mpid").Value = 0

                            cmdCriteria.Parameters.Add("@sDuration", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@sDuration").Value = ""



                            cmdCriteria.Parameters.Add("@sDrugQtyQualifier", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@sDrugQtyQualifier").Value = ""
                            '----



                            cmdCriteria.ExecuteNonQuery()
                            cmdCriteria.Parameters.Clear()
                        Next
                        cmdCriteria.Parameters.Clear()
                        cmdCriteria.CommandText = "DM_InsTemplates_DTL"

                        For i As Integer = 1 To oCriteria.Referrals.Count

                            cmdCriteria.Parameters.Add("@Criteria_ID", SqlDbType.BigInt)
                            cmdCriteria.Parameters("@Criteria_ID").Value = criteriaID

                            cmdCriteria.Parameters.Add("@OrderID", SqlDbType.BigInt)
                            cmdCriteria.Parameters("@OrderID").Value = CType(oCriteria.Referrals.Item(i), myList).ID   'Item(i).TestID

                            cmdCriteria.Parameters.Add("@OrderType", SqlDbType.BigInt)
                            cmdCriteria.Parameters("@OrderType").Value = TemplateCategoryID.Referrals

                            'sarika DM Denormalization 20090331
                            cmdCriteria.Parameters.Add("@TemplateName", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@TemplateName").Value = CType(oCriteria.Referrals.Item(i), myList).Value

                            cmdCriteria.Parameters.Add("@Template", SqlDbType.Image)
                            cmdCriteria.Parameters("@Template").Value = Nothing

                            cmdCriteria.Parameters.Add("@dm_Templatedtl_TemplateDtlInfo", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@dm_Templatedtl_TemplateDtlInfo").Value = ""
                            '--


                            'sarika DM Denormalization for Rx on 20090410

                            cmdCriteria.Parameters.Add("@sDrugForm", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@sDrugForm").Value = ""

                            cmdCriteria.Parameters.Add("@sRoute", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@sRoute").Value = ""

                            cmdCriteria.Parameters.Add("@sFrequency", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@sFrequency").Value = ""

                            cmdCriteria.Parameters.Add("@sNDCCode", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@sNDCCode").Value = ""

                            cmdCriteria.Parameters.Add("@nIsNarcotics", SqlDbType.Int)
                            cmdCriteria.Parameters("@nIsNarcotics").Value = 0

                            cmdCriteria.Parameters.Add("@mpid", SqlDbType.Int)
                            cmdCriteria.Parameters("@mpid").Value = 0

                            cmdCriteria.Parameters.Add("@sDuration", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@sDuration").Value = ""


                            cmdCriteria.Parameters.Add("@sDrugQtyQualifier", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@sDrugQtyQualifier").Value = ""
                            '----


                            cmdCriteria.ExecuteNonQuery()
                            cmdCriteria.Parameters.Clear()
                        Next
                        cmdCriteria.Parameters.Clear()
                        cmdCriteria.CommandText = "DM_InsTemplates_DTL"
                        For i As Integer = 1 To oCriteria.Guidelines.Count

                            cmdCriteria.Parameters.Add("@Criteria_ID", SqlDbType.BigInt)
                            cmdCriteria.Parameters("@Criteria_ID").Value = criteriaID

                            cmdCriteria.Parameters.Add("@OrderID", SqlDbType.BigInt)
                            cmdCriteria.Parameters("@OrderID").Value = CType(oCriteria.Guidelines.Item(i), myList).ID
                            'Item(i).TestID

                            cmdCriteria.Parameters.Add("@OrderType", SqlDbType.BigInt)
                            cmdCriteria.Parameters("@OrderType").Value = TemplateCategoryID.Guidelines

                            'sarika DM Denormalization 20090331
                            cmdCriteria.Parameters.Add("@TemplateName", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@TemplateName").Value = CType(oCriteria.Guidelines.Item(i), myList).DMTemplateName

                            cmdCriteria.Parameters.Add("@Template", SqlDbType.Image)
                            If Not IsNothing(CType(oCriteria.Guidelines.Item(i), myList).DMTemplate) = True AndAlso Not IsDBNull(CType(oCriteria.Guidelines.Item(i), myList).DMTemplate) Then
                                ' Dim img As Byte()
                                'img = CType(oCriteria.Guidelines.Item(i), myList).DMTemplate
                                cmdCriteria.Parameters("@Template").Value = CType((CType(oCriteria.Guidelines.Item(i), myList).DMTemplate), Byte()).Clone()
                                'img = Nothing
                            Else
                                cmdCriteria.Parameters("@Template").Value = DBNull.Value

                            End If

                            cmdCriteria.Parameters.Add("@dm_Templatedtl_TemplateDtlInfo", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@dm_Templatedtl_TemplateDtlInfo").Value = "" ''guideline category
                            '--



                            'sarika DM Denormalization for Rx on 20090410

                            cmdCriteria.Parameters.Add("@sDrugForm", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@sDrugForm").Value = ""

                            cmdCriteria.Parameters.Add("@sRoute", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@sRoute").Value = ""

                            cmdCriteria.Parameters.Add("@sFrequency", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@sFrequency").Value = ""

                            cmdCriteria.Parameters.Add("@sNDCCode", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@sNDCCode").Value = ""

                            cmdCriteria.Parameters.Add("@nIsNarcotics", SqlDbType.Int)
                            cmdCriteria.Parameters("@nIsNarcotics").Value = 0

                            cmdCriteria.Parameters.Add("@mpid", SqlDbType.Int)
                            cmdCriteria.Parameters("@mpid").Value = 0

                            cmdCriteria.Parameters.Add("@sDuration", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@sDuration").Value = ""


                            cmdCriteria.Parameters.Add("@sDrugQtyQualifier", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@sDrugQtyQualifier").Value = ""
                            '----


                            cmdCriteria.ExecuteNonQuery()
                            cmdCriteria.Parameters.Clear()
                        Next
                        cmdCriteria.Parameters.Clear()
                        cmdCriteria.CommandText = "DM_InsTemplates_DTL"
                        For i As Integer = 1 To oCriteria.RxDrugs.Count

                            cmdCriteria.Parameters.Add("@Criteria_ID", SqlDbType.BigInt)
                            cmdCriteria.Parameters("@Criteria_ID").Value = criteriaID

                            cmdCriteria.Parameters.Add("@OrderID", SqlDbType.BigInt)
                            cmdCriteria.Parameters("@OrderID").Value = CType(oCriteria.RxDrugs.Item(i), myList).ID   'Item(i).TestID

                            cmdCriteria.Parameters.Add("@OrderType", SqlDbType.BigInt)
                            cmdCriteria.Parameters("@OrderType").Value = TemplateCategoryID.Rx

                            'sarika DM Denormalization 20090331
                            cmdCriteria.Parameters.Add("@TemplateName", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@TemplateName").Value = CType(oCriteria.RxDrugs.Item(i), myList).DrugName

                            cmdCriteria.Parameters.Add("@Template", SqlDbType.Image)
                            cmdCriteria.Parameters("@Template").Value = Nothing

                            cmdCriteria.Parameters.Add("@dm_Templatedtl_TemplateDtlInfo", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@dm_Templatedtl_TemplateDtlInfo").Value = CType(oCriteria.RxDrugs.Item(i), myList).Dosage


                            '--


                            'sarika DM Denormalization for Rx on 20090410

                            cmdCriteria.Parameters.Add("@sDrugForm", SqlDbType.VarChar)
                            If Not IsNothing(CType(oCriteria.RxDrugs.Item(i), myList).DrugForm) Then
                                cmdCriteria.Parameters("@sDrugForm").Value = CType(oCriteria.RxDrugs.Item(i), myList).DrugForm
                            Else
                                cmdCriteria.Parameters("@sDrugForm").Value = ""
                            End If


                            cmdCriteria.Parameters.Add("@sRoute", SqlDbType.VarChar)
                            If Not IsNothing(CType(oCriteria.RxDrugs.Item(i), myList).Route) Then
                                cmdCriteria.Parameters("@sRoute").Value = CType(oCriteria.RxDrugs.Item(i), myList).Route
                            Else
                                cmdCriteria.Parameters("@sRoute").Value = ""
                            End If


                            cmdCriteria.Parameters.Add("@sFrequency", SqlDbType.VarChar)
                            If Not IsNothing(CType(oCriteria.RxDrugs.Item(i), myList).Frequency) Then
                                cmdCriteria.Parameters("@sFrequency").Value = CType(oCriteria.RxDrugs.Item(i), myList).Frequency
                            Else
                                cmdCriteria.Parameters("@sFrequency").Value = ""
                            End If


                            cmdCriteria.Parameters.Add("@sNDCCode", SqlDbType.VarChar)
                            If Not IsNothing(CType(oCriteria.RxDrugs.Item(i), myList).NDCCode) Then
                                cmdCriteria.Parameters("@sNDCCode").Value = CType(oCriteria.RxDrugs.Item(i), myList).NDCCode
                            Else
                                cmdCriteria.Parameters("@sNDCCode").Value = ""
                            End If

                            cmdCriteria.Parameters.Add("@nIsNarcotics", SqlDbType.Int)
                            If Not IsNothing(CType(oCriteria.RxDrugs.Item(i), myList).IsNarcotic) Then
                                cmdCriteria.Parameters("@nIsNarcotics").Value = CType(oCriteria.RxDrugs.Item(i), myList).IsNarcotic
                            Else
                                cmdCriteria.Parameters("@nIsNarcotics").Value = 0
                            End If

                            cmdCriteria.Parameters.Add("@mpid", SqlDbType.Int)
                            If Not IsNothing(CType(oCriteria.RxDrugs.Item(i), myList).mpid) Then
                                cmdCriteria.Parameters("@mpid").Value = CType(oCriteria.RxDrugs.Item(i), myList).mpid
                            Else
                                cmdCriteria.Parameters("@mpid").Value = 0
                            End If

                            cmdCriteria.Parameters.Add("@sDuration", SqlDbType.VarChar)
                            If Not IsNothing(CType(oCriteria.RxDrugs.Item(i), myList).Duration) Then
                                cmdCriteria.Parameters("@sDuration").Value = CType(oCriteria.RxDrugs.Item(i), myList).Duration
                            Else
                                cmdCriteria.Parameters("@sDuration").Value = ""
                            End If



                            cmdCriteria.Parameters.Add("@sDrugQtyQualifier", SqlDbType.VarChar)
                            If Not IsNothing(CType(oCriteria.RxDrugs.Item(i), myList).DrugQtyQualifier) Then
                                cmdCriteria.Parameters("@sDrugQtyQualifier").Value = CType(oCriteria.RxDrugs.Item(i), myList).DrugQtyQualifier
                            Else
                                cmdCriteria.Parameters("@sDrugQtyQualifier").Value = ""
                            End If


                            '----


                            cmdCriteria.ExecuteNonQuery()
                            cmdCriteria.Parameters.Clear()
                        Next


                        '' Chetan integrated on 09 Oct 2010 for IM in DM Setup

                        cmdCriteria.Parameters.Clear()
                        cmdCriteria.CommandText = "DM_InsTemplates_DTL"
                        For i As Integer = 1 To oCriteria.IMlst.Count

                            cmdCriteria.Parameters.Add("@Criteria_ID", SqlDbType.BigInt)
                            cmdCriteria.Parameters("@Criteria_ID").Value = criteriaID


                            cmdCriteria.Parameters.Add("@OrderID", SqlDbType.BigInt)
                            cmdCriteria.Parameters("@OrderID").Value = CType(oCriteria.IMlst.Item(i), myList).ID                                ' IM ID

                            cmdCriteria.Parameters.Add("@OrderType", SqlDbType.BigInt)
                            cmdCriteria.Parameters("@OrderType").Value = TemplateCategoryID.IM          'Enum - IM


                            cmdCriteria.Parameters.Add("@TemplateName", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@TemplateName").Value = CType(oCriteria.IMlst.Item(i), myList).Value

                            cmdCriteria.Parameters.Add("@Template", SqlDbType.Image)
                            cmdCriteria.Parameters("@Template").Value = Nothing
                            ''If Not IsNothing(CType(oCriteria.Guidelines.Item(i), myList).DMTemplate) = True Then
                            ''    Dim img As Byte()
                            ''    img = CType(oCriteria.Guidelines.Item(i), myList).DMTemplate
                            ''    cmdCriteria.Parameters("@Template").Value = img
                            ''    img = Nothing
                            ''Else
                            ''    cmdCriteria.Parameters("@Template").Value = Nothing
                            ''End If

                            '--
                            cmdCriteria.Parameters.Add("@sDrugForm", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@sDrugForm").Value = CType(oCriteria.IMlst.Item(i), myList).DrugForm     'ConceptID

                            cmdCriteria.Parameters.Add("@sDuration", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@sDuration").Value = CType(oCriteria.IMlst.Item(i), myList).Duration     'DescriptionID

                            cmdCriteria.Parameters.Add("@sFrequency", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@sFrequency").Value = CType(oCriteria.IMlst.Item(i), myList).Frequency     'SnoMedID


                            cmdCriteria.Parameters.Add("@sDrugQtyQualifier", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@sDrugQtyQualifier").Value = CType(oCriteria.IMlst.Item(i), myList).DrugQtyQualifier     'Item Count
                            '''''''''''''''''''''''''''''''''''


                            cmdCriteria.Parameters.Add("@sRoute", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@sRoute").Value = CType(oCriteria.IMlst.Item(i), myList).Route 'Orignal Vaccine name


                            cmdCriteria.Parameters.Add("@sNDCCode", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@sNDCCode").Value = CType(oCriteria.IMlst.Item(i), myList).NDCCode

                            cmdCriteria.Parameters.Add("@nIsNarcotics", SqlDbType.Int)
                            cmdCriteria.Parameters("@nIsNarcotics").Value = 0

                            cmdCriteria.Parameters.Add("@mpid", SqlDbType.Int)
                            cmdCriteria.Parameters("@mpid").Value = CType(oCriteria.IMlst.Item(i), myList).mpid

                            cmdCriteria.Parameters.Add("@dm_Templatedtl_TemplateDtlInfo", SqlDbType.VarChar)
                            cmdCriteria.Parameters("@dm_Templatedtl_TemplateDtlInfo").Value = ""
                            '----


                            cmdCriteria.ExecuteNonQuery()
                            cmdCriteria.Parameters.Clear()
                        Next
                        '' Chetan integrated on 09 Oct 2010 for IM in DM Setup

                        myTrans.Commit()

                        ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "'" & oCriteria.Name & "' Disease Management Criteria Added", gloAuditTrail.ActivityOutCome.Success)
                        ''Added Rahul P on 20101009
                        ''  gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "'" & oCriteria.Name & "' Disease Management Criteria Added", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        ''
                        'Dim objAudit As New clsAudit
                        'objAudit.CreateLog(clsAudit.enmActivityType.Add, "'" & oCriteria.Name & "' Disease Management Criteria Added", gstrLoginName, 0)
                        'objAudit = Nothing
                        If _nCriteriaIDforAuditTrail > 0 Then

                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DM_RuleSetup, gloAuditTrail.ActivityCategory.DM_RuleSetup, gloAuditTrail.ActivityType.Modify, "'" & oCriteria.Name & "' successfully Modified", 0, criteriaID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                        Else

                            If IsCopyRule Then
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DM_RuleSetup, gloAuditTrail.ActivityCategory.DM_RuleSetup, gloAuditTrail.ActivityType.Copy, "'" & oCriteria.Name & "' successfully Copied", 0, criteriaID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            Else
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DM_RuleSetup, gloAuditTrail.ActivityCategory.DM_RuleSetup, gloAuditTrail.ActivityType.Add, "'" & oCriteria.Name & "' successfully Added", 0, criteriaID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            End If


                        End If

                        Return criteriaID

                    Else
                        myTrans.Rollback()
                        Return Nothing
                    End If

                Catch ex As Exception
                    'if some error occur when inserting in any of the tables then all the transactions are rollbacked
                    Try
                        myTrans.Rollback()
                    Catch ex1 As SqlException
                        If Not myTrans.Connection Is Nothing Then
                            'Console.WriteLine("An exception of type " & ex1.GetType().ToString() & _
                            '" was encountered while attempting to roll back the transaction.")
                            '' gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "clsDiseaseManagement -- Add -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                            'UpdateLog("clsDiseaseManagement -- Add -- " & ex.ToString)
                            _ErrorMessage = ex.Message
                        End If
                        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                    'UpdateLog("clsDiseaseManagement -- Add -- " & ex.ToString)
                    _ErrorMessage = ex.Message
                    _ErrorMessage = "Neither record was written to database."
                    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

                    If _nCriteriaIDforAuditTrail > 0 Then

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DM_RuleSetup, gloAuditTrail.ActivityCategory.DM_RuleSetup, gloAuditTrail.ActivityType.Modify, "'" & oCriteria.Name & "' unsuccessfully Modified", 0, criteriaID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)

                    Else

                        If IsCopyRule Then
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DM_RuleSetup, gloAuditTrail.ActivityCategory.DM_RuleSetup, gloAuditTrail.ActivityType.Copy, "'" & oCriteria.Name & "' unsuccessfully Copied", 0, criteriaID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                        Else
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DM_RuleSetup, gloAuditTrail.ActivityCategory.DM_RuleSetup, gloAuditTrail.ActivityType.Add, "'" & oCriteria.Name & "' unsuccessfully Added", 0, criteriaID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                        End If
                    End If
                    Return Nothing
                Finally

                    objparam = Nothing

                    If myTrans IsNot Nothing Then
                        myTrans.Dispose()
                        myTrans = Nothing
                    End If

                    If conn IsNot Nothing Then
                        conn.Close()
                        conn.Dispose()
                        conn = Nothing
                    End If

                    If cmdCriteria IsNot Nothing Then
                        cmdCriteria.Parameters.Clear()
                        cmdCriteria.Dispose()
                        cmdCriteria = Nothing
                    End If

                   
                End Try

            End Function

            Public Function SaveException(ByVal criteriaID As Int64, ByVal oExclusion As gloStream.DiseaseManagement.Supporting.Criteria, ByVal LoadFirst As Boolean) As Int64
                'Dim ODB As New gloStream.gloDataBase.gloDataBase
                Dim conn As New SqlConnection(GetConnectionString)

                'declare a transaction object
                Dim myTrans As SqlTransaction = Nothing
                Dim cmdExclusion As SqlCommand = Nothing
                Dim objparam As SqlParameter = Nothing
                Dim MachineID As Long

                Try
                    'do the validations
                    If oExclusion.Name = "" Then
                        _ErrorMessage = "Please enter the Criteria name. "
                    End If

                    conn.Open()

                    myTrans = conn.BeginTransaction

                    cmdExclusion = conn.CreateCommand
                    cmdExclusion.Transaction = myTrans


                    With cmdExclusion
                        .Connection = conn
                        .CommandType = CommandType.StoredProcedure
                        .CommandText = "DM_InsUpdExclusion"
                    End With

                    objparam = New SqlParameter("@dm_mst_Id", SqlDbType.BigInt)
                    With cmdExclusion.Parameters
                        objparam.Direction = ParameterDirection.Input
                        .Add(objparam)
                        .Add("@MachineID", SqlDbType.BigInt)
                        .Add("@dm_mst_CriteriaName", SqlDbType.VarChar)
                        .Add("@dm_mst_AgeMin", SqlDbType.Decimal)
                        .Add("@dm_mst_AgeMax", SqlDbType.Decimal)
                        .Add("@dm_mst_Gender", SqlDbType.VarChar)
                        .Add("@dm_mst_Race", SqlDbType.VarChar)
                        .Add("@dm_mst_MaritalStatus", SqlDbType.VarChar)
                        .Add("@dm_mst_City", SqlDbType.VarChar)
                        .Add("@dm_mst_Status", SqlDbType.VarChar)
                        .Add("@dm_mst_Zip", SqlDbType.VarChar)
                        .Add("@dm_mst_EmplyementStatus", SqlDbType.VarChar)
                        .Add("@dm_mst_HeightMin", SqlDbType.VarChar)
                        .Add("@dm_mst_HeightMax", SqlDbType.VarChar)
                        .Add("@dm_mst_WeightMin", SqlDbType.Decimal)
                        .Add("@dm_mst_WeightMax", SqlDbType.Decimal)
                        .Add("@dm_mst_BMIMin", SqlDbType.Decimal)
                        .Add("@dm_mst_BMIMax", SqlDbType.Decimal)
                        .Add("@dm_mst_TemperatureMin", SqlDbType.Decimal)
                        .Add("@dm_mst_TemperatureMax", SqlDbType.Decimal)
                        .Add("@dm_mst_PulseMin", SqlDbType.Decimal)
                        .Add("@dm_mst_PulseMax", SqlDbType.Decimal)
                        .Add("@dm_mst_PulseOxMin", SqlDbType.Decimal)
                        .Add("@dm_mst_PulseOxMax", SqlDbType.Decimal)
                        .Add("@dm_mst_BPSittingMin", SqlDbType.Decimal)
                        .Add("@dm_mst_BPSittingMax", SqlDbType.Decimal)
                        .Add("@dm_mst_BPStandingMin", SqlDbType.Decimal)
                        .Add("@dm_mst_BPStandingMax", SqlDbType.Decimal)
                        .Add("@dm_mst_DisplayMessage", SqlDbType.VarChar)
                        .Add("@dm_mst_PatientID", SqlDbType.BigInt)
                        .Add("@dm_mst_OriginalID", SqlDbType.BigInt)
                        .Add("@nUserID", SqlDbType.BigInt)
                        .Add("@sUserName", SqlDbType.VarChar)
                        .Add("@bIsActive", SqlDbType.Bit)
                        .Add("@dm_mst_BPSittingToMin", SqlDbType.Decimal)
                        .Add("@dm_mst_BPSittingToMax", SqlDbType.Decimal)
                        .Add("@dm_mst_BPStandingToMin", SqlDbType.Decimal)
                        .Add("@dm_mst_BPStandingToMax", SqlDbType.Decimal)
                    End With

                    With cmdExclusion
                        MachineID = GetPrefixTransactionID()

                        objparam.Value = criteriaID
                        ''.Parameters("@dm_mst_Id").Value = criteriaID
                        .Parameters("@MachineID").Value = MachineID
                        .Parameters("@dm_mst_CriteriaName").Value = oExclusion.Name
                        '' SUDHIR 20090309 - NEW FIELDS
                        .Parameters("@dm_mst_PatientID").Value = 0
                        .Parameters("@dm_mst_OriginalID").Value = 0
                        '' ''
                        .Parameters("@dm_mst_AgeMin").Value = oExclusion.AgeMinimum
                        .Parameters("@dm_mst_AgeMax").Value = oExclusion.AgeMaximum
                        .Parameters("@dm_mst_Gender").Value = oExclusion.Gender
                        .Parameters("@dm_mst_Race").Value = oExclusion.Race
                        .Parameters("@dm_mst_MaritalStatus").Value = oExclusion.MaritalStatus
                        .Parameters("@dm_mst_City").Value = oExclusion.City
                        .Parameters("@dm_mst_Status").Value = oExclusion.State
                        .Parameters("@dm_mst_Zip").Value = oExclusion.Zip
                        .Parameters("@dm_mst_EmplyementStatus").Value = oExclusion.EmployeeStatus
                        .Parameters("@dm_mst_HeightMin").Value = oExclusion.HeightMinimum
                        .Parameters("@dm_mst_HeightMax").Value = oExclusion.HeightMaximum
                        If oExclusion.WeightMinimum = 0.0 Then
                            .Parameters("@dm_mst_WeightMin").Value = System.DBNull.Value
                        Else
                            .Parameters("@dm_mst_WeightMin").Value = oExclusion.WeightMinimum
                        End If

                        If oExclusion.WeightMaximum = 0.0 Then
                            .Parameters("@dm_mst_WeightMax").Value = System.DBNull.Value
                        Else
                            .Parameters("@dm_mst_WeightMax").Value = oExclusion.WeightMaximum
                        End If

                        If oExclusion.BMIMinimum = 0.0 Then
                            .Parameters("@dm_mst_BMIMin").Value = System.DBNull.Value
                        Else
                            .Parameters("@dm_mst_BMIMin").Value = oExclusion.BMIMinimum
                        End If

                        If oExclusion.BMIMaximum = 0.0 Then
                            .Parameters("@dm_mst_BMIMax").Value = System.DBNull.Value
                        Else
                            .Parameters("@dm_mst_BMIMax").Value = oExclusion.BMIMaximum
                        End If

                        If oExclusion.PulseMinimum = 0.0 Then
                            .Parameters("@dm_mst_PulseMin").Value = System.DBNull.Value
                        Else
                            .Parameters("@dm_mst_PulseMin").Value = oExclusion.PulseMinimum
                        End If

                        If oExclusion.PulseMaximum = 0.0 Then
                            .Parameters("@dm_mst_PulseMax").Value = System.DBNull.Value
                        Else
                            .Parameters("@dm_mst_PulseMax").Value = oExclusion.PulseMaximum
                        End If

                        If oExclusion.BPSittingMinimum = 0.0 Then
                            .Parameters("@dm_mst_BPSittingMin").Value = System.DBNull.Value
                        Else
                            .Parameters("@dm_mst_BPSittingMin").Value = oExclusion.BPSittingMinimum
                        End If

                        If oExclusion.BPSittingMaximum = 0.0 Then
                            .Parameters("@dm_mst_BPSittingMax").Value = System.DBNull.Value
                        Else
                            .Parameters("@dm_mst_BPSittingMax").Value = oExclusion.BPSittingMaximum
                        End If

                        If oExclusion.BPStandingMinimum = 0.0 Then
                            .Parameters("@dm_mst_BPStandingMin").Value = System.DBNull.Value
                        Else
                            .Parameters("@dm_mst_BPStandingMin").Value = oExclusion.BPStandingMinimum
                        End If

                        If oExclusion.BPStandingMaximum = 0.0 Then
                            .Parameters("@dm_mst_BPStandingMax").Value = System.DBNull.Value
                        Else
                            .Parameters("@dm_mst_BPStandingMax").Value = oExclusion.BPStandingMaximum
                        End If

                        .Parameters("@dm_mst_DisplayMessage").Value = oExclusion.DisplayMessage
                        If oExclusion.PulseOXMinimum = 0.0 Then
                            .Parameters("@dm_mst_PulseOxMin").Value = System.DBNull.Value
                        Else
                            .Parameters("@dm_mst_PulseOxMin").Value = oExclusion.PulseOXMinimum
                        End If

                        If oExclusion.PulseOXMaximum = 0.0 Then
                            .Parameters("@dm_mst_PulseOxMax").Value = System.DBNull.Value
                        Else
                            .Parameters("@dm_mst_PulseOxMax").Value = oExclusion.PulseOXMaximum
                        End If

                        If oExclusion.TempratureMinumum = 0.0 Then
                            .Parameters("@dm_mst_TemperatureMin").Value = System.DBNull.Value
                        Else
                            .Parameters("@dm_mst_TemperatureMin").Value = oExclusion.TempratureMinumum
                        End If

                        If oExclusion.TempratureMaximum = 0.0 Then
                            .Parameters("@dm_mst_TemperatureMax").Value = System.DBNull.Value
                        Else
                            .Parameters("@dm_mst_TemperatureMax").Value = oExclusion.TempratureMaximum
                        End If


                        .Parameters("@nUserID").Value = mdlGeneral.gnLoginID
                        .Parameters("@sUserName").Value = mdlGeneral.gstrLoginName
                        .Parameters("@bIsActive").Value = oExclusion.IsActive

                        If oExclusion.BPSittingToMinimum = 0.0 Then
                            .Parameters("@dm_mst_BPSittingToMin").Value = System.DBNull.Value
                        Else
                            .Parameters("@dm_mst_BPSittingToMin").Value = oExclusion.BPSittingToMinimum
                        End If

                        If oExclusion.BPSittingToMaximum = 0.0 Then
                            .Parameters("@dm_mst_BPSittingToMax").Value = System.DBNull.Value
                        Else
                            .Parameters("@dm_mst_BPSittingToMax").Value = oExclusion.BPSittingToMaximum
                        End If

                        If oExclusion.BPStandingToMinimum = 0.0 Then
                            .Parameters("@dm_mst_BPStandingToMin").Value = System.DBNull.Value
                        Else
                            .Parameters("@dm_mst_BPStandingToMin").Value = oExclusion.BPStandingToMinimum
                        End If

                        If oExclusion.BPStandingToMaximum = 0.0 Then
                            .Parameters("@dm_mst_BPStandingToMax").Value = System.DBNull.Value
                        Else
                            .Parameters("@dm_mst_BPStandingToMax").Value = oExclusion.BPStandingToMaximum
                        End If

                    End With

                    cmdExclusion.ExecuteNonQuery()


                    If Not IsNothing(objparam) Then
                        criteriaID = objparam.Value
                        '//MsgBox(Criteria_ID)
                    End If

                    If criteriaID > 0 Then


                        ''Delete All Records from OtherDetail Table & Insert Updated Data

                        cmdExclusion.Connection = conn

                        'If LoadFirst Then
                        cmdExclusion.Parameters.Clear()
                        cmdExclusion.CommandType = CommandType.StoredProcedure
                        cmdExclusion.CommandText = "DM_DeleteExclusionDTL"
                        cmdExclusion.Parameters.Add("@dm_mst_Id", SqlDbType.BigInt)
                        cmdExclusion.Parameters("@dm_mst_Id").Value = criteriaID
                        cmdExclusion.ExecuteNonQuery()

                        cmdExclusion.Parameters.Clear()
                        cmdExclusion.CommandType = CommandType.StoredProcedure
                        cmdExclusion.CommandText = "DM_InExclusionDTL"

                        ''Insert All OtherDetails.
                        For i As Integer = 1 To oExclusion.OtherDetails.Count
                            cmdExclusion.Parameters.Add("@dm_mst_Id", SqlDbType.BigInt)
                            cmdExclusion.Parameters.Add("@dm_dtl_Id", SqlDbType.BigInt)
                            cmdExclusion.Parameters.Add("@MachineID", SqlDbType.BigInt)
                            cmdExclusion.Parameters.Add("@dm_dtl_CategoryName", SqlDbType.VarChar)
                            cmdExclusion.Parameters.Add("@dm_dtl_ItemName", SqlDbType.VarChar)
                            cmdExclusion.Parameters.Add("@dm_dtl_Operator", SqlDbType.VarChar)
                            cmdExclusion.Parameters.Add("@dm_dtl_ResultValue1", SqlDbType.VarChar)
                            cmdExclusion.Parameters.Add("@dm_dtl_ResultValue2", SqlDbType.VarChar)
                            cmdExclusion.Parameters.Add("@dm_dtl_Type", SqlDbType.Int)
                            cmdExclusion.Parameters.Add("@dm_dtl_LionicCode", SqlDbType.VarChar)

                            MachineID = GetPrefixTransactionID()
                            cmdExclusion.Parameters("@dm_mst_Id").Value = criteriaID
                            cmdExclusion.Parameters("@dm_dtl_Id").Value = 0
                            cmdExclusion.Parameters("@MachineID").Value = MachineID
                            cmdExclusion.Parameters("@dm_dtl_CategoryName").Value = oExclusion.OtherDetails(i).CategoryName
                            cmdExclusion.Parameters("@dm_dtl_ItemName").Value = oExclusion.OtherDetails(i).ItemName
                            If Not IsNothing(oExclusion.OtherDetails(i).OperatorName) Then
                                cmdExclusion.Parameters("@dm_dtl_Operator").Value = oExclusion.OtherDetails(i).OperatorName
                            Else
                                cmdExclusion.Parameters("@dm_dtl_Operator").Value = ""
                            End If
                            'cmdExclusion.Parameters("@dm_dtl_Operator").Value = oExclusion.OtherDetails(i).OperatorName
                            cmdExclusion.Parameters("@dm_dtl_ResultValue1").Value = oExclusion.OtherDetails(i).Result1
                            cmdExclusion.Parameters("@dm_dtl_ResultValue2").Value = oExclusion.OtherDetails(i).Result2
                            cmdExclusion.Parameters("@dm_dtl_Type").Value = oExclusion.OtherDetails(i).DetailType.GetHashCode
                            cmdExclusion.Parameters("@dm_dtl_LionicCode").Value = oExclusion.OtherDetails(i).LionicCode

                            cmdExclusion.ExecuteNonQuery()
                            cmdExclusion.Parameters.Clear()
                        Next
                        myTrans.Commit()
                        '' gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "'" & oExclusion.Name & "' Disease Management Criteria Added", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                        Return criteriaID
                    Else
                        myTrans.Rollback()
                        Return Nothing
                    End If

                Catch ex As Exception
                    'if some error occur when inserting in any of the tables then all the transactions are rollbacked
                    Try
                        myTrans.Rollback()
                    Catch ex1 As SqlException
                        If Not myTrans.Connection Is Nothing Then
                            'Console.WriteLine("An exception of type " & ex1.GetType().ToString() & _
                            '" was encountered while attempting to roll back the transaction.")
                            '' gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "clsDiseaseManagement -- Add -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                            'UpdateLog("clsDiseaseManagement -- Add -- " & ex.ToString)
                            _ErrorMessage = ex.Message
                        End If
                    End Try
                    'UpdateLog("clsDiseaseManagement -- Add -- " & ex.ToString)
                    _ErrorMessage = ex.Message
                    _ErrorMessage = "Neither record was written to database."
                    Return Nothing
                Finally
                    objparam = Nothing

                    If myTrans IsNot Nothing Then
                        myTrans.Dispose()
                        myTrans = Nothing
                    End If

                    If conn IsNot Nothing Then
                        conn.Close()
                        conn.Dispose()
                        conn = Nothing
                    End If
                    If cmdExclusion IsNot Nothing Then
                        cmdExclusion.Parameters.Clear()
                        cmdExclusion.Dispose()
                        cmdExclusion = Nothing
                    End If
                End Try

            End Function

            Public Function AddRuleHistory(ByVal nUserID As Int64, ByVal sUserName As String, ByVal sNote As String, ByVal sMachineName As String, ByVal nCriteriaID As Int64)

                Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())


                If oDB IsNot Nothing Then
                    If oDB.Connect(False) Then
                        Dim oParamater As New gloDatabaseLayer.DBParameters()
                        Try
                            oParamater.Add("@nHistoryId", 0, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt)
                            oParamater.Add("@nUserId", nUserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                            oParamater.Add("@sUserName", sUserName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                            oParamater.Add("@sNote", sNote, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                            oParamater.Add("@sMachineName", sMachineName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                            oParamater.Add("@sVersion", gstrVersion, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                            oParamater.Add("@nRuleId", nCriteriaID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                            oDB.Execute("dm_Rules_AddHistory", oParamater)
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
                    Else
                        If oDB IsNot Nothing Then
                            oDB.Disconnect()
                            oDB.Dispose()
                            oDB = Nothing
                        End If
                    End If
                End If
                Return Nothing
            End Function

            Public Function UpdateCriteriaStatus(ByVal criteriaID As Int64, ByVal bisActive As Boolean, ByVal _sActivationDeActivationNote As String, Optional ByVal _RecommendationName As String = "")
                Dim oDB As gloDatabaseLayer.DBLayer = Nothing
                Dim oParameters As gloDatabaseLayer.DBParameters = Nothing
                Try
                    oDB = New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)
                    oParameters = New gloDatabaseLayer.DBParameters()
                    oDB.Connect(False)
                    oParameters.Add("@dm_mst_Id", criteriaID, ParameterDirection.Input, SqlDbType.BigInt)
                    oParameters.Add("@bIsActive", bisActive, ParameterDirection.Input, SqlDbType.Bit)
                    oParameters.Add("@nUserId", mdlGeneral.gnLoginID, ParameterDirection.Input, SqlDbType.BigInt)
                    oParameters.Add("@sUserName", mdlGeneral.gstrLoginName, ParameterDirection.Input, SqlDbType.VarChar)
                    oParameters.Add("@sNote", _sActivationDeActivationNote, ParameterDirection.Input, SqlDbType.VarChar)
                    oParameters.Add("@sMachineName", System.Environment.MachineName, ParameterDirection.Input, SqlDbType.VarChar)
                    oParameters.Add("@sVersion", gstrVersion, ParameterDirection.Input, SqlDbType.VarChar)
                    oDB.Execute("gspDM_UpdateStatus", oParameters)

                    If Not bisActive Then
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DM_RuleSetup, gloAuditTrail.ActivityCategory.DM_RuleSetup, gloAuditTrail.ActivityType.Modify, "'" & _RecommendationName & "' successfully De-activated", 0, criteriaID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    Else
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DM_RuleSetup, gloAuditTrail.ActivityCategory.DM_RuleSetup, gloAuditTrail.ActivityType.Modify, "'" & _RecommendationName & "' successfully Activated", 0, criteriaID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    End If

                Catch ex As Exception
                    UpdateLog("clsDiseaseManagement -- UpdateCriteriaStatus -- " & ex.ToString)

                    If Not bisActive Then

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DM_RuleSetup, gloAuditTrail.ActivityCategory.DM_RuleSetup, gloAuditTrail.ActivityType.Modify, "'" & _RecommendationName & "' unsuccessfully De-activated", 0, criteriaID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                    Else

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DM_RuleSetup, gloAuditTrail.ActivityCategory.DM_RuleSetup, gloAuditTrail.ActivityType.Modify, "'" & _RecommendationName & "' unsuccessfully Activated", 0, criteriaID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)

                    End If
                    _ErrorMessage = ex.Message
                Finally
                    If Not IsNothing(oDB) Then
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                    End If
                    If Not IsNothing(oParameters) Then
                        oParameters.Dispose()
                        oParameters = Nothing
                    End If

                End Try

                Return Nothing
            End Function
            
            'Function is not used
            Public Function AddPatientCriteria(ByVal ItemList As ArrayList, ByVal CriteriaID As Int64, ByVal PatientID As Int64, ByVal CriteriaName As String, ByVal Message As String, ByVal IsPatientSpecific As Boolean) As Int64
                'Dim ODB As New gloStream.gloDataBase.gloDataBase
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

                    With cmdCriteria
                        .Connection = conn
                        .CommandType = CommandType.StoredProcedure
                        .CommandText = "DM_InsUpdCriteria"
                    End With

                    objparam = New SqlParameter("@dm_mst_Id", SqlDbType.BigInt)
                    With cmdCriteria.Parameters
                        objparam.Direction = ParameterDirection.InputOutput
                        .Add(objparam)
                        .Add("@MachineID", SqlDbType.BigInt)
                        .Add("@dm_mst_CriteriaName", SqlDbType.VarChar)
                        .Add("@dm_mst_AgeMin", SqlDbType.Decimal)
                        .Add("@dm_mst_AgeMax", SqlDbType.Decimal)
                        .Add("@dm_mst_Gender", SqlDbType.VarChar)
                        .Add("@dm_mst_Race", SqlDbType.VarChar)
                        .Add("@dm_mst_MaritalStatus", SqlDbType.VarChar)
                        .Add("@dm_mst_City", SqlDbType.VarChar)
                        .Add("@dm_mst_Status", SqlDbType.VarChar)
                        .Add("@dm_mst_Zip", SqlDbType.VarChar)
                        .Add("@dm_mst_EmplyementStatus", SqlDbType.VarChar)
                        .Add("@dm_mst_HeightMin", SqlDbType.VarChar)
                        .Add("@dm_mst_HeightMax", SqlDbType.VarChar)
                        .Add("@dm_mst_WeightMin", SqlDbType.Decimal)
                        .Add("@dm_mst_WeightMax", SqlDbType.Decimal)
                        .Add("@dm_mst_BMIMin", SqlDbType.Decimal)
                        .Add("@dm_mst_BMIMax", SqlDbType.Decimal)
                        .Add("@dm_mst_TemperatureMin", SqlDbType.Decimal)
                        .Add("@dm_mst_TemperatureMax", SqlDbType.Decimal)
                        .Add("@dm_mst_PulseMin", SqlDbType.Decimal)
                        .Add("@dm_mst_PulseMax", SqlDbType.Decimal)
                        .Add("@dm_mst_PulseOxMin", SqlDbType.Decimal)
                        .Add("@dm_mst_PulseOxMax", SqlDbType.Decimal)
                        .Add("@dm_mst_BPSittingMin", SqlDbType.Decimal)
                        .Add("@dm_mst_BPSittingMax", SqlDbType.Decimal)
                        .Add("@dm_mst_BPStandingMin", SqlDbType.Decimal)
                        .Add("@dm_mst_BPStandingMax", SqlDbType.Decimal)
                        .Add("@dm_mst_DisplayMessage", SqlDbType.VarChar)
                        .Add("@dm_mst_PatientID", SqlDbType.BigInt)
                        .Add("@dm_mst_OriginalID", SqlDbType.BigInt)
                    End With

                    With cmdCriteria
                        MachineID = GetPrefixTransactionID(PatientID)

                        '' IF PATIENT SPECIFIC THEN UPDATE CRITERIA
                        If IsPatientSpecific = True Then
                            objparam.Value = CriteriaID
                        Else  ''ELSE CREATE COPY OF CRITERIA AND SAVE AGAINST PATIENT
                            objparam.Value = 0
                        End If

                        .Parameters("@MachineID").Value = MachineID
                        .Parameters("@dm_mst_CriteriaName").Value = CriteriaName
                        .Parameters("@dm_mst_PatientID").Value = PatientID
                        If IsPatientSpecific = True Then
                            .Parameters("@dm_mst_OriginalID").Value = 0 ''IF PATIENT SPECIFIC, THEN BY DEFAULT ZERO.
                        Else
                            .Parameters("@dm_mst_OriginalID").Value = CriteriaID ''SAVE CRITERIA ID IN THIS FIELD TO KEEP REFERENCE OF COPIED CRITERIA.
                        End If

                        .Parameters("@dm_mst_DisplayMessage").Value = Message

                        '' BLANK VALUES ''
                        .Parameters("@dm_mst_AgeMin").Value = 0
                        .Parameters("@dm_mst_AgeMax").Value = 0
                        .Parameters("@dm_mst_Gender").Value = ""
                        .Parameters("@dm_mst_Race").Value = ""
                        .Parameters("@dm_mst_MaritalStatus").Value = ""
                        .Parameters("@dm_mst_City").Value = ""
                        .Parameters("@dm_mst_Status").Value = ""
                        .Parameters("@dm_mst_Zip").Value = ""
                        .Parameters("@dm_mst_EmplyementStatus").Value = ""
                        .Parameters("@dm_mst_HeightMin").Value = "" + "'" + "" + "''"
                        .Parameters("@dm_mst_HeightMax").Value = "" + "'" + "" + "''"
                        .Parameters("@dm_mst_WeightMin").Value = 0
                        .Parameters("@dm_mst_WeightMax").Value = 0
                        .Parameters("@dm_mst_BMIMin").Value = 0
                        .Parameters("@dm_mst_BMIMax").Value = 0
                        .Parameters("@dm_mst_PulseMin").Value = 0
                        .Parameters("@dm_mst_PulseMax").Value = 0
                        .Parameters("@dm_mst_BPSittingMin").Value = 0
                        .Parameters("@dm_mst_BPSittingMax").Value = 0
                        .Parameters("@dm_mst_BPStandingMin").Value = 0
                        .Parameters("@dm_mst_BPStandingMax").Value = 0
                        .Parameters("@dm_mst_PulseOxMin").Value = 0
                        .Parameters("@dm_mst_PulseOxMax").Value = 0
                        .Parameters("@dm_mst_TemperatureMin").Value = 0
                        .Parameters("@dm_mst_TemperatureMax").Value = 0
                        '' ''
                    End With

                    cmdCriteria.ExecuteNonQuery()


                    If Not IsNothing(objparam) Then
                        CriteriaID = objparam.Value
                        '//MsgBox(Criteria_ID)
                    End If

                    If CriteriaID > 0 Then
                        ''EMPTY TEMPLATE DETAILS IF UPDATING CRITERIA
                        cmdCriteria.CommandType = CommandType.Text
                        cmdCriteria.CommandText = "DELETE FROM DM_Templates_DTL WHERE dm_Templatedtl_Id = " & CriteriaID & ""
                        cmdCriteria.ExecuteNonQuery()

                        ''insert Orders into Template_MST table
                        cmdCriteria.CommandType = CommandType.StoredProcedure
                        cmdCriteria.Parameters.Clear()
                        cmdCriteria.CommandText = "DM_InsTemplates_DTL"
                        For i As Integer = 0 To ItemList.Count - 1
                            cmdCriteria.Parameters.Add("@Criteria_ID", SqlDbType.BigInt)
                            cmdCriteria.Parameters("@Criteria_ID").Value = CriteriaID

                            cmdCriteria.Parameters.Add("@OrderID", SqlDbType.BigInt)
                            cmdCriteria.Parameters("@OrderID").Value = CType(ItemList(i), myList).ID

                            cmdCriteria.Parameters.Add("@OrderType", SqlDbType.BigInt)
                            cmdCriteria.Parameters("@OrderType").Value = CType(ItemList(i), myList).Index

                            'sarika DM Denormalization 20090331
                            cmdCriteria.Parameters.Add("@TemplateName", SqlDbType.VarChar)
                            'If CType(ItemList(i), myList).Index = 0 Then
                            cmdCriteria.Parameters("@TemplateName").Value = CType(ItemList(i), myList).DMTemplateName

                            'Else
                            'cmdCriteria.Parameters("@TemplateName").Value = ""
                            'End If


                            cmdCriteria.Parameters.Add("@Template", SqlDbType.Image)
                            'if guideline
                            'If CType(ItemList(i), myList).Index = 0 Then
                            cmdCriteria.Parameters("@Template").Value = CType(CType(ItemList(i), myList).DMTemplate, Byte()).Clone()
                            'Else
                            'cmdCriteria.Parameters("@Template").Value = Nothing
                            'End If

                            cmdCriteria.Parameters.Add("@dm_Templatedtl_TemplateDtlInfo", SqlDbType.VarChar)
                            cmdCriteria.Parameters.Add("@sDrugForm", SqlDbType.VarChar)
                            cmdCriteria.Parameters.Add("@sRoute", SqlDbType.VarChar)
                            cmdCriteria.Parameters.Add("@sFrequency", SqlDbType.VarChar)
                            cmdCriteria.Parameters.Add("@sNDCCode", SqlDbType.VarChar)
                            cmdCriteria.Parameters.Add("@nIsNarcotics", SqlDbType.Int)
                            cmdCriteria.Parameters.Add("@sDuration", SqlDbType.VarChar)

                            cmdCriteria.Parameters.Add("@sDrugQtyQualifier", SqlDbType.VarChar)

                            If cmdCriteria.Parameters("@OrderType").Value = TemplateCategoryID.Rx Then
                                cmdCriteria.Parameters("@dm_Templatedtl_TemplateDtlInfo").Value = CType(ItemList(i), myList).Dosage

                                'sarika DM Denormalization for Rx on 20090410

                                If Not IsNothing(CType(ItemList(i), myList).DrugForm) Then
                                    cmdCriteria.Parameters("@sDrugForm").Value = CType(ItemList(i), myList).DrugForm
                                Else
                                    cmdCriteria.Parameters("@sDrugForm").Value = ""
                                End If


                                If Not IsNothing(CType(ItemList(i), myList).Route) Then
                                    cmdCriteria.Parameters("@sRoute").Value = CType(ItemList(i), myList).Route
                                Else
                                    cmdCriteria.Parameters("@sRoute").Value = ""
                                End If


                                If Not IsNothing(CType(ItemList(i), myList).Frequency) Then
                                    cmdCriteria.Parameters("@sFrequency").Value = CType(ItemList(i), myList).Frequency
                                Else
                                    cmdCriteria.Parameters("@sFrequency").Value = ""
                                End If


                                If Not IsNothing(CType(ItemList(i), myList).NDCCode) Then
                                    cmdCriteria.Parameters("@sNDCCode").Value = CType(ItemList(i), myList).NDCCode
                                Else
                                    cmdCriteria.Parameters("@sNDCCode").Value = ""
                                End If

                                If Not IsNothing(CType(ItemList(i), myList).IsNarcotic) Then
                                    cmdCriteria.Parameters("@nIsNarcotics").Value = CType(ItemList(i), myList).IsNarcotic
                                Else
                                    cmdCriteria.Parameters("@nIsNarcotics").Value = 0
                                End If


                                If Not IsNothing(CType(ItemList(i), myList).IsNarcotic) Then
                                    cmdCriteria.Parameters("@mpid").Value = CType(ItemList(i), myList).mpid
                                Else
                                    cmdCriteria.Parameters("@mpid").Value = 0
                                End If

                                If Not IsNothing(CType(ItemList(i), myList).Duration) Then
                                    cmdCriteria.Parameters("@sDuration").Value = CType(ItemList(i), myList).Duration
                                Else
                                    cmdCriteria.Parameters("@sDuration").Value = ""
                                End If


                               
                                If Not IsNothing(CType(ItemList(i), myList).DrugQtyQualifier) Then
                                    cmdCriteria.Parameters("@sDrugQtyQualifier").Value = CType(ItemList(i), myList).DrugQtyQualifier
                                Else
                                    cmdCriteria.Parameters("@sDrugQtyQualifier").Value = ""
                                End If


                                '----

                            Else
                                cmdCriteria.Parameters("@dm_Templatedtl_TemplateDtlInfo").Value = ""
                                cmdCriteria.Parameters("@sDrugForm").Value = ""
                                cmdCriteria.Parameters("@sRoute").Value = ""
                                cmdCriteria.Parameters("@sFrequency").Value = ""

                                cmdCriteria.Parameters("@sNDCCode").Value = ""

                                cmdCriteria.Parameters("@nIsNarcotics").Value = 0

                                cmdCriteria.Parameters("@sDuration").Value = ""

                                cmdCriteria.Parameters("@sDrugQtyQualifier").Value = ""
                            End If

                            '--

                            cmdCriteria.ExecuteNonQuery()
                            cmdCriteria.Parameters.Clear()
                        Next

                        myTrans.Commit()
                        ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "'" & CriteriaName & "' Disease Management Patient Criteria Added", gloAuditTrail.ActivityOutCome.Success)
                        ''Added Rahul P on 20101009
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "'" & CriteriaName & "' Disease Management Patient Criteria Added", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        ''

                        'Dim objAudit As New clsAudit
                        'objAudit.CreateLog(clsAudit.enmActivityType.Add, "'" & CriteriaName & "' Disease Management Patient Criteria Added", gstrLoginName, 0)
                        'objAudit = Nothing

                        Return CriteriaID
                    Else
                        myTrans.Rollback()
                        Return Nothing
                    End If

                Catch ex As Exception
                    'if some error occur when inserting in any of the tables then all the transactions are rollbacked
                    Try
                        myTrans.Rollback()
                    Catch ex1 As SqlException
                        If Not myTrans.Connection Is Nothing Then
                            'Console.WriteLine("An exception of type " & ex1.GetType().ToString() & _
                            '" was encountered while attempting to roll back the transaction.")
                            UpdateLog("clsDiseaseManagement -- Add -- " & ex.ToString)
                            _ErrorMessage = ex.Message
                        End If
                    End Try
                    UpdateLog("clsDiseaseManagement -- Add -- " & ex.ToString)
                    _ErrorMessage = ex.Message
                    _ErrorMessage = "Neither record was written to database."
                    Return Nothing
                Finally

                  

                    If objparam IsNot Nothing Then
                        objparam = Nothing
                    End If
                    If myTrans IsNot Nothing Then
                        myTrans.Dispose()
                        myTrans = Nothing
                    End If
                    If conn IsNot Nothing Then
                        conn.Close()
                        conn.Dispose()
                        conn = Nothing
                    End If

                    If cmdCriteria IsNot Nothing Then
                        cmdCriteria.Parameters.Clear()
                        cmdCriteria.Dispose()
                        cmdCriteria = Nothing
                    End If
                End Try

            End Function

            

            

            

            Public Function Delete(ByVal oCriteriaID As Long, ByVal oCriteriaName As String) As Boolean
                Dim conn As New SqlConnection(GetConnectionString)
                Dim myTrans As SqlTransaction = Nothing
                Dim cmdCriteria As SqlCommand = Nothing

                Try
                    conn.Open()

                    myTrans = conn.BeginTransaction
                    cmdCriteria = conn.CreateCommand
                    cmdCriteria.Transaction = myTrans

                    'first delete from all the detail tables 
                    'detail tables are : DM_CriteriaDrug_DTL ,DM_CriteriaHistory_DTL,DM_ICD9CPT_DTL and DM_Labs_DTL

                    'deleting first from DM_CriteriaDrug_DTL table
                    With cmdCriteria
                        .Connection = conn
                        .CommandType = CommandType.Text
                        .CommandText = "delete from DM_CriteriaDrug_DTL where dm_Drugdtl_Id =" & oCriteriaID
                        cmdCriteria.ExecuteNonQuery()
                    End With

                    'deleting from DM_CriteriaHistory_DTL table
                    cmdCriteria.CommandText = "delete from DM_CriteriaHistory_DTL where  dm_Chdtl_Id =" & oCriteriaID
                    cmdCriteria.ExecuteNonQuery()

                    'deleting from DM_ICD9CPT_DTL table
                    cmdCriteria.CommandText = "delete from DM_ICD9CPT_DTL where  dm_ICD9CPTdtl_Id =" & oCriteriaID
                    cmdCriteria.ExecuteNonQuery()

                    'deleting from DM_Labs_DTL table 

                    cmdCriteria.CommandText = "delete from DM_Labs_DTL where  dm_Labsdtl_Id =" & oCriteriaID
                    cmdCriteria.ExecuteNonQuery()

                    'deleting from the Master table DM_Criteria_MST

                    cmdCriteria.CommandText = "delete from DM_Criteria_MST where  dm_mst_Id =" & oCriteriaID
                    cmdCriteria.ExecuteNonQuery()

                    cmdCriteria.CommandText = "delete from DM_Criteria_DemoVital where  dm_mst_Id =" & oCriteriaID
                    cmdCriteria.ExecuteNonQuery()

                    cmdCriteria.CommandText = "delete from DM_Exclusion_DemoVital where  dm_mst_Id =" & oCriteriaID
                    cmdCriteria.ExecuteNonQuery()


                    cmdCriteria.CommandText = "delete from DM_Criteria_DTL where  dm_mst_Id =" & oCriteriaID
                    cmdCriteria.ExecuteNonQuery()

                    cmdCriteria.CommandText = "delete from DM_Exclusion_DTL where  dm_mst_Id =" & oCriteriaID
                    cmdCriteria.ExecuteNonQuery()

                    myTrans.Commit()
                    '' gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "Criteria  deleted. ", gloAuditTrail.ActivityOutCome.Success)

                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "Criteria deleted.", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20101009
                    '' gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "Criteria deleted.", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ''

                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DM_RuleSetup, gloAuditTrail.ActivityCategory.DM_RuleSetup, gloAuditTrail.ActivityType.Delete, "'" & oCriteriaName & "' successfully Deleted", 0, oCriteriaID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)


                    Return True

                Catch ex As Exception
                    'if some error occur when deleting from any of the table then all the transactions are rollbacked
                    '' gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    Try
                        myTrans.Rollback()
                    Catch ex1 As SqlException
                        If Not myTrans.Connection Is Nothing Then
                            'Console.WriteLine("An exception of type " & ex1.GetType().ToString() & _
                            '" was encountered while attempting to roll back the transaction.")
                            UpdateLog("clsDiseaseManagement -- Delete(2) -- " & ex.ToString)
                            _ErrorMessage = ex.Message
                        End If
                    End Try
                    UpdateLog("clsDiseaseManagement -- Delete(2) -- " & ex.ToString)
                    _ErrorMessage = "Neither record was deleted from the database."

                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DM_RuleSetup, gloAuditTrail.ActivityCategory.DM_RuleSetup, gloAuditTrail.ActivityType.Delete, "'" & oCriteriaName & "' unsuccessfully Deleted", 0, oCriteriaID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)

                    _ErrorMessage = ex.Message
                    Return Nothing
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

                    If cmdCriteria IsNot Nothing Then
                        cmdCriteria.Parameters.Clear()
                        cmdCriteria.Dispose()
                        cmdCriteria = Nothing
                    End If
                End Try
            End Function

            

            Public Function IsRecommendationInCurrentTabWithIP_RO_New_Status(ByVal nRuleId, ByVal nPatientId, ByVal nRecommendationId) As Boolean

                Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
                Dim oParamater As New gloDatabaseLayer.DBParameters()
                Dim oDataTable As DataTable = Nothing
                Dim _isRecommendationInCurrentTabWith_IP_RO_New_Status As Boolean = False
                Dim _result As Object = Nothing

                Try

                    If nRuleId > 0 AndAlso nRecommendationId > 0 AndAlso nPatientId > 0 Then
                        oDB.Connect(False)
                        oParamater.Add("@dm_mst_Id", nRuleId, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                        oParamater.Add("@nRecommendationId", nRecommendationId, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                        oParamater.Add("@nPatientId", nPatientId, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                        oDB.Retrive("DM_CheckRecommendationwithNewIPROStatus", oParamater, oDataTable)
                        If oDataTable IsNot Nothing Then
                            If oDataTable.Rows.Count > 0 Then
                                _result = oDataTable(0)(0)
                                If Not IsNothing(_result) AndAlso _result.ToString() <> "" Then
                                    _isRecommendationInCurrentTabWith_IP_RO_New_Status = Convert.ToBoolean(_result)
                                End If
                            End If
                        End If
                        oDB.Disconnect()
                    End If

                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
                Finally
                    If oDataTable IsNot Nothing Then
                        oDataTable.Dispose()
                        oDataTable = Nothing
                    End If
                    If oParamater IsNot Nothing Then
                        oParamater.Dispose()
                        oParamater = Nothing
                    End If
                    If oDB IsNot Nothing Then
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                    End If

                    _result = Nothing

                End Try

                Return _isRecommendationInCurrentTabWith_IP_RO_New_Status

            End Function

            Public Function UpdateRecommendation(ByVal nRecommendationID As Int64, ByVal nCriteriaID As Int64, ByVal nPatientID As Int64, ByVal sNote As String, ByVal nNoteUserID As Int64, ByVal sNoteUserName As String, ByVal dtSatisfiedDate As Date, ByVal Status As Int16, ByVal StatusDesciption As String, Optional ByVal sRecommendationName As String = "")

                Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())


                If oDB IsNot Nothing Then
                    If oDB.Connect(False) Then
                        Dim oParamater As New gloDatabaseLayer.DBParameters()
                        Try
                            oParamater.Add("@nRecommendationID", nRecommendationID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                            oParamater.Add("@nCriteriaID", nCriteriaID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                            oParamater.Add("@nPatientID", nPatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                            oParamater.Add("@sNote", sNote, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                            oParamater.Add("@nNoteUserID", nNoteUserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                            oParamater.Add("@sNoteUserName", sNoteUserName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                            oParamater.Add("@nStatus", Status, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int)
                            oParamater.Add("@sStatusDesciption", StatusDesciption, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                            If dtSatisfiedDate.Date = Date.MinValue Then
                                oParamater.Add("@dtSatisfiedDate", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Date)
                            Else
                                oParamater.Add("@dtSatisfiedDate", dtSatisfiedDate.ToShortDateString(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Date)
                            End If
                            oParamater.Add("@nStatusUserID", nNoteUserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                            oParamater.Add("@sStatusUserName", sNoteUserName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                            oDB.Execute("UpdateRecommendation", oParamater)

                            Select Case Status

                                Case gloStream.DiseaseManagement.DiseaseManagement.RecommendationStatus.Reopened

                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.Modify, "'" & sRecommendationName & "' Changed status to Reopened", nPatientID, nCriteriaID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                                Case gloStream.DiseaseManagement.DiseaseManagement.RecommendationStatus.Satisfied

                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.Modify, "'" & sRecommendationName & "' Changed status to Satisfied", nPatientID, nCriteriaID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                                Case gloStream.DiseaseManagement.DiseaseManagement.RecommendationStatus.InProcess

                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.Modify, "'" & sRecommendationName & "' Changed status to InProcess", nPatientID, nCriteriaID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                                Case gloStream.DiseaseManagement.DiseaseManagement.RecommendationStatus.CancelAsNotAppilcable

                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.Modify, "'" & sRecommendationName & "' Changed status to CancelAsNotAppilcable", nPatientID, nCriteriaID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                            End Select

                        Catch ex As gloDatabaseLayer.DBException
                            ex.ERROR_Log(ex.ToString())

                            Select Case Status

                                Case gloStream.DiseaseManagement.DiseaseManagement.RecommendationStatus.Reopened

                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.Modify, "'" & sRecommendationName & "' unsuccessfully Changed status to Reopened", nPatientID, nCriteriaID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)

                                Case gloStream.DiseaseManagement.DiseaseManagement.RecommendationStatus.Satisfied

                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.Modify, "'" & sRecommendationName & "' unsuccessfully Changed status to Satisfied", nPatientID, nCriteriaID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)

                                Case gloStream.DiseaseManagement.DiseaseManagement.RecommendationStatus.InProcess

                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.Modify, "'" & sRecommendationName & "' unsuccessfully Changed status to InProcess", nPatientID, nCriteriaID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)

                                Case gloStream.DiseaseManagement.DiseaseManagement.RecommendationStatus.CancelAsNotAppilcable

                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.Modify, "'" & sRecommendationName & "' unsuccessfully Changed status to CancelAsNotAppilcable", nPatientID, nCriteriaID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)

                            End Select
                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)

                            Select Case Status

                                Case gloStream.DiseaseManagement.DiseaseManagement.RecommendationStatus.Reopened

                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.Modify, "'" & gstrLoginName & "' unsuccessfully Recommendation Reopened", nPatientID, nCriteriaID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)

                                Case gloStream.DiseaseManagement.DiseaseManagement.RecommendationStatus.Satisfied

                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.Modify, "'" & gstrLoginName & "' unsuccessfully Recommendation Mark Satisfied ", nPatientID, nCriteriaID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)

                                Case gloStream.DiseaseManagement.DiseaseManagement.RecommendationStatus.InProcess

                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.Modify, "'" & gstrLoginName & "' unsuccessfully Recommendation InProcess", nPatientID, nCriteriaID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)

                                Case gloStream.DiseaseManagement.DiseaseManagement.RecommendationStatus.CancelAsNotAppilcable

                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.Modify, "'" & gstrLoginName & "' unsuccessfully Recommendation CancelAsNotAppilcable", nPatientID, nCriteriaID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)

                            End Select
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
                    Else
                        If oDB IsNot Nothing Then
                            oDB.Disconnect()
                            oDB.Dispose()
                            oDB = Nothing
                        End If
                    End If
                Else

                End If
                Return Nothing
            End Function

            Public Function RecommendationNoteUpdate(ByVal nRecommendationID As Int64, ByVal nCriteriaID As Int64, ByVal nPatientID As Int64, ByVal sNote As String, ByVal nNoteUserID As Int64, ByVal sNoteUserName As String, ByVal dtSatisfiedDate As Date)

                Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
                Dim oParamater As New gloDatabaseLayer.DBParameters()
                Try
                    If oDB IsNot Nothing Then
                        If oDB.Connect(False) Then

                            oParamater.Add("@nRecommendationID", nRecommendationID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                            oParamater.Add("@nCriteriaID", nCriteriaID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                            oParamater.Add("@nPatientID", nPatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                            oParamater.Add("@sNote", sNote, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                            oParamater.Add("@nNoteUserID", nNoteUserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                            oParamater.Add("@sNoteUserName", sNoteUserName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                            If dtSatisfiedDate.Date <> Date.MinValue Then
                                oParamater.Add("@dtSatisfiedDate", dtSatisfiedDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Date)
                            End If
                            oDB.Execute("RecommendationNoteUpdate", oParamater)
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

                Return Nothing
            End Function
            '' Below Function is Not in use
            Public Function RecommendationStatusUpdate(ByVal nRecommendationID As Int64, ByVal nCriteriaID As Int64, ByVal nPatientID As Int64, ByVal Status As Int16, ByVal StatusDesciption As String)

                Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())


                If oDB IsNot Nothing Then
                    If oDB.Connect(False) Then
                        Dim oParamater As New gloDatabaseLayer.DBParameters()
                        Try
                            oParamater.Add("@nRecommendationID", nRecommendationID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                            oParamater.Add("@nCriteriaID", nCriteriaID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                            oParamater.Add("@nPatientID", nPatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                            oParamater.Add("@nStatus", Status, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int)
                            oParamater.Add("@sStatusDesciption", StatusDesciption, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                            oDB.Execute("RecommendationStatusUpdate", oParamater)

                            If Status = 3 Then
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.Modify, "'" & gstrLoginName & "' successfully Recommendation Mark Satisfied ", nPatientID, nCriteriaID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            ElseIf Status = 4 Then

                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.Modify, "'" & gstrLoginName & "' successfully Recommendation Reopened", nPatientID, nCriteriaID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            ElseIf Status = 5 Then
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.Modify, "'" & gstrLoginName & "' successfully Recommendation CancelAsNotAppilcable", nPatientID, nCriteriaID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            ElseIf Status = 2 Then
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.Modify, "'" & gstrLoginName & "' successfully Recommendation InProcess", nPatientID, nCriteriaID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            End If


                        Catch ex As gloDatabaseLayer.DBException
                            ex.ERROR_Log(ex.ToString())
                            If oParamater IsNot Nothing Then
                                oParamater.Dispose()
                                oParamater = Nothing
                            End If

                            If oDB IsNot Nothing Then
                                oDB.Disconnect()
                                oDB.Dispose()
                                oDB = Nothing
                            End If

                            If Status = 3 Then
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.Modify, "'" & gstrLoginName & "' unsuccessfully Recommendation Mark Satisfied ", nPatientID, nCriteriaID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                            ElseIf Status = 4 Then

                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.Modify, "'" & gstrLoginName & "' unsuccessfully Recommendation Reopened", nPatientID, nCriteriaID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                            ElseIf Status = 5 Then
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.Modify, "'" & gstrLoginName & "' unsuccessfully Recommendation CancelAsNotAppilcable", nPatientID, nCriteriaID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                            ElseIf Status = 2 Then
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.Modify, "'" & gstrLoginName & "' unsuccessfully Recommendation InProcess", nPatientID, nCriteriaID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                            End If

                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
                            If oParamater IsNot Nothing Then
                                oParamater.Dispose()
                                oParamater = Nothing
                            End If
                            If oDB IsNot Nothing Then
                                oDB.Disconnect()
                                oDB.Dispose()
                                oDB = Nothing
                            End If


                            If Status = 3 Then
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.Modify, "'" & gstrLoginName & "' unsuccessfully Recommendation Mark Satisfied ", nPatientID, nCriteriaID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                            ElseIf Status = 4 Then

                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.Modify, "'" & gstrLoginName & "' unsuccessfully Recommendation Reopened", nPatientID, nCriteriaID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                            ElseIf Status = 5 Then
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.Modify, "'" & gstrLoginName & "' unsuccessfully Recommendation CancelAsNotAppilcable", nPatientID, nCriteriaID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                            ElseIf Status = 2 Then
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.Modify, "'" & gstrLoginName & "' unsuccessfully Recommendation InProcess", nPatientID, nCriteriaID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                            End If
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
                    Else
                        If oDB IsNot Nothing Then
                            oDB.Disconnect()
                            oDB.Dispose()
                            oDB = Nothing
                        End If
                    End If
                Else

                End If
                Return Nothing
            End Function

            Public Function GetRecommendationsHistory(ByVal nRecommendationID As Int64, ByVal ncriteriaID As Int64, ByVal nPatientID As Int64) As DataTable

                Dim _dt As DataTable = Nothing
                Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
                Dim oParamater As New gloDatabaseLayer.DBParameters()
                Dim sStartdate As String = ""
                Dim sToDate As String = ""
                Try
                    If oDB IsNot Nothing Then
                        If oDB.Connect(False) Then

                            oParamater.Add("@nRecommendationID", nRecommendationID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                            oParamater.Add("@nCriteriaID", ncriteriaID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                            oParamater.Add("@nPatientID", nPatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                            oDB.Retrive("GetRecommendationsHistory", oParamater, _dt)


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

            Public Function GetRuleActivityHistory(ByVal ncriteriaID As Int64) As DataTable

                Dim _dt As DataTable = Nothing
                Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
                Dim oParamater As New gloDatabaseLayer.DBParameters()
                Try
                    If oDB IsNot Nothing Then
                        If oDB.Connect(False) Then
                            oParamater.Add("@nCriteriaID", ncriteriaID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                            oDB.Retrive("GetRuleActivityHistory", oParamater, _dt)
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

            Public Function GetRecommendations(ByVal nPatientID As Int64, ByVal nFlagRecommendation As Int16) As DataTable

                Dim _dt As DataTable = Nothing
                Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
                Dim oParamater As New gloDatabaseLayer.DBParameters()
                Try
                    If oDB IsNot Nothing Then
                        If oDB.Connect(False) Then
                            oParamater.Add("@nPatientID", nPatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                            oParamater.Add("@ReCommendation", nFlagRecommendation, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int)
                            'Parameter Added By Sameer on 20 Sep 2013 For Special Alert Rights for Rules
                            oParamater.Add("@nUserID", gnLoginID, ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                            oDB.Retrive("GetRecommendations", oParamater, _dt)
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

            Public Function GetOpenRecommendations() As DataTable

                Dim _dt As DataTable = Nothing
                Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())

                Try

                    If oDB IsNot Nothing Then
                        If oDB.Connect(False) Then
                            oDB.Retrive("DM_GetOpenRecommendation", _dt)
                        End If
                    End If

                Catch ex As gloDatabaseLayer.DBException
                    ex.ERROR_Log(ex.ToString())

                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)

                Finally

                    If oDB IsNot Nothing Then
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                    End If

                End Try

                Return _dt

            End Function

            Public Function GetRecommendationsCountAndName(ByVal nPatientID As Int64) As DataTable
                Dim _dt As DataTable = Nothing
                Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
                Dim oParamater As New gloDatabaseLayer.DBParameters()
                Try
                    If oDB IsNot Nothing Then
                        If oDB.Connect(False) Then

                            oParamater.Add("@nPatientID", nPatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                            'Parameter Added By Sameer on 20 Sep 2013 For Special Alert Rights for Rules
                            oParamater.Add("@nUserID", gnLoginID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                            oDB.Retrive("dm_getRecommendationCountAlert", oParamater, _dt)


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

            Public Function GetRecommendationOrders(ByVal nRecommendationID As Int64, ByVal nPatientID As Int64) As DataTable
                Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
                Dim dt As DataTable = Nothing
                Dim oParamater As New gloDatabaseLayer.DBParameters()
                Try
                    oDB.Connect(False)
                    oParamater.Add("@nRecommendationId", nRecommendationID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oParamater.Add("@nPatientID", nPatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oDB.Retrive("DM_GetRecommendationOrders", oParamater, dt)

                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)

                Finally

                    If oParamater IsNot Nothing Then
                        oParamater.Dispose()
                        oParamater = Nothing
                    End If

                    If Not IsNothing(oDB) Then
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                    End If
                End Try
                Return dt
            End Function

            Public Function GetCriteria(ByVal oCriteriaID As Long, ByVal PatientID As Int64) As gloStream.DiseaseManagement.Supporting.Criteria
                Dim _strSQL As String = ""
                Dim oCriteria As New gloStream.DiseaseManagement.Supporting.Criteria
                Dim oOtherDetails As New gloStream.DiseaseManagement.Supporting.OtherDetails
                Dim oOtherDetail As gloStream.DiseaseManagement.Supporting.OtherDetail
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim oDataReader As SqlClient.SqlDataReader
                Dim _FillDetail As Boolean = False
                Try
                    If Not oCriteriaID = 0 Then
                        'Criteria Master Record
                        _strSQL = "SELECT  dm_mst_CriteriaName, DM_Criteria_DemoVital.dm_mst_AgeMin, DM_Criteria_DemoVital.dm_mst_AgeMax, " _
                        & " DM_Criteria_DemoVital.dm_mst_City, DM_Criteria_DemoVital.dm_mst_Status, DM_Criteria_DemoVital.dm_mst_Zip, DM_Criteria_DemoVital.dm_mst_EmplyementStatus, " _
                        & " DM_Criteria_DemoVital.dm_mst_HeightMin, DM_Criteria_DemoVital.dm_mst_HeightMax, DM_Criteria_DemoVital.dm_mst_WeightMin, DM_Criteria_DemoVital.dm_mst_WeightMax, DM_Criteria_DemoVital.dm_mst_BMIMin,DM_Criteria_DemoVital.dm_mst_BMIMax," _
                        & " DM_Criteria_DemoVital.dm_mst_TemperatureMin, DM_Criteria_DemoVital.dm_mst_TemperatureMax, DM_Criteria_DemoVital.dm_mst_PulseMin, DM_Criteria_DemoVital.dm_mst_PulseMax, DM_Criteria_DemoVital.dm_mst_PulseOxMin, DM_Criteria_DemoVital.dm_mst_PulseOxMax, DM_Criteria_DemoVital.dm_mst_BPSittingMin, DM_Criteria_DemoVital.dm_mst_BPSittingMax, DM_Criteria_DemoVital.dm_mst_BPStandingMin, DM_Criteria_DemoVital.dm_mst_BPStandingMax, dm_mst_DisplayMessage, DM_Criteria_DemoVital.dm_mst_AgeMax,DATEDIFF(DAY,DATEADD(YEAR,-DM_Criteria_DemoVital.dm_mst_AgeMin,dbo.gloGetDate()),dbo.gloGetDate()) as AgeMin, DATEDIFF(DAY,DATEADD(YEAR,-DM_Criteria_DemoVital.dm_mst_AgeMax,dbo.gloGetDate()),dbo.gloGetDate()) as AgeMax,bIsActive , DM_Criteria_MST.bIsSpecialAlert  " _
                        & " FROM DM_Criteria_MST LEFT OUTER JOIN DM_Criteria_DemoVital ON DM_Criteria_MST.dm_mst_id=DM_Criteria_DemoVital.dm_mst_id " _
                        & " WHERE DM_Criteria_MST.dm_mst_Id = " & oCriteriaID & " AND DM_Criteria_MST.dm_mst_CriteriaName IS NOT NULL"
                        oDB.Connect(GetConnectionString)
                        oDataReader = oDB.ReadQueryRecords(_strSQL)
                        If Not oDataReader Is Nothing Then
                            If oDataReader.HasRows = True Then
                                _FillDetail = True
                                While oDataReader.Read
                                    With oCriteria
                                        Dim _tempAge As Double = 0
                                        Dim _tempAgeYr As Double = 0
                                        Dim _tempAgeMnt As Double = 0

                                        .Name = oDataReader.Item("dm_mst_CriteriaName") & ""
                                        If Not IsDBNull(oDataReader.Item("dm_mst_AgeMin")) Then
                                            _tempAge = 0 : _tempAgeYr = 0 : _tempAgeMnt = 0
                                            _tempAge = CDbl(oDataReader.Item("dm_mst_AgeMin") & "")

                                            Dim _strage As String() = _tempAge.ToString().Split(".") '   =tempage .Substring(0,_tempAge.ToString()     

                                            If _strage.Length > 1 Then
                                                _tempAgeYr = CDbl(_strage(0).ToString())

                                                ''GLO2012-0016324 : DM Setup 
                                                ''as concatinating the _age(1) with the 1 "0" so as to retrive 4 digit value
                                                _strage(1) += "0"
                                                _strage(1) = _strage(1).Substring(0, 2)

                                                _tempAgeMnt = CDbl(_strage(1).ToString())
                                            Else
                                                _tempAgeYr = CDbl(_strage(0).ToString())
                                                _tempAgeMnt = 0
                                            End If

                                            .AgeMinimum = Format(CDbl(_tempAgeYr + (_tempAgeMnt / 12)), "#0.0000")
                                            .AgeInDaysMinimum = oDataReader.Item("AgeMin")
                                            '.AgeMinimum = oDataReader.Item("dm_mst_AgeMin") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_AgeMax")) Then

                                            _tempAge = 0 : _tempAgeYr = 0 : _tempAgeMnt = 0
                                            _tempAge = CDbl(oDataReader.Item("dm_mst_AgeMax") & "")

                                            Dim _strage As String() = _tempAge.ToString().Split(".") '   =tempage .Substring(0,_tempAge.ToString()     

                                            If _strage.Length > 1 Then
                                                _tempAgeYr = CDbl(_strage(0).ToString())

                                                ''GLO2012-0016324 : DM Setup 
                                                ''as concatinating the _age(1) with the 1 "0" so as to retrive 4 digit value
                                                _strage(1) += "0"
                                                _strage(1) = _strage(1).Substring(0, 2)

                                                _tempAgeMnt = CDbl(_strage(1).ToString())
                                            Else
                                                _tempAgeYr = CDbl(_strage(0).ToString())
                                                _tempAgeMnt = 0
                                            End If

                                            .AgeMaximum = Format(CDbl(_tempAgeYr + (_tempAgeMnt / 12)), "#0.0000")

                                            .AgeInDaysMaximum = oDataReader.Item("AgeMax")
                                            '.AgeMaximum = oDataReader.Item("dm_mst_AgeMax") & ""
                                        End If


                                        If Not IsDBNull(oDataReader.Item("dm_mst_City")) Then
                                            .City = oDataReader.Item("dm_mst_City") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_Status")) Then
                                            .State = oDataReader.Item("dm_mst_Status") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_Zip")) Then
                                            .Zip = oDataReader.Item("dm_mst_Zip") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_EmplyementStatus")) Then
                                            .EmployeeStatus = oDataReader.Item("dm_mst_EmplyementStatus") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_HeightMin")) Then
                                            .HeightMinimum = oDataReader.Item("dm_mst_HeightMin") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_HeightMax")) Then
                                            .HeightMaximum = oDataReader.Item("dm_mst_HeightMax") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_WeightMin")) Then
                                            .WeightMinimum = oDataReader.Item("dm_mst_WeightMin") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_WeightMax")) Then
                                            .WeightMaximum = oDataReader.Item("dm_mst_WeightMax") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_BMIMin")) Then
                                            .BMIMinimum = oDataReader.Item("dm_mst_BMIMin") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_BMIMax")) Then
                                            .BMIMaximum = oDataReader.Item("dm_mst_BMIMax") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_TemperatureMin")) Then
                                            .TempratureMinumum = oDataReader.Item("dm_mst_TemperatureMin") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_TemperatureMax")) Then
                                            .TempratureMaximum = oDataReader.Item("dm_mst_TemperatureMax") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_PulseMin")) Then
                                            .PulseMinimum = oDataReader.Item("dm_mst_PulseMin") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_PulseMax")) Then
                                            .PulseMaximum = oDataReader.Item("dm_mst_PulseMax") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_PulseOxMin")) Then
                                            .PulseOXMinimum = oDataReader.Item("dm_mst_PulseOxMin") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_PulseOxMax")) Then
                                            .PulseOXMaximum = oDataReader.Item("dm_mst_PulseOxMax") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_BPSittingMin")) Then
                                            .BPSittingMinimum = oDataReader.Item("dm_mst_BPSittingMin") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_BPSittingMax")) Then
                                            .BPSittingMaximum = oDataReader.Item("dm_mst_BPSittingMax") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_BPStandingMin")) Then
                                            .BPStandingMinimum = oDataReader.Item("dm_mst_BPStandingMin") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_BPStandingMax")) Then
                                            .BPStandingMaximum = oDataReader.Item("dm_mst_BPStandingMax") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_DisplayMessage")) Then
                                            .DisplayMessage = oDataReader.Item("dm_mst_DisplayMessage") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("bIsActive")) Then
                                            .IsActive = oDataReader.Item("bIsActive")
                                        End If

                                        If Not IsDBNull(oDataReader.Item("bIsSpecialAlert")) Then
                                            .bIsSpecialAlert = oDataReader.Item("bIsSpecialAlert")
                                        End If

                                    End With
                                End While
                            End If
                            oDataReader.Close()
                        End If

                        '' FETCH OTHER DETAILS

                        _strSQL = " SELECT ISNULL(dm_mst_Id,0) AS dm_mst_Id, ISNULL(dm_dtl_Id,0) AS dm_dtl_Id, " _
                                & " ISNULL(dm_dtl_CategoryName,'') AS dm_dtl_CategoryName, ISNULL(dm_dtl_ItemName,'') AS dm_dtl_ItemName, " _
                                & " ISNULL(dm_dtl_Operator,'') AS dm_dtl_Operator, ISNULL(dm_dtl_ResultValue1,'') AS dm_dtl_ResultValue1, " _
                                & " ISNULL(dm_dtl_ResultValue2,'') AS dm_dtl_ResultValue2, ISNULL(dm_dtl_Type,0) AS dm_dtl_Type " _
                                & " FROM DM_Criteria_DTL WHERE dm_mst_Id = " & oCriteriaID & ""
                        oDataReader = oDB.ReadQueryRecords(_strSQL)
                        If Not oDataReader Is Nothing Then
                            If oDataReader.HasRows = True Then
                                While oDataReader.Read
                                    oOtherDetail = New Supporting.OtherDetail
                                    oOtherDetail.ItemName = oDataReader.Item("dm_dtl_ItemName")
                                    oOtherDetail.CategoryName = oDataReader.Item("dm_dtl_CategoryName")
                                    oOtherDetail.OperatorName = oDataReader.Item("dm_dtl_Operator")
                                    oOtherDetail.Result1 = oDataReader.Item("dm_dtl_ResultValue1")
                                    oOtherDetail.Result2 = oDataReader.Item("dm_dtl_ResultValue2")
                                    oOtherDetail.DetailType = CType(oDataReader.Item("dm_dtl_Type"), gloStream.DiseaseManagement.Supporting.enumDetailType)
                                    oOtherDetails.Add(oOtherDetail)
                                    oOtherDetail = Nothing
                                End While
                            End If
                            oDataReader.Close()
                        End If

                        '' BIND OTHER DETAILS TO CRITERIA
                        oCriteria.OtherDetails = oOtherDetails

                        '' END OTHER DETAILS

                        '' Added on 20090307
                        ''To Get The Criteria for Patient (if exisits) or from the DM General Criteri
                        oDB.DBParameters.Clear()
                        oDB.DBParameters.Add("@Criteria_ID", oCriteriaID, ParameterDirection.Input, SqlDbType.BigInt)
                        oDB.DBParameters.Add("@PatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt)
                        oDataReader = oDB.ReadRecords("DM_SELECT_Patient_Criteria_MST")
                        Dim objList As myList

                        If Not oDataReader Is Nothing Then
                            If oDataReader.HasRows = True Then
                                While oDataReader.Read
                                    With oCriteria
                                        If Not (IsDBNull(oDataReader.Item("CriteriaID")) And IsDBNull(oDataReader.Item("TemplateID")) And IsDBNull(oDataReader.Item("CategoryID"))) Then
                                            If oDataReader.Item("CategoryID") = TemplateCategoryID.Labs Then
                                                'sarika DM Denormalization 20090404
                                                '.LabOrders.Add(oDataReader.Item("TemplateID"))

                                                objList = New myList
                                                objList.ID = oDataReader.Item("TemplateID")
                                                objList.Index = oDataReader.Item("TemplateID")
                                                objList.Value = oDataReader.Item("TemplateName")
                                                objList.DMTemplateName = oDataReader.Item("TemplateName")
                                                .LabOrders.Add(objList)

                                                objList = Nothing
                                                '----
                                            ElseIf oDataReader.Item("CategoryID") = TemplateCategoryID.Radiology Then

                                                'sarika DM Denormalization 
                                                '.RadiologyOrders.Add(oDataReader.Item("TemplateID"))

                                                objList = New myList
                                                objList.ID = oDataReader.Item("TemplateID")
                                                objList.Index = oDataReader.Item("TemplateID")
                                                objList.Value = oDataReader.Item("TemplateName")
                                                objList.DMTemplateName = oDataReader.Item("TemplateName")
                                                .RadiologyOrders.Add(objList)

                                                objList = Nothing
                                                '----

                                            ElseIf oDataReader.Item("CategoryID") = TemplateCategoryID.Referrals Then


                                                'sarika DM Denormalization 
                                                ' .Referrals.Add(oDataReader.Item("TemplateID"))

                                                objList = New myList
                                                objList.ID = oDataReader.Item("TemplateID")
                                                objList.Index = oDataReader.Item("TemplateID")
                                                objList.Value = oDataReader.Item("TemplateName")
                                                objList.DMTemplateName = oDataReader.Item("TemplateName")
                                                .Referrals.Add(objList)

                                                objList = Nothing
                                                '----


                                            ElseIf oDataReader.Item("CategoryID") = TemplateCategoryID.Guidelines Then
                                                'sarika DM Denormalization 20090331
                                                '.Guidelines.Add(oDataReader.Item("TemplateID"))
                                                objList = New myList
                                                objList.ID = oDataReader.Item("TemplateID")
                                                objList.Index = oDataReader.Item("TemplateID")
                                                objList.DMTemplateName = oDataReader.Item("TemplateName")
                                                objList.Value = oDataReader.Item("TemplateName")

                                                If Not IsDBNull(oDataReader.Item("Template")) Then
                                                    objList.DMTemplate = oDataReader.Item("Template")
                                                Else
                                                    objList.DMTemplate = Nothing
                                                End If


                                                .Guidelines.Add(objList)

                                                objList = Nothing
                                                '---
                                            ElseIf oDataReader.Item("CategoryID") = TemplateCategoryID.Rx Then
                                                'sarika DM Denormalization 20090404
                                                '.RxDrugs.Add(oDataReader.Item("TemplateID"))

                                                objList = New myList
                                                objList.ID = oDataReader.Item("TemplateID")
                                                objList.Index = oDataReader.Item("TemplateID")
                                                objList.Value = oDataReader.Item("TemplateName") & " " & oDataReader.Item("TemplateDtlInfo")
                                                objList.DMTemplateName = oDataReader.Item("TemplateName")
                                                objList.DrugName = oDataReader.Item("TemplateName")
                                                objList.Dosage = oDataReader.Item("TemplateDtlInfo")

                                                'sarika DM Denormalization for Rx 20090410
                                                objList.DrugForm = oDataReader.Item("sDrugForm")
                                                objList.Route = oDataReader.Item("sRoute")
                                                objList.Duration = oDataReader.Item("sDuration")
                                                objList.Frequency = oDataReader.Item("sFrequency")
                                                objList.IsNarcotic = oDataReader.Item("nIsNarcotics")
                                                objList.mpid = oDataReader.Item("mpid")

                                                objList.NDCCode = oDataReader.Item("sNDCCode")

                                                objList.DrugQtyQualifier = oDataReader.Item("sDrugQtyQualifier")
                                                '---

                                                .RxDrugs.Add(objList)

                                                objList = Nothing
                                                '----
                                            ElseIf oDataReader.Item("CategoryID") = TemplateCategoryID.IM Then
                                                '''''''''Added by Chetan as on 09 Oct 2010 - for IM in DM Setup

                                                objList = New myList
                                                objList.ID = oDataReader.Item("TemplateID")             'IM ID                                               
                                                objList.Index = oDataReader.Item("TemplateID")
                                                objList.Value = oDataReader.Item("TemplateName")
                                                objList.DMTemplateName = oDataReader.Item("TemplateName")
                                                objList.DrugForm = oDataReader.Item("sDrugForm")        'ConceptID
                                                objList.Duration = oDataReader.Item("sDuration")        'DescriptionID
                                                objList.Frequency = oDataReader.Item("sFrequency")      'SnoMedID   
                                                objList.DrugQtyQualifier = oDataReader.Item("sDrugQtyQualifier")
                                                objList.Route = oDataReader.Item("sRoute") '''' Vaccine orignal name
                                                objList.NDCCode = oDataReader.Item("sNDCCode")
                                                objList.mpid = oDataReader.Item("mpid")

                                                .IMlst.Add(objList)

                                                objList = Nothing
                                                '----
                                                '''''''''Added by Chetan as on  09 Oct 2010 - for IM in DM Setup



                                            End If
                                        End If
                                    End With
                                End While
                            End If
                            oDataReader.Close()
                        End If
                        oDataReader = Nothing
                        oDB.Disconnect()
                        oDB.Dispose()

                        'Return Object
                        oCriteria.ID = oCriteriaID
                        Return oCriteria
                    Else
                        _ErrorMessage = "Please select Criteria"
                        Return Nothing
                    End If
                Catch ex As SqlException
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    UpdateLog("clsDiseaseManagement -- GetCriteria(2) -- " & ex.ToString)
                    Return Nothing
                Catch ex As Exception
                    UpdateLog("clsDiseaseManagement -- GetCriteria(2) -- " & ex.ToString)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                End Try
            End Function

            Public Function GetExclusion(ByVal oExclusionID As Long, ByVal PatientID As Int64) As gloStream.DiseaseManagement.Supporting.Criteria
                Dim _strSQL As String = ""
                Dim oExclusion As New gloStream.DiseaseManagement.Supporting.Criteria
                Dim oOtherDetails_oExclusion As New gloStream.DiseaseManagement.Supporting.OtherDetails
                Dim oOtherDetail_oExclusion As gloStream.DiseaseManagement.Supporting.OtherDetail
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim oDataReader As SqlClient.SqlDataReader
                Dim _FillDetail As Boolean = False
                Try
                    If Not oExclusionID = 0 Then
                        'Criteria Master Record
                        _strSQL = "SELECT  dm_mst_CriteriaName, DM_Exclusion_DemoVital.dm_mst_AgeMin, DM_Exclusion_DemoVital.dm_mst_AgeMax, " _
                        & " DM_Exclusion_DemoVital.dm_mst_City, DM_Exclusion_DemoVital.dm_mst_Status, DM_Exclusion_DemoVital.dm_mst_Zip, DM_Exclusion_DemoVital.dm_mst_EmplyementStatus, " _
                        & " DM_Exclusion_DemoVital.dm_mst_HeightMin, DM_Exclusion_DemoVital.dm_mst_HeightMax, DM_Exclusion_DemoVital.dm_mst_WeightMin, DM_Exclusion_DemoVital.dm_mst_WeightMax, DM_Exclusion_DemoVital.dm_mst_BMIMin,DM_Exclusion_DemoVital.dm_mst_BMIMax," _
                        & " DM_Exclusion_DemoVital.dm_mst_TemperatureMin, DM_Exclusion_DemoVital.dm_mst_TemperatureMax, DM_Exclusion_DemoVital.dm_mst_PulseMin, DM_Exclusion_DemoVital.dm_mst_PulseMax, DM_Exclusion_DemoVital.dm_mst_PulseOxMin, DM_Exclusion_DemoVital.dm_mst_PulseOxMax, DM_Exclusion_DemoVital.dm_mst_BPSittingMin, DM_Exclusion_DemoVital.dm_mst_BPSittingMax, DM_Exclusion_DemoVital.dm_mst_BPStandingMin, DM_Exclusion_DemoVital.dm_mst_BPStandingMax, dm_mst_DisplayMessage, DM_Exclusion_DemoVital.dm_mst_AgeMax,DATEDIFF(DAY,DATEADD(YEAR,-DM_Exclusion_DemoVital.dm_mst_AgeMin,dbo.gloGetDate()),dbo.gloGetDate()) as AgeMin, DATEDIFF(DAY,DATEADD(YEAR,-DM_Exclusion_DemoVital.dm_mst_AgeMax,dbo.gloGetDate()),dbo.gloGetDate()) as AgeMax,bIsActive  " _
                        & " FROM DM_Criteria_MST LEFT OUTER JOIN DM_Exclusion_DemoVital ON DM_Criteria_MST.dm_mst_id=DM_Exclusion_DemoVital.dm_mst_id " _
                        & " WHERE DM_Criteria_MST.dm_mst_Id = " & oExclusionID & " AND DM_Criteria_MST.dm_mst_CriteriaName IS NOT NULL"
                        oDB.Connect(GetConnectionString)
                        oDataReader = oDB.ReadQueryRecords(_strSQL)
                        If Not oDataReader Is Nothing Then
                            If oDataReader.HasRows = True Then
                                _FillDetail = True
                                While oDataReader.Read
                                    With oExclusion
                                        Dim _tempAge As Double = 0
                                        Dim _tempAgeYr As Double = 0
                                        Dim _tempAgeMnt As Double = 0

                                        .Name = oDataReader.Item("dm_mst_CriteriaName") & ""
                                        If Not IsDBNull(oDataReader.Item("dm_mst_AgeMin")) Then
                                            _tempAge = 0 : _tempAgeYr = 0 : _tempAgeMnt = 0
                                            _tempAge = CDbl(oDataReader.Item("dm_mst_AgeMin") & "")

                                            Dim _strage As String() = _tempAge.ToString().Split(".") '   =tempage .Substring(0,_tempAge.ToString()     

                                            If _strage.Length > 1 Then
                                                _tempAgeYr = CDbl(_strage(0).ToString())

                                                ''GLO2012-0016324 : DM Setup 
                                                ''as concatinating the _age(1) with the 1 "0" so as to retrive 4 digit value
                                                _strage(1) += "0"
                                                _strage(1) = _strage(1).Substring(0, 2)

                                                _tempAgeMnt = CDbl(_strage(1).ToString())
                                            Else
                                                _tempAgeYr = CDbl(_strage(0).ToString())
                                                _tempAgeMnt = 0
                                            End If

                                            .AgeMinimum = Format(CDbl(_tempAgeYr + (_tempAgeMnt / 12)), "#0.0000")
                                            .AgeInDaysMinimum = oDataReader.Item("AgeMin")
                                            '.AgeMinimum = oDataReader.Item("dm_mst_AgeMin") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_AgeMax")) Then

                                            _tempAge = 0 : _tempAgeYr = 0 : _tempAgeMnt = 0
                                            _tempAge = CDbl(oDataReader.Item("dm_mst_AgeMax") & "")

                                            Dim _strage As String() = _tempAge.ToString().Split(".") '   =tempage .Substring(0,_tempAge.ToString()     

                                            If _strage.Length > 1 Then
                                                _tempAgeYr = CDbl(_strage(0).ToString())

                                                ''GLO2012-0016324 : DM Setup 
                                                ''as concatinating the _age(1) with the 1 "0" so as to retrive 4 digit value
                                                _strage(1) += "0"
                                                _strage(1) = _strage(1).Substring(0, 2)

                                                _tempAgeMnt = CDbl(_strage(1).ToString())
                                            Else
                                                _tempAgeYr = CDbl(_strage(0).ToString())
                                                _tempAgeMnt = 0
                                            End If

                                            .AgeMaximum = Format(CDbl(_tempAgeYr + (_tempAgeMnt / 12)), "#0.0000")

                                            .AgeInDaysMaximum = oDataReader.Item("AgeMax")
                                            '.AgeMaximum = oDataReader.Item("dm_mst_AgeMax") & ""
                                        End If


                                        If Not IsDBNull(oDataReader.Item("dm_mst_City")) Then
                                            .City = oDataReader.Item("dm_mst_City") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_Status")) Then
                                            .State = oDataReader.Item("dm_mst_Status") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_Zip")) Then
                                            .Zip = oDataReader.Item("dm_mst_Zip") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_EmplyementStatus")) Then
                                            .EmployeeStatus = oDataReader.Item("dm_mst_EmplyementStatus") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_HeightMin")) Then
                                            .HeightMinimum = oDataReader.Item("dm_mst_HeightMin") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_HeightMax")) Then
                                            .HeightMaximum = oDataReader.Item("dm_mst_HeightMax") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_WeightMin")) Then
                                            .WeightMinimum = oDataReader.Item("dm_mst_WeightMin") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_WeightMax")) Then
                                            .WeightMaximum = oDataReader.Item("dm_mst_WeightMax") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_BMIMin")) Then
                                            .BMIMinimum = oDataReader.Item("dm_mst_BMIMin") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_BMIMax")) Then
                                            .BMIMaximum = oDataReader.Item("dm_mst_BMIMax") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_TemperatureMin")) Then
                                            .TempratureMinumum = oDataReader.Item("dm_mst_TemperatureMin") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_TemperatureMax")) Then
                                            .TempratureMaximum = oDataReader.Item("dm_mst_TemperatureMax") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_PulseMin")) Then
                                            .PulseMinimum = oDataReader.Item("dm_mst_PulseMin") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_PulseMax")) Then
                                            .PulseMaximum = oDataReader.Item("dm_mst_PulseMax") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_PulseOxMin")) Then
                                            .PulseOXMinimum = oDataReader.Item("dm_mst_PulseOxMin") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_PulseOxMax")) Then
                                            .PulseOXMaximum = oDataReader.Item("dm_mst_PulseOxMax") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_BPSittingMin")) Then
                                            .BPSittingMinimum = oDataReader.Item("dm_mst_BPSittingMin") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_BPSittingMax")) Then
                                            .BPSittingMaximum = oDataReader.Item("dm_mst_BPSittingMax") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_BPStandingMin")) Then
                                            .BPStandingMinimum = oDataReader.Item("dm_mst_BPStandingMin") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_BPStandingMax")) Then
                                            .BPStandingMaximum = oDataReader.Item("dm_mst_BPStandingMax") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("dm_mst_DisplayMessage")) Then
                                            .DisplayMessage = oDataReader.Item("dm_mst_DisplayMessage") & ""
                                        End If
                                        If Not IsDBNull(oDataReader.Item("bIsActive")) Then
                                            .IsActive = oDataReader.Item("bIsActive")
                                        End If
                                    End With
                                End While
                            End If
                            oDataReader.Close()
                        End If

                        '' FETCH OTHER DETAILS

                        _strSQL = " SELECT ISNULL(dm_mst_Id,0) AS dm_mst_Id, ISNULL(dm_dtl_Id,0) AS dm_dtl_Id, " _
                                & " ISNULL(dm_dtl_CategoryName,'') AS dm_dtl_CategoryName, ISNULL(dm_dtl_ItemName,'') AS dm_dtl_ItemName, " _
                                & " ISNULL(dm_dtl_Operator,'') AS dm_dtl_Operator, ISNULL(dm_dtl_ResultValue1,'') AS dm_dtl_ResultValue1, " _
                                & " ISNULL(dm_dtl_ResultValue2,'') AS dm_dtl_ResultValue2, ISNULL(dm_dtl_Type,0) AS dm_dtl_Type " _
                                & " FROM DM_Exclusion_DTL WHERE dm_mst_Id = " & oExclusionID & ""
                        oDataReader = oDB.ReadQueryRecords(_strSQL)
                        If Not oDataReader Is Nothing Then
                            If oDataReader.HasRows = True Then
                                While oDataReader.Read
                                    oOtherDetail_oExclusion = New Supporting.OtherDetail
                                    oOtherDetail_oExclusion.ItemName = oDataReader.Item("dm_dtl_ItemName")
                                    oOtherDetail_oExclusion.CategoryName = oDataReader.Item("dm_dtl_CategoryName")
                                    oOtherDetail_oExclusion.OperatorName = oDataReader.Item("dm_dtl_Operator")
                                    oOtherDetail_oExclusion.Result1 = oDataReader.Item("dm_dtl_ResultValue1")
                                    oOtherDetail_oExclusion.Result2 = oDataReader.Item("dm_dtl_ResultValue2")
                                    oOtherDetail_oExclusion.DetailType = CType(oDataReader.Item("dm_dtl_Type"), gloStream.DiseaseManagement.Supporting.enumDetailType)
                                    oOtherDetails_oExclusion.Add(oOtherDetail_oExclusion)
                                    oOtherDetail_oExclusion = Nothing
                                End While
                            End If
                            oDataReader.Close()
                        End If


                        oExclusion.OtherDetails = oOtherDetails_oExclusion
                        oDataReader = Nothing
                        oDB.Disconnect()
                        oDB.Dispose()

                        'Return Object
                        oExclusion.ID = oExclusionID
                        Return oExclusion

                    Else
                        _ErrorMessage = "Please select Criteria"
                        Return Nothing
                    End If
                Catch ex As SqlException
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    UpdateLog("clsDiseaseManagement -- GetCriteria(2) -- " & ex.ToString)
                    Return Nothing
                Catch ex As Exception
                    UpdateLog("clsDiseaseManagement -- GetCriteria(2) -- " & ex.ToString)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                End Try
            End Function

            Public Function DM_GetRuleDetails(ByVal nCriteriaID As Int64) As List(Of gloStream.DiseaseManagement.Supporting.Criteria)

                Dim _ds As DataSet = Nothing
                Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
                Dim oParamater As New gloDatabaseLayer.DBParameters()
                Dim oCriteria As New gloStream.DiseaseManagement.Supporting.Criteria
                Dim oOtherDetails As New gloStream.DiseaseManagement.Supporting.OtherDetails
                Dim oOtherDetail As gloStream.DiseaseManagement.Supporting.OtherDetail
                Dim oExclusion As New gloStream.DiseaseManagement.Supporting.Criteria
                Dim ooExclusionOtherDetails As New gloStream.DiseaseManagement.Supporting.OtherDetails
                Dim oExclusionOtherDetail As gloStream.DiseaseManagement.Supporting.OtherDetail
                Dim sStartdate As String = ""
                Dim sToDate As String = ""
                Dim _criteriaList As New List(Of gloStream.DiseaseManagement.Supporting.Criteria)
                If oDB IsNot Nothing Then
                    If oDB.Connect(False) Then
                        Try

                            oParamater.Add("@nRuleId", nCriteriaID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                            oDB.Retrive("gsp_DM_GetRuleDetails", oParamater, _ds)
                            If (IsNothing(_ds)) Then
                                Return Nothing
                            End If

                            If _ds.Tables.Count > 4 Then

                                _ds.Tables(0).TableName = "TriggerMSTAndVitals"
                                _ds.Tables(1).TableName = "ExclusionMSTAndVitals"
                                _ds.Tables(2).TableName = "TriggerDetails"
                                _ds.Tables(3).TableName = "ExclusionDetails"
                                _ds.Tables(4).TableName = "OrdersToBeGivenDetails"

                                ''''''''''''------------------------Set TriggerMSTAndVitals--------------------
                                If _ds.Tables(0).Rows.Count > 0 Then


                                    For Rowindex As Integer = 0 To _ds.Tables(0).Rows.Count - 1

                                        With oCriteria
                                            Dim _tempAge As Double = 0
                                            Dim _tempAgeYr As Double = 0
                                            Dim _tempAgeMnt As Double = 0

                                            .Name = _ds.Tables(0).Rows(Rowindex)("dm_mst_CriteriaName")
                                            If Not IsDBNull(_ds.Tables(0).Rows(Rowindex)("dm_mst_ageMin_Raw")) Then
                                                _tempAge = 0 : _tempAgeYr = 0 : _tempAgeMnt = 0
                                                _tempAge = CDbl(_ds.Tables(0).Rows(Rowindex)("dm_mst_ageMin_Raw") & "")

                                                Dim _strage As String() = _tempAge.ToString().Split(".") '  

                                                If _strage.Length > 1 Then
                                                    _tempAgeYr = CDbl(_strage(0).ToString())
                                                    _strage(1) += "0"
                                                    _strage(1) = _strage(1).Substring(0, 2)

                                                    _tempAgeMnt = CDbl(_strage(1).ToString())
                                                Else
                                                    _tempAgeYr = CDbl(_strage(0).ToString())
                                                    _tempAgeMnt = 0
                                                End If

                                                .AgeMinimum = _ds.Tables(0).Rows(Rowindex)("dm_mst_ageMin_Raw")
                                                .AgeInDaysMinimum = _ds.Tables(0).Rows(Rowindex)("AgeMin")
                                            End If
                                            If Not IsDBNull(_ds.Tables(0).Rows(Rowindex)("dm_mst_AgeMax_Raw")) Then

                                                _tempAge = 0 : _tempAgeYr = 0 : _tempAgeMnt = 0
                                                _tempAge = CDbl(_ds.Tables(0).Rows(Rowindex)("dm_mst_AgeMax_Raw") & "")

                                                Dim _strage As String() = _tempAge.ToString().Split(".") '   =tempage .Substring(0,_tempAge.ToString()     

                                                If _strage.Length > 1 Then
                                                    _tempAgeYr = CDbl(_strage(0).ToString())

                                                    ''GLO2012-0016324 : DM Setup 
                                                    ''as concatinating the _age(1) with the 1 "0" so as to retrive 4 digit value
                                                    _strage(1) += "0"
                                                    _strage(1) = _strage(1).Substring(0, 2)

                                                    _tempAgeMnt = CDbl(_strage(1).ToString())
                                                Else
                                                    _tempAgeYr = CDbl(_strage(0).ToString())
                                                    _tempAgeMnt = 0
                                                End If

                                                .AgeMaximum = _ds.Tables(0).Rows(Rowindex)("dm_mst_AgeMax_Raw") ''Format(CDbl(_tempAgeYr + (_tempAgeMnt / 12)), "#0.0000")

                                                .AgeInDaysMaximum = _ds.Tables(0).Rows(Rowindex)("AgeMax")
                                                '.AgeMaximum = _ds.Tables(0).Rows(Rowindex)("dm_mst_AgeMax") & ""
                                            End If


                                            If Not IsDBNull(_ds.Tables(0).Rows(Rowindex)("dm_mst_City")) Then
                                                .City = _ds.Tables(0).Rows(Rowindex)("dm_mst_City") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(0).Rows(Rowindex)("dm_mst_Status")) Then
                                                .State = _ds.Tables(0).Rows(Rowindex)("dm_mst_Status") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(0).Rows(Rowindex)("dm_mst_Zip")) Then
                                                .Zip = _ds.Tables(0).Rows(Rowindex)("dm_mst_Zip") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(0).Rows(Rowindex)("dm_mst_EmplyementStatus")) Then
                                                .EmployeeStatus = _ds.Tables(0).Rows(Rowindex)("dm_mst_EmplyementStatus") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(0).Rows(Rowindex)("dm_mst_HeightMin")) Then
                                                .HeightMinimum = _ds.Tables(0).Rows(Rowindex)("dm_mst_HeightMin") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(0).Rows(Rowindex)("dm_mst_HeightMax")) Then
                                                .HeightMaximum = _ds.Tables(0).Rows(Rowindex)("dm_mst_HeightMax") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(0).Rows(Rowindex)("dm_mst_WeightMin")) Then
                                                .WeightMinimum = _ds.Tables(0).Rows(Rowindex)("dm_mst_WeightMin") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(0).Rows(Rowindex)("dm_mst_WeightMax")) Then
                                                .WeightMaximum = _ds.Tables(0).Rows(Rowindex)("dm_mst_WeightMax") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(0).Rows(Rowindex)("dm_mst_BMIMin")) Then
                                                .BMIMinimum = _ds.Tables(0).Rows(Rowindex)("dm_mst_BMIMin") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(0).Rows(Rowindex)("dm_mst_BMIMax")) Then
                                                .BMIMaximum = _ds.Tables(0).Rows(Rowindex)("dm_mst_BMIMax") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(0).Rows(Rowindex)("dm_mst_TemperatureMin")) Then
                                                .TempratureMinumum = _ds.Tables(0).Rows(Rowindex)("dm_mst_TemperatureMin") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(0).Rows(Rowindex)("dm_mst_TemperatureMax")) Then
                                                .TempratureMaximum = _ds.Tables(0).Rows(Rowindex)("dm_mst_TemperatureMax") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(0).Rows(Rowindex)("dm_mst_PulseMin")) Then
                                                .PulseMinimum = _ds.Tables(0).Rows(Rowindex)("dm_mst_PulseMin") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(0).Rows(Rowindex)("dm_mst_PulseMax")) Then
                                                .PulseMaximum = _ds.Tables(0).Rows(Rowindex)("dm_mst_PulseMax") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(0).Rows(Rowindex)("dm_mst_PulseOxMin")) Then
                                                .PulseOXMinimum = _ds.Tables(0).Rows(Rowindex)("dm_mst_PulseOxMin") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(0).Rows(Rowindex)("dm_mst_PulseOxMax")) Then
                                                .PulseOXMaximum = _ds.Tables(0).Rows(Rowindex)("dm_mst_PulseOxMax") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(0).Rows(Rowindex)("dm_mst_BPSittingMin")) Then
                                                .BPSittingMinimum = _ds.Tables(0).Rows(Rowindex)("dm_mst_BPSittingMin") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(0).Rows(Rowindex)("dm_mst_BPSittingMax")) Then
                                                .BPSittingMaximum = _ds.Tables(0).Rows(Rowindex)("dm_mst_BPSittingMax") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(0).Rows(Rowindex)("dm_mst_BPStandingMin")) Then
                                                .BPStandingMinimum = _ds.Tables(0).Rows(Rowindex)("dm_mst_BPStandingMin") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(0).Rows(Rowindex)("dm_mst_BPStandingMax")) Then
                                                .BPStandingMaximum = _ds.Tables(0).Rows(Rowindex)("dm_mst_BPStandingMax") & ""
                                            End If

                                            If Not IsDBNull(_ds.Tables(0).Rows(Rowindex)("dm_mst_BPSitting_ToMin")) Then
                                                .BPSittingToMinimum = _ds.Tables(0).Rows(Rowindex)("dm_mst_BPSitting_ToMin") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(0).Rows(Rowindex)("dm_mst_BPSitting_ToMax")) Then
                                                .BPSittingToMaximum = _ds.Tables(0).Rows(Rowindex)("dm_mst_BPSitting_ToMax") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(0).Rows(Rowindex)("dm_mst_BPStanding_ToMin")) Then
                                                .BPStandingToMinimum = _ds.Tables(0).Rows(Rowindex)("dm_mst_BPStanding_ToMin") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(0).Rows(Rowindex)("dm_mst_BPStanding_ToMax")) Then
                                                .BPStandingToMaximum = _ds.Tables(0).Rows(Rowindex)("dm_mst_BPStanding_ToMax") & ""
                                            End If


                                            If Not IsDBNull(_ds.Tables(0).Rows(Rowindex)("dm_mst_DisplayMessage")) Then
                                                .DisplayMessage = _ds.Tables(0).Rows(Rowindex)("dm_mst_DisplayMessage") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(0).Rows(Rowindex)("bIsActive")) Then
                                                .IsActive = _ds.Tables(0).Rows(Rowindex)("bIsActive")
                                            End If
                                            If Not IsDBNull(_ds.Tables(0).Rows(Rowindex)("bIsOldRule")) Then
                                                .bIsOldRule = _ds.Tables(0).Rows(Rowindex)("bIsOldRule")
                                            End If
                                            If Not IsDBNull(_ds.Tables(0).Rows(Rowindex)("bIsRecurringRule")) Then
                                                .bIsRecuringRule = _ds.Tables(0).Rows(Rowindex)("bIsRecurringRule")
                                            End If
                                            If Not IsDBNull(_ds.Tables(0).Rows(Rowindex)("dtRecurrenceStartDate")) Then
                                                .dtRecurrenceStartDate = _ds.Tables(0).Rows(Rowindex)("dtRecurrenceStartDate")
                                            End If
                                            If Not IsDBNull(_ds.Tables(0).Rows(Rowindex)("dtRecurrenceEndDate")) Then
                                                .dtRecurrenceEndDate = _ds.Tables(0).Rows(Rowindex)("dtRecurrenceEndDate")
                                            End If
                                            If Not IsDBNull(_ds.Tables(0).Rows(Rowindex)("nDurationType")) Then
                                                .nDuratiotype = _ds.Tables(0).Rows(Rowindex)("nDurationType")
                                            End If
                                            If Not IsDBNull(_ds.Tables(0).Rows(Rowindex)("nDurationPeriod")) Then
                                                .nDuratioPeriod = _ds.Tables(0).Rows(Rowindex)("nDurationPeriod")
                                            End If

                                            'ExclusionMSTAndVitals
                                            If Not IsDBNull(_ds.Tables("ExclusionMSTAndVitals").Rows(Rowindex)("bIsSpecialAlert")) Then
                                                .bIsSpecialAlert = _ds.Tables("ExclusionMSTAndVitals").Rows(Rowindex)("bIsSpecialAlert")
                                            End If
                                        End With


                                    Next

                                End If



                                ''''''''''''------------------------ENDSet TriggerMSTAndVitals-------------------------------------------

                                ''----------------------------------------------------------------TriggersDetails---------------------------------------------
                                If _ds.Tables(2).Rows.Count > 0 Then

                                    For Rowindex As Integer = 0 To _ds.Tables(2).Rows.Count - 1
                                        oOtherDetail = New Supporting.OtherDetail
                                        oOtherDetail.ItemName = _ds.Tables(2).Rows(Rowindex)("dm_dtl_ItemName")
                                        oOtherDetail.CategoryName = _ds.Tables(2).Rows(Rowindex)("dm_dtl_CategoryName")
                                        oOtherDetail.OperatorName = _ds.Tables(2).Rows(Rowindex)("dm_dtl_Operator")
                                        oOtherDetail.Result1 = _ds.Tables(2).Rows(Rowindex)("dm_dtl_ResultValue1")
                                        oOtherDetail.Result2 = _ds.Tables(2).Rows(Rowindex)("dm_dtl_ResultValue2")
                                        oOtherDetail.DetailType = CType(_ds.Tables(2).Rows(Rowindex)("dm_dtl_Type"), gloStream.DiseaseManagement.Supporting.enumDetailType)
                                        oOtherDetails.Add(oOtherDetail)
                                        oOtherDetail = Nothing

                                    Next

                                End If
                                oCriteria.OtherDetails = oOtherDetails

                                ''----------------------------------------------------------------END Set Trigger Details-----------------------------------------------------------


                                ''---------------------------------------------------------------- Set Trigger OrdersTo be Geiven Details---------------------------------------------
                                Dim objList As myList
                                If _ds.Tables(4).Rows.Count > 0 Then

                                    For Rowindex As Integer = 0 To _ds.Tables(4).Rows.Count - 1

                                        With oCriteria
                                            If Not (IsDBNull(_ds.Tables(4).Rows(Rowindex)("CriteriaID")) And IsDBNull(_ds.Tables(4).Rows(Rowindex)("TemplateID")) And IsDBNull(_ds.Tables(4).Rows(Rowindex)("CategoryID"))) Then
                                                If _ds.Tables(4).Rows(Rowindex)("CategoryID") = TemplateCategoryID.Labs Then
                                                    'sarika DM Denormalization 20090404
                                                    '.LabOrders.Add(_ds.Tables(4).Rows(Rowindex)("TemplateID"))

                                                    objList = New myList
                                                    objList.ID = _ds.Tables(4).Rows(Rowindex)("TemplateID")
                                                    objList.Index = _ds.Tables(4).Rows(Rowindex)("TemplateID")
                                                    objList.Value = _ds.Tables(4).Rows(Rowindex)("TemplateName")
                                                    objList.DMTemplateName = _ds.Tables(4).Rows(Rowindex)("TemplateName")
                                                    .LabOrders.Add(objList)

                                                    objList = Nothing
                                                    '----
                                                ElseIf _ds.Tables(4).Rows(Rowindex)("CategoryID") = TemplateCategoryID.Radiology Then

                                                    'sarika DM Denormalization 
                                                    '.RadiologyOrders.Add(_ds.Tables(4).Rows(Rowindex)("TemplateID"))

                                                    objList = New myList
                                                    objList.ID = _ds.Tables(4).Rows(Rowindex)("TemplateID")
                                                    objList.Index = _ds.Tables(4).Rows(Rowindex)("TemplateID")
                                                    objList.Value = _ds.Tables(4).Rows(Rowindex)("TemplateName")
                                                    objList.DMTemplateName = _ds.Tables(4).Rows(Rowindex)("TemplateName")
                                                    .RadiologyOrders.Add(objList)

                                                    objList = Nothing
                                                    '----

                                                ElseIf _ds.Tables(4).Rows(Rowindex)("CategoryID") = TemplateCategoryID.Referrals Then


                                                    'sarika DM Denormalization 
                                                    ' .Referrals.Add(_ds.Tables(4).Rows(Rowindex)("TemplateID"))

                                                    objList = New myList
                                                    objList.ID = _ds.Tables(4).Rows(Rowindex)("TemplateID")
                                                    objList.Index = _ds.Tables(4).Rows(Rowindex)("TemplateID")
                                                    objList.Value = _ds.Tables(4).Rows(Rowindex)("TemplateName")
                                                    objList.DMTemplateName = _ds.Tables(4).Rows(Rowindex)("TemplateName")
                                                    .Referrals.Add(objList)

                                                    objList = Nothing
                                                    '----


                                                ElseIf _ds.Tables(4).Rows(Rowindex)("CategoryID") = TemplateCategoryID.Guidelines Then
                                                    'sarika DM Denormalization 20090331
                                                    '.Guidelines.Add(_ds.Tables(4).Rows(Rowindex)("TemplateID"))
                                                    objList = New myList
                                                    objList.ID = _ds.Tables(4).Rows(Rowindex)("TemplateID")
                                                    objList.Index = _ds.Tables(4).Rows(Rowindex)("TemplateID")
                                                    objList.DMTemplateName = _ds.Tables(4).Rows(Rowindex)("TemplateName")
                                                    objList.Value = _ds.Tables(4).Rows(Rowindex)("TemplateName")

                                                    If Not IsDBNull(_ds.Tables(4).Rows(Rowindex)("Template")) Then
                                                        objList.DMTemplate = _ds.Tables(4).Rows(Rowindex)("Template")
                                                    Else
                                                        objList.DMTemplate = Nothing
                                                    End If


                                                    .Guidelines.Add(objList)

                                                    objList = Nothing
                                                    '---
                                                ElseIf _ds.Tables(4).Rows(Rowindex)("CategoryID") = TemplateCategoryID.Rx Then
                                                    'sarika DM Denormalization 20090404
                                                    '.RxDrugs.Add(_ds.Tables(4).Rows(Rowindex)("TemplateID"))

                                                    objList = New myList
                                                    objList.ID = _ds.Tables(4).Rows(Rowindex)("TemplateID")
                                                    objList.Index = _ds.Tables(4).Rows(Rowindex)("TemplateID")
                                                    objList.Value = _ds.Tables(4).Rows(Rowindex)("TemplateName") & " " & _ds.Tables(4).Rows(Rowindex)("TemplateDtlInfo")
                                                    objList.DMTemplateName = _ds.Tables(4).Rows(Rowindex)("TemplateName")
                                                    objList.DrugName = _ds.Tables(4).Rows(Rowindex)("TemplateName")
                                                    objList.Dosage = _ds.Tables(4).Rows(Rowindex)("TemplateDtlInfo")

                                                    'sarika DM Denormalization for Rx 20090410
                                                    objList.DrugForm = _ds.Tables(4).Rows(Rowindex)("sDrugForm")
                                                    objList.Route = _ds.Tables(4).Rows(Rowindex)("sRoute")
                                                    objList.Duration = _ds.Tables(4).Rows(Rowindex)("sDuration")
                                                    objList.Frequency = _ds.Tables(4).Rows(Rowindex)("sFrequency")
                                                    objList.IsNarcotic = _ds.Tables(4).Rows(Rowindex)("nIsNarcotics")
                                                    objList.mpid = _ds.Tables(4).Rows(Rowindex)("mpid")

                                                    objList.NDCCode = _ds.Tables(4).Rows(Rowindex)("sNDCCode")

                                                    objList.DrugQtyQualifier = _ds.Tables(4).Rows(Rowindex)("sDrugQtyQualifier")
                                                    '---

                                                    .RxDrugs.Add(objList)

                                                    objList = Nothing
                                                    '----
                                                ElseIf _ds.Tables(4).Rows(Rowindex)("CategoryID") = TemplateCategoryID.IM Then

                                                    objList = New myList
                                                    objList.ID = _ds.Tables(4).Rows(Rowindex)("TemplateID")             'IM ID                                               
                                                    objList.Index = _ds.Tables(4).Rows(Rowindex)("TemplateID")
                                                    objList.Value = _ds.Tables(4).Rows(Rowindex)("TemplateName")
                                                    objList.DMTemplateName = _ds.Tables(4).Rows(Rowindex)("TemplateName")
                                                    objList.DrugForm = _ds.Tables(4).Rows(Rowindex)("sDrugForm")        'ConceptID
                                                    objList.Duration = _ds.Tables(4).Rows(Rowindex)("sDuration")        'DescriptionID
                                                    objList.Frequency = _ds.Tables(4).Rows(Rowindex)("sFrequency")      'SnoMedID   
                                                    objList.DrugQtyQualifier = _ds.Tables(4).Rows(Rowindex)("sDrugQtyQualifier")
                                                    objList.Route = _ds.Tables(4).Rows(Rowindex)("sRoute") '''' Vaccine orignal name
                                                    objList.NDCCode = _ds.Tables(4).Rows(Rowindex)("sNDCCode")
                                                    objList.mpid = _ds.Tables(4).Rows(Rowindex)("mpid")

                                                    .IMlst.Add(objList)
                                                    objList = Nothing

                                                End If
                                            End If
                                        End With
                                    Next

                                End If
                                ''---------------------------------------------------------------- END Set Trigger OrdersTo be Geiven Details---------------------------------------------
                                oCriteria.ID = nCriteriaID


                                oCriteria.IsTriggerObject = True
                                _criteriaList.Add(oCriteria)

                                ''-------------------------------------------------------Set ExclusionMSTAndVitals------------------------------------------------------------------
                                If _ds.Tables(1).Rows.Count > 0 Then


                                    For Rowindex As Integer = 0 To _ds.Tables(1).Rows.Count - 1

                                        With oExclusion
                                            Dim _tempAge As Double = 0
                                            Dim _tempAgeYr As Double = 0
                                            Dim _tempAgeMnt As Double = 0
                                            .Name = _ds.Tables(1).Rows(Rowindex)("dm_mst_CriteriaName")
                                            If Not IsDBNull(_ds.Tables(1).Rows(Rowindex)("dm_mst_ageMin_Raw")) Then
                                                _tempAge = 0 : _tempAgeYr = 0 : _tempAgeMnt = 0
                                                _tempAge = CDbl(_ds.Tables(1).Rows(Rowindex)("dm_mst_ageMin_Raw") & "")

                                                Dim _strage As String() = _tempAge.ToString().Split(".") '  

                                                If _strage.Length > 1 Then
                                                    _tempAgeYr = CDbl(_strage(0).ToString())
                                                    _strage(1) += "0"
                                                    _strage(1) = _strage(1).Substring(0, 2)

                                                    _tempAgeMnt = CDbl(_strage(1).ToString())
                                                Else
                                                    _tempAgeYr = CDbl(_strage(0).ToString())
                                                    _tempAgeMnt = 0
                                                End If

                                                .AgeMinimum = _ds.Tables(1).Rows(Rowindex)("dm_mst_ageMin_Raw") ''Format(CDbl(_tempAgeYr + (_tempAgeMnt / 12)), "#0.0000")
                                                .AgeInDaysMinimum = _ds.Tables(1).Rows(Rowindex)("AgeMin")
                                            End If
                                            If Not IsDBNull(_ds.Tables(1).Rows(Rowindex)("dm_mst_AgeMax_Raw")) Then

                                                _tempAge = 0 : _tempAgeYr = 0 : _tempAgeMnt = 0
                                                _tempAge = CDbl(_ds.Tables(1).Rows(Rowindex)("dm_mst_AgeMax_Raw") & "")

                                                Dim _strage As String() = _tempAge.ToString().Split(".") '   =tempage .Substring(0,_tempAge.ToString()     

                                                If _strage.Length > 1 Then
                                                    _tempAgeYr = CDbl(_strage(0).ToString())

                                                    ''GLO2012-0016324 : DM Setup 
                                                    ''as concatinating the _age(1) with the 1 "0" so as to retrive 4 digit value
                                                    _strage(1) += "0"
                                                    _strage(1) = _strage(1).Substring(0, 2)

                                                    _tempAgeMnt = CDbl(_strage(1).ToString())
                                                Else
                                                    _tempAgeYr = CDbl(_strage(0).ToString())
                                                    _tempAgeMnt = 0
                                                End If

                                                .AgeMaximum = _ds.Tables(1).Rows(Rowindex)("dm_mst_AgeMax_Raw") ''Format(CDbl(_tempAgeYr + (_tempAgeMnt / 12)), "#0.0000")

                                                .AgeInDaysMaximum = _ds.Tables(1).Rows(Rowindex)("AgeMax")
                                                '.AgeMaximum = _ds.Tables(1).Rows(Rowindex)("dm_mst_AgeMax") & ""
                                            End If


                                            If Not IsDBNull(_ds.Tables(1).Rows(Rowindex)("dm_mst_City")) Then
                                                .City = _ds.Tables(1).Rows(Rowindex)("dm_mst_City") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(1).Rows(Rowindex)("dm_mst_Status")) Then
                                                .State = _ds.Tables(1).Rows(Rowindex)("dm_mst_Status") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(1).Rows(Rowindex)("dm_mst_Zip")) Then
                                                .Zip = _ds.Tables(1).Rows(Rowindex)("dm_mst_Zip") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(1).Rows(Rowindex)("dm_mst_EmplyementStatus")) Then
                                                .EmployeeStatus = _ds.Tables(1).Rows(Rowindex)("dm_mst_EmplyementStatus") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(1).Rows(Rowindex)("dm_mst_HeightMin")) Then
                                                .HeightMinimum = _ds.Tables(1).Rows(Rowindex)("dm_mst_HeightMin") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(1).Rows(Rowindex)("dm_mst_HeightMax")) Then
                                                .HeightMaximum = _ds.Tables(1).Rows(Rowindex)("dm_mst_HeightMax") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(1).Rows(Rowindex)("dm_mst_WeightMin")) Then
                                                .WeightMinimum = _ds.Tables(1).Rows(Rowindex)("dm_mst_WeightMin") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(1).Rows(Rowindex)("dm_mst_WeightMax")) Then
                                                .WeightMaximum = _ds.Tables(1).Rows(Rowindex)("dm_mst_WeightMax") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(1).Rows(Rowindex)("dm_mst_BMIMin")) Then
                                                .BMIMinimum = _ds.Tables(1).Rows(Rowindex)("dm_mst_BMIMin") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(1).Rows(Rowindex)("dm_mst_BMIMax")) Then
                                                .BMIMaximum = _ds.Tables(1).Rows(Rowindex)("dm_mst_BMIMax") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(1).Rows(Rowindex)("dm_mst_TemperatureMin")) Then
                                                .TempratureMinumum = _ds.Tables(1).Rows(Rowindex)("dm_mst_TemperatureMin") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(1).Rows(Rowindex)("dm_mst_TemperatureMax")) Then
                                                .TempratureMaximum = _ds.Tables(1).Rows(Rowindex)("dm_mst_TemperatureMax") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(1).Rows(Rowindex)("dm_mst_PulseMin")) Then
                                                .PulseMinimum = _ds.Tables(1).Rows(Rowindex)("dm_mst_PulseMin") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(1).Rows(Rowindex)("dm_mst_PulseMax")) Then
                                                .PulseMaximum = _ds.Tables(1).Rows(Rowindex)("dm_mst_PulseMax") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(1).Rows(Rowindex)("dm_mst_PulseOxMin")) Then
                                                .PulseOXMinimum = _ds.Tables(1).Rows(Rowindex)("dm_mst_PulseOxMin") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(1).Rows(Rowindex)("dm_mst_PulseOxMax")) Then
                                                .PulseOXMaximum = _ds.Tables(1).Rows(Rowindex)("dm_mst_PulseOxMax") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(1).Rows(Rowindex)("dm_mst_BPSittingMin")) Then
                                                .BPSittingMinimum = _ds.Tables(1).Rows(Rowindex)("dm_mst_BPSittingMin") & ""
                                            End If

                                            If Not IsDBNull(_ds.Tables(1).Rows(Rowindex)("dm_mst_BPSitting_ToMin")) Then
                                                .BPSittingToMinimum = _ds.Tables(1).Rows(Rowindex)("dm_mst_BPSitting_ToMin") & ""
                                            End If

                                            If Not IsDBNull(_ds.Tables(1).Rows(Rowindex)("dm_mst_BPSittingMax")) Then
                                                .BPSittingMaximum = _ds.Tables(1).Rows(Rowindex)("dm_mst_BPSittingMax") & ""
                                            End If

                                            If Not IsDBNull(_ds.Tables(1).Rows(Rowindex)("dm_mst_BPSitting_ToMax")) Then
                                                .BPSittingToMaximum = _ds.Tables(1).Rows(Rowindex)("dm_mst_BPSitting_ToMax") & ""
                                            End If


                                            If Not IsDBNull(_ds.Tables(1).Rows(Rowindex)("dm_mst_BPStandingMin")) Then
                                                .BPStandingMinimum = _ds.Tables(1).Rows(Rowindex)("dm_mst_BPStandingMin") & ""
                                            End If

                                            If Not IsDBNull(_ds.Tables(1).Rows(Rowindex)("dm_mst_BPStanding_ToMin")) Then
                                                .BPStandingToMinimum = _ds.Tables(1).Rows(Rowindex)("dm_mst_BPStanding_ToMin") & ""
                                            End If

                                            If Not IsDBNull(_ds.Tables(1).Rows(Rowindex)("dm_mst_BPStandingMax")) Then
                                                .BPStandingMaximum = _ds.Tables(1).Rows(Rowindex)("dm_mst_BPStandingMax") & ""
                                            End If

                                            If Not IsDBNull(_ds.Tables(1).Rows(Rowindex)("dm_mst_BPStanding_ToMax")) Then
                                                .BPStandingToMaximum = _ds.Tables(1).Rows(Rowindex)("dm_mst_BPStanding_ToMax") & ""
                                            End If

                                            If Not IsDBNull(_ds.Tables(1).Rows(Rowindex)("dm_mst_DisplayMessage")) Then
                                                .DisplayMessage = _ds.Tables(1).Rows(Rowindex)("dm_mst_DisplayMessage") & ""
                                            End If
                                            If Not IsDBNull(_ds.Tables(1).Rows(Rowindex)("bIsActive")) Then
                                                .IsActive = _ds.Tables(1).Rows(Rowindex)("bIsActive")
                                            End If
                                        End With


                                    Next

                                End If



                                ''''''''''''------------------------ENDSet TriggerMSTAndVitals-------------------------------------------

                                ''----------------------------------------------------------------TriggersDetails---------------------------------------------
                                If _ds.Tables(3).Rows.Count > 0 Then

                                    For Rowindex As Integer = 0 To _ds.Tables(3).Rows.Count - 1
                                        oExclusionOtherDetail = New Supporting.OtherDetail
                                        oExclusionOtherDetail.ItemName = _ds.Tables(3).Rows(Rowindex)("dm_dtl_ItemName")
                                        oExclusionOtherDetail.CategoryName = _ds.Tables(3).Rows(Rowindex)("dm_dtl_CategoryName")
                                        oExclusionOtherDetail.OperatorName = _ds.Tables(3).Rows(Rowindex)("dm_dtl_Operator")
                                        oExclusionOtherDetail.Result1 = _ds.Tables(3).Rows(Rowindex)("dm_dtl_ResultValue1")
                                        oExclusionOtherDetail.Result2 = _ds.Tables(3).Rows(Rowindex)("dm_dtl_ResultValue2")
                                        oExclusionOtherDetail.DetailType = CType(_ds.Tables(3).Rows(Rowindex)("dm_dtl_Type"), gloStream.DiseaseManagement.Supporting.enumDetailType)
                                        ooExclusionOtherDetails.Add(oExclusionOtherDetail)
                                        oExclusionOtherDetail = Nothing


                                    Next

                                End If
                                oExclusion.OtherDetails = ooExclusionOtherDetails
                                oExclusion.ID = nCriteriaID
                                ''----------------------------------------------------------------END Set Trigger Details-----------------------------------------------------------

                                oExclusion.IsTriggerObject = False
                                _criteriaList.Add(oExclusion)

                            End If

                            Return _criteriaList

                        Catch ex As gloDatabaseLayer.DBException
                            ex.ERROR_Log(ex.ToString())
                            oCriteria = Nothing
                            oOtherDetails = Nothing
                            oOtherDetail = Nothing
                            oExclusion = Nothing
                            ooExclusionOtherDetails = Nothing
                            oExclusionOtherDetail = Nothing

                            If _ds IsNot Nothing Then
                                _ds.Dispose()
                                _ds = Nothing
                            End If
                            If oParamater IsNot Nothing Then
                                oParamater.Dispose()
                                oParamater = Nothing
                            End If

                            If oDB IsNot Nothing Then
                                oDB.Disconnect()
                                oDB.Dispose()
                                oDB = Nothing
                            End If
                            Return Nothing
                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
                            oCriteria = Nothing
                            oOtherDetails = Nothing
                            oOtherDetail = Nothing
                            oExclusion = Nothing
                            ooExclusionOtherDetails = Nothing
                            oExclusionOtherDetail = Nothing
                            If _ds IsNot Nothing Then
                                _ds.Dispose()
                                _ds = Nothing
                            End If

                            If oParamater IsNot Nothing Then
                                oParamater.Dispose()
                                oParamater = Nothing
                            End If
                            If oDB IsNot Nothing Then
                                oDB.Disconnect()
                                oDB.Dispose()
                                oDB = Nothing
                            End If
                            Return Nothing
                        Finally
                            If oCriteria IsNot Nothing Then
                                'oCriteria.Dispose()
                                oCriteria = Nothing
                            End If

                            oOtherDetails = Nothing
                            oOtherDetail = Nothing
                            oExclusion = Nothing
                            ooExclusionOtherDetails = Nothing
                            oExclusionOtherDetail = Nothing
                            If _ds IsNot Nothing Then
                                _ds.Dispose()
                                _ds = Nothing
                            End If
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
                    End If
                End If

                Return _criteriaList

            End Function









            Public Function GetGuidLine(ByVal GuidLineID As Long) As String

                'Dim oCriteria As New gloStream.DiseaseManagement.Supporting.Criteria
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                oDB.Connect(GetConnectionString())
                'sarika DM Denormalization 20090331

                Dim ReferralName As String = oDB.ExecuteQueryScaler("select sTemplateName from TemplateGallery_MST where nTemplateID = " & GuidLineID & " ")
                '  Dim ReferralName As String = oDB.ExecuteQueryScaler("select sTemplateName from DM_Templates_DTL where dm_Templatedtl_TemplateID = " & GuidLineID & " ")

                '---
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
                Return ReferralName
            End Function
            Public Function GetCriteriaName(ByVal oCriteriaID As Long) As String
                Dim _strSQL As String = ""
                Dim _Result As String = ""

                Try
                    If Not oCriteriaID = 0 Then
                        'Criteria Master Record
                        _strSQL = "SELECT dm_mst_CriteriaName FROM DM_Criteria_MST WHERE dm_mst_Id = " & oCriteriaID & " AND dm_mst_CriteriaName IS NOT NULL"
                        Dim oDB As New gloStream.gloDataBase.gloDataBase
                        oDB.Connect(GetConnectionString)
                        _Result = oDB.ExecuteQueryScaler(_strSQL)
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                        'Return Object
                        Return _Result
                    Else
                        _ErrorMessage = "Please select Criteria"
                        Return Nothing
                    End If
                Catch ex As SqlException
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    UpdateLog("clsDiseaseManagement -- GetCriteriaName -- " & ex.ToString)
                    Return Nothing
                Catch ex As Exception
                    UpdateLog("clsDiseaseManagement -- GetCriteriaName -- " & ex.ToString)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                End Try
            End Function

            Public Function GetCriteriaMessage(ByVal oCriteriaID As Long) As String
                Dim _strSQL As String = ""
                Dim _Result As String = ""

                Try
                    If Not oCriteriaID = 0 Then
                        'Criteria Master Record
                        _strSQL = "SELECT dm_mst_CriteriaName FROM DM_Criteria_MST WHERE dm_mst_Id = " & oCriteriaID & " AND dm_mst_DisplayMessage IS NOT NULL"
                        Dim oDB As New gloStream.gloDataBase.gloDataBase
                        oDB.Connect(GetConnectionString)
                        _Result = oDB.ExecuteQueryScaler(_strSQL)
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                        'Return Object
                        Return _Result
                    Else
                        _ErrorMessage = "Please select Criteria"
                        Return Nothing
                    End If
                Catch ex As SqlException
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    UpdateLog("clsDiseaseManagement -- GetCriteriaName -- " & ex.ToString)
                    Return Nothing
                Catch ex As Exception
                    UpdateLog("clsDiseaseManagement -- GetCriteriaName -- " & ex.ToString)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                End Try
            End Function
            ''added for patient switching optimization  
            Public Function GetCriteriaMessages(ByVal oCriteriaID As String) As DataTable
                Dim _strSQL As String = ""
                Dim _Result As String = ""
                Dim oParamater As New gloDatabaseLayer.DBParameters()
                Dim dt As DataTable = Nothing
                Try
                    If Not oCriteriaID = "0" Then
                        'Criteria Master Record
                        '_strSQL = "SELECT dm_mst_CriteriaName FROM DM_Criteria_MST WHERE dm_mst_Id in (" & oCriteriaID & ") AND dm_mst_DisplayMessage IS NOT NULL"
                        ' Dim oDB As New gloStream.gloDataBase.gloDataBase
                        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
                        Dim _dtCriteriaID As DataTable = New DataTable()
                        If (oDB.Connect(False)) Then


                            _dtCriteriaID.Columns.Add("Dm_mst_id", GetType(Long))
                            Dim strspl As String() = oCriteriaID.Split(",")
                            If (strspl.Length > 0) Then
                                For Len As Int32 = 0 To strspl.Length - 1
                                    _dtCriteriaID.Rows.Add(strspl(Len))
                                Next

                                strspl = Nothing
                            End If
                            oParamater.Add("@tvp_Dm_mst_id", _dtCriteriaID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Structured)
                            oDB.Retrive("gsp_GetDmCriteria", oParamater, dt)
                            '' dt = oDB.(_strSQL)
                            If Not IsNothing(oDB) Then
                                oDB.Disconnect()
                                oDB.Dispose()
                                oDB = Nothing

                            End If
                        Else

                            If oDB IsNot Nothing Then
                                oDB.Dispose()
                                oDB = Nothing
                            End If

                        End If
                        If Not IsNothing(_dtCriteriaID) Then
                            _dtCriteriaID.Dispose()
                            _dtCriteriaID = Nothing
                        End If
                        If Not IsNothing(oParamater) Then
                            oParamater.Clear()
                            oParamater.Dispose()
                            oParamater = Nothing
                        End If
                        'Return Object
                        Return dt
                    Else
                        _ErrorMessage = "Please select Criteria"
                        Return Nothing
                    End If
                Catch ex As SqlException
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    UpdateLog("clsDiseaseManagement -- GetCriteriaName -- " & ex.ToString)
                    Return Nothing
                Catch ex As Exception
                    UpdateLog("clsDiseaseManagement -- GetCriteriaName -- " & ex.ToString)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                End Try
            End Function
            Public Function GetCriteraList() As gloStream.DiseaseManagement.Supporting.ItemDetails
                'criteria master 
                'id, name
                Dim _strSQL As String = ""
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim oDataReader As SqlClient.SqlDataReader = Nothing
                Dim oItemDetails As New gloStream.DiseaseManagement.Supporting.ItemDetails

                Try
                    oDB.Connect(GetConnectionString)

                    '_strSQL = "SELECT dm_mst_Id, dm_mst_CriteriaName FROM DM_Criteria_MST"
                    _strSQL = "SELECT dm_mst_Id, dm_mst_CriteriaName FROM DM_Criteria_MST WHERE dm_mst_PatientID = 0 or dm_mst_PatientID IS NULL"

                    oDataReader = oDB.ReadQueryRecords(_strSQL)

                    If Not oDataReader Is Nothing Then
                        If oDataReader.HasRows = True Then
                            While oDataReader.Read
                                With oItemDetails
                                    If Not (IsDBNull(oDataReader.Item("dm_mst_Id")) And IsDBNull(oDataReader.Item("dm_mst_CriteriaName"))) Then
                                        .Add(oDataReader.Item("dm_mst_Id"), oDataReader.Item("dm_mst_CriteriaName"))
                                    End If
                                End With
                            End While
                        End If
                        oDataReader.Close()
                    End If

                    Return oItemDetails

                Catch ex As SqlException
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    UpdateLog("clsDiseaseManagement -- GetCriteraList -- " & ex.ToString)
                    Return Nothing
                Catch ex As Exception
                    UpdateLog("clsDiseaseManagement -- GetCriteraList -- " & ex.ToString)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    oDataReader.Close()
                    oDB.Disconnect()
                    oDB.Dispose()
                    oDB = Nothing
                    oDataReader = Nothing

                End Try

            End Function
            'SHUBHANGI 20091005
            'To Apply In string search c1DiseaseList 
            Public Function GetCritera() As DataTable

                Dim _strSQL As String = ""
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim dtCriteria As DataTable = Nothing
                ' Dim oItemDetails As New gloStream.DiseaseManagement.Supporting.ItemDetails

                Try
                    oDB.Connect(GetConnectionString)

                    '_strSQL = "SELECT dm_mst_Id, dm_mst_CriteriaName FROM DM_Criteria_MST WHERE dm_mst_PatientID = 0 or dm_mst_PatientID IS NULL"
                    'dtCriteria = oDB.ReadQueryDataTable(_strSQL)

                    dtCriteria = oDB.ReadData("gsp_DM_GetRules")



                Catch ex As SqlException
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    UpdateLog("clsDiseaseManagement -- GetCriteraList -- " & ex.ToString)
                Catch ex As Exception
                    UpdateLog("clsDiseaseManagement -- GetCriteraList -- " & ex.ToString)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    oDB.Disconnect()
                    oDB.Dispose()
                    oDB = Nothing
                    ' oDataReader.Close()
                End Try
                Return dtCriteria
            End Function
            Public Function GetActiveProvider() As DataTable
                Dim oDB As gloStream.gloDataBase.gloDataBase = Nothing
                Dim strSelect As String
                Dim dt As DataTable = Nothing
                Try
                    oDB = New gloDataBase.gloDataBase()
                    oDB.Connect(GetConnectionString)
                    strSelect = "select nProviderID,isnull(sFirstName,'') + ' ' + CASE ISNULL(sMiddleName,'') WHEN  '' THEN '' When sMiddleName then  " _
                                        & "sMiddleName +  ' ' END +  isnull(sLastName,'') as ProviderName  from Provider_MST WHERE bIsblocked =0 Order by ProviderName"

                    dt = oDB.ReadQueryDataTable(strSelect)
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
                Finally
                    strSelect = Nothing
                    oDB.Disconnect()
                    oDB.Dispose()
                    oDB = Nothing
                End Try

                Return dt


            End Function

            Public Function GetRuleReferenceInfo(ByVal recommendationRuleId As Int64) As DataTable

                Dim _db As gloStream.gloDataBase.gloDataBase = Nothing
                Dim _dtRuleRefInfo As DataTable = Nothing
                Dim _strQuery As String = ""


                Try


                    If recommendationRuleId > 0 Then

                        _strQuery =
                        " SELECT nRefInfoId,dm_mst_Id, " &
                        " ISNULL(sBibliographicCitatation,'') AS  BibliographicCitatation," &
                        " ISNULL(sInterventionDeveloper, '') AS InterventionDeveloper," &
                        " ISNULL(sFundingSource, '') AS FundingSource," &
                        " ISNULL(sRelease, '') AS Release," &
                        " ISNULL(sRevisionDates, '') AS RevisionDates, " &
                        " dtCreatedDateTime " &
                        " FROM dbo.DM_Criteria_RefInfo WHERE dbo.DM_Criteria_RefInfo.dm_mst_Id = " & recommendationRuleId & " "

                        _db = New gloStream.gloDataBase.gloDataBase()
                        _db.Connect(GetConnectionString)
                        _dtRuleRefInfo = _db.ReadQueryDataTable(_strQuery)
                        _db.Disconnect()

                    End If

                Catch ex As SqlException
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    UpdateLog("clsDiseaseManagement -- GetRuleReferenceInfo -- " & ex.ToString)
                Catch ex As Exception
                    UpdateLog("clsDiseaseManagement -- GetRuleReferenceInfo -- " & ex.ToString)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If (IsNothing(_db) = False) Then
                        _db.Disconnect()
                        _db.Dispose()
                        _db = Nothing

                    End If
                End Try

                Return _dtRuleRefInfo

            End Function

            ' Not in use
            Public Function FindGuidelinesForMultiplePatient(ByVal CriteriaID As Long) As Collection
                'A & D
                Dim _strSQL As String = ""
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim oDataReader As SqlClient.SqlDataReader = Nothing

                Dim _AgeMin As Int16 = 0
                Dim _AgeMax As Int16 = 0
                Dim _Geneder As String = ""
                Dim _Race As String = ""
                Dim _MaritalStatus As String = ""
                Dim _City As String = ""
                Dim _State As String = ""
                Dim _ZipCode As String = ""
                Dim _EmpStatus As String = ""
                Dim _HeightFtMin As Double = 0
                Dim _HeightFtMax As Double = 0
                Dim _HeightInchMin As Double = 0
                Dim _HeightInchMax As Double = 0
                Dim _WeightMin As Double = 0
                Dim _WeightMax As Double = 0
                Dim _PulseMin As Double = 0
                Dim _PulseMax As Double = 0
                Dim _PulseOXMin As Double = 0
                Dim _PulseOXMax As Double = 0
                Dim _BPSittingMin As Double = 0
                Dim _BPSittingMax As Double = 0
                Dim _BPStandingMin As Double = 0
                Dim _BPStandingMax As Double = 0
                Dim _BMIMin As Double = 0
                Dim _BMIMax As Double = 0
                Dim _TempMin As Double = 0
                Dim _TempMax As Double = 0
                Dim _HaveVital As Boolean = False
                Dim _HaveVitalHeight As Boolean = False

                Dim _Histories As New Collection
                Dim _Drugs As New Collection
                Dim _ICD9s As New Collection
                Dim _CPTs As New Collection
                Dim _Labs As New Collection
                Dim _LabModule As New Collection

                Dim _FinalPatientID As New Collection
                Dim _PrimaryPatientID As New Collection
                Dim _TempPatientID As New Collection
                Dim _PrimaryINPatientID As String = ""

                Try
                    Application.DoEvents()
                    RaiseEvent StartCriteria(True)
                    '*********************>>>--- READ CRITERIA CONDITION ---<<<*******************************
                    'connect to the database    
                    oDB.Connect(GetConnectionString)

                    'set the query string to retrieve the Patient record from the Patient table from the PatientID passed
                    _strSQL = "SELECT dm_mst_Id,dm_mst_AgeMin,dm_mst_AgeMax, " _
                    & " dm_mst_Gender,dm_mst_Race,dm_mst_MaritalStatus, " _
                    & " dm_mst_City,dm_mst_Status,dm_mst_Zip, " _
                    & " dm_mst_EmplyementStatus,dm_mst_HeightMin,dm_mst_HeightMax, " _
                    & " dm_mst_WeightMin,dm_mst_WeightMax, " _
                    & " dm_mst_BMIMin,dm_mst_BMIMax,dm_mst_TemperatureMin,dm_mst_TemperatureMax, " _
                    & " dm_mst_PulseMin,dm_mst_PulseMax,dm_mst_PulseOxMin,dm_mst_PulseOxMax, " _
                    & " dm_mst_BPSittingMin,dm_mst_BPSittingMax,dm_mst_BPStandingMin,dm_mst_BPStandingMax, " _
                    & " dm_mst_DisplayMessage from dm_criteria_mst WHERE dm_mst_Id = " & CriteriaID & " "

                    RaiseEvent ProcessCriteria("Start Findings")

                    'execute the query and get the results in a datareader
                    oDataReader = oDB.ReadQueryRecords(_strSQL)
                    If Not oDataReader Is Nothing Then
                        If oDataReader.HasRows = True Then
                            While oDataReader.Read
                                If Not IsDBNull(oDataReader.Item("dm_mst_AgeMin")) Then
                                    _AgeMin = oDataReader.Item("dm_mst_AgeMin") & ""
                                End If
                                If Not IsDBNull(oDataReader.Item("dm_mst_AgeMax")) Then
                                    _AgeMax = oDataReader.Item("dm_mst_AgeMax") & ""
                                End If
                                If Not IsDBNull(oDataReader.Item("dm_mst_Gender")) Then
                                    _Geneder = oDataReader.Item("dm_mst_Gender") & ""
                                End If
                                If Not IsDBNull(oDataReader.Item("dm_mst_Race")) Then
                                    _Race = oDataReader.Item("dm_mst_Race") & ""
                                End If
                                If Not IsDBNull(oDataReader.Item("dm_mst_MaritalStatus")) Then
                                    _MaritalStatus = oDataReader.Item("dm_mst_MaritalStatus") & ""
                                End If
                                If Not IsDBNull(oDataReader.Item("dm_mst_City")) Then
                                    _City = oDataReader.Item("dm_mst_City") & ""
                                End If
                                If Not IsDBNull(oDataReader.Item("dm_mst_Status")) Then
                                    _State = oDataReader.Item("dm_mst_Status") & ""
                                End If
                                If Not IsDBNull(oDataReader.Item("dm_mst_Zip")) Then
                                    _ZipCode = oDataReader.Item("dm_mst_Zip") & ""
                                End If
                                If Not IsDBNull(oDataReader.Item("dm_mst_EmplyementStatus")) Then
                                    _EmpStatus = oDataReader.Item("dm_mst_EmplyementStatus") & ""
                                End If

                                Dim arrPatHeightMin() As String
                                If Not IsDBNull(oDataReader.Item("dm_mst_HeightMin")) Then
                                    arrPatHeightMin = GetFtInch(oDataReader.Item("dm_mst_HeightMin") & "")
                                    If Not arrPatHeightMin Is Nothing Then
                                        If Not arrPatHeightMin(0) Is Nothing Then
                                            _HeightFtMin = Val(arrPatHeightMin(0))
                                            If Not arrPatHeightMin(1) Is Nothing Then _HeightInchMin = Val(arrPatHeightMin(1))
                                        End If
                                    End If
                                End If

                                Dim arrPatHeightMax() As String
                                If Not IsDBNull(oDataReader.Item("dm_mst_HeightMax")) Then
                                    arrPatHeightMax = GetFtInch(oDataReader.Item("dm_mst_HeightMax") & "")
                                    If Not arrPatHeightMax Is Nothing Then
                                        If Not arrPatHeightMax(0) Is Nothing Then
                                            _HeightFtMax = Val(arrPatHeightMax(0))
                                            If Not arrPatHeightMax(1) Is Nothing Then _HeightInchMax = Val(arrPatHeightMax(1))
                                        End If
                                    End If
                                End If

                                '// now here we directlly convert height into meter, so we use only ft varibale against this
                                _HeightFtMin = FtToMtr(_HeightFtMin, _HeightInchMin)
                                _HeightFtMax = FtToMtr(_HeightFtMax, _HeightInchMax)
                                If _HeightFtMin + _HeightFtMax > 0 Then
                                    _HaveVitalHeight = True
                                End If

                                If Not IsDBNull(oDataReader.Item("dm_mst_WeightMin")) Then
                                    _WeightMin = oDataReader.Item("dm_mst_WeightMin") & ""
                                    If _WeightMin > 0 Then _HaveVital = True
                                End If
                                If Not IsDBNull(oDataReader.Item("dm_mst_WeightMax")) Then
                                    _WeightMax = oDataReader.Item("dm_mst_WeightMax") & ""
                                    If _WeightMax > 0 Then _HaveVital = True
                                End If
                                If Not IsDBNull(oDataReader.Item("dm_mst_PulseMin")) Then
                                    _PulseMin = oDataReader.Item("dm_mst_PulseMin") & ""
                                    If _PulseMin > 0 Then _HaveVital = True
                                End If
                                If Not IsDBNull(oDataReader.Item("dm_mst_PulseMax")) Then
                                    _PulseMax = oDataReader.Item("dm_mst_PulseMax") & ""
                                    If _PulseMax > 0 Then _HaveVital = True
                                End If
                                If Not IsDBNull(oDataReader.Item("dm_mst_PulseOxMin")) Then
                                    _PulseOXMin = oDataReader.Item("dm_mst_PulseOxMin") & ""
                                    If _PulseOXMin > 0 Then _HaveVital = True
                                End If
                                If Not IsDBNull(oDataReader.Item("dm_mst_PulseOxMax")) Then
                                    _PulseOXMax = oDataReader.Item("dm_mst_PulseOxMax") & ""
                                    If _PulseOXMax > 0 Then _HaveVital = True
                                End If
                                If Not IsDBNull(oDataReader.Item("dm_mst_BPSittingMin")) Then
                                    _BPSittingMin = oDataReader.Item("dm_mst_BPSittingMin") & ""
                                    If _BPSittingMin > 0 Then _HaveVital = True
                                End If
                                If Not IsDBNull(oDataReader.Item("dm_mst_BPSittingMax")) Then
                                    _BPSittingMax = oDataReader.Item("dm_mst_BPSittingMax") & ""
                                    If _BPSittingMax > 0 Then _HaveVital = True
                                End If
                                If Not IsDBNull(oDataReader.Item("dm_mst_BPStandingMin")) Then
                                    _BPStandingMin = oDataReader.Item("dm_mst_BPStandingMin") & ""
                                    If _BPStandingMin > 0 Then _HaveVital = True
                                End If
                                If Not IsDBNull(oDataReader.Item("dm_mst_BPStandingMax")) Then
                                    _BPStandingMax = oDataReader.Item("dm_mst_BPStandingMax") & ""
                                    If _BPStandingMax > 0 Then _HaveVital = True
                                End If
                                If Not IsDBNull(oDataReader.Item("dm_mst_BMIMin")) Then
                                    _BMIMin = oDataReader.Item("dm_mst_BMIMin") & ""
                                    If _BMIMin > 0 Then _HaveVital = True
                                End If
                                If Not IsDBNull(oDataReader.Item("dm_mst_BMIMax")) Then
                                    _BMIMax = oDataReader.Item("dm_mst_BMIMax") & ""
                                    If _BMIMax > 0 Then _HaveVital = True
                                End If
                                If Not IsDBNull(oDataReader.Item("dm_mst_TemperatureMin")) Then
                                    _TempMin = oDataReader.Item("dm_mst_TemperatureMin") & ""
                                    If _TempMin > 0 Then _HaveVital = True
                                End If
                                If Not IsDBNull(oDataReader.Item("dm_mst_TemperatureMax")) Then
                                    _TempMax = oDataReader.Item("dm_mst_TemperatureMax") & ""
                                    If _TempMax > 0 Then _HaveVital = True
                                End If
                            End While
                        End If
                        oDataReader.Close()
                    End If

                    '// History
                    '_strSQL = "SELECT dm_Chdtl_HistoryItemId FROM DM_CriteriaHistory_DTL WHERE dm_Chdtl_Id = " & CriteriaID & ""
                    _strSQL = "SELECT dm_Chdtl_HistoryItem, dm_Chdtl_HistoryCategory FROM DM_CriteriaHistory_DTL WHERE dm_Chdtl_Id = " & CriteriaID & ""
                    oDataReader = oDB.ReadQueryRecords(_strSQL)
                    If Not oDataReader Is Nothing Then
                        If oDataReader.HasRows = True Then
                            Dim oList As myList
                            While oDataReader.Read
                                '_Histories.Add(oDataReader.Item("dm_Chdtl_HistoryItemId"))
                                oList = New myList
                                oList.HistoryItem = oDataReader.Item("dm_Chdtl_HistoryItem")
                                oList.HistoryCategory = oDataReader.Item("dm_Chdtl_HistoryCategory")
                                _Histories.Add(oList)
                                oList = Nothing
                            End While
                        End If
                        oDataReader.Close()
                    End If

                    '// Drugs
                    _strSQL = "SELECT dm_Drugdtl_DrugID FROM DM_CriteriaDrug_DTL WHERE dm_Drugdtl_Id = " & CriteriaID & ""
                    oDataReader = oDB.ReadQueryRecords(_strSQL)
                    If Not oDataReader Is Nothing Then
                        If oDataReader.HasRows = True Then
                            While oDataReader.Read
                                _Drugs.Add(oDataReader.Item("dm_Drugdtl_DrugID"))
                            End While
                        End If
                        oDataReader.Close()
                    End If

                    '// ICD9
                    _strSQL = "SELECT dm_ICD9CPTdtl_ICID FROM DM_ICD9CPT_DTL WHERE dm_ICD9CPTdtl_Id = " & CriteriaID & " AND dm_ICD9CPTdtl_Type = 2"
                    oDataReader = oDB.ReadQueryRecords(_strSQL)
                    If Not oDataReader Is Nothing Then
                        If oDataReader.HasRows = True Then
                            While oDataReader.Read
                                _ICD9s.Add(oDataReader.Item("dm_ICD9CPTdtl_ICID"))
                            End While
                        End If
                        oDataReader.Close()
                    End If


                    '// CPT
                    _strSQL = "SELECT dm_ICD9CPTdtl_ICID FROM DM_ICD9CPT_DTL WHERE dm_ICD9CPTdtl_Id = " & CriteriaID & " AND dm_ICD9CPTdtl_Type = 1"
                    oDataReader = oDB.ReadQueryRecords(_strSQL)
                    If Not oDataReader Is Nothing Then
                        If oDataReader.HasRows = True Then
                            While oDataReader.Read
                                _CPTs.Add(oDataReader.Item("dm_ICD9CPTdtl_ICID"))
                            End While
                        End If
                        oDataReader.Close()
                    End If

                    '// Radiology
                    _strSQL = "SELECT dm_Labsdtl_TestID FROM DM_Labs_DTL WHERE dm_Labsdtl_Id = " & CriteriaID & " "
                    oDataReader = oDB.ReadQueryRecords(_strSQL)
                    If Not oDataReader Is Nothing Then
                        If oDataReader.HasRows = True Then
                            While oDataReader.Read
                                _Labs.Add(oDataReader.Item("dm_Labsdtl_TestID"))
                            End While
                        End If
                        oDataReader.Close()
                    End If


                    '// Mahesh 20070804
                    '// Labs
                    _strSQL = "SELECT dm_labdtl_testId,dm_labdtl_resultid FROM DM_LabModule_DTL WHERE dm_labdtl_ID = " & CriteriaID & " "
                    oDataReader = oDB.ReadQueryRecords(_strSQL)
                    If Not oDataReader Is Nothing Then
                        If oDataReader.HasRows = True Then
                            While oDataReader.Read
                                _LabModule.Add(oDataReader.Item("dm_labdtl_testId") & "|" & oDataReader.Item("dm_labdtl_resultid"))
                            End While
                        End If
                        oDataReader.Close()
                    End If

                    '//

                    '*********************>>>--- READ CRITERIA CONDITION ---<<<*******************************

                    '*********************>>>--- QUERY BUILDER AS PER CONDTION ---<<<*************************
                    _strSQL = "SELECT nPatientID FROM Patient WHERE "
                    Dim _CreateSQL As Boolean = False
                    Dim _AgeMinDate As Date, _AgeMaxDate As Date
                    '>>>AGE<<<
                    If _AgeMin > 0 AndAlso _AgeMax > 0 Then
                        _AgeMinDate = DateAdd(DateInterval.Year, -_AgeMin, Date.Now) ' eg. 2012
                        _AgeMaxDate = DateAdd(DateInterval.Year, -_AgeMax, Date.Now) ' eg. 1930
                        _strSQL = _strSQL & "dtDOB BETWEEN '" & _AgeMaxDate & "' AND '" & _AgeMinDate & "' "
                        _CreateSQL = True
                    Else
                        If _AgeMin > 0 AndAlso _AgeMax = 0 Then
                            _AgeMinDate = DateAdd(DateInterval.Year, -_AgeMin, Date.Now)
                            _strSQL = _strSQL & "dtDOB >= '" & _AgeMinDate & "' "
                            _CreateSQL = True
                        ElseIf _AgeMax > 0 AndAlso _AgeMin = 0 Then
                            _AgeMaxDate = DateAdd(DateInterval.Year, -_AgeMax, Date.Now)
                            _strSQL = _strSQL & "dtDOB <= '" & _AgeMaxDate & "' "
                            _CreateSQL = True
                        End If
                    End If
                    '----------------------------------------
                    If _CreateSQL = False Then
                        FindGuidelinesForMultiplePatient = Nothing
                        Exit Function
                    End If

                    '----------------------------------------
                    '>>>GENEDER<<<
                    If _Geneder.Trim <> "" Then
                        If Not _Geneder.Trim = "All" Then
                            _strSQL = _strSQL & " AND sGender = '" & _Geneder.Trim.Replace("'", "''") & "'"
                        End If
                    End If
                    '>>>RACE<<<
                    If _Race.Trim <> "" Then
                        _strSQL = _strSQL & " AND sRace = '" & _Race.Trim.Replace("'", "''") & "'"
                    End If
                    '>>>MARITAL STATUS<<<
                    If _MaritalStatus.Trim <> "" Then
                        _strSQL = _strSQL & " AND sMaritalStatus = '" & _MaritalStatus.Trim.Replace("'", "''") & "'"
                    End If
                    '>>>CITY<<<
                    If _City.Trim <> "" Then
                        _strSQL = _strSQL & " AND sCity = '" & _City.Trim.Replace("'", "''") & "'"
                    End If
                    '>>>STATE<<<
                    If _State.Trim <> "" Then
                        _strSQL = _strSQL & " AND sState = '" & _State.Trim.Replace("'", "''") & "'"
                    End If
                    '>>>ZIPCODE<<<
                    If _ZipCode.Trim <> "" Then
                        _strSQL = _strSQL & " AND sZIP = '" & _ZipCode.Trim.Replace("'", "''") & "'"
                    End If
                    '>>>ZIPCODE<<<
                    If _ZipCode.Trim <> "" Then
                        _strSQL = _strSQL & " AND sZIP = '" & _ZipCode.Trim.Replace("'", "''") & "'"
                    End If
                    '>>>EMPSTATUS<<<
                    If _EmpStatus.Trim <> "" Then
                        _strSQL = _strSQL & " AND sEmploymentStatus = '" & _EmpStatus.Trim.Replace("'", "''") & "'"
                    End If

                    If Not _strSQL.Trim = "" Then
                        oDataReader = oDB.ReadQueryRecords(_strSQL)
                        If Not oDataReader Is Nothing Then
                            If oDataReader.HasRows = True Then
                                While oDataReader.Read
                                    If Not IsDBNull(oDataReader.Item("nPatientID")) Then
                                        _PrimaryPatientID.Add(oDataReader.Item("nPatientID"))
                                    End If
                                End While
                            End If
                            oDataReader.Close()
                        End If
                    End If
                    For i As Int16 = 1 To _PrimaryPatientID.Count
                        If i > 1 Then
                            _PrimaryINPatientID = _PrimaryINPatientID & "," & _PrimaryPatientID(i)
                        Else
                            _PrimaryINPatientID = _PrimaryPatientID(i)
                        End If
                    Next

                    ':::VITALS:::

                    'Dim _PrimaryPatientID As New Collection
                    'Dim _TempPatientID As New Collection
                    'Dim _PrimaryINPatientID As String = ""

                    Dim _RunVitals As Boolean = False
                    If _PrimaryPatientID.Count > 0 Then
                        _strSQL = ""
                        _strSQL = "Select distinct nPatientID from vitals v where dtvitaldate in " _
                        & " (select max(dtvitaldate) from vitals v1 where npatientid=v.npatientid group by nPatientID) " _
                        & " AND nPatientid in (" & _PrimaryINPatientID & ")"


                        '>>>Weight Min & Max <<<
                        If _WeightMin > 0 AndAlso _WeightMax > 0 Then
                            _strSQL = _strSQL & " AND dWeightinlbs BETWEEN " & _WeightMin & " AND " & _WeightMax & ""
                            _RunVitals = True
                        Else
                            If _WeightMin > 0 AndAlso _WeightMax = 0 Then
                                _strSQL = _strSQL & " AND dWeightinlbs >= " & _WeightMin & ""
                                _RunVitals = True
                            ElseIf _WeightMax > 0 AndAlso _WeightMin = 0 Then
                                _strSQL = _strSQL & " AND dWeightinlbs <= " & _WeightMax & ""
                                _RunVitals = True
                            End If
                        End If
                        '>>>Pulse Min & Max <<<
                        If _PulseMin > 0 AndAlso _PulseMax > 0 Then
                            _strSQL = _strSQL & " AND dPulsePerMinute BETWEEN " & _PulseMin & " AND " & _PulseMax & ""
                            _RunVitals = True
                        Else
                            If _PulseMin > 0 AndAlso _PulseMax = 0 Then
                                _strSQL = _strSQL & " AND dPulsePerMinute >= " & _PulseMin & ""
                                _RunVitals = True
                            ElseIf _PulseMax > 0 AndAlso _PulseMin = 0 Then
                                _strSQL = _strSQL & " AND dPulsePerMinute <= " & _PulseMax & ""
                                _RunVitals = True
                            End If
                        End If
                        '>>>Pulse OX Min & Max <<<
                        If _PulseOXMin > 0 AndAlso _PulseOXMax > 0 Then
                            _strSQL = _strSQL & " AND dPulseOx BETWEEN " & _PulseOXMin & " AND " & _PulseOXMax & ""
                            _RunVitals = True
                        Else
                            If _PulseOXMin > 0 AndAlso _PulseOXMax = 0 Then
                                _strSQL = _strSQL & " AND dPulseOx >= " & _PulseOXMin & ""
                                _RunVitals = True
                            ElseIf _PulseMax > 0 AndAlso _PulseMin = 0 Then
                                _strSQL = _strSQL & " AND dPulseOx <= " & _PulseOXMax & ""
                                _RunVitals = True
                            End If
                        End If
                        '>>>BP Sitting Min & Max <<<
                        If _BPSittingMin > 0 AndAlso _BPSittingMax > 0 Then
                            _strSQL = _strSQL & " AND dBloodPressureSittingMin >= " & _BPSittingMin & " AND dBloodPressureSittingMax <= " & _BPSittingMax & ""
                            _RunVitals = True
                        Else
                            If _BPSittingMin > 0 AndAlso _BPSittingMax = 0 Then
                                _strSQL = _strSQL & " AND dBloodPressureSittingMin >= " & _BPSittingMin & ""
                                _RunVitals = True
                            ElseIf _BPSittingMax > 0 AndAlso _BPSittingMin = 0 Then
                                _strSQL = _strSQL & " AND dBloodPressureSittingMax <= " & _BPSittingMax & ""
                                _RunVitals = True
                            End If
                        End If
                        '>>>BP Sitting Min & Max <<<
                        If _BPStandingMin > 0 AndAlso _BPStandingMax > 0 Then
                            _strSQL = _strSQL & " AND dBloodPressureStandingMin >= " & _BPStandingMin & " AND dBloodPressureStandingMax <= " & _BPStandingMax & ""
                            _RunVitals = True
                        Else
                            If _BPStandingMin > 0 AndAlso _BPStandingMax = 0 Then
                                _strSQL = _strSQL & " AND dBloodPressureStandingMin >= " & _BPStandingMin & ""
                                _RunVitals = True
                            ElseIf _BPStandingMax > 0 AndAlso _BPStandingMin = 0 Then
                                _strSQL = _strSQL & " AND dBloodPressureStandingMax <= " & _BPStandingMax & ""
                                _RunVitals = True
                            End If
                        End If
                        '>>>BP Sitting Min & Max <<<
                        If _TempMin > 0 AndAlso _TempMax > 0 Then
                            _strSQL = _strSQL & " AND dTemperature >= " & _TempMin & " AND dTemperature <= " & _TempMax & ""
                            _RunVitals = True
                        Else
                            If _TempMin > 0 AndAlso _TempMax = 0 Then
                                _strSQL = _strSQL & " AND dTemperature >= " & _TempMin & ""
                                _RunVitals = True
                            ElseIf _TempMax > 0 AndAlso _TempMin = 0 Then
                                _strSQL = _strSQL & " AND dTemperature <= " & _TempMax & ""
                                _RunVitals = True
                            End If
                        End If

                        '_HeightFtMin = 0 - Vital
                        '_HeightFtMax = 0 - Vital
                        '_HeightInchMin = 0 - Vital
                        '_HeightInchMax = 0 - Vital

                        If _RunVitals = True Then
                            _TempPatientID = New Collection
                            If Not _strSQL.Trim = "" Then
                                oDataReader = oDB.ReadQueryRecords(_strSQL)
                                If Not oDataReader Is Nothing Then
                                    If oDataReader.HasRows = True Then
                                        While oDataReader.Read
                                            If Not IsDBNull(oDataReader.Item("nPatientID")) Then
                                                _TempPatientID.Add(oDataReader.Item("nPatientID"))
                                            End If
                                        End While
                                    End If
                                    oDataReader.Close()
                                End If
                            End If
                        End If

                        If _HaveVital = True Then
                            If _TempPatientID.Count > 1 Then
                                _PrimaryPatientID = New Collection
                                For i As Int16 = 1 To _TempPatientID.Count
                                    _PrimaryPatientID.Add(_TempPatientID(i))
                                Next
                                _PrimaryINPatientID = ""
                                For i As Int16 = 1 To _PrimaryPatientID.Count
                                    If i > 1 Then
                                        _PrimaryINPatientID = _PrimaryINPatientID & "," & _PrimaryPatientID(i)
                                    Else
                                        _PrimaryINPatientID = _PrimaryPatientID(i)
                                    End If
                                Next
                                _TempPatientID = Nothing
                            Else

                                For i As Int16 = _PrimaryPatientID.Count To 1 Step -1
                                    _PrimaryPatientID.Remove(i)
                                Next
                                GoTo FinishFindingProcess
                            End If
                        End If

                        '//<<<<<<< History >>>>>>>
                        If _HaveVitalHeight = True Then
                            _TempPatientID = New Collection

                            _strSQL = "Select distinct nPatientID,sHeight from vitals v where dtvitaldate in " _
                            & " (select max(dtvitaldate) from vitals v1 where npatientid=v.npatientid group by nPatientID) " _
                            & " AND nPatientid in (" & _PrimaryINPatientID & ")"
                            oDataReader = oDB.ReadQueryRecords(_strSQL)
                            If Not oDataReader Is Nothing Then
                                If oDataReader.HasRows = True Then
                                    While oDataReader.Read
                                        If Not IsDBNull(oDataReader.Item("nPatientID")) Then
                                            Dim _strPatHt() As String
                                            Dim _PatHtFt As Double = 0
                                            Dim _PatHtInch As Double = 0

                                            If Not IsDBNull(oDataReader.Item("sHeight")) Then
                                                _strPatHt = GetFtInch(oDataReader.Item("sHeight") & "")
                                                If Not _strPatHt Is Nothing Then
                                                    If Not _strPatHt(0) Is Nothing Then _PatHtFt = Val(_strPatHt(0))
                                                    If Not _strPatHt(1) Is Nothing Then _PatHtInch = Val(_strPatHt(1))
                                                End If
                                            End If

                                            _PatHtFt = FtToMtr(_PatHtFt, _PatHtInch)

                                            If _HeightFtMin > 0 Or _HeightFtMax > 0 Then
                                                If _HeightFtMin > 0 AndAlso _HeightFtMax > 0 Then
                                                    If _PatHtFt >= _HeightFtMin AndAlso _PatHtFt <= _HeightFtMax Then
                                                        _TempPatientID.Add(oDataReader.Item("nPatientID"))
                                                    End If
                                                ElseIf _HeightFtMin > 0 AndAlso _HeightFtMax = 0 Then
                                                    If _PatHtFt >= _HeightFtMin Then
                                                        _TempPatientID.Add(oDataReader.Item("nPatientID"))
                                                    End If
                                                ElseIf _HeightFtMax > 0 AndAlso _HeightFtMin = 0 Then
                                                    If _PatHtFt <= _HeightFtMax Then
                                                        _TempPatientID.Add(oDataReader.Item("nPatientID"))
                                                    End If
                                                End If
                                            End If


                                        End If
                                    End While
                                End If
                                oDataReader.Close()
                            End If

                            '// Work with Height match record
                            If _TempPatientID.Count > 0 Then
                                _PrimaryPatientID = New Collection
                                For i As Int16 = 1 To _TempPatientID.Count
                                    _PrimaryPatientID.Add(_TempPatientID(i))
                                Next
                                _PrimaryINPatientID = ""
                                For i As Int16 = 1 To _PrimaryPatientID.Count
                                    If i > 1 Then
                                        _PrimaryINPatientID = _PrimaryINPatientID & "," & _PrimaryPatientID(i)
                                    Else
                                        _PrimaryINPatientID = _PrimaryPatientID(i)
                                    End If
                                Next
                            Else

                                For i As Int16 = _PrimaryPatientID.Count To 1 Step -1
                                    _PrimaryPatientID.Remove(i)
                                Next
                                GoTo FinishFindingProcess
                            End If

                            _TempPatientID = Nothing
                        End If
                        '//<<<<<<< History >>>>>>>

                    End If
                    ':::VITALS:::

                    '*********************>>>--- QUERY BUILDER AS PER CONDTION ---<<<*************************

                    '*********************>>>--- HISTORY, DRUGS, CPT, ICD9 Start ---<<<*****************************
                    If _PrimaryINPatientID.Trim <> "" Then
                        '::: HISTORY :::
                        _strSQL = ""
                        If _Histories.Count > 0 Then
                            ''Commented by sudhir 20090302 ''
                            '_strSQL = "SELECT DISTINCT History.nPatientID " _
                            '& " FROM DM_CriteriaHistory_DTL INNER JOIN History_MST ON DM_CriteriaHistory_DTL.dm_Chdtl_HistoryItemId = History_MST.nHistoryID INNER JOIN " _
                            '& " History ON History_MST.sDescription = History.sHistoryItem " _
                            '& " WHERE (DM_CriteriaHistory_DTL.dm_Chdtl_Id = " & CriteriaID & ") AND (History.nPatientID IN (" & _PrimaryINPatientID & "))"

                            _strSQL = "SELECT DISTINCT History.nPatientID FROM History INNER JOIN DM_CriteriaHistory_DTL ON History.sHistoryItem " _
                                    & " = DM_CriteriaHistory_DTL.dm_Chdtl_HistoryItem " _
                                    & " WHERE (DM_CriteriaHistory_DTL.dm_Chdtl_Id = " & CriteriaID & ") AND (History.nPatientID IN " & _PrimaryINPatientID & ")"
                            _TempPatientID = New Collection
                            If Not _strSQL.Trim = "" Then
                                oDataReader = oDB.ReadQueryRecords(_strSQL)
                                If Not oDataReader Is Nothing Then
                                    If oDataReader.HasRows = True Then
                                        While oDataReader.Read
                                            If Not IsDBNull(oDataReader.Item("nPatientID")) Then
                                                _TempPatientID.Add(oDataReader.Item("nPatientID"))
                                            End If
                                        End While
                                    End If
                                    oDataReader.Close()
                                End If
                            End If
                            If _TempPatientID.Count > 0 Then
                                _PrimaryPatientID = New Collection
                                For i As Int16 = 1 To _TempPatientID.Count
                                    _PrimaryPatientID.Add(_TempPatientID(i))
                                Next

                                _PrimaryINPatientID = ""
                                For i As Int16 = 1 To _PrimaryPatientID.Count
                                    If i > 1 Then
                                        _PrimaryINPatientID = _PrimaryINPatientID & "," & _PrimaryPatientID(i)
                                    Else
                                        _PrimaryINPatientID = _PrimaryPatientID(i)
                                    End If
                                Next
                                _TempPatientID = Nothing
                            Else
                                For i As Int16 = _PrimaryPatientID.Count To 1 Step -1
                                    _PrimaryPatientID.Remove(i)
                                Next
                                GoTo FinishFindingProcess
                            End If
                        End If

                        '::: DRUGS :::
                        _strSQL = ""
                        If _Drugs.Count > 0 Then
                            _strSQL = "SELECT DISTINCT Medication.nPatientID " _
                            & " FROM DM_CriteriaDrug_DTL INNER JOIN Drugs_MST ON DM_CriteriaDrug_DTL.dm_Drugdtl_DrugID = Drugs_MST.nDrugsID INNER JOIN " _
                            & " Medication ON Drugs_MST.sDrugName = Medication.sMedication AND Drugs_MST.sDosage = Medication.sDosage " _
                            & " WHERE (DM_CriteriaDrug_DTL.dm_Drugdtl_Id = " & CriteriaID & ") AND (Medication.nPatientID IN (" & _PrimaryINPatientID & "))"

                            _TempPatientID = New Collection
                            If Not _strSQL.Trim = "" Then
                                oDataReader = oDB.ReadQueryRecords(_strSQL)
                                If Not oDataReader Is Nothing Then
                                    If oDataReader.HasRows = True Then
                                        While oDataReader.Read
                                            If Not IsDBNull(oDataReader.Item("nPatientID")) Then
                                                _TempPatientID.Add(oDataReader.Item("nPatientID"))
                                            End If
                                        End While
                                    End If
                                    oDataReader.Close()
                                End If
                            End If
                            If _TempPatientID.Count > 0 Then
                                _PrimaryPatientID = New Collection
                                For i As Int16 = 1 To _TempPatientID.Count
                                    _PrimaryPatientID.Add(_TempPatientID(i))
                                Next

                                _PrimaryINPatientID = ""
                                For i As Int16 = 1 To _PrimaryPatientID.Count
                                    If i > 1 Then
                                        _PrimaryINPatientID = _PrimaryINPatientID & "," & _PrimaryPatientID(i)
                                    Else
                                        _PrimaryINPatientID = _PrimaryPatientID(i)
                                    End If
                                Next
                                _TempPatientID = Nothing
                            Else
                                For i As Int16 = _PrimaryPatientID.Count To 1 Step -1
                                    _PrimaryPatientID.Remove(i)
                                Next
                                GoTo FinishFindingProcess
                            End If
                        End If
                        '::: ICD9 :::
                        _strSQL = ""
                        If _ICD9s.Count > 0 Then
                            _strSQL = "SELECT DISTINCT ExamICD9CPT.nPatientID " _
                            & " FROM DM_ICD9CPT_DTL INNER JOIN ICD9 ON DM_ICD9CPT_DTL.dm_ICD9CPTdtl_ICID = ICD9.nICD9ID INNER JOIN " _
                            & " ExamICD9CPT ON ICD9.sICD9Code = ExamICD9CPT.sICD9Code AND ICD9.sDescription = ExamICD9CPT.sICD9Description " _
                            & " WHERE (DM_ICD9CPT_DTL.dm_ICD9CPTdtl_Type = 2) AND (DM_ICD9CPT_DTL.dm_ICD9CPTdtl_Id = " & CriteriaID & ") " _
                            & " AND (ExamICD9CPT.nPatientID IN (" & _PrimaryINPatientID & "))"

                            _TempPatientID = New Collection
                            If Not _strSQL.Trim = "" Then
                                oDataReader = oDB.ReadQueryRecords(_strSQL)
                                If Not oDataReader Is Nothing Then
                                    If oDataReader.HasRows = True Then
                                        While oDataReader.Read
                                            If Not IsDBNull(oDataReader.Item("nPatientID")) Then
                                                _TempPatientID.Add(oDataReader.Item("nPatientID"))
                                            End If
                                        End While
                                    End If
                                    oDataReader.Close()
                                End If
                            End If
                            If _TempPatientID.Count > 0 Then
                                _PrimaryPatientID = New Collection
                                For i As Int16 = 1 To _TempPatientID.Count
                                    _PrimaryPatientID.Add(_TempPatientID(i))
                                Next

                                _PrimaryINPatientID = ""
                                For i As Int16 = 1 To _PrimaryPatientID.Count
                                    If i > 1 Then
                                        _PrimaryINPatientID = _PrimaryINPatientID & "," & _PrimaryPatientID(i)
                                    Else
                                        _PrimaryINPatientID = _PrimaryPatientID(i)
                                    End If
                                Next
                                _TempPatientID = Nothing
                            Else
                                For i As Int16 = _PrimaryPatientID.Count To 1 Step -1
                                    _PrimaryPatientID.Remove(i)
                                Next
                                GoTo FinishFindingProcess
                            End If
                        End If
                        '::: CPT :::
                        _strSQL = ""
                        If _CPTs.Count > 0 Then
                            _strSQL = "SELECT DISTINCT ExamICD9CPT.nPatientID " _
                            & " FROM DM_ICD9CPT_DTL INNER JOIN CPT_MST ON DM_ICD9CPT_DTL.dm_ICD9CPTdtl_ICID = CPT_MST.nCPTID INNER JOIN " _
                            & " ExamICD9CPT ON CPT_MST.sCPTCode = ExamICD9CPT.sCPTCode AND CPT_MST.sDescription = ExamICD9CPT.sCPTDescription " _
                            & " WHERE (DM_ICD9CPT_DTL.dm_ICD9CPTdtl_Type = 1) AND (DM_ICD9CPT_DTL.dm_ICD9CPTdtl_Id = " & CriteriaID & ") AND (ExamICD9CPT.nPatientID IN " _
                            & " (" & _PrimaryINPatientID & "))"

                            _TempPatientID = New Collection
                            If Not _strSQL.Trim = "" Then
                                oDataReader = oDB.ReadQueryRecords(_strSQL)
                                If Not oDataReader Is Nothing Then
                                    If oDataReader.HasRows = True Then
                                        While oDataReader.Read
                                            If Not IsDBNull(oDataReader.Item("nPatientID")) Then
                                                _TempPatientID.Add(oDataReader.Item("nPatientID"))
                                            End If
                                        End While
                                    End If
                                    oDataReader.Close()
                                End If
                            End If
                            If _TempPatientID.Count > 0 Then
                                _PrimaryPatientID = New Collection
                                For i As Int16 = 1 To _TempPatientID.Count
                                    _PrimaryPatientID.Add(_TempPatientID(i))
                                Next

                                _PrimaryINPatientID = ""
                                For i As Int16 = 1 To _PrimaryPatientID.Count
                                    If i > 1 Then
                                        _PrimaryINPatientID = _PrimaryINPatientID & "," & _PrimaryPatientID(i)
                                    Else
                                        _PrimaryINPatientID = _PrimaryPatientID(i)
                                    End If
                                Next
                                _TempPatientID = Nothing
                            Else
                                For i As Int16 = _PrimaryPatientID.Count To 1 Step -1
                                    _PrimaryPatientID.Remove(i)
                                Next
                                GoTo FinishFindingProcess
                            End If
                        End If

                        '::: RADIOLOGY :::
                        _strSQL = ""
                        If _Labs.Count > 0 Then
                            _strSQL = "SELECT DISTINCT LM_Orders.lm_Patient_ID " _
                            & " FROM DM_Labs_DTL INNER JOIN LM_Orders ON DM_Labs_DTL.dm_Labsdtl_TestID = LM_Orders.lm_test_ID " _
                            & " WHERE (DM_Labs_DTL.dm_Labsdtl_Id = " & CriteriaID & ") AND (LM_Orders.lm_NumericResult BETWEEN " _
                            & " DM_Labs_DTL.dm_Labsdtl_NumericResultMin AND DM_Labs_DTL.dm_Labsdtl_NumericResultMax) AND (LM_Orders.lm_Patient_ID IN " _
                            & " (" & _PrimaryINPatientID & "))"

                            _TempPatientID = New Collection
                            If Not _strSQL.Trim = "" Then
                                oDataReader = oDB.ReadQueryRecords(_strSQL)
                                If Not oDataReader Is Nothing Then
                                    If oDataReader.HasRows = True Then
                                        While oDataReader.Read
                                            If Not IsDBNull(oDataReader.Item("lm_Patient_ID")) Then
                                                _TempPatientID.Add(oDataReader.Item("lm_Patient_ID"))
                                            End If
                                        End While
                                    End If
                                    oDataReader.Close()
                                End If
                            End If
                            If _TempPatientID.Count > 0 Then
                                _PrimaryPatientID = New Collection
                                For i As Int16 = 1 To _TempPatientID.Count
                                    _PrimaryPatientID.Add(_TempPatientID(i))
                                Next

                                _PrimaryINPatientID = ""
                                For i As Int16 = 1 To _PrimaryPatientID.Count
                                    If i > 1 Then
                                        _PrimaryINPatientID = _PrimaryINPatientID & "," & _PrimaryPatientID(i)
                                    Else
                                        _PrimaryINPatientID = _PrimaryPatientID(i)
                                    End If
                                Next
                                _TempPatientID = Nothing
                            Else
                                For i As Int16 = _PrimaryPatientID.Count To 1 Step -1
                                    _PrimaryPatientID.Remove(i)
                                Next
                                GoTo FinishFindingProcess
                            End If
                        End If

                        ' Mahesh 20070804
                        '::: LABS :::
                        _strSQL = ""
                        If _LabModule.Count > 0 Then
                            _strSQL = "SELECT Lab_Order_MST.labom_PatientID, DM_LabModule_DTL.dm_labdtl_TestID AS TestID, DM_LabModule_DTL.dm_labdtl_ResultID AS ResultID, " _
                            & " DM_LabModule_DTL.dm_labdtl_Operator AS CondOperator, DM_LabModule_DTL.dm_labdtl_ResultValue1 AS CondValue1, " _
                            & " DM_LabModule_DTL.dm_labdtl_ResultValue2 AS CondValue2, Lab_Order_Test_ResultDtl.labotrd_ResultValue AS PatResult " _
                            & " FROM Lab_Order_Test_ResultDtl INNER JOIN Lab_Order_MST ON Lab_Order_Test_ResultDtl.labotrd_OrderID = Lab_Order_MST.labom_OrderID INNER JOIN DM_LabModule_DTL ON Lab_Order_Test_ResultDtl.labotrd_TestID = DM_LabModule_DTL.dm_labdtl_TestID AND Lab_Order_Test_ResultDtl.labotrd_ResultNameID = DM_LabModule_DTL.dm_labdtl_ResultID " _
                            & " WHERE (DM_LabModule_DTL.dm_labdtl_ID = " & CriteriaID & ") AND (Lab_Order_MST.labom_PatientID IN (" & _PrimaryINPatientID & ")) " _
                            & " AND DM_LabModule_DTL.dm_labdtl_Operator IS NOT NULL AND Lab_Order_Test_ResultDtl.labotrd_ResultValue IS NOT NULL AND Lab_Order_MST.labom_PatientID IS NOT NULL"

                            'Check Value is greater than or less than or equal to
                            If Not _strSQL.Trim = "" Then
                                Dim _SortPatients As New Collection

                                oDataReader = oDB.ReadQueryRecords(_strSQL)
                                If Not oDataReader Is Nothing Then
                                    If oDataReader.HasRows = True Then
                                        While oDataReader.Read
                                            If Not IsDBNull(oDataReader.Item("CondOperator")) Then
                                                Select Case oDataReader.Item("CondOperator")
                                                    Case "Greater Than"
                                                        If Not IsDBNull(oDataReader.Item("CondValue1")) Then
                                                            If CDbl(oDataReader.Item("PatResult") & "") > CDbl(oDataReader.Item("CondValue1") & "") Then
                                                                _SortPatients.Add(oDataReader.Item("labom_PatientID"))
                                                            End If
                                                        End If
                                                    Case "Less Than"
                                                        If Not IsDBNull(oDataReader.Item("CondValue1")) Then
                                                            If CDbl(oDataReader.Item("PatResult") & "") < CDbl(oDataReader.Item("CondValue1") & "") Then
                                                                _SortPatients.Add(oDataReader.Item("labom_PatientID"))
                                                            End If
                                                        End If
                                                    Case "Between"
                                                        If Not IsDBNull(oDataReader.Item("CondValue1")) Then
                                                            If Not IsDBNull(oDataReader.Item("CondValue2")) Then
                                                                If CDbl(oDataReader.Item("PatResult") & "") >= CDbl(oDataReader.Item("CondValue1") & "") AndAlso CDbl(oDataReader.Item("PatResult") & "") <= CDbl(oDataReader.Item("CondValue2") & "") Then
                                                                    _SortPatients.Add(oDataReader.Item("labom_PatientID"))
                                                                End If
                                                            End If
                                                        End If
                                                End Select
                                            End If
                                        End While
                                    End If
                                    oDataReader.Close()
                                End If

                                _TempPatientID = New Collection

                                If _SortPatients.Count > 0 Then
                                    Dim _SortedPatients As New Collection
                                    Dim _AddPatient As Boolean = False

                                    For i As Int16 = 1 To _SortPatients.Count
                                        _AddPatient = False

                                        If _SortedPatients.Count > 0 Then
                                            For j As Int16 = 1 To _SortedPatients.Count
                                                If _SortedPatients.Item(j) = _SortPatients.Item(i) Then
                                                    _AddPatient = False
                                                    Exit For
                                                End If
                                            Next
                                        Else
                                            _AddPatient = True
                                        End If

                                        If _AddPatient = True Then
                                            _SortedPatients.Add(_SortPatients.Item(i))
                                        End If
                                    Next

                                    _TempPatientID = New Collection
                                    If _SortedPatients.Count > 0 Then
                                        For i As Int16 = 1 To _SortedPatients.Count
                                            _TempPatientID.Add(_SortedPatients.Item(i))
                                        Next
                                    End If
                                End If


                                If _TempPatientID.Count > 0 Then
                                    _PrimaryPatientID = New Collection
                                    For i As Int16 = 1 To _TempPatientID.Count
                                        _PrimaryPatientID.Add(_TempPatientID(i))
                                    Next

                                    _PrimaryINPatientID = ""
                                    For i As Int16 = 1 To _PrimaryPatientID.Count
                                        If i > 1 Then
                                            _PrimaryINPatientID = _PrimaryINPatientID & "," & _PrimaryPatientID(i)
                                        Else
                                            _PrimaryINPatientID = _PrimaryPatientID(i)
                                        End If
                                    Next
                                    _TempPatientID = Nothing
                                Else
                                    For i As Int16 = _PrimaryPatientID.Count To 1 Step -1
                                        _PrimaryPatientID.Remove(i)
                                    Next
                                    GoTo FinishFindingProcess
                                End If


                            End If


                        End If

                    End If


                    '*********************>>>--- HISTORY, DRUGS, CPT, ICD9 Finish ---<<<*****************************
FinishFindingProcess:
                    If _PrimaryPatientID.Count > 0 Then
                        RaiseEvent FinishCriteria(True, _PrimaryPatientID)
                    Else
                        RaiseEvent FinishCriteria(False, _PrimaryPatientID)
                    End If

                Catch ex As SqlException
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    UpdateLog("clsDiseaseManagement -- FindGuidelinesForMultiplePatient -- " & ex.ToString)
                Catch ex As Exception
                    UpdateLog("clsDiseaseManagement -- FindGuidelinesForMultiplePatient -- " & ex.ToString)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    'oDataReader.Close()
                    oDB.Disconnect()
                    oDB.Dispose()
                    oDB = Nothing
                    If oDataReader IsNot Nothing Then
                        oDataReader.Dispose()
                        oDataReader = Nothing
                    End If
                End Try
                Return Nothing
            End Function

            ''Added by Amit on 21-07-2011

            Public Function FindDMCriteriaOFPatient(ByVal PatientID As Long, ByVal gnClinicID As Long) As Collection
                Dim DMPatientID As New Collection

                Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
                Dim oDBps As New gloDatabaseLayer.DBParameters()
                Dim dtTask As DataTable = Nothing
                Try
                    oDB.Connect(False)

                    oDBps.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt)
                    oDB.Retrive("GETOldDMAlert", oDBps, dtTask)

                    oDB.Disconnect()


                    If (IsNothing(dtTask) = False) Then
                        For _row As Int32 = 0 To dtTask.Rows.Count - 1
                            DMPatientID.Add(dtTask.Rows(_row)("dm_mst_Id"))
                        Next

                    End If



                    Return DMPatientID

                Catch dbErr As gloDatabaseLayer.DBException
                    dbErr.ERROR_Log(dbErr.ToString())
                    Return Nothing
                Catch ex As Exception

                    MessageBox.Show("ERROR : " + ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
                    Return Nothing
                Finally
                    If Not IsNothing(oDBps) Then  'Obj Disposed by mitesh
                        oDBps.Dispose()
                        oDBps = Nothing
                    End If
                    If Not IsNothing(dtTask) Then  'Obj Disposed by mitesh
                        dtTask.Dispose()
                        dtTask = Nothing
                    End If
                    If Not IsNothing(oDB) Then
                        oDB.Dispose()
                        oDB = Nothing
                    End If
                    DMPatientID = Nothing
                End Try

            End Function

            Public Function FindDMCriteriaOFPatientPre7030(ByVal PatientID As Long, ByVal gnClinicID As Long) As Collection
                Dim DMPatientID As New Collection

                Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
                Dim oDBps As New gloDatabaseLayer.DBParameters()
                Dim dtTask As DataTable = Nothing
                Try
                    oDB.Connect(False)

                    oDBps.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt)
                    oDBps.Add("@nClinicID ", gnClinicID, ParameterDirection.Input, SqlDbType.BigInt)
                    oDB.Retrive("GetDMCriteriaOFPatientPre7030", oDBps, dtTask) 'Aniket 08-Mar-13: Call the pre 7030 DM SP for older locic


                    oDB.Disconnect()


                    If (IsNothing(dtTask) = False) Then
                        For _row As Int32 = 0 To dtTask.Rows.Count - 1
                            DMPatientID.Add(dtTask.Rows(_row)("dm_mst_Id"))
                        Next
                    End If




                    Return DMPatientID

                Catch dbErr As gloDatabaseLayer.DBException
                    dbErr.ERROR_Log(dbErr.ToString())
                    Return Nothing
                Catch ex As Exception

                    MessageBox.Show("ERROR : " + ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
                    Return Nothing
                Finally
                    If Not IsNothing(oDBps) Then  'Obj Disposed by mitesh
                        oDBps.Dispose()
                        oDBps = Nothing
                    End If
                    If Not IsNothing(dtTask) Then  'Obj Disposed by mitesh
                        dtTask.Dispose()
                        dtTask = Nothing
                    End If
                    If Not IsNothing(oDB) Then
                        oDB.Dispose()
                        oDB = Nothing
                    End If
                    DMPatientID = Nothing
                End Try

            End Function
            ''''' Added on 20070208
            Public Function FindGuidelinesForSinglePatient(ByVal PatientID As Long) As Collection
                'A & D
                Dim _strSQL As String = ""
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim oDataReader As SqlClient.SqlDataReader

                Dim _Age As Decimal = 0
                Dim _Geneder As String = ""
                Dim _Race As String = ""
                Dim _MaritalStatus As String = ""
                Dim _City As String = ""
                Dim _State As String = ""
                Dim _ZipCode As String = ""
                Dim _EmpStatus As String = ""
                Dim _Height As Double = 0
                Dim _Weight As Double = 0
                Dim _Pulse As Double = 0
                Dim _PulseOX As Double = 0
                Dim _BPSittingMin As Double = 0
                Dim _BPSittingMax As Double = 0
                Dim _BPStandingMin As Double = 0
                Dim _BPStandingMax As Double = 0
                Dim _BMI As Double = 0
                Dim _Temperature As Double = 0

                'Dim _Histories As New Collection
                'Dim _Drugs As New Collection
                'Dim _ICD9s As New Collection
                'Dim _CPTs As New Collection
                'Dim _Labs As New Collection
                'Dim _LabModule As New gloStream.DiseaseManagement.Supporting.LabModulePatientDetails


                Dim _FinalPatientID As New Collection
                Dim _PrimaryPatientID As New Collection
                Dim _TempPatientID As New Collection
                Dim _PrimaryINPatientID As String = ""

                Try
                    UpdateLog(" START DM - FindGuidelinesForSinglePatient")
                    ''Application.DoEvents()
                    RaiseEvent StartCriteria(True)

                    '*********************>>>--- READ PATIENT CRITERIA CONDITION ---<<<***********************
                    oDB.Connect(GetConnectionString)
                    '::: GENERAL INFORMATION :::
                    _strSQL = "select dtDOB,sGender,dbo.fn_GetRaceEthnicity(" & PatientID & ",'race','|') as sRace ,sMaritalStatus,sCity,sState,sZip,sEmploymentStatus from patient where npatientid = " & PatientID & " and dtDOB is not null "
                    oDataReader = oDB.ReadQueryRecords(_strSQL)
                    If Not oDataReader Is Nothing Then
                        If oDataReader.HasRows = True Then
                            While oDataReader.Read
                                'gstrPatientDOB = Format(dgPatient.Item(dgPatient.CurrentRowIndex, 6), "MM/dd/yyyy")
                                'Dim nMonths As Int16
                                'nMonths = DateDiff(DateInterval.Month, CType(gstrPatientDOB, Date), Date.Now.Date)
                                'gstrPatientAge = nMonths \ 12 & " Yrs " & nMonths Mod 12 & " Months" ' DateDiff(DateInterval.Month, CType(gstrPatientDOB, Date), Date.Now.Date) & " Yrs"

                                'Dim nMonths As Int16
                                'nMonths = DateDiff(DateInterval.Month, CType(oDataReader.Item("dtDOB") & "", Date), Date.Now.Date)
                                '_Age = Val(nMonths \ 12) & "." & System.Math.Abs(Val(nMonths Mod 12))

                                'Resolve Bug ID : 43905
                                _Age = DateDiff(DateInterval.Day, CType(oDataReader.Item("dtDOB") & "", Date), Date.Now.Date)


                                If Not IsDBNull(oDataReader.Item("sGender")) Then
                                    _Geneder = oDataReader.Item("sGender") & ""
                                End If
                                If Not IsDBNull(oDataReader.Item("sRace")) Then
                                    _Race = oDataReader.Item("sRace") & ""
                                End If
                                If Not IsDBNull(oDataReader.Item("sMaritalStatus")) Then
                                    _MaritalStatus = oDataReader.Item("sMaritalStatus") & ""
                                End If
                                If Not IsDBNull(oDataReader.Item("sCity")) Then
                                    _City = oDataReader.Item("sCity") & ""
                                End If
                                If Not IsDBNull(oDataReader.Item("sState")) Then
                                    _State = oDataReader.Item("sState") & ""
                                End If
                                If Not IsDBNull(oDataReader.Item("sZip")) Then
                                    _ZipCode = oDataReader.Item("sZip") & ""
                                End If
                                If Not IsDBNull(oDataReader.Item("sEmploymentStatus")) Then
                                    _EmpStatus = oDataReader.Item("sEmploymentStatus") & ""
                                End If
                            End While
                        End If
                    End If
                    oDataReader.Close()

                    '::: VITAL INFORMATION :::
                    _strSQL = ""
                    _strSQL = "Select sHeight, dWeightinlbs, dWeightinKg, dPulsePerMinute, dPulseOx, " _
                    & " dBloodPressureSittingMin, dBloodPressureSittingMax, dBloodPressureStandingMin, " _
                    & " dBloodPressureStandingMax, dBMI, dTemperature from vitals v where dtvitaldate in (select max(dtvitaldate) from vitals v1 where npatientid=v.npatientid group by nPatientID) AND nPatientid = " & PatientID & ""

                    oDataReader = oDB.ReadQueryRecords(_strSQL)
                    If Not oDataReader Is Nothing Then
                        If oDataReader.HasRows = True Then
                            While oDataReader.Read
                                If Not IsDBNull(oDataReader.Item("sHeight")) Then
                                    Dim _arrPHeight() As String
                                    Dim _PHtFt As Double = 0
                                    Dim _PHtInch As Double = 0

                                    _arrPHeight = GetFtInch(oDataReader.Item("sHeight") & "")
                                    If Not _arrPHeight Is Nothing Then
                                        If Not _arrPHeight(0) Is Nothing Then _PHtFt = Val(_arrPHeight(0))
                                        If Not _arrPHeight(1) Is Nothing Then _PHtInch = Val(_arrPHeight(1))
                                    End If

                                    _Height = FtToMtr(_PHtFt, _PHtInch)
                                End If
                                If Not IsDBNull(oDataReader.Item("dWeightinlbs")) Then
                                    _Weight = Val(oDataReader.Item("dWeightinlbs"))
                                End If
                                If Not IsDBNull(oDataReader.Item("dPulsePerMinute")) Then
                                    _Pulse = Val(oDataReader.Item("dPulsePerMinute"))
                                End If
                                If Not IsDBNull(oDataReader.Item("dPulseOx")) Then
                                    _PulseOX = Val(oDataReader.Item("dPulseOx"))
                                End If
                                If Not IsDBNull(oDataReader.Item("dBloodPressureSittingMin")) Then
                                    _BPSittingMin = Val(oDataReader.Item("dBloodPressureSittingMin"))
                                End If
                                If Not IsDBNull(oDataReader.Item("dBloodPressureSittingMax")) Then
                                    _BPSittingMax = Val(oDataReader.Item("dBloodPressureSittingMax"))
                                End If
                                If Not IsDBNull(oDataReader.Item("dBloodPressureStandingMin")) Then
                                    _BPStandingMin = Val(oDataReader.Item("dBloodPressureStandingMin"))
                                End If
                                If Not IsDBNull(oDataReader.Item("dBloodPressureStandingMax")) Then
                                    _BPStandingMax = Val(oDataReader.Item("dBloodPressureStandingMax"))
                                End If
                                If Not IsDBNull(oDataReader.Item("dBMI")) Then
                                    _BMI = Val(oDataReader.Item("dBMI"))
                                End If
                                If Not IsDBNull(oDataReader.Item("dTemperature")) Then
                                    _Temperature = Val(oDataReader.Item("dTemperature"))
                                End If
                            End While
                        End If
                    End If
                    oDataReader.Close()

                    '::: HISTORIES :::
                    '' COMMENTED BY SUDHIR 20090302
                    '_strSQL = " SELECT DISTINCT History_MST.nHistoryID AS nHistoryID" _
                    '        & " FROM History INNER JOIN " _
                    '        & " History_MST ON History.sHistoryItem = History_MST.sDescription " _
                    '        & " WHERE History.nPatientID = " & PatientID & ""
                    'oDataReader = oDB.ReadQueryRecords(_strSQL)
                    'If Not oDataReader Is Nothing Then
                    '    If oDataReader.HasRows = True Then
                    '        While oDataReader.Read
                    '            If Not IsDBNull(oDataReader.Item("nHistoryID")) Then
                    '                _Histories.Add(oDataReader.Item("nHistoryID"))
                    '            End If
                    '        End While
                    '    End If
                    'End If
                    'oDataReader.Close()

                    '_strSQL = "SELECT sHistoryItem, sHistoryCategory FROM History WHERE nPatientID = " & PatientID & " "
                    'oDataReader = oDB.ReadQueryRecords(_strSQL)
                    'If Not oDataReader Is Nothing Then
                    '    If oDataReader.HasRows = True Then
                    '        Dim oList As myList
                    '        While oDataReader.Read
                    '            oList = New myList
                    '            oList.HistoryItem = oDataReader.Item("sHistoryItem")
                    '            oList.HistoryCategory = oDataReader.Item("sHistoryCategory")
                    '            _Histories.Add(oList)
                    '            oList = Nothing
                    '        End While
                    '    End If
                    '    oDataReader.Close()
                    'End If
                    ''::: DRUGS :::
                    '_strSQL = ""
                    '_strSQL = " SELECT DISTINCT Drugs_MST.nDrugsID AS nDrugsID" _
                    '        & " FROM Medication INNER JOIN Drugs_MST ON Medication.sMedication = Drugs_MST.sDrugName AND Medication.sDosage = Drugs_MST.sDosage " _
                    '        & " WHERE Medication.nPatientID = " & PatientID & ""
                    'oDataReader = oDB.ReadQueryRecords(_strSQL)
                    'If Not oDataReader Is Nothing Then
                    '    If oDataReader.HasRows = True Then
                    '        While oDataReader.Read
                    '            If Not IsDBNull(oDataReader.Item("nDrugsID")) Then
                    '                _Drugs.Add(oDataReader.Item("nDrugsID"))
                    '            End If
                    '        End While
                    '    End If
                    'End If
                    'oDataReader.Close()

                    ''::: ICD9S :::
                    '_strSQL = ""
                    '_strSQL = " SELECT DISTINCT ICD9.nICD9ID AS nICD9ID " _
                    '        & " FROM ICD9 INNER JOIN ExamICD9CPT ON ICD9.sICD9Code = ExamICD9CPT.sICD9Code AND ICD9.sDescription = ExamICD9CPT.sICD9Description " _
                    '        & " WHERE ExamICD9CPT.nPatientID = " & PatientID & ""
                    'oDataReader = oDB.ReadQueryRecords(_strSQL)
                    'If Not oDataReader Is Nothing Then
                    '    If oDataReader.HasRows = True Then
                    '        While oDataReader.Read
                    '            If Not IsDBNull(oDataReader.Item("nICD9ID")) Then
                    '                _ICD9s.Add(oDataReader.Item("nICD9ID"))
                    '            End If
                    '        End While
                    '    End If
                    'End If
                    'oDataReader.Close()

                    ''::: CPTS :::
                    '_strSQL = ""
                    '_strSQL = " SELECT DISTINCT CPT_MST.nCPTID AS nCPTID " _
                    '        & " FROM ExamICD9CPT INNER JOIN CPT_MST ON ExamICD9CPT.sCPTCode = CPT_MST.sCPTCode AND ExamICD9CPT.sCPTDescription = CPT_MST.sDescription " _
                    '        & " WHERE ExamICD9CPT.nPatientID = " & PatientID & ""
                    'oDataReader = oDB.ReadQueryRecords(_strSQL)
                    'If Not oDataReader Is Nothing Then
                    '    If oDataReader.HasRows = True Then
                    '        While oDataReader.Read
                    '            If Not IsDBNull(oDataReader.Item("nCPTID")) Then
                    '                _CPTs.Add(oDataReader.Item("nCPTID"))
                    '            End If
                    '        End While
                    '    End If
                    'End If
                    'oDataReader.Close()

                    ''::: Radiologies :::
                    '_strSQL = ""
                    '_strSQL = " SELECT DISTINCT LM_Test.lm_test_ID AS lm_test_ID " _
                    '        & " FROM LM_Orders INNER JOIN LM_Test ON LM_Orders.lm_test_ID = LM_Test.lm_test_ID " _
                    '        & " WHERE LM_Orders.lm_Patient_ID = " & PatientID & ""
                    'oDataReader = oDB.ReadQueryRecords(_strSQL)
                    'If Not oDataReader Is Nothing Then
                    '    If oDataReader.HasRows = True Then
                    '        While oDataReader.Read
                    '            If Not IsDBNull(oDataReader.Item("lm_test_ID")) Then
                    '                _Labs.Add(oDataReader.Item("lm_test_ID"))
                    '            End If
                    '        End While
                    '    End If
                    'End If
                    'oDataReader.Close()

                    ''::: Labs :::
                    '_strSQL = "SELECT Lab_Order_Test_ResultDtl.labotrd_OrderID, Lab_Order_Test_ResultDtl.labotrd_TestID, " _
                    '& " Lab_Order_Test_ResultDtl.labotrd_ResultNameID, Lab_Order_Test_ResultDtl.labotrd_ResultValue " _
                    '& " FROM Lab_Order_MST INNER JOIN Lab_Order_Test_Result ON Lab_Order_MST.labom_OrderID = Lab_Order_Test_Result.labotr_OrderID INNER JOIN Lab_Order_Test_ResultDtl ON Lab_Order_Test_Result.labotr_OrderID = Lab_Order_Test_ResultDtl.labotrd_OrderID AND " _
                    '& " Lab_Order_Test_Result.labotr_TestID = Lab_Order_Test_ResultDtl.labotrd_TestID WHERE(Lab_Order_MST.labom_PatientID = " & PatientID & ")"

                    'oDataReader = oDB.ReadQueryRecords(_strSQL)
                    'If Not oDataReader Is Nothing Then
                    '    If oDataReader.HasRows = True Then
                    '        While oDataReader.Read
                    '            If IsDBNull(oDataReader.Item("labotrd_TestID")) = False And IsDBNull(oDataReader.Item("labotrd_ResultNameID")) = False And IsDBNull(oDataReader.Item("labotrd_ResultValue")) = False Then
                    '                Dim _LabModDtl As New gloStream.DiseaseManagement.Supporting.LabModulePatientDetail
                    '                With _LabModDtl
                    '                    If Not IsDBNull(oDataReader.Item("labotrd_OrderID")) Then
                    '                        .OrderID = oDataReader.Item("labotrd_OrderID")
                    '                    End If
                    '                    .TestID = oDataReader.Item("labotrd_TestID")
                    '                    .ResultNameID = oDataReader.Item("labotrd_ResultNameID")
                    '                    .ResultValue = oDataReader.Item("labotrd_ResultValue")
                    '                End With
                    '                _LabModule.Add(_LabModDtl)
                    '                _LabModDtl = Nothing
                    '            End If
                    '        End While
                    '    End If
                    '    oDataReader.Close()
                    'End If


                    '*********************>>>--- READ CRITERIA CONDITION ---<<<*******************************
                    'connect to the database    

                    '' ::: CHECK CRITERIAS FOR GENDER & AGE ::: 
                    Dim COL_CariteriaforSinglePatient As New Collection
                    Dim strCriteriaID As String = ""

                    'Resolve Bug ID : 43905
                    _strSQL = ""
                    _strSQL = " SELECT dm_mst_Id, dm_mst_AgeMin, dm_mst_AgeMax,DATEDIFF(DAY,DATEADD(YEAR,-dm_mst_AgeMin,dbo.gloGetDate()),dbo.gloGetDate()) as AgeMin, DATEDIFF(DAY,DATEADD(YEAR,-dm_mst_AgeMax,dbo.gloGetDate()),dbo.gloGetDate()) as AgeMax " _
                            & " FROM dm_criteria_mst WHERE dm_mst_Gender = '" & _Geneder & "' OR  dm_mst_Gender = 'ALL'"

                    oDataReader = oDB.ReadQueryRecords(_strSQL)
                    If Not oDataReader Is Nothing Then
                        If oDataReader.HasRows = True Then
                            While oDataReader.Read
                                If Not IsDBNull(oDataReader.Item("dm_mst_Id")) Then
                                    If Not IsDBNull(oDataReader.Item("dm_mst_AgeMin")) AndAlso Not IsDBNull(oDataReader.Item("dm_mst_AgeMax")) Then
                                        If Val(oDataReader.Item("AgeMin")) <= _Age AndAlso Val(oDataReader.Item("AgeMax")) >= _Age Then
                                            '' Check For MIN MAX Age Crteria
                                            COL_CariteriaforSinglePatient.Add(oDataReader.Item("dm_mst_Id"))
                                        ElseIf Val(oDataReader.Item("AgeMin")) <= _Age AndAlso Val(oDataReader.Item("AgeMax")) = 0 Then
                                            '' Check For Only MIN Age Crteria , MAX Age = 0
                                            COL_CariteriaforSinglePatient.Add(oDataReader.Item("dm_mst_Id"))
                                        ElseIf Val(oDataReader.Item("AgeMin")) = 0 AndAlso Val(oDataReader.Item("AgeMax")) >= _Age Then
                                            '' Check For Only MAX Age Crteria , MIN Age = 0
                                            COL_CariteriaforSinglePatient.Add(oDataReader.Item("dm_mst_Id"))
                                        ElseIf Val(oDataReader.Item("AgeMin")) <= _Age AndAlso Val(oDataReader.Item("AgeMax")) >= _Age Then
                                            '' Check For No Age Crteria  MIN Age = 0 , MAX Age = 0
                                            COL_CariteriaforSinglePatient.Add(oDataReader.Item("dm_mst_Id"))
                                        End If
                                    End If
                                End If
                            End While
                        End If
                    End If
                    oDataReader.Close()

                    If COL_CariteriaforSinglePatient.Count > 0 Then
                        '''' :::  FOR RACE  ::: 
                        strCriteriaID = GetIDsAsString(COL_CariteriaforSinglePatient)
                        COL_CariteriaforSinglePatient = New Collection

                        _strSQL = ""
                        _strSQL = " SELECT dm_mst_Id " _
                                & " FROM dm_criteria_mst WHERE (dm_mst_Race = '" & _Race.Trim.Replace("'", "''") & "' Or ISNULL(dm_mst_Race,'') = '') AND (dm_criteria_mst.dm_mst_Id IN (" & strCriteriaID & "))"

                        COL_CariteriaforSinglePatient = getCriteriaIDColection(_strSQL)

                        'oDataReader = oDB.ReadQueryRecords(_strSQL)
                        'If Not oDataReader Is Nothing Then
                        '    If oDataReader.HasRows = True Then
                        '        While oDataReader.Read
                        '            If Not IsDBNull(oDataReader.Item("dm_mst_Id")) Then
                        '                COL_CariteriaforSinglePatient.Add(oDataReader.Item("dm_mst_Id"))
                        '            End If
                        '        End While
                        '    End If
                        'End If
                        'oDataReader.Close()
                    End If

                    If COL_CariteriaforSinglePatient.Count > 0 Then
                        '''' :::  FOR Marital Status  ::: 
                        strCriteriaID = GetIDsAsString(COL_CariteriaforSinglePatient)
                        COL_CariteriaforSinglePatient = New Collection

                        _strSQL = ""
                        _strSQL = " SELECT dm_mst_Id " _
                                & " FROM dm_criteria_mst WHERE (dm_mst_MaritalStatus = '" & _MaritalStatus.Trim.Replace("'", "''") & "' OR ISNULL(dm_mst_MaritalStatus,'') = '' )AND (dm_criteria_mst.dm_mst_Id IN (" & strCriteriaID & "))"

                        COL_CariteriaforSinglePatient = getCriteriaIDColection(_strSQL)

                    End If

                    If COL_CariteriaforSinglePatient.Count > 0 Then
                        '''' :::  FOR City  ::: 
                        strCriteriaID = GetIDsAsString(COL_CariteriaforSinglePatient)
                        COL_CariteriaforSinglePatient = New Collection

                        _strSQL = ""
                        _strSQL = " SELECT dm_mst_Id " _
                                & " FROM dm_criteria_mst WHERE (dm_mst_City = '" & _City.Trim.Replace("'", "''") & "' OR ISNULL(dm_mst_City,'') = '')  AND (dm_criteria_mst.dm_mst_Id IN (" & strCriteriaID & "))"

                        COL_CariteriaforSinglePatient = getCriteriaIDColection(_strSQL)

                    End If

                    If COL_CariteriaforSinglePatient.Count > 0 Then
                        '''' :::  FOR State  ::: 
                        strCriteriaID = GetIDsAsString(COL_CariteriaforSinglePatient)
                        COL_CariteriaforSinglePatient = New Collection

                        _strSQL = ""
                        _strSQL = " SELECT dm_mst_Id " _
                                & " FROM dm_criteria_mst WHERE (dm_mst_Status = '" & _State.Trim.Replace("'", "''") & "' OR ISNULL(dm_mst_Status,'') = '') AND (dm_criteria_mst.dm_mst_Id IN (" & strCriteriaID & "))"

                        COL_CariteriaforSinglePatient = getCriteriaIDColection(_strSQL)

                    End If

                    If COL_CariteriaforSinglePatient.Count > 0 Then
                        '''' :::  FOR _ZipCode  ::: 
                        strCriteriaID = GetIDsAsString(COL_CariteriaforSinglePatient)
                        COL_CariteriaforSinglePatient = New Collection

                        _strSQL = ""
                        _strSQL = " SELECT dm_mst_Id " _
                                & " FROM dm_criteria_mst WHERE ( dm_mst_Zip = '" & _ZipCode.Trim.Replace("'", "''") & "' OR ISNULL(dm_mst_Zip,'') = '')  AND (dm_criteria_mst.dm_mst_Id IN (" & strCriteriaID & "))"

                        COL_CariteriaforSinglePatient = getCriteriaIDColection(_strSQL)
                    End If

                    If COL_CariteriaforSinglePatient.Count > 0 Then
                        '''' :::  FOR EmpStatus  ::: 
                        strCriteriaID = GetIDsAsString(COL_CariteriaforSinglePatient)
                        COL_CariteriaforSinglePatient = New Collection

                        _strSQL = ""
                        _strSQL = " SELECT dm_mst_Id " _
                                & " FROM dm_criteria_mst WHERE (dm_mst_EmplyementStatus = '" & _EmpStatus.Trim.Replace("'", "''") & "' OR ISNULL(dm_mst_EmplyementStatus,'') = '')  AND (dm_criteria_mst.dm_mst_Id IN (" & strCriteriaID & "))"

                        COL_CariteriaforSinglePatient = getCriteriaIDColection(_strSQL)
                    End If


                    If COL_CariteriaforSinglePatient.Count > 0 Then
                        '''' :::  FOR EmpStatus  ::: 
                        strCriteriaID = GetIDsAsString(COL_CariteriaforSinglePatient)
                        COL_CariteriaforSinglePatient = New Collection

                        _strSQL = ""
                        _strSQL = " SELECT dm_mst_Id " _
                                & " FROM dm_criteria_mst WHERE (dm_mst_EmplyementStatus = '" & _EmpStatus.Trim.Replace("'", "''") & "' OR ISNULL(dm_mst_EmplyementStatus,'') = '')  AND (dm_criteria_mst.dm_mst_Id IN (" & strCriteriaID & "))"

                        COL_CariteriaforSinglePatient = getCriteriaIDColection(_strSQL)

                        'oDataReader = oDB.ReadQueryRecords(_strSQL)
                        'If Not oDataReader Is Nothing Then
                        '    If oDataReader.HasRows = True Then
                        '        While oDataReader.Read
                        '            If Not IsDBNull(oDataReader.Item("dm_mst_Id")) Then
                        '                COL_CariteriaforSinglePatient.Add(oDataReader.Item("dm_mst_Id"))
                        '            End If
                        '        End While
                        '    End If
                        'End If
                        'oDataReader.Close()
                    End If

                    ''''Dim _Height As Double = 0
                    ''''Dim _Weight As Double = 0
                    ''''Dim _Pulse As Double = 0
                    ''''Dim _PulseOX As Double = 0
                    ''''Dim _BPSittingMin As Double = 0
                    ''''Dim _BPSittingMax As Double = 0
                    ''''Dim _BPStandingMin As Double = 0
                    ''''Dim _BPStandingMax As Double = 0
                    ''''Dim _BMI As Double = 0
                    ''''Dim _Temperature As Double = 0
                    If COL_CariteriaforSinglePatient.Count > 0 Then
                        '''' :::  FOR Height  ::: 
                        strCriteriaID = GetIDsAsString(COL_CariteriaforSinglePatient)
                        COL_CariteriaforSinglePatient = New Collection

                        _strSQL = ""
                        _strSQL = " SELECT dm_mst_Id " _
                                & " FROM dm_criteria_mst WHERE (dm_mst_EmplyementStatus = '" & _EmpStatus.Trim.Replace("'", "''") & "' OR ISNULL(dm_mst_EmplyementStatus,'') = '')  AND (dm_criteria_mst.dm_mst_Id IN (" & strCriteriaID & "))"

                        COL_CariteriaforSinglePatient = getCriteriaIDColection(_strSQL)

                    End If
                    ' RaiseEvent ProcessCriteria("Start Findings")

                    If COL_CariteriaforSinglePatient.Count > 0 Then
                        For i As Integer = COL_CariteriaforSinglePatient.Count To 1 Step -1
                            If FindGuidelinesForSinglePatientCriteria(COL_CariteriaforSinglePatient(i), PatientID) = False Then
                                COL_CariteriaforSinglePatient.Remove(i)
                            End If
                        Next
                    End If

                    '*********************>>>--- END READ CRITERIA CONDITION ---<<<*******************************

                    '*********************>>>--- READ PATIENT SPECIFIC CRITERIA CONDITION ---<<<*******************************
                    '' To get the Patient Specific criteria conditions
                    Dim Col_PatientSpecific As New Collection

                    _strSQL = ""
                    If PatientID > 0 Then
                        _strSQL = "SELECT dm_mst_Id FROM DM_Criteria_MST WHERE dm_mst_PatientID = " & PatientID & ""
                        Col_PatientSpecific = getCriteriaIDColection(_strSQL)
                    End If


                    If IsNothing(Col_PatientSpecific) = False Then
                        If Col_PatientSpecific.Count > 0 Then
                            For i As Integer = 1 To Col_PatientSpecific.Count
                                COL_CariteriaforSinglePatient.Add(Col_PatientSpecific(i))
                            Next
                        End If
                    End If

                    '*********************>>>--- END READ PATIENT SPECIFIC CRITERIA CONDITION ---<<<*******************************

                    UpdateLog(" END DM - FindGuidelinesForSinglePatient")

                    Return COL_CariteriaforSinglePatient
                Catch ex As SqlException
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    UpdateLog("clsDiseaseManagement -- FindGuidelinesForSinglePatient -- " & ex.ToString)
                    Return Nothing
                Catch ex As Exception
                    UpdateLog("clsDiseaseManagement -- FindGuidelinesForSinglePatient -- " & ex.ToString)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    'oDataReader.Close()
                    oDB.Disconnect()
                    oDB.Dispose()
                    oDB = Nothing
                End Try
            End Function

            '            Private Function FindGuidelinesForSinglePatientCriteria(ByVal CriteriaID As Long, ByVal PatientID As Long) As Boolean
            '                'A & D
            '                Dim _strSQL As String = ""
            '                Dim oDB As New gloStream.gloDataBase.gloDataBase
            '                Dim oDataReader As SqlClient.SqlDataReader

            '                Dim _AgeMin As Int16 = 0
            '                Dim _AgeMax As Int16 = 0
            '                Dim _Geneder As String = ""
            '                Dim _Race As String = ""
            '                Dim _MaritalStatus As String = ""
            '                Dim _City As String = ""
            '                Dim _State As String = ""
            '                Dim _ZipCode As String = ""
            '                Dim _EmpStatus As String = ""
            '                Dim _HeightFtMin As Double = 0
            '                Dim _HeightFtMax As Double = 0
            '                Dim _HeightInchMin As Double = 0
            '                Dim _HeightInchMax As Double = 0
            '                Dim _WeightMin As Double = 0
            '                Dim _WeightMax As Double = 0
            '                Dim _PulseMin As Double = 0
            '                Dim _PulseMax As Double = 0
            '                Dim _PulseOXMin As Double = 0
            '                Dim _PulseOXMax As Double = 0
            '                Dim _BPSittingMin As Double = 0
            '                Dim _BPSittingMax As Double = 0
            '                Dim _BPStandingMin As Double = 0
            '                Dim _BPStandingMax As Double = 0
            '                Dim _BMIMin As Double = 0
            '                Dim _BMIMax As Double = 0
            '                Dim _TempMin As Double = 0
            '                Dim _TempMax As Double = 0
            '                Dim _HaveVital As Boolean = False
            '                Dim _HaveVitalHeight As Boolean = False

            '                Dim _Histories As New Collection
            '                Dim _Drugs As New Collection
            '                Dim _ICD9s As New Collection
            '                Dim _CPTs As New Collection
            '                Dim _Labs As New Collection
            '                Dim _LabModule As New Collection

            '                Dim _FinalPatientID As New Collection
            '                Dim _PrimaryPatientID As New Collection
            '                Dim _TempPatientID As New Collection
            '                Dim _PrimaryINPatientID As String = ""

            '                Try
            '                    UpdateLog(" START DM - FindGuidelinesForSinglePatientCriteria")
            '                    ''Application.DoEvents()
            '                    'RaiseEvent StartCriteria(True)
            '                    '*********************>>>--- READ CRITERIA CONDITION ---<<<*******************************
            '                    'connect to the database    
            '                    oDB.Connect(GetConnectionString)

            '                    'set the query string to retrieve the Patient record from the Patient table from the PatientID passed
            '                    _strSQL = " SELECT dm_mst_Id,dm_mst_AgeMin,dm_mst_AgeMax, " _
            '                            & " dm_mst_Gender,dm_mst_Race,dm_mst_MaritalStatus, " _
            '                            & " dm_mst_City,dm_mst_Status,dm_mst_Zip, " _
            '                            & " dm_mst_EmplyementStatus,dm_mst_HeightMin,dm_mst_HeightMax, " _
            '                            & " dm_mst_WeightMin,dm_mst_WeightMax, " _
            '                            & " dm_mst_BMIMin,dm_mst_BMIMax,dm_mst_TemperatureMin,dm_mst_TemperatureMax, " _
            '                            & " dm_mst_PulseMin,dm_mst_PulseMax,dm_mst_PulseOxMin,dm_mst_PulseOxMax, " _
            '                            & " dm_mst_BPSittingMin,dm_mst_BPSittingMax,dm_mst_BPStandingMin,dm_mst_BPStandingMax, " _
            '                            & " dm_mst_DisplayMessage from dm_criteria_mst WHERE dm_mst_Id = " & CriteriaID & " "

            '                    ''RaiseEvent ProcessCriteria("Start Findings")

            '                    'execute the query and get the results in a datareader
            '                    oDataReader = oDB.ReadQueryRecords(_strSQL)
            '                    If Not oDataReader Is Nothing Then
            '                        If oDataReader.HasRows = True Then
            '                            While oDataReader.Read
            '                                If Not IsDBNull(oDataReader.Item("dm_mst_AgeMin")) Then
            '                                    _AgeMin = oDataReader.Item("dm_mst_AgeMin") & ""
            '                                End If
            '                                If Not IsDBNull(oDataReader.Item("dm_mst_AgeMax")) Then
            '                                    _AgeMax = oDataReader.Item("dm_mst_AgeMax") & ""
            '                                End If
            '                                If Not IsDBNull(oDataReader.Item("dm_mst_Gender")) Then
            '                                    _Geneder = oDataReader.Item("dm_mst_Gender") & ""
            '                                End If
            '                                If Not IsDBNull(oDataReader.Item("dm_mst_Race")) Then
            '                                    _Race = oDataReader.Item("dm_mst_Race") & ""
            '                                End If
            '                                If Not IsDBNull(oDataReader.Item("dm_mst_MaritalStatus")) Then
            '                                    _MaritalStatus = oDataReader.Item("dm_mst_MaritalStatus") & ""
            '                                End If
            '                                If Not IsDBNull(oDataReader.Item("dm_mst_City")) Then
            '                                    _City = oDataReader.Item("dm_mst_City") & ""
            '                                End If
            '                                If Not IsDBNull(oDataReader.Item("dm_mst_Status")) Then
            '                                    _State = oDataReader.Item("dm_mst_Status") & ""
            '                                End If
            '                                If Not IsDBNull(oDataReader.Item("dm_mst_Zip")) Then
            '                                    _ZipCode = oDataReader.Item("dm_mst_Zip") & ""
            '                                End If
            '                                If Not IsDBNull(oDataReader.Item("dm_mst_EmplyementStatus")) Then
            '                                    _EmpStatus = oDataReader.Item("dm_mst_EmplyementStatus") & ""
            '                                End If

            '                                Dim arrPatHeightMin() As String
            '                                If Not IsDBNull(oDataReader.Item("dm_mst_HeightMin")) Then
            '                                    arrPatHeightMin = GetFtInch(oDataReader.Item("dm_mst_HeightMin") & "")
            '                                    If Not arrPatHeightMin Is Nothing Then
            '                                        If Not arrPatHeightMin(0) Is Nothing Then
            '                                            _HeightFtMin = Val(arrPatHeightMin(0))
            '                                            If Not arrPatHeightMin(1) Is Nothing Then _HeightInchMin = Val(arrPatHeightMin(1))
            '                                        End If
            '                                    End If
            '                                End If

            '                                Dim arrPatHeightMax() As String
            '                                If Not IsDBNull(oDataReader.Item("dm_mst_HeightMax")) Then
            '                                    arrPatHeightMax = GetFtInch(oDataReader.Item("dm_mst_HeightMax") & "")
            '                                    If Not arrPatHeightMax Is Nothing Then
            '                                        If Not arrPatHeightMax(0) Is Nothing Then
            '                                            _HeightFtMax = Val(arrPatHeightMax(0))
            '                                            If Not arrPatHeightMax(1) Is Nothing Then _HeightInchMax = Val(arrPatHeightMax(1))
            '                                        End If
            '                                    End If
            '                                End If

            '                                '// now here we directlly convert height into meter, so we use only ft varibale against this
            '                                _HeightFtMin = FtToMtr(_HeightFtMin, _HeightInchMin)
            '                                _HeightFtMax = FtToMtr(_HeightFtMax, _HeightInchMax)
            '                                If _HeightFtMin + _HeightFtMax > 0 Then
            '                                    _HaveVitalHeight = True
            '                                End If

            '                                If Not IsDBNull(oDataReader.Item("dm_mst_WeightMin")) Then
            '                                    _WeightMin = oDataReader.Item("dm_mst_WeightMin") & ""
            '                                    If _WeightMin > 0 Then _HaveVital = True
            '                                End If
            '                                If Not IsDBNull(oDataReader.Item("dm_mst_WeightMax")) Then
            '                                    _WeightMax = oDataReader.Item("dm_mst_WeightMax") & ""
            '                                    If _WeightMax > 0 Then _HaveVital = True
            '                                End If
            '                                If Not IsDBNull(oDataReader.Item("dm_mst_PulseMin")) Then
            '                                    _PulseMin = oDataReader.Item("dm_mst_PulseMin") & ""
            '                                    If _PulseMin > 0 Then _HaveVital = True
            '                                End If
            '                                If Not IsDBNull(oDataReader.Item("dm_mst_PulseMax")) Then
            '                                    _PulseMax = oDataReader.Item("dm_mst_PulseMax") & ""
            '                                    If _PulseMax > 0 Then _HaveVital = True
            '                                End If
            '                                If Not IsDBNull(oDataReader.Item("dm_mst_PulseOxMin")) Then
            '                                    _PulseOXMin = oDataReader.Item("dm_mst_PulseOxMin") & ""
            '                                    If _PulseOXMin > 0 Then _HaveVital = True
            '                                End If
            '                                If Not IsDBNull(oDataReader.Item("dm_mst_PulseOxMax")) Then
            '                                    _PulseOXMax = oDataReader.Item("dm_mst_PulseOxMax") & ""
            '                                    If _PulseOXMax > 0 Then _HaveVital = True
            '                                End If
            '                                If Not IsDBNull(oDataReader.Item("dm_mst_BPSittingMin")) Then
            '                                    _BPSittingMin = oDataReader.Item("dm_mst_BPSittingMin") & ""
            '                                    If _BPSittingMin > 0 Then _HaveVital = True
            '                                End If
            '                                If Not IsDBNull(oDataReader.Item("dm_mst_BPSittingMax")) Then
            '                                    _BPSittingMax = oDataReader.Item("dm_mst_BPSittingMax") & ""
            '                                    If _BPSittingMax > 0 Then _HaveVital = True
            '                                End If
            '                                If Not IsDBNull(oDataReader.Item("dm_mst_BPStandingMin")) Then
            '                                    _BPStandingMin = oDataReader.Item("dm_mst_BPStandingMin") & ""
            '                                    If _BPStandingMin > 0 Then _HaveVital = True
            '                                End If
            '                                If Not IsDBNull(oDataReader.Item("dm_mst_BPStandingMax")) Then
            '                                    _BPStandingMax = oDataReader.Item("dm_mst_BPStandingMax") & ""
            '                                    If _BPStandingMax > 0 Then _HaveVital = True
            '                                End If
            '                                If Not IsDBNull(oDataReader.Item("dm_mst_BMIMin")) Then
            '                                    _BMIMin = oDataReader.Item("dm_mst_BMIMin") & ""
            '                                    If _BMIMin > 0 Then _HaveVital = True
            '                                End If
            '                                If Not IsDBNull(oDataReader.Item("dm_mst_BMIMax")) Then
            '                                    _BMIMax = oDataReader.Item("dm_mst_BMIMax") & ""
            '                                    If _BMIMax > 0 Then _HaveVital = True
            '                                End If
            '                                If Not IsDBNull(oDataReader.Item("dm_mst_TemperatureMin")) Then
            '                                    _TempMin = oDataReader.Item("dm_mst_TemperatureMin") & ""
            '                                    If _TempMin > 0 Then _HaveVital = True
            '                                End If
            '                                If Not IsDBNull(oDataReader.Item("dm_mst_TemperatureMax")) Then
            '                                    _TempMax = oDataReader.Item("dm_mst_TemperatureMax") & ""
            '                                    If _TempMax > 0 Then _HaveVital = True
            '                                End If
            '                            End While
            '                        End If
            '                        oDataReader.Close()
            '                    End If

            '                    '// History
            '                    '_strSQL = "SELECT dm_Chdtl_HistoryItemId FROM DM_CriteriaHistory_DTL WHERE dm_Chdtl_Id = " & CriteriaID & ""
            '                    _strSQL = "SELECT ISNULL(dm_Chdtl_HistoryItem,'') AS dm_Chdtl_HistoryItem, ISNULL(dm_Chdtl_HistoryCategory,'') AS dm_Chdtl_HistoryCategory FROM DM_CriteriaHistory_DTL WHERE dm_Chdtl_Id = " & CriteriaID & ""
            '                    oDataReader = oDB.ReadQueryRecords(_strSQL)
            '                    If Not oDataReader Is Nothing Then
            '                        If oDataReader.HasRows = True Then
            '                            Dim oList As myList
            '                            While oDataReader.Read
            '                                '_Histories.Add(oDataReader.Item("dm_Chdtl_HistoryItemId"))
            '                                oList = New myList
            '                                oList.HistoryItem = oDataReader.Item("dm_Chdtl_HistoryItem")
            '                                oList.HistoryCategory = oDataReader.Item("dm_Chdtl_HistoryCategory")
            '                                _Histories.Add(oList)
            '                                oList = Nothing
            '                            End While
            '                        End If
            '                    End If
            '                    oDataReader.Close()

            '                    '// Drugs
            '                    _strSQL = "SELECT dm_Drugdtl_DrugID FROM DM_CriteriaDrug_DTL WHERE dm_Drugdtl_Id = " & CriteriaID & ""
            '                    oDataReader = oDB.ReadQueryRecords(_strSQL)
            '                    If Not oDataReader Is Nothing Then
            '                        If oDataReader.HasRows = True Then
            '                            While oDataReader.Read
            '                                _Drugs.Add(oDataReader.Item("dm_Drugdtl_DrugID"))
            '                            End While
            '                        End If
            '                    End If
            '                    oDataReader.Close()

            '                    '// ICD9
            '                    _strSQL = "SELECT dm_ICD9CPTdtl_ICID FROM DM_ICD9CPT_DTL WHERE dm_ICD9CPTdtl_Id = " & CriteriaID & " AND dm_ICD9CPTdtl_Type = 2"
            '                    oDataReader = oDB.ReadQueryRecords(_strSQL)
            '                    If Not oDataReader Is Nothing Then
            '                        If oDataReader.HasRows = True Then
            '                            While oDataReader.Read
            '                                _ICD9s.Add(oDataReader.Item("dm_ICD9CPTdtl_ICID"))
            '                            End While
            '                        End If
            '                    End If
            '                    oDataReader.Close()

            '                    '// CPT
            '                    _strSQL = "SELECT dm_ICD9CPTdtl_ICID FROM DM_ICD9CPT_DTL WHERE dm_ICD9CPTdtl_Id = " & CriteriaID & " AND dm_ICD9CPTdtl_Type = 1"
            '                    oDataReader = oDB.ReadQueryRecords(_strSQL)
            '                    If Not oDataReader Is Nothing Then
            '                        If oDataReader.HasRows = True Then
            '                            While oDataReader.Read
            '                                _CPTs.Add(oDataReader.Item("dm_ICD9CPTdtl_ICID"))
            '                            End While
            '                        End If
            '                    End If
            '                    oDataReader.Close()

            '                    '// Labs
            '                    _strSQL = "SELECT dm_Labsdtl_TestID FROM DM_Labs_DTL WHERE dm_Labsdtl_Id = " & CriteriaID & " "
            '                    oDataReader = oDB.ReadQueryRecords(_strSQL)
            '                    If Not oDataReader Is Nothing Then
            '                        If oDataReader.HasRows = True Then
            '                            While oDataReader.Read
            '                                _Labs.Add(oDataReader.Item("dm_Labsdtl_TestID"))
            '                            End While
            '                        End If
            '                    End If
            '                    oDataReader.Close()

            '                    '// Mahesh 20070804
            '                    '// Labs
            '                    _strSQL = "SELECT dm_labdtl_testId,dm_labdtl_resultid FROM DM_LabModule_DTL WHERE dm_labdtl_ID = " & CriteriaID & " "
            '                    oDataReader = oDB.ReadQueryRecords(_strSQL)
            '                    If Not oDataReader Is Nothing Then
            '                        If oDataReader.HasRows = True Then
            '                            While oDataReader.Read
            '                                _LabModule.Add(oDataReader.Item("dm_labdtl_testId") & "|" & oDataReader.Item("dm_labdtl_resultid"))
            '                            End While
            '                        End If
            '                        oDataReader.Close()
            '                    End If

            '                    '//

            '                    '*********************>>>--- READ CRITERIA CONDITION ---<<<*******************************

            '                    '*********************>>>--- QUERY BUILDER AS PER CONDTION ---<<<*************************
            '                    _strSQL = "SELECT nPatientID FROM Patient WHERE "
            '                    Dim _CreateSQL As Boolean = False
            '                    Dim _AgeMinDate As Date, _AgeMaxDate As Date
            '                    '>>>AGE<<<
            '                    If _AgeMin > 0 AndAlso _AgeMax > 0 Then
            '                        _AgeMinDate = DateAdd(DateInterval.Year, -_AgeMin, Date.Now) ' eg. 2012
            '                        _AgeMaxDate = DateAdd(DateInterval.Year, -_AgeMax, Date.Now) ' eg. 1930
            '                        _strSQL = _strSQL & "dtDOB BETWEEN '" & _AgeMaxDate & "' AND '" & _AgeMinDate & "' "
            '                        _CreateSQL = True
            '                    Else
            '                        If _AgeMin > 0 AndAlso _AgeMax = 0 Then
            '                            _AgeMinDate = DateAdd(DateInterval.Year, -_AgeMin, Date.Now)
            '                            _strSQL = _strSQL & "dtDOB >= '" & _AgeMinDate & "' "
            '                            _CreateSQL = True
            '                        ElseIf _AgeMax > 0 AndAlso _AgeMin = 0 Then
            '                            _AgeMaxDate = DateAdd(DateInterval.Year, -_AgeMax, Date.Now)
            '                            _strSQL = _strSQL & "dtDOB <= '" & _AgeMaxDate & "' "
            '                            _CreateSQL = True
            '                        End If
            '                    End If
            '                    '----------------------------------------
            '                    If _CreateSQL = False Then Exit Function
            '                    '----------------------------------------
            '                    '>>>GENEDER<<<
            '                    If _Geneder.Trim <> "" Then
            '                        If Not _Geneder.Trim = "All" Then
            '                            _strSQL = _strSQL & " AND sGender = '" & _Geneder & "'"
            '                        End If
            '                    End If
            '                    '>>>RACE<<<
            '                    If _Race.Trim <> "" Then
            '                        _strSQL = _strSQL & " AND sRace = '" & _Race & "'"
            '                    End If
            '                    '>>>MARITAL STATUS<<<
            '                    If _MaritalStatus.Trim <> "" Then
            '                        _strSQL = _strSQL & " AND sMaritalStatus = '" & _MaritalStatus & "'"
            '                    End If
            '                    '>>>CITY<<<
            '                    If _City.Trim <> "" Then
            '                        _strSQL = _strSQL & " AND sCity = '" & _City & "'"
            '                    End If
            '                    '>>>STATE<<<
            '                    If _State.Trim <> "" Then
            '                        _strSQL = _strSQL & " AND sState = '" & _State & "'"
            '                    End If
            '                    '>>>ZIPCODE<<<
            '                    If _ZipCode.Trim <> "" Then
            '                        _strSQL = _strSQL & " AND sZIP = '" & _ZipCode & "'"
            '                    End If
            '                    '>>>ZIPCODE<<<
            '                    If _ZipCode.Trim <> "" Then
            '                        _strSQL = _strSQL & " AND sZIP = '" & _ZipCode & "'"
            '                    End If
            '                    '>>>EMPSTATUS<<<
            '                    If _EmpStatus.Trim <> "" Then
            '                        _strSQL = _strSQL & " AND sEmploymentStatus = '" & _EmpStatus & "'"
            '                    End If

            '                    If Not _strSQL.Trim = "" Then

            '                        _strSQL = _strSQL & " AND nPatientID = " & PatientID

            '                        oDataReader = oDB.ReadQueryRecords(_strSQL)
            '                        If Not oDataReader Is Nothing Then
            '                            If oDataReader.HasRows = True Then
            '                                While oDataReader.Read
            '                                    If Not IsDBNull(oDataReader.Item("nPatientID")) Then
            '                                        _PrimaryPatientID.Add(oDataReader.Item("nPatientID"))
            '                                    End If
            '                                End While
            '                            End If
            '                            oDataReader.Close()
            '                        End If
            '                    End If
            '                    For i As Int16 = 1 To _PrimaryPatientID.Count
            '                        If i > 1 Then
            '                            _PrimaryINPatientID = _PrimaryINPatientID & "," & _PrimaryPatientID(i)
            '                        Else
            '                            _PrimaryINPatientID = _PrimaryPatientID(i)
            '                        End If
            '                    Next

            '                    ':::VITALS:::

            '                    'Dim _PrimaryPatientID As New Collection
            '                    'Dim _TempPatientID As New Collection
            '                    'Dim _PrimaryINPatientID As String = ""

            '                    Dim _RunVitals As Boolean = False
            '                    If _PrimaryPatientID.Count > 0 Then
            '                        _strSQL = ""
            '                        _strSQL = "Select distinct nPatientID from vitals v where dtvitaldate in " _
            '                        & " (select max(dtvitaldate) from vitals v1 where npatientid=v.npatientid group by nPatientID) " _
            '                        & " AND nPatientid in (" & _PrimaryINPatientID & ")"


            '                        '>>>Weight Min & Max <<<
            '                        If _WeightMin > 0 AndAlso _WeightMax > 0 Then
            '                            _strSQL = _strSQL & " AND dWeightinlbs BETWEEN " & _WeightMin & " AND " & _WeightMax & ""
            '                            _RunVitals = True
            '                        Else
            '                            If _WeightMin > 0 AndAlso _WeightMax = 0 Then
            '                                _strSQL = _strSQL & " AND dWeightinlbs >= " & _WeightMin & ""
            '                                _RunVitals = True
            '                            ElseIf _WeightMax > 0 AndAlso _WeightMin = 0 Then
            '                                _strSQL = _strSQL & " AND dWeightinlbs <= " & _WeightMax & ""
            '                                _RunVitals = True
            '                            End If
            '                        End If
            '                        '>>>Pulse Min & Max <<<
            '                        If _PulseMin > 0 AndAlso _PulseMax > 0 Then
            '                            _strSQL = _strSQL & " AND dPulsePerMinute BETWEEN " & _PulseMin & " AND " & _PulseMax & ""
            '                            _RunVitals = True
            '                        Else
            '                            If _PulseMin > 0 AndAlso _PulseMax = 0 Then
            '                                _strSQL = _strSQL & " AND dPulsePerMinute >= " & _PulseMin & ""
            '                                _RunVitals = True
            '                            ElseIf _PulseMax > 0 AndAlso _PulseMin = 0 Then
            '                                _strSQL = _strSQL & " AND dPulsePerMinute <= " & _PulseMax & ""
            '                                _RunVitals = True
            '                            End If
            '                        End If
            '                        '>>>Pulse OX Min & Max <<<
            '                        If _PulseOXMin > 0 AndAlso _PulseOXMax > 0 Then
            '                            _strSQL = _strSQL & " AND dPulseOx BETWEEN " & _PulseOXMin & " AND " & _PulseOXMax & ""
            '                            _RunVitals = True
            '                        Else
            '                            If _PulseOXMin > 0 AndAlso _PulseOXMax = 0 Then
            '                                _strSQL = _strSQL & " AND dPulseOx >= " & _PulseOXMin & ""
            '                                _RunVitals = True
            '                            ElseIf _PulseMax > 0 AndAlso _PulseMin = 0 Then
            '                                _strSQL = _strSQL & " AND dPulseOx <= " & _PulseOXMax & ""
            '                                _RunVitals = True
            '                            End If
            '                        End If
            '                        '>>>BP Sitting Min & Max <<<
            '                        If _BPSittingMin > 0 AndAlso _BPSittingMax > 0 Then
            '                            _strSQL = _strSQL & " AND dBloodPressureSittingMin >= " & _BPSittingMin & " AND dBloodPressureSittingMax <= " & _BPSittingMax & ""
            '                            _RunVitals = True
            '                        Else
            '                            If _BPSittingMin > 0 AndAlso _BPSittingMax = 0 Then
            '                                _strSQL = _strSQL & " AND dBloodPressureSittingMin >= " & _BPSittingMin & ""
            '                                _RunVitals = True
            '                            ElseIf _BPSittingMax > 0 AndAlso _BPSittingMin = 0 Then
            '                                _strSQL = _strSQL & " AND dBloodPressureSittingMax <= " & _BPSittingMax & ""
            '                                _RunVitals = True
            '                            End If
            '                        End If
            '                        '>>>BP Sitting Min & Max <<<
            '                        If _BPStandingMin > 0 AndAlso _BPStandingMax > 0 Then
            '                            _strSQL = _strSQL & " AND dBloodPressureStandingMin >= " & _BPStandingMin & " AND dBloodPressureStandingMax <= " & _BPStandingMax & ""
            '                            _RunVitals = True
            '                        Else
            '                            If _BPStandingMin > 0 AndAlso _BPStandingMax = 0 Then
            '                                _strSQL = _strSQL & " AND dBloodPressureStandingMin >= " & _BPStandingMin & ""
            '                                _RunVitals = True
            '                            ElseIf _BPStandingMax > 0 AndAlso _BPStandingMin = 0 Then
            '                                _strSQL = _strSQL & " AND dBloodPressureStandingMax <= " & _BPStandingMax & ""
            '                                _RunVitals = True
            '                            End If
            '                        End If
            '                        '>>>BP Sitting Min & Max <<<
            '                        If _TempMin > 0 AndAlso _TempMax > 0 Then
            '                            _strSQL = _strSQL & " AND dTemperature >= " & _TempMin & " AND dTemperature <= " & _TempMax & ""
            '                            _RunVitals = True
            '                        Else
            '                            If _TempMin > 0 AndAlso _TempMax = 0 Then
            '                                _strSQL = _strSQL & " AND dTemperature >= " & _TempMin & ""
            '                                _RunVitals = True
            '                            ElseIf _TempMax > 0 AndAlso _TempMin = 0 Then
            '                                _strSQL = _strSQL & " AND dTemperature <= " & _TempMax & ""
            '                                _RunVitals = True
            '                            End If
            '                        End If

            '                        '_HeightFtMin = 0 - Vital
            '                        '_HeightFtMax = 0 - Vital
            '                        '_HeightInchMin = 0 - Vital
            '                        '_HeightInchMax = 0 - Vital

            '                        If _RunVitals = True Then
            '                            _TempPatientID = New Collection
            '                            If Not _strSQL.Trim = "" Then
            '                                oDataReader = oDB.ReadQueryRecords(_strSQL)
            '                                If Not oDataReader Is Nothing Then
            '                                    If oDataReader.HasRows = True Then
            '                                        While oDataReader.Read
            '                                            If Not IsDBNull(oDataReader.Item("nPatientID")) Then
            '                                                _TempPatientID.Add(oDataReader.Item("nPatientID"))
            '                                            End If
            '                                        End While
            '                                    End If
            '                                    oDataReader.Close()
            '                                End If
            '                            End If
            '                        End If

            '                        If _HaveVital = True Then
            '                            If _TempPatientID.Count > 1 Then
            '                                _PrimaryPatientID = New Collection
            '                                For i As Int16 = 1 To _TempPatientID.Count
            '                                    _PrimaryPatientID.Add(_TempPatientID(i))
            '                                Next
            '                                _PrimaryINPatientID = ""
            '                                For i As Int16 = 1 To _PrimaryPatientID.Count
            '                                    If i > 1 Then
            '                                        _PrimaryINPatientID = _PrimaryINPatientID & "," & _PrimaryPatientID(i)
            '                                    Else
            '                                        _PrimaryINPatientID = _PrimaryPatientID(i)
            '                                    End If
            '                                Next
            '                                _TempPatientID = Nothing
            '                            Else

            '                                For i As Int16 = _PrimaryPatientID.Count To 1 Step -1
            '                                    _PrimaryPatientID.Remove(i)
            '                                Next
            '                                GoTo FinishFindingProcess
            '                            End If
            '                        End If

            '                        '//<<<<<<< History >>>>>>>
            '                        If _HaveVitalHeight = True Then
            '                            _TempPatientID = New Collection

            '                            _strSQL = "Select distinct nPatientID,sHeight from vitals v where dtvitaldate in " _
            '                            & " (select max(dtvitaldate) from vitals v1 where npatientid=v.npatientid group by nPatientID) " _
            '                            & " AND nPatientid in (" & _PrimaryINPatientID & ")"
            '                            oDataReader = oDB.ReadQueryRecords(_strSQL)
            '                            If Not oDataReader Is Nothing Then
            '                                If oDataReader.HasRows = True Then
            '                                    While oDataReader.Read
            '                                        If Not IsDBNull(oDataReader.Item("nPatientID")) Then
            '                                            Dim _strPatHt() As String
            '                                            Dim _PatHtFt As Double = 0
            '                                            Dim _PatHtInch As Double = 0

            '                                            If Not IsDBNull(oDataReader.Item("sHeight")) Then
            '                                                _strPatHt = GetFtInch(oDataReader.Item("sHeight") & "")
            '                                                If Not _strPatHt Is Nothing Then
            '                                                    If Not _strPatHt(0) Is Nothing Then _PatHtFt = Val(_strPatHt(0))
            '                                                    If Not _strPatHt(1) Is Nothing Then _PatHtInch = Val(_strPatHt(1))
            '                                                End If
            '                                            End If

            '                                            _PatHtFt = FtToMtr(_PatHtFt, _PatHtInch)

            '                                            If _HeightFtMin > 0 Or _HeightFtMax > 0 Then
            '                                                If _HeightFtMin > 0 And _HeightFtMax > 0 Then
            '                                                    If _PatHtFt >= _HeightFtMin And _PatHtFt <= _HeightFtMax Then
            '                                                        _TempPatientID.Add(oDataReader.Item("nPatientID"))
            '                                                    End If
            '                                                ElseIf _HeightFtMin > 0 And _HeightFtMax = 0 Then
            '                                                    If _PatHtFt >= _HeightFtMin Then
            '                                                        _TempPatientID.Add(oDataReader.Item("nPatientID"))
            '                                                    End If
            '                                                ElseIf _HeightFtMax > 0 And _HeightFtMin = 0 Then
            '                                                    If _PatHtFt <= _HeightFtMax Then
            '                                                        _TempPatientID.Add(oDataReader.Item("nPatientID"))
            '                                                    End If
            '                                                End If
            '                                            End If


            '                                        End If
            '                                    End While
            '                                End If
            '                                oDataReader.Close()
            '                            End If

            '                            '// Work with Height match record
            '                            If _TempPatientID.Count > 0 Then
            '                                _PrimaryPatientID = New Collection
            '                                For i As Int16 = 1 To _TempPatientID.Count
            '                                    _PrimaryPatientID.Add(_TempPatientID(i))
            '                                Next
            '                                _PrimaryINPatientID = ""
            '                                For i As Int16 = 1 To _PrimaryPatientID.Count
            '                                    If i > 1 Then
            '                                        _PrimaryINPatientID = _PrimaryINPatientID & "," & _PrimaryPatientID(i)
            '                                    Else
            '                                        _PrimaryINPatientID = _PrimaryPatientID(i)
            '                                    End If
            '                                Next
            '                            Else

            '                                For i As Int16 = _PrimaryPatientID.Count To 1 Step -1
            '                                    _PrimaryPatientID.Remove(i)
            '                                Next
            '                                GoTo FinishFindingProcess
            '                            End If

            '                            _TempPatientID = Nothing
            '                        End If
            '                        '//<<<<<<< History >>>>>>>

            '                    End If
            '                    ':::VITALS:::

            '                    '*********************>>>--- QUERY BUILDER AS PER CONDTION ---<<<*************************

            '                    '*********************>>>--- HISTORY, DRUGS, CPT, ICD9 Start ---<<<*****************************
            '                    If _PrimaryINPatientID.Trim <> "" Then

            '                        '::: HISTORY :::
            '                        _strSQL = ""
            '                        If _Histories.Count > 0 Then
            '                            '_strSQL = "SELECT DISTINCT History.nPatientID " _
            '                            '& " FROM DM_CriteriaHistory_DTL INNER JOIN History_MST ON DM_CriteriaHistory_DTL.dm_Chdtl_HistoryItemId = History_MST.nHistoryID INNER JOIN " _
            '                            '& " History ON History_MST.sDescription = History.sHistoryItem " _
            '                            '& " WHERE (DM_CriteriaHistory_DTL.dm_Chdtl_Id = " & CriteriaID & ") AND (History.nPatientID IN (" & _PrimaryINPatientID & "))"

            '                            _strSQL = "SELECT DISTINCT History.nPatientID FROM History INNER JOIN DM_CriteriaHistory_DTL ON History.sHistoryItem " _
            '                                    & " = DM_CriteriaHistory_DTL.dm_Chdtl_HistoryItem " _
            '                                    & " WHERE (DM_CriteriaHistory_DTL.dm_Chdtl_Id = " & CriteriaID & ") AND (History.nPatientID IN (" & _PrimaryINPatientID & "))"
            '                            _TempPatientID = New Collection
            '                            If Not _strSQL.Trim = "" Then
            '                                oDataReader = oDB.ReadQueryRecords(_strSQL)
            '                                If Not oDataReader Is Nothing Then
            '                                    If oDataReader.HasRows = True Then
            '                                        While oDataReader.Read
            '                                            If Not IsDBNull(oDataReader.Item("nPatientID")) Then
            '                                                _TempPatientID.Add(oDataReader.Item("nPatientID"))
            '                                            End If
            '                                        End While
            '                                    End If
            '                                    oDataReader.Close()
            '                                End If
            '                            End If
            '                            If _TempPatientID.Count > 0 Then
            '                                _PrimaryPatientID = New Collection
            '                                For i As Int16 = 1 To _TempPatientID.Count
            '                                    _PrimaryPatientID.Add(_TempPatientID(i))
            '                                Next

            '                                _PrimaryINPatientID = ""
            '                                For i As Int16 = 1 To _PrimaryPatientID.Count
            '                                    If i > 1 Then
            '                                        _PrimaryINPatientID = _PrimaryINPatientID & "," & _PrimaryPatientID(i)
            '                                    Else
            '                                        _PrimaryINPatientID = _PrimaryPatientID(i)
            '                                    End If
            '                                Next
            '                                _TempPatientID = Nothing
            '                            Else
            '                                For i As Int16 = _PrimaryPatientID.Count To 1 Step -1
            '                                    _PrimaryPatientID.Remove(i)
            '                                Next
            '                                GoTo FinishFindingProcess
            '                            End If
            '                        End If

            '                        '::: DRUGS :::
            '                        _strSQL = ""
            '                        If _Drugs.Count > 0 Then
            '                            _strSQL = "SELECT DISTINCT Medication.nPatientID " _
            '                            & " FROM DM_CriteriaDrug_DTL INNER JOIN Drugs_MST ON DM_CriteriaDrug_DTL.dm_Drugdtl_DrugID = Drugs_MST.nDrugsID INNER JOIN " _
            '                            & " Medication ON Drugs_MST.sDrugName = Medication.sMedication AND Drugs_MST.sDosage = Medication.sDosage " _
            '                            & " WHERE (DM_CriteriaDrug_DTL.dm_Drugdtl_Id = " & CriteriaID & ") AND (Medication.nPatientID IN (" & _PrimaryINPatientID & "))"

            '                            _TempPatientID = New Collection
            '                            If Not _strSQL.Trim = "" Then
            '                                oDataReader = oDB.ReadQueryRecords(_strSQL)
            '                                If Not oDataReader Is Nothing Then
            '                                    If oDataReader.HasRows = True Then
            '                                        While oDataReader.Read
            '                                            If Not IsDBNull(oDataReader.Item("nPatientID")) Then
            '                                                _TempPatientID.Add(oDataReader.Item("nPatientID"))
            '                                            End If
            '                                        End While
            '                                    End If
            '                                    oDataReader.Close()
            '                                End If
            '                            End If
            '                            If _TempPatientID.Count > 0 Then
            '                                _PrimaryPatientID = New Collection
            '                                For i As Int16 = 1 To _TempPatientID.Count
            '                                    _PrimaryPatientID.Add(_TempPatientID(i))
            '                                Next

            '                                _PrimaryINPatientID = ""
            '                                For i As Int16 = 1 To _PrimaryPatientID.Count
            '                                    If i > 1 Then
            '                                        _PrimaryINPatientID = _PrimaryINPatientID & "," & _PrimaryPatientID(i)
            '                                    Else
            '                                        _PrimaryINPatientID = _PrimaryPatientID(i)
            '                                    End If
            '                                Next
            '                                _TempPatientID = Nothing
            '                            Else
            '                                For i As Int16 = _PrimaryPatientID.Count To 1 Step -1
            '                                    _PrimaryPatientID.Remove(i)
            '                                Next
            '                                GoTo FinishFindingProcess
            '                            End If
            '                        End If
            '                        '::: ICD9 :::
            '                        _strSQL = ""
            '                        If _ICD9s.Count > 0 Then
            '                            _strSQL = "SELECT DISTINCT ExamICD9CPT.nPatientID " _
            '                            & " FROM DM_ICD9CPT_DTL INNER JOIN ICD9 ON DM_ICD9CPT_DTL.dm_ICD9CPTdtl_ICID = ICD9.nICD9ID INNER JOIN " _
            '                            & " ExamICD9CPT ON ICD9.sICD9Code = ExamICD9CPT.sICD9Code AND ICD9.sDescription = ExamICD9CPT.sICD9Description " _
            '                            & " WHERE (DM_ICD9CPT_DTL.dm_ICD9CPTdtl_Type = 2) AND (DM_ICD9CPT_DTL.dm_ICD9CPTdtl_Id = " & CriteriaID & ") " _
            '                            & " AND (ExamICD9CPT.nPatientID IN (" & _PrimaryINPatientID & "))"

            '                            _TempPatientID = New Collection
            '                            If Not _strSQL.Trim = "" Then
            '                                oDataReader = oDB.ReadQueryRecords(_strSQL)
            '                                If Not oDataReader Is Nothing Then
            '                                    If oDataReader.HasRows = True Then
            '                                        While oDataReader.Read
            '                                            If Not IsDBNull(oDataReader.Item("nPatientID")) Then
            '                                                _TempPatientID.Add(oDataReader.Item("nPatientID"))
            '                                            End If
            '                                        End While
            '                                    End If
            '                                    oDataReader.Close()
            '                                End If
            '                            End If
            '                            If _TempPatientID.Count > 0 Then
            '                                _PrimaryPatientID = New Collection
            '                                For i As Int16 = 1 To _TempPatientID.Count
            '                                    _PrimaryPatientID.Add(_TempPatientID(i))
            '                                Next

            '                                _PrimaryINPatientID = ""
            '                                For i As Int16 = 1 To _PrimaryPatientID.Count
            '                                    If i > 1 Then
            '                                        _PrimaryINPatientID = _PrimaryINPatientID & "," & _PrimaryPatientID(i)
            '                                    Else
            '                                        _PrimaryINPatientID = _PrimaryPatientID(i)
            '                                    End If
            '                                Next
            '                                _TempPatientID = Nothing
            '                            Else
            '                                For i As Int16 = _PrimaryPatientID.Count To 1 Step -1
            '                                    _PrimaryPatientID.Remove(i)
            '                                Next
            '                                GoTo FinishFindingProcess
            '                            End If
            '                        End If
            '                        '::: CPT :::
            '                        _strSQL = ""
            '                        If _CPTs.Count > 0 Then
            '                            _strSQL = "SELECT DISTINCT Treatment.nPatientID " _
            '                            & " FROM DM_ICD9CPT_DTL INNER JOIN CPT_MST ON DM_ICD9CPT_DTL.dm_ICD9CPTdtl_ICID = CPT_MST.nCPTID INNER JOIN " _
            '                            & " Treatment ON CPT_MST.sCPTCode = Treatment.sCPTCode AND CPT_MST.sDescription = Treatment.sCPTDescription " _
            '                            & " WHERE (DM_ICD9CPT_DTL.dm_ICD9CPTdtl_Type = 1) AND (DM_ICD9CPT_DTL.dm_ICD9CPTdtl_Id = " & CriteriaID & ") AND (Treatment.nPatientID IN " _
            '                            & " (" & _PrimaryINPatientID & "))"

            '                            _TempPatientID = New Collection
            '                            If Not _strSQL.Trim = "" Then
            '                                oDataReader = oDB.ReadQueryRecords(_strSQL)
            '                                If Not oDataReader Is Nothing Then
            '                                    If oDataReader.HasRows = True Then
            '                                        While oDataReader.Read
            '                                            If Not IsDBNull(oDataReader.Item("nPatientID")) Then
            '                                                _TempPatientID.Add(oDataReader.Item("nPatientID"))
            '                                            End If
            '                                        End While
            '                                    End If
            '                                    oDataReader.Close()
            '                                End If
            '                            End If
            '                            If _TempPatientID.Count > 0 Then
            '                                _PrimaryPatientID = New Collection
            '                                For i As Int16 = 1 To _TempPatientID.Count
            '                                    _PrimaryPatientID.Add(_TempPatientID(i))
            '                                Next

            '                                _PrimaryINPatientID = ""
            '                                For i As Int16 = 1 To _PrimaryPatientID.Count
            '                                    If i > 1 Then
            '                                        _PrimaryINPatientID = _PrimaryINPatientID & "," & _PrimaryPatientID(i)
            '                                    Else
            '                                        _PrimaryINPatientID = _PrimaryPatientID(i)
            '                                    End If
            '                                Next
            '                                _TempPatientID = Nothing
            '                            Else
            '                                For i As Int16 = _PrimaryPatientID.Count To 1 Step -1
            '                                    _PrimaryPatientID.Remove(i)
            '                                Next
            '                                GoTo FinishFindingProcess
            '                            End If
            '                        End If

            '                        '::: LABS :::
            '                        _strSQL = ""
            '                        If _Labs.Count > 0 Then
            '                            _strSQL = "SELECT DISTINCT LM_Orders.lm_Patient_ID " _
            '                            & " FROM DM_Labs_DTL INNER JOIN LM_Orders ON DM_Labs_DTL.dm_Labsdtl_TestID = LM_Orders.lm_test_ID " _
            '                            & " WHERE (DM_Labs_DTL.dm_Labsdtl_Id = " & CriteriaID & ") AND (LM_Orders.lm_NumericResult BETWEEN " _
            '                            & " DM_Labs_DTL.dm_Labsdtl_NumericResultMin AND DM_Labs_DTL.dm_Labsdtl_NumericResultMax) AND (LM_Orders.lm_Patient_ID IN " _
            '                            & " (" & _PrimaryINPatientID & "))"

            '                            _TempPatientID = New Collection
            '                            If Not _strSQL.Trim = "" Then
            '                                oDataReader = oDB.ReadQueryRecords(_strSQL)
            '                                If Not oDataReader Is Nothing Then
            '                                    If oDataReader.HasRows = True Then
            '                                        While oDataReader.Read
            '                                            If Not IsDBNull(oDataReader.Item("lm_Patient_ID")) Then
            '                                                _TempPatientID.Add(oDataReader.Item("lm_Patient_ID"))
            '                                            End If
            '                                        End While
            '                                    End If
            '                                    oDataReader.Close()
            '                                End If
            '                            End If
            '                            If _TempPatientID.Count > 0 Then
            '                                _PrimaryPatientID = New Collection
            '                                For i As Int16 = 1 To _TempPatientID.Count
            '                                    _PrimaryPatientID.Add(_TempPatientID(i))
            '                                Next

            '                                _PrimaryINPatientID = ""
            '                                For i As Int16 = 1 To _PrimaryPatientID.Count
            '                                    If i > 1 Then
            '                                        _PrimaryINPatientID = _PrimaryINPatientID & "," & _PrimaryPatientID(i)
            '                                    Else
            '                                        _PrimaryINPatientID = _PrimaryPatientID(i)
            '                                    End If
            '                                Next
            '                                _TempPatientID = Nothing
            '                            Else
            '                                For i As Int16 = _PrimaryPatientID.Count To 1 Step -1
            '                                    _PrimaryPatientID.Remove(i)
            '                                Next
            '                                GoTo FinishFindingProcess
            '                            End If
            '                        End If


            '                        ' Mahesh 20070804
            '                        '::: LABS :::
            '                        _strSQL = ""
            '                        If _LabModule.Count > 0 Then
            '                            _strSQL = "SELECT Lab_Order_MST.labom_PatientID, DM_LabModule_DTL.dm_labdtl_TestID AS TestID, DM_LabModule_DTL.dm_labdtl_ResultID AS ResultID, " _
            '                            & " DM_LabModule_DTL.dm_labdtl_Operator AS CondOperator, DM_LabModule_DTL.dm_labdtl_ResultValue1 AS CondValue1, " _
            '                            & " DM_LabModule_DTL.dm_labdtl_ResultValue2 AS CondValue2, Lab_Order_Test_ResultDtl.labotrd_ResultValue AS PatResult " _
            '                            & " FROM Lab_Order_Test_ResultDtl INNER JOIN Lab_Order_MST ON Lab_Order_Test_ResultDtl.labotrd_OrderID = Lab_Order_MST.labom_OrderID INNER JOIN DM_LabModule_DTL ON Lab_Order_Test_ResultDtl.labotrd_TestID = DM_LabModule_DTL.dm_labdtl_TestID AND Lab_Order_Test_ResultDtl.labotrd_ResultNameID = DM_LabModule_DTL.dm_labdtl_ResultID " _
            '                            & " WHERE (DM_LabModule_DTL.dm_labdtl_ID = " & CriteriaID & ") AND (Lab_Order_MST.labom_PatientID IN (" & _PrimaryINPatientID & ")) " _
            '                            & " AND DM_LabModule_DTL.dm_labdtl_Operator IS NOT NULL AND Lab_Order_Test_ResultDtl.labotrd_ResultValue IS NOT NULL AND Lab_Order_MST.labom_PatientID IS NOT NULL"

            '                            'Check Value is greater than or less than or equal to
            '                            If Not _strSQL.Trim = "" Then
            '                                Dim _SortPatients As New Collection

            '                                oDataReader = oDB.ReadQueryRecords(_strSQL)
            '                                If Not oDataReader Is Nothing Then
            '                                    If oDataReader.HasRows = True Then
            '                                        While oDataReader.Read
            '                                            If Not IsDBNull(oDataReader.Item("CondOperator")) Then
            '                                                Select Case oDataReader.Item("CondOperator")
            '                                                    Case "Greater Than"
            '                                                        If Not IsDBNull(oDataReader.Item("CondValue1")) Then
            '                                                            If CDbl(oDataReader.Item("PatResult") & "") > CDbl(oDataReader.Item("CondValue1") & "") Then
            '                                                                _SortPatients.Add(oDataReader.Item("labom_PatientID"))
            '                                                            End If
            '                                                        End If
            '                                                    Case "Less Than"
            '                                                        If Not IsDBNull(oDataReader.Item("CondValue1")) Then
            '                                                            If CDbl(oDataReader.Item("PatResult") & "") < CDbl(oDataReader.Item("CondValue1") & "") Then
            '                                                                _SortPatients.Add(oDataReader.Item("labom_PatientID"))
            '                                                            End If
            '                                                        End If
            '                                                    Case "Between"
            '                                                        If Not IsDBNull(oDataReader.Item("CondValue1")) Then
            '                                                            If Not IsDBNull(oDataReader.Item("CondValue2")) Then
            '                                                                If CDbl(oDataReader.Item("PatResult") & "") >= CDbl(oDataReader.Item("CondValue1") & "") And CDbl(oDataReader.Item("PatResult") & "") <= CDbl(oDataReader.Item("CondValue2") & "") Then
            '                                                                    _SortPatients.Add(oDataReader.Item("labom_PatientID"))
            '                                                                End If
            '                                                            End If
            '                                                        End If
            '                                                End Select
            '                                            End If
            '                                        End While
            '                                    End If
            '                                    oDataReader.Close()
            '                                End If

            '                                _TempPatientID = New Collection

            '                                If _SortPatients.Count > 0 Then
            '                                    Dim _SortedPatients As New Collection
            '                                    Dim _AddPatient As Boolean = False

            '                                    For i As Int16 = 1 To _SortPatients.Count
            '                                        _AddPatient = False

            '                                        If _SortedPatients.Count > 0 Then
            '                                            For j As Int16 = 1 To _SortedPatients.Count
            '                                                If _SortedPatients.Item(j) = _SortPatients.Item(i) Then
            '                                                    _AddPatient = False
            '                                                    Exit For
            '                                                End If
            '                                            Next
            '                                        Else
            '                                            _AddPatient = True
            '                                        End If

            '                                        If _AddPatient = True Then
            '                                            _SortedPatients.Add(_SortPatients.Item(i))
            '                                        End If
            '                                    Next

            '                                    _TempPatientID = New Collection
            '                                    If _SortedPatients.Count > 0 Then
            '                                        For i As Int16 = 1 To _SortedPatients.Count
            '                                            _TempPatientID.Add(_SortedPatients.Item(i))
            '                                        Next
            '                                    End If
            '                                End If


            '                                If _TempPatientID.Count > 0 Then
            '                                    _PrimaryPatientID = New Collection
            '                                    For i As Int16 = 1 To _TempPatientID.Count
            '                                        _PrimaryPatientID.Add(_TempPatientID(i))
            '                                    Next

            '                                    _PrimaryINPatientID = ""
            '                                    For i As Int16 = 1 To _PrimaryPatientID.Count
            '                                        If i > 1 Then
            '                                            _PrimaryINPatientID = _PrimaryINPatientID & "," & _PrimaryPatientID(i)
            '                                        Else
            '                                            _PrimaryINPatientID = _PrimaryPatientID(i)
            '                                        End If
            '                                    Next
            '                                    _TempPatientID = Nothing
            '                                Else
            '                                    For i As Int16 = _PrimaryPatientID.Count To 1 Step -1
            '                                        _PrimaryPatientID.Remove(i)
            '                                    Next
            '                                    GoTo FinishFindingProcess
            '                                End If

            '                            End If
            '                        End If



            '                    End If


            '                    '*********************>>>--- HISTORY, DRUGS, CPT, ICD9 Finish ---<<<*****************************
            '                    UpdateLog(" END DM - FindGuidelinesForSinglePatientCriteria")
            'FinishFindingProcess:
            '                    If _PrimaryPatientID.Count > 0 Then
            '                        Return True
            '                        ' RaiseEvent FinishCriteria(True, _PrimaryPatientID)
            '                    Else
            '                        Return False
            '                        ' RaiseEvent FinishCriteria(False, _PrimaryPatientID)
            '                    End If

            '                Catch ex As SqlException
            '                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            '                    UpdateLog("clsDiseaseManagement -- FindGuidelinesForSinglePatientCriteria -- " & ex.ToString)
            '                Catch ex As Exception
            '                    UpdateLog("clsDiseaseManagement -- FindGuidelinesForSinglePatientCriteria -- " & ex.ToString)
            '                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            '                Finally
            '                    'oDataReader.Close()
            '                    oDB.Disconnect()
            '                End Try
            '            End Function

            Public Function GetPatientDetails(ByVal PatientID As Int64) As Supporting.PatientDetail

                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim dt As DataTable
                Dim oPatientDetails As New Supporting.PatientDetail
                Dim oOtherDetail As Supporting.OtherDetail
                Dim strQuery As String = ""
                Dim i As Int16
                oDB.Connect(GetConnectionString)
                Try

                    '' Get Patient's Demographic Details
                    'line commented and modified by  dipak20091216 to handle exception
                    'strQuery = "Select  sPatientCode, ISNULL(sFirstName,'') + SPACE(1) + ISNULL(sMiddleName,'') + SPACE(1) + ISNULL(sLastName,'') AS PatientName ,dtDOB, sGender FROM Patient WHERE nPatientID = " & PatientID & ""
                    strQuery = "Select  sPatientCode, ISNULL(sFirstName,'') + SPACE(1) + ISNULL(sMiddleName,'') + SPACE(1) + ISNULL(sLastName,'') AS PatientName ,dtDOB, ISNULL(sGender,0) as sGender FROM Patient WHERE nPatientID = " & PatientID & ""
                    dt = oDB.ReadQueryDataTable(strQuery)
                    If IsNothing(dt) = False Then
                        If dt.Rows.Count > 0 Then
                            With dt.Rows(0)
                                oPatientDetails.PatientCode = .Item("sPatientCode")
                                oPatientDetails.PatientName = .Item("PatientName")
                                oPatientDetails.Age = GetPatientAgeinYrs(.Item("dtDOB"))
                                oPatientDetails.AgeInDays = DateDiff(DateInterval.Day, CType(.Item("dtDOB") & "", Date), Date.Now.Date)
                                oPatientDetails.Gender = .Item("sGender")
                            End With
                        End If
                        If dt IsNot Nothing Then
                            dt.Dispose()
                            dt = Nothing
                        End If
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
                        End If
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
                    If dt IsNot Nothing Then
                        dt.Dispose()
                        dt = Nothing
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
                    If dt IsNot Nothing Then
                        dt.Dispose()
                        dt = Nothing
                    End If
                    If nVisitID <> 0 Then
                        'fill the Patient's Drug information using the Medication table
                        strQuery = "SELECT distinct LTRIM(RTRIM(ISNULL(Medication.sMedication,''))) AS sMedication ,LTRIM(RTRIM(ISNULL(Medication.sDosage,''))) AS sDosage, LTRIM(RTRIM(ISNULL(Medication.sRoute,''))) AS sRoute , dtMedicationDate,ISNULL(sDrugForm,'') as sDrugForm FROM Medication WHERE Medication.nVisitID = " & nVisitID & " AND Medication.nPatientID = " & PatientID & ""

                        dt = oDB.ReadQueryDataTable(strQuery)

                        If IsNothing(dt) = False Then
                            For i = 0 To dt.Rows.Count - 1
                                oOtherDetail = New Supporting.OtherDetail
                                With dt.Rows(i)
                                    oOtherDetail.ItemID = 0
                                    oOtherDetail.ItemDate = .Item("dtMedicationDate")
                                    oOtherDetail.CategoryName = .Item("sMedication")
                                    'oOtherDetail.ItemName = .Item("sDosage")
                                    oOtherDetail.ItemName = .Item("sDrugForm")
                                    oOtherDetail.DetailType = Supporting.enumDetailType.Medication
                                    oPatientDetails.OtherDetails.Add(oOtherDetail)
                                End With
                            Next
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
                    If dt IsNot Nothing Then
                        dt.Dispose()
                        dt = Nothing
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
                        End If
                    End If


                    '' Get Patient's Latest Order Details 

                    '' Get Latest Orders Date against which the Labs Is Entered
                    If dt IsNot Nothing Then
                        dt.Dispose()
                        dt = Nothing
                    End If
                    strQuery = "SELECT TOP 1 LM_Orders.lm_Visit_ID, LM_Orders.lm_OrderDate FROM   LM_Orders WHERE lm_Patient_ID = " & PatientID & " ORDER BY lm_OrderDate DESC "
                    Dim dtOrders As DataTable
                    dtOrders = oDB.ReadQueryDataTable(strQuery)
                    Dim VisitID As Int64
                    Dim OrderDate As Date
                    If IsNothing(dtOrders) = False Then
                        If dtOrders.Rows.Count > 0 Then
                            VisitID = Convert.ToInt64(dtOrders.Rows(0)("lm_Visit_ID"))
                            OrderDate = Convert.ToDateTime(dtOrders.Rows(0)("lm_OrderDate"))

                            '' Get Orders For the latest Order DateTime
                            'strQuery = "SELECT     ISNULL(LM_Category.lm_category_Description,'') AS lm_category_Description, ISNULL(LM_Test.lm_test_Name,lm_test_Name ) AS lm_test_Name, LM_Orders.lm_Visit_ID, LM_Orders.lm_Patient_ID, LM_Orders.lm_OrderDate " _
                            '        & " FROM         LM_Orders INNER JOIN " _
                            '        & " LM_Test ON LM_Orders.lm_test_ID = LM_Test.lm_test_ID INNER JOIN " _
                            '        & " LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID " _
                            '        & " WHERE lm_Visit_ID = " & VisitID & " AND lm_OrderDate = '" & OrderDate & "'"

                            strQuery = " SELECT ISNULL(lm_sCategoryName,'') AS lm_sCategoryName, ISNULL(lm_sTestName,'') AS lm_sTestName, lm_OrderDate, " _
                            & " ISNULL(lm_sGroupName,'') AS lm_sGroupName FROM LM_Orders WHERE lm_Visit_ID = " & VisitID & " AND lm_OrderDate = '" & OrderDate & "'"

                            dt = oDB.ReadQueryDataTable(strQuery)

                            If IsNothing(dt) = False Then
                                For i = 0 To dt.Rows.Count - 1
                                    oOtherDetail = New Supporting.OtherDetail
                                    With dt.Rows(i)
                                        oOtherDetail.ItemID = VisitID
                                        oOtherDetail.ItemDate = .Item("lm_OrderDate")
                                        oOtherDetail.CategoryName = .Item("lm_sCategoryName")
                                        oOtherDetail.OperatorName = .Item("lm_sGroupName")
                                        oOtherDetail.ItemName = .Item("lm_sTestName")
                                        oOtherDetail.DetailType = Supporting.enumDetailType.Order
                                        oPatientDetails.OtherDetails.Add(oOtherDetail)
                                    End With
                                Next
                            End If
                        End If
                    End If
                    If dtOrders IsNot Nothing Then
                        dtOrders.Dispose()
                        dtOrders = Nothing
                    End If
                    

                    '' ICD9 ''
                    strQuery = "SELECT DISTINCT sICD9Code, LTRIM(RTRIM(sICD9Description)) AS sICD9Description FROM ExamICD9CPT WHERE nPatientID = " & PatientID & ""
                    Dim dtICD9 As DataTable
                    dtICD9 = oDB.ReadQueryDataTable(strQuery)
                    If dtICD9 IsNot Nothing Then
                        For iICD9 As Integer = 0 To dtICD9.Rows.Count - 1
                            oOtherDetail = New Supporting.OtherDetail
                            oOtherDetail.CategoryName = dtICD9.Rows(iICD9)("sICD9Code")
                            oOtherDetail.ItemName = dtICD9.Rows(iICD9)("sICD9Description")
                            oOtherDetail.DetailType = Supporting.enumDetailType.ICD9
                            oPatientDetails.OtherDetails.Add(oOtherDetail)
                        Next
                        dtICD9.Dispose()
                        dtICD9 = Nothing
                    End If


                    '' CPT ''
                    strQuery = "SELECT DISTINCT sCPTcode, LTRIM(RTRIM(sCPTDescription)) AS sCPTDescription FROM ExamICD9CPT WHERE nPatientID = " & PatientID & ""
                    Dim dtCPT As DataTable
                    dtCPT = oDB.ReadQueryDataTable(strQuery)
                    If dtCPT IsNot Nothing Then
                        For iCPT As Integer = 0 To dtCPT.Rows.Count - 1
                            oOtherDetail = New Supporting.OtherDetail
                            oOtherDetail.CategoryName = dtCPT.Rows(iCPT)("sCPTcode")
                            oOtherDetail.ItemName = dtCPT.Rows(iCPT)("sCPTDescription")
                            oOtherDetail.DetailType = Supporting.enumDetailType.CPT
                            oPatientDetails.OtherDetails.Add(oOtherDetail)
                        Next

                        dtCPT.Dispose()
                        dtCPT = Nothing
                    End If

                    ''Problemlist chetan added 
                    strQuery = "SELECT ISNULL(sCheifComplaint,'') AS sCheifComplaint FROM PROBLEMLIST WHERE nPatientID = " & PatientID & ""
                    Dim dtProbList As DataTable = Nothing
                    dtProbList = oDB.ReadQueryDataTable(strQuery)
                    If dtProbList IsNot Nothing Then
                        If Not IsNothing(dtProbList) Then
                            For iProbList As Integer = 0 To dtProbList.Rows.Count - 1
                                oOtherDetail = New Supporting.OtherDetail
                                oOtherDetail.ItemName = dtProbList.Rows(iProbList)("sCheifComplaint")
                                oOtherDetail.CategoryName = "ProblemList"
                                oOtherDetail.DetailType = Supporting.enumDetailType.Problemlist
                                oPatientDetails.OtherDetails.Add(oOtherDetail)
                            Next
                        End If

                        dtProbList.Dispose()
                        dtProbList = Nothing
                    End If

                    If dt IsNot Nothing Then
                        dt.Dispose()
                        dt = Nothing
                    End If

                Catch ex As Exception
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    oDB.Disconnect()
                    oDB.Dispose()
                    oDB = Nothing
                End Try

                Return oPatientDetails

            End Function

            Public Function FindGuidelinesForSinglePatientCriteria(ByVal CriteriaID As Long, ByVal PatientID As Long) As Boolean
                Dim oCriteria As Supporting.Criteria = Nothing
                Dim oPatientDetail As Supporting.PatientDetail = Nothing
                Try

                    'Dim _CriteriaName As String = GetCriteriaName(CriteriaID)
                    oCriteria = GetCriteria(CriteriaID, 0)

                    If IsNothing(oCriteria) = True Then
                        Return False
                        Exit Function
                    End If

                    oPatientDetail = GetPatientDetails(PatientID)

                    ''AGE
                    'If Not (oPatientDetail.Age > oCriteria.AgeMinimum And oPatientDetail.Age < oCriteria.AgeMaximum) Then
                    '    Return False
                    'End If
                    'Resolve Bug ID : 43905
                    If Not (oPatientDetail.AgeInDays >= oCriteria.AgeInDaysMinimum AndAlso oPatientDetail.AgeInDays <= oCriteria.AgeInDaysMaximum) Then
                        Return False
                    End If

                    ''GENDER
                    If Not (oPatientDetail.Gender = oCriteria.Gender OrElse oCriteria.Gender = "All") Then
                        Return False
                    End If


                    ''HEIGHT
                    Dim CriteriaHeightMax As Decimal = FtToMtr(oCriteria.HeightMaximum)
                    Dim CriteriaHeightMin As Decimal = FtToMtr(oCriteria.HeightMinimum)

                    If CriteriaHeightMax > 0 AndAlso CriteriaHeightMin > 0 AndAlso oPatientDetail.Height <> "" Then
                        Dim PatientHeight As Decimal = FtToMtr(oPatientDetail.Height)
                        If Not (PatientHeight > CriteriaHeightMin AndAlso PatientHeight < CriteriaHeightMax) Then
                            Return False
                        End If
                    End If

                    ''WEIGHT
                    If oCriteria.WeightMinimum > 0 AndAlso oCriteria.WeightMaximum > 0 Then
                        If Not (oPatientDetail.WeightInlbs > oCriteria.WeightMinimum AndAlso oPatientDetail.WeightInlbs < oCriteria.WeightMaximum) Then
                            Return False
                        End If
                    End If

                    ''PULSE
                    If oCriteria.PulseMinimum > 0 AndAlso oCriteria.PulseMaximum > 0 Then
                        If Not (oPatientDetail.Pulse > oCriteria.PulseMinimum AndAlso oPatientDetail.Pulse < oCriteria.PulseMaximum) Then
                            Return False
                        End If
                    End If

                    ''PULSE_OX
                    If oCriteria.PulseOXMinimum > 0 AndAlso oCriteria.PulseOXMaximum > 0 Then
                        If Not (oPatientDetail.PulseOX > oCriteria.PulseOXMinimum AndAlso oPatientDetail.PulseOX < oCriteria.PulseOXMaximum) Then
                            Return False
                        End If
                    End If

                    ''BP SITTING MAX
                    If oCriteria.BPSittingMaximum > 0 Then
                        If Not (oPatientDetail.BPSittingMaximum = oCriteria.BPSittingMaximum) Then
                            Return False
                        End If
                    End If

                    ''BP SITTING MIN
                    If oCriteria.BPSittingMinimum > 0 Then
                        If Not (oPatientDetail.BPSittingMinimum = oCriteria.BPSittingMinimum) Then
                            Return False
                        End If
                    End If

                    ''BP STANDIN MAX
                    If oCriteria.BPStandingMaximum > 0 Then
                        If Not (oPatientDetail.BPStandingMaximum = oCriteria.BPStandingMaximum) Then
                            Return False
                        End If
                    End If

                    ''BP STANDIN MIN
                    If oCriteria.BPStandingMinimum > 0 Then
                        If Not (oPatientDetail.BPStandingMinimum = oCriteria.BPStandingMinimum) Then
                            Return False
                        End If
                    End If

                    ''BMI
                    If oCriteria.BMIMinimum > 0 AndAlso oCriteria.BMIMaximum > 0 Then
                        If Not (oPatientDetail.BMI > oCriteria.BMIMinimum AndAlso oPatientDetail.BMI < oCriteria.BMIMaximum) Then
                            Return False
                        End If
                    End If

                    '' TEMPERATURE ''
                    If oCriteria.TempratureMinumum > 0 AndAlso oCriteria.TempratureMaximum > 0 Then
                        If Not (oPatientDetail.TempratureInF > oCriteria.TempratureMinumum AndAlso oPatientDetail.TempratureInF < oCriteria.TempratureMaximum) Then
                            Return False
                        End If
                    End If


                    ''OTHER DETAILS
                    Dim _MatchCounter As Integer
                    For iPatDetail As Integer = 1 To oPatientDetail.OtherDetails.Count
                        For iCriteria As Integer = 1 To oCriteria.OtherDetails.Count
                            If IsOtherDetailSame(oPatientDetail.OtherDetails(iPatDetail), oCriteria.OtherDetails(iCriteria)) Then
                                _MatchCounter = _MatchCounter + 1
                                Exit For '' SUDHIR 20091223 '' LOGIC AS PER 2.7.3 '' ALL CRITERIA SHOULD MATCH FOR ALERT ''

                                '' Any of the Criteria Matches then Return TRUE
                                ''Return True '' 20090812 -- Logic Changed -
                            End If
                        Next
                    Next

                    '' SUDHIR 20091223 '' LOGIC AS PER 2.7.3 '' ALL CRITERIA SHOULD MATCH FOR ALERT ''
                    '' If All Criterias of Patient & DM are Matching then Return TRUE
                    If Not _MatchCounter = oCriteria.OtherDetails.Count Then
                        Return False
                    End If

                    '' ALL CRITERIA SATISFIED ''
                    Return True
                Catch ex As Exception
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    If oPatientDetail IsNot Nothing Then
                        oPatientDetail.Dispose()
                        oPatientDetail = Nothing
                    End If
                    If oCriteria IsNot Nothing Then
                        oCriteria.Dispose()
                        oCriteria = Nothing
                    End If
                End Try

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
                If CriteriaDetail.OperatorName = PatientDetail.OperatorName AndAlso CriteriaDetail.DetailType = Supporting.enumDetailType.Order Then '' ONLY FOR ORDER ''
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

            Private Function GetIDsAsString(ByVal COL As Collection) As String
                Dim strCol As String = ""
                For i As Integer = 1 To COL.Count
                    If strCol = "" Then
                        strCol = COL(i)
                    Else
                        strCol = strCol & "," & COL(i)
                    End If
                Next
                Return strCol
            End Function

            Private Function getCriteriaIDColection(ByVal strSQL As String) As Collection
                Dim oDB As New gloDataBase.gloDataBase
                Dim oDataReader As SqlDataReader
                Dim COLECTION As New Collection
                Try
                    oDB.Connect(GetConnectionString)
                    oDataReader = oDB.ReadQueryRecords(strSQL)
                    If Not oDataReader Is Nothing Then
                        If oDataReader.HasRows = True Then
                            While oDataReader.Read
                                If Not IsDBNull(oDataReader.Item("dm_mst_Id")) Then
                                    COLECTION.Add(oDataReader.Item("dm_mst_Id"))
                                End If
                            End While
                        End If
                    End If
                    oDataReader.Close()
                    oDataReader = Nothing
                    oDB.Disconnect()
                    oDB.Dispose()
                    oDB = Nothing

                    Return COLECTION

                Catch ex As SqlException
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    UpdateLog("clsDiseaseManagement -- getCriteriaIDColection -- " & ex.ToString)
                    Return Nothing
                Catch ex As Exception
                    UpdateLog("clsDiseaseManagement -- getCriteriaIDColection -- " & ex.ToString)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                End Try
            End Function


            Public Function IsExists(ByVal oCriteriaName As String, ByVal blnModify As Boolean, ByVal m_CriteriaId As Int64) As Boolean
                'criteria master name exists
                Dim _strSQL As String = ""
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim oDataReader As SqlClient.SqlDataReader
                Dim _Result As Boolean = False

                Try

                    'connect to the database
                    oDB.Connect(GetConnectionString)
                    'set the query string

                    'Added below condition to check duplicate record when modify the DM-Criteria
                    If blnModify = False Then
                        _strSQL = "SELECT dm_mst_Id,dm_mst_CriteriaName FROM DM_Criteria_MST where dm_mst_patientID = 0 and dm_mst_CriteriaName = '" & oCriteriaName & "'"
                    Else
                        '_strSQL = "SELECT dm_mst_Id,dm_mst_CriteriaName FROM DM_Criteria_MST where dm_mst_CriteriaName = (select case when lower ('" & oCriteriaName & "') = (SELECT lower (dm_mst_CriteriaName) FROM DM_Criteria_MST where dm_mst_Id=" & m_CriteriaId & ") then '' else  '" & oCriteriaName & "' end )"
                        _strSQL = "select dm_mst_Id,dm_mst_CriteriaName from DM_Criteria_MST where dm_mst_patientID = 0 and lower(dm_mst_CriteriaName)= lower ('" & oCriteriaName & "') and  dm_mst_Id <> " & m_CriteriaId
                    End If
                    'execute the query and return a datareader
                    oDataReader = oDB.ReadQueryRecords(_strSQL)

                    'check if there is any data in the datareader
                    If Not oDataReader Is Nothing Then
                        If oDataReader.HasRows = True Then
                            While oDataReader.Read
                                If Not IsDBNull(oDataReader.Item("dm_mst_CriteriaName")) Then
                                    'if the criteria name matches a name in the table then return true
                                    If LCase(oDataReader.Item("dm_mst_CriteriaName")) = LCase(oCriteriaName) Then
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
                    UpdateLog("clsDiseaseManagement -- IsExists -- " & ex.ToString)
                    Return Nothing
                Catch ex As Exception
                    UpdateLog("clsDiseaseManagement -- IsExists -- " & ex.ToString)
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

                Dim CriteriaID As Long = 0            '''''Previously it was declared as integer changed by Anil on 29/10/2007 toresolve bug no-436
                Dim _Result As Boolean = True
                Dim _Count As Long = 0

                Dim Conn As SqlConnection
                Dim cmd As SqlCommand

                Try

                    'connect to the database
                    Conn = New SqlConnection(GetConnectionString())
                    Conn.Open()

                    'extract the criteria id from the table for the given criteria name
                    _strSQL = "SELECT dm_mst_Id FROM DM_Criteria_MST where dm_mst_CriteriaName = '" & oCriteriaName & "'"
                    cmd = New SqlCommand(_strSQL, Conn)

                    CriteriaID = cmd.ExecuteScalar()  'Val(oDB.ExecuteQueryScaler(_strSQL))
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                    Conn.Close()
                    Conn.Dispose()
                    Conn = Nothing

                    _strSQL = "SELECT COUNT(nRuleId) FROM dbo.DM_Patient_ClinicalRecommendations WHERE nRuleId =" & CriteriaID
                    'execute the query and return a datareader
                    oDB.Connect(GetConnectionString)
                    _Count = oDB.ExecuteQueryScaler(_strSQL)

                    ''set the query string
                    '_strSQL = "SELECT COUNT(DM_TransId) FROM DM_Patient where DM_nCriteriaID =" & CriteriaID
                    ''execute the query and return a datareader
                    '_Count = oDB.ExecuteQueryScaler(_strSQL)

                    'check if there is any data in the datareader
                    If _Count > 0 Then
                        _Result = False
                    End If

                    Return _Result
                Catch ex As SqlException
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    UpdateLog("clsDiseaseManagement -- IsDelete -- " & ex.ToString)
                    Return Nothing
                Catch ex As Exception
                    UpdateLog("clsDiseaseManagement -- IsDelete -- " & ex.ToString)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ' Return _Result
                    Return Nothing
                Finally
                   
                    If Not IsNothing(oDB) Then
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                    End If
                    
                End Try
                'Return True
            End Function

            Public Function IsPatientSpecificCriteria(ByVal CriteriaID As Int64) As Boolean
                Dim oDB As New gloDataBase.gloDataBase
                Dim Query As String = "SELECT COUNT(*) FROM DM_Criteria_MST WHERE dm_mst_Id = " & CriteriaID & " AND ISNULL(dm_mst_PatientID,0) <> 0"
                Dim oResult As Object = 0
                Try
                    oDB.Connect(GetConnectionString)
                    oResult = oDB.ExecuteQueryScaler(Query)

                    If CType(oResult, Integer) > 0 Then
                        Return True
                    Else
                        Return False
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                Finally
                    oResult = Nothing
                    If Not IsNothing(oDB) Then
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                    End If

                End Try
            End Function

            Public Function IsInPatientHealthPlan(ByVal CriteriaID As Int64, ByVal PatientID As Int64) As Boolean
                Dim oDB As New gloDataBase.gloDataBase
                Dim Query As String = "SELECT COUNT(*) FROM DM_Patient WHERE DM_nCriteriaID = " & CriteriaID & " AND DM_nPatientID = " & PatientID & ""
                Dim oResult As Object = 0
                Try
                    oDB.Connect(GetConnectionString)
                    oResult = oDB.ExecuteQueryScaler(Query)

                    If CType(oResult, Integer) > 0 Then
                        Return True

                    Else
                        Return False

                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                Finally
                    oResult = Nothing
                    If Not IsNothing(oDB) Then
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                    End If
                End Try
            End Function

            Public Sub New()
                MyBase.new()
            End Sub

            'Protected Overrides Sub Finalize()
            '    MyBase.Finalize()
            'End Sub

            Public Function FindPatientSpecificTriggers(ByVal PatientId As Int64) As DataTable
                Dim oDB As New DataBaseLayer


                Dim oResultTable As DataTable
                Try
                    Dim _strSQL As String

                    _strSQL = "SELECT distinct DM_nCriteriaID from DM_Patient where DM_nPatientId = " & PatientId & " AND DM_nCriteriaID IN ( SELECT dm_mst_Id FROM dbo.DM_Criteria_MST WHERE bIsActive=1) "

                    '' _strSQL = "SELECT  * from DM_Patient where DM_nPatientId = " & PatientId
                    oResultTable = oDB.GetDataTable_Query(_strSQL)
                    If Not oResultTable Is Nothing Then
                        Return oResultTable
                    End If
                    Return Nothing
                Catch ex As SqlException
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Catch ex As Exception
                    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    If Not IsNothing(oDB) Then

                        oDB.Dispose()
                        oDB = Nothing
                    End If
                End Try
            End Function

            Public Function FindPatientSpecificDueTriggers(ByVal PatientId As Int64, ByVal CriteriaId As Int64) As DataTable
                Dim oDB As New DataBaseLayer


                Dim oResultTable As DataTable
                Try
                    Dim _strSQL As String

                    _strSQL = "SELECT DM_TransId, DM_dtTransDate, DM_nPatientID, DM_nCriteriaID, DM_nTriggerID, DM_sResult, DM_nPrint, DM_nFax, DM_nType, DM_DueType, DM_DueValue, " & _
                     " DM_bIsOverride, DM_sReason, DM_sNotes, DM_bIsGiven, DM_bIsRecurring, DM_TriggerName, DM_CriteriaName, DM_TriggerDtlInfo, sDrugForm, sRoute, " & _
                      "sFrequency, sNDCCode, nIsNarcotics, sDuration, sDrugQtyQualifier from DM_Patient where DM_bIsGiven = 'False' and DM_nPatientId = " & PatientId & " and  DM_nCriteriaID = " & CriteriaId & "  "


                    oResultTable = oDB.GetDataTable_Query(_strSQL)
                    If Not oResultTable Is Nothing Then
                        Return oResultTable
                    End If
                    Return Nothing
                Catch ex As SqlException
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Catch ex As Exception
                    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    If Not IsNothing(oDB) Then

                        oDB.Dispose()
                    End If
                End Try
            End Function

            Public Function FindDueTriggerDetails(ByVal TransId As Int64, ByVal bIsRecurring As Boolean) As DataTable
                Dim oDB As New DataBaseLayer


                Dim oResultTable As DataTable
                Try
                    Dim _strSQL As String
                    If bIsRecurring Then
                        '_strSQL = "SELECT * from DM_Patient, DM_Patient_DTL where DM_bIsGiven = 'False' and DM_Patient.DM_TransId = " & TransId
                        _strSQL = "SELECT DM_dtTransDate,DM_DueType, DM_DueValue,DM_sReason,DM_sNotes,DM_bIsRecurring, DM_dtStartDate, DM_dtEndDate, DM_nDurationType, DM_nDurationPeriod  FROM DM_Patient INNER JOIN DM_Patient_DTL ON DM_Patient.DM_TransId = DM_Patient_DTL.DM_TransId where DM_bIsGiven = 'False' and DM_Patient.DM_TransId = " & TransId
                    Else
                        _strSQL = "SELECT DM_dtTransDate, DM_DueType, DM_DueValue, DM_sReason, DM_sNotes,DM_bIsRecurring from DM_Patient where DM_bIsGiven = 'False' and DM_TransId = " & TransId
                    End If


                    oResultTable = oDB.GetDataTable_Query(_strSQL)
                    If Not oResultTable Is Nothing Then
                        Return oResultTable
                    End If
                    Return Nothing
                Catch ex As SqlException
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Catch ex As Exception
                    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    If Not IsNothing(oDB) Then
                        oDB.Dispose()
                    End If
                End Try
            End Function
            Private Function Get_PatientDetails(ByVal _PatientID As Long)
                Dim dtPatient As DataTable = Nothing

                Try
                    dtPatient = GetPatientInfo(_PatientID)
                    If IsNothing(dtPatient) = False Then
                        If dtPatient.Rows.Count > 0 Then
                            strPatientCode = Convert.ToString(dtPatient.Rows(0)("sPatientCode"))
                            strPatientFirstName = Convert.ToString(dtPatient.Rows(0)("sFirstName"))
                            strPatientLastName = Convert.ToString(dtPatient.Rows(0)("sLastName"))
                            strPatientDOB = Convert.ToString(dtPatient.Rows(0)("dtDOB"))
                            strPatientAge = GetAge(Convert.ToDateTime(dtPatient.Rows(0)("dtDOB")))
                            strPatientGender = Convert.ToString(dtPatient.Rows(0)("sGender"))
                            strPatientMaritalStatus = Convert.ToString(dtPatient.Rows(0)("sMaritalStatus"))

                        End If
                    End If
                Catch ex As Exception

                Finally
                    If IsNothing(dtPatient) = False Then
                        dtPatient.Dispose()
                        dtPatient = Nothing
                    End If


                End Try
                Return Nothing
            End Function
            Public Function CheckDueGuidelines(ByVal _TransId As Int64, ByVal IsRecurring As Boolean, ByVal PatientID As Long) As Boolean
                ''Get the Trigger Details based on the Transaction id
                Dim dtTriggerDetails As DataTable = FindDueTriggerDetails(_TransId, IsRecurring)
                Dim _DueType, _DueValue As String
                Dim _DurationType As String
                Dim _DurationPeriod As Integer
                Dim _StartDate As Date
                Dim _EndDate As Date
                Try

                    Call Get_PatientDetails(PatientID)
                    Dim nPatAge As Int32 = GetPatientAgeinYrs(strPatientDOB)
                    If Not dtTriggerDetails Is Nothing Then
                        ''Loop though all the records ideally it should contain only one record
                        For _cnt As Int32 = 0 To dtTriggerDetails.Rows.Count - 1

                            If Not IsDBNull(dtTriggerDetails.Rows(_cnt)("DM_DueType")) Then
                                _DueType = CType(dtTriggerDetails.Rows(_cnt)("DM_DueType"), String)
                                If _DueType = "Age" Then
                                    If Not IsDBNull(dtTriggerDetails.Rows(_cnt)("DM_DueValue")) Then
                                        _DueValue = CType(dtTriggerDetails.Rows(_cnt)("DM_DueValue"), String)
                                        If _DueValue.Contains(">=") Then
                                            _DueValue = _DueValue.Remove(0, 2)
                                            If (nPatAge >= CType(_DueValue.Trim, Int32)) Then
                                                Return True
                                            End If
                                        ElseIf _DueValue.Contains(">") Then
                                            _DueValue = _DueValue.Remove(0, 1)
                                            If (nPatAge > CType(_DueValue.Trim, Int32)) Then
                                                Return True
                                            End If
                                        ElseIf _DueValue.Contains("=") Then
                                            _DueValue = _DueValue.Remove(0, 1)
                                            If (nPatAge = CType(_DueValue.Trim, Int32)) Then
                                                Return True
                                            End If

                                        End If
                                    End If
                                Else
                                    '' SUDHIR - 20090311 - FOR EVERY CHECK IN 
                                    If IsRecurring Then
                                        If Not IsDBNull(dtTriggerDetails.Rows(_cnt)("DM_nDurationType")) Then
                                            _DurationType = CType(dtTriggerDetails.Rows(_cnt)("DM_nDurationType"), String)
                                            _StartDate = CType(dtTriggerDetails.Rows(_cnt)("DM_dtStartDate"), Date)
                                            _EndDate = CType(dtTriggerDetails.Rows(_cnt)("DM_dtEndDate"), Date)
                                            If _DurationType = "On Every Check In" Then
                                                If IsPatientCheckIn(PatientID) = True Then
                                                    If Now.Date >= _StartDate.Date AndAlso Now.Date <= _EndDate.Date Then
                                                        Return True
                                                    End If
                                                End If
                                            Else
                                                '' SUDHIR - 20090313 - FOR RECURRENCE
                                                Dim arrTriggerDates As New ArrayList
                                                Dim _tmpDate As Date
                                                _DurationPeriod = CType(dtTriggerDetails.Rows(_cnt)("DM_nDurationPeriod"), Integer)
                                                Select Case UCase(_DurationType)
                                                    Case UCase("Days")
                                                        _tmpDate = _StartDate ''RECURRECE WILL START FROM STARTING DATE '' STARTING DATE WILL NOT CONSIDER AS TRIGGERING DATE
                                                        While (_tmpDate >= _StartDate AndAlso _tmpDate <= _EndDate AndAlso _DurationPeriod <> 0)  '' FROM DATE - TO DATE VALIDATION
                                                            _tmpDate = _tmpDate.AddDays(_DurationPeriod) '' ADDING INTERVALS
                                                            arrTriggerDates.Add(_tmpDate.ToShortDateString) '' CREATE LIST OF POSSIBLE TRIGGER DATES
                                                        End While
                                                        If arrTriggerDates.Contains(Now.Date.ToShortDateString) Then ''SEARCH TODAYS DATE FOR TRIGGER DATE.. 
                                                            Return True '' RETURN RESULT , SAME LOGIC FOR BELOW CASES
                                                        End If
                                                    Case UCase("Weeks")
                                                        _tmpDate = _StartDate
                                                        While (_tmpDate >= _StartDate AndAlso _tmpDate <= _EndDate AndAlso _DurationPeriod <> 0)
                                                            _tmpDate = _tmpDate.AddDays(_DurationPeriod * 7) '' 7 MULTIPLY FOR FOR WEEKS
                                                            arrTriggerDates.Add(_tmpDate.ToShortDateString)
                                                        End While
                                                        If arrTriggerDates.Contains(Now.Date.ToShortDateString) Then
                                                            Return True
                                                        End If
                                                    Case UCase("Months")
                                                        _tmpDate = _StartDate
                                                        While (_tmpDate >= _StartDate AndAlso _tmpDate <= _EndDate AndAlso _DurationPeriod <> 0)
                                                            _tmpDate = _tmpDate.AddMonths(_DurationPeriod)
                                                            arrTriggerDates.Add(_tmpDate.ToShortDateString)
                                                        End While
                                                        If arrTriggerDates.Contains(Now.Date.ToShortDateString) Then
                                                            Return True
                                                        End If
                                                    Case UCase("Years")
                                                        _tmpDate = _StartDate
                                                        While (_tmpDate >= _StartDate AndAlso _tmpDate <= _EndDate AndAlso _DurationPeriod <> 0)
                                                            _tmpDate = _tmpDate.AddYears(_DurationPeriod)
                                                            arrTriggerDates.Add(_tmpDate.ToShortDateString)
                                                        End While
                                                        If arrTriggerDates.Contains(Now.Date.ToShortDateString) Then
                                                            Return True
                                                        End If
                                                End Select
                                                arrTriggerDates = Nothing
                                                '' RECURRENCE LOGIC END
                                            End If
                                        End If
                                    Else
                                        If Not IsDBNull(dtTriggerDetails.Rows(_cnt)("DM_DueValue")) Then
                                            Dim _DueDate As DateTime
                                            Dim _TransDate As DateTime
                                            If dtTriggerDetails.Rows(_cnt)("DM_DueValue") <> "" Then
                                                _DueDate = CType(dtTriggerDetails.Rows(_cnt)("DM_DueValue"), DateTime)
                                                _TransDate = CType(dtTriggerDetails.Rows(_cnt)("DM_dtTransDate"), DateTime)
                                                If _DueDate.Date = _TransDate.Date AndAlso _DueDate.Date = Now.Date Then
                                                    Return False
                                                End If
                                                Select Case DateTime.Compare(DateTime.Now.Date, _DueDate.Date)
                                                    Case -1
                                                        ''Smaller

                                                    Case 1
                                                        ''Bigger
                                                        Return True

                                                    Case 0
                                                        ''equal
                                                        Return True
                                                End Select
                                            Else
                                                Return True
                                            End If
                                        End If


                                    End If

                                    ''END SUDHIR - ON EVERY CHECK IN

                                End If
                            End If
                        Next
                    End If
                    Return False
                Catch ex As Exception
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                Finally
                    dtTriggerDetails.Dispose()
                    dtTriggerDetails = Nothing
                End Try

            End Function
            Public Function CheckDMReason(ByVal _TransId As Int64, ByVal IsRecurring As Boolean) As Boolean
                Dim dtTriggerDetails As DataTable = FindDueTriggerDetails(_TransId, IsRecurring)
                Try

                
                If Not dtTriggerDetails Is Nothing Then
                    ''Loop though all the records ideally it should contain only one record
                    For _cnt As Int32 = 0 To dtTriggerDetails.Rows.Count - 1
                        Dim blnPastFlag As Boolean = False
                        If Not IsDBNull(dtTriggerDetails.Rows(_cnt)(1)) Then
                            'Dim _TransDate As DateTime = CType(dtTriggerDetails.Rows(_cnt)(1), DateTime)
                            Dim _TransDate As DateTime = CType(dtTriggerDetails.Rows(_cnt)("DM_dtTransDate"), DateTime)
                            Select Case DateTime.Compare(_TransDate.Date, DateTime.Now.Date)
                                Case -1
                                    ''Smaller
                                    Return True
                                Case 1
                                    ''Bigger
                                    blnPastFlag = True

                                Case 0D
                                    ''equal
                                    blnPastFlag = True
                            End Select
                        Else
                            blnPastFlag = True
                        End If
                        If blnPastFlag Then
                            'If Not IsDBNull(dtTriggerDetails.Rows(_cnt)(12)) Then
                            '    If dtTriggerDetails.Rows(_cnt)(12).ToString <> "" Then
                            If Not IsDBNull(dtTriggerDetails.Rows(_cnt)("DM_sReason")) Then
                                If dtTriggerDetails.Rows(_cnt)("DM_sReason").ToString <> "" Then
                                    Return False
                                Else
                                    Return True
                                End If
                            Else
                                Return True
                            End If
                        End If

                    Next
                    End If
                    Return False
                Catch ex As Exception
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                Finally
                    dtTriggerDetails.Dispose()
                    dtTriggerDetails = Nothing
                End Try
            End Function


            Public Function GetPatientAgeinYrs(ByVal PatientDOB As Date) As Double
                Dim nMonths, nPatAge As Double
                nMonths = DateDiff(DateInterval.Month, CType(PatientDOB, Date), Date.Now.Date)
                nPatAge = CDbl(nMonths) / 12.0
                Return Format(nPatAge, "#0.00")
            End Function

            'Public Function GetPatientAgeinYrs(ByVal PatientDOB As Date) As Int32
            '    Dim nMonths, nPatAge As Int32
            '    nMonths = DateDiff(DateInterval.Month, CType(PatientDOB, Date), Date.Now.Date)
            '    nPatAge = nMonths \ 12
            '    Return nPatAge
            'End Function
            Public Function GetSpecifiHealthPlan(ByVal _Criteria As Int64) As DataTable
                Dim oDB As New DataBaseLayer
                Dim oResultTable As DataTable
                Try
                    Dim _strSQL As String

                    _strSQL = "SELECT DISTINCT dm_mst_Id AS CriteriaID, dm_mst_Gender AS Gender, dm_mst_CriteriaName as Name, dm_mst_DisplayMessage as Message FROM DM_Criteria_MST where dm_mst_Id=" & _Criteria

                    oResultTable = oDB.GetDataTable_Query(_strSQL)
                    If Not oResultTable Is Nothing Then
                        If oResultTable.Rows.Count > 0 Then
                            Return oResultTable
                        End If

                    End If
                    Return Nothing
                Catch ex As SqlException
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Catch ex As Exception
                    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    If Not IsNothing(oDB) Then
                        oDB.Dispose()
                    End If
                End Try
            End Function
            Public Function GetAllHealthPlans() As DataTable
                Dim oDB As New DataBaseLayer
                Dim oResultTable As DataTable
                Try
                    Dim _strSQL As String

                    _strSQL = "SELECT DISTINCT dm_mst_Id AS CriteriaID, dm_mst_Gender AS Gender, dm_mst_CriteriaName as Name, dm_mst_DisplayMessage as Message FROM DM_Criteria_MST"

                    oResultTable = oDB.GetDataTable_Query(_strSQL)
                    If Not oResultTable Is Nothing Then
                        Return oResultTable
                    End If
                    Return Nothing
                Catch ex As SqlException
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Catch ex As Exception
                    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    If Not IsNothing(oDB) Then
                        oDB.Dispose()
                    End If
                End Try
            End Function
            Public Function GetDMAlerts(ByVal _PatientId As Int64) As String
                Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())


                Dim _oCriterias As Collection
                Dim strDMAlert As String = ""



                If oDB IsNot Nothing Then
                    If oDB.Connect(False) Then
                        Dim oParamater As New gloDatabaseLayer.DBParameters()
                        Try
                            oParamater.Add("@nPatientID", _PatientId, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                            oParamater.Add("@nClinicID", gloGlobal.gloPMGlobal.ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                            oParamater.Add("@sVersion", gstrVersion, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)

                            'Parameter Added By Sameer on 08 Oct 2013 For Special Alert Rights for Rules
                            oParamater.Add("@nUserID", gnLoginID, ParameterDirection.Input, System.Data.SqlDbType.BigInt)

                            Dim oDataTable As DataTable = Nothing
                            oDB.Retrive("GETDMAlert", oParamater, oDataTable)

                            If oDataTable IsNot Nothing Then
                                If oDataTable.Rows.Count > 0 Then
                                    strDMAlert = oDataTable(0)(0)
                                    If strDMAlert.Length > 0 Then
                                        strDMAlert = strDMAlert
                                    End If
                                End If
                                If oDataTable IsNot Nothing Then
                                    oDataTable.Dispose()
                                    oDataTable = Nothing
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
                    Else
                        If oDB IsNot Nothing Then
                            oDB.Disconnect()
                            oDB.Dispose()
                            oDB = Nothing
                        End If
                    End If
                End If


                Dim oDMSingleProcess As New gloStream.DiseaseManagement.DiseaseManagement
                Dim oPatTriggers As DataTable = Nothing
                Try
                    '  Application.DoEvents()

                    _oCriterias = oDMSingleProcess.FindDMCriteriaOFPatient(_PatientId, gnClinicID)
                    ' _oCriterias = oDMSingleProcess.FindGuidelinesForSinglePatient(gnPatientID)

                    oPatTriggers = oDMSingleProcess.FindPatientSpecificTriggers(_PatientId)
                    If Not oPatTriggers Is Nothing Then
                        If _oCriterias.Count > 0 Then
                            For _cnt As Int32 = 0 To oPatTriggers.Rows.Count - 1
                                Dim oPatientCriterias As DataTable = oDMSingleProcess.FindPatientSpecificDueTriggers(_PatientId, oPatTriggers.Rows(_cnt)("DM_nCriteriaID"))
                                If Not oPatientCriterias Is Nothing Then
                                    ''to remove the guidelines from the Collectiont that are not due based on the due values
                                    Dim nDueValue As Int32 = 0
                                    For _row As Int32 = 0 To oPatientCriterias.Rows.Count - 1
                                        Dim TransId As Int64
                                        Dim bIsRecurring As Boolean = False
                                        TransId = oPatientCriterias.Rows(_row)("DM_TransId")
                                        bIsRecurring = oPatientCriterias.Rows(_row)("DM_bIsRecurring")

                                        If Not oDMSingleProcess.CheckDueGuidelines(TransId, bIsRecurring, _PatientId) Then
                                            nDueValue += 1
                                        End If
                                    Next
                                    If nDueValue = oPatientCriterias.Rows.Count Then
                                        For _myindex As Int32 = _oCriterias.Count To 1 Step -1
                                            If _oCriterias(_myindex) = oPatTriggers.Rows(_cnt)("DM_nCriteriaID") Then
                                                _oCriterias.Remove(_myindex)
                                            End If
                                        Next
                                    End If
                                    oPatientCriterias.Dispose()
                                End If

                            Next
                        Else
                            For _cnt As Int32 = 0 To oPatTriggers.Rows.Count - 1
                                Dim oPatientCriterias As DataTable = oDMSingleProcess.FindPatientSpecificDueTriggers(_PatientId, oPatTriggers.Rows(_cnt)("DM_nCriteriaID"))
                                If Not oPatientCriterias Is Nothing Then
                                    ''to remove the guidelines from the Collectiont that are not due based on the due values
                                    Dim nDueValue As Int32 = 0
                                    For _row As Int32 = 0 To oPatientCriterias.Rows.Count - 1
                                        Dim TransId As Int64
                                        Dim bIsRecurring As Boolean = False
                                        TransId = oPatientCriterias.Rows(_row)("DM_TransId")
                                        bIsRecurring = oPatientCriterias.Rows(_row)("DM_bIsRecurring")

                                        If oDMSingleProcess.CheckDueGuidelines(TransId, bIsRecurring, _PatientId) Then
                                            nDueValue += 1
                                        End If

                                    Next
                                    If nDueValue = oPatientCriterias.Rows.Count AndAlso nDueValue <> 0 Then
                                        _oCriterias.Add(CType(oPatTriggers.Rows(_cnt)("DM_nCriteriaID"), Int64))
                                    End If
                                    oPatientCriterias.Dispose()
                                End If

                            Next
                        End If
                        oPatTriggers.Dispose()
                        oPatTriggers = Nothing
                    End If
                    If IsNothing(_oCriterias) = False Then
                        For i As Integer = 1 To _oCriterias.Count
                            If strDMAlert.Length = 0 Then
                                strDMAlert = oDMSingleProcess.GetCriteriaMessage(_oCriterias(i))
                            Else
                                strDMAlert = strDMAlert & ", " & oDMSingleProcess.GetCriteriaMessage(_oCriterias(i))







                            End If

                        Next
                    End If

                    Return strDMAlert
                Catch ex As Exception
                    Return ""
                Finally
                    _oCriterias = Nothing
                    If Not IsNothing(oDMSingleProcess) Then
                        oDMSingleProcess.Dispose()
                        oDMSingleProcess = Nothing
                    End If
                    If Not IsNothing(oPatTriggers) Then
                        oPatTriggers.Dispose()
                        oPatTriggers = Nothing
                    End If
                 
                End Try
                Return strDMAlert
            End Function

            Public Function GetRecommendationsAlerts(ByVal _PatientId As Int64)
                Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())

                If oDB IsNot Nothing Then
                    If oDB.Connect(False) Then
                        Dim oParamater As New gloDatabaseLayer.DBParameters()
                        Try
                            oParamater.Add("@nPatientID", _PatientId, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                            oParamater.Add("@nClinicID", gloGlobal.gloPMGlobal.ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                            oParamater.Add("@sVersion", gstrVersion, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                            oDB.Execute("GetDMRecommendationsOFPatient", oParamater)
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
                    Else
                        If oDB IsNot Nothing Then
                            oDB.Disconnect()
                            oDB.Dispose()
                            oDB = Nothing
                        End If
                    End If
                End If
                Return Nothing
            End Function

            Public Function RefreshAllPatientRecommendationsAlerts()
                Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())

                If oDB IsNot Nothing Then
                    If oDB.Connect(False) Then
                        Dim oParamater As New gloDatabaseLayer.DBParameters()
                        Try
                            oParamater.Add("@nClinicID", gloGlobal.gloPMGlobal.ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                            oParamater.Add("@sVersion", gstrVersion, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                            oDB.Execute("GetDMRecommendationsOFPatient_Refresh", oParamater)
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
                    Else
                        If oDB IsNot Nothing Then
                            oDB.Disconnect()
                            oDB.Dispose()
                            oDB = Nothing
                        End If
                    End If
                End If
                Return Nothing
            End Function

            'sarika DM Denormalization 20090331
            Public Function GetTemplate(ByVal TemplateID As Int64) As Object
                Dim img As Object
                Dim oDB As New DataBaseLayer
                Dim _strSQL As String = ""

                Try
                    _strSQL = "select sDescription from TemplateGallery_MST where  nTemplateID = " & TemplateID & ""

                    img = oDB.GetDataValue(_strSQL, False)

                    Return img
                Catch ex As Exception
                    Throw ex
                Finally
                    If Not IsNothing(oDB) Then
                        oDB.Dispose()
                        oDB = Nothing
                    End If
                End Try
            End Function
            '----

            Public Function GetTemplateByName(ByVal Templatename As String) As Object
                Dim img As Object
                Dim oDB As New DataBaseLayer
                Dim _strSQL As String = ""

                Try
                    _strSQL = "select sDescription from TemplateGallery_MST where  sTemplateName = '" & Templatename & "'"

                    img = oDB.GetDataValue(_strSQL, False)

                    Return img
                Catch ex As Exception
                    Throw ex
                Finally
                    If Not IsNothing(oDB) Then
                        oDB.Dispose()
                        oDB = Nothing
                    End If
                End Try
            End Function

        End Class


        Namespace Common

            Public Class Criteria
                Implements IDisposable
             
                Private _ErrorMessage As String

                Public Property ErrorMessage() As String
                    Get
                        Return _ErrorMessage
                    End Get
                    Set(ByVal Value As String)
                        _ErrorMessage = Value
                    End Set
                End Property

                

                Public Function Histories(ByVal oCategory As String) As gloStream.DiseaseManagement.Supporting.History
                    Dim ODB As New gloStream.gloDataBase.gloDataBase
                    Dim dtHistory As DataTable = Nothing
                    Dim oHistory As New gloStream.DiseaseManagement.Supporting.History

                    Dim sqlstr As String

                    Try
                        If gblnCodedHistory Then ''IF CODED HISTORY ENABLED
                            Dim objclsPatientHistory As New clsPatientHistory
                            If oCategory.StartsWith("Aller") Then
                                ODB.Connect(GetConnectionString)

                                sqlstr = "SELECT History_MST.nHistoryID, History_MST.sDescription FROM  Category_MST INNER JOIN "
                                sqlstr &= " History_MST ON Category_MST.nCategoryID = History_MST.nCategoryID "
                                sqlstr &= "where Category_MST.sDescription  = '" & oCategory & "' ORDER BY History_MST.sDescription "

                                dtHistory = ODB.ReadQueryDataTable(sqlstr)
                                If Not IsNothing(dtHistory) Then
                                    For i As Integer = 0 To dtHistory.Rows.Count - 1
                                        oHistory.Items.Add(dtHistory.Rows(i)("nHistoryID"), dtHistory.Rows(i)("sDescription"))
                                    Next
                                    dtHistory.Dispose()
                                    dtHistory = Nothing
                                End If
                                Return oHistory
                            Else

                                dtHistory = objclsPatientHistory.GetAllICD9Gallery
                                If Not IsNothing(dtHistory) Then
                                    For i As Integer = 0 To dtHistory.Rows.Count - 1
                                        oHistory.Items.Add(dtHistory.Rows(i)("ICD9ID"), dtHistory.Rows(i)("Description"))
                                    Next
                                    dtHistory.Dispose()
                                    dtHistory = Nothing
                                End If
                                Return oHistory

                            End If
                        objclsPatientHistory.Dispose()
                        objclsPatientHistory = Nothing

                            Else
                        ODB.Connect(GetConnectionString)
                        sqlstr = "SELECT History_MST.nHistoryID, History_MST.sDescription FROM  Category_MST INNER JOIN "
                        sqlstr &= " History_MST ON Category_MST.nCategoryID = History_MST.nCategoryID "
                        sqlstr &= "where Category_MST.sDescription  = '" & oCategory & "' ORDER BY History_MST.sDescription "

                        dtHistory = ODB.ReadQueryDataTable(sqlstr)

                        If Not IsNothing(dtHistory) Then
                            For i As Integer = 0 To dtHistory.Rows.Count - 1
                                oHistory.Items.Add(dtHistory.Rows(i)("nHistoryID"), dtHistory.Rows(i)("sDescription"))
                            Next

                            dtHistory.Dispose()
                            dtHistory = Nothing
                        End If



                        Return oHistory
                            End If
                            Return Nothing

                    Catch ex As SqlException
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateLog("clsDiseaseManagement -- Histories -- " & ex.ToString)
                        Return Nothing
                    Catch ex As Exception
                        UpdateLog("clsDiseaseManagement -- Histories -- " & ex.ToString)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Finally
                        ODB.Dispose()
                        ODB = Nothing
                        If Not IsNothing(dtHistory) Then
                            dtHistory.Dispose()
                            dtHistory = Nothing
                        End If

                        oHistory = Nothing
                    End Try
                End Function
                ''Sandip Darade 20090305
                ''for the function to return datatable for coded history                
                Public Function GetHistoriesDataTable(ByVal oCategory As String) As DataTable

                    Dim patientHistory As New clsPatientHistory()

                    Dim ODB As New gloStream.gloDataBase.gloDataBase
                    Dim dtHistory As DataTable
                    '' SUDHIR 20090622 ''
                    oCategory = oCategory.Replace("'", "''")
                    '' END SUDHIR ''

                    'Dim sqlstr As String

                    Try
                        If gblnCodedHistory Then ''IF CODED HISTORY ENABLED
                            If oCategory.StartsWith("Aller") Then
                                dtHistory = patientHistory.GetAllAllergies()                                
                                Return dtHistory
                            Else

                                Dim objclsPatientHistory As New clsPatientHistory
                                dtHistory = objclsPatientHistory.GetAllICD9Gallery
                                objclsPatientHistory.Dispose()
                                objclsPatientHistory = Nothing

                                Return dtHistory

                            End If

                        ElseIf oCategory.StartsWith("Aller") Then
                            dtHistory = patientHistory.GetAllAllergies()

                            Return dtHistory
                        Else
                            ODB.Connect(GetConnectionString)

                            dtHistory = patientHistory.GetAllHistory(oCategory)

                            Return dtHistory
                        End If
                        Return Nothing

                    Catch ex As SqlException
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateLog("clsDiseaseManagement -- Histories -- " & ex.ToString)
                        Return Nothing
                    Catch ex As Exception
                        UpdateLog("clsDiseaseManagement -- Histories -- " & ex.ToString)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Finally
                        If (IsNothing(ODB) = False) Then
                            ODB.Disconnect()
                            ODB.Dispose()
                        End If
                        ODB = Nothing
                        If patientHistory IsNot Nothing Then
                            patientHistory.Dispose()
                            patientHistory = Nothing
                        End If
                    End Try
                End Function
       
                Public Function Drugs(ByVal SearchDrug As String) As gloStream.DiseaseManagement.Supporting.Drugs

                    Dim ODB As New gloStream.gloDataBase.gloDataBase
                    Dim oDataReader As SqlClient.SqlDataReader
                    Dim oDrugs As New gloStream.DiseaseManagement.Supporting.Drugs
                    '  Dim sqlstr As String

                    Try
                        ''''''''''''''''''Code is Modified by Anil on 20071106
                        'sqlstr = "SELECT nDrugsID, sDrugName, sDosage, sRoute, sFrequency, sDuration FROM Drugs_MST WHERE(bIsClinicalDrug = " & 1 & ""

                        ODB.DBParameters.Add("@drugletter", SearchDrug, ParameterDirection.Input, SqlDbType.VarChar)
                        'sarika DM Denormalization for Rx on 20090410
                        '                        ODB.DBParameters.Add("@flag", 1, ParameterDirection.Input, SqlDbType.Int)
                        ODB.DBParameters.Add("@flag", 16, ParameterDirection.Input, SqlDbType.Int)
                        '--
                        ODB.DBParameters.Add("@PatientID", 0, ParameterDirection.Input, SqlDbType.BigInt)

                        ODB.Connect(GetConnectionString)
                        oDataReader = ODB.ReadRecords("gsp_FillDrugs_Mst")

                        If Not oDataReader Is Nothing Then
                            If oDataReader.HasRows = True Then
                                While oDataReader.Read
                                    Dim oDrug As New gloStream.DiseaseManagement.Supporting.Drug
                                    With oDrug
                                        If Not IsDBNull(oDataReader.Item(0)) Then  '' DrugID
                                            .ID = oDataReader.Item(0)
                                        End If
                                        If Not IsDBNull(oDataReader.Item(1)) Then '' Drug Name
                                            .Name = oDataReader.Item(1)
                                            .DrugName = oDataReader.Item(1)
                                            'sarika DM Denormalization for Rx 20090410
                                            If Not IsDBNull(oDataReader.Item("Dosage")) Then
                                                .Name = .Name & oDataReader.Item("Dosage")
                                                .Dosage = oDataReader.Item("Dosage")
                                            End If
                                        End If

                                        'sarika DM Denormalization
                                        'If Not IsDBNull(oDataReader.Item(2)) Then '' Drug Name
                                        '    .DrugName = oDataReader.Item(1)
                                        'End If
                                        'If Not IsDBNull(oDataReader.Item("Dosage")) Then '' Dosage
                                        '    .Dosage = oDataReader.Item("Dosage")
                                        'End If
                                        ''---
                                        'If Not IsDBNull(oDataReader.Item(2)) Then '' Dosage
                                        '    .Dosage = oDataReader.Item(2)
                                        'End If

                                        If Not IsDBNull(oDataReader.Item("DrugForm")) Then ''  Route  
                                            .DrugForm = oDataReader.Item("DrugForm")
                                        End If
                                        If Not IsDBNull(oDataReader.Item("sRoute")) Then ''  Route  
                                            .Route = oDataReader.Item("sRoute")
                                        End If
                                        If Not IsDBNull(oDataReader.Item("sFrequency")) Then '' Frquency
                                            .Frequency = oDataReader.Item("sFrequency")
                                        End If
                                        If Not IsDBNull(oDataReader.Item("sNDCCode")) Then ''  NDC Code
                                            .NDCCode = oDataReader.Item("sNDCCode")
                                        End If
                                        If Not IsDBNull(oDataReader.Item("sDuration")) Then ''  Duration
                                            .Duration = oDataReader.Item("sDuration")
                                        End If
                                        If Not IsDBNull(oDataReader.Item("nIsNarcotics")) Then ''  IS narcotics
                                            .IsNarcotics = oDataReader.Item("nIsNarcotics")
                                        End If
                                       
                                        If Not IsDBNull(oDataReader.Item("sDrugQtyQualifier")) Then ''  Drug Quantity Qualifier
                                            .DrugQtyQualifier = oDataReader.Item("sDrugQtyQualifier")
                                        End If


                                    End With
                                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                                    oDrugs.Add(oDrug)
                                    oDrug = Nothing

                                End While
                            End If
                            oDataReader.Close()
                        End If

                        ODB.Disconnect()

                        Return oDrugs

                    Catch ex As SqlException
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateLog("clsDiseaseManagement -- Drugs -- " & ex.ToString)
                        Return Nothing
                    Catch ex As Exception
                        UpdateLog("clsDiseaseManagement -- Drugs -- " & ex.ToString)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Finally
                        ODB.Dispose()
                        ODB = Nothing
                        oDataReader = Nothing

                    End Try
                End Function

                Public Function ICD9s() As gloStream.DiseaseManagement.Supporting.ICD9s
                    Dim ODB As New gloStream.gloDataBase.gloDataBase
                    Dim oDataReader As SqlClient.SqlDataReader
                    Dim oICD9s As New gloStream.DiseaseManagement.Supporting.ICD9s
                    Dim sqlstr As String

                    Try

                        ODB.Connect(GetConnectionString)
                        sqlstr = "SELECT nICD9ID, sICD9Code, sDescription FROM ICD9"

                        oDataReader = ODB.ReadQueryRecords(sqlstr)

                        If Not oDataReader Is Nothing Then
                            If oDataReader.HasRows = True Then
                                While oDataReader.Read
                                    Dim oICD9 As New gloStream.DiseaseManagement.Supporting.ICD9
                                    With oICD9
                                        If Not IsDBNull(oDataReader.Item("nICD9ID")) Then
                                            .ID = oDataReader.Item("nICD9ID")
                                        End If
                                        If Not IsDBNull(oDataReader.Item("sICD9Code")) Then
                                            .Code = oDataReader.Item("sICD9Code")
                                        End If
                                        If Not IsDBNull(oDataReader.Item("sDescription")) Then
                                            .Name = oDataReader.Item("sDescription")
                                        End If
                                    End With
                                    oICD9s.Add(oICD9)
                                End While
                            End If
                            oDataReader.Close()
                        End If
                        ODB.Disconnect()

                        Return oICD9s
                    Catch ex As SqlException
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateLog("clsDiseaseManagement -- ICD9s -- " & ex.ToString)
                        Return Nothing
                    Catch ex As Exception
                        UpdateLog("clsDiseaseManagement -- ICD9s -- " & ex.ToString)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Finally
                        ODB.Dispose()
                        ODB = Nothing
                        oDataReader = Nothing
                        oICD9s = Nothing
                    End Try

                End Function

                Public Function CPTs() As gloStream.DiseaseManagement.Supporting.CPTs
                    Dim ODB As New gloStream.gloDataBase.gloDataBase
                    Dim oDataReader As SqlClient.SqlDataReader
                    Dim oCPTs As New gloStream.DiseaseManagement.Supporting.CPTs
                    Dim sqlstr As String

                    Try

                        ODB.Connect(GetConnectionString)

                        sqlstr = " SELECT nCPTID, sCPTCode, sDescription FROM  CPT_MST "

                        oDataReader = ODB.ReadQueryRecords(sqlstr)
                        If Not oDataReader Is Nothing Then
                            If oDataReader.HasRows = True Then
                                While oDataReader.Read
                                    Dim oCPT As New gloStream.DiseaseManagement.Supporting.CPT
                                    With oCPT
                                        If Not IsDBNull(oDataReader.Item("nCPTID")) Then
                                            .ID = oDataReader.Item("nCPTID")
                                        End If
                                        If Not IsDBNull(oDataReader.Item("sCPTCode")) Then
                                            .Code = oDataReader.Item("sCPTCode")
                                        End If
                                        If Not IsDBNull(oDataReader.Item("sDescription")) Then
                                            .Name = oDataReader.Item("sDescription")
                                        End If
                                    End With
                                    oCPTs.Add(oCPT)
                                End While
                            End If
                            oDataReader.Close()
                        End If



                        Return oCPTs

                    Catch ex As SqlException
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateLog("clsDiseaseManagement -- CPTs -- " & ex.ToString)
                        Return Nothing
                    Catch ex As Exception
                        UpdateLog("clsDiseaseManagement -- CPTs -- " & ex.ToString)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Finally
                        If ODB IsNot Nothing Then
                            ODB.Disconnect()
                            ODB.Dispose()
                            ODB = Nothing
                        End If
                        oDataReader = Nothing
                        oCPTs = Nothing
                    End Try
                End Function

                Public Function Orders() As gloStream.DiseaseManagement.Supporting.Orders
                    Dim ODB As New gloStream.gloDataBase.gloDataBase
                    Dim oLabs As New gloStream.DiseaseManagement.Supporting.Orders
                    Dim oLab As gloStream.DiseaseManagement.Supporting.Order
                    Dim oGroup As gloStream.DiseaseManagement.Supporting.OrderGroup

                    '  Dim sqlstr As String

                    Dim oDTLab_Category As DataTable, oDTLab_Groups As DataTable, oDTLab_Tests As DataTable

                    Try
                        ODB.Connect(GetConnectionString)
                        'fill the oDTLab_Category table by the data(id,description) from the LM_Category table  
                        oDTLab_Category = ODB.ReadData("DM_SelectLMCategory")
                        If (IsNothing(oDTLab_Category) = False) Then
                            With oLabs
                                'for each category in the oDTLab_Category(LM_Category) table
                                For i As Integer = 0 To oDTLab_Category.Rows.Count - 1
                                    oLab = New gloStream.DiseaseManagement.Supporting.Order
                                    With oLab

                                        .ID = oDTLab_Category.Rows(i)("lm_category_ID")
                                        Dim id As Int64 = .ID
                                        .Category = oDTLab_Category.Rows(i)("lm_category_Description")

                                        'fill the oDTLab_Groups table by the data(id,name) from the LM_Test table  
                                        'where  'LM_Test(TestGroupFlag = 'G' and CateogryID = LM_Category(i))
                                        ODB.DBParameters.Clear()
                                        ODB.DBParameters.Add("@id", id, ParameterDirection.Input, SqlDbType.BigInt)
                                        oDTLab_Groups = ODB.ReadData("DM_SelectCategoryWiseLabGroup")
                                        If Not IsNothing(oDTLab_Groups) Then
                                            For j As Integer = 0 To oDTLab_Groups.Rows.Count - 1
                                                oGroup = New gloStream.DiseaseManagement.Supporting.OrderGroup
                                                With oGroup
                                                    .ID = oDTLab_Groups.Rows(j)("lm_test_ID")
                                                    Dim groupid As Int64 = .ID
                                                    .Name = oDTLab_Groups.Rows(j)("lm_test_Name")
                                                    'fill the oDTLab_Tests table by data (id,description) from the LM_test table 
                                                    'where LM_Test(TestGroupFlag = 'T' and CategoryID = LM_Category(i) and groupNo = LM_Test(j).id)
                                                    ODB.DBParameters.Clear()
                                                    ODB.DBParameters.Add("@id", id, ParameterDirection.Input, SqlDbType.BigInt)
                                                    ODB.DBParameters.Add("@Groupid", groupid, ParameterDirection.Input, SqlDbType.BigInt)
                                                    oDTLab_Tests = ODB.ReadData("DM_SelectGroupWiseLabTests")
                                                    If (IsNothing(oDTLab_Tests) = False) Then
                                                        For k As Integer = 0 To oDTLab_Tests.Rows.Count - 1
                                                            .Tests.Add(oDTLab_Tests.Rows(k)("lm_test_ID"), oDTLab_Tests.Rows(k)("lm_test_Name"))
                                                        Next
                                                        oDTLab_Tests.Dispose()
                                                        oDTLab_Tests = Nothing
                                                    End If

                                                End With
                                                oLab.OrderGroups.Add(oGroup)
                                                oGroup = Nothing
                                            Next

                                            oDTLab_Groups.Dispose()
                                            oDTLab_Groups = Nothing
                                        End If

                                    End With
                                    .Add(oLab)
                                    oLab = Nothing
                                Next
                            End With
                            oDTLab_Category.Dispose()
                            oDTLab_Category = Nothing
                        End If
  
                        Return oLabs

                    Catch ex As SqlException
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateLog("clsDiseaseManagement -- Labs -- " & ex.ToString)
                        Return Nothing
                    Catch ex As Exception
                        UpdateLog("clsDiseaseManagement -- Labs -- " & ex.ToString)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Finally
                        If ODB IsNot Nothing Then
                            ODB.Disconnect()
                            ODB.Dispose()
                            ODB = Nothing
                        End If
                    End Try

                End Function

                Public Function GetOrdersTable(ByVal nCriteriaID As Int64) As DataTable

                    Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
                    Dim oParamater As New gloDatabaseLayer.DBParameters()
                    Dim _dtLabTestsResults As DataTable = Nothing


                    Try
                        oDB.Connect(False)
                        oParamater.Add("@dm_MST_ID", nCriteriaID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                        oDB.Retrive("DM_GetRadiologyLabOrdersV2", oParamater, _dtLabTestsResults)
                        Return _dtLabTestsResults
                        'If oParamater IsNot Nothing Then
                        '    oParamater.Dispose()
                        '    oParamater = Nothing
                        'End If

                        'If oDB IsNot Nothing Then
                        '    oDB.Disconnect()
                        '    oDB.Dispose()
                        '    oDB = Nothing
                        'End If
                    Catch ex As SqlException
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateLog("clsDiseaseManagement -- LabModuleTests -- " & ex.ToString)

                        If oParamater IsNot Nothing Then
                            oParamater.Dispose()
                            oParamater = Nothing
                        End If

                        If oDB IsNot Nothing Then
                            oDB.Disconnect()
                            oDB.Dispose()
                            oDB = Nothing
                        End If
                        Return Nothing
                    Catch ex As Exception
                        UpdateLog("clsDiseaseManagement -- LabModuleTests -- " & ex.ToString)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        If oParamater IsNot Nothing Then
                            oParamater.Dispose()
                            oParamater = Nothing
                        End If

                        If oDB IsNot Nothing Then
                            oDB.Disconnect()
                            oDB.Dispose()
                            oDB = Nothing
                        End If
                        Return Nothing
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
                    'Return Nothing
                End Function
                Public Function GetExOrdersTable(ByVal nCriteriaID As Int64) As DataTable

                    Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
                    Dim oParamater As New gloDatabaseLayer.DBParameters()
                    Dim _dtLabTestsResults As DataTable = Nothing


                    Try
                        oDB.Connect(False)
                        oParamater.Add("@dm_MST_ID", nCriteriaID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                        oDB.Retrive("DM_GetExRadiologyLabOrdersV2", oParamater, _dtLabTestsResults)
                        Return _dtLabTestsResults
                        'If oParamater IsNot Nothing Then
                        '    oParamater.Dispose()
                        '    oParamater = Nothing
                        'End If

                        'If oDB IsNot Nothing Then
                        '    oDB.Disconnect()
                        '    oDB.Dispose()
                        '    oDB = Nothing
                        'End If
                    Catch ex As SqlException
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateLog("clsDiseaseManagement -- LabModuleTests -- " & ex.ToString)

                        If oParamater IsNot Nothing Then
                            oParamater.Dispose()
                            oParamater = Nothing
                        End If

                        If oDB IsNot Nothing Then
                            oDB.Disconnect()
                            oDB.Dispose()
                            oDB = Nothing
                        End If
                        Return Nothing
                    Catch ex As Exception
                        UpdateLog("clsDiseaseManagement -- LabModuleTests -- " & ex.ToString)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        If oParamater IsNot Nothing Then
                            oParamater.Dispose()
                            oParamater = Nothing
                        End If

                        If oDB IsNot Nothing Then
                            oDB.Disconnect()
                            oDB.Dispose()
                            oDB = Nothing
                        End If
                        Return Nothing
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
                
                Public Function OrdersTable() As DataTable
                    Dim oDB As New DataBaseLayer

                    Dim oResultTable As DataTable
                    Try
                        Dim _strSQL As String

                        ' _strSQL = "SELECT    lm_test_ID, lm_test_Name   FROM   LM_Test  WHERE  (lm_test_TestGroupFlag = 'T')"
                        ''Sandip Darade 20090820
                        _strSQL = " SELECT   LM_Test.lm_test_ID as lm_test_ID, LM_Test.lm_test_Name as lm_test_Name FROM   LM_Test INNER JOIN " _
                        & "LM_Test AS LM_Test_1 ON LM_Test.lm_test_GroupNo = LM_Test_1.lm_test_ID INNER JOIN " _
                        & " LM_Category ON LM_Test_1.lm_test_CategoryID = LM_Category.lm_category_ID "

                        oResultTable = oDB.GetDataTable_Query(_strSQL)
                        If Not oResultTable Is Nothing Then
                            Return oResultTable
                        End If
                        Return Nothing
                    Catch ex As SqlException
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Finally
                        If Not IsNothing(oDB) Then
                            oDB.Dispose()
                            oDB = Nothing
                        End If
                    End Try
                End Function


                Public Function LabModuleTests() As gloStream.DiseaseManagement.Supporting.LabModuleTests
                    Dim ODB As New gloStream.gloDataBase.gloDataBase
                    Dim _LabModuleTests As New gloStream.DiseaseManagement.Supporting.LabModuleTests
                    Dim _LabModuleTest As gloStream.DiseaseManagement.Supporting.LabModuleTest
                    Dim _LabModuleResult As gloStream.DiseaseManagement.Supporting.LabModuleTestResult

                    Dim odtl_Test As DataTable = Nothing
                    Dim odtl_Result As DataTable = Nothing
                    ODB.Connect(GetConnectionString)
                    Try
                        Dim strSelectQryTest As String = "SELECT DISTINCT labtm_Name,labtm_id FROM Lab_Test_Mst"
                        odtl_Test = ODB.ReadQueryDataTable(strSelectQryTest)
                        If Not IsNothing(odtl_Test) Then
                            For i As Integer = 0 To odtl_Test.Rows.Count - 1
                                _LabModuleTest = New gloStream.DiseaseManagement.Supporting.LabModuleTest
                                With _LabModuleTest
                                    .TestID = odtl_Test.Rows(i)("labtm_id")
                                    .Name = odtl_Test.Rows(i)("labtm_Name")

                                    Dim strSelectQryResult As String = "Select DISTINCT ISNULL(Lab_Order_Test_ResultDtl.labotrd_ResultName,'') as 'labotrd_ResultName',ISNULL(Lab_Order_Test_ResultDtl.labotrd_ResultNameID,0) as 'labotrd_ResultNameID' FROM Lab_Order_Test_ResultDtl INNER JOIN Lab_Order_Test_Result ON Lab_Order_Test_ResultDtl.labotrd_TestID = " & odtl_Test.Rows(i)("labtm_id") & " "
                                    odtl_Result = ODB.ReadQueryDataTable(strSelectQryResult)
                                    If Not IsNothing(odtl_Result) Then
                                        For j As Integer = 0 To odtl_Result.Rows.Count - 1
                                            _LabModuleResult = New gloStream.DiseaseManagement.Supporting.LabModuleTestResult
                                            With _LabModuleResult
                                                .ResultID = odtl_Result.Rows(j)("labotrd_ResultNameID")
                                                .ResultName = odtl_Result.Rows(j)("labotrd_ResultName")
                                            End With

                                            .LabModuleTestResults.Add(_LabModuleResult)
                                            _LabModuleResult = Nothing
                                        Next 'j
                                        odtl_Result.Dispose()
                                        odtl_Result = Nothing
                                    End If
                                End With

                                _LabModuleTests.Add(_LabModuleTest)
                                _LabModuleTest = Nothing
                            Next 'i
                            odtl_Test.Dispose()
                            odtl_Test = Nothing
                        End If
                        Return _LabModuleTests

                    Catch ex As SqlException
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateLog("clsDiseaseManagement -- LabModuleTests -- " & ex.ToString)
                        Return Nothing
                    Catch ex As Exception
                        UpdateLog("clsDiseaseManagement -- LabModuleTests -- " & ex.ToString)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Finally
                        If odtl_Result IsNot Nothing Then
                            odtl_Result.Dispose()
                            odtl_Result = Nothing
                        End If
                        If odtl_Test IsNot Nothing Then
                            odtl_Test.Dispose()
                            odtl_Test = Nothing
                        End If
                        If ODB IsNot Nothing Then
                            ODB.Disconnect()
                            ODB.Dispose()
                            ODB = Nothing
                        End If
                    End Try
                End Function

                Public Function GetCriteriaRefInfo(ByVal nCriteriaID As Int64) As DataTable
                    Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
                    Dim oParamater As New gloDatabaseLayer.DBParameters()
                    Dim _dtCriteriaRefInfo As DataTable = Nothing


                    Try
                        oDB.Connect(False)
                        oParamater.Add("@dm_MST_ID", nCriteriaID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                        oDB.Retrive("DM_GetCriteriaRefInfo", oParamater, _dtCriteriaRefInfo)
                        Return _dtCriteriaRefInfo
                        'If oParamater IsNot Nothing Then
                        '    oParamater.Dispose()
                        '    oParamater = Nothing
                        'End If

                        'If oDB IsNot Nothing Then
                        '    oDB.Disconnect()
                        '    oDB.Dispose()
                        '    oDB = Nothing
                        'End If
                    Catch ex As SqlException
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateLog("clsDiseaseManagement -- GetCriteriaRefInfo -- " & ex.ToString)

                        If oParamater IsNot Nothing Then
                            oParamater.Dispose()
                            oParamater = Nothing
                        End If

                        If oDB IsNot Nothing Then
                            oDB.Disconnect()
                            oDB.Dispose()
                            oDB = Nothing
                        End If
                        Return Nothing
                    Catch ex As Exception
                        UpdateLog("clsDiseaseManagement -- GetCriteriaRefInfo -- " & ex.ToString)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        If oParamater IsNot Nothing Then
                            oParamater.Dispose()
                            oParamater = Nothing
                        End If

                        If oDB IsNot Nothing Then
                            oDB.Disconnect()
                            oDB.Dispose()
                            oDB = Nothing
                        End If
                        Return Nothing
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


                Public Function GetLabModuleTestsTable(ByVal nCriteriaID As Int64) As DataTable

                    Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
                    Dim oParamater As New gloDatabaseLayer.DBParameters()
                    Dim _dtLabTestsResults As DataTable = Nothing


                    Try
                        oDB.Connect(False)
                        oParamater.Add("@dm_MST_ID", nCriteriaID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                        oDB.Retrive("DM_GetLabResultsV2", oParamater, _dtLabTestsResults)
                        Return _dtLabTestsResults
                        'If oParamater IsNot Nothing Then
                        '    oParamater.Dispose()
                        '    oParamater = Nothing
                        'End If

                        'If oDB IsNot Nothing Then
                        '    oDB.Disconnect()
                        '    oDB.Dispose()
                        '    oDB = Nothing
                        'End If
                    Catch ex As SqlException
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateLog("clsDiseaseManagement -- LabModuleTests -- " & ex.ToString)

                        If oParamater IsNot Nothing Then
                            oParamater.Dispose()
                            oParamater = Nothing
                        End If

                        If oDB IsNot Nothing Then
                            oDB.Disconnect()
                            oDB.Dispose()
                            oDB = Nothing
                        End If
                        Return Nothing
                    Catch ex As Exception
                        UpdateLog("clsDiseaseManagement -- LabModuleTests -- " & ex.ToString)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        If oParamater IsNot Nothing Then
                            oParamater.Dispose()
                            oParamater = Nothing
                        End If

                        If oDB IsNot Nothing Then
                            oDB.Disconnect()
                            oDB.Dispose()
                            oDB = Nothing
                        End If
                        Return Nothing
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

                Public Function GeExLabModuleTestsTable(ByVal nCriteriaID As Int64) As DataTable

                    Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
                    Dim oParamater As New gloDatabaseLayer.DBParameters()
                    Dim _dtLabTestsResults As DataTable = Nothing


                    Try
                        oDB.Connect(False)
                        oParamater.Add("@dm_MST_ID", nCriteriaID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                        oDB.Retrive("DM_GetExLabResultsV2", oParamater, _dtLabTestsResults)
                        Return _dtLabTestsResults
                        'If oParamater IsNot Nothing Then
                        '    oParamater.Dispose()
                        '    oParamater = Nothing
                        'End If

                        'If oDB IsNot Nothing Then
                        '    oDB.Disconnect()
                        '    oDB.Dispose()
                        '    oDB = Nothing
                        'End If
                    Catch ex As SqlException
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateLog("clsDiseaseManagement -- LabModuleTests -- " & ex.ToString)

                        If oParamater IsNot Nothing Then
                            oParamater.Dispose()
                            oParamater = Nothing
                        End If

                        If oDB IsNot Nothing Then
                            oDB.Disconnect()
                            oDB.Dispose()
                            oDB = Nothing
                        End If
                        Return Nothing
                    Catch ex As Exception
                        UpdateLog("clsDiseaseManagement -- LabModuleTests -- " & ex.ToString)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        If oParamater IsNot Nothing Then
                            oParamater.Dispose()
                            oParamater = Nothing
                        End If

                        If oDB IsNot Nothing Then
                            oDB.Disconnect()
                            oDB.Dispose()
                            oDB = Nothing
                        End If
                        Return Nothing
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

                

                Public Function LabModuleTest() As DataTable
                    Dim ODB As New gloStream.gloDataBase.gloDataBase
                    Dim dtTest As DataTable
                    Try
                        ODB.Connect(GetConnectionString)
                        Dim strSelectQry As String = "SELECT DISTINCT labtm_Name,labtm_id FROM Lab_Test_Mst"
                        dtTest = ODB.ReadQueryDataTable(strSelectQry)
                        If Not IsDBNull(dtTest) = True Then
                            Return dtTest
                        End If
                        Return Nothing
                    Catch ex As SqlException
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateLog("clsDiseaseManagement -- LabModuleTest -- " & ex.ToString)
                        Return Nothing
                    Catch ex As Exception
                        UpdateLog("clsDiseaseManagement -- LabModuleTest -- " & ex.ToString)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Finally
                        ODB.Disconnect()
                        ODB.Dispose()
                        ODB = Nothing
                    End Try
                End Function
                '' chetan added for gettting chiefcomplaint from problemlist
                Public Function GetProblemList(Optional ByVal strsearch As String = "") As DataTable
                    Dim ODB As New gloStream.gloDataBase.gloDataBase
                    Dim dtResult As DataTable
                    ODB.Connect(GetConnectionString)
                    Try
                        'Dim strSelectQry As String = "SELECT DISTINCT labotrd_ResultName FROM Lab_Order_Test_ResultDtl Where labotd_testid = " & TestID & ""
                        Dim strSelectQry As String = ""
                        If strsearch.Trim() <> "" Then
                            strSelectQry = "Select DISTINCT ISNULL(sCheifComplaint,'') as sChiefComplaint from ProblemList where sCheifComplaint like '%" & strsearch & "%' "


                        Else
                            strSelectQry = "Select DISTINCT ISNULL(sCheifComplaint,'') as sChiefComplaint from ProblemList "

                        End If
                        dtResult = ODB.ReadQueryDataTable(strSelectQry)
                        If Not IsDBNull(dtResult) = True Then
                            Return dtResult
                        End If
                        Return Nothing
                    Catch ex As SqlException
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateLog("clsDiseaseManagement -- GetProblemList -- " & ex.ToString)
                        Return Nothing
                    Catch ex As Exception
                        UpdateLog("clsDiseaseManagement -- GetProblemList -- " & ex.ToString)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Finally
                        If ODB IsNot Nothing Then
                            ODB.Disconnect()
                            ODB.Dispose()
                            ODB = Nothing
                        End If
                    End Try
                End Function

                Public Function LabModuleResult(ByVal TestID As Long) As DataTable
                    Dim ODB As New gloStream.gloDataBase.gloDataBase
                    Dim dtResult As DataTable
                    ODB.Connect(GetConnectionString)
                    Try
                        'Dim strSelectQry As String = "SELECT DISTINCT labotrd_ResultName FROM Lab_Order_Test_ResultDtl Where labotd_testid = " & TestID & ""
                        Dim strSelectQry As String = "Select DISTINCT Lab_Order_Test_ResultDtl.labotrd_ResultName,Lab_Order_Test_ResultDtl.labotrd_ResultNameID FROM Lab_Order_Test_ResultDtl INNER JOIN Lab_Order_Test_Result ON Lab_Order_Test_ResultDtl.labotrd_TestID = " & TestID & " "
                        dtResult = ODB.ReadQueryDataTable(strSelectQry)
                        If Not IsDBNull(dtResult) = True Then
                            Return dtResult
                        End If
                        Return Nothing
                    Catch ex As SqlException
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateLog("clsDiseaseManagement -- LabModuleResult -- " & ex.ToString)
                        Return Nothing
                    Catch ex As Exception
                        UpdateLog("clsDiseaseManagement -- LabModuleResult -- " & ex.ToString)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Finally
                        ODB.Disconnect()
                        ODB.Dispose()
                        ODB = Nothing
                    End Try
                End Function

                '-----------------------------------------------------------------------
                '
                '-----------------------------------------------------------------------
                Public Function Age() As Collection
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
                        _ErrorMessage = ex.Message
                        Return Nothing
                    Finally
                        _Age = Nothing
                    End Try
                End Function

                Public Function Gender() As Collection
                    'Male,Female,Other,All ' ref: gloEMR - patient registration form code file

                    'declare the collection object
                    Dim _Gender As New Collection

                    Try
                        'fill the collection object
                        With _Gender
                            .Add("Male")
                            .Add("Female")
                            .Add("Other")
                            .Add("Unknown")
                            .Add("All")
                        End With

                        Return _Gender

                    Catch ex As Exception
                        UpdateLog("clsDiseaseManagement -- Gender -- " & ex.ToString)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Finally
                        _Gender = Nothing
                    End Try
                End Function

                Public Function Race() As Collection
                    'Category_Mst where type = race

                    'declare the datareader and connection object
                    Dim ODB As New gloStream.gloDataBase.gloDataBase
                    Dim oDataReader As SqlClient.SqlDataReader
                    Dim sqlstr As String

                    'declare the collection object
                    Dim _Race As New Collection

                    sqlstr = "SELECT sDescription FROM Category_MST WHERE sCategoryType IN ('Race','Race Specification')"

                    'Connect to the database
                    ODB.Connect(GetConnectionString)

                    'Get records and return a datareader
                    oDataReader = ODB.ReadQueryRecords(sqlstr)

                    Try
                        If Not oDataReader Is Nothing Then
                            If oDataReader.HasRows = True Then
                                While oDataReader.Read
                                    'Fill the collection object
                                    With _Race
                                        If Not IsDBNull(oDataReader("sDescription")) Then
                                            .Add(oDataReader("sDescription"))
                                        End If
                                    End With
                                End While
                            End If
                            oDataReader.Close()
                        End If

                        Return _Race
                    Catch ex As SqlException
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateLog("clsDiseaseManagement -- Race -- " & ex.ToString)
                        Return Nothing
                    Catch ex As Exception
                        UpdateLog("clsDiseaseManagement -- Race -- " & ex.ToString)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Finally
                        If oDataReader IsNot Nothing Then
                            oDataReader.Dispose()
                            oDataReader = Nothing
                        End If
                        _Race = Nothing
                        ODB.Disconnect()
                        ODB.Dispose()
                        ODB = Nothing
                    End Try

                End Function

                Public Function MaritalStatus() As Collection
                    'Unmarried, Married, Single, Widowed, Divorced ' ' ref: gloEMR - patient registration form code file

                    'declare a collection object
                    Dim _MaritalStatus As New Collection

                    Try
                        'Fill the collection object
                        With _MaritalStatus
                            .Add("Unmarried")
                            .Add("Married")
                            .Add("Single")
                            .Add("Widowed")
                            .Add("Divorced")
                        End With

                        Return _MaritalStatus
                    Catch ex As Exception
                        UpdateLog("clsDiseaseManagement -- MaritalStatus -- " & ex.ToString)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Finally
                        ''  _MaritalStatus = Nothing
                    End Try
                End Function

                Public Function State() As Collection
                    'CSZ_MST - Field Name = ST

                    'declare the datareader and the connection object
                    Dim ODB As New gloStream.gloDataBase.gloDataBase
                    Dim oDataReader As SqlClient.SqlDataReader
                    Dim sqlstr As String

                    'declare the collection object
                    Dim _State As New Collection

                    sqlstr = "SELECT DISTINCT ST FROM CSZ_MST"

                    'Connect to the database
                    ODB.Connect(GetConnectionString)

                    'Execute the query ang return a datareader
                    oDataReader = ODB.ReadQueryRecords(sqlstr)

                    Try
                        'read from the datareader
                        If Not oDataReader Is Nothing Then
                            If oDataReader.HasRows = True Then
                                While oDataReader.Read
                                    'Fill the collection object
                                    With _State
                                        If Not IsDBNull(oDataReader.Item("ST")) Then
                                            .Add(oDataReader.Item("ST"))
                                        End If
                                    End With
                                End While
                            End If
                            oDataReader.Close()
                        End If


                        Return _State
                    Catch ex As SqlException
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateLog("clsDiseaseManagement -- State -- " & ex.ToString)
                        Return Nothing
                    Catch ex As Exception
                        UpdateLog("clsDiseaseManagement -- State -- " & ex.ToString)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Finally
                        If oDataReader IsNot Nothing Then
                            oDataReader.Dispose()
                            oDataReader = Nothing
                        End If
                        ODB.Disconnect()
                        ODB.Dispose()
                        ODB = Nothing
                        _State = Nothing
                    End Try
                End Function

                Public Function EmploymentStatus() As Collection
                    'Retired, Employed, Unemployed, Self-Employed, Student

                    'declare the collection object
                    Dim _EmploymentStatus As New Collection

                    Try
                        'fill the collection object
                        With _EmploymentStatus
                            .Add("Retired")
                            .Add("Employed")
                            .Add("Unemployed")
                            .Add("Self-Employed")
                            .Add("Student")
                        End With

                        Return _EmploymentStatus
                    Catch ex As Exception
                        UpdateLog("clsDiseaseManagement -- EmploymentStatus -- " & ex.ToString)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Finally
                        _EmploymentStatus = Nothing
                    End Try
                End Function

                Public Sub New()
                    MyBase.new()
                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub

#Region "IDisposable Support"
                Private disposedValue As Boolean ' To detect redundant calls

                ' IDisposable
                Protected Overridable Sub Dispose(ByVal disposing As Boolean)
                    If Not Me.disposedValue Then
                        If disposing Then
                            ' TODO: dispose managed state (managed objects).
                        End If

                        ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                        ' TODO: set large fields to null.
                    End If
                    Me.disposedValue = True
                End Sub

                ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
                'Protected Overrides Sub Finalize()
                '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
                '    Dispose(False)
                '    MyBase.Finalize()
                'End Sub

                ' This code added by Visual Basic to correctly implement the disposable pattern.
                Public Sub Dispose() Implements IDisposable.Dispose
                    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
                    Dispose(True)
                    GC.SuppressFinalize(Me)
                End Sub
#End Region

            End Class

            Public Class Guidelines
                Private _ErrorMessage As String

                Public Property ErrorMessage() As String
                    Get
                        Return _ErrorMessage
                    End Get
                    Set(ByVal Value As String)
                        _ErrorMessage = Value
                    End Set
                End Property

                Public Function Guidelines(ByVal oType As String) As gloStream.DiseaseManagement.Supporting.ItemDetails
                    Dim ODB As New gloStream.gloDataBase.gloDataBase
                    Dim oDataReader As SqlClient.SqlDataReader = Nothing
                    Dim oGuidelines As New gloStream.DiseaseManagement.Supporting.ItemDetails

                    Dim sqlstr As String

                    Try
                        '      oGuidelines.Add()
                        ODB.Connect(GetConnectionString)

                        'sarika DM Denormalization 20090401

                        'sqlstr = "SELECT TemplateGallery_MST.nTemplateID, TemplateGallery_MST.sTemplateName"
                        'sqlstr &= " FROM TemplateGallery_MST INNER JOIN Category_MST ON TemplateGallery_MST.nCategoryID = Category_MST.nCategoryID"
                        'sqlstr &= " WHERE Category_MST.sCategoryType = 'Template' AND Category_MST.sDescription = '" & oType & "'"

                        sqlstr = "SELECT TemplateGallery_MST.nTemplateID, TemplateGallery_MST.sTemplateName, TemplateGallery_MST.sDescription"
                        sqlstr &= " FROM TemplateGallery_MST INNER JOIN Category_MST ON TemplateGallery_MST.nCategoryID = Category_MST.nCategoryID"
                        sqlstr &= " WHERE Category_MST.sCategoryType = 'Template' AND Category_MST.sDescription = '" & oType & "'"


                        '--------


                        oDataReader = ODB.ReadQueryRecords(sqlstr)

                        If Not oDataReader Is Nothing Then
                            If oDataReader.HasRows = True Then
                                While oDataReader.Read
                                    If Not (IsDBNull(oDataReader.Item("nTemplateID")) And IsDBNull(oDataReader.Item("sTemplateName"))) Then
                                        oGuidelines.Add(oDataReader.Item("nTemplateID"), oDataReader.Item("sTemplateName"), oDataReader.Item("sDescription"))
                                    End If
                                End While
                            End If
                            oDataReader.Close()
                        End If

                        ODB.Disconnect()

                        Return oGuidelines
                    Catch ex As SqlException
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateLog("clsDiseaseManagement -- Guidelines -- " & ex.ToString)
                        Return Nothing
                    Catch ex As Exception
                        UpdateLog("clsDiseaseManagement -- Guidelines -- " & ex.ToString)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Finally
                        If Not ODB Is Nothing Then
                            ODB.Dispose()
                            ODB = Nothing
                        End If

                        If Not oDataReader Is Nothing Then
                            oDataReader = Nothing
                        End If


                        oGuidelines = Nothing
                    End Try

                End Function

                Public Function GuidelinesTables(ByVal oType As String) As DataTable
                    Dim oDB As New DataBaseLayer
                    Dim dtresult As DataTable = Nothing
                    Dim sqlstr As String

                    Try

                        sqlstr = "SELECT TemplateGallery_MST.nTemplateID, TemplateGallery_MST.sTemplateName, TemplateGallery_MST.sDescription"
                        sqlstr &= " FROM TemplateGallery_MST INNER JOIN Category_MST ON TemplateGallery_MST.nCategoryID = Category_MST.nCategoryID"
                        sqlstr &= " WHERE Category_MST.sCategoryType = 'Template' AND Category_MST.sDescription  in ('Patient Education','Preventive Services','Wellness Guidelines')"

                        dtresult = oDB.GetDataTable_Query(sqlstr)
                        If Not dtresult Is Nothing Then
                            Return dtresult
                        End If
                        '--------
                        Return Nothing
                    Catch ex As SqlException
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateLog("clsDiseaseManagement -- Guidelines -- " & ex.ToString)
                        Return Nothing
                    Catch ex As Exception
                        UpdateLog("clsDiseaseManagement -- Guidelines -- " & ex.ToString)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Finally
                        If oDB IsNot Nothing Then
                            oDB.Dispose()
                            oDB = Nothing
                        End If
                    End Try

                End Function

                Public Sub New()
                    MyBase.new()
                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub
            End Class

            Public Class GuidelinesType

                Public Shared ReadOnly Property PreventiveServices() As String
                    Get
                        Return "Preventive Services"
                    End Get
                End Property

                Public Shared ReadOnly Property WellnessGuidelines() As String
                    Get
                        Return "Wellness Guidelines"
                    End Get
                End Property

                Public Shared ReadOnly Property PatientEducation() As String
                    Get
                        Return "Patient Education"
                    End Get
                End Property

                Public Sub New()
                    MyBase.new()
                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub
            End Class

        End Namespace

        Namespace Supporting

            '//<<<<<<<<<<<<<< CATEGORY >>>>>>>>>>>>>>>>>>//

            Public Class Category
                Private _ID As Long
                Private _Name As String
                Private _Type As String = "History"

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

                Private ReadOnly Property Type() As String
                    Get
                        Return _Type
                    End Get
                End Property

                Public Sub New()
                    MyBase.new()
                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub
            End Class

            Public Class Categories
                Implements System.Collections.IEnumerable
                Private mCol As Collection
                Public Sub Dispose()
                    If (IsNothing(mCol) = False) Then
                        mCol.Clear()
                        mCol = Nothing
                    End If
                End Sub
                Public Function Add(ByRef oCategory As gloStream.DiseaseManagement.Supporting.Category) As gloStream.DiseaseManagement.Supporting.Category
                    mCol.Add(oCategory)
                    Return Nothing
                End Function

                Public Function Add(ByVal ID As Long, ByVal Name As String) As gloStream.DiseaseManagement.Supporting.Category
                    Dim objNewMember As gloStream.DiseaseManagement.Supporting.Category
                    Try
                        objNewMember = New gloStream.DiseaseManagement.Supporting.Category
                        objNewMember.ID = ID
                        objNewMember.Name = Name
                        mCol.Add(objNewMember)
                        Add = objNewMember
                        objNewMember = Nothing
                    Catch ex As Exception
                        UpdateLog("clsDiseaseManagement -- Supporting -- Add -- " & ex.ToString)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    End Try
                End Function

                Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As gloStream.DiseaseManagement.Supporting.Category
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

            '//<<<<<<<<<<<<<< HISTORY >>>>>>>>>>>>>>>>>>//

            Public Class History
                Private _Category As String
                Private _CategoryID As Long
                Private _HistoryItems As gloStream.DiseaseManagement.Supporting.HistoryItems
                Private bAssigned = False

                Public Sub Dispose()
                    If (bAssigned) Then
                        _HistoryItems.Dispose()
                        bAssigned = False
                    End If
                End Sub
                Public Property CategoryID() As Long
                    Get
                        Return _CategoryID
                    End Get
                    Set(ByVal Value As Long)
                        _CategoryID = Value
                    End Set
                End Property

                Public Property Category() As String
                    Get
                        Return _Category
                    End Get
                    Set(ByVal Value As String)
                        _Category = Value
                    End Set
                End Property

                Public Property Items() As gloStream.DiseaseManagement.Supporting.HistoryItems
                    Get
                        Return _HistoryItems
                    End Get
                    Set(ByVal Value As gloStream.DiseaseManagement.Supporting.HistoryItems)
                        If (bAssigned) Then
                            _HistoryItems.Dispose()
                            bAssigned = False
                        End If
                        _HistoryItems = Value
                    End Set
                End Property

                Public Sub New()
                    MyBase.new()
                    _HistoryItems = New gloStream.DiseaseManagement.Supporting.HistoryItems
                    bAssigned = True
                End Sub

                Protected Overrides Sub Finalize()
                    _HistoryItems = Nothing
                    MyBase.Finalize()
                End Sub

            End Class

            Public Class Histories
                Implements System.Collections.IEnumerable
                Private mCol As Collection
                Public Sub Dispose()
                    If (IsNothing(mCol) = False) Then
                        mCol.Clear()
                        mCol = Nothing
                    End If
                End Sub
                Public Function Add(ByRef oHistory As gloStream.DiseaseManagement.Supporting.History) As gloStream.DiseaseManagement.Supporting.History
                    mCol.Add(oHistory)
                    Return Nothing
                End Function

                Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As gloStream.DiseaseManagement.Supporting.History
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

            Public Class HistoryItem
                Private _ID As Long
                Private _Name As String = ""
                Private _CategoryID As Long
                Private _CategoryName As String = ""

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

                Public Property CategoryID() As Long
                    Get
                        Return _CategoryID
                    End Get
                    Set(ByVal Value As Long)
                        _CategoryID = Value
                    End Set
                End Property

                Public Property CategoryName() As String
                    Get
                        Return _CategoryName
                    End Get
                    Set(ByVal Value As String)
                        _CategoryName = Value
                    End Set
                End Property

                Public Sub New()
                    MyBase.new()
                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub
            End Class

            Public Class HistoryItems
                Implements System.Collections.IEnumerable
                Private mCol As Collection
                Public Sub Dispose()
                    If (IsNothing(mCol) = False) Then
                        mCol.Clear()
                        mCol = Nothing
                    End If
                End Sub
                Public Function Add(ByRef oHistory As gloStream.DiseaseManagement.Supporting.HistoryItem) As gloStream.DiseaseManagement.Supporting.HistoryItem
                    mCol.Add(oHistory)
                    Return Nothing
                End Function

                Public Function Add(ByVal ID As Long, ByVal Name As String, Optional ByVal CategoryID As Int64 = 0, Optional ByVal CategoryName As String = "") As gloStream.DiseaseManagement.Supporting.HistoryItem
                    Dim objNewMember As gloStream.DiseaseManagement.Supporting.HistoryItem
                    Try
                        objNewMember = New gloStream.DiseaseManagement.Supporting.HistoryItem
                        objNewMember.ID = ID
                        objNewMember.Name = Name
                        objNewMember.CategoryID = CategoryID
                        objNewMember.CategoryName = CategoryName
                        mCol.Add(objNewMember)
                        Add = objNewMember
                        objNewMember = Nothing

                    Catch ex As Exception
                        UpdateLog("clsDiseaseManagement -- History -- Add -- " & ex.ToString)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    End Try

                End Function

                Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As gloStream.DiseaseManagement.Supporting.HistoryItem
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


            '//<<<<<<<<<<<<<< DRUGS >>>>>>>>>>>>>>>>>>//

            Public Class Drug
                Private _ID As Long = 0
                Private _Name As String = ""
                Private _Dosage As String = ""
                Private _Route As String = ""
                Private _Frequency As String = ""
                Private _Duration As String = ""
                Private _mpid As Int32 = 0

                'sarika DM Denormalization
                Private _DrugName As String = ""
                '--

                'sarika DM Denormalization for Rx 20090410
                Private _DrugForm As String = ""
                Private _NDCCode As String = ""
                Private _IsNarcotics As Integer = 0

                Private _DrugQtyQualifier As String = ""
                '---


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

                Public Property mpid() As Int32
                    Get
                        Return _mpid
                    End Get
                    Set(ByVal Value As Int32)
                        _mpid = Value
                    End Set
                End Property

                Public Property Dosage() As String
                    Get
                        Return _Dosage
                    End Get
                    Set(ByVal Value As String)
                        _Dosage = Value
                    End Set
                End Property

                Public Property Route() As String
                    Get
                        Return _Route
                    End Get
                    Set(ByVal Value As String)
                        _Route = Value
                    End Set
                End Property

                Public Property Frequency() As String
                    Get
                        Return _Frequency
                    End Get
                    Set(ByVal Value As String)
                        _Frequency = Value
                    End Set
                End Property

                Public Property Duration() As String
                    Get
                        Return _Duration
                    End Get
                    Set(ByVal Value As String)
                        _Duration = Value
                    End Set
                End Property


                'sarika DM Denormalization
                Public Property DrugName() As String
                    Get
                        Return _DrugName
                    End Get
                    Set(ByVal Value As String)
                        _DrugName = Value
                    End Set
                End Property

                'sarika DM Denormalization for Rx 20090410
                Public Property DrugForm() As String
                    Get
                        Return _DrugForm
                    End Get
                    Set(ByVal Value As String)
                        _DrugForm = Value
                    End Set
                End Property


                Public Property NDCCode() As String
                    Get
                        Return _NDCCode
                    End Get
                    Set(ByVal value As String)
                        _NDCCode = value
                    End Set
                End Property

                Public Property IsNarcotics() As Integer
                    Get
                        Return _IsNarcotics
                    End Get
                    Set(ByVal value As Integer)
                        _IsNarcotics = value
                    End Set
                End Property

                Public Property DrugQtyQualifier() As String
                    Get
                        Return _DrugQtyQualifier
                    End Get
                    Set(ByVal value As String)
                        _DrugQtyQualifier = value
                    End Set
                End Property
                '--


                Public Sub New()
                    MyBase.new()
                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub
            End Class

            Public Class Drugs
                Implements System.Collections.IEnumerable
                Private mCol As Collection
                Public Sub Dispose()
                    If (IsNothing(mCol) = False) Then
                        mCol.Clear()
                        mCol = Nothing
                    End If
                End Sub
                Public Function Add(ByRef oDrug As gloStream.DiseaseManagement.Supporting.Drug) As gloStream.DiseaseManagement.Supporting.Drug
                    mCol.Add(oDrug)
                    Return Nothing
                End Function

                Public Function Add(ByVal ID As Long, ByVal Name As String, ByVal Dosage As String, ByVal Route As String, ByVal Frequency As String, ByVal Duration As String) As gloStream.DiseaseManagement.Supporting.Drug
                    Dim objNewMember As gloStream.DiseaseManagement.Supporting.Drug
                    Try
                        objNewMember = New gloStream.DiseaseManagement.Supporting.Drug
                        objNewMember.ID = ID
                        objNewMember.Name = Name
                        objNewMember.Dosage = Dosage
                        objNewMember.Route = Route
                        objNewMember.Frequency = Frequency
                        objNewMember.Duration = Duration

                        mCol.Add(objNewMember)
                        Add = objNewMember
                        objNewMember = Nothing
                    Catch ex As Exception
                        UpdateLog("clsDiseaseManagement -- Drugs -- Add -- " & ex.ToString)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    End Try

                End Function

                'sarika DM Denormalization for Rx 20090410
                Public Function Add(ByVal ID As Long, ByVal Name As String, ByVal Dosage As String, ByVal Route As String, ByVal Frequency As String, ByVal Duration As String, ByVal DrugForm As String, ByVal NDCCode As String, ByVal IsNarcotics As Integer, ByVal DrugQtyQualifier As String) As gloStream.DiseaseManagement.Supporting.Drug
                    Dim objNewMember As gloStream.DiseaseManagement.Supporting.Drug
                    Try
                        objNewMember = New gloStream.DiseaseManagement.Supporting.Drug
                        objNewMember.ID = ID
                        objNewMember.Name = Name
                        objNewMember.Dosage = Dosage
                        objNewMember.Route = Route
                        objNewMember.Frequency = Frequency
                        objNewMember.Duration = Duration

                        objNewMember.NDCCode = NDCCode
                        objNewMember.IsNarcotics = IsNarcotics

                        objNewMember.DrugQtyQualifier = DrugQtyQualifier
                        mCol.Add(objNewMember)
                        Add = objNewMember
                        objNewMember = Nothing
                    Catch ex As Exception
                        UpdateLog("clsDiseaseManagement -- Drugs -- Add -- " & ex.ToString)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    End Try

                End Function
                '---

                Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As gloStream.DiseaseManagement.Supporting.Drug
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


            '//<<<<<<<<<<<<<< ICD9 >>>>>>>>>>>>>>>>>>//

            Public Class ICD9
                Private _ID As Long
                Private _Code As String
                Private _Name As String

                Public Property ID() As Long
                    Get
                        Return _ID
                    End Get
                    Set(ByVal Value As Long)
                        _ID = Value
                    End Set
                End Property

                Public Property Code() As String
                    Get
                        Return _Code
                    End Get
                    Set(ByVal Value As String)
                        _Code = Value
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

                Public Sub New()
                    MyBase.new()
                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub
            End Class

            Public Class ICD9s
                Implements System.Collections.IEnumerable
                Private mCol As Collection
                Public Sub Dispose()
                    If (IsNothing(mCol) = False) Then
                        mCol.Clear()
                        mCol = Nothing
                    End If
                End Sub
                Public Function Add(ByRef oICD9 As gloStream.DiseaseManagement.Supporting.ICD9) As gloStream.DiseaseManagement.Supporting.ICD9
                    mCol.Add(oICD9)
                    Return Nothing
                End Function

                Public Function Add(ByVal ID As Long, ByVal Code As String, ByVal Name As String) As gloStream.DiseaseManagement.Supporting.ICD9
                    Dim objNewMember As gloStream.DiseaseManagement.Supporting.ICD9
                    Try
                        objNewMember = New gloStream.DiseaseManagement.Supporting.ICD9
                        objNewMember.ID = ID
                        objNewMember.Code = Code
                        objNewMember.Name = Name
                        mCol.Add(objNewMember)
                        Add = objNewMember
                        objNewMember = Nothing
                    Catch ex As Exception
                        UpdateLog("clsDiseaseManagement -- ICD9 -- Add -- " & ex.ToString)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    End Try
                End Function

                Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As gloStream.DiseaseManagement.Supporting.ICD9
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


            '//<<<<<<<<<<<<<< CPT >>>>>>>>>>>>>>>>>>//

            Public Class CPT
                Private _ID As Long
                Private _Code As String
                Private _Name As String

                Public Property ID() As Long
                    Get
                        Return _ID
                    End Get
                    Set(ByVal Value As Long)
                        _ID = Value
                    End Set
                End Property

                Public Property Code() As String
                    Get
                        Return _Code
                    End Get
                    Set(ByVal Value As String)
                        _Code = Value
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

                Public Sub New()
                    MyBase.new()
                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub
            End Class

            '---CPT Collection---
            Public Class CPTs
                Implements System.Collections.IEnumerable
                Private mCol As Collection
                Public Sub Dispose()
                    If (IsNothing(mCol) = False) Then
                        mCol.Clear()
                        mCol = Nothing
                    End If
                End Sub
                Public Function Add(ByRef oCPT As gloStream.DiseaseManagement.Supporting.CPT) As gloStream.DiseaseManagement.Supporting.CPT
                    mCol.Add(oCPT)
                    Return Nothing
                End Function

                Public Function Add(ByVal ID As Long, ByVal Code As String, ByVal Name As String) As gloStream.DiseaseManagement.Supporting.CPT
                    Dim objNewMember As gloStream.DiseaseManagement.Supporting.CPT
                    Try
                        objNewMember = New gloStream.DiseaseManagement.Supporting.CPT
                        objNewMember.ID = ID
                        objNewMember.Code = Code
                        objNewMember.Name = Name
                        mCol.Add(objNewMember)
                        Add = objNewMember
                        objNewMember = Nothing
                    Catch ex As Exception
                        UpdateLog("clsDiseaseManagement -- CPTs -- Add -- " & ex.ToString)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    End Try

                End Function

                Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As gloStream.DiseaseManagement.Supporting.CPT
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

            '//<<<<<<<<<<<<<< ORDER >>>>>>>>>>>>>>>>>>//

            Public Class Order
                Private _ID As Long
                Private _Category As String
                Private _LabGroups As gloStream.DiseaseManagement.Supporting.OrderGroups
                Private bAssigned As Boolean = True
                Public Sub Dispose()
                    If (bAssigned) Then
                        _LabGroups.Dispose()
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

                Public Property Category() As String
                    Get
                        Return _Category
                    End Get
                    Set(ByVal Value As String)
                        _Category = Value
                    End Set
                End Property

                Public Property OrderGroups() As gloStream.DiseaseManagement.Supporting.OrderGroups
                    Get
                        Return _LabGroups
                    End Get
                    Set(ByVal Value As gloStream.DiseaseManagement.Supporting.OrderGroups)
                        If (bAssigned) Then
                            _LabGroups.Dispose()
                            bAssigned = False
                        End If
                        _LabGroups = Value
                    End Set
                End Property

                Public Sub New()
                    MyBase.new()
                    _LabGroups = New gloStream.DiseaseManagement.Supporting.OrderGroups
                    bAssigned = True
                End Sub

                Protected Overrides Sub Finalize()
                    _LabGroups = Nothing
                    MyBase.Finalize()
                End Sub

            End Class

            '--- ORDER Collection---
            Public Class Orders
                Implements System.Collections.IEnumerable
                Private mCol As Collection
                Public Sub Dispose()
                    If (IsNothing(mCol) = False) Then
                        mCol.Clear()
                        mCol = Nothing
                    End If
                End Sub
                Public Function Add(ByRef oLab As gloStream.DiseaseManagement.Supporting.Order) As gloStream.DiseaseManagement.Supporting.Order
                    mCol.Add(oLab)
                    Return Nothing
                End Function

                Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As gloStream.DiseaseManagement.Supporting.Order
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

            '---ORDER Group Class with Tests Collection---
            Public Class OrderGroup
                Private _ID As Long
                Private _Name As String
                Private _Tests As gloStream.DiseaseManagement.Supporting.ItemDetails
                Private bAssigned As Boolean = False
                Public Sub Dispose()
                    If (bAssigned) Then
                        _Tests.Dispose()
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

                Public Property Tests() As gloStream.DiseaseManagement.Supporting.ItemDetails
                    Get
                        Return _Tests
                    End Get
                    Set(ByVal Value As gloStream.DiseaseManagement.Supporting.ItemDetails)
                        If (bAssigned) Then
                            _Tests.Dispose()
                            bAssigned = False
                        End If
                        _Tests = Value
                    End Set
                End Property

                Public Sub New()
                    MyBase.new()
                    _Tests = New gloStream.DiseaseManagement.Supporting.ItemDetails
                    bAssigned = True
                End Sub

                Protected Overrides Sub Finalize()
                    _Tests = Nothing
                    MyBase.Finalize()
                End Sub

            End Class

            '---ORDER Group collection Class with Tests Collection---
            Public Class OrderGroups
                Implements System.Collections.IEnumerable
                Private mCol As Collection
                Public Sub Dispose()
                    If (IsNothing(mCol) = False) Then
                        mCol.Clear()
                        mCol = Nothing
                    End If
                End Sub
                Public Function Add(ByRef oLabGroup As gloStream.DiseaseManagement.Supporting.OrderGroup) As gloStream.DiseaseManagement.Supporting.OrderGroup
                    mCol.Add(oLabGroup)
                    Return Nothing
                End Function

                ''Public Function Add(ByVal oID As Long, ByVal oDescription As String) As gloStream.DiseaseManagement.Supporting.ItemDetail
                ''    'create a new object
                ''    Dim objNewMember As gloStream.DiseaseManagement.Supporting.ItemDetail
                ''    objNewMember = New gloStream.DiseaseManagement.Supporting.ItemDetail
                ''    objNewMember.ID = oID
                ''    objNewMember.Description = oDescription
                ''    mCol.Add(objNewMember)
                ''    Add = objNewMember
                ''    objNewMember = Nothing
                ''End Function

                Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As gloStream.DiseaseManagement.Supporting.OrderGroup
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


            '//<<<<<<<<<<<<<< NEW LAB MODULE TEST RESULT >>>>>>>>>>>>>>>>>>//

            Public Enum enumTestModuleResultType
                None = 0
                SingleResult = 1
                ProfileResult = 2
            End Enum

            Public Enum enumTestModuleResultValueType
                None = 0
                Text = 1
                Numeric = 2
            End Enum

            Public Enum enumTestModuleResultReadType
                None = 0
                Prilimnary = 1
                Final = 2
                Ammend = 3
            End Enum

            Public Class LabModuleTest
                Private _TestID As Int64 = 0
                Private _Code As String = ""
                Private _Name As String = ""
                Private _ResultType As enumTestModuleResultType = enumTestModuleResultType.None
                Private _LabModuleTestResults As LabModuleTestResults
                Private bAssigned As Boolean = False
                Public Sub Dispose()
                    If (bAssigned) Then
                        _LabModuleTestResults.Dispose()
                        bAssigned = False
                    End If
                End Sub
                Public Property TestID() As Int64
                    Get
                        Return _TestID
                    End Get
                    Set(ByVal value As Int64)
                        _TestID = value
                    End Set
                End Property

                Public Property Code() As String
                    Get
                        Return _Code
                    End Get
                    Set(ByVal value As String)
                        _Code = value
                    End Set
                End Property

                Public Property Name() As String
                    Get
                        Return _Name
                    End Get
                    Set(ByVal value As String)
                        _Name = value
                    End Set
                End Property

                Public Property ResultType() As enumTestModuleResultType
                    Get
                        Return _ResultType
                    End Get
                    Set(ByVal value As enumTestModuleResultType)
                        _ResultType = value
                    End Set
                End Property

                Public Property LabModuleTestResults() As LabModuleTestResults
                    Get
                        Return _LabModuleTestResults
                    End Get
                    Set(ByVal value As LabModuleTestResults)
                        If (bAssigned) Then
                            _LabModuleTestResults.Dispose()
                            bAssigned = False
                        End If
                        _LabModuleTestResults = value
                    End Set
                End Property


                Public Sub New()
                    MyBase.New()
                    _LabModuleTestResults = New LabModuleTestResults
                    bAssigned = True
                End Sub

                Protected Overrides Sub Finalize()
                    _LabModuleTestResults = Nothing
                    MyBase.Finalize()
                End Sub
            End Class

            Public Class LabModuleTests
                Implements System.Collections.IEnumerable
                Private mCol As Collection
                Public Sub Dispose()
                    If (IsNothing(mCol) = False) Then
                        mCol.Clear()
                        mCol = Nothing
                    End If
                End Sub
                Public Function Add(ByRef oItemDetail As gloStream.DiseaseManagement.Supporting.LabModuleTest) As gloStream.DiseaseManagement.Supporting.LabModuleTest
                    mCol.Add(oItemDetail)
                    Return Nothing
                End Function

                Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As gloStream.DiseaseManagement.Supporting.LabModuleTest
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

            Public Class LabModuleTestResult
                Private _TestID As Int64 = 0
                Private _ResultID As Int64 = 0
                Private _ResultName As String = ""
                Private _ValueType As enumTestModuleResultValueType = enumTestModuleResultValueType.None
                Private _Unit As String = ""
                Private _Operators As String
                Private _ResultValue1 As String
                Private _ResultValue2 As String

                Public Property TestID() As Int64
                    Get
                        Return _TestID
                    End Get
                    Set(ByVal value As Int64)
                        _TestID = value
                    End Set
                End Property

                Public Property ResultID() As Int64
                    Get
                        Return _ResultID
                    End Get
                    Set(ByVal value As Int64)
                        _ResultID = value
                    End Set
                End Property

                Public Property ResultName() As String
                    Get
                        Return _ResultName
                    End Get
                    Set(ByVal value As String)
                        _ResultName = value
                    End Set
                End Property

                Public Property ValueType() As enumTestModuleResultValueType
                    Get
                        Return _ValueType
                    End Get
                    Set(ByVal value As enumTestModuleResultValueType)
                        _ValueType = value
                    End Set
                End Property

                Public Property Unit() As String
                    Get
                        Return _Unit
                    End Get
                    Set(ByVal value As String)
                        _Unit = value
                    End Set
                End Property

                Public Property Operators() As String
                    Get
                        Return _Operators
                    End Get
                    Set(ByVal value As String)
                        _Operators = value
                    End Set
                End Property

                Public Property ResultValue1() As String
                    Get
                        Return _ResultValue1
                    End Get
                    Set(ByVal value As String)
                        _ResultValue1 = value
                    End Set
                End Property

                Public Property ResultValue2() As String
                    Get
                        Return _ResultValue2
                    End Get
                    Set(ByVal value As String)
                        _ResultValue2 = value
                    End Set
                End Property

                Public Sub New()
                    MyBase.New()
                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub
            End Class

            Public Class LabModuleTestResults
                Implements System.Collections.IEnumerable
                Private mCol As Collection
                Public Sub Dispose()
                    If (IsNothing(mCol) = False) Then
                        mCol.Clear()
                        mCol = Nothing
                    End If
                End Sub
                Public Function Add(ByRef oItemDetail As gloStream.DiseaseManagement.Supporting.LabModuleTestResult) As gloStream.DiseaseManagement.Supporting.LabModuleTestResult
                    mCol.Add(oItemDetail)
                    Return Nothing
                End Function

                Public Function Add(ByVal oTestID As Int64, ByVal oResultID As Int64, ByVal oResultName As String, ByVal oValueType As enumTestModuleResultValueType, ByVal oUnit As String, ByVal oOperators As String, ByVal oResultValue1 As String, ByVal oResultValue2 As String) As gloStream.DiseaseManagement.Supporting.LabModuleTestResult
                    'create a new object
                    Dim objNewMember As gloStream.DiseaseManagement.Supporting.LabModuleTestResult
                    Try
                        objNewMember = New gloStream.DiseaseManagement.Supporting.LabModuleTestResult
                        With objNewMember
                            .TestID = oTestID
                            .ResultID = oResultID
                            .ResultName = oResultName
                            .ValueType = oValueType
                            .Unit = oUnit
                            .Operators = oOperators
                            .ResultValue1 = oResultValue1
                            .ResultValue2 = oResultValue2
                        End With
                        mCol.Add(objNewMember)
                        Add = objNewMember
                        objNewMember = Nothing
                    Catch ex As Exception
                        UpdateLog("clsDiseaseManagement -- LabModuleTestResults -- Add -- " & ex.ToString)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    End Try

                End Function

                Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As gloStream.DiseaseManagement.Supporting.LabModuleTestResult
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

            '//<<<<<<<<<<<<<< COMMON ITEM ID AND NAME >>>>>>>>>>>>>>>>>>//

            Public Class ItemDetail
                Private _ItemID As Long
                Private _ItemDescription As String

                'sarika DM Denormalization
                Private _Template As Byte() = Nothing
                '----

                Public Property ID() As Long
                    Get
                        Return _ItemID
                    End Get
                    Set(ByVal Value As Long)
                        _ItemID = Value
                    End Set
                End Property

                Public Property Description() As String
                    Get
                        Return _ItemDescription
                    End Get
                    Set(ByVal Value As String)
                        _ItemDescription = Value
                    End Set
                End Property

                'sarika DM Denormalization
                Public Property Template() As Byte()
                    Get
                        Return _Template
                    End Get
                    Set(ByVal value As Byte())
                        _Template = value
                    End Set
                End Property
                '----

                Public Sub New()
                    MyBase.new()
                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub
            End Class

            Public Class ItemDetails
                Implements System.Collections.IEnumerable
                Private mCol As Collection
                Public Sub Dispose()
                    If (IsNothing(mCol) = False) Then
                        mCol.Clear()
                        mCol = Nothing
                    End If
                End Sub
                Public Function Add(ByRef oItemDetail As gloStream.DiseaseManagement.Supporting.ItemDetail) As gloStream.DiseaseManagement.Supporting.ItemDetail
                    mCol.Add(oItemDetail)
                    Return Nothing
                End Function

                Public Function Add(ByVal oID As Long, ByVal oDescription As String) As gloStream.DiseaseManagement.Supporting.ItemDetail
                    'create a new object
                    Dim objNewMember As gloStream.DiseaseManagement.Supporting.ItemDetail
                    Try
                        objNewMember = New gloStream.DiseaseManagement.Supporting.ItemDetail
                        objNewMember.ID = oID
                        objNewMember.Description = oDescription
                        mCol.Add(objNewMember)
                        Add = objNewMember
                        objNewMember = Nothing
                    Catch ex As Exception
                        UpdateLog("clsDiseaseManagement -- ItemDetails -- Add -- " & ex.ToString)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    End Try
                End Function

                'sarika DM Denormalization

                Public Function Add(ByVal oID As Long, ByVal oDescription As String, ByVal oTemplate As Byte()) As gloStream.DiseaseManagement.Supporting.ItemDetail
                    'create a new object
                    Dim objNewMember As gloStream.DiseaseManagement.Supporting.ItemDetail
                    Try
                        objNewMember = New gloStream.DiseaseManagement.Supporting.ItemDetail
                        objNewMember.ID = oID
                        objNewMember.Description = oDescription
                        objNewMember.Template = oTemplate
                        mCol.Add(objNewMember)
                        Add = objNewMember
                        objNewMember = Nothing
                    Catch ex As Exception
                        UpdateLog("clsDiseaseManagement -- ItemDetails -- Add -- " & ex.ToString)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    End Try
                End Function
                '-----

                Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As gloStream.DiseaseManagement.Supporting.ItemDetail
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
                Private _AgeInDays_Minimum As Int32 = 0
                Private _AgeInDays_Maximum As Int32 = 0
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
                Private _bisActive As Boolean = False
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

                Private _BPSitting_ToMinimum As Double = 0
                Private _BPSitting_ToMaximum As Double = 0
                Private _BPStanding_ToMinimum As Double = 0
                Private _BPStanding_ToMaximum As Double = 0

                Private _DisplayMessage As String = ""
                Private _bIsTriggerObject As Boolean = True
                Private _bIsOldRule As Boolean = False
                Private _bIsRecuringRule As Boolean = False
                Private _dtRecurrenceStartDate As Date = Date.MinValue
                Private _dtRecurrenceEndDate As Date = Date.MinValue
                Private _nDuratiotype As Integer = -1
                Private _nDuratioPeriod As Integer = 0
                Private _sBibliographicCitatation As String = ""
                Private _sInterventionDeveloper As String = ""
                Private _sFundingSource As String = ""
                Private _sRelease As String = ""
                Private _sRevisionDates As String = ""


                Private _OtherDetails As Supporting.OtherDetails
                Private _Histories As Collection
                Private _Drugs As Collection
                Private _ICD9s As Collection
                Private _CPTs As Collection
                Private _Labs As gloStream.DiseaseManagement.Supporting.Criteria_Labs
                'Here we are not creating seprate class for criteria of Lab Module, bcz its already cover in selection class with name LabModuleTestResults
                Private _LabModuleTests As gloStream.DiseaseManagement.Supporting.LabModuleTestResults

                Private _Guidelines As Collection
                Private _LabOrders As Collection
                Private _RadiologyOrders As Collection
                Private _RxDrugs As Collection
                Private _Referrals As Collection

                '''''''''Added by Chetan on 09 Oct 2010 - for IM in DM Setup
                Private _IMlst As Collection
                '''''''''Added by Chetan on 09 Oct 2010 - for IM in DM Setup


                Private bOtherDetails As Boolean = False
                Private bHistories As Boolean = False
                Private bDrugs As Boolean = False
                Private bICD9s As Boolean = False
                Private bCPTs As Boolean = False
                Private bLabs As Boolean = False
                'Here we are not creating seprate class for criteria of Lab Module, bcz its already cover in selection class with name LabModuleTestResults
                Private bLabModuleTests As Boolean = False

                Private bGuidelines As Boolean = False
                Private bLabOrders As Boolean = False
                Private bRadiologyOrders As Boolean = False
                Private bRxDrugs As Boolean = False
                Private bReferrals As Boolean = False

                '''''''''Added by Chetan on 09 Oct 2010 - for IM in DM Setup
                Private bIMlst As Boolean = False
                '''''''''Added by Chetan on 09 Oct 2010 - for IM in DM Setup


                'Added by Sameer On 19 Sept 2013 for Special Alert CheckBox in DM Rule Setup 
                Private _bIsSpecialAlert As Boolean = False

                Public Sub Dispose()
                    If (bIMlst) Then
                        _IMlst.Clear()
                        bIMlst = False
                    End If
                    If (bReferrals) Then
                        _Referrals.Clear()
                        bReferrals = False
                    End If
                    If (bRxDrugs) Then
                        _RxDrugs.Clear()
                        bRxDrugs = False

                    End If
                    If (bRadiologyOrders) Then
                        _RadiologyOrders.Clear()
                        bRadiologyOrders = False
                    End If
                    If (bLabOrders) Then
                        _LabOrders.Clear()
                        bLabOrders = False
                    End If
                    If (bGuidelines) Then
                        _Guidelines.Clear()
                        bGuidelines = False
                    End If
                    If (bLabModuleTests) Then
                        _LabModuleTests.Dispose()
                        bLabModuleTests = False
                    End If
                    If (bLabs) Then
                        _Labs.Dispose()
                        bLabs = False
                    End If
                    If (bCPTs) Then
                        _CPTs.Clear()
                        bCPTs = False
                    End If
                    If (bICD9s) Then
                        _ICD9s.Clear()
                        bICD9s = False
                    End If
                    If (bDrugs) Then
                        _Drugs.Clear()
                        bDrugs = False
                    End If
                    If (bHistories) Then
                        _Histories.Clear()
                        bHistories = False
                    End If
                    If (bOtherDetails) Then
                        _OtherDetails.Dispose()
                        bOtherDetails = False
                    End If

                End Sub

                Public Property sBibliographicCitatation() As String
                    Get
                        Return _sBibliographicCitatation
                    End Get
                    Set(ByVal Value As String)
                        _sBibliographicCitatation = Value
                    End Set
                End Property

                Public Property sInterventionDeveloper() As String
                    Get
                        Return _sInterventionDeveloper
                    End Get
                    Set(ByVal Value As String)
                        _sInterventionDeveloper = Value
                    End Set
                End Property

                Public Property sFundingSource() As String
                    Get
                        Return _sFundingSource
                    End Get
                    Set(ByVal Value As String)
                        _sFundingSource = Value
                    End Set
                End Property

                Public Property sRelease() As String
                    Get
                        Return _sRelease
                    End Get
                    Set(ByVal Value As String)
                        _sRelease = Value
                    End Set
                End Property
                Public Property sRevisionDates() As String
                    Get
                        Return _sRevisionDates
                    End Get
                    Set(ByVal Value As String)
                        _sRevisionDates = Value
                    End Set
                End Property

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

                Public Property IsTriggerObject As Boolean
                    Get
                        Return _bIsTriggerObject
                    End Get
                    Set(ByVal Value As Boolean)
                        _bIsTriggerObject = Value
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
                Public Property AgeInDaysMinimum() As Int32
                    Get
                        Return _AgeInDays_Minimum
                    End Get
                    Set(ByVal Value As Int32)
                        _AgeInDays_Minimum = Value
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
                Public Property AgeInDaysMaximum() As Int32
                    Get
                        Return _AgeInDays_Maximum
                    End Get
                    Set(ByVal Value As Int32)
                        _AgeInDays_Maximum = Value
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

                Public Property IsActive() As Boolean
                    Get
                        Return _bisActive
                    End Get
                    Set(ByVal Value As Boolean)
                        _bisActive = Value
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

                Public Property BPSittingToMinimum() As Double
                    Get
                        Return _BPSitting_ToMinimum
                    End Get
                    Set(ByVal Value As Double)
                        _BPSitting_ToMinimum = Value
                    End Set
                End Property

                Public Property BPSittingToMaximum() As Double
                    Get
                        Return _BPSitting_ToMaximum
                    End Get
                    Set(ByVal Value As Double)
                        _BPSitting_ToMaximum = Value
                    End Set
                End Property

                Public Property BPStandingToMinimum() As Double
                    Get
                        Return _BPStanding_ToMinimum
                    End Get
                    Set(ByVal Value As Double)
                        _BPStanding_ToMinimum = Value
                    End Set
                End Property

                Public Property BPStandingToMaximum() As Double
                    Get
                        Return _BPStanding_ToMaximum
                    End Get
                    Set(ByVal Value As Double)
                        _BPStanding_ToMaximum = Value
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

                Public Property OtherDetails() As Supporting.OtherDetails
                    Get
                        Return _OtherDetails
                    End Get
                    Set(ByVal value As Supporting.OtherDetails)
                        If (bOtherDetails) Then
                            _OtherDetails.Dispose()
                            bOtherDetails = False
                        End If
                        _OtherDetails = value
                    End Set
                End Property

                Public Property Histories() As Collection
                    Get
                        Return _Histories
                    End Get
                    Set(ByVal Value As Collection)
                        If (bHistories) Then
                            _Histories.Clear()
                            bHistories = False
                        End If
                        _Histories = Value
                    End Set
                End Property

                Public Property Drugs() As Collection
                    Get
                        Return _Drugs
                    End Get
                    Set(ByVal Value As Collection)
                        If (bDrugs) Then
                            _Drugs.Clear()
                            bDrugs = False
                        End If
                        _Drugs = Value
                    End Set
                End Property

                Public Property ICD9s() As Collection
                    Get
                        Return _ICD9s
                    End Get
                    Set(ByVal Value As Collection)
                        If (bICD9s) Then
                            _ICD9s.Clear()
                            bICD9s = False
                        End If
                        _ICD9s = Value
                    End Set
                End Property

                Public Property CPTs() As Collection
                    Get
                        Return _CPTs
                    End Get
                    Set(ByVal Value As Collection)
                        If (bCPTs) Then
                            _CPTs.Clear()
                            bCPTs = False
                        End If
                        _CPTs = Value
                    End Set
                End Property

                Public Property Labs() As gloStream.DiseaseManagement.Supporting.Criteria_Labs
                    Get
                        Return _Labs
                    End Get
                    Set(ByVal Value As gloStream.DiseaseManagement.Supporting.Criteria_Labs)
                        If (bLabs) Then
                            _Labs.Dispose()
                            bLabs = False
                        End If
                        _Labs = Value
                    End Set
                End Property

                Public Property LabModuleTests() As gloStream.DiseaseManagement.Supporting.LabModuleTestResults
                    Get
                        Return _LabModuleTests
                    End Get
                    Set(ByVal Value As gloStream.DiseaseManagement.Supporting.LabModuleTestResults)
                        If (bLabModuleTests) Then
                            _LabModuleTests.Dispose()
                            bLabModuleTests = False
                        End If
                        _LabModuleTests = Value
                    End Set
                End Property

                Public Property Guidelines() As Collection
                    Get
                        Return _Guidelines
                    End Get
                    Set(ByVal Value As Collection)
                        If (bGuidelines) Then
                            _Guidelines.Clear()
                            bGuidelines = False
                        End If
                        _Guidelines = Value
                    End Set
                End Property

                Public Property LabOrders() As Collection
                    Get
                        Return _LabOrders
                    End Get
                    Set(ByVal Value As Collection)
                        If (bLabOrders) Then
                            _LabOrders.Clear()
                            bLabOrders = False
                        End If
                        _LabOrders = Value
                    End Set
                End Property
                Public Property RadiologyOrders() As Collection
                    Get
                        Return _RadiologyOrders
                    End Get
                    Set(ByVal Value As Collection)
                        If (bRadiologyOrders) Then
                            _RadiologyOrders.Clear()
                            bRadiologyOrders = False
                        End If
                        _RadiologyOrders = Value
                    End Set
                End Property
                Public Property RxDrugs() As Collection
                    Get
                        Return _RxDrugs
                    End Get
                    Set(ByVal Value As Collection)
                        If (bRxDrugs) Then
                            _RxDrugs.Clear()
                            bRxDrugs = False

                        End If
                        _RxDrugs = Value
                    End Set
                End Property
                Public Property Referrals() As Collection
                    Get
                        Return _Referrals
                    End Get
                    Set(ByVal Value As Collection)
                        If (bReferrals) Then
                            _Referrals.Clear()
                            bReferrals = False
                        End If
                        _Referrals = Value
                    End Set
                End Property
                Public Property IMlst() As Collection
                    '''''''''Added by Ujwala Atre as on 20100907 - for IM in DM Setup
                    Get
                        Return _IMlst
                    End Get
                    Set(ByVal Value As Collection)
                        If (bIMlst) Then
                            _IMlst.Clear()
                            bIMlst = False
                        End If
                        _IMlst = Value
                    End Set
                    '''''''''Added by Ujwala Atre as on 20100907 - for IM in DM Setup
                End Property

                Public Property bIsOldRule() As Boolean

                    Get
                        Return _bIsOldRule
                    End Get
                    Set(ByVal Value As Boolean)
                        _bIsOldRule = Value
                    End Set

                End Property
                Public Property bIsRecuringRule() As Boolean

                    Get
                        Return _bIsRecuringRule
                    End Get
                    Set(ByVal Value As Boolean)
                        _bIsRecuringRule = Value
                    End Set

                End Property

                Public Property dtRecurrenceStartDate() As Date

                    Get
                        Return _dtRecurrenceStartDate
                    End Get
                    Set(ByVal Value As Date)
                        _dtRecurrenceStartDate = Value
                    End Set

                End Property

                Public Property dtRecurrenceEndDate() As Date

                    Get
                        Return _dtRecurrenceEndDate
                    End Get
                    Set(ByVal Value As Date)
                        _dtRecurrenceEndDate = Value
                    End Set

                End Property

                Public Property nDuratiotype() As Integer

                    Get
                        Return _nDuratiotype
                    End Get
                    Set(ByVal Value As Integer)
                        _nDuratiotype = Value
                    End Set

                End Property

                Public Property nDuratioPeriod() As Integer

                    Get
                        Return _nDuratioPeriod
                    End Get
                    Set(ByVal Value As Integer)
                        _nDuratioPeriod = Value
                    End Set

                End Property

                'Added by Sameer On 19 Sept 2013 for Special Alert CheckBox in DM Rule Setup 
                Public Property bIsSpecialAlert() As Boolean
                    Get
                        Return _bIsSpecialAlert
                    End Get
                    Set(ByVal value As Boolean)
                        _bIsSpecialAlert = value
                    End Set
                End Property

                Public Sub New()
                    MyBase.new()
                    _Histories = New Collection
                    bHistories = True
                    _Drugs = New Collection
                    bDrugs = True
                    _ICD9s = New Collection
                    bICD9s = True
                    _CPTs = New Collection
                    bCPTs = True
                    _Guidelines = New Collection
                    bGuidelines = True
                    _LabOrders = New Collection
                    bLabOrders = True
                    _RadiologyOrders = New Collection
                    bRadiologyOrders = True
                    _RxDrugs = New Collection
                    bRxDrugs = True
                    _Referrals = New Collection
                    bReferrals = True
                    _Labs = New gloStream.DiseaseManagement.Supporting.Criteria_Labs
                    bLabs = True
                    _Guidelines = New Collection
                    bGuidelines = True
                    _LabModuleTests = New gloStream.DiseaseManagement.Supporting.LabModuleTestResults
                    bLabModuleTests = True
                    _IMlst = New Collection
                    bIMlst = True
                End Sub

                Protected Overrides Sub Finalize()
                    _Histories = Nothing
                    _Drugs = Nothing
                    _ICD9s = Nothing
                    _CPTs = Nothing
                    _Labs = Nothing
                    _Guidelines = Nothing
                    _LabModuleTests = Nothing
                    _Guidelines = Nothing
                    _LabOrders = Nothing
                    _RadiologyOrders = Nothing
                    _RxDrugs = Nothing
                    _Referrals = Nothing
                    _IMlst = Nothing
                    MyBase.Finalize()
                End Sub
            End Class

            Public Class PatientDetail

                Private _CriteriaID As Int64 = 0
                Private _CriteriaName As String = ""
                Private _PatientID As Long = 0
                Private _PatientCode As String = ""
                Private _PatientName As String = ""
                Private _Age As Double = 0
                Private _AgeInDays As Int32 = 0
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
                Public Property AgeInDays() As Int32
                    Get
                        Return _AgeInDays
                    End Get
                    Set(ByVal Value As Int32)
                        _AgeInDays = Value
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

            Public Enum enumDetailType
                None = 0
                History = 1
                Medication = 2
                ICD9 = 3
                CPT = 4
                Lab = 5
                Order = 6
                Problemlist = 7
                Race = 8
                Gender = 9
                MaritalStatus = 10
                SnomedCode = 11
                ICD10 = 12
                Insurance = 14
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
                Private mLionicCode As String = ""
                Private mDetailType As enumDetailType

                Public Property LionicCode() As String
                    Get
                        Return mLionicCode
                    End Get
                    Set(ByVal Value As String)
                        mLionicCode = Value
                    End Set
                End Property
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
                Public Function Add(ByRef oOtherDetail As gloStream.DiseaseManagement.Supporting.OtherDetail) As gloStream.DiseaseManagement.Supporting.OtherDetail
                    mCol.Add(oOtherDetail)
                    Return Nothing
                End Function

                Public Function Add(ByVal ItemID As Int64, ByVal CategoryID As Int64, ByVal CategoryName As String, ByVal ItemName As String, ByVal OperatorName As String, ByVal Result1 As String, ByVal Result2 As String) As gloStream.DiseaseManagement.Supporting.OtherDetail
                    'create a new object
                    Dim oOtherDetail As gloStream.DiseaseManagement.Supporting.OtherDetail
                    Try
                        oOtherDetail = New gloStream.DiseaseManagement.Supporting.OtherDetail
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
                        UpdateLog("clsCardioVascular -- ItemDetails -- Add -- " & ex.ToString)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    End Try
                End Function

                Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As gloStream.DiseaseManagement.Supporting.OtherDetail
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

            Public Class Criteria_Lab
                Private _GroupID As Long
                Private _TestID As Long
                Private _NumericMinimumResult As Double
                Private _NumericMaximumResult As Double

                Public Property GroupID() As Long
                    Get
                        Return _GroupID
                    End Get
                    Set(ByVal Value As Long)
                        _GroupID = Value
                    End Set
                End Property

                Public Property TestID() As Long
                    Get
                        Return _TestID
                    End Get
                    Set(ByVal Value As Long)
                        _TestID = Value
                    End Set
                End Property

                Public Property NumericMinimumResult() As Double
                    Get
                        Return _NumericMinimumResult
                    End Get
                    Set(ByVal Value As Double)
                        _NumericMinimumResult = Value
                    End Set
                End Property


                Public Property NumericMaximumResult() As Double
                    Get
                        Return _NumericMaximumResult
                    End Get
                    Set(ByVal Value As Double)
                        _NumericMaximumResult = Value
                    End Set
                End Property

                Public Sub New()
                    MyBase.new()
                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub
            End Class

            Public Class Criteria_Labs
                Implements System.Collections.IEnumerable
                Private mCol As Collection
                Public Sub Dispose()
                    If (IsNothing(mCol) = False) Then
                        mCol.Clear()
                        mCol = Nothing
                    End If
                End Sub
                Public Function Add(ByVal oGroupID As Long, ByVal oTestID As Long, ByVal oNumericMinResult As Double, ByVal oNumericMaxResult As Double) As gloStream.DiseaseManagement.Supporting.Criteria_Lab
                    'create a new object
                    Try
                        Dim objNewMember As gloStream.DiseaseManagement.Supporting.Criteria_Lab
                        objNewMember = New gloStream.DiseaseManagement.Supporting.Criteria_Lab
                        objNewMember.GroupID = oGroupID
                        objNewMember.TestID = oTestID
                        objNewMember.NumericMinimumResult = oNumericMinResult
                        objNewMember.NumericMaximumResult = oNumericMaxResult
                        mCol.Add(objNewMember)
                        Add = objNewMember
                        objNewMember = Nothing
                    Catch ex As Exception
                        UpdateLog("clsDiseaseManagement -- Criteria_Labs -- Add -- " & ex.ToString)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    End Try
                End Function

                Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As gloStream.DiseaseManagement.Supporting.Criteria_Lab
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


            '//<<<<<<<<<<<<<<<< FIND LAB MODULE ITEMS FOR PATIENTS >>>>>>>>>>>>>>//
            Public Class LabModulePatientDetail
                Private _OrderID As Long
                Private _TestID As Long
                Private _ResultNameID As Long
                Private _ResultValue As String

                Public Property OrderID() As Long
                    Get
                        Return _OrderID
                    End Get
                    Set(ByVal Value As Long)
                        _OrderID = Value
                    End Set
                End Property

                Public Property TestID() As Long
                    Get
                        Return _TestID
                    End Get
                    Set(ByVal Value As Long)
                        _TestID = Value
                    End Set
                End Property

                Public Property ResultNameID() As Long
                    Get
                        Return _ResultNameID
                    End Get
                    Set(ByVal Value As Long)
                        _ResultNameID = Value
                    End Set
                End Property

                Public Property ResultValue() As String
                    Get
                        Return _ResultValue
                    End Get
                    Set(ByVal Value As String)
                        _ResultValue = Value
                    End Set
                End Property

                Public Sub New()
                    MyBase.new()
                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub
            End Class

            Public Class LabModulePatientDetails
                Implements System.Collections.IEnumerable
                Private mCol As Collection
                Public Sub Dispose()
                    If (IsNothing(mCol) = False) Then
                        mCol.Clear()
                        mCol = Nothing
                    End If
                End Sub
                Public Function Add(ByRef oItemDetail As gloStream.DiseaseManagement.Supporting.LabModulePatientDetail) As gloStream.DiseaseManagement.Supporting.LabModulePatientDetail
                    mCol.Add(oItemDetail)
                    Return Nothing
                End Function

                Public Function Add(ByVal oOrderID As Long, ByVal oTestID As Long, ByVal oResultNameID As Long, ByVal oResultValue As String) As gloStream.DiseaseManagement.Supporting.LabModulePatientDetail
                    'create a new object
                    Try
                        Dim objNewMember As gloStream.DiseaseManagement.Supporting.LabModulePatientDetail
                        objNewMember = New gloStream.DiseaseManagement.Supporting.LabModulePatientDetail
                        objNewMember.OrderID = oOrderID
                        objNewMember.TestID = oTestID
                        objNewMember.ResultNameID = oResultNameID
                        objNewMember.ResultValue = oResultValue
                        mCol.Add(objNewMember)
                        Add = objNewMember
                        objNewMember = Nothing
                    Catch ex As Exception
                        UpdateLog("clsDiseaseManagement -- LabModulePatientDetails -- Add -- " & ex.ToString)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    End Try
                End Function

                Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As gloStream.DiseaseManagement.Supporting.LabModulePatientDetail
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

            Public Class TriggerDetails
                ' Private _CriteriaId As Int64
                Private _TransId As Int64
                Private _TriggerId As Int64
                Private _Recurring As Boolean
                Private _Reason As String
                Private _Notes As String
                Private _StartDate As DateTime
                Private _EndDate As DateTime
                Private _DurationType As String
                Private _DurationPeriod As Int32
                Private _DueType As String
                Private _DueValue As String
                Private _OnEveryCheckIn As Boolean

                Public Sub New()
                    MyBase.New()
                End Sub
                'Public Property CriteriaId() As Int64
                '    Get
                '        Return _CriteriaId
                '    End Get
                '    Set(ByVal Value As Int64)
                '        _CriteriaId = Value
                '    End Set
                'End Property

                Public Property TransId() As Int64
                    Get
                        Return _TransId
                    End Get
                    Set(ByVal Value As Int64)
                        _TransId = Value
                    End Set
                End Property

                Public Property TriggerId() As Int64
                    Get
                        Return _TriggerId
                    End Get
                    Set(ByVal Value As Int64)
                        _TriggerId = Value
                    End Set
                End Property

                Public Property DurationType() As String
                    Get
                        Return _DurationType
                    End Get
                    Set(ByVal Value As String)
                        _DurationType = Value
                    End Set
                End Property

                Public Property DurationPeriod() As Int32
                    Get
                        Return _DurationPeriod
                    End Get
                    Set(ByVal Value As Int32)
                        _DurationPeriod = Value
                    End Set
                End Property

                Public Property Recurring() As Boolean
                    Get
                        Return _Recurring
                    End Get
                    Set(ByVal Value As Boolean)
                        _Recurring = Value
                    End Set
                End Property

                Public Property Reason() As String
                    Get
                        Return _Reason
                    End Get
                    Set(ByVal Value As String)
                        _Reason = Value
                    End Set
                End Property

                Public Property Notes() As String
                    Get
                        Return _Notes
                    End Get
                    Set(ByVal Value As String)
                        _Notes = Value
                    End Set
                End Property

                Public Property DueType() As String
                    Get
                        Return _DueType
                    End Get
                    Set(ByVal Value As String)
                        _DueType = Value
                    End Set
                End Property

                Public Property DueValue() As String
                    Get
                        Return _DueValue
                    End Get
                    Set(ByVal Value As String)
                        _DueValue = Value
                    End Set
                End Property
                Public Property StartDate() As DateTime
                    Get
                        Return _StartDate
                    End Get
                    Set(ByVal Value As DateTime)
                        _StartDate = Value
                    End Set
                End Property
                Public Property EndDate() As DateTime
                    Get
                        Return _EndDate
                    End Get
                    Set(ByVal Value As DateTime)
                        _EndDate = Value
                    End Set
                End Property
                Public Property OnEveryCheckIn() As Boolean
                    Get
                        Return _OnEveryCheckIn
                    End Get
                    Set(ByVal Value As Boolean)
                        _OnEveryCheckIn = Value
                    End Set
                End Property

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub
            End Class
        End Namespace

    End Namespace
End Namespace
