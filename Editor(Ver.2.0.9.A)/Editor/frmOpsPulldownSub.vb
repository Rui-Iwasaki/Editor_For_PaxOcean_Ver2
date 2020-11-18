Public Class frmOpsPulldownSub

#Region "変数定義"

    Private mudtMenuSubNew() As gTypSetOpsPulldownMenuSub = Nothing

    ''サブグループ数（画面表示関数で取得）
    Private mintRowCnt As Integer

    ''戻り値
    Private mintRtn As Integer

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 0:OK  <> 0:キャンセル
    ' 引き数    : ARG1 - (IO) プルダウンサブメニュー設定構造体
    '           : ARG2 - (I ) セットカウント数
    '           : ARG3 - (IO) フォーム
    ' 機能説明  : 本画面を表示する
    '--------------------------------------------------------------------
    Friend Function gShow(ByRef udtMenuSub() As gTypSetOpsPulldownMenuSub, _
                          ByVal intSetCnt As Integer, _
                          ByRef frmOwner As Form) As Integer

        Try

            ''ボタン選択フラグ初期化
            mintRtn = 1

            ''引数保存
            mudtMenuSubNew = udtMenuSub

            ''サブグループ数
            mintRowCnt = intSetCnt

            ''本画面表示
            Call gShowFormModelessForCloseWait2(Me, frmOwner)

            ''OKで閉じる場合は戻り値設定
            If mintRtn = 0 Then
                udtMenuSub = mudtMenuSubNew
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
    Private Sub frmOpsPulldownSub_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''グリッドを初期化する
            Call mInitialDataGrid(mintRowCnt)

            ''画面設定
            Call mSetDisplay(mudtMenuSubNew)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub frmOpsPulldownSub_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        Try

            ''画面表示時のセル選択を解除
            grdSub.CurrentCell = Nothing

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : OKボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 保存処理を行う
    '--------------------------------------------------------------------
    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click

        Try

            ''グリッドの保留中の変更を全て適用させる
            Call grdSub.EndEdit()

            ''入力チェック
            If Not mChkInput() Then Return

            ''設定値の保存
            Call mSetStructure(mudtMenuSubNew)

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

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームクローズ
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmOpsPulldownSub_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

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
    Private Sub grdSub_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdSub.EditingControlShowing

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdSub_KeyPress

                ''イベントハンドラを追加する
                AddHandler tb.KeyPress, AddressOf grdSub_KeyPress

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
    Private Sub grdSub_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdSub.KeyPress

        Try

            ''選択セルの名称取得
            Dim strColumnName As String = grdSub.CurrentCell.OwningColumn.Name

            ''[SUB_MENU]
            If strColumnName = "txtSubMenu" Then
                e.Handled = gCheckTextInput(32, sender, e.KeyChar, False)
            End If

            ''[PROCESS_ITEM]
            If strColumnName.Substring(0, 7) = "txtItem" Then
                e.Handled = gCheckTextInput(3, sender, e.KeyChar)
            End If

            ''[SCREEN_NUMBER]
            If strColumnName = "txtScreenNumber" Then
                e.Handled = gCheckTextInput(3, sender, e.KeyChar)
            End If

            ''[KEY_CODE]
            If strColumnName = "txtKeyCode" Then
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
    Private Sub grdSub_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles grdSub.CellValidating

        Try

            ''------------------------------------------------------------------------
            '' 入力チェックは mChkInput で実施するよう変更
            ''------------------------------------------------------------------------

            'Dim dgv As DataGridView = CType(sender, DataGridView)
            'Dim strColumnName = dgv.Columns(e.ColumnIndex).Name

            'If e.RowIndex < 0 Or e.RowIndex >= mintRowCnt Or e.ColumnIndex < 0 Then Exit Sub

            ' ''[PROCESS_ITEM]
            'If strColumnName.Substring(0, 7) = "txtItem" Then
            '    If dgv.Rows(e.RowIndex).Cells(e.ColumnIndex).Value <> Nothing Then
            '        e.Cancel = gChkTextNumSpan(0, 255, e.FormattedValue)
            '    End If
            'End If

            ' ''[SCREEN_NUMBER]
            'If strColumnName = "txtScreenNumber" Then
            '    e.Cancel = gChkTextNumSpan(0, 39, e.FormattedValue)
            'End If

            ' ''[KEY_CODE]
            'If strColumnName = "txtKeyCode" Then
            '    e.Cancel = gChkTextNumSpan(0, 255, e.FormattedValue)
            'End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

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

            For i As Integer = 0 To mintRowCnt - 1

                If Not gChkInputText(grdSub.Rows(i).Cells("txtSubMenu"), "SUB MENU", i + 1, True, True) Then Return False
                If Not gChkInputNum(grdSub.Rows(i).Cells("txtItem1"), 0, 255, "PROCESS ITEM 1", i + 1, False, True) Then Return False
                If Not gChkInputNum(grdSub.Rows(i).Cells("txtItem2"), 0, 255, "PROCESS ITEM 2", i + 1, False, True) Then Return False
                If Not gChkInputNum(grdSub.Rows(i).Cells("txtItem3"), 0, 255, "PROCESS ITEM 3", i + 1, False, True) Then Return False
                If Not gChkInputNum(grdSub.Rows(i).Cells("txtItem4"), 0, 255, "PROCESS ITEM 4", i + 1, False, True) Then Return False
                If Not gChkInputNum(grdSub.Rows(i).Cells("txtScreenNumber"), 0, 999, "SCREEN NUMBER", i + 1, False, True) Then Return False
                If Not gChkInputNum(grdSub.Rows(i).Cells("txtKeyCode"), 0, 255, "KEY CODE", i + 1, False, True) Then Return False

            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッドを初期化する
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mInitialDataGrid(ByVal hintSetRowCnt As Integer)

        Try

            Dim i As Integer, j As Integer
            Dim cellStyle As New DataGridViewCellStyle

            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "txtSubMenu"
            Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "txtItem1"
            Dim Column3 As New DataGridViewTextBoxColumn : Column3.Name = "txtItem2"
            Dim Column4 As New DataGridViewTextBoxColumn : Column4.Name = "txtItem3"
            Dim Column5 As New DataGridViewTextBoxColumn : Column5.Name = "txtItem4"
            Dim Column6 As New DataGridViewTextBoxColumn : Column6.Name = "txtScreenNumber"
            Dim Column7 As New DataGridViewTextBoxColumn : Column7.Name = "txtKeyCode"

            Column2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column6.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column7.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            With grdHead

                ''列
                .Columns.Clear()
                .Columns.Add(New DataGridViewCheckBoxColumn())
                .Columns.Add(New DataGridViewCheckBoxColumn())
                .Columns.Add(New DataGridViewCheckBoxColumn())
                .Columns.Add(New DataGridViewCheckBoxColumn())
                .AllowUserToResizeColumns = False                                               ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing  ''行ヘッダー幅の変更不可

                ''列ヘッダー
                .Columns(0).HeaderText = "" : .Columns(0).Width = 350
                .Columns(1).HeaderText = "PROCESS ITEM" : .Columns(1).Width = 200
                .Columns(2).HeaderText = "" : .Columns(2).Width = 70
                .Columns(3).HeaderText = "" : .Columns(3).Width = 70
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

            With grdSub

                ''列
                .Columns.Clear()
                .Columns.Add(Column1) : .Columns.Add(Column2) : .Columns.Add(Column3)
                .Columns.Add(Column4) : .Columns.Add(Column5) : .Columns.Add(Column6)
                .Columns.Add(Column7)
                .AllowUserToResizeColumns = False                                               ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing  ''行ヘッダー幅の変更不可

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "SUB MENU" : .Columns(0).Width = 350
                .Columns(1).HeaderText = "1" : .Columns(1).Width = 50
                .Columns(2).HeaderText = "2" : .Columns(2).Width = 50
                .Columns(3).HeaderText = "3" : .Columns(3).Width = 50
                .Columns(4).HeaderText = "4" : .Columns(4).Width = 50
                .Columns(5).HeaderText = "SCREEN NUMBER" : .Columns(5).Width = 70
                .Columns(6).HeaderText = "KEY CODE" : .Columns(6).Width = 70
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 18
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

                ''[SetCount]設定数分だけを入力可とする
                For i = 0 To .RowCount - 1

                    If i > hintSetRowCnt - 1 Then

                        ''[SetCount]未設定サブグループ行
                        For j = 0 To grdSub.ColumnCount - 1
                            .Rows(i).Cells(j).Style.BackColor = gColorGridRowBackReadOnly
                        Next
                        .Rows(i).ReadOnly = True

                    Else

                        ''[SetCount]設定サブグループ行
                        For j = 1 To grdSub.ColumnCount - 1

                            ''SetCount初期化
                            .Rows(i).Cells(j).Value = 0

                        Next

                    End If

                Next i

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdSub)

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) システム設定構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtMenuSub() As gTypSetOpsPulldownMenuSub)

        Try

            For i As Integer = 0 To UBound(udtMenuSub)

                With udtMenuSub(i)

                    ''サブメニュー名称
                    .strName = Trim(grdSub.Rows(i).Cells("txtSubMenu").Value)

                    ''項目１
                    .SubbytMenuType1 = CCbyte(grdSub.Rows(i).Cells("txtItem1").Value)

                    ''項目２
                    .SubbytMenuType2 = CCbyte(grdSub.Rows(i).Cells("txtItem2").Value)

                    ''項目３
                    .SubbytMenuType3 = CCbyte(grdSub.Rows(i).Cells("txtItem3").Value)

                    ''項目４
                    .SubbytMenuType4 = CCbyte(grdSub.Rows(i).Cells("txtItem4").Value)

                    ''画面番号
                    .ViewNo1 = DataExchange2(Val(grdSub.Rows(i).Cells("txtScreenNumber").Value))

                    ''キーコード
                    .bytKeyCode = CCbyte(grdSub.Rows(i).Cells("txtKeyCode").Value)

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
    Private Sub mSetDisplay(ByVal udtMenuSub() As gTypSetOpsPulldownMenuSub)

        Try

            For i As Integer = 0 To UBound(udtMenuSub)

                With udtMenuSub(i)

                    ''サブメニュー名称
                    grdSub.Rows(i).Cells("txtSubMenu").Value = Trim(.strName)

                    ''項目１
                    grdSub.Rows(i).Cells("txtItem1").Value = .SubbytMenuType1

                    ''項目２
                    grdSub.Rows(i).Cells("txtItem2").Value = .SubbytMenuType2

                    ''項目３
                    grdSub.Rows(i).Cells("txtItem3").Value = .SubbytMenuType3

                    ''項目４
                    grdSub.Rows(i).Cells("txtItem4").Value = .SubbytMenuType4

                    ''画面番号
                    grdSub.Rows(i).Cells("txtScreenNumber").Value = DataExchange(gGet2Byte(.ViewNo1))

                    ''キーコード
                    grdSub.Rows(i).Cells("txtKeyCode").Value = .bytKeyCode

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    '画面番号変更
    '--------------------------------------------------------------------
    Private Function DataExchange(ByVal ViewNo1 As Integer) As Integer

        Try

            Dim bytValue1, bytValue2 As Byte
            Dim bytArray(3) As Byte

            Call gSeparat2Byte(ViewNo1, bytValue1, bytValue2)

            bytArray(0) = bytValue2
            bytArray(1) = bytValue1

            DataExchange = BitConverter.ToInt32(bytArray, 0)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    '画面番号変更
    '--------------------------------------------------------------------
    Private Function DataExchange2(ByVal ViewNo1 As Integer) As Short

        Try

            Dim bytValue1, bytValue2 As Byte
            Dim bytArray(1) As Byte

            Call gSeparat2Byte(ViewNo1, bytValue1, bytValue2)

            bytArray(0) = bytValue2
            bytArray(1) = bytValue1

            DataExchange2 = BitConverter.ToInt16(bytArray, 0)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

End Class
