Imports gloDIControl.DrugInteractionCollection
Public Class ClsAutoScreening
    Inherits System.Windows.Forms.UserControl
    'Drugs Collection
    Public objDrugCol As gloInteractionCollection

    'Allergy Collection
    Public objAllerCol As gloInteractionCollection

    '-------------------------------------------------------
    'PatientMedicalCondition Collection
    Public objMedicalConditionCol As gloInteractionCollection
    '-------------------------------------------------------

    'Raised when error thrown in class
    Public Event Errevent(ByVal message As String)

    Public ResultCol As gloInteractionCollection
    Private _gloDrugInteraction As gloDrugInteraction
    Public m_DISeverityLevel As String
    Public m_DFASeverityLevel As String
    '-----------------------------------------
    Public m_ADESeverityLevel As String
    '-----------------------------------------
    Public m_DIDocLevel As String
    Public m_DFADocLevel As String
    Private _blnDFAScreenStatus As Boolean
    Sub New()
        MyBase.New()
        objDrugCol = Nothing
        objAllerCol = Nothing
        '-----------------------------------
        objMedicalConditionCol = Nothing
        '-----------------------------------
        _gloDrugInteraction = Nothing
        ResultCol = Nothing

        m_DISeverityLevel = ""
        m_DFASeverityLevel = ""
        '-----------------------------------
        m_ADESeverityLevel = ""
        '-----------------------------------
        m_DIDocLevel = ""
        m_DFADocLevel = ""

        ResultCol = New gloInteractionCollection
        objDrugCol = New gloInteractionCollection
        objAllerCol = New gloInteractionCollection
        '-----------------------------------------
        objMedicalConditionCol = New gloInteractionCollection
        '-----------------------------------------
    End Sub
    Public Sub mydispose()
        'If Not IsNothing(objgloScreening) Then
        '    objgloScreening.mydispose()
        '    objgloScreening = Nothing
        'End If
        'Dim i As Int32
        'If Not IsNothing(objDrugCol) Then
        '    If Not IsNothing(objDrugCol.GetGloInteractionCol) Then
        '        If objDrugCol.GetGloInteractionCol.Count > 0 Then
        '            For i = objDrugCol.GetGloInteractionCol.Count To 1 Step -1
        '                objDrugCol.GetGloInteractionCol.Remove(i)
        '            Next
        '            objDrugCol.GetGloInteractionCol = Nothing
        '        End If
        '        objDrugCol = Nothing
        '    End If
        'End If
        'If Not IsNothing(objAllerCol) Then
        '    If Not IsNothing(objAllerCol.GetGloInteractionCol) Then
        '        If objAllerCol.GetGloInteractionCol.Count > 0 Then
        '            For i = objAllerCol.GetGloInteractionCol.Count To 1 Step -1
        '                objAllerCol.GetGloInteractionCol.Remove(i)
        '            Next
        '            objAllerCol.GetGloInteractionCol = Nothing
        '        End If
        '        objAllerCol = Nothing
        '    End If
        'End If
        'If Not IsNothing(ResultCol) Then
        '    If ResultCol.Count > 0 Then
        '        For i = ResultCol.Count To 1 Step -1
        '            ResultCol.Remove(i)
        '        Next
        '    End If
        '    ResultCol = Nothing
        'End If
        If Not IsNothing(_gloDrugInteraction) Then
            _gloDrugInteraction.mydispose()
            _gloDrugInteraction = Nothing
        End If
        If Not IsNothing(objDrugCol) Then
            objDrugCol.Clear()
            objDrugCol.Dispose()
            objDrugCol = Nothing
        End If
        If Not IsNothing(objAllerCol) Then
            objAllerCol.Clear()
            objAllerCol.Dispose()
            objAllerCol = Nothing
        End If
        If Not IsNothing(ResultCol) Then
            ResultCol.Clear()
            ResultCol.Dispose()
            ResultCol = Nothing
        End If
        '---------------------------------------------
        If Not IsNothing(objMedicalConditionCol) Then
            objMedicalConditionCol.Clear()
            objMedicalConditionCol.Dispose()
            objMedicalConditionCol = Nothing
        End If
        '---------------------------------------------

    End Sub
    Public Property DFAScreenStatus() As Boolean
        Get
            Return _blnDFAScreenStatus
        End Get
        Set(ByVal value As Boolean)
            _blnDFAScreenStatus = value
        End Set
    End Property
    Public Sub AddDrugtocol(ByVal id As Int64, ByVal strdrugname As String, ByVal strroute As String, ByVal strdosage As String, ByVal strfrequency As String, ByVal strduration As String)
        Dim objdrug As New gloDrug
        Try

            objdrug.ID = id
            objdrug.Name = strdrugname
            objdrug.Route = strroute
            objdrug.Frequency = strfrequency
            objdrug.Dosage = strdosage
            objdrug.Duration = strduration
            'Add drugs to drugs collection
            objDrugCol.Add(objdrug)
        Catch ex As gloScreeningException
            Throw ex
        Catch ex As Exception
            Dim objex As New gloScreeningException
            objex.ErrMessage = ex.Message
            Throw objex
        End Try

    End Sub

    'For NDC Interaction
    'Public Sub AddDrugtocol(ByVal Id As Int64)
    '    Dim objmedication As New gloDrug
    '    Try
    '        'objmedication = New gloInteraction
    '        objmedication.ID = Id
    '        objmedication.Name = ""
    '        'Add allergies to Allergies Collection
    '        objDrugCol.Add(objmedication)
    '        objmedication = Nothing
    '    Catch ex As gloScreeningException
    '        Throw ex
    '    Catch ex As Exception
    '        Dim objex As New gloScreeningException
    '        objex.ErrMessage = ex.Message
    '        Throw objex
    '    End Try
    'End Sub
    Public Sub AddDrugtocol(ByVal Id As String)
        Dim objmedication As New gloDrug
        Try
            'objmedication = New gloInteraction
            objmedication.ID = Id
            objmedication.Name = ""
            'Add allergies to Allergies Collection
            objDrugCol.Add(objmedication)
            objmedication = Nothing
        Catch ex As gloScreeningException
            Throw ex
        Catch ex As Exception
            Dim objex As New gloScreeningException
            objex.ErrMessage = ex.Message
            Throw objex
        End Try
    End Sub
    'For NDC Interaction

    'Code Commented for optimsation by supriya
    'Public Sub AddDrugtocol(ByVal arrcol As Collection)
    '    Try
    '        Dim objallergy As gloInteraction
    '        Dim i As Int16
    '        objAllerCol = Nothing
    '        objAllerCol = New gloInteractionCollection
    '        For i = 1 To arrcol.Count
    '            objallergy = New gloInteraction
    '            objallergy.ID = CType(arrcol.Item(i), Int64)
    '            objallergy.Name = ""
    '            'Add allergies to Allergies Collection
    '            objAllerCol.Add(objallergy)
    '            objallergy = Nothing
    '        Next
    '    Catch ex As Exception
    '        RaiseEvent Errevent("Error Processing Screening")
    '    End Try

    'End Sub
    'Code Commented for optimsation by supriya
    Public Sub AddDrugtocol1(ByVal Id As String)
        Dim objallergy As New gloInteraction
        Try
            'objAllerCol = Nothing
            'objAllerCol = New gloInteractionCollection
            'objallergy = New gloInteraction
            objallergy.ID = Id
            objallergy.Name = ""
            'Add allergies to Allergies Collection
            objAllerCol.Add(objallergy)
            objallergy = Nothing
        Catch ex As gloScreeningException
            Throw ex
        Catch ex As Exception
            Dim objex As New gloScreeningException
            objex.ErrMessage = ex.Message
            Throw objex
        End Try

    End Sub

    'new function to fill the medicalcondition collection 
    '--------------------------------------------------------------------
    Public Sub AddDrugtocol2(ByVal Id As Int64)
        Dim objmedicalcondition As New gloInteraction
        Try
            'objMedicalConditionCol = Nothing
            'objMedicalConditionCol = New gloInteractionCollection

            'objmedicalcondition = New gloInteraction
            objmedicalcondition.ID = Id
            objmedicalcondition.Name = ""
            'Add allergies to Allergies Collection
            objMedicalConditionCol.Add(objmedicalcondition)
            objmedicalcondition = Nothing
        Catch ex As gloScreeningException
            Throw ex
        Catch ex As Exception
            Dim objex As New gloScreeningException
            objex.ErrMessage = ex.Message
            Throw objex
        End Try

    End Sub
    '--------------------------------------------------------------------

    Public Function PerformScreening() As Hashtable
        'Try
        '    Dim objhash As New Hashtable
        '    objgloScreening = New gloAllScreening(objDrugCol.GetGloInteractionCol, objAllerCol.GetGloInteractionCol)
        '    objgloScreening.SetScreeningAlert(m_ADESeverityLevel, m_DISeverityLevel, m_DFASeverityLevel, m_DIDocLevel, m_DFADocLevel)
        '    objgloScreening.PerformScreening(ResultCol)
        '    If Not IsNothing(objgloScreening.objgloInteractionCol) Then
        '        Dim col As Collection
        '        col = objgloScreening.objgloInteractionCol.GetGloInteractionCol
        '        If col.Count > 0 Then
        '            objhash = New Hashtable
        '            Dim i As Int16
        '            For i = 1 To col.Count
        '                objhash.Add(CType(col.Item(i), gloInteraction).Name, CType(col.Item(i), gloInteraction).ID)
        '            Next
        '        End If
        '    End If
        '    Return objhash
        'Catch ex As Exception
        '    RaiseEvent Errevent("Error Processing Screening")
        '    Return Nothing
        'End Try
        Dim objhash As New Hashtable
        objhash.Clear()
        Dim _gloInteractionCollection As New gloInteractionCollection
        Try
            _gloDrugInteraction = New gloDrugInteraction(objDrugCol, objAllerCol, objMedicalConditionCol)
            _gloDrugInteraction.DISeverityLevel = m_DISeverityLevel
            _gloDrugInteraction.DFASeverityLevel = m_DFASeverityLevel
            '--------------------------------------------------------
            _gloDrugInteraction.ADESeverityLevel = m_ADESeverityLevel
            '--------------------------------------------------------
            _gloDrugInteraction.DIDocLevel = m_DIDocLevel
            _gloDrugInteraction.DFADocLevel = m_DFADocLevel
            _gloDrugInteraction.DFAStatus = _blnDFAScreenStatus
            _gloDrugInteraction.PopulatePatientProfile()
            _gloDrugInteraction.PerformScreening(_gloInteractionCollection)
            ResultCol = _gloDrugInteraction.ReturnResultCol1
            If Not IsNothing(_gloInteractionCollection) Then

                If _gloInteractionCollection.Count > 0 Then
                    objhash = New Hashtable
                    Dim i As Int16
                    For i = 0 To _gloInteractionCollection.Count - 1
                        objhash.Add(_gloInteractionCollection.Item(i).Name, _gloInteractionCollection.Item(i).ID)
                    Next
                End If
            End If
            Return objhash
        Catch ex As gloScreeningException
            Throw ex
        Catch ex As Exception
            Dim objex As New gloScreeningException
            objex.ErrMessage = ex.Message
            Throw objex
        Finally
            _gloInteractionCollection.Clear()
            _gloInteractionCollection = Nothing
            'objhash.Clear()
            'objhash = Nothing
        End Try
    End Function
    
    Public Function PerformScreening(ByRef m_DrugAlert As String, ByVal m_NDCCode As String) As Hashtable
    
        Dim objhash As New Hashtable
        objhash.Clear()
        Dim _gloInteractionCollection As New gloInteractionCollection
        Try
            _gloDrugInteraction = New gloDrugInteraction(objDrugCol, objAllerCol, objMedicalConditionCol)
            _gloDrugInteraction.DISeverityLevel = m_DISeverityLevel
            _gloDrugInteraction.DFASeverityLevel = m_DFASeverityLevel
            '--------------------------------------------------------
            _gloDrugInteraction.ADESeverityLevel = m_ADESeverityLevel
            '--------------------------------------------------------
            _gloDrugInteraction.DIDocLevel = m_DIDocLevel
            _gloDrugInteraction.DFADocLevel = m_DFADocLevel

            _gloDrugInteraction.PopulatePatientProfile()
            _gloDrugInteraction.NDCCode = m_NDCCode 'For NDC Interaction
            _gloDrugInteraction.DFAStatus = _blnDFAScreenStatus
            _gloDrugInteraction.PerformScreening(_gloInteractionCollection)
            m_DrugAlert = _gloDrugInteraction.DrugAlert

            If Not IsNothing(_gloInteractionCollection) Then
                If _gloInteractionCollection.Count > 0 Then

                    objhash = New Hashtable
                    Dim i As Int16
                    For i = 0 To _gloInteractionCollection.Count - 1
                        objhash.Add(_gloInteractionCollection.Item(i).Name, _gloInteractionCollection.Item(i).ID)
                    Next
                End If
            End If
            Return objhash
        Catch ex As gloScreeningException
            Throw ex
        Catch ex As Exception
            Dim objex As New gloScreeningException
            objex.ErrMessage = ex.Message
            Throw objex
        Finally
            _gloInteractionCollection.Clear()
            _gloInteractionCollection = Nothing
            'objhash.Clear()
            'objhash = Nothing
        End Try
    End Function
    '
    Public ReadOnly Property DrugID(ByVal index As Int32) As Int64
        Get
            If Not IsNothing(ResultCol) Then
                If index <= ResultCol.Count Then
                    Return ResultCol.Item(index).ID
                Else
                    Return 0
                End If
            Else
                Return 0
            End If
        End Get
    End Property
    Public Sub SetScreeningAlert(ByVal ADESeverityLevel As String, ByVal DISeverityLevel As String, ByVal DFASeverityLevel As String, ByVal DIDocLevel As String, ByVal DFADocLevel As String)
        m_DISeverityLevel = DISeverityLevel
        m_DFASeverityLevel = DFASeverityLevel
        '------------------------------------
        m_ADESeverityLevel = ADESeverityLevel
        '------------------------------------
        m_DIDocLevel = DIDocLevel
        m_DFADocLevel = DFADocLevel
    End Sub

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'ClsAutoScreening
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Name = "ClsAutoScreening"
        Me.ResumeLayout(False)

    End Sub
End Class

