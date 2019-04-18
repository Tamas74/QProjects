Imports gloEMR.gloEMRWord
Imports System.Data.SqlClient


Public Class frmPatientConsentTracking

#Region "Variables"

    Dim _PatientID As Long = 0
    Dim _ConsenttrackingID As Long = 0
    Dim _IsLiquidLink As Boolean = False

#End Region

#Region "Constructor"

    Public Sub New(ByVal PatientID As Long)
        InitializeComponent()
        _PatientID = PatientID
    End Sub

    Public Sub New(ByVal PatientID As Long, ByVal ConsenttrackingID As Long, Optional ByVal IsLiquidLink As Boolean = False)
        InitializeComponent()
        _PatientID = PatientID
        _ConsenttrackingID = ConsenttrackingID
        _IsLiquidLink = IsLiquidLink
    End Sub

#End Region

#Region "Form Events"

    Private Sub frmPatientConsentTracking_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        cmbConsenttype.DataSource = Nothing
        cmbConsenttype.Items.Clear()

        cmbconsentstatus.DataSource = Nothing
        cmbconsentstatus.Items.Clear()


        cmbObtainedby.DataSource = Nothing
        cmbObtainedby.Items.Clear()
        If Not IsNothing(combo) Then
            combo.Items.Clear()
            combo.Dispose()
            combo = Nothing
        End If
    End Sub

    Private Sub frmPatientConsentTracking_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            cmbConsenttype.DrawMode = DrawMode.OwnerDrawFixed
            AddHandler cmbConsenttype.DrawItem, AddressOf ShowTooltipOnComboBox
            If _IsLiquidLink = True Then
                Dim result As Long = GetTop1ConsentTrackingID()
                If result > 0 Then
                    _ConsenttrackingID = result
                End If
            End If

            loadCombo()
            If _ConsenttrackingID > 0 Then
                LoadData()
            End If
        Catch ex As Exception
            MessageBox.Show(Me, ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub

    Private Sub frmPatientConsentTracking_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "Functions"

    Public Function GetTop1ConsentTrackingID() As Int64
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim TrackingId As Long = 0
        Try
            If _ConsenttrackingID = 0 Then
                oDB.Connect(False)
                Dim objResult As New Object
                objResult = oDB.ExecuteScalar_Query("SELECT nPatientConsentTrackingID FROM dbo.PatientConsentTracking  WHERE  nPatientID = " & _PatientID & "  ORDER BY dtConsentDate DESC ")
                oDB.Disconnect()
                If Not IsNothing(objResult) Then
                    If Convert.ToString(objResult) <> String.Empty Then
                        TrackingId = Convert.ToUInt64(objResult)
                    End If
                End If
                Return TrackingId
                End If
        Catch ex As Exception
            MessageBox.Show("Error on Consent Tracking." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return TrackingId
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
        Return TrackingId
    End Function

    Private Sub LoadData()
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters = Nothing
        Dim dtdata As DataTable = Nothing
        Try
            If _ConsenttrackingID > 0 Then
                oDB.Connect(False)
                oParam = New gloDatabaseLayer.DBParameters
                oParam.Add("@nPatientConsentTrackingID", _ConsenttrackingID, ParameterDirection.Input, SqlDbType.BigInt)
                oParam.Add("@PatientID", _PatientID, ParameterDirection.Input, SqlDbType.BigInt)
                oDB.Retrive("GetConsentTrackingList", oParam, dtdata)
                oDB.Disconnect()
                If (IsNothing(dtdata) = False) Then
                    If dtdata.Rows.Count > 0 Then
                        dtConsentDate.Value = Convert.ToDateTime(dtdata(0)("dtConsentDate"))
                        cmbConsenttype.SelectedValue = Convert.ToUInt64(dtdata(0)("nConsentType"))
                        cmbconsentstatus.SelectedValue = Convert.ToInt32(dtdata(0)("nConsentStatus"))
                        txtConsenter.Text = Convert.ToString(dtdata(0)("sConsenter"))
                        cmbObtainedby.SelectedValue = Convert.ToUInt64(dtdata(0)("nObtainedBy"))
                        txtCommentes.Text = Convert.ToString(dtdata(0)("sComments"))
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error on Consent Tracking." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            If IsNothing(oParam) = False Then
                oParam.Dispose()
                oParam = Nothing
            End If
        End Try

    End Sub


    Private Sub loadCombo()
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim dsIM As DataSet = Nothing
        Try
            oDB.Connect(False)
            oDB.Retrive("ConsentTrackingFillControl", dsIM)
            oDB.Disconnect()

            If (IsNothing(dsIM) = False) Then

                'Get Consent Type  
                If (dsIM.Tables.Count > 0) Then

                    cmbConsenttype.DataSource = Nothing
                    cmbConsenttype.Items.Clear()
                    cmbConsenttype.DataSource = dsIM.Tables(0)
                    cmbConsenttype.ValueMember = "nCategoryID"
                    cmbConsenttype.DisplayMember = "sDescription"
                    cmbConsenttype.SelectedIndex = -1
                End If

                If dsIM.Tables.Count > 1 Then
                    'Get Consent Status 

                    cmbconsentstatus.DataSource = Nothing
                    cmbconsentstatus.Items.Clear()
                    cmbconsentstatus.DataSource = dsIM.Tables(1)
                    cmbconsentstatus.ValueMember = "nConsentStatusID"
                    cmbconsentstatus.DisplayMember = "sConsentStatus"
                    If dsIM.Tables(1).Rows.Count > 0 Then
                        cmbconsentstatus.SelectedIndex = dsIM.Tables(1).Rows.Count - 1
                    Else
                        cmbconsentstatus.SelectedIndex = -1
                    End If
                End If
                If dsIM.Tables.Count > 2 Then
                    'Get User List

                    cmbObtainedby.DataSource = Nothing
                    cmbObtainedby.Items.Clear()
                    cmbObtainedby.DataSource = dsIM.Tables(2)
                    cmbObtainedby.ValueMember = "nUserID"
                    cmbObtainedby.DisplayMember = "sLoginName"
                    If (_ConsenttrackingID = 0) Then
                        cmbObtainedby.SelectedValue = mdlGeneral.gnLoginID
                    Else
                        cmbObtainedby.SelectedIndex = -1
                    End If
                End If
            End If

        Catch ex As Exception
            MessageBox.Show("Error on Consent Tracking." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Sub

    Public Function IsValidData() As Boolean
        Try
            Dim IsValid As Boolean = True

            If dtConsentDate.Value = Nothing Then
                MessageBox.Show("Please Select date of consent.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                IsValid = False
            ElseIf cmbConsenttype.SelectedIndex = -1 Then
                MessageBox.Show("Please Select Consent Type.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                cmbConsenttype.Select()
                IsValid = False
            ElseIf cmbconsentstatus.SelectedIndex = -1 Then
                MessageBox.Show("Please Select Consent Status.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                cmbconsentstatus.Select()
                IsValid = False
            ElseIf txtConsenter.Text.Trim = String.Empty Then
                MessageBox.Show("Please Enter Consenter.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtConsenter.Select()
                IsValid = False
            ElseIf (txtConsenter.Text.Trim <> String.Empty) And (txtConsenter.Text.Trim.Length > 100) Then
                MessageBox.Show("Please Enter Consenter within 100 character.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtConsenter.SelectAll()
                IsValid = False
            ElseIf cmbObtainedby.SelectedIndex = -1 Then
                MessageBox.Show("Please Select Obtained by.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                cmbObtainedby.Select()
                IsValid = False
            ElseIf (txtCommentes.Text.Trim <> String.Empty) And (txtCommentes.Text.Trim.Length > 500) Then
                MessageBox.Show("Please Enter Comment within 500 character.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtCommentes.SelectAll()
                IsValid = False
            End If

            Return IsValid
        Catch ex As Exception
            MessageBox.Show(Me, ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            Return False
        End Try
    End Function

    Public Sub SaveData()
        Try
            '@Opeation = 1 Add , 2 Modify , 3 Delete 
            Dim OperationType As Integer = 0
            If _ConsenttrackingID = 0 Then
                OperationType = 1
            Else
                OperationType = 2
            End If

            Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
            Dim oParam As gloDatabaseLayer.DBParameters
            oDB.Connect(False)
            oParam = New gloDatabaseLayer.DBParameters
            oParam.Add("@Opeation", OperationType, ParameterDirection.Input, SqlDbType.Int)
            oParam.Add("@nPatientConsentTrackingID", _ConsenttrackingID, ParameterDirection.Input, SqlDbType.BigInt)
            oParam.Add("@nPatientID", _PatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oParam.Add("@dtconsentDate", Convert.ToDateTime(dtConsentDate.Value), ParameterDirection.Input, SqlDbType.DateTime)
            oParam.Add("@nConsentType", Convert.ToUInt64(cmbConsenttype.SelectedValue), ParameterDirection.Input, SqlDbType.BigInt)
            oParam.Add("@nConsentStatus", Convert.ToInt32(cmbconsentstatus.SelectedValue), ParameterDirection.Input, SqlDbType.SmallInt)
            oParam.Add("@Consenter", Convert.ToString(txtConsenter.Text.Trim()), ParameterDirection.Input, SqlDbType.VarChar)
            oParam.Add("@nObtainedBy", Convert.ToUInt64(cmbObtainedby.SelectedValue), ParameterDirection.Input, SqlDbType.BigInt)
            oParam.Add("@Comments", Convert.ToString(txtCommentes.Text.Trim()), ParameterDirection.Input, SqlDbType.VarChar)
            Dim intResult As Integer = oDB.Execute("ConsentTrackingOperation", oParam)
            oParam.Dispose()
            oParam = Nothing
            oDB.Dispose()
            oParam = Nothing
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(Me, ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub

#End Region

#Region "Button Events"

    Private Sub tblbtn_Save_Click(sender As System.Object, e As System.EventArgs) Handles tblbtn_Save.Click
        Try
            If IsValidData() Then
                SaveData()
            End If
        Catch ex As Exception
            MessageBox.Show(Me, ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub

    Private Sub tblbtn_Close_Click(sender As System.Object, e As System.EventArgs) Handles tblbtn_Close.Click
        Me.Close()
    End Sub

#End Region


   
    Private combo As ComboBox = Nothing
    Private Sub ShowTooltipOnComboBox(ByVal sender As Object, ByVal e As DrawItemEventArgs)
        Try

            If combo Is Nothing Then
                combo = CType(sender, ComboBox)
            End If
            If combo.Items.Count > 0 And e.Index >= 0 Then

                e.DrawBackground()
                Dim br As SolidBrush = New SolidBrush(e.ForeColor)
                e.Graphics.DrawString(combo.GetItemText(combo.Items(e.Index)).ToString(), e.Font, br, e.Bounds)
                br.Dispose()
                br = Nothing

                If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
                    If combo.DroppedDown Then
                        If getWidthofListItems(combo.GetItemText(combo.Items(e.Index)).ToString(), combo) >= combo.DropDownWidth - 20 Then
                            Me.ToolTip2.Show(combo.GetItemText(combo.Items(e.Index)), combo, e.Bounds.Right - 200, e.Bounds.Bottom + 25, 1500)
                        Else
                            ToolTip2.RemoveAll()
                        End If
                    End If
                End If
                e.DrawFocusRectangle()
            End If
        Catch ex As Exception
            ex = Nothing
        End Try
    End Sub
    Private Function getWidthofListItems(ByVal _text As String, ByVal combo As ComboBox) As Integer  'code review changes 
        Dim width As Integer = 0
        Dim g As Graphics = Me.CreateGraphics()
        If Not g Is Nothing Then
            Dim s As SizeF = g.MeasureString(_text, combo.Font)
            width = Convert.ToInt32(s.Width)
            'Dispose graphics object
            g.Dispose()
        End If
        Return width
    End Function

 

    
End Class
