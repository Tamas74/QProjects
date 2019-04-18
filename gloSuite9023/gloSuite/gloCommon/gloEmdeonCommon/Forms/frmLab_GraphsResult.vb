Imports AxMSChart20Lib
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Drawing.Printing
Imports System.Drawing.Imaging
Imports System.Windows.Forms
imports System.Drawing  


Public Class frmLab_GraphsResult
    Inherits System.Windows.Forms.Form

    Private Conn As SqlConnection
    Private Dv As DataView
    Private Cmd As SqlCommand
    Public MinMaxdt_renge As DataTable

    Dim fromDate As DateTime
    Dim toDate As DateTime
    Dim nTest As Int64
    Dim nResult As Int64
    Dim npatientIds As Int64
    Dim _strTestName As String = ""
    Dim _strResultname As String = ""
    Dim _dt_MinMax As DataTable
    Dim _dt_AllPlotValues As DataTable
    Dim dt_MinMax As DataTable
    Dim _bBtnCriteria As Boolean
    Dim _bFromLabs As Boolean
    Dim Pcte As System.Drawing.Image = Nothing
    Dim _Min As Integer
    Dim _Max As Integer
    Dim arrResultList As New ArrayList
    Dim gstrMessageBoxCaption As String = ""
    Dim gnPatientID As Int64
    Dim _ConnectionString As String

    '' added by Abhijeet on date 20100514
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings


