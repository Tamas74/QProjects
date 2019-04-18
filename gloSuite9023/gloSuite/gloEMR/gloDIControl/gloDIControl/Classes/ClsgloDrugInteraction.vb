'Imports Medispan
'Imports Medispan.DIB
'Imports Medispan.Collections
'Imports Medispan.Configuration
'Imports Medispan.IREF.Business
'Imports Medispan.IREF.Render
'Imports Medispan.IREF.Container
Imports gloDIControl.DrugInteractionCollection
Imports gloCentralizedDIB
Imports System.Runtime.Serialization
Imports System.Linq
'Imports System.Windows.Forms
Imports System.Text

'Declare an Interface IDrugInteraction that must be implemneted
'by Class gloDrugInteraction
Interface IDrugInteraction
    Function PopulatePatientProfile() As Boolean
    Function PerformScreening(ByVal objInteractionCol As DrugInteractionCollection.gloInteractionCollection) As Boolean
    Property ResultSet() As String
End Interface
Interface Imonograph
    Function ShowMonograph(ByVal objInteractionresult As DrugInteractionCollection.gloInteraction) As String
    'Function ShowMonograph(ByVal ID As Int32) As String
End Interface

#Region "gloDrugInteraction"
'Define gloDrugInteraction and implement the populatepatientprofile
'function.Define PerformScreening function as overridable and override it         in derived classed to customise the screening logic.
Friend Class gloDrugInteraction
    Implements IDrugInteraction

  
    'patientmedicalcondition collection is added at the end of the constructor
    Sub New(ByVal Drugs As gloInteractionCollection, ByVal Allergies As gloInteractionCollection, ByVal PatientMedicalConditions As gloInteractionCollection)
        MyBase.New()
        '   objpatientprofile = New PatientProfile
        objDrugs = Drugs
        objallergies = Allergies
        '-------------------------------------------------
        objPatientMedicalCollection = PatientMedicalConditions
        '-------------------------------------------------
        m_DrugAlert = ""

        m_NDCCode = ""
    End Sub
    Public Sub New()
        MyBase.New()
    End Sub
    'Constructor Added for DI optimisation by supriya
    'Defines the XML tag to be used to do connectivity
    Protected DATASOURCE_ID As String = "SQLOleDb"
    Friend m_AgeInDays As String = ClsDIGeneral.PatientAgeInDays '"25915"
    'Declares a patientprofile object which will contain a collection of
    'Protected objpatientprofile As PatientProfile


    ' Code added - variable declarations for gloMedispan REST service - for Centralized DB implementation by Ujwala - as on 30092014 - Start
    Protected lstDrugsList As List(Of String) = New List(Of String)
    Protected lstAllergiesList As List(Of String) = New List(Of String)
    Protected lstMedicalConditionsList As List(Of Int32) = New List(Of Int32)
    ' Code added - variable declarations for gloMedispan REST service - for Centralized DB implementation by Ujwala - as on 30092014 - End

    Public objDrugs As gloInteractionCollection
    Public objallergies As gloInteractionCollection
    '-------------------------------------------------
    Public objPatientMedicalCollection As gloInteractionCollection 'object added by sagar on 6 aug 2007 to perform screening against the patient
    '-------------------------------------------------
    Friend objMedications As gloInteractionCollection
    Friend ResultCol As gloInteractionCollection
    Friend ResultCol1 As gloInteractionCollection
    Private m_ResultSet As String

    'For NDC Interaction

    Private m_NDCCode As String
    'For NDC Interaction
    Private m_DrugAlert As String


    Friend m_DISeverityFilter As String
    Friend m_DFASeverityfilter As String
    '-----------------------------------
    Friend m_ADESeverityfilter As Int16
    '-----------------------------------
    Friend m_DIDocLevel As Int16
    Friend m_DFADocLevel As Int16
    Private _blnDFAStatus As Boolean = True
    Friend Sub mydispose()        
        '   objpatientprofile = Nothing

        If Not IsNothing(objDrugs) Then
            objDrugs.Clear()
            objDrugs = Nothing
        End If
        If Not IsNothing(objallergies) Then
            objallergies.Clear()
            objallergies = Nothing
        End If
        '-------------------------------------------------
        If Not IsNothing(objPatientMedicalCollection) Then
            objPatientMedicalCollection.Clear()
            objPatientMedicalCollection = Nothing
        End If
        '-------------------------------------------------
        If Not IsNothing(objMedications) Then
            objMedications.Clear()
            objMedications = Nothing
        End If
        If Not IsNothing(ResultCol) Then
            ResultCol.Clear()
            ResultCol = Nothing
        End If
        If Not IsNothing(ResultCol1) Then
            ResultCol1.Clear()
            ResultCol1 = Nothing
        End If
    End Sub
    Friend WriteOnly Property DFAStatus() As Boolean
        Set(ByVal Value As Boolean)
            _blnDFAStatus = Value
        End Set
    End Property
    Friend WriteOnly Property DISeverityLevel() As String
        Set(ByVal Value As String)
            m_DISeverityFilter = Value
        End Set
    End Property
    Friend WriteOnly Property DFASeverityLevel() As String
        Set(ByVal Value As String)
            m_DFASeverityfilter = Value
        End Set
    End Property
    '-----------------------------------------------------
    Friend WriteOnly Property ADESeverityLevel() As String
        Set(ByVal Value As String)
            If Value = "Major" Then
                m_ADESeverityfilter = 0
            ElseIf Value = "Moderate" Then
                m_ADESeverityfilter = 1
            ElseIf Value = "Minor" Then
                m_ADESeverityfilter = 2
            End If
        End Set
    End Property
    '-----------------------------------------------------
    Friend WriteOnly Property DIDocLevel() As String
        Set(ByVal Value As String)
            If Value = "Established" Then
                m_DIDocLevel = 0
            ElseIf Value = "Probable" Then
                m_DIDocLevel = 1
            ElseIf Value = "Suspected" Then
                m_DIDocLevel = 2
            ElseIf Value = "Possible" Then
                m_DIDocLevel = 3
            ElseIf Value = "Doubtful" Then
                m_DIDocLevel = 4
            ElseIf Value = "None" Then
                m_DIDocLevel = 5
            End If
        End Set
    End Property
    Friend WriteOnly Property DFADocLevel() As String
        Set(ByVal Value As String)
            If Value = "Established" Then
                m_DFADocLevel = 0
            ElseIf Value = "Probable" Then
                m_DFADocLevel = 1
            ElseIf Value = "Suspected" Then
                m_DFADocLevel = 2
            ElseIf Value = "Possible" Then
                m_DFADocLevel = 3
            ElseIf Value = "Doubtful" Then
                m_DFADocLevel = 4
            ElseIf Value = "None" Then
                m_DFADocLevel = 5
            End If
        End Set
    End Property

#Region "gloDrugInteraction Functions"

    Public Overridable Function PerformScreening(ByVal objInteractionCol As DrugInteractionCollection.gloInteractionCollection) As Boolean Implements IDrugInteraction.PerformScreening
        Return False
    End Function

    Public Overridable Function PopulatePatientProfile() As Boolean Implements IDrugInteraction.PopulatePatientProfile
        Return False
    End Function

#End Region
#Region "gloDrugInteraction Properties"
    'Code commented for Drug Optimisation by supriya
    'Friend WriteOnly Property SetDrugList() As Collection
    '    Set(ByVal Value As Collection)
    '        objDrugs = Value
    '    End Set
    'End Property
    'Friend WriteOnly Property SetAllergyList() As Collection
    '    Set(ByVal Value As Collection)
    '        objallergies = Value
    '    End Set
    'End Property
    'Code commented for Drug Optimisation by supriya


    Friend WriteOnly Property SetAge() As String
        Set(ByVal Value As String)
            m_AgeInDays = Value
        End Set
    End Property
#End Region
    Public ReadOnly Property DrugAlert() As String
        Get
            Return m_DrugAlert
        End Get
    End Property

    Public Property NDCCode() As String
        Get
            Return m_NDCCode
        End Get
        Set(ByVal Value As String)
            m_NDCCode = Value
        End Set
    End Property


    Friend Property ResultSet() As String Implements IDrugInteraction.ResultSet
        Get
            Return m_ResultSet
        End Get
        Set(ByVal Value As String)
            m_ResultSet = Value
        End Set
    End Property

    Friend ReadOnly Property ReturnResultCol1() As gloInteractionCollection
        Get
            Return ResultCol1
        End Get

    End Property
End Class
Friend Class gloADEScreen
    Inherits gloDrugInteraction
    Sub New(ByVal Drugs As gloInteractionCollection, ByVal Allergies As gloInteractionCollection, ByVal PatientMedicalConditions As gloInteractionCollection, ByVal ADESeverityfilter As String)
        MyBase.new(Drugs, Allergies, PatientMedicalConditions)
        If ADESeverityFilter = "Major" Then
            m_ADESeverityfilter = 0
        ElseIf ADESeverityFilter = "Moderate" Then
            m_ADESeverityfilter = 1
        ElseIf ADESeverityFilter = "Minor" Then
            m_ADESeverityfilter = 2
        End If
    End Sub

    Sub New(ByVal ADESeverityfilter As String)
        'Call constructor for base class
        'and then execute constructor for the derived class

        MyBase.New() '---------------------
        If ADESeverityfilter = "Major" Then
            m_ADESeverityfilter = 0
        ElseIf ADESeverityfilter = "Moderate" Then
            m_ADESeverityfilter = 1
        ElseIf ADESeverityfilter = "Minor" Then
            m_ADESeverityfilter = 2
        End If
    End Sub
    Friend m_ID As Int64
    Friend Const DataSource_Id As String = "SQLOleDb"
    Friend m_ADESeverityfilter As Int16
#Region "gloADEScreen Properties"
    Public WriteOnly Property SearchID() As Int64
        Set(ByVal Value As Int64)
            m_ID = Value
        End Set
    End Property
#End Region

