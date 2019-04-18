Imports gloUserControlLibrary
Imports System.Data.SqlClient
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports System.Text

Public Class frmCV_ImplantDevice


    Dim m_patientID As Int64 = 0
    Dim m_ExamID As Int64 = 0
    Dim m_VisitID As Int64 = 0
    Dim m_ClinicId As Int32 = 0

    Dim mPA_ProcDate As Date
    Dim mPA_Flag As Boolean = False
    Dim mPA_Procedure As String = ""
    Dim mPA_WhichFunction As String = ""


    Private WithEvents dgCustomGrid As CustomTask
    Private Col_Check As Integer = 2
    Private Col_Name As Integer = 0
    Private Col_Dosage As Integer = 1
    Private Col_Cnt As Integer = 3
    Dim _TempRx As String
    Dim _Temprow As Int32
    Dim strLst As String = ""



    Private Const COL_COUNT = 22

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
    Private Const COL_LeadLocation = 15
    Private Const COL_ThresholdAtrial = 16
    Private Const COL_ThresholdVentricular = 17
    Private Const COL_SensingAtrial = 18
    Private Const COL_SensingVentricular = 19
    Private Const COL_ImpedenceAtrial = 20
    Private Const COL_ImpedenceVentricular = 21

    Dim cStyleDeviceManf As C1.Win.C1FlexGrid.CellStyle
    '' add ctstyle for combo of c1
    Dim cStyleDeviceType As C1.Win.C1FlexGrid.CellStyle

    Dim cStyleProductName As C1.Win.C1FlexGrid.CellStyle
    Dim cStyleLeadtype As C1.Win.C1FlexGrid.CellStyle

    'Dim ds As New DataSet
    'Dim dt As DataTable

    Public blnIsNew As Boolean

    Private WithEvents _PatientStrip As gloUC_PatientStrip = Nothing
    'Private WithEvents ogloUC_generalsearch As gloUserControlLibrary.gloUCGeneralSearch
    Dim objCath As New Cls_CardioVasculars

    Private Sub frmCV_ImplantDevice_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            If IsNothing(_PatientStrip) = False Then
                pnlPatientDetails.Controls.Remove(_PatientStrip)
                _PatientStrip.Dispose()
                _PatientStrip = Nothing
            End If
            objCath.Dispose()
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.Close, "Close Implant Devices", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20100916
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.Close, "Close Implant Devices", 0, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            ''
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    '

    Private Sub frmCV_ImplantDevice_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        gloC1FlexStyle.Style(C1ImplantDevice)

        Try

            If IsNothing(_PatientStrip) = False Then
                pnlPatientDetails.Controls.Remove(_PatientStrip)
                _PatientStrip.Dispose()
                _PatientStrip = Nothing
            End If
            _PatientStrip = New gloUC_PatientStrip
            _PatientStrip.ShowDetail(m_patientID, gloUC_PatientStrip.enumFormName.PatientEducation)
            'If mPA_Flag = False Then
            '    _PatientStrip.ShowDetail(m_patientID, gloUC_PatientStrip.enumFormName.PatientEducation)
            'Else
            '    _PatientStrip.ShowDetail(m_patientID, gloUC_PatientStrip.enumFormName.PatientEducation)
            'End If

            _PatientStrip.Dock = DockStyle.Top
            _PatientStrip.BringToFront()
            _PatientStrip.Padding = New Padding(3, 0, 3, 0)
            pnlPatientDetails.Controls.Add(_PatientStrip)

            DesignC1Grid()

            ''Dim dt As DataTable = PopulateCardioDeviceData()
            ''FillC1Grid(dt)
            If blnIsNew = True Then
                DtProcDate.Enabled = True
            Else                
                DtProcDate.Value = mPA_ProcDate
                DtProcDate.Enabled = False
            End If

            '' FillC1Grid()
            If mPA_Flag = True Then
                Dim dt1 As DataTable = PopulateDatafromProcAssociation()
                FillC1Grid(dt1)
                '''''''''''''' view device from Electrophysiology
                If mPA_WhichFunction = "VIEW" Then
                    tlstrip_Save.Visible = False
                    C1ImplantDevice.AllowEditing = False
                    tlstrip_DeleteRow.Visible = False
                End If
                '''''''''''''' view device from Electrophysiology

                ''''''''''''''
                'btnBrowseCPT.Enabled = False
                'btnClearCPT.Enabled = False
                'BtnClearAllCPT.Enabled = False
                ''''''''''''''
                btnBrowseProc.Enabled = False
                btnClearProc.Enabled = False
                BtnClearAllProc.Enabled = False
                ''''''''''''''
                If Not IsNothing(dt1) Then
                    dt1.Dispose()
                    dt1 = Nothing
                End If

                ''ogloUC_generalsearch.IntialiseDatatable(dt1)
            Else
                Dim dt As DataTable = PopulateCardioDeviceData()
                '''''''''' for proc-device association
                If Not IsNothing(dt) Then
                    If Not IsDBNull(dt) Then
                        If dt.Rows.Count > 0 Then
                            If Convert.ToBoolean(dt.Rows(0)("bProcDeviceAssociation")) = True Then
                                tlstrip_Save.Enabled = False
                                tlstrip_Save.ToolTipText = "This Device(s) is associated with Procedure, go to Electrophysiology to modify the device(s)."
                                tlstrip_DeleteRow.Enabled = False
                                tlstrip_DeleteRow.ToolTipText = "This Device(s) is associated with Procedure, go to Electrophysiology to delete the device(s)."
                            End If
                        End If
                    End If
                End If
                '''''''''' for proc-device association
                FillC1Grid(dt)
                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If


                ''  ogloUC_generalsearch.IntialiseDatatable(dt)
                End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Try
            gloPatient.gloPatient.GetWindowTitle(Me, m_patientID, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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
            Dim strquery As String = "SELECT ISNULL(CV_CardiologyDevice.nCardiologyDeviceID, 0) AS CardiologyDeviceID, ISNULL(CV_CardiologyDevice.nPatientID, 0) AS PatientID, ISNULL(CV_CardiologyDevice.nExamID, 0) AS ExamID, ISNULL(CV_CardiologyDevice.nVisitID, 0) AS VisitID, ISNULL(CV_CardiologyDevice.nClinicID, 0) AS ClinicID, ISNULL(CV_CardiologyDevice.dtDateofImplant, 0) AS DateofImplant, ISNULL(CV_CardiologyDevice.sDeviceType, '') AS DeviceType, ISNULL(CV_CardiologyDevice.sProductName, '') AS ProductName, ISNULL(CV_CardiologyDevice.sDeviceManufacturer, '') AS DeviceManufacturer, ISNULL(CV_CardiologyDevice.sProductSpecification, '') AS ProductSpecification, ISNULL(CV_CardiologyDevice.sProductSerialNo, '') AS ProductSerialNo, ISNULL(CV_CardiologyDevice.sManufacturerModelNo, '') AS sManufacturerModelNo, ISNULL(CV_CardiologyDevice.sLeadType, '') AS LeadType, CV_CardiologyDevice.dtDateRemoved  AS DateRemoved, ISNULL(CV_CardiologyDevice.sPhysicalLocation, '') AS PhysicalLocation " & _
            " , isnull(sLeadLocation,'') as LeadLocation, isnull(sThresholdAtrial,'') as ThresholdAtrial, isnull(sThresholdVentricular,'') as ThresholdVentricular  , isnull(sSensingAtrial,'') as SensingAtrial, isnull(sSensingVentricular,'') as SensingVentricular , isnull(sImpedenceAtrial,'') as ImpedenceAtrial, isnull(sImpedenceVentricular,'') as ImpedenceVentricular FROM CV_CardiologyDevice INNER JOIN CV_ProcedureDeviceAssociation ON CV_CardiologyDevice.nCardiologyDeviceID = CV_ProcedureDeviceAssociation.nProcDeviceAssociationID where CV_ProcedureDeviceAssociation.nPatientID = " & m_patientID & " and CV_ProcedureDeviceAssociation.nExamID=" & m_ExamID & " And CV_ProcedureDeviceAssociation.nVisitID = " & m_VisitID & " and CV_ProcedureDeviceAssociation.nClinicID = " & m_ClinicId & " and CV_ProcedureDeviceAssociation.dtProceduredate='" & mPA_ProcDate & "' and CV_ProcedureDeviceAssociation.sProcedures='" & mPA_Procedure & "'"
            '"select isnull(nCardiologyDeviceID,0)as CardiologyDeviceID,isnull(nPatientID,0) as PatientID, isnull(nExamID,0) as ExamID, isnull(nVisitID,0)as VisitID , isnull(nClinicID,0) as ClinicID, isnull(dtDateofImplant,0)as DateofImplant, isnull(sDeviceType,'') as DeviceType, isnull(sProductName,'')as ProductName, isnull(sDeviceManufacturer,'') as DeviceManufacturer,isnull(sProductSpecification,'') as ProductSpecification, isnull(sProductSerialNo,'') as ProductSerialNo ,isnull(sManufacturerModelNo,'') as sManufacturerModelNo, isnull(sLeadType,'') as LeadType, isnull(dtDateRemoved,'') as DateRemoved, isnull(sPhysicalLocation,'') as PhysicalLocation from dbo.CV_CardiologyDevice where nPatientID = " & m_PatientID & " and nExamID=" & m_ExamID & " And nVisitID = " & m_VisitID & " and nClinicID = " & m_ClinicID & " and dtDateofImplant='" & mPA_ProcDate & "'"
            cmd = New SqlCommand(strquery, conn)
            sqladpt = New SqlDataAdapter(cmd)

            'Fill data adapter
            sqladpt.Fill(dt)
            sqladpt.Dispose()
            sqladpt = Nothing
            conn.Close()
            conn.Dispose()
            conn = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            'Return Data table
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
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
            '''''''Dim strquery As String = "select isnull(nCardiologyDeviceID,0)as CardiologyDeviceID,isnull(nPatientID,0) as PatientID, isnull(nExamID,0) as ExamID, isnull(nVisitID,0)as VisitID , isnull(nClinicID,0) as ClinicID, isnull(dtDateofImplant,0)as DateofImplant, isnull(sDeviceType,'') as DeviceType, isnull(sProductName,'')as ProductName, isnull(sDeviceManufacturer,'') as DeviceManufacturer,isnull(sProductSpecification,'') as ProductSpecification, isnull(sProductSerialNo,'') as ProductSerialNo ,isnull(sManufacturerModelNo,'') as sManufacturerModelNo, isnull(sLeadType,'') as LeadType,dtDateRemoved as DateRemoved, isnull(sPhysicalLocation,'') as PhysicalLocation from dbo.CV_CardiologyDevice where nPatientID = " & m_patientID & " And nVisitID = " & m_VisitID & ""
            Dim strquery As String = "select isnull(nCardiologyDeviceID,0)as CardiologyDeviceID,isnull(nPatientID,0) as PatientID, isnull(nExamID,0) as ExamID, isnull(nVisitID,0)as VisitID , isnull(nClinicID,0) as ClinicID, isnull(dtDateofImplant,0)as DateofImplant, isnull(sDeviceType,'') as DeviceType, isnull(sProductName,'')as ProductName, isnull(sDeviceManufacturer,'') as DeviceManufacturer,isnull(sProductSpecification,'') as ProductSpecification, isnull(sProductSerialNo,'') as ProductSerialNo ,isnull(sManufacturerModelNo,'') as sManufacturerModelNo, isnull(sLeadType,'') as LeadType,dtDateRemoved as DateRemoved, isnull(sPhysicalLocation,'') as PhysicalLocation, isnull(sLeadLocation,'') as LeadLocation, isnull(sThresholdAtrial,'') as ThresholdAtrial, isnull(sThresholdVentricular,'') as ThresholdVentricular " & _
            " , isnull(sSensingAtrial,'') as SensingAtrial, isnull(sSensingVentricular,'') as SensingVentricular , isnull(sImpedenceAtrial,'') as ImpedenceAtrial, isnull(sImpedenceVentricular,'') as ImpedenceVentricular,isnull(bProcDeviceAssociation,0) as bProcDeviceAssociation from dbo.CV_CardiologyDevice where nPatientID = " & m_patientID & " And nVisitID = " & m_VisitID & " and dtdateofimplant='" & mPA_ProcDate & "' "
            cmd = New SqlCommand(strquery, conn)
            sqladpt = New SqlDataAdapter(cmd)

            'Fill data adapter
            sqladpt.Fill(dt)
            sqladpt.Dispose()
            sqladpt = Nothing
            conn.Close()
            conn.Dispose()
            conn = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            'Return Data table
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try

    End Function

    Private Function GetProductData() As DataTable
        Dim Connection As New SqlConnection(GetConnectionString)
        Try
            Connection.Open()
            Dim CommandString As String = "select sDescription from category_mst where sCategoryType='Cardio Product Name'"

            Dim adp As New SqlDataAdapter(CommandString, Connection)
            Dim ds As New DataSet
            adp.Fill(ds)
            Dim dt As DataTable = ds.Tables(0).Copy()
            adp.Dispose()
            adp = Nothing
            Connection.Close()
            Connection.Dispose()
            Connection = Nothing

            ds.Dispose()
            ds = Nothing
            Return dt


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Private Function GetDeviceTypeData() As DataTable
        Dim Connection As New SqlConnection(GetConnectionString)
        Try
            Connection.Open()
            Dim CommandString As String = "select sDescription from category_mst where sCategoryType='Cardio Device Type'"

            Dim adp As New SqlDataAdapter(CommandString, Connection)
            Dim ds As New DataSet
            adp.Fill(ds)
            Dim dt As DataTable = ds.Tables(0).Copy()
            adp.Dispose()
            adp = Nothing
            Connection.Close()
            Connection.Dispose()
            Connection = Nothing

            ds.Dispose()
            ds = Nothing
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Private Function GetDevicemanfData() As DataTable
        Dim Connection As New SqlConnection(GetConnectionString)
        Try
            Connection.Open()
            Dim CommandString As String = "select sDescription from category_mst where sCategoryType='Cardio Device Manf.'"

            Dim adp As New SqlDataAdapter(CommandString, Connection)
            Dim ds As New DataSet
            adp.Fill(ds)
            Dim dt As DataTable = ds.Tables(0).Copy()
            adp.Dispose()
            adp = Nothing
            Connection.Close()
            Connection.Dispose()
            Connection = Nothing

            ds.Dispose()
            ds = Nothing
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If (IsNothing(Connection) = False) Then
                If Connection.State = ConnectionState.Open Then
                    Connection.Close()
                End If
                Connection.Dispose()
                Connection = Nothing
            End If
        End Try
    End Function

    Private Function GetLeadData() As DataTable
        Dim Connection As New SqlConnection(GetConnectionString)
        Try
            Connection.Open()
            Dim CommandString As String = "select sDescription from category_mst where sCategoryType='Cardio Lead Type'"

            Dim adp As New SqlDataAdapter(CommandString, Connection)
            Dim ds As New DataSet
            adp.Fill(ds)
            Dim dt As DataTable = ds.Tables(0).Copy()
            adp.Dispose()
            adp = Nothing
            Connection.Close()
            Connection.Dispose()
            Connection = Nothing

            ds.Dispose()
            ds = Nothing
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If (IsNothing(Connection) = False) Then
                If Connection.State = ConnectionState.Open Then
                    Connection.Close()
                End If
                Connection.Dispose()
                Connection = Nothing
            End If
        End Try
    End Function

    Private Sub DesignC1Grid()
        Try

            With C1ImplantDevice
                '.Clear()
                C1ImplantDevice.DataSource = Nothing
                .Clear()

                ' 'Setfont
                gloC1FlexStyle.Style(C1ImplantDevice)
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
                .Cols(COL_DateofImplant).Visible = False
                .Cols(COL_TypeofDevice).Visible = True
                .Cols(COL_ProductName).Visible = True
                .Cols(COL_DeviceManufacturer).Visible = True
                .Cols(COL_ProductSpecifications).Visible = True
                .Cols(COL_ProductSerialNumber).Visible = True
                .Cols(COL_ManufacturerModelNumber).Visible = True
                .Cols(COL_LeadsType).Visible = True
                .Cols(COL_DateRemoved).Visible = True

                .Cols(COL_PatientID).Visible = False
                .Cols(COL_ExamID).Visible = False
                .Cols(COL_VisitID).Visible = False
                .Cols(COL_CardiologyDeviceID).Visible = False
                .Cols(COL_ClinicID).Visible = False

                '''''''''''''' By Ujwala as on 11192010
                .Cols(COL_PhysicalLocationofDeviceImplant).Visible = False
                .Cols(COL_LeadLocation).Visible = False
                .Cols(COL_ThresholdAtrial).Visible = False
                .Cols(COL_ThresholdVentricular).Visible = False
                .Cols(COL_SensingAtrial).Visible = False
                .Cols(COL_SensingVentricular).Visible = False
                .Cols(COL_ImpedenceAtrial).Visible = False
                .Cols(COL_ImpedenceVentricular).Visible = False
                '''''''''''''' By Ujwala as on 11192010

                'set col allow property
                .Cols(COL_DateofImplant).AllowEditing = False
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
                .Cols(COL_LeadLocation).AllowEditing = True
                .Cols(COL_ThresholdAtrial).AllowEditing = True
                .Cols(COL_ThresholdVentricular).AllowEditing = True
                .Cols(COL_SensingAtrial).AllowEditing = True
                .Cols(COL_SensingVentricular).AllowEditing = True
                .Cols(COL_ImpedenceAtrial).AllowEditing = True
                .Cols(COL_ImpedenceVentricular).AllowEditing = True

                'set col width            
                Dim cWidth As Int32 = pnlPatientDetails.Width

                '.Cols(COL_HIDDENAPPLICATIONNAME).Width = Convert.ToInt32(cWidth * 0.2)
                .Cols(COL_DateofImplant).Width = 0
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
                .Cols(COL_LeadLocation).Width = Convert.ToInt32(cWidth * 0.2)
                .Cols(COL_ThresholdAtrial).Width = Convert.ToInt32(cWidth * 0.2)
                .Cols(COL_ThresholdVentricular).Width = Convert.ToInt32(cWidth * 0.2)
                .Cols(COL_SensingAtrial).Width = Convert.ToInt32(cWidth * 0.2)
                .Cols(COL_SensingVentricular).Width = Convert.ToInt32(cWidth * 0.2)
                .Cols(COL_ImpedenceAtrial).Width = Convert.ToInt32(cWidth * 0.2)
                .Cols(COL_ImpedenceVentricular).Width = Convert.ToInt32(cWidth * 0.2)

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

                .Cols(COL_LeadLocation).DataType = GetType(String)
                .Cols(COL_ThresholdAtrial).DataType = GetType(String)
                .Cols(COL_ThresholdVentricular).DataType = GetType(String)
                .Cols(COL_SensingAtrial).DataType = GetType(String)
                .Cols(COL_SensingVentricular).DataType = GetType(String)
                .Cols(COL_ImpedenceAtrial).DataType = GetType(String)
                .Cols(COL_ImpedenceVentricular).DataType = GetType(String)

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

                .SetData(0, COL_LeadLocation, "Leads Location")
                .SetData(0, COL_ThresholdAtrial, "Threshold Atrial")
                .SetData(0, COL_ThresholdVentricular, "Threshold Ventricular ")
                .SetData(0, COL_SensingAtrial, "Sensing Atrial")
                .SetData(0, COL_SensingVentricular, "Sensing Ventricular")
                .SetData(0, COL_ImpedenceAtrial, "Impedence Atrial")
                .SetData(0, COL_ImpedenceVentricular, "Impedence Ventricular")

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

            Dim x As Int16
            Dim i As Int16


            If Not IsNothing(datatable) Then
                If Not IsDBNull(datatable) Then
                    If datatable.Rows.count > 0 Then
                        '''''''disable date
                        DtProcDate.Value = mPA_ProcDate
                        DtProcDate.Enabled = False
                        '''''''''''''''''
                        '''''''''''clear all
                        lstCPTcode.Items.Clear()
                        lstProcedures.Items.Clear()
                        Dim oGetLst As DataTable = Nothing
                        '''''''''''cpt
                        If mPA_Flag = True Then
                            oGetLst = GetList("select distinct sCPTcode from CV_CardiologyDevice where npatientid=" & datatable.Rows(0)("PatientID") & " and dtdateofimplant='" & datatable.Rows(0)("DateofImplant") & "' and sCPTcode<>'' and sProcedures='" & mPA_Procedure & "'")
                            If Not IsNothing(oGetLst) Then
                                For x = 0 To oGetLst.Rows.Count - 1
                                    lstCPTcode.Items.Add(oGetLst.Rows(x)(0))
                                Next
                                oGetLst.Dispose()
                                oGetLst = Nothing
                            End If
                        Else
                            oGetLst = GetList("select distinct sCPTcode from CV_CardiologyDevice where npatientid=" & datatable.Rows(0)("PatientID") & " and dtdateofimplant='" & datatable.Rows(0)("DateofImplant") & "' and sCPTcode<>''")
                            If Not IsNothing(oGetLst) Then
                                For x = 0 To oGetLst.Rows.Count - 1
                                    lstCPTcode.Items.Add(oGetLst.Rows(x)(0))
                                Next
                                oGetLst.Dispose()
                                oGetLst = Nothing
                            End If
                        End If
                        '''''''''''cpt

                        '''''''''''proc
                        If mPA_Flag = True Then
                            lstProcedures.Items.Add(mPA_Procedure)
                        Else
                            oGetLst = GetList("select distinct sProcedures from CV_CardiologyDevice where npatientid=" & datatable.Rows(0)("PatientID") & " and dtdateofimplant='" & datatable.Rows(0)("DateofImplant") & "' and sProcedures<>''")
                            If Not IsNothing(oGetLst) Then
                                For x = 0 To oGetLst.Rows.Count - 1
                                    lstProcedures.Items.Add(oGetLst.Rows(x)(0))
                                Next
                                oGetLst.Dispose()
                                oGetLst = Nothing
                            End If
                        End If
                        '''''''''''proc

                        oGetLst = Nothing
                    End If
                    End If
                End If
                C1ImplantDevice.Dock = DockStyle.Fill
                Dim _TotalWidth As Single = 0
                _TotalWidth = (C1ImplantDevice.Width - 20) / 11
                'Dim cStyle As C1.Win.C1FlexGrid.CellStyle

                C1ImplantDevice.Cols.Count = COL_COUNT
                C1ImplantDevice.Rows.Count = 1
                C1ImplantDevice.AllowEditing = True
                C1ImplantDevice.AllowAddNew = True

                C1ImplantDevice.Styles.ClearUnused()

            Dim dtDevicetype As DataTable = GetDeviceTypeData()

                Dim strComboString As String = " "
                For icnt As Int32 = 0 To dtDevicetype.Rows.Count - 1
                    strComboString = strComboString & "|" & dtDevicetype.Rows(icnt)(0).ToString
                Next

                'Dim rgOperator As C1.Win.C1FlexGrid.CellRange = C1Cardiology.GetCellRange(r.Index, COL_TypeofDevice, r.Index, COL_TypeofDevice)
            ' cStyleDeviceType = C1ImplantDevice.Styles.Add("Devicetype")
            Try
                If (C1ImplantDevice.Styles.Contains("Devicetype")) Then
                    cStyleDeviceType = C1ImplantDevice.Styles("Devicetype")
                Else
                    cStyleDeviceType = C1ImplantDevice.Styles.Add("Devicetype")
                 
                End If
            Catch ex As Exception
                cStyleDeviceType = C1ImplantDevice.Styles.Add("Devicetype")
              
            End Try
                cStyleDeviceType.ComboList = strComboString
                strComboString = ""
                'rgOperator.Style = cStyleDeviceType
            dtDevicetype.Dispose()
            dtDevicetype = Nothing

            Dim dtProductname As DataTable =GetProductData()
            strComboString = " "
                For i = 0 To dtProductname.Rows.Count - 1
                    strComboString = strComboString & "|" & dtProductname.Rows(i)(0).ToString
                Next

                'Dim rgOperator1 As C1.Win.C1FlexGrid.CellRange = C1Cardiology.GetCellRange(r.Index, COL_ProductName, r.Index, COL_ProductName)
            ' cStyleProductName = C1ImplantDevice.Styles.Add("ProductionName")
            Try
                If (C1ImplantDevice.Styles.Contains("ProductionName")) Then
                    cStyleProductName = C1ImplantDevice.Styles("ProductionName")
                Else
                    cStyleProductName = C1ImplantDevice.Styles.Add("ProductionName")

                End If
            Catch ex As Exception
                cStyleProductName = C1ImplantDevice.Styles.Add("ProductionName")

            End Try
                cStyleProductName.ComboList = strComboString
                strComboString = ""
                'rgOperator1.Style = cStyleProductName
            dtProductname.Dispose()
            dtProductname = Nothing

            Dim dtDeviceManf As DataTable = GetDevicemanfData()
            strComboString = " "
                For i = 0 To dtDeviceManf.Rows.Count - 1
                    strComboString = strComboString & "|" & dtDeviceManf.Rows(i)(0).ToString
                Next

                'Dim rgOperator2 As C1.Win.C1FlexGrid.CellRange = C1Cardiology.GetCellRange(r.Index, COL_DeviceManufacturer, r.Index, COL_DeviceManufacturer)
            'cStyleDeviceManf = C1ImplantDevice.Styles.Add("DeviceManf")
            Try
                If (C1ImplantDevice.Styles.Contains("DeviceManf")) Then
                    cStyleDeviceManf = C1ImplantDevice.Styles("DeviceManf")
                Else
                    cStyleDeviceManf = C1ImplantDevice.Styles.Add("DeviceManf")

                End If
            Catch ex As Exception
                cStyleDeviceManf = C1ImplantDevice.Styles.Add("DeviceManf")

            End Try
                cStyleDeviceManf.ComboList = strComboString
                strComboString = ""
                'rgOperator2.Style = cStyleDeviceManf
            dtDeviceManf.Dispose()
            dtDeviceManf = Nothing

            Dim dtLeadtype As DataTable = GetLeadData()
            strComboString = " "
                For i = 0 To dtLeadtype.Rows.Count - 1
                    strComboString = strComboString & "|" & dtLeadtype.Rows(i)(0).ToString
                Next

                'Dim rgOperator3 As C1.Win.C1FlexGrid.CellRange = C1Cardiology.GetCellRange(r.Index, COL_LeadsType, r.Index, COL_LeadsType)
            'cStyleLeadtype = C1ImplantDevice.Styles.Add("Leadtype")
            Try
                If (C1ImplantDevice.Styles.Contains("Leadtype")) Then
                    cStyleLeadtype = C1ImplantDevice.Styles("Leadtype")
                Else
                    cStyleLeadtype = C1ImplantDevice.Styles.Add("Leadtype")

                End If
            Catch ex As Exception
                cStyleLeadtype = C1ImplantDevice.Styles.Add("Leadtype")

            End Try
                cStyleLeadtype.ComboList = strComboString
                strComboString = ""
            dtLeadtype.Dispose()
            dtLeadtype = Nothing

            ' rgOperator3.Style = cStyleLeadtype
                C1ImplantDevice.SetCellStyle(1, COL_TypeofDevice, cStyleDeviceType)

                C1ImplantDevice.SetCellStyle(1, COL_ProductName, cStyleProductName)

                C1ImplantDevice.SetCellStyle(1, COL_DeviceManufacturer, cStyleDeviceManf)

                C1ImplantDevice.SetCellStyle(1, COL_LeadsType, cStyleLeadtype)
                If IsNothing(datatable) Then
                    C1ImplantDevice.SetData(1, COL_PatientID, datatable.Rows(i)("PatientID"))
                    C1ImplantDevice.SetData(1, COL_ExamID, datatable.Rows(i)("ExamID"))
                    C1ImplantDevice.SetData(1, COL_VisitID, datatable.Rows(i)("VisitID"))
                    C1ImplantDevice.SetData(1, COL_ClinicID, datatable.Rows(i)("ClinicID"))
                End If
                ''dt fill
                If Not IsNothing(datatable) Then
                    If Not IsDBNull(datatable) Then
                        If datatable.Rows.count > 0 Then

                            '''''''disable date
                            DtProcDate.Enabled = False
                            '''''''''''''''''

                            With C1ImplantDevice
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


                                    .SetData(i + 1, COL_PatientID, datatable.Rows(i)("PatientID"))
                                    .SetData(i + 1, COL_ExamID, datatable.Rows(i)("ExamID"))
                                    .SetData(i + 1, COL_VisitID, datatable.Rows(i)("VisitID"))
                                    .SetData(i + 1, COL_ClinicID, datatable.Rows(i)("ClinicID"))
                                    .SetData(i + 1, COL_CardiologyDeviceID, datatable.Rows(i)("CardiologyDeviceID"))
                                    .SetData(i + 1, COL_TypeofDevice, datatable.Rows(i)("DeviceType"))

                                    .SetData(i + 1, COL_ProductName, datatable.Rows(i)("ProductName"))

                                    .SetData(i + 1, COL_DeviceManufacturer, datatable.Rows(i)("DeviceManufacturer"))

                                    .SetData(i + 1, COL_LeadsType, datatable.Rows(i)("LeadType"))

                                ''''''''''''''''''''''''''''by Ujwala as on 19112010

                                txtPhLoc.Text = datatable.Rows(0)("PhysicalLocation")
                                txtLeadLoc.Text = datatable.Rows(0)("LeadLocation")
                                txtThAtr.Text = datatable.Rows(0)("ThresholdAtrial")
                                txtThVen.Text = datatable.Rows(0)("ThresholdVentricular")
                                txtSenAtr.Text = datatable.Rows(0)("SensingAtrial")
                                txtSenVen.Text = datatable.Rows(0)("SensingVentricular")
                                txtImpAtr.Text = datatable.Rows(0)("ImpedenceAtrial")
                                txtImpVen.Text = datatable.Rows(0)("ImpedenceVentricular")
                                
                                '''''''''''''''''''''
                                ''.SetData(i + 1, COL_PhysicalLocationofDeviceImplant, datatable.Rows(i)("PhysicalLocation"))
                                ''    .SetData(i + 1, COL_LeadLocation, datatable.Rows(i)("LeadLocation"))
                                ''    .SetData(i + 1, COL_ThresholdAtrial, datatable.Rows(i)("ThresholdAtrial"))
                                ''    .SetData(i + 1, COL_ThresholdVentricular, datatable.Rows(i)("ThresholdVentricular"))
                                ''    .SetData(i + 1, COL_SensingAtrial, datatable.Rows(i)("SensingAtrial"))
                                ''    .SetData(i + 1, COL_SensingVentricular, datatable.Rows(i)("SensingVentricular"))
                                ''    .SetData(i + 1, COL_ImpedenceAtrial, datatable.Rows(i)("ImpedenceAtrial"))
                                ''    .SetData(i + 1, COL_ImpedenceVentricular, datatable.Rows(i)("ImpedenceVentricular"))                                    
                                ''''''''''''''''''''''''''''by Ujwala as on 19112010

                            Next
                        End With


                        Dim r As Integer = 0
                        r = 0
                        Do Until (r >= C1ImplantDevice.Rows.Count - 1)
                            If C1ImplantDevice.Rows(r)(COL_TypeofDevice).ToString.Trim() = "" Then
                                C1ImplantDevice.Rows.Remove(r)
                            Else
                                If r < C1ImplantDevice.Rows.Count Then
                                    r = r + 1
                                End If
                            End If
                        Loop

                        '''''''''''''''''''''''''''''''''
                        '''''''''''''''''''''''''''''''''
                        'For Each r As C1.Win.C1FlexGrid.Row In C1ImplantDevice.Rows
                        '    If r.Item(COL_TypeofDevice).ToString.Trim() = "" Then
                        '        C1ImplantDevice.Rows.Remove(r.Index)
                        '    End If
                        'Next
                    End If
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub New(ByVal PatientID As Int64, ByVal VisitID As Int64, ByVal ProcDate As Date)

        InitializeComponent()

        m_patientID = PatientID
        mPA_ProcDate = ProcDate
        m_VisitID = VisitID

    End Sub

    Public Sub New(ByVal PatientID As Int64, ByVal examid As Int64, ByVal VisitID As Int64)

        InitializeComponent()

        m_patientID = PatientID
        m_ExamID = examid
        m_VisitID = VisitID

    End Sub

    Public Sub New(ByVal PatientID As Int64, ByVal examid As Int64, ByVal visitid As Int64, ByVal ProcDate As Date, ByVal Clinicid As Int64, ByVal sProcedure As String, ByVal ForFunct As String)
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
        ''Added Rahul P on 20100916
        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.Close, "Closed CardioLogy device", 0, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
        ''
        'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Other, "Closed CardioLogy device ", gstrLoginName, gstrClientMachineName, m_patientID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
        Me.Close()

    End Sub

    ''save

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
        Dim nCnt As Integer = 0

        Dim oClsCardioDeviceCollection As ClsCardioDeviceCollection
        ' Dim oClsCardioDeviceCollection_Update As ClsCardioDeviceCollection
        Dim oClsCardiologyDevice As clsCardiologyDevice = Nothing

        Try
            C1ImplantDevice.Row = 0
            C1ImplantDevice.RowSel = 0

            oClsCardioDeviceCollection = New ClsCardioDeviceCollection()
            ' oClsCardioDeviceCollection_Update = New ClsCardioDeviceCollection()
            ''''''''''''''
            ''   If mPA_Flag = False Then
            nCnt = If(lstCPTcode.Items.Count > lstProcedures.Items.Count, (lstCPTcode.Items.Count), (lstProcedures.Items.Count))

            For j As Int32 = 0 To nCnt - 1
                oClsCardiologyDevice = New clsCardiologyDevice()
                oClsCardiologyDevice.PatientID = m_patientID
                oClsCardiologyDevice.ExamID = m_ExamID
                oClsCardiologyDevice.VisitID = m_VisitID
                oClsCardiologyDevice.ClinicID = gnClinicID
                oClsCardiologyDevice.DateofImplant = DtProcDate.Value
                
                If lstCPTcode.Items.Count > j Then
                    oClsCardiologyDevice.sCPT = lstCPTcode.Items(j).ToString.Trim()
                Else
                    oClsCardiologyDevice.sCPT = ""
                End If

                If lstProcedures.Items.Count > j Then
                    oClsCardiologyDevice.sProc = lstProcedures.Items(j).ToString.Trim()
                Else
                    oClsCardiologyDevice.sProc = ""
                End If

                ''''''' for proc-device association                
                If mPA_Flag = True Then
                    oClsCardiologyDevice.ProcedureDate = mPA_ProcDate
                    oClsCardiologyDevice.sProc = mPA_Procedure & ""
                End If
                ''''''' for proc-device association

                ''''''''''''''''''''''
                oClsCardiologyDevice.PhysicalLocation = txtPhLoc.Text
                oClsCardiologyDevice.sLeadLocation = txtLeadLoc.Text
                oClsCardiologyDevice.sThresholdAtrial = txtThAtr.Text
                oClsCardiologyDevice.sThresholdVentricular = txtThVen.Text
                oClsCardiologyDevice.sSensingAtrial = txtSenAtr.Text
                oClsCardiologyDevice.sSensingVentricular = txtSenVen.Text
                oClsCardiologyDevice.sImpedenceAtrial = txtImpAtr.Text
                oClsCardiologyDevice.sImpedenceVentricular = txtImpVen.Text
                '''''''''''''' by Ujwala as on 19112010

                oClsCardioDeviceCollection.Add(oClsCardiologyDevice)
            Next
            ''   End If
            ''''''''''''''

            For i As Int32 = 1 To C1ImplantDevice.Rows.Count - 1
                If CType(C1ImplantDevice.GetData(i, COL_TypeofDevice), String) <> "" Then

                    oClsCardiologyDevice = New clsCardiologyDevice()
                    oClsCardiologyDevice.CardiologyDeviceID = GenerateUnique_CardioDeviceID(m_patientID)


                    C1ImplantDevice.SetData(i, COL_CardiologyDeviceID, oClsCardiologyDevice.CardiologyDeviceID)

                    ''''''''''''''
                    If mPA_Flag = True Then
                        oClsCardiologyDevice.PatientID = m_patientID
                        oClsCardiologyDevice.ExamID = m_ExamID
                        oClsCardiologyDevice.VisitID = m_VisitID
                        oClsCardiologyDevice.ClinicID = m_ClinicId
                        oClsCardiologyDevice.DateofImplant = mPA_ProcDate
                    Else
                        oClsCardiologyDevice.PatientID = m_patientID
                        oClsCardiologyDevice.ExamID = m_ExamID
                        oClsCardiologyDevice.VisitID = m_VisitID
                    End If
                    ''''''''''''''

                    oClsCardiologyDevice.PatientID = m_patientID
                    oClsCardiologyDevice.ExamID = m_ExamID
                    oClsCardiologyDevice.VisitID = m_VisitID
                    oClsCardiologyDevice.ClinicID = m_ClinicId

                    C1ImplantDevice.SetData(i, COL_PatientID, oClsCardiologyDevice.PatientID)

                    C1ImplantDevice.SetData(i, COL_ExamID, oClsCardiologyDevice.ExamID)

                    C1ImplantDevice.SetData(i, COL_VisitID, oClsCardiologyDevice.VisitID)

                    oClsCardiologyDevice.ClinicID = gnClinicID

                    oClsCardiologyDevice.DateofImplant = DtProcDate.Value
                    ''C1ImplantDevice.GetData(i, COL_DateofImplant)
                    oClsCardiologyDevice.DeviceType = C1ImplantDevice(i, COL_TypeofDevice) & ""
                    oClsCardiologyDevice.ProductName = C1ImplantDevice(i, COL_ProductName) & ""
                    oClsCardiologyDevice.DeviceManufacturer = C1ImplantDevice(i, COL_DeviceManufacturer) & ""
                    oClsCardiologyDevice.ProductSpecification = C1ImplantDevice(i, COL_ProductSpecifications) & ""
                    oClsCardiologyDevice.ProductSerialNo = C1ImplantDevice(i, COL_ProductSerialNumber) & ""
                    oClsCardiologyDevice.ManufacturerModelNo = C1ImplantDevice(i, COL_ManufacturerModelNumber) & ""
                    oClsCardiologyDevice.LeadType = C1ImplantDevice(i, COL_LeadsType) & ""
                    If Not C1ImplantDevice.GetData(i, COL_DateRemoved) = Nothing Then
                        oClsCardiologyDevice.DateRemoved = C1ImplantDevice.GetData(i, COL_DateRemoved)
                    End If


                    '''''' for proc-device association
                    '' oClsCardiologyDevice.Procedures = mPA_Procedure & ""
                    '''''' for proc-device association
                    If mPA_Flag = True Then
                        oClsCardiologyDevice.sProc = mPA_Procedure & ""
                    End If
                    oClsCardiologyDevice.ProcedureDate = mPA_ProcDate

                    '''''''''''''' by Ujwala as on 19112010
                    ''oClsCardiologyDevice.PhysicalLocation = C1ImplantDevice(i, COL_PhysicalLocationofDeviceImplant) & ""
                    ''oClsCardiologyDevice.sLeadLocation = C1ImplantDevice(i, COL_LeadLocation) & ""
                    ''oClsCardiologyDevice.sThresholdAtrial = C1ImplantDevice(i, COL_ThresholdAtrial) & ""
                    ''oClsCardiologyDevice.sThresholdVentricular = C1ImplantDevice(i, COL_ThresholdVentricular) & ""
                    ''oClsCardiologyDevice.sSensingAtrial = C1ImplantDevice(i, COL_SensingAtrial) & ""
                    ''oClsCardiologyDevice.sSensingVentricular = C1ImplantDevice(i, COL_SensingVentricular) & ""
                    ''oClsCardiologyDevice.sImpedenceAtrial = C1ImplantDevice(i, COL_ImpedenceAtrial) & ""
                    ''oClsCardiologyDevice.sImpedenceVentricular = C1ImplantDevice(i, COL_ImpedenceVentricular) & ""
                    ''''''''''''''''''''''
                    oClsCardiologyDevice.PhysicalLocation = txtPhLoc.Text
                    oClsCardiologyDevice.sLeadLocation = txtLeadLoc.Text
                    oClsCardiologyDevice.sThresholdAtrial = txtThAtr.Text
                    oClsCardiologyDevice.sThresholdVentricular = txtThVen.Text
                    oClsCardiologyDevice.sSensingAtrial = txtSenAtr.Text
                    oClsCardiologyDevice.sSensingVentricular = txtSenVen.Text
                    oClsCardiologyDevice.sImpedenceAtrial = txtImpAtr.Text
                    oClsCardiologyDevice.sImpedenceVentricular = txtImpVen.Text
                    '''''''''''''' by Ujwala as on 19112010

                    oClsCardioDeviceCollection.Add(oClsCardiologyDevice)
                End If
            Next

            'To Commit changes Last Row in C1Grid 
            If Not IsNothing(C1ImplantDevice) Then
                If C1ImplantDevice.Rows.Count > 0 Then
                    C1ImplantDevice.RowSel = 0
                End If
            End If

            'If oClsCardioDeviceCollection.Count > 0 Then
            Dim objCardio As New ClsEjectionFractionDBLayer
            Dim sPA_proc As String = ""
            If mPA_Flag = True Then
                sPA_proc = mPA_Procedure
            End If
            'objCardio.SaveCardiology(oClsCardioDeviceCollection, oClsCardiologyDevice.PatientID, oClsCardiologyDevice.VisitID, oClsCardiologyDevice.DateofImplant, sPA_proc)
            objCardio.SaveCardiology(oClsCardioDeviceCollection, m_patientID, m_VisitID, DtProcDate.Value, sPA_proc)
            If mPA_Flag = True Then
                objCardio.SaveCardiologyAssociation(oClsCardioDeviceCollection)
            End If
            'End If

            Me.Close()
            objCardio = Nothing
            oClsCardioDeviceCollection.Clear()
            oClsCardioDeviceCollection.Dispose()
            oClsCardioDeviceCollection = Nothing

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function SaveCardioDeviceData(ByVal oClsCardioCollection As ClsCardioDeviceCollection) As Boolean
        Dim cnn As New SqlConnection(GetConnectionString)
        cnn.Open()
        Dim cmd As SqlCommand = Nothing
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
                If Not IsNothing(cmd) Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If
                objCardio = Nothing
            Next
            Return True

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            cnn.Dispose()
            cnn = Nothing
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

        End Try
    End Function

    Private Function UpdateCardioDeviceData(ByVal oClsCardioCollection As ClsCardioDeviceCollection) As Boolean

        Dim cnn As New SqlConnection(GetConnectionString)
        cnn.Open()

        Dim cmd As SqlCommand = Nothing
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
                If Not IsNothing(cmd) Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If
            Next

            Return True

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            cnn.Dispose()
            cnn = Nothing
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

        End Try
    End Function

    Private Function SaveProcdureDeviceAssociation(ByVal oClsCardioCollection As ClsCardioDeviceCollection) As Boolean

        Dim cnn As New SqlConnection(GetConnectionString)
        cnn.Open()

        Dim cmd As SqlCommand = Nothing
        'Dim cnt As Int32
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
                If Not IsNothing(cmd) Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If
                objCardio = Nothing
            Next
            Return True

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            cnn.Dispose()
            cnn = Nothing
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
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

        Dim cmd As SqlCommand = Nothing
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
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
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
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            Return _OID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            cnn.Dispose()
            cnn = Nothing
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
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

    Private Function DeleteCVProcdureDeviceAssociation(ByVal ProcID As Int64) As Boolean

        Dim cnn As New SqlConnection(GetConnectionString)
        cnn.Open()

        Dim cmd As SqlCommand = Nothing
        'Dim cnt As Int32
        Dim _strSQL As String = ""
        Try

            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.Text
            _strSQL = "Delete from CV_ProcedureDeviceAssociation where nProcDeviceAssociationID=" & ProcID & ""
            cmd.CommandText = _strSQL
            If ((cmd.ExecuteNonQuery()) > 0) Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.Delete, "deleted records of Cardiology device ", 0, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                Return True
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.Delete, "deleted records of Cardiology device ", 0, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                Return False
            End If
           
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            cnn.Dispose()
            cnn = Nothing
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Delete, "deleted records of Cardiology device ", gstrLoginName, gstrClientMachineName, m_patientID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.Delete, "deleted records of Cardiology device ", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20100916

            ''
        End Try
    End Function

    Private Sub C1ImplantDevice_AfterAddRow(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1ImplantDevice.AfterAddRow
       
    End Sub

    Private Sub C1ImplantDevice_EnterCell(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1ImplantDevice.EnterCell
        With C1ImplantDevice
            If .RowSel >= 0 Then
                .SetCellStyle(.RowSel, COL_TypeofDevice, cStyleDeviceType)

                .SetCellStyle(.RowSel, COL_ProductName, cStyleProductName)

                .SetCellStyle(.RowSel, COL_DeviceManufacturer, cStyleDeviceManf)

                .SetCellStyle(.RowSel, COL_LeadsType, cStyleLeadtype)
            End If
        End With

    End Sub

    Private Sub C1ImplantDevice_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1ImplantDevice.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Public Function FillCPT() As DataTable
        Dim oDB As New DataBaseLayer
        Dim _strSQL As String = ""

        Try

            _strSQL = "select Distinct (isNull(rtrim(sCPTCode),'') + ' - ' + isNull(ltrim(sDescription),'')) as [CPT] from CPT_MST Where  sCPTCode <>'' AND sDescription<>''"
            Dim oCPT As DataTable = oDB.GetDataTable_Query(_strSQL)
            If Not oCPT Is Nothing Then
                Return oCPT
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        Finally
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function

    Public Function FillProcedure() As DataTable
        Dim oDB As New DataBaseLayer
        Dim _strSQL As String = ""

        Try

            _strSQL = "select isnull( sDescription,'') as [Procedure Description] from Category_MST where sCategoryType='Procedures' order by sDescription"
            Dim oCPT As DataTable = oDB.GetDataTable_Query(_strSQL)
            If Not oCPT Is Nothing Then
                Return oCPT
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        Finally
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function

    Public Function GetList(ByVal strQry As String) As DataTable
        Dim oDB As New DataBaseLayer
        Dim oList As New DataTable

        Try

            oList = oDB.GetDataTable_Query(strQry)
            If Not oList Is Nothing Then
                Return oList
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        Finally
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function

    Private Sub LoadUserGrid()
        Try
            AddControl()
            If Not IsNothing(dgCustomGrid) Then
                dgCustomGrid.Visible = True
                'dgCustomGrid.Width = pnlWordObj.Width
                pnlCustomTask.Width = 450
                dgCustomGrid.Width = pnlCustomTask.Width

                pnlCustomTask.Height = 250
                'pnlcustomTask.Width = dgCustomGrid.Width
                dgCustomGrid.Height = pnlCustomTask.Height
                dgCustomGrid.txtsearch.Width = 150


                dgCustomGrid.BringToFront()
                dgCustomGrid.SetVisible = False
                BindUserGrid()
                dgCustomGrid.Selectsearch(CustomDataGrid.enmcontrol.Search)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Cardiac Catheterization", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    'Remove customGrid control to form 
    Private Sub RemoveControl()
        If Not IsNothing(dgCustomGrid) Then
            'pnlWordObj.Controls.Remove(dgCustomGrid)
            pnlCustomTask.Controls.Remove(dgCustomGrid)
            dgCustomGrid.Visible = False
            dgCustomGrid.Dispose()
            dgCustomGrid = Nothing
        End If
    End Sub

    'Add customGrid control to form 
    Private Sub AddControl()

        If Not IsNothing(dgCustomGrid) Then
            RemoveControl()
        End If
        dgCustomGrid = New CustomTask
        pnlCustomTask.Controls.Add(dgCustomGrid)
        pnlCustomTask.BringToFront()

        ''''''''''''''''''''''
        ''dgCustomGrid.lblCaption.Visible = True
        ''''''''''''''''''''''

        Dim y As Int64
        Dim x As Int64

        If strLst = "cpt" Then
            y = 220
            x = 287
            'dgCustomGrid.lblCaption.Text = " CPT List "
        ElseIf strLst = "procedure" Then
            y = 220
            x = 500  ''300
            'dgCustomGrid.lblCaption.Text = " Procedure List "
        End If

        pnlCustomTask.Location = New Point(x, y)
        pnlCustomTask.Visible = True
        dgCustomGrid.Visible = True
        pnlCustomTask.BringToFront()
        dgCustomGrid.BringToFront()

    End Sub

    Private Sub BindUserGrid()
        Try
            Dim dt As DataTable = Nothing

            If strLst = "cpt" Then
                dt = FillCPT()
                pnlCustomTask.Width = 450
            ElseIf strLst = "procedure" Then
                dt = FillProcedure()
                pnlCustomTask.Width = 400
            End If
            ''''''''
            dgCustomGrid.Width = pnlCustomTask.Width
            ''''''''
            CustomDrugsGridStyle()
            Dim col As New DataColumn
            col.ColumnName = "Select"
            col.DataType = System.Type.GetType("System.Boolean")

            col.DefaultValue = CBool("False")
            dt.Columns.Add(col)

            If Not IsNothing(dt) Then
                ''dt.Columns("sICD9Display").Caption = "Diagnosis Name"
                dgCustomGrid.datasource(dt.Copy().DefaultView)
                dt.Dispose()
                dt = Nothing
            End If
            ''Reset the grid
            Dim _TotalWidth As Single = dgCustomGrid.C1Task.Width - 5
            dgCustomGrid.C1Task.Cols.Move(dgCustomGrid.C1Task.Cols.Count - 1, 0)
            dgCustomGrid.C1Task.AllowEditing = True
            dgCustomGrid.C1Task.Cols(0).AllowEditing = True
            dgCustomGrid.C1Task.Cols(0).Width = _TotalWidth * 0.1
            dgCustomGrid.C1Task.Cols(1).AllowEditing = False
            dgCustomGrid.C1Task.Cols(1).Width = _TotalWidth * 0.45
            dgCustomGrid.C1Task.ScrollBars = ScrollBars.Both
            dgCustomGrid.C1Task.ExtendLastCol = True

            

        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, "Catheterization", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Catheterization", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub CustomDrugsGridStyle()

        Dim _TotalWidth As Single = dgCustomGrid.C1Task.Width - 5

        ' '' Show Drugs Info
        With dgCustomGrid.C1Task
            .Cols.Fixed = 0
            .Rows.Fixed = 1
            .Cols.Count = Col_Cnt
            .AllowEditing = True

            .SetData(0, Col_Check, "Select")
            '.Cols(Col_Check).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            .Cols(Col_Check).Width = _TotalWidth * 0.1
            .Cols(Col_Check).AllowEditing = True
            .Cols(Col_Check).DataType = System.Type.GetType("System.Boolean")

            .SetData(0, Col_Name, "Name")
            .Cols(Col_Name).Width = _TotalWidth * 0.45
            ' .Cols(Col_DrugName).AllowEditing = False

        End With

    End Sub

    Private Sub SetCheckValues(ByVal LstBx As ListBox)
        Dim k As Integer
        For k = 0 To LstBx.Items.Count - 1
            For i As Int32 = 0 To dgCustomGrid.C1Task.Rows.Count - 1
                If dgCustomGrid.GetItem(i, 1).ToString.Trim = LstBx.Items(k).ToString.Trim Then
                    dgCustomGrid.C1Task.SetCellCheck(i, 0, C1.Win.C1FlexGrid.CheckEnum.Checked)
                    Exit For
                End If
            Next
        Next
    End Sub

    Private Sub dgCustomGrid_CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.CloseClick
        dgCustomGrid.Visible = False
        pnlCustomTask.Visible = False
        ''''''''
        LockControls(Me, pnlCustomTask, True)
        ''''''''
    End Sub

    Private Sub dgCustomGrid_OKClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.OKClick
        Try
            Dim i As Int32
            'Dim j As Int32-

            ''''''''
            LockControls(Me, pnlCustomTask, True)
            ''''''''

            '''''''''''''''''''''
            If strLst = "cpt" Then
                lstCPTcode.Items.Clear()
            ElseIf strLst = "procedure" Then
                lstProcedures.Items.Clear()
            End If
            '''''''''''''''''''''

            '''''''''''''''''''''
            dgCustomGrid.txtsearch.Text = ""
            '''''''''''''''''''''

            For i = 0 To dgCustomGrid.C1Task.Rows.Count - 1
                If dgCustomGrid.C1Task.GetCellCheck(i, 0) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                    If strLst = "cpt" Then
                        lstCPTcode.Items.Add(dgCustomGrid.GetItem(i, 1).ToString)
                    ElseIf strLst = "procedure" Then
                        lstProcedures.Items.Add(dgCustomGrid.GetItem(i, 1).ToString)                   
                    End If
                End If
            Next

            pnlCustomTask.Visible = False

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dgCustomGrid.Visible = False
        End Try
    End Sub

    Private Sub dgCustomGrid_SearchChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.SearchChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dvPatient As DataView = CType(dgCustomGrid.C1Task.DataSource(), DataView) '' (CType(dt.DefaultView, DataView))

            If IsNothing(dvPatient) Then
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim strPatientSearchDetails As String
            If Trim(dgCustomGrid.txtsearch.Text) <> "" Then
                strPatientSearchDetails = Replace(dgCustomGrid.txtsearch.Text, "'", "''")
                strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
            Else
                strPatientSearchDetails = ""
            End If

            dvPatient.RowFilter = "[" & dvPatient.Table.Columns(0).ColumnName & "] Like '%" & strPatientSearchDetails & "%' "
            'OR " _
            '                                                 & dvPatient.Table.Columns(2).ColumnName & " Like '%" & strPatientSearchDetails & "%' OR " _
            '                                                & dvPatient.Table.Columns(3).ColumnName & " Like '%" & strPatientSearchDetails & "%' "



            dgCustomGrid.Enabled = False
            dgCustomGrid.datasource(dvPatient)
            dgCustomGrid.Enabled = True
            Me.Cursor = Cursors.Default
            dgCustomGrid.txtsearch.Focus()
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.Select, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub btnBrowseCPT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseCPT.Click
        strLst = "cpt"
        LoadUserGrid()
        ''''''''
        SetCheckValues(lstCPTcode)
        ''''''''
        pnlCustomTask.BringToFront()

        ''''''''
        LockControls(Me, pnlCustomTask)
        ''''''''
    End Sub


    Private Sub btnClearCPT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearCPT.Click
        While (lstCPTcode.SelectedItems.Count > 0)          
            lstCPTcode.Items.Remove(lstCPTcode.SelectedItems(0))
        End While
    End Sub

    Private Sub BtnClearAllCPT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClearAllCPT.Click
        lstCPTcode.Items.Clear()       
    End Sub

    Private Sub btnBrowseProc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseProc.Click
        strLst = "procedure"
        LoadUserGrid()
        ''''''''
        SetCheckValues(lstProcedures)
        ''''''''
        pnlCustomTask.BringToFront()

        ''''''''
        LockControls(Me, pnlCustomTask)
        ''''''''
    End Sub

    Private Sub btnClearProc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearProc.Click
        While (lstProcedures.SelectedItems.Count > 0)
            lstProcedures.Items.Remove(lstProcedures.SelectedItems(0))
        End While
    End Sub

    Private Sub BtnClearAllProc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClearAllProc.Click
        lstProcedures.Items.Clear()
    End Sub

    Private Sub tlstrip_DeleteRow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlstrip_DeleteRow.Click
        If C1ImplantDevice.RowSel > 0 Then
            C1ImplantDevice.Rows.Remove(C1ImplantDevice.RowSel)
        End If
    End Sub

    Private Sub lstCPTcode_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstCPTcode.MouseMove
        objCath.SetListBoxToolTip(lstCPTcode, C1SuperTooltip1, Control.MousePosition)
    End Sub

    Private Sub lstProcedures_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstProcedures.MouseMove
        objCath.SetListBoxToolTip(lstProcedures, C1SuperTooltip1, Control.MousePosition)
    End Sub

   
    Private Sub C1ImplantDevice_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles C1ImplantDevice.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Delete
                    C1ImplantDevice.Clear(C1.Win.C1FlexGrid.ClearFlags.Content, C1ImplantDevice.Row, C1ImplantDevice.Col)
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class