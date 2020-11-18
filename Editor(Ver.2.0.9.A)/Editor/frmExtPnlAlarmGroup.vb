Public Class frmExtPnlAlarmGroup

#Region "変数定義"

    Private mudtSetExtAlmNew As gTypSetExtCommon = Nothing

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
    Private Sub frmExtPnlAlarmGroup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''グリッドを初期化する
            Call mInitialDataGrid()

            ''配列再定義
            mudtSetExtAlmNew.InitArray()

            ''画面設定
            Call mSetDisplay(gudt.SetExtAlarm.udtExtAlarmCommon)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub frmExtPnlAlarmGroup_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        Try

            grdAlarmGroup.CurrentCell = Nothing
            grdMachCargo.CurrentCell = Nothing

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
            If Not mChkStructureEquals(gudt.SetExtAlarm.udtExtAlarmCommon, mudtSetExtAlmNew) Then

                ''変更された場合は設定を更新する
                Call mCopyStructure(mudtSetExtAlmNew, gudt.SetExtAlarm.udtExtAlarmCommon)

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
    Private Sub frmExtPnlAlarmGroup_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try

            ''グリッドの保留中の変更を全て適用させる
            grdMachCargo.EndEdit()
            grdAlarmGroup.EndEdit()

            ''設定値を比較用構造体に格納
            Call mSetStructure(mudtSetExtAlmNew)

            ''データが変更されているかチェック
            If Not mChkStructureEquals(gudt.SetExtAlarm.udtExtAlarmCommon, mudtSetExtAlmNew) Then

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
                        Call mCopyStructure(mudtSetExtAlmNew, gudt.SetExtAlarm.udtExtAlarmCommon)

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
    Private Sub frmExtPnlAlarmGroup_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

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

            Dim Column1 As New DataGridViewCheckBoxColumn : Column1.Name = "chk1"
            Dim Column2 As New DataGridViewCheckBoxColumn : Column2.Name = "chk2"
            Dim Column3 As New DataGridViewCheckBoxColumn : Column3.Name = "chk3"
            Dim Column4 As New DataGridViewCheckBoxColumn : Column4.Name = "chk4"
            Dim Column5 As New DataGridViewCheckBoxColumn : Column5.Name = "chk5"
            Dim Column6 As New DataGridViewCheckBoxColumn : Column6.Name = "chk6"
            Dim Column7 As New DataGridViewCheckBoxColumn : Column7.Name = "chk7"
            Dim Column8 As New DataGridViewCheckBoxColumn : Column8.Name = "chk8"
            Dim Column9 As New DataGridViewCheckBoxColumn : Column9.Name = "chk9"
            Dim Column10 As New DataGridViewCheckBoxColumn : Column10.Name = "chk10"
            Dim Column11 As New DataGridViewCheckBoxColumn : Column11.Name = "chk11"
            Dim Column12 As New DataGridViewCheckBoxColumn : Column12.Name = "chk12"
            Dim Column13 As New DataGridViewCheckBoxColumn : Column13.Name = "chk13"
            Dim Column14 As New DataGridViewCheckBoxColumn : Column14.Name = "chk14"
            Dim Column15 As New DataGridViewCheckBoxColumn : Column15.Name = "chk15"
            Dim Column16 As New DataGridViewCheckBoxColumn : Column16.Name = "chk16"
            Dim Column17 As New DataGridViewCheckBoxColumn : Column17.Name = "chk17"
            Dim Column18 As New DataGridViewCheckBoxColumn : Column18.Name = "chk18"
            Dim Column19 As New DataGridViewCheckBoxColumn : Column19.Name = "chk19"
            Dim Column20 As New DataGridViewCheckBoxColumn : Column20.Name = "chk20"
            Dim Column21 As New DataGridViewCheckBoxColumn : Column21.Name = "chk21"
            Dim Column22 As New DataGridViewCheckBoxColumn : Column22.Name = "chk22"
            Dim Column23 As New DataGridViewCheckBoxColumn : Column23.Name = "chk23"
            Dim Column24 As New DataGridViewCheckBoxColumn : Column24.Name = "chk24"

            Dim Column30 As New DataGridViewCheckBoxColumn : Column30.Name = "chk30"
            Dim Column31 As New DataGridViewCheckBoxColumn : Column31.Name = "chk31"

            With grdHead

                ''列
                .Columns.Clear()
                .Columns.Add(New DataGridViewCheckBoxColumn())
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''列ヘッダー
                .Columns(0).HeaderText = "Alarm Group No."
                .Columns(0).Width = 600
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 1
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可

                ''行ヘッダー
                .RowHeadersWidth = 70

                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                ''スクロールバー
                .ScrollBars = ScrollBars.None

            End With

            With grdAlarmGroup

                ''列
                .Columns.Clear()
                .Columns.Add(Column1) : .Columns.Add(Column2) : .Columns.Add(Column3)
                .Columns.Add(Column4) : .Columns.Add(Column5) : .Columns.Add(Column6)
                .Columns.Add(Column7) : .Columns.Add(Column8) : .Columns.Add(Column9)
                .Columns.Add(Column10) : .Columns.Add(Column11) : .Columns.Add(Column12)
                .Columns.Add(Column13) : .Columns.Add(Column14) : .Columns.Add(Column15)
                .Columns.Add(Column16) : .Columns.Add(Column17) : .Columns.Add(Column18)
                .Columns.Add(Column19) : .Columns.Add(Column20) : .Columns.Add(Column21)
                .Columns.Add(Column22) : .Columns.Add(Column23) : .Columns.Add(Column24)
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                For i = 1 To .ColumnCount
                    .Columns(i - 1).HeaderText = i.ToString
                    .Columns(i - 1).Width = 25
                Next
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 12 + 1
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする

                ''行ヘッダー
                For i = 1 To .RowCount
                    .Rows(i - 1).HeaderCell.Value = "LED" & i.ToString
                Next
                .RowHeadersWidth = 70

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
                .ScrollBars = ScrollBars.None

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdAlarmGroup)

            End With

            With grdMachCargo

                ''列
                .Columns.Clear()
                .Columns.Add(Column30) : .Columns.Add(Column31)
                .AllowUserToResizeColumns = False               ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "Machinery" : .Columns(0).Width = 65
                .Columns(1).HeaderText = "Cargo" : .Columns(1).Width = 65
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 12 + 1
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする

                ''行ヘッダー
                .RowHeadersVisible = False

                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                ''スクロールバー
                .ScrollBars = ScrollBars.None

                ''コンバイン設定の場合のみ設定可とする
                If frmExtMenu.optCombine.Checked Then

                    .Enabled = True

                    ''偶数行の背景色を変える
                    cellStyle.BackColor = gColorGridRowBack
                    For i = 0 To .Rows.Count - 1
                        If i Mod 2 <> 0 Then
                            .Rows(i).DefaultCellStyle = cellStyle
                        End If
                    Next

                Else

                    .Enabled = False

                    ''ReadOnly色設定
                    For i = 0 To .RowCount - 1
                        .Rows(i).Cells("chk30").Style.BackColor = gColorGridRowBackReadOnly
                        .Rows(i).Cells("chk31").Style.BackColor = gColorGridRowBackReadOnly
                    Next

                End If

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdMachCargo)

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

            ''この画面は入力チェックなし
            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) 延長警報盤共通設定構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet As gTypSetExtCommon)

        Try

            With udtSet

                For i As Integer = 0 To UBound(.intGroupType)

                    ''Alarm Group No
                    .intGroupType(i) = IIf(grdAlarmGroup.Rows(i).Cells("chk1").Value, gBitSet(.intGroupType(i), 0, True), gBitSet(.intGroupType(i), 0, False))
                    .intGroupType(i) = IIf(grdAlarmGroup.Rows(i).Cells("chk2").Value, gBitSet(.intGroupType(i), 1, True), gBitSet(.intGroupType(i), 1, False))
                    .intGroupType(i) = IIf(grdAlarmGroup.Rows(i).Cells("chk3").Value, gBitSet(.intGroupType(i), 2, True), gBitSet(.intGroupType(i), 2, False))
                    .intGroupType(i) = IIf(grdAlarmGroup.Rows(i).Cells("chk4").Value, gBitSet(.intGroupType(i), 3, True), gBitSet(.intGroupType(i), 3, False))
                    .intGroupType(i) = IIf(grdAlarmGroup.Rows(i).Cells("chk5").Value, gBitSet(.intGroupType(i), 4, True), gBitSet(.intGroupType(i), 4, False))
                    .intGroupType(i) = IIf(grdAlarmGroup.Rows(i).Cells("chk6").Value, gBitSet(.intGroupType(i), 5, True), gBitSet(.intGroupType(i), 5, False))
                    .intGroupType(i) = IIf(grdAlarmGroup.Rows(i).Cells("chk7").Value, gBitSet(.intGroupType(i), 6, True), gBitSet(.intGroupType(i), 6, False))
                    .intGroupType(i) = IIf(grdAlarmGroup.Rows(i).Cells("chk8").Value, gBitSet(.intGroupType(i), 7, True), gBitSet(.intGroupType(i), 7, False))
                    .intGroupType(i) = IIf(grdAlarmGroup.Rows(i).Cells("chk9").Value, gBitSet(.intGroupType(i), 8, True), gBitSet(.intGroupType(i), 8, False))
                    .intGroupType(i) = IIf(grdAlarmGroup.Rows(i).Cells("chk10").Value, gBitSet(.intGroupType(i), 9, True), gBitSet(.intGroupType(i), 9, False))
                    .intGroupType(i) = IIf(grdAlarmGroup.Rows(i).Cells("chk11").Value, gBitSet(.intGroupType(i), 10, True), gBitSet(.intGroupType(i), 10, False))
                    .intGroupType(i) = IIf(grdAlarmGroup.Rows(i).Cells("chk12").Value, gBitSet(.intGroupType(i), 11, True), gBitSet(.intGroupType(i), 11, False))
                    .intGroupType(i) = IIf(grdAlarmGroup.Rows(i).Cells("chk13").Value, gBitSet(.intGroupType(i), 12, True), gBitSet(.intGroupType(i), 12, False))
                    .intGroupType(i) = IIf(grdAlarmGroup.Rows(i).Cells("chk14").Value, gBitSet(.intGroupType(i), 13, True), gBitSet(.intGroupType(i), 13, False))
                    .intGroupType(i) = IIf(grdAlarmGroup.Rows(i).Cells("chk15").Value, gBitSet(.intGroupType(i), 14, True), gBitSet(.intGroupType(i), 14, False))
                    .intGroupType(i) = IIf(grdAlarmGroup.Rows(i).Cells("chk16").Value, gBitSet(.intGroupType(i), 15, True), gBitSet(.intGroupType(i), 15, False))
                    .intGroupType(i) = IIf(grdAlarmGroup.Rows(i).Cells("chk17").Value, gBitSet(.intGroupType(i), 16, True), gBitSet(.intGroupType(i), 16, False))
                    .intGroupType(i) = IIf(grdAlarmGroup.Rows(i).Cells("chk18").Value, gBitSet(.intGroupType(i), 17, True), gBitSet(.intGroupType(i), 17, False))
                    .intGroupType(i) = IIf(grdAlarmGroup.Rows(i).Cells("chk19").Value, gBitSet(.intGroupType(i), 18, True), gBitSet(.intGroupType(i), 18, False))
                    .intGroupType(i) = IIf(grdAlarmGroup.Rows(i).Cells("chk20").Value, gBitSet(.intGroupType(i), 19, True), gBitSet(.intGroupType(i), 19, False))
                    .intGroupType(i) = IIf(grdAlarmGroup.Rows(i).Cells("chk21").Value, gBitSet(.intGroupType(i), 20, True), gBitSet(.intGroupType(i), 20, False))
                    .intGroupType(i) = IIf(grdAlarmGroup.Rows(i).Cells("chk22").Value, gBitSet(.intGroupType(i), 21, True), gBitSet(.intGroupType(i), 21, False))
                    .intGroupType(i) = IIf(grdAlarmGroup.Rows(i).Cells("chk23").Value, gBitSet(.intGroupType(i), 22, True), gBitSet(.intGroupType(i), 22, False))
                    .intGroupType(i) = IIf(grdAlarmGroup.Rows(i).Cells("chk24").Value, gBitSet(.intGroupType(i), 23, True), gBitSet(.intGroupType(i), 23, False))

                    ''Mach/Cargo
                    .intGroupType(i) = IIf(grdMachCargo.Rows(i).Cells("chk30").Value, gBitSet(.intGroupType(i), 24, True), gBitSet(.intGroupType(i), 24, False))
                    .intGroupType(i) = IIf(grdMachCargo.Rows(i).Cells("chk31").Value, gBitSet(.intGroupType(i), 25, True), gBitSet(.intGroupType(i), 25, False))

                Next

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 延長警報盤共通設定構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal udtSet As gTypSetExtCommon)

        Try

            With udtSet

                For i As Integer = 0 To UBound(.intGroupType)

                    ''Alarm Group No
                    grdAlarmGroup.Rows(i).Cells("chk1").Value = IIf(gBitCheck(.intGroupType(i), 0), True, False)
                    grdAlarmGroup.Rows(i).Cells("chk2").Value = IIf(gBitCheck(.intGroupType(i), 1), True, False)
                    grdAlarmGroup.Rows(i).Cells("chk3").Value = IIf(gBitCheck(.intGroupType(i), 2), True, False)
                    grdAlarmGroup.Rows(i).Cells("chk4").Value = IIf(gBitCheck(.intGroupType(i), 3), True, False)
                    grdAlarmGroup.Rows(i).Cells("chk5").Value = IIf(gBitCheck(.intGroupType(i), 4), True, False)
                    grdAlarmGroup.Rows(i).Cells("chk6").Value = IIf(gBitCheck(.intGroupType(i), 5), True, False)
                    grdAlarmGroup.Rows(i).Cells("chk7").Value = IIf(gBitCheck(.intGroupType(i), 6), True, False)
                    grdAlarmGroup.Rows(i).Cells("chk8").Value = IIf(gBitCheck(.intGroupType(i), 7), True, False)
                    grdAlarmGroup.Rows(i).Cells("chk9").Value = IIf(gBitCheck(.intGroupType(i), 8), True, False)
                    grdAlarmGroup.Rows(i).Cells("chk10").Value = IIf(gBitCheck(.intGroupType(i), 9), True, False)
                    grdAlarmGroup.Rows(i).Cells("chk11").Value = IIf(gBitCheck(.intGroupType(i), 10), True, False)
                    grdAlarmGroup.Rows(i).Cells("chk12").Value = IIf(gBitCheck(.intGroupType(i), 11), True, False)
                    grdAlarmGroup.Rows(i).Cells("chk13").Value = IIf(gBitCheck(.intGroupType(i), 12), True, False)
                    grdAlarmGroup.Rows(i).Cells("chk14").Value = IIf(gBitCheck(.intGroupType(i), 13), True, False)
                    grdAlarmGroup.Rows(i).Cells("chk15").Value = IIf(gBitCheck(.intGroupType(i), 14), True, False)
                    grdAlarmGroup.Rows(i).Cells("chk16").Value = IIf(gBitCheck(.intGroupType(i), 15), True, False)
                    grdAlarmGroup.Rows(i).Cells("chk17").Value = IIf(gBitCheck(.intGroupType(i), 16), True, False)
                    grdAlarmGroup.Rows(i).Cells("chk18").Value = IIf(gBitCheck(.intGroupType(i), 17), True, False)
                    grdAlarmGroup.Rows(i).Cells("chk19").Value = IIf(gBitCheck(.intGroupType(i), 18), True, False)
                    grdAlarmGroup.Rows(i).Cells("chk20").Value = IIf(gBitCheck(.intGroupType(i), 19), True, False)
                    grdAlarmGroup.Rows(i).Cells("chk21").Value = IIf(gBitCheck(.intGroupType(i), 20), True, False)
                    grdAlarmGroup.Rows(i).Cells("chk22").Value = IIf(gBitCheck(.intGroupType(i), 21), True, False)
                    grdAlarmGroup.Rows(i).Cells("chk23").Value = IIf(gBitCheck(.intGroupType(i), 22), True, False)
                    grdAlarmGroup.Rows(i).Cells("chk24").Value = IIf(gBitCheck(.intGroupType(i), 23), True, False)

                    ''Mach/Cargo
                    grdMachCargo.Rows(i).Cells("chk30").Value = IIf(gBitCheck(.intGroupType(i), 24), True, False)
                    grdMachCargo.Rows(i).Cells("chk31").Value = IIf(gBitCheck(.intGroupType(i), 25), True, False)

                Next

            End With

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

            For i As Integer = 0 To UBound(udtTarget.intGroupType)

                udtTarget.intGroupType(i) = udtSource.intGroupType(i)

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
    Private Function mChkStructureEquals(ByVal udt1 As gTypSetExtCommon, _
                                         ByVal udt2 As gTypSetExtCommon) As Boolean

        Try

            For i As Integer = 0 To UBound(udt1.intGroupType)

                If udt1.intGroupType(i) <> udt2.intGroupType(i) Then Return False

            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return False
        End Try

    End Function

#End Region

End Class