#Region "gloADEScreen Functions"
    Public Function PerformScreeningService(ByVal objInteractionCol As DrugInteractionCollection.gloInteractionCollection) As Boolean
        'Dim dfaFilter As New DFAFilter
        'Dim CentralizedDIBService As New gloCentralizedDIB.FormularyDIBService()
        'Dim arg As New gloCentralizedDIBArgument()
        'Dim sJSONArgument As String = String.Empty
        'Dim service As New gloCentralizedDIB.FormularyDIBService()
        'Dim sReturnedJSONString As String = String.Empty
        'Dim Converter As New gloCentralizedDIB.DIBJsonConverter(Of GroupedADEResults)
        'Dim GroupedADEReturnType As GroupedADEResults = Nothing
        'Dim objInteraction As DrugInteractionCollection.gloMonoInteraction = Nothing
        'Dim strADEScreen As New System.Text.StringBuilder

        'Try
        '    dfaFilter.MajorDocLevel = m_DFADocLevel
        '    dfaFilter.ModerateDocLevel = m_DFADocLevel
        '    dfaFilter.MinorDocLevel = m_DFADocLevel
        '    dfaFilter.InteractionType = DFAInteractionType.BOTH

        '    With arg
        '        .MajorDocLevel = m_DFADocLevel
        '        .ModerateDocLevel = m_DFADocLevel
        '        .MinorDocLevel = m_DFADocLevel
        '        .DFAInteractionType = DFAInteractionType.BOTH
        '        .PRCMgmtLevelFilter = PRCMgmtLevelFilter.EXTREME_CAUTION
        '        .INDProxyLevelFilter = INDProxyLevelFilter.CERTAIN
        '        .DIScreenResults_MajorDocLevel = m_DIDocLevel
        '        .DIScreenResults_MinorDocLevel = m_DIDocLevel
        '        .DIScreenResults_ModerateDocLevel = m_DIDocLevel
        '        .DrugNameTypeFilter = DrugNameTypeFilter.ALL
        '        .DrugsList = Me.lstDrugsList
        '        .AllergiesList = Me.lstAllergiesList
        '        .MedicalConditions = Me.lstMedicalConditionsList
        '    End With

        '    sJSONArgument = gloCentralizedDIB.FormularyDIBArgumentPacker.GetJsonString(arg)
        '    sReturnedJSONString = service.GetResponseString(sJSONArgument, URL.ADEScreening)
        '    GroupedADEReturnType = Converter.GetConvertedResults(sReturnedJSONString)
        '    ResultCol1 = New gloInteractionCollection

        '    If GroupedADEReturnType.ADEReturnList IsNot Nothing Then
        '        If GroupedADEReturnType.ADEReturnList.Any() Then
        '            For Each element As ADEReturnType In GroupedADEReturnType.ADEReturnList
        '                strADEScreen.Append(element.ScreenMessage)
        '                strADEScreen.Append(vbCr)

        '                strADEScreen.Append(vbTab)
        '                strADEScreen.Append("Effect Type: ")
        '                strADEScreen.Append(element.EffectTypeCodeDescription)
        '                strADEScreen.Append(vbCr)

        '                strADEScreen.Append(vbTab)
        '                strADEScreen.Append("Doc Level: ")
        '                strADEScreen.Append(element.DocLevelCodeDescription)
        '                strADEScreen.Append(vbCr)

        '                strADEScreen.Append(vbTab)
        '                strADEScreen.Append("Incidence: ")
        '                strADEScreen.Append(element.IncidenceCodeDescription)
        '                strADEScreen.Append(vbCr)

        '                strADEScreen.Append(vbTab)
        '                strADEScreen.Append("Onset: ")
        '                strADEScreen.Append(element.OnsetCodeDescription)
        '                strADEScreen.Append(vbCr)

        '                strADEScreen.Append(vbTab)
        '                strADEScreen.Append("Severity: ")
        '                strADEScreen.Append(element.SeverityCodeDescription)
        '                strADEScreen.Append(vbCr)
        '                strADEScreen.Append(vbCr)

        '                objInteraction = New DrugInteractionCollection.gloInteraction

        '                objInteraction.Name = strADEScreen.ToString
        '                objInteractionCol.Add(objInteraction)
        '                objInteraction = Nothing
        '                strADEScreen.Remove(0, strADEScreen.Length)

        '            Next
        '        End If
        '    End If

        '    If GroupedADEReturnType.ADEMessageList IsNot Nothing Then
        '        If GroupedADEReturnType.ADEMessageList.Any() Then
        '            strADEScreen.Remove(0, strADEScreen.Length)

        '            For Each element As ADEMessageType In GroupedADEReturnType.ADEMessageList
        '                strADEScreen.Append(element.MessageItemType)
        '                strADEScreen.Append(": ")
        '                strADEScreen.Append(element.MessageText)
        '                strADEScreen.Append(" (")
        '                strADEScreen.Append(element.ItemDescription)
        '                strADEScreen.Append(") ")
        '                strADEScreen.Append(System.Environment.NewLine)
        '            Next
        '        End If
        '    End If

        '    If Not IsNothing(strADEScreen) Then
        '        ResultSet = strADEScreen.ToString(0, strADEScreen.Length)
        '    End If

        '    Return False
        'Catch ex As Exception
        '    Dim objgloScreeningException As New gloScreeningException
        '    objgloScreeningException.ErrMessage = ex.Message
        '    Throw objgloScreeningException
        'Finally
        '    If objInteraction IsNot Nothing Then
        '        objInteraction = Nothing
        '    End If

        '    If arg IsNot Nothing Then
        '        arg.Dispose()
        '        arg = Nothing
        '    End If

        '    sJSONArgument = String.Empty
        '    sReturnedJSONString = String.Empty

        '    If service IsNot Nothing Then
        '        service = Nothing
        '    End If

        '    If Converter IsNot Nothing Then
        '        Converter = Nothing
        '    End If

        '    If GroupedADEReturnType IsNot Nothing Then
        '        GroupedADEReturnType.Dispose()
        '        GroupedADEReturnType = Nothing
        '    End If                
        'End Try
        Return False
       
    End Function

    Public Overrides Function PerformScreening(ByVal objInteractionCol As DrugInteractionCollection.gloInteractionCollection) As Boolean
        'If gloCentralizedDIB.InitialiseServiceVars.gblnUseDIBService = True Then
        '    Return Me.PerformScreeningService(objInteractionCol)
        'Else
        '    Dim adeScreenResults As ADEScreenResults
        '    Dim objcode As Code
        '    Dim strADEScreen As New System.Text.StringBuilder

        '    'In this example, all screening is done
        '    'retrospectively - that is, all drugs, conditions, and allergies
        '    ' are patiented against each other. ProspectiveOnly is False.
        '    'Patient for Adverse Drug Effects. The severity filter is set with
        '    ' an ADESeverityFilter enumeration, in this case,
        '    ' ADESeverityFilter.MINOR.
        '    'This means that alerts will be returned if their severity filter
        '    'is Minor, Moderate or Major. The severity filter in this case is
        '    'a non-exclusive, low end parameter. The messages collection
        '    'is empty before the method call is made.
        '    'Insert the collections created from the Patient's profile
        '    'as the parameters for the patienting method call.
        '    adeScreenResults = Screening.ADEScreen(objpatientprofile, False, m_ADESeverityfilter)
        '    If adeScreenResults.Count > 0 Then
        '        Dim objinteraction As DrugInteractionCollection.gloInteraction

        '        Dim index As Integer

        '        For index = 0 To adeScreenResults.Count - 1
        '            'process the alerts....
        '            strADEScreen.Append(adeScreenResults(index).ScreenMessage)
        '            strADEScreen.Append(vbCr)
        '            'The code local variable is created not by the new keyword,
        '            'but by loading relevant information. In this case, a
        '            'datasource, the ADETypes for the EffectTypeCode from the
        '            'enumeration CodeTypes, and the actual value for
        '            'the EffectTypeCode gotten by the screening itself. This is //replicated
        '            'by each code load, using the respective
        '            'Screening(Process)
        '            '37-16 Drug Information Bridge 3.1
        '            'Published: 05/02
        '            'Revised: 08/05
        '            'enumeration to the respective screen
        '            'information to be displayed. code.Description retreives
        '            'these strings.
        '            objcode = Code.LoadByTypeAndId(DataSource_Id, CodeTypes.ADETypes, adeScreenResults(index).EffectTypeCode)
        '            strADEScreen.Append(vbTab)
        '            strADEScreen.Append("Effect Type: ")
        '            strADEScreen.Append(objcode.Description)
        '            strADEScreen.Append(vbCr)
        '            objcode = Code.LoadByTypeAndId(DataSource_Id, CodeTypes.ADEDocumentationLevels, adeScreenResults(index).DocLevelCode)
        '            strADEScreen.Append(vbTab)
        '            strADEScreen.Append("Doc Level: ")
        '            strADEScreen.Append(objcode.Description)
        '            strADEScreen.Append(vbCr)
        '            objcode = Code.LoadByTypeAndId(DataSource_Id, CodeTypes.ADEIncidences, adeScreenResults(index).IncidenceCode)
        '            strADEScreen.Append(vbTab)
        '            strADEScreen.Append("Incidence: ")
        '            strADEScreen.Append(objcode.Description)
        '            strADEScreen.Append(vbCr)
        '            objcode = Code.LoadByTypeAndId(DataSource_Id, CodeTypes.ADEOnsets, adeScreenResults(index).OnsetCode)
        '            strADEScreen.Append(vbTab)
        '            strADEScreen.Append("Onset: ")
        '            strADEScreen.Append(objcode.Description)
        '            strADEScreen.Append(vbCr)
        '            objcode = Code.LoadByTypeAndId(DataSource_Id, CodeTypes.ADESeverities, adeScreenResults(index).SeverityCode)
        '            strADEScreen.Append(vbTab)
        '            strADEScreen.Append("Severity: ")
        '            strADEScreen.Append(objcode.Description)
        '            strADEScreen.Append(vbCr)
        '            strADEScreen.Append(vbCr)

        '            objinteraction = New DrugInteractionCollection.gloInteraction

        '            objinteraction.Name = strADEScreen.ToString
        '            'CType(objinteraction, gloInteractionResult).MonographID = adeScreenResults(index).MonographId
        '            objInteractionCol.Add(objinteraction)
        '            objinteraction = Nothing
        '            strADEScreen.Remove(0, strADEScreen.Length)
        '            'strADEScreen = Nothing

        '        Next
        '    End If
        '    If adeScreenResults.Messages.Count > 0 Then
        '        strADEScreen.Remove(0, strADEScreen.Length)


        '        Dim index As Integer
        '        For index = 0 To adeScreenResults.Messages.Count - 1
        '            strADEScreen.Append(adeScreenResults.Messages(index).ItemType)
        '            strADEScreen.Append(": ")
        '            strADEScreen.Append(adeScreenResults.Messages(index).MessageText)
        '            strADEScreen.Append(" (")
        '            strADEScreen.Append(adeScreenResults.Messages(index).ItemDescription)
        '            strADEScreen.Append(") ")
        '            strADEScreen.Append(System.Environment.NewLine)
        '        Next
        '    End If
        '    If Not IsNothing(strADEScreen) Then
        '        ResultSet = strADEScreen.ToString(0, strADEScreen.Length)
        '    End If
        '    Return False
        'End If

        Return False

    End Function
    '*******************************Code unnecessary so commented for optimsation by supriya
    'Get Adverse Drug Effects for a selected dispensable drug
    'Public Function PerformScreening(ByVal objinteraction As DrugInteractionCollection.gloInteractionCollection) As Boolean
    '    Try
    '        Const MAX_RESULTS As Integer = 20
    '        Const TAB As String = vbTab
    '        ' Loads the DispensableDrug object with a dispensableDrugID
    '        Dim dispensableDrug As New DispensableDrug
    '        If objDrugs.Count > 0 Then
    '            'Pass the drug Id to load dispensable drug

    '            dispensableDrug.LoadById(DataSource_Id, m_ID)

    '            ' Use a DispensableDrug object to retrieve a
    '            ' collection of ADELookupResults. Three filters
    '            ' are required: severity, incidence, and doc level.
    '            ' These filters can vary to include more, fewer,
    '            ' or different search results. Any of the filters
    '            ' from the ADESeverityFilter, ADEIncidenceFilter,
    '            ' and ADEDocLevelFilter enumerations.
    '            Dim results As ADELookupResults

    '            'results = dispensableDrug.GetAdverseEffects(ADESeverityFilter.MODERATE, ADEIncidenceFilter.UNKNOWN_MORE_FREQUENT, ADEDocLevelFilter.POSSIBLE)
    '            results = dispensableDrug.GetAdverseEffects(m_ADESeverityfilter, ADEIncidenceFilter.UNKNOWN_MORE_FREQUENT, ADEDocLevelFilter.POSSIBLE)
    '            Dim displayText As New System.Text.StringBuilder("AdverseDrugEffects Report For ")

    '            displayText.Append(dispensableDrug.Description)
    '            displayText.Append(System.Environment.NewLine)
    '            '
    '            Dim item As New System.Text.StringBuilder
    '            Dim majorSeverity As String = ""
    '            Dim moderateSeverity As String = ""
    '            Dim minorSeverity As String = ""
    '            ' Classify each result based on its severity,
    '            ' then display information about that result.
    '            ' If there are more than the maximum number of
    '            ' desired results (here this is set to 20), the
    '            ' extras will not be displayed. Some result sets
    '            ' are very large. Full results can be printed
    '            ' if the MAX_RESULTS limitation is not checked.
    '            Dim i As Int32 = 0
    '            For i = 0 To MAX_RESULTS - 1 And results.Count - 1
    '                If (True) Then
    '                    Dim result As ADELookupResult = results(i)
    '                    ' Get the Type code for the ADELookupResult
    '                    ' object.
    '                    Dim effectTypeCode As Code = Code.LoadByTypeAndId(DataSource_Id, CodeTypes.ADETypes, result.EffectTypeCode)
    '                    ' Create an output string for the current
    '                    ' result.
    '                    item.Append(vbTab)
    '                    item.Append(result.MedCondNameProf)
    '                    item.Append(" (")
    '                    item.Append(effectTypeCode.Description)
    '                    item.Append(") ")
    '                    ' Examine specific cases for the result.
    '                    ' If it meets any of these conditions,
    '                    ' add a message about that condition to the
    '                    ' display.
    '                    If result.RestrictionId <> 0 Then
    '                        item.Append(" (restricted) ")
    '                    End If
    '                    If result.SpecialConditions.Count > 0 Then
    '                        item.Append(" (with special conditions) ")
    '                    End If
    '                    item.Append(System.Environment.NewLine)
    '                    ' Add each of the comments related to
    '                    ' the result to the output.
    '                    Dim comment As Comment
    '                    For Each comment In result.Comments
    '                        item.Append(vbTab)
    '                        item.Append(vbTab)
    '                        item.Append("(")
    '                        item.Append(comment.Text)
    '                        item.Append(")")
    '                        item.Append(System.Environment.NewLine)
    '                    Next comment
    '                    ' The SeverityCode field of result will
    '                    ' be equal to "01" if it is a major severity.
    '                    Const MAJOR As String = "01"
    '                    ' The SeverityCode field of result will
    '                    ' be equal to "02" if it is a moderate severity.
    '                    Const MODERATE As String = "02"
    '                    ' The SeverityCode field of result will
    '                    ' be equal to "03" if it is a minor severity.
    '                    Const MINOR As String = "03"
    '                    ' Add the information about the current result
    '                    ' to the proper category
    '                    Select Case result.SeverityCode
    '                        Case MAJOR
    '                            majorSeverity = item.ToString()
    '                        Case MODERATE
    '                            moderateSeverity = item.ToString()
    '                        Case MINOR
    '                            minorSeverity = item.ToString()
    '                    End Select
    '                End If
    '            Next
    '            ' If the results is greater than MAX_RESULTS, add message
    '            If results.Count >= MAX_RESULTS Then
    '                displayText.Append(vbTab)
    '                displayText.Append("This Is Partial List. Some Results Omitted.")
    '                displayText.Append("vbcr")
    '                displayText.Append("vbcr")
    '            End If
    '            ' Depeding on the severity, add the proper results
    '            ' to the displayText.
    '            If majorSeverity <> "" Then
    '                displayText.Append("Major Severity:")
    '                displayText.Append("vbcr")
    '                displayText.Append(majorSeverity)
    '                displayText.Append("vbcr")
    '            End If
    '            If moderateSeverity <> "" Then
    '                displayText.Append("Moderate Severity:")
    '                displayText.Append("vbcr")
    '                displayText.Append(moderateSeverity)
    '                displayText.Append("vbcr")
    '            End If
    '            If minorSeverity <> "" Then
    '                displayText.Append("Minor Severity:")
    '                displayText.Append("vbcr")
    '                displayText.Append(minorSeverity)
    '                displayText.Append("vbcr")
    '            End If
    '            ' Print results to console
    '            'Console.WriteLine(displayText)
    '            ResultSet = displayText.ToString(0, displayText.Length)
    '        End If
    '    Catch ex As Exception
    '        Dim objgloScreeningException As New gloScreeningException
    '        Throw objgloScreeningException
    '    End Try
    'End Function
    '*******************************Code unnecessary so commented for optimsation by supriya
    'Friend Function GetAdverseEffectsDescForMedicalConditions(ByVal results As ADELookupResults) As String

    '    'Const MAX_RESULTS As Integer = 20

    '    'Dim item As New System.Text.StringBuilder
    '    'Dim displaytext As New System.Text.StringBuilder
    '    'Dim majorSeverity As String = ""
    '    'Dim moderateSeverity As String = ""
    '    'Dim minorSeverity As String = ""
    '    'Try
    '    '    ' Classify each result based on its severity,
    '    '    ' then display information about that result.
    '    '    ' If there are more than the maximum number of
    '    '    ' desired results (here this is set to 20), the
    '    '    ' extras will not be displayed. Some result sets
    '    '    ' are very large.Full results can be printed
    '    '    ' if the MAX_RESULTS limitation is not checked.
    '    '    Dim i As Int32 = 0
    '    '    If Not IsNothing(results) Then
    '    '        For i = 0 To MAX_RESULTS - 1 And results.Count - 1
    '    '            ' Dim result As ADELookupResult = results(i)
    '    '            ' Create an output string for the current
    '    '            ' result.

    '    '            item.Append(vbTab)
    '    '            item.Append(result.DispensableDrugDescription)
    '    '            ' Examine specific cases for the result.
    '    '            ' If it meets any of these conditions,
    '    '            ' add a message about that condition to the
    '    '            ' display.
    '    '            If result.RestrictionId <> 0 Then
    '    '                item.Append(" (restricted) ")
    '    '            End If
    '    '            If result.SpecialConditions.Count > 0 Then
    '    '                item.Append(" (with special conditions) ")
    '    '            End If
    '    '            item.Append(vbCr)
    '    '            ' Add each of the comments related to
    '    '            ' the result to the output.
    '    '            Dim comment As Comment
    '    '            For Each comment In result.Comments
    '    '                item.Append(vbTab)

    '    '                item.Append(vbTab)
    '    '                item.Append("(")
    '    '                item.Append(comment.Text)
    '    '                item.Append(")")
    '    '                item.Append(vbCr)
    '    '            Next comment
    '    '            ' The SeverityCode field of result will
    '    '            ' be equal to "01" if it is a major severity.
    '    '            Const MAJOR As String = "01"
    '    '            ' The SeverityCode field of result will
    '    '            ' be equal to "02" if it is a moderate severity.
    '    '            Const MODERATE As String = "02"
    '    '            ' The SeverityCode field of result will
    '    '            ' be equal to "03" if it is a minor severity.
    '    '            Const MINOR As String = "03"
    '    '            ' Add the information about the current result
    '    '            ' to the proper category
    '    '            Select Case result.SeverityCode
    '    '                Case MAJOR
    '    '                    majorSeverity = item.ToString()
    '    '                Case MODERATE
    '    '                    moderateSeverity = item.ToString()
    '    '                Case MINOR
    '    '                    minorSeverity = item.ToString()
    '    '            End Select
    '    '        Next
    '    '        ' Create the results depending on the severity
    '    '        ' Create the results depending on the severity



    '    '        displaytext.Append(item.ToString)

    '    '        displaytext.Append(vbCr)
    '    '        displaytext.Append(vbCr)

    '    '        If (results.Count >= MAX_RESULTS) Then

    '    '            'ToDo: Error processing original source shown below
    '    '            '// Create the results depending on the severity
    '    '            displaytext.Append(vbTab)
    '    '            displaytext.Append("This Is Partial List. Some Results Omitted.")
    '    '            displaytext.Append(vbCr)
    '    '            displaytext.Append(vbCr)
    '    '        End If

    '    '        If majorSeverity <> "" Then
    '    '            displaytext.Append("Major Severity:")
    '    '            displaytext.Append(vbCr)
    '    '            displaytext.Append(majorSeverity)
    '    '            displaytext.Append(vbCr)
    '    '        End If
    '    '        If moderateSeverity <> "" Then
    '    '            displaytext.Append("Moderate Severity:")
    '    '            displaytext.Append(vbCr)
    '    '            displaytext.Append(moderateSeverity)
    '    '            displaytext.Append(vbCr)
    '    '        End If
    '    '        If minorSeverity <> "" Then
    '    '            displaytext.Append("Minor Severity:")
    '    '            displaytext.Append(vbCr)
    '    '            displaytext.Append(minorSeverity)
    '    '            displaytext.Append(vbCr)
    '    '        End If

    '    '    End If

    '    '    Return displaytext.ToString(0, displaytext.Length)
    '    'Catch ex As Exception
    '    '    Dim objex As New gloScreeningException
    '    '    objex.ErrMessage = "Cannot retrieve Adverse Drug Effect List for Medical Condition"
    '    '    Return ""
    '    '    Throw objex
    '    'End Try
    '    Return ""
    'End Function

    'Friend Function GetScreeningDescFordrugs(ByVal results As ADELookupResults) As String
    '    If gloCentralizedDIB.InitialiseServiceVars.gblnUseDIBService = True Then
    '        Return Me.GetScreeningDescFordrugsService(results)
    '    Else
    '        Const MAX_RESULTS As Integer = 20

    '        Dim item As New System.Text.StringBuilder
    '        Dim majorSeverity As String = ""
    '        Dim moderateSeverity As String = ""
    '        Dim minorSeverity As String = ""
    '        Dim displayText As New System.Text.StringBuilder("AdverseDrugEffects Report For ")
    '        Try
    '            Dim i As Int32 = 0
    '            If Not IsNothing(results) Then
    '                For i = 0 To results.Count - 1
    '                    If (True) Then
    '                        Dim result As ADELookupResult = results(i)
    '                        ' Get the Type code for the ADELookupResult
    '                        ' object.
    '                        Dim effectTypeCode As Code = Code.LoadByTypeAndId(DataSource_Id, CodeTypes.ADETypes, result.EffectTypeCode)
    '                        ' Create an output string for the current
    '                        ' result.
    '                        item.Append(vbTab)
    '                        item.Append(result.MedCondNameProf)
    '                        item.Append("(")
    '                        item.Append(effectTypeCode.Description)
    '                        item.Append(") ")
    '                        ' Examine specific cases for the result.
    '                        ' If it meets any of these conditions,
    '                        ' add a message about that condition to the
    '                        ' display.
    '                        If result.RestrictionId <> 0 Then
    '                            item.Append(" (restricted) ")
    '                        End If
    '                        If result.SpecialConditions.Count > 0 Then
    '                            item.Append(" (with special conditions) ")
    '                        End If
    '                        item.Append(System.Environment.NewLine)
    '                        ' Add each of the comments related to
    '                        ' the result to the output.
    '                        Dim comment As Comment
    '                        For Each comment In result.Comments
    '                            item.Append(vbTab)
    '                            item.Append(vbTab)
    '                            item.Append("(")
    '                            item.Append(comment.Text)
    '                            item.Append(")")
    '                            item.Append(vbCr)
    '                        Next comment
    '                        ' The SeverityCode field of result will
    '                        ' be equal to "01" if it is a major severity.
    '                        Const MAJOR As String = "01"
    '                        ' The SeverityCode field of result will
    '                        ' be equal to "02" if it is a moderate severity.
    '                        Const MODERATE As String = "02"
    '                        ' The SeverityCode field of result will
    '                        ' be equal to "03" if it is a minor severity.
    '                        Const MINOR As String = "03"
    '                        ' Add the information about the current result
    '                        ' to the proper category
    '                        Select Case result.SeverityCode
    '                            Case MAJOR
    '                                majorSeverity = item.ToString()
    '                            Case MODERATE
    '                                moderateSeverity = item.ToString()
    '                            Case MINOR
    '                                minorSeverity = item.ToString()
    '                        End Select
    '                    End If
    '                Next
    '                ' If the results is greater than MAX_RESULTS, add message
    '                displayText.Append(item.ToString)
    '                displayText.Append(vbCr)
    '                displayText.Append(vbCr)
    '                If results.Count >= MAX_RESULTS Then
    '                    displayText.Append(vbTab)
    '                    displayText.Append("This Is Partial List. Some Results Omitted.")
    '                    displayText.Append(vbCr)
    '                    displayText.Append(vbCr)
    '                End If
    '                ' Depeding on the severity, add the proper results
    '                ' to the displayText.
    '                If majorSeverity <> "" Then
    '                    displayText.Append("Major Severity:")
    '                    displayText.Append(vbCr)
    '                    displayText.Append(majorSeverity)
    '                    displayText.Append(vbCr)
    '                End If
    '                If moderateSeverity <> "" Then
    '                    displayText.Append("Moderate Severity:")
    '                    displayText.Append(vbCr)
    '                    displayText.Append(moderateSeverity)
    '                    displayText.Append(vbCr)
    '                End If
    '                If minorSeverity <> "" Then
    '                    displayText.Append("Minor Severity:")
    '                    displayText.Append(vbCr)
    '                    displayText.Append(minorSeverity)
    '                    displayText.Append(vbCr)
    '                End If
    '                ' Print results to console
    '                'Console.WriteLine(displayText)
    '                Return displayText.ToString(0, displayText.Length)
    '            Else
    '                Return Nothing
    '            End If
    '        Catch ex As Exception
    '            Dim objex As New gloScreeningException
    '            objex.ErrMessage = "Could not Retrieve the Adverse Drug Effects for given drug"
    '            Throw objex
    '            Return Nothing
    '        End Try
    '    End If
    'End Function

    'Private Function GetScreeningDescFordrugsService(ByVal results As ADELookupResults) As String
    '    Const MAX_RESULTS As Integer = 20
    '    Dim item As New System.Text.StringBuilder
    '    Dim majorSeverity As String = ""
    '    Dim moderateSeverity As String = ""
    '    Dim minorSeverity As String = ""
    '    Dim displayText As New System.Text.StringBuilder("AdverseDrugEffects Report For ")

    '    Dim arg As New gloCentralizedADEArgument()

    '    If results IsNot Nothing AndAlso results.Count > 0 Then
    '        For Each element As ADELookupResult In results
    '            arg.EffectTypeCode.Add(element.EffectTypeCode)
    '        Next
    '    End If

    '    Dim sJSONArgument As String = String.Empty
    '    Dim service As New gloCentralizedDIB.FormularyDIBService()
    '    Dim sReturnedJSONString As String = String.Empty
    '    Dim Converter As New gloCentralizedDIB.DIBJsonConverter(Of EffectTypeCodeDescriptionList)
    '    Dim EffectTypeCodeDescriptionList As EffectTypeCodeDescriptionList = Nothing

    '    Try
    '        sJSONArgument = gloCentralizedDIB.FormularyDIBArgumentPacker.GetJsonString(arg)
    '        sReturnedJSONString = service.GetResponseString(sJSONArgument, URL.GetEffectTypeCodeList)
    '        EffectTypeCodeDescriptionList = Converter.GetConvertedResults(sReturnedJSONString)

    '        Dim i As Int32 = 0
    '        If Not IsNothing(results) Then
    '            For i = 0 To results.Count - 1
    '                If (True) Then
    '                    Dim result As ADELookupResult = results(i)
    '                    ' Get the Type code for the ADELookupResult
    '                    ' object.


    '                    'Dim effectTypeCode As Code = Code.LoadByTypeAndId(DataSource_Id, CodeTypes.ADETypes, result.EffectTypeCode)
    '                    ' Create an output string for the current
    '                    ' result.
    '                    item.Append(vbTab)
    '                    item.Append(result.MedCondNameProf)
    '                    item.Append("(")
    '                    item.Append(EffectTypeCodeDescriptionList.EffectDescriptionList(i))
    '                    item.Append(") ")
    '                    ' Examine specific cases for the result.
    '                    ' If it meets any of these conditions,
    '                    ' add a message about that condition to the
    '                    ' display.
    '                    If result.RestrictionId <> 0 Then
    '                        item.Append(" (restricted) ")
    '                    End If
    '                    If result.SpecialConditions.Count > 0 Then
    '                        item.Append(" (with special conditions) ")
    '                    End If
    '                    item.Append(System.Environment.NewLine)
    '                    ' Add each of the comments related to
    '                    ' the result to the output.
    '                    Dim comment As Comment
    '                    For Each comment In result.Comments
    '                        item.Append(vbTab)
    '                        item.Append(vbTab)
    '                        item.Append("(")
    '                        item.Append(comment.Text)
    '                        item.Append(")")
    '                        item.Append(vbCr)
    '                    Next comment
    '                    ' The SeverityCode field of result will
    '                    ' be equal to "01" if it is a major severity.
    '                    Const MAJOR As String = "01"
    '                    ' The SeverityCode field of result will
    '                    ' be equal to "02" if it is a moderate severity.
    '                    Const MODERATE As String = "02"
    '                    ' The SeverityCode field of result will
    '                    ' be equal to "03" if it is a minor severity.
    '                    Const MINOR As String = "03"
    '                    ' Add the information about the current result
    '                    ' to the proper category
    '                    Select Case result.SeverityCode
    '                        Case MAJOR
    '                            majorSeverity = item.ToString()
    '                        Case MODERATE
    '                            moderateSeverity = item.ToString()
    '                        Case MINOR
    '                            minorSeverity = item.ToString()
    '                    End Select
    '                End If
    '            Next
    '            ' If the results is greater than MAX_RESULTS, add message
    '            displayText.Append(item.ToString)
    '            displayText.Append(vbCr)
    '            displayText.Append(vbCr)
    '            If results.Count >= MAX_RESULTS Then
    '                displayText.Append(vbTab)
    '                displayText.Append("This Is Partial List. Some Results Omitted.")
    '                displayText.Append(vbCr)
    '                displayText.Append(vbCr)
    '            End If
    '            ' Depeding on the severity, add the proper results
    '            ' to the displayText.
    '            If majorSeverity <> "" Then
    '                displayText.Append("Major Severity:")
    '                displayText.Append(vbCr)
    '                displayText.Append(majorSeverity)
    '                displayText.Append(vbCr)
    '            End If
    '            If moderateSeverity <> "" Then
    '                displayText.Append("Moderate Severity:")
    '                displayText.Append(vbCr)
    '                displayText.Append(moderateSeverity)
    '                displayText.Append(vbCr)
    '            End If
    '            If minorSeverity <> "" Then
    '                displayText.Append("Minor Severity:")
    '                displayText.Append(vbCr)
    '                displayText.Append(minorSeverity)
    '                displayText.Append(vbCr)
    '            End If
    '            ' Print results to console
    '            'Console.WriteLine(displayText)
    '            Return displayText.ToString(0, displayText.Length)
    '        Else
    '            Return Nothing
    '        End If
    '    Catch ex As Exception
    '        Dim objex As New gloScreeningException
    '        objex.ErrMessage = "Could not Retrieve the Adverse Drug Effects for given drug"
    '        Throw objex
    '        Return Nothing
    '    Finally
    '        If arg IsNot Nothing Then
    '            arg.Dispose()
    '            arg = Nothing
    '        End If

    '        sJSONArgument = String.Empty
    '        sReturnedJSONString = String.Empty

    '        If service IsNot Nothing Then
    '            service = Nothing
    '        End If

    '        If Converter IsNot Nothing Then
    '            Converter = Nothing
    '        End If

    '        If EffectTypeCodeDescriptionList IsNot Nothing Then
    '            EffectTypeCodeDescriptionList.Dispose()
    '            EffectTypeCodeDescriptionList = Nothing
    '        End If
    '    End Try
    'End Function
    Friend Function GetAdverseEffectsForMedicalConditionService(ByVal id As Int64) As GroupedADELookupResult
        Dim Argument As New ADEAdverseEffectsArg()
        Dim sJSONArgument As String = String.Empty
        Dim service As New gloCentralizedDIB.FormularyDIBService()
        Dim sReturnedJSONString As String = String.Empty
        Dim Converter As New gloCentralizedDIB.DIBJsonConverter(Of GroupedADELookupResult)
        Dim GroupedADELookupResult As GroupedADELookupResult = Nothing
        Try


            With Argument
                '.ADESeverityFilter = m_ADESeverityfilter
                '.ADESeverityFilter = ADEIncidenceFilter.UNKNOWN_MORE_FREQUENT
                '.ADEDocLevelFilter = ADEDocLevelFilter.PROBABLE
                .ID = id
            End With

            sJSONArgument = gloCentralizedDIB.FormularyDIBArgumentPacker.GetJsonString(Argument)
            sReturnedJSONString = service.GetResponseString(sJSONArgument, URL.ADEAdverseEffectsForMedicalCondition)
            GroupedADELookupResult = Converter.GetConvertedResults(sReturnedJSONString)

            Return GroupedADELookupResult

        Catch ex As Exception
            Dim objex As New gloScreeningException
            objex.ErrMessage = "Error Loading Adverse Drug Effects for Medications"
            Throw objex
            Return Nothing
        Finally
            If Converter IsNot Nothing Then
                Converter = Nothing
            End If

            If service IsNot Nothing Then
                service = Nothing
            End If

            If Argument IsNot Nothing Then
                Argument = Nothing
            End If

            sReturnedJSONString = String.Empty
            sJSONArgument = String.Empty
        End Try
    End Function

    'Friend Function GetAdverseEffectsForMedicalCondition(ByVal id As Int64) As ADELookupResults
    '    'Try
    '    '    'Const TAB As String = vbTab

    '    '    Dim medicalCondition As MedicalCondition = medicalCondition.LoadById(DataSource_Id, id)
    '    '    ' Use a MedicalCondition object to retrieve a
    '    '    ' collection of ADELookupResults. Three filters
    '    '    ' are required: severity, incidence, and doc level.
    '    '    ' These filters can vary to include more, fewer,
    '    '    ' or different search results. Any of the filters
    '    '    ' from the ADESeverityFilter, ADEIncidenceFilter,
    '    '    ' and ADEDocLevelFilter enumerations.

    '    '    '***************23/01/2006
    '    '    Dim results As ADELookupResults = medicalCondition.GetDrugsThatCause(m_ADESeverityfilter, ADEIncidenceFilter.UNKNOWN_MORE_FREQUENT, ADEDocLevelFilter.PROBABLE)
    '    '    'Dim displayText As New System.Text.StringBuilder("AdverseDrugEffects Report For ")

    '    '    'displayText.Append(medicalCondition.NameProfessional)
    '    '    'displayText.Append("/n")
    '    '    '***************23/01/2006
    '    '    '
    '    '    If Not IsNothing(results) Then
    '    '        Return results
    '    '    Else
    '    '        Return Nothing
    '    '    End If

    '    'Catch ex As Exception
    '    '    Dim objex As New gloScreeningException
    '    '    objex.ErrMessage = "Error Loading Adverse Drug Effects for Medications"
    '    '    Throw objex
    '    '    Return Nothing
    '    'End Try
    '    Return Nothing
    'End Function
    'Friend Function SearchDrug(ByVal substring As String) As gloInteractionCollection
    '    'code commented by supriya for optimsation
    '    'Try
    '    '    ' A Navigation object will be used to
    '    '    ' search for DispensableDrugs containing
    '    '    ' the given substring
    '    '    Dim navigation As New Navigation
    '    '    navigation.Load(DATASOURCE_ID)
    '    '    'ToDo: Error processing original source shown below
    '    '    'Navigation navigation = new Navigation();
    '    '    'navigation.Load(DATASOURCE_ID);
    '    '    '----------------^--- Syntax error: 'identifier' expected
    '    '    '
    '    '    'ToDo: Error processing original source shown below
    '    '    'Navigation navigation = new Navigation();
    '    '    'navigation.Load(DATASOURCE_ID);
    '    '    '------------------------------^--- Syntax error: 'identifier' expected
    '    '    ' the DispensableDrugSearch method in Navigation
    '    '    ' requires a DrugFilter object
    '    '    Dim filter As New DrugFilter
    '    '    ' Any of the enumerations from SearchMethod can
    '    '    ' be used instead of SearchMethod.SIMPLE.
    '    '    ' This is also true of PhoneticSearch.NO_PHONETIC
    '    '    Dim items As IdDescriptionItems = navigation.DispensableDrugSearch(substring, SearchMethod.SIMPLE, PhoneticSearch.NO_PHONETIC, filter)
    '    '    ' Display the information gathered from the search

    '    '    Dim item As IdDescriptionItem
    '    '    Dim objinteraction As gloInteraction
    '    '    objdrugs = New Collection
    '    '    For Each item In items
    '    '        objinteraction = New gloInteraction
    '    '        objinteraction.ID = item.Id
    '    '        objinteraction.Name = item.Description
    '    '        'Populate Drugs Collection with ID and Drugname
    '    '        objdrugs.Add(objinteraction)
    '    '        objinteraction = Nothing
    '    '    Next
    '    'Catch ex As Exception
    '    '    Dim objex As New gloScreeningException
    '    '    objex.ErrMessage = "Error Searching Drug List"
    '    '    Throw objex
    '    'End Try
    '    'code commented by supriya for optimsation

    '    Dim navigation As New Navigation
    '    Dim _gloInteractionCollection As New gloInteractionCollection
    '    Try
    '        ' A Navigation object will be used to
    '        ' search for DispensableDrugs containing
    '        ' the given substring

    '        navigation.Load(DataSource_Id)
    '        'ToDo: Error processing original source shown below
    '        'Navigation navigation = new Navigation();
    '        'navigation.Load(DATASOURCE_ID);
    '        '----------------^--- Syntax error: 'identifier' expected
    '        '
    '        'ToDo: Error processing original source shown below
    '        'Navigation navigation = new Navigation();
    '        'navigation.Load(DATASOURCE_ID);
    '        '------------------------------^--- Syntax error: 'identifier' expected
    '        ' the DispensableDrugSearch method in Navigation
    '        ' requires a DrugFilter object
    '        Dim filter As New DrugFilter
    '        ' Any of the enumerations from SearchMethod can
    '        ' be used instead of SearchMethod.SIMPLE.
    '        ' This is also true of PhoneticSearch.NO_PHONETIC
    '        Dim items As IdDescriptionItems = navigation.DispensableDrugSearch(substring, SearchMethod.SIMPLE, PhoneticSearch.NO_PHONETIC, filter)
    '        ' Display the information gathered from the search

    '        Dim item As IdDescriptionItem
    '        Dim objinteraction As gloInteraction
    '        For Each item In items
    '            objinteraction = New gloInteraction
    '            objinteraction.ID = item.Id
    '            objinteraction.Name = item.Description
    '            'Populate Drugs Collection with ID and Drugname
    '            _gloInteractionCollection.Add(objinteraction)
    '            objinteraction = Nothing
    '        Next
    '        Return _gloInteractionCollection
    '    Catch ex As Exception
    '        Dim objex As New gloScreeningException
    '        objex.ErrMessage = "Error Searching Drug List"
    '        Throw objex
    '    Finally
    '        navigation = Nothing
    '        _gloInteractionCollection.Dispose()
    '        _gloInteractionCollection = Nothing
    '    End Try

    'End Function
    'SearchMedications
    Friend Function SearchMedicalConditions(ByVal substring As String, ByRef _GloDrugInteractionCol As gloInteractionCollection)
        'If gloCentralizedDIB.InitialiseServiceVars.gblnUseDIBService = True Then
        '    Return Me.SearchMedicalConditionsService(substring, _GloDrugInteractionCol)
        'Else
        '    'Code commented for Optimisation by supriya
        '    'Try

        '    '    Dim navigation As New Navigation
        '    '    ' Conditions for medical condition search.
        '    '    ' These values could also be selected
        '    '    ' by a user via the gui.
        '    '    Const INCLUDE_SYMPTOMS As Boolean = False
        '    '    Const HAS_DRUGS_TO_TREAT_ONLY As Boolean = False
        '    '    ' Execute the search.
        '    '    ' Search conditions may be changed to include
        '    '    ' more or fewer search results. This code
        '    '    ' uses a simple search with no phonetic
        '    '    ' searching.
        '    '    navigation.Load(DATASOURCE_ID)
        '    '    Dim medicalConditions As IdDescriptionItems = navigation.MedicalConditionsSearch(substring, SearchMethod.SIMPLE, PhoneticSearch.NO_PHONETIC, MedCondNameSearchType.PROFESSIONALONLY, INCLUDE_SYMPTOMS, HAS_DRUGS_TO_TREAT_ONLY)
        '    '    ' If medical conditions were found by the
        '    '    ' navigation search, display those results.
        '    '    Dim displayText As New System.Text.StringBuilder
        '    '    If medicalConditions.Count > 0 Then
        '    '        Dim objgloInteraction As gloInteraction
        '    '        objdrugs = New Collection
        '    '        Dim item As IdDescriptionItem
        '    '        For Each item In medicalConditions
        '    '            Dim condition As MedicalCondition = MedicalCondition.LoadById(DATASOURCE_ID, Integer.Parse(item.Id))
        '    '            objgloInteraction = New gloInteraction
        '    '            'Populate the medication collection with 
        '    '            'MedicationId and medical condition description
        '    '            objgloInteraction.ID = condition.Id
        '    '            objgloInteraction.Name = condition.NameProfessional
        '    '            objdrugs.Add(objgloInteraction)
        '    '            objgloInteraction = Nothing
        '    '        Next item
        '    '    Else

        '    '    End If
        '    'Catch ex As Exception
        '    '    Dim objex As New gloScreeningException
        '    '    objex.ErrMessage = "Error Searching Medication List"
        '    '    Throw objex
        '    'End Try
        '    'Code Commented for optimsation by supriya
        '    Dim navigation As New Navigation
        '    Try


        '        ' Conditions for medical condition search.
        '        ' These values could also be selected
        '        ' by a user via the gui.
        '        Const INCLUDE_SYMPTOMS As Boolean = False
        '        Const HAS_DRUGS_TO_TREAT_ONLY As Boolean = False
        '        ' Execute the search.
        '        ' Search conditions may be changed to include
        '        ' more or fewer search results. This code
        '        ' uses a simple search with no phonetic
        '        ' searching.
        '        navigation.Load(DataSource_Id)
        '        Dim medicalConditions As IdDescriptionItems = navigation.MedicalConditionsSearch(substring, SearchMethod.SIMPLE, PhoneticSearch.NO_PHONETIC, MedCondNameSearchType.PROFESSIONALONLY, INCLUDE_SYMPTOMS, HAS_DRUGS_TO_TREAT_ONLY)
        '        ' If medical conditions were found by the
        '        ' navigation search, display those results.
        '        Dim displayText As New System.Text.StringBuilder
        '        If medicalConditions.Count > 0 Then
        '            Dim objgloInteraction As gloInteraction
        '            Dim item As IdDescriptionItem
        '            For Each item In medicalConditions
        '                Dim condition As MedicalCondition = MedicalCondition.LoadById(DataSource_Id, Integer.Parse(item.Id))
        '                objgloInteraction = New gloInteraction
        '                'Populate the medication collection with 
        '                'MedicationId and medical condition description
        '                objgloInteraction.ID = condition.Id
        '                objgloInteraction.Name = condition.NameProfessional
        '                _GloDrugInteractionCol.Add(objgloInteraction)
        '                objgloInteraction = Nothing
        '            Next item
        '        End If
        '        Return _GloDrugInteractionCol
        '    Catch ex As Exception
        '        Dim objex As New gloScreeningException
        '        objex.ErrMessage = "Error Searching Medication List"
        '        Throw objex
        '    Finally
        '        navigation = Nothing

        '    End Try
        'End If

        Return Nothing

    End Function

    Friend Function SearchMedicalConditionsService(ByVal substring As String, ByRef _GloDrugInteractionCol As gloInteractionCollection)
        Dim Argument As New ADESearchMedicalConditionsArgument()
        Dim sJSONArgument As String = String.Empty
        Dim service As New gloCentralizedDIB.FormularyDIBService()
        Dim sReturnedJSONString As String = String.Empty
        Dim Converter As New gloCentralizedDIB.DIBJsonConverter(Of GroupedADESearchMedicalConditions)
        Dim GroupedADESearchMedicalConditions As GroupedADESearchMedicalConditions = Nothing
        Dim objgloInteraction As gloInteraction = Nothing
        Try


            With Argument
                .SubString = substring
                .HasDrugsToTreatOnly = False
                .IncludeSymtoms = False

                '.SearchMethod = SearchMethod.SIMPLE
                '.PhoneticSearch = PhoneticSearch.NO_PHONETIC
                '.MedCondNameSearchType = MedCondNameSearchType.PROFESSIONALONLY
            End With

            sJSONArgument = gloCentralizedDIB.FormularyDIBArgumentPacker.GetJsonString(Argument)
            sReturnedJSONString = service.GetResponseString(sJSONArgument, URL.ADESearchMedicalConditions)
            GroupedADESearchMedicalConditions = Converter.GetConvertedResults(sReturnedJSONString)

            If GroupedADESearchMedicalConditions IsNot Nothing AndAlso GroupedADESearchMedicalConditions.Conditions.Any() Then

                For Each element As ADESearchMedicalConditionsReturn In GroupedADESearchMedicalConditions.Conditions
                    objgloInteraction = New gloInteraction
                    objgloInteraction.ID = element.ID
                    objgloInteraction.Name = element.NameProfessional
                    _GloDrugInteractionCol.Add(objgloInteraction)
                    objgloInteraction = Nothing
                Next
                
            End If

            Return objgloInteraction
        Catch ex As Exception
            Dim objex As New gloScreeningException
            objex.ErrMessage = "Error Loading Adverse Drug Effects for Medications"
            Throw objex
            Return Nothing
        Finally
            If Converter IsNot Nothing Then
                Converter = Nothing
            End If

            If service IsNot Nothing Then
                service = Nothing
            End If

            If Argument IsNot Nothing Then
                Argument = Nothing
            End If

            sReturnedJSONString = String.Empty
            sJSONArgument = String.Empty
        End Try
    End Function
