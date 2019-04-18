Imports gloUserControlLibrary
Imports System.Data.SqlClient
Public Class frmAdvanceGraph
    Implements IPatientContext
    
    Private WithEvents gloUC_PatientStrip1 As gloUC_PatientStrip = Nothing

    Private HeightWidth As Integer = 800   'by sudhir 20081112
    Private _enumPercetile As enumGrowthChartPercentile
    Dim _PatientID As Long
    Private strLicensekey As String = ""
    Private strExtLicensekey As String = ""
    Dim oclsEncryption As clsencryption = Nothing
    Public Enum enumGrowthChartPercentile
        DontShowPercentile = 0
        ShowPercentile = 1
        ShowPercentileOnMouseHoover = 2
    End Enum

    Private Sub tls_Chart_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tls_Chart.ItemClicked
        If Not IsNothing(e.ClickedItem.Tag) Then
            Select Case e.ClickedItem.Tag.ToString
                Case "Stature-WeightVsAge"
                    ShowGraph(4)
                Case "HeightVsStature"
                    ShowGraph(5)
                Case "ZoomIn"  ''by sudhir 20081112
                    ZoomIn()
                Case "ZoomOut"
                    ZoomOut()  ''end sudhir
                Case "Print"
                    AxAdvChart.PrintChart()
                Case "Close"
                    Me.Close()
                Case Else

            End Select
        End If
    End Sub

    ''by sudhir 20081112  
    Private Sub ZoomIn()
        If HeightWidth < 1300 Then
            HeightWidth = HeightWidth + 100
            AxAdvChart.Height = HeightWidth
            SetChartPanelSize()
            SetScrollBarValues()
        End If
    End Sub
    Private Sub ZoomOut()
        If HeightWidth > 700 Then
            HeightWidth = HeightWidth - 100
            AxAdvChart.Height = HeightWidth
            SetChartPanelSize()
            SetScrollBarValues()
        End If
    End Sub

    Private Sub SetChartPanelSize()  'pnlChart Updates its size according to AxAdvChart size change
        pnlChart.Height = AxAdvChart.Height + 100
        pnlChart.Width = AxAdvChart.Width
        pnlChart.Location = New System.Drawing.Point(pnlChart.Location.X, "-" & VScrollBar.Value)
    End Sub

    Private Sub SetScrollBarValues()  'Set Both Scrollbar's Values, It synchronize with size of pnlChart panel
        Dim tmp As Int16 = (pnlChart.Height - pnlBackground.Height) + 50
        'VScrollBar.Maximum = (pnlChart.Height - pnlBackground.Height) + 50
        If tmp > 0 Then
            VScrollBar.Maximum = tmp
        End If
        HScrollBar.Maximum = (pnlChart.Width - pnlBackground.Width)
        VScrollBar.Minimum = 0
        HScrollBar.Minimum = 0

        If VScrollBar.Value = VScrollBar.Maximum Then
            pnlChart.Location = New System.Drawing.Point(pnlChart.Location.X, pnlChart.Location.Y + 100)
        End If
    End Sub
    
    'Age Calculator function to Display Exact Age in Text ' used to set comments
    Public Shared Function FormatAge(ByVal BirthDate As DateTime, ByVal VitalDate As DateTime) As String
        ' Compute the difference between BirthDate 'CODE FROM gloPM
        'year and end year. 
        Dim IsBirthDateLeap As Boolean = False
        Dim years As Integer = VitalDate.Year - BirthDate.Year
        Dim months As Integer = 0
        Dim days As Integer = 0
        'Test if BirthDay for LeapYear.
        If BirthDate.Day = 29 AndAlso BirthDate.Month = 2 Then
            IsBirthDateLeap = True
        End If
        ' Check if the last year was a full year. 
        If VitalDate < BirthDate.AddYears(years) AndAlso years <> 0 Then
            years -= 1
        End If
        BirthDate = BirthDate.AddYears(years)
        ' Now we know BirthDate <= end and the diff between them 
        ' is < 1 year. 
        If BirthDate.Year = VitalDate.Year Then
            months = VitalDate.Month - BirthDate.Month
        Else
            months = (12 - BirthDate.Month) + VitalDate.Month
        End If
        ' Check if the last month was a full month. 
        If VitalDate < BirthDate.AddMonths(months) AndAlso months <> 0 Then
            months -= 1
        End If
        BirthDate = BirthDate.AddMonths(months)
        ' Now we know that BirthDate < end and is within 1 month 
        ' of each other. 
        days = (VitalDate - BirthDate).Days

        'To Adjust Age if BirthDate is 29th Feb in leap year
        If IsBirthDateLeap = True Then   ''Sequence of following IF code is too important.. DON'T MODIFY
            days -= 1
            If VitalDate.Day = 29 AndAlso VitalDate.Month = 2 Then
                days += 1
            ElseIf VitalDate.Year Mod 4 = 0 Then
                days += 1
            End If
            If days < 0 AndAlso VitalDate.Year Mod 4 <> 0 Then
                days = 30
                months = months - 1
                If months < 0 Then
                    months = 11
                    years = years - 1
                End If
            End If
            If months = 12 Then
                days = 30
                months = 11
            End If
        End If

        'Return years & " years " & months & " months " & days & " days"
        'Following code to display age in Numeric and Text
        If years = 0 Then
            If months = 0 Then
                If days <= 1 Then
                    Return days & " Day"
                Else
                    Return days & " Days"
                End If
            ElseIf months = 1 Then
                If days = 0 Then
                    Return months & " Month"
                ElseIf days = 1 Then
                    Return months & " Month " & days & " Day"
                Else
                    Return months & " Month " & days & " Days"
                End If
            ElseIf months > 1 Then
                If days = 0 Then
                    Return months & " Months"
                ElseIf days = 1 Then
                    Return months & " Months " & days & " Day"
                Else
                    Return months & " Months " & days & " Days"
                End If
            End If
        ElseIf years = 1 Then
            If months = 0 Then
                If days = 0 Then
                    Return years & " Year "
                ElseIf days = 1 Then
                    Return years & " Year " & days & " Day"
                Else
                    Return years & " Year " & days & " Days"
                End If
            ElseIf months = 1 Then
                If days = 0 Then
                    Return years & " Year " & months & " Month "
                ElseIf days = 1 Then
                    Return years & " Yr " & months & " Mon " & days & " Day"
                Else
                    Return years & " Yr " & months & " Mon " & days & " Days"
                End If
            ElseIf months > 1 Then
                If days = 0 Then
                    Return years & " Year " & months & " Months "
                ElseIf days = 1 Then
                    Return years & " Yr " & months & " Mons " & days & " Day"
                Else
                    Return years & " Yr " & months & " Mons " & days & " Days"
                End If
            End If
        ElseIf years > 1 Then
            If months = 0 Then
                If days = 0 Then
                    Return years & " Years "
                ElseIf days = 1 Then
                    Return years & " Years " & days & " Day"
                Else
                    Return years & " Years " & days & " Days"
                End If
            ElseIf months = 1 Then
                If days = 0 Then
                    Return years & " Years " & months & " Month"
                ElseIf days = 1 Then
                    Return years & " Yrs " & months & " Mon " & days & " Day"
                Else
                    Return years & " Yrs " & months & " Mon " & days & " Days"
                End If
            ElseIf months > 1 Then
                If days = 0 Then
                    Return years & " Years " & months & " Months"
                ElseIf days = 1 Then
                    Return years & " Yrs " & months & " Mons " & days & " Day"
                Else
                    Return years & " Yrs " & months & " Mons " & days & " Days"
                End If
            End If
        End If
        Return ""
    End Function

    Private Sub ShowGraph(ByVal ChartType As Integer)
        Try            
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then

                    With AxAdvChart
                        If strLicensekey <> "" AndAlso strExtLicensekey <> "" Then
                            .LicenseKey = strLicensekey
                            .ExtLicenseKey = strExtLicensekey
                        End If
                        
                        .GrowthChartType = ChartType

                        .FirstName = dt.Rows(0)("sFirstName").ToString()
                        .LastName = dt.Rows(0)("sLastName").ToString()
                        .Gender = dt.Rows(0)("Gender")

                        Dim inx As Integer
                        .RemoveAllData()       'by sudhir 20081110

                        '' SHOW PERCENTILE SETTING ''
                        Select Case _enumPercetile
                            Case enumGrowthChartPercentile.ShowPercentile
                                .ShowPercentile = 1
                                .ShowPercentilesOnMouseOver = 0
                            Case enumGrowthChartPercentile.ShowPercentileOnMouseHoover
                                .ShowPercentile = 0
                                .ShowPercentilesOnMouseOver = 1
                            Case enumGrowthChartPercentile.DontShowPercentile
                                .ShowPercentile = 0
                                .ShowPercentilesOnMouseOver = 0
                        End Select

                        Select Case ChartType
                            Case "1", "40" 'length-Weight vs Age'
                                For i As Integer = dt.Rows.Count - 1 To 0 Step -1  'by sudhir 20081110
                                    inx = CLng(.AddNewData())
                                    Call .SetAge(inx, CType(dt.Rows(i)("AGE"), Single) / 30.4375) '30.4375 is WHO Standard To Convert Days into Months. by sudhir 20081110
                                    If Not IsDBNull(dt.Rows(i)("dWeightinKg")) Then 'by sudhir 20081110
                                        Call .SetWeight(inx, CType(dt.Rows(i)("dWeightinKg"), Single))
                                    End If
                                    Call .SetHeight(inx, GetFtInchTocm(dt.Rows(i)("sHeight")))
                                    If Not IsDBNull(dt.Rows(i)("dHeadCirCumferance")) Then 'by sudhir 20081110
                                        Call .SetHeadCir(inx, CType(dt.Rows(i)("dHeadCirCumferance"), Single))
                                    End If
                                    Call .SetTestDate(inx, CDate(dt.Rows(i)("dtVitalDate")))
                                    Call .SetComments(inx, FormatAge(dt.Rows(i)("dtDOB"), dt.Rows(i)("dtVitalDate")))   'by sudhir 20081110
                                Next
                                Call .UpdateGraphView()
                            Case "2", "41" 'Head Circum - Wight Vs Age'
                                For i As Integer = dt.Rows.Count - 1 To 0 Step -1  'by sudhir 20081110
                                    inx = CLng(.AddNewData())
                                    Call .SetAge(inx, CType(dt.Rows(i)("AGE"), Single) / 30.4375) '30.4375 is WHO Standard To Convert Days into Months. by sudhir 20081110
                                    If Not IsDBNull(dt.Rows(i)("dWeightinKg")) Then 'by sudhir 20081110
                                        Call .SetWeight(inx, CType(dt.Rows(i)("dWeightinKg"), Single))
                                    End If
                                    Call .SetHeight(inx, GetFtInchTocm(dt.Rows(i)("sHeight")))
                                    If Not IsDBNull(dt.Rows(i)("dHeadCirCumferance")) Then 'by sudhir 20081110
                                        Call .SetHeadCir(inx, CType(dt.Rows(i)("dHeadCirCumferance"), Single))
                                    End If
                                    Call .SetTestDate(inx, CDate(dt.Rows(i)("dtVitalDate")))
                                    Call .SetComments(inx, FormatAge(dt.Rows(i)("dtDOB"), dt.Rows(i)("dtVitalDate")))   'by sudhir 20081110
                                Next
                                Call .UpdateGraphView()
                            Case "3", "18" 'BMI Vs Age'
                                For i As Integer = dt.Rows.Count - 1 To 0 Step -1  'by sudhir 20081110
                                    inx = CLng(.AddNewData())
                                    Call .SetAge(inx, CType(dt.Rows(i)("AGE"), Single) / 30.4375) '30.4375 is WHO Standard To Convert Days into Months. by sudhir 20081110
                                    If Not IsDBNull(dt.Rows(i)("dWeightinKg")) Then 'by sudhir 20081110
                                        Call .SetWeight(inx, CType(dt.Rows(i)("dWeightinKg"), Single))
                                    End If
                                    Call .SetHeight(inx, GetFtInchTocm(dt.Rows(i)("sHeight")))
                                    Call .SetTestDate(inx, CDate(dt.Rows(i)("dtVitalDate")))
                                    Call .SetComments(inx, FormatAge(dt.Rows(i)("dtDOB"), dt.Rows(i)("dtVitalDate")))   'by sudhir 20081110
                                Next
                                Call .UpdateGraphView()
                            Case "4" 'Stature-Weight Vs Age'
                                For i As Integer = dt.Rows.Count - 1 To 0 Step -1  'by sudhir 20081110
                                    inx = CLng(.AddNewData())
                                    Call .SetAge(inx, CType(dt.Rows(i)("AGE"), Single) / 30.4375) '30.4375 is WHO Standard To Convert Days into Months. by sudhir 20081110
                                    If Not IsDBNull(dt.Rows(i)("dWeightinKg")) Then 'by sudhir 20081110
                                        Call .SetWeight(inx, CType(dt.Rows(i)("dWeightinKg"), Single))
                                    End If
                                    Call .SetHeight(inx, GetFtInchTocm(dt.Rows(i)("sHeight")))
                                    Call .SetTestDate(inx, CDate(dt.Rows(i)("dtVitalDate")))
                                    Call .SetComments(inx, FormatAge(dt.Rows(i)("dtDOB"), dt.Rows(i)("dtVitalDate")))   'by sudhir 20081110
                                Next
                                .UpdateGraphView()

                            Case "5" 'Height - Stature'
                                For i As Integer = dt.Rows.Count - 1 To 0 Step -1  'by sudhir 20081110
                                    inx = CLng(.AddNewData())
                                    Call .SetAge(inx, CType(dt.Rows(i)("AGE"), Single) / 30.4375) '30.4375 is WHO Standard To Convert Days into Months. by sudhir 20081110
                                    If Not IsDBNull(dt.Rows(i)("dWeightinKg")) Then 'by sudhir 20081110
                                        Call .SetWeight(inx, CType(dt.Rows(i)("dWeightinKg"), Single))
                                    End If
                                    Call .SetHeight(inx, GetFtInchTocm(dt.Rows(i)("sHeight")))
                                    Call .SetTestDate(inx, CDate(dt.Rows(i)("dtVitalDate")))
                                    Call .SetComments(inx, FormatAge(dt.Rows(i)("dtDOB"), dt.Rows(i)("dtVitalDate")))   'by sudhir 20081110
                                Next
                                .UpdateGraphView()
                            Case "33"  ''1 to 36 months with Down Syndrome: Length-for-age and Weight-for-age
                                For i As Integer = dt.Rows.Count - 1 To 0 Step -1
                                    inx = CLng(.AddNewData())
                                    Call .SetAge(inx, CType(dt.Rows(i)("AGE"), Single) / 30.4375) '30.4375 is WHO Standard To Convert Days into Months. by sudhir 20081110
                                    If Not IsDBNull(dt.Rows(i)("dWeightinKg")) Then 'by sudhir 20081110
                                        Call .SetWeight(inx, CType(dt.Rows(i)("dWeightinKg"), Single))
                                    End If
                                    Call .SetHeight(inx, GetFtInchTocm(dt.Rows(i)("sHeight")))
                                    Call .SetTestDate(inx, CDate(dt.Rows(i)("dtVitalDate")))
                                    Call .SetComments(inx, FormatAge(dt.Rows(i)("dtDOB"), dt.Rows(i)("dtVitalDate")))   'by sudhir 20081110
                                Next
                                .UpdateGraphView()
                            Case "34" ''2 to 18 years with Down Syndrome: Stature-for-age and Weight-for-age
                                For i As Integer = dt.Rows.Count - 1 To 0 Step -1
                                    inx = CLng(.AddNewData())
                                    Call .SetAge(inx, CType(dt.Rows(i)("AGE"), Single) / 30.4375) '30.4375 is WHO Standard To Convert Days into Months. by sudhir 20081110
                                    If Not IsDBNull(dt.Rows(i)("dWeightinKg")) Then 'by sudhir 20081110
                                        Call .SetWeight(inx, CType(dt.Rows(i)("dWeightinKg"), Single))
                                    End If
                                    Call .SetHeight(inx, GetFtInchTocm(dt.Rows(i)("sHeight")))
                                    Call .SetTestDate(inx, CDate(dt.Rows(i)("dtVitalDate")))
                                    Call .SetComments(inx, FormatAge(dt.Rows(i)("dtDOB"), dt.Rows(i)("dtVitalDate")))   'by sudhir 20081110
                                Next
                                .UpdateGraphView()
                            Case "35" ''Birth to 36 months with Down Syndrome: Head circumference-for-age
                                For i As Integer = dt.Rows.Count - 1 To 0 Step -1
                                    inx = CLng(.AddNewData())
                                    Call .SetAge(inx, CType(dt.Rows(i)("AGE"), Single) / 30.4375) '30.4375 is WHO Standard To Convert Days into Months. by sudhir 20081110
                                    If Not IsDBNull(dt.Rows(i)("dWeightinKg")) Then 'by sudhir 20081110
                                        Call .SetWeight(inx, CType(dt.Rows(i)("dWeightinKg"), Single))
                                    End If
                                    If Not IsDBNull(dt.Rows(i)("dHeadCirCumferance")) Then 'by sudhir 20081110
                                        Call .SetHeadCir(inx, CType(dt.Rows(i)("dHeadCirCumferance"), Single))
                                    End If
                                    Call .SetHeight(inx, GetFtInchTocm(dt.Rows(i)("sHeight")))
                                    Call .SetTestDate(inx, CDate(dt.Rows(i)("dtVitalDate")))
                                    Call .SetComments(inx, FormatAge(dt.Rows(i)("dtDOB"), dt.Rows(i)("dtVitalDate")))   'by sudhir 20081110
                                Next
                                .UpdateGraphView()
                        End Select
                    End With
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetFtInchTocm(ByVal strHeight As String) As Single
        Try
            If (strHeight = "") Then
                Return Nothing
            End If
            Dim temp() As String
            Dim subTemp As String
            Dim HeightInCM As Single
            temp = Split(strHeight, "'", 2)
            If temp.Length = 2 Then
                subTemp = Replace(temp.GetValue(1), "''", "")

                If subTemp.Trim <> "" Then
                    HeightInCM = CType((temp.GetValue(0) * 30.48) + (subTemp * 2.54), Single)
                Else
                    HeightInCM = CType((temp.GetValue(0) * 30.48), Single)
                End If
            ElseIf temp.Length = 1 Then
                HeightInCM = CType((temp.GetValue(0) * 30.48), Single)
            Else
                HeightInCM = 0
            End If
            Return HeightInCM
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Private Sub frmAdvanceGraph_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.ShowGraph, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub frmAdvanceGraph_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Set_PatientDetailStrip()


            If gstrAdvChartLicensekey <> "" AndAlso gstrAdvChartExtLicensekey <> "" Then
                oclsEncryption = New clsencryption
                strLicensekey = oclsEncryption.DecryptFromBase64String(gstrAdvChartLicensekey, "123456789")
                strExtLicensekey = oclsEncryption.DecryptFromBase64String(gstrAdvChartExtLicensekey, "123456789")
            End If

            ''sudhir 20081112
            AxAdvChart.Height = HeightWidth
            AxAdvChart.Width = 1100  ''GrowthChart Width is always constant to maintain center position
            
            SetChartPanelSize()
            ''end sudhir
            GetVitals()
            _enumPercetile = GetPercentileSetting()
            ShowGraph(1)

            Me.ShowIcon = True
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oclsEncryption) Then
                oclsEncryption.Dispose()
                oclsEncryption = Nothing
            End If
        End Try

    End Sub

    Private Sub Set_PatientDetailStrip()

        If (IsNothing(gloUC_PatientStrip1) = False) Then
            Me.Controls.Remove(gloUC_PatientStrip1)
            gloUC_PatientStrip1.Dispose()
            gloUC_PatientStrip1 = Nothing
        End If
        gloUC_PatientStrip1 = New gloUC_PatientStrip


        With gloUC_PatientStrip1
            .Dock = DockStyle.Top
            .Padding = New Padding(0, 0, 0, 3)
            .ShowDetail(_PatientID, gloUC_PatientStrip.enumFormName.VitalGraph)
            .MinimizeStrip = True
            .SendToBack()
            pnlMain.BringToFront()

        End With

        pnlMain.Controls.Add(gloUC_PatientStrip1)

    End Sub

    Dim dt As DataTable = Nothing
    Public Function GetVitals() As DataTable
        Dim Con As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Try
            Con = New SqlConnection
            Con.ConnectionString = GetConnectionString()
            cmd = New SqlCommand
            With cmd
                .Connection = Con
                .CommandType = CommandType.StoredProcedure
                .CommandText = "GetPatientVitals"
            End With
            cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            cmd.Parameters(0).Value = _PatientID
            da = New SqlDataAdapter(cmd)
            If (IsNothing(dt) = False) Then
                dt.Dispose()
                dt = Nothing
            End If
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If da IsNot Nothing Then

                da.Dispose()
                da = Nothing
            End If
            If Con IsNot Nothing Then
                Con.Close()
                Con.Dispose()
                Con = Nothing
            End If
         
        End Try
    End Function

    'by sudhir 20081112
    Private Sub VScrollBar_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles VScrollBar.Scroll
        Try
            Dim Vpt As New System.Drawing.Point
            Vpt.X = pnlChart.Location.X
            Vpt.Y = Val("-" & VScrollBar.Value)
            pnlChart.Location = Vpt
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub HScrollBar_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles HScrollBar.Scroll
        Try
            Dim Hpt As New System.Drawing.Point
            Hpt.X = Val("-" & HScrollBar.Value)
            Hpt.Y = pnlChart.Location.Y
            pnlChart.Location = Hpt
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub pnlBackground_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlBackground.SizeChanged
        ''by sudhir 20081112
        SetChartPanelSize()
        SetScrollBarValues()
        Dim pt As New System.Drawing.Point
        pt.X = Val("-" & HScrollBar.Value)
        pt.Y = Val("-" & VScrollBar.Value)
        pnlChart.Location = pt  ''Synchronize location while Minimizing & Maximizing
    End Sub
    'End Sudhir

    ''by sudhir 20081113 event to Scroll GrowthChart 
    Private Sub frmAdvanceGraph_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseWheel
        Try
            Dim P As Point = PointToClient(Cursor.Position)
            Dim myHeight As Integer = If(IsNothing(gloUC_PatientStrip1), 0, gloUC_PatientStrip1.Height)

            ''Following condition to scroll only if MousePointer is on GrowthChart
            If P.X > 0 AndAlso P.X < pnlBackground.Width AndAlso P.Y > (myHeight + tls_Chart.Height) AndAlso P.Y < (pnlBackground.Height + myHeight + tls_Chart.Height) Then
                If e.Delta > 0 Then
                    If VScrollBar.Value + (-(e.Delta) / 5) > 0 Then
                        VScrollBar.Value = VScrollBar.Value + (-(e.Delta) / 5)
                    Else
                        VScrollBar.Value = 0
                    End If
                    VScrollBar_Scroll(Nothing, Nothing)
                Else
                    If VScrollBar.Value - (e.Delta / 5) < VScrollBar.Maximum Then
                        VScrollBar.Value = VScrollBar.Value - (e.Delta / 5)
                    Else
                        VScrollBar.Value = VScrollBar.Maximum
                    End If
                    VScrollBar_Scroll(Nothing, Nothing)
                End If
            End If
        Catch ex As ArgumentOutOfRangeException
            If e.Delta < 0 Then
                VScrollBar.Value = VScrollBar.Maximum
            Else
                VScrollBar.Value = 0
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Function GetPercentileSetting() As enumGrowthChartPercentile
        Dim Con As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing

        Try
            Con = New SqlConnection(GetConnectionString)
            Dim sQuery As String = "SELECT sSettingsValue FROM Settings WHERE sSettingsName = 'Advanced Growth Chart Percentile' AND nClinicID = " & gnClinicID & " "
            cmd = New SqlCommand(sQuery, Con)
            Dim oResult As Object
            Con.Open()
            oResult = cmd.ExecuteScalar()
            Con.Close()

            If oResult Is Nothing Then
                Return enumGrowthChartPercentile.DontShowPercentile
            Else
                Return CType(oResult, enumGrowthChartPercentile)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return enumGrowthChartPercentile.DontShowPercentile
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Con IsNot Nothing Then
                Con.Close()
                Con.Dispose()
                Con = Nothing
            End If
        End Try
    End Function


    Public Sub New(ByVal PatientID As Long)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _PatientID = PatientID
    End Sub

    ''' <summary>
    ''' Property written for Patient Context
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return _PatientID  'Curent patient variable(Local variable) for this module 
        End Get
    End Property

    Private Sub tlsmnu_lenage_weigage_Click(sender As System.Object, e As System.EventArgs) Handles tlsmnu_lenage_weigage.Click
        ''Length-for-age and Weight-for-age 33
        ShowGraph(33)
        AxAdvChart.Height = HeightWidth
        SetChartPanelSize()
    End Sub

  
    Private Sub tlsmnu_statageandweigage_Click(sender As System.Object, e As System.EventArgs) Handles tlsmnu_statageandweigage.Click
        '' Stature-for-age and Weight-for-age 34
        ShowGraph(34)
        AxAdvChart.Height = HeightWidth
        SetChartPanelSize()
    End Sub

    Private Sub tlsmnu_Hedacircage_Click(sender As System.Object, e As System.EventArgs) Handles tlsmnu_Hedacircage.Click
        ''Head circumference-for-age 35
        ShowGraph(35)
        AxAdvChart.Height = HeightWidth
        SetChartPanelSize()
    End Sub

    Private Sub tlsmnu_WHOBMIforAge_Click(sender As System.Object, e As System.EventArgs) Handles tlsmnu_WHOBMIforAge.Click
        ShowGraph(18)
        If HeightWidth > 700 Then
            AxAdvChart.Height = HeightWidth - 100
        End If
        SetChartPanelSize()        
    End Sub

    Private Sub tlsmnu_CDCBMIforAge_Click(sender As System.Object, e As System.EventArgs) Handles tlsmnu_CDCBMIforAge.Click
        ShowGraph(3)
        AxAdvChart.Height = HeightWidth
        SetChartPanelSize()
    End Sub

    Private Sub tlsmnu_CDCHeadcircWeightVsAge_Click(sender As System.Object, e As System.EventArgs) Handles tlsmnu_CDCHeadcircWeightVsAge.Click
        ShowGraph(2)
        AxAdvChart.Height = HeightWidth
        SetChartPanelSize()
    End Sub

    Private Sub tlsmnu_WHOHeadcircWeightVsAge_Click(sender As System.Object, e As System.EventArgs) Handles tlsmnu_WHOHeadcircWeightVsAge.Click
        ShowGraph(41)
        AxAdvChart.Height = HeightWidth
        SetChartPanelSize()
    End Sub

    Private Sub tlsmnu_CDClenweiVsAge_Click(sender As System.Object, e As System.EventArgs) Handles tlsmnu_CDClenweiVsAge.Click
        ShowGraph(1)
        AxAdvChart.Height = HeightWidth
        SetChartPanelSize()
    End Sub

    Private Sub tlsmnu_WHOlenweiVsAge_Click(sender As System.Object, e As System.EventArgs) Handles tlsmnu_WHOlenweiVsAge.Click
        ShowGraph(40)
        AxAdvChart.Height = HeightWidth
        SetChartPanelSize()
    End Sub
End Class