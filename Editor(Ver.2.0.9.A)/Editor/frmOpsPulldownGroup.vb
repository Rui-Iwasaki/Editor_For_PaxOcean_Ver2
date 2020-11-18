Public Class frmOpsPulldownGroup

#Region "変数定義"

    Private mudtMenuGroupNew() As gTypSetOpsPulldownMenuGroup = Nothing

    ''グループ数（画面表示関数で取得）
    Private mintRowCnt As Integer

    ''戻り値
    Private mintRtn As Integer

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 0:OK  <> 0:キャンセル
    ' 引き数    : ARG1 - (IO) プルダウングループメニュー設定構造体
    '           : ARG2 - (I ) セットカウント数
    '           : ARG3 - (IO) フォーム
    ' 機能説明  : 本画面を表示する
    '--------------------------------------------------------------------
    Friend Function gShow(ByRef udtMenuGroup() As gTypSetOpsPulldownMenuGroup, _
                          ByVal intSetCnt As Integer, _
                          ByRef frmOwner As Form) As Integer

        Try

            ''ボタン選択フラグ初期化
            mintRtn = 1

            ''引数保存
            mudtMenuGroupNew = udtMenuGroup

            ''サブグループ数
            mintRowCnt = intSetCnt

            ''本画面表示
            Call gShowFormModelessForCloseWait2(Me, frmOwner)

            ''OKで閉じる場合は戻り値設定
            If mintRtn = 0 Then
                udtMenuGroup = mudtMenuGroupNew
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
    Private Sub frmOpsPulldownGroup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''グリッドを初期化する
            Call mInitialDataGrid(mintRowCnt)

            ''画面設定
            Call mSetDisplay(mudtMenuGroupNew)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub frmOpsPulldownGroup_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        Try

            ''画面表示時のセル選択を解除
            grdGroup.CurrentCell = Nothing

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

    '--------------------------------------------------------------------
    ' 機能      : Okボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 保存処理を行う
    '--------------------------------------------------------------------
    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click

        Try

            ''グリッドの保留中の変更を全て適用させる
            Call grdGroup.EndEdit()

            ''入力チェック
            If Not mChkInput() Then Return

            ''設定値の保存
            Call mSetStructure(mudtMenuGroupNew)

            mintRtn = 0
            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Cancelボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        Try

            Me.Close()

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
    Private Sub grdGroup_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdGroup.EditingControlShowing

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdGroup_KeyPress

                ''イベントハンドラを追加する
                AddHandler tb.KeyPress, AddressOf grdGroup_KeyPress

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
    Private Sub grdGroup_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdGroup.KeyPress

        Try

            ''選択セルの名称取得
            Dim strColumnName As String = grdGroup.CurrentCell.OwningColumn.Name

            ''[GROUP_MENU_NAME]
            If strColumnName = "txtGroupName" Then
                e.Handled = gCheckTextInput(24, sender, e.KeyChar, False)
            End If

            ''[SET_COUNT]
            If strColumnName = "txtSetCount" Then
                e.Handled = gCheckTextInput(2, sender, e.KeyChar)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 'SET COUNT'　入力チェック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdGroup_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles grdGroup.CellValidating

        Try

            ''------------------------------------------------------------
            '' 入力チェックは mChkInput で実施するよう変更
            ''------------------------------------------------------------

            'Dim dgv As DataGridView = CType(sender, DataGridView)
            'Dim strColumnName = dgv.Columns(e.ColumnIndex).Name

            ' ''[SET_COUNT]
            'If strColumnName = "txtSetCount" Then
            '    e.Cancel = gChkTextNumSpan(0, 17, e.FormattedValue)
            'End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： >>ボタン クリック時処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdGroup_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdGroup.CellContentClick

        Try

            ''処理を抜ける条件
            If e.RowIndex < 0 Or e.RowIndex > grdGroup.RowCount - 1 Then Return ''行数が0より小さい、もしくは最大行数より大きい場合
            If e.ColumnIndex < 0 Or e.ColumnIndex > grdGroup.ColumnCount - 1 Then Return ''列数が0より小さい、もしくは最大列数より大きい場合

            ''共通数値入力チェック
            If Not gChkInputNum(grdGroup.Rows(e.RowIndex).Cells("txtSetCount"), 1, 17, "Set Count", e.RowIndex + 1, False, True) Then Return

            Dim intCount As Integer = gConvNullToZero(grdGroup.Rows(e.RowIndex).Cells("txtSetCount").Value)

            ''セットカウント０、セットカウント空白の時はボタンクリックで何もしない
            If intCount <> 0 Then

                ''設定値を比較用構造体に格納
                Call mSetStructure(mudtMenuGroupNew)

                If frmOpsPulldownSub.gShow(mudtMenuGroupNew(e.RowIndex).udtSub, intCount, Me) = 0 Then
                    Call mSetDisplay(mudtMenuGroupNew)
                End If

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
    ' 引数      ：
    ' 戻値      ：
    '----------------------------------------------------------------------------
    Private Sub mInitialDataGrid(ByVal hintSetRowCnt As Integer)

        Try

            Dim i As Integer
            Dim cellStyle As New DataGridViewCellStyle

            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "txtGroupName"
            Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "txtSetCount"
            Dim Column3 As New DataGridViewButtonColumn : Column3.Name = "cmdNext"

            Column2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            With grdGroup

                ''列
                .Columns.Clear()
                .Columns.Add(Column1) : .Columns.Add(Column2) : .Columns.Add(Column3)
                .AllowUserToResizeColumns = False                                               ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing  ''行ヘッダー幅の変更不可

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "GROUP MENU NAME" : .Columns(0).Width = 280
                .Columns(1).HeaderText = "SET COUNT" : .Columns(1).Width = 90
                .Columns(2).HeaderText = "" : .Columns(2).Width = 60
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 13
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
                .ScrollBars = ScrollBars.None

                ''＞＞ボタン　初期値
                Column3.UseColumnTextForButtonValue = True
                Column3.Text = ">>"

                ''[SetCount]設定数分だけを入力可とする
                For i = 0 To .RowCount - 1

                    If i > hintSetRowCnt - 1 Then

                        ''[SetCount]未設定サブグループ行
                        .Rows(i).Cells("txtGroupName").Style.BackColor = gColorGridRowBackReadOnly
                        .Rows(i).Cells("txtSetCount").Style.BackColor = gColorGridRowBackReadOnly
                        .Rows(i).ReadOnly = True

                    Else

                        ''[SetCount]設定サブグループ行
                        ''SetCount初期化
                        .Rows(i).Cells(1).Value = 0
                    End If
                Next

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdGroup)

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

            For i As Integer = 0 To mintRowCnt - 1

                If Not gChkInputText(grdGroup(0, i), "GROUP NAME", i + 1, True, True) Then Return False
                If Not gChkInputNum(grdGroup(1, i), 0, 17, "SET COUNT", i + 1, False, True) Then Return False

            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) システム設定構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet() As gTypSetOpsPulldownMenuGroup)

        Try

            For i As Integer = 0 To UBound(udtSet)

                With udtSet(i)

                    ''サブメニューグループ名称
                    .strName = Trim(grdGroup.Rows(i).Cells("txtGroupName").Value)

                    ''サブメニューグループ設定数
                    .bytCount = CCbyte(grdGroup.Rows(i).Cells("txtSetCount").Value)

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) システム設定構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal udtSet() As gTypSetOpsPulldownMenuGroup)

        Try

            For i As Integer = 0 To UBound(udtSet)

                With udtSet(i)

                    ''グループ名称
                    grdGroup.Rows(i).Cells("txtGroupName").Value = gGetString(.strName)

                    ''グループ設定数
                    grdGroup.Rows(i).Cells("txtSetCount").Value = .bytCount

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

End Class
