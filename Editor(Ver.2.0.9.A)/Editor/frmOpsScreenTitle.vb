Public Class frmOpsScreenTitle

#Region "変数定義"

    ''スクリーンタイトル構造体
    Private mudtSetScreenTitleNewWork As gTypSetOpsScreenTitle = Nothing
    Private mudtSetScreenTitleNewMach As gTypSetOpsScreenTitle = Nothing
    Private mudtSetScreenTitleNewCarg As gTypSetOpsScreenTitle = Nothing

    ''画面名称構造体
    Private mudtIniScreenTitle() As gTypCodeName = Nothing

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

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームロード
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmOpsScreenTitle_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''iniファイルからスクリーンタイトル取得
            If gGetComboCodeName(mudtIniScreenTitle, gEnmComboType.ctOpsScreenTitle) <> 0 Then

                ''取得出来なかった場合は画面を開かない
                Call Me.Close()

            Else

                ''初期化フラグ
                mblnInitFlg = True

                ''グリッドを初期化する
                Call mInitialDataGrid(mudtIniScreenTitle)



                ''配列再定義
                Call mudtSetScreenTitleNewWork.InitArray()
                Call mudtSetScreenTitleNewMach.InitArray()
                Call mudtSetScreenTitleNewCarg.InitArray()

                ''Machinery/Cargoの情報を取得する
                Call mCopyStructure(gudt.SetOpsScreenTitleM, mudtSetScreenTitleNewMach)
                Call mCopyStructure(gudt.SetOpsScreenTitleC, mudtSetScreenTitleNewCarg)

                ''Machinery/Cargoボタン設定
                Call gSetCombineControl(optMachinery, optCargo)

                ''画面設定
                If optMachinery.Checked Then Call mCopyStructure(mudtSetScreenTitleNewMach, mudtSetScreenTitleNewWork)
                If optCargo.Checked Then Call mCopyStructure(mudtSetScreenTitleNewCarg, mudtSetScreenTitleNewWork)
                Call mSetDisplay(mudtSetScreenTitleNewWork)

                ''初期化フラグ
                mblnInitFlg = False

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub frmOpsScreenTitle_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        Try

            ''初期化フラグ
            mblnInitFlg = True

            ''画面表示時の初期カーソル位置
            grdScreen.CurrentCell = Nothing

            ''初期化フラグ
            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Machineryボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : パートの切替え
    '--------------------------------------------------------------------
    Private Sub optMachinery_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optMachinery.CheckedChanged

        Try

            ''初期化中は処理しない
            If mblnInitFlg Then Return

            If optMachinery.Checked Then      ''Machinery選択

                ''設定値の取得
                Call mSetStructure(mudtSetScreenTitleNewWork)

                ''Cargo情報の退避
                Call mCopyStructure(mudtSetScreenTitleNewWork, mudtSetScreenTitleNewCarg)

                ''画面設定
                Call mSetDisplay(mudtSetScreenTitleNewMach)

            ElseIf optCargo.Checked Then    ''Cargo選択

                ''設定値の取得
                Call mSetStructure(mudtSetScreenTitleNewWork)

                ''Machinery情報の退避
                Call mCopyStructure(mudtSetScreenTitleNewWork, mudtSetScreenTitleNewMach)

                ''画面設定
                Call mSetDisplay(mudtSetScreenTitleNewCarg)

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

            Dim blnMach As Boolean = False
            Dim blnCarg As Boolean = False

            ''入力チェック
            If Not mChkInput() Then Return

            ''設定値を作業用構造体に格納
            Call mSetStructure(mudtSetScreenTitleNewWork)

            ''設定値の保存
            If optMachinery.Checked Then Call mCopyStructure(mudtSetScreenTitleNewWork, mudtSetScreenTitleNewMach)
            If optCargo.Checked Then Call mCopyStructure(mudtSetScreenTitleNewWork, mudtSetScreenTitleNewCarg)

            ''データが変更されているかチェック
            blnMach = mChkStructureEquals(mudtSetScreenTitleNewMach, gudt.SetOpsScreenTitleM)
            blnCarg = mChkStructureEquals(mudtSetScreenTitleNewCarg, gudt.SetOpsScreenTitleC)

            ''データが変更されている場合
            If (Not blnMach) Or (Not blnCarg) Then

                ''変更された場合は設定を更新する
                If Not blnMach Then Call mCopyStructure(mudtSetScreenTitleNewMach, gudt.SetOpsScreenTitleM)
                If Not blnCarg Then Call mCopyStructure(mudtSetScreenTitleNewCarg, gudt.SetOpsScreenTitleC)

                ''表示更新（入力禁止文字があった場合の表示確認）
                Call mSetDisplay(mudtSetScreenTitleNewWork)

                ''メッセージ表示
                Call MessageBox.Show("It saved.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                ''更新フラグ設定
                gblnUpdateAll = True
                If Not blnMach Then gudt.SetEditorUpdateInfo.udtSave.bytOpsScreenTitleM = 1
                If Not blnCarg Then gudt.SetEditorUpdateInfo.udtSave.bytOpsScreenTitleC = 1
                If Not blnMach Then gudt.SetEditorUpdateInfo.udtCompile.bytOpsScreenTitleM = 1
                If Not blnCarg Then gudt.SetEditorUpdateInfo.udtCompile.bytOpsScreenTitleC = 1

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

            Dim blnMach As Boolean = False
            Dim blnCarg As Boolean = False

            ''グリッドの保留中の変更を全て適用させる
            Call grdScreen.EndEdit()

            ''設定値を作業用構造体に格納
            Call mSetStructure(mudtSetScreenTitleNewWork)

            ''設定値の保存
            If optMachinery.Checked Then Call mCopyStructure(mudtSetScreenTitleNewWork, mudtSetScreenTitleNewMach)
            If optCargo.Checked Then Call mCopyStructure(mudtSetScreenTitleNewWork, mudtSetScreenTitleNewCarg)

            ''データが変更されているかチェック
            blnMach = mChkStructureEquals(mudtSetScreenTitleNewMach, gudt.SetOpsScreenTitleM)
            blnCarg = mChkStructureEquals(mudtSetScreenTitleNewCarg, gudt.SetOpsScreenTitleC)

            ''データが変更されている場合
            If (Not blnMach) Or (Not blnCarg) Then

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
                        If Not blnMach Then Call mCopyStructure(mudtSetScreenTitleNewMach, gudt.SetOpsScreenTitleM)
                        If Not blnCarg Then Call mCopyStructure(mudtSetScreenTitleNewCarg, gudt.SetOpsScreenTitleC)

                        ''更新フラグ設定
                        gblnUpdateAll = True
                        If Not blnMach Then gudt.SetEditorUpdateInfo.udtSave.bytOpsScreenTitleM = 1
                        If Not blnCarg Then gudt.SetEditorUpdateInfo.udtSave.bytOpsScreenTitleC = 1
                        If Not blnMach Then gudt.SetEditorUpdateInfo.udtCompile.bytOpsScreenTitleM = 1
                        If Not blnCarg Then gudt.SetEditorUpdateInfo.udtCompile.bytOpsScreenTitleC = 1

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
    ' 機能説明  ： KeyPressイベントを発生させる
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdScreen_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdScreen.EditingControlShowing

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdScreen_KeyPress

                ''イベントハンドラを追加する
                AddHandler tb.KeyPress, AddressOf grdScreen_KeyPress

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
    Private Sub grdScreen_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdScreen.KeyPress

        Try

            ''選択セルの名称取得
            Dim strColumnName As String = grdScreen.CurrentCell.OwningColumn.Name

            ''[SET_SCREEN_TITLE]
            If strColumnName = "txtScreenTitle" Then
                e.Handled = gCheckTextInput(30, sender, e.KeyChar, False)

                ''特別入力制限
                If e.KeyChar = "\"c Then e.Handled = True

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
    ' 引数      ： ARG1 - (I ) 画面タイトル構造体
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mInitialDataGrid(ByVal hudtScreenTitle() As gTypCodeName)

        Try

            Dim i As Integer
            Dim cellStyle As New DataGridViewCellStyle

            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "txtScreenName" : Column1.ReadOnly = True
            Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "txtScreenTitle"

            With grdScreen

                ''列
                .Columns.Clear()
                .Columns.Add(Column1) : .Columns.Add(Column2)
                .AllowUserToResizeColumns = False           ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "Default Screen" : .Columns(0).Width = 200
                .Columns(1).HeaderText = "Set Screen Title" : .Columns(1).Width = 300
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = UBound(hudtScreenTitle) + 2     ''行数
                .AllowUserToAddRows = False                 ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False              ''行の高さの変更不可
                .AllowUserToDeleteRows = False              ''行の削除を不可にする

                ''行ヘッダー
                .TopLeftHeaderCell.Value = "Screen No"
                For i = 0 To .RowCount - 1
                    .Rows(i).HeaderCell.Value = hudtScreenTitle(i).shtCode.ToString()
                Next
                .RowHeadersWidth = 75
                .RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ''偶数行の背景色を変える
                cellStyle.BackColor = gColorGridRowBack
                For i = 0 To .Rows.Count - 1
                    If i Mod 2 <> 0 Then
                        .Rows(i).DefaultCellStyle = cellStyle
                    End If
                Next

                ''画面設定
                For i = 0 To .RowCount - 1
                    .Rows(i).Cells("txtScreenName").Value = hudtScreenTitle(i).strName          ''名称設定
                    .Rows(i).Cells("txtScreenName").Style.BackColor = gColorGridRowBackReadOnly ''ReadOnly色設定
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
                Call gSetGridCopyAndPaste(grdScreen)

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

            ''入力制限（強制置換え）
            If grdScreen.Columns("txtScreenTitle").HeaderText = "Set Screen Title" Then

                For i = 0 To grdScreen.RowCount - 1

                    grdScreen.Rows(i).Cells("txtScreenTitle").Value = grdScreen.Rows(i).Cells("txtScreenTitle").Value.Replace("\", "")

                Next

            End If

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) 画面タイトル構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet As gTypSetOpsScreenTitle)

        Try

            For i As Integer = 0 To grdScreen.Rows.Count - 1

                With udtSet.udtOpsScreenTitle(i)

                    .bytScreenNo = i + 1
                    .strScreenName = gGetString(grdScreen.Rows(i).Cells("txtScreenTitle").Value)

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 画面タイトル構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal udtSet As gTypSetOpsScreenTitle)

        Try

            For i As Integer = 0 To grdScreen.Rows.Count - 1

                With udtSet.udtOpsScreenTitle(i)

                    grdScreen.Rows(i).Cells("txtScreenTitle").Value = gGetString(.strScreenName)

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
    Private Sub mCopyStructure(ByVal udtSource As gTypSetOpsScreenTitle, _
                               ByRef udtTarget As gTypSetOpsScreenTitle)

        Try

            For i As Integer = LBound(udtSource.udtOpsScreenTitle) To UBound(udtSource.udtOpsScreenTitle)

                udtTarget.udtOpsScreenTitle(i) = udtSource.udtOpsScreenTitle(i)

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 構造体比較
    ' 返り値    : True:相違なし、False:相違あり
    ' 引き数    : ARG1 - (I ) Mach構造体
    '           : ARG2 - (I ) Carg構造体
    ' 機能説明  : 構造体の設定値を比較する
    ' 備考　　  : 構造体メンバの中に構造体配列がいると Equals メソッドで正しい結果が得られないため関数を用意
    ' 　　　　  : 構造体メンバの中に構造体配列がいない場合は、 Equals メソッドで処理しても良いが一応これを使うこと
    ' 　　　　  : String文字列の比較には gCompareString を使用すること（単純な = だとNULL文字の有り無しで結果が変わってしまう）
    '--------------------------------------------------------------------
    Private Function mChkStructureEquals(ByVal udt1 As gTypSetOpsScreenTitle, _
                                         ByVal udt2 As gTypSetOpsScreenTitle) As Boolean

        Try

            For i As Integer = LBound(udt1.udtOpsScreenTitle) To UBound(udt1.udtOpsScreenTitle)

                If Not gCompareString((udt1.udtOpsScreenTitle(i).strScreenName), _
                                      (udt2.udtOpsScreenTitle(i).strScreenName)) Then Return False

            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region


End Class
