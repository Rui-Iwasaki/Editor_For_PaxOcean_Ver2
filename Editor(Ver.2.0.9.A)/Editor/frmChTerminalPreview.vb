Public Class frmChTerminalPreview
#Region "変数"

#End Region

#Region "画面"
    Private Sub btnPreview_Click(sender As System.Object, e As System.EventArgs) Handles btnPreview.Click
        '印刷プレビューﾎﾞﾀﾝ
        Try
            ''端子台情報の取得
            Dim udtFuInfo() As gTypFuInfo = Nothing
            Call gMakeFuInfoStructure(udtFuInfo)

            Dim intScFlg As Integer = 0
            Dim intDmyFlg As Integer = 0
            Dim intPagePrint As Integer = 0
            Dim intFuNameType As Integer = 0
            intScFlg = IIf(chkSecretChannel.Checked, 1, 0)
            intDmyFlg = IIf(chkDummyData.Checked, 1, 0)
            intPagePrint = IIf(chkPagePrint.Checked, 1, 0)  ''ページ印刷 2013.10.18
            intFuNameType = IIf(chkFuName.Checked, 1, 0)    '' FIELD UNIT名称表記  2013.11.15

            Call frmPrtTerminalPreview.gShow(udtFuInfo, _
                                             False, _
                                             True, _
                                             0, _
                                             0, _
                                             intScFlg, _
                                             intDmyFlg, _
                                             intPagePrint, _
                                             intFuNameType, _
                                             gintTermFuNo, _
                                             gintTermSlotNo)

            Me.Close()
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    Private Sub btnExit_Click(sender As System.Object, e As System.EventArgs) Handles btnExit.Click
        'Close ﾎﾞﾀﾝ
        Me.Close()
    End Sub
#End Region

#Region "関数"

#End Region

    Private Sub frmChTerminalPreview_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class