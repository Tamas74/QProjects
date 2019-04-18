Imports gloGeneralItem
Imports gloSnoMed


#Region " Enumerations "

Public Enum gloGridListControlType
    Providers = 1
    Procedures = 2
    CPT = 3
    ICD9 = 4
    Modifier = 5
    PatientInsurance = 6
    POS = 7
    TOS = 8
    ZIP = 9
    Cvx = 10
    Mvx = 11
    TradeName = 12
    LOINCCode = 13
    LabsCPT = 14
    ClinicalInstruction = 15
    ICD10 = 16
End Enum

Public Enum SearchColumn
    Code = 1
    Description = 2
    Name = 3
End Enum

#End Region
Public Class gloUC_GridList

    Private _MessageBoxCaption As String = "gloEMR"
    Private _DatabaseConnectionString As String = ""
    Private appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    Private _SelectedItems As System.Collections.Generic.List(Of gloItem)
    Private _ControlType As gloGridListControlType
    Private _ismultiselect As Boolean = False
    Private _thiswidth As Integer = 0
    Private _ControlHeader As String = ""
    Private _PatientID As Int64
    Private _ClinicID As Int64 = 0
    Private _UserID As Int64 = 0
    Private _CurrentSelectedRow As Integer = 0
    Private _SearchCount As Integer = 0
    Private _ShowHeader As Boolean = True
    Private _ImmunizationName As String = ""
    Private _LOINCOrCPTName As String = ""
    Private _TOSID As Int64 = 0
    Private _CPTID As Int64 = 0
    Private _ICD9ID As Int64 = 0

    Private _dv As DataView = Nothing

    Private _parentRowIndex As Integer = 0
    Private _parentColIndex As Integer = 0


    Private dvNext As DataView = Nothing

    Private _dtList As DataTable = Nothing

    Private _SelectedCPTCode As String = ""
    Private _SelectedFacilityCode As String = ""
    Private _ControlSearchText As String = ""
    Public Event ItemSelected(ByVal sender As Object, ByVal e As EventArgs)
    Public Event InternalGridKeyDown(ByVal sender As Object, ByVal e As EventArgs)
    Public Event InternalGridLostFocus(ByVal sender As Object, ByVal e As EventArgs)

    'shweta
    Public Event CloseBtnClick(ByVal sender As Object, ByVal e As EventArgs)
    Public Event AddBtnClick(ByVal sender As Object, ByVal e As EventArgs)
    Public Event ModifyBtnClick(ByVal sender As Object, ByVal e As EventArgs)

    Public Shadows Event LostFocus(ByVal sender As Object, ByVal e As EventArgs)
    Public IsGridLostFocus As Boolean = False
    Public isGridListVisible As Boolean = True
    Private _FireLostFocus As Boolean = True
    ' Public SelectedContent As String = ""

#Region " Property Procedures "

    Public Property ControlSearchText() As String
        Get
            Return _ControlSearchText
        End Get
        Set(ByVal value As String)
            _ControlSearchText = value
        End Set
    End Property
    Public Property SearchCount() As Integer
        Get
            Return _SearchCount
        End Get
        Set(ByVal value As Integer)
            _SearchCount = value
        End Set
    End Property

    Public Property ControlHeader() As String
        Get
            Return _ControlHeader
        End Get
        Set(ByVal value As String)
            _ControlHeader = value
        End Set
    End Property

    Public Property SelectedItems() As System.Collections.Generic.List(Of gloItem)
        Get
            Return _SelectedItems
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of gloItem))
            _SelectedItems = value
        End Set
    End Property

    Public Property TOSID() As Int64
        Get
            Return _TOSID
        End Get
        Set(ByVal value As Int64)
            _TOSID = value
        End Set
    End Property
    Public Property CPTID() As Int64
        Get
            Return _CPTID
        End Get
        Set(ByVal value As Int64)
            _CPTID = value
        End Set
    End Property
    Public Property ICD9ID() As Int64
        Get
            Return _ICD9ID
        End Get
        Set(ByVal value As Int64)
            _ICD9ID = value
        End Set
    End Property

    Public Property ControlType() As gloGridListControlType
        Get
            Return _ControlType
        End Get
        Set(ByVal value As gloGridListControlType)
            _ControlType = value
        End Set
    End Property

    Public Property ParentRowIndex() As Integer
        Get
            Return _parentRowIndex
        End Get
        Set(ByVal value As Integer)
            _parentRowIndex = value
        End Set
    End Property
    Public Property ParentColIndex() As Integer
        Get
            Return _parentColIndex
        End Get
        Set(ByVal value As Integer)
            _parentColIndex = value
        End Set
    End Property

    Public Property SelectedCPTCode() As String
        Get
            Return _SelectedCPTCode
        End Get
        Set(ByVal value As String)
            _SelectedCPTCode = value
        End Set
    End Property
    Public Property SelectedFacilityCode() As String
        Get
            Return _SelectedFacilityCode
        End Get
        Set(ByVal value As String)
            _SelectedFacilityCode = value
        End Set
    End Property

    Public Property DatabaseConnectionString() As String
        Get
            Return _DatabaseConnectionString
        End Get
        Set(ByVal value As String)
            _DatabaseConnectionString = value
        End Set
    End Property
    Public Property ClinicID() As Int64
        Get
            Return _ClinicID
        End Get
        Set(ByVal value As Int64)
            _ClinicID = value
        End Set
    End Property
    Public Property ShowHeader() As Boolean
        Get
            Return _ShowHeader
        End Get
        Set(ByVal value As Boolean)
            _ShowHeader = value
            Panel4.Visible = value
        End Set
    End Property
    Public Property ImmunizationName() As String
        Get
            Return _ImmunizationName
        End Get
        Set(ByVal value As String)
            _ImmunizationName = value
        End Set
    End Property
    Public Property LOINCOrCPTName() As String
        Get
            Return _LOINCOrCPTName
        End Get
        Set(ByVal value As String)
            _LOINCOrCPTName = value
        End Set
    End Property
