Imports gloUserControlLibrary
Imports gloEMR.gloStream.DiseaseManagement.Supporting
Imports gloemr.gloStream.DiseaseManagement
Public Class frmDM_PatientSpecific
    Inherits System.Windows.Forms.Form
    Implements IPatientContext
    Private m_CriteriaCol As New Collection
    Private m_PatientCol As New Collection
    Private m_PatientID As Long
    Private m_VisitID As Long

    Private WithEvents _PatientStrip As New gloUC_PatientStrip

    Dim oPatTriggers As DataTable
    Dim oDM As New DiseaseManagement
    Dim arrLabs As New ArrayList
    Dim arrRadiology As New ArrayList
    Dim arrGuidline As New ArrayList
    Dim arrDrug As New ArrayList
    Dim oclsDM_PatientSpecific As New clsDM_Template
    Dim lst As myList
    Dim objSender As Object = Nothing
    Dim arrCommonCriterias As New ArrayList
    Dim nPatAge As Int32 = 0
    Dim dtOtherHeathPlans As DataTable
    Dim _TemplateID As Int64 = 0
    ''Sandip Darade 20090820
    Dim _IsModifyCriteria As Boolean
    Dim _SelectedCriteriaID As Int64 = 0
    Dim _IsDeleteCriteria As Boolean
    Private strPatientCode As String
    Private strPatientFirstName As String
    Private strPatientMiddleName As String
    Private strPatientLastName As String
    Private strPatientDOB As String
    Private strPatientAge As String
    Private strPatientGender As String
    Private strPatientMaritalStatus As String

    Private bParentTrigger As Boolean = True        ''for the trvHeathplan after 
    Private bChildTrigger As Boolean = True
    'Public Sub New()

    '    ' This call is required by the Windows Form Designer.
    '    InitializeComponent()

    '    ' Add any initialization after the InitializeComponent() call.

    'End Sub
    Public Sub New(ByVal COL_PatientID As Collection, ByVal blnIsSinglePatient As Boolean, ByVal VisitID As Long)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'm_CriteriaCol = COL_CriteriaID
        m_PatientCol = COL_PatientID
        m_PatientID = COL_PatientID(1)
        m_VisitID = VisitID
        'Add any initialization after the InitializeComponent() call

    End Sub

    Private Sub frmDM_PatientSpecific_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            'CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
            If (IsNothing(Me.ParentForm) = False) Then
                CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub frmDM_PatientSpecific_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            ''Fill the patient who meet health plan conditions
            FillPatients()
            loadPatientStrip()
            Call Get_PatientDetails()
            'FillPatients_old()
            'trvPatients.Select()
            'GloUC_trvPatients.Select()
            ''Get the genreral health plan conditions

            GetGeneralCriteria()

            FillOperators()
            FillDropdowns()
            BindHealthPlan()
            'selectGender()
            GetOtherHeathPlans()
            'BindOtherHealthPlans()
            ' Fill_OtherInfo()
           
            ShowRecommendationsAlert()
           
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, m_PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function Get_PatientDetails()
        Dim dtPatient As DataTable = Nothing

        Try
            ' dtPatient = New DataTable
            dtPatient = GetPatientInfo(m_PatientID)
            If IsNothing(dtPatient) = False Then
                If dtPatient.Rows.Count > 0 Then
                    strPatientCode = Convert.ToString(dtPatient.Rows(0)("sPatientCode"))
                    strPatientFirstName = Convert.ToString(dtPatient.Rows(0)("sFirstName"))
                    strPatientLastName = Convert.ToString(dtPatient.Rows(0)("sLastName"))
                    strPatientDOB = Convert.ToString(dtPatient.Rows(0)("dtDOB"))
                    strPatientAge = GetAge(Convert.ToDateTime(dtPatient.Rows(0)("dtDOB")))
                    strPatientGender = Convert.ToString(dtPatient.Rows(0)("sGender"))
                    strPatientMaritalStatus = Convert.ToString(dtPatient.Rows(0)("sMaritalStatus"))

                End If
            End If
        Catch ex As Exception

        Finally
            If IsNothing(dtPatient) = False Then
                dtPatient.Dispose()
                dtPatient = Nothing
            End If


        End Try
        Return Nothing
    End Function
    Private Sub Fill_OtherInfo()


        Dim associatenode As New myTreeNode("Orders", -1)


        'associatenode.Key = -1
        'associatenode.Text = "Orders"
        'associatenode.ImageIndex = 5
        'associatenode.SelectedImageIndex = 5
        trvHealthPlan.Nodes.Add(associatenode)
        'trOrderInfo.Nodes.Add(associatenode)

        Dim MyChild As New myTreeNode
        MyChild.Text = "Labs"
        MyChild.Key = -1
        MyChild.ImageIndex = 6
        MyChild.SelectedImageIndex = 6
        associatenode.Nodes.Add(MyChild)

        MyChild = New myTreeNode
        MyChild.Text = "Orders"
        MyChild.Key = -1
        MyChild.ImageIndex = 7
        MyChild.SelectedImageIndex = 7
        associatenode.Nodes.Add(MyChild)

        MyChild = New myTreeNode
        MyChild.Text = "Guidelines"
        MyChild.Key = -1
        MyChild.ImageIndex = 8
        MyChild.SelectedImageIndex = 8
        associatenode.Nodes.Add(MyChild)

        MyChild = New myTreeNode
        MyChild.Text = "Rx"
        MyChild.Key = -1
        MyChild.ImageIndex = 9
        MyChild.SelectedImageIndex = 9
        associatenode.Nodes.Add(MyChild)

        MyChild = New myTreeNode
        MyChild.Text = "Referrals"
        MyChild.Key = -1
        MyChild.ImageIndex = 10
        MyChild.SelectedImageIndex = 10
        associatenode.Nodes.Add(MyChild)

        trvHealthPlan.ExpandAll()

    End Sub

    ''' <summary>
    ''' Get the distinct general health plan criteria conditions
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetGeneralCriteria()
        ''Check for Patient Specific Criteria exists
        ''If exists in the General Criteria collection, Remove the Patient Specific criteria from the general collection

        ''Get all the patient specific health plan conditions

        'Added optimized code for DM Alert
        'Aniket 08-Mar-13: Call the pre 7030 DM SP for older locic
        m_CriteriaCol = oDM.FindDMCriteriaOFPatientPre7030(m_PatientID, gnClinicID)

        oPatTriggers = oDM.FindPatientSpecificTriggers(m_PatientID)

        If m_CriteriaCol Is Nothing Then
            Exit Sub
        End If

        arrCommonCriterias = New ArrayList

        For _index As Integer = 1 To m_CriteriaCol.Count
            For _cnt As Int32 = 0 To oPatTriggers.Rows.Count - 1
                If m_CriteriaCol(_index) = oPatTriggers.Rows(_cnt)("DM_nCriteriaID") Then
                    arrCommonCriterias.Add(_index)
                    'm_CriteriaCol.Remove(_index)
                End If
            Next
        Next

        If arrCommonCriterias.Count > 0 Then
            For i As Integer = arrCommonCriterias.Count - 1 To 0 Step -1
                m_CriteriaCol.Remove(CType(arrCommonCriterias(i), Integer))
            Next
        End If
    End Sub


    ' ''' <summary>
    ' ''' To Fill Patient Treeview with the Patients who meet the Health Plan Criterias
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private Sub FillPatients_old()
    '    With trvPatients
    '        .Nodes.Clear()
    '        For i As Int16 = 1 To m_PatientCol.Count
    '            Dim oNode As New myTreeNode
    '            With oNode
    '                ''Get the patient name
    '                .Text = oclsDM.GetPatientName(m_PatientCol(i))
    '                .Tag = m_PatientCol(i)
    '                If i = 1 Then
    '                    ''Initialize the Patient Id
    '                    m_PatientID = .Tag
    '                End If
    '                .SelectedImageIndex = 0
    '                .ImageIndex = 0
    '            End With

    '            .Nodes.Add(oNode)
    '            oNode = Nothing
    '        Next
    '    End With
    'End Sub


    Private Sub FillPatients()

        Dim dt As New DataTable
        Dim Col0 As New DataColumn("PatientID")
        Col0.DataType = System.Type.GetType("System.Decimal")
        dt.Columns.Add(Col0)
        Dim Col1 As New DataColumn("PatientName")
        Col1.DataType = System.Type.GetType("System.String")
        dt.Columns.Add(Col1)
        Dim row As DataRow = Nothing
        Dim oclsDM As New clsDM_Template
        For i As Int16 = 1 To m_PatientCol.Count

            row = dt.NewRow
            row("PatientID") = m_PatientCol(i)
            row("PatientName") = oclsDM.GetPatientName(m_PatientCol(i))
            If i = 1 Then
                ''Initialize the Patient Id
                m_PatientID = m_PatientCol(i)
            End If
            dt.Rows.Add(row)
        Next
        oclsDM = Nothing
        'If Not IsNothing(dt) Then
        '    GloUC_trvPatients.ImageIndex = 0
        '    GloUC_trvPatients.SelectedImageIndex = 0
        '    GloUC_trvPatients.ParentImageIndex = 1
        '    GloUC_trvPatients.SelectedParentImageIndex = 1
        '    GloUC_trvPatients.DataSource = dt

        '    GloUC_trvPatients.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation
        '    GloUC_trvPatients.CodeMember = Convert.ToString(dt.Columns("PatientName").ColumnName)
        '    GloUC_trvPatients.ValueMember = Convert.ToString(dt.Columns("PatientID").ColumnName)
        '    GloUC_trvPatients.DescriptionMember = Convert.ToString(dt.Columns("PatientName").ColumnName)
        '    GloUC_trvPatients.FillTreeView()
        'End If

    End Sub

    ''' <summary>
    ''' Fill the Health plan Trigger conditions in the TreeView as required
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BindHealthPlan()
        ''Bind the Treeview with patient specific health plan conditions and General health plan conditions
        '' commented by Sandip Darade 20090820
        ''dont remove criteria existing on the form 

        'trvHealthPlan.Nodes.Clear()

        ''First fill with the patient specific health plans from DM_patient
        FillPatientHealthPlanCriterias()

        ''Now Fill the general health plans satifies for the patient
        For _index As Integer = 1 To m_CriteriaCol.Count
            AddHealthPlanNode(CType(m_CriteriaCol(_index), Int64), False)
        Next

    End Sub
    Private Sub AddHealthPlanNode(ByVal _CriteriaId As Int64, ByVal blnManual As Boolean)
        Dim oCriteria As Criteria
        Dim rootNode As myTreeNode = Nothing
        Dim parentNode As myTreeNode = Nothing
        oCriteria = oDM.GetCriteria(_CriteriaId, m_PatientID)
        Dim objList As myList

        With oCriteria

            rootNode = New myTreeNode
            rootNode.Text = .Name & " - " & .DisplayMessage
            rootNode.NodeName = .Name
            rootNode.Name = .Name
            rootNode.Key = _CriteriaId
            rootNode.DMTemplateName = .Name
            rootNode.ImageIndex = 17
            rootNode.SelectedImageIndex = 17
            '' COMMENT BY SUDHIR 20090313 ''
            'rootNode.IsFinished = blnManual
            rootNode.IsFinished = oDM.IsInPatientHealthPlan(_CriteriaId, m_PatientID)
            ''Sandip Darade 20090820
            ''check for duplicate criteria 
            For Each n As myTreeNode In trvHealthPlan.Nodes
                If (rootNode.Key = n.Key) Then
                    If (n.Key = _CriteriaId) Then
                        If (_IsModifyCriteria = False) Then
                            Exit Sub
                        ElseIf (_SelectedCriteriaID = _CriteriaId And _IsModifyCriteria = True) Then
                            trvHealthPlan.Nodes.Remove(n)
                            Exit For
                        ElseIf (_SelectedCriteriaID = _CriteriaId And _IsModifyCriteria = False) Then

                        ElseIf (_SelectedCriteriaID <> _CriteriaId) Then

                            Exit Sub
                        End If

                    End If
                Else
                End If
            Next
            trvHealthPlan.Nodes.Add(rootNode)


            For i As Integer = 1 To .LabOrders.Count
                Try
                    Dim blnExist As Boolean = False
                    For Each TargetNode As myTreeNode In rootNode.Nodes
                        If TargetNode.Text = "Labs" Then

                            blnExist = True
                        End If
                    Next
                    If Not blnExist Then
                        parentNode = New myTreeNode
                        parentNode.Text = "Labs"
                        parentNode.ImageIndex = 6
                        parentNode.SelectedImageIndex = 6
                        rootNode.Nodes.Add(parentNode)
                    End If

                    'sarika DM Denormalization
                    'Dim LabName As String = oDM.GetLabTestName(.LabOrders.Item(i))
                    'Dim mynode As New myTreeNode(LabName, .LabOrders.Item(i))

                    'objList = New myList
                    objList = CType(.LabOrders.Item(i), myList)
                    Dim mynode As New myTreeNode(objList.Value, objList.ID)
                    mynode.DMTemplateName = objList.Value
                    objList = Nothing
                    '----

                    'check if selected node is rootnode
                    If Not IsNothing(mynode) Then
                        AddAssociates(mynode, parentNode)
                        mynode.Dispose()
                        mynode = Nothing
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Next

            For i As Integer = 1 To .RadiologyOrders.Count
                Try
                    Dim blnExist As Boolean = False
                    For Each TargetNode As myTreeNode In rootNode.Nodes
                        If TargetNode.Text = "Orders" Then

                            blnExist = True
                        End If
                    Next
                    If Not blnExist Then
                        parentNode = New myTreeNode
                        parentNode.Text = "Orders"
                        parentNode.ImageIndex = 7
                        parentNode.SelectedImageIndex = 7
                        rootNode.Nodes.Add(parentNode)
                    End If


                    'sarika DM Denormalization
                    'Dim RadiologyOrderName As String = oDM.GetRadiologyOrder(.RadiologyOrders.Item(i))
                    'Dim mynode As New myTreeNode(RadiologyOrderName, .RadiologyOrders.Item(i))

                    'objList = New myList
                    objList = CType(.RadiologyOrders.Item(i), myList)
                    Dim mynode As New myTreeNode(objList.Value, objList.ID)
                    mynode.DMTemplateName = objList.Value
                    objList = Nothing
                    '----


                    'check if selected node is rootnode
                    If Not IsNothing(mynode) Then
                        AddAssociates(mynode, parentNode)
                        mynode.Dispose()
                        mynode = Nothing
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Next

            For i As Integer = 1 To .Referrals.Count
                Try
                    Dim blnExist As Boolean = False
                    For Each TargetNode As myTreeNode In rootNode.Nodes
                        If TargetNode.Text = "Referrals" Then

                            blnExist = True
                        End If
                    Next

                    If Not blnExist Then
                        parentNode = New myTreeNode
                        parentNode.Text = "Referrals"
                        parentNode.ImageIndex = 10
                        parentNode.SelectedImageIndex = 10
                        rootNode.Nodes.Add(parentNode)
                    End If

                    'sarika DM Denormalization
                    '     Dim ReferralsName As String = oDM.GetRefferalName(.Referrals.Item(i))
                    'Dim mynode As New myTreeNode(ReferralsName, .Referrals.Item(i))

                    'objList = New myList
                    objList = CType(.Referrals.Item(i), myList)
                    Dim mynode As New myTreeNode(objList.Value, objList.ID)
                    mynode.DMTemplateName = objList.Value
                    objList = Nothing
                    '----

                    'check if selected node is rootnode
                    If Not IsNothing(mynode) Then
                        AddAssociates(mynode, parentNode)
                        mynode.Dispose()
                        mynode = Nothing
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Next

            For i As Integer = 1 To .RxDrugs.Count
                Try
                    Dim blnExist As Boolean = False
                    For Each TargetNode As myTreeNode In rootNode.Nodes
                        If TargetNode.Text = "Rx" Then

                            blnExist = True
                        End If
                    Next
                    If Not blnExist Then
                        parentNode = New myTreeNode
                        parentNode.Text = "Rx"
                        parentNode.ImageIndex = 9
                        parentNode.SelectedImageIndex = 9
                        rootNode.Nodes.Add(parentNode)
                    End If



                    'sarika DM Denormalization
                    '        Dim DrugName As String = oDM.GetDrugName(.RxDrugs.Item(i))
                    '    Dim mynode As New myTreeNode(DrugName, .RxDrugs.Item(i))

                    'objList = New myList
                    objList = CType(.RxDrugs.Item(i), myList)
                    Dim mynode As New myTreeNode(objList.Value, objList.ID, objList.DrugName, objList.Dosage, objList.DrugForm, objList.Route, objList.Frequency, objList.NDCCode, objList.IsNarcotic, objList.Duration, objList.mpid, objList.DrugQtyQualifier)
                    mynode.DMTemplateName = objList.DrugName
                    objList = Nothing
                    '----


                    'check if selected node is rootnode
                    If Not IsNothing(mynode) Then
                        AddAssociates(mynode, parentNode)
                        mynode.Dispose()
                        mynode = Nothing
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Next

            For i As Integer = 1 To .Guidelines.Count
                Try
                    Dim blnExist As Boolean = False
                    For Each TargetNode As myTreeNode In rootNode.Nodes
                        If TargetNode.Text = "Guidelines" Then

                            blnExist = True
                        End If
                    Next
                    If Not blnExist Then
                        parentNode = New myTreeNode
                        parentNode.Text = "Guidelines"
                        parentNode.ImageIndex = 8
                        parentNode.SelectedImageIndex = 8
                        rootNode.Nodes.Add(parentNode)
                    End If

                    '     Dim GuildLine As String = oDM.GetGuidLine(.Guidelines.Item(i))
                    Dim mynode As New myTreeNode '(GuildLine, .Guidelines.Item(i))
                    mynode.Tag = CType(.Guidelines.Item(i), myList).ID
                    mynode.Text = CType(.Guidelines.Item(i), myList).DMTemplateName
                    'mynode. = CType(.Guidelines.Item(i), myList).DMTemplateName 
                    mynode.DMTemplateName = CType(.Guidelines.Item(i), myList).DMTemplateName
                    mynode.DMTemplate = CType(oCriteria.Guidelines.Item(i), myList).DMTemplate
                    ''Sandip Darade 20090819
                    ''Add  template Id 
                    mynode.Key = CType(.Guidelines.Item(i), myList).ID

                    'check if selected node is rootnode
                    If Not IsNothing(mynode) Then
                        AddAssociates(mynode, parentNode)
                        mynode.Dispose()
                        mynode = Nothing
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Next

            '' chetan added for IM 13-oct 2010
            For i As Integer = 1 To .IMlst.Count
                Try
                    Dim blnExist As Boolean = False
                    For Each TargetNode As myTreeNode In rootNode.Nodes
                        If TargetNode.Text = "IM" Then

                            blnExist = True
                        End If
                    Next
                    If Not blnExist Then
                        parentNode = New myTreeNode
                        parentNode.Text = "IM"
                        parentNode.ImageIndex = 6
                        parentNode.SelectedImageIndex = 6
                        rootNode.Nodes.Add(parentNode)
                    End If

                    'sarika DM Denormalization
                    'Dim LabName As String = oDM.GetLabTestName(.LabOrders.Item(i))
                    'Dim mynode As New myTreeNode(LabName, .LabOrders.Item(i))

                    'objList = New myList
                    objList = CType(.IMlst.Item(i), myList)
                    Dim mynode As New myTreeNode(objList.Value, objList.ID)
                    mynode.DMTemplateName = objList.Value

                    mynode.NodeName = objList.Value
                    mynode.Key = objList.ID
                    mynode.DrugName = objList.DMTemplateName
                    mynode.Duration = objList.Duration
                    mynode.Route = objList.Route
                    mynode.Frequency = objList.Frequency
                    mynode.IsNarcotics = objList.IsNarcotic
                    mynode.NDCCode = objList.NDCCode
                    mynode.DrugName = objList.DrugName
                    mynode.DrugForm = objList.DrugForm
                    mynode.Dosage = objList.Dosage
                    mynode.DrugQtyQualifier = objList.DrugQtyQualifier



                    'lst.Index = myCriteria.Key


                    objList = Nothing
                    '----

                    'check if selected node is rootnode
                    If Not IsNothing(mynode) Then
                        AddAssociates(mynode, parentNode)
                        mynode.Dispose()
                        mynode = Nothing
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Next



        End With
        oCriteria.Dispose()
    End Sub

    Private Sub AddModifiedCriteria(ByVal oCriteria As Criteria, ByVal blnManual As Boolean)
        Dim rootNode As myTreeNode = Nothing
        Dim parentNode As myTreeNode = Nothing
        Dim objList As myList = Nothing

        With oCriteria
            rootNode = New myTreeNode
            rootNode.Text = .Name & " - " & .DisplayMessage
            rootNode.NodeName = .Name
            rootNode.Name = .Name
            rootNode.DMTemplateName = .Name
            rootNode.Key = oCriteria.ID '' _CriteriaId
            rootNode.ImageIndex = 17
            rootNode.SelectedImageIndex = 17
            rootNode.IsFinished = blnManual

            '' Check if the Criteria is Already exist in  the Health Plan 
            ''If it exists then Clear & Add the New orders to it

            Dim isCriateriaExists As Boolean = False
            For Each rootNode In trvHealthPlan.Nodes
                'sarika DM Denormalization 20090417
                'If rootNode.Key = oCriteria.ID Then
                If rootNode.Name = oCriteria.Name Then
                    '--
                    isCriateriaExists = True
                    Exit For
                End If
            Next

            If isCriateriaExists = True Then
                rootNode.Nodes.Clear()
            Else
                trvHealthPlan.Nodes.Add(rootNode)
            End If

            For i As Integer = 1 To .LabOrders.Count
                Try
                    Dim blnExist As Boolean = False
                    For Each TargetNode As myTreeNode In rootNode.Nodes
                        If TargetNode.Text = "Labs" Then

                            blnExist = True
                        End If
                    Next
                    If Not blnExist Then
                        parentNode = New myTreeNode
                        parentNode.Text = "Labs"
                        parentNode.ImageIndex = 6
                        parentNode.SelectedImageIndex = 6
                        rootNode.Nodes.Add(parentNode)
                    End If
                    'sarika DM Denormalization
                    'Dim LabName As String = oDM.GetLabTestName(.LabOrders.Item(i))
                    'Dim mynode As New myTreeNode(LabName, .LabOrders.Item(i))

                    ' objList = New myList
                    objList = CType(.LabOrders.Item(i), myList)
                    Dim mynode As New myTreeNode(objList.Value, objList.ID)
                    objList = Nothing
                    '----

                    'check if selected node is rootnode
                    If Not IsNothing(mynode) Then
                        AddAssociates(mynode, parentNode)
                        mynode.Dispose()
                        mynode = Nothing
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Next

            For i As Integer = 1 To .RadiologyOrders.Count
                Try
                    Dim blnExist As Boolean = False
                    For Each TargetNode As myTreeNode In rootNode.Nodes
                        If TargetNode.Text = "Orders" Then

                            blnExist = True
                        End If
                    Next
                    If Not blnExist Then
                        parentNode = New myTreeNode
                        parentNode.Text = "Orders"
                        parentNode.ImageIndex = 7
                        parentNode.SelectedImageIndex = 7
                        rootNode.Nodes.Add(parentNode)
                    End If

                    'sarika DM Denormalization
                    'Dim RadiologyOrderName As String = oDM.GetRadiologyOrder(.RadiologyOrders.Item(i))
                    'Dim mynode As New myTreeNode(RadiologyOrderName, .RadiologyOrders.Item(i))

                    ' objList = New myList
                    objList = CType(.RadiologyOrders.Item(i), myList)
                    Dim mynode As New myTreeNode(objList.Value, objList.ID)
                    objList = Nothing
                    '----

                    'check if selected node is rootnode
                    If Not IsNothing(mynode) Then
                        AddAssociates(mynode, parentNode)
                        mynode.Dispose()
                        mynode = Nothing
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Next

            For i As Integer = 1 To .Referrals.Count
                Try
                    Dim blnExist As Boolean = False
                    For Each TargetNode As myTreeNode In rootNode.Nodes
                        If TargetNode.Text = "Referrals" Then

                            blnExist = True
                        End If
                    Next

                    If Not blnExist Then
                        parentNode = New myTreeNode
                        parentNode.Text = "Referrals"
                        parentNode.ImageIndex = 10
                        parentNode.SelectedImageIndex = 10
                        rootNode.Nodes.Add(parentNode)
                    End If


                    'sarika DM Denormalization
                    '     Dim ReferralsName As String = oDM.GetRefferalName(.Referrals.Item(i))
                    'Dim mynode As New myTreeNode(ReferralsName, .Referrals.Item(i))

                    'objList = New myList
                    objList = CType(.Referrals.Item(i), myList)
                    Dim mynode As New myTreeNode(objList.Value, objList.ID)
                    objList = Nothing
                    '----

                    'check if selected node is rootnode
                    If Not IsNothing(mynode) Then
                        AddAssociates(mynode, parentNode)
                        mynode.Dispose()
                        mynode = Nothing
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Next

            For i As Integer = 1 To .RxDrugs.Count
                Try
                    Dim blnExist As Boolean = False
                    For Each TargetNode As myTreeNode In rootNode.Nodes
                        If TargetNode.Text = "Rx" Then

                            blnExist = True
                        End If
                    Next
                    If Not blnExist Then
                        parentNode = New myTreeNode
                        parentNode.Text = "Rx"
                        parentNode.ImageIndex = 9
                        parentNode.SelectedImageIndex = 9
                        rootNode.Nodes.Add(parentNode)
                    End If

                    'sarika DM Denormalization
                    '        Dim DrugName As String = oDM.GetDrugName(.RxDrugs.Item(i))
                    '    Dim mynode As New myTreeNode(DrugName, .RxDrugs.Item(i))

                    ' objList = New myList
                    objList = CType(.RxDrugs.Item(i), myList)
                    Dim mynode As New myTreeNode(objList.Value, objList.ID, objList.DrugName, objList.Dosage, objList.DrugForm, objList.Route, objList.Frequency, objList.NDCCode, objList.IsNarcotic, objList.Duration, objList.mpid, objList.DrugQtyQualifier)
                    objList = Nothing
                    '----
                    'check if selected node is rootnode
                    If Not IsNothing(mynode) Then
                        AddAssociates(mynode, parentNode)
                        mynode.Dispose()
                        mynode = Nothing
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Next

            For i As Integer = 1 To .Guidelines.Count
                Try
                    Dim blnExist As Boolean = False
                    For Each TargetNode As myTreeNode In rootNode.Nodes
                        If TargetNode.Text = "Guidelines" Then

                            blnExist = True
                        End If
                    Next
                    If Not blnExist Then
                        parentNode = New myTreeNode
                        parentNode.Text = "Guidelines"
                        parentNode.ImageIndex = 8
                        parentNode.SelectedImageIndex = 8
                        rootNode.Nodes.Add(parentNode)
                    End If

                    Dim GuildLine As String = oDM.GetGuidLine(.Guidelines.Item(i))
                    Dim mynode As New myTreeNode(GuildLine, .Guidelines.Item(i))

                    'check if selected node is rootnode
                    If Not IsNothing(mynode) Then
                        AddAssociates(mynode, parentNode)
                        mynode.Dispose()
                        mynode = Nothing
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Next
        End With
    End Sub

    Sub FillPatientHealthPlanCriterias()

        Dim rootNode As myTreeNode
        '  Dim parentNode As myTreeNode
        ''loop through the  avialble patient Specific Criterias
        For _index As Int32 = 0 To oPatTriggers.Rows.Count - 1
            Dim _CriteriaId As Int64
            _CriteriaId = oPatTriggers.Rows(_index)("DM_nCriteriaID")
            If _CriteriaId > 0 Then

                ''Get the criteria object based on criteria Id
                Dim oCriteria As Criteria
                oCriteria = oDM.GetCriteria(_CriteriaId, m_PatientID)
                With oCriteria

                    rootNode = New myTreeNode
                    rootNode.Text = .Name & " - " & .DisplayMessage
                    rootNode.Key = _CriteriaId
                    rootNode.ImageIndex = 17
                    rootNode.IsFinished = oDM.IsInPatientHealthPlan(_CriteriaId, m_PatientID)
                    rootNode.SelectedImageIndex = 17
                    trvHealthPlan.Nodes.Add(rootNode)
                End With
                ''Get all the Due guidelines in details w.r.to the selected criteria
                Dim oPatientCriterias As DataTable
                oPatientCriterias = oDM.FindPatientSpecificDueTriggers(m_PatientID, _CriteriaId)
                If (IsNothing(oPatientCriterias) = False) Then
                    For _cnt As Int32 = 0 To oPatientCriterias.Rows.Count - 1
                        Dim CriteriaName As String = ""
                        Dim TriggerName As String = "", strType As String = ""
                        Dim imgIndex As Int32
                        Dim TriggerId, TransId As Int64

                        Dim TriggerInfo As String = ""
                        Dim Route As String = ""
                        Dim Frequency As String = ""
                        Dim Duration As String = ""
                        Dim DrugForm As String = ""
                        Dim NDCCode As String = ""
                        Dim IsNarcotics As Integer = 0

                        Dim DrugQtyQualifier As String = ""

                        'sarika DM Denormalization 20090403
                        Dim TriggerData As Object = Nothing
                        '---
                        Dim bIsRecurring As Boolean = False

                        TransId = oPatientCriterias.Rows(_cnt)("DM_TransId")
                        TriggerId = oPatientCriterias.Rows(_cnt)("DM_nTriggerID")
                        bIsRecurring = oPatientCriterias.Rows(_cnt)("DM_bIsRecurring")

                        'sarika DM Denormalization 20090403
                        If Not IsDBNull(oPatientCriterias.Rows(_cnt)("DM_TriggerName")) = True Then
                            TriggerName = oPatientCriterias.Rows(_cnt)("DM_TriggerName")
                        Else
                            TriggerName = ""
                        End If
                        If Not IsDBNull(oPatientCriterias.Rows(_cnt)("DM_sResult")) = True Then
                            TriggerData = oPatientCriterias.Rows(_cnt)("DM_sResult")

                        Else
                            TriggerData = Nothing
                        End If
                        '---

                        ''check whether the guideline is due w.r.to due value set
                        If oDM.CheckDueGuidelines(TransId, bIsRecurring, m_PatientID) Then

                            Select Case oPatientCriterias.Rows(_cnt)("DM_nType")
                                Case DiseaseManagement.TemplateCategoryID.Guidelines
                                    'sarika DM Denormalization 20090403
                                    '    TriggerName = oDM.GetGuidLine(TriggerId)
                                    '---
                                    'TriggerName = 
                                    strType = "Guidelines"
                                    imgIndex = 8
                                Case DiseaseManagement.TemplateCategoryID.Labs
                                    '   TriggerName = oDM.GetLabResultName(TriggerId)
                                    strType = "Labs"
                                    imgIndex = 6

                                Case DiseaseManagement.TemplateCategoryID.Radiology
                                    'TriggerName = oDM.GetRadiologyOrder(TriggerId)
                                    strType = "Orders"
                                    imgIndex = 7

                                Case DiseaseManagement.TemplateCategoryID.Rx
                                    ' TriggerName = oDM.GetDrugName(TriggerId)
                                    strType = "Rx"
                                    imgIndex = 9

                                Case DiseaseManagement.TemplateCategoryID.Referrals
                                    '  TriggerName = oDM.GetRefferalName(TriggerId)
                                    strType = "Referrals"
                                    imgIndex = 10

                            End Select

                            CriteriaName = oPatientCriterias.Rows(_cnt)("DM_CriteriaName")
                            TriggerInfo = oPatientCriterias.Rows(_cnt)("DM_TriggerDtlInfo")
                            DrugForm = oPatientCriterias.Rows(_cnt)("sDrugForm")
                            Route = oPatientCriterias.Rows(_cnt)("sRoute")
                            Duration = oPatientCriterias.Rows(_cnt)("sDuration")
                            Frequency = oPatientCriterias.Rows(_cnt)("sFrequency")
                            NDCCode = oPatientCriterias.Rows(_cnt)("sNDCCode")
                            IsNarcotics = oPatientCriterias.Rows(_cnt)("nIsNarcotics")
                            DrugQtyQualifier = oPatientCriterias.Rows(_cnt)("sDrugQtyQualifier")
                              ''Add patient specific due value to the tree node

                            'sarika DM Denormalization 20090403
                            'AddCriteriaNodes(TransId, TriggerId, TriggerName, bIsRecurring, rootNode, strType, imgIndex)
                            '   AddCriteriaNodes(TransId, TriggerId, TriggerName, TriggerData, bIsRecurring, rootNode, strType, imgIndex)
                            AddCriteriaNodes(TransId, TriggerId, TriggerName, TriggerData, bIsRecurring, rootNode, strType, imgIndex, CriteriaName, TriggerInfo, DrugForm, Route, Frequency, Duration, NDCCode, IsNarcotics, DrugQtyQualifier)
                            ' TriggerData = Nothing
                            '---


                        End If
                    Next

                    oPatientCriterias.Dispose()
                    oPatientCriterias = Nothing
                End If

                If rootNode.GetNodeCount(False) <= 0 Then
                    rootNode.Remove()
                End If

                oCriteria.Dispose()
                oCriteria = Nothing
            End If
        Next


    End Sub

    ''' <summary>
    ''' Create Due String Using Following Parameter
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetDueDescString(ByVal DueType As String, ByVal DueValue As String, ByVal Reason As String, ByVal Note As String) As String
        Return Nothing
    End Function

    ''' <summary>
    ''' Save health plan triggers i.e orders
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidateHealthPlan()
        arrLabs = New ArrayList
        arrRadiology = New ArrayList
        arrGuidline = New ArrayList
        arrDrug = New ArrayList
        Dim arrIM As ArrayList = New ArrayList

        Dim gloArrLabs As ArrayList = New ArrayList() ''Added by Abhijeet on 20100626

        ''loop through each Criteria Nodes in treeview
        For Each myCriteria As myTreeNode In trvHealthPlan.Nodes
            ''loop through each Parent node(i.e. labs, Orders, Rx, Guidelines, Referrals) in Criteria node
            For Each parentNode As myTreeNode In myCriteria.Nodes
                ''loop through each child node   i.e Trigger item
                For Each mynode As myTreeNode In parentNode.Nodes
                    ''If selected to give the respective triggers

                    Dim Emdeonlst As New gloEmdeonCommon.myList '' by Abhijeet on 20100626


                    If mynode.Checked = True Then
                        ''Add it to collections for opening in respective transaction forms
                        Select Case mynode.Parent.Text
                            Case "Labs"
                                lst = New myList
                                lst.Value = mynode.NodeName
                                lst.DMTemplateName = mynode.DMTemplateName
                                lst.ID = mynode.Key
                                lst.IsFinished = False
                                lst.Index = myCriteria.Key
                                arrLabs.Add(lst)

                                '' by ABhijeet on 20100626,used Emdeoncommon.mylist instead of gloEMR.myList                                
                                Emdeonlst.Value = mynode.NodeName
                                Emdeonlst.DMTemplateName = mynode.DMTemplateName
                                Emdeonlst.ID = mynode.Key
                                Emdeonlst.IsFinished = False
                                Emdeonlst.Index = myCriteria.Key
                                gloArrLabs.Add(Emdeonlst)
                                '' End of changes by Abhijeet on 20100626 

                            Case "Orders"
                                lst = New myList
                                lst.Value = mynode.NodeName
                                lst.ID = mynode.Key
                                lst.IsFinished = False
                                lst.Index = myCriteria.Key
                                arrRadiology.Add(lst)
                            Case "Referrals"

                            Case "Rx"
                                lst = New myList
                                lst.Value = mynode.NodeName
                                lst.ID = mynode.Key
                                lst.DMTemplateName = mynode.DrugName
                                lst.Duration = mynode.Duration
                                lst.Route = mynode.Route
                                lst.Frequency = mynode.Frequency
                                lst.IsNarcotic = mynode.IsNarcotics
                                lst.NDCCode = mynode.NDCCode
                                lst.DrugName = mynode.DrugName
                                lst.DrugForm = mynode.DrugForm
                                lst.Dosage = mynode.Dosage
                                lst.DrugQtyQualifier = mynode.DrugQtyQualifier
                                lst.NDCCode = mynode.NDCCode
                                lst.mpid = mynode.mpid

                                lst.IsFinished = False
                                lst.Index = myCriteria.Key
                                arrDrug.Add(lst)
                            Case "Guidelines"
                                Dim Associatenode As myTreeNode
                                Associatenode = mynode
                                lst = New myList
                                lst.Value = mynode.NodeName
                                lst.ID = mynode.Key
                                lst.DMTemplateName = mynode.Text
                                ''Sandip Darade  20100222 bug id 6182
                                '' if no templatedata  present for the current template pull from template gallery master
                                If Not IsNothing(mynode.DMTemplate) Then

                                    If (CType(mynode.DMTemplate, Array).GetUpperBound(0)) <= 0 Then

                                        Dim ob As Object = oDM.GetTemplateByName(mynode.NodeName)
                                        mynode.DMTemplate = ob
                                    End If
                                Else

                                    Dim ob As Object = oDM.GetTemplateByName(mynode.NodeName)
                                    mynode.DMTemplate = ob
                                End If
                                lst.DMTemplate = mynode.DMTemplate
                                lst.DMSID = mynode.Key
                                lst.IsFinished = False
                                lst.Index = myCriteria.Key
                                arrGuidline.Add(lst)

                            Case "IM" '' chetan added

                                lst = New myList
                                lst.Value = mynode.NodeName
                                lst.ID = mynode.Key
                                lst.DMTemplateName = mynode.Text
                                lst.Duration = mynode.Duration
                                lst.Route = mynode.Route
                                lst.Frequency = mynode.Frequency
                                lst.IsNarcotic = mynode.IsNarcotics
                                lst.NDCCode = mynode.NDCCode
                                lst.DrugName = mynode.DrugName
                                lst.DrugForm = mynode.DrugForm
                                lst.Dosage = mynode.Dosage
                                lst.DrugQtyQualifier = mynode.DrugQtyQualifier
                                lst.NDCCode = mynode.NDCCode

                                lst.IsFinished = False
                                lst.Index = myCriteria.Key

                                arrIM.Add(lst)
                        End Select
                    Else
                        ''If not selected check whether Reason for not giving is documented or not
                        If mynode.TemplateResult Is Nothing Then
                            ''Reson Not documented, so prompt the user to document the reason
                            ''Sanjog-Added on 20101130 to show the parent name to ask for overriding
                            MessageBox.Show("This " & parentNode.Text & " is recommended. Please provide reason for overriding", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)
                            ''show the user with required info to document the reason against the node
                            trvHealthPlan.SelectedNode = mynode

                            Dim objTrvArgs As New TreeNodeMouseClickEventArgs(trvHealthPlan.SelectedNode, Windows.Forms.MouseButtons.Left, 2, trvHealthPlan.SelectedNode.Bounds.X, trvHealthPlan.SelectedNode.Bounds.Y)
                            trvHealthPlan_NodeMouseDoubleClick(objSender, objTrvArgs)
                            Exit Sub
                        Else
                            ''Get the Trigger deatils as object and validate for reason field
                            trvHealthPlan.SelectedNode = mynode
                            Dim oTrigger As TriggerDetails = CType(mynode.TemplateResult, TriggerDetails)
                            oTrigger = CType(CType(trvHealthPlan.SelectedNode, myTreeNode).TemplateResult, TriggerDetails)

                            If oTrigger.Reason = "" Then
                                ''Reson Not documented, so prompt the user to document the reason
                                ''Sanjog-Added on 20101130 to show the parent name to ask for overriding
                                MessageBox.Show("This " & parentNode.Text & " is recommended. Please provide reason for overriding", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)
                                ''show the user with required info to document the reason against the node
                                trvHealthPlan.SelectedNode = mynode
                                Dim objTrvArgs As New TreeNodeMouseClickEventArgs(trvHealthPlan.SelectedNode, Windows.Forms.MouseButtons.Left, 2, trvHealthPlan.SelectedNode.Bounds.X, trvHealthPlan.SelectedNode.Bounds.Y)
                                trvHealthPlan_NodeMouseDoubleClick(objSender, objTrvArgs)
                                Exit Sub
                            Else
                                ''Save this record info in DB as not given status and 

                            End If
                        End If
                    End If

                Next
            Next
        Next
        ''Check labs to be given are available
        If arrLabs.Count > 0 Then

            'Modified according to latest requirement
            'Added by madan on 20100601
            Dim _TestList As String = String.Empty
            Dim _MyTestList As gloEmdeonCommon.myList = Nothing
            ''End of by Abhijeet on 20100625

            _TestList += "Lab Tests:" & vbNewLine
            For index As Integer = 0 To gloArrLabs.Count - 1
                _MyTestList = gloArrLabs(index)
                If index = 0 Then
                    _TestList += _MyTestList.Value
                Else
                    _TestList += ", " + _MyTestList.Value
                End If

            Next

            'Added by madan on 20100619
            If gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_Provider_Usage <> "" Then

                Select Case gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_Provider_Usage

                    Case "TASK"
                        gloLabSettings("TASK", _TestList, arrLabs)
                    Case "LABORDER"
                        gloLabSettings("LABORDER")
                    Case "RECORDRESULTS"
                        gloLabSettings("RECORDRESULTS", "", gloArrLabs)
                    Case "ASK"
                        ' new modal dialog for instant choice for next action to be performed.
                        Dim frmAskform As New gloEmdeonInterface.Forms.frmCnfrmLabFlow()
                        frmAskform.ShowInTaskbar = False
                        frmAskform.BringToFront()
                        frmAskform.ShowDialog(IIf(IsNothing(frmAskform.Parent), Me, frmAskform.Parent))
                        gloLabSettings(frmAskform.LabFlowConfirm, _TestList, gloArrLabs)
                        frmAskform.Dispose()
                        frmAskform = Nothing
                    Case Else
                        MessageBox.Show("Please configure Default user for Task in EMR Admin - Lab Settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Select
                End Select
            End If
            ''Madan added for viewing gloLab-- on 20100406
            'If gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_hsilabel <> "" Then



            '    Dim frm_viewgloLab As New gloEmdeonInterface.Forms.frmViewgloLab(m_PatientID)
            '    With frm_viewgloLab
            '        .LabOrderParameter.OrderID = 0
            '        .LabOrderParameter.OrderNumberID = 0
            '        .LabOrderParameter.OrderNumberPrefix = "ORD"
            '        .LabOrderParameter.PatientID = m_PatientID
            '        .LabOrderParameter.VisitID = gnVisitID
            '        .LabOrderParameter.TransactionDate = Now
            '        .LabOrderParameter.CloseAfterSave = True

            '        .WindowState = FormWindowState.Maximized
            '        .StartPosition = FormStartPosition.CenterScreen
            '        .BringToFront()
            '        .ShowInTaskbar = False
            '        .ShowDialog(Me.Owner)
            '    End With
            'Else
            '    Dim frm As New frmLab_RequestOrder
            '    With frm.LabOrderParameter
            '        .IsEditMode = False
            '        .OrderID = 0
            '        .OrderNumberID = 0
            '        .OrderNumberPrefix = "ORD"
            '        .PatientID = m_PatientID
            '        .VisitID = gnVisitID
            '        .TransactionDate = Now
            '        .CloseAfterSave = True
            '    End With
            '    With frm
            '        '                .MdiParent = Me.ParentForm
            '        '.WindowState = FormWindowState.Maximized
            '        ''pass the collection of labs to be given
            '        ._arrLabs = arrLabs
            '        .StartPosition = FormStartPosition.CenterScreen
            '        .BringToFront()
            '        .ShowInTaskbar = False
            '        .ShowDialog(Me.Owner)
            '    End With
            'End If


        End If
        ''Check Orders orders  to be given are available
        If arrRadiology.Count > 0 Then
            'Dim frm As New frm_LM_Orders(m_VisitID, Now, m_PatientID)
            Dim frm As frm_LM_Orders
            frm = frm_LM_Orders.GetInstance(m_VisitID, Now, m_PatientID)

            If IsNothing(frm) = True Then
                Exit Sub
            End If
            With frm
                '._ExamID = m_ExamID
                ''pass the collection of orders to be given
                ._ArrRadi = arrRadiology
                '._patientID = m_PatientID
                .StartPosition = FormStartPosition.CenterScreen
                '._VisitID = gnVisitID
                ._VisitID = m_VisitID
                ._VisitDate = Now
                .ShowInTaskbar = False
                ' .MdiParent = Me.ParentForm
                ' .WindowState = FormWindowState.Maximized
                .BringToFront()
                .ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                .Dispose()
            End With
        End If

        'Check Guideline templates to be given are available
        If arrGuidline.Count > 0 Then
            Dim frm As New frmDM_Template(arrGuidline, m_PatientID)
            With frm
                .StartPosition = FormStartPosition.CenterScreen
                .ShowInTaskbar = False
                .ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                .Dispose()
            End With
        End If

        'Check Drug to be given are available


        If arrDrug.Count > 0 Then
            Dim frm As frmPrescription
            Dim obj As New clsProvider
            frm = frmPrescription.GetInstance(arrDrug, CType(obj.GetPatientProvider(m_PatientID), Long), 0, m_PatientID)
            obj.Dispose()
            obj = Nothing
            If IsNothing(frm) = True Then
                Exit Sub
            End If
            If frmPrescription.IsOpen = False Then
                'Incident #00013567 : Medication carry forward case
                'following changes done to resolve incident
                'If frm.LockForm(m_PatientID) = False Then
                '    frm.Dispose()
                '    frm = Nothing
                'Else
                With frm
                    '.ShowReconcileMessage()
                    .IsfrmDM = True
                    .StartPosition = FormStartPosition.CenterParent
                    .ShowInTaskbar = False
                    Try
                        .ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))

                    Catch ex As InvalidOperationException
                        MessageBox.Show("Rx/Meds screen cannot be opened as it is already open in the background.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Finally
                        If Not IsNothing(frm) = True Then
                            frm.Dispose()
                            frm = Nothing
                        End If
                    End Try

                End With
                'End If
            Else
                MessageBox.Show("Rx/Meds screen cannot be opened as it is already open in the background.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        End If

        ' chetan added for IM
        If arrIM.Count > 0 Then
            For i As Integer = 0 To arrIM.Count - 1
                Dim lst As myList
                lst = CType(arrIM(i), myList)
                Dim frm As New frmImTransaction(0, m_PatientID, lst)
                frm.Text = "Add Immunization"
                frm.ShowInTaskbar = False
                frm.StartPosition = FormStartPosition.CenterParent
                frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))

                frm.Dispose()
                frm = Nothing
            Next


        End If

        'For i As Integer = 0 To arrLabs.Count - 1
        '    Dim lst As New myList
        '    lst = CType(arrLabs(i), myList)
        '    SaveGivenTriggers(lst.Index, lst, DiseaseManagement.TemplateCategoryID.Labs, lst.IsFinished)
        'Next

        For i As Integer = 0 To gloArrLabs.Count - 1
            Dim lst As New myList 'gloEmdeonCommon.myList
            '  lst = CType(gloArrLabs(i), myList) 'gloEmdeonCommon.myList)
            lst.Value = CType(gloArrLabs(i), gloEmdeonCommon.myList).Value
            lst.DMTemplateName = CType(gloArrLabs(i), gloEmdeonCommon.myList).DMTemplateName
            lst.ID = CType(gloArrLabs(i), gloEmdeonCommon.myList).ID
            lst.IsFinished = CType(gloArrLabs(i), gloEmdeonCommon.myList).IsFinished
            lst.Index = CType(gloArrLabs(i), gloEmdeonCommon.myList).Index
            'lst.IsFinished = CType(gloArrLabs(i), gloEmdeonCommon.myList).IsFinished
            'lst = CType(lst, myList)
            SaveGivenTriggers(lst.Index, lst, DiseaseManagement.TemplateCategoryID.Labs, lst.IsFinished)
        Next
        For i As Integer = 0 To arrRadiology.Count - 1
            Dim lst As myList
            lst = CType(arrRadiology(i), myList)
            'sarika DM Denormalization
            ' SaveGivenTriggers(lst.Index, lst.ID, DiseaseManagement.TemplateCategoryID.Radiology, lst.IsFinished)
            SaveGivenTriggers(lst.Index, lst, DiseaseManagement.TemplateCategoryID.Radiology, lst.IsFinished)

            '---
        Next

        For i As Integer = 0 To arrGuidline.Count - 1
            Dim lst As myList
            lst = CType(arrGuidline(i), myList)
            'sarika DM Denormalization 20090403
            'SaveGivenTriggers(lst.Index, lst.ID, DiseaseManagement.TemplateCategoryID.Guidelines, lst.IsFinished)
            ' SaveGivenTriggers(lst.Index, lst.ID, lst.DMTemplateName, lst.DMTemplate, DiseaseManagement.TemplateCategoryID.Guidelines, lst.IsFinished)
            SaveGivenTriggers(lst.Index, lst, DiseaseManagement.TemplateCategoryID.Guidelines, lst.IsFinished)
            '---
        Next

        For i As Integer = 0 To arrDrug.Count - 1
            Dim lst As myList
            lst = CType(arrDrug(i), myList)
            'sarika DM Denormalization
            '             SaveGivenTriggers(lst.Index, lst.ID, DiseaseManagement.TemplateCategoryID.Rx, lst.IsFinished)
            SaveGivenTriggers(lst.Index, lst, DiseaseManagement.TemplateCategoryID.Rx, lst.IsFinished)

            '---
        Next

        For i As Integer = 0 To arrIM.Count - 1
            Dim lst As myList
            lst = CType(arrIM(i), myList)
            SaveGivenTriggers(lst.Index, lst, DiseaseManagement.TemplateCategoryID.IM, lst.IsFinished)

        Next

        ''Commented by sanjog on 20101129 to dnt ask description
        '    For i As Integer = 0 To arrLabs.Count - 1
        '        Dim lst As New myList
        '        lst = CType(arrLabs(i), myList)
        '        If lst.IsFinished = False Then
        '            If ValidateTrigger(lst.Index, lst.ID, DiseaseManagement.TemplateCategoryID.Labs) = False Then
        '                Exit Sub
        '            End If
        '        End If
        '    Next
        '    For i As Integer = 0 To arrRadiology.Count - 1
        '        Dim lst As New myList
        '        lst = CType(arrRadiology(i), myList)
        '        If lst.IsFinished = False Then
        '            If ValidateTrigger(lst.Index, lst.ID, DiseaseManagement.TemplateCategoryID.Radiology) = False Then
        '                Exit Sub
        '            End If
        '        End If
        '    Next
        '    For i As Integer = 0 To arrGuidline.Count - 1
        '        Dim lst As New myList
        '        lst = CType(arrGuidline(i), myList)
        '        If lst.IsFinished = False Then
        '            If ValidateTrigger(lst.Index, lst.ID, DiseaseManagement.TemplateCategoryID.Guidelines) = False Then
        '                Exit Sub
        '            End If
        '        End If
        '    Next

        '    For i As Integer = 0 To arrDrug.Count - 1
        '        Dim lst As New myList
        '        lst = CType(arrDrug(i), myList)
        '        If lst.IsFinished = False Then
        '            If ValidateTrigger(lst.Index, lst.ID, DiseaseManagement.TemplateCategoryID.Rx) = False Then
        '                Exit Sub
        '            End If
        '        End If
        'Next

        ' '' chetan integrated for IM
        'For i As Integer = 0 To arrIM.Count - 1
        '    Dim lst As New myList
        '    lst = CType(arrIM(i), myList)
        '    If lst.IsFinished = False Then
        '        If ValidateTrigger(lst.Index, lst.ID, DiseaseManagement.TemplateCategoryID.IM) = False Then
        '            Exit Sub
        '        End If
        '    End If
        'Next
        ''Commented by sanjog on 20101129 to dnt ask description

        SaveDueTriggers()
        ' Me.Close()
        gloWord.WordDialogBoxBackgroundCloser.Close(Me)
    End Sub
    Dim _Messageshown As Boolean
    Private Sub AskforGuideline()
        For Each myCriteria As myTreeNode In trvHealthPlan.Nodes
            ''loop through each Parent node(i.e. labs, Orders, Rx, Guidelines, Referrals) in Criteria node

            For Each parentNode As myTreeNode In myCriteria.Nodes
                ''loop through each child node   i.e Trigger item

                For Each mynode As myTreeNode In parentNode.Nodes
                    ''If selected to give the respective triggers
                    If mynode.Checked = False Then
                        If mynode.TemplateResult Is Nothing Then
                            ''Reson Not documented, so prompt the user to document the reason
                            ''Sanjog-Added on 20101130 to show the parent name to ask for overriding
                            MessageBox.Show("This " & parentNode.Text & " is recommended. Please provide reason for overriding", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)
                            ''show the user with required info to document the reason against the node
                            _Messageshown = True
                            trvHealthPlan.SelectedNode = mynode
                            Dim objTrvArgs As New TreeNodeMouseClickEventArgs(trvHealthPlan.SelectedNode, Windows.Forms.MouseButtons.Left, 2, trvHealthPlan.SelectedNode.Bounds.X, trvHealthPlan.SelectedNode.Bounds.Y)
                            trvHealthPlan_NodeMouseDoubleClick(objSender, objTrvArgs)
                            Exit Sub
                        End If
                    End If
                Next
                'If (_Messageshown = True) Then
                '    Exit For
                'End If
            Next
            'If (_Messageshown = True) Then
            '    Exit For
            'End If
        Next

    End Sub
    '''' <summary>
    '''' Not Called from Anywhere
    '''' </summary>
    '''' <param name="_CriteriaID"></param>
    '''' <param name="_TriggerId"></param>
    '''' <param name="nType"></param>
    '''' <param name="bIsgiven"></param>
    '''' <remarks></remarks>
    'Private Sub SaveGivenTriggers(ByVal _CriteriaID As Int64, ByVal _TriggerId As Int64, ByVal nType As DiseaseManagement.TemplateCategoryID, ByVal bIsgiven As Boolean)
    '    For Each myCriteria As myTreeNode In trvHealthPlan.Nodes
    '        ''loop through each Parent node(i.e. labs, Orders, Rx, Guidelines, Referrals) in Criteria node
    '        If myCriteria.Key = _CriteriaID Then
    '            For Each parentNode As myTreeNode In myCriteria.Nodes
    '                ''loop through each child node   i.e Trigger item
    '                For Each mynode As myTreeNode In parentNode.Nodes
    '                    If mynode.Key = _TriggerId Then
    '                        If bIsgiven Then
    '                            ''If not selected check whether Reason for not giving is documented or not
    '                            If mynode.TemplateResult Is Nothing Then
    '                                ''Delete the Exisiting record if present
    '                                oclsDM_PatientSpecific.DeleteTemplate(_CriteriaID, _TriggerId, m_PatientID)
    '                                'sarika DM Denormalization 20090403
    '                                '   oclsDM_PatientSpecific.Save_Trigger(0, m_PatientID, _CriteriaID, _TriggerId, Format(Now, "MM/dd/yyyy"), "", frmDM_Template.Type.Save, nType, , , , , , True, False)
    '                                oclsDM_PatientSpecific.Save_Trigger(0, m_PatientID, _CriteriaID, _TriggerId, mynode.DMTemplateName, myCriteria.Name, mynode.DMTemplate, Format(Now, "MM/dd/yyyy"), "", frmDM_Template.Type.Save, nType, , , , , , True, False, mynode.Dosage)
    '                                '---
    '                            Else
    '                                ''Get the Trigger details as object and validate for reason field

    '                                Dim oTrigger As TriggerDetails = CType(mynode.TemplateResult, TriggerDetails)
    '                                If oTrigger.Recurring Then

    '                                    ''Delete the Exisiting record if present
    '                                    oclsDM_PatientSpecific.DeleteTemplate(_CriteriaID, _TriggerId, m_PatientID)

    '                                    Dim TrnsId As Int64
    '                                    TrnsId = oclsDM_PatientSpecific.Save_Trigger(0, m_PatientID, _CriteriaID, _TriggerId, mynode.DMTemplateName, myCriteria.Name, mynode.DMTemplate, Format(Now, "MM/dd/yyyy"), "", frmDM_Template.Type.Save, nType, oTrigger.DueType, oTrigger.DueValue, False, oTrigger.Reason, oTrigger.Notes, False, True, mynode.Dosage)
    '                                    ''Here u need to insert the recurring value details
    '                                    If TrnsId <> 0 Then
    '                                        oclsDM_PatientSpecific.Save_TemplateDetail(TrnsId, oTrigger.StartDate, oTrigger.EndDate, oTrigger.DurationType, oTrigger.DurationPeriod)
    '                                    End If
    '                                Else
    '                                    ''Delete the Exisiting record if present
    '                                    oclsDM_PatientSpecific.DeleteTemplate(_CriteriaID, _TriggerId, m_PatientID)
    '                                    oclsDM_PatientSpecific.Save_Trigger(0, m_PatientID, _CriteriaID, _TriggerId, mynode.DMTemplateName, myCriteria.Name, mynode.DMTemplate, Format(Now, "MM/dd/yyyy"), "", frmDM_Template.Type.Save, nType, oTrigger.DueType, oTrigger.DueValue, False, oTrigger.Reason, oTrigger.Notes, True, False, mynode.Dosage)
    '                                End If
    '                            End If
    '                            If parentNode.Nodes.Count > 1 Then
    '                                mynode.Remove()
    '                            Else
    '                                parentNode.Remove()
    '                            End If
    '                        Else
    '                            mynode.Checked = False
    '                        End If


    '                    End If
    '                Next
    '            Next
    '        End If

    '    Next

    'End Sub


    Private Sub SaveInvalidDueTriggers()
        For Each myCriteria As myTreeNode In trvHealthPlan.Nodes
            ''loop through each Parent node(i.e. labs, Orders, Rx, Guidelines, Referrals) in Criteria node

            For Each parentNode As myTreeNode In myCriteria.Nodes
                ''loop through each child node   i.e Trigger item
                For Each mynode As myTreeNode In parentNode.Nodes

                    ''If not selected only
                    Dim nType As DiseaseManagement.TemplateCategoryID
                    Select Case parentNode.Text
                        Case "Labs"
                            nType = DiseaseManagement.TemplateCategoryID.Labs
                        Case "Orders"
                            nType = DiseaseManagement.TemplateCategoryID.Radiology
                        Case "Referrals"
                            nType = DiseaseManagement.TemplateCategoryID.Referrals
                        Case "Rx"
                            nType = DiseaseManagement.TemplateCategoryID.Rx
                        Case "Guidelines"
                            nType = DiseaseManagement.TemplateCategoryID.Guidelines
                    End Select
                    If mynode.TemplateResult Is Nothing Then
                        ''Delete the Exisiting record if present
                        oclsDM_PatientSpecific.DeleteTemplate(myCriteria.Key, mynode.Key, m_PatientID)
                        'Changed by Shweta 20100117
                        'Against the bug id:5350 
                        ' oclsDM_PatientSpecific.Save_Trigger(0, m_PatientID, myCriteria.Key, mynode.Key, mynode.DMTemplateName, myCriteria.Name, mynode.DMTemplate, Format(Now, "MM/dd/yyyy"), "", frmDM_Template.Type.Save, nType, , , False, , , False, False, mynode.Dosage)
                        oclsDM_PatientSpecific.Save_Trigger(0, m_PatientID, myCriteria.Key, mynode.Key, mynode.DMTemplateName, myCriteria.Name, mynode.DMTemplate, Format(Now, "MM/dd/yyyy"), "", frmDM_Template.Type.Save, nType, mynode, , , False, , , False, False, mynode.Dosage)
                        'End 
                    Else
                        ''Get the Trigger details as object and validate for reason field
                        Dim oTrigger As TriggerDetails = CType(mynode.TemplateResult, TriggerDetails)
                        If Not oTrigger Is Nothing Then
                            If oTrigger.Recurring Then
                                ''Delete the Exisiting record if present
                                oclsDM_PatientSpecific.DeleteTemplate(myCriteria.Key, mynode.Key, m_PatientID)
                                Dim TrnsId As Int64
                                'Changed by Shweta 20100117
                                'Against the bug id:5350 
                                'TrnsId = oclsDM_PatientSpecific.Save_Trigger(0, m_PatientID, myCriteria.Key, mynode.Key, mynode.DMTemplateName, myCriteria.Name, mynode.DMTemplate, Format(Now, "MM/dd/yyyy"), "", frmDM_Template.Type.Save, nType, oTrigger.DueType, oTrigger.DueValue, False, oTrigger.Reason, oTrigger.Notes, False, True, mynode.Dosage)
                                TrnsId = oclsDM_PatientSpecific.Save_Trigger(0, m_PatientID, myCriteria.Key, mynode.Key, mynode.DMTemplateName, myCriteria.Name, mynode.DMTemplate, Format(Now, "MM/dd/yyyy"), "", frmDM_Template.Type.Save, nType, mynode, oTrigger.DueType, oTrigger.DueValue, False, oTrigger.Reason, oTrigger.Notes, False, True, mynode.Dosage)
                                'End 
                                ''Here u need to insert the recurring value details
                                If TrnsId <> 0 Then
                                    oclsDM_PatientSpecific.Save_TemplateDetail(TrnsId, oTrigger.StartDate, oTrigger.EndDate, oTrigger.DurationType, oTrigger.DurationPeriod)
                                End If
                            Else
                                ''Delete the Exisiting record if present
                                oclsDM_PatientSpecific.DeleteTemplate(myCriteria.Key, mynode.Key, m_PatientID)

                                'oclsDM_PatientSpecific.Save_Trigger(0, m_PatientID, myCriteria.Key, mynode.Key, mynode.DMTemplateName, myCriteria.Text, mynode.DMTemplate, Format(Now, "MM/dd/yyyy"), "", frmDM_Template.Type.Save, nType, oTrigger.DueType, oTrigger.DueValue, False, oTrigger.Reason, oTrigger.Notes, False, False)
                                If Not IsNothing(mynode.DMTemplate) Then
                                    'Changed by Shweta 20100117
                                    'Against the bug id:5350 
                                    'oclsDM_PatientSpecific.Save_Trigger(0, m_PatientID, myCriteria.Key, mynode.Tag, mynode.DMTemplateName, myCriteria.Name, mynode.DMTemplate, Format(Now, "MM/dd/yyyy"), "", frmDM_Template.Type.Save, nType, oTrigger.DueType, oTrigger.DueValue, False, oTrigger.Reason, oTrigger.Notes, False, False, mynode.Dosage)
                                    oclsDM_PatientSpecific.Save_Trigger(0, m_PatientID, myCriteria.Key, mynode.Tag, mynode.DMTemplateName, myCriteria.Name, mynode.DMTemplate, Format(Now, "MM/dd/yyyy"), "", frmDM_Template.Type.Save, nType, mynode, oTrigger.DueType, oTrigger.DueValue, False, oTrigger.Reason, oTrigger.Notes, False, False, mynode.Dosage)
                                    'end
                                Else
                                    'Changed by Shweta 20100117
                                    'Against the bug id:5350 
                                    'oclsDM_PatientSpecific.Save_Trigger(0, m_PatientID, myCriteria.Key, mynode.Tag, mynode.DMTemplateName, myCriteria.Name, Nothing, Format(Now, "MM/dd/yyyy"), "", frmDM_Template.Type.Save, nType, oTrigger.DueType, oTrigger.DueValue, False, oTrigger.Reason, oTrigger.Notes, False, False, mynode.Dosage)
                                    oclsDM_PatientSpecific.Save_Trigger(0, m_PatientID, myCriteria.Key, mynode.Tag, mynode.DMTemplateName, myCriteria.Name, Nothing, Format(Now, "MM/dd/yyyy"), "", frmDM_Template.Type.Save, nType, mynode, oTrigger.DueType, oTrigger.DueValue, False, oTrigger.Reason, oTrigger.Notes, False, False, mynode.Dosage)
                                    'End
                                End If
                            End If
                        End If
                    End If
                Next
            Next
        Next
    End Sub

    Private Function ValidateTrigger(ByVal _CriteriaID As Int64, ByVal _TriggerId As Int64, ByVal nType As DiseaseManagement.TemplateCategoryID) As Boolean

        For Each myCriteria As myTreeNode In trvHealthPlan.Nodes
            ''loop through each Parent node(i.e. labs, Orders, Rx, Guidelines, Referrals) in Criteria node
            If myCriteria.Key = _CriteriaID Then
                For Each parentNode As myTreeNode In myCriteria.Nodes
                    ''loop through each child node   i.e Trigger item
                    For Each mynode As myTreeNode In parentNode.Nodes
                        If mynode.Key = _TriggerId Then
                            ''If not selected check whether Reason for not giving is documented or not
                            If mynode.TemplateResult Is Nothing Then
                                ''Reson Not documented, so prompt the user to document the reason
                                MessageBox.Show("This guideline is recommended. Please provide reason for overriding", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)
                                ''show the user with required info to document the reason against the node
                                trvHealthPlan.SelectedNode = mynode
                                Dim objTrvArgs As New TreeNodeMouseClickEventArgs(trvHealthPlan.SelectedNode, Windows.Forms.MouseButtons.Left, 2, trvHealthPlan.SelectedNode.Bounds.X, trvHealthPlan.SelectedNode.Bounds.Y)
                                trvHealthPlan_NodeMouseDoubleClick(objSender, objTrvArgs)
                                Return False
                            Else
                                ''Get the Trigger deatils as object and validate for reason field
                                Dim oTrigger As TriggerDetails = CType(mynode.TemplateResult, TriggerDetails)
                                If oTrigger.Reason = "" Then
                                    ''Reson Not documented, so prompt the user to document the reason
                                    MessageBox.Show("This guideline is recommended. Please provide reason for overriding", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)
                                    ''show the user with required info to document the reason against the node
                                    trvHealthPlan.SelectedNode = mynode
                                    Dim objTrvArgs As New TreeNodeMouseClickEventArgs(trvHealthPlan.SelectedNode, Windows.Forms.MouseButtons.Left, 2, trvHealthPlan.SelectedNode.Bounds.X, trvHealthPlan.SelectedNode.Bounds.Y)
                                    trvHealthPlan_NodeMouseDoubleClick(objSender, objTrvArgs)
                                    Return False
                                Else
                                    ''Save this record info in DB as not given status and 

                                End If
                            End If

                        End If
                    Next
                Next
            End If
        Next
        Return True
    End Function

    Private Sub SaveDueTriggers()
        For Each myCriteria As myTreeNode In trvHealthPlan.Nodes
            ''loop through each Parent node(i.e. labs, Orders, Rx, Guidelines, Referrals) in Criteria node

            For Each parentNode As myTreeNode In myCriteria.Nodes
                ''loop through each child node   i.e Trigger item
                For Each mynode As myTreeNode In parentNode.Nodes
                    If mynode.Checked = False Then
                        ''If not selected only
                        Dim nType As DiseaseManagement.TemplateCategoryID
                        Select Case parentNode.Text
                            Case "Labs"
                                nType = DiseaseManagement.TemplateCategoryID.Labs
                            Case "Orders"
                                nType = DiseaseManagement.TemplateCategoryID.Radiology
                            Case "Referrals"
                                nType = DiseaseManagement.TemplateCategoryID.Referrals
                            Case "Rx"
                                nType = DiseaseManagement.TemplateCategoryID.Rx
                            Case "Guidelines"
                                nType = DiseaseManagement.TemplateCategoryID.Guidelines
                            Case "IM"
                                nType = DiseaseManagement.TemplateCategoryID.IM

                        End Select
                        ''Get the Trigger details as object and validate for reason field
                        Dim oTrigger As TriggerDetails = CType(mynode.TemplateResult, TriggerDetails)
                        If Not oTrigger Is Nothing Then
                            If oTrigger.Recurring Then
                                ''Delete the Exisiting record if present
                                oclsDM_PatientSpecific.DeleteTemplate(myCriteria.Key, mynode.Key, m_PatientID)
                                Dim TrnsId As Int64
                                'Changed by Shweta 20100117
                                'Against the bug id:5350 
                                'TrnsId = oclsDM_PatientSpecific.Save_Trigger(0, m_PatientID, myCriteria.Key, mynode.Key, mynode.DMTemplateName, myCriteria.Name, mynode.DMTemplate, Format(Now, "MM/dd/yyyy"), "", frmDM_Template.Type.Save, nType, oTrigger.DueType, oTrigger.DueValue, False, oTrigger.Reason, oTrigger.Notes, False, True, mynode.Dosage)
                                TrnsId = oclsDM_PatientSpecific.Save_Trigger(0, m_PatientID, myCriteria.Key, mynode.Key, mynode.DMTemplateName, myCriteria.Name, mynode.DMTemplate, Format(Now, "MM/dd/yyyy"), "", frmDM_Template.Type.Save, nType, mynode, oTrigger.DueType, oTrigger.DueValue, False, oTrigger.Reason, oTrigger.Notes, False, True, mynode.Dosage)
                                'End
                                ''Here u need to insert the recurring value details
                                If TrnsId <> 0 Then
                                    oclsDM_PatientSpecific.Save_TemplateDetail(TrnsId, oTrigger.StartDate, oTrigger.EndDate, oTrigger.DurationType, oTrigger.DurationPeriod)
                                End If
                            Else
                                ''Delete the Exisiting record if present

                                oclsDM_PatientSpecific.DeleteTemplate(myCriteria.Key, mynode.Key, m_PatientID)
                                'Changed by Shweta 20100117
                                'Against the bug id:5350 
                                'oclsDM_PatientSpecific.Save_Trigger(0, m_PatientID, myCriteria.Key, mynode.Key, mynode.DMTemplateName, myCriteria.Name, mynode.DMTemplate, Format(Now, "MM/dd/yyyy"), "", frmDM_Template.Type.Save, nType, oTrigger.DueType, oTrigger.DueValue, False, oTrigger.Reason, oTrigger.Notes, False, False, mynode.Dosage)
                                oclsDM_PatientSpecific.Save_Trigger(0, m_PatientID, myCriteria.Key, mynode.Key, mynode.DMTemplateName, myCriteria.Name, mynode.DMTemplate, Format(Now, "MM/dd/yyyy"), "", frmDM_Template.Type.Save, nType, mynode, oTrigger.DueType, oTrigger.DueValue, False, oTrigger.Reason, oTrigger.Notes, False, False, mynode.Dosage)
                                'End
                            End If
                        End If
                    End If
                Next
            Next


        Next
    End Sub

    Private Sub AddAssociates(ByVal mynode As myTreeNode, ByVal targetNode As myTreeNode)

        ''Map all the node values to the associated node
        Dim Associatenode As myTreeNode
        Associatenode = mynode.Clone
        Associatenode.Key = mynode.Key
        Associatenode.Text = mynode.Text
        Associatenode.Tag = 0
        Associatenode.NodeName = mynode.DMTemplateName
        Associatenode.DMTemplateName = mynode.DMTemplateName
        'sarika DM Denormalization
        If targetNode.Text = "Guidelines" Then
            Associatenode.DMTemplateName = mynode.DMTemplateName
            Associatenode.DMTemplate = mynode.DMTemplate

        End If

        Associatenode.DrugName = mynode.DrugName
        Associatenode.Dosage = mynode.Dosage
        Associatenode.DrugForm = mynode.DrugForm
        Associatenode.Duration = mynode.Duration
        Associatenode.mpid = mynode.mpid
        Associatenode.DrugQtyQualifier = mynode.DrugQtyQualifier
        Associatenode.NDCCode = mynode.NDCCode
        Associatenode.Frequency = mynode.Frequency
        Associatenode.Route = mynode.Route
        Associatenode.IsNarcotics = mynode.IsNarcotics
        '--

        Associatenode.ImageIndex = 0
        Associatenode.SelectedImageIndex = 0
        Associatenode.TemplateResult = Nothing
        targetNode.Nodes.Add(Associatenode)
        trvHealthPlan.ExpandAll()
    End Sub


    Private Sub AddCriteriaNodes(ByVal _TransId As Int64, ByVal _TriggerId As Int64, ByVal _TriggerName As String, ByVal _Recurring As Boolean, ByVal rootNode As myTreeNode, ByVal strType As String, ByVal imgIndex As Int32)
        Try
            Dim blnExist As Boolean = False
            Dim parentNode As myTreeNode = Nothing
            Dim strDueDesc As String = ""
            ''Loop through Criteria node to check for the parent node of 'strtype' is available
            For Each TargetNode As myTreeNode In rootNode.Nodes
                If TargetNode.Text = strType Then
                    parentNode = TargetNode
                    blnExist = True
                End If
            Next
            ''if not exists then add the node to the root node as parent node
            If Not blnExist Then
                parentNode = New myTreeNode
                parentNode.Text = strType
                parentNode.ImageIndex = imgIndex
                parentNode.SelectedImageIndex = imgIndex
                rootNode.Nodes.Add(parentNode)
            End If

            ''Map all the node values to the associated node
            Dim Associatenode As New myTreeNode
            Associatenode.Key = _TriggerId

            Associatenode.Tag = _TransId
            Associatenode.NodeName = _TriggerName
            ''Get the Trigger due details and map it to Triggerdetails object 
            Dim oDetails As DataTable = oDM.FindDueTriggerDetails(_TransId, _Recurring)
            If Not oDetails Is Nothing Then
                If oDetails.Rows.Count > 0 Then
                    Dim objTrigger As New TriggerDetails
                    objTrigger.TransId = _TransId
                    objTrigger.TriggerId = _TriggerId
                    'If Not IsDBNull(CType(oDetails.Rows(0)("DM_nCriteriaID"), String)) Then
                    '    objTrigger.CriteriaId = CType(oDetails.Rows(0)("DM_nCriteriaID"), String)
                    'End If

                    If Not IsDBNull(oDetails.Rows(0)("DM_DueType")) Then
                        objTrigger.DueType = CType(oDetails.Rows(0)("DM_DueType"), String)
                    End If

                    If Not IsDBNull(oDetails.Rows(0)("DM_DueValue")) Then
                        objTrigger.DueValue = CType(oDetails.Rows(0)("DM_DueValue"), String)
                        If oDetails.Rows(0)("DM_DueValue") <> "" Then
                            strDueDesc = " " & "-" & " " & " Due on " & objTrigger.DueType & " " & objTrigger.DueValue
                        End If
                    End If

                    If Not IsDBNull(oDetails.Rows(0)("DM_bIsRecurring")) Then
                        objTrigger.Recurring = CType(oDetails.Rows(0)("DM_bIsRecurring"), Boolean)
                    End If

                    If Not IsDBNull(oDetails.Rows(0)("DM_sReason")) Then
                        objTrigger.Reason = CType(oDetails.Rows(0)("DM_sReason"), String)
                        If oDetails.Rows(0)("DM_sReason") <> "" Then
                            strDueDesc = strDueDesc & " " & "-" & " " & "Reason :" & objTrigger.Reason
                        End If
                    End If

                    If Not IsDBNull(oDetails.Rows(0)("DM_sNotes")) Then
                        objTrigger.Notes = CType(oDetails.Rows(0)("DM_sNotes"), String)
                        If oDetails.Rows(0)("DM_sNotes") <> "" Then
                            strDueDesc = strDueDesc & " " & "-" & " " & "Notes :" & objTrigger.Notes
                        End If
                    End If

                    If _Recurring Then
                        If Not IsDBNull(oDetails.Rows(0)("DM_dtStartDate")) Then
                            objTrigger.StartDate = CType(oDetails.Rows(0)("DM_dtStartDate"), DateTime)
                        End If
                        If Not IsDBNull(oDetails.Rows(0)("DM_dtEndDate")) Then
                            objTrigger.EndDate = CType(oDetails.Rows(0)("DM_dtEndDate"), DateTime)
                        End If
                        If Not IsDBNull(oDetails.Rows(0)("DM_nDurationType")) Then
                            objTrigger.DurationType = CType(oDetails.Rows(0)("DM_nDurationType"), String)
                        End If
                        If Not IsDBNull(oDetails.Rows(0)("DM_nDurationPeriod")) Then
                            objTrigger.DurationPeriod = CType(oDetails.Rows(0)("DM_nDurationPeriod"), Int32)
                            If objTrigger.DurationType = chkOnEveryCheckIn.Text Then
                                objTrigger.OnEveryCheckIn = True
                                strDueDesc = strDueDesc & " - "
                            Else
                                objTrigger.OnEveryCheckIn = False
                                strDueDesc = strDueDesc & " " & "-" & " " & "Recur every " & objTrigger.DurationPeriod
                            End If
                            strDueDesc = strDueDesc & " " & objTrigger.DurationType
                        End If

                    End If

                    ''Save it as TemplateResult object for the treenode
                    Associatenode.TemplateResult = objTrigger
                Else
                    ''if due detail are not available set to null
                    Associatenode.TemplateResult = Nothing
                End If
            Else

                ''if due detail are not available set to null
                Associatenode.TemplateResult = Nothing
            End If

            Associatenode.Text = _TriggerName & strDueDesc
            Associatenode.ImageIndex = 0
            Associatenode.SelectedImageIndex = 0
            ''Add the respective node to the parent node
            parentNode.Nodes.Add(Associatenode)
            trvHealthPlan.ExpandAll()

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub



    ''' <summary>
    ''' Load the Patient Strip 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub loadPatientStrip()
        ''Show the patient details based on id passed
        _PatientStrip.ShowDetail(m_PatientID, gloUC_PatientStrip.enumFormName.HealthPlan)
        _PatientStrip.Dock = DockStyle.Top
        ''Add the patient Strip control to the panel
        If pnlMain.Controls.Contains(_PatientStrip) = False Then
            Me.pnlMain.Controls.Add(_PatientStrip)
            'pnlToolStrip.SendToBack()
            _PatientStrip.Padding = New Padding(0, 0, 3, 0)
        End If
    End Sub

    'Private Sub trvPatients_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs)
    '    Try
    '        ''Update the Patient Id with the selected Patient for further reference
    '        If Not IsNothing(trvPatients.SelectedNode) Then
    '            m_PatientID = trvPatients.SelectedNode.Tag
    '        End If
    '        loadPatientStrip()

    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub
    ''' <summary>
    ''' Dhruv 20101302
    ''' For checking the nodes if it is first or the second
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub trvHealthPlan_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvHealthPlan.AfterCheck
        If bChildTrigger = True Then
            CheckAllChildren(e.Node, e.Node.Checked)
        End If
        If bParentTrigger = True Then
            CheckAllParents(e.Node, e.Node.Checked)
        End If

    End Sub

    'Private Sub trvHealthPlan_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvHealthPlan.MouseDown

    '    'Me.TopMost = True
    '    'Try
    '    '    trvHealthPlan.ContextMenuStrip = Nothing
    '    '    If e.Button = MouseButtons.Right Then
    '    '        Dim trvnode As myTreeNode
    '    '        trvnode = CType(trvHealthPlan.GetNodeAt(e.X, e.Y), myTreeNode)

    '    '        trvHealthPlan.ContextMenuStrip = cntHealthPlan
    '    '        cntHealthPlan.Items.Clear()

    '    '        'Dim oChildMenu As ToolStripMenuItem
    '    '        oChildMenu = New ToolStripMenuItem()
    '    '        oChildMenu.Text = "New Patient Criteria"
    '    '        oChildMenu.Name = "New"
    '    '        oChildMenu.Image = ImageList1.Images(12)
    '    '        AddHandler oChildMenu.Click, AddressOf AddNewCriteria
    '    '        cntHealthPlan.Items.Add(oChildMenu)
    '    '        oChildMenu = Nothing

    '    '        If Not IsNothing(trvnode) Then
    '    '            If IsNothing(trvnode.Parent) = False Then
    '    '                If (trvnode.Parent.Text = "Guidelines") Then
    '    '                    _TemplateID = trvnode.Key
    '    '                    cntHealthPlan.Items.Clear()

    '    '                    oChildMenu = New ToolStripMenuItem()
    '    '                    oChildMenu.Text = "Edit Guideline"
    '    '                    oChildMenu.Name = "Edit Guideline"
    '    '                    oChildMenu.Image = ImageList1.Images(15)
    '    '                    AddHandler oChildMenu.Click, AddressOf mnu_EditGuideline_Click
    '    '                    cntHealthPlan.Items.Add(oChildMenu)
    '    '                    oChildMenu = Nothing
    '    '                    trvnode.ContextMenuStrip = cntHealthPlan
    '    '                End If
    '    '            End If
    '    '        End If
    '    '    End If

    '    'Catch ex As Exception
    '    '    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    'End Try

    '    'Me.TopMost = False

    'End Sub

    'Private Sub mnu_EditGuideline_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_EditGuideline.Click
    '    Dim _parent As String = ""

    '    If Not IsNothing(trvHealthPlan.SelectedNode) Then
    '        If Not IsNothing(trvHealthPlan.SelectedNode.Parent) Then
    '            _parent = trvHealthPlan.SelectedNode.Parent.Text
    '        End If

    '    End If
    '    If _parent = "Guidelines" Then
    '        UpdateTemplate(CType(trvHealthPlan.SelectedNode, myTreeNode))
    '    Else
    '        If _TemplateID <> 0 Then
    '            Dim frm As New frmTemplateGallery(_TemplateID)

    '            frm.ShowDialog(Me)
    '            _TemplateID = 0
    '        End If
    '    End If

    'End Sub

    'sarika DM Denormlization
    Private Sub UpdateTemplate(ByVal mySelectedNode As myTreeNode)

        ' Dim objTemplateGallery As New clsTemplateGallery
        Dim objfrmTemplateGallery As frmTemplateGallery
        Try


            '   blnModify = True



            ''''''''''''''''''
            objfrmTemplateGallery = New frmTemplateGallery(True)
            '  Me.DMSelectedNode = mySelectedNode
            With objfrmTemplateGallery
                .DMSelectedNode = mySelectedNode
                .Text = "Modify Template"
                '.mycaller = Me
                .MdiParent = Me.ParentForm
                '.Parent = Me
                'CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
                'CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False

                '.WindowState = FormWindowState.Maximized
                '.BringToFront()
                '.ShowDialog(Me)

                .Show()
                .WindowState = FormWindowState.Maximized
                .BringToFront()


                mySelectedNode = .DMSelectedNode
            End With

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Template Gallery", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
            CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True

            'Finally
            '  objfrmTemplateGallery = Nothing
        End Try

    End Sub
    '---


    'Private Sub trvHealthPlan_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvHealthPlan.NodeMouseClick
    '    Me.TopMost = True
    '    If Not e.Node Is Nothing Then
    '        trvHealthPlan.SelectedNode = e.Node
    '        If e.Button = Windows.Forms.MouseButtons.Right Then
    '            If trvHealthPlan.SelectedNode.Level = 0 Then

    '                trvHealthPlan.ContextMenuStrip = cntHealthPlan
    '                cntHealthPlan.Items.Clear()

    '                Dim oChildMenu As ToolStripMenuItem

    '                oChildMenu = New ToolStripMenuItem()
    '                oChildMenu.Text = "New Patient Criteria"
    '                oChildMenu.Name = "New"
    '                oChildMenu.Image = ImageList1.Images(12)
    '                AddHandler oChildMenu.Click, AddressOf AddNewCriteria
    '                cntHealthPlan.Items.Add(oChildMenu)

    '                oChildMenu = Nothing

    '                If CType(trvHealthPlan.SelectedNode, myTreeNode).IsFinished = False Then
    '                    oChildMenu = New ToolStripMenuItem()
    '                    oChildMenu.Text = "Delete Health Plan"
    '                    oChildMenu.Name = "Delete"
    '                    oChildMenu.Image = ImageList1.Images(13)

    '                    AddHandler oChildMenu.Click, AddressOf DeleteCriteria
    '                    cntHealthPlan.Items.Add(oChildMenu)
    '                    oChildMenu = Nothing
    '                End If

    '                '' Sudhir 20090306
    '                If oDM.IsPatientSpecificCriteria(CType(trvHealthPlan.SelectedNode, myTreeNode).Key) = True And CType(trvHealthPlan.SelectedNode, myTreeNode).IsFinished = False Then
    '                    oChildMenu = New ToolStripMenuItem()
    '                    oChildMenu.Text = "Modify Health Plan"
    '                    oChildMenu.Name = "Modify"
    '                    oChildMenu.Image = ImageList1.Images(14)
    '                    AddHandler oChildMenu.Click, AddressOf ModifyCriteria
    '                    cntHealthPlan.Items.Add(oChildMenu)
    '                    oChildMenu = Nothing
    '                End If

    '                ''
    '            Else
    '                'trvHealthPlan.ContextMenuStrip = Nothing
    '            End If
    '        Else
    '            trvHealthPlan.ContextMenuStrip = Nothing
    '        End If
    '    End If
    '    Me.TopMost = False
    'End Sub


    Public Sub SetMenus(ByVal sender As Object, ByVal e As EventArgs)

        Try
            If trvHealthPlan.SelectedNode.Level = 0 Then

                Dim mynode As myTreeNode
                mynode = CType(trvHealthPlan.SelectedNode, myTreeNode)
                If Not IsNothing(mynode) Then
                    Dim _CriteriaId As Int64 = mynode.Key
                    Dim dtData As DataTable = oDM.GetSpecifiHealthPlan(_CriteriaId)
                    If Not dtData Is Nothing Then


                        mynode.Remove()
                        Dim drNewRow As DataRow = dtOtherHeathPlans.NewRow
                        drNewRow(0) = dtData.Rows(0)(0)
                        drNewRow(1) = dtData.Rows(0)(1)
                        drNewRow(2) = dtData.Rows(0)(2)
                        drNewRow(3) = dtData.Rows(0)(3)
                        dtOtherHeathPlans.Rows.Add(drNewRow)
                        dtOtherHeathPlans.AcceptChanges()
                        'BindOtherHealthPlans()
                    End If
                    'Try
                    '    If (IsNothing(trvHealthPlan.ContextMenu) = False) Then
                    '        trvHealthPlan.ContextMenu.Dispose()
                    '        trvHealthPlan.ContextMenu = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    trvHealthPlan.ContextMenu = Nothing

                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    '' Sudhir 20090306
    Private Sub ModifyCriteria(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If trvHealthPlan.SelectedNode.Level = 0 Then
                Dim mynode As myTreeNode
                mynode = CType(trvHealthPlan.SelectedNode, myTreeNode)
                If Not IsNothing(mynode) Then
                    Dim _CriteriaId As Int64 = mynode.Key
                    Dim ofrm As New frmDM_PatientCriteria(_CriteriaId, m_PatientID)
                    ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                    ofrm.Dispose()
                    'Try
                    '    If (IsNothing(trvHealthPlan.ContextMenu) = False) Then
                    '        trvHealthPlan.ContextMenu.Dispose()
                    '        trvHealthPlan.ContextMenu = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    trvHealthPlan.ContextMenu = Nothing
                    _IsModifyCriteria = True
                    _SelectedCriteriaID = _CriteriaId
                    '' REFRESH HEALTH PLAN ''
                    GetGeneralCriteria()
                    BindHealthPlan()
                    'selectGender()
                    GetOtherHeathPlans()
                    ' BindOtherHealthPlans()
                    ''

                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DeleteCriteria(ByVal sender As Object, ByVal e As EventArgs)
        Try
            _IsModifyCriteria = False
            If trvHealthPlan.SelectedNode.Level = 0 Then
                Dim mynode As myTreeNode
                mynode = CType(trvHealthPlan.SelectedNode, myTreeNode)
                If Not IsNothing(mynode) Then
                    Dim _CriteriaId As Int64 = mynode.Key

                    If oDM.IsPatientSpecificCriteria(_CriteriaId) = True Then
                        oDM.Delete(_CriteriaId, mynode.Text)
                    Else
                        Dim dtData As DataTable = oDM.GetSpecifiHealthPlan(_CriteriaId)
                        If Not dtData Is Nothing Then
                            mynode.Remove()
                            Dim drNewRow As DataRow = dtOtherHeathPlans.NewRow
                            drNewRow(0) = dtData.Rows(0)(0)
                            drNewRow(1) = dtData.Rows(0)(1)
                            drNewRow(2) = dtData.Rows(0)(2)
                            drNewRow(3) = dtData.Rows(0)(3)
                            dtOtherHeathPlans.Rows.Add(drNewRow)
                            dtOtherHeathPlans.AcceptChanges()
                            'BindOtherHealthPlans()
                        End If
                    End If
                    'Try
                    '    If (IsNothing(trvHealthPlan.ContextMenu) = False) Then
                    '        trvHealthPlan.ContextMenu.Dispose()
                    '        trvHealthPlan.ContextMenu = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    trvHealthPlan.ContextMenu = Nothing

                    '' REFRESH HEALTH PLAN ''
                    'GetGeneralCriteria()
                    'BindHealthPlan()
                    'selectGender()
                    ' GetOtherHeathPlans()
                    'BindOtherHealthPlans()
                    ''
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''

    Private Sub trvHealthPlan_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvHealthPlan.NodeMouseDoubleClick
        Try
            If e.Node Is Nothing Then
                Exit Sub
            End If
            ''select the node that was double clicked
            trvHealthPlan.SelectedNode = e.Node
            If Not IsNothing(trvHealthPlan.SelectedNode) Then
                ''Validate the selected node for field node but should not be Parent or table node
                'If trvHealthPlan.SelectedNode Is trvHealthPlan.Nodes.Item(0) Then
                '    Exit Sub
                ''validate the node is parent node
                If trvHealthPlan.SelectedNode.Parent Is trvHealthPlan.Nodes.Item(0) Then
                    Exit Sub

                ElseIf trvHealthPlan.SelectedNode.Nodes.Count = 0 Then
                    ''Reset the panels visibility and controls
                    ResetEditPanel()
                    'Splitter2.Visible = True
                    pnlEdit.Visible = True
                    Dim thisNode As myTreeNode = CType(trvHealthPlan.SelectedNode, myTreeNode)
                    lblOrder.Text = thisNode.NodeName
                    ''check for trigger due details available in the node
                    If thisNode.TemplateResult Is Nothing Then
                        ''if not available then show default values
                        If (cmbDueType.Items.Count > 0) Then
                            cmbDueType.SelectedIndex = 0
                        End If

                        SetDueOnControls()
                    Else
                        ''Bind the Trigger details to Due controls
                        Dim objTriger As TriggerDetails
                        objTriger = CType(thisNode.TemplateResult, TriggerDetails)
                        ShowTriggerDetails(objTriger)
                        SetDueOnControls()
                        objTriger = Nothing
                    End If
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub cmbDueType_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDueType.SelectionChangeCommitted
        SetDueOnControls()
    End Sub
    ''' <summary>
    ''' Set the controls properties based on the selection of Due type
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDueOnControls()
        If cmbDueType.Text = "Date" Then
            dtDueDate.Visible = True
            chckRecurring.Visible = True
            cmbOperator.Visible = False
            cmbValue.Visible = False
        ElseIf cmbDueType.Text = "Age" Then
            dtDueDate.Visible = False
            chckRecurring.Visible = False
            cmbOperator.Visible = True
            cmbValue.Visible = True
            pnlRecurring.Visible = False
        End If
    End Sub

    Private Sub chckRecurring_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chckRecurring.CheckStateChanged

        ''Set the Recurring panel visibilty
        If chckRecurring.Checked Then
            pnlRecurring.Visible = True
            cmbDurationType.SelectedIndex = 0
            cmbPeriod.SelectedIndex = 0
            dtStartDate.ResetText()
            dtEndDate.ResetText()
            chkOnEveryCheckIn.Checked = False
        Else
            pnlRecurring.Visible = False
        End If
        dtDueDate.Enabled = Not (chckRecurring.Checked)

    End Sub


    Private Sub chkOnEveryCheckIn_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkOnEveryCheckIn.CheckStateChanged
        If chkOnEveryCheckIn.Checked = True Then
            cmbPeriod.Enabled = False
            cmbDurationType.Enabled = False
        Else
            cmbPeriod.Enabled = True
            cmbDurationType.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Reset all the Trigger due details and controls
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ResetEditPanel()
        cmbOperator.SelectedIndex = 0
        cmbValue.SelectedIndex = 0
        cmbDueType.SelectedIndex = 0
        cmbDurationType.SelectedIndex = 0
        cmbPeriod.SelectedIndex = 0
        chckRecurring.Checked = False
        dtStartDate.ResetText()
        dtEndDate.ResetText()
        dtDueDate.ResetText()
        txtReason.Text = String.Empty
        txtNotes.Text = String.Empty
        dtDueDate.Visible = True
        chckRecurring.Visible = True
        cmbOperator.Visible = False
        cmbValue.Visible = False
        pnlRecurring.Visible = False
        chkOnEveryCheckIn.Checked = False
    End Sub


    ''' <summary>
    ''' Load the Dropdonlists with numbers for age selection and period selection
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillDropdowns()
        Dim oDMCriteria As New gloStream.DiseaseManagement.Common.Criteria

        Dim oCollection As New Collection
        oCollection = oDMCriteria.Age
        nPatAge = oDM.GetPatientAgeinYrs(strPatientDOB)
        cmbValue.Items.Clear()
        cmbPeriod.Items.Clear()
        ''To fill the Values with 1 to 125 genral age limit
        For i As Int16 = 1 To oCollection.Count
            cmbPeriod.Items.Add(oCollection(i))
            ''Only add the Values that starts from APtient age
            'If oCollection(i) >= nPatAge Then
            cmbValue.Items.Add(oCollection(i))
            'End If
        Next

        oCollection = Nothing
        oDMCriteria.Dispose()
        oDMCriteria = Nothing
    End Sub


    Private Sub FillOperators()
        Dim dtOperator As New DataTable
        Dim myrow As DataRow
        dtOperator.Columns.Add(New DataColumn("OperatorID", GetType(String)))
        dtOperator.Columns.Add(New DataColumn("Operator", GetType(String)))

        myrow = dtOperator.NewRow()
        myrow(0) = "="
        myrow(1) = "Equals to"
        dtOperator.Rows.Add(myrow)

        myrow = dtOperator.NewRow()
        myrow(0) = ">"
        myrow(1) = "Greater than"
        dtOperator.Rows.Add(myrow)

        myrow = dtOperator.NewRow()
        myrow(0) = ">="
        myrow(1) = "Greater than or equal to"
        dtOperator.Rows.Add(myrow)

        cmbOperator.DataSource = dtOperator
        cmbOperator.DisplayMember = dtOperator.Columns("Operator").ToString()
        cmbOperator.ValueMember = dtOperator.Columns("OperatorID").ToString()

    End Sub


    Private Sub OverrideDetails()
        Try
            _Messageshown = False
            If (txtReason.Text.Trim = "") Then

                MessageBox.Show("Please enter reason.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtReason.Focus()
                _Messageshown = True
                Exit Sub
            End If
            ''Get the selected treenode
            Dim mynode As myTreeNode = CType(trvHealthPlan.SelectedNode, myTreeNode)
            ''''If The Node is open For Edit means for second time to Enter Due 
            Dim strNodeName() As String = Split(mynode.Text, "-", 2) ''''The array of Node
            Dim strDueDesc As String = ""
            If strNodeName.Length = 2 Then '''' Identify Node has already Due 
                mynode.Text = "" ''''Make Node emty
                mynode.Text = strNodeName.GetValue(0) '''' Node Name without Due Parameter
                strDueDesc = strNodeName.GetValue(0) '''' Set Node Name to string
            End If

            ''Initialize the Trigger details objec for saving it in treenode object as template result
            Dim oDMCr As New TriggerDetails

            oDMCr.TransId = mynode.Key ''Trnsaction id
            oDMCr.TriggerId = mynode.Tag ''Trigger Id

            oDMCr.DueType = cmbDueType.Text
            If cmbDueType.Text = "Date" Then
                oDMCr.DueValue = dtDueDate.Value.ToShortDateString ''due date
                oDMCr.Recurring = chckRecurring.Checked '' Is recurring
                If chckRecurring.Checked Then
                    strDueDesc = strDueDesc & " - From " & dtStartDate.Value.ToShortDateString & " To " & dtEndDate.Value.ToShortDateString
                Else
                    strDueDesc = strDueDesc & " - Due on Date " & dtDueDate.Value.ToShortDateString
                End If
            Else
                oDMCr.DueValue = cmbOperator.SelectedValue & cmbValue.Text '' Operator with value
                strDueDesc = strDueDesc & " - Due on Age " & cmbOperator.SelectedValue & " " & cmbValue.Text
            End If
            oDMCr.Reason = txtReason.Text.Trim '' Reason
            If txtReason.Text.Trim <> "" Then
                strDueDesc = strDueDesc & " - Reason :" & txtReason.Text.Trim
            End If
            oDMCr.Notes = txtNotes.Text.Trim '' Notes
            If txtNotes.Text.Trim <> "" Then
                strDueDesc = strDueDesc & " - Notes :" & txtNotes.Text.Trim
            End If

            ''if Recurring then set the recurring values 
            If chckRecurring.Checked Then
                oDMCr.StartDate = dtStartDate.Value
                oDMCr.EndDate = dtEndDate.Value
                ''SUDHIR 20090311 - ON EVERY CHECK IN 
                oDMCr.OnEveryCheckIn = chkOnEveryCheckIn.Checked
                If chkOnEveryCheckIn.Checked = True Then
                    ''CHECK IN STATE CAN BE IDENTIFY FROM DurationType
                    oDMCr.DurationPeriod = 0
                    oDMCr.DurationType = chkOnEveryCheckIn.Text
                    strDueDesc = strDueDesc & " - On Every Check In "
                Else
                    oDMCr.DurationPeriod = cmbPeriod.Text
                    oDMCr.DurationType = cmbDurationType.Text
                    If cmbPeriod.Text <> "" And cmbDurationType.Text <> "" Then
                        strDueDesc = strDueDesc & " - Recurr every " & cmbPeriod.Text & " " & cmbDurationType.Text
                    End If
                End If

            End If
            ''Set the template result object
            mynode.TemplateResult = oDMCr
            oDMCr = Nothing
            pnlEdit.Visible = False
            'Splitter2.Visible = False

            If strNodeName.Length = 2 Then
                trvHealthPlan.SelectedNode.Text = strDueDesc
            Else
                trvHealthPlan.SelectedNode.Text = mynode.Text & strDueDesc
            End If

            ResetEditPanel()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    Private Sub tlsPatientDM_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsPatientDM.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Save"

                ValidateHealthPlan()
            Case "Close"
                'Commnented by Shweta 20091224
                'Against the bug id:5475
                'SaveInvalidDueTriggers()
                'End commenting 20091224
                ' Me.Close()
                gloWord.WordDialogBoxBackgroundCloser.Close(Me)
            Case "ViewRecommendation"
                Dim gnVisitID As Long
                gnVisitID = GetVisitID(Date.Now, m_PatientID)
                Dim _toSendPatLst As New Collection
                _toSendPatLst.Add(m_PatientID)
                Dim frmDisplayRecommendations As New frmDM_DisplayRecommendations(_toSendPatLst, True, gnVisitID)

                With frmDisplayRecommendations
                    .ShowInTaskbar = False
                    .ShowDialog(IIf(IsNothing(frmDisplayRecommendations.Parent), Me, frmDisplayRecommendations.Parent))
                End With

                'Memory Leak
                If Not IsNothing(frmDisplayRecommendations) Then
                    frmDisplayRecommendations.Dispose()
                    frmDisplayRecommendations = Nothing
                End If

        End Select
    End Sub
    ''' <summary>
    ''' Set the Trigger details to the trigger due controls
    ''' </summary>
    ''' <param name="oTrigger"></param>
    ''' <remarks></remarks>
    Private Sub ShowTriggerDetails(ByVal oTrigger As TriggerDetails)
        cmbDueType.Text = oTrigger.DueType
        If oTrigger.DueType = "Date" Then
            dtDueDate.Value = CType(oTrigger.DueValue, DateTime)
            ''if it is recurring set the Recurring values to the controls
            If oTrigger.Recurring Then
                chckRecurring.Checked = True
                If oTrigger.StartDate.Date <> "#12:00:00 AM#" Then
                    dtStartDate.Value = oTrigger.StartDate
                End If
                If oTrigger.EndDate.Date <> "#12:00:00 AM#" Then
                    dtEndDate.Value = oTrigger.EndDate
                End If

                cmbDurationType.Text = oTrigger.DurationType
                cmbPeriod.Text = oTrigger.DurationPeriod
                If oTrigger.OnEveryCheckIn Then
                    chkOnEveryCheckIn.Checked = True
                End If
                pnlRecurring.Visible = True
            Else
                pnlRecurring.Visible = False
            End If
        Else
            Dim strTemp As String = CType(oTrigger.DueValue, String)
            If strTemp.Contains(">=") Then
                strTemp = strTemp.Remove(0, 2)
                cmbOperator.SelectedIndex = 2
                cmbValue.Text = strTemp
            ElseIf strTemp.Contains(">") Then

                strTemp = strTemp.Remove(0, 1)
                cmbOperator.SelectedIndex = 1
                cmbValue.Text = strTemp
            ElseIf strTemp.Contains("=") Then
                strTemp = strTemp.Remove(0, 1)
                cmbOperator.SelectedIndex = 0
                cmbValue.Text = strTemp

            End If
        End If

        txtReason.Text = oTrigger.Reason
        txtNotes.Text = oTrigger.Notes
        oTrigger = Nothing
    End Sub

    Private Sub tlsDetails_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsDetails.ItemClicked
        Select Case e.ClickedItem.Tag.ToString().ToUpper
            Case "OK".ToUpper
                OverrideDetails()
                ''Sandip Darade  20090910
                ''check for guidelines in one go
                If (_Messageshown = False) Then
                    AskforGuideline()
                End If

            Case "Cancel".ToUpper
                ''Close the Edit panel and reset the trigger due controls
                pnlEdit.Visible = False
                'Splitter2.Visible = False
                ResetEditPanel()
        End Select
    End Sub

    Private Sub GetOtherHeathPlans()
        ' dtOtherHeathPlans = New DataTable
        dtOtherHeathPlans = oDM.GetAllHealthPlans
        If Not dtOtherHeathPlans Is Nothing Then
            For Each dRow As DataRow In dtOtherHeathPlans.Rows
                Dim blndelete As Boolean = False
                For _index As Integer = 1 To m_CriteriaCol.Count
                    If m_CriteriaCol(_index) = dRow("CriteriaID") Then
                        dRow.Delete()
                        blndelete = True
                        Exit For
                    End If
                Next
                If blndelete = False Then
                    For _cnt As Int32 = 0 To oPatTriggers.Rows.Count - 1
                        If oPatTriggers.Rows(_cnt)("DM_nCriteriaID") = dRow("CriteriaID") Then
                            dRow.Delete()
                            Exit For
                        End If
                    Next
                End If
            Next
            dtOtherHeathPlans.AcceptChanges()
        End If
    End Sub
    'Private Sub BindOtherHealthPlans()
    '    If Not dtOtherHeathPlans Is Nothing Then
    '        Dim myNode As TreeNode
    '        trvOtherCriterias.Nodes.Clear()
    '        Dim dvOtherPlans As New DataView(dtOtherHeathPlans)

    '        If rbMale.Checked Then
    '            dvOtherPlans.RowFilter = "[Gender] = 'Male' OR [Gender] = 'All'"
    '        ElseIf rbFemale.Checked Then
    '            dvOtherPlans.RowFilter = "[Gender] = 'Female' OR [Gender] = 'All'"

    '        End If
    '        For _index As Int32 = 0 To dvOtherPlans.Count - 1
    '            myNode = New TreeNode
    '            myNode.Text = dvOtherPlans(_index)("Name")
    '            myNode.Tag = dvOtherPlans(_index)("CriteriaID")
    '            myNode.ImageIndex = 11
    '            myNode.SelectedImageIndex = 11
    '            trvOtherCriterias.Nodes.Add(myNode)
    '            myNode = Nothing
    '        Next

    '    End If
    'End Sub

    'Private Sub trvOtherCriterias_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    '    Try
    '        ''TEMPORARY EXIT BY SUDHIR 20090313
    '        trvOtherCriterias.ContextMenuStrip = Nothing
    '        Exit Sub

    '        trvOtherCriterias.SelectedNode = trvOtherCriterias.GetNodeAt(e.X, e.Y)
    '        If e.Button = MouseButtons.Right Then
    '            trvOtherCriterias.ContextMenuStrip = cntHealthPlan
    '            cntHealthPlan.Items.Clear()

    '            Dim oChildMenu As ToolStripMenuItem
    '            oChildMenu = New ToolStripMenuItem()
    '            oChildMenu.Text = "New Patient Criteria"
    '            oChildMenu.Name = "New"
    '            oChildMenu.Image = ImageList1.Images(12)
    '            AddHandler oChildMenu.Click, AddressOf AddNewCriteria
    '            cntHealthPlan.Items.Add(oChildMenu)
    '            oChildMenu = Nothing

    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    'Private Sub trvOtherCriterias_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs)
    '    'TO DO: Need to handle the Context Menu in different way - Mahesh
    '    'trvOtherCriterias.ContextMenuStrip = Nothing
    '    Exit Sub
    '    ''
    '    If Not IsNothing(e.Node) Then
    '        trvOtherCriterias.SelectedNode = e.Node
    '        If e.Button = Windows.Forms.MouseButtons.Right Then
    '            trvOtherCriterias.ContextMenuStrip = cntHealthPlan
    '            cntHealthPlan.Items.Clear()

    '            Dim oChildMenu As ToolStripMenuItem

    '            oChildMenu = New ToolStripMenuItem()
    '            oChildMenu.Text = "Modify Criteria"
    '            oChildMenu.Name = "Modify"
    '            oChildMenu.Image = ImageList1.Images(16)
    '            AddHandler oChildMenu.Click, AddressOf ModifyOtherCriteria
    '            cntHealthPlan.Items.Add(oChildMenu)
    '            oChildMenu = Nothing

    '        End If
    '    Else
    '        trvOtherCriterias.ContextMenuStrip = Nothing
    '    End If
    'End Sub

    'Private Sub ModifyOtherCriteria(ByVal sender As Object, ByVal e As EventArgs)
    '    Try
    '        Dim MyNode As TreeNode
    '        MyNode = trvOtherCriterias.SelectedNode
    '        If Not IsNothing(MyNode) Then
    '            Dim _CriteriaId As Int64 = CType(MyNode.Tag, Int64)
    '            Dim ofrm As New frmDM_PatientCriteria(_CriteriaId, m_PatientID)
    '            ofrm.ShowDialog(Me)
    '            trvHealthPlan.ContextMenu = Nothing

    '            '' REFRESH HEALTH PLAN ''
    '            BindHealthPlan()
    '            selectGender()
    '            GetOtherHeathPlans()
    '            BindOtherHealthPlans()
    '            ''
    '        End If

    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub AddNewCriteria(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim ofrm As New frmDM_PatientCriteria(0, m_PatientID)
            ofrm.WindowState = FormWindowState.Normal
            ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
            ofrm.Dispose()
            ofrm = Nothing
            'Try
            '    If (IsNothing(trvHealthPlan.ContextMenu) = False) Then
            '        trvHealthPlan.ContextMenu.Dispose()
            '        trvHealthPlan.ContextMenu = Nothing
            '    End If
            'Catch ex As Exception

            'End Try
            trvHealthPlan.ContextMenu = Nothing
            _IsModifyCriteria = False
            '' REFRESH HEALTH PLAN ''
            'frmDM_PatientSpecific_Load(Nothing, Nothing)
            GetGeneralCriteria()
            BindHealthPlan()
            'selectGender()
            GetOtherHeathPlans()
            'BindOtherHealthPlans()
            ''

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub trvOtherCriterias_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs)
    '    If Not e.Node Is Nothing Then
    '        trvOtherCriterias.SelectedNode = e.Node
    '        AddHealthPlanNode(trvOtherCriterias.SelectedNode.Tag, True)
    '        For Each dRow As DataRow In dtOtherHeathPlans.Rows
    '            If trvOtherCriterias.SelectedNode.Tag = dRow("CriteriaID") Then
    '                dRow.Delete()
    '                dtOtherHeathPlans.AcceptChanges()
    '                Exit For
    '            End If
    '        Next
    '        trvOtherCriterias.SelectedNode.Remove()
    '    End If
    'End Sub

    'Private Sub rbSelection(ByVal sender As Object, ByVal e As System.EventArgs)
    '    BindOtherHealthPlans()
    'End Sub

    'Private Sub selectGender()
    '    Dim oPat As New clsPatient
    '    Dim dtPatientDetails As New DataTable
    '    dtPatientDetails = oPat.Fill_PatientDetails(m_PatientID)
    '    If Not dtPatientDetails Is Nothing Then
    '        If dtPatientDetails.Rows.Count > 0 Then
    '            If Not IsDBNull(dtPatientDetails.Rows(0)("Gender")) Then
    '                Dim strGender As String = dtPatientDetails.Rows(0)("Gender")
    '                If strGender = "Male" Then
    '                    rbMale.Checked = True
    '                ElseIf strGender = "Female" Then
    '                    rbFemale.Checked = True
    '                End If
    '            End If
    '        End If
    '    End If
    'End Sub


    'Private Sub rbMale_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    If rbMale.Checked = True Then
    '        rbMale.Font = New Font("Tahoma", 9, FontStyle.Bold)
    '    Else
    '        rbMale.Font = New Font("Tahoma", 9, FontStyle.Regular)
    '    End If
    'End Sub

    'Private Sub rbFemale_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    If rbFemale.Checked = True Then
    '        rbFemale.Font = New Font("Tahoma", 9, FontStyle.Bold)
    '    Else
    '        rbFemale.Font = New Font("Tahoma", 9, FontStyle.Regular)
    '    End If
    'End Sub

    Private Sub btn_tls_Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_tls_Ok.Click

    End Sub


