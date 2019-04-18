Public Class frmIM_AdjustInventory

#Region "Variable Declaration"

    Private _DosageOnHand As String
    Private _SKU As String
    Private _TradeName As String
    Private _Vaccine As String
    Private _Manufacturer As String
    Private _LotNumber As String
    Private _LocationID As String
    Private _DosageGiven As String
    Private _CategoryID As Long


    Private blnRecordSaved As Boolean = False

#End Region

#Region "Property"

    Public Property SKU() As String
        Get
            Return _SKU
        End Get
        Set(value As String)

            _SKU = value
        End Set
    End Property

    Public Property TradeName() As String
        Get
            Return _TradeName
        End Get
        Set(value As String)
            _TradeName = value
        End Set
    End Property

    Public Property Vaccine() As String
        Get
            Return _Vaccine
        End Get
        Set(value As String)
            _Vaccine = value
        End Set
    End Property

    Public Property Manufacturer() As String
        Get
            Return _Manufacturer
        End Get
        Set(value As String)
            _Manufacturer = value
        End Set
    End Property

    Public Property LotNumber() As String
        Get
            Return _LotNumber
        End Get
        Set(value As String)
            _LotNumber = value
        End Set
    End Property

    Public Property DosageOnHand() As String
        Get
            Return _DosageOnHand
        End Get
        Set(value As String)
            _DosageOnHand = value
        End Set
    End Property

    Public Property LocationID() As String
        Get
            Return _LocationID
        End Get
        Set(value As String)
            _LocationID = value
        End Set
    End Property

    Public Property DosageGiven() As String
        Get
            Return _DosageGiven
        End Get
        Set(value As String)
            _DosageGiven = value
        End Set
    End Property

    Public Property CategoryID() As Long
        Get
            Return _CategoryID
        End Get
        Set(value As Long)
            _CategoryID = value
        End Set
    End Property

#End Region

#Region "Form Event"

    Private Sub frmIM_AdjustInventoryvb_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            GetVaccinAvailableDoses()
            txtSKU.Text = _SKU
            txtTradeName.Text = _TradeName
            txtVaccine.Text = _Vaccine
            txtManufacturer.Text = _Manufacturer
            txtLotNumber.Text = _LotNumber
            txtDosageReturnToStock.Text = _DosageGiven
            txtCurrentInventory.Text = (Val(txtDosageOnHand.Text) + Val(txtDosageReturnToStock.Text))
            txtCurrentInventory.SelectionStart = Len(txtCurrentInventory.Text)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "Sub-Procedure"

    Private Sub GetVaccinAvailableDoses()
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters
        Dim dt As New DataTable

        oDB.Connect(False)
        oParam = New gloDatabaseLayer.DBParameters
        oParam.Add("@im_nCategoryID", _CategoryID, ParameterDirection.Input, SqlDbType.Decimal)
        oParam.Add("@im_sVaccine", _Vaccine, ParameterDirection.Input, SqlDbType.NVarChar)
        oParam.Add("@im_sTradeName", _TradeName, ParameterDirection.Input, SqlDbType.NVarChar)
        oParam.Add("@im_LotNumber", _LotNumber, ParameterDirection.Input, SqlDbType.NVarChar)
        oParam.Add("@im_nLocationID", _LocationID, ParameterDirection.Input, SqlDbType.Decimal)

        oDB.Retrive("IM_GetVaccinAvailableDoses", oParam, dt)
        oDB.Disconnect()

        If dt.Rows.Count > 0 Then
            If dt.Rows(0)(0) = 0 Then
                txtDosageOnHand.ForeColor = Color.Red
            Else
                txtDosageOnHand.ForeColor = Color.Blue
            End If
            txtDosageOnHand.Text = Format(dt.Rows(0)(0), "########0.##").ToString
        End If

        oParam.Dispose()
        oParam = Nothing
        oDB.Dispose()
        oDB = Nothing
    End Sub

    Private Sub UpdateFinalDoses()
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters
        Dim dt As New DataTable

        If txtCurrentInventory.Text = "" Or txtCurrentInventory.Text = "." Then
            txtCurrentInventory.Text = 0
        End If

        oDB.Connect(False)
        oParam = New gloDatabaseLayer.DBParameters

        oParam.Add("@im_sVaccine", _Vaccine, ParameterDirection.Input, SqlDbType.NVarChar)
        oParam.Add("@im_sManufacturer", _Manufacturer, ParameterDirection.Input, SqlDbType.NVarChar)
        oParam.Add("@im_sTradeName", _TradeName, ParameterDirection.Input, SqlDbType.NVarChar)
        oParam.Add("@im_LotNumber", _LotNumber, ParameterDirection.Input, SqlDbType.NVarChar)
        oParam.Add("@im_nLocationID", _LocationID, ParameterDirection.Input, SqlDbType.Decimal)
        oParam.Add("@Doses", CType(txtCurrentInventory.Text, Decimal), ParameterDirection.Input, SqlDbType.Decimal)

        oDB.Execute("IM_UpdateDosesAfterDelete", oParam)
        oDB.Disconnect()

        oParam.Dispose()
        oParam = Nothing
        oDB.Dispose()
        oDB = Nothing
    End Sub