#End Region

    Public Sub New()

        InitializeComponent()

    End Sub
    Public Sub New(ByVal ControlType As gloGridListControlType, ByVal IsMultiSelect As Boolean, ByVal ControlWidth As Int32, ByVal ParentRowIndex As Int32, ByVal ParentColIndex As Int32, ByVal sDataBaseConnectionString As String)

        InitializeComponent()


        _ControlType = ControlType
        _ismultiselect = IsMultiSelect
        Me.Width = ControlWidth
        _SelectedItems = New System.Collections.Generic.List(Of gloItem)
        Me.ParentColIndex = ParentColIndex
        Me.ParentRowIndex = ParentRowIndex
        _DatabaseConnectionString = sDataBaseConnectionString


    End Sub
    ''Mayuri
    Public Sub New(ByVal ControlType As gloGridListControlType, ByVal IsMultiSelect As Boolean, ByVal ControlWidth As Int32)

        InitializeComponent()
        _ismultiselect = IsMultiSelect
        Me.Width = ControlWidth
        _SelectedItems = New System.Collections.Generic.List(Of gloItem)
        '_DatabaseConnectionString = sDataBaseConnectionString
        _ControlType = ControlType

    End Sub
    ''
    'Public Sub New(ByVal ControlType As gloGridListControlType, ByVal Controlwidth As Int32, ByVal databaseConnectionStr As String)

    '    InitializeComponent()
    '    _ControlType = ControlType
    '    Me.Width = Controlwidth
    '    _SelectedItems = New System.Collections.Generic.List(Of gloItem)
    '    _DatabaseConnectionString = sDataBaseConnectionString

    'End Sub

    Public Shared Sub Style(ByVal FlexGrid As C1.Win.C1FlexGrid.C1FlexGrid, ByVal blnShowCellLabels As Boolean)
        FlexGrid.BackColor = System.Drawing.Color.FromArgb(240, 247, 255)
        FlexGrid.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        FlexGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row

        ' Normal Style
        FlexGrid.Styles.Normal.BackColor = System.Drawing.Color.FromArgb(240, 247, 255)
        FlexGrid.Styles.Normal.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221)
        FlexGrid.Styles.Normal.Font = gloGlobal.clsgloFont.gFont 'New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        FlexGrid.Styles.Normal.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125)

        ' Alternet Style
        FlexGrid.Styles.Alternate.BackColor = System.Drawing.Color.FromArgb(222, 231, 250)
        FlexGrid.Styles.Alternate.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221)
        FlexGrid.Styles.Alternate.Font = gloGlobal.clsgloFont.gFont 'New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0)
        FlexGrid.Styles.Alternate.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125)

        ' Fixed Style
        FlexGrid.Styles.Fixed.BackColor = System.Drawing.Color.FromArgb(86, 126, 211)
        FlexGrid.Styles.Fixed.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221)
        FlexGrid.Styles.Fixed.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0)
        FlexGrid.Styles.Fixed.ForeColor = System.Drawing.Color.White

        ' Heighlight Style
        FlexGrid.Styles.Highlight.BackColor = System.Drawing.Color.FromArgb(255, 197, 108)
        FlexGrid.Styles.Highlight.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221)
        FlexGrid.Styles.Highlight.Font = gloGlobal.clsgloFont.gFont 'New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0)
        FlexGrid.Styles.Highlight.ForeColor = System.Drawing.Color.Black

        ' Focus Style
        FlexGrid.Styles.Focus.BackColor = System.Drawing.Color.FromArgb(255, 224, 160)
        FlexGrid.Styles.Focus.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221)
        FlexGrid.Styles.Focus.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0)
        FlexGrid.Styles.Focus.ForeColor = System.Drawing.Color.Black

        ' EDITOR Style
        FlexGrid.Styles.Editor.BackColor = System.Drawing.Color.Beige
        FlexGrid.Styles.Editor.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221)
        FlexGrid.Styles.Editor.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125)
        FlexGrid.Styles.Editor.Font = gloGlobal.clsgloFont.gFont 'New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0)
        FlexGrid.Styles.Editor.ForeColor = System.Drawing.Color.Black

        ' Search Style
        FlexGrid.Styles.Search.BackColor = System.Drawing.Color.FromArgb(255, 197, 108)
        FlexGrid.Styles.Search.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221)
        FlexGrid.Styles.Search.Font = gloGlobal.clsgloFont.gFont 'New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0)
        FlexGrid.Styles.Search.ForeColor = System.Drawing.Color.White

        ' Frozen Style
        FlexGrid.Styles.Frozen.BackColor = System.Drawing.Color.FromArgb(255, 224, 160)
        FlexGrid.Styles.Frozen.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221)
        FlexGrid.Styles.Frozen.Font = gloGlobal.clsgloFont.gFont 'New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0)
        FlexGrid.Styles.Frozen.ForeColor = System.Drawing.Color.Black

        ' new Row Style
        FlexGrid.Styles.NewRow.BackColor = System.Drawing.Color.FromArgb(240, 247, 255)
        FlexGrid.Styles.NewRow.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221)
        FlexGrid.Styles.NewRow.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125)
        FlexGrid.Styles.NewRow.Font = gloGlobal.clsgloFont.gFont 'New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0)


        ' Empty Area Style
        FlexGrid.Styles.EmptyArea.BackColor = System.Drawing.Color.White
        FlexGrid.Styles.EmptyArea.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221)
        FlexGrid.Styles.EmptyArea.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125)

        FlexGrid.ShowCellLabels = blnShowCellLabels
    End Sub
    Dim ToolTip2 As System.Windows.Forms.ToolTip = Nothing
    Private Sub uctl_Treatment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Style(C1GridList, False)


        ToolTip2 = New System.Windows.Forms.ToolTip
        ToolTip2.SetToolTip(Me.btnCloseRefill, " Close ")
        ToolTip2.SetToolTip(Me.btnModify, " Modify ")
        ToolTip2.SetToolTip(Me.btnAdd, " Add ")
        ToolTip2.SetToolTip(Me.btnSelect, " Select ")


        Try
            'btnCloseRefill.Visible = True
            btnCloseRefill.BringToFront()
            lblHeader.Text = ControlHeader
            lblHeader.BringToFront()

            Application.DoEvents()
            FillControl("")

        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally

        End Try
    End Sub

