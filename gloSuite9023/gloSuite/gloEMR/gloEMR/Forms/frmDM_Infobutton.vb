Imports gloSettings
Imports System.Data.SqlClient
Imports System.Net
Imports System.Xml
Imports System.Xml.Linq
Imports gloEMR.gloEMRWord

Public Class frmDM_Infobutton

    Dim PatientID As Long = 0
    Dim DmRuleID As Long = 0
    Dim VisitID As Long = 0
    Dim PatientAge As gloUserControlLibrary.AgeDetail
    Dim Gender As String
    Dim Language As String = ""
    Dim COL_Infobutton As Integer = 0
    Dim COL_PatientID As Integer = 1
    Dim COL_RuleID As Integer = 2
    Dim COL_Display As Integer = 3
    Dim COL_Code As Integer = 4
    Dim COL_Loinc As Integer = 5
    Dim COL_Snomed As Integer = 6
    Dim COL_Ndc As Integer = 7
    Dim COL_RxNorm As Integer = 8
    Dim COL_TriggerCriteria As Integer = 9

    Private WithEvents oMenu As System.Windows.Forms.ToolStripItem
    Private oContextMenuFlexGrid As C1.Win.C1FlexGrid.C1FlexGrid

    Public Sub New(ByVal _DmRuleID As Long, ByVal _PatientID As Long, ByVal _PatientAge As gloUserControlLibrary.AgeDetail, ByVal _Gender As String, ByVal _Language As String, ByVal _visitID As Long)
        MyBase.New()
        InitializeComponent()
        PatientID = _PatientID
        DmRuleID = _DmRuleID
        PatientAge = _PatientAge
        Gender = _Gender
        Language = _Language
        VisitID = _visitID
        lblchkIncludeAge.Text = Convert.ToString(_PatientAge.Age)
        lblchkIncludeGender.Text = _Gender
    End Sub

    Private Sub frmDM_Infobutton_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Dim dsDmData As DataSet = LoadDM_Rule_data()
        Try
            If Not IsNothing(dsDmData) Then
                If dsDmData.Tables.Count > 0 Then
                    If Not IsNothing(dsDmData.Tables(2)) Then
                        If dsDmData.Tables(2).Rows.Count > 0 Then
                            c1DmProblem.DataSource = dsDmData.Tables(2)
                        End If
                    End If

                    If Not IsNothing(dsDmData.Tables(1)) Then
                        If dsDmData.Tables(1).Rows.Count > 0 Then
                            c1DmMedication.DataSource = dsDmData.Tables(1)
                        End If
                    End If

                    If Not IsNothing(dsDmData.Tables(4)) Then
                        If dsDmData.Tables(4).Rows.Count > 0 Then
                            c1DmLab.DataSource = dsDmData.Tables(4)
                        End If
                    End If

                End If

                DesignCriteriaGrid(c1DmProblem)
                DesignCriteriaGrid(c1DmMedication)
                DesignCriteriaGrid(c1DmLab)
            End If
            If pnlInfoLinks.Visible Then
                c1InfoLinks.Rows(0).Visible = False
            End If
            cmbAudience.Text = "Provider"
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("Openinfosource :: " & ex.ToString(), True)
        End Try
    End Sub

    Private Sub ts_btnClose_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnClose.Click
        Me.Close()
    End Sub

    Private Function LoadDM_Rule_data() As DataSet
        Dim _ds As DataSet = Nothing
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParamater As New gloDatabaseLayer.DBParameters()
        Try
            If oDB IsNot Nothing Then
                If oDB.Connect(False) Then
                    oParamater.Add("@nPatientID", PatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oParamater.Add("@DMCriteriaID", DmRuleID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                    oDB.Retrive("DM_GetTriggerredPatientData", oParamater, _ds)
                End If
            End If

            For Each dTable As DataTable In _ds.Tables
                dTable.Columns.Add("Infobutton").SetOrdinal(0)
            Next

        Catch ex As gloDatabaseLayer.DBException
            ex.ERROR_Log("Openinfosource :: " & ex.ToString())
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("Openinfosource :: " & ex.ToString(), True)
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

        Return _ds
    End Function

    Private Sub DesignCriteriaGrid(ByVal c1Grid As C1.Win.C1FlexGrid.C1FlexGrid)

        If c1Grid.Cols.Count > 0 Then
            c1Grid.Cols(COL_PatientID).AllowEditing = False
            c1Grid.Cols(COL_RuleID).AllowEditing = False
            c1Grid.Cols(COL_Display).AllowEditing = False
            c1Grid.Cols(COL_Code).AllowEditing = False
            c1Grid.Cols(COL_Loinc).AllowEditing = False
            c1Grid.Cols(COL_RxNorm).AllowEditing = False
            c1Grid.Cols(COL_Snomed).AllowEditing = False
            c1Grid.Cols(COL_Ndc).AllowEditing = False
            c1Grid.Cols(COL_TriggerCriteria).AllowEditing = False
            c1Grid.Cols(COL_Infobutton).AllowEditing = False

            c1Grid.Cols(COL_PatientID).Caption = "PatientId"
            c1Grid.Cols(COL_RuleID).Caption = "RuleId"
            If c1Grid.Name = "c1DmMedication" Then
                c1Grid.Cols(COL_Display).Caption = "Medications"
            ElseIf c1Grid.Name = "c1DmProblem" Then
                c1Grid.Cols(COL_Display).Caption = "Problem List"
            ElseIf c1Grid.Name = "c1DmLab" Then
                c1Grid.Cols(COL_Display).Caption = "Lab Orders and Results"
            End If
            c1Grid.Cols(COL_Code).Caption = "Code"
            c1Grid.Cols(COL_Loinc).Caption = "LOINC Code"
            c1Grid.Cols(COL_RxNorm).Caption = "RxNorm"
            c1Grid.Cols(COL_Snomed).Caption = "Snomed Code"
            c1Grid.Cols(COL_Ndc).Caption = "NDCCode"
            c1Grid.Cols(COL_TriggerCriteria).Caption = "Category"
            c1Grid.Cols(COL_Infobutton).Caption = " "


            c1Grid.Cols(COL_PatientID).Visible = False
            c1Grid.Cols(COL_RuleID).Visible = False
            c1Grid.Cols(COL_Display).Visible = True
            c1Grid.Cols(COL_Code).Visible = False
            c1Grid.Cols(COL_Loinc).Visible = False
            c1Grid.Cols(COL_RxNorm).Visible = False
            c1Grid.Cols(COL_Snomed).Visible = False
            c1Grid.Cols(COL_Ndc).Visible = False
            c1Grid.Cols(COL_TriggerCriteria).Visible = False
            c1Grid.Cols(COL_Infobutton).Visible = True

            c1Grid.Cols(COL_PatientID).Width = 0
            c1Grid.Cols(COL_RuleID).Width = 0
            c1Grid.Cols(COL_Display).Width = Me.Width * 0.9
            c1Grid.Cols(COL_Code).Width = 0
            c1Grid.Cols(COL_Loinc).Width = 0
            c1Grid.Cols(COL_RxNorm).Width = 0
            c1Grid.Cols(COL_Snomed).Width = 0
            c1Grid.Cols(COL_Ndc).Width = 0
            c1Grid.Cols(COL_TriggerCriteria).Width = 0
            c1Grid.Cols(COL_Infobutton).Width = Me.Width * 0.03

            c1Grid.Cols(COL_Infobutton).ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.CenterCenter

            With c1Grid
                Dim dt As DataTable = c1Grid.DataSource
                If Not IsNothing(dt) Then
                    For i As Int16 = 0 To dt.Rows.Count - 1
                        .SetCellImage(i + 1, 0, My.Resources.infobutton)
                    Next
                End If
            End With
        End If

    End Sub

    Private Sub GetNLMInfobuttonDocument(ByVal strSelectedCode As String, ByVal strSelectedCodeSystem As String, ByVal PatientLanguage As String, ByVal sAgeValue As String, ByVal sAgeUnit As String, ByVal sGender As String)
        Dim clsinfobutton_DM As New gloEMRGeneralLibrary.clsInfobutton

        'If strSelectedCodeSystem = "2.16.840.1.113883.6.96" Then       'SnomedCT
        '    clsinfobutton_DM.Openinfosource(strSelectedCode, strSelectedCodeSystem, PatientLanguage, PatientID, gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList, VisitID, sAgeValue, sAgeUnit, Gender, gnLoginProviderID)
        'ElseIf strSelectedCodeSystem = "2.16.840.1.113883.6.90" Then   'ICD10
        '    clsinfobutton_DM.Openinfosource(strSelectedCode, strSelectedCodeSystem, PatientLanguage, PatientID, gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList, VisitID, sAgeValue, sAgeUnit, Gender, gnLoginProviderID)
        'ElseIf strSelectedCodeSystem = "2.16.840.1.113883.6.103" Then  'ICD9
        '    clsinfobutton_DM.Openinfosource(strSelectedCode, strSelectedCodeSystem, PatientLanguage, PatientID, gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList, VisitID, sAgeValue, sAgeUnit, Gender, gnLoginProviderID)
        'ElseIf strSelectedCodeSystem = "2.16.840.1.113883.6.69" Then   'NDC
        '    clsinfobutton_DM.Openinfosource(strSelectedCode, strSelectedCodeSystem, PatientLanguage, PatientID, gloEMRGeneralLibrary.clsInfobutton.enumSource.Medication, VisitID, sAgeValue, sAgeUnit, Gender, gnLoginProviderID)
        'ElseIf strSelectedCodeSystem = "2.16.840.1.113883.6.88" Then   'RxNorm
        '    clsinfobutton_DM.Openinfosource(strSelectedCode, strSelectedCodeSystem, PatientLanguage, PatientID, gloEMRGeneralLibrary.clsInfobutton.enumSource.Medication, VisitID, sAgeValue, sAgeUnit, Gender, gnLoginProviderID)
        'ElseIf strSelectedCodeSystem = "2.16.840.1.113883.6.1" Then    'Loinc
        '    clsinfobutton_DM.Openinfosource(strSelectedCode, strSelectedCodeSystem, PatientLanguage, PatientID, gloEMRGeneralLibrary.clsInfobutton.enumSource.Orders, VisitID, sAgeValue, sAgeUnit, Gender, gnLoginProviderID)
        'End If


        Dim src As Integer = 0

        If strSelectedCodeSystem = "2.16.840.1.113883.6.96" Then       'SnomedCT
            src = gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList
        ElseIf strSelectedCodeSystem = "2.16.840.1.113883.6.90" Then   'ICD10
            src = gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList
        ElseIf strSelectedCodeSystem = "2.16.840.1.113883.6.103" Then  'ICD9
            src = gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList
        ElseIf strSelectedCodeSystem = "2.16.840.1.113883.6.69" Then   'NDC
            src = gloEMRGeneralLibrary.clsInfobutton.enumSource.Medication
        ElseIf strSelectedCodeSystem = "2.16.840.1.113883.6.88" Then   'RxNorm
            src = gloEMRGeneralLibrary.clsInfobutton.enumSource.Medication
        ElseIf strSelectedCodeSystem = "2.16.840.1.113883.6.1" Then    'Loinc
            src = gloEMRGeneralLibrary.clsInfobutton.enumSource.Orders
        End If

        Dim blnUseDefaultPrinter As Boolean
        gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
        If IsNothing(gloRegistrySetting.GetRegistryValue("UseDefaultPrinter")) = False Then
            If gloRegistrySetting.GetRegistryValue("UseDefaultPrinter") = 1 Then
                blnUseDefaultPrinter = True
            Else
                blnUseDefaultPrinter = False
            End If
        Else
            blnUseDefaultPrinter = True
        End If
        gloRegistrySetting.CloseRegistryKey()

        Dim Newurl As String = ""
        Newurl = GetURL(Me.GetURL(sGender, sAgeValue, sAgeUnit, strSelectedCode, strSelectedCodeSystem, PatientLanguage), PatientLanguage)
        'lblInfobuttonLink.Text = Newurl
        Dim InfoButtonForm As gloEMRGeneralLibrary.frmInfoButtonBrowser = gloEMRGeneralLibrary.frmInfoButtonBrowser.GetInstance
        With InfoButtonForm
            .LoginProviderID = gnLoginProviderID
            .PatientId = PatientID
            .VisitID = VisitID
            .Source = src
            .EducationID = 0
            .gblnUseDefaultPrinter = blnUseDefaultPrinter
            .ResourceCategory = gloEMRGeneralLibrary.clsInfobutton.enumResourceCategory.OnlineLibrary
            .ResourceType = gloEMRGeneralLibrary.clsInfobutton.enumResourceType.PatientReferenceMaterial
            .ValidatePortalFeatures()
            .NavigateTo(Newurl)
            .Visible = True
            .Top = True
            .BringToFront()
            .Activate()
            .Show()

        End With
    End Sub

    Private Sub c1DmCriteria_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles c1DmProblem.MouseDown, c1DmMedication.MouseDown, c1DmLab.MouseDown
        If e.Button = MouseButtons.Left Then
            Dim IncludeGender As Boolean = False
            Dim PatientGender As String = ""
            Dim IncludeAge As Boolean = False
            Dim AgeUnit As String = ""
            Dim AgeValue As String = ""
            Dim PatientLanguage As String = ""
            Dim Code As String = ""
            Dim CodeSystem As String = ""
            Dim CodeDescription As String = ""
            Dim Audience As String = ""
            Dim ProviderId As Long = 0
            Dim SelectedPatientId As Long = 0
            Dim VisitId As Long = 0
            Dim CallingForm As Form = Nothing
            Dim ICDVersion As Integer = 0
            Me.Cursor = Cursors.WaitCursor
            Try
                IncludeGender = chkIncludeGender.Checked
                PatientGender = Gender
                IncludeAge = chkIncludeAge.Checked
                AgeUnit = ""
                AgeValue = ""
                PatientLanguage = Language
                Code = ""
                CodeSystem = ""
                CodeDescription = ""
                Audience = cmbAudience.Text
                ProviderId = 0
                SelectedPatientId = PatientID
                VisitId = VisitId
                CallingForm = Me

                If PatientAge.Years <> 0 Then
                    AgeUnit = "a"
                    AgeValue = PatientAge.Years
                ElseIf PatientAge.Months <> 0 Then
                    AgeUnit = "mo"
                    AgeValue = PatientAge.Months
                ElseIf PatientAge.Days <> 0 Then
                    AgeUnit = "d"
                    AgeValue = PatientAge.Days
                End If

                With sender
                    Dim c As Integer = .HitTest(e.X, e.Y).Column
                    Dim r As Integer = .HitTest(e.X, e.Y).Row
                    If c = COL_Infobutton Then
                        If r > 0 Then
                            Code = Convert.ToString(sender.GetData(sender.RowSel, COL_Code))
                            If Convert.ToString(sender.GetData(sender.RowSel, COL_TriggerCriteria)) = "CPT" Then           'SnomedCT
                                CodeSystem = "2.16.840.1.113883.6.96"
                            ElseIf Convert.ToString(sender.GetData(sender.RowSel, COL_TriggerCriteria)) = "Drugs" Then     'NDC
                                CodeSystem = "2.16.840.1.113883.6.69"
                            ElseIf Convert.ToString(sender.GetData(sender.RowSel, COL_TriggerCriteria)) = "ICD10" Then     'ICD10
                                CodeSystem = "2.16.840.1.113883.6.90"
                                ICDVersion = 10
                            ElseIf Convert.ToString(sender.GetData(sender.RowSel, COL_TriggerCriteria)) = "ICD9" Then      'ICD9
                                CodeSystem = "2.16.840.1.113883.6.103"
                                ICDVersion = 9
                            ElseIf Convert.ToString(sender.GetData(sender.RowSel, COL_TriggerCriteria)) = "Orders" Then    'Loinc
                                CodeSystem = "2.16.840.1.113883.6.1"
                            ElseIf Convert.ToString(sender.GetData(sender.RowSel, COL_TriggerCriteria)) = "Snomed" Then    'SnomedCT
                                CodeSystem = "2.16.840.1.113883.6.96"
                            ElseIf Convert.ToString(sender.GetData(sender.RowSel, COL_TriggerCriteria)) = "History" Then
                                If Convert.ToString(sender.GetData(sender.RowSel, COL_Snomed)) <> "" Then                                                                          'SnomedCT
                                    CodeSystem = "2.16.840.1.113883.6.96"
                                    Code = Convert.ToString(sender.GetData(sender.RowSel, COL_Snomed))
                                ElseIf Convert.ToString(sender.GetData(sender.RowSel, COL_Ndc)) <> "" Then                                                                           'NDC
                                    CodeSystem = "2.16.840.1.113883.6.69"
                                    Code = Convert.ToString(sender.GetData(sender.RowSel, COL_Ndc))
                                ElseIf Convert.ToString(sender.GetData(sender.RowSel, COL_RxNorm)) <> "" Then                                                                        'RxNorm
                                    CodeSystem = "2.16.840.1.113883.6.88"
                                    Code = Convert.ToString(sender.GetData(sender.RowSel, COL_RxNorm))
                                ElseIf Convert.ToString(sender.GetData(sender.RowSel, COL_Loinc)) <> "" Then                                                                         'Loinc
                                    CodeSystem = "2.16.840.1.113883.6.1"
                                    Code = Convert.ToString(sender.GetData(sender.RowSel, COL_Loinc))
                                End If
                            End If
                            CodeDescription = Convert.ToString(sender.GetData(sender.RowSel, COL_Display))

                            Dim oClsInfobutton As New gloEMRGeneralLibrary.clsInfobutton()
                            Dim dtEduTemplates As DataTable = Nothing

                            Dim AgeinYears As Decimal = PatientAge.Years
                            If PatientAge.Months > 0 Then
                                AgeinYears = AgeinYears + (PatientAge.Months / 10)
                            ElseIf PatientAge.Days > 1 Then
                                AgeinYears = AgeinYears + 0.1
                            End If

                            If CodeSystem = "2.16.840.1.113883.6.90" Or CodeSystem = "2.16.840.1.113883.6.103" Then     'ICD10
                                 dtEduTemplates = oClsInfobutton.GetEducationMaterial(Code, CodeSystem, AgeinYears, PatientGender, ICDVersion)
                            Else
                                dtEduTemplates = oClsInfobutton.GetEducationMaterial(Code, CodeSystem, AgeinYears, PatientGender)
                            End If


                            mnuMedlineInfobutton.Visible = False
                            mnuProviderReference.DropDownItems.Clear()
                            mnuPatientEducation.DropDownItems.Clear()

                            If Not IsNothing(dtEduTemplates) Then
                                If dtEduTemplates.Rows.Count > 0 Then
                                    For i As Integer = 0 To dtEduTemplates.Rows.Count - 1
                                        oMenu = New ToolStripMenuItem
                                        oMenu.Text = Convert.ToString(dtEduTemplates.Rows(i)("sTemplateName"))
                                        AddHandler oMenu.Click, AddressOf OpenInternalInfodocument
                                        If Convert.ToInt32(dtEduTemplates.Rows(i)("nResourceType")) = 1 Then
                                            oMenu.Tag = Convert.ToString(dtEduTemplates.Rows(i)("nTemplateID")) + "-Patient Reference Material"
                                            mnuPatientEducation.DropDownItems.Add(oMenu)
                                        ElseIf Convert.ToInt32(dtEduTemplates.Rows(i)("nResourceType")) = 2 Then
                                            If Convert.ToBoolean(dtEduTemplates.Rows(i)("bIsAdvancedProviderReference")) Then
                                                If gblnAdvancedReferenceEnabled = True Then
                                                    oMenu.Tag = Convert.ToString(dtEduTemplates.Rows(i)("nTemplateID")) + "-Provider Reference Material"
                                                    mnuProviderReference.DropDownItems.Add(oMenu)
                                                End If
                                            Else
                                                oMenu.Tag = Convert.ToString(dtEduTemplates.Rows(i)("nTemplateID")) + "-Provider Reference Material"
                                                mnuProviderReference.DropDownItems.Add(oMenu)
                                            End If
                                        End If
                                    Next

                                    cntAssociateExams.Visible = True
                                    cntAssociateExams.Show(CType(sender, Control), e.Location)
                                    sender.ContextMenuStrip = cntAssociateExams
                                    sender.ContextMenuStrip.Visible = True
                                    oContextMenuFlexGrid = sender
                                End If
                            End If

                            'If gblnEducationMaterialEnabled Then
                            If mnuPatientEducation.DropDownItems.Count > 0 Or mnuProviderReference.DropDownItems.Count > 0 Then
                                mnuMedlineInfobutton.Visible = True
                                mnuProviderReference.Visible = True
                                mnuPatientEducation.Visible = True
                                cntAssociateExams.Visible = True

                                If mnuProviderReference.DropDownItems.Count <= 0 Then
                                    mnuProviderReference.Visible = False
                                End If
                                If mnuPatientEducation.DropDownItems.Count <= 0 Then
                                    mnuPatientEducation.Visible = False
                                End If
                            Else
                                mnuMedlineInfobutton.Visible = False
                                mnuProviderReference.Visible = False
                                mnuPatientEducation.Visible = False
                                cntAssociateExams.Visible = False
                                oClsInfobutton.GetEducationMaterial_OpenInfobutton(IncludeGender, PatientGender, IncludeAge, AgeUnit, AgeValue, Language, Code, CodeSystem, CodeDescription, Audience, ProviderId, SelectedPatientId, VisitId, CallingForm)
                            End If
                            'End If


                        End If
                    End If
                End With

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog("OpenInfobutton :: " & ex.ToString(), False)
            End Try
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub mnuMedlineInfobutton_Click(sender As System.Object, e As System.EventArgs) Handles mnuMedlineInfobutton.Click
        Dim IncludeGender As Boolean = False
        Dim PatientGender As String = ""
        Dim IncludeAge As Boolean = False
        Dim AgeUnit As String = ""
        Dim AgeValue As String = ""
        Dim PatientLanguage As String = ""
        Dim Code As String = ""
        Dim CodeSystem As String = ""
        Dim CodeDescription As String = ""
        Dim Audience As String = ""
        Dim ProviderId As Long = 0
        Dim SelectedPatientId As Long = 0
        Dim VisitId As Long = 0
        Dim CallingForm As Form = Nothing
        Me.Cursor = Cursors.WaitCursor
        Try
            IncludeGender = chkIncludeGender.Checked
            PatientGender = Gender
            IncludeAge = chkIncludeAge.Checked
            AgeUnit = ""
            AgeValue = ""
            PatientLanguage = Language
            Code = ""
            CodeSystem = ""
            CodeDescription = ""
            Audience = cmbAudience.Text
            ProviderId = 0
            SelectedPatientId = PatientID
            VisitId = VisitId
            CallingForm = Me

            If PatientAge.Years <> 0 Then
                AgeUnit = "a"
                AgeValue = PatientAge.Years
            ElseIf PatientAge.Months <> 0 Then
                AgeUnit = "mo"
                AgeValue = PatientAge.Months
            ElseIf PatientAge.Days <> 0 Then
                AgeUnit = "d"
                AgeValue = PatientAge.Days
            End If

            With oContextMenuFlexGrid
                Dim c As Integer = .ColSel
                Dim r As Integer = .RowSel
                If c = COL_Infobutton Then
                    If r > 0 Then
                        Code = Convert.ToString(.GetData(.RowSel, COL_Code))
                        If Convert.ToString(.GetData(.RowSel, COL_TriggerCriteria)) = "CPT" Then           'SnomedCT
                            CodeSystem = "2.16.840.1.113883.6.96"
                        ElseIf Convert.ToString(.GetData(.RowSel, COL_TriggerCriteria)) = "Drugs" Then     'NDC
                            CodeSystem = "2.16.840.1.113883.6.69"
                        ElseIf Convert.ToString(.GetData(.RowSel, COL_TriggerCriteria)) = "ICD10" Then     'ICD10
                            CodeSystem = "2.16.840.1.113883.6.90"
                        ElseIf Convert.ToString(.GetData(.RowSel, COL_TriggerCriteria)) = "ICD9" Then      'ICD9
                            CodeSystem = "2.16.840.1.113883.6.103"
                        ElseIf Convert.ToString(.GetData(.RowSel, COL_TriggerCriteria)) = "Orders" Then    'Loinc
                            CodeSystem = "2.16.840.1.113883.6.1"
                        ElseIf Convert.ToString(.GetData(.RowSel, COL_TriggerCriteria)) = "Snomed" Then    'SnomedCT
                            CodeSystem = "2.16.840.1.113883.6.96"
                        ElseIf Convert.ToString(.GetData(.RowSel, COL_TriggerCriteria)) = "History" Then
                            If Convert.ToString(.GetData(.RowSel, COL_Snomed)) <> "" Then                                                                          'SnomedCT
                                CodeSystem = "2.16.840.1.113883.6.96"
                                Code = Convert.ToString(.GetData(.RowSel, COL_Snomed))
                            ElseIf Convert.ToString(.GetData(.RowSel, COL_Ndc)) <> "" Then                                                                           'NDC
                                CodeSystem = "2.16.840.1.113883.6.69"
                                Code = Convert.ToString(.GetData(.RowSel, COL_Ndc))
                            ElseIf Convert.ToString(.GetData(.RowSel, COL_RxNorm)) <> "" Then                                                                        'RxNorm
                                CodeSystem = "2.16.840.1.113883.6.88"
                                Code = Convert.ToString(.GetData(.RowSel, COL_RxNorm))
                            ElseIf Convert.ToString(.GetData(.RowSel, COL_Loinc)) <> "" Then                                                                         'Loinc
                                CodeSystem = "2.16.840.1.113883.6.1"
                                Code = Convert.ToString(.GetData(.RowSel, COL_Loinc))
                            End If
                        End If
                        CodeDescription = Convert.ToString(.GetData(.RowSel, COL_Display))

                        Dim oClsInfobutton As New gloEMRGeneralLibrary.clsInfobutton()
                        oClsInfobutton.GetEducationMaterial_OpenInfobutton(IncludeGender, PatientGender, IncludeAge, AgeUnit, AgeValue, Language, Code, CodeSystem, CodeDescription, Audience, ProviderId, SelectedPatientId, VisitId, CallingForm)
                    End If
                End If
            End With

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("OpenInfobutton :: " & ex.ToString(), False)
        End Try
        Me.Cursor = Cursors.Default
    End Sub

    Public Sub OpenInternalInfodocument(ByVal sender As Object, ByVal e As EventArgs)
        Dim oCurrentMenu As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        Dim tag() As String = oCurrentMenu.Tag.ToString().Split("-")
        Dim TemplateName As String = oCurrentMenu.Text
        Dim nTempId As Int64 = CType(tag(0), Int64)
        Dim OpenFor As String = tag(1).ToString()
        Dim objWord As New clsWordDocument
        Dim dtPtEducation As New DataTable
        dtPtEducation = objWord.FillTemplates(enumTemplateFlag.PatientEducation)
        Dim ofrmPatientEducation As New frmPatientEducationPreview()
        Try
            ofrmPatientEducation.Text = OpenFor + "-" + TemplateName
            ofrmPatientEducation.PATID = PatientID
            ofrmPatientEducation.TempName = TemplateName
            ofrmPatientEducation.Sourc = gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList
            ofrmPatientEducation.ResourcCat = gloEMRGeneralLibrary.clsInfobutton.enumResourceCategory.InternalLibrary
            If tag(1) = "Provider Reference Material" Then
                ofrmPatientEducation.ResourcTyp = gloEMRGeneralLibrary.clsInfobutton.enumResourceType.ProviderReferenceMaterial
            Else
                ofrmPatientEducation.ResourcTyp = gloEMRGeneralLibrary.clsInfobutton.enumResourceType.PatientReferenceMaterial
            End If

            ofrmPatientEducation.TMPID = nTempId
            ofrmPatientEducation.ISGRID = False
            ofrmPatientEducation.ShowDialog()

            ofrmPatientEducation.WindowState = FormWindowState.Maximized

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If Not IsNothing(Me.MdiParent) Then
                CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
                CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True
            End If
            If Not IsNothing(ofrmPatientEducation) Then
                ofrmPatientEducation.Dispose()
                ofrmPatientEducation = Nothing
            End If
        Finally
            If Not IsNothing(ofrmPatientEducation) Then
                ofrmPatientEducation.Close()
            End If
            If Not IsNothing(ofrmPatientEducation) Then
                ofrmPatientEducation.Dispose()
                ofrmPatientEducation = Nothing
            End If
        End Try
    End Sub

    'Private Sub c1DmCriteria_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles c1DmProblem.MouseDown, c1DmMedication.MouseDown, c1DmLab.MouseDown
    '    If e.Button = MouseButtons.Left Then

    '        Dim strSelectedCode As String
    '        Dim strSelectedCodeSystem As String = ""
    '        Dim strSelectedCodeDisplay As String = ""
    '        Dim AgeinYears As Decimal
    '        Dim sAgeUnit As String = ""
    '        Dim sAgeValue As String = ""
    '        Dim Audience As String = ""
    '        Dim TaskContext As String = ""

    '        Dim PatientLanguage As String = ""
    '        Dim strLang As String = ""

    '        Dim ParameterString As String = ""
    '        Dim _ISSmonedCodeMandatory As Boolean = False

    '        Dim oclsProblemListV2 As clsPatientProblemList = New clsPatientProblemList
    '        Dim clsinfobutton_DM As New gloEMRGeneralLibrary.clsInfobutton

    '        Dim blnUseDefaultPrinter As Boolean
    '        Dim arrInfoLinks As New ArrayList()
    '        Dim dtEducation As DataTable
    '        Dim bUseOpenInfobutton As Boolean = True
    '        Dim strNDCCode As String = ""

    '        Dim RxNormResponse As gloGlobal.DIB.RxnormFlagInfo = Nothing
    '        Try



    '            If cmbAudience.Text = "Provider" Then
    '                Audience = "PROV"
    '            Else
    '                Audience = "PAT"
    '            End If

    '            gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
    '            If IsNothing(gloRegistrySetting.GetRegistryValue("UseDefaultPrinter")) = False Then
    '                If gloRegistrySetting.GetRegistryValue("UseDefaultPrinter") = 1 Then
    '                    blnUseDefaultPrinter = True
    '                Else
    '                    blnUseDefaultPrinter = False
    '                End If
    '            Else
    '                blnUseDefaultPrinter = True
    '            End If
    '            gloRegistrySetting.CloseRegistryKey()

    '            Me.Cursor = Cursors.WaitCursor
    '            _ISSmonedCodeMandatory = oclsProblemListV2.IsSnomedMandatory()

    '            If chkIncludeGender.Checked = False Then
    '                Gender = ""
    '            Else
    '                If lblchkIncludeGender.Text.ToUpper() = "Male".ToUpper() Then
    '                    Gender = "M"
    '                ElseIf lblchkIncludeGender.Text.ToUpper() = "Female".ToUpper() Then
    '                    Gender = "F"
    '                End If
    '                Gender = lblchkIncludeGender.Text
    '            End If

    '            If chkIncludeAge.Checked = False Then
    '                sAgeUnit = ""
    '                sAgeValue = ""
    '            Else
    '                If PatientAge.Months > 0 Then
    '                    AgeinYears = AgeinYears + (PatientAge.Months / 10)
    '                ElseIf PatientAge.Days > 1 Then
    '                    AgeinYears = AgeinYears + 0.1
    '                End If

    '                If PatientAge.Years <> 0 Then
    '                    sAgeUnit = "a"
    '                    sAgeValue = PatientAge.Years
    '                ElseIf PatientAge.Months <> 0 Then
    '                    sAgeUnit = "mo"
    '                    sAgeValue = PatientAge.Months
    '                ElseIf PatientAge.Days <> 0 Then
    '                    sAgeUnit = "d"
    '                    sAgeValue = PatientAge.Days
    '                End If
    '            End If





    '            If PatientLanguage = "English" Then
    '                strLang = "en"
    '            ElseIf PatientLanguage = "Spanish" Or PatientLanguage = "Spanish; Castilian" Then
    '                strLang = "sp"
    '            Else
    '                strLang = "en"
    '            End If


    '            With sender
    '                Dim c As Integer = .HitTest(e.X, e.Y).Column
    '                Dim r As Integer = .HitTest(e.X, e.Y).Row
    '                If c = COL_Infobutton Then
    '                    If r > 0 Then
    '                        strSelectedCode = Convert.ToString(sender.GetData(sender.RowSel, COL_Code))
    '                        If Convert.ToString(sender.GetData(sender.RowSel, COL_TriggerCriteria)) = "CPT" Then           'SnomedCT
    '                            strSelectedCodeSystem = "2.16.840.1.113883.6.96"
    '                        ElseIf Convert.ToString(sender.GetData(sender.RowSel, COL_TriggerCriteria)) = "Drugs" Then     'NDC
    '                            strSelectedCodeSystem = "2.16.840.1.113883.6.69"
    '                        ElseIf Convert.ToString(sender.GetData(sender.RowSel, COL_TriggerCriteria)) = "ICD10" Then     'ICD10
    '                            strSelectedCodeSystem = "2.16.840.1.113883.6.90"
    '                        ElseIf Convert.ToString(sender.GetData(sender.RowSel, COL_TriggerCriteria)) = "ICD9" Then      'ICD9
    '                            strSelectedCodeSystem = "2.16.840.1.113883.6.103"
    '                        ElseIf Convert.ToString(sender.GetData(sender.RowSel, COL_TriggerCriteria)) = "Orders" Then    'Loinc
    '                            strSelectedCodeSystem = "2.16.840.1.113883.6.1"
    '                        ElseIf Convert.ToString(sender.GetData(sender.RowSel, COL_TriggerCriteria)) = "Snomed" Then    'SnomedCT
    '                            strSelectedCodeSystem = "2.16.840.1.113883.6.96"
    '                        ElseIf Convert.ToString(sender.GetData(sender.RowSel, COL_TriggerCriteria)) = "History" Then
    '                            If Convert.ToString(sender.GetData(sender.RowSel, COL_Snomed)) <> "" Then                                                                          'SnomedCT
    '                                strSelectedCodeSystem = "2.16.840.1.113883.6.96"
    '                                strSelectedCode = Convert.ToString(sender.GetData(sender.RowSel, COL_Snomed))
    '                            ElseIf Convert.ToString(sender.GetData(sender.RowSel, COL_Ndc)) <> "" Then                                                                           'NDC
    '                                strSelectedCodeSystem = "2.16.840.1.113883.6.69"
    '                                strSelectedCode = Convert.ToString(sender.GetData(sender.RowSel, COL_Ndc))
    '                            ElseIf Convert.ToString(sender.GetData(sender.RowSel, COL_RxNorm)) <> "" Then                                                                        'RxNorm
    '                                strSelectedCodeSystem = "2.16.840.1.113883.6.88"
    '                                strSelectedCode = Convert.ToString(sender.GetData(sender.RowSel, COL_RxNorm))
    '                            ElseIf Convert.ToString(sender.GetData(sender.RowSel, COL_Loinc)) <> "" Then                                                                         'Loinc
    '                                strSelectedCodeSystem = "2.16.840.1.113883.6.1"
    '                                strSelectedCode = Convert.ToString(sender.GetData(sender.RowSel, COL_Loinc))
    '                            End If
    '                        End If
    '                        strSelectedCodeDisplay = Convert.ToString(sender.GetData(sender.RowSel, COL_Display))

    '                        If strSelectedCodeSystem = "2.16.840.1.113883.6.96" Or
    '                           strSelectedCodeSystem = "2.16.840.1.113883.6.90" Or
    '                           strSelectedCodeSystem = "2.16.840.1.113883.6.103" Then       'SnomedCT/ICD10/ICD9
    '                            TaskContext = "PROBLISTREV"
    '                        ElseIf strSelectedCodeSystem = "2.16.840.1.113883.6.69" Or
    '                               strSelectedCodeSystem = "2.16.840.1.113883.6.88" Then   'NDC/RxNorm
    '                            TaskContext = "MLREV"
    '                        ElseIf strSelectedCodeSystem = "2.16.840.1.113883.6.1" Then    'Loinc
    '                            TaskContext = "LABRREV"
    '                        End If

    '                        If strSelectedCodeSystem = "2.16.840.1.113883.6.69" Then
    '                            Using oGSHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
    '                                strNDCCode = strSelectedCode
    '                                RxNormResponse = oGSHelper.GetRxNormCode(strSelectedCode)

    '                                If RxNormResponse IsNot Nothing Then
    '                                    strSelectedCode = RxNormResponse.Code
    '                                    RxNormResponse = Nothing
    '                                End If

    '                                strSelectedCodeSystem = "2.16.840.1.113883.6.88"
    '                            End Using
    '                        End If


    '                        ParameterString = "taskContext.c.c=" + TaskContext +
    '                                          "&mainSearchCriteria.v.c=" + strSelectedCode +
    '                                          "&mainSearchCriteria.v.cs=" + strSelectedCodeSystem +
    '                                          "&mainSearchCriteria.v.dn=" + strSelectedCodeDisplay + ""


    '                        If chkIncludeGender.Checked Then
    '                            ParameterString = ParameterString + "&patientPerson.administrativeGenderCode.c=" + Gender + ""
    '                        End If
    '                        If chkIncludeAge.Checked Then
    '                            ParameterString = ParameterString + "&age.v.v=" + sAgeValue +
    '                                                                "&age.v.u=" + sAgeUnit + ""
    '                        End If
    '                        ParameterString = ParameterString + "&informationRecipient.languageCode.c=" + strLang + ""
    '                        ParameterString = ParameterString + "&informationRecipient=" + Audience + ""
    '                        ParameterString = ParameterString + "&performer=PROV"
    '                        ParameterString = ParameterString + "&knowledgeResponseType=text/xml"

    '                        dtEducation = OpenInfobuttonSource(ParameterString)


    '                        'If No Response for RXNorm Code then Check for NDC Code
    '                        If IsNothing(dtEducation) AndAlso strSelectedCodeSystem = "2.16.840.1.113883.6.88" Then
    '                            strSelectedCodeSystem = "2.16.840.1.113883.6.69" 'NDC

    '                            ParameterString = "taskContext.c.c=" + TaskContext +
    '                                              "&mainSearchCriteria.v.c=" + strNDCCode +
    '                                              "&mainSearchCriteria.v.cs=" + strSelectedCodeSystem +
    '                                              "&mainSearchCriteria.v.dn=" + strSelectedCodeDisplay + ""
    '                            If chkIncludeGender.Checked Then
    '                                ParameterString = ParameterString + "&patientPerson.administrativeGenderCode.c=" + Gender + ""
    '                            End If
    '                            If chkIncludeAge.Checked Then
    '                                ParameterString = ParameterString + "&age.v.v=" + sAgeValue +
    '                                                                    "&age.v.u=" + sAgeUnit + ""
    '                            End If
    '                            ParameterString = ParameterString + "&informationRecipient.languageCode.c=" + strLang + ""
    '                            ParameterString = ParameterString + "&informationRecipient=" + Audience + ""
    '                            ParameterString = ParameterString + "&performer=PROV"
    '                            ParameterString = ParameterString + "&knowledgeResponseType=text/xml"
    '                            dtEducation = OpenInfobuttonSource(ParameterString)
    '                        End If
    '                        ''


    '                        If Not IsNothing(dtEducation) Then
    '                            If dtEducation.Rows.Count > 1 Then
    '                                lblInfobuttonLink.Text = GetInfobuttonURL("OpenInfobutton") + ParameterString
    '                                pnlInfoLinks.Visible = True
    '                                trvLinks.Nodes.Clear()
    '                                trvLinks.ImageKey = Nothing
    '                                trvLinks.SelectedImageIndex = -1

    '                                Dim node As TreeNode
    '                                Dim subNode As TreeNode
    '                                For Each row As DataRow In dtEducation.Rows
    '                                    'search in the treeview if any country is already present
    '                                    node = Searchnode(row.Item(0).ToString(), trvLinks)
    '                                    If node IsNot Nothing Then
    '                                        subNode = New TreeNode(row.Item(1).ToString())
    '                                        subNode.Tag = Convert.ToString(row.Item(2))
    '                                        subNode.ForeColor = Color.Blue
    '                                        node.Nodes.Add(subNode)
    '                                    Else
    '                                        node = New TreeNode(row.Item(0).ToString())
    '                                        subNode = New TreeNode(row.Item(1).ToString())
    '                                        subNode.Tag = Convert.ToString(row.Item(2))
    '                                        subNode.ForeColor = Color.Blue
    '                                        node.Nodes.Add(subNode)
    '                                        trvLinks.Nodes.Add(node)
    '                                    End If
    '                                Next

    '                                trvLinks.ExpandAll()

    '                            ElseIf dtEducation.Rows.Count = 1 Then
    '                                lblInfobuttonLink.Text = Convert.ToString(dtEducation.Rows(0)("Link"))
    '                                pnlInfoLinks.Visible = False
    '                                c1InfoLinks.DataSource = Nothing
    '                                Dim InfoButtonForm As gloEMRGeneralLibrary.frmInfoButtonBrowser = gloEMRGeneralLibrary.frmInfoButtonBrowser.GetInstance
    '                                With InfoButtonForm
    '                                    .LoginProviderID = gnLoginProviderID
    '                                    .PatientId = PatientID
    '                                    .VisitID = VisitID
    '                                    If strSelectedCodeSystem = "2.16.840.1.113883.6.96" Then       'SnomedCT
    '                                        .Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList
    '                                    ElseIf strSelectedCodeSystem = "2.16.840.1.113883.6.90" Then   'ICD10
    '                                        .Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList
    '                                    ElseIf strSelectedCodeSystem = "2.16.840.1.113883.6.103" Then  'ICD9
    '                                        .Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList
    '                                    ElseIf strSelectedCodeSystem = "2.16.840.1.113883.6.69" Then   'NDC
    '                                        .Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Medication
    '                                    ElseIf strSelectedCodeSystem = "2.16.840.1.113883.6.88" Then   'RxNorm
    '                                        .Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Medication
    '                                    ElseIf strSelectedCodeSystem = "2.16.840.1.113883.6.1" Then    'Loinc
    '                                        .Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Orders
    '                                    End If
    '                                    .EducationID = 0
    '                                    .gblnUseDefaultPrinter = blnUseDefaultPrinter
    '                                    .ResourceCategory = gloEMRGeneralLibrary.clsInfobutton.enumResourceCategory.OnlineLibrary
    '                                    .ResourceType = gloEMRGeneralLibrary.clsInfobutton.enumResourceType.PatientReferenceMaterial
    '                                    .ValidatePortalFeatures()
    '                                    .NavigateTo(Convert.ToString(dtEducation.Rows(0)("Link")))
    '                                    .Visible = False
    '                                    .Top = True
    '                                    .BringToFront()
    '                                    .Activate()
    '                                    .ShowDialog(Me)
    '                                End With
    '                            Else
    '                                pnlInfoLinks.Visible = False
    '                                c1InfoLinks.DataSource = Nothing
    '                                'lblInfobuttonLink.Text = GetURL(Me.GetURL(Gender, sAgeValue, sAgeUnit, strSelectedCode, strSelectedCodeSystem, strLang), strLang)
    '                                GetNLMInfobuttonDocument(strSelectedCode, strSelectedCodeSystem, strLang, sAgeValue, sAgeUnit, Gender)
    '                            End If
    '                        Else
    '                            pnlInfoLinks.Visible = False
    '                            c1InfoLinks.DataSource = Nothing
    '                            'lblInfobuttonLink.Text = GetURL(Me.GetURL(Gender, sAgeValue, sAgeUnit, strSelectedCode, strSelectedCodeSystem, strLang), strLang)
    '                            GetNLMInfobuttonDocument(strSelectedCode, strSelectedCodeSystem, strLang, sAgeValue, sAgeUnit, Gender)
    '                        End If

    '                        'If Not IsNothing(arrInfoLinks) Then
    '                        '    If arrInfoLinks.Count > 1 Then 'If More than one link available then display All the links
    '                        '        lblInfobuttonLink.Text = GetInfobuttonURL("OpenInfobutton") + ParameterString
    '                        '        pnlInfoLinks.Visible = True
    '                        '        Dim dtLinks As New DataTable()
    '                        '        Dim dCol1 As New DataColumn("Link")
    '                        '        dtLinks.Columns.Add(dCol1)
    '                        '        dtLinks.Columns(0).Caption = ""
    '                        '        For i As Int16 = 0 To arrInfoLinks.Count - 1 Step 1
    '                        '            dtLinks.Rows.Add()
    '                        '            dtLinks.Rows(dtLinks.Rows.Count - 1)("link") = arrInfoLinks(i)
    '                        '        Next
    '                        '        c1InfoLinks.DataSource = dtLinks
    '                        '        c1InfoLinks.AllowEditing = False
    '                        '        With c1InfoLinks
    '                        '            Dim dt As DataTable = c1InfoLinks.DataSource
    '                        '            For i As Int16 = 0 To dt.Rows.Count - 1
    '                        '                .SetCellImage(i + 1, 0, My.Resources.Browse)
    '                        '            Next
    '                        '        End With
    '                        '    ElseIf arrInfoLinks.Count = 1 Then  'If single link avialble then open the link directly
    '                        '        lblInfobuttonLink.Text = GetInfobuttonURL("OpenInfobutton") + ParameterString
    '                        '        pnlInfoLinks.Visible = False
    '                        '        c1InfoLinks.DataSource = Nothing
    '                        '        Dim InfoButtonForm As gloEMRGeneralLibrary.frmInfoButtonBrowser = gloEMRGeneralLibrary.frmInfoButtonBrowser.GetInstance
    '                        '        With InfoButtonForm
    '                        '            .LoginProviderID = gnLoginProviderID
    '                        '            .PatientId = PatientID
    '                        '            .VisitID = VisitID
    '                        '            If strSelectedCodeSystem = "2.16.840.1.113883.6.96" Then       'SnomedCT
    '                        '                .Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList
    '                        '            ElseIf strSelectedCodeSystem = "2.16.840.1.113883.6.90" Then   'ICD10
    '                        '                .Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList
    '                        '            ElseIf strSelectedCodeSystem = "2.16.840.1.113883.6.103" Then  'ICD9
    '                        '                .Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList
    '                        '            ElseIf strSelectedCodeSystem = "2.16.840.1.113883.6.69" Then   'NDC
    '                        '                .Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Medication
    '                        '            ElseIf strSelectedCodeSystem = "2.16.840.1.113883.6.88" Then   'RxNorm
    '                        '                .Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Medication
    '                        '            ElseIf strSelectedCodeSystem = "2.16.840.1.113883.6.1" Then    'Loinc
    '                        '                .Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Orders
    '                        '            End If
    '                        '            .EducationID = 0
    '                        '            .gblnUseDefaultPrinter = blnUseDefaultPrinter
    '                        '            .ResourceCategory = gloEMRGeneralLibrary.clsInfobutton.enumResourceCategory.OnlineLibrary
    '                        '            .ResourceType = gloEMRGeneralLibrary.clsInfobutton.enumResourceType.PatientReferenceMaterial
    '                        '            .ValidatePortalFeatures()
    '                        '            .NavigateTo(arrInfoLinks(0))
    '                        '            .Visible = False
    '                        '            .Top = True
    '                        '            .BringToFront()
    '                        '            .Activate()
    '                        '            .ShowDialog(Me)

    '                        '        End With
    '                        '    Else 'If links are not available,then use old Infobutton Link
    '                        '        pnlInfoLinks.Visible = False
    '                        '        c1InfoLinks.DataSource = Nothing

    '                        '        lblInfobuttonLink.Text = GetURL(Me.GetURL(Gender, sAgeValue, sAgeUnit, strSelectedCode, strSelectedCodeSystem, strLang), strLang)
    '                        '        GetNLMInfobuttonDocument(strSelectedCode, strSelectedCodeSystem, strLang, sAgeValue, sAgeUnit, Gender)
    '                        '    End If
    '                        'Else
    '                        '    pnlInfoLinks.Visible = False
    '                        '    c1InfoLinks.DataSource = Nothing
    '                        '    lblInfobuttonLink.Text = GetURL(Me.GetURL(Gender, sAgeValue, sAgeUnit, strSelectedCode, strSelectedCodeSystem, strLang), strLang)
    '                        '    GetNLMInfobuttonDocument(strSelectedCode, strSelectedCodeSystem, strLang, sAgeValue, sAgeUnit, Gender)
    '                        'End If

    '                    End If
    '                End If
    '            End With
    '            Me.Cursor = Cursors.Default
    '        Catch ex As Exception
    '            gloAuditTrail.gloAuditTrail.ExceptionLog("Openinfosource :: " & ex.ToString(), False)
    '        Finally
    '            strSelectedCode = Nothing
    '            strSelectedCodeSystem = Nothing
    '            sAgeUnit = Nothing
    '            sAgeValue = Nothing
    '            Audience = Nothing
    '            TaskContext = Nothing
    '            PatientLanguage = Nothing
    '            strLang = Nothing
    '            ParameterString = Nothing

    '            If Not IsNothing(oclsProblemListV2) Then
    '                oclsProblemListV2.Dispose()
    '                oclsProblemListV2 = Nothing
    '            End If

    '            If Not IsNothing(clsinfobutton_DM) Then
    '                clsinfobutton_DM = Nothing
    '            End If

    '            arrInfoLinks = Nothing
    '        End Try
    '    End If

    'End Sub

  
    Private Sub c1InfoLinks_MouseDoubleClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles c1InfoLinks.MouseDoubleClick
        Dim selCol As Int16 = c1InfoLinks.HitTest(e.X, e.Y).Column
        Dim selRow As Int16 = c1InfoLinks.HitTest(e.X, e.Y).Row

        Dim SelctedLink As String = c1InfoLinks.GetData(selRow, selCol)

        Dim blnUseDefaultPrinter As Boolean
        Try
            gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
            If IsNothing(gloRegistrySetting.GetRegistryValue("UseDefaultPrinter")) = False Then
                If gloRegistrySetting.GetRegistryValue("UseDefaultPrinter") = 1 Then
                    blnUseDefaultPrinter = True
                Else
                    blnUseDefaultPrinter = False
                End If
            Else
                blnUseDefaultPrinter = True
            End If
            gloRegistrySetting.CloseRegistryKey()

            Dim InfoButtonForm As gloEMRGeneralLibrary.frmInfoButtonBrowser = gloEMRGeneralLibrary.frmInfoButtonBrowser.GetInstance
            With InfoButtonForm
                .LoginProviderID = gnLoginProviderID
                .PatientId = PatientID
                .VisitID = VisitID
                If Convert.ToString(c1DmProblem.GetData(c1DmProblem.RowSel, COL_TriggerCriteria)) = "CPT" Then           'SnomedCT
                    .Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList
                ElseIf Convert.ToString(c1DmProblem.GetData(c1DmProblem.RowSel, COL_TriggerCriteria)) = "Drugs" Then     'NDC
                    .Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Medication
                ElseIf Convert.ToString(c1DmProblem.GetData(c1DmProblem.RowSel, COL_TriggerCriteria)) = "ICD10" Then     'ICD10
                    .Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList
                ElseIf Convert.ToString(c1DmProblem.GetData(c1DmProblem.RowSel, COL_TriggerCriteria)) = "ICD9" Then      'ICD9
                    .Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList
                ElseIf Convert.ToString(c1DmProblem.GetData(c1DmProblem.RowSel, COL_TriggerCriteria)) = "Orders" Then    'Loinc
                    .Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Orders
                ElseIf Convert.ToString(c1DmProblem.GetData(c1DmProblem.RowSel, COL_TriggerCriteria)) = "Snomed" Then    'SnomedCT
                    .Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList
                ElseIf Convert.ToString(c1DmProblem.GetData(c1DmProblem.RowSel, COL_TriggerCriteria)) = "History" Then
                    If Convert.ToString(c1DmProblem.GetData(c1DmProblem.RowSel, COL_Snomed)) <> "" Then                                                                          'SnomedCT
                        .Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList
                    ElseIf Convert.ToString(c1DmProblem.GetData(c1DmProblem.RowSel, COL_Ndc)) <> "" Then                                                                           'NDC
                        .Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Medication
                    ElseIf Convert.ToString(c1DmProblem.GetData(c1DmProblem.RowSel, COL_RxNorm)) <> "" Then                                                                        'RxNorm
                        .Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Medication
                    ElseIf Convert.ToString(c1DmProblem.GetData(c1DmProblem.RowSel, COL_Loinc)) <> "" Then                                                                         'Loinc
                        .Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Orders
                    End If
                End If

                .EducationID = 0
                .gblnUseDefaultPrinter = blnUseDefaultPrinter
                .ResourceCategory = gloEMRGeneralLibrary.clsInfobutton.enumResourceCategory.OnlineLibrary
                .ResourceType = gloEMRGeneralLibrary.clsInfobutton.enumResourceType.PatientReferenceMaterial
                .ValidatePortalFeatures()
                .NavigateTo(SelctedLink)
                '.Visible = True
                '.Top = True
                .BringToFront()
                '.Activate()
                .Select()
                .ShowDialog(Me.Owner)
            End With

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("Openinfosource :: " & ex.ToString(), False)
        Finally

        End Try
    End Sub

    Private Sub c1DmProblem_Click(sender As System.Object, e As System.EventArgs) Handles c1DmProblem.Click

    End Sub

    Private Sub c1InfoLinks_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles c1InfoLinks.MouseMove
        Try
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, CType(sender, C1.Win.C1FlexGrid.C1FlexGrid), e.Location)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub chkIncludeGender_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkIncludeGender.CheckedChanged
        If chkIncludeGender.Checked Then
            lblchkIncludeGender.Enabled = True
        Else
            lblchkIncludeGender.Enabled = False
        End If
    End Sub

    Private Sub chkIncludeAge_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkIncludeAge.CheckedChanged
        If chkIncludeAge.Checked Then
            lblchkIncludeAge.Enabled = True
        Else
            lblchkIncludeAge.Enabled = False
        End If
    End Sub


    Public Function OpenInfobuttonSource(ByVal ParameterString As String) As DataTable
        Dim arrLinks As New ArrayList()
        Dim CoreURL As String = ""
        Dim blnUseDefaultPrinter As Boolean
        Dim sURL As String = ""
        Dim Newurl As String = ""
        Dim strPath As String = ""


        Dim strSql As String = "select sSettingsValue from Settings where sSettingsName='OPENINFOBUTTON_URL'"
        Dim con As New SqlConnection(GetConnectionString())
        Dim cmd As New SqlCommand(strSql, con)
        Dim da As New SqlDataAdapter(cmd)
        Dim dtSetting As New DataTable
        Dim xDoc As New XmlDocument()


        Try
            gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
            If IsNothing(gloRegistrySetting.GetRegistryValue("UseDefaultPrinter")) = False Then
                If gloRegistrySetting.GetRegistryValue("UseDefaultPrinter") = 1 Then
                    blnUseDefaultPrinter = True
                Else
                    blnUseDefaultPrinter = False
                End If
            Else
                blnUseDefaultPrinter = True
            End If
            gloRegistrySetting.CloseRegistryKey()

            con.Open()
            da.Fill(dtSetting)
            da.Dispose()
            da = Nothing

            If Not IsNothing(dtSetting) Then
                CoreURL = Convert.ToString(dtSetting.Rows(0)("sSettingsValue"))
                dtSetting.Dispose()
                dtSetting = Nothing
            End If

            Newurl = CoreURL + ParameterString

            Dim oclient As New WebClient
            xDoc.LoadXml(oclient.DownloadString(Newurl))
            Dim dtEducation As New DataTable
            Dim colSource As New DataColumn("Source")
            Dim colTitle As New DataColumn("Title")
            Dim colLink As New DataColumn("Link")

            dtEducation.Columns.Add(colSource)
            dtEducation.Columns.Add(colTitle)
            dtEducation.Columns.Add(colLink)


            Dim xnl As XmlNodeList = xDoc.GetElementsByTagName("entry")
            Dim xnlFeed As XmlNodeList = xDoc.GetElementsByTagName("feed")
            Dim strSource As String = ""
            Dim strTitle As String = ""
            Dim strSubTitle As String = ""
            Dim strLink As String = ""

            For Each node As XmlNode In xnlFeed
                For Each childNode As XmlNode In node.ChildNodes
                    If childNode.Name = "title" Then
                        strSource = childNode.InnerText
                    ElseIf childNode.Name = "entry" Then
                        xnl = childNode.ChildNodes
                        For Each ccNode As XmlNode In xnl
                            If ccNode.Name = "link" Then
                                strLink = ccNode.Attributes("href").Value

                                Dim dRow As DataRow = dtEducation.NewRow()
                                dRow("Source") = strSource
                                dRow("Title") = strTitle
                                'dRow("SubTitle") = strSubTitle
                                dRow("Link") = strLink
                                dtEducation.Rows.Add(dRow)

                            ElseIf ccNode.Name = "title" Then
                                strTitle = Convert.ToString(ccNode.InnerText)
                            ElseIf ccNode.Name = "subtitle" Then
                                strSubTitle = Convert.ToString(ccNode.InnerText)
                            End If

                        Next
                    End If
                Next
            Next

            oclient.Dispose()
            oclient = Nothing
            Return dtEducation
        Catch ex As WebException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.GetType().ToString() + " : " + ex.Message, False)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.GetType().ToString() + " : " + ex.Message, False)
            Return Nothing
        Finally

            CoreURL = Nothing
            sURL = Nothing
            strSql = Nothing
            Newurl = Nothing
            strPath = Nothing

            If Not IsNothing(xDoc) Then
                xDoc = Nothing
            End If

            If Not IsNothing(con) Then
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Dispose()
                con = Nothing
            End If

            If Not IsNothing(cmd) Then
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If

            If Not IsNothing(dtSetting) Then
                dtSetting.Dispose()
                dtSetting = Nothing
            End If
        End Try

    End Function

    Private Function Searchnode(ByVal nodetext As String, ByVal trv As TreeView) As TreeNode
        For Each node As TreeNode In trv.Nodes
            If node.Text = nodetext Then
                Return node
            End If
        Next
        Return Nothing
    End Function

    Private Sub btnHideLinks_Click(sender As System.Object, e As System.EventArgs) Handles btnHideLinks.Click
        pnlInfoLinks.Hide()
        c1DmProblem.Focus()
        Me.Cursor = Cursors.Default
    End Sub


    Private Function GetInfobuttonURL(ByVal linkModule As String) As String
        Dim sURL As String = ""
        Dim strSql As String = ""
        If linkModule = "OpenInfobutton" Then
            strSql = "select sSettingsValue from Settings where sSettingsName='OPENINFOBUTTON_URL'"
        Else
            strSql = "select sSettingsValue from Settings where sSettingsName='INFOBUTTON_URL'"
        End If

        Dim con As New SqlConnection(GetConnectionString)
        Dim cmd As New SqlCommand(strSql, con)
        Dim CoreURL As String = ""

        Try
            con.Open()
            Dim da As New SqlDataAdapter(cmd)
            Dim dtSetting As New DataTable
            da.Fill(dtSetting)
            da.Dispose()
            da = Nothing

            If Not IsNothing(dtSetting) Then
                CoreURL = Convert.ToString(dtSetting.Rows(0)("sSettingsValue"))
                dtSetting.Dispose()
                dtSetting = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("Openinfosource :: " & ex.ToString(), False)
        Finally

            If Not IsNothing(con) Then ''connection state closed
                If (con.State = ConnectionState.Open) Then
                    con.Close()
                End If
                con.Dispose()
            End If

        End Try

        Return CoreURL
    End Function

    Private Function GetURL(ByVal sGender As String, ByVal sAge As String, ByVal sAgeUnit As String, ByVal Code As String, ByVal CodeSystem As String, ByVal strLang As String)
        Dim sURL As String = ""
        Dim strSql As String = "select sSettingsValue from Settings where sSettingsName='INFOBUTTON_URL'"
        Dim con As New SqlConnection(GetConnectionString())
        Dim cmd As New SqlCommand(strSql, con)
        Dim CoreURL As String = ""

        Try
            con.Open()
            Dim da As New SqlDataAdapter(cmd)
            Dim dtSetting As New DataTable
            da.Fill(dtSetting)
            da.Dispose()
            da = Nothing

            If Not IsNothing(dtSetting) Then
                CoreURL = Convert.ToString(dtSetting.Rows(0)("sSettingsValue"))
                dtSetting.Dispose()
                dtSetting = Nothing
            End If

            sURL = CoreURL + "patientPerson.administrativeGenderCode.c=" + sGender + "&" +
                                "age.v.v=" + sAge + "&age.v.u=" + sAgeUnit + "&" + "mainSearchCriteria.v.c=" + Code + "&mainSearchCriteria.v.cs=" + CodeSystem + "&" +
                                 "performer.languageCode.c=en&informationRecipient.languageCode.c=" + strLang + "&knowledgeResponseType=text/XML"
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("Openinfosource :: " & ex.ToString(), False)
        End Try

        Return sURL
    End Function

    Public Function Geturl(ByVal strUrl As String, ByVal _PatientLanguage As String) As String

        Dim oclient As New WebClient
        Dim urlstring As String = oclient.DownloadString(strUrl)
        oclient.Dispose()
        oclient = Nothing
        Dim FinalUrl As String = strUrl
        Dim informationStringen As String = ""
        Dim informationStringSP As String = ""
        Dim NotHavingStringen As String = ""
        Dim NotHavingStringsp As String = ""
        Dim FootterString As String = ""
        Dim tochkTitle As String = ""
        Dim informationString As String = ""
        Dim NotHavingString As String = ""

        'below Added for Bug #93626: 00001080: Problem List info button
        ' and new settings value inserted in database setting table
        Dim infoLogoStringEN As String = ""
        Dim infoSingleResultStringEN As String = ""
        Dim infoMultiResultStringEN As String = ""
        Dim infoLogoStringSP As String = ""
        Dim infoSingleResultStringSP As String = ""
        Dim infoMultiResultStringSP As String = ""
        Try
            Dim dtSettings As DataTable = GetInfobuttonSettings()
            If Not IsNothing(dtSettings) Then
                If dtSettings.Rows.Count > 0 Then
                    For i As Integer = 0 To dtSettings.Rows.Count - 1 Step 1
                        Select Case Convert.ToString(dtSettings.Rows(i)("sSettingsName"))
                            Case "Infobutton_Information_StringEN"
                                informationStringen = Convert.ToString(dtSettings.Rows(i)("sSettingsValue"))
                            Case "Infobutton_Information_StringSP"
                                informationStringSP = Convert.ToString(dtSettings.Rows(i)("sSettingsValue"))
                            Case "Infobutton_NotHaving_StringEN"
                                NotHavingStringen = Convert.ToString(dtSettings.Rows(i)("sSettingsValue"))
                            Case "Infobutton_NotHaving_StringSP"
                                NotHavingStringsp = Convert.ToString(dtSettings.Rows(i)("sSettingsValue"))
                            Case "Infobutton_FooterString"
                                FootterString = Convert.ToString(dtSettings.Rows(i)("sSettingsValue"))
                            Case "Infobutton_tochkTitle"
                                tochkTitle = Convert.ToString(dtSettings.Rows(i)("sSettingsValue"))

                                'below Added for Bug #93626: 00001080: Problem List info button
                            Case "INFOBUTTON_LOGOTEXT_EN"
                                infoLogoStringEN = Convert.ToString(dtSettings.Rows(i)("sSettingsValue"))
                            Case "INFOBUTTON_LOGOTEXT_SP"
                                infoLogoStringSP = Convert.ToString(dtSettings.Rows(i)("sSettingsValue"))
                            Case "INFOBUTTON_ResultSingle_EN"
                                infoSingleResultStringEN = Convert.ToString(dtSettings.Rows(i)("sSettingsValue"))
                            Case "INFOBUTTON_ResultSingle_SP"
                                infoSingleResultStringSP = Convert.ToString(dtSettings.Rows(i)("sSettingsValue"))
                            Case "INFOBUTTON_ResultMulti_EN"
                                infoMultiResultStringEN = Convert.ToString(dtSettings.Rows(i)("sSettingsValue"))
                            Case "INFOBUTTON_ResultMulti_SP"
                                infoMultiResultStringSP = Convert.ToString(dtSettings.Rows(i)("sSettingsValue"))
                        End Select
                    Next
                End If
                dtSettings.Dispose()
                dtSettings = Nothing
            End If

            ' Added by hemant for Bug #93626: 00001080: Problem List info button
            Dim startSearchString As String = ""
            Dim endSearchString As String = ""
            If _PatientLanguage = "sp" Then
                informationString = informationStringSP
                NotHavingString = NotHavingStringsp
                startSearchString = infoLogoStringSP
                endSearchString = infoMultiResultStringSP
            Else
                informationString = informationStringen
                NotHavingString = NotHavingStringen
                startSearchString = infoLogoStringEN
                endSearchString = infoMultiResultStringEN
            End If

            'if setting does not exist then continue to execute as per old implementation
            If Not (String.IsNullOrEmpty(startSearchString) Or String.IsNullOrEmpty(endSearchString)) Then
                Dim startSearchIndex As Integer = urlstring.IndexOf(startSearchString)
                If startSearchIndex <> -1 Then
                    Dim startSearchIndexFinal As Integer = startSearchIndex + startSearchString.Length
                    ' if single results found singular and plural for end serach string
                    Dim endSearchStringIndex As Integer = urlstring.IndexOf(endSearchString)
                    If endSearchStringIndex = -1 Then
                        If _PatientLanguage = "en" Then
                            endSearchString = infoSingleResultStringEN
                        ElseIf _PatientLanguage = "sp" Then
                            endSearchString = infoSingleResultStringSP
                        End If
                        endSearchStringIndex = urlstring.IndexOf(endSearchString)
                    End If
                    If endSearchStringIndex <> -1 And endSearchStringIndex > startSearchIndexFinal Then
                        Dim strBetween As String = urlstring.Substring(startSearchIndexFinal, endSearchStringIndex - startSearchIndexFinal + endSearchString.Length)
                        Dim strFinalBetween As String = strBetween.Substring(strBetween.LastIndexOf(">") + 1)

                        If Not String.IsNullOrEmpty(strFinalBetween) Then
                            Dim strArr() As String
                            strArr = strFinalBetween.Trim().Split()
                            Dim cnt As Integer
                            If strArr.Length >= 3 Then
                                If Integer.TryParse(strArr(0), cnt) Then
                                    If cnt = 0 Or cnt > 1 Then
                                        Return FinalUrl
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
            '   Added by hemant for Bug #93626: 00001080: Problem List info button Ends here 
            Dim myInformationValue As Integer = urlstring.IndexOf(informationString)
            Dim mySubstring As String = urlstring
            If (myInformationValue = -1) Then
                If (urlstring.IndexOf(NotHavingString) > -1) Then
                    Return FinalUrl
                End If
            End If
            If (myInformationValue > -1) Then
                mySubstring = urlstring.Substring(myInformationValue + Len(informationString))
            End If
            '  Dim myArticleValue As Integer = mySubstring.IndexOf(toFind)
            Dim myTitleValue As Integer = mySubstring.IndexOf(tochkTitle)
            '  Dim myFooterValue As Integer = mySubstring.IndexOf(FootterString)

            Dim resultstr As String = mySubstring.Substring(myTitleValue)

            Dim myhtm As Integer = resultstr.IndexOf(".htm")
            Dim myLess As Integer = resultstr.IndexOf("<")
            Do
                If myLess < myhtm Then
                    mySubstring = resultstr.Substring(Len(tochkTitle))
                    myTitleValue = mySubstring.IndexOf(tochkTitle)
                    resultstr = mySubstring.Substring(myTitleValue)
                    myhtm = resultstr.IndexOf(".htm")
                    myLess = resultstr.IndexOf("<")
                Else
                    Exit Do
                End If
            Loop

            myhtm = resultstr.IndexOf(".htm""")
            Dim totalLength As Integer = 0
            If myhtm = -1 Or myhtm > 200 Then
                myhtm = resultstr.IndexOf(".html")
                totalLength = 5
            Else

                totalLength = 4
            End If
            If myhtm <> -1 Then
                FinalUrl = resultstr.Substring(0, myhtm + totalLength)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally

        End Try

        Return FinalUrl
    End Function

    Public Function GetInfobuttonSettings() As DataTable
        Dim Con As New SqlConnection(GetConnectionString())
        Dim cmd As SqlCommand = Nothing
        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If
        Dim sqladpt As SqlDataAdapter = Nothing
        Dim dtBibliographicinfo As New DataTable()
        Try
            cmd = New SqlCommand("SELECT nSettingsID,sSettingsName,sSettingsValue  FROM Settings WHERE sSettingsName like '%Infobutton%'", Con)
            sqladpt = New SqlDataAdapter
            sqladpt.SelectCommand = cmd
            sqladpt.Fill(dtBibliographicinfo)
            Return dtBibliographicinfo
        Catch ex As SqlException
            Return Nothing
        Catch ex As Exception
            Return Nothing
        Finally
            Con.Close()
            Con.Dispose()
            Con = Nothing
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(sqladpt) Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
        End Try
    End Function

    Private Sub trvLinks_NodeMouseClick(sender As System.Object, e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvLinks.NodeMouseClick
        Dim blnUseDefaultPrinter As Boolean
        Try
            Dim SelectedLink As String = Convert.ToString(e.Node.Tag)

            If SelectedLink <> "" Then
                gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
                If IsNothing(gloRegistrySetting.GetRegistryValue("UseDefaultPrinter")) = False Then
                    If gloRegistrySetting.GetRegistryValue("UseDefaultPrinter") = 1 Then
                        blnUseDefaultPrinter = True
                    Else
                        blnUseDefaultPrinter = False
                    End If
                Else
                    blnUseDefaultPrinter = True
                End If
                gloRegistrySetting.CloseRegistryKey()

                Dim InfoButtonForm As gloEMRGeneralLibrary.frmInfoButtonBrowser = gloEMRGeneralLibrary.frmInfoButtonBrowser.GetInstance
                With InfoButtonForm
                    .LoginProviderID = gnLoginProviderID
                    .PatientId = PatientID
                    .VisitID = VisitID
                    If Convert.ToString(c1DmProblem.GetData(c1DmProblem.RowSel, COL_TriggerCriteria)) = "CPT" Then           'SnomedCT
                        .Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList
                    ElseIf Convert.ToString(c1DmProblem.GetData(c1DmProblem.RowSel, COL_TriggerCriteria)) = "Drugs" Then     'NDC
                        .Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Medication
                    ElseIf Convert.ToString(c1DmProblem.GetData(c1DmProblem.RowSel, COL_TriggerCriteria)) = "ICD10" Then     'ICD10
                        .Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList
                    ElseIf Convert.ToString(c1DmProblem.GetData(c1DmProblem.RowSel, COL_TriggerCriteria)) = "ICD9" Then      'ICD9
                        .Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList
                    ElseIf Convert.ToString(c1DmProblem.GetData(c1DmProblem.RowSel, COL_TriggerCriteria)) = "Orders" Then    'Loinc
                        .Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Orders
                    ElseIf Convert.ToString(c1DmProblem.GetData(c1DmProblem.RowSel, COL_TriggerCriteria)) = "Snomed" Then    'SnomedCT
                        .Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList
                    ElseIf Convert.ToString(c1DmProblem.GetData(c1DmProblem.RowSel, COL_TriggerCriteria)) = "History" Then
                        If Convert.ToString(c1DmProblem.GetData(c1DmProblem.RowSel, COL_Snomed)) <> "" Then                                                                          'SnomedCT
                            .Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList
                        ElseIf Convert.ToString(c1DmProblem.GetData(c1DmProblem.RowSel, COL_Ndc)) <> "" Then                                                                           'NDC
                            .Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Medication
                        ElseIf Convert.ToString(c1DmProblem.GetData(c1DmProblem.RowSel, COL_RxNorm)) <> "" Then                                                                        'RxNorm
                            .Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Medication
                        ElseIf Convert.ToString(c1DmProblem.GetData(c1DmProblem.RowSel, COL_Loinc)) <> "" Then                                                                         'Loinc
                            .Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Orders
                        End If
                    End If

                    .EducationID = 0
                    .gblnUseDefaultPrinter = blnUseDefaultPrinter
                    .ResourceCategory = gloEMRGeneralLibrary.clsInfobutton.enumResourceCategory.OnlineLibrary
                    .ResourceType = gloEMRGeneralLibrary.clsInfobutton.enumResourceType.PatientReferenceMaterial
                    .ValidatePortalFeatures()
                    .NavigateTo(SelectedLink)
                    '.Visible = True
                    '.Top = True
                    .BringToFront()
                    '.Activate()
                    .Select()
                    .ShowDialog(Me.Owner)
                End With

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("Openinfosource :: " & ex.ToString(), False)
        Finally

        End Try
    End Sub

   
End Class