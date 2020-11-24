Public Class frmPrtTerminal

#Region "変数定義"

    ''構造体定義
    Private udtFuInfo() As gTypFuInfo = Nothing                 ''Fu情報構造体
    Private udtPageCnt() As gTypPrintTerminalInfo = Nothing     ''印刷ページカウント用

    Private mintPageCntIndex As Integer     ''印刷ページの枚数確認（最大ページ数Index）
    Private mblnPrtAllFlg As Boolean        ''印刷フラグ
    Private mintSc As Integer               ''隠しCH    設定保存
    Private mintDmy As Integer              ''ダミーCH  設定保存

    ''印刷モード
    Private Const mCstPrtModePrint As Boolean = True
    Private Const mCstPrtModePreview As Boolean = False

    ''[CodeNo] 内容
    Private mstrFuListCodeNo() As String = {"00", "01", "02", "03", "04", "05", "06", "07", "08", _
                                            "09", "0A", "0B", "0C", "0D", "0E", "0F", "10", _
                                            "11", "12", "13", "14"}

#End Region

#Region "画面イベント"

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームロード
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmPrtTerminal_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''プリンター名称の取得
            Dim pd As New System.Drawing.Printing.PrintDocument
            lblPrinter.Text = pd.PrinterSettings.PrinterName
            pd.Dispose()

            ''PartSelect削除　ver.1.4.0 2011.08.17

            ''端子台情報の取得
            Call gMakeFuInfoStructure(udtFuInfo)

            ''ページ数の取得
            Call mCntPages(udtFuInfo, udtPageCnt, mintPageCntIndex)

            Dim intData() As Integer = Nothing
            Call subGetDrawNo(udtFuInfo, intData)


            ''印刷するものがある場合は From-To にページ番号の設定を行う
            'Ver2.0.2.8 DRAW NOのFrom-Toも設定を行う
            If mintPageCntIndex >= 0 Then
                mintPageCntIndex = mintPageCntIndex + 1 + fnLUgetPageMax()
                For i As Integer = 1 To mintPageCntIndex
                    cmbPageRangeFrom.Items.Add(i.ToString)
                    cmbPageRangeTo.Items.Add(i.ToString)
                Next

                cmbDrawFrom.Items.Add("1")
                cmbDrawTo.Items.Add("1")
                For i As Integer = LBound(intData) To UBound(intData) Step 1
                    cmbDrawFrom.Items.Add(intData(i).ToString)
                    cmbDrawTo.Items.Add(intData(i).ToString)
                Next i
            Else
                ''レコードなし
                cmdPreview.Enabled = False
                cmdPrint.Enabled = False
            End If

            optPageRangeAll.Checked = True

            '' Ver1.8.3  2015.11.26  初期ﾀｲﾌﾟで印字する場合はﾁｪｯｸを入れる
            If g_bytFUSet = 0 Then
                chkFormatType.Checked = True
            Else
                chkFormatType.Checked = False
            End If

            'Ver2.0.1.3
            'FUnoとPortNoコンボ設定
            'Ver2.0.2.9 FUとPortで統一
            Dim strData() As String = Nothing
            Call subGetFuAdr(udtFuInfo, strData)
            With cmbFU
                .Items.Add("")
                For i As Integer = LBound(strData) To UBound(strData) Step 1
                    Dim strSplit() As String = strData(i).Split(",")
                    If strSplit(0) = "00" Then
                        strSplit(0) = "FCU"
                    Else
                        strSplit(0) = "FU" & CInt(strSplit(0))
                    End If
                    strSplit(1) = CInt(strSplit(1))
                    .Items.Add(strSplit(0) & "-" & strSplit(1))
                Next i
            End With
            With cmbFuTo
                .Items.Add("")
                For i As Integer = LBound(strData) To UBound(strData) Step 1
                    Dim strSplit() As String = strData(i).Split(",")
                    If strSplit(0) = "00" Then
                        strSplit(0) = "FCU"
                    Else
                        strSplit(0) = "FU" & CInt(strSplit(0))
                    End If
                    strSplit(1) = CInt(strSplit(1))
                    .Items.Add(strSplit(0) & "-" & strSplit(1))
                Next i
            End With

            ''>>>FUコンボ
            'Dim dstTbl As New DataSet
            'Dim strWk(1) As String
            'dstTbl.Tables.Add("Table1")
            'dstTbl.Tables(0).Columns.Add("Index")
            'dstTbl.Tables(0).Columns.Add("Group")
            'For i As Integer = 0 To gCstCountFuNo - 1
            '    strWk(0) = i

            '    Dim strData As String = ""
            '    If i = 0 Then
            '        strData = "FCU"
            '    Else
            '        strData = "FU" & i.ToString
            '    End If
            '    strWk(1) = strData

            '    Call dstTbl.Tables(0).Rows.Add(strWk)
            'Next i
            'cmbFU.DataSource = Nothing
            'cmbFU.Items.Clear()
            'cmbFU.ValueMember = dstTbl.Tables(0).Columns(0).ColumnName
            'cmbFU.DisplayMember = dstTbl.Tables(0).Columns(1).ColumnName
            'cmbFU.DataSource = dstTbl.Tables(0)
            'cmbFU.SelectedIndex = -1
            ''>>>Portコンボ
            'Dim dstTblPort As New DataSet
            'Dim strWkPort(1) As String
            'dstTblPort.Tables.Add("Table1")
            'dstTblPort.Tables(0).Columns.Add("Index")
            'dstTblPort.Tables(0).Columns.Add("Group")
            'For i As Integer = 0 To gCstCountFuPort - 1
            '    strWkPort(0) = i
            '    strWkPort(1) = "SLOT" & (i + 1).ToString

            '    Call dstTblPort.Tables(0).Rows.Add(strWkPort)
            'Next i
            'cmbPort.DataSource = Nothing
            'cmbPort.Items.Clear()
            'cmbPort.ValueMember = dstTblPort.Tables(0).Columns(0).ColumnName
            'cmbPort.DisplayMember = dstTblPort.Tables(0).Columns(1).ColumnName
            'cmbPort.DataSource = dstTblPort.Tables(0)
            'cmbPort.SelectedIndex = -1


            'Ver2.0.1.3 FuAdr印刷対応
            If gintTermFuNo > -1 Then
                optPageRangeFuAdr.Checked = True
                'Ver2.0.2.9 FU変更に伴い、修正
                Dim strCboFU As String = ""
                If gintTermFuNo = 0 Then
                    strCboFU = "FCU-" & (gintTermSlotNo + 1).ToString
                Else
                    strCboFU = "FU" & gintTermFuNo.ToString & "-" & (gintTermSlotNo + 1).ToString
                End If
                cmbFU.Text = strCboFU
                cmbFuTo.Text = strCboFU
                'cmbFU.SelectedValue = gintTermFuNo
                'cmbPort.SelectedValue = gintTermSlotNo
                'FuAdr印刷ﾌﾗｸﾞはここで確実に落とす
                gintTermFuNo = -1
                gintTermSlotNo = -1
            End If

            'Ver2.0.4.4 端子表印字レンジ印刷するしない
            If g_bytTermRange = 1 Then
                chkRangePrint.Checked = True
            Else
                chkRangePrint.Checked = False
            End If

            'Ver2.0.7.4 基板ﾊﾞｰｼﾞｮﾝ印刷対応
            If g_bytTerVer = 1 Then
                chkTerVer.Checked = True
            Else
                chkTerVer.Checked = False
            End If

            'Ver2.0.8.7 DI端子表に共通コモンのメッセージ
            If g_bytTerDIMsg = 1 Then
                chkTerDIMsg.Checked = True
            Else
                chkTerDIMsg.Checked = False
            End If

            'Ver2.0.8.7 DI端子表に共通コモンのメッセージ
            If g_bytExoTxtEtoJ = 1 Then
                chkExpTxtEtoJ.Checked = True
            Else
                chkExpTxtEtoJ.Checked = False
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
    Private Sub frmPrtTerminal_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

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
            'Ver2.0.0.7 印刷前ファイル保存＆再読み込み処理
            If fnPrintBfSave() = False Then
                Exit Sub
            End If


            Dim intPageFromIndex As Integer = 0
            Dim intPageToIndex As Integer = 0
            Dim intScFlg As Integer = 0
            Dim intDmyFlg As Integer = 0
            Dim intPagePrint As Integer = 0 ''ページ印刷 2013.10.18
            Dim intFuNameType As Integer = 0 '' FIELD UNIT名称表記  2013.11.15

            '▼ 2011.05.30 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            'プレビュー画面を表示する場合、印刷範囲の設定は踏襲しない
            mblnPrtAllFlg = True ''ALL
            '----------------------------------------------------------------
            'If optPageRangeAll.Checked Then

            '    ''ALL
            '    mblnPrtAllFlg = True

            'Else

            '    ''Pagesの入力チェック
            '    If mChkInput() = False Then Exit Sub

            '    mblnPrtAllFlg = False
            '    intPageFromIndex = CInt(cmbPageRangeFrom.Text) - 1
            '    intPageToIndex = CInt(cmbPageRangeTo.Text) - 1

            'End If
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            ''OPTION項目の設定確認
            intScFlg = IIf(chkSecretChannel.Checked, 1, 0)
            intDmyFlg = IIf(chkDummyData.Checked, 1, 0)
            intPagePrint = IIf(chkPagePrint.Checked, 1, 0)  ''ページ印刷 2013.10.18
            intFuNameType = IIf(chkFuName.Checked, 1, 0)    '' FIELD UNIT名称表記  2013.11.15

            g_bytTermRange = IIf(chkRangePrint.Checked, 1, 0)   'Ver2.0.4.4 レンジ印刷するしない

            g_bytTerVer = IIf(chkTerVer.Checked, 1, 0)   'Ver2.0.7.4 基板ﾊﾞｰｼﾞｮﾝ印刷するしない

            g_bytTerDIMsg = IIf(chkTerDIMsg.Checked, 1, 0)   'Ver2.0.8.7 DI端子表に共通コモンのメッセージ

            ''PartSelect削除　ver.1.4.0 2011.08.17

            'Ver2.0.1.3 FUadr印刷対応
            If optPageRangeFuAdr.Checked = True Then
                Dim intFu As Integer = -1
                Dim intPort As Integer = -1
                If cmbFU.Text.Trim <> "" Then
                    Dim strSplit() As String = cmbFU.Text.Split("-")
                    'FU
                    If strSplit(0) = "FCU" Then
                        intFu = 0
                    Else
                        strSplit(0) = strSplit(0).Replace("FU", "")
                        intFu = CInt(strSplit(0))
                    End If
                    'PORT
                    intPort = CInt(strSplit(1))
                    intPort = intPort - 1
                End If
                ''詳細画面の表示処理
                Call frmPrtTerminalPreview.gShow(udtFuInfo, _
                                                 mCstPrtModePreview, _
                                                 mblnPrtAllFlg, _
                                                 intPageFromIndex, _
                                                 intPageToIndex, _
                                                 intScFlg, _
                                                 intDmyFlg, _
                                                 intPagePrint, _
                                                 intFuNameType, _
                                                 intFu, intPort)
            Else
                ''詳細画面の表示処理
                Call frmPrtTerminalPreview.gShow(udtFuInfo, _
                                                 mCstPrtModePreview, _
                                                 mblnPrtAllFlg, _
                                                 intPageFromIndex, _
                                                 intPageToIndex, _
                                                 intScFlg, _
                                                 intDmyFlg, _
                                                 intPagePrint, _
                                                 intFuNameType)
            End If
            

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： [PRINT] ボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click

        Try
            'Ver2.0.0.7 印刷前ファイル保存＆再読み込み処理
            If fnPrintBfSave() = False Then
                Exit Sub
            End If

            Dim intPageFromIndex As Integer = 0
            Dim intPageToIndex As Integer = 0
            Dim intScFlg As Integer = 0
            Dim intDmyFlg As Integer = 0
            Dim intPagePrint As Integer = 0
            Dim intFuNameType As Integer = 0 '' FIELD UNIT名称表記  2013.11.15

            'ALL選択
            If optPageRangeAll.Checked Then
                ''ALL
                mblnPrtAllFlg = True
            End If

            'Page選択
            If optPageRangePages.Checked Then
                ''Pagesの入力チェック
                If mChkInput() = False Then Exit Sub

                mblnPrtAllFlg = False
                intPageFromIndex = CInt(cmbPageRangeFrom.Text) - 1
                intPageToIndex = CInt(cmbPageRangeTo.Text) - 1
            End If

            'Ver2.0.2.8
            'DrawNo 選択
            If optDrawNo.Checked Then
                ''Pagesの入力チェック
                If mChkInput_DrawNo() = False Then Exit Sub

                mblnPrtAllFlg = False
                intPageFromIndex = fnGetDrawNoToTuBan(CInt(cmbDrawFrom.Text)) + 1 + fnLUgetPageMax()
                intPageToIndex = fnGetDrawNoToTuBan_MAX(CInt(cmbDrawTo.Text)) + 1 + fnLUgetPageMax()
            End If


            'Ver2.0.1.3
            'FUadr選択
            Dim intFU As Integer = -1
            Dim intPort As Integer = -1
            If optPageRangeFuAdr.Checked Then
                If cmbFU.Text.Trim <> "" Then
                    Dim strSplit() As String = cmbFU.Text.Split("-")
                    'FU
                    If strSplit(0) = "FCU" Then
                        intFU = 0
                    Else
                        strSplit(0) = strSplit(0).Replace("FU", "")
                        intFU = CInt(strSplit(0))
                    End If
                    'PORT
                    intPort = CInt(strSplit(1))
                    intPort = intPort - 1
                    mblnPrtAllFlg = False
                End If
            End If
            'Ver2.0.2.9
            'FUadrTo選択
            Dim intFUto As Integer = -1
            Dim intPortTo As Integer = -1
            If optPageRangeFuAdr.Checked Then
                If cmbFuTo.Text.Trim <> "" Then
                    Dim strSplit() As String = cmbFuTo.Text.Split("-")
                    'FU
                    If strSplit(0) = "FCU" Then
                        intFUto = 0
                    Else
                        strSplit(0) = strSplit(0).Replace("FU", "")
                        intFUto = CInt(strSplit(0))
                    End If
                    'PORT
                    intPortTo = CInt(strSplit(1))
                    intPortTo = intPortTo - 1
                    mblnPrtAllFlg = False
                End If
            End If



            ''OPTION項目の設定確認
            intScFlg = IIf(chkSecretChannel.Checked, 1, 0)
            intDmyFlg = IIf(chkDummyData.Checked, 1, 0)
            intPagePrint = IIf(chkPagePrint.Checked, 1, 0)  ''ページ印刷 2013.10.18
            intFuNameType = IIf(chkFuName.Checked, 1, 0)    '' FIELD UNIT名称表記  2013.11.15

            g_bytTermRange = IIf(chkRangePrint.Checked, 1, 0)   'Ver2.0.4.4 レンジ印刷するしない

            g_bytTerVer = IIf(chkTerVer.Checked, 1, 0)   'Ver2.0.7.4 基板ﾊﾞｰｼﾞｮﾝ印刷するしない

            g_bytTerDIMsg = IIf(chkTerDIMsg.Checked, 1, 0)   'Ver2.0.8.7 DI端子表に共通コモンのメッセージ


            ''PartSelect削除　ver.1.4.0 2011.08.17

            ''詳細画面の表示処理
            Call frmPrtTerminalPreview.gShow(udtFuInfo, _
                                             mCstPrtModePrint, _
                                             mblnPrtAllFlg, _
                                             intPageFromIndex, _
                                             intPageToIndex, _
                                             intScFlg, _
                                             intDmyFlg, _
                                             intPagePrint, _
                                             intFuNameType, _
                                             intFU, intPort, intFUto, intPortTo)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#Region "入力イベント"

    '----------------------------------------------------------------------------
    ' 機能説明  ： ALL クリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub optPageRangeAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optPageRangeAll.CheckedChanged

        Try

            cmbPageRangeFrom.Enabled = False : lblFrom.Enabled = False
            cmbPageRangeTo.Enabled = False : lblTo.Enabled = False

            'Ver2.0.2.8 DrawNo
            cmbDrawFrom.Enabled = False : lblDrawFrom.Enabled = False
            cmbDrawTo.Enabled = False : lblDrawTo.Enabled = False

            'Ver2.0.1.3
            'FU,Port指定
            cmbFU.Enabled = False : cmbPort.Enabled = False
            cmbFuTo.Enabled = False : cmbPortTo.Enabled = False

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

            'Ver2.0.2.8 DrawNo
            cmbDrawFrom.Enabled = False : lblDrawFrom.Enabled = False
            cmbDrawTo.Enabled = False : lblDrawTo.Enabled = False

            'Ver2.0.1.3
            'FU,Port指定
            cmbFU.Enabled = False : cmbPort.Enabled = False
            cmbFuTo.Enabled = False : cmbPortTo.Enabled = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    'Ver2.0.2.8 DrawNo
    '----------------------------------------------------------------------------
    ' 機能説明  ： DRAW NO クリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub optDrawNo_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles optDrawNo.CheckedChanged
        Try

            cmbPageRangeFrom.Enabled = False : lblFrom.Enabled = False
            cmbPageRangeTo.Enabled = False : lblTo.Enabled = False

            cmbDrawFrom.Enabled = True : lblDrawFrom.Enabled = True
            cmbDrawTo.Enabled = True : lblDrawTo.Enabled = True

            'FU,Port指定
            cmbFU.Enabled = False : cmbPort.Enabled = False
            cmbFuTo.Enabled = False : cmbPortTo.Enabled = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： FUadr クリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub optPageRangeFuAdr_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles optPageRangeFuAdr.CheckedChanged
        Try

            cmbPageRangeFrom.Enabled = False : lblFrom.Enabled = False
            cmbPageRangeTo.Enabled = False : lblTo.Enabled = False

            'Ver2.0.2.8 DrawNo
            cmbDrawFrom.Enabled = False : lblDrawFrom.Enabled = False
            cmbDrawTo.Enabled = False : lblDrawTo.Enabled = False

            'Ver2.0.1.3
            'FU,Port指定
            cmbFU.Enabled = True : cmbPort.Enabled = True
            cmbFuTo.Enabled = True : cmbPortTo.Enabled = True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
