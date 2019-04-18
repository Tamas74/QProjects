Public Class CustomInventory
    Inherits System.Windows.Forms.UserControl
    Public Event OKClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event CloseClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event CompanyClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Private m_ContactId As Int64
    Private m_OldContactId As Int64
    Private m_InventoryItemId As Int64
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        Dim dtpControls() As System.Windows.Forms.DateTimePicker = {dtpSampleInDate, dtpExpiryDate}
        Dim cntControls() As System.Windows.Forms.Control = {dtpSampleInDate, dtpExpiryDate}


        components.Dispose()

        If (IsNothing(dtpControls) = False) Then
            If dtpControls.Length > 0 Then
                gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
            End If
        End If

        If (IsNothing(cntControls) = False) Then
            If cntControls.Length > 0 Then
                gloGlobal.cEventHelper.DisposeAllControls(cntControls)
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents grpMain As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtContactPerson As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCompany As System.Windows.Forms.TextBox
    Friend WithEvents btnCompany As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtpSampleInDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpExpiryDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtLotNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtQuantity As System.Windows.Forms.TextBox
    Friend WithEvents txtUnit As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.grpMain = New System.Windows.Forms.GroupBox
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnOK = New System.Windows.Forms.Button
        Me.txtUnit = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtQuantity = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.dtpExpiryDate = New System.Windows.Forms.DateTimePicker
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtLotNumber = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.dtpSampleInDate = New System.Windows.Forms.DateTimePicker
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnCompany = New System.Windows.Forms.Button
        Me.txtCompany = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtContactPerson = New System.Windows.Forms.TextBox
        Me.grpMain.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpMain
        '
        Me.grpMain.Controls.Add(Me.btnClose)
        Me.grpMain.Controls.Add(Me.btnOK)
        Me.grpMain.Controls.Add(Me.txtUnit)
        Me.grpMain.Controls.Add(Me.Label7)
        Me.grpMain.Controls.Add(Me.txtQuantity)
        Me.grpMain.Controls.Add(Me.Label6)
        Me.grpMain.Controls.Add(Me.dtpExpiryDate)
        Me.grpMain.Controls.Add(Me.Label5)
        Me.grpMain.Controls.Add(Me.txtLotNumber)
        Me.grpMain.Controls.Add(Me.Label1)
        Me.grpMain.Controls.Add(Me.GroupBox1)
        Me.grpMain.Location = New System.Drawing.Point(8, 8)
        Me.grpMain.Name = "grpMain"
        Me.grpMain.Size = New System.Drawing.Size(560, 232)
        Me.grpMain.TabIndex = 0
        Me.grpMain.TabStop = False
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(488, 200)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(64, 24)
        Me.btnClose.TabIndex = 15
        Me.btnClose.Text = "&Close"
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(424, 200)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(64, 24)
        Me.btnOK.TabIndex = 14
        Me.btnOK.Text = "&Ok"
        '
        'txtUnit
        '
        Me.txtUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUnit.Location = New System.Drawing.Point(368, 168)
        Me.txtUnit.Name = "txtUnit"
        Me.txtUnit.Size = New System.Drawing.Size(96, 21)
        Me.txtUnit.TabIndex = 13
        Me.txtUnit.Text = ""
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(295, 166)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(64, 24)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Unit"
        '
        'txtQuantity
        '
        Me.txtQuantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtQuantity.Location = New System.Drawing.Point(107, 168)
        Me.txtQuantity.Name = "txtQuantity"
        Me.txtQuantity.Size = New System.Drawing.Size(184, 21)
        Me.txtQuantity.TabIndex = 11
        Me.txtQuantity.Text = ""
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(8, 166)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 24)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Quantity"
        '
        'dtpExpiryDate
        '
        Me.dtpExpiryDate.CustomFormat = "MM/dd/yyyy hh:mm:ss"
        Me.dtpExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpExpiryDate.Location = New System.Drawing.Point(368, 136)
        Me.dtpExpiryDate.Name = "dtpExpiryDate"
        Me.dtpExpiryDate.Size = New System.Drawing.Size(184, 21)
        Me.dtpExpiryDate.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(295, 136)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 16)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Expiry Date"
        '
        'txtLotNumber
        '
        Me.txtLotNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLotNumber.Location = New System.Drawing.Point(107, 136)
        Me.txtLotNumber.Name = "txtLotNumber"
        Me.txtLotNumber.Size = New System.Drawing.Size(184, 21)
        Me.txtLotNumber.TabIndex = 2
        Me.txtLotNumber.Text = ""
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 136)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 24)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Lot Number"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dtpSampleInDate)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.btnCompany)
        Me.GroupBox1.Controls.Add(Me.txtCompany)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtContactPerson)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(3, 17)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(554, 111)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'dtpSampleInDate
        '
        Me.dtpSampleInDate.CustomFormat = "MM/dd/yyyy hh:mm:ss"
        Me.dtpSampleInDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpSampleInDate.Location = New System.Drawing.Point(107, 80)
        Me.dtpSampleInDate.Name = "dtpSampleInDate"
        Me.dtpSampleInDate.Size = New System.Drawing.Size(184, 21)
        Me.dtpSampleInDate.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(8, 80)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(96, 16)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Sample In Date"
        '
        'btnCompany
        '
        Me.btnCompany.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCompany.Location = New System.Drawing.Point(497, 16)
        Me.btnCompany.Name = "btnCompany"
        Me.btnCompany.Size = New System.Drawing.Size(24, 21)
        Me.btnCompany.TabIndex = 5
        Me.btnCompany.Text = "..."
        '
        'txtCompany
        '
        Me.txtCompany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCompany.Location = New System.Drawing.Point(107, 16)
        Me.txtCompany.Name = "txtCompany"
        Me.txtCompany.Size = New System.Drawing.Size(392, 21)
        Me.txtCompany.TabIndex = 4
        Me.txtCompany.Text = ""
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(8, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 16)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Company Name"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(8, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 16)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Contact Person"
        '
        'txtContactPerson
        '
        Me.txtContactPerson.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtContactPerson.Location = New System.Drawing.Point(107, 48)
        Me.txtContactPerson.Name = "txtContactPerson"
        Me.txtContactPerson.Size = New System.Drawing.Size(416, 21)
        Me.txtContactPerson.TabIndex = 0
        Me.txtContactPerson.Text = ""
        '
        'CustomInventory
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(207, Byte), CType(227, Byte), CType(254, Byte))
        Me.Controls.Add(Me.grpMain)
        Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "CustomInventory"
        Me.Size = New System.Drawing.Size(576, 248)
        Me.grpMain.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Enum enmcontrolname
        CompanyName
        ContactPerson
        SampleIndate
        Expirydate
        LotNo
        Quantity
        OK
        Close
        Company
    End Enum
    Public Property ContactPerson() As String
        Get
            Return txtContactPerson.Text
        End Get
        Set(ByVal Value As String)
            txtContactPerson.Text = Value
        End Set
    End Property
    Public ReadOnly Property MinExpiryDate() As String
        Get
            Return dtpExpiryDate.MinDate
        End Get
    End Property
    Public Property Company() As String
        Get
            Return txtCompany.Text
        End Get
        Set(ByVal Value As String)
            txtCompany.Text = Value
        End Set
    End Property
    Public Property ContactID() As Int64
        Get
            Return m_ContactId
        End Get
        Set(ByVal Value As Int64)
            m_ContactId = Value
        End Set
    End Property
    Public Property InventoryItemID() As Int64
        Get
            Return m_InventoryItemId
        End Get
        Set(ByVal Value As Int64)
            m_InventoryItemId = Value
        End Set
    End Property
    Public Property OldContactID() As Int64
        Get
            Return m_OldContactId
        End Get
        Set(ByVal Value As Int64)
            m_OldContactId = Value
        End Set
    End Property
    Public Property SampleInDate() As DateTime
        Get
            Return dtpSampleInDate.Value
        End Get
        Set(ByVal Value As DateTime)
            dtpSampleInDate.Value = Value
        End Set
    End Property
    Public Property ExpiryDate() As DateTime
        Get
            Return dtpExpiryDate.Value
        End Get
        Set(ByVal Value As DateTime)
            dtpExpiryDate.Value = Value
        End Set
    End Property
    Public Property LotNumber() As String
        Get
            Return txtLotNumber.Text
        End Get
        Set(ByVal Value As String)
            txtLotNumber.Text = Value
        End Set
    End Property
    Public Property Quantity() As Double
        Get
            Return txtQuantity.Text
        End Get
        Set(ByVal Value As Double)
            txtQuantity.Text = Value
        End Set
    End Property
    Public Property Unit() As String
        Get
            Return txtUnit.Text
        End Get
        Set(ByVal Value As String)
            txtUnit.Text = Value
        End Set
    End Property
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        RaiseEvent OKClick(sender, e)
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        RaiseEvent CloseClick(sender, e)
    End Sub

    Private Sub btnCompany_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCompany.Click
        RaiseEvent CompanyClick(sender, e)
    End Sub
    Public Sub SetControlfocus(ByVal enm As enmcontrolname)
        Select Case enm
            Case enmcontrolname.CompanyName
                txtCompany.Focus()
            Case enmcontrolname.ContactPerson
                txtContactPerson.Focus()
            Case enmcontrolname.SampleIndate
                dtpSampleInDate.Focus()
            Case enmcontrolname.Quantity
                txtQuantity.Focus()
            Case enmcontrolname.LotNo
                txtLotNumber.Focus()
            Case enmcontrolname.Expirydate
                dtpExpiryDate.Focus()
            Case enmcontrolname.OK
                Dim objSender As Object = Nothing
                Dim obje As EventArgs = Nothing
                btnOK_Click(objSender, obje)
            Case enmcontrolname.Close
                Dim objSender As Object = Nothing
                Dim obje As EventArgs = Nothing
                btnClose_Click(objSender, obje)
            Case enmcontrolname.Company
                Dim objSender As Object = Nothing
                Dim obje As EventArgs = Nothing
                btnCompany_Click(objSender, obje)
        End Select
    End Sub
End Class
