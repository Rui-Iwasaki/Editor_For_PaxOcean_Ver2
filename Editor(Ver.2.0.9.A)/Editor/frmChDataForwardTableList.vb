Public Class frmChDataForwardTableList

#Region "定数定義"

    ''データサブコードの最大入力文字列長
    Private Const mCstCodeMaxLengthSubCodeCalc As Integer = 2
    Private Const mCstCodeMaxLengthSubCodeComm As Integer = 1

    ''オフセットアドレスの最大入力文字列長
    Private Const mCstCodeMaxLengthOffset As Integer = 8

    ''データサイズの最大入力文字列長
    Private Const mCstCodeMaxLengthSize As Integer = 4

#End Region

#Region "変数定義"

    Private mudtSetChDataForwardTableSet As gTypSetChDataForward = Nothing

    ''初期化フラグ
    Private mblnInitFlg As Boolean

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
    Private Sub frmChDataForwardTableList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''初期化フラグ
            mblnInitFlg = True

            ''グリッドを初期化する
            Call mInitialDataGrid()

            ''配列再定義
            Call mudtSetChDataForwardTableSet.InitArray()

            ''画面設定
            Call mSetDisplay(gudt.SetChDataForward)

            ''初期化フラグ
            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub frmChDataForwardTableList_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        Try

            ''画面表示時のセル選択を解除
            grdData.CurrentCell = Nothing

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
            Call mSetStructure(mudtSetChDataForwardTableSet)

            ''データが変更されているかチェック
            If Not mChkStructureEquals(mudtSetChDataForwardTableSet, gudt.SetChDataForward) Then

                ''変更された場合は設定を更新する
                Call mCopyStructure(mudtSetChDataForwardTableSet, gudt.SetChDataForward)

                ''メッセージ表示
                Call MessageBox.Show("It saved.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                ''更新フラグ設定
                gblnUpdateAll = True
                gudt.SetEditorUpdateInfo.udtSave.bytChDataForwardTableSet = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytChDataForwardTableSet = 1

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

            ''グリッドの保留中の変更を全て適用させる
            Call grdData.EndEdit()

            ''設定値を比較用構造体に格納
            Call mSetStructure(mudtSetChDataForwardTableSet)

            ''データが変更されているかチェック
            If Not mChkStructureEquals(mudtSetChDataForwardTableSet, gudt.SetChDataForward) Then

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
                        Call mCopyStructure(mudtSetChDataForwardTableSet, gudt.SetChDataForward)

                        ''更新フラグ設定
                        gblnUpdateAll = True
                        gudt.SetEditorUpdateInfo.udtSave.bytChDataForwardTableSet = 1
                        gudt.SetEditorUpdateInfo.udtCompile.bytChDataForwardTableSet = 1

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



#Region "グリッドイベント"

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッドエラー発生時のイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdData_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles grdData.DataError

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベントを発生させる
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdData_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdData.EditingControlShowing

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)
            Dim strColumnName As String

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdData_KeyPress

                ''該当する列ならイベントハンドラを追加する
                strColumnName = dgv.CurrentCell.OwningColumn.Name

                If strColumnName.Substring(0, 3) = "txt" Then

                    AddHandler tb.KeyPress, AddressOf grdData_KeyPress

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
    ' 備考      ： データサブコード、オフセットアドレス、データサイズ項目の
    '           ： 入力文字数をチェックする
    '----------------------------------------------------------------------------
    Private Sub grdData_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdData.KeyPress

        Try

            ''初期化中は処理を抜ける
            If mblnInitFlg Then Return

            ''例外キーチェック
            If Asc(e.KeyChar) = 8 Then Return ''BackSpace

            If (e.KeyChar >= "0"c And e.KeyChar <= "9"c) Then

                ''---------------------------------
                '' 入力値が0～9の場合
                ''---------------------------------
                e.Handled = mCheckInputLengthNum(sender, e)

                ''---------------------------------
                '' 入力値が小文字のa～fの場合
                ''---------------------------------
            ElseIf (e.KeyChar >= "a"c And e.KeyChar <= "f"c) Or (e.KeyChar >= "A"c And e.KeyChar <= "F"c) Then

                ''文字を大文字に変換
                Select Case e.KeyChar
                    Case "a"c : e.KeyChar = "A"c
                    Case "b"c : e.KeyChar = "B"c
                    Case "c"c : e.KeyChar = "C"c
                    Case "d"c : e.KeyChar = "D"c
                    Case "e"c : e.KeyChar = "E"c
                    Case "f"c : e.KeyChar = "F"c
                End Select

                ''入力チェック
                e.Handled = mCheckInputLengthValue(sender, e)

            Else

                ''---------------------------------
                '' 0～9、a～f、A～F以外は入力不可
                ''---------------------------------
                e.Handled = True

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 数値の入力チェック関数
    ' 返り値    : True：入力不可、False：入力可
    ' 引き数    : ARG1 - (I ) グリッド テキストボックスオブジェクト
    '           : ARG2 - (I ) グリッド KeyPressイベント
    ' 機能説明  : 数値の入力文字数をチェックする
    '--------------------------------------------------------------------
    Private Function mCheckInputLengthNum(ByVal objGridText As Object, _
                                          ByVal evt As System.Windows.Forms.KeyPressEventArgs) As Boolean

        Dim blnRtn As Boolean = False
        Dim strColumnName As String = grdData.CurrentCell.OwningColumn.Name

        Try

            If strColumnName = "txtSubCode" Then

                ''--------------------
                '' データサブコード
                ''--------------------
                Select Case CCInt(grdData.Rows(grdData.CurrentCell.RowIndex).Cells("cmbCode").Value)

                    Case gCstCodeChDataForwardCodeCalc

                        ''データコードで「演算」を選択した場合
                        blnRtn = mCheckInputLengthValueLimit(mCstCodeMaxLengthSubCodeCalc, objGridText)

                    Case Else

                        ''データコードで「設定なし」「通信」を選択した場合
                        blnRtn = gCheckTextInput(mCstCodeMaxLengthSubCodeComm, objGridText, evt.KeyChar)

                End Select

            ElseIf strColumnName.Substring(0, 9) = "txtOffset" Then

                ''--------------------
                '' オフセットアドレス
                ''--------------------
                blnRtn = gCheckTextInput(mCstCodeMaxLengthOffset, objGridText, evt.KeyChar)

            ElseIf strColumnName.Substring(0, 7) = "txtSize" Then

                ''--------------------
                '' データサイズ
                ''--------------------
                blnRtn = gCheckTextInput(mCstCodeMaxLengthSize, objGridText, evt.KeyChar)

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            blnRtn = True
        End Try

        Return blnRtn

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 文字列の入力チェック関数
    ' 返り値    : True：入力不可、False：入力可
    ' 引き数    : ARG1 - (I ) グリッド テキストボックスオブジェクト
    '           : ARG2 - (I ) グリッド KeyPressイベント
    ' 機能説明  : 文字の入力文字数をチェックする
    '--------------------------------------------------------------------
    Private Function mCheckInputLengthValue(ByVal objGridText As Object, _
                                            ByVal evt As System.Windows.Forms.KeyPressEventArgs) As Boolean

        Dim blnRtn As Boolean = False
        Dim strColumnName As String = grdData.CurrentCell.OwningColumn.Name

        Try

            If strColumnName = "txtSubCode" Then

                ''--------------------
                '' データサブコード
                ''--------------------
                Select Case CCInt(grdData.Rows(grdData.CurrentCell.RowIndex).Cells("cmbCode").Value)

                    Case gCstCodeChDataForwardCodeCalc

                        ''データコードで「演算」を選択した場合
                        blnRtn = mCheckInputLengthValueLimit(mCstCodeMaxLengthSubCodeCalc, objGridText)

                    Case Else

                        ''データコードで「設定なし」「通信」を選択した場合　
                        blnRtn = mCheckInputLengthValueLimit(mCstCodeMaxLengthSubCodeComm, objGridText)

                End Select

            ElseIf strColumnName.Substring(0, 9) = "txtOffset" Then

                ''--------------------
                '' オフセットアドレス
                ''--------------------
                blnRtn = mCheckInputLengthValueLimit(mCstCodeMaxLengthOffset, objGridText)

            ElseIf strColumnName.Substring(0, 7) = "txtSize" Then

                ''--------------------
                '' データサイズ
                ''--------------------
                blnRtn = mCheckInputLengthValueLimit(mCstCodeMaxLengthSize, objGridText)

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            blnRtn = True
        End Try

        Return blnRtn

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 最大入力文字列長以上の入力制限
    ' 返り値    : True：入力不可、False：入力可
    ' 引き数    : ARG1 - (I ) 最大入力文字列長
    '           : ARG2 - (I ) グリッド KeyPressイベント
    ' 機能説明  : 入力文字数をチェックする
    '--------------------------------------------------------------------
    Private Function mCheckInputLengthValueLimit(ByVal hintMaxLength As Integer, _
                                                 ByVal hobjGridText As Object) As Boolean
        Try

            ''入力文字列の取得
            Dim strInputString As String = hobjGridText.EditingControlFormattedValue.ToString

            ''入力文字列長の取得
            Dim intInputLength As Integer = strInputString.Length

            ''入力文字数のチェック
            If intInputLength >= hintMaxLength Then

                ''最大入力文字列長以上の場合は入力を制限する
                Return True

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return True
        End Try

        Return False

    End Function

    ''----------------------------------------------------------------------------
    '' 機能説明  ： 入力チェック
    '' 引数      ： なし
    '' 戻値      ： なし
    ''----------------------------------------------------------------------------
    Private Sub grdData_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles grdData.CellValidating

        Try

            ''------------------------------------------------------------
            '' 入力チェックは mChkInput で実施するよう変更
            ''------------------------------------------------------------

            ''If e.RowIndex < 0 Or e.ColumnIndex <> 2 Then Exit Sub

            'Dim dgv As DataGridView = CType(sender, DataGridView)

            ' ''グリッドの保留中の変更を全て適用させる
            'Call dgv.EndEdit()

            'If e.ColumnIndex <> grdData.ColumnCount - 1 Then

            '    If Not IsNumeric("&H" & Trim(dgv.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)) Then
            '        dgv.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = 0
            '    End If

            'End If


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

            Dim Column1 As New DataGridViewComboBoxColumn : Column1.Name = "cmbCode"
            Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "txtSubCode"
            Dim Column3 As New DataGridViewTextBoxColumn : Column3.Name = "txtOffsetToFCU"
            Dim Column4 As New DataGridViewTextBoxColumn : Column4.Name = "txtSizeToFCU"
            Dim Column5 As New DataGridViewTextBoxColumn : Column5.Name = "txtOffsetToOPS"
            Dim Column6 As New DataGridViewTextBoxColumn : Column6.Name = "txtSizeToOPS"

            Column2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column6.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            With grdHead

                ''列
                .Columns.Clear()
                .Columns.Add(New DataGridViewCheckBoxColumn()) : .Columns.Add(New DataGridViewCheckBoxColumn())
                .Columns.Add(New DataGridViewCheckBoxColumn()) : .Columns.Add(New DataGridViewCheckBoxColumn())
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''列ヘッダー
                .Columns(0).HeaderText = "" : .Columns(0).Width = 90
                .Columns(1).HeaderText = "" : .Columns(1).Width = 90
                .Columns(2).HeaderText = "OPS > FCU (HEX)" : .Columns(2).Width = 180
                .Columns(3).HeaderText = "FCU > OPS (HEX)" : .Columns(3).Width = 180
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 1
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可

                ''行ヘッダー
                .RowHeadersWidth = 50

                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                ''スクロールバー
                .ScrollBars = ScrollBars.None

            End With

            With grdData

                ''列
                .Columns.Clear()
                .Columns.Add(Column1) : .Columns.Add(Column2) : .Columns.Add(Column3)
                .Columns.Add(Column4) : .Columns.Add(Column5) : .Columns.Add(Column6)
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "DATA CODE" : .Columns(0).Width = 90
                .Columns(1).HeaderText = "DATA SUB CODE (HEX)" : .Columns(1).Width = 90
                .Columns(2).HeaderText = "OFFSET ADDRESS" : .Columns(2).Width = 90
                .Columns(3).HeaderText = "DATA SIZE" : .Columns(3).Width = 90
                .Columns(4).HeaderText = "OFFSET ADDRESS" : .Columns(4).Width = 90
                .Columns(5).HeaderText = "DATA SIZE" : .Columns(5).Width = 90
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 64 + 1
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

                ''コンボボックス初期設定
                Call gSetComboBox(Column1, gEnmComboType.ctChDataForwardTableListColumn1)

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdData)

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 入力チェック
    ' 返り値    : True：入力OK、False：入力NG
    ' 引き数    : なし
    ' 機能説明  : 入力チェックを行う
    '--------------------------------------------------------------------
    Private Function mChkInput() As Boolean

        Try

            Dim strCode As String = ""
            Dim strSubCode As String = ""
            Dim strOffsetToFCU As String, strSizeToFCU As String = ""
            Dim strOffsetToOPS As String, strSizeToOPS As String = ""

            ''グリッドの保留中の変更を全て適用させる
            Call grdData.EndEdit()

            For i = 0 To grdData.RowCount - 1

                ''設定値取得
                strCode = gGetString(grdData.Rows(i).Cells("cmbCode").Value)
                strSubCode = gGetString(grdData.Rows(i).Cells("txtSubCode").Value)
                strOffsetToFCU = gGetString(grdData.Rows(i).Cells("txtOffsetToFCU").Value)
                strSizeToFCU = gGetString(grdData.Rows(i).Cells("txtSizeToFCU").Value)
                strOffsetToOPS = gGetString(grdData.Rows(i).Cells("txtOffsetToOPS").Value)
                strSizeToOPS = gGetString(grdData.Rows(i).Cells("txtSizeToOPS").Value)

                If strCode = gCstCodeChDataForwardCodeNone.ToString And _
                   strSubCode = "0" And _
                   strOffsetToFCU = "0" And _
                   strSizeToFCU = "0" And _
                   strOffsetToOPS = "0" And _
                   strSizeToOPS = "0" Then

                    ''OK

                Else

                    ''-----------------------------
                    '' 入力チェック
                    ''-----------------------------
                    ''データサブコード
                    Select Case CCInt(grdData.Rows(i).Cells("cmbCode").Value)

                        Case gCstCodeChDataForwardCodeNone

                            ''入力チェックなし

                        Case gCstCodeChDataForwardCodeCalc

                            ''データコードで「演算」を選択した場合
                            If Not gChkInputNum(CCUInt32Hex(grdData.Rows(i).Cells("txtSubCode").Value), 1, 255, "DATA SUB CODE", i + 1, True, True) Then Return False

                        Case Else

                            ''データコードで「演算」以外を選択した場合（新規追加項目を含む）
                            If Not gChkInputNum(grdData.Rows(i).Cells("txtSubCode"), 1, 9, "DATA SUB CODE", i + 1, True, True) Then Return False

                    End Select

                    ''オフセットアドレス（OPS→FCU）※KeyPressで制限済
                    ''If Not gChkInputNum(CCUInt32Hex(grdData.Rows(i).Cells("txtOffsetToFCU").Value), 0, 4294967295, "OFFSET ADDRESS (OPS > FCU)", i + 1, True, True) Then Return False

                    ''データサイズ（OPS→FCU）
                    If Not gChkInputNum(CCUInt32Hex(grdData.Rows(i).Cells("txtSizeToFCU").Value), 0, 65535, "DATA SIZE (OPS > FCU)", i + 1, True, True) Then Return False

                    ''オフセットアドレス（FCU→OPS）※KeyPressで制限済
                    ''If Not gChkInputNum(CCUInt32Hex(grdData.Rows(i).Cells("txtOffsetToOPS").Value), 0, 4294967295, "OFFSET ADDRESS (FCU > OPS)", i + 1, True, True) Then Return False

                    ''データサイズ（FCU→OPS）
                    If Not gChkInputNum(CCUInt32Hex(grdData.Rows(i).Cells("txtSizeToOPS").Value), 0, 65535, "DATA SIZE (FCU > OPS)", i + 1, True, True) Then Return False

                End If

            Next i

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) データ転送テーブル設定構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet As gTypSetChDataForward)

        Try

            For i As Integer = 0 To UBound(udtSet.udtDetail)

                With udtSet.udtDetail(i)

                    ''データコード
                    .shtDataCode = CCShort(grdData.Rows(i).Cells("cmbCode").Value)

                    ''データサブコード
                    Select Case CCShort(grdData.Rows(i).Cells("cmbCode").Value)

                        Case gCstCodeChDataForwardCodeCalc

                            ''データコードで「演算」を選択  
                            .shtDataSubCode = CCUInt16Hex(grdData.Rows(i).Cells("txtSubCode").Value)

                        Case Else

                            ''データコードで「設定なし」「通信」「新規追加項目」を選択
                            .shtDataSubCode = CCShort(grdData.Rows(i).Cells("txtSubCode").Value)

                    End Select

                    ''オフセットアドレス(OPS→FCU)  
                    .intOffsetToFCU = CCUInt32Hex(grdData.Rows(i).Cells("txtOffsetToFCU").Value)

                    ''データサイズ(OPS→FCU)        
                    .shtSizeToFCU = CCUInt16Hex(grdData.Rows(i).Cells("txtSizeToFCU").Value)

                    ''オフセットアドレス(FCU→OPS)  
                    .intOffsetToOPS = CCUInt32Hex(grdData.Rows(i).Cells("txtOffsetToOPS").Value)

                    ''データサイズ(FCU→OPS)        
                    .shtSizeToOps = CCUInt16Hex(grdData.Rows(i).Cells("txtSizeToOPS").Value)

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) データ転送テーブル設定構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal udtSet As gTypSetChDataForward)

        Try

            For i As Integer = 0 To UBound(udtSet.udtDetail)

                With udtSet.udtDetail(i)

                    ''データコード
                    grdData.Rows(i).Cells("cmbCode").Value = .shtDataCode.ToString

                    ''データサブコード
                    Select Case CCShort(grdData.Rows(i).Cells("cmbCode").Value)

                        Case gCstCodeChDataForwardCodeCalc

                            ''データコードで「演算」を選択  16進数表記
                            grdData.Rows(i).Cells("txtSubCode").Value = Hex(.shtDataSubCode)

                        Case Else

                            ''データコードで「設定なし」「通信」「新規追加項目」を選択
                            grdData.Rows(i).Cells("txtSubCode").Value = .shtDataSubCode

                    End Select

                    ''オフセットアドレス(OPS→FCU)          16進数表記
                    grdData.Rows(i).Cells("txtOffsetToFCU").Value = Hex(.intOffsetToFCU)

                    ''データサイズ(OPS→FCU)                16進数表記
                    grdData.Rows(i).Cells("txtSizeToFCU").Value = Hex(.shtSizeToFCU)

                    ''オフセットアドレス(FCU→OPS)          16進数表記
                    grdData.Rows(i).Cells("txtOffsetToOPS").Value = Hex(.intOffsetToOPS)

                    ''データサイズ(FCU→OPS)                16進数表記
                    grdData.Rows(i).Cells("txtSizeToOPS").Value = Hex(.shtSizeToOps)

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
    ' 　　　    : ARG2 - ( O) 複製先
    ' 機能説明  : 構造体を複製する
    ' 備考　　  : 構造体メンバの中に構造体配列がいると単純に = では複製できないため関数を用意
    ' 　　　　  : ↑ = でやると配列部分が参照渡しになり（？）値更新時に両方更新されてしまう
    ' 　　　　  : 構造体メンバの中に構造体配列がいない場合は、この関数を使わずに = で処理しても良い
    '--------------------------------------------------------------------
    Private Sub mCopyStructure(ByVal udtSource As gTypSetChDataForward, _
                               ByRef udtTarget As gTypSetChDataForward)

        Try

            With udtTarget

                For i As Integer = 0 To UBound(udtTarget.udtDetail)

                    udtTarget.udtDetail(i) = udtSource.udtDetail(i)

                Next

            End With

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
    Private Function mChkStructureEquals(ByVal udt1 As gTypSetChDataForward, _
                                         ByVal udt2 As gTypSetChDataForward) As Boolean

        Try

            For i As Integer = 0 To UBound(udt1.udtDetail)

                ''データコード
                If udt1.udtDetail(i).shtDataCode <> udt2.udtDetail(i).shtDataCode Then Return False

                ''データサブコード
                If udt1.udtDetail(i).shtDataSubCode <> udt2.udtDetail(i).shtDataSubCode Then Return False

                ''オフセットアドレス(OPS→FCU)
                If udt1.udtDetail(i).intOffsetToFCU <> udt2.udtDetail(i).intOffsetToFCU Then Return False

                ''データサイズ(OPS→FCU)
                If udt1.udtDetail(i).shtSizeToFCU <> udt2.udtDetail(i).shtSizeToFCU Then Return False

                ''オフセットアドレス(FCU→OPS)
                If udt1.udtDetail(i).intOffsetToOPS <> udt2.udtDetail(i).intOffsetToOPS Then Return False

                ''データサイズ(FCU→OPS)
                If udt1.udtDetail(i).shtSizeToOps <> udt2.udtDetail(i).shtSizeToOps Then Return False

            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

End Class
