'Imports gloGeneral
Imports System.Xml
Imports System.IO
Imports System.Drawing
Imports System.Windows.Forms
Imports gloEMRGeneralLibrary.gloPrintFax.ReportActors
Imports gloEMRGeneralLibrary.gloGeneral
Namespace gloprintfax
    Friend Class CustomPrint
        Inherits ClsPrintandFax
        Implements IDisposable

        Dim strFileName As String = gloSettings.FolderSettings.AppTempFolderPath & gloGlobal.clsFileExtensions.GetUniqueDateString() & "PrescriptionReport.xml"
        Dim ReportHeaderCol As New Collection
        Dim PageHeaderCol As New Collection
        Dim PageFooterCol As New Collection
        Dim ReportFooterCol As New Collection
        Dim SectionsCol As Collection

        Dim pnlPresciptionReport As Panel

        Private GridPrinter As RxReportPrinter
        Dim objDataDictionary As IDataDictionary
        Private disposedValue As Boolean = False        ' To detect redundant calls
        Private _PrintDialog1 As PrintDialog
        Private _pnlPresciptionReport As Panel
        Private _WhereClause As String
        'Private Function getUniqueID() As String
        '    Static firstTime As Boolean = True
        '    Static myWatch As New Stopwatch()
        '    Static myTime As DateTime
        '    If firstTime = True Then
        '        firstTime = False
        '        myTime = Now()
        '        myWatch.Start()
        '    End If
        '    Dim TmSp As New TimeSpan(myTime.Ticks + myWatch.ElapsedTicks)
        '    getUniqueID = TmSp.Ticks.ToString()
        '    TmSp = Nothing

        'End Function
        Friend Property FileName() As String
            Get
                Return strFileName
            End Get
            Set(ByVal value As String)
                strFileName = value
            End Set
        End Property
        Public WriteOnly Property WhereClause() As String
            Set(ByVal value As String)
                _WhereClause = value
            End Set
        End Property
        Public Property PrintdialogControl() As PrintDialog
            Get
                Return _PrintDialog1
            End Get
            Set(ByVal value As PrintDialog)
                _PrintDialog1 = value
            End Set
        End Property
        Friend Sub CreateXML()
            If IsNothing(objDataDictionary) Then

                objDataDictionary = New CustomPrintDBLayer
            End If

            Dim objDatatable As DataTable = objDataDictionary.GetReport()
            If (IsNothing(objDatatable) = False) Then
                If Not objDatatable.Rows.Count > 1 Then

                    Dim content As Byte() = CType(objDatatable.Rows(0).Item("ReportData"), Byte())
                    '     Dim stream As MemoryStream = New MemoryStream(content)
                    If (IsNothing(content) = False) Then
                        Dim oFile As New System.IO.FileStream(strFileName, System.IO.FileMode.Create)
                        'stream.WriteTo(oFile)
                        oFile.Write(content, 0, content.Length)
                        oFile.Close()
                        oFile.Dispose()
                        oFile = Nothing
                    End If

                    'Else
                    '    InsertRxReport()
                End If
                objDatatable.Dispose()
                objDatatable = Nothing
            End If


        End Sub
        Friend Sub DeleteXML()
            If File.Exists(strFileName) Then
                File.Delete(strFileName)
            End If

        End Sub
        Friend Sub readXML(ByVal blnPrintStatus As Boolean)
            Dim objdatagrid As DataGrid = Nothing
            Dim objDetails As DataTable = Nothing
            Dim oDetails As DataTable = Nothing
            Try
                Dim ds As New DataSet
                ds.ReadXml(strFileName, XmlReadMode.InferSchema)
                Dim dt As DataTable
                '' Dim xxx As String = "Provider=sqloledb;Data Source=SakarServer;User Id=sa;Password=;database=DrugInter"


                ''Set Sections
                Call ClearCollection()
                SectionsCol = New Collection
                ReadSection(ds)

                ''Set For Datagrid 
                oDetails = New DataTable
                oDetails.Columns.Add("Text", GetType(String))
                oDetails.Columns.Add("Left", GetType(Int16))

                ''Reading each table for setting the controls
                For Each dt In ds.Tables
                    '''' Check for Database tag

                    Select Case dt.TableName.Substring(0, 3)
                        'Case "ReportHeade"
                        '    SectionDetails(dt, dt.TableName)
                        'Case "PageHeade"
                        '    SectionDetails(dt, dt.TableName)
                        'Case "Detail"
                        '    SectionDetails(dt, dt.TableName)
                        'Case "PageFoote"
                        '    SectionDetails(dt, dt.TableName)
                        'Case "ReportFoote"
                        '    SectionDetails(dt, dt.TableName)
                        Case "RHF"
                            ' BindControl(dt)
                            BindCollection(dt, dt.TableName.Substring(0, 3))
                            ' MsgBox("RHField")
                        Case "PHF"
                            'BindControl(dt)
                            BindCollection(dt, dt.TableName.Substring(0, 3))
                        Case "DTF"
                            Dim objDV As DataView
                            objDV = dt.Copy().DefaultView
                            If objDV.Count > 0 Then
                                If dt.Rows(0).Item("FieldType") <> "Image" And dt.Rows(0).Item("FieldType") <> "Caption" Then
                                    Dim objectdr As DataRow = oDetails.NewRow
                                    objectdr.Item("Text") = dt.Rows(0).Item("Text")
                                    objectdr.Item("Left") = dt.Rows(0).Item("Left")
                                    oDetails.Rows.Add(objectdr)
                                    'DetailsField.Add(dt.Rows(0).Item("Text"))
                                    ' strLocation = dt.Rows(0).Item("Top") & "," & dt.Rows(0).Item("Left")

                                End If
                            End If
                            objDV.Dispose()
                            objDV = Nothing

                            ' MsgBox("DTField")
                        Case "PFF"
                            ' BindControl(dt)
                            BindCollection(dt, dt.TableName.Substring(0, 3))
                        Case "RFF"
                            ' BindControl(dt)
                            BindCollection(dt, dt.TableName.Substring(0, 3))

                    End Select

                Next

                If Not IsNothing(objdatagrid) Then
                    objdatagrid = Nothing
                End If
                If Not ds Is Nothing Then ''disposed as per glo Code optimizer tool in 8000 version
                    ds.Clear()
                    ds.Dispose()
                    ds = Nothing
                End If
                Dim _PrintFont As New Font(System.Drawing.FontFamily.GenericSansSerif, 8)
                'Dim _PrinttextFont As New Font(System.Drawing.FontFamily.GenericSansSerif, 8)
                objdatagrid = New DataGrid

                objdatagrid.Font = _PrintFont
                If oDetails.Rows.Count > 0 Then

                    objDetails =  BindGrid(oDetails)
                    objdatagrid.DataSource = objDetails
                    _pnlPresciptionReport.Controls.Clear()
                    objdatagrid.Dock = DockStyle.Fill
                    _pnlPresciptionReport.Controls.Add(objdatagrid)

                    CustomGridStyle(objdatagrid, objDetails, _PrintFont)
                    'pnlDetails.Controls.Clear()
                    'pnlDetails.Controls.Add(objdatagrid)
                    objdatagrid.Visible = False
                End If

                Dim j As Int32
                Dim height As Double
                For j = 0 To objDetails.Rows.Count - 1
                    If objdatagrid.GetCellBounds(j, 0).Size.Height > height Then
                        height = objdatagrid.GetCellBounds(j, 0).Size.Height
                    End If
                Next

                If Not blnPrintStatus Then
                    'InvokePrintPreview(objdatagrid)
                Else
                    InvokePrint(objdatagrid, height)
                End If
           
                If (IsNothing(objdatagrid) = False) Then
                    If (pnlPresciptionReport.Controls.Contains(objdatagrid)) Then
                        pnlPresciptionReport.Controls.Remove(objdatagrid)
                    End If
                    objdatagrid.TableStyles.Clear()
                    objdatagrid.Dispose()
                    objdatagrid = Nothing
                End If
                _PrintFont.Dispose()
                _PrintFont = Nothing
            Catch ex As Exception

            Finally
                If Not IsNothing(objDetails) Then
                    objDetails.Dispose()
                    objDetails = Nothing
                End If

                If Not IsNothing(oDetails) Then ''disposed as per glo Code optimizer tool in 8000 version
                    oDetails.Dispose()
                    oDetails = Nothing
                End If
            End Try


        End Sub
        Private Sub CustomGridStyle(ByVal Grid As DataGrid, ByVal dt As DataTable, ByRef _PrinttextFont As Font)
            'Dim dv As DataView
            'dv = dt.DefaultView
            Dim ts As New clsDataGridTableStyle(dt.TableName)
            'Dim objclsCPT As New clsCPT

            'objclsCPT.SortDataview(dv.Table.Columns(2).ColumnName)
            Dim i As Integer
            'Code written by mahesh on 15/02/2007
            'Dim Drug As New DataGridTextBoxColumn
            'Dim SIG As New DataGridTextBoxColumn
            'For i = 0 To dt.Columns.Count - 1

            '    If UCase(dt.Columns(i).ColumnName) = UCase("Drug") Then
            '        Drug.Width = Grid.Width * 0.2
            '    ElseIf UCase(dt.Columns(i).ColumnName) = UCase("SIG") Then
            '        SIG.Width = Grid.Width * 0.3
            '    Else

            '    End If

            'Next
            'Code written by mahesh on 15/02/2007
            Dim objstyle As MultiLineColumn


            Dim dbwidth As Double
            dbwidth = Grid.Width
            For i = 0 To dt.Columns.Count - 1
                objstyle = New MultiLineColumn
                objstyle.TextBox.Font = _PrinttextFont
                objstyle.MappingName = dt.Columns(i).ColumnName
                objstyle.HeaderText = dt.Columns(i).ColumnName
                objstyle.Alignment = HorizontalAlignment.Left

                If UCase(dt.Columns(i).ColumnName) = UCase("Drug") Then
                    objstyle.Width = Grid.Width * 0.31
                    objstyle.AutoAdjustHeight = True
                    dbwidth = dbwidth - objstyle.Width
                ElseIf UCase(dt.Columns(i).ColumnName) = UCase("SIG") Then
                    objstyle.Width = Grid.Width * 0.35
                    objstyle.AutoAdjustHeight = True
                    dbwidth = dbwidth - objstyle.Width
                ElseIf UCase(dt.Columns(i).ColumnName) = UCase("Dispense") Then
                    objstyle.Width = Grid.Width * 0.11
                    objstyle.AutoAdjustHeight = False
                ElseIf UCase(dt.Columns(i).ColumnName) = UCase("Refill") Then
                    objstyle.Width = Grid.Width * 0.09
                    objstyle.AutoAdjustHeight = False
                Else
                    objstyle.Width = Grid.Width * 0.14
                    objstyle.AutoAdjustHeight = False
                End If
                ts.GridColumnStyles.Add(objstyle)
                'objstyle.Dispose()
                'objstyle = Nothing
            Next

            'ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {Drug, SIG})
            Grid.TableStyles.Clear()
            Grid.TableStyles.Add(ts)

        End Sub

        Private Sub InvokePrint(ByRef dggrid As DataGrid, ByVal height As Double)
            'Instantiate GridPrinter class
            If GridPrinter Is Nothing Then
                GridPrinter = New RxReportPrinter(dggrid)
            End If

            'Invoke PrintDialog
            _PrintDialog1.Document = GridPrinter.PrintDocument

            'Populate Sections Collection
            GridPrinter.SetHeaderControls(ReportHeaderCol, 1)
            GridPrinter.SetHeaderControls(PageHeaderCol, 2)
            GridPrinter.SetHeaderControls(PageFooterCol, 4)
            GridPrinter.SetHeaderControls(ReportFooterCol, 5)
            GridPrinter.SetHeaderControls(SectionsCol, 6)

            'Invoke the Print Method of GridPrinter

            'If .ShowDialog = DialogResult.OK Then
            GridPrinter._Rowheight = height
            GridPrinter.Print()
            'End If

            'CleanUp Code
            _PrintDialog1.Document = Nothing
            If (IsNothing(GridPrinter) = False) Then
                GridPrinter.Dispose()
                GridPrinter = Nothing
            End If
            'GridPrinter.PrintDocument.Dispose()
            'GridPrinter.Dispose()
            'GridPrinter = Nothing
            'dggrid.Dispose()

        End Sub

        Private Sub ReadSection(ByVal oData As DataSet)
            Dim otable As DataTable
            For Each otable In oData.Tables
                Select Case otable.TableName
                    Case "ReportHeader"
                        SectionDetails(otable, otable.TableName)
                    Case "PageHeader"
                        SectionDetails(otable, otable.TableName)
                    Case "Details"
                        SectionDetails(otable, otable.TableName)
                    Case "PageFooter"
                        SectionDetails(otable, otable.TableName)
                    Case "ReportFooter"
                        SectionDetails(otable, otable.TableName)

                End Select
            Next
        End Sub
        Private Sub SectionDetails(ByVal oTable As DataTable, ByVal sectiontype As String)
            Dim objSection As New ReportActors.Section
            ' Dim dv As DataView = oTable.DefaultView
            objSection.SectionType = sectiontype
            objSection.SectionWidth = oTable.Rows(0).Item("Width")
            objSection.SectionHeight = oTable.Rows(0).Item("Height")
            SectionsCol.Add(objSection)
        End Sub
        Friend Sub CallCustomPrint()
            'If gstrRxReportpath <> "" Then
            If File.Exists(strFileName) Then
                File.Delete(strFileName)
            End If
            CreateXML()
            If File.Exists(strFileName) Then
               
                readXML(True)
            Else
                MsgBox("Kindly Design the Prescription Report from gloEMR Admin and then Print Report", MsgBoxStyle.ApplicationModal + MsgBoxStyle.OkOnly, clsgeneral.gstrMessageBoxCaption & "-Prescription")
            End If
            'Else
            'MsgBox("Kindly Design the Prescription Report from gloEMR Admin and then Print Report", MsgBoxStyle.ApplicationModal + MsgBoxStyle.OKOnly, gstrMessageBoxCaption & "-Prescription")
            'End If
        End Sub
        Private Sub ClearCollection()
            Dim i As Int16
            'Make sure to Clear the collection before u can populate
            'it with new items
            If Not IsNothing(SectionsCol) Then

                For i = SectionsCol.Count To 1 Step -1
                    SectionsCol.Remove(i)
                Next
            End If
            If Not IsNothing(ReportHeaderCol) Then
                For i = ReportHeaderCol.Count To 1 Step -1
                    ReportHeaderCol.Remove(i)
                Next
            End If
            If Not IsNothing(PageHeaderCol) Then
                For i = PageHeaderCol.Count To 1 Step -1
                    PageHeaderCol.Remove(i)
                Next
            End If
            If Not IsNothing(PageFooterCol) Then
                For i = PageFooterCol.Count To 1 Step -1
                    PageFooterCol.Remove(i)
                Next
            End If
            If Not IsNothing(ReportFooterCol) Then
                For i = ReportFooterCol.Count To 1 Step -1
                    ReportFooterCol.Remove(i)
                Next
            End If
        End Sub

        Private Sub BindCollection(ByVal objTable As DataTable, ByVal Section As String)
            Dim objField As Field
            Dim objDV As DataView = objTable.DefaultView
            If objDV.Count > 0 Then
                objField = New Field
                If objTable.Rows(0).Item("FieldType") <> "Image" And objTable.Rows(0).Item("FieldType") <> "Caption" Then

                    With objField
                        .FieldName = objTable.Rows(0).Item("Name")
                        .FieldText = objTable.Rows(0).Item("Text")
                        .FieldTop = objTable.Rows(0).Item("Top") '* scaley
                        .FieldLeft = objTable.Rows(0).Item("Left") '* scalex
                        .FieldWidth = objTable.Rows(0).Item("Width")
                        .FieldType = objTable.Rows(0).Item("FieldType")
                        .FieldHeight = objTable.Rows(0).Item("Height")

                        .FontName = objTable.Rows(0).Item("FontName")
                        .FontSize = Val(objTable.Rows(0).Item("FontSize"))
                        .FontStyle = objTable.Rows(0).Item("FontStyle")
                    End With
                    Select Case objTable.Rows(0).Item("Text")
                        Case "Current Date"
                            'objField.FieldText = Date.Today.ToShortDateString
                        Case "Current Time"
                            'objField.FieldText = Date.Today.ToShortTimeString
                        Case "Current Page"

                        Case "File Author"

                        Case "Record Count"

                        Case "DisclaimerNotes"

                        Case "SeniorProvider1"
                            If IsNothing(objDataDictionary) Then

                                objDataDictionary = New CustomPrintDBLayer
                            End If
                            Dim Arrlist As ArrayList = objDataDictionary.GetProviders
                            If Not Arrlist Is Nothing Then
                                If Arrlist.Count > 0 Then
                                    Try
                                        If Not IsDBNull(Arrlist(0)) Then
                                            objField.FieldText = Arrlist(0)
                                        End If
                                    Catch ex As Exception
                                        objField.FieldText = ""

                                    End Try

                                Else
                                    objField.FieldText = ""
                                End If
                            Else
                                objField.FieldText = ""
                            End If
                        Case "SeniorProvider2"
                            If IsNothing(objDataDictionary) Then

                                objDataDictionary = New CustomPrintDBLayer
                            End If
                            Dim Arrlist As ArrayList = objDataDictionary.GetProviders
                            If Not Arrlist Is Nothing Then
                                If Arrlist.Count > 0 Then
                                    Try
                                        If Not IsDBNull(Arrlist(1)) Then
                                            objField.FieldText = Arrlist(1)
                                        End If
                                    Catch ex As Exception
                                        objField.FieldText = ""
                                    End Try

                                Else
                                    objField.FieldText = ""
                                End If
                            Else
                                objField.FieldText = ""
                            End If

                        Case Else
                            If IsNothing(objDataDictionary) Then

                                objDataDictionary = New CustomPrintDBLayer
                            End If
                            Dim FieldValue = objDataDictionary.GetData(objField.FieldText)
                            If Not FieldValue Is Nothing Then
                                objField.FieldText = FieldValue
                            End If

                    End Select
                    Select Case Section
                        Case "RHF"
                            ReportHeaderCol.Add(objField)
                        Case "PHF"
                            PageHeaderCol.Add(objField)
                        Case "PFF"
                            PageFooterCol.Add(objField)
                        Case "RFF"
                            ReportFooterCol.Add(objField)

                    End Select

                ElseIf objTable.Rows(0).Item("FieldType") <> "Image" Then
                    With objField
                        .FieldName = objTable.Rows(0).Item("Name")
                        .FieldText = objTable.Rows(0).Item("Text")
                        .FieldTop = objTable.Rows(0).Item("Top") '* scaley
                        .FieldLeft = objTable.Rows(0).Item("Left") '* scalex
                        .FieldWidth = objTable.Rows(0).Item("Width")
                        .FieldType = objTable.Rows(0).Item("FieldType")
                        .FieldHeight = objTable.Rows(0).Item("Height")

                        .FontName = objTable.Rows(0).Item("FontName")

                        .FontSize = Val(objTable.Rows(0).Item("FontSize"))
                        .FontStyle = objTable.Rows(0).Item("FontStyle")
                    End With
                    Select Case Section
                        Case "RHF"
                            ReportHeaderCol.Add(objField)
                        Case "PHF"
                            PageHeaderCol.Add(objField)
                        Case "PFF"
                            PageFooterCol.Add(objField)
                        Case "RFF"
                            ReportFooterCol.Add(objField)
                    End Select

                Else

                    With objField
                        .FieldName = objTable.Rows(0).Item("Name")
                        .FieldText = objTable.Rows(0).Item("Text")
                        .FieldTop = objTable.Rows(0).Item("Top") '* scaley
                        .FieldLeft = objTable.Rows(0).Item("Left") '* scalex
                        .FieldWidth = objTable.Rows(0).Item("Width")
                        .FieldType = objTable.Rows(0).Item("FieldType")
                        .FieldHeight = objTable.Rows(0).Item("Height")
                    End With

                    If objField.FieldText = "ClinicLogo" Then
                        Dim obj As CustomPrintDBLayer
                        obj = New CustomPrintDBLayer
                        Dim objdatatable As DataTable
                        objdatatable = obj.GetClinicLogo()
                        If (IsNothing(objdatatable) = False) Then
                            Dim content As Byte() = CType(objdatatable.Rows(0).Item("ClinicLogo"), Byte())
                            Dim stream As MemoryStream = New MemoryStream(content)
                            objField.FieldImage = Image.FromStream(stream)
                            Try
                                stream.Dispose()
                                stream = Nothing
                            Catch ex As Exception

                            End Try
                            objdatatable.Dispose()
                            objdatatable = Nothing
                        End If
                       
                        obj.Dispose()
                        obj = Nothing
                    ElseIf objField.FieldText = "ProviderSignature" Then
                        Dim obj As CustomPrintDBLayer
                        obj = New CustomPrintDBLayer
                        Dim objdatatable As DataTable
                        objdatatable = obj.GetProviderSign()
                        If (IsNothing(objdatatable) = False) Then
                            Dim content As Byte() = CType(objdatatable.Rows(0).Item("ProviderSignature"), Byte())
                            Dim stream As MemoryStream = New MemoryStream(content)
                            objField.FieldImage = Image.FromStream(stream)
                            Try
                                stream.Dispose()
                                stream = Nothing
                            Catch ex As Exception

                            End Try
                            objdatatable.Dispose()
                            objdatatable = Nothing
                        End If
                        
                        obj.Dispose()
                        obj = Nothing
                    End If
                    Select Case Section
                        Case "RHF"
                            ReportHeaderCol.Add(objField)
                        Case "PHF"
                            PageHeaderCol.Add(objField)
                        Case "PFF"
                            PageFooterCol.Add(objField)
                        Case "RFF"
                            ReportFooterCol.Add(objField)
                    End Select
                End If

            End If
        End Sub
        Private Function BindGrid(ByVal oTable As DataTable) As DataTable
            Dim strQuery As String
            Dim obj As CustomPrintDBLayer
            obj = New CustomPrintDBLayer
            Dim oView As DataView = oTable.DefaultView
            Dim strsort = oTable.Columns(1).ColumnName
            oView.Sort = "[" & strsort & "]"
            strQuery = "select "
            If oView.Count > 1 Then
                For cnt As Int16 = 0 To oView.Count - 2
                    strQuery &= oView.Item(cnt).Item("Text") & "," 'DetailsField(cnt) & ","
                Next
            End If
            Dim strwhereclause As String = ""

            'If m_status = "FaxNC" Then
            '    strwhereclause = " where NarcoticCategory= 'C1'"
            'ElseIf m_status = "PrintN" Then
            '    strwhereclause = " where NarcoticCategory <>'C1'"
            'End If
            strQuery &= oView.Item(oView.Count - 1).Item("Text") & " from RxPrintFaxReport" & strwhereclause 'DetailsField(DetailsField.Count - 1) 
            Dim objTable As DataTable = obj.getReportData(strQuery)
            obj.Dispose()
            obj = Nothing
           
            Return objTable

        End Function
        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called

                    ClearCollection()
                End If

                ' TODO: free shared unmanaged resources
            End If
            Me.disposedValue = True
        End Sub

#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

        Friend Sub New(ByVal objPrintDialog As PrintDialog, ByVal objpnlPresciptionReport As Panel)
            MyBase.New()
            _PrintDialog1 = objPrintDialog
            _pnlPresciptionReport = objpnlPresciptionReport
        End Sub
        Public Sub New()
            MyBase.new()
        End Sub
    End Class
End Namespace