Imports System.Data
Imports System.Data.SqlClient
Imports gloUserControlLibrary
Imports System.Windows.Forms.Integration
Imports gloPatient
Imports System.IO
Public Class frmCardioVasGrph
    Implements IPatientContext

    Dim _PatientID As Long = 0
    Dim _ExamID As Long = 0
    Dim _VisitID As Long = 0
    Dim _ClinicID As Long = 0
    Public m_IsReadOnly As Boolean = False
    Private WithEvents _PatientStrip As gloUC_PatientStrip = Nothing
    Dim SentDSToTimeline As DataSet = Nothing
    Public Sub New(ByVal PatientID As Long, ByVal ExamID As Long, ByVal VisitID As Long)
        MyBase.New()
        ''EjectionFractionID is Zero when EjectionFraction List is Open from Ejection Fraction

        _ClinicID = 1
        ''PatientID  is Zero when Patient list  is Open from Patient list
        _PatientID = PatientID


        ''ExamID is Zero when Exam list  is Open from Exam list
        _ExamID = ExamID

        '' VisitID is Zero when Problem List is Open from Problem List
        _VisitID = VisitID ' 391117437030546801


        '' ClinicID is Zero when Clinic List is Open from Clinic List


        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub


    Private Sub loadPatientStrip()
        If (IsNothing(_PatientStrip) = False) Then
            Me.Controls.Remove(_PatientStrip)
            _PatientStrip.Dispose()
            _PatientStrip = Nothing
        End If
        _PatientStrip = New gloUC_PatientStrip
        _PatientStrip.ShowDetail(_PatientID, gloUC_PatientStrip.enumFormName.Timeline)
        _PatientStrip.Dock = DockStyle.Top
        _PatientStrip.Padding = New Padding(3, 0, 3, 0)


        Me.Controls.Add(_PatientStrip)
        pnlToolstrip.SendToBack()
        PnlGraph.BringToFront()
        If m_IsReadOnly Then
            _PatientStrip.DTPEnabled = False
        End If

    End Sub

    Private Sub frmCardioVasGrph_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub frmCardioVasGrph_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try


            ' File.Delete(uc.strFileName)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub frmCardioVasGrph_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Dim odb As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
        ' Dim dt As DataTable
        Dim _strSql As String = ""

        Dim cntid As Integer = 1
        Dim itemcnt As Integer = 1

        Dim ds As New DataSet
        Try


            ' ds.ReadXml(Application.StartupPath & "\\XMLFile" & "\\TimelineData1.XML")
            Dim dtScenario As DataTable = New DataTable
            dtScenario.TableName = "Scenario"
            dtScenario.Columns.Add("Scenario_Id", GetType(Int64))
            dtScenario.Columns.Add("PatientId", GetType(Integer))
            dtScenario.Columns.Add("Description", GetType(String))
            dtScenario.Columns.Add("BaseDate", GetType(DateTime))
            dtScenario.Columns.Add("TimeFrequencySelectedIndex", GetType(Integer))

            ds.Tables.Add(dtScenario)

            Dim dtSection As DataTable = New DataTable
            dtSection.TableName = "Section"
            dtSection.Columns.Add("Section_Id", GetType(Int64))
            dtSection.Columns.Add("Name", GetType(String))
            dtSection.Columns.Add("Scenario_Id", GetType(Int64))

            ds.Tables.Add(dtSection)
            'Dim drrel As New DataRelation("scesec", dtScenario.Columns("Scenario_Id"), dtSection.Columns("Scenario_Id"))


            Dim dtrow1 As DataTable = New DataTable
            dtrow1.TableName = "Row"
            dtrow1.Columns.Add("Row_Id", GetType(Int64))

            dtrow1.Columns.Add("Id", GetType(Int64))
            dtrow1.Columns.Add("Name", GetType(String))
            dtrow1.Columns.Add("Background", GetType(String))
            dtrow1.Columns.Add("MaxLabelStackLevels", GetType(String))
            dtrow1.Columns.Add("Description", GetType(String))
            dtrow1.Columns.Add("ShowLabelOvercrowdingNotifications", GetType(String))
            dtrow1.Columns.Add("Section_Id", GetType(Int64))

            ds.Tables.Add(dtrow1)
            'Dim drrel2 As New DataRelation("secrow", dtSection.Columns("Section_Id"), dtrow1.Columns("Section_Id"))

            Dim dtitem1 As DataTable = New DataTable
            dtitem1.TableName = "Item"
            dtitem1.Columns.Add("FluidStrength", GetType(String))

            dtitem1.Columns.Add("Brand", GetType(String))
            dtitem1.Columns.Add("Form", GetType(String))
            dtitem1.Columns.Add("DoseLabel", GetType(String))
            dtitem1.Columns.Add("Dose", GetType(String))
            dtitem1.Columns.Add("Route", GetType(String))
            dtitem1.Columns.Add("ExamID", GetType(String))
            dtitem1.Columns.Add("ExamStatus", GetType(String))
            dtitem1.Columns.Add("VisitID", GetType(String))
            dtitem1.Columns.Add("sTemplateName", GetType(String))
            dtitem1.Columns.Add("Frequency", GetType(String))
            dtitem1.Columns.Add("Item_Id", GetType(Int64))
            dtitem1.Columns.Add("DoseDuration", GetType(String))
            dtitem1.Columns.Add("SolidStrength", GetType(String))
            dtitem1.Columns.Add("Units", GetType(String))
            dtitem1.Columns.Add("UnitsDescription", GetType(String))
            dtitem1.Columns.Add("YAxisMaxValue", GetType(Single))
            dtitem1.Columns.Add("YAxisMinValue", GetType(Single))
            dtitem1.Columns.Add("YAxisPadding", GetType(Single))
            dtitem1.Columns.Add("YAxisMajorInterval", GetType(Single))
            dtitem1.Columns.Add("YAxisIntervalMinimumHeight", GetType(Single))
            dtitem1.Columns.Add("Height", GetType(Integer))
            dtitem1.Columns.Add("ShowNormalRange", GetType(Boolean))
            dtitem1.Columns.Add("NormalRangeDescription", GetType(String))
            dtitem1.Columns.Add("NormalRangeMinimumValue", GetType(Single))
            dtitem1.Columns.Add("NormalRangeMaximumValue", GetType(Single))
            dtitem1.Columns.Add("NormalRangeBrush", GetType(String))
            dtitem1.Columns.Add("HoverBackground", GetType(String))
            dtitem1.Columns.Add("InterpolationLineColor", GetType(String))
            dtitem1.Columns.Add("DataMarkerTemplate", GetType(String))
            dtitem1.Columns.Add("Style", GetType(String))
            dtitem1.Columns.Add("PointTemplate", GetType(String))
            dtitem1.Columns.Add("LabelTemplate", GetType(String))
            dtitem1.Columns.Add("Comments", GetType(String))
            dtitem1.Columns.Add("Name", GetType(String))
            dtitem1.Columns.Add("StartDate", GetType(Date))
            dtitem1.Columns.Add("EndDate", GetType(Date))
            dtitem1.Columns.Add("Type", GetType(String))
            dtitem1.Columns.Add("Row_Id", GetType(Int64))
            ds.Tables.Add(dtitem1)
            'Dim drrel3 As New DataRelation("rowitem", dtrow1.Columns("Row_Id"), dtitem1.Columns("Row_Id"))


            Dim dtevents As DataTable = New DataTable
            dtevents.TableName = "Events"
            dtevents.Columns.Add("Events_Id", GetType(Int64))
            dtevents.Columns.Add("Item_Id", GetType(Int64))
            ds.Tables.Add(dtevents)
            'Dim drrel4 As New DataRelation("rowevts", dtitem1.Columns("Item_Id"), dtevents.Columns("Item_Id"))

            Dim dtevent As DataTable = New DataTable
            dtevent.TableName = "Event"
            dtevent.Columns.Add("Comments", GetType(String))
            dtevent.Columns.Add("Clinician", GetType(String))
            dtevent.Columns.Add("Type", GetType(String))
            dtevent.Columns.Add("ActualStartDate", GetType(Date))
            dtevent.Columns.Add("ActualEndDate", GetType(Date))
            dtevent.Columns.Add("Status", GetType(String))
            dtevent.Columns.Add("PlannedStartDate", GetType(Date))
            dtevent.Columns.Add("Y1", GetType(Single))
            dtevent.Columns.Add("Y2", GetType(Single))
            dtevent.Columns.Add("Events_Id", GetType(Int64))
            ds.Tables.Add(dtevent)
            'Dim drrel5 As New DataRelation("evtsevt", dtevents.Columns("Events_Id"), dtevent.Columns("Events_Id"))


            Dim drscen As DataRow = ds.Tables("Scenario").NewRow()

            drscen("BaseDate") = DateTime.Now.ToString()
            drscen("Scenario_Id") = 0
            drscen("PatientId") = 1
            drscen("Description") = "Secondary Care Data"

            drscen("TimeFrequencySelectedIndex") = 19

            ds.Tables("Scenario").Rows.Add(drscen) '   = DateTime.Now.ToString()

            '  Dim dt1 As DataTable = ds.Tables("Section")

            'ds.Tables("Event").Rows.Clear()   ''Clearing All tables
            'ds.Tables("Events").Rows.Clear()
            'ds.Tables("Item").Rows.Clear()
            'ds.Tables("Row").Rows.Clear()
            'ds.Tables("Section").Rows.Clear()












            '   secExamId = Convert.ToInt32(drexam(0))
            'Dim dt2 As DataTable = ds.Tables(4)
            'Dim dte As DataTable = ds.Tables(5)
            'Dim dtrow As DataTable = ds.Tables(2)
            'Dim dtitem As DataTable = ds.Tables(3)
            Try


                ds.Tables("Scenario").Rows(0)("BaseDate") = DateTime.Now.ToString()
                ds.Tables("Scenario").Rows(0)("TimeFrequencySelectedIndex") = "17"
            Catch ex As Exception
            End Try



            Dim dt1 As DataTable = ds.Tables("Section")

            ds.Tables("Event").Rows.Clear()
            ds.Tables("Events").Rows.Clear()
            ds.Tables("Item").Rows.Clear()
            ds.Tables("Row").Rows.Clear()
            ds.Tables("Section").Rows.Clear()
            ' fillgraph(cntid, itemcnt, ds)
            '  Dim grphdt As DataTable = PopulateEjectionFractionList()
            '   Dim modalitydt As DataRow() = grphdt.Select("distinct ModalityTest")
            fillgraph(cntid, itemcnt, ds)
            Dim el As New ElementHost
            Dim uc As New gloUCCardioVasculargraph(SentDSToTimeline)


            el.Child = uc
            el.Dock = DockStyle.Fill
            ' uc.Height = PnlGraph.Height
            ' uc.Width = PnlGraph.Width

            PnlGraph.Controls.Clear()
            PnlGraph.Controls.Add(el)
            ' Me.Controls.Add(el)
            Dim a As Integer = 0

            Call loadPatientStrip()
        Catch ex As Exception

        End Try
        Try
            gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Function PopulateEjectionFractionList() As DataTable

        'Declaration of variables for making connection
        Dim dt As New DataTable
        Dim cmd As SqlCommand
        Dim sqladpt As SqlDataAdapter

        'Connection string
        Dim conn As SqlConnection = New SqlConnection(GetConnectionString)
        '        cmd = New SqlCommand("Select * from CV_EjectionFraction where nPatientID=" & _PatientID & " and nExamID=" & _ExamID & " and nVisitID=" & _VisitID, conn)
        '   Dim strquery As String = "Select isnull(nEjectionFractionID,0)as EjectionFractionID,isnull(nPatientID,0)as PatientID,isnull(nExamID,0)as ExamID,isnull(nVisitID,0)as VisitID,isnull(nClinicID,0)as ClinicID,isnull(dtDateofTest,0)as TestDate,isnull(sModalityTest,'')as ModalityTest,isnull(sQuantityPercent,0)as QuantityPercent,isnull(sQuantityDesc,0)as QuantityDescription from CV_EjectionFraction  where nPatientID=" & _PatientID & " and nVisitID=" & _VisitID & " Order By sQuantityPercent"
        Dim strquery As String = "Select isnull(nEjectionFractionID,0)as EjectionFractionID,isnull(nPatientID,0)as PatientID,isnull(nExamID,0)as ExamID,isnull(nVisitID,0)as VisitID,isnull(nClinicID,0)as ClinicID,isnull(dtDateofTest,0)as TestDate,isnull(sModalityTest,'')as ModalityTest,isnull(sQuantityPercent,0)as QuantityPercent,isnull(sQuantityDesc,0)as QuantityDescription from CV_EjectionFraction  where nPatientID=" & _PatientID & " and len(sModalityTest)>0 and  ISNUMERIC(squantitypercent)>0 Order By sQuantityPercent"

        cmd = New SqlCommand(strquery, conn)
        sqladpt = New SqlDataAdapter(cmd)

        'Fill data adapter
        sqladpt.Fill(dt)
        If cmd IsNot Nothing Then
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
        End If
        sqladpt.Dispose()
        sqladpt = Nothing
        conn.Close()
        conn.Dispose()
        conn = Nothing
        'Return Data table
        Return dt
    End Function



    Public Sub fillgraph(ByVal cntid As Integer, ByVal itemcnt As Integer, ByVal ds As DataSet)
        'Dim dt_selectedResult As New DataTable
        'Dim oclsviewGraphs As New clsViewGraphs

        Dim grphdt As DataTable = PopulateEjectionFractionList()

        Dim uniq As DataTable = grphdt.DefaultView.ToTable(True, "ModalityTest")
        Dim tempid As Integer = 0
        Dim respid As Integer = 0
        Dim evtid As Integer = 1
        Try


            If grphdt.Rows.Count > 0 Then


                Dim drexam As DataRow = ds.Tables("Section").NewRow()
                drexam(0) = "0"
                drexam("Name") = "CardioVascular"
                drexam("Scenario_Id") = "0"
                ds.Tables("Section").Rows.Add(drexam)
                ds.Tables("Scenario").Rows(0)("BaseDate") = DateTime.Now.ToString()
                ds.Tables("Scenario").Rows(0)("TimeFrequencySelectedIndex") = "17"


                ds.Tables("Events").Rows.Clear()
                ds.Tables("Event").Rows.Clear()




                Dim flagtemp As Integer = 0
                Dim flagresp As Integer = 0
                Dim len As Integer = 0
                Dim arr As New ArrayList()
                For Each drp As DataRow In uniq.Rows


                    flagtemp = 0
                    'Sanjog- Added on 2011 March 31 to handle the "'" case in name 
                    For Each drc As DataRow In grphdt.Select("ModalityTest='" & drp(0).ToString().Replace("'", "''") & "'")
                        'Sanjog- Added on 2011 March 31 to handle the "'" case in name 

                        If drc("testdate").ToString().Trim() <> "" AndAlso drc("testdate").ToString().Trim() <> "01 Jan 1900" Then
                            'If Convert.ToInt32(drc("Celcius")) <> -1 Then
                            If len = 0 Then
                                arr.Clear()
                            End If
                            len += 1
                            arr.Add(CType(drc("QuantityPercent"), Integer))
                            If flagtemp = 0 Then



                                Dim dr As DataRow = ds.Tables("Row").NewRow
                                dr("Row_Id") = cntid.ToString()
                                dr("Id") = cntid.ToString()
                                dr("Name") = drp(0).ToString()
                                dr("Background") = "#EBCCCC"
                                dr("Section_Id") = "0"
                                dr("Name") = drc("ModalityTest")
                                ds.Tables("Row").Rows.Add(dr)

                                Dim dritem As DataRow = ds.Tables("Item").NewRow
                                dritem("Item_Id") = itemcnt.ToString()
                                dritem("Units") = ""
                                ' dritem("UnitsDescription") = "°C:Degree Celcius"
                                dritem("YAxisMaxValue") = "100"
                                dritem("Name") = drc("ModalityTest")
                                dritem("YAxisMinValue") = "0"
                                dritem("YAxisPadding") = "11"
                                dritem("YAxisMajorInterval") = "10"
                                dritem("YAxisIntervalMinimumHeight") = "20"
                                dritem("Height") = "200"
                                dritem("ShowNormalRange") = "False"
                                ' dritem("NormalRangeDescription") = "Adult normal range"
                                'dritem("NormalRangeMinimumValue") = "35"
                                'dritem("NormalRangeMaximumValue") = "37"
                                dritem("NormalRangeBrush") = "#FEFCE9"
                                dritem("HoverBackground") = "#B84E4E"
                                dritem("InterpolationLineColor") = "#990000"
                                dritem("DataMarkerTemplate") = "{StaticResource Square}"
                                dritem("Style") = "{StaticResource OverriddenDefaultStyle}"
                                dritem("Row_Id") = cntid.ToString()


                                ds.Tables("Item").Rows.Add(dritem)


                                Dim drevts As DataRow = ds.Tables("Events").NewRow()
                                drevts("Events_Id") = evtid.ToString()
                                drevts("Item_Id") = itemcnt.ToString()
                                ds.Tables("Events").Rows.Add(drevts)


                                tempid = evtid
                                evtid = evtid + 1
                                cntid = cntid + 1
                                itemcnt = itemcnt + 1

                                flagtemp = 1

                            End If


                            Dim drevt As DataRow = ds.Tables("Event").NewRow()
                            drevt("Type") = "GraphData"


                            'drevt("ActualStartDate") = drc("dtvitaldate").ToString
                            'drevt("Y1") = drc("drespiratoryRate").ToString()
                            'drevt("Events_Id") = respid.ToString()


                            drevt("ActualStartDate") = drc("TestDate").ToString
                            drevt("Y1") = drc("QuantityPercent").ToString()
                            drevt("Events_Id") = tempid.ToString()


                            ds.Tables("Event").Rows.Add(drevt)
                            'Sanjog- Added on 2011 March 31 to handle the "'" case in name 
                            If len = grphdt.Select("ModalityTest='" & drp(0).ToString().Replace("'", "''") & "'").ToArray().Length Then
                                'Sanjog- Added on 2011 March 31 to handle the "'" case in name 
                                arr.Sort()
                                Dim max As Integer = arr(arr.Count - 1)
                                Dim min As Integer = arr(0)
                                If (max - min) > 100 Then
                                    Dim interval As Integer = Math.Floor((max - min) / 10)
                                    ds.Tables("Item").Rows(ds.Tables("Item").Rows.Count - 1)("YAxisMajorInterval") = interval.ToString()
                                    ds.Tables("Item").Rows(ds.Tables("Item").Rows.Count - 1)("YAxisMaxValue") = max.ToString()
                                End If
                                len = 0
                            End If
                        End If



                        'If Convert.ToInt32(drc("drespiratoryRate")) <> -99 Then
                        '    If flagresp = 0 Then



                        '        Dim dr As DataRow = ds.Tables("Row").NewRow
                        '        dr("Row_Id") = cntid.ToString()
                        '        dr("Id") = cntid.ToString()
                        '        dr("Name") = "Respiratory Rate"
                        '        dr("Background") = "#E7D3E7"
                        '        dr("Section_Id") = "1"

                        '        ds.Tables("Row").Rows.Add(dr)

                        '        Dim dritem As DataRow = ds.Tables("Item").NewRow
                        '        dritem("Item_Id") = itemcnt.ToString()
                        '        dritem("Units") = ""

                        '        dritem("YAxisMaxValue") = "80"
                        '        dritem("YAxisMinValue") = "0"
                        '        dritem("YAxisPadding") = "12"
                        '        dritem("YAxisMajorInterval") = "10"
                        '        dritem("YAxisIntervalMinimumHeight") = "40"
                        '        dritem("Height") = "100"
                        '        dritem("ShowNormalRange") = "True"
                        '        'dritem("NormalRangeDescription") = "Adult normal range"
                        '        '     dritem("NormalRangeMinimumValue") = "10"
                        '        '   dritem("NormalRangeMaximumValue") = "20"
                        '        dritem("NormalRangeBrush") = "#FEFCE9"
                        '        dritem("HoverBackground") = "#B966B9"
                        '        dritem("InterpolationLineColor") = "#B966B9"
                        '        dritem("DataMarkerTemplate") = "{StaticResource Triangle}"
                        '        dritem("Style") = "{StaticResource OverriddenDefaultStyle}"
                        '        dritem("Row_Id") = cntid.ToString()


                        '        ds.Tables("Item").Rows.Add(dritem)


                        '        Dim drevts As DataRow = ds.Tables("Events").NewRow()
                        '        drevts("Events_Id") = evtid.ToString()
                        '        drevts("Item_Id") = itemcnt.ToString()
                        '        ds.Tables("Events").Rows.Add(drevts)


                        '        respid = evtid
                        '        evtid = evtid + 1
                        '        cntid = cntid + 1
                        '        itemcnt = itemcnt + 1

                        '        flagresp = 1

                        '    End If


                        '    Dim drevt As DataRow = ds.Tables("Event").NewRow()
                        '    drevt("Type") = "GraphData"




                        '    drevt("ActualStartDate") = drc("dtvitaldate").ToString
                        '    drevt("Y1") = drc("drespiratoryRate").ToString()
                        '    drevt("Events_Id") = respid.ToString()


                        '    ds.Tables("Event").Rows.Add(drevt)
                        'End If
                        'End If
                    Next

                Next
            End If
            '  uc.strFileName = TimelineNewFileName()
            'ds.WriteXml(uc.strFileName)
        Catch ex As Exception
        Finally
            SentDSToTimeline = ds
        End Try

    End Sub
    'Private Function TimelineNewFileName() As String

    '    Dim _Path As String = gstrgloEMRStartupPath & "\Temp"
    '    Dim _NewDocumentName As String = ""
    '    Dim _Extension As String = ".XML"
    '    Dim _dtCurrentDateTime As DateTime = System.DateTime.Now

    '    Dim i As Integer = 0
    '    _NewDocumentName = Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss tt") & " " & _dtCurrentDateTime.Millisecond & _Extension
    '    While File.Exists(_Path & "\" & _NewDocumentName) = True
    '        i = i + 1
    '        _NewDocumentName = Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss tt") & " " & _dtCurrentDateTime.Millisecond & "-" & i & _Extension
    '    End While
    '    Return _Path & "\" & _NewDocumentName

    'End Function
    Private Sub tblStrip_32_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tblStrip_32.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Close"
                Me.Close()

        End Select

    End Sub
    ''' <summary>
    ''' Property writen for Patient Context
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return _PatientID  'Curent patient variable(Local variable) for this module 
        End Get
    End Property
End Class