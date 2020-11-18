Public Class frmChCompositeDetail

#Region "定数定義"

#End Region

#Region "変数設定"

    Dim mintRtn As Integer
    Dim mudtCompositeRec As gTypSetChCompositeRec

    Private mudtValveDetail As frmChListChannelList.mValveInfo
    Private mudtCompositeDetail As frmChListChannelList.mCompositeInfo
    Private mudtCompositeEditType As gEnmCompositeEditType

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 1:OK 0:キャンセル
    ' 引き数    : ARG1 - (IO) リポーズ入力設定構造体
    ' 機能説明  : 本画面を表示する
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Function gShow(ByRef udtCompositeRec As gTypSetChCompositeRec, _
                          ByRef frmOwner As Form, _
                          ByVal udtCompositeEditType As gEnmCompositeEditType) As Integer

        Try

            ''引数保存
            mudtCompositeRec = udtCompositeRec
            mudtCompositeEditType = udtCompositeEditType

            ''本画面表示
            Call gShowFormModelessForCloseWait2(Me, frmOwner)

            ''OKで閉じる場合は戻り値設定
            If mintRtn = 1 Then
                udtCompositeRec = mudtCompositeRec
            End If

            Return mintRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 1:OK 0:キャンセル
    ' 引き数    : ARG1 - (IO) リポーズ入力設定構造体
    ' 機能説明  : 本画面を表示する
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Function gShow(ByRef udtCompositeRec As gTypSetChCompositeRec, _
                          ByRef frmOwner As Form, _
                          ByRef udtValveDetail As frmChListChannelList.mValveInfo, _
                          ByVal udtCompositeEditType As gEnmCompositeEditType) As Integer

        Try

            ''引数保存
            mudtCompositeRec = udtCompositeRec
            mudtValveDetail = udtValveDetail
            mudtCompositeEditType = udtCompositeEditType

            ''本画面表示
            Call gShowFormModelessForCloseWait2(Me, frmOwner)

            ''OKで閉じる場合は戻り値設定
            If mintRtn = 1 Then
                udtCompositeRec = mudtCompositeRec
                udtValveDetail = mudtValveDetail
            End If

            Return mintRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 1:OK 0:キャンセル
    ' 引き数    : ARG1 - (IO) リポーズ入力設定構造体
    ' 機能説明  : 本画面を表示する
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Function gShow(ByRef udtCompositeRec As gTypSetChCompositeRec, _
                          ByRef frmOwner As Form, _
                          ByRef udtCompositeDetail As frmChListChannelList.mCompositeInfo, _
                          ByVal udtCompositeEditType As gEnmCompositeEditType) As Integer

        Try

            ''引数保存
            mudtCompositeRec = udtCompositeRec
            mudtCompositeDetail = udtCompositeDetail
            mudtCompositeEditType = udtCompositeEditType

            ''本画面表示
            Call gShowFormModelessForCloseWait2(Me, frmOwner)

            ''OKで閉じる場合は戻り値設定
            If mintRtn = 1 Then
                udtCompositeRec = mudtCompositeRec
                udtCompositeDetail = mudtCompositeDetail
            End If

            Return mintRtn

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
    Private Sub frmChGroupReposeDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''グリッドを初期化する
            Call gCompInitControl(grdBitStatusMap, grdAnyMap, txtFilterCoeficient, True)

            ''画面設定
            Call gCompSetDisplay(mudtCompositeRec, grdBitStatusMap, grdAnyMap, txtFilterCoeficient)

            ''BitStatusMap 使用可の場合はセルロックをはずす
            For i As Integer = 0 To grdBitStatusMap.Rows.Count - 1

                If grdBitStatusMap(0, i).Value = True Then

                    For j As Integer = 1 To grdBitStatusMap.ColumnCount - 1

                        'T.Ueki
                        If j <= 2 Then
                            grdBitStatusMap.Rows(i).Cells(j).ReadOnly = True

                        Else
                            grdBitStatusMap.Rows(i).Cells(j).ReadOnly = False
                        End If

                        If j <= 2 Then
                            grdBitStatusMap(j, i).Style.BackColor = gColorGridRowBackReadOnly
                        Else

                            If i Mod 2 <> 0 Then
                                grdBitStatusMap(j, i).Style.BackColor = gColorGridRowBack
                            Else
                                grdBitStatusMap.Rows(i).Cells(j).Style.BackColor = gColorGridRowBackBase

                            End If
                        End If
                    Next
                End If
            Next

            ''Any Map 使用可の場合はセルロックをはずす
            If grdAnyMap(0, 0).Value = True Then

                For j As Integer = 1 To grdAnyMap.ColumnCount - 1

                    'T.Ueki
                    If j <= 2 Then
                        grdAnyMap.Rows(0).Cells(j).ReadOnly = True
                        grdAnyMap(j, 0).Style.BackColor = gColorGridRowBackReadOnly
                    Else
                        grdAnyMap.Rows(0).Cells(j).ReadOnly = False
                        grdAnyMap(j, 0).Style.BackColor = gColorGridRowBackBase
                    End If
   
                Next

            End If

            ''仮設定表示
            Select Case mudtCompositeEditType
                Case gEnmCompositeEditType.cetNone
                Case gEnmCompositeEditType.cetValve
                    Call gCompSetDummySetting(mudtValveDetail, grdBitStatusMap, grdAnyMap)
                Case gEnmCompositeEditType.cetComposite
                    Call gCompSetDummySetting(mudtCompositeDetail, grdBitStatusMap, grdAnyMap)
            End Select

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
    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        Try

            ''グリッドの保留中の変更を全て適用させる
            Call grdBitStatusMap.EndEdit()
            Call grdAnyMap.EndEdit()

            ''入力チェック
            If Not mChkInput() Then Return

            ''設定値の保存
            Call mSetStructure(mudtCompositeRec)

            ''仮設定保存
            Select Case mudtCompositeEditType
                Case gEnmCompositeEditType.cetNone
                Case gEnmCompositeEditType.cetValve
                    Call mSetStructureDummySetting(mudtValveDetail)
                Case gEnmCompositeEditType.cetComposite
                    Call mSetStructureDummySetting(mudtCompositeDetail)
            End Select

            mintRtn = 1
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
    Private Sub frmChGroupReposeDetail_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtFilterCoeficient_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFilterCoeficient.KeyPress

        Try

            e.Handled = gCheckTextInput(3, sender, e.KeyChar)       '' フィルタ定数変更　ver.1.4.4 2012.05.08

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
    Private Sub grdBitStatusMap_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdBitStatusMap.EditingControlShowing

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)
            Dim intColumn As Integer

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdBitStatusMap_KeyPress

                ''該当する列ならイベントハンドラを追加する
                intColumn = dgv.CurrentCell.OwningColumn.Index

                If intColumn = 3 Or intColumn = 4 Or intColumn = 5 Or intColumn = 6 Or intColumn = 15 Then

                    AddHandler tb.KeyPress, AddressOf grdBitStatusMap_KeyPress

                End If

            End If


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub grdAnyMap_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdAnyMap.Click

    End Sub

    Private Sub grdAnyMap_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdAnyMap.EditingControlShowing

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)
            Dim intColumn As Integer

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdAnyMap_KeyPress

                ''該当する列ならイベントハンドラを追加する
                intColumn = dgv.CurrentCell.OwningColumn.Index

                If intColumn >= 3 And intColumn <= 76 Then

                    AddHandler tb.KeyPress, AddressOf grdAnyMap_KeyPress

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
    Private Sub grdBitStatusMap_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdBitStatusMap.KeyPress

        Try
            ''KeyDownイベントを再度Call(USEを変更後、ReadOnlyを解除して再度ペースト)
            If Asc(e.KeyChar) = 22 Then 'Ctl + V

                If grdBitStatusMap.CurrentCell.OwningColumn.Name = "chkUse" Then

                    Dim cls As New clsDataGridViewPlus

                    ' クリップボードの内容から複数行のCOPYをした場合の行数をGET
                    Dim clipText As String = Clipboard.GetText()

                    ' 改行を変換
                    clipText = clipText.Replace(vbCrLf, vbLf)
                    clipText = clipText.Replace(vbCr, vbLf)

                    ' 改行で分割
                    Dim lines() As String = clipText.Split(vbLf)

                    For i As Integer = 0 To UBound(lines)

                        If grdBitStatusMap.CurrentCell.RowIndex + i > grdBitStatusMap.RowCount - 1 Then Exit For

                        Call grdBitStatusMap_CellValueChanged(grdBitStatusMap, New DataGridViewCellEventArgs(0, grdBitStatusMap.CurrentCell.RowIndex + i))

                    Next

                    Call grdBitStatusMap_CellValueChanged(grdBitStatusMap, New DataGridViewCellEventArgs(0, grdBitStatusMap.CurrentCell.RowIndex))
                    Call cls.DataGridViewPlus_KeyDown(grdBitStatusMap, New KeyEventArgs(Keys.Control + Keys.V))
                    cls.Dispose()

                End If

            End If

            If Asc(e.KeyChar) >= 0 And Asc(e.KeyChar) <= 31 Then Exit Sub
            If grdBitStatusMap.CurrentCell.ReadOnly Then Exit Sub

            Dim intColumn As Integer = grdBitStatusMap.CurrentCell.OwningColumn.Index

            Select Case intColumn
                Case 3, 5, 6        ''EXT G,  Grep1,  Grep2

                    Dim dgv As DataGridViewTextBoxEditingControl = CType(sender, DataGridViewTextBoxEditingControl)
                    e.Handled = gCheckTextInput(2, dgv, e.KeyChar, True)

                Case 4              ''Delay

                    Dim dgv As DataGridViewTextBoxEditingControl = CType(sender, DataGridViewTextBoxEditingControl)
                    e.Handled = gCheckTextInput(3, dgv, e.KeyChar, True)

                Case 15             ''Status Name

                    Dim dgv As DataGridViewTextBoxEditingControl = CType(sender, DataGridViewTextBoxEditingControl)
                    e.Handled = gCheckTextInput(8, dgv, e.KeyChar, False)

            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub grdAnyMap_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdAnyMap.KeyPress

        Try
            ''KeyDownイベントを再度Call(USEを変更後、ReadOnlyを解除して再度ペースト)
            If Asc(e.KeyChar) = 22 Then 'Ctl + V

                If grdAnyMap.CurrentCell.OwningColumn.Name = "chkUse" Then

                    Dim cls As New clsDataGridViewPlus
                    Call grdAnyMap_CellValueChanged(grdAnyMap, New DataGridViewCellEventArgs(0, grdAnyMap.CurrentCell.RowIndex))
                    Call cls.DataGridViewPlus_KeyDown(grdAnyMap, New KeyEventArgs(Keys.Control + Keys.V))
                    cls.Dispose()

                End If

            End If


            If Asc(e.KeyChar) >= 0 And Asc(e.KeyChar) <= 31 Then Exit Sub
            If grdBitStatusMap.CurrentCell.ReadOnly Then Exit Sub

            Dim intColumn As Integer = grdAnyMap.CurrentCell.OwningColumn.Index

            Select Case intColumn
                Case 3, 5, 6    ''EXT G,  Grep1,  Grep2

                    Dim dgv As DataGridViewTextBoxEditingControl = CType(sender, DataGridViewTextBoxEditingControl)
                    e.Handled = gCheckTextInput(2, dgv, e.KeyChar, True)

                Case 4          ''Delay

                    Dim dgv As DataGridViewTextBoxEditingControl = CType(sender, DataGridViewTextBoxEditingControl)
                    e.Handled = gCheckTextInput(3, dgv, e.KeyChar, True)

                Case 7          ''Status Name

                    Dim dgv As DataGridViewTextBoxEditingControl = CType(sender, DataGridViewTextBoxEditingControl)
                    e.Handled = gCheckTextInput(8, dgv, e.KeyChar, False)

            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  ： 仮設定の処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '--------------------------------------------------------------------
    Private Sub grdBitStatusMap_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdBitStatusMap.KeyDown

        Try

            If e.KeyCode = gCstDummySetKey Then

                ''選択されているセルがある場合
                If grdBitStatusMap.SelectedCells.Count > 0 Then

                    For i As Integer = 0 To grdBitStatusMap.SelectedCells.Count - 1

                        With grdBitStatusMap.SelectedCells(i)

                            ''仮設定可能な列の場合
                            If mChkDummySetColumnGrid1(mudtCompositeEditType, .ColumnIndex) Then

                                ''セルの背景色を変更する
                                Call gDummySetColorChangeGrid(grdBitStatusMap.SelectedCells(i), grdBitStatusMap)

                            End If

                        End With
                    Next
                End If
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  ： 仮設定の処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '--------------------------------------------------------------------
    Private Sub grdAnyMap_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdAnyMap.KeyDown

        Try

            If e.KeyCode = gCstDummySetKey Then

                ''選択されているセルがある場合
                If grdAnyMap.SelectedCells.Count > 0 Then

                    For i As Integer = 0 To grdAnyMap.SelectedCells.Count - 1

                        With grdAnyMap.SelectedCells(i)

                            ''仮設定可能な列の場合
                            If mChkDummySetColumnGrid2(mudtCompositeEditType, .ColumnIndex) Then

                                ''セルの背景色を変更する
                                Call gDummySetColorChangeGrid(grdAnyMap.SelectedCells(i), grdAnyMap)

                            End If

                        End With
                    Next
                End If
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


    '----------------------------------------------------------------------------
    ' 機能説明  ： セルの値が変更されて、変更が保存されていない場合に発生するイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    ' 機能説明  ： セルの値を保存する
    ' 備考      ： グリッド内 Use チェックボックスの CellValueChanged イベントを発生させるための処理
    '----------------------------------------------------------------------------
    Private Sub grdBitStatusMap_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdBitStatusMap.CurrentCellDirtyStateChanged

        If grdBitStatusMap.CurrentCellAddress.X = 0 AndAlso grdBitStatusMap.IsCurrentRowDirty Then
            grdBitStatusMap.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッド値変更時
    ' 引数      ： なし
    ' 戻値      ： なし
    ' 機能説明  ： Useチェック時に対象行を設定可にする
    '----------------------------------------------------------------------------
    Private Sub grdBitStatusMap_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdBitStatusMap.CellValueChanged


        If e.ColumnIndex = 3 And e.RowIndex >= 0 Then

            '' 2013.11.30 Ext.Gが0の場合もアラーム設定は有とする
            'If Val(grdBitStatusMap(3, e.RowIndex).Value) = 0 Then
            If Not IsNumeric(grdBitStatusMap(3, e.RowIndex).Value) Then
                grdBitStatusMap(2, e.RowIndex).Value = False
                grdBitStatusMap(1, e.RowIndex).Value = False
            Else
                grdBitStatusMap(2, e.RowIndex).Value = True
                grdBitStatusMap(1, e.RowIndex).Value = True
            End If
        End If

            If e.ColumnIndex <> 0 Or e.RowIndex < 0 Then Exit Sub

            '使用可/不可　切替
            If grdBitStatusMap(0, e.RowIndex).EditedFormattedValue = True Then

            For i As Integer = 1 To grdBitStatusMap.ColumnCount - 1

                If 1 <= i And i <= 2 Then
                    grdBitStatusMap(i, e.RowIndex).ReadOnly = True
                    grdBitStatusMap(i, e.RowIndex).Style.BackColor = gColorGridRowBackReadOnly
                Else
                    grdBitStatusMap(i, e.RowIndex).ReadOnly = False

                    If e.RowIndex Mod 2 <> 0 Then
                        grdBitStatusMap(i, e.RowIndex).Style.BackColor = gColorGridRowBack
                    Else
                        grdBitStatusMap(i, e.RowIndex).Style.BackColor = gColorGridRowBackBase
                    End If
                End If

               
            Next

            Else
                grdBitStatusMap(1, e.RowIndex).Value = False
                grdBitStatusMap(2, e.RowIndex).Value = False
                grdBitStatusMap(3, e.RowIndex).Value = ""
                grdBitStatusMap(4, e.RowIndex).Value = ""
                grdBitStatusMap(5, e.RowIndex).Value = ""
                grdBitStatusMap(6, e.RowIndex).Value = ""
                For i As Integer = 7 To 14
                    grdBitStatusMap(i, e.RowIndex).Value = False
                Next
                grdBitStatusMap(15, e.RowIndex).Value = ""

                For i As Integer = 1 To grdBitStatusMap.ColumnCount - 1
                    grdBitStatusMap(i, e.RowIndex).ReadOnly = True
                    grdBitStatusMap(i, e.RowIndex).Style.BackColor = gColorGridRowBackReadOnly
                Next

            End If

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： セルの値が変更されて、変更が保存されていない場合に発生するイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    ' 機能説明  ： セルの値を保存する
    ' 備考      ： グリッド内 Use チェックボックスの CellValueChanged イベントを発生させるための処理
    '----------------------------------------------------------------------------
    Private Sub grdAnyMap_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdAnyMap.CurrentCellDirtyStateChanged

        If grdAnyMap.CurrentCellAddress.X = 0 AndAlso grdAnyMap.IsCurrentRowDirty Then
            grdAnyMap.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If

    End Sub

    Private Sub grdAnyMap_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdAnyMap.CellValueChanged


        If e.ColumnIndex = 3 And e.RowIndex >= 0 Then

            If Val(grdAnyMap(3, e.RowIndex).Value) = 0 Then
                grdAnyMap(2, e.RowIndex).Value = False
                grdAnyMap(1, e.RowIndex).Value = False
            Else
                grdAnyMap(2, e.RowIndex).Value = True
                grdAnyMap(1, e.RowIndex).Value = True
            End If
        End If

        If e.ColumnIndex <> 0 Or e.RowIndex < 0 Then Exit Sub

        '使用可/不可　切替
        If grdAnyMap(0, e.RowIndex).EditedFormattedValue = True Then

            'T.Ueki
            For i As Integer = 1 To grdAnyMap.ColumnCount - 1

                If 1 <= i And i <= 2 Then
                    grdAnyMap(i, e.RowIndex).ReadOnly = True
                    grdAnyMap(i, e.RowIndex).Style.BackColor = gColorGridRowBackReadOnly
                Else
                    grdAnyMap(i, e.RowIndex).ReadOnly = False

                    If e.RowIndex Mod 2 <> 0 Then
                        grdAnyMap(i, e.RowIndex).Style.BackColor = gColorGridRowBack
                    Else
                        grdAnyMap(i, e.RowIndex).Style.BackColor = gColorGridRowBackBase
                    End If
                End If

                'grdAnyMap(i, e.RowIndex).ReadOnly = False
                'grdAnyMap(i, e.RowIndex).Style.BackColor = gColorGridRowBackBase
            Next

        Else
            grdAnyMap(1, e.RowIndex).Value = False
            grdAnyMap(2, e.RowIndex).Value = False
            grdAnyMap(3, e.RowIndex).Value = ""
            grdAnyMap(4, e.RowIndex).Value = ""
            grdAnyMap(5, e.RowIndex).Value = ""
            grdAnyMap(6, e.RowIndex).Value = ""
            grdAnyMap(7, e.RowIndex).Value = ""

            For i As Integer = 1 To grdAnyMap.ColumnCount - 1
                grdAnyMap(i, e.RowIndex).ReadOnly = True
                grdAnyMap(i, e.RowIndex).Style.BackColor = gColorGridRowBackReadOnly
            Next

        End If

    End Sub

