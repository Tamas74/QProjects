Imports System.Data.SqlClient
Public Class frmAddRoles
    '' Code added by  Sandip Darade

#Region "Declarations"
    Private _databaseconnectionstring As String = ""
    Private _messageBoxCaption As String = "gloEMR"
    Private appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    Private _ClinicID As Int64 = 0
    Private _ExamID As Int64 = 0
    Private _ExamDetailID As Int64 = 0
    Private _ProviderID As Int64 = 0
    Private _UserID As Int64 = 0
    Private _Roles As String = ""
    Private _UserName As String = ""
    Private _ProviderName As String = ""
    Private _Category As String = ""
    Private _Ismodify As Boolean = False

    Public Property ClinicID() As Int64
        Get
            Return _ClinicID
        End Get
        Set(ByVal value As Int64)
            _ClinicID = value
        End Set
    End Property
    Private Const Col_UserName As Integer = 1
    Private Const Col_ProviderId As Integer = 0
    Private Const Col_ProviderName As Integer = 2
    Private Const Col_Role As Integer = 3
    Private Const Col_ExamdetailID As Integer = 4
    Private Const Col_UserID As Integer = 5



#End Region

#Region "Constructors"

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New(ByVal ExamID As Int64)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        _ExamID = ExamID
        ' Add any initialization after the InitializeComponent() call.

    End Sub

#End Region

#Region "Form Load"
    Private Sub frmAddRoles_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        gloC1FlexStyle.Style(C1Providers_Roles)

        Designgrid()
        Fill_Roles()
        ModifyRoles()
    End Sub
#End Region

#Region "Button click events"

    Private Sub tlsbtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsbtnSave.Click

        Savedata()
        Me.Close()
    End Sub

    Private Sub tlsbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsbtnClose.Click
        Me.Close()
    End Sub

#End Region

