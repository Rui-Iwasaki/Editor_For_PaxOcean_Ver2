Public Class frmExtPnlLcdDutyDisplay

#Region "変数定義"

    Private mudtSetExtAlmNew As gTypSetExtCommon = Nothing    ''DutyName
    Private mudtSetExtAlmSepNew() As gTypSetExtRec = Nothing  ''Panel

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
    Private Sub frmExtPnlLcdDutyDisplay_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''グリッドを初期化する
            Call mInitialDataGrid()

            ''配列再定義
            mudtSetExtAlmNew.InitArray()
            ReDim mudtSetExtAlmSepNew(UBound(gudt.SetExtAlarm.udtExtAlarm))
            For i = 0 To UBound(mudtSetExtAlmSepNew)
                Call mudtSetExtAlmSepNew(i).InitArray()
            Next

            ''画面設定
            Call mSetDisplay(gudt.SetExtAlarm.udtExtAlarmCommon)
            Call mSetDisplay(gudt.SetExtAlarm.udtExtAlarm)

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
            Call mSetStructure(mudtSetExtAlmSepNew)

            ''データが変更されているかチェック
            If Not mChkStructureEquals() Then

                ''変更された場合は設定を更新する
                Call mCopyStructure(mudtSetExtAlmNew, gudt.SetExtAlarm.udtExtAlarmCommon)
                Call mCopyStructure(mudtSetExtAlmSepNew, gudt.SetExtAlarm.udtExtAlarm)

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
    Private Sub frmExtPnlLcdDutyDisplay_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try

            ''グリッドの保留中の変更を全て適用させる
            grdDutyName.EndEdit()
            grdPanel.EndEdit()

            ''設定値を比較用構造体に格納
            Call mSetStructure(mudtSetExtAlmNew)
            Call mSetStructure(mudtSetExtAlmSepNew)

            ''データが変更されているかチェック
            If Not mChkStructureEquals() Then

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
                        Call mCopyStructure(mudtSetExtAlmNew, gudt.SetExtAlarm.udtExtAlarmCommon)
                        Call mCopyStructure(mudtSetExtAlmSepNew, gudt.SetExtAlarm.udtExtAlarm)

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
    Private Sub frmExtPnlLcdDutyDisplay_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub



