Public Class frmOpsGraphExtGus

#Region "変数定義"

    Private mudtSetOpsGraphNew As gTypSetOpsGraphExhaust = Nothing
    Private mintRtn As Integer
    Private mblnInitFlg As Boolean

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 0：OK  <> 0：キャンセル
    ' 引き数    : ARG1 - (IO) OPS設定：偏差グラフ設定構造体
    '           : ARG2 - (I ) グラフ設定画面 選択行Index
    ' 機能説明  : 本画面を表示する
    '--------------------------------------------------------------------
    Friend Function gShow(ByRef udtExhaust As gTypSetOpsGraphExhaust, _
                          ByVal intRow As Integer, _
                          ByRef frmOwner As Form) As Integer

        Try

            ''ボタン選択フラグ初期化
            mintRtn = 1

            ''引数保存
            mudtSetOpsGraphNew = udtExhaust

            ''本画面表示
            Call gShowFormModelessForCloseWait2(Me, frmOwner)

            ''OKで閉じる場合は戻り値設定
            If mintRtn = 0 Then udtExhaust = mudtSetOpsGraphNew

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
    Private Sub frmOpsGraphExtGus_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''初期化フラグ
            mblnInitFlg = True

            ''グリッドを初期化する
            Call mInitialDataGrid()

            ''画面設定
            Call mSetDisplay(mudtSetOpsGraphNew)
            txtCylinderCount.BackColor = gColorGridRowBackReadOnly

            ''背景色設定
            Call mColorSet()

            ''T/C区切り線の部品コントロール
            Call mSetControlEnable()

            ''初期化フラグ
            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  ： Okボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '--------------------------------------------------------------------
    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click

        Try

            ''グリッドの保留中の変更を全て適用させる
            Call grdCylinder.EndEdit()
            Call grdTC.EndEdit()

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
    Private Sub frmOpsGraphExtGus_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub





#Region "20Graph CheckBoxイベント処理"

    ''----------------------------------------------------------------------------
    '' 機能説明  ： 20 Graph チェックボックスクリック
    '' 引数      ： なし
    '' 戻値      ： なし
    ''----------------------------------------------------------------------------
    'Private Sub chk20Graph_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        ''コントロール使用可/不可設定
    '        fra20Graph.Enabled = IIf(chk20Graph.Checked, True, False)

    '    Catch ex As Exception
    '        Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '    End Try

    'End Sub

#End Region