#Region " Patient Details Strip "
    Private WithEvents gloUC_PatientStrip1 As gloUserControlLibrary.gloUC_PatientStrip

    Private Sub GloUC_PatientStrip1_ControlSizeChanged() Handles gloUC_PatientStrip1.ControlSizeChanged
        Try
            '' pnlPatientHeader.Height = gloUC_PatientStrip1.Height
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Set_PatientDetailStrip()
        ' '' Add Patient Details Control
        gloUC_PatientStrip1 = New gloUserControlLibrary.gloUC_PatientStrip

        With gloUC_PatientStrip1
            .Dock = DockStyle.Top
            .Padding = New Padding(3, 0, 3, 0)
            '' Pass Paarameters Type of Form
            .ShowDetail(gnPatientID)
            .SendToBack()
        End With
        Me.Controls.Add(gloUC_PatientStrip1)
        '''''
        'C1FlexTransaction.BringToFront()
        pnlMainAll.BringToFront()
        pnlTopDtls.BringToFront()
        pnlMainAll.BringToFront()
        pnl_ToolStrip.SendToBack()
        chtResultsGraph.BringToFront()
        gloUC_PatientStrip1.DTPEnabled = False

        '' Hide Previous Patient Details
        'pnlTop.Visible = False
        ' ''
    End Sub

#End Region

    Public Sub New(ByVal ConnectionString As String, ByVal dtFrom As DateTime, ByVal DtTo As DateTime, Optional ByVal cmbTest As Int64 = 0, Optional ByVal cmbResult As Int64 = 0, Optional ByVal patientId As Int64 = 0, Optional ByVal strTestName As String = "", Optional ByVal strResultName As String = "", Optional ByVal dt_AllPlotValues As DataTable = Nothing, Optional ByVal dt_MinMaxFr As DataTable = Nothing, Optional ByVal bBtnCriteria As Boolean = True, Optional ByVal bFromLabs As Boolean = False, Optional ByVal Min As Decimal = 0, Optional ByVal Max As Decimal = 0)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        fromDate = dtFrom
        toDate = DtTo
        nTest = cmbTest
        nResult = cmbResult
        npatientIds = patientId
        _strTestName = strTestName
        _strResultname = strResultName
        _dt_AllPlotValues = dt_AllPlotValues
        _dt_MinMax = dt_MinMaxFr
        _bBtnCriteria = bBtnCriteria
        _bFromLabs = bFromLabs
        _Min = Min
        _Max = Max
        gnPatientID = patientId
        ' Add any initialization after the InitializeComponent() call.
        Dim sqlconn As String
        _ConnectionString = ConnectionString
        sqlconn = _ConnectionString
        Conn = New System.Data.SqlClient.SqlConnection(sqlconn)
        PrintPro1.Licensing.UnlockRuntime(2052484428, 498072154, 1464682479, 5619)

        '' Code by : Abhijeet Farkande on date : 20100514
        '' changes : accessing the message box caption from app setting 
        If gstrMessageBoxCaption = "" Then
            If Not IsNothing(appSettings) Then
                If Not IsNothing(appSettings("MessageBOXCaption")) Then
                    If appSettings("") <> "" Then
                        gstrMessageBoxCaption = appSettings("MessageBOXCaption").ToString()
                    Else
                        gstrMessageBoxCaption = "gloEMR"
                    End If
                Else
                    gstrMessageBoxCaption = "gloEMR"
                End If
            Else
                gstrMessageBoxCaption = "gloEMR"
            End If
        End If
        '' End of code by Abhijeet for accessing message box caption                
    End Sub

    '' COMMENT BY SUDHIR 20090618 ''
    'Public Sub DrawGraph()

    '    Dim dt As DataTable




    '    dt = New DataTable

    '    dt = _dt_AllPlotValues

    '    Dim ResultCount As Integer = 0
    '    Dim i As Integer '= 20

    '    '' string data where Date for Result is store
    '    Dim DateCollection(dt.Rows.Count - 1, 2) As String  ''''Set Label for X - axis


    '    '' string data where Name for Result is store
    '    Dim ColumnCollection(dt.Rows.Count - 1, 2) As String ''''Set Column Name.

    '    ''''Set Graph Data like Patient Name and code etc.
    '    Dim strPatientInfo As String = Space(5) & " Patient Code:" & Space(1) & gloUC_PatientStrip1.PatientCode & Space(5) & "Patient Name:" & Space(1) & gloUC_PatientStrip1.PatientName & Space(5) & "DOB:" & Space(1) & gloUC_PatientStrip1.PatientDateOfBirth & Space(5) & "Phone No.:" & Space(1) & gloUC_PatientStrip1.PatientPhone & Space(5)
    '    chtResultsGraph.Footnote.Location.LocationType = MSChart20Lib.VtChLocationType.VtChLocationTypeTop
    '    chtResultsGraph.Footnote.Text = strPatientInfo

    '    ' Graph Title
    '    chtResultsGraph.TitleText = "Period Vs Value"
    '    chtResultsGraph.Legend.Location.LocationType = MSChart20Lib.VtChLocationType.VtChLocationTypeBottom
    '    chtResultsGraph.Legend.Location.Visible = True

    '    Try
    '        With chtResultsGraph
    '            .chartType = MSChart20Lib.VtChChartType.VtChChartType2dCombination


    '            DataGrid1.DataSource = Nothing
    '            DataGrid1.DataSource = dt

    '            'Axis Labels for the Graph
    '            chtResultsGraph.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisTitle.Text = " Period "
    '            chtResultsGraph.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).AxisTitle.Text = " Value "

    '            Dim dv As DataView
    '            dv = New DataView(dt)
    '            dv.Sort = "ResultName"




    '            ''''Take only Date as Column with new DataTable For sorting
    '            Dim dt_date As New DataTable
    '            dt_date.Columns.Add(New DataColumn("Date", GetType(Date)))
    '            Dim r As DataRow
    '            For j As Integer = 0 To dt.Rows.Count - 1
    '                r = dt_date.NewRow()
    '                r.Item("Date") = dt.Rows(j)(0)
    '                dt_date.Rows.Add(r)
    '            Next

    '            Dim dvtemp As DataView
    '            dvtemp = New DataView(dt_date)
    '            dvtemp.Sort = "Date asc"
    '            Dim temp As Date

    '            ''''Fill Date with Sorted Collection
    '            Dim cnt As Integer = 0
    '            For k As Integer = 0 To dvtemp.Table.Rows.Count - 1
    '                If temp <> dvtemp.Item(k)(0) Then
    '                    temp = dvtemp.Item(k)(0)
    '                    DateCollection(cnt, 1) = Format(dvtemp.Item(k)(0), "MM/dd/yyyy")
    '                    cnt = cnt + 1
    '                End If
    '            Next

    '            ''''Fill DataColletion End 


    '            Dim result As String = ""

    '            arrResultList = New ArrayList

    '            ''''Take each Result in Seprate DataTable and store it in Arraylist
    '            For i = 0 To dv.Table.Rows.Count - 1
    '                If result <> dv.Item(i)("ResultName").ToString Then
    '                    result = dv.Item(i)("ResultName").ToString()
    '                    FilterDate(result, dv, dt)
    '                End If
    '            Next

    '            '''' Take array which bind to Graph
    '            Dim Ary(cnt - 1, arrResultList.Count - 1) As String

    '            Dim tmp As String = ""
    '            Dim nRow As Integer = 0
    '            Dim nCol As Integer = 0

    '            ''''Fill Array with selected value for the Result to Plot Graph
    '            For j As Integer = 0 To dv.Table.Rows.Count - 1
    '                tmp = dv.Item(j)("ResultName").ToString()
    '                If j = 0 Then
    '                    nRow = 0 : nCol = 0
    '                    Ary(nRow, nCol) = dv.Item(j)("Value")
    '                ElseIf tmp = dv.Item(j - 1)("ResultName").ToString Then
    '                    nRow = nRow + 1 : nCol = nCol
    '                    Ary(nRow, nCol) = dv.Item(j)("Value")
    '                ElseIf tmp <> dv.Item(j - 1)("ResultName").ToString Then
    '                    nRow = 0 : nCol = nCol + 1
    '                    Ary(nRow, nCol) = dv.Item(j)("Value")
    '                End If
    '            Next

    '            cnt = 0
    '            Dim tempStr As String = ""
    '            For k As Integer = 0 To dv.Table.Rows.Count - 1
    '                If tempStr <> dv.Item(k)("ResultName") Then
    '                    tempStr = dv.Item(k)("ResultName")
    '                    ColumnCollection(cnt, 1) = dv.Item(k)("ResultName")
    '                    cnt = cnt + 1
    '                End If

    '            Next



    '            'set data to draw chart
    '            .ChartData = CType(Ary, Object) ' CType(arrResults, Object)

    '            Dim nRed As Integer = 50
    '            Dim nGreen As Integer = 50
    '            Dim nBlue As Integer = 50
    '            ' set graph styles For Series Collection
    '            For i = 0 To nCol

    '                .Plot.SeriesCollection(i + 1).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
    '                .Plot.SeriesCollection(i + 1).Select()
    '                .Plot.SeriesCollection(i + 1).ShowLine = True
    '                '.Plot.SeriesCollection(i + 1).DataPoints(-1).Marker.FillColor.Set(0, 0, 0)
    '                .Plot.SeriesCollection(i + 1).DataPoints(-1).Brush.Style = MSChart20Lib.VtBrushStyle.VtBrushStyleSolid
    '                .Plot.SeriesCollection(i + 1).Pen.Width = 25
    '                .Plot.SeriesCollection(i + 1).DataPoints(-1).Marker.FillColor.Automatic = True
    '                .Plot.SeriesCollection(i + 1).SeriesMarker.Auto = True
    '                .Plot.SeriesCollection(i + 1).DataPoints(-1).Marker.Visible = True
    '                .Plot.SeriesCollection(i + 1).DataPoints(-1).Marker.Size = 1
    '                .Plot.SeriesCollection(i + 1).DataPoints(-1).Marker.Style = MSChart20Lib.VtMarkerStyle.VtMarkerStyleFilledSquare
    '                '.Plot.SeriesCollection(i + 1).DataPoints(-1).Brush.FillColor.Set(255, 0, 0)


    '                If i = 0 Then
    '                    .Plot.SeriesCollection(i + 1).LegendText = ColumnCollection(i, 1)
    '                    .Plot.SeriesCollection(i + 1).DataPoints(-1).Brush.FillColor.Set(0, 0, 255) '''' Set Bydefault Blue color for First Series
    '                    '.Plot.SeriesCollection(i + 1).DataPoints(-1).Marker.FillColor.Automatic = False
    '                    ' .Plot.SeriesCollection(i + 1).DataPoints(-1).Marker.FillColor.Set(0, 0, 255)
    '                ElseIf i = 1 Then
    '                    .Plot.SeriesCollection(i + 1).LegendText = ColumnCollection(i, 1)
    '                    .Plot.SeriesCollection(i + 1).DataPoints(-1).Brush.FillColor.Set(0, 255, 0) '''' Set Bydefault Green color for First Series
    '                    .Plot.SeriesCollection(i + 1).DataPoints(-1).Marker.FillColor.Automatic = False
    '                    .Plot.SeriesCollection(i + 1).DataPoints(-1).Marker.FillColor.Set(0, 255, 0)
    '                ElseIf i = 2 Then
    '                    .Plot.SeriesCollection(i + 1).LegendText = ColumnCollection(i, 1)
    '                    .Plot.SeriesCollection(i + 1).DataPoints(-1).Brush.FillColor.Set(255, 0, 0) '''' Set Bydefault Red color for First Series
    '                    ' .Plot.SeriesCollection(i + 1).DataPoints(-1).Marker.FillColor.Automatic = False
    '                    '.Plot.SeriesCollection(i + 1).DataPoints(-1).Marker.FillColor.Set(255, 0, 0)
    '                Else
    '                    nRed = nRed + 80
    '                    nGreen = nGreen + 20
    '                    nBlue = nBlue + 40
    '                    .Plot.SeriesCollection(i + 1).LegendText = ColumnCollection(i, 1)
    '                    .Plot.SeriesCollection(i + 1).DataPoints(-1).Brush.FillColor.Set(nRed, nGreen, nBlue)
    '                    '.Plot.SeriesCollection(i + 1).DataPoints(-1).Marker.FillColor.Automatic = False
    '                    '.Plot.SeriesCollection(i + 1).DataPoints(-1).Marker.FillColor.Set(nRed, nGreen, nBlue)

    '                End If

    '            Next

    '            'For cnt = 0 To nCol

    '            '    If cnt = 0 Then

    '            '        .Plot.SeriesCollection(cnt + 1).Pen.VtColor.Set(0, 0, 0)
    '            '    ElseIf cnt = 1 Then

    '            '        .Plot.SeriesCollection(cnt + 1).Pen.VtColor.Set(0, 0, 0)
    '            '    ElseIf cnt = 2 Then

    '            '        .Plot.SeriesCollection(cnt + 1).Pen.VtColor.Set(0, 0, 0)
    '            '    Else

    '            '        .Plot.SeriesCollection(cnt + 1).Pen.VtColor.Set(0, 0, 0)
    '            '    End If

    '            'Next

    '            'set the display properties of the graphs curve.
    '            'Dim cnt As Integer
    '            'For cnt = 1 To 3 ' count 12 because user can add more values to it if any modiifcation will be done 
    '            '    .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
    '            '    .Plot.SeriesCollection(cnt).Pen.Width = 22
    '            '    ' set color to different arrays
    '            '    If cnt = 0 Then
    '            '        .Plot.SeriesCollection(cnt).Pen.VtColor.Set(0, 155, 0)
    '            '    ElseIf cnt = 1 Then
    '            '        .Plot.SeriesCollection(cnt).Pen.VtColor.Set(200, 50, 0) 'MAX
    '            '        '.Plot.SeriesCollection(cnt).Pen.Width = 50 '''' Pramod
    '            '    ElseIf cnt = 2 Then
    '            '        .Plot.SeriesCollection(cnt).Pen.VtColor.Set(0, 155, 0) '
    '            '    ElseIf cnt = 3 Then
    '            '        .Plot.SeriesCollection(cnt).Pen.VtColor.Set(0, 0, 0) ' Actual i.e. Star
    '            '    ElseIf cnt = 4 Then
    '            '        .Plot.SeriesCollection(cnt).Pen.VtColor.Set(0, 155, 0) ' MIN
    '            '        '.Plot.SeriesCollection(cnt).Pen.Width = 50 '''' Pramod
    '            '    Else
    '            '        .Plot.SeriesCollection(cnt).Pen.VtColor.Set(0, 0, 255)
    '            '        .Plot.SeriesCollection(cnt).Pen.Width = 50
    '            '    End If
    '            '    '.Plot.SeriesCollection(cnt).DataPoints(-1).Marker.Style = MSChart20Lib.VtMarkerStyle.VtMarkerStyleFilledCircle
    '            'Next

    '            ' set the graph properties for the y axis.                
    '            '.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MajorDivision = 10
    '            '.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MinorDivision = 1
    '            '.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerLabel = 1
    '            '.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerTick = 1

    '            ''set the label of x axis.
    '            For i = 1 To .RowCount
    '                If DateCollection(i - 1, 1) <> Nothing Then
    '                    .Row = i
    '                    .RowLabel = DateCollection(i - 1, 1)
    '                End If
    '            Next

    '        End With
    '    Catch ex As Exception
    '        '   MsgBox(ex.ToString)
    '        Throw ex
    '    End Try
    'End Sub

    Public Sub DrawGraph()

        Dim dt As DataTable
        dt = New DataTable

        dt = _dt_AllPlotValues

        Dim ResultCount As Integer = 0
        Dim i As Integer '= 20

        '' string data where Date for Result is store
        Dim DateCollection(dt.Rows.Count - 1, 2) As String  ''''Set Label for X - axis


        '' string data where Name for Result is store
        Dim ColumnCollection(dt.Rows.Count - 1, 2) As String ''''Set Column Name.

        ''''Set Graph Data like Patient Name and code etc.
        Dim strPatientInfo As String = Space(5) & " Patient Code:" & Space(1) & gloUC_PatientStrip1.PatientCode & Space(5) & "Patient Name:" & Space(1) & gloUC_PatientStrip1.PatientName & Space(5) & "DOB:" & Space(1) & gloUC_PatientStrip1.PatientDateOfBirth & Space(5) & "Phone No.:" & Space(1) & gloUC_PatientStrip1.PatientPhone & Space(5)
        chtResultsGraph.Footnote.Location.LocationType = MSChart20Lib.VtChLocationType.VtChLocationTypeTop
        chtResultsGraph.Footnote.Text = strPatientInfo

        ' Graph Title
        chtResultsGraph.TitleText = "Period Vs Value"
        chtResultsGraph.Legend.Location.LocationType = MSChart20Lib.VtChLocationType.VtChLocationTypeBottom
        chtResultsGraph.Legend.Location.Visible = True

        Try
            With chtResultsGraph
                .chartType = MSChart20Lib.VtChChartType.VtChChartType2dCombination


                DataGrid1.DataSource = Nothing
                DataGrid1.DataSource = dt

                'Axis Labels for the Graph
                chtResultsGraph.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisTitle.Text = " Period "
                chtResultsGraph.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).AxisTitle.Text = " Value "

                Dim dv As DataView
                dv = New DataView(dt)
                dv.Sort = "ResultName"




                ''''Take only Date as Column with new DataTable For sorting
                Dim dt_date As New DataTable
                dt_date.Columns.Add(New DataColumn("Date", GetType(Date)))
                Dim r As DataRow
                For j As Integer = 0 To dt.Rows.Count - 1
                    r = dt_date.NewRow()
                    r.Item("Date") = dt.Rows(j)(0)
                    dt_date.Rows.Add(r)
                Next

                Dim dvtemp As DataView
                dvtemp = New DataView(dt_date)
                dvtemp.Sort = "Date asc"
                Dim temp As Date

                ''''Fill Date with Sorted Collection
                Dim cnt As Integer = 0
                For k As Integer = 0 To dvtemp.Table.Rows.Count - 1
                    If temp <> dvtemp.Item(k)(0) Then
                        temp = dvtemp.Item(k)(0)
                        DateCollection(cnt, 1) = Format(dvtemp.Item(k)(0), "MM/dd/yyyy")
                        cnt = cnt + 1
                    End If
                Next

                ''''Fill DataColletion End 


                Dim result As String = ""

                arrResultList = New ArrayList

                ''''Take each Result in Separate DataTable and store it in Arraylist
                For i = 0 To dv.Table.Rows.Count - 1
                    If result <> dv.Item(i)("ResultName").ToString Then
                        result = dv.Item(i)("ResultName").ToString()
                        result = result.Replace("'", "''")
                        FilterDate(result, dv, dt)
                    End If
                Next

                '''' Take array which bind to Graph
                'Dim Ary(cnt - 1, arrResultList.Count - 1) As String
                Dim Ary(cnt - 1, dv.Table.Rows.Count - 1) As String

                Dim tmpResultName As String = ""
                Dim tmpResultDate As String = ""
                Dim nRow As Integer = 0
                Dim nCol As Integer = 0
                Dim ResultName As New ArrayList()
                Dim ResultDate As New ArrayList()
                ''''Fill Array with selected value for the Result to Plot Graph
                Dim dtTemp As New DataTable
                dtTemp = dv.Table
                For j As Integer = 0 To dtTemp.Rows.Count - 1
                    tmpResultName = dtTemp.Rows(j)("ResultName").ToString()
                    tmpResultDate = dtTemp.Rows(j)("Date").ToString()
                    Dim _ResultRepeat As Boolean = False
                    Dim _RepeatCol As Integer


                    If ResultName.Contains(dtTemp.Rows(j)("ResultName").ToString()) = False Then
                        ResultName.Add(dtTemp.Rows(j)("ResultName").ToString())
                        nCol = nCol + 1
                        'Code Start- Added on 20101115 by sanjog
                        'nRow = j + 1
                        'Code End- Added on 20101115 by sanjog
                    Else
                        _ResultRepeat = True
                        _RepeatCol = ResultName.IndexOf(dtTemp.Rows(j)("ResultName").ToString())
                        'Code Start- Added on 20101115 by sanjog
                        'nRow = nRow + 1
                        'Code End- Added on 20101115 by sanjog
                    End If

                    If ResultDate.Contains(dtTemp.Rows(j)("Date").ToString()) = False Then
                        ResultDate.Add(dtTemp.Rows(j)("Date").ToString())
                        nRow = nRow + 1
                    End If


                    If _ResultRepeat Then
                        Ary(nRow - 1, _RepeatCol) = dtTemp.Rows(j)("Value")
                    Else
                        Ary(nRow - 1, nCol - 1) = dtTemp.Rows(j)("Value")
                    End If
                Next

                cnt = 0
                Dim arrTemp As New ArrayList
                For k As Integer = 0 To dtTemp.Rows.Count - 1
                    If Not arrTemp.Contains(dtTemp.Rows(k)("ResultName")) Then
                        arrTemp.Add(dtTemp.Rows(k)("ResultName"))
                        ColumnCollection(cnt, 1) = dtTemp.Rows(k)("ResultName")
                        cnt = cnt + 1
                    End If
                Next

                'set data to draw chart
                .ChartData = CType(Ary, Object) ' CType(arrResults, Object)
                Dim nRed As Integer = 50
                Dim nGreen As Integer = 50
                Dim nBlue As Integer = 50
                ' set graph styles For Series Collection
                For i = 0 To nCol - 1

                    .Plot.SeriesCollection(i + 1).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                    .Plot.SeriesCollection(i + 1).Select()
                    .Plot.SeriesCollection(i + 1).ShowLine = True
                    .Plot.SeriesCollection(i + 1).DataPoints(-1).Brush.Style = MSChart20Lib.VtBrushStyle.VtBrushStyleSolid
                    .Plot.SeriesCollection(i + 1).Pen.Width = 25
                    .Plot.SeriesCollection(i + 1).DataPoints(-1).Marker.FillColor.Automatic = True
                    .Plot.SeriesCollection(i + 1).SeriesMarker.Auto = True
                    .Plot.SeriesCollection(i + 1).DataPoints(-1).Marker.Visible = True
                    .Plot.SeriesCollection(i + 1).DataPoints(-1).Marker.Size = 1
                    .Plot.SeriesCollection(i + 1).DataPoints(-1).Marker.Style = MSChart20Lib.VtMarkerStyle.VtMarkerStyleFilledSquare


                    If i = 0 Then
                        .Plot.SeriesCollection(i + 1).LegendText = ColumnCollection(i, 1)
                        .Plot.SeriesCollection(i + 1).DataPoints(-1).Brush.FillColor.Set(0, 0, 255) '''' Set Bydefault Blue color for First Series
                    ElseIf i = 1 Then
                        .Plot.SeriesCollection(i + 1).LegendText = ColumnCollection(i, 1)
                        .Plot.SeriesCollection(i + 1).DataPoints(-1).Brush.FillColor.Set(0, 255, 0) '''' Set Bydefault Green color for First Series
                        .Plot.SeriesCollection(i + 1).DataPoints(-1).Marker.FillColor.Automatic = False
                        .Plot.SeriesCollection(i + 1).DataPoints(-1).Marker.FillColor.Set(0, 255, 0)
                    ElseIf i = 2 Then
                        .Plot.SeriesCollection(i + 1).LegendText = ColumnCollection(i, 1)
                        .Plot.SeriesCollection(i + 1).DataPoints(-1).Brush.FillColor.Set(255, 0, 0) '''' Set Bydefault Red color for First Series
                    Else
                        nRed = nRed + 80
                        nGreen = nGreen + 20
                        nBlue = nBlue + 40
                        .Plot.SeriesCollection(i + 1).LegendText = ColumnCollection(i, 1)
                        .Plot.SeriesCollection(i + 1).DataPoints(-1).Brush.FillColor.Set(nRed, nGreen, nBlue)

                    End If

                Next

                ''set the label of x axis.
                For i = 1 To .RowCount
                    If DateCollection(i - 1, 1) <> Nothing Then
                        .Row = i
                        .RowLabel = DateCollection(i - 1, 1)
                    End If
                Next

            End With
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    '' END SUDHIR ''

    ''' <summary>
    ''' Fill ArrayList with DataTable which contain same Result Name with Value and Date Given
    ''' </summary>
    ''' <param name="strSearch">Result Name of which DataTable Create</param>
    ''' <param name="dv">Sorted view with Name and Date </param>
    ''' <param name="dt">Orignal DataTable</param>
    ''' <remarks></remarks>
    Public Sub FilterDate(ByVal strSearch As String, ByVal dv As DataView, ByVal dt As DataTable)
        Try
            ''Sandip Darade 20090618
            ''Special characters removed
            strSearch = strSearch.Replace("'", "''")
            dv.RowFilter = dt.Columns(2).ColumnName & " Like '%" & strSearch & "%'"

            Dim dt_Result As New DataTable
            dt_Result = dv.ToTable
            arrResultList.Add(dt_Result)
            dv.Sort = "ResultName"
            dv.RowFilter = ""
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub
    Public Sub DrawGraph_old()
        'Dim oclsViewGraphs As New clsViewGraphs
        Dim dt As DataTable
        ''Dim nTolarenceVal As Integer = 5
        dt = New DataTable

        dt = _dt_AllPlotValues
        dt_MinMax = _dt_MinMax

        'Dim dt_MINMAX As New DataTable

        '''''''' new data table req. for the data fill for the ranges
        'MinMaxdt_renge = New DataTable

        'Dim clmnMin As New DataColumn
        'With clmnMin
        '    .ColumnName = "Min"
        '    '.DataType = System.Type.GetType("System.integer")
        'End With
        'MinMaxdt_renge.Columns.Add(clmnMin)

        'Dim clmnMax As New DataColumn
        'With clmnMax
        '    .ColumnName = "Max"
        '    '.DataType = System.Type.GetType("System.Integer")
        '    '.DefaultValue = strVal(1)
        'End With
        'MinMaxdt_renge.Columns.Add(clmnMax)
        '''''''''

        'Dim j As Integer = 0
        'Dim nActualValue = 0


        'For j = 0 To dt.Rows.Count - 1
        '    'dt_MINMAX = getMinMAxRanges(dt.Rows(j)(0))  'Split(dt.Rows(j)("labotrd_ResultRange"), "-")
        '    'nActualValue = getMinMAxRanges()
        '    Dim strVal() As String
        '    If IsDBNull(dt.Rows(j)(0)) = False Then
        '        strVal = Split(dt.Rows(j)(0), "-")
        '    End If
        '    Dim r As DataRow
        '    r = MinMaxdt_renge.NewRow()
        '    r.Item(0) = CType(strVal(0), Integer)
        '    r.Item(1) = CType(strVal(1), Integer)
        '    MinMaxdt_renge.Rows.Add(r)
        'Next

        'dt_MINMAX = MinMaxdt_renge


        'view data in Datagrid
        DataGrid2.DataSource = dt_MinMax

        Dim i As Integer '= 20
        '' string data where patient Age collection when vital record enterd.
        ' ''Dim DateCollection(dt.Rows.Count - 1, 2) As String
        Dim DateCollection(dt_MinMax.Rows.Count - 1, 2) As String

        '' string for the data where hight of the patient is collected.
        Dim arrResults(dt_MinMax.Rows.Count - 1, 12) As String

        'Dim arrGraph(dt_MINMAX.Rows.Count - 1, 3) As String
        Dim strPatientInfo As String = Space(5) & " Patient Code:" & Space(1) & gloUC_PatientStrip1.PatientCode & Space(5) & "Patient Name:" & Space(1) & gloUC_PatientStrip1.PatientName & Space(5) & "DOB:" & Space(1) & gloUC_PatientStrip1.PatientDateOfBirth & Space(5) & "Phone No.:" & Space(1) & gloUC_PatientStrip1.PatientPhone & Space(5)
        chtResultsGraph.Footnote.Location.LocationType = MSChart20Lib.VtChLocationType.VtChLocationTypeTop
        chtResultsGraph.Footnote.Text = strPatientInfo

        chtResultsGraph.TitleText = "Period Vs Value"

        'chtResultsGraph.Backdrop.Fill.Brush.PatternColor.Automatic()

        Try
            With chtResultsGraph
                .chartType = MSChart20Lib.VtChChartType.VtChChartType2dCombination
                '.chartType = MSChart20Lib.VtChChartType.VtChChartType2dLine
                'If dt_MINMAX.Rows.Count <= 0 Then
                If dt_MinMax.Rows.Count >= 1 Then
                    DataGrid1.DataSource = Nothing
                    DataGrid1.DataSource = dt
                    'Axis Labels for the Graph
                    chtResultsGraph.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisTitle.Text = " Period "
                    chtResultsGraph.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).AxisTitle.Text = " Value "

                    ' get the total number of the vital entries for the patients
                    For i = 0 To dt_MinMax.Rows.Count - 1  'dt.Rows.Count - 1
                        ' get gender and assign standard Height to the array string
                        'arrResults(i, 1) = dt_MINMAX.Rows(i)("Agemos")                       

                        arrResults(i, 0) = dt_MinMax.Rows(i)(1) '+ nTolarenceVal ' * 0.394
                        arrResults(i, 1) = dt_MinMax.Rows(i)(0) '- nTolarenceVal ' * 0.394 '' plot
                        arrResults(i, 2) = dt.Rows(i)(1) 'actual value   '' plot 
                        arrResults(i, 3) = dt_MinMax.Rows(i)(0) '+ nTolarenceVal '' plot 
                        arrResults(i, 4) = dt.Rows(i)(1) '400 'dt.Rows(i)(1)

                        'DateCollection(i, 1) = dt_MINMAX.Rows(i)(2)
                        If IsDate(dt.Rows(i)(3)) = True Then
                            'DateCollection(i, 1) = CType(dt.Rows(i)(3), System.DateTime).Date
                            DateCollection(i, 1) = dt.Rows(i)(3)
                        Else
                            DateCollection(i, 1) = dt.Rows(i)(3)
                        End If
                    Next
                Else
                    MessageBox.Show("No Test data available for this Result. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Me.Close()
                    Exit Sub

                End If

                'Fill For PAtient
                Dim Count As Integer
                Dim m As Integer = 0
                Count = arrResults.Length / arrResults.GetLength(1)

                For i = 0 To Count - 1
                    For m = 0 To dt.Rows.Count - 1
                        If arrResults(i, 1) = dt.Rows(m)(1) Then
                            '' If Age Matches then Add Patients Ht in to The Array                          

                            arrResults(i, 2) = dt.Rows(i)(1)
                            Exit For
                        End If
                    Next
                    arrResults(i, 1) = Nothing
                Next

                'set data to draw chart
                .ChartData = CType(arrResults, Object)


                ' set graph styles for the patient vital entries.
                .Plot.SeriesCollection(1).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                .Plot.SeriesCollection(1).Select()
                '.Plot.SeriesCollection(3).SeriesMarker.Show = True
                .Plot.SeriesCollection(1).ShowLine = False
                '.Plot.SeriesCollection(3).DataPoints(-1).Marker.Style = MSChart20Lib.VtMarkerStyle.VtMarkerStyleDiamond
                .Plot.SeriesCollection(1).DataPoints(-1).Marker.Style = MSChart20Lib.VtMarkerStyle.VtMarkerStyleFilledDiamond
                .Plot.SeriesCollection(1).DataPoints(-1).Marker.Visible = True
                .Plot.SeriesCollection(1).DataPoints(-1).Marker.FillColor.Set(255, 0, 0)
                '.Plot.SeriesCollection(3).DataPoints(-1).Marker.Pen.Style = MSChart20Lib.VtPenStyle.VtPenStyleDashDot
                .Plot.SeriesCollection(1).DataPoints(-1).Brush.Style = MSChart20Lib.VtBrushStyle.VtBrushStyleSolid
                .Plot.SeriesCollection(1).DataPoints(-1).Marker.Size = 20
                .Plot.SeriesCollection(1).Pen.Width = 20
                .Plot.SeriesCollection(1).DataPoints(-1).Brush.FillColor.Set(255, 0, 0)

                ' set graph styles for the patient vital entries.
                .Plot.SeriesCollection(3).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                .Plot.SeriesCollection(3).Select()
                '.Plot.SeriesCollection(3).SeriesMarker.Show = True
                .Plot.SeriesCollection(3).ShowLine = False
                '.Plot.SeriesCollection(3).DataPoints(-1).Marker.Style = MSChart20Lib.VtMarkerStyle.VtMarkerStyleDiamond
                .Plot.SeriesCollection(3).DataPoints(-1).Marker.Style = MSChart20Lib.VtMarkerStyle.VtMarkerStyleFilledDiamond
                .Plot.SeriesCollection(3).DataPoints(-1).Marker.Visible = True
                .Plot.SeriesCollection(3).DataPoints(-1).Marker.FillColor.Set(255, 0, 0)
                '.Plot.SeriesCollection(3).DataPoints(-1).Marker.Pen.Style = MSChart20Lib.VtPenStyle.VtPenStyleDashDot
                .Plot.SeriesCollection(3).DataPoints(-1).Brush.Style = MSChart20Lib.VtBrushStyle.VtBrushStyleSolid
                .Plot.SeriesCollection(3).DataPoints(-1).Marker.Size = 20
                .Plot.SeriesCollection(3).Pen.Width = 20
                .Plot.SeriesCollection(3).DataPoints(-1).Brush.FillColor.Set(255, 0, 0)


                ' set the display properties of the graphs curve.
                Dim cnt As Integer
                For cnt = 1 To 12
                    .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                    .Plot.SeriesCollection(cnt).Pen.Width = 22
                    If cnt = 0 Then
                        .Plot.SeriesCollection(cnt).Pen.VtColor.Set(0, 155, 0)
                    ElseIf cnt = 1 Then
                        .Plot.SeriesCollection(cnt).Pen.VtColor.Set(200, 50, 0) 'MAX
                        .Plot.SeriesCollection(cnt).Pen.Width = 50
                    ElseIf cnt = 2 Then
                        .Plot.SeriesCollection(cnt).Pen.VtColor.Set(0, 155, 0) '
                    ElseIf cnt = 3 Then
                        .Plot.SeriesCollection(cnt).Pen.VtColor.Set(0, 0, 0) ' Star
                    ElseIf cnt = 4 Then
                        .Plot.SeriesCollection(cnt).Pen.VtColor.Set(0, 155, 0) ' MIN
                        .Plot.SeriesCollection(cnt).Pen.Width = 50
                    Else
                        .Plot.SeriesCollection(cnt).Pen.VtColor.Set(0, 0, 255)
                        .Plot.SeriesCollection(cnt).Pen.Width = 50
                    End If
                Next

                ' set the graph properties for the y axis.
                '.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Minimum = 18
                '.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Maximum = 42
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MajorDivision = 10
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MinorDivision = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerLabel = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerTick = 1

                ' set the properties for the x axis.
                '.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MajorDivision = 10
                '.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MinorDivision = 10

                'set the label of x axis.
                For i = 1 To .RowCount
                    .Row = i
                    .RowLabel = DateCollection(i - 1, 1)
                Next

            End With
        Catch ex As Exception
            ' MsgBox(ex.ToString)
            Throw ex
        End Try
    End Sub

    Private Sub frmLab_GraphsResult_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Call fillData()
        Try

            lblfromDate.Text = Format(fromDate, "MM/dd/yyyy")
            lblToDate.Text = Format(toDate, "MM/dd/yyyy")

            Set_PatientDetailStrip()
            Call DrawGraph()
            'Dim ograph As New frmLab_Graphs
            lblTestDetails.Text = _strTestName 'ograph.cmbTests.Text  'nTest
            lblResultDetails.Text = _strResultname 'ograph.cmbResults.Text  'nResult

            If _bBtnCriteria = False Then
                ts_btnChangeCriteria.Visible = False
            Else
                ts_btnChangeCriteria.Visible = True
            End If
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, gnPatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub

    Private Sub ChangeCriteriaCC()

        Dim frm As New frmLab_Graphs(_ConnectionString, 0, True, fromDate, toDate, _strTestName, _strResultname)
        Try
            With frm
                .ShowInTaskbar = False
                Me.Close()
                .ShowDialog(Me.MdiParent)
                .Dispose()
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try

        'Me.Close()
    End Sub

    Private Sub PrintCC()
        Try


            'Dim oOldClipboard As IDataObject
            'Dim oChartClipboard As IDataObject
            'oOldClipboard = Clipboard.GetDataObject() ' save current clipboard contents
            'chtResultsGraph.EditCopy() ' copy image of chart
            'oChartClipboard = Clipboard.GetDataObject
            'Dim oBitmap As Bitmap
            'oBitmap = oChartClipboard.GetData(DataFormats.Bitmap, True)
            'Dim X As Integer
            'Dim Y As Integer
            'Dim sFmt = New StringFormat()
            'Dim leftMargin As Single

            ''leftMargin = printDocument1.PrinterSettings.DefaultPageSettings.Margins.Left / 2

            'Y = printDocument1.PrinterSettings.DefaultPageSettings.Margins.Top
            'X = (printDocument1.PrinterSettings.DefaultPageSettings.PaperSize.Width - chtResultsGraph.Width) / 2
            'e.Graphics.DrawImage(oBitmap, X, Y)

            ''However, another source indicates that this code would also work. First put
            ''a printdocument1, menuitemPrintChart and a PrintDialog on the form. Then,
            ''write code that will save the chart as a temp.jpeg and use the following...
            ''Dim myCanvas As Graphics = e.Graphics
            ''Dim background As Bitmap
            ''background = New Bitmap("temp.jpeg")
            ''myCanvas.DrawImage(background, 0, 0)
            ''background.Dispose()

            'StreamToPrint = New IO.StreamReader("temp.jpeg")
            'PrintDialog1.Document = PrintDocument1
            'PrintDialog1.PrinterSettings = PrintDocument1.PrinterSettings
            'PrintDialog1.AllowSomePages = True
            'If PrintDialog1.ShowDialog = DialogResult.OK Then
            '    PrintDocument1.PrinterSettings = PrintDialog1.PrinterSettings
            '    PrintDocument1.Print()
            '    If Not (StreamToPrint Is Nothing) Then
            '        StreamToPrint.Close()
            '        Kill("temp.jpeg")
            '    End If
            'End If

            '--------------------
            'chtResultsGraph.EditCopy()

            'Dim chartCapture As Bitmap '= Clipboard.GetDataObject().GetData("Bitmap", True)
            'chartCapture = Clipboard.GetDataObject().GetData("Bitmap", True)
            'chartCapture.Save("Image.Jpeg")

            '      {
            'MSChart.EditCopy();
            'Bitmap chartCapture = 
            '    (Bitmap) Clipboard.GetDataObject().GetData("Bitmap", true);
            '//setting the alignment for the printing session
            'e.Graphics.DrawImage(chartCapture, 8,80);
            'chartCapture.Dispose();
            '''''''''''''''''''''''''''''''''''''
            'your computer.
            'Dim strSourceFile As String = "C:\Windows\greenstone.bmp"

            ''Replace the path below to the path of a WMF file to create
            'Dim strDestFile As String = "C:\Windows\greenstone.wmf"

            'Try
            '    'Load the image using the FromFile method and store it in
            '    'the imgOriginal variable.
            '    Dim imgOriginal As Image = Image.FromFile(strSourceFile)
            '    MessageBox.Show(ControlChars.Quote & strSourceFile & _
            '       ControlChars.Quote & " loaded successfully.")

            '    'Save the original image out as a Windows MetaFile Format file.
            '    imgOriginal.Save(strDestFile, ImageFormat.Wmf)
            '    MessageBox.Show(ControlChars.Quote & strDestFile & _
            '       ControlChars.Quote & " was saved successfully.")

            '    'Load the new image using the FromFile method and attempt
            '    'to store it in a variable declared as MetaFile.
            '    Dim wmfNew As Metafile = Image.FromFile(strDestFile) '<--Code fails here
            '    MessageBox.Show(ControlChars.Quote & strDestFile & _
            '       ControlChars.Quote & " loaded successfully.")

            'Catch excFileNotFound As System.IO.FileNotFoundException
            '    MessageBox.Show(ControlChars.Quote & strSourceFile & _
            '       ControlChars.Quote & " is not a valid path. " & _
            '       "Please correct the code to use a valid path.")
            'End Try

            ''''''''''''''

            '' ''Dim oOldClipboard As IDataObject
            '' ''Dim oChartClipboard As IDataObject
            '' ''oOldClipboard = Clipboard.GetDataObject() ' save current clipboard contents()
            '' ''chtResultsGraph.EditCopy() ' copy image of chart

            '' ''oChartClipboard = Clipboard.GetDataObject
            '' ''Dim oBitmap As Bitmap
            '' ''oBitmap = oChartClipboard.GetData(DataFormats.Bitmap, True)
            '' ''Dim X As Integer
            '' ''Dim Y As Integer
            '' ''Dim sFmt = New StringFormat()
            '' ''Dim leftMargin As Single
            '' ''leftMargin = printGraph.PrinterSettings.DefaultPageSettings.Margins.Left / 2

            '' ''Y = printGraph.PrinterSettings.DefaultPageSettings.Margins.Top
            '' ''X = (printGraph.PrinterSettings.DefaultPageSettings.PaperSize.Width - chtResultsGraph.Width) / 2

            '''''''''''''''''''''''''''

            'Dim _ChartCapture As Bitmap
            'With chtResultsGraph
            '    .EditCopy()
            '    '_ChartCapture = CType(Clipboard.GetDataObject().GetData("Bitmap", True), Bitmap)
            '    Dim chartCapture As Bitmap = DirectCast(Clipboard.GetDataObject().GetData("Bitmap", True), Bitmap)
            '    chartCapture.Save("Image.Jpeg")
            'End With
            '' _ChartCapture.Save("Image.jpeg")

            'printGraph.DefaultPageSettings.Landscape = True
            'printDialog.Document = printGraph
            'printGraph.Print()
            '''''''''''''''''''''''


            'Print(1, "Image.Jpeg")
            'MSChart.EditCopy()

            'MSChart.EditCopy();
            'Bitmap chartCapture = 
            '(Bitmap) Clipboard.GetDataObject().GetData("Bitmap", true);
            'chartCapture.Save("Image.Jpeg");
            'break;

            'printGraph.DefaultPageSettings.Landscape = true;
            'printDialog.Document = printGraph;
            'if(printDialog.ShowDialog() == DialogResult.OK)
            '{
            '  printGraph.Print();
            '}
            'break;
            chtResultsGraph.Backdrop.Fill.Brush.Style = MSChart20Lib.VtBrushStyle.VtBrushStyleSolid
            chtResultsGraph.Backdrop.Fill.Style = MSChart20Lib.VtFillStyle.VtFillStyleBrush
            System.Windows.Forms.Application.DoEvents()
            If Not IsNothing(Pcte) Then
                Pcte.Dispose()
                Pcte = Nothing
            End If
            Pcte = Hardcopy.CreateBitmap(chtResultsGraph)

            'PictureBox1.Image = Pcte
            ''removed pegausImageXpress7 -> Dhruv
            'With PicImag_container
            '    .Picture = Pcte
            '    .ZoomToFit(PegasusImaging.WinForms.ImagXpress7.enumZoomFit.ZOOMFIT_BEST)
            '    .BorderType = PegasusImaging.WinForms.ImagXpress7.enumBorderType.BORD_None
            'End With
            'm_oGraphPic1.FormattedPicture = VB6.ImageToIPictureDisp(Pcte)
            chtResultsGraph.Backdrop.Fill.Style = MSChart20Lib.VtFillStyle.VtFillStyleNull
            chtResultsGraph.Backdrop.Fill.Brush.Style = MSChart20Lib.VtBrushStyle.VtBrushStyleSolid


            If gloGlobal.gloTSPrint.isCopyPrint Then
                Dim impPrint As gloGlobal.ImagePrint = New gloGlobal.ImagePrint()
                Dim dictImages As Dictionary(Of [String], [Byte]()) = impPrint.printdoc_Print_Conversion(8.5F, 11.0F, 600, 600, Pcte)
                Dim fileName As String = gloGlobal.clsFileExtensions.NewDocumentName(gloGlobal.gloTSPrint.TempPath, ".zip", "MMddyyyyHHmmssffff")
                Dim lstDocs As New List(Of gloPrintDialog.gloPrintProgressController.DocumentInfo)()
                Dim ZipedFiles As List(Of String) = gloGlobal.gloTSPrint.ZipAllBytes(dictImages, fileName, gloGlobal.gloTSPrint.NoOfPages)
                For i As Integer = 0 To ZipedFiles.Count - 1
                    Dim DocInfo As New gloPrintDialog.gloPrintProgressController.DocumentInfo()
                    DocInfo.PdfFileName = ZipedFiles(i)
                    DocInfo.SrcFileName = ZipedFiles(i)
                    DocInfo.footerInfo = Nothing
                    lstDocs.Add(DocInfo)
                Next
                gloPrintDialog.gloPrintProgressController.SendForPrint(lstDocs)
            Else
                Dim printer As PegasusImaging.WinForms.PrintPro4.Printer
                Dim job As PegasusImaging.WinForms.PrintPro4.PrintJob
                printer = PegasusImaging.WinForms.PrintPro4.Printer.SelectPrinter(PrintPro1, False)

                If Not printer Is Nothing Then
                    ' Create a PrintJob object for the above-selected Printer. If the printer is not specified, PrintPRO
                    ' will just use Windows's default printer.
                    job = New PegasusImaging.WinForms.PrintPro4.PrintJob(printer)
                    job.Name = "Print Graph"

                    ' Print all of the information on the page
                    PrintPage(job)
                    ' Finish the print job to end the current page and print the document.
                    job.Finish()
                    job.Dispose()
                    job = Nothing
                    printer.Dispose()
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If (IsNothing(Pcte) = False) Then
                Pcte.Dispose()
                Pcte = Nothing
            End If

        End Try
    End Sub

    'Private Sub SendForPrint(lstDocs As List(Of gloPrintDialog.gloPrintProgressController.DocumentInfo))
    '    Dim ogloPrintProgressController As gloPrintDialog.gloPrintProgressController = Nothing

    '    Try
    '        Dim extendedPrinterSettings As New gloPrintDialog.gloExtendedPrinterSettings()
    '        extendedPrinterSettings.IsShowProgress = False
    '        extendedPrinterSettings.IsBackGroundPrint = True
    '        ogloPrintProgressController = New gloPrintDialog.gloPrintProgressController(lstDocs, Nothing, extendedPrinterSettings, blnUseEMFForSSRS:=True)
    '        ogloPrintProgressController.ShowProgress(Nothing)
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gloGlobal.gloTSPrint.getMessageCaption(), MessageBoxButtons.OK, MessageBoxIcon.[Error])

    '        ex = Nothing
    '    Finally
    '    End Try
    'End Sub

    Private Sub PrintPage(ByVal job As PegasusImaging.WinForms.PrintPro4.PrintJob)
        ' job.PrintImage(picture, new PointF(1640, 5700), new SizeF(5000, 4000), new PointF(0, 0), new SizeF(picture.Width, picture.Height), false)
        Try
            If (IsNothing(Pcte) = False) Then
                job.PrintImage(Pcte, New PointF(1000, 1000), New SizeF(10600, 9600), True)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Private Sub PrintDocument1_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
    '    Dim myCanvas As Graphics = e.Graphics
    '    Dim background As Bitmap
    '    background = New Bitmap("temp.jpeg")
    '    myCanvas.DrawImage(background, 0, 0)
    '    background.Dispose()
    'End Sub

    'Private Sub printGraph_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles printGraph.PrintPage
    '    chtResultsGraph.EditCopy()
    '    Dim bmp As Bitmap
    '    bmp = Clipboard.GetDataObject().GetData("bitmap", True)
    '    e.Graphics.DrawImage(bmp, 8, 80)
    '    bmp.Dispose()

    '    'Bitmap chartCapture = (Bitmap) Clipboard.GetDataObject().GetData("Bitmap", true);
    '    'e.Graphics.DrawImage(chartCapture, 8,80);
    '    'chartCapture.Dispose();  
    'End Sub


    Private Sub tlsp_LabGraphResult_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_LabGraphResult.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Print"
                    PrintCC()

                Case "Change Criteria"
                    ChangeCriteriaCC()

                Case "Close"
                    Me.Close()

            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)

        End Try
    End Sub


    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class