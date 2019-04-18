Imports System.Data.SqlClient
Imports System.IO
Imports gloCCDSchema
Imports System.Xml
Imports System.Xml.Serialization
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports gloCommon




Public Class FrmPatientCCDAConsent
    Dim _nId As Int64 = 0
    Dim _nPatientId As Int64 = 0
    Dim _sSectionName As String
    Dim _sCDAPrivacyText As String
    Dim _PurposeofUse As String
    Dim sprivacyTextSval As String = ""

    Private Sub tblClose_Click(sender As System.Object, e As System.EventArgs) Handles tblClose.Click
        Dim drResult As DialogResult = MessageBox.Show("Do you want to save your changes?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)
        If drResult = DialogResult.Yes Then
            SaveCCDAPatientConsent()
            Me.Close()
        ElseIf drResult = DialogResult.No Then
            Me.Close()
        End If
    End Sub


    Private Sub FrmPatientCCDAConsent_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim scheme As gloCommon.Cls_TabIndexSettings.TabScheme = gloCommon.Cls_TabIndexSettings.TabScheme.AcrossFirst
        Dim tom As New Cls_TabIndexSettings(Me)
        tom.SetTabOrder(scheme)
        tom = Nothing

        chkCOCareTeamMem.Visible = False

        ''_nPatientId = MainMenu.gnPatientID
        getPurposeofUseCodes()
        setFormData()
    End Sub

    Public Sub New(gnPatientID As Long)
        ' TODO: Complete member initialization 
        InitializeComponent()
        _nPatientId = gnPatientID
    End Sub
    Private Sub setFormData()
        Dim dsCCDAPatientConsent As New DataSet
        Dim clCCDAPatientConsent As New DataView

        Dim oCDADataExtraction As gloCCDLibrary.gloCDADataExtraction = New gloCCDLibrary.gloCDADataExtraction()      

        dsCCDAPatientConsent = oCDADataExtraction.getCCDAPatientConsentVal(_nPatientId)

        oCDADataExtraction = Nothing
        '' for Patient Consent - chkboxes
        clCCDAPatientConsent = dsCCDAPatientConsent.Tables(0).DefaultView
        ''Dim bval As Boolean = False
        Dim nCount As Int16
        For nCount = 0 To clCCDAPatientConsent.Count - 1
            ''bval = False
            For Each ctrl As Control In pnlClinicalSummary.Controls
                If TypeOf ctrl Is CheckBox And ctrl.Text.ToLower() <> "select all" Then
                    If ctrl.Text.ToLower().Contains("goals") Or ctrl.Text.ToLower().Contains("health") Then
                        If ctrl.Text.ToLower().Trim() = getText(clCCDAPatientConsent(nCount).Item(0).ToString().ToLower().Trim()) Then
                            DirectCast(ctrl, CheckBox).Checked = True
                            ''bval = True
                            Exit For
                        End If
                    Else
                        If ctrl.Text.ToLower().Trim() = clCCDAPatientConsent(nCount).Item(0).ToString().ToLower().Trim() Then
                            DirectCast(ctrl, CheckBox).Checked = True
                            ''bval = True
                            Exit For
                        End If
                    End If
                End If
            Next
            ''If bval = False Then
            ''    MessageBox.Show(clCCDAPatientConsent(nCount).Item(0).ToString())
            ''End If
        Next

        If clCCDAPatientConsent.Count = 25 Then
            ChkAll.Checked = True
        End If

        '' for Patient Privacy Text 
        clCCDAPatientConsent = dsCCDAPatientConsent.Tables(1).DefaultView

        If clCCDAPatientConsent.Count > 0 Then
            'handle dbnull exception Resolve Bug ID - Bug #111358 
            If Not IsDBNull(clCCDAPatientConsent(0).Item(0)) Then
                If clCCDAPatientConsent(0).Item(0).Trim() <> "" Then
                    txtCDAPrivacyText.Text = clCCDAPatientConsent(0).Item(0).ToString().Trim()
                End If
            End If
        End If

        If dsCCDAPatientConsent.Tables(3).DefaultView.Count > 0 And txtCDAPrivacyText.Text.Trim() = "" Then
            sprivacyTextSval = dsCCDAPatientConsent.Tables(3).DefaultView.Table(0).Item(0).ToString().Trim()
            txtCDAPrivacyText.Text = dsCCDAPatientConsent.Tables(3).DefaultView.Table(0).Item(0).ToString().Trim()
        End If


        '' for Patient Privacy Text 
        clCCDAPatientConsent = dsCCDAPatientConsent.Tables(2).DefaultView

        If clCCDAPatientConsent.Count > 0 Then
            cmbPurposeofUse.Text = clCCDAPatientConsent(0).Item(0).ToString()
        End If


        clCCDAPatientConsent = Nothing
        dsCCDAPatientConsent = Nothing
    End Sub

    ''Private Function getCCDAPatientConsentVal(ByVal _nPatientId As Int64) As DataSet

    ''    ''Dim clCCDAPatientConsent As New Collection
    ''    Dim objCon As New SqlConnection
    ''    Dim objCmd As New SqlCommand
    ''    ''Dim objSQLDataReader As SqlDataReader
    ''    Dim sqlParam As SqlParameter = Nothing
    ''    Dim da As SqlDataAdapter = New SqlDataAdapter
    ''    Dim ds As DataSet = New DataSet
    ''    Try
    ''        objCon.ConnectionString = GetConnectionString()

    ''        objCmd.CommandType = CommandType.StoredProcedure
    ''        objCmd.CommandText = "gsp_getCCDAPatientConsent"
    ''        objCmd.Connection = objCon


    ''        sqlParam = objCmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
    ''        sqlParam.Direction = ParameterDirection.Input
    ''        sqlParam.Value = _nPatientId

    ''        If objCon.State = ConnectionState.Closed Then
    ''            objCon.Open()
    ''        End If

    ''        da = New SqlDataAdapter(objCmd)
    ''        da.Fill(ds)
    ''        ''objSQLDataReader = objCmd.ExecuteReader
    ''        ''While objSQLDataReader.Read
    ''        ''    clCCDAPatientConsent.Add(objSQLDataReader.Item(0))
    ''        ''End While
    ''        ''objSQLDataReader.Close()
    ''        objCon.Close()
    ''        ''objSQLDataReader = Nothing

    ''        Return ds

    ''    Catch ex As Exception
    ''        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    ''        Throw ex
    ''        Return Nothing
    ''    Finally
    ''        If Not IsNothing(objCmd) Then
    ''            objCmd.Parameters.Clear()
    ''            objCmd.Dispose()
    ''            objCmd = Nothing
    ''        End If
    ''        If Not IsNothing(objCon) Then
    ''            objCon.Dispose()
    ''            objCon = Nothing
    ''        End If
    ''    End Try


    ''End Function

    Private Sub SaveCCDAPatientConsent()
        Try

            _sSectionName = ""
            For Each ctrl As Control In pnlClinicalSummary.Controls
                If TypeOf ctrl Is CheckBox AndAlso DirectCast(ctrl, CheckBox).Checked AndAlso DirectCast(ctrl, CheckBox).Text <> "Clear All" AndAlso DirectCast(ctrl, CheckBox).Text <> "Select All" Then
                    '' sSections(i) = ctrl.Text
                    If ctrl.Text.ToLower().Contains("goals") Or ctrl.Text.ToLower().Contains("health") Then
                        If _sSectionName.Trim() = "" Then
                            _sSectionName = getText(ctrl.Text.Trim(), True)
                        Else
                            _sSectionName = _sSectionName + "," + getText(ctrl.Text.Trim(), True)
                        End If
                    Else
                        If _sSectionName.Trim() = "" Then
                            _sSectionName = ctrl.Text.Trim()
                        Else
                            _sSectionName = _sSectionName + "," + ctrl.Text.Trim()
                        End If
                    End If                   
                End If
            Next
            If txtCDAPrivacyText.Text.Trim() <> sprivacyTextSval.Trim() And sprivacyTextSval.Trim() <> "" Then
                _sCDAPrivacyText = txtCDAPrivacyText.Text.Trim()
            ElseIf sprivacyTextSval.Trim() = "" Then
                _sCDAPrivacyText = txtCDAPrivacyText.Text.Trim()
            End If
            _PurposeofUse = cmbPurposeofUse.Text.Trim()

            
            Dim oCDADataExtraction As gloCCDLibrary.gloCDADataExtraction = New gloCCDLibrary.gloCDADataExtraction()
            _nId = oCDADataExtraction.SaveCDAConsent(_nId, _nPatientId, _sSectionName, _sCDAPrivacyText, _PurposeofUse)

            If _sSectionName.Trim() = "" And _sCDAPrivacyText = Nothing Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.CCDAPatientConsent, gloAuditTrail.ActivityType.Save, "CCDA Patient Consent Saved as blank for patient consent and privacy text.", _nPatientId, _nId, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.CCDAPatientConsent, gloAuditTrail.ActivityType.Save, "CCDA Patient Consent Saved.", _nPatientId, _nId, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
            oCDADataExtraction = Nothing

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.CCDAPatientConsent, gloAuditTrail.ActivityType.Save, ex, gloAuditTrail.ActivityOutCome.Failure)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Function getText(ByVal sval As String, Optional ByVal bisave As Boolean = False) As String
        If bisave Then
            Select Case sval.ToLower()
                Case "goals"
                    sval = "Goals Section"
                Case "health concerns"
                    sval = "Health Concerns Section"
            End Select
        Else
            Select Case sval.ToLower()
                Case "goals section"
                    sval = "goals"
                Case "health concerns section"
                    sval = "health concerns"
            End Select
        End If
        Return sval
    End Function

    Private Sub getPurposeofUseCodes()
        With cmbPurposeofUse
            .Items.Clear()
            Dim clPurposeofUseCodes As New Collection
            clPurposeofUseCodes = getPurposeofUseCodesVal()

            Dim nCount As Int16
            For nCount = 1 To clPurposeofUseCodes.Count
                .Items.Add(clPurposeofUseCodes.Item(nCount).ToString.Trim)
            Next
            .SelectedIndex = 0
        End With
    End Sub

    Private Function getPurposeofUseCodesVal() As Collection

        Dim clPurposeofUseCodes As New Collection
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader

        Try
            objCon.ConnectionString = GetConnectionString()

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_getCCDAPurposeofUseCodes"
            objCmd.Connection = objCon
            objCon.Open()
            objSQLDataReader = objCmd.ExecuteReader
            While objSQLDataReader.Read
                clPurposeofUseCodes.Add(objSQLDataReader.Item(0))
            End While
            objSQLDataReader.Close()
            objCon.Close()
            objSQLDataReader = Nothing

            Return clPurposeofUseCodes
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
            Return Nothing
        Finally
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try


    End Function

    Private Sub tblSaveCls_Click(sender As System.Object, e As System.EventArgs) Handles tblSaveCls.Click
        SaveCCDAPatientConsent()
        Me.Close()
    End Sub

    Private Sub ChkAll_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkAll.CheckedChanged
        If ChkAll.Checked = True Then
            ChkAll.Text = "Clear All"
            Call SelectAll()
        Else
            ChkAll.Text = "Select All"
            Call UnSelectAll()
        End If        
    End Sub

    Private Sub SelectAll()
        ''ChkAll.Checked = True
        chkCOProblems.Checked = True
        chkCOAllergy.Checked = True
        chkCOCareTeamMem.Checked = True
        chkCOProcedures.Checked = True
        chkCOVitalSigns.Checked = True
        chkCOlabResult.Checked = True
        chkCOLabTest.Checked = True
        chkCOMedication.Checked = True
        chkCSClinicalInstru.Checked = True
        chkCOFamilyHistory.Checked = True
        chkCOSocialHistory.Checked = True
        chkImplant.Checked = True        
        chkCSFutureAppt.Checked = True        
        chkCSVisitMedications.Checked = True
        chkCSVisitImmunization.Checked = True
        chkAmbImmunization.Checked = True
        chkTransCareEncounter.Checked = True
        chkTransCareCognitiveStat.Checked = True
        ChkGoals.Checked = True
        ChkTreatmentPlan.Checked = True
        chkInterventions.Checked = True
        ChkCOAssessments.Checked = True
        ChkHealthConcerns.Checked = True
        chkHealthStatus.Checked = True
        chkCSVisitReason.Checked = True
    End Sub

    Private Sub UnSelectAll()
        chkCOProblems.Checked = False
        chkCOAllergy.Checked = False
        chkCOCareTeamMem.Checked = False
        chkCOProcedures.Checked = False
        chkCOVitalSigns.Checked = False
        chkCOlabResult.Checked = False
        chkCOLabTest.Checked = False
        chkCOMedication.Checked = False
        chkCSClinicalInstru.Checked = False
        chkCOFamilyHistory.Checked = False
        chkCOSocialHistory.Checked = False
        chkImplant.Checked = False
        chkCSFutureAppt.Checked = False
        chkCSVisitMedications.Checked = False
        chkCSVisitImmunization.Checked = False
        chkAmbImmunization.Checked = False
        chkTransCareEncounter.Checked = False
        chkTransCareCognitiveStat.Checked = False
        ChkGoals.Checked = False
        ChkTreatmentPlan.Checked = False
        chkInterventions.Checked = False
        ChkCOAssessments.Checked = False
        ChkHealthConcerns.Checked = False
        chkHealthStatus.Checked = False
        chkCSVisitReason.Checked = False
    End Sub
End Class