#Region " Fill Control "

    Public Sub FillControl(ByVal SearchText As String)
        ' C1GridList.ScrollBars = ScrollBars.None
        Try
            SearchText = SearchText.Replace("'", "''").Replace("[", "").Replace("]", "")
            _ControlSearchText = SearchText

            If Not IsNothing(_ControlType) Then
                If _DatabaseConnectionString <> "" Then
                    Dim _sqlQuery As String = ""
                    Dim _sqlAlternetQuery As String = ""

                    Select Case _ControlType

                        Case gloGridListControlType.CPT
                            If True Then
                                If SearchText.Trim() <> "" Then
                                    _sqlQuery = "SELECT DISTINCT TOP 100 case when ISNULL(nCPTID,0) >= 0 then 0 end nCPTID,ISNULL(sCPTCode,'') AS Code,ISNULL(sDescription,'') AS Description FROM CPT_MST WHERE sCPTCode like '%" & SearchText & "%' OR sDescription like '%" & SearchText & "%'"
                                Else
                                    _sqlQuery = "SELECT DISTINCT TOP 100 case when ISNULL(nCPTID,0) >= 0 then 0 end nCPTID,ISNULL(sCPTCode,'') AS Code,ISNULL(sDescription,'') AS Description FROM CPT_MST "
                                End If
                            End If
                            Exit Select
                        Case gloGridListControlType.ClinicalInstruction
                            If True Then
                                If SearchText.Trim() <> "" Then
                                    _sqlQuery = " SELECT [sClinicalInstruction] as Instruction ,[sClinicalInstructionDtl]  as Description FROM [ClinicalInstruction_MST] where sClinicalInstruction like '%" & SearchText & "%' OR sClinicalInstructionDtl like '%" & SearchText & "%'"
                                Else
                                    _sqlQuery = "SELECT [sClinicalInstruction] as Instruction ,[sClinicalInstructionDtl]  as Description FROM [ClinicalInstruction_MST] "
                                End If
                            End If
                            Exit Select
                        Case gloGridListControlType.ICD9
                            If True Then
                                If SearchText.Trim() <> "" Then
                                    _sqlQuery = "SELECT DISTINCT TOP 100 case when ISNULL(nICD9ID,0) >= 0 then 0 end nICD9ID,ISNULL(sICD9Code,'') AS Code,ISNULL(sDescription,'') AS Description FROM ICD9 " & " WHERE isnull(nICDRevision,9)=9 AND (sICD9Code like '%" & SearchText & "%' OR sDescription like '%" & SearchText.Replace(".", "") & "%')"
                                Else
                                    _sqlQuery = "SELECT DISTINCT TOP 100 case when ISNULL(nICD9ID,0) >= 0 then 0 end nICD9ID,ISNULL(sICD9Code,'') AS Code,ISNULL(sDescription,'') AS Description FROM ICD9 WHERE isnull(nICDRevision,9)=9 "
                                End If

                            End If
                            Exit Select
                        Case gloGridListControlType.ICD10
                            If True Then
                                If SearchText.Trim() <> "" Then
                                    _sqlQuery = "SELECT DISTINCT TOP 100 case when ISNULL(nICD9ID,0) >= 0 then 0 end nICD10ID,ISNULL(sICD9Code,'') AS Code,ISNULL(sDescription,'') AS Description FROM ICD9 " & " WHERE nICDRevision=10 AND ( sICD9Code like '%" & SearchText & "%' OR sDescription like '%" & SearchText.Replace(".", "") & "%')"
                                Else
                                    _sqlQuery = "SELECT DISTINCT TOP 100 case when ISNULL(nICD9ID,0) >= 0 then 0 end nICD10ID,ISNULL(sICD9Code,'') AS Code,ISNULL(sDescription,'') AS Description FROM ICD9 WHERE nICDRevision=10"
                                End If

                            End If
                            Exit Select
                        Case gloGridListControlType.Modifier
                            If SearchText.Trim() <> "" Then
                                _sqlQuery = " SELECT DISTINCT ISNULL(Modifier_MST.nModifierID, 0) AS nModifierID, " & " ISNULL(Modifier_MST.sModifierCode, '') AS Code, " & " ISNULL(Modifier_MST.sDescription,'') AS Description " & " FROM Modifier_MST "
                            Else
                                _sqlQuery = " SELECT DISTINCT ISNULL(Modifier_MST.nModifierID, 0) AS nModifierID, " & " ISNULL(Modifier_MST.sModifierCode, '') AS Code, " & " ISNULL(Modifier_MST.sDescription,'') AS Description " & " FROM Modifier_MST "

                            End If

                            Exit Select
                            ''Added by Mayuri:20100218-Case No:#0001471
                        Case gloGridListControlType.Procedures
                            If SearchText.Trim() <> "" Then
                                _sqlQuery = "SELECT DISTINCT  0 as nCPTID , ISNULL(sCPTcode,'') AS sCPTcode, ISNULL(sCPTDescription,'') AS sCPTDescription FROM ExamICD9CPT WHERE sCPTCode like '%" & SearchText & "%' OR sCPTDescription like '%" & SearchText & "%' Order by sCPTcode "
                            Else
                                _sqlQuery = "SELECT DISTINCT  0 as nCPTID , ISNULL(sCPTcode,'') AS sCPTcode, ISNULL(sCPTDescription,'') AS sCPTDescription FROM ExamICD9CPT Order by sCPTcode"
                            End If
                            'Shweta
                        Case gloGridListControlType.ZIP
                            If SearchText.Trim() = "" Then
                                '_sqlQuery = "Select distinct TOP 100 isnull(Zip,0) As Zip, City,nID,county,ST from CSZ_MST where zip like '%" & SearchText.Trim & "%'"
                                _sqlQuery = "SELECT DISTINCT TOP 100 ISNULL(ZIP,0) AS Zip, ISNULL(City,'') as City, nID, ISNULL(County,'') as County, ISNULL(ST,'') AS ST, ISNULL(AreaCode,0) AS AreaCode FROM CSZ_MST WHERE Zip LIKE '" & SearchText.Trim & "%'"
                            Else
                                '_sqlQuery = "Select distinct TOP 100 isnull(Zip,0) As Zip, City,nID,county,ST from CSZ_MST where zip like '%" & SearchText.Trim & "%'"
                                _sqlQuery = "SELECT DISTINCT TOP 100 ISNULL(ZIP,0) AS Zip, ISNULL(City,'') as City, nID, ISNULL(County,'') as County, ISNULL(ST,'') AS ST, ISNULL(AreaCode,0) AS AreaCode FROM CSZ_MST WHERE Zip LIKE '" & SearchText.Trim & "%'"
                            End If
                        Case gloGridListControlType.Cvx
                            If SearchText.Trim() <> "" Then
                                _sqlQuery = " SELECT Vaccine FROM (SELECT  LTRIM(RTRIM(isnull(CvxCode,''))) + ' - ' + LTRIM(RTRIM(isnull(ShortDescription,''))) as [Vaccine] FROM dbo.IMCvx WHERE LTRIM(RTRIM(isnull(CvxCode,''))) + ' - ' + LTRIM(RTRIM(isnull(ShortDescription,''))) like '%" & SearchText & "%'  UNION select LTRIM(RTRIM(isnull(sCode,''))) + ' - ' + LTRIM(RTRIM(isnull(sDescription,''))) as Vaccine from  Category_MST where (sCategoryType ='Vaccine' And (LTRIM(RTRIM(isnull(sCode,''))) + ' - ' + LTRIM(RTRIM(isnull(sDescription,''))) like '%" & SearchText & "%')))tmp ORDER BY dbo.IM_SeparateCodeAndDescription (vaccine,'-','description')  "
                            Else
                                _sqlQuery = " SELECT Vaccine FROM (SELECT  LTRIM(RTRIM(isnull(CvxCode,''))) + ' - ' +  LTRIM(RTRIM(isnull(ShortDescription,''))) as [Vaccine] FROM dbo.IMCvx  UNION select LTRIM(RTRIM(isnull(sCode,''))) + ' - ' + LTRIM(RTRIM(isnull(sDescription,''))) as [Vaccine] from  Category_MST where sCategoryType ='Vaccine')tmp order by dbo.IM_SeparateCodeAndDescription (vaccine,'-','description')  "

                            End If
                        Case gloGridListControlType.LOINCCode
                            If SearchText.Trim() <> "" Then
                                _sqlQuery = " SELECT LOINCCode + ' : ' +LOINCLongName AS [LOINC Order Code] FROM LOINC_MST WHERE LOINCCode LIKE '%" & SearchText.Trim & "%' OR LOINCLongName LIKE '%" & SearchText.Trim & "%'"  ''% added at first to get data using like clause
                            Else
                                _sqlQuery = " SELECT LOINCCode + ' : ' + LOINCLongName AS [LOINC Order Code] FROM LOINC_MST"

                            End If
                        Case gloGridListControlType.LabsCPT
                            If SearchText.Trim() <> "" Then
                                _sqlQuery = "SELECT DISTINCT Top 100 ISNULL(sCPTCode,'') + ' : ' +ISNULL(sDescription,'') AS Description FROM CPT_MST WHERE sCPTCode like '%" & SearchText & "%' OR sDescription like '%" & SearchText & "%'"
                            Else
                                _sqlQuery = "SELECT DISTINCT Top 100 ISNULL(sCPTCode,'') +' : '+ISNULL(sDescription,'') AS Description FROM CPT_MST "
                            End If
                            '
                        Case gloGridListControlType.Mvx
                            If SearchText.Trim() <> "" Then
                                _sqlQuery = " SELECT LTRIM(RTRIM(isnull(MvxCode,''))) + ' - ' + LTRIM(RTRIM(isnull(ManufacturerName,''))) as [Manufacturer] FROM dbo.IMMvx  WHERE LTRIM(RTRIM(isnull(MvxCode,''))) + ' - ' + LTRIM(RTRIM(isnull(ManufacturerName,''))) like '%" & SearchText & "%' UNION select LTRIM(RTRIM(isnull(sCode,''))) + ' - ' + LTRIM(RTRIM(isnull(sDescription,''))) as [Manufacturer] from  Category_MST where (sCategoryType ='Manufacturer' And (LTRIM(RTRIM(isnull(sCode,''))) + ' - ' + LTRIM(RTRIM(isnull(sDescription,'')))) like '%" & SearchText & "%') order by [Manufacturer]  "
                            Else
                                _sqlQuery = " SELECT LTRIM(RTRIM(isnull(MvxCode,''))) + ' - ' + LTRIM(RTRIM(isnull(ManufacturerName,''))) as [Manufacturer] FROM dbo.IMMvx  UNION select LTRIM(RTRIM(isnull(sCode,''))) + ' - ' + LTRIM(RTRIM(isnull(sDescription,''))) as [Manufacturer] from  Category_MST where sCategoryType ='Manufacturer' order by [Manufacturer]  "
                            End If
                        Case gloGridListControlType.TradeName
                            If SearchText.Trim <> "" Then
                                _sqlQuery = " SELECT LTRIM(RTRIM([CdcProductName])) as [Trade Name] FROM dbo.IMTradeName  WHERE LTRIM(RTRIM(isnull([CdcProductName],''))) like '%" & SearchText & "%' UNION  select  LTRIM(RTRIM(isnull(sDescription,''))) as [Trade Name] from  Category_MST where (sCategoryType ='TradeName' AND  LTRIM(RTRIM(isnull(sDescription,''))) like '%" & SearchText & "%') order by [Trade Name] "
                            Else
                                _sqlQuery = "SELECT LTRIM(RTRIM([CdcProductName])) as [Trade Name] FROM dbo.IMTradeName UNION  select  LTRIM(RTRIM(isnull(sDescription,''))) as [Trade Name] from Category_MST where sCategoryType ='TradeName' order by [Trade Name] "
                            End If
                        Case Else
                            Exit Select
                    End Select

                    If _sqlQuery.Trim() <> "" Then
                        If (IsNothing(_dtList) = False) Then
                            _dtList.Dispose()
                            _dtList = Nothing
                        End If
                        _dtList = New DataTable
                        Dim adpt As New System.Data.SqlClient.SqlDataAdapter
                        Dim Cmd As System.Data.SqlClient.SqlCommand = Nothing
                        Dim Conn As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(_DatabaseConnectionString)
                        If _ControlType = gloGridListControlType.ICD9 Then  '' For ICD9

                            Cmd = New System.Data.SqlClient.SqlCommand("gsp_Diagnosis_Search", Conn)

                            Dim objParam As System.Data.SqlClient.SqlParameter
                            objParam = Cmd.Parameters.Add("@SearchString", SqlDbType.Text)
                            objParam.Direction = ParameterDirection.Input
                            objParam.Value = SearchText

                            Dim objParam1 As System.Data.SqlClient.SqlParameter
                            objParam1 = Cmd.Parameters.Add("@nICDRevision", SqlDbType.Int)
                            objParam1.Direction = ParameterDirection.Input
                            objParam1.Value = gloGlobal.gloICD.CodeRevision.ICD9


                            Cmd.CommandType = CommandType.StoredProcedure
                            adpt.SelectCommand = Cmd
                            objParam = Nothing
                            objParam1 = Nothing
                            adpt.Fill(_dtList)

                            'Remove Unneccessory Columns
                            If _dtList.Columns.Count > 3 Then
                                For i As Integer = _dtList.Columns.Count - 1 To 3 Step -1
                                    _dtList.Columns.RemoveAt(i)
                                Next
                            End If
                            'Rename Column Name
                            For j As Integer = 0 To _dtList.Columns.Count - 1 Step 1
                                If _dtList.Columns(j).ColumnName = "sICD9Code" Then
                                    _dtList.Columns(j).ColumnName = "Code"
                                ElseIf _dtList.Columns(j).ColumnName = "sDescription" Then
                                    _dtList.Columns(j).ColumnName = "Description"
                                End If
                            Next
                        ElseIf ControlType = gloGridListControlType.ICD10 Then  '' For ICD9

                            Cmd = New System.Data.SqlClient.SqlCommand("gsp_Diagnosis_Search", Conn)
                            Dim objParam As System.Data.SqlClient.SqlParameter
                            objParam = Cmd.Parameters.Add("@SearchString", SqlDbType.Text)
                            objParam.Direction = ParameterDirection.Input
                            objParam.Value = SearchText

                            Dim objParam1 As System.Data.SqlClient.SqlParameter
                            objParam1 = Cmd.Parameters.Add("@nICDRevision", SqlDbType.Int)
                            objParam1.Direction = ParameterDirection.Input
                            objParam1.Value = gloGlobal.gloICD.CodeRevision.ICD10

                            Cmd.CommandType = CommandType.StoredProcedure
                            adpt.SelectCommand = Cmd
                            objParam = Nothing
                            objParam1 = Nothing
                            adpt.Fill(_dtList)
                            'Remove Unneccessory Columns
                            If _dtList.Columns.Count > 3 Then
                                For i As Integer = _dtList.Columns.Count - 1 To 3 Step -1
                                    _dtList.Columns.RemoveAt(i)
                                Next
                            End If
                            'Rename Column Name
                            For j As Integer = 0 To _dtList.Columns.Count - 1 Step 1
                                If _dtList.Columns(j).ColumnName = "sICD9Code" Then
                                    _dtList.Columns(j).ColumnName = "Code"
                                ElseIf _dtList.Columns(j).ColumnName = "sDescription" Then
                                    _dtList.Columns(j).ColumnName = "Description"
                                End If
                            Next
                        Else
                            Retrive_Query(_sqlQuery, _dtList)
                        End If
                        If Not IsNothing(adpt) Then
                            adpt.Dispose()
                            adpt = Nothing
                        End If
                       
                        If Not IsNothing(Cmd) Then
                            Cmd.Parameters.Clear()
                            Cmd.Dispose()
                            Cmd = Nothing
                        End If

                        If Not IsNothing(Conn) Then
                            Conn.Dispose()
                            Conn = Nothing
                        End If



                        If _dtList IsNot Nothing Then
                            'Code commented on 20090129 by Sagar to empty inner list
                            'if (_dtList.Rows.Count > 0)
                            '{
                            _dv = _dtList.DefaultView
                            C1GridList.DataSource = _dv
                            If C1GridList IsNot Nothing AndAlso C1GridList.Rows.Count > 0 Then
                                If _ControlType = gloGridListControlType.Cvx Or _ControlType = gloGridListControlType.Mvx Or _ControlType = gloGridListControlType.TradeName Then
                                    If IsRecordExist(_ImmunizationName) Then
                                        'To refresh list after performing add/update/delete operation
                                        Dim rowIndex As Int64
                                        rowIndex = C1GridList.FindRow(_ImmunizationName, 1, 0, False, True, False)
                                        '_CurrentSelectedRow = rowIndex
                                        C1GridList.Select(rowIndex, 0, True)
                                    End If
                                ElseIf _ControlType = gloGridListControlType.ClinicalInstruction Then
                                    C1GridList.Select()
                                ElseIf _ControlType = gloGridListControlType.LabsCPT Or _ControlType = gloGridListControlType.LOINCCode Then
                                    If IsLabsRecordExist(_LOINCOrCPTName) Then
                                        'To refresh list after performing add/update/delete operation
                                        Dim rowIndex As Int64
                                        rowIndex = C1GridList.FindRow(_LOINCOrCPTName, 1, 0, False, True, False)
                                        '_CurrentSelectedRow = rowIndex
                                        C1GridList.Select(rowIndex, 0, True)
                                    End If

                                End If
                            End If
                            '}
                            'else
                            '{ _dv = null; }
                            Application.DoEvents()
                            DesignGridList()
                        Else
                            _dv = Nothing
                        End If


                    End If
                End If

            End If
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            '  C1GridList.ScrollBars = ScrollBars.Both
        End Try
    End Sub