#Region "DM Denoemalized Code"
    Private Sub AddCriteriaNodes(ByVal _TransId As Int64, ByVal _TriggerId As Int64, ByVal _TriggerName As String, ByVal _TriggerData As Object, ByVal _Recurring As Boolean, ByVal rootNode As myTreeNode, ByVal strType As String, ByVal imgIndex As Int32, ByVal CriteriaName As String, ByVal TriggerInfo As String, ByVal DrugForm As String, ByVal Route As String, ByVal Frequency As String, ByVal Duration As String, ByVal NDCCode As String, ByVal IsNarcotics As Integer, ByVal DrugQtyQualifier As String)
        Try
            Dim blnExist As Boolean = False
            Dim parentNode As myTreeNode = Nothing
            Dim strDueDesc As String = ""
            ''Loop through Criteria node to check for the parent node of 'strtype' is available
            For Each TargetNode As myTreeNode In rootNode.Nodes
                If TargetNode.Text = strType Then
                    parentNode = TargetNode
                    blnExist = True
                End If
            Next
            ''if not exists then add the node to the root node as parent node
            If Not blnExist Then
                parentNode = New myTreeNode
                parentNode.Text = strType
                parentNode.ImageIndex = imgIndex
                parentNode.SelectedImageIndex = imgIndex
                rootNode.Nodes.Add(parentNode)
            End If

            ''Map all the node values to the associated node
            Dim Associatenode As New myTreeNode
            Associatenode.Key = _TriggerId

            Associatenode.Tag = _TransId
            Associatenode.NodeName = _TriggerName

            'sarika DM Denormalization 20090403
            If Not IsNothing(_TriggerData) Then
                Associatenode.DMTemplate = _TriggerData
            Else
                Associatenode.DMTemplate = Nothing
            End If

            Associatenode.Dosage = TriggerInfo
            Associatenode.DrugForm = DrugForm
            Associatenode.Route = Route
            Associatenode.Duration = Duration
            Associatenode.Frequency = Frequency
            'Associatenode.mpid = mpid
            Associatenode.DrugQtyQualifier = DrugQtyQualifier
            Associatenode.NDCCode = NDCCode
            Associatenode.IsNarcotics = IsNarcotics


            '----------

            ''Get the Trigger due details and map it to Triggerdetails object 
            Dim oDetails As DataTable = oDM.FindDueTriggerDetails(_TransId, _Recurring)
            If Not oDetails Is Nothing Then
                If oDetails.Rows.Count > 0 Then
                    Dim objTrigger As New TriggerDetails
                    objTrigger.TransId = _TransId
                    objTrigger.TriggerId = _TriggerId
                    'If Not IsDBNull(CType(oDetails.Rows(0)("DM_nCriteriaID"), String)) Then
                    '    objTrigger.CriteriaId = CType(oDetails.Rows(0)("DM_nCriteriaID"), String)
                    'End If

                    If Not IsDBNull(oDetails.Rows(0)("DM_DueType")) Then
                        objTrigger.DueType = CType(oDetails.Rows(0)("DM_DueType"), String)
                    End If

                    If Not IsDBNull(oDetails.Rows(0)("DM_DueValue")) Then
                        objTrigger.DueValue = CType(oDetails.Rows(0)("DM_DueValue"), String)
                        If oDetails.Rows(0)("DM_DueValue") <> "" Then
                            strDueDesc = " " & "-" & " " & " Due on " & objTrigger.DueType & " " & objTrigger.DueValue
                        End If
                    End If

                    If Not IsDBNull(oDetails.Rows(0)("DM_bIsRecurring")) Then
                        objTrigger.Recurring = CType(oDetails.Rows(0)("DM_bIsRecurring"), Boolean)
                    End If

                    If Not IsDBNull(oDetails.Rows(0)("DM_sReason")) Then
                        objTrigger.Reason = CType(oDetails.Rows(0)("DM_sReason"), String)
                        If oDetails.Rows(0)("DM_sReason") <> "" Then
                            strDueDesc = strDueDesc & " " & "-" & " " & "Reason :" & objTrigger.Reason
                        End If
                    End If

                    If Not IsDBNull(oDetails.Rows(0)("DM_sNotes")) Then
                        objTrigger.Notes = CType(oDetails.Rows(0)("DM_sNotes"), String)
                        If oDetails.Rows(0)("DM_sNotes") <> "" Then
                            strDueDesc = strDueDesc & " " & "-" & " " & "Notes :" & objTrigger.Notes
                        End If
                    End If

                    If _Recurring Then
                        If Not IsDBNull(oDetails.Rows(0)("DM_dtStartDate")) Then
                            objTrigger.StartDate = CType(oDetails.Rows(0)("DM_dtStartDate"), DateTime)
                        End If
                        If Not IsDBNull(oDetails.Rows(0)("DM_dtEndDate")) Then
                            objTrigger.EndDate = CType(oDetails.Rows(0)("DM_dtEndDate"), DateTime)
                        End If
                        If Not IsDBNull(oDetails.Rows(0)("DM_nDurationType")) Then
                            objTrigger.DurationType = CType(oDetails.Rows(0)("DM_nDurationType"), String)
                        End If
                        If Not IsDBNull(oDetails.Rows(0)("DM_nDurationPeriod")) Then
                            objTrigger.DurationPeriod = CType(oDetails.Rows(0)("DM_nDurationPeriod"), Int32)
                            If objTrigger.DurationType = chkOnEveryCheckIn.Text Then
                                objTrigger.OnEveryCheckIn = True
                                strDueDesc = strDueDesc & " - "
                            Else
                                objTrigger.OnEveryCheckIn = False
                                strDueDesc = strDueDesc & " " & "-" & " " & "Recur every " & objTrigger.DurationPeriod
                            End If
                            strDueDesc = strDueDesc & " " & objTrigger.DurationType
                        End If

                    End If

                    ''Save it as TemplateResult object for the treenode
                    Associatenode.TemplateResult = objTrigger
                Else
                    ''if due detail are not available set to null
                    Associatenode.TemplateResult = Nothing
                End If
            Else

                ''if due detail are not available set to null
                Associatenode.TemplateResult = Nothing
            End If

            Associatenode.Text = _TriggerName & strDueDesc
            Associatenode.ImageIndex = 0
            Associatenode.SelectedImageIndex = 0
            ''Add the respective node to the parent node
            parentNode.Nodes.Add(Associatenode)
            trvHealthPlan.ExpandAll()

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    'Private Sub SaveGivenTriggers(ByVal _CriteriaID As Int64, ByVal _TriggerId As Int64, ByVal _TriggerName As String, ByVal _TriggerResult As Object, ByVal nType As DiseaseManagement.TemplateCategoryID, ByVal bIsgiven As Boolean)
    '    For Each myCriteria As myTreeNode In trvHealthPlan.Nodes
    '        ''loop through each Parent node(i.e. labs, Orders, Rx, Guidelines, Referrals) in Criteria node
    '        If myCriteria.Key = _CriteriaID Then
    '            For Each parentNode As myTreeNode In myCriteria.Nodes
    '                ''loop through each child node   i.e Trigger item
    '                For Each mynode As myTreeNode In parentNode.Nodes
    '                    If mynode.Key = _TriggerId Then
    '                        If bIsgiven Then
    '                            ''If not selected check whether Reason for not giving is documented or not
    '                            If mynode.TemplateResult Is Nothing Then
    '                                ''Delete the Exisiting record if present
    '                                oclsDM_PatientSpecific.DeleteTemplate(_CriteriaID, _TriggerId, m_PatientID)
    '                                oclsDM_PatientSpecific.Save_Trigger(0, m_PatientID, _CriteriaID, _TriggerId, mynode.DMTemplateName, myCriteria.Name, mynode.DMTemplate, Format(Now, "MM/dd/yyyy"), "", frmDM_Template.Type.Save, nType, , , , , , True, False, mynode.Dosage)
    '                            Else
    '                                ''Get the Trigger details as object and validate for reason field

    '                                Dim oTrigger As TriggerDetails = CType(mynode.TemplateResult, TriggerDetails)
    '                                If oTrigger.Recurring Then

    '                                    ''Delete the Exisiting record if present
    '                                    oclsDM_PatientSpecific.DeleteTemplate(_CriteriaID, _TriggerId, m_PatientID)

    '                                    Dim TrnsId As Int64
    '                                    TrnsId = oclsDM_PatientSpecific.Save_Trigger(0, m_PatientID, _CriteriaID, _TriggerId, mynode.DMTemplateName, myCriteria.Name, mynode.DMTemplate, Format(Now, "MM/dd/yyyy"), "", frmDM_Template.Type.Save, nType, oTrigger.DueType, oTrigger.DueValue, False, oTrigger.Reason, oTrigger.Notes, False, True, mynode.Dosage)
    '                                    ''Here u need to insert the recurring value details
    '                                    If TrnsId <> 0 Then
    '                                        oclsDM_PatientSpecific.Save_TemplateDetail(TrnsId, oTrigger.StartDate, oTrigger.EndDate, oTrigger.DurationType, oTrigger.DurationPeriod)
    '                                    End If
    '                                Else
    '                                    ''Delete the Exisiting record if present
    '                                    oclsDM_PatientSpecific.DeleteTemplate(_CriteriaID, _TriggerId, m_PatientID)
    '                                    oclsDM_PatientSpecific.Save_Trigger(0, m_PatientID, _CriteriaID, _TriggerId, mynode.DMTemplateName, myCriteria.Name, mynode.DMTemplate, Format(Now, "MM/dd/yyyy"), "", frmDM_Template.Type.Save, nType, oTrigger.DueType, oTrigger.DueValue, False, oTrigger.Reason, oTrigger.Notes, True, False, mynode.Dosage)
    '                                End If
    '                            End If
    '                            If parentNode.Nodes.Count > 1 Then
    '                                mynode.Remove()
    '                            Else
    '                                parentNode.Remove()
    '                            End If
    '                        Else
    '                            mynode.Checked = False
    '                        End If

    '                    End If
    '                Next
    '            Next
    '        End If

    '    Next

    'End Sub


    Private Sub SaveGivenTriggers(ByVal _CriteriaID As Int64, ByVal objList As myList, ByVal nType As DiseaseManagement.TemplateCategoryID, ByVal bIsgiven As Boolean)
        Try
            For Each myCriteria As myTreeNode In trvHealthPlan.Nodes
                ''loop through each Parent node(i.e. labs, Orders, Rx, Guidelines, Referrals) in Criteria node
                If myCriteria.Key = _CriteriaID Then
                    For Each parentNode As myTreeNode In myCriteria.Nodes
                        ''loop through each child node   i.e Trigger item
                        If Not IsNothing(parentNode) Then
                            For Each mynode As myTreeNode In parentNode.Nodes
                                If Not IsNothing(mynode) Then
                                    If mynode.NodeName = objList.Value Then
                                        If bIsgiven Then
                                            ''If not selected check whether Reason for not giving is documented or not
                                            If mynode.TemplateResult Is Nothing Then
                                                ''Delete the Exisiting record if present
                                                oclsDM_PatientSpecific.DeleteTemplate(_CriteriaID, objList.ID, m_PatientID)
                                                'Changed by Shweta 20100117
                                                'Against the bug id:5350 
                                                'oclsDM_PatientSpecific.Save_Trigger(0, m_PatientID, _CriteriaID, objList.ID, mynode.DMTemplateName, myCriteria.NodeName, mynode.DMTemplate, Format(Now, "MM/dd/yyyy"), "", frmDM_Template.Type.Save, nType, , , , , , True, False, mynode.Dosage)
                                                oclsDM_PatientSpecific.Save_Trigger(0, m_PatientID, _CriteriaID, objList.ID, mynode.DMTemplateName, myCriteria.NodeName, mynode.DMTemplate, Format(Now, "MM/dd/yyyy"), "", frmDM_Template.Type.Save, nType, mynode, , , , , , True, False, mynode.Dosage)
                                                'End
                                            Else
                                                ''Get the Trigger details as object and validate for reason field

                                                Dim oTrigger As TriggerDetails = CType(mynode.TemplateResult, TriggerDetails)
                                                If oTrigger.Recurring Then

                                                    ''Delete the Exisiting record if present
                                                    oclsDM_PatientSpecific.DeleteTemplate(_CriteriaID, objList.ID, m_PatientID)

                                                    Dim TrnsId As Int64
                                                    'Changed by Shweta 20100117
                                                    'Against the bug id:5350 
                                                    TrnsId = oclsDM_PatientSpecific.Save_Trigger(0, m_PatientID, _CriteriaID, objList.ID, mynode.DMTemplateName, myCriteria.Name, mynode.DMTemplate, Format(Now, "MM/dd/yyyy"), "", frmDM_Template.Type.Save, nType, mynode, oTrigger.DueType, oTrigger.DueValue, False, oTrigger.Reason, oTrigger.Notes, False, True, mynode.Dosage)
                                                    'End
                                                    ''Here u need to insert the recurring value details
                                                    If TrnsId <> 0 Then

                                                        oclsDM_PatientSpecific.Save_TemplateDetail(TrnsId, oTrigger.StartDate, oTrigger.EndDate, oTrigger.DurationType, oTrigger.DurationPeriod)
                                                    End If
                                                Else
                                                    ''Delete the Exisiting record if present
                                                    oclsDM_PatientSpecific.DeleteTemplate(_CriteriaID, objList.ID, m_PatientID)
                                                    'Changed by Shweta 20100117
                                                    'Against the bug id:5350 
                                                    'oclsDM_PatientSpecific.Save_Trigger(0, m_PatientID, _CriteriaID, objList.ID, mynode.DMTemplateName, myCriteria.Name, mynode.DMTemplate, Format(Now, "MM/dd/yyyy"), "", frmDM_Template.Type.Save, nType, oTrigger.DueType, oTrigger.DueValue, False, oTrigger.Reason, oTrigger.Notes, True, False, mynode.Dosage)
                                                    oclsDM_PatientSpecific.Save_Trigger(0, m_PatientID, _CriteriaID, objList.ID, mynode.DMTemplateName, myCriteria.Name, mynode.DMTemplate, Format(Now, "MM/dd/yyyy"), "", frmDM_Template.Type.Save, nType, mynode, oTrigger.DueType, oTrigger.DueValue, False, oTrigger.Reason, oTrigger.Notes, True, False, mynode.Dosage)
                                                    'End
                                                End If
                                            End If
                                            If parentNode.Nodes.Count > 1 Then
                                                mynode.Remove()
                                            Else
                                                parentNode.Remove()
                                            End If
                                        Else
                                            mynode.Checked = False
                                        End If

                                    End If
                                End If
                            Next
                        End If
                    Next
                End If

            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