#End Region
    Public Function FillMedicalConditionsService(ByRef _GloDrugInteractionCol As gloInteractionCollection) As gloInteractionCollection

        Dim sJSONArgument As String = String.Empty
        Dim service As New gloCentralizedDIB.FormularyDIBService()
        Dim sReturnedJSONString As String = String.Empty
        Dim Converter As New gloCentralizedDIB.DIBJsonConverter(Of GroupedADEFillMedicalConditionsReturn)
        Dim GroupedADEFillMedicalConditionsReturn As GroupedADEFillMedicalConditionsReturn = Nothing
        Dim objgloInteraction As gloInteraction = Nothing
        Try

            sReturnedJSONString = service.GetResponseString(sJSONArgument, URL.ADEFillMedicalConditions)
            GroupedADEFillMedicalConditionsReturn = Converter.GetConvertedResults(sReturnedJSONString)

            If GroupedADEFillMedicalConditionsReturn IsNot Nothing AndAlso GroupedADEFillMedicalConditionsReturn.Conditions.Any() Then
                For Each element As ADEFillMedicalConditionsReturn In GroupedADEFillMedicalConditionsReturn.Conditions
                    objgloInteraction = New gloInteraction
                    objgloInteraction.ID = element.ID
                    objgloInteraction.Name = element.NamePatient
                    _GloDrugInteractionCol.Add(objgloInteraction)
                    objgloInteraction = Nothing
                Next
            End If
            Return _GloDrugInteractionCol

        Catch ex As Exception
            Dim objex As New gloScreeningException
            objex.ErrMessage = "Error Loading Adverse Drug Effects for Medications"
            Throw objex
            Return Nothing
        Finally
            If Converter IsNot Nothing Then
                Converter = Nothing
            End If

            If service IsNot Nothing Then
                service = Nothing
            End If

            sReturnedJSONString = String.Empty
            sJSONArgument = String.Empty
        End Try
    End Function

    Public Function FillMedicalConditions(ByRef _GloDrugInteractionCol As gloInteractionCollection) As gloInteractionCollection

        Return Nothing
    End Function

    Friend Function GetScreeningForDrugsService(ByVal id As Int64) As GroupedADELookupResult
        
        Return Nothing
    End Function

  
