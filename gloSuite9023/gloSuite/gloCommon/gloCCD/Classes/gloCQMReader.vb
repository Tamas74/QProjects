Imports gloEMeasure
Public Class gloCQMReader
    '  Dim dtFinal As New DataTable
    Dim _CMsID As String = ""
    Dim _CMSIDNumber As Int16
    Dim dtPlanDetails As DataTable
    Dim dtFinal As DataTable
    Public Function ExtractCQMEmeasure(ByVal strCCDFilePath As String, ByVal nproviderid As Int64, ByVal _dtFinal As DataTable, ByVal CMSID As String, ByVal _dtPlanDetails As DataTable) As ReconcileList

        Dim oCQMSchema As POQM_MT000001UV03QualityMeasureDocument = Nothing
        Dim dtDataCriteria As DataTable = Nothing

        Try
            _CMsID = CMSID
            'dtDataCriteria = New DataTable()
            dtPlanDetails = _dtPlanDetails
            dtFinal = _dtFinal
            'dtFinal.Columns.Add("Root")
            'dtFinal.Columns.Add("Extension")
            'dtFinal.Columns.Add("ValuSet")
            'dtFinal.Columns.Add("Title")
            'dtFinal.Columns.Add("Criteria")
            Dim DataCriteria As POQM_MT000001UV03DataCriteriaSection = Nothing
            Dim popcriteriasection As gloEMeasure.POQM_MT000001UV03PopulationCriteriaSection = Nothing
            oCQMSchema = gloCCDSchema.gloSerialization.GetCQMData(strCCDFilePath)
            If Not IsNothing(oCQMSchema.subjectOf) Then
                If oCQMSchema.subjectOf.Length > 0 Then
                    For i As Integer = 0 To oCQMSchema.subjectOf.Length - 1
                        If Not IsNothing(oCQMSchema.subjectOf(i).measureAttribute) Then
                            If Not IsNothing(oCQMSchema.subjectOf(i).measureAttribute.code) Then
                                If Not IsNothing(oCQMSchema.subjectOf(i).measureAttribute.code.originalText) Then
                                    If Convert.ToString(oCQMSchema.subjectOf(i).measureAttribute.code.originalText.value) = "eMeasure Identifier (Measure Authoring Tool)" Then
                                        If Not IsNothing(oCQMSchema.subjectOf(i).measureAttribute.value) Then
                                            _CMSIDNumber = DirectCast(oCQMSchema.subjectOf(i).measureAttribute.value, gloEMeasure.ED).value
                                            Exit For
                                        End If

                                    End If
                                End If

                            End If



                        End If
                    Next


                End If
            End If
            If Not IsNothing(oCQMSchema.component) Then
                If oCQMSchema.component.Length > 0 Then
                    For i As Integer = 0 To oCQMSchema.component.Length - 1
                        If Not IsNothing(oCQMSchema.component(i).Item) Then
                            If Convert.ToString(oCQMSchema.component(i).Item) = "gloEMeasure.POQM_MT000001UV03DataCriteriaSection" Then

                                DataCriteria = DirectCast(oCQMSchema.component(i).Item, gloEMeasure.POQM_MT000001UV03DataCriteriaSection)
                                dtDataCriteria = GetDataCriteriaDetails(DataCriteria)


                            End If



                            If Convert.ToString(oCQMSchema.component(i).Item) = "gloEMeasure.POQM_MT000001UV03PopulationCriteriaSection" Then

                                If Not IsNothing(DirectCast(oCQMSchema.component(i).Item, gloEMeasure.POQM_MT000001UV03PopulationCriteriaSection).component) Then
                                    If DirectCast(oCQMSchema.component(i).Item, gloEMeasure.POQM_MT000001UV03PopulationCriteriaSection).component.Length > 0 Then
                                        popcriteriasection = DirectCast(oCQMSchema.component(i).Item, gloEMeasure.POQM_MT000001UV03PopulationCriteriaSection)
                                        GetPopulationCriteria(popcriteriasection, dtDataCriteria, dtFinal)
                                        ReadAgeCriteria(DataCriteria)
                                    End If
                                End If

                            End If
                        End If
                    Next


                End If

            End If


        Catch ex As Exception
            '  oReconcileList = Nothing
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            ex = Nothing
        Finally
            If Not IsNothing(dtDataCriteria) Then
                dtDataCriteria.Dispose()
                dtDataCriteria = Nothing
            End If
        End Try
        Return Nothing
        ' Return oReconcileList

    End Function
    Private Sub GetPopulationCriteria(ByVal popcriteriasection As POQM_MT000001UV03PopulationCriteriaSection, ByVal dtDataCriteria As DataTable, ByVal dtFinal As DataTable)

        If Not IsNothing(popcriteriasection.component) Then
            If popcriteriasection.component.Length > 0 Then

                For j As Integer = 0 To popcriteriasection.component.Length - 1
                    Dim InitialPop As POQM_MT000001UV03InitialPopulationCriteria = Nothing
                    Dim DenomCriteria As POQM_MT000001UV03DenominatorCriteria = Nothing
                    Dim NumeratorCriteria As POQM_MT000001UV03NumeratorCriteria = Nothing
                    If Not IsNothing(popcriteriasection.component(j)) Then


                        If Convert.ToString(popcriteriasection.component(j).Item) = "gloEMeasure.POQM_MT000001UV03InitialPopulationCriteria" Then

                            ReadAllCriterias(popcriteriasection.component(j).Item, dtDataCriteria, dtFinal, "Initial Population")

                        ElseIf Convert.ToString(popcriteriasection.component(j).Item) = "gloEMeasure.POQM_MT000001UV03DenominatorCriteria" Then

                            ReadAllCriterias(popcriteriasection.component(j).Item, dtDataCriteria, dtFinal, "Denominator")
                        ElseIf Convert.ToString(popcriteriasection.component(j).Item) = "gloEMeasure.POQM_MT000001UV03NumeratorCriteria" Then

                            ReadAllCriterias(popcriteriasection.component(j).Item, dtDataCriteria, dtFinal, "Numerator")
                        End If
                    End If
                    InitialPop = Nothing
                    DenomCriteria = Nothing
                    NumeratorCriteria = Nothing
                Next
            End If
        End If

        popcriteriasection = Nothing
    End Sub

    Private Sub ReadAllCriterias(ByVal criteria As Object, ByVal dtDataCriteria As DataTable, ByVal dtFinal As DataTable, ByVal Type As String)
        If Not IsNothing(criteria.precondition) Then
            If criteria.precondition.Length > 0 Then
                For i As Integer = 0 To criteria.precondition.Length - 1

                    If Convert.ToString(criteria.precondition(i).Item) = "gloEMeasure.POQM_MT000001UV03CriteriaReference" Then
                        Dim precon As POQM_MT000001UV03CriteriaReference = Nothing
                        precon = DirectCast(criteria.precondition(i).Item, gloEMeasure.POQM_MT000001UV03CriteriaReference)
                        ReadCriteria(precon, dtDataCriteria, dtFinal, Type)
                    Else
                        ReadFinal(criteria.precondition(i).Item, dtDataCriteria, dtFinal, Type)
                        '  ReadDetails(criteria.precondition(i).Item, dtDataCriteria, dtFinal, Type)

                    End If

                Next
            End If
        End If
    End Sub


    Private Sub ReadCriteria(ByVal precon As POQM_MT000001UV03CriteriaReference, ByVal dtDataCriteria As DataTable, ByVal dtFinal As DataTable, ByVal Type As String)
        If Not IsNothing(precon) Then
            If Not IsNothing(precon.id) Then
                GetPopulationCategoryDetails(dtDataCriteria, Convert.ToString(precon.id.root), dtFinal, Type)

            End If
        End If
    End Sub
    'Private Sub ReadDenominator(ByVal DenomCriteria As POQM_MT000001UV03DenominatorCriteria, ByVal dtDataCriteria As DataTable, dtFinal As DataTable)
    '    If Not IsNothing(DenomCriteria.precondition) Then
    '        If DenomCriteria.precondition.Length > 0 Then
    '            For m As Integer = 0 To DenomCriteria.precondition.Length - 1
    '                Dim precon As POQM_MT000001UV03CriteriaReference = Nothing
    '                If Convert.ToString(DenomCriteria.precondition(m).Item) = "gloEMeasure.POQM_MT000001UV03CriteriaReference" Then
    '                    ReadCriteria(precon, dtDataCriteria, dtFinal, "Denominator")
    '                    'precon = DirectCast(DenomCriteria.precondition(m).Item, gloEMeasure.POQM_MT000001UV03CriteriaReference)
    '                    'If Not IsNothing(precon) Then
    '                    '    If Not IsNothing(precon.id) Then
    '                    '        GetPopulationCategoryDetails(dtDataCriteria, Convert.ToString(precon.id.root), dtFinal, "Denominator")

    '                    '    End If
    '                    'End If
    '                ElseIf Convert.ToString(DenomCriteria.precondition(m).Item) = "gloEMeasure.POQM_MT000001UV03AtLeastOneTrue" Then
    '                    Dim precon1 As POQM_MT000001UV03AtLeastOneTrue = Nothing
    '                    precon1 = DirectCast(DenomCriteria.precondition(m).Item, gloEMeasure.POQM_MT000001UV03AtLeastOneTrue)
    '                    ReadAtleastOneTrue(precon1, dtDataCriteria, dtFinal, "Denominator")
    '                    'If Not IsNothing(precon1) Then
    '                    '    If Not IsNothing(precon1.precondition) Then
    '                    '        If precon1.precondition.Length > 0 Then
    '                    '            For k As Integer = 0 To precon1.precondition.Length - 1
    '                    '                If Not IsNothing(precon1.precondition(k).Item) Then

    '                    '                    If Convert.ToString(precon1.precondition(k).Item) = "gloEMeasure.POQM_MT000001UV03CriteriaReference" Then
    '                    '                        Dim precon2 As POQM_MT000001UV03CriteriaReference = Nothing
    '                    '                        precon2 = DirectCast(precon1.precondition(k).Item, gloEMeasure.POQM_MT000001UV03CriteriaReference)
    '                    '                        If Not IsNothing(precon2) Then
    '                    '                            If Not IsNothing(precon2.id) Then
    '                    '                                GetPopulationCategoryDetails(dtDataCriteria, Convert.ToString(precon2.id.root), dtFinal, "Denominator")

    '                    '                            End If
    '                    '                        End If

    '                    '                    End If
    '                    '                End If
    '                    '            Next
    '                    '        End If

    '                    '    End If

    '                    'End If
    '                End If
    '            Next
    '        End If
    '    End If
    'End Sub
    'Private Sub ReadNumerator(ByVal NumeratorCriteria As POQM_MT000001UV03NumeratorCriteria, ByVal dtDataCriteria As DataTable, dtFinal As DataTable)
    '    If Not IsNothing(NumeratorCriteria.precondition) Then
    '        If NumeratorCriteria.precondition.Length > 0 Then
    '            For i As Integer = 0 To NumeratorCriteria.precondition.Length - 1

    '                If Convert.ToString(NumeratorCriteria.precondition(i).Item) = "gloEMeasure.POQM_MT000001UV03CriteriaReference" Then
    '                    Dim precon As POQM_MT000001UV03CriteriaReference = Nothing
    '                    precon = DirectCast(NumeratorCriteria.precondition(i).Item, gloEMeasure.POQM_MT000001UV03CriteriaReference)

    '                    ReadCriteria(precon, dtDataCriteria, dtFinal, "Numerator")
    '                ElseIf Convert.ToString(NumeratorCriteria.precondition(i).Item) = "gloEMeasure.POQM_MT000001UV03AtLeastOneTrue" Then
    '                    Dim precon As POQM_MT000001UV03AtLeastOneTrue = Nothing
    '                    precon = DirectCast(NumeratorCriteria.precondition(i).Item, gloEMeasure.POQM_MT000001UV03AtLeastOneTrue)
    '                    ReadAtleastOneTrue(precon, dtDataCriteria, dtFinal, "Numerator")


    '                ElseIf Convert.ToString(NumeratorCriteria.precondition(i).Item) = "gloEMeasure.POQM_MT000001UV03AtLeastOneFalse" Then
    '                    Dim precon As POQM_MT000001UV03AtLeastOneFalse = Nothing
    '                    precon = DirectCast(NumeratorCriteria.precondition(i).Item, gloEMeasure.POQM_MT000001UV03AtLeastOneFalse)
    '                    ReadAtleastOneFalse(precon, dtDataCriteria, dtFinal, "Numerator")


    '                ElseIf Convert.ToString(NumeratorCriteria.precondition(i).Item) = "gloEMeasure.POQM_MT000001UV03AllTrue" Then
    '                    Dim precon As POQM_MT000001UV03AllTrue = Nothing
    '                    precon = DirectCast(NumeratorCriteria.precondition(i).Item, gloEMeasure.POQM_MT000001UV03AllTrue)
    '                    ReadAllTrue(precon, dtDataCriteria, dtFinal, "Numerator")

    '                ElseIf Convert.ToString(NumeratorCriteria.precondition(i).Item) = "gloEMeasure.POQM_MT000001UV03AllFalse" Then
    '                    Dim precon As POQM_MT000001UV03AllFalse = Nothing
    '                    precon = DirectCast(NumeratorCriteria.precondition(i).Item, gloEMeasure.POQM_MT000001UV03AllFalse)
    '                    ReadAllFalse(precon, dtDataCriteria, dtFinal, "Numerator")


    '                ElseIf Convert.ToString(NumeratorCriteria.precondition(i).Item) = "gloEMeasure.POQM_MT000001UV03OnlyOneTrue" Then
    '                    Dim precon As POQM_MT000001UV03OnlyOneTrue = Nothing
    '                    precon = DirectCast(NumeratorCriteria.precondition(i).Item, gloEMeasure.POQM_MT000001UV03OnlyOneTrue)
    '                    ReadOnlyOneTrue(precon, dtDataCriteria, dtFinal, "Numerator")

    '                ElseIf Convert.ToString(NumeratorCriteria.precondition(i).Item) = "gloEMeasure.POQM_MT000001UV03OnlyOneFalse" Then
    '                    Dim precon As POQM_MT000001UV03OnlyOneFalse = Nothing
    '                    precon = DirectCast(NumeratorCriteria.precondition(i).Item, gloEMeasure.POQM_MT000001UV03OnlyOneFalse)
    '                    ReadOnlyOneFalse(precon, dtDataCriteria, dtFinal, "Numerator")

    '                End If

    '            Next
    '        End If
    '    End If
    'End Sub
    'Private Sub ReadAllFalse(ByVal preall As POQM_MT000001UV03AllTrue, ByVal dtDataCriteria As DataTable, ByVal dtFinal As DataTable)

    '    ' Dim precon As POQM_MT000001UV03AllFalse = Nothing
    '    ' precon = preall.precondition(index).Item
    '    If Not IsNothing(preall) Then
    '        If Not IsNothing(preall.precondition) Then
    '            If preall.precondition.Length > 0 Then
    '                For i As Integer = 0 To preall.precondition.Length - 1
    '                    If Convert.ToString(preall.precondition(i).Item) = "gloEMeasure.POQM_MT000001UV03CriteriaReference" Then
    '                        If Not IsNothing(preall.precondition(i).Item) Then
    '                            Dim itemcriteria As POQM_MT000001UV03CriteriaReference = DirectCast(preall.precondition(i).Item, gloEMeasure.POQM_MT000001UV03CriteriaReference)
    '                            If Not IsNothing(itemcriteria) Then
    '                                If Not IsNothing(itemcriteria.id) Then
    '                                    GetPopulationCategoryDetails(dtDataCriteria, Convert.ToString(itemcriteria.id.root), dtFinal, "Numerator")

    '                                End If
    '                            End If
    '                        End If
    '                    End If
    '                Next
    '            End If
    '        End If
    '    End If
    'End Sub
    'Private Sub ReadAtleastOneFalse(ByVal precon As Object, ByVal dtDataCriteria As DataTable, ByVal dtFinal As DataTable, ByVal Type As String)

    '    ' Dim precon As POQM_MT000001UV03AtLeastOneFalse = Nothing
    '    'precon = DirectCast(NumeratorCriteria.precondition(i).Item, gloEMeasure.POQM_MT000001UV03AtLeastOneFalse)
    '    If Not IsNothing(precon) Then
    '        If Not IsNothing(precon.precondition) Then
    '            If precon.precondition.Length > 0 Then
    '                For k As Integer = 0 To precon.precondition.Length - 1
    '                    If Not IsNothing(precon.precondition(k).Item) Then

    '                        If Convert.ToString(precon.precondition(k).Item) = "gloEMeasure.POQM_MT000001UV03CriteriaReference" Then
    '                            Dim precon1 As POQM_MT000001UV03CriteriaReference = Nothing
    '                            precon1 = DirectCast(precon.precondition(k).Item, gloEMeasure.POQM_MT000001UV03CriteriaReference)
    '                            ReadCriteria(precon1, dtDataCriteria, dtFinal, Type)
    '                            'If Not IsNothing(precon1) Then
    '                            '    If Not IsNothing(precon1.id) Then
    '                            '        GetPopulationCategoryDetails(dtDataCriteria, Convert.ToString(precon1.id.root), dtFinal, Type)

    '                            '    End If
    '                            'End If

    '                        End If
    '                    End If
    '                Next
    '            End If

    '        End If

    '    End If
    'End Sub
    'Private Sub ReadAtleastOneTrue(ByVal precon As POQM_MT000001UV03AtLeastOneTrue, ByVal dtDataCriteria As DataTable, ByVal dtFinal As DataTable, ByVal Type As String)

    '    If Not IsNothing(precon) Then
    '        If Not IsNothing(precon.precondition) Then
    '            If precon.precondition.Length > 0 Then
    '                For k As Integer = 0 To precon.precondition.Length - 1
    '                    If Not IsNothing(precon.precondition(k).Item) Then

    '                        If Convert.ToString(precon.precondition(k).Item) = "gloEMeasure.POQM_MT000001UV03CriteriaReference" Then
    '                            Dim precon1 As POQM_MT000001UV03CriteriaReference = Nothing
    '                            precon1 = DirectCast(precon.precondition(k).Item, gloEMeasure.POQM_MT000001UV03CriteriaReference)
    '                            ReadCriteria(precon1, dtDataCriteria, dtFinal, Type)
    '                            'If Not IsNothing(precon1) Then
    '                            '    If Not IsNothing(precon1.id) Then
    '                            '        GetPopulationCategoryDetails(dtDataCriteria, Convert.ToString(precon1.id.root), dtFinal, Type)

    '                            '    End If
    '                            'End If

    '                        ElseIf Convert.ToString(precon.precondition(k).Item) = "gloEMeasure.POQM_MT000001UV03AllTrue" Then
    '                            ReadAllTrue(precon.precondition(k).Item, dtDataCriteria, dtFinal, Type)

    '                        End If

    '                    End If
    '                    ''
    '                Next
    '            End If

    '        End If



    '    End If
    'End Sub
    Private Sub ReadAllTrue(ByVal precon As Object, ByVal dtDataCriteria As DataTable, ByVal dtFinal As DataTable, ByVal Type As String)
        '  ReadDetails(precon, dtDataCriteria, dtFinal, Type)
        If Not IsNothing(precon) Then
            If Not IsNothing(precon.precondition) Then
                If precon.precondition.Length > 0 Then
                    For k As Integer = 0 To precon.precondition.Length - 1
                        If Not IsNothing(precon.precondition(k).Item) Then

                            If Convert.ToString(precon.precondition(k).Item) = "gloEMeasure.POQM_MT000001UV03CriteriaReference" Then
                                Dim precon1 As POQM_MT000001UV03CriteriaReference = Nothing
                                precon1 = DirectCast(precon.precondition(k).Item, gloEMeasure.POQM_MT000001UV03CriteriaReference)
                                ReadCriteria(precon1, dtDataCriteria, dtFinal, Type)
                                'If Not IsNothing(precon1) Then
                                '    If Not IsNothing(precon1.id) Then
                                '        GetPopulationCategoryDetails(dtDataCriteria, Convert.ToString(precon1.id.root), dtFinal, Type)

                                '    End If
                                'End If
                            Else
                                ReadAllFalse(precon.precondition(k).Item, dtDataCriteria, dtFinal, Type)
                                'ElseIf Convert.ToString(precon.precondition(k).Item) = "gloEMeasure.POQM_MT000001UV03AllFalse" Then
                                '    ReadAllFalse(precon.precondition(k).Item, dtDataCriteria, dtFinal, Type)

                            End If

                        End If
                    Next
                End If

            End If

        End If
    End Sub
    Private Sub ReadFinal(Criteria As Object, ByVal dtDataCriteria As DataTable, ByVal dtFinal As DataTable, ByVal Type As String)
        If Not IsNothing(Criteria) Then
            If Not IsNothing(Criteria.precondition) Then
                If Criteria.precondition.Length > 0 Then
                    Dim k As Integer
                    While k <= Criteria.precondition.length - 1
                        If Not IsNothing(Criteria.precondition(k).Item) Then
                            If Convert.ToString(Criteria.precondition(k).Item) = "gloEMeasure.POQM_MT000001UV03CriteriaReference" Then
                                Dim precon1 As POQM_MT000001UV03CriteriaReference = Nothing
                                precon1 = DirectCast(Criteria.precondition(k).Item, gloEMeasure.POQM_MT000001UV03CriteriaReference)
                                ReadCriteria(Criteria.precondition(k).Item, dtDataCriteria, dtFinal, Type)
                            Else
                                ReadFinal(Criteria.precondition(k).Item, dtDataCriteria, dtFinal, Type)
                            End If
                        End If
                        k = k + 1
                    End While
                    'For k As Integer = 0 To Criteria.precondition.Length - 1
                    '    If Not IsNothing(Criteria.precondition(k).Item) Then

                    '        If Convert.ToString(Criteria.precondition(k).Item) = "gloEMeasure.POQM_MT000001UV03CriteriaReference" Then
                    '            Dim precon1 As POQM_MT000001UV03CriteriaReference = Nothing
                    '            precon1 = DirectCast(Criteria.precondition(k).Item, gloEMeasure.POQM_MT000001UV03CriteriaReference)
                    '            ReadCriteria(Criteria.precondition(k).Item, dtDataCriteria, dtFinal, Type)
                    '        Else
                    '            ' ReadAllTrue(Criteria.precondition(k).Item, dtDataCriteria, dtFinal, Type)
                    '            ''
                    '            If Not IsNothing(Criteria) Then
                    '                If Not IsNothing(Criteria.precondition) Then
                    '                    If Criteria.precondition.Length > 0 Then
                    '                        For kl As Integer = 0 To Criteria.precondition.Length - 1
                    '                            If Not IsNothing(Criteria.precondition(kl).Item) Then

                    '                                If Convert.ToString(Criteria.precondition(kl).Item) = "gloEMeasure.POQM_MT000001UV03CriteriaReference" Then
                    '                                    Dim precon1 As POQM_MT000001UV03CriteriaReference = Nothing
                    '                                    precon1 = DirectCast(Criteria.precondition(kl).Item, gloEMeasure.POQM_MT000001UV03CriteriaReference)
                    '                                    ReadCriteria(precon1, dtDataCriteria, dtFinal, Type)
                    '                                    'If Not IsNothing(precon1) Then
                    '                                    '    If Not IsNothing(precon1.id) Then
                    '                                    '        GetPopulationCategoryDetails(dtDataCriteria, Convert.ToString(precon1.id.root), dtFinal, Type)

                    '                                    '    End If
                    '                                    'End If
                    '                                Else
                    '                                    ReadAllFalse(precon.precondition(kl).Item, dtDataCriteria, dtFinal, Type)
                    '                                    'ElseIf Convert.ToString(precon.precondition(k).Item) = "gloEMeasure.POQM_MT000001UV03AllFalse" Then
                    '                                    '    ReadAllFalse(precon.precondition(k).Item, dtDataCriteria, dtFinal, Type)

                    '                                End If

                    '                            End If
                    '                        Next
                    '                    End If

                    '                End If

                    '            End If
                    '            ''
                    '        End If
                    '    End If


                    'Next
                End If

            End If

        End If
    End Sub
    Private Sub ReadDetails(Criteria As Object, ByVal dtDataCriteria As DataTable, ByVal dtFinal As DataTable, ByVal Type As String)
        If Not IsNothing(Criteria) Then
            If Not IsNothing(Criteria.precondition) Then
                If Criteria.precondition.Length > 0 Then
                    For k As Integer = 0 To Criteria.precondition.Length - 1
                        If Not IsNothing(Criteria.precondition(k).Item) Then

                            If Convert.ToString(Criteria.precondition(k).Item) = "gloEMeasure.POQM_MT000001UV03CriteriaReference" Then
                                Dim precon1 As POQM_MT000001UV03CriteriaReference = Nothing
                                precon1 = DirectCast(Criteria.precondition(k).Item, gloEMeasure.POQM_MT000001UV03CriteriaReference)
                                ReadCriteria(Criteria.precondition(k).Item, dtDataCriteria, dtFinal, Type)
                            Else
                                ReadAllTrue(Criteria.precondition(k).Item, dtDataCriteria, dtFinal, Type)

                            End If
                        End If


                    Next
                End If

            End If

        End If
    End Sub
    Private Sub ReadAllFalse(ByVal precon As Object, ByVal dtDataCriteria As DataTable, ByVal dtFinal As DataTable, ByVal Type As String)
        If Not IsNothing(precon) Then

            If Not IsNothing(precon.precondition) Then
                If precon.precondition.Length > 0 Then
                    For k As Integer = 0 To precon.precondition.Length - 1
                        If Not IsNothing(precon.precondition(k).Item) Then

                            If Convert.ToString(precon.precondition(k).Item) = "gloEMeasure.POQM_MT000001UV03CriteriaReference" Then
                                Dim precon1 As POQM_MT000001UV03CriteriaReference = Nothing
                                precon1 = DirectCast(precon.precondition(k).Item, gloEMeasure.POQM_MT000001UV03CriteriaReference)
                                ReadCriteria(precon1, dtDataCriteria, dtFinal, Type)
                                'If Not IsNothing(precon1) Then
                                '    If Not IsNothing(precon1.id) Then
                                '        GetPopulationCategoryDetails(dtDataCriteria, Convert.ToString(precon1.id.root), dtFinal, Type)

                                '    End If
                                'End If
                            Else
                                ReadAllTrue(precon, dtDataCriteria, dtFinal, Type)
                                '  ReadAllFalse(precon, dtDataCriteria, dtFinal, Type)
                            End If
                        End If
                    Next
                End If

            End If
        End If
    End Sub
    'Private Sub ReadOnlyOneTrue(ByVal precon As POQM_MT000001UV03OnlyOneTrue, ByVal dtDataCriteria As DataTable, ByVal dtFinal As DataTable, ByVal Type As String)
    '    If Not IsNothing(precon) Then

    '        If Not IsNothing(precon.precondition) Then
    '            If precon.precondition.Length > 0 Then
    '                For k As Integer = 0 To precon.precondition.Length - 1
    '                    If Not IsNothing(precon.precondition(k).Item) Then

    '                        If Convert.ToString(precon.precondition(k).Item) = "gloEMeasure.POQM_MT000001UV03CriteriaReference" Then
    '                            Dim precon1 As POQM_MT000001UV03CriteriaReference = Nothing
    '                            precon1 = DirectCast(precon.precondition(k).Item, gloEMeasure.POQM_MT000001UV03CriteriaReference)
    '                            ReadCriteria(precon1, dtDataCriteria, dtFinal, Type)
    '                            'If Not IsNothing(precon1) Then
    '                            '    If Not IsNothing(precon1.id) Then
    '                            '        GetPopulationCategoryDetails(dtDataCriteria, Convert.ToString(precon1.id.root), dtFinal, Type)

    '                            '    End If
    '                            'End If

    '                        End If
    '                    End If
    '                Next
    '            End If

    '        End If
    '    End If
    'End Sub
    'Private Sub ReadOnlyOneFalse(ByVal precon As POQM_MT000001UV03OnlyOneFalse, ByVal dtDataCriteria As DataTable, ByVal dtFinal As DataTable, ByVal Type As String)
    '    If Not IsNothing(precon) Then

    '        If Not IsNothing(precon.precondition) Then
    '            If precon.precondition.Length > 0 Then
    '                For k As Integer = 0 To precon.precondition.Length - 1
    '                    If Not IsNothing(precon.precondition(k).Item) Then

    '                        If Convert.ToString(precon.precondition(k).Item) = "gloEMeasure.POQM_MT000001UV03CriteriaReference" Then
    '                            Dim precon1 As POQM_MT000001UV03CriteriaReference = Nothing
    '                            precon1 = DirectCast(precon.precondition(k).Item, gloEMeasure.POQM_MT000001UV03CriteriaReference)
    '                            ReadCriteria(precon1, dtDataCriteria, dtFinal, Type)
    '                            'If Not IsNothing(precon1) Then
    '                            '    If Not IsNothing(precon1.id) Then
    '                            '        GetPopulationCategoryDetails(dtDataCriteria, Convert.ToString(precon1.id.root), dtFinal, Type)

    '                            '    End If
    '                            'End If

    '                        End If
    '                    End If
    '                Next
    '            End If

    '        End If
    '    End If
    'End Sub
    'Private Sub ReadPopulationCriteriaReference(ByVal InitialPop As POQM_MT000001UV03InitialPopulationCriteria, ByVal index As Integer, ByVal dtDataCriteria As DataTable, dtFinal As DataTable)
    '    Dim precon As POQM_MT000001UV03CriteriaReference = Nothing

    '    precon = DirectCast(InitialPop.precondition(index).Item, gloEMeasure.POQM_MT000001UV03CriteriaReference)

    '    If Not IsNothing(precon) Then
    '        If Not IsNothing(precon.id) Then
    '            GetPopulationCategoryDetails(dtDataCriteria, Convert.ToString(precon.id.root), dtFinal, "Initial Population")

    '        End If
    '    End If


    'End Sub

    Private Sub GetPopulationCategoryDetails(ByVal dtDataCriteria As DataTable, Root As String, ByVal dtFinal As DataTable, ByVal Type As String)
        Dim drRow() As DataRow = dtDataCriteria.Select("GroupID =  '" + Root + "'")
        If drRow.Length > 0 Then
            For g As Integer = 0 To drRow.Length - 1
                drRow(g)("Criteria") = Type
                drRow(g)("CMSID") = _CMsID
                dtFinal.ImportRow(drRow(g))
            Next
        Else
            drRow = dtDataCriteria.Select("Root =  '" + Root + "'")
            If drRow.Length > 0 Then
                drRow(0)("Criteria") = Type
                drRow(0)("CMSID") = _CMsID
                dtFinal.ImportRow(drRow(0))
                drRow = Nothing
            End If
        End If
        drRow = Nothing

        'Dim drRow() As DataRow = dtDataCriteria.Select("Root =  '" + Root + "'")
        'If drRow.Length > 0 Then
        '    drRow(0)("Criteria") = Type
        '    drRow(0)("CMSID") = _CMsID
        '    dtFinal.ImportRow(drRow(0))
        '    drRow = Nothing
        'Else

        'drRow = dtDataCriteria.Select("GroupID =  '" + Root + "'")
        'If drRow.Length > 0 Then
        '    For g As Integer = 0 To drRow.Length - 1
        '        drRow(g)("Criteria") = Type
        '        drRow(g)("CMSID") = _CMsID
        '        dtFinal.ImportRow(drRow(g))
        '    Next

        'End If
        'drRow = Nothing
        ' End If
    End Sub
    Private Sub ReadAgeCriteria(ByVal DataCriteria As POQM_MT000001UV03DataCriteriaSection)
        Dim AgeMin As Decimal
        Dim AgeMinDetails As Integer = 0
        ' Dim AgeMingreaterandequal As Integer = 1
        '  Dim AgeMinequal As Integer = 2
        Dim AgeMax As Decimal
        Dim AgeMaxDetails As Integer = 0
        If Not IsNothing(DataCriteria.entry) Then
            If DataCriteria.entry.Length > 0 Then
                For k As Integer = 0 To DataCriteria.entry.Length - 1
                    Dim SourceOF As POQM_MT000001UV03SourceOf = Nothing
                    SourceOF = DataCriteria.entry(k)
                    If Not IsNothing(SourceOF.Item) Then
                        If Convert.ToString(SourceOF.Item) = "gloEMeasure.POQM_MT000001UV03ObservationCriteria" Then

                            Dim Observation As POQM_MT000001UV03ObservationCriteria = Nothing
                            Observation = DirectCast(SourceOF.Item, gloEMeasure.POQM_MT000001UV03ObservationCriteria)
                            If Not IsNothing(Observation) Then


                                If Not IsNothing(Observation.code) Then
                                    If Not IsNothing(Observation.code.code) Then
                                        Dim _isFirstPop As Boolean = False
                                        If Observation.code.code = "21112-8" Then
                                            If Not IsNothing(Observation.id) Then
                                                If Not IsNothing(Observation.id.root) Then

                                                    Dim drRow() As DataRow = dtFinal.Select("CMSID='" + _CMsID + "' AND Root= '" + Convert.ToString(Observation.id.root) + "' and (Criteria ='Initial Population' or Criteria='Denominator')")

                                                    If drRow.Length > 0 Then
                                                        If Not IsNothing(Observation.temporallyRelatedInformation) Then
                                                            If Observation.temporallyRelatedInformation.Length > 0 Then
                                                                ' Dim obsTempo As POQM_MT000001UV03SourceOf4 = Nothing
                                                                Dim obsTempo() As POQM_MT000001UV03SourceOf4 = DirectCast(Observation.temporallyRelatedInformation, gloEMeasure.POQM_MT000001UV03SourceOf4())
                                                                If Not IsNothing(obsTempo(0)) Then
                                                                    If Not IsNothing(obsTempo(0).temporalInformation) Then
                                                                        If Not IsNothing(obsTempo(0).temporalInformation.delta) Then
                                                                            If Not IsNothing(obsTempo(0).temporalInformation.delta.low) Then


                                                                                If Convert.ToDecimal(obsTempo(0).temporalInformation.delta.low.value) <> "0" And obsTempo(0).temporalInformation.delta.high.nullFlavor.ToString = NullFlavor.PINF.ToString Then
                                                                                    If obsTempo(0).temporalInformation.delta.lowClosed = True AndAlso obsTempo(0).temporalInformation.delta.highClosed = True Then
                                                                                        AgeMinDetails = 2
                                                                                    End If
                                                                                    If Not IsNothing(obsTempo(0).temporalInformation.delta.low) Then
                                                                                        If Convert.ToString(obsTempo(0).temporalInformation.delta.low.unit) = "mo" Then
                                                                                            If Convert.ToString(obsTempo(0).temporalInformation.delta.low.value).Trim.Length > 1 Then
                                                                                                AgeMin = CType("." & Convert.ToString(obsTempo(0).temporalInformation.delta.low.value), Double)
                                                                                            Else
                                                                                                AgeMin = CType(".0" & Convert.ToString(obsTempo(0).temporalInformation.delta.low.value), Double)
                                                                                            End If


                                                                                        Else
                                                                                            AgeMin = Convert.ToDecimal(obsTempo(0).temporalInformation.delta.low.value)
                                                                                        End If

                                                                                        If AgeMin <> 0 Then
                                                                                            AgeMinDetails = 1
                                                                                        End If

                                                                                    End If
                                                                                    If Not IsNothing(obsTempo(0).temporalInformation.delta.high) Then
                                                                                        If Convert.ToDecimal(obsTempo(0).temporalInformation.delta.high.value) <> "0" Then
                                                                                            AgeMinDetails = 2
                                                                                        End If

                                                                                    End If
                                                                                Else

                                                                                    If obsTempo(0).temporalInformation.delta.lowClosed = True AndAlso obsTempo(0).temporalInformation.delta.highClosed = True Then
                                                                                        AgeMaxDetails = 2
                                                                                        AgeMax = Convert.ToDecimal(obsTempo(0).temporalInformation.delta.high.value)
                                                                                    End If

                                                                                    If Not IsNothing(obsTempo(0).temporalInformation.delta.high) Then
                                                                                        If Convert.ToDecimal(obsTempo(0).temporalInformation.delta.low.value) = "0" AndAlso Convert.ToDecimal(obsTempo(0).temporalInformation.delta.high.value) <> "0" Then
                                                                                            AgeMax = Convert.ToDecimal(obsTempo(0).temporalInformation.delta.high.value)
                                                                                            If AgeMaxDetails = "2" Then
                                                                                                AgeMaxDetails = 1
                                                                                            Else

                                                                                                AgeMaxDetails = 0
                                                                                            End If

                                                                                        ElseIf Convert.ToDecimal(obsTempo(0).temporalInformation.delta.low.value) = Convert.ToDecimal(obsTempo(0).temporalInformation.delta.high.value) Then
                                                                                            AgeMaxDetails = 2
                                                                                        End If

                                                                                    End If
                                                                                    If Not IsNothing(obsTempo(0).temporalInformation.delta.low) Then
                                                                                        If Convert.ToDecimal(obsTempo(0).temporalInformation.delta.low.value) <> "0" AndAlso Convert.ToDecimal(obsTempo(0).temporalInformation.delta.high.value) = "0" Then
                                                                                            AgeMaxDetails = 2
                                                                                        End If
                                                                                    End If
                                                                                End If



                                                                                Dim drRow1() As DataRow = dtPlanDetails.Select("CMSID =  '" + Convert.ToString(_CMSIDNumber).Trim + "'")


                                                                                If drRow1.Length > 0 Then
                                                                                    For g As Integer = 0 To drRow1.Length - 1
                                                                                        drRow1(g)("AgeMin") = AgeMin
                                                                                        drRow1(g)("AgeMinDetails") = AgeMinDetails
                                                                                        drRow1(g)("AgeMax") = AgeMax
                                                                                        drRow1(g)("AgeMaxDetails") = AgeMaxDetails

                                                                                    Next
                                                                                    Exit For
                                                                                Else
                                                                                    Dim drage As DataRow = Nothing
                                                                                    drage = dtPlanDetails.NewRow()
                                                                                    drage(0) = _CMSIDNumber
                                                                                    drage(1) = AgeMin
                                                                                    drage(2) = AgeMinDetails
                                                                                    drage(3) = AgeMax
                                                                                    drage(4) = AgeMaxDetails
                                                                                    dtPlanDetails.Rows.Add(drage)
                                                                                    drage = Nothing
                                                                                End If


                                                                            End If




                                                                        End If
                                                                    End If
                                                                End If

                                                            End If
                                                        End If
                                                    End If
                                                End If

                                            End If
                                          
                                        End If
                                    End If
                                End If
                            End If


                        End If

                    End If
                Next
            End If
        End If

    End Sub
    Private Function GetDataCriteriaDetails(ByVal DataCriteria As POQM_MT000001UV03DataCriteriaSection) As DataTable
        Dim dtDataCriteria As New DataTable
        dtDataCriteria.Columns.Add("Root")
        dtDataCriteria.Columns.Add("Extension")
        dtDataCriteria.Columns.Add("ValuSet")
        dtDataCriteria.Columns.Add("Title")
        dtDataCriteria.Columns.Add("GroupID")
        dtDataCriteria.Columns.Add("GroupRoot")
        dtDataCriteria.Columns.Add("Criteria")
        dtDataCriteria.Columns.Add("CMSID")
        dtDataCriteria.Columns.Add("NegationID")


        If Not IsNothing(DataCriteria.entry) Then
            If DataCriteria.entry.Length > 0 Then
                For k As Integer = 0 To DataCriteria.entry.Length - 1
                    Dim SourceOF As POQM_MT000001UV03SourceOf = Nothing
                    SourceOF = DataCriteria.entry(k)
                    If Not IsNothing(SourceOF.Item) Then
                        Dim Root As String = ""
                        Dim Extension As String = ""
                        Dim ValueSet As String = ""
                        Dim Title As String = ""
                        Dim _isNegation As Boolean = False
                        ''Encounter,obervation,procedure,substanceadmin,suplly,Grouper,Reference
                        If Convert.ToString(SourceOF.Item) = "gloEMeasure.POQM_MT000001UV03EncounterCriteria" Then
                            Dim Encounter As POQM_MT000001UV03EncounterCriteria = Nothing
                            Encounter = DirectCast(SourceOF.Item, gloEMeasure.POQM_MT000001UV03EncounterCriteria)

                            ReadValueSetDetails(Encounter, dtDataCriteria)

                            Dim outbound() As POQM_MT000001UV03SourceOf2 = DirectCast(Encounter.outboundRelationship, gloEMeasure.POQM_MT000001UV03SourceOf2())
                            If Not IsNothing(outbound) Then

                                If Not IsNothing(DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2())) Then
                                    If DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2()).Length > 0 Then
                                        ' If outbound(0).typeCode = ActRelationshipType.COMP Then
                                        '' ReadTemporalInfo(Encounter, dtDataCriteria, True)
                                        If Not IsNothing(Encounter.temporallyRelatedInformation) Then
                                            If Encounter.temporallyRelatedInformation.Length > 0 Then

                                                If _CMSIDNumber = 165 Then
                                                    ' If Encounter.temporallyRelatedInformation(0).typeCode = "DURING" Then ''condition Added for CMS22 BMI Encounter codes



                                                    For h As Integer = 0 To DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2()).Length - 1
                                                        If Not IsNothing(DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2())(h)) Then
                                                            If Convert.ToString(DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2())(h).Item) = "gloEMeasure.POQM_MT000001UV03CriteriaReference" Then
                                                                Dim criteriaref As POQM_MT000001UV03CriteriaReference = Nothing
                                                                criteriaref = DirectCast(DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2())(h).Item, gloEMeasure.POQM_MT000001UV03CriteriaReference)
                                                                If Not IsNothing(criteriaref) Then
                                                                    If criteriaref.classCode = ActClassRoot.GROUPER Then
                                                                        Root = criteriaref.id.root
                                                                        Extension = Convert.ToString(criteriaref.id.extension).Replace("'", "''")
                                                                        Dim drRow() As DataRow = dtDataCriteria.Select("GroupID= '" + Root + "'")
                                                                        If drRow.Length > 0 Then
                                                                            For g As Integer = 0 To drRow.Length - 1
                                                                                drRow(g)("GroupID") = Convert.ToString(Encounter.id.root)
                                                                            Next

                                                                        End If

                                                                        drRow = Nothing
                                                                    Else


                                                                        Root = criteriaref.id.root
                                                                        Extension = Convert.ToString(criteriaref.id.extension).Replace("'", "''")

                                                                        Dim drRow() As DataRow = dtDataCriteria.Select("Root= '" + Root + "'")
                                                                        If drRow.Length > 0 Then

                                                                            drRow(0)("GroupID") = Convert.ToString(Encounter.id.root)
                                                                        End If
                                                                        drRow = Nothing
                                                                    End If
                                                                End If
                                                                criteriaref = Nothing
                                                            End If

                                                        End If

                                                    Next
                                                    'Else

                                                    'End If
                                                Else
                                                    If Encounter.temporallyRelatedInformation(0).typeCode = "DURING" Then ''condition Added for CMS22 BMI Encounter codes



                                                        For h As Integer = 0 To DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2()).Length - 1
                                                            If Not IsNothing(DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2())(h)) Then
                                                                If Convert.ToString(DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2())(h).Item) = "gloEMeasure.POQM_MT000001UV03CriteriaReference" Then
                                                                    Dim criteriaref As POQM_MT000001UV03CriteriaReference = Nothing
                                                                    criteriaref = DirectCast(DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2())(h).Item, gloEMeasure.POQM_MT000001UV03CriteriaReference)
                                                                    If Not IsNothing(criteriaref) Then
                                                                        If criteriaref.classCode = ActClassRoot.GROUPER Then
                                                                            Root = criteriaref.id.root
                                                                            Extension = Convert.ToString(criteriaref.id.extension).Replace("'", "''")
                                                                            Dim drRow() As DataRow = dtDataCriteria.Select("GroupID= '" + Root + "'")
                                                                            If drRow.Length > 0 Then
                                                                                For g As Integer = 0 To drRow.Length - 1
                                                                                    drRow(g)("GroupID") = Convert.ToString(Encounter.id.root)
                                                                                Next

                                                                            End If

                                                                            drRow = Nothing
                                                                        Else


                                                                            Root = criteriaref.id.root
                                                                            Extension = Convert.ToString(criteriaref.id.extension).Replace("'", "''")

                                                                            Dim drRow() As DataRow = dtDataCriteria.Select("Root= '" + Root + "'")
                                                                            If drRow.Length > 0 Then

                                                                                drRow(0)("GroupID") = Convert.ToString(Encounter.id.root)
                                                                            End If
                                                                            drRow = Nothing
                                                                        End If
                                                                    End If
                                                                    criteriaref = Nothing
                                                                End If

                                                            End If

                                                        Next
                                                    Else

                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                End If

                            End If

                            outbound = Nothing
                            Encounter = Nothing
                        ElseIf Convert.ToString(SourceOF.Item) = "gloEMeasure.POQM_MT000001UV03ObservationCriteria" Then

                            Dim Observation As POQM_MT000001UV03ObservationCriteria = Nothing
                            Observation = DirectCast(SourceOF.Item, gloEMeasure.POQM_MT000001UV03ObservationCriteria)
                            ReadValueSetDetails(Observation, dtDataCriteria)

                            ''Group and OCCR
                            ''ReadTemporalInfo(Observation, dtDataCriteria)
                            Dim temporallyrelatedinfo() As POQM_MT000001UV03SourceOf4 = DirectCast(Observation.temporallyRelatedInformation, gloEMeasure.POQM_MT000001UV03SourceOf4())
                            If Not IsNothing(temporallyrelatedinfo) Then
                                If temporallyrelatedinfo.Length > 0 Then

                                    If _CMsID.ToUpper = "CMS56V4".ToUpper Or _CMsID.ToUpper = "CMS66V4".ToUpper Then

                                    Else
                                        '  If temporallyrelatedinfo(0).typeCode <> "EBS" Then


                                        If _CMSIDNumber = 90 Then
                                        Else
                                            For h As Integer = 0 To DirectCast(temporallyrelatedinfo, gloEMeasure.POQM_MT000001UV03SourceOf4()).Length - 1
                                                If Not IsNothing(DirectCast(temporallyrelatedinfo, gloEMeasure.POQM_MT000001UV03SourceOf4())(h)) Then
                                                    If Convert.ToString(DirectCast(temporallyrelatedinfo, gloEMeasure.POQM_MT000001UV03SourceOf4())(h).Item) = "gloEMeasure.POQM_MT000001UV03CriteriaReference" Then
                                                        Dim criteriaref As POQM_MT000001UV03CriteriaReference = Nothing
                                                        criteriaref = DirectCast(DirectCast(temporallyrelatedinfo, gloEMeasure.POQM_MT000001UV03SourceOf4())(h).Item, gloEMeasure.POQM_MT000001UV03CriteriaReference)
                                                        If Not IsNothing(criteriaref) Then
                                                            If criteriaref.classCode = ActClassRoot.GROUPER Then
                                                                Root = criteriaref.id.root
                                                                Extension = Convert.ToString(criteriaref.id.extension).Replace("'", "''")
                                                                Dim drRow() As DataRow = dtDataCriteria.Select("GroupID= '" + Root + "'")
                                                                'If _CMsID.ToUpper = "CMS90V5".ToUpper Then
                                                                'Else
                                                                If drRow.Length > 0 Then
                                                                    For g As Integer = 0 To drRow.Length - 1
                                                                        '  If Convert.ToString(drRow(g)("GroupID")) = "" Then
                                                                        drRow(g)("GroupID") = Convert.ToString(Observation.id.root)
                                                                        '  End If
                                                                    Next

                                                                End If

                                                                'End If

                                                                drRow = Nothing
                                                            Else

                                                            End If
                                                        End If
                                                        criteriaref = Nothing
                                                    End If

                                                End If
                                            Next
                                            'End If

                                        End If
                                    End If

                                End If
                            End If
                            Dim outbound() As POQM_MT000001UV03SourceOf2 = DirectCast(Observation.outboundRelationship, gloEMeasure.POQM_MT000001UV03SourceOf2())
                            If Not IsNothing(outbound) Then

                                If Not IsNothing(DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2())) Then
                                    If DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2()).Length > 0 Then
                                        ' If outbound(0).typeCode = ActRelationshipType.COMP Then


                                        For h As Integer = 0 To DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2()).Length - 1
                                            If Not IsNothing(DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2())(h)) Then
                                                If Convert.ToString(DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2())(h).Item) = "gloEMeasure.POQM_MT000001UV03CriteriaReference" Then
                                                    Dim criteriaref As POQM_MT000001UV03CriteriaReference = Nothing
                                                    criteriaref = DirectCast(DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2())(h).Item, gloEMeasure.POQM_MT000001UV03CriteriaReference)
                                                    If Not IsNothing(criteriaref) Then
                                                        If criteriaref.classCode = ActClassRoot.GROUPER Then
                                                            Root = criteriaref.id.root
                                                            Extension = Convert.ToString(criteriaref.id.extension).Replace("'", "''")
                                                            Dim drRow() As DataRow = dtDataCriteria.Select("GroupID= '" + Root + "'")
                                                            If drRow.Length > 0 Then
                                                                For g As Integer = 0 To drRow.Length - 1
                                                                    drRow(g)("GroupID") = Convert.ToString(Observation.id.root)
                                                                Next

                                                            End If
                                                            'If drRow.Length > 0 Then
                                                            '    drRow(0)("GroupID") = Convert.ToString(grouper.id.root)
                                                            'End If
                                                            drRow = Nothing
                                                        Else


                                                            Root = criteriaref.id.root
                                                            Extension = Convert.ToString(criteriaref.id.extension).Replace("'", "''")
                                                            'Dim search As String = "Root= '" + Root + "' and Extension ='" + Extension + "'"
                                                            '  Dim drRow() As DataRow = dtDataCriteria.Select("Root= '" + Root + "' and Extension ='" + Extension + "'")
                                                            Dim drRow() As DataRow = dtDataCriteria.Select("Root= '" + Root + "'")
                                                            If drRow.Length > 0 Then
                                                                For g As Integer = 0 To drRow.Length - 1
                                                                    drRow(g)("GroupID") = Convert.ToString(Observation.id.root)
                                                                Next
                                                            End If
                                                            drRow = Nothing
                                                        End If
                                                    End If
                                                    criteriaref = Nothing
                                                End If

                                            End If
                                        Next

                                    End If
                                End If

                            End If
                            Dim LineNo As String = ""
                            ' Dim AgeMaxequal As Boolean = False

                            outbound = Nothing

                            Observation = Nothing
                        ElseIf Convert.ToString(SourceOF.Item) = "gloEMeasure.POQM_MT000001UV03SubstanceAdministrationCriteria" Then
                            Dim substance As POQM_MT000001UV03SubstanceAdministrationCriteria = Nothing
                            substance = DirectCast(SourceOF.Item, gloEMeasure.POQM_MT000001UV03SubstanceAdministrationCriteria)

                            If Not IsNothing(substance.id) Then
                                Root = substance.id.root
                                Extension = substance.id.extension

                            End If
                            If Not IsNothing(substance.title) Then
                                If Not IsNothing(substance.title.value) Then
                                    Title = substance.title.value

                                End If
                            End If
                            Try
                                If Not IsNothing(substance.participation) Then
                                    If substance.participation.Length > 0 Then
                                        If Not IsNothing(substance.participation(0)) Then
                                            If Not IsNothing(substance.participation(0).Item) Then
                                                '  Dim oparticipation As POQM_MT000001UV03Participant = substance.participation(0).Item
                                                Dim orol As POQM_MT000001UV03Role = substance.participation(0).Item
                                                If Not IsNothing(orol) Then
                                                    If Not IsNothing(orol.Item) Then
                                                        If Convert.ToString(orol.Item) = "gloEMeasure.POQM_MT000001UV03ManufacturedMaterial" Then
                                                            Dim manu As POQM_MT000001UV03ManufacturedMaterial = orol.Item
                                                            If Not IsNothing(manu) Then
                                                                ValueSet = manu.code.valueSet
                                                            End If
                                                        ElseIf Convert.ToString(orol.Item) = "gloEMeasure.POQM_MT000001UV03Material" Then
                                                            Dim manu As POQM_MT000001UV03Material = orol.Item
                                                            If Not IsNothing(manu) Then
                                                                ValueSet = manu.code.valueSet
                                                            End If
                                                        End If

                                                    End If


                                                End If

                                            End If
                                        End If

                                    End If
                                End If

                            Catch ex As Exception

                            End Try
                            Try
                                If ValueSet = "" Then
                                    If Not IsNothing(substance.code) Then
                                        ValueSet = substance.code.valueSet

                                    End If
                                End If
                            Catch ex As Exception

                            End Try
                            If Convert.ToString(ValueSet) <> "" Then
                                Dim dr As DataRow = Nothing
                                dr = dtDataCriteria.NewRow()
                                dr(0) = Root
                                dr(1) = Extension
                                dr(2) = ValueSet
                                dr(3) = Title

                                dtDataCriteria.Rows.Add(dr)
                                dr = Nothing
                            End If
                            ' Observation = Nothing

                        ElseIf Convert.ToString(SourceOF.Item) = "gloEMeasure.POQM_MT000001UV03ActCriteria" Then
                            Dim act As POQM_MT000001UV03ActCriteria = Nothing
                            act = DirectCast(SourceOF.Item, gloEMeasure.POQM_MT000001UV03ActCriteria)
                            ReadValueSetDetails(act, dtDataCriteria)

                            act = Nothing

                        ElseIf Convert.ToString(SourceOF.Item) = "gloEMeasure.POQM_MT000001UV03ProcedureCriteria" Then
                            Dim proc As POQM_MT000001UV03ProcedureCriteria = Nothing
                            proc = DirectCast(SourceOF.Item, gloEMeasure.POQM_MT000001UV03ProcedureCriteria)
                            ReadValueSetDetails(proc, dtDataCriteria)
                            proc = Nothing
                        ElseIf Convert.ToString(SourceOF.Item) = "gloEMeasure.POQM_MT000001UV03GrouperCriteria" Then

                            Dim grouper As POQM_MT000001UV03GrouperCriteria = Nothing
                            grouper = DirectCast(SourceOF.Item, gloEMeasure.POQM_MT000001UV03GrouperCriteria)
                            ''ReadTemporalInfo(grouper, dtDataCriteria, True)
                            Dim temporallyrelatedinfo() As POQM_MT000001UV03SourceOf4 = DirectCast(grouper.temporallyRelatedInformation, gloEMeasure.POQM_MT000001UV03SourceOf4())

                            If Not IsNothing(temporallyrelatedinfo) Then
                                If temporallyrelatedinfo.Length > 0 Then


                                    For h As Integer = 0 To DirectCast(temporallyrelatedinfo, gloEMeasure.POQM_MT000001UV03SourceOf4()).Length - 1
                                        If Not IsNothing(DirectCast(temporallyrelatedinfo, gloEMeasure.POQM_MT000001UV03SourceOf4())(h)) Then
                                            If Convert.ToString(DirectCast(temporallyrelatedinfo, gloEMeasure.POQM_MT000001UV03SourceOf4())(h).Item) = "gloEMeasure.POQM_MT000001UV03CriteriaReference" Then
                                                Dim criteriaref As POQM_MT000001UV03CriteriaReference = Nothing
                                                criteriaref = DirectCast(DirectCast(temporallyrelatedinfo, gloEMeasure.POQM_MT000001UV03SourceOf4())(h).Item, gloEMeasure.POQM_MT000001UV03CriteriaReference)
                                                If Not IsNothing(criteriaref) Then
                                                    If criteriaref.classCode.ToString() = ActClassRoot.GROUPER.ToString() Then
                                                        Root = criteriaref.id.root
                                                        Extension = Convert.ToString(criteriaref.id.extension).Replace("'", "''")
                                                        Dim drRow() As DataRow = dtDataCriteria.Select("GroupID= '" + Root + "'")

                                                        If drRow.Length > 0 Then
                                                            For g As Integer = 0 To drRow.Length - 1
                                                                drRow(g)("GroupID") = Convert.ToString(grouper.id.root)
                                                            Next
                                                        End If

                                                        'End If

                                                        drRow = Nothing
                                                    Else
                                                        ' check this code
                                                        Root = criteriaref.id.root
                                                        Extension = Convert.ToString(criteriaref.id.extension).Replace("'", "''")

                                                        Dim drRow1() As DataRow = dtDataCriteria.Select("Root= '" + Root + "'")

                                                        For g As Integer = 0 To drRow1.Length - 1

                                                            If Convert.ToString(drRow1(g)("GroupID")) = "" Then
                                                                drRow1(g)("GroupID") = Convert.ToString(grouper.id.root)
                                                            End If
                                                        Next
                                                        drRow1 = Nothing
                                                    End If
                                                End If
                                                criteriaref = Nothing
                                            End If

                                        End If
                                    Next
                                End If

                            End If

                            If Not IsNothing(grouper.outboundRelationship) Then
                                If grouper.outboundRelationship.Length > 0 Then
                                    '  Dim outbound = grouper.outboundRelationship
                                    Dim outbound() As POQM_MT000001UV03SourceOf2 = DirectCast(grouper.outboundRelationship, gloEMeasure.POQM_MT000001UV03SourceOf2())
                                    If Not IsNothing(outbound) Then

                                        If Not IsNothing(DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2())) Then
                                            If DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2()).Length > 0 Then
                                                ' If outbound(0).typeCode = ActRelationshipType.COMP Then


                                                For h As Integer = 0 To DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2()).Length - 1
                                                    If Not IsNothing(DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2())(h)) Then
                                                        If Convert.ToString(DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2())(h).Item) = "gloEMeasure.POQM_MT000001UV03CriteriaReference" Then
                                                            Dim criteriaref As POQM_MT000001UV03CriteriaReference = Nothing
                                                            criteriaref = DirectCast(DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2())(h).Item, gloEMeasure.POQM_MT000001UV03CriteriaReference)
                                                            If Not IsNothing(criteriaref) Then
                                                                'If _CMsID.ToUpper = "CMS165V4".ToUpper Then

                                                                'End If
                                                                If criteriaref.classCode.ToString() = ActClassRoot.GROUPER.ToString() Or criteriaref.classCode.ToString() = ActClassRoot.OBS.ToString() Or criteriaref.classCode.ToString() = ActClassRoot.ACT.ToString() Or criteriaref.classCode.ToString() = ActClassRoot.SBADM.ToString() Or criteriaref.classCode.ToString() = ActClassRoot.PROC.ToString() Then
                                                                    If criteriaref.classCode = ActClassRoot.ENC Then
                                                                        Root = Root
                                                                    End If
                                                                    Root = criteriaref.id.root

                                                                    Extension = Convert.ToString(criteriaref.id.extension).Replace("'", "''")
                                                                    Dim drRow() As DataRow = dtDataCriteria.Select("GroupID= '" + Root + "'")
                                                                    If drRow.Length > 0 Then
                                                                        If _CMSIDNumber = 69 Or _CMSIDNumber = 147 Or _CMSIDNumber = 22 Then
                                                                            For g As Integer = 0 To drRow.Length - 1

                                                                                If Convert.ToString(drRow(g)("GroupID")) = "" Then
                                                                                    drRow(g)("GroupID") = Convert.ToString(grouper.id.root)
                                                                                End If
                                                                            Next
                                                                        Else
                                                                            For g As Integer = 0 To drRow.Length - 1

                                                                                drRow(g)("GroupID") = Convert.ToString(grouper.id.root)

                                                                            Next
                                                                        End If

                                                                    Else
                                                                        Root = criteriaref.id.root
                                                                        Extension = Convert.ToString(criteriaref.id.extension).Replace("'", "''")

                                                                        Dim drRow1() As DataRow = dtDataCriteria.Select("Root= '" + Root + "'")
                                                                        If _CMSIDNumber = 69 Or _CMSIDNumber = 147 Or _CMSIDNumber = 22 Then
                                                                            For g As Integer = 0 To drRow1.Length - 1

                                                                                If Convert.ToString(drRow1(g)("GroupID")) = "" Then
                                                                                    drRow1(g)("GroupID") = Convert.ToString(grouper.id.root)
                                                                                End If
                                                                            Next
                                                                        Else
                                                                            For g As Integer = 0 To drRow1.Length - 1

                                                                                If Convert.ToString(drRow1(g)("GroupID")) = "" Then
                                                                                    drRow1(g)("GroupID") = Convert.ToString(grouper.id.root)
                                                                                End If
                                                                            Next
                                                                        End If

                                                                        drRow1 = Nothing
                                                                    End If

                                                                    drRow = Nothing

                                                                Else


                                                                    Root = criteriaref.id.root
                                                                    Extension = Convert.ToString(criteriaref.id.extension).Replace("'", "''")
                                                                    Dim drRow() As DataRow
                                                                    'If _CMsID.ToUpper = "cms165v4".ToUpper Then
                                                                    '    drRow = dtDataCriteria.Select("GroupID= '" + Root + "'")
                                                                    'Else
                                                                    drRow = dtDataCriteria.Select("Root= '" + Root + "'")
                                                                    'End If

                                                                    If drRow.Length > 0 Then
                                                                        If _CMSIDNumber = 147 Or _CMSIDNumber = 22 Then
                                                                            If Convert.ToString(drRow(0)("GroupID")) = "" Then
                                                                                drRow(0)("GroupID") = Convert.ToString(grouper.id.root)
                                                                            End If
                                                                        Else
                                                                            drRow(0)("GroupID") = Convert.ToString(grouper.id.root)
                                                                        End If


                                                                    End If
                                                                    drRow = Nothing
                                                                End If
                                                            End If
                                                            criteriaref = Nothing
                                                        End If

                                                    End If
                                                Next

                                            End If
                                        End If

                                    End If
                                    outbound = Nothing
                                End If
                            End If
                            grouper = Nothing
                        End If

                        SourceOF = Nothing
                    End If
                Next
            End If
            UpdateGroupRoot(dtDataCriteria, DataCriteria)
            UpdateGrouperCriteria(dtDataCriteria, DataCriteria)
            DataCriteria = Nothing
        End If
        ' Update Group Criteria for invidual sections

        Return dtDataCriteria
    End Function
    Private Sub UpdateGrouperCriteria(ByRef dtDataCriteria As DataTable, ByVal DataCriteria As POQM_MT000001UV03DataCriteriaSection)
        Dim Root As String = ""
        Dim extension As String = ""
        If Not IsNothing(DataCriteria.entry) Then
            If DataCriteria.entry.Length > 0 Then
                For k As Integer = 0 To DataCriteria.entry.Length - 1
                    Dim SourceOF As POQM_MT000001UV03SourceOf = Nothing
                    SourceOF = DataCriteria.entry(k)
                    If Not IsNothing(SourceOF.Item) Then
                        If Convert.ToString(SourceOF.Item) = "gloEMeasure.POQM_MT000001UV03GrouperCriteria" Then
                            Dim grouper As POQM_MT000001UV03GrouperCriteria = Nothing
                            grouper = DirectCast(SourceOF.Item, gloEMeasure.POQM_MT000001UV03GrouperCriteria)
                            ''ReadTemporalInfo(grouper, dtDataCriteria, True)
                            Dim temporallyrelatedinfo() As POQM_MT000001UV03SourceOf4 = DirectCast(grouper.temporallyRelatedInformation, gloEMeasure.POQM_MT000001UV03SourceOf4())

                            If Not IsNothing(temporallyrelatedinfo) Then
                                If temporallyrelatedinfo.Length > 0 Then


                                    For h As Integer = 0 To DirectCast(temporallyrelatedinfo, gloEMeasure.POQM_MT000001UV03SourceOf4()).Length - 1
                                        If Not IsNothing(DirectCast(temporallyrelatedinfo, gloEMeasure.POQM_MT000001UV03SourceOf4())(h)) Then
                                            If Convert.ToString(DirectCast(temporallyrelatedinfo, gloEMeasure.POQM_MT000001UV03SourceOf4())(h).Item) = "gloEMeasure.POQM_MT000001UV03CriteriaReference" Then
                                                Dim criteriaref As POQM_MT000001UV03CriteriaReference = Nothing
                                                criteriaref = DirectCast(DirectCast(temporallyrelatedinfo, gloEMeasure.POQM_MT000001UV03SourceOf4())(h).Item, gloEMeasure.POQM_MT000001UV03CriteriaReference)
                                                If Not IsNothing(criteriaref) Then
                                                    If criteriaref.classCode.ToString() = ActClassRoot.GROUPER.ToString() Or criteriaref.classCode.ToString() = ActClassRoot.PROC.ToString() Then
                                                        Root = criteriaref.id.root
                                                        extension = Convert.ToString(criteriaref.id.extension).Replace("'", "''")
                                                        Dim drRow() As DataRow = dtDataCriteria.Select("GroupID= '" + Root + "'")

                                                        If drRow.Length > 0 Then
                                                            For g As Integer = 0 To drRow.Length - 1
                                                                drRow(g)("GroupID") = Convert.ToString(grouper.id.root)
                                                            Next
                                                        End If

                                                        'End If

                                                        drRow = Nothing
                                                    Else
                                                        ' check this code
                                                        'Root = criteriaref.id.root
                                                        'Extension = Convert.ToString(criteriaref.id.extension).Replace("'", "''")

                                                        'Dim drRow1() As DataRow = dtDataCriteria.Select("Root= '" + Root + "'")

                                                        '    For g As Integer = 0 To drRow1.Length - 1

                                                        '        If Convert.ToString(drRow1(g)("GroupID")) = "" Then
                                                        '            drRow1(g)("GroupID") = Convert.ToString(grouper.id.root)
                                                        '        End If
                                                        '    Next
                                                        'drRow1 = Nothing
                                                    End If
                                                End If
                                                criteriaref = Nothing
                                            End If

                                        End If
                                    Next
                                End If

                            End If

                            If Not IsNothing(grouper.outboundRelationship) Then
                                If grouper.outboundRelationship.Length > 0 Then
                                    '  Dim outbound = grouper.outboundRelationship
                                    Dim outbound() As POQM_MT000001UV03SourceOf2 = DirectCast(grouper.outboundRelationship, gloEMeasure.POQM_MT000001UV03SourceOf2())
                                    If Not IsNothing(outbound) Then

                                        If Not IsNothing(DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2())) Then
                                            If DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2()).Length > 0 Then
                                                ' If outbound(0).typeCode = ActRelationshipType.COMP Then


                                                For h As Integer = 0 To DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2()).Length - 1
                                                    If Not IsNothing(DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2())(h)) Then
                                                        If Convert.ToString(DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2())(h).Item) = "gloEMeasure.POQM_MT000001UV03CriteriaReference" Then
                                                            Dim criteriaref As POQM_MT000001UV03CriteriaReference = Nothing
                                                            criteriaref = DirectCast(DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2())(h).Item, gloEMeasure.POQM_MT000001UV03CriteriaReference)
                                                            If Not IsNothing(criteriaref) Then
                                                                'If _CMsID.ToUpper = "CMS165V4".ToUpper Then

                                                                'End If
                                                                If criteriaref.classCode.ToString() = ActClassRoot.GROUPER.ToString() Or criteriaref.classCode.ToString() = ActClassRoot.OBS.ToString() Or criteriaref.classCode.ToString() = ActClassRoot.ACT.ToString() Or criteriaref.classCode.ToString() = ActClassRoot.SBADM.ToString() Or criteriaref.classCode.ToString() = ActClassRoot.ENC.ToString() Or criteriaref.classCode.ToString() = ActClassRoot.PROC.ToString() Then
                                                                    If criteriaref.classCode = ActClassRoot.ENC Then
                                                                        Root = Root
                                                                    End If
                                                                    Root = criteriaref.id.root

                                                                    extension = Convert.ToString(criteriaref.id.extension).Replace("'", "''")
                                                                    Dim drRow() As DataRow = dtDataCriteria.Select("GroupID= '" + Root + "'")
                                                                    If drRow.Length > 0 Then
                                                                        If _CMSIDNumber = 69 Or _CMSIDNumber = 147 Or _CMSIDNumber = 22 Then
                                                                            For g As Integer = 0 To drRow.Length - 1
                                                                                'Check the below commented code
                                                                                'If Convert.ToString(drRow(g)("GroupID")) = "" Then
                                                                                drRow(g)("GroupID") = Convert.ToString(grouper.id.root)
                                                                                'End If
                                                                            Next
                                                                        Else
                                                                            For g As Integer = 0 To drRow.Length - 1

                                                                                drRow(g)("GroupID") = Convert.ToString(grouper.id.root)

                                                                            Next
                                                                        End If

                                                                    Else
                                                                        Root = criteriaref.id.root
                                                                        extension = Convert.ToString(criteriaref.id.extension).Replace("'", "''")

                                                                        Dim drRow1() As DataRow = dtDataCriteria.Select("Root= '" + Root + "'")
                                                                        If _CMSIDNumber = 69 Or _CMSIDNumber = 147 Or _CMSIDNumber = 22 Then
                                                                            For g As Integer = 0 To drRow1.Length - 1

                                                                                If Convert.ToString(drRow1(g)("GroupID")) = "" Then
                                                                                    drRow1(g)("GroupID") = Convert.ToString(grouper.id.root)
                                                                                End If
                                                                            Next
                                                                        Else
                                                                            For g As Integer = 0 To drRow1.Length - 1

                                                                                If Convert.ToString(drRow1(g)("GroupID")) = "" Then
                                                                                    drRow1(g)("GroupID") = Convert.ToString(grouper.id.root)
                                                                                End If
                                                                            Next
                                                                        End If

                                                                        drRow1 = Nothing
                                                                    End If

                                                                    drRow = Nothing

                                                                Else


                                                                    Root = criteriaref.id.root
                                                                    extension = Convert.ToString(criteriaref.id.extension).Replace("'", "''")
                                                                    Dim drRow() As DataRow
                                                                    'If _CMsID.ToUpper = "cms165v4".ToUpper Then
                                                                    '    drRow = dtDataCriteria.Select("GroupID= '" + Root + "'")
                                                                    'Else
                                                                    drRow = dtDataCriteria.Select("Root= '" + Root + "'")
                                                                    'End If

                                                                    If drRow.Length > 0 Then
                                                                        If _CMSIDNumber = 147 Or _CMSIDNumber = 22 Then
                                                                            If Convert.ToString(drRow(0)("GroupID")) = "" Then
                                                                                drRow(0)("GroupID") = Convert.ToString(grouper.id.root)
                                                                            End If
                                                                        Else
                                                                            drRow(0)("GroupID") = Convert.ToString(grouper.id.root)
                                                                        End If


                                                                    End If
                                                                    drRow = Nothing
                                                                End If
                                                            End If
                                                            criteriaref = Nothing
                                                        End If

                                                    End If
                                                Next

                                            End If
                                        End If

                                    End If
                                    outbound = Nothing
                                End If
                            End If
                            grouper = Nothing
                        End If

                    End If

                Next

            End If
        End If


    End Sub
    Private Sub UpdateGroupRoot(ByRef dtDataCriteria As DataTable, ByVal DataCriteria As POQM_MT000001UV03DataCriteriaSection)
        Dim Root As String = ""
        Dim extension As String = ""
        If Not IsNothing(DataCriteria.entry) Then
            If DataCriteria.entry.Length > 0 Then
                For k As Integer = 0 To DataCriteria.entry.Length - 1
                    Dim SourceOF As POQM_MT000001UV03SourceOf = Nothing
                    SourceOF = DataCriteria.entry(k)
                    If Not IsNothing(SourceOF.Item) Then
                        If Convert.ToString(SourceOF.Item) = "gloEMeasure.POQM_MT000001UV03ProcedureCriteria" Then
                            Dim proc As POQM_MT000001UV03ProcedureCriteria = Nothing
                            proc = DirectCast(SourceOF.Item, gloEMeasure.POQM_MT000001UV03ProcedureCriteria)

                            Dim temporallyrelatedinfo() As POQM_MT000001UV03SourceOf4 = DirectCast(proc.temporallyRelatedInformation, gloEMeasure.POQM_MT000001UV03SourceOf4())
                            If Not IsNothing(temporallyrelatedinfo) Then
                                If temporallyrelatedinfo.Length > 0 Then
                                    For h As Integer = 0 To DirectCast(temporallyrelatedinfo, gloEMeasure.POQM_MT000001UV03SourceOf4()).Length - 1
                                        If Not IsNothing(DirectCast(temporallyrelatedinfo, gloEMeasure.POQM_MT000001UV03SourceOf4())(h)) Then
                                            If Convert.ToString(DirectCast(temporallyrelatedinfo, gloEMeasure.POQM_MT000001UV03SourceOf4())(h).Item) = "gloEMeasure.POQM_MT000001UV03CriteriaReference" Then
                                                Dim criteriaref As POQM_MT000001UV03CriteriaReference = Nothing
                                                criteriaref = DirectCast(DirectCast(temporallyrelatedinfo, gloEMeasure.POQM_MT000001UV03SourceOf4())(h).Item, gloEMeasure.POQM_MT000001UV03CriteriaReference)
                                                If Not IsNothing(criteriaref) Then
                                                    If criteriaref.classCode.ToString() = ActClassRoot.GROUPER.ToString() Or criteriaref.classCode.ToString() = ActClassRoot.PROC.ToString() Then
                                                        Root = criteriaref.id.root
                                                        extension = Convert.ToString(criteriaref.id.extension).Replace("'", "''")
                                                        Dim drRow() As DataRow = dtDataCriteria.Select("GroupID= '" + Root + "'")

                                                        If drRow.Length > 0 Then
                                                            For g As Integer = 0 To drRow.Length - 1
                                                                drRow(g)("GroupID") = Convert.ToString(proc.id.root)
                                                            Next
                                                        Else
                                                            Root = criteriaref.id.root
                                                            extension = Convert.ToString(criteriaref.id.extension).Replace("'", "''")
                                                            Dim drRow1() As DataRow = dtDataCriteria.Select("Root= '" + Root + "'")
                                                            For g As Integer = 0 To drRow1.Length - 1

                                                                If Convert.ToString(drRow1(g)("GroupID")) = "" Then
                                                                    drRow1(g)("GroupID") = Convert.ToString(proc.id.root)
                                                                End If
                                                            Next
                                                            drRow1 = Nothing
                                                        End If
                                                        'End If
                                                        drRow = Nothing
                                                    End If
                                                End If
                                                criteriaref = Nothing
                                            End If

                                        End If
                                    Next
                                End If

                            End If
                            If Not IsNothing(proc.outboundRelationship) Then
                                If proc.outboundRelationship.Length > 0 Then
                                    Dim outbound() As POQM_MT000001UV03SourceOf2 = DirectCast(proc.outboundRelationship, gloEMeasure.POQM_MT000001UV03SourceOf2())
                                    If Not IsNothing(outbound) Then
                                        If Not IsNothing(DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2())) Then
                                            If DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2()).Length > 0 Then
                                                For h As Integer = 0 To DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2()).Length - 1
                                                    If Not IsNothing(DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2())(h)) Then
                                                        If Convert.ToString(DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2())(h).Item) = "gloEMeasure.POQM_MT000001UV03CriteriaReference" Then
                                                            Dim criteriaref As POQM_MT000001UV03CriteriaReference = Nothing
                                                            criteriaref = DirectCast(DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2())(h).Item, gloEMeasure.POQM_MT000001UV03CriteriaReference)
                                                            If Not IsNothing(criteriaref) Then
                                                                If criteriaref.classCode.ToString() = ActClassRoot.PROC.ToString() Then
                                                                    Root = criteriaref.id.root
                                                                    extension = Convert.ToString(criteriaref.id.extension).Replace("'", "''")
                                                                    Dim drRow() As DataRow = dtDataCriteria.Select("GroupID= '" + Root + "'")
                                                                    If drRow.Length > 0 Then
                                                                        For g As Integer = 0 To drRow.Length - 1
                                                                            drRow(g)("GroupID") = Convert.ToString(proc.id.root)
                                                                        Next
                                                                    Else
                                                                        Root = criteriaref.id.root
                                                                        extension = Convert.ToString(criteriaref.id.extension).Replace("'", "''")
                                                                        Dim drRow1() As DataRow = dtDataCriteria.Select("Root= '" + Root + "'")
                                                                        For g As Integer = 0 To drRow1.Length - 1
                                                                            'If Convert.ToString(drRow1(g)("GroupID")) = "" Then
                                                                            drRow1(g)("GroupID") = Convert.ToString(proc.id.root)
                                                                            'End If
                                                                        Next
                                                                        drRow1 = Nothing
                                                                    End If
                                                                    drRow = Nothing
                                                                End If
                                                            End If
                                                        End If
                                                    End If
                                                Next
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        ElseIf Convert.ToString(SourceOF.Item) = "gloEMeasure.POQM_MT000001UV03ObservationCriteria" Then

                            Dim OBS As POQM_MT000001UV03ObservationCriteria = Nothing
                            OBS = DirectCast(SourceOF.Item, gloEMeasure.POQM_MT000001UV03ObservationCriteria)
                            Dim temporallyrelatedinfo() As POQM_MT000001UV03SourceOf4 = DirectCast(OBS.temporallyRelatedInformation, gloEMeasure.POQM_MT000001UV03SourceOf4())

                            If Not IsNothing(temporallyrelatedinfo) Then
                                If temporallyrelatedinfo.Length > 0 Then
                                    For h As Integer = 0 To DirectCast(temporallyrelatedinfo, gloEMeasure.POQM_MT000001UV03SourceOf4()).Length - 1
                                        If Not IsNothing(DirectCast(temporallyrelatedinfo, gloEMeasure.POQM_MT000001UV03SourceOf4())(h)) Then
                                            If Convert.ToString(DirectCast(temporallyrelatedinfo, gloEMeasure.POQM_MT000001UV03SourceOf4())(h).Item) = "gloEMeasure.POQM_MT000001UV03CriteriaReference" Then
                                                Dim criteriaref As POQM_MT000001UV03CriteriaReference = Nothing
                                                criteriaref = DirectCast(DirectCast(temporallyrelatedinfo, gloEMeasure.POQM_MT000001UV03SourceOf4())(h).Item, gloEMeasure.POQM_MT000001UV03CriteriaReference)
                                                If Not IsNothing(criteriaref) Then
                                                    If criteriaref.classCode.ToString() = ActClassRoot.GROUPER.ToString() Or criteriaref.classCode.ToString() = ActClassRoot.OBS.ToString() Then
                                                        Root = criteriaref.id.root
                                                        extension = Convert.ToString(criteriaref.id.extension).Replace("'", "''")
                                                        Dim drRow() As DataRow = dtDataCriteria.Select("GroupID= '" + Root + "'")

                                                        If drRow.Length > 0 Then
                                                            For g As Integer = 0 To drRow.Length - 1
                                                                drRow(g)("GroupID") = Convert.ToString(OBS.id.root)
                                                            Next
                                                        Else
                                                            Root = criteriaref.id.root
                                                            extension = Convert.ToString(criteriaref.id.extension).Replace("'", "''")
                                                            Dim drRow1() As DataRow = dtDataCriteria.Select("Root= '" + Root + "'")
                                                            For g As Integer = 0 To drRow1.Length - 1

                                                                If Convert.ToString(drRow1(g)("GroupID")) = "" Then
                                                                    drRow1(g)("GroupID") = Convert.ToString(OBS.id.root)
                                                                End If
                                                            Next
                                                            drRow1 = Nothing
                                                        End If

                                                        'End If

                                                        drRow = Nothing

                                                    End If
                                                End If
                                                criteriaref = Nothing
                                            End If

                                        End If
                                    Next
                                End If

                            End If
                            If Not IsNothing(OBS.outboundRelationship) Then
                                If OBS.outboundRelationship.Length > 0 Then
                                    Dim outbound() As POQM_MT000001UV03SourceOf2 = DirectCast(OBS.outboundRelationship, gloEMeasure.POQM_MT000001UV03SourceOf2())
                                    If Not IsNothing(outbound) Then
                                        If Not IsNothing(DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2())) Then
                                            If DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2()).Length > 0 Then
                                                For h As Integer = 0 To DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2()).Length - 1
                                                    If Not IsNothing(DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2())(h)) Then
                                                        If Convert.ToString(DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2())(h).Item) = "gloEMeasure.POQM_MT000001UV03CriteriaReference" Then
                                                            Dim criteriaref As POQM_MT000001UV03CriteriaReference = Nothing
                                                            criteriaref = DirectCast(DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2())(h).Item, gloEMeasure.POQM_MT000001UV03CriteriaReference)
                                                            If Not IsNothing(criteriaref) Then
                                                                If criteriaref.classCode.ToString() = ActClassRoot.OBS.ToString() Then
                                                                    Root = criteriaref.id.root
                                                                    extension = Convert.ToString(criteriaref.id.extension).Replace("'", "''")
                                                                    Dim drRow() As DataRow = dtDataCriteria.Select("GroupID= '" + Root + "'")
                                                                    If drRow.Length > 0 Then
                                                                        For g As Integer = 0 To drRow.Length - 1
                                                                            drRow(g)("GroupID") = Convert.ToString(OBS.id.root)
                                                                        Next
                                                                    Else
                                                                        Root = criteriaref.id.root
                                                                        extension = Convert.ToString(criteriaref.id.extension).Replace("'", "''")
                                                                        Dim drRow1() As DataRow = dtDataCriteria.Select("Root= '" + Root + "'")
                                                                        For g As Integer = 0 To drRow1.Length - 1
                                                                            If _CMSIDNumber = 166 Then
                                                                                If Convert.ToString(drRow1(g)("GroupID")) = "" Then
                                                                                    drRow1(g)("GroupID") = Convert.ToString(OBS.id.root)
                                                                                End If
                                                                            Else
                                                                                'If Convert.ToString(drRow1(g)("GroupID")) = "" Then
                                                                                drRow1(g)("GroupID") = Convert.ToString(OBS.id.root)
                                                                                'End If
                                                                            End If

                                                                            ''If Convert.ToString(drRow1(g)("GroupID")) = "" Then
                                                                            'drRow1(g)("GroupID") = Convert.ToString(OBS.id.root)
                                                                            ''End If
                                                                        Next
                                                                        drRow1 = Nothing
                                                                    End If
                                                                    drRow = Nothing
                                                                End If
                                                            End If
                                                        End If
                                                    End If
                                                Next
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        ElseIf Convert.ToString(SourceOF.Item) = "gloEMeasure.POQM_MT000001UV03EncounterCriteria" Then
                            Dim ENC As POQM_MT000001UV03EncounterCriteria = Nothing
                            ENC = DirectCast(SourceOF.Item, gloEMeasure.POQM_MT000001UV03EncounterCriteria)
                            Dim temporallyrelatedinfo() As POQM_MT000001UV03SourceOf4 = DirectCast(ENC.temporallyRelatedInformation, gloEMeasure.POQM_MT000001UV03SourceOf4())

                            If Not IsNothing(temporallyrelatedinfo) Then
                                If temporallyrelatedinfo.Length > 0 Then
                                    For h As Integer = 0 To DirectCast(temporallyrelatedinfo, gloEMeasure.POQM_MT000001UV03SourceOf4()).Length - 1
                                        If Not IsNothing(DirectCast(temporallyrelatedinfo, gloEMeasure.POQM_MT000001UV03SourceOf4())(h)) Then
                                            If Convert.ToString(DirectCast(temporallyrelatedinfo, gloEMeasure.POQM_MT000001UV03SourceOf4())(h).Item) = "gloEMeasure.POQM_MT000001UV03CriteriaReference" Then
                                                Dim criteriaref As POQM_MT000001UV03CriteriaReference = Nothing
                                                criteriaref = DirectCast(DirectCast(temporallyrelatedinfo, gloEMeasure.POQM_MT000001UV03SourceOf4())(h).Item, gloEMeasure.POQM_MT000001UV03CriteriaReference)
                                                If Not IsNothing(criteriaref) Then
                                                    If criteriaref.classCode.ToString() = ActClassRoot.GROUPER.ToString() Or criteriaref.classCode.ToString() = ActClassRoot.ENC.ToString() Or criteriaref.classCode.ToString() = ActClassRoot.SBADM.ToString() Then
                                                        Root = criteriaref.id.root
                                                        extension = Convert.ToString(criteriaref.id.extension).Replace("'", "''")
                                                        Dim drRow() As DataRow = dtDataCriteria.Select("GroupID= '" + Root + "'")

                                                        If drRow.Length > 0 Then
                                                            For g As Integer = 0 To drRow.Length - 1
                                                                drRow(g)("GroupID") = Convert.ToString(ENC.id.root)
                                                            Next
                                                        Else
                                                            Root = criteriaref.id.root
                                                            extension = Convert.ToString(criteriaref.id.extension).Replace("'", "''")
                                                            Dim drRow1() As DataRow = dtDataCriteria.Select("Root= '" + Root + "'")
                                                            For g As Integer = 0 To drRow1.Length - 1

                                                                If Convert.ToString(drRow1(g)("GroupID")) = "" Then
                                                                    drRow1(g)("GroupID") = Convert.ToString(ENC.id.root)
                                                                End If
                                                            Next
                                                            drRow1 = Nothing
                                                        End If

                                                        'End If

                                                        drRow = Nothing

                                                    End If
                                                End If
                                                criteriaref = Nothing
                                            End If

                                        End If
                                    Next
                                End If

                            End If
                            If Not IsNothing(ENC.outboundRelationship) Then
                                If ENC.outboundRelationship.Length > 0 Then
                                    Dim outbound() As POQM_MT000001UV03SourceOf2 = DirectCast(ENC.outboundRelationship, gloEMeasure.POQM_MT000001UV03SourceOf2())
                                    If Not IsNothing(outbound) Then
                                        If Not IsNothing(DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2())) Then
                                            If DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2()).Length > 0 Then
                                                For h As Integer = 0 To DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2()).Length - 1
                                                    If Not IsNothing(DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2())(h)) Then
                                                        If Convert.ToString(DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2())(h).Item) = "gloEMeasure.POQM_MT000001UV03CriteriaReference" Then
                                                            Dim criteriaref As POQM_MT000001UV03CriteriaReference = Nothing
                                                            criteriaref = DirectCast(DirectCast(outbound, gloEMeasure.POQM_MT000001UV03SourceOf2())(h).Item, gloEMeasure.POQM_MT000001UV03CriteriaReference)
                                                            If Not IsNothing(criteriaref) Then
                                                                If criteriaref.classCode.ToString() = ActClassRoot.ENC.ToString() Or criteriaref.classCode.ToString() = ActClassRoot.SBADM.ToString() Then
                                                                    Root = criteriaref.id.root
                                                                    extension = Convert.ToString(criteriaref.id.extension).Replace("'", "''")
                                                                    Dim drRow() As DataRow = dtDataCriteria.Select("GroupID= '" + Root + "'")
                                                                    If drRow.Length > 0 Then
                                                                        For g As Integer = 0 To drRow.Length - 1
                                                                            drRow(g)("GroupID") = Convert.ToString(ENC.id.root)
                                                                        Next
                                                                    Else
                                                                        Root = criteriaref.id.root
                                                                        extension = Convert.ToString(criteriaref.id.extension).Replace("'", "''")
                                                                        Dim drRow1() As DataRow = dtDataCriteria.Select("Root= '" + Root + "'")
                                                                        For g As Integer = 0 To drRow1.Length - 1
                                                                            'If Convert.ToString(drRow1(g)("GroupID")) = "" Then
                                                                            drRow1(g)("GroupID") = Convert.ToString(ENC.id.root)
                                                                            'End If
                                                                        Next
                                                                        drRow1 = Nothing
                                                                    End If
                                                                    drRow = Nothing
                                                                End If
                                                            End If
                                                        End If
                                                    End If
                                                Next
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If

                Next
            End If
        End If

    End Sub
    Private Sub ReadValueSetDetails(ByVal Criteria As Object, ByVal dtDataCriteria As DataTable)
        Dim Root As String = ""
        Dim Extension As String = ""
        Dim ValueSet As String = ""
        Dim Title As String = ""
        Dim _isNegation As Boolean = False
        If Criteria.actionNegationInd = True Then
            _isNegation = True
        End If
        If Not IsNothing(Criteria.id) Then
            Root = Criteria.id.root
            Extension = Criteria.id.extension

        End If
        If Not IsNothing(Criteria.title) Then
            If Not IsNothing(Criteria.title.value) Then
                Title = Criteria.title.value

            End If
        End If
        Try
            If Convert.ToString(Criteria) = "gloEMeasure.POQM_MT000001UV03ActCriteria" Then
                If Not IsNothing(DirectCast(Criteria, gloEMeasure.POQM_MT000001UV03ActCriteria).code) Then
                    If Convert.ToString(DirectCast(Criteria, gloEMeasure.POQM_MT000001UV03ActCriteria).code.flavorId) = "" Then
                        If Title = "Laboratory Test, Performed" Then
                            If Convert.ToString(DirectCast(Criteria, gloEMeasure.POQM_MT000001UV03ActCriteria).code) <> "gloEMeasure.CD" Then
                                ValueSet = DirectCast(DirectCast(Criteria, gloEMeasure.POQM_MT000001UV03ActCriteria).code, gloEMeasure.CD).valueSet
                            End If
                        Else
                            ValueSet = DirectCast(DirectCast(Criteria, gloEMeasure.POQM_MT000001UV03ActCriteria).code, gloEMeasure.CD).valueSet
                        End If


                    End If


                End If
            ElseIf Convert.ToString(Criteria) = "gloEMeasure.POQM_MT000001UV03ObservationCriteria" Then
                If Not IsNothing(DirectCast(Criteria, gloEMeasure.POQM_MT000001UV03ObservationCriteria).value) Then
                    'If Convert.ToString(DirectCast(Criteria, gloEMeasure.POQM_MT000001UV03ObservationCriteria).code.flavorId) = "" Then
                    '    If Title = "Laboratory Test, Performed" Then
                    '        If Convert.ToString(DirectCast(Criteria, gloEMeasure.POQM_MT000001UV03ObservationCriteria).code) <> "gloEMeasure.CD" Then
                    '            ValueSet = DirectCast(DirectCast(Criteria, gloEMeasure.POQM_MT000001UV03ObservationCriteria).code, gloEMeasure.CD).valueSet
                    '        End If
                    '    Else
                    ValueSet = DirectCast(DirectCast(Criteria, gloEMeasure.POQM_MT000001UV03ObservationCriteria).value, gloEMeasure.CD).valueSet
                    'End If
                    'End If
                End If
            End If


        Catch ex As Exception

        End Try
        Try
            If ValueSet = "" Then
                If Not IsNothing(Criteria.code) Then
                    ValueSet = Criteria.code.valueSet

                End If
            End If
        Catch ex As Exception

        End Try

        If Convert.ToString(ValueSet) <> "" Then
            Dim dr As DataRow = Nothing
            dr = dtDataCriteria.NewRow()
            dr(0) = Root
            dr(1) = Extension
            dr(2) = ValueSet
            dr(3) = Title
            dr(8) = _isNegation

            dtDataCriteria.Rows.Add(dr)
            dr = Nothing
        ElseIf Title = "Patient Characteristic Birthdate" Then

            Dim dr As DataRow = Nothing
            dr = dtDataCriteria.NewRow()
            dr(0) = Root
            dr(1) = Extension
            dr(2) = ValueSet
            dr(3) = Title
            dr(8) = _isNegation

            dtDataCriteria.Rows.Add(dr)
            dr = Nothing
        End If


        'If Convert.ToString(ValueSet) <> "" Then
        '    Dim dr As DataRow = Nothing
        '    dr = dtDataCriteria.NewRow()
        '    dr(0) = Root
        '    dr(1) = Extension
        '    dr(2) = ValueSet
        '    dr(3) = Title
        '    dtDataCriteria.Rows.Add(dr)
        '    dr = Nothing
        'End If
    End Sub
    Private Sub ReadTemporalInfo(ByVal Criteria As Object, ByVal dtDataCriteria As DataTable, Optional ByVal _isGrouper As Boolean = False)
        Dim temporallyrelatedinfo() As POQM_MT000001UV03SourceOf4 = DirectCast(Criteria.temporallyRelatedInformation, gloEMeasure.POQM_MT000001UV03SourceOf4())
        If Not IsNothing(temporallyrelatedinfo) Then
            If temporallyrelatedinfo.Length > 0 Then
                If _isGrouper Then
                    For h As Integer = 0 To DirectCast(temporallyrelatedinfo, gloEMeasure.POQM_MT000001UV03SourceOf4()).Length - 1
                        If Not IsNothing(DirectCast(temporallyrelatedinfo, gloEMeasure.POQM_MT000001UV03SourceOf4())(h)) Then
                            If Convert.ToString(DirectCast(temporallyrelatedinfo, gloEMeasure.POQM_MT000001UV03SourceOf4())(h).Item) = "gloEMeasure.POQM_MT000001UV03CriteriaReference" Then
                                Dim criteriaref As POQM_MT000001UV03CriteriaReference = Nothing
                                criteriaref = DirectCast(DirectCast(temporallyrelatedinfo, gloEMeasure.POQM_MT000001UV03SourceOf4())(h).Item, gloEMeasure.POQM_MT000001UV03CriteriaReference)
                                If Not IsNothing(criteriaref) Then
                                    If criteriaref.classCode = ActClassRoot.GROUPER Then
                                        'Root = criteriaref.id.root
                                        'Extension = Convert.ToString(criteriaref.id.extension).Replace("'", "''")
                                        Dim drRow() As DataRow = dtDataCriteria.Select("GroupID= '" + criteriaref.id.root + "'")
                                        'If _CMsID.ToUpper = "CMS90V5".ToUpper Then
                                        'Else
                                        If drRow.Length > 0 Then
                                            For g As Integer = 0 To drRow.Length - 1
                                                '  If Convert.ToString(drRow(g)("GroupID")) = "" Then
                                                drRow(g)("GroupID") = Convert.ToString(Criteria.id.root)
                                                '  End If
                                            Next

                                        End If

                                        'End If

                                        drRow = Nothing
                                    Else

                                        'Root = criteriaref.id.root
                                        'Extension = Convert.ToString(criteriaref.id.extension).Replace("'", "''")

                                        'Dim drRow() As DataRow = dtDataCriteria.Select("Root= '" + criteriaref.id.root + "'")
                                        'If drRow.Length > 0 Then

                                        '    drRow(0)("GroupID") = Convert.ToString(criteriaref.id.root)
                                        'End If
                                        'drRow = Nothing
                                    End If
                                End If
                                criteriaref = Nothing
                            End If

                        End If
                    Next
                Else
                    If _CMsID.ToUpper = "CMS56V4".ToUpper Or _CMsID.ToUpper = "CMS66V4".ToUpper Or _CMsID.ToUpper = "CMS90V5".ToUpper Then

                    Else


                        'If _CMsID.ToUpper = "CMS90V5".ToUpper Then
                        'Else
                        'If _isEncounter = True AndAlso Criteria.temporallyRelatedInformation(0).typeCode <> "DURING" Then
                        '    Exit Sub

                        'End If

                        For h As Integer = 0 To DirectCast(temporallyrelatedinfo, gloEMeasure.POQM_MT000001UV03SourceOf4()).Length - 1
                            If Not IsNothing(DirectCast(temporallyrelatedinfo, gloEMeasure.POQM_MT000001UV03SourceOf4())(h)) Then
                                If Convert.ToString(DirectCast(temporallyrelatedinfo, gloEMeasure.POQM_MT000001UV03SourceOf4())(h).Item) = "gloEMeasure.POQM_MT000001UV03CriteriaReference" Then
                                    Dim criteriaref As POQM_MT000001UV03CriteriaReference = Nothing
                                    criteriaref = DirectCast(DirectCast(temporallyrelatedinfo, gloEMeasure.POQM_MT000001UV03SourceOf4())(h).Item, gloEMeasure.POQM_MT000001UV03CriteriaReference)
                                    If Not IsNothing(criteriaref) Then
                                        If criteriaref.classCode = ActClassRoot.GROUPER Then
                                            'Root = criteriaref.id.root
                                            'Extension = Convert.ToString(criteriaref.id.extension).Replace("'", "''")
                                            Dim drRow() As DataRow = dtDataCriteria.Select("GroupID= '" + criteriaref.id.root + "'")
                                            'If _CMsID.ToUpper = "CMS90V5".ToUpper Then
                                            'Else
                                            If drRow.Length > 0 Then
                                                For g As Integer = 0 To drRow.Length - 1
                                                    '  If Convert.ToString(drRow(g)("GroupID")) = "" Then
                                                    drRow(g)("GroupID") = Convert.ToString(Criteria.id.root)
                                                    '  End If
                                                Next

                                            End If

                                            'End If

                                            drRow = Nothing
                                        Else

                                            'Root = criteriaref.id.root
                                            'Extension = Convert.ToString(criteriaref.id.extension).Replace("'", "''")

                                            'Dim drRow() As DataRow = dtDataCriteria.Select("Root= '" + criteriaref.id.root + "'")
                                            'If drRow.Length > 0 Then

                                            '    drRow(0)("GroupID") = Convert.ToString(criteriaref.id.root)
                                            'End If
                                            'drRow = Nothing
                                        End If
                                    End If
                                    criteriaref = Nothing
                                End If

                            End If
                        Next
                    End If

                    'End If

                    ' End If
                End If

            End If
        End If
    End Sub
End Class
