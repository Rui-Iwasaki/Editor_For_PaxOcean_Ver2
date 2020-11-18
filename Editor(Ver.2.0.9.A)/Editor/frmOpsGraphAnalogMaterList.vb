Public Class frmOpsGraphAnalogMaterList

#Region "定数定義"

    ''表示するアナログメーター数
    Private Const mCstCodeNumberOfBmp_8 As Integer = 8
    Private Const mCstCodeNumberOfBmp_5 As Integer = 5
    Private Const mCstCodeNumberOfBmp_2 As Integer = 2

#End Region

#Region "変数定義"

    Private mudtSetOpsGraphNew As gTypSetOpsGraphAnalogMeter = Nothing
    Private mintRtn As Integer

    ''初期化フラグ
    Private mblnInitFlg As Boolean

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 0:OK  <> 0:キャンセル
    ' 引き数    : ARG1 - (IO) OPS設定：アナログメーター構造体
    '           : ARG2 - (I ) グラフ設定画面 選択行Index
    ' 機能説明  : 本画面を表示する
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Function gShow(ByRef udtAnalogMeter As gTypSetOpsGraphAnalogMeter, _
                          ByVal intRow As Integer, _
                          ByRef frmOwner As Form) As Integer

        Try

            ''ボタン選択フラグ初期化
            mintRtn = 1

            ''引数保存
            mudtSetOpsGraphNew = udtAnalogMeter

            ''本画面表示
            Call gShowFormModelessForCloseWait2(Me, frmOwner)

            ''OKで閉じる場合は戻り値設定
            If mintRtn = 0 Then
                udtAnalogMeter = mudtSetOpsGraphNew
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
    Private Sub frmOpsGraphAnalogMaterList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''初期化フラグ
            mblnInitFlg = True

            ''コンボボックス初期設定
            'Call gSetComboBox(cmbDisplayType, gEnmComboType.ctOpsGraphAnalogDispType)

            ''グリッドを初期化する
            Call mInitialDataGrid()

            ''画面設定
            Call mSetDisplay(mudtSetOpsGraphNew)

            ''グリッドの表示行調整
            'Call mAdjustGridRows()

            ''初期化フラグ
            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームクローズ
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmOpsGraphAnalogMaterList_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

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
    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        Try

            ''入力チェック
            If Not mChkInput() Then Return

            ''設定値の保存
            Call mSetStructure(mudtSetOpsGraphNew)

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

            mintRtn = 1
            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub



