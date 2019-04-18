Imports System.Windows.Forms
Imports System.Drawing

Public Class cls_MU_General
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    Private _databaseConnectionString As String = String.Empty
    Private _MessageBoxCaption As String = String.Empty
    'Public gFont As Font = New Font("Tahoma", 9, FontStyle.Regular)
    'Public gFont_SMALL As Font = New Font("Tahoma", 8.25!, FontStyle.Regular)
    'Public gFont_SMALL_BOLD As Font = New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CType((0), Byte)))
    'Public gFont_BOLD = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Public gstrExamTypes As String = ""
    Public gstrVisitTypes As String = ""

    Public Function ReplaceSpecialCharacters(ByVal strSpecialChar As String) As String
        Try

            strSpecialChar = Replace(strSpecialChar, "#", "[#]") & ""
            strSpecialChar = Replace(strSpecialChar, "$", "[$]") & ""
            strSpecialChar = Replace(strSpecialChar, "%", "[%]") & ""
            strSpecialChar = Replace(strSpecialChar, "^", "[^]") & ""
            strSpecialChar = Replace(strSpecialChar, "&", "[&]") & ""

            '' Was Commented Before 2090602
            '' Uncommneted By Mahesh to Handle the Special Char in search By Replacing char with '[Char]'
            '' Ref: http://sqlserver2000.databases.aspfaq.com/how-do-i-search-for-special-characters-e-g-in-sql-server.html
            strSpecialChar = Replace(strSpecialChar, "~", "[~]") & ""
            strSpecialChar = Replace(strSpecialChar, "!", "[!]") & ""
            strSpecialChar = Replace(strSpecialChar, "*", "[*]") & ""
            strSpecialChar = Replace(strSpecialChar, ";", "[;]") & ""
            strSpecialChar = Replace(strSpecialChar, "/", "[/]") & ""
            strSpecialChar = Replace(strSpecialChar, "?", "[?]") & ""
            strSpecialChar = Replace(strSpecialChar, ">", "[>]") & ""
            strSpecialChar = Replace(strSpecialChar, "<", "[<]") & ""
            strSpecialChar = Replace(strSpecialChar, "\", "[\]") & ""
            strSpecialChar = Replace(strSpecialChar, "|", "[|]") & ""
            strSpecialChar = Replace(strSpecialChar, "{", "[{]") & ""
            strSpecialChar = Replace(strSpecialChar, "}", "[}]") & ""
            strSpecialChar = Replace(strSpecialChar, "-", "[-]") & ""
            strSpecialChar = Replace(strSpecialChar, "_", "[_]") & ""
            ''END Was Commented Before 2090602
            Return strSpecialChar
        Catch ex As Exception
            MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Public Sub ValidateText(ByVal Text As String, ByVal e As KeyPressEventArgs)
        Try
            If InStr(Trim(Text), ".") <> 0 And (e.KeyChar = ChrW(46)) Or ((e.KeyChar >= ChrW(35) And e.KeyChar <= ChrW(38)) Or (e.KeyChar = ChrW(64)) Or (e.KeyChar = ChrW(33)) Or (e.KeyChar = ChrW(42)) Or (e.KeyChar = ChrW(43)) Or (e.KeyChar = ChrW(45)) Or (e.KeyChar = ChrW(60)) Or (e.KeyChar = ChrW(59)) Or (e.KeyChar = ChrW(61)) Or (e.KeyChar = ChrW(94)) Or (e.KeyChar = ChrW(96)) Or (e.KeyChar = ChrW(124)) Or (e.KeyChar = ChrW(125)) Or (e.KeyChar = ChrW(126))) Then
                e.Handled = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Public Sub New()
        If appSettings("MessageBOXCaption") IsNot Nothing Then
            If appSettings("MessageBOXCaption") <> "" Then
                _MessageBoxCaption = Convert.ToString(appSettings("MessageBOXCaption"))
            End If
        End If

        If appSettings("DataBaseConnectionString") IsNot Nothing Then
            If appSettings("DataBaseConnectionString") <> "" Then
                _databaseConnectionString = Convert.ToString(appSettings("DataBaseConnectionString"))
            End If
        End If
    End Sub
    ''Sanjog -Added on 20101101 for insert Update log
    Public Sub UpdateLog(ByVal strLogMessage As String)
        Try


            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.CQMReport, gloAuditTrail.ActivityType.View, strLogMessage, gloAuditTrail.ActivityOutCome.Success)

            
        Catch ex As Exception

        End Try
    End Sub
    ''Sanjog -Added on 20101101 for insert Update log
End Class

Public Enum enum_CQMFilters
    NPI = 1
    TIN = 2
    ProviderAddress = 3
    Race = 4
    Payer = 5
    Ethnicity = 6
    Problem = 7
    Gender = 8
    Age = 9
    PracticeAddressLine1 = 10
    PracticeAddressLine2 = 11
    PracticeCity = 12
    PracticeState = 13
    PracticeZIP = 14
    PracticeCountry = 15
    PracticeCounty = 16
    ProviderTaxonomy = 17
End Enum