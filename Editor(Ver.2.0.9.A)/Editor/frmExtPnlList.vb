Public Class frmExtPnlList

#Region "変数定義"

    Private mudtSetExtAlmNew As gTypSetExtAlarm = Nothing
    Private mudtSetExtAlmSepNew As gTypSetExtRec = Nothing

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 本画面を表示する
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Sub gShow(ByRef frmOwner As Form)

        Try

            ''本画面表示
            Call gShowFormModelessForCloseWait2(Me, frmOwner)

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
    Private Sub frmExtPnlList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''構造体配列初期化
            Call mudtSetExtAlmNew.InitArray()
            Call mudtSetExtAlmNew.udtExtAlarmCommon.InitArray()
            For i As Integer = 0 To UBound(mudtSetExtAlmNew.udtExtAlarm)
                Call mudtSetExtAlmNew.udtExtAlarm(i).InitArray()
            Next

            ''コンボボックス 初期設定
            Call gSetComboBox(cmbWH, gEnmComboType.ctExtPnlListWh)
            Call gSetComboBox(cmbPR, gEnmComboType.ctExtPnlListPr)
            Call gSetComboBox(cmbCE, gEnmComboType.ctExtPnlListCe)
            Call gSetComboBox(cmbEngineerCall, gEnmComboType.ctExtPnlListEngineerCall)

            ''グリッド 初期設定
            Call mInitialDataGrid()

            ''構造体コピー
            Call mCopyStructure(gudt.SetExtAlarm, mudtSetExtAlmNew)

            ''画面設定
            Call mSetDisplay(mudtSetExtAlmNew)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Saveボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 保存処理を行う
    '--------------------------------------------------------------------
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Try

            ''入力チェック
            If Not mChkInput() Then Return

            ''設定値を比較用構造体に格納
            Call mSetStructure(mudtSetExtAlmNew)

            ''データが変更されているかチェック
            If Not mChkStructureEquals(gudt.SetExtAlarm, mudtSetExtAlmNew) Then

                ''変更された場合は設定を更新する
                Call mCopyStructure(mudtSetExtAlmNew, gudt.SetExtAlarm)

                ''メッセージ表示
                Call MessageBox.Show("It saved.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                ''更新フラグ設定
                gblnUpdateAll = True
                gudt.SetEditorUpdateInfo.udtSave.bytExtAlarm = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytExtAlarm = 1

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

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

    '--------------------------------------------------------------------
    ' 機能      : Printボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 画面印刷を行う
    '--------------------------------------------------------------------
    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click

        Try

            Call gPrintScreen(True)

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
    Private Sub frmExtPnlList_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try

            ''設定値を比較用構造体に格納
            Call mSetStructure(mudtSetExtAlmNew)

            ''データが変更されているかチェック
            If Not mChkStructureEquals(gudt.SetExtAlarm, mudtSetExtAlmNew) Then

                ''変更されている場合はメッセージ表示
                Select Case MessageBox.Show("Setting has been changed." & vbNewLine & _
                                            "Do you save the changes?", Me.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                    Case Windows.Forms.DialogResult.Yes

                        ''入力チェック
                        If Not mChkInput() Then
                            e.Cancel = True
                            Return
                        End If

                        ''変更されている場合は設定を更新する
                        Call mCopyStructure(mudtSetExtAlmNew, gudt.SetExtAlarm)

                        ''更新フラグ設定
                        gblnUpdateAll = True
                        gudt.SetEditorUpdateInfo.udtSave.bytExtAlarm = 1
                        gudt.SetEditorUpdateInfo.udtCompile.bytExtAlarm = 1

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
    Private Sub frmExtPnlList_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub



#Region "グリッドイベント"

    '----------------------------------------------------------------------------
    ' 機能説明  ： LED Patternボタン クリック時処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdEXT_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdEXT.CellContentClick

        Try

            ''処理を抜ける条件
            If e.RowIndex < 0 Or e.RowIndex > grdEXT.RowCount - 1 Then Return ''行数が0より小さい、もしくは最大行数より大きい場合
            If e.ColumnIndex < 0 Or e.ColumnIndex > grdEXT.ColumnCount - 1 Then Return ''列数が0より小さい、もしくは最大列数より大きい場合
            If grdEXT.CurrentCell.OwningColumn.Name <> grdEXT.Columns(8).Name Then Return ''「LED Pattern」ボタンではない場合

            ''設定値を比較用構造体に格納
            Call mSetStructure(mudtSetExtAlmNew)

            ''詳細画面で[OK]を選択したら"Panel Set"画面の表示更新
            If frmExtPnlLED.gShow(mudtSetExtAlmNew.udtExtAlarm(e.RowIndex), e.RowIndex, Me) = 0 Then
                Call mSetDisplay(mudtSetExtAlmNew)
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
    Private Sub grdEXT_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdEXT.EditingControlShowing

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdEXT_KeyPress

                ''イベントハンドラを追加する
                AddHandler tb.KeyPress, AddressOf grdEXT_KeyPress

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
    Private Sub grdEXT_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdEXT.KeyPress

        Try

            ''Accommodation
            If grdEXT.CurrentCell.OwningColumn.Name = "txtAccommodation" Then
                e.Handled = gCheckTextInput(16, sender, e.KeyChar, False)
            End If

            ''OPT
            If grdEXT.CurrentCell.OwningColumn.Name = "txtOPT" Then
                e.Handled = gCheckTextInput(3, sender, e.KeyChar)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 入力チェック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdEXT_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles grdEXT.CellValidating

        Try

            ''------------------------------------------------------------
            '' 入力チェックは mChkInput で実施するよう変更
            ''------------------------------------------------------------

            ''Dim dgv As DataGridView = CType(sender, DataGridView)

            ''If dgv.Columns(e.ColumnIndex).Name.Substring(0, 5) = "txtOP" Then

            ''    e.Cancel = gChkTextNumSpan(0, 255, e.FormattedValue)

            ''End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region



#End Region

#Region "内部関数"

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッドを初期化する
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mInitialDataGrid()

        Try

            Dim i As Integer
            Dim cellStyle As New DataGridViewCellStyle

            Dim Column1 As New DataGridViewComboBoxColumn : Column1.Name = "cmbNo"
            Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "txtAccommodation"
            Dim Column3 As New DataGridViewTextBoxColumn : Column3.Name = "txtDutyNo" : Column3.ReadOnly = True
            Dim Column4 As New DataGridViewComboBoxColumn : Column4.Name = "cmbReAlarm"
            Dim Column5 As New DataGridViewComboBoxColumn : Column5.Name = "cmbBzCut"
            Dim Column6 As New DataGridViewComboBoxColumn : Column6.Name = "cmbFree"
            Dim Column7 As New DataGridViewTextBoxColumn : Column7.Name = "txtOPT"
            Dim Column8 As New DataGridViewComboBoxColumn : Column8.Name = "cmbLedLcd"
            Dim Column9 As New DataGridViewButtonColumn : Column9.Name = "cmdLed"
            Dim Column10 As New DataGridViewCheckBoxColumn : Column10.Name = "chkMach"
            Dim Column11 As New DataGridViewCheckBoxColumn : Column11.Name = "chkCargo"

            Column2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            Column3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Column7.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            With grdEXT

                ''列
                .Columns.Clear()
                .Columns.Add(Column1) : .Columns.Add(Column2) : .Columns.Add(Column3)
                .Columns.Add(Column4) : .Columns.Add(Column5) : .Columns.Add(Column6)
                .Columns.Add(Column7) : .Columns.Add(Column8) : .Columns.Add(Column9)
                .Columns.Add(Column10) : .Columns.Add(Column11)
                .AllowUserToResizeColumns = False       ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing          ''列ヘッダー幅の変更不可

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "ID" : .Columns(0).Width = 80
                .Columns(1).HeaderText = "Accomodation" : .Columns(1).Width = 190
                .Columns(2).HeaderText = "Duty No." : .Columns(2).Width = 80
                .Columns(3).HeaderText = "RE ALM" : .Columns(3).Width = 80
                .Columns(4).HeaderText = "BZ Cut" : .Columns(4).Width = 80
                .Columns(5).HeaderText = "Free" : .Columns(5).Width = 80
                .Columns(6).HeaderText = "OPT" : .Columns(6).Width = 50
                .Columns(7).HeaderText = "LED/LCD" : .Columns(7).Width = 80
                .Columns(8).HeaderText = "" : .Columns(8).Width = 80
                .Columns(9).HeaderText = "Machinery" : .Columns(9).Width = 65
                .Columns(10).HeaderText = "Cargo" : .Columns(10).Width = 65
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter    ''列ヘッダー　センタリング

                ''行
                .RowCount = 20 + 1
                .AllowUserToAddRows = False             ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False          ''行の高さの変更不可
                .AllowUserToDeleteRows = False          ''行の削除を不可にする

                ''行ヘッダー
                For i = 1 To .RowCount
                    .Rows(i - 1).HeaderCell.Value = i.ToString
                Next
                .RowHeadersWidth = 50
                .RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                ''スクロールバー
                .ScrollBars = ScrollBars.None

                ''LED Patternボタン　初期値
                Column9.UseColumnTextForButtonValue = True
                Column9.Text = "LED Pattern"

                ''コンボボックス 初期設定
                Call gSetComboBox(Column1, gEnmComboType.ctExtPnlListNo)        ''No.
                Call gSetComboBox(Column4, gEnmComboType.ctExtPnlListReAlm)     ''RE ALM
                Call gSetComboBox(Column5, gEnmComboType.ctExtPnlListBzCut)     ''BZ Cut
                Call gSetComboBox(Column6, gEnmComboType.ctExtPnlListFree)      ''Free
                Call gSetComboBox(Column8, gEnmComboType.ctExtPnlListLedLcd)    ''LED/LCD

                ''偶数行の背景色を変える
                cellStyle.BackColor = gColorGridRowBack
                For i = 0 To .Rows.Count - 1
                    If i Mod 2 <> 0 Then
                        .Rows(i).DefaultCellStyle = cellStyle
                    End If
                Next

                ''ReadOnly色設定（DutyNo）
                For i = 0 To .RowCount - 1
                    .Rows(i).Cells(2).Style.BackColor = gColorGridRowBackReadOnly
                Next

                ''コンバイン時のみMach/Cargoの設定を可とする
                If frmExtMenu.optCombine.Checked Then

                    Column10.ReadOnly = False
                    Column11.ReadOnly = False

                Else

                    Column10.ReadOnly = True
                    Column11.ReadOnly = True

                    ''ReadOnly色設定
                    For i = 0 To .RowCount - 1
                        .Rows(i).Cells("chkMach").Style.BackColor = gColorGridRowBackReadOnly
                        .Rows(i).Cells("chkCargo").Style.BackColor = gColorGridRowBackReadOnly
                    Next

                End If

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdEXT)

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 入力チェック
    ' 返り値    : True:入力OK、False:入力NG
    ' 引き数    : なし
    ' 機能説明  : 入力チェックを行う
    '--------------------------------------------------------------------
    Private Function mChkInput() As Boolean

        Try

            Dim i As Integer
            Dim strValOpt As String = ""

            With grdEXT

                For i = 0 To .RowCount - 1

                    ''OPTレンジチェック
                    If Not gChkInputNum(.Rows(i).Cells("txtOPT"), 0, 255, "OPT", i + 1, True, True) Then Return False

                    ''入力項目確認
                    If gGetString(.Rows(i).Cells("txtOPT").Value) <> "" Then
                        ''OK

                    ElseIf gGetString(.Rows(i).Cells("txtOPT").Value) = "" Then
                        Call MessageBox.Show("Please set 'OPT' data of the line No." & i + 1, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False

                    End If

                Next

            End With

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) 延長警報盤設定構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet As gTypSetExtAlarm)

        Try

            With udtSet.udtExtAlarmCommon

                ''特殊(川汽)仕様 (W/H)
                .shtSpecialWh = CCShort(cmbWH.SelectedValue)

                ''特殊(川汽)仕様 (P/R)
                .shtSpecialPr = CCShort(cmbPR.SelectedValue)

                ''特殊(川汽)仕様 (C/E)
                .shtSpecialCe = CCShort(cmbCE.SelectedValue)

                ''Engineer Call設定
                .shtEngCall = CCShort(cmbEngineerCall.SelectedValue)


                '' Ver1.8.7 2015.12.10 特殊設定追加
                .Option1 = CCShort(txtComm1.Text)
                .Option2 = CCShort(txtComm2.Text)
                .Option3 = CCShort(txtComm3.Text)

            End With

            For i As Integer = 0 To UBound(udtSet.udtExtAlarm)

                ''延長警報盤使用有無
                udtSet.udtExtAlarmCommon.shtUse(i) = IIf(CCbyte(grdEXT.Rows(i).Cells(0).Value) = 0, 0, 1)

                With udtSet.udtExtAlarm(i)

                    ''通信ID番号
                    .shtNo = CCbyte(grdEXT.Rows(i).Cells(0).Value)

                    ''設置場所
                    .strPlace = gGetString(grdEXT.Rows(i).Cells(1).Value)

                    ''Re_Alarm設定有無
                    .shtReAlarm = CCShort(grdEXT.Rows(i).Cells(3).Value)

                    ''ブザーカット有無
                    .shtBuzzCut = CCShort(grdEXT.Rows(i).Cells(4).Value)

                    ''フリーエンジニア有無
                    .shtFreeEng = CCShort(grdEXT.Rows(i).Cells(5).Value)

                    ''オプション
                    .shtOption = CCShort(grdEXT.Rows(i).Cells(6).Value)

                    ''パネルタイプ
                    .shtPanel = CCShort(grdEXT.Rows(i).Cells(7).Value)

                    ''Machinery
                    .shtPart = IIf(grdEXT.Rows(i).Cells(9).Value, gBitSet(.shtPart, 0, True), gBitSet(.shtPart, 0, False))

                    ''Cargo
                    .shtPart = IIf(grdEXT.Rows(i).Cells(10).Value, gBitSet(.shtPart, 1, True), gBitSet(.shtPart, 1, False))

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 延長警報盤設定構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal udtSet As gTypSetExtAlarm)

        Try

            With udtSet.udtExtAlarmCommon

                ''特殊(川汽)仕様 (W/H)
                cmbWH.SelectedValue = .shtSpecialWh.ToString

                ''特殊(川汽)仕様 (P/R)
                cmbPR.SelectedValue = .shtSpecialPr.ToString

                ''特殊(川汽)仕様 (C/E)
                cmbCE.SelectedValue = .shtSpecialCe.ToString

                ''Engineer Call設定
                cmbEngineerCall.SelectedValue = .shtEngCall.ToString

                '' Ver1.8.7 2015.12.10 特殊設定追加
                txtComm1.Text = .Option1.ToString
                txtComm2.Text = .Option2.ToString
                txtComm3.Text = .Option3.ToString

            End With

            For i As Integer = 0 To UBound(udtSet.udtExtAlarm)

                With udtSet.udtExtAlarm(i)

                    ''通信ID番号
                    grdEXT.Rows(i).Cells(0).Value = .shtNo.ToString

                    ''設置場所
                    grdEXT.Rows(i).Cells(1).Value = gGetString(.strPlace)

                    ''Duty No.
                    grdEXT.Rows(i).Cells(2).Value = IIf(.shtDuty = 0, "NO", .shtDuty)

                    ''Re_Alarm設定有無
                    grdEXT.Rows(i).Cells(3).Value = .shtReAlarm.ToString

                    ''ブザーカット有無
                    grdEXT.Rows(i).Cells(4).Value = .shtBuzzCut.ToString

                    ''フリーエンジニア有無
                    grdEXT.Rows(i).Cells(5).Value = .shtFreeEng.ToString

                    ''オプション
                    grdEXT.Rows(i).Cells(6).Value = .shtOption

                    ''パネルタイプ
                    grdEXT.Rows(i).Cells(7).Value = .shtPanel.ToString

                    ''Machinery
                    grdEXT.Rows(i).Cells(9).Value = gBitCheck(.shtPart, 0)

                    ''Cargo
                    grdEXT.Rows(i).Cells(10).Value = gBitCheck(.shtPart, 1)

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

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
    Private Sub mCopyStructure(ByVal udtSource As gTypSetExtAlarm, _
                               ByRef udtTarget As gTypSetExtAlarm)

        Try

            Dim i As Integer

            udtTarget.udtExtAlarmCommon.shtSpecialWh = udtSource.udtExtAlarmCommon.shtSpecialWh     ''W/H
            udtTarget.udtExtAlarmCommon.shtSpecialPr = udtSource.udtExtAlarmCommon.shtSpecialPr     ''P/R
            udtTarget.udtExtAlarmCommon.shtSpecialCe = udtSource.udtExtAlarmCommon.shtSpecialCe     ''C/E
            udtTarget.udtExtAlarmCommon.shtEngCall = udtSource.udtExtAlarmCommon.shtEngCall         ''EngNo

            '' Ver1.8.7 2015.12.10 追加
            udtTarget.udtExtAlarmCommon.Option1 = udtSource.udtExtAlarmCommon.Option1     ''特殊設定1
            udtTarget.udtExtAlarmCommon.Option2 = udtSource.udtExtAlarmCommon.Option2     ''特殊設定2
            udtTarget.udtExtAlarmCommon.Option3 = udtSource.udtExtAlarmCommon.Option3     ''特殊設定3

            ''共通設定の延長警報盤使用有無　
            For i = 0 To UBound(udtSource.udtExtAlarmCommon.shtUse)
                udtTarget.udtExtAlarmCommon.shtUse(i) = udtSource.udtExtAlarmCommon.shtUse(i)
            Next

            For i = 0 To UBound(udtTarget.udtExtAlarm)
                udtTarget.udtExtAlarm(i).shtNo = udtSource.udtExtAlarm(i).shtNo                     ''パネル通信ID番号
                udtTarget.udtExtAlarm(i).strPlace = udtSource.udtExtAlarm(i).strPlace               ''設置場所
                udtTarget.udtExtAlarm(i).shtDuty = udtSource.udtExtAlarm(i).shtDuty                 ''Duty No.
                udtTarget.udtExtAlarm(i).shtReAlarm = udtSource.udtExtAlarm(i).shtReAlarm           ''Re_Alarm設定有無
                udtTarget.udtExtAlarm(i).shtBuzzCut = udtSource.udtExtAlarm(i).shtBuzzCut           ''ブザーカット有無
                udtTarget.udtExtAlarm(i).shtFreeEng = udtSource.udtExtAlarm(i).shtFreeEng           ''フリーエンジニア有無
                udtTarget.udtExtAlarm(i).shtOption = udtSource.udtExtAlarm(i).shtOption             ''オプション
                udtTarget.udtExtAlarm(i).shtPanel = udtSource.udtExtAlarm(i).shtPanel               ''パネルタイプ（LED/LCD）
                udtTarget.udtExtAlarm(i).shtPart = udtSource.udtExtAlarm(i).shtPart                 ''Mach/Cargo

                ''LED Pattern
                udtTarget.udtExtAlarm(i).shtEngNo = udtSource.udtExtAlarm(i).shtEngNo               ''Engineer CallNo設定
                udtTarget.udtExtAlarm(i).shtDuty = udtSource.udtExtAlarm(i).shtDuty                 ''Duty番号
                udtTarget.udtExtAlarm(i).shtDutyBuzz = udtSource.udtExtAlarm(i).shtDutyBuzz         ''Dutyブザーストップ動作設定
                udtTarget.udtExtAlarm(i).shtWatchLed = udtSource.udtExtAlarm(i).shtWatchLed         ''Watch LED表示方法選択
                udtTarget.udtExtAlarm(i).shtLedOut = udtSource.udtExtAlarm(i).shtLedOut             ''LED表示方法選択
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

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
    Private Function mChkStructureEquals(ByVal udt1 As gTypSetExtAlarm, _
                                         ByVal udt2 As gTypSetExtAlarm) As Boolean

        Try

            If udt1.udtExtAlarmCommon.shtSpecialWh <> udt2.udtExtAlarmCommon.shtSpecialWh Then Return False ''W/H
            If udt1.udtExtAlarmCommon.shtSpecialPr <> udt2.udtExtAlarmCommon.shtSpecialPr Then Return False ''P/R
            If udt1.udtExtAlarmCommon.shtSpecialCe <> udt2.udtExtAlarmCommon.shtSpecialCe Then Return False ''C/E
            If udt1.udtExtAlarmCommon.shtEngCall <> udt2.udtExtAlarmCommon.shtEngCall Then Return False ''EngNo

            '' Ver1.8.7 2015.12.10 追加
            If udt1.udtExtAlarmCommon.Option1 <> udt2.udtExtAlarmCommon.Option1 Then Return False ''特殊設定1
            If udt1.udtExtAlarmCommon.Option2 <> udt2.udtExtAlarmCommon.Option2 Then Return False ''特殊設定2
            If udt1.udtExtAlarmCommon.Option3 <> udt2.udtExtAlarmCommon.Option3 Then Return False ''特殊設定3

            For i As Integer = 0 To UBound(udt1.udtExtAlarm)
                If udt1.udtExtAlarm(i).shtNo <> udt2.udtExtAlarm(i).shtNo Then Return False ''パネル通信ID番号
                If Not gCompareString(udt1.udtExtAlarm(i).strPlace, udt2.udtExtAlarm(i).strPlace) Then Return False ''設置場所
                If udt1.udtExtAlarm(i).shtDuty <> udt2.udtExtAlarm(i).shtDuty Then Return False ''Duty No.
                If udt1.udtExtAlarm(i).shtReAlarm <> udt2.udtExtAlarm(i).shtReAlarm Then Return False ''Re_Alarm設定有無
                If udt1.udtExtAlarm(i).shtBuzzCut <> udt2.udtExtAlarm(i).shtBuzzCut Then Return False ''ブザーカット有無
                If udt1.udtExtAlarm(i).shtFreeEng <> udt2.udtExtAlarm(i).shtFreeEng Then Return False ''フリーエンジニア有無
                If udt1.udtExtAlarm(i).shtOption <> udt2.udtExtAlarm(i).shtOption Then Return False ''オプション
                If udt1.udtExtAlarm(i).shtPanel <> udt2.udtExtAlarm(i).shtPanel Then Return False ''パネルタイプ（LED/LCD）
                If udt1.udtExtAlarm(i).shtPart <> udt2.udtExtAlarm(i).shtPart Then Return False ''Mach/Cargo

                ''LED Pattern
                If udt1.udtExtAlarm(i).shtEngNo <> udt2.udtExtAlarm(i).shtEngNo Then Return False ''Eeengineer CallNo設定
                If udt1.udtExtAlarm(i).shtDuty <> udt2.udtExtAlarm(i).shtDuty Then Return False ''Duty番号
                If udt1.udtExtAlarm(i).shtDutyBuzz <> udt2.udtExtAlarm(i).shtDutyBuzz Then Return False ''Dutyブザーストップ動作設定
                If udt1.udtExtAlarm(i).shtWatchLed <> udt2.udtExtAlarm(i).shtWatchLed Then Return False ''Watch LED表示方法選択
                If udt1.udtExtAlarm(i).shtLedOut <> udt2.udtExtAlarm(i).shtLedOut Then Return False ''LED表示方法選択
            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return True
        End Try

    End Function

#End Region

End Class
