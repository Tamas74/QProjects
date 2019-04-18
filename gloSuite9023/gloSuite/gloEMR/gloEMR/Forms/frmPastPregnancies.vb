Imports C1.Win.C1FlexGrid
Imports System.Collections.Specialized

Public Class frmPastPregnancies

#Region "Form Variables"
    Private nPatientID As Int64 = 0
    Private blnSaveClicked As Boolean

    '05-May-15 Aniket: Implementation of Past Pregnancies Liquid Link
    Public myCaller As frmPatientExam
    Public myCaller1 As Object

#Region "C1 Grid Columns constants"

    Private Const COL_PAST_PREGNANCIES_ID = 0
    Private Const COL_PATIENT_ID = 1
    Private Const COL_PREGNANCY_DATE = 2
    Private Const COL_GA_WEEKS = 3
    Private Const COL_LENGTH_OF_LABOR = 4
    Private Const COL_BIRTH_WEIGHT = 5
    Private Const COL_GENDER = 6
    Private Const COL_DELIVERY_TYPE = 7
    Private Const COL_ANES = 8
    Private Const COL_PLACE_OF_DELIVERY = 9
    Private Const COL_PRE_TERM_LABOR = 10
    Private Const COL_COMMENTS = 11

#End Region '"C1 Grid Columns constants"
    Private Const COL_COUNT = 12

    Private dtDeliveryTypes As DataTable = Nothing

#End Region '"Form Variables"

#Region "Constructors"
    Public Sub New(_PatientID As Int64)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        nPatientID = _PatientID
    End Sub