End Class
#End Region
#Region "gloPARScreen"
Friend Class gloPARScreen
    Inherits gloDrugInteraction
    Implements Imonograph
    Sub New(ByVal Drugs As gloInteractionCollection, ByVal Allergies As gloInteractionCollection, ByVal PatientMedicalConditions As gloInteractionCollection)
        'Call constructor for base class
        'and then execute constructor for the derived class
        MyBase.New(Drugs, Allergies, PatientMedicalConditions)
    End Sub
    Public Overrides Function PerformScreening(ByVal objInteractionCol As DrugInteractionCollection.gloInteractionCollection) As Boolean
     
        Return False
    End Function

    Public Function PerformScreeningService(ByVal objInteractionCol As DrugInteractionCollection.gloInteractionCollection) As Boolean
       
        Return False
    End Function

    Public Function ShowMonograph(ByVal objInteractionresult As DrugInteractionCollection.gloInteraction) As String Implements Imonograph.ShowMonograph
       
        Return Nothing
    End Function

    Public Function ShowMonographByService(ByVal objInteractionresult As DrugInteractionCollection.gloInteraction) As String
        Dim sReturned As String = String.Empty
        Dim monogramArgument As New MonogramArgument()
        Dim sJsonConvertedArg As String = String.Empty
        Dim service As New gloCentralizedDIB.FormularyDIBService()
        Dim sResponseString As String = String.Empty
        Dim Converter As New gloCentralizedDIB.DIBJsonConverter(Of String)

        Try
            With monogramArgument
                .ID = CType(objInteractionresult, DrugInteractionCollection.gloMonoInteraction).MonographID
                .Language = "MMW-PE"
                .MonogramType = MonogramType.PAR                
            End With

            sJsonConvertedArg = gloCentralizedDIB.FormularyDIBArgumentPacker.GetJsonString(monogramArgument)
            sResponseString = service.GetResponseString(sJsonConvertedArg, URL.ShowMonograph)
            sReturned = Converter.GetConvertedResults(sResponseString)
            Return sReturned
        Catch ex As Exception
            Dim objgloScreeningException As New gloScreeningException
            objgloScreeningException.ErrMessage = "Error Displaying PAR Monographs"
            Throw objgloScreeningException
            Return ""
        Finally
            If monogramArgument IsNot Nothing Then
                monogramArgument.Dispose()
                monogramArgument = Nothing
            End If

            sJsonConvertedArg = String.Empty

            If service IsNot Nothing Then
                service = Nothing
            End If

            sResponseString = String.Empty

            If Converter IsNot Nothing Then
                Converter = Nothing
            End If
        End Try
    End Function
