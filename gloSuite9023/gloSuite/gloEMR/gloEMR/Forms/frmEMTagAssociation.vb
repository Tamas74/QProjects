Imports System.Reflection
Public Class frmEMTagAssociation

    Public arrLabs As New ArrayList
    Public arrOrders As New ArrayList
    Public arrOtherDiag As New ArrayList
    Public arrManagment As New ArrayList
    Private FormName As String = ""
    'Public Const strLabs As String = "Labs"
    'Public Const strOrders As String = "Orders"
    'Public Const strOtherDiagnosis As String = "OtherDiagnosis"
    'Public Const strMangementOption As String = "ManagementOption"
    Private _dt As New DataTable
    Private Sub tlb_Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlb_Ok.Click
        Try
            Dim propertyInfos As PropertyInfo()
            Dim strprop As String
            Dim pType As Type
            '   Dim Emylist As myList
            '  Dim EGeneralItems As gloGeneralItem.gloItems
            Dim oListItem As gloGeneralItem.gloItem

            ''Labs
            arrLabs = New ArrayList
            propertyInfos = GetType(AlphaII.CodeWizard.Objects.EvaluationManagement.MedComplexLabs).GetProperties(BindingFlags.CreateInstance Or BindingFlags.DeclaredOnly Or BindingFlags.Default Or BindingFlags.ExactBinding Or BindingFlags.FlattenHierarchy Or BindingFlags.GetField Or BindingFlags.GetProperty Or BindingFlags.IgnoreCase Or BindingFlags.IgnoreReturn Or BindingFlags.Instance Or BindingFlags.InvokeMethod Or BindingFlags.NonPublic Or BindingFlags.OptionalParamBinding Or BindingFlags.Public Or BindingFlags.PutDispProperty Or BindingFlags.PutRefDispProperty Or BindingFlags.SetField Or BindingFlags.SetProperty Or BindingFlags.Static Or BindingFlags.SuppressChangeType)
            For Each propertyInfo As PropertyInfo In propertyInfos
                strprop = propertyInfo.Name
                pType = propertyInfo.PropertyType

                If pType.Name = "Boolean" Then
                    If chkLDeepIncisionalBiopsy.Checked = True And strprop = "IncisionalBiopsyRoutine" Then
                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strLabs.ToString()
                        'EGeneralItems.Add(oListItem)
                        arrLabs.Add(oListItem)
                        oListItem.Dispose()
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strLabs.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrLabs.Add(Emylist)
                    End If
                    If chkLSuperficialbiopsy.Checked = True And strprop = "SuperficialBiopsyRoutine" Then
                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strLabs.ToString()
                        'EGeneralItems.Add(oListItem)
                        arrLabs.Add(oListItem)
                        oListItem.Dispose()

                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strLabs.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrLabs.Add(Emylist)
                    End If
                    If chkLTypesandCrossmatch.Checked = True And strprop = "TypeCrossmatchRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strLabs.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrLabs.Add(Emylist)
                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strLabs.ToString()
                        'EGeneralItems.Add(oListItem)
                        arrLabs.Add(oListItem)
                        oListItem.Dispose()

                    End If
                    If chkLPT.Checked = True And strprop = "PTRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strLabs.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrLabs.Add(Emylist)
                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strLabs.ToString()
                        'EGeneralItems.Add(oListItem)
                        arrLabs.Add(oListItem)
                        oListItem.Dispose()

                    End If
                    If chkLABGS.Checked = True And strprop = "ABGsRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strLabs.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrLabs.Add(Emylist)
                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strLabs.ToString()
                        'EGeneralItems.Add(oListItem)
                        arrLabs.Add(oListItem)
                        oListItem.Dispose()

                    End If
                    If chkLCardiacenzymes.Checked = True And strprop = "CardiacEnzymesRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strLabs.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrLabs.Add(Emylist)
                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strLabs.ToString()
                        'EGeneralItems.Add(oListItem)
                        arrLabs.Add(oListItem)
                        oListItem.Dispose()

                    End If
                    If chkLChemicalProfile.Checked = True And strprop = "ChemicalProfileRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strLabs.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrLabs.Add(Emylist)
                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strLabs.ToString()
                        'EGeneralItems.Add(oListItem)
                        arrLabs.Add(oListItem)
                        oListItem.Dispose()

                    End If
                    If chkLETOH.Checked = True And strprop = "DrugScreenRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strLabs.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrLabs.Add(Emylist)
                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strLabs.ToString()
                        'EGeneralItems.Add(oListItem)
                        arrLabs.Add(oListItem)
                        oListItem.Dispose()

                    End If
                    If chkLElectrolytes.Checked = True And strprop = "ElectrolytesRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strLabs.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrLabs.Add(Emylist)
                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strLabs.ToString()
                        'EGeneralItems.Add(oListItem)
                        arrLabs.Add(oListItem)
                        oListItem.Dispose()

                    End If
                    If chkLBun.Checked = True And strprop = "BunCreatinineRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strLabs.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrLabs.Add(Emylist)
                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strLabs.ToString()
                        'EGeneralItems.Add(oListItem)
                        arrLabs.Add(oListItem)
                        oListItem.Dispose()

                    End If
                    If chkLAmylase.Checked = True And strprop = "AmylaseRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strLabs.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrLabs.Add(Emylist)
                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strLabs.ToString()
                        'EGeneralItems.Add(oListItem)
                        arrLabs.Add(oListItem)
                        oListItem.Dispose()

                    End If
                    If chkLPregnancyTest.Checked = True And strprop = "PregnancyTestRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strLabs.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrLabs.Add(Emylist)
                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strLabs.ToString()
                        'EGeneralItems.Add(oListItem)
                        arrLabs.Add(oListItem)
                        oListItem.Dispose()

                    End If
                    If chkLFlu.Checked = True And strprop = "FluStrepMonoRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strLabs.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrLabs.Add(Emylist)
                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strLabs.ToString()
                        'EGeneralItems.Add(oListItem)
                        arrLabs.Add(oListItem)
                        oListItem.Dispose()

                    End If
                    If chkLLCBC.Checked = True And strprop = "CbcUaRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strLabs.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrLabs.Add(Emylist)
                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strLabs.ToString()
                        'EGeneralItems.Add(oListItem)
                        arrLabs.Add(oListItem)
                        oListItem.Dispose()

                    End If
                    If chkLIndependentVisualizationoftest.Checked = True And strprop = "IndependentVisualTest" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strLabs.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrLabs.Add(Emylist)
                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strLabs.ToString()
                        'EGeneralItems.Add(oListItem)
                        arrLabs.Add(oListItem)
                        oListItem.Dispose()

                    End If
                    If chkLDiscussionwithperformingPhysician.Checked = True And strprop = "DiscussionWPerformingPhys" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strLabs.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrLabs.Add(Emylist)
                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strLabs.ToString()
                        'EGeneralItems.Add(oListItem)
                        arrLabs.Add(oListItem)
                        oListItem.Dispose()

                    End If
                Else
                    If nudLOtherLabs.Value <> 0 Then
                        If strprop = "OtherLabsCount" Then
                            'Emylist = New myList
                            'Emylist.AssociatedProperty = strprop
                            'Emylist.AssociatedCategory = strLabs.ToString()
                            'Emylist.AssociatedItem = nudLOtherLabs.Value.ToString()
                            'arrLabs.Add(Emylist)
                            oListItem = New gloGeneralItem.gloItem
                            oListItem.Description = strprop
                            oListItem.Code = strLabs.ToString()
                            oListItem.Status = nudLOtherLabs.Value.ToString()
                            'EGeneralItems.Add(oListItem)
                            arrLabs.Add(oListItem)
                            oListItem.Dispose()

                        End If
                    End If
                End If
            Next

            ''''Orders
            arrOrders = New ArrayList
            propertyInfos = GetType(AlphaII.CodeWizard.Objects.EvaluationManagement.MedComplexXrayRadiology).GetProperties(BindingFlags.CreateInstance Or BindingFlags.DeclaredOnly Or BindingFlags.Default Or BindingFlags.ExactBinding Or BindingFlags.FlattenHierarchy Or BindingFlags.GetField Or BindingFlags.GetProperty Or BindingFlags.IgnoreCase Or BindingFlags.IgnoreReturn Or BindingFlags.Instance Or BindingFlags.InvokeMethod Or BindingFlags.NonPublic Or BindingFlags.OptionalParamBinding Or BindingFlags.Public Or BindingFlags.PutDispProperty Or BindingFlags.PutRefDispProperty Or BindingFlags.SetField Or BindingFlags.SetProperty Or BindingFlags.Static Or BindingFlags.SuppressChangeType)
            For Each propertyInfo As PropertyInfo In propertyInfos
                strprop = propertyInfo.Name
                pType = propertyInfo.PropertyType

                If pType.Name = "Boolean" Then
                    If chkXVascularStudieswrisk.Checked = True And strprop = "VascularStudiesWRiskRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strOrders.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrOrders.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strOrders.ToString()
                        'EGeneralItems.Add(oListItem)
                        arrOrders.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkXVascularStudies.Checked = True And strprop = "VascularStudiesRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strOrders.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrOrders.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strOrders.ToString()
                        'EGeneralItems.Add(oListItem)
                        arrOrders.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkXMRI.Checked = True And strprop = "MRIRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strOrders.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrOrders.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strOrders.ToString()
                        'EGeneralItems.Add(oListItem)
                        arrOrders.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkXcatScan.Checked = True And strprop = "CATScanRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strOrders.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrOrders.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strOrders.ToString()
                        'EGeneralItems.Add(oListItem)
                        arrOrders.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkXIVP.Checked = True And strprop = "IVPRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strOrders.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrOrders.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strOrders.ToString()
                        'EGeneralItems.Add(oListItem)
                        arrOrders.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkXGIGallablader.Checked = True And strprop = "GIGallbladderRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strOrders.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrOrders.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strOrders.ToString()
                        'EGeneralItems.Add(oListItem)
                        arrOrders.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkXTLSpire.Checked = True And strprop = "TLSpineRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strOrders.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrOrders.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strOrders.ToString()
                        'EGeneralItems.Add(oListItem)
                        arrOrders.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkXDiscographt.Checked = True And strprop = "DiscographyRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strOrders.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrOrders.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strOrders.ToString()
                        'EGeneralItems.Add(oListItem)
                        arrOrders.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkXDiagosticUltrasound.Checked = True And strprop = "DiagUltrasoundRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strOrders.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrOrders.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strOrders.ToString()
                        'EGeneralItems.Add(oListItem)
                        arrOrders.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkXCspine.Checked = True And strprop = "CSpineRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strOrders.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrOrders.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strOrders.ToString()
                        'EGeneralItems.Add(oListItem)
                        arrOrders.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkXHipPelvis.Checked = True And strprop = "HipPelvisRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strOrders.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrOrders.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strOrders.ToString()
                        'EGeneralItems.Add(oListItem)
                        arrOrders.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkXAbdomen.Checked = True And strprop = "AbdomenRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strOrders.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrOrders.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strOrders.ToString()
                        'EGeneralItems.Add(oListItem)
                        arrOrders.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkXExtrimities.Checked = True And strprop = "ExtremitiesRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strOrders.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrOrders.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strOrders.ToString()
                        'EGeneralItems.Add(oListItem)
                        arrOrders.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkXChest.Checked = True And strprop = "ChestRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strOrders.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrOrders.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strOrders.ToString()
                        'EGeneralItems.Add(oListItem)
                        arrOrders.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkXIndepedent.Checked = True And strprop = "IndependentVisualTest" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strOrders.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrOrders.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strOrders.ToString()
                        'EGeneralItems.Add(oListItem)
                        arrOrders.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkXperformingPhy.Checked = True And strprop = "DiscussWPerformingPhys" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strOrders.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrOrders.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strOrders.ToString()
                        'EGeneralItems.Add(oListItem)
                        arrOrders.Add(oListItem)
                        oListItem.Dispose()
                    End If
                Else
                    If nudXOtherXray.Value <> 0 Then
                        If strprop = "OtherXRaysCount" Then
                            'Emylist = New myList
                            'Emylist.AssociatedProperty = strprop
                            'Emylist.AssociatedCategory = strOrders.ToString()
                            'Emylist.AssociatedItem = nudXOtherXray.Value.ToString()
                            'arrOrders.Add(Emylist)

                            oListItem = New gloGeneralItem.gloItem
                            oListItem.Description = strprop
                            oListItem.Code = strOrders.ToString()
                            oListItem.Status = nudXOtherXray.Value.ToString()
                            'EGeneralItems.Add(oListItem)
                            arrOrders.Add(oListItem)
                            oListItem.Dispose()
                        End If
                    End If
                End If
            Next


            ''''Other Diagnosis
            arrOtherDiag = New ArrayList
            propertyInfos = GetType(AlphaII.CodeWizard.Objects.EvaluationManagement.MedComplexOtherDiagnosticTests).GetProperties(BindingFlags.CreateInstance Or BindingFlags.DeclaredOnly Or BindingFlags.Default Or BindingFlags.ExactBinding Or BindingFlags.FlattenHierarchy Or BindingFlags.GetField Or BindingFlags.GetProperty Or BindingFlags.IgnoreCase Or BindingFlags.IgnoreReturn Or BindingFlags.Instance Or BindingFlags.InvokeMethod Or BindingFlags.NonPublic Or BindingFlags.OptionalParamBinding Or BindingFlags.Public Or BindingFlags.PutDispProperty Or BindingFlags.PutRefDispProperty Or BindingFlags.SetField Or BindingFlags.SetProperty Or BindingFlags.Static Or BindingFlags.SuppressChangeType)
            For Each propertyInfo As PropertyInfo In propertyInfos
                strprop = propertyInfo.Name
                pType = propertyInfo.PropertyType
                If pType.Name = "Boolean" Then

                    If chkOEndoScopewRisk.Checked = True And strprop = "EndoscopeWRiskRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrOtherDiag.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strOtherDiagnosis.ToString()
                        arrOtherDiag.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkOEndoscopeworisk.Checked = True And strprop = "EndoscopeRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrOtherDiag.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strOtherDiagnosis.ToString()
                        arrOtherDiag.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkOCuldcentesis.Checked = True And strprop = "CuldocentesesRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrOtherDiag.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strOtherDiagnosis.ToString()
                        arrOtherDiag.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkOThoracentesis.Checked = True And strprop = "ThoracentesisRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrOtherDiag.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strOtherDiagnosis.ToString()
                        arrOtherDiag.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkOLumbarPunctor.Checked = True And strprop = "LumbarPunctureRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrOtherDiag.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strOtherDiagnosis.ToString()
                        arrOtherDiag.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkONuclearScan.Checked = True And strprop = "NuclearScanRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrOtherDiag.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strOtherDiagnosis.ToString()
                        arrOtherDiag.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkOPulmonary.Checked = True And strprop = "PulmonaryStudiesRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrOtherDiag.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strOtherDiagnosis.ToString()
                        arrOtherDiag.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkODopplerFlowStudies.Checked = True And strprop = "DopplerFlowStudiesRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrOtherDiag.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strOtherDiagnosis.ToString()
                        arrOtherDiag.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkOVectorCardiogram.Checked = True And strprop = "VectorcardiogramRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrOtherDiag.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strOtherDiagnosis.ToString()
                        arrOtherDiag.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkOEEG.Checked = True And strprop = "EegEmgRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrOtherDiag.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strOtherDiagnosis.ToString()
                        arrOtherDiag.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkOTreadmill.Checked = True And strprop = "TreadmillStressTestRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrOtherDiag.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strOtherDiagnosis.ToString()
                        arrOtherDiag.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkOHolterMonitor.Checked = True And strprop = "HolterMonitorRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrOtherDiag.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strOtherDiagnosis.ToString()
                        arrOtherDiag.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkOEKG.Checked = True And strprop = "EkgEcgRoutine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrOtherDiag.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strOtherDiagnosis.ToString()
                        arrOtherDiag.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkOIndependentVisualization.Checked = True And strprop = "IndependentVisualTest" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrOtherDiag.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strOtherDiagnosis.ToString()
                        arrOtherDiag.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkODiscuswithPerfoming.Checked = True And strprop = "DiscussWPerformingPhys" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrOtherDiag.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strOtherDiagnosis.ToString()
                        arrOtherDiag.Add(oListItem)
                        oListItem.Dispose()
                    End If
                Else
                    If nudODignosisstudies.Value <> 0 Then
                        If strprop = "OtherDiagnosticStudiesCount" Then
                            'Emylist = New myList
                            'Emylist.AssociatedProperty = strprop
                            'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                            'Emylist.AssociatedItem = nudODignosisstudies.Value.ToString()
                            'arrOtherDiag.Add(Emylist)

                            oListItem = New gloGeneralItem.gloItem
                            oListItem.Description = strprop
                            oListItem.Code = strOtherDiagnosis.ToString()
                            oListItem.Status = nudODignosisstudies.Value.ToString()
                            arrOtherDiag.Add(oListItem)
                            oListItem.Dispose()
                        End If
                    End If
                End If
            Next


            '''' Managment Option
            arrManagment = New ArrayList
            propertyInfos = GetType(AlphaII.CodeWizard.Objects.EvaluationManagement.MedComplexManagementOptions).GetProperties(BindingFlags.CreateInstance Or BindingFlags.DeclaredOnly Or BindingFlags.Default Or BindingFlags.ExactBinding Or BindingFlags.FlattenHierarchy Or BindingFlags.GetField Or BindingFlags.GetProperty Or BindingFlags.IgnoreCase Or BindingFlags.IgnoreReturn Or BindingFlags.Instance Or BindingFlags.InvokeMethod Or BindingFlags.NonPublic Or BindingFlags.OptionalParamBinding Or BindingFlags.Public Or BindingFlags.PutDispProperty Or BindingFlags.PutRefDispProperty Or BindingFlags.SetField Or BindingFlags.SetProperty Or BindingFlags.Static Or BindingFlags.SuppressChangeType)
            For Each propertyInfo As PropertyInfo In propertyInfos
                strprop = propertyInfo.Name
                pType = propertyInfo.PropertyType
                If pType.Name = "Boolean" Then
                    If chkMDecisionofcase.Checked = True And strprop = "DiscussCaseWHealthProvider" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strMangementOption.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrManagment.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strMangementOption.ToString()
                        arrManagment.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkMreviewandsummary.Checked = True And strprop = "ReviewMedicalRecsOther" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strMangementOption.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrManagment.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strMangementOption.ToString()
                        arrManagment.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkMDecisiontoobtain.Checked = True And strprop = "DecisionObtainMedicalRecsOther" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strMangementOption.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrManagment.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strMangementOption.ToString()
                        arrManagment.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkMDecisionnottoresuscitate.Checked = True And strprop = "DecisionNotResuscitate" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strMangementOption.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrManagment.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strMangementOption.ToString()
                        arrManagment.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkMMajoremergencySurgery.Checked = True And strprop = "MajorEmergencySurgery" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strMangementOption.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrManagment.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strMangementOption.ToString()
                        arrManagment.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkMMajorSurgerywrisk.Checked = True And strprop = "MajorSurgeryWRiskFactors" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strMangementOption.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrManagment.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strMangementOption.ToString()
                        arrManagment.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkMMajorsurgeryworisk.Checked = True And strprop = "MajorSurgery" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strMangementOption.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrManagment.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strMangementOption.ToString()
                        arrManagment.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkMMinorSurgeryWrisk.Checked = True And strprop = "MinorSurgeryWRiskFactors" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strMangementOption.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrManagment.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strMangementOption.ToString()
                        arrManagment.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkMminorsurgeryWOrisk.Checked = True And strprop = "MinorSurgery" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strMangementOption.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrManagment.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strMangementOption.ToString()
                        arrManagment.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkMClosefx.Checked = True And strprop = "ClosedFx" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strMangementOption.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrManagment.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strMangementOption.ToString()
                        arrManagment.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkMPhysicalOccupationaltherapy.Checked = True And strprop = "PhysicalTherapy" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strMangementOption.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrManagment.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strMangementOption.ToString()
                        arrManagment.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkMNuclearMedicine.Checked = True And strprop = "NuclearMedicine" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strMangementOption.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrManagment.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strMangementOption.ToString()
                        arrManagment.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkMRespiratoryTreatment.Checked = True And strprop = "RespiratoryTreatments" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strMangementOption.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrManagment.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strMangementOption.ToString()
                        arrManagment.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkMTelemetry.Checked = True And strprop = "Telemetry" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strMangementOption.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrManagment.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strMangementOption.ToString()
                        arrManagment.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkMHighRiskmeds.Checked = True And strprop = "HighRiskMeds" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strMangementOption.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrManagment.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strMangementOption.ToString()
                        arrManagment.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkMIVmedswadditives.Checked = True And strprop = "IVMedsWAdditives" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strMangementOption.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrManagment.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strMangementOption.ToString()
                        arrManagment.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkMivmeds.Checked = True And strprop = "IVMeds" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strMangementOption.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrManagment.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strMangementOption.ToString()
                        arrManagment.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkMPerscripmeds.Checked = True And strprop = "PrescripIMMeds" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strMangementOption.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrManagment.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strMangementOption.ToString()
                        arrManagment.Add(oListItem)
                        oListItem.Dispose()
                    End If
                    If chkMOTCmeds.Checked = True And strprop = "OverCounterMeds" Then
                        'Emylist = New myList
                        'Emylist.AssociatedProperty = strprop
                        'Emylist.AssociatedCategory = strMangementOption.ToString()
                        'Emylist.AssociatedItem = "True"
                        'arrManagment.Add(Emylist)

                        oListItem = New gloGeneralItem.gloItem
                        oListItem.Description = strprop
                        oListItem.Code = strMangementOption.ToString()
                        arrManagment.Add(oListItem)
                        oListItem.Dispose()
                    End If
                Else
                    If nudMTimeSpent.Value <> 0 Then
                        If strprop = "ConfWPatientFamilyMinutes" Then
                            'Emylist = New myList
                            'Emylist.AssociatedProperty = strprop
                            'Emylist.AssociatedCategory = strMangementOption.ToString()
                            'Emylist.AssociatedItem = nudMTimeSpent.Value.ToString()
                            'arrManagment.Add(Emylist)

                            oListItem = New gloGeneralItem.gloItem
                            oListItem.Description = strprop
                            oListItem.Code = strMangementOption.ToString()
                            oListItem.Status = nudMTimeSpent.Value.ToString()
                            arrManagment.Add(oListItem)
                            oListItem.Dispose()
                        End If
                    End If
                End If
            Next
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub




    Private Sub tb_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tb_Cancel.Click
        Me.Close()
    End Sub

    Private Sub frmEMTagAssociation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        Try
            ''Added by Mayuri:20100616
            ''To check whether form open from Labs,orders,flowsheet
            If FormName = "Labs" Then

                'tbEMAssociation.TabPages.Remove(tbpManagementOption)
                'tbEMAssociation.TabPages.Remove(tbpOtherDiagnosisTest)
                'tbEMAssociation.TabPages.Remove(tbpXRay)
                ' pnlLabsEMFields.Visible = False
                'Me.Size = New System.Drawing.Size(588, 425)
            ElseIf FormName = "Flowsheet" Then
                'tbEMAssociation.TabPages.Remove(tbpManagementOption)
                'chkLLCBC.Visible = False
                'pnlLabsEMFields.Visible = False
                'pnlOrdersEMFields.Visible = False
                'pnlOtherDiagnosticEMFields.Visible = False
                'Me.Size = New System.Drawing.Size(588, 425)
                'ADDED BY SHUBHANGI 20100617
            ElseIf FormName = "Orders" Then
                'tbEMAssociation.TabPages.Remove(tbpManagementOption)
                'tbEMAssociation.TabPages.Remove(tbpLabs)

                'pnlLabsEMFields.Visible = False
                'pnlOrdersEMFields.Visible = False
                'pnlOtherDiagnosticEMFields.Visible = False
                'Me.Size = New System.Drawing.Size(588, 425)
            End If
            ''Labs
            Dim strAssociatedProperty As String = ""
            Dim strAssociatedCategory As String = ""
            Dim strItem As String = ""
            'Dim item As Int16
            For i As Integer = 0 To arrLabs.Count - 1

                'strAssociatedCategory = CType(arrLabs.Item(i), myList).AssociatedCategory
                'strAssociatedProperty = CType(arrLabs.Item(i), myList).AssociatedProperty
                'strItem = CType(arrLabs.Item(i), myList).AssociatedItem
                ''Added by Mayuri:20100617-Used glgenealitem for collection as it is accesible in clsgoEMRLab also
                strAssociatedCategory = CType(arrLabs.Item(i), gloGeneralItem.gloItem).Code
                strAssociatedProperty = CType(arrLabs.Item(i), gloGeneralItem.gloItem).Description
                strItem = CType(arrLabs.Item(i), gloGeneralItem.gloItem).Status
                'item = CType(arrLabs.Item(i), gloGeneralItem.gloItem).ID

                If strAssociatedProperty = "IncisionalBiopsyRoutine" And strAssociatedCategory = strLabs.ToString() Then
                    chkLDeepIncisionalBiopsy.Checked = True

                End If
                If strAssociatedProperty = "SuperficialBiopsyRoutine" And strAssociatedCategory = strLabs.ToString() Then

                    chkLSuperficialbiopsy.Checked = True

                End If
                If strAssociatedProperty = "TypeCrossmatchRoutine" And strAssociatedCategory = strLabs.ToString() Then
                    chkLTypesandCrossmatch.Checked = True

                End If

                If strAssociatedProperty = "PTRoutine" And strAssociatedCategory = strLabs.ToString() Then
                    chkLPT.Checked = True

                End If

                If strAssociatedProperty = "ABGsRoutine" And strAssociatedCategory = strLabs.ToString() Then
                    chkLABGS.Checked = True

                End If
                If strAssociatedProperty = "CardiacEnzymesRoutine" And strAssociatedCategory = strLabs.ToString() Then
                    chkLCardiacenzymes.Checked = True

                End If
                If strAssociatedProperty = "ChemicalProfileRoutine" And strAssociatedCategory = strLabs.ToString() Then
                    chkLChemicalProfile.Checked = True

                End If
                If strAssociatedProperty = "DrugScreenRoutine" And strAssociatedCategory = strLabs.ToString() Then
                    chkLETOH.Checked = True

                End If
                If strAssociatedProperty = "ElectrolytesRoutine" And strAssociatedCategory = strLabs.ToString() Then
                    chkLElectrolytes.Checked = True

                End If

                If strAssociatedProperty = "BunCreatinineRoutine" And strAssociatedCategory = strLabs.ToString() Then
                    chkLBun.Checked = True

                End If
                If strAssociatedProperty = "AmylaseRoutine" And strAssociatedCategory = strLabs.ToString() Then
                    chkLAmylase.Checked = True

                End If

                If strAssociatedProperty = "PregnancyTestRoutine" And strAssociatedCategory = strLabs.ToString() Then
                    chkLPregnancyTest.Checked = True

                End If
                If strAssociatedProperty = "FluStrepMonoRoutine" And strAssociatedCategory = strLabs.ToString() Then
                    chkLFlu.Checked = True

                End If

                If strAssociatedProperty = "CbcUaRoutine" And strAssociatedCategory = strLabs.ToString() Then
                    chkLLCBC.Checked = True

                End If
                If strAssociatedProperty = "IndependentVisualTest" And strAssociatedCategory = strLabs.ToString() Then
                    chkLIndependentVisualizationoftest.Checked = True

                End If
                If strAssociatedProperty = "DiscussionWPerformingPhys" And strAssociatedCategory = strLabs.ToString() Then
                    chkLDiscussionwithperformingPhysician.Checked = True

                End If

                If strAssociatedProperty = "OtherLabsCount" And strAssociatedCategory = strLabs.ToString() Then
                    nudLOtherLabs.Value = Convert.ToInt16(strItem)
                End If

            Next


            For i As Integer = 0 To arrOrders.Count - 1
                'strAssociatedCategory = CType(arrOrders.Item(i), myList).AssociatedCategory
                'strAssociatedProperty = CType(arrOrders.Item(i), myList).AssociatedProperty
                'strItem = CType(arrOrders.Item(i), myList).AssociatedItem

                strAssociatedCategory = CType(arrOrders.Item(i), gloGeneralItem.gloItem).Code
                strAssociatedProperty = CType(arrOrders.Item(i), gloGeneralItem.gloItem).Description
                strItem = CType(arrOrders.Item(i), gloGeneralItem.gloItem).Status

                If strAssociatedProperty = "VascularStudiesWRiskRoutine" And strAssociatedCategory = strOrders.ToString() Then
                    chkXVascularStudieswrisk.Checked = True

                End If
                If strAssociatedProperty = "VascularStudiesRoutine" And strAssociatedCategory = strOrders.ToString() Then
                    chkXVascularStudies.Checked = True

                End If

                If strAssociatedProperty = "MRIRoutine" And strAssociatedCategory = strOrders.ToString() Then
                    chkXMRI.Checked = True

                End If
                If strAssociatedProperty = "CATScanRoutine" And strAssociatedCategory = strOrders.ToString() Then
                    chkXcatScan.Checked = True

                End If
                If strAssociatedProperty = "IVPRoutine" And strAssociatedCategory = strOrders.ToString() Then
                    chkXIVP.Checked = True

                End If

                If strAssociatedProperty = "GIGallbladderRoutine" And strAssociatedCategory = strOrders.ToString() Then
                    chkXGIGallablader.Checked = True

                End If
                If strAssociatedProperty = "TLSpineRoutine" And strAssociatedCategory = strOrders.ToString() Then
                    chkXTLSpire.Checked = True

                End If
                If strAssociatedProperty = "DiscographyRoutine" And strAssociatedCategory = strOrders.ToString() Then
                    chkXDiscographt.Checked = True

                End If
                If strAssociatedProperty = "DiagUltrasoundRoutine" And strAssociatedCategory = strOrders.ToString() Then
                    chkXDiagosticUltrasound.Checked = True

                End If
                If strAssociatedProperty = "CSpineRoutine" And strAssociatedCategory = strOrders.ToString() Then
                    chkXCspine.Checked = True

                End If
                If strAssociatedProperty = "HipPelvisRoutine" And strAssociatedCategory = strOrders.ToString() Then
                    chkXHipPelvis.Checked = True

                End If
                If strAssociatedProperty = "AbdomenRoutine" And strAssociatedCategory = strOrders.ToString() Then
                    chkXAbdomen.Checked = True

                End If
                If strAssociatedProperty = "ExtremitiesRoutine" And strAssociatedCategory = strOrders.ToString() Then
                    chkXExtrimities.Checked = True

                End If
                If strAssociatedProperty = "ChestRoutine" And strAssociatedCategory = strOrders.ToString() Then
                    chkXChest.Checked = True

                End If
                If strAssociatedProperty = "IndependentVisualTest" And strAssociatedCategory = strOrders.ToString() Then
                    chkXIndepedent.Checked = True

                End If
                If strAssociatedProperty = "DiscussWPerformingPhys" And strAssociatedCategory = strOrders.ToString() Then
                    chkXperformingPhy.Checked = True

                End If
                If strAssociatedProperty = "OtherXRaysCount" And strAssociatedCategory = strOrders.ToString() Then
                    nudXOtherXray.Value = Convert.ToInt16(strItem)
                End If

            Next

            ''''Other Diagnosis
            For i As Integer = 0 To arrOtherDiag.Count - 1

                'strAssociatedCategory = CType(arrOtherDiag.Item(i), myList).AssociatedCategory
                'strAssociatedProperty = CType(arrOtherDiag.Item(i), myList).AssociatedProperty
                'strItem = CType(arrOtherDiag.Item(i), myList).AssociatedItem

                strAssociatedCategory = CType(arrOtherDiag.Item(i), gloGeneralItem.gloItem).Code
                strAssociatedProperty = CType(arrOtherDiag.Item(i), gloGeneralItem.gloItem).Description
                strItem = CType(arrOtherDiag.Item(i), gloGeneralItem.gloItem).Status


                If strAssociatedProperty = "EndoscopeWRiskRoutine" And strAssociatedCategory = strOtherDiagnosis.ToString() Then
                    chkOEndoScopewRisk.Checked = True

                End If
                If strAssociatedProperty = "EndoscopeRoutine" And strAssociatedCategory = strOtherDiagnosis.ToString() Then
                    chkOEndoscopeworisk.Checked = True

                End If
                If strAssociatedProperty = "CuldocentesesRoutine" And strAssociatedCategory = strOtherDiagnosis.ToString() Then
                    chkOCuldcentesis.Checked = True

                End If
                If strAssociatedProperty = "ThoracentesisRoutine" And strAssociatedCategory = strOtherDiagnosis.ToString() Then
                    chkOThoracentesis.Checked = True

                End If
                If strAssociatedProperty = "LumbarPunctureRoutine" And strAssociatedCategory = strOtherDiagnosis.ToString() Then
                    chkOLumbarPunctor.Checked = True

                End If
                If strAssociatedProperty = "NuclearScanRoutine" And strAssociatedCategory = strOtherDiagnosis.ToString() Then
                    chkONuclearScan.Checked = True

                End If
                If strAssociatedProperty = "PulmonaryStudiesRoutine" And strAssociatedCategory = strOtherDiagnosis.ToString() Then
                    chkOPulmonary.Checked = True

                End If
                If strAssociatedProperty = "DopplerFlowStudiesRoutine" And strAssociatedCategory = strOtherDiagnosis.ToString() Then
                    chkODopplerFlowStudies.Checked = True

                End If
                If strAssociatedProperty = "VectorcardiogramRoutine" And strAssociatedCategory = strOtherDiagnosis.ToString() Then
                    chkOVectorCardiogram.Checked = True

                End If
                If strAssociatedProperty = "EegEmgRoutine" And strAssociatedCategory = strOtherDiagnosis.ToString() Then
                    chkOEEG.Checked = True

                End If
                If strAssociatedProperty = "TreadmillStressTestRoutine" And strAssociatedCategory = strOtherDiagnosis.ToString() Then
                    chkOTreadmill.Checked = True

                End If
                If strAssociatedProperty = "HolterMonitorRoutine" And strAssociatedCategory = strOtherDiagnosis.ToString() Then
                    chkOHolterMonitor.Checked = True

                End If
                If strAssociatedProperty = "EkgEcgRoutine" And strAssociatedCategory = strOtherDiagnosis.ToString() Then
                    chkOEKG.Checked = True

                End If
                If strAssociatedProperty = "IndependentVisualTest" And strAssociatedCategory = strOtherDiagnosis.ToString() Then
                    chkOIndependentVisualization.Checked = True

                End If
                If strAssociatedProperty = "DiscussWPerformingPhys" And strAssociatedCategory = strOtherDiagnosis.ToString() Then
                    chkODiscuswithPerfoming.Checked = True

                End If

                If strAssociatedProperty = "OtherDiagnosticStudiesCount" And strAssociatedCategory = strOtherDiagnosis.ToString() Then
                    nudODignosisstudies.Value = Convert.ToInt16(strItem)
                End If

            Next



            '''' Managment Option
            For i As Integer = 0 To arrManagment.Count - 1
                'strAssociatedCategory = CType(arrManagment.Item(i), myList).AssociatedCategory
                'strAssociatedProperty = CType(arrManagment.Item(i), myList).AssociatedProperty
                'strItem = CType(arrManagment.Item(i), myList).AssociatedItem

                strAssociatedCategory = CType(arrManagment.Item(i), gloGeneralItem.gloItem).Code
                strAssociatedProperty = CType(arrManagment.Item(i), gloGeneralItem.gloItem).Description
                strItem = CType(arrManagment.Item(i), gloGeneralItem.gloItem).Status


                If strAssociatedProperty = "DiscussCaseWHealthProvider" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMDecisionofcase.Checked = True
                End If
                If strAssociatedProperty = "ReviewMedicalRecsOther" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMreviewandsummary.Checked = True

                End If
                If strAssociatedProperty = "DecisionObtainMedicalRecsOther" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMDecisiontoobtain.Checked = True

                End If
                If strAssociatedProperty = "DecisionNotResuscitate" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMDecisionnottoresuscitate.Checked = True

                End If
                If strAssociatedProperty = "MajorEmergencySurgery" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMMajoremergencySurgery.Checked = True

                End If
                If strAssociatedProperty = "MajorSurgeryWRiskFactors" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMMajorSurgerywrisk.Checked = True

                End If
                If strAssociatedProperty = "MajorSurgery" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMMajorsurgeryworisk.Checked = True

                End If
                If strAssociatedProperty = "MinorSurgeryWRiskFactors" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMMinorSurgeryWrisk.Checked = True

                End If
                If strAssociatedProperty = "MinorSurgery" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMminorsurgeryWOrisk.Checked = True

                End If
                If strAssociatedProperty = "ClosedFx" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMClosefx.Checked = True

                End If
                If strAssociatedProperty = "PhysicalTherapy" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMPhysicalOccupationaltherapy.Checked = True

                End If
                If strAssociatedProperty = "NuclearMedicine" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMNuclearMedicine.Checked = True

                End If
                If strAssociatedProperty = "RespiratoryTreatments" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMRespiratoryTreatment.Checked = True

                End If
                If strAssociatedProperty = "Telemetry" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMTelemetry.Checked = True

                End If
                If strAssociatedProperty = "HighRiskMeds" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMHighRiskmeds.Checked = True

                End If
                If strAssociatedProperty = "IVMedsWAdditives" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMIVmedswadditives.Checked = True

                End If
                If strAssociatedProperty = "IVMeds" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMivmeds.Checked = True

                End If
                If strAssociatedProperty = "PrescripIMMeds" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMPerscripmeds.Checked = True

                End If
                If strAssociatedProperty = "OverCounterMeds" And strAssociatedCategory = strMangementOption.ToString() Then
                    chkMOTCmeds.Checked = True

                End If

                If strAssociatedProperty = "ConfWPatientFamilyMinutes" And strAssociatedCategory = strMangementOption.ToString() Then
                    nudMTimeSpent.Value = Convert.ToInt16(strItem)
                End If
            Next
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Public Sub New(ByVal _arrLabs As ArrayList, Optional ByVal _arrOrders As ArrayList = Nothing, Optional ByVal _arrOtherDiag As ArrayList = Nothing, Optional ByVal _arrManagment As ArrayList = Nothing, Optional ByVal _FormName As String = "")

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        arrLabs = _arrLabs
        arrOrders = _arrOrders
        arrOtherDiag = _arrOtherDiag
        arrManagment = _arrManagment
        FormName = _FormName
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub tb_Refresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tb_Refresh.Click

        If tbEMAssociation.SelectedTab.Name = tbpManagementOption.Name Then
            ResetManagementCheckBox()
        ElseIf tbEMAssociation.SelectedTab.Name = tbpLabs.Name Then
            ResetLabsCheckBox()
        ElseIf tbEMAssociation.SelectedTab.Name = tbpXRay.Name Then
            ResetXRayCheckBox()
        ElseIf tbEMAssociation.SelectedTab.Name = tbpOtherDiagnosisTest.Name Then
            ResetOtherDiagnosisTestCheckBox()
        End If
    End Sub

    Public Function ResetManagementCheckBox() As Boolean
        Try
            'Uncheck all check boxes within management tab page
            chkMDecisionofcase.Checked = False
            chkMreviewandsummary.Checked = False
            chkMDecisiontoobtain.Checked = False
            chkMDecisionnottoresuscitate.Checked = False
            chkMMajoremergencySurgery.Checked = False
            chkMMajorSurgerywrisk.Checked = False
            chkMMajorsurgeryworisk.Checked = False
            chkMMinorSurgeryWrisk.Checked = False
            chkMminorsurgeryWOrisk.Checked = False
            chkMClosefx.Checked = False
            chkMPhysicalOccupationaltherapy.Checked = False
            chkMNuclearMedicine.Checked = False
            chkMRespiratoryTreatment.Checked = False
            chkMTelemetry.Checked = False
            chkMHighRiskmeds.Checked = False
            chkMIVmedswadditives.Checked = False
            chkMivmeds.Checked = False
            chkMPerscripmeds.Checked = False
            chkMOTCmeds.Checked = False

            'Set value of numeric up-down to zero
            nudMTimeSpent.Value = 0
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        Return Nothing
    End Function
    Public Function ResetLabsCheckBox() As Boolean
        Try
            'Uncheck all Check Boxes within Labs Tab Page
            chkLABGS.Checked = False
            chkLDeepIncisionalBiopsy.Checked = False
            chkLSuperficialbiopsy.Checked = False
            chkLTypesandCrossmatch.Checked = False
            chkLPT.Checked = False
            chkLABGS.Checked = False
            chkLCardiacenzymes.Checked = False
            chkLETOH.Checked = False
            chkLElectrolytes.Checked = False
            chkLBun.Checked = False
            chkLAmylase.Checked = False
            chkLPregnancyTest.Checked = False
            chkLFlu.Checked = False
            chkLLCBC.Checked = False
            chkLIndependentVisualizationoftest.Checked = False
            chkLDiscussionwithperformingPhysician.Checked = False
            chkLChemicalProfile.Checked = False

            'Set value of numeric up-down to zero
            nudLOtherLabs.Value = 0
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        Return Nothing
    End Function
    Public Function ResetXRayCheckBox() As Boolean
        Try
            'Uncheck all check boxes within XRay Tab Page
            chkXAbdomen.Checked = False
            chkXVascularStudieswrisk.Checked = False
            chkXVascularStudies.Checked = False
            chkXMRI.Checked = False
            chkXcatScan.Checked = False
            chkXIVP.Checked = False
            chkXGIGallablader.Checked = False
            chkXTLSpire.Checked = False
            chkXDiscographt.Checked = False
            chkXDiagosticUltrasound.Checked = False
            chkXCspine.Checked = False
            chkXHipPelvis.Checked = False
            chkXExtrimities.Checked = False
            chkXChest.Checked = False
            chkXIndepedent.Checked = False
            chkXperformingPhy.Checked = False

            'Set value of numeric up-down to zero
            nudXOtherXray.Value = 0
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        Return Nothing
    End Function
    Public Function ResetOtherDiagnosisTestCheckBox() As Boolean
        Try
            'Reset all check boxes eithin Other Diagnostic test
            chkOEndoScopewRisk.Checked = False
            chkOEndoscopeworisk.Checked = False
            chkOCuldcentesis.Checked = False
            chkOThoracentesis.Checked = False
            chkOLumbarPunctor.Checked = False
            chkONuclearScan.Checked = False
            chkOPulmonary.Checked = False
            chkODopplerFlowStudies.Checked = False
            chkOVectorCardiogram.Checked = False
            chkOEEG.Checked = False
            chkOTreadmill.Checked = False
            chkOHolterMonitor.Checked = False
            chkOEKG.Checked = False
            chkODiscuswithPerfoming.Checked = False
            chkODopplerFlowStudies.Checked = False
            chkOIndependentVisualization.Checked = False
            'Set value of numeric up-down to zero
            nudODignosisstudies.Value = 0
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        Return Nothing
    End Function

   
   
End Class