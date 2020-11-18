Public Class frmChControlUseNotuseList

#Region "変数定義"

    ''初期化フラグ
    Private mblnInitFlg As Boolean

    ''構造体定義
    Private mudtSetCtrlUseNotuseWork As gTypSetChCtrlUse = Nothing
    Private mudtSetCtrlUseNotuseNewMach As gTypSetChCtrlUse = Nothing
    Private mudtSetCtrlUseNotuseNewCarg As gTypSetChCtrlUse = Nothing

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
    Private Sub frmChControlUseNotuseList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            'Ver2.0.4.1
            'クリップボードとﾌﾗｸﾞをｸﾘｱする。
            Clipboard.Clear()
            blnAllRowFlg = False

            ''初期化フラグ
            mblnInitFlg = True

            ''グリッドの初期化
            Call mInitialDataGrid()

            ''配列再定義
            Call mudtSetCtrlUseNotuseWork.InitArray()
            Call mudtSetCtrlUseNotuseNewMach.InitArray()
            Call mudtSetCtrlUseNotuseNewCarg.InitArray()
            For i As Integer = 0 To UBound(gudt.SetChCtrlUseM.udtCtrlUseNotuseRec)
                Call mudtSetCtrlUseNotuseWork.udtCtrlUseNotuseRec(i).InitArray()
                Call mudtSetCtrlUseNotuseNewMach.udtCtrlUseNotuseRec(i).InitArray()
                Call mudtSetCtrlUseNotuseNewCarg.udtCtrlUseNotuseRec(i).InitArray()
            Next

            ''Machinery/Cargoの情報を取得する
            Call mCopyStructure(gudt.SetChCtrlUseM, mudtSetCtrlUseNotuseNewMach)
            Call mCopyStructure(gudt.SetChCtrlUseC, mudtSetCtrlUseNotuseNewCarg)

            ''Machinery/Cargoボタン設定
            Call gSetCombineControl(optMachinery, optCargo)

            ''画面設定
            If optMachinery.Checked Then Call mCopyStructure(mudtSetCtrlUseNotuseNewMach, mudtSetCtrlUseNotuseWork)
            If optCargo.Checked Then Call mCopyStructure(mudtSetCtrlUseNotuseNewCarg, mudtSetCtrlUseNotuseWork)
            Call mSetDisplay(mudtSetCtrlUseNotuseWork)

            ''初期化フラグ
            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub frmChControlUseNotuseList_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        Try

            ''初期化フラグ
            mblnInitFlg = True

            ''画面表示時のセル選択を解除
            grdUse.CurrentCell = Nothing

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

            If optMachinery.Checked Then

                ''作業用構造体にMachinery情報を設定
                Call mCopyStructure(mudtSetCtrlUseNotuseNewMach, mudtSetCtrlUseNotuseWork)

                ''画面設定
                Call mSetDisplay(mudtSetCtrlUseNotuseWork)

            ElseIf optCargo.Checked Then

                ''作業用構造体にCargo情報を設定
                Call mCopyStructure(mudtSetCtrlUseNotuseNewCarg, mudtSetCtrlUseNotuseWork)

                ''画面設定
                Call mSetDisplay(mudtSetCtrlUseNotuseWork)

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

            ''設定値を比較用構造体に格納
            '　→グリッドイベント実行時に情報保存済

            ''データが変更されているかチェック
            blnMach = mChkStructureEquals(mudtSetCtrlUseNotuseNewMach, gudt.SetChCtrlUseM)
            blnCarg = mChkStructureEquals(mudtSetCtrlUseNotuseNewCarg, gudt.SetChCtrlUseC)

            ''データが変更されている場合
            If (Not blnMach) Or (Not blnCarg) Then

                ''変更された場合は設定を更新する
                If Not blnMach Then Call mCopyStructure(mudtSetCtrlUseNotuseNewMach, gudt.SetChCtrlUseM)
                If Not blnCarg Then Call mCopyStructure(mudtSetCtrlUseNotuseNewCarg, gudt.SetChCtrlUseC)

                ''メッセージ表示
                Call MessageBox.Show("It saved.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                ''更新フラグ設定
                gblnUpdateAll = True
                If Not blnMach Then gudt.SetEditorUpdateInfo.udtSave.bytCtrlUseNotuseM = 1
                If Not blnCarg Then gudt.SetEditorUpdateInfo.udtSave.bytCtrlUseNotuseC = 1
                If Not blnMach Then gudt.SetEditorUpdateInfo.udtCompile.bytCtrlUseNotuseM = 1
                If Not blnCarg Then gudt.SetEditorUpdateInfo.udtCompile.bytCtrlUseNotuseC = 1

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
    Private Sub frmChControlUseNotuseList_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try

            Dim blnMach As Boolean = False
            Dim blnCarg As Boolean = False

            ''グリッドの保留中の変更を全て適用させる
            Call grdUse.EndEdit()

            ''設定値を比較用構造体に格納
            '　→グリッドイベント実行時に情報保存済

            ''データが変更されているかチェック
            blnMach = mChkStructureEquals(mudtSetCtrlUseNotuseNewMach, gudt.SetChCtrlUseM)
            blnCarg = mChkStructureEquals(mudtSetCtrlUseNotuseNewCarg, gudt.SetChCtrlUseC)

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
                        If Not blnMach Then Call mCopyStructure(mudtSetCtrlUseNotuseNewMach, gudt.SetChCtrlUseM)
                        If Not blnCarg Then Call mCopyStructure(mudtSetCtrlUseNotuseNewCarg, gudt.SetChCtrlUseC)

                        ''更新フラグ設定
                        gblnUpdateAll = True
                        If Not blnMach Then gudt.SetEditorUpdateInfo.udtSave.bytCtrlUseNotuseM = 1
                        If Not blnCarg Then gudt.SetEditorUpdateInfo.udtSave.bytCtrlUseNotuseC = 1
                        If Not blnMach Then gudt.SetEditorUpdateInfo.udtCompile.bytCtrlUseNotuseM = 1
                        If Not blnCarg Then gudt.SetEditorUpdateInfo.udtCompile.bytCtrlUseNotuseC = 1

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
    Private Sub frmChControlUseNotuseList_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub





#Region "グリッドイベント"

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッドダブルクリック時
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdUse_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdUse.CellDoubleClick

        Try

            ''処理を抜ける条件
            If e.RowIndex < 0 Or e.RowIndex > grdUse.RowCount - 1 Then Return ''行数が0より小さい、もしくは最大行数より大きい場合
            If e.ColumnIndex < 0 Or e.ColumnIndex > grdUse.ColumnCount - 1 Then Return ''列数が0より小さい、もしくは最大列数より大きい場合

            ''詳細画面の表示処理
            If frmChControlUseNotuseDetail.gShow(mudtSetCtrlUseNotuseWork.udtCtrlUseNotuseRec(e.RowIndex), e.RowIndex, Me) = 0 Then

                ''設定値を比較用構造体に格納
                Call mSetStructureInfo()

                ''画面表示
                Call mSetDisplayOne(mudtSetCtrlUseNotuseWork, e.RowIndex)

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    'KeyDown
    Public intPB_Row As Integer = -1
    Private Sub grdUse_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles grdUse.KeyDown
        Try
            'Ver2.0.4.1 行のコポペ
            Dim i As Integer = 0
            Dim z As Integer = 0

            If (e.Modifiers And Keys.Control) = Keys.Control And e.KeyCode = Keys.C Then
                'CTRL+C コポー
                With grdUse
                    If .SelectedCells(0).RowIndex >= 0 Then
                        intPB_Row = .SelectedCells(0).RowIndex
                    End If
                End With
            ElseIf (e.Modifiers And Keys.Control) = Keys.Control And e.KeyCode = Keys.V Then
                'CTRL+V ペースト
                If intPB_Row > 0 Then
                    '変数へ値格納(詳細を含めて)
                    With mudtSetCtrlUseNotuseWork
                        .udtCtrlUseNotuseRec(grdUse.SelectedCells(0).RowIndex).shtCount = .udtCtrlUseNotuseRec(intPB_Row).shtCount
                        .udtCtrlUseNotuseRec(grdUse.SelectedCells(0).RowIndex).bytFlg = .udtCtrlUseNotuseRec(intPB_Row).bytFlg
                        For i = 0 To UBound(.udtCtrlUseNotuseRec(intPB_Row).udtUseNotuseDetails)
                            .udtCtrlUseNotuseRec(grdUse.SelectedCells(0).RowIndex).udtUseNotuseDetails(i).shtChno = .udtCtrlUseNotuseRec(intPB_Row).udtUseNotuseDetails(i).shtChno
                            .udtCtrlUseNotuseRec(grdUse.SelectedCells(0).RowIndex).udtUseNotuseDetails(i).bytType = .udtCtrlUseNotuseRec(intPB_Row).udtUseNotuseDetails(i).bytType
                            .udtCtrlUseNotuseRec(grdUse.SelectedCells(0).RowIndex).udtUseNotuseDetails(i).shtBit = .udtCtrlUseNotuseRec(intPB_Row).udtUseNotuseDetails(i).shtBit
                            .udtCtrlUseNotuseRec(grdUse.SelectedCells(0).RowIndex).udtUseNotuseDetails(i).shtProcess1 = .udtCtrlUseNotuseRec(intPB_Row).udtUseNotuseDetails(i).shtProcess1
                            .udtCtrlUseNotuseRec(grdUse.SelectedCells(0).RowIndex).udtUseNotuseDetails(i).shtProcess2 = .udtCtrlUseNotuseRec(intPB_Row).udtUseNotuseDetails(i).shtProcess2
                        Next i
                    End With
                    'グリッドへ値格納
                    With grdUse
                        '項目番号
                        .Rows(.SelectedCells(0).RowIndex).Cells(0).Value = gConvZeroToNull(mudtSetCtrlUseNotuseWork.udtCtrlUseNotuseRec(intPB_Row).shtCount)

                        ''条件数が 0 の場合は条件名称を表示しない
                        If mudtSetCtrlUseNotuseWork.udtCtrlUseNotuseRec(intPB_Row).shtCount <> 0 Then

                            ''条件名称取得
                            .Rows(.SelectedCells(0).RowIndex).Cells(1).Value = gGetComboItemName(mudtSetCtrlUseNotuseWork.udtCtrlUseNotuseRec(intPB_Row).bytFlg, gEnmComboType.ctChCtrlUseNotuseDetail)

                        End If

                    End With

                End If
                intPB_Row = -1
            ElseIf e.KeyCode = Keys.Delete Then
                'DELETEキー(複数行削除可)
                '変数へ値格納(詳細を含めて)
                With mudtSetCtrlUseNotuseWork
                    For z = 0 To grdUse.SelectedCells.Count - 1 Step 1
                        .udtCtrlUseNotuseRec(grdUse.SelectedCells(z).RowIndex).shtCount = 0
                        .udtCtrlUseNotuseRec(grdUse.SelectedCells(z).RowIndex).bytFlg = 0
                        For i = 0 To UBound(.udtCtrlUseNotuseRec(grdUse.SelectedCells(z).RowIndex).udtUseNotuseDetails)
                            .udtCtrlUseNotuseRec(grdUse.SelectedCells(z).RowIndex).udtUseNotuseDetails(i).shtChno = 0
                            .udtCtrlUseNotuseRec(grdUse.SelectedCells(z).RowIndex).udtUseNotuseDetails(i).bytType = 0
                            .udtCtrlUseNotuseRec(grdUse.SelectedCells(z).RowIndex).udtUseNotuseDetails(i).shtBit = 0
                            .udtCtrlUseNotuseRec(grdUse.SelectedCells(z).RowIndex).udtUseNotuseDetails(i).shtProcess1 = 0
                            .udtCtrlUseNotuseRec(grdUse.SelectedCells(z).RowIndex).udtUseNotuseDetails(i).shtProcess2 = 0
                        Next i

                        'グリッドへ値格納
                        With grdUse
                            '項目番号
                            .Rows(.SelectedCells(z).RowIndex).Cells(0).Value = gConvZeroToNull(0)
                            .Rows(.SelectedCells(z).RowIndex).Cells(1).Value = ""
                        End With
                    Next z
                    
                End With
                
            End If
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッド行ヘッダーダブルクリック
    ' 引数      ： なし
    ' 戻値      ： なし 
    '----------------------------------------------------------------------------
    Private Sub grdUse_RowHeaderMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdUse.RowHeaderMouseDoubleClick

        Try

            ''処理を抜ける条件
            If e.RowIndex < 0 Or e.RowIndex > grdUse.RowCount - 1 Then Return ''行数が0より小さい、もしくは最大行数より大きい場合
            If e.ColumnIndex > grdUse.ColumnCount - 1 Then Return ''列数が最大列数より大きい場合

            ''詳細画面の表示処理
            If frmChControlUseNotuseDetail.gShow(mudtSetCtrlUseNotuseWork.udtCtrlUseNotuseRec(e.RowIndex), e.RowIndex, Me) = 0 Then

                ''設定値を比較用構造体に格納
                Call mSetStructureInfo()

                ''画面表示
                Call mSetDisplayOne(mudtSetCtrlUseNotuseWork, e.RowIndex)

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

            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "txtCount" : Column1.ReadOnly = True
            Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "txtFlg" : Column2.ReadOnly = True
            Column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Column2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            With grdUse

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
                .Columns(0).HeaderText = "Count" : .Columns(0).Width = 100
                .Columns(1).HeaderText = "Flg" : .Columns(1).Width = 350
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                ''.RowCount = 32 + 1
                .RowCount = gAmxControlUseNotUse + 1    '' Ver1.9.6 2016.02.17 ｴﾘｱ拡張
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

                ''ReadOnly色設定
                For i = 0 To .RowCount - 1
                    .Rows(i).Cells("txtCount").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtFlg").Style.BackColor = gColorGridRowBackReadOnly
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
                Call gSetGridCopyAndPaste(grdUse)

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

            ''この画面は入力チェックなし
            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) コントロール使用可／不可設定構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet As gTypSetChCtrlUse)

        Try

            ''処理なし

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) コントロール使用可／不可設定構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal udtSet As gTypSetChCtrlUse)

        Try

            For intRow As Integer = 0 To UBound(udtSet.udtCtrlUseNotuseRec)

                mSetDisplayOne(udtSet, intRow)

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mSetDisplayOne(ByVal udtSet As gTypSetChCtrlUse, ByVal intSelectedRow As Integer)

        Try

            With udtSet.udtCtrlUseNotuseRec(intSelectedRow)

                ''項目番号
                grdUse.Rows(intSelectedRow).Cells(0).Value = gConvZeroToNull(.shtCount)

                ''条件数が 0 の場合は条件名称を表示しない
                If .shtCount <> 0 Then

                    ''条件名称取得
                    grdUse.Rows(intSelectedRow).Cells(1).Value = gGetComboItemName(.bytFlg, gEnmComboType.ctChCtrlUseNotuseDetail)
                Else
                    '条件名称ｸﾘｱ
                    grdUse.Rows(intSelectedRow).Cells(1).Value = ""
                End If

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
    Private Sub mCopyStructure(ByVal udtSource As gTypSetChCtrlUse, _
                               ByRef udtTarget As gTypSetChCtrlUse)

        Try

            For intRow As Integer = 0 To UBound(udtTarget.udtCtrlUseNotuseRec)

                ''条件数
                udtTarget.udtCtrlUseNotuseRec(intRow).shtCount = udtSource.udtCtrlUseNotuseRec(intRow).shtCount

                ''条件種類
                udtTarget.udtCtrlUseNotuseRec(intRow).bytFlg = udtSource.udtCtrlUseNotuseRec(intRow).bytFlg

                ''----------------
                '' 詳細設定
                ''----------------
                For intCol As Integer = 0 To UBound(udtTarget.udtCtrlUseNotuseRec(intRow).udtUseNotuseDetails)

                    ''CH NO.
                    udtTarget.udtCtrlUseNotuseRec(intRow).udtUseNotuseDetails(intCol).shtChno = _
                                        udtSource.udtCtrlUseNotuseRec(intRow).udtUseNotuseDetails(intCol).shtChno
                    ''条件タイプ
                    udtTarget.udtCtrlUseNotuseRec(intRow).udtUseNotuseDetails(intCol).bytType = _
                                        udtSource.udtCtrlUseNotuseRec(intRow).udtUseNotuseDetails(intCol).bytType
                    ''ビット条件
                    udtTarget.udtCtrlUseNotuseRec(intRow).udtUseNotuseDetails(intCol).shtBit = _
                                        udtSource.udtCtrlUseNotuseRec(intRow).udtUseNotuseDetails(intCol).shtBit
                    ''Process1
                    udtTarget.udtCtrlUseNotuseRec(intRow).udtUseNotuseDetails(intCol).shtProcess1 = _
                                        udtSource.udtCtrlUseNotuseRec(intRow).udtUseNotuseDetails(intCol).shtProcess1
                    ''Process2
                    udtTarget.udtCtrlUseNotuseRec(intRow).udtUseNotuseDetails(intCol).shtProcess2 = _
                                        udtSource.udtCtrlUseNotuseRec(intRow).udtUseNotuseDetails(intCol).shtProcess2

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
    Private Function mChkStructureEquals(ByVal udt1 As gTypSetChCtrlUse, _
                                         ByVal udt2 As gTypSetChCtrlUse) As Boolean

        Try

            For i As Integer = 0 To UBound(udt1.udtCtrlUseNotuseRec)

                'If Not mChkStructureEqualsDetail(udt1, udt2, i) Then Return False

                ''条件数
                If udt1.udtCtrlUseNotuseRec(i).shtCount <> udt2.udtCtrlUseNotuseRec(i).shtCount Then Return False

                ''条件種類
                If udt1.udtCtrlUseNotuseRec(i).bytFlg <> udt2.udtCtrlUseNotuseRec(i).bytFlg Then Return False

                ''詳細設定
                For intGridRow As Integer = 0 To UBound(udt1.udtCtrlUseNotuseRec(i).udtUseNotuseDetails)

                    ''CH NO.
                    If udt1.udtCtrlUseNotuseRec(i).udtUseNotuseDetails(intGridRow).shtChno <> _
                                    udt2.udtCtrlUseNotuseRec(i).udtUseNotuseDetails(intGridRow).shtChno Then Return False
                    ''条件タイプ
                    If udt1.udtCtrlUseNotuseRec(i).udtUseNotuseDetails(intGridRow).bytType <> _
                                    udt2.udtCtrlUseNotuseRec(i).udtUseNotuseDetails(intGridRow).bytType Then Return False
                    ''ビット条件
                    If udt1.udtCtrlUseNotuseRec(i).udtUseNotuseDetails(intGridRow).shtBit <> _
                                    udt2.udtCtrlUseNotuseRec(i).udtUseNotuseDetails(intGridRow).shtBit Then Return False
                    ''Process1
                    If udt1.udtCtrlUseNotuseRec(i).udtUseNotuseDetails(intGridRow).shtProcess1 <> _
                                    udt2.udtCtrlUseNotuseRec(i).udtUseNotuseDetails(intGridRow).shtProcess1 Then Return False
                    ''Process2
                    If udt1.udtCtrlUseNotuseRec(i).udtUseNotuseDetails(intGridRow).shtProcess2 <> _
                                    udt2.udtCtrlUseNotuseRec(i).udtUseNotuseDetails(intGridRow).shtProcess2 Then Return False

                Next

            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function
    ''--------------------------------------------------------------------
    '' 機能      : 構造体比較
    '' 返り値    : True:相違なし、False:相違あり
    '' 引き数    : ARG1 - (I ) 構造体１
    '' 　　　    : ARG2 - (I ) 構造体２
    '' 機能説明  : 構造体の設定値を比較する
    '' 備考　　  : 構造体メンバの中に構造体配列がいると Equals メソッドで正しい結果が得られないため関数を用意
    '' 　　　　  : 構造体メンバの中に構造体配列がいない場合は、 Equals メソッドで処理しても良いが一応これを使うこと
    '' 　　　　  : String文字列の比較には gCompareString を使用すること（単純な = だとNULL文字の有り無しで結果が変わってしまう）
    ''--------------------------------------------------------------------
    'Private Function mChkStructureEquals(ByVal udtMach As gTypSetChCtrlUse, _
    '                                     ByVal udtCarg As gTypSetChCtrlUse) As Boolean

    '    Try

    '        For i As Integer = 0 To UBound(udtMach.udtCtrlUseNotuseRec)

    '            ''Machinery情報
    '            If Not mChkStructureEqualsDetail(gudt.SetChCtrlUseM, udtMach, i) Then Return False

    '            ''Cargo情報
    '            If Not mChkStructureEqualsDetail(gudt.SetChCtrlUseC, udtCarg, i) Then Return False

    '        Next

    '        Return True

    '    Catch ex As Exception
    '        Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '    End Try

    'End Function

    '--------------------------------------------------------------------
    ' 機能      : 設定値の取得
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 詳細設定画面の情報を該当構造体に設定
    '--------------------------------------------------------------------
    Private Sub mSetStructureInfo()

        Try

            If optMachinery.Checked Then

                ''Machinery構造体に設定
                Call mCopyStructure(mudtSetCtrlUseNotuseWork, mudtSetCtrlUseNotuseNewMach)

            ElseIf optCargo.Checked Then

                ''Cargo構造体に設定
                Call mCopyStructure(mudtSetCtrlUseNotuseWork, mudtSetCtrlUseNotuseNewCarg)

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

End Class
