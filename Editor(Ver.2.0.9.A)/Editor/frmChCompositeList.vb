Public Class frmChCompositeList

#Region "変数定義"

    Private mudtSetChCompositeNew As gTypSetChComposite

    ''初期化フラグ
    Private mblnInitFlg As Boolean

    ''SELECTフラグ
    Private mblnSelectFlg As Boolean

    ''選択モード
    Private mblnSelectMode As Boolean

    ''CH No.
    Private mintChNo As Integer

    ''選択テーブル番号
    Private mintSelectTableNo As Integer

    ''自動編集フラグ
    Private mblnAutoEdit As Boolean

    ''コンポジットテーブル使用フラグ
    ''既に使用CH設定済みのテーブル + 一時保存のテーブルのテーブル使用状況
    ''（ChList - ChCompositeSet - CompositeList - CompositeDetail 間で使用する）
    Private mblnCompositeTableUse() As Boolean

    Private mudtValveDetail As frmChListChannelList.mValveInfo
    Private mudtCompositeDetail As frmChListChannelList.mCompositeInfo
    Private mudtCompositeEditType As gEnmCompositeEditType

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 1:OK  0:キャンセル
    ' 引き数    : ARG1 - (I ) 選択モード
    ' 　　　    : ARG2 - (I ) CH No.
    ' 　　　    : ARG3 - (IO) 選択テーブル番号
    ' 機能説明  : 本画面を表示する
    ' 備考      : ARG3 は ARG1 を True にした場合に指定する
    '           : 
    '           : ARG3 の IN  : 新規なら0、更新なら現在設定中のテーブル番号を渡す
    '           : ARG3 の OUT : 画面から選択されたテーブル番号
    '--------------------------------------------------------------------
    Friend Function gShow1(ByVal blnSelectMode As Boolean, _
                           ByVal intChNo As Integer, _
                           ByRef intSelectTableNo As Integer, _
                           ByVal udtCompositeEditType As gEnmCompositeEditType) As Integer

        Try

            ''引数保存
            mblnSelectMode = blnSelectMode
            mintChNo = intChNo
            mintSelectTableNo = intSelectTableNo
            mudtCompositeEditType = udtCompositeEditType

            ''自動編集フラグ
            mblnAutoEdit = False

            ''本画面表示
            Call gShowFormModelessForCloseWait1(Me)

            ''戻り値設定
            intSelectTableNo = mintSelectTableNo

            If mblnSelectFlg Then
                Return 1
            Else
                Return 0
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    Friend Function gShow2(ByVal blnSelectMode As Boolean, _
                           ByVal intChNo As Integer, _
                           ByRef intSelectTableNo As Integer, _
                           ByRef frmOwner As Form, _
                           ByVal udtCompositeEditType As gEnmCompositeEditType) As Integer

        Try

            ''引数保存
            mblnSelectMode = blnSelectMode
            mintChNo = intChNo
            mintSelectTableNo = intSelectTableNo
            mudtCompositeEditType = udtCompositeEditType

            ''自動編集フラグ
            mblnAutoEdit = False

            ''本画面表示
            Call gShowFormModelessForCloseWait2(Me, frmOwner)

            ''戻り値設定
            intSelectTableNo = mintSelectTableNo

            If mblnSelectFlg Then
                Return 1
            Else
                Return 0
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    Friend Function gShowEdit(ByVal blnSelectMode As Boolean, _
                              ByVal intChNo As Integer, _
                              ByRef intSelectTableNo As Integer, _
                              ByRef udtValveDetail As frmChListChannelList.mValveInfo, _
                              ByRef blnCompositeTableUse() As Boolean, _
                              ByRef frmOwner As Form, _
                              ByVal udtCompositeEditType As gEnmCompositeEditType) As Integer

        Try

            ''引数保存
            mblnSelectMode = blnSelectMode
            mintChNo = intChNo
            mintSelectTableNo = intSelectTableNo
            mudtValveDetail = udtValveDetail
            mblnCompositeTableUse = blnCompositeTableUse
            mudtCompositeEditType = udtCompositeEditType

            'Ver2.0.4.1 ひとまず自動化をやめる
            ''自動編集フラグ
            'mblnAutoEdit = True
            mblnAutoEdit = False

            ''本画面表示
            Call gShowFormModelessForCloseWait2(Me, frmOwner)

            ''戻り値設定
            blnCompositeTableUse = mblnCompositeTableUse
            udtValveDetail = mudtValveDetail
            intSelectTableNo = mintSelectTableNo

            If mblnSelectFlg Then
                Return 1
            Else
                Return 0
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    Friend Function gShowEdit(ByVal blnSelectMode As Boolean, _
                              ByVal intChNo As Integer, _
                              ByRef intSelectTableNo As Integer, _
                              ByRef udtCompositeDetail As frmChListChannelList.mCompositeInfo, _
                              ByRef blnCompositeTableUse() As Boolean, _
                              ByRef frmOwner As Form, _
                              ByVal udtCompositeEditType As gEnmCompositeEditType) As Integer

        Try

            ''引数保存
            mblnSelectMode = blnSelectMode
            mintChNo = intChNo
            mintSelectTableNo = intSelectTableNo
            mudtCompositeDetail = udtCompositeDetail
            mblnCompositeTableUse = blnCompositeTableUse
            mudtCompositeEditType = udtCompositeEditType

            'Ver2.0.7.J
            '自動化やめる
            ''自動編集フラグ
            'mblnAutoEdit = True
            mblnAutoEdit = False

            ''本画面表示
            Call gShowFormModelessForCloseWait2(Me, frmOwner)

            ''戻り値設定
            blnCompositeTableUse = mblnCompositeTableUse
            udtCompositeDetail = mudtCompositeDetail
            intSelectTableNo = mintSelectTableNo

            If mblnSelectFlg Then
                Return 1
            Else
                Return 0
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "画面イベント"

    '--------------------------------------------------------------------
    ' 機能      : フォームロード
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 画面表示初期処理を行う
    '--------------------------------------------------------------------
    Private Sub frmChCompositeList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            Dim intRow As Integer = -1
            'Dim blnSelectedFlg As Boolean = False

            ''初期化フラグ
            mblnInitFlg = True

            ''Selectフラグ
            mblnSelectFlg = False

            ''選択モードの場合はSelectボタン表示
            cmdSelect.Visible = IIf(mblnSelectMode, True, False)

            ''グリッドの初期化
            Call mInitialDataGrid()
            Call gCompInitControl(grdBitStatusMap, grdAnyMap, txtFilterCoeficient, False)

            ''配列再定義
            Call mudtSetChCompositeNew.InitArray()
            For i As Integer = 0 To UBound(mudtSetChCompositeNew.udtComposite)
                mudtSetChCompositeNew.udtComposite(i).InitArray()
            Next

            ''構造体のコピー
            Call mCopyStructure(gudt.SetChComposite, mudtSetChCompositeNew)

            ''画面設定
            Call mSetDisplay(gudt.SetChComposite)

            ''初期選択状態
            If mintSelectTableNo = 0 Then

                ''自動編集モードの場合
                If mblnAutoEdit Then

                    ''使用していないテーブルを探す
                    For i As Integer = 0 To UBound(mblnCompositeTableUse)
                        If Not mblnCompositeTableUse(i) Then
                            intRow = i
                            Exit For
                        End If
                    Next

                    ''使用していないテーブルが見つかった場合は選択する
                    If intRow <> -1 Then
                        grdComposite.CurrentCell = grdComposite.Rows(intRow).Cells(0)
                        grdComposite.Rows(intRow).Cells(0).Selected = True
                    Else

                        '-------------------------------------------------------------------
                        ''使用していないテーブルが見つからない場合（６４テーブル全て使用）
                        '-------------------------------------------------------------------
                        ''メッセージを表示して処理を抜ける
                        Call MessageBox.Show("No composite table is available. All the 64 tables are in use now.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        mblnSelectFlg = False
                        Me.Close()
                        Return

                    End If

                Else

                    ''先頭行を初期選択する
                    grdComposite.CurrentCell = grdComposite.Rows(0).Cells(0)
                    grdComposite.Rows(0).Cells(0).Selected = True

                End If

            Else

                ''選択済みの行を初期選択する
                grdComposite.CurrentCell = grdComposite.Rows(mintSelectTableNo - 1).Cells(0)
                grdComposite.Rows(mintSelectTableNo - 1).Cells(0).Selected = True

            End If

            ''設定表示
            Call gCompSetDisplay(mudtSetChCompositeNew.udtComposite(grdComposite.CurrentCell.RowIndex), _
                                 grdBitStatusMap, _
                                 grdAnyMap, _
                                 txtFilterCoeficient)

            ''初期化フラグ
            mblnInitFlg = False

            ''自動編集モードの場合は編集画面を開く
            If mblnAutoEdit Then
                Call cmdEdit_Click(cmdEdit, New EventArgs)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        'Try

        '    Dim intRow As Integer = 0
        '    Dim blnSelectedFlg As Boolean = False

        '    ''初期化フラグ
        '    mblnInitFlg = True

        '    ''Selectフラグ
        '    mblnSelectFlg = False

        '    ''選択モードの場合はSelectボタン表示
        '    cmdSelect.Visible = IIf(mblnSelectMode, True, False)

        '    ''グリッドの初期化
        '    Call mInitialDataGrid()
        '    Call gCompInitControl(grdBitStatusMap, grdAnyMap, txtFilterCoeficient, False)

        '    ''配列再定義
        '    Call mudtSetChCompositeNew.InitArray()
        '    For i As Integer = 0 To UBound(mudtSetChCompositeNew.udtComposite)
        '        mudtSetChCompositeNew.udtComposite(i).InitArray()
        '    Next

        '    ''構造体のコピー
        '    Call mCopyStructure(gudt.SetChComposite, mudtSetChCompositeNew)

        '    ''画面設定
        '    Call mSetDisplay(gudt.SetChComposite)

        '    ''初期選択状態
        '    If mintSelectTableNo = 0 Then

        '        ''選択モードの場合
        '        If mblnSelectMode Then

        '            ''新規の時は空いている行を初期選択する
        '            For i As Integer = 0 To grdComposite.RowCount - 1
        '                If grdComposite(0, i).Value = "" Then
        '                    intRow = i
        '                    Exit For
        '                End If
        '            Next
        '            grdComposite.CurrentCell = grdComposite.Rows(intRow).Cells(0)
        '            grdComposite.Rows(intRow).Cells(0).Selected = True
        '        Else
        '            ''先頭行を初期選択する
        '            grdComposite.CurrentCell = grdComposite.Rows(0).Cells(0)
        '            grdComposite.Rows(0).Cells(0).Selected = True
        '        End If
        '    Else
        '        ''選択済みの行を初期選択する
        '        grdComposite.CurrentCell = grdComposite.Rows(mintSelectTableNo - 1).Cells(0)
        '        grdComposite.Rows(mintSelectTableNo - 1).Cells(0).Selected = True
        '        blnSelectedFlg = True
        '    End If

        '    ''設定表示
        '    Call gCompSetDisplay(mudtSetChCompositeNew.udtComposite(grdComposite.CurrentCell.RowIndex), _
        '                         grdBitStatusMap, _
        '                         grdAnyMap, _
        '                         txtFilterCoeficient)

        '    ''初期化フラグ
        '    mblnInitFlg = False

        '    ''自動編集モードの場合
        '    If mblnAutoEdit Then

        '        ''コンポジットテーブル番号が設定されている場合
        '        If mintSelectTableNo <> 0 Then

        '            If blnSelectedFlg Then Call cmdEdit_Click(cmdEdit, New EventArgs)

        '            ' ''空きテーブル番号を探して選択する
        '            'For i As Integer = 0 To grdComposite.RowCount - 1

        '            '    If Trim(grdComposite.Rows(i).Cells("txtChNo").Value) = "" Then
        '            '        grdComposite.CurrentCell = grdComposite.Rows(i).Cells(0)
        '            '        grdComposite.Rows(i).Cells(0).Selected = True
        '            '        blnSelectedFlg = True
        '            '        Exit For
        '            '    End If

        '            'Next

        '        End If

        '        ' ''対象のテーブルが選択できた場合は編集画面を開く
        '        'If blnSelectedFlg Then Call cmdEdit_Click(cmdEdit, New EventArgs)

        '    End If

        'Catch ex As Exception
        '    Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        'End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Selectボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし 
    '----------------------------------------------------------------------------
    Private Sub cmdSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelect.Click

        ''選択されていない場合は処理を抜ける
        If grdComposite.SelectedCells.Count <= 0 Then Return

        ''他のＣＨで割付られている場合は選択不可
        If grdComposite.SelectedCells(0).Value = "" Then
            ''OK
        Else
            If CCInt(grdComposite.SelectedCells(0).Value) = mintChNo Then
                ''OK
            Else
                'Ver2.0.7.J 他のCHで割り付けられてもOKにするために、Returnをコメントアウト
                'Return
            End If
        End If

        mintSelectTableNo = grdComposite.SelectedCells(0).RowIndex + 1

        mblnSelectFlg = True

        ''画面を閉じる
        Call Me.Close()

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Editボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし 
    '----------------------------------------------------------------------------
    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click

        Try
            Dim intRtn As Integer

            ''処理を抜ける条件
            If grdComposite.CurrentCell.RowIndex < 0 Or _
               grdComposite.CurrentCell.RowIndex > grdComposite.RowCount - 1 Then Return ''行数が0より小さい、もしくは最大行数より大きい場合

            ''カーソルのある行Indexの取得
            Dim intRowIndex = grdComposite.CurrentCell.RowIndex

            ''詳細画面の表示処理
            Select Case mudtCompositeEditType
                Case gEnmCompositeEditType.cetNone
                    intRtn = frmChCompositeDetail.gShow(mudtSetChCompositeNew.udtComposite(intRowIndex), Me, gEnmCompositeEditType.cetNone)
                Case gEnmCompositeEditType.cetValve
                    intRtn = frmChCompositeDetail.gShow(mudtSetChCompositeNew.udtComposite(intRowIndex), Me, mudtValveDetail, gEnmCompositeEditType.cetValve)
                Case gEnmCompositeEditType.cetComposite
                    intRtn = frmChCompositeDetail.gShow(mudtSetChCompositeNew.udtComposite(intRowIndex), Me, mudtCompositeDetail, gEnmCompositeEditType.cetComposite)
            End Select

            ''変更を保存した場合
            If intRtn = 1 Then

                ''設定表示
                Call gCompSetDisplay(mudtSetChCompositeNew.udtComposite(intRowIndex), _
                                     grdBitStatusMap, _
                                     grdAnyMap, _
                                     txtFilterCoeficient)

                ''自動編集モードの場合
                If mblnAutoEdit Then

                    ''コンポジットテーブル使用フラグの該当テーブルを「使用」にする
                    mblnCompositeTableUse(intRowIndex) = True

                    ''自動で保存する
                    Call cmdSave_Click(cmdSave, New EventArgs)

                    ''自動で画面を閉じる
                    Call cmdSelect_Click(cmdSelect, New EventArgs)

                End If

            Else

                ''自動編集モードの場合
                If mblnAutoEdit Then

                    ''自動で画面を閉じる
                    Call cmdExit_Click(cmdExit, New EventArgs)

                End If

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

            ''データが変更されているかチェック
            If Not mChkStructureEquals(gudt.SetChComposite, mudtSetChCompositeNew) Then

                ''変更された場合は設定を更新する
                Call mCopyStructure(mudtSetChCompositeNew, gudt.SetChComposite)

                ''自動編集モードの場合
                If mblnAutoEdit Then

                    ''メッセージを表示しない

                Else

                    ''メッセージを表示する
                    Call MessageBox.Show("It saved.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                End If


                ''更新フラグ設定
                gblnUpdateAll = True
                gudt.SetEditorUpdateInfo.udtSave.bytComposite = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytComposite = 1

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

            ''データが変更されているかチェック
            If Not mChkStructureEquals(gudt.SetChComposite, mudtSetChCompositeNew) Then

                ''変更されている場合はメッセージ表示
                Select Case MessageBox.Show("Setting has been changed." & vbNewLine & _
                                            "Do you save the changes?", Me.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                    Case Windows.Forms.DialogResult.Yes

                        ' ''入力チェック
                        'If Not mChkInput() Then
                        '    e.Cancel = True
                        '    Return
                        'End If

                        ''変更されている場合は設定を更新する
                        Call mCopyStructure(mudtSetChCompositeNew, gudt.SetChComposite)

                        ''更新フラグ設定
                        gblnUpdateAll = True
                        gudt.SetEditorUpdateInfo.udtSave.bytComposite = 1
                        gudt.SetEditorUpdateInfo.udtCompile.bytComposite = 1

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
    ' 機能説明  ： グリッドダブルクリック
    ' 引数      ： なし
    ' 戻値      ： なし 
    '----------------------------------------------------------------------------
    Private Sub grdcomposite_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdComposite.CellDoubleClick

        Try

            ''グリッドの保留中の変更を全て適用させる
            grdComposite.EndEdit()

            ''処理を抜ける条件
            If e.RowIndex < 0 Or e.RowIndex > grdComposite.RowCount - 1 Then Return ''行数が0より小さい、もしくは最大行数より大きい場合
            If gChkCellIsCmb(grdComposite.CurrentCell.OwningColumn.Name) Then Return ''コンボボックス列の場合は処理を抜ける

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
    Private Sub grdcomposite_RowHeaderMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdComposite.RowHeaderMouseDoubleClick

        Try

            ''グリッドの保留中の変更を全て適用させる
            grdComposite.EndEdit()

            ''行数が0より小さい、もしくは最大行数より大きい場合処理を抜ける
            If e.RowIndex < 0 Or e.RowIndex > grdComposite.RowCount - 1 Then Return

            ''Editボタンのクリックイベントを呼び出す
            Call cmdEdit_Click(cmdEdit, New EventArgs)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッド行選択時に詳細を表示する
    ' 引数      ： なし
    ' 戻値      ： なし 
    '----------------------------------------------------------------------------
    Private Sub grdComposite_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdComposite.CellEnter

        ''初期化中は何もしない
        If mblnInitFlg Then Return

        ''選択行の詳細を表示する
        Call gCompSetDisplay(mudtSetChCompositeNew.udtComposite(e.RowIndex), grdBitStatusMap, grdAnyMap, txtFilterCoeficient)

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

            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "txtChNo" : Column1.ReadOnly = True
            Column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            With grdComposite

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
                .Columns(0).HeaderText = "CH No." : .Columns(0).Width = 52
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing
                .ColumnHeadersHeight = .ColumnHeadersHeight + 4
                .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

                ''行
                .RowCount = 64 + 1
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする

                ''行ヘッダー
                .RowHeadersWidth = 42
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
                .ScrollBars = ScrollBars.Vertical

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdComposite)

                .MultiSelect = False

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
            Call grdComposite.EndEdit()

            ' ''入力値のレンジチェック
            'If Not mChkInputData() Then Return False

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    Private Function mChkInputData() As Boolean

        Try

            For i = 0 To grdComposite.RowCount - 1

                If Not gChkInputNum(grdComposite.Rows(i).Cells("txtChNo"), 0, 65535, "CH No.", i + 1, True, True) Then
                    Return False
                End If

            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) リポーズ入力設定構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal udtSet As gTypSetChComposite)

        Try

            ''コンポジットリストグリッド
            For i As Integer = 0 To UBound(udtSet.udtComposite)

                grdComposite.Rows(i).Cells("txtChNo").Value = gConvZeroToNull(udtSet.udtComposite(i).shtChid, "0000")

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
    Private Sub mCopyStructure(ByVal udtSource As gTypSetChComposite, _
                               ByRef udtTarget As gTypSetChComposite)

        Try

            For i As Integer = 0 To UBound(udtSource.udtComposite)

                udtTarget.udtComposite(i).shtChid = udtSource.udtComposite(i).shtChid             ''CH ID
                udtTarget.udtComposite(i).shtDiFilter = udtSource.udtComposite(i).shtDiFilter     ''DI Filter

                '---------------------------
                ' 詳細画面
                '---------------------------
                For j As Integer = 0 To UBound(udtSource.udtComposite(i).udtCompInf)
                    udtTarget.udtComposite(i).udtCompInf(j).bytBitPattern = udtSource.udtComposite(i).udtCompInf(j).bytBitPattern   ''ステータスビットパターン
                    udtTarget.udtComposite(i).udtCompInf(j).bytAlarmUse = udtSource.udtComposite(i).udtCompInf(j).bytAlarmUse       ''ステータスビット仕様有無
                    udtTarget.udtComposite(i).udtCompInf(j).bytDelay = udtSource.udtComposite(i).udtCompInf(j).bytDelay             ''ディレィタイマ値(ｱﾗｰﾑ継続時間)
                    udtTarget.udtComposite(i).udtCompInf(j).bytExtGroup = udtSource.udtComposite(i).udtCompInf(j).bytExtGroup       ''EXT. グループ(延長警報ｸﾞﾙｰﾌﾟ)
                    udtTarget.udtComposite(i).udtCompInf(j).bytGRepose1 = udtSource.udtComposite(i).udtCompInf(j).bytGRepose1       ''グループ・リポーズ１
                    udtTarget.udtComposite(i).udtCompInf(j).bytGRepose2 = udtSource.udtComposite(i).udtCompInf(j).bytGRepose2       ''グループ・リポーズ２
                    udtTarget.udtComposite(i).udtCompInf(j).strStatusName = udtSource.udtComposite(i).udtCompInf(j).strStatusName   ''ステータス名称
                    udtTarget.udtComposite(i).udtCompInf(j).bytManualReposeState = udtSource.udtComposite(i).udtCompInf(j).bytManualReposeState ''マニュアルリポーズ状態
                    udtTarget.udtComposite(i).udtCompInf(j).bytManualReposeSet = udtSource.udtComposite(i).udtCompInf(j).bytManualReposeSet     ''マニュアルリポーズ設定
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
    Private Function mChkStructureEquals(ByVal udt1 As gTypSetChComposite, _
                                         ByVal udt2 As gTypSetChComposite) As Boolean

        Try

            For i As Integer = 0 To UBound(udt1.udtComposite)

                If udt1.udtComposite(i).shtChid <> udt2.udtComposite(i).shtChid Then Return False ''CH ID
                If udt1.udtComposite(i).shtDiFilter <> udt2.udtComposite(i).shtDiFilter Then Return False ''Di Filter

                '---------------------------
                ' 詳細画面
                '---------------------------
                For j As Integer = 0 To UBound(udt1.udtComposite(i).udtCompInf)
                    If udt1.udtComposite(i).udtCompInf(j).bytBitPattern <> udt2.udtComposite(i).udtCompInf(j).bytBitPattern Then Return False ''ステータスビットパターン
                    If udt1.udtComposite(i).udtCompInf(j).bytAlarmUse <> udt2.udtComposite(i).udtCompInf(j).bytAlarmUse Then Return False ''ステータスビット仕様有無
                    If udt1.udtComposite(i).udtCompInf(j).bytDelay <> udt2.udtComposite(i).udtCompInf(j).bytDelay Then Return False ''ディレィタイマ値(ｱﾗｰﾑ継続時間)
                    If udt1.udtComposite(i).udtCompInf(j).bytExtGroup <> udt2.udtComposite(i).udtCompInf(j).bytExtGroup Then Return False ''EXT. グループ(延長警報ｸﾞﾙｰﾌﾟ)
                    If udt1.udtComposite(i).udtCompInf(j).bytGRepose1 <> udt2.udtComposite(i).udtCompInf(j).bytGRepose1 Then Return False ''グループ・リポーズ１
                    If udt1.udtComposite(i).udtCompInf(j).bytGRepose2 <> udt2.udtComposite(i).udtCompInf(j).bytGRepose2 Then Return False ''グループ・リポーズ２
                    If udt1.udtComposite(i).udtCompInf(j).strStatusName <> udt2.udtComposite(i).udtCompInf(j).strStatusName Then Return False ''ステータス名称
                    If udt1.udtComposite(i).udtCompInf(j).bytManualReposeState <> udt2.udtComposite(i).udtCompInf(j).bytManualReposeState Then Return False ''マニュアルリポーズ状態
                    If udt1.udtComposite(i).udtCompInf(j).bytManualReposeSet <> udt2.udtComposite(i).udtCompInf(j).bytManualReposeSet Then Return False ''マニュアルリポーズ設定
                Next

            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

End Class