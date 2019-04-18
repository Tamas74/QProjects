Imports System.IO


Public Class frmCMSPrinterSettingsNew

    Dim _OutputFilePath As String
    Dim isSaveAndClose As Boolean = False
    Dim bIsReset As Boolean
    Dim bIsInvalidData As Boolean
    Dim _selectedPrinter As String
    Public _IsSelected As Boolean = False
    Dim bIsCapitalized As Boolean = False



#Region " C1 Constants "
    Private Const COL_CAPTION = 0
    Private Const COL_BOXNAME = 1
    Private Const COL_X = 2
    Private Const COL_Y = 3
    Private Const COL_PANEL = 4
    Private Const COL_CharSize = 5
#End Region
    ',Hemant
    Public Sub New()
        InitializeComponent()
    End Sub
    Public Sub New(ByVal name As String)
        InitializeComponent()
        _selectedPrinter = name

    End Sub

    'Hemant

#Region " Form Load and Close "

    Private Sub frmCMSPrinterSettings_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            FillSettingGrid(_selectedPrinter) 'by hemant
            ResetLabels()          
            Me.Text = _selectedPrinter
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
        End Try
    End Sub


    Private Sub frmCMSPrinterSettings_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        If isSaveAndClose = False Then

            If CloseForm() = True Then

            Else
                e.Cancel = True
            End If
        End If

    End Sub

#End Region

