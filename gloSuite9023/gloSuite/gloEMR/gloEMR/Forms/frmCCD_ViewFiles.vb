
Imports gloUserControlLibrary
Imports System.Data.SqlClient
Imports C1.Win.C1FlexGrid
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports System.IO 'Added by kanchan on 20101020
Imports System.Xml 'Added by kanchan on 20101020
Imports System.Xml.Xsl 'Added by kanchan on 20101020
Imports gloCCDLibrary




Public Class frmCCD_ViewFiles
    Implements IPatientContext
    ' Dim cl As clsEjectionFraction

    Dim IsImport As Boolean = True  'Added by kanchan on 20101020
    Dim _PatientID As Int64 = 0  'Added by kanchan on 20101020
    '''' Column No

    'Dim COL_PATIENTID As Integer = 0

    Dim COL_FirstName As Integer = 0
    Dim COL_LastName As Integer = 1
    Dim COL_dtCreatedDate As Integer = 2
    Dim COL_dtDocTimeStamp As Integer = 3
    Dim COL_FileName As Integer = 0
    Dim COL_Source As Integer = 1


    'Code Start-Added by kanchan on 20101020
    Dim COL_CCDId As Integer = 4
    Dim COL_DocType As Integer = 5
    Dim COL_Notes As Integer = 6

    Dim COL_ImportUser As Integer = 6
    Dim COL_Status As Integer = 7

    Dim COL_HASHVALUE As Integer = 7
    Dim COL_HASHALGO As Integer = 8
    Dim COL_PatientID As Integer = 9

    'Code End-Added by kanchan on 20101020
    Dim COLUMN_COUNT As Int16 = 11 'Count changed from '4' by kanchan on 20101020

    Dim COL_sStatus As Integer = 10

    Dim Col_CDAErrorId As Integer = 0
    Dim Col_ErrorFileName As Integer = 1
    'Dim Col_CDAErrorDesc As Integer = 1
    Dim Col_Errorimportuser As Integer = 3
    Dim COl_ErrorImportDate As Integer = 2


    Private WithEvents ogloUC_generalsearch As gloUserControlLibrary.gloUCGeneralSearch = Nothing
    Private WithEvents gloUC_PatientStrip1 As gloUserControlLibrary.gloUC_PatientStrip = Nothing
    'Added by kanchan on 20101020 for View Selected Patient CCD
    Public Property SelectedPatientID() As Int64
        Get
            Return _PatientID
        End Get
        Set(ByVal value As Int64)
            _PatientID = value
        End Set
    End Property
    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return _PatientID    'Curent patient variable(Local variable) for this module 
        End Get
    End Property

    Public Sub New()

        MyBase.New()
        ''EjectionFractionID is Zero when EjectionFraction List is Open from Ejection Fraction

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    Private Sub frmCCD_ViewFiles_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            If Not IsNothing(Me.ParentForm) Then
                CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewCCD, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub frmCCD_ViewFiles_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If IsNothing(gloUC_PatientStrip1) = False Then
            Me.Controls.Remove(gloUC_PatientStrip1)
            gloUC_PatientStrip1.Dispose()
            gloUC_PatientStrip1 = Nothing
        End If
        If IsNothing(ogloUC_generalsearch) = False Then
            Panel3.Controls.Remove(ogloUC_generalsearch)
            ogloUC_generalsearch.Dispose()
            ogloUC_generalsearch = Nothing
        End If
        If IsNothing(Me) = False Then
            Try
                'Application.DoEvents()
                Me.Dispose()
            Catch exdispose As Exception

            End Try
        End If
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If _PatientID <> 0 Then
            Set_PatientDetailStrip()
            PnlDisplayFileType.Visible = False
        End If

        gloC1FlexStyle.Style(cfgCCD)

        Try

            If IsNothing(ogloUC_generalsearch) = False Then
                Panel3.Controls.Remove(ogloUC_generalsearch)
                ogloUC_generalsearch.Dispose()
                ogloUC_generalsearch = Nothing
            End If
            ogloUC_generalsearch = New gloUCGeneralSearch()

            Panel3.Controls.Add(ogloUC_generalsearch)
            ogloUC_generalsearch.Dock = DockStyle.Left

            ogloUC_generalsearch.BringToFront()

            Dim dt As DataTable = Nothing

            dt = PopulateCCDFiles()
            
            SetGridStyle(dt)

            ogloUC_generalsearch.IntialiseDatatable(dt)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewCCD, gloAuditTrail.ActivityType.View, "Viewed CCD file", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)


            If IsImport = True Then
                tls_SaveAs.Visible = False
            Else
                tls_SaveAs.Visible = True
            End If
            AddValues()
         
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewCCD, gloAuditTrail.ActivityType.View, ex, gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub


    Private Sub Set_PatientDetailStrip()

        If IsNothing(gloUC_PatientStrip1) = False Then
            Me.Controls.Remove(gloUC_PatientStrip1)
            gloUC_PatientStrip1.Dispose()
            gloUC_PatientStrip1 = Nothing
        End If

        gloUC_PatientStrip1 = New gloUserControlLibrary.gloUC_PatientStrip

        With gloUC_PatientStrip1


            .ShowDetail(_PatientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.CCD)

            .DTPEnabled = False
        End With
        '03-Mar-16 Aniket: Resolving Bug #93913: gloEMR->pending clincial information image on dashboard->Banner is not displaying properly
        Me.Controls.Add(gloUC_PatientStrip1)
        gloUC_PatientStrip1.Dock = DockStyle.Top
        gloUC_PatientStrip1.Padding = New Padding(3, 0, 3, 0)
        gloUC_PatientStrip1.BringToFront()

        pnlMain.BringToFront()

    End Sub


    'function for Populating Ejection fraction list

    Private Function PopulateCCDFiles() As DataTable

        'Declaration of variables for making connection
        Dim dt As New DataTable
        Dim sqladpt As SqlDataAdapter = Nothing
        Dim strquery As String

        Try

            If _PatientID <> 0 Then

                If IsImport = True Then
                    strquery = "Select replace(sFileName,'.rtf','') as FileName, sSource As Source,  dtCreatedDate as dtCreatedDate , dtDocTimeStamp as DocumentTimeStamp, isnull(nCCDID,0)as CCDId, Case sFileType WHEN 'CCR' THEN 'Continuity of Care Record' WHEN 'CCD' THEN 'Continuity of Care Document' WHEN 'CDA' THEN 'Clinical Document Architecture'  ELSE '' End as FileType, " _
                                & " sLoginName As ImportUser, Case nStatus When 1 then ''  When 2 Then 'Yes'  end As Status,ISNULL(sHashValue,'') AS HashValue,ISNULL(sHashAlgoType,'') AS HashAlgorithm,ISNULL(nPatientID,0) AS nPatientID   from CCD_Files " _
                                & " LEFT OUTER JOIN uSer_Mst on Isnull(nImportUserID,0) = nUserID   WHERE nPatientID = " & _PatientID & " Order By CCD_Files.dtCreatedDate Desc"

                Else
                    strquery = "Select isnull(sFirstName,'')as FirstName,isnull(sLastName,'')as LastName,dtDocTimeStamp as DocumentTimeStamp," _
                            & " replace(sFileName,'.rtf','') as FileName,isnull(nCCDID,0)as CCDId," _
                            & " Case sFileType WHEN 'CCR' THEN 'Continuity of Care Record' WHEN 'CCD' THEN 'Continuity of Care Document' WHEN 'CDA' THEN 'Clinical Document Architecture' " _
                            & " ELSE '' End as FileType,isnull(sNotes,'') as Notes,ISNULL(sHashValue,'') AS HashValue,ISNULL(sHashAlgoType,'') AS HashAlgorithm,ISNULL(nPatientID,0) AS PatientID,ISNULL(sStatus,'Generated') from CCD_Exported_Files WHERE IsNull(bISBlankCCDA,0) = 0 and nPatientID =" & _PatientID & " Order By dtDocTimeStamp Desc"
                End If
            Else
                tlbbtn_Extract.Visible = False
                tlbbtn_Reconcile.Visible = False
                If IsImport = True Then
                    strquery = "Select replace(sFileName,'.rtf','') as FileName, sSource As Source,  dtCreatedDate as dtCreatedDate  , dtDocTimeStamp as DocumentTimeStamp, isnull(nCCDID,0)as CCDId, Case sFileType WHEN 'CCR' THEN 'Continuity of Care Record' WHEN 'CCD' THEN 'Continuity of Care Document' WHEN 'CDA' THEN 'Clinical Document Architecture'  ELSE '' End as FileType, " _
                                & " sLoginName As ImportUser, Case nStatus When 1 then ''  When 2 Then 'Yes'  end As Status,ISNULL(sHashValue,'') AS HashValue,ISNULL(sHashAlgoType,'') AS HashAlgorithm,ISNULL(nPatientID,0) AS nPatientID  from CCD_Files " _
                                & " LEFT OUTER JOIN User_Mst on Isnull(nImportUserID,0) = nUserID Order By CCD_Files.dtCreatedDate Desc"
                Else
                    strquery = "Select isnull(sFirstName,'')as FirstName,isnull(sLastName,'')as LastName,dtDocTimeStamp as DocumentTimeStamp," _
                            & " replace(sFileName,'.rtf','') as FileName,isnull(nCCDID,0)as CCDId," _
                            & " Case sFileType WHEN 'CCR' THEN 'Continuity of Care Record' WHEN 'CCD' THEN 'Continuity of Care Document' WHEN 'CDA' THEN 'Clinical Document Architecture' " _
                            & " ELSE '' End as FileType,isnull(sNotes,'') as Notes,ISNULL(sHashValue,'') AS HashValue,ISNULL(sHashAlgoType,'') AS HashAlgorithm,ISNULL(nPatientID,0) AS PatientID,ISNULL(sStatus,'Generated') from CCD_Exported_Files Where IsNull(bISBlankCCDA,0) = 0 Order By dtDocTimeStamp Desc"
                End If

            End If




            sqladpt = New SqlDataAdapter(strquery, GetConnectionString)
            
            sqladpt.Fill(dt)

            Return dt

        Catch ex As Exception
            Return Nothing

        Finally
            If Not IsNothing(sqladpt) Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
        End Try

    End Function

    'Function for insert 

    Private Sub SetGridStyle(ByVal dt As DataTable)
        'cfgCCD.Clear()
        cfgCCD.DataSource = Nothing

        With cfgCCD
            .DataSource = dt

            .Dock = DockStyle.Fill
            Dim _TotalWidth As Single = 0
            'Code Added by kanchan on 20101020 to display CCD
            '_TotalWidth = (.Width - 20) / 4
            _TotalWidth = (.Width - 20) / 9
            'Dim cStyle As C1.Win.C1FlexGrid.CellStyle


            ' .Rows.Count = 1
            .AllowEditing = True
            .AllowAddNew = False

            .Styles.ClearUnused()

            '.Cols(COL_PATIENTID).Width = .Width * 0
            '.Cols(COL_PATIENTID).AllowEditing = False
            '.SetData(0, COL_PATIENTID, "PatientID")
            '.Cols(COL_PATIENTID).TextAlignFixed = TextAlignEnum.CenterCenter
            If rbExport.Checked = True Then
                ' .Cols.Count = 10
                .Cols(COL_FirstName).Width = _TotalWidth * 1
                .Cols(COL_FirstName).AllowEditing = False
                '.SetData(0, COL_FirstName, "FirstName")
                .Cols(COL_FirstName).Caption = "First Name"
                .Cols(COL_FirstName).TextAlignFixed = TextAlignEnum.CenterCenter

                .Cols(COL_LastName).Width = _TotalWidth * 1
                .Cols(COL_LastName).AllowEditing = False
                ' .SetData(0, COL_LastName, "LastName")
                .Cols(COL_LastName).TextAlignFixed = TextAlignEnum.CenterCenter
                .Cols(COL_LastName).Caption = "Last Name"

                COL_dtDocTimeStamp = 2
                .Cols(COL_dtDocTimeStamp).Width = _TotalWidth * 1
                .Cols(COL_dtDocTimeStamp).AllowEditing = False
                cfgCCD.Cols(COL_dtDocTimeStamp).DataType = GetType(System.String)
                ' .SetData(0, COL_dtDocTimeStamp, "Date Time Stamp")
                .Cols(COL_dtDocTimeStamp).TextAlignFixed = TextAlignEnum.CenterCenter
                .Cols(COL_dtDocTimeStamp).Caption = "Date Time"


                COL_FileName = 3
                .Cols(COL_FileName).Width = _TotalWidth * 2
                ' .SetData(0, COL_FileName, "Document Name")
                .Cols(COL_FileName).DataType = GetType(Date)
                .Cols(COL_FileName).AllowEditing = False
                .Cols(COL_FileName).Caption = "Document Name"


                'Code Start-Added by kanchan on 20101020 
                COL_CCDId = 4
                .Cols(COL_CCDId).Width = _TotalWidth * 0
                '.SetData(0, COL_CCDId, "CCD ID")
                .Cols(COL_CCDId).DataType = GetType(Int64)
                .Cols(COL_CCDId).AllowEditing = False
                .Cols(COL_CCDId).TextAlignFixed = TextAlignEnum.CenterCenter
                .Cols(COL_CCDId).Caption = "CCD ID"


                'Code Start-Added by kanchan on 20101020 
                COL_DocType = 5
                .Cols(COL_DocType).Width = _TotalWidth * 1.5
                '.SetData(0, COL_DocType, "Document Type")
                .Cols(COL_DocType).AllowEditing = False
                .Cols(COL_DocType).TextAlignFixed = TextAlignEnum.CenterCenter
                .Cols(COL_DocType).Caption = "Document Type"


                'Code Start-Added by kanchan on 20101020
                COL_Notes = 6
                .Cols(COL_Notes).Width = _TotalWidth * 1.5
                '.SetData(0, COL_Notes, "Notes")
                .Cols(COL_Notes).AllowEditing = False
                .Cols(COL_Notes).TextAlignFixed = TextAlignEnum.CenterCenter
                .Cols(COL_Notes).Caption = "Notes"



                'Code Start-Added by kanchan on 20101020 
                COL_HASHVALUE = 7
                .Cols(COL_HASHVALUE).Width = _TotalWidth * 2
                '.SetData(0, COL_HASHVALUE, "Hash Value")
                .Cols(COL_HASHVALUE).AllowEditing = True
                .Cols(COL_HASHVALUE).TextAlignFixed = TextAlignEnum.CenterCenter
                .Cols(COL_HASHVALUE).Caption = "Hash Value"


                'Code Start-Added by kanchan on 20101020 
                COL_HASHALGO = 8
                .Cols(COL_HASHALGO).Width = _TotalWidth * 1
                '.SetData(0, COL_HASHALGO, "Hash Algorithm")
                .Cols(COL_HASHALGO).AllowEditing = True
                .Cols(COL_HASHALGO).Visible = False
                .Cols(COL_HASHALGO).TextAlignFixed = TextAlignEnum.CenterCenter
                .Cols(COL_HASHALGO).Caption = "Hash Algorithm"


                'Code Start-Added by kanchan on 20101020 
                COL_PatientID = 9
                .Cols(COL_PatientID).Width = _TotalWidth * 1
                '.SetData(0, COL_PatientID, "PatientID")
                .Cols(COL_PatientID).AllowEditing = False
                .Cols(COL_PatientID).Visible = False
                .Cols(COL_PatientID).TextAlignFixed = TextAlignEnum.CenterCenter
                .Cols(COL_PatientID).Caption = "PatientID"

                COL_sStatus = 10
                .Cols(COL_sStatus).Width = _TotalWidth * 1
                '.SetData(0, COL_PatientID, "PatientID")
                .Cols(COL_sStatus).AllowEditing = False
                .Cols(COL_sStatus).Visible = True
                .Cols(COL_sStatus).TextAlignFixed = TextAlignEnum.CenterCenter
                .Cols(COL_sStatus).Caption = "Status"


            End If

            If rbImport.Checked = True Then

                ' .Cols.Count = 10
                .Cols(0).Width = 0
                '.SetData(0, COL_FileName, "Document Name")
                .Cols(0).DataType = GetType(Date)
                .Cols(0).AllowEditing = True



                .Cols(COL_Source).Width = 500
                '.SetData(0, COL_Source, "Source")
                .Cols(COL_Source).AllowEditing = False
                .Cols(COL_Source).TextAlignFixed = TextAlignEnum.CenterCenter
                .Cols(COL_Source).Caption = "Source"

                .Cols(COL_dtCreatedDate).Width = 150
                .Cols(COL_dtCreatedDate).AllowEditing = False
                cfgCCD.Cols(COL_dtCreatedDate).DataType = GetType(System.String)
                '.SetData(0, COL_dtCreatedDate, "Created Date")
                .Cols(COL_dtCreatedDate).TextAlignFixed = TextAlignEnum.CenterCenter
                .Cols(COL_dtCreatedDate).Caption = "Created Date"

                COL_dtDocTimeStamp = 3
                .Cols(COL_dtDocTimeStamp).Width = 150
                .Cols(COL_dtDocTimeStamp).AllowEditing = False
                cfgCCD.Cols(COL_dtDocTimeStamp).DataType = GetType(System.String)
                '.SetData(0, COL_dtDocTimeStamp, "Import Date")
                .Cols(COL_dtDocTimeStamp).TextAlignFixed = TextAlignEnum.CenterCenter
                .Cols(COL_dtDocTimeStamp).Caption = "Import Date"

                COL_CCDId = 4
                .Cols(COL_CCDId).Width = 0
                '.SetData(0, COL_CCDId, "CCD ID")
                .Cols(COL_CCDId).DataType = GetType(Int64)
                .Cols(COL_CCDId).AllowEditing = True
                .Cols(COL_CCDId).Visible = False
                .Cols(COL_CCDId).TextAlignFixed = TextAlignEnum.CenterCenter


                'Code Start-Added by kanchan on 20101020 
                COL_DocType = 5
                .Cols(COL_DocType).Width = 250
                '.SetData(0, COL_DocType, "Document Type")
                .Cols(COL_DocType).AllowEditing = False
                .Cols(COL_DocType).TextAlignFixed = TextAlignEnum.CenterCenter
                .Cols(COL_DocType).Caption = "Document Type"


                COL_ImportUser = 6
                .Cols(COL_ImportUser).Width = 80
                '.SetData(0, COL_ImportUser, "Import User")
                .Cols(COL_ImportUser).AllowEditing = False
                .Cols(COL_ImportUser).TextAlignFixed = TextAlignEnum.CenterCenter
                .Cols(COL_ImportUser).Caption = "Import User"



                COL_Status = 7
                .Cols(COL_Status).Width = 100
                '.SetData(0, COL_Status, "Extracted")
                .Cols(COL_Status).AllowEditing = False
                .Cols(COL_Status).TextAlignFixed = TextAlignEnum.CenterCenter
                .Cols(COL_Status).Caption = "Extracted"



                COL_HASHVALUE = 8
                'Code Start-Added by kanchan on 20101020 
                .Cols(COL_HASHVALUE).Width = 200
                '.SetData(0, COL_HASHVALUE, "Hash Value")
                .Cols(COL_HASHVALUE).AllowEditing = True
                .Cols(COL_HASHVALUE).TextAlignFixed = TextAlignEnum.CenterCenter
                .Cols(COL_HASHVALUE).Caption = "Hash Value"


                COL_HASHALGO = 9
                'Code Start-Added by kanchan on 20101020 
                .Cols(COL_HASHALGO).Width = 80
                '.SetData(0, COL_HASHALGO, "Hash Algorithm")
                .Cols(COL_HASHALGO).AllowEditing = False
                .Cols(COL_HASHALGO).Visible = False
                .Cols(COL_HASHALGO).TextAlignFixed = TextAlignEnum.CenterCenter
                .Cols(COL_HASHALGO).Caption = "Hash Algorithm"



                'Code Start-Added by kanchan on 20101020 
                COL_PatientID = 10
                .Cols(COL_PatientID).Width = 0
                '.SetData(0, COL_PatientID, "PatientID")
                .Cols(COL_PatientID).AllowEditing = False
                .Cols(COL_PatientID).Visible = False
                .Cols(COL_PatientID).TextAlignFixed = TextAlignEnum.CenterCenter



            End If


        End With
    End Sub

    Private Sub SetGridStyleforInvalidFiles(ByVal dt As DataTable)
        cfgCCD.DataSource = Nothing

        With cfgCCD
            .DataSource = dt

            .Dock = DockStyle.Fill
            Dim _TotalWidth As Single = 0
          
            _TotalWidth = (.Width - 20) / 5

            .AllowEditing = True
            .AllowAddNew = False

            .Styles.ClearUnused()
            If rbImport.Checked = True AndAlso cmbSelectFileType.SelectedValue = 2 Then

                '' .Cols.Count = 10
                '.Cols(0).Width = 0
                ''.SetData(0, COL_FileName, "Document Name")
                '.Cols(0).DataType = GetType(Date)
                '.Cols(0).AllowEditing = True

                .Cols(Col_ErrorFileName).Width = _TotalWidth * 2.5
                '.SetData(0, COL_Source, "Source")
                .Cols(Col_ErrorFileName).AllowEditing = False
                .Cols(Col_ErrorFileName).TextAlignFixed = TextAlignEnum.CenterCenter
                .Cols(Col_ErrorFileName).Caption = "File Name"

               


                .Cols(COl_ErrorImportDate).Width = _TotalWidth * 1.5
                .Cols(COl_ErrorImportDate).AllowEditing = False
                cfgCCD.Cols(COl_ErrorImportDate).DataType = GetType(System.String)
                '.SetData(0, COL_dtDocTimeStamp, "Import Date")
                .Cols(COl_ErrorImportDate).TextAlignFixed = TextAlignEnum.CenterCenter
                .Cols(COl_ErrorImportDate).Caption = "Import Date"


                .Cols(Col_CDAErrorId).Width = _TotalWidth * 0
                '.SetData(0, COL_CCDId, "CCD ID")
                .Cols(Col_CDAErrorId).DataType = GetType(Int64)
                .Cols(Col_CDAErrorId).AllowEditing = True
                .Cols(Col_CDAErrorId).Visible = False
                .Cols(Col_CDAErrorId).TextAlignFixed = TextAlignEnum.CenterCenter

                'Code Start-Added by kanchan on 20101020 


                .Cols(Col_Errorimportuser).Width = _TotalWidth * 1
                '.SetData(0, COL_ImportUser, "Import User")
                .Cols(Col_Errorimportuser).AllowEditing = False
                .Cols(Col_Errorimportuser).TextAlignFixed = TextAlignEnum.CenterCenter
                .Cols(Col_Errorimportuser).Caption = "Import User"

            End If
        End With

    End Sub

    Private Sub tsEjectionFraction_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tsEjectionFraction.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Refresh"
                    'Code Start-Added by kanchan on 20101020 for view Import/Exported CCD-CCR
                    'Dim dt As DataTable = PopulateCCDFiles()
                    'If Not IsNothing(dt) Then
                    '    If dt.Rows.Count > 0 Then
                    '        SetGridStyle(dt)
                    '        ogloUC_generalsearch.IntialiseDatatable(dt)
                    '    End If
                    'End If
                    If cmbSelectFileType.SelectedValue = 2 AndAlso rbImport.Checked = True Then
                        Refresh_ErrorDetails()
                    Else
                        Refresh_Form()
                    End If


                Case "Extract"
                    ''Added by Mayuri:20130213-To reconciled information
                    ReconcileInformation()
                Case "Close"
                    Me.Close()
                Case "Reconcile"
                    ShowReconciliation()
                Case "ViewCCDCCR"
                    ModifyCCDFile()
                Case "CDAErrors"
                    ModifyCCDFile()
            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewCCD, gloAuditTrail.ActivityType.View, ex, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub ShowReconciliation()
        Dim ogloCCDReconcile As New gloCCDLibrary.gloCCDReconcilation
        Dim _dtUnfinishedReconcile As DataTable = Nothing
        Try

            If (IsNothing(cfgCCD) = False AndAlso cfgCCD.Rows.Count > 1) Then

                Dim frmReconcilation As New frmReconcileList(_PatientID)
                frmReconcilation.LoginUser = gstrLoginName
                frmReconcilation.LoginID = gnLoginID
                frmReconcilation.ShowDialog(IIf(IsNothing(frmReconcilation.Parent), Me, frmReconcilation.Parent))

                If IsNothing(Me.ParentForm) = False Then
                    CType(Me.ParentForm, MainMenu).ShowReconciliationAlert()
                End If

                If IsNothing(frmReconcilation) = False Then
                    frmReconcilation.Dispose()
                    frmReconcilation = Nothing
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If IsNothing(ogloCCDReconcile) = False Then
                ogloCCDReconcile.Dispose()
                ogloCCDReconcile = Nothing
            End If
            If IsNothing(_dtUnfinishedReconcile) = False Then
                _dtUnfinishedReconcile.Dispose()
                _dtUnfinishedReconcile = Nothing
            End If
        End Try

    End Sub
    Private Sub ReconcileInformation()
        Try


            If (IsNothing(cfgCCD) = False AndAlso cfgCCD.Rows.Count > 1) Then

                If cfgCCD.RowSel > 0 Then

                    If IsNothing(cfgCCD.GetData(cfgCCD.Row, COL_CCDId)) = False Then

                        Dim _CCDID As Int64 = cfgCCD.GetData(cfgCCD.Row, COL_CCDId)
                        Dim _CCDPatientID As Int64 = cfgCCD.GetData(cfgCCD.Row, COL_PatientID)
                        Dim _SourceName As String = Convert.ToString(cfgCCD.GetData(cfgCCD.Row, COL_Source))
                        Dim _ListStatus As Integer
                        Dim _DateExtracted As String = Convert.ToString(Format(cfgCCD.GetData(cfgCCD.Row, COL_dtDocTimeStamp), "MM/dd/yyyy"))
                        Dim _importedUser As String = Convert.ToString(cfgCCD.GetData(cfgCCD.Row, COL_ImportUser))

                        If Convert.ToString(cfgCCD.GetData(cfgCCD.Row, COL_Status)) = "Yes" Then
                            _ListStatus = "2"
                            Dim oResult As Windows.Forms.DialogResult
                            If _importedUser <> "" Then
                                oResult = MessageBox.Show("Reconciliation lists were already generated on " & _DateExtracted & " by " & _importedUser & "." & vbNewLine & "Continue generating new reconciliation lists?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                            Else
                                oResult = MessageBox.Show("Reconciliation lists were already generated on " & _DateExtracted & vbNewLine & "Continue generating new reconciliation lists?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                            End If

                            If oResult = DialogResult.Yes Then
                                Dim ofrm As New frmCCD_ExtractReconcillation(_CCDID, _CCDPatientID, _SourceName, _ListStatus)
                                If IsNothing(ofrm) = False Then
                                    ofrm.ShowInTaskbar = False
                                    ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                                    ofrm.Close()
                                    ofrm.Dispose()
                                    ofrm = Nothing
                                End If
                                Refresh_Form()
                            ElseIf oResult = DialogResult.No Then
                                Exit Sub
                            End If
                        ElseIf Convert.ToString(cfgCCD.GetData(cfgCCD.Row, COL_Status)) = "" Then
                            _ListStatus = "1"
                            Dim ofrm As New frmCCD_ExtractReconcillation(_CCDID, _CCDPatientID, _SourceName, _ListStatus)
                            If IsNothing(ofrm) = False Then
                                ofrm.ShowInTaskbar = False
                                ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                                ofrm.Close()
                                ofrm.Dispose()
                                ofrm = Nothing
                            End If
                            Refresh_Form()
                        Else
                            Dim ofrm As New frmCCD_ExtractReconcillation(_CCDID, _CCDPatientID, _SourceName, _ListStatus)
                            If IsNothing(ofrm) = False Then
                                ofrm.ShowInTaskbar = False
                                ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                                ofrm.Close()
                                ofrm.Dispose()
                                ofrm = Nothing
                            End If
                            Refresh_Form()
                        End If



                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewCCD, gloAuditTrail.ActivityType.View, ex, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub
    'Code Start-Added by kanchan on 20101020 for view Import/Exported CCD-CCR
    Private Function Refresh_Form()
        Dim _dt As DataTable = Nothing
        Try
            ogloUC_generalsearch.SearchString = String.Empty
            _dt = PopulateCCDFiles()

            SetGridStyle(_dt)
            ogloUC_generalsearch.IntialiseDatatable(_dt)
            '    End If
            'End If

            If IsImport = True Then
                If _PatientID <> 0 Then
                    tlbbtn_Extract.Visible = True
                    tlbbtn_Reconcile.Visible = True
                Else
                    tlbbtn_Extract.Visible = False
                    tlbbtn_Reconcile.Visible = False
                End If
                tls_SaveAs.Visible = False

            Else
                tls_SaveAs.Visible = True
                tlbbtn_Extract.Visible = False
                tlbbtn_Reconcile.Visible = False
            End If




        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewCCD, gloAuditTrail.ActivityType.View, ex, gloAuditTrail.ActivityOutCome.Failure)
        Finally
            'If Not IsNothing(_dt) Then
            '    _dt.Dispose()
            '    _dt = Nothing
            'End If
        End Try
        Return Nothing
    End Function
    Private Sub Refresh_ErrorDetails()
        Dim _dt As DataTable = Nothing
        Try
            ogloUC_generalsearch.SearchString = String.Empty
            _dt = getCDAErrorsFiles(0, 0)

            SetGridStyleforInvalidFiles(_dt)
            ogloUC_generalsearch.IntialiseDatatable(_dt)
            '    End If
            'End If

            'If IsImport = True Then
            '    If _PatientID <> 0 Then
            '        tlbbtn_Extract.Visible = True
            '        tlbbtn_Reconcile.Visible = True
            '    Else
            '        tlbbtn_Extract.Visible = False
            '        tlbbtn_Reconcile.Visible = False
            '    End If
            '    tls_SaveAs.Visible = False

            'Else
            '    tls_SaveAs.Visible = True
            '    tlbbtn_Extract.Visible = False
            '    tlbbtn_Reconcile.Visible = False
            'End If




        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewDocumentErrors, gloAuditTrail.ActivityType.View, ex, gloAuditTrail.ActivityOutCome.Failure)
        Finally
            'If Not IsNothing(_dt) Then
            '    _dt.Dispose()
            '    _dt = Nothing
            'End If
        End Try
    End Sub
    Private Sub AddValues()
        RemoveHandler cmbSelectFileType.SelectedIndexChanged, AddressOf cmbSelectFileType_SelectedIndexChanged
        Dim dtFiletype As DataTable = New DataTable()
        dtFiletype.Columns.Add("Filetype")
        dtFiletype.Columns.Add("Value")
        dtFiletype.Rows.Add("Valid File", 1)
        dtFiletype.Rows.Add("Invalid File", 2)
        cmbSelectFileType.DataSource = dtFiletype
        cmbSelectFileType.DisplayMember = "Filetype"
        cmbSelectFileType.ValueMember = "Value"
        AddHandler cmbSelectFileType.SelectedIndexChanged, AddressOf cmbSelectFileType_SelectedIndexChanged
    End Sub

    Private Sub cfgCCD_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cfgCCD.MouseDoubleClick
        Try

            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = cfgCCD.HitTest(ptPoint)

            If (cfgCCD.Rows.Count > 0 AndAlso htInfo.Row > 0) Then
                ModifyCCDFile()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewCCD, gloAuditTrail.ActivityType.View, ex, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub
    Private Sub ModifyCCDFile()
        Try
            Me.Cursor = Cursors.WaitCursor
            If (cfgCCD.Rows.Count > 1) Then
                If cmbSelectFileType.SelectedValue = 2 AndAlso rbImport.Checked = True Then
                    If IsNothing(cfgCCD.GetData(cfgCCD.Row, Col_CDAErrorId)) = False Then
                        Dim _ErrorCCDID As Int64 = cfgCCD.GetData(cfgCCD.Row, Col_CDAErrorId)
                        Dim dt As DataTable = getCDAErrorsFiles(1, _ErrorCCDID)
                        If IsNothing(dt) = False Then
                            If dt.Rows.Count > 0 Then
                                Dim filebytes As Byte() = dt.Rows(0)("CDAErrorDescription")
                                If IsNothing(filebytes) = False AndAlso filebytes.Length > 0 Then
                                    Dim errorresult As String = System.Text.Encoding.UTF8.GetString(filebytes)
                                    If errorresult <> "" Then
                                        Dim cdareader As gloCCDLibrary.gloCDAReader = New gloCCDLibrary.gloCDAReader()
                                        cdareader.ShowResponse(errorresult, Convert.ToString(dt.Rows(0)("File Name")), _ErrorCCDID)
                                        If Not IsNothing(cdareader) Then
                                            cdareader.Dispose()
                                        End If
                                    End If

                                End If
                            End If
                        End If
                    End If
                Else
                    If cfgCCD.GetData(0, cfgCCD.ColSel) <> "Hash Value" Then
                        If IsNothing(cfgCCD.GetData(cfgCCD.Row, COL_FileName)) = False Then

                            'Code Start-Added by kanchan on 20101020 for CCD browser display

                            'Dim filename As String = cfgCCD.GetData(cfgCCD.Row, COL_FileName)
                            'Dim firstName As String = cfgCCD.GetData(cfgCCD.Row, COL_FirstName)
                            'Dim LastName As String = cfgCCD.GetData(cfgCCD.Row, COL_LastName)
                            'Dim frm As New frmCCD_ViewDetails(System.Windows.Forms.Application.StartupPath & "\Temp\" & filename & ".doc")
                            'Dim frm As New frmCCD_ViewDetails(filename)


                            Dim _CCDID As Int64 = cfgCCD.GetData(cfgCCD.Row, COL_CCDId)
                            'Code Start-Added by kanchan on 20101020 for view Import/Exported CCD-CCR
                            Dim FilePath As String
                            If IsImport = True Then
                                FilePath = RetrieveDocumentFile(_CCDID)
                            Else
                                FilePath = cfgCCD.GetData(cfgCCD.Row, COL_FileName)
                                Dim oFile As FileInfo = New FileInfo(FilePath)
                                If oFile.Exists = False Then
                                    FilePath = RetrieveDocumentFile(_CCDID)
                                End If
                                oFile = Nothing
                            End If

                            'Dim frm As New frmPatientClinicalInformation(filename & ".rtf")
                            If (IsNothing(FilePath) = False) Then


                                Dim frm As New frmPatientClinicalInformation()


                                gloLibCCDGeneral.CCDFilePath = frm.CCDXMLFilePath
                                ' Added transformation
                                Dim myXslTransform As Xml.Xsl.XslTransform
                                Dim _strfileName As String = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".html", "yyyyMMddhhmmssffff") 'DateTime.Now.ToString("yyyyMMddhhmmssffff") & System.Guid.NewGuid().ToString() & ".html"


                                'Dim ogloCCDInterface As gloCCDInterface
                                'ogloCCDInterface = New gloCCDInterface
                                Dim sFileType As String = String.Empty
                                sFileType = GetClinicalFileType(FilePath)
                                Dim ClinicName As String = cfgCCD.GetData(cfgCCD.Row, COL_Source).ToString()
                                frm.SourceName = ClinicName
                                If sFileType = "CCR" Then
                                    myXslTransform = New Xml.Xsl.XslTransform()
                                    'myXslTransform.Load("http://www.glostream.com/css/XSLT/gloccrCss.xsl")
                                    myXslTransform.Load(Application.StartupPath & "/gloccrCss.xsl")
                                    myXslTransform.Transform(FilePath, _strfileName) 'System.IO.Path.Combine(gloSettings.FolderSettings.AppTempFolderPath, _strfileName))
                                    frm.WebBrowser1.Navigate(_strfileName) 'System.IO.Path.Combine(gloSettings.FolderSettings.AppTempFolderPath, _strfileName))
                                ElseIf sFileType = "CCD" Then
                                    myXslTransform = New Xml.Xsl.XslTransform()
                                    'myXslTransform.Load("http://www.glostream.com/css/XSLT/gloCCDAcss_MU2.xsl")
                                    myXslTransform.Load(Application.StartupPath & "/gloCCDAcss_MU2.xsl")
                                    myXslTransform.Transform(FilePath, _strfileName) 'System.IO.Path.Combine(gloSettings.FolderSettings.AppTempFolderPath, _strfileName))
                                    frm.WebBrowser1.Navigate(_strfileName) 'System.IO.Path.Combine(gloSettings.FolderSettings.AppTempFolderPath, _strfileName))

                                Else
                                    frm.WebBrowser1.Navigate(FilePath)
                                End If



                                frm.IsShowSaveButton = False

                                'frm.MdiParent = Me.MdiParent
                                frm.WindowState = FormWindowState.Maximized
                                frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                                'After showing the file in browser delete temporary file .
                                If System.IO.File.Exists(_strfileName) Then 'System.IO.Path.Combine(gloSettings.FolderSettings.AppTempFolderPath, _strfileName)) Then
                                    File.Delete(_strfileName) 'System.IO.Path.Combine(gloSettings.FolderSettings.AppTempFolderPath, _strfileName))
                                End If

                                If Not IsNothing(frm) Then
                                    frm.Dispose()
                                    frm = Nothing
                                End If
                            End If

                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewCCD, gloAuditTrail.ActivityType.View, ex, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub cfgCCD_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles cfgCCD.MouseDown
        If e.Button = MouseButtons.Right Then
            If rbExport.Checked = True Then
                Dim strStatus As String = ""
                'Dim nCCDID As Int64 = 0
                Try
                    Dim CmpControls() As System.Windows.Forms.ContextMenuStrip = {cmsCCDCCR}


                    If (IsNothing(CmpControls) = False) Then
                        If CmpControls.Length > 0 Then
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(CmpControls)
                        End If
                    End If

                    'If (IsNothing(CmpControls) = False) Then
                    '    If CmpControls.Length > 0 Then
                    '        gloGlobal.cEventHelper.DisposeContextMenuStrip(CmpControls)
                    '    End If
                    'End If
                    If (IsNothing(cmsCCDCCR.Items) = False) Then
                        cmsCCDCCR.Items.Clear()
                    End If
                   
                Catch

                End Try

                'cmsCCDCCR.Items.Clear()
                Dim _nRow As Integer = cfgCCD.HitTest(e.X, e.Y).Row
                If _nRow >= 0 Then
                    cfgCCD.RowSel = _nRow
                    strStatus = Convert.ToString(cfgCCD.GetData(cfgCCD.RowSel, 10))
                    'nCCDID = Convert.ToInt64(cfgCCD.GetData(cfgCCD.RowSel, 4))
                    If (strStatus.ToUpper() <> "GENERATED") Then
                        Exit Sub
                    End If
                    '    nAppointmentID = Convert.ToInt64(c1PatientStatus.GetData(c1PatientStatus.RowSel, 3)) ''appointmentID
                Else
                    Exit Sub
                End If

                Dim oMenuItem As ToolStripMenuItem
                Try
                    Dim CmpControls() As System.Windows.Forms.ContextMenuStrip = {cfgCCD.ContextMenuStrip}

                    If (IsNothing(CmpControls) = False) Then
                        If CmpControls.Length > 0 Then
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(CmpControls)
                        End If
                    End If

                    If (IsNothing(CmpControls) = False) Then
                        If CmpControls.Length > 0 Then
                            gloGlobal.cEventHelper.DisposeContextMenuStrip(CmpControls)
                        End If
                    End If
                    If (IsNothing(cfgCCD.ContextMenuStrip.Items) = False) Then
                        cfgCCD.ContextMenuStrip.Items.Clear()
                    End If
                   
                Catch

                End Try
              
                cfgCCD.ContextMenuStrip = cmsCCDCCR
                Dim nRow As Integer
                nRow = _nRow
                oMenuItem = New ToolStripMenuItem
                oMenuItem.Text = "CCD/CCR file declined by patient"
                oMenuItem.Tag = "CCD/CCR file declined by patient"
                oMenuItem.ForeColor = Color.FromArgb(31, 73, 125)
                'oMenuItem.Image = Imgts_PatientDetails.Images(33)
                cmsCCDCCR.Items.Add(oMenuItem)
                AddHandler oMenuItem.Click, AddressOf ChangeStatus
            End If
        End If

    End Sub

    Private Sub ChangeStatus(ByVal sender As Object, ByVal e As EventArgs)
        Dim nCCDID As Int64 = 0
        nCCDID = Convert.ToInt64(cfgCCD.GetData(cfgCCD.RowSel, 4))

        Dim oResult As New Object
        Dim sqlParam As SqlParameter = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim conn As New SqlConnection(GetConnectionString)

        Try
            cmd = New SqlCommand()
            cmd.Connection = conn
            cmd.CommandText = "CCD_FileDeclinedByPatient"
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = cmd.Parameters.Add("@nCCDID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nCCDID
            conn.Open()
            oResult = cmd.ExecuteScalar()

            cfgCCD.SetData(cfgCCD.RowSel, COL_sStatus, "Declined")


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.View, ex, gloAuditTrail.ActivityOutCome.Failure)
            Throw ex
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            If Not IsNothing(conn) Then
                conn.Dispose()
                conn = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing
            oResult = Nothing
        End Try

       
    End Sub

    Private Sub cfgCCD_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cfgCCD.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub
    'Code Start-Added by kanchan on 20101020 for view Import/Exported CCD-CCR
    Public Function GetClinicalFileType(ByVal strFilePath As String) As String
        Dim xreader As XmlReader = Nothing
        Dim strTypre As String = ""
        Try
            'Dim oXMLSettings As New Xml.XmlReaderSettings()

            xreader = XmlReader.Create(strFilePath)
            While xreader.Read
                If xreader.NodeType = XmlNodeType.Element Then
                    Select Case xreader.Name
                        Case "ContinuityOfCareRecord"
                            'gloLibCCDGeneral.ClinicalDocFileType = "CCR"
                            strTypre = "CCR"
                        Case "ClinicalDocument"
                            'gloLibCCDGeneral.ClinicalDocFileType = "CCD"
                            strTypre = "CCD"
                        Case "ccr:ContinuityOfCareRecord"
                            'gloLibCCDGeneral.ClinicalDocFileType = "CCR"
                            strTypre = "CCR"
                    End Select
                End If
            End While

            Return strTypre

        Catch ex As Exception
            Return ""
        Finally
            If Not IsNothing(xreader) Then
                xreader.Close()
                xreader = Nothing
            End If
        End Try
    End Function
    'Code Start-Added by kanchan on 20101020 for view Import/Exported CCD-CCR
    Public Function RetrieveDocumentFile(ByVal nCCDId As Int64) As String
        Dim oResult As Object = Nothing
        Dim strFileName As String = ""
        Dim sqlParam As SqlParameter
        Dim cmd As SqlCommand = Nothing
        Dim conn As New SqlConnection(GetConnectionString)

        Try
            cmd = New SqlCommand()
            cmd.Connection = conn
            If IsImport = True Then
                cmd.CommandText = "CCD_RetrieveCCDFile"
            Else
                cmd.CommandText = "CCD_RetrieveExportedCCDFile"
            End If
            'cmd = New SqlCommand("CCD_RetrieveCCDFile", conn)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = cmd.Parameters.Add("@nCCDID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nCCDId
            conn.Open()
            oResult = cmd.ExecuteScalar()

            If oResult Is Nothing Then
                Return ""
            End If

            If IsDBNull(oResult) = False Then
                strFileName = ExamNewFaxFileName(gloSettings.FolderSettings.AppTempFolderPath, ".xml")
                '' generate Physical file
                strFileName = GenerateFile(oResult, strFileName)
                Return strFileName
            Else
                Return ""
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.View, ex, gloAuditTrail.ActivityOutCome.Failure)
            Throw ex
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            If Not IsNothing(conn) Then
                conn.Dispose()
                conn = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing
            oResult = Nothing
        End Try

    End Function
    'Code Start-Added by kanchan on 20101020 for CCD browser display
    Public Function GenerateFile(ByVal cntFromDB As Object, ByVal strFileName As String) As String
        Dim content() As Byte
        '  Dim stream As MemoryStream = Nothing
        Dim oFile As System.IO.FileStream = Nothing
        Try
            If Not cntFromDB Is Nothing Then
                content = CType(cntFromDB, Byte())
                '   stream = New MemoryStream(content)
                oFile = New System.IO.FileStream(strFileName, System.IO.FileMode.Create)
                oFile.Write(content, 0, content.Length)
                '  stream.WriteTo(oFile)
                'oFile.Close()
                'oFile.Dispose()
                'stream.Close()
                'stream.Dispose()
                'stream = Nothing
                'oFile = Nothing
                content = Nothing
                Return strFileName
            Else
                Return Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            Throw ex
        Finally
            If Not IsNothing(oFile) Then
                oFile.Close()
                oFile.Dispose()
                oFile = Nothing
            End If

            'If Not IsNothing(stream) Then
            '    stream.Close()
            '    stream.Dispose()
            '    stream = Nothing
            'End If
            content = Nothing
        End Try

    End Function
    'Code Start-Added by kanchan on 20101020 for view Import/Exported CCD-CCR
    Private Sub rbImport_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbImport.CheckedChanged
        If rbImport.Checked = True Then
            IsImport = True
            rbImport.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            'cmbSelectFileType.Visible = True
            If _PatientID = 0 Then
                PnlDisplayFileType.Visible = True
            Else
                PnlDisplayFileType.Visible = False
            End If
            'tsb_ViewCDAErrors.Visible = True
        Else
            IsImport = False
            rbImport.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
            'cmbSelectFileType.Visible = False
            PnlDisplayFileType.Visible = False
            tsb_ViewCDAErrors.Visible = False
        End If
    End Sub
    'Code Start-Added by kanchan on 20101020 for view Import/Exported CCD-CCR
    Private Sub rbExport_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbExport.CheckedChanged
        If rbExport.Checked = True Then
            IsImport = False
            rbExport.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            cmbSelectFileType.SelectedValue = 1
        Else
            IsImport = True
            rbExport.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
        Refresh_Form()
    End Sub
    'Code Start-Added by kanchan on 20101020 for view Import/Exported CCD-CCR
    Private Sub tls_SaveAs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tls_SaveAs.Click

        Try
            If (IsNothing(cfgCCD) = False AndAlso cfgCCD.Rows.Count > 1) Then

                If cfgCCD.RowSel > 0 Then

                    If IsNothing(cfgCCD.GetData(cfgCCD.Row, COL_CCDId)) = False Then

                        Dim _CCDID As Int64 = cfgCCD.GetData(cfgCCD.Row, COL_CCDId)
                        Dim _CCDPatientID As Int64 = cfgCCD.GetData(cfgCCD.Row, COL_PatientID)
                        Dim _DocType As String = cfgCCD.GetData(cfgCCD.Row, COL_DocType)
                        SaveAsCCDFile(_CCDID, _CCDPatientID, _DocType)

                    Else
                        MessageBox.Show("Select the CCD file.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

                Else
                    MessageBox.Show("Select the CCD file.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewCCD, gloAuditTrail.ActivityType.View, ex, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    'Code Start-Added by kanchan on 20101020 for view Import/Exported CCD-CCR
    Public Function SaveAsCCDFile(ByVal nCCDId As Int64, ByVal nCCDPatientID As Int64, ByVal sDocType As String) As String
        Try
            Dim _PatientLastName As String = ""

            If (nCCDId > 0 AndAlso nCCDPatientID > 0) Then

                Dim FilePath As String
                Dim oFileInfo As System.IO.FileInfo
                Dim _isCopyCreated As Boolean = False
                ''Added on 20110916-CCD Changes
                Dim ogloCCDDBLayer As gloCCDLibrary.gloCCDDatabaseLayer
                ogloCCDDBLayer = New gloCCDLibrary.gloCCDDatabaseLayer()
                _PatientLastName = ogloCCDDBLayer.GetPatientLastName(_PatientID)

                If IsNothing(ogloCCDDBLayer) = False Then
                    ogloCCDDBLayer.Dispose()
                    ogloCCDDBLayer = Nothing
                End If

                ''End code Added on 20110916
                FilePath = RetrieveDocumentFile(nCCDId)
                If (IsNothing(FilePath) = False) Then


                    oFileInfo = New FileInfo(FilePath)

                    Dim objgloCCDInterface As New gloCCDLibrary.gloCCDInterface()
                    Dim _objfrmencrypt As frmCCDEncryption
                    Dim _FileName As String = ""
                    If sDocType = "Clinical Document Architecture" Then
                        _objfrmencrypt = New frmCCDEncryption(gstrCCDFilePath, nCCDPatientID, _PatientLastName, "CDA")
                    Else
                        _objfrmencrypt = New frmCCDEncryption(gstrCCDFilePath, nCCDPatientID, "", "", "", _PatientLastName)
                    End If

                    _objfrmencrypt.ShowDialog(IIf(IsNothing(_objfrmencrypt.Parent), Me, _objfrmencrypt.Parent))

                    If _objfrmencrypt._issave = True Then
                        _FileName = _objfrmencrypt.FilePath.ToString()
                    End If

                    If _FileName <> "" Then
                        oFileInfo.CopyTo(_FileName)
                        If sDocType = "Clinical Document Architecture" Then
                            Dim oCDADataExtraction As gloCDADataExtraction = New gloCCDLibrary.gloCDADataExtraction()
                            oCDADataExtraction.SaveExportedCDA(nCCDPatientID, _FileName, "CDA File Copy")
                            If IsNothing(oCDADataExtraction) = False Then
                                oCDADataExtraction.Dispose()
                                oCDADataExtraction = Nothing
                            End If
                        Else
                            _isCopyCreated = objgloCCDInterface.SaveAsExportedCCD(nCCDPatientID, _FileName, "CCD File Copy")
                        End If
                    End If



                    If IsNothing(objgloCCDInterface) = False Then
                        objgloCCDInterface.Dispose()
                        objgloCCDInterface = Nothing
                    End If

                    If _objfrmencrypt.IsSecureDocument(_objfrmencrypt.sEncryptKey) = True Then
                        _FileName = CompressCCDFile(_FileName, _objfrmencrypt.sEncryptKey)
                    End If
                    _objfrmencrypt.Dispose()
                    oFileInfo = Nothing
                    'As per discussion the View CCDA File on Save As should not get deleted
                    'Added for Auto Deleting CCDA files
                    'Try
                    '    If Not IsNothing(_FileName) Then
                    '        If _isAutoDeleteCCDAFiles = True Then
                    '            File.Delete(_FileName)
                    '        End If
                    '    End If
                    'Catch ex As Exception
                    '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
                    'End Try
                End If
                Refresh_Form()

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewCCD, gloAuditTrail.ActivityType.View, ex, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return Nothing
    End Function
    'Code Start-Added by kanchan on 20101020 for Hashing & security of CCD File
    Private Function CompressCCDFile(ByVal filePath As String, ByVal sEncryotKey As String) As String

        Dim _compressedFilePath As String = ""
        Dim _fileInfo As FileInfo

        Try

            If sEncryotKey.Trim() <> "" Then

                _compressedFilePath = gloSecurity.gloEncryption.PerformFileEncryption(filePath, sEncryotKey, True)

                _fileInfo = New FileInfo(_compressedFilePath)

                If _fileInfo.Exists = True Then
                    _fileInfo = Nothing
                    _fileInfo = New FileInfo(filePath)
                    _fileInfo.Delete()
                    _fileInfo = Nothing
                End If
                _fileInfo = Nothing

            End If

        Catch ex As Exception

        End Try

        Return _compressedFilePath

    End Function
    'Code Start-Added by kanchan on 20101020 for Hashing & security of CCD File
    Private Function IsSecureDocument(ByRef EncryptionKey As String) As Boolean

        Try

            Dim oResult As DialogResult = MessageBox.Show("Do you want to encrypt the document", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            Dim blnSecureDoc As Boolean = False
            Dim blnSecureContinue As Boolean = False
            Dim sEncryotKey As String = ""
            Dim bEncryptedExe As Boolean = True

            If oResult = DialogResult.Yes Then

                Dim ofrmExportEncryption As New frmExportEncryption
                ofrmExportEncryption.ShowInTaskbar = False
                ofrmExportEncryption.ShowDialog(IIf(IsNothing(ofrmExportEncryption.Parent), Me, ofrmExportEncryption.Parent))

                If ofrmExportEncryption.DialogResult = DialogResult.OK Then
                    sEncryotKey = ofrmExportEncryption.sEncryptKey
                    bEncryptedExe = ofrmExportEncryption.bEncryptedExe
                    blnSecureDoc = True
                    blnSecureContinue = True
                ElseIf ofrmExportEncryption.DialogResult = DialogResult.Cancel Then
                    blnSecureDoc = False
                    blnSecureContinue = False
                End If

                ofrmExportEncryption.Dispose()

            End If

            EncryptionKey = sEncryotKey
            Return blnSecureContinue

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    'Private Sub tlbbtn_Reconcile_Click(sender As System.Object, e As System.EventArgs)
    '    Dim frmReconcilation As New gloCCDLibrary.frmReconcileList(_PatientID)
    '    If frmReconcilation.ShowDialog(Me) = DialogResult.OK Then
    '        If IsNothing(Me.ParentForm) = False Then
    '            CType(Me.ParentForm, MainMenu).ShowReconciliationAlert()
    '        End If

    '    End If

    'End Sub

    

   

    Private Function getCDAErrorsFiles(ByVal type As Int16, Optional ByVal CDAErrorId As Int64 = 0) As DataTable
        Try
            Dim dt As DataTable = Nothing
            Dim sqlParam As SqlParameter
            Dim cmd As SqlCommand = Nothing
            Dim conn As New SqlConnection(GetConnectionString)
            Try
                cmd = New SqlCommand()
                cmd.Connection = conn
                cmd.CommandText = "gsp_getCDAErrors"
                'cmd = New SqlCommand("CCD_RetrieveCCDFile", conn)
                cmd.CommandType = CommandType.StoredProcedure

                sqlParam = cmd.Parameters.Add("@type", SqlDbType.SmallInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = type

                sqlParam = cmd.Parameters.Add("@ErrorCCDID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = CDAErrorId
                conn.Open()
                Dim dr = cmd.ExecuteReader()
                dt = New DataTable()
                dt.Load(dr)
                Return dt
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewDocumentErrors, gloAuditTrail.ActivityType.View, ex, gloAuditTrail.ActivityOutCome.Failure)
                Throw ex
            Finally
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                If Not IsNothing(conn) Then
                    conn.Dispose()
                    conn = Nothing
                End If
                If Not IsNothing(cmd) Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If
                sqlParam = Nothing
            End Try
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

   
    Private Sub cmbSelectFileType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbSelectFileType.SelectedIndexChanged
        Try
            'Dim dt As DataTable = Nothing
            If cmbSelectFileType.SelectedValue = 2 Then
                tsb_ViewCDAErrors.Visible = True
                tlb_ViewCCD.Visible = False
                Refresh_ErrorDetails()
                'dt = getCDAErrorsFiles(0)
                'SetGridStyleforInvalidFiles(dt)
                'ogloUC_generalsearch.SearchString = String.Empty
                'ogloUC_generalsearch.IntialiseDatatable(dt)

            ElseIf cmbSelectFileType.SelectedValue = 1 Then
                tsb_ViewCDAErrors.Visible = False
                tlb_ViewCCD.Visible = True
                Refresh_Form()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewDocumentErrors, gloAuditTrail.ActivityType.View, ex, gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

 
    Private Sub tsEjectionFraction_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles tsEjectionFraction.MouseDoubleClick

    End Sub
End Class
'Private Sub SetGridStyle(ByVal dt As DataTable)
'          '        

'       dth = _TotalWidth * 1.0
'        .SetData(0, COL_EXAMNAMEBUTTON, "Open Exam")
'        '''' 
'        If blnOpenFromExam Then
'            .Cols(COL_EXAMNAMEBUTTON).Width = Width * 0
'        End If
'        Dim strStatus As String

