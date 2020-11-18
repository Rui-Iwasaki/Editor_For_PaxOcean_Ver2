Public Class frmChControlUseNotuseDetail

#Region "変数定義"

    Private mudtSetCtrlUseNotuse As gTypSetChCtrlUseRec = Nothing

    Private mintRtn As Integer              ''ボタンフラグ
    Private mintDispNo As Integer           ''選択項目番号

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 0：OK  <> 0：キャンセル
    ' 引き数    : ARG1 - (IO) コントロール使用可／不可設定構造体
    '           : ARG2 - (I ) 行Index
    ' 機能説明  : 本画面を表示する
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Function gShow(ByRef udtSetCtrl As gTypSetChCtrlUseRec, _
                          ByVal intRow As Integer, _
                          ByRef frmOwner As Form) As Integer

        Try

            ''ボタン選択フラグ初期化
            mintRtn = 1

            ''引数保存
            mudtSetCtrlUseNotuse = udtSetCtrl

            ''項目番号の保存
            mintDispNo = intRow + 1

            ''本画面表示
            Call gShowFormModelessForCloseWait2(Me, frmOwner)

            ''OKで閉じる場合は戻り値設定
            If mintRtn = 0 Then udtSetCtrl = mudtSetCtrlUseNotuse

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
    Private Sub frmChControlUseNotuseDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''グリッド 初期設定
            Call mInitialDataGrid()

            ''コンボボックス初期設定
            Call gSetComboBox(cmbFlg, gEnmComboType.ctChCtrlUseNotuseDetail)

            ''画面左上 No設定
            lblNo.Text = mintDispNo

            ''画面設定
            Call mSetDisplay(mudtSetCtrlUseNotuse)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub frmChControlUseNotuseDetail_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        Try

            ''画面表示時のセル選択を解除
            grdUse.CurrentCell = Nothing

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームクローズ
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmChControlUseNotuseDetail_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

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
            Call grdUse.EndEdit()

            ''入力チェック
            If Not mChkInput() Then Return

            ''設定値の保存
            Call mSetStructure(mudtSetCtrlUseNotuse)

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





#Region "イベント処理"

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベントを発生させる
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdUse_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdUse.EditingControlShowing

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdUse_KeyPress

                ''該当する列ならイベントハンドラを追加する
                If dgv.CurrentCell.OwningColumn.Name.Substring(0, 3) = "txt" Then

                    AddHandler tb.KeyPress, AddressOf grdUse_KeyPress

                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 入力値をフォーマットする
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdUse_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdUse.CellValidated

        Try

            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If dgv.CurrentCell.OwningColumn.Name = "txtCHNO" Then

                If IsNumeric(grdUse.Rows(e.RowIndex).Cells("txtCHNO").Value) Then

                    grdUse.Rows(e.RowIndex).Cells("txtCHNO").Value = Format(CInt(grdUse.Rows(e.RowIndex).Cells(0).Value), "0000")

                    '' Ver1.9.7 2016.02.17  CHNo.が入力された時、コンボボックスが未設定ならばBitAndを設定する
                    If Trim(grdUse.Rows(e.RowIndex).Cells("cmbType").Value) = "" Then
                        grdUse.Rows(e.RowIndex).Cells("cmbType").Value = gConvZeroToNull(3)
                        'Ver2.0.0.4 さらにBitに64を格納する
                        grdUse.Rows(e.RowIndex).Cells("txtBit").Value = "64"

                    End If
                    ''//

                End If

                'Ver2.0.0.4
                'CHNoが入力されている件数をカウントして設定
                Dim intRecCount As Integer = 0
                With grdUse
                    For i = 0 To grdUse.RowCount - 1 Step 1
                        If .Rows(i).Cells("txtCHNO").Value <> "" And IsNumeric(.Rows(i).Cells("txtCHNO").Value) Then
                            intRecCount += 1
                        End If
                    Next i
                End With
                txtCount.Text = intRecCount
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub





#Region "入力チェック"

    '----------------------------------------------------------------------------
    ' 機能説明  ： テキストボックス　入力制限
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtCount_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtCount.Validating

        Try

            ''------------------------------------------------------------
            '' 入力チェックは mChkInput で実施するよう変更
            ''------------------------------------------------------------

            'e.Cancel = gChkTextNumSpan(0, 32, sender.Text)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 入力チェック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdUse_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles grdUse.CellValidating

        Try

            ''------------------------------------------------------------
            '' 入力チェックは mChkInput で実施するよう変更
            ''------------------------------------------------------------

            ''Dim dgv As DataGridView = CType(sender, DataGridView)
            ''Dim strColumnName = dgv.Columns(e.ColumnIndex).Name

            '' ''[CH_NO.]
            ''If strColumnName = "txtCHNO" Then
            ''    e.Cancel = gChkTextNumSpan(0, 65535, e.FormattedValue)
            ''End If

            '' ''[Bit],[Comm1],[Comm2]
            ''If strColumnName = "txtBit" Or strColumnName = "txtComm1" Or strColumnName = "txtComm2" Then
            ''    e.Cancel = gChkTextNumSpan(0, 255, e.FormattedValue)
            ''End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "KeyPressイベント"

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベントを発生させる
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtCount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCount.KeyPress

        Try

            e.Handled = gCheckTextInput(2, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベント発生時処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdUse_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdUse.KeyPress

        Try

            ''選択セルの名称取得
            Dim strColumnName As String = grdUse.CurrentCell.OwningColumn.Name

            ''[CH_NO.]
            If strColumnName = "txtCHNO" Then
                e.Handled = gCheckTextInput(5, sender, e.KeyChar)
            End If

            ''[Bit],[Comm1],[Comm2]
            If strColumnName = "txtBit" Or strColumnName = "txtComm1" Or strColumnName = "txtComm2" Then
                e.Handled = gCheckTextInput(3, sender, e.KeyChar)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region





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

            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "txtCHNO"
            Dim Column2 As New DataGridViewComboBoxColumn : Column2.Name = "cmbType"
            Dim Column3 As New DataGridViewTextBoxColumn : Column3.Name = "txtBit"
            Dim Column4 As New DataGridViewTextBoxColumn : Column4.Name = "txtComm1"
            Dim Column5 As New DataGridViewTextBoxColumn : Column5.Name = "txtComm2"

            Column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Column3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            With grdUse

                ''列
                .Columns.Clear()
                .Columns.Add(Column1) : .Columns.Add(Column2)
                .Columns.Add(Column3) : .Columns.Add(Column4)
                .Columns.Add(Column5)
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing  ''行ヘッダー幅の変更不可

                '全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "CH No." : .Columns(0).Width = 110
                .Columns(1).HeaderText = "Type" : .Columns(1).Width = 150
                .Columns(2).HeaderText = "Bit" : .Columns(2).Width = 50
                .Columns(3).HeaderText = "Comm1" : .Columns(3).Width = 60
                .Columns(4).HeaderText = "Comm2" : .Columns(4).Width = 60
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter    ''列ヘッダー　センタリング

                ''行
                .RowCount = 32 + 1
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
                .ScrollBars = ScrollBars.Vertical

                ''コンボボックス初期設定
                Call gSetComboBox(Column2, gEnmComboType.ctChCtrlUseNotuseDetailgrd)

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

            Dim strCH As String
            Dim intType As Integer
            Dim strBit As String, strComm1 As String, strComm2 As String

            ''入力値のレンジチェック
            If Not gChkInputNum(txtCount, 0, 32, "Count", False, True) Then Return False

            For i As Integer = 0 To grdUse.RowCount - 1

                ''入力値のレンジチェック
                If Not gChkInputNum(grdUse.Rows(i).Cells("txtChNo"), 0, 65535, "CH No.", i + 1, True, True) Then Return False
                If Not gChkInputNum(grdUse.Rows(i).Cells("txtBit"), 0, 255, "Bit", i + 1, True, True) Then Return False
                If Not gChkInputNum(grdUse.Rows(i).Cells("txtComm1"), 0, 255, "Comm1", i + 1, True, True) Then Return False
                If Not gChkInputNum(grdUse.Rows(i).Cells("txtComm2"), 0, 255, "Comm2", i + 1, True, True) Then Return False


                strCH = gGetString(grdUse.Rows(i).Cells(0).Value)
                intType = CCInt(grdUse.Rows(i).Cells(1).Value)
                strBit = gGetString(grdUse.Rows(i).Cells(2).Value)
                strComm1 = gGetString(grdUse.Rows(i).Cells(3).Value)
                strComm2 = gGetString(grdUse.Rows(i).Cells(4).Value)


                ''入力箇所のチェック
                If strCH = "" And intType = 0 And strBit = "0" And strComm1 = "0" And strComm2 = "0" Then
                    ''OK
                ElseIf strCH = "" And intType = 0 And strBit = "" And strComm1 = "0" And strComm2 = "0" Then
                    ''OK
                ElseIf strCH = "" And intType = 0 And strBit = "0" And strComm1 = "" And strComm2 = "0" Then
                    ''OK
                ElseIf strCH = "" And intType = 0 And strBit = "0" And strComm1 = "0" And strComm2 = "" Then
                    ''OK
                ElseIf strCH = "" And intType = 0 And strBit = "" And strComm1 = "" And strComm2 = "0" Then
                    ''OK
                ElseIf strCH = "" And intType = 0 And strBit = "" And strComm1 = "0" And strComm2 = "" Then
                    ''OK
                ElseIf strCH = "" And intType = 0 And strBit = "0" And strComm1 = "" And strComm2 = "" Then
                    ''OK
                ElseIf strCH = "" And intType = 0 And strBit = "" And strComm1 = "" And strComm2 = "" Then
                    ''OK

                Else

                    If strCH = "" Then
                        Call MessageBox.Show("Please set 'CH No.' data of the line No." & i + 1, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False
                    End If

                    If intType = 0 Then
                        Call MessageBox.Show("Please set 'Type' data of the line No." & i + 1, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False
                    End If

                    If strBit = "" Then
                        Call MessageBox.Show("Please set 'Bit' data of the line No." & i + 1, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False
                    End If

                    If strComm1 = "" Then
                        Call MessageBox.Show("Please set 'Comm1' data of the line No." & i + 1, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False
                    End If

                    If strComm2 = "" Then
                        Call MessageBox.Show("Please set 'Comm2' data of the line No." & i + 1, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False
                    End If

                End If

                ''CH番号の重複登録は不可
                ''Dim j As Integer

                ''For j = i + 1 To grdUse.RowCount - 1

                ''    If gGetString(grdUse(0, i).Value) <> "" Then

                ''        If gGetString(grdUse(0, i).Value) = gGetString(grdUse(0, j).Value) Then

                ''            Call MessageBox.Show("The same name as [CH No.] cannot be set of CH No [" & grdUse(0, i).Value & "] and CH No [" & grdUse(0, j).Value & "].", _
                ''                                 "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ''            Return False

                ''        End If

                ''    End If

                ''Next j

            Next i

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
    Private Sub mSetStructure(ByRef udtSet As gTypSetChCtrlUseRec)

        Try

            ''条件数
            udtSet.shtCount = CCShort(txtCount.Text)

            ''条件種類
            udtSet.bytFlg = CCbyte(cmbFlg.SelectedValue)

            For intRow As Integer = 0 To UBound(udtSet.udtUseNotuseDetails)

                With udtSet.udtUseNotuseDetails(intRow)

                    .shtChno = CCUInt16(grdUse.Rows(intRow).Cells("txtCHNO").Value)         ''CH NO.
                    .bytType = CCbyte(grdUse.Rows(intRow).Cells("cmbType").Value)           ''条件タイプ
                    .shtBit = CCShort(grdUse.Rows(intRow).Cells("txtBit").Value)            ''ビット条件
                    .shtProcess1 = CCShort(grdUse.Rows(intRow).Cells("txtComm1").Value)     ''Process1
                    .shtProcess2 = CCShort(grdUse.Rows(intRow).Cells("txtComm2").Value)     ''Process2

                End With

            Next

            'Ver2.0.6.1
            '条件数or条件種類が０ならば詳細は全てゼロクリア
            If udtSet.shtCount = 0 Or udtSet.bytFlg = 0 Then
                udtSet.shtCount = 0
                udtSet.bytFlg = 0
                For i = 0 To UBound(udtSet.udtUseNotuseDetails)
                    With udtSet.udtUseNotuseDetails(i)
                        .shtChno = 0
                        .bytType = 0
                        .shtBit = 0
                        .shtProcess1 = 0
                        .shtProcess2 = 0
                    End With
                Next i
            End If

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
    Private Sub mSetDisplay(ByRef udtSet As gTypSetChCtrlUseRec)

        Try

            ''条件数
            txtCount.Text = udtSet.shtCount.ToString

            ''条件種類
            cmbFlg.SelectedValue = udtSet.bytFlg.ToString

            For intRow As Integer = 0 To UBound(udtSet.udtUseNotuseDetails)

                With udtSet.udtUseNotuseDetails(intRow)

                    grdUse.Rows(intRow).Cells("txtCHNO").Value = gConvZeroToNull(.shtChno, "0000")  ''CH NO.
                    grdUse.Rows(intRow).Cells("cmbType").Value = gConvZeroToNull(.bytType)          ''条件タイプ
                    grdUse.Rows(intRow).Cells("txtBit").Value = .shtBit                             ''ビット条件
                    grdUse.Rows(intRow).Cells("txtComm1").Value = .shtProcess1                      ''Process1
                    grdUse.Rows(intRow).Cells("txtComm2").Value = .shtProcess2                      ''Process2

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

    Private Sub grdUse_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdUse.CellContentClick

    End Sub
End Class
