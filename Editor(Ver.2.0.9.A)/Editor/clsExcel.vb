Imports System.Runtime.InteropServices

'クラス概要
'EXCELを操作するクラス
'
Public Class clsExcel
    Private objExcel As Object = Nothing     'Excel.Application
    Private objWorkBooks As Object = Nothing 'Excel.WorkBooks
    Private objWorkBook As Object = Nothing  'Excel.Workbook
    Private objSheets As Object = Nothing    'Excel.Worksheets
    Private objSheet As Object = Nothing     'Excel.Worksheet

    'EXCELファイルをブートする
    Public Function openEXCEL(pstrExcelPath As String) As Boolean
        '引数：EXCELファイルのフルパス
        '戻り値：正常終了=True、異常終了=False
        Dim bRet As Boolean '戻り値

        bRet = False

        Try
            'EXCELをブート
            objExcel = CreateObject("Excel.Application")

            'EXCELをディスプレイに表示させない
            objExcel.Visible = False
            objExcel.DisplayAlerts = False

            'WorkBooksを格納
            objWorkBooks = objExcel.Workbooks

            '引数で与えられたファイルオープン
            objWorkBook = objWorkBooks.Open(pstrExcelPath)

            'シートSオブジェクトセット
            objSheets = objWorkBook.WorkSheets

            bRet = True
        Catch ex As Exception
            '異常が起きたらFalse
            bRet = False
        End Try

        Return bRet
    End Function

    'EXCELを終了させる後始末
    Public Sub subExcelEND()
        '下階層から順次終了させないとゴミが残るため、この処理で一括処理
        ' EXCEL終了処理
        If Not IsNothing(objSheet) Then
            Marshal.ReleaseComObject(objSheet)        'オブジェクト参照を解放
            objSheet = Nothing                        'オブジェクト解放
        End If
        If Not IsNothing(objSheets) Then
            Marshal.ReleaseComObject(objSheets)       'オブジェクト参照を解放
            objSheets = Nothing                       'オブジェクト解放
        End If
        If Not IsNothing(objWorkBook) Then
            objWorkBook.Close(False)                    'ファイルを閉じる
            Marshal.ReleaseComObject(objWorkBook)       'オブジェクト参照を解放
            objWorkBook = Nothing                       'オブジェクト解放
        End If

        If Not IsNothing(objWorkBooks) Then
            Marshal.ReleaseComObject(objWorkBooks)      'オブジェクト参照を解放
            objWorkBooks = Nothing                      'オブジェクト解放
        End If

        If Not IsNothing(objExcel) Then
            objExcel.Quit()                             'EXCELを閉じる
            Marshal.ReleaseComObject(objExcel)          'オブジェクト参照を解放
            objExcel = Nothing                          'オブジェクト解放
        End If
        System.GC.Collect()                             'オブジェクトを確実に削除
    End Sub

    'Sheet数を戻す
    Public Function fnGetSheetCount() As Integer
        Return objSheets.Count
    End Function

    'シート番号を指定してシートをセット
    Public Function fnSetSheet(pintSheet As Integer) As Boolean
        Dim bRet As Boolean = False
        Try

            '該当セルの値を書き込む
            objSheet = objSheets(pintSheet)

            bRet = True
        Catch ex As Exception

        End Try

        Return bRet
    End Function

    'シート名、Row,Colセルを指定して値を取得する関数
    Public Function fnGetExcelData(pstrSheet As String, pintROWcell As Integer, pintCOLcell As Integer, ByRef pstrVal As String) As Boolean
        '引数：シート名、セルROW座標、セルCOL座標、取得した値
        '戻り値：True,False 正常、異常

        Dim bRet As Boolean = False
        Dim objSheet As Object = Nothing     'Excel.Worksheet

        Try
            'シートを取得
            objSheets = objWorkBook.WorkSheets
            For Each objSheet In objSheets
                If objSheet.Name = pstrSheet Then
                    'シート名が一致した場合のみ
                    '該当セルの値を取得する
                    Dim oRange As Object = objSheet.Cells(pintROWcell, pintCOLcell)
                    pstrVal = oRange.Value
                    bRet = True
                    Exit For
                End If
            Next
        Catch ex As Exception
            bRet = False
        End Try

        Return bRet
    End Function

    'Row,Colを指定して値をセットする関数(配列指定で一括書き込み)
    'Public Function fnSetExcelData(pintROWcell As Integer, pintCOLcell As Integer, pstrVal(,) As String) As Boolean
    '    '引数：シート番号、セルROW座標、セルCOL座標、取得した値
    '    '戻り値：True,False 正常、異常

    '    Dim bRet As Boolean = False

    '    Try
    '        '該当セルの値を書き込む
    '        ' B5:FV
    '        Dim cells As Object = objSheet.Cells
    '        Dim tl As Object = cells(pintROWcell, pintCOLcell)
    '        Dim br As Object = cells(150, Max_Col_Count)

    '        Dim oRange As Object = objSheet.Range(tl, br)
    '        oRange.Value = pstrVal
    '        bRet = True

    '        If Not IsNothing(oRange) Then
    '            Marshal.ReleaseComObject(oRange)       'オブジェクト参照を解放
    '            oRange = Nothing                       'オブジェクト解放
    '        End If
    '    Catch ex As Exception
    '        bRet = False
    '    End Try

    '    Return bRet
    'End Function

    'シート名を変更する関数
    Public Function fnSetSheetName(pstrSheetName As String) As Boolean
        Dim bRet As Boolean = False

        Try
            objSheet.Name = pstrSheetName
            bRet = True
        Catch ex As Exception
            bRet = False
        End Try

        Return bRet
    End Function


    'EXCELを名前を付けて保存する
    Public Function fnExcelSaveAs(pstrExcelPath As String) As Boolean
        Dim bRet As Boolean = False
        Try
            objWorkBook.SaveAs(pstrExcelPath)
            bRet = True
        Catch ex As Exception
            bRet = False
        End Try

        Return bRet
    End Function
End Class