'        '  Dim strPresc As String = " "

'        strStatus = "Resolved" & "|" & "Active" & "|" & "Inactive" & "|" & "Chronic"
'        Dim csStatus As CellStyle = .Styles.Add("Status")
'        '' Fill Values In ComboBox
'        csStatus.ComboList = strStatus
'        .Cols(COL_STATUS).Style = csStatus

'        Fill_Diagnosis(_VisitID)
'        Dim csDia As CellStyle = .Styles.Add("Dia")
'        '' Fill Values In ComboBox
'        csDia.ComboList = strDia
'        .Cols(COL_DIAGNOSIS).Style = csDia

'        Dim csTempRx As CellStyle = .Styles.Add("TempRx")
'        csTempRx.ComboList = ""
'        .Cols(COL_PRESCRIPTION).Style = csTempRx


'        cStyle = .Styles.Add("BubbleValues")
'        cStyle.ComboList = "..."
'        .Cols(COL_DIAGNOSISBUTTON).Style = cStyle

'        cStyle = .Styles.Add("BubbleValues")
'        cStyle.ComboList = "..."
'        .Cols(COL_RxBUTTON).Style = cStyle

'        .Cols(COL_COMPLAINTS).Style = Nothing

'        cStyle = .Styles.Add("BubbleValues")
'        cStyle.ComboList = "..."
'        .Cols(COL_EXAMNAMEBUTTON).Style = cStyle
'        ''
'        'dtpDOS.Value = Format(GetVisitdate(_VisitID), "MM/dd/yyyy")

