


Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Public Class frmMSTZip

    'Public m_ZipCode As Integer

#Region " Private Variables "
    Dim oPatientReg As ClsPatientRegistrationDBLayer
    Private m_ID As Int64
    'Private m_ZipCode As Int64
    Private m_ZipCode As String
    Private m_State As String
    Private m_City As String
    Private m_County As String
    Private m_AreaCode As String
#End Region

#Region " Public Properties "
    Public ReadOnly Property ID() As Int64
        Get
            Return m_ID
        End Get
    End Property
    Public ReadOnly Property ZipCode() As Int64
        Get
            Return m_ZipCode
        End Get
    End Property
    Public ReadOnly Property State() As String
        Get
            Return m_State
        End Get
    End Property
    Public ReadOnly Property City() As String
        Get
            Return m_City
        End Get
    End Property
    Public ReadOnly Property County() As String
        Get
            Return m_County
        End Get
    End Property
    Public ReadOnly Property AreaCode() As String
        Get
            Return m_AreaCode
        End Get
    End Property
#End Region

#Region " Constructor "

    'Public Sub New(ByVal ID As Int64, ByVal nzipCode As Int64, ByVal sCity As String, ByVal sCounty As String, ByVal sState As String, ByVal nAreaCode As String)
    Public Sub New(ByVal ID As Int64, ByVal nzipCode As String, ByVal sCity As String, ByVal sCounty As String, ByVal sState As String, ByVal nAreaCode As String)

        MyBase.New()
        m_ID = ID
        'm_ZipCode = nzipCode
        m_ZipCode = nzipCode
        m_State = sState
        m_City = sCity
        m_County = sCounty
        m_AreaCode = nAreaCode
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New(ByVal nZipCode As Int64, ByVal sCity As String, ByVal sState As String)
        MyBase.New()
        InitializeComponent()
        m_ZipCode = nZipCode
        m_City = sCity
        m_State = sState
    End Sub
    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub
#End Region

