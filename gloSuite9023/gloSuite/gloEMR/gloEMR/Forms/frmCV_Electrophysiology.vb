Imports gloUserControlLibrary
Imports System.Data.SqlClient



Public Class frmCV_Electrophysiology
    Implements IPatientContext
    Enum Associates
        CPT = 1
        Provider = 2
        User = 3
        Procedure = 4
    End Enum



    Private WithEvents _PatientStrip As gloUC_PatientStrip = Nothing
    Private blnIsLoaded As Boolean = False
    Private mPatientID As Int64 = 0
    Private mProcedureDate As Date
    Private mVisitID As Int64 = 0
    Public strSearch As String = String.Empty
    Public AssociateID As Associates = Associates.CPT





    Public Sub New(ByVal Patientid As Int64, ByVal PDate As Date, ByVal Visitid As Int64)

        InitializeComponent()
        mPatientID = Patientid  '12300000
        mProcedureDate = PDate '"2/20/2009 12:00:00 AM"
        mVisitID = Visitid  '123100000
        blnIsLoaded = False
        DTPicker1.Value = mProcedureDate
        blnIsLoaded = True
    End Sub

    Private Sub frmCV_Electrophysiology_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub frmCV_Electrophysiology_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If IsNothing(_PatientStrip) = False Then
            pnl_Main.Controls.Remove(_PatientStrip)
            _PatientStrip.Dispose()
            _PatientStrip = Nothing
        End If
    End Sub

    Private Sub frmCV_Electrophysiology_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            mnuRemoveCPT.Visible = False
            mnuRemoveProcedure.Visible = False
            mnuRemoveUser.Visible = False
            txtsearchrht.Focus()

            If (trvProcedureDate.GetNodeCount(True)) > 0 Then                   'Select default 1st node
                trvProcedureDate.SelectedNode = trvProcedureDate.Nodes.Item(0)
            End If

            If IsNothing(_PatientStrip) = False Then
                pnl_Main.Controls.Remove(_PatientStrip)
                _PatientStrip.Dispose()
                _PatientStrip = Nothing
            End If

            ''Add patientStrip
            _PatientStrip = New gloUC_PatientStrip
            _PatientStrip.ShowDetail(mPatientID, gloUC_PatientStrip.enumFormName.PatientEducation)
            _PatientStrip.Dock = DockStyle.Top
            _PatientStrip.BringToFront()
            _PatientStrip.Padding = New Padding(3, 0, 3, 0)
            pnl_Main.Controls.Add(_PatientStrip)

            ''get cv_Electrophysiologydata
            Dim dt As DataTable = GetCVElectroPhysiologyData(mPatientID, mProcedureDate, mVisitID)

            FillProcedureDateTreeView(mProcedureDate)

            'Fill Right Panel Content

            'Populate By Default ICD9 data in Associates' TreeView
            Dim rootnode As myTreeNode = New myTreeNode("CPT", -1)
            rootnode.ImageIndex = 0
            rootnode.SelectedImageIndex = 0
            trvAssociates.Nodes.Add(rootnode)

            FillAssociates(Associates.CPT, strSearch)

            Try
                gloPatient.gloPatient.GetWindowTitle(Me, mPatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
            dt.Dispose()
            dt = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString)
        End Try

    End Sub

    Public Function GetCVElectroPhysiologyData(ByVal mPatientID As Int64, ByVal mProcedureDate As Date, ByVal mVisitID As Long) As DataTable
        Try
            Dim dt As New DataTable
            Dim sqladpt As SqlDataAdapter
            Dim conn As SqlConnection = New SqlConnection(GetConnectionString)

            Dim strQuery As String = "select isnull(nElectroPhysiologyID,0) as ElectroPhysiologyID, isnull(nPatientID,0) as PatientID, isnull(nExamID,0) as ExamID, isnull(nVisitID,0) as VisitID, isnull(nClinicID,0) as ClinicID, isnull(dtProcedureDate,0) as ProcedureDate, isnull(sCPTCode,'') as CPTCode, isnull(sProcedures,'') as Procedures,isnull(sUserProvider,'') as UserProvider from CV_ElectroPhysiology where nPatientID =  " & mPatientID & " and nVisitID = " & mVisitID & " and dtProcedureDate='" & mProcedureDate & "' "

            Dim cmd As SqlCommand = New SqlCommand(strQuery, conn)
            sqladpt = New SqlDataAdapter
            sqladpt.SelectCommand = cmd
            sqladpt.Fill(dt)
            sqladpt.Dispose()
            sqladpt = Nothing
            conn.Close()
            conn.Dispose()
            conn = Nothing

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString)
            Return Nothing
        End Try

    End Function
    Public Function GetCVElectro_CPTData(ByVal mPatientID As Int64, ByVal mProcedureDate As Date, ByVal mVisitID As Long) As DataTable
        Try
            Dim dt As New DataTable
            Dim sqladpt As SqlDataAdapter
            Dim conn As SqlConnection = New SqlConnection(GetConnectionString)

            Dim strQuery As String = "select isnull(sCPTCode,'') as CPTCode from CV_ElectroPhysiology where nPatientID =  " & mPatientID & " and nVisitID = " & mVisitID & " and dtProcedureDate='" & mProcedureDate & "' "

            Dim cmd As SqlCommand = New SqlCommand(strQuery, conn)
            sqladpt = New SqlDataAdapter
            sqladpt.SelectCommand = cmd
            sqladpt.Fill(dt)
            sqladpt.Dispose()
            sqladpt = Nothing
            conn.Close()
            conn.Dispose()
            conn = Nothing

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString)
            Return Nothing
        End Try

    End Function
    Public Function GetCVElectro_ProcedureData(ByVal mPatientID As Int64, ByVal mProcedureDate As Date, ByVal mVisitID As Long) As DataTable
        Try
            Dim dt As New DataTable
            Dim sqladpt As SqlDataAdapter
            Dim conn As SqlConnection = New SqlConnection(GetConnectionString)

            Dim strQuery As String = "select isnull(sProcedures,'') as Procedures from CV_ElectroPhysiology where nPatientID =  " & mPatientID & " and nVisitID = " & mVisitID & " and dtProcedureDate='" & mProcedureDate & "' "

            Dim cmd As SqlCommand = New SqlCommand(strQuery, conn)
            sqladpt = New SqlDataAdapter
            sqladpt.SelectCommand = cmd
            sqladpt.Fill(dt)
            sqladpt.Dispose()
            sqladpt = Nothing
            conn.Close()
            conn.Dispose()
            conn = Nothing

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString)
            Return Nothing
        End Try

    End Function
    Public Function GetCVElectro_UsersData(ByVal mPatientID As Int64, ByVal mProcedureDate As Date, ByVal mVisitID As Long) As DataTable
        Try
            Dim dt As New DataTable
            Dim sqladpt As SqlDataAdapter
            Dim conn As SqlConnection = New SqlConnection(GetConnectionString)

            Dim strQuery As String = "select isnull(sUserProvider,'') as UserProvider from CV_ElectroPhysiology where nPatientID =  " & mPatientID & " and nVisitID = " & mVisitID & " and dtProcedureDate='" & mProcedureDate & "' "

            Dim cmd As SqlCommand = New SqlCommand(strQuery, conn)
            sqladpt = New SqlDataAdapter
            sqladpt.SelectCommand = cmd
            sqladpt.Fill(dt)
            sqladpt.Dispose()
            sqladpt = Nothing
            conn.Close()
            conn.Dispose()
            conn = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString)
            Return Nothing
        End Try

    End Function
    Public Function FillProcedureDateTreeView(ByVal dtPDate As Date)
        Try

            If IsNothing(dtPDate) Then
                dtPDate = Today.Date
            End If

            trvProcedureDate.Nodes.Clear()

            Dim rootnode As myTreeNode
            Dim i As Integer

            rootnode = New myTreeNode("Procedure Date: '" & dtPDate & "'", -1)
            rootnode.ImageIndex = 4
            rootnode.SelectedImageIndex = 4
            trvProcedureDate.Nodes.Add(rootnode)

            rootnode = New myTreeNode("CPT", -1)
            rootnode.ImageIndex = 0
            rootnode.SelectedImageIndex = 0
            trvProcedureDate.Nodes.Item(0).Nodes.Add(rootnode)

            Dim dtCPT As DataTable = GetCVElectro_CPTData(mPatientID, dtPDate, mVisitID)
            If IsNothing(dtCPT) Then
                DTPicker1.Enabled = False
            ElseIf dtCPT.Rows.Count > 0 Then
                DTPicker1.Enabled = False
            End If
            If dtCPT.Rows.Count > 0 Then
                For i = 0 To dtCPT.Rows.Count - 1
                    If dtCPT.Rows(i)("CPTCode") <> "" Then
                        Dim mychildnode As myTreeNode
                        Dim strCPTCOde As String = dtCPT.Rows(i)("CPTCode")
                        mychildnode = New myTreeNode(strCPTCOde, 0)
                        mychildnode.ImageIndex = 3
                        mychildnode.SelectedImageIndex = 3
                        trvProcedureDate.Nodes.Item(0).Nodes.Item(0).Nodes.Add(mychildnode)
                    End If
                Next
            End If


            rootnode = New myTreeNode("Procedure", -1)
            rootnode.ImageIndex = 2
            rootnode.SelectedImageIndex = 2
            trvProcedureDate.Nodes.Item(0).Nodes.Add(rootnode)
            Dim dtProcedure As DataTable = GetCVElectro_ProcedureData(mPatientID, dtPDate, mVisitID)
            If dtProcedure.Rows.Count > 0 Then
                For i = 0 To dtProcedure.Rows.Count - 1
                    If dtProcedure.Rows(i)("Procedures") <> "" Then
                        Dim mychildnode As myTreeNode
                        Dim strPro As String = dtProcedure.Rows(i)("Procedures")
                        mychildnode = New myTreeNode(strPro, 0)
                        mychildnode.ImageIndex = 3
                        mychildnode.SelectedImageIndex = 3
                        trvProcedureDate.Nodes.Item(0).Nodes.Item(1).Nodes.Add(mychildnode)
                    End If
                Next
            End If

            rootnode = New myTreeNode("Users", -1)
            rootnode.ImageIndex = 1
            rootnode.SelectedImageIndex = 1
            trvProcedureDate.Nodes.Item(0).Nodes.Add(rootnode)
            Dim dtUser As DataTable = GetCVElectro_UsersData(mPatientID, dtPDate, mVisitID)
            If dtUser.Rows.Count > 0 Then
                If dtUser.Rows(0)("UserProvider") <> "" Then
                    Dim strarr() As String = Split(dtUser.Rows(0)("UserProvider"), "|")
                    If strarr.Length > 0 Then
                        For i = 0 To strarr.Length - 1
                            Dim mychildnode As myTreeNode
                            Dim struser As String = strarr(i) 'dtUser.Rows(i)("UserProvider")
                            mychildnode = New myTreeNode(struser, 0)
                            mychildnode.ImageIndex = 3
                            mychildnode.SelectedImageIndex = 3
                            trvProcedureDate.Nodes.Item(0).Nodes.Item(2).Nodes.Add(mychildnode)
                        Next
                    End If
                End If
            End If
            trvProcedureDate.ExpandAll()
            trvProcedureDate.Show()
            trvProcedureDate.Select()
            dtUser.Dispose()
            dtUser = Nothing
            dtProcedure.Dispose()
            dtProcedure = Nothing
            dtCPT.Dispose()
            dtCPT = Nothing

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString)
        End Try
        Return Nothing
    End Function
    Public Function getUser()
        Try
            Dim dt As New DataTable
            Dim sqladpt As SqlDataAdapter
            Dim conn As SqlConnection = New SqlConnection(GetConnectionString)

            'Dim strQuery As String = "select isnull(sLoginName,'') as LoginName, isnull(nUserID,0 ) as UserID from User_MST "
            Dim strQuery As String = "select sLoginName ,isnull(sfirstname,'') + Space(1) + isnull(slastname,'') as LoginName , isnull(nUserID,0 ) as UserID from User_MST "

            Dim cmd As SqlCommand = New SqlCommand(strQuery, conn)
            sqladpt = New SqlDataAdapter
            sqladpt.SelectCommand = cmd
            sqladpt.Fill(dt)
            sqladpt.Dispose()
            sqladpt = Nothing
            conn.Close()
            conn.Dispose()
            conn = Nothing

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString)
            Return Nothing
        End Try

    End Function
    Public Function getProvider()
        Try
            Dim dt As New DataTable
            Dim sqladpt As SqlDataAdapter
            Dim conn As SqlConnection = New SqlConnection(GetConnectionString)

            Dim strQuery As String = "select isnull(sFirstName,'') + space(1) + isnull(sLastName,'') as ProviderName,isnull(nProviderID ,0 ) as ProviderID from Provider_MST"

            Dim cmd As SqlCommand = New SqlCommand(strQuery, conn)
            sqladpt = New SqlDataAdapter
            sqladpt.SelectCommand = cmd
            sqladpt.Fill(dt)
            sqladpt.Dispose()
            sqladpt = Nothing
            conn.Close()
            conn.Dispose()
            conn = Nothing

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString)
            Return Nothing
        End Try

    End Function
    Public Function getProcedure()
        Try
            Dim dt As New DataTable
            Dim sqladpt As SqlDataAdapter
            Dim conn As SqlConnection = New SqlConnection(GetConnectionString)

            Dim strQuery As String = "select isnull( sDescription,'') as ProDescription, isnull(nCategoryID,0) as CategoryID from Category_MST where sCategoryType='Procedures'"

            Dim cmd As SqlCommand = New SqlCommand(strQuery, conn)
            sqladpt = New SqlDataAdapter
            sqladpt.SelectCommand = cmd
            sqladpt.Fill(dt)
            sqladpt.Dispose()
            sqladpt = Nothing
            conn.Close()
            conn.Dispose()
            conn = Nothing

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString)
            Return Nothing
        End Try

    End Function
    Public Function getCPT()
        Try
            Dim dt As New DataTable
            Dim sqladpt As SqlDataAdapter
            Dim conn As SqlConnection = New SqlConnection(GetConnectionString)

            Dim strQuery As String = "select isnull(sCPTCode,0)+space(1)+isnull(sDescription,'') as CPT, isnull(sCPTCode,0) as CPTCode from CPT_MST"

            Dim cmd As SqlCommand = New SqlCommand(strQuery, conn)
            sqladpt = New SqlDataAdapter
            sqladpt.SelectCommand = cmd
            sqladpt.Fill(dt)
            sqladpt.Dispose()
            sqladpt = Nothing
            conn.Close()
            conn.Dispose()
            conn = Nothing

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString)
            Return Nothing
        End Try

    End Function


    Public Function FillAssociates(ByVal id As Associates, ByVal strQuerySearch As String)

        Try
            If Not IsNothing(trvAssociates.Nodes.Item(0)) Then
                trvAssociates.Nodes.Item(0).Nodes.Clear()
            End If


            trvAssociates.Nodes.Clear()
            'txtsearchAssociates.Text = ""
            Dim rootnode As myTreeNode = Nothing

            pnl_btnCPT.Dock = DockStyle.Bottom
            btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            btnCPT.BackgroundImageLayout = ImageLayout.Stretch


            pnl_btnUser.Dock = DockStyle.Bottom
            btnUser.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            btnUser.BackgroundImageLayout = ImageLayout.Stretch


            pnl_btnProcedure.Dock = DockStyle.Bottom
            btnProcedure.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            btnProcedure.BackgroundImageLayout = ImageLayout.Stretch


            pnl_btnProvider.Dock = DockStyle.Bottom
            btnProvider.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            btnProvider.BackgroundImageLayout = ImageLayout.Stretch

            If id = Associates.CPT Then
                AssociateID = Associates.CPT


                pnl_btnCPT.Dock = DockStyle.Top
                pnl_txtsearchrht.BringToFront()
                pnl_trvAssociates.BringToFront()
                With btnCPT
                    .ForeColor = Color.FromArgb(31, 73, 125)
                    .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                    .BackgroundImageLayout = ImageLayout.Stretch
                End With
                rootnode = New myTreeNode("CPT", -1)
                rootnode.ImageIndex = 0
                rootnode.SelectedImageIndex = 0

            ElseIf id = Associates.Procedure Then
                AssociateID = Associates.Procedure
                pnl_btnProcedure.Dock = DockStyle.Top
                pnl_txtsearchrht.BringToFront()
                pnl_trvAssociates.BringToFront()
                With btnProcedure
                    .ForeColor = Color.FromArgb(31, 73, 125)
                    .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                    .BackgroundImageLayout = ImageLayout.Stretch
                End With

                rootnode = New myTreeNode("Procedure", -1)
                rootnode.ImageIndex = 2
                rootnode.SelectedImageIndex = 2
            ElseIf id = Associates.Provider Then
                AssociateID = Associates.Provider
                pnl_btnProvider.Dock = DockStyle.Top
                pnl_txtsearchrht.BringToFront()
                pnl_trvAssociates.BringToFront()
                With btnProvider
                    .ForeColor = Color.FromArgb(31, 73, 125)
                    .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                    .BackgroundImageLayout = ImageLayout.Stretch
                End With

                rootnode = New myTreeNode("Provider", -1)
                rootnode.ImageIndex = 1
                rootnode.SelectedImageIndex = 1
                '' Fill Tags
            ElseIf id = Associates.User Then
                AssociateID = Associates.User
                pnl_btnUser.Dock = DockStyle.Top
                pnl_txtsearchrht.BringToFront()
                pnl_trvAssociates.BringToFront()
                With btnUser
                    .ForeColor = Color.FromArgb(31, 73, 125)
                    .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                    .BackgroundImageLayout = ImageLayout.Stretch
                End With
                rootnode = New myTreeNode("User", -1)
                rootnode.ImageIndex = 1
                rootnode.SelectedImageIndex = 1
            End If
            trvAssociates.Nodes.Add(rootnode)

            Dim dt As DataTable = FillControls(id, strSearch)

            'search text
            ' Dim tdt As DataTable
            Dim dv As New DataView(dt.Copy())

            If id = Associates.CPT Then
                dv.RowFilter = "CPT Like '%" & strQuerySearch & "%'"
            ElseIf id = Associates.User Then
                dv.RowFilter = "LoginName Like '%" & strQuerySearch & "%'"
            ElseIf id = Associates.Provider Then
                dv.RowFilter = "ProviderName Like '%" & strQuerySearch & "%'"
            ElseIf id = Associates.Procedure Then
                dv.RowFilter = "ProDescription Like '%" & strQuerySearch & "%'"
            End If
            'search text complete

            Dim dtAssociates As DataTable = dv.ToTable

            trvAssociates.BeginUpdate()
            trvAssociates.Visible = False
            trvAssociates.Nodes(0).Nodes.Clear()

            Dim i As Integer

            '   Dim myTempNode As myTreeNode

            For i = 0 To dtAssociates.Rows.Count - 1
                ' Dim mychildnode As myTreeNode
                If id = Associates.CPT Then
                    'trvAssociates.Nodes.Item(0).Nodes.Add(New myTreeNode(dt.Rows(i)(1), dt.Rows(i)(0), CType(dt.Rows(i)(2), String)))
                    Dim mynode As New myTreeNode(CType(dtAssociates.Rows(i)("CPT"), String), 0)
                    mynode.ImageIndex = 0
                    mynode.SelectedImageIndex = 0
                    trvAssociates.Nodes.Item(0).Nodes.Add(mynode)
                ElseIf id = Associates.User Then
                    Dim mynode As New myTreeNode(CType(dtAssociates.Rows(i)(0), String) & "-" & CType(dtAssociates.Rows(i)("LoginName"), String), dtAssociates.Rows(i)("UserID"))
                    mynode.Tag = CType(dtAssociates.Rows(i)(0), String)
                    mynode.ImageIndex = 1
                    mynode.SelectedImageIndex = 1
                    trvAssociates.Nodes.Item(0).Nodes.Add(mynode)
                ElseIf id = Associates.Provider Then
                    Dim mynode As New myTreeNode(CType(dtAssociates.Rows(i)("ProviderName"), String), dtAssociates.Rows(i)("ProviderID"))
                    mynode.ImageIndex = 1
                    mynode.SelectedImageIndex = 1
                    trvAssociates.Nodes.Item(0).Nodes.Add(mynode)
                ElseIf id = Associates.Procedure Then
                    Dim mynode As New myTreeNode(CType(dtAssociates.Rows(i)("ProDescription"), String), dtAssociates.Rows(i)("CategoryID"))
                    mynode.ImageIndex = 2
                    mynode.SelectedImageIndex = 2
                    trvAssociates.Nodes.Item(0).Nodes.Add(mynode)

                End If
                'rootnode.Nodes.Add(dt.Rows(i)(1))
            Next
            trvAssociates.Visible = True
            trvAssociates.ExpandAll()
            trvAssociates.Select()
            trvAssociates.SelectedNode = trvAssociates.Nodes.Item(0)

            txtsearchrht.Focus()
            dtAssociates.Dispose()
            dtAssociates = Nothing
            dv.Dispose()
            dv = Nothing
            dt.Dispose()
            dt = Nothing
            

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString)
        Finally
            trvAssociates.EndUpdate()
        End Try
        Return Nothing
    End Function

    Public Function FillControls(ByVal id, ByVal strSearch) As DataTable

        Dim dt As DataTable = Nothing

        Try

            If id = Associates.CPT Then
                dt = getCPT()
            ElseIf id = Associates.User Then
                dt = getUser()
            ElseIf id = Associates.Procedure Then
                dt = getProcedure()
            ElseIf id = Associates.Provider Then
                dt = getProvider()
            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

        Return dt

    End Function

    Private Sub tlsbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsbtnClose.Click
        Me.Close()
        'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Other, "Closed the electro physiology form", gstrLoginName, gstrClientMachineName, mPatientID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
        ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Close, "Closed the electro physiology form", gloAuditTrail.ActivityOutCome.Success)
        ''Added Rahul P on 20101011
        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Close, "Closed the electro physiology form", mPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        ''
    End Sub

    Private Sub btnCPT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCPT.Click
        ''******************************Ojeswini02282009*******************************************************************
        'For btn Hover and Leave images
        pnl_btnCPT.Dock = DockStyle.Top
        btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        btnCPT.BackgroundImageLayout = ImageLayout.Stretch
        btnCPT.Tag = "Selected"

        pnl_btnUser.Dock = DockStyle.Bottom
        btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnCPT.BackgroundImageLayout = ImageLayout.Stretch
        btnCPT.Tag = "UnSelected"

        pnl_btnProvider.Dock = DockStyle.Bottom
        btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnCPT.BackgroundImageLayout = ImageLayout.Stretch
        btnCPT.Tag = "UnSelected"

        pnl_btnProcedure.Dock = DockStyle.Bottom
        btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnCPT.BackgroundImageLayout = ImageLayout.Stretch
        btnCPT.Tag = "UnSelected"

        ' txtsearchrht.Text = ""
        txtsearchrht.Focus()
        AssociateID = Associates.CPT

        FillAssociates(Associates.CPT, strSearch)
        If gblnResetSearchTextBox = True Then
            txtsearchrht.ResetText()
        Else
            txtsearchrht_TextChanged(sender, e)
        End If
    End Sub

    Private Sub btnProcedure_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcedure.Click
        ''******************************Ojeswini02282009*******************************************************************
        'For btn Hover and Leave images
        pnl_btnProcedure.Dock = DockStyle.Top
        btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        btnCPT.BackgroundImageLayout = ImageLayout.Stretch
        btnCPT.Tag = "Selected"

        pnl_btnCPT.Dock = DockStyle.Top
        btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnCPT.BackgroundImageLayout = ImageLayout.Stretch
        btnCPT.Tag = "UnSelected"

        pnl_btnUser.Dock = DockStyle.Bottom
        btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnCPT.BackgroundImageLayout = ImageLayout.Stretch
        btnCPT.Tag = "UnSelected"

        pnl_btnProvider.Dock = DockStyle.Bottom
        btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnCPT.BackgroundImageLayout = ImageLayout.Stretch
        btnCPT.Tag = "UnSelected"

        'txtsearchrht.Text = ""
        txtsearchrht.Focus()
        AssociateID = Associates.Procedure

        FillAssociates(Associates.Procedure, strSearch)
        'Shuhbangi 20091006
        'Check ResetSearchTextBox  setting 
        If gblnResetSearchTextBox = True Then
            txtsearchrht.ResetText()
        Else
            txtsearchrht_TextChanged(sender, e)
        End If

    End Sub

    Private Sub btnProvider_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProvider.Click
        ''******************************Ojeswini02282009*******************************************************************
        'For btn Hover and Leave images
        pnl_btnProvider.Dock = DockStyle.Top
        btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        btnCPT.BackgroundImageLayout = ImageLayout.Stretch
        btnCPT.Tag = "Selected"

        pnl_btnProcedure.Dock = DockStyle.Top
        btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnCPT.BackgroundImageLayout = ImageLayout.Stretch
        btnCPT.Tag = "UnSelected"

        pnl_btnCPT.Dock = DockStyle.Top
        btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnCPT.BackgroundImageLayout = ImageLayout.Stretch
        btnCPT.Tag = "UnSelected"

        pnl_btnUser.Dock = DockStyle.Bottom
        btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnCPT.BackgroundImageLayout = ImageLayout.Stretch
        btnCPT.Tag = "UnSelected"

        ' txtsearchrht.Text = ""
        txtsearchrht.Focus()
        AssociateID = Associates.Provider
        FillAssociates(Associates.Provider, strSearch)
        'Shuhbangi 20091006
        'Check ResetSearchTextBox  setting 
        If gblnResetSearchTextBox = True Then
            txtsearchrht.ResetText()
        Else
            txtsearchrht_TextChanged(sender, e)
        End If
    End Sub

    Private Sub btnUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUser.Click
        ''******************************Ojeswini02282009*******************************************************************
        'For btn Hover and Leave images
        pnl_btnUser.Dock = DockStyle.Top
        btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        btnCPT.BackgroundImageLayout = ImageLayout.Stretch
        btnCPT.Tag = "Selected"

        pnl_btnProvider.Dock = DockStyle.Top
        btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnCPT.BackgroundImageLayout = ImageLayout.Stretch
        btnCPT.Tag = "UnSelected"

        pnl_btnProcedure.Dock = DockStyle.Top
        btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnCPT.BackgroundImageLayout = ImageLayout.Stretch
        btnCPT.Tag = "UnSelected"

        pnl_btnCPT.Dock = DockStyle.Top
        btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnCPT.BackgroundImageLayout = ImageLayout.Stretch
        btnCPT.Tag = "UnSelected"

        'txtsearchrht.Text = ""
        txtsearchrht.Focus()
        AssociateID = Associates.User

        FillAssociates(Associates.User, strSearch)
        'Shuhbangi 20091006
        'Check ResetSearchTextBox  setting 
        If gblnResetSearchTextBox = True Then
            txtsearchrht.ResetText()
        Else
            txtsearchrht_TextChanged(sender, e)
        End If

    End Sub

    Private Sub txtsearchrht_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtsearchrht.TextChanged
        'strSearch = txtsearchrht.Text
        'FillAssociates(AssociateID, strSearch)
        'txtsearchrht.Focus()
        Try
            Dim strSearchDetails As String
            If Trim(txtsearchrht.Text) <> "" Then
                strSearchDetails = Replace(txtsearchrht.Text, "'", "''")
                strSearchDetails = Replace(strSearchDetails, "[", "") & ""
                strSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strSearchDetails)
            Else
                strSearchDetails = ""
            End If

            'If Len(Trim(txtsearchrht.Text)) <= 1 Then
            'If txtsearchrht.Tag <> Trim(txtsearchrht.Text) Then
            If AssociateID = Associates.CPT Then
                FillAssociates(Associates.CPT, Trim(strSearchDetails))
                txtsearchrht.Tag = Trim(txtsearchrht.Text)
            ElseIf AssociateID = Associates.Procedure Then
                FillAssociates(Associates.Procedure, Trim(strSearchDetails))
                txtsearchrht.Tag = Trim(txtsearchrht.Text)
            ElseIf AssociateID = Associates.Provider Then
                FillAssociates(Associates.Provider, Trim(strSearchDetails))
                txtsearchrht.Tag = Trim(txtsearchrht.Text)
            ElseIf AssociateID = Associates.User Then
                FillAssociates(Associates.User, Trim(strSearchDetails))
                txtsearchrht.Tag = Trim(txtsearchrht.Text)
            End If
            'End If
            'End If

            Dim mychildnode As myTreeNode
            'child node collection
            For Each mychildnode In trvAssociates.Nodes.Item(0).Nodes
                'compare selected node text and entered text
                Dim str As String
                str = Mid(UCase(Trim(mychildnode.Text)), 1, Len(UCase(Trim(txtsearchrht.Text))))
                If str = UCase(Trim(txtsearchrht.Text)) Then
                    'for showing the selected drug at the top 
                    trvAssociates.SelectedNode = trvAssociates.SelectedNode.LastNode
                    trvAssociates.SelectedNode = mychildnode
                    txtsearchrht.Focus()
                    Exit Sub
                End If
            Next

            ''child node collection
            'For Each mychildnode In trvAssociates.Nodes.Item(0).Nodes
            '    'compare selected node text and entered text
            '    Dim str As String
            '    str = Mid(UCase(Trim(mychildnode.Tag)), 1, Len(UCase(Trim(txtsearchrht.Text))))
            '    If str = UCase(Trim(txtsearchrht.Text)) Then
            '        '*************code added by sagar for showing the selected drug at the top on 4 july 2007
            '        trvAssociates.SelectedNode = trvAssociates.SelectedNode.LastNode
            '        '*************
            '        trvAssociates.SelectedNode = mychildnode
            '        txtsearchrht.Focus()
            '        Exit Sub
            '    End If
            'Next
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Query, "Searched Electrophysiology having substring " & txtsearchrht.Text.Trim, gstrLoginName, gstrClientMachineName, gnPatientID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Query, "Searched Electrophysiology having substring ", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Query, "Searched Electrophysiology having substring ", mPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvAssociates_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trvAssociates.DoubleClick
        Try

            Dim mynode As myTreeNode
            mynode = CType(trvAssociates.SelectedNode, myTreeNode)

            If Not IsNothing(mynode) Then
                AddNode(mynode)
                'Shubhangi 20091209
                'Check the setting Reset search text box after assiging category
                If gblnResetSearchTextBox = True Then
                    txtsearchrht.ResetText()
                End If
            End If
            'selectedTreeview.ExpandAll()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub AddNode(ByVal mynode As myTreeNode)


        If Not IsNothing(trvProcedureDate) Then

            For x As Integer = 0 To trvProcedureDate.Nodes(0).Nodes.Count - 1
                For y As Integer = 0 To trvProcedureDate.Nodes(0).Nodes(x).Nodes.Count - 1
                    If (trvProcedureDate.Nodes(0).Nodes(x).Nodes(y).Text.ToUpper = mynode.Text.ToUpper) Then

                        Exit Sub
                    End If

                Next
            Next


            If mynode.Parent Is trvAssociates.Nodes.Item(0) Then 'trvCPT


                If mynode.Parent.Text = "CPT" Then

                    Dim associatenode As myTreeNode

                    associatenode = mynode.Clone
                    associatenode.Key = mynode.Key
                    associatenode.Text = mynode.Text
                    associatenode.ImageIndex = 3
                    associatenode.SelectedImageIndex = 3

                    trvProcedureDate.Nodes.Item(0).Nodes.Item(0).Nodes.Add(associatenode)

                ElseIf mynode.Parent.Text = "Procedure" Then
                    Dim associatenode As myTreeNode

                    associatenode = mynode.Clone
                    associatenode.Key = mynode.Key
                    associatenode.Text = mynode.Text
                    associatenode.ImageIndex = 3
                    associatenode.SelectedImageIndex = 3

                    trvProcedureDate.Nodes.Item(0).Nodes.Item(1).Nodes.Add(associatenode)

                ElseIf mynode.Parent.Text = "User" Then

                    Dim associatenode As myTreeNode

                    associatenode = mynode.Clone
                    associatenode.Key = mynode.Key
                    'associatenode.Text = mynode.Tag 'mynode.Text
                    associatenode.Text = mynode.Text ''Sandip Darade 20090312
                    associatenode.ImageIndex = 3
                    associatenode.SelectedImageIndex = 3

                    trvProcedureDate.Nodes.Item(0).Nodes.Item(2).Nodes.Add(associatenode)

                ElseIf mynode.Parent.Text = "Provider" Then
                    Dim associatenode As myTreeNode

                    associatenode = mynode.Clone
                    associatenode.Key = mynode.Key
                    associatenode.Text = mynode.Text
                    associatenode.ImageIndex = 3
                    associatenode.SelectedImageIndex = 3

                    trvProcedureDate.Nodes.Item(0).Nodes.Item(2).Nodes.Add(associatenode)
                End If
                trvProcedureDate.ExpandAll()
                trvProcedureDate.Select()


            End If
        End If
    End Sub

    Private Sub trvAssociates_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles trvAssociates.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            Try
                Dim mynode As myTreeNode
                mynode = CType(trvAssociates.SelectedNode, myTreeNode)
                If Not IsNothing(mynode) Then
                    AddNode(mynode)
                End If
                'selectedTreeview.ExpandAll()
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub tlsbtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsbtnSave.Click
        Dim cptcount As Int16 = 0
        Dim UserCount As Int16 = 0
        Dim Proccount As Int16 = 0
        Dim Maxcount As Int16 = 0
        Dim ElectoID As Long = 0
        Try
            If mVisitID = 0 Then
                Try
                    mVisitID = GenerateVisitID(DTPicker1.Value.Date, mPatientID)
                Catch ex As Exception

                End Try
            End If
            'deleteprevious data against Date, PID,VID
            If trvProcedureDate.Nodes.Item(0).Nodes.Item(0).Nodes.Count > 0 Then
                cptcount = trvProcedureDate.Nodes.Item(0).Nodes.Item(0).Nodes.Count
            End If
            If trvProcedureDate.Nodes.Item(0).Nodes.Item(1).Nodes.Count > 0 Then
                Proccount = trvProcedureDate.Nodes.Item(0).Nodes.Item(1).Nodes.Count
            End If
            If trvProcedureDate.Nodes.Item(0).Nodes.Item(2).Nodes.Count > 0 Then
                UserCount = trvProcedureDate.Nodes.Item(0).Nodes.Item(2).Nodes.Count
            End If
            If cptcount > Proccount Then
                If cptcount > UserCount Then
                    Maxcount = cptcount
                Else
                    Maxcount = UserCount
                End If
            ElseIf Proccount > UserCount Then
                Maxcount = Proccount
            Else
                Maxcount = UserCount
            End If

           

            'With trvProcedureDate
            ''Dim NewProcedureDate = DTPicker1.Text
            ''Dim dt As DataTable = GetCVElectroPhysiologyData(mPatientID, NewProcedureDate, mVisitID)
            ''If Not IsNothing(dt) Then
            ''    If dt.Rows.Count > 0 Then
            ''        ElectoID = dt.Rows(0)("ElectroPhysiologyID")
            ''    End If
            ''Else
            ''    ElectoID = 0
            ''End If
            If Maxcount > 0 Then
                Dim objElectroPhysio As clsElectroPhysio
                Dim Arrlist As New ArrayList
                Dim objElectroPhysioDBLayer As New clsElectroPhysioDBLayer
                DeleteCVElectophysiologydata(mPatientID, DTPicker1.Text, mVisitID)
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Delete, "Deleted the electro record .  ", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101011
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Delete, "Deleted the electro record .  ", mPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''

                For i As Int16 = 0 To Maxcount - 1
                    objElectroPhysio = New clsElectroPhysio
                    objElectroPhysio.ElectroPhysiologyID = ElectoID
                    objElectroPhysio.PatientID = mPatientID
                    objElectroPhysio.ExamID = 0
                    objElectroPhysio.VisitID = mVisitID
                    objElectroPhysio.ClinicID = 1
                    objElectroPhysio.dtProcedureDate = DTPicker1.Value.Date

                    If cptcount > 0 Then
                        If i >= cptcount Then
                            objElectroPhysio.CPTCode = ""
                        Else
                            objElectroPhysio.CPTCode = trvProcedureDate.Nodes.Item(0).Nodes.Item(0).Nodes.Item(i).Text
                        End If
                    Else
                        objElectroPhysio.CPTCode = ""
                    End If

                    If Proccount > 0 Then
                        If i >= Proccount Then
                            objElectroPhysio.Procedures = ""
                        Else
                            objElectroPhysio.Procedures = trvProcedureDate.Nodes.Item(0).Nodes.Item(1).Nodes.Item(i).Text
                        End If
                    Else
                        objElectroPhysio.Procedures = ""
                    End If

                    If UserCount > 0 Then
                        Dim strUser As String = ""
                        For j As Int16 = 0 To UserCount - 1
                            If j = 0 Then
                                strUser = trvProcedureDate.Nodes.Item(0).Nodes.Item(2).Nodes.Item(j).Text
                            Else
                                strUser = strUser + "|" + trvProcedureDate.Nodes.Item(0).Nodes.Item(2).Nodes.Item(j).Text
                            End If
                        Next
                        objElectroPhysio.UserProvider = strUser
                    Else
                        objElectroPhysio.UserProvider = ""
                    End If

                    Arrlist.Add(objElectroPhysio)
                Next

                objElectroPhysioDBLayer.SaveElectroPhysioTest(Arrlist)

                Me.Close()
                objElectroPhysioDBLayer = Nothing
                Arrlist.Clear()
            Else
                MessageBox.Show("Please select some data to save.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            'End With
            'Return True

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Function DeleteCVElectophysiologydata(ByVal PatientID As Int64, ByVal DTPicker As Date, ByVal VisitID As Int64)
        Dim conn As SqlConnection = New SqlConnection(GetConnectionString)
        Try
            conn.Open()
            Dim strQuery As String = "Delete from CV_ElectroPhysiology where nPatientID=" & PatientID & " and nVisitID=" & VisitID & " and  dtProcedureDate='" & DTPicker & "'"
            Dim cmd As SqlCommand = New SqlCommand(strQuery, conn)

            cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing

        End Try
        Return Nothing
    End Function

    Private Sub DTPicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DTPicker1.ValueChanged
        Try
            If blnIsLoaded = True Then
                mProcedureDate = DTPicker1.Value.Date
                trvProcedureDate.Nodes.Item(0).Text = "Procedure Date: '" & CType(mProcedureDate, String) & "'"
                'When date is changed check if there is a visitid for that date
                mVisitID = GetVisitID(DTPicker1.Value.Date, mPatientID)
                ' if visit exists for that date then
                If mVisitID <> 0 Then
                    'Further check if there is stresstest for that date

                    Dim strquery As String = "select count(*) from CV_ElectroPhysiology where nvisitid=" & mVisitID & ""
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    oDB.Connect(GetConnectionString)
                    Dim objval As Object = oDB.ExecuteQueryScaler(strquery)
                    If Not IsNothing(objval) Then
                        If CType(objval, Int32) > 0 Then
                            ''Added for Bug Id 5460
                            If MessageBox.Show("Do you want to load Procedure for date '" & CType(mProcedureDate, String) & "'?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                                FillProcedureDateTreeView(mProcedureDate)
                            End If
                            ''End
                        End If
                    End If
                    oDB.Dispose()
                    oDB = Nothing

                End If
            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub txtsearchrht_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsearchrht.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            trvAssociates.Select()
        Else
            trvAssociates.SelectedNode = trvAssociates.Nodes.Item(0)
        End If
        '\\ 20090128 added by suraj - for drugs search with allowed char '-'
        If pnl_btnCPT.Dock = DockStyle.Top Then
            '    ValidateTextSearch(txtsearchAssociates.Text, e)
            'Else
            mdlGeneral.ValidateText(txtsearchrht.Text, e)
        End If
    End Sub


    Private Sub trvProcedureDate_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvProcedureDate.MouseDown

        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then

                Dim currentNode As myTreeNode
                currentNode = CType(trvProcedureDate.GetNodeAt(e.X, e.Y), myTreeNode)

                If Not IsNothing(currentNode) Then
                    trvProcedureDate.SelectedNode = CType(currentNode, TreeNode)
                    If currentNode.Level = 2 Then
                        'trvProcedureDate.ContextMenuStrip = ConMenuDeleteNode
                        'Try
                        '    If (IsNothing(currentNode.ContextMenuStrip) = False) Then
                        '        currentNode.ContextMenuStrip.Dispose()
                        '        currentNode.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try

                        currentNode.ContextMenuStrip = ConMenuDeleteNode
                        If currentNode.Parent.Text.ToUpper() = "CPT" Then
                            mnuRemoveCPT.Visible = True
                            mnuRemoveProcedure.Visible = False
                            mnuRemoveUser.Visible = False
                        ElseIf currentNode.Parent.Text.ToUpper() = "PROCEDURE" Then
                            mnuRemoveCPT.Visible = False
                            mnuRemoveProcedure.Visible = True
                            mnuRemoveUser.Visible = False

                        ElseIf currentNode.Parent.Text.ToUpper() = "USERS" Then
                            mnuRemoveCPT.Visible = False
                            mnuRemoveProcedure.Visible = False
                            mnuRemoveUser.Visible = True
                        End If
                    Else
                        'Try
                        '    If (IsNothing(currentNode.ContextMenuStrip) = False) Then
                        '        currentNode.ContextMenuStrip.Dispose()
                        '        currentNode.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        currentNode.ContextMenuStrip = Nothing
                    End If
                Else
                    '            currentNode.ContextMenuStrip = Nothing
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub mnuRemoveCPT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRemoveCPT.Click

        If Not IsNothing(trvProcedureDate.SelectedNode) AndAlso trvProcedureDate.SelectedNode.Level = 2 Then

            '  If trvProcedureDate.SelectedNode.Text <> "CPT" And trvProcedureDate.SelectedNode.Text <> "Procedure" And trvProcedureDate.SelectedNode.Text <> "Users" Then
            ' If Not IsNothing(trvProcedureDate.SelectedNode.Parent) Then
            trvProcedureDate.SelectedNode.Remove()
            trvProcedureDate.ExpandAll()
            'End If
            'End If
        End If
    End Sub

    Private Sub mnuRemoveProcedure_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuRemoveProcedure.Click
        If trvProcedureDate.SelectedNode.Text <> "Procedure" Then
            trvProcedureDate.SelectedNode.Remove()
            trvProcedureDate.ExpandAll()
        End If
    End Sub

    Private Sub mnuRemoveUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuRemoveUser.Click
        If trvProcedureDate.SelectedNode.Text <> "Users" Then
            trvProcedureDate.SelectedNode.Remove()
            trvProcedureDate.ExpandAll()
        End If

    End Sub
    '******************************Ojeswini02282009*******************************************************************
    'btn hover and leave images
    Private Sub btnCPT_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCPT.MouseLeave
        If pnl_btnCPT.Dock = DockStyle.Bottom Then
            btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            btnCPT.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
            btnCPT.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub btnCPT_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCPT.MouseHover
        btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        btnCPT.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnProcedure_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcedure.MouseHover
        btnProcedure.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        btnProcedure.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnProcedure_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcedure.MouseLeave
        If pnl_btnProcedure.Dock = DockStyle.Bottom Then
            btnProcedure.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            btnProcedure.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnProcedure.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
            btnProcedure.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub btnProvider_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProvider.MouseHover
        btnProvider.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        btnProvider.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnProvider_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProvider.MouseLeave
        If pnl_btnProvider.Dock = DockStyle.Bottom Then
            btnProvider.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            btnProvider.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnProvider.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
            btnProvider.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub btnUser_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUser.MouseHover
        btnUser.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        btnUser.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnUser_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUser.MouseLeave
        If pnl_btnUser.Dock = DockStyle.Bottom Then
            btnUser.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            btnUser.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnUser.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
            btnUser.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    '******************************Ojeswini02282009*******************************************************************

    Private Sub btnsearchrhtClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsearchrhtClear.Click
        'shubhangi 20091006
        'Use clear button to clear search text box
        txtsearchrht.ResetText()
        txtsearchrht.Focus()
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
