
Imports System.IO
Imports gloCCDLibrary
Imports gloCCDSchema

Imports System.Data.SqlClient
Imports gloDatabaseLayer


Public Class frmPatspecCDAspc
    Dim _patientID As Int64 = 0
    Dim _OpenfrmProblem As Boolean = False

    Dim _OpenfrmHistory As Boolean = False
    Dim _OpenfrmImmunization As Boolean = False
    Dim _OpenfrmMedication As Boolean = False
    Dim _OpenfrmImplants As Boolean = False
    Public Property patientID As Long
        Get

            Return _patientID
        End Get
        Set(ByVal Value As Long)
            _patientID = Value
        End Set
    End Property

    Public Property OpenfrmProblem As Boolean ''Added For NKProblem Functionality
        Get

            Return _OpenfrmProblem
        End Get
        Set(ByVal Value As Boolean)
            _OpenfrmProblem = Value
        End Set
    End Property
    ''
    Public Property OpenfrmHistory As Boolean ''Added For NKAllergies Functionality
        Get

            Return _OpenfrmHistory
        End Get
        Set(ByVal Value As Boolean)
            _OpenfrmHistory = Value
        End Set
    End Property
    Public Property OpenfrmImmunization As Boolean ''Added For NKImmunization Functionality
        Get

            Return _OpenfrmImmunization
        End Get
        Set(ByVal Value As Boolean)
            _OpenfrmImmunization = Value
        End Set
    End Property
    Public Property OpenfrmMedication As Boolean ''Added For NKMedication Functionality
        Get

            Return _OpenfrmMedication
        End Get
        Set(ByVal Value As Boolean)
            _OpenfrmMedication = Value
        End Set
    End Property

    Public Property OpenfrmImplants As Boolean ''Added For NKImplant Functionality
        Get

            Return _OpenfrmImplants
        End Get
        Set(ByVal Value As Boolean)
            _OpenfrmImplants = Value
        End Set
    End Property


    Dim sSectionname As String
    Dim nSectionValue As Int16
    Dim dtsave As DataTable = Nothing

    Private Sub frmPatspecCDAspc_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not IsNothing(dtsave) Then
            dtsave.Dispose()
            dtsave = Nothing
        End If
    End Sub



    Private Sub frmPatspecCDAspc_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load


        cmbProblems.SelectedIndex = 0
        cmbMedicationallergies.SelectedIndex = 0
        cmbLaboratory_valueResult.SelectedIndex = 0
        cmbProcedures.SelectedIndex = 0
        cmbCarePlan.SelectedIndex = 0
        cmbfamilyhistory.SelectedIndex = 0
        cmbEncounterDiagnoses.SelectedIndex = 0
        cmbCognitiveStatus.SelectedIndex = 0
        cmbReason_Referral.SelectedIndex = 0
        cmbMedications.SelectedIndex = 0
        cmbLaboratoryTest.SelectedIndex = 0
        cmbVitalSigns.SelectedIndex = 0
        cmbCareTeamMember.SelectedIndex = 0
        cmbClinicalInstructions.SelectedIndex = 0
        cmbSocialHistory.SelectedIndex = 0
        cmbImmunizations.SelectedIndex = 0
        cmbFunctionalstatus.SelectedIndex = 0
        cmbReferringProviders.SelectedIndex = 0
        cmbImplants.SelectedIndex = 0
        cmbHealthConcerns.SelectedIndex = 0






      
        dtsave = Getpatient_CDASectionsDetails(patientID)
        If IsNothing(dtsave) Then
            dtsave = New DataTable()



            Dim Col_Sectionname As New DataColumn("sSectionname", GetType(String))

            Dim Col_SectionValue As New DataColumn("nSectionValue", GetType(Int16))

            'dtsave.Columns.Add(dPatientID)
            dtsave.Columns.Add(Col_Sectionname)
            dtsave.Columns.Add(Col_SectionValue)
        Else
            For Each dr As DataRow In dtsave.Rows
                Select Case Convert.ToString(dr("sSectionname"))
                    Case "Problem"
                        cmbProblems.SelectedIndex = Convert.ToInt16(dr("nSectionValue"))
                    Case "Medication Allergies"
                        cmbMedicationallergies.SelectedIndex = Convert.ToInt16(dr("nSectionValue"))
                    Case "Laboratory value(s)/Result(s)"
                        cmbLaboratory_valueResult.SelectedIndex = Convert.ToInt16(dr("nSectionValue"))
                    Case "Procedures"
                        cmbProcedures.SelectedIndex = Convert.ToInt16(dr("nSectionValue"))
                    Case "Care Plan"
                        cmbCarePlan.SelectedIndex = Convert.ToInt16(dr("nSectionValue"))
                    Case "Family History"
                        cmbfamilyhistory.SelectedIndex = Convert.ToInt16(dr("nSectionValue"))
                   
                    Case "Encounter Diagnoses"
                        cmbEncounterDiagnoses.SelectedIndex = Convert.ToInt16(dr("nSectionValue"))
                    Case "Cognitive Status"
                        cmbCognitiveStatus.SelectedIndex = Convert.ToInt16(dr("nSectionValue"))
                    Case "Reason for Referral"
                        cmbReason_Referral.SelectedIndex = Convert.ToInt16(dr("nSectionValue"))
                    Case "Medications"
                        cmbMedications.SelectedIndex = Convert.ToInt16(dr("nSectionValue"))
                    Case "Laboratory Test(s)"
                        cmbLaboratoryTest.SelectedIndex = Convert.ToInt16(dr("nSectionValue"))
                    Case "Vital Signs"
                        cmbVitalSigns.SelectedIndex = Convert.ToInt16(dr("nSectionValue"))
                    Case "Care Team Member(s)"
                        cmbCareTeamMember.SelectedIndex = Convert.ToInt16(dr("nSectionValue"))
                    Case "Clinical Instructions"
                        cmbClinicalInstructions.SelectedIndex = Convert.ToInt16(dr("nSectionValue"))
                    Case "Social History"
                        cmbSocialHistory.SelectedIndex = Convert.ToInt16(dr("nSectionValue"))
                    Case "Immunizations"
                        cmbImmunizations.SelectedIndex = Convert.ToInt16(dr("nSectionValue"))
                    Case "Functional Status"
                        cmbFunctionalstatus.SelectedIndex = Convert.ToInt16(dr("nSectionValue"))
                    Case "Referring Providers"
                        cmbReferringProviders.SelectedIndex = Convert.ToInt16(dr("nSectionValue"))

                    Case "Implants"
                        cmbImplants.SelectedIndex = Convert.ToInt16(dr("nSectionValue"))
                    Case "Health Concerns"
                        cmbHealthConcerns.SelectedIndex = Convert.ToInt16(dr("nSectionValue"))




                End Select

            Next

        End If
      

        If (OpenfrmProblem = True) Then
            cmbProblems.SelectedIndex = 1
        End If

        If (OpenfrmHistory = True) Then
            cmbMedicationallergies.SelectedIndex = 1
        End If

        If (OpenfrmImmunization = True) Then
            cmbImmunizations.SelectedIndex = 1
        End If
        If (OpenfrmMedication = True) Then
            cmbMedications.SelectedIndex = 1
        End If
        If (OpenfrmImplants = True) Then
            cmbImplants.SelectedIndex = 1
        End If

    End Sub


    Private Sub tsb_Close_Click(sender As System.Object, e As System.EventArgs) Handles tsb_Close.Click
        Me.Close()
    End Sub

   
    Private Sub tsb_Save_Click(sender As System.Object, e As System.EventArgs) Handles tsb_Save.Click
        dtsave.Rows.Clear()
        dtsave.Rows.Add("Problem", cmbProblems.SelectedIndex)
        dtsave.Rows.Add("Medication Allergies", cmbMedicationallergies.SelectedIndex)
        dtsave.Rows.Add("Laboratory value(s)/Result(s)", cmbLaboratory_valueResult.SelectedIndex)
        dtsave.Rows.Add("Procedures", cmbProcedures.SelectedIndex)
        dtsave.Rows.Add("Care Plan", cmbCarePlan.SelectedIndex)
        dtsave.Rows.Add("Family History", cmbfamilyhistory.SelectedIndex)
        dtsave.Rows.Add("Encounter Diagnoses", cmbEncounterDiagnoses.SelectedIndex)
        dtsave.Rows.Add("Cognitive Status", cmbCognitiveStatus.SelectedIndex)
        dtsave.Rows.Add("Reason for Referral", cmbReason_Referral.SelectedIndex)
        dtsave.Rows.Add("Medications", cmbMedications.SelectedIndex)
        dtsave.Rows.Add("Laboratory Test(s)", cmbLaboratoryTest.SelectedIndex)
        dtsave.Rows.Add("Vital Signs", cmbVitalSigns.SelectedIndex)

        dtsave.Rows.Add("Care Team Member(s)", cmbCareTeamMember.SelectedIndex)
        dtsave.Rows.Add("Clinical Instructions", cmbClinicalInstructions.SelectedIndex)
        dtsave.Rows.Add("Social History", cmbSocialHistory.SelectedIndex)
        dtsave.Rows.Add("Immunizations", cmbImmunizations.SelectedIndex)
        dtsave.Rows.Add("Functional Status", cmbFunctionalstatus.SelectedIndex)
        dtsave.Rows.Add("Referring Providers", cmbReferringProviders.SelectedIndex)

        dtsave.Rows.Add("Implants", cmbImplants.SelectedIndex)
        dtsave.Rows.Add("Health Concerns", cmbHealthConcerns.SelectedIndex)

        Save(_patientID)
        Me.Close()

    End Sub
    Public Function Save(ByVal PatientID As Long) As Boolean

        ' Optional ByVal dtCCDA As DataTable = Nothing, Optional ByVal TVP_patient_CDASections As String = ""

        Dim _databaseConnectionString As String = gloGlobal.gloPMGlobal.DatabaseConnectionString
        Dim odb As New gloDatabaseLayer.DBLayer(_databaseConnectionString)


        Dim dtpCCDA As DataTable = Nothing

        Dim oParameter As gloDatabaseLayer.DBParameter = Nothing
        Dim oParameters As gloDatabaseLayer.DBParameters = Nothing

        Try


            odb.Connect(False)


            oParameters = New gloDatabaseLayer.DBParameters

            oParameter = New gloDatabaseLayer.DBParameter
            oParameter.DataType = SqlDbType.BigInt
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.ParameterName = "@nPatientID"
            oParameter.Value = PatientID
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter
            oParameter.DataType = SqlDbType.Structured
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.ParameterName = "@TVP_patient_CDASections"
            oParameter.Value = dtsave
            oParameters.Add(oParameter)
            oParameter = Nothing


            odb.Execute("gsp_InUppatient_CDASections", oParameters)

            odb.Disconnect()

            'Return dtsave 

        Catch ex As Exception
            'MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
            Return Nothing

        Finally

            If Not IsNothing(oParameters) Then
                oParameters.Clear()
                oParameters.Dispose()
                oParameters = Nothing
            End If
            

        End Try
        Return Nothing

    End Function
    Private Function Getpatient_CDASectionsDetails(nPatientID As Long) As DataTable

        Dim _databaseConnectionString As String = gloGlobal.gloPMGlobal.DatabaseConnectionString
        Dim odb As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim oParameters As New gloDatabaseLayer.DBParameters()
        Dim dt As New DataTable()

        Try
            odb.Connect(False)
            oParameters.Add("@nPatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt)
            ''Getpatient_CDASections( 2015 certification code)
            odb.Retrive("Getpatient_CDASections", oParameters, dt)
            Return dt
        Catch dbEx As gloDatabaseLayer.DBException
            dbEx.ERROR_Log(dbEx.ToString())
            dbEx = Nothing
            Return Nothing
        Catch ex As Exception
            'MessageBox.Show("ERROR : " & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            If Not IsNothing(oParameters) Then
                oParameters.Clear()
                oParameters.Dispose()
                oParameters = Nothing
            End If
            If odb IsNot Nothing Then
                odb.Disconnect()
            End If
            If odb IsNot Nothing Then
                odb.Dispose()
                odb = Nothing
            End If
        End Try


    End Function

  
   

End Class