#End Region
    Public Function IsRecordExist(ByVal _ImmunizationName As String) As Boolean
        Try
            Dim i As Int16

            If _ControlType = gloGridListControlType.Cvx Then
                If IsNothing(_dv) = False Then
                    If _dv.Table.Rows.Count > 0 Then
                        For i = 0 To _dv.Table.Rows.Count - 1
                            If _ImmunizationName.Trim = _dv.Table.Rows(i)("Vaccine").ToString().Trim Then
                                Return True
                                'Else
                                '    Return False
                            End If
                        Next
                    End If
                End If
            End If
            If _ControlType = gloGridListControlType.Mvx Then
                If IsNothing(_dv) = False Then
                    If _dv.Table.Rows.Count > 0 Then
                        For i = 0 To _dv.Table.Rows.Count - 1
                            If _ImmunizationName.Trim = _dv.Table.Rows(i)("Manufacturer").ToString().Trim Then
                                Return True
                                'Else
                                '    Return False
                            End If
                        Next
                    End If
                End If
            End If
            If _ControlType = gloGridListControlType.TradeName Then
                If IsNothing(_dv) = False Then
                    If _dv.Table.Rows.Count > 0 Then
                        For i = 0 To _dv.Table.Rows.Count - 1
                            If _ImmunizationName.Trim = _dv.Table.Rows(i)("Trade Name").ToString().Trim Then
                                Return True
                                'Else
                                '    Return False
                            End If
                        Next
                    End If
                End If
            End If

            Return False
        Catch ex As Exception
            Return False
        Finally
        End Try
    End Function
    Public Function IsLabsRecordExist(ByVal _LOINCOrCPTName As String) As Boolean
        Try
            Dim i As Int16
            If _ControlType = gloGridListControlType.LOINCCode Then
                If IsNothing(_dv) = False Then
                    If _dv.Table.Rows.Count > 0 Then
                        For i = 0 To _dv.Table.Rows.Count - 1
                            If _LOINCOrCPTName.Trim = _dv.Table.Rows(i)("LOINC Order Code").ToString().Trim Then
                                Return True
                                'Else
                                '    Return False
                            End If
                        Next
                    End If
                End If
            End If
            If _ControlType = gloGridListControlType.LabsCPT Then
                If IsNothing(_dv) = False Then
                    If _dv.Table.Rows.Count > 0 Then
                        For i = 0 To _dv.Table.Rows.Count - 1
                            If _LOINCOrCPTName.Trim = _dv.Table.Rows(i)("Description").ToString().Trim Then
                                Return True
                                'Else
                                '    Return False
                            End If
                        Next
                    End If
                End If
            End If
            Return False
        Catch ex As Exception
            Return False
        Finally
        End Try
    End Function
#Region "Retrieve Result"
    Private Sub Retrive_Query(ByVal strQuery As String, ByRef dt As DataTable)
        Dim conn As System.Data.SqlClient.SqlConnection = Nothing
        Dim cmd As System.Data.SqlClient.SqlCommand = Nothing
        Dim sqladpt As New SqlClient.SqlDataAdapter
        Try
            conn = New SqlClient.SqlConnection(_DatabaseConnectionString)
            cmd = New SqlClient.SqlCommand(strQuery, conn)
            'sqladpt = New SqlClient.SqlDataAdapter

            sqladpt.SelectCommand = cmd
            If (IsNothing(dt)) Then
                dt = New DataTable
            End If
            sqladpt.Fill(dt)
        Catch ex As Exception

        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
            End If
            If (IsNothing(sqladpt) = False) Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If

        End Try
    End Sub
