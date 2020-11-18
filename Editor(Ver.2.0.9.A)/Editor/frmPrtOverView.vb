Public Class frmPrtOverView

#Region "画面イベント"

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームロード
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmPrtOverView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            Dim pd As New System.Drawing.Printing.PrintDocument

            lblPrinter.Text = pd.PrinterSettings.PrinterName
            pd.Dispose()

            ''コンボボックス初期設定
            cmbSelectPart.Items.Add(gCstNamePrintCmbSelectPartMach)
            cmbSelectPart.Items.Add(gCstNamePrintCmbSelectPartCarg)

            ''PartSelectコンボでMachinery選択
            cmbSelectPart.SelectedItem = gCstNamePrintCmbSelectPartMach

            ''コンバイン設定時以外はパート選択不可　ver.1.4.0 2011.08.01
            If gudt.SetSystem.udtSysSystem.shtCombineUse = 0 Then
                cmbSelectPart.Enabled = False
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Previewボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPreview.Click

        Try

            Dim strHistoryNo As String = txtHistoryNo.Text
            Dim blnSelectMachinery As Boolean

            ''Select Part情報取得
            If cmbSelectPart.SelectedItem = gCstNamePrintCmbSelectPartMach Then blnSelectMachinery = True
            If cmbSelectPart.SelectedItem = gCstNamePrintCmbSelectPartCarg Then blnSelectMachinery = False

            ''プレビュー画面表示
            frmPrtOverViewPreview.gShow(strHistoryNo, _
                                        chkShipNo.Checked, _
                                        gCstCodePrintGraphViewPrintModePreview, _
                                        blnSelectMachinery)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Printボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click

        Try
            '' Ver1.10.1 2016.02.26 ﾌﾟﾘﾝﾀﾀﾞｲｱﾛｸﾞでｷｬﾝｾﾙできるので、確認ﾒｯｾｰｼﾞを表示しないように変更
            ''If MsgBox("Do you start printing?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Print") = MsgBoxResult.Yes Then

            Dim PrintDialog1 As New PrintDialog()
            Dim strHistoryNo As String = txtHistoryNo.Text
            Dim blnSelectMachinery As Boolean

            ''Select Part情報取得
            If cmbSelectPart.SelectedItem = gCstNamePrintCmbSelectPartMach Then blnSelectMachinery = True
            If cmbSelectPart.SelectedItem = gCstNamePrintCmbSelectPartCarg Then blnSelectMachinery = False

            ''ファイルへ出力 チェックボックスを無効にする 
            PrintDialog1.AllowPrintToFile = False

            PrintDialog1.PrinterSettings = New System.Drawing.Printing.PrinterSettings()

            ''プレビュー画面表示
            Call frmPrtOverViewPreview.gShow(txtHistoryNo.Text, _
                                             chkShipNo.Checked, _
                                             gCstCodePrintGraphViewPrintModePrint, _
                                             blnSelectMachinery)

            PrintDialog1.Dispose()

            ''End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Exitボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click

        Try

            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームクローズ
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmPrtOverView_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub



#Region "入力制限"

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtFileName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtHistoryNo.KeyPress

        Try

            e.Handled = gCheckTextInput(6, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region



#End Region

#Region "内部関数"



#End Region

End Class