#End Region

#End Region

#Region "内部関数"

    '----------------------------------------------------------------------------
    ' 機能説明  ： 印刷ページの枚数チェック
    ' 引数      ： ARG1 - (I ) FU情報構造体
    '           ： ARG2 - (IO) 印刷用構造体   
    '           ： ARG3 - (IO) ページ数の確認
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mCntPages(ByVal udtFuInfo() As gTypFuInfo, _
                          ByRef udtTerminalPageInfo() As gTypPrintTerminalInfo, _
                          ByRef intPageCnt As Integer)

        Try

            Dim i As Integer, j As Integer
            Dim intPageIndex As Integer = 0

            ReDim udtTerminalPageInfo(400)

            Dim intData As Integer

            ''FU機器
            For i = 0 To UBound(udtFuInfo)

                ''スロット
                For j = 0 To UBound(udtFuInfo(i).udtFuPort)

                    ''スロット割付けがない場合は次へ
                    If udtFuInfo(i).udtFuPort(j).intPortType <> 0 Then

                        udtTerminalPageInfo(intPageIndex).udtPageType = gConvPortType(udtFuInfo(i).udtFuPort(j).intPortType)

                        'Ver2.0.2.8 DRAW NO SET
                        If i = 0 Then
                            intData = 2 + j
                        Else
                            intData = (7 + (8 * (i - 1)) + j)
                        End If
                        udtTerminalPageInfo(intPageIndex).strDrawinfNoInfo = intData

                        intPageIndex += 1



                        ''デジタルだった場合は２枚連続出力（１枚目：No.01～32、２枚目：No.33～64）
                        If udtTerminalPageInfo(intPageIndex - 1).udtPageType = gEnmPrintTerminalPageType.tptDigital Then

                            'Ver2.0.0.2 デジタル2枚目が空白の場合カウントしない
                            If ChkTerminalCount(udtFuInfo(i), 2, i, j) = False Then
                            Else
                                'Ver2.0.2.8 DRAW NO SET
                                If i = 0 Then
                                    intData = 2 + j
                                Else
                                    intData = (7 + (8 * (i - 1)) + j)
                                End If
                                udtTerminalPageInfo(intPageIndex).strDrawinfNoInfo = intData

                                intPageIndex += 1
                            End If
                        End If

                    End If

                Next j

            Next i

            ''ページ数のカウント
            intPageCnt = intPageIndex

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： D/I、D/Oの点数確認
    ' 引数      ： ARG1 - (I ) 基板種類
    '           ： ARG2 - (I ) 1 or 2
    '           ： ARG3 - (I ) FU番号
    '           ： ARG4 - (I ) SLOT番号
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Function ChkTerminalCount(ByVal hudtFuInfo As gTypFuInfo, ByVal page As Integer, ByVal funo As Integer, ByVal Slotno As Integer) As Boolean

        Dim i As Integer
        Dim exist_flg As Boolean = False
        Dim mtype As Integer = 0

        mtype = hudtFuInfo.udtFuPort(Slotno).intPortType

        Select Case mtype
            Case 1 ''DO
                If page = 2 Then
                    For i = 32 To 63
                        '' 参照変数変更   2013.11.02
                        'If gudt.SetChDisp.udtChDisp(funo).udtSlotInfo(Slotno).udtPinInfo(i).shtChid <> 0 Then

                        ' 2015.09.16 M.Kaihara
                        ' 端子リスト印刷において2枚目のページが印刷されない不具合を修正。
                        ' 2枚目のページリストを数える際、strChNoだけでなくstrChNo2, strChNo3も見るように修正。
                        ' If hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo <> "" Then
                        If hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo <> "" Or hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo2 <> "" Or hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo3 <> "" Then
                            exist_flg = True
                            Exit For
                        End If
                    Next
                End If

            Case 2 ''DI
                For i = 32 To 63
                    '' 参照変数変更   2013.11.02
                    'If gudt.SetChDisp.udtChDisp(funo).udtSlotInfo(Slotno).udtPinInfo(i).shtChid <> 0 Then

                    ' 2015.09.16 M.Kaihara
                    ' 端子リスト印刷において2枚目のページが印刷されない不具合を修正。
                    ' 2枚目のページリストを数える際、strChNoだけでなくstrChNo2, strChNo3も見るように修正。
                    ' If hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo <> "" Then
                    If hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo <> "" Or hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo2 <> "" Or hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo3 <> "" Then
                        exist_flg = True
                        Exit For
                    End If
                Next
        End Select

        Return exist_flg

    End Function

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

                '' 2015.11.13 Ver1.7.8  ﾍﾟｰｼﾞ番号を入力可能ｺﾝﾎﾞﾎﾞｯｸｽに変更したのでﾁｪｯｸ処理を変更
                If cmbPageRangeFrom.Text = "" Then
                    MsgBox("Please select Pages FROM.", MsgBoxStyle.Exclamation, "Channel List Print")
                    Return False
                Else
                    nFromPage = CInt(cmbPageRangeFrom.Text)

                    If nFromPage < 1 Or mintPageCntIndex < nFromPage Then
                        MsgBox("Please select Pages FROM.", MsgBoxStyle.Exclamation, "Channel List Print")
                        Return False
                    End If
                End If

                If cmbPageRangeTo.Text = "" Then
                    MsgBox("Please select Pages TO.", MsgBoxStyle.Exclamation, "Channel List Print")
                    Return False
                Else
                    nToPage = CInt(cmbPageRangeTo.Text)

                    If nToPage < 1 Or mintPageCntIndex < nToPage Then
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
    'Ver2.0.2.8
    Private Function mChkInput_DrawNo() As Boolean

        Try
            Dim nFromPage As Integer
            Dim nToPage As Integer

            If optDrawNo.Checked Then
                If cmbDrawFrom.Text = "" Then
                    MsgBox("Please select Pages DrawNo FROM.", MsgBoxStyle.Exclamation, "Terminal Print")
                    Return False
                Else
                    nFromPage = CInt(cmbDrawFrom.Text)
                End If

                If cmbDrawTo.Text = "" Then
                    MsgBox("Please select Pages DrawNo TO.", MsgBoxStyle.Exclamation, "Terminal Print")
                    Return False
                Else
                    nToPage = CInt(cmbDrawTo.Text)
                End If

                If nFromPage > nToPage Then
                    MsgBox("There are injustice data.  [DrawNo]", MsgBoxStyle.Exclamation, "Terminal Print")
                    Return False
                End If
            End If

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function



    'Ver2.0.0.7 印刷及びプレビュー前に保存して再読込処理
    Private Function fnPrintBfSave() As Boolean
        Dim bRet As Boolean = False

        '保存対象がなければ保存せずに印刷処理続行
        If gudt.SetEditorUpdateInfo.udtSave.bytChDisp <> 1 Then
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
            frmFileAccess.gShow(gudtFileInfo, gEnmAccessMode.amLoad, False, False, False, gudt, gudt2, False, False)

            '>>>印刷処理用変数の再読み込み(FormLoadの処理を抜粋)
            ''端子台情報の取得
            Call gMakeFuInfoStructure(udtFuInfo)
            ''ページ数の取得
            Call mCntPages(udtFuInfo, udtPageCnt, mintPageCntIndex)

            Dim intData() As Integer = Nothing
            Call subGetDrawNo(udtFuInfo, intData)

            ''印刷するものがある場合は From-To にページ番号の設定を行う
            'Ver2.0.2.8 DRAW NOのFrom-Toも設定を行う
            If mintPageCntIndex >= 0 Then
                mintPageCntIndex = mintPageCntIndex + 1 + fnLUgetPageMax()
                For i As Integer = 1 To mintPageCntIndex
                    cmbPageRangeFrom.Items.Add(i.ToString)
                    cmbPageRangeTo.Items.Add(i.ToString)
                Next

                cmbDrawFrom.Items.Add("1")
                cmbDrawTo.Items.Add("1")
                For i As Integer = LBound(intData) To UBound(intData) Step 1
                    cmbDrawFrom.Items.Add(intData(i).ToString)
                    cmbDrawTo.Items.Add(intData(i).ToString)
                Next i
            Else
                ''レコードなし
                cmdPreview.Enabled = False
                cmdPrint.Enabled = False
            End If
            'optPageRangeAll.Checked = True
            If g_bytFUSet = 0 Then
                chkFormatType.Checked = True
            Else
                chkFormatType.Checked = False
            End If

            'Ver2.0.4.4
            'レンジ印刷するしない
            If g_bytTermRange = 0 Then
                'する
                chkRangePrint.Checked = False
            Else
                'する
                chkRangePrint.Checked = True
            End If

            'Ver2.0.7.4
            '基板ﾊﾞｰｼﾞｮﾝ印刷するしない
            If g_bytTerVer = 0 Then
                'する
                chkTerVer.Checked = False
            Else
                'しない
                chkTerVer.Checked = True
            End If

            'Ver2.0.8.7 DI端子表に共通コモンのメッセージ
            If g_bytTerDIMsg = 0 Then
                'しない
                chkTerDIMsg.Checked = False
            Else
                'する
                chkTerDIMsg.Checked = True
            End If

            bRet = True
        End If

        Return bRet
    End Function

    'Ver2.0.2.8 DRAW PAGE NO 取得関数
    Private Sub subGetDrawNo(ByVal udtFuInfo() As gTypFuInfo, ByRef pintDrawNo() As Integer)
        Dim i As Integer
        Dim j As Integer

        Dim lstAry As New ArrayList
        Dim intData As Integer

        For i = LBound(udtFuInfo) To UBound(udtFuInfo) Step 1
            For j = LBound(udtFuInfo(i).udtFuPort) To UBound(udtFuInfo(i).udtFuPort) Step 1
                If udtFuInfo(i).udtFuPort(j).intPortType <> 0 Then
                    If i = 0 Then
                        intData = 2 + j
                    Else
                        intData = (7 + (8 * (i - 1)) + j)
                    End If
                    lstAry.Add(intData)
                End If
            Next j
        Next i

        '一意の要素を一時的に格納しておくハッシュテーブル
        Dim ht As New System.Collections.Hashtable(lstAry.Count)

        '基になる配列の要素を列挙する
        For Each i In lstAry
            'ハッシュテーブルに追加する
            ht(i) = i
        Next

        Dim lastVal As ArrayList = New ArrayList(ht.Values)
        lastVal.Sort()

        '配列に変換する
        pintDrawNo = DirectCast(lastVal.ToArray(GetType(Integer)), Integer())


    End Sub

    'Ver2.0.2.9 FU ADR 取得関数
    Private Sub subGetFuAdr(ByVal udtFuInfo() As gTypFuInfo, ByRef pstrFUadr() As String)
        Dim i As Integer
        Dim j As Integer

        Dim lstAry As New ArrayList
        Dim strData As String

        For i = LBound(udtFuInfo) To UBound(udtFuInfo) Step 1
            For j = LBound(udtFuInfo(i).udtFuPort) To UBound(udtFuInfo(i).udtFuPort) Step 1
                If udtFuInfo(i).udtFuPort(j).intPortType <> 0 Then
                    strData = ""
                    If i = 0 Then
                        strData = "00," & (j + 1).ToString("00")
                    Else
                        strData = "" & i.ToString("00") & "," & (j + 1).ToString("00")
                    End If
                    lstAry.Add(strData)
                End If
            Next j
        Next i

        '一意の要素を一時的に格納しておくハッシュテーブル
        Dim ht As New System.Collections.Hashtable(lstAry.Count)

        '基になる配列の要素を列挙する
        Dim idx As Integer = 0
        For Each strAtai In lstAry
            'ハッシュテーブルに追加する
            ht(idx) = strAtai
            idx = idx + 1
        Next

        Dim lastVal As ArrayList = New ArrayList(ht.Values)
        lastVal.Sort()

        '配列に変換する
        pstrFUadr = DirectCast(lastVal.ToArray(GetType(String)), String())


    End Sub


    'Ver2.0.2.8 DRAW NOから一番若い通番取得
    Private Function fnGetDrawNoToTuBan(pintDrawNo As Integer) As Integer
        Dim i As Integer

        If pintDrawNo = 1 Then
            Return 0 - 1 - fnLUgetPageMax()
        End If


        For i = LBound(udtPageCnt) To UBound(udtPageCnt) Step 1
            If udtPageCnt(i).strDrawinfNoInfo = pintDrawNo.ToString Then
                Return i
            End If
        Next i

        Return -1
    End Function
    'Ver2.0.2.8 DRAW NOから一番大きい通番取得
    Private Function fnGetDrawNoToTuBan_MAX(pintDrawNo As Integer) As Integer
        Dim i As Integer
        Dim intRet As Integer = -1

        If pintDrawNo = 1 Then
            Return -1
        End If

        For i = LBound(udtPageCnt) To UBound(udtPageCnt) Step 1
            If udtPageCnt(i).strDrawinfNoInfo = pintDrawNo.ToString Then
                intRet = i
            Else
                If intRet <> -1 Then
                    Return intRet
                End If
            End If
        Next i

        Return -1
    End Function