#End Region

#Region " Design Grid List "


    Private Sub DesignGridList()
        Dim _Width As Integer = Me.Width - 3
        Try
            C1GridList.AllowEditing = False
            C1GridList.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
            C1GridList.ShowCellLabels = True
            Application.DoEvents()
            Select Case _ControlType

                Case gloGridListControlType.CPT
                    If True Then
                        'nCPTID,sCPTCode,sCPTDesc 
                        C1GridList.Cols("nCPTID").Visible = False
                        C1GridList.Cols("Code").Visible = True
                        C1GridList.Cols("Description").Visible = True

                        C1GridList.Cols("nCPTID").Width = 0
                        C1GridList.Cols("Code").Width = Convert.ToInt32(_Width * 0.2R)
                        C1GridList.Cols("Description").Width = Convert.ToInt32(_Width * 0.8R)
                    End If
                    Exit Select
                Case gloGridListControlType.ICD9
                    If True Then
                        'nICD9ID, sICD9Code, sDescription 
                        C1GridList.Cols("nICD9ID").Visible = False
                        C1GridList.Cols("Code").Visible = True
                        C1GridList.Cols("Description").Visible = True

                        C1GridList.Cols("nICD9ID").Width = 0
                        C1GridList.Cols("Code").Width = Convert.ToInt32(_Width * 0.2R)
                        C1GridList.Cols("Description").Width = Convert.ToInt32(_Width * 0.8R)
                    End If
                    Exit Select
                Case gloGridListControlType.ICD10
                    If True Then
                        'nICD9ID, sICD9Code, sDescription 
                        'Code Changes For bug No. 70462:: Exam - DXCPT- Application is not adding the Diagnosis code if we search & press enter
                        C1GridList.Cols("nICD9ID").Visible = False
                        C1GridList.Cols("Code").Visible = True
                        C1GridList.Cols("Description").Visible = True

                        C1GridList.Cols("nICD10ID").Width = 0
                        C1GridList.Cols("Code").Width = Convert.ToInt32(_Width * 0.2R)
                        C1GridList.Cols("Description").Width = Convert.ToInt32(_Width * 0.8R)
                    End If
                    Exit Select
                Case gloGridListControlType.Modifier
                    If True Then
                        'nModifierID,sModifierCode,sDescription
                        C1GridList.Cols("nModifierID").Visible = False
                        C1GridList.Cols("sModifierCode").Visible = True
                        C1GridList.Cols("sDescription").Visible = True

                        C1GridList.Cols("nModifierID").Width = 0
                        C1GridList.Cols("sModifierCode").Width = Convert.ToInt32(_Width * 0.2R)
                        C1GridList.Cols("sDescription").Width = Convert.ToInt32(_Width * 0.8R)
                    End If
                    Exit Select
                Case gloGridListControlType.ZIP
                    If True Then

                        C1GridList.Cols("Zip").TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

                        C1GridList.Cols("Zip").Visible = True
                        C1GridList.Cols("City").Visible = True
                        C1GridList.Cols("nID").Visible = False
                        C1GridList.Cols("County").Visible = False
                        C1GridList.Cols("ST").Visible = False
                        C1GridList.Cols("AreaCode").Visible = False

                        C1GridList.Cols("Zip").Width = Convert.ToInt32(_Width * 0.2R)
                        C1GridList.Cols("City").Width = Convert.ToInt32(_Width * 0.7R)
                        C1GridList.Cols("nID").Width = 0
                        C1GridList.Cols("County").Width = 0
                        C1GridList.Cols("ST").Width = 0
                        C1GridList.Cols("AreaCode").Width = 0
                        'C1GridList.Cols("sDescription").Width = Convert.ToInt32(_Width * 0.8R)
                        C1GridList.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
                    End If
                    Exit Select
                    ''Added by Mayuri:20100217-Case No:#
                Case gloGridListControlType.Procedures
                    If True Then
                        'nCPTID,sCPTCode,sCPTDesc 
                        C1GridList.Cols("nCPTID").Visible = False
                        C1GridList.Cols("sCPTCode").Visible = True
                        C1GridList.Cols("sCPTDescription").Visible = True

                        C1GridList.Cols("nCPTID").Width = 0
                        C1GridList.Cols("sCPTCode").Width = Convert.ToInt32(_Width * 0.2R)
                        C1GridList.Cols("sCPTDescription").Width = Convert.ToInt32(_Width * 0.8R)
                        C1GridList.Cols("sCPTCode").Caption = "Code"
                        C1GridList.Cols("sCPTDescription").Caption = "Description"

                    End If
                    Exit Select
                Case gloGridListControlType.Cvx
                    C1GridList.Cols("Vaccine").Visible = True
                    C1GridList.Cols("Vaccine").Width = Convert.ToInt32(_Width * 0.8R)
                    Exit Select
                Case gloGridListControlType.LOINCCode
                    C1GridList.Cols("LOINC Order Code").Visible = True
                    C1GridList.Cols("LOINC Order Code").Width = Convert.ToInt32(_Width * 0.8R)
                    Exit Select
                Case gloGridListControlType.LabsCPT
                    C1GridList.Cols("Description").Visible = True
                    C1GridList.Cols("Description").Width = Convert.ToInt32(_Width * 0.8R)
                    Exit Select
                Case gloGridListControlType.Mvx
                    C1GridList.Cols("Manufacturer").Visible = True
                    C1GridList.Cols("Manufacturer").Width = Convert.ToInt32(_Width * 0.8R)
                    Exit Select
                Case gloGridListControlType.ClinicalInstruction
                    C1GridList.Cols("Instruction").Visible = True
                    C1GridList.Cols("Description").Visible = True
                    C1GridList.Cols("Instruction").Width = Convert.ToInt32(_Width * 0.2R)
                    C1GridList.Cols("Description").Width = Convert.ToInt32(_Width * 0.8R)
                    Exit Select
                Case gloGridListControlType.TradeName
                    C1GridList.Cols("Trade Name").Visible = True
                    C1GridList.Cols("Trade Name").Width = Convert.ToInt32(_Width * 0.8R)
                    Exit Select
                Case Else
                    Exit Select
            End Select
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally

        End Try
    End Sub
#End Region


