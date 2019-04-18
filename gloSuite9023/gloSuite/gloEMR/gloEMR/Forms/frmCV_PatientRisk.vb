Imports gloEMR.gloStream.CardioVascular

Public Class frmCV_PatientRisk
    Implements IPatientContext

#Region " C1 Constants "
    Private Const Col_Risk_CategoryID = 0
    Private Const Col_Risk_Count = 1

    Private Const RowHeight = 22
#End Region

    Dim oCV As New CardioVascular
    Dim oCriterias As Supporting.Criterias
    Dim oPatientDetail As Supporting.PatientDetail
    Dim oRisks As Supporting.PatientDetails

    Dim mPatientID As Int64 = 0
    Private _RiskFound As Boolean = False

#Region " Constructors "
    Public Sub New(ByVal PatientID As Int64)
        InitializeComponent()
        mPatientID = PatientID

        '' SUDHIR 20100104 '' 
        oPatientDetail = oCV.GetPatientDetails(mPatientID)
        oCriterias = GetAllCriteria()
        oRisks = oCV.GetAllRisk(oPatientDetail, oCriterias)
        If oRisks IsNot Nothing Then
            If oRisks.Count > 0 Then
                _RiskFound = True
            Else
                _RiskFound = False
            End If
        Else
            _RiskFound = False

        End If

    End Sub
#End Region

#Region " Public Properties "
    Public Property RiskFound() As Boolean
        Get
            Return _RiskFound
        End Get
        Set(ByVal value As Boolean)
            _RiskFound = value
        End Set
    End Property