#End Region

#Region "Button Click"

    Private Sub btn_Save_Click(sender As System.Object, e As System.EventArgs) Handles btn_Save.Click
        Try
            Dim blnBlank As Boolean

            If txtCurrentInventory.Text = "" Or txtCurrentInventory.Text = "." Then
                blnBlank = True
            End If

            frmIMTransactionList.blnCancelDelete = False
            blnRecordSaved = True
            UpdateFinalDoses()

            If blnBlank Then
                MessageBox.Show("Dosage on hand after change (" + txtCurrentInventory.Text + ") has been updated.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

    Private Sub frmIM_AdjustInventory_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If blnRecordSaved = False Then
            Dim res As MsgBoxResult

            If txtCurrentInventory.Text = "" Or txtCurrentInventory.Text = "." Then
                txtCurrentInventory.Text = 0
            End If

            res = MessageBox.Show("Do you want to update the vaccine inventory with (" & txtCurrentInventory.Text & ")?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)

            If res = MsgBoxResult.Yes Then
                frmIMTransactionList.blnCancelDelete = False
                UpdateFinalDoses()
            ElseIf res = MsgBoxResult.Cancel Then
                e.Cancel = True
            Else
                frmIMTransactionList.blnCancelDelete = True
            End If
        End If
    End Sub

    Private Sub txtCurrentInventory_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtCurrentInventory.KeyPress
        AllowDecimal(txtCurrentInventory.Text, e)
    End Sub

    Private Sub AllowDecimal(ByVal Text As String, ByVal e As KeyPressEventArgs)
        If InStr(Trim(Text), ".") <> 0 AndAlso (e.KeyChar = ChrW(46)) Then
            e.Handled = True
        Else
            If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(46)) OrElse (e.KeyChar = ChrW(8))) Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub tblbtn_Close_Click(sender As System.Object, e As System.EventArgs) Handles tblbtn_Close.Click
        If blnRecordSaved = False Then
            Dim res As MsgBoxResult

            If txtCurrentInventory.Text = "" Or txtCurrentInventory.Text = "." Then
                txtCurrentInventory.Text = 0
            End If

            res = MessageBox.Show("Do you want to update the vaccine inventory with (" & txtCurrentInventory.Text & ")?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)

            If res = MsgBoxResult.Yes Then
                frmIMTransactionList.blnCancelDelete = False
                UpdateFinalDoses()

                blnRecordSaved = True

                Me.Close()
            ElseIf res = MsgBoxResult.No Then
                frmIMTransactionList.blnCancelDelete = True
                blnRecordSaved = True
                Me.Close()
            End If
        End If
    End Sub

End Class