#Region " Search Functionality "

    Private Sub txtListSearch_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally

        End Try
    End Sub

    Public Sub Search(ByVal SearchText As String, ByVal SearchCol As SearchColumn)

        Try

            Dim _dvTemp As DataView = DirectCast(C1GridList.DataSource, DataView)

            If (IsNothing(_dvTemp) = False) Then
                SearchText = SearchText.Replace("'", "''").Replace("[", "").Replace("]", "")
                Dim _colIndex As Integer = 0
                Select Case _ControlType

                    Case gloGridListControlType.CPT, gloGridListControlType.ICD9, gloGridListControlType.Modifier, gloGridListControlType.POS, gloGridListControlType.TOS, gloGridListControlType.Procedures
                        If True Then
                            If SearchCol = SearchColumn.Code Then
                                _colIndex = 1
                            ElseIf SearchCol = SearchColumn.Description Then
                                _colIndex = 2
                            Else
                                _colIndex = 1
                            End If

                            SearchText.Replace("'", "")

                            If SearchText.StartsWith("%") = True Or SearchText.StartsWith("*") = True Then

                                _dvTemp.RowFilter = (_dvTemp.Table.Columns(_colIndex).ColumnName & " Like '%") + SearchText & "%'"
                            Else
                                _dvTemp.RowFilter = (_dvTemp.Table.Columns(_colIndex).ColumnName & " Like '") + SearchText & "%'"
                            End If
                            C1GridList.DataSource = _dvTemp
                            DesignGridList()
                        End If
                        Exit Select
                    Case Else
                        Exit Select


                End Select
            End If
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            If C1GridList.DataSource IsNot Nothing Then
                Me.SearchCount = DirectCast(C1GridList.DataSource, DataView).Count
            End If

        End Try
    End Sub

    Public Sub InStringSearch(ByVal SearchText As String)
        Dim _colCodeIndex As Integer = 0
        Dim _colDescriptionIndex As Integer = 0
        Dim _searchStringArray As String() = Nothing
        Dim _searchString As String = ""
        Dim _dvTemp As DataView = Nothing
        Try
            If (IsNothing(_dtList) = False) Then
                _dvTemp = _dtList.DefaultView
                '(DataView)c1GridList.DataSource;
                SearchText = SearchText.Replace("'", "''").Replace("[", "").Replace("(", "").Replace("]", "").Replace(")", "")

                Select Case _ControlType

                    Case gloGridListControlType.CPT, gloGridListControlType.ICD9, gloGridListControlType.Modifier, gloGridListControlType.Procedures
                        If True Then
                            _colCodeIndex = 1
                            _colDescriptionIndex = 2
                            _searchStringArray = SearchText.Split(","c)


                        End If
                        Exit Select
                    Case Else
                        Exit Select
                End Select

                If SearchText <> "" Then
                    If _searchStringArray.Length = 1 Then
                        _searchString = _searchStringArray(0)
                        _dvTemp.RowFilter = (_dvTemp.Table.Columns(_colDescriptionIndex).ColumnName & " Like '%") + _searchString & "%' "
                    Else
                        Dim dtTemp As DataTable = Nothing
                        For i As Integer = 0 To _searchStringArray.Length - 1
                            _searchString = _searchStringArray(i)
                            If _searchString.Trim() <> "" Then
                                'If i = 0 Then
                                '    dtTemp = _dvTemp.ToTable()
                                '    dvNext = dtTemp.DefaultView
                                'Else
                                '    dtTemp = dvNext.ToTable()
                                '    dvNext = dtTemp.DefaultView
                                'End If
                                If i = 0 Then
                                    dtTemp = _dvTemp.ToTable()
                                    dvNext = dtTemp.Copy().DefaultView
                                    dtTemp.Dispose()
                                    dtTemp = Nothing
                                Else
                                    dtTemp = dvNext.ToTable()
                                    dvNext = dtTemp.Copy().DefaultView
                                    dtTemp.Dispose()
                                    dtTemp = Nothing
                                End If
                                dvNext.RowFilter = (dvNext.Table.Columns(_colDescriptionIndex).ColumnName & " Like '%") + _searchString & "%' "
                            End If
                        Next
                        If Not IsNothing(dtTemp) Then ''disposed as per glo Code optimizer tool in 8000 version
                            dtTemp.Dispose()
                            dtTemp = Nothing
                        End If
                        If _searchString <> "" Then
                            If _searchStringArray.Length > 1 Then
                                _dvTemp = dvNext
                            End If
                        End If

                    End If
                Else
                    _dvTemp.RowFilter = ""
                    _dvTemp.RowStateFilter = DataViewRowState.OriginalRows
                End If
            End If
            C1GridList.DataSource = _dvTemp



            DesignGridList()
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            If C1GridList.DataSource IsNot Nothing Then
                Me.SearchCount = DirectCast(C1GridList.DataSource, DataView).Count
            End If

        End Try
    End Sub

    Public Sub AdvanceSearch(ByVal SearchText As String)
        Dim _colCodeIndex As Integer = 0
        Dim _colDescriptionIndex As Integer = 0
        Dim _searchStringArray As String() = Nothing
        Dim _searchString As String = ""
        Dim _dvTemp As DataView = Nothing
        Try
            If (IsNothing(_dtList) = False) Then
                _dvTemp = _dtList.DefaultView
                '(DataView)c1GridList.DataSource;
                SearchText = SearchText.Replace("'", "''").Replace("[", "").Replace("(", "").Replace("]", "").Replace(")", "")
                _ControlSearchText = SearchText

                Select Case _ControlType

                    Case gloGridListControlType.CPT, gloGridListControlType.ICD9, gloGridListControlType.Modifier, gloGridListControlType.Procedures
                        If True Then
                            _colCodeIndex = 1
                            _colDescriptionIndex = 2
                            _searchStringArray = SearchText.Split(","c)


                        End If
                        Exit Select
                    Case Else
                        Exit Select
                End Select

                If SearchText <> "" Then
                    If _searchStringArray.Length = 1 Then
                        _searchString = _searchStringArray(0)
                        If _ControlType = gloGridListControlType.Providers Then
                            _dvTemp.RowFilter = (_dvTemp.Table.Columns(_colDescriptionIndex).ColumnName & " Like '%") + _searchString & "%' "
                        Else
                            _dvTemp.RowFilter = (((_dvTemp.Table.Columns(_colDescriptionIndex).ColumnName & " Like '%") + _searchString & "%' OR ") + _dvTemp.Table.Columns(_colCodeIndex).ColumnName & " Like '%") + _searchString & "%' "
                        End If
                    Else
                        Dim dtTemp As DataTable = Nothing
                        For i As Integer = 0 To _searchStringArray.Length - 1
                            _searchString = _searchStringArray(i)
                            If _searchString.Trim() <> "" Then
                                'If i = 0 Then
                                '    dtTemp = _dvTemp.ToTable()
                                '    dvNext = dtTemp.DefaultView
                                'Else
                                '    dtTemp = dvNext.ToTable()
                                '    dvNext = dtTemp.DefaultView
                                'End If
                                If i = 0 Then
                                    dtTemp = _dvTemp.ToTable()
                                    dvNext = dtTemp.Copy().DefaultView
                                    dtTemp.Dispose()
                                    dtTemp = Nothing
                                Else
                                    dtTemp = dvNext.ToTable()
                                    dvNext = dtTemp.Copy().DefaultView
                                    dtTemp.Dispose()
                                    dtTemp = Nothing
                                End If
                                If _ControlType = gloGridListControlType.Providers Then
                                    dvNext.RowFilter = (dvNext.Table.Columns(_colDescriptionIndex).ColumnName & " Like '%") + _searchString & "%' "
                                Else
                                    dvNext.RowFilter = (((dvNext.Table.Columns(_colDescriptionIndex).ColumnName & " Like '%") + _searchString & "%' OR ") + dvNext.Table.Columns(_colCodeIndex).ColumnName & " Like '%") + _searchString & "%' "
                                End If
                            End If
                        Next
                        If (IsNothing(dtTemp) = False) Then
                            dtTemp.Dispose()
                            dtTemp = Nothing
                        End If
                        If _searchString <> "" Then
                            If _searchStringArray.Length > 1 Then
                                _dvTemp = dvNext
                            End If
                        End If

                    End If
                Else
                    _dvTemp.RowFilter = ""
                    _dvTemp.RowStateFilter = DataViewRowState.OriginalRows
                End If
            End If

            C1GridList.DataSource = _dvTemp


            DesignGridList()
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            If C1GridList.DataSource IsNot Nothing Then
                Me.SearchCount = DirectCast(C1GridList.DataSource, DataView).Count
            End If

        End Try
    End Sub

#End Region