End Class
#End Region


'****8
#Region "gloPRCScreen"
Friend Class gloPRCScreen
    Inherits gloDrugInteraction
    'Implements Imonograph
    Sub New(ByVal Drugs As gloInteractionCollection, ByVal Allergies As gloInteractionCollection, ByVal PatientMedicalConditions As gloInteractionCollection)
        'Call constructor for base class
        'and then execute constructor for the derived class
        MyBase.New(Drugs, Allergies, PatientMedicalConditions)
    End Sub

    Public Overrides Function PerformScreening(ByVal objInteractionCol As DrugInteractionCollection.gloInteractionCollection) As Boolean

        Return False
    End Function

    Public Function PerformScreeningService(ByVal objInteractionCol As DrugInteractionCollection.gloInteractionCollection) As Boolean
      
        Return False
    End Function

End Class
#End Region
'****8



#Region "gloDIScreen"
Friend Class gloDIScreen
    Inherits gloDrugInteraction
    Implements Imonograph
    Sub New(ByVal Drugs As gloInteractionCollection, ByVal Allergies As gloInteractionCollection, ByVal PatientMedicalConditions As gloInteractionCollection)
        'Call constructor for base class
        'and then execute constructor for the derived class
        MyBase.New(Drugs, Allergies, PatientMedicalConditions)
    End Sub
    Public Overrides Function PerformScreening(ByVal objInteractionCol As DrugInteractionCollection.gloInteractionCollection) As Boolean
        Return False
        
    End Function
  
    Public Function ShowMonograph(ByVal objInteractionresult As DrugInteractionCollection.gloInteraction) As String Implements Imonograph.ShowMonograph
        Return Nothing

    End Function

    Public Function ShowMonographByService(ByVal objInteractionresult As DrugInteractionCollection.gloInteraction) As String
        Dim sReturned As String = String.Empty
        Dim monogramArgument As New MonogramArgument()
        Dim sJsonConvertedArg As String = String.Empty
        Dim service As New gloCentralizedDIB.FormularyDIBService()
        Dim sResponseString As String = String.Empty
        Dim Converter As New gloCentralizedDIB.DIBJsonConverter(Of String)

        Try


            With monogramArgument
                .ID = CType(objInteractionresult, DrugInteractionCollection.gloMonoInteraction).MonographID
                .Language = "MMW-PE"
                .MonogramType = MonogramType.DI
                .Drug1 = CType(objInteractionresult, DrugInteractionCollection.gloInteractionResult).Drug1
                .Drug2 = CType(objInteractionresult, DrugInteractionCollection.gloInteractionResult).Drug2
            End With

            sJsonConvertedArg = gloCentralizedDIB.FormularyDIBArgumentPacker.GetJsonString(monogramArgument)
            sResponseString = service.GetResponseString(sJsonConvertedArg, URL.ShowMonograph)
            sReturned = Converter.GetConvertedResults(sResponseString)
            Return sReturned
        Catch ex As Exception
            Dim objgloScreeningException As New gloScreeningException
            objgloScreeningException.ErrMessage = "Error Displaying PAR Monographs"
            Throw objgloScreeningException
            Return ""
        Finally
            If monogramArgument IsNot Nothing Then
                monogramArgument.Dispose()
                monogramArgument = Nothing
            End If

            sJsonConvertedArg = String.Empty

            If service IsNot Nothing Then
                service = Nothing
            End If

            sResponseString = String.Empty

            If Converter IsNot Nothing Then
                Converter = Nothing
            End If
        End Try
    End Function

    Public Function PerformScreeningService(ByVal objInteractionCol As DrugInteractionCollection.gloInteractionCollection) As Boolean
      
        Return False
    End Function
    
