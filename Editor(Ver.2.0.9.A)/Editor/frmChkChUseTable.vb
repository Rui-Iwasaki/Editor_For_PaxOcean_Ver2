Public Class frmChkChUseTable

#Region "定数定義"

#End Region

#Region "変数定義"

    Private mintRtn As Integer
    Private mudtFileInfo As gTypFileInfo
    Private mblnCancelFlg As Boolean

    Private mstrPrintingText As String
    Private mstrPrintingPosition As Integer
    Private mstrPrintFont As Font

    Private mblnEnglish As Boolean

    Private mblnCombine As Boolean    ''コンバイン設定

    Dim mstrCsv() As String = Nothing

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 引き数    : ARG1 - (I ) ファイル情報構造体
    ' 返り値    : 0:キャンセル、1:実行、-1:失敗
    ' 機能説明  : 画面表示を行う
    '--------------------------------------------------------------------
    Friend Function gShow(ByVal udtFileInfo As gTypFileInfo) As Integer

        Try

            ''戻り値初期化
            mintRtn = 0

            ''引数保存
            mudtFileInfo = udtFileInfo
            'mudtCompileType = udtCompileType

            ''画面表示
            Call Me.ShowDialog()

            ''戻り値設定
            Return mintRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "画面イベント"

    '--------------------------------------------------------------------
    ' 機能      : フォームロード
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 画面表示初期処理を行う
    '--------------------------------------------------------------------
    Private Sub frmCmpCompier_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''ヘッダーメッセージ出力
            Call mDispHeaderMessage()

            ''文字フラグ
            mblnEnglish = optEnglish.Checked

            ''コンバイン時はファイルを分けないように変更     2014.03.12
            mblnCombine = False
            ''▼▼▼ 20110627 FCU2台設定≠コンバイン 対応 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            ' ''コンバイン設定取得
            'mblnCombine = gChkCombineSetting()
            '---------------------------------------------------------------------------------
            ''Machinery/Cargoの両選択が可能なのは、FCUが２台 or FCUが１台でコンバイン設定が有りの場合
            'mblnCombine = False
            'If gudt.SetSystem.udtSysFcu.shtFcuCnt = 2 Then               ''FCU 2台
            '    mblnCombine = True

            'ElseIf gudt.SetSystem.udtSysFcu.shtFcuCnt = 1 Then           ''FCU 1台
            '    If gudt.SetSystem.udtSysSystem.shtCombineUse = 1 Then    ''コンバイン設定が有り
            '        mblnCombine = True
            '    End If
            'End If
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Englishクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub optEnglish_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optEnglish.CheckedChanged

        Try

            ''文字フラグ
            mblnEnglish = optEnglish.Checked

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Japaneseクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub optJapanese_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optJapanese.CheckedChanged

        Try

            ''文字フラグ
            mblnEnglish = optEnglish.Checked

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： CH No.クリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub optCH_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optCH.CheckedChanged

        Try
            txtChNo.Visible = True
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： All Channelsクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub optAllCH_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optAllCH.CheckedChanged

        Try
            txtChNo.Visible = False
            txtChNo.Text = ""
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： サーチボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click

        Try
            Dim intChNo As Integer
            Dim intUseCnt As Integer = 0

            Dim strWaitMsg1 As String = ""
            Dim strWaitMsg2 As String = ""
            Dim udtMsgResult As DialogResult

            If optCH.Checked Then
                intChNo = CCInt(txtChNo.Text)
                If intChNo = 0 Then Exit Sub
            End If

            If cmdSearch.Text = "Search Cancel" Then
                mblnCancelFlg = True
                Exit Sub
            End If

            If mblnEnglish Then
                udtMsgResult = MessageBox.Show("Do you start channel use table search?", "Search", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                strWaitMsg1 = "Now Searching"
                strWaitMsg2 = "Please wait..."
            Else
                udtMsgResult = MessageBox.Show("チャンネル使用テーブル検索処理を開始します。" & vbCrLf & _
                                               "よろしいですか？", "検索", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                strWaitMsg1 = "サーチ中です。"
                strWaitMsg2 = "しばらくお待ち下さい..."
            End If

            If udtMsgResult = Windows.Forms.DialogResult.Yes Then

                ''画面設定
                Call mSetDisplayEnable(False, strWaitMsg1, strWaitMsg2)

                ''テキストクリア
                txtMsg.Text = ""

                ''ヘッダーメッセージ出力
                Call mDispHeaderMessage()

                Call mAddMsgText("The search begins.", "検索を開始します。")
                Call mAddMsgText("", "")

                ''CSV出力用ヘッダー行作成
                Call mMakeCsvHeader(mstrCsv)

                ''検索開始
                If optCH.Checked Then

                    ''CSV出力用データ行作成
                    ReDim Preserve mstrCsv(1)
                    mstrCsv(1) = intChNo.ToString("0000") & ","

                    ''単独CH
                    ''---------------------------------
                    Call mChSearch(intChNo, intUseCnt, mstrCsv)
                    ''---------------------------------

                    ''CSV出力用データ修正（最後の , を取る）
                    mstrCsv(1) = Mid(mstrCsv(1), 1, mstrCsv(1).Length - 1)

                    Call mAddMsgText("", "")
                    Call mAddMsgText(" -Number of tables used：" & intUseCnt.ToString, " -使用テーブル数：" & intUseCnt.ToString)
                    Call mAddMsgText("", "")

                ElseIf optAllCH.Checked Then

                    ''全てのCH
                    For i As Integer = LBound(gudt.SetChInfo.udtChannel) To UBound(gudt.SetChInfo.udtChannel)

                        With gudt.SetChInfo.udtChannel(i)

                            If .udtChCommon.shtChType <> 0 And .udtChCommon.shtChno <> 0 Then

                                intChNo = .udtChCommon.shtChno
                                intUseCnt = 0

                                ''CSV出力用データ行作成
                                ReDim Preserve mstrCsv(UBound(mstrCsv) + 1)
                                mstrCsv(UBound(mstrCsv)) = intChNo.ToString("0000") & ","

                                ''---------------------------------
                                Call mChSearch(intChNo, intUseCnt, mstrCsv)
                                ''---------------------------------

                                ''CSV出力用データ修正（最後の , を取る）
                                mstrCsv(UBound(mstrCsv)) = Mid(mstrCsv(UBound(mstrCsv)), 1, mstrCsv(UBound(mstrCsv)).Length - 1)

                                If intUseCnt <> 0 Then
                                    'Call mAddMsgText("", "")
                                    'Call mAddMsgText(" -Number of tables used：" & intUseCnt.ToString, " -使用テーブル数：" & intUseCnt.ToString)
                                End If
                                'Call mAddMsgText("", "")

                                If mblnCancelFlg Then Exit For

                            End If

                        End With
                    Next

                End If

                If mblnCancelFlg = False Then
                    Call mAddMsgText("", "")
                    Call mAddMsgText("Finished searching.", "検索が終了しました。")
                    Call mAddMsgText("", "")
                Else
                    Call mAddMsgText("", "")
                    Call mAddMsgText("The search was canceled.", "検索をキャンセルしました。")
                    Call mAddMsgText("", "")
                End If

                ''画面設定
                Call mSetDisplayEnable(True)

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： ファイル出力ボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdOutput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOutput.Click

        Try

            Dim dlgOutput As New SaveFileDialog()
            Dim udtMsgResult As DialogResult

            If mstrCsv Is Nothing Then Return

            With dlgOutput

                .FileName = "CH_USE_" & Format(Now, "yyyyMMddHHmm") & ".csv"
                .InitialDirectory = gGetAppPath()
                .Filter = "CSV File(*.txt)|*.csv"
                .FilterIndex = 1
                '.Title = "保存先のファイルを選択してください"
                .RestoreDirectory = True
                .OverwritePrompt = True
                .CheckPathExists = True

                ''ダイアログを表示する
                If .ShowDialog() = DialogResult.OK Then

                    Try

                        ''ファイル出力
                        Dim intFileNum As Integer = FreeFile()
                        Call FileOpen(intFileNum, .FileName, OpenMode.Append)
                        For i As Integer = 0 To UBound(mstrCsv)
                            Call Print(intFileNum, mstrCsv(i) & vbNewLine)
                        Next
                        Call FileClose(intFileNum)

                        If mblnEnglish Then
                            udtMsgResult = MessageBox.Show("The CSV file is output successfully." & vbNewLine & vbNewLine & .FileName, _
                                                           "CH use table", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            udtMsgResult = MessageBox.Show("検索結果の出力が完了しました。" & vbNewLine & vbNewLine & .FileName, _
                                                           "ＣＨ使用テーブル検索", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If


                    Catch ex As Exception

                    End Try

                End If

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 印刷ボタンクリック(非表示)
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click

        Try

            Dim strMsg As String
            Dim strTitle As String

            If mblnEnglish Then
                strMsg = "May I print the search result?"
                strTitle = "Search result Print"
            Else
                strMsg = "検索結果を印字します。よろしいですか？"
                strTitle = "検索結果の印字"
            End If

            If MessageBox.Show(strMsg, strTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                '印刷する文字列と位置を設定する
                mstrPrintingText = txtMsg.Text
                mstrPrintingPosition = 0

                '印刷に使うフォントを指定する
                mstrPrintFont = New Font("Arial", 10)

                'PrintDocumentオブジェクトの作成
                Dim pd As New System.Drawing.Printing.PrintDocument

                'PrintPageイベントハンドラの追加
                AddHandler pd.PrintPage, AddressOf pd_PrintPage

                '印刷を開始する
                pd.Print()

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 印刷処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub pd_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs)

        Try

            If mstrPrintingPosition = 0 Then
                '改行記号を'\n'に統一する
                mstrPrintingText = mstrPrintingText.Replace(vbCrLf, vbLf)
                mstrPrintingText = mstrPrintingText.Replace(vbCr, vbLf)
            End If

            '印刷する初期位置を決定
            Dim x As Integer = e.MarginBounds.Left
            Dim y As Integer = e.MarginBounds.Top

            '1ページに収まらなくなるか、印刷する文字がなくなるかまでループ
            While e.MarginBounds.Bottom > y + mstrPrintFont.Height AndAlso _
                    mstrPrintingPosition < mstrPrintingText.Length
                Dim line As String = ""

                While True
                    '印刷する文字がなくなるか、
                    '改行の時はループから抜けて印刷する
                    If mstrPrintingPosition >= mstrPrintingText.Length OrElse _
                            mstrPrintingText.Chars(mstrPrintingPosition) = vbLf Then
                        mstrPrintingPosition += 1
                        Exit While
                    End If
                    '一文字追加し、印刷幅を超えるか調べる
                    line += mstrPrintingText.Chars(mstrPrintingPosition)
                    If e.Graphics.MeasureString(line, mstrPrintFont).Width _
                            > e.MarginBounds.Width Then
                        '印刷幅を超えたため、折り返す
                        line = line.Substring(0, line.Length - 1)
                        Exit While
                    End If
                    '印刷文字位置を次へ
                    mstrPrintingPosition += 1
                End While
                '一行書き出す
                e.Graphics.DrawString(line, mstrPrintFont, Brushes.Black, x, y)
                '次の行の印刷位置を計算
                y += mstrPrintFont.GetHeight(e.Graphics)
            End While

            '次のページがあるか調べる
            If mstrPrintingPosition >= mstrPrintingText.Length Then
                e.HasMorePages = False
                '初期化する
                mstrPrintingPosition = 0
            Else
                e.HasMorePages = True
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
    Private Sub frmCmpCompier_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

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

            Me.Dispose()
            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtChNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtChNo.KeyPress

        Try

            e.Handled = gCheckTextInput(5, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： CH No.入力値をフォーマットする
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtChNo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtChNo.Validated

        Try

            If IsNumeric(txtChNo.Text) Then
                txtChNo.Text = Integer.Parse(txtChNo.Text).ToString("0000")
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "内部関数"

    '--------------------------------------------------------------------
    ' 機能      : ☆チャンネルサーチ
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) チャンネルNo
    '           : ARG2 - (IO) 使用テーブル数
    '--------------------------------------------------------------------
    Private Sub mChSearch(ByVal intChNo As Integer, _
                          ByRef intUseCnt As Integer, _
                          ByRef strCsv() As String)

        Try
            Dim blnUse As Boolean

            ''開始メッセージ
            Call mAddMsgText("[CH No：" & intChNo.ToString("0000") & "]", "[CH No：" & intChNo.ToString("0000") & "]")
            'Call mAddMsgText("CH No：" & intChNo.ToString("0000") & "  --- Search beginning ---", "CH No：" & intChNo.ToString("0000") & "  --- 検索を開始します。---")
            'Call mAddMsgText("", "")

            ''出力チャンネル設定
            Call mChkChannelChOutput(intChNo, gudt.SetChOutput, blnUse)
            Call mOutputChannelConvMsg(IIf(mblnEnglish, "Output Set Channel Setting", "出力チャンネル設定"), blnUse, strCsv)
            If blnUse Then intUseCnt += 1
            If mblnCancelFlg Then Return

            ''グループリポーズ設定
            Call mChkChannelChGroupRepose(intChNo, gudt.SetChGroupRepose, blnUse)
            Call mOutputChannelConvMsg(IIf(mblnEnglish, "Group Repose Channel Setting", "グループリポーズ設定"), blnUse, strCsv)
            If blnUse Then intUseCnt += 1
            If mblnCancelFlg Then Return

            ''論理出力設定
            Call mChkChannelChAndOr(intChNo, gudt.SetChAndOr, blnUse)
            Call mOutputChannelConvMsg(IIf(mblnEnglish, "And Or Channel Setting", "論理出力設定"), blnUse, strCsv)
            If blnUse Then intUseCnt += 1
            If mblnCancelFlg Then Return

            ' ''SIO設定
            'Call mChkChannelChSIO(intChNo, gudt.SetChSio, blnUse)
            'Call mOutputChannelConvMsg(IIf(mblnEnglish, "SIO Channel Setting", "SIO設定"), blnUse, strCsv)
            'If blnUse Then intUseCnt += 1
            'If mblnCancelFlg Then Return

            ''SIO通信チャンネル設定
            For i As Integer = 0 To UBound(gudt.SetChSioCh)
                Call mChkChannelChSIOCh(intChNo, gudt.SetChSioCh(i), blnUse)
                Call mOutputChannelConvMsg(IIf(mblnEnglish, "SIO Channel Setting  Port " & i + 1, "SIO通信チャンネル設定 ポート " & i + 1), blnUse, strCsv)
                If blnUse Then intUseCnt += 1
                If mblnCancelFlg Then Return
            Next

            '▼▼▼ 20110705 積算データ設定とかぶるので必要ない？ ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            ''運転積算トリガチャンネル設定
            Call mChkChannelChRevoTrriger(intChNo, gudt.SetChInfo, blnUse)
            Call mOutputChannelConvMsg(IIf(mblnEnglish, "Pulse Revolution Trigger Channnel Setting", "運転積算トリガチャンネル設定"), blnUse, strCsv)
            If blnUse Then intUseCnt += 1
            If mblnCancelFlg Then Return
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            ''積算データ設定
            Call mChkChannelChRunHour(intChNo, gudt.SetChRunHour, blnUse)
            Call mOutputChannelConvMsg(IIf(mblnEnglish, "RUN HOUR Channel Setting", "積算チャンネル設定"), blnUse, strCsv)
            If blnUse Then intUseCnt += 1
            If mblnCancelFlg Then Return

            ''シーケンス設定
            Call mChkChannelSeqSequence(intChNo, gudt.SetSeqSet, blnUse)
            Call mOutputChannelConvMsg(IIf(mblnEnglish, "Sequence Set Channel Setting", "シーケンス設定"), blnUse, strCsv)
            If blnUse Then intUseCnt += 1
            If mblnCancelFlg Then Return

            ''排ガス演算設定
            Call mChkChannelExhGus(intChNo, gudt.SetChExhGus, blnUse)
            Call mOutputChannelConvMsg(IIf(mblnEnglish, "Exh Gas Channel Setting", "排ガス演算設定"), blnUse, strCsv)
            If blnUse Then intUseCnt += 1
            If mblnCancelFlg Then Return

            ''データ保存テーブル設定
            Call mChkChannelDataSaveTable(intChNo, gudt.SetChDataSave, blnUse)
            Call mOutputChannelConvMsg(IIf(mblnEnglish, "Data SaveTable Channel Setting", "データ保存テーブル設定"), blnUse, strCsv)
            If blnUse Then intUseCnt += 1
            If mblnCancelFlg Then Return

            ''コンポジット設定
            Call mChkChannelComposite(intChNo, gudt.SetChComposite, blnUse)
            Call mOutputChannelConvMsg(IIf(mblnEnglish, "Composite Channel Setting", "コンポジット設定"), blnUse, strCsv)
            If blnUse Then intUseCnt += 1
            If mblnCancelFlg Then Return

            ''演算式テーブル
            Call mChkChannelOpeExp(intChNo, gudt.SetSeqOpeExp, blnUse)
            Call mOutputChannelConvMsg(IIf(mblnEnglish, "Operation Expression Setting", "演算式テーブル"), blnUse, strCsv)
            If blnUse Then intUseCnt += 1
            If mblnCancelFlg Then Return

            ''OPSグラフ関連

            ''偏差グラフ（排気ガスグラフ）設定
            If mblnCombine Then
                ''マシナリ
                For i As Integer = 0 To UBound(gudt.SetOpsGraphM.udtGraphExhaustRec)
                    Call mChkChannelGraphExhaustRec(intChNo, gudt.SetOpsGraphM.udtGraphExhaustRec(i), blnUse)
                    Call mOutputChannelConvMsg(IIf(mblnEnglish, "Exhaust Gas Graph Setting(Machinery) " & i + 1, "偏差グラフ設定(マシナリ) " & i + 1), blnUse, strCsv)
                    If blnUse Then intUseCnt += 1
                    If mblnCancelFlg Then Return
                Next

                ''カーゴ
                For i As Integer = 0 To UBound(gudt.SetOpsGraphC.udtGraphExhaustRec)
                    Call mChkChannelGraphExhaustRec(intChNo, gudt.SetOpsGraphC.udtGraphExhaustRec(i), blnUse)
                    Call mOutputChannelConvMsg(IIf(mblnEnglish, "Exhaust Gas Graph Setting(Cargo) " & i + 1, "偏差グラフ設定(カーゴ) " & i + 1), blnUse, strCsv)
                    If blnUse Then intUseCnt += 1
                    If mblnCancelFlg Then Return
                Next
            Else
                For i As Integer = 0 To UBound(gudt.SetOpsGraphM.udtGraphExhaustRec)
                    Call mChkChannelGraphExhaustRec(intChNo, gudt.SetOpsGraphM.udtGraphExhaustRec(i), blnUse)
                    Call mOutputChannelConvMsg(IIf(mblnEnglish, "Exhaust Gas Graph Setting " & i + 1, "偏差グラフ設定 " & i + 1), blnUse, strCsv)
                    If blnUse Then intUseCnt += 1
                    If mblnCancelFlg Then Return
                Next
            End If

            ''バーグラフ設定
            If mblnCombine Then
                ''マシナリ
                For i As Integer = 0 To UBound(gudt.SetOpsGraphM.udtGraphBarRec)
                    Call mChkChannelGraphBarRec(intChNo, gudt.SetOpsGraphM.udtGraphBarRec(i), blnUse)
                    Call mOutputChannelConvMsg(IIf(mblnEnglish, "Bar Graph Setting(Machinery) " & i + 1, "バーグラフ設定(マシナリ) " & i + 1), blnUse, strCsv)
                    If blnUse Then intUseCnt += 1
                    If mblnCancelFlg Then Return
                Next

                ''カーゴ
                For i As Integer = 0 To UBound(gudt.SetOpsGraphC.udtGraphBarRec)
                    Call mChkChannelGraphBarRec(intChNo, gudt.SetOpsGraphC.udtGraphBarRec(i), blnUse)
                    Call mOutputChannelConvMsg(IIf(mblnEnglish, "Bar Graph Setting(Cargo) " & i + 1, "バーグラフ設定(カーゴ) " & i + 1), blnUse, strCsv)
                    If blnUse Then intUseCnt += 1
                    If mblnCancelFlg Then Return
                Next
            Else
                For i As Integer = 0 To UBound(gudt.SetOpsGraphM.udtGraphBarRec)
                    Call mChkChannelGraphBarRec(intChNo, gudt.SetOpsGraphM.udtGraphBarRec(i), blnUse)
                    Call mOutputChannelConvMsg(IIf(mblnEnglish, "Bar Graph Setting " & i + 1, "バーグラフ設定 " & i + 1), blnUse, strCsv)
                    If blnUse Then intUseCnt += 1
                    If mblnCancelFlg Then Return
                Next
            End If

            ''アナログメーター
            If mblnCombine Then
                ''マシナリ
                For i As Integer = 0 To UBound(gudt.SetOpsGraphM.udtGraphAnalogMeterRec)
                    Call mChkChannelAnalogMeterRec(intChNo, gudt.SetOpsGraphM.udtGraphAnalogMeterRec(i), blnUse)
                    Call mOutputChannelConvMsg(IIf(mblnEnglish, "Analog Meter Setting(Machinery) " & i + 1, "アナログメーター設定(マシナリ) " & i + 1), blnUse, strCsv)
                    If blnUse Then intUseCnt += 1
                    If mblnCancelFlg Then Return
                Next

                ''カーゴ
                For i As Integer = 0 To UBound(gudt.SetOpsGraphC.udtGraphAnalogMeterRec)
                    Call mChkChannelAnalogMeterRec(intChNo, gudt.SetOpsGraphC.udtGraphAnalogMeterRec(i), blnUse)
                    Call mOutputChannelConvMsg(IIf(mblnEnglish, "Analog Meter Setting(Cargo) " & i + 1, "アナログメーター設定(カーゴ) " & i + 1), blnUse, strCsv)
                    If blnUse Then intUseCnt += 1
                    If mblnCancelFlg Then Return
                Next
            Else
                For i As Integer = 0 To UBound(gudt.SetOpsGraphM.udtGraphAnalogMeterRec)
                    Call mChkChannelAnalogMeterRec(intChNo, gudt.SetOpsGraphM.udtGraphAnalogMeterRec(i), blnUse)
                    Call mOutputChannelConvMsg(IIf(mblnEnglish, "Analog Meter Setting " & i + 1, "アナログメーター設定 " & i + 1), blnUse, strCsv)
                    If blnUse Then intUseCnt += 1
                    If mblnCancelFlg Then Return
                Next
            End If

            ''フリーグラフ設定      ' 2013.07.22 グラフとフリーグラフと分離  K.Fujimoto
            If mblnCombine Then

                ''マシナリ
                For i As Integer = 0 To UBound(gudt.SetOpsFreeGraphM.udtFreeGraphRec)
                    Call mChkChannelGraphFreeRec(intChNo, gudt.SetOpsFreeGraphM.udtFreeGraphRec(i), blnUse)
                    Call mOutputChannelConvMsg(IIf(mblnEnglish, "Free Graph Setting(Machinery) " & i + 1, "フリーグラフ設定(マシナリ) " & i + 1), blnUse, strCsv)
                    If blnUse Then intUseCnt += 1
                    If mblnCancelFlg Then Return
                Next

                ''カーゴ
                For i As Integer = 0 To UBound(gudt.SetOpsFreeGraphC.udtFreeGraphRec)
                    Call mChkChannelGraphFreeRec(intChNo, gudt.SetOpsFreeGraphC.udtFreeGraphRec(i), blnUse)
                    Call mOutputChannelConvMsg(IIf(mblnEnglish, "Free Graph Setting(Cargo) " & i + 1, "フリーグラフ設定(カーゴ) " & i + 1), blnUse, strCsv)
                    If blnUse Then intUseCnt += 1
                    If mblnCancelFlg Then Return
                Next
            Else
                For i As Integer = 0 To UBound(gudt.SetOpsFreeGraphM.udtFreeGraphRec)
                    Call mChkChannelGraphFreeRec(intChNo, gudt.SetOpsFreeGraphM.udtFreeGraphRec(i), blnUse)
                    Call mOutputChannelConvMsg(IIf(mblnEnglish, "Free Graph Setting " & i + 1, "フリーグラフ設定 " & i + 1), blnUse, strCsv)
                    If blnUse Then intUseCnt += 1
                    If mblnCancelFlg Then Return
                Next
            End If

            ''コントロール使用可/不可
            If mblnCombine Then
                ''マシナリ
                For i As Integer = 0 To UBound(gudt.SetChCtrlUseM.udtCtrlUseNotuseRec)
                    Call mChkChannelChCtrlUse(intChNo, gudt.SetChCtrlUseM.udtCtrlUseNotuseRec(i), blnUse)
                    Call mOutputChannelConvMsg(IIf(mblnEnglish, "Control use/not use Setting(Machinery) " & i + 1, "コントロール使用可/不可(マシナリ) " & i + 1), blnUse, strCsv)
                    If blnUse Then intUseCnt += 1
                    If mblnCancelFlg Then Return
                Next

                ''カーゴ
                For i As Integer = 0 To UBound(gudt.SetChCtrlUseC.udtCtrlUseNotuseRec)
                    Call mChkChannelChCtrlUse(intChNo, gudt.SetChCtrlUseC.udtCtrlUseNotuseRec(i), blnUse)
                    Call mOutputChannelConvMsg(IIf(mblnEnglish, "Control use/not use Setting(Cargo) " & i + 1, "コントロール使用可/不可(カーゴ) " & i + 1), blnUse, strCsv)
                    If blnUse Then intUseCnt += 1
                    If mblnCancelFlg Then Return
                Next
            Else
                For i As Integer = 0 To UBound(gudt.SetChCtrlUseM.udtCtrlUseNotuseRec)
                    Call mChkChannelChCtrlUse(intChNo, gudt.SetChCtrlUseM.udtCtrlUseNotuseRec(i), blnUse)
                    Call mOutputChannelConvMsg(IIf(mblnEnglish, "Control use/not use Setting " & i + 1, "コントロール使用可/不可 " & i + 1), blnUse, strCsv)
                    If blnUse Then intUseCnt += 1
                    If mblnCancelFlg Then Return
                Next
            End If

            ''GWS通信チャンネル設定  2014.02.04
            For i As Integer = 0 To UBound(gudt.SetOpsGwsCh.udtGwsFileRec)
                Call mChkChannelChGwsCh(intChNo, gudt.SetOpsGwsCh.udtGwsFileRec(i), blnUse)
                Call mOutputChannelConvMsg(IIf(mblnEnglish, "GWS Channel Setting  File " & i + 1, "GWS通信チャンネル設定 ファイル " & i + 1), blnUse, strCsv)
                If blnUse Then intUseCnt += 1
                If mblnCancelFlg Then Return
            Next

            '▼▼▼ 20110705 ログフォーマットがないので追加する ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネルサーチ（出力チャンネル設定）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) チャンネルNo
    '           : ARG2 - (I ) 出力チャンネル設定構造体
    '           : ARG3 - (IO) true:CH使用  false:CH不使用
    '--------------------------------------------------------------------
    Private Sub mChkChannelChOutput(ByVal intChNo As Integer, _
                                    ByVal udtSet As gTypSetChOutput, _
                                    ByRef blnUse As Boolean)
        Try

            blnUse = False

            For i As Integer = 0 To UBound(udtSet.udtCHOutPut)

                With udtSet.udtCHOutPut(i)

                    ''TypeがCHデータの時のみ処理を行う
                    ''（CHデータ以外（論理出力or又はand）の時は、CH NOではなく論理出力IDが入っているため）
                    If .bytType = gCstCodeFuOutputChTypeCh Then

                        If intChNo = .shtChid Then

                            blnUse = True
                            Exit For

                        End If

                    End If

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネルサーチ（グループリポーズ設定）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) チャンネルNo
    '           : ARG2 - (I ) 出力チャンネル設定構造体
    '           : ARG3 - (IO) true:CH使用  false:CH不使用
    '--------------------------------------------------------------------
    Private Sub mChkChannelChGroupRepose(ByVal intChNo As Integer, _
                                         ByVal udtSet As gTypSetChGroupRepose, _
                                         ByRef blnUse As Boolean)

        Try

            blnUse = False

            For i As Integer = 0 To UBound(udtSet.udtRepose)

                With udtSet.udtRepose(i)

                    If intChNo = .shtChId Then

                        blnUse = True
                        Exit For

                    Else

                        For j As Integer = 0 To UBound(.udtReposeInf)

                            With .udtReposeInf(j)

                                If intChNo = .shtChId Then

                                    blnUse = True
                                    Exit For

                                End If

                            End With

                        Next

                    End If

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネルサーチ（論理出力設定）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) チャンネルNo
    '           : ARG2 - (I ) 論理出力設定構造体
    '           : ARG3 - (IO) true:CH使用  false:CH不使用
    '--------------------------------------------------------------------
    Private Sub mChkChannelChAndOr(ByVal intChNo As Integer, _
                                   ByVal udtSet As gTypSetChAndOr, _
                                   ByRef blnUse As Boolean)

        Try

            blnUse = False

            For i As Integer = 0 To UBound(udtSet.udtCHOut)

                For j As Integer = 0 To UBound(udtSet.udtCHOut(i).udtCHAndOr)

                    With udtSet.udtCHOut(i).udtCHAndOr(j)

                        If intChNo = .shtChid Then

                            blnUse = True
                            Exit For

                        End If

                    End With

                Next

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    ''--------------------------------------------------------------------
    '' 機能      : チャンネルサーチ（SIO設定）
    '' 返り値    : なし
    '' 引き数    : ARG1 - (I ) チャンネルNo
    ''           : ARG2 - (I ) SIO設定構造体
    ''           : ARG3 - (IO) true:CH使用  false:CH不使用
    ''--------------------------------------------------------------------
    'Private Sub mChkChannelChSIO(ByVal intChNo As Integer, _
    '                               ByVal udtSet As gTypSetChSio, _
    '                               ByRef blnUse As Boolean)

    '    Try
    '        blnUse = False

    '        For i As Integer = 0 To UBound(udtSet.udtVdr)

    '            With udtSet.udtVdr(i)

    '                If intChNo = .shtSendCH Then

    '                    blnUse = True
    '                    Exit For

    '                End If

    '            End With

    '        Next

    '    Catch ex As Exception
    '        Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '    End Try

    'End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネルサーチ（SIO設定CH設定）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) チャンネルNo
    '           : ARG2 - (I ) SIO設定CH設定構造体
    '           : ARG3 - (IO) true:CH使用  false:CH不使用
    '--------------------------------------------------------------------
    Private Sub mChkChannelChSIOCh(ByVal intChNo As Integer, _
                                   ByVal udtSet As gTypSetChSioCh, _
                                   ByRef blnUse As Boolean)
        Try

            blnUse = False

            For i As Integer = 0 To UBound(udtSet.udtSioChRec)

                With udtSet.udtSioChRec(i)

                    If intChNo = .shtChNo Then          '' 2011.12.15 K.Tanigawa .shtSize = 0 を削除

                        blnUse = True
                        Exit For

                    End If

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネルサーチ（運転積算チャンネルトリガ設定）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) チャンネルNo
    '           : ARG2 - (I ) チャンネルデータ設定構造体
    '           : ARG3 - (IO) true:CH使用  false:CH不使用
    '--------------------------------------------------------------------
    Private Sub mChkChannelChRevoTrriger(ByVal intChNo As Integer, _
                                         ByVal udtSet As gTypSetChInfo, _
                                         ByRef blnUse As Boolean)
        Try

            blnUse = False

            For i As Integer = 0 To UBound(udtSet.udtChannel)

                With udtSet.udtChannel(i)

                    ''パルス積算CHの場合
                    If .udtChCommon.shtChType = gCstCodeChTypePulse Then

                        ''データ種別が運転積算の場合
                        '' Ver1.11.8.3 2016.11.08  運転積算 通信CH追加
                        '' Ver1.12.0.1 2017.01.13  関数に変更
                        'If .udtChCommon.shtData = gCstCodeChDataTypePulseRevoTotalHour _
                        'Or .udtChCommon.shtData = gCstCodeChDataTypePulseRevoTotalMin _
                        'Or .udtChCommon.shtData = gCstCodeChDataTypePulseRevoDayHour _
                        'Or .udtChCommon.shtData = gCstCodeChDataTypePulseRevoDayMin _
                        'Or .udtChCommon.shtData = gCstCodeChDataTypePulseRevoLapHour _
                        'Or .udtChCommon.shtData = gCstCodeChDataTypePulseRevoLapMin _
                        'Or .udtChCommon.shtData = gCstCodeChDataTypePulseRevoExtDev Then

                        If gChkRunHourCH(.udtChCommon._shtChno) Then
                            If intChNo = .RevoTrigerChid Then

                                blnUse = True
                                Exit For

                            End If

                        End If

                    End If

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネルサーチ（積算データ設定）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) チャンネルNo
    '           : ARG2 - (I ) 積算データ設定構造体
    '           : ARG3 - (IO) true:CH使用  false:CH不使用
    '--------------------------------------------------------------------
    Private Sub mChkChannelChRunHour(ByVal intChNo As Integer, _
                                     ByVal udtSet As gTypSetChRunHour, _
                                     ByRef blnUse As Boolean)
        Try
            blnUse = False

            For i As Integer = 0 To UBound(udtSet.udtDetail)

                With udtSet.udtDetail(i)

                    If intChNo = .shtChid Then

                        blnUse = True
                        Exit For

                    End If

                    If intChNo = .shtTrgChid Then

                        blnUse = True
                        Exit For

                    End If

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネルサーチ（シーケンス設定）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) チャンネルNo
    '           : ARG2 - (I ) シーケンス設定構造体
    '           : ARG3 - (IO) true:CH使用  false:CH不使用
    '--------------------------------------------------------------------
    Private Sub mChkChannelSeqSequence(ByVal intChNo As Integer, _
                                       ByVal udtSet As gTypSetSeqSet, _
                                       ByRef blnUse As Boolean)
        Try
            Dim udtSequenceLogicSub() As gTypCodeName = Nothing

            blnUse = False

            For i As Integer = 0 To UBound(udtSet.udtDetail)

                With udtSet.udtDetail(i)

                    If .shtLogicType <> 0 Then

                        ''シーケンスロジックサブ設定取得
                        Call gGetComboCodeName(udtSequenceLogicSub, gEnmComboType.ctSeqSetDetailLogic, Format(.shtLogicType, "00"))

                        For j As Integer = 0 To UBound(udtSequenceLogicSub)

                            ''１番目の項目が１の場合（=CH Noの指定）'' 条件追加 Ver.1.4.7 K.Tanigawa
                            If (udtSequenceLogicSub(j).shtCode = gCstCodeSeqLogicSubDataTypeChNo) Or (.shtLogicType = 26 And .shtUseCh(j) = 1) _
                                                                                                  Or (.shtLogicType = 27 And .shtUseCh(j) = 1) Then

                                If intChNo = .shtLogicItem(j) Then

                                    blnUse = True
                                    Exit For

                                End If

                            End If

                        Next

                    End If

                    If blnUse Then Exit For

                    If intChNo = .shtOutChid Then   ''出力チャンネル

                        blnUse = True
                        Exit For

                    End If

                    For j As Integer = 0 To UBound(.udtInput)
                        With .udtInput(j)

                            If .shtChSelect = gCstCodeSeqChSelectCalc Then

                                If intChNo = .shtChid Then  ''入力チャンネル

                                    blnUse = True
                                    Exit For

                                End If

                            End If

                        End With
                    Next

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネルサーチ（排ガス演算）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) チャンネルNo
    '           : ARG2 - (I ) 排ガス演算設定構造体
    '           : ARG3 - (IO) true:CH使用  false:CH不使用
    '--------------------------------------------------------------------
    Private Sub mChkChannelExhGus(ByVal intChNo As Integer, _
                                  ByVal udtSet As gTypSetChExhGus, _
                                  ByRef blnUse As Boolean)
        Try
            blnUse = False

            For i As Integer = 0 To UBound(udtSet.udtExhGusRec)

                With udtSet.udtExhGusRec(i)

                    If intChNo = .shtAveChid Then  ''平均チャンネル

                        blnUse = True
                        Exit For

                    End If

                    If intChNo = .shtRepChid Then  ''リポーズチャンネル

                        blnUse = True
                        Exit For

                    End If

                    For j As Integer = 0 To UBound(.udtExhGusCyl)
                        With .udtExhGusCyl(j)
                            If intChNo = .shtChid Then  ''シリンダチャンネル

                                blnUse = True
                                Exit For

                            End If
                        End With
                    Next

                    If blnUse Then Exit For

                    For j As Integer = 0 To UBound(.udtExhGusDev)
                        With .udtExhGusDev(j)
                            If intChNo = .shtChid Then  ''偏差チャンネル

                                blnUse = True
                                Exit For

                            End If
                        End With
                    Next

                    If blnUse Then Exit For

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネルサーチ（データ保存テーブル設定）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) チャンネルNo
    '           : ARG2 - (I ) データ保存テーブル設定構造体
    '           : ARG3 - (IO) true:CH使用  false:CH不使用
    '--------------------------------------------------------------------
    Private Sub mChkChannelDataSaveTable(ByVal intChNo As Integer, _
                                         ByVal udtSet As gTypSetChDataSave, _
                                         ByRef blnUse As Boolean)
        Try
            blnUse = False

            For i As Integer = 0 To UBound(udtSet.udtDetail)

                With udtSet.udtDetail(i)

                    If intChNo = .shtChid Then

                        blnUse = True
                        Exit For

                    End If

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネルサーチ（コンポジット設定）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) チャンネルNo
    '           : ARG2 - (I ) コンポジット設定構造体
    '           : ARG3 - (IO) true:CH使用  false:CH不使用
    '--------------------------------------------------------------------
    Private Sub mChkChannelComposite(ByVal intChNo As Integer, _
                                     ByVal udtSet As gTypSetChComposite, _
                                     ByRef blnUse As Boolean)
        Try
            blnUse = False

            For i As Integer = 0 To UBound(udtSet.udtComposite)

                With udtSet.udtComposite(i)

                    Try

                        If intChNo = .shtChid Then

                            blnUse = True
                            Exit For

                        End If

                    Catch ex As Exception
                        Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
                    End Try

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネルサーチ（演算式テーブル）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) チャンネルNo
    '           : ARG2 - (I ) 演算式テーブル構造体
    '           : ARG3 - (IO) true:CH使用  false:CH不使用
    '--------------------------------------------------------------------
    Private Sub mChkChannelOpeExp(ByVal intChNo As Integer, _
                                  ByVal udtSet As gTypSetSeqOperationExpression, _
                                  ByRef blnUse As Boolean)
        Try
            Dim shtChid As UShort

            blnUse = False

            For i As Integer = 0 To UBound(udtSet.udtTables)

                With udtSet.udtTables(i)

                    For j As Integer = 0 To UBound(.udtAryInf)

                        With .udtAryInf(j)

                            If .shtType = gCstCodeSeqFixTypeChData _
                            Or .shtType = gCstCodeSeqFixTypeLowSet _
                            Or .shtType = gCstCodeSeqFixTypeHighSet _
                            Or .shtType = gCstCodeSeqFixTypeLLSet _
                            Or .shtType = gCstCodeSeqFixTypeHHSet Then

                                ''バイト配列から設定されているCH NOを取得
                                shtChid = gConnect2Byte(.bytInfo(2), .bytInfo(3))

                                If intChNo = shtChid Then

                                    blnUse = True
                                    Exit For

                                End If

                            End If

                        End With

                    Next

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネルサーチ（偏差グラフテーブル）  
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) チャンネルNo
    '           : ARG2 - (I ) 排気ガスグラフ（偏差グラフ）構造体
    '           : ARG3 - (IO) true:CH使用  false:CH不使用
    '--------------------------------------------------------------------
    Private Sub mChkChannelGraphExhaustRec(ByVal intChNo As Integer, _
                                           ByVal udtSet As gTypSetOpsGraphExhaust, _
                                           ByRef blnUse As Boolean)

        Try

            blnUse = False

            If intChNo = udtSet.shtAveCh Then   ''平均CH

                blnUse = True

            End If
            If blnUse Then Exit Sub

            For i As Integer = 0 To UBound(udtSet.udtCylinder)

                With udtSet.udtCylinder(i)

                    If intChNo = .shtChCylinder Then    ''シリンダのCH

                        blnUse = True
                        Exit For

                    End If

                    If intChNo = .shtChDeviation Then   ''偏差のCH

                        blnUse = True
                        Exit For

                    End If

                End With

            Next
            If blnUse Then Exit Sub

            For i As Integer = 0 To UBound(udtSet.udtTurboCharger)

                With udtSet.udtTurboCharger(i)

                    If intChNo = .shtChTurboCharger Then    ''T/CのCH

                        blnUse = True
                        Exit For

                    End If

                End With

            Next
            If blnUse Then Exit Sub

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネルサーチ（バーグラフテーブル）  
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) チャンネルNo
    '           : ARG2 - (I ) バーグラフ構造体
    '           : ARG3 - (IO) true:CH使用  false:CH不使用
    '--------------------------------------------------------------------
    Private Sub mChkChannelGraphBarRec(ByVal intChNo As Integer, _
                                       ByVal udtSet As gTypSetOpsGraphBar, _
                                       ByRef blnUse As Boolean)

        Try
            blnUse = False

            For i As Integer = 0 To UBound(udtSet.udtCylinder)

                With udtSet.udtCylinder(i)

                    If intChNo = .shtChCylinder Then    ''シリンダのCH

                        blnUse = True
                        Exit For

                    End If

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネルサーチ（アナログメーターテーブル）  
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) チャンネルNo
    '           : ARG2 - (I ) アナログメーター構造体
    '           : ARG3 - (IO) true:CH使用  false:CH不使用
    '--------------------------------------------------------------------
    Private Sub mChkChannelAnalogMeterRec(ByVal intChNo As Integer, _
                                          ByVal udtSet As gTypSetOpsGraphAnalogMeter, _
                                          ByRef blnUse As Boolean)
        Try

            blnUse = False

            For i As Integer = 0 To UBound(udtSet.udtDetail)

                With udtSet.udtDetail(i)

                    If intChNo = .shtChNo Then

                        blnUse = True
                        Exit For

                    End If

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネルサーチ（フリーグラフテーブル）  
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) チャンネルNo
    '           : ARG2 - (I ) フリーグラフ構造体
    '           : ARG3 - (IO) true:CH使用  false:CH不使用
    '             2013.07.22 グラフとフリーグラフと分離  K.Fujimoto
    '--------------------------------------------------------------------
    Private Sub mChkChannelGraphFreeRec(ByVal intChNo As Integer, _
                                        ByVal udtSet As gTypSetOpsFreeGraphRec, _
                                        ByRef blnUse As Boolean)
        Try

            blnUse = False


            For i As Integer = 0 To UBound(udtSet.udtFreeDetail)

                With udtSet.udtFreeDetail(i)

                    If intChNo = .shtChNo Then
                        blnUse = True
                        Exit For
                    End If

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネルサーチ（コントロール使用可/不可テーブル）  
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) チャンネルNo
    '           : ARG2 - (I ) コントロール使用可/不可設定構造体
    '           : ARG3 - (IO) true:CH使用  false:CH不使用
    '--------------------------------------------------------------------
    Private Sub mChkChannelChCtrlUse(ByVal intChNo As Integer, _
                                     ByVal udtSet As gTypSetChCtrlUseRec, _
                                     ByRef blnUse As Boolean)
        Try
            blnUse = False

            For i As Integer = 0 To UBound(udtSet.udtUseNotuseDetails)

                With udtSet.udtUseNotuseDetails(i)

                    If intChNo = .shtChno Then

                        blnUse = True
                        Exit For

                    End If

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネルサーチ（GWS設定CH設定）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) チャンネルNo
    '           : ARG2 - (I ) SIO設定CH設定構造体
    '           : ARG3 - (IO) true:CH使用  false:CH不使用
    '--------------------------------------------------------------------
    Private Sub mChkChannelChGwsCh(ByVal intChNo As Integer, _
                                   ByVal udtSet As gTypSetOpsGwsFileRec, _
                                   ByRef blnUse As Boolean)
        Try

            blnUse = False

            For i As Integer = 0 To UBound(udtSet.udtGwsChRec)

                With udtSet.udtGwsChRec(i)

                    If intChNo = .shtChNo Then

                        blnUse = True
                        Exit For

                    End If

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    '--------------------------------------------------------------------
    ' 機能      : ヘッダーメッセージ出力
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : ヘッダーメッセージを出力する
    '--------------------------------------------------------------------
    Private Sub mDispHeaderMessage()

        Try
            Call mAddMsgText(" --- SEARCH FOR THE TABLE THAT USES THE CHANNEL --- ", " --- チャンネル使用テーブル検索 --- ")
            Call mAddMsgText(" File Name : [ FILE : " & mudtFileInfo.strFileName & " ] ", " ファイル名 : [ FILE : " & mudtFileInfo.strFileName & " ] ")
            Call mAddMsgText("", "")

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネル変換メッセージ出力
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 名称
    ' 　　　    : ARG2 - (I ) true:使用  false:不使用
    ' 機能説明  : チャンネル変換メッセージを出力する
    '--------------------------------------------------------------------
    Private Sub mOutputChannelConvMsg(ByVal strCurName As String, _
                                      ByVal blnUse As Boolean, _
                                      ByRef strCsvStr() As String)

        Try

            ''メッセージ
            If blnUse Then

                ''CH有り
                Call mAddMsgText(" -" & strCurName & " ... Use", " -" & strCurName & " ... 使用")
                strCsvStr(UBound(strCsvStr)) &= IIf(mblnEnglish, "Use,", "使用,")

            Else

                ''CHなし
                'Call mAddMsgText(" -" & strCurName & " ... Nothing", " -" & strCurName & " ... なし")
                strCsvStr(UBound(strCsvStr)) &= ","

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#Region "ログテキスト追加"

    '--------------------------------------------------------------------
    ' 機能      : テキスト表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) メッセージ
    ' 機能説明  : 処理メッセージをテキストに表示する
    '--------------------------------------------------------------------
    Private Sub mAddMsgText(ByVal strMsgEng As String, _
                       ByVal strMsgJpn As String)

        Try

            ''メッセージ追加
            txtMsg.Text &= IIf(mblnEnglish, strMsgEng, strMsgJpn) & vbCrLf '  vbNewLine

            ''表示位置設定
            txtMsg.SelectionStart = Len(txtMsg.Text)
            txtMsg.Focus()
            txtMsg.ScrollToCaret()

            ''テキスト更新
            Call txtMsg.Refresh()
            Call Application.DoEvents()
            'Call System.Threading.Thread.Sleep(1)
            'Call Application.DoEvents()

            ''キャンセル時
            'If mblnCancelFlg Then

            '    txtMsg.Text &= IIf(mblnEnglish, "Search Canceled.", "検索処理をキャンセルしました。") & vbNewLine

            '    txtMsg.SelectionStart = Len(txtMsg.Text)
            '    txtMsg.Focus()
            '    txtMsg.ScrollToCaret()

            '    Call mSetDisplayEnable(True)

            'End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "画面設定"

    Private Sub mSetDisplayEnable(ByVal blnFlg As Boolean, _
                         Optional ByVal strWaitMsg1 As String = "", _
                         Optional ByVal strWaitMsg2 As String = "")

        If blnFlg Then

            ''Waitフレーム
            fraWait.Visible = False
            cmdPrint.Enabled = True
            cmdOutput.Enabled = True
            cmdExit.Enabled = True
            cmdSearch.Text = "Search"

            mblnCancelFlg = True

        Else

            ''Waitフレーム
            lblWait1.Text = strWaitMsg1
            lblWait2.Text = strWaitMsg2
            fraWait.Visible = True
            fraWait.Refresh()

            cmdPrint.Enabled = False
            cmdOutput.Enabled = False
            cmdExit.Enabled = False
            cmdSearch.Text = "Search Cancel"

            mblnCancelFlg = False

        End If

    End Sub