#Region "イベント処理"

    '----------------------------------------------------------------------------
    ' 機能説明  ： CH No. フォーマット
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdAnalogMeter_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdAnalogMeter.CellValidated

        Try

            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If dgv.CurrentCell.OwningColumn.Name = "txtChNo" Then

                If IsNumeric(grdAnalogMeter.Rows(e.RowIndex).Cells(0).Value) Then

                    grdAnalogMeter.Rows(e.RowIndex).Cells(0).Value() = Integer.Parse(grdAnalogMeter.Rows(e.RowIndex).Cells(0).Value).ToString("0000")
                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
    Private Sub grdAnalogMeter_CellValueChanged(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdAnalogMeter.CellValueChanged
        Try
            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If dgv.CurrentCell.OwningColumn.Name = "txtChNo" Then

                If IsNumeric(grdAnalogMeter.Rows(e.RowIndex).Cells(0).Value) Then
                    Dim intRet As Integer = fnSetScale(grdAnalogMeter.Rows(e.RowIndex).Cells(0).Value)
                    If intRet > 0 Then
                        '■外販
                        '外販の場合、Scaleを自動で求める
                        '↓メータ分割数　外販でなくても自動で求めるよう変更(コメントアウト) hori 20200206
                        'If gintNaiGai = 1 Then
                        'Scaleに0が入力された場合自動計算 hori 20200228
                        'If IsNumeric(grdAnalogMeter.Rows(e.RowIndex).Cells(1).Value) Then
                        '    If grdAnalogMeter.Rows(e.RowIndex).Cells(1).Value = 0 Then
                        '        grdAnalogMeter.Rows(e.RowIndex).Cells(1).Value = intRet
                        '        'End If
                        '    End If
                        'End If
                    End If
                End If

            End If
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    '    '----------------------------------------------------------------------------
    '    ' 機能説明  ： DisplayType プルダウンリスト項目を変更した時
    '    ' 引数      ： なし
    '    ' 戻値      ： なし
    '    '----------------------------------------------------------------------------
    '    Private Sub cmbDisplayType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDisplayType.SelectedIndexChanged

    '        Try

    '            ''初期化中は処理を抜ける
    '            If mblnInitFlg Then Return

    '            ''グリッドの表示行調整
    '            Call mAdjustGridRows()

    '        Catch ex As Exception
    '            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '        End Try

    '    End Sub

#End Region

#Region "入力制限・入力チェック"

    '----------------------------------------------------------------------------
    ' 機能説明  ： Graph Title 入力制限
    ' 引数      ： なし 
    ' 戻値      ： なし 
    '----------------------------------------------------------------------------
    Private Sub txtGraphTitle_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGraphTitle.KeyPress

        Try

            e.Handled = gCheckTextInput(32, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベントを発生させる
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdAnalogMeter_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdAnalogMeter.EditingControlShowing

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdAnalogMeter_KeyPress

                ''イベントハンドラを追加する
                AddHandler tb.KeyPress, AddressOf grdAnalogMeter_KeyPress

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
    Private Sub grdAnalogMeter_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdAnalogMeter.KeyPress

        Try

            ''選択セルの名称取得
            Dim strColumnName As String = grdAnalogMeter.CurrentCell.OwningColumn.Name

            ''[CH_NO.]
            If strColumnName = "txtChNo" Then
                e.Handled = gCheckTextInput(5, sender, e.KeyChar)
            End If

            ''[SCALE]
            If strColumnName = "txtScale" Then
                e.Handled = gCheckTextInput(1, sender, e.KeyChar)
            End If

            ''[COLOR]
            If strColumnName = "txtColor" Then
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
    Private Sub grdAnalogMeter_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles grdAnalogMeter.CellValidating

        Try

            ''------------------------------------------------------------
            '' 入力チェックは mChkInput で実施するよう変更
            ''------------------------------------------------------------

            ''Dim dgv As DataGridView = CType(sender, DataGridView)
            ''Dim strColumnName = dgv.Columns(e.ColumnIndex).Name

            '' ''[CH_NO.]
            ''If strColumnName = "txtChNo" Then
            ''    e.Cancel = gChkTextNumSpan(0, 65535, e.FormattedValue)
            ''End If

            '' ''[SCALE]
            ''If strColumnName = "txtScale" Then
            ''    e.Cancel = gChkTextNumSpan(3, 7, e.FormattedValue)
            ''End If

            '' ''[COLOR]
            ''If strColumnName = "txtColor" Then
            ''    e.Cancel = gChkTextNumSpan(0, 255, e.FormattedValue)
            ''End If

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

            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "txtChNo"
            Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "txtScale"
            Dim Column3 As New DataGridViewTextBoxColumn : Column3.Name = "txtColor"


            '■外販
            '外販の場合,Colorは非表示
            If gintNaiGai = 1 Then
                Column3.Visible = False
            End If

            Column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Column2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            With grdAnalogMeter

                ''列
                .Columns.Clear()
                .Columns.Add(Column1) : .Columns.Add(Column2) : .Columns.Add(Column3)
                .AllowUserToResizeColumns = False   ''列幅の変更不可

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "CH No." : .Columns(0).Width = 80
                .Columns(1).HeaderText = "Scale" : .Columns(1).Width = 80
                .Columns(2).HeaderText = "Color" : .Columns(2).Width = 80

                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing  ''行ヘッダー幅の変更不可

                ''行
                .RowCount = 8 + 1                   ''行数＋ヘッダー行
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする

                ''行ヘッダー
                For i As Integer = 1 To .RowCount
                    .Rows(i - 1).HeaderCell.Value = i.ToString
                Next
                .RowHeadersWidth = 50
                .RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ''偶数行の背景色を変える
                Call mSetRowColor()

                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                ''スクロールバー
                .ScrollBars = ScrollBars.None

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdAnalogMeter)

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

            Dim i As Integer
            'Dim j As Integer
            Dim strChNo As String, strScale As String, strColor As String

            With grdAnalogMeter

                For i = 0 To .RowCount - 1

                    ''-----------------------------
                    '' レンジチェック
                    ''-----------------------------
                    If Not gChkInputNum(.Rows(i).Cells("txtChNo"), 0, 65535, "CH No.", i + 1, True, True) Then Return False

                    'メータ分割　hori 20200228
                    If IsNumeric(grdAnalogMeter.Rows(i).Cells(1).Value) Then
                        If Integer.Parse(.Rows(i).Cells("txtScale").Value) = 0 Then 'Scaleの値が"0"なら入力OK
                            Return True
                        ElseIf Not gChkInputNum(.Rows(i).Cells("txtScale"), 3, 7, "Scale", i + 1, True, True) Then  '3～7の範囲外なら入力NG
                            Return False

                        End If
                    End If

                    If Not gChkInputNum(.Rows(i).Cells("txtColor"), 0, 8, "Color", i + 1, True, True) Then Return False

                    strChNo = gGetString(.Rows(i).Cells("txtChNo").Value)
                    strScale = gGetString(.Rows(i).Cells("txtScale").Value)
                    strColor = gGetString(.Rows(i).Cells("txtColor").Value)

                    ''-----------------------------
                    '' 入力項目確認
                    ''-----------------------------
                    If strChNo = "" And strScale = "" And strColor = "" Then
                        ''OK

                    ElseIf strChNo = "" And strScale = "" And strColor = "0" Then
                        ''OK

                    Else

                        If strChNo = "" Then
                            Call MessageBox.Show("Please set 'CH No.' data of the line No." & i + 1, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return False
                        End If

                        If strScale = "" Then
                            Call MessageBox.Show("Please set 'Scale' data of the line No." & i + 1, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return False
                        End If

                        If strChNo <> "" And strColor = "" Then
                            Call MessageBox.Show("Please set 'Color' data of the line No." & i + 1, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return False
                        End If

                    End If

                    ''-----------------------------
                    '' CH番号の重複登録は不可
                    ''-----------------------------
                    'For j = i + 1 To .RowCount - 1

                    '    If gGetString(grdAnalogMeter(0, i).Value) <> "" Then

                    '        If gGetString(grdAnalogMeter(0, i).Value) = gGetString(grdAnalogMeter(0, j).Value) Then

                    '            Call MessageBox.Show("The same name as [CH No.] cannot be set of CH No [" & grdAnalogMeter(0, i).Value & "] and CH No [" & grdAnalogMeter(0, j).Value & "].", _
                    '                                 "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '            Return False

                    '        End If

                    '    End If

                    'Next j

                Next i

            End With

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) アナログメーター構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet As gTypSetOpsGraphAnalogMeter)

        Try

            With udtSet

                ''グラフタイトル
                .strTitle = gGetString(txtGraphTitle.Text)

                ''表示タイプ
                .bytMeterType = 1
                '.bytMeterType = CCbyte(cmbDisplayType.SelectedValue)

                For i As Integer = 0 To grdAnalogMeter.RowCount - 1

                    With .udtDetail(i)

                        ''CH番号
                        .shtChNo = CCUInt16(grdAnalogMeter.Rows(i).Cells(0).Value)

                        ''目盛り分割数
                        .bytScale = CCbyte(grdAnalogMeter.Rows(i).Cells(1).Value)

                        ''表示色
                        .bytColor = CCbyte(grdAnalogMeter.Rows(i).Cells(2).Value)

                    End With

                Next

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) アナログメーター構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal udtSet As gTypSetOpsGraphAnalogMeter)

        Try

            With udtSet

                ''グラフタイトル
                txtGraphTitle.Text = gGetString(.strTitle)

                ''表示タイプ
                cmbDisplayType.SelectedValue = .bytMeterType.ToString

                For i As Integer = 0 To grdAnalogMeter.RowCount - 1

                    With .udtDetail(i)

                        ''CH番号
                        grdAnalogMeter.Rows(i).Cells(0).Value = gConvZeroToNull(.shtChNo, "0000")

                        ''目盛り分割数　自動計算 20200302 hori
                        If grdAnalogMeter.Rows(i).Cells(0).Value = "" Then  'CHNoが空白の場合
                            grdAnalogMeter.Rows(i).Cells(1).Value = gConvZeroToNull(.bytScale)  '空白を返す
                        Else
                            grdAnalogMeter.Rows(i).Cells(1).Value = .bytScale   '自動計算 20200302 hori
                        End If

                        ''表示色
                        grdAnalogMeter.Rows(i).Cells(2).Value = .bytColor


                    End With

                Next

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 行間色の設定
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 行間色を設定する
    '--------------------------------------------------------------------
    Private Sub mSetRowColor()

        Try

            Dim cellStyle As New DataGridViewCellStyle
            Dim cellStyleWhite As New DataGridViewCellStyle

            cellStyle.BackColor = gColorGridRowBack
            cellStyleWhite.BackColor = Color.White

            For i = 0 To grdAnalogMeter.Rows.Count - 1

                If i Mod 2 <> 0 Then
                    ''偶数色
                    grdAnalogMeter.Rows(i).DefaultCellStyle = cellStyle
                Else
                    ''奇数色
                    grdAnalogMeter.Rows(i).DefaultCellStyle = cellStyleWhite
                End If
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '    '----------------------------------------------------------------------------
    '    ' 機能説明  ： グリッドの表示行調整
    '    ' 引数      ： なし
    '    ' 戻値      ： なし
    '    '----------------------------------------------------------------------------
    '    Private Sub mAdjustGridRows()

    '        Try

    '            ''行数調整
    '            Select Case (cmbDisplayType.SelectedIndex + 1)
    '                Case gCstCodeOpsAnalogMeterMeterType8

    '                    ''8行表示
    '                    Call mAdjustGridRowsDetail(mCstCodeNumberOfBmp_8, grdAnalogMeter.RowCount)

    '                Case gCstCodeOpsAnalogMeterMeterType1_4, _
    '                     gCstCodeOpsAnalogMeterMeterType4_1, _
    '                     gCstCodeOpsAnalogMeterMeterType2_1_2

    '                    ''5行表示
    '                    Call mAdjustGridRowsDetail(mCstCodeNumberOfBmp_5, grdAnalogMeter.RowCount)

    '                Case gCstCodeOpsAnalogMeterMeterType2

    '                    ''2行表示
    '                    Call mAdjustGridRowsDetail(mCstCodeNumberOfBmp_2, grdAnalogMeter.RowCount)
    '            End Select

    '            ''行間色設定
    '            Call mSetRowColor()

    '        Catch ex As Exception
    '            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '        End Try

    '    End Sub

    '    '--------------------------------------------------------------------
    '    ' 機能      : グリッドの表示行調整
    '    ' 返り値    : なし
    '    ' 引き数    : ARG1 - (I ) 規定行数
    '    '           : ARG2 - (I ) 現在表示している行数
    '    ' 機能説明  : メータータイプによって表示する行数を調整する
    '    '--------------------------------------------------------------------
    '    Private Sub mAdjustGridRowsDetail(ByVal hintSetRowNum As Integer, _
    '                                      ByVal hintDispGridRowNum As Integer)

    '        Try

    '            Dim i As Integer

    '            ''---------------------
    '            '' 最大数まで行追加
    '            ''---------------------
    '            For i = hintDispGridRowNum + 1 To UBound(mudtSetOpsGraphNew.udtDetail) + 1
    '                grdAnalogMeter.Rows.Add()
    '            Next

    '            ''---------------------
    '            '' 規定数まで行削除
    '            ''---------------------
    '            If grdAnalogMeter.RowCount > hintSetRowNum Then

    '                For i = UBound(mudtSetOpsGraphNew.udtDetail) To hintSetRowNum Step -1

    '                    ''グリッドの行削除
    '                    Call grdAnalogMeter.Rows.Remove(grdAnalogMeter.Rows(i))

    '                    ''構造体のデータ削除
    '                    With mudtSetOpsGraphNew.udtDetail(i)
    '                        .shtChNo = 0    ''CH番号
    '                        .bytScale = 0   ''目盛り分割数
    '                        .bytColor = 0   ''表示色
    '                    End With

    '                Next

    '            End If

    '            ''---------------------
    '            '' 行ヘッダー再設定
    '            ''---------------------
    '            For i = 1 To grdAnalogMeter.RowCount
    '                grdAnalogMeter.Rows(i - 1).HeaderCell.Value = i.ToString
    '            Next

    '        Catch ex As Exception
    '            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '        End Try

    '    End Sub


    '■外販関数
    'CHNOからレンジを取得してScaleを戻す関数
    Public Function fnSetScale(pintCHNO As Integer) As Integer  'Private→Public 自動計算 hori
        Dim intRet As Integer = -1

        Try
            Dim i As Integer = 0
            Dim intCHrec As Integer = -1

            Dim intRangeLow As Integer = 0
            Dim intRangeHigh As Integer = 0

            '計測点から該当レコードを特定
            With gudt.SetChInfo
                For i = 0 To UBound(.udtChannel) Step 1
                    If .udtChannel(i).udtChCommon.shtChno = pintCHNO Then
                        intCHrec = i
                        Exit For
                    End If
                Next i
                '見つからないなら-1で戻る
                If intCHrec = -1 Then
                    Return intRet
                End If

                'アナログCH、バルブAIDO,AIAO以外は-1で戻る。それ以外はレンジを格納
                If .udtChannel(intCHrec).udtChCommon.shtChType = gCstCodeChTypeAnalog Then
                    intRangeLow = .udtChannel(intCHrec).AnalogRangeLow
                    intRangeHigh = .udtChannel(intCHrec).AnalogRangeHigh
                Else
                    If .udtChannel(intCHrec).udtChCommon.shtChType = gCstCodeChTypeValve Then
                        Select Case .udtChannel(intCHrec).udtChCommon.shtData
                            Case gCstCodeChDataTypeValveAI_DO1, gCstCodeChDataTypeValveAI_DO2, gCstCodeChDataTypeValvePT_DO2
                                intRangeLow = .udtChannel(intCHrec).ValveAiDoRangeLow
                                intRangeHigh = .udtChannel(intCHrec).ValveAiDoRangeHigh
                            Case gCstCodeChDataTypeValveAI_AO1, gCstCodeChDataTypeValveAI_AO2, gCstCodeChDataTypeValvePT_AO2
                                intRangeLow = .udtChannel(intCHrec).ValveAiAoRangeLow
                                intRangeHigh = .udtChannel(intCHrec).ValveAiAoRangeHigh
                            Case Else
                                Return intRet
                        End Select
                    Else
                        Return intRet
                    End If
                End If

                '自動計算ロジック追加 20200402 hori
                Dim intAbsLow As Integer    'レンジ下限の絶対値
                Dim intAbsHigh As Integer    'レンジ上限の絶対値
                intAbsLow = System.Math.Abs(intRangeLow)
                intAbsHigh = System.Math.Abs(intRangeHigh)
                If intAbsLow = intAbsHigh Then   '絶対値が等しい(:0がレンジの真ん中に位置する)場合
                    '絶対値が3で割り切れる場合；分割数=6、それ以外：分割数=4
                    If intAbsHigh Mod 3 = 0 Then
                        Return 6
                    Else
                        Return 4
                    End If
                Else

                    'レンジ幅を求める
                    Dim intRangeWidh As Integer = System.Math.Abs(intRangeHigh - intRangeLow)

                    '自動計算ロジック追加　20200402　hori 
                    '上限または下限の値が0でない場合のみ処理を行う
                    If intAbsLow <> 0 And intAbsHigh <> 0 Then
                        'レンジ幅を上限/下限の最大公約数で割った数値が3以上7以下ならその値を分割数とする
                        Dim intScaleForZero As Integer = intRangeWidh / mGetGCM(intAbsLow, intAbsHigh)
                        If intScaleForZero >= 3 And intScaleForZero <= 7 Then
                            Return intScaleForZero
                        End If
                    End If

                    '0がなくなるまで10で割る
                    Dim strRangeWidth As String = intRangeWidh.ToString
                    While (strRangeWidth.Substring(strRangeWidth.Length - 1) = "0")
                        'Ver2.0.7.6 「0」の場合永久ループするため、修正
                        If intRangeWidh <= 0 Then
                            Exit While
                        End If
                        intRangeWidh = intRangeWidh / 10
                        strRangeWidth = intRangeWidh.ToString
                    End While

                    Select Case intRangeWidh
                        Case 1
                            '1の場合はscale=5 hori 20200206
                            Return 5
                        Case 2
                            '2の場合はscale=4 hori
                            Return 4
                        Case 3 To 7
                            '3-7の場合は、scale=その値
                            Return intRangeWidh
                        Case Is >= 8
                            '8以上で
                            '①5で割った結果が、整数かつ8未満である場合はscale=Range/5
                            If intRangeWidh Mod 5 = 0 And intRangeWidh Mod 5 < 8 Then
                                Return intRangeWidh / 5
                            Else
                                '①以外の場合は、scale=3～7のうち、Rangeの最大約数とする
                                For i = 7 To 3 Step -1
                                    If intRangeWidh Mod i = 0 Then
                                        Return i
                                    End If
                                Next i
                                '妙な値の場合は、scale=4
                                Return 4
                            End If
                        Case Else
                            '妙な値の場合は、scale=4
                            Return 4
                    End Select
                End If
            End With

            Return intRet

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function


    '--------------------------------------------------------------------
    ' 機能      : 最大公約数の取得
    ' 返り値    : 最大公約数
    ' 引き数    : ARG1 - (I ) 整数値1
    '           : ARG2 - (I ) 整数値2
    ' 機能説明  : 整数値1と整数値2の最大公約数を求めて、その値を返す
    '--------------------------------------------------------------------
    Private Function mGetGCM(ByVal a As Integer, ByVal b As Integer) As Integer
        Try
            
                Do While a <> b
                    If a > b Then
                        a = a - b
                    Else
                        b = b - a
                    End If
                Loop

            Return a

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

End Class