#End Region

#End Region

#Region "内部関数"

    '--------------------------------------------------------------------
    ' 機能      : 入力チェック（1.レンジ　2.入力項目　3.CH番号の重複）
    ' 返り値    : True:入力OK、False:入力NG
    ' 引き数    : なし
    ' 機能説明  : 入力チェックを行う
    '--------------------------------------------------------------------
    Private Function mChkInput() As Boolean


        Try

            Dim i As Integer
            Dim j As Integer
            Dim strBitMap1 As String
            Dim strBitMap2 As String

            ''共通数値入力チェック
            If Not gChkInputNum(txtFilterCoeficient, 1, 250, "Filter Coeficient", True, True) Then Return False '' フィルタ定数変更　ver.1.4.4 2012.05.08

            ''Bit Status Map
            For i = 0 To grdBitStatusMap.Rows.Count - 1
                If Not gChkInputNum(grdBitStatusMap(3, i), 0, 24, "EXT.G", i + 1, True, True) Then Return False
                If Not gChkInputNum(grdBitStatusMap(4, i), 0, 240, "Delay", i + 1, True, True) Then Return False
                If Not gChkInputNum(grdBitStatusMap(5, i), 0, 48, "G.REP1", i + 1, True, True) Then Return False
                If Not gChkInputNum(grdBitStatusMap(6, i), 0, 48, "G.REP2", i + 1, True, True) Then Return False
            Next

            ''Any Map
            If Not gChkInputNum(grdAnyMap(3, 0), 0, 24, "EXT.G", 1, True, True) Then Return False
            If Not gChkInputNum(grdAnyMap(4, 0), 0, 240, "Delay", 1, True, True) Then Return False
            If Not gChkInputNum(grdAnyMap(5, 0), 0, 48, "G.REP1", 1, True, True) Then Return False
            If Not gChkInputNum(grdAnyMap(6, 0), 0, 48, "G.REP2", 1, True, True) Then Return False

            ' ''Bit Count以上のBitMapにチェックが入っていないか？
            'intValue = CCInt(txtBitCount.Text)
            'If intValue < 8 Then

            '    For i = 0 To 7

            '        For j = intValue To 7

            '            If grdBitStatusMap(j + 7, i).Value = True Then

            '                Call MessageBox.Show("Bit Status Map [" & i + 1 & "]  Please set only " & intValue.ToString & " bits. ", "InputError", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '                Return False

            '            End If
            '        Next

            '    Next

            'End If

            ''同じ状態がダブっていないか？
            For i = 0 To 7

                strBitMap1 = IIf(grdBitStatusMap(1, i).Value, "1", "0")
                strBitMap1 += IIf(grdBitStatusMap(2, i).Value, "1", "0")
                strBitMap1 += IIf(grdBitStatusMap(7, i).Value, "1", "0")
                strBitMap1 += IIf(grdBitStatusMap(8, i).Value, "1", "0")
                strBitMap1 += IIf(grdBitStatusMap(9, i).Value, "1", "0")
                strBitMap1 += IIf(grdBitStatusMap(10, i).Value, "1", "0")
                strBitMap1 += IIf(grdBitStatusMap(11, i).Value, "1", "0")
                strBitMap1 += IIf(grdBitStatusMap(12, i).Value, "1", "0")
                strBitMap1 += IIf(grdBitStatusMap(13, i).Value, "1", "0")
                strBitMap1 += IIf(grdBitStatusMap(14, i).Value, "1", "0")

                If strBitMap1 <> "0000000000" Then

                    If i < 7 Then
                        For j = i + 1 To 7

                            strBitMap2 = IIf(grdBitStatusMap(1, j).Value, "1", "0")
                            strBitMap2 += IIf(grdBitStatusMap(2, j).Value, "1", "0")
                            strBitMap2 += IIf(grdBitStatusMap(7, j).Value, "1", "0")
                            strBitMap2 += IIf(grdBitStatusMap(8, j).Value, "1", "0")
                            strBitMap2 += IIf(grdBitStatusMap(9, j).Value, "1", "0")
                            strBitMap2 += IIf(grdBitStatusMap(10, j).Value, "1", "0")
                            strBitMap2 += IIf(grdBitStatusMap(11, j).Value, "1", "0")
                            strBitMap2 += IIf(grdBitStatusMap(12, j).Value, "1", "0")
                            strBitMap2 += IIf(grdBitStatusMap(13, j).Value, "1", "0")
                            strBitMap2 += IIf(grdBitStatusMap(14, j).Value, "1", "0")

                            If strBitMap2 <> "0000000000" Then

                                If strBitMap1 = strBitMap2 Then
                                    Call MessageBox.Show("Bit Status Map [" & i + 1 & "] and [" & j + 1 & "]  are the same settings.", "InputError", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Return False
                                End If

                            End If

                        Next j
                    End If

                    ''状態チェックがされているのに Status Name が未入力でないか？
                    If gGetString(grdBitStatusMap(15, i).Value) = "" Then
                        Call MessageBox.Show("Bit Status Map [" & i + 1 & "]  Please set the Status Name. ", "InputError", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False
                    End If

                End If

            Next i

            If grdAnyMap(1, 0).Value Or grdAnyMap(2, 0).Value Then
                If gGetString(grdAnyMap(7, 0).Value) = "" Then
                    Call MessageBox.Show("Any Map : Please set the Status Name. ", "InputError", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False
                End If
            End If


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
    Private Sub mSetStructure(ByRef udtSet As gTypSetChCompositeRec)

        Try

            ''DI Filter
            udtSet.shtDiFilter = gConvNullToZero(txtFilterCoeficient.Text)

            ''Bit Status Map
            For i As Integer = 0 To grdBitStatusMap.Rows.Count - 1

                With udtSet.udtCompInf(i)

                    .bytAlarmUse = gBitSet(.bytAlarmUse, 0, IIf(grdBitStatusMap(0, i).Value, True, False))
                    .bytAlarmUse = gBitSet(.bytAlarmUse, 2, IIf(grdBitStatusMap(1, i).Value, True, False))
                    .bytAlarmUse = gBitSet(.bytAlarmUse, 1, IIf(grdBitStatusMap(2, i).Value, True, False))

                    .bytExtGroup = IIf(Trim(grdBitStatusMap(3, i).Value) = "", gCstCodeChCompExtGroupNothing, Trim(grdBitStatusMap(3, i).Value))
                    .bytDelay = IIf(Trim(grdBitStatusMap(4, i).Value) = "", gCstCodeChCompDelayTimerNothing, Trim(grdBitStatusMap(4, i).Value))
                    .bytGRepose1 = IIf(Trim(grdBitStatusMap(5, i).Value) = "", gCstCodeChCompGroupRepose1Nothing, Trim(grdBitStatusMap(5, i).Value))
                    .bytGRepose2 = IIf(Trim(grdBitStatusMap(6, i).Value) = "", gCstCodeChCompGroupRepose2Nothing, Trim(grdBitStatusMap(6, i).Value))

                    .bytBitPattern = gBitSet(.bytBitPattern, 0, IIf(grdBitStatusMap(7, i).Value, True, False))
                    .bytBitPattern = gBitSet(.bytBitPattern, 1, IIf(grdBitStatusMap(8, i).Value, True, False))
                    .bytBitPattern = gBitSet(.bytBitPattern, 2, IIf(grdBitStatusMap(9, i).Value, True, False))
                    .bytBitPattern = gBitSet(.bytBitPattern, 3, IIf(grdBitStatusMap(10, i).Value, True, False))
                    .bytBitPattern = gBitSet(.bytBitPattern, 4, IIf(grdBitStatusMap(11, i).Value, True, False))
                    .bytBitPattern = gBitSet(.bytBitPattern, 5, IIf(grdBitStatusMap(12, i).Value, True, False))
                    .bytBitPattern = gBitSet(.bytBitPattern, 6, IIf(grdBitStatusMap(13, i).Value, True, False))
                    .bytBitPattern = gBitSet(.bytBitPattern, 7, IIf(grdBitStatusMap(14, i).Value, True, False))

                    .strStatusName = Trim(grdBitStatusMap(15, i).Value)

                    .bytManualReposeState = 0   ''マニュアルリポーズ：無効

                    '.bytManualReposeSet = IIf(.bytAlarmUse = 0, 0, 1)   ''m_repose_set
                    .bytManualReposeSet = IIf(grdBitStatusMap(1, i).Value = True, 1, 0)   ''m_repose_set

                End With

            Next

            ''Any Map
            With udtSet.udtCompInf(UBound(udtSet.udtCompInf))

                .bytAlarmUse = gBitSet(.bytAlarmUse, 0, IIf(grdAnyMap(0, 0).Value, True, False))
                .bytAlarmUse = gBitSet(.bytAlarmUse, 2, IIf(grdAnyMap(1, 0).Value, True, False))
                .bytAlarmUse = gBitSet(.bytAlarmUse, 1, IIf(grdAnyMap(2, 0).Value, True, False))

                .bytExtGroup = IIf(Trim(grdAnyMap(3, 0).Value) = "", gCstCodeChCompExtGroupNothing, Trim(grdAnyMap(3, 0).Value))
                .bytDelay = IIf(Trim(grdAnyMap(4, 0).Value) = "", gCstCodeChCompDelayTimerNothing, Trim(grdAnyMap(4, 0).Value))
                .bytGRepose1 = IIf(Trim(grdAnyMap(5, 0).Value) = "", gCstCodeChCompGroupRepose1Nothing, Trim(grdAnyMap(5, 0).Value))
                .bytGRepose2 = IIf(Trim(grdAnyMap(6, 0).Value) = "", gCstCodeChCompGroupRepose2Nothing, Trim(grdAnyMap(6, 0).Value))

                .strStatusName = Trim(grdAnyMap(7, 0).Value)

                .bytManualReposeState = 0   ''マニュアルリポーズ：無効

                '.bytManualReposeSet = IIf(.bytAlarmUse = 0, 0, 1)   ''m_repose_set
                '.bytManualReposeSet = IIf(grdBitStatusMap(1, 0).Value = True, 1, 0)   ''m_repose_set
                .bytManualReposeSet = IIf(grdAnyMap(1, 0).Value = True, 1, 0)   ''m_repose_set

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mSetStructureDummySetting(ByRef udtValveDetail As frmChListChannelList.mValveInfo)

        udtValveDetail.DummyCmpStatus1ExtGr = gDummyCheckControl(grdBitStatusMap.Rows(0).Cells(3))
        udtValveDetail.DummyCmpStatus1Delay = gDummyCheckControl(grdBitStatusMap.Rows(0).Cells(4))
        udtValveDetail.DummyCmpStatus1GRep1 = gDummyCheckControl(grdBitStatusMap.Rows(0).Cells(5))
        udtValveDetail.DummyCmpStatus1GRep2 = gDummyCheckControl(grdBitStatusMap.Rows(0).Cells(6))
        udtValveDetail.DummyCmpStatus1StaNm = gDummyCheckControl(grdBitStatusMap.Rows(0).Cells(15))

        udtValveDetail.DummyCmpStatus2ExtGr = gDummyCheckControl(grdBitStatusMap.Rows(1).Cells(3))
        udtValveDetail.DummyCmpStatus2Delay = gDummyCheckControl(grdBitStatusMap.Rows(1).Cells(4))
        udtValveDetail.DummyCmpStatus2GRep1 = gDummyCheckControl(grdBitStatusMap.Rows(1).Cells(5))
        udtValveDetail.DummyCmpStatus2GRep2 = gDummyCheckControl(grdBitStatusMap.Rows(1).Cells(6))
        udtValveDetail.DummyCmpStatus2StaNm = gDummyCheckControl(grdBitStatusMap.Rows(1).Cells(15))

        udtValveDetail.DummyCmpStatus3ExtGr = gDummyCheckControl(grdBitStatusMap.Rows(2).Cells(3))
        udtValveDetail.DummyCmpStatus3Delay = gDummyCheckControl(grdBitStatusMap.Rows(2).Cells(4))
        udtValveDetail.DummyCmpStatus3GRep1 = gDummyCheckControl(grdBitStatusMap.Rows(2).Cells(5))
        udtValveDetail.DummyCmpStatus3GRep2 = gDummyCheckControl(grdBitStatusMap.Rows(2).Cells(6))
        udtValveDetail.DummyCmpStatus3StaNm = gDummyCheckControl(grdBitStatusMap.Rows(2).Cells(15))

        udtValveDetail.DummyCmpStatus4ExtGr = gDummyCheckControl(grdBitStatusMap.Rows(3).Cells(3))
        udtValveDetail.DummyCmpStatus4Delay = gDummyCheckControl(grdBitStatusMap.Rows(3).Cells(4))
        udtValveDetail.DummyCmpStatus4GRep1 = gDummyCheckControl(grdBitStatusMap.Rows(3).Cells(5))
        udtValveDetail.DummyCmpStatus4GRep2 = gDummyCheckControl(grdBitStatusMap.Rows(3).Cells(6))
        udtValveDetail.DummyCmpStatus4StaNm = gDummyCheckControl(grdBitStatusMap.Rows(3).Cells(15))

        udtValveDetail.DummyCmpStatus5ExtGr = gDummyCheckControl(grdBitStatusMap.Rows(4).Cells(3))
        udtValveDetail.DummyCmpStatus5Delay = gDummyCheckControl(grdBitStatusMap.Rows(4).Cells(4))
        udtValveDetail.DummyCmpStatus5GRep1 = gDummyCheckControl(grdBitStatusMap.Rows(4).Cells(5))
        udtValveDetail.DummyCmpStatus5GRep2 = gDummyCheckControl(grdBitStatusMap.Rows(4).Cells(6))
        udtValveDetail.DummyCmpStatus5StaNm = gDummyCheckControl(grdBitStatusMap.Rows(4).Cells(15))

        udtValveDetail.DummyCmpStatus6ExtGr = gDummyCheckControl(grdBitStatusMap.Rows(5).Cells(3))
        udtValveDetail.DummyCmpStatus6Delay = gDummyCheckControl(grdBitStatusMap.Rows(5).Cells(4))
        udtValveDetail.DummyCmpStatus6GRep1 = gDummyCheckControl(grdBitStatusMap.Rows(5).Cells(5))
        udtValveDetail.DummyCmpStatus6GRep2 = gDummyCheckControl(grdBitStatusMap.Rows(5).Cells(6))
        udtValveDetail.DummyCmpStatus6StaNm = gDummyCheckControl(grdBitStatusMap.Rows(5).Cells(15))

        udtValveDetail.DummyCmpStatus7ExtGr = gDummyCheckControl(grdBitStatusMap.Rows(6).Cells(3))
        udtValveDetail.DummyCmpStatus7Delay = gDummyCheckControl(grdBitStatusMap.Rows(6).Cells(4))
        udtValveDetail.DummyCmpStatus7GRep1 = gDummyCheckControl(grdBitStatusMap.Rows(6).Cells(5))
        udtValveDetail.DummyCmpStatus7GRep2 = gDummyCheckControl(grdBitStatusMap.Rows(6).Cells(6))
        udtValveDetail.DummyCmpStatus7StaNm = gDummyCheckControl(grdBitStatusMap.Rows(6).Cells(15))

        udtValveDetail.DummyCmpStatus8ExtGr = gDummyCheckControl(grdBitStatusMap.Rows(7).Cells(3))
        udtValveDetail.DummyCmpStatus8Delay = gDummyCheckControl(grdBitStatusMap.Rows(7).Cells(4))
        udtValveDetail.DummyCmpStatus8GRep1 = gDummyCheckControl(grdBitStatusMap.Rows(7).Cells(5))
        udtValveDetail.DummyCmpStatus8GRep2 = gDummyCheckControl(grdBitStatusMap.Rows(7).Cells(6))
        udtValveDetail.DummyCmpStatus8StaNm = gDummyCheckControl(grdBitStatusMap.Rows(7).Cells(15))

        udtValveDetail.DummyCmpStatus9ExtGr = gDummyCheckControl(grdAnyMap.Rows(0).Cells(3))
        udtValveDetail.DummyCmpStatus9Delay = gDummyCheckControl(grdAnyMap.Rows(0).Cells(4))
        udtValveDetail.DummyCmpStatus9GRep1 = gDummyCheckControl(grdAnyMap.Rows(0).Cells(5))
        udtValveDetail.DummyCmpStatus9GRep2 = gDummyCheckControl(grdAnyMap.Rows(0).Cells(6))
        udtValveDetail.DummyCmpStatus9StaNm = gDummyCheckControl(grdAnyMap.Rows(0).Cells(7))

    End Sub

    Private Sub mSetStructureDummySetting(ByRef udtCompositeDetail As frmChListChannelList.mCompositeInfo)

        udtCompositeDetail.DummyCmpStatus1ExtGr = gDummyCheckControl(grdBitStatusMap.Rows(0).Cells(3))
        udtCompositeDetail.DummyCmpStatus1Delay = gDummyCheckControl(grdBitStatusMap.Rows(0).Cells(4))
        udtCompositeDetail.DummyCmpStatus1GRep1 = gDummyCheckControl(grdBitStatusMap.Rows(0).Cells(5))
        udtCompositeDetail.DummyCmpStatus1GRep2 = gDummyCheckControl(grdBitStatusMap.Rows(0).Cells(6))
        udtCompositeDetail.DummyCmpStatus1StaNm = gDummyCheckControl(grdBitStatusMap.Rows(0).Cells(15))

        udtCompositeDetail.DummyCmpStatus2ExtGr = gDummyCheckControl(grdBitStatusMap.Rows(1).Cells(3))
        udtCompositeDetail.DummyCmpStatus2Delay = gDummyCheckControl(grdBitStatusMap.Rows(1).Cells(4))
        udtCompositeDetail.DummyCmpStatus2GRep1 = gDummyCheckControl(grdBitStatusMap.Rows(1).Cells(5))
        udtCompositeDetail.DummyCmpStatus2GRep2 = gDummyCheckControl(grdBitStatusMap.Rows(1).Cells(6))
        udtCompositeDetail.DummyCmpStatus2StaNm = gDummyCheckControl(grdBitStatusMap.Rows(1).Cells(15))

        udtCompositeDetail.DummyCmpStatus3ExtGr = gDummyCheckControl(grdBitStatusMap.Rows(2).Cells(3))
        udtCompositeDetail.DummyCmpStatus3Delay = gDummyCheckControl(grdBitStatusMap.Rows(2).Cells(4))
        udtCompositeDetail.DummyCmpStatus3GRep1 = gDummyCheckControl(grdBitStatusMap.Rows(2).Cells(5))
        udtCompositeDetail.DummyCmpStatus3GRep2 = gDummyCheckControl(grdBitStatusMap.Rows(2).Cells(6))
        udtCompositeDetail.DummyCmpStatus3StaNm = gDummyCheckControl(grdBitStatusMap.Rows(2).Cells(15))

        udtCompositeDetail.DummyCmpStatus4ExtGr = gDummyCheckControl(grdBitStatusMap.Rows(3).Cells(3))
        udtCompositeDetail.DummyCmpStatus4Delay = gDummyCheckControl(grdBitStatusMap.Rows(3).Cells(4))
        udtCompositeDetail.DummyCmpStatus4GRep1 = gDummyCheckControl(grdBitStatusMap.Rows(3).Cells(5))
        udtCompositeDetail.DummyCmpStatus4GRep2 = gDummyCheckControl(grdBitStatusMap.Rows(3).Cells(6))
        udtCompositeDetail.DummyCmpStatus4StaNm = gDummyCheckControl(grdBitStatusMap.Rows(3).Cells(15))

        udtCompositeDetail.DummyCmpStatus5ExtGr = gDummyCheckControl(grdBitStatusMap.Rows(4).Cells(3))
        udtCompositeDetail.DummyCmpStatus5Delay = gDummyCheckControl(grdBitStatusMap.Rows(4).Cells(4))
        udtCompositeDetail.DummyCmpStatus5GRep1 = gDummyCheckControl(grdBitStatusMap.Rows(4).Cells(5))
        udtCompositeDetail.DummyCmpStatus5GRep2 = gDummyCheckControl(grdBitStatusMap.Rows(4).Cells(6))
        udtCompositeDetail.DummyCmpStatus5StaNm = gDummyCheckControl(grdBitStatusMap.Rows(4).Cells(15))

        udtCompositeDetail.DummyCmpStatus6ExtGr = gDummyCheckControl(grdBitStatusMap.Rows(5).Cells(3))
        udtCompositeDetail.DummyCmpStatus6Delay = gDummyCheckControl(grdBitStatusMap.Rows(5).Cells(4))
        udtCompositeDetail.DummyCmpStatus6GRep1 = gDummyCheckControl(grdBitStatusMap.Rows(5).Cells(5))
        udtCompositeDetail.DummyCmpStatus6GRep2 = gDummyCheckControl(grdBitStatusMap.Rows(5).Cells(6))
        udtCompositeDetail.DummyCmpStatus6StaNm = gDummyCheckControl(grdBitStatusMap.Rows(5).Cells(15))

        udtCompositeDetail.DummyCmpStatus7ExtGr = gDummyCheckControl(grdBitStatusMap.Rows(6).Cells(3))
        udtCompositeDetail.DummyCmpStatus7Delay = gDummyCheckControl(grdBitStatusMap.Rows(6).Cells(4))
        udtCompositeDetail.DummyCmpStatus7GRep1 = gDummyCheckControl(grdBitStatusMap.Rows(6).Cells(5))
        udtCompositeDetail.DummyCmpStatus7GRep2 = gDummyCheckControl(grdBitStatusMap.Rows(6).Cells(6))
        udtCompositeDetail.DummyCmpStatus7StaNm = gDummyCheckControl(grdBitStatusMap.Rows(6).Cells(15))

        udtCompositeDetail.DummyCmpStatus8ExtGr = gDummyCheckControl(grdBitStatusMap.Rows(7).Cells(3))
        udtCompositeDetail.DummyCmpStatus8Delay = gDummyCheckControl(grdBitStatusMap.Rows(7).Cells(4))
        udtCompositeDetail.DummyCmpStatus8GRep1 = gDummyCheckControl(grdBitStatusMap.Rows(7).Cells(5))
        udtCompositeDetail.DummyCmpStatus8GRep2 = gDummyCheckControl(grdBitStatusMap.Rows(7).Cells(6))
        udtCompositeDetail.DummyCmpStatus8StaNm = gDummyCheckControl(grdBitStatusMap.Rows(7).Cells(15))

        udtCompositeDetail.DummyCmpStatus9ExtGr = gDummyCheckControl(grdAnyMap.Rows(0).Cells(3))
        udtCompositeDetail.DummyCmpStatus9Delay = gDummyCheckControl(grdAnyMap.Rows(0).Cells(4))
        udtCompositeDetail.DummyCmpStatus9GRep1 = gDummyCheckControl(grdAnyMap.Rows(0).Cells(5))
        udtCompositeDetail.DummyCmpStatus9GRep2 = gDummyCheckControl(grdAnyMap.Rows(0).Cells(6))
        udtCompositeDetail.DummyCmpStatus9StaNm = gDummyCheckControl(grdAnyMap.Rows(0).Cells(7))

    End Sub

    Private Function mChkDummySetColumnGrid1(ByVal udtCompositeEditType As gEnmCompositeEditType, _
                                             ByVal intColumnIndex As Integer) As Boolean

        Dim blnRtn As Boolean = False

        Select Case udtCompositeEditType
            Case gEnmCompositeEditType.cetNone

                blnRtn = False

            Case gEnmCompositeEditType.cetValve, gEnmCompositeEditType.cetComposite

                If intColumnIndex = 3 _
                Or intColumnIndex = 4 _
                Or intColumnIndex = 5 _
                Or intColumnIndex = 6 _
                Or intColumnIndex = 15 Then
                    blnRtn = True
                End If

        End Select

        Return blnRtn

    End Function

    Private Function mChkDummySetColumnGrid2(ByVal udtCompositeEditType As gEnmCompositeEditType, _
                                             ByVal intColumnIndex As Integer) As Boolean

        Dim blnRtn As Boolean = False

        Select Case udtCompositeEditType
            Case gEnmCompositeEditType.cetNone

                blnRtn = False

            Case gEnmCompositeEditType.cetValve, gEnmCompositeEditType.cetComposite

                If intColumnIndex = 3 _
                Or intColumnIndex = 4 _
                Or intColumnIndex = 5 _
                Or intColumnIndex = 6 _
                Or intColumnIndex = 7 Then
                    blnRtn = True
                End If

        End Select

        Return blnRtn

    End Function

#End Region

End Class