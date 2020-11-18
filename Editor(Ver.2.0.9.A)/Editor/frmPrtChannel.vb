Public Class frmPrtChannel

#Region "変数定義"

    Private mudtChannelGroup As gTypChannelGroup

    ''総ページ数
    Private mPageMax As Integer

#End Region

#Region "画面イベント"

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームロード
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmPrtChannel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''プリンター名称SET
            Dim pd As New System.Drawing.Printing.PrintDocument
            lblPrinter.Text = pd.PrinterSettings.PrinterName
            pd.Dispose()


            'Ver2.0.1.3
            'Groupコンボ設定
            Dim dstTbl As New DataSet
            Dim strWk(1) As String

            dstTbl.Tables.Add("Table1")
            dstTbl.Tables(0).Columns.Add("Index")
            dstTbl.Tables(0).Columns.Add("Group")

            For i As Integer = 0 To gCstChannelGroupMax - 1

                strWk(0) = i + 1
                With gudt.SetChGroupSetM.udtGroup.udtGroupInfo(i)
                    Dim strName As String = ""
                    strName = strName & .strName1.Trim & " "
                    strName = strName & .strName2.Trim & " "
                    strName = strName & .strName3.Trim & " "
                    strWk(1) = .shtGroupNo.ToString("00") & ":" & strName
                End With


                Call dstTbl.Tables(0).Rows.Add(strWk)
            Next i

            cmbGroup.DataSource = Nothing
            cmbGroup.Items.Clear()
            cmbGroup.ValueMember = dstTbl.Tables(0).Columns(0).ColumnName
            cmbGroup.DisplayMember = dstTbl.Tables(0).Columns(1).ColumnName
            cmbGroup.DataSource = dstTbl.Tables(0)
            cmbGroup.SelectedIndex = -1



            ''チャンネルグループ情報取得
            Call gMakeChannelData(gudt.SetChInfo, mudtChannelGroup)

            ''ページ設定　関数に変更   2015.11.13 Ver1.7.8
            Call SetPageData()
            '' ''ページ数を取得
            ''Call mGetPageMax()

            ''If mPageMax > 0 Then

            ''    ''コンボボックスにページ番号を設定
            ''    For i As Integer = 1 To mPageMax
            ''        cmbPageRangeFrom.Items.Add(i.ToString)
            ''        cmbPageRangeTo.Items.Add(i.ToString)
            ''    Next

            ''Else
            ''    ''レコードなし
            ''    cmdPrint.Enabled = False
            ''End If

            optPageRangeAll.Checked = True

            '' Ver1.10.5 2016.05.09 ｺﾝﾊﾞｲﾝ設定 CHﾘｽﾄ印刷ﾓｰﾄﾞ
            If g_bytNotCombine = 1 Then
                chkCombine.Checked = True
            End If
            ''//

            'Ver2.0.4.2 計測点リスト印字ｸﾞﾙｰﾌﾟ順
            If g_bytChListOrder = 1 Then
                chkOrder.Checked = True
            Else
                chkOrder.Checked = False
            End If


            'Ver2.0.1.3 Group印刷対応
            If gintChPrintGrNo > -1 Then
                optPageRangeGroup.Checked = True
                cmbGroup.SelectedValue = gintChPrintGrNo
                'Group印刷ﾌﾗｸﾞはここで確実に落とす
                gintChPrintGrNo = -1
            End If

            'Ver2.0.8.N R,W,J,SのINSGを印字するしない
            If g_bytChListINSGprint = 1 Then
                chkINSG.Checked = True
            Else
                chkINSG.Checked = False
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
    Private Sub frmPrtChannel_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

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

    Private Sub GetPrintType()
        If chkCombine.Checked = True Then
            g_bytNotCombine = 1
        Else
            g_bytNotCombine = 0
        End If
        'Ver2.0.4.2 計測点リスト印字ｸﾞﾙｰﾌﾟ順
        If chkOrder.Checked = True Then
            g_bytChListOrder = 1
        Else
            g_bytChListOrder = 0
        End If
        'Ver2.0.8.N R,W,J,SのINSGを印字するしない
        If chkINSG.Checked = True Then
            g_bytChListINSGprint = 1
        Else
            g_bytChListINSGprint = 0
        End If
    End Sub
        

    '----------------------------------------------------------------------------
    ' 機能説明  ： Previewボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPreview.Click

        Try
            'Ver2.0.0.7 印刷前ファイル保存＆再読み込み処理
            If fnPrintBfSave() = False Then
                Exit Sub
            End If

            Dim intPrintAllPage As Integer = 0
            Dim intPageFrom As Integer = 0
            Dim intPageTo As Integer = 0

            If optPageRangeAll.Checked Then

                ''プレビュー表示時は全ページ指定
                ''ALL
                intPrintAllPage = 0

            End If

            frmPrtChannelPreview.mCHNoFg = chkCHNo.Checked      '' Ver1.8.5.2 2015.12.02 ﾀｸﾞ表示時にCHNo.を印字するﾁｪｯｸを追加

            Call GetPrintType()       '' Ver1.10.5 2016.05.09  印刷ﾓｰﾄﾞ追加

            'Ver2.0.1.3 Group印刷対応
            If optPageRangeGroup.Checked = True Then
                'Group選択の場合引数でGroup番号を渡す
                Call frmPrtChannelPreview.gShow(1, intPrintAllPage, intPageFrom, intPageTo, _
                                            chkSecretChannel.Checked, chkDummyData.Checked, "", cmbGroup.SelectedValue)
            Else
                Call frmPrtChannelPreview.gShow(1, intPrintAllPage, intPageFrom, intPageTo, _
                                            chkSecretChannel.Checked, chkDummyData.Checked)
            End If
            

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
            'Ver2.0.0.7 印刷前ファイル保存＆再読み込み処理
            If fnPrintBfSave() = False Then
                Exit Sub
            End If

            Dim intPrintAllPage As Integer = 0
            Dim intPageFrom As Integer = 0
            Dim intPageTo As Integer = 0

            'If MsgBox("Do you start printing?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Print") = MsgBoxResult.No Then Exit Sub

            'Dim PrintDialog1 As New PrintDialog()
            Dim strPrinterName As String

            'PrintDialog1.AllowPrintToFile = False   ''ファイルへ出力 チェックボックスを無効にする 
            'PrintDialog1.PrinterSettings = New System.Drawing.Printing.PrinterSettings()

            ''印刷ダイアログを表示
            'If PrintDialog1.ShowDialog() = DialogResult.OK Then

            ''選択済みプリンター
            'strPrinterName = PrintDialog1.PrinterSettings.PrinterName
            strPrinterName = lblPrinter.Text

            Call GetPrintType()       '' Ver1.10.5 2016.05.09  印刷ﾓｰﾄﾞ追加

            'ALLを選んだ場合
            If optPageRangeAll.Checked Then
                'ALL
                intPrintAllPage = 0
            End If

            'Pageを選んだ場合
            If optPageRangePages.Checked Then
                If mChkInput() = False Then Exit Sub

                ''Pages
                intPrintAllPage = 1
                intPageFrom = CInt(cmbPageRangeFrom.Text)
                intPageTo = CInt(cmbPageRangeTo.Text)
            End If

            'Ver2.0.1.3 Group印刷対応
            'Grupを選んだ場合
            Dim intGrNo As Integer = 0
            If optPageRangeGroup.Checked = True Then
                intGrNo = cmbGroup.SelectedValue
            End If



            frmPrtChannelPreview.mCHNoFg = chkCHNo.Checked      '' Ver1.8.5.2 2015.12.02 ﾀｸﾞ表示時にCHNo.を印字するﾁｪｯｸを追加

            


            'Ver2.0.1.3 GroupNoを引数へ追加
            Call frmPrtChannelPreview.gShow(0, intPrintAllPage, intPageFrom, intPageTo, _
                                            chkSecretChannel.Checked, chkDummyData.Checked, _
                                            strPrinterName, intGrNo)

            'End If

            'PrintDialog1.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定チェック
    ' 返り値    : True:設定OK、False:設定NG
    ' 引き数    : なし
    ' 機能説明  : 設定チェックを行う
    '--------------------------------------------------------------------
    Private Function mChkInput() As Boolean

        Try
            Dim nFromPage As Integer
            Dim nToPage As Integer

            If optPageRangePages.Checked Then

                '' 2015.11.13  Ver1.7.8  入力OKのｺﾝﾎﾞﾎﾞｯｸｽに変更したため、ﾁｪｯｸ処理変更
                If cmbPageRangeFrom.Text = "" Then
                    MsgBox("Please select Pages FROM.", MsgBoxStyle.Exclamation, "Channel List Print")
                    Return False
                Else
                    nFromPage = CInt(cmbPageRangeFrom.Text)

                    If nFromPage < 1 Or nFromPage > mPageMax Then
                        MsgBox("Please select Pages FROM.", MsgBoxStyle.Exclamation, "Channel List Print")
                        Return False
                    End If
                End If

                If cmbPageRangeTo.Text = "" Then
                    MsgBox("Please select Pages TO.", MsgBoxStyle.Exclamation, "Channel List Print")
                    Return False
                Else
                    nToPage = CInt(cmbPageRangeTo.Text)

                    If nToPage < 1 Or nToPage > mPageMax Then
                        MsgBox("Please select Pages TO.", MsgBoxStyle.Exclamation, "Channel List Print")
                        Return False
                    End If
                End If

                If nFromPage > nToPage Then
                    MsgBox("There are injustice data.  [Pages]", MsgBoxStyle.Exclamation, "Channel List Print")
                    Return False
                End If

                ''If cmbPageRangeFrom.SelectedIndex < 0 Then
                ''    MsgBox("Please select Pages FROM.", MsgBoxStyle.Exclamation, "Channel List Print")
                ''    Return False
                ''End If

                ''If cmbPageRangeTo.SelectedIndex < 0 Then
                ''    MsgBox("Please select Pages TO.", MsgBoxStyle.Exclamation, "Channel List Print")
                ''    Return False
                ''End If

                ''If CInt(cmbPageRangeFrom.Text) > CInt(cmbPageRangeTo.Text) Then
                ''    MsgBox("There are injustice data.  [Pages]", MsgBoxStyle.Exclamation, "Channel List Print")
                ''    Return False
                ''End If

            End If

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： ALL クリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub optPageRangeAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optPageRangeAll.CheckedChanged

        Try

            cmbPageRangeFrom.Enabled = False : lblFrom.Enabled = False
            cmbPageRangeTo.Enabled = False : lblTo.Enabled = False

            'Ver2.0.1.3 Group
            cmbGroup.Enabled = False
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

            'Ver2.0.1.3 Group
            cmbGroup.Enabled = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： GROUP クリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub optPageRangeGroup_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles optPageRangeGroup.CheckedChanged
        Try

            cmbPageRangeFrom.Enabled = False : lblFrom.Enabled = False
            cmbPageRangeTo.Enabled = False : lblTo.Enabled = False

            'Ver2.0.1.3 Group
            cmbGroup.Enabled = True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 隠しCH ﾁｪｯｸﾎﾞｯｸｽ クリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub chkSecretChannel_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkSecretChannel.CheckedChanged

        Call SetPageData()      ''ページ設定   2015.11.13 Ver1.7.8

    End Sub

