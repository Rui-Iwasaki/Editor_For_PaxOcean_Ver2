Public Class frmSeqLinearTable_GAI

#Region "変数定義"

    Private mblnInitFlg As Boolean
    Private mintNowSelectIndex As Integer
    Private mudtSetSeqLinearTableNew As gTypSetSeqLinear

    Private mintRtn As Integer
    Private mintSetNo As Integer    '指定行番号
    Private mintBack As Integer
#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 本画面を表示する
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Function gShow(ByVal intSetNo As Integer, _
                          ByRef intBack As Integer, _
                          ByRef frmOwner As Form) As Integer

        Try
            mintRtn = 1

            mintSetNo = intSetNo

            ''本画面表示
            Call gShowFormModelessForCloseWait2(Me, frmOwner)

            ''OKで閉じる場合は戻り値設定
            If mintRtn = 0 Then
                intBack = mintBack
            End If

            Return mintRtn
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "画面イベント"

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームロード
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmSeqLinearTable_GAI_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''初期化開始
            mblnInitFlg = True

            ''Table No コンボ　初期設定
            Call gSetComboBox(cmbTableNo, gEnmComboType.ctSeqLineTableNo)
            cmbTableNo.SelectedIndex = mintSetNo

            ''現在選択されているコンボのインデックスを保存
            mintNowSelectIndex = cmbTableNo.SelectedIndex

            ''グリッド 初期設定
            Call mInitialDataGrid()

            ''構造体配列初期化
            Call mudtSetSeqLinearTableNew.InitArray()
            For i As Integer = LBound(mudtSetSeqLinearTableNew.udtTables) To UBound(mudtSetSeqLinearTableNew.udtTables)
                Call mudtSetSeqLinearTableNew.udtTables(i).InitArray()
            Next

            ''構造体コピー
            Call mCopyStructure(gudt.SetSeqLinear, mudtSetSeqLinearTableNew)

            ''画面設定
            Call mSetDisplay(cmbTableNo.SelectedIndex, gudt.SetSeqLinear)

            ''初期化開始
            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : CSVReadクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : CSVからデータを読み込む
    '--------------------------------------------------------------------
    Private Sub cmdCsvRead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCsvRead.Click

        Try

            Dim strX() As String = Nothing
            Dim strY() As String = Nothing
            Dim dlgFile As New OpenFileDialog

            With dlgFile

                ''[ファイルの種類] ボックスに表示される選択肢を設定する
                .Filter = "csv file (*.csv)|*.csv"

                ''ダイアログ ボックスを表示
                If dlgFile.ShowDialog() = DialogResult.OK Then

                    ''CSVデータ取得
                    If gGetCsvData(dlgFile.FileName, grdLiner.RowCount - 1, strX, strY) = 0 Then

                        For i As Integer = 0 To grdLiner.RowCount - 1
                            grdLiner(0, i).Value = strX(i)
                            grdLiner(1, i).Value = strY(i)
                        Next

                    End If

                End If

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : TableNoコンボチェンジ
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 対象テーブルの情報を表示する
    '--------------------------------------------------------------------
    Private Sub cmbTableNo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTableNo.SelectedIndexChanged

        Try

            ''初期化中は何もしない
            If mblnInitFlg Then Return

            ''ここでの項目変更イベントは処理しない
            mblnInitFlg = True

            ''入力チェック
            If Not mChkInput() Then

                ''入力NGの場合はTableNoを元に戻す
                cmbTableNo.SelectedIndex = mintNowSelectIndex

            Else

                ''現在のTableNoに設定されている値を保存
                Call mSetStructure(mintNowSelectIndex, mudtSetSeqLinearTableNew)

                ''選択されたTableNoの情報を表示
                Call mSetDisplay(cmbTableNo.SelectedIndex, mudtSetSeqLinearTableNew)

                ''現在のTableNoを更新
                mintNowSelectIndex = cmbTableNo.SelectedIndex

            End If

            ''元に戻す
            mblnInitFlg = False

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

            ''SetCountに設定された分の情報を表示
            Call mDispSetCountValue()

            ''入力チェック
            If Not mChkInput() Then Return

            ''設定値を比較用構造体に格納
            Call mSetStructure(cmbTableNo.SelectedIndex, mudtSetSeqLinearTableNew)

            ''データが変更されているかチェック
            If Not mChkStructureEquals(mudtSetSeqLinearTableNew, gudt.SetSeqLinear) Then

                ''変更された場合は設定を更新する
                Call mCopyStructure(mudtSetSeqLinearTableNew, gudt.SetSeqLinear)

                ' ''ポイント数設定
                'Call mSetPoints(gudt.SetSeqLinear)

                ''メッセージ表示
                Call MessageBox.Show("It saved.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                ''更新フラグ設定
                gblnUpdateAll = True
                gudt.SetEditorUpdateInfo.udtSave.bytSeqLinear = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytSeqLinear = 1

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
    Private Sub frmSysSystem_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try

            ''設定値を比較用構造体に格納
            Call mSetStructure(cmbTableNo.SelectedIndex, mudtSetSeqLinearTableNew)

            ''データが変更されているかチェック
            If Not mChkStructureEquals(mudtSetSeqLinearTableNew, gudt.SetSeqLinear) Then

                ''変更されている場合はメッセージ表示
                Select Case MessageBox.Show("Setting has been changed." & vbNewLine & _
                                            "Do you save the changes?", Me.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                    Case Windows.Forms.DialogResult.Yes

                        ''SetCountに設定された分の情報を表示
                        Call mDispSetCountValue()

                        ''入力チェック
                        If Not mChkInput() Then
                            e.Cancel = True
                            Return
                        End If

                        ''変更された場合は設定を更新する
                        Call mCopyStructure(mudtSetSeqLinearTableNew, gudt.SetSeqLinear)

                        ''更新フラグ設定
                        gblnUpdateAll = True
                        gudt.SetEditorUpdateInfo.udtSave.bytSeqLinear = 1
                        gudt.SetEditorUpdateInfo.udtCompile.bytSeqLinear = 1

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

#Region "入力関連"

#Region "入力制限"

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベント発生時処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdLiner_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdLiner.KeyPress

        Try

            ''選択セルの名称取得
            Dim strColumnName As String = grdLiner.CurrentCell.OwningColumn.Name

            ''[TABLE_NO.]
            If strColumnName.Substring(0, 3) = "txt" Then
                ''2010/12/15 制限桁数変更（8→7）※指数表示を避けるため
                e.Handled = gCheckTextInput(7, sender, e.KeyChar, True, True, True)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "入力チェック"

    '----------------------------------------------------------------------------
    ' 機能説明  ： 入力値をフォーマットする
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdLiner_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdLiner.CellValidated

        Try

            Dim strValue As String
            Dim SetCount As Integer

            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub

            strValue = grdLiner.Rows(e.RowIndex).Cells(e.ColumnIndex).Value

            If strValue <> Nothing Then

                If strValue = "." Then
                    strValue = ""

                ElseIf strValue.Substring(0, 1) = "." Then
                    strValue = "0" & strValue

                ElseIf strValue.Substring(strValue.Length - 1, 1) = "." Then
                    strValue = strValue & "0"

                End If

                grdLiner.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = strValue

            End If

            ''SET COUNT数取得 ver.1.4.0 2011.09.26
            For i As Integer = 0 To grdLiner.RowCount - 1
                ''X
                If grdLiner.Rows(i).Cells(0).Value <> Nothing Then
                    SetCount = i + 1
                End If
                ''Y
                If grdLiner.Rows(i).Cells(1).Value <> Nothing Then
                    SetCount = i + 1
                End If
            Next
            numSetCount.Value = SetCount

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
#End Region

#Region "イベントハンドラ操作"

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベントを発生させる
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdLiner_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdLiner.EditingControlShowing

        Try

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdLiner_KeyPress

                ''イベントハンドラを追加する
                AddHandler tb.KeyPress, AddressOf grdLiner_KeyPress

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

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

            Dim i As Integer
            Dim intLastIndex As Integer = -1
            Dim strValueX As String = ""
            Dim strValueY As String = ""

            ''グリッドの保留中の変更を全て適用させる
            grdLiner.EndEdit()

            ''XとYは対で入力されているか？
            With grdLiner
                For i = 0 To .RowCount - 1

                    strValueX = .Rows(i).Cells(0).Value
                    strValueY = .Rows(i).Cells(1).Value

                    If strValueX <> "" And strValueY <> "" Then

                        ''Xに数値が入力されているか
                        If Not IsNumeric(strValueX) Then
                            Call MessageBox.Show("Please input the numerical value to 'X' of line no " & i + 1, "LINEAR TABLE", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Function
                        End If

                        ''Yに数値が入力されているか
                        If Not IsNumeric(strValueY) Then
                            Call MessageBox.Show("Please input the numerical value to 'Y' of line no " & i + 1, "LINEAR TABLE", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Function
                        End If

                        ''最終インデックスを保存
                        intLastIndex = i

                    ElseIf strValueX = "" And strValueY = "" Then
                        ''OK
                    Else
                        If strValueX = "" Then
                            Call MessageBox.Show("Please set 'X' data of the line no " & i + 1, "LINEAR TABLE", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Function
                        ElseIf strValueY = "" Then
                            Call MessageBox.Show("Please set 'Y' data of the line no " & i + 1, "LINEAR TABLE", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Function
                        End If

                        ''最終インデックスを保存
                        intLastIndex = i

                    End If
                Next

                ''間抜けなく入力されているか
                For i = 0 To intLastIndex

                    strValueX = .Rows(i).Cells(0).Value
                    strValueY = .Rows(i).Cells(1).Value

                    If strValueX = "" And strValueY = "" Then
                        Call MessageBox.Show("Please set 'X' and 'Y' data of the line no " & i + 1, "LINEAR TABLE", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Function
                    End If

                Next

                ''データはリニアに登録されているか
                For i = 0 To intLastIndex

                    ''データリニアのチェックは不要　ver.1.4.0 2011.09.26
                    ' ''１行目はチェックなし
                    'If i <> 0 Then

                    '    If CSng(strValueX) >= CSng(.Rows(i).Cells(0).Value) Then
                    '        Call MessageBox.Show("Please input the linear value to 'X' of line no " & i + 1, "LINEAR TABLE", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '        Exit Function
                    '    End If

                    '    If CSng(strValueY) > CSng(.Rows(i).Cells(1).Value) Then
                    '        Call MessageBox.Show("Please input the linear value to 'Y' of line no " & i + 1, "LINEAR TABLE", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '        Exit Function
                    '    End If

                    'End If

                    strValueX = .Rows(i).Cells(0).Value
                    strValueY = .Rows(i).Cells(1).Value

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
    ' 引き数    : ARG1 - ( O) リニアライズテーブル構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByVal intTableIndex As Integer, ByRef udtSet As gTypSetSeqLinear)

        Try

            udtSet.udtPoints(intTableIndex).shtPoint = numSetCount.Value

            With udtSet.udtTables(intTableIndex)

                For intRow As Integer = LBound(.udtRow) To UBound(.udtRow)

                    If intRow <= numSetCount.Value - 1 Then

                        .udtRow(intRow).sngPtX = IIf(Trim(grdLiner.Item(0, intRow).Value) = "", 0, grdLiner.Item(0, intRow).Value)
                        .udtRow(intRow).sngPtY = IIf(Trim(grdLiner.Item(1, intRow).Value) = "", 0, grdLiner.Item(1, intRow).Value)

                    Else

                        .udtRow(intRow).sngPtX = 0
                        .udtRow(intRow).sngPtY = 0

                    End If

                Next

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) リニアライズテーブル構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal intTableIndex As Integer, ByVal udtSet As gTypSetSeqLinear)

        Try

            numSetCount.Value = udtSet.udtPoints(intTableIndex).shtPoint

            With udtSet.udtTables(intTableIndex)

                For intRow As Integer = LBound(.udtRow) To UBound(.udtRow)

                    If intRow <= numSetCount.Value - 1 Then

                        grdLiner.Item(0, intRow).Value = .udtRow(intRow).sngPtX
                        grdLiner.Item(1, intRow).Value = .udtRow(intRow).sngPtY

                    Else

                        grdLiner.Item(0, intRow).Value = ""
                        grdLiner.Item(1, intRow).Value = ""

                    End If


                Next

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mDispSetCountValue()

        For i As Integer = 0 To grdLiner.RowCount - 1

            If i <= numSetCount.Value - 1 Then

                grdLiner.Item(0, i).Value = IIf(Trim(grdLiner.Item(0, i).Value) = "", 0, grdLiner.Item(0, i).Value)
                grdLiner.Item(1, i).Value = IIf(Trim(grdLiner.Item(1, i).Value) = "", 0, grdLiner.Item(1, i).Value)

            Else

                grdLiner.Item(0, i).Value = ""
                grdLiner.Item(1, i).Value = ""

            End If


        Next

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッドを初期化する
    ' 引数      ：
    ' 戻値      ：
    '----------------------------------------------------------------------------
    Private Sub mInitialDataGrid()

        Try

            Dim i As Integer
            Dim cellStyle As New DataGridViewCellStyle

            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "txtX"
            Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "txtY"

            Column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            With grdLiner

                ''列
                .Columns.Clear()
                .Columns.Add(Column1) : .Columns.Add(Column2)
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "X" : .Columns(0).Width = 100
                .Columns(1).HeaderText = "Y" : .Columns(1).Width = 100
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 1025
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing  ''行ヘッダー幅の変更不可

                ''行ヘッダー
                For i = 1 To .RowCount
                    .Rows(i - 1).HeaderCell.Value = i.ToString
                Next
                .RowHeadersWidth = 80
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
                Call gSetGridCopyAndPaste(grdLiner)

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
    Private Sub mCopyStructure(ByVal udtSource As gTypSetSeqLinear, _
                               ByRef udtTarget As gTypSetSeqLinear)

        Try

            ''ポイント数
            For i As Integer = LBound(udtTarget.udtPoints) To UBound(udtTarget.udtPoints)
                udtTarget.udtPoints(i).shtPoint = udtSource.udtPoints(i).shtPoint
            Next

            ''テーブル情報
            For i As Integer = LBound(udtTarget.udtTables) To UBound(udtTarget.udtTables)
                For j As Integer = LBound(udtTarget.udtTables(i).udtRow) To UBound(udtTarget.udtTables(i).udtRow)
                    udtTarget.udtTables(i).udtRow(j).sngPtX = udtSource.udtTables(i).udtRow(j).sngPtX
                    udtTarget.udtTables(i).udtRow(j).sngPtY = udtSource.udtTables(i).udtRow(j).sngPtY
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
    Private Function mChkStructureEquals(ByVal udt1 As gTypSetSeqLinear, _
                                         ByVal udt2 As gTypSetSeqLinear) As Boolean

        Try

            ''ポイント数
            For i As Integer = LBound(udt1.udtPoints) To UBound(udt1.udtPoints)
                If udt1.udtPoints(i).shtPoint <> udt2.udtPoints(i).shtPoint Then Return False
            Next

            ''テーブル情報
            For i As Integer = LBound(udt1.udtTables) To UBound(udt1.udtTables)
                For j As Integer = LBound(udt1.udtTables(i).udtRow) To UBound(udt1.udtTables(i).udtRow)
                    If udt1.udtTables(i).udtRow(j).sngPtX <> udt2.udtTables(i).udtRow(j).sngPtX Then Return False
                    If udt1.udtTables(i).udtRow(j).sngPtY <> udt2.udtTables(i).udtRow(j).sngPtY Then Return False
                Next
            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : ポイント数設定
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) リニアライズテーブル構造体
    ' 機能説明  : リニアライズ設定情報からポイント数を設定する
    '--------------------------------------------------------------------
    Private Sub mSetPoints(ByRef udtLinear As gTypSetSeqLinear)

        Try

            Dim intCnt As Integer

            For i As Integer = LBound(udtLinear.udtTables) To UBound(udtLinear.udtTables)

                For j As Integer = LBound(udtLinear.udtTables(i).udtRow) To UBound(udtLinear.udtTables(i).udtRow)

                    With udtLinear.udtTables(i).udtRow(j)

                        ''何か値が設定されていたらカウントアップ
                        If .sngPtX <> 0 And .sngPtY <> 0 Then intCnt += 1

                    End With

                Next

                ''ポイント数設定
                udtLinear.udtPoints(i).shtPoint = intCnt

                intCnt = 0

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

End Class