#End Region

    '05-May-15 Aniket: Implementation of Past Pregnancies Liquid Link
    Private Sub frmPastPregnancies_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        If blnSaveClicked = True Then
            If IsNothing(myCaller) = False Then
                myCaller.GetdataFromOtherForms(gloEMRWord.enumDocType.PastPregancies)
            ElseIf IsNothing(myCaller1) = False Then
                myCaller1.GetdataFromOtherForms(gloEMRWord.enumDocType.PastPregancies)
            End If
        End If

    End Sub '"Constructors"


    Private Sub frmPastPregnancies_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor

        Try
            Me.SuspendLayout()

            BindPatientPastPregnancies()

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PastPregnancies, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, "Patient Past Pregnancies Viewed", nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, True)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PastPregnancies, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        Finally
            Me.ResumeLayout()
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub BindPatientPastPregnancies()
        Dim dtPastPregnancies As DataTable = Nothing
        Try
            dtPastPregnancies = GetPastPregnancies(nPatientID)
            dtDeliveryTypes = GetDeliveryType()

            gloC1FlexStyle.Style(c1PastPregnancies)

            SetGridStyle(dtPastPregnancies)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PastPregnancies, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    Private Function GetPastPregnancies(_PatientID As Int64) As DataTable
        Dim dtData As DataTable = Nothing
        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim oPara As gloDatabaseLayer.DBParameters = Nothing

        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString())

            oPara = New gloDatabaseLayer.DBParameters()
            oPara.Clear()
            oPara.Add("@patientID", _PatientID, ParameterDirection.Input, SqlDbType.BigInt)

            oDB.Connect(False)

            oDB.Retrive("GetPastPregnanciesByPatientID", oPara, dtData)

            '  oDB.Retrive_Query("SELECT PastPregnanciesID,nPatientID,nGAWeeks,dtPregnancyDate,sLengthOfLabor,nBirthWeight,sGender,nDeliveryType,sAnes,sPlaceOfDelivery,sPreTermLabor,sComments FROM PastPregnancies WHERE nPatientID = " & _PatientID & " ORDER BY dtPregnancyDate DESC", dtData)

            oDB.Disconnect()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PastPregnancies, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            If Not IsNothing(oPara) Then
                oPara.Clear()
                oPara.Dispose()
                oPara = Nothing
            End If

        End Try

        Return dtData
    End Function

    Private Function GetDeliveryType() As DataTable
        Dim dtData As DataTable = Nothing
        Dim oDB As gloDatabaseLayer.DBLayer = Nothing

        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString())

            oDB.Connect(False)

            oDB.Retrive("GetDeliveryType", dtData)

            '    oDB.Retrive_Query("SELECT nCategoryID,sDescription FROM Category_MST WHERE ( sCategoryType = 'Delivery Type' ) ORDER BY sDescription", dtData)

            oDB.Disconnect()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PastPregnancies, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try

        Return dtData
    End Function

    Private Sub SetGridStyle(ByVal dt As DataTable)
        Dim cStyleGender As C1.Win.C1FlexGrid.CellStyle = Nothing
        Dim cStyleDeliveryType As C1.Win.C1FlexGrid.CellStyle = Nothing
        Dim lstDeliveryTypes As ListDictionary = Nothing

        Try
            With c1PastPregnancies
                .Clear()
                .AutoResize = False
                .ExtendLastCol = False
                .AllowDragging = False

                .Dock = DockStyle.Fill

                .Cols.Count = COL_COUNT
                .Rows.Count = 1
                .AllowEditing = True
                .AllowAddNew = True

                .Styles.ClearUnused()

                .SetData(0, COL_PAST_PREGNANCIES_ID, "Past Pregnancies ID")
                .SetData(0, COL_PATIENT_ID, "Patient ID")
                .SetData(0, COL_PREGNANCY_DATE, "Preg. Date")
                .SetData(0, COL_GA_WEEKS, "GA Weeks")
                .SetData(0, COL_LENGTH_OF_LABOR, "Length of Labor")
                .SetData(0, COL_BIRTH_WEIGHT, "Birth Weight")
                .SetData(0, COL_GENDER, "Gender")
                .SetData(0, COL_DELIVERY_TYPE, "Delivery Type")
                .SetData(0, COL_ANES, "Anes")
                .SetData(0, COL_PLACE_OF_DELIVERY, "Place of Delivery")
                .SetData(0, COL_PRE_TERM_LABOR, "Pre Term Labor")
                .SetData(0, COL_COMMENTS, "Comments")

                Dim _TotalWidth As Int32 = .Width - 5

                .Cols(COL_PAST_PREGNANCIES_ID).Width = 0
                .Cols(COL_PATIENT_ID).Width = 0
                .Cols(COL_PREGNANCY_DATE).Width = _TotalWidth * 0.1
                .Cols(COL_GA_WEEKS).Width = _TotalWidth * 0.06
                .Cols(COL_LENGTH_OF_LABOR).Width = _TotalWidth * 0.1
                .Cols(COL_BIRTH_WEIGHT).Width = _TotalWidth * 0.08
                .Cols(COL_GENDER).Width = _TotalWidth * 0.08
                .Cols(COL_DELIVERY_TYPE).Width = _TotalWidth * 0.1
                .Cols(COL_ANES).Width = _TotalWidth * 0.06
                .Cols(COL_PLACE_OF_DELIVERY).Width = _TotalWidth * 0.1
                .Cols(COL_PRE_TERM_LABOR).Width = _TotalWidth * 0.1
                .Cols(COL_COMMENTS).Width = _TotalWidth * 0.22

                .Cols(COL_PAST_PREGNANCIES_ID).DataType = System.Type.GetType("System.Int64")
                .Cols(COL_PATIENT_ID).DataType = System.Type.GetType("System.Int64")
                .Cols(COL_PREGNANCY_DATE).DataType = System.Type.GetType("System.DateTime")
                .Cols(COL_GA_WEEKS).DataType = System.Type.GetType("System.Int16")
                .Cols(COL_LENGTH_OF_LABOR).DataType = System.Type.GetType("System.String")
                .Cols(COL_BIRTH_WEIGHT).DataType = System.Type.GetType("System.Decimal")
                .Cols(COL_GENDER).DataType = System.Type.GetType("System.String")
                .Cols(COL_DELIVERY_TYPE).DataType = System.Type.GetType("System.Int64")
                .Cols(COL_ANES).DataType = System.Type.GetType("System.String")
                .Cols(COL_PLACE_OF_DELIVERY).DataType = System.Type.GetType("System.String")
                .Cols(COL_PRE_TERM_LABOR).DataType = System.Type.GetType("System.String")
                .Cols(COL_COMMENTS).DataType = System.Type.GetType("System.String")

                .Cols(COL_BIRTH_WEIGHT).Format = "####.##"

                Try
                    If (.Styles.Contains("Gender")) Then
                        cStyleGender = .Styles("Gender")
                    Else
                        cStyleGender = .Styles.Add("Gender")
                    End If
                Catch ex As Exception
                    cStyleGender = .Styles.Add("Gender")
                End Try

                cStyleGender.ComboList = " |Female|Male"
                .Cols(COL_GENDER).Style = cStyleGender

                Try
                    If (.Styles.Contains("DeliveryType")) Then
                        cStyleDeliveryType = .Styles("DeliveryType")
                    Else
                        cStyleDeliveryType = .Styles.Add("DeliveryType")
                    End If
                Catch ex As Exception
                    cStyleDeliveryType = .Styles.Add("DeliveryType")
                End Try

                lstDeliveryTypes = New ListDictionary

                If Not IsNothing(dtDeliveryTypes) AndAlso dtDeliveryTypes.Rows.Count > 0 Then
                    For i As Int32 = 0 To dtDeliveryTypes.Rows.Count - 1
                        lstDeliveryTypes.Add(dtDeliveryTypes.Rows(i)("nCategoryID"), dtDeliveryTypes.Rows(i)("sDescription"))
                    Next
                End If

                cStyleDeliveryType.DataMap = lstDeliveryTypes
                .Cols(COL_DELIVERY_TYPE).Style = cStyleDeliveryType

                For i As Int16 = 0 To dt.Rows.Count - 1
                    .Rows.Add()

                    .SetData(i + 1, COL_PAST_PREGNANCIES_ID, dt.Rows(i)("PastPregnanciesID"))
                    .SetData(i + 1, COL_PATIENT_ID, dt.Rows(i)("nPatientID"))
                    .SetData(i + 1, COL_PREGNANCY_DATE, dt.Rows(i)("dtPregnancyDate"))
                    .SetData(i + 1, COL_GA_WEEKS, dt.Rows(i)("nGAWeeks"))
                    .SetData(i + 1, COL_LENGTH_OF_LABOR, dt.Rows(i)("sLengthOfLabor"))
                    .SetData(i + 1, COL_BIRTH_WEIGHT, dt.Rows(i)("nBirthWeight"))
                    .SetData(i + 1, COL_GENDER, dt.Rows(i)("sGender"))
                    .SetData(i + 1, COL_DELIVERY_TYPE, dt.Rows(i)("nDeliveryType"))
                    .SetData(i + 1, COL_ANES, dt.Rows(i)("sAnes"))
                    .SetData(i + 1, COL_PLACE_OF_DELIVERY, dt.Rows(i)("sPlaceOfDelivery"))
                    .SetData(i + 1, COL_PRE_TERM_LABOR, dt.Rows(i)("sPreTermLabor"))
                    .SetData(i + 1, COL_COMMENTS, dt.Rows(i)("sComments"))

                Next

                .AllowResizing = AllowResizingEnum.Columns
                .Row = 1
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PastPregnancies, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        Finally
        End Try
    End Sub


