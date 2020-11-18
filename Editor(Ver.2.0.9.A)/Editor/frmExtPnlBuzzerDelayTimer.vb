Public Class frmExtPnlBuzzerDelayTimer

#Region "変数定義"

    Private mudtSetExtAlmSepNew() As gTypSetExtRec = Nothing

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
    Private Sub frmExtPnlBuzzerDelayTimer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''グリッドを初期化する
            Call mInitialDataGrid()

            ''配列再定義
            ReDim mudtSetExtAlmSepNew(UBound(gudt.SetExtAlarm.udtExtAlarm))
            For i = 0 To UBound(mudtSetExtAlmSepNew)
                mudtSetExtAlmSepNew(i).InitArray()
            Next

            ''画面設定
            Call mSetDisplay(gudt.SetExtAlarm.udtExtAlarm)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub frmExtPnlBuzzerDelayTimer_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        Try

            ''1行目LED1を選択色設定
            grdBZ.CurrentCell = grdBZ(1, 0)

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
            Call mSetStructure(mudtSetExtAlmSepNew)

            ''データが変更されているかチェック
            If Not mChkStructureEquals(mudtSetExtAlmSepNew, gudt.SetExtAlarm.udtExtAlarm) Then

                ''変更された場合は設定を更新する        
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
    Private Sub frmExtPnlBuzzerDelayTimer_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try

            ''グリッドの保留中の変更を全て適用させる
            'grdBZ.EndEdit()

            ''設定値を比較用構造体に格納
            Call mSetStructure(mudtSetExtAlmSepNew)

            ''データが変更されているかチェック
            If Not mChkStructureEquals(mudtSetExtAlmSepNew, gudt.SetExtAlarm.udtExtAlarm) Then

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
    Private Sub frmExtPnlBuzzerDelayTimer_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

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
    Private Sub grdBZ_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdBZ.EditingControlShowing

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdBZ_KeyPress

                ''イベントハンドラを追加する
                AddHandler tb.KeyPress, AddressOf grdBZ_KeyPress

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
    Private Sub grdBZ_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdBZ.KeyPress

        Try

            e.Handled = gCheckTextInput(4, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 入力チェック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdBZ_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles grdBZ.CellValidating

        Try

            ''------------------------------------------------------------
            '' 入力チェックは mChkInput で実施するよう変更
            ''------------------------------------------------------------

            'e.Cancel = gChkTextNumSpan(0, 1800, e.FormattedValue)

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

            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "txtAccommodation" : Column1.ReadOnly = True
            Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "txtLED1"
            Dim Column3 As New DataGridViewTextBoxColumn : Column3.Name = "txtLED2"
            Dim Column4 As New DataGridViewTextBoxColumn : Column4.Name = "txtLED3"
            Dim Column5 As New DataGridViewTextBoxColumn : Column5.Name = "txtLED4"
            Dim Column6 As New DataGridViewTextBoxColumn : Column6.Name = "txtLED5"
            Dim Column7 As New DataGridViewTextBoxColumn : Column7.Name = "txtLED6"
            Dim Column8 As New DataGridViewTextBoxColumn : Column8.Name = "txtLED7"
            Dim Column9 As New DataGridViewTextBoxColumn : Column9.Name = "txtLED8"
            Dim Column10 As New DataGridViewTextBoxColumn : Column10.Name = "txtLED9"
            Dim Column11 As New DataGridViewTextBoxColumn : Column11.Name = "txtLED10"
            Dim Column12 As New DataGridViewTextBoxColumn : Column12.Name = "txtLED11"
            Dim Column13 As New DataGridViewTextBoxColumn : Column13.Name = "txtLED12"

            Column2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column6.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column7.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column8.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column9.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column10.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column11.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column12.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column13.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            With grdBZ

                ''列
                .Columns.Clear()
                .Columns.Add(Column1) : .Columns.Add(Column2) : .Columns.Add(Column3)
                .Columns.Add(Column4) : .Columns.Add(Column5) : .Columns.Add(Column6)
                .Columns.Add(Column7) : .Columns.Add(Column8) : .Columns.Add(Column9)
                .Columns.Add(Column10) : .Columns.Add(Column11) : .Columns.Add(Column12)
                .Columns.Add(Column13)
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter    ''列ヘッダー　センタリング

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "Accomodation" : .Columns(0).Width = 210
                For i = 1 To .ColumnCount - 1                       ''「Accommodation」分、列カウントを-1する
                    '.Columns(i).HeaderText = "LED" & i.ToString     ''LED1～12の列名称
                    .Columns(i).HeaderText = "Grp" & i.ToString        '1～12の列名称
                    .Columns(i).Width = 50                          ''LED1～12の列幅設定
                Next

                ''行
                .RowCount = 20 + 1
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする

                ''行ヘッダー
                .TopLeftHeaderCell.Value = "EXT No"
                For i = 1 To .RowCount
                    .Rows(i - 1).HeaderCell.Value = i.ToString
                Next
                .RowHeadersWidth = 70
                .RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ''偶数行の背景色を変える
                cellStyle.BackColor = gColorGridRowBack
                For i = 0 To .Rows.Count - 1
                    If i Mod 2 <> 0 Then
                        .Rows(i).DefaultCellStyle = cellStyle
                    End If
                Next

                ''書込み不可列の背景色を変える
                For i = 0 To .RowCount - 1
                    .Rows(i).Cells("txtAccommodation").Style.BackColor = gColorGridRowBackReadOnly
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
                Call gSetGridCopyAndPaste(grdBZ)

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

            For i As Integer = 0 To grdBZ.RowCount - 1

                For j As Integer = 1 To grdBZ.ColumnCount - 1

                    ''共通数値入力チェック
                    If Not gChkInputNum(grdBZ.Rows(i).Cells("txtLED" & j), 0, 1800, "LED" & j, i + 1, True, True) Then Return False

                Next

            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return False
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) 延長警報盤個別設定構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet() As gTypSetExtRec)

        Try

            For i As Integer = 0 To UBound(udtSet)

                For j As Integer = 0 To UBound(udtSet(i).shtLedTimer)

                    udtSet(i).shtLedTimer(j) = CCShort(grdBZ(j + 1, i).Value)

                Next

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 延長警報盤個別設定構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal udtSet() As gTypSetExtRec)

        Try

            For i As Integer = 0 To UBound(udtSet)

                For j As Integer = 0 To UBound(udtSet(i).shtLedTimer)

                    ''Accommodation
                    grdBZ(0, i).Value = gGetString(udtSet(i).strPlace)

                    ''LED
                    grdBZ(j + 1, i).Value = udtSet(i).shtLedTimer(j)

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
    Private Sub mCopyStructure(ByVal udtSource() As gTypSetExtRec, _
                               ByRef udtTarget() As gTypSetExtRec)

        Try

            For i As Integer = 0 To UBound(udtSource)

                For j As Integer = 0 To UBound(udtSource(i).shtLedTimer)

                    udtTarget(i).shtLedTimer(j) = udtSource(i).shtLedTimer(j)

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
    Private Function mChkStructureEquals(ByVal udt1() As gTypSetExtRec, _
                                         ByVal udt2() As gTypSetExtRec) As Boolean

        Try

            For i As Integer = 0 To UBound(udt1)

                For j As Integer = 0 To UBound(udt1(i).shtLedTimer)

                    If udt1(i).shtLedTimer(j) <> udt2(i).shtLedTimer(j) Then Return False

                Next

            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return False
        End Try

    End Function

#End Region

End Class
