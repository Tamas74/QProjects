

Public Class frmdmsetuprpt
    Shared dtcri As DataTable = Nothing
    Shared dtpat As DataTable = Nothing
    Private Sub frmdmsetuprpt_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       
        Dim strprb As String = gstrSMProblem
        'Private Function FindGuidelinesForSinglePatientCriteria(ByVal CriteriaID As Long, ByVal PatientID As Long) As Boolean
        '    Dim oCriteria As New Supporting.Criteria
        '    Dim oPatientDetail As New Supporting.PatientDetail
        '    Try

        '        'Dim _CriteriaName As String = GetCriteriaName(CriteriaID)
        '        oCriteria = GetCriteria(CriteriaID, 0)

        '        If IsNothing(oCriteria) = True Then
        '            Return False
        '            Exit Function
        '        End If

        '        oPatientDetail = GetPatientDetails(PatientID)

        '        ''AGE
        '        If Not (oPatientDetail.Age > oCriteria.AgeMinimum And oPatientDetail.Age < oCriteria.AgeMaximum) Then
        '            Return False
        '        End If

        '        ''GENDER
        '        If Not (oPatientDetail.Gender = oCriteria.Gender Or oCriteria.Gender = "All") Then
        '            Return False
        '        End If


        '        ''HEIGHT
        '        Dim CriteriaHeightMax As Decimal = FtToMtr(oCriteria.HeightMaximum)
        '        Dim CriteriaHeightMin As Decimal = FtToMtr(oCriteria.HeightMinimum)

        '        If CriteriaHeightMax > 0 And CriteriaHeightMin > 0 And oPatientDetail.Height <> "" Then
        '            Dim PatientHeight As Decimal = FtToMtr(oPatientDetail.Height)
        '            If Not (PatientHeight > CriteriaHeightMin And PatientHeight < CriteriaHeightMax) Then
        '                Return False
        '            End If
        '        End If

        '        ''WEIGHT
        '        If oCriteria.WeightMinimum > 0 And oCriteria.WeightMaximum > 0 Then
        '            If Not (oPatientDetail.WeightInlbs > oCriteria.WeightMinimum And oPatientDetail.WeightInlbs < oCriteria.WeightMaximum) Then
        '                Return False
        '            End If
        '        End If

        '        ''PULSE
        '        If oCriteria.PulseMinimum > 0 And oCriteria.PulseMaximum > 0 Then
        '            If Not (oPatientDetail.Pulse > oCriteria.PulseMinimum And oPatientDetail.Pulse < oCriteria.PulseMaximum) Then
        '                Return False
        '            End If
        '        End If

        '        ''PULSE_OX
        '        If oCriteria.PulseOXMinimum > 0 And oCriteria.PulseOXMaximum > 0 Then
        '            If Not (oPatientDetail.PulseOX > oCriteria.PulseOXMinimum And oPatientDetail.PulseOX < oCriteria.PulseOXMaximum) Then
        '                Return False
        '            End If
        '        End If

        '        ''BP SITTING MAX
        '        If oCriteria.BPSittingMaximum > 0 Then
        '            If Not (oPatientDetail.BPSittingMaximum = oCriteria.BPSittingMaximum) Then
        '                Return False
        '            End If
        '        End If

        '        ''BP SITTING MIN
        '        If oCriteria.BPSittingMinimum > 0 Then
        '            If Not (oPatientDetail.BPSittingMinimum = oCriteria.BPSittingMinimum) Then
        '                Return False
        '            End If
        '        End If

        '        ''BP STANDIN MAX
        '        If oCriteria.BPStandingMaximum > 0 Then
        '            If Not (oPatientDetail.BPStandingMaximum = oCriteria.BPStandingMaximum) Then
        '                Return False
        '            End If
        '        End If

        '        ''BP STANDIN MIN
        '        If oCriteria.BPStandingMinimum > 0 Then
        '            If Not (oPatientDetail.BPStandingMinimum = oCriteria.BPStandingMinimum) Then
        '                Return False
        '            End If
        '        End If

        '        ''BMI
        '        If oCriteria.BMIMinimum > 0 And oCriteria.BMIMaximum > 0 Then
        '            If Not (oPatientDetail.BMI > oCriteria.BMIMinimum And oPatientDetail.BMI < oCriteria.BMIMaximum) Then
        '                Return False
        '            End If
        '        End If

        '        '' TEMPERATURE ''
        '        If oCriteria.TempratureMinumum > 0 And oCriteria.TempratureMaximum > 0 Then
        '            If Not (oPatientDetail.TempratureInF > oCriteria.TempratureMinumum And oPatientDetail.TempratureInF < oCriteria.TempratureMaximum) Then
        '                Return False
        '            End If
        '        End If


        '        ''OTHER DETAILS
        '        Dim _MatchCounter As Integer
        '        For iPatDetail As Integer = 1 To oPatientDetail.OtherDetails.Count
        '            For iCriteria As Integer = 1 To oCriteria.OtherDetails.Count
        '                If IsOtherDetailSame(oPatientDetail.OtherDetails(iPatDetail), oCriteria.OtherDetails(iCriteria)) Then
        '                    _MatchCounter = _MatchCounter + 1
        '                    Exit For '' SUDHIR 20091223 '' LOGIC AS PER 2.7.3 '' ALL CRITERIA SHOULD MATCH FOR ALERT ''

        '                    '' Any of the Criteria Matches then Return TRUE
        '                    ''Return True '' 20090812 -- Logic Changed -
        '                End If
        '            Next
        '        Next

        '        '' SUDHIR 20091223 '' LOGIC AS PER 2.7.3 '' ALL CRITERIA SHOULD MATCH FOR ALERT ''
        '        '' If All Criterias of Patient & DM are Matching then Return TRUE
        '        If Not _MatchCounter = oCriteria.OtherDetails.Count Then
        '            Return False
        '        End If

        '        '' ALL CRITERIA SATISFIED ''
        '        Return True
        '    Catch ex As Exception
        '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    End Try
        'End Function

    End Sub

    Public Shared Function getlistcode(ByVal Criterianame As String) As String
        Try
            Dim oDMSingleProcess As New gloStream.DiseaseManagement.DiseaseManagement
            Dim strpatcode As String = ""
            If Criterianame.Trim() <> "" Then
                dtcri = GetCriteriaCategory(Criterianame)
                dtpat = GetPatientID()
                For Len As Integer = 0 To dtpat.Rows.Count - 1
                    For lencri As Integer = 0 To dtcri.Rows.Count - 1
                        If oDMSingleProcess.FindGuidelinesForSinglePatientCriteria(dtcri.Rows(lencri)(0), dtpat.Rows(Len)(0)) = True Then
                            strpatcode &= dtpat.Rows(Len)(0) & "|"
                            Exit For
                        End If
                    Next
                Next

                '' GetPatientData(strpatcode)
                If strpatcode.Length > 2 Then
                    strpatcode = strpatcode.Substring(0, strpatcode.Length - 1)
                End If
                Return strpatcode
            Else

                Return ""
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            Return ""
        End Try
    End Function
    Private Sub GetPatientData(ByVal strpatcode As String)
        Dim oDB As New gloStream.gloDataBase.gloDataBase

        Dim Query As String = ""

        Try
            oDB.Connect(GetConnectionString)
            Query = "SELECT dm_mst_id  FROM DM_Criteria_MST "
            'dtcri = 
            If Not IsNothing(dtcri) Then
                '  Return dtcri
            End If
            oDB.Disconnect()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Shared Function GetCriteriaCategory(ByVal Criterianame As String) As DataTable
        Dim oDB As New gloStream.gloDataBase.gloDataBase

        Dim Query As String = ""

        Try
            oDB.Connect(GetConnectionString)
            Query = "SELECT dm_mst_id  FROM DM_Criteria_MST where dm_mst_CriteriaName in (" & Criterianame & ")"
            dtcri = oDB.ReadQueryDataTable(Query)
            If Not IsNothing(dtcri) Then
                Return dtcri
            End If
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Disconnect()
            oDB.Dispose()
        End Try
    End Function


    Private Shared Function GetPatientID() As DataTable
        Dim oDB As New gloStream.gloDataBase.gloDataBase

        Dim Query As String = ""

        Try
            oDB.Connect(GetConnectionString)
            Query = "SELECT nPatientId  FROM Patient "
            dtpat = oDB.ReadQueryDataTable(Query)
            If Not IsNothing(dtpat) Then
                Return dtpat
            End If
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Disconnect()
            oDB.Dispose()
        End Try
    End Function
End Class