#Region " Grid Events "

    Private Sub C1GridList_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1GridList.LostFocus
        If _FireLostFocus = True Then RaiseEvent LostFocus(sender, e)
    End Sub

    Private Sub c1GridList_MouseDoubleClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles C1GridList.MouseDoubleClick

        Dim rowIndex As Integer = 0
        Dim _id As Int64 = 0
        Dim _code As String = ""
        Dim _desc As String = ""
        Dim _SnoCode As String = ""
        Dim _SnoDesc As String = ""
        Dim blnIsSingleSnoMed As Boolean

        Try
            If C1GridList IsNot Nothing Then
                If C1GridList.Rows.Count > 0 Then
                    rowIndex = C1GridList.RowSel
                    Select Case _ControlType

                        Case gloGridListControlType.CPT, gloGridListControlType.ICD9, gloGridListControlType.ICD10, gloGridListControlType.Modifier, gloGridListControlType.ZIP, gloGridListControlType.Procedures, gloGridListControlType.Cvx, gloGridListControlType.Mvx, gloGridListControlType.TradeName, gloGridListControlType.LOINCCode, gloGridListControlType.LabsCPT
                            If True Then
                                If _ControlType = gloGridListControlType.LabsCPT Or _ControlType = gloGridListControlType.LOINCCode Or _ControlType = gloGridListControlType.Cvx Or _ControlType = gloGridListControlType.Mvx Or _ControlType = gloGridListControlType.TradeName Then
                                    _desc = Convert.ToString(C1GridList.GetData(rowIndex, 0))
                                Else
                                    _id = Convert.ToInt64(C1GridList.GetData(rowIndex, 0))
                                    _code = Convert.ToString(C1GridList.GetData(rowIndex, 1))
                                    _desc = Convert.ToString(C1GridList.GetData(rowIndex, 2))
                                    If _ControlType = gloGridListControlType.ICD9 Or _ControlType = gloGridListControlType.ICD10 Then
                                        Dim objclsSnomed As New clsSnomedIcdMap
                                        Dim frm As FrmSelectProblem

                                        Dim ICDRevision As String = ""

                                        If _ControlType = gloGridListControlType.ICD9 Then
                                            ICDRevision = "9"
                                        ElseIf _ControlType = gloGridListControlType.ICD10 Then
                                            ICDRevision = "10"
                                        End If


                                        Dim dtProblemSnomed As DataTable = objclsSnomed.Get_DefaultSnomedForICD(_code, _desc, ICDRevision, DatabaseConnectionString)
                                        If Not IsNothing(dtProblemSnomed) Then
                                            If dtProblemSnomed.Rows.Count > 1 Then
                                                If objclsSnomed.IsSnomedMandatory(DatabaseConnectionString) Then ''condition shiftedfor bugid 86608
                                                    gstrSMDBConnstr = GetHybridConnectionString(gstrSMDBServerName, gstrSMDBDatabaseName, gblnSMDBAuthen, gstrSMDBUserID, gstrSMDBPassWord)
                                                    frm = New gloSnoMed.FrmSelectProblem("Select Problem", gstrSMDBConnstr, DatabaseConnectionString)
                                                    '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
                                                    blnIsSingleSnoMed = False
                                                    frm.blnIsProblem = True
                                                    If ICDRevision = 9 Then
                                                        frm.strCodeSystem = "ICD9"
                                                    ElseIf ICDRevision = 10 Then
                                                        frm.strCodeSystem = "ICD10"
                                                    End If
                                                    frm.txtSMSearch.Text = Convert.ToString(_code)
                                                    frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                                                    _SnoCode = Convert.ToString(frm.strSelectedConceptID)
                                                    _SnoDesc = Convert.ToString(frm.strSelectedDescription)
                                                    frm.Dispose()
                                                    frm = Nothing
                                                End If
                                            Else
                                                '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
                                                blnIsSingleSnoMed = True
                                                If dtProblemSnomed.Rows.Count > 0 Then
                                                    _SnoCode = Convert.ToString(dtProblemSnomed.Rows(0)("CONCEPTID"))
                                                    _SnoDesc = Convert.ToString(dtProblemSnomed.Rows(0)("TermDescription"))
                                                End If
                                            End If
                                            dtProblemSnomed.Dispose()
                                            dtProblemSnomed = Nothing
                                        End If
                                   
                                        ICDRevision = Nothing
                                        If Not IsNothing(objclsSnomed) Then
                                            objclsSnomed = Nothing
                                        End If

                                    End If
                                End If
                                IsGridLostFocus = True
                            End If

                        Case gloGridListControlType.ClinicalInstruction
                            _code = Convert.ToString(C1GridList.GetData(rowIndex, 0))
                            _desc = Convert.ToString(C1GridList.GetData(rowIndex, 1))


                            Exit Select

                    End Select
                End If
            End If

            Dim oListItem As New gloItem()
            oListItem.ID = _id
            oListItem.Code = _code
            oListItem.Description = _desc
            '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
            oListItem.IsSnoMedOneToOneMapping = blnIsSingleSnoMed



            If _ControlType = gloGridListControlType.ICD9 Or _ControlType = gloGridListControlType.ICD10 Then
                Dim oListSubItem As New gloSubItem
                oListSubItem.Code = _SnoCode
                oListSubItem.Description = _SnoDesc
                '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
                oListSubItem.IsSnoMedOneToOneMapping = blnIsSingleSnoMed
                oListItem.SubItems.Add(oListSubItem)

                oListSubItem.Dispose()
                oListSubItem = Nothing
            End If


            _SelectedItems.Clear()

            _SelectedItems.Add(oListItem)
        Catch ex As Exception

            'gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            _FireLostFocus = False
            RaiseEvent ItemSelected(Nothing, Nothing)
        End Try
    End Sub

    Private Sub c1GridList_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles C1GridList.KeyDown

        Try
            If e.KeyCode = Keys.Enter Then
                c1GridList_MouseDoubleClick(Nothing, Nothing)
            ElseIf e.KeyCode = Keys.Escape Then
                RaiseEvent InternalGridKeyDown(sender, e)
                RaiseEvent InternalGridLostFocus(sender, e)
            End If
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally

        End Try
    End Sub

    Private Sub c1GridList_AfterSelChange(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RangeEventArgs) Handles C1GridList.AfterSelChange
        Try
            If C1GridList IsNot Nothing AndAlso C1GridList.Rows.Count > 0 Then

                _CurrentSelectedRow = C1GridList.RowSel
                ' If (_CurrentSelectedRow >= 1) Then
                'SelectedContent = C1GridList.Rows(_CurrentSelectedRow)(0).ToString()
                'End If

            End If

        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
        End Try
    End Sub

