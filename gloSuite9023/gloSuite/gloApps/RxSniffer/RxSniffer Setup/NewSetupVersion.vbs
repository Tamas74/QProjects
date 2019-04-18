''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''  Change the product name version number of an MSI setup project
''  and update relevant GUIDs
''  
''  S. Lakshmanaraj 8/25/2015  
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
set a = wscript.arguments
if a.count = 0 then wscript.quit 1

'read and backup project file
Set fso = CreateObject("Scripting.FileSystemObject")
Set f = fso.OpenTextFile(a(0))
s = f.ReadAll
f.Close
fbak = a(0) & ".bak"
if fso.fileexists(fbak) then fso.deletefile fbak
fso.movefile a(0), fbak
set re = new regexp
re.global = true

'find, increment and replace version number
're.pattern = "(""ProductVersion"" = ""8:)(\d+(\.\d+)+)"""
'set m = re.execute(s)
'v = m(0).submatches(1)
'v1 = split(v, ".")
'v1(ubound(v1)) = v1(ubound(v1)) + 1
'vnew = join(v1, ".")
''msgbox v & " --> " & vnew
's = re.replace(s, "$1" & vnew & """")

'replace ProductCode

re.pattern = "(""ProductCode"" = ""8:)(\{.+\})"""
guid = CreateObject("Scriptlet.TypeLib").Guid
guid = left(guid, len(guid) - 2)
if a.count > 1 then s = re.replace(s, "$1" & guid & """")

'replace PackageCode
re.pattern = "(""PackageCode"" = ""8:)(\{.+\})"""
guid = CreateObject("Scriptlet.TypeLib").Guid
guid = left(guid, len(guid) - 2)
if a.count > 1 then s = re.replace(s, "$1" & guid & """")

'replace UpgradeCode
re.pattern = "(""UpgradeCode"" = ""8:)(\{.+\})"""
guid = CreateObject("Scriptlet.TypeLib").Guid
guid = left(guid, len(guid) - 2)
s = re.replace(s, "$1" & guid & """")

'replace ProductName
re.pattern = "(?:\""ProductName\"" = \""8:.*)"
if a.count > 1 then s = re.Replace(s, """ProductName"" = ""8:" & a(1) & """"+chr(13)+chr(10))

'write project file
fnew = a(0)
set f = fso.CreateTextfile(fnew, true)
f.write(s)
f.close