End Class

#End Region
#Region "gloDTScreen"
Friend Class gloDTScreen
    Inherits gloDrugInteraction
    Sub New(ByVal Drugs As gloInteractionCollection, ByVal Allergies As gloInteractionCollection, ByVal PatientMedicalConditions As gloInteractionCollection)
        'Call constructor for base class
        'and then execute constructor for the derived class
        MyBase.New(Drugs, Allergies, PatientMedicalConditions)
    End Sub

    Public Overrides Function PerformScreening(ByVal objInteractionCol As gloInteractionCollection) As Boolean

        'If gloCentralizedDIB.InitialiseServiceVars.gblnUseDIBService = True Then
        '    Return Me.PerformScreeningService(objInteractionCol)
        'Else

        '    Dim objdtscreenresults As DTScreenResults
        '    Dim objcode As Code
        '    Dim strDIScreen As System.Text.StringBuilder

        '    objdtscreenresults = Screening.DTScreen(objpatientprofile, False, False, False)
        '    Dim objinteraction As DrugInteractionCollection.gloInteraction

        '    Try

        '        If objdtscreenresults.Count > 0 Then
        '            Dim objdtscreenresult As DTScreenResult
        '            For Each objdtscreenresult In objdtscreenresults
        '                strDIScreen = New System.Text.StringBuilder
        '                strDIScreen.Append("Duplication Class : ")
        '                strDIScreen.Append(objdtscreenresult.ClassDescription)
        '                strDIScreen.Append("(Allowance of ")
        '                strDIScreen.Append(objdtscreenresult.Allowance)
        '                strDIScreen.Append(")")
        '                strDIScreen.Append(vbCr)
        '                strDIScreen.Append(vbCr)
        '                strDIScreen.Append(objdtscreenresult.ScreenMessage)
        '                strDIScreen.Append(vbCr)
        '                strDIScreen.Append(vbCr)

        '                objcode = Code.LoadByTypeAndId(DATASOURCE_ID, CodeTypes.DTPotentialAbuses, objdtscreenresult.PotentialAbuse)
        '                strDIScreen.Append("Potential Abuse: " & objcode.Description)
        '                strDIScreen.Append(vbCr)

        '                objinteraction = New DrugInteractionCollection.gloInteraction
        '                Dim objpatientdrugs As PatientDrugs

        '                If Not IsNothing(objdtscreenresult) Then
        '                    objpatientdrugs = objdtscreenresult.Drugs
        '                    If objpatientdrugs.Count > 0 Then
        '                        'Code Commented for DI optimisation
        '                        'ResultCol = New Collection
        '                        Dim objpatientdrug As PatientDrug
        '                        For Each objpatientdrug In objpatientdrugs
        '                            strDIScreen.Append(objpatientdrug.Description)
        '                            strDIScreen.Append(vbCr)
        '                            'Code Commented for DI optimisation
        '                            'ResultCol.Add(objpatientdrug.Id)
        '                            'Code Commented for DI optimisation
        '                        Next
        '                    End If
        '                End If
        '                strDIScreen.Append(vbCr)
        '                strDIScreen.Append(vbCr)
        '                objinteraction.Name = strDIScreen.ToString
        '                objInteractionCol.Add(objinteraction)
        '            Next
        '        End If


        '        Dim objmessages As ScreenMessages
        '        objmessages = objdtscreenresults.Messages

        '        strDIScreen = New System.Text.StringBuilder

        '        If objmessages.Count > 0 Then
        '            strDIScreen.Append(vbCr)
        '            strDIScreen.Append("Messages")
        '            strDIScreen.Append(vbCr)

        '            Dim objmessage As ScreenMessage
        '            For Each objmessage In objmessages
        '                strDIScreen.Append(objmessage.ItemDescription)
        '                strDIScreen.Append(vbCr)
        '                strDIScreen.Append("       ")
        '                strDIScreen.Append(objmessage.MessageText)
        '                strDIScreen.Append(vbCr)
        '            Next

        '        End If
        '        ResultSet = strDIScreen.ToString(0, strDIScreen.Length)
        '    Catch ex As Exception
        '        Dim objgloScreeningException As New gloScreeningException
        '        'objgloScreeningException.ErrMessage = "Error Performing Duplicate Therapy Interaction"
        '        objgloScreeningException.ErrMessage = ex.Message
        '        Throw objgloScreeningException
        '    Finally
        '        objdtscreenresults = Nothing
        '        objcode = Nothing
        '        strDIScreen = Nothing
        '        objdtscreenresults = Nothing
        '        objinteraction = Nothing
        '    End Try
        '    Return False
        'End If
        Return False
    End Function

    Public Function PerformScreeningService(ByVal objInteractionCol As gloInteractionCollection) As Boolean
        'Dim strDIScreen As New System.Text.StringBuilder
        'Dim objInteraction As DrugInteractionCollection.gloInteraction = Nothing
        'Dim sJSONArgument As String = String.Empty
        'Dim service As New gloCentralizedDIB.FormularyDIBService()
        'Dim arg As New gloCentralizedDIBArgument()
        'Dim sReturnedJSONString As String = String.Empty
        'Dim Converter As New gloCentralizedDIB.DIBJsonConverter(Of GroupedDTScreenReturnType)
        'Dim GroupedDTScreenReturnType As GroupedDTScreenReturnType = Nothing

        'Try
        '    With arg
        '        .MajorDocLevel = m_DIDocLevel
        '        .ModerateDocLevel = m_DIDocLevel
        '        .MinorDocLevel = m_DIDocLevel
        '        .DFAInteractionType = DFAInteractionType.BOTH
        '        .PRCMgmtLevelFilter = PRCMgmtLevelFilter.EXTREME_CAUTION
        '        .INDProxyLevelFilter = INDProxyLevelFilter.CERTAIN
        '        .DIScreenResults_MajorDocLevel = m_DIDocLevel
        '        .DIScreenResults_MinorDocLevel = m_DIDocLevel
        '        .DIScreenResults_ModerateDocLevel = m_DIDocLevel
        '        .DrugNameTypeFilter = DrugNameTypeFilter.ALL
        '        .DrugsList = Me.lstDrugsList
        '        .AllergiesList = Me.lstAllergiesList
        '        .MedicalConditions = Me.lstMedicalConditionsList
        '    End With

        '    sJSONArgument = gloCentralizedDIB.FormularyDIBArgumentPacker.GetJsonString(arg)
        '    sReturnedJSONString = service.GetResponseString(sJSONArgument, URL.DTScreening)
        '    GroupedDTScreenReturnType = Converter.GetConvertedResults(sReturnedJSONString)

        '    If GroupedDTScreenReturnType.DTScreenReturnTypes.Any() Then
        '        For Each element As DTScreenReturnType In GroupedDTScreenReturnType.DTScreenReturnTypes
        '            strDIScreen = New System.Text.StringBuilder
        '            strDIScreen.Append("Duplication Class : ")
        '            strDIScreen.Append(element.ClassDescription)
        '            strDIScreen.Append("(Allowance of ")
        '            strDIScreen.Append(element.Allowance)
        '            strDIScreen.Append(")")
        '            strDIScreen.Append(vbCr)
        '            strDIScreen.Append(vbCr)
        '            strDIScreen.Append(element.ScreenMessage)
        '            strDIScreen.Append(vbCr)
        '            strDIScreen.Append(vbCr)
        '            strDIScreen.Append("Potential Abuse: " & element.ObjectCodeDescription)
        '            strDIScreen.Append(vbCr)

        '            objInteraction = New DrugInteractionCollection.gloInteraction
        '            If Not IsNothing(element) Then

        '                If element.Descriptions.Any() Then
        '                    For Each stringElement As String In element.Descriptions
        '                        strDIScreen.Append(stringElement)
        '                        strDIScreen.Append(vbCr)
        '                    Next
        '                End If
        '            End If
        '            strDIScreen.Append(vbCr)
        '            strDIScreen.Append(vbCr)
        '            objInteraction.Name = strDIScreen.ToString
        '            objInteractionCol.Add(objInteraction)
        '        Next
        '    End If

        '    strDIScreen = New System.Text.StringBuilder

        '    If GroupedDTScreenReturnType.DTMessageList.Any() Then
        '        strDIScreen.Append(vbCr)
        '        strDIScreen.Append("Messages")
        '        strDIScreen.Append(vbCr)

        '        For Each DTMessageElement As DTMessage In GroupedDTScreenReturnType.DTMessageList
        '            strDIScreen.Append(DTMessageElement.ItemDescription)
        '            strDIScreen.Append(vbCr)
        '            strDIScreen.Append("       ")
        '            strDIScreen.Append(DTMessageElement.MessageText)
        '            strDIScreen.Append(vbCr)
        '        Next

        '    End If
        '    ResultSet = strDIScreen.ToString(0, strDIScreen.Length)
        'Catch ex As Exception
        '    Dim objgloScreeningException As New gloScreeningException
        '    objgloScreeningException.ErrMessage = ex.Message
        '    Throw objgloScreeningException
        'Finally

        '    If objInteraction IsNot Nothing Then
        '        objInteraction = Nothing
        '    End If
        '    If strDIScreen IsNot Nothing Then
        '        strDIScreen.Clear()
        '        strDIScreen = Nothing
        '    End If

        '    If arg IsNot Nothing Then
        '        arg.Dispose()
        '        arg = Nothing
        '    End If

        '    sJSONArgument = String.Empty
        '    sReturnedJSONString = String.Empty

        '    If service IsNot Nothing Then
        '        service = Nothing
        '    End If

        '    If Converter IsNot Nothing Then
        '        Converter = Nothing
        '    End If

        '    If GroupedDTScreenReturnType IsNot Nothing Then
        '        GroupedDTScreenReturnType.Dispose()
        '        GroupedDTScreenReturnType = Nothing
        '    End If
        'End Try
        Return False
    End Function

