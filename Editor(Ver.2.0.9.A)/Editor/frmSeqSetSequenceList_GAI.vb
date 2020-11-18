Public Class frmSeqSetSequenceList_GAI

#Region "変数定義"

    Private mudtSetSequenceIDNew As gTypSetSeqID
    Private mudtSetSequenceSetNew As gTypSetSeqSet

    Private mudtCopyBuff As gTypSetSeqSetRec
    Private mudtCopyBuff_S() As gTypSetSeqSetRec

    'LogicName高速取得用
    Private prAryLogicNo As ArrayList
    Private prAryLogicName As ArrayList
#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 1:OK  0:キャンセル
    ' 引き数    : ARG1 - (IO) シーケンス設定構造体
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

#Region "Form"
    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームロード
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmSeqSetSequenceList_GAI_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            Dim i As Integer = 0

            'Ver2.0.4.1
            'クリップボードとﾌﾗｸﾞをｸﾘｱする。
            Clipboard.Clear()
            blnAllRowFlg = False

            'LogincName高速取得用配列設定
            Dim prSequenceLogic() As gTypCodeName = Nothing
            Call gGetComboCodeName(prSequenceLogic, gEnmComboType.ctSeqSetDetailLogic)
            prAryLogicNo = New ArrayList
            prAryLogicName = New ArrayList
            For i = 0 To UBound(prSequenceLogic) - 1 Step 1
                prAryLogicNo.Add(prSequenceLogic(i).shtCode.ToString)
                prAryLogicName.Add(prSequenceLogic(i).strName)
            Next i



            ''グリッド 初期設定
            Call mInitialDataGrid()

            ''構造体配列初期化
            Call mudtSetSequenceIDNew.InitArray()
            Call mudtSetSequenceSetNew.InitArray()
            For i = LBound(mudtSetSequenceSetNew.udtDetail) To UBound(mudtSetSequenceSetNew.udtDetail)
                Call mudtSetSequenceSetNew.udtDetail(i).InitArray()
            Next i

            ''構造体コピー
            Call mCopyStructure(gudt.SetSeqID, mudtSetSequenceIDNew)
            Call mCopyStructure(gudt.SetSeqSet, mudtSetSequenceSetNew)

            ''画面設定
            Call mSetDisplay(mudtSetSequenceSetNew)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
    '--------------------------------------------------------------------
    ' 機能      : フォームクローズ中
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 設定が変更されている場合は確認メッセージを表示する
    '--------------------------------------------------------------------
    Private Sub frmSysSystem_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try

            Dim blnFlgID As Boolean = False
            Dim blnFlgSet As Boolean = False

            ''設定値を比較用構造体に格納
            Call mSetStructure(mudtSetSequenceIDNew)
            Call mSetStructure(mudtSetSequenceSetNew)


            ''更新フラグ取得
            If Not mChkStructureEquals(mudtSetSequenceIDNew, gudt.SetSeqID) Then blnFlgID = True
            If Not mChkStructureEquals(mudtSetSequenceSetNew, gudt.SetSeqSet) Then blnFlgSet = True

            ''データが変更されているかチェック
            If blnFlgID Or blnFlgSet Then

                ''変更されている場合はメッセージ表示
                Select Case MessageBox.Show("Setting has been changed." & vbNewLine & _
                                            "Do you save the changes?", Me.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                    Case Windows.Forms.DialogResult.Yes

                        ''入力チェック
                        If Not mChkInput() Then
                            e.Cancel = True
                            Return
                        End If

                        ''変更された場合は設定を更新する
                        If blnFlgID Then
                            Call mCopyStructure(mudtSetSequenceIDNew, gudt.SetSeqID)
                            gblnUpdateAll = True
                            gudt.SetEditorUpdateInfo.udtSave.bytSeqSequenceID = 1
                            gudt.SetEditorUpdateInfo.udtCompile.bytSeqSequenceID = 1
                        End If

                        If blnFlgSet Then
                            Call mCopyStructure(mudtSetSequenceSetNew, gudt.SetSeqSet)
                            gblnUpdateAll = True
                            gudt.SetEditorUpdateInfo.udtSave.bytSeqSequenceSet = 1
                            gudt.SetEditorUpdateInfo.udtCompile.bytSeqSequenceSet = 1
                        End If



                    Case Windows.Forms.DialogResult.No

                        ''何もしない

                    Case Windows.Forms.DialogResult.Cancel

                        ''画面を閉じない
                        e.Cancel = True

                End Select

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : フォームクローズ
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : フォームのインスタンスを破棄する
    '--------------------------------------------------------------------
    Private Sub frmSysSystem_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            Me.Dispose()
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "Button"
    '--------------------------------------------------------------------
    ' 機能      : Saveボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 保存処理を行う
    '--------------------------------------------------------------------
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Try

            Dim blnFlgID As Boolean = False
            Dim blnFlgSet As Boolean = False


            ''入力チェック
            If Not mChkInput() Then Return

            ''設定値を比較用構造体に格納
            Call mSetStructure(mudtSetSequenceIDNew)
            Call mSetStructure(mudtSetSequenceSetNew)


            ''シーケンスID（USE列）が変更されている場合は設定を更新する
            If Not mChkStructureEquals(mudtSetSequenceIDNew, gudt.SetSeqID) Then
                Call mCopyStructure(mudtSetSequenceIDNew, gudt.SetSeqID)
                blnFlgID = True
            End If

            ''シーケンス設定が変更されている場合は設定を更新する
            If Not mChkStructureEquals(mudtSetSequenceSetNew, gudt.SetSeqSet) Then
                Call mCopyStructure(mudtSetSequenceSetNew, gudt.SetSeqSet)
                blnFlgSet = True
            End If

            If blnFlgID Or blnFlgSet Then

                ''メッセージ表示
                Call MessageBox.Show("It saved.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                ''更新フラグ設定
                gblnUpdateAll = True
                gudt.SetEditorUpdateInfo.udtSave.bytSeqSequenceID = 1
                gudt.SetEditorUpdateInfo.udtSave.bytSeqSequenceSet = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytSeqSequenceID = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytSeqSequenceSet = 1

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Deleteボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 選択行の設定を削除する
    '--------------------------------------------------------------------
    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        'Ver2.0.1.5 複数行選択対応

        Try

            ''選択セルの行位置が0より小さい、もしくは最大行数より大きい場合は処理を抜ける
            If grdSEQ.CurrentCell.RowIndex < 0 Or _
               grdSEQ.CurrentCell.RowIndex > grdSEQ.RowCount - 1 Then Return

            If MessageBox.Show("Do you delete the selected sequence?", _
                               Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                With grdSEQ
                    If .SelectedRows.Count > 0 Then
                        Dim rowIndexs(.SelectedRows.Count - 1) As Integer
                        Dim i As Integer
                        '選択されている行のIndexを取得し
                        For i = 0 To .SelectedRows.Count - 1
                            rowIndexs(i) = .SelectedRows(i).Index
                        Next
                        '昇順に並び替えて
                        Array.Sort(rowIndexs)
                        '配列に格納する
                        For i = 0 To .SelectedRows.Count - 1
                            'シーケンスIDリセット
                            Call gInitSetSeqSequenceIDOne(mudtSetSequenceIDNew.shtID(rowIndexs(i)))
                            'シーケンス設定リセット
                            Call gInitSetSeqSequenceDetailOne(mudtSetSequenceSetNew.udtDetail(rowIndexs(i)).shtId, _
                                                              mudtSetSequenceSetNew.udtDetail(rowIndexs(i)))
                            '画面更新
                            Call mSetDisplayOne(rowIndexs(i), mudtSetSequenceSetNew.udtDetail(rowIndexs(i)))
                        Next
                    End If
                End With

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Printボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 画面のハードコピーを行う
    '--------------------------------------------------------------------
    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click

        Try
            'Ver2.0.0.7
            '印刷は、一覧のCSV化とする
            'Call gPrintScreen(True)

            '出力開始
            Call subOpen()


            'グリッドをファイルへ書き込み
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim strVal As String = ""

            '■タイトル生成
            For i = 0 To grdSEQ.ColumnCount - 1 Step 1
                'タイトル生成
                Select Case i
                    Case 1
                        '1=LogicNoは何もしない
                    Case Else
                        strVal = strVal & """" & grdSEQ.Columns(i).HeaderText & ""","
                End Select
            Next i
            strVal = strVal.TrimEnd(CType(",", Char))
            Call subWrite(strVal)

            '■データ生成
            For i = 0 To grdSEQ.RowCount - 1 Step 1
                strVal = ""
                For j = 0 To grdSEQ.ColumnCount - 1 Step 1
                    Dim strData As String = grdSEQ(j, i).Value
                    strData = NZf(strData)
                    Select Case j
                        Case 1
                            '1=LogicNoは何もしない
                        Case grdSEQ.ColumnCount - 1
                            '最後はカンマ無し。
                            strVal = strVal & strData
                        Case Else
                            strVal = strVal & strData & ","
                    End Select
                Next j
                Call subWrite(strVal)
            Next i


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        Finally
            '出力終了
            Call subClose()

            '終了ﾒｯｾｰｼﾞ
            Call MessageBox.Show("Has completed.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub
#Region "ファイル出力関連関数"
    'Ver2.0.0.7
    Private sw As IO.StreamWriter
    'ファイルオープン
    Private Sub subOpen()
        Dim dt As DateTime = Now
        Dim strPathBase As String = ""
        strPathBase = System.IO.Path.Combine(gudtFileInfo.strFilePath, gudtFileInfo.strFileVersion)
        strPathBase = System.IO.Path.Combine(strPathBase, "SEQ_LIST_" & dt.ToString("yyyyMMddHHmmss") & ".csv")

        sw = Nothing
        Try
            sw = New IO.StreamWriter(strPathBase, True, System.Text.Encoding.GetEncoding("Shift-JIS"))
        Catch ex As Exception
        End Try
    End Sub
    'データ書き込み
    Private Sub subWrite(pstrMsg As String)
        sw.WriteLine(pstrMsg)
    End Sub
    'ファイルクローズ
    Private Sub subClose()
        If sw Is Nothing = False Then sw.Close()
    End Sub
#End Region

    '--------------------------------------------------------------------
    ' 機能      : Exitボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : フォームを閉じる
    '--------------------------------------------------------------------
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub


    'ChNo Searchボタンクリック
    '一覧内ChNo検索
    Private Sub btnChNoSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnChNoSearch.Click
        Try
            Dim i As Integer
            Dim j As Integer
            Dim strChNo As String
            Dim culj As Integer

            'ChNoが未入力なら処理抜け
            If txtChNoSearch.Text.Trim = "" Then
                Exit Sub
            End If

            If IsNumeric(txtChNoSearch.Text) = True Then
                strChNo = CInt(txtChNoSearch.Text.Trim).ToString("0000")
            Else
                strChNo = txtChNoSearch.Text.Trim
            End If


            'グリッドを検索
            With grdSEQ
                culj = .CurrentCell.RowIndex + 1
                If culj > .RowCount - 1 Then
                    culj = 0
                End If

                '現在の選択行から下を検索する
                For i = 3 To .ColumnCount - 1 Step 1
                    For j = culj To .RowCount - 1 Step 1
                        If .Item(i, j).Value = strChNo Then
                            .CurrentCell = grdSEQ(i, j)
                            Exit Sub
                        End If
                    Next j
                Next i
                '見つからない場合は頭から現在の選択行まで検索する
                For i = 3 To .ColumnCount - 1 Step 1
                    For j = 0 To culj Step 1
                        If .Item(i, j).Value = strChNo Then
                            .CurrentCell = grdSEQ(i, j)
                            Exit Sub
                        End If
                    Next j
                Next i

                '完全に見つからない場合はメッセージを出して終了
                MessageBox.Show("No CHNo.", "CHNo Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub


    'Clearボタン機能は非表示
    '--------------------------------------------------------------------
    ' 機能      : Clearボタンクリック
    '--------------------------------------------------------------------
    Private Sub cmdClear_Click(sender As System.Object, e As System.EventArgs) Handles cmdClear.Click
        Try
            If MessageBox.Show("Do you Clear the sequence details?", _
                               Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Call ClearSeqSetting()
                '保存
                Call mCopyStructure(mudtSetSequenceIDNew, gudt.SetSeqID)
                Call mCopyStructure(mudtSetSequenceSetNew, gudt.SetSeqSet)
                ''更新フラグ設定
                gblnUpdateAll = True
                gudt.SetEditorUpdateInfo.udtSave.bytSeqSequenceID = 1
                gudt.SetEditorUpdateInfo.udtSave.bytSeqSequenceSet = 1
                gudt.SetEditorUpdateInfo.udtSave.bytSeqLinear = 1
                gudt.SetEditorUpdateInfo.udtSave.bytSeqOperationExpression = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytSeqSequenceID = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytSeqSequenceSet = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytSeqLinear = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytSeqOperationExpression = 1
                '再表示
                Call mInitialDataGrid()
                Call mSetDisplay(mudtSetSequenceSetNew)

            End If
            'Me.Close()
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    '--------------------------------------------------------------------
    ' 機能      : ｼｰｹﾝｽ設定ｸﾘｱ
    '--------------------------------------------------------------------
    Private Sub ClearSeqSetting()
        Dim i As Integer
        Dim j As Integer

        For i = LBound(mudtSetSequenceSetNew.udtDetail) To UBound(mudtSetSequenceSetNew.udtDetail)
            With mudtSetSequenceSetNew.udtDetail(i)
                'Ver2.0.4.8
                .shtId = i + 1 + 10000
                .shtLogicType = 0

                '' 入力ﾁｪｯｸ
                For j = 0 To UBound(.udtInput)
                    With .udtInput(j)

                        .shtSysno = 0
                        .shtChid = 0            ''CHID
                        .shtChSelect = 0        ''CH選択
                        .shtIoSelect = 0        ''入出力区分
                        .bytStatus = 0          ''参照ステータス
                        .bytType = 0            ''タイプ
                        .shtMask = 0
                        .shtAnalogType = 0
                        .strSpare = 0
                    End With
                Next

                .strRemarks = ""

                For j = LBound(.shtUseCh) To UBound(.shtUseCh)
                    .shtUseCh(j) = 0
                Next

                ' 出力設定
                .shtOutSysno = 0
                .shtOutChid = 0
                .shtOutData = 0         ''出力データ
                .shtOutDelay = 0        ''出力オフディレイ
                .bytOutStatus = 0       ''出力ステータス
                .bytOutIoSelect = 0     ''入出力区分
                .bytOutDataType = 0     ''出力データタイプ
                .bytOutInv = 0          ''出力反転
                .bytFuno = 255            ''FU　番号
                .bytPort = 255            ''FU ポート番号
                .bytPin = 255             ''FU　計測点位置
                .bytPinNo = 0           ''FU　計測点個数
                .bytOutType = 0         ''出力タイプ
                .bytOneShot = 0         ''出力ワンショット時間
                .bytContine = 0         ''処理継続中止
                .bytSpare1 = 0          ''備考
            End With
        Next

        '' ｼｰｹﾝｽID
        For i = LBound(mudtSetSequenceIDNew.shtID) To UBound(mudtSetSequenceIDNew.shtID)
            mudtSetSequenceIDNew.shtID(i) = 0
        Next

        'Ver2.0.2.8 リニアライズテーブル削除
        'リニアライズテーブル削除
        For i = LBound(gudt.SetSeqLinear.udtPoints) To UBound(gudt.SetSeqLinear.udtPoints)
            With gudt.SetSeqLinear.udtPoints(i)
                .shtPoint = 0
            End With
        Next i
        For i = LBound(gudt.SetSeqLinear.udtTables) To UBound(gudt.SetSeqLinear.udtTables)
            With gudt.SetSeqLinear.udtTables(i)
                For j = LBound(.udtRow) To UBound(.udtRow)
                    .udtRow(j).sngPtX = 0
                    .udtRow(j).sngPtY = 0
                Next j
            End With
        Next i

        'Ver2.0.2.8 演算式テーブル削除
        '演算式ﾃｰﾌﾞﾙ削除
        For i = LBound(gudt.SetSeqOpeExp.udtTables) To UBound(gudt.SetSeqOpeExp.udtTables)
            With gudt.SetSeqOpeExp.udtTables(i)
                .strExp = ""
                For j = LBound(.strVariavleName) To UBound(.strVariavleName)
                    .strVariavleName(j) = ""
                Next j
                For j = LBound(.udtAryInf) To UBound(.udtAryInf)
                    .udtAryInf(j).shtType = 0
                    .udtAryInf(j).bytInfo(0) = 0
                    .udtAryInf(j).bytInfo(1) = 0
                    .udtAryInf(j).bytInfo(2) = 0
                    .udtAryInf(j).bytInfo(3) = 0
                    .udtAryInf(j).strFixNum = ""
                Next j
            End With
        Next i


        'Grid Clear
        For i = 0 To grdSEQ.RowCount - 1 Step 1
            grdSEQ.Item(1, i).Value() = False
        Next i
    End Sub

#End Region


#Region "GRID"
    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッドダブルクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdSEQ_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSEQ.CellDoubleClick

        Try

            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub

            'ロジックNoが指定されていない場合ロジック選択画面へ
            'LogicNo取得
            Dim intLogicNo As Integer = CCUInt16(grdSEQ(1, e.RowIndex).Value)
            If intLogicNo <= 0 Then
                Dim strSelectLogicCode As String = ""
                Dim strSelectLogicName As String = ""
                Dim udtSequenceLogic() As gTypCodeName = Nothing
                'シーケンスロジック設定取得
                Call gGetComboCodeName(udtSequenceLogic, gEnmComboType.ctSeqSetDetailLogic)
                If frmSeqSetSequenceLogic.gShow(udtSequenceLogic, strSelectLogicCode, strSelectLogicName, Me) <> 0 Then
                    'ロジック設定しなければ次画面へ遷移させない
                    Exit Sub
                Else
                    mudtSetSequenceSetNew.udtDetail(e.RowIndex).shtLogicType = CCUInt16(strSelectLogicCode)
                End If
            End If


            ''シーケンス設定詳細画面表示
            If frmSeqSetSequenceDetail_GAI.gShow(mudtSetSequenceSetNew, mudtSetSequenceSetNew.udtDetail(e.RowIndex), Me) = 1 Then

                ''画面更新
                Call mSetDisplay(mudtSetSequenceSetNew)

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベントを発生させる
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdSEQ_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdSEQ.EditingControlShowing

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdSEQ_KeyPress

                ''イベントハンドラを追加する
                AddHandler tb.KeyPress, AddressOf grdSEQ_KeyPress

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベント発生時処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdSEQ_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdSEQ.KeyPress

        Try
            'グリット内への入力時の制御

            '選択セルの名称取得
            Dim strColumnName As String = grdSEQ.CurrentCell.OwningColumn.Name

            '[REMARKS]
            If strColumnName = "txtRemarks" Then
                e.Handled = gCheckTextInput(16, sender, e.KeyChar, False)
            End If

            Select Case strColumnName
                Case "txtCh1", "txtCh2", "txtCh3", "txtCh4", "txtCh5", "txtCh6", "txtCh7", "txtCh8", "txtOutCH"
                    '[InputCH]1～8,[OutCH] MAX5桁の数値のみ
                    e.Handled = gCheckTextInput(5, sender, e.KeyChar, True)
                Case "txtOutFU", "txtOutSlot"
                    '[Out FU],[Out SLOT] MAX2桁の数値のみ
                    e.Handled = gCheckTextInput(2, sender, e.KeyChar, True)
                Case "txtOutPin"
                    '[Out PIN] MAX3桁の数値のみ
                    e.Handled = gCheckTextInput(3, sender, e.KeyChar, True)
            End Select


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
    Private Sub grdSEQ_CellValidating(sender As Object, e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles grdSEQ.CellValidating
        Try
            'グリット入力直後の入力値妥当性チェック

            If e.RowIndex < 0 Or e.ColumnIndex < 2 Then Exit Sub

            Dim strValue As String
            strValue = grdSEQ(e.ColumnIndex, e.RowIndex).EditedFormattedValue

            'LogicNo取得
            Dim intLogicNo As Integer = CCUInt16(grdSEQ(1, e.RowIndex).Value)

            '選択セルの名称取得
            Dim strColumnName As String = grdSEQ.CurrentCell.OwningColumn.Name

            If NZfS(strValue).Trim <> "" Then
                'LogicNo未指定の場合エラー
                If intLogicNo <= 0 Then
                    MsgBox("Logic Not selected.", MsgBoxStyle.Exclamation, "CONTROL SEQUENCE List")
                    e.Cancel = True : Exit Sub
                End If

                Select Case strColumnName
                    Case "txtRemarks"
                        '[REMARKS]
                    Case "txtCh1", "txtCh2", "txtCh3", "txtCh4", "txtCh5", "txtCh6", "txtCh7", "txtCh8", "txtOutCH"
                        '[InputCH]1～8,[OutCH] MAX5桁の数値のみ 101～11024
                        If strValue.Length > 5 Or (CCInt(strValue) > 11024 Or CCInt(strValue) < 101) Then
                            MsgBox("The value is illegal.", MsgBoxStyle.Exclamation, "CONTROL SEQUENCE List")
                            e.Cancel = True : Exit Sub
                        End If
                    Case "txtOutFU", "txtOutSlot"
                        '[Out FU],[Out SLOT] MAX2桁の数値のみ 0～20
                        If strValue.Length > 2 Or (CCInt(strValue) > 20 Or CCInt(strValue) < 0) Then
                            MsgBox("The value is illegal.", MsgBoxStyle.Exclamation, "CONTROL SEQUENCE List")
                            e.Cancel = True : Exit Sub
                        End If
                    Case "txtOutPin"
                        '[Out PIN] MAX3桁の数値のみ 0～999
                        If strValue.Length > 3 Or (CCInt(strValue) > 999 Or CCInt(strValue) < 0) Then
                            MsgBox("The value is illegal.", MsgBoxStyle.Exclamation, "CONTROL SEQUENCE List")
                            e.Cancel = True : Exit Sub
                        End If
                End Select
            Else
                '未入力はOK
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッド行ヘッダーダブルクリック
    ' 引数      ： なし
    ' 戻値      ： なし 
    '----------------------------------------------------------------------------
    Private Sub grdSEQ_RowHeaderMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdSEQ.RowHeaderMouseDoubleClick

        Try

            ''グリッドの保留中の変更を全て適用させる
            grdSEQ.EndEdit()

            ''行数が0より小さい、もしくは最大行数より大きい場合処理を抜ける
            If e.RowIndex < 0 Or e.RowIndex > grdSEQ.RowCount - 1 Then Return

            'ロジックNoが指定されていない場合ロジック選択画面へ
            'LogicNo取得
            Dim intLogicNo As Integer = CCUInt16(grdSEQ(1, e.RowIndex).Value)
            If intLogicNo <= 0 Then
                Dim strSelectLogicCode As String = ""
                Dim strSelectLogicName As String = ""
                Dim udtSequenceLogic() As gTypCodeName = Nothing
                'シーケンスロジック設定取得
                Call gGetComboCodeName(udtSequenceLogic, gEnmComboType.ctSeqSetDetailLogic)
                If frmSeqSetSequenceLogic.gShow(udtSequenceLogic, strSelectLogicCode, strSelectLogicName, Me) <> 0 Then
                    'ロジック設定しなければ次画面へ遷移させない
                    Exit Sub
                Else
                    mudtSetSequenceSetNew.udtDetail(e.RowIndex).shtLogicType = CCUInt16(strSelectLogicCode)
                End If
            End If
            

            ''シーケンス設定詳細画面表示
            If frmSeqSetSequenceDetail_GAI.gShow(mudtSetSequenceSetNew, mudtSetSequenceSetNew.udtDetail(e.RowIndex), Me) = 1 Then

                ''画面更新
                Call mSetDisplay(mudtSetSequenceSetNew)

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub grdSEQ_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSEQ.KeyDown
        'Ver2.0.1.5 複数行コポペ機能化

        Dim intCurRowIndex As Integer
        Dim intBackupID As Integer

        If grdSEQ.SelectedRows.Count <> 0 Then

            ''対象行インデックス取得
            'intCurRowIndex = grdSEQ.SelectedCells(0).RowIndex

            If (e.Modifiers And Keys.Control) = Keys.Control And e.KeyCode = Keys.C Then
                ''Ctrl+C の場合は構造体コピー
                'mudtCopyBuff = DeepCopyHelper.DeepCopy(mudtSetSequenceSetNew.udtDetail(intCurRowIndex))
                With grdSEQ
                    If .SelectedRows.Count > 0 Then
                        Erase mudtCopyBuff_S
                        ReDim mudtCopyBuff_S(.SelectedRows.Count - 1)
                        Dim rowIndexs(.SelectedRows.Count - 1) As Integer
                        Dim i As Integer
                        '選択されている行のIndexを取得し
                        For i = 0 To .SelectedRows.Count - 1
                            rowIndexs(i) = .SelectedRows(i).Index
                        Next
                        '昇順に並び替えて
                        Array.Sort(rowIndexs)
                        '配列に格納する
                        For i = 0 To .SelectedRows.Count - 1
                            mudtCopyBuff_S(i) = DeepCopyHelper.DeepCopy(mudtSetSequenceSetNew.udtDetail(rowIndexs(i)))
                        Next
                    End If
                End With
            ElseIf (e.Modifiers And Keys.Control) = Keys.Control And e.KeyCode = Keys.V Then

                ''コピーバッファに何もない場合は処理を抜ける
                'If mudtCopyBuff.shtId = 0 Then Return
                If IsNothing(mudtCopyBuff_S) = True Then Return
                If mudtCopyBuff_S(0).shtId = 0 Then Return

                '選択先頭行番号取得
                intCurRowIndex = grdSEQ.SelectedCells(0).RowIndex

                ''貼りつけ前に現在のSeqIDを保存
                'intBackupID = mudtSetSequenceSetNew.udtDetail(intCurRowIndex).shtId
                ''コピーバッファにある情報を構造体に上書き
                'mudtSetSequenceSetNew.udtDetail(intCurRowIndex) = DeepCopyHelper.DeepCopy(mudtCopyBuff)
                ''SeqIDを元に戻す
                'mudtSetSequenceSetNew.udtDetail(intCurRowIndex).shtId = intBackupID
                ''画面更新
                'Call mSetDisplayOne(intCurRowIndex, mudtSetSequenceSetNew.udtDetail(intCurRowIndex))

                Dim i As Integer = 0
                For i = 0 To UBound(mudtCopyBuff_S) Step 1
                    '貼りつけ前に現在のSeqIDを保存
                    intBackupID = mudtSetSequenceSetNew.udtDetail(intCurRowIndex + i).shtId
                    'コピーバッファにある情報を構造体に上書き
                    mudtSetSequenceSetNew.udtDetail(intCurRowIndex + i) = DeepCopyHelper.DeepCopy(mudtCopyBuff_S(i))
                    'SeqIDを元に戻す
                    mudtSetSequenceSetNew.udtDetail(intCurRowIndex + i).shtId = intBackupID
                    '画面更新
                    Call mSetDisplayOne(intCurRowIndex + i, mudtSetSequenceSetNew.udtDetail(intCurRowIndex + i))
                Next i

            End If
        End If


    End Sub

#End Region

#End Region

#Region "内部関数"

    '--------------------------------------------------------------------
    ' 機能      : 入力チェック
    ' 返り値    : True:入力OK、False:入力NG
    ' 引き数    : なし
    ' 機能説明  : 入力チェックを行う
    '--------------------------------------------------------------------
    Private Function mChkInput() As Boolean

        Try

            ''グリッドの保留中の変更を全て適用させる
            grdSEQ.EndEdit()

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#Region "シーケンスID用"

#Region "構造体複製"

    '--------------------------------------------------------------------
    ' 機能      : 構造体複製
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 複製元
    ' 　　　    : ARG1 - ( O) 複製先
    ' 機能説明  : 構造体を複製する
    ' 備考　　  : 構造体メンバの中に構造体配列がいると単純に = では複製できないため関数を用意
    ' 　　　　  : ↑ = でやると配列部分が参照渡しになり（？）値更新時に両方更新されてしまう
    ' 　　　　  : 構造体メンバの中に構造体配列がいない場合は、この関数を使わずに = で処理しても良い
    '--------------------------------------------------------------------
    Private Sub mCopyStructure(ByVal udtSource As gTypSetSeqID, _
                               ByRef udtTarget As gTypSetSeqID)

        Try

            ''シーケンスIDコピー
            For i As Integer = LBound(udtSource.shtID) To UBound(udtSource.shtID)

                udtTarget.shtID(i) = udtSource.shtID(i)

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "構造体比較"

    '--------------------------------------------------------------------
    ' 機能      : 構造体比較
    ' 返り値    : True:相違なし、False:相違あり
    ' 引き数    : ARG1 - (I ) 構造体１
    ' 　　　    : ARG1 - (I ) 構造体２
    ' 機能説明  : 構造体の設定値を比較する
    ' 備考　　  : 構造体メンバの中に構造体配列がいると Equals メソッドで正しい結果が得られないため関数を用意
    ' 　　　　  : 構造体メンバの中に構造体配列がいない場合は、 Equals メソッドで処理しても良いが一応これを使うこと
    ' 　　　　  : String文字列の比較には gCompareString を使用すること（単純な = だとNULL文字の有り無しで結果が変わってしまう）
    '--------------------------------------------------------------------
    Private Function mChkStructureEquals(ByVal udt1 As gTypSetSeqID, _
                                         ByVal udt2 As gTypSetSeqID) As Boolean

        Try


            ''シーケンスID比較
            For i As Integer = LBound(udt1.shtID) To UBound(udt2.shtID)

                If udt1.shtID(i) <> udt2.shtID(i) Then Return False

            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "設定値格納"

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) リニアライズテーブル構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet As gTypSetSeqID)

        Try

            For intRow As Integer = LBound(udtSet.shtID) To UBound(udtSet.shtID)
                'LogicNoが設定されている場合にチェック
                udtSet.shtID(intRow) = IIf(CCUInt16(grdSEQ(1, intRow).Value) > 0, 1, 0)
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#End Region

#Region "シーケンス設定用"

#Region "構造体複製"

    '--------------------------------------------------------------------
    ' 機能      : 構造体複製
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 複製元
    ' 　　　    : ARG1 - ( O) 複製先
    ' 機能説明  : 構造体を複製する
    ' 備考　　  : 構造体メンバの中に構造体配列がいると単純に = では複製できないため関数を用意
    ' 　　　　  : ↑ = でやると配列部分が参照渡しになり（？）値更新時に両方更新されてしまう
    ' 　　　　  : 構造体メンバの中に構造体配列がいない場合は、この関数を使わずに = で処理しても良い
    '--------------------------------------------------------------------
    Private Sub mCopyStructure(ByVal udtSource As gTypSetSeqSet, _
                               ByRef udtTarget As gTypSetSeqSet)

        Try

            For i As Integer = LBound(udtSource.udtDetail) To UBound(udtSource.udtDetail)

                ''シーケンス設定コピー
                udtTarget.udtDetail(i).shtId = udtSource.udtDetail(i).shtId                         ''シーケンスＩＤ
                udtTarget.udtDetail(i).shtLogicType = udtSource.udtDetail(i).shtLogicType           ''出力ロジックタイプ
                udtTarget.udtDetail(i).strRemarks = udtSource.udtDetail(i).strRemarks               ''備考
                udtTarget.udtDetail(i).shtOutSysno = udtSource.udtDetail(i).shtOutSysno             ''SYSTEM No.
                udtTarget.udtDetail(i).shtOutChid = udtSource.udtDetail(i).shtOutChid               ''CH ID
                udtTarget.udtDetail(i).shtOutData = udtSource.udtDetail(i).shtOutData               ''出力データ
                udtTarget.udtDetail(i).shtOutDelay = udtSource.udtDetail(i).shtOutDelay             ''出力オフディレイ
                udtTarget.udtDetail(i).bytOutStatus = udtSource.udtDetail(i).bytOutStatus           ''出力ステータス
                udtTarget.udtDetail(i).bytOutIoSelect = udtSource.udtDetail(i).bytOutIoSelect       ''入出力区分
                udtTarget.udtDetail(i).bytOutDataType = udtSource.udtDetail(i).bytOutDataType       ''出力データタイプ
                udtTarget.udtDetail(i).bytOutInv = udtSource.udtDetail(i).bytOutInv                 ''出力反転
                udtTarget.udtDetail(i).bytFuno = udtSource.udtDetail(i).bytFuno                     ''FU　番号
                udtTarget.udtDetail(i).bytPort = udtSource.udtDetail(i).bytPort                     ''FU ポート番号
                udtTarget.udtDetail(i).bytPin = udtSource.udtDetail(i).bytPin                       ''FU　計測点位置
                udtTarget.udtDetail(i).bytPinNo = udtSource.udtDetail(i).bytPinNo                   ''FU　計測点個数
                udtTarget.udtDetail(i).bytOutType = udtSource.udtDetail(i).bytOutType               ''出力タイプ
                udtTarget.udtDetail(i).bytOneShot = udtSource.udtDetail(i).bytOneShot               ''出力ワンショット時間
                udtTarget.udtDetail(i).bytContine = udtSource.udtDetail(i).bytContine               ''処理継続中止

                ''演算参照テーブル比較
                For j As Integer = LBound(udtSource.udtDetail(i)._shtLogicItem) To UBound(udtSource.udtDetail(i)._shtLogicItem)
                    udtTarget.udtDetail(i).shtLogicItem(j) = udtSource.udtDetail(i).shtLogicItem(j)
                Next

                ''チャンネル使用有無比較
                For j As Integer = LBound(udtSource.udtDetail(i)._shtLogicItem) To UBound(udtSource.udtDetail(i)._shtLogicItem)
                    udtTarget.udtDetail(i).shtUseCh(j) = udtSource.udtDetail(i).shtUseCh(j)
                Next

                ''Input情報コピー
                For j As Integer = LBound(udtSource.udtDetail(i).udtInput) To UBound(udtSource.udtDetail(i).udtInput)
                    udtTarget.udtDetail(i).udtInput(j).shtSysno = udtSource.udtDetail(i).udtInput(j).shtSysno
                    udtTarget.udtDetail(i).udtInput(j).shtChid = udtSource.udtDetail(i).udtInput(j).shtChid
                    udtTarget.udtDetail(i).udtInput(j).shtChSelect = udtSource.udtDetail(i).udtInput(j).shtChSelect
                    udtTarget.udtDetail(i).udtInput(j).shtIoSelect = udtSource.udtDetail(i).udtInput(j).shtIoSelect
                    udtTarget.udtDetail(i).udtInput(j).bytStatus = udtSource.udtDetail(i).udtInput(j).bytStatus
                    udtTarget.udtDetail(i).udtInput(j).bytType = udtSource.udtDetail(i).udtInput(j).bytType
                    udtTarget.udtDetail(i).udtInput(j).shtMask = udtSource.udtDetail(i).udtInput(j).shtMask
                    udtTarget.udtDetail(i).udtInput(j).shtAnalogType = 1
                Next

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "構造体比較"

    '--------------------------------------------------------------------
    ' 機能      : 構造体比較
    ' 返り値    : True:相違なし、False:相違あり
    ' 引き数    : ARG1 - (I ) 構造体１
    ' 　　　    : ARG1 - (I ) 構造体２
    ' 機能説明  : 構造体の設定値を比較する
    ' 備考　　  : 構造体メンバの中に構造体配列がいると Equals メソッドで正しい結果が得られないため関数を用意
    ' 　　　　  : 構造体メンバの中に構造体配列がいない場合は、 Equals メソッドで処理しても良いが一応これを使うこと
    ' 　　　　  : String文字列の比較には gCompareString を使用すること（単純な = だとNULL文字の有り無しで結果が変わってしまう）
    '--------------------------------------------------------------------
    Private Function mChkStructureEquals(ByVal udt1 As gTypSetSeqSet, _
                                         ByVal udt2 As gTypSetSeqSet) As Boolean

        Try

            For i As Integer = LBound(udt1.udtDetail) To UBound(udt1.udtDetail)

                ''シーケンス設定比較
                If udt1.udtDetail(i).shtId <> udt2.udtDetail(i).shtId Then Return False ''シーケンスＩＤ
                If udt1.udtDetail(i).shtLogicType <> udt2.udtDetail(i).shtLogicType Then Return False ''出力ロジックタイプ
                If udt1.udtDetail(i).shtOutSysno <> udt2.udtDetail(i).shtOutSysno Then Return False ''SYSTEM No.
                If Not gCompareString(udt1.udtDetail(i).strRemarks, udt2.udtDetail(i).strRemarks) Then Return False ''備考
                If udt1.udtDetail(i).shtOutChid <> udt2.udtDetail(i).shtOutChid Then Return False ''CH ID
                If udt1.udtDetail(i).shtOutData <> udt2.udtDetail(i).shtOutData Then Return False ''出力データ
                If udt1.udtDetail(i).shtOutDelay <> udt2.udtDetail(i).shtOutDelay Then Return False ''出力オフディレイ
                If udt1.udtDetail(i).bytOutStatus <> udt2.udtDetail(i).bytOutStatus Then Return False ''出力ステータス
                If udt1.udtDetail(i).bytOutIoSelect <> udt2.udtDetail(i).bytOutIoSelect Then Return False ''入出力区分
                If udt1.udtDetail(i).bytOutDataType <> udt2.udtDetail(i).bytOutDataType Then Return False ''出力データタイプ
                If udt1.udtDetail(i).bytOutInv <> udt2.udtDetail(i).bytOutInv Then Return False ''出力反転
                If udt1.udtDetail(i).bytFuno <> udt2.udtDetail(i).bytFuno Then Return False ''FU　番号
                If udt1.udtDetail(i).bytPort <> udt2.udtDetail(i).bytPort Then Return False ''FU ポート番号
                If udt1.udtDetail(i).bytPin <> udt2.udtDetail(i).bytPin Then Return False ''FU　計測点位置
                If udt1.udtDetail(i).bytPinNo <> udt2.udtDetail(i).bytPinNo Then Return False ''FU　計測点位置
                If udt1.udtDetail(i).bytOutType <> udt2.udtDetail(i).bytOutType Then Return False ''出力タイプ
                If udt1.udtDetail(i).bytOneShot <> udt2.udtDetail(i).bytOneShot Then Return False ''出力ワンショット時間
                If udt1.udtDetail(i).bytContine <> udt2.udtDetail(i).bytContine Then Return False ''処理継続中止

                ''演算参照テーブル比較
                For j As Integer = LBound(udt1.udtDetail(i)._shtLogicItem) To UBound(udt1.udtDetail(i)._shtLogicItem)
                    If udt1.udtDetail(i).shtLogicItem(j) <> udt2.udtDetail(i).shtLogicItem(j) Then Return False
                Next

                ''チャンネル使用有無比較
                For j As Integer = LBound(udt1.udtDetail(i)._shtLogicItem) To UBound(udt1.udtDetail(i)._shtLogicItem)
                    If udt1.udtDetail(i).shtUseCh(j) <> udt2.udtDetail(i).shtUseCh(j) Then Return False
                Next

                ''Input情報比較
                For j As Integer = LBound(udt1.udtDetail(i).udtInput) To UBound(udt1.udtDetail(i).udtInput)
                    If udt1.udtDetail(i).udtInput(j).shtSysno <> udt2.udtDetail(i).udtInput(j).shtSysno Then Return False
                    If udt1.udtDetail(i).udtInput(j).shtChid <> udt2.udtDetail(i).udtInput(j).shtChid Then Return False
                    If udt1.udtDetail(i).udtInput(j).shtChSelect <> udt2.udtDetail(i).udtInput(j).shtChSelect Then Return False
                    If udt1.udtDetail(i).udtInput(j).shtIoSelect <> udt2.udtDetail(i).udtInput(j).shtIoSelect Then Return False
                    If udt1.udtDetail(i).udtInput(j).bytStatus <> udt2.udtDetail(i).udtInput(j).bytStatus Then Return False
                    If udt1.udtDetail(i).udtInput(j).bytType <> udt2.udtDetail(i).udtInput(j).bytType Then Return False
                    If udt1.udtDetail(i).udtInput(j).shtMask <> udt2.udtDetail(i).udtInput(j).shtMask Then Return False
                    'Ver2.0.2.8 比較から外す
                    'If udt1.udtDetail(i).udtInput(j).shtAnalogType <> udt2.udtDetail(i).udtInput(j).shtAnalogType Then Return False
                Next

            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "設定値格納"

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) リニアライズテーブル構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet As gTypSetSeqSet)

        Try

            For i As Integer = LBound(udtSet.udtDetail) To UBound(udtSet.udtDetail)

                With udtSet.udtDetail(i)
                    'ID
                    .shtId = IIf(Trim(grdSEQ.Item(0, i).Value) = "", 0, Trim(grdSEQ.Item(0, i).Value))
                    'Logic No
                    .shtLogicType = CCUInt16(grdSEQ.Item(1, i).Value)
                    'Remarks
                    .strRemarks = grdSEQ.Item(3, i).Value
                    'Out CH
                    .shtOutChid = CCUInt16(grdSEQ.Item(12, i).Value)
                    'Out Adr
                    ' FU,SLOT,PIN
                    .bytFuno = gGetFuNo(grdSEQ.Item(13, i).Value, True)
                    .bytPort = IIf(CCbyte(Trim(grdSEQ.Item(14, i).Value)) = 0, gCstCodeChNotSetFuPortByte, CCbyte(Trim(grdSEQ.Item(14, i).Value)))
                    .bytPin = IIf(CCbyte(Trim(grdSEQ.Item(15, i).Value)) = 0, gCstCodeChNotSetFuPinByte, CCbyte(Trim(grdSEQ.Item(15, i).Value)))

                    'InputCH1～8
                    For j As Integer = LBound(.udtInput) To UBound(.udtInput)
                        With .udtInput(j)
                            .shtChid = CCUInt16(grdSEQ.Item(j + 4, i).Value)
                        End With
                    Next j
                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "設定値表示"

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) リニアライズテーブル構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal udtSet As gTypSetSeqSet)

        Try

            For i As Integer = LBound(udtSet.udtDetail) To UBound(udtSet.udtDetail)

                Call mSetDisplayOne(i, udtSet.udtDetail(i))

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mSetDisplayOne(ByVal intIndex As Integer, _
                               ByVal udtDetail As gTypSetSeqSetRec)

        Try
            'ID,LogicNo(非表示),LogicName,Remarks,InputCH1～8,OutCH,OutAdr(FU,SLOT,PIN)
            Dim strLoginName As String = ""
            Dim intIDX As Integer = -1
            Dim strFuAdr As String = ""

            With udtDetail
                'ID
                grdSEQ.Item(0, intIndex).Value = IIf(.shtId = 0, "", .shtId)
                'Loginc No
                grdSEQ.Item(1, intIndex).Value = .shtLogicType
                'Logic Name
                If .shtLogicType = 0 Then
                    strLoginName = ""
                Else
                    intIDX = prAryLogicNo.IndexOf(.shtLogicType.ToString)
                    If intIDX < 0 Then
                        strLoginName = ""
                    Else
                        strLoginName = prAryLogicName(intIDX)
                    End If
                End If
                grdSEQ.Item(2, intIndex).Value = strLoginName
                'Remarks
                grdSEQ.Item(3, intIndex).Value = .strRemarks
                'Out CH
                grdSEQ.Item(12, intIndex).Value = gConvZeroToNull(.shtOutChid, "0000")
                'Out Adr
                ' FU,SLOT,PIN
                grdSEQ.Item(13, intIndex).Value = fnBackFuAdr(.bytFuno)
                grdSEQ.Item(14, intIndex).Value = fnBackFuAdr(.bytPort)
                grdSEQ.Item(15, intIndex).Value = fnBackFuAdr(.bytPin)

                'InputCH1～8
                For j As Integer = LBound(.udtInput) To UBound(.udtInput)
                    With .udtInput(j)
                        grdSEQ.Item(j + 4, intIndex).Value = IIf(.shtChid = 0, "", Format(.shtChid, "0000"))
                    End With
                Next j

            End With


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
    Private Function fnBackFuAdr(pFuAdr As Integer) As String
        Dim strRet As String = ""
        Try
            '0xFF(255)以上ならば設定無しとして空白を戻す
            If gGet2Byte(pFuAdr) >= &HFF Then
                fnBackFuAdr = ""
                Exit Function
            End If

            strRet = pFuAdr.ToString
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
        Return strRet
    End Function

#End Region

#End Region

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッドを初期化する
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mInitialDataGrid()

        Try

            Dim i As Integer
            Dim cellStyle As New DataGridViewCellStyle

            'ID,LogicNo(非表示),LogicName,Remarks,InputCH1～8,OutCH,OutAdr(FU,SLOT,PIN)
            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "txtID" : Column1.ReadOnly = True
            Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "txtLogicNo" : Column2.Visible = False
            Dim Column3 As New DataGridViewTextBoxColumn : Column3.Name = "txtLogic" : Column3.ReadOnly = True
            Dim Column4 As New DataGridViewTextBoxColumn : Column4.Name = "txtRemarks"
            Dim Column5 As New DataGridViewTextBoxColumn : Column5.Name = "txtCh1"
            Dim Column6 As New DataGridViewTextBoxColumn : Column6.Name = "txtCh2"
            Dim Column7 As New DataGridViewTextBoxColumn : Column7.Name = "txtCh3"
            Dim Column8 As New DataGridViewTextBoxColumn : Column8.Name = "txtCh4"
            Dim Column9 As New DataGridViewTextBoxColumn : Column9.Name = "txtCh5"
            Dim Column10 As New DataGridViewTextBoxColumn : Column10.Name = "txtCh6"
            Dim Column11 As New DataGridViewTextBoxColumn : Column11.Name = "txtCh7"
            Dim Column12 As New DataGridViewTextBoxColumn : Column12.Name = "txtCh8"
            Dim Column13 As New DataGridViewTextBoxColumn : Column13.Name = "txtOutCH"
            Dim Column14 As New DataGridViewTextBoxColumn : Column14.Name = "txtOutFU"
            Dim Column15 As New DataGridViewTextBoxColumn : Column15.Name = "txtOutSlot"
            Dim Column16 As New DataGridViewTextBoxColumn : Column16.Name = "txtOutPin"

            With grdSEQ

                ''列
                .Columns.Clear()
                .Columns.Add(Column1) : .Columns.Add(Column2) : .Columns.Add(Column3)
                .Columns.Add(Column4) : .Columns.Add(Column5) : .Columns.Add(Column6)
                .Columns.Add(Column7) : .Columns.Add(Column8) : .Columns.Add(Column9)
                .Columns.Add(Column10) : .Columns.Add(Column11) : .Columns.Add(Column12)
                .Columns.Add(Column13) : .Columns.Add(Column14) : .Columns.Add(Column15)
                .Columns.Add(Column16)
                '.AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                    c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "ID" : .Columns(0).Width = 50
                .Columns(1).HeaderText = "LogicNo" : .Columns(1).Width = 40
                .Columns(2).HeaderText = "Logic" : .Columns(2).Width = 130
                .Columns(3).HeaderText = "Remarks" : .Columns(3).Width = 120
                .Columns(4).HeaderText = "Input CH1" : .Columns(4).Width = 70
                .Columns(5).HeaderText = "Input CH2" : .Columns(5).Width = 70
                .Columns(6).HeaderText = "Input CH3" : .Columns(6).Width = 70
                .Columns(7).HeaderText = "Input CH4" : .Columns(7).Width = 70
                .Columns(8).HeaderText = "Input CH5" : .Columns(8).Width = 70
                .Columns(9).HeaderText = "Input CH6" : .Columns(9).Width = 70
                .Columns(10).HeaderText = "Input CH7" : .Columns(10).Width = 70
                .Columns(11).HeaderText = "Input CH8" : .Columns(11).Width = 70
                .Columns(12).HeaderText = "Out CH" : .Columns(12).Width = 60
                .Columns(13).HeaderText = "Out FU" : .Columns(13).Width = 30
                .Columns(14).HeaderText = "Out SLOT" : .Columns(14).Width = 30
                .Columns(15).HeaderText = "Out PIN" : .Columns(15).Width = 40

                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング
                .ColumnHeadersHeight = 20

                ''行
                .RowCount = 1025
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする
                .RowHeadersVisible = False          '行ヘッダー非表示



                ''行ヘッダー
                For i = 1 To .RowCount
                    .Rows(i - 1).HeaderCell.Value = i.ToString
                Next
                .RowHeadersWidth = 80
                .RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing  ''行ヘッダー幅の変更不可

                ''偶数行の背景色を変える
                cellStyle.BackColor = gColorGridRowBack
                For i = 0 To .Rows.Count - 1
                    If i Mod 2 <> 0 Then
                        .Rows(i).DefaultCellStyle = cellStyle
                    End If
                Next

                ''ReadOnly色設定
                For i = 0 To .RowCount - 1
                    .Rows(i).Cells("txtID").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtLogic").Style.BackColor = gColorGridRowBackReadOnly
                Next i

                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                ''スクロールバー
                .ScrollBars = ScrollBars.Vertical

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdSEQ)

                ''選択モード
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                'Ver2.0.1.5 複数行選択可にするためコメントアウト
                '.MultiSelect = False

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#Region "NEST Clip"
    '特殊機能 選択した行を展開したものをクリップボードへコピー
    Private Sub btnNEST_Click(sender As System.Object, e As System.EventArgs) Handles btnNEST.Click
        Try
            Dim intRow As Integer = -1
            Dim strLast As String = ""

            '選択セルの行位置が0より小さい、もしくは最大行数より大きい場合は処理を抜ける
            If grdSEQ.CurrentCell.RowIndex < 0 Or _
               grdSEQ.CurrentCell.RowIndex > grdSEQ.RowCount - 1 Then Return

            intRow = grdSEQ.CurrentCell.RowIndex

            With gudt.SetSeqSet.udtDetail(intRow)
                'ﾛｼﾞｯｸ展開
                Dim intLogic As Integer = -1
                intLogic = gudt.SetSeqSet.udtDetail(intRow).shtLogicType

                Dim strLogic As String = gGetComboItemName(intLogic, gEnmComboType.ctSeqSetDetailLogic)
                strLast = strLogic & "("

                'CHID展開
                For i As Integer = 0 To UBound(.udtInput) Step 1
                    If .udtInput(i).shtChid > 10000 Then
                        '10000以上なら再展開
                        Call subNEST(.udtInput(i).shtChid - 10001, strLast)
                    Else
                        If .udtInput(i).shtChid <= 0 Then
                            '0以下なら空白で次へ
                        Else
                            '1～9999ならCHID
                            strLast = strLast & .udtInput(i).shtChid & ","
                        End If
                    End If
                Next i
                strLast = strLast.TrimEnd(CType(",", Char))
                strLast = strLast & ")"
            End With

            Clipboard.SetText(strLast)

            MessageBox.Show("ClipBoard Copy OK!", "Finish", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception

        End Try
    End Sub
    'NEST展開関数(再帰的)
    Private Sub subNEST(pintRow As Integer, ByRef pstrLAST As String)
        Try
            With gudt.SetSeqSet.udtDetail(pintRow)
                'ﾛｼﾞｯｸ展開
                Dim intLogic As Integer = -1
                intLogic = gudt.SetSeqSet.udtDetail(pintRow).shtLogicType

                Dim strLogic As String = gGetComboItemName(intLogic, gEnmComboType.ctSeqSetDetailLogic)
                pstrLAST = pstrLAST & strLogic & "[" & (pintRow + 10001).ToString & "]" & "("

                'CHID展開
                For i As Integer = 0 To UBound(.udtInput) Step 1
                    If .udtInput(i).shtChid > 10000 Then
                        '10000以上なら再展開
                        Call subNEST(.udtInput(i).shtChid - 10001, pstrLAST)
                    Else
                        If .udtInput(i).shtChid <= 0 Then
                            '0以下なら空白で次へ
                        Else
                            '1～9999ならCHID
                            pstrLAST = pstrLAST & .udtInput(i).shtChid & ","
                        End If
                    End If
                Next i
                pstrLAST = pstrLAST.TrimEnd(CType(",", Char))
                pstrLAST = pstrLAST & "),"
            End With
        Catch ex As Exception

        End Try
    End Sub
#End Region

#End Region


End Class
