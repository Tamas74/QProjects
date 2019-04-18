Imports System.Windows.Forms
Public Class frmViewCQMCriteriaReason
    'Dim dtqrda1 As DataTable = Nothing
    Dim dsqrda1 As DataSet = Nothing
    Dim _npatientid As Int64 = 0
    Dim _type As String
    Dim _measurename As String
    Dim _databaseConnectionString As String
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    Dim Col_Module As Int16 = 0
    Dim _IsInExclusion As Boolean = False
    'Dim Col_CodeType As Int16 = 1
    Private _ReportingYear As String = ""
    'Public Delegate Sub DelShowHelp(ByVal obj As Object)
    'Public Event EvtShowHelp As DelShowHelp
    Public Property ReportingYear As String
        Get
            Return _ReportingYear
        End Get
        Set(ByVal value As String)
            _ReportingYear = value
        End Set
    End Property
#Region "vitals"
    Dim Col_BpSystolic As Int16 = 1
    Dim Col_BpDiastolic As Int16 = 2
    Dim Col_BpBMI As Int16 = 3
    Dim Col_vDate As Int16 = 4
    'Dim Col_vitPatientId As Int16 = 5
    Dim Col_vitPopCriteria As Int16 = 5
#End Region
#Region "Medication"
    Dim Col_Drugname As Int16 = 1
    Dim Col_mDate As Int16 = 2
    'Dim Col_medReasonConceptId As Int16 = 3
    'Dim Col_medpatientId As Int16 = 5
    Dim Col_medPopCriteria As Int16 = 3
#End Region
#Region "Immunization"
    Dim Col_CVX As Int16 = 1
    'Dim Col_TransId2 As Int16 = 3
    Dim Col_Frequency As Int16 = 2
    Dim Col_Amount As Int16 = 3
    Dim Col_Unit As Int16 = 4
    'Dim Col_immReasonConceptId As Int16 = 5
    Dim Col_ImmDate As Int16 = 5
    'Dim Col_ImmpatientId As Int16 = 9
    Dim Col_ImmPopCriteria As Int16 = 6
#End Region

#Region "General"
    Dim Col_ICD As Int16 = 1
    'Dim Col_ICD10 As Int16 = 3
    Dim Col_CPT As Int16 = 2
    Dim Col_ConceptID As Int16 = 3
    Dim Col_Date As Int16 = 4
    'Dim Col_ReasonConceptId As Int16 = 7
    'Dim Col_ReasonICD9 As Int16 = 8
    'Dim Col_ReasonICD10 As Int16 = 9
    'Dim Col_ReasonLOINC As Int16 = 10
    'Dim Col_PatientId As Int16 = 6
    Dim Col_PopCriteria As Int16 = 5
#End Region

#Region "Order"
    Dim Col_LOINC As Int16 = 1
    Dim Col_ReasonConceptId As Int16 = 2
    Dim Col_ReasonICD9 As Int16 = 3
    Dim Col_REasonICD10 As Int16 = 4
    Dim Col_ReasonLOINC As Int16 = 5
    Dim Col_oDate As Int16 = 6
    'Dim Col_opatientid As Int16 = 8
    Dim Col_opopcriteria As Int16 = 7
#End Region
#Region "NotNumerator"
    Dim Col_NotModule As Int16 = 0
    Dim Col_Code As Int16 = 1
    Dim Col_Codesystem As Int16 = 2
    Dim Col_Description As Int16 = 3
    Dim Col_ResultSnomed As Int16 = 4
