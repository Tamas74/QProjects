Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports gloEMR.gloEMRWord
Imports System.IO

Public Class clsCPTAssociation

    Private Dv As DataView = Nothing
    Private dt As DataTable = Nothing
    Public Sub Dispose()

        ''slr free dv
        If Not IsNothing(dv) Then
            dv.Dispose()
            dv = Nothing
        End If
        'If Not IsNothing(ds) Then
        '    ds.Dispose()
        '    ds = Nothing
        'End If

        'slr free Con
        If Not IsNothing(dt) Then
            dt.Dispose()
            dt = Nothing
        End If

    End Sub

    Public ReadOnly Property DsDataview() As DataView
        Get
            'Dv = Ds.Tables("Category_Mst").DefaultView
            Return Dv
            'Return Ds
        End Get
    End Property
    Public ReadOnly Property GetDataTable() As DataTable
        Get
            Return dt
        End Get
    End Property

#Region " CPTAssociation Functions & Procedures "

    '''' <summary>
    ''''  Used In CPT-Assocation to save frmCPTAssocation 
    '''' </summary>
    '''' <param name="CPTID"></param>
    '''' <param name="CPTName"></param>
    '''' <param name="arrlist"></param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    Public Function AddData(ByVal CPTID As Long, ByVal CPTName As String, ByVal arrlist As ArrayList) As Boolean
        Dim oDB As DataBaseLayer
        Dim oParamater As gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
        Try
            oDB = New DataBaseLayer
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nCPTId"
            oParamater.Value = CPTID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oDB.Delete("gsp_DeleteCPTTemplate")

            oDB.Dispose()

            oDB = Nothing

            For i As Int32 = 0 To arrlist.Count - 1
                Dim objmylist As myList
                objmylist = CType(arrlist.Item(i), myList)

                'Insert data in ICD9CPT
                If objmylist.Description = "c" Then

                    oDB = New DataBaseLayer
                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.BigInt
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@nCPTId"
                    oParamater.Value = CPTID
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New DBParameter
                    oParamater.DataType = SqlDbType.BigInt
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@nTemplateID"
                    oParamater.Value = objmylist.Index
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oDB.Add("gsp_InsertCPTTemplate")

                    oDB.Dispose()

                    oDB = Nothing



                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "CPT Association Added", gstrLoginName, gstrClientMachineName)
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Add, "CPT Association Added", gloAuditTrail.ActivityOutCome.Success)
                End If
            Next

            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return False
        End Try
    End Function

    Public Function FillCPT(ByVal id As Int16) As DataTable

        Dim oDB As New DataBaseLayer
        Dim oParamater As gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
        Dim oResultTable As DataTable
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@Flag"
            oParamater.Value = id
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oResultTable = oDB.GetDataTable("gsp_FillCPTCategory_MST")
            If Not oResultTable Is Nothing Then
                Return oResultTable
            Else
                Return Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally

            oDB.Dispose()
        End Try
    End Function
    ''top 100 logic implemented
    Public Function FillCPT(ByVal id As Int16, ByVal SearchText As String, Optional ByVal SearchType As Integer = 0) As DataTable

        Dim oDB As New DataBaseLayer
        Dim oParamater As gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
        Dim oResultTable As DataTable
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@Flag"
            oParamater.Value = id
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            If SearchText <> "" Then
                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.VarChar
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@SearchText"
                oParamater.Value = SearchText
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing
            End If
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@SearchType"
            oParamater.Value = SearchType
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oResultTable = oDB.GetDataTable("gsp_FillTopCPT")
            If Not oResultTable Is Nothing Then
                Return oResultTable
            Else
                Return Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally

            oDB.Dispose()
        End Try
    End Function
    Public Function FetchCPTforUpdate(ByVal CPTID As Long) As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParamater As gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
        Dim oResultTable As DataTable = Nothing
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nCPTID"
            oParamater.Value = CPTID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oResultTable = oDB.GetDataTable("gsp_scanCPTAssociation")
            If Not oResultTable Is Nothing Then
                Return oResultTable
            Else
                Return Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            oDB.Dispose()
        End Try
    End Function

    Public Function GetAssociatedCPT(ByVal id As Int16) As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Dim oResultTable As DataTable
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@Flag"
            oParamater.Value = id
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oResultTable = oDB.GetDataTable("gsp_FillCPTAccociation")
            If Not oResultTable Is Nothing Then
                Return oResultTable
            Else
                Return Nothing
            End If

            ''''dt(0) = CPTID
            ''''dt(1)= CPTCODE + Cpt Name
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            oDB.Dispose()
        End Try


    End Function

    Public Function GetAccociatedTemplates(ByVal CPTID As Long) As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Dim oResultTable As DataTable
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@CPTID"
            oParamater.Value = CPTID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oResultTable = oDB.GetDataTable("gsp_FillTemplates")
            If Not oResultTable Is Nothing Then
                Return oResultTable
            Else
                Return Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            oDB.Dispose()
        End Try
    End Function
    'SHUBHANGI 20110125 TO RESOLV ISSUE 7844
    Public Function GetCPTTemplatesAssociation(ByVal id As Int16) As DataTable

        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Dim oResultTable As DataTable
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@Flag"
            oParamater.Value = id
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oResultTable = oDB.GetDataTable("FillCPTAccociation")
            If Not oResultTable Is Nothing Then
                Return oResultTable
            Else
                Return Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing

        Finally
            'If Not IsNothing(oResultTable) Then
            '    oResultTable.Dispose()
            '    oResultTable = Nothing
            'End If

            If Not IsNothing(oDB) Then
                oDB.Dispose()
            End If

        End Try
    End Function
    'END

    Public Function SelectTemplateGallery(ByVal TemplateID As Long) As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Dim oResultTable As DataTable
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateID"
            oParamater.Value = TemplateID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oResultTable = oDB.GetDataTable("gsp_ScanTemplateGallery_MST")
            If Not oResultTable Is Nothing Then
                Return oResultTable
            Else
                Return Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Dispose()
        End Try
    End Function