#End Region

#Region "内部関数"

    '----------------------------------------------------------------------------
    ' 機能説明  ： グループ別チャンネル数、ページ数を取得する
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mGetPageMax()

        Try
            Dim i As Integer, j As Integer
            Dim intPageCnt As Integer = 0
            Dim flgSC As Boolean = False '', flgDmy As Boolean = False
            Dim stval As Integer = 0
            Dim endval As Integer = 0
            Dim intType As Integer = 0
            Dim CHflag As Boolean = False

            mPageMax = 0
            intPageCnt = 0

            For i = 0 To UBound(mudtChannelGroup.udtGroup)

                With mudtChannelGroup.udtGroup(i)

                    ''20CH区切りでチェック
                    For j = 0 To 4
                        stval = 0 + (20 * j)
                        endval = 19 + (20 * j)
                        intType = 0
                        CHflag = False

                        ''CH有無、コンポジットCH確認
                        For k = stval To endval
                            If .udtChannelData(k).udtChCommon.shtChno > 0 Then

                                flgSC = IIf(gBitCheck(.udtChannelData(k).udtChCommon.shtFlag1, 1), True, False)     ''隠しCH
                                If chkSecretChannel.Checked = False And flgSC = True Then
                                    ''隠しCH表示無しの設定で、当該CHが隠しCHであった
                                Else
                                    If .udtChannelData(k).udtChCommon.shtChType = gCstCodeChTypeValve And .udtChannelData(k).udtChCommon.shtData = gCstCodeChDataTypeValveDI_DO Or _
                                       .udtChannelData(k).udtChCommon.shtChType = gCstCodeChTypeComposite Then
                                        intType = 1
                                    End If
                                    CHflag = True
                                End If
                            End If
                        Next

                        ''CH有り
                        If CHflag = True Then
                            For k = stval To endval
                                If intType = 1 Then
                                    If k >= 10 + (20 * j) Then
                                        intPageCnt += 1
                                    End If
                                End If
                            Next
                            intPageCnt += 1
                        End If
                    Next

                End With

            Next i

            mPageMax = intPageCnt

            If mPageMax = 0 Then mPageMax = 1

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： ﾍﾟｰｼﾞ数を取得し、ｺﾝﾎﾞﾎﾞｯｸｽに設定する
    '               2015.11.12 Ver1.7.8　追加  隠しCH設定変更時も設定を行うため、関数に変更
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub SetPageData()

        Call mGetPageMax()      ''ページ数を取得

        If mPageMax > 0 Then

            '' ｺﾝﾎﾞﾎﾞｯｸｽのﾃﾞｰﾀが入っていれば削除
            If cmbPageRangeFrom.Items.Count <> 0 Then
                cmbPageRangeFrom.Items.Clear()
            End If
            If cmbPageRangeTo.Items.Count <> 0 Then
                cmbPageRangeTo.Items.Clear()
            End If


            ''コンボボックスにページ番号を設定
            For i As Integer = 1 To mPageMax
                cmbPageRangeFrom.Items.Add(i.ToString)
                cmbPageRangeTo.Items.Add(i.ToString)
            Next
        Else
            ''レコードなし
            cmdPrint.Enabled = False
        End If
    End Sub


    'Ver2.0.0.7 印刷及びプレビュー前に保存して再読込処理
    Private Function fnPrintBfSave() As Boolean
        Dim bRet As Boolean = False

        '保存対象がなければ保存せずに印刷処理続行
        If gudt.SetEditorUpdateInfo.udtSave.bytChannel <> 1 Then
            Return True
        End If

        '保存ダイアログ表示
        If frmFileVersion.gShow(gEnmFileMode.fmEdit, gudtFileInfo, False) <> 0 Then
            '>>>設定値保存
            My.Settings.SelectVersion = gudtFileInfo.strFileVersion
            Call My.Settings.Save()

            'Ver2.0.5.9 全体更新フラグ初期化
            gblnUpdateAll = False

            '>>>保存したファイルを再読込
            frmFileAccess.gShow(gudtFileInfo, gEnmAccessMode.amLoad, False, False, False, gudt, False, False)

            '>>>印刷処理用変数の再読み込み(FormLoadの処理を抜粋)
            ''チャンネルグループ情報取得
            Call gMakeChannelData(gudt.SetChInfo, mudtChannelGroup)
            ''ページ設定
            Call SetPageData()
            optPageRangeAll.Checked = True
            ''ｺﾝﾊﾞｲﾝ設定 CHﾘｽﾄ印刷ﾓｰﾄﾞ
            If g_bytNotCombine = 1 Then
                chkCombine.Checked = True
            End If
            'Ver2.0.4.2 計測点リスト印字ｸﾞﾙｰﾌﾟ順
            If g_bytChListOrder = 1 Then
                chkOrder.Checked = True
            Else
                chkOrder.Checked = False
            End If
            'Ver2.0.8.N R,W,J,SのINSGを印字するしない
            If g_bytChListINSGprint = 1 Then
                chkINSG.Checked = True
            Else
                chkINSG.Checked = False
            End If

            bRet = True
        End If

        Return bRet
    End Function