#Region "Functions area"

   

    Private Sub frmMSTZip_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If (IsNothing(oPatientReg) = False) Then   ''added for   bugid 71311
            oPatientReg.Dispose()
            oPatientReg = Nothing
        End If
    End Sub
    Private Sub frmMSTZip_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ''dhruv20100121 commented as the functionality Changed
            'txtCity.Text = m_City
            'txtCounty.Text = m_County
            'txtZIPCode.Text = m_ZipCode
            'If m_ZipCode > 0 And m_City <> "" And m_State <> "" And m_ID = 0 Then
            '    SetZIPDetails(m_ZipCode, m_City, m_State)
            'End If
            ''----------------------------
            oPatientReg = New ClsPatientRegistrationDBLayer
            If m_ID <> 0 Then
                SetZIPDetails(m_ID)                    ''Checking for the modified Zip
            End If

            If m_ID <> 0 Then
                Fill_State()                            ''Checking for the modifed Zip
                Fill_Zip()
                Me.Text = " Modify Zip "                ''Dhruv ---------------------Ends
                ''
            Else
                Fill_State()                            ''Checking for the new Zip
                Me.Text = " Add Zip "                   ''Dhruv -----------------Ends
            End If

            If gstrCountry = "US" Then
                txtZIPCode.MaxLength = 5
            Else
                txtZIPCode.MaxLength = 6
            End If

            'If m_ZipCode = 0 Then                       ''Checking When Zip Code is 0 then Enter the Empty String
            '    txtZIPCode.Text = ""
            'End If                                      ''Dhruv-----------------------------------------------Ends


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
#Region "To save the "
    'Save the ZIp entry in to database
    'Private Sub SaveMSTZIP()

    ''' <summary>
    ''' Dhruv 20100121 
    ''' To Save the zip code when it is open over the modify and add 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SaveMSTZIP()

        Dim objViewZip As New frmVWZip
        If IsNothing(oPatientReg) Then
            oPatientReg = New ClsPatientRegistrationDBLayer
        End If
        Try
            Dim _isZipCode As Boolean = False
            Dim _isAreaCode As Boolean = False

            '_isZipCode = ValidatingNumericText(txtZIPCode)
            If _isZipCode = True Then
                MessageBox.Show("Enter valid zip code.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtZIPCode.Focus()
                Exit Try
            End If
            If Trim(txtZIPCode.Text) = "" Then
                MessageBox.Show("ZIP code must be entered.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtZIPCode.Focus()
                Exit Sub
            End If
            If Trim(txtCity.Text) = "" Then
                MessageBox.Show("ZIP code must have city.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCity.Focus()
                Exit Sub
            End If
            If Trim(cmbState.Text) = "" Then
                MessageBox.Show("ZIP code must have state.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbState.Focus()
                Exit Sub
            End If
            txtZIPCode.Text = ZipLeadingWithZero(txtZIPCode)   ''Calling the funtion to add the leading with zero if zip-Codes is less then 5 digit
            m_City = txtCity.Text
            m_ZipCode = txtZIPCode.Text
            m_State = cmbState.Text
            m_County = txtCounty.Text
            _isAreaCode = ValidatingNumericText(txtAreacode)            ''Validaing the text under the text box.
            If _isAreaCode = True Then
                MessageBox.Show("Enter valid area code.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtAreacode.Focus()
                Exit Try
            End If


            Dim areacode As Int64                                       ''Checking the Area Code for its Empty and having certains value
            'Dim areacode As String
            If txtAreacode.Text = "" Then

                areacode = 0
            Else
                areacode = Convert.ToInt64(txtAreacode.Text)            ''Dhruv-------------------------------------------------------Ends 
                'AreaCode = txtAreacode.Text
            End If


            If m_ID = 0 Then                                            ''When "new entry" is done of the zip code ("add")

                'If _isAreaCode = True Then
                '    MessageBox.Show("Please enter valid area code", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                '    txtAreacode.Focus()
                '    Exit Try
                'End If

                If oPatientReg.IsReocrdPresent(txtCity.Text, cmbState.Text, txtZIPCode.Text, txtCounty.Text, areacode) = True Then
                    'Return nID B'coz that nID require to set ocus on newlly added record 
                    m_ID = oPatientReg.AddCity(txtCity.Text, cmbState.Text, txtZIPCode.Text, areacode, txtCounty.Text)
                    Me.DialogResult = Windows.Forms.DialogResult.Yes
                    ' Me.Close()
                    gloWord.WordDialogBoxBackgroundCloser.Close(Me)
                Else
                    'MessageBox.Show("Zip Code with same city is already present", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    MessageBox.Show("Zip code already present", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else                                                        ''When modify entry is done for the zip code("Modify")
                If oPatientReg.IsReocrdPresent(txtCity.Text, cmbState.Text, txtZIPCode.Text, txtCounty.Text, areacode) = True Then
                    'If oPatientReg.IsReocrdPresent(txtCity.Text, cmbState.Text, txtZIPCode.Text, txtCounty.Text, areacode) = True Then
                    'Return nID B'coz that nID require to set ocus on newlly added record 
                    oPatientReg.UpdateCity(txtCity.Text, txtZIPCode.Text, m_ID, txtCounty.Text, cmbState.Text, txtAreacode.Text)
                    Me.DialogResult = Windows.Forms.DialogResult.Yes
                    '  Me.Close()
                    gloWord.WordDialogBoxBackgroundCloser.Close(Me)
                Else
                    ''Dhruv 20100121 Commented according to the functionality
                    'MessageBox.Show("Zip Code with same city is already present", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'MessageBox.Show("Zip code already present", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ''--------------------------------------------------------
                End If
                ''Dhruv 20100120 commented Functionality changed
                'oPatientReg.UpdateCity(txtCity.Text, txtZIPCode.Text, m_ID, m_County)
                'Me.DialogResult = Windows.Forms.DialogResult.Yes
                ''--------------------
                ' Me.Close()
                gloWord.WordDialogBoxBackgroundCloser.Close(Me)
            End If
            ' oPatientReg.Dispose()    commented for   bugid 71311
            ' oPatientReg = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.DialogResult = Windows.Forms.DialogResult.No
        Finally
            objViewZip = Nothing
        End Try
    End Sub
#End Region

    'Close the zip form
    Private Sub CloseMSTZIP()
        Try
            '  Me.Close()
            gloWord.WordDialogBoxBackgroundCloser.Close(Me)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    'fill the State combobox while loading the form
    Private Sub Fill_State()
        Dim dt As New DataTable
        If IsNothing(oPatientReg) Then
            oPatientReg = New ClsPatientRegistrationDBLayer
        End If
        dt = oPatientReg.FillControls("T") '' getAllState()
        If IsNothing(dt) = False Then
            If dt.Rows.Count > 0 Then
                cmbState.DataSource = dt
                cmbState.DisplayMember = dt.Columns(0).ColumnName
                cmbState.SelectedIndex = 0
            End If
        End If
        'If (IsNothing(oPatientReg) = False) Then    commented for   bugid 71311
        '    oPatientReg.Dispose()
        '    oPatientReg = Nothing
        'End If
    End Sub
    'Private Sub Fill_County()
    '    Dim dt As New DataTable
    '    dt = oPatientReg.getAllCounty()
    '    If IsNothing(dt) = False Then
    '        If dt.Rows.Count > 0 Then
    '            cmbCounty.DataSource = dt
    '            cmbCounty.ValueMember = dt.Columns(0).ColumnName
    '            cmbCounty.DisplayMember = dt.Columns(0).ColumnName
    '            cmbCounty.SelectedIndex = 0
    '        End If
    '    End If
    'End Sub
    'Get the selected zip details

#Region "To Fill the Zip"
    ''' <summary>
    ''' Dhruv 20100120 
    ''' To set the zip value into the c1 flex grid
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Fill_Zip()
        ''assignning the value of the zip during 
        txtZIPCode.Text = m_ZipCode
        txtCity.Text = m_City
        txtCounty.Text = m_County
        txtAreacode.Text = m_AreaCode

        ''assigning the back color to c1 flex
        txtZIPCode.BackColor = Color.White
        txtAreacode.BackColor = Color.White
        cmbState.BackColor = Color.White
        txtCounty.BackColor = Color.White

        ''Not to set the property.
        ''assigning the read only property
        'txtZIPCode.ReadOnly = True
        'cmbState.Enabled = False
        'txtCounty.ReadOnly = True


        'cmbState.DropDownStyle = ComboBoxStyle.Simple
        cmbState.Text = m_State

    End Sub