#End Region

    Private Sub frmCV_PatientRisk_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub


    Private Sub frmCV_PatientRisk_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        gloC1FlexStyle.Style(C1FamilyHistory)
        gloC1FlexStyle.Style(C1PatientRisk)
        gloC1FlexStyle.Style(C1SocialHistory)

        Try
            'oPatientDetail = oCV.GetPatientDetails(mPatientID)
            'oCriterias = GetAllCriteria()
            'oRisks = oCV.GetAllRisk(oPatientDetail, oCriterias)
            If oRisks IsNot Nothing Then
                If oRisks.Count > 0 Then
                    FillAllRisk(oRisks)
                End If
            End If

            'If oRisks.Count <= 1 And C1PatientRisk.Rows.Count <= 1 Then
            '    MessageBox.Show("No risk found for patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    'Me.Close()
            'End If
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, "Viewed CardioVascular Patient Risk", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, "Viewed CardioVascular Patient Risk", mPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.RecordViewed, "Viewed the Patient Risk records", gstrLoginName, gstrClientMachineName, mPatientID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, "Could not view CardioVascular Patient Risk", gloAuditTrail.ActivityOutCome.Failure)
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.RecordViewed, "Could not view the Patient Risk records", gstrLoginName, gstrClientMachineName, mPatientID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Try
            gloPatient.gloPatient.GetWindowTitle(Me, mPatientID, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Function GetAllCriteria() As gloStream.CardioVascular.Supporting.Criterias
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim arrList As New ArrayList
        Dim Query As String = ""
        Dim oReader As SqlClient.SqlDataReader = Nothing
        Dim oCriterias As New gloStream.CardioVascular.Supporting.Criterias
        Dim oCriteria As gloStream.CardioVascular.Supporting.Criteria = Nothing
        Try
            Query = "SELECT cv_mst_Id FROM CV_Criteria_MST"
            oDB.Connect(False)
            oDB.Retrive_Query(Query, oReader)
            If Not IsNothing(oReader) Then
                While oReader.Read
                    oCriteria = New gloStream.CardioVascular.Supporting.Criteria
                    oCriteria = oCV.GetCriteria(oReader.Item("cv_mst_Id"))
                    oCriterias.Add(oCriteria)
                    oCriteria = Nothing
                End While
            End If
            Return oCriterias
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Disconnect()
        End Try

    End Function

    Private Sub FillAllRisk(ByVal Risks As Supporting.PatientDetails)

        C1PatientRisk.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns
        C1PatientRisk.Clear(C1.Win.C1FlexGrid.ClearFlags.All)
        C1PatientRisk.Cols.Count = Col_Risk_Count
        C1PatientRisk.Rows.Count = 0
        C1PatientRisk.Rows.Fixed = 0

        Dim csCriteria As C1.Win.C1FlexGrid.CellStyle '= C1PatientRisk.Styles.Add("csCriteria")
        Try
            If (C1PatientRisk.Styles.Contains("csCriteria")) Then
                csCriteria = C1PatientRisk.Styles("csCriteria")
            Else
                csCriteria = C1PatientRisk.Styles.Add("csCriteria")
                csCriteria.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD 'New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CType((0), Byte)))
                csCriteria.BackColor = Color.FromArgb(145, 175, 225)
                csCriteria.ForeColor = Color.White
                csCriteria.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.LeftTop
                csCriteria.ImageSpacing = 2
                csCriteria.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal
                csCriteria.Border.Color = Color.FromArgb(31, 73, 125)
            End If
        Catch ex As Exception
            csCriteria = C1PatientRisk.Styles.Add("csCriteria")
            csCriteria.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD 'New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CType((0), Byte)))
            csCriteria.BackColor = Color.FromArgb(145, 175, 225)
            csCriteria.ForeColor = Color.White
            csCriteria.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.LeftTop
            csCriteria.ImageSpacing = 2
            csCriteria.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal
            csCriteria.Border.Color = Color.FromArgb(31, 73, 125)
        End Try
       

        Dim csParent As C1.Win.C1FlexGrid.CellStyle '= C1PatientRisk.Styles.Add("csParent")
        Try
            If (C1PatientRisk.Styles.Contains("csParent")) Then
                csParent = C1PatientRisk.Styles("csParent")
            Else
                csParent = C1PatientRisk.Styles.Add("csParent")
                csParent.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD 'New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CType((0), Byte)))
                csParent.BackColor = Color.FromArgb(222, 231, 250)
                csParent.ForeColor = Color.FromArgb(31, 73, 125)
                csParent.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.LeftTop
                csParent.ImageSpacing = 2
                csParent.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal
                csParent.Border.Color = Color.FromArgb(159, 181, 221)
            End If
        Catch ex As Exception
            csParent = C1PatientRisk.Styles.Add("csParent")
            csParent.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD 'New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CType((0), Byte)))
            csParent.BackColor = Color.FromArgb(222, 231, 250)
            csParent.ForeColor = Color.FromArgb(31, 73, 125)
            csParent.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.LeftTop
            csParent.ImageSpacing = 2
            csParent.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal
            csParent.Border.Color = Color.FromArgb(159, 181, 221)
        End Try
      

        Dim csChild As C1.Win.C1FlexGrid.CellStyle '= C1PatientRisk.Styles.Add("csChild")
        Try
            If (C1PatientRisk.Styles.Contains("csChild")) Then
                csChild = C1PatientRisk.Styles("csChild")
            Else
                csChild = C1PatientRisk.Styles.Add("csChild")
                csChild.Font = gloGlobal.clsgloFont.gFont_SMALL 'New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CType((0), Byte)))
                csChild.BackColor = Color.FromArgb(240, 247, 255)
                csChild.ForeColor = Color.FromArgb(31, 73, 125)
                csChild.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.LeftTop
                csChild.ImageSpacing = 2
                csChild.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal
            End If
        Catch ex As Exception
            csChild = C1PatientRisk.Styles.Add("csChild")
            csChild.Font = gloGlobal.clsgloFont.gFont_SMALL 'New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CType((0), Byte)))
            csChild.BackColor = Color.FromArgb(240, 247, 255)
            csChild.ForeColor = Color.FromArgb(31, 73, 125)
            csChild.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.LeftTop
            csChild.ImageSpacing = 2
            csChild.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal
        End Try
       

        Dim csLastChild As C1.Win.C1FlexGrid.CellStyle '= C1PatientRisk.Styles.Add("csLastChild")

        Try
            If (C1PatientRisk.Styles.Contains("csLastChild")) Then
                csLastChild = C1PatientRisk.Styles("csLastChild")
            Else
                csLastChild = C1PatientRisk.Styles.Add("csLastChild")
                csLastChild.Font = gloGlobal.clsgloFont.gFont_SMALL 'New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CType((0), Byte)))
                csLastChild.BackColor = Color.FromArgb(240, 247, 255)
                csLastChild.ForeColor = Color.FromArgb(31, 73, 125)
                csLastChild.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.LeftTop
                csLastChild.ImageSpacing = 2
                csLastChild.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal
                csLastChild.Border.Color = Color.FromArgb(31, 73, 125)
            End If
        Catch ex As Exception
            csLastChild = C1PatientRisk.Styles.Add("csLastChild")
            csLastChild.Font = gloGlobal.clsgloFont.gFont_SMALL 'New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CType((0), Byte)))
            csLastChild.BackColor = Color.FromArgb(240, 247, 255)
            csLastChild.ForeColor = Color.FromArgb(31, 73, 125)
            csLastChild.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.LeftTop
            csLastChild.ImageSpacing = 2
            csLastChild.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal
            csLastChild.Border.Color = Color.FromArgb(31, 73, 125)
        End Try
 

        C1PatientRisk.Tree.Column = Col_Risk_CategoryID
        C1PatientRisk.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
        C1PatientRisk.Tree.Indent = 18
        'C1PatientRisk.Tree.LineStyle = Drawing2D.DashStyle.Dot
        C1PatientRisk.Tree.LineColor = Color.Transparent

        C1PatientRisk.AllowEditing = False

        Dim _Width As Integer = C1PatientRisk.Width
        C1PatientRisk.Cols(Col_Risk_CategoryID).Width = _Width

        Try
            If Not IsNothing(Risks) Then
                For i As Integer = 1 To Risks.Count

                    Dim oChild As C1.Win.C1FlexGrid.Node
                    Dim CriteriaRowIndex As Integer
                    Dim ParentRowIndex As Integer
                    Dim ChildRowIndex As Integer

                    If C1PatientRisk.Rows.Count > 0 Then
                        C1PatientRisk.SetCellStyle(C1PatientRisk.Rows.Count - 1, Col_Risk_CategoryID, "csLastChild")
                    End If

                    C1PatientRisk.Rows.Add()
                    CriteriaRowIndex = C1PatientRisk.Rows.Count - 1 ''This Will be Parent Row Index for next Child in this loop.
                    C1PatientRisk.Rows(CriteriaRowIndex).IsNode = True
                    C1PatientRisk.Rows(CriteriaRowIndex).ImageAndText = True
                    C1PatientRisk.Rows(CriteriaRowIndex).Node.Image = ImageList.Images(0)
                    C1PatientRisk.Rows(CriteriaRowIndex).Node.Level = 0
                    C1PatientRisk.Rows(CriteriaRowIndex).Node.Data = Risks(i).CriteriaName.ToString
                    C1PatientRisk.Rows(CriteriaRowIndex).Node.Key = ""
                    C1PatientRisk.Rows(CriteriaRowIndex).Height = RowHeight
                    C1PatientRisk.SetCellStyle(CriteriaRowIndex, Col_Risk_CategoryID, "csCriteria")

                    '' DEMOGRAPHIC ''Insert Demographic Node.. Even if we don't know whether it will having ChileNodes Or Not
                    oChild = C1PatientRisk.Rows(CriteriaRowIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, "Demographic", "Demographic", Nothing)
                    ParentRowIndex = oChild.Row.Index
                    C1PatientRisk.Rows(ParentRowIndex).Height = RowHeight
                    C1PatientRisk.Rows(ParentRowIndex).Node.Image = ImageList.Images(2)
                    C1PatientRisk.SetCellStyle(ParentRowIndex, Col_Risk_CategoryID, "csParent")

                    ''SHOW AGE
                    If Risks(i).Age <> 0 Then
                        oChild = C1PatientRisk.Rows(ParentRowIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, "Age : " & Risks(i).Age.ToString, "", Nothing)
                        ChildRowIndex = oChild.Row.Index
                        C1PatientRisk.Rows(ChildRowIndex).Height = RowHeight
                        C1PatientRisk.Rows(ChildRowIndex).Node.Image = ImageList.Images(7)
                        C1PatientRisk.SetCellStyle(ChildRowIndex, Col_Risk_CategoryID, "csChild")
                    End If
                    ''SHOW GENDER
                    If Risks(i).Gender <> "" Then
                        oChild = C1PatientRisk.Rows(ParentRowIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, "Gender : " & Risks(i).Gender.ToString, "", Nothing)
                        ChildRowIndex = oChild.Row.Index
                        C1PatientRisk.Rows(ChildRowIndex).Height = RowHeight
                        C1PatientRisk.Rows(ChildRowIndex).Node.Image = ImageList.Images(7)
                        C1PatientRisk.SetCellStyle(ChildRowIndex, Col_Risk_CategoryID, "csChild")
                    End If

                    '' VITALS ''Insert Vital Node.. Even if we don't know whether it will having ChileNodes Or Not
                    oChild = C1PatientRisk.Rows(CriteriaRowIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, "Vitals", "Vitals", Nothing)
                    ParentRowIndex = oChild.Row.Index
                    C1PatientRisk.Rows(ParentRowIndex).Height = RowHeight
                    C1PatientRisk.Rows(ParentRowIndex).Node.Image = ImageList.Images(1)
                    C1PatientRisk.SetCellStyle(ParentRowIndex, Col_Risk_CategoryID, "csParent")

                    ''SHOW HEIGHT
                    If Risks(i).Height <> "" Then
                        oChild = C1PatientRisk.Rows(ParentRowIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, "Height : " & Risks(i).Height.ToString, "", Nothing)
                        ChildRowIndex = oChild.Row.Index
                        C1PatientRisk.Rows(ChildRowIndex).Height = RowHeight
                        C1PatientRisk.Rows(ChildRowIndex).Node.Image = ImageList.Images(7)
                        C1PatientRisk.SetCellStyle(ChildRowIndex, Col_Risk_CategoryID, "csChild")
                    End If
                    ''SHOW WEIGHT
                    If Risks(i).WeightInlbs <> 0 Then
                        oChild = C1PatientRisk.Rows(ParentRowIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, "Weight : " & Risks(i).WeightInlbs.ToString & "  lbs", "", Nothing)
                        ChildRowIndex = oChild.Row.Index
                        C1PatientRisk.Rows(ChildRowIndex).Height = RowHeight
                        C1PatientRisk.Rows(ChildRowIndex).Node.Image = ImageList.Images(7)
                        C1PatientRisk.SetCellStyle(ChildRowIndex, Col_Risk_CategoryID, "csChild")
                    End If
                    ''SHOW PULSE
                    If Risks(i).Pulse <> 0 Then
                        oChild = C1PatientRisk.Rows(ParentRowIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, "Pulse : " & Risks(i).Pulse.ToString, "", Nothing)
                        ChildRowIndex = oChild.Row.Index
                        C1PatientRisk.Rows(ChildRowIndex).Height = RowHeight
                        C1PatientRisk.Rows(ChildRowIndex).Node.Image = ImageList.Images(7)
                        C1PatientRisk.SetCellStyle(ChildRowIndex, Col_Risk_CategoryID, "csChild")
                    End If
                    ''SHOW PULSE_OX
                    If Risks(i).PulseOX <> 0 Then
                        oChild = C1PatientRisk.Rows(ParentRowIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, "PulseOX : " & Risks(i).PulseOX.ToString, "", Nothing)
                        ChildRowIndex = oChild.Row.Index
                        C1PatientRisk.Rows(ChildRowIndex).Height = RowHeight
                        C1PatientRisk.Rows(ChildRowIndex).Node.Image = ImageList.Images(7)
                        C1PatientRisk.SetCellStyle(ChildRowIndex, Col_Risk_CategoryID, "csChild")
                    End If
                    ''SHOW BP SITTING MAX
                    If Risks(i).BPSittingMaximum <> 0 Then
                        oChild = C1PatientRisk.Rows(ParentRowIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, "BP Sitting Maximum : " & Risks(i).BPSittingMaximum.ToString, "", Nothing)
                        ChildRowIndex = oChild.Row.Index
                        C1PatientRisk.Rows(ChildRowIndex).Height = RowHeight
                        C1PatientRisk.Rows(ChildRowIndex).Node.Image = ImageList.Images(7)
                        C1PatientRisk.SetCellStyle(ChildRowIndex, Col_Risk_CategoryID, "csChild")
                    End If
                    ''SHOW BP SITTING MIN
                    If Risks(i).BPSittingMinimum <> 0 Then
                        oChild = C1PatientRisk.Rows(ParentRowIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, "BP Sitting Minimum : " & Risks(i).BPSittingMinimum.ToString, "", Nothing)
                        ChildRowIndex = oChild.Row.Index
                        C1PatientRisk.Rows(ChildRowIndex).Height = RowHeight
                        C1PatientRisk.Rows(ChildRowIndex).Node.Image = ImageList.Images(7)
                        C1PatientRisk.SetCellStyle(ChildRowIndex, Col_Risk_CategoryID, "csChild")
                    End If
                    ''SHOW BP STANDING MAX
                    If Risks(i).BPStandingMaximum <> 0 Then
                        oChild = C1PatientRisk.Rows(ParentRowIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, "BP Standing Maximum : " & Risks(i).BPStandingMaximum.ToString, "", Nothing)
                        ChildRowIndex = oChild.Row.Index
                        C1PatientRisk.Rows(ChildRowIndex).Height = RowHeight
                        C1PatientRisk.Rows(ChildRowIndex).Node.Image = ImageList.Images(7)
                        C1PatientRisk.SetCellStyle(ChildRowIndex, Col_Risk_CategoryID, "csChild")
                    End If
                    ''SHOW BP STANDING MIN
                    If Risks(i).BPStandingMinimum <> 0 Then
                        oChild = C1PatientRisk.Rows(ParentRowIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, "BP Standing Minimum : " & Risks(i).BPStandingMinimum.ToString, "", Nothing)
                        ChildRowIndex = oChild.Row.Index
                        C1PatientRisk.Rows(ChildRowIndex).Height = RowHeight
                        C1PatientRisk.Rows(ChildRowIndex).Node.Image = ImageList.Images(7)
                        C1PatientRisk.SetCellStyle(ChildRowIndex, Col_Risk_CategoryID, "csChild")
                    End If
                    ''SHOW BMI
                    If Risks(i).BMI <> 0 Then
                        oChild = C1PatientRisk.Rows(ParentRowIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, "BMI : " & Risks(i).BMI.ToString, "", Nothing)
                        ChildRowIndex = oChild.Row.Index
                        C1PatientRisk.Rows(ChildRowIndex).Height = RowHeight
                        C1PatientRisk.Rows(ChildRowIndex).Node.Image = ImageList.Images(7)
                        C1PatientRisk.SetCellStyle(ChildRowIndex, Col_Risk_CategoryID, "csChild")
                    End If
                    ''SHOW TEMPERATURE
                    If Risks(i).TempratureInF <> 0 Then
                        oChild = C1PatientRisk.Rows(ParentRowIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, "Temperature : " & Risks(i).TempratureInF.ToString & "  F", "", Nothing)
                        ChildRowIndex = oChild.Row.Index
                        C1PatientRisk.Rows(ChildRowIndex).Height = RowHeight
                        C1PatientRisk.Rows(ChildRowIndex).Node.Image = ImageList.Images(7)
                        C1PatientRisk.SetCellStyle(ChildRowIndex, Col_Risk_CategoryID, "csChild")
                    End If

                    ''Remove Demographic / Vitals Node if it has no ChildNodes
                    For inx As Integer = C1PatientRisk.Rows.Count - 1 To 1 Step -1
                        If C1PatientRisk.Rows(inx).Node.Level = 1 Then
                            If C1PatientRisk.Rows(inx).Node.Children = 0 Then
                                C1PatientRisk.Rows(inx).Node.RemoveNode()
                            End If
                        End If
                    Next

                    For j As Integer = 1 To Risks(i).OtherDetails.Count
                        Dim c1Index As Integer = 0
                        Dim ParentFound As Boolean = False
                        Dim NodeDataString As String = ""

                        Select Case Risks(i).OtherDetails(j).DetailType

                            Case Supporting.enumDetailType.History
                                For c1Index = CriteriaRowIndex To C1PatientRisk.Rows.Count - 1
                                    If C1PatientRisk.Rows(c1Index).Node.Level = 1 Then
                                        If C1PatientRisk.Rows(c1Index).Node.Data = "History" Then
                                            ParentRowIndex = c1Index
                                            ParentFound = True
                                            Exit For
                                        End If
                                    End If
                                Next

                                If Not ParentFound Then
                                    oChild = C1PatientRisk.Rows(CriteriaRowIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, "History", "", Nothing)
                                    ParentRowIndex = oChild.Row.Index
                                    C1PatientRisk.Rows(ParentRowIndex).Height = RowHeight
                                    C1PatientRisk.Rows(ParentRowIndex).Node.Image = ImageList.Images(3)
                                    C1PatientRisk.SetCellStyle(ParentRowIndex, Col_Risk_CategoryID, "csParent")
                                End If

                                NodeDataString = Risks(i).OtherDetails(j).CategoryName & " : " & Risks(i).OtherDetails(j).ItemName
                                oChild = C1PatientRisk.Rows(ParentRowIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, NodeDataString, "", Nothing)
                                ChildRowIndex = oChild.Row.Index
                                C1PatientRisk.Rows(ChildRowIndex).Height = RowHeight
                                C1PatientRisk.Rows(ChildRowIndex).Node.Image = ImageList.Images(7)
                                C1PatientRisk.SetCellStyle(ChildRowIndex, Col_Risk_CategoryID, "csChild")

                            Case Supporting.enumDetailType.Medication
                                For c1Index = CriteriaRowIndex To C1PatientRisk.Rows.Count - 1
                                    If C1PatientRisk.Rows(c1Index).Node.Level = 1 Then
                                        If C1PatientRisk.Rows(c1Index).Node.Data = "Drugs" Then
                                            ParentRowIndex = c1Index
                                            ParentFound = True
                                            Exit For
                                        End If
                                    End If
                                Next

                                If Not ParentFound Then
                                    oChild = C1PatientRisk.Rows(CriteriaRowIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, "Drugs", "", Nothing)
                                    ParentRowIndex = oChild.Row.Index
                                    C1PatientRisk.Rows(ParentRowIndex).Height = RowHeight
                                    C1PatientRisk.Rows(ParentRowIndex).Node.Image = ImageList.Images(4)
                                    C1PatientRisk.SetCellStyle(ParentRowIndex, Col_Risk_CategoryID, "csParent")
                                End If

                                NodeDataString = Risks(i).OtherDetails(j).CategoryName & " : " & Risks(i).OtherDetails(j).ItemName
                                oChild = C1PatientRisk.Rows(ParentRowIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, NodeDataString, "", Nothing)
                                ChildRowIndex = oChild.Row.Index
                                C1PatientRisk.Rows(ChildRowIndex).Height = RowHeight
                                C1PatientRisk.Rows(ChildRowIndex).Node.Image = ImageList.Images(7)
                                C1PatientRisk.SetCellStyle(ChildRowIndex, Col_Risk_CategoryID, "csChild")

                            Case Supporting.enumDetailType.Lab
                                For c1Index = CriteriaRowIndex To C1PatientRisk.Rows.Count - 1
                                    If C1PatientRisk.Rows(c1Index).Node.Level = 1 Then
                                        If C1PatientRisk.Rows(c1Index).Node.Data = "Labs" Then
                                            ParentRowIndex = c1Index
                                            ParentFound = True
                                            Exit For
                                        End If
                                    End If
                                Next

                                If Not ParentFound Then
                                    oChild = C1PatientRisk.Rows(CriteriaRowIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, "Labs", "", Nothing)
                                    ParentRowIndex = oChild.Row.Index
                                    C1PatientRisk.Rows(ParentRowIndex).Height = RowHeight
                                    C1PatientRisk.Rows(ParentRowIndex).Node.Image = ImageList.Images(5)
                                    C1PatientRisk.SetCellStyle(ParentRowIndex, Col_Risk_CategoryID, "csParent")
                                End If

                                NodeDataString = Risks(i).OtherDetails(j).CategoryName & " : " & Risks(i).OtherDetails(j).ItemName
                                oChild = C1PatientRisk.Rows(ParentRowIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, NodeDataString, "", Nothing)
                                ChildRowIndex = oChild.Row.Index
                                C1PatientRisk.Rows(ChildRowIndex).Height = RowHeight
                                C1PatientRisk.Rows(ChildRowIndex).Node.Image = ImageList.Images(7)
                                C1PatientRisk.SetCellStyle(ChildRowIndex, Col_Risk_CategoryID, "csChild")

                            Case Supporting.enumDetailType.Order
                                For c1Index = CriteriaRowIndex To C1PatientRisk.Rows.Count - 1
                                    If C1PatientRisk.Rows(c1Index).Node.Level = 1 Then
                                        If C1PatientRisk.Rows(c1Index).Node.Data = "Orders" Then
                                            ParentRowIndex = c1Index
                                            ParentFound = True
                                            Exit For
                                        End If
                                    End If
                                Next

                                If Not ParentFound Then
                                    oChild = C1PatientRisk.Rows(CriteriaRowIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, "Orders", "", Nothing)
                                    ParentRowIndex = oChild.Row.Index
                                    C1PatientRisk.Rows(ParentRowIndex).Height = RowHeight
                                    C1PatientRisk.Rows(ParentRowIndex).Node.Image = ImageList.Images(6)
                                    C1PatientRisk.SetCellStyle(ParentRowIndex, Col_Risk_CategoryID, "csParent")
                                End If

                                NodeDataString = Risks(i).OtherDetails(j).CategoryName & " : " & Risks(i).OtherDetails(j).ItemName
                                oChild = C1PatientRisk.Rows(ParentRowIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, NodeDataString, "", Nothing)
                                ChildRowIndex = oChild.Row.Index
                                C1PatientRisk.Rows(ChildRowIndex).Height = RowHeight
                                C1PatientRisk.Rows(ChildRowIndex).Node.Image = ImageList.Images(7)
                                C1PatientRisk.SetCellStyle(ChildRowIndex, Col_Risk_CategoryID, "csChild")

                            Case Supporting.enumDetailType.None
                        End Select
                    Next  ''End OtherDetails
                Next ''End oRisks
            End If

            ''Diselect Row
            C1PatientRisk.Row = -1
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tls_PatientRisk_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tls_PatientRisk.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Close"
                gloWord.WordDialogBoxBackgroundCloser.Close(Me)
                'Me.Close()
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.CloseTransaction, gloAuditTrail.ActivityType.Close, "Closed the CardioVascular Patient Risk Form", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101011
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.CloseTransaction, gloAuditTrail.ActivityType.Close, "Closed the CardioVascular Patient Risk Form", mPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Other, "Closed the Patient Risk Form", gstrLoginName, gstrClientMachineName, mPatientID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
        End Select
    End Sub

    Private Sub FillLatestPatientHistory()
        'Dim oCriterias As Supporting.Criterias
        'Dim oPatientDetail As Supporting.PatientDetail
        '20090818
        C1FamilyHistory.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        C1FamilyHistory.Cols.Count = 2
        C1FamilyHistory.Rows.Count = 1
        C1FamilyHistory.Rows.Fixed = 1
        C1FamilyHistory.SetData(0, 0, "Family History")
        C1FamilyHistory.SetData(0, 1, "Comments")
        C1FamilyHistory.Cols(0).Width = C1FamilyHistory.Width / 2 - 5
        C1FamilyHistory.Cols(1).Width = C1FamilyHistory.Width / 2 - 5
        '20090818
        C1SocialHistory.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        C1SocialHistory.Cols.Count = 2
        C1SocialHistory.Rows.Count = 1
        C1SocialHistory.Rows.Fixed = 1
        C1SocialHistory.SetData(0, 0, "Social History")
        C1SocialHistory.SetData(0, 1, "Comments")
        C1SocialHistory.Cols(0).Width = C1SocialHistory.Width / 2 - 5
        C1SocialHistory.Cols(1).Width = C1SocialHistory.Width / 2 - 5
        If (IsNothing(oPatientDetail) = False) Then
            oPatientDetail.Dispose()
            oPatientDetail = Nothing
        End If
        '' Get Latest Patient History of The Patient
        oPatientDetail = oCV.GetLatestPatientHistory(mPatientID)
        If IsNothing(oPatientDetail) = False Then
            For index As Integer = 1 To oPatientDetail.OtherDetails.Count
                If oPatientDetail.OtherDetails(index).DetailType = Supporting.enumDetailType.History Then
                    If oPatientDetail.OtherDetails(index).CategoryName.StartsWith("Family") = True Then
                        '' Add The Family His
                        With C1FamilyHistory
                            Dim row As Integer = .Rows.Add().Index
                            .SetData(row, 0, oPatientDetail.OtherDetails(index).ItemName) ''  HistoryItem
                            .SetData(row, 1, oPatientDetail.OtherDetails(index).Result1) '' Comments
                        End With
                    ElseIf oPatientDetail.OtherDetails(index).CategoryName.StartsWith("Social") = True Then
                        With C1SocialHistory
                            Dim row As Integer = .Rows.Add().Index
                            .SetData(row, 0, oPatientDetail.OtherDetails(index).ItemName) '' History Item
                            .SetData(row, 1, oPatientDetail.OtherDetails(index).Result1) '' Comments
                        End With
                    End If
                End If

            Next
        End If


    End Sub

    Private Sub frmCV_PatientRisk_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            pnlFamilyHistory.Width = Me.Width / 2
            FillLatestPatientHistory()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub C1PatientRisk_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1PatientRisk.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub C1SocialHistory_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1SocialHistory.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub C1FamilyHistory_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1FamilyHistory.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub


    ''' <summary>
    ''' Property writen for Patient Context
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return mPatientID  'Curent patient variable(Local variable) for this module 
        End Get
    End Property
End Class