#End Region


    Private Sub GloUC_trvPatients_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs)
        Try

            Dim oPatientNode As gloUserControlLibrary.myTreeNode = CType(e.Node, gloUserControlLibrary.myTreeNode)
            If Not IsNothing(oPatientNode) Then
                m_PatientID = oPatientNode.ID
            End If
            loadPatientStrip()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvHealthPlan_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvHealthPlan.AfterSelect

    End Sub
#Region "To check the the selcted nodes or not"

    ''' <summary>
    ''' Dhruv 20101302
    ''' checking for the Children node is selected or not
    ''' </summary>
    ''' <param name="e"></param>
    ''' <param name="IsCheck"></param>
    ''' <remarks></remarks>
    Private Sub CheckAllChildren(ByVal e As TreeNode, ByVal IsCheck As Boolean)
        bParentTrigger = False
        For Each mytreenode As TreeNode In e.Nodes
            bChildTrigger = False
            mytreenode.Checked = IsCheck
            bChildTrigger = True
            CheckAllChildren(mytreenode, IsCheck)
        Next
        bParentTrigger = True
    End Sub
    ''Checking for the parent node is selected or not
    Private Sub CheckAllParents(ByVal e As TreeNode, ByVal IsCheck As Boolean)
        If e Is Nothing Then
            Exit Sub
        End If
        If e.Parent Is Nothing Then
            Exit Sub
        End If

        bChildTrigger = False
        bParentTrigger = False

        If IsCheck Then
            Dim bNodeFound As Boolean = False
            For Each _Node As TreeNode In e.Parent.Nodes
                If _Node.Checked = False Then
                    e.Parent.Checked = False
                    bNodeFound = True
                    Exit For
                End If
            Next
            If bNodeFound = False Then
                e.Parent.Checked = True
            End If
        Else
            e.Parent.Checked = IsCheck
        End If

        CheckAllParents(e.Parent, IsCheck)
        bParentTrigger = True
        bChildTrigger = True
    End Sub
#End Region

    Private Sub gloLabOrderScreen()

        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim objclsgeneral As New gloEmdeonInterface.Classes.clsGeneral()
        Dim _LoginUserProviderID As Long = 0
        Dim _PatientProviderID As Long = 0


        Dim loopcnt As Int16 = 0
        Dim objClsgloLabPatientLayer As gloEmdeonInterface.Classes.clsgloLabPatientLayer = New gloEmdeonInterface.Classes.clsgloLabPatientLayer()
        Dim objpatient As New gloPatient.Patient()
        Dim objgloPatient As New gloPatient.gloPatient(GetConnectionString)
        'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
        'objpatient = objgloPatient.GetPatient(gnPatientID)
        objpatient = objgloPatient.GetPatient(m_PatientID)
        'end modification

        _LoginUserProviderID = GetProviderIDForUser(gnLoginID)
        _PatientProviderID = objpatient.DemographicsDetail.PatientProviderID

        'Commented  by madan on 20100601
        'If Not compareProvider(_PatientProviderID, _LoginUserProviderID) Then
        '    Return
        'End If

        If Not gloEmdeonInterface.Classes.clsEmdeonGeneral.CheckConnectionParameters(GetConnectionString) Then
            MessageBox.Show("Lab Settings have not been configured in gloEMR Admin." + vbCrLf + "Please complete Lab Settings before ordering.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' Checking Internet Connection
        Dim LabConncetionAvailable As gloEmdeonInterface.Classes.clsGeneral.InternetConnectivity
        LabConncetionAvailable = objclsgeneral.IsInternetConnectionAvailable()

        If LabConncetionAvailable = gloEmdeonInterface.Classes.clsGeneral.InternetConnectivity.Success Then

            'Modified by madan on 20100601
            If Not compareProvider(_PatientProviderID, _LoginUserProviderID) Then
                Return
            End If
            'Added by madan on 20100601
            Dim _billingStatus As Boolean = False

            Dim objGloPatientLayer = New gloEmdeonInterface.Classes.clsgloLabPatientLayer()
            _billingStatus = objClsgloLabPatientLayer.CheckBillingType(objpatient)


            If _billingStatus = True Then
                If gloEmdeonInterface.Classes.clsEmdeonGeneral.IsDemoLab = True Then
                    Dim frmLabDemo As New gloEmdeonInterface.Forms.frmLabDemonstration(m_PatientID)
                    frmLabDemo.WindowState = FormWindowState.Maximized
                    frmLabDemo.BringToFront()
                    frmLabDemo.ShowDialog(IIf(IsNothing(frmLabDemo.Parent), Me, frmLabDemo.Parent))
                    frmLabDemo.Dispose()
                Else

                    Dim strQry As String = String.Empty
                    Dim boolPatientReg As [Boolean] = False
                    ' flag for patient registration 
                    'string strQry="select count(*) from patient_gloLab where nPatientId="_patientID;
                    If ConfirmNull(objpatient.DemographicsDetail.PatientCode.ToString()) Then
                        'strQry = "select count(*) from patient_gloLab where sPatientCode='" & objpatient.DemographicsDetail.PatientCode.ToString().Trim() & "'"
                        strQry = "SELECT COUNT(*) FROM PatientExternalCodes INNER JOIN Patient ON PatientExternalCodes.nPatientId = Patient.nPatientID  where PatientExternalCodes.sExternalType = 'EMDEON' AND  Patient.sPatientCode='" & objpatient.DemographicsDetail.PatientCode.ToString().Trim() & "'"
                    End If
                    oDB.Connect(False)

                    ' loop for checking patient registration
                    'Try 3 times for patient registration if fails to register.
                    For loopcnt = 1 To 3
                        ' checking patient is registered or not
                        'Int32 cnt = Convert.ToInt32(objSqlCmd.ExecuteScalar());
                        Dim cnt As Int32 = 0
                        cnt = Convert.ToInt32(oDB.ExecuteScalar_Query(strQry))
                        If cnt < 1 Then
                            Application.DoEvents()
                            'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                            'gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_patID = gnPatientID
                            gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_patID = m_PatientID
                            'end modification

                            'boolPatientReg = objClsgloLabPatientLayer.RegisterPatienttoEmdeon(objpatient);
                            boolPatientReg = objClsgloLabPatientLayer.RegisterGloPatient(objpatient, GetConnectionString)

                            'lblProcessInformation.Text = "Process Information";
                            'gloEmdeonInterface.Forms.frmViewgloLab.pnlregistration.Visible = False
                            If boolPatientReg Then
                                Exit For
                            End If
                        Else
                            boolPatientReg = True
                            Exit For
                        End If
                    Next

                    If boolPatientReg = True Then
                        ' if patient is registered
                        'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                        'Dim objfrmEmdonInterface As New gloEmdeonInterface.Forms.frmEmdeonInterface(gnPatientID)
                        Dim objfrmEmdonInterface As New gloEmdeonInterface.Forms.frmEmdeonInterface(m_PatientID)
                        'end modification
                        objfrmEmdonInterface.WindowState = FormWindowState.Maximized
                        objfrmEmdonInterface.ShowDialog(IIf(IsNothing(objfrmEmdonInterface.Parent), Me, objfrmEmdonInterface.Parent))
                        objfrmEmdonInterface.Dispose()
                        objfrmEmdonInterface = Nothing
                        '' By Abhijeet Farkande on date 20100223
                        '' changes : refreshing the Order information for patient
                        'fillOpenOrdsGrid()
                        'gloUCLab_History_gUC_FillOrder(2)
                        ''gloUCLab_Transaction.ClearTest();
                        '' end of changes by Abhijeet for refreshing the order details.
                        'gloEmdeonInterface.Forms.frmViewgloLab.gloLabUC_Transaction1.ClearTest()
                    Else

                        If ConfirmNull(gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_Identifier.ToString()) Then
                            MessageBox.Show(gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_Identifier.ToString().Trim(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            MessageBox.Show("Patient is not registered With Emdeon,please try again.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
                    ' }
                    'else
                    '{
                    ' MessageBox.Show("Please check admin settings.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    '}
                End If
            End If
        Else
            If LabConncetionAvailable = gloEmdeonInterface.Classes.clsGeneral.InternetConnectivity.NoInternet Then

                ' MessageBox.Show("Connection error, internet connection not available." & vbCr & vbLf & vbCr & vbLf & "You must be connected to the internet to access gloLab orders.", "gloEMR", MessageBoxButtons.OK)
                Dim objFrmConnectionConfirm As New gloEmdeonInterface.Forms.frmConfirmInternetConnection(True)
                objFrmConnectionConfirm.ShowInTaskbar = False
                objFrmConnectionConfirm.ShowDialog(IIf(IsNothing(objFrmConnectionConfirm.Parent), Me, objFrmConnectionConfirm.Parent))
                objFrmConnectionConfirm.Dispose()

            ElseIf LabConncetionAvailable = gloEmdeonInterface.Classes.clsGeneral.InternetConnectivity.ServerNotresponding Then

                Dim objFrmConnectionConfirm As New gloEmdeonInterface.Forms.frmConfirmInternetConnection(False)
                objFrmConnectionConfirm.ShowInTaskbar = False
                objFrmConnectionConfirm.ShowDialog(IIf(IsNothing(objFrmConnectionConfirm.Parent), Me, objFrmConnectionConfirm.Parent))
                objFrmConnectionConfirm.Dispose()

            End If
        End If

    End Sub


    Protected Function ConfirmNull(ByVal strValue As String) As Boolean
        Dim blnCheck As Boolean = False
        Try
            If strValue IsNot Nothing AndAlso strValue.ToString().Trim().Length <> 0 AndAlso strValue.ToString() <> "" Then

                blnCheck = True
            End If
        Catch ex As Exception
            'objclsGeneral.UpdateLog("Null reference value!" + ex.ToString());
        End Try
        Return blnCheck
    End Function
    'Added by madan-- on 20100419 
    Public Function GetProviderIDForUser(ByVal UserID As Int64) As Int64
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim ProID As Int64 = 0
        Try
            oDB.Connect(False)
            'ProID = Trim(oDB.ExecuteScaler) 
            ProID = Convert.ToInt64(oDB.ExecuteScalar_Query("SELECT nProviderID from user_mst where nUserID = " & UserID & ""))
            oDB.Disconnect()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)

            ProID = 0
        Finally
            oDB.Dispose()
        End Try
        Return ProID
    End Function
    ' added by madan on 20100419-- for provider comparison. 
    Private Function compareProvider(ByVal _PatientProviderID As Int64, ByVal _LoginUserProviderID As Int64) As Boolean
        Dim objClsGeneral As New gloEmdeonInterface.Classes.clsGeneral()
        Dim strProviderName As String = String.Empty
        Dim strLoginUserName As String = String.Empty
        Dim strLabID As String = String.Empty
        Try
            If _PatientProviderID <> 0 Then
                strProviderName = objClsGeneral.GetProviderName(_PatientProviderID, gnClinicID)
            End If
            If _LoginUserProviderID <> 0 Then
                strLoginUserName = objClsGeneral.GetProviderName(_LoginUserProviderID, gnClinicID)
            End If
            If _LoginUserProviderID = 0 Then
                'Modified as per 'DREW NALON' Commnets by madan on 20100515 

                Dim drMesgResult As DialogResult = MessageBox.Show(("The user you are using is not set up as a provider. If you proceed, the lab order " & vbCr & vbLf & "provider will be defaulted to the current patient’s provider '") + strProviderName & "'." & vbCr & vbLf & vbCr & vbLf & "Would you like to proceed with creating a new order?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)


                'if (MessageBox.Show("Login user is not a provider, Do you like to proceed placing orders with labs as patient provider", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) 
                If drMesgResult = DialogResult.Yes Then
                    strLabID = objClsGeneral.GetProvidergloLabId(_PatientProviderID)
                    If ConfirmNull(strLabID.ToString()) Then
                        Return True
                    Else
                        If MessageBox.Show("The current provider '" & strProviderName & " ' does not have a lab ID set up." & vbCr & vbLf & "If you place a lab order, you will have to select a provider in the labs interface." & vbCr & vbLf & "Would you like to proceed with the lab order? " & vbCr & vbLf & vbCr & vbLf & "To set up a lab ID, go to provider settings.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Return True
                        Else
                            Return False
                        End If
                    End If
                Else
                    Return False
                End If
            End If

            If _LoginUserProviderID <> _PatientProviderID Then
                'Modified as per "DREW NALON" Comments by madan on 20100515. 
                Dim dgResult As DialogResult = MessageBox.Show((("This patient is currently assigned to the provider '" & strProviderName & "'.Would " & vbCr & vbLf & "you like to change the patient provider to '") + strLoginUserName & "' ? " & vbCr & vbLf & vbCr & vbLf & "If you select 'No', the lab order will be created for '") + strProviderName & "'.", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                If dgResult = DialogResult.Yes Then
                    'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                    'If objClsGeneral.changePatientProvider(_LoginUserProviderID, gnPatientID) Then
                    If objClsGeneral.changePatientProvider(_LoginUserProviderID, m_PatientID) Then
                        'end modification
                        strLabID = objClsGeneral.GetProvidergloLabId(_LoginUserProviderID)
                        If ConfirmNull(strLabID.ToString()) Then
                            Return True
                        Else
                            If MessageBox.Show("The current provider '" & strLoginUserName & " ' does not have a lab ID set up." & vbCr & vbLf & "If you place a lab order, you will have to select a provider in the labs interface." & vbCr & vbLf & "Would you like to proceed with the lab order? " & vbCr & vbLf & vbCr & vbLf & "To set up a lab ID, go to provider settings.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                Return True
                            Else
                                Return False
                            End If
                        End If
                    Else
                        Return False
                    End If
                ElseIf dgResult = DialogResult.No Then
                    strLabID = objClsGeneral.GetProvidergloLabId(_PatientProviderID)
                    If ConfirmNull(strLabID.ToString()) Then
                        Return True
                    Else
                        'Modified as per "DREW NALON" Comments by madan on 20100515. 
                        If MessageBox.Show("The current provider '" & strProviderName & " ' does not have a lab ID set up." & vbCr & vbLf & "If you place a lab order, you will have to select a provider in the labs interface." & vbCr & vbLf & "Would you like to proceed with the lab order? " & vbCr & vbLf & vbCr & vbLf & "To set up a lab ID, go to provider settings.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Return True
                        Else
                            Return False
                        End If
                    End If
                ElseIf dgResult = DialogResult.Cancel Then
                    Return False

                End If
            End If

            If _LoginUserProviderID = _PatientProviderID Then
                strLabID = objClsGeneral.GetProvidergloLabId(_LoginUserProviderID)
                If ConfirmNull(strLabID.ToString()) Then
                    Return True
                Else
                    If MessageBox.Show("The current provider '" & strLoginUserName & " ' does not have a lab ID set up." & vbCr & vbLf & "If you place a lab order, you will have to select a provider in the labs interface." & vbCr & vbLf & "Would you like to proceed with the lab order? " & vbCr & vbLf & vbCr & vbLf & "To set up a lab ID, go to provider settings.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return False
        End Try
    End Function

    'Added by madan on 20100616
    '' By Abhijeet on 20100626,Added optional parameter '_arrLabs' in function defination
    Private Sub gloLabSettings(ByVal _TaskType As String, Optional ByVal _TestList As String = "", Optional ByVal _arrLabs As ArrayList = Nothing)

        Select Case _TaskType.ToString().ToUpper()
            Case "TASK"
                Dim objClsGeneral As New gloEmdeonInterface.Classes.clsGeneral()
                Dim objClsgloLabPatientLayer As gloEmdeonInterface.Classes.clsgloLabPatientLayer = New gloEmdeonInterface.Classes.clsgloLabPatientLayer()
                Dim objpatient As New gloPatient.Patient()
                Dim objgloPatient As New gloPatient.gloPatient(GetConnectionString)
                'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                'objpatient = objgloPatient.GetPatient(gnPatientID)
                objpatient = objgloPatient.GetPatient(m_PatientID)
                'end modification
                Dim _LoginProviderId As Int64 = 0
                _LoginProviderId = GetProviderIDForUser(gnLoginID)

                ''Sanjog - Added on 2011 August 25 to solve bug no 8255
                Dim strlabs As String = ""
                Dim strlab As String = ""
                strlab = ""
                'Dim sDescription As String = " For Lab Test:" & vbCrLf
                Dim ncnt As Integer = 1

                If Not IsNothing(_arrLabs) Then
                    For olab As Integer = 0 To arrLabs.Count - 1
                        strlab = (arrLabs.Item(olab)).ID & "~" & (arrLabs.Item(olab)).Value
                        If olab = 0 Then
                            strlabs = strlab
                            'sDescription = sDescription & " " & ncnt & ". " & arrLabs.Item(olab).Value & vbCrLf
                            ncnt = ncnt + 1
                        Else
                            strlabs = strlabs & "|" & strlab
                            'sDescription = sDescription & " " & ncnt & ". " & arrLabs.Item(olab).Value & vbCrLf
                            ncnt = ncnt + 1
                        End If
                    Next
                End If

                ''Sanjog - Added on 2011 August 25 to solve bug no 8255

                objClsGeneral.TestlistOnly = strlabs
                objClsGeneral.TestList = _TestList
                ''Added by Abhijeet on 20100625 , for audit trial implementation
                'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                'Dim nTaskID As Long = objClsGeneral.AssignTaskToUser(gnPatientID, objpatient.DemographicsDetail.PatientProviderID, _LoginProviderId)
                Dim nTaskID As Long = objClsGeneral.AssignTaskToUser(m_PatientID, objpatient.DemographicsDetail.PatientProviderID, _LoginProviderId, 0, gloTaskMail.TaskType.PlaceLabOrder)
                'end modification
                If nTaskID > 0 Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.LabOrderRequest, "Task assigned for placing lab order", m_PatientID, 0, _LoginProviderId, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
                '' End of changes by Abhijeet on 20100625

                _LoginProviderId = 0
            Case "LABORDER"
                'If gloEmdeonInterface.Classes.clsEmdeonGeneral.IsDemoLab = True Then
                '    Return
                'End If
                gloLabOrderScreen()

            Case "RECORDRESULTS"
                'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                'Dim frmNormalLab As New gloEmdeonInterface.Forms.frmViewNormalLab(gnPatientID)
                Dim frmNormalLab As New gloEmdeonInterface.Forms.frmViewNormalLab(m_PatientID)
                AddHandler frmNormalLab.Event_CallCDA, AddressOf mdlGeneral.OpenCDA
                'end modification
                frmNormalLab.ArrLabs = _arrLabs '' Added by Abhijeet on 20100626
                frmNormalLab.WindowState = FormWindowState.Maximized
                frmNormalLab.ShowInTaskbar = False
                frmNormalLab.BringToFront()
                frmNormalLab.ShowDialog(IIf(IsNothing(frmNormalLab.Parent), Me, frmNormalLab.Parent))
                RemoveHandler frmNormalLab.Event_CallCDA, AddressOf mdlGeneral.OpenCDA
                frmNormalLab.Dispose()
                frmNormalLab = Nothing
                'If Not IsNothing(frmNormalLab.ArrLabs) Then
                '    If frmNormalLab.ArrLabs.Count > 0 Then
                '        ''arrLabs.Clear()
                '        arrLabs = CType(_arrLabs, myList)
                '    End If
                'End If

            Case Else
                MessageBox.Show("Please configure Default user for Task in EMR Admin - Lab Settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Select
        End Select
    End Sub
    ''' <summary>
    ''' Property writen for Patient Context
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return m_PatientID   'Curent patient variable(Local variable) for this module 
        End Get
    End Property

    Private Sub ShowRecommendationsAlert()
        Dim oDM As New gloStream.DiseaseManagement.DiseaseManagement
        Dim _dt As DataTable = Nothing
        Try
            pnlRecomendationAlert.Visible = False
            _dt = oDM.GetRecommendationsCountAndName(m_PatientId)

            If _dt IsNot Nothing AndAlso _dt.Rows.Count > 0 Then

                If Convert.ToInt32(_dt.Rows(0)("RecommendationCount")) > 0 Then

                    lblRecomendationAlert.Text = "Recommendations (" & _dt.Rows(0)("RecommendationCount") & ") :" 'set the count of recommendations here 
                    lblLastRecomendationName.Text = _dt.Rows(0)("RecommendationName").ToString()  'set the last announced recommendation name here
                    If gbShowviewRecommendation Then
                        pnlRecomendationAlert.Visible = True
                        btn_tls_RecommendationviewRecommendation.Visible = True
                    Else
                        pnlRecomendationAlert.Visible = False
                        btn_tls_RecommendationviewRecommendation.Visible = False

                    End If

                End If

            End If

        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            pnlRecomendationAlert.Visible = False

            If oDM IsNot Nothing Then
                oDM.Dispose()
                oDM = Nothing
            End If

            If _dt IsNot Nothing Then
                _dt.Dispose()
                _dt = Nothing
            End If
        Finally

            If oDM IsNot Nothing Then
                oDM.Dispose()
                oDM = Nothing
            End If

            If _dt IsNot Nothing Then
                _dt.Dispose()
                _dt = Nothing
            End If
        End Try
    End Sub
End Class
