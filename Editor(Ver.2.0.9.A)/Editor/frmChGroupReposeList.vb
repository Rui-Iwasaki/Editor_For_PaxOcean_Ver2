Public Class frmChGroupReposeList

#Region "変数定義"

    Private mudtSetReposeNew() As gTypSetChGroupReposeRec = Nothing

    ''初期化フラグ
    Private mintInitFlg As Boolean

    ''ウィンドウを開いた時のチェックボックスの状態 2018.12.13 倉重
    Private g_bytPRE_GREPNUM As Byte

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
    Private Sub frmChGroupReposeList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''初期化フラグ
            mintInitFlg = True

            ''チェックボックスの初期状態を格納 2018.12.13 倉重
            g_bytPRE_GREPNUM = g_bytGREPNUM

            ''グリッドの初期化
            Call mInitialDataGrid()

            ''配列再定義
            ReDim mudtSetReposeNew(UBound(gudt.SetChGroupRepose.udtRepose))
            For i As Integer = 0 To UBound(gudt.SetChGroupRepose.udtRepose)
                Call mudtSetReposeNew(i).InitArray()
            Next

            ''チェックボックスの初期状態を決定 2018.12.13 倉重
            If g_bytGREPNUM = 1 Then
                Me.ChkAddgrep.Checked = True
            Else
                Me.ChkAddgrep.Checked = False
            End If

            ''構造体のコピー
            Call mCopyStructure(gudt.SetChGroupRepose.udtRepose, mudtSetReposeNew)

            ''画面設定
            Call mSetDisplay(mudtSetReposeNew)

            ''ボタンコントロール
            Call mSetControlEnable(grdGroupRepose.CurrentCell.RowIndex)

            ''初期化フラグ
            mintInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub frmChGroupReposeList_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        Try

            ''画面表示時のセル選択を解除（grdGroupRepose.CurrentCell = Nothing にするとTimerで落ちるので注意）
            grdGroupRepose.Rows(0).Cells(0).Selected = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Editボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし 
    '----------------------------------------------------------------------------
    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click

        Try

            ''処理を抜ける条件
            If grdGroupRepose.CurrentCell.RowIndex < 0 Or _
               grdGroupRepose.CurrentCell.RowIndex > grdGroupRepose.RowCount - 1 Then Return ''行数が0より小さい、もしくは最大行数より大きい場合

            ''カーソルのある行Indexの取得
            Dim intRowIndex = grdGroupRepose.CurrentCell.RowIndex

            ''Normal選択時は詳細画面を表示しない
            If CCInt(grdGroupRepose.Rows(intRowIndex).Cells("cmbType").Value) = gCstCodeChGroupReposeTypeNormal Then Exit Sub

            ''詳細画面の表示処理
            Call frmChGroupReposeDetail.gShow(mudtSetReposeNew(intRowIndex), intRowIndex, Me)

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
            Call mSetStructure(mudtSetReposeNew)

            ''データが変更されているかチェック
            If Not mChkStructureEquals(gudt.SetChGroupRepose.udtRepose, mudtSetReposeNew) Then

                ''変更された場合は設定を更新する
                Call mCopyStructure(mudtSetReposeNew, gudt.SetChGroupRepose.udtRepose)

                ''メッセージ表示
                Call MessageBox.Show("It saved.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                ''更新フラグ設定
                gblnUpdateAll = True
                gudt.SetEditorUpdateInfo.udtSave.bytRepose = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytRepose = 1

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
    Private Sub frmChGroupReposeList_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try

            ''グリッドの保留中の変更を全て適用させる
            Call grdGroupRepose.EndEdit()

            ''設定値を比較用構造体に格納
            Call mSetStructure(mudtSetReposeNew)

            ''データが変更されているかチェック
            If Not mChkStructureEquals(gudt.SetChGroupRepose.udtRepose, mudtSetReposeNew) Then

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
                        Call mCopyStructure(mudtSetReposeNew, gudt.SetChGroupRepose.udtRepose)

                        ''更新フラグ設定
                        gblnUpdateAll = True
                        gudt.SetEditorUpdateInfo.udtSave.bytRepose = 1
                        gudt.SetEditorUpdateInfo.udtCompile.bytRepose = 1

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
    Private Sub frmChGroupReposeList_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub



#Region "グリッドイベント"

    '----------------------------------------------------------------------------
    ' 機能説明  ： プルダウンリストの項目を変更した時の処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdGroupRepose_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdGroupRepose.CellValueChanged

        Try

            ''処理を抜ける条件
            If mintInitFlg Then Return ''初期化中の場合
            If e.RowIndex < 0 Or e.RowIndex > grdGroupRepose.RowCount - 1 Then Return ''行数が0より小さい、もしくは最大行数より大きい場合
            If e.ColumnIndex < 0 Or e.ColumnIndex > grdGroupRepose.ColumnCount - 1 Then Return ''列数が0より小さい、もしくは最大列数より大きい場合

            Dim dgv As DataGridView = CType(sender, DataGridView)

            ''列名がデータ種別コードの場合
            If grdGroupRepose.CurrentCell.OwningColumn.Name = "cmbType" Then

                Call mSetControlEnable(grdGroupRepose.CurrentCell.RowIndex)

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 行変更の感知（クリック、矢印キー）
    ' 引数      ： なし
    ' 戻値      ： なし 
    '----------------------------------------------------------------------------
    Private Sub grdGroupRepose_RowValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdGroupRepose.RowValidated

        Try

            ''処理を抜ける条件
            If mintInitFlg Then Return ''初期化中の場合

            ''Editボタンの使用可/不可設定
            Call Timer1.Start()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッドダブルクリック
    ' 引数      ： なし
    ' 戻値      ： なし 
    '----------------------------------------------------------------------------
    Private Sub grdGroupRepose_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdGroupRepose.CellDoubleClick

        Try

            ''処理を抜ける条件
            If e.RowIndex < 0 Or e.RowIndex > grdGroupRepose.RowCount - 1 Then Return ''行数が0より小さい、もしくは最大行数より大きい場合
            If gChkCellIsCmb(grdGroupRepose.CurrentCell.OwningColumn.Name) Then Return ''コンボボックス列の場合は処理を抜ける

            ''グリッドの保留中の変更を全て適用させる
            Call grdGroupRepose.EndEdit()

            ''Editボタンのクリックイベントを呼び出す
            Call cmdEdit_Click(cmdEdit, New EventArgs)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッド行ヘッダーダブルクリック
    ' 引数      ： なし
    ' 戻値      ： なし 
    '----------------------------------------------------------------------------
    Private Sub grdGroupRepose_RowHeaderMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdGroupRepose.RowHeaderMouseDoubleClick

        Try

            ''行数が0より小さい、もしくは最大行数より大きい場合処理を抜ける
            If e.RowIndex < 0 Or e.RowIndex > grdGroupRepose.RowCount - 1 Then Return

            ''グリッドの保留中の変更を全て適用させる
            grdGroupRepose.EndEdit()

            ''Editボタンのクリックイベントを呼び出す
            Call cmdEdit_Click(cmdEdit, New EventArgs)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベントの追加
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdGroupRepose_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdGroupRepose.EditingControlShowing

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdGroupRepose_KeyPress

                ''該当する列ならイベントハンドラを追加する
                If dgv.CurrentCell.OwningColumn.Name.Substring(0, 3) = "txt" Then

                    AddHandler tb.KeyPress, AddressOf grdGroupRepose_KeyPress

                End If

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
    Private Sub grdGroupRepose_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdGroupRepose.KeyPress

        Try

            Dim strColumnName As String

            ''選択セルの名称取得
            strColumnName = grdGroupRepose.CurrentCell.OwningColumn.Name

            ''[CH_NO.]
            If strColumnName = "txtChNo" Then
                e.Handled = gCheckTextInput(5, sender, e.KeyChar)
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
    Private Sub grdGroupRepose_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles grdGroupRepose.CellValidating

        Try

            ''------------------------------------------------------------
            '' 入力チェックは mChkInput で実施するよう変更
            ''------------------------------------------------------------

            ''Dim dgv As DataGridView = CType(sender, DataGridView)
            ''Dim strColumnName = dgv.Columns(e.ColumnIndex).Name

            '' ''[CH_NO.]
            ''If strColumnName = "txtChNo" Then
            ''    e.Cancel = gChkTextNumSpan(1, 65535, e.FormattedValue)
            ''End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 入力値をフォーマットする
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdGroupRepose_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdGroupRepose.CellValidated

        Try

            ''処理を抜ける条件
            If e.RowIndex < 0 Or e.RowIndex > grdGroupRepose.RowCount - 1 Then Return ''行数が0より小さい、もしくは最大行数より大きい場合
            If e.ColumnIndex < 0 Or e.ColumnIndex > grdGroupRepose.ColumnCount - 1 Then Return ''列数が0より小さい、もしくは最大列数より大きい場合

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If dgv.CurrentCell.OwningColumn.Name <> "txtChNo" Then Exit Sub

            If IsNumeric(grdGroupRepose.Rows(e.RowIndex).Cells("txtChNo").Value) Then

                grdGroupRepose.Rows(e.RowIndex).Cells("txtChNo").Value = Integer.Parse(grdGroupRepose.Rows(e.RowIndex).Cells("txtChNo").Value).ToString("0000")

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region



#End Region

#Region "内部関数"

    '--------------------------------------------------------------------
    ' 機能説明  ： グリッドを初期化する
    ' 引数      ： なし
    ' 戻値      ： なし 
    '--------------------------------------------------------------------
    Private Sub mInitialDataGrid()

        Try

            Dim i As Integer
            Dim cellStyle As New DataGridViewCellStyle

            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "txtChNo"
            Dim Column2 As New DataGridViewComboBoxColumn : Column2.Name = "cmbType" : Column2.Visible = False  'Ver2.0.1.8 タイプは非表示化
            Column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            With grdGroupRepose

                ''列
                .Columns.Clear()
                .Columns.Add(Column1)
                .Columns.Add(Column2)
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "CH No." : .Columns(0).Width = 96
                .Columns(1).HeaderText = "Type" : .Columns(1).Width = 120
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                ''チェックマークの有無で行数を変更する 2018.11.13 倉重
                If g_bytGREPNUM = 1 Then
                    .RowCount = 72 + 1
                Else
                    .RowCount = 48 + 1
                End If
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする

                ''行ヘッダー
                .RowHeadersWidth = 65
                .RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                For i = 1 To .RowCount
                    .Rows(i - 1).HeaderCell.Value = i.ToString
                Next

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

                ''コンボボックス初期設定
                Call gSetComboBox(Column2, gEnmComboType.ctChGroupReposeListColumn2)

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdGroupRepose)

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

            ''グリッドの保留中の変更を全て適用させる
            Call grdGroupRepose.EndEdit()

            ''入力値のレンジチェック
            If Not mCheckInputData() Then Return False

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    Private Function mCheckInputData() As Boolean

        Try

            For i = 0 To grdGroupRepose.RowCount - 1

                If Not gChkInputNum(grdGroupRepose.Rows(i).Cells("txtChNo"), 0, 65535, "CH No.", i + 1, True, True) Then Return False

            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) リポーズ入力設定構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet() As gTypSetChGroupReposeRec)

        Try

            For intRow As Integer = LBound(udtSet) To UBound(udtSet)

                With udtSet(intRow)

                    ''CH ID
                    .shtChId = CCUInt16(grdGroupRepose.Rows(intRow).Cells("txtChNo").Value)

                    ''データ種別コード
                    .shtData = CCShort(grdGroupRepose.Rows(intRow).Cells("cmbType").Value)

                End With

                ''チェックマークがついていないかつ47行目の格納時にFor文を抜ける 2018.12.13 倉重
                If Me.ChkAddgrep.Checked = False And intRow = 47 Then
                    Exit For
                End If

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) リポーズ入力設定構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal udtSet() As gTypSetChGroupReposeRec)

        Try

            For intRow As Integer = LBound(udtSet) To UBound(udtSet)

                With udtSet(intRow)

                    ''CH ID
                    grdGroupRepose.Rows(intRow).Cells("txtChNo").Value = gConvZeroToNull(.shtChId, "0000")

                    ''データ種別コード
                    grdGroupRepose.Rows(intRow).Cells("cmbType").Value = .shtData.ToString

                End With

                ''チェックマークがついていないかつ47行目の表示時にFor文を抜ける 2018.12.13 倉重
                If Me.ChkAddgrep.Checked = False And intRow = 47 Then
                    Exit For
                End If

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
    Private Sub mCopyStructure(ByVal udtSource() As gTypSetChGroupReposeRec, _
                               ByRef udtTarget() As gTypSetChGroupReposeRec)

        Try

            Dim intListRow As Integer
            Dim intListDetailRow As Integer

            ''チェックボックスの初期状態を格納 2018.12.13 倉重
            g_bytPRE_GREPNUM = g_bytGREPNUM

            For intListRow = LBound(udtTarget) To UBound(udtTarget)

                ''CH ID
                udtTarget(intListRow).shtChId = udtSource(intListRow).shtChId

                ''データ種別コード
                udtTarget(intListRow).shtData = udtSource(intListRow).shtData

                '---------------------------
                ' 詳細画面
                '---------------------------
                For intListDetailRow = 0 To UBound(udtTarget(intListRow).udtReposeInf)

                    ''CH ID
                    udtTarget(intListRow).udtReposeInf(intListDetailRow).shtChId = udtSource(intListRow).udtReposeInf(intListDetailRow).shtChId

                    ''マスク値
                    udtTarget(intListRow).udtReposeInf(intListDetailRow).bytMask = udtSource(intListRow).udtReposeInf(intListDetailRow).bytMask

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
    ' 　　　    : ARG1 - (I ) 構造体２
    ' 機能説明  : 構造体の設定値を比較する
    ' 備考　　  : 構造体メンバの中に構造体配列がいると Equals メソッドで正しい結果が得られないため関数を用意
    ' 　　　　  : 構造体メンバの中に構造体配列がいない場合は、 Equals メソッドで処理しても良いが一応これを使うこと
    ' 　　　　  : String文字列の比較には gCompareString を使用すること（単純な = だとNULL文字の有り無しで結果が変わってしまう）
    '--------------------------------------------------------------------
    Private Function mChkStructureEquals(ByVal udt1() As gTypSetChGroupReposeRec, _
                                         ByVal udt2() As gTypSetChGroupReposeRec) As Boolean

        Try

            Dim intListRow As Integer
            Dim intListDetailRow As Integer

            ''チェックボックスの内容が初期と現在で違う場合はfalseを返す 2018.12.13 倉重
            If g_bytPRE_GREPNUM <> g_bytGREPNUM Then Return False

            For intListRow = LBound(udt1) To UBound(udt1)

                ''CH ID
                If udt1(intListRow).shtChId <> udt2(intListRow).shtChId Then Return False

                ''データ種別コード
                If udt1(intListRow).shtData <> udt2(intListRow).shtData Then Return False

                '---------------------------
                ' 詳細画面
                '---------------------------
                For intListDetailRow = LBound(udt1(intListRow).udtReposeInf) To UBound(udt1(intListRow).udtReposeInf)

                    ''CH ID
                    If udt1(intListRow).udtReposeInf(intListDetailRow).shtChId <> udt2(intListRow).udtReposeInf(intListDetailRow).shtChId Then Return False

                    ''マスク値
                    If udt1(intListRow).udtReposeInf(intListDetailRow).bytMask <> udt2(intListRow).udtReposeInf(intListDetailRow).bytMask Then Return False

                Next

                ''チェックマークがついていないかつ47行目の表示時にFor文を抜ける 2018.12.13 倉重
                If g_bytGREPNUM = 0 And intListRow = 47 Then
                    Exit For
                End If

            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '----------------------------------------------------------------------------
    ' 機能      : コントロール使用可/不可設定
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 選択行数
    ' 機能説明  : コンボボックの状態からボタンの使用可/不可を設定する
    '----------------------------------------------------------------------------
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        Try

            ''処理を抜ける条件
            If mintInitFlg Then Return ''初期化中の場合
            If grdGroupRepose.CurrentCell.RowIndex < 0 Then Return

            Timer1.Stop()

            ''Editボタンの使用可/不可設定
            Call mSetControlEnable(grdGroupRepose.CurrentCell.RowIndex)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能      : コントロール使用可/不可設定
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 選択行数
    ' 機能説明  : コンボボックの状態からボタンの使用可/不可を設定する
    '----------------------------------------------------------------------------
    Private Sub mSetControlEnable(ByVal hintRowIndex As Integer)

        Try

            ''データ種別コードが「Normal」の時だけ、Editボタンの使用を不可とする
            If CCInt(grdGroupRepose.Rows(hintRowIndex).Cells("cmbType").Value) = gCstCodeChGroupReposeTypeNormal Then
                cmdEdit.Enabled = False
            Else
                cmdEdit.Enabled = True
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能      : ADDのチェックボックス処理
    ' 返り値    : なし
    ' 引き数    : -
    ' 機能説明  : ADDのチェックボックスに変化があった場合の処理
    ' 履歴    　: 2018.12.13 新規作成 倉重
    '----------------------------------------------------------------------------
    Private Sub chkAddgrep_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkAddgrep.CheckedChanged

        Dim cellStyle As New DataGridViewCellStyle

        With grdGroupRepose
            '列ヘッダー
            .Columns(0).HeaderText = "CH No." : .Columns(0).Width = 96
            .Columns(1).HeaderText = "Type" : .Columns(1).Width = 120

            '行
            If chkAddgrep.Checked = True Then
                .RowCount = 72
            Else
                .RowCount = 48
            End If

            '列
            For i = 1 To .RowCount
                .Rows(i - 1).HeaderCell.Value = i.ToString
            Next

            ''偶数行の背景色を変える
            cellStyle.BackColor = gColorGridRowBack
            For i = 0 To .Rows.Count - 1
                If i Mod 2 <> 0 Then
                    .Rows(i).DefaultCellStyle = cellStyle
                End If
            Next

        End With

        ''iniファイルの中身をチェックボックスの状態に変更する
        If chkAddgrep.Checked = True Then
            g_bytGREPNUM = 1
        Else
            g_bytGREPNUM = 0

            '最大値が減った時に削除した配列を初期化
            For i As Integer = 48 To UBound(gudt.SetChGroupRepose.udtRepose)
                mudtSetReposeNew(i).shtChId = 0
                mudtSetReposeNew(i).shtData = 0
                For j As Integer = LBound(mudtSetReposeNew(j).udtReposeInf) To UBound(mudtSetReposeNew(j).udtReposeInf)

                    ''CH ID
                    mudtSetReposeNew(i).udtReposeInf(j).shtChId = 0

                    ''マスク値
                    mudtSetReposeNew(i).udtReposeInf(j).bytMask = 0

                Next
            Next
        End If

    End Sub

#End Region

End Class

