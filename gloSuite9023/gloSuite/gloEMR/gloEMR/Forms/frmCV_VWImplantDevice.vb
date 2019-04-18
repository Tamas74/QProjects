Imports C1.Win.C1FlexGrid

Public Class frmCV_VWImplantDevice
    Implements IPatientContext

    Dim nPatientID As Long = 0
    Dim nCardiologyDeviceID As Long = 0
    Dim nVisitID As Long = 0
    Dim nExamID As Long = 0
    Dim nClinicID As Long = 0
    Dim nDateofImplant As Date

    Dim COL_CardiologyDeviceID As Integer = 0
    Dim COL_PatientID As Integer = 1
    Dim COL_ExamID As Integer = 2
    Dim COL_VisitID As Integer = 3
    Dim COL_ClinicID As Integer = 4
    Dim COL_DateofImplant As Integer = 5
    Dim COL_CPTCODE As Integer = 6
    Dim COL_TestType = 7
    Dim COL_DeviceType As Integer = 8
    Dim COL_ProductName As Integer = 9
    Dim COL_DeviceManufacturer As Integer = 10
    Dim COL_ProductSpecification As Integer = 11
    Dim COL_ProductSerialNo As Integer = 12
    Dim COL_ManufacturerModelNo As Integer = 13
    Dim COL_LeadType As Integer = 14
    Dim COL_DateRemoved As Integer = 15
    Dim COL_PhysicalLocation As Integer = 16
    Dim COL_Procedures As Integer = 17
    Dim COL_LeadLocation As Integer = 18
    Dim COL_ThresholdAtrial As Integer = 19
    Dim COL_ThresholdVentricular As Integer = 20
    Dim COL_SensingAtrial As Integer = 21
    Dim COL_SensingVentricular As Integer = 22
    Dim COL_ImpedanceAtrial As Integer = 23
    Dim COL_ImpedanceVentricular As Integer = 24
    Dim COL_DateofStudyInvisible As Integer = 25

    Dim COLUMN_COUNT As Integer = 26


    Public Sub New(ByVal PatientID As Long)
        MyBase.New()
        nPatientID = PatientID
        nClinicID = 1
        ''This call is required by the Windows Form Designer.
        InitializeComponent()
        ''Add any initialization after the InitializeComponent() call
    End Sub

    Private Sub frmCV_VWImplantDevice_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub frmCV_VWImplantDevice_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetGridStyle()
        FillImplantDevice()
        Try
            gloPatient.gloPatient.GetWindowTitle(Me, nPatientID, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub SetGridStyle()

        ' Dim struser As String
        With C1CV_ImplantDevice
            ' Dim i As Int16
            .Dock = DockStyle.Fill


            .Cols.Count = COLUMN_COUNT  '' COLUMN_COUNT
            .Rows.Fixed = 1
            .Rows.Count = 1
            .AllowEditing = False
            .AllowAddNew = False

            .Styles.ClearUnused()

            .Cols(COL_CardiologyDeviceID).Width = .Width * 0
            .Cols(COL_CardiologyDeviceID).AllowEditing = False
            .SetData(0, COL_CardiologyDeviceID, "Cardiology Device ID")
            .Cols(COL_CardiologyDeviceID).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_PatientID).Width = .Width * 0
            .Cols(COL_PatientID).AllowEditing = False
            .SetData(0, COL_PatientID, "Patient ID")
            .Cols(COL_VisitID).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_ExamID).Width = .Width * 0
            .Cols(COL_ExamID).AllowEditing = False
            .SetData(0, COL_VisitID, "Exam ID")
            .Cols(COL_ExamID).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_VisitID).Width = .Width * 0
            .Cols(COL_VisitID).AllowEditing = False
            .SetData(0, COL_VisitID, "Visit ID")
            .Cols(COL_VisitID).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_ClinicID).Width = .Width * 0
            .Cols(COL_ClinicID).AllowEditing = False
            .SetData(0, COL_VisitID, "Clinic ID")
            .Cols(COL_ClinicID).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_DateofImplant).Width = .Width * 1.3
            .Cols(COL_DateofImplant).AllowEditing = False
            .SetData(0, COL_DateofImplant, "Date of Implant")
            .Cols(COL_DateofImplant).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_CPTCODE).Width = .Width * 0
            .Cols(COL_CPTCODE).AllowEditing = False
            .SetData(0, COL_CPTCODE, "CPT Code")
            .Cols(COL_CPTCODE).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_TestType).Width = .Width * 0
            .Cols(COL_TestType).AllowEditing = False
            .SetData(0, COL_TestType, "Test Type")
            .Cols(COL_TestType).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_DeviceType).Width = .Width * 0
            .Cols(COL_DeviceType).AllowEditing = False
            .SetData(0, COL_DeviceType, "Device Type ")
            .Cols(COL_DeviceType).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_ProductName).Width = .Width * 0
            .Cols(COL_ProductName).AllowEditing = False
            .SetData(0, COL_ProductName, "Product Name")
            .Cols(COL_ProductName).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_DeviceManufacturer).Width = .Width * 0
            .Cols(COL_DeviceManufacturer).AllowEditing = False
            .SetData(0, COL_DeviceManufacturer, "Device Manufacturer")
            .Cols(COL_DeviceManufacturer).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_ProductSpecification).Width = .Width * 0
            .Cols(COL_ProductSpecification).AllowEditing = False
            .SetData(0, COL_ProductSpecification, "Product Specification")
            .Cols(COL_ProductSpecification).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_ProductSerialNo).Width = .Width * 0
            .Cols(COL_ProductSerialNo).AllowEditing = False
            .SetData(0, COL_ProductSerialNo, "Product Serial No")
            .Cols(COL_ProductSerialNo).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_ManufacturerModelNo).Width = .Width * 0
            .Cols(COL_ManufacturerModelNo).AllowEditing = False
            .SetData(0, COL_ManufacturerModelNo, "Manufacturer Model No")
            .Cols(COL_ManufacturerModelNo).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_LeadType).Width = .Width * 0
            .Cols(COL_LeadType).AllowEditing = False
            .SetData(0, COL_LeadType, "Lead Type")
            .Cols(COL_LeadType).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_DateRemoved).Width = 0
            .SetData(0, COL_DateRemoved, "Date Removed")
            .Cols(COL_DateRemoved).DataType = GetType(String)
            .Cols(COL_DateRemoved).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_PhysicalLocation).Width = .Width * 0
            .Cols(COL_PhysicalLocation).AllowEditing = False
            .SetData(0, COL_PhysicalLocation, "Physical Location")
            .Cols(COL_PhysicalLocation).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_Procedures).Width = .Width * 0
            .Cols(COL_Procedures).AllowEditing = False
            .SetData(0, COL_Procedures, "Procedures")
            .Cols(COL_Procedures).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_LeadLocation).Width = .Width * 0
            .Cols(COL_LeadLocation).AllowEditing = False
            .SetData(0, COL_LeadLocation, "Lead Location")
            .Cols(COL_LeadLocation).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_ThresholdAtrial).Width = .Width * 0
            .Cols(COL_ThresholdAtrial).AllowEditing = False
            .SetData(0, COL_ThresholdAtrial, "Threshold Atrial")
            .Cols(COL_ThresholdAtrial).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_ThresholdVentricular).Width = .Width * 0
            .Cols(COL_ThresholdVentricular).AllowEditing = False
            .SetData(0, COL_ThresholdVentricular, "Threshold Ventricular")
            .Cols(COL_ThresholdVentricular).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_SensingAtrial).Width = .Width * 0
            .Cols(COL_SensingAtrial).AllowEditing = False
            .SetData(0, COL_SensingAtrial, "Sensing Atrial")
            .Cols(COL_SensingAtrial).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_SensingVentricular).Width = .Width * 0
            .Cols(COL_SensingVentricular).AllowEditing = False
            .SetData(0, COL_SensingVentricular, "Sensing Ventricular")
            .Cols(COL_SensingVentricular).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_ImpedanceAtrial).Width = .Width * 0
            .Cols(COL_ImpedanceAtrial).AllowEditing = False
            .SetData(0, COL_ImpedanceAtrial, "Impedance Atrial")
            .Cols(COL_ImpedanceAtrial).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_ImpedanceVentricular).Width = .Width * 0
            .Cols(COL_ImpedanceVentricular).AllowEditing = False
            .SetData(0, COL_ImpedanceVentricular, "Impedance Ventricular")
            .Cols(COL_ImpedanceVentricular).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_DateofStudyInvisible).Width = 0
            .SetData(0, COL_DateofStudyInvisible, "Date of Study Invisible")
            .Cols(COL_DateofStudyInvisible).DataType = GetType(String)
            .Cols(COL_DateofStudyInvisible).TextAlignFixed = TextAlignEnum.LeftCenter

        End With
    End Sub


    Private Sub FillImplantDevice()
        Try

            Dim _Row As Integer

            ''set properties of treeview in flexgrid
            With C1CV_ImplantDevice
                .Tree.Column = COL_DateofImplant
                .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
                .Tree.LineStyle = Drawing2D.DashStyle.Solid
                .Tree.Indent = 15
                .Cols(COL_DateofImplant).TextAlign = TextAlignEnum.LeftCenter
            End With

            Dim dtDateofImplant As DataTable
            Dim dtCPT As DataTable
            Dim dtProcedures As DataTable
            Dim dtProductInfo As DataTable
            Dim dtDeviceType As DataTable
            'Dim dtLeadType As DataTable
            'Dim dtDateRemoved As DataTable
            'Dim dtPhysicalLocation As DataTable
            'Dim dtLeadLocation As DataTable
            'Dim dtPacingThreshold As DataTable
            'Dim dtSensing As DataTable
            'Dim dtImpedance As DataTable


            Dim nDOS As Int16
            Dim nCPT As Int16
            Dim nProcedure As Int16
            Dim nProductInfo As Int16
            Dim nDeviceType As Int16
            'Dim nLeadType As Int16
            'Dim nDateRemoved As Int16
            'Dim nPhysicalLocation As Int16
            'Dim nLeadLocation As Int16
            'Dim nPacingThreshold As Int16
            'Dim nSensing As Int16
            'Dim nImpedance As Int16


            Dim strdtQry As String
            Dim strCptQry As String
            Dim strProcedureQry As String
            'Dim strProductInfoQry As String
            'Dim strDeviceTypeQry As String
            'Dim strLeadTypeQry As String
            'Dim strDateRemovedQry As String
            'Dim strPhysicalLocationQry As String
            'Dim strLeadLocationQry As String
            'Dim strPacingThresholdQry As String
            'Dim strSensingQry As String
            'Dim strImpedanceQry
            Dim strconcatCPT1 As String = ""
            Dim nextRow As Integer
            Dim strCombine As String = ""



            strdtQry = "SELECT Distinct isnull(nPatientID,0) as nPatientID,isnull(nExamID,0) as nExamID,isnull(nVisitID,0) as nVisitID,isnull(nClinicID,0) as nClinicID,dtDateofImplant as DateOfImplant FROM CV_CardiologyDevice WHERE nPatientID='" & nPatientID & "'  order by DateOfImplant"
            Dim oDB As New gloStream.gloDataBase.gloDataBase
            oDB.Connect(GetConnectionString)
            dtDateofImplant = oDB.ReadQueryDataTable(strdtQry)
            oDB.Disconnect()

            With dtDateofImplant
                If IsNothing(dtDateofImplant) = False Then
                    For nDOS = 0 To dtDateofImplant.Rows.Count - 1
                        Dim CardiologyDeviceID As Int64 = 0
                        Dim PatientID As Int64 = 0
                        Dim VisitID As Int64 = 0
                        Dim ExamID As Int64 = 0
                        Dim ClinicID As Int64 = 0
                        ' Dim DateofImplant As Date


                        Dim count As Integer = nDOS + 1
                        If CStr(dtDateofImplant.Rows(nDOS)("DateOfImplant")).Trim <> "" Then
                            C1CV_ImplantDevice.Rows.Add()
                            _Row = C1CV_ImplantDevice.Rows.Count - 1
                            ''set the properties for newly added row
                            With C1CV_ImplantDevice.Rows(_Row)
                                .AllowEditing = False
                                .ImageAndText = True
                                .Height = 24
                                .IsNode = True
                                .Node.Level = 0
                                .Node.Data = Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString
                                .Node.Image = ImageList1.Images(0)
                            End With
                            nextRow = _Row
                            With C1CV_ImplantDevice
                                .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                .SetData(_Row, COL_ExamID, dtDateofImplant.Rows(nDOS)("nExamID"))
                                .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                .SetData(_Row, COL_ClinicID, dtDateofImplant.Rows(nDOS)("nClinicID"))
                                ''.SetData(_Row, COL_CardiologyDeviceID, dtDateofImplant.Rows(nDOS)("nCardiologyDeviceID"))
                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)

                                PatientID = dtDateofImplant.Rows(nDOS)("nPatientID")
                                VisitID = dtDateofImplant.Rows(nDOS)("nVisitID")
                                ExamID = dtDateofImplant.Rows(nDOS)("nExamID")
                                ClinicID = dtDateofImplant.Rows(nDOS)("nClinicID")
                                ''CardiologyDeviceID = dtDateofImplant.Rows(nDOS)("nCardiologyDeviceID")
                            End With


                            Dim dtImplantDate As Date = Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateofImplant")).ToShortDateString()



                            ''Query for selecting CPTCode
                            strCptQry = "SELECT DISTINCT isnull(sCPTCode,'') as CPTCode from CV_CardiologyDevice where nPatientID=" & nPatientID & " AND dtDateofImplant='" & dtImplantDate & "' AND sCPTCode<>''"
                            oDB.Connect(GetConnectionString)
                            dtCPT = oDB.ReadQueryDataTable(strCptQry)
                            oDB.Disconnect()


                            With dtCPT
                                If IsNothing(dtCPT) = False Then
                                    If dtCPT.Rows.Count >= 0 Then
                                        C1CV_ImplantDevice.Rows.Add()
                                        _Row = C1CV_ImplantDevice.Rows.Count - 1
                                        With C1CV_ImplantDevice.Rows(_Row)
                                            .AllowEditing = False
                                            .ImageAndText = True
                                            .Height = 24
                                            .IsNode = True
                                            .Node.Level = 1
                                            .Node.Data = "CPT"
                                            .Node.Image = ImageList1.Images(1)
                                        End With
                                        With C1CV_ImplantDevice
                                            .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                            .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                        End With
                                    End If
                                    For nCPT = 0 To dtCPT.Rows.Count - 1
                                        Dim strCPT As String = dtCPT.Rows(nCPT)("CPTCode").ToString()
                                        If strCPT.Trim <> "" Then
                                            C1CV_ImplantDevice.Rows.Add()
                                            _Row = C1CV_ImplantDevice.Rows.Count - 1
                                            With C1CV_ImplantDevice.Rows(_Row)
                                                .AllowEditing = True
                                                .ImageAndText = True
                                                .Height = 24
                                                .IsNode = True
                                                .Node.Level = 2
                                                .TextAlign = TextAlignEnum.LeftCenter
                                                .Node.Data = strCPT
                                                .Node.Image = ImageList1.Images(3)
                                            End With
                                            With C1CV_ImplantDevice
                                                .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                                .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                            End With
                                        End If
                                    Next
                                End If
                            End With


                            ''Query for selecting Procedures
                            strProcedureQry = "SELECT DISTINCT isnull(sProcedures,'') as Procedures from CV_CardiologyDevice where nPatientID=" & nPatientID & " AND dtDateofImplant='" & dtImplantDate & "' AND sProcedures<>'' "
                            oDB.Connect(GetConnectionString)
                            dtProcedures = oDB.ReadQueryDataTable(strProcedureQry)
                            oDB.Disconnect()


                            With dtProcedures
                                If IsNothing(dtProcedures) = False Then
                                    If dtProcedures.Rows.Count >= 0 Then
                                        C1CV_ImplantDevice.Rows.Add()
                                        _Row = C1CV_ImplantDevice.Rows.Count - 1
                                        With C1CV_ImplantDevice.Rows(_Row)
                                            .AllowEditing = False
                                            .ImageAndText = True
                                            .Height = 24
                                            .IsNode = True
                                            .Node.Level = 1
                                            .Node.Data = "Procedures"
                                            .Node.Image = ImageList1.Images(2)
                                        End With
                                        With C1CV_ImplantDevice
                                            .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                            .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                        End With
                                    End If
                                    For nProcedure = 0 To dtProcedures.Rows.Count - 1
                                        Dim strProcedures As String = dtProcedures.Rows(nProcedure)("Procedures").ToString()
                                        If strProcedures.Trim <> "" Then
                                            C1CV_ImplantDevice.Rows.Add()
                                            _Row = C1CV_ImplantDevice.Rows.Count - 1
                                            With C1CV_ImplantDevice.Rows(_Row)
                                                .AllowEditing = True
                                                .ImageAndText = True
                                                .Height = 24
                                                .IsNode = True
                                                .Node.Level = 2
                                                .TextAlign = TextAlignEnum.LeftCenter
                                                .Node.Data = strProcedures
                                                .Node.Image = ImageList1.Images(3)
                                            End With
                                            With C1CV_ImplantDevice
                                                .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                                .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                            End With
                                        End If
                                    Next
                                End If
                            End With


                            '''''''''''''''''''''''''''' By Ujwala as on 11192010
                            ''Query for selecting Product Information
                            strProcedureQry = "SELECT DISTINCT isnull(sPhysicalLocation,'') as sPhysicalLocation , isnull(sLeadLocation,'') as sLeadLocation , isnull(sThresholdAtrial,'') as sThresholdAtrial,isnull(sThresholdVentricular,'') as sThresholdVentricular, isnull(sSensingAtrial,'') as sSensingAtrial,isnull(sSensingVentricular,'') as sSensingVentricular,isnull(sImpedenceAtrial,'') as sImpedenceAtrial,isnull(sImpedenceVentricular,'') as sImpedenceVentricular from CV_CardiologyDevice where nPatientID=" & nPatientID & " AND dtDateofImplant='" & dtImplantDate & "'"
                            oDB.Connect(GetConnectionString)
                            dtProductInfo = oDB.ReadQueryDataTable(strProcedureQry)
                            oDB.Disconnect()

                            If Not IsNothing(dtProductInfo.Rows(0)("sPhysicalLocation").ToString()) Then
                                If (dtProductInfo.Rows(0)("sPhysicalLocation").ToString() <> "") Then
                                    C1CV_ImplantDevice.Rows.Add()
                                    _Row = C1CV_ImplantDevice.Rows.Count - 1
                                    With C1CV_ImplantDevice.Rows(_Row)
                                        .AllowEditing = True
                                        .ImageAndText = True
                                        .Height = 24
                                        .IsNode = True
                                        .Node.Level = 1
                                        .Node.Data = "Physical Location" + " " + ":" + " " + dtProductInfo.Rows(0)("sPhysicalLocation").ToString()
                                        .Node.Image = ImageList1.Images(22)
                                    End With
                                    With C1CV_ImplantDevice
                                        .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                        .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                        .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                    End With
                                End If
                            End If

                            If Not IsNothing(dtProductInfo.Rows(0)("sLeadLocation").ToString()) Then
                                If (dtProductInfo.Rows(0)("sLeadLocation").ToString() <> "") Then
                                    C1CV_ImplantDevice.Rows.Add()
                                    _Row = C1CV_ImplantDevice.Rows.Count - 1
                                    With C1CV_ImplantDevice.Rows(_Row)
                                        .AllowEditing = True
                                        .ImageAndText = True
                                        .Height = 24
                                        .IsNode = True
                                        .Node.Level = 1
                                        .Node.Data = "Lead Location" + " " + ":" + " " + dtProductInfo.Rows(0)("sLeadLocation").ToString()
                                        .Node.Image = ImageList1.Images(16)
                                    End With
                                    With C1CV_ImplantDevice
                                        .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                        .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                        .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                    End With
                                End If
                            End If

                            If Not IsNothing(dtProductInfo.Rows(0)("sThresholdAtrial").ToString()) Then
                                If (dtProductInfo.Rows(0)("sThresholdAtrial").ToString() <> "") Then
                                    C1CV_ImplantDevice.Rows.Add()
                                    _Row = C1CV_ImplantDevice.Rows.Count - 1
                                    With C1CV_ImplantDevice.Rows(_Row)
                                        .AllowEditing = True
                                        .ImageAndText = True
                                        .Height = 24
                                        .IsNode = True
                                        .Node.Level = 1
                                        .Node.Data = "Threshold Atrial" + " " + ":" + " " + dtProductInfo.Rows(0)("sThresholdAtrial").ToString()
                                        .Node.Image = ImageList1.Images(23)
                                    End With
                                    With C1CV_ImplantDevice
                                        .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                        .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                        .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                    End With
                                End If
                            End If

                            If Not IsNothing(dtProductInfo.Rows(0)("sThresholdVentricular").ToString()) Then
                                If (dtProductInfo.Rows(0)("sThresholdVentricular").ToString() <> "") Then
                                    C1CV_ImplantDevice.Rows.Add()
                                    _Row = C1CV_ImplantDevice.Rows.Count - 1
                                    With C1CV_ImplantDevice.Rows(_Row)
                                        .AllowEditing = True
                                        .ImageAndText = True
                                        .Height = 24
                                        .IsNode = True
                                        .Node.Level = 1
                                        .Node.Data = "Threshold Ventricular" + " " + ":" + " " + dtProductInfo.Rows(0)("sThresholdVentricular").ToString()
                                        .Node.Image = ImageList1.Images(24)
                                    End With
                                    With C1CV_ImplantDevice
                                        .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                        .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                        .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                    End With
                                End If
                            End If

                            If Not IsNothing(dtProductInfo.Rows(0)("sSensingAtrial")) Then
                                If (dtProductInfo.Rows(0)("sSensingAtrial").ToString() <> "") Then
                                    C1CV_ImplantDevice.Rows.Add()
                                    _Row = C1CV_ImplantDevice.Rows.Count - 1
                                    With C1CV_ImplantDevice.Rows(_Row)
                                        .AllowEditing = True
                                        .ImageAndText = True
                                        .Height = 24
                                        .IsNode = True
                                        .Node.Level = 1
                                        .Node.Data = "Sensing Atrial" + " " + ":" + " " + dtProductInfo.Rows(0)("sSensingAtrial").ToString()
                                        .Node.Image = ImageList1.Images(25)
                                    End With
                                    With C1CV_ImplantDevice
                                        .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                        .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                        .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                    End With
                                End If
                            End If

                            If Not IsNothing(dtProductInfo.Rows(0)("sSensingVentricular").ToString()) Then
                                If (dtProductInfo.Rows(0)("sSensingVentricular").ToString() <> "") Then
                                    C1CV_ImplantDevice.Rows.Add()
                                    _Row = C1CV_ImplantDevice.Rows.Count - 1
                                    With C1CV_ImplantDevice.Rows(_Row)
                                        .AllowEditing = True
                                        .ImageAndText = True
                                        .Height = 24
                                        .IsNode = True
                                        .Node.Level = 1
                                        .Node.Data = "Sensing Ventricular" + " " + ":" + " " + dtProductInfo.Rows(0)("sSensingVentricular").ToString()
                                        .Node.Image = ImageList1.Images(26)
                                    End With
                                    With C1CV_ImplantDevice
                                        .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                        .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                        .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                    End With
                                End If
                            End If

                            If Not IsNothing(dtProductInfo.Rows(0)("sImpedenceAtrial").ToString()) Then
                                If (dtProductInfo.Rows(0)("sImpedenceAtrial").ToString() <> "") Then
                                    C1CV_ImplantDevice.Rows.Add()
                                    _Row = C1CV_ImplantDevice.Rows.Count - 1
                                    With C1CV_ImplantDevice.Rows(_Row)
                                        .AllowEditing = True
                                        .ImageAndText = True
                                        .Height = 24
                                        .IsNode = True
                                        .Node.Level = 1
                                        .Node.Data = "Impedence Atrial" + " " + ":" + " " + dtProductInfo.Rows(0)("sImpedenceAtrial").ToString()
                                        .Node.Image = ImageList1.Images(27)
                                    End With
                                    With C1CV_ImplantDevice
                                        .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                        .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                        .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                    End With
                                End If
                            End If

                            If Not IsNothing(dtProductInfo.Rows(0)("sImpedenceVentricular").ToString()) Then
                                If (dtProductInfo.Rows(0)("sImpedenceVentricular").ToString() <> "") Then
                                    C1CV_ImplantDevice.Rows.Add()
                                    _Row = C1CV_ImplantDevice.Rows.Count - 1
                                    With C1CV_ImplantDevice.Rows(_Row)
                                        .AllowEditing = True
                                        .ImageAndText = True
                                        .Height = 24
                                        .IsNode = True
                                        .Node.Level = 1
                                        .Node.Data = "Impedence Ventricular" + " " + ":" + " " + dtProductInfo.Rows(0)("sImpedenceVentricular").ToString()
                                        .Node.Image = ImageList1.Images(28)
                                    End With
                                    With C1CV_ImplantDevice
                                        .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                        .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                        .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                    End With
                                End If
                            End If
                            '''''''''''''''''''''''''''' By Ujwala as on 11192010

                            ''Query for selecting Type of Device
                            strCptQry = "SELECT DISTINCT isnull(sDeviceType,'') as DeviceType from CV_CardiologyDevice where nPatientID=" & nPatientID & " AND dtDateofImplant='" & dtImplantDate & "' AND sDeviceType<>'' "
                            oDB.Connect(GetConnectionString)
                            dtDeviceType = oDB.ReadQueryDataTable(strCptQry)
                            oDB.Disconnect()


                            With dtDeviceType
                                If IsNothing(dtDeviceType) = False Then
                                    If dtDeviceType.Rows.Count >= 0 Then
                                        C1CV_ImplantDevice.Rows.Add()
                                        _Row = C1CV_ImplantDevice.Rows.Count - 1
                                        With C1CV_ImplantDevice.Rows(_Row)
                                            .AllowEditing = False
                                            .ImageAndText = True
                                            .Height = 24
                                            .IsNode = True
                                            .Node.Level = 1
                                            .Node.Data = "Type Of Device"
                                            .Node.Image = ImageList1.Images(14)
                                        End With
                                        With C1CV_ImplantDevice
                                            .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                            .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                        End With
                                    End If
                                    For nDeviceType = 0 To dtDeviceType.Rows.Count - 1
                                        Dim strDeviceType As String = dtDeviceType.Rows(nDeviceType)("DeviceType")
                                        If strDeviceType.Trim <> "" Then
                                            C1CV_ImplantDevice.Rows.Add()
                                            _Row = C1CV_ImplantDevice.Rows.Count - 1

                                            With C1CV_ImplantDevice.Rows(_Row)
                                                .AllowEditing = True
                                                .ImageAndText = True
                                                .Height = 24
                                                .IsNode = True
                                                .Node.Level = 2
                                                .Node.Data = strDeviceType
                                                .Node.Image = ImageList1.Images(21)
                                            End With
                                            With C1CV_ImplantDevice
                                                .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                                .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                            End With


                                            ''Query for selecting Product Information
                                            strProcedureQry = "SELECT DISTINCT isnull(sProductName,'') as sProductName,isnull(sDeviceManufacturer,'') as sDeviceManufacturer,isnull(sProductSpecification,'') as sProductSpecification,isnull(sProductSerialNo,'') as sProductSerialNo,isnull(sManufacturerModelNo,'') as sManufacturerModelNo,isnull(sLeadType,'') as sLeadType,dtDateRemoved , isnull(sPhysicalLocation,'') as sPhysicalLocation , isnull(sLeadLocation,'') as sLeadLocation , isnull(sThresholdAtrial,'') as sThresholdAtrial,isnull(sThresholdVentricular,'') as sThresholdVentricular, isnull(sSensingAtrial,'') as sSensingAtrial,isnull(sSensingVentricular,'') as sSensingVentricular,isnull(sImpedenceAtrial,'') as sImpedenceAtrial,isnull(sImpedenceVentricular,'') as sImpedenceVentricular from CV_CardiologyDevice where nPatientID=" & nPatientID & " AND dtDateofImplant='" & dtImplantDate & "' and sDeviceType='" & strDeviceType.Replace("'", "''") & "'"
                                            oDB.Connect(GetConnectionString)
                                            dtProductInfo = oDB.ReadQueryDataTable(strProcedureQry)
                                            oDB.Disconnect()

                                            ''Added Rahul on 20100903
                                            With dtProductInfo
                                                If IsNothing(dtProductInfo) = False Then
                                                    For nProductInfo = 0 To dtProductInfo.Rows.Count - 1
                                                        If dtProductInfo.Rows.Count >= 0 Then
                                                            C1CV_ImplantDevice.Rows.Add()
                                                            _Row = C1CV_ImplantDevice.Rows.Count - 1
                                                            With C1CV_ImplantDevice.Rows(_Row)
                                                                .AllowEditing = False
                                                                .ImageAndText = True
                                                                .Height = 24
                                                                .IsNode = True
                                                                .Node.Level = 3
                                                                .Node.Data = strDeviceType + " " + (nProductInfo + 1).ToString()
                                                                .Node.Image = ImageList1.Images(21)
                                                            End With
                                                            With C1CV_ImplantDevice
                                                                .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                                                .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                                            End With
                                                        End If
                                                        If (dtProductInfo.Rows(nProductInfo)("sProductName").ToString() <> "") Then
                                                            C1CV_ImplantDevice.Rows.Add()
                                                            _Row = C1CV_ImplantDevice.Rows.Count - 1
                                                            With C1CV_ImplantDevice.Rows(_Row)
                                                                .AllowEditing = True
                                                                .ImageAndText = True
                                                                .Height = 24
                                                                .IsNode = True
                                                                .Node.Level = 4
                                                                .Node.Data = "Product Name" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("sProductName").ToString()
                                                                .Node.Image = ImageList1.Images(3)
                                                            End With
                                                            With C1CV_ImplantDevice
                                                                .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                                                .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                                            End With
                                                        End If
                                                        If Not IsNothing(dtProductInfo.Rows(nProductInfo)("sDeviceManufacturer").ToString()) Then
                                                            If (dtProductInfo.Rows(nProductInfo)("sDeviceManufacturer").ToString() <> "") Then
                                                                C1CV_ImplantDevice.Rows.Add()
                                                                _Row = C1CV_ImplantDevice.Rows.Count - 1
                                                                With C1CV_ImplantDevice.Rows(_Row)
                                                                    .AllowEditing = True
                                                                    .ImageAndText = True
                                                                    .Height = 24
                                                                    .IsNode = True
                                                                    .Node.Level = 4
                                                                    .Node.Data = "Device Manufacturer" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("sDeviceManufacturer").ToString()
                                                                    .Node.Image = ImageList1.Images(3)
                                                                End With
                                                                With C1CV_ImplantDevice
                                                                    .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                                                    .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                                                    .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                                                End With
                                                            End If
                                                        End If
                                                        If Not IsNothing(dtProductInfo.Rows(nProductInfo)("sProductSpecification").ToString()) Then
                                                            If (dtProductInfo.Rows(nProductInfo)("sProductSpecification").ToString() <> "") Then
                                                                C1CV_ImplantDevice.Rows.Add()
                                                                _Row = C1CV_ImplantDevice.Rows.Count - 1
                                                                With C1CV_ImplantDevice.Rows(_Row)
                                                                    .AllowEditing = True
                                                                    .ImageAndText = True
                                                                    .Height = 24
                                                                    .IsNode = True
                                                                    .Node.Level = 4
                                                                    .Node.Data = "Product Specification" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("sProductSpecification").ToString()
                                                                    .Node.Image = ImageList1.Images(3)
                                                                End With
                                                                With C1CV_ImplantDevice
                                                                    .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                                                    .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                                                    .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                                                End With
                                                            End If
                                                        End If
                                                        If Not IsNothing(dtProductInfo.Rows(nProductInfo)("sProductSerialNo").ToString()) Then
                                                            If (dtProductInfo.Rows(nProductInfo)("sProductSerialNo").ToString() <> "") Then
                                                                C1CV_ImplantDevice.Rows.Add()
                                                                _Row = C1CV_ImplantDevice.Rows.Count - 1
                                                                With C1CV_ImplantDevice.Rows(_Row)
                                                                    .AllowEditing = True
                                                                    .ImageAndText = True
                                                                    .Height = 24
                                                                    .IsNode = True
                                                                    .Node.Level = 4
                                                                    .Node.Data = "Product Serial No" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("sProductSerialNo").ToString()
                                                                    .Node.Image = ImageList1.Images(3)
                                                                End With
                                                                With C1CV_ImplantDevice
                                                                    .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                                                    .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                                                    .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                                                End With
                                                            End If
                                                        End If
                                                        If Not IsNothing(dtProductInfo.Rows(nProductInfo)("sManufacturerModelNo").ToString()) Then
                                                            If (dtProductInfo.Rows(nProductInfo)("sManufacturerModelNo").ToString() <> "") Then
                                                                C1CV_ImplantDevice.Rows.Add()
                                                                _Row = C1CV_ImplantDevice.Rows.Count - 1
                                                                With C1CV_ImplantDevice.Rows(_Row)
                                                                    .AllowEditing = True
                                                                    .ImageAndText = True
                                                                    .Height = 24
                                                                    .IsNode = True
                                                                    .Node.Level = 4
                                                                    .Node.Data = "Manufacturer Model No" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("sManufacturerModelNo").ToString()
                                                                    .Node.Image = ImageList1.Images(3)
                                                                End With
                                                                With C1CV_ImplantDevice
                                                                    .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                                                    .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                                                    .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                                                End With
                                                            End If
                                                        End If

                                                        '''''''''''''''''''''''''''''''''''''''''''
                                                        If Not IsNothing(dtProductInfo.Rows(nProductInfo)("sLeadType").ToString()) Then
                                                            If (dtProductInfo.Rows(nProductInfo)("sLeadType").ToString() <> "") Then
                                                                C1CV_ImplantDevice.Rows.Add()
                                                                _Row = C1CV_ImplantDevice.Rows.Count - 1
                                                                With C1CV_ImplantDevice.Rows(_Row)
                                                                    .AllowEditing = True
                                                                    .ImageAndText = True
                                                                    .Height = 24
                                                                    .IsNode = True
                                                                    .Node.Level = 4
                                                                    .Node.Data = "Lead Type" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("sLeadType").ToString()
                                                                    .Node.Image = ImageList1.Images(3)
                                                                End With
                                                                With C1CV_ImplantDevice
                                                                    .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                                                    .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                                                    .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                                                End With
                                                            End If
                                                        End If

                                                        If Not IsNothing(dtProductInfo.Rows(nProductInfo)("dtDateRemoved").ToString()) Then
                                                            If (dtProductInfo.Rows(nProductInfo)("dtDateRemoved").ToString() <> "") Then
                                                                C1CV_ImplantDevice.Rows.Add()
                                                                _Row = C1CV_ImplantDevice.Rows.Count - 1
                                                                With C1CV_ImplantDevice.Rows(_Row)
                                                                    .AllowEditing = True
                                                                    .ImageAndText = True
                                                                    .Height = 24
                                                                    .IsNode = True
                                                                    .Node.Level = 4
                                                                    .Node.Data = "Date Removed" + " : " + FormatDateTime(dtProductInfo.Rows(nProductInfo)("dtDateRemoved"), DateFormat.ShortDate)
                                                                    .Node.Image = ImageList1.Images(3)
                                                                End With
                                                                With C1CV_ImplantDevice
                                                                    .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                                                    .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                                                    .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                                                End With
                                                            End If
                                                        End If


                                                    Next
                                                End If

                                            End With
                                            ''----

                                            With dtProductInfo
                                                If IsNothing(dtProductInfo) = False Then
                                                    For nProductInfo = 0 To dtProductInfo.Rows.Count - 1
                                                        'If Not IsNothing(dtProductInfo.Rows(nProductInfo)("sProductName").ToString()) Then
                                                        '    If (dtProductInfo.Rows(nProductInfo)("sProductName").ToString() <> "") Then
                                                        '        C1CV_ImplantDevice.Rows.Add()
                                                        '        _Row = C1CV_ImplantDevice.Rows.Count - 1
                                                        '        With C1CV_ImplantDevice.Rows(_Row)
                                                        '            .AllowEditing = True
                                                        '            .ImageAndText = True
                                                        '            .Height = 24
                                                        '            .IsNode = True
                                                        '            .Node.Level = 3
                                                        '            .Node.Data = "Product Name" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("sProductName").ToString()
                                                        '            .Node.Image = ImageList1.Images(3)
                                                        '        End With
                                                        '        With C1CV_ImplantDevice
                                                        '            .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                                        '            .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                                        '            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                                        '        End With
                                                        '    End If
                                                        'End If
                                                        'If Not IsNothing(dtProductInfo.Rows(nProductInfo)("sDeviceManufacturer").ToString()) Then
                                                        '    If (dtProductInfo.Rows(nProductInfo)("sDeviceManufacturer").ToString() <> "") Then
                                                        '        C1CV_ImplantDevice.Rows.Add()
                                                        '        _Row = C1CV_ImplantDevice.Rows.Count - 1
                                                        '        With C1CV_ImplantDevice.Rows(_Row)
                                                        '            .AllowEditing = True
                                                        '            .ImageAndText = True
                                                        '            .Height = 24
                                                        '            .IsNode = True
                                                        '            .Node.Level = 3
                                                        '            .Node.Data = "Device Manufacturer" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("sDeviceManufacturer").ToString()
                                                        '            .Node.Image = ImageList1.Images(3)
                                                        '        End With
                                                        '        With C1CV_ImplantDevice
                                                        '            .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                                        '            .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                                        '            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                                        '        End With
                                                        '    End If
                                                        'End If
                                                        'If Not IsNothing(dtProductInfo.Rows(nProductInfo)("sProductSpecification").ToString()) Then
                                                        '    If (dtProductInfo.Rows(nProductInfo)("sProductSpecification").ToString() <> "") Then
                                                        '        C1CV_ImplantDevice.Rows.Add()
                                                        '        _Row = C1CV_ImplantDevice.Rows.Count - 1
                                                        '        With C1CV_ImplantDevice.Rows(_Row)
                                                        '            .AllowEditing = True
                                                        '            .ImageAndText = True
                                                        '            .Height = 24
                                                        '            .IsNode = True
                                                        '            .Node.Level = 3
                                                        '            .Node.Data = "Product Specification" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("sProductSpecification").ToString()
                                                        '            .Node.Image = ImageList1.Images(3)
                                                        '        End With
                                                        '        With C1CV_ImplantDevice
                                                        '            .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                                        '            .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                                        '            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                                        '        End With
                                                        '    End If
                                                        'End If
                                                        'If Not IsNothing(dtProductInfo.Rows(nProductInfo)("sProductSerialNo").ToString()) Then
                                                        '    If (dtProductInfo.Rows(nProductInfo)("sProductSerialNo").ToString() <> "") Then
                                                        '        C1CV_ImplantDevice.Rows.Add()
                                                        '        _Row = C1CV_ImplantDevice.Rows.Count - 1
                                                        '        With C1CV_ImplantDevice.Rows(_Row)
                                                        '            .AllowEditing = True
                                                        '            .ImageAndText = True
                                                        '            .Height = 24
                                                        '            .IsNode = True
                                                        '            .Node.Level = 3
                                                        '            .Node.Data = "Product Serial No" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("sProductSerialNo").ToString()
                                                        '            .Node.Image = ImageList1.Images(3)
                                                        '        End With
                                                        '        With C1CV_ImplantDevice
                                                        '            .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                                        '            .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                                        '            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                                        '        End With
                                                        '    End If
                                                        'End If
                                                        'If Not IsNothing(dtProductInfo.Rows(nProductInfo)("sManufacturerModelNo").ToString()) Then
                                                        '    If (dtProductInfo.Rows(nProductInfo)("sManufacturerModelNo").ToString() <> "") Then
                                                        '        C1CV_ImplantDevice.Rows.Add()
                                                        '        _Row = C1CV_ImplantDevice.Rows.Count - 1
                                                        '        With C1CV_ImplantDevice.Rows(_Row)
                                                        '            .AllowEditing = True
                                                        '            .ImageAndText = True
                                                        '            .Height = 24
                                                        '            .IsNode = True
                                                        '            .Node.Level = 3
                                                        '            .Node.Data = "Manufacturer Model No" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("sManufacturerModelNo").ToString()
                                                        '            .Node.Image = ImageList1.Images(3)
                                                        '        End With
                                                        '        With C1CV_ImplantDevice
                                                        '            .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                                        '            .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                                        '            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                                        '        End With
                                                        '    End If
                                                        'End If

                                                        ''''''''''''''''''''''''''''''''''''''''''''
                                                        'If Not IsNothing(dtProductInfo.Rows(nProductInfo)("sLeadType").ToString()) Then
                                                        '    If (dtProductInfo.Rows(nProductInfo)("sLeadType").ToString() <> "") Then
                                                        '        C1CV_ImplantDevice.Rows.Add()
                                                        '        _Row = C1CV_ImplantDevice.Rows.Count - 1
                                                        '        With C1CV_ImplantDevice.Rows(_Row)
                                                        '            .AllowEditing = True
                                                        '            .ImageAndText = True
                                                        '            .Height = 24
                                                        '            .IsNode = True
                                                        '            .Node.Level = 3
                                                        '            .Node.Data = "Lead Type" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("sLeadType").ToString()
                                                        '            .Node.Image = ImageList1.Images(3)
                                                        '        End With
                                                        '        With C1CV_ImplantDevice
                                                        '            .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                                        '            .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                                        '            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                                        '        End With
                                                        '    End If
                                                        'End If

                                                        'If Not IsNothing(dtProductInfo.Rows(nProductInfo)("dtDateRemoved").ToString()) Then
                                                        '    If (dtProductInfo.Rows(nProductInfo)("dtDateRemoved").ToString() <> "") Then
                                                        '        C1CV_ImplantDevice.Rows.Add()
                                                        '        _Row = C1CV_ImplantDevice.Rows.Count - 1
                                                        '        With C1CV_ImplantDevice.Rows(_Row)
                                                        '            .AllowEditing = True
                                                        '            .ImageAndText = True
                                                        '            .Height = 24
                                                        '            .IsNode = True
                                                        '            .Node.Level = 3
                                                        '            .Node.Data = "Date Removed" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("dtDateRemoved").ToString()
                                                        '            .Node.Image = ImageList1.Images(3)
                                                        '        End With
                                                        '        With C1CV_ImplantDevice
                                                        '            .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                                        '            .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                                        '            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                                        '        End With
                                                        '    End If
                                                        'End If

                                                        'If Not IsNothing(dtProductInfo.Rows(nProductInfo)("sPhysicalLocation").ToString()) Then
                                                        '    If (dtProductInfo.Rows(nProductInfo)("sPhysicalLocation").ToString() <> "") Then
                                                        '        C1CV_ImplantDevice.Rows.Add()
                                                        '        _Row = C1CV_ImplantDevice.Rows.Count - 1
                                                        '        With C1CV_ImplantDevice.Rows(_Row)
                                                        '            .AllowEditing = True
                                                        '            .ImageAndText = True
                                                        '            .Height = 24
                                                        '            .IsNode = True
                                                        '            .Node.Level = 3
                                                        '            .Node.Data = "Physical Location" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("sPhysicalLocation").ToString()
                                                        '            .Node.Image = ImageList1.Images(3)
                                                        '        End With
                                                        '        With C1CV_ImplantDevice
                                                        '            .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                                        '            .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                                        '            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                                        '        End With
                                                        '    End If
                                                        'End If

                                                        'If Not IsNothing(dtProductInfo.Rows(nProductInfo)("sLeadLocation").ToString()) Then
                                                        '    If (dtProductInfo.Rows(nProductInfo)("sLeadLocation").ToString() <> "") Then
                                                        '        C1CV_ImplantDevice.Rows.Add()
                                                        '        _Row = C1CV_ImplantDevice.Rows.Count - 1
                                                        '        With C1CV_ImplantDevice.Rows(_Row)
                                                        '            .AllowEditing = True
                                                        '            .ImageAndText = True
                                                        '            .Height = 24
                                                        '            .IsNode = True
                                                        '            .Node.Level = 3
                                                        '            .Node.Data = "Lead Location" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("sLeadLocation").ToString()
                                                        '            .Node.Image = ImageList1.Images(3)
                                                        '        End With
                                                        '        With C1CV_ImplantDevice
                                                        '            .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                                        '            .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                                        '            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                                        '        End With
                                                        '    End If
                                                        'End If

                                                        'If Not IsNothing(dtProductInfo.Rows(nProductInfo)("sThresholdAtrial").ToString()) Then
                                                        '    If (dtProductInfo.Rows(nProductInfo)("sThresholdAtrial").ToString() <> "") Then
                                                        '        C1CV_ImplantDevice.Rows.Add()
                                                        '        _Row = C1CV_ImplantDevice.Rows.Count - 1
                                                        '        With C1CV_ImplantDevice.Rows(_Row)
                                                        '            .AllowEditing = True
                                                        '            .ImageAndText = True
                                                        '            .Height = 24
                                                        '            .IsNode = True
                                                        '            .Node.Level = 3
                                                        '            .Node.Data = "Threshold Atrial" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("sThresholdAtrial").ToString()
                                                        '            .Node.Image = ImageList1.Images(3)
                                                        '        End With
                                                        '        With C1CV_ImplantDevice
                                                        '            .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                                        '            .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                                        '            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                                        '        End With
                                                        '    End If
                                                        'End If

                                                        'If Not IsNothing(dtProductInfo.Rows(nProductInfo)("sThresholdVentricular").ToString()) Then
                                                        '    If (dtProductInfo.Rows(nProductInfo)("sThresholdVentricular").ToString() <> "") Then
                                                        '        C1CV_ImplantDevice.Rows.Add()
                                                        '        _Row = C1CV_ImplantDevice.Rows.Count - 1
                                                        '        With C1CV_ImplantDevice.Rows(_Row)
                                                        '            .AllowEditing = True
                                                        '            .ImageAndText = True
                                                        '            .Height = 24
                                                        '            .IsNode = True
                                                        '            .Node.Level = 3
                                                        '            .Node.Data = "Threshold Ventricular" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("sThresholdVentricular").ToString()
                                                        '            .Node.Image = ImageList1.Images(3)
                                                        '        End With
                                                        '        With C1CV_ImplantDevice
                                                        '            .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                                        '            .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                                        '            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                                        '        End With
                                                        '    End If
                                                        'End If

                                                        'If Not IsNothing(dtProductInfo.Rows(nProductInfo)("sSensingAtrial").ToString()) Then
                                                        '    If (dtProductInfo.Rows(nProductInfo)("sSensingAtrial").ToString() <> "") Then
                                                        '        C1CV_ImplantDevice.Rows.Add()
                                                        '        _Row = C1CV_ImplantDevice.Rows.Count - 1
                                                        '        With C1CV_ImplantDevice.Rows(_Row)
                                                        '            .AllowEditing = True
                                                        '            .ImageAndText = True
                                                        '            .Height = 24
                                                        '            .IsNode = True
                                                        '            .Node.Level = 3
                                                        '            .Node.Data = "Sensing Atrial" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("sSensingAtrial").ToString()
                                                        '            .Node.Image = ImageList1.Images(3)
                                                        '        End With
                                                        '        With C1CV_ImplantDevice
                                                        '            .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                                        '            .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                                        '            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                                        '        End With
                                                        '    End If
                                                        'End If

                                                        'If Not IsNothing(dtProductInfo.Rows(nProductInfo)("sSensingVentricular").ToString()) Then
                                                        '    If (dtProductInfo.Rows(nProductInfo)("sSensingVentricular").ToString() <> "") Then
                                                        '        C1CV_ImplantDevice.Rows.Add()
                                                        '        _Row = C1CV_ImplantDevice.Rows.Count - 1
                                                        '        With C1CV_ImplantDevice.Rows(_Row)
                                                        '            .AllowEditing = True
                                                        '            .ImageAndText = True
                                                        '            .Height = 24
                                                        '            .IsNode = True
                                                        '            .Node.Level = 3
                                                        '            .Node.Data = "Sensing Ventricular" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("sSensingVentricular").ToString()
                                                        '            .Node.Image = ImageList1.Images(3)
                                                        '        End With
                                                        '        With C1CV_ImplantDevice
                                                        '            .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                                        '            .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                                        '            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                                        '        End With
                                                        '    End If
                                                        'End If

                                                        'If Not IsNothing(dtProductInfo.Rows(nProductInfo)("sImpedenceAtrial").ToString()) Then
                                                        '    If (dtProductInfo.Rows(nProductInfo)("sImpedenceAtrial").ToString() <> "") Then
                                                        '        C1CV_ImplantDevice.Rows.Add()
                                                        '        _Row = C1CV_ImplantDevice.Rows.Count - 1
                                                        '        With C1CV_ImplantDevice.Rows(_Row)
                                                        '            .AllowEditing = True
                                                        '            .ImageAndText = True
                                                        '            .Height = 24
                                                        '            .IsNode = True
                                                        '            .Node.Level = 3
                                                        '            .Node.Data = "Impedence Atrial" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("sImpedenceAtrial").ToString()
                                                        '            .Node.Image = ImageList1.Images(3)
                                                        '        End With
                                                        '        With C1CV_ImplantDevice
                                                        '            .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                                        '            .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                                        '            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                                        '        End With
                                                        '    End If
                                                        'End If

                                                        'If Not IsNothing(dtProductInfo.Rows(nProductInfo)("sImpedenceVentricular").ToString()) Then
                                                        '    If (dtProductInfo.Rows(nProductInfo)("sImpedenceVentricular").ToString() <> "") Then
                                                        '        C1CV_ImplantDevice.Rows.Add()
                                                        '        _Row = C1CV_ImplantDevice.Rows.Count - 1
                                                        '        With C1CV_ImplantDevice.Rows(_Row)
                                                        '            .AllowEditing = True
                                                        '            .ImageAndText = True
                                                        '            .Height = 24
                                                        '            .IsNode = True
                                                        '            .Node.Level = 3
                                                        '            .Node.Data = "Impedence Ventricular" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("sImpedenceVentricular").ToString()
                                                        '            .Node.Image = ImageList1.Images(3)
                                                        '        End With
                                                        '        With C1CV_ImplantDevice
                                                        '            .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
                                                        '            .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
                                                        '            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("DateOfImplant")).ToShortDateString)
                                                        '        End With
                                                        '    End If
                                                        'End If
                                                        '''''''''''''''''''''''''''''''''''''''''''
                                                    Next   ''For nProductInfo = 0 To dtProductInfo.Rows.Count - 1
                                                End If
                                            End With
                                        End If
                                    Next    ''For nDeviceType = 0 To dtDeviceType.Rows.Count - 1
                                End If   ''If IsNothing(dtDeviceType) = False Then
                            End With





                            '' ''Query for selecting Lead Type
                            ''strLeadTypeQry = "SELECT DISTINCT sLeadType as LeadType from CV_CardiologyDevice where nPatientID=" & nPatientID & " AND dtDateofImplant='" & dtImplantDate & "' AND sLeadType<>'' "
                            ''oDB.Connect(GetConnectionString)
                            ''dtLeadType = oDB.ReadQueryDataTable(strLeadTypeQry)
                            ''oDB.Disconnect()


                            ''With dtLeadType
                            ''    If IsNothing(dtLeadType) = False Then
                            ''        If dtLeadType.Rows.Count > 0 Then
                            ''            C1CV_ImplantDevice.Rows.Add()
                            ''            _Row = C1CV_ImplantDevice.Rows.Count - 1
                            ''            With C1CV_ImplantDevice.Rows(_Row)
                            ''                .AllowEditing = False
                            ''                .ImageAndText = True
                            ''                .Height = 24
                            ''                .IsNode = True
                            ''                .Node.Level = 1
                            ''                .Node.Data = "Lead Type"
                            ''                '.Node.Image = ImageList1.Images(6)
                            ''            End With
                            ''        End If
                            ''        For nLeadType = 0 To dtLeadType.Rows.Count - 1
                            ''            Dim strLeadType As String = dtLeadType.Rows(nLeadType)("LeadType").ToString()
                            ''            If strLeadType.Trim <> "" Then
                            ''                C1CV_ImplantDevice.Rows.Add()
                            ''                _Row = C1CV_ImplantDevice.Rows.Count - 1
                            ''                With C1CV_ImplantDevice.Rows(_Row)
                            ''                    .AllowEditing = True
                            ''                    .ImageAndText = True
                            ''                    .Height = 24
                            ''                    .IsNode = True
                            ''                    .Node.Level = 2
                            ''                    .TextAlign = TextAlignEnum.LeftCenter
                            ''                    .Node.Data = strLeadType
                            ''                    '.Node.Image = ImageList1.Images(3)
                            ''                End With

                            ''            End If
                            ''        Next
                            ''    End If
                            ''End With



                            '' ''Query for selecting Date Removed
                            ''strDateRemovedQry = "SELECT DISTINCT dtDateRemoved as DateRemoved from CV_CardiologyDevice where nPatientID=" & nPatientID & " AND dtDateofImplant='" & dtImplantDate & "' AND dtDateRemoved<>'' "
                            ''oDB.Connect(GetConnectionString)
                            ''dtDateRemoved = oDB.ReadQueryDataTable(strDateRemovedQry)
                            ''oDB.Disconnect()


                            ''With dtDateRemoved
                            ''    If IsNothing(dtDateRemoved) = False Then
                            ''        If dtDateRemoved.Rows.Count > 0 Then
                            ''            C1CV_ImplantDevice.Rows.Add()
                            ''            _Row = C1CV_ImplantDevice.Rows.Count - 1
                            ''            With C1CV_ImplantDevice.Rows(_Row)
                            ''                .AllowEditing = False
                            ''                .ImageAndText = True
                            ''                .Height = 24
                            ''                .IsNode = True
                            ''                .Node.Level = 1
                            ''                .Node.Data = "Date Removed"
                            ''                '.Node.Image = ImageList1.Images(6)
                            ''            End With
                            ''        End If
                            ''        For nDateRemoved = 0 To dtDateRemoved.Rows.Count - 1
                            ''            Dim strDateRemoved As String = dtDateRemoved.Rows(nDateRemoved)("DateRemoved").ToString()
                            ''            If strDateRemoved.Trim <> "" Then
                            ''                C1CV_ImplantDevice.Rows.Add()
                            ''                _Row = C1CV_ImplantDevice.Rows.Count - 1
                            ''                With C1CV_ImplantDevice.Rows(_Row)
                            ''                    .AllowEditing = True
                            ''                    .ImageAndText = True
                            ''                    .Height = 24
                            ''                    .IsNode = True
                            ''                    .Node.Level = 2
                            ''                    .TextAlign = TextAlignEnum.LeftCenter
                            ''                    .Node.Data = strDateRemoved
                            ''                    '.Node.Image = ImageList1.Images(3)
                            ''                End With

                            ''            End If
                            ''        Next
                            ''    End If
                            ''End With


                            '' ''Query for selecting Physical Location
                            ''strPhysicalLocationQry = "SELECT DISTINCT sPhysicalLocation as PhysicalLocation from CV_CardiologyDevice where nPatientID=" & nPatientID & " AND dtDateofImplant='" & dtImplantDate & "' AND sPhysicalLocation<>'' "
                            ''oDB.Connect(GetConnectionString)
                            ''dtPhysicalLocation = oDB.ReadQueryDataTable(strPhysicalLocationQry)
                            ''oDB.Disconnect()


                            ''With dtPhysicalLocation
                            ''    If IsNothing(dtPhysicalLocation) = False Then
                            ''        If dtPhysicalLocation.Rows.Count > 0 Then
                            ''            C1CV_ImplantDevice.Rows.Add()
                            ''            _Row = C1CV_ImplantDevice.Rows.Count - 1
                            ''            With C1CV_ImplantDevice.Rows(_Row)
                            ''                .AllowEditing = False
                            ''                .ImageAndText = True
                            ''                .Height = 24
                            ''                .IsNode = True
                            ''                .Node.Level = 1
                            ''                .Node.Data = "Physical Location"
                            ''                '.Node.Image = ImageList1.Images(6)
                            ''            End With
                            ''        End If
                            ''        For nPhysicalLocation = 0 To dtPhysicalLocation.Rows.Count - 1
                            ''            Dim strPhysicalLocation As String = dtPhysicalLocation.Rows(nPhysicalLocation)("PhysicalLocation").ToString()
                            ''            If strPhysicalLocation.Trim <> "" Then
                            ''                C1CV_ImplantDevice.Rows.Add()
                            ''                _Row = C1CV_ImplantDevice.Rows.Count - 1
                            ''                With C1CV_ImplantDevice.Rows(_Row)
                            ''                    .AllowEditing = True
                            ''                    .ImageAndText = True
                            ''                    .Height = 24
                            ''                    .IsNode = True
                            ''                    .Node.Level = 2
                            ''                    .TextAlign = TextAlignEnum.LeftCenter
                            ''                    .Node.Data = strPhysicalLocation
                            ''                    '.Node.Image = ImageList1.Images(3)
                            ''                End With

                            ''            End If
                            ''        Next
                            ''    End If
                            ''End With



                            '' ''Query for selecting Lead Location
                            ''strLeadLocationQry = "SELECT DISTINCT sLeadLocation as LeadLocation from CV_CardiologyDevice where nPatientID=" & nPatientID & " AND dtDateofImplant='" & dtImplantDate & "' AND sLeadLocation<>''"
                            ''oDB.Connect(GetConnectionString)
                            ''dtLeadLocation = oDB.ReadQueryDataTable(strLeadLocationQry)
                            ''oDB.Disconnect()


                            ''With dtLeadLocation
                            ''    If IsNothing(dtLeadLocation) = False Then
                            ''        If dtLeadLocation.Rows.Count > 0 Then
                            ''            C1CV_ImplantDevice.Rows.Add()
                            ''            _Row = C1CV_ImplantDevice.Rows.Count - 1
                            ''            With C1CV_ImplantDevice.Rows(_Row)
                            ''                .AllowEditing = False
                            ''                .ImageAndText = True
                            ''                .Height = 24
                            ''                .IsNode = True
                            ''                .Node.Level = 1
                            ''                .Node.Data = "Lead Location"
                            ''                '.Node.Image = ImageList1.Images(6)
                            ''            End With
                            ''        End If
                            ''        For nLeadLocation = 0 To dtLeadLocation.Rows.Count - 1
                            ''            Dim strLeadLocation As String = dtLeadLocation.Rows(nLeadLocation)("LeadLocation").ToString()
                            ''            If strLeadLocation.Trim <> "" Then
                            ''                C1CV_ImplantDevice.Rows.Add()
                            ''                _Row = C1CV_ImplantDevice.Rows.Count - 1
                            ''                With C1CV_ImplantDevice.Rows(_Row)
                            ''                    .AllowEditing = True
                            ''                    .ImageAndText = True
                            ''                    .Height = 24
                            ''                    .IsNode = True
                            ''                    .Node.Level = 2
                            ''                    .TextAlign = TextAlignEnum.LeftCenter
                            ''                    .Node.Data = strLeadLocation
                            ''                    '.Node.Image = ImageList1.Images(3)
                            ''                End With

                            ''            End If
                            ''        Next
                            ''    End If
                            ''End With



                            '' ''Query for selecting Pacing Threshold
                            ''strPacingThresholdQry = "SELECT DISTINCT sThresholdAtrial,SThresholdVentricular from CV_CardiologyDevice where nPatientID=" & nPatientID & " AND dtDateofImplant='" & dtImplantDate & "' "
                            ''oDB.Connect(GetConnectionString)
                            ''dtPacingThreshold = oDB.ReadQueryDataTable(strPacingThresholdQry)
                            ''oDB.Disconnect()


                            ''With dtPacingThreshold
                            ''    If IsNothing(dtPacingThreshold) = False Then
                            ''        If dtPacingThreshold.Rows.Count > 0 Then
                            ''            C1CV_ImplantDevice.Rows.Add()
                            ''            _Row = C1CV_ImplantDevice.Rows.Count - 1
                            ''            With C1CV_ImplantDevice.Rows(_Row)
                            ''                .AllowEditing = False
                            ''                .ImageAndText = True
                            ''                .Height = 24
                            ''                .IsNode = True
                            ''                .Node.Level = 1
                            ''                .Node.Data = "Pacing Threshold"
                            ''                '.Node.Image = ImageList1.Images(6)
                            ''            End With
                            ''        End If
                            ''        For nPacingThreshold = 0 To dtPacingThreshold.Rows.Count - 1
                            ''            strCombine = ""
                            ''            If Not IsNothing(dtPacingThreshold.Rows(nPacingThreshold)("sThresholdAtrial").ToString()) Then
                            ''                If (dtPacingThreshold.Rows(nPacingThreshold)("sThresholdAtrial").ToString() <> "") Then
                            ''                    C1CV_ImplantDevice.Rows.Add()
                            ''                    _Row = C1CV_ImplantDevice.Rows.Count - 1

                            ''                    With C1CV_ImplantDevice.Rows(_Row)
                            ''                        .AllowEditing = True
                            ''                        .ImageAndText = True
                            ''                        .Height = 24
                            ''                        .IsNode = True
                            ''                        .Node.Level = 2
                            ''                        .Node.Data = "Threshold Atrial" + " " + ":" + " " + dtPacingThreshold.Rows(nPacingThreshold)("sThresholdAtrial").ToString()
                            ''                        '.Node.Image = ImageList1.Images(5)
                            ''                    End With
                            ''                End If
                            ''            End If
                            ''            If Not IsNothing(dtPacingThreshold.Rows(nPacingThreshold)("SThresholdVentricular").ToString()) Then
                            ''                If (dtPacingThreshold.Rows(nPacingThreshold)("SThresholdVentricular").ToString() <> "") Then
                            ''                    C1CV_ImplantDevice.Rows.Add()
                            ''                    _Row = C1CV_ImplantDevice.Rows.Count - 1

                            ''                    With C1CV_ImplantDevice.Rows(_Row)
                            ''                        .AllowEditing = True
                            ''                        .ImageAndText = True
                            ''                        .Height = 24
                            ''                        .IsNode = True
                            ''                        .Node.Level = 2
                            ''                        .Node.Data = "Threshold Ventricular" + " " + ":" + " " + dtPacingThreshold.Rows(nPacingThreshold)("SThresholdVentricular").ToString()
                            ''                        '.Node.Image = ImageList1.Images(5)
                            ''                    End With
                            ''                End If
                            ''            End If

                            ''        Next
                            ''    End If
                            ''End With



                            '' ''Query for selecting Sensing
                            ''strSensingQry = "SELECT DISTINCT sSensingAtrial,sSensingVentricular from CV_CardiologyDevice where nPatientID=" & nPatientID & " AND dtDateofImplant='" & dtImplantDate & "' "
                            ''oDB.Connect(GetConnectionString)
                            ''dtSensing = oDB.ReadQueryDataTable(strSensingQry)
                            ''oDB.Disconnect()


                            ''With dtSensing
                            ''    If IsNothing(dtSensing) = False Then
                            ''        If dtSensing.Rows.Count > 0 Then
                            ''            C1CV_ImplantDevice.Rows.Add()
                            ''            _Row = C1CV_ImplantDevice.Rows.Count - 1
                            ''            With C1CV_ImplantDevice.Rows(_Row)
                            ''                .AllowEditing = False
                            ''                .ImageAndText = True
                            ''                .Height = 24
                            ''                .IsNode = True
                            ''                .Node.Level = 1
                            ''                .Node.Data = "Sensing"
                            ''                '.Node.Image = ImageList1.Images(6)
                            ''            End With
                            ''        End If
                            ''        For nSensing = 0 To dtSensing.Rows.Count - 1
                            ''            strCombine = ""
                            ''            If Not IsNothing(dtSensing.Rows(nSensing)("sSensingAtrial").ToString()) Then
                            ''                If (dtSensing.Rows(nSensing)("sSensingAtrial").ToString() <> "") Then
                            ''                    C1CV_ImplantDevice.Rows.Add()
                            ''                    _Row = C1CV_ImplantDevice.Rows.Count - 1

                            ''                    With C1CV_ImplantDevice.Rows(_Row)
                            ''                        .AllowEditing = True
                            ''                        .ImageAndText = True
                            ''                        .Height = 24
                            ''                        .IsNode = True
                            ''                        .Node.Level = 2
                            ''                        .Node.Data = "Sensing Atrial" + " " + ":" + " " + dtSensing.Rows(nSensing)("sSensingAtrial").ToString()
                            ''                        '.Node.Image = ImageList1.Images(5)
                            ''                    End With
                            ''                End If
                            ''            End If
                            ''            If Not IsNothing(dtSensing.Rows(nSensing)("sSensingVentricular").ToString()) Then
                            ''                If (dtSensing.Rows(nSensing)("sSensingVentricular").ToString() <> "") Then
                            ''                    C1CV_ImplantDevice.Rows.Add()
                            ''                    _Row = C1CV_ImplantDevice.Rows.Count - 1

                            ''                    With C1CV_ImplantDevice.Rows(_Row)
                            ''                        .AllowEditing = True
                            ''                        .ImageAndText = True
                            ''                        .Height = 24
                            ''                        .IsNode = True
                            ''                        .Node.Level = 2
                            ''                        .Node.Data = "Sensing Ventricular" + " " + ":" + " " + dtSensing.Rows(nSensing)("sSensingVentricular").ToString()
                            ''                        '.Node.Image = ImageList1.Images(5)
                            ''                    End With
                            ''                End If
                            ''            End If
                            ''        Next
                            ''    End If
                            ''End With



                            '' ''Query for selecting Impedance
                            ''strImpedanceQry = "SELECT DISTINCT sImpedenceAtrial,sImpedenceVentricular from CV_CardiologyDevice where nPatientID=" & nPatientID & " AND dtDateofImplant='" & dtImplantDate & "'"
                            ''oDB.Connect(GetConnectionString)
                            ''dtImpedance = oDB.ReadQueryDataTable(strImpedanceQry)
                            ''oDB.Disconnect()


                            ''With dtImpedance
                            ''    If IsNothing(dtImpedance) = False Then
                            ''        If dtImpedance.Rows.Count > 0 Then
                            ''            C1CV_ImplantDevice.Rows.Add()
                            ''            _Row = C1CV_ImplantDevice.Rows.Count - 1
                            ''            With C1CV_ImplantDevice.Rows(_Row)
                            ''                .AllowEditing = False
                            ''                .ImageAndText = True
                            ''                .Height = 24
                            ''                .IsNode = True
                            ''                .Node.Level = 1
                            ''                .Node.Data = "Impedence"
                            ''                '.Node.Image = ImageList1.Images(6)
                            ''            End With
                            ''        End If
                            ''        For nImpedance = 0 To dtImpedance.Rows.Count - 1
                            ''            strCombine = ""
                            ''            If Not IsNothing(dtImpedance.Rows(nImpedance)("sImpedenceAtrial").ToString()) Then
                            ''                If (dtImpedance.Rows(nImpedance)("sImpedenceAtrial").ToString() <> "") Then
                            ''                    C1CV_ImplantDevice.Rows.Add()
                            ''                    _Row = C1CV_ImplantDevice.Rows.Count - 1
                            ''                    With C1CV_ImplantDevice.Rows(_Row)
                            ''                        .AllowEditing = True
                            ''                        .ImageAndText = True
                            ''                        .Height = 24
                            ''                        .IsNode = True
                            ''                        .Node.Level = 2
                            ''                        .Node.Data = "Impedence Atrial" + " " + ":" + " " + dtImpedance.Rows(nImpedance)("sImpedenceAtrial").ToString()
                            ''                        '.Node.Image = ImageList1.Images(5)
                            ''                    End With
                            ''                End If
                            ''            End If
                            ''            If Not IsNothing(dtImpedance.Rows(nImpedance)("sImpedenceVentricular").ToString()) Then
                            ''                If (dtImpedance.Rows(nImpedance)("sImpedenceVentricular").ToString() <> "") Then
                            ''                    C1CV_ImplantDevice.Rows.Add()
                            ''                    _Row = C1CV_ImplantDevice.Rows.Count - 1
                            ''                    With C1CV_ImplantDevice.Rows(_Row)
                            ''                        .AllowEditing = True
                            ''                        .ImageAndText = True
                            ''                        .Height = 24
                            ''                        .IsNode = True
                            ''                        .Node.Level = 2
                            ''                        .Node.Data = "Impedence Ventricular" + " " + ":" + " " + dtImpedance.Rows(nImpedance)("sImpedenceVentricular").ToString()
                            ''                        '.Node.Image = ImageList1.Images(5)
                            ''                    End With
                            ''                End If
                            ''            End If
                            ''        Next
                            ''    End If
                            ''End With


                        End If     '' CStr(dtDateofImplant.Rows(nDOS)("DateofStudy")).Trim <> "" Then
                    Next   ''For nDOS = 0 To dtDateofImplant.Rows.Count - 1
                End If   ''If IsNothing(dtDateofImplant) = False Then
            End With   '' With dtDateofImplant



            dtDateofImplant = Nothing

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        'Use to clear search text box
        txtsearch.ResetText()
        txtsearch.Focus()
    End Sub


    Private Sub txtsearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtsearch.TextChanged
        Try

            Dim strSearch As String
            With txtsearch
                If Trim(.Text) <> "" Then
                    strSearch = Replace(.Text, "'", "''")
                Else
                    strSearch = ""
                End If
            End With

            With C1CV_ImplantDevice

                If strSearch.Trim <> "" And strSearch.Trim.Length = 1 Then
                    ''''''''''''
                    Dim objComm As New Cls_CardioVasculars
                    objComm.ExpandAll(C1CV_ImplantDevice)
                    objComm = Nothing
                    ''''''''''''
                End If


                .Row = .FindRow(strSearch, 1, COL_DateofImplant, False, False, True)
                If .Row > 0 Then
                    Exit Sub
                End If

                '' InString Search 
                Dim strNode As String = ""
                For i As Int16 = 1 To .Rows.Count - 1
                    strNode = ""
                    strNode = UCase(.GetData(i, COL_DateofImplant).ToString.Trim)
                    If InStr(strNode, UCase(strSearch.Trim), CompareMethod.Text) > 0 Then
                        .Row = i
                        Exit Sub
                    End If
                Next
            End With


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.Search, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ts_ViewButtons_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        Try
            Select Case e.ClickedItem.Tag

                Case "Add"
                    nPatientID = nPatientID
                    nExamID = 0
                    'Check for visit aganist current date,if visit not available pass 0
                    nVisitID = GetVisitID(Now.Date, nPatientID)
                    nClinicID = gnClinicID
                    nDateofImplant = Now.Date
                    SetGridStyle()
                    FillImplantDevice()
                    Dim ofrmImplant As New frmCV_ImplantDevice(nPatientID, nVisitID, nDateofImplant)
                    ofrmImplant.blnIsNew = True
                    ofrmImplant.ShowDialog(IIf(IsNothing(ofrmImplant.Parent), Me, ofrmImplant.Parent))
                    SetGridStyle()
                    FillImplantDevice()
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.View, "Record viewed for Cardio Vascular Implant Device", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20100916
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.View, "Record viewed for Cardio Vascular Implant Device", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                    ''
                    ofrmImplant.Dispose()
                    ofrmImplant = Nothing
                Case "Modify"
                    If C1CV_ImplantDevice.Row > 0 Then
                        nPatientID = C1CV_ImplantDevice.GetData(C1CV_ImplantDevice.Row, COL_PatientID)
                        nCardiologyDeviceID = C1CV_ImplantDevice.GetData(C1CV_ImplantDevice.Row, COL_CardiologyDeviceID)
                        nExamID = C1CV_ImplantDevice.GetData(C1CV_ImplantDevice.Row, COL_ExamID)
                        nVisitID = C1CV_ImplantDevice.GetData(C1CV_ImplantDevice.Row, COL_VisitID)
                        nClinicID = C1CV_ImplantDevice.GetData(C1CV_ImplantDevice.Row, COL_ClinicID)
                        nDateofImplant = C1CV_ImplantDevice.GetData(C1CV_ImplantDevice.Row, COL_DateofStudyInvisible)

                        SetGridStyle()
                        FillImplantDevice()
                        Dim ofrmImplant As New frmCV_ImplantDevice(nPatientID, nVisitID, nDateofImplant)
                        ofrmImplant.ShowDialog(IIf(IsNothing(ofrmImplant.Parent), Me, ofrmImplant.Parent))
                        SetGridStyle()
                        FillImplantDevice()
                        ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.Add, "Implant Device modified.  . ", gloAuditTrail.ActivityOutCome.Success)
                        ''Added Rahul P on 20100916
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.Modify, "Implant Device modified. ", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                        ''
                        ofrmImplant.Dispose()
                        ofrmImplant = Nothing
                    End If

                Case "Delete"
                    If C1CV_ImplantDevice.Row > 0 Then
                        nPatientID = C1CV_ImplantDevice.GetData(C1CV_ImplantDevice.Row, COL_PatientID)
                        nVisitID = C1CV_ImplantDevice.GetData(C1CV_ImplantDevice.Row, COL_VisitID)
                        nClinicID = C1CV_ImplantDevice.GetData(C1CV_ImplantDevice.Row, COL_ClinicID)

                        nDateofImplant = C1CV_ImplantDevice.GetData(C1CV_ImplantDevice.Row, COL_DateofStudyInvisible)
                        If MessageBox.Show("Are you sure you want to delete the Implant Device?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            Dim objEjectionDBLayer As New ClsEjectionFractionDBLayer
                            objEjectionDBLayer.DeleteCardiology(nPatientID, nVisitID, nDateofImplant)
                            txtsearch.Text = ""
                            SetGridStyle()
                            FillImplantDevice()
                        End If

                    End If

                Case "Refresh"
                    SetGridStyle()
                    FillImplantDevice()

                Case "Close"
                    Me.Close()

            End Select

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub C1CV_ImplantDevice_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1CV_ImplantDevice.MouseDoubleClick
        Try
            If C1CV_ImplantDevice.Row > 0 Then
                nPatientID = C1CV_ImplantDevice.GetData(C1CV_ImplantDevice.Row, COL_PatientID)
                nVisitID = C1CV_ImplantDevice.GetData(C1CV_ImplantDevice.Row, COL_VisitID)
                nDateofImplant = C1CV_ImplantDevice.GetData(C1CV_ImplantDevice.Row, COL_DateofStudyInvisible)

                SetGridStyle()
                FillImplantDevice()
                Dim ofrmImplant As New frmCV_ImplantDevice(nPatientID, nVisitID, nDateofImplant)
                ofrmImplant.ShowDialog(IIf(IsNothing(ofrmImplant.Parent), Me, ofrmImplant.Parent))
                SetGridStyle()
                FillImplantDevice()
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.Add, "Implant Device modified.  . ", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20100916
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ImplantDevices, gloAuditTrail.ActivityType.Modify, "Implant Device modified. ", nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                ''
                ofrmImplant.Dispose()
                ofrmImplant = Nothing
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub C1CV_ImplantDevice_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1CV_ImplantDevice.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return nPatientID  'Curent patient variable(Local variable) for this module 
        End Get
    End Property

End Class