'        '' Table dt Contains following Columns
'        '' ProblemID, VisitID , dtDOS, Diagnosis, Complaint ,Status
'        For i = 0 To dt.Rows.Count - 1
'            .Rows.Add()

'            ''''Set Column Style 
'            '''' Assinge the Cell for ComboBox
'            'Dim rgDia As CellRange = .GetCellRange(.Rows.Count - 1, COL_DIAGNOSIS)
'            'rgDia.Style = csDia  '' .Styles.Add("Dia")

'            '''' Assinge the Cell for ComboBox
'            'Dim rgStatus As CellRange = .GetCellRange(.Rows.Count - 1, COL_STATUS)
'            'rgStatus.Style = csStatus ''''  .Styles.Add("Status")

'            '' Fill the Retrived information to relative controls
'            'dtpDOS.Value = Format(dt.Rows(i)("dtDOS"), "MM/dd/yyyy")

'            .SetData(i + 1, COL_VISITID, dt.Rows(i)("VisitID"))
'            .SetData(i + 1, COL_DOS, Format(dt.Rows(i)("dtDOS"), "MM/dd/yyyy"))
'            .SetData(i + 1, COL_PROBLEMID, dt.Rows(i)("ProblemID"))
'            .SetData(i + 1, COL_COMPLAINTS, dt.Rows(i)("Complaint"))
'            .SetData(i + 1, COL_DIAGNOSIS, dt.Rows(i)("Diagnosis"))
'            '' To set the values in Rx Combolist
'            Dim csPresc As CellStyle = .Styles.Add("Rx" & i + 1)
'            '' Fill Values In ComboBox
'            If Not IsDBNull(dt.Rows(i)("Prescription")) Then
'                strRx = dt.Rows(i)("Prescription").ToString
'                strRx = strRx.Replace(",", "|")
'            End If
'            csPresc.ComboList = strRx
'            .Cols(COL_PRESCRIPTION).Style = csPresc

