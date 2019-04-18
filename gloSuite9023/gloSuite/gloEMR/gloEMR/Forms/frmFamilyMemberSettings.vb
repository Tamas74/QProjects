
Imports System.Data
Imports System.Data.SqlClient


Public Class frmFamilyMemberSettings
    Implements IDisposable
    Public isNew As Boolean = True
    Public MemberId As Long = 0
    Public isDetailsScreen As Boolean = False
    Public _CategoryID As Long
    Public _Description As String = ""
    Public _SelectedCategoty = ""

    Public strRelation As String = ""
    Public strConceptID As String = ""
    Public strDescriptionID As String = ""
    Public strSnoMedID As String = ""
    Public strSnomedDescription As String = ""
    Public strSnomedDefination As String = ""

    'Private Cmd As System.Data.SqlClient.SqlCommand
    'Private Conn As SqlConnection
    'Private Adapter As System.Data.SqlClient.SqlDataAdapter

    Public Sub New()
        MyBase.New()
        isNew = True
        InitializeComponent()
    End Sub

    Public Sub New(ByVal memId As Long)
        MyBase.New()
        MemberId = memId ''True-New,False-Modify
        isNew = False
        InitializeComponent()
    End Sub


    Private Sub frmFamilyMemberSettings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not isNew Then
            FetchMember()
        End If
        txtRelation.Focus()
    End Sub

    Private Sub txtRelation_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRelation.KeyPress

        If e.KeyChar = ":" OrElse e.KeyChar = "|" Then
            e.Handled = True
        End If

    End Sub

    Private Sub tlsp_SettingList_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_SettingList.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Save"
                InUpSetting()
            Case "Close"
                Me.Close()
        End Select
    End Sub

    Private Sub btn_SnomedCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_SnomedCode.Click
        gstrSMDBConnstr = GetHybridConnectionString(gstrSMDBServerName, gstrSMDBDatabaseName, gblnSMDBAuthen, gstrSMDBUserID, gstrSMDBPassWord)
        Dim frm As New gloSnoMed.FrmSelectProblem("History", gstrSMDBConnstr, GetConnectionString())
        ' Dim Hs_StrSnoMedID As String
        Dim str As String = ""
        Try
            ' frm.StartPosition = FormStartPosition.CenterScreen
            ' frm.ShowInTaskbar = False
            If txt_ConceptID.Text.Trim <> "" Then
                frm.txtSMSearch.Text = strConceptID ''txt_ConceptID.Text.Trim
            Else
                frm.txtSMSearch.Text = txtRelation.Text
            End If
            frm.strConceptDesc = txtRelation.Text
            frm.strDescriptionID = strDescriptionID
            frm.strConceptID = strConceptID   ''txt_ConceptID.Text.Trim
            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            txtRelation.Focus()
            If frm._DialogResult Then
                strRelation = frm.strSelectedDescription
                strConceptID = frm.strConceptID
                strDescriptionID = frm.strDescriptionID
                strSnoMedID = frm.StrSnoMedID
                strSnomedDefination = frm.strSelectedDefination
                strSnomedDescription = frm.strSelectedDescription


                If txtRelation.Text = "" Then
                    txtRelation.Text = strRelation
                End If
                txt_ConceptID.Text = strConceptID + "-" + strSnomedDescription  ''8020 prd changes, chetan 
                txtSnomedcode.Text = strSnoMedID
                ' txtDescriptionId.Text = strDescriptionID  ''descriptionID removed 8020 prd changes
                txtsnodesc.Text = strSnomedDescription
                'If Not strSnomedDescription = "" And Not strSnomedDefination = "" Then
                '    BindSnomedToTree(strSnomedDescription & "|" & strSnomedDefination, trv_SNOMEDDesc)
                'Else
                '    trv_SNOMEDDesc.Nodes.Clear()
                'End If

            End If
        Catch ex As Exception
            frm.Dispose()
        Finally
            If Not IsNothing(frm) Then
                frm.Dispose()
                frm = Nothing
            End If
        End Try
    End Sub

    Private Sub btn_Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Delete.Click
        ClearData()
        txtRelation.Focus()
    End Sub




    Public Sub FetchMember()
        Dim Cmd As System.Data.SqlClient.SqlCommand = Nothing
        Dim Conn As SqlConnection = Nothing
        Dim Adapter As System.Data.SqlClient.SqlDataAdapter = Nothing
        Try
            Adapter = New System.Data.SqlClient.SqlDataAdapter
            Dim sqlconn As String
            sqlconn = GetConnectionString()
            Conn = New System.Data.SqlClient.SqlConnection(sqlconn)

            Cmd = New SqlCommand("gsp_viewFamilyMember_MST", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter
            objParam = Cmd.Parameters.Add("@nMemberID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = MemberId

            Adapter.SelectCommand = Cmd
            Dim dtMember As New DataTable


            Adapter.Fill(dtMember)
            If Not IsNothing(dtMember) Then
                strRelation = dtMember.Rows(0)("Relation").ToString()
                strConceptID = dtMember.Rows(0)("ConceptID").ToString()
                strDescriptionID = dtMember.Rows(0)("DescriptionId").ToString()
                strSnoMedID = dtMember.Rows(0)("SnomedID").ToString()
                strSnomedDescription = dtMember.Rows(0)("Description").ToString()
                strSnomedDefination = dtMember.Rows(0)("SnomedDescription").ToString()



                txtRelation.Text = strRelation
                txt_ConceptID.Text = strConceptID + "-" + strSnomedDescription ''8020 prd changes, chetan 
                txtSnomedcode.Text = strSnoMedID
                txtsnodesc.Text = strSnomedDescription
                ' txtDescriptionId.Text = strDescriptionID
                'If Not strSnomedDescription = "" And Not strSnomedDefination = "" Then
                '    BindSnomedToTree(strSnomedDescription & "|" & strSnomedDefination, trv_SNOMEDDesc)
                'Else
                '    trv_SNOMEDDesc.Nodes.Clear()
                'End If

            End If


            objParam = Nothing
            If Not IsNothing(dtMember) Then
                dtMember.Dispose()
                dtMember = Nothing
            End If
        Catch ex As SqlException
            UpdateLog(ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Conn.Close()

            If Not IsNothing(Conn) Then
                Conn.Dispose()
                Conn = Nothing
            End If
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If Not IsNothing(Adapter) Then
                Adapter.Dispose()
                Adapter = Nothing
            End If

        End Try
        'Return Ds
        'Return Ds
    End Sub

    Public Function validateData() As Boolean

        If txtRelation.Text = "" Then
            MessageBox.Show("Relation is required.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtRelation.Focus()
            Return False
        End If

        If txt_ConceptID.Text = "" Then
            MessageBox.Show("Snomed is required.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            btn_SnomedCode.Focus()
            Return False
        End If
        Dim Cmd As System.Data.SqlClient.SqlCommand = Nothing
        Dim Conn As SqlConnection = Nothing
        Dim Adapter As System.Data.SqlClient.SqlDataAdapter = Nothing
        Dim dtMember As New DataTable

        Try
            Adapter = New System.Data.SqlClient.SqlDataAdapter
            Dim sqlconn As String
            sqlconn = GetConnectionString()
            Conn = New System.Data.SqlClient.SqlConnection(sqlconn)

            Cmd = New SqlCommand("gsp_viewFamilyMember_MST", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter
            objParam = Cmd.Parameters.Add("@nMemberID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = 0

            Adapter.SelectCommand = Cmd

            Adapter.Fill(dtMember)

            For i As Integer = 0 To dtMember.Rows.Count - 1

                If txtRelation.Text.ToUpper() = dtMember.Rows(i)("Relation").ToString().ToUpper() Then
                    If isNew Then
                        MessageBox.Show("Relation already exist.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        txtRelation.Focus()
                        Return False
                    Else
                        If MemberId.ToString() <> dtMember.Rows(i)("Id").ToString() Then
                            MessageBox.Show("Relation already exist.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            txtRelation.Focus()
                            Return False
                        End If
                    End If
                Else
                    If strConceptID = dtMember.Rows(i)("ConceptId").ToString() And
                        strSnomedDescription = dtMember.Rows(i)("Description").ToString() Then
                        If isNew Then
                            MessageBox.Show("Relation already exist.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            txtRelation.Focus()
                            Return False
                        Else
                            If MemberId.ToString() <> dtMember.Rows(i)("Id").ToString() Then
                                MessageBox.Show("Concept Id already associated with another relation.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                btn_SnomedCode.Focus()
                                Return False
                            End If
                        End If
                    End If
                End If
            Next

            objParam = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            Conn.Close()

            If Not IsNothing(Conn) Then
                Conn.Dispose()
                Conn = Nothing
            End If
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If Not IsNothing(Adapter) Then
                Adapter.Dispose()
                Adapter = Nothing
            End If
            If Not IsNothing(dtMember) Then
                dtMember.Dispose()
                dtMember = Nothing
            End If
        End Try

        Return True

    End Function

    Public Sub InUpSetting()
        Dim objParam As SqlParameter = Nothing
        Dim Cmd As System.Data.SqlClient.SqlCommand = Nothing
        Dim Conn As SqlConnection = Nothing

        Try

            If validateData() Then


                Dim sqlconn As String
                sqlconn = GetConnectionString()
                Conn = New System.Data.SqlClient.SqlConnection(sqlconn)

                Cmd = New System.Data.SqlClient.SqlCommand("gsp_InUpFamilyMember_Mst", Conn)
                Cmd.CommandType = CommandType.StoredProcedure


                objParam = Cmd.Parameters.Add("@nMemberId", SqlDbType.VarChar, 255)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = MemberId

                objParam = Cmd.Parameters.Add("@sRelation", SqlDbType.VarChar, 255)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = txtRelation.Text

                objParam = Cmd.Parameters.Add("@sDescription", SqlDbType.VarChar, 255)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = strSnomedDescription

                objParam = Cmd.Parameters.Add("@sSnomedID", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = strSnoMedID

                objParam = Cmd.Parameters.Add("@sSnomedDescription", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = strSnomedDefination

                objParam = Cmd.Parameters.Add("@sConceptID", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = strConceptID

                objParam = Cmd.Parameters.Add("@sDescriptionID", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = strDescriptionID

                Conn.Open()
                Cmd.ExecuteNonQuery()

                ClearData()
                'objParam = Nothing

                Me.Close()

                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, " History Added", gloAuditTrail.ActivityOutCome.Success)

                Conn.Close()
            End If
        Catch ex As SqlException
            Conn.Close()
            UpdateLog(ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(Conn) Then
                Conn.Dispose()
                Conn = Nothing
            End If
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If Not IsNothing(objParam) Then
                objParam = Nothing
            End If
        End Try
    End Sub

    Public Sub ClearData()
        txt_ConceptID.Text = ""
        '  txtDescriptionId.Text = ""
        txtSnomedcode.Text = ""
        txtsnodesc.Text = ""
        ' trv_SNOMEDDesc.Nodes.Clear()
        strSnomedDefination = ""
        strDescriptionID = ""
        strSnoMedID = ""
        strSnomedDescription = ""
        strSnomedDefination = ""
    End Sub


    '#Region "IDisposable Support"
    '    Private disposedValue As Boolean ' To detect redundant calls

    '    ' IDisposable
    '    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
    '        If Not Me.disposedValue Then
    '            If disposing Then
    '                ' TODO: dispose managed state (managed objects).
    '            End If

    '            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
    '            ' TODO: set large fields to null.

    '            isNew = Nothing
    '            MemberId = Nothing
    '            isDetailsScreen = Nothing
    '            _CategoryID = Nothing
    '            _Description = Nothing
    '            _SelectedCategoty = Nothing

    '            strRelation = Nothing
    '            strConceptID = Nothing
    '            strDescriptionID = Nothing
    '            strSnoMedID = Nothing
    '            strSnomedDescription = Nothing
    '            strSnomedDefination = Nothing


    '            If Not IsNothing(dtFamilyMember) Then
    '                dtFamilyMember = Nothing
    '                dtFamilyMember.Dispose()
    '            End If

    '            If Not IsNothing(Cmd) Then
    '                Cmd = Nothing
    '                Cmd.Dispose()
    '            End If

    '            If Not IsNothing(Conn) Then
    '                Conn = Nothing
    '                Conn.Dispose()
    '            End If

    '            If Not IsNothing(Adapter) Then
    '                Adapter = Nothing
    '                Adapter.Dispose()
    '            End If

    '        End If
    '        Me.disposedValue = True
    '    End Sub

    '    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    '    'Protected Overrides Sub Finalize()
    '    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    '    Dispose(False)
    '    '    MyBase.Finalize()
    '    'End Sub

    '    ' This code added by Visual Basic to correctly implement the disposable pattern.

    '    Public Sub Dispose() Implements IDisposable.Dispose
    '        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '        Dispose(True)
    '        GC.SuppressFinalize(Me)
    '    End Sub
    '#End Region

    
End Class