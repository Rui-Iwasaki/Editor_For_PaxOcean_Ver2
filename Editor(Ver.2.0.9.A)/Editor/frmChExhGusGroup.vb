Public Class frmChExhGusGroup

#Region "定数定義"

    ''排ガステーブルの配列数
    Private Const mCstExhRecCnt As Integer = 15

#End Region

#Region "変数定義"

    Private mblnInitFlg As Boolean
    Private mudtSetExhGusNew As gTypSetChExhGus = Nothing
    Private mintCylCnt() As Integer = Nothing
    Private mintDevCnt As Integer

    ''現在選択されているコンボのインデックスを保存
    Private mintNowSelectIndex As Integer

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : なし
    ' 引き数    : なし
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

    '--------------------------------------------------------------------
    ' 機能      : フォームロード
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 画面表示初期処理を行う
    '--------------------------------------------------------------------
    Private Sub frmChExhGusGroup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''初期化開始
            mblnInitFlg = True

            ''配列再定義
            ReDim mintCylCnt(mCstExhRecCnt)
            Call mudtSetExhGusNew.InitArray()
            For i As Integer = 0 To UBound(gudt.SetChExhGus.udtExhGusRec)
                Call mudtSetExhGusNew.udtExhGusRec(i).InitArray()
            Next

            ''コンボボックス初期設定
            Call gSetComboBox(cmbNo, gEnmComboType.ctChExhGusGroupcmbNo)
            cmbNo.SelectedIndex = 0

            ''現在選択されているコンボのインデックスを保存
            mintNowSelectIndex = cmbNo.SelectedIndex

            ''グリッド初期設定
            Call mInitialDataGrid()

            ''構造体のコピー
            Call mCopyStructure(gudt.SetChExhGus, mudtSetExhGusNew)

            ''画面設定
            Call mSetDisplay(cmbNo.SelectedIndex, mudtSetExhGusNew)

            ''初期化開始
            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub frmChGroupReposeDetail_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        Try

            ''画面表示時のセル選択を解除
            grdCylinderCH.CurrentCell = Nothing
            grdDevCH.CurrentCell = Nothing

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： TableNoコンボチェンジ
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmbNo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbNo.SelectedIndexChanged

        Try

            ''初期化中は何もしない
            If mblnInitFlg Then Return

            ''入力チェック
            If Not mChkInput() Then

                ''ここでの項目変更イベントは処理しない
                mblnInitFlg = True

                ''入力NGの場合はTableNoを元に戻す
                cmbNo.SelectedIndex = mintNowSelectIndex

                ''元に戻す
                mblnInitFlg = False

            Else

                ''現在のTableNoに設定されている値を保存
                Call mSetStructure(mintNowSelectIndex, mudtSetExhGusNew)

                ''選択されたTableNoの情報を表示
                Call mSetDisplay(cmbNo.SelectedIndex, mudtSetExhGusNew)

                ''現在のTableNoを更新
                mintNowSelectIndex = cmbNo.SelectedIndex

            End If

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
            Call mSetStructure(cmbNo.SelectedIndex, mudtSetExhGusNew)

            ''データが変更されているかチェック
            If Not mChkStructureEquals(mudtSetExhGusNew, gudt.SetChExhGus) Then

                ''変更された場合は設定を更新する
                Call mCopyStructure(mudtSetExhGusNew, gudt.SetChExhGus)

                ''メッセージ表示
                Call MessageBox.Show("It saved.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                ''更新フラグ設定
                gblnUpdateAll = True
                gudt.SetEditorUpdateInfo.udtSave.bytExhGus = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytExhGus = 1

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
    ' 機能      : フォームクローズ中
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 設定が変更されている場合は確認メッセージを表示する
    '--------------------------------------------------------------------
    Private Sub frmChExhGusGroup_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try

            ''グリッドの保留中の変更を全て適用させる
            Call grdCylinderCH.EndEdit()
            Call grdDevCH.EndEdit()

            ''設定値を比較用構造体に格納
            Call mSetStructure(cmbNo.SelectedIndex, mudtSetExhGusNew)

            ''データが変更されているかチェック
            If Not mChkStructureEquals(mudtSetExhGusNew, gudt.SetChExhGus) Then

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
                        Call mCopyStructure(mudtSetExhGusNew, gudt.SetChExhGus)

                        ''更新フラグ設定
                        gblnUpdateAll = True
                        gudt.SetEditorUpdateInfo.udtSave.bytExhGus = 1
                        gudt.SetEditorUpdateInfo.udtCompile.bytExhGus = 1

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
    Private Sub frmChExhGusGroup_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub





#Region "KeyPressイベントの発生"

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベントを発生させる
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdCylinderCH_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdCylinderCH.EditingControlShowing

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdCylinderCH_KeyPress

                ''イベントハンドラを追加する
                AddHandler tb.KeyPress, AddressOf grdCylinderCH_KeyPress

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub grdDevCH_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdDevCH.EditingControlShowing

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdDevCH_KeyPress

                ''イベントハンドラを追加する
                AddHandler tb.KeyPress, AddressOf grdDevCH_KeyPress

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "入力制限"

    '----------------------------------------------------------------------------
    ' 機能説明  ： 平均値出力CH KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtAvgCH_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAvgCH.KeyPress

        Try

            e.Handled = gCheckTextInput(5, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： リポーズCH KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtReposeCH_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtReposeCH.KeyPress

        Try

            e.Handled = gCheckTextInput(5, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッド KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdCylinderCH_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdCylinderCH.KeyPress

        Try

            e.Handled = gCheckTextInput(5, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub grdDevCH_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdDevCH.KeyPress

        Try

            e.Handled = gCheckTextInput(5, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "入力チェック"

    '----------------------------------------------------------------------------
    ' 機能説明  ： 平均値出力CH 入力チェック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtAvgCH_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtAvgCH.Validating

        Try

            ''------------------------------------------------------------
            '' 入力チェックは mChkInput で実施するよう変更
            ''------------------------------------------------------------

            'e.Cancel = gChkTextNumSpan(0, 65535, sender.Text)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： リポーズCH 入力チェック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtReposeCH_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtReposeCH.Validating

        Try

            ''------------------------------------------------------------
            '' 入力チェックは mChkInput で実施するよう変更
            ''------------------------------------------------------------

            'e.Cancel = gChkTextNumSpan(0, 65535, sender.Text)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 入力チェック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdCylinderCH_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles grdCylinderCH.CellValidating

        Try

            ''------------------------------------------------------------
            '' 入力チェックは mChkInput で実施するよう変更
            ''------------------------------------------------------------

            'e.Cancel = gChkTextNumSpan(0, 65535, e.FormattedValue)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub grdDevCH_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles grdDevCH.CellValidating

        Try

            ''------------------------------------------------------------
            '' 入力チェックは mChkInput で実施するよう変更
            ''------------------------------------------------------------

            'e.Cancel = gChkTextNumSpan(0, 65535, e.FormattedValue)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "入力値フォーマット"

    '----------------------------------------------------------------------------
    ' 機能説明  ： 平均値出力CH フォーマット
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtAvgCH_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAvgCH.Validated

        Try

            If IsNumeric(txtAvgCH.Text) Then

                txtAvgCH.Text = Integer.Parse(txtAvgCH.Text).ToString("0000")

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： リポーズCH フォーマット
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtReposeCH_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtReposeCH.Validated

        Try

            If IsNumeric(txtReposeCH.Text) Then

                txtReposeCH.Text = Integer.Parse(txtReposeCH.Text).ToString("0000")

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 入力値をフォーマットする
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdCylinderCH_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCylinderCH.CellValidated

        Try

            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Return

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If IsNumeric(grdCylinderCH.Rows(e.RowIndex).Cells(0).Value) Then
                grdCylinderCH.Rows(e.RowIndex).Cells(0).Value() = Integer.Parse(grdCylinderCH.Rows(e.RowIndex).Cells(0).Value).ToString("0000")
            End If

            ''シリンダ本数 更新
            ''Call mCalcCylCnt(mintNowSelectIndex)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 入力値をフォーマットする
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdDevCH_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdDevCH.CellValidated

        Try

            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Return

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If IsNumeric(grdDevCH.Rows(e.RowIndex).Cells(0).Value) Then
                grdDevCH.Rows(e.RowIndex).Cells(0).Value() = Integer.Parse(grdDevCH.Rows(e.RowIndex).Cells(0).Value).ToString("0000")
            End If

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

            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "txtChNo1"
            Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "txtChNo2"

            Column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Column2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            With grdCylinderCH

                ''列
                .Columns.Clear()
                .Columns.Add(Column1)
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "CH No."
                .Columns(0).Width = 80
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 24 + 1
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする

                ''行ヘッダー
                For i = 1 To .RowCount
                    .Rows(i - 1).HeaderCell.Value = i.ToString
                Next
                .RowHeadersWidth = 44
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
                Call gSetGridCopyAndPaste(grdCylinderCH)

            End With

            With grdDevCH

                ''列
                .Columns.Clear()
                .Columns.Add(Column2)
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "CH No."
                .Columns(0).Width = 80
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 24 + 1
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする

                ''行ヘッダー
                For i = 1 To .RowCount
                    .Rows(i - 1).HeaderCell.Value = i.ToString
                Next
                .RowHeadersWidth = 44
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
                Call gSetGridCopyAndPaste(grdDevCH)

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

            Dim i As Integer, j As Integer

            ''グリッドの保留中の変更を全て適用させる
            Call grdCylinderCH.EndEdit()
            Call grdDevCH.EndEdit()

            ''-----------------------------
            '' レンジチェック
            ''-----------------------------
            If Not gChkInputNum(txtAvgCH, 0, 65535, "Avg CH", True, True) Then Return False ''平均値出力CH
            If Not gChkInputNum(txtReposeCH, 0, 65535, "Repose CH", True, True) Then Return False ''リポーズCH

            ''grdCyl
            With grdCylinderCH
                For i = 0 To .RowCount - 1
                    If Not gChkInputNum(.Rows(i).Cells("txtChNo1"), 0, 65535, "Cylinder CH No.", i + 1, True, True) Then Return False
                Next
            End With

            ''grdDev
            With grdDevCH
                For i = 0 To .RowCount - 1
                    If Not gChkInputNum(.Rows(i).Cells("txtChNo2"), 0, 65535, "Dev CH No.", i + 1, True, True) Then Return False
                Next
            End With

            ''-----------------------------
            '' 重複登録チェック
            ''-----------------------------
            '' [Cylinder CH No.] 
            For i = 0 To grdCylinderCH.RowCount - 1

                For j = i + 1 To grdCylinderCH.RowCount - 1

                    If gGetString(grdCylinderCH(0, i).Value) <> "" Then

                        If gGetString(grdCylinderCH(0, i).Value) = gGetString(grdCylinderCH(0, j).Value) Then

                            Call MessageBox.Show("The same name as [Cylinder CH No.] cannot be set of CH No [" & grdCylinderCH(0, i).Value & "] and CH No [" & grdCylinderCH(0, j).Value & "].", _
                                                 "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return False

                        End If

                    End If

                Next j

            Next i

            '' [Dev CH No.]
            For i = 0 To grdDevCH.RowCount - 1

                For j = i + 1 To grdCylinderCH.RowCount - 1

                    If gGetString(grdDevCH(0, i).Value) <> "" Then

                        If gGetString(grdDevCH(0, i).Value) = gGetString(grdDevCH(0, j).Value) Then

                            Call MessageBox.Show("The same name as [Dev CH No.] cannot be set of CH No [" & grdDevCH(0, i).Value & "] and CH No [" & grdDevCH(0, j).Value & "].", _
                                                 "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return False

                        End If

                    End If

                Next j

            Next i


            'Ver2.0.3.1
            'DevCHとシリンダーCH数が不一致の場合確認
            Dim intSiriCHcount As Integer = 0
            Dim intDevCHcount As Integer = 0
            'シリンダ CHの件数取得
            For i = 0 To grdCylinderCH.RowCount - 1
                If NZf(grdCylinderCH(0, i).Value) <> "" Then
                    intSiriCHcount = intSiriCHcount + 1
                End If
            Next i
            'Dev CHの件数取得
            For i = 0 To grdDevCH.RowCount - 1
                If NZf(grdDevCH(0, i).Value) <> "" Then
                    intDevCHcount = intDevCHcount + 1
                End If
            Next i
            'DEV 件数がゼロなら正常で処理抜け
            If intDevCHcount <= 0 Then
                Return True
            End If
            'シリンダCH数とDEVCH数が不一致の場合確認
            If intDevCHcount <> intSiriCHcount Then
                If vbCancel = MessageBox.Show("Cylinder CH count not equal Dev CH count. Are you OK?", "Warning" _
                                , MessageBoxButtons.OKCancel, MessageBoxIcon.Question) Then
                    'ｷｬﾝｾﾙなら異常終了
                    Return False
                End If
            End If



            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 現在選択されているコンボのインデックス
    '           : ARG2 - ( O) 排ガス演算処理設定構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByVal intTableIndex As Integer, _
                              ByRef udtSet As gTypSetChExhGus)

        Try

            ''[シリンダ本数のカウント方法]
            ''最上位設定のチャンネル=シリンダ本数とします。（[既設] OPS設定-排ガス：シリンダ数カウント方法と合わせる）

            With udtSet.udtExhGusRec(intTableIndex)

                ''シリンダ本数 計算
                Call mCalcCylCnt(intTableIndex)

                ''シリンダ本数
                .shtNum = mintCylCnt(intTableIndex)

                ''平均値出力CH
                .shtAveChid = CCUInt16(txtAvgCH.Text)

                ''リポーズCH
                .shtRepChid = CCUInt16(txtReposeCH.Text)

                ''---------------------
                '' 詳細設定
                ''---------------------
                For i As Integer = 0 To UBound(.udtExhGusCyl)

                    ''シリンダＣＨ
                    .udtExhGusCyl(i).shtChid = CCUInt16(grdCylinderCH.Rows(i).Cells("txtChNo1").Value)

                    ''偏差ＣＨ
                    .udtExhGusDev(i).shtChid = CCUInt16(grdDevCH.Rows(i).Cells("txtChNo2").Value)

                Next

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 現在選択されているコンボのインデックス
    '           : ARG2 - (I ) 排ガス演算処理設定構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal intTableIndex As Integer, _
                            ByVal udtSet As gTypSetChExhGus)

        Try

            With udtSet.udtExhGusRec(intTableIndex)

                ''平均値出力CH
                txtAvgCH.Text = gConvZeroToNull(.shtAveChid, "0000")

                ''リポーズCH
                txtReposeCH.Text = gConvZeroToNull(.shtRepChid, "0000")

                ''CylCntの設定
                mintCylCnt(intTableIndex) = .shtNum

                ''---------------------
                '' 詳細設定
                ''---------------------
                For i As Integer = 0 To UBound(.udtExhGusCyl)

                    ''シリンダＣＨ
                    grdCylinderCH.Rows(i).Cells(0).Value = gConvZeroToNull(.udtExhGusCyl(i).shtChid, "0000")

                    ''偏差ＣＨ
                    grdDevCH.Rows(i).Cells(0).Value = gConvZeroToNull(.udtExhGusDev(i).shtChid, "0000")

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
    ' 　　　    : ARG2 - ( O) 複製先
    ' 機能説明  : 構造体を複製する
    ' 備考　　  : 構造体メンバの中に構造体配列がいると単純に = では複製できないため関数を用意
    ' 　　　　  : ↑ = でやると配列部分が参照渡しになり（？）値更新時に両方更新されてしまう
    ' 　　　　  : 構造体メンバの中に構造体配列がいない場合は、この関数を使わずに = で処理しても良い
    '--------------------------------------------------------------------
    Private Sub mCopyStructure(ByVal udtSource As gTypSetChExhGus, _
                               ByRef udtTarget As gTypSetChExhGus)

        Try

            For intRec As Integer = 0 To UBound(udtTarget.udtExhGusRec)

                ''★シリンダカウント用
                mintCylCnt(intRec) = udtSource.udtExhGusRec(intRec).shtNum

                ''シリンダ本数
                udtTarget.udtExhGusRec(intRec).shtNum = udtSource.udtExhGusRec(intRec).shtNum

                ''平均値出力CH  [SYSTEM_NO]
                udtTarget.udtExhGusRec(intRec).shtAveSysno = udtSource.udtExhGusRec(intRec).shtAveSysno

                ''平均値出力CH  [CH_ID]
                udtTarget.udtExhGusRec(intRec).shtAveChid = udtSource.udtExhGusRec(intRec).shtAveChid

                ''リポーズCH    [SYSTEM_NO]
                udtTarget.udtExhGusRec(intRec).shtRepSysno = udtSource.udtExhGusRec(intRec).shtRepSysno

                ''リポーズCH    [CH_ID]
                udtTarget.udtExhGusRec(intRec).shtRepChid = udtSource.udtExhGusRec(intRec).shtRepChid

                ''---------------------
                '' 詳細設定
                ''---------------------
                For intCh As Integer = 0 To UBound(udtTarget.udtExhGusRec(intRec).udtExhGusCyl)

                    ''シリンダＣＨ
                    udtTarget.udtExhGusRec(intRec).udtExhGusCyl(intCh).shtSysno = _
                                                    udtSource.udtExhGusRec(intRec).udtExhGusCyl(intCh).shtSysno
                    udtTarget.udtExhGusRec(intRec).udtExhGusCyl(intCh).shtChid = _
                                                    udtSource.udtExhGusRec(intRec).udtExhGusCyl(intCh).shtChid

                    ''偏差ＣＨ
                    udtTarget.udtExhGusRec(intRec).udtExhGusDev(intCh).shtSysno = _
                                                    udtSource.udtExhGusRec(intRec).udtExhGusDev(intCh).shtSysno
                    udtTarget.udtExhGusRec(intRec).udtExhGusDev(intCh).shtChid = _
                                                    udtSource.udtExhGusRec(intRec).udtExhGusDev(intCh).shtChid

                Next

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 構造体比較
    ' 返り値    : True:相違なし、False:相違あり
    ' 引き数    : ARG1 - (I ) 構造体１
    ' 　　　    : ARG2 - (I ) 構造体２
    ' 機能説明  : 構造体の設定値を比較する
    ' 備考　　  : 構造体メンバの中に構造体配列がいると Equals メソッドで正しい結果が得られないため関数を用意
    ' 　　　　  : 構造体メンバの中に構造体配列がいない場合は、 Equals メソッドで処理しても良いが一応これを使うこと
    ' 　　　　  : String文字列の比較には gCompareString を使用すること（単純な = だとNULL文字の有り無しで結果が変わってしまう）
    '--------------------------------------------------------------------
    Private Function mChkStructureEquals(ByVal udt1 As gTypSetChExhGus, _
                                         ByVal udt2 As gTypSetChExhGus) As Boolean

        Try

            For intRec As Integer = 0 To UBound(udt1.udtExhGusRec)

                ''シリンダ本数
                If udt1.udtExhGusRec(intRec).shtNum <> udt2.udtExhGusRec(intRec).shtNum Then Return False

                ''平均値 [SYSTEM_NO]
                If udt1.udtExhGusRec(intRec).shtAveSysno <> udt2.udtExhGusRec(intRec).shtAveSysno Then Return False

                ''平均値 [CH_ID]
                If udt1.udtExhGusRec(intRec).shtAveChid <> udt2.udtExhGusRec(intRec).shtAveChid Then Return False

                ''リポーズ [SYSTEM_NO]
                If udt1.udtExhGusRec(intRec).shtRepSysno <> udt2.udtExhGusRec(intRec).shtRepSysno Then Return False

                ''リポーズ [CH_ID]
                If udt1.udtExhGusRec(intRec).shtRepChid <> udt2.udtExhGusRec(intRec).shtRepChid Then Return False

                ''---------------------
                '' 詳細設定
                ''---------------------
                For intCh As Integer = 0 To UBound(udt1.udtExhGusRec(intRec).udtExhGusCyl)

                    ''シリンダＣＨ
                    If udt1.udtExhGusRec(intRec).udtExhGusCyl(intCh).shtSysno <> _
                                                    udt2.udtExhGusRec(intRec).udtExhGusCyl(intCh).shtSysno Then Return False
                    If udt1.udtExhGusRec(intRec).udtExhGusCyl(intCh).shtChid <> _
                                                    udt2.udtExhGusRec(intRec).udtExhGusCyl(intCh).shtChid Then Return False

                    ''偏差ＣＨ
                    If udt1.udtExhGusRec(intRec).udtExhGusDev(intCh).shtSysno <> _
                                            udt2.udtExhGusRec(intRec).udtExhGusDev(intCh).shtSysno Then Return False
                    If udt1.udtExhGusRec(intRec).udtExhGusDev(intCh).shtChid <> _
                                            udt2.udtExhGusRec(intRec).udtExhGusDev(intCh).shtChid Then Return False

                Next

            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： シリンダ本数の計算
    ' 引数      ： ARG1 - (I ) 現在選択されているコンボのインデックス
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mCalcCylCnt(ByVal intTableIndex As Integer)

        Try

            ''初期化
            mintCylCnt(intTableIndex) = 0

            For i = grdCylinderCH.Rows.Count - 1 To 0 Step -1

                If Trim(grdCylinderCH.Rows(i).Cells(0).Value) <> "" Then

                    ''加算
                    mintCylCnt(intTableIndex) = i + 1
                    Exit For

                End If

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

End Class