'            ' .SetData(i + 1, COL_PRESCRIPTION, dt.Rows(i)("Prescription"))
'            Dim cR As C1.Win.C1FlexGrid.CellRange = .GetCellRange(i + 1, COL_PRESCRIPTION)
'            cR.Style = csPresc

'            If dt.Rows(i)("Status") = Status.Inactive Then
'                .SetData(i + 1, COL_STATUS, "Inactive")
'                'ElseIf dt.Rows(i)("Status") = Status.Pending Or dt.Rows(i)("Status") = Status.Active Then
'            ElseIf dt.Rows(i)("Status") = Status.Active Then
'                .SetData(i + 1, COL_STATUS, "Active")
'            ElseIf dt.Rows(i)("Status") = Status.Resolved Then
'                .SetData(i + 1, COL_STATUS, "Resolved")
'            ElseIf dt.Rows(i)("Status") = Status.Chronic Then
'                .SetData(i + 1, COL_STATUS, "Chronic")
'            End If

'            '''' By Mahesh, 20070317
'            .SetData(i + 1, COL_USER, dt.Rows(i)("nUserID"))

'            '' By Mahesh, 20070326 
'            ''  SET Diagnosis Comment Button
'            .SetData(i + 1, COL_DIAGNOSISBUTTON, "")
'            Dim rgDig As C1.Win.C1FlexGrid.CellRange = .GetCellRange(i + 1, COL_DIAGNOSISBUTTON, i + 1, COL_DIAGNOSISBUTTON)
'            rgDig.Style = cStyle

'            .SetData(i + 1, COL_RxBUTTON, "")
'            Dim rgRx As C1.Win.C1FlexGrid.CellRange = .GetCellRange(i + 1, COL_RxBUTTON, i + 1, COL_RxBUTTON)
'            rgRx.Style = cStyle

'        Next
'        '.Cols(COL_DIAGNOSIS).AllowEditing = False
'        '.Cols(COL_STATUS).AllowEditing = False
'    End With
'End Sub

