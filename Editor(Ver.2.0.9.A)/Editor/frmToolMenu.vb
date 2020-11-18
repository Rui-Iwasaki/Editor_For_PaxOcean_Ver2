Public Class frmToolMenu

#Region "画面"
    Private Sub btnChkChList_Click(sender As System.Object, e As System.EventArgs) Handles btnChkChList.Click
        '計測点検索画面
        Call frmToolChkChList.ShowDialog()
    End Sub

    'Private Sub btnExcel_Click(sender As System.Object, e As System.EventArgs) Handles btnExcel.Click
    '    'Excelコンバータ起動
    '    Dim strAppPath As String
    '    'プログラム実行フォルダ取得
    '    strAppPath = gGetAppPath()
    '    'Excelパス作成
    '    Dim strExcelEXE As String = System.IO.Path.Combine(strAppPath, "EXCELCNV")

    '    'exeキック
    '    Dim Ret As Long
    '    Ret = Shell(strExcelEXE + "\CHList_EXCEL.exe", vbNormalFocus)
    'End Sub

    Private Sub btnGetCHnoToFUadr_Click(sender As System.Object, e As System.EventArgs) Handles btnGetCHnoToFUadr.Click
        'CHnoからFuアドレスの一覧を生成する画面
        Call frmToolGetCHnoToFUadr.ShowDialog()
    End Sub

	Private Sub btnGrpDisp_Click(sender As System.Object, e As System.EventArgs) Handles btnGrpDisp.Click
		'Grp設定を見る画面
		Call frmToolDispGrp.ShowDialog()
	End Sub

	Private Sub btnOutput_Click(sender As Object, e As EventArgs) Handles btnOutput.Click
		'Grp設定を見る画面
		Call frmToolDispOutOrAnd.ShowDialog()
	End Sub

	Private Sub btnExit_Click(sender As System.Object, e As System.EventArgs) Handles btnExit.Click
        'EXITボタン
        Me.Close()
    End Sub


#End Region

#Region "関数"

#End Region


End Class