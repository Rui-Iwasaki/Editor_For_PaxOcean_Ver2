Public Class frmToolExcelCnv
    '本画面は、フォーム起動後、タイマーを動かす。タイマー内でバッチ処理動作
    '　※Excelﾌｧｲﾙが存在しない場合は何もせずに正常終了
    '戻り値
    ' 0=正常終了
    '-1=Excelシートが36件以上＝グループ数オーバー

#Region "固定値"

#End Region

#Region "変数"
    Private prRet As Integer        '戻り値　正常=0
    Private printMode As Integer    'mode 0=Excel to MPT  1=MPT to Excel
    Private pstrPathBase As String  'ﾒｲﾝとなるパス

    Private clExcel As clsExcel     'Excel操作クラス
#End Region

#Region "画面"
    Friend Function gShow(ByVal pintMode As Integer) As Integer


        Try
            '引数保存
            printMode = pintMode
            prRet = 0

            ''画面表示
            Call Me.ShowDialog()

            Return prRet

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    'Form_Load
    Private Sub frmToolExcelCnv_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            'タイマースタート
            tmrStart.Interval = 100
            tmrStart.Enabled = True
            tmrStart.Start()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    'スタートタイマー
    Private Sub tmrStart_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrStart.Tick

        Try

            ''タイマーストップ
            tmrStart.Enabled = False
            tmrStart.Stop()

            Select Case printMode
                Case 0
                    'Excel To MPT
                    Call subMainEtoM()
                    Me.Close()
                Case 1
                    'MPT To Excel
                    Call subMainMtoE()
                    Me.Close()
            End Select
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    'Form_Closed=後始末
    Private Sub frmToolExcelCnv_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            Me.Dispose()
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

#End Region

#Region "関数"

#Region "Excel To MPT"
    'Excel To MPT メイン　最初に呼ばれる
    Private Sub subMainEtoM()
        Try
            pstrPathBase = System.IO.Path.Combine(gudtFileInfo.strFilePath, gudtFileInfo.strFileName)
            'Excelが存在しないなら処理抜け
            Dim strExcelPath As String = System.IO.Path.Combine(pstrPathBase, gudtFileInfo.strFileName & "_list.xlsx")
            If System.IO.File.Exists(strExcelPath) = False Then
                Return
            End If

            'Excelが存在するなら開いて内容をﾒﾓﾘに展開
            If fnGetExcelData(strExcelPath) = False Then
                Return
            End If


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    'Excelを開いてﾒﾓﾘ展開
    Private Function fnGetExcelData(pstrExcelPath As String) As Boolean
        Dim bRet As Boolean = False
        Try
            clExcel = New clsExcel

            'EXCELファイルオープン
            clExcel.openEXCEL(pstrExcelPath)
            prgBar.Value += 1

            'シート数が36件ではないならｸﾞﾙｰﾌﾟ数エラーとして終了
            Dim intCount As Integer = clExcel.fnGetSheetCount()
            If intCount <> 36 Then
                prRet = -1
                Return False
            End If


            bRet = True
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        Finally
            '最後はEXCELクラスを後始末する
            clExcel.subExcelEND()
            clExcel = Nothing
        End Try

        Return bRet
    End Function
#End Region

#Region "MPT To Excel"
    'MPT To Excel メイン　最初に呼ばれる
    Private Sub subMainMtoE()
        Try
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
#End Region

#End Region

End Class