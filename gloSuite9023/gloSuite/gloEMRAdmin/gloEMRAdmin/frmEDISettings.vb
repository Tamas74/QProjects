Public Class frmEDISettings
#Region " Contructors "
    Public Sub New()
        InitializeComponent()
        isFormLoad = True
    End Sub

    Public Sub New(ByVal DatabaseConnectionString As String)
        InitializeComponent()
        _databaseconnectionstring = DatabaseConnectionString
        isFormLoad = True
    End Sub

#End Region

#Region " Private Variables "

    Private _databaseconnectionstring As String = ""
    Private isFormLoad As Boolean
#End Region

    Private Sub frmEDISettings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillValidationFields()
        If (trvFields.Nodes.Count > 0) Then

            FillFieldStatusFromDatabase()
            trvFields.SelectedNode = trvFields.Nodes(0)
        End If
    End Sub

    Private Sub FillFieldStatusFromDatabase()
        Dim dt As New DataTable()
        Try
            If trvFields.Nodes.Count > 0 Then
                dt = GetSettings()
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For _FirstIndex As Integer = 0 To trvFields.Nodes.Count - 1
                        For _Index As Integer = 0 To trvFields.Nodes(_FirstIndex).Nodes.Count - 1
                            For _row As Integer = 0 To dt.Rows.Count - 1
                                If Convert.ToString(trvFields.Nodes(_FirstIndex).Nodes(_Index).Text) = Convert.ToString(dt.Rows(_row)("sSettingsName")) Then
                                    trvFields.Nodes(_FirstIndex).Nodes(_Index).Checked = Convert.ToBoolean(dt.Rows(_row)("sSettingsValue"))
                                End If
                            Next
                        Next
                    Next

                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FillValidationFields()
        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseconnectionstring)
        Dim dt As New DataTable()
        Dim _strSQL As String = ""
        Dim oNode As TreeNode = Nothing
        Try
            trvFields.Nodes.Clear()
            trvFields.Nodes.Add("Clearing House")
            trvFields.Nodes(0).Nodes.Add("Sender ID")
            trvFields.Nodes(0).Nodes.Add("Receiver ID")
            trvFields.Nodes(0).Nodes.Add("Sender Code")
            trvFields.Nodes(0).Nodes.Add("Receiver Code")

            trvFields.Nodes.Add("Submitter")
            trvFields.Nodes(1).Nodes.Add("Submitter Name")
            trvFields.Nodes(1).Nodes.Add("Submitter Contact Person Name")
            trvFields.Nodes(1).Nodes.Add("Submitter Contact Person Number")
            trvFields.Nodes(1).Nodes.Add("Submitter City")
            trvFields.Nodes(1).Nodes.Add("Submitter State")
            trvFields.Nodes(1).Nodes.Add("Submitter Zip")
            trvFields.Nodes(1).Nodes.Add("Submitter ETIN")
            trvFields.Nodes(1).Nodes.Add("Submitter Address")

            trvFields.Nodes.Add("Billing Provider")
            trvFields.Nodes(2).Nodes.Add("Billing Provider First Name")
            trvFields.Nodes(2).Nodes.Add("Billing Provider Middle Name")
            trvFields.Nodes(2).Nodes.Add("Billing Provider Last Name")
            trvFields.Nodes(2).Nodes.Add("Billing Provider City")
            trvFields.Nodes(2).Nodes.Add("Billing Provider State")
            trvFields.Nodes(2).Nodes.Add("Billing Provider Address")
            trvFields.Nodes(2).Nodes.Add("Billing Provider Zip")
            trvFields.Nodes(2).Nodes.Add("Billing Provider NPI")
            trvFields.Nodes(2).Nodes.Add("Billing Provider Employer ID")
            trvFields.Nodes(2).Nodes.Add("Billing Provider Taxonomy")

            trvFields.Nodes.Add("Facility")
            trvFields.Nodes(3).Nodes.Add("Facility Name")
            trvFields.Nodes(3).Nodes.Add("Facility Address")
            trvFields.Nodes(3).Nodes.Add("Facility City")
            trvFields.Nodes(3).Nodes.Add("Facility State")
            trvFields.Nodes(3).Nodes.Add("Facility Zip")
            trvFields.Nodes(3).Nodes.Add("Facility NPI")

            trvFields.Nodes.Add("Receiver")
            trvFields.Nodes(4).Nodes.Add("Receiver Name")
            trvFields.Nodes(4).Nodes.Add("Receiver ETIN")

            trvFields.Nodes.Add("Patient")
            trvFields.Nodes(5).Nodes.Add("Patient First Name")
            trvFields.Nodes(5).Nodes.Add("Patient Middle Name")
            trvFields.Nodes(5).Nodes.Add("Patient Last Name")
            trvFields.Nodes(5).Nodes.Add("Patient SSN")
            trvFields.Nodes(5).Nodes.Add("Patient Gender")
            trvFields.Nodes(5).Nodes.Add("Patient Date of Birth")
            trvFields.Nodes(5).Nodes.Add("Patient Address")
            trvFields.Nodes(5).Nodes.Add("Patient City")
            trvFields.Nodes(5).Nodes.Add("Patient State")
            trvFields.Nodes(5).Nodes.Add("Patient Zip")

            trvFields.Nodes.Add("Subscriber")
            trvFields.Nodes(6).Nodes.Add("Subscriber First Name")
            trvFields.Nodes(6).Nodes.Add("Subscriber Middle Name")
            trvFields.Nodes(6).Nodes.Add("Subscriber Last Name")
            trvFields.Nodes(6).Nodes.Add("Subscriber Insurance ID")
            trvFields.Nodes(6).Nodes.Add("Subscriber Insurance Type")
            trvFields.Nodes(6).Nodes.Add("Subscriber Relationship")
            trvFields.Nodes(6).Nodes.Add("Insurance Type")
            trvFields.Nodes(6).Nodes.Add("Subscriber Address")
            trvFields.Nodes(6).Nodes.Add("Subscriber Group ID")
            trvFields.Nodes(6).Nodes.Add("Subscriber City")
            trvFields.Nodes(6).Nodes.Add("Subscriber State")
            trvFields.Nodes(6).Nodes.Add("Subscriber Zip")
            trvFields.Nodes(6).Nodes.Add("Subscriber Date of Birth")
            trvFields.Nodes(6).Nodes.Add("Subscriber Gender")

            trvFields.Nodes.Add("Payer")
            trvFields.Nodes(7).Nodes.Add("Payer Name")
            trvFields.Nodes(7).Nodes.Add("Payer ID")
            trvFields.Nodes(7).Nodes.Add("Payer Address")
            trvFields.Nodes(7).Nodes.Add("Payer City")
            trvFields.Nodes(7).Nodes.Add("Payer State")
            trvFields.Nodes(7).Nodes.Add("Payer Zip")

            trvFields.Nodes.Add("Secondary Insurance")
            trvFields.Nodes(8).Nodes.Add("Secondary Insurance Subscriber First Name")
            trvFields.Nodes(8).Nodes.Add("Secondary Insurance Subscriber Last Name")
            trvFields.Nodes(8).Nodes.Add("Secondary Insurance Subscriber Middle Name")
            trvFields.Nodes(8).Nodes.Add("Secondary Insurance Type")
            trvFields.Nodes(8).Nodes.Add("Secondary Insurance Belongs To Type")
            trvFields.Nodes(8).Nodes.Add("Secondary Insurance Subscriber Relationship")
            trvFields.Nodes(8).Nodes.Add("Secondary Insurance ID")
            trvFields.Nodes(8).Nodes.Add("Secondary Insurance Group ID")
            trvFields.Nodes(8).Nodes.Add("Secondary Insurance Address")
            trvFields.Nodes(8).Nodes.Add("Secondary Insurance Name")
            trvFields.Nodes(8).Nodes.Add("Secondary Insurance Payer ID")
            trvFields.Nodes(8).Nodes.Add("Secondary Insurance City")
            trvFields.Nodes(8).Nodes.Add("Secondary Insurance State")
            trvFields.Nodes(8).Nodes.Add("Secondary Insurance Zip")
            trvFields.Nodes(8).Nodes.Add("Secondary Insurance Subscriber Date of Birth")
            trvFields.Nodes(8).Nodes.Add("Secondary Insurance Subscriber Gender")

            trvFields.Nodes.Add("Tertiary Insurance")
            trvFields.Nodes(9).Nodes.Add("Tertiary Insurance Subscriber First Name")
            trvFields.Nodes(9).Nodes.Add("Tertiary Insurance Subscriber Middle Name")
            trvFields.Nodes(9).Nodes.Add("Tertiary Insurance Subscriber Last Name")
            trvFields.Nodes(9).Nodes.Add("Tertiary Insurance Type")
            trvFields.Nodes(9).Nodes.Add("Tertiary Insurance Belongs To Type")
            trvFields.Nodes(9).Nodes.Add("Tertiary Insurance Subscriber Relationship")
            trvFields.Nodes(9).Nodes.Add("Tertiary Insurance ID")
            trvFields.Nodes(9).Nodes.Add("Tertiary Insurance Group ID")
            trvFields.Nodes(9).Nodes.Add("Tertiary Insurance Address")
            trvFields.Nodes(9).Nodes.Add("Tertiary Insurance Name")
            trvFields.Nodes(9).Nodes.Add("Tertiary Insurance Payer ID")
            trvFields.Nodes(9).Nodes.Add("Tertiary Insurance City")
            trvFields.Nodes(9).Nodes.Add("Tertiary Insurance State")
            trvFields.Nodes(9).Nodes.Add("Tertiary Insurance Zip")
            trvFields.Nodes(9).Nodes.Add("Tertiary Insurance Subscriber Date of Birth")
            trvFields.Nodes(9).Nodes.Add("Tertiary Insurance Subscriber Gender")


            trvFields.Nodes.Add("Referring Provider")
            trvFields.Nodes(10).Nodes.Add("Referring Provider Last Name")
            trvFields.Nodes(10).Nodes.Add("Referring Provider First Name")
            trvFields.Nodes(10).Nodes.Add("Referring Provider Middle Name")
            trvFields.Nodes(10).Nodes.Add("Referring Provider NPI")
            trvFields.Nodes(10).Nodes.Add("Referring Provider Taxonomy")
            trvFields.Nodes(10).Nodes.Add("Referring Provider TaxID")


            trvFields.ExpandAll()
        Catch ex As Exception
        Finally

        End Try
    End Sub

    Private Function GetSettings() As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseconnectionstring)
        Dim _strSQL As String = ""
        Dim dt As New DataTable()

        Try
            oDB.Connect(False)
            _strSQL = "SELECT nSettingsID, sSettingsName, sSettingsValue, nClinicID, nUserID, nUserClinicFlag FROM BL_Settings_EDI "

            oDB.Retrive_Query(_strSQL, dt)
        Catch ex As Exception
        Finally
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
        End Try
        Return dt
    End Function

    Private Sub SaveSetting(ByVal Name As String, ByVal Value As String)
        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseconnectionstring)
        Dim _strSQL As String = ""
        Dim _result As Object = Nothing
        Dim _ID As Int64 = 0
        'Boolean _IsSaved = false; 
        Try
            oDB.Connect(False)
            _strSQL = "SELECT nSettingsID FROM BL_Settings_EDI WHERE sSettingsName = '" & Name & "'"

            _result = oDB.ExecuteScalar_Query(_strSQL)

            If _result IsNot Nothing AndAlso _result.ToString() <> "" Then
                _ID = Convert.ToInt64(_result)
                If _ID > 0 Then
                    _result = New Object()
                    '_strSQL = ((" UPDATE BL_Settings_EDI SET sSettingsName = '" & Name & "', sSettingsValue = '") + Value & "' " & " WHERE (nSettingsID=") + _ID & ")"
                    _strSQL = " UPDATE BL_Settings_EDI SET sSettingsName = '" & Name & "', sSettingsValue = '" & Value & "' " & " WHERE nSettingsID= " & _ID & ""


                    _result = oDB.ExecuteScalar_Query(_strSQL)
                Else
                    _result = New Object()

                    _strSQL = "SELECT ISNULL(MAX(nSettingsID),0)+1 FROM BL_Settings_EDI "
                    _result = oDB.ExecuteScalar_Query(_strSQL)
                    If _result IsNot Nothing Then
                        _ID = Convert.ToInt64(_result)
                        '_strSQL = (((" INSERT INTO BL_Settings_EDI (nSettingsID, sSettingsName, sSettingsValue, nClinicID, nUserID, nUserClinicFlag) " & " VALUES (") + _ID & ", '") + Name & "', '") + Value & "', 1, 0, 1)"
                        _strSQL = " INSERT INTO BL_Settings_EDI (nSettingsID, sSettingsName, sSettingsValue, nClinicID, nUserID, nUserClinicFlag )  VALUES (" & _ID & ", '" & Name & "', '" & Value & "', 1, 0, 1)"


                        _result = oDB.ExecuteScalar_Query(_strSQL)
                    End If
                End If
            Else
                _result = New Object()

                _strSQL = "SELECT ISNULL(MAX(nSettingsID),0)+1 FROM BL_Settings_EDI "
                _result = oDB.ExecuteScalar_Query(_strSQL)
                If _result IsNot Nothing AndAlso _result <> "" Then
                    _ID = Convert.ToInt64(_result)
                    _strSQL = " INSERT INTO BL_Settings_EDI (nSettingsID, sSettingsName, sSettingsValue, nClinicID, nUserID, nUserClinicFlag ) VALUES (" & _ID & ", '" & Name & "', '" & Value & "', 1, 0, 1)"

                    _result = oDB.ExecuteScalar_Query(_strSQL)
                End If

            End If
        Catch ex As Exception
        Finally
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()

            End If

        End Try
    End Sub

    Private Sub Save()
        Try
            If trvFields.Nodes.Count > 0 Then
                For _FirstIndex As Integer = 0 To trvFields.Nodes.Count - 1

                    For _Index As Integer = 0 To trvFields.Nodes(_FirstIndex).Nodes.Count - 1
                        SaveSetting(Convert.ToString(trvFields.Nodes(_FirstIndex).Nodes(_Index).Text), Convert.ToString(trvFields.Nodes(_FirstIndex).Nodes(_Index).Checked))
                    Next
                Next
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tlsbtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsbtnSave.Click
        Try
            Save()
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub tlsbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsbtnClose.Click
        Me.Close()
    End Sub