#Region "グリッドイベント"

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベントを発生させる
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdDutyName_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdDutyName.EditingControlShowing

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdDutyName_KeyPress

                ''イベントハンドラを追加する
                AddHandler tb.KeyPress, AddressOf grdDutyName_KeyPress

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
    Private Sub grdPanel_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdPanel.EditingControlShowing

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdPanel_KeyPress

                ''イベントハンドラを追加する
                AddHandler tb.KeyPress, AddressOf grdPanel_KeyPress

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
    Private Sub grdDutyName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdDutyName.KeyPress

        Try

            e.Handled = gCheckTextInput(3, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベント発生時処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdPanel_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdPanel.KeyPress

        Try

            e.Handled = gCheckTextInput(2, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 入力チェック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdPanel_CellValidating(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles grdPanel.CellValidating

        Try

            ''------------------------------------------------------------
            '' 入力チェックは mChkInput で実施するよう変更
            ''------------------------------------------------------------

            'e.Cancel = gChkTextNumSpan(0, 30, e.FormattedValue)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    'Panelの行が変わったらexampleも変える
    Private Sub grdPanel_CellEnter(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPanel.CellEnter
        Try
            Dim intRowValue As Integer = -1

            With grdPanel
                If e.RowIndex < 0 Then
                    Exit Sub
                End If

                'POS設定
                ' 1
                intRowValue = CInt(NZfZero(grdPanel(0, e.RowIndex).Value))
                If intRowValue <> 0 And intRowValue <= 30 Then
                    lblPos1.Text = NZfZero(grdDutyName(0, intRowValue - 1).Value)
                Else
                    lblPos1.Text = ""
                End If
                ' 2
                intRowValue = CInt(NZfZero(grdPanel(1, e.RowIndex).Value))
                If intRowValue <> 0 And intRowValue <= 30 Then
                    lblPos2.Text = NZfZero(grdDutyName(0, intRowValue - 1).Value)
                Else
                    lblPos2.Text = ""
                End If
                ' 3
                intRowValue = CInt(NZfZero(grdPanel(2, e.RowIndex).Value))
                If intRowValue <> 0 And intRowValue <= 30 Then
                    lblPos3.Text = NZfZero(grdDutyName(0, intRowValue - 1).Value)
                Else
                    lblPos3.Text = ""
                End If
                ' 4
                intRowValue = CInt(NZfZero(grdPanel(3, e.RowIndex).Value))
                If intRowValue <> 0 And intRowValue <= 30 Then
                    lblPos4.Text = NZfZero(grdDutyName(0, intRowValue - 1).Value)
                Else
                    lblPos4.Text = ""
                End If
                ' 5
                intRowValue = CInt(NZfZero(grdPanel(4, e.RowIndex).Value))
                If intRowValue <> 0 And intRowValue <= 30 Then
                    lblPos5.Text = NZfZero(grdDutyName(0, intRowValue - 1).Value)
                Else
                    lblPos5.Text = ""
                End If
                ' 6
                intRowValue = CInt(NZfZero(grdPanel(5, e.RowIndex).Value))
                If intRowValue <> 0 And intRowValue <= 30 Then
                    lblPos6.Text = NZfZero(grdDutyName(0, intRowValue - 1).Value)
                Else
                    lblPos6.Text = ""
                End If
                ' 7
                intRowValue = CInt(NZfZero(grdPanel(6, e.RowIndex).Value))
                If intRowValue <> 0 And intRowValue <= 30 Then
                    lblPos7.Text = NZfZero(grdDutyName(0, intRowValue - 1).Value)
                Else
                    lblPos7.Text = ""
                End If
                ' 8
                intRowValue = CInt(NZfZero(grdPanel(7, e.RowIndex).Value))
                If intRowValue <> 0 And intRowValue <= 30 Then
                    lblPos8.Text = NZfZero(grdDutyName(0, intRowValue - 1).Value)
                Else
                    lblPos8.Text = ""
                End If
            End With
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

            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "txtDutyName"
            Dim Column10 As New DataGridViewTextBoxColumn : Column10.Name = "txtPos1"
            Dim Column11 As New DataGridViewTextBoxColumn : Column11.Name = "txtPos2"
            Dim Column12 As New DataGridViewTextBoxColumn : Column12.Name = "txtPos3"
            Dim Column13 As New DataGridViewTextBoxColumn : Column13.Name = "txtPos4"
            Dim Column14 As New DataGridViewTextBoxColumn : Column14.Name = "txtPos5"
            Dim Column15 As New DataGridViewTextBoxColumn : Column15.Name = "txtPos6"
            Dim Column16 As New DataGridViewTextBoxColumn : Column16.Name = "txtPos7"
            Dim Column17 As New DataGridViewTextBoxColumn : Column17.Name = "txtPos8"

            With grdDutyName

                .Columns.Clear()
                .Columns.Add(Column1)
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "Duty Name" : .Columns(0).Width = 95
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 31
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする

                ''行ヘッダー
                For i = 1 To .RowCount
                    .Rows(i - 1).HeaderCell.Value = i.ToString
                Next
                .RowHeadersWidth = 50
                .RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ''偶数行の背景色を変える
                cellStyle.BackColor = gColorGridRowBack
                For i = 0 To .Rows.Count - 1
                    If i Mod 2 <> 0 Then
                        .Rows(i).DefaultCellStyle = cellStyle
                    End If
                Next

                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                ''スクロールバー
                .ScrollBars = ScrollBars.Vertical

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdDutyName)

            End With

            With grdPanel

                .Columns.Clear()
                .Columns.Add(Column10) : .Columns.Add(Column11) : .Columns.Add(Column12)
                .Columns.Add(Column13) : .Columns.Add(Column14) : .Columns.Add(Column15)
                .Columns.Add(Column16) : .Columns.Add(Column17)
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                Column10.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                Column11.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                Column12.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                Column13.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                Column14.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                Column15.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                Column16.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                Column17.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .TopLeftHeaderCell.Value = "Panel No."
                For i = 1 To .ColumnCount
                    .Columns(i - 1).HeaderText = "Pos" & i.ToString
                    .Columns(i - 1).Width = 60
                Next
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 21
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする

                ''行ヘッダー
                For i = 1 To .RowCount
                    .Rows(i - 1).HeaderCell.Value = i.ToString
                Next
                .RowHeadersWidth = 96
                .RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ''偶数行の背景色を変える
                cellStyle.BackColor = gColorGridRowBack
                For i = 0 To .Rows.Count - 1
                    If i Mod 2 <> 0 Then
                        .Rows(i).DefaultCellStyle = cellStyle
                    End If
                Next

                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                ''スクロールバー
                .ScrollBars = ScrollBars.Vertical

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdPanel)

            End With

            'exsampleも初期化
            lblPos1.Text = ""
            lblPos2.Text = ""
            lblPos3.Text = ""
            lblPos4.Text = ""
            lblPos5.Text = ""
            lblPos6.Text = ""
            lblPos7.Text = ""
            lblPos8.Text = ""
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能      : 入力チェック
    ' 返り値    : True:入力OK、False:入力NG
    ' 引き数    : なし
    ' 機能説明  : 入力チェックを行う
    '----------------------------------------------------------------------------
    Private Function mChkInput() As Boolean

        Try

            ''グリッドの保留中の変更を全て適用させる
            Call grdPanel.EndEdit()

            With grdPanel

                For i = 0 To .RowCount - 1

                    If Not gChkInputNum(.Rows(i).Cells("txtPos1"), 0, 30, "Pos1", i + 1, True, True) Then Return False
                    If Not gChkInputNum(.Rows(i).Cells("txtPos2"), 0, 30, "Pos2", i + 1, True, True) Then Return False
                    If Not gChkInputNum(.Rows(i).Cells("txtPos3"), 0, 30, "Pos3", i + 1, True, True) Then Return False
                    If Not gChkInputNum(.Rows(i).Cells("txtPos4"), 0, 30, "Pos4", i + 1, True, True) Then Return False
                    If Not gChkInputNum(.Rows(i).Cells("txtPos5"), 0, 30, "Pos5", i + 1, True, True) Then Return False
                    If Not gChkInputNum(.Rows(i).Cells("txtPos6"), 0, 30, "Pos6", i + 1, True, True) Then Return False
                    If Not gChkInputNum(.Rows(i).Cells("txtPos7"), 0, 30, "Pos7", i + 1, True, True) Then Return False
                    If Not gChkInputNum(.Rows(i).Cells("txtPos8"), 0, 30, "Pos8", i + 1, True, True) Then Return False

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
    ' 引き数    : ARG1 - ( O) 延長警報盤構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet As gTypSetExtCommon)

        Try

            For i As Integer = 0 To UBound(udtSet.udtExtDuty)

                ''Duty
                udtSet.udtExtDuty(i).strDutyName = Trim(grdDutyName(0, i).Value)

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mSetStructure(ByRef udtSet() As gTypSetExtRec)

        Try

            For i As Integer = 0 To UBound(udtSet)

                For j As Integer = 0 To UBound(udtSet(i).shtPosition)

                    ''Panel
                    udtSet(i).shtPosition(j) = CCShort(grdPanel(j, i).Value)

                Next

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 延長警報盤構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal udtSet As gTypSetExtCommon)

        Try

            For i As Integer = 0 To UBound(udtSet.udtExtDuty)

                ''Duty
                grdDutyName(0, i).Value = gGetString(udtSet.udtExtDuty(i).strDutyName)

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mSetDisplay(ByVal udtSet() As gTypSetExtRec)

        Try

            For i As Integer = 0 To UBound(udtSet)

                For j As Integer = 0 To UBound(udtSet(i).shtPosition)

                    ''Panel
                    grdPanel(j, i).Value = udtSet(i).shtPosition(j).ToString

                Next

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
    Private Sub mCopyStructure(ByVal udtSource As gTypSetExtCommon, _
                               ByRef udtTarget As gTypSetExtCommon)

        Try

            For i As Integer = 0 To UBound(udtSource.udtExtDuty)

                ''Duty
                udtTarget.udtExtDuty(i) = udtSource.udtExtDuty(i)

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mCopyStructure(ByVal udtSource() As gTypSetExtRec, _
                               ByRef udtTarget() As gTypSetExtRec)

        Try

            For intRow As Integer = 0 To UBound(udtSource)

                For intCol As Integer = 0 To UBound(udtSource(intRow).shtPosition)

                    ''Panel
                    udtTarget(intRow).shtPosition(intCol) = udtSource(intRow).shtPosition(intCol)

                Next

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 構造体比較
    ' 返り値    : True:相違なし、False:相違あり
    ' 引き数    : なし
    ' 機能説明  : チェック関数（共通）
    '--------------------------------------------------------------------
    Private Function mChkStructureEquals() As Boolean

        Try

            ''Duty
            If Not mChkStructureEquals(gudt.SetExtAlarm.udtExtAlarmCommon, mudtSetExtAlmNew) Then
                Return False
            End If

            ''Panel
            For i As Integer = 0 To UBound(gudt.SetExtAlarm.udtExtAlarm)
                If Not mChkStructureEquals(gudt.SetExtAlarm.udtExtAlarm(i), mudtSetExtAlmSepNew(i)) Then
                    Return False
                End If
            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

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
    Private Function mChkStructureEquals(ByVal udt1 As gTypSetExtCommon, _
                                         ByVal udt2 As gTypSetExtCommon) As Boolean

        Try

            For i As Integer = 0 To UBound(udt1.udtExtDuty)

                ''Duty
                If Not gCompareString(udt1.udtExtDuty(i).strDutyName, udt2.udtExtDuty(i).strDutyName) Then Return False

            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return False
        End Try

    End Function

    Private Function mChkStructureEquals(ByVal udt1 As gTypSetExtRec, _
                                         ByVal udt2 As gTypSetExtRec) As Boolean

        Try

            For i As Integer = 0 To UBound(udt1.shtPosition)

                ''Panel
                If udt1.shtPosition(i) <> udt2.shtPosition(i) Then Return False

            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return False
        End Try

    End Function

#End Region


End Class