#Region "フォーマット処理"

    '----------------------------------------------------------------------------
    ' 機能説明  ： AVE CH No. フォーマット
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtAvgChNo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAvgChNo.Validated

        Try

            If IsNumeric(txtAvgChNo.Text) Then
                txtAvgChNo.Text = Integer.Parse(txtAvgChNo.Text).ToString("0000")
            End If

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

            ''処理を抜ける条件
            If mblnInitFlg Then Return ''初期化中の場合
            If e.RowIndex < 0 Or e.RowIndex > grdCylinder.RowCount - 1 Then Return ''行数が0より小さい、もしくは最大行数より大きい場合
            If e.ColumnIndex < 0 Or e.ColumnIndex > grdCylinder.ColumnCount - 1 Then Return ''列数が0より小さい、もしくは最大列数より大きい場合

            Dim dgv As DataGridView = CType(sender, DataGridView)
            Dim Gyou As Integer

            ''[Cylinder CH_NO.]フォーマット
            If dgv.CurrentCell.OwningColumn.Name = "txtCylinder" Then
                If IsNumeric(grdCylinder.Rows(e.RowIndex).Cells("txtCylinder").Value) Then
                    grdCylinder.Rows(e.RowIndex).Cells("txtCylinder").Value() = Integer.Parse(grdCylinder.Rows(e.RowIndex).Cells("txtCylinder").Value).ToString("0000")
                End If
            End If

            ''[Deviation CH_NO.]フォーマット
            If dgv.CurrentCell.OwningColumn.Name = "txtDeviation" Then
                If IsNumeric(grdCylinder.Rows(e.RowIndex).Cells("txtDeviation").Value) Then
                    grdCylinder.Rows(e.RowIndex).Cells("txtDeviation").Value() = Integer.Parse(grdCylinder.Rows(e.RowIndex).Cells("txtDeviation").Value).ToString("0000")
                End If
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

                Case Is < 16
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





            ''20 Graph チェックボックス
            Call mCheckCylTcCount()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： T/C CH No. フォーマット
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdTC_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdTC.CellValidated

        Try

            ''処理を抜ける条件
            If mblnInitFlg Then Return ''初期化中の場合
            If e.RowIndex < 0 Or e.RowIndex > grdTC.RowCount - 1 Then Return ''行数が0より小さい、もしくは最大行数より大きい場合
            If e.ColumnIndex < 0 Or e.ColumnIndex > grdTC.ColumnCount - 1 Then Return ''列数が0より小さい、もしくは最大列数より大きい場合

            Dim dgv As DataGridView = CType(sender, DataGridView)
            If dgv.CurrentCell.OwningColumn.Name <> "txtTC" Then Return ''T/C CH_NO 列以外の場合

            ''[T/C CH_NO.]フォーマット
            If IsNumeric(grdTC.Rows(e.RowIndex).Cells("txtTC").Value) Then
                grdTC.Rows(e.RowIndex).Cells("txtTC").Value() = Integer.Parse(grdTC.Rows(e.RowIndex).Cells("txtTC").Value).ToString("0000")
            End If

            ''Setup Cylinder Count 更新
            txtCylinderCount.Text = 0
            For i = grdCylinder.Rows.Count - 1 To 0 Step -1
                If grdCylinder.Rows(i).Cells(0).Value <> Nothing Then
                    txtCylinderCount.Text = i + 1
                    Exit For
                End If
            Next

            ''20 Graph チェックボックス
            Call mCheckCylTcCount()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "入力制限"

