Public Class frmPrtLocalUnit

#Region "定数定義"

    Private udtFuInfo() As gTypFuInfo = Nothing                 ''Fu情報構造体

    '印刷モード
    Private Const mCstCodePrintModePrintAll As Integer = 0
    Private Const mCstCodePrintModePrintPages As Integer = 1
    Private Const mCstCodePrintModePreview As Integer = 2

#End Region

#Region "画面イベント"

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームロード
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmPrtLocalUnit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            Dim i, intTemp As Integer

            ''プリンター名称の取得
            Dim pd As New System.Drawing.Printing.PrintDocument
            lblPrinter.Text = pd.PrinterSettings.PrinterName
            pd.Dispose()

            ''PartSelect削除　ver.1.4.0 2011.08.02

            ''端子台情報の取得  2013.11.02
            Call gMakeFuInfoStructure(udtFuInfo)

            ''ページ数の取得
            For i = 0 To UBound(gudt.SetFu.udtFu)
                If gudt.SetFu.udtFu(i).shtUse = 1 Then
                    intTemp = i + 1
                End If
            Next

            intTemp = CInt((intTemp) / 7 + 0.5)
            Call cmbPageRangeFrom.Items.Clear()
            Call cmbPageRangeTo.Items.Clear()
            For i = 1 To intTemp
                cmbPageRangeFrom.Items.Add(CStr(i))
                cmbPageRangeTo.Items.Add(CStr(i))
            Next

            ''PartSelect削除　ver.1.4.0 2011.08.02

            ''All選択
            optPageRangeAll.Checked = True

            '' Ver1.8.6.1  2015.12.03  初期ﾀｲﾌﾟで印字する場合はﾁｪｯｸを入れる
            If g_bytFUSet = 0 Then
                chkFormatType.Checked = True
            Else
                chkFormatType.Checked = False
            End If


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームクローズ
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmPrtLocalUnit_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

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
    ' 機能説明  ： Previewボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPreview.Click

        Try

            Dim intPageFrom As Integer = 0
            Dim intPageTo As Integer = 0
            Dim intPagePrint As Integer = 0 ''ページ印刷 2013.10.18

            ''OPTION項目の設定確認
            intPagePrint = IIf(chkPagePrint.Checked, 1, 0)  ''ページ印刷 2013.10.18

            ''PartSelect削除　ver.1.4.0 2011.08.02

            ''プレビュー画面表示
            Call frmPrtLocalUnitPreview.gShow(udtFuInfo, mCstCodePrintModePreview, intPageFrom, intPageTo, intPagePrint)

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

            Dim intPrintMode As Integer
            Dim intPageFrom As Integer = 0
            Dim intPageTo As Integer = 0
            Dim intPagePrint As Integer = 0

            ''OPTION項目の設定確認
            intPagePrint = IIf(chkPagePrint.Checked, 1, 0)  ''ページ印刷 2013.10.18

            ''PartSelect削除　ver.1.4.0 2011.08.02

            ''PrintMode選択
            If optPageRangeAll.Checked Then

                ''All
                intPrintMode = mCstCodePrintModePrintAll
                intPageFrom = 0
                intPageTo = 0

            Else

                ''設定値エラーの時は処理を抜ける
                If mChkInput() = False Then Exit Sub

                ''Pages
                intPrintMode = mCstCodePrintModePrintPages
                intPageFrom = CInt(cmbPageRangeFrom.Text)
                intPageTo = CInt(cmbPageRangeTo.Text)

            End If

            ''プレビュー画面表示
            Call frmPrtLocalUnitPreview.gShow(udtFuInfo, intPrintMode, intPageFrom, intPageTo, intPagePrint)

            ''End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： ALL クリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub optPageRangeAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optPageRangeAll.CheckedChanged

        Try

            cmbPageRangeFrom.Enabled = False : lblFrom.Enabled = False
            cmbPageRangeTo.Enabled = False : lblTo.Enabled = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： PAGES クリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub optPageRangePages_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optPageRangePages.CheckedChanged

        Try

            cmbPageRangeFrom.Enabled = True : lblFrom.Enabled = True
            cmbPageRangeTo.Enabled = True : lblTo.Enabled = True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


    '----------------------------------------------------------------------------
    ' 機能説明  ： 印字ﾌｫｰﾏｯﾄﾁｪｯｸﾎﾞｯｸｽ クリック
    '               Ver1.8.6.1  2015.12.03  追加
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub chkFormatType_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkFormatType.CheckedChanged

        Dim oldType As Byte

        oldType = g_bytFUSet

        ''初期ﾀｲﾌﾟで印字する場合はﾁｪｯｸを入れる
        If chkFormatType.Checked = True Then
            g_bytFUSet = 0
        Else
            g_bytFUSet = 1
        End If

        '' 設定が変わった場合は保存ﾌﾗｸﾞをｾｯﾄ
        If oldType <> g_bytFUSet Then
            '' Verup処理中ならば何もしない
            If gudtFileInfo.blnVersionUP Then
                Debug.Print("Verup")
            Else
                gblnUpdateAll = True
            End If
        End If

    End Sub

#End Region

#Region "内部関数"

    '--------------------------------------------------------------------
    ' 機能      : 設定チェック
    ' 返り値    : True:設定OK、False:設定NG
    ' 引き数    : なし
    ' 機能説明  : 設定チェックを行う
    '--------------------------------------------------------------------
    Private Function mChkInput() As Boolean

        Try

            If optPageRangePages.Checked Then

                If cmbPageRangeFrom.SelectedIndex < 0 Then
                    MsgBox("Please select Pages FROM.", MsgBoxStyle.Exclamation, "Local Unit Print")
                    Return False
                End If

                If cmbPageRangeTo.SelectedIndex < 0 Then
                    MsgBox("Please select Pages TO.", MsgBoxStyle.Exclamation, "Local Unit Print")
                    Return False
                End If

                If CInt(cmbPageRangeFrom.Text) > CInt(cmbPageRangeTo.Text) Then
                    MsgBox("There are injustice data.  [Pages]", MsgBoxStyle.Exclamation, "Local Unit Print")
                    Return False
                End If

            End If

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region


End Class
