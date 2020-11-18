Public Class frmOpsGraphBarGraph

#Region "変数定義"

    Private mudtSetOpsGraphNew As gTypSetOpsGraphBar = Nothing
    Private mintRtn As Integer

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 0:OK  <> 0:キャンセル
    ' 引き数    : ARG1 - (IO) OPS設定：バーグラフ設定構造体
    '           : ARG2 - (I ) グラフ設定画面 選択行Index
    ' 機能説明  : 本画面を表示する
    '--------------------------------------------------------------------
    Friend Function gShow(ByRef udtGraphBar As gTypSetOpsGraphBar, _
                          ByVal intRow As Integer, _
                          ByRef frmOwner As Form) As Integer

        Try

            ''ボタン選択フラグ初期化
            mintRtn = 1

            ''引数保存
            mudtSetOpsGraphNew = udtGraphBar

            ''本画面表示
            Call gShowFormModelessForCloseWait2(Me, frmOwner)

            ''OKで閉じる場合は戻り値設定
            If mintRtn = 0 Then udtGraphBar = mudtSetOpsGraphNew

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
    Private Sub frmOpsGraphBarGraph_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''グリッドを初期化する
            Call mInitialDataGrid()

            ''画面設定
            Call mSetDisplay(mudtSetOpsGraphNew)
            txtCylinderCount.BackColor = gColorGridRowBackReadOnly

            ''コントロール使用可/不可設定
            'Call mSetControlEnable()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  ： Okボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '--------------------------------------------------------------------
    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        Try

            ''グリッドの保留中の変更を全て適用させる
            Call grdCylinder.EndEdit()

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
    Private Sub frmOpsGraphBarGraph_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub



#Region "イベント処理"

    '----------------------------------------------------------------------------
    ' 機能説明  ： グラフ本数が17～20の場合 '20 Graph'チェックボックスを有効にする
    ' 引数      ： なし 
    ' 戻値      ： なし 
    '----------------------------------------------------------------------------
    Private Sub txtCylinderCount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCylinderCount.TextChanged

        Try

            Dim intCount As Integer

            If txtCylinderCount.Text <> "" Then intCount = Integer.Parse(txtCylinderCount.Text)

 





            '▼▼▼ 20110425 ファイルデータ１７版対応 20Graph削除 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            'If intCount >= gCstCodeOps20GraphSplitLower And intCount <= gCstCodeOps20GraphSplitUpper Then
            '    chk20Graph.Enabled = True
            '    fra20Graph.Enabled = False
            'Else
            '    chk20Graph.Enabled = False
            '    chk20Graph.Checked = False
            '    fra20Graph.Enabled = False
            'End If
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： コントロール使用可/不可設定を行う
    ' 引数      ： なし 
    ' 戻値      ： なし 
    '----------------------------------------------------------------------------
    Private Sub chk20Graph_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk20Graph.CheckedChanged

        Try

            ''コントロール使用可/不可設定
            'Call mSetControlEnable()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Cylinder CH No. フォーマット
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdCylinder_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCylinder.CellValidated

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)

            Dim Gyou As Integer

            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Return
            If dgv.CurrentCell.OwningColumn.Name <> "txtChNo" Then Return

            ''[CH_NO.]フォーマット
            If IsNumeric(grdCylinder.Rows(e.RowIndex).Cells(0).Value) Then
                grdCylinder.Rows(e.RowIndex).Cells(0).Value() = Integer.Parse(grdCylinder.Rows(e.RowIndex).Cells(0).Value).ToString("0000")
            End If

            ''Setup Cylinder Count 更新
            txtCylinderCount.Text = 0
            For i As Integer = grdCylinder.Rows.Count - 1 To 0 Step -1
                If grdCylinder.Rows(i).Cells(0).Value <> Nothing Then
                    txtCylinderCount.Text = i + 1
                    Exit For
                End If
            Next

            Gyou = Val(txtCylinderCount.Text)

            Select Case Gyou

                Case Is <= 16
                    optLine1.Checked = True
                    optLine1.Enabled = True
                    optLine2.Enabled = False

                Case Is <= 20
                    optLine1.Enabled = True
                    optLine2.Enabled = True


                Case Is >= 21
                    optLine2.Checked = True
                    optLine1.Enabled = False
                    optLine2.Enabled = True

            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "入力制限"

    '----------------------------------------------------------------------------
    ' 機能説明  ： Graph Title 入力制限
    '----------------------------------------------------------------------------
    Private Sub txtGraphTitle_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGraphTitle.KeyPress

        Try

            e.Handled = gCheckTextInput(32, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Name Item Up 入力制限
    '----------------------------------------------------------------------------
    Private Sub txtNameItemUp_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNameItemUp.KeyPress

        Try

            e.Handled = gCheckTextInput(4, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Name Item Down 入力制限
    '----------------------------------------------------------------------------
    Private Sub txtNameItemDown_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNameItemDown.KeyPress

        Try

            e.Handled = gCheckTextInput(4, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベントを発生させる
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdCylinder_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdCylinder.EditingControlShowing

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdCylinder_KeyPress

                ''イベントハンドラを追加する
                AddHandler tb.KeyPress, AddressOf grdCylinder_KeyPress

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
    Private Sub grdCylinder_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdCylinder.KeyPress

        Try

            ''選択セルの名称取得
            Dim strColumnName As String = grdCylinder.CurrentCell.OwningColumn.Name

            ''[CH_NO.]
            If strColumnName = "txtChNo" Then
                e.Handled = gCheckTextInput(5, sender, e.KeyChar)
            End If

            ''[Title]
            If strColumnName = "txtTitle" Then
                e.Handled = gCheckTextInput(5, sender, e.KeyChar, False)
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
    Private Sub grdCylinder_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles grdCylinder.CellValidating

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

            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "txtChNo"
            Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "txtTitle"

            Column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            With grdCylinder

                ''列
                .Columns.Clear()
                .Columns.Add(Column1) : .Columns.Add(Column2)
                .AllowUserToResizeColumns = False   ''列幅の変更不可

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "CH No." : .Columns(0).Width = 80
                .Columns(1).HeaderText = "Title" : .Columns(1).Width = 170
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 24 + 1                  ''行数＋ヘッダー行
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing  ''行ヘッダー幅の変更不可

                ''行ヘッダー
                For i = 0 To .RowCount - 1
                    .Rows(i).HeaderCell.Value = (i + 1).ToString
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

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdCylinder)

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
            Call grdCylinder.EndEdit()

            Dim i As Integer
            'Dim j As Integer
            Dim strChNo As String, strTitle As String

            With grdCylinder

                For i = 0 To .RowCount - 1

                    ''-----------------------------
                    '' レンジチェック
                    ''-----------------------------
                    If Not gChkInputNum(.Rows(i).Cells("txtChNo"), 0, 65535, "CH No.", i + 1, True, True) Then Return False

                    ''-----------------------------
                    '' 入力項目チェック
                    ''-----------------------------
                    strChNo = gGetString(.Rows(i).Cells("txtChNo").Value)
                    strTitle = gGetString(.Rows(i).Cells("txtTitle").Value)

                    If strChNo <> "" And strTitle <> "" Then
                        ''OK

                    ElseIf strChNo = "" And strTitle = "" Then
                        ''OK

                    Else
                        If strChNo = "" Then
                            Call MessageBox.Show("Please set 'CH No.' data of the line No." & i + 1, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return False
                        End If

                        'If strTitle = "" Then
                        '    Call MessageBox.Show("Please set 'Title' data of the line No." & i + 1, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        '    Return False
                        'End If

                    End If

                    ''-----------------------------
                    '' CH番号の重複登録は不可
                    ''-----------------------------
                    'For j = i + 1 To .RowCount - 1

                    '    If gGetString(grdCylinder(0, i).Value) <> "" Then

                    '        If gGetString(grdCylinder(0, i).Value) = gGetString(grdCylinder(0, j).Value) Then

                    '            Call MessageBox.Show("The same name as [CH No.] cannot be set of CH No [" & grdCylinder(0, i).Value & "] and CH No [" & grdCylinder(0, j).Value & "].", _
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
    Private Sub mSetStructure(ByRef udtSet As gTypSetOpsGraphBar)

        Try

            With udtSet

                '▼▼▼ 20110607 PRG変更対応（前後のスペース削除を行わない）▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                .strTitle = txtGraphTitle.Text                  ''グラフタイトル
                .strItemUp = txtNameItemUp.Text                 ''グラフデータ名称（上段）
                .strItemDown = txtNameItemDown.Text             ''グラフデータ名称（下段）
                '----------------------------------------------------------------------------------------------------
                '.strTitle = Trim(txtGraphTitle.Text)           ''グラフタイトル
                '.strItemUp = Trim(txtNameItemUp.Text)          ''グラフデータ名称（上段）
                '.strItemDown = Trim(txtNameItemDown.Text)      ''グラフデータ名称（下段）
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                .bytCyCnt = CCbyte(txtCylinderCount.Text)       ''シリンダの数

                '▼▼▼ 20110330 ファイルデータ１７版対応 20Graph→表示切替に変更 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                If optDisplayPercentage.Checked Then
                    .bytDisplay = 1
                Else
                    .bytDisplay = 0
                End If

                ''Line設定
                If optLine2.Checked Then .bytLine = 2 '' 2：2Line
                If optLine1.Checked Then .bytLine = 1 '' 1：1Line
                '---------------------------------------------------------------------------------------------------
                '.byt20Graph = IIf(chk20Graph.Checked, 1, 0)     ''グラフ20本区切り

                ''Line設定
                'If Not chk20Graph.Checked Then
                '    .bytLine = 0                          '' 0：設定なし
                'Else
                '    If optLine2.Checked Then .bytLine = 2 '' 2：2Line
                '    If optLine1.Checked Then .bytLine = 1 '' 1：1Line
                'End If
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                ''分割数
                If optDivision0.Checked Then .bytDevision = 1 '' 4分割
                If optDivision1.Checked Then .bytDevision = 2 '' 6分割
                If optDivision2.Checked Then .bytDevision = 3 '' 3×5分割

                ''--------------------
                '' 設定詳細
                ''--------------------
                For i As Integer = 0 To UBound(.udtCylinder)

                    ''CH_NO.
                    .udtCylinder(i).shtChCylinder = CCUInt16(grdCylinder.Rows(i).Cells(0).Value)

                    ''Title
                    ' ver1.4.0 2011.07.21 前後のスペース削除を行わない
                    '.udtCylinder(i).strTitle = Trim(grdCylinder.Rows(i).Cells(1).Value)
                    .udtCylinder(i).strTitle = grdCylinder.Rows(i).Cells(1).Value

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
    Private Sub mSetDisplay(ByVal udtSet As gTypSetOpsGraphBar)

        Try

            Dim Gyou As Integer

            With udtSet

                '▼▼▼ 2011.06.07 PRG変更対応（前後のスペース削除を行わない）▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                txtGraphTitle.Text = .strTitle         ''グラフタイトル
                txtNameItemUp.Text = .strItemUp        ''グラフデータ名称（上段）
                txtNameItemDown.Text = .strItemDown    ''グラフデータ名称（下段）
                '----------------------------------------------------------------------------------------------------
                'txtGraphTitle.Text = gGetString(.strTitle)         ''グラフタイトル
                'txtNameItemUp.Text = gGetString(.strItemUp)        ''グラフデータ名称（上段）
                'txtNameItemDown.Text = gGetString(.strItemDown)    ''グラフデータ名称（下段）
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                txtCylinderCount.Text = gGetString(.bytCyCnt)       ''シリンダの数

                '▼▼▼ 2011.03.30 ファイルデータ１７版対応 20Graph→表示切替に変更 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                Select Case .bytDisplay
                    Case 0
                        optDisplayMeasurement.Checked = True
                        optDisplayPercentage.Checked = False
                    Case 1
                        optDisplayMeasurement.Checked = False
                        optDisplayPercentage.Checked = True
                End Select
                '---------------------------------------------------------------------------------------------------
                ''グラフ20本区切り
                'chk20Graph.Checked = IIf(.byt20Graph, True, False)
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                ''Line設定
                'Select Case .bytLine
                '    Case gCstCodeOpsBarGraphLine2
                '        optLine2.Checked = True
                '        optLine1.Checked = False
                '    Case gCstCodeOpsBarGraphLine1
                '        optLine2.Checked = False
                '        optLine1.Checked = True
                '    Case Else
                '        optLine2.Checked = False
                '        optLine1.Checked = False
                'End Select

                Gyou = Val(txtCylinderCount.Text)

                Select Case Gyou

                    Case Is <= 16
                        optLine1.Checked = True
                        optLine1.Enabled = True
                        optLine2.Enabled = False

                    Case Is <= 20

                        Select Case .bytLine
                            Case gCstCodeOpsBarGraphLine2
                                optLine2.Checked = True
                                optLine1.Checked = False
                            Case gCstCodeOpsBarGraphLine1
                                optLine2.Checked = False
                                optLine1.Checked = True
                            Case Else
                                optLine2.Checked = False
                                optLine1.Checked = False
                        End Select

                        optLine1.Enabled = True
                        optLine2.Enabled = True


                    Case Is >= 21
                        optLine2.Checked = True
                        optLine1.Enabled = False
                        optLine2.Enabled = True

                End Select


                ''分割数
                Select Case .bytDevision

                    Case gCstCodeOpsBarGraphDivision4  ''4分割選択
                        optDivision0.Checked = True   ''4分割
                        optDivision1.Checked = False  ''6分割
                        optDivision2.Checked = False  ''3×5分割

                    Case gCstCodeOpsBarGraphDivision6  ''6分割選択
                        optDivision0.Checked = False
                        optDivision1.Checked = True
                        optDivision2.Checked = False

                    Case gCstCodeOpsBarGraphDivision3_5  ''3ｘ5分割選択
                        optDivision0.Checked = False
                        optDivision1.Checked = False
                        optDivision2.Checked = True

                    Case Else
                        optDivision0.Checked = False
                        optDivision1.Checked = False
                        optDivision2.Checked = False

                End Select

                ''--------------------
                '' 設定詳細
                ''--------------------
                For i As Integer = 0 To UBound(.udtCylinder)

                    ''CH_NO.
                    grdCylinder.Rows(i).Cells(0).Value = gConvZeroToNull(.udtCylinder(i).shtChCylinder, "0000")

                    ''Title
                    ' ver1.4.0 2011.07.21 前後のスペース削除を行わない
                    'grdCylinder.Rows(i).Cells(1).Value = gGetString(.udtCylinder(i).strTitle)
                    grdCylinder.Rows(i).Cells(1).Value = .udtCylinder(i).strTitle

                Next

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : コントロール使用可/不可設定
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 各チェックボックの状態から関連フレームの使用可/不可を設定する
    '--------------------------------------------------------------------
    'Private Sub mSetControlEnable()

    '    Try

    '        ''1Line/2Lineフレーム
    '        fra20Graph.Enabled = IIf(chk20Graph.Checked, True, False)

    '    Catch ex As Exception
    '        Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '    End Try

    'End Sub

#End Region

End Class