#End Region
#Region "over the toolstrip button clicked  clicked"
    Private Sub tlsp_MSTCPT_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_MSTCPT.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Save"
                    SaveMSTZIP() 'Save the operation done into the database
                Case "Close"
                    'Shubhangi 20091109
                    'Display this message for the confirmation of save
                    'Dim result As String = MsgBox("Do you want to save the changes ? ", MsgBoxStyle.Question + MsgBoxStyle.YesNo)
                    'If result = MsgBoxResult.Yes Then
                    'SaveMSTZIP()
                    'Else
                    CloseMSTZIP() 'Cancel the performed operation
                    'End If

            End Select

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

#Region "When key press Keyboard checking the value"

    ''' <summary>
    ''' Note ascii value 8 -- the backspace
    ''' and 48 to 57 -- represents the only numeric values 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtAreacode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAreacode.KeyPress
        'validate the  area code textbox
        Try
            If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
                e.Handled = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#Region "Validating the ZipCode" ''Sandip Darade 20100320
    Private Sub txtZIPCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtZIPCode.KeyPress
        ''Commented By Dhruv 20100102
        '    'VAlidate the Zip code textbox
        '    Try
        '        If Not ((e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57)) Or (e.KeyChar = ChrW(8))) Then
        '            e.Handled = True
        '        End If
        '    Catch ex As Exception
        '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    End Try

        'Sandip Darade 20100320
        Dim _strRegex As String = ""
        ''Allow digits only if country is US 
        If (gstrCountry = "US") Then
            _strRegex = "^([0-9]*)$"
        Else ''allow alphanumerics if country is Canada 
            _strRegex = "^([0-9a-zA-Z]*)$"
        End If
        If Not (e.KeyChar = Convert.ToChar(8)) Then

            If Regex.IsMatch(e.KeyChar.ToString(), _strRegex) = False Then
                e.Handled = True
            End If
            If (e.KeyChar = Convert.ToChar(32)) Then
                'Allow space 
                If txtZIPCode.Text <> "" Then
                    e.Handled = False

                End If
            End If
        End If

    End Sub

    Private Sub txtZIPCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtZIPCode.TextChanged
        'Sandip Darade 20100320
        ''Remove Special character 
        Dim _strRegex As String = ""
        ''Allow digits only if country is US 
        If (gstrCountry = "US") Then
            _strRegex = "[0-9]"
        Else ''allow alphanumerics if country is Canada 
            _strRegex = "^([0-9a-zA-Z])"
        End If
        Dim strZipcode As String = txtZIPCode.Text
        For Each c As Char In strZipcode
            If Regex.IsMatch(c.ToString(), _strRegex) = False Then

                strZipcode = strZipcode.Replace(c.ToString(), "")

            End If
        Next
        txtZIPCode.Text = strZipcode
    End Sub
#End Region
#End Region

    'Private Sub SetZIPDetails(ByVal nZIPCode As Int64, ByVal sCity As String, ByVal sState As String)
    Private Sub SetZIPDetails(ByVal nID As Int64)
        Try
            Dim con As New SqlConnection(GetConnectionString)
            'Dim _Query As String = "SELECT ISNULL(ZIP,0) AS Zip, ISNULL(City,'') as City, nID, ISNULL(County,'') as County, " _
            '        & " ISNULL(ST,'') AS ST, ISNULL(AreaCode,0) AS AreaCode FROM CSZ_MST WHERE Zip = " & nZIPCode & " " _
            '        & " AND City = '" & sCity & "' AND ST = '" & sState & "'"
            Dim _Query As String = "SELECT ISNULL(ZIP,0) AS Zip, ISNULL(City,'') as City, nID, ISNULL(County,'') as County, " _
                  & " ISNULL(ST,'') AS ST, ISNULL(AreaCode,0) AS AreaCode FROM CSZ_MST WHERE nID=" & nID & ""
            Dim adp As New SqlDataAdapter(_Query, con)
            Dim dt As New DataTable
            adp.Fill(dt)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    m_ID = Convert.ToInt64(dt.Rows(0)("nID"))
                    'm_State = dt.Rows(0)("ST")
                    'm_City = dt.Rows(0)("City")
                    m_State = dt.Rows(0)("ST")
                    m_City = dt.Rows(0)("City")
                    m_County = dt.Rows(0)("County")
                    m_AreaCode = dt.Rows(0)("AreaCode")
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''Commented By Dhruv 20100102 
#Region "Validating the text"
    '    ''' <summary>
    '    ''' Validating the text over the text box
    '    ''' </summary>
    '    ''' <param name="sender"></param>
    '    ''' <param name="e"></param>
    '    ''' <remarks></remarks>
    '    Private Sub txtZIPCode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtZIPCode.Validating
    '        Dim _isZipCode As Boolean = False
    '        _isZipCode = ValidatingNumericText(DirectCast(sender, TextBox))
    '        If _isZipCode = True Then
    '            MessageBox.Show("Please enter valid zip code", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            txtZIPCode.Focus()
    '        End If
    '    End Sub
    ''' <summary>
    ''' Dhruv 20100109
    ''' Validating the text for the zipCode and Areacode
    ''' Validating the text  
    ''' </summary>
    ''' <param name="textBox"></param>
    ''' <remarks></remarks>
    Private Function ValidatingNumericText(ByVal textBox As TextBox) As Boolean
        Dim _str As String
        Dim _strLength As Integer = 0
        Dim _Counter As Integer = 0
        Dim _flag As Integer = 0
        Try
            _str = textBox.Text.Trim()
            _strLength = _str.Length
            While (_Counter < _strLength)
                If ((Asc(_str(_Counter)) >= 48) And (Asc(_str(_Counter)) <= 57)) Then
                Else
                    _flag = 1
                    ''textBox.Text = ""
                    Exit While
                End If
                _Counter += 1
            End While


            If (_flag <> 0) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Validating the text over the text box
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtAreacode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtAreacode.Validating
        Dim _isAreaCode As Boolean = False
        _isAreaCode = ValidatingNumericText(DirectCast(sender, TextBox))

        If _isAreaCode = True Then
            MessageBox.Show("Please enter valid area code", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtAreacode.Focus()
        End If
    End Sub
    ''' <summary>
    ''' Dhruv 20100309 
    ''' if the Zip is less then 5 digit if entered then it will lead the zip code with '0'  (Zero)
    ''' </summary>
    ''' <param name="textbox"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ZipLeadingWithZero(ByVal textbox As TextBox) As String
        Dim _spacer As String = ""
        Dim zipCodeValue As String = ""
        Dim _zipLength As Int16
        Dim y As Integer
        Try
            _zipLength = textbox.Text.Length
            If _zipLength <> 5 Then                     '' ''checking the length of the value inside the text box
                'For y = 0 To 5 - _zipLength
                For y = _zipLength To 4                 ''We are only providing the 5 digit so if the lesser then 5 digit is entered then it will check and enter the '0' uptil the value is not 5 digit
                    _spacer = _spacer & "0"             ''variable is decalred for the inserting the zero
                Next
            End If
            zipCodeValue = _spacer & textbox.Text       ''after that bind the value to the txtbox.
            Return zipCodeValue                         ''it will returns the functions
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return Nothing
        End Try
    End Function

#End Region
#End Region

    
End Class