#End Region


    Private Sub chkCombine_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkCombine.CheckedChanged
        Dim oldType As Byte

        oldType = g_bytNotCombine

        '初期ﾀｲﾌﾟで印字する場合はﾁｪｯｸを入れる
        If chkCombine.Checked = True Then
            g_bytNotCombine = 1
        Else
            g_bytNotCombine = 0
        End If

        '設定が変わった場合は保存ﾌﾗｸﾞをｾｯﾄ
        If oldType <> g_bytNotCombine Then
            '' Verup処理中ならば何もしない
            If gudtFileInfo.blnVersionUP Then
                Debug.Print("Verup")
            Else
                gblnUpdateAll = True
            End If
        End If
    End Sub

    Private Sub chkOrder_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkOrder.CheckedChanged
        Dim oldType As Byte

        oldType = g_bytChListOrder

        '初期ﾀｲﾌﾟで印字する場合はﾁｪｯｸを入れる
        If chkOrder.Checked = True Then
            g_bytChListOrder = 1
        Else
            g_bytChListOrder = 0
        End If

        '設定が変わった場合は保存ﾌﾗｸﾞをｾｯﾄ
        If oldType <> g_bytChListOrder Then
            '' Verup処理中ならば何もしない
            If gudtFileInfo.blnVersionUP Then
                Debug.Print("Verup")
            Else
                gblnUpdateAll = True
            End If
        End If
    End Sub

   
    Private Sub chkINSG_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkINSG.CheckedChanged
        Dim oldType As Byte

        oldType = g_bytChListINSGprint

        '初期ﾀｲﾌﾟで印字する場合はﾁｪｯｸを入れる
        If chkOrder.Checked = True Then
            g_bytChListINSGprint = 1
        Else
            g_bytChListINSGprint = 0
        End If

        '設定が変わった場合は保存ﾌﾗｸﾞをｾｯﾄ
        If oldType <> g_bytChListINSGprint Then
            '' Verup処理中ならば何もしない
            If gudtFileInfo.blnVersionUP Then
                Debug.Print("Verup")
            Else
                gblnUpdateAll = True
            End If
        End If
    End Sub
End Class
