Public Class frmSeqOperationFixed_GAI

#Region "変数定義"

    Private mblnInitFlg As Boolean
    Private mintNowSelectIndex As Integer
    Private mudtSetSeqOpeExpNew As gTypSetSeqOperationExpression
    Private dgvCmb As DataGridViewComboBoxEditingControl = Nothing

    Private mblnVariableFlg As Boolean
    Private mblnFixedFlg As Boolean
    Private mblnVariableFixedFlg As Boolean

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
    Private Sub frmSeqOperationFixed_GAI_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''初期化開始
            mblnInitFlg = True

            ''Table No コンボ　初期設定
            Call gSetComboBox(cmbTableNo, gEnmComboType.ctSeqOpeTableNo)
            cmbTableNo.SelectedIndex = mintSetNo

            ''現在選択されているコンボのインデックスを保存
            mintNowSelectIndex = cmbTableNo.SelectedIndex

            ''グリッド 初期設定
            Call mInitialDataGrid()

            ''構造体配列初期化
            Call mudtSetSeqOpeExpNew.InitArray()
            For i As Integer = LBound(mudtSetSeqOpeExpNew.udtTables) To UBound(mudtSetSeqOpeExpNew.udtTables)
                Call mudtSetSeqOpeExpNew.udtTables(i).InitArray()
                For j As Integer = LBound(mudtSetSeqOpeExpNew.udtTables(i).udtAryInf) To UBound(mudtSetSeqOpeExpNew.udtTables(i).udtAryInf)
                    Call mudtSetSeqOpeExpNew.udtTables(i).udtAryInf(j).InitArray()
                Next
            Next

            ''構造体コピー
            Call mCopyStructure(gudt.SetSeqOpeExp, mudtSetSeqOpeExpNew)

            ''画面設定
            Call mSetDisplay(cmbTableNo.SelectedIndex, gudt.SetSeqOpeExp)

            ''初期化開始
            mblnInitFlg = False

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
                Call mSetStructure(mintNowSelectIndex, mudtSetSeqOpeExpNew)

                ''選択されたTableNoの情報を表示
                Call mSetDisplay(cmbTableNo.SelectedIndex, mudtSetSeqOpeExpNew)

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

            ''入力チェック
            If Not mChkInput() Then Return

            ''設定値を比較用構造体に格納
            Call mSetStructure(cmbTableNo.SelectedIndex, mudtSetSeqOpeExpNew)

            ''データが変更されているかチェック
            If Not mChkStructureEquals(mudtSetSeqOpeExpNew, gudt.SetSeqOpeExp) Then

                ''変更された場合は設定を更新する
                Call mCopyStructure(mudtSetSeqOpeExpNew, gudt.SetSeqOpeExp)

                ''メッセージ表示
                Call MessageBox.Show("It saved.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                ''更新フラグ設定
                gblnUpdateAll = True
                gudt.SetEditorUpdateInfo.udtSave.bytSeqOperationExpression = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytSeqOperationExpression = 1

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
            Call mSetStructure(cmbTableNo.SelectedIndex, mudtSetSeqOpeExpNew)

            ''データが変更されているかチェック
            If Not mChkStructureEquals(mudtSetSeqOpeExpNew, gudt.SetSeqOpeExp) Then

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
                        Call mCopyStructure(mudtSetSeqOpeExpNew, gudt.SetSeqOpeExp)

                        ''更新フラグ設定
                        gblnUpdateAll = True
                        gudt.SetEditorUpdateInfo.udtSave.bytSeqOperationExpression = 1
                        gudt.SetEditorUpdateInfo.udtCompile.bytSeqOperationExpression = 1

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
    ' 機能      : VariableNameグリッドキー押下イベント
    ' 返り値　　: なし
    ' 引き数    : なし
    ' 機能説明  : 各種入力制限を行う
    '----------------------------------------------------------------------------
    Private Sub grdVariableName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdVariableName.KeyPress

        Try

            ''選択セルの名称取得
            Dim strColumnName As String = grdVariableName.CurrentCell.OwningColumn.Name

            ''[VARIABLE NAME]
            If strColumnName = "txtVariableName" Then
                e.Handled = gCheckTextInput(8, sender, e.KeyChar, False)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


    '----------------------------------------------------------------------------
    ' 機能      : CHグリッドData列キー押下イベント
    ' 返り値　　: なし
    ' 引き数    : なし
    ' 機能説明  : 各種入力制限を行う
    '----------------------------------------------------------------------------
    Private Sub grdCH_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdCH.KeyPress

        Try

            'Dim objText As DataGridViewTextBoxEditingControl
            'objText = CType(sender, DataGridViewTextBoxEditingControl)

            Select Case grdCH.CurrentCell.OwningColumn.Name
                Case "txtData"

                    ''選択されている定数種類によって入力制限処理を分岐
                    Select Case grdCH(1, grdCH.SelectedCells(0).RowIndex).Value
                        Case gCstCodeSeqFixTypeChData, _
                             gCstCodeSeqFixTypeLowSet, gCstCodeSeqFixTypeHighSet, _
                             gCstCodeSeqFixTypeLLSet, gCstCodeSeqFixTypeHHSet

                            ''CHデータ
                            e.Handled = gCheckTextInput(5, sender, e.KeyChar, True)

                        Case gCstCodeSeqFixTypeFixFloat

                            ''定数（Float）
                            e.Handled = gCheckTextInput(8, sender, e.KeyChar, True, True, True)

                        Case gCstCodeSeqFixTypeFixLong

                            ''定数（Long）
                            e.Handled = gCheckTextInput(8, sender, e.KeyChar, True, True, False)

                    End Select

                Case "txtFixedName"

                    e.Handled = gCheckTextInput(8, sender, e.KeyChar, False)

            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "入力チェック"

    '----------------------------------------------------------------------------
    ' 機能      : VariableNameセル編集後イベント
    ' 返り値　　: なし
    ' 引き数    : なし
    ' 機能説明  : 入力チェックを行う
    '----------------------------------------------------------------------------
    Private Sub grdVariableName_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles grdVariableName.CellValidating

        Try

            'Dim dgv As DataGridView = CType(sender, DataGridView)

            'Call dgv.EndEdit()

            'Select Case dgv.Columns(e.ColumnIndex).Name
            '    Case "txtVariableName"

            '        ''数値のみ入力の場合
            '        If IsNumeric(Trim(dgv.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)) Then

            '            Call MessageBox.Show("The name only of the numerical value cannot be set." & vbNewLine & _
            '                                 "Please re-input [Variable Name] of RowNo[" & e.RowIndex + 1 & "].", _
            '                                 "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '            e.Cancel = True

            '        End If

            'End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能      : CHグリッドセル編集後イベント
    ' 返り値　　: なし
    ' 引き数    : なし
    ' 機能説明  : 入力チェックを行う
    '----------------------------------------------------------------------------
    Private Sub grdCH_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles grdCH.CellValidating

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)

            Call dgv.EndEdit()

            Select Case dgv.Columns(e.ColumnIndex).Name
                Case "cmbType"

                    ''Dataに何か入力されている場合
                    If Trim(dgv.Rows(e.RowIndex).Cells(e.ColumnIndex + 1).Value) <> "" Then

                        ''数値ではない場合
                        If Not IsNumeric(Trim(dgv.Rows(e.RowIndex).Cells(e.ColumnIndex + 1).Value)) Then
                            dgv.Rows(e.RowIndex).Cells(e.ColumnIndex + 1).Value = 0
                        End If

                        ''範囲を超えている場合
                        If dgv.Rows(e.RowIndex).Cells(e.ColumnIndex + 1).Value > 65535 Then
                            dgv.Rows(e.RowIndex).Cells(e.ColumnIndex + 1).Value = 65535
                        End If

                    End If


                Case "txtData"

                    ''選択されている定数種類によって入力制限処理を分岐
                    Select Case grdCH(1, grdCH.SelectedCells(0).RowIndex).Value
                        Case gCstCodeSeqFixTypeChData, _
                             gCstCodeSeqFixTypeLowSet, gCstCodeSeqFixTypeHighSet, _
                             gCstCodeSeqFixTypeLLSet, gCstCodeSeqFixTypeHHSet

                            ' ''何か入力されている場合
                            'If Trim(dgv.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) <> "" Then

                            '    ''数値ではない場合
                            '    If Not IsNumeric(Trim(dgv.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)) Then
                            '        dgv.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = ""
                            '    End If

                            '    ''範囲を超えている場合
                            '    If dgv.Rows(e.RowIndex).Cells(e.ColumnIndex).Value > 65535 Then
                            '        dgv.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = 65535
                            '    End If

                            'End If

                            ' ''CHデータ
                            'e.Cancel = gChkTextNumSpan(0, 65535, dgv.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)

                        Case gCstCodeSeqFixTypeFixFloat, gCstCodeSeqFixTypeFixLong

                            ''-（マイナス）符号のみが入力されている場合は 0 に変換
                            If Trim(dgv.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) = "-" Then
                                dgv.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = 0
                            End If

                    End Select

                Case "txtFixedName"

                    ' ''数値のみ入力の場合
                    'If IsNumeric(Trim(dgv.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)) Then

                    '    Call MessageBox.Show("The name only of the numerical value cannot be set." & vbNewLine & _
                    '                         "Please re-input [Fixed Number Name] of CH No[" & dgv.Rows(e.RowIndex).Cells(0).Value & "].", _
                    '                         "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '    e.Cancel = True

                    'End If

            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "イベントハンドラ操作"

    '----------------------------------------------------------------------------
    ' 機能      : CHグリッドセル編集コントロール表示イベント
    ' 返り値　　: なし
    ' 引き数    : なし
    ' 機能説明  : キー押下イベントハンドラの設定を行う
    '----------------------------------------------------------------------------
    Private Sub grdCH_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdCH.EditingControlShowing

        Try

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdCH_KeyPress

                ''イベントハンドラを追加する
                AddHandler tb.KeyPress, AddressOf grdCH_KeyPress

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能      : VariableNameグリッドセル編集コントロール表示イベント
    ' 返り値　　: なし
    ' 引き数    : なし
    ' 機能説明  : キー押下イベントハンドラの設定を行う
    '----------------------------------------------------------------------------
    Private Sub grdVariableName_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdVariableName.EditingControlShowing

        Try

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdVariableName_KeyPress

                ''イベントハンドラを追加する
                AddHandler tb.KeyPress, AddressOf grdVariableName_KeyPress

            End If

            Dim dgv As DataGridView = CType(sender, DataGridView)

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

            '=======================
            ''VariableNameグリッド
            '=======================
            For i As Integer = 0 To grdVariableName.RowCount - 1

                ''何か入力されている場合
                If Trim(grdVariableName(0, i).Value) <> "" Then

                    ''先頭１文字がアルファベット or アンダーバー以外の場合
                    If Not mChkAlphabetOrUnderbar(Mid(Trim(grdVariableName(0, i).Value), 1, 1)) Then
                        Call MessageBox.Show("Please input an alphabet or underbar to the first character in [Variable Name] of RowNo[" & i + 1 & "].", _
                                             "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False
                    End If
                End If
            Next

            ''VariableNameに重複した名前は登録不可
            For i As Integer = 0 To grdVariableName.RowCount - 1

                If Trim(grdVariableName(0, i).Value) <> "" Then

                    For j As Integer = i + 1 To grdVariableName.RowCount - 1

                        If Trim(grdVariableName(0, i).Value) = Trim(grdVariableName(0, j).Value) Then

                            Call MessageBox.Show("The same name as [Variable Name] cannot be set of RowNo[" & i + 1 & "] and RowNo[" & j + 1 & "].", _
                             "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return False

                        End If
                    Next
                End If
            Next

            '=======================
            ''CHグリッド
            '=======================
            For i As Integer = 0 To grdCH.RowCount - 1

                ''選択されている定数種類によって入力制限処理を分岐
                Select Case grdCH(1, i).Value
                    Case gCstCodeSeqFixTypeChData, _
                         gCstCodeSeqFixTypeLowSet, gCstCodeSeqFixTypeHighSet, _
                         gCstCodeSeqFixTypeLLSet, gCstCodeSeqFixTypeHHSet

                        ''CHデータ:共通数値入力チェック
                        If Not gChkInputNum(grdCH.Rows(i).Cells("txtData"), 0, 65535, "Data", i + 1, True, True) Then Return False

                    Case gCstCodeSeqFixTypeFixFloat, gCstCodeSeqFixTypeFixLong

                        ''定数:共通数値入力チェック
                        If Not gChkInputNum(grdCH.Rows(i).Cells("txtData"), -2147483648, 2147483647, "Data", i + 1, True, True) Then Return False

                End Select

                If Trim(grdCH(2, i).Value) <> "" Then

                    ''DATAに 0 以外が入力されていて、FixedNumberName が空白の場合
                    If Trim(grdCH(2, i).Value) <> "0" And Trim(grdCH(3, i).Value) = "" Then
                        Call MessageBox.Show("Please input [Fixed Number Name] of CH No[" & grdCH(0, i).Value & "].", _
                                             "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False
                    End If

                    '' ver.1.4.0 コメントアウト　数値文字も入力可能とする
                    ''DATAに 0 以外が入力されていて、FixedNumberName の先頭１文字がアルファベット or アンダーバー以外の場合
                    'If Trim(grdCH(2, i).Value) <> "0" Then
                    '    If Not mChkAlphabetOrUnderbar(Mid(Trim(grdCH(3, i).Value), 1, 1)) Then

                    '        Call MessageBox.Show("Please input an alphabet or underbar to the first character in [Fixed Number Name] of CH No[" & grdCH(0, i).Value & "].", _
                    '                             "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '        Return False

                    '    End If
                    'End If
                End If

            Next

            ''FixedNumberNameに重複した名前は登録不可
            For i As Integer = 0 To grdCH.RowCount - 1

                If Trim(grdCH(3, i).Value) <> "" Then

                    For j As Integer = i + 1 To grdCH.RowCount - 1

                        If Trim(grdCH(3, i).Value) = Trim(grdCH(3, j).Value) Then

                            Call MessageBox.Show("The same name as [Fixed Number Name] cannot be set of CH No[" & grdCH(0, i).Value & "] and CH No[" & grdCH(0, j).Value & "].", _
                             "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return False

                        End If
                    Next
                End If
            Next

            '=======================
            ''Expressionテキスト
            '=======================
            ''変換後の文字数チェックする場合はコメントをはずす
            'Dim strSaveExpression As String
            'strSaveExpression = mSaveExpression(txtExpression.Text)
            'If Len(strSaveExpression) > 32 Then
            '    Call MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Return False
            'End If

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    Private Function mChkAlphabetOrUnderbar(ByVal strInput As String) As Boolean

        Try

            Dim strInputUpper As String

            ''文字を大文字に変換
            strInputUpper = StrConv(strInput, VbStrConv.Uppercase)

            ''アルファベット or アンダーバー の場合は True
            If (Chr(Asc(strInputUpper)) >= "A"c And Chr(Asc(strInputUpper)) <= "Z"c) Or Chr(Asc(strInput)) = "_"c Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) 演算式テーブル構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByVal intTableIndex As Integer, ByRef udtSet As gTypSetSeqOperationExpression)

        Try

            'Dim shtChNo As Short
            'Dim shtChId As Short
            'Dim shtSysNo As Short
            Dim bytAryWK() As Byte = Nothing

            With udtSet.udtTables(intTableIndex)

                If VariableFixedSetCHK(txtExpression.Text) = False Then
                    Call MessageBox.Show("Please input an Variable or Fixed to the Nothing", _
                                            "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                Else
                    ''演算式
                    .strExp = mSaveExpression(txtExpression.Text)
                End If

                ''VariavleName
                For intRow As Integer = LBound(.strVariavleName) To UBound(.strVariavleName)
                    .strVariavleName(intRow) = Trim(grdVariableName.Item(0, intRow).Value)
                Next

                ''AryInf
                For intRow As Integer = LBound(.udtAryInf) To UBound(.udtAryInf)

                    ''定数種類
                    .udtAryInf(intRow).shtType = grdCH.Item(1, intRow).Value

                    ''定数種類別の値格納
                    Select Case .udtAryInf(intRow).shtType
                        Case gCstCodeSeqFixTypeChData, _
                             gCstCodeSeqFixTypeLowSet, gCstCodeSeqFixTypeHighSet, _
                             gCstCodeSeqFixTypeLLSet, gCstCodeSeqFixTypeHHSet

                            ''CHデータ
                            If Trim(grdCH.Item(2, intRow).Value) = "" Then
                                bytAryWK = BitConverter.GetBytes(CShort(0))
                            Else
                                bytAryWK = BitConverter.GetBytes(CUInt(grdCH.Item(2, intRow).Value))
                            End If

                            ReDim Preserve bytAryWK(3)
                            bytAryWK(2) = bytAryWK(0) : bytAryWK(0) = 0
                            bytAryWK(3) = bytAryWK(1) : bytAryWK(1) = 0

                            .udtAryInf(intRow).bytInfo(0) = bytAryWK(0)
                            .udtAryInf(intRow).bytInfo(1) = bytAryWK(1)
                            .udtAryInf(intRow).bytInfo(2) = bytAryWK(2)
                            .udtAryInf(intRow).bytInfo(3) = bytAryWK(3)

                            '▼▼▼ ver.1.4.0 2011.07.05 Long指定、Float指定を分けて保存 ▼▼▼▼▼▼▼▼▼

                        Case gCstCodeSeqFixTypeFixFloat

                            ''定数(Float)
                            .udtAryInf(intRow).bytInfo = BitConverter.GetBytes(CSng(IIf(Trim(grdCH.Item(2, intRow).Value) = "", 0, Trim(grdCH.Item(2, intRow).Value))))

                        Case gCstCodeSeqFixTypeFixLong

                            ''定数(Long)
                            .udtAryInf(intRow).bytInfo = BitConverter.GetBytes(CInt(IIf(Trim(grdCH.Item(2, intRow).Value) = "", 0, Trim(grdCH.Item(2, intRow).Value))))

                            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                    End Select

                    ''定数名称
                    .udtAryInf(intRow).strFixNum = grdCH.Item(3, intRow).Value

                Next

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 演算式テーブル構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal intTableIndex As Integer, ByVal udtSet As gTypSetSeqOperationExpression)

        Try

            'Dim shtChNo As UInt16
            Dim intValue As Integer

            With udtSet.udtTables(intTableIndex)

                ''VariableName
                For intRow As Integer = LBound(.strVariavleName) To UBound(.strVariavleName)
                    grdVariableName.Item(0, intRow).Value = Trim(.strVariavleName(intRow))
                Next

                ''AryInf
                For intRow As Integer = LBound(.udtAryInf) To UBound(.udtAryInf)

                    ''CH NO
                    grdCH.Item(0, intRow).Value = 20000 + (intTableIndex * 16) + (intRow + 1)

                    ''定数種類
                    grdCH.Item(1, intRow).Value = CStr(.udtAryInf(intRow).shtType)

                    ''定数種類別の値表示
                    Select Case .udtAryInf(intRow).shtType
                        Case gCstCodeSeqFixTypeChData

                            ''CHデータ
                            intValue = gConnect2Byte(.udtAryInf(intRow).bytInfo(2), _
                                                    .udtAryInf(intRow).bytInfo(3))

                            grdCH.Item(2, intRow).Value = IIf(intValue = 0, "", intValue)

                        Case gCstCodeSeqFixTypeLowSet, gCstCodeSeqFixTypeHighSet, _
                             gCstCodeSeqFixTypeLLSet, gCstCodeSeqFixTypeHHSet

                            ''L,H,LL,HH
                            intValue = gConnect2Byte(.udtAryInf(intRow).bytInfo(2), _
                                                    .udtAryInf(intRow).bytInfo(3))

                            grdCH.Item(2, intRow).Value = intValue

                            '' ↓↓↓↓↓ K.Tanigawa 2012/01/11 FloatとLongのCase処理を分離 ↓↓↓↓↓ 
                        Case gCstCodeSeqFixTypeFixFloat

                            ''定数
                            grdCH.Item(2, intRow).Value = BitConverter.ToSingle(.udtAryInf(intRow).bytInfo, 0)

                            '' ↓↓↓↓↓ K.Tanigawa 2012/01/11 FloatとLongのCase処理を分離 ↓↓↓↓↓ 
                        Case gCstCodeSeqFixTypeFixLong

                            ''定数
                            grdCH.Item(2, intRow).Value = BitConverter.ToInt32(.udtAryInf(intRow).bytInfo, 0)

                    End Select

                    ''定数名称
                    grdCH.Item(3, intRow).Value = gGetString(.udtAryInf(intRow).strFixNum)

                    ''演算式
                    If .strExp <> "" Then
                        txtExpression.Text = mLoadExpression(.strExp)
                    End If

                Next

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッドを初期化する
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mInitialDataGrid()

        Try

            Dim i As Integer
            Dim cellStyle As New DataGridViewCellStyle

            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "txtVariableName"

            Dim Column10 As New DataGridViewTextBoxColumn : Column10.Name = "txtChNo"
            Dim Column11 As New DataGridViewComboBoxColumn : Column11.Name = "cmbType"
            Dim Column12 As New DataGridViewTextBoxColumn : Column12.Name = "txtData"
            Dim Column13 As New DataGridViewTextBoxColumn : Column13.Name = "txtFixedName"

            Column10.ReadOnly = True
            Column10.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            With grdVariableName

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
                .Columns(0).HeaderText = "Variable Name" : .Columns(0).Width = 100
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 9
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing  ''行ヘッダー幅の変更不可

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
                .ScrollBars = ScrollBars.None

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdVariableName)

            End With

            With grdCH

                ''列
                .Columns.Clear()
                .Columns.Add(Column10) : .Columns.Add(Column11) : .Columns.Add(Column12) : .Columns.Add(Column13)
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "CH No." : .Columns(0).Width = 80
                .Columns(1).HeaderText = "Type" : .Columns(1).Width = 140
                .Columns(2).HeaderText = "Data" : .Columns(2).Width = 90
                .Columns(3).HeaderText = "Fixed Number Name" : .Columns(3).Width = 130
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 17
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする

                'For i = 1 To 16
                '    .Rows(i - 1).Cells(0).Value = "200" & i.ToString("00")
                'Next

                ''行ヘッダー
                .RowHeadersVisible = False

                ''コンボボックス初期設定
                Call gSetComboBox(Column11, gEnmComboType.ctSeqOpeFixedType)

                ''偶数行の背景色を変える
                cellStyle.BackColor = gColorGridRowBack
                For i = 0 To .Rows.Count - 1
                    If i Mod 2 <> 0 Then
                        .Rows(i).DefaultCellStyle = cellStyle
                    End If
                Next

                ''ReadOnly色設定
                For i = 0 To .RowCount - 1
                    .Rows(i).Cells("txtChNo").Style.BackColor = gColorGridRowBackReadOnly
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
                Call gSetGridCopyAndPaste(grdCH)

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
    Private Sub mCopyStructure(ByVal udtSource As gTypSetSeqOperationExpression, _
                               ByRef udtTarget As gTypSetSeqOperationExpression)

        Try

            ''テーブル情報
            For i As Integer = LBound(udtSource.udtTables) To UBound(udtSource.udtTables)

                ''演算式コピー
                udtTarget.udtTables(i).strExp = udtSource.udtTables(i).strExp

                ''VariableNameコピー
                For j As Integer = LBound(udtSource.udtTables(i).strVariavleName) To UBound(udtSource.udtTables(i).strVariavleName)
                    udtTarget.udtTables(i).strVariavleName(j) = udtSource.udtTables(i).strVariavleName(j)
                Next

                ''AryInfコピー
                For j As Integer = LBound(udtSource.udtTables(i).udtAryInf) To UBound(udtSource.udtTables(i).udtAryInf)

                    ''定数種類
                    udtTarget.udtTables(i).udtAryInf(j).shtType = udtSource.udtTables(i).udtAryInf(j).shtType

                    ''情報（バイト配列）
                    For k As Integer = LBound(udtSource.udtTables(i).udtAryInf(j).bytInfo) To UBound(udtSource.udtTables(i).udtAryInf(j).bytInfo)
                        udtTarget.udtTables(i).udtAryInf(j).bytInfo(k) = udtSource.udtTables(i).udtAryInf(j).bytInfo(k)
                    Next

                    ''定数名称
                    udtTarget.udtTables(i).udtAryInf(j).strFixNum = udtSource.udtTables(i).udtAryInf(j).strFixNum

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
    Private Function mChkStructureEquals(ByVal udt1 As gTypSetSeqOperationExpression, _
                                         ByVal udt2 As gTypSetSeqOperationExpression) As Boolean

        Try


            ''テーブル情報
            For i As Integer = LBound(udt1.udtTables) To UBound(udt1.udtTables)

                ''演算式比較
                If Not gCompareString(udt1.udtTables(i).strExp, udt2.udtTables(i).strExp) Then Return False

                ''VariableName比較
                For j As Integer = LBound(udt1.udtTables(i).strVariavleName) To UBound(udt1.udtTables(i).strVariavleName)
                    If Not gCompareString(udt1.udtTables(i).strVariavleName(j), udt2.udtTables(i).strVariavleName(j)) Then Return False
                Next

                ''AryInf比較
                For j As Integer = LBound(udt1.udtTables(i).udtAryInf) To UBound(udt1.udtTables(i).udtAryInf)

                    ''定数種類比較
                    If udt1.udtTables(i).udtAryInf(j).shtType <> udt2.udtTables(i).udtAryInf(j).shtType Then Return False

                    ''情報（バイト配列）比較
                    For k As Integer = LBound(udt1.udtTables(i).udtAryInf(j).bytInfo) To UBound(udt1.udtTables(i).udtAryInf(j).bytInfo)
                        If udt1.udtTables(i).udtAryInf(j).bytInfo(k) <> udt2.udtTables(i).udtAryInf(j).bytInfo(k) Then Return False
                    Next

                    ''定数名称比較
                    If Not gCompareString(udt1.udtTables(i).udtAryInf(j).strFixNum, udt2.udtTables(i).udtAryInf(j).strFixNum) Then Return False

                Next

            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    Private Function mSaveExpression(ByVal strOrg As String) As String

        Dim strVariableName() As String = Nothing
        Dim strFixedName() As String = Nothing
        Dim strRtn As String = ""
        Dim strchar As String
        Dim strword As String = ""
        Dim ascchar As Integer

        ''名称リスト取得
        Call mGetVariableNameList(strVariableName)
        Call mGetFixedNameList(strFixedName)

        '' ver.1.4.0 2011.07.28 変換処理変更
        If strOrg <> "" Then
            For i As Integer = 0 To Len(strOrg) - 1
                strchar = Mid(strOrg, i + 1, 1)                                     ' 1文字取得
                ascchar = Asc(strchar)                                              ' 整数値に変換

                If (ascchar >= Asc("a") And ascchar <= Asc("z")) Or _
                   (ascchar >= Asc("A") And ascchar <= Asc("Z")) Or _
                   (ascchar >= Asc("0") And ascchar <= Asc("9")) Then

                    strword = strword & strchar                                     ' 変換ワード取得
                    If i = Len(strOrg) - 1 Then                                     ' 最終文字
                        For j As Integer = 0 To UBound(strVariableName)             ' a-h 変数 Variable
                            If strword = strVariableName(j) Then
                                strRtn = strRtn & mGetReplaceCharVariable(j + 1)
                                strword = ""
                                Exit For
                            End If
                        Next

                        If strword <> "" Then
                            For j As Integer = 0 To UBound(strFixedName)            ' A-P 定数 Fixed
                                If strword = strFixedName(j) Then
                                    strRtn = strRtn & mGetReplaceCharFixed(j + 1)
                                    strword = ""
                                    Exit For
                                End If
                            Next
                        End If

                        If strword <> "" Then
                            strRtn = strRtn & strword
                            strword = ""
                        End If
                    End If
                Else
                    If strword <> "" Then

                        For j As Integer = 0 To UBound(strVariableName)             ' a-h 変数 Variable
                            If strword = strVariableName(j) Then
                                strRtn = strRtn & mGetReplaceCharVariable(j + 1)
                                strword = ""
                                Exit For
                            End If
                        Next

                    End If

                    If strword <> "" Then

                        For j As Integer = 0 To UBound(strFixedName)                ' A-P 定数 Fixed
                            If strword = strFixedName(j) Then
                                strRtn = strRtn & mGetReplaceCharFixed(j + 1)
                                strword = ""
                                Exit For
                            End If
                        Next

                    End If

                    If strword <> "" Then
                        If ascchar = Asc("+") Or ascchar = Asc("-") Or ascchar = Asc("/") Or ascchar = Asc("*") Or ascchar = Asc("(") And ascchar = Asc(")") _
                                 Or ascchar = Asc("^") Or ascchar = Asc("#") Or ascchar = Asc("$") Or ascchar = Asc("%") Or ascchar = Asc("&") And ascchar = Asc("!") _
                                 Or ascchar = Asc("?") Or ascchar = Asc("@") Or ascchar = Asc("~") Then

                            strRtn = strRtn & strword
                            strword = ""
                            mblnVariableFixedFlg = True
                        Else
                            If ascchar <> Asc(" ") Then
                                mblnVariableFixedFlg = False
                                strRtn = ""
                                strword = ""
                            End If

                        End If

                    End If

                strRtn = strRtn & strchar                                       ' 演算子
                End If

            Next
        End If

        ' ver1.4.0 2011.07.28 コメントアウト　▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
        ' ''VariableNameを置き換え
        'For i As Integer = 0 To UBound(strVariableName)

        '    If strVariableName(i) <> "" Then
        '        strOrg = Replace(strOrg, strVariableName(i), mGetReplaceCharVariable(i + 1))
        '    End If

        'Next

        ' ''FixedNameを置き換え
        'For i As Integer = 0 To UBound(strFixedName)

        '    If strFixedName(i) <> "" Then
        '        strOrg = Replace(strOrg, strFixedName(i), mGetReplaceCharFixed(i + 1))
        '    End If

        'Next
        ' ▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

        Return strRtn

    End Function

    Private Function VariableFixedSetCHK(ByVal strOrg As String) As Boolean

        Dim strVariableName() As String = Nothing
        Dim strFixedName() As String = Nothing
        Dim strRtn As String = ""
        Dim strchar As String
        Dim strword As String = ""
        Dim ascchar As Integer

        ''名称リスト取得
        Call mGetVariableNameList(strVariableName)
        Call mGetFixedNameList(strFixedName)

        VariableFixedSetCHK = True
        mblnVariableFixedFlg = True
        mblnFixedFlg = False
        mblnVariableFlg = False

        If strOrg <> "" Then
            For i As Integer = 0 To Len(strOrg) - 1
                strchar = Mid(strOrg, i + 1, 1)                                     ' 1文字取得
                ascchar = Asc(strchar)                                              ' 整数値に変換

                If (ascchar >= Asc("a") And ascchar <= Asc("z")) Or (ascchar >= Asc("A") And ascchar <= Asc("Z")) Or (ascchar >= Asc("0") And ascchar <= Asc("9")) Then

                    strword = strword & strchar                                     ' 変換ワード取得

                    If i = Len(strOrg) - 1 Then                                     ' 最終文字
                        For j As Integer = 0 To UBound(strVariableName)             ' a-h 変数 Variable
                            If strword = strVariableName(j) Then
                                mblnVariableFlg = True
                            End If

                            If strword = strFixedName(j) Then
                                mblnFixedFlg = True
                            End If

                            If mblnVariableFlg = False And mblnFixedFlg = False Then
                                mblnVariableFixedFlg = False
                                Exit For
                            End If
                        Next

                        strword = ""

                    End If
                Else
                    If strword <> "" Then
                        For j As Integer = 0 To UBound(strVariableName)             ' a-h 変数 Variable
                            If strword = strVariableName(j) Then
                                mblnVariableFlg = True
                            End If

                            If strword = strFixedName(j) Then
                                mblnFixedFlg = True
                            End If

                            If mblnVariableFlg = False And mblnFixedFlg = False Then
                                mblnVariableFixedFlg = False
                                Exit For
                            End If
                        Next

                        strword = ""

                    End If
                End If

                If mblnVariableFixedFlg = False Then
                    VariableFixedSetCHK = False
                    Exit For
                End If
            Next

        End If

    End Function

    Private Function mLoadExpression(ByVal strOrg As String) As String

        Dim strVariableName() As String = Nothing
        Dim strFixedName() As String = Nothing
        Dim strRtn As String = ""
        Dim strchar As String
        Dim ascchar As Integer

        '▼▼▼ 20110705 特定条件時に正しく変換されない場合がある ▼▼▼▼▼▼▼▼▼
        'VariableName=(1)A,(2)B,(3)C
        'FixedNumberName=(1)g
        'Expression=A*B-(C+g)
        'とした場合、本LoadでVariableNameの(1)Aがgに変換されてしまう
        '原因はVariableNameで設定した名前"A"と、FixedNumberNameのファイルデータに
        '保存すべき名前"A"が同じになってしまっているため
        '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

        ''名称リスト取得
        Call mGetVariableNameList(strVariableName)
        Call mGetFixedNameList(strFixedName)

        '' ver.1.4.0 2011.07.28 変換処理変更
        If strOrg(0) <> Nothing Then
            For i As Integer = 0 To Len(strOrg) - 1
                strchar = Mid(strOrg, i + 1, 1)                             ' 1文字取得
                ascchar = Asc(strchar)                                      ' 整数値に変換
                If (ascchar >= Asc("a") And ascchar <= Asc("h")) Then       ' a-h 変数 Variable
                    If strVariableName(ascchar - Asc("a")) <> "" Then
                        strRtn = strRtn & strVariableName(ascchar - Asc("a"))
                    Else
                        strRtn = strRtn & strchar
                    End If
                ElseIf (ascchar >= Asc("A") And ascchar <= Asc("P")) Then   ' A-P 定数 Fixed
                    If strFixedName(ascchar - Asc("A")) <> "" Then
                        strRtn = strRtn & strFixedName(ascchar - Asc("A"))
                    Else
                        strRtn = strRtn & strchar
                    End If
                Else
                    strRtn = strRtn & strchar                               ' 演算子
                End If
            Next
        End If

        ' ver1.4.0 2011.07.28 コメントアウト　▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
        ' ''VariableNameを置き換え
        'strRtn = strOrg
        'For i As Integer = 0 To UBound(strVariableName)
        '    If strVariableName(i) <> "" Then
        '        strRtn = Replace(strRtn, mGetReplaceCharVariable(i + 1), strVariableName(i))
        '    End If
        'Next

        ' ''FixedNameを置き換え
        'For i As Integer = 0 To UBound(strFixedName)
        '    If strFixedName(i) <> "" Then
        '        strRtn = Replace(strRtn, mGetReplaceCharFixed(i + 1), strFixedName(i))
        '    End If
        'Next

        'If strOrg = "" Then
        '    strRtn = strOrg
        'End If
        ' ▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

        Return strRtn

    End Function

    Private Sub mGetVariableNameList(ByRef strVariableName() As String)

        With grdVariableName

            For i As Integer = 0 To .RowCount - 1

                ReDim Preserve strVariableName(i)
                strVariableName(i) = Trim(.Item(0, i).Value)

            Next

        End With

    End Sub

    Private Sub mGetFixedNameList(ByRef strFixedName() As String)

        With grdCH

            For i As Integer = 0 To .RowCount - 1

                ReDim Preserve strFixedName(i)
                strFixedName(i) = Trim(.Item(3, i).Value)

            Next

        End With

    End Sub

    Private Function mGetReplaceCharVariable(ByVal intNo As Integer) As String

        Select Case intNo
            Case 1 : Return "a"
            Case 2 : Return "b"
            Case 3 : Return "c"
            Case 4 : Return "d"
            Case 5 : Return "e"
            Case 6 : Return "f"
            Case 7 : Return "g"
            Case 8 : Return "h"
            Case Else : Return "x"
        End Select

    End Function

    Private Function mGetReplaceCharFixed(ByVal intNo As Integer) As String

        Select Case intNo
            Case 1 : Return "A"
            Case 2 : Return "B"
            Case 3 : Return "C"
            Case 4 : Return "D"
            Case 5 : Return "E"
            Case 6 : Return "F"
            Case 7 : Return "G"
            Case 8 : Return "H"
            Case 9 : Return "I"
            Case 10 : Return "J"
            Case 11 : Return "K"
            Case 12 : Return "L"
            Case 13 : Return "M"
            Case 14 : Return "N"
            Case 15 : Return "O"
            Case 16 : Return "P"
            Case Else : Return "X"
        End Select

    End Function

#End Region

End Class
