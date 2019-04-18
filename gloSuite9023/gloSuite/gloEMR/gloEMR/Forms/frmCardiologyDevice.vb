Imports gloUserControlLibrary
Imports System.Data.SqlClient
Public Class frmCardiologyDevice


    Dim m_patientID As Int64 = 0
    Dim m_ExamID As Int64 = 0
    Dim m_VisitID As Int64 = 0
    Dim m_ClinicId As Int32 = 0

    Dim mPA_ProcDate As Date
    Dim mPA_Flag As Boolean = False
    Dim mPA_Procedure As String = ""
    Dim mPA_WhichFunction As String = ""

    Private Const COL_COUNT = 15

    Private Const COL_DateofImplant = 0
    Private Const COL_TypeofDevice = 1
    Private Const COL_ProductName = 2
    Private Const COL_DeviceManufacturer = 3
    Private Const COL_ProductSpecifications = 4
    Private Const COL_ProductSerialNumber = 5
    Private Const COL_ManufacturerModelNumber = 6
    Private Const COL_LeadsType = 7
    Private Const COL_DateRemoved = 8
    Private Const COL_PhysicalLocationofDeviceImplant = 9
    Private Const COL_PatientID = 10
    Private Const COL_ExamID = 11
    Private Const COL_VisitID = 12
    Private Const COL_CardiologyDeviceID = 13
    Private Const COL_ClinicID = 14
    Dim cStyleDeviceManf As C1.Win.C1FlexGrid.CellStyle
    '' add ctstyle for combo of c1
    Dim cStyleDeviceType As C1.Win.C1FlexGrid.CellStyle

    Dim cStyleProductName As C1.Win.C1FlexGrid.CellStyle
    Dim cStyleLeadtype As C1.Win.C1FlexGrid.CellStyle

    Dim ds As New DataSet
    Dim dt As DataTable

    Private WithEvents _PatientStrip As gloUC_PatientStrip
    Private WithEvents ogloUC_generalsearch As gloUserControlLibrary.gloUCGeneralSearch
    '

    Private Sub frmCardiologyDevice_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        gloC1FlexStyle.Style(C1Cardiology)

        Try

            _PatientStrip = New gloUC_PatientStrip
            If mPA_Flag = False Then
                _PatientStrip.ShowDetail(m_patientID, gloUC_PatientStrip.enumFormName.PatientEducation)
            Else
                _PatientStrip.ShowDetail(m_patientID, gloUC_PatientStrip.enumFormName.PatientEducation)
            End If

            _PatientStrip.Dock = DockStyle.Top
            _PatientStrip.BringToFront()
            _PatientStrip.Padding = New Padding(3, 0, 3, 0)
            pnlPatientDetails.Controls.Add(_PatientStrip)

            'search with UC
            ogloUC_generalsearch = New gloUCGeneralSearch()
            Panel3.Controls.Add(ogloUC_generalsearch)
            ogloUC_generalsearch.Dock = DockStyle.Fill
            ogloUC_generalsearch.BringToFront()


            DesignC1Grid()
            ' FillC1Grid()
            If mPA_Flag = True Then

                Dim dt1 As DataTable = PopulateDatafromProcAssociation()
                FillC1Grid(dt1)

                If mPA_WhichFunction = "VIEW" Then
                    tlstrip_Delete.Visible = False
                    tlstrip_Save.Visible = False
                    tlstrip_Refresh.Visible = False
                    C1Cardiology.AllowEditing = False
                End If

                ogloUC_generalsearch.IntialiseDatatable(dt1)
            Else
                Dim dt As DataTable = PopulateCardioDeviceData()
                FillC1Grid(dt)
                ogloUC_generalsearch.IntialiseDatatable(dt)
            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function PopulateDatafromProcAssociation() As DataTable
        Try
            'Declaration of variables for making connection
            Dim dt As New DataTable
            Dim cmd As SqlCommand
            Dim sqladpt As SqlDataAdapter

            'Connection string
            Dim conn As SqlConnection = New SqlConnection(GetConnectionString)
            '        cmd = New SqlCommand("Select * from CV_EjectionFraction where nPatientID=" & _PatientID & " and nExamID=" & _ExamID & " and nVisitID=" & _VisitID, conn)
            'Dim strquery As String = "Select isnull(nEjectionFractionID,0)as EjectionFractionID,isnull(nPatientID,0)as PatientID,isnull(nExamID,0)as ExamID,isnull(nVisitID,0)as VisitID,isnull(nClinicID,0)as ClinicID,isnull(dtDateofTest,0)as TestDate,isnull(sModalityTest,'')as ModalityTest,isnull(sQuantityPercent,0)as QuantityPercent,isnull(sQuantityDesc,0)as QuantityDescription from CV_EjectionFraction  where nPatientID=" & _PatientID & " and nVisitID=" & _VisitID
            Dim strquery As String = "SELECT ISNULL(CV_CardiologyDevice.nCardiologyDeviceID, 0) AS CardiologyDeviceID, ISNULL(CV_CardiologyDevice.nPatientID, 0) AS PatientID, ISNULL(CV_CardiologyDevice.nExamID, 0) AS ExamID, ISNULL(CV_CardiologyDevice.nVisitID, 0) AS VisitID, ISNULL(CV_CardiologyDevice.nClinicID, 0) AS ClinicID, ISNULL(CV_CardiologyDevice.dtDateofImplant, 0) AS DateofImplant, ISNULL(CV_CardiologyDevice.sDeviceType, '') AS DeviceType, ISNULL(CV_CardiologyDevice.sProductName, '') AS ProductName, ISNULL(CV_CardiologyDevice.sDeviceManufacturer, '') AS DeviceManufacturer, ISNULL(CV_CardiologyDevice.sProductSpecification, '') AS ProductSpecification, ISNULL(CV_CardiologyDevice.sProductSerialNo, '') AS ProductSerialNo, ISNULL(CV_CardiologyDevice.sManufacturerModelNo, '') AS sManufacturerModelNo, ISNULL(CV_CardiologyDevice.sLeadType, '') AS LeadType, CV_CardiologyDevice.dtDateRemoved  AS DateRemoved, ISNULL(CV_CardiologyDevice.sPhysicalLocation, '') AS PhysicalLocation FROM CV_CardiologyDevice INNER JOIN CV_ProcedureDeviceAssociation ON CV_CardiologyDevice.nCardiologyDeviceID = CV_ProcedureDeviceAssociation.nProcDeviceAssociationID where CV_ProcedureDeviceAssociation.nPatientID = " & m_patientID & " and CV_ProcedureDeviceAssociation.nExamID=" & m_ExamID & " And CV_ProcedureDeviceAssociation.nVisitID = " & m_VisitID & " and CV_ProcedureDeviceAssociation.nClinicID = " & m_ClinicId & " and CV_ProcedureDeviceAssociation.dtProceduredate='" & mPA_ProcDate & "' and CV_ProcedureDeviceAssociation.sProcedures='" & mPA_Procedure & "'"
            '"select isnull(nCardiologyDeviceID,0)as CardiologyDeviceID,isnull(nPatientID,0) as PatientID, isnull(nExamID,0) as ExamID, isnull(nVisitID,0)as VisitID , isnull(nClinicID,0) as ClinicID, isnull(dtDateofImplant,0)as DateofImplant, isnull(sDeviceType,'') as DeviceType, isnull(sProductName,'')as ProductName, isnull(sDeviceManufacturer,'') as DeviceManufacturer,isnull(sProductSpecification,'') as ProductSpecification, isnull(sProductSerialNo,'') as ProductSerialNo ,isnull(sManufacturerModelNo,'') as sManufacturerModelNo, isnull(sLeadType,'') as LeadType, isnull(dtDateRemoved,'') as DateRemoved, isnull(sPhysicalLocation,'') as PhysicalLocation from dbo.CV_CardiologyDevice where nPatientID = " & m_PatientID & " and nExamID=" & m_ExamID & " And nVisitID = " & m_VisitID & " and nClinicID = " & m_ClinicID & " and dtDateofImplant='" & mPA_ProcDate & "'"
            cmd = New SqlCommand(strquery, conn)
            sqladpt = New SqlDataAdapter(cmd)

            'Fill data adapter
            sqladpt.Fill(dt)

            'Return Data table
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Function

    Private Function PopulateCardioDeviceData() As DataTable
        Try
            'Declaration of variables for making connection
            Dim dt As New DataTable
            Dim cmd As SqlCommand
            Dim sqladpt As SqlDataAdapter

            'Connection string
            Dim conn As SqlConnection = New SqlConnection(GetConnectionString)
            '        cmd = New SqlCommand("Select * from CV_EjectionFraction where nPatientID=" & _PatientID & " and nExamID=" & _ExamID & " and nVisitID=" & _VisitID, conn)
            'Dim strquery As String = "Select isnull(nEjectionFractionID,0)as EjectionFractionID,isnull(nPatientID,0)as PatientID,isnull(nExamID,0)as ExamID,isnull(nVisitID,0)as VisitID,isnull(nClinicID,0)as ClinicID,isnull(dtDateofTest,0)as TestDate,isnull(sModalityTest,'')as ModalityTest,isnull(sQuantityPercent,0)as QuantityPercent,isnull(sQuantityDesc,0)as QuantityDescription from CV_EjectionFraction  where nPatientID=" & _PatientID & " and nVisitID=" & _VisitID
            Dim strquery As String = "select isnull(nCardiologyDeviceID,0)as CardiologyDeviceID,isnull(nPatientID,0) as PatientID, isnull(nExamID,0) as ExamID, isnull(nVisitID,0)as VisitID , isnull(nClinicID,0) as ClinicID, isnull(dtDateofImplant,0)as DateofImplant, isnull(sDeviceType,'') as DeviceType, isnull(sProductName,'')as ProductName, isnull(sDeviceManufacturer,'') as DeviceManufacturer,isnull(sProductSpecification,'') as ProductSpecification, isnull(sProductSerialNo,'') as ProductSerialNo ,isnull(sManufacturerModelNo,'') as sManufacturerModelNo, isnull(sLeadType,'') as LeadType,dtDateRemoved as DateRemoved, isnull(sPhysicalLocation,'') as PhysicalLocation from dbo.CV_CardiologyDevice where nPatientID = " & m_patientID & " And nVisitID = " & m_VisitID & ""
            cmd = New SqlCommand(strquery, conn)
            sqladpt = New SqlDataAdapter(cmd)

            'Fill data adapter
            sqladpt.Fill(dt)

            'Return Data table
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Function

    Private Function GetProductData() As DataTable
        Dim Connection As New SqlConnection(GetConnectionString)
        Try
            Connection.Open()
            Dim CommandString As String = "select sDescription from category_mst where sCategoryType='Cardio Product Name'"

            Dim adp As New SqlDataAdapter(CommandString, Connection)
            ds.Clear()
            adp.Fill(ds)
            dt = ds.Tables(0)
            Return dt

            Connection.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Function GetDeviceTypeData() As DataTable
        Dim Connection As New SqlConnection(GetConnectionString)
        Try
            Connection.Open()
            Dim CommandString As String = "select sDescription from category_mst where sCategoryType='Cardio Device Type'"

            Dim adp As New SqlDataAdapter(CommandString, Connection)
            ds.Clear()
            adp.Fill(ds)
            dt = ds.Tables(0)
            Return dt
            'setDataGridStyle(dt)
            'DataGrid.DataSource = ds.Tables(0)
            'DataGrid.Show()
            Connection.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Function GetDevicemanfData() As DataTable
        Dim Connection As New SqlConnection(GetConnectionString)
        Try
            Connection.Open()
            Dim CommandString As String = "select sDescription from category_mst where sCategoryType='Cardio Device Manf.'"

            Dim adp As New SqlDataAdapter(CommandString, Connection)
            ds.Clear()
            adp.Fill(ds)
            dt = ds.Tables(0)
            Return dt
            'setDataGridStyle(dt)
            'DataGrid.DataSource = ds.Tables(0)
            'DataGrid.Show()
            'Connection.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Connection.State = ConnectionState.Open Then
                Connection.Close()
            End If
            Connection = Nothing
        End Try
    End Function

    Private Function GetLeadData() As DataTable
        Dim Connection As New SqlConnection(GetConnectionString)
        Try
            Connection.Open()
            Dim CommandString As String = "select sDescription from category_mst where sCategoryType='Cardio Lead Type'"

            Dim adp As New SqlDataAdapter(CommandString, Connection)
            ds.Clear()
            adp.Fill(ds)
            dt = ds.Tables(0)
            Return dt
            'setDataGridStyle(dt)
            'DataGrid.DataSource = ds.Tables(0)
            'DataGrid.Show()
            ' Connection.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Connection.State = ConnectionState.Open Then
                Connection.Close()
            End If
            Connection = Nothing
        End Try
    End Function

    Private Sub DesignC1Grid()
        Try

            With C1Cardiology
                C1Cardiology.DataSource = Nothing
                .Clear()

                ' 'Setfont
                gloC1FlexStyle.Style(C1Cardiology)
                '.Font = New System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Regular)
                .SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
                '.BackColor = System.Drawing.Color.White
                .AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None

                .Cols.Count = COL_COUNT
                '.Cols.Fixed = 1

                .Rows.Count = 1
                '.Rows.Fixed = 1

                'set col visible 
                'to visible falsse
                .Cols(COL_DateofImplant).Visible = True
                .Cols(COL_TypeofDevice).Visible = True
                .Cols(COL_ProductName).Visible = True
                .Cols(COL_DeviceManufacturer).Visible = True
                .Cols(COL_ProductSpecifications).Visible = True
                .Cols(COL_ProductSerialNumber).Visible = True
                .Cols(COL_ManufacturerModelNumber).Visible = True
                .Cols(COL_LeadsType).Visible = True
                .Cols(COL_DateRemoved).Visible = True
                .Cols(COL_PhysicalLocationofDeviceImplant).Visible = True
                .Cols(COL_PatientID).Visible = False
                .Cols(COL_ExamID).Visible = False
                .Cols(COL_VisitID).Visible = False
                .Cols(COL_CardiologyDeviceID).Visible = False
                .Cols(COL_ClinicID).Visible = False


                'set col allow property
                .Cols(COL_DateofImplant).AllowEditing = True
                .Cols(COL_TypeofDevice).AllowEditing = True
                .Cols(COL_ProductName).AllowEditing = True
                .Cols(COL_DeviceManufacturer).AllowEditing = True
                .Cols(COL_ProductSpecifications).AllowEditing = True
                .Cols(COL_ProductSerialNumber).AllowEditing = True
                .Cols(COL_ManufacturerModelNumber).AllowEditing = True
                .Cols(COL_LeadsType).AllowEditing = True
                .Cols(COL_DateRemoved).AllowEditing = True
                .Cols(COL_PhysicalLocationofDeviceImplant).AllowEditing = True
                .Cols(COL_PatientID).AllowEditing = False
                .Cols(COL_ExamID).AllowEditing = False
                .Cols(COL_VisitID).AllowEditing = False
                .Cols(COL_CardiologyDeviceID).AllowEditing = False
                .Cols(COL_ClinicID).AllowEditing = False

                'set col width            
                Dim cWidth As Int32 = pnlPatientDetails.Width

                '.Cols(COL_HIDDENAPPLICATIONNAME).Width = Convert.ToInt32(cWidth * 0.2)
                .Cols(COL_DateofImplant).Width = Convert.ToInt32(cWidth * 0.1)
                .Cols(COL_TypeofDevice).Width = Convert.ToInt32(cWidth * 0.2)
                .Cols(COL_ProductName).Width = Convert.ToInt32(cWidth * 0.2)
                .Cols(COL_DeviceManufacturer).Width = Convert.ToInt32(cWidth * 0.2)
                .Cols(COL_ProductSpecifications).Width = Convert.ToInt32(cWidth * 0.2)
                .Cols(COL_ProductSerialNumber).Width = Convert.ToInt32(cWidth * 0.2)
                .Cols(COL_DateofImplant).Width = Convert.ToInt32(cWidth * 0.2)
                .Cols(COL_ManufacturerModelNumber).Width = Convert.ToInt32(cWidth * 0.2)
                .Cols(COL_LeadsType).Width = Convert.ToInt32(cWidth * 0.2)
                .Cols(COL_DateRemoved).Width = Convert.ToInt32(cWidth * 0.2)
                .Cols(COL_PhysicalLocationofDeviceImplant).Width = Convert.ToInt32(cWidth * 0.2)
                .Cols(COL_PatientID).Width = 0
                .Cols(COL_ExamID).Width = 0
                .Cols(COL_VisitID).Width = 0
                .Cols(COL_CardiologyDeviceID).Width = 0
                .Cols(COL_ClinicID).Width = 0

                'Dim SendingApplicationName As String = ""
                'SendingApplicationName = "Sending Application ID|Sending Application Universal ID|Sending Application Type"
                'Dim devicetype As String = ""
                'set col datatype
                .Cols(COL_DateofImplant).DataType = GetType(DateTime)
                .Cols(COL_TypeofDevice).DataType = GetType(String)
                '.Cols(COL_TypeofDevice).ComboList = devicetype
                .Cols(COL_ProductName).DataType = GetType(String)
                .Cols(COL_DeviceManufacturer).DataType = GetType(String)
                .Cols(COL_ProductSpecifications).DataType = GetType(String)
                .Cols(COL_ProductSerialNumber).DataType = GetType(String)
                .Cols(COL_ManufacturerModelNumber).DataType = GetType(String)
                .Cols(COL_LeadsType).DataType = GetType(String)
                .Cols(COL_DateRemoved).DataType = GetType(DateTime)
                .Cols(COL_PhysicalLocationofDeviceImplant).DataType = GetType(String)
                .Cols(COL_PatientID).DataType = GetType(Int64)
                .Cols(COL_ExamID).DataType = GetType(Int64)
                .Cols(COL_VisitID).DataType = GetType(Int64)
                .Cols(COL_CardiologyDeviceID).DataType = GetType(Int64)
                .Cols(COL_ClinicID).DataType = GetType(Int32)

                'set Heading
                .SetData(0, COL_DateofImplant, "Date of Implant")
                .SetData(0, COL_TypeofDevice, "Type of Device")
                .SetData(0, COL_ProductName, "Product Name ")
                .SetData(0, COL_DeviceManufacturer, "Device Manufacturer")
                .SetData(0, COL_ProductSpecifications, "Product Specifications")
                .SetData(0, COL_ProductSerialNumber, "Product Serial Number")
                .SetData(0, COL_ManufacturerModelNumber, "Manufacturer Model Number")
                .SetData(0, COL_LeadsType, "Leads Type")
                .SetData(0, COL_DateRemoved, "Date Removed")
                .SetData(0, COL_PhysicalLocationofDeviceImplant, "Physical Location of Device Implant")
                .SetData(0, COL_PatientID, "PatientID")
                .SetData(0, COL_ExamID, "ExamID")
                .SetData(0, COL_VisitID, "VisitID")
                .SetData(0, COL_ExamID, "CardiologyDeviceID")
                .SetData(0, COL_VisitID, "ClinicID")

                '.Cols(Col_SendingApplication).DataType = GetType(String)
                '.Cols(Col_SendingApplication).ComboList = SendingApplicationName
                '.Cols(Col_SendingApplication).AllowEditing = True
                'set Heading
                '.SetData(0, COL_HIDDENAPPLICATIONNAME, "System ApplicationName")
                '.SetData(0, COL_ApplName, "Sending Application Name")
                '.SetData(0, COL_FACILITYNAME, "Sending Facility Name")
            End With
            'C1Client.EndInit()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'C1Client.EndInit()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub FillC1Grid(Optional ByVal datatable = Nothing)

        Try

            'Dim r As C1.Win.C1FlexGrid.Row
            'r = C1Cardiology.Rows.Add()
            'C1Cardiology.SetData(r.Index, COL_PatientID, m_patientID)
            'C1Cardiology.SetData(r.Index, COL_ExamID, m_ExamID)
            'C1Cardiology.SetData(r.Index, COL_VisitID, m_VisitID)
            ' C1Cardiology.SetData(r.Index, COL_CardiologyDeviceID, 0)

            Dim i As Int16
            C1Cardiology.Dock = DockStyle.Fill
            Dim _TotalWidth As Single = 0
            _TotalWidth = (C1Cardiology.Width - 20) / 11
            'Dim cStyle As C1.Win.C1FlexGrid.CellStyle

            C1Cardiology.Cols.Count = COL_COUNT
            C1Cardiology.Rows.Count = 1
            C1Cardiology.AllowEditing = True
            C1Cardiology.AllowAddNew = True

            C1Cardiology.Styles.ClearUnused()

            Dim dtDevicetype As DataTable
            dtDevicetype = New DataTable()
            dtDevicetype = GetDeviceTypeData()

            Dim strComboString As String = " "
            For icnt As Int32 = 0 To dtDevicetype.Rows.Count - 1
                strComboString = strComboString & "|" & dtDevicetype.Rows(icnt)(0).ToString
            Next

            'Dim rgOperator As C1.Win.C1FlexGrid.CellRange = C1Cardiology.GetCellRange(r.Index, COL_TypeofDevice, r.Index, COL_TypeofDevice)
            cStyleDeviceType = C1Cardiology.Styles.Add("Devicetype")
            cStyleDeviceType.ComboList = strComboString
            strComboString = ""
            'rgOperator.Style = cStyleDeviceType

            Dim dtProductname As DataTable
            dtProductname = New DataTable()
            dtProductname = GetProductData()
            strComboString = ""
            For i = 0 To dtProductname.Rows.Count - 1
                strComboString = strComboString & "|" & dtProductname.Rows(i)(0).ToString
            Next

            'Dim rgOperator1 As C1.Win.C1FlexGrid.CellRange = C1Cardiology.GetCellRange(r.Index, COL_ProductName, r.Index, COL_ProductName)
            cStyleProductName = C1Cardiology.Styles.Add("ProductionName")
            cStyleProductName.ComboList = strComboString
            strComboString = ""
            'rgOperator1.Style = cStyleProductName

            Dim dtDeviceManf As DataTable
            dtDeviceManf = New DataTable()
            dtDeviceManf = GetDevicemanfData()
            strComboString = ""
            For i = 0 To dtDeviceManf.Rows.Count - 1
                strComboString = strComboString & "|" & dtDeviceManf.Rows(i)(0).ToString
            Next

            'Dim rgOperator2 As C1.Win.C1FlexGrid.CellRange = C1Cardiology.GetCellRange(r.Index, COL_DeviceManufacturer, r.Index, COL_DeviceManufacturer)
            cStyleDeviceManf = C1Cardiology.Styles.Add("DeviceManf")
            cStyleDeviceManf.ComboList = strComboString
            strComboString = ""
            'rgOperator2.Style = cStyleDeviceManf

            Dim dtLeadtype As DataTable
            dtLeadtype = New DataTable()
            dtLeadtype = GetLeadData()
            strComboString = ""
            For i = 0 To dtLeadtype.Rows.Count - 1
                strComboString = strComboString & "|" & dtLeadtype.Rows(i)(0).ToString
            Next

            'Dim rgOperator3 As C1.Win.C1FlexGrid.CellRange = C1Cardiology.GetCellRange(r.Index, COL_LeadsType, r.Index, COL_LeadsType)
            cStyleLeadtype = C1Cardiology.Styles.Add("Leadtype")
            cStyleLeadtype.ComboList = strComboString
            strComboString = ""
            ' rgOperator3.Style = cStyleLeadtype
            C1Cardiology.SetCellStyle(1, COL_TypeofDevice, cStyleDeviceType)

            C1Cardiology.SetCellStyle(1, COL_ProductName, cStyleProductName)

            C1Cardiology.SetCellStyle(1, COL_DeviceManufacturer, cStyleDeviceManf)

            C1Cardiology.SetCellStyle(1, COL_LeadsType, cStyleLeadtype)
            If IsNothing(datatable) Then
                C1Cardiology.SetData(1, COL_PatientID, datatable.Rows(i)("PatientID"))
                C1Cardiology.SetData(1, COL_ExamID, datatable.Rows(i)("ExamID"))
                C1Cardiology.SetData(1, COL_VisitID, datatable.Rows(i)("VisitID"))
                C1Cardiology.SetData(1, COL_ClinicID, datatable.Rows(i)("ClinicID"))
            End If
            ''dt fill
            If Not IsNothing(datatable) Then
                If Not IsDBNull(datatable) Then
                    If datatable.Rows.count > 0 Then

                        With C1Cardiology
                            For i = 0 To datatable.Rows.Count - 1
                                .Rows.Add()

                                ''''Set Column Style 
                                '''' Assinge the Cell for ComboBox
                                'Dim rgDia As CellRange = .GetCellRange(.Rows.Count - 1, COL_DIAGNOSIS)
                                'rgDia.Style = csDia  '' .Styles.Add("Dia")
                                '''' Assinge the Cell for ComboBox
                                'Dim rgStatus As CellRange = .GetCellRange(.Rows.Count - 1, COL_STATUS)
                                'rgStatus.Style = csStatus ''''  .Styles.Add("Status")
                                '' Fill the Retrived information to relative controls
                                'dtpDOS.Value = Format(dt.Rows(i)("dtDOS"), "MM/dd/yyyy")

                                .SetCellStyle(i + 1, COL_TypeofDevice, cStyleDeviceType)

                                .SetCellStyle(i + 1, COL_ProductName, cStyleProductName)

                                .SetCellStyle(i + 1, COL_DeviceManufacturer, cStyleDeviceManf)

                                .SetCellStyle(i + 1, COL_LeadsType, cStyleLeadtype)

                                .SetData(i + 1, COL_DateofImplant, datatable.Rows(i)("DateofImplant"))
                                .SetData(i + 1, COL_ProductSpecifications, datatable.Rows(i)("ProductSpecification"))
                                .SetData(i + 1, COL_ProductSerialNumber, datatable.Rows(i)("ProductSerialNo"))
                                .SetData(i + 1, COL_ManufacturerModelNumber, datatable.Rows(i)("sManufacturerModelNo"))
                                '.SetData(i + 1, COL_DateRemoved, Format(datatable.Rows(i)("DateRemoved"), "MM/dd/yyyy"))
                                If Not IsDBNull(datatable.Rows(i)("DateRemoved")) Then
                                    .SetData(i + 1, COL_DateRemoved, datatable.Rows(i)("DateRemoved"))
                                End If
                                .SetData(i + 1, COL_PhysicalLocationofDeviceImplant, datatable.Rows(i)("PhysicalLocation"))
                                .SetData(i + 1, COL_PatientID, datatable.Rows(i)("PatientID"))
                                .SetData(i + 1, COL_ExamID, datatable.Rows(i)("ExamID"))
                                .SetData(i + 1, COL_VisitID, datatable.Rows(i)("VisitID"))
                                .SetData(i + 1, COL_ClinicID, datatable.Rows(i)("ClinicID"))
                                .SetData(i + 1, COL_CardiologyDeviceID, datatable.Rows(i)("CardiologyDeviceID"))
                                .SetData(i + 1, COL_TypeofDevice, datatable.Rows(i)("DeviceType"))

                                .SetData(i + 1, COL_ProductName, datatable.Rows(i)("ProductName"))

                                .SetData(i + 1, COL_DeviceManufacturer, datatable.Rows(i)("DeviceManufacturer"))

                                .SetData(i + 1, COL_LeadsType, datatable.Rows(i)("LeadType"))



                            Next
                        End With

                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub New(ByVal PatientID As Int64, ByVal ExamID As Int64, ByVal VisitID As Int64)

        InitializeComponent()

        m_patientID = PatientID
        m_ExamID = ExamID
        m_VisitID = VisitID

    End Sub

    Public Sub New(ByVal PatientID As Int64, ByVal examid As Int64, ByVal visitid As Int64, ByVal Clinicid As Int64, ByVal ProcDate As Date, ByVal sProcedure As String, ByVal ForFunct As String)
        InitializeComponent()

        m_patientID = PatientID
        m_ExamID = examid
        m_VisitID = visitid
        m_ClinicId = Clinicid
        mPA_ProcDate = ProcDate
        mPA_Flag = True
        mPA_Procedure = sProcedure
        mPA_WhichFunction = ForFunct

    End Sub

    Private Sub tlstrip_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlstrip_Close.Click
        ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.Close, "Closed CardioLogy device", gloAuditTrail.ActivityOutCome.Success)
        ''Added Rahul P on 20101011
        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.Close, "Closed CardioLogy device", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        ''
        'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Other, "Closed CardioLogy device ", gstrLoginName, gstrClientMachineName, m_patientID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
        Me.Close()

    End Sub

    ''save

    Private Sub tlstrip1_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim patientID As Int64 = 0
        Dim examID As Int64 = 0
        Dim visitID As Int64 = 0
        Dim nClinicID As Int64 = 0

        Dim dtDateofImplant As String = ""
        Dim sDeviceType As String = ""
        Dim sProductName As String = ""
        Dim sDeviceManufacturer As String = ""
        Dim sProductSpecification As String = ""

        Dim sProductSerialNo As String = ""
        Dim sManufacturerModelNo As String = ""
        Dim sLeadType As String = ""
        Dim dtDateRemoved As String = ""
        Dim sPhysicalLocation As String = ""

        Dim oClsCardioDeviceCollection As ClsCardioDeviceCollection
        Dim oClsCardioDeviceCollection_Update As ClsCardioDeviceCollection
        Dim oClsCardiologyDevice As clsCardiologyDevice

        Try
            C1Cardiology.Row = 0
            C1Cardiology.RowSel = 0

            oClsCardioDeviceCollection = New ClsCardioDeviceCollection()
            oClsCardioDeviceCollection_Update = New ClsCardioDeviceCollection()

            For i As Int32 = 1 To C1Cardiology.Rows.Count - 1
                If CType(C1Cardiology.GetData(i, COL_CardiologyDeviceID), Long) = 0 Then
                    If CType(C1Cardiology.GetData(i, COL_DateofImplant), String) <> "" Then

                        oClsCardiologyDevice = New clsCardiologyDevice()
                        oClsCardiologyDevice.CardiologyDeviceID = GenerateUnique_CardioDeviceID(m_patientID)


                        C1Cardiology.SetData(i, COL_CardiologyDeviceID, oClsCardiologyDevice.CardiologyDeviceID)

                        If mPA_Flag = True Then
                            oClsCardiologyDevice.PatientID = m_patientID
                            oClsCardiologyDevice.ExamID = m_ExamID
                            oClsCardiologyDevice.VisitID = m_VisitID
                            oClsCardiologyDevice.ClinicID = m_ClinicId
                            'oClsCardiologyDevice.DateofImplant = mPA_ProcDate
                        Else
                            oClsCardiologyDevice.PatientID = m_patientID
                            oClsCardiologyDevice.ExamID = m_ExamID
                            oClsCardiologyDevice.VisitID = m_VisitID
                        End If
                        C1Cardiology.SetData(i, COL_PatientID, oClsCardiologyDevice.PatientID)

                        C1Cardiology.SetData(i, COL_ExamID, oClsCardiologyDevice.ExamID)

                        C1Cardiology.SetData(i, COL_VisitID, oClsCardiologyDevice.VisitID)

                        oClsCardiologyDevice.ClinicID = 1

                        oClsCardiologyDevice.DateofImplant = C1Cardiology.GetData(i, COL_DateofImplant)
                        oClsCardiologyDevice.DeviceType = C1Cardiology(i, COL_TypeofDevice)
                        oClsCardiologyDevice.ProductName = C1Cardiology(i, COL_ProductName)
                        oClsCardiologyDevice.DeviceManufacturer = C1Cardiology(i, COL_DeviceManufacturer)
                        oClsCardiologyDevice.ProductSpecification = C1Cardiology(i, COL_ProductSpecifications)
                        oClsCardiologyDevice.ProductSerialNo = C1Cardiology(i, COL_ProductSerialNumber)
                        oClsCardiologyDevice.ManufacturerModelNo = C1Cardiology(i, COL_ManufacturerModelNumber)
                        oClsCardiologyDevice.LeadType = C1Cardiology(i, COL_LeadsType)
                        oClsCardiologyDevice.DateRemoved = C1Cardiology.GetData(i, COL_DateRemoved)
                        oClsCardiologyDevice.PhysicalLocation = C1Cardiology(i, COL_PhysicalLocationofDeviceImplant)

                        oClsCardioDeviceCollection.Add(oClsCardiologyDevice)


                    End If
                Else
                    oClsCardiologyDevice = New clsCardiologyDevice()

                    oClsCardiologyDevice.CardiologyDeviceID = CType(C1Cardiology.GetData(i, COL_CardiologyDeviceID), Long)

                    oClsCardiologyDevice.PatientID = CType(C1Cardiology.GetData(i, COL_PatientID), Long)
                    oClsCardiologyDevice.ExamID = CType(C1Cardiology.GetData(i, COL_ExamID), Long)
                    oClsCardiologyDevice.VisitID = CType(C1Cardiology.GetData(i, COL_VisitID), Long)
                    'nClinicID=C1Cardiology.GetData(i, col)
                    oClsCardiologyDevice.ClinicID = 1
                    oClsCardiologyDevice.DateofImplant = C1Cardiology.GetData(i, COL_DateofImplant)
                    oClsCardiologyDevice.DeviceType = C1Cardiology(i, COL_TypeofDevice)
                    oClsCardiologyDevice.ProductName = C1Cardiology(i, COL_ProductName)
                    oClsCardiologyDevice.DeviceManufacturer = C1Cardiology(i, COL_DeviceManufacturer)
                    oClsCardiologyDevice.ProductSpecification = C1Cardiology(i, COL_ProductSpecifications)
                    oClsCardiologyDevice.ProductSerialNo = C1Cardiology(i, COL_ProductSerialNumber)
                    oClsCardiologyDevice.ManufacturerModelNo = C1Cardiology(i, COL_ManufacturerModelNumber)
                    oClsCardiologyDevice.LeadType = C1Cardiology(i, COL_LeadsType)
                    oClsCardiologyDevice.DateRemoved = C1Cardiology.GetData(i, COL_DateRemoved)
                    oClsCardiologyDevice.PhysicalLocation = C1Cardiology(i, COL_PhysicalLocationofDeviceImplant)

                    oClsCardioDeviceCollection_Update.Add(oClsCardiologyDevice)

                    ''\\ now Update CardioDevice Data
                End If
            Next

            'for Inserting data
            If Not IsNothing(oClsCardioDeviceCollection) Then
                'To Commit changes Last Row in C1Grid 
                If Not IsNothing(C1Cardiology) Then
                    If C1Cardiology.Rows.Count > 0 Then
                        C1Cardiology.RowSel = 0
                    End If
                End If

                If oClsCardioDeviceCollection.Count > 0 Then
                    If mPA_Flag = True Then '\\ save data in both table-> CV_ProcedureDeviceAssociation & CV_CardioDevice
                        If SaveCardioDeviceData(oClsCardioDeviceCollection) Then
                        End If
                        If SaveProcdureDeviceAssociation(oClsCardioDeviceCollection) Then
                        End If
                    Else
                        If SaveCardioDeviceData(oClsCardioDeviceCollection) Then
                        End If
                    End If

                End If
            End If
            'for Updating data
            If Not IsNothing(oClsCardioDeviceCollection_Update) Then

                'To Commit changes Last Row in C1Grid 
                If Not IsNothing(C1Cardiology) Then
                    If C1Cardiology.Rows.Count > 0 Then
                        C1Cardiology.RowSel = 0
                    End If
                End If

                If oClsCardioDeviceCollection_Update.Count > 0 Then
                    If UpdateCardioDeviceData(oClsCardioDeviceCollection_Update) Then
                    End If
                End If
            End If

            oClsCardioDeviceCollection = Nothing
            oClsCardioDeviceCollection_Update = Nothing

            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tlstrip_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlstrip_Save.Click
        Dim patientID As Int64 = 0
        Dim examID As Int64 = 0
        Dim visitID As Int64 = 0
        Dim nClinicID As Int64 = 0

        Dim dtDateofImplant As String = ""
        Dim sDeviceType As String = ""
        Dim sProductName As String = ""
        Dim sDeviceManufacturer As String = ""
        Dim sProductSpecification As String = ""

        Dim sProductSerialNo As String = ""
        Dim sManufacturerModelNo As String = ""
        Dim sLeadType As String = ""
        Dim dtDateRemoved As String = ""
        Dim sPhysicalLocation As String = ""

        Dim oClsCardioDeviceCollection As ClsCardioDeviceCollection
        Dim oClsCardioDeviceCollection_Update As ClsCardioDeviceCollection
        Dim oClsCardiologyDevice As clsCardiologyDevice

        Try
            C1Cardiology.Row = 0
            C1Cardiology.RowSel = 0

            oClsCardioDeviceCollection = New ClsCardioDeviceCollection()
            oClsCardioDeviceCollection_Update = New ClsCardioDeviceCollection()

            For i As Int32 = 1 To C1Cardiology.Rows.Count - 1
                If CType(C1Cardiology.GetData(i, COL_CardiologyDeviceID), Long) = 0 Then
                    If CType(C1Cardiology.GetData(i, COL_DateofImplant), String) <> "" Then

                        oClsCardiologyDevice = New clsCardiologyDevice()
                        ''oClsCardiologyDevice.CardiologyDeviceID = GenerateUnique_CardioDeviceID(m_patientID)


                        C1Cardiology.SetData(i, COL_CardiologyDeviceID, oClsCardiologyDevice.CardiologyDeviceID)

                        If mPA_Flag = True Then
                            oClsCardiologyDevice.PatientID = m_patientID
                            oClsCardiologyDevice.ExamID = m_ExamID
                            oClsCardiologyDevice.VisitID = m_VisitID
                            oClsCardiologyDevice.ClinicID = m_ClinicId
                            'oClsCardiologyDevice.DateofImplant = mPA_ProcDate
                        Else
                            oClsCardiologyDevice.PatientID = m_patientID
                            oClsCardiologyDevice.ExamID = m_ExamID
                            oClsCardiologyDevice.VisitID = m_VisitID
                        End If
                        C1Cardiology.SetData(i, COL_PatientID, oClsCardiologyDevice.PatientID)

                        C1Cardiology.SetData(i, COL_ExamID, oClsCardiologyDevice.ExamID)

                        C1Cardiology.SetData(i, COL_VisitID, oClsCardiologyDevice.VisitID)

                        oClsCardiologyDevice.ClinicID = gnClinicID

                        oClsCardiologyDevice.DateofImplant = C1Cardiology.GetData(i, COL_DateofImplant)
                        oClsCardiologyDevice.DeviceType = C1Cardiology(i, COL_TypeofDevice) & ""
                        oClsCardiologyDevice.ProductName = C1Cardiology(i, COL_ProductName) & ""
                        oClsCardiologyDevice.DeviceManufacturer = C1Cardiology(i, COL_DeviceManufacturer) & ""
                        oClsCardiologyDevice.ProductSpecification = C1Cardiology(i, COL_ProductSpecifications) & ""
                        oClsCardiologyDevice.ProductSerialNo = C1Cardiology(i, COL_ProductSerialNumber) & ""
                        oClsCardiologyDevice.ManufacturerModelNo = C1Cardiology(i, COL_ManufacturerModelNumber) & ""
                        oClsCardiologyDevice.LeadType = C1Cardiology(i, COL_LeadsType) & ""
                        oClsCardiologyDevice.DateRemoved = C1Cardiology.GetData(i, COL_DateRemoved)
                        oClsCardiologyDevice.PhysicalLocation = C1Cardiology(i, COL_PhysicalLocationofDeviceImplant) & ""
                        oClsCardiologyDevice.Procedures = mPA_Procedure & ""
                        oClsCardiologyDevice.ProcedureDate = mPA_ProcDate
                        oClsCardioDeviceCollection.Add(oClsCardiologyDevice)


                    End If
                Else
                    oClsCardiologyDevice = New clsCardiologyDevice()

                    oClsCardiologyDevice.CardiologyDeviceID = CType(C1Cardiology.GetData(i, COL_CardiologyDeviceID), Long)

                    oClsCardiologyDevice.PatientID = CType(C1Cardiology.GetData(i, COL_PatientID), Long)
                    oClsCardiologyDevice.ExamID = CType(C1Cardiology.GetData(i, COL_ExamID), Long)
                    oClsCardiologyDevice.VisitID = CType(C1Cardiology.GetData(i, COL_VisitID), Long)
                    'nClinicID=C1Cardiology.GetData(i, col)
                    oClsCardiologyDevice.ClinicID = gnClinicID
                    oClsCardiologyDevice.DateofImplant = C1Cardiology.GetData(i, COL_DateofImplant)
                    oClsCardiologyDevice.DeviceType = C1Cardiology(i, COL_TypeofDevice) & ""
                    oClsCardiologyDevice.ProductName = C1Cardiology(i, COL_ProductName) & ""
                    oClsCardiologyDevice.DeviceManufacturer = C1Cardiology(i, COL_DeviceManufacturer) & ""
                    oClsCardiologyDevice.ProductSpecification = C1Cardiology(i, COL_ProductSpecifications) & ""
                    oClsCardiologyDevice.ProductSerialNo = C1Cardiology(i, COL_ProductSerialNumber) & ""
                    oClsCardiologyDevice.ManufacturerModelNo = C1Cardiology(i, COL_ManufacturerModelNumber) & ""
                    oClsCardiologyDevice.LeadType = C1Cardiology(i, COL_LeadsType) & ""
                    oClsCardiologyDevice.DateRemoved = C1Cardiology.GetData(i, COL_DateRemoved)
                    oClsCardiologyDevice.PhysicalLocation = C1Cardiology(i, COL_PhysicalLocationofDeviceImplant) & ""
                    oClsCardiologyDevice.Procedures = mPA_Procedure & ""
                    oClsCardiologyDevice.ProcedureDate = mPA_ProcDate
                    oClsCardioDeviceCollection.Add(oClsCardiologyDevice)

                    ''\\ now Update CardioDevice Data
                End If
            Next

            If oClsCardioDeviceCollection.Count > 0 Then
                Dim objCardio As New ClsEjectionFractionDBLayer
                objCardio.SaveCardiology(oClsCardioDeviceCollection)
                If mPA_Flag = True Then
                    objCardio.SaveCardiologyAssociation(oClsCardioDeviceCollection)
                End If
            End If
            'for Inserting data
            


            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function SaveCardioDeviceData(ByVal oClsCardioCollection As ClsCardioDeviceCollection) As Boolean
        Dim cnn As New SqlConnection(GetConnectionString)
        cnn.Open()
        Dim cmd As SqlCommand
        'Dim cnt As Int32
        Dim _strSQL As String = ""

        Try
            For i As Int32 = 0 To oClsCardioCollection.Count - 1
                cmd = New SqlCommand
                cmd.Connection = cnn
                cmd.CommandType = CommandType.Text
                Dim objCardio As clsCardiologyDevice
                objCardio = oClsCardioCollection.Item(i)

                ' _strSQL = "Insert into hl7_clients(sSendingApplicationName, sSendingFacility,sSendingApplicationField) values ('" & sApplName & "','" & sApplFacility & "','" & sAppField & "')"
                _strSQL = "insert into  CV_CardiologyDevice( nCardiologyDeviceID, nPatientID, nExamID, nVisitID, nClinicID, dtDateofImplant, sDeviceType, sProductName, sDeviceManufacturer," & _
                          "sProductSpecification, sProductSerialNo, sManufacturerModelNo, sLeadType, dtDateRemoved, sPhysicalLocation) values('" & objCardio.CardiologyDeviceID & "','" & objCardio.PatientID & "','" & objCardio.ExamID & "','" & objCardio.VisitID & "','" & objCardio.ClinicID & "','" & objCardio.DateofImplant & "','" & objCardio.DeviceType & "','" & objCardio.ProductName & "','" & objCardio.DeviceManufacturer & "','" & objCardio.ProductSpecification & "','" & objCardio.ProductSerialNo & "','" & objCardio.ManufacturerModelNo & "','" & objCardio.LeadType & "','" & objCardio.DateRemoved & "','" & objCardio.PhysicalLocation & "')"
                cmd.CommandText = _strSQL
                If ((cmd.ExecuteNonQuery()) > 0) Then
                    ' Return True
                Else
                    Return False
                End If
                objCardio = Nothing
            Next
            Return True

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            cnn = Nothing
            If Not IsNothing(cmd) Then
                cmd.Dispose()
                cmd = Nothing
            End If

        End Try
    End Function

    Private Function UpdateCardioDeviceData(ByVal oClsCardioCollection As ClsCardioDeviceCollection) As Boolean

        Dim cnn As New SqlConnection(GetConnectionString)
        cnn.Open()

        Dim cmd As SqlCommand
        'Dim cnt As Int32
        Dim _strSQL As String = ""
        Try
            For i As Int32 = 0 To oClsCardioCollection.Count - 1
                cmd = New SqlCommand
                cmd.Connection = cnn
                cmd.CommandType = CommandType.Text
                ''values('" & oClsCardioCollection.Item(0).CardiologyDeviceID & "','" & oClsCardioCollection.Item(0).PatientID & "','" & oClsCardioCollection.Item(0).ExamID & "','" & oClsCardioCollection.Item(0).VisitID & "','" & oClsCardioCollection.Item(0).ClinicID & "','" & oClsCardioCollection.Item(0).DateofImplant & "','" & oClsCardioCollection.Item(0).DeviceType & "','" & oClsCardioCollection.Item(0).ProductName & "','" & oClsCardioCollection.Item(0).DeviceManufacturer & "','" & oClsCardioCollection.Item(0).ProductSpecification & "','" & oClsCardioCollection.Item(0).ProductSerialNo & "','" & oClsCardioCollection.Item(0).ManufacturerModelNo & "','" & oClsCardioCollection.Item(0).LeadType & "','" & oClsCardioCollection.Item(0).DateRemoved & "','" & oClsCardioCollection.Item(0).PhysicalLocation & "')"
                _strSQL = "update CV_CardiologyDevice set nExamID='" & oClsCardioCollection.Item(i).ExamID & "', nClinicID='" & oClsCardioCollection.Item(i).ClinicID & "', dtDateofImplant='" & oClsCardioCollection.Item(i).DateofImplant & "', sDeviceType='" & oClsCardioCollection.Item(i).DeviceType & "', sProductName='" & oClsCardioCollection.Item(i).ProductName & "', sDeviceManufacturer='" & oClsCardioCollection.Item(i).DeviceManufacturer & "',sProductSpecification='" & oClsCardioCollection.Item(i).ProductSpecification & "', sProductSerialNo='" & oClsCardioCollection.Item(i).ProductSerialNo & "', sManufacturerModelNo='" & oClsCardioCollection.Item(i).ManufacturerModelNo & "', sLeadType='" & oClsCardioCollection.Item(i).LeadType & "', dtDateRemoved='" & oClsCardioCollection.Item(i).DateRemoved & "', sPhysicalLocation='" & oClsCardioCollection.Item(i).PhysicalLocation & "' where nCardiologyDeviceID='" & oClsCardioCollection.Item(i).CardiologyDeviceID & "' and nPatientID='" & oClsCardioCollection.Item(i).PatientID & "' and nVisitID='" & oClsCardioCollection.Item(i).VisitID & "'"
                cmd.CommandText = _strSQL
                If ((cmd.ExecuteNonQuery()) > 0) Then
                    ' Return True
                Else
                    Return False
                End If
            Next

            Return True

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            cnn = Nothing
            If Not IsNothing(cmd) Then
                cmd.Dispose()
                cmd = Nothing
            End If

        End Try
    End Function

    Private Function SaveProcdureDeviceAssociation(ByVal oClsCardioCollection As ClsCardioDeviceCollection) As Boolean

        Dim cnn As New SqlConnection(GetConnectionString)
        cnn.Open()

        Dim cmd As SqlCommand
        Dim cnt As Int32
        Dim _strSQL As String = ""
        Try
            For i As Int32 = 0 To oClsCardioCollection.Count - 1
                cmd = New SqlCommand
                cmd.Connection = cnn
                cmd.CommandType = CommandType.Text
                Dim objCardio As clsCardiologyDevice
                objCardio = oClsCardioCollection.Item(i)
                _strSQL = "insert into CV_ProcedureDeviceAssociation( nProcDeviceAssociationID, nPatientID, nExamID, nVisitID, nClinicID, dtProcedureDate, sProcedures) values('" & objCardio.CardiologyDeviceID & "','" & objCardio.PatientID & "','" & objCardio.ExamID & "','" & objCardio.VisitID & "','" & objCardio.ClinicID & "','" & objCardio.DateofImplant & "','" & mPA_Procedure & "')"
                cmd.CommandText = _strSQL
                If ((cmd.ExecuteNonQuery()) > 0) Then
                    ' Return True
                Else
                    Return False
                End If
                objCardio = Nothing
            Next
            Return True

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            cnn = Nothing
            If Not IsNothing(cmd) Then
                cmd.Dispose()
                cmd = Nothing
            End If

        End Try
    End Function

    Private Function GenerateUnique_CardioDeviceID(ByVal PatientID As Long) As Long
        Dim MachineId As Int64 = 0
        Dim _OID As Int64 = 0
        Dim strquery As String = ""
        Dim cnn As New SqlConnection(GetConnectionString)
        cnn.Open()

        Dim cmd As SqlCommand
        Try
            MachineId = GetPrefixTransactionID(PatientID)
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.Text

            'cmd.CommandText = "SELECT ncontactid FROM contacts_mst WHERE convert(varchar(18),ncontactid) Like convert(varchar(18)," & MachineId & " )+ '%'"
            strquery = "SELECT nCardiologyDeviceID FROM CV_CardiologyDevice WHERE convert(varchar(18),nCardiologyDeviceID) Like convert(varchar(18)," & MachineId & " )+ '%'"
            cmd.CommandText = strquery
            'Get this ID
            _OID = cmd.ExecuteScalar()
            cmd.Dispose()
            cmd = Nothing '

            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.Text
            'cmd.CommandType = CommandType.Text
            If _OID = 0 Then
                strquery = "select convert(numeric(18,0), convert(varchar(18)," & MachineId & ") + '01')"
            Else
                strquery = "select isnull(max(nCardiologyDeviceID),0)+1 from CV_CardiologyDevice where convert(varchar(18),nCardiologyDeviceID) Like convert(varchar(18)," & MachineId & " )+ '%'"
            End If
            cmd.CommandText = strquery
            _OID = cmd.ExecuteScalar()
            cmd.Dispose()
            cmd = Nothing
            Return _OID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            cnn = Nothing
            If Not IsNothing(cmd) Then
                cmd.Dispose()
                cmd = Nothing
            End If

        End Try

    End Function

    Public Function GetPrefixTransactionID(ByVal PatientId As Long) As Long
        Dim strID As String
        Dim strPatientID As String
        Dim strPatientTempID As String
        'Randomize(strPatientID)
        strPatientID = CStr(PatientId)
        If strPatientID.Length >= 15 Then
            strPatientTempID = strPatientID.Substring(4, 1) & strPatientID.Substring(9, 1) & strPatientID.Substring(14, 1)
        Else
            Select Case strPatientID.Length
                Case 1
                    strPatientTempID = "00" & strPatientID
                Case 2
                    strPatientTempID = "0" & strPatientID
                Case Else
                    strPatientTempID = Microsoft.VisualBasic.Strings.Right(strPatientID, 3)
            End Select
        End If
        Dim dtDate As DateTime
        dtDate = System.DateTime.Now

        strID = DateDiff(DateInterval.Day, CDate("1/1/1900"), dtDate.Date)
        strID = strID & strPatientTempID.Substring(0, 1)
        strID = strID & DateDiff(DateInterval.Second, dtDate.Date, dtDate)
        strID = strID & strPatientTempID.Substring(1, 1)
        strID = strID & dtDate.Millisecond
        strID = strID & strPatientTempID.Substring(2, 1)
        Return CLng(strID)
    End Function

    Private Sub tlstrip_Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlstrip_Delete.Click
        Dim cnn As New SqlConnection(GetConnectionString)
        cnn.Open()

        Dim cmd As SqlCommand
        Dim cnt As Int32
        Dim _strSQL As String = ""

        Try
            
            Dim intSelectRow1 As Integer = C1Cardiology.Row
            If C1Cardiology.Rows.Count > 0 Then
                Dim ID As Long = CType(C1Cardiology.GetData(intSelectRow1, COL_CardiologyDeviceID), Long)
                If ID = 0 Then
                    C1Cardiology.Rows.Remove(intSelectRow1)
                    ''solving TFS id issue- 1859
                    MessageBox.Show("No Implant Device records available for deletion.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ''end
                Else
                    'Dhruv Shown the messagebox - > BugID 5800
                    If (MessageBox.Show("Are you sure you want to delete these record?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
                        If mPA_Flag = True Then
                            DeleteCVProcdureDeviceAssociation(ID)
                        End If

                        cmd = New SqlCommand
                        cmd.Connection = cnn
                        cmd.CommandType = CommandType.Text
                        _strSQL = "Delete from CV_CardiologyDevice where nCardiologyDeviceID=" & ID & ""
                        cmd.CommandText = _strSQL
                        If ((cmd.ExecuteNonQuery()) > 0) Then
                            C1Cardiology.Rows.Remove(intSelectRow1)
                        End If

                    End If 'Messagebox If
                End If 'ID If
            End If ' Cardiology IF

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            cnn = Nothing
            If Not IsNothing(cmd) Then
                cmd.Dispose()
                cmd = Nothing
            End If

        End Try
    End Sub

    Private Function DeleteCVProcdureDeviceAssociation(ByVal ProcID As Int64) As Boolean

        Dim cnn As New SqlConnection(GetConnectionString)
        cnn.Open()

        Dim cmd As SqlCommand
        Dim cnt As Int32
        Dim _strSQL As String = ""
        Try

            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.Text
            _strSQL = "Delete from CV_ProcedureDeviceAssociation where nProcDeviceAssociationID=" & ProcID & ""
            cmd.CommandText = _strSQL
            If ((cmd.ExecuteNonQuery()) > 0) Then
                Return True
            Else
                Return False
            End If
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Delete, "deleted records of Cardiology device ", gstrLoginName, gstrClientMachineName, m_patientID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.Delete, "deleted records of Cardiology device ", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.Delete, "deleted records of Cardiology device ", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            cnn = Nothing
            If Not IsNothing(cmd) Then
                cmd.Dispose()
                cmd = Nothing
            End If

        End Try
    End Function

    Private Sub tlstrip_Refresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlstrip_Refresh.Click
        Try
            DesignC1Grid()
            If mPA_Flag = True Then
                Dim dt1 As DataTable = PopulateDatafromProcAssociation()
                FillC1Grid(dt1)
            Else
                Dim dt As DataTable = PopulateCardioDeviceData()
                FillC1Grid(dt)
            End If
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Other, "Refreshed CardioLogy device data ", gstrLoginName, gstrClientMachineName, m_patientID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.Refresh, "Refreshed CardioLogy device data ", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.Refresh, "Refreshed CardioLogy device data ", m_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.Refresh, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ogloUC_generalsearch_AfterTextSearch(ByVal dv As System.Data.DataView) Handles ogloUC_generalsearch.AfterTextSearch
        Try
            If Not IsNothing(dv) Then
                FillC1Grid(dv.ToTable)
            Else
                FillC1Grid(dt)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub C1Cardiology_AfterAddRow(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1Cardiology.AfterAddRow
        With C1Cardiology
            .SetCellStyle(e.Row, COL_TypeofDevice, cStyleDeviceType)

            .SetCellStyle(e.Row, COL_ProductName, cStyleProductName)

            .SetCellStyle(e.Row, COL_DeviceManufacturer, cStyleDeviceManf)

            .SetCellStyle(e.Row, COL_LeadsType, cStyleLeadtype)

        End With
       
    End Sub

    Private Sub C1Cardiology_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1Cardiology.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        'Shubhangi 20091006
        'Use to clear search text box
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub
End Class