Public Class frmPrtGraph

#Region "変数定義"

    ''初期化フラグ
    Private mblnInitFlg As Boolean

#End Region

#Region "画面イベント"

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームロード
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmPrtGraph_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''初期化フラグ
            mblnInitFlg = True

            ''プリンター名称の取得
            Dim pd As New System.Drawing.Printing.PrintDocument
            lblPrinter.Text = pd.PrinterSettings.PrinterName
            pd.Dispose()

            ''PartSelectコンボボックス初期設定
            cmbSelectPart.Items.Add(gCstNamePrintCmbSelectPartMach)
            cmbSelectPart.Items.Add(gCstNamePrintCmbSelectPartCarg)

            ''3種グラフ選択
            optGraphNormal.Checked = True

            ''PartSelectでMachinery選択
            cmbSelectPart.SelectedItem = gCstNamePrintCmbSelectPartMach

            ''初期化フラグ
            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： SelectPart SelectedIndexChangedイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmbSelectPart_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSelectPart.SelectedIndexChanged

        Try

            If cmbSelectPart.SelectedItem = gCstNamePrintCmbSelectPartMach Then     ''Machinery選択

                ''3種グラフ ラジオボタンクリック
                Call cmbGraph_set(gudt.SetOpsGraphM)

            ElseIf cmbSelectPart.SelectedItem = gCstNamePrintCmbSelectPartCarg Then ''Cargo選択

                ''フリーグラフ ラジオボタンクリック
                Call cmbGraph_set(gudt.SetOpsGraphC)

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： [3種グラフ] - ラジオボタン
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub optGraphNormal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optGraphNormal.CheckedChanged

        Try

            ''初期化中は処理しない
            If mblnInitFlg Then Return

            If cmbSelectPart.SelectedItem = gCstNamePrintCmbSelectPartMach Then Call cmbGraph_set(gudt.SetOpsGraphM)
            If cmbSelectPart.SelectedItem = gCstNamePrintCmbSelectPartCarg Then Call cmbGraph_set(gudt.SetOpsGraphC)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： [フリーグラフ] - ラジオボタン
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub optGraphFree_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optGraphFree.CheckedChanged

        Try

            ''初期化中は処理しない
            If mblnInitFlg Then Return

            If cmbSelectPart.SelectedItem = gCstNamePrintCmbSelectPartMach Then Call cmbGraph_set(gudt.SetOpsGraphM)
            If cmbSelectPart.SelectedItem = gCstNamePrintCmbSelectPartCarg Then Call cmbGraph_set(gudt.SetOpsGraphC)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： [フリーグラフ] - OPS番号 プルダウン
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmbOpsNo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbOpsNo.SelectedIndexChanged

        Try

            ''初期化中は処理しない
            If mblnInitFlg Then Return

            If cmbSelectPart.SelectedItem = gCstNamePrintCmbSelectPartMach Then Call mExeOpsNoChange(True, gudt.SetOpsGraphM)
            If cmbSelectPart.SelectedItem = gCstNamePrintCmbSelectPartCarg Then Call mExeOpsNoChange(False, gudt.SetOpsGraphC)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Previewボタン
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPreview.Click

        Try

            If cmbSelectPart.SelectedItem = gCstNamePrintCmbSelectPartMach Then Call mExePreview(True, gudt.SetOpsGraphM)
            If cmbSelectPart.SelectedItem = gCstNamePrintCmbSelectPartCarg Then Call mExePreview(False, gudt.SetOpsGraphC)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Printボタン
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click

        Try

            If cmbSelectPart.SelectedItem = gCstNamePrintCmbSelectPartMach Then Call mExePrint(True, gudt.SetOpsGraphM)
            If cmbSelectPart.SelectedItem = gCstNamePrintCmbSelectPartCarg Then Call mExePrint(False, gudt.SetOpsGraphC)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： All Printボタン
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdAllPrint_Click(sender As System.Object, e As System.EventArgs) Handles cmdAllPrint.Click
        'Ver2.0.0.2
        '全グラフ一括印刷機能
        'つくりとしては、print機能をデータにあるグラフ分ﾙｰﾌﾟするだけのイメージ
        Try

            If cmbSelectPart.SelectedItem = gCstNamePrintCmbSelectPartMach Then Call mExeAllPrint(True, gudt.SetOpsGraphM)
            If cmbSelectPart.SelectedItem = gCstNamePrintCmbSelectPartCarg Then Call mExeAllPrint(False, gudt.SetOpsGraphC)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Exitボタン
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
    Private Sub frmPrtGraph_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

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

