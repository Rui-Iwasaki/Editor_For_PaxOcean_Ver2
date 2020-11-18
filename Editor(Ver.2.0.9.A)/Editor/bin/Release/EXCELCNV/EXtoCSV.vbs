Option Explicit

Call Main(WScript.Arguments)

Private Sub Main(args)
    Dim i
    For i = 0 To args.Count - 1
        Call SaveAsCSV(args(i))
    Next
End Sub

Private Sub SaveAsCSV(file)
    Dim xls
    Dim fpath
    Dim pos
    pos = InStrRev(file, "\")
    fpath = Left(file, pos)


    Set xls = CreateObject("Excel.Application")
    
    xls.Visible = False
    xls.DisplayAlerts = False
    
    xls.Workbooks.Open file
    
    With  xls.ActiveWorkbook
        Dim i
        For i = 1 To .Sheets.Count
            If .Sheets(i).Visible = True Then
                .Sheets(i).Select
                Dim csv 
                csv = fpath & .Sheets(i).Name & ".tmp55"
                .SaveAs csv, -4158
            End If
        Next
    End With
    
    xls.Quit
    Set xls = Nothing
End Sub