#Region " Treeview Events "

    Private Sub trvFields_AfterCheck(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles trvFields.AfterCheck
        Try

            If isFormLoad Then
                isFormLoad = False
                If e.Node.Level = 0 Then
                    For _Index As Integer = 0 To e.Node.Nodes.Count - 1
                        e.Node.Nodes(_Index).Checked = CBool((e.Node.Checked))
                    Next
                Else
                    Dim blCheckParent As Boolean = False
                    'check if all childs are checked 

                    For _Index As Integer = 0 To e.Node.Parent.Nodes.Count - 1
                        If e.Node.Parent.Nodes(_Index).Checked = False Then
                            blCheckParent = False
                            Exit For
                        End If
                        blCheckParent = True
                    Next
                    If e.Node.Parent IsNot Nothing Then
                        If blCheckParent = True Then
                            e.Node.Parent.Checked = True
                        Else
                            e.Node.Parent.Checked = False
                        End If
                    End If
                End If
                isFormLoad = True

                trvFields.SelectedNode = e.Node
            End If
        Catch ex As Exception
        End Try
    End Sub

#End Region

    Private Sub trvFields_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvFields.AfterSelect

    End Sub

    
    Private Sub trvFields_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvFields.MouseDown
        trvFields.SelectedNode = trvFields.GetNodeAt(e.X, e.Y)
    End Sub
End Class