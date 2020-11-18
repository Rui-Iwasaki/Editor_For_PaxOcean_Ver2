Public Class frmChOutputSelect

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 本画面を表示する
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Sub gShow()

        Try

            ''本画面表示
            Call gShowFormModelessForCloseWait1(Me)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "画面イベント"

    '--------------------------------------------------------------------
    ' 機能      : フォームロード
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 画面表示初期処理を行う
    '--------------------------------------------------------------------
    Private Sub frmChOutputSelect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： DOボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdDO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDO.Click

        Try

            frmChOutputDoList.ShowDialog()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： AOボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdAO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAO.Click

        Try

            frmChOutputAoList.ShowDialog()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

End Class