End Class
#End Region
#Region "gloDFAScreen"
'Drug to Food Interaction
Friend Class gloDFAScreen
    Inherits gloDrugInteraction
    Implements Imonograph

    'new parameter collection patientmedicalcondition added to the base class constructor
    Sub New(ByVal Drugs As gloInteractionCollection, ByVal Allergies As gloInteractionCollection, ByVal PatientMedicalConditions As gloInteractionCollection)
        MyBase.new(Drugs, Allergies, PatientMedicalConditions)
    End Sub

    Public Function ShowMonograph(ByVal objInteractionresult As DrugInteractionCollection.gloInteraction) As String Implements Imonograph.ShowMonograph
     
        Return ""
    End Function

    Public Function ShowMonographByService(ByVal objInteractionresult As DrugInteractionCollection.gloInteraction) As String
        Dim monogramArgument As MonogramArgument = Nothing
        Dim sJsonArgument As String = String.Empty
        Dim objService As New gloCentralizedDIB.FormularyDIBService()
        Dim Converter As New gloCentralizedDIB.DIBJsonConverter(Of String)
        Dim sConvertedResponseString As String = String.Empty
        Dim sReturnedMonogram As String = String.Empty

        Try
            monogramArgument = New MonogramArgument()

            With monogramArgument
                .ID = CType(objInteractionresult, DrugInteractionCollection.gloMonoInteraction).MonographID
                .Language = "MMW-PE"
                .MonogramType = MonogramType.DFA
            End With

            sJsonArgument = gloCentralizedDIB.FormularyDIBArgumentPacker.GetJsonString(monogramArgument)
            sConvertedResponseString = objService.GetResponseString(sJsonArgument, URL.ShowMonograph)
            sReturnedMonogram = Converter.GetConvertedResults(sConvertedResponseString)

            Return sReturnedMonogram
        Catch ex As Exception
            Dim objgloScreeningException As New gloScreeningException
            objgloScreeningException.ErrMessage = "Error Displaying PAR Monographs"
            Throw objgloScreeningException
            Return ""
        Finally

            If monogramArgument IsNot Nothing Then
                monogramArgument.Dispose()
                monogramArgument = Nothing
            End If

            sJsonArgument = String.Empty
            If objService IsNot Nothing Then
                objService = Nothing
            End If

            If Converter IsNot Nothing Then
                Converter = Nothing
            End If

            sConvertedResponseString = String.Empty
        End Try
    End Function

    Public Function PerformScreeningService(ByVal objInteractionCol As DrugInteractionCollection.gloInteractionCollection)
     
        Return False
    End Function

    Public Overrides Function PerformScreening(ByVal objInteractionCol As DrugInteractionCollection.gloInteractionCollection) As Boolean
     
        Return False
        '  End If
    End Function

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
#End Region

