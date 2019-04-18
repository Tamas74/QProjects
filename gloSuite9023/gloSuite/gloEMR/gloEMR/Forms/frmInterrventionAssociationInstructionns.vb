Public Class frmInterrventionAssociationInstructionns


    '' ''    Enum InterventionAssociationType
    '' ''    Goal = 1
    '' ''    Nutrition = 2
    '' ''    Medication = 3
    '' ''    LabOrder = 4
    '' ''    Immunization = 5
    '' ''    Encounter = 6
    '' ''    Education = 7
    '' ''    End Enum
    Dim _nInterventionAssociationID As Int64 = 0
    Dim _nInterventionAssociationType As Integer = 0

    'Public Sub New(ByVal InterventionAssociationType As Integer, ByVal InterventionAssociationID As Int64)

    '    ' This call is required by the Windows Form Designer.
    '    InitializeComponent()

    '    ' Add any initialization after the InitializeComponent() call.
    '    '_nInterventionAssociationID = InterventionAssociationID

    '    '_nInterventionAssociationType = InterventionAssociationType ''type can be medication, education, nutritions and any other types sent from Intervention Association form

    'End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private sInstruction As String
    Public Property Instruction() As String
        Get
            Return sInstruction
        End Get
        Set(ByVal value As String)
            sInstruction = value
        End Set
    End Property

    'Private nAssociationID As Long
    'Public Property AssociationID() As Long
    '    Get
    '        Return nAssociationID
    '    End Get
    '    Set(ByVal value As Long)
    '        nAssociationID = value
    '    End Set
    'End Property

    'Private nInterventionType As Integer
    'Public Property InterventionType() As Integer
    '    Get
    '        Return nInterventionType
    '    End Get
    '    Set(ByVal value As Integer)
    '        nInterventionType = value
    '    End Set
    'End Property

    'Private Instructionarray() As ArrayList
    'Public Property Instruction() As ArrayList
    '    Get
    '        Return Instructionarray()
    '    End Get
    '    Set(ByVal value As ArrayList)
    '        Instructionarray() = value
    '    End Set
    'End Property
    'Private sMyList As List(Of String)()
    'Public Property InsMyList As List(Of String)()
    '    Get
    '        Return sMyList
    '    End Get
    '    'Set(ByVal value As List(Of String)())
    '    '    MyList = value
    '    'End Set
    '    Set(value As List(Of String)())
    '        sMyList = value
    '    End Set
    'End Property


    Private Sub tStrpSaveClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tStrpSaveClose.Click
        Try
            sInstruction = txtInterventionAssociationInstructions.Text
            'nInterventionType = _nInterventionAssociationType
            'nAssociationID = _nInterventionAssociationID





            'Dim oDB As New gloStream.gloDataBase.gloDataBase

            'Dim strSQL As String
            'Dim retval As Boolean
            'Try

            '    '_sPharmacyNotes = txtInterventionAssociationInstructions.Text
            '    'If _sPharmacyNotes <> "" Then '''''validation
            '    '    txtInterventionAssociationInstructions.MaxLength = txtInterventionAssociationInstructions.MaxLength - _sPharmacyNotes.Length
            '    '    If txtInterventionAssociationInstructions.Text.Length > txtInterventionAssociationInstructions.MaxLength Then
            '    '        MessageBox.Show("Maximum characters length has been exceeded.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    '        If (IsNothing(oDB) = False) Then
            '    '            oDB.Disconnect()
            '    '            oDB.Dispose()
            '    '            oDB = Nothing
            '    '        End If
            '    '        Exit Sub
            '    '    End If
            '    'Else

            '    'End If

            '    'If txtPharmacyNotes.Text <> "" Then
            '    '    _sPharmacyNotes = _sPharmacyNotes & Environment.NewLine & txtPharmacyNotes.Text
            '    '    _sPharmacyNotes = ReplaceSpecialCharacters(_sPharmacyNotes)
            '    '    strSQL = "update prescription set sNotes = '" & _sPharmacyNotes & "' where nprescriptionid = " & _nPrescriptionId & " and npatientid = " & _nPatientId
            '    '    oDB.Connect(GetConnectionString)
            '    '    retval = oDB.ExecuteNonSQLQuery(strSQL)


            '    'Else

            '    'End If

            '    '_sPharmacyNotes = txtInterventionAssociationInstructions.Text
            '    '_sPharmacyNotes = ReplaceSpecialCharacters(_sPharmacyNotes)
            '    'strSQL = "update prescription set sNotes = '" & _sPharmacyNotes & "' where nprescriptionid = " & _nPrescriptionId & " and npatientid = " & _nPatientId
            '    oDB.Connect(GetConnectionString)
            '    retval = oDB.ExecuteNonSQLQuery(strSQL)


            Me.Close()
        Catch ex As Exception
            'If (IsNothing(oDB) = False) Then
            '    oDB.Disconnect()
            '    oDB.Dispose()
            '    oDB = Nothing
            'End If
        Finally
            'If (IsNothing(oDB) = False) Then
            '    oDB.Disconnect()
            '    oDB.Dispose()
            '    oDB = Nothing
            'End If

        End Try
    End Sub


    Private Sub tlbbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbbtnClose.Click
        Me.Close()
    End Sub

    Private Sub frmInterrventionAssociationInstructionns_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            If Instruction <> "" Then
                txtInterventionAssociationInstructions.Text = Instruction
            End If
        Catch ex As Exception

        End Try
    End Sub

   


End Class


'Public Class frmInterrventionAssociationInstructionns

'    Public Shared Property AppConnectionString As String
'        Get
'        End Get
'        Set(value As String)
'        End Set
'    End Property

'    Public Shared Property PatientId As Long
'        Get
'        End Get
'        Set(value As Long)
'        End Set
'    End Property

'    Public Shared Property TransactionDate As DateTime
'        Get
'        End Get
'        Set(value As DateTime)
'        End Set
'    End Property
'End Class