#Region " Private Methods "

    'Private Sub FillSettingGrid()
    '    Try
    '        Dim oCMS As New clsCMSSettingsNew()
    '        Dim dtSettings As DataTable
    '        dtSettings = oCMS.GetCMSPrinterSettings()
    '        If dtSettings IsNot Nothing Then
    '            If dtSettings.Rows.Count > 0 Then
    '                C1Settings.DataSource = dtSettings
    '                DesignGrid()
    '            Else '' IF NO SETTINGS FOUND THEN LOAD DEFAULT SETTINGS ''
    '                dtSettings = oCMS.GetDefaultCMSPrinterSettings()
    '                If dtSettings IsNot Nothing Then
    '                    C1Settings.DataSource = dtSettings
    '                    DesignGrid()
    '                End If
    '            End If
    '        End If

    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
    '    End Try
    'End Sub
    'Hemant

    Private Sub FillSettingGrid(ByVal printerName As String)
        Try
            Dim oCMS As New clsCMSSettingsNew()
            Dim dtSettings As DataTable
            dtSettings = oCMS.GetCMSPrinterSettings(printerName)
            If dtSettings IsNot Nothing Then
                If dtSettings.Rows.Count > 0 Then
                    C1Settings.DataSource = dtSettings
                    DesignGrid()
                Else '' IF NO SETTINGS FOUND THEN LOAD DEFAULT SETTINGS ''
                    dtSettings = oCMS.GetDefaultCMSPrinterSettings()
                    If dtSettings IsNot Nothing Then
                        C1Settings.DataSource = dtSettings
                        DesignGrid()
                    End If
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
        End Try
    End Sub
    'Hemant

    Private Sub ResetLabels()
        lblX.Text = 0
        lblX1.Text = 0
        lblX2.Text = 0
        lblX3.Text = 0
        lblX4.Text = 0
        lblX5.Text = 0
        lblX6.Text = 0
        lblX7.Text = 0
        lblX8.Text = 0
        lblX9.Text = 0
        lblX10.Text = 0
        lblX11.Text = 0
        lblX12.Text = 0

        lblY.Text = 0
        lblY1.Text = 0
        lblY2.Text = 0
        lblY3.Text = 0
        lblY4.Text = 0
        lblY5.Text = 0
        lblY6.Text = 0
        lblY7.Text = 0
        lblY8.Text = 0
        lblY9.Text = 0
        lblY10.Text = 0
        lblY11.Text = 0
        lblY12.Text = 0

    End Sub

    Private Sub ResetSettings()
        Try
            Dim oCMS As New clsCMSSettingsNew()
            Dim dtSettings As DataTable
            dtSettings = Nothing
            dtSettings = oCMS.GetPrinterSpecificDefaultSettings(_selectedPrinter)
            If dtSettings IsNot Nothing AndAlso dtSettings.Rows.Count > 0 Then
                If dtSettings.Rows.Count > 0 Then
                    C1Settings.DataSource = dtSettings
                    DesignGrid()
                End If
            Else
                dtSettings = Nothing
                dtSettings = oCMS.GetDefaultCMSPrinterSettings()
                If dtSettings IsNot Nothing Then
                    If dtSettings.Rows.Count > 0 Then
                        C1Settings.DataSource = dtSettings
                        DesignGrid()
                    End If
                End If
            End If

                ResetLabels()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try
    End Sub

    Private Sub DesignGrid()
        gloC1FlexStyle.Style(C1Settings)
        C1Settings.AllowEditing = True
        C1Settings.Cols(COL_CAPTION).AllowEditing = False
        C1Settings.Cols(COL_X).AllowEditing = True
        C1Settings.Cols(COL_Y).AllowEditing = True
        C1Settings.Cols(COL_CharSize).AllowEditing = True

        'C1Settings.Cols(COL_Y).Editor.DataBindings.

        C1Settings.Cols(COL_CAPTION).Width = C1Settings.Width * 0.55
        C1Settings.Cols(COL_X).Width = C1Settings.Width * 0.1
        C1Settings.Cols(COL_Y).Width = C1Settings.Width * 0.1
        C1Settings.Cols(COL_CharSize).Width = C1Settings.Width * 0.2



        C1Settings.Cols(COL_CAPTION).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        C1Settings.Cols(COL_X).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        C1Settings.Cols(COL_Y).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        C1Settings.Cols(COL_CharSize).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

        C1Settings.Cols(COL_BOXNAME).Visible = False
        C1Settings.Cols(COL_PANEL).Visible = False
    End Sub
    Private Function CloseForm() As Boolean

        Dim res As DialogResult = MessageBox.Show("Do you want to save changes ? ", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)
        If res = DialogResult.Yes Then
            SaveCMSSettings()
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Return True
        End If
        If res = DialogResult.No Then
            Return True
        End If
        If res = DialogResult.Cancel Then
            Return False
        End If

    End Function
    ' 12032013 Sameer - RowChange() is used for forcefully calling C1Settings_RowValidating Event  
    Private Sub RowChange()
        Dim rowIndex As Integer = C1Settings.RowSel
        If C1Settings.Rows.Count - 1 = C1Settings.RowSel Then
            C1Settings.Select(C1Settings.RowSel - 1, C1Settings.ColSel)
        Else
            C1Settings.Select(C1Settings.RowSel + 1, C1Settings.ColSel)
        End If
        C1Settings.Select(rowIndex, C1Settings.ColSel)
    End Sub
    Private Sub SaveCMSSettings()
        Try

            '11-20-2013 SP added For saving record Sameer  
            C1Settings.FinishEditing()
            If C1Settings.Rows.Count > 1 Then
                Me.Cursor = Cursors.WaitCursor
                Dim oDB As New gloDatabaseLayer.DBLayer(mdlGeneral.GetConnectionString)
                Dim dtAllRecords As DataTable = C1Settings.DataSource()
                Dim dtModifiedRecords As DataTable
                If bIsReset = True Or _IsSelected Then
                    dtModifiedRecords = dtAllRecords
                Else
                    dtModifiedRecords = dtAllRecords.GetChanges(DataRowState.Modified Or DataRowState.Added)
                End If

                dtAllRecords.AcceptChanges()
                Try
                    If Not dtModifiedRecords Is Nothing Then
                        oDB.Connect(False)
                        For Each dRow As DataRow In dtModifiedRecords.Rows
                            Dim oDBParameters As New gloDatabaseLayer.DBParameters()
                            oDBParameters.Add("@sBoxCaption", dRow.Item(COL_CAPTION).ToString.Replace("'", "''"), ParameterDirection.Input, SqlDbType.VarChar)
                            oDBParameters.Add("@sBoxName", dRow.Item(COL_BOXNAME).ToString.Replace("'", "''"), ParameterDirection.Input, SqlDbType.VarChar)
                            oDBParameters.Add("@nXCoordinate", dRow.Item(COL_X).ToString(), ParameterDirection.Input, SqlDbType.Int)
                            oDBParameters.Add("@nYCoordinate", dRow.Item(COL_Y).ToString(), ParameterDirection.Input, SqlDbType.Int)
                            oDBParameters.Add("@sPrinterName", _selectedPrinter, ParameterDirection.Input, SqlDbType.VarChar)

                            oDBParameters.Add("@nClinicID", 1, ParameterDirection.Input, SqlDbType.Int)
                            oDBParameters.Add("@nFormType", 1, ParameterDirection.Input, SqlDbType.Int)
                            oDBParameters.Add("@nPanel", dRow.Item(COL_PANEL).ToString, ParameterDirection.Input, SqlDbType.Int)
                            oDBParameters.Add("@nCharSize", dRow.Item(COL_CharSize).ToString, ParameterDirection.Input, SqlDbType.Int)
                            oDB.Execute("gsp_INSERT_UpdateNewCms1500Settings", oDBParameters)
                            If oDBParameters IsNot Nothing Then
                                oDBParameters.Dispose()
                                oDBParameters = Nothing
                            End If
                        Next
                    End If


                Catch dbEx As gloDatabaseLayer.DBException
                    dbEx.ERROR_Log(dbEx.ToString())
                Catch ex As Exception
                    ex.ToString()
                Finally
                    oDB.Disconnect()
                    oDB.Dispose()

                End Try

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        Finally
            Me.Cursor = Cursors.Default
            bIsReset = False
        End Try
    End Sub


    Private Sub SaveAsDefaultsCMSSettings()
        Try
            '11-20-2013 SP added For saving record Sameer
            C1Settings.FinishEditing()
            If C1Settings.Rows.Count > 1 Then
                Me.Cursor = Cursors.WaitCursor
                Dim oDB As New gloDatabaseLayer.DBLayer(mdlGeneral.GetConnectionString)
                Dim dtAllRecords As DataTable = DirectCast(C1Settings.DataSource(), DataTable) '.GetChanges(DataRowState.Modified Or DataRowState.Added)

                Try
                    If Not dtAllRecords Is Nothing Then
                        oDB.Connect(False)

                        For Each dRow As DataRow In dtAllRecords.Rows
                            Dim oDBParameters As New gloDatabaseLayer.DBParameters()
                            oDBParameters.Add("@sBoxCaption", dRow.Item(COL_CAPTION).ToString.Replace("'", "''"), ParameterDirection.Input, SqlDbType.VarChar)
                            oDBParameters.Add("@sBoxName", dRow.Item(COL_BOXNAME).ToString.Replace("'", "''"), ParameterDirection.Input, SqlDbType.VarChar)
                            oDBParameters.Add("@nXCoordinate", dRow.Item(COL_X).ToString(), ParameterDirection.Input, SqlDbType.Int)
                            oDBParameters.Add("@nYCoordinate", dRow.Item(COL_Y).ToString(), ParameterDirection.Input, SqlDbType.Int)
                            oDBParameters.Add("@sPrinterName", _selectedPrinter, ParameterDirection.Input, SqlDbType.VarChar)
                            oDBParameters.Add("@nClinicID", 1, ParameterDirection.Input, SqlDbType.Int)
                            oDBParameters.Add("@nFormType", 0, ParameterDirection.Input, SqlDbType.Int)
                            oDBParameters.Add("@nPanel", dRow.Item(COL_PANEL).ToString, ParameterDirection.Input, SqlDbType.Int)
                            oDBParameters.Add("@nCharSize", dRow.Item(COL_CharSize).ToString, ParameterDirection.Input, SqlDbType.Int)
                            oDB.Execute("gsp_INSERT_UpdateNewCms1500Settings", oDBParameters)
                            If oDBParameters IsNot Nothing Then
                                oDBParameters.Dispose()
                                oDBParameters = Nothing
                            End If
                        Next
                    End If
                Catch dbEx As gloDatabaseLayer.DBException
                    dbEx.ERROR_Log(dbEx.ToString())
                    If oDB IsNot Nothing Then
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing

                    End If

                Catch ex As Exception
                    ex.ToString()
                Finally
                    oDB.Disconnect()
                    oDB.Dispose()
                    Me.Cursor = Cursors.Default
                End Try

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try
    End Sub

    Private Function SetHCFA1500PaperForm() As gloHCFA1500PaperFormNew
        Dim oHCFA1500 As New gloHCFA1500PaperFormNew(_selectedPrinter) 'by hemant

        Try
            Dim clsPrintCMS1500PaperForm As New gloPrintPaperFormNew
            Dim _result As String = clsPrintCMS1500PaperForm.GetFontSetupSettingformCmsAndUB("Capatalize CMS1500 02/12 data")
            bIsCapitalized = Convert.ToBoolean(If(_result = "", False, Convert.ToBoolean(_result)))

            If IsNothing(clsPrintCMS1500PaperForm) Then
                clsPrintCMS1500PaperForm.Dispose()
                clsPrintCMS1500PaperForm = Nothing
            End If

            oHCFA1500.CF_Top_InsuranceHeader.Value = formatedData(oHCFA1500.CF_Top_InsuranceHeader, "goClinic" + Environment.NewLine() + "Blue Cross Blue Shield of New York" + Environment.NewLine() + "PO Box 9999 Address 2" + Environment.NewLine() + "Empire State NY 100001")

            oHCFA1500.CF_1_Insuracne_Type_Champva.Value = True
            oHCFA1500.CF_1_Insuracne_Type_FECA.Value = True
            oHCFA1500.CF_1_Insuracne_Type_GroupHealthPlan.Value = True
            oHCFA1500.CF_1_Insuracne_Type_Other.Value = True
            oHCFA1500.CF_1_Insuracne_Type_Medicaid.Value = True
            oHCFA1500.CF_1_Insuracne_Type_Medicare.Value = True
            oHCFA1500.CF_1_Insuracne_Type_Tricare.Value = True


            oHCFA1500.CF_10_PatientConditionTo_AutoAccident_No.Value = True
            oHCFA1500.CF_10_PatientConditionTo_AutoAccident_Yes.Value = True

            oHCFA1500.CF_10_PatientConditionTo_Employement_No.Value = True
            oHCFA1500.CF_10_PatientConditionTo_Employement_Yes.Value = True

            oHCFA1500.CF_10_PatientConditionTo_OtherAccident_No.Value = True
            oHCFA1500.CF_10_PatientConditionTo_OtherAccident_Yes.Value = True
            oHCFA1500.CF_10_PatientConditionTo_AutoAccident_State.Value = formatedData(oHCFA1500.CF_10_PatientConditionTo_AutoAccident_State, "ST")

            oHCFA1500.CF_8_Reserved_For_Nucc_Use.Value = formatedData(oHCFA1500.CF_8_Reserved_For_Nucc_Use, "Reserved For Nucc Use")
            'oHCFA1500.CF_8_PatientStatus_FullTimeStudent.Value = True
            'oHCFA1500.CF_8_PatientStatus_Married.Value = True
            'oHCFA1500.CF_8_PatientStatus_Other.Value = True
            ''Commented on 17/10/2013 Because it is removed from new CMS 1500 format
            ''oHCFA1500.CF_8_PatientStatus_PartTimeStudent.Value = True
            'oHCFA1500.CF_8_PatientStatus_Single.Value = True

            oHCFA1500.CF_6_PatientRelationship_Child.Value = True
            oHCFA1500.CF_6_PatientRelationship_Other.Value = True
            oHCFA1500.CF_6_PatientRelationship_Self.Value = True
            oHCFA1500.CF_6_PatientRelationship_Spouse.Value = True

            oHCFA1500.CF_7_Insureds_Address.Value = formatedData(oHCFA1500.CF_7_Insureds_Address, "Insured Address")
            oHCFA1500.CF_7_Insureds_City.Value = formatedData(oHCFA1500.CF_7_Insureds_City, "City")
            oHCFA1500.CF_7_Insureds_State.Value = formatedData(oHCFA1500.CF_7_Insureds_State, "ST")
            oHCFA1500.CF_7_Insureds_Tel_AreaCode.Value = formatedData(oHCFA1500.CF_7_Insureds_Tel_AreaCode, "123")
            oHCFA1500.CF_7_Insureds_Tel_Number.Value = formatedData(oHCFA1500.CF_7_Insureds_Tel_Number, "123456789")
            oHCFA1500.CF_7_Insureds_Zip.Value = formatedData(oHCFA1500.CF_7_Insureds_Zip, "123456789")
            oHCFA1500.CF_11_Insureds_DOB_DD.Value = formatedData(oHCFA1500.CF_11_Insureds_DOB_DD, "10")
            oHCFA1500.CF_11_Insureds_DOB_MM.Value = formatedData(oHCFA1500.CF_11_Insureds_DOB_MM, "10")
            oHCFA1500.CF_11_Insureds_DOB_YY.Value = formatedData(oHCFA1500.CF_11_Insureds_DOB_YY, "2000")

            ' Line commented on 17/10/2013 because EMPLOYER’S NAME OR SCHOOL changed to Other Claim Id 

            oHCFA1500.CF_11_Other_Claim_ID_Designated_by_NUCC.Value = formatedData(oHCFA1500.CF_11_Other_Claim_ID_Designated_by_NUCC, "Other Claim ID")

            oHCFA1500.CF_11_Insureds_InsuracnePlan.Value = formatedData(oHCFA1500.CF_11_Insureds_InsuracnePlan, "InsurancePlan")
            oHCFA1500.CF_11_Insureds_OtherHealthPlan_No.Value = True
            oHCFA1500.CF_11_Insureds_OtherHealthPlan_Yes.Value = True

            oHCFA1500.CF_11_Insureds_PolicyGroupNo.Value = formatedData(oHCFA1500.CF_11_Insureds_PolicyGroupNo, "1234")
            oHCFA1500.CF_11_Insureds_Sex_Female.Value = True
            oHCFA1500.CF_11_Insureds_Sex_Male.Value = True
            oHCFA1500.CF_11_Qualifier_No.Value = formatedData(oHCFA1500.CF_11_Qualifier_No, "11Q")
            ' oHCFA1500.CF_11_Qualifier.Value = "Qualifier"

            ' CF_11_Other_Claim_ID_Designated_by_NUCC


            oHCFA1500.CF_1a_InsuredsIDNumber.Value = formatedData(oHCFA1500.CF_1a_InsuredsIDNumber, "1323")
            oHCFA1500.CF_2_Patient_Name.Value = formatedData(oHCFA1500.CF_2_Patient_Name, "Test Patient")
            oHCFA1500.CF_3_Patient_DOB_DD.Value = formatedData(oHCFA1500.CF_3_Patient_DOB_DD, "10")
            oHCFA1500.CF_3_Patient_DOB_MM.Value = formatedData(oHCFA1500.CF_3_Patient_DOB_MM, "10")
            oHCFA1500.CF_3_Patient_DOB_YY.Value = formatedData(oHCFA1500.CF_3_Patient_DOB_YY, "2000")
            oHCFA1500.CF_3_Patient_Sex_Female.Value = True
            oHCFA1500.CF_3_Patient_Sex_Male.Value = True
            oHCFA1500.CF_4_Insureds_Name.Value = formatedData(oHCFA1500.CF_4_Insureds_Name, "Insured Name")

            oHCFA1500.CF_5_Patient_Address.Value = formatedData(oHCFA1500.CF_5_Patient_Address, "Patient Address")
            oHCFA1500.CF_5_Patient_City.Value = formatedData(oHCFA1500.CF_5_Patient_City, "City")
            oHCFA1500.CF_5_Patient_State.Value = formatedData(oHCFA1500.CF_5_Patient_State, "ST")
            oHCFA1500.CF_5_Patient_Tel_AreaCode.Value = formatedData(oHCFA1500.CF_5_Patient_Tel_AreaCode, "123")
            oHCFA1500.CF_5_Patient_Tel_Number.Value = formatedData(oHCFA1500.CF_5_Patient_Tel_Number, "123456789")
            oHCFA1500.CF_5_Patient_Zip.Value = formatedData(oHCFA1500.CF_5_Patient_Zip, "123456")

            'Commented on 17/10/2013 Because it is removed from new CMS 1500 format and used as a RESEVED FOR NUCC USE
            'oHCFA1500.CF_9_Other_Insureds_DOB_DD.Value = "10"
            'oHCFA1500.CF_9_Other_Insureds_DOB_MM.Value = "10"
            'oHCFA1500.CF_9_Other_Insureds_DOB_YY.Value = "2000"

            'oHCFA1500.CF_9_Other_Insureds_EmployerName.Value = "Emp Name"

            oHCFA1500.CF_9_Other_Insureds_InsuracnePlan.Value = formatedData(oHCFA1500.CF_9_Other_Insureds_InsuracnePlan, "Insurance Plan")
            oHCFA1500.CF_9_Other_Insureds_Name.Value = formatedData(oHCFA1500.CF_9_Other_Insureds_Name, "Insurance Name")
            oHCFA1500.CF_9_Other_Insureds_PolicyGroupNo.Value = formatedData(oHCFA1500.CF_9_Other_Insureds_PolicyGroupNo, "123456")

            oHCFA1500.CF_9_b_Reserved_For_Nucc_Use.Value = formatedData(oHCFA1500.CF_9_b_Reserved_For_Nucc_Use, "Reserved For Nucc Use")

            oHCFA1500.CF_9_c_Reserved_For_Nucc_Use.Value = formatedData(oHCFA1500.CF_9_c_Reserved_For_Nucc_Use, "Reserved For Nucc Use")

            oHCFA1500.CF_10_Claim_Codes_Designated_by_NUCC.Value = formatedData(oHCFA1500.CF_10_Claim_Codes_Designated_by_NUCC, "CLAIM CODES")



            'Commented on 17/10/2013 Because it is removed from new CMS 1500 format and used as a RESEVED FOR NUCC USE
            'Public CF_9_Other_Insureds_EmployerName As FormFieldString
            'End Comment 17/10/2013
            'oHCFA1500.CF_9_Other_Insureds_Sex_Female.Value = True
            'oHCFA1500.CF_9_Other_Insureds_Sex_Male.Value = True

            oHCFA1500.CF_12_PatientAuthorizedPersons_Signature.Value = formatedData(oHCFA1500.CF_12_PatientAuthorizedPersons_Signature, "Signature")
            oHCFA1500.CF_12_PatientAuthorizedPersons_Signature_Date.Value = formatedData(oHCFA1500.CF_12_PatientAuthorizedPersons_Signature_Date, "10/10/2000")
            oHCFA1500.CF_13_InsuredsAuthorizedPersons_Signature.Value = formatedData(oHCFA1500.CF_13_InsuredsAuthorizedPersons_Signature, "Signature")

            oHCFA1500.CF_14_Date_Of_Current_Illness_Injury_or_Pregnancy_LMP_DD.Value = formatedData(oHCFA1500.CF_14_Date_Of_Current_Illness_Injury_or_Pregnancy_LMP_DD, "10")
            oHCFA1500.CF_14_Date_Of_Current_Illness_Injury_or_Pregnancy_LMP_MM.Value = formatedData(oHCFA1500.CF_14_Date_Of_Current_Illness_Injury_or_Pregnancy_LMP_MM, "10")
            oHCFA1500.CF_14_Date_Of_Current_Illness_Injury_or_Pregnancy_LMP_YY.Value = formatedData(oHCFA1500.CF_14_Date_Of_Current_Illness_Injury_or_Pregnancy_LMP_YY, "2000")
            'CF_14_Qualifier
            oHCFA1500.CF_14_Qualifier_No.Value = formatedData(oHCFA1500.CF_14_Qualifier_No, "14Q")
            'oHCFA1500.CF_14_Qualifier.Value = "Qualifier"
            '18/10/2013 Fied Names changed 
            oHCFA1500.CF_15_Other_Date_DD.Value = formatedData(oHCFA1500.CF_15_Other_Date_DD, "10")
            oHCFA1500.CF_15_Other_Dates_MM.Value = formatedData(oHCFA1500.CF_15_Other_Dates_MM, "10")
            oHCFA1500.CF_15_Other_Date_YY.Value = formatedData(oHCFA1500.CF_15_Other_Date_YY, "2000")

            oHCFA1500.CF_15_Qualifier_No.Value = formatedData(oHCFA1500.CF_15_Qualifier_No, "15Q")
            'oHCFA1500.CF_15_Qualifier.Value = "Qualifier"


            oHCFA1500.CF_16_UnableToWorkFromDate_DD.Value = formatedData(oHCFA1500.CF_16_UnableToWorkFromDate_DD, "10")
            oHCFA1500.CF_16_UnableToWorkFromDate_MM.Value = formatedData(oHCFA1500.CF_16_UnableToWorkFromDate_MM, "10")
            oHCFA1500.CF_16_UnableToWorkFromDate_YY.Value = formatedData(oHCFA1500.CF_16_UnableToWorkFromDate_YY, "2000")
            oHCFA1500.CF_16_UnableToWorkTillDate_DD.Value = formatedData(oHCFA1500.CF_16_UnableToWorkTillDate_DD, "10")
            oHCFA1500.CF_16_UnableToWorkTillDate_MM.Value = formatedData(oHCFA1500.CF_16_UnableToWorkTillDate_MM, "10")
            oHCFA1500.CF_16_UnableToWorkTillDate_YY.Value = formatedData(oHCFA1500.CF_16_UnableToWorkTillDate_YY, "2000")



            oHCFA1500.CF_17_ReferringProvider_Name.Value = formatedData(oHCFA1500.CF_17_ReferringProvider_Name, " Ref Provider Name ")
            'oHCFA1500.CF_17a_ReferringProvider_UnknownField.Value = "Unknown"
            oHCFA1500.CF_17b_ReferringProvider_NPI.Value = formatedData(oHCFA1500.CF_17b_ReferringProvider_NPI, "Ref Provider NPI")

            'CF_17_Qualifier_No
            oHCFA1500.CF_17_Qualifier_No.Value = formatedData(oHCFA1500.CF_17_Qualifier_No, "17Q")
            ' oHCFA1500.CF_17_Qualifier.Value = "Qualifier"




            oHCFA1500.CF_18_HospitalizationFromDate_DD.Value = formatedData(oHCFA1500.CF_18_HospitalizationFromDate_DD, "10")
            oHCFA1500.CF_18_HospitalizationFromDate_MM.Value = formatedData(oHCFA1500.CF_18_HospitalizationFromDate_MM, "10")
            oHCFA1500.CF_18_HospitalizationFromDate_YY.Value = formatedData(oHCFA1500.CF_18_HospitalizationFromDate_YY, "2000")

            oHCFA1500.CF_18_HospitalizationTillDate_DD.Value = formatedData(oHCFA1500.CF_18_HospitalizationTillDate_DD, "10")
            oHCFA1500.CF_18_HospitalizationTillDate_MM.Value = formatedData(oHCFA1500.CF_18_HospitalizationTillDate_MM, "10")
            oHCFA1500.CF_18_HospitalizationTillDate_YY.Value = formatedData(oHCFA1500.CF_18_HospitalizationTillDate_YY, "2000")


            oHCFA1500.CF_19_Additional_Claim_Information_Designated_by_NUCC.Value = formatedData(oHCFA1500.CF_19_Additional_Claim_Information_Designated_by_NUCC, "Additional Claim Info ")
            oHCFA1500.CF_20_OutsideLab_Charges_Principal.Value = formatedData(oHCFA1500.CF_20_OutsideLab_Charges_Principal, "Charges")
            oHCFA1500.CF_20_OutsideLab_Charges_Secondary.Value = formatedData(oHCFA1500.CF_20_OutsideLab_Charges_Secondary, "S Charges")
            oHCFA1500.CF_20_OutsideLab_No.Value = True
            oHCFA1500.CF_20_OutsideLab_Yes.Value = True

            'CF_21_Icd_Ind_No
            oHCFA1500.CF_21_Icd_Ind_No.Value = formatedData(oHCFA1500.CF_21_Icd_Ind_No, "I1")
            oHCFA1500.CF_21_Icd_Ind.Value = formatedData(oHCFA1500.CF_21_Icd_Ind, "I2")

            oHCFA1500.CF_21_Diagnosis_A_Principal.Value = formatedData(oHCFA1500.CF_21_Diagnosis_A_Principal, "PD-A")
            oHCFA1500.CF_21_Diagnosis_1_Secondary.Value = formatedData(oHCFA1500.CF_21_Diagnosis_1_Secondary, "SD")
            oHCFA1500.CF_21_Diagnosis_B_Principal.Value = formatedData(oHCFA1500.CF_21_Diagnosis_B_Principal, "PD-B")
            oHCFA1500.CF_21_Diagnosis_2_Secondary.Value = formatedData(oHCFA1500.CF_21_Diagnosis_2_Secondary, "SD")
            oHCFA1500.CF_21_Diagnosis_C_Principal.Value = formatedData(oHCFA1500.CF_21_Diagnosis_C_Principal, "PD-C")
            oHCFA1500.CF_21_Diagnosis_3_Secondary.Value = formatedData(oHCFA1500.CF_21_Diagnosis_3_Secondary, "SD")

            oHCFA1500.CF_21_Diagnosis_D_Principal.Value = formatedData(oHCFA1500.CF_21_Diagnosis_D_Principal, "PD-D")

            oHCFA1500.CF_21_Diagnosis_E_Principal.Value = formatedData(oHCFA1500.CF_21_Diagnosis_E_Principal, "PD-E")
            oHCFA1500.CF_21_Diagnosis_F_Principal.Value = formatedData(oHCFA1500.CF_21_Diagnosis_F_Principal, "PD-F")
            oHCFA1500.CF_21_Diagnosis_G_Principal.Value = formatedData(oHCFA1500.CF_21_Diagnosis_G_Principal, "PD-G")
            oHCFA1500.CF_21_Diagnosis_H_Principal.Value = formatedData(oHCFA1500.CF_21_Diagnosis_H_Principal, "PD-H")
            oHCFA1500.CF_21_Diagnosis_I_Principal.Value = formatedData(oHCFA1500.CF_21_Diagnosis_I_Principal, "PD-I")
            oHCFA1500.CF_21_Diagnosis_J_Principal.Value = formatedData(oHCFA1500.CF_21_Diagnosis_J_Principal, "PD-J")
            oHCFA1500.CF_21_Diagnosis_K_Principal.Value = formatedData(oHCFA1500.CF_21_Diagnosis_K_Principal, "PD-K")
            oHCFA1500.CF_21_Diagnosis_L_Principal.Value = formatedData(oHCFA1500.CF_21_Diagnosis_L_Principal, "PD-L")





            oHCFA1500.CF_21_Diagnosis_4_Secondary.Value = formatedData(oHCFA1500.CF_21_Diagnosis_4_Secondary, "SD")

            oHCFA1500.CF_22_Resubmission.Value = formatedData(oHCFA1500.CF_22_Resubmission, "code")
            oHCFA1500.CF_22_Original_Refrence_No.Value = formatedData(oHCFA1500.CF_22_Original_Refrence_No, "12346")
            oHCFA1500.CF_23_PriorAuthorization_No.Value = formatedData(oHCFA1500.CF_23_PriorAuthorization_No, "123456")

            oHCFA1500.CF_24A_L1_Note.Value = formatedData(oHCFA1500.CF_24A_L1_Note, "Note 1")
            oHCFA1500.CF_24A_L2_Note.Value = formatedData(oHCFA1500.CF_24A_L2_Note, "Note 2")
            oHCFA1500.CF_24A_L3_Note.Value = formatedData(oHCFA1500.CF_24A_L3_Note, "Note 3")
            oHCFA1500.CF_24A_L4_Note.Value = formatedData(oHCFA1500.CF_24A_L4_Note, "Note 4")
            oHCFA1500.CF_24A_L5_Note.Value = formatedData(oHCFA1500.CF_24A_L5_Note, "Note 5")
            oHCFA1500.CF_24A_L6_Note.Value = formatedData(oHCFA1500.CF_24A_L6_Note, "Note 6")

            oHCFA1500.CF_24A_L1_DOS_From_DD.Value = formatedData(oHCFA1500.CF_24A_L1_DOS_From_DD, "10")
            oHCFA1500.CF_24A_L1_DOS_From_MM.Value = formatedData(oHCFA1500.CF_24A_L1_DOS_From_MM, "10")
            oHCFA1500.CF_24A_L1_DOS_From_YY.Value = formatedData(oHCFA1500.CF_24A_L1_DOS_From_YY, "2000")
            oHCFA1500.CF_24A_L1_DOS_To_DD.Value = formatedData(oHCFA1500.CF_24A_L1_DOS_To_DD, "10")
            oHCFA1500.CF_24A_L1_DOS_To_MM.Value = formatedData(oHCFA1500.CF_24A_L1_DOS_To_MM, "10")
            oHCFA1500.CF_24A_L1_DOS_To_YY.Value = formatedData(oHCFA1500.CF_24A_L1_DOS_To_YY, "2000")
            oHCFA1500.CF_24A_L2_DOS_From_DD.Value = formatedData(oHCFA1500.CF_24A_L2_DOS_From_DD, "10")
            oHCFA1500.CF_24A_L2_DOS_From_MM.Value = formatedData(oHCFA1500.CF_24A_L2_DOS_From_MM, "10")
            oHCFA1500.CF_24A_L2_DOS_From_YY.Value = formatedData(oHCFA1500.CF_24A_L2_DOS_From_YY, "2000")
            oHCFA1500.CF_24A_L2_DOS_To_DD.Value = formatedData(oHCFA1500.CF_24A_L2_DOS_To_DD, "10")
            oHCFA1500.CF_24A_L2_DOS_To_MM.Value = formatedData(oHCFA1500.CF_24A_L2_DOS_To_MM, "10")
            oHCFA1500.CF_24A_L2_DOS_To_YY.Value = formatedData(oHCFA1500.CF_24A_L2_DOS_To_YY, "2000")
            oHCFA1500.CF_24A_L3_DOS_From_DD.Value = formatedData(oHCFA1500.CF_24A_L3_DOS_From_DD, "10")
            oHCFA1500.CF_24A_L3_DOS_From_MM.Value = formatedData(oHCFA1500.CF_24A_L3_DOS_From_MM, "10")
            oHCFA1500.CF_24A_L3_DOS_From_YY.Value = formatedData(oHCFA1500.CF_24A_L3_DOS_From_YY, "2000")
            oHCFA1500.CF_24A_L3_DOS_To_DD.Value = formatedData(oHCFA1500.CF_24A_L3_DOS_To_DD, "10")
            oHCFA1500.CF_24A_L3_DOS_To_MM.Value = formatedData(oHCFA1500.CF_24A_L3_DOS_To_MM, "10")
            oHCFA1500.CF_24A_L3_DOS_To_YY.Value = formatedData(oHCFA1500.CF_24A_L3_DOS_To_YY, "2000")
            oHCFA1500.CF_24A_L4_DOS_From_DD.Value = formatedData(oHCFA1500.CF_24A_L4_DOS_From_DD, "10")
            oHCFA1500.CF_24A_L4_DOS_From_MM.Value = formatedData(oHCFA1500.CF_24A_L4_DOS_From_MM, "10")
            oHCFA1500.CF_24A_L4_DOS_From_YY.Value = formatedData(oHCFA1500.CF_24A_L4_DOS_From_YY, "2000")
            oHCFA1500.CF_24A_L4_DOS_To_DD.Value = formatedData(oHCFA1500.CF_24A_L4_DOS_To_DD, "10")
            oHCFA1500.CF_24A_L4_DOS_To_MM.Value = formatedData(oHCFA1500.CF_24A_L4_DOS_To_MM, "10")
            oHCFA1500.CF_24A_L4_DOS_To_YY.Value = formatedData(oHCFA1500.CF_24A_L4_DOS_To_YY, "2000")
            oHCFA1500.CF_24A_L5_DOS_From_DD.Value = formatedData(oHCFA1500.CF_24A_L5_DOS_From_DD, "10")
            oHCFA1500.CF_24A_L5_DOS_From_MM.Value = formatedData(oHCFA1500.CF_24A_L5_DOS_From_MM, "10")
            oHCFA1500.CF_24A_L5_DOS_From_YY.Value = formatedData(oHCFA1500.CF_24A_L5_DOS_From_YY, "2000")
            oHCFA1500.CF_24A_L5_DOS_To_DD.Value = formatedData(oHCFA1500.CF_24A_L5_DOS_To_DD, "10")
            oHCFA1500.CF_24A_L5_DOS_To_MM.Value = formatedData(oHCFA1500.CF_24A_L5_DOS_To_MM, "10")
            oHCFA1500.CF_24A_L5_DOS_To_YY.Value = formatedData(oHCFA1500.CF_24A_L5_DOS_To_YY, "2000")
            oHCFA1500.CF_24A_L6_DOS_From_DD.Value = formatedData(oHCFA1500.CF_24A_L6_DOS_From_DD, "10")
            oHCFA1500.CF_24A_L6_DOS_From_MM.Value = formatedData(oHCFA1500.CF_24A_L6_DOS_From_MM, "10")
            oHCFA1500.CF_24A_L6_DOS_From_YY.Value = formatedData(oHCFA1500.CF_24A_L6_DOS_From_YY, "2000")
            oHCFA1500.CF_24A_L6_DOS_To_DD.Value = formatedData(oHCFA1500.CF_24A_L6_DOS_To_DD, "10")
            oHCFA1500.CF_24A_L6_DOS_To_MM.Value = formatedData(oHCFA1500.CF_24A_L6_DOS_To_MM, "10")
            oHCFA1500.CF_24A_L6_DOS_To_YY.Value = formatedData(oHCFA1500.CF_24A_L6_DOS_To_YY, "2000")


            oHCFA1500.CF_24B_L1_POS_Code.Value = formatedData(oHCFA1500.CF_24B_L1_POS_Code, "123")
            oHCFA1500.CF_24B_L2_POS_Code.Value = formatedData(oHCFA1500.CF_24B_L2_POS_Code, "123")
            oHCFA1500.CF_24B_L3_POS_Code.Value = formatedData(oHCFA1500.CF_24B_L3_POS_Code, "123")
            oHCFA1500.CF_24B_L4_POS_Code.Value = formatedData(oHCFA1500.CF_24B_L4_POS_Code, "123")
            oHCFA1500.CF_24B_L5_POS_Code.Value = formatedData(oHCFA1500.CF_24B_L5_POS_Code, "123")
            oHCFA1500.CF_24B_L6_POS_Code.Value = formatedData(oHCFA1500.CF_24B_L6_POS_Code, "123")


            oHCFA1500.CF_24C_L1_EMG_Code.Value = formatedData(oHCFA1500.CF_24C_L1_EMG_Code, "123")
            oHCFA1500.CF_24C_L2_EMG_Code.Value = formatedData(oHCFA1500.CF_24C_L2_EMG_Code, "123")
            oHCFA1500.CF_24C_L3_EMG_Code.Value = formatedData(oHCFA1500.CF_24C_L3_EMG_Code, "123")
            oHCFA1500.CF_24C_L4_EMG_Code.Value = formatedData(oHCFA1500.CF_24C_L4_EMG_Code, "123")
            oHCFA1500.CF_24C_L5_EMG_Code.Value = formatedData(oHCFA1500.CF_24C_L5_EMG_Code, "123")
            oHCFA1500.CF_24C_L6_EMG_Code.Value = formatedData(oHCFA1500.CF_24C_L6_EMG_Code, "123")


            oHCFA1500.CF_24D_L1_CPT_HCPCS_Code.Value = formatedData(oHCFA1500.CF_24D_L1_CPT_HCPCS_Code, "123")
            oHCFA1500.CF_24D_L2_CPT_HCPCS_Code.Value = formatedData(oHCFA1500.CF_24D_L2_CPT_HCPCS_Code, "123")
            oHCFA1500.CF_24D_L3_CPT_HCPCS_Code.Value = formatedData(oHCFA1500.CF_24D_L3_CPT_HCPCS_Code, "123")
            oHCFA1500.CF_24D_L4_CPT_HCPCS_Code.Value = formatedData(oHCFA1500.CF_24D_L4_CPT_HCPCS_Code, "123")
            oHCFA1500.CF_24D_L5_CPT_HCPCS_Code.Value = formatedData(oHCFA1500.CF_24D_L5_CPT_HCPCS_Code, "123")
            oHCFA1500.CF_24D_L6_CPT_HCPCS_Code.Value = formatedData(oHCFA1500.CF_24D_L6_CPT_HCPCS_Code, "123")

            oHCFA1500.CF_IsPresent_Line1 = True
            oHCFA1500.CF_IsPresent_Line2 = True
            oHCFA1500.CF_IsPresent_Line3 = True
            oHCFA1500.CF_IsPresent_Line4 = True
            oHCFA1500.CF_IsPresent_Line5 = True
            oHCFA1500.CF_IsPresent_Line6 = True

            oHCFA1500.CF_24D_L1_Modifier_1_Code.Value = formatedData(oHCFA1500.CF_24D_L1_Modifier_1_Code, "123")
            oHCFA1500.CF_24D_L1_Modifier_2_Code.Value = formatedData(oHCFA1500.CF_24D_L1_Modifier_2_Code, "123")
            oHCFA1500.CF_24D_L1_Modifier_3_Code.Value = formatedData(oHCFA1500.CF_24D_L1_Modifier_3_Code, "123")
            oHCFA1500.CF_24D_L1_Modifier_4_Code.Value = formatedData(oHCFA1500.CF_24D_L1_Modifier_4_Code, "123")
            oHCFA1500.CF_24D_L2_Modifier_1_Code.Value = formatedData(oHCFA1500.CF_24D_L2_Modifier_1_Code, "123")
            oHCFA1500.CF_24D_L2_Modifier_2_Code.Value = formatedData(oHCFA1500.CF_24D_L2_Modifier_2_Code, "123")
            oHCFA1500.CF_24D_L2_Modifier_3_Code.Value = formatedData(oHCFA1500.CF_24D_L2_Modifier_3_Code, "123")
            oHCFA1500.CF_24D_L2_Modifier_4_Code.Value = formatedData(oHCFA1500.CF_24D_L2_Modifier_4_Code, "123")
            oHCFA1500.CF_24D_L3_Modifier_1_Code.Value = formatedData(oHCFA1500.CF_24D_L3_Modifier_1_Code, "123")
            oHCFA1500.CF_24D_L3_Modifier_2_Code.Value = formatedData(oHCFA1500.CF_24D_L3_Modifier_2_Code, "123")
            oHCFA1500.CF_24D_L3_Modifier_3_Code.Value = formatedData(oHCFA1500.CF_24D_L3_Modifier_3_Code, "123")
            oHCFA1500.CF_24D_L3_Modifier_4_Code.Value = formatedData(oHCFA1500.CF_24D_L3_Modifier_4_Code, "123")
            oHCFA1500.CF_24D_L4_Modifier_1_Code.Value = formatedData(oHCFA1500.CF_24D_L4_Modifier_1_Code, "123")
            oHCFA1500.CF_24D_L4_Modifier_2_Code.Value = formatedData(oHCFA1500.CF_24D_L4_Modifier_2_Code, "123")
            oHCFA1500.CF_24D_L4_Modifier_3_Code.Value = formatedData(oHCFA1500.CF_24D_L4_Modifier_3_Code, "123")
            oHCFA1500.CF_24D_L4_Modifier_4_Code.Value = formatedData(oHCFA1500.CF_24D_L4_Modifier_4_Code, "123")
            oHCFA1500.CF_24D_L5_Modifier_1_Code.Value = formatedData(oHCFA1500.CF_24D_L5_Modifier_1_Code, "123")
            oHCFA1500.CF_24D_L5_Modifier_2_Code.Value = formatedData(oHCFA1500.CF_24D_L5_Modifier_2_Code, "123")
            oHCFA1500.CF_24D_L5_Modifier_3_Code.Value = formatedData(oHCFA1500.CF_24D_L5_Modifier_3_Code, "123")
            oHCFA1500.CF_24D_L5_Modifier_4_Code.Value = formatedData(oHCFA1500.CF_24D_L5_Modifier_4_Code, "123")
            oHCFA1500.CF_24D_L6_Modifier_1_Code.Value = formatedData(oHCFA1500.CF_24D_L6_Modifier_1_Code, "123")
            oHCFA1500.CF_24D_L6_Modifier_2_Code.Value = formatedData(oHCFA1500.CF_24D_L6_Modifier_2_Code, "123")
            oHCFA1500.CF_24D_L6_Modifier_3_Code.Value = formatedData(oHCFA1500.CF_24D_L6_Modifier_3_Code, "123")
            oHCFA1500.CF_24D_L6_Modifier_4_Code.Value = formatedData(oHCFA1500.CF_24D_L6_Modifier_4_Code, "123")

            oHCFA1500.CF_24E_L1_Diagnosis_Pointers.Value = formatedData(oHCFA1500.CF_24E_L1_Diagnosis_Pointers, "ABCD")
            oHCFA1500.CF_24E_L2_Diagnosis_Pointers.Value = formatedData(oHCFA1500.CF_24E_L2_Diagnosis_Pointers, "ABCD")
            oHCFA1500.CF_24E_L3_Diagnosis_Pointers.Value = formatedData(oHCFA1500.CF_24E_L3_Diagnosis_Pointers, "ABCD")
            oHCFA1500.CF_24E_L4_Diagnosis_Pointers.Value = formatedData(oHCFA1500.CF_24E_L4_Diagnosis_Pointers, "ABCD")
            oHCFA1500.CF_24E_L5_Diagnosis_Pointers.Value = formatedData(oHCFA1500.CF_24E_L5_Diagnosis_Pointers, "ABCD")
            oHCFA1500.CF_24E_L6_Diagnosis_Pointers.Value = formatedData(oHCFA1500.CF_24E_L6_Diagnosis_Pointers, "ABCD")


            oHCFA1500.CF_24F_L1_Charges_Principal.Value = formatedData(oHCFA1500.CF_24F_L1_Charges_Principal, "123")
            oHCFA1500.CF_24F_L1_Charges_Secondary.Value = formatedData(oHCFA1500.CF_24F_L1_Charges_Secondary, "123")
            oHCFA1500.CF_24F_L2_Charges_Principal.Value = formatedData(oHCFA1500.CF_24F_L2_Charges_Principal, "123")
            oHCFA1500.CF_24F_L2_Charges_Secondary.Value = formatedData(oHCFA1500.CF_24F_L2_Charges_Secondary, "123")
            oHCFA1500.CF_24F_L3_Charges_Principal.Value = formatedData(oHCFA1500.CF_24F_L3_Charges_Principal, "123")
            oHCFA1500.CF_24F_L3_Charges_Secondary.Value = formatedData(oHCFA1500.CF_24F_L3_Charges_Secondary, "123")
            oHCFA1500.CF_24F_L4_Charges_Principal.Value = formatedData(oHCFA1500.CF_24F_L4_Charges_Principal, "123")
            oHCFA1500.CF_24F_L4_Charges_Secondary.Value = formatedData(oHCFA1500.CF_24F_L4_Charges_Secondary, "123")
            oHCFA1500.CF_24F_L5_Charges_Principal.Value = formatedData(oHCFA1500.CF_24F_L5_Charges_Principal, "123")
            oHCFA1500.CF_24F_L5_Charges_Secondary.Value = formatedData(oHCFA1500.CF_24F_L5_Charges_Secondary, "123")
            oHCFA1500.CF_24F_L6_Charges_Principal.Value = formatedData(oHCFA1500.CF_24F_L6_Charges_Principal, "123")
            oHCFA1500.CF_24F_L6_Charges_Secondary.Value = formatedData(oHCFA1500.CF_24F_L6_Charges_Secondary, "123")

            oHCFA1500.CF_24G_L1_Days_Units.Value = formatedData(oHCFA1500.CF_24G_L1_Days_Units, "1")
            oHCFA1500.CF_24G_L2_Days_Units.Value = formatedData(oHCFA1500.CF_24G_L2_Days_Units, "1")
            oHCFA1500.CF_24G_L3_Days_Units.Value = formatedData(oHCFA1500.CF_24G_L3_Days_Units, "1")
            oHCFA1500.CF_24G_L4_Days_Units.Value = formatedData(oHCFA1500.CF_24G_L4_Days_Units, "1")
            oHCFA1500.CF_24G_L5_Days_Units.Value = formatedData(oHCFA1500.CF_24G_L5_Days_Units, "1")
            oHCFA1500.CF_24G_L6_Days_Units.Value = formatedData(oHCFA1500.CF_24G_L6_Days_Units, "1")

            oHCFA1500.CF_24H_L1_EPSDT_FamilyPlan.Value = formatedData(oHCFA1500.CF_24H_L1_EPSDT_FamilyPlan, "a")
            oHCFA1500.CF_24H_L2_EPSDT_FamilyPlan.Value = formatedData(oHCFA1500.CF_24H_L2_EPSDT_FamilyPlan, "a")
            oHCFA1500.CF_24H_L3_EPSDT_FamilyPlan.Value = formatedData(oHCFA1500.CF_24H_L3_EPSDT_FamilyPlan, "a")
            oHCFA1500.CF_24H_L4_EPSDT_FamilyPlan.Value = formatedData(oHCFA1500.CF_24H_L4_EPSDT_FamilyPlan, "a")
            oHCFA1500.CF_24H_L5_EPSDT_FamilyPlan.Value = formatedData(oHCFA1500.CF_24H_L5_EPSDT_FamilyPlan, "a")
            oHCFA1500.CF_24H_L6_EPSDT_FamilyPlan.Value = formatedData(oHCFA1500.CF_24H_L6_EPSDT_FamilyPlan, "a")

            oHCFA1500.CF_24H_L1_EPSDT_FamilyPlan_Shaded.Value = formatedData(oHCFA1500.CF_24H_L1_EPSDT_FamilyPlan_Shaded, "h1")
            oHCFA1500.CF_24H_L2_EPSDT_FamilyPlan_Shaded.Value = formatedData(oHCFA1500.CF_24H_L2_EPSDT_FamilyPlan_Shaded, "h2")
            oHCFA1500.CF_24H_L3_EPSDT_FamilyPlan_Shaded.Value = formatedData(oHCFA1500.CF_24H_L3_EPSDT_FamilyPlan_Shaded, "h3")
            oHCFA1500.CF_24H_L4_EPSDT_FamilyPlan_Shaded.Value = formatedData(oHCFA1500.CF_24H_L4_EPSDT_FamilyPlan_Shaded, "h4")
            oHCFA1500.CF_24H_L5_EPSDT_FamilyPlan_Shaded.Value = formatedData(oHCFA1500.CF_24H_L5_EPSDT_FamilyPlan_Shaded, "h5")
            oHCFA1500.CF_24H_L6_EPSDT_FamilyPlan_Shaded.Value = formatedData(oHCFA1500.CF_24H_L6_EPSDT_FamilyPlan_Shaded, "h6")

            oHCFA1500.CF_24J_L1_RenderingProvider_NPI.Value = formatedData(oHCFA1500.CF_24J_L1_RenderingProvider_NPI, "123")
            oHCFA1500.CF_24J_L2_RenderingProvider_NPI.Value = formatedData(oHCFA1500.CF_24J_L2_RenderingProvider_NPI, "123")
            oHCFA1500.CF_24J_L3_RenderingProvider_NPI.Value = formatedData(oHCFA1500.CF_24J_L3_RenderingProvider_NPI, "123")
            oHCFA1500.CF_24J_L4_RenderingProvider_NPI.Value = formatedData(oHCFA1500.CF_24J_L4_RenderingProvider_NPI, "123")
            oHCFA1500.CF_24J_L5_RenderingProvider_NPI.Value = formatedData(oHCFA1500.CF_24J_L5_RenderingProvider_NPI, "123")
            oHCFA1500.CF_24J_L6_RenderingProvider_NPI.Value = formatedData(oHCFA1500.CF_24J_L6_RenderingProvider_NPI, "123")

            oHCFA1500.CF_24J_L1_RenderingProvider_OthrQualifier.Value = formatedData(oHCFA1500.CF_24J_L1_RenderingProvider_OthrQualifier, "AA")
            oHCFA1500.CF_24J_L2_RenderingProvider_OthrQualifier.Value = formatedData(oHCFA1500.CF_24J_L2_RenderingProvider_OthrQualifier, "BB")
            oHCFA1500.CF_24J_L3_RenderingProvider_OthrQualifier.Value = formatedData(oHCFA1500.CF_24J_L3_RenderingProvider_OthrQualifier, "CC")
            oHCFA1500.CF_24J_L4_RenderingProvider_OthrQualifier.Value = formatedData(oHCFA1500.CF_24J_L4_RenderingProvider_OthrQualifier, "DD")
            oHCFA1500.CF_24J_L5_RenderingProvider_OthrQualifier.Value = formatedData(oHCFA1500.CF_24J_L5_RenderingProvider_OthrQualifier, "EE")
            oHCFA1500.CF_24J_L6_RenderingProvider_OthrQualifier.Value = formatedData(oHCFA1500.CF_24J_L6_RenderingProvider_OthrQualifier, "FF")

            oHCFA1500.CF_24J_L1_RenderingProvider_OthrQualifierValue.Value = formatedData(oHCFA1500.CF_24J_L1_RenderingProvider_OthrQualifierValue, "123222")
            oHCFA1500.CF_24J_L2_RenderingProvider_OthrQualifierValue.Value = formatedData(oHCFA1500.CF_24J_L2_RenderingProvider_OthrQualifierValue, "1233333")
            oHCFA1500.CF_24J_L3_RenderingProvider_OthrQualifierValue.Value = formatedData(oHCFA1500.CF_24J_L3_RenderingProvider_OthrQualifierValue, "123444")
            oHCFA1500.CF_24J_L4_RenderingProvider_OthrQualifierValue.Value = formatedData(oHCFA1500.CF_24J_L4_RenderingProvider_OthrQualifierValue, "123555")
            oHCFA1500.CF_24J_L5_RenderingProvider_OthrQualifierValue.Value = formatedData(oHCFA1500.CF_24J_L5_RenderingProvider_OthrQualifierValue, "123666")
            oHCFA1500.CF_24J_L6_RenderingProvider_OthrQualifierValue.Value = formatedData(oHCFA1500.CF_24J_L6_RenderingProvider_OthrQualifierValue, "123777")

            oHCFA1500.CF_25_FederalTax_ID_No.Value = formatedData(oHCFA1500.CF_25_FederalTax_ID_No, "123")
            oHCFA1500.CF_25_FederalTaxID_Qualifier_EIN.Value = True
            oHCFA1500.CF_25_FederalTaxID_Qualifier_SSN.Value = True
            oHCFA1500.CF_26_PatientAccount_No.Value = formatedData(oHCFA1500.CF_26_PatientAccount_No, "123")
            oHCFA1500.CF_27_AcceptAssignment_NO.Value = True
            oHCFA1500.CF_27_AcceptAssignment_YES.Value = True

            oHCFA1500.CF_28_TotalCharge_Principal.Value = formatedData(oHCFA1500.CF_28_TotalCharge_Principal, "123")
            oHCFA1500.CF_28_TotalCharge_Secondary.Value = formatedData(oHCFA1500.CF_28_TotalCharge_Secondary, "123")
            oHCFA1500.CF_29_AmountPaid_Principal.Value = formatedData(oHCFA1500.CF_29_AmountPaid_Principal, "123")
            oHCFA1500.CF_29_AmountPaid_Secondary.Value = formatedData(oHCFA1500.CF_29_AmountPaid_Secondary, "123")
            oHCFA1500.CF_30_BalanceDue_Principal.Value = formatedData(oHCFA1500.CF_30_BalanceDue_Principal, "Rsrv")
            oHCFA1500.CF_30_Rsvd_for_NUCC_Use.Value = formatedData(oHCFA1500.CF_30_Rsvd_for_NUCC_Use, "Rsrv")

            oHCFA1500.CF_31_Physician_Supplier_Signature.Value = formatedData(oHCFA1500.CF_31_Physician_Supplier_Signature, "Sign")
            oHCFA1500.CF_31_Physician_Supplier_Signature_Date.Value = formatedData(oHCFA1500.CF_31_Physician_Supplier_Signature_Date, "10/10/2000")
            ' oHCFA1500.CF_31_Physician_Supplier_QualifierValue.Value = "123456789"

            oHCFA1500.CF_32_Service_Facility_Name.Value = formatedData(oHCFA1500.CF_32_Service_Facility_Name, "Name")
            oHCFA1500.CF_32_Service_Facility_Address_Line1.Value = formatedData(oHCFA1500.CF_32_Service_Facility_Address_Line1, "Address 1")
            oHCFA1500.CF_32_Service_Facility_Address_Line2.Value = formatedData(oHCFA1500.CF_32_Service_Facility_Address_Line2, "Address 2")

            oHCFA1500.CF_32a_Service_Facility_NPI.Value = formatedData(oHCFA1500.CF_32a_Service_Facility_NPI, "12356")
            oHCFA1500.CF_32b_Service_Facility_UPIN_OtherID.Value = formatedData(oHCFA1500.CF_32b_Service_Facility_UPIN_OtherID, "456")


            oHCFA1500.CF_33_BillingProvider_Name.Value = formatedData(oHCFA1500.CF_33_BillingProvider_Name, "Name")
            oHCFA1500.CF_33_BillingProvider_Address_Line1.Value = formatedData(oHCFA1500.CF_33_BillingProvider_Address_Line1, "Address 1")
            oHCFA1500.CF_33_BillingProvider_Address_Line2.Value = formatedData(oHCFA1500.CF_33_BillingProvider_Address_Line2, "Address 2")

            oHCFA1500.CF_33_BillingProvider_Tel_AreaCode.Value = formatedData(oHCFA1500.CF_33_BillingProvider_Tel_AreaCode, "123")
            oHCFA1500.CF_33_BillingProvider_Tel_Number.Value = formatedData(oHCFA1500.CF_33_BillingProvider_Tel_Number, "123456789")
            oHCFA1500.CF_33a_BillingProvider_NPI.Value = formatedData(oHCFA1500.CF_33a_BillingProvider_NPI, "123")


            oHCFA1500.CF_33b_BillingProvider_UPIN_OtherID.Value = formatedData(oHCFA1500.CF_33b_BillingProvider_UPIN_OtherID, "12345")
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)

            oHCFA1500 = Nothing
        Finally
        End Try
        Return oHCFA1500
    End Function

    Private Sub printdoc_HCFA1500_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles printdoc_HCFA1500.PrintPage

        Dim _width As Integer = e.PageSettings.Bounds.Width - 30
        Dim _height As Integer = e.PageSettings.Bounds.Height
        Dim _X As Integer = e.PageSettings.Bounds.X
        Dim _Y As Integer = e.PageSettings.Bounds.Y



        Dim cardHeight As Double = 0
        Dim cardWidth As Double = 0
        'Dim Offset As Int64 = 10
        Dim MyBounds As Rectangle = e.MarginBounds

        Dim MySize As New Size(CInt(e.Graphics.DpiX), CInt(e.Graphics.DpiY))


        Dim MarginRect As New Rectangle(0, 1, 1, 1)

        MarginRect.Y = MarginRect.Y * MySize.Height / 72
        MarginRect.X = MarginRect.X * MySize.Width / 72
        MarginRect.Height = MarginRect.Height * MySize.Height / 72
        MarginRect.Width = MarginRect.Width * MySize.Width / 72

        MyBounds.Y = MyBounds.Y + MarginRect.Top
        MyBounds.X = MyBounds.X + MarginRect.Left
        MyBounds.Height = MyBounds.Height * MySize.Height / 72 - (2 * MarginRect.Height)
        MyBounds.Width = MyBounds.Width * MySize.Width / 72 - (2 * MarginRect.Width)


        Dim y As Int64 = -10
        ' e.PageSettings.Bounds.Y; //MyBounds.Top; 
        Dim x As Int64 = -120
        ' e.PageSettings.Bounds.X; //MyBounds.Left;
        Dim thisImage As System.Drawing.Image = System.Drawing.Image.FromFile(_OutputFilePath)

        Dim units As GraphicsUnit = e.Graphics.PageUnit
        'graphicsunits
        Dim thisImageBound As RectangleF = thisImage.GetBounds(units)
        e.Graphics.PageUnit = units
        'thisImageBound.Height = 2181;


        cardHeight = thisImageBound.Height * MySize.Height / thisImage.VerticalResolution
        cardWidth = thisImageBound.Width * MySize.Width / thisImage.HorizontalResolution


        Dim ScaleX As Double = MyBounds.Width / cardWidth
        Dim ScaleY As Double = MyBounds.Height / cardHeight
        Dim ScaleZ As Double = 1.0
        If ScaleZ < ScaleX Then
            ScaleZ = ScaleX
        End If
        If ScaleZ < ScaleY Then
            ScaleZ = ScaleY
        End If
        cardHeight *= ScaleZ
        cardWidth *= ScaleZ

        e.Graphics.DrawImage(thisImage, x, y, Convert.ToInt64(cardWidth), Convert.ToInt64(cardHeight))

        If thisImage IsNot Nothing Then
            thisImage.Dispose()
            thisImage = Nothing

        End If



    End Sub

    Private Sub ExportSettings()
        Try
            Dim oDialog As New SaveFileDialog()
            oDialog.DefaultExt = ".csv"
            oDialog.Filter = "CSV File|*.csv"
            If oDialog.ShowDialog = Windows.Forms.DialogResult.OK Then

                Dim _FileName As String = oDialog.FileName

                Dim oFile As New FileStream(_FileName, FileMode.Create)
                Dim oWriter As New StreamWriter(oFile)
                Dim _Temp As String
                Dim _Range As C1.Win.C1FlexGrid.CellRange

                For iRow As Integer = 1 To C1Settings.Rows.Count - 1
                    _Range = C1Settings.GetCellRange(iRow, COL_CAPTION, iRow, COL_CharSize)
                    _Temp = _Range.Clip.Trim.Replace("	", ",")
                    oWriter.WriteLine(_Temp)
                Next

                oWriter.Close()
                oFile.Close()
            End If
        Catch ex As IOException
            MessageBox.Show("File cannot be access", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub ImportSettings()
        Try

            Dim oDialog As New OpenFileDialog()
            oDialog.Filter = "CSV File|*.csv"
            If oDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim _FilePath As String = oDialog.FileName

                If File.Exists(_FilePath) Then
                    Dim oFile As New FileStream(_FilePath, FileMode.Open)
                    Dim oReader As New StreamReader(oFile)
                    Dim _Temp As String
                    Dim _SplitString() As String

                    '' CREATE DATA TABLE ''
                    Dim dtSettings As New DataTable
                    Dim oRow As DataRow
                    dtSettings.Columns.Add("Field Name", Type.GetType("System.String"))
                    dtSettings.Columns.Add("Box Name", Type.GetType("System.String"))
                    dtSettings.Columns.Add("X", Type.GetType("System.Int64"))
                    dtSettings.Columns.Add("Y", Type.GetType("System.Int64"))
                    dtSettings.Columns.Add("nPanel")
                    dtSettings.Columns.Add("CharSize")


                    While oReader.EndOfStream = False
                        _Temp = oReader.ReadLine()
                        If _Temp.Split(",").Length = 5 Then
                            _Temp = _Temp + ",30" ''Added 30 for CharSize column of DB.
                        End If
                        _SplitString = _Temp.Split(",")
                        If _SplitString.Length = 6 Then
                            oRow = dtSettings.NewRow
                            oRow("Field Name") = _SplitString(0)
                            oRow("Box Name") = _SplitString(1)
                            oRow("X") = _SplitString(2)
                            oRow("Y") = _SplitString(3)
                            oRow("nPanel") = _SplitString(4)
                            oRow("CharSize") = _SplitString(5)
                            dtSettings.Rows.Add(oRow)
                        Else
                            MessageBox.Show("Invalid file format.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            oReader.Close()
                            oFile.Close()
                            Exit Sub
                        End If
                    End While

                    oReader.Close()
                    oFile.Close()

                    C1Settings.DataSource = dtSettings
                    DesignGrid()
                End If


            End If
        Catch ex As Exception

        End Try
    End Sub

    'Private Function IsCoordinateValidate() As Boolean
    '    For iRow As Integer = 1 To C1Settings.Rows.Count - 1
    '        If (Convert.ToString(C1Settings.GetData(iRow, COL_X)).Trim <> "" And Convert.ToString(C1Settings.GetData(iRow, COL_Y)).Trim <> "") Then
    '            If (C1Settings.GetData(iRow, COL_X) > Int16.MaxValue Or C1Settings.GetData(iRow, COL_Y) > Int16.MaxValue Or C1Settings.GetData(iRow, COL_PANEL) > Int16.MaxValue) Then
    '                MessageBox.Show("Invalid Data", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                If (C1Settings.GetData(iRow, COL_X) > Int16.MaxValue) Then
    '                    C1Settings.Select(iRow, COL_X, True)
    '                    Return False
    '                Else
    '                    C1Settings.Select(iRow, COL_Y, True)
    '                    Return False
    '                End If
    '            End If

    '        Else
    '            MessageBox.Show("Co-ordinate Cannot be blank", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            If (Convert.ToString(C1Settings.GetData(iRow, COL_X)).Trim = "") Then
    '                C1Settings.Select(iRow, COL_X, True)
    '                Return False
    '            Else
    '                C1Settings.Select(iRow, COL_Y, True)
    '                Return False

    '            End If
    '        End If
    '    Next
    '    Return True
    'End Function
#End Region

#Region " Toolstrip Buttons "
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click

        'If (IsCoordinateValidate()) Then
        RowChange()
        If bIsInvalidData = True Then
            Exit Sub
        End If

        isSaveAndClose = True
        SaveCMSSettings()
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()

        'End If

    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        If MessageBox.Show("Are you sure you want to reset co-ordinates?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            bIsReset = True
            ResetSettings()
        End If
    End Sub

    Private Sub tlBtnTestPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlBtnTestPrint.Click
        ' If (IsCoordinateValidate()) Then

        Try
            RowChange()
            If bIsInvalidData = True Then
                Exit Sub
            End If
            SaveCMSSettings()

            'Dim _InputFilePath As String = Application.StartupPath & "\CMS1500_BLANK.TIF"

            Dim oHCFA1500PaperForm As New gloHCFA1500PaperFormNew(_selectedPrinter)
            Dim oPrintForm As New gloPrintPaperFormNew()

            'If System.IO.File.Exists(_InputFilePath) Then

            oHCFA1500PaperForm = SetHCFA1500PaperForm()
            If oHCFA1500PaperForm IsNot Nothing Then
                _OutputFilePath = oPrintForm.PrintHCFA1500Form(oHCFA1500PaperForm)
            End If
            If System.IO.File.Exists(_OutputFilePath) Then

                Dim isPrinted As Boolean = False
                If gloGlobal.gloTSPrint.isCopyPrint Then
                    Dim sPrinterName As String = "Default"
                    If printdoc_HCFA1500.PrinterSettings.DefaultPageSettings.PrinterSettings.PrinterName <> "" Then
                        sPrinterName = printdoc_HCFA1500.PrinterSettings.DefaultPageSettings.PrinterSettings.PrinterName
                    End If
                    Dim oConversion As New clsPrintDocumentConversion(mdlGeneral.GetConnectionString, sPrinterName, "CMS")
                    Dim convertedFile As [String] = printdoc_HCFA1500_ConversionWithLesserResolution(oConversion, True)
                    If Not (gloGlobal.gloTSPrint.UseEMFForClaims) Then
                        Dim tempPDFFilePath As [String] = gloSettings.FolderSettings.AppTempFolderPath + DateTime.Now.ToString("yyyyMMddhhmmsstt") + ".pdf"
                        convertedFile = clsClinicalChartPrinting.ConvertTiffToPDF(convertedFile, tempPDFFilePath, True, True, False)
                    End If
                    isPrinted = gloClinicalQueueFunctions.CopyPrintDoc(convertedFile, "CMS1500", "PrintData")
                    If isPrinted Then
                        _OutputFilePath = convertedFile
                    End If
                End If

                If Not isPrinted Then
                    Dim oprintcontroller As New System.Drawing.Printing.StandardPrintController()
                    Dim opapers As System.Drawing.Printing.PrinterSettings.PaperSizeCollection
                    opapers = printdoc_HCFA1500.PrinterSettings.PaperSizes


                    For i As Integer = 0 To opapers.Count - 1

                        If opapers(i).PaperName.ToString().ToUpper() = "LETTER" Then
                            printdoc_HCFA1500.PrinterSettings.DefaultPageSettings.PaperSize = opapers(i)
                            Exit For
                        End If

                    Next

                    printdoc_HCFA1500.PrinterSettings.DefaultPageSettings.Landscape = False
                    printdoc_HCFA1500.PrintController = oprintcontroller
                    printdoc_HCFA1500.Print()
                End If
            End If
            'Else
            'MessageBox.Show("Source CMS1500 file not present", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

            'End If

            If oHCFA1500PaperForm Is Nothing Then
                oHCFA1500PaperForm.Dispose()
            End If

            If oPrintForm Is Nothing Then
                oPrintForm.Dispose()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try
        'End If
    End Sub

    Private Sub tlbImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbImport.Click
        Try
            ImportSettings()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try
    End Sub

    Private Sub tlbExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbExport.Click
        Try
            ' If (IsCoordinateValidate()) Then
            ExportSettings()
            'End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try
    End Sub

    Private Sub tlbSetAsDefaults_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbSetAsDefaults.Click
        Try
            RowChange()
            If bIsInvalidData = True Then
                Exit Sub
            End If
            'If (IsCoordinateValidate()) Then
            SaveAsDefaultsCMSSettings()
            'End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try
    End Sub


#End Region

#Region " Button Events "
    Private Sub btnRight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        btnRight1.Click, btnRight2.Click, btnRight3.Click, btnRight4.Click, btnRight5.Click, btnRight6.Click, _
        btnRight7.Click, btnRight8.Click, btnRight9.Click, btnRight10.Click, btnRight11.Click, btnRight12.Click

        Try
            Cursor = Cursors.WaitCursor
            If C1Settings.Rows.Count <= 1 Then
                Exit Sub
            End If

            Dim _Temp As Integer = 0
            Dim _Panel As Integer = CType(sender, Button).Tag

            For iRow As Integer = 1 To C1Settings.Rows.Count - 1
                If Val(C1Settings.GetData(iRow, COL_PANEL)) = _Panel Then
                    _Temp = Val(C1Settings.GetData(iRow, COL_X)) + 2
                    C1Settings.SetData(iRow, COL_X, _Temp)
                End If
            Next

            Select Case _Panel
                Case 1
                    lblX1.Text = Val(lblX1.Text) + 1
                Case 2
                    lblX2.Text = Val(lblX2.Text) + 1
                Case 3
                    lblX3.Text = Val(lblX3.Text) + 1
                Case 4
                    lblX4.Text = Val(lblX4.Text) + 1
                Case 5
                    lblX5.Text = Val(lblX5.Text) + 1
                Case 6
                    lblX6.Text = Val(lblX6.Text) + 1
                Case 7
                    lblX7.Text = Val(lblX7.Text) + 1
                Case 8
                    lblX8.Text = Val(lblX8.Text) + 1
                Case 9
                    lblX9.Text = Val(lblX9.Text) + 1
                Case 10
                    lblX10.Text = Val(lblX10.Text) + 1
                Case 11
                    lblX11.Text = Val(lblX11.Text) + 1
                Case 12
                    lblX12.Text = Val(lblX12.Text) + 1
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Cursor = Cursors.Default
    End Sub

    Private Sub btnLeft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        btnLeft1.Click, btnLeft2.Click, btnLeft3.Click, btnLeft4.Click, btnLeft5.Click, btnLeft6.Click, btnLeft7.Click, _
        btnLeft8.Click, btnLeft9.Click, btnLeft10.Click, btnLeft11.Click, btnLeft12.Click

        Try
            Cursor = Cursors.WaitCursor
            If C1Settings.Rows.Count <= 1 Then
                Exit Sub
            End If

            Dim _Temp As Integer = 0
            Dim _Panel As Integer = CType(sender, Button).Tag

            For iRow As Integer = 1 To C1Settings.Rows.Count - 1
                If Val(C1Settings.GetData(iRow, COL_PANEL)) = _Panel Then
                    _Temp = Val(C1Settings.GetData(iRow, COL_X)) - 2
                    C1Settings.SetData(iRow, COL_X, _Temp)
                End If
            Next

            Select Case _Panel
                Case 1
                    lblX1.Text = Val(lblX1.Text) - 1
                Case 2
                    lblX2.Text = Val(lblX2.Text) - 1
                Case 3
                    lblX3.Text = Val(lblX3.Text) - 1
                Case 4
                    lblX4.Text = Val(lblX4.Text) - 1
                Case 5
                    lblX5.Text = Val(lblX5.Text) - 1
                Case 6
                    lblX6.Text = Val(lblX6.Text) - 1
                Case 7
                    lblX7.Text = Val(lblX7.Text) - 1
                Case 8
                    lblX8.Text = Val(lblX8.Text) - 1
                Case 9
                    lblX9.Text = Val(lblX9.Text) - 1
                Case 10
                    lblX10.Text = Val(lblX10.Text) - 1
                Case 11
                    lblX11.Text = Val(lblX11.Text) - 1
                Case 12
                    lblX12.Text = Val(lblX12.Text) - 1
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Cursor = Cursors.Default
    End Sub

    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        btnUp1.Click, btnUp2.Click, btnUp3.Click, btnUp4.Click, btnUp5.Click, btnUp6.Click, btnUp7.Click, _
        btnUp8.Click, btnUp9.Click, btnUp10.Click, btnUp11.Click, btnUp12.Click
        Cursor = Cursors.WaitCursor
        Try
            If C1Settings.Rows.Count <= 1 Then
                Exit Sub
            End If

            Dim _Temp As Integer = 0
            Dim _Panel As Integer = CType(sender, Button).Tag

            For iRow As Integer = 1 To C1Settings.Rows.Count - 1
                If Val(C1Settings.GetData(iRow, COL_PANEL)) = _Panel Then
                    _Temp = Val(C1Settings.GetData(iRow, COL_Y)) - 2
                    C1Settings.SetData(iRow, COL_Y, _Temp)
                End If
            Next

            Select Case _Panel
                Case 1
                    lblY1.Text = Val(lblY1.Text) + 1
                Case 2
                    lblY2.Text = Val(lblY2.Text) + 1
                Case 3
                    lblY3.Text = Val(lblY3.Text) + 1
                Case 4
                    lblY4.Text = Val(lblY4.Text) + 1
                Case 5
                    lblY5.Text = Val(lblY5.Text) + 1
                Case 6
                    lblY6.Text = Val(lblY6.Text) + 1
                Case 7
                    lblY7.Text = Val(lblY7.Text) + 1
                Case 8
                    lblY8.Text = Val(lblY8.Text) + 1
                Case 9
                    lblY9.Text = Val(lblY9.Text) + 1
                Case 10
                    lblY10.Text = Val(lblY10.Text) + 1
                Case 11
                    lblY11.Text = Val(lblY11.Text) + 1
                Case 12
                    lblY12.Text = Val(lblY12.Text) + 1
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Cursor = Cursors.Default
    End Sub

    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        btnDown1.Click, btnDown2.Click, btnDown3.Click, btnDown4.Click, btnDown5.Click, btnDown6.Click, btnDown7.Click, _
        btnDown8.Click, btnDown9.Click, btnDown10.Click, btnDown11.Click, btnDown12.Click
        Try
            Cursor = Cursors.WaitCursor
            If C1Settings.Rows.Count <= 1 Then
                Exit Sub
            End If

            Dim _Temp As Integer = 0
            Dim _Panel As Integer = CType(sender, Button).Tag

            For iRow As Integer = 1 To C1Settings.Rows.Count - 1
                If Val(C1Settings.GetData(iRow, COL_PANEL)) = _Panel Then
                    _Temp = Val(C1Settings.GetData(iRow, COL_Y)) + 2
                    C1Settings.SetData(iRow, COL_Y, _Temp)
                End If
            Next

            Select Case _Panel
                Case 1
                    lblY1.Text = Val(lblY1.Text) - 1
                Case 2
                    lblY2.Text = Val(lblY2.Text) - 1
                Case 3
                    lblY3.Text = Val(lblY3.Text) - 1
                Case 4
                    lblY4.Text = Val(lblY4.Text) - 1
                Case 5
                    lblY5.Text = Val(lblY5.Text) - 1
                Case 6
                    lblY6.Text = Val(lblY6.Text) - 1
                Case 7
                    lblY7.Text = Val(lblY7.Text) - 1
                Case 8
                    lblY8.Text = Val(lblY8.Text) - 1
                Case 9
                    lblY9.Text = Val(lblY9.Text) - 1
                Case 10
                    lblY10.Text = Val(lblY10.Text) - 1
                Case 11
                    lblY11.Text = Val(lblY11.Text) - 1
                Case 12
                    lblY12.Text = Val(lblY12.Text) - 1
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Cursor = Cursors.Default
    End Sub

    Private Sub btnWholeDown_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDown.Click
        Cursor = Cursors.WaitCursor
        Try
            If C1Settings.Rows.Count <= 1 Then
                Exit Sub
            End If

            Dim _Temp As Integer = 0

            For iRow As Integer = 1 To C1Settings.Rows.Count - 1
                _Temp = Val(C1Settings.GetData(iRow, COL_Y)) + 2
                C1Settings.SetData(iRow, COL_Y, _Temp)
            Next

            lblY.Text = Val(lblY.Text) - 1

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Cursor = Cursors.Default
    End Sub

    Private Sub btnWholeUp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUp.Click
        Cursor = Cursors.WaitCursor
        Try
            If C1Settings.Rows.Count <= 1 Then
                Exit Sub
            End If

            Dim _Temp As Integer = 0

            For iRow As Integer = 1 To C1Settings.Rows.Count - 1
                _Temp = Val(C1Settings.GetData(iRow, COL_Y)) - 2
                C1Settings.SetData(iRow, COL_Y, _Temp)
            Next

            lblY.Text = Val(lblY.Text) + 1

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Cursor = Cursors.Default
    End Sub

    Private Sub btnWholeRight_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRight.Click
        Cursor = Cursors.WaitCursor
        Try
            If C1Settings.Rows.Count <= 1 Then
                Exit Sub
            End If

            Dim _Temp As Integer = 0

            For iRow As Integer = 1 To C1Settings.Rows.Count - 1
                _Temp = Val(C1Settings.GetData(iRow, COL_X)) + 2
                C1Settings.SetData(iRow, COL_X, _Temp)
            Next

            lblX.Text = Val(lblX.Text) + 1

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Cursor = Cursors.Default
    End Sub

    Private Sub btnWholeLeft_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLeft.Click
        Cursor = Cursors.WaitCursor
        Try
            If C1Settings.Rows.Count <= 1 Then
                Exit Sub
            End If

            Dim _Temp As Integer = 0

            For iRow As Integer = 1 To C1Settings.Rows.Count - 1
                _Temp = Val(C1Settings.GetData(iRow, COL_X)) - 2
                C1Settings.SetData(iRow, COL_X, _Temp)
            Next

            lblX.Text = Val(lblX.Text) - 1

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Cursor = Cursors.Default
    End Sub
#End Region


    Private Sub printdoc_HCFA1500_EndPrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles printdoc_HCFA1500.EndPrint
        If File.Exists(_OutputFilePath) = True Then
            File.Delete(_OutputFilePath)
        End If
    End Sub

    Private Sub C1Settings_SetupEditor(sender As System.Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1Settings.SetupEditor

        If e.Col = C1Settings.Cols(COL_X).Index Or e.Col = C1Settings.Cols(COL_Y).Index Then
            Dim txtData As TextBox = DirectCast(Me.C1Settings.Editor, TextBox)
            txtData.MaxLength = 5
        End If

    End Sub



    Private Sub C1Settings_RowValidating(sender As System.Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1Settings.RowValidating
        If Convert.ToString(C1Settings.GetData(e.Row, e.Col)) = "" Then
            MessageBox.Show("Please enter valid Data.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            e.Cancel = True
            bIsInvalidData = True
            C1Settings.StartEditing(e.Row, e.Col)
        Else
            bIsInvalidData = False
        End If

    End Sub

    Private Sub C1Settings_ValidateEdit(sender As System.Object, e As C1.Win.C1FlexGrid.ValidateEditEventArgs) Handles C1Settings.ValidateEdit
        If Me.Visible = True Then
            If Convert.ToString(C1Settings.Editor.Text) = "" Then
                MessageBox.Show("Please enter valid Data.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                e.Cancel = True
                bIsInvalidData = True
                C1Settings.StartEditing(e.Row, e.Col)
            Else
                bIsInvalidData = False
            End If
        End If

    End Sub

    Private _DpiX As Single = 96.0F
    Private _DpiY As Single = 96.0F
    Private _Bounds As Rectangle
    Private _MarginBounds As Rectangle
    Private _thisImage As Image = Nothing
    Private _cPiX As Single = 96.0F
    Private _cPiY As Single = 96.0F

    Dim _IsPrintForm As Boolean = False
    Private toCreateEMF As Boolean = gloGlobal.gloTSPrint.UseEMFForClaims

    Private Function printdoc_HCFA1500_ConversionWithLesserResolution(oConversion As clsPrintDocumentConversion, Optional isForPrint As [Boolean] = False) As String
        If Not isForPrint Then
            toCreateEMF = False
        End If
        Dim PaperWidth As Single = oConversion.PaperWidth
        Dim PaperHeight As Single = oConversion.PaperHeight
        _DpiX = oConversion.DpiX
        _DpiY = oConversion.DpiY
        _Bounds = New Rectangle(oConversion.BoundX, oConversion.BoundY, oConversion.BoundWidth, oConversion.BoundHeight)
        _MarginBounds = New Rectangle(oConversion.MarginBoundsX, oConversion.MarginBoundsY, oConversion.MarginBoundsWidth, oConversion.MarginBoundsHeight)


        Dim bAnyError As Boolean = False



        Try
            Using thisImage As System.Drawing.Image = System.Drawing.Image.FromFile(_OutputFilePath)
                _thisImage = thisImage
                Try

                    If toCreateEMF Then
                        _cPiX = _DpiX
                        _cPiY = _DpiY
                    Else
                        _cPiX = Math.Max((CSng(thisImage.Width) / PaperWidth), (CSng(thisImage.Height) / PaperHeight))
                        _cPiX = Math.Min(thisImage.HorizontalResolution, _cPiX)
                        _cPiX = Math.Min(_DpiY, _cPiX)
                        '_cPiY = Math.Min(_DpiY, (float)thisImage.Height / PaperHeight);

                        _cPiY = _cPiX * thisImage.VerticalResolution / thisImage.HorizontalResolution
                    End If
                    Dim emfBytes As Byte() = Nothing
                    Dim bmpWidht As Integer = Convert.ToInt32(PaperWidth * _DpiX)
                    Dim bmpHeight As Integer = Convert.ToInt32(PaperHeight * _DpiY)
                    Using NewBitmap As System.Drawing.Bitmap = New Bitmap(bmpWidht, bmpHeight)
                        Try
                            NewBitmap.SetResolution(_cPiX, _cPiY)
                            If toCreateEMF Then
                                emfBytes = gloGlobal.CreateEMF.GetEMFBytes(PaperWidth, PaperHeight, bmpWidht, bmpHeight, AddressOf CreateEMFForCMS1500FormWithLesserResolution)
                            Else
                                Using eGraphics As System.Drawing.Graphics = Graphics.FromImage(NewBitmap)
                                    FitToImageWithLesserResolutionToScaleAndKeepAtCenter(eGraphics)
                                End Using
                            End If
                        Catch generatedExceptionName As Exception
                            If toCreateEMF Then
                                toCreateEMF = False
                                Try
                                    Using eGraphics As System.Drawing.Graphics = Graphics.FromImage(NewBitmap)
                                        FitToImageWithLesserResolutionToScaleAndKeepAtCenter(eGraphics)

                                    End Using
                                Catch ex As Exception
                                    bAnyError = True
                                    gloAuditTrail.gloAuditTrail.ExceptionLog("Error occured during scaling tiff file at printdoc_HCFA1500_ConversionWithLesserResolution method", False)
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                                End Try

                            End If
                        End Try
                        If bAnyError Then
                            Return _OutputFilePath
                        Else


                            Dim _printFilePath As String = ""
                            Dim sPath As String = ""
                            Try
                                sPath = gloSettings.FolderSettings.AppTempFolderPath + "Paper_Claim_Files"

                                If System.IO.Directory.Exists(sPath) = False Then
                                    System.IO.Directory.CreateDirectory(sPath)
                                End If

                                If _IsPrintForm = True Then
                                    _printFilePath = (sPath & Convert.ToString("\")) + DateTime.Now.ToString("yyyyMMddhhmmsstt") + "f" + (If(toCreateEMF, ".emf", ".jpg"))
                                Else
                                    _printFilePath = (sPath & Convert.ToString("\")) + DateTime.Now.ToString("yyyyMMddhhmmsstt") + "f_BLANK" + (If(toCreateEMF, ".emf", ".tif"))
                                End If

                                If File.Exists(_printFilePath) = True Then
                                    File.Delete(_printFilePath)
                                End If

                                If toCreateEMF Then
                                    File.WriteAllBytes(_printFilePath, emfBytes)
                                Else
                                    NewBitmap.Save(_printFilePath, If(_IsPrintForm, System.Drawing.Imaging.ImageFormat.Jpeg, System.Drawing.Imaging.ImageFormat.Tiff))
                                End If

                                Return _printFilePath
                            Catch ex As Exception
                                gloAuditTrail.gloAuditTrail.ExceptionLog("Error occured during new bitmap save at printdoc_HCFA1500_ConversionWithLesserResolution method", False)
                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)

                                Return _OutputFilePath



                            End Try
                        End If
                    End Using
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog("Error occured during opening bitmap save at printdoc_HCFA1500_ConversionWithLesserResolution method", False)
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)

                    Return _OutputFilePath

                End Try

            End Using
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Return _OutputFilePath
        End Try

    End Function

    Private Sub FitToImageWithLesserResolutionToScaleAndKeepAtCenter(eGraphics As System.Drawing.Graphics)

        If Not _IsPrintForm Then
            'Dim Offset As Int64 = 10    
            Dim MyBounds As New RectangleF(_MarginBounds.Location, _MarginBounds.Size)

            Dim MarginRect As New RectangleF(0.0F, 1.0F, 1.0F, 1.0F)
            'MarginRect.Y = MarginRect.Y * _DpiY / 72;
            'MarginRect.X = MarginRect.X * _DpiX / 72;
            'MarginRect.Height = MarginRect.Height * _DpiY / 72;
            'MarginRect.Width = MarginRect.Width * _DpiX / 72;

            'MyBounds.Y = MyBounds.Y + MarginRect.Top;
            'MyBounds.X = MyBounds.X + MarginRect.Left;
            MyBounds.Height = (MyBounds.Height - (2.0F * MarginRect.Height)) / 72.0F
            MyBounds.Width = (MyBounds.Width - (2.0F * MarginRect.Width)) / 72.0F
            Dim y As Single = 25.0F * _cPiY / _DpiY
            ' e.PageSettings.Bounds.Y; //MyBounds.Top; 
            Dim x As Single = -80.0F * _cPiX / _DpiX
            ' e.PageSettings.Bounds.X; //MyBounds.Left;

            'GraphicsUnit units = eGraphics.PageUnit; //graphicsunits
            'RectangleF thisImageBound = _thisImage.GetBounds(ref units);
            'eGraphics.PageUnit = units;
            'thisImageBound.Height = 2181;


            Dim cardHeight As Single = _thisImage.Height / _thisImage.VerticalResolution
            Dim cardWidth As Single = _thisImage.Width / _thisImage.HorizontalResolution
            Dim ScaleX As Single = MyBounds.Width / cardWidth
            Dim ScaleY As Single = MyBounds.Height / cardHeight
            Dim ScaleZ As Single = 1.0F
            If ScaleZ < ScaleX Then
                ScaleZ = ScaleX
            End If
            If ScaleZ < ScaleY Then
                ScaleZ = ScaleY
            End If
            cardHeight *= (ScaleZ * _cPiY)
            cardWidth *= (ScaleZ * _cPiX)
            eGraphics.DrawImage(_thisImage, New RectangleF(x, y, cardWidth, cardHeight))
        Else
            Dim _width As Integer = _Bounds.Width - 30
            Dim _height As Integer = _Bounds.Height
            Dim _X As Integer = _Bounds.X
            Dim _Y As Integer = _Bounds.Y
            eGraphics.DrawImage(_thisImage, CSng(_X) * _cPiY / _DpiY, CSng(_Y) * _cPiY / _DpiY, CSng(_width) * _cPiY / _DpiY, CSng(_height) * _cPiY / _DpiY)
        End If
    End Sub


    Private Function CreateEMFForCMS1500FormWithLesserResolution(thisGraphics As Graphics, bmpWidth As Single, bmpHeight As Single) As Integer
        Try
            thisGraphics.Clear(Color.White)
            FitToImageWithLesserResolutionToScaleAndKeepAtCenter(thisGraphics)

            Return 0
        Catch
            Return 1
        End Try

    End Function

    Private Function formatedData(BoxField As FormFieldString, sdata As String) As String
        Try
            If sdata.Trim().Length > Convert.ToInt32(BoxField.CharSize) Then
                sdata = Convert.ToString(sdata).Substring(0, Convert.ToInt32(BoxField.CharSize))
            End If
            If bIsCapitalized Then
                Return sdata.ToUpper()
            Else
                Return sdata
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return ""
        End Try


    End Function
End Class