#End Region

    Public Function GetCurrentSelectedItem() As Boolean

        Dim _id As Int64 = 0
        Dim _code As String = ""
        Dim _desc As String = ""
        Dim rowIndex As Integer = 0
        Dim _retValue As Boolean = False
        Dim _SnoCode As String = ""
        Dim _SnoDesc As String = ""
        Dim blnIsSingleSnoMed As Boolean

        Try
            _SelectedItems.Clear()
            If C1GridList IsNot Nothing Then
                If C1GridList.Rows.Count > 0 Then

                    rowIndex = _CurrentSelectedRow

                    If rowIndex < 0 Then
                        GetCurrentSelectedItem = Nothing
                        Exit Function
                    End If

                    Select Case _ControlType
                        ''Added LOINCCode and LabsCPT
                        Case gloGridListControlType.LOINCCode, gloGridListControlType.LabsCPT, gloGridListControlType.CPT, gloGridListControlType.ICD9, gloGridListControlType.ICD10, gloGridListControlType.Modifier, gloGridListControlType.Procedures, gloGridListControlType.Cvx, gloGridListControlType.Mvx, gloGridListControlType.TradeName
                            If True Then
                                If _ControlType = gloGridListControlType.LOINCCode Or _ControlType = gloGridListControlType.LabsCPT Or _ControlType = gloGridListControlType.Cvx Or _ControlType = gloGridListControlType.Mvx Or _ControlType = gloGridListControlType.TradeName Then
                                    _desc = Convert.ToString(C1GridList.GetData(rowIndex, 0))
                                Else
                                    _id = Convert.ToInt64(C1GridList.GetData(rowIndex, 0))
                                    _code = Convert.ToString(C1GridList.GetData(rowIndex, 1))
                                    _desc = Convert.ToString(C1GridList.GetData(rowIndex, 2))

                                    If _ControlType = gloGridListControlType.ICD9 Or _ControlType = gloGridListControlType.ICD10 Then

                                        Dim objclsSnomed As New clsSnomedIcdMap
                                        Dim frm As FrmSelectProblem

                                        Dim ICDRevision As String = ""

                                        If _ControlType = gloGridListControlType.ICD9 Then
                                            ICDRevision = "9"
                                        ElseIf _ControlType = gloGridListControlType.ICD10 Then
                                            ICDRevision = "10"
                                        End If


                                        Dim dtProblemSnomed As DataTable = objclsSnomed.Get_DefaultSnomedForICD(_code, _desc, ICDRevision, DatabaseConnectionString)
                                        If Not IsNothing(dtProblemSnomed) Then
                                            If dtProblemSnomed.Rows.Count > 1 Then

                                                'Resolving Bug #86608: gloEMR >New Exam>ProbList>001.1 ICD is added but its associated snomed is not populated under the "Snomed CT ID" column. 
                                                If objclsSnomed.IsSnomedMandatory(DatabaseConnectionString) Then
                                                    gstrSMDBConnstr = GetHybridConnectionString(gstrSMDBServerName, gstrSMDBDatabaseName, gblnSMDBAuthen, gstrSMDBUserID, gstrSMDBPassWord)
                                                    frm = New gloSnoMed.FrmSelectProblem("Select Problem", gstrSMDBConnstr, DatabaseConnectionString)

                                                    '14-Jul-14 Aniket: Problem List SnoMed Project ICD Driven
                                                    blnIsSingleSnoMed = False

                                                    frm.blnIsProblem = True
                                                    If ICDRevision = 9 Then
                                                        frm.strCodeSystem = "ICD9"
                                                    ElseIf ICDRevision = 10 Then
                                                        frm.strCodeSystem = "ICD10"
                                                    End If
                                                    frm.txtSMSearch.Text = Convert.ToString(_code)
                                                    frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                                                    _SnoCode = Convert.ToString(frm.strSelectedConceptID)
                                                    _SnoDesc = Convert.ToString(frm.strSelectedDescription)
                                                    frm.Dispose()
                                                    frm = Nothing
                                                End If
                                            Else

                                                '14-Jul-14 Aniket: Problem List SnoMed Project ICD Driven
                                                blnIsSingleSnoMed = True
                                                If dtProblemSnomed.Rows.Count > 0 Then
                                                    _SnoCode = Convert.ToString(dtProblemSnomed.Rows(0)("CONCEPTID"))
                                                    _SnoDesc = Convert.ToString(dtProblemSnomed.Rows(0)("TermDescription"))
                                                End If
                                            End If
                                            dtProblemSnomed.Dispose()
                                            dtProblemSnomed = Nothing
                                        End If
                                        'End If

                                        ICDRevision = Nothing
                                        If Not IsNothing(objclsSnomed) Then
                                            objclsSnomed = Nothing
                                        End If

                                    End If

                                End If

                            End If
                            Exit Select
                    End Select

                    Dim oListItem As New gloItem()
                    oListItem.ID = _id
                    oListItem.Code = _code
                    oListItem.Description = _desc
                    '14-Jul-14 Aniket: Problem List SnoMed Project ICD Driven
                    oListItem.IsSnoMedOneToOneMapping = blnIsSingleSnoMed


                    If _ControlType = gloGridListControlType.ICD9 Or _ControlType = gloGridListControlType.ICD10 Then
                        Dim oListSubItem As New gloSubItem
                        oListSubItem.Code = _SnoCode
                        oListSubItem.Description = _SnoDesc
                        '14-Jul-14 Aniket: Problem List SnoMed Project ICD Driven
                        oListSubItem.IsSnoMedOneToOneMapping = blnIsSingleSnoMed
                        oListItem.SubItems.Add(oListSubItem)
                        oListSubItem.Dispose()
                        oListSubItem = Nothing
                    End If
                    
                    _SelectedItems.Clear()

                    _SelectedItems.Add(oListItem)
                    _retValue = True
                End If


            End If
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            _FireLostFocus = False
            RaiseEvent ItemSelected(Nothing, Nothing)
        End Try

        Return _retValue
    End Function

    'Private Function IsTableExists(ByVal strTableName As String) As Boolean
    '    Dim oDB As New gloDatabaseLayer.DBLayer(_DatabaseConnectionString)
    '    Dim oParameters As New gloDatabaseLayer.DBParameters()
    '    Try
    '        oDB.Connect(False)
    '        oParameters.Add("@sTableName", strTableName, ParameterDirection.InputOutput, SqlDbType.VarChar)
    '        Dim _oresult As New Object()
    '        oDB.Execute("TABLEEXISTS", oParameters, _oresult)
    '        If Convert.ToInt64(_oresult) > 0 AndAlso _oresult IsNot Nothing Then
    '            Return True
    '        Else
    '            'MessageBox.Show("Table Not Exists", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
    '            Return False
    '        End If
    '    Catch ex As Exception

    '        'MessageBox.Show("ERROR : " + ex.ToString(), "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error);
    '        Return False
    '    Finally
    '        oDB.Disconnect()
    '        oDB.Dispose()

    '        oParameters.Dispose()

    '    End Try
    'End Function

    Private Sub c1GridList_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)

        'ShowToolTip(C1SuperTooltip1, DirectCast(sender, C1.Win.C1FlexGrid.C1FlexGrid), e.Location)
    End Sub
    'Public Shared Sub ShowToolTip(ByVal oC1ToolTip As C1.Win.C1SuperTooltip.C1SuperTooltip, ByVal oGrid As C1.Win.C1FlexGrid.C1FlexGrid, ByVal nLocation As System.Drawing.Point)
    '    Try
    '        Dim myfont As System.Drawing.Font = oGrid.Font
    '        Dim stringsize As System.Drawing.SizeF

    '        Dim colsize As Integer = 0
    '        Dim sText As String = ""
    '        Dim nRow As Integer = 0
    '        Dim nCol As Integer = 0

    '        If oGrid.MouseCol > -1 AndAlso oGrid.MouseRow > -1 Then
    '            oC1ToolTip.Font = myfont
    '            oC1ToolTip.MaximumWidth = 400

    '            nRow = oGrid.MouseRow
    '            nCol = oGrid.MouseCol

    '            If Not oGrid.Cols(nCol).DataType Is GetType(System.Boolean) Then
    '                If nRow > 0 Then
    '                    If oGrid.GetData(nRow, nCol) IsNot Nothing Then
    '                        sText = oGrid.GetData(nRow, nCol).ToString()
    '                    End If
    '                    colsize = oGrid.Cols(nCol).WidthDisplay
    '                End If
    '                Dim oGrp As System.Drawing.Graphics = oGrid.CreateGraphics()
    '                stringsize = oGrp.MeasureString(sText, myfont)

    '                If stringsize.Width > colsize Then
    '                    oC1ToolTip.SetToolTip(oGrid, sText)
    '                Else
    '                    oC1ToolTip.SetToolTip(oGrid, "")
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception


    '    End Try
    'End Sub

    'Public Shared Sub ShowToolTip(ByVal oC1ToolTip As C1.Win.C1SuperTooltip.C1SuperTooltip, ByVal oGrid As C1.Win.C1FlexGrid.C1FlexGrid, ByVal nLocation As System.Drawing.Point, ByVal getnextcolumn As Boolean)
    '    Try
    '        Dim myfont As System.Drawing.Font = oGrid.Font
    '        Dim stringsize As System.Drawing.SizeF

    '        Dim colsize As Integer = 0
    '        Dim sText As String = ""
    '        Dim nRow As Integer = 0
    '        Dim nCol As Integer = 0

    '        If oGrid.MouseCol > -1 AndAlso oGrid.MouseRow > -1 Then
    '            oC1ToolTip.Font = myfont
    '            oC1ToolTip.MaximumWidth = 400

    '            nRow = oGrid.MouseRow
    '            If getnextcolumn = True Then
    '                nCol = oGrid.MouseCol + 1
    '            Else
    '                nCol = oGrid.MouseCol
    '            End If

    '            If Not oGrid.Cols(nCol).DataType Is GetType(System.Boolean) Then

    '                If nRow > 0 Then
    '                    If oGrid.GetData(nRow, nCol) IsNot Nothing Then
    '                        sText = oGrid.GetData(nRow, nCol).ToString()
    '                    End If
    '                    If getnextcolumn = True Then
    '                        colsize = oGrid.Cols(nCol - 1).WidthDisplay
    '                    Else
    '                        colsize = oGrid.Cols(nCol).WidthDisplay
    '                    End If
    '                End If
    '                Dim oGrp As System.Drawing.Graphics = oGrid.CreateGraphics()
    '                stringsize = oGrp.MeasureString(sText, myfont)

    '                If stringsize.Width > colsize Then
    '                    oC1ToolTip.SetToolTip(oGrid, sText)
    '                Else
    '                    oC1ToolTip.SetToolTip(oGrid, "")
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception


    '    End Try
    'End Sub

    Private Sub btnCloseRefill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseRefill.Click
        If Not IsNothing(ToolTip1) Then
            ToolTip1.RemoveAll()
            ToolTip1.Dispose()
        End If
        RaiseEvent CloseBtnClick(sender, e)
    End Sub


    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        _FireLostFocus = False
        RaiseEvent ItemSelected(sender, e)
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        RaiseEvent AddBtnClick(sender, e)
    End Sub

    Private Sub btnModify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModify.Click
        RaiseEvent ModifyBtnClick(sender, e)
    End Sub


    Private Sub gloUC_GridList_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Leave
        If (IsGridLostFocus = False) Then
            RaiseEvent InternalGridLostFocus(sender, e)
        End If
    End Sub



   

End Class