#Region "From Control Events "

    Private Sub tblbtn_Close_Click(sender As System.Object, e As System.EventArgs) Handles tblbtn_Close.Click
        Try
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PastPregnancies, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub
    Private Function ConvertToDBObject(ByVal myObj As Object) As Object
        Dim myString As String = Convert.ToString(myObj)
        If (String.IsNullOrEmpty(myString)) Then
            Return DBNull.Value
        End If
        Return myString
    End Function
    Private Sub tblbtn_Ok_Click(sender As System.Object, e As System.EventArgs) Handles tblbtn_Ok.Click
        Dim dtData As DataTable = Nothing
        Dim drRow As DataRow = Nothing
        Dim oDBLayer As gloDatabaseLayer.DBLayer = Nothing
        Dim oDBParameter As gloDatabaseLayer.DBParameters = Nothing

        Try
            c1PastPregnancies.FinishEditing()

            dtData = New DataTable

            dtData.Columns.Add("PastPregnanciesID", System.Type.GetType("System.Int64"))
            dtData.Columns.Add("nPatientID", System.Type.GetType("System.Int64"))

            dtData.Columns.Add("nGAWeeks", System.Type.GetType("System.Int16"))
            dtData.Columns.Add("dtPregnancyDate", System.Type.GetType("System.DateTime"))
            dtData.Columns.Add("sLengthOfLabor", System.Type.GetType("System.String"))
            dtData.Columns.Add("nBirthWeight", System.Type.GetType("System.Decimal"))
            dtData.Columns.Add("sGender", System.Type.GetType("System.String"))
            dtData.Columns.Add("nDeliveryType", System.Type.GetType("System.Int64"))
            dtData.Columns.Add("sAnes", System.Type.GetType("System.String"))
            dtData.Columns.Add("sPlaceOfDelivery", System.Type.GetType("System.String"))
            dtData.Columns.Add("sPreTermLabor", System.Type.GetType("System.String"))
            dtData.Columns.Add("sComments", System.Type.GetType("System.String"))



            For i As Int16 = 1 To c1PastPregnancies.Rows.Count - 1
                If Not IsNothing(c1PastPregnancies.GetData(i, COL_PAST_PREGNANCIES_ID)) Then
                    If String.IsNullOrEmpty(Convert.ToString(c1PastPregnancies.GetData(i, COL_PREGNANCY_DATE))) Then
                        MessageBox.Show("Select Pregnancy Date.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        c1PastPregnancies.Focus()
                        c1PastPregnancies.Select(i, COL_PREGNANCY_DATE)
                        Exit Sub
                    End If

                    If Not String.IsNullOrEmpty(Convert.ToString(c1PastPregnancies.GetData(i, COL_GA_WEEKS))) AndAlso Convert.ToInt16(c1PastPregnancies.GetData(i, COL_GA_WEEKS)) < 0 Then
                        MessageBox.Show("GA Weeks must be greater than zero.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        c1PastPregnancies.Focus()
                        c1PastPregnancies.Select(i, COL_GA_WEEKS)
                        Exit Sub
                    End If

                    If Not String.IsNullOrEmpty(Convert.ToString(c1PastPregnancies.GetData(i, COL_GA_WEEKS))) AndAlso Convert.ToString(c1PastPregnancies.GetData(i, COL_GA_WEEKS)).Length > 5 Then
                        MessageBox.Show("GA Weeks exceeds the max limit.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        c1PastPregnancies.Focus()
                        c1PastPregnancies.Select(i, COL_GA_WEEKS)
                        Exit Sub
                    End If

                    If Not String.IsNullOrEmpty(Convert.ToString(c1PastPregnancies.GetData(i, COL_LENGTH_OF_LABOR))) AndAlso Convert.ToString(c1PastPregnancies.GetData(i, COL_LENGTH_OF_LABOR)).Length > 100 Then
                        MessageBox.Show("Length of Labor exceeds the max limit.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        c1PastPregnancies.Focus()
                        c1PastPregnancies.Select(i, COL_LENGTH_OF_LABOR)
                        Exit Sub
                    End If

                    If Not String.IsNullOrEmpty(Convert.ToString(c1PastPregnancies.GetData(i, COL_BIRTH_WEIGHT))) AndAlso (Convert.ToInt64(c1PastPregnancies.GetData(i, COL_BIRTH_WEIGHT)) < 0 OrElse Convert.ToString(c1PastPregnancies.GetData(i, COL_BIRTH_WEIGHT)).StartsWith("-")) Then
                        MessageBox.Show("Birth Weight must be greater than zero.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        c1PastPregnancies.Focus()
                        c1PastPregnancies.Select(i, COL_BIRTH_WEIGHT)
                        Exit Sub
                    End If

                    If Not String.IsNullOrEmpty(Convert.ToString(c1PastPregnancies.GetData(i, COL_BIRTH_WEIGHT))) AndAlso Convert.ToString(c1PastPregnancies.GetData(i, COL_BIRTH_WEIGHT)).Replace(".", "").Length > 18 Then
                        MessageBox.Show("Birth Weight exceeds the max limit.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        c1PastPregnancies.Focus()
                        c1PastPregnancies.Select(i, COL_BIRTH_WEIGHT)
                        Exit Sub
                    End If

                    If Not String.IsNullOrEmpty(Convert.ToString(c1PastPregnancies.GetData(i, COL_ANES))) AndAlso Convert.ToString(c1PastPregnancies.GetData(i, COL_ANES)).Length > 100 Then
                        MessageBox.Show("Anes exceeds the max limit.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        c1PastPregnancies.Focus()
                        c1PastPregnancies.Select(i, COL_ANES)
                        Exit Sub
                    End If

                    If Not String.IsNullOrEmpty(Convert.ToString(c1PastPregnancies.GetData(i, COL_PLACE_OF_DELIVERY))) AndAlso Convert.ToString(c1PastPregnancies.GetData(i, COL_PLACE_OF_DELIVERY)).Length > 200 Then
                        MessageBox.Show("Place of Delivery exceeds the max limit.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        c1PastPregnancies.Focus()
                        c1PastPregnancies.Select(i, COL_PLACE_OF_DELIVERY)
                        Exit Sub
                    End If

                    If Not String.IsNullOrEmpty(Convert.ToString(c1PastPregnancies.GetData(i, COL_PRE_TERM_LABOR))) AndAlso Convert.ToString(c1PastPregnancies.GetData(i, COL_PRE_TERM_LABOR)).Length > 200 Then
                        MessageBox.Show("Pre Term Labor exceeds the max limit.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        c1PastPregnancies.Focus()
                        c1PastPregnancies.Select(i, COL_PRE_TERM_LABOR)
                        Exit Sub
                    End If

                    If Not String.IsNullOrEmpty(Convert.ToString(c1PastPregnancies.GetData(i, COL_COMMENTS))) AndAlso Convert.ToString(c1PastPregnancies.GetData(i, COL_COMMENTS)).Length > 500 Then
                        MessageBox.Show("Comments exceeds the max limit.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        c1PastPregnancies.Focus()
                        c1PastPregnancies.Select(i, COL_COMMENTS)
                        Exit Sub
                    End If

                    drRow = dtData.NewRow()

                    drRow("PastPregnanciesID") = c1PastPregnancies.GetData(i, COL_PAST_PREGNANCIES_ID)
                    drRow("nPatientID") = c1PastPregnancies.GetData(i, COL_PATIENT_ID)
                    drRow("nGAWeeks") = ConvertToDBObject(c1PastPregnancies.GetData(i, COL_GA_WEEKS))
                    drRow("dtPregnancyDate") = c1PastPregnancies.GetData(i, COL_PREGNANCY_DATE)
                    drRow("sLengthOfLabor") = ConvertToDBObject(c1PastPregnancies.GetData(i, COL_LENGTH_OF_LABOR))
                    drRow("nBirthWeight") = ConvertToDBObject(c1PastPregnancies.GetData(i, COL_BIRTH_WEIGHT))
                    drRow("sGender") = ConvertToDBObject(c1PastPregnancies.GetData(i, COL_GENDER))
                    drRow("nDeliveryType") = ConvertToDBObject(c1PastPregnancies.GetData(i, COL_DELIVERY_TYPE))
                    drRow("sAnes") = ConvertToDBObject(c1PastPregnancies.GetData(i, COL_ANES))
                    drRow("sPlaceOfDelivery") = ConvertToDBObject(c1PastPregnancies.GetData(i, COL_PLACE_OF_DELIVERY))
                    drRow("sPreTermLabor") = ConvertToDBObject(c1PastPregnancies.GetData(i, COL_PRE_TERM_LABOR))
                    drRow("sComments") = ConvertToDBObject(c1PastPregnancies.GetData(i, COL_COMMENTS))
                    dtData.Rows.Add(drRow)
                    drRow = Nothing
                End If
            Next


            If Not IsNothing(dtData) AndAlso dtData.Rows.Count > 0 Then
                oDBLayer = New gloDatabaseLayer.DBLayer(GetConnectionString())

                oDBParameter = New gloDatabaseLayer.DBParameters()
                oDBParameter.Add("@SavePastPregnancies", dtData, ParameterDirection.Input, SqlDbType.Structured)

                oDBLayer.Connect(False)
                oDBLayer.Execute("gsp_InUpPastPregnancies", oDBParameter)
                oDBLayer.Disconnect()

                'FillPastPregnanciesGrid()
            End If

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PastPregnancies, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Save, "Patient Past Pregnancies Modified", nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, True)
            blnSaveClicked = True
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PastPregnancies, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Save, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        Finally
            If Not IsNothing(oDBParameter) Then
                oDBParameter.Dispose()
                oDBParameter = Nothing
            End If

            If Not IsNothing(oDBLayer) Then
                oDBLayer.Dispose()
                oDBLayer = Nothing
            End If

            drRow = Nothing

            If Not IsNothing(dtData) Then
                dtData.Dispose()
                dtData = Nothing
            End If
        End Try
    End Sub

    Private Sub tblbtn_Delete_Click(sender As System.Object, e As System.EventArgs) Handles tblbtn_Delete.Click
        Dim oDBLayer As gloDatabaseLayer.DBLayer = Nothing
        Dim sSQL As String = String.Empty

        Try
            c1PastPregnancies.Focus()

            If c1PastPregnancies.Rows.Count > 0 Then
                Dim iPastPregnanciesID As Int64 = Convert.ToInt64(c1PastPregnancies.GetData(c1PastPregnancies.Row, COL_PAST_PREGNANCIES_ID))

                If iPastPregnanciesID <= 0 Then
                    c1PastPregnancies.Rows.Remove(c1PastPregnancies.Row)
                Else
                    If (MessageBox.Show("Are you sure you want to delete the record?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
                        oDBLayer = New gloDatabaseLayer.DBLayer(GetConnectionString())
                        oDBLayer.Connect(False)

                        sSQL = "DELETE FROM PastPregnancies WHERE PastPregnanciesID = " & iPastPregnanciesID

                        If oDBLayer.Execute_Query(sSQL) > 0 Then
                            c1PastPregnancies.Rows.Remove(c1PastPregnancies.Row)

                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PastPregnancies, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "Patient Past Pregnancy record removed", nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, True)
                        End If

                        oDBLayer.Disconnect()
                    End If
                End If
            Else
                MessageBox.Show("No Past Pregnancies record available for deletion.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PastPregnancies, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        Finally
            sSQL = Nothing

            If Not IsNothing(oDBLayer) Then
                oDBLayer.Dispose()
                oDBLayer = Nothing
            End If
        End Try
    End Sub

#End Region '"Form Control Events "

#Region " C1 Grid Events"

    Private Sub c1PastPregnancies_AfterAddRow(sender As Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles c1PastPregnancies.AfterAddRow
        Try
            c1PastPregnancies.SetData(e.Row, COL_PAST_PREGNANCIES_ID, 0)
            c1PastPregnancies.SetData(e.Row, COL_PATIENT_ID, nPatientID)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PastPregnancies, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    Private Sub c1PastPregnancies_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles c1PastPregnancies.MouseMove
        Try
            ShowToolTip(C1SuperTooltip1, sender, e.Location)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PastPregnancies, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    Private Sub ShowToolTip(ByVal oC1ToolTip As C1.Win.C1SuperTooltip.C1SuperTooltip, ByVal oGrid As C1.Win.C1FlexGrid.C1FlexGrid, ByVal nLocation As System.Drawing.Point)
        Try
            Dim myFont As Font = oGrid.Font
            Dim stringsize As SizeF
            Dim colsize As Integer = 0
            Dim sText As String = ""
            Dim nRow As Integer
            Dim nCol As Integer

            If oGrid.MouseCol > -1 And oGrid.MouseRow > -1 Then
                oC1ToolTip.Font = myFont
                'oC1ToolTip.MaximumWidth = 400

                nRow = oGrid.MouseRow
                nCol = oGrid.MouseCol

                If nRow > 0 Then 'And nCol > 0 Then
                    If Not oGrid.GetData(nRow, nCol) Is Nothing Then
                        'sText = oGrid.GetData(nRow, nCol).ToString()
                        'TO RESOLVED 8821
                        sText = oGrid.GetData(nRow, nCol)

                        If Not String.IsNullOrEmpty(sText) AndAlso nCol = COL_DELIVERY_TYPE Then
                            If Not IsNothing(dtDeliveryTypes) AndAlso dtDeliveryTypes.Rows.Count > 0 Then
                                sText = (From s In dtDeliveryTypes.AsEnumerable() Where String.Compare(s.Field(Of Decimal)("nCategoryID").ToString(), sText, True) = 0 Select s.Field(Of String)("sDescription")).FirstOrDefault
                            End If
                        End If
                    End If
                    colsize = oGrid.Cols(nCol).WidthDisplay

                End If
                Dim oGrp As Graphics = oGrid.CreateGraphics()
                Dim chars As Integer
                Dim lines As Integer
                stringsize = oGrp.MeasureString(sText.ToString(), myFont, SizeF.Empty, StringFormat.GenericDefault(), chars, lines)
                ''Code Review Changes: Dispose Graphics object
                oGrp.Dispose()
                '' oGrid.GetCellRect(nRow, nCol).Height
                'If stringsize.Width > colsize Or lines > 1 And oGrid.GetCellRect(nRow, nCol).Height < (19 * lines) Then
                If stringsize.Width > colsize Or lines > 1 Then
                    'oC1ToolTip.SetToolTip(oGrid, sText.ToString())
                    ''TO RESOLVED 8821
                    oC1ToolTip.SetToolTip(oGrid, sText)
                Else
                    oC1ToolTip.SetToolTip(oGrid, "")
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PastPregnancies, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub


    Private Sub c1PastPregnancies_ValidateEdit(sender As Object, e As C1.Win.C1FlexGrid.ValidateEditEventArgs) Handles c1PastPregnancies.ValidateEdit
        Dim tb As TextBox = Nothing
        Try
            If c1PastPregnancies.Cols(e.Col).DataType = GetType(Int16) Then
                tb = New TextBox
                tb = DirectCast(c1PastPregnancies.Editor, TextBox)

                If tb.Text = "" Then
                    c1PastPregnancies(e.Row, e.Col) = Nothing
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PastPregnancies, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        Finally
            tb = Nothing
        End Try
    End Sub

#End Region '" C1 Grid Events"
End Class