#Region "内部関数"

    '----------------------------------------------------------------------------
    ' 機能説明  ： ラジオボタンクリック
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmbGraph_set(ByRef udtSetOpsGraph As gTypSetOpsGraph)

        Try

            Dim strTitle As String
            Dim i As Integer = 0

            If optGraphNormal.Checked = True Then

                cmbGraph.Enabled = True
                cmbGraph.Items.Clear()
                For i = 0 To UBound(udtSetOpsGraph.udtGraphTitleRec)
                    strTitle = gGetString(udtSetOpsGraph.udtGraphTitleRec(i).strName)
                    If strTitle <> "" Then
                        cmbGraph.Items.Add(strTitle)
                    End If
                Next
                cmbOpsNo.Enabled = False
                cmbFree.Enabled = False
                cmbOpsNo.Items.Clear()
                cmbFree.Items.Clear()

            Else
                ' 2013.07.22 グラフとフリーグラフと分離(以下コメント）  K.Fujimoto
                'cmbOpsNo.Enabled = True
                'cmbFree.Enabled = True
                'cmbOpsNo.Items.Clear()
                'cmbFree.Items.Clear()
                'For i = 0 To UBound(udtSetOpsGraph.udtGraphFreeRec)
                '    strTitle = Format(i + 1, "00")
                '    If strTitle <> "" Then
                '        cmbOpsNo.Items.Add(strTitle)
                '    End If
                'Next
                'cmbGraph.Enabled = False
                'cmbGraph.Items.Clear()

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： プルダウン
    ' 引数      ： ARG1 - (IO) OPSグラフ設定構造体
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mExeOpsNoChange(ByVal hblnMachinery As Boolean, _
                                ByRef udtSetOpsGraph As gTypSetOpsGraph)

        Try
            ' 2013.07.22 グラフとフリーグラフと分離(以下コメント）  K.Fujimoto
            'Dim strTitle As String
            'Dim i As Integer = 0

            'cmbFree.Items.Clear()
            'If cmbOpsNo.Text <> "" Then
            '    For i = 0 To UBound(udtSetOpsGraph.udtGraphFreeRec(CInt(cmbOpsNo.Text) - 1).udtFreeGraphTitle)
            '        strTitle = gGetString(udtSetOpsGraph.udtGraphFreeRec(CInt(cmbOpsNo.Text) - 1).udtFreeGraphTitle(i).strGraphTitle)
            '        If strTitle <> "" Then
            '            cmbFree.Items.Add(strTitle)
            '        End If
            '    Next

            'End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： プレビュー
    ' 引数      ： ARG1 - (I ) パート選択情報（True:、False:） 
    '           ： ARG2 - (IO) OPSグラフ設定構造体
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mExePreview(ByVal hblnSelectMachinery As Boolean, _
                            ByRef udtSetOpsGraph As gTypSetOpsGraph)

        Try

            Dim strTitle As String
            Dim i As Integer = 0

            If optGraphNormal.Checked = True Then

                ''-------------------------
                '' 3種グラフ
                ''-------------------------
                If cmbGraph.Text <> "" Then

                    For i = 0 To UBound(udtSetOpsGraph.udtGraphTitleRec)
                        strTitle = gGetString(udtSetOpsGraph.udtGraphTitleRec(i).strName)
                        If strTitle = cmbGraph.Text Then
                            Exit For
                        End If
                    Next

                    If i < UBound(udtSetOpsGraph.udtGraphTitleRec) + 1 Then
                        ' 同じタイトル名称の場合の不具合修正 2013.07.24 K.Fujimoto
                        Call frmPrtGraphPreview.gShow(udtSetOpsGraph, _
                                                      cmbGraph.SelectedIndex, _
                                                      udtSetOpsGraph.udtGraphTitleRec(i).bytType, _
                                                      txtHistoryNo.Text, _
                                                      chkShipNo.Checked, _
                                                      0, _
                                                      gCstCodePrintGraphViewPrintModePreview, _
                                                      hblnSelectMachinery)
                    End If

                End If

            ElseIf optGraphFree.Checked = True Then
                ' 2013.07.22 グラフとフリーグラフと分離(以下コメント）  K.Fujimoto
                ''-------------------------
                '' フリーグラフ
                ''-------------------------
                'If cmbFree.Text <> "" Then
                '    For i = 0 To UBound(udtSetOpsGraph.udtGraphFreeRec(CInt(cmbOpsNo.Text) - 1).udtFreeGraphTitle)
                '        strTitle = gGetString(udtSetOpsGraph.udtGraphFreeRec(CInt(cmbOpsNo.Text) - 1).udtFreeGraphTitle(i).strGraphTitle)
                '        If strTitle = cmbFree.Text Then
                '            Exit For
                '        End If
                '    Next
                '    If i < UBound(udtSetOpsGraph.udtGraphFreeRec(CInt(cmbOpsNo.Text) - 1).udtFreeGraphTitle) + 1 Then
                '        Call frmPrtGraphPreview.gShow(udtSetOpsGraph, _
                '                                      CInt(cmbOpsNo.Text) - 1, _
                '                                      CByte(4), _
                '                                      txtHistoryNo.Text, _
                '                                      chkShipNo.Checked, _
                '                                      i, _
                '                                      gCstCodePrintGraphViewPrintModePreview, _
                '                                      hblnSelectMachinery)
                '    End If
                'End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 印字
    ' 引数      ： ARG1 - (I ) パート選択情報（True:、False:） 
    '           ： ARG2 - (IO) OPSグラフ設定構造体
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mExePrint(ByVal hblnSelectMachinery As Boolean, _
                          ByRef udtSetOpsGraph As gTypSetOpsGraph)

        Dim PrintDialog1 As New PrintDialog()
        Dim strTitle As String = ""
        Dim i As Integer = 0

        ''ファイルへ出力 チェックボックスを無効にする 
        PrintDialog1.AllowPrintToFile = False
        PrintDialog1.PrinterSettings = New System.Drawing.Printing.PrinterSettings()
        PrintDialog1.UseEXDialog = True         '' 64bit版対応 2014.09.18

        If optGraphNormal.Checked = True Then

            ''-------------------------
            '' 3種グラフ
            ''-------------------------
            If cmbGraph.Text <> "" Then
                For i = 0 To UBound(udtSetOpsGraph.udtGraphTitleRec)
                    strTitle = gGetString(udtSetOpsGraph.udtGraphTitleRec(i).strName)
                    If strTitle = cmbGraph.Text Then
                        Exit For
                    End If
                Next

                If strTitle <> "" Then

                    '' Ver1.10.1 2016.02.26 ﾌﾟﾘﾝﾀﾀﾞｲｱﾛｸﾞでｷｬﾝｾﾙできるので、確認ﾒｯｾｰｼﾞを表示しないように変更
                    ''If MsgBox("Do you start printing?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Print") = MsgBoxResult.Yes Then

                    Call frmPrtGraphPreview.gShow(udtSetOpsGraph, _
                                                  i, _
                                                  udtSetOpsGraph.udtGraphTitleRec(i).bytType, _
                                                  txtHistoryNo.Text, _
                                                  chkShipNo.Checked, _
                                                  0, _
                                                  gCstCodePrintGraphViewPrintModePrint, _
                                                  hblnSelectMachinery)
                    ''End If

                End If

            Else
                MsgBox("Please select the Graph.", MsgBoxStyle.Exclamation, "Print")
            End If

        ElseIf optGraphFree.Checked = True Then
            ' 2013.07.22 グラフとフリーグラフと分離(以下コメント）  K.Fujimoto
            ' ''-------------------------
            ' '' フリーグラフ
            ' ''-------------------------
            'If cmbFree.Text <> "" Then
            '    For i = 0 To UBound(udtSetOpsGraph.udtGraphFreeRec(CInt(cmbOpsNo.Text) - 1).udtFreeGraphTitle)
            '        strTitle = gGetString(udtSetOpsGraph.udtGraphFreeRec(CInt(cmbOpsNo.Text) - 1).udtFreeGraphTitle(i).strGraphTitle)
            '        If strTitle = cmbFree.Text Then
            '            Exit For
            '        End If
            '    Next

            '    If strTitle <> "" Then

            '        If MsgBox("Do you start printing?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Print") = MsgBoxResult.Yes Then

            '            Call frmPrtGraphPreview.gShow(udtSetOpsGraph, _
            '                                          CInt(cmbOpsNo.Text) - 1, _
            '                                          CByte(4), _
            '                                          txtHistoryNo.Text, _
            '                                          chkShipNo.Checked, _
            '                                          i, _
            '                                          gCstCodePrintGraphViewPrintModePrint, _
            '                                          hblnSelectMachinery)
            '        End If

            '    End If

            'Else
            '    MsgBox("Please select the Graph.", MsgBoxStyle.Exclamation, "Print")
            'End If

        End If

        PrintDialog1.Dispose()

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 一括    印字
    ' 引数      ： ARG1 - (I ) パート選択情報（True:、False:） 
    '           ： ARG2 - (IO) OPSグラフ設定構造体
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    'Ver2.0.0.2 グラフを一括で印刷する機能
    Private Sub mExeAllPrint(ByVal hblnSelectMachinery As Boolean, _
                          ByRef udtSetOpsGraph As gTypSetOpsGraph)

        Dim PrintDialog1 As New PrintDialog()
        Dim strTitle As String = ""
        Dim i As Integer = 0

        ''ファイルへ出力 チェックボックスを無効にする 
        PrintDialog1.AllowPrintToFile = False
        PrintDialog1.PrinterSettings = New System.Drawing.Printing.PrinterSettings()
        PrintDialog1.UseEXDialog = True         '' 64bit版対応 2014.09.18

        ''-------------------------
        '' データの数分、ﾙｰﾌﾟし、最初のデータを見つけ、印刷処理へ
        ''------------------------- 
        For i = 0 To UBound(udtSetOpsGraph.udtGraphTitleRec)
            strTitle = gGetString(udtSetOpsGraph.udtGraphTitleRec(i).strName)
            If strTitle <> "" Then
                Call frmPrtGraphPreview.gShow(udtSetOpsGraph, _
                                              i, _
                                              udtSetOpsGraph.udtGraphTitleRec(i).bytType, _
                                              txtHistoryNo.Text, _
                                              chkShipNo.Checked, _
                                              0, _
                                              gCstCodePrintGraphViewPrintModeAllPrint, _
                                              hblnSelectMachinery)
                Exit For
            End If
        Next


        PrintDialog1.Dispose()

    End Sub

#End Region

End Class
