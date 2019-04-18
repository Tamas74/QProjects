Imports System.Data
Imports System.Data.SqlClient
Imports gloEMRGeneralLibrary.gloEMRDatabase
Public Class clsDAS


    Public Sub New()
        '    Dim sqlconn As String
        '   sqlconn = GetConnectionString()
        '        Conn = New System.Data.SqlClient.SqlConnection(sqlconn)
    End Sub

    '   Private Conn As SqlConnection

    Private _VitalID As String
    Private _IsDiagramSelected As Integer

    Public Property VitalID() As String
        Get
            Return _VitalID
        End Get
        Set(ByVal value As String)
            _VitalID = value
        End Set
    End Property


    Public Property IsDiagramSelected() As String
        Get
            Return _IsDiagramSelected
        End Get
        Set(ByVal value As String)
            _IsDiagramSelected = value
        End Set
    End Property

    Public nDASID As Int64
    Public nVitalID As Int64
    Public IsDiagram As Integer
    Public TenderJointCount As Integer
    Public SwollenJointCount As Integer
    Public IsESR As Integer
    Public ESRCount As Single
    Public IsCRP As Integer
    Public CRPCount As Single
    Public IsPainScale As Integer
    Public PainScore As Integer
    Public FormulaName As String
    Public CalculatedFormula As String
    Public SelectedJointNodes As String
    Public DASValue As Single
    Public DASImage As Image

    'Private Cmd As System.Data.SqlClient.SqlCommand

    Public Function GetDAS(ByVal VitalID As Int64) As clsDAS

        Dim dt As DataTable = Nothing
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter = Nothing

        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nVitalID"
            oParamater.Value = VitalID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            dt = oDB.GetDataTable("Get_DAS")
            If (IsNothing(dt) = False) Then


                If dt.Rows.Count > 0 Then
                    nDASID = Convert.ToInt64(dt.Rows(0)("nDASID"))
                    nVitalID = Convert.ToInt64(dt.Rows(0)("nVitalID"))
                    IsDiagram = Convert.ToInt16(dt.Rows(0)("IsDiagram"))
                    TenderJointCount = Convert.ToInt16(dt.Rows(0)("TenderJointCount"))
                    SwollenJointCount = Convert.ToInt16(dt.Rows(0)("SwollenJointCount"))
                    IsESR = Convert.ToInt16(dt.Rows(0)("IsESR"))
                    ESRCount = Convert.ToSingle(dt.Rows(0)("ESRCount"))
                    IsCRP = Convert.ToInt16(dt.Rows(0)("IsCRP"))
                    CRPCount = Convert.ToSingle(dt.Rows(0)("CRPCount"))
                    IsPainScale = Convert.ToInt16(dt.Rows(0)("IsPainScale"))
                    PainScore = Convert.ToInt16(dt.Rows(0)("PainScore"))
                    FormulaName = Convert.ToString(dt.Rows(0)("FormulaName"))
                    CalculatedFormula = Convert.ToString(dt.Rows(0)("CalculatedFormula"))
                    SelectedJointNodes = Convert.ToString(dt.Rows(0)("SelectedJointNodes"))
                    DASValue = Convert.ToSingle(dt.Rows(0)("DASValue"))

                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oParamater) Then
                oParamater = Nothing
            End If

            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If

            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
        Return Nothing
    End Function

    Public Function InUp_DAS() As Int64

        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter = Nothing

        Try


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nVitalID"
            oParamater.Value = nVitalID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Bit
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@IsDiagram"
            oParamater.Value = IsDiagram
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TenderJointCount"
            oParamater.Value = TenderJointCount
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@SwollenJointCount"
            oParamater.Value = SwollenJointCount
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Bit
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@IsESR"
            oParamater.Value = IsESR
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Float
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ESRCount"
            oParamater.Value = ESRCount
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Bit
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@IsCRP"
            oParamater.Value = IsCRP
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Float
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@CRPCount"
            oParamater.Value = CRPCount
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Bit
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@IsPainScale"
            oParamater.Value = IsPainScale
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@PainScore"
            oParamater.Value = PainScore
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@FormulaName"
            oParamater.Value = FormulaName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@CalculatedFormula"
            oParamater.Value = CalculatedFormula
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@SelectedJointNodes"
            oParamater.Value = SelectedJointNodes
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Float
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DASValue"
            oParamater.Value = DASValue
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.InputOutput
            oParamater.Name = "@nDASID"
            oParamater.Value = nDASID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Image
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@iDASImage"
            oParamater.Value = ConvertImageToBytes(DASImage)
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            nDASID = oDB.Add("InUp_DAS")
            Return nDASID

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(oParamater) Then
                oParamater = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Public Function ConvertImageToBytes(ByVal imgDas As Image) As Byte()
        'Dim bytes() As Byte
        'If imgDas IsNot Nothing Then
        '    Dim BitmapConverter As System.ComponentModel.TypeConverter = System.ComponentModel.TypeDescriptor.GetConverter(imgDas.[GetType]())
        '    bytes = DirectCast(BitmapConverter.ConvertTo(imgDas, GetType(Byte())), Byte())
        'Else
        '    bytes = Nothing
        'End If
        'Return bytes
        Dim bitmapBytes As Byte()
        Using stream As New System.IO.MemoryStream
            imgDas.Save(stream, imgDas.RawFormat)
            bitmapBytes = stream.ToArray
        End Using
        Return bitmapBytes


    End Function

    Public Sub Delete_DAS(ByVal vitalid As Int64)
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter = Nothing

        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nVitalID"
            oParamater.Value = vitalid
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oDB.Delete("Delete_DAS")


        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Public Function GetDASValueFromLab(ByVal PatientID As Int64) As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter = Nothing
        Dim dt As DataTable = Nothing

        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@PatientID"
            oParamater.Value = PatientID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.DateTime
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ToDate"
            oParamater.Value = Date.Now
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            dt = oDB.GetDataTable("GetDASValueFromLab")
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(oParamater) Then
                oParamater = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try

    End Function


End Class