#End Region


#Region " Form Gallery Functions & Procedures "


    Public Function InsertIntoTempFormGallery(ByVal PatientID As Long) As DataTable
        '''''' Here we Create Template Table from Selected Patient with Corssponding Fields
        '''''' PatientID , CPTID ,TemplateID, Result
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Dim oResultTable As DataTable
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@PatientID"
            oParamater.Value = PatientID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            oResultTable = oDB.GetDataTable("gsp_InsertTempFormGallery")
            If Not oResultTable Is Nothing Then
                Return oResultTable
            Else
                Return Nothing
            End If

            '''''nPatientID  , nCPTID  , nTemplateID, sResult
            ''''dt.Columns(0) = nPatientID
            ''''dt.Columns(1) = nCPTID
            ''''dt.Columns(2) = nTemplateID
            ''''dt.Columns(3) = sResult


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Dispose()
        End Try
    End Function

    Public Function ViewFormGallery(ByVal PatientID As Long) As DataView
        '''''' Here we Create Template Table from Selected Patient with Corssponding Fields
        '''''' PatientID , CPTID ,TemplateID, Result
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@PatientID"
            oParamater.Value = PatientID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            If (IsNothing(dt) = False) Then
                dt.Dispose()
                dt = Nothing
            End If
            dt = oDB.GetDataTable("gsp_ViewFormGallery")
            If Not dt Is Nothing Then
                If (IsNothing(Dv) = False) Then
                    Dv.Dispose()
                    Dv = Nothing
                End If
                Dv = New DataView(dt.Copy())

                Return Dv
            Else
                Return Nothing
            End If


            '''''nPatientID  , nCPTID  , nTemplateID, sResult
            ''''dt.Columns(0) = nPatientID
            ''''dt.Columns(1) = nCPTID
            ''''dt.Columns(2) = nTemplateID
            ''''dt.Columns(3) = sResult

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Dispose()
        End Try
    End Function

    Public Function SelectFormGallery(ByVal PatientID As Long, ByVal VisitID As Long) As DataTable
        '''''' Here we Create Template Table from Selected Patient with Corssponding Fields
        '''''' PatientID , CPTID ,TemplateID, Result

        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Dim oResultTable As DataTable
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@PatientID"
            oParamater.Value = PatientID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@VisitID"
            oParamater.Value = VisitID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oResultTable = oDB.GetDataTable("gsp_ScanFormGallery")
            If Not oResultTable Is Nothing Then
                Return oResultTable
            Else
                Return Nothing
            End If

            '''''nPatientID  , nCPTID  , nTemplateID, sResult
            ''''dt.Columns(0) = nPatientID
            ''''dt.Columns(1) = nCPTID
            ''''dt.Columns(2) = nTemplateID
            ''''dt.Columns(3) = TemplateName
            ''''dt.Columns(4) = sResult


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Dispose()
        End Try
    End Function

    '' To delete SINGLE Seleted formTemplete from VIEWform
    Public Function DeleteForm(ByVal VisitID As Long, ByVal VisitDate As String, ByVal CPTID As Long, ByVal TemplateID As Long, ByVal TemplateName As String, ByVal PatientID As Long, ByVal FormID As Long, Optional ByVal Flag As Boolean = False) As Boolean
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        ' Dim oResultTable As DataTable
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@CPTID"
            oParamater.Value = CPTID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@VisitID"
            oParamater.Value = VisitID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateID"
            oParamater.Value = TemplateID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@FormID"
            oParamater.Value = FormID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oDB.Delete("gsp_DeleteForm")

            If Flag = False Then
                '''' If Delete is Called from View Form then add that entry in AuditLOG 
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Delete, "Form Gallery deleted", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Delete, "Form Deleted", gstrLoginName, gstrClientMachineName, gnPatientID)
            End If

            Return True

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            oDB.Dispose()
        End Try
    End Function

    Public Sub SaveTempFormGallery(ByVal PatientID As Long, ByVal CPTID As Long, ByVal TemplateID As Long, ByVal strFilePath As String)
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter

        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@CPTID"
            oParamater.Value = CPTID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@PatientID"
            oParamater.Value = PatientID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateID"
            oParamater.Value = TemplateID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Image
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@Result"
            Dim objword As New clsWordDocument
            '' To convert from Object to Binary Format
            oParamater.Value = objword.ConvertFiletoBinary(strFilePath)
            objword = Nothing
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oDB.Add("gsp_InUpTempFormGallery")

            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Add, "Form Added", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101008
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Add, "Form Added", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.Add, "Form Added", gstrLoginName, gstrClientMachineName, gnPatientID)
            'objAudit = Nothing

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            oDB.Dispose()
        End Try
    End Sub

    Public Function SelectForm(ByVal PatientID As Long, ByVal VisitID As Long, ByVal CPTID As Long, ByVal TemplateID As Int64, ByVal FormID As Int64) As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Dim oResultTable As DataTable
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@CPTID"
            oParamater.Value = CPTID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@PatientID"
            oParamater.Value = PatientID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@VisitID"
            oParamater.Value = VisitID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateID"
            oParamater.Value = TemplateID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@FormID"
            oParamater.Value = FormID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oResultTable = oDB.GetDataTable("gsp_SelectForm")
            If Not oResultTable Is Nothing Then
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Modify, "Form Modified", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101008
                'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Modify, "Form Modified", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'Dim oAudit As New clsAudit
                'oAudit.CreateLog(clsAudit.enmActivityType.Modify, "Form Modified", gstrLoginName, gstrClientMachineName, gnPatientID)
                'oAudit = Nothing
                Return oResultTable

            Else
                Return Nothing
            End If



        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Dispose()
        End Try
    End Function

    Public Function CheckInFormGallery(ByVal PatientID As Long, ByVal VisitID As Long, ByVal CPTID As Long, ByVal TemplateID As Long, ByVal FormID As Long) As Boolean
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        'Dim oResultTable As DataTable
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@CPTID"
            oParamater.Value = CPTID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@PatientID"
            oParamater.Value = PatientID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@VisitID"
            oParamater.Value = VisitID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateID"
            oParamater.Value = TemplateID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@FormID"
            oParamater.Value = FormID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            Dim rowAffected As Int64
            rowAffected = CType(oDB.GetDataValue("gsp_CheckFormGallery"), Int64)

            If rowAffected > 0 Then
                Return True
            Else
                Return False
            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            oDB.Dispose()
        End Try
    End Function

    Public Sub SaveFormGallery(ByVal PatientID As Long, ByVal VisitID As Long, ByVal Arrlist As ArrayList)
        Dim oDB As DataBaseLayer
        Dim oParamater As DBParameter
        Try
            oDB = New DataBaseLayer

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@VisitID"
            oParamater.Value = VisitID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oDB.Delete("gsp_DeleteFormGallery")
            oDB.Dispose()
            oDB = Nothing

            For i As Int16 = 0 To Arrlist.Count - 1
                Dim lst As myList
                lst = CType(Arrlist(i), myList)

                oDB = New DataBaseLayer

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.Int
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@FormID"
                oParamater.Value = 0
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@PatientID"
                oParamater.Value = PatientID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@VisitID"
                oParamater.Value = VisitID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@CPTID"
                oParamater.Value = lst.ID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@TemplateID"
                oParamater.Value = lst.Index
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.Image
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@Result"
                'SLR: Assigned directly lst.TemplateResult on 12/2/2014
                'Dim objword As New clsWordDocument
                'Dim strFileName As String
                'strFileName = ExamNewDocumentName
                If (IsNothing(lst.TemplateResult) = False) Then
                    oParamater.Value = CType(lst.TemplateResult, Byte()).Clone() 'objword.ConvertFiletoBinary(objword.GenerateFile(lst.TemplateResult, strFileName))
                Else
                    oParamater.Value = DBNull.Value
                End If
                 'objword = Nothing
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@MachineID"
                oParamater.Value = GetPrefixTransactionID()
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oDB.Add("gsp_InUpFormGallery")

                oDB.Dispose()
                oDB = Nothing

            Next
            If frmVWFormGallery.blnModify = True Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Modify, "Form Gallery modified", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                'ElseIf frmCPTTemplate._IsFormGallerySaved = False And frmVWFormGallery.blnModify = False Then
                'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Modify, "Form Gallery modified", gnPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
            Else

                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Add, "Form Gallery added", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)


            End If

            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Add, "Form Gallery Added", gloAuditTrail.ActivityOutCome.Success)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDB = Nothing
        End Try
    End Sub
#End Region

    'Public Sub GenerateVisitID(ByVal PatientID As Long)
    '    Try
    '        Dim cmdVisits As SqlCommand
    '        Dim objParam As SqlParameter
    '        Dim objFlagParam As SqlParameter

    '        cmdVisits = New SqlCommand("gsp_InsertVisits", Conn)
    '        cmdVisits.CommandType = CommandType.StoredProcedure
    '        Conn.ConnectionString = GetConnectionString()

    '        objParam = cmdVisits.Parameters.Add("@nPatientID", SqlDbType.bigInt)
    '        objParam.Direction = ParameterDirection.Input
    '        'objParam.Value = objPrescription.PatientID
    '        objParam.Value = PatientID

    '        objParam = cmdVisits.Parameters.Add("@dtVisitdate", SqlDbType.DateTime)
    '        objParam.Direction = ParameterDirection.Input
    '        objParam.Value = Now

    '        'Retrieve Appointment ID
    '        Dim nAppointmentID As Integer
    '        Dim objAppointmentID As New clsAppointments
    '        nAppointmentID = objAppointmentID.GetPatientAppointment(System.DateTime.Now, gnPatientID)
    '        objAppointmentID = Nothing


    '        objParam = cmdVisits.Parameters.Add("@AppointmentID", SqlDbType.Int)
    '        objParam.Direction = ParameterDirection.Input
    '        objParam.Value = nAppointmentID

    '        objParam = cmdVisits.Parameters.Add("@MachineID", SqlDbType.BigInt)
    '        objParam.Direction = ParameterDirection.Input
    '        objParam.Value = GetPrefixTransactionID

    '        objParam = cmdVisits.Parameters.Add("@VisitID", 0)
    '        objParam.Direction = ParameterDirection.Output
    '        'objParam.Value = 0


    '        objFlagParam = cmdVisits.Parameters.Add("@flag", SqlDbType.Int)
    '        objFlagParam.Direction = ParameterDirection.Output

    '        If Conn.State = ConnectionState.Closed Then
    '            Conn.Open()
    '        End If

    '        cmdVisits.ExecuteNonQuery()
    '        gnVisitID = objParam.Value
    '        If objFlagParam.Value = 0 Then
    '            Dim objAudit As New clsAudit
    '            objAudit.CreateLog(clsAudit.enmActivityType.Add, "Visit Added on " & CType(Now, String), gstrLoginName, gstrClientMachineName)
    '            objAudit = Nothing
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString,gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        Conn.Close()
    '    End Try
    'End Sub

    Public Sub InsertUpdateFormGallery(ByVal patientID As Long, ByVal formID As Long, ByVal CPTID As Long, ByVal visitID As Long, ByVal templateID As Long, ByVal fileInByte As Object)
        Dim oDB As DataBaseLayer = Nothing
        Dim oParamater As DBParameter = Nothing
        Try
            oDB = New DataBaseLayer

            oParamater = New DBParameter
            If Not IsNothing(oParamater) Then
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@FormID"
                oParamater.Value = formID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing
            End If

            oParamater = New DBParameter
            If Not IsNothing(oParamater) Then
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@PatientID"
                oParamater.Value = patientID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing
            End If


            oParamater = New DBParameter
            If Not IsNothing(oParamater) Then
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@VisitID"
                oParamater.Value = visitID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing
            End If

            oParamater = New DBParameter
            If Not IsNothing(oParamater) Then
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@CPTID"
                oParamater.Value = CPTID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing
            End If

            oParamater = New DBParameter
            If Not IsNothing(oParamater) Then
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@TemplateID"
                oParamater.Value = templateID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing
            End If

            oParamater = New DBParameter
            If Not IsNothing(oParamater) Then
                oParamater.DataType = SqlDbType.Image
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@Result"
                'Dim objword As New clsWordDocument
                '  Dim strFileName As String
                If (IsNothing(fileInByte) = False) Then
                    oParamater.Value = fileInByte
                Else
                    oParamater.Value = DBNull.Value
                End If

                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing
            End If

            oParamater = New DBParameter
            If Not IsNothing(oParamater) Then
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@MachineID"
                oParamater.Value = GetPrefixTransactionID()
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing
                oDB.Add("gsp_InUpSaveFormGallery")
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If Not IsNothing(oParamater) Then
                oParamater = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Sub

End Class