#Region "テキストボックス"

    Private Sub txtGraphTitle_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGraphTitle.KeyPress

        Try

            e.Handled = gCheckTextInput(32, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtNameItemUp_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNameItemUp.KeyPress

        Try

            e.Handled = gCheckTextInput(4, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtNameItemDown_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNameItemDown.KeyPress

        Try

            e.Handled = gCheckTextInput(4, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtCylinderCount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCylinderCount.KeyPress

        Try

            e.Handled = gCheckTextInput(2, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtTcTitile_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTcTitile.KeyPress

        Try

            e.Handled = gCheckTextInput(16, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtTcComm1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTcComm1.KeyPress


        Try

            e.Handled = gCheckTextInput(4, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtTcComm2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTcComm2.KeyPress

        Try

            e.Handled = gCheckTextInput(4, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtAvgChNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAvgChNo.KeyPress

        Try

            e.Handled = gCheckTextInput(5, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtTcCount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTcCount.KeyPress

        Try

            e.Handled = gCheckTextInput(2, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "データグリッド"

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

    Private Sub grdTC_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdTC.EditingControlShowing

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdTC_KeyPress

                ''イベントハンドラを追加する
                AddHandler tb.KeyPress, AddressOf grdTC_KeyPress

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub grdCylinder_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdCylinder.KeyPress

        Try

            ''選択セルの名称取得
            Dim strColumnName As String = grdCylinder.CurrentCell.OwningColumn.Name

            ''[Cylinder CH_NO.], [Deviation CH_NO.]
            If strColumnName = "txtCylinder" Or strColumnName = "txtDeviation" Then
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

    Private Sub grdTC_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdTC.KeyPress

        Try

            ''選択セルの名称取得
            Dim strColumnName As String = grdTC.CurrentCell.OwningColumn.Name

            ''[T/C CH_NO.]
            If strColumnName = "txtTC" Then
                e.Handled = gCheckTextInput(5, sender, e.KeyChar)
            End If

            ''[Title]
            If strColumnName = "txtTCTitle" Then
                e.Handled = gCheckTextInput(5, sender, e.KeyChar, False)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#End Region

#Region "入力チェック"

    Private Sub txtTcCount_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtTcCount.Validating

        Try

            ''入力レンジ制限チェック
            e.Cancel = gChkTextNumSpanExtGus(4, 8, 0, sender.Text)

            ''入力範囲エラーの時は表示更新を行わない
            If Not e.Cancel Then

                ''T/C 前回値保持
                Dim intTcCntBefore As Integer = CCInt(mudtSetOpsGraphNew.bytTcCnt)

                ''T/C 手入力した値の保持
                mudtSetOpsGraphNew.bytTcCnt = CCInt(sender.text)

                ''行数設定
                Call mSetTcRowAdjust(intTcCntBefore)

                ''コントロール設定
                Call mSetTcControl()

                ''背景色設定
                Call mSetTcRowBackColor()

                ''20Graph設定
                Call mCheckCylTcCount()

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtAvgChNo_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtAvgChNo.Validating

        Try

            ''------------------------------------------------------------
            '' 入力チェックは mChkInput で実施するよう変更
            ''------------------------------------------------------------

            'e.Cancel = gChkTextNumSpan(0, 65535, sender.Text)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub grdCylinder_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles grdCylinder.CellValidating

        Try

            ''------------------------------------------------------------
            '' 入力チェックは mChkInput で実施するよう変更
            ''------------------------------------------------------------

            ''Dim dgv As DataGridView = CType(sender, DataGridView)
            ''Dim strColumnName = dgv.Columns(e.ColumnIndex).Name

            '' ''[Cylinder CH_NO.], [Deviation CH_NO.]
            ''If strColumnName = "txtCylinder" Or strColumnName = "txtDeviation" Then
            ''    e.Cancel = gChkTextNumSpan(0, 65535, e.FormattedValue)
            ''End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub grdTC_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles grdTC.CellValidating

        Try

            ''------------------------------------------------------------
            '' 入力チェックは mChkInput で実施するよう変更
            ''------------------------------------------------------------

            ''Dim dgv As DataGridView = CType(sender, DataGridView)
            ''Dim strColumnName = dgv.Columns(e.ColumnIndex).Name

            '' ''[T/C CH_NO.]
            ''If strColumnName = "txtTC" Then
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

            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "txtCylinder"
            Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "txtDeviation"
            Dim Column3 As New DataGridViewTextBoxColumn : Column3.Name = "txtTitle"

            Dim Column10 As New DataGridViewTextBoxColumn : Column10.Name = "txtTC"
            Dim Column11 As New DataGridViewTextBoxColumn : Column11.Name = "txtTCTitle"
            Dim Column12 As New DataGridViewCheckBoxColumn : Column12.Name = "chkSplitLine"

            Column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Column2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Column10.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            With grdCylinder

                ''列
                .Columns.Clear()
                .Columns.Add(Column1) : .Columns.Add(Column2) : .Columns.Add(Column3)
                .AllowUserToResizeColumns = False   ''列幅の変更不可

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "Cylinder CH No." : .Columns(0).Width = 70
                .Columns(1).HeaderText = "Deviation CH No." : .Columns(1).Width = 70
                .Columns(2).HeaderText = "Title" : .Columns(2).Width = 200
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

            With grdTC

                ''列
                .Columns.Clear()
                .Columns.Add(Column10) : .Columns.Add(Column11) : .Columns.Add(Column12)
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing  ''行ヘッダー幅の変更不可

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "T/C CH No." : .Columns(0).Width = 80
                .Columns(1).HeaderText = "Title" : .Columns(1).Width = 155
                .Columns(2).HeaderText = "Split Line" : .Columns(2).Width = 45
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 8 + 1                   ''配列と列ヘッダー分：計：＋２
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする

                ''行ヘッダー
                For i = 0 To .RowCount - 1
                    .Rows(i).HeaderCell.Value = (i + 1).ToString
                Next
                .RowHeadersWidth = 50

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

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdTC)

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

            Dim i As Integer
            'Dim j As Integer
            Dim strCylChNo As String, strDevChNo As String, strCylTitle As String
            Dim strTcChNo As String, strTcTitle As String

            ''=============================
            '' AVG CH 入力チェック
            ''=============================
            If Not gChkInputNum(txtAvgChNo, 0, 65535, "AVG CH No.", True, True) Then Return False ''平均値出力CH
            ''T/C CH_NO. は即レスポンスが必要なため、グリッドのCellValidatingでレンジチェックを実施  

            ''=============================
            '' Cylinder
            ''=============================
            With grdCylinder

                For i = 0 To .RowCount - 1

                    ''-----------------------------
                    '' レンジチェック
                    ''-----------------------------
                    If Not gChkInputNum(.Rows(i).Cells("txtCylinder"), 0, 65535, "Cylinder CH No.", i + 1, True, True) Then Return False
                    If Not gChkInputNum(.Rows(i).Cells("txtDeviation"), 0, 65535, "Deviation CH No.", i + 1, True, True) Then Return False

                    ''-----------------------------
                    '' 入力チェック
                    ''-----------------------------
                    strCylChNo = gGetString(.Rows(i).Cells("txtCylinder").Value)
                    strDevChNo = gGetString(.Rows(i).Cells("txtDeviation").Value)
                    strCylTitle = gGetString(.Rows(i).Cells("txtTitle").Value)


                    If strCylChNo <> "" And strDevChNo <> "" And strCylTitle <> "" Then
                        ''OK
                    ElseIf strCylChNo = "" And strDevChNo = "" And strCylTitle = "" Then
                        ''OK
                    Else
                        If strCylChNo = "" Then
                            Call MessageBox.Show("Please set 'Cyl CH No.' data of the line No." & i + 1, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return False
                        End If

                        If strDevChNo = "" Then
                            Call MessageBox.Show("Please set 'Dev CH No.' data of the line No." & i + 1, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return False
                        End If

                        'If strCylTitle = "" Then
                        '    Call MessageBox.Show("Please set 'Title' data of the line No." & i + 1, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        '    Return False
                        'End If

                    End If

                    ''-----------------------------
                    '' CH番号の重複登録は不可
                    ''-----------------------------
                    'For j = i + 1 To .RowCount - 1

                    '    ''Cylinder
                    '    If gGetString(grdCylinder(0, i).Value) <> "" Then

                    '        If gGetString(grdCylinder(0, i).Value) = gGetString(grdCylinder(0, j).Value) Then

                    '            Call MessageBox.Show("The same name as [Cyl_CH No.] cannot be set of CH No [" & grdCylinder(0, i).Value & "] and CH No [" & grdCylinder(0, j).Value & "].", _
                    '                                 "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '            Return False

                    '        End If

                    '    End If

                    '    ''Deviation
                    '    If gGetString(grdCylinder(1, i).Value) <> "" Then

                    '        If gGetString(grdCylinder(1, i).Value) = gGetString(grdCylinder(1, j).Value) Then

                    '            Call MessageBox.Show("The same name as [Dev_CH No.] cannot be set of CH No [" & grdCylinder(1, i).Value & "] and CH No [" & grdCylinder(1, j).Value & "].", _
                    '                                 "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '            Return False

                    '        End If

                    '    End If

                    'Next j

                Next i

            End With

            ''=============================
            '' Turbo Charger
            ''=============================
            With grdTC

                For i = 0 To .RowCount - 1

                    ''-----------------------------
                    '' レンジチェック
                    ''-----------------------------
                    If Not gChkInputNum(.Rows(i).Cells("txtTC"), 0, 65535, "T/C CH No.", i + 1, True, True) Then Return False

                    ''-----------------------------
                    '' 入力チェック
                    ''-----------------------------
                    strTcChNo = gGetString(.Rows(i).Cells("txtTC").Value)
                    strTcTitle = gGetString(.Rows(i).Cells("txtTCTitle").Value)


                    If strTcChNo <> "" And strTcTitle <> "" Then
                        ''OK
                    ElseIf strTcChNo = "" And strTcTitle = "" Then
                        ''OK
                    Else
                        If strTcChNo = "" Then
                            Call MessageBox.Show("Please set 'T/C CH No.' data of the line No." & i + 1, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return False
                        End If

                        'If strTcTitle = "" Then
                        '    Call MessageBox.Show("Please set 'T/C Title' data of the line No." & i + 1, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        '    Return False
                        'End If
                    End If

                    ''-----------------------------
                    '' CH番号の重複登録は不可
                    ''-----------------------------
                    'For j = i + 1 To .RowCount - 1

                    '    If gGetString(grdTC(0, i).Value) <> "" Then

                    '        If gGetString(grdTC(0, i).Value) = gGetString(grdTC(0, j).Value) Then

                    '            Call MessageBox.Show("The same name as [CH No.] cannot be set of CH No [" & grdTC(0, i).Value & "] and CH No [" & grdTC(0, j).Value & "].", _
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
    Private Sub mSetStructure(ByRef udtSet As gTypSetOpsGraphExhaust)

        Try

            Dim i As Integer

            With udtSet

                '▼▼▼ 20110607 PRG変更対応（前後のスペース削除を行わない）▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                .strTitle = txtGraphTitle.Text                  ''グラフタイトル
                .strItemUp = txtNameItemUp.Text                 ''グラフデータ名称（上段）
                .strItemDown = txtNameItemDown.Text             ''グラフデータ名称（下段）
                .strTcTitle = txtTcTitile.Text                  ''T/Cグラフのタイトル
                .strTcComm1 = txtTcComm1.Text                   ''T/Cコメント1
                .strTcComm2 = txtTcComm2.Text                   ''T/Cコメント2
                '---------------------------------------------------------------------------------------------------
                '.strTitle = Trim(txtGraphTitle.Text)           ''グラフタイトル
                '.strItemUp = Trim(txtNameItemUp.Text)          ''グラフデータ名称（上段）
                '.strItemDown = Trim(txtNameItemDown.Text)      ''グラフデータ名称（下段）
                '.strTcTitle = Trim(txtTcTitile.Text)           ''T/Cグラフのタイトル
                '.strTcComm1 = Trim(txtTcComm1.Text)            ''T/Cコメント1
                '.strTcComm2 = Trim(txtTcComm2.Text)            ''T/Cコメント2
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                .shtAveCh = CCUInt16(txtAvgChNo.Text)           ''平均CH
                .bytDevMark = CCbyte(numDevMark.Value)          ''偏差目盛の上下限値
                .bytCyCnt = Trim(txtCylinderCount.Text)         ''Cyl数
                .bytTcCnt = Trim(txtTcCount.Text)               ''T/C数

                '▼▼▼ 20110330 ファイルデータ１７版対応 20Graph削除 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                '.byt20Graph = IIf(chk20Graph.Checked, 1, 0)     ''グラフ20本区切り
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                '▼▼▼ 20110330 ファイルデータ１７版対応 20Graph削除 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                ''Line設定
                If optLine2.Checked Then .bytLine = 2 '' 2：2Line
                If optLine1.Checked Then .bytLine = 1 '' 1：1Line
                '---------------------------------------------------------------------------------------------------
                'If Not chk20Graph.Checked Then
                '    .bytLine = 0                                '' 0：設定なし
                'Else
                '    If optLine2.Checked Then .bytLine = 2 '' 2：2Line
                '    If optLine1.Checked Then .bytLine = 1 '' 1：1Line
                'End If
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                ''グリッドの設定情報クリア
                For i = 0 To UBound(.udtCylinder)
                    Call gInitOpsGraphExhaustCylinder(.udtCylinder(i))
                Next
                For i = 0 To UBound(.udtTurboCharger)
                    Call gInitOpsGraphExhaustTurboCharger(.udtTurboCharger(i))
                Next

                ''--------------------
                '' Cylinder
                ''--------------------
                For i = 0 To grdCylinder.Rows.Count - 1

                    With .udtCylinder(i)

                        ''Cyl CH_NO.
                        .shtChCylinder = CCUInt16(grdCylinder.Rows(i).Cells(0).Value)

                        ''Dev CH_NO.
                        .shtChDeviation = CCUInt16(grdCylinder.Rows(i).Cells(1).Value)

                        ''Title
                        ' ver1.4.0 2011.07.21 前後のスペース削除を行わない
                        '.strTitle = Trim(grdCylinder.Rows(i).Cells(2).Value)
                        .strTitle = grdCylinder.Rows(i).Cells(2).Value

                    End With

                Next

                ''--------------------
                '' T/C
                ''--------------------
                For i = 0 To grdTC.Rows.Count - 1

                    With .udtTurboCharger(i)

                        ''T/C CH_NO.
                        .shtChTurboCharger = CCUInt16(grdTC.Rows(i).Cells(0).Value)

                        ''Title
                        ' ver1.4.0 2011.07.21 前後のスペース削除を行わない
                        '.strTitle = Trim(grdTC.Rows(i).Cells(1).Value)
                        .strTitle = grdTC.Rows(i).Cells(1).Value

                        ''Split Line
                        .bytSplitLine = IIf(grdTC.Rows(i).Cells(2).Value, 1, 0)

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
    Private Sub mSetDisplay(ByVal udtSet As gTypSetOpsGraphExhaust)

        Try

            Dim Gyou As Integer

            With udtSet

                '▼▼▼ 2011.06.07 PRG変更対応（前後のスペース削除を行わない）▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                txtGraphTitle.Text = .strTitle                          ''グラフタイトル
                txtNameItemUp.Text = .strItemUp                         ''グラフデータ名称（上段）
                txtNameItemDown.Text = .strItemDown                     ''グラフデータ名称（下段）
                txtTcTitile.Text = .strTcTitle                          ''T/Cグラフのタイトル
                txtTcComm1.Text = .strTcComm1                           ''T/Cグラフのコメント1
                txtTcComm2.Text = .strTcComm2                           ''T/Cグラフのコメント2
                '---------------------------------------------------------------------------------------------------
                'txtGraphTitle.Text = gGetString(.strTitle)             ''グラフタイトル
                'txtNameItemUp.Text = gGetString(.strItemUp)            ''グラフデータ名称（上段）
                'txtNameItemDown.Text = gGetString(.strItemDown)        ''グラフデータ名称（下段）
                'txtTcTitile.Text = gGetString(.strTcTitle)             ''T/Cグラフのタイトル
                'txtTcComm1.Text = gGetString(.strTcComm1)              ''T/Cグラフのコメント1
                'txtTcComm2.Text = gGetString(.strTcComm2)              ''T/Cグラフのコメント2
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                txtCylinderCount.Text = gGetString(.bytCyCnt)           ''シリンダの数
                txtTcCount.Text = gGetString(.bytTcCnt)                 ''T/Cの数
                txtAvgChNo.Text = gConvZeroToNull(.shtAveCh, "0000")    ''平均CH
                numDevMark.Value = .bytDevMark.ToString                 ''偏差目盛の上下限値

                '▼▼▼ 2011.03.30 ファイルデータ１７版対応 20Graph削除 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                ' ''グラフ20本区切り
                'If (.bytCyCnt + .bytTcCnt) >= gCstCodeOps20GraphSplitLower And _
                '   (.bytCyCnt + .bytTcCnt) <= gCstCodeOps20GraphSplitUpper Then

                '    ''チェックボックスの有効/無効
                '    chk20Graph.Enabled = True

                '    ''チェックボックス
                '    chk20Graph.Checked = IIf(.byt20Graph, True, False)

                'Else

                '    ''チェックボックスの有効/無効
                '    chk20Graph.Enabled = False

                'End If
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                ''Line設定
                'Select Case .bytLine
                '    Case gCstCodeOpsExhGraphLine2
                '        optLine2.Checked = True
                '        optLine1.Checked = False

                '    Case gCstCodeOpsExhGraphLine1
                '        optLine2.Checked = False
                '        optLine1.Checked = True

                '    Case Else
                '        optLine2.Checked = False
                '        optLine1.Checked = False
                'End Select

                Gyou = Val(txtCylinderCount.Text)

                Select Case Gyou

                    Case Is < 16
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

                ''グリッドの数をMAXまで増やす
                Call mSetGridRowMax()

                ''---------------------
                '' Cylinder
                ''---------------------
                For i As Integer = 0 To UBound(.udtCylinder)

                    With .udtCylinder(i)

                        ''Cyl CH_NO.
                        grdCylinder.Rows(i).Cells(0).Value = gConvZeroToNull(.shtChCylinder, "0000")

                        ''Dev CH_NO.
                        grdCylinder.Rows(i).Cells(1).Value = gConvZeroToNull(.shtChDeviation, "0000")

                        ''Title
                        ' ver1.4.0 2011.07.21 前後のスペース削除を行わない
                        'grdCylinder.Rows(i).Cells(2).Value = gGetString(.strTitle)
                        grdCylinder.Rows(i).Cells(2).Value = .strTitle

                    End With

                Next

                ''---------------------
                '' T/C
                ''---------------------
                For i As Integer = 0 To UBound(.udtTurboCharger)

                    With .udtTurboCharger(i)

                        ''T/C CH_NO.
                        grdTC.Rows(i).Cells(0).Value = gConvZeroToNull(.shtChTurboCharger, "0000")

                        ''Title
                        ' ver1.4.0 2011.07.21 前後のスペース削除を行わない
                        'grdTC.Rows(i).Cells(1).Value = gGetString(.strTitle)
                        grdTC.Rows(i).Cells(1).Value = .strTitle

                        ''Split Line
                        grdTC.Rows(i).Cells(2).Value = IIf(.bytSplitLine, True, False)

                    End With

                Next

                ''不要行の削除
                Call mSetGridRowCount(.bytTcCnt)

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : グリッドの行追加
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : グリッドの行数を最大値に設定
    '--------------------------------------------------------------------
    Private Sub mSetGridRowMax()

        Try

            Dim cellStyle As New DataGridViewCellStyle

            For i As Integer = grdCylinder.Rows.Count + 1 To gCstCodeOpsRowCountExhBar
                grdCylinder.Rows.Add()
            Next

            For i As Integer = grdTC.Rows.Count + 1 To gCstCodeOpsRowCountAnalog
                grdTC.Rows.Add()
            Next


            ''Cyl 偶数行の背景色を変える
            cellStyle.BackColor = gColorGridRowBack
            For i = 0 To grdTC.Rows.Count - 1

                ''行ヘッダー設定
                grdTC.Rows(i).HeaderCell.Value = CStr(i + 1)

                If i Mod 2 <> 0 Then
                    grdTC.Rows(i).DefaultCellStyle = cellStyle
                End If

            Next

            ''Dev 偶数行の背景色を変える
            cellStyle.BackColor = gColorGridRowBack
            For i = 0 To grdCylinder.Rows.Count - 1

                ''行ヘッダー設定
                grdCylinder.Rows(i).HeaderCell.Value = CStr(i + 1)

                If i Mod 2 <> 0 Then
                    grdCylinder.Rows(i).DefaultCellStyle = cellStyle
                End If

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : グリッドの行削除
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) T/C Count
    ' 機能説明  : T/C Count 設定によって行削除
    '--------------------------------------------------------------------
    Private Sub mSetGridRowCount(ByVal intRowCountTC As Integer)

        Try

            For i As Integer = (gCstCodeOpsRowCountExhBar - 1) To (gCstCodeOpsRowCountExhBar - 1) - intRowCountTC + 1 Step -1
                Call grdCylinder.Rows.Remove(grdCylinder.Rows(i))
            Next

            For i As Integer = (gCstCodeOpsRowCountAnalog - 1) To intRowCountTC Step -1
                Call grdTC.Rows.Remove(grdTC.Rows(i))
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : コントロール設定
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 部品コントロールの使用可/不可を設定する
    '--------------------------------------------------------------------
    Private Sub mSetControlEnable()

        Try

            ''----------------------------------------------
            '' 20Graph－1Line/2Lineフレーム使用可/不可設定
            ''----------------------------------------------
            '▼▼▼ 20110330 ファイルデータ１７版対応 20Graph削除 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            'fra20Graph.Enabled = IIf(chk20Graph.Checked, True, False)
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            ''----------------------------------------------
            '' T/C 区切り線の最後尾は使用不可
            ''----------------------------------------------
            If CCInt(mudtSetOpsGraphNew.bytTcCnt) > 0 Then

                ''使用不可設定
                grdTC.Rows(CCInt(mudtSetOpsGraphNew.bytTcCnt) - 1).Cells("chkSplitLine").ReadOnly = True

                ''使用不可色の設定
                grdTC.Rows(CCInt(mudtSetOpsGraphNew.bytTcCnt) - 1).Cells("chkSplitLine").Style.BackColor = gColorGridRowBackReadOnly

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : コントロール設定
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : CylCountとTcCountによって20Graphの使用可/不可設定を行う
    '--------------------------------------------------------------------
    Private Sub mCheckCylTcCount()

        Try

            Dim intCountCyl As Integer, intCountTc As Integer

            ''シリンダ数の取得
            If txtCylinderCount.Text <> "0" Then
                intCountCyl = Integer.Parse(txtCylinderCount.Text)
            End If

            ''T/C数 数値以外の場合は強制的に０を書く
            If Not IsNumeric(txtTcCount.Text) Then
                txtTcCount.Text = "0"
            End If

            ''T/C数の取得
            If txtTcCount.Text <> "0" Then
                intCountTc = Integer.Parse(txtTcCount.Text)
            End If

            ''20Graphの表示設定
            '▼▼▼ 20110330 ファイルデータ１７版対応 20Graph削除 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            ' ''シリンダ数とT/C数の合計が17本以上かつ20本以下の場合Enableとする。
            'If (intCountCyl + intCountTc) >= gCstCodeOps20GraphSplitLower And _
            '   (intCountCyl + intCountTc) <= gCstCodeOps20GraphSplitUpper Then

            '    chk20Graph.Enabled = True
            '    fra20Graph.Enabled = False

            'Else

            '    chk20Graph.Enabled = False
            '    chk20Graph.Checked = False
            '    fra20Graph.Enabled = False

            'End If

            ' ''コントロール使用可/不可設定
            'fra20Graph.Enabled = IIf(chk20Graph.Checked, True, False)

            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 背景色の設定
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 行数によって背景色設定（偶数：青　奇数：白）
    '--------------------------------------------------------------------
    Private Sub mColorSet()

        Try

            Dim CellStyleWhite As New Color
            Dim CellStyleBlue As New Color

            ''背景色の設定
            CellStyleWhite = gColorGridRowBackBase
            CellStyleBlue = gColorGridRowBack

            For i As Integer = 0 To grdTC.RowCount - 1

                ''偶数行：青、奇数行：白
                If (i + 1) Mod 2 <> 0 Then
                    grdTC.Rows(i).Cells("chkSplitLine").Style.BackColor = CellStyleWhite
                Else
                    grdTC.Rows(i).Cells("chkSplitLine").Style.BackColor = CellStyleBlue
                End If

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub



#Region "T/C 数値入力した際の表示調整"

    ''行数調整
    Private Sub mSetTcRowAdjust(ByVal hintTcCntBefore As Integer)

        Try

            ''末尾のSplitチェック ReadOnly解除
            If hintTcCntBefore <> 0 Then
                grdTC.Rows(hintTcCntBefore - 1).Cells("chkSplitLine").ReadOnly = False
            End If

            ''行をMAXまで増やす
            Call mSetGridRowMax()

            ''不要行の削除
            Call mSetGridRowCount(CCInt(mudtSetOpsGraphNew.bytTcCnt))

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    ''部品コントロール
    Private Sub mSetTcControl()

        Try

            ''Splitの使用可/不可設定
            Call mSetControlEnable()

            ''末尾のSplitチェックは強制的に無効
            If CCInt(mudtSetOpsGraphNew.bytTcCnt) <> 0 Then
                grdTC.Rows(CCInt(mudtSetOpsGraphNew.bytTcCnt) - 1).Cells("chkSplitLine").Value = 0
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    ''背景色の設定
    Private Sub mSetTcRowBackColor()

        Try

            ''背景色の設定
            Call mColorSet()

            ''最後尾のSplitをReadOnly色設定
            If txtTcCount.Text <> 0 Then
                grdTC.Rows(CCInt(mudtSetOpsGraphNew.bytTcCnt) - 1).Cells("chkSplitLine").Style.BackColor = gColorGridRowBackReadOnly
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region



#End Region

End Class