#End Region

#Region "CSVヘッダー行作成"

    Private Sub mMakeCsvHeader(ByRef strCsv() As String)

        ''ヘッダー行配列作成
        ReDim strCsv(0)

        strCsv(0) &= ""

        ''チャンネル番号
        strCsv(0) &= IIf(mblnEnglish, "CH No.", "チャンネル番号") & ","

        ''出力チャンネル設定
        strCsv(0) &= IIf(mblnEnglish, "Output Set", "出力チャンネル") & ","

        ''グループリポーズ設定
        strCsv(0) &= IIf(mblnEnglish, "Group Repose", "グループリポーズ") & ","

        ''論理出力設定
        strCsv(0) &= IIf(mblnEnglish, "And Or", "論理出力") & ","

        ''SIO設定
        strCsv(0) &= IIf(mblnEnglish, "SIO", "SIO") & ","

        ''SIO通信チャンネル設定
        For i As Integer = 0 To UBound(gudt.SetChSioCh)
            strCsv(0) &= IIf(mblnEnglish, "SIO Comm Port " & i + 1, "SIO通信 ポート " & i + 1) & ","
        Next

        ''運転積算トリガチャンネル設定
        strCsv(0) &= IIf(mblnEnglish, "Pulse Revolution Trigger", "運転積算トリガチャンネル") & ","

        ''積算データ設定
        strCsv(0) &= IIf(mblnEnglish, "RUN HOUR", "積算チャンネル") & ","

        ''シーケンス設定
        strCsv(0) &= IIf(mblnEnglish, "Sequence Set", "シーケンス") & ","

        ''排ガス演算設定
        strCsv(0) &= IIf(mblnEnglish, "Exh Gas", "排ガス演算") & ","

        ''データ保存テーブル設定
        strCsv(0) &= IIf(mblnEnglish, "Data SaveTable", "データ保存テーブル") & ","

        ''コンポジット設定
        strCsv(0) &= IIf(mblnEnglish, "Composite Channel", "コンポジット") & ","

        ''演算式テーブル
        strCsv(0) &= IIf(mblnEnglish, "Operation Expression", "演算式テーブル") & ","

        ''OPSグラフ関連
        ''偏差グラフ（排気ガスグラフ）設定
        If mblnCombine Then
            ''マシナリ
            For i As Integer = 0 To UBound(gudt.SetOpsGraphM.udtGraphExhaustRec)
                strCsv(0) &= IIf(mblnEnglish, "Exhaust Gas Graph(Machinery) " & i + 1, "偏差グラフ(マシナリ) " & i + 1) & ","
            Next

            ''カーゴ
            For i As Integer = 0 To UBound(gudt.SetOpsGraphC.udtGraphExhaustRec)
                strCsv(0) &= IIf(mblnEnglish, "Exhaust Gas Graph(Cargo) " & i + 1, "偏差グラフ(カーゴ) " & i + 1) & ","
            Next
        Else
            For i As Integer = 0 To UBound(gudt.SetOpsGraphM.udtGraphExhaustRec)
                strCsv(0) &= IIf(mblnEnglish, "Exhaust Gas Graph " & i + 1, "偏差グラフ " & i + 1) & ","
            Next
        End If

        ''バーグラフ設定
        If mblnCombine Then
            ''マシナリ
            For i As Integer = 0 To UBound(gudt.SetOpsGraphM.udtGraphBarRec)
                strCsv(0) &= IIf(mblnEnglish, "Bar Graph(Machinery) " & i + 1, "バーグラフ(マシナリ) " & i + 1) & ","
            Next

            ''カーゴ
            For i As Integer = 0 To UBound(gudt.SetOpsGraphC.udtGraphBarRec)
                strCsv(0) &= IIf(mblnEnglish, "Bar Graph(Cargo) " & i + 1, "バーグラフ(カーゴ) " & i + 1) & ","
            Next
        Else
            For i As Integer = 0 To UBound(gudt.SetOpsGraphM.udtGraphBarRec)
                strCsv(0) &= IIf(mblnEnglish, "Bar Graph " & i + 1, "バーグラフ" & i + 1) & ","
            Next
        End If

        ''アナログメーター
        If mblnCombine Then
            ''マシナリ
            For i As Integer = 0 To UBound(gudt.SetOpsGraphM.udtGraphAnalogMeterRec)
                strCsv(0) &= IIf(mblnEnglish, "Analog Meter(Machinery) " & i + 1, "アナログメーター(マシナリ) " & i + 1) & ","
            Next

            ''カーゴ
            For i As Integer = 0 To UBound(gudt.SetOpsGraphC.udtGraphAnalogMeterRec)
                strCsv(0) &= IIf(mblnEnglish, "Analog Meter(Cargo) " & i + 1, "アナログメーター(カーゴ) " & i + 1) & ","
            Next
        Else
            For i As Integer = 0 To UBound(gudt.SetOpsGraphM.udtGraphAnalogMeterRec)
                strCsv(0) &= IIf(mblnEnglish, "Analog Meter " & i + 1, "アナログメーター" & i + 1) & ","
            Next
        End If

        ''フリーグラフ設定  ' 2013.07.22 グラフとフリーグラフと分離  K.Fujimoto
        If mblnCombine Then
            ''マシナリ
            For i As Integer = 0 To UBound(gudt.SetOpsFreeGraphM.udtFreeGraphRec)
                strCsv(0) &= IIf(mblnEnglish, "Free Graph(Machinery) " & i + 1, "フリーグラフ(マシナリ) " & i + 1) & ","
            Next

            ''カーゴ
            For i As Integer = 0 To UBound(gudt.SetOpsFreeGraphC.udtFreeGraphRec)
                strCsv(0) &= IIf(mblnEnglish, "Free Graph(Cargo) " & i + 1, "フリーグラフ(カーゴ) " & i + 1) & ","
            Next
        Else
            For i As Integer = 0 To UBound(gudt.SetOpsFreeGraphM.udtFreeGraphRec)
                strCsv(0) &= IIf(mblnEnglish, "Free Graph " & i + 1, "フリーグラフ" & i + 1) & ","
            Next
        End If

        ''コントロール使用可/不可
        If mblnCombine Then
            ''マシナリ
            For i As Integer = 0 To UBound(gudt.SetChCtrlUseM.udtCtrlUseNotuseRec)
                strCsv(0) &= IIf(mblnEnglish, "Control use/not use(Machinery) " & i + 1, "コントロール使用可/不可(マシナリ) " & i + 1) & ","
            Next

            ''カーゴ
            For i As Integer = 0 To UBound(gudt.SetChCtrlUseC.udtCtrlUseNotuseRec)
                strCsv(0) &= IIf(mblnEnglish, "Control use/not use(Cargo) " & i + 1, "コントロール使用可/不可(カーゴ) " & i + 1) & ","
            Next
        Else
            For i As Integer = 0 To UBound(gudt.SetChCtrlUseM.udtCtrlUseNotuseRec)
                strCsv(0) &= IIf(mblnEnglish, "Control use/not use " & i + 1, "コントロール使用可/不可 " & i + 1) & ","
            Next
        End If

        ''CSV出力用データ修正（最後の , を取る）
        strCsv(0) = Mid(strCsv(0), 1, strCsv(0).Length - 1)

    End Sub

#End Region

#End Region

End Class