#End Region
    '' Ver1.8.3  2015.11.26 
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

    Private Sub chkRangePrint_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkRangePrint.CheckedChanged
        Dim oldType As Byte

        oldType = g_bytTermRange

        ''初期ﾀｲﾌﾟで印字する場合はﾁｪｯｸを入れる
        If chkRangePrint.Checked = True Then
            g_bytTermRange = 1
        Else
            g_bytTermRange = 0
        End If

        '' 設定が変わった場合は保存ﾌﾗｸﾞをｾｯﾄ
        If oldType <> g_bytTermRange Then
            '' Verup処理中ならば何もしない
            If gudtFileInfo.blnVersionUP Then
                Debug.Print("Verup")
            Else
                gblnUpdateAll = True
            End If
        End If
    End Sub

    'Ver2.0.7.0 基板ﾊﾞｰｼﾞｮﾝ印刷対応 DEL
    Private Sub chkTerVer_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkTerVer.CheckedChanged

        If chkTerVer.Checked = True Then
            g_bytTerVer = 1
        Else
            g_bytTerVer = 0
        End If

    End Sub

    'Ver2.0.8.7 DI端子表に共通コモンのメッセージ
    Private Sub chkTerDIMsg_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkTerDIMsg.CheckedChanged

        If chkTerDIMsg.Checked = True Then
            g_bytTerDIMsg = 1
        Else
            g_bytTerDIMsg = 0
        End If

    End Sub



    'LU総ページ数取得
    Private Function fnLUgetPageMax() As Integer
        Dim intTemp As Integer = 0

        For i = 0 To UBound(gudt.SetFu.udtFu)
            If gudt.SetFu.udtFu(i).shtUse = 1 Then
                intTemp = i
            End If
        Next

        ''Page総数
        If intTemp = 0 Then
            '何も設定されていない場合は、ページ数「1」
            intTemp = 1
        Else
            intTemp = CInt((intTemp + 1) / 7 + 0.4)
        End If

        Return intTemp
    End Function

    ''ver2.0.8.I 2019.02.21 追加
    ''チェックボックスがある時、説明文が英和逆転する
    Private Sub chkExpTxtEtoJ_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkExpTxtEtoJ.CheckedChanged

        If chkExpTxtEtoJ.Checked = True Then
            g_bytExoTxtEtoJ = 1
        Else
            g_bytExoTxtEtoJ = 0
        End If

    End Sub
End Class