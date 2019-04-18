Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports gloGlobal



Namespace gloEMRLab


    Public Class gloEMRLabTest
        Implements IDisposable
        Private _LabActor As gloEMRActors.LabActor.Test
        Private _Exception As gloEMRLabExceptions

        Dim _gloEMRDatabase As DataBaseLayer

        '28-Apr-14 8020 Orders PRD: Show tests by Order Type
        Public Enum OrderTestType
            LabTests = 1
            RadiologyImaging = 2
            Referrals = 3
            Other = 4
            Groups = 5
            PlannedOrder = 6
        End Enum
        Dim LabActorAssigned As Boolean = True
        Public Property LabActor() As gloEMRActors.LabActor.Test
            Get
                Return _LabActor
            End Get
            Set(ByVal value As gloEMRActors.LabActor.Test)
                If (LabActorAssigned) Then
                    If (IsNothing(_LabActor) = False) Then
                        _LabActor.Dispose()
                        _LabActor = Nothing
                    End If
                    LabActorAssigned = False
                End If
                _LabActor = value

            End Set
        End Property

        Public Function Add(Optional ByVal _arrLabs As ArrayList = Nothing, Optional ByVal _arrOrders As ArrayList = Nothing, Optional ByVal _arrOtherDiagnosis As ArrayList = Nothing, Optional ByVal _arrmanagement As ArrayList = Nothing, Optional ByRef _TestID As Long = 0) As Int64  ''_TestID added for selecting particular test
            _gloEMRDatabase = New DataBaseLayer
            Dim objDBParameter As DBParameter
            Dim _LabTestID As Int64

            Try
                _gloEMRDatabase.DBParametersCol.Clear()
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = GetPrefixTransactionID(Date.Now)
                objDBParameter.Name = "@MachineID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabActor.Code
                objDBParameter.Name = "@labtm_Code"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabActor.Name
                objDBParameter.Name = "@labtm_Name"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.Int
                If _LabActor.Ordarable = True Then
                    objDBParameter.Value = 1
                Else
                    objDBParameter.Value = 0
                End If
                objDBParameter.Name = "@labtm_Ordarable"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = _LabActor.Specimen ''GetSpecimanID(_LabActor.Specimen)
                objDBParameter.Name = "@labtm_SpecimenID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = _LabActor.Collection ''GetCollectionContainerID(_LabActor.Collection)
                objDBParameter.Name = "@labtm_CollectionID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = _LabActor.Storage ''GetStorageTempratureID(_LabActor.Storage)
                objDBParameter.Name = "@labtm_StorageID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabActor.Instruction
                objDBParameter.Name = "@labtm_Instruction"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabActor.Precaution
                objDBParameter.Name = "@labtm_Precuation"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing


                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar 'changed by sagaarK because the data type was asked to change to varchar
                objDBParameter.Value = _LabActor.LOINCId
                objDBParameter.Name = "@labtm_LOINCId"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = _LabActor.DepartmentCategoryID
                objDBParameter.Name = "@labtm_DeprtCatID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = _LabActor.TestHeadID
                objDBParameter.Name = "@labtm_TestHeadID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.Int
                objDBParameter.Value = CInt(_LabActor.ResultType)
                objDBParameter.Name = "@labtm_ResultType"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                '20090317
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabActor.SpecimenName
                objDBParameter.Name = "@labtm_SpecimenName"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                '20090317
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabActor.CollectionName
                objDBParameter.Name = "@labtm_CollectionName"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                '20090317
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabActor.StorageName
                objDBParameter.Name = "@labtm_StorageName"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                '20090317
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = _LabActor.nClinicID
                objDBParameter.Name = "@nClinicID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing


                ''Added by Mayuri:20130527-Orders PRD change-7030-MU stage 2
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabActor.LOINCLongName
                objDBParameter.Name = "@labtm_LOINCLongName"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabActor.sCPTCode
                objDBParameter.Name = "@labtm_CPTCode"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = _LabActor.nTemplateID
                objDBParameter.Name = "@labtm_TemplateID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabActor.sCPTDescription
                objDBParameter.Name = "@labtm_CPTDEscription"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabActor.MUReportingCategory
                objDBParameter.Name = "@labtm_MUReportingCategory"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing


                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabActor.IsStructuredLabTest
                objDBParameter.Name = "@IsPositiveNegativeNumeric"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing


                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.Int
                If _LabActor.bOutboundTransistion = True Then
                    objDBParameter.Value = 1
                Else
                    objDBParameter.Value = 0
                End If
                objDBParameter.Name = "@bOutboundTransistion"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.InputOutput
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = 0
                objDBParameter.Name = "@id"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing




                _LabTestID = _gloEMRDatabase.Add("Lab_InsertLabTestMST") '("Lab_InsertLabTest")
                _TestID = _LabTestID
                If _LabTestID > 0 Then
                    With _LabActor.Results
                        For i As Int16 = 0 To .Count - 1
                            _gloEMRDatabase.DBParametersCol.Clear()

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.BigInt
                            objDBParameter.Value = _LabTestID
                            objDBParameter.Name = "@labtrd_TestID"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.BigInt
                            objDBParameter.Value = .Item(i).ResultID
                            objDBParameter.Name = "@labtrd_ResultID"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = .Item(i).ResultName
                            objDBParameter.Name = "@labtrd_ResultName"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.Int
                            objDBParameter.Value = CInt(.Item(i).ValueType)
                            objDBParameter.Name = "@labtrd_ValueType"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = .Item(i).Unit
                            objDBParameter.Name = "@labtrd_Unit"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = .Item(i).DefaultValue
                            objDBParameter.Name = "@labtrd_DefaultValue"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = .Item(i).ReferenceRange
                            objDBParameter.Name = "@labtrd_RefRange"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = .Item(i).Comments
                            objDBParameter.Name = "@labtrd_Comments"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = .Item(i).Instruction
                            objDBParameter.Name = "@labtrd_Instruction"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.BigInt
                            objDBParameter.Value = .Item(i).BoundID
                            objDBParameter.Name = "@labtrd_BoundID"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = .Item(i).BoundMaleLower
                            objDBParameter.Name = "@labtrd_BoundML"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = .Item(i).BoundMaleUpper
                            objDBParameter.Name = "@labtrd_BoundMU"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = .Item(i).BoundFemaleLower
                            objDBParameter.Name = "@labtrd_BoundFML"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = .Item(i).BoundFemaleUpper
                            objDBParameter.Name = "@labtrd_BoundFMU"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = .Item(i).LoincID
                            objDBParameter.Name = "@labtrd_LoincID"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing

                            '05-May-15 Aniket: Resolving Bug #83001: gloEMR>Edit>Orders & Result setup>Result details are not getting saved. 
                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = .Item(i).AlternateResultCode
                            objDBParameter.Name = "@labtrd_AlternateResultCode"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing

                            _gloEMRDatabase.Add("Lab_InsertLabTestResult")
                        Next
                    End With
                End If


                If _LabTestID > 0 Then
                    With _LabActor.PreferedResults
                        For i As Int16 = 0 To .Count - 1
                            If (.Item(i).TLabCI_Id > 0) Then
                                _gloEMRDatabase.DBParametersCol.Clear()
                                objDBParameter = New DBParameter
                                objDBParameter.Direction = ParameterDirection.Input
                                objDBParameter.DataType = SqlDbType.BigInt
                                objDBParameter.Value = .Item(i).TestMstPreferredLabID
                                objDBParameter.Name = "@LabTestMstPreferredLabID"
                                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                                objDBParameter = Nothing

                                objDBParameter = New DBParameter
                                objDBParameter.Direction = ParameterDirection.Input
                                objDBParameter.DataType = SqlDbType.BigInt
                                objDBParameter.Value = _TestID
                                objDBParameter.Name = "@labtm_ID"
                                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                                objDBParameter = Nothing

                                objDBParameter = New DBParameter
                                objDBParameter.Direction = ParameterDirection.Input
                                objDBParameter.DataType = SqlDbType.BigInt
                                objDBParameter.Value = .Item(i).TLabCI_Id
                                objDBParameter.Name = "@labci_Id"
                                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                                objDBParameter = Nothing

                                objDBParameter = New DBParameter
                                objDBParameter.Direction = ParameterDirection.Input
                                objDBParameter.DataType = SqlDbType.VarChar
                                objDBParameter.Value = .Item(i).sComments
                                objDBParameter.Name = "@sComments"
                                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                                objDBParameter = Nothing
                                _gloEMRDatabase.Add("gsp_InPreferedLabs")
                            End If
                        Next
                    End With
                End If

                If Not IsNothing(_arrLabs) AndAlso Not IsNothing(_arrOrders) AndAlso Not IsNothing(_arrOtherDiagnosis) AndAlso Not IsNothing(_arrmanagement) Then
                    If _arrLabs.Count > 0 Then
                        Dim strDeleteQRY As String = "DELETE AssociatedEMField where nFieldID= " & _LabTestID & " and nFieldType= '1'"

                        Convert.ToInt64(_gloEMRDatabase.Delete_Query(strDeleteQRY))
                    End If
                End If
                If Not IsNothing(_arrLabs) Then
                    For i As Integer = 0 To _arrLabs.Count - 1
                        _gloEMRDatabase.DBParametersCol.Clear()
                        objDBParameter = New DBParameter
                        objDBParameter.DataType = SqlDbType.BigInt
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.Name = "@nFieldID"
                        objDBParameter.Value = _LabTestID
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing

                        objDBParameter = New DBParameter
                        objDBParameter.DataType = SqlDbType.VarChar
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.Name = "@sAssociatedEMName"
                        objDBParameter.Value = CType(_arrLabs.Item(i), gloGeneralItem.gloItem).Description
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing

                        objDBParameter = New DBParameter
                        objDBParameter.DataType = SqlDbType.Int
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.Name = "@nFieldType"
                        objDBParameter.Value = 1
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing

                        objDBParameter = New DBParameter
                        objDBParameter.DataType = SqlDbType.VarChar
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.Name = "@sAssociatedEMCategory"
                        objDBParameter.Value = CType(_arrLabs.Item(i), gloGeneralItem.gloItem).Code
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing

                        objDBParameter = New DBParameter
                        objDBParameter.DataType = SqlDbType.VarChar
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.Name = "@sStatus"
                        objDBParameter.Value = CType(_arrLabs.Item(i), gloGeneralItem.gloItem).Status
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing
                        _gloEMRDatabase.Add("FillEMFields")
                    Next
                End If
                If Not IsNothing(_arrOrders) Then
                    For i As Integer = 0 To _arrOrders.Count - 1
                        _gloEMRDatabase.DBParametersCol.Clear()
                        objDBParameter = New DBParameter
                        objDBParameter.DataType = SqlDbType.BigInt
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.Name = "@nFieldID"
                        objDBParameter.Value = _LabTestID
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing

                        objDBParameter = New DBParameter
                        objDBParameter.DataType = SqlDbType.VarChar
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.Name = "@sAssociatedEMName"
                        objDBParameter.Value = CType(_arrOrders.Item(i), gloGeneralItem.gloItem).Description
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing

                        objDBParameter = New DBParameter
                        objDBParameter.DataType = SqlDbType.Int
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.Name = "@nFieldType"
                        objDBParameter.Value = 1
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing

                        objDBParameter = New DBParameter
                        objDBParameter.DataType = SqlDbType.VarChar
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.Name = "@sAssociatedEMCategory"
                        objDBParameter.Value = CType(_arrOrders.Item(i), gloGeneralItem.gloItem).Code
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing

                        objDBParameter = New DBParameter
                        objDBParameter.DataType = SqlDbType.VarChar
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.Name = "@sStatus"
                        objDBParameter.Value = CType(_arrOrders.Item(i), gloGeneralItem.gloItem).Status
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing
                        _gloEMRDatabase.Add("FillEMFields")
                    Next
                End If


                If Not IsNothing(_arrOtherDiagnosis) Then
                    For i As Integer = 0 To _arrOtherDiagnosis.Count - 1
                        _gloEMRDatabase.DBParametersCol.Clear()
                        objDBParameter = New DBParameter
                        objDBParameter.DataType = SqlDbType.BigInt
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.Name = "@nFieldID"
                        objDBParameter.Value = _LabTestID
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing

                        objDBParameter = New DBParameter
                        objDBParameter.DataType = SqlDbType.VarChar
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.Name = "@sAssociatedEMName"
                        objDBParameter.Value = CType(_arrOtherDiagnosis.Item(i), gloGeneralItem.gloItem).Description
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing

                        objDBParameter = New DBParameter
                        objDBParameter.DataType = SqlDbType.Int
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.Name = "@nFieldType"
                        objDBParameter.Value = 1
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing

                        objDBParameter = New DBParameter
                        objDBParameter.DataType = SqlDbType.VarChar
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.Name = "@sAssociatedEMCategory"
                        objDBParameter.Value = CType(_arrOtherDiagnosis.Item(i), gloGeneralItem.gloItem).Code
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing

                        objDBParameter = New DBParameter
                        objDBParameter.DataType = SqlDbType.VarChar
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.Name = "@sStatus"
                        objDBParameter.Value = CType(_arrOtherDiagnosis.Item(i), gloGeneralItem.gloItem).Status
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing
                        _gloEMRDatabase.Add("FillEMFields")
                    Next
                End If

                If Not IsNothing(_arrmanagement) Then
                    For i As Integer = 0 To _arrmanagement.Count - 1
                        _gloEMRDatabase.DBParametersCol.Clear()
                        objDBParameter = New DBParameter
                        objDBParameter.DataType = SqlDbType.BigInt
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.Name = "@nFieldID"
                        objDBParameter.Value = _LabTestID
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing

                        objDBParameter = New DBParameter
                        objDBParameter.DataType = SqlDbType.VarChar
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.Name = "@sAssociatedEMName"
                        objDBParameter.Value = CType(_arrmanagement.Item(i), gloGeneralItem.gloItem).Description
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing

                        objDBParameter = New DBParameter
                        objDBParameter.DataType = SqlDbType.Int
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.Name = "@nFieldType"
                        objDBParameter.Value = 1
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing

                        objDBParameter = New DBParameter
                        objDBParameter.DataType = SqlDbType.VarChar
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.Name = "@sAssociatedEMCategory"
                        objDBParameter.Value = CType(_arrmanagement.Item(i), gloGeneralItem.gloItem).Code
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing

                        objDBParameter = New DBParameter
                        objDBParameter.DataType = SqlDbType.VarChar
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.Name = "@sStatus"
                        objDBParameter.Value = CType(_arrmanagement.Item(i), gloGeneralItem.gloItem).Status
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing
                        _gloEMRDatabase.Add("FillEMFields")
                    Next
                End If

            Catch ex As Exception
                _Exception = New gloEMRLabExceptions
                _Exception.ErrorMessage = "Test with same name already exists, please enter another name"
                Throw _Exception
                _Exception.Dispose()
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
            Return Nothing
        End Function

        Public Function UpdateAssociatedSmartDiagnosis(ByVal sUpdatedTestName As String, ByVal nTestID As Long, Optional ByVal sTestCode As String = "") As Boolean
            '' GLO2010-0004476 : Updates to Labs/Orders not reflected in Smart Orders already configured
            Dim _strSQL As String = ""
            Dim _Result As Boolean = False
            Dim oSQLCommand As New SqlCommand
            Dim _sqlConnection As New gloEMRDataConnection(DataBaseLayer.ConnectionString)

            Try
                oSQLCommand.Connection = _sqlConnection.gloSqlConnection
                If sTestCode.Trim = "" Then
                    oSQLCommand.CommandText = "UPDATE ICD9Lm_test SET lm_test_Name = '" & sUpdatedTestName.Replace("'", "''") & "' WHERE lm_test_ID = " & nTestID
                    If oSQLCommand.ExecuteNonQuery > 0 Then
                        _Result = True
                    End If
                Else
                    oSQLCommand.CommandText = "UPDATE ICD9Lab_Order SET labtm_Name = '" & sUpdatedTestName.Replace("'", "''") & "',labtm_Code='" & sTestCode.Replace("'", "''") & "' WHERE labtm_ID = " & nTestID
                    If oSQLCommand.ExecuteNonQuery > 0 Then
                        _Result = True
                    End If
                End If
                ''Replace added to sTestCode to solve bugid 68344 
            Catch ex As Exception
                Throw ex
            Finally
                If oSQLCommand IsNot Nothing Then
                    oSQLCommand.Parameters.Clear()
                    oSQLCommand.Dispose()
                    oSQLCommand = Nothing
                End If

                If Not IsNothing(_sqlConnection) Then
                    _sqlConnection.DisConnect()
                    _sqlConnection.Dispose()
                End If
            End Try
            Return _Result
        End Function
        'Public Function UpdateAssociatedSmartOrders(ByVal sUpdatedTestName As String, ByVal nTestID As Long) As Boolean
        '    '' GLO2010-0004476 : Updates to Labs/Orders not reflected in Smart Orders already configured
        '    Dim _strSQL As String = ""
        '    Dim _Result As Boolean = False

        '    Dim oSQLCommand As New SqlCommand
        '    Dim _sqlConnection As New gloEMRDataConnection(DataBaseLayer.ConnectionString)

        '    Try
        '        oSQLCommand.Connection = _sqlConnection.gloSqlConnection

        '        oSQLCommand.CommandText = "UPDATE Orderset SET sAssociateName = '" & sUpdatedTestName.Replace("'", "''") & "' WHERE nAssociateID = " & nTestID
        '        If oSQLCommand.ExecuteNonQuery > 0 Then
        '            _Result = True
        '        End If

        '    Catch ex As Exception
        '        Throw ex
        '    Finally
        '        If Not IsNothing(_sqlConnection) Then
        '            _sqlConnection.DisConnect()
        '            _sqlConnection.Dispose()
        '        End If
        '        oSQLCommand.Dispose()
        '    End Try
        '    Return _Result
        'End Function
        Public Function UpdateAssociatedSmartOrders(ByVal sUpdatedTestName As String, ByVal nTestID As Long) As Boolean ''function changed 8020 Order PRD changes
            _gloEMRDatabase = New DataBaseLayer
            Dim objDBParameter As DBParameter
            Dim cntrec As Integer
            Dim _Result As Boolean = False

            Try


                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = sUpdatedTestName.ToString().Replace("'", "''")
                objDBParameter.Name = "@sAssociateName"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing


                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = nTestID
                objDBParameter.Name = "@nAssociateID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing
                cntrec = _gloEMRDatabase.GetDataValue("gsp_OrderSet_getAssociateid", True)
                If cntrec = 0 Then
                    _Result = False
                Else
                    _Result = True
                End If

            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.DBParametersCol.Clear()
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
            Return _Result
        End Function
       
        Public Function Modify(ByVal TestID As Int64, Optional ByVal _arrLabs As ArrayList = Nothing, Optional ByVal _arrOrders As ArrayList = Nothing, Optional ByVal _arrOtherDiagnosis As ArrayList = Nothing, Optional ByVal _arrmanagement As ArrayList = Nothing) As Boolean
            _gloEMRDatabase = New DataBaseLayer
            Dim objDBParameter As DBParameter
            Dim strLabs As String = "Labs"
            Dim _LabTestID As Int64


            Try
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = TestID
                objDBParameter.Name = "@labtm_ID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabActor.Code
                objDBParameter.Name = "@labtm_Code"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabActor.Name
                objDBParameter.Name = "@labtm_Name"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.Int
                objDBParameter.Value = _LabActor.Ordarable
                objDBParameter.Name = "@labtm_Ordarable"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = _LabActor.Specimen
                objDBParameter.Name = "@labtm_SpecimenID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = _LabActor.Collection
                objDBParameter.Name = "@labtm_CollectionID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = _LabActor.Storage
                objDBParameter.Name = "@labtm_StorageID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabActor.Instruction
                objDBParameter.Name = "@labtm_Instruction"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabActor.Precaution
                objDBParameter.Name = "@labtm_Precuation"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabActor.LOINCId
                objDBParameter.Name = "@labtm_LOINCId"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = _LabActor.DepartmentCategoryID
                objDBParameter.Name = "@labtm_DeprtCatID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = _LabActor.TestHeadID
                objDBParameter.Name = "@labtm_TestHeadID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.Int
                objDBParameter.Value = _LabActor.ResultType
                objDBParameter.Name = "@labtm_ResultType"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                '20090318
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabActor.SpecimenName
                objDBParameter.Name = "@labtm_SpecimenName"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing
                '20090318
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabActor.CollectionName
                objDBParameter.Name = "@labtm_CollectionName"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                '20090318
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabActor.StorageName
                objDBParameter.Name = "@labtm_StorageName"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing
                '20090318
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = _LabActor.nClinicID
                objDBParameter.Name = "@nClinicID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                ''Added by Mayuri:20130527-Orders PRD change-7030-MU stage 2
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabActor.LOINCLongName
                objDBParameter.Name = "@labtm_LOINCLongName"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabActor.sCPTCode
                objDBParameter.Name = "@labtm_CPTCode"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = _LabActor.nTemplateID
                objDBParameter.Name = "@labtm_TemplateID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabActor.sCPTDescription
                objDBParameter.Name = "@labtm_CPTDEscription"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabActor.MUReportingCategory
                objDBParameter.Name = "@labtm_MUReportingCategory"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing
                ''

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabActor.IsStructuredLabTest
                objDBParameter.Name = "@IsPositiveNegativeNumeric"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing


                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.Int
                If _LabActor.bOutboundTransistion = True Then
                    objDBParameter.Value = 1
                Else
                    objDBParameter.Value = 0
                End If
                objDBParameter.Name = "@bOutboundTransistion"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                _LabTestID = _gloEMRDatabase.Add("Lab_UpdateLabTestMST") ''("Lab_UpdateLabTest")

                ' delete all the child rows
                _gloEMRDatabase.Delete_Query("delete from Lab_Test_ResultDtl where labtrd_TestID = " & TestID & "")

                With _LabActor.Results
                    For i As Int16 = 0 To .Count - 1
                        _gloEMRDatabase.DBParametersCol.Clear()

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.BigInt
                        objDBParameter.Value = TestID
                        objDBParameter.Name = "@labtrd_TestID"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.BigInt
                        objDBParameter.Value = CInt(.Item(i).ResultID)
                        objDBParameter.Name = "@labtrd_ResultID"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.VarChar
                        objDBParameter.Value = .Item(i).ResultName
                        objDBParameter.Name = "@labtrd_ResultName"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.Int
                        objDBParameter.Value = CInt(.Item(i).ValueType)
                        objDBParameter.Name = "@labtrd_ValueType"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing


                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.VarChar
                        objDBParameter.Value = .Item(i).Unit
                        objDBParameter.Name = "@labtrd_Unit"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.VarChar
                        objDBParameter.Value = .Item(i).DefaultValue
                        objDBParameter.Name = "@labtrd_DefaultValue"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.VarChar
                        objDBParameter.Value = .Item(i).ReferenceRange
                        objDBParameter.Name = "@labtrd_RefRange"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.VarChar
                        objDBParameter.Value = .Item(i).Comments
                        objDBParameter.Name = "@labtrd_Comments"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.VarChar
                        objDBParameter.Value = .Item(i).Instruction
                        objDBParameter.Name = "@labtrd_Instruction"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.BigInt
                        objDBParameter.Value = CInt(.Item(i).BoundID)
                        objDBParameter.Name = "@labtrd_BoundID"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.VarChar
                        objDBParameter.Value = .Item(i).BoundMaleLower
                        objDBParameter.Name = "@labtrd_BoundML"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.VarChar
                        objDBParameter.Value = .Item(i).BoundMaleUpper
                        objDBParameter.Name = "@labtrd_BoundMU"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.VarChar
                        objDBParameter.Value = .Item(i).BoundFemaleLower
                        objDBParameter.Name = "@labtrd_BoundFML"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.VarChar
                        objDBParameter.Value = .Item(i).BoundFemaleUpper
                        objDBParameter.Name = "@labtrd_BoundFMU"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.VarChar
                        objDBParameter.Value = .Item(i).LoincID
                        objDBParameter.Name = "@labtrd_LoincID"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.VarChar
                        objDBParameter.Value = .Item(i).AlternateResultCode
                        objDBParameter.Name = "@labtrd_AlternateResultCode"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing

                        _gloEMRDatabase.Add("Lab_InsertLabTestResult")
                    Next
                End With
                _gloEMRDatabase.Delete_Query("delete from Lab_Test_Mst_PreferredLab where labtm_ID = " & TestID & "")
                With _LabActor.PreferedResults
                    
                    For i As Int16 = 0 To .Count - 1
                        If (.Item(i).TLabCI_Id > 0) Then
                            _gloEMRDatabase.DBParametersCol.Clear()
                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.BigInt
                            objDBParameter.Value = .Item(i).TestMstPreferredLabID
                            objDBParameter.Name = "@LabTestMstPreferredLabID"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.BigInt
                            objDBParameter.Value = TestID
                            objDBParameter.Name = "@labtm_ID"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.BigInt
                            objDBParameter.Value = .Item(i).TLabCI_Id
                            objDBParameter.Name = "@labci_Id"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = .Item(i).sComments
                            objDBParameter.Name = "@sComments"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing
                            _gloEMRDatabase.Add("gsp_InPreferedLabs")
                        End If
                    Next

                End With
               
                If TestID > 0 Then

                    If Not IsNothing(_arrLabs) AndAlso Not IsNothing(_arrOrders) AndAlso Not IsNothing(_arrOtherDiagnosis) AndAlso Not IsNothing(_arrmanagement) Then
                        'If _arrLabs.Count > 0 Then
                        Dim strDeleteQRY As String = "DELETE AssociatedEMField where nFieldID= " & TestID & " and nFieldType= '1'"

                        Convert.ToInt64(_gloEMRDatabase.Delete_Query(strDeleteQRY))
                        'End If
                    End If

                    If Not IsNothing(_arrLabs) Then

                        For i As Integer = 0 To _arrLabs.Count - 1
                            _gloEMRDatabase.DBParametersCol.Clear()
                            objDBParameter = New DBParameter
                            objDBParameter.DataType = SqlDbType.BigInt
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.Name = "@nFieldID"
                            objDBParameter.Value = TestID
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing

                            objDBParameter = New DBParameter
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.Name = "@sAssociatedEMName"
                            objDBParameter.Value = CType(_arrLabs.Item(i), gloGeneralItem.gloItem).Description
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing

                            objDBParameter = New DBParameter
                            objDBParameter.DataType = SqlDbType.Int
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.Name = "@nFieldType"
                            objDBParameter.Value = 1
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing

                            objDBParameter = New DBParameter
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.Name = "@sAssociatedEMCategory"
                            objDBParameter.Value = CType(_arrLabs.Item(i), gloGeneralItem.gloItem).Code
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing


                            objDBParameter = New DBParameter
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.Name = "@sStatus"
                            objDBParameter.Value = CType(_arrLabs.Item(i), gloGeneralItem.gloItem).Status
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing
                            _gloEMRDatabase.Add("FillEMFields")
                        Next
                    End If
                    If Not IsNothing(_arrOrders) Then
                        For i As Integer = 0 To _arrOrders.Count - 1
                            _gloEMRDatabase.DBParametersCol.Clear()
                            objDBParameter = New DBParameter
                            objDBParameter.DataType = SqlDbType.BigInt
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.Name = "@nFieldID"
                            objDBParameter.Value = TestID
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing

                            objDBParameter = New DBParameter
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.Name = "@sAssociatedEMName"
                            objDBParameter.Value = CType(_arrOrders.Item(i), gloGeneralItem.gloItem).Description
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing

                            objDBParameter = New DBParameter
                            objDBParameter.DataType = SqlDbType.Int
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.Name = "@nFieldType"
                            objDBParameter.Value = 1
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing

                            objDBParameter = New DBParameter
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.Name = "@sAssociatedEMCategory"
                            objDBParameter.Value = CType(_arrOrders.Item(i), gloGeneralItem.gloItem).Code
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing

                            objDBParameter = New DBParameter
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.Name = "@sStatus"
                            objDBParameter.Value = CType(_arrOrders.Item(i), gloGeneralItem.gloItem).Status
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing
                            _gloEMRDatabase.Add("FillEMFields")
                        Next
                    End If

                    If Not IsNothing(_arrOtherDiagnosis) Then
                        For i As Integer = 0 To _arrOtherDiagnosis.Count - 1
                            _gloEMRDatabase.DBParametersCol.Clear()
                            objDBParameter = New DBParameter
                            objDBParameter.DataType = SqlDbType.BigInt
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.Name = "@nFieldID"
                            objDBParameter.Value = TestID
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing

                            objDBParameter = New DBParameter
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.Name = "@sAssociatedEMName"
                            objDBParameter.Value = CType(_arrOtherDiagnosis.Item(i), gloGeneralItem.gloItem).Description
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing

                            objDBParameter = New DBParameter
                            objDBParameter.DataType = SqlDbType.Int
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.Name = "@nFieldType"
                            objDBParameter.Value = 1
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing

                            objDBParameter = New DBParameter
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.Name = "@sAssociatedEMCategory"
                            objDBParameter.Value = CType(_arrOtherDiagnosis.Item(i), gloGeneralItem.gloItem).Code
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing

                            objDBParameter = New DBParameter
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.Name = "@sStatus"
                            objDBParameter.Value = CType(_arrOtherDiagnosis.Item(i), gloGeneralItem.gloItem).Status
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing
                            _gloEMRDatabase.Add("FillEMFields")
                        Next
                    End If

                    If Not IsNothing(_arrmanagement) Then
                        For i As Integer = 0 To _arrmanagement.Count - 1
                            _gloEMRDatabase.DBParametersCol.Clear()
                            objDBParameter = New DBParameter
                            objDBParameter.DataType = SqlDbType.BigInt
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.Name = "@nFieldID"
                            objDBParameter.Value = TestID
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing

                            objDBParameter = New DBParameter
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.Name = "@sAssociatedEMName"
                            objDBParameter.Value = CType(_arrmanagement.Item(i), gloGeneralItem.gloItem).Description
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing

                            objDBParameter = New DBParameter
                            objDBParameter.DataType = SqlDbType.Int
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.Name = "@nFieldType"
                            objDBParameter.Value = 1
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing

                            objDBParameter = New DBParameter
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.Name = "@sAssociatedEMCategory"
                            objDBParameter.Value = CType(_arrmanagement.Item(i), gloGeneralItem.gloItem).Code
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing

                            objDBParameter = New DBParameter
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.Name = "@sStatus"
                            objDBParameter.Value = CType(_arrmanagement.Item(i), gloGeneralItem.gloItem).Status
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            objDBParameter = Nothing
                            _gloEMRDatabase.Add("FillEMFields")
                        Next
                    End If

                End If
            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
            Return Nothing
        End Function

        Public Function Delete(ByVal TestID As Int64) As Boolean
            Dim _gloEMRDatabase As New DataBaseLayer
            Dim objDBParameter As DBParameter
            Try

                objDBParameter = New DBParameter
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.Name = "@TestID"
                objDBParameter.Value = TestID
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                _gloEMRDatabase.Delete("DeleteTestMst")

                Return True

            Catch ex As Exception
                Throw ex
                Return False
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try

        End Function

        '29-Jan-13 Aniket: Resolving Bug #62647
        Public Function IsTestUsed(ByVal TestID As Int64) As Boolean

            Dim oDB As New DataBaseLayer
            Dim oParameter As New DBParameter
            Dim intTestCount As Integer

            Try

                oParameter.DataType = SqlDbType.BigInt
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@LabTestID"
                oParameter.Value = TestID
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing
                intTestCount = oDB.GetDataValue("gsp_IsLabTestUsed")


                If intTestCount = 0 Then
                    Return False
                Else
                    Return True
                End If


            Catch ex As SqlException
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                Return ""

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                Return ""

            Finally
                oDB.Dispose()
                oDB = Nothing
            End Try

        End Function

        Public Function IsExists(ByVal TestName As String) As Boolean
            Dim _ID As String = ""
            Dim _Result As Boolean = False

            Dim _gloEMRDBID As New DataBaseLayer
            Try

                Dim _sqlquery = "SELECT labtm_ID FROM Lab_Test_Mst WHERE Upper(labtm_Name) = '" & TestName.ToUpper.Replace("'", "''") & "'"
                _ID = _gloEMRDBID.GetRecord_Query(_sqlquery)

                If Val(_ID) > 0 Then
                    _Result = True
                End If


            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try

            Return _Result
        End Function


        Public Function IsCodeExists(ByVal TestCode As String) As Boolean
            Dim _ID As String = ""
            Dim _Result As Boolean = False

            Dim _gloEMRDBID As New DataBaseLayer

            Try
                Dim _sqlquery = "SELECT labtm_ID FROM Lab_Test_Mst WHERE Upper(labtm_Code) = '" & TestCode.ToUpper.Replace("'", "''") & "'"
                _ID = _gloEMRDBID.GetRecord_Query(_sqlquery)
                If Val(_ID) > 0 Then
                    _Result = True
                End If
            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try
            Return _Result
        End Function


        Public Function GetTest(ByVal TestID As Int64) As gloEMRActors.LabActor.Test
            _gloEMRDatabase = New DataBaseLayer

            Dim oTest As New gloEMRActors.LabActor.Test
            Dim dt As DataTable = Nothing

            Try
                dt = _gloEMRDatabase.GetDataTable_Query("select labtm_ID, labtm_Code, labtm_Name, labtm_Ordarable, isnull(labtm_SpecimenID,0) as labtm_SpecimenID, isnull(labtm_CollectionID,0) as labtm_CollectionID, isnull(labtm_StorageID,0) as labtm_StorageID, isnull(labtm_Instruction,'') as labtm_Instruction ,isnull(labtm_Precuation,'') as labtm_Precuation,isnull(labtm_IsFinished,'') as labtm_IsFinished ,CASE WHEN (isnull(LOINCLongName,'')='' ) THEN isnull(labtm_LOINCId,'')WHEN (isnull(labtm_LOINCId,'')='' ) THEN isnull(LOINCLongName,'') ELSE isnull(labtm_LOINCId,'') + ' : ' +ISNULL(LOINCLongName,'') END as [LOINC Order Code], isnull(labtm_DeprtCatID,0) as labtm_DeprtCatID, isnull(labtm_TestHeadID,0) as labtm_TestHeadID,labtm_ResultType,isnull(labtm_SpecimenName,'')as labtm_SpecimenName, isnull(labtm_CollectionName,'') as labtm_CollectionName, isnull(labtm_StorageName,'') as labtm_StorageName, nClinicID, CASE ISNULL(sCPTCode,'') WHEN '' THEN '' ELSE  ISNULL(sCPTCode,'') +' : ' +ISNULL(sCPTDescription,'') END  CPT,ISNULL(nTemplateID,0) as nTemplateID,ISNULL(sMUReportingCategory,'') as sMUReportingCategory, IsNull(sIsPositiveNegativeNumeric,'') AS sIsPositiveNegativeNumeric, bOutboundTransistion  from Lab_Test_Mst where labtm_Code is not null and labtm_Name is not null and labtm_ID is not null and labtm_ID=" & TestID)
                If Not dt Is Nothing Then
                    If dt.Rows.Count > 0 Then


                        oTest.TestID = dt.Rows(0)("labtm_ID")
                        oTest.Code = dt.Rows(0)("labtm_Code")
                        oTest.Name = dt.Rows(0)("labtm_Name")

                        If Not IsDBNull(dt.Rows(0)("labtm_Ordarable")) Then
                            oTest.Ordarable = dt.Rows(0)("labtm_Ordarable")
                        End If

                        If Not IsDBNull(dt.Rows(0)("labtm_SpecimenID")) Then
                            oTest.Specimen = dt.Rows(0)("labtm_SpecimenID")
                        End If

                        If Not IsDBNull(dt.Rows(0)("labtm_SpecimenName")) Then
                            oTest.SpecimenName = dt.Rows(0)("labtm_SpecimenName")
                        End If

                        If Not IsDBNull(dt.Rows(0)("labtm_CollectionID")) Then
                            oTest.Collection = dt.Rows(0)("labtm_CollectionID")
                        End If

                        If Not IsDBNull(dt.Rows(0)("labtm_CollectionName")) Then
                            oTest.CollectionName = dt.Rows(0)("labtm_CollectionName")
                        End If

                        If Not IsDBNull(dt.Rows(0)("labtm_StorageID")) Then
                            oTest.Storage = dt.Rows(0)("labtm_StorageID")
                        End If

                        If Not IsDBNull(dt.Rows(0)("labtm_StorageName")) Then
                            oTest.StorageName = dt.Rows(0)("labtm_StorageName")
                        End If

                        If Not IsDBNull(dt.Rows(0)("labtm_Instruction")) Then
                            oTest.Instruction = dt.Rows(0)("labtm_Instruction")
                        End If

                        If Not IsDBNull(dt.Rows(0)("labtm_Precuation")) Then
                            oTest.Precaution = dt.Rows(0)("labtm_Precuation")
                        End If

                        If Not IsDBNull(dt.Rows(0)("labtm_IsFinished")) Then
                            oTest.IsFinished = dt.Rows(0)("labtm_IsFinished")
                        End If

                        'If Not IsDBNull(dt.Rows(0)("labtm_LOINCId")) Then
                        '    oTest.LOINCId = dt.Rows(0)("labtm_LOINCId")
                        'End If
                        If Not IsDBNull(dt.Rows(0)("labtm_DeprtCatID")) Then
                            oTest.DepartmentCategoryID = dt.Rows(0)("labtm_DeprtCatID")
                        End If

                        If Not IsDBNull(dt.Rows(0)("labtm_TestHeadID")) Then
                            oTest.TestHeadID = dt.Rows(0)("labtm_TestHeadID")
                        End If

                        If Not IsDBNull(dt.Rows(0)("labtm_ResultType")) Then
                            oTest.ResultType = dt.Rows(0)("labtm_ResultType")
                        End If

                        'ORders PRD -MU Stage 2'
                        If Not IsDBNull(dt.Rows(0)("LOINC Order Code")) Then
                            oTest.LOINCLongName = dt.Rows(0)("LOINC Order Code")
                        End If
                        If Not IsDBNull(dt.Rows(0)("CPT")) Then
                            oTest.sCPTDescription = dt.Rows(0)("CPT")
                        End If

                        If Not IsDBNull(dt.Rows(0)("nTemplateID")) Then
                            oTest.nTemplateID = dt.Rows(0)("nTemplateID")
                        End If

                        If Not IsDBNull(dt.Rows(0)("sMUReportingCategory")) Then
                            oTest.MUReportingCategory = dt.Rows(0)("sMUReportingCategory")
                        End If

                        If Not IsDBNull(dt.Rows(0)("sIsPositiveNegativeNumeric")) Then
                            oTest.IsStructuredLabTest = dt.Rows(0)("sIsPositiveNegativeNumeric")
                        End If

                        If Not IsDBNull(dt.Rows(0)("bOutboundTransistion")) Then
                            oTest.bOutboundTransistion = dt.Rows(0)("bOutboundTransistion")
                        End If

                    End If
                    dt.Dispose()
                    dt = Nothing
                End If
                dt = Nothing

                'Details
                Dim oTestResult As gloEMRActors.LabActor.TestResult

                ' dt = New DataTable
                dt = _gloEMRDatabase.GetDataTable_Query("select labtrd_TestID,labtrd_ResultID,labtrd_ResultName,labtrd_ValueType,labtrd_RefRange,labtrd_Comments,labtrd_LOINCId, labtrd_AlternateResultCode from Lab_Test_ResultDtl where labtrd_ResultName is not null and labtrd_ValueType is not null and labtrd_ResultID is not null and labtrd_TestID= " & TestID & " ORDER BY labtrd_ResultID")
                If Not dt Is Nothing Then
                    If dt.Rows.Count > 0 Then
                        For i As Int16 = 0 To dt.Rows.Count - 1
                            oTestResult = New gloEMRActors.LabActor.TestResult

                            With oTestResult
                                .TestID = dt.Rows(i)("labtrd_TestID")
                                .ResultID = dt.Rows(i)("labtrd_ResultID")

                                .ResultName = dt.Rows(i)("labtrd_ResultName")
                                .ValueType = dt.Rows(i)("labtrd_ValueType")
                                If Not IsDBNull(dt.Rows(i)("labtrd_RefRange")) Then
                                    .ReferenceRange = dt.Rows(i)("labtrd_RefRange") & ""
                                End If
                                If Not IsDBNull(dt.Rows(i)("labtrd_Comments")) Then
                                    .Comments = dt.Rows(i)("labtrd_Comments") & ""
                                End If

                                If Not IsDBNull(dt.Rows(i)("labtrd_LOINCId")) Then
                                    .LoincID = dt.Rows(i)("labtrd_LOINCId") & ""
                                End If

                                If Not IsDBNull(dt.Rows(i)("labtrd_AlternateResultCode")) Then
                                    .AlternateResultCode = dt.Rows(i)("labtrd_AlternateResultCode") & ""
                                End If
                                
                            End With

                            If Not oTestResult Is Nothing Then
                                oTest.Results.Add(oTestResult)
                            End If

                            oTestResult = Nothing
                        Next
                    End If
                    dt.Dispose()
                    dt = Nothing
                End If

                Return oTest
            Catch ex As Exception
                Throw ex
                Return Nothing
            Finally
                oTest = Nothing
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If
            End Try

        End Function

        Public Function GetAssociatedEMField(ByVal TestID As Int64) As String
            _gloEMRDatabase = New DataBaseLayer

            'Dim oTest As New gloEMRActors.LabActor.Test
            Dim str As String = ""

            Try

                str = _gloEMRDatabase.GetRecord_Query("SELECT sAssociatedEMName FROM AssociatedEMField WHERE nFieldID = " & TestID & " AND nFieldType = '1'")

                Return str
            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try

        End Function
        Public Function GetEMAssociatedField(ByVal ID As Int64) As DataTable
            Dim _result As DataTable = Nothing
            Dim oDB As New DataBaseLayer
            Try

                Dim strQRY As String = "SELECT sAssociatedEMName,sAssociatedEMCategory,sStatus FROM AssociatedEMField WHERE nFieldID = " & ID & " AND nFieldType = '1' "

                _result = oDB.GetDataTable_Query(strQRY)
            Catch ex As Exception

            Finally
                If Not IsNothing(oDB) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If
            End Try
            Return _result
        End Function

        Public Function GetTests(Optional ByVal IncludeResults As Boolean = False) As gloEMRActors.LabActor.Tests
            _gloEMRDatabase = New DataBaseLayer

            Dim oTests As New gloEMRActors.LabActor.Tests
            Dim oTest As gloEMRActors.LabActor.Test
            Dim dt As DataTable = Nothing
            Dim dtResult As DataTable = Nothing

            Try
                dt = _gloEMRDatabase.GetDataTable_Query("select labtm_ID,labtm_Code,labtm_Name,labtm_Ordarable,labtm_SpecimenID, " & _
                                                        " labtm_CollectionID,labtm_StorageID,labtm_Instruction,labtm_Precuation,labtm_LOINCId, " & _
                                                        " labtm_DeprtCatID,labtm_TestHeadID,labtm_ResultType " & _
                                                        " from Lab_Test_Mst where labtm_Code is not null and labtm_Name is not null and labtm_ID is not null order by labtm_Name ") ''order by added to get the name ascending sorting wise
                If (IsNothing(dt) = False) Then


                    If dt.Rows.Count > 0 Then
                        For i As Integer = 0 To dt.Rows.Count - 1
                            oTest = New gloEMRActors.LabActor.Test

                            oTest.TestID = dt.Rows(i)("labtm_ID")
                            oTest.Code = dt.Rows(i)("labtm_Code")
                            oTest.Name = dt.Rows(i)("labtm_Name")

                            If IncludeResults = True Then



                                If Not IsDBNull(dt.Rows(i)("labtm_Ordarable")) Then
                                    oTest.Ordarable = dt.Rows(i)("labtm_Ordarable")
                                End If

                                If Not IsDBNull(dt.Rows(0)("labtm_SpecimenID")) Then
                                    oTest.Specimen = GetSpecimanName(dt.Rows(i)("labtm_SpecimenID"))
                                End If

                                If Not IsDBNull(dt.Rows(0)("labtm_CollectionID")) Then
                                    oTest.Collection = GetCollectionContainerName(dt.Rows(i)("labtm_CollectionID"))
                                End If
                                If Not IsDBNull(dt.Rows(0)("labtm_StorageID")) Then
                                    oTest.Storage = GetStorageTempratureName(dt.Rows(i)("labtm_StorageID"))
                                End If

                                If Not IsDBNull(dt.Rows(i)("labtm_Instruction")) Then
                                    oTest.Instruction = dt.Rows(i)("labtm_Instruction")
                                End If

                                If Not IsDBNull(dt.Rows(i)("labtm_Precuation")) Then
                                    oTest.Precaution = dt.Rows(i)("labtm_Precuation")
                                End If
                                If Not IsDBNull(dt.Rows(i)("labtm_LOINCId")) Then
                                    oTest.LOINCId = dt.Rows(i)("labtm_LOINCId")
                                End If
                                'If Not IsDBNull(dt.Rows(i)("labotrd_AlternateResultCode")) Then
                                '    oTest.AlternateResultCode = dt.Rows(i)("labotrd_AlternateResultCode")
                                'End If
                                If Not IsDBNull(dt.Rows(i)("labtm_DeprtCatID")) Then
                                    oTest.DepartmentCategoryID = dt.Rows(i)("labtm_DeprtCatID")
                                End If

                                If Not IsDBNull(dt.Rows(i)("labtm_TestHeadID")) Then
                                    oTest.TestHeadID = dt.Rows(i)("labtm_TestHeadID")
                                End If

                                If Not IsDBNull(dt.Rows(i)("labtm_ResultType")) Then
                                    oTest.ResultType = dt.Rows(i)("labtm_ResultType")
                                End If


                                'Details
                                Dim oTestResult As gloEMRActors.LabActor.TestResult


                                dtResult = _gloEMRDatabase.GetDataTable_Query("select labtrd_TestID,labtrd_ResultID,labtrd_ResultName " & _
                                                                              " labtrd_ValueType,labtrd_RefRange,labtrd_Comments " & _
                                                                              " from Lab_Test_ResultDtl where labtrd_ResultName is not null and labtrd_ValueType is not null and labtrd_ResultID is not null and  labtrd_TestID = " & oTest.TestID & " ORDER BY labtrd_ResultID")
                                If Not dtResult Is Nothing Then
                                    If dtResult.Rows.Count > 0 Then
                                        For j As Int64 = 0 To dtResult.Rows.Count - 1
                                            oTestResult = New gloEMRActors.LabActor.TestResult

                                            With oTestResult
                                                .TestID = dtResult.Rows(j)("labtrd_TestID")
                                                .ResultID = dtResult.Rows(j)("labtrd_ResultID")
                                                .ResultName = dtResult.Rows(j)("labtrd_ResultName")
                                                .ValueType = dtResult.Rows(j)("labtrd_ValueType")

                                                If Not IsDBNull(dtResult.Rows(j)("labtrd_RefRange")) Then
                                                    .ReferenceRange = dtResult.Rows(j)("labtrd_RefRange") & ""
                                                End If
                                                If Not IsDBNull(dtResult.Rows(j)("labtrd_Comments")) Then
                                                    .Comments = dtResult.Rows(j)("labtrd_Comments") & ""
                                                End If
                                            End With

                                            If Not oTestResult Is Nothing Then
                                                oTest.Results.Add(oTestResult)
                                            End If

                                            oTestResult = Nothing
                                        Next
                                    End If
                                    dtResult.Dispose()
                                    dtResult = Nothing

                                End If
                                dtResult = Nothing
                            End If

                            oTests.Add(oTest)

                            oTest = Nothing
                        Next
                    End If
                    dt.Dispose()
                    dt = Nothing
                End If
                Return oTests

            Catch ex As Exception
                Throw ex
                Return Nothing
            Finally
                If Not IsNothing(dtResult) Then
                    dtResult.Dispose()
                    dtResult = Nothing
                End If
                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try

        End Function


        Public Function GetTestsNew(Optional ByVal IncludeResults As Boolean = False) As gloEMRActors.LabActor.Tests
            _gloEMRDatabase = New DataBaseLayer

            Dim oTests As New gloEMRActors.LabActor.Tests
            Dim oTest As gloEMRActors.LabActor.Test
            Dim dt As DataTable = Nothing
            Dim dtResult As DataTable = Nothing

            Try
                dt = _gloEMRDatabase.GetDataTable_Query("select labtm_ID,labtm_Code,labtm_Name,labtm_Ordarable,labtm_SpecimenID, " & _
                                                        " labtm_CollectionID,labtm_StorageID,labtm_Instruction,labtm_Precuation,labtm_LOINCId, " & _
                                                        " labtm_DeprtCatID,labtm_TestHeadID,labtm_ResultType, " & _
                                                         " ISNULL(sMUReportingCategory,'') as sMUReportingCategory ," & _
                                                         "IsNull(sIsPositiveNegativeNumeric,'') AS sIsPositiveNegativeNumeric ," & _
                                                         " IsNULL(bOutboundTransistion,'False') as bOutboundTransistion, ISNULL(dtupdateddate,'') as dtupdateddate " & _
                                                         " from Lab_Test_Mst where labtm_Code is not null and labtm_Name is not null and labtm_ID is not null order by labtm_Name ") ''order by added to get the name ascending sorting wise
                If (IsNothing(dt) = False) Then


                    If dt.Rows.Count > 0 Then
                        For i As Integer = 0 To dt.Rows.Count - 1
                            oTest = New gloEMRActors.LabActor.Test

                            oTest.TestID = dt.Rows(i)("labtm_ID")
                            oTest.Code = dt.Rows(i)("labtm_Code")
                            oTest.Name = dt.Rows(i)("labtm_Name")
                            oTest.MUReportingCategory = dt.Rows(i)("sMUReportingCategory")
                            oTest.IsStructuredLabTest = dt.Rows(i)("sIsPositiveNegativeNumeric")
                            oTest.bOutboundTransistion = dt.Rows(i)("bOutboundTransistion")
                            oTest.dtUpdatedDate = dt.Rows(i)("dtupdateddate")
                            If IncludeResults = True Then



                                If Not IsDBNull(dt.Rows(i)("labtm_Ordarable")) Then
                                    oTest.Ordarable = dt.Rows(i)("labtm_Ordarable")
                                End If

                                If Not IsDBNull(dt.Rows(0)("labtm_SpecimenID")) Then
                                    oTest.Specimen = GetSpecimanName(dt.Rows(i)("labtm_SpecimenID"))
                                End If

                                If Not IsDBNull(dt.Rows(0)("labtm_CollectionID")) Then
                                    oTest.Collection = GetCollectionContainerName(dt.Rows(i)("labtm_CollectionID"))
                                End If
                                If Not IsDBNull(dt.Rows(0)("labtm_StorageID")) Then
                                    oTest.Storage = GetStorageTempratureName(dt.Rows(i)("labtm_StorageID"))
                                End If

                                If Not IsDBNull(dt.Rows(i)("labtm_Instruction")) Then
                                    oTest.Instruction = dt.Rows(i)("labtm_Instruction")
                                End If

                                If Not IsDBNull(dt.Rows(i)("labtm_Precuation")) Then
                                    oTest.Precaution = dt.Rows(i)("labtm_Precuation")
                                End If
                                If Not IsDBNull(dt.Rows(i)("labtm_LOINCId")) Then
                                    oTest.LOINCId = dt.Rows(i)("labtm_LOINCId")
                                End If
                                'If Not IsDBNull(dt.Rows(i)("labotrd_AlternateResultCode")) Then
                                '    oTest.AlternateResultCode = dt.Rows(i)("labotrd_AlternateResultCode")
                                'End If
                                If Not IsDBNull(dt.Rows(i)("labtm_DeprtCatID")) Then
                                    oTest.DepartmentCategoryID = dt.Rows(i)("labtm_DeprtCatID")
                                End If

                                If Not IsDBNull(dt.Rows(i)("labtm_TestHeadID")) Then
                                    oTest.TestHeadID = dt.Rows(i)("labtm_TestHeadID")
                                End If

                                If Not IsDBNull(dt.Rows(i)("labtm_ResultType")) Then
                                    oTest.ResultType = dt.Rows(i)("labtm_ResultType")
                                End If


                                'Details
                                Dim oTestResult As gloEMRActors.LabActor.TestResult


                                dtResult = _gloEMRDatabase.GetDataTable_Query("select labtrd_TestID,labtrd_ResultID,labtrd_ResultName " & _
                                                                              " labtrd_ValueType,labtrd_RefRange,labtrd_Comments " & _
                                                                              " from Lab_Test_ResultDtl where labtrd_ResultName is not null and labtrd_ValueType is not null and labtrd_ResultID is not null and  labtrd_TestID = " & oTest.TestID & " ORDER BY labtrd_ResultID")
                                If Not dtResult Is Nothing Then
                                    If dtResult.Rows.Count > 0 Then
                                        For j As Int64 = 0 To dtResult.Rows.Count - 1
                                            oTestResult = New gloEMRActors.LabActor.TestResult

                                            With oTestResult
                                                .TestID = dtResult.Rows(j)("labtrd_TestID")
                                                .ResultID = dtResult.Rows(j)("labtrd_ResultID")
                                                .ResultName = dtResult.Rows(j)("labtrd_ResultName")
                                                .ValueType = dtResult.Rows(j)("labtrd_ValueType")

                                                If Not IsDBNull(dtResult.Rows(j)("labtrd_RefRange")) Then
                                                    .ReferenceRange = dtResult.Rows(j)("labtrd_RefRange") & ""
                                                End If
                                                If Not IsDBNull(dtResult.Rows(j)("labtrd_Comments")) Then
                                                    .Comments = dtResult.Rows(j)("labtrd_Comments") & ""
                                                End If
                                            End With

                                            If Not oTestResult Is Nothing Then
                                                oTest.Results.Add(oTestResult)
                                            End If

                                            oTestResult = Nothing
                                        Next
                                    End If
                                    dtResult.Dispose()
                                    dtResult = Nothing
                                End If
                                dtResult = Nothing
                            End If

                            oTests.Add(oTest)

                            If IsNothing(oTest) = False Then
                                oTest.Dispose()
                                oTest = Nothing
                            End If
                            

                        Next
                    End If
                    dt.Dispose()
                    dt = Nothing
                End If
                Return oTests

            Catch ex As Exception
                Throw ex
                Return Nothing
            Finally
                If Not IsNothing(dtResult) Then
                    dtResult.Dispose()
                    dtResult = Nothing
                End If
                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try

        End Function

        '28-Apr-14 8020 Orders PRD: Show tests by Order Type
        Public Function GetTestsByType(ByVal TestType As OrderTestType, Optional ByVal PreferredId As Int64 = 0) As gloEMRActors.LabActor.Tests

            Dim oDB As New DataBaseLayer
            Dim oTests As New gloEMRActors.LabActor.Tests
            Dim oTest As gloEMRActors.LabActor.Test
            Dim dtTests As DataTable = Nothing
            Dim objDBParameter As DBParameter

            Try
                If PreferredId = 0 Then
                    If TestType = OrderTestType.LabTests Then
                        dtTests = oDB.GetDataTable("gsp_GetLabOrderTests")

                    ElseIf TestType = OrderTestType.RadiologyImaging Then
                        dtTests = oDB.GetDataTable("gsp_GetRadiologyImagingOrderTests")

                    ElseIf TestType = OrderTestType.Other Then  ''added for order PRd 8020 
                        dtTests = oDB.GetDataTable("gsp_OtherOrderTests")

                    ElseIf TestType = OrderTestType.Referrals Then
                        dtTests = oDB.GetDataTable("gsp_ReferralLabOrderTests")
                    End If
                Else


                    _gloEMRDatabase = New DataBaseLayer

                    If TestType = OrderTestType.LabTests Then

                        _gloEMRDatabase.DBParametersCol.Clear()
                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.BigInt
                        objDBParameter.Value = PreferredId
                        objDBParameter.Name = "@LabCI_ID"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing
                        dtTests = _gloEMRDatabase.GetDataTable("gsp_GetLabOrderTests")

                        If Not IsNothing(_gloEMRDatabase) Then
                            _gloEMRDatabase.Dispose()
                            _gloEMRDatabase = Nothing
                        End If



                    ElseIf TestType = OrderTestType.RadiologyImaging Then

                        _gloEMRDatabase.DBParametersCol.Clear()
                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.BigInt
                        objDBParameter.Value = PreferredId
                        objDBParameter.Name = "@LabCI_ID"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing
                        dtTests = _gloEMRDatabase.GetDataTable("gsp_GetRadiologyImagingOrderTests")

                        If Not IsNothing(_gloEMRDatabase) Then
                            _gloEMRDatabase.Dispose()
                            _gloEMRDatabase = Nothing
                        End If



                    ElseIf TestType = OrderTestType.Other Then

                        _gloEMRDatabase.DBParametersCol.Clear()
                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.BigInt
                        objDBParameter.Value = PreferredId
                        objDBParameter.Name = "@LabCI_ID"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing
                        dtTests = _gloEMRDatabase.GetDataTable("gsp_OtherOrderTests")

                        If Not IsNothing(_gloEMRDatabase) Then
                            _gloEMRDatabase.Dispose()
                            _gloEMRDatabase = Nothing
                        End If



                    ElseIf TestType = OrderTestType.Referrals Then

                        _gloEMRDatabase.DBParametersCol.Clear()
                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.BigInt
                        objDBParameter.Value = PreferredId
                        objDBParameter.Name = "@LabCI_ID"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing

                        dtTests = _gloEMRDatabase.GetDataTable("gsp_ReferralLabOrderTests")

                        If Not IsNothing(_gloEMRDatabase) Then
                            _gloEMRDatabase.Dispose()
                            _gloEMRDatabase = Nothing
                        End If

                    ElseIf TestType = OrderTestType.PlannedOrder Then
                        Try

                        
                        _gloEMRDatabase.DBParametersCol.Clear()
                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.BigInt
                        objDBParameter.Value = PreferredId
                        objDBParameter.Name = "@nPatientID"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        objDBParameter = Nothing

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.VarChar
                        objDBParameter.Value = "LabOrder"
                        objDBParameter.Name = "@PlanType"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        dtTests = _gloEMRDatabase.GetDataTable("gsp_getPatientPlanOfTreatment_ByType")

                        If Not IsNothing(_gloEMRDatabase) Then
                            _gloEMRDatabase.Dispose()
                            _gloEMRDatabase = Nothing
                        End If
                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                        End Try
                    End If



                End If

                If (IsNothing(dtTests) = False) Then



                    For intTests As Integer = 0 To dtTests.Rows.Count - 1
                        oTest = New gloEMRActors.LabActor.Test

                        oTest.TestID = dtTests.Rows(intTests)("labtm_ID")
                        oTest.Code = dtTests.Rows(intTests)("labtm_Code")
                        oTest.Name = dtTests.Rows(intTests)("labtm_Name")
                        oTest.LOINCId = dtTests.Rows(intTests)("labtm_LOINCId")
                        oTests.Add(oTest)
                        oTest = Nothing
                    Next
                    dtTests.Dispose()
                    dtTests = Nothing
                End If
                Return oTests

            Catch ex As Exception
                Throw ex
                Return Nothing

            Finally

                If Not IsNothing(dtTests) Then
                    dtTests.Dispose()
                    dtTests = Nothing
                End If
                If Not IsNothing(oDB) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If
            End Try

        End Function



        Public Function GetTestsOrderDelete(Optional ByVal IncludeResults As Boolean = False) As gloEMRActors.LabActor.Tests

            Dim oDB As New DataBaseLayer

            _gloEMRDatabase = New DataBaseLayer

            Dim oTests As New gloEMRActors.LabActor.Tests
            Dim oTest As gloEMRActors.LabActor.Test
            Dim dt As DataTable = Nothing
            Dim dtResult As DataTable = Nothing



            Try
                '28-Apr-14 8020 Orders PRD: Show tests by Order Type
                dt = oDB.GetDataTable("gsp_GetLabOrderTests")

                'dt = _gloEMRDatabase.GetDataTable_Query("select labtm_ID,labtm_Code,labtm_Name,labtm_Ordarable,labtm_SpecimenID, " & _
                '                                        " labtm_CollectionID,labtm_StorageID,labtm_Instruction,labtm_Precuation,labtm_LOINCId, " & _
                '                                        " labtm_DeprtCatID,labtm_TestHeadID,labtm_ResultType " & _
                '                                        " from Lab_Test_Mst where labtm_Code is not null and labtm_Name is not null and labtm_ID is not null and isnull(sMUReportingCategory,'') <> 'Referral' ")
                If (IsNothing(dt) = False) Then


                    If dt.Rows.Count > 0 Then
                        For i As Integer = 0 To dt.Rows.Count - 1
                            oTest = New gloEMRActors.LabActor.Test

                            oTest.TestID = dt.Rows(i)("labtm_ID")
                            oTest.Code = dt.Rows(i)("labtm_Code")
                            oTest.Name = dt.Rows(i)("labtm_Name")

                            If IncludeResults = True Then



                                If Not IsDBNull(dt.Rows(i)("labtm_Ordarable")) Then
                                    oTest.Ordarable = dt.Rows(i)("labtm_Ordarable")
                                End If

                                If Not IsDBNull(dt.Rows(0)("labtm_SpecimenID")) Then
                                    oTest.Specimen = GetSpecimanName(dt.Rows(i)("labtm_SpecimenID"))
                                End If

                                If Not IsDBNull(dt.Rows(0)("labtm_CollectionID")) Then
                                    oTest.Collection = GetCollectionContainerName(dt.Rows(i)("labtm_CollectionID"))
                                End If
                                If Not IsDBNull(dt.Rows(0)("labtm_StorageID")) Then
                                    oTest.Storage = GetStorageTempratureName(dt.Rows(i)("labtm_StorageID"))
                                End If

                                If Not IsDBNull(dt.Rows(i)("labtm_Instruction")) Then
                                    oTest.Instruction = dt.Rows(i)("labtm_Instruction")
                                End If

                                If Not IsDBNull(dt.Rows(i)("labtm_Precuation")) Then
                                    oTest.Precaution = dt.Rows(i)("labtm_Precuation")
                                End If
                                If Not IsDBNull(dt.Rows(i)("labtm_LOINCId")) Then
                                    oTest.LOINCId = dt.Rows(i)("labtm_LOINCId")
                                End If
                                'If Not IsDBNull(dt.Rows(i)("labotrd_AlternateResultCode")) Then
                                '    oTest.AlternateResultCode = dt.Rows(i)("labotrd_AlternateResultCode")
                                'End If
                                If Not IsDBNull(dt.Rows(i)("labtm_DeprtCatID")) Then
                                    oTest.DepartmentCategoryID = dt.Rows(i)("labtm_DeprtCatID")
                                End If

                                If Not IsDBNull(dt.Rows(i)("labtm_TestHeadID")) Then
                                    oTest.TestHeadID = dt.Rows(i)("labtm_TestHeadID")
                                End If

                                If Not IsDBNull(dt.Rows(i)("labtm_ResultType")) Then
                                    oTest.ResultType = dt.Rows(i)("labtm_ResultType")
                                End If


                                'Details
                                Dim oTestResult As gloEMRActors.LabActor.TestResult


                                dtResult = _gloEMRDatabase.GetDataTable_Query("select labtrd_TestID,labtrd_ResultID,labtrd_ResultName " & _
                                                                              " labtrd_ValueType,labtrd_RefRange,labtrd_Comments " & _
                                                                              " from Lab_Test_ResultDtl where labtrd_ResultName is not null and labtrd_ValueType is not null and labtrd_ResultID is not null and  labtrd_TestID = " & oTest.TestID & " ORDER BY labtrd_ResultID")
                                If Not dtResult Is Nothing Then
                                    If dtResult.Rows.Count > 0 Then
                                        For j As Int64 = 0 To dtResult.Rows.Count - 1
                                            oTestResult = New gloEMRActors.LabActor.TestResult

                                            With oTestResult
                                                .TestID = dtResult.Rows(j)("labtrd_TestID")
                                                .ResultID = dtResult.Rows(j)("labtrd_ResultID")
                                                .ResultName = dtResult.Rows(j)("labtrd_ResultName")
                                                .ValueType = dtResult.Rows(j)("labtrd_ValueType")

                                                If Not IsDBNull(dtResult.Rows(j)("labtrd_RefRange")) Then
                                                    .ReferenceRange = dtResult.Rows(j)("labtrd_RefRange") & ""
                                                End If
                                                If Not IsDBNull(dtResult.Rows(j)("labtrd_Comments")) Then
                                                    .Comments = dtResult.Rows(j)("labtrd_Comments") & ""
                                                End If
                                            End With

                                            If Not oTestResult Is Nothing Then
                                                oTest.Results.Add(oTestResult)
                                            End If

                                            oTestResult = Nothing
                                        Next
                                    End If
                                    dtResult.Dispose()
                                    dtResult = Nothing
                                End If
                                dtResult = Nothing
                            End If

                            oTests.Add(oTest)

                            oTest = Nothing
                        Next
                    End If
                    dt.Dispose()
                    dt = Nothing
                End If
                Return oTests

            Catch ex As Exception
                Throw ex
                Return Nothing
            Finally
                If Not IsNothing(dtResult) Then
                    dtResult.Dispose()
                    dtResult = Nothing
                End If
                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
                If Not IsNothing(oDB) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If
            End Try

        End Function


        Public Function GetRefferalTestsOrder(Optional ByVal IncludeResults As Boolean = False, Optional ByVal PreferredID As Int64 = 0) As gloEMRActors.LabActor.Tests
            _gloEMRDatabase = New DataBaseLayer
            Dim objDBParameter As DBParameter
            Dim oTests As New gloEMRActors.LabActor.Tests
            Dim oTest As gloEMRActors.LabActor.Test
            Dim dt As DataTable = Nothing
            Dim dtResult As DataTable = Nothing
            Try
                Try
                    _gloEMRDatabase.DBParametersCol.Clear()
                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.BigInt
                    objDBParameter.Value = PreferredID
                    objDBParameter.Name = "@LabCI_ID"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                    objDBParameter = Nothing
                    dt = _gloEMRDatabase.GetDataTable("gsp_ReferralLabOrderTests")
                Catch ex As Exception
                    Throw ex
                    Return Nothing
                Finally
                    If Not IsNothing(_gloEMRDatabase) Then
                        _gloEMRDatabase.Dispose()
                        _gloEMRDatabase = Nothing
                    End If

                End Try



                If (IsNothing(dt) = False) Then


                    If dt.Rows.Count > 0 Then
                        For i As Integer = 0 To dt.Rows.Count - 1
                            oTest = New gloEMRActors.LabActor.Test

                            oTest.TestID = dt.Rows(i)("labtm_ID")
                            oTest.Code = dt.Rows(i)("labtm_Code")
                            oTest.Name = dt.Rows(i)("labtm_Name")

                            If IncludeResults = True Then



                                If Not IsDBNull(dt.Rows(i)("labtm_Ordarable")) Then
                                    oTest.Ordarable = dt.Rows(i)("labtm_Ordarable")
                                End If

                                If Not IsDBNull(dt.Rows(0)("labtm_SpecimenID")) Then
                                    oTest.Specimen = GetSpecimanName(dt.Rows(i)("labtm_SpecimenID"))
                                End If

                                If Not IsDBNull(dt.Rows(0)("labtm_CollectionID")) Then
                                    oTest.Collection = GetCollectionContainerName(dt.Rows(i)("labtm_CollectionID"))
                                End If
                                If Not IsDBNull(dt.Rows(0)("labtm_StorageID")) Then
                                    oTest.Storage = GetStorageTempratureName(dt.Rows(i)("labtm_StorageID"))
                                End If

                                If Not IsDBNull(dt.Rows(i)("labtm_Instruction")) Then
                                    oTest.Instruction = dt.Rows(i)("labtm_Instruction")
                                End If

                                If Not IsDBNull(dt.Rows(i)("labtm_Precuation")) Then
                                    oTest.Precaution = dt.Rows(i)("labtm_Precuation")
                                End If
                                If Not IsDBNull(dt.Rows(i)("labtm_LOINCId")) Then
                                    oTest.LOINCId = dt.Rows(i)("labtm_LOINCId")
                                End If
                                'If Not IsDBNull(dt.Rows(i)("labotrd_AlternateResultCode")) Then
                                '    oTest.AlternateResultCode = dt.Rows(i)("labotrd_AlternateResultCode")
                                'End If
                                If Not IsDBNull(dt.Rows(i)("labtm_DeprtCatID")) Then
                                    oTest.DepartmentCategoryID = dt.Rows(i)("labtm_DeprtCatID")
                                End If

                                If Not IsDBNull(dt.Rows(i)("labtm_TestHeadID")) Then
                                    oTest.TestHeadID = dt.Rows(i)("labtm_TestHeadID")
                                End If

                                If Not IsDBNull(dt.Rows(i)("labtm_ResultType")) Then
                                    oTest.ResultType = dt.Rows(i)("labtm_ResultType")
                                End If


                                'Details
                                Dim oTestResult As gloEMRActors.LabActor.TestResult


                                dtResult = _gloEMRDatabase.GetDataTable_Query("select labtrd_TestID,labtrd_ResultID,labtrd_ResultName " & _
                                                                              " labtrd_ValueType,labtrd_RefRange,labtrd_Comments " & _
                                                                              " from Lab_Test_ResultDtl where labtrd_ResultName is not null and labtrd_ValueType is not null and labtrd_ResultID is not null and  labtrd_TestID = " & oTest.TestID & " ORDER BY labtrd_ResultID")
                                If Not dtResult Is Nothing Then
                                    If dtResult.Rows.Count > 0 Then
                                        For j As Int64 = 0 To dtResult.Rows.Count - 1
                                            oTestResult = New gloEMRActors.LabActor.TestResult

                                            With oTestResult
                                                .TestID = dtResult.Rows(j)("labtrd_TestID")
                                                .ResultID = dtResult.Rows(j)("labtrd_ResultID")
                                                .ResultName = dtResult.Rows(j)("labtrd_ResultName")
                                                .ValueType = dtResult.Rows(j)("labtrd_ValueType")

                                                If Not IsDBNull(dtResult.Rows(j)("labtrd_RefRange")) Then
                                                    .ReferenceRange = dtResult.Rows(j)("labtrd_RefRange") & ""
                                                End If
                                                If Not IsDBNull(dtResult.Rows(j)("labtrd_Comments")) Then
                                                    .Comments = dtResult.Rows(j)("labtrd_Comments") & ""
                                                End If
                                            End With

                                            If Not oTestResult Is Nothing Then
                                                oTest.Results.Add(oTestResult)
                                            End If

                                            oTestResult = Nothing
                                        Next
                                    End If
                                    dtResult.Dispose()
                                    dtResult = Nothing
                                End If
                                dtResult = Nothing
                            End If

                            oTests.Add(oTest)

                            oTest = Nothing
                        Next
                    End If
                    dt.Dispose()
                    dt = Nothing
                End If
                Return oTests

            Catch ex As Exception
                Throw ex
                Return Nothing
            Finally
                If Not IsNothing(dtResult) Then
                    dtResult.Dispose()
                    dtResult = Nothing
                End If
                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try

        End Function

        '//---<<< PRIVATE FUNCTIONS >>>---//
        Private Function GetSpecimanID(ByVal Specimen As String) As Int64
            Dim _ID As String = ""
            Dim _Result As Int64 = 0

            Dim _gloEMRDBID As New DataBaseLayer
            Try
                _ID = _gloEMRDBID.GetRecord_Query("SELECT labsm_ID FROM Lab_Specimen_Mst WHERE Upper(labsm_Name) = '" & Specimen.ToUpper & "'")

                If Val(_ID) > 0 Then
                    _Result = _ID
                End If

            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try

            Return _Result
        End Function

        Private Function GetCollectionContainerID(ByVal CollectionContainer As String) As Int64
            Dim _ID As String = ""
            Dim _Result As Int64 = 0

            Dim _gloEMRDBID As New DataBaseLayer
            Try
                _ID = _gloEMRDBID.GetRecord_Query("SELECT labcm_ID FROM Lab_Collection_Mst WHERE Upper(labcm_Name) = '" & CollectionContainer.ToUpper & "'")

                If Val(_ID) > 0 Then
                    _Result = _ID
                End If


            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try

            Return _Result
        End Function

        Private Function GetStorageTempratureID(ByVal StorageTemprature As String) As Int64
            Dim _ID As String = ""
            Dim _Result As Int64 = 0

            Dim _gloEMRDBID As New DataBaseLayer
            Try
                '_ID = _gloEMRDBID.GetRecord_Query("SELECT labstm_ID FROM Lab_StorageTemp_Mst WHERE Upper(labstm_Name) = '" & StorageTemprature.ToUpper & "'")
                _ID = _gloEMRDBID.GetRecord_Query("SELECT labCSST_ID FROM Lab_CSST_MST WHERE Upper(labCSST_Name) = '" & StorageTemprature.ToUpper & "'")

                If Val(_ID) > 0 Then
                    _Result = _ID
                End If


            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try
            Return _Result
        End Function


        Private Function GetSpecimanName(ByVal SpecimenID As Int64) As String
            Dim _Result As String = ""

            Dim _gloEMRDBID As New DataBaseLayer
            Try
                _Result = _gloEMRDBID.GetRecord_Query("SELECT  labsm_Name FROM Lab_Specimen_Mst WHERE labsm_ID = " & SpecimenID & " ").Trim

            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try

            Return _Result

        End Function

        Private Function GetCollectionContainerName(ByVal CollectionContainerID As Int64) As String
            Dim _Result As String = ""
            Dim _gloEMRDBID As New DataBaseLayer
            Try
                _Result = _gloEMRDBID.GetRecord_Query("SELECT  labcm_Name FROM Lab_Collection_Mst WHERE labcm_ID = " & CollectionContainerID & " ").Trim
            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try
            Return _Result
        End Function

        Private Function GetStorageTempratureName(ByVal StorageTempratureID As Int64) As String
            Dim _Result As String = ""

            Dim _gloEMRDBID As New DataBaseLayer
            Try
                _Result = _gloEMRDBID.GetRecord_Query("SELECT  labstm_Name FROM Lab_StorageTemp_Mst WHERE labstm_ID = " & StorageTempratureID & " ").Trim

            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try
            Return _Result
        End Function

        'its unique function...ref date means Patient Date Of Birth, Current Date, Order Date, etc
        Private Function GetPrefixTransactionID(ByVal ReferenceDate As DateTime) As Long
            Dim strID As String
            Dim dtDate As DateTime
            Try
                dtDate = System.DateTime.Now
                strID = DateDiff(DateInterval.Day, CDate("1/1/1900"), dtDate.Date) & DateDiff(DateInterval.Second, dtDate.Date, dtDate) & DateDiff(DateInterval.Day, CDate("1/1/1900"), ReferenceDate.Date)

            Catch ex As Exception
                Throw ex
            End Try
            Return CLng(strID)
        End Function


        Public Sub New()
            MyBase.New()
            _LabActor = New gloEMRActors.LabActor.Test

        End Sub


        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
                    If (IsNothing(_LabActor) = False) Then
                        If (LabActorAssigned) Then
                            If (IsNothing(_LabActor) = False) Then
                                _LabActor.Dispose()
                                _LabActor = Nothing
                            End If
                            LabActorAssigned = False
                        End If

                    End If
                End If
                '_LabActor = Nothing
                ' TODO: free shared unmanaged resources
            End If
            Me.disposedValue = True
        End Sub

#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class

    Public Class gloEMRLabSpecimen
        Implements IDisposable

        Private _LabSpecimen As gloEMRActors.LabActor.LabSpecimen
        Private bLabSpecimen As Boolean = True
        Private _Exception As gloEMRLabExceptions

        Dim _gloEMRDatabase As DataBaseLayer


        Public Property LabSpecimen() As gloEMRActors.LabActor.LabSpecimen
            Get
                Return _LabSpecimen
            End Get
            Set(ByVal value As gloEMRActors.LabActor.LabSpecimen)
                If (bLabSpecimen) Then
                    If (IsNothing(_LabSpecimen) = False) Then
                        _LabSpecimen.Dispose()
                        _LabSpecimen = Nothing
                    End If
                    bLabSpecimen = False
                End If
                _LabSpecimen = value
            End Set
        End Property

        Public Function Add() As Int64
            _gloEMRDatabase = New DataBaseLayer
            Dim objDBParameter As DBParameter
            Dim _LabSpecimenID As Int64 = 0

            Try
                _gloEMRDatabase.DBParametersCol.Clear()
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = GetPrefixTransactionID(Date.Now)
                objDBParameter.Name = "@MachineID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabSpecimen.LabSpecimenCode
                objDBParameter.Name = "@labsm_Code"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabSpecimen.LabSpecimenName
                objDBParameter.Name = "@labsm_Name"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                _LabSpecimenID = _gloEMRDatabase.Add("Lab_InsertSpecimen")

            Catch ex As Exception
                _Exception = New gloEMRLabExceptions
                _Exception.ErrorMessage = ex.Message
                Throw _Exception
                _Exception.Dispose()
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
            Return Nothing
        End Function

        Public Function Modify(ByVal SpecimenID As Int64) As Boolean
            _gloEMRDatabase = New DataBaseLayer
            Dim objDBParameter As DBParameter
            Dim _LabSpecimenID As Int64

            Try
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = SpecimenID
                objDBParameter.Name = "@SpecimenID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabSpecimen.LabSpecimenCode
                objDBParameter.Name = "@labsm_Code"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabSpecimen.LabSpecimenName
                objDBParameter.Name = "@labsm_Name"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                _LabSpecimenID = _gloEMRDatabase.Add("Lab_UpdateSpecimen")


            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
            Return Nothing
        End Function

        Public Function Delete(ByVal SpecimenID As Int64) As Boolean
            Dim _gloEMRDBID As New DataBaseLayer
            Try
                _gloEMRDBID.Delete_Query("delete   FROM Lab_Specimen_Mst WHERE labsm_ID = " & SpecimenID)

            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try
            Return True
        End Function

        Public Function IsExists(ByVal SpecimenName As String) As Boolean
            Dim _ID As String = ""
            Dim _Result As Boolean = False

            Dim _gloEMRDBID As New DataBaseLayer

            Try
                _ID = _gloEMRDBID.GetRecord_Query("SELECT labsm_ID FROM Lab_Specimen_Mst WHERE Upper(labsm_Name) = '" & SpecimenName.ToUpper & "'")

                If Val(_ID) > 0 Then
                    _Result = True
                End If
            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try
            Return _Result
        End Function

        Public Function IsCodeExists(ByVal SpecimenCode As String) As Boolean
            Dim _ID As String = ""
            Dim _Result As Boolean = False
            Dim _gloEMRDBID As New DataBaseLayer

            Try
                _ID = _gloEMRDBID.GetRecord_Query("SELECT labsm_ID FROM Lab_Specimen_Mst WHERE Upper(labsm_Code) = '" & SpecimenCode.ToUpper & "'")

                If Val(_ID) > 0 Then
                    _Result = True
                End If
            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try
            Return _Result
        End Function

        Public Function GetSpecimen(ByVal SpecimenID As Int64) As gloEMRActors.LabActor.LabSpecimen
            _gloEMRDatabase = New DataBaseLayer

            Dim oSpecimen As gloEMRActors.LabActor.LabSpecimen
            Dim dt As DataTable = Nothing

            Try
                dt = _gloEMRDatabase.GetDataTable_Query("select labsm_ID,labsm_Code,labsm_Name from Lab_Specimen_Mst where labsm_Code is not null and labsm_Name is not null and labsm_ID is not null and labsm_ID =" & SpecimenID)
                oSpecimen = New gloEMRActors.LabActor.LabSpecimen
                If Not dt Is Nothing Then
                    oSpecimen.LabSpecimenID = dt.Rows(0)("labsm_ID")
                    oSpecimen.LabSpecimenCode = dt.Rows(0)("labsm_Code")
                    oSpecimen.LabSpecimenName = dt.Rows(0)("labsm_Name")
                    dt.Dispose()
                    dt = Nothing
                End If
                Return oSpecimen
            Catch ex As Exception
                Throw ex
                Return Nothing
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try

        End Function

        Public Function GetSpecimens() As gloEMRActors.LabActor.LabSpecimens
            _gloEMRDatabase = New DataBaseLayer

            Dim oSpecimens As New gloEMRActors.LabActor.LabSpecimens
            Dim oSpecimen As gloEMRActors.LabActor.LabSpecimen
            Dim dt As DataTable = Nothing

            Try
                dt = _gloEMRDatabase.GetDataTable_Query("select labsm_ID,labsm_Code,labsm_Name from Lab_Specimen_Mst where labsm_Code is not null and labsm_Name is not null and labsm_ID is not null")
                If Not dt Is Nothing Then
                    For i As Int16 = 0 To dt.Rows.Count - 1
                        oSpecimen = New gloEMRActors.LabActor.LabSpecimen
                        oSpecimen.LabSpecimenID = dt.Rows(i)("labsm_ID")
                        oSpecimen.LabSpecimenCode = dt.Rows(i)("labsm_Code")
                        oSpecimen.LabSpecimenName = dt.Rows(i)("labsm_Name")
                        If Not oSpecimen Is Nothing Then
                            oSpecimens.Add(oSpecimen)
                        End If
                        oSpecimen = Nothing
                    Next
                    dt.Dispose()
                    dt = Nothing
                End If
                Return oSpecimens
            Catch ex As Exception
                Throw ex
                Return oSpecimens
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try

        End Function

        Private Function GetSpecimanID(ByVal Specimen As String) As Integer
            Dim _ID As String = ""
            Dim _Result As Integer = 0
            Dim _gloEMRDBID As New DataBaseLayer
            Try
                _ID = _gloEMRDBID.GetRecord_Query("SELECT labsm_ID FROM Lab_Specimen_Mst WHERE Upper(labsm_Name) = '" & Specimen.ToUpper & "'")

                If Val(_ID) > 0 Then
                    _Result = CInt(_ID)
                End If
            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try

            Return _Result
        End Function

        Private Function GetSpecimanName(ByVal SpecimenID As Integer) As String
            Dim _Result As String = ""

            Dim _gloEMRDBID As New DataBaseLayer

            Try
                _Result = _gloEMRDBID.GetRecord_Query("SELECT  labsm_Name FROM Lab_Specimen_Mst WHERE labsm_ID = " & SpecimenID & " ").Trim
            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try

            Return _Result

        End Function


        'its unique function...ref date means Patient Date Of Birth, Current Date, Order Date, etc
        Private Function GetPrefixTransactionID(ByVal ReferenceDate As DateTime) As Long
            Dim strID As String
            Dim dtDate As DateTime

            Try
                dtDate = System.DateTime.Now
                strID = DateDiff(DateInterval.Day, CDate("1/1/1900"), dtDate.Date) & DateDiff(DateInterval.Second, dtDate.Date, dtDate) & DateDiff(DateInterval.Day, CDate("1/1/1900"), ReferenceDate.Date)

            Catch ex As Exception
                Throw ex
            End Try
            Return CLng(strID)
        End Function


        Public Sub New()
            MyBase.New()
            _LabSpecimen = New gloEMRActors.LabActor.LabSpecimen
        End Sub


        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
                    If (bLabSpecimen) Then
                        If (IsNothing(_LabSpecimen) = False) Then
                            _LabSpecimen.Dispose()
                            _LabSpecimen = Nothing
                        End If
                        bLabSpecimen = False
                    End If
                 
                End If

                ' TODO: free shared unmanaged resources
            End If
            Me.disposedValue = True
        End Sub

#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class

    Public Class gloEMRLabCollectionContainer
        Implements IDisposable
        Private _LabCollectionContainer As gloEMRActors.LabActor.LabCollectionContainer
        Private bLabCollectionContainer As Boolean = True
        Private _Exception As gloEMRLabExceptions

        Dim _gloEMRDatabase As DataBaseLayer


        Public Property LabCollectionContainer() As gloEMRActors.LabActor.LabCollectionContainer
            Get
                Return _LabCollectionContainer
            End Get
            Set(ByVal value As gloEMRActors.LabActor.LabCollectionContainer)
                If (bLabCollectionContainer) Then
                    If (IsNothing(_LabCollectionContainer) = False) Then
                        _LabCollectionContainer.Dispose()
                        _LabCollectionContainer = Nothing

                    End If
                    bLabCollectionContainer = False
                 End If
                _LabCollectionContainer = value
            End Set
        End Property

        Public Function Add() As Int64
            _gloEMRDatabase = New DataBaseLayer
            Dim objDBParameter As DBParameter
            Dim _LabSpecimenID As Int64 = 0

            Try
                _gloEMRDatabase.DBParametersCol.Clear()
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = GetPrefixTransactionID(Date.Now)
                objDBParameter.Name = "@MachineID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabCollectionContainer.LabCollectionContainerCode
                objDBParameter.Name = "@labcm_Code"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabCollectionContainer.LabCollectionContainerName
                objDBParameter.Name = "@labcm_Name"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                _LabSpecimenID = _gloEMRDatabase.Add("Lab_InsertCollectionContainerName")

            Catch ex As Exception
                _Exception = New gloEMRLabExceptions
                _Exception.ErrorMessage = ex.Message
                Throw _Exception
                _Exception.Dispose()
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
            Return Nothing
        End Function

        Public Function Modify(ByVal CollectionID As Int64) As Boolean
            _gloEMRDatabase = New DataBaseLayer
            Dim objDBParameter As DBParameter
            Dim _LabCollectionID As Int64

            Try
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = CollectionID
                objDBParameter.Name = "@CollectionID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabCollectionContainer.LabCollectionContainerCode
                objDBParameter.Name = "@labcm_Code"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabCollectionContainer.LabCollectionContainerName
                objDBParameter.Name = "@labcm_Name"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                _LabCollectionID = _gloEMRDatabase.Add("Lab_UpdateCollectionContainer")


            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
            Return Nothing
        End Function

        Public Function Delete(ByVal CollectionID As Int64) As Boolean
            Dim _gloEMRDBID As New DataBaseLayer
            Try
                _gloEMRDBID.Delete_Query("delete   FROM Lab_Collection_Mst WHERE labcm_ID = " & CollectionID)
            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try
            Return True
        End Function

        Public Function IsExists(ByVal CollectionName As String) As Boolean
            Dim _ID As String = ""
            Dim _Result As Boolean = False

            Dim _gloEMRDBID As New DataBaseLayer

            Try
                _ID = _gloEMRDBID.GetRecord_Query("SELECT labcm_ID FROM Lab_Collection_Mst WHERE Upper(labcm_Name) = '" & CollectionName.ToUpper & "'")

                If Val(_ID) > 0 Then
                    _Result = True
                End If
            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try

            '_gloEMRDatabase.Dispose()
            Return _Result
        End Function
        Public Function IsCodeExists(ByVal CollectionCode As String) As Boolean
            Dim _ID As String = ""
            Dim _Result As Boolean = False

            Dim _gloEMRDBID As New DataBaseLayer

            Try
                _ID = _gloEMRDBID.GetRecord_Query("SELECT labcm_ID FROM Lab_Collection_Mst WHERE Upper(labcm_Code) = '" & CollectionCode.ToUpper & "'")

                If Val(_ID) > 0 Then
                    _Result = True
                End If

            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try

            '_gloEMRDatabase.Dispose()
            Return _Result
        End Function

        Public Function GetCollectionContainer(ByVal CollectionID As Int64) As gloEMRActors.LabActor.LabCollectionContainer
            _gloEMRDatabase = New DataBaseLayer

            Dim oCollection As gloEMRActors.LabActor.LabCollectionContainer
            Dim dt As DataTable = Nothing

            Try
                dt = _gloEMRDatabase.GetDataTable_Query("select labcm_ID,labcm_Code,labcm_Name from Lab_Collection_Mst where labcm_Code is not null and labcm_Name is not null and labcm_ID is not null and labcm_ID =" & CollectionID)
                oCollection = New gloEMRActors.LabActor.LabCollectionContainer
                If Not dt Is Nothing Then
                    If dt.Rows.Count > 0 Then
                        oCollection.LabCollectionContainerID = dt.Rows(0)("labcm_ID")
                        oCollection.LabCollectionContainerCode = dt.Rows(0)("labcm_Code")
                        oCollection.LabCollectionContainerName = dt.Rows(0)("labcm_Name")
                    End If
                    dt.Dispose()
                    dt = Nothing
                End If
                Return oCollection
            Catch ex As Exception
                Throw ex
                Return Nothing
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try

        End Function

        Public Function GetCollectionContainers() As gloEMRActors.LabActor.LabCollectionContainers
            _gloEMRDatabase = New DataBaseLayer

            Dim oCollections As New gloEMRActors.LabActor.LabCollectionContainers
            Dim oCollection As gloEMRActors.LabActor.LabCollectionContainer
            Dim dt As DataTable = Nothing

            Try
                'dt = _gloEMRDatabase.GetDataTable_Query("select * from Lab_Collection_Mst where labcm_Code is not null and labcm_Name is not null and labcm_ID is not null")
                dt = _gloEMRDatabase.GetDataTable_Query("select labcm_ID,labcm_Code,labcm_Name from Lab_Collection_Mst where labcm_Code is not null and labcm_Name is not null and labcm_ID is not null")
                If Not dt Is Nothing Then
                    For i As Int16 = 0 To dt.Rows.Count - 1
                        oCollection = New gloEMRActors.LabActor.LabCollectionContainer
                        oCollection.LabCollectionContainerID = dt.Rows(i)("labcm_ID")
                        oCollection.LabCollectionContainerCode = dt.Rows(i)("labcm_Code")
                        oCollection.LabCollectionContainerName = dt.Rows(i)("labcm_Name")
                        If Not oCollection Is Nothing Then
                            oCollections.Add(oCollection)
                        End If
                        oCollection = Nothing
                    Next
                    dt.Dispose()
                    dt = Nothing
                End If
                Return oCollections
            Catch ex As Exception
                Throw ex
                Return oCollections
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try

        End Function

        Private Function GetCollectionID(ByVal CollectionName As String) As Integer
            Dim _ID As String = ""
            Dim _Result As Integer = 0

            Dim _gloEMRDBID As New DataBaseLayer

            Try
                _ID = _gloEMRDBID.GetRecord_Query("SELECT labcm_ID FROM Lab_Collection_Mst WHERE Upper(labcm_Name) = '" & CollectionName.ToUpper & "'")

                If Val(_ID) > 0 Then
                    _Result = CInt(_ID)
                End If
            Catch ex As Exception
                Throw ex
            Finally

                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try

            Return _Result
        End Function

        Private Function GetSpecimanName(ByVal SpecimenID As Integer) As String
            Dim _Result As String = ""

            Dim _gloEMRDBID As New DataBaseLayer

            Try
                _Result = _gloEMRDBID.GetRecord_Query("SELECT  labsm_Name FROM Lab_Specimen_Mst WHERE labsm_ID = " & SpecimenID & " ").Trim
            Catch ex As Exception
                Throw ex
            Finally

                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try


            '_gloEMRDatabase.Dispose()
            Return _Result

        End Function


        'its uniqe function...ref date means Patient Date Of Birth, Current Date, Order Date, etc
        Private Function GetPrefixTransactionID(ByVal ReferenceDate As DateTime) As Long
            Dim strID As String
            Dim dtDate As DateTime

            Try
                dtDate = System.DateTime.Now
                strID = DateDiff(DateInterval.Day, CDate("1/1/1900"), dtDate.Date) & DateDiff(DateInterval.Second, dtDate.Date, dtDate) & DateDiff(DateInterval.Day, CDate("1/1/1900"), ReferenceDate.Date)
            Catch ex As Exception
                Throw ex
            End Try

            Return CLng(strID)
        End Function


        Public Sub New()
            MyBase.New()
            _LabCollectionContainer = New gloEMRActors.LabActor.LabCollectionContainer
        End Sub


        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
                    If (bLabCollectionContainer) Then
                        If (IsNothing(_LabCollectionContainer) = False) Then
                            _LabCollectionContainer.Dispose()
                            _LabCollectionContainer = Nothing

                        End If
                        bLabCollectionContainer = False
                    End If
                End If

                ' TODO: free shared unmanaged resources
            End If
            Me.disposedValue = True
        End Sub




#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class

    Public Class gloEMRLabStorageTemperature
        Implements IDisposable
        Private _LabStorageTemperature As gloEMRActors.LabActor.LabStorageTemperature
        Private bLabStorageTemperature As Boolean = True
        Private _Exception As gloEMRLabExceptions

        Dim _gloEMRDatabase As DataBaseLayer


        Public Property LabStorageTemperature() As gloEMRActors.LabActor.LabStorageTemperature
            Get
                Return _LabStorageTemperature
            End Get
            Set(ByVal value As gloEMRActors.LabActor.LabStorageTemperature)
                If (bLabStorageTemperature) Then
                    If (IsNothing(_LabStorageTemperature) = False) Then
                        _LabStorageTemperature.Dispose()
                        _LabStorageTemperature = Nothing
                    End If
                    bLabStorageTemperature = False
                End If
                _LabStorageTemperature = value
            End Set
        End Property

        Public Function Add() As Int64
            _gloEMRDatabase = New DataBaseLayer
            Dim objDBParameter As DBParameter
            Dim _LabStorageTempID As Int64 = 0

            Try
                _gloEMRDatabase.DBParametersCol.Clear()
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = GetPrefixTransactionID(Date.Now)
                objDBParameter.Name = "@MachineID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabStorageTemperature.LabStorageTemperatureCode
                objDBParameter.Name = "@labstm_Code"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabStorageTemperature.LabStorageTemperatureName
                objDBParameter.Name = "@labstm_Name"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)


                _LabStorageTempID = _gloEMRDatabase.Add("Lab_InsertStorageTemperature")

            Catch ex As Exception
                _Exception = New gloEMRLabExceptions
                _Exception.ErrorMessage = ex.Message
                Throw _Exception
                _Exception.Dispose()
            Finally

                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try


            Return Nothing
        End Function

        Public Function Modify(ByVal StorageTempID As Int64) As Boolean
            _gloEMRDatabase = New DataBaseLayer
            Dim objDBParameter As DBParameter
            Dim _LabStorageTempID As Int64

            Try
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = StorageTempID
                objDBParameter.Name = "@StorageTempID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)


                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabStorageTemperature.LabStorageTemperatureCode
                objDBParameter.Name = "@labstm_Code"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabStorageTemperature.LabStorageTemperatureName
                objDBParameter.Name = "@labstm_Name"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)


                _LabStorageTempID = _gloEMRDatabase.Add("Lab_UpdateStorageTemperature")


            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
            Return Nothing
        End Function

        Public Function Delete(ByVal StorageTempID As Int64) As Boolean
            Dim _gloEMRDBID As New DataBaseLayer

            Try
                _gloEMRDBID.Delete_Query("delete   FROM Lab_StorageTemp_Mst WHERE labstm_ID = " & StorageTempID)
            Catch ex As Exception
                Throw ex
            Finally

                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try
            'delete detail record 


            '_gloEMRDatabase.Dispose()
            Return True
        End Function

        Public Function IsExists(ByVal StorageTempName As String) As Boolean
            Dim _ID As String = ""
            Dim _Result As Boolean = False

            Dim _gloEMRDBID As New DataBaseLayer

            Try
                _ID = _gloEMRDBID.GetRecord_Query("SELECT labstm_ID FROM Lab_StorageTemp_Mst WHERE Upper(labstm_Name) = '" & StorageTempName.ToUpper & "'")

                If Val(_ID) > 0 Then
                    _Result = True
                End If

            Catch ex As Exception
                Throw ex
            Finally

                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try

            '_gloEMRDatabase.Dispose()
            Return _Result
        End Function

        Public Function IsCodeExists(ByVal StorageTempCode As String) As Boolean
            Dim _ID As String = ""
            Dim _Result As Boolean = False

            Dim _gloEMRDBID As New DataBaseLayer
            Try
                _ID = _gloEMRDBID.GetRecord_Query("SELECT labstm_ID FROM Lab_StorageTemp_Mst WHERE Upper(labstm_Code) = '" & StorageTempCode.ToUpper & "'")

                If Val(_ID) > 0 Then
                    _Result = True
                End If

            Catch ex As Exception
                Throw ex
            Finally

                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try

            '_gloEMRDatabase.Dispose()
            Return _Result
        End Function


        Public Function IsDelete(ByVal StorageTempName As String) As Boolean
            Dim _ID As String = ""
            Dim _Result As Boolean = True ' False

            'Dim _gloEMRDBID As New DataBaseLayer
            '_ID = _gloEMRDBID.GetRecord_Query("SELECT labstm_ID FROM Lab_StorageTemp_Mst WHERE Upper(labstm_Name) = '" & StorageTempName.ToUpper & "'")

            'If Val(_ID) > 0 Then
            '    _Result = True
            'End If

            '_gloEMRDatabase.Dispose()
            Return _Result
        End Function


        Public Function GetStorageTemperature(ByVal StorageTempID As Int64) As gloEMRActors.LabActor.LabStorageTemperature
            _gloEMRDatabase = New DataBaseLayer

            Dim oStorageTemp As gloEMRActors.LabActor.LabStorageTemperature
            Dim dt As DataTable = Nothing

            Try
                ' dt = _gloEMRDatabase.GetDataTable_Query("select * from Lab_StorageTemp_Mst where labstm_Code is not null and labstm_Name is not null and labstm_ID is not null and labstm_ID =" & StorageTempID)
                dt = _gloEMRDatabase.GetDataTable_Query("select labstm_ID,labstm_Code,labstm_Name from Lab_StorageTemp_Mst where labstm_Code is not null and labstm_Name is not null and labstm_ID is not null and labstm_ID =" & StorageTempID)
                oStorageTemp = New gloEMRActors.LabActor.LabStorageTemperature
                If Not dt Is Nothing Then
                    oStorageTemp.LabStorageTemperatureID = dt.Rows(0)("labstm_ID")
                    oStorageTemp.LabStorageTemperatureCode = dt.Rows(0)("labstm_Code")
                    oStorageTemp.LabStorageTemperatureName = dt.Rows(0)("labstm_Name")
                    dt.Dispose()
                    dt = Nothing
                End If
                Return oStorageTemp
            Catch ex As Exception
                Throw ex
                Return oStorageTemp

            Finally

                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try

        End Function

        Public Function GetStorageTemperatures() As gloEMRActors.LabActor.LabStorageTemperatures
            _gloEMRDatabase = New DataBaseLayer

            Dim oStorageTemps As New gloEMRActors.LabActor.LabStorageTemperatures
            Dim oStorageTemp As gloEMRActors.LabActor.LabStorageTemperature
            Dim dt As DataTable = Nothing

            Try
                'dt = _gloEMRDatabase.GetDataTable_Query("select * from Lab_StorageTemp_Mst where labstm_Code is not null and labstm_Name is not null and labstm_ID is not null")
                dt = _gloEMRDatabase.GetDataTable_Query("select labstm_ID,labstm_Code,labstm_Name from Lab_StorageTemp_Mst where labstm_Code is not null and labstm_Name is not null and labstm_ID is not null")
                If Not dt Is Nothing Then
                    For i As Int16 = 0 To dt.Rows.Count - 1
                        oStorageTemp = New gloEMRActors.LabActor.LabStorageTemperature
                        oStorageTemp.LabStorageTemperatureID = dt.Rows(i)("labstm_ID")
                        oStorageTemp.LabStorageTemperatureCode = dt.Rows(i)("labstm_Code")
                        oStorageTemp.LabStorageTemperatureName = dt.Rows(i)("labstm_Name")
                        If Not oStorageTemp Is Nothing Then
                            oStorageTemps.Add(oStorageTemp)
                        End If
                        oStorageTemp = Nothing
                    Next
                    dt.Dispose()
                    dt = Nothing
                End If
                Return oStorageTemps
            Catch ex As Exception
                Throw ex
                Return oStorageTemps
            Finally

                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If

            End Try

        End Function

        Private Function GetStorageTempID(ByVal StorageTemperature As String) As Integer
            Dim _ID As String = ""
            Dim _Result As Integer = 0

            Dim _gloEMRDBID As New DataBaseLayer

            Try
                _ID = _gloEMRDBID.GetRecord_Query("SELECT labstm_ID FROM Lab_StorageTemp_Mst WHERE Upper(labstm_Name) = '" & StorageTemperature.ToUpper & "'")

                If Val(_ID) > 0 Then
                    _Result = CInt(_ID)
                End If
            Catch ex As Exception
                Throw ex
            Finally

                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try


            '_gloEMRDatabase.Dispose()
            Return _Result
        End Function

        Private Function GetStorageTempName(ByVal StorageTemperatureID As Integer) As String
            Dim _Result As String = ""

            Dim _gloEMRDBID As New DataBaseLayer

            Try


                _Result = _gloEMRDBID.GetRecord_Query("SELECT  labstm_Name FROM Lab_StorageTemp_Mst WHERE labstm_ID = " & StorageTemperatureID & " ").Trim
            Catch ex As Exception
                Throw ex
            Finally

                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try
            '_gloEMRDatabase.Dispose()
            Return _Result

        End Function


        'its uniqe function...ref date means Patient Date Of Birth, Current Date, Order Date, etc
        Private Function GetPrefixTransactionID(ByVal ReferenceDate As DateTime) As Long
            Dim strID As String
            Dim dtDate As DateTime

            Try
                dtDate = System.DateTime.Now
                strID = DateDiff(DateInterval.Day, CDate("1/1/1900"), dtDate.Date) & DateDiff(DateInterval.Second, dtDate.Date, dtDate) & DateDiff(DateInterval.Day, CDate("1/1/1900"), ReferenceDate.Date)

            Catch ex As Exception
                Throw ex
            End Try
            Return CLng(strID)
        End Function


        Public Sub New()
            MyBase.New()
            _LabStorageTemperature = New gloEMRActors.LabActor.LabStorageTemperature
        End Sub


        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
                    If (bLabStorageTemperature) Then
                        If (IsNothing(_LabStorageTemperature) = False) Then
                            _LabStorageTemperature.Dispose()
                            _LabStorageTemperature = Nothing
                        End If
                        bLabStorageTemperature = False
                    End If
                End If
                '_LabStorageTemperature = Nothing
                ' TODO: free shared unmanaged resources
            End If
            Me.disposedValue = True
        End Sub




#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class

#Region "Specimen, Collection, Storage Temperature"
    '//new common class created for Collection, Specimen and storage temp
    Public Class gloEMRLabCSST
        Implements IDisposable

        ''Public Enum LabCCSTType
        ''    Specimen = 1
        ''    Collection = 2
        ''    StorageTemperature = 3
        ''End Enum

        Private mLabCSST As gloEMRActors.LabActor.LabCSST
        Private _Exception As gloEMRLabExceptions

        Dim _gloEMRDatabase As DataBaseLayer


        Public Property LabCSST() As gloEMRActors.LabActor.LabCSST
            Get
                Return mLabCSST
            End Get
            Set(ByVal value As gloEMRActors.LabActor.LabCSST)
                mLabCSST = value
            End Set
        End Property

        Public Function Add() As Int64
            _gloEMRDatabase = New DataBaseLayer
            Dim objDBParameter As DBParameter
            Dim mLabCSST_ID As Int64 = 0

            Try
                _gloEMRDatabase.DBParametersCol.Clear()
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = GetPrefixTransactionID(Date.Now)
                objDBParameter.Name = "@MachineID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = LabCSST.LabCSST_Code
                objDBParameter.Name = "@labCSST_Code"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = LabCSST.LabCSST_Name
                objDBParameter.Name = "@labCSST_Name"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                '\\enum type
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.Int
                objDBParameter.Value = LabCSST.LabCSST_Type
                objDBParameter.Name = "@labCSST_Type"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                '\\nclinicid
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = LabCSST.nClinicID
                objDBParameter.Name = "@nClinicID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)


                mLabCSST_ID = _gloEMRDatabase.Add("Lab_InsertCSST")

            Catch ex As Exception
                _Exception = New gloEMRLabExceptions
                _Exception.ErrorMessage = ex.Message
                Throw _Exception
                _Exception.Dispose()
            Finally

                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If

            End Try
            Return Nothing
        End Function

        Public Function Modify(ByVal nlabCSST_ID As Int64, ByVal enumtype As Int16) As Boolean
            _gloEMRDatabase = New DataBaseLayer
            Dim objDBParameter As DBParameter
            Dim mnlabCSST_ID As Int64

            Try
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = nlabCSST_ID
                objDBParameter.Name = "@labCSST_ID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)


                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = LabCSST.LabCSST_Code
                objDBParameter.Name = "@labCSST_Code"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = LabCSST.LabCSST_Name
                objDBParameter.Name = "@labCSST_Name"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                '\\enum type
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.Int
                objDBParameter.Value = enumtype
                objDBParameter.Name = "@labCSST_Type"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                '\\nclinicid
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = LabCSST.nClinicID
                objDBParameter.Name = "@nClinicID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)


                mnlabCSST_ID = _gloEMRDatabase.Add("Lab_UpdateCSST")


            Catch ex As Exception
                Throw ex
            Finally

                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If

            End Try
            Return Nothing
        End Function

        Public Function Delete(ByVal nlabCSST_ID As Int64, ByVal type As Int16) As Boolean
            Dim _gloEMRDBID As New DataBaseLayer


            Try
                _gloEMRDBID.Delete_Query("delete  FROM Lab_CSST_MST WHERE labCSST_ID = " & nlabCSST_ID & " and labCSST_Type=" & type & "")

            Catch ex As Exception
                Throw ex
            Finally

                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try


            Return True

        End Function

        '01-Oct-14 Aniket: Resolving Orders and Results Master Bugs (8030 Phase II)
        Public Function IsExistsType(ByVal stypeName As String, ByVal EnumType As Integer) As Boolean
            Dim _ID As String = ""
            Dim _Result As Boolean = False

            Dim _gloEMRDBID As New DataBaseLayer
            Try


                Dim _sqlquery = "SELECT labCSST_ID FROM Lab_CSST_MST WHERE labCSST_Type = " & EnumType & " and Upper(labCSST_Name) = '" & stypeName.ToUpper.Replace("'", "''") & "'"
                _ID = _gloEMRDBID.GetRecord_Query(_sqlquery)
                If Val(_ID) > 0 Then
                    _Result = True
                End If

            Catch ex As Exception
                Throw ex
            Finally

                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try


            Return _Result
        End Function

        Public Function IsExists(ByVal stypeName As String) As Boolean
            Dim _ID As String = ""
            Dim _Result As Boolean = False

            Dim _gloEMRDBID As New DataBaseLayer
            Try


                Dim _sqlquery = "SELECT labCSST_ID FROM Lab_CSST_MST WHERE Upper(labCSST_Name) = '" & stypeName.ToUpper.Replace("'", "''") & "'"
                _ID = _gloEMRDBID.GetRecord_Query(_sqlquery)
                If Val(_ID) > 0 Then
                    _Result = True
                End If

            Catch ex As Exception
                Throw ex
            Finally

                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try


            Return _Result
        End Function

        Public Function IsCodeExists(ByVal nlabCSST_Code As String) As Boolean

            Dim _ID As String = ""
            Dim _Result As Boolean = False

            Dim _gloEMRDBID As New DataBaseLayer

            Try

            
                Dim _sqlquery = "SELECT labCSST_ID FROM Lab_CSST_MST WHERE Upper(labCSST_Code) = '" & nlabCSST_Code.ToUpper.Replace("'", "''") & "'"
                _ID = _gloEMRDBID.GetRecord_Query(_sqlquery)

                If Val(_ID) > 0 Then
                    _Result = True
                End If
            Catch ex As Exception
                Throw ex
            Finally

                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try

            Return _Result

        End Function

        '01-Oct-14 Aniket: Resolving Orders and Results Master Bugs (8030 Phase II)
        Public Function IsCodeExistsType(ByVal nlabCSST_Code As String, ByVal EnumType As Integer) As Boolean

            Dim _ID As String = ""
            Dim _Result As Boolean = False

            Dim _gloEMRDBID As New DataBaseLayer

            Try


                Dim _sqlquery = "SELECT labCSST_ID FROM Lab_CSST_MST WHERE  labCSST_Type = " & EnumType & " AND Upper(labCSST_Code) = '" & nlabCSST_Code.ToUpper.Replace("'", "''") & "'"
                _ID = _gloEMRDBID.GetRecord_Query(_sqlquery)

                If Val(_ID) > 0 Then
                    _Result = True
                End If
            Catch ex As Exception
                Throw ex
            Finally

                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try

            Return _Result

        End Function

        Public Function IsDelete(ByVal sTypeName As String) As Boolean
            Dim _ID As String = ""
            Dim _Result As Boolean = True ' False

            'Dim _gloEMRDBID As New DataBaseLayer
            '_ID = _gloEMRDBID.GetRecord_Query("SELECT labtm_ID FROM Lab_Test_Mst WHERE Upper(labtm_Name) = '" & TestName.ToUpper & "'")

            'If Val(_ID) > 0 Then
            '    _Result = True
            'End If

            '_gloEMRDatabase.Dispose()
            Return _Result
        End Function


        Public Function GetLabCSSTTypeInfo(ByVal nCSST_ID As Int64, ByVal type As Int16) As gloEMRActors.LabActor.LabCSST
            _gloEMRDatabase = New DataBaseLayer

            Dim oLabCSST As gloEMRActors.LabActor.LabCSST
            Dim dt As DataTable = Nothing

            Try
                ' dt = _gloEMRDatabase.GetDataTable_Query("select * from Lab_CSST_MST where labCSST_Code is not null and labCSST_Name is not null and labCSST_ID is not null and labCSST_ID =" & nCSST_ID & " and labCSST_Type=" & type & "")
                dt = _gloEMRDatabase.GetDataTable_Query("select labCSST_ID,labCSST_Code,labCSST_Name,labCSST_Type,nClinicID  from Lab_CSST_MST where labCSST_Code is not null and labCSST_Name is not null and labCSST_ID is not null and labCSST_ID =" & nCSST_ID & " and labCSST_Type=" & type & "")


                oLabCSST = New gloEMRActors.LabActor.LabCSST
                If Not dt Is Nothing Then
                    If dt.Rows.Count > 0 Then
                        oLabCSST.LabCSST_ID = dt.Rows(0)("labCSST_ID")
                        oLabCSST.LabCSST_Code = dt.Rows(0)("labCSST_Code")
                        oLabCSST.LabCSST_Name = dt.Rows(0)("labCSST_Name")
                        oLabCSST.LabCSST_Type = dt.Rows(0)("labCSST_Type")
                        oLabCSST.nClinicID = dt.Rows(0)("nClinicID")
                    End If
                    dt.Dispose()
                    dt = Nothing
                End If
                Return oLabCSST
            Catch ex As Exception
                Throw ex
                Return oLabCSST
            Finally

                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try

        End Function

        Public Function GetLabCSSTs(ByVal type As Int16) As gloEMRActors.LabActor.LabCSSTs
            _gloEMRDatabase = New DataBaseLayer

            Dim olabCSSTs As New gloEMRActors.LabActor.LabCSSTs
            Dim olabCSST As gloEMRActors.LabActor.LabCSST
            Dim dt As DataTable = Nothing

            Try
                dt = _gloEMRDatabase.GetDataTable_Query("select labCSST_ID,isnull(labCSST_Code,'') as labCSST_Code, isnull(labCSST_Name,'') as labCSST_Name , labCSST_Type, nClinicID from Lab_CSST_MST where labCSST_Code is not null and labCSST_Name is not null and labCSST_ID is not null and labCSST_Type=" & type & "") ' type :1 is for specimen
                If Not dt Is Nothing Then
                    If dt.Rows.Count > 0 Then
                        For i As Int16 = 0 To dt.Rows.Count - 1
                            olabCSST = New gloEMRActors.LabActor.LabCSST
                            olabCSST.LabCSST_ID = dt.Rows(i)("labCSST_ID")
                            olabCSST.LabCSST_Code = dt.Rows(i)("labCSST_Code")
                            olabCSST.LabCSST_Name = dt.Rows(i)("labCSST_Name")
                            olabCSST.LabCSST_Type = dt.Rows(0)("labCSST_Type")
                            olabCSST.nClinicID = dt.Rows(0)("nClinicID")
                            If Not olabCSST Is Nothing Then
                                olabCSSTs.Add(olabCSST)
                            End If
                            olabCSST = Nothing
                        Next
                    End If
                    dt.Dispose()
                    dt = Nothing
                End If
                Return olabCSSTs
            Catch ex As Exception
                Throw ex
                Return olabCSSTs
            Finally

                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try


        End Function

        Public Function SeperateCodeAnddescription(ByVal LOINCOrderCode As String, ByVal CPT As String) As DataSet
            Dim oDB As New DataBaseLayer
            Dim oParamater As DBParameter
            Dim _ds As DataSet = Nothing
            Try


                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.VarChar
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@LOINCOrderCode"
                oParamater.Value = LOINCOrderCode
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing


                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.VarChar
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@CPT"
                oParamater.Value = CPT
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing


                _ds = oDB.GetDataSet("Labs_GetCodeAndDescription", True)




                If Not _ds Is Nothing Then
                    Return _ds
                Else
                    Return Nothing
                End If

            Catch ex As Exception
                Throw ex
                Return Nothing
            Finally

                'If Not IsNothing(_ds) Then
                '    _ds.Dispose()
                '    _ds = Nothing
                'End If
                If Not IsNothing(oDB) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If
            End Try
        End Function


        ''SELECT DISTINCT nCPTID,sCPTCode,sDescription FROM CPT_MST WHERE nClinicID = " + _ClinicID + "
        Public Function GetTemplateNames() As DataTable
            _gloEMRDatabase = New DataBaseLayer
            Dim dt As DataTable = Nothing

            Try
                dt = _gloEMRDatabase.GetDataTable_Query("SELECT nTemplateID,sTemplateName FROM TemplateGallery_MST WHERE sCategoryName='Orders' ORDER BY sTemplateName")

                Return dt
            Catch ex As Exception
                Throw ex
                Return Nothing
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try

        End Function
        Public Function GetTemplateName(ByVal TemplateID As String) As String
            Dim _TemplateName As String = ""
            Try


                _gloEMRDatabase = New DataBaseLayer


                'Dim dt As New DataTable
                Dim _ID As Long = 0
                Dim _Result As String = ""
                If TemplateID <> 0 Then
                    _TemplateName = _gloEMRDatabase.GetRecord_Query("SELECT sTemplateName FROM TemplateGallery_MST WHERE nTemplateID = '" & TemplateID & "'")
                End If

            Catch ex As Exception
                Throw ex
                Return Nothing
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
            Return _TemplateName
        End Function
        Private Function GetLabCSST_ID(ByVal sName As String, ByVal type As Int16) As Integer
            Dim _ID As String = ""
            Dim _Result As Integer = 0

            Dim _gloEMRDBID As New DataBaseLayer

            Try
                _ID = _gloEMRDBID.GetRecord_Query("SELECT labCSST_ID FROM Lab_CSST_MST WHERE Upper(labCSST_Name) = '" & sName.ToUpper & "' and labCSST_Type=" & type & "")

                If Val(_ID) > 0 Then
                    _Result = CInt(_ID)
                End If

            Catch ex As Exception
                Throw ex
            Finally

                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try


            '_gloEMRDatabase.Dispose()
            Return _Result
        End Function

        Private Function GetLabCSST_typeName(ByVal nlabCSST_ID As Integer, ByVal type As Int16) As String
            Dim _Result As String = ""

            Dim _gloEMRDBID As New DataBaseLayer

            Try


                _Result = _gloEMRDBID.GetRecord_Query("SELECT  labCSST_Name FROM Lab_CSST_MST WHERE labCSST_ID = " & nlabCSST_ID & " and labCSST_Type=" & type & "").Trim

                '_gloEMRDatabase.Dispose()
            Catch ex As Exception
                Throw ex
            Finally

                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try


            Return _Result

        End Function


        'its uniqe function...ref date means Patient Date Of Birth, Current Date, Order Date, etc
        Private Function GetPrefixTransactionID(ByVal ReferenceDate As DateTime) As Long
            Dim strID As String
            Dim dtDate As DateTime

            Try
                dtDate = System.DateTime.Now
                strID = DateDiff(DateInterval.Day, CDate("1/1/1900"), dtDate.Date) & DateDiff(DateInterval.Second, dtDate.Date, dtDate) & DateDiff(DateInterval.Day, CDate("1/1/1900"), ReferenceDate.Date)
            Catch ex As Exception
                Throw ex
            End Try

            Return CLng(strID)
        End Function


        Public Sub New()
            MyBase.New()
            mLabCSST = New gloEMRActors.LabActor.LabCSST
        End Sub


        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
                    If (IsNothing(mLabCSST) = False) Then
                        mLabCSST.Dispose()
                        mLabCSST = Nothing
                    End If
                End If
                mLabCSST = Nothing
                ' TODO: free shared unmanaged resources
            End If
            Me.disposedValue = True
        End Sub



#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
    '-------
#End Region
    ''Added by Mayuri:20130530-Order PRD changes done.
    Public Class gloEMRLabLoincMst
        Implements IDisposable

        ''Public Enum LabCCSTType
        ''    Specimen = 1
        ''    Collection = 2
        ''    StorageTemperature = 3
        ''End Enum

        Private mLabLoinc As gloEMRActors.LabActor.LabLoincMst
        Private bmLabLoinc As Boolean = True
        Private _Exception As gloEMRLabExceptions

        Dim _gloEMRDatabase As DataBaseLayer


        Public Property LabLoinc() As gloEMRActors.LabActor.LabLoincMst
            Get
                Return mLabLoinc
            End Get
            Set(ByVal value As gloEMRActors.LabActor.LabLoincMst)
                If (bmLabLoinc) Then
                    If (IsNothing(mLabLoinc) = False) Then
                        mLabLoinc.Dispose()
                        mLabLoinc = Nothing
                    End If
                    bmLabLoinc = False
                End If
                mLabLoinc = value
            End Set
        End Property
#Region "LOINC Order Code Entry"

        Public Function GetLabLoincMstInfo(ByVal nLoincMst_ID As Int64) As gloEMRActors.LabActor.LabLoincMst
            _gloEMRDatabase = New DataBaseLayer

            Dim oLabLoincMst As gloEMRActors.LabActor.LabLoincMst = Nothing
            Dim dt As DataTable = Nothing

            Try

                dt = _gloEMRDatabase.GetDataTable_Query("select LOINCID,LOINCCode,LOINCLongName  from LOINC_MST where LOINCCode is not null and LOINCLongName is not null and LOINCID is not null and LOINCID =" & nLoincMst_ID)


                oLabLoincMst = New gloEMRActors.LabActor.LabLoincMst
                If Not dt Is Nothing Then
                    If dt.Rows.Count > 0 Then
                        oLabLoincMst.LabLoinc_ID = dt.Rows(0)("LOINCID")
                        oLabLoincMst.LabLoinc_Code = dt.Rows(0)("LOINCCode")
                        oLabLoincMst.LabLoinc_Name = dt.Rows(0)("LOINCLongName")

                    End If
                    dt.Dispose()
                    dt = Nothing
                End If
                Return oLabLoincMst
            Catch ex As Exception
                Throw ex
                Return oLabLoincMst
            Finally
                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try

        End Function

        Public Function GetLabLoincMst() As gloEMRActors.LabActor.LabLoincMsts
            _gloEMRDatabase = New DataBaseLayer

            Dim olabLoincMsts As New gloEMRActors.LabActor.LabLoincMsts
            Dim olabLoincMst As gloEMRActors.LabActor.LabLoincMst
            Dim dt As DataTable = Nothing

            Try

                dt = _gloEMRDatabase.GetDataTable_Query("select LOINCID,LOINCCode,LOINCLongName  from LOINC_MST where LOINCCode is not null and LOINCLongName is not null and LOINCID is not null")


                'olabLoincMst = New gloEMRActors.LabActor.LabLoincMst
                If Not dt Is Nothing Then

                    If dt.Rows.Count > 0 Then
                        For i As Int16 = 0 To dt.Rows.Count - 1
                            olabLoincMst = New gloEMRActors.LabActor.LabLoincMst
                            olabLoincMst.LabLoinc_ID = dt.Rows(i)("LOINCID")
                            olabLoincMst.LabLoinc_Code = dt.Rows(i)("LOINCCode")
                            olabLoincMst.LabLoinc_Name = dt.Rows(i)("LOINCLongName")

                            If Not olabLoincMst Is Nothing Then
                                olabLoincMsts.Add(olabLoincMst)
                            End If
                            'olabLoincMst.Dispose()
                            olabLoincMst = Nothing
                        Next
                    End If
                    dt.Dispose()
                    dt = Nothing
                End If
                Return olabLoincMsts
            Catch ex As Exception
                Throw ex
                Return olabLoincMsts
            Finally
                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try

        End Function

        Public Function AddModifyLOINCCode(ByVal nlabLoinc_ID As Int64) As Boolean
            _gloEMRDatabase = New DataBaseLayer
            Dim objDBParameter As DBParameter
            Dim lab_LoincID As Int64

            Try



                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = LabLoinc.LabLoinc_Code
                objDBParameter.Name = "@labLOINC_Code"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = LabLoinc.LabLoinc_Name
                objDBParameter.Name = "@labLOINC_Name"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.InputOutput
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = nlabLoinc_ID
                objDBParameter.Name = "@labLOINC_ID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                lab_LoincID = _gloEMRDatabase.Add("Lab_InUpLOINCCodeMst")


            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
            Return Nothing
        End Function

        Public Function DeleteLOINCCode(ByVal nlabLoinc_ID As Int64) As Boolean
            Dim _gloEMRDBID As New DataBaseLayer

            'delete detail record 
            Try
                _gloEMRDBID.Delete_Query("delete  FROM LOINC_MST WHERE LOINCID = " & nlabLoinc_ID)

            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try

            '_gloEMRDatabase.Dispose()
            Return True
        End Function

        Public Function IsExistsLOINCName(ByVal LOINClongName As String) As Boolean
            Dim _ID As String = ""
            Dim _Result As Boolean = False

            Dim _gloEMRDBID As New DataBaseLayer
            Try

                Dim _sqlquery = "SELECT LOINCID FROM LOINC_MST WHERE Upper(LOINCLongName) = '" & LOINClongName.ToUpper.Replace("'", "''") & "'"
                _ID = _gloEMRDBID.GetRecord_Query(_sqlquery)
                If Val(_ID) > 0 Then
                    _Result = True
                End If

            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try

            '_gloEMRDatabase.Dispose()
            Return _Result
        End Function
        Public Function IsLOINCCodeExists(ByVal nlabLoinc_Code As String) As Boolean
            Dim _ID As String = ""
            Dim _Result As Boolean = False

            Dim _gloEMRDBID As New DataBaseLayer
            Try


                Dim _sqlquery = "SELECT LOINCID FROM LOINC_MST WHERE Upper(LOINCCode) = '" & nlabLoinc_Code.ToUpper.Replace("'", "''") & "'"
                _ID = _gloEMRDBID.GetRecord_Query(_sqlquery)
                If Val(_ID) > 0 Then
                    _Result = True
                End If
            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try


            '_gloEMRDatabase.Dispose()
            Return _Result
        End Function

#End Region
        Public Sub New()
            MyBase.New()
            mLabLoinc = New gloEMRActors.LabActor.LabLoincMst
        End Sub


        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
                    If (bmLabLoinc) Then
                        If (IsNothing(mLabLoinc) = False) Then
                            mLabLoinc.Dispose()
                            mLabLoinc = Nothing
                        End If
                        bmLabLoinc = False
                    End If
                End If
                'mLabLoinc = Nothing
                ' TODO: free shared unmanaged resources
            End If
            Me.disposedValue = True
        End Sub



#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region
    End Class

    Public Class gloEMRLabGroup
        Implements IDisposable
        Private _LabGroup As gloEMRActors.LabActor.LabGroup
        Private bLabGroup As Boolean = True
        Private _Exception As gloEMRLabExceptions

        Dim _gloEMRDatabase As DataBaseLayer


        Public Property LabGroup() As gloEMRActors.LabActor.LabGroup
            Get
                Return _LabGroup
            End Get
            Set(ByVal value As gloEMRActors.LabActor.LabGroup)
                If (bLabGroup) Then
                    If (IsNothing(_LabGroup) = False) Then
                        _LabGroup.Dispose()
                        _LabGroup = Nothing
                    End If
                    bLabGroup = False
                End If
                _LabGroup = value
            End Set
        End Property

        Public Function Add() As Int64
            _gloEMRDatabase = New DataBaseLayer
            Dim objDBParameter As DBParameter
            Dim _LabGroupID As Int64

            Try
                _gloEMRDatabase.DBParametersCol.Clear()
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = GetPrefixTransactionID(Date.Now)
                objDBParameter.Name = "@MachineID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabGroup.LabGroupCode
                objDBParameter.Name = "@labgm_GroupCode"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabGroup.LabGroupName
                objDBParameter.Name = "@labgm_GroupName"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)


                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.InputOutput
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = 0
                objDBParameter.Name = "@id"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                _LabGroupID = _gloEMRDatabase.Add("Lab_InsertGroup")

                If _LabGroupID > 0 Then
                    With _LabGroup.Tests
                        For i As Int16 = 0 To .Count - 1
                            _gloEMRDatabase.DBParametersCol.Clear()

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.BigInt
                            objDBParameter.Value = _LabGroupID
                            objDBParameter.Name = "@Labgd_GroupID"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.BigInt
                            objDBParameter.Value = .Item(i).TestID
                            objDBParameter.Name = "@Labgd_TestID"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            _gloEMRDatabase.Add("Lab_InsertGroupTest")
                        Next
                    End With
                End If

            Catch ex As Exception
                _Exception = New gloEMRLabExceptions
                _Exception.ErrorMessage = "Test with same name already exists, please enter another name"
                Throw _Exception
                _Exception.Dispose()
            Finally

                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
            Return Nothing
        End Function

        Public Function Modify(ByVal GroupID As Int64) As Boolean
            _gloEMRDatabase = New DataBaseLayer
            Dim objDBParameter As DBParameter
            Dim _LabGroupID As Int64

            Try
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = GroupID
                objDBParameter.Name = "@labgm_ID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)


                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabGroup.LabGroupCode
                objDBParameter.Name = "@labgm_GroupCode"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabGroup.LabGroupName
                objDBParameter.Name = "@labgm_GroupName"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                _LabGroupID = _gloEMRDatabase.Add("Lab_UpdateGroup")

                _gloEMRDatabase.Delete_Query(" delete  from Lab_Group_Dtl where Labgd_GroupID = " & GroupID & "")

                ' If _LabGroupID > 0 Then
                With _LabGroup.Tests
                    For i As Int16 = 0 To .Count - 1
                        _gloEMRDatabase.DBParametersCol.Clear()

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.BigInt
                        objDBParameter.Value = GroupID
                        objDBParameter.Name = "@Labgd_GroupID"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.BigInt
                        objDBParameter.Value = .Item(i).TestID
                        objDBParameter.Name = "@Labgd_TestID"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                        _gloEMRDatabase.Add("Lab_InsertGroupTest")
                    Next
                End With
                '   End If


            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
            Return Nothing
        End Function

        Public Function Delete(ByVal GroupID As Int64) As Boolean
            Dim _gloEMRDBID As New DataBaseLayer
            Try


                'delete detail record 
                _gloEMRDBID.Delete_Query("delete  FROM Lab_Group_Dtl WHERE Labgd_TestID = " & GroupID)

                'delete master record
                _gloEMRDBID.Delete_Query("delete  FROM Lab_Group_Mst WHERE labgm_ID = " & GroupID)
                '_gloEMRDatabase.Dispose()
            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try
            Return True
        End Function

        Public Function IsExists(ByVal GroupName As String) As Boolean
            Dim _ID As String = ""
            Dim _Result As Boolean = False

            Dim _gloEMRDBID As New DataBaseLayer
            Try


                '  _ID = _gloEMRDBID.GetRecord_Query("SELECT labgm_ID FROM Lab_Group_Mst WHERE Upper(labgm_GroupName) = '" & GroupName.ToUpper & "'")
                ''Sandip Darade 20090618
                Dim _sqlquery = "SELECT labgm_ID FROM Lab_Group_Mst WHERE Upper(labgm_GroupName) = '" & GroupName.ToUpper.Replace("'", "''") & "'"
                _ID = _gloEMRDBID.GetRecord_Query(_sqlquery)

                If Val(_ID) > 0 Then
                    _Result = True
                End If
            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try
            '_gloEMRDatabase.Dispose()
            Return _Result
        End Function

        Public Function IsCodeExists(ByVal GroupCode As String) As Boolean
            Dim _ID As String = ""
            Dim _Result As Boolean = False


            Dim _gloEMRDBID As New DataBaseLayer
            Try


                ' _ID = _gloEMRDBID.GetRecord_Query("SELECT labgm_ID FROM Lab_Group_Mst WHERE Upper(labgm_GroupCode) = '" & GroupCode.ToUpper & "'")
                ''Sandip Darade 20090618
                Dim _sqlquery = "SELECT labgm_ID FROM Lab_Group_Mst WHERE Upper(labgm_GroupCode) = '" & GroupCode.ToUpper.Replace("'", "''") & "'"
                _ID = _gloEMRDBID.GetRecord_Query(_sqlquery)

                If Val(_ID) > 0 Then
                    _Result = True
                End If
            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try
            '_gloEMRDatabase.Dispose()
            Return _Result
        End Function

        Public Function IsDelete(ByVal GroupName As String) As Boolean
            Dim _ID As String = ""
            Dim _Result As Boolean = True ' False

            'Dim _gloEMRDBID As New DataBaseLayer
            '_ID = _gloEMRDBID.GetRecord_Query("SELECT labgm_ID FROM Lab_Group_Mst WHERE Upper(labgm_GroupName) = '" & GroupName.ToUpper & "'")

            'If Val(_ID) > 0 Then
            '    _Result = True
            'End If

            '_gloEMRDatabase.Dispose()
            Return _Result
        End Function


        Public Function GetGroup(ByVal GroupID As Int64) As gloEMRActors.LabActor.LabGroup
            _gloEMRDatabase = New DataBaseLayer

            Dim oGroup As gloEMRActors.LabActor.LabGroup
            Dim dt As DataTable = Nothing

            Try
                'dt = New DataTable
                dt = _gloEMRDatabase.GetDataTable_Query("select * from Lab_Group_Mst where  labgm_ID =" & GroupID)
                oGroup = New gloEMRActors.LabActor.LabGroup
                If Not dt Is Nothing Then
                    oGroup.LabGroupID = dt.Rows(0)("labgm_ID")
                    oGroup.LabGroupCode = dt.Rows(0)("labgm_GroupCode")
                    oGroup.LabGroupName = dt.Rows(0)("labgm_GroupName")
                    dt.Dispose()
                    dt = Nothing
                End If
                dt = Nothing

                'dt = New DataTable
                Dim oTest As gloEMRActors.LabActor.Test
                Dim _strSQL As String = ""
                _strSQL = "SELECT Lab_Test_Mst.labtm_ID, Lab_Test_Mst.labtm_Code, Lab_Test_Mst.labtm_Name " _
                & " FROM Lab_Group_Dtl INNER JOIN Lab_Test_Mst ON Lab_Group_Dtl.Labgd_TestID = Lab_Test_Mst.labtm_ID " _
                & " WHERE (Lab_Group_Dtl.Labgd_GroupID = " & GroupID & ") AND (Lab_Test_Mst.labtm_ID IS NOT NULL) AND (Lab_Test_Mst.labtm_Code IS NOT NULL) AND (Lab_Test_Mst.labtm_Name IS NOT NULL)"

                dt = _gloEMRDatabase.GetDataTable_Query(_strSQL)
                If Not dt Is Nothing Then
                    If dt.Rows.Count > 0 Then
                        For i As Int16 = 0 To dt.Rows.Count - 1
                            oTest = New gloEMRActors.LabActor.Test
                            With oTest
                                .TestID = dt.Rows(i)("labtm_ID")
                                .Code = dt.Rows(i)("labtm_Code")
                                .Name = dt.Rows(i)("labtm_Name")
                            End With
                            If Not oTest Is Nothing Then
                                oGroup.Tests.Add(oTest)
                            End If
                            oTest = Nothing
                        Next
                    End If
                    dt.Dispose()
                    dt = Nothing
                End If
                dt = Nothing

                Return oGroup
            Catch ex As Exception
                Throw ex
                Return oGroup

            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try

        End Function

        Public Function GetGroups(Optional ByVal PreferredID As Int64 = 0) As gloEMRActors.LabActor.LabGroups
            _gloEMRDatabase = New DataBaseLayer
            Dim objDBParameter As DBParameter
            Dim oGroups As New gloEMRActors.LabActor.LabGroups
            Dim oGroup As gloEMRActors.LabActor.LabGroup
            Dim dt As DataTable = Nothing
            Dim _GroupID As Int64 = 0

            Try
                dt = _gloEMRDatabase.GetDataTable_Query("select * from Lab_Group_Mst")
                If Not dt Is Nothing Then
                    For i As Int16 = 0 To dt.Rows.Count - 1
                        oGroup = New gloEMRActors.LabActor.LabGroup
                        _GroupID = dt.Rows(i)("labgm_ID")
                        oGroup.LabGroupID = dt.Rows(i)("labgm_ID")
                        oGroup.LabGroupCode = dt.Rows(i)("labgm_GroupCode")
                        oGroup.LabGroupName = dt.Rows(i)("labgm_GroupName")
                        With oGroup
                            Dim dtTest As DataTable = Nothing

                            Try
                                _gloEMRDatabase.DBParametersCol.Clear()
                                objDBParameter = New DBParameter
                                objDBParameter.Direction = ParameterDirection.Input
                                objDBParameter.DataType = SqlDbType.BigInt
                                objDBParameter.Value = PreferredID
                                objDBParameter.Name = "@LabCI_ID"
                                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                                objDBParameter = Nothing

                                objDBParameter = New DBParameter
                                objDBParameter.Direction = ParameterDirection.Input
                                objDBParameter.DataType = SqlDbType.BigInt
                                objDBParameter.Value = _GroupID
                                objDBParameter.Name = "@GroupID"
                                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                                objDBParameter = Nothing

                                dtTest = _gloEMRDatabase.GetDataTable("gsp_GroupLabOrderTests")
                            Catch ex As Exception
                                Throw ex
                                Return Nothing

                            End Try


                            If Not dtTest Is Nothing Then
                                For j As Int16 = 0 To dtTest.Rows.Count - 1
                                    Dim oTest As New gloEMRActors.LabActor.Test
                                    With oTest
                                        .TestID = dtTest.Rows(j)("Labgd_TestID")
                                        .Code = dtTest.Rows(j)("labtm_Code")
                                        .Name = dtTest.Rows(j)("labtm_Name")
                                        .LOINCId = dtTest.Rows(j)("labtm_LOINCId")
                                    End With
                                    If Not oTest Is Nothing Then
                                        oGroup.Tests.Add(oTest)
                                    End If
                                    oTest = Nothing
                                Next
                                dtTest.Dispose()
                                dtTest = Nothing
                            End If
                            dtTest = Nothing
                        End With
                        If Not oGroup Is Nothing Then
                            oGroups.Add(oGroup)
                        End If
                        oGroup = Nothing
                    Next
                    dt.Dispose()
                    dt = Nothing
                End If
                dt = Nothing

                Return oGroups
            Catch ex As Exception
                Throw ex
                Return oGroups
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try


        End Function

        Private Function GetGroupID(ByVal LabGroup As String) As Integer
            Dim _ID As String = ""
            Dim _Result As Integer = 0

            Dim _gloEMRDBID As New DataBaseLayer

            Try


                _ID = _gloEMRDBID.GetRecord_Query("SELECT labgm_ID FROM Lab_Group_Mst WHERE Upper(labgm_GroupName) = '" & LabGroup.ToUpper & "'")

                If Val(_ID) > 0 Then
                    _Result = CInt(_ID)
                End If
            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try
            '_gloEMRDatabase.Dispose()
            Return _Result
        End Function

        Private Function GetGroupName(ByVal LabGroupID As Integer) As String
            Dim _Result As String = ""

            Dim _gloEMRDBID As New DataBaseLayer

            Try


                _Result = _gloEMRDBID.GetRecord_Query("SELECT  labgm_GroupName FROM Lab_Group_Mst WHERE labgm_ID = " & LabGroupID & " ").Trim

                '_gloEMRDatabase.Dispose()
            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try
            Return _Result

        End Function


        'its uniqe function...ref date means Patient Date Of Birth, Current Date, Order Date, etc
        Private Function GetPrefixTransactionID(ByVal ReferenceDate As DateTime) As Long
            Dim strID As String
            Dim dtDate As DateTime

            Try


                dtDate = System.DateTime.Now
                strID = DateDiff(DateInterval.Day, CDate("1/1/1900"), dtDate.Date) & DateDiff(DateInterval.Second, dtDate.Date, dtDate) & DateDiff(DateInterval.Day, CDate("1/1/1900"), ReferenceDate.Date)

            Catch ex As Exception
                Throw ex
            End Try

            Return CLng(strID)
        End Function


        Public Sub New()
            MyBase.New()
            _LabGroup = New gloEMRActors.LabActor.LabGroup
        End Sub


        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
                    If (bLabGroup) Then
                        If (IsNothing(_LabGroup) = False) Then
                            _LabGroup.Dispose()
                            _LabGroup = Nothing
                        End If
                        bLabGroup = False
                    End If
                End If
                '_LabGroup = Nothing
                ' TODO: free shared unmanaged resources
            End If
            Me.disposedValue = True
        End Sub




#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class

    Public Class gloEMRLabExceptions
        Inherits ApplicationException
        Implements IDisposable

        Private _ErrMessage As String

        Public Property ErrorMessage() As String
            Get
                Return _ErrMessage
            End Get
            Set(ByVal value As String)
                _ErrMessage = value
            End Set
        End Property

        Public Sub New()
            MyBase.New()
        End Sub

        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
                End If

                ' TODO: free shared unmanaged resources
            End If
            Me.disposedValue = True
        End Sub

#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class

    Public Class gloEMRLabOrder
        Implements IDisposable
        Private _LabOrder As gloEMRActors.LabActor.LabOrder
        Private _Exception As gloEMRLabExceptions

        Dim _gloEMRDatabase As DataBaseLayer

        Dim bLabOrderAssigned As Boolean = False
        Public Property LabOrder() As gloEMRActors.LabActor.LabOrder
            Get
                Return _LabOrder
            End Get
            Set(ByVal value As gloEMRActors.LabActor.LabOrder)
                If (bLabOrderAssigned) Then
                    _LabOrder = Nothing
                End If
                _LabOrder = value
                bLabOrderAssigned = False
            End Set
        End Property





        Public Function SplitOrder(ByVal _OrderID As Int64, ByVal strTestID As String) As Boolean

            _gloEMRDatabase = New DataBaseLayer
            Dim objDBParameter As DBParameter



            Try

                _gloEMRDatabase.DBParametersCol.Clear()


                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = _OrderID
                objDBParameter.Name = "@OrderID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = strTestID
                objDBParameter.Name = "@OrderTests"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                _gloEMRDatabase.Add("Labs_SplitOrder")

                Return True

            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If

            End Try
        End Function
        Public Function Add(ByVal IsFinished As Int16) As Int64
            _gloEMRDatabase = New DataBaseLayer
            Dim objDBParameter As DBParameter
            Dim _LabOrderID As Int64
            'Dim _LabOrderResultID As Int64
            'Dim _LabOrderUserID As Int64
            'Dim _LabOrderDiagID As Int64
            'Dim _LabOrderResDetID As Int64

            Try
                'insert into the main table Lab_Order_MST
                'labom_ID
                _gloEMRDatabase.DBParametersCol.Clear()
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = GetPrefixTransactionID(Date.Now)
                objDBParameter.Name = "@MachineID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabOrder.OrderNoPrefix
                objDBParameter.Name = "@labom_OrderNoPrefix"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.Int
                objDBParameter.Value = _LabOrder.OrderNoID
                objDBParameter.Name = "@labom_OrderNoID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.DateTime
                objDBParameter.Value = _LabOrder.TransactionDate
                objDBParameter.Name = "@labom_DateTime"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)


                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = _LabOrder.PatientID
                objDBParameter.Name = "@labom_PatientID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.Int
                objDBParameter.Value = _LabOrder.PatientAge.Years
                objDBParameter.Name = "@labom_PatientAgeYear"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.Int
                objDBParameter.Value = _LabOrder.PatientAge.Months
                objDBParameter.Name = "@labom_PatientAgeMonth"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.Int
                objDBParameter.Value = _LabOrder.PatientAge.Days
                objDBParameter.Name = "@labom_PatientAgeDay"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = _LabOrder.ProviderID '//Check for ID
                objDBParameter.Name = "@labom_ProviderID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = _LabOrder.PreferredLabID
                objDBParameter.Name = "@labom_PreferredLabID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)


                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = _LabOrder.SampledByID
                objDBParameter.Name = "@labom_SampledByID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = _LabOrder.ReferredByID
                objDBParameter.Name = "@labom_ReferredByID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = _LabOrder.ReferredToID
                objDBParameter.Name = "@labom_ReferredToID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)


                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.Int
                objDBParameter.Value = _LabOrder.SendTo
                objDBParameter.Name = "@SendTo"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = _LabOrder.VisitID
                objDBParameter.Name = "@labom_VisitID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabOrder.ExternalCode
                objDBParameter.Name = "@labom_ExternalCode"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = _LabOrder.DMSID
                objDBParameter.Name = "@labom_DMSID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                '\\ Lab Denormalization 20090318
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabOrder.PreferredLab
                objDBParameter.Name = "@labom_PreferredLabName"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabOrder.SampledBy
                objDBParameter.Name = "@labom_SampledByName"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabOrder.ReferredByFName
                objDBParameter.Name = "@labom_ReferredByFName"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabOrder.ReferredByMName
                objDBParameter.Name = "@labom_ReferredByMName"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabOrder.ReferredByLName
                objDBParameter.Name = "@labom_ReferredByLName"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = _LabOrder.ClinicID
                objDBParameter.Name = "@nClinicID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.Int
                objDBParameter.Value = _LabOrder.OrderStatusNumber
                objDBParameter.Name = "@OrderStatusNumber"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.Bit
                objDBParameter.Value = _LabOrder.blnOrderNotCPOE
                objDBParameter.Name = "@blnOrderNotCPOE"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.Bit
                objDBParameter.Value = _LabOrder.bOutboundTransistion
                objDBParameter.Name = "@bOutboundTransistion"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                '29-Sep-2014 Aniket: Insert the Order Creator User for MU Stage 2 Report
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabOrder.OrderCreatorUser
                objDBParameter.Name = "@sOrderCreaterUser"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabOrder.FastingStatus
                objDBParameter.Name = "@sFastingStatus"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.InputOutput
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = 0
                objDBParameter.Name = "@id"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

     

                _LabOrderID = _gloEMRDatabase.Add("Lab_InsertOrderMaster")

                ''insert the multiple users against the above order using the _LabOrderID
                If Not _LabOrder.Users Is Nothing Then
                    Call Save_LabOrderUsers(_LabOrderID)
                End If

                '' ''insert the multiple tests against the above order using the _LabOrderID
                Call Save_LabOrderDetails(_LabOrderID, IsFinished)

                Return _LabOrderID

            Catch ex As Exception
                Throw ex

            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If

            End Try
        End Function
        Public Function UpdateTaskData(ByVal Users As gloEMRGeneralLibrary.gloEMRActors.LabActor.ItemDetails, ByVal Subject As String, ByVal nFromID As Int64, ByVal TaskDate As Date, ByVal PatientID As Long, ByVal TaskType As Integer, ByVal TaskId As Long, ByVal Notes As String, ByVal TaskDueDate As Date)
            _gloEMRDatabase = New DataBaseLayer
            Dim objDBParameter As DBParameter
            Dim _TaskID As Int64 = 0

            Try

                '//Insert data into Task_Mst
                'parameter for MachineID

                _gloEMRDatabase.DBParametersCol.Clear()

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = nFromID '_LabOrder.ReferredByID
                objDBParameter.Name = "@nFromId"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.DateTime
                objDBParameter.Value = TaskDate
                objDBParameter.Name = "@dtTaskDate"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = Subject
                objDBParameter.Name = "@sSubject"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.DateTime
                objDBParameter.Value = Format(TaskDueDate, "MM/dd/yyyy hh:mm tt")
                objDBParameter.Name = "@dtDuedate"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = "High"
                objDBParameter.Name = "@sPriority"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = "Not Started"
                objDBParameter.Name = "@sStatus"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = Notes
                objDBParameter.Name = "@sNotes"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = PatientID
                objDBParameter.Name = "@nPatientId"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = ""
                objDBParameter.Name = "@FAXTIFFFileName"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.Int
                objDBParameter.Value = TaskType ' 0
                objDBParameter.Name = "@TaskType"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = GetPrefixTransactionID(Date.Now)
                objDBParameter.Name = "@MachineID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                '//output parameter
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.InputOutput
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = TaskId '_LabOrder.PreferredLabID
                objDBParameter.Name = "@nTaskId"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                _TaskID = _gloEMRDatabase.Add("gsp_InUpTasks_Mst")


                'delete from tasks_dtl
                ' '  gsp_DeleteTasks_DTL()

                Dim _gloEMRDBID As New DataBaseLayer


                If (IsNothing(_gloEMRDBID) = False) Then
                    _gloEMRDBID.Delete_Query("DELETE FROM Tasks_DTL WHERE nTaskId = " & _TaskID)

                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
                

                'insert into Tasks_dtl
                For i As Integer = 0 To Users.Count - 1
                    _gloEMRDatabase.DBParametersCol.Clear()

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.BigInt
                    objDBParameter.Value = _TaskID '_LabOrder.ReferredByID
                    objDBParameter.Name = "@nTaskID"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.BigInt
                    objDBParameter.Value = GetUserID(Users.Item(i).Description)  '_LabOrder.ReferredByID
                    objDBParameter.Name = "@nToId"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    _gloEMRDatabase.Add("gsp_InsertTasks_DTL")

                Next

                Return True

            Catch ex As Exception
                Throw ex
                Return Nothing
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If

            End Try
        End Function

        Public Function UpdateLabOrderUsers(ByVal ArrTo() As Long, ByVal _TaskDate As DateTime) As Boolean
            Dim _OrderID As Int64
            Dim _gloEMRDBID As New DataBaseLayer

            Try
                'get the OrderId from the Task date and get the taskid
                _OrderID = GetOrderID(_TaskDate)

                'delete the users against this OrderId
                _gloEMRDBID.Delete_Query("Delete from Lab_Order_UserDtl where laboud_OrderID =" & _OrderID)

                'insert the users from the arrto against this OrderID
                For i As Integer = 0 To ArrTo.Length - 1
                    InsertUser(_OrderID, ArrTo(i))
                Next

                Return True
            Catch ex As Exception
                Throw ex
                Return False
            Finally
                If (IsNothing(_gloEMRDBID) = False) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If

            End Try
        End Function
        ''Sanjog For C1Library on OrderTab
        Public Function GetOrder_New(ByVal PatientId As Int64, ByVal FromDate As String, ByVal TODate As String, ByVal OrderAknwlgstatus As String, ByVal RetunFormat As Integer) As DataTable

            _gloEMRDatabase = New DataBaseLayer
            Dim dt As DataTable = Nothing
            Dim objDBParameter As DBParameter


            Try

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = PatientId
                objDBParameter.Name = "@PatientID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing


                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = FromDate
                objDBParameter.Name = "@FromDate"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = TODate
                objDBParameter.Name = "@ToDate"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing




                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = OrderAknwlgstatus
                objDBParameter.Name = "@OrderAcknwStatus"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.Int
                objDBParameter.Value = RetunFormat
                objDBParameter.Name = "@ReturnFormat"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                '29-May-13 Aniket: Orders PRD: Updated the name of the Order Listing Stored Procedure
                dt = _gloEMRDatabase.GetDataTable("gsp_GetOrderListing")

                Return dt

            Catch ex As Exception
                Return Nothing
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If

            End Try

        End Function

        ''new function added for new orders tab added on Dashboard
        Public Function GetOrder_ByPatientID(ByVal PatientId As Int64, Optional ByVal FromDate As String = "", Optional ByVal TODate As String = "", Optional ByVal OrderAknwlgstatus As String = "") As DataTable

            _gloEMRDatabase = New DataBaseLayer
            Dim dt As DataTable = Nothing
            Dim objDBParameter As DBParameter


            Try

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = PatientId
                objDBParameter.Name = "@PatientID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing
                ''added for order PRD changes 8020
                If (FromDate.Trim() <> "") Then
                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.VarChar
                    objDBParameter.Value = FromDate
                    objDBParameter.Name = "@FromDate"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                    objDBParameter = Nothing

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.VarChar
                    objDBParameter.Value = TODate
                    objDBParameter.Name = "@ToDate"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                    objDBParameter = Nothing
                End If


                If (OrderAknwlgstatus.Trim() <> "") Then
                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.VarChar
                    objDBParameter.Value = OrderAknwlgstatus
                    objDBParameter.Name = "@OrderAcknwStatus"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                    objDBParameter = Nothing
                End If
                dt = _gloEMRDatabase.GetDataTable("gsp_GetOrderByPatientID")

                Return dt

            Catch ex As Exception
                Return Nothing
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If

            End Try

        End Function

        Public Function GetOrder_Merge(ByVal PatientId As Int64, ByVal OrderType As Integer) As DataTable

            _gloEMRDatabase = New DataBaseLayer
            Dim dt As DataTable = Nothing
            Dim objDBParameter As DBParameter


            Try

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = PatientId
                objDBParameter.Name = "@PatientID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.Int
                objDBParameter.Value = OrderType
                objDBParameter.Name = "@OrderType"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing


                dt = _gloEMRDatabase.GetDataTable("gsp_GetOrderListing_Merge")

                Return dt

            Catch ex As Exception
                Return Nothing
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If

            End Try

        End Function

        Public Function GetSelectedOrder(ByVal orderID As Int64) As DataTable

            _gloEMRDatabase = New DataBaseLayer
            Dim dt As DataTable = Nothing
            Dim objDBParameter As DBParameter


            Try

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = orderID
                objDBParameter.Name = "@OrderID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = 0
                objDBParameter.Name = "@PatientID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.Int
                objDBParameter.Value = 2
                objDBParameter.Name = "@OrderType"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing


                dt = _gloEMRDatabase.GetDataTable("gsp_GetOrderListing_Merge")

                Return dt

            Catch ex As Exception
                Return Nothing
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If

            End Try

        End Function


        Public Function GetOrderList(ByVal PatientId As Int64, ByVal FromDate As String, ByVal TODate As String, ByVal OrderAknwlgstatus As String, ByVal RetunFormat As Integer) As DataTable

            _gloEMRDatabase = New DataBaseLayer
            Dim dt As DataTable = Nothing
            Dim objDBParameter As DBParameter


            Try

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = PatientId
                objDBParameter.Name = "@PatientID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                If FromDate <> "" Then
                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.VarChar
                    objDBParameter.Value = FromDate
                    objDBParameter.Name = "@FromDate"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                    objDBParameter = Nothing

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.VarChar
                    objDBParameter.Value = TODate
                    objDBParameter.Name = "@ToDate"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                    objDBParameter = Nothing
                End If


                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = OrderAknwlgstatus
                objDBParameter.Name = "@OrderAcknwStatus"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.Int
                objDBParameter.Value = RetunFormat
                objDBParameter.Name = "@ReturnFormat"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                '29-May-13 Aniket: Orders PRD: Updated the name of the Order Listing Stored Procedure
                dt = _gloEMRDatabase.GetDataTable("gsp_GetOrderList")

                Return dt

            Catch ex As Exception
                Return Nothing
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If

            End Try

        End Function
        ''Sanjog For C1Library on OrderTab

        Public Function InsertUser(ByVal OrderID As Int64, ByVal UserID As Int64) As Boolean
            _gloEMRDatabase = New DataBaseLayer
            Dim objDBParameter As DBParameter

            Try

                _gloEMRDatabase.DBParametersCol.Clear()

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = OrderID
                objDBParameter.Name = "@laboud_OrderID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = UserID
                objDBParameter.Name = "@laboud_UserID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                _gloEMRDatabase.Add("Lab_InsertOrderUser")

                Return True
            Catch ex As Exception
                Throw ex
                Return False
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If

            End Try

        End Function

        Public Function GetOrderID(ByVal Tdate As DateTime) As Int64
            Dim _OrderID As Int64 = 0
            Dim _Result As Int64 = 0
            Dim _gloEMRDBID As New DataBaseLayer

            Try

                _OrderID = _gloEMRDBID.GetRecord_Query("SELECT labom_OrderID FROM Lab_Order_MST WHERE labom_TransactionDate = '" & Tdate & "'")

                If Val(_OrderID) > 0 Then
                    _Result = Convert.ToInt64(_OrderID)
                End If

                Return _OrderID
            Catch ex As Exception
                Throw ex
                Return 0
            Finally
                If (IsNothing(_gloEMRDBID) = False) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try
        End Function

        Public Function GetPreviousOrderIDs(ByVal Todate As String, ByVal PatientId As Long) As Collection
            Dim _OrderID As Int64 = 0
            Dim _Result As Int64 = 0
            'Dim _gloEMRDBID As New DataBaseLayer
            Dim ordCollection As New Collection

            _gloEMRDatabase = New DataBaseLayer
            Dim dt As DataTable = Nothing
            Try
                '    dt = _gloEMRDatabase.GetDataTable_Query("SELECT DISTINCT labom_OrderID, Lab_Order_MST.labom_OrderNoPrefix, Lab_Order_MST.labom_OrderNoID, Lab_Order_MST.labom_TransactionDate , ISNULL(Lab_Order_MST.labom_VisitID,0) AS labom_VisitID FROM Lab_Order_MST " _
                '& " LEFT OUTER JOIN Lab_Order_Test_Result ON Lab_Order_MST.labom_OrderID = Lab_Order_Test_Result.labotr_OrderID LEFT OUTER JOIN Lab_Order_Test_ResultDtl ON Lab_Order_Test_Result.labotr_OrderID = Lab_Order_Test_ResultDtl.labotrd_OrderID AND Lab_Order_Test_Result.labotr_TestID = Lab_Order_Test_ResultDtl.labotrd_TestID " _
                '& " WHERE(labom_PatientID = " & PatientId & " and labom_TransactionDate = " & Todate & ")ORDER By labom_TransactionDate, labom_OrderNoID")

                dt = _gloEMRDatabase.GetDataTable_Query("SELECT DISTINCT labom_OrderID, Lab_Order_MST.labom_OrderNoPrefix, Lab_Order_MST.labom_OrderNoID, Lab_Order_MST.labom_TransactionDate , ISNULL(Lab_Order_MST.labom_VisitID,0) AS labom_VisitID FROM Lab_Order_MST " _
            & " LEFT OUTER JOIN Lab_Order_Test_Result ON Lab_Order_MST.labom_OrderID = Lab_Order_Test_Result.labotr_OrderID LEFT OUTER JOIN Lab_Order_Test_ResultDtl ON Lab_Order_Test_Result.labotr_OrderID = Lab_Order_Test_ResultDtl.labotrd_OrderID AND Lab_Order_Test_Result.labotr_TestID = Lab_Order_Test_ResultDtl.labotrd_TestID " _
            & " WHERE(labom_PatientID = " & PatientId & ")ORDER By labom_TransactionDate, labom_OrderNoID")

                If Not dt Is Nothing Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        ordCollection.Add(dt.Rows(i)("labom_OrderID"))
                    Next
                    dt.Dispose()
                    dt = Nothing
                    Return ordCollection
                Else
                    Return ordCollection
                End If

                Return ordCollection

            Catch ex As Exception
                Throw ex
                Return ordCollection

            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
             
            End Try

        End Function
        Public Function GetSelectedICDDetails(ByVal OrderID As Long, ByVal TestID As Long, ByVal ICDCode As String) As DataTable

            _gloEMRDatabase = New DataBaseLayer
            Dim dt As DataTable = Nothing
            Try

                dt = _gloEMRDatabase.GetDataTable_Query("SELECT ISNULL(labodtl_Code,'') as labodtl_Code,ISNULL(labodtl_Description,'') AS labodtl_Description," _
         & " ISNULL(nICDRevision,9) as nICDRevision FROM Lab_Order_TestDtl_DiagCPT  WHERE labodtl_OrderID= " & OrderID & " and labodtl_Code='" & ICDCode & "' and labodtl_Type=1")
                '   If IsNothing(dt) Then
                If dt.Rows.Count <= 0 Then
                    dt = _gloEMRDatabase.GetDataTable_Query("SELECT ISNULL(sICD9Code,'') as labodtl_Code,space(1) + ISNULL(sDescription,'') AS labodtl_Description," _
  & " ISNULL(nICDRevision,9) as nICDRevision FROM ICD9  WHERE  sICD9Code='" & ICDCode & "'")

                    'End If
                End If
                Return dt

            Catch ex As Exception
                Throw ex
                Return dt

            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If

            End Try

        End Function
        Private Function GetSpecimanID(ByVal Specimen As String) As Int64
            Dim _ID As String = ""
            Dim _Result As Int64 = 0

            Dim _gloEMRDBID As New DataBaseLayer

            Try

                ''Added by madan to handle "'" on 20101030
                If Specimen.Contains("'") Then
                    Specimen = Specimen.Replace("'", "''")
                End If
                ''End madan.

                ' _ID = _gloEMRDBID.GetRecord_Query("SELECT labsm_ID FROM Lab_Specimen_Mst WHERE Upper(labsm_Name) = '" & Specimen.ToUpper & "'")
                _ID = _gloEMRDBID.GetRecord_Query("SELECT labCSST_ID FROM Lab_CSST_MST WHERE Upper(labCSST_Name) = '" & Specimen.ToUpper & "'")

                If Val(_ID) > 0 Then
                    _Result = Convert.ToInt64(_ID)
                End If
            Catch ex As Exception
                Throw ex
            Finally

                If (IsNothing(_gloEMRDBID) = False) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try


            '_gloEMRDatabase.Dispose()
            Return _Result
        End Function
        Public Function GetOrderStatus(ByVal OrderID As Int64) As Int16
            Dim _ID As String = ""
            Dim _Result As Int16 = 0

            Dim _gloEMRDBID As New DataBaseLayer

            Try
                _ID = _gloEMRDBID.GetRecord_Query("SELECT ISNULL(OrderStatusNumber,0) AS OrderStatusNumber  FROM Lab_Order_Mst WHERE labom_OrderID = '" & OrderID & "'")

                If Val(_ID) > 0 Then
                    _Result = Convert.ToInt16(_ID)
                End If
            Catch ex As Exception
                Throw ex
            Finally
                If (IsNothing(_gloEMRDBID) = False) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try
            '_gloEMRDatabase.Dispose()
            Return _Result
        End Function
        Private Function GetCollectionID(ByVal CollectionName As String) As Int64
            Dim _ID As String = ""
            Dim _Result As Int64 = 0

            Dim _gloEMRDBID As New DataBaseLayer

            Try


                _ID = _gloEMRDBID.GetRecord_Query("SELECT labcm_ID FROM Lab_Collection_Mst WHERE Upper(labcm_Name) = '" & CollectionName.ToUpper & "'")

                If Val(_ID) > 0 Then
                    _Result = Convert.ToInt64(_ID)
                End If
            Catch ex As Exception
                Throw ex
            Finally
                If (IsNothing(_gloEMRDBID) = False) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try
            '_gloEMRDatabase.Dispose()
            Return _Result
        End Function

        Private Function GetStorageTempID(ByVal StorageTemperature As String) As Int64
            Dim _ID As String = ""
            Dim _Result As Int64 = 0

            Dim _gloEMRDBID As New DataBaseLayer

            Try


                _ID = _gloEMRDBID.GetRecord_Query("SELECT labstm_ID FROM Lab_StorageTemp_Mst WHERE Upper(labstm_Name) = '" & StorageTemperature.ToUpper & "'")

                If Val(_ID) > 0 Then
                    _Result = Convert.ToInt64(_ID)
                End If
            Catch ex As Exception
                Throw ex
            Finally
                If (IsNothing(_gloEMRDBID) = False) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try
            '_gloEMRDatabase.Dispose()
            Return _Result
        End Function

        'sarika 29th may 07
        '----------------------------------
        Public Function GetOrderFromVisitID(ByVal VisitID As Int64, ByVal ProviderID As Int64) As Int64
            Dim _OrderID As Int64 = 0

            Dim _Result As Int64 = 0
            Dim _strSQL As String = ""
            Dim _gloEMRDBID As New DataBaseLayer
            Dim objDBParameter As DBParameter

            Try
                ' _strSQL = ""

                _gloEMRDBID.DBParametersCol.Clear()
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = VisitID
                objDBParameter.Name = "@nVisitID"
                _gloEMRDBID.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = ProviderID
                objDBParameter.Name = "@nProvider"
                _gloEMRDBID.DBParametersCol.Add(objDBParameter)

                _OrderID = _gloEMRDBID.GetDataValue("Lab_GetOrderID", True)

                If Val(_OrderID) > 0 Then
                    _Result = Convert.ToInt64(_OrderID)
                End If

                Return _OrderID
            Catch ex As Exception

                Throw ex
                Return 0
            Finally
                If (IsNothing(_gloEMRDBID) = False) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try
        End Function
        '----------------------------------

        Public Function Modify(ByVal LabOrderID As Int64, ByVal IsFinished As Int16) As Boolean
            _gloEMRDatabase = New DataBaseLayer

            Dim objDBParameter As DBParameter
            Dim _LabOrderID As Int64 = LabOrderID
            'Dim _LabOrderUserID As Int64
            'Dim _LabOrderDiagID As Int64
            'Dim _LabOrderResDetID As Int64

            'Dim _LabOrderResultID As Int64
            Dim _gloEMRDBID As New DataBaseLayer

            Try
                'delete data from all the child tables
                If _gloEMRDBID.Delete_Query("DELETE FROM Lab_Order_UserDtl WHERE laboud_OrderID = " & LabOrderID) = True Then
                    If _gloEMRDBID.Delete_Query("DELETE FROM Lab_Order_TestDtl WHERE labotd_OrderID = " & LabOrderID) = True Then
                        If _gloEMRDBID.Delete_Query("DELETE FROM Lab_Order_TestDtl_DiagCPT WHERE labodtl_OrderID = " & LabOrderID) Then
                            If _gloEMRDBID.Delete_Query("DELETE FROM Lab_Order_Test_ResultDtl where labotrd_OrderID = " & LabOrderID) Then
                                If _gloEMRDBID.Delete_Query("DELETE FROM Lab_Order_Test_Result where labotr_OrderID = " & LabOrderID) Then

                                    'update into the main table Lab_Order_MST




                                    _gloEMRDatabase.DBParametersCol.Clear()
                                    objDBParameter = New DBParameter
                                    objDBParameter.Direction = ParameterDirection.Input
                                    objDBParameter.DataType = SqlDbType.BigInt
                                    objDBParameter.Value = LabOrderID 'GetPrefixTransactionID(Date.Now)
                                    objDBParameter.Name = "@labom_OrderID"
                                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                                    objDBParameter = New DBParameter
                                    objDBParameter.Direction = ParameterDirection.Input
                                    objDBParameter.DataType = SqlDbType.VarChar
                                    objDBParameter.Value = _LabOrder.OrderNoPrefix
                                    objDBParameter.Name = "@labom_OrderNoPrefix"
                                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                                    objDBParameter = New DBParameter
                                    objDBParameter.Direction = ParameterDirection.Input
                                    objDBParameter.DataType = SqlDbType.BigInt
                                    objDBParameter.Value = _LabOrder.OrderNoID
                                    objDBParameter.Name = "@labom_OrderNoID"
                                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                                    objDBParameter = New DBParameter
                                    objDBParameter.Direction = ParameterDirection.Input
                                    objDBParameter.DataType = SqlDbType.DateTime
                                    objDBParameter.Value = _LabOrder.TransactionDate
                                    objDBParameter.Name = "@labom_DateTime"
                                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)


                                    objDBParameter = New DBParameter
                                    objDBParameter.Direction = ParameterDirection.Input
                                    objDBParameter.DataType = SqlDbType.BigInt
                                    objDBParameter.Value = _LabOrder.PatientID
                                    objDBParameter.Name = "@labom_PatientID"
                                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                                    objDBParameter = New DBParameter
                                    objDBParameter.Direction = ParameterDirection.Input
                                    objDBParameter.DataType = SqlDbType.Int
                                    objDBParameter.Value = _LabOrder.PatientAge.Years
                                    objDBParameter.Name = "@labom_PatientAgeYear"
                                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                                    objDBParameter = New DBParameter
                                    objDBParameter.Direction = ParameterDirection.Input
                                    objDBParameter.DataType = SqlDbType.Int
                                    objDBParameter.Value = _LabOrder.PatientAge.Months
                                    objDBParameter.Name = "@labom_PatientAgeMonth"
                                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)


                                    objDBParameter = New DBParameter
                                    objDBParameter.Direction = ParameterDirection.Input
                                    objDBParameter.DataType = SqlDbType.Int
                                    objDBParameter.Value = _LabOrder.PatientAge.Days
                                    objDBParameter.Name = "@labom_PatientAgeDay"
                                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                                    objDBParameter = New DBParameter
                                    objDBParameter.Direction = ParameterDirection.Input
                                    objDBParameter.DataType = SqlDbType.BigInt
                                    objDBParameter.Value = _LabOrder.ProviderID ' Check For ID
                                    objDBParameter.Name = "@labom_ProviderID"
                                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                                    objDBParameter = New DBParameter
                                    objDBParameter.Direction = ParameterDirection.Input
                                    objDBParameter.DataType = SqlDbType.BigInt
                                    objDBParameter.Value = _LabOrder.PreferredLabID
                                    objDBParameter.Name = "@labom_PreferredLabID"
                                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)


                                    objDBParameter = New DBParameter
                                    objDBParameter.Direction = ParameterDirection.Input
                                    objDBParameter.DataType = SqlDbType.BigInt
                                    objDBParameter.Value = _LabOrder.SampledByID
                                    objDBParameter.Name = "@labom_SampledByID"
                                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)



                                    objDBParameter = New DBParameter
                                    objDBParameter.Direction = ParameterDirection.Input
                                    objDBParameter.DataType = SqlDbType.BigInt
                                    objDBParameter.Value = _LabOrder.ReferredByID
                                    objDBParameter.Name = "@labom_ReferredByID"
                                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                                    'sarika 29th may 07
                                    '--------------
                                    objDBParameter = New DBParameter
                                    objDBParameter.Direction = ParameterDirection.Input
                                    objDBParameter.DataType = SqlDbType.BigInt
                                    objDBParameter.Value = _LabOrder.VisitID
                                    objDBParameter.Name = "@labom_VisitID"
                                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                                    '--------------

                                    objDBParameter = New DBParameter
                                    objDBParameter.Direction = ParameterDirection.Input
                                    objDBParameter.DataType = SqlDbType.BigInt
                                    objDBParameter.Value = _LabOrder.DMSID
                                    objDBParameter.Name = "@labom_DMSID"
                                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)


                                    '\\ Lab Denormalization 20090318
                                    objDBParameter = New DBParameter
                                    objDBParameter.Direction = ParameterDirection.Input
                                    objDBParameter.DataType = SqlDbType.VarChar
                                    objDBParameter.Value = _LabOrder.PreferredLab
                                    objDBParameter.Name = "@labom_PreferredLabName"
                                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                                    objDBParameter = New DBParameter
                                    objDBParameter.Direction = ParameterDirection.Input
                                    objDBParameter.DataType = SqlDbType.VarChar
                                    objDBParameter.Value = _LabOrder.SampledBy
                                    objDBParameter.Name = "@labom_SampledByName"
                                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                                    objDBParameter = New DBParameter
                                    objDBParameter.Direction = ParameterDirection.Input
                                    objDBParameter.DataType = SqlDbType.VarChar
                                    objDBParameter.Value = _LabOrder.ReferredByFName
                                    objDBParameter.Name = "@labom_ReferredByFName"
                                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                                    objDBParameter = New DBParameter
                                    objDBParameter.Direction = ParameterDirection.Input
                                    objDBParameter.DataType = SqlDbType.VarChar
                                    objDBParameter.Value = _LabOrder.ReferredByMName
                                    objDBParameter.Name = "@labom_ReferredByMName"
                                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                                    objDBParameter = New DBParameter
                                    objDBParameter.Direction = ParameterDirection.Input
                                    objDBParameter.DataType = SqlDbType.VarChar
                                    objDBParameter.Value = _LabOrder.ReferredByLName
                                    objDBParameter.Name = "@labom_ReferredByLName"
                                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                                    objDBParameter = New DBParameter
                                    objDBParameter.Direction = ParameterDirection.Input
                                    objDBParameter.DataType = SqlDbType.BigInt
                                    objDBParameter.Value = _LabOrder.ClinicID
                                    objDBParameter.Name = "@nClinicID"
                                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                                    If _LabOrder.OrderStatusNumber <> 0 Then
                                        objDBParameter = New DBParameter
                                        objDBParameter.Direction = ParameterDirection.Input
                                        objDBParameter.DataType = SqlDbType.Int
                                        objDBParameter.Value = _LabOrder.OrderStatusNumber
                                        objDBParameter.Name = "@OrderStatusNumber"
                                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                                    End If

                                    objDBParameter = New DBParameter
                                    objDBParameter.Direction = ParameterDirection.Input
                                    objDBParameter.DataType = SqlDbType.Bit
                                    objDBParameter.Value = _LabOrder.blnOrderNotCPOE
                                    objDBParameter.Name = "@blnOrderNotCPOE"
                                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)


                                    objDBParameter = New DBParameter
                                    objDBParameter.Direction = ParameterDirection.Input
                                    objDBParameter.DataType = SqlDbType.Bit
                                    objDBParameter.Value = _LabOrder.bOutboundTransistion
                                    objDBParameter.Name = "@bOutboundTransistion"
                                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)


                                    objDBParameter = New DBParameter
                                    objDBParameter.Direction = ParameterDirection.Input
                                    objDBParameter.DataType = SqlDbType.BigInt
                                    objDBParameter.Value = _LabOrder.ReferredToID
                                    objDBParameter.Name = "@labom_ReferredToID"
                                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)


                                    objDBParameter = New DBParameter
                                    objDBParameter.Direction = ParameterDirection.Input
                                    objDBParameter.DataType = SqlDbType.Int
                                    objDBParameter.Value = _LabOrder.SendTo
                                    objDBParameter.Name = "@SendTo"
                                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                                    objDBParameter = New DBParameter
                                    objDBParameter.Direction = ParameterDirection.Input
                                    objDBParameter.DataType = SqlDbType.VarChar
                                    objDBParameter.Value = _LabOrder.FastingStatus
                                    objDBParameter.Name = "@sFastingStatus"
                                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                                    Dim _TempID As Int64 = 0
                                    _TempID = _gloEMRDatabase.Add("Lab_UpdateOrderMaster")

                                    ' ''insert into detail tables
                                    ' ''insert the multiple users against the above order using the _LabOrderID

                                    Call Save_LabOrderUsers(_LabOrderID)

                                    ' ''insert the multiple tests against the above order using the _LabOrderID
                                    Call Save_LabOrderDetails(_LabOrderID, IsFinished)

                                    'Deleted Test Attachments From Lab_Order_Test_ResultDtl_Attachments Table
                                    '_gloEMRDatabase.DBParametersCol.Clear()

                                    '16-Jul-14 Aniket: Resolving issue occured due to object disposal
                                    _gloEMRDatabase = New DataBaseLayer

                                    objDBParameter = New DBParameter
                                    objDBParameter.Direction = ParameterDirection.Input
                                    objDBParameter.DataType = SqlDbType.BigInt
                                    objDBParameter.Value = LabOrderID
                                    objDBParameter.Name = "@OrderID"
                                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)


                                    _gloEMRDatabase.Add("Lab_UPDATE_Test_AttachmentsForRemovedTESTS")


                                End If
                            End If
                        End If
                    End If
                End If

            Catch ex As Exception
                Throw ex
            Finally
                If (IsNothing(_gloEMRDBID) = False) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
            Return Nothing
        End Function

        Public Function Delete(ByVal LabOrderID As Int64) As Boolean
            Dim _gloEMRDBID As New DataBaseLayer

            Try
                '-----------------
                'delete the child tables

                If _gloEMRDBID.Delete_Query("DELETE FROM Lab_Order_UserDtl WHERE laboud_OrderID = " & LabOrderID) = True Then
                    If _gloEMRDBID.Delete_Query("DELETE FROM Lab_Order_TestDtl WHERE labotd_OrderID = " & LabOrderID) = True Then
                        If _gloEMRDBID.Delete_Query("DELETE FROM Lab_Order_TestDtl_DiagCPT WHERE labodtl_OrderID = " & LabOrderID) = True Then
                            If _gloEMRDBID.Delete_Query("DELETE FROM Lab_Order_Test_ResultDtl where labotrd_OrderID = " & LabOrderID) = True Then
                                If _gloEMRDBID.Delete_Query("DELETE FROM Lab_Order_Test_Result where labotr_OrderID = " & LabOrderID) = True Then
                                    '-----------------
                                    'delete the master table Lab_Order_MST
                                    If _gloEMRDBID.Delete_Query("DELETE FROM Lab_Order_MST WHERE labom_OrderID = " & LabOrderID) = True Then

                                        Return True
                                    End If

                                End If
                            End If
                        End If
                    End If
                End If

            Catch ex As Exception
                Throw ex
            Finally
                If (IsNothing(_gloEMRDBID) = False) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try
            Return Nothing
        End Function

        Private Function GetPrefixTransactionID(ByVal ReferenceDate As DateTime) As Long
            Dim strID As String
            Dim dtDate As DateTime

            Try
                dtDate = System.DateTime.Now
                strID = DateDiff(DateInterval.Day, CDate("1/1/1900"), dtDate.Date) & DateDiff(DateInterval.Second, dtDate.Date, dtDate) & DateDiff(DateInterval.Day, CDate("1/1/1900"), ReferenceDate.Date)

            Catch ex As Exception
                Throw ex
            End Try
            Return CLng(strID)
        End Function

        Private Function GetUserID(ByVal strLoginName As String) As Int64
            Dim _ID As String = ""
            Dim _Result As Int64 = 0

            Dim _gloEMRDBID As New DataBaseLayer

            Try

                _ID = _gloEMRDBID.GetRecord_Query("SELECT nUserID FROM User_MST WHERE Upper(sLoginName) = '" & strLoginName.ToUpper & "'")

                If Val(_ID) > 0 Then
                    _Result = Convert.ToInt64(_ID)
                End If

                '_gloEMRDatabase.Dispose()

            Catch ex As Exception
                Throw ex
            Finally
                If (IsNothing(_gloEMRDBID) = False) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try
            Return _Result
        End Function

        'Private Function GetUsers() As DataTable
        '    Dim _ID As String = ""
        '    Dim _Result As Int64 = 0

        '    Dim _gloEMRDBID As New DataBaseLayer
        '    _ID = _gloEMRDBID.GetRecord_Query("SELECT nUserID FROM User_MST ")

        '    If Val(_ID) > 0 Then
        '        _Result = Convert.ToInt64(_ID)
        '    End If

        '    '_gloEMRDatabase.Dispose()
        '    Return _Result
        'End Function

        Private Function GetPreferredLabRefSampleByID(ByVal strContactName As String, ByVal strRefSampledByName As String, ByVal ContactType As gloEMRActors.LabActor.enumContactType) As Int64
            Dim _ID As String = ""
            Dim _Result As Int64 = 0

            Dim _gloEMRDBID As New DataBaseLayer

            Try


                If ContactType = gloEMRActors.LabActor.enumContactType.PreferredLab Then
                    _ID = _gloEMRDBID.GetRecord_Query("SELECT labci_Id FROM Lab_ContactInfo WHERE Upper(labci_ContactName) = '" & strContactName.ToUpper & "' and labci_Type = " & ContactType & "")
                Else
                    _ID = _gloEMRDBID.GetRecord_Query("SELECT labci_Id FROM Lab_ContactInfo WHERE rtrim(ltrim((isnull(Upper(labci_FirstName) + ' ' ,'') + isnull(Upper(labci_MiddleName)+' ','') + isnull(Upper(labci_LastName) + ' ','')))) = '" & strRefSampledByName.ToUpper & "' and labci_Type = " & ContactType & "")
                End If


                If Val(_ID) > 0 Then
                    _Result = Convert.ToInt64(_ID)
                End If
            Catch ex As Exception
                Throw ex
            Finally
                If (IsNothing(_gloEMRDBID) = False) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try
            '_gloEMRDatabase.Dispose()
            Return _Result
        End Function

        ' '' <summary>
        ' ''  by Mahesh ''20070518
        ' '' <param name="OrderID"></param>
        ' '' <returns> gloEMRActors.LabActor.LabOrder </returns>
        ' '' <remarks>
        ' ''    To Get Order Detials From DB Agianst The Provided OrderID
        ' ''</remarks>
        ' '' </summary>
        Public Function GetOrders(ByVal PatientID As Long, ByVal SelectionCriteria As gloEMRActors.enmHistoryCriteria, Optional ByVal IncludingDetail As Boolean = False) As gloEMRActors.LabActor.LabOrders
            Dim _Orders As New gloEMRActors.LabActor.LabOrders
            Dim _Order As gloEMRActors.LabActor.LabOrder
            Dim dt As DataTable = Nothing

            Try


                dt = GetLabOrders(PatientID, Now.Date, "O", SelectionCriteria)
                If IsNothing(dt) = False Then
                    With _Orders
                        For i As Integer = 0 To dt.Rows.Count - 1
                            _Order = New gloEMRActors.LabActor.LabOrder
                            ''labom_OrderID, labom_OrderNoPrefix, labom_OrderNoID, labom_TransactionDate,labom_VisitID
                            _Order.OrderID = dt.Rows(i)("labom_OrderID")
                            _Order.OrderNoPrefix = dt.Rows(i)("labom_OrderNoPrefix")
                            _Order.OrderNoID = dt.Rows(i)("labom_OrderNoID")
                            _Order.TransactionDate = dt.Rows(i)("labom_TransactionDate")
                            _Order.VisitID = dt.Rows(i)("labom_VisitID")

                            .Add(_Order)
                            _Order = Nothing
                        Next

                    End With
                    dt.Dispose()
                    dt = Nothing
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return _Orders
        End Function

        Private Function GetLabOrders(ByVal PatientID As Int64, ByVal dtDate As DateTime, ByVal strFormStatus As String, ByVal SelectionCriteria As gloEMRActors.enmHistoryCriteria, Optional ByVal IncludingDetail As Boolean = False) As DataTable
            Dim dt As DataTable = Nothing
            Dim objDBParameter As DBParameter

            Try
                ' dt = New DataTable


                _gloEMRDatabase = New DataBaseLayer
                _gloEMRDatabase.DBParametersCol.Clear()


                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = SelectionCriteria
                objDBParameter.Name = "@Interval"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = PatientID
                objDBParameter.Name = "@PatientID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.DateTime
                objDBParameter.Value = dtDate
                objDBParameter.Name = "@dtSysdate"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)


                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.Char
                objDBParameter.Value = strFormStatus
                objDBParameter.Name = "@formstatus"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                dt = _gloEMRDatabase.GetDataTable("Lab_CheckRecordcount")

                Return dt


            Catch ex As Exception
                Throw ex
                Return Nothing
            Finally

                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function

        ''Public Function GetOrder(ByVal OrderID As Int64) As gloEMRActors.LabActor.LabOrder
        ''    _gloEMRDatabase = New DataBaseLayer

        ''    Dim oLabOrder As New gloEMRActors.LabActor.LabOrder
        ''    Dim dt As DataTable
        ''    Dim i, j, k As Integer

        ''    Try

        ''        '//// <> Order Master 
        ''        dt = New DataTable
        ''        With _gloEMRDatabase
        ''            Dim oPara As New gloEMRDatabase.DBParameter
        ''            .DBParametersCol.Clear()
        ''            oPara.DataType = SqlDbType.BigInt
        ''            oPara.Direction = ParameterDirection.Input
        ''            oPara.Value = OrderID
        ''            oPara.Name = "@OrderID"
        ''            .DBParametersCol.Add(oPara)
        ''            ' ''Fill Order Master 
        ''            dt = .GetDataTable("Lab_GetOrderMaster")
        ''            ' ''labom_OrderNoPrefix, labom_OrderNoID, labom_TransactionDate, labom_PatientID, labom_PatientAgeYear, labom_PatientAgeMonth, labom_PatientAgeDay,
        ''            ' ''labom_ProviderID, labom_PreferredLabID, labom_SampledByID, labom_ReferredByID, ProviderName, ContactName, SampledBy, RefferedBy
        ''            oPara = Nothing
        ''        End With

        ''        If Not dt Is Nothing Then
        ''            ' '' Fill Order Master Object  
        ''            With oLabOrder
        ''                .OrderNoPrefix = dt.Rows(0)("labom_OrderNoPrefix")
        ''                .OrderNoID = dt.Rows(0)("labom_OrderNoID")
        ''                .TransactionDate = dt.Rows(0)("labom_TransactionDate")
        ''                .PatientID = dt.Rows(0)("labom_PatientID")
        ''                .PatientAge.Years = dt.Rows(0)("labom_PatientAgeYear")
        ''                .PatientAge.Months = dt.Rows(0)("labom_PatientAgeMonth")
        ''                .PatientAge.Days = dt.Rows(0)("labom_PatientAgeDay")
        ''                .ProviderID = dt.Rows(0)("labom_ProviderID")
        ''                .Provider = dt.Rows(0)("ProviderName")
        ''                .PreferredLab = dt.Rows(0)("ContactName")
        ''                .PreferredLabID = dt.Rows(0)("labom_PreferredLabID")
        ''                .SampledBy = dt.Rows(0)("SampledBy")
        ''                .SampledByID = dt.Rows(0)("labom_SampledByID")
        ''                .ReferredBy = dt.Rows(0)("RefferedBy")
        ''                .ReferredByID = dt.Rows(0)("labom_ReferredByID")
        ''                .ExternalCode = dt.Rows(0)("labom_ExternalCode")
        ''                .VisitID = dt.Rows(0)("labom_VisitID")
        ''                .DMSID = dt.Rows(0)("labom_DMSID")

        ''                '' 20071121 Mahesh
        ''                Dim strQuery As String = ""
        ''                Dim dtTaskDtl As DataTable
        ''                ''strQuery = "SELECT dtDueDate , sNotes From  Tasks_Mst Where nPatientID=" & .PatientID & " AND dtTaskDate='" & .TransactionDate & "' AND nTaskType = 4"
        ''                strQuery = "SELECT nDueDate, sNoteExt From TM_TaskMST Where nPatientID =" & .PatientID & " AND nStartDate ='" & gloDateMaster.gloDate.DateTimeAsNumber(.TransactionDate) & "' AND nTaskType = 4"
        ''                dtTaskDtl = _gloEMRDatabase.GetDataTable_Query(strQuery)
        ''                If IsNothing(dtTaskDtl) = False Then
        ''                    If dtTaskDtl.Rows.Count > 0 Then
        ''                        If IsDBNull(dtTaskDtl.Rows(0)(0)) = False Then
        ''                            .TaskDueDate = gloDateMaster.gloDate.DateAsDateTime(dtTaskDtl.Rows(0)(0))
        ''                        End If
        ''                        If IsDBNull(dtTaskDtl.Rows(0)(1)) = False Then
        ''                            .TaskDescription = dtTaskDtl.Rows(0)(1)
        ''                        End If
        ''                    End If
        ''                    dtTaskDtl = Nothing
        ''                End If
        ''                ''
        ''            End With

        ''        End If
        ''        dt = Nothing

        ''        '//// <> Fill User Details Object 
        ''        dt = New DataTable
        ''        With _gloEMRDatabase
        ''            Dim oPara As New gloEMRDatabase.DBParameter
        ''            .DBParametersCol.Clear()

        ''            oPara.DataType = SqlDbType.BigInt
        ''            oPara.Direction = ParameterDirection.Input
        ''            oPara.Value = OrderID
        ''            oPara.Name = "@OrderID"
        ''            .DBParametersCol.Add(oPara)
        ''            dt = .GetDataTable("Lab_GetOrderUser")
        ''            ' '' nUserID, sLoginName, UserName
        ''            oPara = Nothing
        ''        End With

        ''        If Not dt Is Nothing Then
        ''            '' Order Users Object 
        ''            '' Fill UserID & LoginName
        ''            With oLabOrder.Users
        ''                For i = 0 To dt.Rows.Count - 1
        ''                    .Add(dt.Rows(i)("nUserID"), "", dt.Rows(i)("sLoginName") & "")
        ''                Next
        ''            End With
        ''        End If
        ''        dt = Nothing

        ''        '//// <> Fill Order Test Details Object 
        ''        dt = New DataTable
        ''        With _gloEMRDatabase
        ''            Dim oPara As New gloEMRDatabase.DBParameter
        ''            .DBParametersCol.Clear()
        ''            oPara.DataType = SqlDbType.BigInt
        ''            oPara.Direction = ParameterDirection.Input
        ''            oPara.Value = OrderID
        ''            oPara.Name = "@OrderID"
        ''            .DBParametersCol.Add(oPara)
        ''            dt = .GetDataTable("Lab_GetOrderTestDtl")
        ''            ''labotd_TestID, labotd_LineNo, labotd_Note, labotd_LOINCCode, labotd_Instruction, labotd_Precaution, Lab_Order_TestDtl.labotd_DateTime, 
        ''            ''labotd_SpecimenID, labsm_Code, labsm_Name, labotd_CollectionID, labcm_Code, labcm_Name, labotd_StorageID, labstm_Code, labstm_Name , 
        ''            '' nUserID, sLoginName, UserName, labotd_Comment
        ''            oPara = Nothing
        ''        End With

        ''        If Not dt Is Nothing Then
        ''            '' Order Test Details Object 
        ''            With oLabOrder.OrderTests
        ''                Dim oOrderTest As gloEMRActors.LabActor.OrderTest
        ''                For i = 0 To dt.Rows.Count - 1
        ''                    oOrderTest = New gloEMRActors.LabActor.OrderTest

        ''                    oOrderTest.TestID = dt.Rows(i)("labotd_TestID")
        ''                    oOrderTest.TestLineNo = dt.Rows(i)("labotd_LineNo")
        ''                    oOrderTest.Note = dt.Rows(i)("labotd_Note") & ""
        ''                    oOrderTest.LOINCCode = dt.Rows(i)("labotd_LOINCCode")
        ''                    oOrderTest.Instruction = dt.Rows(i)("labotd_Instruction") & ""
        ''                    oOrderTest.Precaution = dt.Rows(i)("labotd_Precaution") & ""
        ''                    oOrderTest.TestDateTime = dt.Rows(i)("labotd_DateTime")
        ''                    oOrderTest.Specimen = dt.Rows(i)("labsm_Name") & ""
        ''                    oOrderTest.Collection = dt.Rows(i)("labcm_Name") & ""
        ''                    oOrderTest.Storage = dt.Rows(i)("labstm_Name") & ""
        ''                    oOrderTest.UserID = dt.Rows(i)("nUserID")
        ''                    oOrderTest.Comments = dt.Rows(i)("labotd_Comment") & ""
        ''                    oOrderTest.DMSID = dt.Rows(i)("labotd_DMSID")

        ''                    '//// <> Fill Order Test Details Diagnosis & Treatment Object 
        ''                    Dim dtDia As New DataTable
        ''                    With _gloEMRDatabase
        ''                        Dim oPara As gloEMRDatabase.DBParameter
        ''                        oPara = New gloEMRDatabase.DBParameter
        ''                        .DBParametersCol.Clear()
        ''                        oPara.DataType = SqlDbType.BigInt
        ''                        oPara.Direction = ParameterDirection.Input
        ''                        oPara.Value = OrderID
        ''                        oPara.Name = "@OrderID"
        ''                        .DBParametersCol.Add(oPara)

        ''                        oPara = New gloEMRDatabase.DBParameter
        ''                        oPara.DataType = SqlDbType.BigInt
        ''                        oPara.Direction = ParameterDirection.Input
        ''                        oPara.Value = oOrderTest.TestID
        ''                        oPara.Name = "@TestID"
        ''                        .DBParametersCol.Add(oPara)

        ''                        oPara = New gloEMRDatabase.DBParameter
        ''                        oPara.DataType = SqlDbType.BigInt
        ''                        oPara.Direction = ParameterDirection.Input
        ''                        oPara.Value = 1   ' '' Diagnosis
        ''                        oPara.Name = "@Type"
        ''                        .DBParametersCol.Add(oPara)

        ''                        dtDia = .GetDataTable("Lab_GetOrderTestDtl_DiagCPT")
        ''                        '' labodtl_DiagCPTID, labodtl_Code, labodtl_Description, labodtl_Type
        ''                        oPara = Nothing
        ''                        For j = 0 To dtDia.Rows.Count - 1
        ''                            oOrderTest.Diagonosis.Add(dtDia.Rows(j)("labodtl_DiagCPTID"), dtDia.Rows(j)("labodtl_Code"), dtDia.Rows(j)("labodtl_Description"))
        ''                        Next
        ''                    End With

        ''                    Dim dtTreat As New DataTable
        ''                    With _gloEMRDatabase
        ''                        Dim oPara As gloEMRDatabase.DBParameter
        ''                        oPara = New gloEMRDatabase.DBParameter
        ''                        .DBParametersCol.Clear()
        ''                        oPara.DataType = SqlDbType.BigInt
        ''                        oPara.Direction = ParameterDirection.Input
        ''                        oPara.Value = OrderID
        ''                        oPara.Name = "@OrderID"
        ''                        .DBParametersCol.Add(oPara)

        ''                        oPara = New gloEMRDatabase.DBParameter
        ''                        oPara.DataType = SqlDbType.BigInt
        ''                        oPara.Direction = ParameterDirection.Input
        ''                        oPara.Value = oOrderTest.TestID
        ''                        oPara.Name = "@TestID"
        ''                        .DBParametersCol.Add(oPara)

        ''                        oPara = New gloEMRDatabase.DBParameter
        ''                        oPara.DataType = SqlDbType.BigInt
        ''                        oPara.Direction = ParameterDirection.Input
        ''                        oPara.Value = 2   ' '' Treatment
        ''                        oPara.Name = "@Type"
        ''                        .DBParametersCol.Add(oPara)

        ''                        dtTreat = .GetDataTable("Lab_GetOrderTestDtl_DiagCPT")
        ''                        '' labodtl_DiagCPTID, labodtl_Code, labodtl_Description, labodtl_Type
        ''                        oPara = Nothing

        ''                        For j = 0 To dtTreat.Rows.Count - 1
        ''                            oOrderTest.Treatments.Add(dtTreat.Rows(j)("labodtl_DiagCPTID"), dtTreat.Rows(j)("labodtl_Code"), dtTreat.Rows(j)("labodtl_Description"))
        ''                        Next

        ''                    End With


        ''                    '' //// <> Fill Order Test Result Object 
        ''                    Dim dtResult As New DataTable
        ''                    With _gloEMRDatabase
        ''                        Dim oPara As gloEMRDatabase.DBParameter
        ''                        .DBParametersCol.Clear()
        ''                        oPara = New gloEMRDatabase.DBParameter
        ''                        oPara.DataType = SqlDbType.BigInt
        ''                        oPara.Direction = ParameterDirection.Input
        ''                        oPara.Value = OrderID
        ''                        oPara.Name = "@OrderID"
        ''                        .DBParametersCol.Add(oPara)

        ''                        oPara = New gloEMRDatabase.DBParameter
        ''                        oPara.DataType = SqlDbType.BigInt
        ''                        oPara.Direction = ParameterDirection.Input
        ''                        oPara.Value = oOrderTest.TestID
        ''                        oPara.Name = "@TestID"
        ''                        .DBParametersCol.Add(oPara)

        ''                        dtResult = .GetDataTable("Lab_GetOrderTestResult")
        ''                        'labotr_TestResultNumber, labotr_TestResultName, labotr_TestResultDateTime
        ''                        oPara = Nothing
        ''                    End With

        ''                    With oOrderTest
        ''                        'Retrive from db
        ''                        Dim oTestResult As New gloEMRActors.LabActor.OrderTestResult

        ''                        For nResults As Int16 = 0 To dtResult.Rows.Count - 1
        ''                            oTestResult = New gloEMRActors.LabActor.OrderTestResult
        ''                            With oTestResult
        ''                                .OrderID = OrderID
        ''                                .TestID = oOrderTest.TestID
        ''                                .TestResultNumber = dtResult.Rows(nResults)("labotr_TestResultNumber")
        ''                                .TestResultName = dtResult.Rows(nResults)("labotr_TestResultName")
        ''                                .TestResultDateTime = dtResult.Rows(nResults)("labotr_TestResultDateTime")
        ''                                .IsFinished = dtResult.Rows(nResults)("labotr_IsFinished")
        ''                                .DMSID = dtResult.Rows(nResults)("labotr_DMSID")
        ''                            End With


        ''                            '' //// <> Fill Order Test Result Details Object 
        ''                            Dim dtResultDTL As New DataTable
        ''                            With _gloEMRDatabase
        ''                                Dim oPara As gloEMRDatabase.DBParameter
        ''                                .DBParametersCol.Clear()
        ''                                oPara = New gloEMRDatabase.DBParameter
        ''                                oPara.DataType = SqlDbType.BigInt
        ''                                oPara.Direction = ParameterDirection.Input
        ''                                oPara.Value = OrderID
        ''                                oPara.Name = "@OrderID"
        ''                                .DBParametersCol.Add(oPara)

        ''                                oPara = New gloEMRDatabase.DBParameter
        ''                                oPara.DataType = SqlDbType.BigInt
        ''                                oPara.Direction = ParameterDirection.Input
        ''                                oPara.Value = oOrderTest.TestID
        ''                                oPara.Name = "@TestID"
        ''                                .DBParametersCol.Add(oPara)

        ''                                oPara = New gloEMRDatabase.DBParameter
        ''                                oPara.DataType = SqlDbType.BigInt
        ''                                oPara.Direction = ParameterDirection.Input
        ''                                oPara.Value = oTestResult.TestResultNumber
        ''                                oPara.Name = "@TestResultNumber"
        ''                                .DBParametersCol.Add(oPara)

        ''                                dtResultDTL = .GetDataTable("Lab_GetOrderTestResultDetails")
        ''                                'labotrd_TestResultNumber, labotrd_ResultLineNo ,labotrd_ResultNameID , labotrd_ResultName,labotrd_ResultValue, labotrd_ResultUnit , labotrd_ResultRange,labotrd_ResultType ,
        ''                                'labotrd_ResultComment, labotrd_ResultWord, labotrd_ResultDMSID, labotrd_ResultUserID, labotrd_ResultDateTime()
        ''                                oPara = Nothing
        ''                            End With

        ''                            Dim oOrderTestResultDetail As gloEMRActors.LabActor.OrderTestResultDetail
        ''                            For k = 0 To dtResultDTL.Rows.Count - 1
        ''                                oOrderTestResultDetail = New gloEMRActors.LabActor.OrderTestResultDetail

        ''                                With oOrderTestResultDetail
        ''                                    .OrderID = OrderID
        ''                                    .TestID = oOrderTest.TestID
        ''                                    .TestResultNumber = dtResultDTL.Rows(k)("labotrd_TestResultNumber")
        ''                                    .ResultLineNo = dtResultDTL.Rows(k)("labotrd_ResultLineNo")
        ''                                    .ResultNameID = dtResultDTL.Rows(k)("labotrd_ResultNameID")
        ''                                    .ResultName = dtResultDTL.Rows(k)("labotrd_ResultName")
        ''                                    .ResultValue = dtResultDTL.Rows(k)("labotrd_ResultValue")
        ''                                    .ResultUnit = dtResultDTL.Rows(k)("labotrd_ResultUnit")
        ''                                    .ResultRange = dtResultDTL.Rows(k)("labotrd_ResultRange")
        ''                                    .ResultTypeCode = dtResultDTL.Rows(k)("labotrd_ResultType")
        ''                                    .AbnormalFlagCode = dtResultDTL.Rows(k)("labotrd_AbnormalFlag")
        ''                                    .ResultComment = dtResultDTL.Rows(k)("labotrd_ResultComment")
        ''                                    .ResultWord = dtResultDTL.Rows(k)("labotrd_ResultWord")
        ''                                    .ResultDMSID = dtResultDTL.Rows(k)("labotrd_ResultDMSID")
        ''                                    .UserID = dtResultDTL.Rows(k)("labotrd_ResultUserID")
        ''                                    .ResultDateTime = dtResultDTL.Rows(k)("labotrd_ResultDateTime")
        ''                                    .IsFinished = dtResultDTL.Rows(k)("labotrd_IsFinished")
        ''                                End With

        ''                                oTestResult.TestResultDetails.Add(oOrderTestResultDetail)
        ''                                oOrderTestResultDetail = Nothing
        ''                            Next
        ''                            .OrderTestResults.Add(oTestResult)
        ''                            oTestResult = Nothing
        ''                        Next

        ''                    End With

        ''                    .Add(oOrderTest)

        ''                Next
        ''            End With
        ''        End If
        ''        dt = Nothing


        ''        'dt = New DataTable
        ''        'With _gloEMRDatabase
        ''        '    Dim oPara As New gloEMRDatabase.DBParameter
        ''        '    oPara.DataType = SqlDbType.BigInt
        ''        '    oPara.Direction = ParameterDirection.Input
        ''        '    oPara.Value = OrderID
        ''        '    oPara.Name = "@OrderID"
        ''        '    .DBParametersCol.Add(oPara)
        ''        '    dt = .GetDataTable("Lab_GetOrderTestDtl")
        ''        '    ''labotd_TestID, labotd_LineNo, labotd_Note, labotd_LOINCCode, labotd_Instruction, labotd_Precaution, Lab_Order_TestDtl.labotd_DateTime, 
        ''        '    ''labotd_SpecimenID, labsm_Code, labsm_Name, labotd_CollectionID, labcm_Code, labcm_Name, labotd_StorageID, labstm_Code, labstm_Name , nUserID, sLoginName, UserName
        ''        'End With

        ''        'If Not dt Is Nothing Then
        ''        '    '' Order Ttest Details Object 
        ''        '    With oLabOrder.OrderTests
        ''        '        Dim oOrderTest As gloEMRActors.LabActor.OrderTest
        ''        '        For i = 0 To dt.Rows.Count - 1
        ''        '            oOrderTest = New gloEMRActors.LabActor.OrderTest

        ''        '            oOrderTest.TestID = dt.Rows(i)("labotd_TestID")
        ''        '            oOrderTest.TestLineNo = dt.Rows(i)("labotd_LineNo")
        ''        '            oOrderTest.Note = dt.Rows(i)("labotd_Note") & ""
        ''        '            oOrderTest.LOINCCode = dt.Rows(i)("labotd_LOINCCode")
        ''        '            oOrderTest.Instruction = dt.Rows(i)("labotd_Instruction") & ""
        ''        '            oOrderTest.Precaution = dt.Rows(i)("labotd_Precaution") & ""
        ''        '            oOrderTest.TestDateTime = dt.Rows(i)("labotd_DateTime")
        ''        '            oOrderTest.Specimen = dt.Rows(i)("labsm_Name") & ""
        ''        '            oOrderTest.Collection = dt.Rows(i)("labcm_Name") & ""
        ''        '            oOrderTest.Storage = dt.Rows(i)("labstm_Name") & ""
        ''        '            oOrderTest.UserID = dt.Rows(i)("nUserID")
        ''        '            .Add(oOrderTest)
        ''        '        Next
        ''        '    End With
        ''        'End If
        ''        'dt = Nothing


        ''        Return oLabOrder
        ''    Catch ex As Exception
        ''        Return Nothing
        ''    End Try
        ''End Function
        'Added by madan on 20100612-- for Previous history in view lab.

        ''Sanjog Added New Funct For Optimize
        Public Function GetOrderByHistory_New(ByVal OrderID As Int64, ByVal LastTransactionDate As Nullable(Of DateTime), ByVal PatientID As Int64) As gloEMRActors.LabActor.LabOrder
            _gloEMRDatabase = New DataBaseLayer

            Dim oLabOrder As New gloEMRActors.LabActor.LabOrder
            Dim DSMain As DataSet = Nothing
            Dim i As Integer

            Try
                Dim oParam As gloEMRDatabase.DBParameter
                'Get ALL Order Details against ORDERID
                With _gloEMRDatabase
                    oParam = New gloEMRDatabase.DBParameter
                    oParam.DataType = SqlDbType.BigInt
                    oParam.Direction = ParameterDirection.Input
                    oParam.Value = OrderID
                    oParam.Name = "@OrderID"
                    .DBParametersCol.Add(oParam)
                    oParam = Nothing

                    oParam = New gloEMRDatabase.DBParameter
                    oParam.DataType = SqlDbType.BigInt
                    oParam.Direction = ParameterDirection.Input
                    oParam.Value = PatientID
                    oParam.Name = "@PatientID"
                    .DBParametersCol.Add(oParam)
                    oParam = Nothing

                    oParam = New gloEMRDatabase.DBParameter
                    oParam.DataType = SqlDbType.Int
                    oParam.Direction = ParameterDirection.Input
                    If Not IsNothing(LastTransactionDate) Then
                        oParam.Value = 1
                    Else
                        oParam.Value = 0
                    End If

                    oParam.Name = "@IsTestResultDateTime"
                    .DBParametersCol.Add(oParam)
                    oParam = Nothing

                    oParam = New gloEMRDatabase.DBParameter
                    oParam.DataType = SqlDbType.DateTime
                    oParam.Direction = ParameterDirection.Input
                    oParam.Value = LastTransactionDate
                    oParam.Name = "@TestResultDateTime"
                    .DBParametersCol.Add(oParam)
                    oParam = Nothing

                    DSMain = .GetDataSet("Lab_GetOrderMaster_For_UC_Transaction")
                    oParam = Nothing
                End With

                If (IsNothing(DSMain) = False) Then
                    If DSMain.Tables(0).Rows.Count > 0 Then
                        ' '' Fill Order Master Object  
                        With oLabOrder
                            .OrderNoPrefix = DSMain.Tables(0).Rows(0)("labom_OrderNoPrefix")
                            .OrderNoID = DSMain.Tables(0).Rows(0)("labom_OrderNoID")
                            .TransactionDate = DSMain.Tables(0).Rows(0)("labom_TransactionDate")
                            .PatientID = DSMain.Tables(0).Rows(0)("labom_PatientID")
                            .PatientAge.Years = DSMain.Tables(0).Rows(0)("labom_PatientAgeYear")
                            .PatientAge.Months = DSMain.Tables(0).Rows(0)("labom_PatientAgeMonth")
                            .PatientAge.Days = DSMain.Tables(0).Rows(0)("labom_PatientAgeDay")
                            .ProviderID = DSMain.Tables(0).Rows(0)("labom_ProviderID")
                            .Provider = DSMain.Tables(0).Rows(0)("ProviderName")
                            .PreferredLab = DSMain.Tables(0).Rows(0)("ContactName")
                            .PreferredLabID = DSMain.Tables(0).Rows(0)("labom_PreferredLabID")
                            .SampledBy = DSMain.Tables(0).Rows(0)("SampledBy")
                            .SampledByID = DSMain.Tables(0).Rows(0)("labom_SampledByID")
                            .ReferredBy = DSMain.Tables(0).Rows(0)("RefferedBy")
                            .ReferredByID = DSMain.Tables(0).Rows(0)("labom_ReferredByID")
                            .ExternalCode = DSMain.Tables(0).Rows(0)("labom_ExternalCode")
                            .VisitID = DSMain.Tables(0).Rows(0)("labom_VisitID")
                            .DMSID = DSMain.Tables(0).Rows(0)("labom_DMSID")
                            .SendTo = DSMain.Tables(0).Rows(0)("SendTo")
                            .ReferredTo = DSMain.Tables(0).Rows(0)("RefferedTo")
                            .ReferredToID = DSMain.Tables(0).Rows(0)("labom_ReferredToID")

                            ''strQuery = "SELECT TM_TaskMST.nDueDate, TM_Task_Progress.sDescription FROM TM_TaskMST INNER JOIN TM_Task_Progress ON TM_TaskMST.nTaskID = TM_Task_Progress.nTaskID WHERE TM_TaskMST.nPatientID = " & .PatientID & " AND TM_TaskMST.nReferenceID1 = " & OrderID & " AND TM_TaskMST.nTaskType = 4"

                            ''dtTaskDtl = _gloEMRDatabase.GetDataTable_Query(strQuery)

                            If IsNothing(DSMain.Tables(1)) = False Then
                                If DSMain.Tables(1).Rows.Count > 0 Then
                                    If IsDBNull(DSMain.Tables(1).Rows(0)(0)) = False Then
                                        .TaskDueDate = gloDateMaster.gloDate.DateAsDate(DSMain.Tables(1).Rows(0)(0))
                                    End If
                                    If IsDBNull(DSMain.Tables(1).Rows(0)(1)) = False Then
                                        .TaskDescription = DSMain.Tables(1).Rows(0)(1)
                                    End If

                                    If Not DSMain.Tables(2) Is Nothing Then
                                        With oLabOrder.Users
                                            For i = 0 To DSMain.Tables(2).Rows.Count - 1
                                                .Add(DSMain.Tables(2).Rows(i)("nUserID"), "", DSMain.Tables(2).Rows(i)("sLoginName") & "")
                                            Next
                                        End With
                                    End If
                                End If

                            End If
                            ''
                        End With
                    End If

                    ''dt = Nothing

                    '//// <> Fill Order Test Details Object 
                    'dt = New DataTable
                    'With _gloEMRDatabase
                    '    Dim oPara As New gloEMRDatabase.DBParameter
                    '    .DBParametersCol.Clear()
                    '    oPara.DataType = SqlDbType.BigInt
                    '    oPara.Direction = ParameterDirection.Input
                    '    oPara.Value = OrderID
                    '    oPara.Name = "@OrderID"
                    '    .DBParametersCol.Add(oPara)

                    '    oPara = New gloEMRDatabase.DBParameter
                    '    oPara.DataType = SqlDbType.Bit
                    '    oPara.Direction = ParameterDirection.Input
                    '    oPara.Value = 1
                    '    oPara.Name = "@IsTestResultDateTime"
                    '    .DBParametersCol.Add(oPara)


                    '    oPara = New gloEMRDatabase.DBParameter
                    '    oPara.DataType = SqlDbType.DateTime
                    '    oPara.Direction = ParameterDirection.Input
                    '    oPara.Value = LastTransactionDate
                    '    oPara.Name = "@TestResultDateTime"
                    '    .DBParametersCol.Add(oPara)

                    '    dt = .GetDataTable("Lab_GetOrderTestDtl_gloLab")

                    '    oPara = Nothing
                    'End With


                    If Not DSMain.Tables(4) Is Nothing Then
                        oLabOrder.DiagnosisCount = DSMain.Tables(4).Rows.Count

                    End If
                    If Not DSMain.Tables(3) Is Nothing Then
                        '' Order Test Details Object 
                        With oLabOrder.OrderTests
                            Dim oOrderTest As gloEMRActors.LabActor.OrderTest
                            For i = 0 To DSMain.Tables(3).Rows.Count - 1
                                oOrderTest = New gloEMRActors.LabActor.OrderTest

                                oOrderTest.TestID = DSMain.Tables(3).Rows(i)("labotd_TestID")
                                oOrderTest.TestName = DSMain.Tables(3).Rows(i)("labotd_TestName")
                                oOrderTest.TestLineNo = DSMain.Tables(3).Rows(i)("labotd_LineNo")
                                oOrderTest.Note = DSMain.Tables(3).Rows(i)("labotd_Note") & ""
                                oOrderTest.LOINCCode = DSMain.Tables(3).Rows(i)("labotd_LOINCCode")
                                oOrderTest.CPT = DSMain.Tables(3).Rows(i)("sCPTCode")
                                oOrderTest.Instruction = DSMain.Tables(3).Rows(i)("labotd_Instruction") & ""
                                oOrderTest.Precaution = DSMain.Tables(3).Rows(i)("labotd_Precaution") & ""
                                oOrderTest.TestDateTime = DSMain.Tables(3).Rows(i)("labotd_DateTime")
                                oOrderTest.Is_Finished = DSMain.Tables(3).Rows(i)("labotd_IsFinished")

                                If IsDBNull(DSMain.Tables(3).Rows(i)("labotd_TestScheduledDateTime")) = False Then
                                    oOrderTest.ScheduleDateTime = DSMain.Tables(3).Rows(i)("labotd_TestScheduledDateTime")
                                Else
                                    oOrderTest.ScheduleDateTime = Nothing
                                End If


                                'Added by Mitesh
                                If DSMain.Tables(3).Columns.Contains("labotrd_TestSpecimenCollectionDateTime") = True Then
                                    If IsDBNull(DSMain.Tables(3).Rows(i)("labotrd_TestSpecimenCollectionDateTime")) = False Then
                                        oOrderTest.TestSpecimenCollectionDateTime = DSMain.Tables(3).Rows(i)("labotrd_TestSpecimenCollectionDateTime")
                                    Else
                                        oOrderTest.TestSpecimenCollectionDateTime = Nothing
                                    End If
                                Else
                                    oOrderTest.TestSpecimenCollectionDateTime = Nothing
                                End If

                                If DSMain.Tables(3).Columns.Contains("ReportedDateTime") = True Then
                                    If IsDBNull(DSMain.Tables(3).Rows(i)("ReportedDateTime")) = False Then
                                        oOrderTest.ReportedDateTime = DSMain.Tables(3).Rows(i)("ReportedDateTime")
                                    Else
                                        oOrderTest.ReportedDateTime = Nothing
                                    End If
                                Else
                                    oOrderTest.ReportedDateTime = Nothing
                                End If

                                oOrderTest.Specimen = DSMain.Tables(3).Rows(i)("SpecimenName") & ""
                                oOrderTest.Collection = DSMain.Tables(3).Rows(i)("CollectionName") & ""
                                oOrderTest.Storage = DSMain.Tables(3).Rows(i)("StorageTemperature") & ""
                                '    oOrderTest.SpecimenName = 
                                oOrderTest.UserID = DSMain.Tables(3).Rows(i)("nUserID")
                                oOrderTest.Comments = DSMain.Tables(3).Rows(i)("labotd_Comment") & ""
                                oOrderTest.DMSID = DSMain.Tables(3).Rows(i)("labotd_DMSID")
                                'Added by madan on 20101007-- for lab dicom
                                oOrderTest.DicomID = DSMain.Tables(3).Rows(i)("labotd_DICOMID")
                                'End madan

                                oOrderTest.TestStatus = DSMain.Tables(3).Rows(i)("labotd_TestStatus")
                                oOrderTest.SpecimenSource = DSMain.Tables(3).Rows(i)("labotd_SpecimenSource")
                                oOrderTest.SpecimenConditionDisp = DSMain.Tables(3).Rows(i)("labotd_SpecimenConditionDisp")
                                oOrderTest.TestCode = DSMain.Tables(3).Rows(i)("labtm_Code")
                                oOrderTest.TestType = DSMain.Tables(3).Rows(i)("labotd_TestType")

                                oOrderTest.SpecimenTypeIdentifier = DSMain.Tables(3).Rows(i)("labotd_SpecimenTypeIdentifier")
                                oOrderTest.SpecimenTypeText = DSMain.Tables(3).Rows(i)("labotd_SpecimenTypeText")
                                oOrderTest.SpecimenTypeCodingSystem = DSMain.Tables(3).Rows(i)("labotd_SpecimenTypeCodingSystem")
                                If Not IsDBNull(DSMain.Tables(3).Rows(i)("labotd_SpecimenCollectionStartDateTime")) Then
                                    oOrderTest.SpecimenCollectionStartDateTime = DSMain.Tables(3).Rows(i)("labotd_SpecimenCollectionStartDateTime")
                                Else
                                    oOrderTest.SpecimenCollectionStartDateTime = Nothing
                                End If
                                oOrderTest.SpecimenRejectReason = DSMain.Tables(3).Rows(i)("labotd_SpecimenRejectReason")
                                oOrderTest.SpecimenCondition = DSMain.Tables(3).Rows(i)("labotd_SpecimenCondition")
                                oOrderTest.SpecimenActionCode = DSMain.Tables(3).Rows(i)("labotd_SpecimenActionCode")
                                If Not IsDBNull(DSMain.Tables(3).Rows(i)("labotd_TestScheduledEndDateTime")) Then
                                    oOrderTest.TestScheduledEndDateTime = DSMain.Tables(3).Rows(i)("labotd_TestScheduledEndDateTime")
                                Else
                                    oOrderTest.TestScheduledEndDateTime = Nothing
                                End If
                                oOrderTest.labotd_DateTimeUTC = DSMain.Tables(3).Rows(i)("labotd_DateTimeUTC")
                                oOrderTest.labotd_TestScheduledDateTimeUTC = DSMain.Tables(3).Rows(i)("labotd_TestScheduledDateTimeUTC")
                                oOrderTest.labotd_TestScheduledEndDateTimeUTC = DSMain.Tables(3).Rows(i)("labotd_TestScheduledEndDateTimeUTC")
                                oOrderTest.labotd_SpecimenCollectionStartDateTimeUTC = DSMain.Tables(3).Rows(i)("labotd_SpecimenCollectionStartDateTimeUTC")
                               
                                ''//// <> Fill Order Test Details Diagnosis & Treatment Object 
                                'Dim dtDia As New DataTable
                                'With _gloEMRDatabase
                                '    Dim oPara As gloEMRDatabase.DBParameter
                                '    oPara = New gloEMRDatabase.DBParameter
                                '    .DBParametersCol.Clear()
                                '    oPara.DataType = SqlDbType.BigInt
                                '    oPara.Direction = ParameterDirection.Input
                                '    oPara.Value = OrderID
                                '    oPara.Name = "@OrderID"
                                '    .DBParametersCol.Add(oPara)

                                '    oPara = New gloEMRDatabase.DBParameter
                                '    oPara.DataType = SqlDbType.VarChar
                                '    oPara.Direction = ParameterDirection.Input
                                '    oPara.Value = oOrderTest.TestName
                                '    oPara.Name = "@TestName"
                                '    .DBParametersCol.Add(oPara)

                                '    oPara = New gloEMRDatabase.DBParameter
                                '    oPara.DataType = SqlDbType.BigInt
                                '    oPara.Direction = ParameterDirection.Input
                                '    oPara.Value = 1   ' '' Diagnosis
                                '    oPara.Name = "@Type"
                                '    .DBParametersCol.Add(oPara)

                                '    dtDia = .GetDataTable("Lab_GetOrderTestDtl_DiagCPT")
                                '    '' labodtl_DiagCPTID, labodtl_Code, labodtl_Description, labodtl_Type
                                '    oPara = Nothing
                                Dim DV_Diagnosys As DataView
                                DV_Diagnosys = DSMain.Tables(4).DefaultView()
                                Dim strFilter As String = ""
                                strFilter = "labodtl_TestID ='" & oOrderTest.TestID & "' AND TestName='" & oOrderTest.TestName.Replace("'", "''") & "' AND labodtl_Type='1'"

                                DV_Diagnosys.RowFilter = strFilter

                                ''nicdrevision added for icd10 functionality for emdeon screen
                                For Each drView As DataRowView In DV_Diagnosys
                                    'j = 0 To DV_Diagnosys.Count - 1
                                    oOrderTest.Diagonosis.Add(drView("labodtl_DiagCPTID"), drView("labodtl_Code"), drView("labodtl_Description"), drView("nIcdRevision"))
                                Next


                                'Dim dtTreat As New DataTable
                                'With _gloEMRDatabase
                                '    Dim oPara As gloEMRDatabase.DBParameter
                                '    oPara = New gloEMRDatabase.DBParameter
                                '    .DBParametersCol.Clear()
                                '    oPara.DataType = SqlDbType.BigInt
                                '    oPara.Direction = ParameterDirection.Input
                                '    oPara.Value = OrderID
                                '    oPara.Name = "@OrderID"
                                '    .DBParametersCol.Add(oPara)

                                '    oPara = New gloEMRDatabase.DBParameter
                                '    oPara.DataType = SqlDbType.VarChar
                                '    oPara.Direction = ParameterDirection.Input
                                '    oPara.Value = oOrderTest.TestName
                                '    oPara.Name = "@TestName"
                                '    .DBParametersCol.Add(oPara)

                                '    oPara = New gloEMRDatabase.DBParameter
                                '    oPara.DataType = SqlDbType.BigInt
                                '    oPara.Direction = ParameterDirection.Input
                                '    oPara.Value = 2   ' '' Treatment
                                '    oPara.Name = "@Type"
                                '    .DBParametersCol.Add(oPara)

                                '    dtTreat = .GetDataTable("Lab_GetOrderTestDtl_DiagCPT")
                                '    '' labodtl_DiagCPTID, labodtl_Code, labodtl_Description, labodtl_Type
                                '    oPara = Nothing

                                '    For j = 0 To dtTreat.Rows.Count - 1
                                '        oOrderTest.Treatments.Add(dtTreat.Rows(j)("labodtl_DiagCPTID"), dtTreat.Rows(j)("labodtl_Code"), dtTreat.Rows(j)("labodtl_Description"))
                                '    Next

                                'End With

                                DV_Diagnosys = DSMain.Tables(4).DefaultView()
                                strFilter = ""
                                strFilter = "labodtl_TestID ='" & oOrderTest.TestID & "' AND TestName='" & oOrderTest.TestName.Replace("'", "''") & "' AND labodtl_Type='2'"

                                DV_Diagnosys.RowFilter = strFilter

                                ''nicdrevision added for icd10 functionality for emdeon screen
                                For Each drView As DataRowView In DV_Diagnosys
                                    oOrderTest.Treatments.Add(drView("labodtl_DiagCPTID"), drView("labodtl_Code"), drView("labodtl_Description"), drView("nIcdRevision"))
                                Next




                                ' '' //// <> Fill Order Test Result Object 
                                'Dim dtResult As New DataTable
                                'With _gloEMRDatabase
                                '    Dim oPara As gloEMRDatabase.DBParameter
                                '    .DBParametersCol.Clear()
                                '    oPara = New gloEMRDatabase.DBParameter
                                '    oPara.DataType = SqlDbType.BigInt
                                '    oPara.Direction = ParameterDirection.Input
                                '    oPara.Value = OrderID
                                '    oPara.Name = "@OrderID"
                                '    .DBParametersCol.Add(oPara)

                                '    oPara = New gloEMRDatabase.DBParameter
                                '    oPara.DataType = SqlDbType.VarChar
                                '    oPara.Direction = ParameterDirection.Input
                                '    oPara.Value = oOrderTest.TestName
                                '    oPara.Name = "@TestName"
                                '    .DBParametersCol.Add(oPara)

                                '    oPara = New gloEMRDatabase.DBParameter
                                '    oPara.DataType = SqlDbType.Bit
                                '    oPara.Direction = ParameterDirection.Input
                                '    oPara.Value = 1
                                '    oPara.Name = "@IsTestResultDateTime"
                                '    .DBParametersCol.Add(oPara)


                                '    oPara = New gloEMRDatabase.DBParameter
                                '    oPara.DataType = SqlDbType.DateTime
                                '    oPara.Direction = ParameterDirection.Input
                                '    oPara.Value = LastTransactionDate
                                '    oPara.Name = "@TestResultDateTime"
                                '    .DBParametersCol.Add(oPara)

                                '    dtResult = .GetDataTable("Lab_GetOrderTestResult_gloLab")
                                '    'labotr_TestResultNumber, labotr_TestResultName, labotr_TestResultDateTime
                                '    oPara = Nothing
                                'End With


                                Dim DV_OrderTestResult As DataView
                                DV_OrderTestResult = DSMain.Tables(5).DefaultView()


                                strFilter = ""
                                'strFilter = "labotr_TestName='" & oOrderTest.TestName.Replace("'", "''") & "' AND labotr_TestResultDateTime='" & LastTransactionDate.ToString().Trim() & "'"
                                strFilter = "labotr_TestId ='" & oOrderTest.TestID & "' AND labotr_TestName='" & oOrderTest.TestName.Replace("'", "''") & "'"
                                DV_OrderTestResult.RowFilter = strFilter


                                With oOrderTest
                                    'Retrive from db
                                    '   Dim oTestResult As New gloEMRActors.LabActor.OrderTestResult

                                    For Each drTestResult As DataRowView In DV_OrderTestResult
                                        Dim oTestResult As gloEMRActors.LabActor.OrderTestResult = New gloEMRActors.LabActor.OrderTestResult
                                        With oTestResult
                                            .OrderID = OrderID
                                            .TestID = oOrderTest.TestID
                                            .TestName = oOrderTest.TestName
                                            .TestResultNumber = drTestResult("labotr_TestResultNumber")
                                            .TestResultName = drTestResult("labotr_TestResultName")
                                            .TestResultDateTime = drTestResult("labotr_TestResultDateTime")
                                            .IsFinished = drTestResult("labotr_IsFinished")
                                            .DMSID = drTestResult("labotr_DMSID")
                                            ' Added below two fileds as per qwest certification and same as updateing while save and close in view order form.
                                            'Added by madan-- on 20100409...
                                            Dim _blncheckDate As Boolean = IsDate(drTestResult("labotr_SpecimenReceivedDateTime"))
                                            If (_blncheckDate = True) Then
                                                .BlnSpecimenReceivedDateTime = True
                                                .SpecimenReceivedDateTime = drTestResult("labotr_SpecimenReceivedDateTime")
                                            Else
                                                .BlnSpecimenReceivedDateTime = False
                                            End If
                                            _blncheckDate = False
                                            _blncheckDate = IsDate(drTestResult("labotr_ResultTransferDateTime"))
                                            If (_blncheckDate = True) Then
                                                .BlnResultTransferDateTime = True
                                                .ResultTransferDateTime = drTestResult("labotr_ResultTransferDateTime")
                                            Else
                                                .BlnResultTransferDateTime = False
                                                .ResultTransferDateTime = Nothing
                                            End If

                                            .AlternateTestCode = drTestResult("labotr_AlternateTestCode")
                                            .AlternateTestName = drTestResult("labotr_AlternateTestName")

                                            .TestResultDateTimeUTC = Convert.ToInt32(drTestResult("labotr_TestResultDateTimeUTC"))
                                            .SpecimenReceivedDateTimeUTC = Convert.ToInt32(drTestResult("labotr_SpecimenReceivedDateTimeUTC"))
                                            .ResultTransferDateTimeUTC = Convert.ToInt32(drTestResult("labotr_ResultTransferDateTimeUTC"))


                                        End With


                                        '' //// <> Fill Order Test Result Details Object 
                                        'Dim dtResultDTL As New DataTable
                                        'With _gloEMRDatabase
                                        '    Dim oPara As gloEMRDatabase.DBParameter
                                        '    .DBParametersCol.Clear()
                                        '    oPara = New gloEMRDatabase.DBParameter
                                        '    oPara.DataType = SqlDbType.BigInt
                                        '    oPara.Direction = ParameterDirection.Input
                                        '    oPara.Value = OrderID
                                        '    oPara.Name = "@OrderID"
                                        '    .DBParametersCol.Add(oPara)

                                        '    oPara = New gloEMRDatabase.DBParameter
                                        '    oPara.DataType = SqlDbType.VarChar
                                        '    oPara.Direction = ParameterDirection.Input
                                        '    oPara.Value = oOrderTest.TestName
                                        '    oPara.Name = "@TestName"
                                        '    .DBParametersCol.Add(oPara)

                                        '    oPara = New gloEMRDatabase.DBParameter
                                        '    oPara.DataType = SqlDbType.BigInt
                                        '    oPara.Direction = ParameterDirection.Input
                                        '    oPara.Value = oTestResult.TestResultNumber
                                        '    oPara.Name = "@TestResultNumber"
                                        '    .DBParametersCol.Add(oPara)

                                        '    oPara = New gloEMRDatabase.DBParameter
                                        '    oPara.DataType = SqlDbType.Bit
                                        '    oPara.Direction = ParameterDirection.Input
                                        '    oPara.Value = 1
                                        '    oPara.Name = "@IsTestResultDateTime"
                                        '    .DBParametersCol.Add(oPara)


                                        '    oPara = New gloEMRDatabase.DBParameter
                                        '    oPara.DataType = SqlDbType.DateTime
                                        '    oPara.Direction = ParameterDirection.Input
                                        '    oPara.Value = LastTransactionDate
                                        '    oPara.Name = "@TestResultDateTime"
                                        '    .DBParametersCol.Add(oPara)

                                        '    dtResultDTL = .GetDataTable("Lab_GetOrderTestResultDetails_gloLab")
                                        '    'labotrd_TestResultNumber, labotrd_ResultLineNo ,labotrd_ResultNameID , labotrd_ResultName,labotrd_ResultValue, labotrd_ResultUnit , labotrd_ResultRange,labotrd_ResultType ,
                                        '    'labotrd_ResultComment, labotrd_ResultWord, labotrd_ResultDMSID, labotrd_ResultUserID, labotrd_ResultDateTime()
                                        '    oPara = Nothing
                                        'End With

                                        Dim DV_ResultDetails As DataView
                                        DV_ResultDetails = DSMain.Tables(6).DefaultView

                                        strFilter = ""
                                        strFilter = "labotrd_TestId ='" & oOrderTest.TestID & "'  AND labotrd_TestName='" & oOrderTest.TestName.Replace("'", "''") & "' AND labotrd_TestResultNumber='" & oTestResult.TestResultNumber & "'"

                                        DV_ResultDetails.RowFilter = strFilter


                                        Dim oOrderTestResultDetail As gloEMRActors.LabActor.OrderTestResultDetail
                                        For Each drResultDetails As DataRowView In DV_ResultDetails
                                            oOrderTestResultDetail = New gloEMRActors.LabActor.OrderTestResultDetail

                                            With oOrderTestResultDetail
                                                .OrderID = OrderID
                                                .TestID = oOrderTest.TestID
                                                .TestName = oOrderTest.TestName
                                                .TestResultNumber = drResultDetails("labotrd_TestResultNumber")
                                                .ResultLineNo = drResultDetails("labotrd_ResultLineNo")
                                                .ResultNameID = drResultDetails("labotrd_ResultNameID")
                                                .ResultName = drResultDetails("labotrd_ResultName")
                                                .ResultValue = drResultDetails("labotrd_ResultValue")
                                                .ResultUnit = drResultDetails("labotrd_ResultUnit")
                                                .ResultRange = drResultDetails("labotrd_ResultRange")
                                                .ResultTypeCode = drResultDetails("labotrd_ResultType")
                                                .AbnormalFlagCode = drResultDetails("labotrd_AbnormalFlag")
                                                .ResultComment = drResultDetails("labotrd_ResultComment")
                                                .ResultWord = drResultDetails("labotrd_ResultWord")
                                                .ResultDMSID = drResultDetails("labotrd_ResultDMSID")
                                                .UserID = drResultDetails("labotrd_ResultUserID")
                                                If Not IsDBNull(drResultDetails("labotrd_ResultDateTime")) Then
                                                    .ResultDateTime = drResultDetails("labotrd_ResultDateTime")
                                                Else
                                                    .ResultDateTime = Nothing
                                                End If

                                                .IsFinished = drResultDetails("labotrd_IsFinished")
                                                .ResultLOINCID = drResultDetails("labotrd_LOINCID")
                                                'Added below two fileds as per qwest certification and same as updateing while save and close in view order form.
                                                'Added by madan-- on 20100409...
                                                .AlternateResultName = drResultDetails("labotrd_AlternateResultName")
                                                .AlternateResultCode = drResultDetails("labotrd_AlternateResultCode")
                                                .ProducerIdentifier = drResultDetails("labotrd_ProducerIdentifier")

                                                ''Added field by Abhijeet on 20101026
                                                .LabFacilityName = drResultDetails("labotrd_LabFacilityName")
                                                .LabFacilityStreetAddress = drResultDetails("labotrd_LabFacilityStreetAddress")
                                                .LabFacilityCity = drResultDetails("labotrd_LabFacilityCity")
                                                .LabFacilityState = drResultDetails("labotrd_LabFacilityState")
                                                .LabFacilityZipCode = drResultDetails("labotrd_LabFacilityZipCode")
                                                .PatientSpecificRange = drResultDetails("labotrd_specificResultRefRange")
                                                ''End of changes by Abhijeet on 20101026
                                                'Sanjog
                                                .TestSpecimenCollectionDate = drResultDetails("labotrd_testspecimencollectiondatetime")
                                                'Sanjog

                                                .LabFacilityIdentifierTypeCode = drResultDetails("labotrd_LabFacilityIdentifierTypeCode")
                                                .LabFacilityOrganizationIdentifier = drResultDetails("labotrd_LabFacilityOrganizationIdentifier")
                                                .LabFacilityCountry = drResultDetails("labotrd_LabFacilityCountry")
                                                .LabFacilityCountyOrParishCode = drResultDetails("labotrd_LabFacilityCountyOrParishCode")
                                                .ResultCode = drResultDetails("labotrd_ResultCode")
                                                .ResultCodeType = drResultDetails("labotrd_ResultCodeType")
                                                .LabFacilityMedicalDirectorIDNumber = drResultDetails("labotrd_LabFacilityMedicalDirectorIDNumber")
                                                .LabFacilityMedicalDirectorLastName = drResultDetails("labotrd_LabFacilityMedicalDirectorLastName")
                                                .LabFacilityMedicalDirectorMiddleName = drResultDetails("labotrd_LabFacilityMedicalDirectorMiddleName")
                                                .LabFacilityMedicalDirectorSuffix = drResultDetails("labotrd_LabFacilityMedicalDirectorSuffix")
                                                .LabFacilityMedicalDirectorPrefix = drResultDetails("labotrd_LabFacilityMedicalDirectorPrefix")
                                                .LabFacilityMedicalDirectorFirstName = drResultDetails("labotrd_LabFacilityMedicalDirectorFirstName")
                                                .ResultParentChildFlag = Convert.ToInt64(drResultDetails("labotrd_ResultParentChildFlag"))
                                                .ResultDateTimeUTC = Convert.ToInt32(drResultDetails("labotrd_ResultDateTimeUTC"))
                                                .TestSpecimenCollectionDateTimeUTC = Convert.ToInt32(drResultDetails("labotrd_TestSpecimenCollectionDateTimeUTC"))

                                            End With

                                            oTestResult.TestResultDetails.Add(oOrderTestResultDetail)
                                            oOrderTestResultDetail = Nothing
                                        Next
                                        .OrderTestResults.Add(oTestResult)
                                        oTestResult = Nothing
                                    Next

                                End With

                                .Add(oOrderTest)

                            Next
                        End With
                    End If
                    DSMain.Dispose()
                    DSMain = Nothing
                End If
                Return oLabOrder

            Catch ex As Exception
                Throw ex
                Return Nothing

            Finally

                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function
        ''Sanjog Added New Funct For Optimize
        'Public Function GetOrderByHistory(ByVal OrderID As Int64, ByVal LastTransactionDate As DateTime) As gloEMRActors.LabActor.LabOrder
        '    _gloEMRDatabase = New DataBaseLayer

        '    Dim oLabOrder As New gloEMRActors.LabActor.LabOrder
        '    Dim dt As DataTable = Nothing
        '    Dim i, j, k As Integer

        '    Try

        '        '//// <> Order Master 
        '        'dt = New DataTable
        '        With _gloEMRDatabase
        '            Dim oPara As New gloEMRDatabase.DBParameter
        '            .DBParametersCol.Clear()
        '            oPara.DataType = SqlDbType.BigInt
        '            oPara.Direction = ParameterDirection.Input
        '            oPara.Value = OrderID
        '            oPara.Name = "@OrderID"
        '            .DBParametersCol.Add(oPara)
        '            ' ''Fill Order Master 
        '            dt = .GetDataTable("Lab_GetOrderMaster")
        '            ' ''labom_OrderNoPrefix, labom_OrderNoID, labom_TransactionDate, labom_PatientID, labom_PatientAgeYear, labom_PatientAgeMonth, labom_PatientAgeDay,
        '            ' ''labom_ProviderID, labom_PreferredLabID, labom_SampledByID, labom_ReferredByID, ProviderName, ContactName, SampledBy, RefferedBy
        '            oPara = Nothing
        '        End With

        '        If Not dt Is Nothing Then
        '            ' '' Fill Order Master Object  
        '            With oLabOrder
        '                .OrderNoPrefix = dt.Rows(0)("labom_OrderNoPrefix")
        '                .OrderNoID = dt.Rows(0)("labom_OrderNoID")
        '                .TransactionDate = dt.Rows(0)("labom_TransactionDate")
        '                .PatientID = dt.Rows(0)("labom_PatientID")
        '                .PatientAge.Years = dt.Rows(0)("labom_PatientAgeYear")
        '                .PatientAge.Months = dt.Rows(0)("labom_PatientAgeMonth")
        '                .PatientAge.Days = dt.Rows(0)("labom_PatientAgeDay")
        '                .ProviderID = dt.Rows(0)("labom_ProviderID")
        '                .Provider = dt.Rows(0)("ProviderName")
        '                .PreferredLab = dt.Rows(0)("ContactName")
        '                .PreferredLabID = dt.Rows(0)("labom_PreferredLabID")
        '                .SampledBy = dt.Rows(0)("SampledBy")
        '                .SampledByID = dt.Rows(0)("labom_SampledByID")
        '                .ReferredBy = dt.Rows(0)("RefferedBy")
        '                .ReferredByID = dt.Rows(0)("labom_ReferredByID")
        '                .ExternalCode = dt.Rows(0)("labom_ExternalCode")
        '                .VisitID = dt.Rows(0)("labom_VisitID")
        '                .DMSID = dt.Rows(0)("labom_DMSID")
        '                .SendTo = dt.Rows(0)("SendTo")
        '                .ReferredTo = dt.Rows(0)("RefferedTo")
        '                .ReferredToID = dt.Rows(0)("labom_ReferredToID")
        '                '' 20071121 Mahesh
        '                Dim strQuery As String = ""
        '                Dim dtTaskDtl As DataTable

        '                ''COMMENT BY SUDHIR 20090205 - AS NEW TM_TaskMST
        '                'strQuery = "SELECT dtDueDate , sNotes From  Tasks_Mst Where nPatientID=" & .PatientID & " AND dtTaskDate='" & .TransactionDate & "' AND nTaskType = 4"
        '                strQuery = "SELECT TM_TaskMST.nDueDate, TM_Task_Progress.sDescription FROM TM_TaskMST INNER JOIN TM_Task_Progress ON TM_TaskMST.nTaskID = TM_Task_Progress.nTaskID WHERE TM_TaskMST.nPatientID = " & .PatientID & " AND TM_TaskMST.nReferenceID1 = " & OrderID & " AND TM_TaskMST.nTaskType = 4"

        '                dtTaskDtl = _gloEMRDatabase.GetDataTable_Query(strQuery)
        '                If IsNothing(dtTaskDtl) = False Then
        '                    If dtTaskDtl.Rows.Count > 0 Then
        '                        If IsDBNull(dtTaskDtl.Rows(0)(0)) = False Then
        '                            .TaskDueDate = gloDateMaster.gloDate.DateAsDate(dtTaskDtl.Rows(0)(0))
        '                            '.TaskDueDate = dtTaskDtl.Rows(0)(0)
        '                        End If
        '                        If IsDBNull(dtTaskDtl.Rows(0)(1)) = False Then
        '                            .TaskDescription = dtTaskDtl.Rows(0)(1)
        '                        End If

        '                        '' Added on 20090707 
        '                        '' To Handle Inconsistant Data between Tasks & Labs
        '                        '//// <> Fill User Details Object 
        '                        Dim dtUser As DataTable = Nothing
        '                        With _gloEMRDatabase
        '                            Dim oPara As New gloEMRDatabase.DBParameter
        '                            .DBParametersCol.Clear()

        '                            oPara.DataType = SqlDbType.BigInt
        '                            oPara.Direction = ParameterDirection.Input
        '                            oPara.Value = OrderID
        '                            oPara.Name = "@OrderID"
        '                            .DBParametersCol.Add(oPara)
        '                            dtUser = .GetDataTable("Lab_GetOrderUser")
        '                            ' '' nUserID, sLoginName, UserName
        '                            oPara = Nothing
        '                        End With

        '                        If Not dtUser Is Nothing Then
        '                            '' Order Users Object 
        '                            '' Fill UserID & LoginName
        '                            With oLabOrder.Users
        '                                For i = 0 To dtUser.Rows.Count - 1
        '                                    .Add(dtUser.Rows(i)("nUserID"), "", dtUser.Rows(i)("sLoginName") & "")
        '                                Next
        '                            End With
        '                            dtUser.Dispose()
        '                            dtUser = Nothing
        '                        End If
        '                        dtUser = Nothing
        '                        ''
        '                    End If
        '                    dtTaskDtl.Dispose()
        '                    dtTaskDtl = Nothing
        '                End If
        '                ''
        '            End With
        '            dt.Dispose()
        '            dt = Nothing
        '        End If
        '        dt = Nothing

        '        '' Commented on 20090707 
        '        ''  code Shifted to above loop to Handle the inconsistant data between Labs & Tasks
        '        ''//// <> Fill User Details Object 
        '        'Dim dtUser As New DataTable
        '        'With _gloEMRDatabase
        '        '    Dim oPara As New gloEMRDatabase.DBParameter
        '        '    .DBParametersCol.Clear()

        '        '    oPara.DataType = SqlDbType.BigInt
        '        '    oPara.Direction = ParameterDirection.Input
        '        '    oPara.Value = OrderID
        '        '    oPara.Name = "@OrderID"
        '        '    .DBParametersCol.Add(oPara)
        '        '    dtUser = .GetDataTable("Lab_GetOrderUser")
        '        '    ' '' nUserID, sLoginName, UserName
        '        '    oPara = Nothing
        '        'End With

        '        'If Not dtUser Is Nothing Then
        '        '    '' Order Users Object 
        '        '    '' Fill UserID & LoginName
        '        '    With oLabOrder.Users
        '        '        For i = 0 To dtUser.Rows.Count - 1
        '        '            .Add(dtUser.Rows(i)("nUserID"), "", dtUser.Rows(i)("sLoginName") & "")
        '        '        Next
        '        '    End With
        '        'End If
        '        'dtUser = Nothing

        '        '//// <> Fill Order Test Details Object 
        '        'dt = New DataTable
        '        With _gloEMRDatabase
        '            Dim oPara As New gloEMRDatabase.DBParameter
        '            .DBParametersCol.Clear()
        '            oPara.DataType = SqlDbType.BigInt
        '            oPara.Direction = ParameterDirection.Input
        '            oPara.Value = OrderID
        '            oPara.Name = "@OrderID"
        '            .DBParametersCol.Add(oPara)

        '            oPara = New gloEMRDatabase.DBParameter
        '            oPara.DataType = SqlDbType.Bit
        '            oPara.Direction = ParameterDirection.Input
        '            oPara.Value = 1
        '            oPara.Name = "@IsTestResultDateTime"
        '            .DBParametersCol.Add(oPara)


        '            oPara = New gloEMRDatabase.DBParameter
        '            oPara.DataType = SqlDbType.DateTime
        '            oPara.Direction = ParameterDirection.Input
        '            oPara.Value = LastTransactionDate
        '            oPara.Name = "@TestResultDateTime"
        '            .DBParametersCol.Add(oPara)

        '            dt = .GetDataTable("Lab_GetOrderTestDtl_gloLab")
        '            ''labotd_TestID, labotd_LineNo, labotd_Note, labotd_LOINCCode, labotd_Instruction, labotd_Precaution, Lab_Order_TestDtl.labotd_DateTime, 
        '            ''labotd_SpecimenID, labsm_Code, labsm_Name, labotd_CollectionID, labcm_Code, labcm_Name, labotd_StorageID, labstm_Code, labstm_Name , 
        '            '' nUserID, sLoginName, UserName, labotd_Comment
        '            oPara = Nothing
        '        End With

        '        If Not dt Is Nothing Then
        '            '' Order Test Details Object 
        '            With oLabOrder.OrderTests
        '                Dim oOrderTest As gloEMRActors.LabActor.OrderTest
        '                For i = 0 To dt.Rows.Count - 1
        '                    oOrderTest = New gloEMRActors.LabActor.OrderTest

        '                    oOrderTest.TestID = dt.Rows(i)("labotd_TestID")
        '                    oOrderTest.TestName = dt.Rows(i)("labotd_TestName")
        '                    oOrderTest.TestLineNo = dt.Rows(i)("labotd_LineNo")
        '                    oOrderTest.Note = dt.Rows(i)("labotd_Note") & ""
        '                    oOrderTest.LOINCCode = dt.Rows(i)("labotd_LOINCCode")
        '                    oOrderTest.Instruction = dt.Rows(i)("labotd_Instruction") & ""
        '                    oOrderTest.Precaution = dt.Rows(i)("labotd_Precaution") & ""
        '                    oOrderTest.TestDateTime = dt.Rows(i)("labotd_DateTime")
        '                    oOrderTest.Specimen = dt.Rows(i)("SpecimenName") & ""
        '                    oOrderTest.Collection = dt.Rows(i)("CollectionName") & ""
        '                    oOrderTest.Storage = dt.Rows(i)("StorageTemperature") & ""
        '                    '    oOrderTest.SpecimenName = 
        '                    oOrderTest.UserID = dt.Rows(i)("nUserID")
        '                    oOrderTest.Comments = dt.Rows(i)("labotd_Comment") & ""
        '                    oOrderTest.DMSID = dt.Rows(i)("labotd_DMSID")
        '                    'Added by madan on 20101007-- for lab dicom
        '                    oOrderTest.DicomID = dt.Rows(i)("labotd_DICOMID")
        '                    'End madan

        '                    ''Added by Abhijeet on 20101026
        '                    oOrderTest.TestStatus = dt.Rows(i)("labotd_TestStatus")
        '                    oOrderTest.SpecimenSource = dt.Rows(i)("labotd_SpecimenSource")
        '                    oOrderTest.SpecimenConditionDisp = dt.Rows(i)("labotd_SpecimenConditionDisp")
        '                    oOrderTest.TestCode = dt.Rows(i)("labtm_Code")
        '                    oOrderTest.TestType = dt.Rows(i)("labotd_TestType")
        '                    ''End of changes by Abhijeet on 20101026

        '                    '//// <> Fill Order Test Details Diagnosis & Treatment Object 
        '                    Dim dtDia As DataTable = Nothing
        '                    With _gloEMRDatabase
        '                        Dim oPara As gloEMRDatabase.DBParameter
        '                        oPara = New gloEMRDatabase.DBParameter
        '                        .DBParametersCol.Clear()
        '                        oPara.DataType = SqlDbType.BigInt
        '                        oPara.Direction = ParameterDirection.Input
        '                        oPara.Value = OrderID
        '                        oPara.Name = "@OrderID"
        '                        .DBParametersCol.Add(oPara)

        '                        oPara = New gloEMRDatabase.DBParameter
        '                        oPara.DataType = SqlDbType.VarChar
        '                        oPara.Direction = ParameterDirection.Input
        '                        oPara.Value = oOrderTest.TestName
        '                        oPara.Name = "@TestName"
        '                        .DBParametersCol.Add(oPara)

        '                        oPara = New gloEMRDatabase.DBParameter
        '                        oPara.DataType = SqlDbType.BigInt
        '                        oPara.Direction = ParameterDirection.Input
        '                        oPara.Value = 1   ' '' Diagnosis
        '                        oPara.Name = "@Type"
        '                        .DBParametersCol.Add(oPara)

        '                        dtDia = .GetDataTable("Lab_GetOrderTestDtl_DiagCPT")
        '                        '' labodtl_DiagCPTID, labodtl_Code, labodtl_Description, labodtl_Type
        '                        oPara = Nothing
        '                        If (IsNothing(dtDia) = False) Then

        '                            oLabOrder.DiagnosisCount = dtDia.Rows.Count
        '                            For j = 0 To dtDia.Rows.Count - 1
        '                                oOrderTest.Diagonosis.Add(dtDia.Rows(j)("labodtl_DiagCPTID"), dtDia.Rows(j)("labodtl_Code"), dtDia.Rows(j)("labodtl_Description"))
        '                            Next
        '                            dtDia.Dispose()
        '                            dtDia = Nothing
        '                        End If
        '                    End With

        '                    Dim dtTreat As DataTable = Nothing
        '                    With _gloEMRDatabase
        '                        Dim oPara As gloEMRDatabase.DBParameter
        '                        oPara = New gloEMRDatabase.DBParameter
        '                        .DBParametersCol.Clear()
        '                        oPara.DataType = SqlDbType.BigInt
        '                        oPara.Direction = ParameterDirection.Input
        '                        oPara.Value = OrderID
        '                        oPara.Name = "@OrderID"
        '                        .DBParametersCol.Add(oPara)

        '                        oPara = New gloEMRDatabase.DBParameter
        '                        oPara.DataType = SqlDbType.VarChar
        '                        oPara.Direction = ParameterDirection.Input
        '                        oPara.Value = oOrderTest.TestName
        '                        oPara.Name = "@TestName"
        '                        .DBParametersCol.Add(oPara)

        '                        oPara = New gloEMRDatabase.DBParameter
        '                        oPara.DataType = SqlDbType.BigInt
        '                        oPara.Direction = ParameterDirection.Input
        '                        oPara.Value = 2   ' '' Treatment
        '                        oPara.Name = "@Type"
        '                        .DBParametersCol.Add(oPara)

        '                        dtTreat = .GetDataTable("Lab_GetOrderTestDtl_DiagCPT")
        '                        '' labodtl_DiagCPTID, labodtl_Code, labodtl_Description, labodtl_Type
        '                        oPara = Nothing
        '                        If (IsNothing(dtTreat) = False) Then


        '                            For j = 0 To dtTreat.Rows.Count - 1
        '                                oOrderTest.Treatments.Add(dtTreat.Rows(j)("labodtl_DiagCPTID"), dtTreat.Rows(j)("labodtl_Code"), dtTreat.Rows(j)("labodtl_Description"))
        '                            Next
        '                            dtTreat.Dispose()
        '                            dtTreat = Nothing
        '                        End If
        '                    End With


        '                    '' //// <> Fill Order Test Result Object 
        '                    Dim dtResult As DataTable = Nothing
        '                    With _gloEMRDatabase
        '                        Dim oPara As gloEMRDatabase.DBParameter
        '                        .DBParametersCol.Clear()
        '                        oPara = New gloEMRDatabase.DBParameter
        '                        oPara.DataType = SqlDbType.BigInt
        '                        oPara.Direction = ParameterDirection.Input
        '                        oPara.Value = OrderID
        '                        oPara.Name = "@OrderID"
        '                        .DBParametersCol.Add(oPara)

        '                        oPara = New gloEMRDatabase.DBParameter
        '                        oPara.DataType = SqlDbType.VarChar
        '                        oPara.Direction = ParameterDirection.Input
        '                        oPara.Value = oOrderTest.TestName
        '                        oPara.Name = "@TestName"
        '                        .DBParametersCol.Add(oPara)

        '                        oPara = New gloEMRDatabase.DBParameter
        '                        oPara.DataType = SqlDbType.Bit
        '                        oPara.Direction = ParameterDirection.Input
        '                        oPara.Value = 1
        '                        oPara.Name = "@IsTestResultDateTime"
        '                        .DBParametersCol.Add(oPara)


        '                        oPara = New gloEMRDatabase.DBParameter
        '                        oPara.DataType = SqlDbType.DateTime
        '                        oPara.Direction = ParameterDirection.Input
        '                        oPara.Value = LastTransactionDate
        '                        oPara.Name = "@TestResultDateTime"
        '                        .DBParametersCol.Add(oPara)

        '                        dtResult = .GetDataTable("Lab_GetOrderTestResult_gloLab")
        '                        'labotr_TestResultNumber, labotr_TestResultName, labotr_TestResultDateTime
        '                        oPara = Nothing
        '                    End With

        '                    If (IsNothing(dtResult) = False) Then


        '                        With oOrderTest
        '                            'Retrive from db
        '                            Dim oTestResult As gloEMRActors.LabActor.OrderTestResult = Nothing

        '                            For nResults As Int16 = 0 To dtResult.Rows.Count - 1
        '                                oTestResult = New gloEMRActors.LabActor.OrderTestResult
        '                                With oTestResult
        '                                    .OrderID = OrderID
        '                                    .TestID = oOrderTest.TestID
        '                                    .TestName = oOrderTest.TestName
        '                                    .TestResultNumber = dtResult.Rows(nResults)("labotr_TestResultNumber")
        '                                    .TestResultName = dtResult.Rows(nResults)("labotr_TestResultName")
        '                                    .TestResultDateTime = dtResult.Rows(nResults)("labotr_TestResultDateTime")
        '                                    .IsFinished = dtResult.Rows(nResults)("labotr_IsFinished")
        '                                    .DMSID = dtResult.Rows(nResults)("labotr_DMSID")
        '                                    ' Added below two fileds as per qwest certification and same as updateing while save and close in view order form.
        '                                    'Added by madan-- on 20100409...
        '                                    Dim _blncheckDate As Boolean = IsDate(dtResult.Rows(nResults)("labotr_SpecimenReceivedDateTime"))
        '                                    If (_blncheckDate = True) Then
        '                                        .BlnSpecimenReceivedDateTime = True
        '                                        .SpecimenReceivedDateTime = dtResult.Rows(nResults)("labotr_SpecimenReceivedDateTime")
        '                                    Else
        '                                        .BlnSpecimenReceivedDateTime = False
        '                                    End If
        '                                    _blncheckDate = False
        '                                    _blncheckDate = IsDate(dtResult.Rows(nResults)("labotr_ResultTransferDateTime"))
        '                                    If (_blncheckDate = True) Then
        '                                        .BlnResultTransferDateTime = True
        '                                        .ResultTransferDateTime = dtResult.Rows(nResults)("labotr_ResultTransferDateTime")
        '                                    Else
        '                                        .BlnResultTransferDateTime = False
        '                                        .ResultTransferDateTime = Nothing
        '                                    End If

        '                                    'If Not dtResult.Rows(nResults)("labotr_SpecimenReceivedDateTime") Is Nothing AndAlso dtResult.Rows(nResults)("labotr_SpecimenReceivedDateTime").ToString() = "" Then
        '                                    'If Not dtResult.Rows(nResults)("labotr_SpecimenReceivedDateTime") Is Nothing Then
        '                                    '    .BlnSpecimenReceivedDateTime = True
        '                                    'Else
        '                                    '    .SpecimenReceivedDateTime = dtResult.Rows(nResults)("labotr_SpecimenReceivedDateTime")
        '                                    'End If

        '                                    ''If dtResult.Rows(nResults)("labotr_ResultTransferDateTime") Is Nothing AndAlso dtResult.Rows(nResults)("labotr_ResultTransferDateTime").ToString() = "" Then
        '                                    'If Not dtResult.Rows(nResults)("labotr_ResultTransferDateTime") Is Nothing Then
        '                                    '    .BlnResultTransferDateTime = True
        '                                    'Else
        '                                    '    .ResultTransferDateTime = dtResult.Rows(nResults)("labotr_ResultTransferDateTime")
        '                                    'End If
        '                                    .AlternateTestCode = dtResult.Rows(nResults)("labotr_AlternateTestCode")
        '                                    .AlternateTestName = dtResult.Rows(nResults)("labotr_AlternateTestName")

        '                                End With


        '                                '' //// <> Fill Order Test Result Details Object 
        '                                Dim dtResultDTL As DataTable = Nothing
        '                                With _gloEMRDatabase
        '                                    Dim oPara As gloEMRDatabase.DBParameter
        '                                    .DBParametersCol.Clear()
        '                                    oPara = New gloEMRDatabase.DBParameter
        '                                    oPara.DataType = SqlDbType.BigInt
        '                                    oPara.Direction = ParameterDirection.Input
        '                                    oPara.Value = OrderID
        '                                    oPara.Name = "@OrderID"
        '                                    .DBParametersCol.Add(oPara)

        '                                    oPara = New gloEMRDatabase.DBParameter
        '                                    oPara.DataType = SqlDbType.VarChar
        '                                    oPara.Direction = ParameterDirection.Input
        '                                    oPara.Value = oOrderTest.TestName
        '                                    oPara.Name = "@TestName"
        '                                    .DBParametersCol.Add(oPara)

        '                                    oPara = New gloEMRDatabase.DBParameter
        '                                    oPara.DataType = SqlDbType.BigInt
        '                                    oPara.Direction = ParameterDirection.Input
        '                                    oPara.Value = oTestResult.TestResultNumber
        '                                    oPara.Name = "@TestResultNumber"
        '                                    .DBParametersCol.Add(oPara)

        '                                    oPara = New gloEMRDatabase.DBParameter
        '                                    oPara.DataType = SqlDbType.Bit
        '                                    oPara.Direction = ParameterDirection.Input
        '                                    oPara.Value = 1
        '                                    oPara.Name = "@IsTestResultDateTime"
        '                                    .DBParametersCol.Add(oPara)


        '                                    oPara = New gloEMRDatabase.DBParameter
        '                                    oPara.DataType = SqlDbType.DateTime
        '                                    oPara.Direction = ParameterDirection.Input
        '                                    oPara.Value = LastTransactionDate
        '                                    oPara.Name = "@TestResultDateTime"
        '                                    .DBParametersCol.Add(oPara)

        '                                    dtResultDTL = .GetDataTable("Lab_GetOrderTestResultDetails_gloLab")
        '                                    'labotrd_TestResultNumber, labotrd_ResultLineNo ,labotrd_ResultNameID , labotrd_ResultName,labotrd_ResultValue, labotrd_ResultUnit , labotrd_ResultRange,labotrd_ResultType ,
        '                                    'labotrd_ResultComment, labotrd_ResultWord, labotrd_ResultDMSID, labotrd_ResultUserID, labotrd_ResultDateTime()
        '                                    oPara = Nothing
        '                                End With

        '                                If (IsNothing(dtResultDTL) = False) Then


        '                                    Dim oOrderTestResultDetail As gloEMRActors.LabActor.OrderTestResultDetail
        '                                    For k = 0 To dtResultDTL.Rows.Count - 1
        '                                        oOrderTestResultDetail = New gloEMRActors.LabActor.OrderTestResultDetail

        '                                        With oOrderTestResultDetail
        '                                            .OrderID = OrderID
        '                                            .TestID = oOrderTest.TestID
        '                                            .TestName = oOrderTest.TestName
        '                                            .TestResultNumber = dtResultDTL.Rows(k)("labotrd_TestResultNumber")
        '                                            .ResultLineNo = dtResultDTL.Rows(k)("labotrd_ResultLineNo")
        '                                            .ResultNameID = dtResultDTL.Rows(k)("labotrd_ResultNameID")
        '                                            .ResultName = dtResultDTL.Rows(k)("labotrd_ResultName")
        '                                            .ResultValue = dtResultDTL.Rows(k)("labotrd_ResultValue")
        '                                            .ResultUnit = dtResultDTL.Rows(k)("labotrd_ResultUnit")
        '                                            .ResultRange = dtResultDTL.Rows(k)("labotrd_ResultRange")
        '                                            .ResultTypeCode = dtResultDTL.Rows(k)("labotrd_ResultType")
        '                                            .AbnormalFlagCode = dtResultDTL.Rows(k)("labotrd_AbnormalFlag")
        '                                            .ResultComment = dtResultDTL.Rows(k)("labotrd_ResultComment")
        '                                            .ResultWord = dtResultDTL.Rows(k)("labotrd_ResultWord")
        '                                            .ResultDMSID = dtResultDTL.Rows(k)("labotrd_ResultDMSID")
        '                                            .UserID = dtResultDTL.Rows(k)("labotrd_ResultUserID")
        '                                            .ResultDateTime = dtResultDTL.Rows(k)("labotrd_ResultDateTime")
        '                                            .IsFinished = dtResultDTL.Rows(k)("labotrd_IsFinished")
        '                                            .ResultLOINCID = dtResultDTL.Rows(k)("labotrd_LOINCID")
        '                                            'Added below two fileds as per qwest certification and same as updateing while save and close in view order form.
        '                                            'Added by madan-- on 20100409...
        '                                            .AlternateResultName = dtResultDTL.Rows(k)("labotrd_AlternateResultName")
        '                                            .AlternateResultCode = dtResultDTL.Rows(k)("labotrd_AlternateResultCode")
        '                                            .ProducerIdentifier = dtResultDTL.Rows(k)("labotrd_ProducerIdentifier")

        '                                            ''Added field by Abhijeet on 20101026
        '                                            .LabFacilityName = dtResultDTL.Rows(k)("labotrd_LabFacilityName")
        '                                            .LabFacilityStreetAddress = dtResultDTL.Rows(k)("labotrd_LabFacilityStreetAddress")
        '                                            .LabFacilityCity = dtResultDTL.Rows(k)("labotrd_LabFacilityCity")
        '                                            .LabFacilityState = dtResultDTL.Rows(k)("labotrd_LabFacilityState")
        '                                            .LabFacilityZipCode = dtResultDTL.Rows(k)("labotrd_LabFacilityZipCode")
        '                                            .PatientSpecificRange = dtResultDTL.Rows(k)("labotrd_specificResultRefRange")
        '                                            ''End of changes by Abhijeet on 20101026
        '                                            'Sanjog
        '                                            .TestSpecimenCollectionDate = dtResultDTL.Rows(k)("labotrd_testspecimencollectiondatetime")
        '                                            'Sanjog
        '                                        End With

        '                                        oTestResult.TestResultDetails.Add(oOrderTestResultDetail)
        '                                        oOrderTestResultDetail = Nothing
        '                                    Next
        '                                    dtResultDTL.Dispose()
        '                                    dtResultDTL = Nothing
        '                                End If
        '                                .OrderTestResults.Add(oTestResult)
        '                                oTestResult = Nothing
        '                            Next

        '                        End With
        '                        dtResult.Dispose()
        '                        dtResult = Nothing
        '                    End If
        '                    .Add(oOrderTest)

        '                Next
        '            End With
        '            dt.Dispose()
        '            dt = Nothing
        '        End If
        '        dt = Nothing


        '        'dt = New DataTable
        '        'With _gloEMRDatabase
        '        '    Dim oPara As New gloEMRDatabase.DBParameter
        '        '    oPara.DataType = SqlDbType.BigInt
        '        '    oPara.Direction = ParameterDirection.Input
        '        '    oPara.Value = OrderID
        '        '    oPara.Name = "@OrderID"
        '        '    .DBParametersCol.Add(oPara)
        '        '    dt = .GetDataTable("Lab_GetOrderTestDtl")
        '        '    ''labotd_TestID, labotd_LineNo, labotd_Note, labotd_LOINCCode, labotd_Instruction, labotd_Precaution, Lab_Order_TestDtl.labotd_DateTime, 
        '        '    ''labotd_SpecimenID, labsm_Code, labsm_Name, labotd_CollectionID, labcm_Code, labcm_Name, labotd_StorageID, labstm_Code, labstm_Name , nUserID, sLoginName, UserName
        '        'End With

        '        'If Not dt Is Nothing Then
        '        '    '' Order Ttest Details Object 
        '        '    With oLabOrder.OrderTests
        '        '        Dim oOrderTest As gloEMRActors.LabActor.OrderTest
        '        '        For i = 0 To dt.Rows.Count - 1
        '        '            oOrderTest = New gloEMRActors.LabActor.OrderTest

        '        '            oOrderTest.TestID = dt.Rows(i)("labotd_TestID")
        '        '            oOrderTest.TestLineNo = dt.Rows(i)("labotd_LineNo")
        '        '            oOrderTest.Note = dt.Rows(i)("labotd_Note") & ""
        '        '            oOrderTest.LOINCCode = dt.Rows(i)("labotd_LOINCCode")
        '        '            oOrderTest.Instruction = dt.Rows(i)("labotd_Instruction") & ""
        '        '            oOrderTest.Precaution = dt.Rows(i)("labotd_Precaution") & ""
        '        '            oOrderTest.TestDateTime = dt.Rows(i)("labotd_DateTime")
        '        '            oOrderTest.Specimen = dt.Rows(i)("labsm_Name") & ""
        '        '            oOrderTest.Collection = dt.Rows(i)("labcm_Name") & ""
        '        '            oOrderTest.Storage = dt.Rows(i)("labstm_Name") & ""
        '        '            oOrderTest.UserID = dt.Rows(i)("nUserID")
        '        '            .Add(oOrderTest)
        '        '        Next
        '        '    End With
        '        'End If
        '        'dt = Nothing


        '        Return oLabOrder
        '    Catch ex As Exception
        '        Throw ex
        '        Return Nothing
        '    Finally
        '        If Not IsNothing(_gloEMRDatabase) Then
        '            _gloEMRDatabase.Dispose()
        '            _gloEMRDatabase = Nothing
        '        End If
        '    End Try
        'End Function
        ''Sanjog For LabOrder With Prior
        Public Function GetOrder_New(ByVal OrderID As Int64, ByVal PatientID As Int64, ByVal PriorResult As Integer) As gloEMRActors.LabActor.LabOrder
            _gloEMRDatabase = New DataBaseLayer
            Dim DSMain As DataSet = Nothing
            Dim oLabOrder As New gloEMRActors.LabActor.LabOrder
            Dim i, k As Integer

            Try
                '//// <> Order Master 

                Dim oParam As gloEMRDatabase.DBParameter
                'Get ALL Order Details against ORDERID
                With _gloEMRDatabase
                    oParam = New gloEMRDatabase.DBParameter
                    oParam.DataType = SqlDbType.BigInt
                    oParam.Direction = ParameterDirection.Input
                    oParam.Value = OrderID
                    oParam.Name = "@OrderID"
                    .DBParametersCol.Add(oParam)
                    oParam = Nothing

                    oParam = New gloEMRDatabase.DBParameter
                    oParam.DataType = SqlDbType.BigInt
                    oParam.Direction = ParameterDirection.Input
                    oParam.Value = PatientID
                    oParam.Name = "@PatientID"
                    .DBParametersCol.Add(oParam)
                    oParam = Nothing

                    oParam = New gloEMRDatabase.DBParameter
                    oParam.DataType = SqlDbType.Int
                    oParam.Direction = ParameterDirection.Input
                    oParam.Value = PriorResult
                    oParam.Name = "@PriorResult"
                    .DBParametersCol.Add(oParam)
                    oParam = Nothing


                    DSMain = .GetDataSet("Lab_GetOrderMaster_For_UC_Transaction_WithPrior")
                    oParam = Nothing
                End With




                'dt = New DataTable
                'With _gloEMRDatabase
                '    Dim oPara As New gloEMRDatabase.DBParameter
                '    .DBParametersCol.Clear()
                '    oPara.DataType = SqlDbType.BigInt
                '    oPara.Direction = ParameterDirection.Input
                '    oPara.Value = OrderID
                '    oPara.Name = "@OrderID"
                '    .DBParametersCol.Add(oPara)
                '    ' ''Fill Order Master 
                '    dt = .GetDataTable("Lab_GetOrderMaster")
                '    oPara = Nothing
                'End With
                If (IsNothing(DSMain) = False) Then


                    If Not DSMain.Tables(0) Is Nothing Then
                        ' '' Fill Order Master Object  
                        With oLabOrder
                            .OrderNoPrefix = DSMain.Tables(0).Rows(0)("labom_OrderNoPrefix")
                            .OrderNoID = DSMain.Tables(0).Rows(0)("labom_OrderNoID")
                            .TransactionDate = DSMain.Tables(0).Rows(0)("labom_TransactionDate")
                            .PatientID = DSMain.Tables(0).Rows(0)("labom_PatientID")
                            .PatientAge.Years = DSMain.Tables(0).Rows(0)("labom_PatientAgeYear")
                            .PatientAge.Months = DSMain.Tables(0).Rows(0)("labom_PatientAgeMonth")
                            .PatientAge.Days = DSMain.Tables(0).Rows(0)("labom_PatientAgeDay")
                            .ProviderID = DSMain.Tables(0).Rows(0)("labom_ProviderID")
                            .Provider = DSMain.Tables(0).Rows(0)("ProviderName")
                            .PreferredLab = DSMain.Tables(0).Rows(0)("ContactName")
                            .PreferredLabID = DSMain.Tables(0).Rows(0)("labom_PreferredLabID")
                            .SampledBy = DSMain.Tables(0).Rows(0)("SampledBy")
                            .SampledByID = DSMain.Tables(0).Rows(0)("labom_SampledByID")
                            .ReferredBy = DSMain.Tables(0).Rows(0)("RefferedBy")
                            .ReferredByID = DSMain.Tables(0).Rows(0)("labom_ReferredByID")
                            .ExternalCode = DSMain.Tables(0).Rows(0)("labom_ExternalCode")
                            .VisitID = DSMain.Tables(0).Rows(0)("labom_VisitID")
                            .DMSID = DSMain.Tables(0).Rows(0)("labom_DMSID")
                            .SendTo = DSMain.Tables(0).Rows(0)("SendTo")
                            .ReferredTo = DSMain.Tables(0).Rows(0)("RefferedTo")
                            .ReferredToID = DSMain.Tables(0).Rows(0)("labom_ReferredToID")
                            '' 20071121 Mahesh
                            'Dim strQuery As String = ""
                            'Dim dtTaskDtl As DataTable

                            ' ''COMMENT BY SUDHIR 20090205 - AS NEW TM_TaskMST
                            ''strQuery = "SELECT dtDueDate , sNotes From  Tasks_Mst Where nPatientID=" & .PatientID & " AND dtTaskDate='" & .TransactionDate & "' AND nTaskType = 4"
                            'strQuery = "SELECT TM_TaskMST.nDueDate, TM_Task_Progress.sDescription FROM TM_TaskMST INNER JOIN TM_Task_Progress ON TM_TaskMST.nTaskID = TM_Task_Progress.nTaskID WHERE TM_TaskMST.nPatientID = " & .PatientID & " AND TM_TaskMST.nReferenceID1 = " & OrderID & " AND TM_TaskMST.nTaskType = 4"

                            'dtTaskDtl = _gloEMRDatabase.GetDataTable_Query(strQuery)
                            If Not DSMain.Tables(1) Is Nothing Then
                                If DSMain.Tables(1).Rows.Count > 0 Then
                                    If IsDBNull(DSMain.Tables(1).Rows(0)(0)) = False Then
                                        .TaskDueDate = gloDateMaster.gloDate.DateAsDate(DSMain.Tables(1).Rows(0)(0))
                                        '.TaskDueDate = dtTaskDtl.Rows(0)(0)
                                    End If
                                    If IsDBNull(DSMain.Tables(1).Rows(0)(1)) = False Then
                                        .TaskDescription = DSMain.Tables(1).Rows(0)(1)
                                    End If

                                    '' Added on 20090707 
                                    '' To Handle Inconsistant Data between Tasks & Labs
                                    '//// <> Fill User Details Object 

                                    'Dim dtUser As New DataTable
                                    'With _gloEMRDatabase
                                    '    Dim oPara As New gloEMRDatabase.DBParameter
                                    '    .DBParametersCol.Clear()

                                    '    oPara.DataType = SqlDbType.BigInt
                                    '    oPara.Direction = ParameterDirection.Input
                                    '    oPara.Value = OrderID
                                    '    oPara.Name = "@OrderID"
                                    '    .DBParametersCol.Add(oPara)
                                    '    dtUser = .GetDataTable("Lab_GetOrderUser")
                                    '    ' '' nUserID, sLoginName, UserName
                                    '    oPara = Nothing
                                    'End With

                                    If Not DSMain.Tables(2) Is Nothing Then
                                        '' Order Users Object 
                                        '' Fill UserID & LoginName
                                        With oLabOrder.Users
                                            For i = 0 To DSMain.Tables(2).Rows.Count - 1
                                                .Add(DSMain.Tables(2).Rows(i)("nUserID"), "", DSMain.Tables(2).Rows(i)("sLoginName") & "")
                                            Next
                                        End With
                                    End If

                                    ''
                                End If

                            End If
                            ''
                        End With

                    End If
                    'dt = Nothing

                    '//// <> Fill Order Test Details Object 
                    'dt = New DataTable
                    'With _gloEMRDatabase
                    '    Dim oPara As New gloEMRDatabase.DBParameter
                    '    .DBParametersCol.Clear()
                    '    oPara.DataType = SqlDbType.BigInt
                    '    oPara.Direction = ParameterDirection.Input
                    '    oPara.Value = OrderID
                    '    oPara.Name = "@OrderID"
                    '    .DBParametersCol.Add(oPara)
                    '    dt = .GetDataTable("Lab_GetOrderTestDtl")
                    '    oPara = Nothing
                    'End With
                    If Not DSMain.Tables(4) Is Nothing Then
                        oLabOrder.DiagnosisCount = DSMain.Tables(4).Rows.Count

                    End If
                    If Not DSMain.Tables(3) Is Nothing Then
                        '' Order Test Details Object 
                        With oLabOrder.OrderTests
                            Dim oOrderTest As gloEMRActors.LabActor.OrderTest
                            For i = 0 To DSMain.Tables(3).Rows.Count - 1
                                oOrderTest = New gloEMRActors.LabActor.OrderTest

                                oOrderTest.TestID = DSMain.Tables(3).Rows(i)("labotd_TestID")
                                oOrderTest.TestName = DSMain.Tables(3).Rows(i)("labotd_TestName")
                                oOrderTest.TestLineNo = DSMain.Tables(3).Rows(i)("labotd_LineNo")
                                oOrderTest.Note = DSMain.Tables(3).Rows(i)("labotd_Note") & ""
                                oOrderTest.LOINCCode = DSMain.Tables(3).Rows(i)("labotd_LOINCCode")
                                oOrderTest.CPT = DSMain.Tables(3).Rows(i)("sCPTCode")
                                oOrderTest.Instruction = DSMain.Tables(3).Rows(i)("labotd_Instruction") & ""
                                oOrderTest.Precaution = DSMain.Tables(3).Rows(i)("labotd_Precaution") & ""
                                oOrderTest.TestDateTime = DSMain.Tables(3).Rows(i)("labotd_DateTime")
                                oOrderTest.Is_Finished = DSMain.Tables(3).Rows(i)("labotd_IsFinished")


                                If IsDBNull(DSMain.Tables(3).Rows(i)("labotd_TestScheduledDateTime")) = False Then
                                    oOrderTest.ScheduleDateTime = DSMain.Tables(3).Rows(i)("labotd_TestScheduledDateTime")
                                Else
                                    oOrderTest.ScheduleDateTime = Nothing
                                End If

                                'Added by Mitesh
                                If IsDBNull(DSMain.Tables(3).Rows(i)("labotrd_TestSpecimenCollectionDateTime")) = False Then
                                    oOrderTest.TestSpecimenCollectionDateTime = DSMain.Tables(3).Rows(i)("labotrd_TestSpecimenCollectionDateTime")
                                Else
                                    oOrderTest.TestSpecimenCollectionDateTime = Nothing
                                End If

                                If IsDBNull(DSMain.Tables(3).Rows(i)("ReportedDateTime")) = False Then
                                    oOrderTest.ReportedDateTime = DSMain.Tables(3).Rows(i)("ReportedDateTime")
                                Else
                                    oOrderTest.ReportedDateTime = Nothing
                                End If


                                oOrderTest.Specimen = DSMain.Tables(3).Rows(i)("SpecimenName") & ""
                                oOrderTest.Collection = DSMain.Tables(3).Rows(i)("CollectionName") & ""
                                oOrderTest.Storage = DSMain.Tables(3).Rows(i)("StorageTemperature") & ""
                                '    oOrderTest.SpecimenName = 
                                oOrderTest.UserID = DSMain.Tables(3).Rows(i)("nUserID")
                                oOrderTest.Comments = DSMain.Tables(3).Rows(i)("labotd_Comment") & ""
                                oOrderTest.DMSID = DSMain.Tables(3).Rows(i)("labotd_DMSID")
                                '//Added by madan on 20101007
                                oOrderTest.DicomID = DSMain.Tables(3).Rows(i)("labotd_DICOMID")

                                ''Added by Abhijeet on 20101027
                                oOrderTest.TestStatus = DSMain.Tables(3).Rows(i)("labotd_TestStatus")
                                oOrderTest.SpecimenSource = DSMain.Tables(3).Rows(i)("labotd_SpecimenSource")
                                oOrderTest.SpecimenConditionDisp = DSMain.Tables(3).Rows(i)("labotd_SpecimenConditionDisp")
                                oOrderTest.TestCode = DSMain.Tables(3).Rows(i)("labtm_Code")
                                oOrderTest.TestType = DSMain.Tables(3).Rows(i)("labotd_TestType")
                                ''end of changes Added by Abhijeet on 20101027

                                oOrderTest.SpecimenTypeIdentifier = DSMain.Tables(3).Rows(i)("labotd_SpecimenTypeIdentifier")
                                oOrderTest.SpecimenTypeText = DSMain.Tables(3).Rows(i)("labotd_SpecimenTypeText")
                                oOrderTest.SpecimenTypeCodingSystem = DSMain.Tables(3).Rows(i)("labotd_SpecimenTypeCodingSystem")
                                If Not IsDBNull(DSMain.Tables(3).Rows(i)("labotd_SpecimenCollectionStartDateTime")) Then
                                    oOrderTest.SpecimenCollectionStartDateTime = DSMain.Tables(3).Rows(i)("labotd_SpecimenCollectionStartDateTime")
                                Else
                                    oOrderTest.SpecimenCollectionStartDateTime = Nothing
                                End If
                                oOrderTest.SpecimenRejectReason = DSMain.Tables(3).Rows(i)("labotd_SpecimenRejectReason")
                                oOrderTest.SpecimenCondition = DSMain.Tables(3).Rows(i)("labotd_SpecimenCondition")
                                oOrderTest.SpecimenActionCode = DSMain.Tables(3).Rows(i)("labotd_SpecimenActionCode")
                                If Not IsDBNull(DSMain.Tables(3).Rows(i)("labotd_TestScheduledEndDateTime")) Then
                                    oOrderTest.TestScheduledEndDateTime = DSMain.Tables(3).Rows(i)("labotd_TestScheduledEndDateTime")
                                Else
                                    oOrderTest.TestScheduledEndDateTime = Nothing
                                End If
                                oOrderTest.labotd_DateTimeUTC = DSMain.Tables(3).Rows(i)("labotd_DateTimeUTC")
                                oOrderTest.labotd_TestScheduledDateTimeUTC = DSMain.Tables(3).Rows(i)("labotd_TestScheduledDateTimeUTC")
                                oOrderTest.labotd_TestScheduledEndDateTimeUTC = DSMain.Tables(3).Rows(i)("labotd_TestScheduledEndDateTimeUTC")
                                oOrderTest.labotd_SpecimenCollectionStartDateTimeUTC = DSMain.Tables(3).Rows(i)("labotd_SpecimenCollectionStartDateTimeUTC")
                                oOrderTest.TestPreferredLabID = DSMain.Tables(3).Rows(i)("labodtl_PreferredlabId")
                                oOrderTest.TestPreferredLab = DSMain.Tables(3).Rows(i)("labodtl_Preferredlab")

                                '//// <> Fill Order Test Details Diagnosis & Treatment Object 
                                'Dim dtDia As New DataTable
                                'With _gloEMRDatabase
                                '    Dim oPara As gloEMRDatabase.DBParameter
                                '    oPara = New gloEMRDatabase.DBParameter
                                '    .DBParametersCol.Clear()
                                '    oPara.DataType = SqlDbType.BigInt
                                '    oPara.Direction = ParameterDirection.Input
                                '    oPara.Value = OrderID
                                '    oPara.Name = "@OrderID"
                                '    .DBParametersCol.Add(oPara)

                                '    oPara = New gloEMRDatabase.DBParameter
                                '    oPara.DataType = SqlDbType.VarChar
                                '    oPara.Direction = ParameterDirection.Input
                                '    oPara.Value = oOrderTest.TestName
                                '    oPara.Name = "@TestName"
                                '    .DBParametersCol.Add(oPara)

                                '    oPara = New gloEMRDatabase.DBParameter
                                '    oPara.DataType = SqlDbType.BigInt
                                '    oPara.Direction = ParameterDirection.Input
                                '    oPara.Value = 1   ' '' Diagnosis
                                '    oPara.Name = "@Type"
                                '    .DBParametersCol.Add(oPara)

                                '    dtDia = .GetDataTable("Lab_GetOrderTestDtl_DiagCPT")
                                '    '' labodtl_DiagCPTID, labodtl_Code, labodtl_Description, labodtl_Type
                                '    oPara = Nothing
                                '    For j = 0 To dtDia.Rows.Count - 1
                                '        oOrderTest.Diagonosis.Add(dtDia.Rows(j)("labodtl_DiagCPTID"), dtDia.Rows(j)("labodtl_Code"), dtDia.Rows(j)("labodtl_Description"))
                                '    Next
                                'End With


                                Dim DV_Diagnosys As DataView
                                DV_Diagnosys = DSMain.Tables(4).DefaultView()

                                Dim strFilter As String = ""
                                strFilter = "labodtl_TestID ='" & oOrderTest.TestID & "' AND  TestName='" & oOrderTest.TestName.Replace("'", "''") & "' AND labodtl_Type='1'"


                                DV_Diagnosys.RowFilter = strFilter

                                ''nicdrevision added for icd10 functionality for emdeon screen
                                For Each drView As DataRowView In DV_Diagnosys
                                    'j = 0 To DV_Diagnosys.Count - 1
                                    oOrderTest.Diagonosis.Add(drView("labodtl_DiagCPTID"), drView("labodtl_Code"), drView("labodtl_Description"), drView("nIcdRevision"))
                                Next




                                'Dim dtTreat As New DataTable
                                'With _gloEMRDatabase
                                '    Dim oPara As gloEMRDatabase.DBParameter
                                '    oPara = New gloEMRDatabase.DBParameter
                                '    .DBParametersCol.Clear()
                                '    oPara.DataType = SqlDbType.BigInt
                                '    oPara.Direction = ParameterDirection.Input
                                '    oPara.Value = OrderID
                                '    oPara.Name = "@OrderID"
                                '    .DBParametersCol.Add(oPara)

                                '    oPara = New gloEMRDatabase.DBParameter
                                '    oPara.DataType = SqlDbType.VarChar
                                '    oPara.Direction = ParameterDirection.Input
                                '    oPara.Value = oOrderTest.TestName
                                '    oPara.Name = "@TestName"
                                '    .DBParametersCol.Add(oPara)

                                '    oPara = New gloEMRDatabase.DBParameter
                                '    oPara.DataType = SqlDbType.BigInt
                                '    oPara.Direction = ParameterDirection.Input
                                '    oPara.Value = 2   ' '' Treatment
                                '    oPara.Name = "@Type"
                                '    .DBParametersCol.Add(oPara)

                                '    dtTreat = .GetDataTable("Lab_GetOrderTestDtl_DiagCPT")
                                '    '' labodtl_DiagCPTID, labodtl_Code, labodtl_Description, labodtl_Type
                                '    oPara = Nothing

                                '    For j = 0 To dtTreat.Rows.Count - 1
                                '        oOrderTest.Treatments.Add(dtTreat.Rows(j)("labodtl_DiagCPTID"), dtTreat.Rows(j)("labodtl_Code"), dtTreat.Rows(j)("labodtl_Description"))
                                '    Next

                                'End With

                                DV_Diagnosys = DSMain.Tables(4).DefaultView()
                                strFilter = ""
                                strFilter = "labodtl_TestID ='" & oOrderTest.TestID & "' AND TestName='" & oOrderTest.TestName.Replace("'", "''") & "' AND labodtl_Type='2'"

                                DV_Diagnosys.RowFilter = strFilter

                                ''nicdrevision added for icd10 functionality for emdeon screen
                                For Each drView As DataRowView In DV_Diagnosys
                                    oOrderTest.Treatments.Add(drView("labodtl_DiagCPTID"), drView("labodtl_Code"), drView("labodtl_Description"), drView("nIcdRevision"))
                                Next


                                '' //// <> Fill Order Test Result Object 
                                'Dim dtResult As New DataTable
                                'With _gloEMRDatabase
                                '    Dim oPara As gloEMRDatabase.DBParameter
                                '    .DBParametersCol.Clear()
                                '    oPara = New gloEMRDatabase.DBParameter
                                '    oPara.DataType = SqlDbType.BigInt
                                '    oPara.Direction = ParameterDirection.Input
                                '    oPara.Value = OrderID
                                '    oPara.Name = "@OrderID"
                                '    .DBParametersCol.Add(oPara)

                                '    oPara = New gloEMRDatabase.DBParameter
                                '    oPara.DataType = SqlDbType.VarChar
                                '    oPara.Direction = ParameterDirection.Input
                                '    oPara.Value = oOrderTest.TestName
                                '    oPara.Name = "@TestName"
                                '    .DBParametersCol.Add(oPara)

                                '    'Sanjog -Added on 2011 March 2 to show the prior and non prior result
                                '    oPara = New gloEMRDatabase.DBParameter
                                '    oPara.DataType = SqlDbType.Int
                                '    oPara.Direction = ParameterDirection.Input
                                '    oPara.Value = 1
                                '    oPara.Name = "@PriorResult"
                                '    .DBParametersCol.Add(oPara)
                                '    'Sanjog -Added on 2011 March 2 to show the prior and non prior result

                                '    dtResult = .GetDataTable("Lab_GetOrderTestResult")
                                '    'labotr_TestResultNumber, labotr_TestResultName, labotr_TestResultDateTime
                                '    oPara = Nothing
                                'End With

                                Dim DV_OrderTestResult As DataView
                                DV_OrderTestResult = DSMain.Tables(5).DefaultView()


                                strFilter = ""
                                'strFilter = "labotr_TestName='" & oOrderTest.TestName.Replace("'", "''") & "' AND labotr_TestResultDateTime='" & LastTransactionDate.ToString().Trim() & "'"
                                strFilter = " labotr_TestId ='" & oOrderTest.TestID & "'  AND  labotr_TestName='" & oOrderTest.TestName.Replace("'", "''") & "'"
                                DV_OrderTestResult.RowFilter = strFilter


                                With oOrderTest
                                    'Retrive from db
                                    Dim oTestResult As gloEMRActors.LabActor.OrderTestResult = Nothing

                                    For Each drTestResult As DataRowView In DV_OrderTestResult
                                        oTestResult = New gloEMRActors.LabActor.OrderTestResult
                                        With oTestResult
                                            .OrderID = OrderID
                                            .TestID = oOrderTest.TestID
                                            .TestName = oOrderTest.TestName
                                            .TestResultNumber = drTestResult("labotr_TestResultNumber")
                                            .TestResultName = drTestResult("labotr_TestResultName")
                                            .TestResultDateTime = drTestResult("labotr_TestResultDateTime")
                                            .IsFinished = drTestResult("labotr_IsFinished")
                                            .DMSID = drTestResult("labotr_DMSID")
                                            ' Added below two fileds as per qwest certification and same as updateing while save and close in view order form.
                                            'Added by madan-- on 20100409...
                                            Dim _blncheckDate As Boolean = IsDate(drTestResult("labotr_SpecimenReceivedDateTime"))
                                            If (_blncheckDate = True) Then
                                                .BlnSpecimenReceivedDateTime = True
                                                .SpecimenReceivedDateTime = drTestResult("labotr_SpecimenReceivedDateTime")
                                            Else
                                                .BlnSpecimenReceivedDateTime = False
                                            End If
                                            _blncheckDate = False
                                            _blncheckDate = IsDate(drTestResult("labotr_ResultTransferDateTime"))
                                            If (_blncheckDate = True) Then
                                                .BlnResultTransferDateTime = True
                                                .ResultTransferDateTime = drTestResult("labotr_ResultTransferDateTime")
                                            Else
                                                .BlnResultTransferDateTime = False
                                                .ResultTransferDateTime = Nothing
                                            End If

                                            .AlternateTestCode = drTestResult("labotr_AlternateTestCode")
                                            .AlternateTestName = drTestResult("labotr_AlternateTestName")
                                            .TestResultDateTimeUTC = Convert.ToInt32(drTestResult("labotr_TestResultDateTimeUTC"))
                                            .SpecimenReceivedDateTimeUTC = Convert.ToInt32(drTestResult("labotr_SpecimenReceivedDateTimeUTC"))
                                            .ResultTransferDateTimeUTC = Convert.ToInt32(drTestResult("labotr_ResultTransferDateTimeUTC"))


                                        End With


                                        '' //// <> Fill Order Test Result Details Object 
                                        'Dim dtResultDTL As New DataTable
                                        'With _gloEMRDatabase
                                        '    Dim oPara As gloEMRDatabase.DBParameter
                                        '    .DBParametersCol.Clear()
                                        '    oPara = New gloEMRDatabase.DBParameter
                                        '    oPara.DataType = SqlDbType.BigInt
                                        '    oPara.Direction = ParameterDirection.Input
                                        '    oPara.Value = OrderID
                                        '    oPara.Name = "@OrderID"
                                        '    .DBParametersCol.Add(oPara)

                                        '    oPara = New gloEMRDatabase.DBParameter
                                        '    oPara.DataType = SqlDbType.VarChar
                                        '    oPara.Direction = ParameterDirection.Input
                                        '    oPara.Value = oOrderTest.TestName
                                        '    oPara.Name = "@TestName"
                                        '    .DBParametersCol.Add(oPara)

                                        '    oPara = New gloEMRDatabase.DBParameter
                                        '    oPara.DataType = SqlDbType.BigInt
                                        '    oPara.Direction = ParameterDirection.Input
                                        '    oPara.Value = oTestResult.TestResultNumber
                                        '    oPara.Name = "@TestResultNumber"
                                        '    .DBParametersCol.Add(oPara)

                                        '    dtResultDTL = .GetDataTable("Lab_GetOrderTestResultDetails")

                                        '    oPara = Nothing
                                        'End With

                                        Dim DV_ResultDetails As DataView
                                        DV_ResultDetails = DSMain.Tables(6).DefaultView

                                        strFilter = ""
                                        strFilter = " labotrd_TestId ='" & oOrderTest.TestID & "'  AND  labotrd_TestName='" & oOrderTest.TestName.Replace("'", "''") & "' AND labotrd_TestResultNumber='" & oTestResult.TestResultNumber & "'"

                                        DV_ResultDetails.RowFilter = strFilter

                                        Dim oOrderTestResultDetail As gloEMRActors.LabActor.OrderTestResultDetail
                                        k = 0
                                        For Each drResultDetails As DataRowView In DV_ResultDetails
                                            oOrderTestResultDetail = New gloEMRActors.LabActor.OrderTestResultDetail

                                            With oOrderTestResultDetail
                                                .OrderID = OrderID
                                                .TestID = oOrderTest.TestID
                                                .TestName = oOrderTest.TestName
                                                .TestResultNumber = drResultDetails("labotrd_TestResultNumber")
                                                .ResultLineNo = drResultDetails("labotrd_ResultLineNo")
                                                .ResultNameID = drResultDetails("labotrd_ResultNameID")
                                                .ResultName = drResultDetails("labotrd_ResultName")
                                                .ResultValue = drResultDetails("labotrd_ResultValue")
                                                .ResultUnit = drResultDetails("labotrd_ResultUnit")
                                                .ResultRange = drResultDetails("labotrd_ResultRange")
                                                .ResultTypeCode = drResultDetails("labotrd_ResultType")
                                                .AbnormalFlagCode = drResultDetails("labotrd_AbnormalFlag")
                                                .ResultComment = drResultDetails("labotrd_ResultComment")
                                                .ResultWord = drResultDetails("labotrd_ResultWord")
                                                .ResultDMSID = drResultDetails("labotrd_ResultDMSID")
                                                .UserID = drResultDetails("labotrd_ResultUserID")
                                                If Not IsDBNull(drResultDetails("labotrd_ResultDateTime")) Then
                                                    .ResultDateTime = drResultDetails("labotrd_ResultDateTime")
                                                Else
                                                    .ResultDateTime = Nothing
                                                End If

                                                .IsFinished = drResultDetails("labotrd_IsFinished")
                                                .ResultLOINCID = drResultDetails("labotrd_LOINCID")
                                                'Added below two fileds as per qwest certification and same as updateing while save and close in view order form.
                                                'Added by madan-- on 20100409...
                                                .AlternateResultName = drResultDetails("labotrd_AlternateResultName")
                                                .AlternateResultCode = drResultDetails("labotrd_AlternateResultCode")
                                                .ProducerIdentifier = drResultDetails("labotrd_ProducerIdentifier")

                                                ''Added field by Abhijeet on 20101027
                                                .LabFacilityName = drResultDetails("labotrd_LabFacilityName")
                                                .LabFacilityStreetAddress = drResultDetails("labotrd_LabFacilityStreetAddress")
                                                .LabFacilityCity = drResultDetails("labotrd_LabFacilityCity")
                                                .LabFacilityState = drResultDetails("labotrd_LabFacilityState")
                                                .LabFacilityZipCode = drResultDetails("labotrd_LabFacilityZipCode")
                                                .PatientSpecificRange = drResultDetails("labotrd_specificResultRefRange")
                                                ''End of changes by Abhijeet on 20101027
                                                'Sanjog
                                                .TestSpecimenCollectionDate = drResultDetails("labotrd_testspecimencollectiondatetime")
                                                'Sanjog
                                                .LabFacilityIdentifierTypeCode = drResultDetails("labotrd_LabFacilityIdentifierTypeCode")
                                                .LabFacilityOrganizationIdentifier = drResultDetails("labotrd_LabFacilityOrganizationIdentifier")
                                                .LabFacilityCountry = drResultDetails("labotrd_LabFacilityCountry")
                                                .LabFacilityCountyOrParishCode = drResultDetails("labotrd_LabFacilityCountyOrParishCode")
                                                .ResultCode = drResultDetails("labotrd_ResultCode")
                                                .ResultCodeType = drResultDetails("labotrd_ResultCodeType")
                                                .LabFacilityMedicalDirectorIDNumber = drResultDetails("labotrd_LabFacilityMedicalDirectorIDNumber")
                                                .LabFacilityMedicalDirectorLastName = drResultDetails("labotrd_LabFacilityMedicalDirectorLastName")
                                                .LabFacilityMedicalDirectorMiddleName = drResultDetails("labotrd_LabFacilityMedicalDirectorMiddleName")
                                                .LabFacilityMedicalDirectorSuffix = drResultDetails("labotrd_LabFacilityMedicalDirectorSuffix")
                                                .LabFacilityMedicalDirectorPrefix = drResultDetails("labotrd_LabFacilityMedicalDirectorPrefix")
                                                .LabFacilityMedicalDirectorFirstName = drResultDetails("labotrd_LabFacilityMedicalDirectorFirstName")
                                                .ResultParentChildFlag = Convert.ToInt64(drResultDetails("labotrd_ResultParentChildFlag"))
                                                .ResultDateTimeUTC = Convert.ToInt32(drResultDetails("labotrd_ResultDateTimeUTC"))
                                                .TestSpecimenCollectionDateTimeUTC = Convert.ToInt32(drResultDetails("labotrd_TestSpecimenCollectionDateTimeUTC"))
                                            End With

                                            oTestResult.TestResultDetails.Add(oOrderTestResultDetail)
                                            oOrderTestResultDetail = Nothing
                                            k = k + 1
                                        Next
                                        .OrderTestResults.Add(oTestResult)
                                        oTestResult = Nothing
                                    Next

                                End With

                                .Add(oOrderTest)

                            Next
                        End With

                    End If
                    DSMain.Dispose()
                    DSMain = Nothing
                End If
                'dt = Nothing

                Return oLabOrder
            Catch ex As Exception
                Throw ex
                Return Nothing
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function
        ''Sanjog For LabOrder With Prior

        'end by madan on 20100612-- for Previous history 
        Public Function GetOrder(ByVal OrderID As Int64) As gloEMRActors.LabActor.LabOrder
            _gloEMRDatabase = New DataBaseLayer

            Dim oLabOrder As New gloEMRActors.LabActor.LabOrder
            Dim dt As DataTable
            Dim i, j, k As Integer

            Try

                '//// <> Order Master 
                '                dt = New DataTable
                With _gloEMRDatabase
                    Dim oPara As New gloEMRDatabase.DBParameter
                    .DBParametersCol.Clear()
                    oPara.DataType = SqlDbType.BigInt
                    oPara.Direction = ParameterDirection.Input
                    oPara.Value = OrderID
                    oPara.Name = "@OrderID"
                    .DBParametersCol.Add(oPara)
                    ' ''Fill Order Master 
                    dt = .GetDataTable("Lab_GetOrderMaster")
                    ' ''labom_OrderNoPrefix, labom_OrderNoID, labom_TransactionDate, labom_PatientID, labom_PatientAgeYear, labom_PatientAgeMonth, labom_PatientAgeDay,
                    ' ''labom_ProviderID, labom_PreferredLabID, labom_SampledByID, labom_ReferredByID, ProviderName, ContactName, SampledBy, RefferedBy
                    oPara = Nothing
                End With

                If Not dt Is Nothing Then
                    ' '' Fill Order Master Object  
                    With oLabOrder
                        .OrderID = OrderID
                        .OrderNoPrefix = dt.Rows(0)("labom_OrderNoPrefix")
                        .OrderNoID = dt.Rows(0)("labom_OrderNoID")
                        .TransactionDate = dt.Rows(0)("labom_TransactionDate")
                        .PatientID = dt.Rows(0)("labom_PatientID")
                        .PatientAge.Years = dt.Rows(0)("labom_PatientAgeYear")
                        .PatientAge.Months = dt.Rows(0)("labom_PatientAgeMonth")
                        .PatientAge.Days = dt.Rows(0)("labom_PatientAgeDay")
                        .ProviderID = dt.Rows(0)("labom_ProviderID")
                        .Provider = dt.Rows(0)("ProviderName")
                        .PreferredLab = dt.Rows(0)("ContactName")
                        .PreferredLabID = dt.Rows(0)("labom_PreferredLabID")
                        .SampledBy = dt.Rows(0)("SampledBy")
                        .SampledByID = dt.Rows(0)("labom_SampledByID")
                        .ReferredBy = dt.Rows(0)("RefferedBy")
                        .ReferredByID = dt.Rows(0)("labom_ReferredByID")
                        .ExternalCode = dt.Rows(0)("labom_ExternalCode")
                        .VisitID = dt.Rows(0)("labom_VisitID")
                        .DMSID = dt.Rows(0)("labom_DMSID")
                        .OrderStatusNumber = If(IsDBNull(dt.Rows(0)("OrderStatusNumber")) = True, 0, dt.Rows(0)("OrderStatusNumber"))
                        .blnOrderNotCPOE = If(IsDBNull(dt.Rows(0)("blnOrderNotCPOE")) = True, False, dt.Rows(0)("blnOrderNotCPOE"))
                        .bOutboundTransistion = If(IsDBNull(dt.Rows(0)("bOutboundTransistion")) = True, False, dt.Rows(0)("bOutboundTransistion"))

                        If (dt.Rows(0)("labom_LabComment").ToString() = "FASTING") Then
                            .FastingStatus = "FASTING"
                        Else
                            .FastingStatus = ""
                        End If


                        .SendTo = dt.Rows(0)("SendTo")
                        .ReferredTo = dt.Rows(0)("RefferedTo")
                        .ReferredToID = dt.Rows(0)("labom_ReferredToID")

                        Dim strQuery As String = ""
                        Dim dtTaskDtl As DataTable = Nothing


                        strQuery = "SELECT TM_TaskMST.nDueDate, TM_Task_Progress.sDescription FROM TM_TaskMST INNER JOIN TM_Task_Progress ON TM_TaskMST.nTaskID = TM_Task_Progress.nTaskID WHERE TM_TaskMST.nPatientID = " & .PatientID & " AND TM_TaskMST.nReferenceID1 = " & OrderID & " AND TM_TaskMST.nTaskType = 4"

                        dtTaskDtl = _gloEMRDatabase.GetDataTable_Query(strQuery)
                        If IsNothing(dtTaskDtl) = False Then
                            If dtTaskDtl.Rows.Count > 0 Then
                                If IsDBNull(dtTaskDtl.Rows(0)(0)) = False Then
                                    .TaskDueDate = gloDateMaster.gloDate.DateAsDate(dtTaskDtl.Rows(0)(0))
                                    '.TaskDueDate = dtTaskDtl.Rows(0)(0)
                                End If
                                If IsDBNull(dtTaskDtl.Rows(0)(1)) = False Then
                                    .TaskDescription = dtTaskDtl.Rows(0)(1)
                                End If

                                '' Added on 20090707 
                                '' To Handle Inconsistant Data between Tasks & Labs
                                '//// <> Fill User Details Object 
                                Dim dtUser As DataTable = Nothing
                                With _gloEMRDatabase
                                    Dim oPara As New gloEMRDatabase.DBParameter
                                    .DBParametersCol.Clear()

                                    oPara.DataType = SqlDbType.BigInt
                                    oPara.Direction = ParameterDirection.Input
                                    oPara.Value = OrderID
                                    oPara.Name = "@OrderID"
                                    .DBParametersCol.Add(oPara)
                                    dtUser = .GetDataTable("Lab_GetOrderUser")
                                    ' '' nUserID, sLoginName, UserName
                                    oPara = Nothing
                                End With

                                If Not dtUser Is Nothing Then
                                    '' Order Users Object 
                                    '' Fill UserID & LoginName
                                    With oLabOrder.Users
                                        For i = 0 To dtUser.Rows.Count - 1
                                            .Add(dtUser.Rows(i)("nUserID"), "", dtUser.Rows(i)("sLoginName") & "")
                                        Next
                                    End With
                                    dtUser.Dispose()
                                    dtUser = Nothing
                                End If
                                dtUser = Nothing
                                ''
                            End If
                            dtTaskDtl.Dispose()
                            dtTaskDtl = Nothing
                        End If
                        ''
                    End With
                    dt.Dispose()
                    dt = Nothing
                End If
                dt = Nothing

                '' Commented on 20090707 
                ''  code Shifted to above loop to Handle the inconsistant data between Labs & Tasks
                ''//// <> Fill User Details Object 
                'Dim dtUser As New DataTable
                'With _gloEMRDatabase
                '    Dim oPara As New gloEMRDatabase.DBParameter
                '    .DBParametersCol.Clear()

                '    oPara.DataType = SqlDbType.BigInt
                '    oPara.Direction = ParameterDirection.Input
                '    oPara.Value = OrderID
                '    oPara.Name = "@OrderID"
                '    .DBParametersCol.Add(oPara)
                '    dtUser = .GetDataTable("Lab_GetOrderUser")
                '    ' '' nUserID, sLoginName, UserName
                '    oPara = Nothing
                'End With

                'If Not dtUser Is Nothing Then
                '    '' Order Users Object 
                '    '' Fill UserID & LoginName
                '    With oLabOrder.Users
                '        For i = 0 To dtUser.Rows.Count - 1
                '            .Add(dtUser.Rows(i)("nUserID"), "", dtUser.Rows(i)("sLoginName") & "")
                '        Next
                '    End With
                'End If
                'dtUser = Nothing

                '//// <> Fill Order Test Details Object 
                '                dt = New DataTable
                With _gloEMRDatabase
                    Dim oPara As New gloEMRDatabase.DBParameter
                    .DBParametersCol.Clear()
                    oPara.DataType = SqlDbType.BigInt
                    oPara.Direction = ParameterDirection.Input
                    oPara.Value = OrderID
                    oPara.Name = "@OrderID"
                    .DBParametersCol.Add(oPara)
                    dt = .GetDataTable("Lab_GetOrderTestDtl")
                    ''labotd_TestID, labotd_LineNo, labotd_Note, labotd_LOINCCode, labotd_Instruction, labotd_Precaution, Lab_Order_TestDtl.labotd_DateTime, 
                    ''labotd_SpecimenID, labsm_Code, labsm_Name, labotd_CollectionID, labcm_Code, labcm_Name, labotd_StorageID, labstm_Code, labstm_Name , 
                    '' nUserID, sLoginName, UserName, labotd_Comment
                    oPara = Nothing
                End With

                If Not dt Is Nothing Then
                    '' Order Test Details Object 
                    With oLabOrder.OrderTests
                        Dim oOrderTest As gloEMRActors.LabActor.OrderTest
                        For i = 0 To dt.Rows.Count - 1
                            oOrderTest = New gloEMRActors.LabActor.OrderTest

                            oOrderTest.TestID = dt.Rows(i)("labotd_TestID")
                            oOrderTest.TestName = dt.Rows(i)("labotd_TestName")
                            oOrderTest.TestLineNo = dt.Rows(i)("labotd_LineNo")
                            oOrderTest.Note = dt.Rows(i)("labotd_Note") & ""
                            oOrderTest.LOINCCode = dt.Rows(i)("labotd_LOINCCode")
                            oOrderTest.CPT = dt.Rows(i)("sCPTCode")
                            oOrderTest.Instruction = dt.Rows(i)("labotd_Instruction") & ""
                            oOrderTest.Precaution = dt.Rows(i)("labotd_Precaution") & ""
                            oOrderTest.TestDateTime = dt.Rows(i)("labotd_DateTime")
                            oOrderTest.Is_Finished = dt.Rows(i)("labotd_IsFinished")

                            If IsDBNull(dt.Rows(i)("labotd_TestScheduledDateTime")) = False Then
                                oOrderTest.ScheduleDateTime = dt.Rows(i)("labotd_TestScheduledDateTime")
                            Else
                                oOrderTest.ScheduleDateTime = Nothing
                            End If




                            'Added by Mitesh
                            If IsDBNull(dt.Rows(i)("labotrd_TestSpecimenCollectionDateTime")) = False Then
                                oOrderTest.TestSpecimenCollectionDateTime = dt.Rows(i)("labotrd_TestSpecimenCollectionDateTime")
                            Else
                                oOrderTest.TestSpecimenCollectionDateTime = Nothing
                            End If

                            If IsDBNull(dt.Rows(i)("ReportedDateTime")) = False Then
                                oOrderTest.ReportedDateTime = dt.Rows(i)("ReportedDateTime")
                            Else
                                oOrderTest.ReportedDateTime = Nothing
                            End If
                            '-------------x---
                            oOrderTest.Specimen = dt.Rows(i)("SpecimenName") & ""
                            oOrderTest.Collection = dt.Rows(i)("CollectionName") & ""
                            oOrderTest.Storage = dt.Rows(i)("StorageTemperature") & ""
                            '    oOrderTest.SpecimenName = 
                            oOrderTest.UserID = dt.Rows(i)("nUserID")
                            oOrderTest.Comments = dt.Rows(i)("labotd_Comment") & ""
                            oOrderTest.DMSID = dt.Rows(i)("labotd_DMSID")
                            '//Added by madan on 20101007
                            oOrderTest.DicomID = dt.Rows(i)("labotd_DICOMID")

                            ''Added by Abhijeet on 20101027
                            oOrderTest.TestStatus = dt.Rows(i)("labotd_TestStatus")
                            oOrderTest.SpecimenSource = dt.Rows(i)("labotd_SpecimenSource")
                            oOrderTest.SpecimenConditionDisp = dt.Rows(i)("labotd_SpecimenConditionDisp")
                            oOrderTest.TestCode = dt.Rows(i)("labtm_Code")
                            oOrderTest.TestType = dt.Rows(i)("labotd_TestType")
                            'Added  By Yatin to Get Word Template Value from Lab Test Details Table::20130530
                            If IsDBNull(CType(dt.Rows(i)("labotd_Template"), Object)) = True Then
                                oOrderTest.TestTemplate = Nothing '' Template 
                            Else
                                oOrderTest.TestTemplate = CType(dt.Rows(i)("labotd_Template"), Object) '' Template 
                            End If

                            ''end of changes Added by Abhijeet on 20101027

                            'Start of Code Added by manoj jadhav on 20131024 for MU2:Incorporate Labsab Result
                            If IsDBNull(dt.Rows(i)("labotd_TestScheduledEndDateTime")) Then
                                oOrderTest.TestScheduledEndDateTime = Nothing
                            Else
                                oOrderTest.TestScheduledEndDateTime = dt.Rows(i)("labotd_TestScheduledEndDateTime")
                            End If
                            oOrderTest.SpecimenTypeIdentifier = Convert.ToString(dt.Rows(i)("labotd_SpecimenTypeIdentifier"))
                            oOrderTest.SpecimenTypeText = Convert.ToString(dt.Rows(i)("labotd_SpecimenTypeText"))
                            oOrderTest.SpecimenTypeCodingSystem = Convert.ToString(dt.Rows(i)("labotd_SpecimenTypeCodingSystem"))
                            If IsDBNull(dt.Rows(i)("labotd_SpecimenCollectionStartDateTime")) Then
                                oOrderTest.SpecimenCollectionStartDateTime = Nothing
                            Else
                                oOrderTest.SpecimenCollectionStartDateTime = dt.Rows(i)("labotd_SpecimenCollectionStartDateTime")
                            End If
                            oOrderTest.SpecimenRejectReason = Convert.ToString(dt.Rows(i)("labotd_SpecimenRejectReason"))
                            oOrderTest.SpecimenCondition = Convert.ToString(dt.Rows(i)("labotd_SpecimenCondition"))
                            oOrderTest.SpecimenActionCode = Convert.ToString(dt.Rows(i)("labotd_SpecimenActionCode"))
                            oOrderTest.labotd_DateTimeUTC = Convert.ToInt32(dt.Rows(i)("labotd_DateTimeUTC"))
                            oOrderTest.labotd_TestScheduledDateTimeUTC = Convert.ToInt32(dt.Rows(i)("labotd_TestScheduledDateTimeUTC"))
                            oOrderTest.labotd_TestScheduledEndDateTimeUTC = Convert.ToInt32(dt.Rows(i)("labotd_TestScheduledEndDateTimeUTC"))
                            oOrderTest.labotd_SpecimenCollectionStartDateTimeUTC = Convert.ToInt32(dt.Rows(i)("labotd_SpecimenCollectionStartDateTimeUTC"))
                            'End of Code Added by manoj jadhav on 20131024 for MU2:Incorporate Lab Result
                            oOrderTest.ConceptID = Convert.ToString(dt.Rows(i)("sConceptID"))
                            oOrderTest.ValueSetID = Convert.ToString(dt.Rows(i)("sValueSetOID"))
                            oOrderTest.ValueSetName = Convert.ToString(dt.Rows(i)("sValueSetName"))

                            Try
                                Dim dtpLab As DataTable = Nothing
                                Dim dspLab As DataSet = Nothing
                                With _gloEMRDatabase
                                    Dim oPara As gloEMRDatabase.DBParameter
                                    oPara = New gloEMRDatabase.DBParameter
                                    .DBParametersCol.Clear()
                                    oPara.DataType = SqlDbType.BigInt
                                    oPara.Direction = ParameterDirection.Input
                                    oPara.Value = OrderID
                                    oPara.Name = "@OrderID"
                                    .DBParametersCol.Add(oPara)

                                    oPara = New gloEMRDatabase.DBParameter
                                    oPara.DataType = SqlDbType.BigInt
                                    oPara.Direction = ParameterDirection.Input
                                    oPara.Value = oOrderTest.TestID    ' '' Diagnosis
                                    oPara.Name = "@TestID"
                                    .DBParametersCol.Add(oPara)



                                    ' dtDia = .GetDataTable("Lab_GetOrderTestDtl_DiagCPT")
                                    dspLab = .GetDataSet("Lab_GetOrderTestDtl_PreferredLab")
                                    dtpLab = dspLab.Tables(0)
                                    '' labodtl_DiagCPTID, labodtl_Code, labodtl_Description, labodtl_Type
                                    oPara = Nothing
                                    If (IsNothing(dtpLab) = False) Then
                                        oOrderTest.TestPreferredLab = Convert.ToString(dtpLab.Rows(0)("labodtl_Preferredlab"))
                                        oOrderTest.TestPreferredLabID = Convert.ToString(dtpLab.Rows(0)("labodtl_PreferredlabId"))

                                        dtpLab.Dispose()
                                        dtpLab = Nothing
                                    End If
                                End With
                            Catch ex As Exception

                            End Try


                            '//// <> Fill Order Test Details Diagnosis & Treatment Object 
                            Dim dtDia As DataTable = Nothing
                            Dim dsDia As DataSet = Nothing
                            With _gloEMRDatabase
                                Dim oPara As gloEMRDatabase.DBParameter
                                oPara = New gloEMRDatabase.DBParameter
                                .DBParametersCol.Clear()
                                oPara.DataType = SqlDbType.BigInt
                                oPara.Direction = ParameterDirection.Input
                                oPara.Value = OrderID
                                oPara.Name = "@OrderID"
                                .DBParametersCol.Add(oPara)

                                oPara = New gloEMRDatabase.DBParameter
                                oPara.DataType = SqlDbType.VarChar
                                oPara.Direction = ParameterDirection.Input
                                oPara.Value = oOrderTest.TestName
                                oPara.Name = "@TestName"
                                .DBParametersCol.Add(oPara)

                                oPara = New gloEMRDatabase.DBParameter
                                oPara.DataType = SqlDbType.BigInt
                                oPara.Direction = ParameterDirection.Input
                                oPara.Value = 1   ' '' Diagnosis
                                oPara.Name = "@Type"
                                .DBParametersCol.Add(oPara)


                                oPara = New gloEMRDatabase.DBParameter
                                oPara.DataType = SqlDbType.BigInt
                                oPara.Direction = ParameterDirection.Input
                                oPara.Value = oOrderTest.TestID    ' '' Diagnosis
                                oPara.Name = "@TestID"
                                .DBParametersCol.Add(oPara)



                                ' dtDia = .GetDataTable("Lab_GetOrderTestDtl_DiagCPT")
                                dsDia = .GetDataSet("Lab_GetOrderTestDtl_DiagCPT")
                                dtDia = dsDia.Tables(0)
                                '' labodtl_DiagCPTID, labodtl_Code, labodtl_Description, labodtl_Type
                                oPara = Nothing
                                If (IsNothing(dtDia) = False) Then
                                    ''Added by Mayuri-to maintain diagnosis codes count in dropdowncontrol
                                    oLabOrder.DiagnosisCount = dsDia.Tables(1).Rows.Count
                                    For j = 0 To dtDia.Rows.Count - 1
                                        oOrderTest.Diagonosis.Add(dtDia.Rows(j)("labodtl_DiagCPTID"), dtDia.Rows(j)("labodtl_Code"), dtDia.Rows(j)("labodtl_Description"), dtDia.Rows(j)("nICDRevision"))
                                    Next
                                    dtDia.Dispose()
                                    dtDia = Nothing
                                End If
                            End With

                            Dim dtTreat As DataTable = Nothing
                            Dim dsTreat As DataSet = Nothing
                            With _gloEMRDatabase
                                Dim oPara As gloEMRDatabase.DBParameter
                                oPara = New gloEMRDatabase.DBParameter
                                .DBParametersCol.Clear()
                                oPara.DataType = SqlDbType.BigInt
                                oPara.Direction = ParameterDirection.Input
                                oPara.Value = OrderID
                                oPara.Name = "@OrderID"
                                .DBParametersCol.Add(oPara)

                                oPara = New gloEMRDatabase.DBParameter
                                oPara.DataType = SqlDbType.VarChar
                                oPara.Direction = ParameterDirection.Input
                                oPara.Value = oOrderTest.TestName
                                oPara.Name = "@TestName"
                                .DBParametersCol.Add(oPara)

                                oPara = New gloEMRDatabase.DBParameter
                                oPara.DataType = SqlDbType.BigInt
                                oPara.Direction = ParameterDirection.Input
                                oPara.Value = 2   ' '' Treatment
                                oPara.Name = "@Type"
                                .DBParametersCol.Add(oPara)

                                oPara = New gloEMRDatabase.DBParameter
                                oPara.DataType = SqlDbType.BigInt
                                oPara.Direction = ParameterDirection.Input
                                oPara.Value = oOrderTest.TestID    ' '' Diagnosis
                                oPara.Name = "@TestID"
                                .DBParametersCol.Add(oPara)
                                dsTreat = .GetDataSet("Lab_GetOrderTestDtl_DiagCPT")
                                dtTreat = dsTreat.Tables(0)
                                '' labodtl_DiagCPTID, labodtl_Code, labodtl_Description, labodtl_Type
                                oPara = Nothing
                                If (IsNothing(dtTreat) = False) Then


                                    For j = 0 To dtTreat.Rows.Count - 1

                                        oOrderTest.Treatments.Add(dtTreat.Rows(j)("labodtl_DiagCPTID"), dtTreat.Rows(j)("labodtl_Code"), dtTreat.Rows(j)("labodtl_Description"), dtTreat.Rows(j)("nICDRevision"))
                                    Next
                                    dtTreat.Dispose()
                                    dtTreat = Nothing
                                End If

                            End With


                            '' //// <> Fill Order Test Result Object 
                            Dim dtResult As DataTable = Nothing
                            With _gloEMRDatabase
                                Dim oPara As gloEMRDatabase.DBParameter
                                .DBParametersCol.Clear()
                                oPara = New gloEMRDatabase.DBParameter
                                oPara.DataType = SqlDbType.BigInt
                                oPara.Direction = ParameterDirection.Input
                                oPara.Value = OrderID
                                oPara.Name = "@OrderID"
                                .DBParametersCol.Add(oPara)

                                oPara = New gloEMRDatabase.DBParameter
                                oPara.DataType = SqlDbType.VarChar
                                oPara.Direction = ParameterDirection.Input
                                oPara.Value = oOrderTest.TestName
                                oPara.Name = "@TestName"
                                .DBParametersCol.Add(oPara)

                                oPara = New gloEMRDatabase.DBParameter
                                oPara.DataType = SqlDbType.BigInt
                                oPara.Direction = ParameterDirection.Input
                                oPara.Value = oOrderTest.TestID
                                oPara.Name = "@TestID"
                                .DBParametersCol.Add(oPara)

                                'Sanjog -Added on 2011 March 2 to show the prior and non prior result
                                oPara = New gloEMRDatabase.DBParameter
                                oPara.DataType = SqlDbType.Int
                                oPara.Direction = ParameterDirection.Input
                                oPara.Value = 1
                                oPara.Name = "@PriorResult"
                                .DBParametersCol.Add(oPara)
                                'Sanjog -Added on 2011 March 2 to show the prior and non prior result

                                dtResult = .GetDataTable("Lab_GetOrderTestResult")
                                'labotr_TestResultNumber, labotr_TestResultName, labotr_TestResultDateTime
                                oPara = Nothing
                            End With
                            If (IsNothing(dtResult) = False) Then


                                With oOrderTest
                                    'Retrive from db
                                    Dim oTestResult As gloEMRActors.LabActor.OrderTestResult = Nothing

                                    For nResults As Int16 = 0 To dtResult.Rows.Count - 1
                                        oTestResult = New gloEMRActors.LabActor.OrderTestResult
                                        With oTestResult
                                            .OrderID = OrderID
                                            .TestID = oOrderTest.TestID
                                            .TestName = oOrderTest.TestName
                                            .TestResultNumber = dtResult.Rows(nResults)("labotr_TestResultNumber")
                                            .TestResultName = dtResult.Rows(nResults)("labotr_TestResultName")
                                            .TestResultDateTime = dtResult.Rows(nResults)("labotr_TestResultDateTime")
                                            .IsFinished = dtResult.Rows(nResults)("labotr_IsFinished")
                                            .DMSID = dtResult.Rows(nResults)("labotr_DMSID")
                                            ' Added below two fileds as per qwest certification and same as updateing while save and close in view order form.
                                            'Added by madan-- on 20100409...
                                            Dim _blncheckDate As Boolean = IsDate(dtResult.Rows(nResults)("labotr_SpecimenReceivedDateTime"))
                                            If (_blncheckDate = True) Then
                                                .BlnSpecimenReceivedDateTime = True
                                                .SpecimenReceivedDateTime = dtResult.Rows(nResults)("labotr_SpecimenReceivedDateTime")
                                            Else
                                                .BlnSpecimenReceivedDateTime = False
                                            End If
                                            _blncheckDate = False
                                            _blncheckDate = IsDate(dtResult.Rows(nResults)("labotr_ResultTransferDateTime"))
                                            If (_blncheckDate = True) Then
                                                .BlnResultTransferDateTime = True
                                                .ResultTransferDateTime = dtResult.Rows(nResults)("labotr_ResultTransferDateTime")
                                            Else
                                                .BlnResultTransferDateTime = False
                                                .ResultTransferDateTime = Nothing
                                            End If

                                            'If Not dtResult.Rows(nResults)("labotr_SpecimenReceivedDateTime") Is Nothing AndAlso dtResult.Rows(nResults)("labotr_SpecimenReceivedDateTime").ToString() = "" Then
                                            'If Not dtResult.Rows(nResults)("labotr_SpecimenReceivedDateTime") Is Nothing Then
                                            '    .BlnSpecimenReceivedDateTime = True
                                            'Else
                                            '    .SpecimenReceivedDateTime = dtResult.Rows(nResults)("labotr_SpecimenReceivedDateTime")
                                            'End If

                                            ''If dtResult.Rows(nResults)("labotr_ResultTransferDateTime") Is Nothing AndAlso dtResult.Rows(nResults)("labotr_ResultTransferDateTime").ToString() = "" Then
                                            'If Not dtResult.Rows(nResults)("labotr_ResultTransferDateTime") Is Nothing Then
                                            '    .BlnResultTransferDateTime = True
                                            'Else
                                            '    .ResultTransferDateTime = dtResult.Rows(nResults)("labotr_ResultTransferDateTime")
                                            'End If
                                            .AlternateTestCode = dtResult.Rows(nResults)("labotr_AlternateTestCode")
                                            .AlternateTestName = dtResult.Rows(nResults)("labotr_AlternateTestName")
                                            'Start of Code Added by manoj jadhav on 20131024 for MU2:Incorporate Lab Result
                                            .TestResultDateTimeUTC = Convert.ToInt32(dtResult.Rows(nResults)("TestResultDateTimeUTC"))
                                            .SpecimenReceivedDateTimeUTC = Convert.ToInt32(dtResult.Rows(nResults)("SpecimenReceivedDateTimeUTC"))
                                            .ResultTransferDateTimeUTC = Convert.ToInt32(dtResult.Rows(nResults)("ResultTransferDateTimeUTC"))
                                            'End of Code Added by manoj jadhav on 20131024 for MU2:Incorporate Lab Result
                                        End With


                                        '' //// <> Fill Order Test Result Details Object 
                                        Dim dtResultDTL As DataTable = Nothing
                                        With _gloEMRDatabase
                                            Dim oPara As gloEMRDatabase.DBParameter
                                            .DBParametersCol.Clear()
                                            oPara = New gloEMRDatabase.DBParameter
                                            oPara.DataType = SqlDbType.BigInt
                                            oPara.Direction = ParameterDirection.Input
                                            oPara.Value = OrderID
                                            oPara.Name = "@OrderID"
                                            .DBParametersCol.Add(oPara)

                                            oPara = New gloEMRDatabase.DBParameter
                                            oPara.DataType = SqlDbType.VarChar
                                            oPara.Direction = ParameterDirection.Input
                                            oPara.Value = oOrderTest.TestName
                                            oPara.Name = "@TestName"
                                            .DBParametersCol.Add(oPara)

                                            oPara = New gloEMRDatabase.DBParameter
                                            oPara.DataType = SqlDbType.BigInt
                                            oPara.Direction = ParameterDirection.Input
                                            oPara.Value = oOrderTest.TestID
                                            oPara.Name = "@TestID"
                                            .DBParametersCol.Add(oPara)

                                            oPara = New gloEMRDatabase.DBParameter
                                            oPara.DataType = SqlDbType.BigInt
                                            oPara.Direction = ParameterDirection.Input
                                            oPara.Value = oTestResult.TestResultNumber
                                            oPara.Name = "@TestResultNumber"
                                            .DBParametersCol.Add(oPara)

                                            dtResultDTL = .GetDataTable("Lab_GetOrderTestResultDetails")
                                            'labotrd_TestResultNumber, labotrd_ResultLineNo ,labotrd_ResultNameID , labotrd_ResultName,labotrd_ResultValue, labotrd_ResultUnit , labotrd_ResultRange,labotrd_ResultType ,
                                            'labotrd_ResultComment, labotrd_ResultWord, labotrd_ResultDMSID, labotrd_ResultUserID, labotrd_ResultDateTime()
                                            oPara = Nothing
                                        End With
                                        If (IsNothing(dtResultDTL) = False) Then


                                            Dim oOrderTestResultDetail As gloEMRActors.LabActor.OrderTestResultDetail
                                            For k = 0 To dtResultDTL.Rows.Count - 1
                                                oOrderTestResultDetail = New gloEMRActors.LabActor.OrderTestResultDetail

                                                With oOrderTestResultDetail
                                                    .OrderID = OrderID
                                                    .TestID = oOrderTest.TestID
                                                    .TestName = oOrderTest.TestName
                                                    .TestResultNumber = dtResultDTL.Rows(k)("labotrd_TestResultNumber")
                                                    .ResultLineNo = dtResultDTL.Rows(k)("labotrd_ResultLineNo")
                                                    .ResultNameID = dtResultDTL.Rows(k)("labotrd_ResultNameID")
                                                    .ResultName = dtResultDTL.Rows(k)("labotrd_ResultName")
                                                    .ResultValue = dtResultDTL.Rows(k)("labotrd_ResultValue")
                                                    .ResultUnit = dtResultDTL.Rows(k)("labotrd_ResultUnit")
                                                    .ResultRange = dtResultDTL.Rows(k)("labotrd_ResultRange")
                                                    .ResultTypeCode = dtResultDTL.Rows(k)("labotrd_ResultType")
                                                    .AbnormalFlagCode = dtResultDTL.Rows(k)("labotrd_AbnormalFlag")
                                                    .ResultComment = dtResultDTL.Rows(k)("labotrd_ResultComment")
                                                    .ResultWord = dtResultDTL.Rows(k)("labotrd_ResultWord")
                                                    .ResultDMSID = dtResultDTL.Rows(k)("labotrd_ResultDMSID")
                                                    .UserID = dtResultDTL.Rows(k)("labotrd_ResultUserID")
                                                    .ResultDateTime = dtResultDTL.Rows(k)("labotrd_ResultDateTime")
                                                    .IsFinished = dtResultDTL.Rows(k)("labotrd_IsFinished")
                                                    .ResultLOINCID = dtResultDTL.Rows(k)("labotrd_LOINCID")
                                                    'Added below two fileds as per qwest certification and same as updateing while save and close in view order form.
                                                    'Added by madan-- on 20100409...
                                                    .AlternateResultName = dtResultDTL.Rows(k)("labotrd_AlternateResultName")
                                                    .AlternateResultCode = dtResultDTL.Rows(k)("labotrd_AlternateResultCode")
                                                    .ProducerIdentifier = dtResultDTL.Rows(k)("labotrd_ProducerIdentifier")

                                                    ''Added field by Abhijeet on 20101027
                                                    .LabFacilityName = dtResultDTL.Rows(k)("labotrd_LabFacilityName")
                                                    .LabFacilityStreetAddress = dtResultDTL.Rows(k)("labotrd_LabFacilityStreetAddress")
                                                    .LabFacilityCity = dtResultDTL.Rows(k)("labotrd_LabFacilityCity")
                                                    .LabFacilityState = dtResultDTL.Rows(k)("labotrd_LabFacilityState")
                                                    .LabFacilityZipCode = dtResultDTL.Rows(k)("labotrd_LabFacilityZipCode")
                                                    .PatientSpecificRange = dtResultDTL.Rows(k)("labotrd_specificResultRefRange")
                                                    ''End of changes by Abhijeet on 20101027
                                                    'Sanjog
                                                    .TestSpecimenCollectionDate = dtResultDTL.Rows(k)("labotrd_testspecimencollectiondatetime")
                                                    'Sanjog
                                                    'Start of Code Added by manoj jadhav on 20131024 for MU2:Incorporate Lab Result
                                                    .LabFacilityIdentifierTypeCode = Convert.ToString(dtResultDTL.Rows(k)("LabFacilityIdentifierTypeCode"))
                                                    .LabFacilityOrganizationIdentifier = Convert.ToString(dtResultDTL.Rows(k)("LabFacilityOrganizationIdentifier"))
                                                    .LabFacilityCountry = Convert.ToString(dtResultDTL.Rows(k)("LabFacilityCountry"))
                                                    .LabFacilityCountyOrParishCode = Convert.ToString(dtResultDTL.Rows(k)("LabFacilityCountyOrParishCode"))
                                                    .ResultCode = Convert.ToString(dtResultDTL.Rows(k)("ResultCode"))
                                                    .ResultCodeType = Convert.ToString(dtResultDTL.Rows(k)("ResultCodeType"))
                                                    .LabFacilityMedicalDirectorIDNumber = Convert.ToString(dtResultDTL.Rows(k)("LabFacilityMedicalDirectorIDNumber"))
                                                    .LabFacilityMedicalDirectorLastName = Convert.ToString(dtResultDTL.Rows(k)("LabFacilityMedicalDirectorLastName"))
                                                    .LabFacilityMedicalDirectorMiddleName = Convert.ToString(dtResultDTL.Rows(k)("LabFacilityMedicalDirectorMiddleName"))
                                                    .LabFacilityMedicalDirectorSuffix = Convert.ToString(dtResultDTL.Rows(k)("LabFacilityMedicalDirectorSuffix"))
                                                    .LabFacilityMedicalDirectorPrefix = Convert.ToString(dtResultDTL.Rows(k)("LabFacilityMedicalDirectorPrefix"))
                                                    .LabFacilityMedicalDirectorFirstName = Convert.ToString(dtResultDTL.Rows(k)("LabFacilityMedicalDirectorFirstName"))
                                                    .ResultParentChildFlag = Convert.ToInt64(dtResultDTL.Rows(k)("ResultParentChildFlag"))
                                                    .ResultDateTimeUTC = Convert.ToInt32(dtResultDTL.Rows(k)("ResultDateTimeUTC"))
                                                    .TestSpecimenCollectionDateTimeUTC = Convert.ToInt32(dtResultDTL.Rows(k)("TestSpecimenCollectionDateTimeUTC"))
                                                    .LabResultConceptID = Convert.ToString(dtResultDTL.Rows(k)("sConceptID"))
                                                    .LabResultICD9 = Convert.ToString(dtResultDTL.Rows(k)("sICD9"))
                                                    .LabResultICD10 = Convert.ToString(dtResultDTL.Rows(k)("sICD10"))
                                                    .LabResultLOINC = Convert.ToString(dtResultDTL.Rows(k)("sLOINC"))



                                                    'end of Code Added by manoj jadhav on 20131024 for MU2:Incorporate Lab Result
                                                End With

                                                oTestResult.TestResultDetails.Add(oOrderTestResultDetail)
                                                oOrderTestResultDetail = Nothing
                                            Next
                                            dtResultDTL.Dispose()
                                            dtResultDTL = Nothing
                                        End If
                                        .OrderTestResults.Add(oTestResult)
                                        oTestResult = Nothing
                                    Next

                                End With
                                dtResult.Dispose()
                                dtResult = Nothing
                            End If

                            .Add(oOrderTest)

                        Next
                    End With
                    dt.Dispose()
                    dt = Nothing
                End If
                dt = Nothing


                'dt = New DataTable
                'With _gloEMRDatabase
                '    Dim oPara As New gloEMRDatabase.DBParameter
                '    oPara.DataType = SqlDbType.BigInt
                '    oPara.Direction = ParameterDirection.Input
                '    oPara.Value = OrderID
                '    oPara.Name = "@OrderID"
                '    .DBParametersCol.Add(oPara)
                '    dt = .GetDataTable("Lab_GetOrderTestDtl")
                '    ''labotd_TestID, labotd_LineNo, labotd_Note, labotd_LOINCCode, labotd_Instruction, labotd_Precaution, Lab_Order_TestDtl.labotd_DateTime, 
                '    ''labotd_SpecimenID, labsm_Code, labsm_Name, labotd_CollectionID, labcm_Code, labcm_Name, labotd_StorageID, labstm_Code, labstm_Name , nUserID, sLoginName, UserName
                'End With

                'If Not dt Is Nothing Then
                '    '' Order Ttest Details Object 
                '    With oLabOrder.OrderTests
                '        Dim oOrderTest As gloEMRActors.LabActor.OrderTest
                '        For i = 0 To dt.Rows.Count - 1
                '            oOrderTest = New gloEMRActors.LabActor.OrderTest

                '            oOrderTest.TestID = dt.Rows(i)("labotd_TestID")
                '            oOrderTest.TestLineNo = dt.Rows(i)("labotd_LineNo")
                '            oOrderTest.Note = dt.Rows(i)("labotd_Note") & ""
                '            oOrderTest.LOINCCode = dt.Rows(i)("labotd_LOINCCode")
                '            oOrderTest.Instruction = dt.Rows(i)("labotd_Instruction") & ""
                '            oOrderTest.Precaution = dt.Rows(i)("labotd_Precaution") & ""
                '            oOrderTest.TestDateTime = dt.Rows(i)("labotd_DateTime")
                '            oOrderTest.Specimen = dt.Rows(i)("labsm_Name") & ""
                '            oOrderTest.Collection = dt.Rows(i)("labcm_Name") & ""
                '            oOrderTest.Storage = dt.Rows(i)("labstm_Name") & ""
                '            oOrderTest.UserID = dt.Rows(i)("nUserID")
                '            .Add(oOrderTest)
                '        Next
                '    End With
                'End If
                'dt = Nothing


                Return oLabOrder
            Catch ex As Exception
                Throw ex
                Return Nothing
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function

        ''' <summary>
        ''' Fill Outstanding Orders For Selected Date And Provider
        ''' </summary>
        ''' <param name="dtFromDate">Order From Date</param>
        ''' <param name="dtToDate">Order To Date</param>
        ''' <returns>Call From frmOutstandingOrders Form</returns>
        ''' <remarks>Add By Pramod on 22012008 For CCHIT-2007</remarks>
        Public Function GetOutStandingOrder(ByVal dtFromDate As DateTime, ByVal dtToDate As DateTime, ByVal ProviderID As Int64, Optional ByVal OrderStatus As Int64 = 0) As DataTable
            Dim dt As DataTable
            _gloEMRDatabase = New DataBaseLayer

            Try


                With _gloEMRDatabase
                    Dim oPara As New gloEMRDatabase.DBParameter
                    oPara = New gloEMRDatabase.DBParameter
                    oPara.DataType = SqlDbType.DateTime
                    oPara.Direction = ParameterDirection.Input
                    oPara.Value = dtFromDate
                    oPara.Name = "@FromDate"
                    .DBParametersCol.Add(oPara)

                    oPara = New gloEMRDatabase.DBParameter
                    oPara.DataType = SqlDbType.DateTime
                    oPara.Direction = ParameterDirection.Input
                    oPara.Value = dtToDate
                    oPara.Name = "@ToDate"
                    .DBParametersCol.Add(oPara)

                    oPara = New gloEMRDatabase.DBParameter
                    oPara.DataType = SqlDbType.BigInt
                    oPara.Direction = ParameterDirection.Input
                    oPara.Value = ProviderID
                    oPara.Name = "@nProviderID "
                    .DBParametersCol.Add(oPara)

                    oPara = New gloEMRDatabase.DBParameter
                    oPara.DataType = SqlDbType.BigInt
                    oPara.Direction = ParameterDirection.Input
                    oPara.Value = OrderStatus
                    oPara.Name = "@intOrderStatusNumber "
                    .DBParametersCol.Add(oPara)

                    dt = .GetDataTable("gsp_GetLabResult")


                    oPara = Nothing
                End With

                'For i As Integer = dt.Rows.Count - 1 To 0 Step -1
                '    _gloEMRDatabase = New DataBaseLayer

                '    Dim strquery As String

                '    strquery = _gloEMRDatabase.GetRecord_Query("select * FROM Lab_Acknowledgment Where nOrderNumberPrefix = '" & dt.Rows(i)("labom_OrderNoPrefix") & "' and nOrderNumberID = " & dt.Rows(i)("labom_OrderNoID") & " ")
                '    If strquery <> "" Then
                '        dt.Rows.Remove(dt.Rows(i))
                '    End If
                'Next

            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
            Return dt
        End Function
        Public Function CheckAcknoledgement(ByVal OrederNumberPrifix As String, ByVal OrderNumberID As Long) As Boolean
            '_gloEMRDatabase = New DataBaseLayer
            ''Dim dt As New DataTable 
            'Dim strquery As String
            'strquery = _gloEMRDatabase.GetRecord_Query("select * FROM Lab_Acknowledgment Where nOrderNumberPrefix = '" & OrederNumberPrifix & "' and nOrderNumberID = " & OrderNumberID & " ")
            'If strquery = "" Then
            '    Return False
            'Else
            '    Return True
            'End If
            Return Nothing
        End Function
        Private Sub Save_LabOrderUsers(ByVal LabOrderID As Int64)
            'insert the multiple users against the above order using the _LabOrderID
            _gloEMRDatabase = New DataBaseLayer
            Dim objDBParameter As DBParameter

            Try


                For i As Integer = 0 To _LabOrder.Users.Count - 1

                    _gloEMRDatabase.DBParametersCol.Clear()

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.BigInt
                    objDBParameter.Value = LabOrderID
                    objDBParameter.Name = "@laboud_OrderID"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.BigInt
                    'Labs Denormalization
                    'objDBParameter.Value = GetUserID(_LabOrder.Users.Item(i).Description)
                    objDBParameter.Value = _LabOrder.Users.Item(i).ID
                    '------------
                    objDBParameter.Name = "@laboud_UserID"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                    '\\ Lab Denormalization 20090318
                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.VarChar
                    objDBParameter.Value = _LabOrder.Users.Item(i).Description
                    objDBParameter.Name = "@laboud_UserName"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    _gloEMRDatabase.Add("Lab_InsertOrderUser")
                Next
            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Sub

        'Added  By Yatin to create file name for word template::20130530
        Public ReadOnly Property ExamNewDocumentName() As String
            Get
                'Dim _Path As String = gloSettings.FolderSettings.AppTempFolderPath
                'Dim _NewDocumentName As String = ""
                'Dim _Extension As String = ".docx"
                'Dim _dtCurrentDateTime As DateTime = System.DateTime.Now
                'Dim i As Integer = 0
                '_NewDocumentName = (Strings.Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss tt") & " ") + _dtCurrentDateTime.Millisecond.ToString() & _Extension
                'While File.Exists(_Path & "\" & _NewDocumentName) = True And i < Integer.MaxValue
                '    i = i + 1
                '    _NewDocumentName = (Strings.Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss tt") & " ") + _dtCurrentDateTime.Millisecond & "-" & i & _Extension
                'End While
                'Return _Path & "\" & _NewDocumentName
                Return gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".docx", "MMddyyyyHHmmssffff")
            End Get
        End Property
  

        Private Sub Save_LabOrderDetails(ByVal LabOrderID As Long, ByVal IsFinished As Int16)
            _gloEMRDatabase = New DataBaseLayer
            Dim objDBParameter As DBParameter

            ' ''insert the multiple tests against the above order using the _LabOrderID
            Try


                For i As Integer = 0 To _LabOrder.OrderTests.Count - 1

                    ' '' Fill OrderTest Details 
                    _gloEMRDatabase.DBParametersCol.Clear()

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.BigInt
                    objDBParameter.Value = LabOrderID
                    objDBParameter.Name = "@labotd_OrderID"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.BigInt
                    objDBParameter.Value = _LabOrder.OrderTests.Item(i).TestID
                    objDBParameter.Name = "@labotd_TestID"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.Int
                    objDBParameter.Value = _LabOrder.OrderTests.Item(i).TestLineNo ' // check it
                    objDBParameter.Name = "@labotd_LineNo"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.VarChar
                    objDBParameter.Value = _LabOrder.OrderTests.Item(i).Note & ""
                    objDBParameter.Name = "@labotd_Note"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.BigInt
                    If Not _LabOrder.OrderTests.Item(i).SpecimenName Is Nothing Then
                        objDBParameter.Value = GetSpecimanID(_LabOrder.OrderTests.Item(i).SpecimenName)
                    Else
                        objDBParameter.Value = 0
                    End If

                    objDBParameter.Name = "@labotd_SpecimenID"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.BigInt
                    If Not _LabOrder.OrderTests.Item(i).CollectionName Is Nothing Then
                        objDBParameter.Value = GetSpecimanID(_LabOrder.OrderTests.Item(i).CollectionName)
                    Else
                        objDBParameter.Value = 0
                    End If

                    objDBParameter.Name = "@labotd_CollectionID"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)


                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.BigInt
                    If Not _LabOrder.OrderTests.Item(i).StorageName Is Nothing Then
                        objDBParameter.Value = GetSpecimanID(_LabOrder.OrderTests.Item(i).StorageName)
                    Else
                        objDBParameter.Value = 0
                    End If
                    objDBParameter.Name = "@labotd_StorageID"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    '' commented Sandip Darade 20090403
                    'objDBParameter = New DBParameter
                    'objDBParameter.Direction = ParameterDirection.Input
                    'objDBParameter.DataType = SqlDbType.BigInt
                    'objDBParameter.Value = _LabOrder.OrderTests.Item(i).LOINCCode
                    'objDBParameter.Name = "@labotd_LOINCCode"
                    '_gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                    ''Sandip Darade 20090403
                    ''LoinicCode as string 
                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.VarChar
                    objDBParameter.Value = _LabOrder.OrderTests.Item(i).LOINCCode
                    objDBParameter.Name = "@labotd_LOINCCode"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)



                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.VarChar
                    objDBParameter.Value = _LabOrder.OrderTests.Item(i).Instruction & ""
                    objDBParameter.Name = "@labotd_Instruction"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)


                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.VarChar
                    objDBParameter.Value = _LabOrder.OrderTests.Item(i).Precaution & ""
                    objDBParameter.Name = "@labotd_Precaution"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.Bit
                    objDBParameter.Value = _LabOrder.OrderTests.Item(i).Is_Finished

                    'If (_LabOrder.OrderTests.Item(i).IsWd_Finished) = True Then
                    '    objDBParameter.Value = "Yes"
                    'Else
                    '    objDBParameter.Value = "No"
                    'End If
                    objDBParameter.Name = "@labotd_IsFinished"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.DateTime
                    objDBParameter.Value = _LabOrder.TransactionDate '// Check It
                    objDBParameter.Name = "@labotd_DateTime"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.BigInt
                    objDBParameter.Value = _LabOrder.OrderTests.Item(i).UserID
                    objDBParameter.Name = "@labotd_UserID"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    '' 20070602
                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.VarChar
                    objDBParameter.Size = 5000
                    objDBParameter.Value = _LabOrder.OrderTests.Item(i).Comments & ""
                    objDBParameter.Name = "@labotd_Comment"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)



                    '\\ Lab Denormalization 20090318
                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.VarChar
                    objDBParameter.Value = _LabOrder.OrderTests.Item(i).TestName  'testname
                    objDBParameter.Name = "@labotd_TestName"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.VarChar
                    objDBParameter.Value = _LabOrder.OrderTests.Item(i).SpecimenName
                    objDBParameter.Name = "@labotd_SpecimenName"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.VarChar
                    objDBParameter.Value = _LabOrder.OrderTests.Item(i).CollectionName
                    objDBParameter.Name = "@labotd_CollectionName"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.VarChar
                    objDBParameter.Value = _LabOrder.OrderTests.Item(i).StorageName
                    objDBParameter.Name = "@labotd_StorageName"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    '////
                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.BigInt
                    objDBParameter.Value = _LabOrder.OrderTests.Item(i).DMSID
                    objDBParameter.Name = "@labotd_DMSID"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    '///DMSID Collection IDs
                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.VarChar
                    objDBParameter.Value = _LabOrder.OrderTests.Item(i).DMSIDCollection
                    objDBParameter.Name = "@labotd_DMScollection"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    ''Added by madan on 20101007
                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.BigInt
                    objDBParameter.Value = _LabOrder.OrderTests.Item(i).DicomID
                    objDBParameter.Name = "@labotd_DICOMID"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                    'End madan changes.

                    ''Added by Abhijeet on 20101027
                    '//TestStatus
                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.VarChar
                    objDBParameter.Value = _LabOrder.OrderTests.Item(i).TestStatus
                    objDBParameter.Name = "@labotd_TestStatus"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                    '//labotd_SpecimenSource
                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.VarChar
                    objDBParameter.Value = _LabOrder.OrderTests.Item(i).SpecimenSource
                    objDBParameter.Name = "@labotd_SpecimenSource"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                    '//labotd_SpecimenConditionDisp
                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.VarChar
                    objDBParameter.Value = _LabOrder.OrderTests.Item(i).SpecimenConditionDisp
                    objDBParameter.Name = "@labotd_SpecimenConditionDisp"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                    '//TestType
                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.VarChar
                    objDBParameter.Value = _LabOrder.OrderTests.Item(i).TestType
                    objDBParameter.Name = "@labotd_TestType"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                    ''End of changed by Abhijeet on 20101027

                    'Added  By Yatin to save Word Template in Lab Test Details Table::20130530
                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.Image
                    If IsNothing(_LabOrder.OrderTests.Item(i).TestTemplate) = False Then
                        'SLR: Assigned directly on 12/2/2014
                        objDBParameter.Value = CType(_LabOrder.OrderTests.Item(i).TestTemplate, Byte()).Clone()
                        'Dim mstream As ADODB.Stream
                        'mstream = New ADODB.Stream
                        'mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
                        'mstream.Open()
                        'mstream.Write(_LabOrder.OrderTests.Item(i).TestTemplate)   '' Template (Object)
                        'Dim strFileName As String
                        'strFileName = ExamNewDocumentName
                        'mstream.SaveToFile(strFileName, ADODB.SaveOptionsEnum.adSaveCreateOverWrite)
                        'objDBParameter.Value = mstream.Read
                        'mstream.Close()
                        'mstream = Nothing
                    Else
                        objDBParameter.Value = DBNull.Value
                    End If


                    'objDBParameter.Value = _LabOrder.OrderTests.Item(i).TestTemplate
                    objDBParameter.Name = "@labotd_Template"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.VarChar
                    objDBParameter.Value = _LabOrder.OrderTests.Item(i).CPT
                    objDBParameter.Name = "@labotd_CPT"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.DateTime
                    If _LabOrder.OrderTests.Item(i).ScheduleDateTime = Nothing OrElse _LabOrder.OrderTests.Item(i).ScheduleDateTime = "1/1/0001 12:00:00 AM" OrElse _LabOrder.OrderTests.Item(i).ScheduleDateTime = "12:00:00 AM" Then
                        objDBParameter.Value = Nothing
                    Else
                        objDBParameter.Value = _LabOrder.OrderTests.Item(i).ScheduleDateTime
                    End If

                    objDBParameter.Name = "@labotd_TestScheduledDateTime"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.VarChar
                    objDBParameter.Value = _LabOrder.OrderTests.Item(i).SpecimenTypeIdentifier
                    objDBParameter.Name = "@labotd_SpecimenTypeIdentifier"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.VarChar
                    objDBParameter.Value = _LabOrder.OrderTests.Item(i).SpecimenTypeText
                    objDBParameter.Name = "@labotd_SpecimenTypeText"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.VarChar
                    objDBParameter.Value = _LabOrder.OrderTests.Item(i).SpecimenTypeCodingSystem
                    objDBParameter.Name = "@labotd_SpecimenTypeCodingSystem"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.DateTime
                    objDBParameter.Name = "@labotd_SpecimenCollectionStartDateTime"
                    If _LabOrder.OrderTests.Item(i).SpecimenCollectionStartDateTime = Nothing OrElse _LabOrder.OrderTests.Item(i).SpecimenCollectionStartDateTime = "1/1/0001 12:00:00 AM" OrElse _LabOrder.OrderTests.Item(i).SpecimenCollectionStartDateTime = "12:00:00 AM" Then
                        objDBParameter.Value = Nothing
                    Else
                        objDBParameter.Value = _LabOrder.OrderTests.Item(i).SpecimenCollectionStartDateTime
                    End If
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.VarChar
                    objDBParameter.Value = _LabOrder.OrderTests.Item(i).SpecimenRejectReason
                    objDBParameter.Name = "@labotd_SpecimenRejectReason"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.VarChar
                    objDBParameter.Value = _LabOrder.OrderTests.Item(i).SpecimenCondition
                    objDBParameter.Name = "@labotd_SpecimenCondition"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.VarChar
                    objDBParameter.Value = _LabOrder.OrderTests.Item(i).SpecimenActionCode
                    objDBParameter.Name = "@labotd_SpecimenActionCode"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.DateTime

                    objDBParameter.Name = "@labotd_TestScheduledEndDateTime"
                    If _LabOrder.OrderTests.Item(i).TestScheduledEndDateTime = Nothing OrElse _LabOrder.OrderTests.Item(i).TestScheduledEndDateTime = "1/1/0001 12:00:00 AM" OrElse _LabOrder.OrderTests.Item(i).TestScheduledEndDateTime = "12:00:00 AM" Then
                        objDBParameter.Value = Nothing
                    Else
                        objDBParameter.Value = _LabOrder.OrderTests.Item(i).TestScheduledEndDateTime
                    End If

                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.Int
                    objDBParameter.Value = _LabOrder.OrderTests.Item(i).labotd_DateTimeUTC
                    objDBParameter.Name = "@labotd_DateTimeUTC"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.Int
                    objDBParameter.Value = _LabOrder.OrderTests.Item(i).labotd_TestScheduledDateTimeUTC
                    objDBParameter.Name = "@labotd_TestScheduledDateTimeUTC"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.Int
                    objDBParameter.Value = _LabOrder.OrderTests.Item(i).labotd_TestScheduledEndDateTimeUTC
                    objDBParameter.Name = "@labotd_TestScheduledEndDateTimeUTC"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.Int
                    objDBParameter.Value = _LabOrder.OrderTests.Item(i).labotd_SpecimenCollectionStartDateTimeUTC
                    objDBParameter.Name = "@labotd_SpecimenCollectionStartDateTimeUTC"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)


                   

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.VarChar
                    objDBParameter.Value = _LabOrder.OrderTests.Item(i).ValueSetID
                    objDBParameter.Name = "@labotd_Valuset"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.VarChar
                    objDBParameter.Value = _LabOrder.OrderTests.Item(i).ValueSetName
                    objDBParameter.Name = "@labotd_valuesetname"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.VarChar
                    objDBParameter.Value = _LabOrder.OrderTests.Item(i).ConceptID
                    objDBParameter.Name = "@labotd_ConceptID"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)


                    objDBParameter = New DBParameter
                    objDBParameter.Direction = ParameterDirection.Input
                    objDBParameter.DataType = SqlDbType.VarChar
                    objDBParameter.Value = _LabOrder.OrderTests.Item(i).ConceptIDDescription
                    objDBParameter.Name = "@labotd_DescriptionID"
                    _gloEMRDatabase.DBParametersCol.Add(objDBParameter)




                    'call the stored procedure to Insert OderTest Details
                    _gloEMRDatabase.Add("Lab_InsertOrderTestDtl")
                    Dim _LabOrderTests As gloEMRActors.LabActor.OrderTest
                    _LabOrderTests = New gloEMRActors.LabActor.OrderTest
                    If _LabOrder.OrderTests.Item(i).DMSIDCollection <> "" Then
                        _LabOrderTests.INUP_Test_Attachment(_LabOrder.PatientID, LabOrderID, _LabOrder.OrderTests.Item(i).TestID, _LabOrder.OrderTests.Item(i).DMSIDCollection)
                    End If
                    If Not IsNothing(_LabOrderTests) Then
                        _LabOrderTests.Dispose()
                        _LabOrderTests = Nothing
                    End If

                    '//Preferred Lab for the test

                    Try
                        If _LabOrder.OrderTests.Item(i).TestPreferredLab <> "" AndAlso _LabOrder.OrderTests.Item(i).TestPreferredLabID <> 0 Then
                            _gloEMRDatabase.DBParametersCol.Clear()
                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.BigInt
                            objDBParameter.Value = LabOrderID
                            objDBParameter.Name = "@labodtl_OrderID"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.BigInt
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).TestID
                            objDBParameter.Name = "@labodtl_TestID"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.BigInt
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).TestPreferredLabID  '// we are not taking Diagnosis & CPT Master ID
                            objDBParameter.Name = "@labodtl_PreferredLabId"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).TestPreferredLab
                            objDBParameter.Name = "@labodtl_PreferredLab"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            _gloEMRDatabase.Add("Lab_InsertOrderTestDtl_PreferredLab")
                        End If
                        
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
                    End Try
                    '//

                    '//Dignosis against Test
                    For j As Integer = 0 To _LabOrder.OrderTests.Item(i).Diagonosis.Count - 1
                        _gloEMRDatabase.DBParametersCol.Clear()

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.BigInt
                        objDBParameter.Value = LabOrderID
                        objDBParameter.Name = "@labodtl_OrderID"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.BigInt
                        objDBParameter.Value = _LabOrder.OrderTests.Item(i).TestID
                        objDBParameter.Name = "@labodtl_TestID"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.BigInt
                        objDBParameter.Value = _LabOrder.OrderTests.Item(i).Diagonosis.Item(j).ID  '// we are not taking Diagnosis & CPT Master ID
                        objDBParameter.Name = "@labodtl_DiagCPTID"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.VarChar
                        objDBParameter.Value = _LabOrder.OrderTests.Item(i).Diagonosis.Item(j).Code & ""
                        objDBParameter.Name = "@labodtl_Code"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.VarChar
                        objDBParameter.Value = _LabOrder.OrderTests.Item(i).Diagonosis.Item(j).Description & ""
                        objDBParameter.Name = "@labodtl_Description"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                        ''nicdrevision added for icd10 functionality for emdeon screen
                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.Int
                        objDBParameter.Value = _LabOrder.OrderTests.Item(i).Diagonosis.Item(j).IcdRevision  '1 for Diagonosis and 2 for Treatment
                        objDBParameter.Name = "@nIcdRevision"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)




                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.Int
                        objDBParameter.Value = 1  '1 for Diagonosis and 2 for Treatment
                        objDBParameter.Name = "@labodtl_Type"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                        '\\Lab Denormalization
                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.VarChar
                        objDBParameter.Value = _LabOrder.OrderTests.Item(i).TestName
                        objDBParameter.Name = "@labodtl_TestName"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                        _gloEMRDatabase.Add("Lab_InsertOrderDiagonosis")

                    Next


                    '//Treatments against Test
                    For j As Integer = 0 To _LabOrder.OrderTests.Item(i).Treatments.Count - 1
                        _gloEMRDatabase.DBParametersCol.Clear()

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.BigInt
                        objDBParameter.Value = LabOrderID
                        objDBParameter.Name = "@labodtl_OrderID"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.BigInt
                        objDBParameter.Value = _LabOrder.OrderTests.Item(i).TestID
                        objDBParameter.Name = "@labodtl_TestID"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.BigInt
                        objDBParameter.Value = _LabOrder.OrderTests.Item(i).Treatments.Item(j).ID  '// we are not taking Diagnosis & CPT Master ID
                        objDBParameter.Name = "@labodtl_DiagCPTID"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.VarChar
                        objDBParameter.Value = _LabOrder.OrderTests.Item(i).Treatments.Item(j).Code & ""
                        objDBParameter.Name = "@labodtl_Code"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.VarChar
                        objDBParameter.Value = _LabOrder.OrderTests.Item(i).Treatments.Item(j).Description & ""
                        objDBParameter.Name = "@labodtl_Description"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                        ''nicdrevision added for icd10 functionality for emdeon screen
                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.Int
                        objDBParameter.Value = _LabOrder.OrderTests.Item(i).Treatments.Item(j).IcdRevision  '1 for Diagonosis and 2 for Treatment
                        objDBParameter.Name = "@nIcdRevision"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)


                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.Int
                        objDBParameter.Value = 2  '1 for Diagonosis and 2 for Treatment
                        objDBParameter.Name = "@labodtl_Type"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                        '\\Lab Denormalization
                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.VarChar
                        objDBParameter.Value = _LabOrder.OrderTests.Item(i).TestName
                        objDBParameter.Name = "@labodtl_TestName"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                        _gloEMRDatabase.Add("Lab_InsertOrderDiagonosis")

                    Next
                    '_LabOrder.OrderTests.Item(i).OrderTestResults 


                    For j As Integer = 0 To _LabOrder.OrderTests.Item(i).OrderTestResults.Count - 1
                        'insert record in the master table Lab_Order_Test_Result
                        _gloEMRDatabase.DBParametersCol.Clear()

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.BigInt
                        objDBParameter.Value = LabOrderID
                        objDBParameter.Name = "@labotr_OrderID"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.BigInt
                        objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestID
                        objDBParameter.Name = "@labotr_TestID"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.Int
                        objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultNumber
                        objDBParameter.Name = "@labotr_TestResultNumber"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.VarChar
                        objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultName & ""
                        objDBParameter.Name = "@labotr_TestResultName"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.DateTime
                        objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDateTime
                        objDBParameter.Name = "@labotr_TestResultDateTime"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)


                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.Int
                        objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).IsFinished
                        'If IsFinished = 1 Then
                        '    objDBParameter.Value = 1
                        'Else
                        '    objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).IsFinished
                        'End If
                        objDBParameter.Name = "@labotr_IsFinished"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.BigInt
                        objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).DMSID
                        objDBParameter.Name = "@labotr_DMSID"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                        '\\Lab Denormalization
                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.VarChar
                        objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestName
                        objDBParameter.Name = "@labotr_TestName"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                        'Added below two fileds as per qwest certification and same as updateing while save and close in view order form.
                        'Added by Madan-- on 20100409--

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.DateTime
                        'If _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).BlnSpecimenReceivedDateTime = True Then
                        objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).SpecimenReceivedDateTime
                        'Else
                        'objDBParameter.Value = DBNull.Value
                        'End If
                        objDBParameter.Name = "@labotr_SpecimenReceivedDateTime"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)


                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.DateTime
                        'If _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).BlnResultTransferDateTime = True Then

                        '  If _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).ResultTransferDateTime = "12:00:00 AM" Then
                        If _LabOrder.OrderTests.Item(i).ReportedDateTime = "12:00:00 AM" Then
                            'objDBParameter.Value = Nothing
                            objDBParameter.Value = DateTime.Now
                        Else
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).ReportedDateTime
                        End If


                        ' Else
                        'objDBParameter.Value = DBNull.Value
                        'End If
                        objDBParameter.Name = "@labotr_ResultTransferDateTime"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.VarChar
                        objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).AlternateTestName
                        objDBParameter.Name = "@labotr_AlternateTestName"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                        objDBParameter = New DBParameter
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.VarChar
                        objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).AlternateTestCode
                        objDBParameter.Name = "@labotr_AlternateTestCode"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                        objDBParameter = New DBParameter()
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.Int
                        objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDateTimeUTC
                        objDBParameter.Name = "@TestResultDateTimeUTC"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                        objDBParameter = New DBParameter()
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.Int
                        objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).SpecimenReceivedDateTimeUTC
                        objDBParameter.Name = "@SpecimenReceivedDateTimeUTC"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                        objDBParameter = New DBParameter()
                        objDBParameter.Direction = ParameterDirection.Input
                        objDBParameter.DataType = SqlDbType.Int
                        objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).ResultTransferDateTimeUTC
                        objDBParameter.Name = "@ResultTransferDateTimeUTC"
                        _gloEMRDatabase.DBParametersCol.Add(objDBParameter)


                        'call the stored procedure to insert the result master record
                        _gloEMRDatabase.Add("Lab_InsertOrderTestResult")

                        'insert  multiple result details against the above ordertestresult using the _LabOrderResultID in the detail table Lab_Order_Test_ResultDtl
                        For k As Integer = 0 To _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Count - 1
                            _gloEMRDatabase.DBParametersCol.Clear()

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.BigInt
                            objDBParameter.Value = LabOrderID
                            objDBParameter.Name = "@labotrd_OrderID"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.BigInt
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).TestID
                            objDBParameter.Name = "@labotrd_TestID"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.Int
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).TestResultNumber
                            objDBParameter.Name = "@labotrd_TestResultNumber"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)


                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.Int
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).ResultLineNo
                            objDBParameter.Name = "@labotrd_ResultLineNo"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.BigInt
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).ResultNameID
                            objDBParameter.Name = "@labotrd_ResultNameID"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Size = 255
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).ResultName & ""
                            objDBParameter.Name = "@labotrd_ResultName"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Size = 5000
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).ResultValue & ""
                            objDBParameter.Name = "@labotrd_ResultValue"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Size = 100
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).ResultUnit & ""
                            objDBParameter.Name = "@labotrd_ResultUnit"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Size = 255
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).ResultRange & ""
                            objDBParameter.Name = "@labotrd_ResultRange"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).ResultTypeCode & ""
                            objDBParameter.Name = "@labotrd_ResultType"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).AbnormalFlagCode & ""
                            objDBParameter.Name = "@labotrd_AbnormalFlag"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            ''objDBParameter.Size = 2000
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).ResultComment & ""
                            objDBParameter.Name = "@labotrd_ResultComment"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.Image
                            If (IsNothing(_LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).ResultWord) = False) Then
                                objDBParameter.Value = CType(_LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).ResultWord, Byte()).Clone()
                            Else
                                objDBParameter.Value = DBNull.Value
                            End If
                            objDBParameter.Name = "@labotrd_ResultWord"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.BigInt
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).ResultDMSID
                            objDBParameter.Name = "@labotrd_ResultDMSID"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.BigInt
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).UserID
                            objDBParameter.Name = "@labotrd_ResultUserID"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.DateTime
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).ResultDateTime
                            objDBParameter.Name = "@labotrd_ResultDateTime"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)


                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.DateTime
                            'Dim str As String = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).TestSpecimenCollectionDate
                            Dim str As String = _LabOrder.OrderTests.Item(i).TestSpecimenCollectionDateTime
                            If str = Nothing OrElse str = "1/1/0001 12:00:00 AM" OrElse str = "12:00:00 AM" Then
                                objDBParameter.Value = Nothing
                            Else
                                objDBParameter.Value = _LabOrder.OrderTests.Item(i).TestSpecimenCollectionDateTime   ''_LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).TestSpecimenCollectionDate
                            End If

                            objDBParameter.Name = "@labotrd_TestSpecimenCollectionDateTime"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)


                            'objDBParameter = New DBParameter
                            'objDBParameter.Direction = ParameterDirection.Input
                            'objDBParameter.DataType = SqlDbType.Int
                            'If IsFinished = 1 Then
                            '    objDBParameter.Value = 1
                            'Else
                            '    objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).IsFinished
                            'End If
                            'objDBParameter.Name = "@labotrd_IsFinished"
                            '_gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            ''Sudhir 20090227
                            '@labotrd_LOINCID
                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Size = 50
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).ResultLOINCID & ""
                            objDBParameter.Name = "@labotrd_LOINCID"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            '\\Lab Denormalization
                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).TestName
                            objDBParameter.Name = "@labotrd_TestName"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            'Added below three fileds as per qwest certification and same as updateing while save and close in view order form.
                            'Added by Madan-- on 20100409--
                            '\\Lab AlternateResultName
                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).AlternateResultName
                            objDBParameter.Name = "@labotrd_AlternateResultName"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            '\\Alternate result code
                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).AlternateResultCode
                            objDBParameter.Name = "@labotrd_AlternateResultCode"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            ' \\ProducerIdentifier
                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).ProducerIdentifier
                            objDBParameter.Name = "@labotrd_ProducerIdentifier"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            'End Madan.

                            ''code Added by Abhijeet fileds in result detail table on 20101027
                            '\\LabFacilityName
                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).LabFacilityName
                            objDBParameter.Name = "@labotrd_LabFacilityName"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            '\\LabFacilityStreetAddress
                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).LabFacilityStreetAddress
                            objDBParameter.Name = "@labotrd_LabFacilityStreetAddress"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            '\\LabFacilityCity
                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).LabFacilityCity
                            objDBParameter.Name = "@labotrd_LabFacilityCity"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            '\\LabFacilityState
                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).LabFacilityState
                            objDBParameter.Name = "@labotrd_LabFacilityState"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            '\\LabFacilityZipCode
                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).LabFacilityZipCode
                            objDBParameter.Name = "@labotrd_LabFacilityZipCode"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                            ''End of code by Abhijeet on 20101027 

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).LabFacilityIdentifierTypeCode
                            objDBParameter.Name = "@labotrd_LabFacilityIdentifierTypeCode"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)



                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).LabFacilityOrganizationIdentifier
                            objDBParameter.Name = "@labotrd_LabFacilityOrganizationIdentifier"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).LabFacilityCountry
                            objDBParameter.Name = "@labotrd_LabFacilityCountry"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).LabFacilityCountyOrParishCode
                            objDBParameter.Name = "@labotrd_LabFacilityCountyOrParishCode"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).ResultCode
                            objDBParameter.Name = "@labotrd_ResultCode"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).ResultCodeType
                            objDBParameter.Name = "@labotrd_ResultCodeType"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).LabFacilityMedicalDirectorIDNumber
                            objDBParameter.Name = "@labotrd_LabFacilityMedicalDirectorIDNumber"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).LabFacilityMedicalDirectorLastName
                            objDBParameter.Name = "@labotrd_LabFacilityMedicalDirectorLastName"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)


                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).LabFacilityMedicalDirectorMiddleName
                            objDBParameter.Name = "@labotrd_LabFacilityMedicalDirectorMiddleName"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).LabFacilityMedicalDirectorSuffix
                            objDBParameter.Name = "@labotrd_LabFacilityMedicalDirectorSuffix"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).LabFacilityMedicalDirectorPrefix
                            objDBParameter.Name = "@labotrd_LabFacilityMedicalDirectorPrefix"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)


                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).LabFacilityMedicalDirectorFirstName
                            objDBParameter.Name = "@labotrd_LabFacilityMedicalDirectorFirstName"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.BigInt
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).ResultParentChildFlag
                            objDBParameter.Name = "@labotrd_ResultParentChildFlag"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.Int
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).ResultDateTimeUTC
                            objDBParameter.Name = "@labotrd_ResultDateTimeUTC"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.Int
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).TestSpecimenCollectionDateTimeUTC
                            objDBParameter.Name = "@labotrd_TestSpecimenCollectionDateTimeUTC"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).LabResultConceptID
                            objDBParameter.Name = "@labotrd_ConceptID"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).LabResultICD9
                            objDBParameter.Name = "@labotrd_ICD9"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).LabResultICD10
                            objDBParameter.Name = "@labotrd_ICD10"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.VarChar
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).TestResultDetails.Item(k).LabResultLOINC
                            objDBParameter.Name = "@labotrd_LOINC"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                            objDBParameter = New DBParameter
                            objDBParameter.Direction = ParameterDirection.Input
                            objDBParameter.DataType = SqlDbType.Bit
                            objDBParameter.Value = _LabOrder.OrderTests.Item(i).OrderTestResults.Item(j).IsFinished
                            objDBParameter.Name = "@labotrd_IsFinished"
                            _gloEMRDatabase.DBParametersCol.Add(objDBParameter)



                            'call the stored procedure to add the result detail record
                            _gloEMRDatabase.Add("Lab_InsertOrderTestResultDetails")

                        Next
                    Next
                Next

                Dim _clsLabOrderTests As gloEMRActors.LabActor.OrderTest
                _clsLabOrderTests = New gloEMRActors.LabActor.OrderTest
                If IsNothing(_LabOrder.OrderTests.Item(0).LabURLDocument) = False Then
                    _clsLabOrderTests.INUP_Test_URLdocument(_LabOrder.PatientID, LabOrderID, _LabOrder.OrderTests.Item(0).LabURLDocument)
                End If
                If Not IsNothing(_clsLabOrderTests) Then
                    _clsLabOrderTests.Dispose()
                    _clsLabOrderTests = Nothing
                End If

            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Sub

        Public Function GetSplitOrderTests(ByVal OrderID As Int64) As DataTable
            _gloEMRDatabase = New DataBaseLayer
            Dim dt As DataTable = Nothing
            'dt = New DataTable
            With _gloEMRDatabase
                Dim oPara As New gloEMRDatabase.DBParameter
                .DBParametersCol.Clear()
                oPara.DataType = SqlDbType.BigInt
                oPara.Direction = ParameterDirection.Input
                oPara.Value = OrderID
                oPara.Name = "@OrderID"
                .DBParametersCol.Add(oPara)
                dt = .GetDataTable("Lab_GetSplitOrderTestDtl")

                oPara = Nothing
            End With
            If Not IsNothing(_gloEMRDatabase) Then
                _gloEMRDatabase.Dispose()
                _gloEMRDatabase = Nothing
            End If
            Return dt
        End Function

        Public Sub New()
            MyBase.New()
            _LabOrder = New gloEMRActors.LabActor.LabOrder
            bLabOrderAssigned = True
        End Sub


        'Developer:Sanjog Dhamke
        'Date: 20 Dec 2011 (6060)
        'Bug ID/PRD Name/Sales force Case: PRD Lab Usability - To show Add,Modify & Deletion functionality for ACkw.
        'Reason:To insert patient note into DB.

        Public Function Get_Acknowledgment(ByVal OrderID As Long, ByVal TaskSrNo As Long, ByVal nUserID As Long) As DataTable
            Dim dt As DataTable = Nothing
            Dim objDBParameter As DBParameter = Nothing

            Try
                'dt = New DataTable

                _gloEMRDatabase = New DataBaseLayer
                _gloEMRDatabase.DBParametersCol.Clear()

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = OrderID
                objDBParameter.Name = "@OrderID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = TaskSrNo
                objDBParameter.Name = "@nTaskSrNO"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = nUserID
                objDBParameter.Name = "@nLoginUserID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                dt = _gloEMRDatabase.GetDataTable("Get_Acknowledgment")

                Return dt

            Catch ex As Exception
                Throw ex
                Return Nothing
            Finally
                'If Not IsNothing(dt) Then
                '    dt.Dispose()
                '    dt = Nothing
                'End If
                If Not IsNothing(objDBParameter) Then
                    objDBParameter = Nothing
                End If
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function


        Public Sub Add_Acknowledgment(ByVal OrderID As Long, ByVal OrderPrefix As String, ByVal OrderNumber As Long, ByVal UserID As Long, ByVal Reviewdate As DateTime, ByVal comments As String, ByVal patNote As String, ByVal AkwSrNo As Long)
            _gloEMRDatabase = New DataBaseLayer

            Dim objDBParameter As DBParameter
            Try

                _gloEMRDatabase.DBParametersCol.Clear()

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = OrderID
                objDBParameter.Name = "@nOrderId"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = OrderPrefix
                objDBParameter.Name = "@OrderNumberPrefix"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = OrderNumber
                objDBParameter.Name = "@nOrderNumberID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = UserID
                objDBParameter.Name = "@nViwedUserID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.DateTime
                objDBParameter.Value = Reviewdate
                objDBParameter.Name = "@dtpReviwed"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = comments
                objDBParameter.Name = "@sComments"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = patNote
                objDBParameter.Name = "@sPatientNote"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = AkwSrNo
                objDBParameter.Name = "@nAcknowledgeSrNo"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                _gloEMRDatabase.Add("Lab_InsertAcknowledgment")


            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try

        End Sub

        Public Sub Delete_Acknowledgement(ByVal AckSrNo As Int64, ByVal OrderID As Int64)
            _gloEMRDatabase = New DataBaseLayer
            Dim objDBParameter As DBParameter
            Try

                _gloEMRDatabase.DBParametersCol.Clear()

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = OrderID
                objDBParameter.Name = "@nOrderID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = AckSrNo
                objDBParameter.Name = "@nAckSrNo"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                _gloEMRDatabase.Delete("Del_Acknowlwdgement")

            Catch ex As Exception
                _Exception = New gloEMRLabExceptions
                _Exception.ErrorMessage = ex.Message
                Throw _Exception
                _Exception.Dispose()
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Sub
        'END
        Public Function GetOrderTests(ByVal OrderID As Long) As ArrayList
            Dim arrTestIDs As New ArrayList
            _gloEMRDatabase = New gloEMRDatabase.DataBaseLayer
            Dim dtOrderTests As DataTable = Nothing
            Dim _strSQL As String = ""

            Try
                _strSQL = "select isnull(labotd_TestName,'') as labotd_TestName from Lab_Order_TestDtl where labotd_OrderID = " & OrderID & ""

                dtOrderTests = _gloEMRDatabase.GetDataTable_Query(_strSQL)
                If (IsNothing(dtOrderTests) = False) Then


                    If dtOrderTests.Rows.Count > 0 Then
                        For i As Integer = 0 To dtOrderTests.Rows.Count - 1
                            arrTestIDs.Add(dtOrderTests.Rows(i)("labotd_TestName"))
                        Next
                    End If
                    dtOrderTests.Dispose()
                    dtOrderTests = Nothing
                End If

            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try

            Return arrTestIDs
        End Function

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    If bLabOrderAssigned Then
                        If (IsNothing(_LabOrder) = False) Then
                            _LabOrder.Dispose()
                            _LabOrder = Nothing
                        End If
                    End If
                    ' TODO: free unmanaged resources when explicitly called
                End If
                '_LabOrder = Nothing
                ' TODO: free shared unmanaged resources
            End If
            Me.disposedValue = True
        End Sub

#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class

    Public Class gloEMRLabContactInfo
        Implements IDisposable

        Private _LabContactInfo As gloEMRActors.LabActor.LabContactInformation
        Private _Exception As gloEMRLabExceptions
        Dim _gloEMRDatabase As DataBaseLayer

        Public Property LabContactInfo() As gloEMRActors.LabActor.LabContactInformation
            Get
                Return _LabContactInfo
            End Get
            Set(ByVal value As gloEMRActors.LabActor.LabContactInformation)
                _LabContactInfo = value
            End Set
        End Property

        Public Function Add(ByVal ContactType As gloEMRActors.LabActor.enumContactType)
            _gloEMRDatabase = New DataBaseLayer
            Dim objDBParameter As DBParameter
            Dim _LabContactInfoID As Int64 = 0

            Try
                _gloEMRDatabase.DBParametersCol.Clear()
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = GetPrefixTransactionID(Date.Now)
                objDBParameter.Name = "@MachineID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabContactInfo.ContactName
                objDBParameter.Name = "@labci_ContactName"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabContactInfo.FirstName
                objDBParameter.Name = "@labci_FirstName"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabContactInfo.MiddleName
                objDBParameter.Name = "@labci_MiddleName"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabContactInfo.LastName
                objDBParameter.Name = "@labci_LastName"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.Int
                objDBParameter.Value = CInt(ContactType)
                objDBParameter.Name = "@labci_Type"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                '-----
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabContactInfo.Address1
                objDBParameter.Name = "@sAddressLine1"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabContactInfo.Address2
                objDBParameter.Name = "@sAddressLine2"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabContactInfo.City
                objDBParameter.Name = "@sCity"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabContactInfo.State
                objDBParameter.Name = "@sState"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabContactInfo.Zip
                objDBParameter.Name = "@sZip"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabContactInfo.Country
                objDBParameter.Name = "@sCountry"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabContactInfo.County
                objDBParameter.Name = "@sCounty"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabContactInfo.Phone
                objDBParameter.Name = "@sPhone"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                '------
                'sarika 16th oct 07
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.InputOutput
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = 0
                objDBParameter.Name = "@NewID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                '-------------

                _LabContactInfoID = _gloEMRDatabase.Add("Lab_InsertContactInfo")

                Return _LabContactInfoID
            Catch ex As Exception
                _Exception = New gloEMRLabExceptions
                _Exception.ErrorMessage = ex.Message
                Throw _Exception
                _Exception.Dispose()
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function

        Public Function Modify(ByVal ContactInfoID As Int64, ByVal ContactType As gloEMRActors.LabActor.enumContactType) As Boolean
            _gloEMRDatabase = New DataBaseLayer
            Dim objDBParameter As DBParameter
            Dim _LabContactInfoID As Int64 = 0

            Try
                _gloEMRDatabase.DBParametersCol.Clear()
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = ContactInfoID
                objDBParameter.Name = "@labci_Id"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabContactInfo.ContactName
                objDBParameter.Name = "@labci_ContactName"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)


                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabContactInfo.FirstName
                objDBParameter.Name = "@labci_FirstName"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabContactInfo.MiddleName
                objDBParameter.Name = "@labci_MiddleName"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabContactInfo.LastName
                objDBParameter.Name = "@labci_LastName"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.Int
                objDBParameter.Value = CInt(ContactType)
                objDBParameter.Name = "@labci_Type"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                '-----
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabContactInfo.Address1
                objDBParameter.Name = "@sAddressLine1"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabContactInfo.Address2
                objDBParameter.Name = "@sAddressLine2"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabContactInfo.City
                objDBParameter.Name = "@sCity"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabContactInfo.State
                objDBParameter.Name = "@sState"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabContactInfo.Zip
                objDBParameter.Name = "@sZip"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabContactInfo.Country
                objDBParameter.Name = "@sCountry"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabContactInfo.County
                objDBParameter.Name = "@sCounty"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = _LabContactInfo.Phone
                objDBParameter.Name = "@sPhone"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                '------

                _LabContactInfoID = _gloEMRDatabase.Add("Lab_UpdateContactInfo")

                Return True
            Catch ex As Exception
                _Exception = New gloEMRLabExceptions
                _Exception.ErrorMessage = ex.Message
                Throw _Exception
                _Exception.Dispose()
            Finally

                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function

        Public Function Delete(ByVal ContactInfoID As Int64, ByVal ContactType As gloEMRActors.LabActor.enumContactType) As Boolean
            Dim _gloEMRDBID As New DataBaseLayer

            'delete detail record 
            Try

                _gloEMRDBID.Delete_Query("Delete FROM Lab_ContactInfo WHERE labci_Id = " & ContactInfoID & " and  labci_Type=" & CInt(ContactType) & "")

            Catch ex As Exception

                Throw ex

            Finally

                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If

            End Try

            '_gloEMRDatabase.Dispose()
            Return True
        End Function

        Public Function IsExists(ByVal ContactName As String, ByVal FirstName As String, ByVal MiddleName As String, ByVal LastName As String, ByVal ContactType As gloEMRActors.LabActor.enumContactType) As Boolean

            Dim _ID As String = ""
            Dim _Result As Boolean = False
            Dim _gloEMRDBID As New DataBaseLayer
            Dim _strSQL As String = ""

            Try


                ''Sandip Darade 20090306
                ''Code added to replace  single quote to avoid exception
                ContactName = ContactName.Replace("'", "")
                FirstName = FirstName.Replace("'", "")
                MiddleName = MiddleName.Replace("'", "")
                LastName = LastName.Replace("'", "")


                If ContactType = gloEMRActors.LabActor.enumContactType.PreferredLab Then
                    'preferred lab
                    _strSQL = "SELECT labci_Id FROM Lab_ContactInfo WHERE Upper(labci_ContactName) = '" & ContactName.ToUpper & "' and  labci_Type=" & CInt(ContactType) & ""
                    _ID = _gloEMRDBID.GetRecord_Query(_strSQL)
                Else
                    'referred by / sampled by
                    _strSQL = "select labci_Id from Lab_ContactInfo where labci_FirstName = '" & FirstName & "' and labci_MiddleName = '" & MiddleName & "' and labci_LastName = '" & LastName & "'  and labci_Type = " & CInt(ContactType) & ""
                    _ID = _gloEMRDBID.GetRecord_Query(_strSQL)
                End If

                If Val(_ID) > 0 Then
                    _Result = True
                End If

                '_gloEMRDatabase.Dispose()

            Catch ex As Exception
                Throw ex
            Finally

                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try

            Return _Result

        End Function


        'sarika 16th oct 07
        'if the form is invoked from the custom search user control from Lab Orders form
        Public Function IsExists(ByVal ContactID As Long, ByVal ContactName As String, ByVal FirstName As String, ByVal MiddleName As String, ByVal LastName As String, ByVal ContactType As gloEMRActors.LabActor.enumContactType) As Boolean

            Dim _ID As String = ""
            Dim _Result As Boolean = False
            Dim _gloEMRDBID As New DataBaseLayer
            Dim _strSQL As String = ""


            Try

                ''Sandip Darade 20090306
                ''Code added to replace  single quote to avoid exception
                ContactName = ContactName.Replace("'", "")
                FirstName = FirstName.Replace("'", "")
                MiddleName = MiddleName.Replace("'", "")
                LastName = LastName.Replace("'", "")


                If ContactType = gloEMRActors.LabActor.enumContactType.PreferredLab Then
                    'preferred lab
                    _strSQL = "SELECT labci_Id FROM Lab_ContactInfo WHERE Upper(labci_ContactName) = '" & ContactName.ToUpper & "' and  labci_Type=" & CInt(ContactType) & " and labci_Id <> " & ContactID
                    _ID = _gloEMRDBID.GetRecord_Query(_strSQL)
                    'Else
                    '    'referred by / sampled by
                    '    _strSQL = "select labci_Id from Lab_ContactInfo where labci_FirstName = '" & FirstName & "' and labci_MiddleName = '" & MiddleName & "' and labci_LastName = '" & LastName & "'  and labci_Type = " & CInt(ContactType) & ""
                    '    _ID = _gloEMRDBID.GetRecord_Query(_strSQL)
                End If

                If Val(_ID) > 0 Then
                    _Result = True
                End If

                '_gloEMRDatabase.Dispose()

            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try

            Return _Result

        End Function
        '-----------------------------------

        Public Function IsDelete(ByVal SpecimenName As String, ByVal ContactType As gloEMRActors.LabActor.enumContactType) As Boolean
            Dim _ID As String = ""
            Dim _Result As Boolean = True ' False

            'Dim _gloEMRDBID As New DataBaseLayer
            '_ID = _gloEMRDBID.GetRecord_Query("SELECT labtm_ID FROM Lab_Test_Mst WHERE Upper(labtm_Name) = '" & TestName.ToUpper & "'")

            'If Val(_ID) > 0 Then
            '    _Result = True
            'End If

            '_gloEMRDatabase.Dispose()
            Return _Result
        End Function

        Public Function GetContactInformation(ByVal ContactInfoID As Int64, ByVal ContactType As gloEMRActors.LabActor.enumContactType) As gloEMRActors.LabActor.LabContactInformation
            _gloEMRDatabase = New DataBaseLayer

            Dim oContactInfo As gloEMRActors.LabActor.LabContactInformation
            Dim dt As DataTable

            Try
                dt = _gloEMRDatabase.GetDataTable_Query("select * from Lab_ContactInfo where labci_Id =" & ContactInfoID & " and  labci_Type=" & CInt(ContactType) & " AND labci_ID IS NOT NULL")
                oContactInfo = New gloEMRActors.LabActor.LabContactInformation
                If Not dt Is Nothing Then
                    oContactInfo.ContactID = dt.Rows(0)("labci_Id")
                    If Not IsDBNull(dt.Rows(0)("labci_ContactName")) Then
                        oContactInfo.ContactName = dt.Rows(0)("labci_ContactName")
                    End If
                    If Not IsDBNull(dt.Rows(0)("labci_FirstName")) Then
                        oContactInfo.FirstName = dt.Rows(0)("labci_FirstName")
                    End If
                    If Not IsDBNull(dt.Rows(0)("labci_MiddleName")) Then
                        oContactInfo.MiddleName = dt.Rows(0)("labci_MiddleName")
                    End If
                    If Not IsDBNull(dt.Rows(0)("labci_LastName")) Then
                        oContactInfo.LastName = dt.Rows(0)("labci_LastName")
                    End If
                    If Not IsDBNull(dt.Rows(0)("labci_Type")) Then
                        oContactInfo.Type = CInt(dt.Rows(0)("labci_Type"))
                    End If

                    '----
                    If Not IsDBNull(dt.Rows(0)("sAddressLine1")) Then
                        oContactInfo.Address1 = dt.Rows(0)("sAddressLine1")
                    End If
                    If Not IsDBNull(dt.Rows(0)("sAddressLine2")) Then
                        oContactInfo.Address2 = dt.Rows(0)("sAddressLine2")
                    End If
                    If Not IsDBNull(dt.Rows(0)("sCity")) Then
                        oContactInfo.City = dt.Rows(0)("sCity")
                    End If
                    If Not IsDBNull(dt.Rows(0)("sState")) Then
                        oContactInfo.State = dt.Rows(0)("sState")
                    End If
                    If Not IsDBNull(dt.Rows(0)("sZip")) Then
                        oContactInfo.Zip = dt.Rows(0)("sZip")
                    End If
                    If Not IsDBNull(dt.Rows(0)("sCountry")) Then
                        oContactInfo.Country = dt.Rows(0)("sCountry")
                    End If
                    If Not IsDBNull(dt.Rows(0)("sCounty")) Then
                        oContactInfo.County = dt.Rows(0)("sCounty")
                    End If
                    If Not IsDBNull(dt.Rows(0)("sPhone")) Then
                        oContactInfo.Phone = dt.Rows(0)("sPhone")
                    End If
                    '----

                    dt.Dispose()
                    dt = Nothing
                End If
                Return oContactInfo
            Catch ex As Exception
                Throw ex
                Return oContactInfo
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function

        Public Function GetContactInformations(ByVal ContactType As gloEMRActors.LabActor.enumContactType) As gloEMRActors.LabActor.LabContactInformations
            _gloEMRDatabase = New DataBaseLayer

            Dim oContactInfos As New gloEMRActors.LabActor.LabContactInformations
            Dim oContactInfo As gloEMRActors.LabActor.LabContactInformation
            Dim dt As DataTable

            Try
                dt = _gloEMRDatabase.GetDataTable_Query("select * from Lab_ContactInfo where labci_Type=" & CInt(ContactType) & " AND labci_Id IS NOT NULL")
                If Not dt Is Nothing Then
                    For i As Int16 = 0 To dt.Rows.Count - 1
                        oContactInfo = New gloEMRActors.LabActor.LabContactInformation

                        oContactInfo.ContactID = dt.Rows(i)("labci_Id")
                        If Not IsDBNull(dt.Rows(i)("labci_ContactName")) Then
                            oContactInfo.ContactName = dt.Rows(i)("labci_ContactName")
                        End If
                        If Not IsDBNull(dt.Rows(i)("labci_FirstName")) Then
                            oContactInfo.FirstName = dt.Rows(i)("labci_FirstName")
                        End If
                        If Not IsDBNull(dt.Rows(i)("labci_MiddleName")) Then
                            oContactInfo.MiddleName = dt.Rows(i)("labci_MiddleName")
                        End If
                        If Not IsDBNull(dt.Rows(i)("labci_LastName")) Then
                            oContactInfo.LastName = dt.Rows(i)("labci_LastName")
                        End If
                        If Not IsDBNull(dt.Rows(i)("labci_Type")) Then
                            oContactInfo.Type = CInt(dt.Rows(i)("labci_Type"))
                        End If

                        If Not oContactInfo Is Nothing Then
                            oContactInfos.Add(oContactInfo)
                        End If
                        oContactInfo = Nothing
                    Next
                    dt.Dispose()
                    dt = Nothing
                End If
                Return oContactInfos
            Catch ex As Exception
                Throw ex
                Return oContactInfos
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try

        End Function


        'sarika 4th july 07
        Public Function GetContactInfoTable(ByVal ContactType As gloEMRActors.LabActor.enumContactType) As DataTable
            _gloEMRDatabase = New DataBaseLayer

            'Dim oContactInfos As New gloEMRActors.LabActor.LabContactInformations
            'Dim oContactInfo As gloEMRActors.LabActor.LabContactInformation
            Dim dt As DataTable = Nothing

            Try
                If ContactType = gloEMRActors.LabActor.enumContactType.PreferredLab Then
                    dt = _gloEMRDatabase.GetDataTable_Query("select labci_Id,labci_ContactName as ContactName from Lab_ContactInfo where labci_Type=" & CInt(ContactType) & " AND labci_Id IS NOT NULL")
                Else
                    dt = _gloEMRDatabase.GetDataTable_Query("select labci_Id,isnull(labci_FirstName,'') as FirstName,isnull(labci_MiddleName,'') as MiddleName,isnull(labci_LastName,'') as LastName from Lab_ContactInfo where labci_Type=" & CInt(ContactType) & " AND labci_Id IS NOT NULL")
                End If

                'If Not dt Is Nothing Then
                '    For i As Int16 = 0 To dt.Rows.Count - 1
                '        oContactInfo = New gloEMRActors.LabActor.LabContactInformation

                '        oContactInfo.ContactID = dt.Rows(i)("labci_Id")
                '        If Not IsDBNull(dt.Rows(i)("labci_ContactName")) Then
                '            oContactInfo.ContactName = dt.Rows(i)("labci_ContactName")
                '        End If
                '        If Not IsDBNull(dt.Rows(i)("labci_FirstName")) Then
                '            oContactInfo.FirstName = dt.Rows(i)("labci_FirstName")
                '        End If
                '        If Not IsDBNull(dt.Rows(i)("labci_MiddleName")) Then
                '            oContactInfo.MiddleName = dt.Rows(i)("labci_MiddleName")
                '        End If
                '        If Not IsDBNull(dt.Rows(i)("labci_LastName")) Then
                '            oContactInfo.LastName = dt.Rows(i)("labci_LastName")
                '        End If
                '        If Not IsDBNull(dt.Rows(i)("labci_Type")) Then
                '            oContactInfo.Type = CInt(dt.Rows(i)("labci_Type"))
                '        End If

                '        If Not oContactInfo Is Nothing Then
                '            oContactInfos.Add(oContactInfo)
                '        End If
                '        oContactInfo = Nothing
                '    Next
                'End If
                Return dt
            Catch ex As Exception
                Throw ex
                Return Nothing
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function

        '-----------------------

        Private Function GetPrefixTransactionID(ByVal ReferenceDate As DateTime) As Long
            Dim strID As String
            Dim dtDate As DateTime

            Try


                dtDate = System.DateTime.Now
                strID = DateDiff(DateInterval.Day, CDate("1/1/1900"), dtDate.Date) & DateDiff(DateInterval.Second, dtDate.Date, dtDate) & DateDiff(DateInterval.Day, CDate("1/1/1900"), ReferenceDate.Date)
            Catch ex As Exception
                Throw ex
            End Try
            Return CLng(strID)
        End Function




        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    If (IsNothing(_LabContactInfo) = False) Then
                        _LabContactInfo.Dispose()
                        _LabContactInfo = Nothing
                    End If
                    ' TODO: free unmanaged resources when explicitly called
                End If
                _LabContactInfo = Nothing
                ' TODO: free shared unmanaged resources
            End If
            Me.disposedValue = True
        End Sub

        Public Function GetProviderName(ByVal ProviderID As Int64) As DataTable

            Dim _gloEMRDBID As New DataBaseLayer
            Dim _strSQL As String = ""
            Dim _ProviderName As String = ""
            Dim dt As DataTable = Nothing
            Try


                _strSQL = "SELECT  sFirstName,sMiddleName,sLastName FROM Provider_MST WHERE nProviderID = " & ProviderID & " "
                dt = _gloEMRDBID.GetDataTable_Query(_strSQL) ' .GetRecord_Query(_strSQL)

            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDBID) Then
                    _gloEMRDBID.Dispose()
                    _gloEMRDBID = Nothing
                End If
            End Try
            Return dt
        End Function

#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

        Public Sub New()
            MyBase.New()
            _LabContactInfo = New gloEMRActors.LabActor.LabContactInformation
        End Sub



    End Class

    Public Class LabAckNotes
        Implements IDisposable

        Dim _gloEMRDatabase As DataBaseLayer
        Private _Exception As gloEMRLabExceptions
        Public Function AddModify(ByVal labAckNotes_ID As Long, ByVal labAckNotes As String, ByVal labAckNotes_Type As Int16)
            _gloEMRDatabase = New DataBaseLayer
            Dim objDBParameter As DBParameter
            Dim _LabNotesID As Int64 = 0

            Try
                _gloEMRDatabase.DBParametersCol.Clear()
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = labAckNotes_ID
                objDBParameter.Name = "@labAckNotes_ID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = labAckNotes
                objDBParameter.Name = "@labAckNotes"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.Int
                objDBParameter.Value = labAckNotes_Type
                objDBParameter.Name = "@labAckNotes_Type"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.InputOutput
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = 0
                objDBParameter.Name = "@NewID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                '-------------

                _LabNotesID = _gloEMRDatabase.Add("Lab_InUpNotes")

                Return _LabNotesID
            Catch ex As Exception
                _Exception = New gloEMRLabExceptions
                _Exception.ErrorMessage = ex.Message
                Throw _Exception
                _Exception.Dispose()
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function

        Public Function Get_AckNotes(ByVal labAckNotes_ID As Long, ByVal labAckNotes_Type As Int16) As DataTable
            Dim dt As DataTable = Nothing
            Dim objDBParameter As DBParameter = Nothing

            Try
                '    dt = New DataTable

                _gloEMRDatabase = New DataBaseLayer
                _gloEMRDatabase.DBParametersCol.Clear()

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = labAckNotes_ID
                objDBParameter.Name = "@labAckNotes_ID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.Int
                objDBParameter.Value = labAckNotes_Type
                objDBParameter.Name = "@labAckNotes_Type"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                dt = _gloEMRDatabase.GetDataTable("Lab_SelectNotesBy_ID_Type")

                Return dt

            Catch ex As Exception
                Throw ex
                Return Nothing
            Finally
                'If Not IsNothing(dt) Then
                '    dt.Dispose()
                '    dt = Nothing
                'End If
                If Not IsNothing(objDBParameter) Then
                    objDBParameter = Nothing
                End If

                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If

            End Try
        End Function

        Public Function Delete(ByVal labAckNotes_ID As Long)
            _gloEMRDatabase = New DataBaseLayer
            Dim objDBParameter As DBParameter
            Dim _LabNotesID As Int64 = 0

            Try
                _gloEMRDatabase.DBParametersCol.Clear()
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = labAckNotes_ID
                objDBParameter.Name = "@labAckNotes_ID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                _LabNotesID = _gloEMRDatabase.Add("Lab_DeleteNote")

                Return _LabNotesID
            Catch ex As Exception
                _Exception = New gloEMRLabExceptions
                _Exception.ErrorMessage = ex.Message
                Throw _Exception
                _Exception.Dispose()
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function

        Public Function AddNormalNote(ByVal labAckInternalNotes_ID As Long, ByVal labAckPatientNotes_ID As Long)
            _gloEMRDatabase = New DataBaseLayer
            Dim objDBParameter As DBParameter
            Dim _LabNormalNotesID As Int64 = 0

            Try
                _gloEMRDatabase.DBParametersCol.Clear()
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = labAckInternalNotes_ID
                objDBParameter.Name = "@labAckInternalNotes_ID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = labAckPatientNotes_ID
                objDBParameter.Name = "@labAckPatientNotes_ID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                _LabNormalNotesID = _gloEMRDatabase.Add("Lab_InsertNormalNotes")

                Return _LabNormalNotesID
            Catch ex As Exception
                _Exception = New gloEMRLabExceptions
                _Exception.ErrorMessage = ex.Message
                Throw _Exception
                _Exception.Dispose()
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function
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

End Namespace