#Region "Add-Retrieve/Design grid  Methods "
    ''design grid 
    Private Sub Designgrid()

        C1Providers_Roles.Visible = True
        Try
            Dim ocl As New clsPatientExams
            Dim dt As DataTable = Nothing
            dt = ocl.GetUsers()
            '' dt = GetUsers()

            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    'C1Providers_Roles.Clear()
                    C1Providers_Roles.Rows.Fixed = 1
                    C1Providers_Roles.Rows.Count = 1
                    C1Providers_Roles.Cols.Count = 6

                    C1Providers_Roles.SetData(0, Col_UserName, "Login Name")
                    C1Providers_Roles.SetData(0, Col_UserID, "User ID")
                    C1Providers_Roles.SetData(0, Col_ProviderId, "ProviderID")
                    C1Providers_Roles.SetData(0, Col_ProviderName, "Name")
                    C1Providers_Roles.SetData(0, Col_Role, "Role")
                    C1Providers_Roles.SetData(0, Col_ExamdetailID, "ExamdetailID")

                    C1Providers_Roles.Cols(Col_UserName).Visible = True
                    C1Providers_Roles.Cols(Col_UserID).Visible = False
                    C1Providers_Roles.Cols(Col_ProviderId).Visible = False
                    C1Providers_Roles.Cols(Col_ProviderName).Visible = True
                    C1Providers_Roles.Cols(Col_Role).Visible = True
                    C1Providers_Roles.Cols(Col_ExamdetailID).Visible = False




                    Dim nWidth As Integer = C1Providers_Roles.Width
                    C1Providers_Roles.Cols(Col_UserName).Width = CInt((0.31 * (nWidth)))
                    C1Providers_Roles.Cols(Col_UserID).Width = 0
                    C1Providers_Roles.Cols(Col_ProviderId).Width = 0
                    C1Providers_Roles.Cols(Col_ProviderName).Width = CInt((0.33 * (nWidth)))
                    C1Providers_Roles.Cols(Col_Role).Width = CInt((0.33 * (nWidth)))
                    C1Providers_Roles.Cols(Col_ExamdetailID).Width = 0




                    For i As Integer = 0 To dt.Rows.Count - 1
                        Dim RowIndex As Int32 = C1Providers_Roles.Rows.Count
                        C1Providers_Roles.Rows.Add()
                        C1Providers_Roles.SetData(RowIndex, Col_ProviderId, dt.Rows(i)("nProviderID"))
                        C1Providers_Roles.SetData(RowIndex, Col_UserID, dt.Rows(i)("nUserID"))
                        C1Providers_Roles.SetData(RowIndex, Col_ProviderName, dt.Rows(i)("sName"))
                        C1Providers_Roles.SetData(RowIndex, Col_UserName, dt.Rows(i)("sLoginName"))
                        C1Providers_Roles.SetData(RowIndex, Col_ExamdetailID, 0)

                    Next

                    C1Providers_Roles.AllowEditing = True
                    C1Providers_Roles.Cols(Col_ProviderName).AllowEditing = False
                    C1Providers_Roles.Cols(Col_UserName).AllowEditing = False

                End If
                dt.Dispose()
            End If

            ocl.Dispose()
        Catch ex As Exception

        End Try
    End Sub

    ''fill roles in the combolist 
    Private Sub Fill_Roles()
        Try
            Dim ocls As New clsPatientExams
            Dim dtRoles As DataTable = ocls.GetRoles()


            If dtRoles IsNot Nothing AndAlso dtRoles.Rows.Count > 0 Then

                For i As Integer = 0 To dtRoles.Rows.Count - 1
                    _Roles += " |" & Convert.ToString(dtRoles.Rows(i)("Category"))
                Next

                C1Providers_Roles.Cols(Col_Role).ComboList = _Roles

            End If
            ocls.Dispose()
            If (IsNothing(dtRoles) = False) Then
                dtRoles.Dispose()
            End If
        Catch ex As Exception
        End Try


    End Sub

    ''design grid to modify existing roles
    Private Function ModifyRoles()
        Try
            Dim dt As DataTable = Nothing
            Dim ocls As New clsPatientExams

            dt = ocls.GetProvidersRole(_ExamID)
            If dt IsNot Nothing Then
                For j As Integer = 0 To dt.Rows.Count - 1
                    For k As Integer = 1 To C1Providers_Roles.Rows.Count - 1
                       
                        If Convert.ToString(C1Providers_Roles.GetData(k, Col_UserName)) <> "" Then
                            If Convert.ToString(C1Providers_Roles.GetData(k, Col_UserName)) = Convert.ToString(dt.Rows(j)("sUsername")) Then
                               
                                C1Providers_Roles.SetData(k, Col_ExamdetailID, Convert.ToInt64(dt.Rows(j)("nExamDetailID")))
                                C1Providers_Roles.SetData(k, Col_Role, Convert.ToString(dt.Rows(j)("sCategory")))

                                _Ismodify = True

                            End If
                        End If

                    Next
                Next


                C1Providers_Roles.AllowEditing = True
                C1Providers_Roles.Cols(Col_ProviderName).AllowEditing = False
                dt.Dispose()
            End If

            ocls.Dispose()
        Catch ex As Exception

        End Try
        Return Nothing
    End Function

    ''save provider's role
    Private Function Savedata() As Boolean
        Dim ocls As New clsPatientExams
        Try
            ocls.Deleterole(_ExamID)

            For i As Integer = 1 To C1Providers_Roles.Rows.Count - 1
                _ProviderID = Convert.ToInt64(C1Providers_Roles.GetData(i, Col_ProviderId))
                _ProviderName = Convert.ToString(C1Providers_Roles.GetData(i, Col_ProviderName))
                _UserName = Convert.ToString(C1Providers_Roles.GetData(i, Col_UserName))
                _Roles = Convert.ToString(C1Providers_Roles.GetData(i, Col_Role))
                _UserID = Convert.ToInt64(C1Providers_Roles.GetData(i, Col_UserID))

                If _Roles.Trim() <> "" Then
                    ocls.Add(_ExamID, _ExamDetailID, _ProviderID, _ProviderName, _UserID, _UserName, _Roles)
                End If
            Next

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
        ocls.Dispose()
        Return False
    End Function

#End Region

    Private Sub C1Providers_Roles_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1Providers_Roles.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Public Shared Sub ShowToolTip(ByVal oC1ToolTip As C1.Win.C1SuperTooltip.C1SuperTooltip, ByVal oGrid As C1.Win.C1FlexGrid.C1FlexGrid, ByVal nLocation As System.Drawing.Point)
        Try
            Dim myFont As Font = oGrid.Font
            Dim stringsize As SizeF
            Dim colsize As Integer = 0
            Dim sText As String = ""
            Dim nRow As Integer
            Dim nCol As Integer

            If oGrid.MouseCol > -1 AndAlso oGrid.MouseRow > -1 Then
                oC1ToolTip.Font = myFont
                oC1ToolTip.MaximumWidth = 400

                nRow = oGrid.MouseRow
                nCol = oGrid.MouseCol

                If nRow > 0 Then 'And nCol > 0 Then
                    If Not oGrid.GetData(nRow, nCol) Is Nothing Then
                        sText = oGrid.GetData(nRow, nCol)
                    End If
                    colsize = oGrid.Cols(nCol).WidthDisplay
                End If
                Dim oGrp As Graphics = oGrid.CreateGraphics()
                stringsize = oGrp.MeasureString(sText, myFont)
                ''Code Review Changes: Dispose Graphics object
                oGrp.Dispose()
                If stringsize.Width > colsize Then
                    oC1ToolTip.SetToolTip(oGrid, sText)
                Else
                    oC1ToolTip.SetToolTip(oGrid, "")
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

End Class