Public Class PatientEducation
    Public Function GetPatientEducation(ByVal Id As Int64) As String
        Return ""
        
    End Function

    Public Function GetPatientEducationService(ByVal Id As Int64) As String
        Dim strDIScreen As New System.Text.StringBuilder
        Dim arg As New gloCentralizedDIBPatientEducationArgument
        Dim sJSONArgument As String = String.Empty
        Dim service As New gloCentralizedDIB.FormularyDIBService()
        Dim sReturnedJSONString As String = String.Empty
        Dim Converter As New gloCentralizedDIB.DIBJsonConverter(Of String)
        Dim sDisplayText As String = String.Empty

        Try
            With arg
                .ID = Id
                .Language = "MMW-CE"
            End With

            sJSONArgument = gloCentralizedDIB.FormularyDIBArgumentPacker.GetJsonString(arg)
            sReturnedJSONString = service.GetResponseString(sJSONArgument, URL.PatientEducation)
            sDisplayText = Converter.GetConvertedResults(sReturnedJSONString)
            Return sDisplayText
        Catch ex As Exception
            Return ""
        End Try
        
    End Function
End Class
#Region "gloScreeningException"
Public Class gloScreeningException
    Inherits ApplicationException
    Private m_message As String
    Public Property ErrMessage() As String
        Get
            Return m_message
        End Get
        Set(ByVal Value As String)
            m_message = Value
        End Set
    End Property
End Class
#End Region
Namespace DrugInteractionCollection
#Region "gloInteractionCol"
    Public Class gloInteractionCollection
        Inherits CollectionBase
        Implements IDisposable
        Private disposedValue As Boolean = False        ' To detect redundant calls
        'Remove Item at specified index
        Public Sub Remove(ByVal index As Integer)
            ' Check to see if there is a widget at the supplied index.
            If index > Count - 1 Or index < 0 Then
                ' If no object exists, a messagebox is shown and the operation is 
                ' cancelled.
                'System.Windows.Forms.MessageBox.Show("Index not valid!")
            Else
                ' Invokes the RemoveAt method of the List object.
                List.RemoveAt(index)
            End If
        End Sub
        ' This line declares the Item property as ReadOnly, and 
        ' declares that it will return a SentFax object.
        Public ReadOnly Property Item(ByVal index As Integer) As gloInteraction
            Get
                ' The appropriate item is retrieved from the List object and 
                ' explicitly cast to the SentFax type, then returned to the 
                ' caller.
                Return CType(List.Item(index), gloInteraction)
            End Get
        End Property
        ' Restricts to SentFax types, items that can be added to the collection.
        Public Sub Add(ByVal _gloInteraction As gloInteraction)
            ' Invokes Add method of the List object to add a SentFax.
            List.Add(_gloInteraction)
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
        Public Sub New()
            MyBase.New()
        End Sub

        'Public objgloInteractionCol As Collection
        'Sub New()
        '    objgloInteractionCol = New Collection
        'End Sub
        'Public Property GetGloInteractionCol() As Collection
        '    Get
        '        Return objgloInteractionCol
        '    End Get
        '    Set(ByVal Value As Collection)
        '        objgloInteractionCol = Value
        '    End Set
        'End Property



        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
                End If

                ' TODO: free shared unmanaged resources
            End If
            Me.disposedValue = True
            Me.Clear()
        End Sub

#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
#End Region
#Region "gloInteraction"
    Public Class gloInteraction
        'For NDC Interaction
        'Private m_ID As Int64
        Private m_ID As String
        'For NDC Interaction
        Private m_Name As String
        'For NDC Interaction
        'Public Property ID() As Int64
        '    Get
        '        Return m_ID
        '    End Get
        '    Set(ByVal Value As Int64)
        '        m_ID = Value
        '    End Set
        'End Property
        Public Property ID() As String
            Get
                Return m_ID
            End Get
            Set(ByVal Value As String)
                m_ID = Value
            End Set
        End Property
        'For NDC Interaction
        Public Property Name() As String
            Get
                Return m_Name
            End Get
            Set(ByVal Value As String)
                m_Name = Value
            End Set
        End Property

        Public Sub New()

        End Sub
    End Class
#End Region
#Region "gloDrug"
    Public Class gloDrug
        Inherits gloInteraction
        Private m_Dosage As String
        Private m_Route As String
        Private m_frequency As String
        Private m_duration As String
        Private m_IsMedication As Boolean
        Public Property IsMedication() As Boolean
            Get
                Return m_IsMedication
            End Get
            Set(ByVal Value As Boolean)
                m_IsMedication = Value
            End Set
        End Property
        Public Property Dosage() As String
            Get
                Return m_Dosage
            End Get
            Set(ByVal Value As String)
                m_Dosage = Value
            End Set
        End Property
        Public Property Route() As String
            Get
                Return m_Route
            End Get
            Set(ByVal Value As String)
                m_Route = Value
            End Set
        End Property
        Public Property Frequency() As String
            Get
                Return m_frequency
            End Get
            Set(ByVal Value As String)
                m_frequency = Value
            End Set
        End Property
        Public Property Duration() As String
            Get
                Return m_duration
            End Get
            Set(ByVal Value As String)
                m_duration = Value
            End Set
        End Property
    End Class
#End Region
#Region "gloMonoInteraction"
    Public Class gloMonoInteraction
        Inherits gloInteraction
        Private m_monographId As String
        Public Property MonographID() As String
            Get
                Return m_monographId
            End Get
            Set(ByVal Value As String)
                m_monographId = Value
            End Set
        End Property
    End Class
#End Region
#Region "gloInteractionResult"
    Public Class gloInteractionResult
        Inherits gloMonoInteraction
        Private m_ID1 As Int64
        Private m_Drug1 As String
        Private m_Drug2 As String
        Public Property ID1() As Int64
            Get
                Return m_ID1
            End Get
            Set(ByVal Value As Int64)
                m_ID1 = Value
            End Set
        End Property

        Public Property Drug1() As String
            Get
                Return m_Drug1
            End Get
            Set(ByVal Value As String)
                m_Drug1 = Value
            End Set
        End Property

        Public Property Drug2() As String
            Get
                Return m_Drug2
            End Get
            Set(ByVal Value As String)
                m_Drug2 = Value
            End Set
        End Property
    End Class
#End Region
End Namespace