#End Region
    Private strPatientDetails As String


    Public Sub New(ByVal dt As DataSet, ByVal npatientid As Int64, ByVal type As String, ByVal measurename As String, Optional ByVal IsInExclusion As Boolean = False)

        ' This call is required by the designer.
        InitializeComponent()
        dsqrda1 = dt
        _npatientid = npatientid
        _type = type
        _measurename = measurename
        pnlPatientDetails.Visible = True
        _IsInExclusion = IsInExclusion
        ' Add any initialization after the InitializeComponent() call.
        If appSettings("DataBaseConnectionString") IsNot Nothing Then
            If appSettings("DataBaseConnectionString") <> "" Then
                _databaseConnectionString = Convert.ToString(appSettings("DataBaseConnectionString"))
            End If
        End If
    End Sub

  
    Private Sub frmViewCQMCriteriaReason_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            If dsqrda1 IsNot Nothing And dsqrda1.Tables.Count > 0 Then
                FillData()
                SetGridStyle()
                DisplayPatientInfo()
                Me.BackColor = Drawing.Color.White
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CQMReports, gloAuditTrail.ActivityCategory.CQMReport, gloAuditTrail.ActivityType.General, "ViewCriteriaReason" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If Not IsNothing(dsqrda1) Then
                dsqrda1.Dispose()
            End If
        End Try
    End Sub
    Private Sub DisplayPatientInfo()
        If dsqrda1.Tables(5) IsNot Nothing Then
            Dim PatientCode As String = ""
            Dim PatientName As String = ""
            PatientCode = Convert.ToString(dsqrda1.Tables(5).Rows(0)("sPatientCode"))
            PatientName = Convert.ToString(dsqrda1.Tables(5).Rows(0)("PatientName"))
            strPatientDetails = PatientName + "( " + PatientCode + " )"
            Me.Text = Me.Text + " - " & strPatientDetails
            lblPatientDetails.Text = "Showing CQM details for the Patient: " & strPatientDetails
        End If
    End Sub
    Private Function GetNumeratorCriteria() As DataTable
        Dim dt As DataTable = New DataTable()
        Try
            Dim oDB As gloDatabaseLayer.DBLayer = New gloDatabaseLayer.DBLayer(_databaseConnectionString)
            Dim oDBparameters As gloDatabaseLayer.DBParameters = New gloDatabaseLayer.DBParameters()
            oDBparameters.Add("@MeasureName", _measurename, ParameterDirection.Input, SqlDbType.NVarChar)
            oDBparameters.Add("@ReportingYear", _ReportingYear, ParameterDirection.Input, SqlDbType.VarChar)
            oDB.Connect(False)
            oDB.Retrive("gsp_GetNumeratorCriteria", oDBparameters, dt)
            oDB.Disconnect()
            oDBparameters.Dispose()
            oDBparameters = Nothing
            oDB.Dispose()
            oDB = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CQMReports, gloAuditTrail.ActivityCategory.CQMReport, gloAuditTrail.ActivityType.General, "ViewCriteriaReason" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        Return dt
    End Function

    Private Sub tlsbtn_Cancel_Click(sender As Object, e As System.EventArgs) Handles tlsbtn_Cancel.Click
        Try
            Me.Close()
            Me.Dispose()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CQMReports, gloAuditTrail.ActivityCategory.CQMReport, gloAuditTrail.ActivityType.General, "ViewCriteriaReason" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub FillData()
        Try
            Dim dvQRDA1Data As New DataView
            'Dim dtdenominator As DataTable = New DataTable()
            'Dim totalheight As Int32 = 0
            If _type = "Numerator" Then
                'Dim dtnumerator As DataTable = New DataTable()
                dvQRDA1Data = dsqrda1.Tables(0).Copy().DefaultView
                'dvQRDA1Data.RowFilter = "nPatientID = '" & _npatientid & "' and PopCriteria = 2"
                dvQRDA1Data.RowFilter = "PopCriteria = 2"
                If dvQRDA1Data.Count > 0 Then
                    C1Vitals.DataSource = dvQRDA1Data.ToTable()
                End If
                dvQRDA1Data = dsqrda1.Tables(1).Copy.DefaultView
                'dvQRDA1Data.RowFilter = "nPatientID = '" & _npatientid & "' and PopCriteria = 2"
                dvQRDA1Data.RowFilter = "PopCriteria = 2"
                If dvQRDA1Data.Count > 0 Then
                    C1Medication.DataSource = dvQRDA1Data.ToTable()
                End If
                dvQRDA1Data = dsqrda1.Tables(2).Copy.DefaultView
                'dvQRDA1Data.RowFilter = "nPatientID = '" & _npatientid & "' and PopCriteria = 2"
                dvQRDA1Data.RowFilter = "PopCriteria = 2"
                If dvQRDA1Data.Count > 0 Then
                    C1Immunization.DataSource = dvQRDA1Data.ToTable()
                End If
                dvQRDA1Data = dsqrda1.Tables(3).Copy.DefaultView
                'dvQRDA1Data.RowFilter = "nPatientID = '" & _npatientid & "' and PopCriteria = 2"
                dvQRDA1Data.RowFilter = "PopCriteria = 2"
                If dvQRDA1Data.Count > 0 Then
                    C1General.DataSource = dvQRDA1Data.ToTable()
                End If
                dvQRDA1Data = dsqrda1.Tables(4).Copy.DefaultView
                'dvQRDA1Data.RowFilter = "nPatientID = '" & _npatientid & "' and PopCriteria = 2"
                dvQRDA1Data.RowFilter = "PopCriteria = 2"
                If dvQRDA1Data.Count > 0 Then
                    C1OrderResults.DataSource = dvQRDA1Data.ToTable()
                End If

                lblCriteriaReason.Text = "Patient having following conditions"
                Me.Controls.Remove(pnlC1NotNumerator)
                pnlC1NotNumerator.Dispose()
                sptpnlC1NotNumerator.Visible = False
                'Me.Height -= pnlC1NotNumerator.Height
            ElseIf _type = "Denominator" Then
                dvQRDA1Data = dsqrda1.Tables(0).Copy().DefaultView
                'dvQRDA1Data.RowFilter = "nPatientID = '" & _npatientid & "PopCriteria IN(0,1)"
                dvQRDA1Data.RowFilter = "PopCriteria IN(0,1)"
                If dvQRDA1Data.Count > 0 Then
                    C1Vitals.DataSource = dvQRDA1Data.ToTable()
                End If
                dvQRDA1Data = dsqrda1.Tables(1).Copy.DefaultView
                'dvQRDA1Data.RowFilter = "nPatientID = '" & _npatientid & "' and PopCriteria IN(0,1)"
                dvQRDA1Data.RowFilter = "PopCriteria IN(0,1)"
                If dvQRDA1Data.Count > 0 Then
                    C1Medication.DataSource = dvQRDA1Data.ToTable()
                End If
                dvQRDA1Data = dsqrda1.Tables(2).Copy.DefaultView
                'dvQRDA1Data.RowFilter = "nPatientID = '" & _npatientid & "' and PopCriteria IN(0,1)"
                dvQRDA1Data.RowFilter = "PopCriteria IN(0,1)"
                If dvQRDA1Data.Count > 0 Then
                    C1Immunization.DataSource = dvQRDA1Data.ToTable()
                End If
                dvQRDA1Data = dsqrda1.Tables(3).Copy.DefaultView
                'dvQRDA1Data.RowFilter = "nPatientID = '" & _npatientid & "' and PopCriteria IN(0,1)"
                dvQRDA1Data.RowFilter = "PopCriteria IN(0,1)"
                If dvQRDA1Data.Count > 0 Then
                    C1General.DataSource = dvQRDA1Data.ToTable()
                End If
                dvQRDA1Data = dsqrda1.Tables(4).Copy.DefaultView
                'dvQRDA1Data.RowFilter = "nPatientID = '" & _npatientid & "' and PopCriteria IN(0,1)"
                dvQRDA1Data.RowFilter = "PopCriteria IN(0,1)"
                If dvQRDA1Data.Count > 0 Then
                    C1OrderResults.DataSource = dvQRDA1Data.ToTable()
                End If
                lblCriteriaReason.Text = "Patient having following conditions"
                'totalheight = totalheight + pnlMainHeader.Height
                If _IsInExclusion = True Then
                    C1NotNumerator.DataSource = Nothing
                    C1NotNumerator.Visible = False
                    lblHeader.Text = "Patient satisfies Denominator Exclusion criteria"
                Else
                    Dim dt As DataTable = New DataTable()
                    dt = GetNumeratorCriteria()
                    If Not IsNothing(dt) Then
                        If dt.Rows.Count > 0 Then
                            C1NotNumerator.DataSource = dt
                            C1NotNumerator.Cols(Col_Description).Width = 400
                            C1NotNumerator.Cols(Col_ResultSnomed).Width = 180
                            C1NotNumerator.Cols(Col_Code).Width = 150
                            C1NotNumerator.Cols(Col_Module).Width = 150
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CQMReports, gloAuditTrail.ActivityCategory.CQMReport, gloAuditTrail.ActivityType.General, "ViewCriteriaReason" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        
    End Sub
    Private Sub SetGridStyle()
        Try
            If C1Vitals.Rows.Count = 1 AndAlso C1Medication.Rows.Count = 1 AndAlso C1Immunization.Rows.Count = 1 AndAlso C1General.Rows.Count = 1 AndAlso C1OrderResults.Rows.Count = 1 Then
                lblCriteriaReason.Visible = False
            Else
                lblCriteriaReason.Visible = True
            End If
            If C1Vitals.Rows.Count = 1 Then
                Me.Controls.Remove(pnlC1Vitals)
                pnlC1Vitals.Dispose()
                'Me.Height -= pnlC1Vitals.Height
                sptpnlC1Vitals.Visible = False
            Else
                'totalheight = totalheight + C1Vitals.Height
                'C1Vitals.Cols(Col_CodeDesc).Width = 120
                'C1Vitals.Cols(Col_vitPatientId).Visible = False
                C1Vitals.Cols(Col_vitPopCriteria).Visible = False


            End If
            If C1Medication.Rows.Count = 1 Then
                Me.Controls.Remove(pnlC1Medication)
                pnlC1Medication.Dispose()
                sptpnlC1Medication.Visible = False
                'Me.Height -= pnlC1Medication.Height
            Else
                'C1Medication.Cols(Col_medReasonConceptId).Width = 120
                C1Medication.Cols(Col_Drugname).Width = 500
                'totalheight = totalheight + C1Medication.Height
                'C1Medication.Cols(Col_medpatientId).Visible = False
                C1Medication.Cols(Col_medPopCriteria).Visible = False

            End If
            If C1Immunization.Rows.Count = 1 Then
                Me.Controls.Remove(pnlC1Immunization)
                pnlC1Immunization.Dispose()
                sptpnlC1Immunization.Visible = False
                'Me.Height -= pnlC1Immunization.Height
            Else
                'C1Immunization.Cols(Col_immReasonConceptId).Width = 120
                'C1Immunization.Cols(Col_CodeDesc).Width = 120
                'totalheight = totalheight + C1Immunization.Height
                'C1Immunization.Cols(Col_ImmpatientId).Visible = False
                C1Immunization.Cols(Col_ImmPopCriteria).Visible = False

            End If
            If C1General.Rows.Count = 1 Then
                Me.Controls.Remove(pnlC1General)
                pnlC1General.Dispose()
                sptpnlC1General.Visible = False
                'Me.Height -= pnlC1General.Height
            Else
                C1General.Cols(Col_ICD).Width = 250
                C1General.Cols(Col_ConceptID).Width = 250
                C1General.Cols(Col_CPT).Width = 250
                'C1General.Cols(Col_CodeDesc).Width = 120
                'totalheight = totalheight + C1General.Height
                'C1General.Cols(Col_PatientId).Visible = False
                C1General.Cols(Col_PopCriteria).Visible = False
            End If
            If C1OrderResults.Rows.Count = 1 Then
                Me.Controls.Remove(pnlC1OrderResults)
                pnlC1OrderResults.Dispose()
            Else
                C1OrderResults.Cols(Col_ReasonConceptId).Width = 180
                'C1OrderResults.Cols(Col_opatientid).Visible = False
                C1OrderResults.Cols(Col_opopcriteria).Visible = False
            End If
            If _measurename = 166 Then
                If _type = "Numerator" Then
                    lblCriteriaReason.Text = "This Patient does not require any code(Snomed,CPT,ICD,LOINC) to satisfy numerator"
                    lblCriteriaReason.Visible = True
                    C1Vitals.Visible = False
                    C1OrderResults.Visible = False
                    C1Medication.Visible = False
                    C1General.Visible = False
                    C1Immunization.Visible = False
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CQMReports, gloAuditTrail.ActivityCategory.CQMReport, gloAuditTrail.ActivityType.General, "ViewCriteriaReason" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
       
    End Sub
End Class