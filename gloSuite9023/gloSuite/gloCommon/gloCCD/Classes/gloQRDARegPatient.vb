Public Class gloQRDARegPatient
    Dim userid As Long = 0
    Dim username As String = ""
    Public Function RegisterNew_Patient(ByVal oPatient As gloCCDLibrary.Patient, ByVal IsPatientAllDetails As Boolean, ByVal _ProviderName As String, Optional ByVal blnRegProvider As Boolean = False, Optional ByVal _rootguid As String = "") As Int64
        Dim _PatientID As Int64
        'Dim conn As New SqlConnection
        Dim _DupPatID As Int64
        Dim _CCDID As Int64 = 0
        Dim _Language As String = ""
        Dim obj As New gloPatientRegDBLayer()
        Dim objCCDDatabaseLayer As gloCCDDatabaseLayer = Nothing
        gloLibCCDGeneral.Connectionstring = gloGlobal.gloPMGlobal.DatabaseConnectionString
        Dim ogloPatient As gloPatient.gloPatient = New gloPatient.gloPatient(gloLibCCDGeneral.Connectionstring)

        Try

            If Not IsNothing(oPatient) Then
                If (blnRegProvider) Then  ''if register provider flag true then register provider otherwise keep it as it is
                    Dim _ProviderID As Int64 = RegisterProvider(oPatient, _ProviderName)
                    If (_ProviderID > 0) Then
                        oPatient.PatientDemographics.DemographicsDetail.PatientProviderID = _ProviderID
                    End If
                End If
                _DupPatID = getDuplicatePatient(oPatient)
                If _DupPatID = -1 Then


                    getUser()
                    'If Not IsNothing(oPatient.Clinic) Then
                    '    UpdateClinicTaxID(oPatient.Clinic.ClinicTaxID)
                    'End If

                    _PatientID = ogloPatient.Add(oPatient.PatientDemographics)
                    AddDatatoDB(_PatientID, _rootguid)
                    obj.SavePatientAccount(_PatientID)
                    ogloPatient.AddQRDAPatientInsurance(_PatientID, oPatient.PatientDemographics.Insurances, oPatient.PatientDemographics)
                    Dim ogloPatientRegDBLayer As New gloPatientRegDBLayer(gloLibCCDGeneral.Connectionstring)
                    If Not IsNothing(oPatient.PatientProblems) Then
                        'Dim ogloPatientProblemCol As ProblemsCol = oPatient.PatientProblems
                        If oPatient.PatientProblems.Count > 0 Then
                            ogloPatientRegDBLayer.SaveQRDAProblemList(_PatientID, oPatient.PatientProblems, userid, username, _ProviderName)
                        End If
                    End If
                    If Not IsNothing(oPatient.PatientEncounters) Then
                        If oPatient.PatientEncounters.Count > 0 Then
                            ogloPatientRegDBLayer.SaveQRDAPatientEncounters(_PatientID, oPatient.PatientEncounters, userid, oPatient.PatientDemographics.DemographicsDetail.PatientProviderID)
                        End If
                    End If
                    If Not IsNothing(oPatient.PatientMedications) Then
                        If oPatient.PatientMedications.Count > 0 Then
                            ogloPatientRegDBLayer.SaveQRDAMedication(_PatientID, oPatient.PatientMedications, userid, username, False)
                        End If
                    End If
                    If Not IsNothing(oPatient.PatientHistory) Then
                        If oPatient.PatientHistory.Count > 0 Then
                            ogloPatientRegDBLayer.SaveQRDAPatientHistory(_PatientID, oPatient.PatientHistory, username)
                        End If
                    End If
                    If Not IsNothing(oPatient.PatientLabResult) Then
                        If oPatient.PatientLabResult.Count > 0 Then
                            ogloPatientRegDBLayer.SaveQRDALabResult(_PatientID, oPatient.PatientLabResult, userid, username, oPatient.PatientDemographics.DemographicsDetail.PatientProviderID)
                        End If
                    End If
                    If Not IsNothing(oPatient.PatientVitals) Then
                        If oPatient.PatientVitals.Count > 0 Then
                            ogloPatientRegDBLayer.AddQRDAVitals(_PatientID, oPatient.PatientVitals, userid, username)
                        End If
                    End If
                    If Not IsNothing(oPatient.PatientImmunizations) Then
                        If oPatient.PatientImmunizations.Count > 0 Then
                            ogloPatientRegDBLayer.SaveQRDAImmunization(_PatientID, oPatient.PatientImmunizations, userid, username, oPatient.PatientDemographics.DemographicsDetail.PatientProviderID)
                        End If
                    End If
                    If Not IsNothing(ogloPatientRegDBLayer) Then
                        ogloPatientRegDBLayer.Dispose()
                        ogloPatientRegDBLayer = Nothing
                    End If

                Else
                    If CheckDupPatRootid(_DupPatID, _rootguid) = 0 Then

                        Dim ogloPatientRegDBLayer As New gloPatientRegDBLayer(gloLibCCDGeneral.Connectionstring)
                        If Not IsNothing(oPatient.PatientProblems) Then
                            'Dim ogloPatientProblemCol As ProblemsCol = oPatient.PatientProblems
                            If oPatient.PatientProblems.Count > 0 Then
                                ogloPatientRegDBLayer.SaveQRDAProblemList(_DupPatID, oPatient.PatientProblems, userid, username, _ProviderName)
                            End If
                        End If
                        If Not IsNothing(oPatient.PatientEncounters) Then
                            If oPatient.PatientEncounters.Count > 0 Then
                                ogloPatientRegDBLayer.SaveQRDAPatientEncounters(_DupPatID, oPatient.PatientEncounters, userid, oPatient.PatientDemographics.DemographicsDetail.PatientProviderID)
                            End If
                        End If
                        If Not IsNothing(oPatient.PatientMedications) Then
                            If oPatient.PatientMedications.Count > 0 Then
                                ogloPatientRegDBLayer.SaveQRDAMedication(_DupPatID, oPatient.PatientMedications, userid, username, False)
                            End If
                        End If
                        If Not IsNothing(oPatient.PatientHistory) Then
                            If oPatient.PatientHistory.Count > 0 Then
                                ogloPatientRegDBLayer.SaveQRDAPatientHistory(_DupPatID, oPatient.PatientHistory, username)
                            End If
                        End If
                        If Not IsNothing(oPatient.PatientLabResult) Then
                            If oPatient.PatientLabResult.Count > 0 Then
                                ogloPatientRegDBLayer.SaveQRDALabResult(_DupPatID, oPatient.PatientLabResult, userid, username, oPatient.PatientDemographics.DemographicsDetail.PatientProviderID)
                            End If
                        End If
                        If Not IsNothing(oPatient.PatientVitals) Then
                            If oPatient.PatientVitals.Count > 0 Then
                                ogloPatientRegDBLayer.AddQRDAVitals(_DupPatID, oPatient.PatientVitals, userid, username)
                            End If
                        End If
                        If Not IsNothing(oPatient.PatientImmunizations) Then
                            If oPatient.PatientImmunizations.Count > 0 Then
                                ogloPatientRegDBLayer.SaveQRDAImmunization(_DupPatID, oPatient.PatientImmunizations, userid, username, oPatient.PatientDemographics.DemographicsDetail.PatientProviderID)
                            End If
                        End If
                        If Not IsNothing(ogloPatientRegDBLayer) Then
                            ogloPatientRegDBLayer.Dispose()
                            ogloPatientRegDBLayer = Nothing
                        End If
                    End If
                End If
            End If

            'Code Start-Added by kanchan on 20101011 for Modular CCD Rendering & save
            'Add Race,Ethnicity & Language in Category_Mst,if not exists
            objCCDDatabaseLayer = New gloCCDDatabaseLayer()
            'Code Start Added by kanchan on 20101113 to save patient code as external code
            If oPatient.PatientDemographics.DemographicsDetail.PatientExternalCode <> "" Then
                objCCDDatabaseLayer.UpdatePatientExternalCode(_PatientID, oPatient.PatientDemographics.DemographicsDetail.PatientExternalCode)
            End If
            If oPatient.Race <> "" Then
                objCCDDatabaseLayer.UpdateCategoryMaster(oPatient.Race, "Race", _PatientID)
            End If
            If oPatient.ethnicGroupCode <> "" Then
                objCCDDatabaseLayer.UpdateCategoryMaster(oPatient.ethnicGroupCode, "Ethnicity", _PatientID)
            End If
            If Not IsNothing(oPatient.PatientLanguages) AndAlso oPatient.PatientLanguages.Count > 0 Then
                If Not IsNothing(oPatient.PatientLanguages.Item(0).Language) Then
                    _Language = oPatient.PatientLanguages.Item(0).Language
                    If _Language <> "" Then
                        objCCDDatabaseLayer.UpdateCategoryMaster(_Language, "Language", _PatientID)
                    End If
                End If
            End If

            'Code End-Added by kanchan on 20101011 for Modular CCD Rendering & save

            'Code Start-Added by kanchan on 20101102 for registration of all & demo detail option
            'IsPatientAllDetails = True
            If IsPatientAllDetails = True Then
                '' Code Start-Addded by kanchan on 20101011 for Modularwise rendering
                Dim ogloPatientRegDBLayer As New gloPatientRegDBLayer(gloLibCCDGeneral.Connectionstring)

                If Not IsNothing(oPatient.PatientHistory) Then
                    'Dim ogloPatientHistoryCol As gloPatientHistoryCol = oPatient.PatientHistory
                    ogloPatientRegDBLayer.SavePatientHistory(0, 0, _PatientID, oPatient.PatientHistory, "False", oPatient.UserDetails.UserName)
                End If

                'Dim _ProviderName As String = objCCDDatabaseLayer.GetProviderName(objCCDDatabaseLayer.getDefaultProviderId())

                If Not IsNothing(oPatient.PatientProblems) Then
                    'Dim ogloPatientProblemCol As ProblemsCol = oPatient.PatientProblems
                    ogloPatientRegDBLayer.SaveProblemList(_PatientID, oPatient.PatientProblems, oPatient.UserDetails.UserID, oPatient.UserDetails.UserName, _ProviderName)
                End If

                If Not IsNothing(oPatient.PatientVitals) Then
                    'Dim ogloPatientVitalCol As VitalsCol = oPatient.PatientVitals
                    ogloPatientRegDBLayer.AddNewVitals(_PatientID, oPatient.PatientVitals, oPatient.UserDetails.UserID, oPatient.UserDetails.UserName)
                End If

                If Not IsNothing(oPatient.PatientMedications) Then
                    'Dim ogloPatientMedicationCol As MedicationsCol = oPatient.PatientMedications
                    ogloPatientRegDBLayer.SaveMedication(_PatientID, oPatient.PatientMedications, oPatient.UserDetails.UserID, oPatient.UserDetails.UserName)
                End If

                If Not IsNothing(oPatient.PatientImmunizations) Then
                    'Dim ogloPatientImmunizationCol As ImmunizationCol = oPatient.PatientImmunizations
                    ogloPatientRegDBLayer.SaveImmunization(_PatientID, oPatient.PatientImmunizations, oPatient.UserDetails.UserID, oPatient.UserDetails.UserName)
                End If

                If Not IsNothing(oPatient.PatientLabResult) Then
                    'Dim ogloPatientLabResultCol As LabResultsCol = oPatient.PatientLabResult
                    ogloPatientRegDBLayer.SaveLabResult(_PatientID, oPatient.PatientLabResult, oPatient.UserDetails.UserID, oPatient.UserDetails.UserName)
                End If

                If Not IsNothing(ogloPatientRegDBLayer) Then
                    ogloPatientRegDBLayer.Dispose()
                    ogloPatientRegDBLayer = Nothing
                End If

                'Code End-Addded by kanchan on 20101011 for Modularwise rendering
            End If


            Return _PatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return 0
        Finally
            'If conn.State = ConnectionState.Open Then
            '    conn.Close()
            'End If
            'conn.Dispose()
            'conn = Nothing
            _Language = Nothing

            If Not IsNothing(obj) Then
                obj.Dispose()
                obj = Nothing
            End If
            If Not IsNothing(objCCDDatabaseLayer) Then
                objCCDDatabaseLayer.Dispose()
            End If
            If Not IsNothing(ogloPatient) Then
                ogloPatient.Dispose()
                ogloPatient = Nothing
            End If
        End Try

    End Function



  



    Private Sub AddDatatoDB(_PatientID, _rootguid)
        Dim oDB As New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Dim oDBParameter As gloDatabaseLayer.DBParameter
        Dim oTeamMember As PatientSupport = Nothing
        Try



            oDB.Connect(False)


            oDBParameter = New gloDatabaseLayer.DBParameter()
            oDBParameter.ParameterName = "@nPatientID"
            oDBParameter.Value = _PatientID
            oDBParameter.DataType = System.Data.SqlDbType.BigInt
            oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input
            oDBParameters.Add(oDBParameter)


            oDBParameter = New gloDatabaseLayer.DBParameter()
            oDBParameter.ParameterName = "@Rootid"
            oDBParameter.Value = _rootguid
            oDBParameter.DataType = System.Data.SqlDbType.VarChar
            oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input
            oDBParameters.Add(oDBParameter)



            oDB.Execute("gsp_InupPat_RecRootID", oDBParameters)

        Catch ex As Exception

        Finally

            If Not IsNothing(oDB) Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If
            If Not IsNothing(oDBParameters) Then
                oDBParameters.Clear()
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If

        End Try

    End Sub


    Private Function CheckDupPatRootid(_PatientID, _rootguid) As Integer
        Dim oDB As New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Dim oDBParameter As gloDatabaseLayer.DBParameter
        Dim oTeamMember As PatientSupport = Nothing
        Dim cnt As Integer = 0
        Try



            oDB.Connect(False)


            oDBParameter = New gloDatabaseLayer.DBParameter()
            oDBParameter.ParameterName = "@nPatientID"
            oDBParameter.Value = _PatientID
            oDBParameter.DataType = System.Data.SqlDbType.BigInt
            oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input
            oDBParameters.Add(oDBParameter)


            oDBParameter = New gloDatabaseLayer.DBParameter()
            oDBParameter.ParameterName = "@Rootid"
            oDBParameter.Value = _rootguid
            oDBParameter.DataType = System.Data.SqlDbType.VarChar
            oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input
            oDBParameters.Add(oDBParameter)



            cnt = oDB.ExecuteScalar("gsp_getPat_RecRootID", oDBParameters)

        Catch ex As Exception

        Finally

            If Not IsNothing(oDB) Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If
            If Not IsNothing(oDBParameters) Then
                oDBParameters.Clear()
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If

        End Try
        Return cnt
    End Function
    'Private Function UpdateClinicTaxID(ByVal ClinicTaxID As String)
    '    Dim oDB As New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)
    '    Try
    '        oDB.Connect(False)
    '        Dim _sqlQuery As String = "Update Clinic_MST set sTaxID=" & ClinicTaxID & " "


    '        oDB.Execute_Query(_sqlQuery)


    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        oDB.Disconnect()
    '        oDB.Dispose()
    '        oDB = Nothing
    '    End Try
    'End Function
    Public Sub getUser()
        Dim dt As DataTable = New DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)
        Try
            oDB.Connect(False)
            Dim _sqlQuery As String = "SELECT  [nUserID],[sLoginName] FROM [dbo].[User_MST] WHERE nAdministrator=1 AND sLoginName='admin'"


            oDB.Retrive_Query(_sqlQuery, dt)

            If dt.Rows.Count > 0 Then
                userid = Convert.ToInt64(dt.Rows(0)("nUserID"))
                username = Convert.ToString(dt.Rows(0)("sLoginName"))
            End If
        Catch ex As Exception
            Throw ex
        Finally
            oDB.Disconnect()
            oDB.Dispose()
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try

    End Sub
    Public Function RegisterProvider(ByVal oProvider As gloCCDLibrary.Patient, ByRef _ProviderName As String) As Int64
        Dim Providerid As Int64 = 0
        Dim oDB As New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Dim oDBParameter As gloDatabaseLayer.DBParameter
        Dim oTeamMember As PatientSupport = Nothing
        Try


            Dim objPatientSupportCol As PatientSupportCol = oProvider.PatientCareTeam

            If Not IsNothing(objPatientSupportCol.Item(0)) Then

                oTeamMember = objPatientSupportCol.Item(0)
               

                If (IsNothing(oTeamMember.PersonName.FirstName) Or (oTeamMember.PersonName.FirstName = String.Empty)) Then
                    oTeamMember.PersonName.FirstName = "Fake"
                    oTeamMember.PersonName.MiddleName = ""
                End If
                If (IsNothing(oTeamMember.PersonName.LastName) Or (oTeamMember.PersonName.LastName = String.Empty)) Then
                    oTeamMember.PersonName.LastName = "Provider"
                End If
                oDB.Connect(False)

                '@sFirstName
                oDBParameter = New gloDatabaseLayer.DBParameter()
                oDBParameter.ParameterName = "@sFirstName"
                oDBParameter.Value = Convert.ToString(oTeamMember.PersonName.FirstName)
                oDBParameter.DataType = System.Data.SqlDbType.VarChar
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input
                oDBParameters.Add(oDBParameter)


                oDBParameter = New gloDatabaseLayer.DBParameter()
                oDBParameter.ParameterName = "@sMiddleName"
                oDBParameter.Value = Convert.ToString(oTeamMember.PersonName.MiddleName)
                oDBParameter.DataType = System.Data.SqlDbType.VarChar
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input
                oDBParameters.Add(oDBParameter)


                '@sLastName
                oDBParameter = New gloDatabaseLayer.DBParameter()
                oDBParameter.ParameterName = "@sLastName"
                oDBParameter.Value = Convert.ToString(oTeamMember.PersonName.LastName)
                oDBParameter.DataType = System.Data.SqlDbType.VarChar
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input
                oDBParameters.Add(oDBParameter)



                '@dtDOB
                oDBParameter = New gloDatabaseLayer.DBParameter()
                oDBParameter.ParameterName = "@sAddress"
                oDBParameter.Value = Convert.ToString(oTeamMember.PersonContactAddress.AddressLine2)
                oDBParameter.DataType = System.Data.SqlDbType.VarChar
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input
                oDBParameters.Add(oDBParameter)

                oDBParameter = New gloDatabaseLayer.DBParameter()
                oDBParameter.ParameterName = "@streetaddress"
                oDBParameter.Value = Convert.ToString(oTeamMember.PersonContactAddress.Street)
                oDBParameter.DataType = System.Data.SqlDbType.VarChar
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input
                oDBParameters.Add(oDBParameter)

                oDBParameter = New gloDatabaseLayer.DBParameter()
                oDBParameter.ParameterName = "@sCity"
                oDBParameter.Value = Convert.ToString(oTeamMember.PersonContactAddress.City)
                oDBParameter.DataType = System.Data.SqlDbType.VarChar
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input
                oDBParameters.Add(oDBParameter)


                oDBParameter = New gloDatabaseLayer.DBParameter()
                oDBParameter.ParameterName = "@sState"
                oDBParameter.Value = Convert.ToString(oTeamMember.PersonContactAddress.State)
                oDBParameter.DataType = System.Data.SqlDbType.VarChar
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input
                oDBParameters.Add(oDBParameter)

                oDBParameter = New gloDatabaseLayer.DBParameter()
                oDBParameter.ParameterName = "@sZIP"
                oDBParameter.Value = Convert.ToString(oTeamMember.PersonContactAddress.Zip)
                oDBParameter.DataType = System.Data.SqlDbType.VarChar
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input
                oDBParameters.Add(oDBParameter)

                oDBParameter = New gloDatabaseLayer.DBParameter()
                oDBParameter.ParameterName = "@sNPI"
                oDBParameter.Value = Convert.ToString(oTeamMember.PersonName.ProvNPI)
                oDBParameter.DataType = System.Data.SqlDbType.VarChar
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input
                oDBParameters.Add(oDBParameter)


                oDBParameter = New gloDatabaseLayer.DBParameter()
                oDBParameter.ParameterName = "@sEmployerID"
                oDBParameter.Value = Convert.ToString(oTeamMember.PersonName.ProvidersTaxID)
                oDBParameter.DataType = System.Data.SqlDbType.VarChar
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input
                oDBParameters.Add(oDBParameter)

                oDBParameter = New gloDatabaseLayer.DBParameter()
                oDBParameter.ParameterName = "@sTaxonomy"
                oDBParameter.Value = Convert.ToString(oTeamMember.PersonName.TaxonomyCode)
                oDBParameter.DataType = System.Data.SqlDbType.VarChar
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input
                oDBParameters.Add(oDBParameter)

                oDBParameter = New gloDatabaseLayer.DBParameter()
                oDBParameter.ParameterName = "@sCountry"
                oDBParameter.Value = Convert.ToString(oTeamMember.PersonContactAddress.Country)
                oDBParameter.DataType = System.Data.SqlDbType.VarChar
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input
                oDBParameters.Add(oDBParameter)

                oDBParameter = New gloDatabaseLayer.DBParameter()
                oDBParameter.ParameterName = "@sCounty"
                oDBParameter.Value = Convert.ToString(oTeamMember.PersonContactAddress.County)
                oDBParameter.DataType = System.Data.SqlDbType.VarChar
                oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input
                oDBParameters.Add(oDBParameter)

                Providerid = oDB.ExecuteScalar("gsp_RegisterProvider", oDBParameters)
            End If
        Catch ex As Exception
            Providerid = 0
        Finally

            If Not IsNothing(oDB) Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If
            If Not IsNothing(oDBParameters) Then
                oDBParameters.Clear()
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If
            If (Providerid > 0) Then
                _ProviderName = Convert.ToString(oTeamMember.PersonName.FirstName) + " " + Convert.ToString(oTeamMember.PersonName.LastName)
            End If
        End Try
        Return Providerid
    End Function

    Public Function getDuplicatePatient(ByVal oPatient As gloCCDLibrary.Patient) As Int64
        Dim oDB As New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Dim oDBParameter As gloDatabaseLayer.DBParameter
        Dim DUPPatientID As Int64 = -1

        'Dim isexist As Integer
        oDB.Connect(False)

        '@sFirstName
        oDBParameter = New gloDatabaseLayer.DBParameter()
        oDBParameter.ParameterName = "@sFirstName"
        oDBParameter.Value = oPatient.PatientDemographics.DemographicsDetail.PatientFirstName
        oDBParameter.DataType = System.Data.SqlDbType.VarChar
        oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input
        oDBParameters.Add(oDBParameter)

        '@sMiddleName
        oDBParameter = New gloDatabaseLayer.DBParameter()
        oDBParameter.ParameterName = "@sMiddleName"
        oDBParameter.Value = oPatient.PatientDemographics.DemographicsDetail.PatientMiddleName
        oDBParameter.DataType = System.Data.SqlDbType.VarChar
        oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input
        oDBParameters.Add(oDBParameter)

        '@sLastName
        oDBParameter = New gloDatabaseLayer.DBParameter()
        oDBParameter.ParameterName = "@sLastName"
        oDBParameter.Value = oPatient.PatientDemographics.DemographicsDetail.PatientLastName
        oDBParameter.DataType = System.Data.SqlDbType.VarChar
        oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input
        oDBParameters.Add(oDBParameter)



        '@dtDOB
        oDBParameter = New gloDatabaseLayer.DBParameter()
        oDBParameter.ParameterName = "@dtDOB"
        oDBParameter.Value = oPatient.PatientDemographics.DemographicsDetail.PatientDOB
        oDBParameter.DataType = System.Data.SqlDbType.DateTime
        oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input
        oDBParameters.Add(oDBParameter)

        '@sGender
        oDBParameter = New gloDatabaseLayer.DBParameter()
        oDBParameter.ParameterName = "@sGender"
        oDBParameter.Value = oPatient.PatientDemographics.DemographicsDetail.PatientGender
        oDBParameter.DataType = System.Data.SqlDbType.VarChar
        oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input
        oDBParameters.Add(oDBParameter)

        '@nProviderID, 
        oDBParameter = New gloDatabaseLayer.DBParameter()
        oDBParameter.ParameterName = "@nProviderID"
        oDBParameter.Value = oPatient.PatientDemographics.DemographicsDetail.PatientProviderID
        oDBParameter.DataType = System.Data.SqlDbType.BigInt
        oDBParameter.ParameterDirection = System.Data.ParameterDirection.Input
        oDBParameters.Add(oDBParameter)

        oDBParameter = New gloDatabaseLayer.DBParameter()
        oDBParameter.ParameterName = "@nPatientID"
        oDBParameter.Value = DUPPatientID
        oDBParameter.DataType = System.Data.SqlDbType.BigInt
        oDBParameter.ParameterDirection = System.Data.ParameterDirection.Output
        oDBParameters.Add(oDBParameter)


        ''      oDBParameters.Add("@IsExists", isexist, System.Data.ParameterDirection.Output, System.Data.SqlDbType.Int)
        oDB.Execute("get_DuplicatePatient", oDBParameters, DUPPatientID)

        Return DUPPatientID
    End Function
End Class
